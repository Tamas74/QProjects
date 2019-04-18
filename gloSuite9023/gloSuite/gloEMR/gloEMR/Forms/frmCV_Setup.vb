Public Class frmCV_Setup

    Inherits System.Windows.Forms.Form
    ' page Coded by Bipin On 15/1/2007
#Region " Variables "

    Private m_CriteriaId As Int64
    Private m_CriteriaName As String
    Private blnModify As Boolean = False

    Private _LAB_Category As String = "C"
    Private _LAB_Group As String = "G"
    Private _LAB_Test As String = "T"

    Dim strHeight As String
    Dim strHeightMax As String

    Private COL_NAME As Integer = 0
    Private COL_ID As Integer = 1
    Private COL_TESTGROUPFLAG As Integer = 2
    Private COL_LEVELNO As Integer = 3
    Private COL_GROUPNO As Integer = 4
    Private COL_MINVALUE As Integer = 5
    Private COL_MAXVALUE As Integer = 6
    Private COL_IDENTITY As Integer = 7

    Private COL_COUNT As Integer = 8
    Private _selectednode As TreeNode
    Private _selectedmynode As myTreeNode
    Private isHistoryLoaded As Boolean = False
    Private isDrugsLoaded As Boolean = False
    '''' For LabResult Flexgrid

    Private _LabModule_Result As String = "R"
    Private COL_TestID As Integer = 0
    Private COL_TestName As Integer = 1
    Private COL_ResultID As Integer = 2
    Private COL_Operator As Integer = 3
    Private COL_ResultValue1 As Integer = 4
    Private COL_ResultValue2 As Integer = 5
    Private COL_IDENTITYModule As Integer = 6
    Private COL_CountLab As Integer = 7

#End Region

#Region " Windows Controls "
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Friend WithEvents btnLabs As System.Windows.Forms.Button
    Friend WithEvents pnl_tlstrip As System.Windows.Forms.Panel
    Friend WithEvents pnlRight As System.Windows.Forms.Panel
    Friend WithEvents btnLab As System.Windows.Forms.Button
    Friend WithEvents btnRadiologyTest As System.Windows.Forms.Button
    Friend WithEvents btnGuideline As System.Windows.Forms.Button
    Friend WithEvents trvTriggers As System.Windows.Forms.TreeView
    Friend WithEvents pnlSummary As System.Windows.Forms.Panel
    Friend WithEvents pnlSummaryHeader As System.Windows.Forms.Panel
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txt_summary As System.Windows.Forms.TextBox
    Friend WithEvents tlsDM As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents tlsDM_Save As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlsDM_Close As System.Windows.Forms.ToolStripButton
    Friend WithEvents pnlMsg As System.Windows.Forms.Panel
    Friend WithEvents pnlDemoVitals As System.Windows.Forms.Panel
    Friend WithEvents pnlLab As System.Windows.Forms.Panel
    Friend WithEvents btnReferrals As System.Windows.Forms.Button
    Friend WithEvents btnRx As System.Windows.Forms.Button
    Friend WithEvents CntConditions As System.Windows.Forms.ContextMenu
    Friend WithEvents mnuDelete As System.Windows.Forms.MenuItem
    Friend WithEvents mnuReferral As System.Windows.Forms.MenuItem
    Friend WithEvents EditReferral As System.Windows.Forms.MenuItem
    Friend WithEvents pnltxtSearchOrder As System.Windows.Forms.Panel
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Private WithEvents pnbtnDemographics As System.Windows.Forms.Panel
    Private WithEvents label58 As System.Windows.Forms.Label
    Private WithEvents label59 As System.Windows.Forms.Label
    Private WithEvents label60 As System.Windows.Forms.Label
    Private WithEvents label61 As System.Windows.Forms.Label
    Private WithEvents pnlbtnOrders As System.Windows.Forms.Panel
    Private WithEvents Label51 As System.Windows.Forms.Label
    Private WithEvents Label52 As System.Windows.Forms.Label
    Private WithEvents Label53 As System.Windows.Forms.Label
    Private WithEvents Label54 As System.Windows.Forms.Label
    Private WithEvents pnlbtnRadiology As System.Windows.Forms.Panel
    Private WithEvents Label47 As System.Windows.Forms.Label
    Private WithEvents Label48 As System.Windows.Forms.Label
    Private WithEvents Label49 As System.Windows.Forms.Label
    Private WithEvents Label50 As System.Windows.Forms.Label
    Private WithEvents pnlbtnLabs As System.Windows.Forms.Panel
    Private WithEvents Label43 As System.Windows.Forms.Label
    Private WithEvents Label44 As System.Windows.Forms.Label
    Private WithEvents Label45 As System.Windows.Forms.Label
    Private WithEvents Label46 As System.Windows.Forms.Label
    Private WithEvents pnlbtnCPT As System.Windows.Forms.Panel
    Private WithEvents Label39 As System.Windows.Forms.Label
    Private WithEvents Label40 As System.Windows.Forms.Label
    Private WithEvents Label41 As System.Windows.Forms.Label
    Private WithEvents Label42 As System.Windows.Forms.Label
    Private WithEvents pnlbtnICD9 As System.Windows.Forms.Panel
    Private WithEvents Label35 As System.Windows.Forms.Label
    Private WithEvents Label36 As System.Windows.Forms.Label
    Private WithEvents Label37 As System.Windows.Forms.Label
    Private WithEvents Label38 As System.Windows.Forms.Label
    Private WithEvents pnlbtnDrugs As System.Windows.Forms.Panel
    Private WithEvents Label31 As System.Windows.Forms.Label
    Private WithEvents Label32 As System.Windows.Forms.Label
    Private WithEvents Label33 As System.Windows.Forms.Label
    Private WithEvents Label34 As System.Windows.Forms.Label
    Private WithEvents pnlbtnHistory As System.Windows.Forms.Panel
    Private WithEvents Label27 As System.Windows.Forms.Label
    Private WithEvents Label28 As System.Windows.Forms.Label
    Private WithEvents Label29 As System.Windows.Forms.Label
    Private WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents pnlbtnRadiologyTest As System.Windows.Forms.Panel
    Private WithEvents Label55 As System.Windows.Forms.Label
    Private WithEvents Label56 As System.Windows.Forms.Label
    Private WithEvents Label57 As System.Windows.Forms.Label
    Private WithEvents Label62 As System.Windows.Forms.Label
    Friend WithEvents pnlbtnGuideline As System.Windows.Forms.Panel
    Private WithEvents Label17 As System.Windows.Forms.Label
    Private WithEvents Label18 As System.Windows.Forms.Label
    Private WithEvents Label19 As System.Windows.Forms.Label
    Private WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents pnlbtnReferrals As System.Windows.Forms.Panel
    Private WithEvents Label70 As System.Windows.Forms.Label
    Private WithEvents Label71 As System.Windows.Forms.Label
    Private WithEvents Label72 As System.Windows.Forms.Label
    Private WithEvents Label73 As System.Windows.Forms.Label
    Friend WithEvents pnlbtnRx As System.Windows.Forms.Panel
    Private WithEvents Label66 As System.Windows.Forms.Label
    Private WithEvents Label67 As System.Windows.Forms.Label
    Private WithEvents Label68 As System.Windows.Forms.Label
    Private WithEvents Label69 As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Private WithEvents Label15 As System.Windows.Forms.Label
    Private WithEvents Label82 As System.Windows.Forms.Label
    Private WithEvents Label83 As System.Windows.Forms.Label
    Private WithEvents Label84 As System.Windows.Forms.Label
    Friend WithEvents pnlbtnLab As System.Windows.Forms.Panel
    Private WithEvents Label78 As System.Windows.Forms.Label
    Private WithEvents Label79 As System.Windows.Forms.Label
    Private WithEvents Label80 As System.Windows.Forms.Label
    Private WithEvents Label81 As System.Windows.Forms.Label
    Friend WithEvents pnltrvTriggers As System.Windows.Forms.Panel
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Private WithEvents Label74 As System.Windows.Forms.Label
    Private WithEvents Label75 As System.Windows.Forms.Label
    Private WithEvents Label76 As System.Windows.Forms.Label
    Private WithEvents Label77 As System.Windows.Forms.Label
    Friend WithEvents sptLeft As System.Windows.Forms.Splitter
    Private WithEvents Label23 As System.Windows.Forms.Label
    Private WithEvents Label24 As System.Windows.Forms.Label
    Private WithEvents Label63 As System.Windows.Forms.Label
    Friend WithEvents pnlMsgTOP As System.Windows.Forms.Panel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Private WithEvents Label26 As System.Windows.Forms.Label
    Private WithEvents Label64 As System.Windows.Forms.Label
    Private WithEvents Label87 As System.Windows.Forms.Label
    Private WithEvents Label88 As System.Windows.Forms.Label
    Private WithEvents Label91 As System.Windows.Forms.Label
    Private WithEvents Label92 As System.Windows.Forms.Label
    Private WithEvents Label93 As System.Windows.Forms.Label
    Private WithEvents Label94 As System.Windows.Forms.Label
    Private WithEvents Label25 As System.Windows.Forms.Label
    Private WithEvents Label111 As System.Windows.Forms.Label
    Private WithEvents Label112 As System.Windows.Forms.Label
    Private WithEvents Label113 As System.Windows.Forms.Label
    Private WithEvents Label114 As System.Windows.Forms.Label
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Private WithEvents Label107 As System.Windows.Forms.Label
    Private WithEvents Label108 As System.Windows.Forms.Label
    Private WithEvents Label109 As System.Windows.Forms.Label
    Private WithEvents Label110 As System.Windows.Forms.Label
    Private WithEvents Label103 As System.Windows.Forms.Label
    Private WithEvents Label104 As System.Windows.Forms.Label
    Private WithEvents Label105 As System.Windows.Forms.Label
    Private WithEvents Label106 As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Private WithEvents Label65 As System.Windows.Forms.Label
    Private WithEvents Label85 As System.Windows.Forms.Label
    Private WithEvents Label89 As System.Windows.Forms.Label
    Private WithEvents Label90 As System.Windows.Forms.Label
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Private WithEvents Label13 As System.Windows.Forms.Label
    Private WithEvents Label128 As System.Windows.Forms.Label
    Private WithEvents Label129 As System.Windows.Forms.Label
    Private WithEvents Label130 As System.Windows.Forms.Label
    Friend WithEvents Panel10 As System.Windows.Forms.Panel
    Friend WithEvents Label131 As System.Windows.Forms.Label
    Friend WithEvents Label132 As System.Windows.Forms.Label
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Private WithEvents Label133 As System.Windows.Forms.Label
    Private WithEvents Label134 As System.Windows.Forms.Label
    Private WithEvents Label135 As System.Windows.Forms.Label
    Private WithEvents Label136 As System.Windows.Forms.Label
    Friend WithEvents Panel12 As System.Windows.Forms.Panel
    Private WithEvents Label146 As System.Windows.Forms.Label
    Private WithEvents Label147 As System.Windows.Forms.Label
    Private WithEvents Label148 As System.Windows.Forms.Label
    Private WithEvents Label149 As System.Windows.Forms.Label
    Friend WithEvents Panel11 As System.Windows.Forms.Panel
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label137 As System.Windows.Forms.Label
    Friend WithEvents PictureBox4 As System.Windows.Forms.PictureBox
    Private WithEvents Label138 As System.Windows.Forms.Label
    Private WithEvents Label139 As System.Windows.Forms.Label
    Private WithEvents Label140 As System.Windows.Forms.Label
    Private WithEvents Label141 As System.Windows.Forms.Label
    Friend WithEvents Panel13 As System.Windows.Forms.Panel
    Friend WithEvents Panel14 As System.Windows.Forms.Panel
    Private WithEvents Label150 As System.Windows.Forms.Label
    Private WithEvents Label151 As System.Windows.Forms.Label
    Private WithEvents Label152 As System.Windows.Forms.Label
    Private WithEvents Label153 As System.Windows.Forms.Label
    Friend WithEvents Panel16 As System.Windows.Forms.Panel
    Friend WithEvents Label159 As System.Windows.Forms.Label
    Friend WithEvents Label160 As System.Windows.Forms.Label
    Friend WithEvents PictureBox5 As System.Windows.Forms.PictureBox
    Private WithEvents Label161 As System.Windows.Forms.Label
    Private WithEvents Label162 As System.Windows.Forms.Label
    Private WithEvents Label163 As System.Windows.Forms.Label
    Private WithEvents Label164 As System.Windows.Forms.Label
    Friend WithEvents Panel15 As System.Windows.Forms.Panel
    Private WithEvents Label154 As System.Windows.Forms.Label
    Private WithEvents Label156 As System.Windows.Forms.Label
    Private WithEvents Label157 As System.Windows.Forms.Label
    Private WithEvents Label158 As System.Windows.Forms.Label
    Friend WithEvents Label155 As System.Windows.Forms.Label
    Friend WithEvents Panel18 As System.Windows.Forms.Panel
    Private WithEvents Label170 As System.Windows.Forms.Label
    Private WithEvents Label171 As System.Windows.Forms.Label
    Private WithEvents Label172 As System.Windows.Forms.Label
    Private WithEvents Label173 As System.Windows.Forms.Label
    Friend WithEvents Panel17 As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label165 As System.Windows.Forms.Label
    Friend WithEvents PictureBox6 As System.Windows.Forms.PictureBox
    Private WithEvents Label166 As System.Windows.Forms.Label
    Private WithEvents Label167 As System.Windows.Forms.Label
    Private WithEvents Label168 As System.Windows.Forms.Label
    Private WithEvents Label169 As System.Windows.Forms.Label
    Friend WithEvents Panel19 As System.Windows.Forms.Panel
    Private WithEvents Label174 As System.Windows.Forms.Label
    Private WithEvents Label175 As System.Windows.Forms.Label
    Private WithEvents Label176 As System.Windows.Forms.Label
    Private WithEvents Label177 As System.Windows.Forms.Label
    Friend WithEvents mnuEditTemplate As System.Windows.Forms.MenuItem
    Private WithEvents Label179 As System.Windows.Forms.Label
    Friend WithEvents pnlSelectedDrugLabel As System.Windows.Forms.Panel
    Friend WithEvents Panel20 As System.Windows.Forms.Panel
    Private WithEvents Label185 As System.Windows.Forms.Label
    Private WithEvents Label184 As System.Windows.Forms.Label
    Private WithEvents Label186 As System.Windows.Forms.Label
    Friend WithEvents Label187 As System.Windows.Forms.Label
    Private WithEvents Label183 As System.Windows.Forms.Label
    Friend WithEvents pnltrvSelectedDrugs As System.Windows.Forms.Panel
    Private WithEvents Label86 As System.Windows.Forms.Label
    Private WithEvents Label180 As System.Windows.Forms.Label
    Friend WithEvents trvSelectedDrugs As System.Windows.Forms.TreeView
    Private WithEvents Label181 As System.Windows.Forms.Label
    Private WithEvents Label182 As System.Windows.Forms.Label
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents mnuDeleteDrugs As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents C1LabResult As C1.Win.C1FlexGrid.C1FlexGrid
    Private WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents pnlHistory As System.Windows.Forms.Panel
    Friend WithEvents Panel9 As System.Windows.Forms.Panel
    Friend WithEvents trvHistoryRight As System.Windows.Forms.TreeView
    Friend WithEvents pnlHistoryLeft As System.Windows.Forms.Panel
    Private WithEvents Label120 As System.Windows.Forms.Label
    Private WithEvents Label121 As System.Windows.Forms.Label
    Private WithEvents Label122 As System.Windows.Forms.Label
    Private WithEvents Label123 As System.Windows.Forms.Label
    Friend WithEvents trvHistory As System.Windows.Forms.TreeView
    Friend WithEvents Panel8 As System.Windows.Forms.Panel
    Friend WithEvents Panel21 As System.Windows.Forms.Panel
    Private WithEvents Label188 As System.Windows.Forms.Label
    Private WithEvents Label189 As System.Windows.Forms.Label
    Private WithEvents Label190 As System.Windows.Forms.Label
    Friend WithEvents Label191 As System.Windows.Forms.Label
    Private WithEvents Label192 As System.Windows.Forms.Label
    Friend WithEvents Panel22 As System.Windows.Forms.Panel
    Private WithEvents Label193 As System.Windows.Forms.Label
    Private WithEvents Label194 As System.Windows.Forms.Label
    Friend WithEvents trvSelectedHistory As System.Windows.Forms.TreeView
    Private WithEvents Label195 As System.Windows.Forms.Label
    Private WithEvents Label196 As System.Windows.Forms.Label
    Friend WithEvents Panel23 As System.Windows.Forms.Panel
    Friend WithEvents Panel24 As System.Windows.Forms.Panel
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Private WithEvents Label116 As System.Windows.Forms.Label
    Private WithEvents Label117 As System.Windows.Forms.Label
    Private WithEvents Label118 As System.Windows.Forms.Label
    Private WithEvents Label119 As System.Windows.Forms.Label
    Friend WithEvents cmbHistoryCategory As System.Windows.Forms.ComboBox
    Friend WithEvents btnHistorySearch As System.Windows.Forms.Button
    Friend WithEvents ContextMenuHistory As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents mnuDeleteHistory As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents GloUC_trvDrugs As gloUserControlLibrary.gloUC_TreeView
    Friend WithEvents GloUC_trvHistory As gloUserControlLibrary.gloUC_TreeView
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
    Friend WithEvents Splitter2 As System.Windows.Forms.Splitter
    Friend WithEvents Label95 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label98 As System.Windows.Forms.Label
    Friend WithEvents Label97 As System.Windows.Forms.Label
    Friend WithEvents Label96 As System.Windows.Forms.Label
    Friend WithEvents Panel25 As System.Windows.Forms.Panel
    Friend WithEvents txtLabResultSearch As System.Windows.Forms.TextBox
    Friend WithEvents Label126 As System.Windows.Forms.Label
    Friend WithEvents Label127 As System.Windows.Forms.Label
    Friend WithEvents PictureBox3 As System.Windows.Forms.PictureBox
    Private WithEvents Label99 As System.Windows.Forms.Label
    Private WithEvents Label100 As System.Windows.Forms.Label
    Private WithEvents Label101 As System.Windows.Forms.Label
    Private WithEvents Label102 As System.Windows.Forms.Label
    Friend WithEvents Label115 As System.Windows.Forms.Label
    Private WithEvents Label178 As System.Windows.Forms.Label
#End Region

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub
    Public Sub New(ByVal IsModify As Boolean, ByVal CriteriaID As Int64, ByVal CriteriaName As String)
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()
        blnModify = IsModify
        m_CriteriaId = CriteriaID
        m_CriteriaName = CriteriaName

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            Dim CmpControls() As System.Windows.Forms.ContextMenuStrip = {ContextMenuHistory, ContextMenuStrip1}

            If Not (components Is Nothing) Then
                components.Dispose()
            End If

            Try
                gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
            Catch
            End Try




            If (IsNothing(CmpControls) = False) Then
                If CmpControls.Length > 0 Then
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(CmpControls)
                End If
            End If

            If (IsNothing(ContextMenuHistory.Items) = False) Then
                ContextMenuHistory.Items.Clear()
            End If


            If (IsNothing(ContextMenuStrip1.Items) = False) Then
                ContextMenuStrip1.Items.Clear()
            End If

            If (IsNothing(CmpControls) = False) Then
                If CmpControls.Length > 0 Then
                    gloGlobal.cEventHelper.DisposeContextMenuStrip(CmpControls)
                End If
            End If



        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents pnlLeft As System.Windows.Forms.Panel
    Friend WithEvents btnDemographics As System.Windows.Forms.Button
    Friend WithEvents btnOrders As System.Windows.Forms.Button
    Friend WithEvents cmbGender As System.Windows.Forms.ComboBox
    Friend WithEvents cmbRace As System.Windows.Forms.ComboBox
    Friend WithEvents cmbMaritalSt As System.Windows.Forms.ComboBox
    Friend WithEvents pnlDemographics As System.Windows.Forms.Panel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents pnlVitals As System.Windows.Forms.Panel
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtCity As System.Windows.Forms.TextBox
    Friend WithEvents cmbState As System.Windows.Forms.ComboBox
    Friend WithEvents txtZip As System.Windows.Forms.TextBox
    Friend WithEvents cmbEmpStatus As System.Windows.Forms.ComboBox
    Friend WithEvents txtBPsettingMin As System.Windows.Forms.TextBox
    Friend WithEvents txtBPsettingMax As System.Windows.Forms.TextBox
    Friend WithEvents txtBPstandingMin As System.Windows.Forms.TextBox
    Friend WithEvents txtBPstandingMax As System.Windows.Forms.TextBox
    Friend WithEvents txtDrugSearch As System.Windows.Forms.TextBox
    Friend WithEvents txtCPTsearch As System.Windows.Forms.TextBox
    Friend WithEvents txtLabsSearch As System.Windows.Forms.TextBox
    Friend WithEvents pnlDrugs As System.Windows.Forms.Panel
    Friend WithEvents pnlICD9 As System.Windows.Forms.Panel
    Friend WithEvents pnlCPT As System.Windows.Forms.Panel
    Friend WithEvents pnlRadiology As System.Windows.Forms.Panel
    Friend WithEvents pnlSummaryOthers As System.Windows.Forms.Panel
    Friend WithEvents pnlMiddle As System.Windows.Forms.Panel
    Friend WithEvents btnRadiology As System.Windows.Forms.Button
    Friend WithEvents btnCPT As System.Windows.Forms.Button
    Friend WithEvents btnICD9 As System.Windows.Forms.Button
    Friend WithEvents btnDrugs As System.Windows.Forms.Button
    Friend WithEvents btnHistory As System.Windows.Forms.Button
    Friend WithEvents trvDrgs As System.Windows.Forms.TreeView
    Friend WithEvents trvICD9 As System.Windows.Forms.TreeView
    Friend WithEvents trvCPT As System.Windows.Forms.TreeView
    Friend WithEvents txtICD9Search As System.Windows.Forms.TextBox
    Friend WithEvents txtSearchOrder As System.Windows.Forms.TextBox
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents cmbAgeMin As System.Windows.Forms.ComboBox
    Friend WithEvents cmbAgeMax As System.Windows.Forms.ComboBox
    Friend WithEvents txtTemperatureMax As System.Windows.Forms.TextBox
    Friend WithEvents txtBMImax As System.Windows.Forms.TextBox
    Friend WithEvents txtBMImin As System.Windows.Forms.TextBox
    Friend WithEvents txtWeightMax As System.Windows.Forms.TextBox
    Friend WithEvents txtHeightMax As System.Windows.Forms.TextBox
    Friend WithEvents txtTemperatureMin As System.Windows.Forms.TextBox
    Friend WithEvents txtWeightMin As System.Windows.Forms.TextBox
    Friend WithEvents txtHeightMin As System.Windows.Forms.TextBox
    Friend WithEvents txtPulseOXmax As System.Windows.Forms.TextBox
    Friend WithEvents txtPulseOXmin As System.Windows.Forms.TextBox
    Friend WithEvents txtPulseMax As System.Windows.Forms.TextBox
    Friend WithEvents txtPulseMin As System.Windows.Forms.TextBox
    Friend WithEvents lblPulseOXMax As System.Windows.Forms.Label
    Friend WithEvents lblPulseOXMin As System.Windows.Forms.Label
    Friend WithEvents lblPulseMax As System.Windows.Forms.Label
    Friend WithEvents lblPulseMin As System.Windows.Forms.Label
    Friend WithEvents lblTempratureMax As System.Windows.Forms.Label
    Friend WithEvents lblBMImax As System.Windows.Forms.Label
    Friend WithEvents lblBMImin As System.Windows.Forms.Label
    Friend WithEvents lblWeightMax As System.Windows.Forms.Label
    Friend WithEvents lblHeightMax As System.Windows.Forms.Label
    Friend WithEvents lblBPStandingMax As System.Windows.Forms.Label
    Friend WithEvents lblBPStandingMin As System.Windows.Forms.Label
    Friend WithEvents lblBPSittingMax As System.Windows.Forms.Label
    Friend WithEvents lblBPSittingMin As System.Windows.Forms.Label
    Friend WithEvents lblWeightMin As System.Windows.Forms.Label
    Friend WithEvents lblTempratureMin As System.Windows.Forms.Label
    Friend WithEvents lblHeightMin As System.Windows.Forms.Label
    Friend WithEvents lblAgeMax As System.Windows.Forms.Label
    Friend WithEvents lblEmpStatus As System.Windows.Forms.Label
    Friend WithEvents lblZip As System.Windows.Forms.Label
    Friend WithEvents lblState As System.Windows.Forms.Label
    Friend WithEvents lblCity As System.Windows.Forms.Label
    Friend WithEvents lblMaritalStatus As System.Windows.Forms.Label
    Friend WithEvents lblGender As System.Windows.Forms.Label
    Friend WithEvents lblRace As System.Windows.Forms.Label
    Friend WithEvents lblAgeMin As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtName As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtMessage As System.Windows.Forms.TextBox
    Friend WithEvents c1Labs As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtHeightMinInch As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtHeightMaxInch As System.Windows.Forms.TextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCV_Setup))
        Me.pnlMain = New System.Windows.Forms.Panel
        Me.pnlMiddle = New System.Windows.Forms.Panel
        Me.pnlDemoVitals = New System.Windows.Forms.Panel
        Me.pnlVitals = New System.Windows.Forms.Panel
        Me.Label111 = New System.Windows.Forms.Label
        Me.Label112 = New System.Windows.Forms.Label
        Me.Label113 = New System.Windows.Forms.Label
        Me.Label114 = New System.Windows.Forms.Label
        Me.txtHeightMin = New System.Windows.Forms.TextBox
        Me.Label11 = New System.Windows.Forms.Label
        Me.txtHeightMaxInch = New System.Windows.Forms.TextBox
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.txtHeightMinInch = New System.Windows.Forms.TextBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.txtPulseOXmax = New System.Windows.Forms.TextBox
        Me.lblPulseOXMax = New System.Windows.Forms.Label
        Me.txtPulseOXmin = New System.Windows.Forms.TextBox
        Me.lblPulseOXMin = New System.Windows.Forms.Label
        Me.txtPulseMax = New System.Windows.Forms.TextBox
        Me.lblPulseMax = New System.Windows.Forms.Label
        Me.txtPulseMin = New System.Windows.Forms.TextBox
        Me.lblPulseMin = New System.Windows.Forms.Label
        Me.txtTemperatureMax = New System.Windows.Forms.TextBox
        Me.lblTempratureMax = New System.Windows.Forms.Label
        Me.txtBMImax = New System.Windows.Forms.TextBox
        Me.lblBMImax = New System.Windows.Forms.Label
        Me.txtBMImin = New System.Windows.Forms.TextBox
        Me.lblBMImin = New System.Windows.Forms.Label
        Me.txtWeightMax = New System.Windows.Forms.TextBox
        Me.lblWeightMax = New System.Windows.Forms.Label
        Me.txtHeightMax = New System.Windows.Forms.TextBox
        Me.lblHeightMax = New System.Windows.Forms.Label
        Me.txtBPstandingMax = New System.Windows.Forms.TextBox
        Me.txtBPstandingMin = New System.Windows.Forms.TextBox
        Me.txtBPsettingMax = New System.Windows.Forms.TextBox
        Me.txtBPsettingMin = New System.Windows.Forms.TextBox
        Me.lblBPStandingMax = New System.Windows.Forms.Label
        Me.lblBPStandingMin = New System.Windows.Forms.Label
        Me.lblBPSittingMax = New System.Windows.Forms.Label
        Me.lblBPSittingMin = New System.Windows.Forms.Label
        Me.txtTemperatureMin = New System.Windows.Forms.TextBox
        Me.txtWeightMin = New System.Windows.Forms.TextBox
        Me.lblWeightMin = New System.Windows.Forms.Label
        Me.lblTempratureMin = New System.Windows.Forms.Label
        Me.lblHeightMin = New System.Windows.Forms.Label
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.Panel7 = New System.Windows.Forms.Panel
        Me.Label107 = New System.Windows.Forms.Label
        Me.Label108 = New System.Windows.Forms.Label
        Me.Label109 = New System.Windows.Forms.Label
        Me.Label110 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.pnlDemographics = New System.Windows.Forms.Panel
        Me.Label98 = New System.Windows.Forms.Label
        Me.Label97 = New System.Windows.Forms.Label
        Me.Label96 = New System.Windows.Forms.Label
        Me.Label103 = New System.Windows.Forms.Label
        Me.Label104 = New System.Windows.Forms.Label
        Me.Label105 = New System.Windows.Forms.Label
        Me.Label106 = New System.Windows.Forms.Label
        Me.lblEmpStatus = New System.Windows.Forms.Label
        Me.cmbState = New System.Windows.Forms.ComboBox
        Me.cmbEmpStatus = New System.Windows.Forms.ComboBox
        Me.txtZip = New System.Windows.Forms.TextBox
        Me.txtCity = New System.Windows.Forms.TextBox
        Me.lblZip = New System.Windows.Forms.Label
        Me.lblState = New System.Windows.Forms.Label
        Me.lblCity = New System.Windows.Forms.Label
        Me.cmbMaritalSt = New System.Windows.Forms.ComboBox
        Me.cmbRace = New System.Windows.Forms.ComboBox
        Me.cmbAgeMax = New System.Windows.Forms.ComboBox
        Me.cmbAgeMin = New System.Windows.Forms.ComboBox
        Me.cmbGender = New System.Windows.Forms.ComboBox
        Me.lblAgeMax = New System.Windows.Forms.Label
        Me.lblMaritalStatus = New System.Windows.Forms.Label
        Me.lblGender = New System.Windows.Forms.Label
        Me.lblRace = New System.Windows.Forms.Label
        Me.lblAgeMin = New System.Windows.Forms.Label
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.Panel6 = New System.Windows.Forms.Panel
        Me.Label65 = New System.Windows.Forms.Label
        Me.Label85 = New System.Windows.Forms.Label
        Me.Label89 = New System.Windows.Forms.Label
        Me.Label90 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.pnlLab = New System.Windows.Forms.Panel
        Me.Panel12 = New System.Windows.Forms.Panel
        Me.C1LabResult = New C1.Win.C1FlexGrid.C1FlexGrid
        Me.Label146 = New System.Windows.Forms.Label
        Me.Label147 = New System.Windows.Forms.Label
        Me.Label148 = New System.Windows.Forms.Label
        Me.Label149 = New System.Windows.Forms.Label
        Me.Panel13 = New System.Windows.Forms.Panel
        Me.Panel25 = New System.Windows.Forms.Panel
        Me.txtLabResultSearch = New System.Windows.Forms.TextBox
        Me.Label126 = New System.Windows.Forms.Label
        Me.Label127 = New System.Windows.Forms.Label
        Me.PictureBox3 = New System.Windows.Forms.PictureBox
        Me.Label99 = New System.Windows.Forms.Label
        Me.Label100 = New System.Windows.Forms.Label
        Me.Label101 = New System.Windows.Forms.Label
        Me.Label102 = New System.Windows.Forms.Label
        Me.Panel14 = New System.Windows.Forms.Panel
        Me.Label155 = New System.Windows.Forms.Label
        Me.Label150 = New System.Windows.Forms.Label
        Me.Label151 = New System.Windows.Forms.Label
        Me.Label152 = New System.Windows.Forms.Label
        Me.Label153 = New System.Windows.Forms.Label
        Me.pnlHistory = New System.Windows.Forms.Panel
        Me.Panel22 = New System.Windows.Forms.Panel
        Me.Label193 = New System.Windows.Forms.Label
        Me.Label194 = New System.Windows.Forms.Label
        Me.trvSelectedHistory = New System.Windows.Forms.TreeView
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.Label195 = New System.Windows.Forms.Label
        Me.Label196 = New System.Windows.Forms.Label
        Me.Panel8 = New System.Windows.Forms.Panel
        Me.Panel21 = New System.Windows.Forms.Panel
        Me.Label188 = New System.Windows.Forms.Label
        Me.Label189 = New System.Windows.Forms.Label
        Me.Label190 = New System.Windows.Forms.Label
        Me.Label191 = New System.Windows.Forms.Label
        Me.Label192 = New System.Windows.Forms.Label
        Me.Splitter1 = New System.Windows.Forms.Splitter
        Me.Panel9 = New System.Windows.Forms.Panel
        Me.GloUC_trvHistory = New gloUserControlLibrary.gloUC_TreeView
        Me.trvHistoryRight = New System.Windows.Forms.TreeView
        Me.txtSearch = New System.Windows.Forms.TextBox
        Me.pnlHistoryLeft = New System.Windows.Forms.Panel
        Me.Label120 = New System.Windows.Forms.Label
        Me.Label121 = New System.Windows.Forms.Label
        Me.Label122 = New System.Windows.Forms.Label
        Me.Label123 = New System.Windows.Forms.Label
        Me.trvHistory = New System.Windows.Forms.TreeView
        Me.Panel23 = New System.Windows.Forms.Panel
        Me.Panel24 = New System.Windows.Forms.Panel
        Me.cmbHistoryCategory = New System.Windows.Forms.ComboBox
        Me.Label115 = New System.Windows.Forms.Label
        Me.Label116 = New System.Windows.Forms.Label
        Me.Label117 = New System.Windows.Forms.Label
        Me.Label118 = New System.Windows.Forms.Label
        Me.Label119 = New System.Windows.Forms.Label
        Me.btnHistorySearch = New System.Windows.Forms.Button
        Me.pnlDrugs = New System.Windows.Forms.Panel
        Me.pnltrvSelectedDrugs = New System.Windows.Forms.Panel
        Me.Label86 = New System.Windows.Forms.Label
        Me.Label180 = New System.Windows.Forms.Label
        Me.trvSelectedDrugs = New System.Windows.Forms.TreeView
        Me.Label181 = New System.Windows.Forms.Label
        Me.Label182 = New System.Windows.Forms.Label
        Me.pnlSelectedDrugLabel = New System.Windows.Forms.Panel
        Me.Panel20 = New System.Windows.Forms.Panel
        Me.Label185 = New System.Windows.Forms.Label
        Me.Label184 = New System.Windows.Forms.Label
        Me.Label186 = New System.Windows.Forms.Label
        Me.Label187 = New System.Windows.Forms.Label
        Me.Label183 = New System.Windows.Forms.Label
        Me.Splitter2 = New System.Windows.Forms.Splitter
        Me.Panel11 = New System.Windows.Forms.Panel
        Me.GloUC_trvDrugs = New gloUserControlLibrary.gloUC_TreeView
        Me.trvDrgs = New System.Windows.Forms.TreeView
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.txtDrugSearch = New System.Windows.Forms.TextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label137 = New System.Windows.Forms.Label
        Me.PictureBox4 = New System.Windows.Forms.PictureBox
        Me.Label138 = New System.Windows.Forms.Label
        Me.Label139 = New System.Windows.Forms.Label
        Me.Label140 = New System.Windows.Forms.Label
        Me.Label141 = New System.Windows.Forms.Label
        Me.pnlRadiology = New System.Windows.Forms.Panel
        Me.Panel18 = New System.Windows.Forms.Panel
        Me.Label170 = New System.Windows.Forms.Label
        Me.Label171 = New System.Windows.Forms.Label
        Me.c1Labs = New C1.Win.C1FlexGrid.C1FlexGrid
        Me.Label172 = New System.Windows.Forms.Label
        Me.Label173 = New System.Windows.Forms.Label
        Me.Panel17 = New System.Windows.Forms.Panel
        Me.txtLabsSearch = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label165 = New System.Windows.Forms.Label
        Me.PictureBox6 = New System.Windows.Forms.PictureBox
        Me.Label166 = New System.Windows.Forms.Label
        Me.Label167 = New System.Windows.Forms.Label
        Me.Label168 = New System.Windows.Forms.Label
        Me.Label169 = New System.Windows.Forms.Label
        Me.pnlSummaryOthers = New System.Windows.Forms.Panel
        Me.pnlSummary = New System.Windows.Forms.Panel
        Me.txt_summary = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label91 = New System.Windows.Forms.Label
        Me.Label92 = New System.Windows.Forms.Label
        Me.Label93 = New System.Windows.Forms.Label
        Me.Label94 = New System.Windows.Forms.Label
        Me.pnlSummaryHeader = New System.Windows.Forms.Panel
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Label26 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label64 = New System.Windows.Forms.Label
        Me.Label87 = New System.Windows.Forms.Label
        Me.Label88 = New System.Windows.Forms.Label
        Me.pnlCPT = New System.Windows.Forms.Panel
        Me.Panel15 = New System.Windows.Forms.Panel
        Me.Label154 = New System.Windows.Forms.Label
        Me.Label156 = New System.Windows.Forms.Label
        Me.trvCPT = New System.Windows.Forms.TreeView
        Me.Label157 = New System.Windows.Forms.Label
        Me.Label158 = New System.Windows.Forms.Label
        Me.Panel16 = New System.Windows.Forms.Panel
        Me.txtCPTsearch = New System.Windows.Forms.TextBox
        Me.Label159 = New System.Windows.Forms.Label
        Me.Label160 = New System.Windows.Forms.Label
        Me.PictureBox5 = New System.Windows.Forms.PictureBox
        Me.Label161 = New System.Windows.Forms.Label
        Me.Label162 = New System.Windows.Forms.Label
        Me.Label163 = New System.Windows.Forms.Label
        Me.Label164 = New System.Windows.Forms.Label
        Me.pnlICD9 = New System.Windows.Forms.Panel
        Me.Panel5 = New System.Windows.Forms.Panel
        Me.trvICD9 = New System.Windows.Forms.TreeView
        Me.Label179 = New System.Windows.Forms.Label
        Me.Label178 = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.Label128 = New System.Windows.Forms.Label
        Me.Label129 = New System.Windows.Forms.Label
        Me.Label130 = New System.Windows.Forms.Label
        Me.Panel10 = New System.Windows.Forms.Panel
        Me.txtICD9Search = New System.Windows.Forms.TextBox
        Me.Label131 = New System.Windows.Forms.Label
        Me.Label132 = New System.Windows.Forms.Label
        Me.PictureBox2 = New System.Windows.Forms.PictureBox
        Me.Label133 = New System.Windows.Forms.Label
        Me.Label134 = New System.Windows.Forms.Label
        Me.Label135 = New System.Windows.Forms.Label
        Me.Label136 = New System.Windows.Forms.Label
        Me.sptLeft = New System.Windows.Forms.Splitter
        Me.pnlLeft = New System.Windows.Forms.Panel
        Me.Panel19 = New System.Windows.Forms.Panel
        Me.Label174 = New System.Windows.Forms.Label
        Me.Label175 = New System.Windows.Forms.Label
        Me.Label176 = New System.Windows.Forms.Label
        Me.Label177 = New System.Windows.Forms.Label
        Me.pnlbtnOrders = New System.Windows.Forms.Panel
        Me.btnOrders = New System.Windows.Forms.Button
        Me.Label51 = New System.Windows.Forms.Label
        Me.Label52 = New System.Windows.Forms.Label
        Me.Label53 = New System.Windows.Forms.Label
        Me.Label54 = New System.Windows.Forms.Label
        Me.pnlbtnRadiology = New System.Windows.Forms.Panel
        Me.btnRadiology = New System.Windows.Forms.Button
        Me.Label47 = New System.Windows.Forms.Label
        Me.Label48 = New System.Windows.Forms.Label
        Me.Label49 = New System.Windows.Forms.Label
        Me.Label50 = New System.Windows.Forms.Label
        Me.pnlbtnLabs = New System.Windows.Forms.Panel
        Me.btnLabs = New System.Windows.Forms.Button
        Me.Label43 = New System.Windows.Forms.Label
        Me.Label44 = New System.Windows.Forms.Label
        Me.Label45 = New System.Windows.Forms.Label
        Me.Label46 = New System.Windows.Forms.Label
        Me.pnlbtnCPT = New System.Windows.Forms.Panel
        Me.btnCPT = New System.Windows.Forms.Button
        Me.Label39 = New System.Windows.Forms.Label
        Me.Label40 = New System.Windows.Forms.Label
        Me.Label41 = New System.Windows.Forms.Label
        Me.Label42 = New System.Windows.Forms.Label
        Me.pnlbtnICD9 = New System.Windows.Forms.Panel
        Me.btnICD9 = New System.Windows.Forms.Button
        Me.Label35 = New System.Windows.Forms.Label
        Me.Label36 = New System.Windows.Forms.Label
        Me.Label37 = New System.Windows.Forms.Label
        Me.Label38 = New System.Windows.Forms.Label
        Me.pnlbtnDrugs = New System.Windows.Forms.Panel
        Me.btnDrugs = New System.Windows.Forms.Button
        Me.Label31 = New System.Windows.Forms.Label
        Me.Label32 = New System.Windows.Forms.Label
        Me.Label33 = New System.Windows.Forms.Label
        Me.Label34 = New System.Windows.Forms.Label
        Me.pnlbtnHistory = New System.Windows.Forms.Panel
        Me.btnHistory = New System.Windows.Forms.Button
        Me.Label27 = New System.Windows.Forms.Label
        Me.Label28 = New System.Windows.Forms.Label
        Me.Label29 = New System.Windows.Forms.Label
        Me.Label30 = New System.Windows.Forms.Label
        Me.pnbtnDemographics = New System.Windows.Forms.Panel
        Me.btnDemographics = New System.Windows.Forms.Button
        Me.label58 = New System.Windows.Forms.Label
        Me.label59 = New System.Windows.Forms.Label
        Me.label60 = New System.Windows.Forms.Label
        Me.label61 = New System.Windows.Forms.Label
        Me.pnlRight = New System.Windows.Forms.Panel
        Me.pnltrvTriggers = New System.Windows.Forms.Panel
        Me.trvTriggers = New System.Windows.Forms.TreeView
        Me.Label16 = New System.Windows.Forms.Label
        Me.Label74 = New System.Windows.Forms.Label
        Me.Label75 = New System.Windows.Forms.Label
        Me.Label76 = New System.Windows.Forms.Label
        Me.Label77 = New System.Windows.Forms.Label
        Me.pnltxtSearchOrder = New System.Windows.Forms.Panel
        Me.txtSearchOrder = New System.Windows.Forms.TextBox
        Me.Label20 = New System.Windows.Forms.Label
        Me.Label21 = New System.Windows.Forms.Label
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.Label15 = New System.Windows.Forms.Label
        Me.Label82 = New System.Windows.Forms.Label
        Me.Label83 = New System.Windows.Forms.Label
        Me.Label84 = New System.Windows.Forms.Label
        Me.pnlbtnLab = New System.Windows.Forms.Panel
        Me.btnLab = New System.Windows.Forms.Button
        Me.Label78 = New System.Windows.Forms.Label
        Me.Label79 = New System.Windows.Forms.Label
        Me.Label80 = New System.Windows.Forms.Label
        Me.Label81 = New System.Windows.Forms.Label
        Me.pnlbtnReferrals = New System.Windows.Forms.Panel
        Me.btnReferrals = New System.Windows.Forms.Button
        Me.Label70 = New System.Windows.Forms.Label
        Me.Label71 = New System.Windows.Forms.Label
        Me.Label72 = New System.Windows.Forms.Label
        Me.Label73 = New System.Windows.Forms.Label
        Me.pnlbtnRx = New System.Windows.Forms.Panel
        Me.btnRx = New System.Windows.Forms.Button
        Me.Label66 = New System.Windows.Forms.Label
        Me.Label67 = New System.Windows.Forms.Label
        Me.Label68 = New System.Windows.Forms.Label
        Me.Label69 = New System.Windows.Forms.Label
        Me.pnlbtnRadiologyTest = New System.Windows.Forms.Panel
        Me.btnRadiologyTest = New System.Windows.Forms.Button
        Me.Label55 = New System.Windows.Forms.Label
        Me.Label56 = New System.Windows.Forms.Label
        Me.Label57 = New System.Windows.Forms.Label
        Me.Label62 = New System.Windows.Forms.Label
        Me.pnlbtnGuideline = New System.Windows.Forms.Panel
        Me.btnGuideline = New System.Windows.Forms.Button
        Me.Label17 = New System.Windows.Forms.Label
        Me.Label18 = New System.Windows.Forms.Label
        Me.Label19 = New System.Windows.Forms.Label
        Me.Label22 = New System.Windows.Forms.Label
        Me.pnlMsgTOP = New System.Windows.Forms.Panel
        Me.pnlMsg = New System.Windows.Forms.Panel
        Me.Label95 = New System.Windows.Forms.Label
        Me.Label14 = New System.Windows.Forms.Label
        Me.txtMessage = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtName = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label23 = New System.Windows.Forms.Label
        Me.Label24 = New System.Windows.Forms.Label
        Me.Label63 = New System.Windows.Forms.Label
        Me.Label25 = New System.Windows.Forms.Label
        Me.pnl_tlstrip = New System.Windows.Forms.Panel
        Me.tlsDM = New gloGlobal.gloToolStripIgnoreFocus
        Me.tlsDM_Save = New System.Windows.Forms.ToolStripButton
        Me.tlsDM_Close = New System.Windows.Forms.ToolStripButton
        Me.CntConditions = New System.Windows.Forms.ContextMenu
        Me.mnuDelete = New System.Windows.Forms.MenuItem
        Me.mnuReferral = New System.Windows.Forms.MenuItem
        Me.EditReferral = New System.Windows.Forms.MenuItem
        Me.mnuEditTemplate = New System.Windows.Forms.MenuItem
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mnuDeleteDrugs = New System.Windows.Forms.ToolStripMenuItem
        Me.ContextMenuHistory = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mnuDeleteHistory = New System.Windows.Forms.ToolStripMenuItem
        Me.pnlMain.SuspendLayout()
        Me.pnlMiddle.SuspendLayout()
        Me.pnlDemoVitals.SuspendLayout()
        Me.pnlVitals.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.Panel7.SuspendLayout()
        Me.pnlDemographics.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel6.SuspendLayout()
        Me.pnlLab.SuspendLayout()
        Me.Panel12.SuspendLayout()
        CType(Me.C1LabResult, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel13.SuspendLayout()
        Me.Panel25.SuspendLayout()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel14.SuspendLayout()
        Me.pnlHistory.SuspendLayout()
        Me.Panel22.SuspendLayout()
        Me.Panel8.SuspendLayout()
        Me.Panel21.SuspendLayout()
        Me.Panel9.SuspendLayout()
        Me.pnlHistoryLeft.SuspendLayout()
        Me.Panel23.SuspendLayout()
        Me.Panel24.SuspendLayout()
        Me.pnlDrugs.SuspendLayout()
        Me.pnltrvSelectedDrugs.SuspendLayout()
        Me.pnlSelectedDrugLabel.SuspendLayout()
        Me.Panel20.SuspendLayout()
        Me.Panel11.SuspendLayout()
        Me.Panel4.SuspendLayout()
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlRadiology.SuspendLayout()
        Me.Panel18.SuspendLayout()
        CType(Me.c1Labs, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel17.SuspendLayout()
        CType(Me.PictureBox6, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlSummaryOthers.SuspendLayout()
        Me.pnlSummary.SuspendLayout()
        Me.pnlSummaryHeader.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.pnlCPT.SuspendLayout()
        Me.Panel15.SuspendLayout()
        Me.Panel16.SuspendLayout()
        CType(Me.PictureBox5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlICD9.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.Panel10.SuspendLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlLeft.SuspendLayout()
        Me.Panel19.SuspendLayout()
        Me.pnlbtnOrders.SuspendLayout()
        Me.pnlbtnRadiology.SuspendLayout()
        Me.pnlbtnLabs.SuspendLayout()
        Me.pnlbtnCPT.SuspendLayout()
        Me.pnlbtnICD9.SuspendLayout()
        Me.pnlbtnDrugs.SuspendLayout()
        Me.pnlbtnHistory.SuspendLayout()
        Me.pnbtnDemographics.SuspendLayout()
        Me.pnlRight.SuspendLayout()
        Me.pnltrvTriggers.SuspendLayout()
        Me.pnltxtSearchOrder.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlbtnLab.SuspendLayout()
        Me.pnlbtnReferrals.SuspendLayout()
        Me.pnlbtnRx.SuspendLayout()
        Me.pnlbtnRadiologyTest.SuspendLayout()
        Me.pnlbtnGuideline.SuspendLayout()
        Me.pnlMsgTOP.SuspendLayout()
        Me.pnlMsg.SuspendLayout()
        Me.pnl_tlstrip.SuspendLayout()
        Me.tlsDM.SuspendLayout()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.ContextMenuHistory.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlMain
        '
        Me.pnlMain.BackColor = System.Drawing.Color.Transparent
        Me.pnlMain.Controls.Add(Me.pnlMiddle)
        Me.pnlMain.Controls.Add(Me.sptLeft)
        Me.pnlMain.Controls.Add(Me.pnlLeft)
        Me.pnlMain.Controls.Add(Me.pnlRight)
        Me.pnlMain.Controls.Add(Me.pnlMsgTOP)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(0, 54)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(844, 606)
        Me.pnlMain.TabIndex = 2
        '
        'pnlMiddle
        '
        Me.pnlMiddle.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlMiddle.Controls.Add(Me.pnlDemoVitals)
        Me.pnlMiddle.Controls.Add(Me.pnlLab)
        Me.pnlMiddle.Controls.Add(Me.pnlHistory)
        Me.pnlMiddle.Controls.Add(Me.pnlDrugs)
        Me.pnlMiddle.Controls.Add(Me.pnlRadiology)
        Me.pnlMiddle.Controls.Add(Me.pnlSummaryOthers)
        Me.pnlMiddle.Controls.Add(Me.pnlCPT)
        Me.pnlMiddle.Controls.Add(Me.pnlICD9)
        Me.pnlMiddle.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMiddle.Location = New System.Drawing.Point(205, 30)
        Me.pnlMiddle.Name = "pnlMiddle"
        Me.pnlMiddle.Size = New System.Drawing.Size(639, 576)
        Me.pnlMiddle.TabIndex = 1
        '
        'pnlDemoVitals
        '
        Me.pnlDemoVitals.Controls.Add(Me.pnlVitals)
        Me.pnlDemoVitals.Controls.Add(Me.Panel3)
        Me.pnlDemoVitals.Controls.Add(Me.pnlDemographics)
        Me.pnlDemoVitals.Controls.Add(Me.Panel2)
        Me.pnlDemoVitals.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlDemoVitals.Location = New System.Drawing.Point(0, 0)
        Me.pnlDemoVitals.Name = "pnlDemoVitals"
        Me.pnlDemoVitals.Size = New System.Drawing.Size(639, 576)
        Me.pnlDemoVitals.TabIndex = 11
        '
        'pnlVitals
        '
        Me.pnlVitals.BackColor = System.Drawing.Color.Transparent
        Me.pnlVitals.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlVitals.Controls.Add(Me.Label111)
        Me.pnlVitals.Controls.Add(Me.Label112)
        Me.pnlVitals.Controls.Add(Me.Label113)
        Me.pnlVitals.Controls.Add(Me.Label114)
        Me.pnlVitals.Controls.Add(Me.txtHeightMin)
        Me.pnlVitals.Controls.Add(Me.Label11)
        Me.pnlVitals.Controls.Add(Me.txtHeightMaxInch)
        Me.pnlVitals.Controls.Add(Me.Label12)
        Me.pnlVitals.Controls.Add(Me.Label10)
        Me.pnlVitals.Controls.Add(Me.txtHeightMinInch)
        Me.pnlVitals.Controls.Add(Me.Label9)
        Me.pnlVitals.Controls.Add(Me.txtPulseOXmax)
        Me.pnlVitals.Controls.Add(Me.lblPulseOXMax)
        Me.pnlVitals.Controls.Add(Me.txtPulseOXmin)
        Me.pnlVitals.Controls.Add(Me.lblPulseOXMin)
        Me.pnlVitals.Controls.Add(Me.txtPulseMax)
        Me.pnlVitals.Controls.Add(Me.lblPulseMax)
        Me.pnlVitals.Controls.Add(Me.txtPulseMin)
        Me.pnlVitals.Controls.Add(Me.lblPulseMin)
        Me.pnlVitals.Controls.Add(Me.txtTemperatureMax)
        Me.pnlVitals.Controls.Add(Me.lblTempratureMax)
        Me.pnlVitals.Controls.Add(Me.txtBMImax)
        Me.pnlVitals.Controls.Add(Me.lblBMImax)
        Me.pnlVitals.Controls.Add(Me.txtBMImin)
        Me.pnlVitals.Controls.Add(Me.lblBMImin)
        Me.pnlVitals.Controls.Add(Me.txtWeightMax)
        Me.pnlVitals.Controls.Add(Me.lblWeightMax)
        Me.pnlVitals.Controls.Add(Me.txtHeightMax)
        Me.pnlVitals.Controls.Add(Me.lblHeightMax)
        Me.pnlVitals.Controls.Add(Me.txtBPstandingMax)
        Me.pnlVitals.Controls.Add(Me.txtBPstandingMin)
        Me.pnlVitals.Controls.Add(Me.txtBPsettingMax)
        Me.pnlVitals.Controls.Add(Me.txtBPsettingMin)
        Me.pnlVitals.Controls.Add(Me.lblBPStandingMax)
        Me.pnlVitals.Controls.Add(Me.lblBPStandingMin)
        Me.pnlVitals.Controls.Add(Me.lblBPSittingMax)
        Me.pnlVitals.Controls.Add(Me.lblBPSittingMin)
        Me.pnlVitals.Controls.Add(Me.txtTemperatureMin)
        Me.pnlVitals.Controls.Add(Me.txtWeightMin)
        Me.pnlVitals.Controls.Add(Me.lblWeightMin)
        Me.pnlVitals.Controls.Add(Me.lblTempratureMin)
        Me.pnlVitals.Controls.Add(Me.lblHeightMin)
        Me.pnlVitals.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlVitals.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlVitals.Location = New System.Drawing.Point(0, 208)
        Me.pnlVitals.Margin = New System.Windows.Forms.Padding(0)
        Me.pnlVitals.Name = "pnlVitals"
        Me.pnlVitals.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.pnlVitals.Size = New System.Drawing.Size(639, 368)
        Me.pnlVitals.TabIndex = 1
        '
        'Label111
        '
        Me.Label111.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label111.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label111.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label111.Location = New System.Drawing.Point(1, 364)
        Me.Label111.Name = "Label111"
        Me.Label111.Size = New System.Drawing.Size(637, 1)
        Me.Label111.TabIndex = 47
        Me.Label111.Text = "label2"
        '
        'Label112
        '
        Me.Label112.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label112.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label112.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label112.Location = New System.Drawing.Point(0, 1)
        Me.Label112.Name = "Label112"
        Me.Label112.Size = New System.Drawing.Size(1, 364)
        Me.Label112.TabIndex = 46
        Me.Label112.Text = "label4"
        '
        'Label113
        '
        Me.Label113.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label113.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label113.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label113.Location = New System.Drawing.Point(638, 1)
        Me.Label113.Name = "Label113"
        Me.Label113.Size = New System.Drawing.Size(1, 364)
        Me.Label113.TabIndex = 45
        Me.Label113.Text = "label3"
        '
        'Label114
        '
        Me.Label114.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label114.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label114.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label114.Location = New System.Drawing.Point(0, 0)
        Me.Label114.Name = "Label114"
        Me.Label114.Size = New System.Drawing.Size(639, 1)
        Me.Label114.TabIndex = 44
        Me.Label114.Text = "label1"
        '
        'txtHeightMin
        '
        Me.txtHeightMin.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtHeightMin.Location = New System.Drawing.Point(140, 23)
        Me.txtHeightMin.MaxLength = 1
        Me.txtHeightMin.Name = "txtHeightMin"
        Me.txtHeightMin.Size = New System.Drawing.Size(16, 22)
        Me.txtHeightMin.TabIndex = 0
        Me.txtHeightMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(313, 23)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(12, 14)
        Me.Label11.TabIndex = 43
        Me.Label11.Text = """"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtHeightMaxInch
        '
        Me.txtHeightMaxInch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtHeightMaxInch.Location = New System.Drawing.Point(280, 23)
        Me.txtHeightMaxInch.MaxLength = 2
        Me.txtHeightMaxInch.Name = "txtHeightMaxInch"
        Me.txtHeightMaxInch.Size = New System.Drawing.Size(32, 22)
        Me.txtHeightMaxInch.TabIndex = 3
        Me.txtHeightMaxInch.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.Color.Transparent
        Me.Label12.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(270, 23)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(10, 14)
        Me.Label12.TabIndex = 41
        Me.Label12.Text = "'"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(197, 24)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(12, 14)
        Me.Label10.TabIndex = 2
        Me.Label10.Text = """"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtHeightMinInch
        '
        Me.txtHeightMinInch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtHeightMinInch.Location = New System.Drawing.Point(165, 23)
        Me.txtHeightMinInch.MaxLength = 2
        Me.txtHeightMinInch.Name = "txtHeightMinInch"
        Me.txtHeightMinInch.Size = New System.Drawing.Size(32, 22)
        Me.txtHeightMinInch.TabIndex = 1
        Me.txtHeightMinInch.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(154, 25)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(10, 14)
        Me.Label9.TabIndex = 38
        Me.Label9.Text = "'"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtPulseOXmax
        '
        Me.txtPulseOXmax.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPulseOXmax.Location = New System.Drawing.Point(549, 57)
        Me.txtPulseOXmax.MaxLength = 3
        Me.txtPulseOXmax.Name = "txtPulseOXmax"
        Me.txtPulseOXmax.Size = New System.Drawing.Size(49, 22)
        Me.txtPulseOXmax.TabIndex = 9
        Me.txtPulseOXmax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblPulseOXMax
        '
        Me.lblPulseOXMax.AutoSize = True
        Me.lblPulseOXMax.BackColor = System.Drawing.Color.Transparent
        Me.lblPulseOXMax.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPulseOXMax.Location = New System.Drawing.Point(511, 61)
        Me.lblPulseOXMax.Margin = New System.Windows.Forms.Padding(0)
        Me.lblPulseOXMax.Name = "lblPulseOXMax"
        Me.lblPulseOXMax.Size = New System.Drawing.Size(36, 14)
        Me.lblPulseOXMax.TabIndex = 37
        Me.lblPulseOXMax.Text = "Max :"
        Me.lblPulseOXMax.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtPulseOXmin
        '
        Me.txtPulseOXmin.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPulseOXmin.Location = New System.Drawing.Point(447, 57)
        Me.txtPulseOXmin.MaxLength = 3
        Me.txtPulseOXmin.Name = "txtPulseOXmin"
        Me.txtPulseOXmin.Size = New System.Drawing.Size(59, 22)
        Me.txtPulseOXmin.TabIndex = 8
        Me.txtPulseOXmin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblPulseOXMin
        '
        Me.lblPulseOXMin.AutoSize = True
        Me.lblPulseOXMin.BackColor = System.Drawing.Color.Transparent
        Me.lblPulseOXMin.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPulseOXMin.Location = New System.Drawing.Point(359, 61)
        Me.lblPulseOXMin.Name = "lblPulseOXMin"
        Me.lblPulseOXMin.Size = New System.Drawing.Size(85, 14)
        Me.lblPulseOXMin.TabIndex = 35
        Me.lblPulseOXMin.Text = "Pulse OX Min :"
        Me.lblPulseOXMin.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtPulseMax
        '
        Me.txtPulseMax.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPulseMax.Location = New System.Drawing.Point(254, 57)
        Me.txtPulseMax.MaxLength = 3
        Me.txtPulseMax.Name = "txtPulseMax"
        Me.txtPulseMax.Size = New System.Drawing.Size(59, 22)
        Me.txtPulseMax.TabIndex = 7
        Me.txtPulseMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblPulseMax
        '
        Me.lblPulseMax.AutoSize = True
        Me.lblPulseMax.BackColor = System.Drawing.Color.Transparent
        Me.lblPulseMax.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPulseMax.Location = New System.Drawing.Point(214, 61)
        Me.lblPulseMax.Name = "lblPulseMax"
        Me.lblPulseMax.Size = New System.Drawing.Size(36, 14)
        Me.lblPulseMax.TabIndex = 33
        Me.lblPulseMax.Text = "Max :"
        Me.lblPulseMax.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtPulseMin
        '
        Me.txtPulseMin.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPulseMin.Location = New System.Drawing.Point(140, 57)
        Me.txtPulseMin.MaxLength = 3
        Me.txtPulseMin.Name = "txtPulseMin"
        Me.txtPulseMin.Size = New System.Drawing.Size(59, 22)
        Me.txtPulseMin.TabIndex = 6
        Me.txtPulseMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblPulseMin
        '
        Me.lblPulseMin.AutoSize = True
        Me.lblPulseMin.BackColor = System.Drawing.Color.Transparent
        Me.lblPulseMin.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPulseMin.Location = New System.Drawing.Point(72, 61)
        Me.lblPulseMin.Name = "lblPulseMin"
        Me.lblPulseMin.Size = New System.Drawing.Size(65, 14)
        Me.lblPulseMin.TabIndex = 31
        Me.lblPulseMin.Text = "Pulse Min :"
        Me.lblPulseMin.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtTemperatureMax
        '
        Me.txtTemperatureMax.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTemperatureMax.Location = New System.Drawing.Point(549, 128)
        Me.txtTemperatureMax.Name = "txtTemperatureMax"
        Me.txtTemperatureMax.Size = New System.Drawing.Size(49, 22)
        Me.txtTemperatureMax.TabIndex = 17
        Me.txtTemperatureMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblTempratureMax
        '
        Me.lblTempratureMax.AutoSize = True
        Me.lblTempratureMax.BackColor = System.Drawing.Color.Transparent
        Me.lblTempratureMax.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTempratureMax.Location = New System.Drawing.Point(511, 132)
        Me.lblTempratureMax.Margin = New System.Windows.Forms.Padding(0)
        Me.lblTempratureMax.Name = "lblTempratureMax"
        Me.lblTempratureMax.Size = New System.Drawing.Size(36, 14)
        Me.lblTempratureMax.TabIndex = 29
        Me.lblTempratureMax.Text = "Max :"
        Me.lblTempratureMax.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtBMImax
        '
        Me.txtBMImax.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBMImax.Location = New System.Drawing.Point(254, 128)
        Me.txtBMImax.MaxLength = 5
        Me.txtBMImax.Name = "txtBMImax"
        Me.txtBMImax.Size = New System.Drawing.Size(59, 22)
        Me.txtBMImax.TabIndex = 15
        Me.txtBMImax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblBMImax
        '
        Me.lblBMImax.AutoSize = True
        Me.lblBMImax.BackColor = System.Drawing.Color.Transparent
        Me.lblBMImax.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBMImax.Location = New System.Drawing.Point(214, 131)
        Me.lblBMImax.Name = "lblBMImax"
        Me.lblBMImax.Size = New System.Drawing.Size(36, 14)
        Me.lblBMImax.TabIndex = 27
        Me.lblBMImax.Text = "Max :"
        Me.lblBMImax.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtBMImin
        '
        Me.txtBMImin.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBMImin.Location = New System.Drawing.Point(140, 127)
        Me.txtBMImin.MaxLength = 5
        Me.txtBMImin.Name = "txtBMImin"
        Me.txtBMImin.Size = New System.Drawing.Size(59, 22)
        Me.txtBMImin.TabIndex = 14
        Me.txtBMImin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblBMImin
        '
        Me.lblBMImin.AutoSize = True
        Me.lblBMImin.BackColor = System.Drawing.Color.Transparent
        Me.lblBMImin.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBMImin.Location = New System.Drawing.Point(80, 131)
        Me.lblBMImin.Name = "lblBMImin"
        Me.lblBMImin.Size = New System.Drawing.Size(57, 14)
        Me.lblBMImin.TabIndex = 25
        Me.lblBMImin.Text = "BMI Min :"
        Me.lblBMImin.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtWeightMax
        '
        Me.txtWeightMax.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtWeightMax.Location = New System.Drawing.Point(549, 23)
        Me.txtWeightMax.MaxLength = 6
        Me.txtWeightMax.Name = "txtWeightMax"
        Me.txtWeightMax.Size = New System.Drawing.Size(49, 22)
        Me.txtWeightMax.TabIndex = 5
        Me.txtWeightMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblWeightMax
        '
        Me.lblWeightMax.AutoSize = True
        Me.lblWeightMax.BackColor = System.Drawing.Color.Transparent
        Me.lblWeightMax.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblWeightMax.Location = New System.Drawing.Point(511, 27)
        Me.lblWeightMax.Margin = New System.Windows.Forms.Padding(0)
        Me.lblWeightMax.Name = "lblWeightMax"
        Me.lblWeightMax.Size = New System.Drawing.Size(36, 14)
        Me.lblWeightMax.TabIndex = 23
        Me.lblWeightMax.Text = "Max :"
        Me.lblWeightMax.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtHeightMax
        '
        Me.txtHeightMax.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtHeightMax.Location = New System.Drawing.Point(254, 23)
        Me.txtHeightMax.MaxLength = 1
        Me.txtHeightMax.Name = "txtHeightMax"
        Me.txtHeightMax.Size = New System.Drawing.Size(16, 22)
        Me.txtHeightMax.TabIndex = 2
        Me.txtHeightMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblHeightMax
        '
        Me.lblHeightMax.AutoSize = True
        Me.lblHeightMax.BackColor = System.Drawing.Color.Transparent
        Me.lblHeightMax.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeightMax.Location = New System.Drawing.Point(214, 23)
        Me.lblHeightMax.Name = "lblHeightMax"
        Me.lblHeightMax.Size = New System.Drawing.Size(36, 14)
        Me.lblHeightMax.TabIndex = 21
        Me.lblHeightMax.Text = "Max :"
        Me.lblHeightMax.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtBPstandingMax
        '
        Me.txtBPstandingMax.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBPstandingMax.Location = New System.Drawing.Point(447, 93)
        Me.txtBPstandingMax.MaxLength = 3
        Me.txtBPstandingMax.Name = "txtBPstandingMax"
        Me.txtBPstandingMax.Size = New System.Drawing.Size(59, 22)
        Me.txtBPstandingMax.TabIndex = 12
        Me.txtBPstandingMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtBPstandingMin
        '
        Me.txtBPstandingMin.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBPstandingMin.Location = New System.Drawing.Point(549, 93)
        Me.txtBPstandingMin.MaxLength = 3
        Me.txtBPstandingMin.Name = "txtBPstandingMin"
        Me.txtBPstandingMin.Size = New System.Drawing.Size(49, 22)
        Me.txtBPstandingMin.TabIndex = 13
        Me.txtBPstandingMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtBPsettingMax
        '
        Me.txtBPsettingMax.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBPsettingMax.Location = New System.Drawing.Point(140, 93)
        Me.txtBPsettingMax.MaxLength = 3
        Me.txtBPsettingMax.Name = "txtBPsettingMax"
        Me.txtBPsettingMax.Size = New System.Drawing.Size(59, 22)
        Me.txtBPsettingMax.TabIndex = 10
        Me.txtBPsettingMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtBPsettingMin
        '
        Me.txtBPsettingMin.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBPsettingMin.Location = New System.Drawing.Point(254, 93)
        Me.txtBPsettingMin.MaxLength = 3
        Me.txtBPsettingMin.Name = "txtBPsettingMin"
        Me.txtBPsettingMin.Size = New System.Drawing.Size(59, 22)
        Me.txtBPsettingMin.TabIndex = 11
        Me.txtBPsettingMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblBPStandingMax
        '
        Me.lblBPStandingMax.AutoSize = True
        Me.lblBPStandingMax.BackColor = System.Drawing.Color.Transparent
        Me.lblBPStandingMax.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBPStandingMax.Location = New System.Drawing.Point(338, 97)
        Me.lblBPStandingMax.Name = "lblBPStandingMax"
        Me.lblBPStandingMax.Size = New System.Drawing.Size(106, 14)
        Me.lblBPStandingMax.TabIndex = 16
        Me.lblBPStandingMax.Text = "BP-Standing-Max :"
        Me.lblBPStandingMax.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblBPStandingMin
        '
        Me.lblBPStandingMin.AutoSize = True
        Me.lblBPStandingMin.BackColor = System.Drawing.Color.Transparent
        Me.lblBPStandingMin.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBPStandingMin.Location = New System.Drawing.Point(514, 97)
        Me.lblBPStandingMin.Margin = New System.Windows.Forms.Padding(0)
        Me.lblBPStandingMin.Name = "lblBPStandingMin"
        Me.lblBPStandingMin.Size = New System.Drawing.Size(33, 14)
        Me.lblBPStandingMin.TabIndex = 15
        Me.lblBPStandingMin.Text = "Min :"
        Me.lblBPStandingMin.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblBPSittingMax
        '
        Me.lblBPSittingMax.AutoSize = True
        Me.lblBPSittingMax.BackColor = System.Drawing.Color.Transparent
        Me.lblBPSittingMax.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBPSittingMax.Location = New System.Drawing.Point(44, 96)
        Me.lblBPSittingMax.Name = "lblBPSittingMax"
        Me.lblBPSittingMax.Size = New System.Drawing.Size(93, 14)
        Me.lblBPSittingMax.TabIndex = 14
        Me.lblBPSittingMax.Text = "BP-Sitting-Max :"
        Me.lblBPSittingMax.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblBPSittingMin
        '
        Me.lblBPSittingMin.AutoSize = True
        Me.lblBPSittingMin.BackColor = System.Drawing.Color.Transparent
        Me.lblBPSittingMin.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBPSittingMin.Location = New System.Drawing.Point(217, 94)
        Me.lblBPSittingMin.Name = "lblBPSittingMin"
        Me.lblBPSittingMin.Size = New System.Drawing.Size(33, 14)
        Me.lblBPSittingMin.TabIndex = 13
        Me.lblBPSittingMin.Text = "Min :"
        Me.lblBPSittingMin.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtTemperatureMin
        '
        Me.txtTemperatureMin.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTemperatureMin.Location = New System.Drawing.Point(447, 128)
        Me.txtTemperatureMin.Name = "txtTemperatureMin"
        Me.txtTemperatureMin.Size = New System.Drawing.Size(59, 22)
        Me.txtTemperatureMin.TabIndex = 16
        Me.txtTemperatureMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtWeightMin
        '
        Me.txtWeightMin.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtWeightMin.Location = New System.Drawing.Point(447, 23)
        Me.txtWeightMin.MaxLength = 6
        Me.txtWeightMin.Name = "txtWeightMin"
        Me.txtWeightMin.Size = New System.Drawing.Size(59, 22)
        Me.txtWeightMin.TabIndex = 4
        Me.txtWeightMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblWeightMin
        '
        Me.lblWeightMin.AutoSize = True
        Me.lblWeightMin.BackColor = System.Drawing.Color.Transparent
        Me.lblWeightMin.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblWeightMin.Location = New System.Drawing.Point(343, 27)
        Me.lblWeightMin.Name = "lblWeightMin"
        Me.lblWeightMin.Size = New System.Drawing.Size(101, 14)
        Me.lblWeightMin.TabIndex = 3
        Me.lblWeightMin.Text = "Weight(lbs) Min :"
        Me.lblWeightMin.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTempratureMin
        '
        Me.lblTempratureMin.AutoSize = True
        Me.lblTempratureMin.BackColor = System.Drawing.Color.Transparent
        Me.lblTempratureMin.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTempratureMin.Location = New System.Drawing.Point(319, 132)
        Me.lblTempratureMin.Name = "lblTempratureMin"
        Me.lblTempratureMin.Size = New System.Drawing.Size(125, 14)
        Me.lblTempratureMin.TabIndex = 4
        Me.lblTempratureMin.Text = "Temperature(F) Min :"
        Me.lblTempratureMin.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblHeightMin
        '
        Me.lblHeightMin.AutoSize = True
        Me.lblHeightMin.BackColor = System.Drawing.Color.Transparent
        Me.lblHeightMin.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeightMin.Location = New System.Drawing.Point(10, 26)
        Me.lblHeightMin.Name = "lblHeightMin"
        Me.lblHeightMin.Size = New System.Drawing.Size(127, 14)
        Me.lblHeightMin.TabIndex = 1
        Me.lblHeightMin.Text = "Height(ft' inch'') Min :"
        Me.lblHeightMin.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.Panel7)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(0, 180)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.Panel3.Size = New System.Drawing.Size(639, 28)
        Me.Panel3.TabIndex = 46
        Me.Panel3.TabStop = True
        '
        'Panel7
        '
        Me.Panel7.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Blue2007
        Me.Panel7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel7.Controls.Add(Me.Label107)
        Me.Panel7.Controls.Add(Me.Label108)
        Me.Panel7.Controls.Add(Me.Label109)
        Me.Panel7.Controls.Add(Me.Label110)
        Me.Panel7.Controls.Add(Me.Label7)
        Me.Panel7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel7.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel7.Location = New System.Drawing.Point(0, 0)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(639, 25)
        Me.Panel7.TabIndex = 44
        '
        'Label107
        '
        Me.Label107.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label107.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label107.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label107.Location = New System.Drawing.Point(1, 24)
        Me.Label107.Name = "Label107"
        Me.Label107.Size = New System.Drawing.Size(637, 1)
        Me.Label107.TabIndex = 13
        Me.Label107.Text = "label2"
        '
        'Label108
        '
        Me.Label108.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label108.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label108.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label108.Location = New System.Drawing.Point(0, 1)
        Me.Label108.Name = "Label108"
        Me.Label108.Size = New System.Drawing.Size(1, 24)
        Me.Label108.TabIndex = 12
        Me.Label108.Text = "label4"
        '
        'Label109
        '
        Me.Label109.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label109.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label109.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label109.Location = New System.Drawing.Point(638, 1)
        Me.Label109.Name = "Label109"
        Me.Label109.Size = New System.Drawing.Size(1, 24)
        Me.Label109.TabIndex = 11
        Me.Label109.Text = "label3"
        '
        'Label110
        '
        Me.Label110.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label110.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label110.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label110.Location = New System.Drawing.Point(0, 0)
        Me.Label110.Name = "Label110"
        Me.Label110.Size = New System.Drawing.Size(639, 1)
        Me.Label110.TabIndex = 10
        Me.Label110.Text = "label1"
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label7.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.White
        Me.Label7.Image = CType(resources.GetObject("Label7.Image"), System.Drawing.Image)
        Me.Label7.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label7.Location = New System.Drawing.Point(0, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(639, 25)
        Me.Label7.TabIndex = 9
        Me.Label7.Text = "      Vitals"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnlDemographics
        '
        Me.pnlDemographics.BackColor = System.Drawing.Color.Transparent
        Me.pnlDemographics.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlDemographics.Controls.Add(Me.Label98)
        Me.pnlDemographics.Controls.Add(Me.Label97)
        Me.pnlDemographics.Controls.Add(Me.Label96)
        Me.pnlDemographics.Controls.Add(Me.Label103)
        Me.pnlDemographics.Controls.Add(Me.Label104)
        Me.pnlDemographics.Controls.Add(Me.Label105)
        Me.pnlDemographics.Controls.Add(Me.Label106)
        Me.pnlDemographics.Controls.Add(Me.lblEmpStatus)
        Me.pnlDemographics.Controls.Add(Me.cmbState)
        Me.pnlDemographics.Controls.Add(Me.cmbEmpStatus)
        Me.pnlDemographics.Controls.Add(Me.txtZip)
        Me.pnlDemographics.Controls.Add(Me.txtCity)
        Me.pnlDemographics.Controls.Add(Me.lblZip)
        Me.pnlDemographics.Controls.Add(Me.lblState)
        Me.pnlDemographics.Controls.Add(Me.lblCity)
        Me.pnlDemographics.Controls.Add(Me.cmbMaritalSt)
        Me.pnlDemographics.Controls.Add(Me.cmbRace)
        Me.pnlDemographics.Controls.Add(Me.cmbAgeMax)
        Me.pnlDemographics.Controls.Add(Me.cmbAgeMin)
        Me.pnlDemographics.Controls.Add(Me.cmbGender)
        Me.pnlDemographics.Controls.Add(Me.lblAgeMax)
        Me.pnlDemographics.Controls.Add(Me.lblMaritalStatus)
        Me.pnlDemographics.Controls.Add(Me.lblGender)
        Me.pnlDemographics.Controls.Add(Me.lblRace)
        Me.pnlDemographics.Controls.Add(Me.lblAgeMin)
        Me.pnlDemographics.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlDemographics.ForeColor = System.Drawing.Color.Black
        Me.pnlDemographics.Location = New System.Drawing.Point(0, 28)
        Me.pnlDemographics.Name = "pnlDemographics"
        Me.pnlDemographics.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.pnlDemographics.Size = New System.Drawing.Size(639, 152)
        Me.pnlDemographics.TabIndex = 0
        '
        'Label98
        '
        Me.Label98.AutoSize = True
        Me.Label98.ForeColor = System.Drawing.Color.Red
        Me.Label98.Location = New System.Drawing.Point(160, 16)
        Me.Label98.Name = "Label98"
        Me.Label98.Size = New System.Drawing.Size(14, 14)
        Me.Label98.TabIndex = 25
        Me.Label98.Text = "*"
        '
        'Label97
        '
        Me.Label97.AutoSize = True
        Me.Label97.ForeColor = System.Drawing.Color.Red
        Me.Label97.Location = New System.Drawing.Point(24, 50)
        Me.Label97.Name = "Label97"
        Me.Label97.Size = New System.Drawing.Size(14, 14)
        Me.Label97.TabIndex = 24
        Me.Label97.Text = "*"
        '
        'Label96
        '
        Me.Label96.AutoSize = True
        Me.Label96.ForeColor = System.Drawing.Color.Red
        Me.Label96.Location = New System.Drawing.Point(11, 19)
        Me.Label96.Name = "Label96"
        Me.Label96.Size = New System.Drawing.Size(14, 14)
        Me.Label96.TabIndex = 23
        Me.Label96.Text = "*"
        '
        'Label103
        '
        Me.Label103.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label103.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label103.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label103.Location = New System.Drawing.Point(1, 148)
        Me.Label103.Name = "Label103"
        Me.Label103.Size = New System.Drawing.Size(637, 1)
        Me.Label103.TabIndex = 22
        Me.Label103.Text = "label2"
        '
        'Label104
        '
        Me.Label104.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label104.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label104.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label104.Location = New System.Drawing.Point(0, 1)
        Me.Label104.Name = "Label104"
        Me.Label104.Size = New System.Drawing.Size(1, 148)
        Me.Label104.TabIndex = 21
        Me.Label104.Text = "label4"
        '
        'Label105
        '
        Me.Label105.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label105.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label105.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label105.Location = New System.Drawing.Point(638, 1)
        Me.Label105.Name = "Label105"
        Me.Label105.Size = New System.Drawing.Size(1, 148)
        Me.Label105.TabIndex = 20
        Me.Label105.Text = "label3"
        '
        'Label106
        '
        Me.Label106.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label106.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label106.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label106.Location = New System.Drawing.Point(0, 0)
        Me.Label106.Name = "Label106"
        Me.Label106.Size = New System.Drawing.Size(639, 1)
        Me.Label106.TabIndex = 19
        Me.Label106.Text = "label1"
        '
        'lblEmpStatus
        '
        Me.lblEmpStatus.AutoSize = True
        Me.lblEmpStatus.BackColor = System.Drawing.Color.Transparent
        Me.lblEmpStatus.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEmpStatus.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblEmpStatus.Location = New System.Drawing.Point(301, 117)
        Me.lblEmpStatus.Name = "lblEmpStatus"
        Me.lblEmpStatus.Size = New System.Drawing.Size(122, 14)
        Me.lblEmpStatus.TabIndex = 13
        Me.lblEmpStatus.Text = "Employment Status :"
        Me.lblEmpStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblEmpStatus.Visible = False
        '
        'cmbState
        '
        Me.cmbState.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbState.ForeColor = System.Drawing.Color.Black
        Me.cmbState.Location = New System.Drawing.Point(425, 46)
        Me.cmbState.Name = "cmbState"
        Me.cmbState.Size = New System.Drawing.Size(169, 22)
        Me.cmbState.TabIndex = 4
        Me.cmbState.Visible = False
        '
        'cmbEmpStatus
        '
        Me.cmbEmpStatus.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbEmpStatus.ForeColor = System.Drawing.Color.Black
        Me.cmbEmpStatus.Location = New System.Drawing.Point(425, 112)
        Me.cmbEmpStatus.Name = "cmbEmpStatus"
        Me.cmbEmpStatus.Size = New System.Drawing.Size(169, 22)
        Me.cmbEmpStatus.TabIndex = 8
        Me.cmbEmpStatus.Visible = False
        '
        'txtZip
        '
        Me.txtZip.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtZip.ForeColor = System.Drawing.Color.Black
        Me.txtZip.Location = New System.Drawing.Point(425, 79)
        Me.txtZip.MaxLength = 50
        Me.txtZip.Name = "txtZip"
        Me.txtZip.Size = New System.Drawing.Size(170, 22)
        Me.txtZip.TabIndex = 6
        Me.txtZip.Visible = False
        '
        'txtCity
        '
        Me.txtCity.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCity.ForeColor = System.Drawing.Color.Black
        Me.txtCity.Location = New System.Drawing.Point(425, 13)
        Me.txtCity.MaxLength = 50
        Me.txtCity.Name = "txtCity"
        Me.txtCity.Size = New System.Drawing.Size(170, 22)
        Me.txtCity.TabIndex = 2
        Me.txtCity.Visible = False
        '
        'lblZip
        '
        Me.lblZip.AutoSize = True
        Me.lblZip.BackColor = System.Drawing.Color.Transparent
        Me.lblZip.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblZip.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblZip.Location = New System.Drawing.Point(360, 84)
        Me.lblZip.Name = "lblZip"
        Me.lblZip.Size = New System.Drawing.Size(63, 14)
        Me.lblZip.TabIndex = 12
        Me.lblZip.Text = "Zip Code :"
        Me.lblZip.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblZip.Visible = False
        '
        'lblState
        '
        Me.lblState.AutoSize = True
        Me.lblState.BackColor = System.Drawing.Color.Transparent
        Me.lblState.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblState.Location = New System.Drawing.Point(378, 51)
        Me.lblState.Name = "lblState"
        Me.lblState.Size = New System.Drawing.Size(45, 14)
        Me.lblState.TabIndex = 11
        Me.lblState.Text = "State :"
        Me.lblState.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblState.Visible = False
        '
        'lblCity
        '
        Me.lblCity.AutoSize = True
        Me.lblCity.BackColor = System.Drawing.Color.Transparent
        Me.lblCity.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCity.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblCity.Location = New System.Drawing.Point(388, 18)
        Me.lblCity.Name = "lblCity"
        Me.lblCity.Size = New System.Drawing.Size(35, 14)
        Me.lblCity.TabIndex = 10
        Me.lblCity.Text = "City :"
        Me.lblCity.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblCity.Visible = False
        '
        'cmbMaritalSt
        '
        Me.cmbMaritalSt.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbMaritalSt.ForeColor = System.Drawing.Color.Black
        Me.cmbMaritalSt.Location = New System.Drawing.Point(93, 112)
        Me.cmbMaritalSt.Name = "cmbMaritalSt"
        Me.cmbMaritalSt.Size = New System.Drawing.Size(172, 22)
        Me.cmbMaritalSt.TabIndex = 7
        Me.cmbMaritalSt.Visible = False
        '
        'cmbRace
        '
        Me.cmbRace.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbRace.ForeColor = System.Drawing.Color.Black
        Me.cmbRace.Location = New System.Drawing.Point(93, 79)
        Me.cmbRace.Name = "cmbRace"
        Me.cmbRace.Size = New System.Drawing.Size(172, 22)
        Me.cmbRace.TabIndex = 5
        Me.cmbRace.Visible = False
        '
        'cmbAgeMax
        '
        Me.cmbAgeMax.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbAgeMax.ForeColor = System.Drawing.Color.Black
        Me.cmbAgeMax.Location = New System.Drawing.Point(205, 13)
        Me.cmbAgeMax.Name = "cmbAgeMax"
        Me.cmbAgeMax.Size = New System.Drawing.Size(60, 22)
        Me.cmbAgeMax.TabIndex = 1
        '
        'cmbAgeMin
        '
        Me.cmbAgeMin.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbAgeMin.ForeColor = System.Drawing.Color.Black
        Me.cmbAgeMin.Location = New System.Drawing.Point(93, 13)
        Me.cmbAgeMin.Name = "cmbAgeMin"
        Me.cmbAgeMin.Size = New System.Drawing.Size(66, 22)
        Me.cmbAgeMin.TabIndex = 0
        '
        'cmbGender
        '
        Me.cmbGender.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbGender.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbGender.ForeColor = System.Drawing.Color.Black
        Me.cmbGender.Location = New System.Drawing.Point(93, 46)
        Me.cmbGender.Name = "cmbGender"
        Me.cmbGender.Size = New System.Drawing.Size(172, 22)
        Me.cmbGender.TabIndex = 3
        '
        'lblAgeMax
        '
        Me.lblAgeMax.AutoSize = True
        Me.lblAgeMax.BackColor = System.Drawing.Color.Transparent
        Me.lblAgeMax.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAgeMax.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblAgeMax.Location = New System.Drawing.Point(171, 15)
        Me.lblAgeMax.Name = "lblAgeMax"
        Me.lblAgeMax.Size = New System.Drawing.Size(36, 14)
        Me.lblAgeMax.TabIndex = 18
        Me.lblAgeMax.Text = "Max :"
        Me.lblAgeMax.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblMaritalStatus
        '
        Me.lblMaritalStatus.AutoSize = True
        Me.lblMaritalStatus.BackColor = System.Drawing.Color.Transparent
        Me.lblMaritalStatus.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMaritalStatus.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblMaritalStatus.Location = New System.Drawing.Point(2, 116)
        Me.lblMaritalStatus.Name = "lblMaritalStatus"
        Me.lblMaritalStatus.Size = New System.Drawing.Size(88, 14)
        Me.lblMaritalStatus.TabIndex = 5
        Me.lblMaritalStatus.Text = "Marital Status :"
        Me.lblMaritalStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblMaritalStatus.Visible = False
        '
        'lblGender
        '
        Me.lblGender.AutoSize = True
        Me.lblGender.BackColor = System.Drawing.Color.Transparent
        Me.lblGender.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblGender.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblGender.Location = New System.Drawing.Point(35, 50)
        Me.lblGender.Name = "lblGender"
        Me.lblGender.Size = New System.Drawing.Size(55, 14)
        Me.lblGender.TabIndex = 3
        Me.lblGender.Text = "Gender :"
        Me.lblGender.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblRace
        '
        Me.lblRace.AutoSize = True
        Me.lblRace.BackColor = System.Drawing.Color.Transparent
        Me.lblRace.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRace.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblRace.Location = New System.Drawing.Point(49, 83)
        Me.lblRace.Name = "lblRace"
        Me.lblRace.Size = New System.Drawing.Size(41, 14)
        Me.lblRace.TabIndex = 4
        Me.lblRace.Text = "Race :"
        Me.lblRace.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblRace.Visible = False
        '
        'lblAgeMin
        '
        Me.lblAgeMin.AutoSize = True
        Me.lblAgeMin.BackColor = System.Drawing.Color.Transparent
        Me.lblAgeMin.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAgeMin.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblAgeMin.Location = New System.Drawing.Point(23, 17)
        Me.lblAgeMin.Name = "lblAgeMin"
        Me.lblAgeMin.Size = New System.Drawing.Size(67, 14)
        Me.lblAgeMin.TabIndex = 1
        Me.lblAgeMin.Text = "Age - Min :"
        Me.lblAgeMin.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.Panel6)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.Panel2.Size = New System.Drawing.Size(639, 28)
        Me.Panel2.TabIndex = 45
        Me.Panel2.TabStop = True
        '
        'Panel6
        '
        Me.Panel6.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Blue2007
        Me.Panel6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel6.Controls.Add(Me.Label65)
        Me.Panel6.Controls.Add(Me.Label85)
        Me.Panel6.Controls.Add(Me.Label89)
        Me.Panel6.Controls.Add(Me.Label90)
        Me.Panel6.Controls.Add(Me.Label2)
        Me.Panel6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel6.Location = New System.Drawing.Point(0, 0)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(639, 25)
        Me.Panel6.TabIndex = 19
        Me.Panel6.TabStop = True
        '
        'Label65
        '
        Me.Label65.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label65.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label65.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label65.Location = New System.Drawing.Point(1, 24)
        Me.Label65.Name = "Label65"
        Me.Label65.Size = New System.Drawing.Size(637, 1)
        Me.Label65.TabIndex = 13
        Me.Label65.Text = "label2"
        '
        'Label85
        '
        Me.Label85.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label85.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label85.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label85.Location = New System.Drawing.Point(0, 1)
        Me.Label85.Name = "Label85"
        Me.Label85.Size = New System.Drawing.Size(1, 24)
        Me.Label85.TabIndex = 12
        Me.Label85.Text = "label4"
        '
        'Label89
        '
        Me.Label89.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label89.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label89.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label89.Location = New System.Drawing.Point(638, 1)
        Me.Label89.Name = "Label89"
        Me.Label89.Size = New System.Drawing.Size(1, 24)
        Me.Label89.TabIndex = 11
        Me.Label89.Text = "label3"
        '
        'Label90
        '
        Me.Label90.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label90.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label90.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label90.Location = New System.Drawing.Point(0, 0)
        Me.Label90.Name = "Label90"
        Me.Label90.Size = New System.Drawing.Size(639, 1)
        Me.Label90.TabIndex = 10
        Me.Label90.Text = "label1"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.White
        Me.Label2.Image = CType(resources.GetObject("Label2.Image"), System.Drawing.Image)
        Me.Label2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label2.Location = New System.Drawing.Point(0, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(639, 25)
        Me.Label2.TabIndex = 9
        Me.Label2.Text = "      Demographics"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnlLab
        '
        Me.pnlLab.Controls.Add(Me.Panel12)
        Me.pnlLab.Controls.Add(Me.Panel13)
        Me.pnlLab.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlLab.Location = New System.Drawing.Point(0, 0)
        Me.pnlLab.Name = "pnlLab"
        Me.pnlLab.Size = New System.Drawing.Size(639, 576)
        Me.pnlLab.TabIndex = 2
        Me.pnlLab.Visible = False
        '
        'Panel12
        '
        Me.Panel12.BackColor = System.Drawing.Color.Transparent
        Me.Panel12.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel12.Controls.Add(Me.C1LabResult)
        Me.Panel12.Controls.Add(Me.Label146)
        Me.Panel12.Controls.Add(Me.Label147)
        Me.Panel12.Controls.Add(Me.Label148)
        Me.Panel12.Controls.Add(Me.Label149)
        Me.Panel12.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel12.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel12.Location = New System.Drawing.Point(0, 26)
        Me.Panel12.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Panel12.Name = "Panel12"
        Me.Panel12.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.Panel12.Size = New System.Drawing.Size(639, 550)
        Me.Panel12.TabIndex = 20
        '
        'C1LabResult
        '
        Me.C1LabResult.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.C1LabResult.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.C1LabResult.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.C1LabResult.ColumnInfo = "10,0,0,0,0,95,Columns:0{AllowDragging:False;}" & Global.Microsoft.VisualBasic.ChrW(9)
        Me.C1LabResult.Dock = System.Windows.Forms.DockStyle.Fill
        Me.C1LabResult.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.C1LabResult.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.C1LabResult.Location = New System.Drawing.Point(1, 1)
        Me.C1LabResult.Name = "C1LabResult"
        Me.C1LabResult.Rows.DefaultSize = 19
        Me.C1LabResult.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.C1LabResult.ShowCellLabels = True
        Me.C1LabResult.Size = New System.Drawing.Size(634, 545)
        Me.C1LabResult.StyleInfo = resources.GetString("C1LabResult.StyleInfo")
        Me.C1LabResult.TabIndex = 9
        Me.C1LabResult.Tree.NodeImageCollapsed = CType(resources.GetObject("C1LabResult.Tree.NodeImageCollapsed"), System.Drawing.Image)
        Me.C1LabResult.Tree.NodeImageExpanded = CType(resources.GetObject("C1LabResult.Tree.NodeImageExpanded"), System.Drawing.Image)
        '
        'Label146
        '
        Me.Label146.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label146.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label146.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label146.Location = New System.Drawing.Point(1, 546)
        Me.Label146.Name = "Label146"
        Me.Label146.Size = New System.Drawing.Size(634, 1)
        Me.Label146.TabIndex = 8
        Me.Label146.Text = "label2"
        '
        'Label147
        '
        Me.Label147.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label147.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label147.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label147.Location = New System.Drawing.Point(0, 1)
        Me.Label147.Name = "Label147"
        Me.Label147.Size = New System.Drawing.Size(1, 546)
        Me.Label147.TabIndex = 7
        Me.Label147.Text = "label4"
        '
        'Label148
        '
        Me.Label148.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label148.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label148.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label148.Location = New System.Drawing.Point(635, 1)
        Me.Label148.Name = "Label148"
        Me.Label148.Size = New System.Drawing.Size(1, 546)
        Me.Label148.TabIndex = 6
        Me.Label148.Text = "label3"
        '
        'Label149
        '
        Me.Label149.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label149.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label149.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label149.Location = New System.Drawing.Point(0, 0)
        Me.Label149.Name = "Label149"
        Me.Label149.Size = New System.Drawing.Size(636, 1)
        Me.Label149.TabIndex = 5
        Me.Label149.Text = "label1"
        '
        'Panel13
        '
        Me.Panel13.Controls.Add(Me.Panel25)
        Me.Panel13.Controls.Add(Me.Panel14)
        Me.Panel13.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel13.Location = New System.Drawing.Point(0, 0)
        Me.Panel13.Name = "Panel13"
        Me.Panel13.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.Panel13.Size = New System.Drawing.Size(639, 26)
        Me.Panel13.TabIndex = 21
        '
        'Panel25
        '
        Me.Panel25.BackColor = System.Drawing.Color.Transparent
        Me.Panel25.Controls.Add(Me.txtLabResultSearch)
        Me.Panel25.Controls.Add(Me.Label126)
        Me.Panel25.Controls.Add(Me.Label127)
        Me.Panel25.Controls.Add(Me.PictureBox3)
        Me.Panel25.Controls.Add(Me.Label99)
        Me.Panel25.Controls.Add(Me.Label100)
        Me.Panel25.Controls.Add(Me.Label101)
        Me.Panel25.Controls.Add(Me.Label102)
        Me.Panel25.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel25.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel25.ForeColor = System.Drawing.Color.Black
        Me.Panel25.Location = New System.Drawing.Point(0, 0)
        Me.Panel25.Name = "Panel25"
        Me.Panel25.Size = New System.Drawing.Size(636, 23)
        Me.Panel25.TabIndex = 22
        '
        'txtLabResultSearch
        '
        Me.txtLabResultSearch.BackColor = System.Drawing.Color.White
        Me.txtLabResultSearch.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtLabResultSearch.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtLabResultSearch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLabResultSearch.ForeColor = System.Drawing.Color.Black
        Me.txtLabResultSearch.Location = New System.Drawing.Point(29, 5)
        Me.txtLabResultSearch.Name = "txtLabResultSearch"
        Me.txtLabResultSearch.Size = New System.Drawing.Size(606, 15)
        Me.txtLabResultSearch.TabIndex = 0
        '
        'Label126
        '
        Me.Label126.BackColor = System.Drawing.Color.White
        Me.Label126.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label126.Location = New System.Drawing.Point(29, 1)
        Me.Label126.Name = "Label126"
        Me.Label126.Size = New System.Drawing.Size(606, 4)
        Me.Label126.TabIndex = 37
        '
        'Label127
        '
        Me.Label127.BackColor = System.Drawing.Color.White
        Me.Label127.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label127.Location = New System.Drawing.Point(29, 20)
        Me.Label127.Name = "Label127"
        Me.Label127.Size = New System.Drawing.Size(606, 2)
        Me.Label127.TabIndex = 38
        '
        'PictureBox3
        '
        Me.PictureBox3.BackColor = System.Drawing.Color.White
        Me.PictureBox3.Dock = System.Windows.Forms.DockStyle.Left
        Me.PictureBox3.Image = CType(resources.GetObject("PictureBox3.Image"), System.Drawing.Image)
        Me.PictureBox3.Location = New System.Drawing.Point(1, 1)
        Me.PictureBox3.Name = "PictureBox3"
        Me.PictureBox3.Size = New System.Drawing.Size(28, 21)
        Me.PictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox3.TabIndex = 9
        Me.PictureBox3.TabStop = False
        '
        'Label99
        '
        Me.Label99.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label99.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label99.Location = New System.Drawing.Point(1, 22)
        Me.Label99.Name = "Label99"
        Me.Label99.Size = New System.Drawing.Size(634, 1)
        Me.Label99.TabIndex = 35
        Me.Label99.Text = "label1"
        '
        'Label100
        '
        Me.Label100.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label100.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label100.Location = New System.Drawing.Point(1, 0)
        Me.Label100.Name = "Label100"
        Me.Label100.Size = New System.Drawing.Size(634, 1)
        Me.Label100.TabIndex = 36
        Me.Label100.Text = "label1"
        '
        'Label101
        '
        Me.Label101.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label101.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label101.Location = New System.Drawing.Point(0, 0)
        Me.Label101.Name = "Label101"
        Me.Label101.Size = New System.Drawing.Size(1, 23)
        Me.Label101.TabIndex = 39
        Me.Label101.Text = "label4"
        '
        'Label102
        '
        Me.Label102.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label102.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label102.Location = New System.Drawing.Point(635, 0)
        Me.Label102.Name = "Label102"
        Me.Label102.Size = New System.Drawing.Size(1, 23)
        Me.Label102.TabIndex = 40
        Me.Label102.Text = "label4"
        '
        'Panel14
        '
        Me.Panel14.BackColor = System.Drawing.Color.Transparent
        Me.Panel14.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Blue2007
        Me.Panel14.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel14.Controls.Add(Me.Label155)
        Me.Panel14.Controls.Add(Me.Label150)
        Me.Panel14.Controls.Add(Me.Label151)
        Me.Panel14.Controls.Add(Me.Label152)
        Me.Panel14.Controls.Add(Me.Label153)
        Me.Panel14.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel14.Location = New System.Drawing.Point(0, 0)
        Me.Panel14.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Panel14.Name = "Panel14"
        Me.Panel14.Size = New System.Drawing.Size(564, 25)
        Me.Panel14.TabIndex = 20
        Me.Panel14.Visible = False
        '
        'Label155
        '
        Me.Label155.BackColor = System.Drawing.Color.Transparent
        Me.Label155.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label155.ForeColor = System.Drawing.Color.White
        Me.Label155.Image = CType(resources.GetObject("Label155.Image"), System.Drawing.Image)
        Me.Label155.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label155.Location = New System.Drawing.Point(1, 1)
        Me.Label155.Name = "Label155"
        Me.Label155.Size = New System.Drawing.Size(567, 23)
        Me.Label155.TabIndex = 10
        Me.Label155.Text = "     Lab "
        Me.Label155.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label150
        '
        Me.Label150.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label150.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label150.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label150.Location = New System.Drawing.Point(1, 24)
        Me.Label150.Name = "Label150"
        Me.Label150.Size = New System.Drawing.Size(562, 1)
        Me.Label150.TabIndex = 8
        Me.Label150.Text = "label2"
        '
        'Label151
        '
        Me.Label151.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label151.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label151.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label151.Location = New System.Drawing.Point(0, 1)
        Me.Label151.Name = "Label151"
        Me.Label151.Size = New System.Drawing.Size(1, 24)
        Me.Label151.TabIndex = 7
        Me.Label151.Text = "label4"
        '
        'Label152
        '
        Me.Label152.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label152.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label152.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label152.Location = New System.Drawing.Point(563, 1)
        Me.Label152.Name = "Label152"
        Me.Label152.Size = New System.Drawing.Size(1, 24)
        Me.Label152.TabIndex = 6
        Me.Label152.Text = "label3"
        '
        'Label153
        '
        Me.Label153.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label153.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label153.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label153.Location = New System.Drawing.Point(0, 0)
        Me.Label153.Name = "Label153"
        Me.Label153.Size = New System.Drawing.Size(564, 1)
        Me.Label153.TabIndex = 5
        Me.Label153.Text = "label1"
        '
        'pnlHistory
        '
        Me.pnlHistory.Controls.Add(Me.Panel22)
        Me.pnlHistory.Controls.Add(Me.Panel8)
        Me.pnlHistory.Controls.Add(Me.Splitter1)
        Me.pnlHistory.Controls.Add(Me.Panel9)
        Me.pnlHistory.Controls.Add(Me.pnlHistoryLeft)
        Me.pnlHistory.Controls.Add(Me.Panel23)
        Me.pnlHistory.Controls.Add(Me.btnHistorySearch)
        Me.pnlHistory.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlHistory.Location = New System.Drawing.Point(0, 0)
        Me.pnlHistory.Name = "pnlHistory"
        Me.pnlHistory.Size = New System.Drawing.Size(639, 576)
        Me.pnlHistory.TabIndex = 12
        Me.pnlHistory.Visible = False
        '
        'Panel22
        '
        Me.Panel22.BackColor = System.Drawing.Color.Transparent
        Me.Panel22.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel22.Controls.Add(Me.Label193)
        Me.Panel22.Controls.Add(Me.Label194)
        Me.Panel22.Controls.Add(Me.trvSelectedHistory)
        Me.Panel22.Controls.Add(Me.Label195)
        Me.Panel22.Controls.Add(Me.Label196)
        Me.Panel22.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel22.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel22.Location = New System.Drawing.Point(0, 428)
        Me.Panel22.Margin = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.Panel22.Name = "Panel22"
        Me.Panel22.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.Panel22.Size = New System.Drawing.Size(639, 148)
        Me.Panel22.TabIndex = 23
        '
        'Label193
        '
        Me.Label193.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label193.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label193.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label193.Location = New System.Drawing.Point(1, 144)
        Me.Label193.Name = "Label193"
        Me.Label193.Size = New System.Drawing.Size(634, 1)
        Me.Label193.TabIndex = 8
        Me.Label193.Text = "label2"
        '
        'Label194
        '
        Me.Label194.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label194.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label194.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label194.Location = New System.Drawing.Point(0, 1)
        Me.Label194.Name = "Label194"
        Me.Label194.Size = New System.Drawing.Size(1, 144)
        Me.Label194.TabIndex = 7
        Me.Label194.Text = "label4"
        '
        'trvSelectedHistory
        '
        Me.trvSelectedHistory.BackColor = System.Drawing.Color.White
        Me.trvSelectedHistory.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trvSelectedHistory.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trvSelectedHistory.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.trvSelectedHistory.ForeColor = System.Drawing.Color.Black
        Me.trvSelectedHistory.HideSelection = False
        Me.trvSelectedHistory.ImageIndex = 11
        Me.trvSelectedHistory.ImageList = Me.ImageList1
        Me.trvSelectedHistory.ItemHeight = 18
        Me.trvSelectedHistory.Location = New System.Drawing.Point(0, 1)
        Me.trvSelectedHistory.Name = "trvSelectedHistory"
        Me.trvSelectedHistory.SelectedImageIndex = 11
        Me.trvSelectedHistory.ShowLines = False
        Me.trvSelectedHistory.Size = New System.Drawing.Size(635, 144)
        Me.trvSelectedHistory.TabIndex = 0
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "Bullet06.ico")
        Me.ImageList1.Images.SetKeyName(1, "Pat Demographic.ico")
        Me.ImageList1.Images.SetKeyName(2, "History.ico")
        Me.ImageList1.Images.SetKeyName(3, "ICD 09.ico")
        Me.ImageList1.Images.SetKeyName(4, "Drugs.ico")
        Me.ImageList1.Images.SetKeyName(5, "CPT.ico")
        Me.ImageList1.Images.SetKeyName(6, "Lab.ico")
        Me.ImageList1.Images.SetKeyName(7, "Radiology.ico")
        Me.ImageList1.Images.SetKeyName(8, "Guideline Template.ico")
        Me.ImageList1.Images.SetKeyName(9, "RX.ico")
        Me.ImageList1.Images.SetKeyName(10, "PatientDetails.ico")
        Me.ImageList1.Images.SetKeyName(11, "Small Arrow.ico")
        '
        'Label195
        '
        Me.Label195.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label195.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label195.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label195.Location = New System.Drawing.Point(635, 1)
        Me.Label195.Name = "Label195"
        Me.Label195.Size = New System.Drawing.Size(1, 144)
        Me.Label195.TabIndex = 6
        Me.Label195.Text = "label3"
        '
        'Label196
        '
        Me.Label196.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label196.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label196.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label196.Location = New System.Drawing.Point(0, 0)
        Me.Label196.Name = "Label196"
        Me.Label196.Size = New System.Drawing.Size(636, 1)
        Me.Label196.TabIndex = 5
        Me.Label196.Text = "label1"
        '
        'Panel8
        '
        Me.Panel8.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel8.Controls.Add(Me.Panel21)
        Me.Panel8.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel8.Location = New System.Drawing.Point(0, 403)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.Panel8.Size = New System.Drawing.Size(639, 25)
        Me.Panel8.TabIndex = 22
        '
        'Panel21
        '
        Me.Panel21.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Blue2007
        Me.Panel21.Controls.Add(Me.Label188)
        Me.Panel21.Controls.Add(Me.Label189)
        Me.Panel21.Controls.Add(Me.Label190)
        Me.Panel21.Controls.Add(Me.Label191)
        Me.Panel21.Controls.Add(Me.Label192)
        Me.Panel21.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel21.Location = New System.Drawing.Point(0, 0)
        Me.Panel21.Name = "Panel21"
        Me.Panel21.Size = New System.Drawing.Size(636, 22)
        Me.Panel21.TabIndex = 14
        '
        'Label188
        '
        Me.Label188.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label188.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label188.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label188.Location = New System.Drawing.Point(635, 1)
        Me.Label188.Name = "Label188"
        Me.Label188.Size = New System.Drawing.Size(1, 20)
        Me.Label188.TabIndex = 11
        Me.Label188.Text = "label3"
        '
        'Label189
        '
        Me.Label189.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label189.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label189.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label189.Location = New System.Drawing.Point(0, 1)
        Me.Label189.Name = "Label189"
        Me.Label189.Size = New System.Drawing.Size(1, 20)
        Me.Label189.TabIndex = 12
        Me.Label189.Text = "label4"
        '
        'Label190
        '
        Me.Label190.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label190.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label190.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label190.Location = New System.Drawing.Point(0, 0)
        Me.Label190.Name = "Label190"
        Me.Label190.Size = New System.Drawing.Size(636, 1)
        Me.Label190.TabIndex = 10
        Me.Label190.Text = "label1"
        '
        'Label191
        '
        Me.Label191.BackColor = System.Drawing.Color.Transparent
        Me.Label191.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label191.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label191.ForeColor = System.Drawing.Color.White
        Me.Label191.Image = CType(resources.GetObject("Label191.Image"), System.Drawing.Image)
        Me.Label191.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label191.Location = New System.Drawing.Point(0, 0)
        Me.Label191.Name = "Label191"
        Me.Label191.Size = New System.Drawing.Size(636, 21)
        Me.Label191.TabIndex = 9
        Me.Label191.Text = "      Selected History"
        Me.Label191.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label192
        '
        Me.Label192.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label192.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label192.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label192.Location = New System.Drawing.Point(0, 21)
        Me.Label192.Name = "Label192"
        Me.Label192.Size = New System.Drawing.Size(636, 1)
        Me.Label192.TabIndex = 13
        Me.Label192.Text = "label2"
        '
        'Splitter1
        '
        Me.Splitter1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Splitter1.Location = New System.Drawing.Point(0, 400)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(639, 3)
        Me.Splitter1.TabIndex = 24
        Me.Splitter1.TabStop = False
        '
        'Panel9
        '
        Me.Panel9.Controls.Add(Me.GloUC_trvHistory)
        Me.Panel9.Controls.Add(Me.trvHistoryRight)
        Me.Panel9.Controls.Add(Me.txtSearch)
        Me.Panel9.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel9.Location = New System.Drawing.Point(0, 27)
        Me.Panel9.Name = "Panel9"
        Me.Panel9.Padding = New System.Windows.Forms.Padding(0, 0, 3, 0)
        Me.Panel9.Size = New System.Drawing.Size(639, 373)
        Me.Panel9.TabIndex = 20
        '
        'GloUC_trvHistory
        '
        Me.GloUC_trvHistory.BackColor = System.Drawing.Color.Transparent
        Me.GloUC_trvHistory.CheckBoxes = False
        Me.GloUC_trvHistory.CodeMember = Nothing

        Me.GloUC_trvHistory.DescriptionMember = Nothing
        Me.GloUC_trvHistory.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Code_Description
        Me.GloUC_trvHistory.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GloUC_trvHistory.DrugFlag = CType(16, Short)
        Me.GloUC_trvHistory.DrugFormMember = Nothing
        Me.GloUC_trvHistory.DrugQtyQualifierMember = Nothing
        Me.GloUC_trvHistory.DurationMember = Nothing
        Me.GloUC_trvHistory.FrequencyMember = Nothing
        Me.GloUC_trvHistory.ImageIndex = 0
        Me.GloUC_trvHistory.ImageList = Me.ImageList1
        Me.GloUC_trvHistory.ImageObject = Nothing
        Me.GloUC_trvHistory.IsDrug = False
        Me.GloUC_trvHistory.IsNarcoticsMember = Nothing
        Me.GloUC_trvHistory.Location = New System.Drawing.Point(0, 0)
        Me.GloUC_trvHistory.MaximumNodes = 1000
        Me.GloUC_trvHistory.Name = "GloUC_trvHistory"
        Me.GloUC_trvHistory.NDCCodeMember = Nothing
        Me.GloUC_trvHistory.ParentImageIndex = 0
        Me.GloUC_trvHistory.ParentMember = Nothing
        Me.GloUC_trvHistory.RouteMember = Nothing
        Me.GloUC_trvHistory.Search = gloUserControlLibrary.gloUC_TreeView.enumSearchType.Instring
        Me.GloUC_trvHistory.SearchBox = True
        Me.GloUC_trvHistory.SearchText = Nothing
        Me.GloUC_trvHistory.SelectedImageIndex = 0
        Me.GloUC_trvHistory.SelectedNode = Nothing
        Me.GloUC_trvHistory.SelectedNodeIDs = CType(resources.GetObject("GloUC_trvHistory.SelectedNodeIDs"), System.Collections.ArrayList)
        Me.GloUC_trvHistory.SelectedParentImageIndex = 0
        Me.GloUC_trvHistory.Size = New System.Drawing.Size(636, 373)
        Me.GloUC_trvHistory.Sort = gloUserControlLibrary.gloUC_TreeView.enumSortType.ByDescription
        Me.GloUC_trvHistory.TabIndex = 49
        Me.GloUC_trvHistory.Tag = Nothing
        Me.GloUC_trvHistory.UnitMember = Nothing
        Me.GloUC_trvHistory.ValueMember = Nothing
        '
        'trvHistoryRight
        '
        Me.trvHistoryRight.BackColor = System.Drawing.Color.White
        Me.trvHistoryRight.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trvHistoryRight.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.trvHistoryRight.ForeColor = System.Drawing.Color.Black
        Me.trvHistoryRight.HideSelection = False
        Me.trvHistoryRight.ImageIndex = 0
        Me.trvHistoryRight.ImageList = Me.ImageList1
        Me.trvHistoryRight.ItemHeight = 18
        Me.trvHistoryRight.Location = New System.Drawing.Point(0, 1)
        Me.trvHistoryRight.Name = "trvHistoryRight"
        Me.trvHistoryRight.SelectedImageIndex = 0
        Me.trvHistoryRight.ShowLines = False
        Me.trvHistoryRight.Size = New System.Drawing.Size(348, 274)
        Me.trvHistoryRight.TabIndex = 2
        Me.trvHistoryRight.Visible = False
        '
        'txtSearch
        '
        Me.txtSearch.BackColor = System.Drawing.Color.White
        Me.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtSearch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSearch.ForeColor = System.Drawing.Color.Black
        Me.txtSearch.Location = New System.Drawing.Point(189, 103)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(317, 15)
        Me.txtSearch.TabIndex = 1
        Me.txtSearch.Visible = False
        '
        'pnlHistoryLeft
        '
        Me.pnlHistoryLeft.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlHistoryLeft.Controls.Add(Me.Label120)
        Me.pnlHistoryLeft.Controls.Add(Me.Label121)
        Me.pnlHistoryLeft.Controls.Add(Me.Label122)
        Me.pnlHistoryLeft.Controls.Add(Me.Label123)
        Me.pnlHistoryLeft.Controls.Add(Me.trvHistory)
        Me.pnlHistoryLeft.Enabled = False
        Me.pnlHistoryLeft.Location = New System.Drawing.Point(165, 127)
        Me.pnlHistoryLeft.Name = "pnlHistoryLeft"
        Me.pnlHistoryLeft.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.pnlHistoryLeft.Size = New System.Drawing.Size(70, 49)
        Me.pnlHistoryLeft.TabIndex = 1
        Me.pnlHistoryLeft.Visible = False
        '
        'Label120
        '
        Me.Label120.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label120.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label120.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label120.Location = New System.Drawing.Point(1, 45)
        Me.Label120.Name = "Label120"
        Me.Label120.Size = New System.Drawing.Size(65, 1)
        Me.Label120.TabIndex = 12
        Me.Label120.Text = "label2"
        '
        'Label121
        '
        Me.Label121.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label121.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label121.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label121.Location = New System.Drawing.Point(0, 1)
        Me.Label121.Name = "Label121"
        Me.Label121.Size = New System.Drawing.Size(1, 45)
        Me.Label121.TabIndex = 11
        Me.Label121.Text = "label4"
        '
        'Label122
        '
        Me.Label122.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label122.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label122.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label122.Location = New System.Drawing.Point(66, 1)
        Me.Label122.Name = "Label122"
        Me.Label122.Size = New System.Drawing.Size(1, 45)
        Me.Label122.TabIndex = 10
        Me.Label122.Text = "label3"
        '
        'Label123
        '
        Me.Label123.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label123.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label123.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label123.Location = New System.Drawing.Point(0, 0)
        Me.Label123.Name = "Label123"
        Me.Label123.Size = New System.Drawing.Size(67, 1)
        Me.Label123.TabIndex = 9
        Me.Label123.Text = "label1"
        '
        'trvHistory
        '
        Me.trvHistory.BackColor = System.Drawing.Color.White
        Me.trvHistory.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trvHistory.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trvHistory.ForeColor = System.Drawing.Color.Black
        Me.trvHistory.HideSelection = False
        Me.trvHistory.ItemHeight = 18
        Me.trvHistory.Location = New System.Drawing.Point(0, 0)
        Me.trvHistory.Name = "trvHistory"
        Me.trvHistory.ShowLines = False
        Me.trvHistory.Size = New System.Drawing.Size(67, 46)
        Me.trvHistory.TabIndex = 0
        Me.trvHistory.Visible = False
        '
        'Panel23
        '
        Me.Panel23.Controls.Add(Me.Panel24)
        Me.Panel23.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel23.Location = New System.Drawing.Point(0, 0)
        Me.Panel23.Name = "Panel23"
        Me.Panel23.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.Panel23.Size = New System.Drawing.Size(639, 27)
        Me.Panel23.TabIndex = 21
        '
        'Panel24
        '
        Me.Panel24.BackColor = System.Drawing.Color.Transparent
        Me.Panel24.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.Panel24.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel24.Controls.Add(Me.cmbHistoryCategory)
        Me.Panel24.Controls.Add(Me.Label115)
        Me.Panel24.Controls.Add(Me.Label116)
        Me.Panel24.Controls.Add(Me.Label117)
        Me.Panel24.Controls.Add(Me.Label118)
        Me.Panel24.Controls.Add(Me.Label119)
        Me.Panel24.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel24.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel24.ForeColor = System.Drawing.Color.Black
        Me.Panel24.Location = New System.Drawing.Point(0, 0)
        Me.Panel24.Name = "Panel24"
        Me.Panel24.Size = New System.Drawing.Size(636, 24)
        Me.Panel24.TabIndex = 19
        '
        'cmbHistoryCategory
        '
        Me.cmbHistoryCategory.Dock = System.Windows.Forms.DockStyle.Left
        Me.cmbHistoryCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbHistoryCategory.ForeColor = System.Drawing.Color.Black
        Me.cmbHistoryCategory.FormattingEnabled = True
        Me.cmbHistoryCategory.Location = New System.Drawing.Point(131, 1)
        Me.cmbHistoryCategory.Name = "cmbHistoryCategory"
        Me.cmbHistoryCategory.Size = New System.Drawing.Size(287, 22)
        Me.cmbHistoryCategory.TabIndex = 0
        '
        'Label115
        '
        Me.Label115.BackColor = System.Drawing.Color.Transparent
        Me.Label115.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label115.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label115.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label115.Location = New System.Drawing.Point(1, 1)
        Me.Label115.Name = "Label115"
        Me.Label115.Size = New System.Drawing.Size(130, 22)
        Me.Label115.TabIndex = 41
        Me.Label115.Text = "History Category :"
        Me.Label115.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label116
        '
        Me.Label116.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label116.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label116.Location = New System.Drawing.Point(1, 23)
        Me.Label116.Name = "Label116"
        Me.Label116.Size = New System.Drawing.Size(634, 1)
        Me.Label116.TabIndex = 35
        Me.Label116.Text = "label1"
        '
        'Label117
        '
        Me.Label117.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label117.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label117.Location = New System.Drawing.Point(1, 0)
        Me.Label117.Name = "Label117"
        Me.Label117.Size = New System.Drawing.Size(634, 1)
        Me.Label117.TabIndex = 36
        Me.Label117.Text = "label1"
        '
        'Label118
        '
        Me.Label118.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label118.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label118.Location = New System.Drawing.Point(0, 0)
        Me.Label118.Name = "Label118"
        Me.Label118.Size = New System.Drawing.Size(1, 24)
        Me.Label118.TabIndex = 39
        Me.Label118.Text = "label4"
        '
        'Label119
        '
        Me.Label119.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label119.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label119.Location = New System.Drawing.Point(635, 0)
        Me.Label119.Name = "Label119"
        Me.Label119.Size = New System.Drawing.Size(1, 24)
        Me.Label119.TabIndex = 40
        Me.Label119.Text = "label4"
        '
        'btnHistorySearch
        '
        Me.btnHistorySearch.BackColor = System.Drawing.Color.Transparent
        Me.btnHistorySearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnHistorySearch.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.btnHistorySearch.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.btnHistorySearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnHistorySearch.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnHistorySearch.Location = New System.Drawing.Point(254, 134)
        Me.btnHistorySearch.Name = "btnHistorySearch"
        Me.btnHistorySearch.Size = New System.Drawing.Size(63, 22)
        Me.btnHistorySearch.TabIndex = 2
        Me.btnHistorySearch.Text = "Search"
        Me.btnHistorySearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnHistorySearch.UseVisualStyleBackColor = False
        Me.btnHistorySearch.Visible = False
        '
        'pnlDrugs
        '
        Me.pnlDrugs.Controls.Add(Me.pnltrvSelectedDrugs)
        Me.pnlDrugs.Controls.Add(Me.pnlSelectedDrugLabel)
        Me.pnlDrugs.Controls.Add(Me.Splitter2)
        Me.pnlDrugs.Controls.Add(Me.Panel11)
        Me.pnlDrugs.Controls.Add(Me.Panel4)
        Me.pnlDrugs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlDrugs.Location = New System.Drawing.Point(0, 0)
        Me.pnlDrugs.Name = "pnlDrugs"
        Me.pnlDrugs.Size = New System.Drawing.Size(639, 576)
        Me.pnlDrugs.TabIndex = 1
        Me.pnlDrugs.Visible = False
        '
        'pnltrvSelectedDrugs
        '
        Me.pnltrvSelectedDrugs.BackColor = System.Drawing.Color.Transparent
        Me.pnltrvSelectedDrugs.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnltrvSelectedDrugs.Controls.Add(Me.Label86)
        Me.pnltrvSelectedDrugs.Controls.Add(Me.Label180)
        Me.pnltrvSelectedDrugs.Controls.Add(Me.trvSelectedDrugs)
        Me.pnltrvSelectedDrugs.Controls.Add(Me.Label181)
        Me.pnltrvSelectedDrugs.Controls.Add(Me.Label182)
        Me.pnltrvSelectedDrugs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnltrvSelectedDrugs.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnltrvSelectedDrugs.Location = New System.Drawing.Point(0, 422)
        Me.pnltrvSelectedDrugs.Margin = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.pnltrvSelectedDrugs.Name = "pnltrvSelectedDrugs"
        Me.pnltrvSelectedDrugs.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.pnltrvSelectedDrugs.Size = New System.Drawing.Size(639, 154)
        Me.pnltrvSelectedDrugs.TabIndex = 22
        '
        'Label86
        '
        Me.Label86.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label86.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label86.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label86.Location = New System.Drawing.Point(1, 150)
        Me.Label86.Name = "Label86"
        Me.Label86.Size = New System.Drawing.Size(634, 1)
        Me.Label86.TabIndex = 8
        Me.Label86.Text = "label2"
        '
        'Label180
        '
        Me.Label180.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label180.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label180.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label180.Location = New System.Drawing.Point(0, 1)
        Me.Label180.Name = "Label180"
        Me.Label180.Size = New System.Drawing.Size(1, 150)
        Me.Label180.TabIndex = 7
        Me.Label180.Text = "label4"
        '
        'trvSelectedDrugs
        '
        Me.trvSelectedDrugs.BackColor = System.Drawing.Color.White
        Me.trvSelectedDrugs.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trvSelectedDrugs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trvSelectedDrugs.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.trvSelectedDrugs.ForeColor = System.Drawing.Color.Black
        Me.trvSelectedDrugs.HideSelection = False
        Me.trvSelectedDrugs.ImageIndex = 0
        Me.trvSelectedDrugs.ImageList = Me.ImageList1
        Me.trvSelectedDrugs.ItemHeight = 18
        Me.trvSelectedDrugs.Location = New System.Drawing.Point(0, 1)
        Me.trvSelectedDrugs.Name = "trvSelectedDrugs"
        Me.trvSelectedDrugs.SelectedImageIndex = 0
        Me.trvSelectedDrugs.ShowLines = False
        Me.trvSelectedDrugs.Size = New System.Drawing.Size(635, 150)
        Me.trvSelectedDrugs.TabIndex = 0
        '
        'Label181
        '
        Me.Label181.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label181.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label181.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label181.Location = New System.Drawing.Point(635, 1)
        Me.Label181.Name = "Label181"
        Me.Label181.Size = New System.Drawing.Size(1, 150)
        Me.Label181.TabIndex = 6
        Me.Label181.Text = "label3"
        '
        'Label182
        '
        Me.Label182.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label182.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label182.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label182.Location = New System.Drawing.Point(0, 0)
        Me.Label182.Name = "Label182"
        Me.Label182.Size = New System.Drawing.Size(636, 1)
        Me.Label182.TabIndex = 5
        Me.Label182.Text = "label1"
        '
        'pnlSelectedDrugLabel
        '
        Me.pnlSelectedDrugLabel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlSelectedDrugLabel.Controls.Add(Me.Panel20)
        Me.pnlSelectedDrugLabel.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlSelectedDrugLabel.Location = New System.Drawing.Point(0, 397)
        Me.pnlSelectedDrugLabel.Name = "pnlSelectedDrugLabel"
        Me.pnlSelectedDrugLabel.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.pnlSelectedDrugLabel.Size = New System.Drawing.Size(639, 25)
        Me.pnlSelectedDrugLabel.TabIndex = 21
        '
        'Panel20
        '
        Me.Panel20.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Blue2007
        Me.Panel20.Controls.Add(Me.Label185)
        Me.Panel20.Controls.Add(Me.Label184)
        Me.Panel20.Controls.Add(Me.Label186)
        Me.Panel20.Controls.Add(Me.Label187)
        Me.Panel20.Controls.Add(Me.Label183)
        Me.Panel20.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel20.Location = New System.Drawing.Point(0, 0)
        Me.Panel20.Name = "Panel20"
        Me.Panel20.Size = New System.Drawing.Size(636, 22)
        Me.Panel20.TabIndex = 14
        '
        'Label185
        '
        Me.Label185.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label185.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label185.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label185.Location = New System.Drawing.Point(635, 1)
        Me.Label185.Name = "Label185"
        Me.Label185.Size = New System.Drawing.Size(1, 20)
        Me.Label185.TabIndex = 11
        Me.Label185.Text = "label3"
        '
        'Label184
        '
        Me.Label184.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label184.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label184.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label184.Location = New System.Drawing.Point(0, 1)
        Me.Label184.Name = "Label184"
        Me.Label184.Size = New System.Drawing.Size(1, 20)
        Me.Label184.TabIndex = 12
        Me.Label184.Text = "label4"
        '
        'Label186
        '
        Me.Label186.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label186.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label186.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label186.Location = New System.Drawing.Point(0, 0)
        Me.Label186.Name = "Label186"
        Me.Label186.Size = New System.Drawing.Size(636, 1)
        Me.Label186.TabIndex = 10
        Me.Label186.Text = "label1"
        '
        'Label187
        '
        Me.Label187.BackColor = System.Drawing.Color.Transparent
        Me.Label187.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label187.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label187.ForeColor = System.Drawing.Color.White
        Me.Label187.Image = CType(resources.GetObject("Label187.Image"), System.Drawing.Image)
        Me.Label187.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label187.Location = New System.Drawing.Point(0, 0)
        Me.Label187.Name = "Label187"
        Me.Label187.Size = New System.Drawing.Size(636, 21)
        Me.Label187.TabIndex = 9
        Me.Label187.Text = "      Selected Drugs"
        Me.Label187.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label183
        '
        Me.Label183.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label183.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label183.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label183.Location = New System.Drawing.Point(0, 21)
        Me.Label183.Name = "Label183"
        Me.Label183.Size = New System.Drawing.Size(636, 1)
        Me.Label183.TabIndex = 13
        Me.Label183.Text = "label2"
        '
        'Splitter2
        '
        Me.Splitter2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Splitter2.Location = New System.Drawing.Point(0, 394)
        Me.Splitter2.Name = "Splitter2"
        Me.Splitter2.Size = New System.Drawing.Size(639, 3)
        Me.Splitter2.TabIndex = 23
        Me.Splitter2.TabStop = False
        '
        'Panel11
        '
        Me.Panel11.BackColor = System.Drawing.Color.Transparent
        Me.Panel11.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel11.Controls.Add(Me.GloUC_trvDrugs)
        Me.Panel11.Controls.Add(Me.trvDrgs)
        Me.Panel11.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel11.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel11.Location = New System.Drawing.Point(0, 0)
        Me.Panel11.Margin = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.Panel11.Name = "Panel11"
        Me.Panel11.Padding = New System.Windows.Forms.Padding(0, 0, 3, 0)
        Me.Panel11.Size = New System.Drawing.Size(639, 394)
        Me.Panel11.TabIndex = 20
        '
        'GloUC_trvDrugs
        '
        Me.GloUC_trvDrugs.BackColor = System.Drawing.Color.Transparent
        Me.GloUC_trvDrugs.CheckBoxes = False
        Me.GloUC_trvDrugs.CodeMember = Nothing

        Me.GloUC_trvDrugs.DescriptionMember = Nothing
        Me.GloUC_trvDrugs.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Code_Description
        Me.GloUC_trvDrugs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GloUC_trvDrugs.DrugFlag = CType(16, Short)
        Me.GloUC_trvDrugs.DrugFormMember = Nothing
        Me.GloUC_trvDrugs.DrugQtyQualifierMember = Nothing
        Me.GloUC_trvDrugs.DurationMember = Nothing
        Me.GloUC_trvDrugs.FrequencyMember = Nothing
        Me.GloUC_trvDrugs.ImageIndex = 0
        Me.GloUC_trvDrugs.ImageList = Me.ImageList1
        Me.GloUC_trvDrugs.ImageObject = Nothing
        Me.GloUC_trvDrugs.IsDrug = False
        Me.GloUC_trvDrugs.IsNarcoticsMember = Nothing
        Me.GloUC_trvDrugs.Location = New System.Drawing.Point(0, 0)
        Me.GloUC_trvDrugs.MaximumNodes = 1000
        Me.GloUC_trvDrugs.Name = "GloUC_trvDrugs"
        Me.GloUC_trvDrugs.NDCCodeMember = Nothing
        Me.GloUC_trvDrugs.ParentImageIndex = 0
        Me.GloUC_trvDrugs.ParentMember = Nothing
        Me.GloUC_trvDrugs.RouteMember = Nothing
        Me.GloUC_trvDrugs.Search = gloUserControlLibrary.gloUC_TreeView.enumSearchType.Instring
        Me.GloUC_trvDrugs.SearchBox = True
        Me.GloUC_trvDrugs.SearchText = Nothing
        Me.GloUC_trvDrugs.SelectedImageIndex = 0
        Me.GloUC_trvDrugs.SelectedNode = Nothing
        Me.GloUC_trvDrugs.SelectedNodeIDs = CType(resources.GetObject("GloUC_trvDrugs.SelectedNodeIDs"), System.Collections.ArrayList)
        Me.GloUC_trvDrugs.SelectedParentImageIndex = 0
        Me.GloUC_trvDrugs.Size = New System.Drawing.Size(636, 394)
        Me.GloUC_trvDrugs.Sort = gloUserControlLibrary.gloUC_TreeView.enumSortType.ByDescription
        Me.GloUC_trvDrugs.TabIndex = 23
        Me.GloUC_trvDrugs.Tag = Nothing
        Me.GloUC_trvDrugs.UnitMember = Nothing
        Me.GloUC_trvDrugs.ValueMember = Nothing
        '
        'trvDrgs
        '
        Me.trvDrgs.BackColor = System.Drawing.Color.White
        Me.trvDrgs.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trvDrgs.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.trvDrgs.ForeColor = System.Drawing.Color.Black
        Me.trvDrgs.HideSelection = False
        Me.trvDrgs.ImageIndex = 0
        Me.trvDrgs.ImageList = Me.ImageList1
        Me.trvDrgs.ItemHeight = 18
        Me.trvDrgs.Location = New System.Drawing.Point(0, 1)
        Me.trvDrgs.Name = "trvDrgs"
        Me.trvDrgs.SelectedImageIndex = 0
        Me.trvDrgs.ShowLines = False
        Me.trvDrgs.Size = New System.Drawing.Size(635, 328)
        Me.trvDrgs.TabIndex = 0
        Me.trvDrgs.Visible = False
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Transparent
        Me.Panel4.Controls.Add(Me.txtDrugSearch)
        Me.Panel4.Controls.Add(Me.Label8)
        Me.Panel4.Controls.Add(Me.Label137)
        Me.Panel4.Controls.Add(Me.PictureBox4)
        Me.Panel4.Controls.Add(Me.Label138)
        Me.Panel4.Controls.Add(Me.Label139)
        Me.Panel4.Controls.Add(Me.Label140)
        Me.Panel4.Controls.Add(Me.Label141)
        Me.Panel4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel4.ForeColor = System.Drawing.Color.Black
        Me.Panel4.Location = New System.Drawing.Point(0, 0)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.Panel4.Size = New System.Drawing.Size(636, 26)
        Me.Panel4.TabIndex = 19
        Me.Panel4.Visible = False
        '
        'txtDrugSearch
        '
        Me.txtDrugSearch.BackColor = System.Drawing.Color.White
        Me.txtDrugSearch.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtDrugSearch.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtDrugSearch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDrugSearch.ForeColor = System.Drawing.Color.Black
        Me.txtDrugSearch.Location = New System.Drawing.Point(28, 5)
        Me.txtDrugSearch.Name = "txtDrugSearch"
        Me.txtDrugSearch.Size = New System.Drawing.Size(607, 15)
        Me.txtDrugSearch.TabIndex = 0
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.White
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label8.Location = New System.Drawing.Point(28, 1)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(607, 4)
        Me.Label8.TabIndex = 37
        '
        'Label137
        '
        Me.Label137.BackColor = System.Drawing.Color.White
        Me.Label137.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label137.Location = New System.Drawing.Point(28, 20)
        Me.Label137.Name = "Label137"
        Me.Label137.Size = New System.Drawing.Size(607, 2)
        Me.Label137.TabIndex = 38
        '
        'PictureBox4
        '
        Me.PictureBox4.BackColor = System.Drawing.Color.White
        Me.PictureBox4.Dock = System.Windows.Forms.DockStyle.Left
        Me.PictureBox4.Image = CType(resources.GetObject("PictureBox4.Image"), System.Drawing.Image)
        Me.PictureBox4.Location = New System.Drawing.Point(1, 1)
        Me.PictureBox4.Name = "PictureBox4"
        Me.PictureBox4.Size = New System.Drawing.Size(27, 21)
        Me.PictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox4.TabIndex = 9
        Me.PictureBox4.TabStop = False
        '
        'Label138
        '
        Me.Label138.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label138.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label138.Location = New System.Drawing.Point(1, 22)
        Me.Label138.Name = "Label138"
        Me.Label138.Size = New System.Drawing.Size(634, 1)
        Me.Label138.TabIndex = 35
        Me.Label138.Text = "label1"
        '
        'Label139
        '
        Me.Label139.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label139.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label139.Location = New System.Drawing.Point(1, 0)
        Me.Label139.Name = "Label139"
        Me.Label139.Size = New System.Drawing.Size(634, 1)
        Me.Label139.TabIndex = 36
        Me.Label139.Text = "label1"
        '
        'Label140
        '
        Me.Label140.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label140.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label140.Location = New System.Drawing.Point(0, 0)
        Me.Label140.Name = "Label140"
        Me.Label140.Size = New System.Drawing.Size(1, 23)
        Me.Label140.TabIndex = 39
        Me.Label140.Text = "label4"
        '
        'Label141
        '
        Me.Label141.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label141.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label141.Location = New System.Drawing.Point(635, 0)
        Me.Label141.Name = "Label141"
        Me.Label141.Size = New System.Drawing.Size(1, 23)
        Me.Label141.TabIndex = 40
        Me.Label141.Text = "label4"
        '
        'pnlRadiology
        '
        Me.pnlRadiology.Controls.Add(Me.Panel18)
        Me.pnlRadiology.Controls.Add(Me.Panel17)
        Me.pnlRadiology.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlRadiology.Location = New System.Drawing.Point(0, 0)
        Me.pnlRadiology.Name = "pnlRadiology"
        Me.pnlRadiology.Size = New System.Drawing.Size(639, 576)
        Me.pnlRadiology.TabIndex = 1
        '
        'Panel18
        '
        Me.Panel18.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Panel18.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel18.Controls.Add(Me.Label170)
        Me.Panel18.Controls.Add(Me.Label171)
        Me.Panel18.Controls.Add(Me.c1Labs)
        Me.Panel18.Controls.Add(Me.Label172)
        Me.Panel18.Controls.Add(Me.Label173)
        Me.Panel18.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel18.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel18.Location = New System.Drawing.Point(0, 26)
        Me.Panel18.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Panel18.Name = "Panel18"
        Me.Panel18.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.Panel18.Size = New System.Drawing.Size(639, 550)
        Me.Panel18.TabIndex = 20
        '
        'Label170
        '
        Me.Label170.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label170.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label170.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label170.Location = New System.Drawing.Point(1, 546)
        Me.Label170.Name = "Label170"
        Me.Label170.Size = New System.Drawing.Size(637, 1)
        Me.Label170.TabIndex = 8
        Me.Label170.Text = "label2"
        '
        'Label171
        '
        Me.Label171.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label171.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label171.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label171.Location = New System.Drawing.Point(0, 1)
        Me.Label171.Name = "Label171"
        Me.Label171.Size = New System.Drawing.Size(1, 546)
        Me.Label171.TabIndex = 7
        Me.Label171.Text = "label4"
        '
        'c1Labs
        '
        Me.c1Labs.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.c1Labs.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.c1Labs.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.c1Labs.ColumnInfo = "10,0,0,0,0,95,Columns:0{AllowDragging:False;}" & Global.Microsoft.VisualBasic.ChrW(9)
        Me.c1Labs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.c1Labs.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.c1Labs.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.c1Labs.Location = New System.Drawing.Point(0, 1)
        Me.c1Labs.Name = "c1Labs"
        Me.c1Labs.Rows.DefaultSize = 19
        Me.c1Labs.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.c1Labs.ShowCellLabels = True
        Me.c1Labs.Size = New System.Drawing.Size(638, 546)
        Me.c1Labs.StyleInfo = resources.GetString("c1Labs.StyleInfo")
        Me.c1Labs.TabIndex = 0
        Me.c1Labs.Tree.NodeImageCollapsed = CType(resources.GetObject("c1Labs.Tree.NodeImageCollapsed"), System.Drawing.Image)
        Me.c1Labs.Tree.NodeImageExpanded = CType(resources.GetObject("c1Labs.Tree.NodeImageExpanded"), System.Drawing.Image)
        '
        'Label172
        '
        Me.Label172.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label172.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label172.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label172.Location = New System.Drawing.Point(638, 1)
        Me.Label172.Name = "Label172"
        Me.Label172.Size = New System.Drawing.Size(1, 546)
        Me.Label172.TabIndex = 6
        Me.Label172.Text = "label3"
        '
        'Label173
        '
        Me.Label173.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label173.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label173.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label173.Location = New System.Drawing.Point(0, 0)
        Me.Label173.Name = "Label173"
        Me.Label173.Size = New System.Drawing.Size(639, 1)
        Me.Label173.TabIndex = 5
        Me.Label173.Text = "label1"
        '
        'Panel17
        '
        Me.Panel17.BackColor = System.Drawing.Color.Transparent
        Me.Panel17.Controls.Add(Me.txtLabsSearch)
        Me.Panel17.Controls.Add(Me.Label1)
        Me.Panel17.Controls.Add(Me.Label165)
        Me.Panel17.Controls.Add(Me.PictureBox6)
        Me.Panel17.Controls.Add(Me.Label166)
        Me.Panel17.Controls.Add(Me.Label167)
        Me.Panel17.Controls.Add(Me.Label168)
        Me.Panel17.Controls.Add(Me.Label169)
        Me.Panel17.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel17.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel17.ForeColor = System.Drawing.Color.Black
        Me.Panel17.Location = New System.Drawing.Point(0, 0)
        Me.Panel17.Name = "Panel17"
        Me.Panel17.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.Panel17.Size = New System.Drawing.Size(639, 26)
        Me.Panel17.TabIndex = 19
        '
        'txtLabsSearch
        '
        Me.txtLabsSearch.BackColor = System.Drawing.Color.White
        Me.txtLabsSearch.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtLabsSearch.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtLabsSearch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLabsSearch.ForeColor = System.Drawing.Color.Black
        Me.txtLabsSearch.Location = New System.Drawing.Point(29, 5)
        Me.txtLabsSearch.Name = "txtLabsSearch"
        Me.txtLabsSearch.Size = New System.Drawing.Size(609, 15)
        Me.txtLabsSearch.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.White
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label1.Location = New System.Drawing.Point(29, 1)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(609, 4)
        Me.Label1.TabIndex = 37
        '
        'Label165
        '
        Me.Label165.BackColor = System.Drawing.Color.White
        Me.Label165.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label165.Location = New System.Drawing.Point(29, 20)
        Me.Label165.Name = "Label165"
        Me.Label165.Size = New System.Drawing.Size(609, 2)
        Me.Label165.TabIndex = 38
        '
        'PictureBox6
        '
        Me.PictureBox6.BackColor = System.Drawing.Color.White
        Me.PictureBox6.Dock = System.Windows.Forms.DockStyle.Left
        Me.PictureBox6.Image = CType(resources.GetObject("PictureBox6.Image"), System.Drawing.Image)
        Me.PictureBox6.Location = New System.Drawing.Point(1, 1)
        Me.PictureBox6.Name = "PictureBox6"
        Me.PictureBox6.Size = New System.Drawing.Size(28, 21)
        Me.PictureBox6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox6.TabIndex = 9
        Me.PictureBox6.TabStop = False
        '
        'Label166
        '
        Me.Label166.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label166.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label166.Location = New System.Drawing.Point(1, 22)
        Me.Label166.Name = "Label166"
        Me.Label166.Size = New System.Drawing.Size(637, 1)
        Me.Label166.TabIndex = 35
        Me.Label166.Text = "label1"
        '
        'Label167
        '
        Me.Label167.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label167.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label167.Location = New System.Drawing.Point(1, 0)
        Me.Label167.Name = "Label167"
        Me.Label167.Size = New System.Drawing.Size(637, 1)
        Me.Label167.TabIndex = 36
        Me.Label167.Text = "label1"
        '
        'Label168
        '
        Me.Label168.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label168.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label168.Location = New System.Drawing.Point(0, 0)
        Me.Label168.Name = "Label168"
        Me.Label168.Size = New System.Drawing.Size(1, 23)
        Me.Label168.TabIndex = 39
        Me.Label168.Text = "label4"
        '
        'Label169
        '
        Me.Label169.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label169.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label169.Location = New System.Drawing.Point(638, 0)
        Me.Label169.Name = "Label169"
        Me.Label169.Size = New System.Drawing.Size(1, 23)
        Me.Label169.TabIndex = 40
        Me.Label169.Text = "label4"
        '
        'pnlSummaryOthers
        '
        Me.pnlSummaryOthers.Controls.Add(Me.pnlSummary)
        Me.pnlSummaryOthers.Controls.Add(Me.pnlSummaryHeader)
        Me.pnlSummaryOthers.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlSummaryOthers.Location = New System.Drawing.Point(0, 0)
        Me.pnlSummaryOthers.Name = "pnlSummaryOthers"
        Me.pnlSummaryOthers.Size = New System.Drawing.Size(639, 576)
        Me.pnlSummaryOthers.TabIndex = 2
        Me.pnlSummaryOthers.Visible = False
        '
        'pnlSummary
        '
        Me.pnlSummary.Controls.Add(Me.txt_summary)
        Me.pnlSummary.Controls.Add(Me.Label6)
        Me.pnlSummary.Controls.Add(Me.Label91)
        Me.pnlSummary.Controls.Add(Me.Label92)
        Me.pnlSummary.Controls.Add(Me.Label93)
        Me.pnlSummary.Controls.Add(Me.Label94)
        Me.pnlSummary.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlSummary.Location = New System.Drawing.Point(0, 27)
        Me.pnlSummary.Name = "pnlSummary"
        Me.pnlSummary.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.pnlSummary.Size = New System.Drawing.Size(639, 549)
        Me.pnlSummary.TabIndex = 1
        '
        'txt_summary
        '
        Me.txt_summary.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txt_summary.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txt_summary.ForeColor = System.Drawing.Color.Black
        Me.txt_summary.Location = New System.Drawing.Point(7, 1)
        Me.txt_summary.Multiline = True
        Me.txt_summary.Name = "txt_summary"
        Me.txt_summary.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txt_summary.Size = New System.Drawing.Size(631, 544)
        Me.txt_summary.TabIndex = 1
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.White
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(1, 1)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(6, 544)
        Me.Label6.TabIndex = 13
        '
        'Label91
        '
        Me.Label91.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label91.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label91.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label91.Location = New System.Drawing.Point(1, 545)
        Me.Label91.Name = "Label91"
        Me.Label91.Size = New System.Drawing.Size(637, 1)
        Me.Label91.TabIndex = 12
        Me.Label91.Text = "label2"
        '
        'Label92
        '
        Me.Label92.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label92.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label92.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label92.Location = New System.Drawing.Point(0, 1)
        Me.Label92.Name = "Label92"
        Me.Label92.Size = New System.Drawing.Size(1, 545)
        Me.Label92.TabIndex = 11
        '
        'Label93
        '
        Me.Label93.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label93.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label93.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label93.Location = New System.Drawing.Point(638, 1)
        Me.Label93.Name = "Label93"
        Me.Label93.Size = New System.Drawing.Size(1, 545)
        Me.Label93.TabIndex = 10
        Me.Label93.Text = "label3"
        '
        'Label94
        '
        Me.Label94.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label94.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label94.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label94.Location = New System.Drawing.Point(0, 0)
        Me.Label94.Name = "Label94"
        Me.Label94.Size = New System.Drawing.Size(639, 1)
        Me.Label94.TabIndex = 9
        Me.Label94.Text = "label1"
        '
        'pnlSummaryHeader
        '
        Me.pnlSummaryHeader.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlSummaryHeader.Controls.Add(Me.Panel1)
        Me.pnlSummaryHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlSummaryHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlSummaryHeader.Name = "pnlSummaryHeader"
        Me.pnlSummaryHeader.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.pnlSummaryHeader.Size = New System.Drawing.Size(639, 27)
        Me.pnlSummaryHeader.TabIndex = 0
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Transparent
        Me.Panel1.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Blue2007
        Me.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel1.Controls.Add(Me.Label26)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.Label64)
        Me.Panel1.Controls.Add(Me.Label87)
        Me.Panel1.Controls.Add(Me.Label88)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(639, 24)
        Me.Panel1.TabIndex = 20
        '
        'Label26
        '
        Me.Label26.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label26.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label26.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label26.Location = New System.Drawing.Point(1, 23)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(637, 1)
        Me.Label26.TabIndex = 8
        Me.Label26.Text = "label2"
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.White
        Me.Label5.Image = CType(resources.GetObject("Label5.Image"), System.Drawing.Image)
        Me.Label5.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label5.Location = New System.Drawing.Point(1, 1)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(637, 23)
        Me.Label5.TabIndex = 0
        Me.Label5.Text = "     Summary"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label64
        '
        Me.Label64.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label64.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label64.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label64.Location = New System.Drawing.Point(0, 1)
        Me.Label64.Name = "Label64"
        Me.Label64.Size = New System.Drawing.Size(1, 23)
        Me.Label64.TabIndex = 7
        Me.Label64.Text = "label4"
        '
        'Label87
        '
        Me.Label87.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label87.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label87.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label87.Location = New System.Drawing.Point(638, 1)
        Me.Label87.Name = "Label87"
        Me.Label87.Size = New System.Drawing.Size(1, 23)
        Me.Label87.TabIndex = 6
        Me.Label87.Text = "label3"
        '
        'Label88
        '
        Me.Label88.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label88.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label88.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label88.Location = New System.Drawing.Point(0, 0)
        Me.Label88.Name = "Label88"
        Me.Label88.Size = New System.Drawing.Size(639, 1)
        Me.Label88.TabIndex = 5
        Me.Label88.Text = "label1"
        '
        'pnlCPT
        '
        Me.pnlCPT.Controls.Add(Me.Panel15)
        Me.pnlCPT.Controls.Add(Me.Panel16)
        Me.pnlCPT.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlCPT.Location = New System.Drawing.Point(0, 0)
        Me.pnlCPT.Name = "pnlCPT"
        Me.pnlCPT.Size = New System.Drawing.Size(639, 576)
        Me.pnlCPT.TabIndex = 1
        Me.pnlCPT.Visible = False
        '
        'Panel15
        '
        Me.Panel15.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Panel15.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel15.Controls.Add(Me.Label154)
        Me.Panel15.Controls.Add(Me.Label156)
        Me.Panel15.Controls.Add(Me.trvCPT)
        Me.Panel15.Controls.Add(Me.Label157)
        Me.Panel15.Controls.Add(Me.Label158)
        Me.Panel15.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel15.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel15.Location = New System.Drawing.Point(0, 26)
        Me.Panel15.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Panel15.Name = "Panel15"
        Me.Panel15.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.Panel15.Size = New System.Drawing.Size(639, 550)
        Me.Panel15.TabIndex = 20
        '
        'Label154
        '
        Me.Label154.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label154.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label154.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label154.Location = New System.Drawing.Point(1, 546)
        Me.Label154.Name = "Label154"
        Me.Label154.Size = New System.Drawing.Size(637, 1)
        Me.Label154.TabIndex = 8
        Me.Label154.Text = "label2"
        '
        'Label156
        '
        Me.Label156.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label156.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label156.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label156.Location = New System.Drawing.Point(0, 1)
        Me.Label156.Name = "Label156"
        Me.Label156.Size = New System.Drawing.Size(1, 546)
        Me.Label156.TabIndex = 7
        Me.Label156.Text = "label4"
        '
        'trvCPT
        '
        Me.trvCPT.BackColor = System.Drawing.Color.White
        Me.trvCPT.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trvCPT.CheckBoxes = True
        Me.trvCPT.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trvCPT.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.trvCPT.ForeColor = System.Drawing.Color.Black
        Me.trvCPT.HideSelection = False
        Me.trvCPT.ItemHeight = 18
        Me.trvCPT.Location = New System.Drawing.Point(0, 1)
        Me.trvCPT.Name = "trvCPT"
        Me.trvCPT.ShowLines = False
        Me.trvCPT.Size = New System.Drawing.Size(638, 546)
        Me.trvCPT.TabIndex = 0
        '
        'Label157
        '
        Me.Label157.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label157.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label157.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label157.Location = New System.Drawing.Point(638, 1)
        Me.Label157.Name = "Label157"
        Me.Label157.Size = New System.Drawing.Size(1, 546)
        Me.Label157.TabIndex = 6
        Me.Label157.Text = "label3"
        '
        'Label158
        '
        Me.Label158.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label158.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label158.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label158.Location = New System.Drawing.Point(0, 0)
        Me.Label158.Name = "Label158"
        Me.Label158.Size = New System.Drawing.Size(639, 1)
        Me.Label158.TabIndex = 5
        Me.Label158.Text = "label1"
        '
        'Panel16
        '
        Me.Panel16.BackColor = System.Drawing.Color.Transparent
        Me.Panel16.Controls.Add(Me.txtCPTsearch)
        Me.Panel16.Controls.Add(Me.Label159)
        Me.Panel16.Controls.Add(Me.Label160)
        Me.Panel16.Controls.Add(Me.PictureBox5)
        Me.Panel16.Controls.Add(Me.Label161)
        Me.Panel16.Controls.Add(Me.Label162)
        Me.Panel16.Controls.Add(Me.Label163)
        Me.Panel16.Controls.Add(Me.Label164)
        Me.Panel16.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel16.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel16.ForeColor = System.Drawing.Color.Black
        Me.Panel16.Location = New System.Drawing.Point(0, 0)
        Me.Panel16.Name = "Panel16"
        Me.Panel16.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.Panel16.Size = New System.Drawing.Size(639, 26)
        Me.Panel16.TabIndex = 21
        '
        'txtCPTsearch
        '
        Me.txtCPTsearch.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtCPTsearch.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtCPTsearch.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCPTsearch.Location = New System.Drawing.Point(28, 5)
        Me.txtCPTsearch.Name = "txtCPTsearch"
        Me.txtCPTsearch.Size = New System.Drawing.Size(610, 15)
        Me.txtCPTsearch.TabIndex = 0
        '
        'Label159
        '
        Me.Label159.BackColor = System.Drawing.Color.White
        Me.Label159.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label159.Location = New System.Drawing.Point(28, 1)
        Me.Label159.Name = "Label159"
        Me.Label159.Size = New System.Drawing.Size(610, 4)
        Me.Label159.TabIndex = 37
        '
        'Label160
        '
        Me.Label160.BackColor = System.Drawing.Color.White
        Me.Label160.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label160.Location = New System.Drawing.Point(28, 20)
        Me.Label160.Name = "Label160"
        Me.Label160.Size = New System.Drawing.Size(610, 2)
        Me.Label160.TabIndex = 38
        '
        'PictureBox5
        '
        Me.PictureBox5.BackColor = System.Drawing.Color.White
        Me.PictureBox5.Dock = System.Windows.Forms.DockStyle.Left
        Me.PictureBox5.Image = CType(resources.GetObject("PictureBox5.Image"), System.Drawing.Image)
        Me.PictureBox5.Location = New System.Drawing.Point(1, 1)
        Me.PictureBox5.Name = "PictureBox5"
        Me.PictureBox5.Size = New System.Drawing.Size(27, 21)
        Me.PictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox5.TabIndex = 9
        Me.PictureBox5.TabStop = False
        '
        'Label161
        '
        Me.Label161.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label161.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label161.Location = New System.Drawing.Point(1, 22)
        Me.Label161.Name = "Label161"
        Me.Label161.Size = New System.Drawing.Size(637, 1)
        Me.Label161.TabIndex = 35
        Me.Label161.Text = "label1"
        '
        'Label162
        '
        Me.Label162.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label162.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label162.Location = New System.Drawing.Point(1, 0)
        Me.Label162.Name = "Label162"
        Me.Label162.Size = New System.Drawing.Size(637, 1)
        Me.Label162.TabIndex = 36
        Me.Label162.Text = "label1"
        '
        'Label163
        '
        Me.Label163.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label163.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label163.Location = New System.Drawing.Point(0, 0)
        Me.Label163.Name = "Label163"
        Me.Label163.Size = New System.Drawing.Size(1, 23)
        Me.Label163.TabIndex = 39
        Me.Label163.Text = "label4"
        '
        'Label164
        '
        Me.Label164.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label164.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label164.Location = New System.Drawing.Point(638, 0)
        Me.Label164.Name = "Label164"
        Me.Label164.Size = New System.Drawing.Size(1, 23)
        Me.Label164.TabIndex = 40
        Me.Label164.Text = "label4"
        '
        'pnlICD9
        '
        Me.pnlICD9.Controls.Add(Me.Panel5)
        Me.pnlICD9.Controls.Add(Me.Panel10)
        Me.pnlICD9.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlICD9.Location = New System.Drawing.Point(0, 0)
        Me.pnlICD9.Name = "pnlICD9"
        Me.pnlICD9.Size = New System.Drawing.Size(639, 576)
        Me.pnlICD9.TabIndex = 1
        Me.pnlICD9.Visible = False
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.trvICD9)
        Me.Panel5.Controls.Add(Me.Label179)
        Me.Panel5.Controls.Add(Me.Label178)
        Me.Panel5.Controls.Add(Me.Label13)
        Me.Panel5.Controls.Add(Me.Label128)
        Me.Panel5.Controls.Add(Me.Label129)
        Me.Panel5.Controls.Add(Me.Label130)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel5.Location = New System.Drawing.Point(0, 26)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.Panel5.Size = New System.Drawing.Size(639, 550)
        Me.Panel5.TabIndex = 1
        '
        'trvICD9
        '
        Me.trvICD9.BackColor = System.Drawing.Color.White
        Me.trvICD9.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trvICD9.CheckBoxes = True
        Me.trvICD9.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trvICD9.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.trvICD9.ForeColor = System.Drawing.Color.Black
        Me.trvICD9.HideSelection = False
        Me.trvICD9.ItemHeight = 18
        Me.trvICD9.Location = New System.Drawing.Point(4, 4)
        Me.trvICD9.Name = "trvICD9"
        Me.trvICD9.ShowLines = False
        Me.trvICD9.ShowRootLines = False
        Me.trvICD9.Size = New System.Drawing.Size(634, 542)
        Me.trvICD9.TabIndex = 0
        '
        'Label179
        '
        Me.Label179.BackColor = System.Drawing.Color.White
        Me.Label179.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label179.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label179.Location = New System.Drawing.Point(1, 4)
        Me.Label179.Name = "Label179"
        Me.Label179.Size = New System.Drawing.Size(3, 542)
        Me.Label179.TabIndex = 14
        Me.Label179.Text = "label4"
        '
        'Label178
        '
        Me.Label178.BackColor = System.Drawing.Color.White
        Me.Label178.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label178.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label178.Location = New System.Drawing.Point(1, 1)
        Me.Label178.Name = "Label178"
        Me.Label178.Size = New System.Drawing.Size(637, 3)
        Me.Label178.TabIndex = 13
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label13.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label13.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label13.Location = New System.Drawing.Point(1, 546)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(637, 1)
        Me.Label13.TabIndex = 12
        Me.Label13.Text = "label2"
        '
        'Label128
        '
        Me.Label128.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label128.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label128.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label128.Location = New System.Drawing.Point(0, 1)
        Me.Label128.Name = "Label128"
        Me.Label128.Size = New System.Drawing.Size(1, 546)
        Me.Label128.TabIndex = 11
        Me.Label128.Text = "label4"
        '
        'Label129
        '
        Me.Label129.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label129.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label129.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label129.Location = New System.Drawing.Point(638, 1)
        Me.Label129.Name = "Label129"
        Me.Label129.Size = New System.Drawing.Size(1, 546)
        Me.Label129.TabIndex = 10
        Me.Label129.Text = "label3"
        '
        'Label130
        '
        Me.Label130.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label130.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label130.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label130.Location = New System.Drawing.Point(0, 0)
        Me.Label130.Name = "Label130"
        Me.Label130.Size = New System.Drawing.Size(639, 1)
        Me.Label130.TabIndex = 9
        Me.Label130.Text = "label1"
        '
        'Panel10
        '
        Me.Panel10.BackColor = System.Drawing.Color.Transparent
        Me.Panel10.Controls.Add(Me.txtICD9Search)
        Me.Panel10.Controls.Add(Me.Label131)
        Me.Panel10.Controls.Add(Me.Label132)
        Me.Panel10.Controls.Add(Me.PictureBox2)
        Me.Panel10.Controls.Add(Me.Label133)
        Me.Panel10.Controls.Add(Me.Label134)
        Me.Panel10.Controls.Add(Me.Label135)
        Me.Panel10.Controls.Add(Me.Label136)
        Me.Panel10.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel10.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel10.ForeColor = System.Drawing.Color.Black
        Me.Panel10.Location = New System.Drawing.Point(0, 0)
        Me.Panel10.Name = "Panel10"
        Me.Panel10.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.Panel10.Size = New System.Drawing.Size(639, 26)
        Me.Panel10.TabIndex = 19
        '
        'txtICD9Search
        '
        Me.txtICD9Search.BackColor = System.Drawing.Color.White
        Me.txtICD9Search.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtICD9Search.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtICD9Search.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtICD9Search.ForeColor = System.Drawing.Color.Black
        Me.txtICD9Search.Location = New System.Drawing.Point(28, 5)
        Me.txtICD9Search.Name = "txtICD9Search"
        Me.txtICD9Search.Size = New System.Drawing.Size(610, 15)
        Me.txtICD9Search.TabIndex = 0
        '
        'Label131
        '
        Me.Label131.BackColor = System.Drawing.Color.White
        Me.Label131.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label131.Location = New System.Drawing.Point(28, 1)
        Me.Label131.Name = "Label131"
        Me.Label131.Size = New System.Drawing.Size(610, 4)
        Me.Label131.TabIndex = 37
        '
        'Label132
        '
        Me.Label132.BackColor = System.Drawing.Color.White
        Me.Label132.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label132.Location = New System.Drawing.Point(28, 20)
        Me.Label132.Name = "Label132"
        Me.Label132.Size = New System.Drawing.Size(610, 2)
        Me.Label132.TabIndex = 38
        '
        'PictureBox2
        '
        Me.PictureBox2.BackColor = System.Drawing.Color.White
        Me.PictureBox2.Dock = System.Windows.Forms.DockStyle.Left
        Me.PictureBox2.Image = CType(resources.GetObject("PictureBox2.Image"), System.Drawing.Image)
        Me.PictureBox2.Location = New System.Drawing.Point(1, 1)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(27, 21)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox2.TabIndex = 9
        Me.PictureBox2.TabStop = False
        '
        'Label133
        '
        Me.Label133.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label133.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label133.Location = New System.Drawing.Point(1, 22)
        Me.Label133.Name = "Label133"
        Me.Label133.Size = New System.Drawing.Size(637, 1)
        Me.Label133.TabIndex = 35
        Me.Label133.Text = "label1"
        '
        'Label134
        '
        Me.Label134.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label134.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label134.Location = New System.Drawing.Point(1, 0)
        Me.Label134.Name = "Label134"
        Me.Label134.Size = New System.Drawing.Size(637, 1)
        Me.Label134.TabIndex = 36
        Me.Label134.Text = "label1"
        '
        'Label135
        '
        Me.Label135.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label135.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label135.Location = New System.Drawing.Point(0, 0)
        Me.Label135.Name = "Label135"
        Me.Label135.Size = New System.Drawing.Size(1, 23)
        Me.Label135.TabIndex = 39
        Me.Label135.Text = "label4"
        '
        'Label136
        '
        Me.Label136.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label136.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label136.Location = New System.Drawing.Point(638, 0)
        Me.Label136.Name = "Label136"
        Me.Label136.Size = New System.Drawing.Size(1, 23)
        Me.Label136.TabIndex = 40
        Me.Label136.Text = "label4"
        '
        'sptLeft
        '
        Me.sptLeft.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.sptLeft.Location = New System.Drawing.Point(202, 30)
        Me.sptLeft.Name = "sptLeft"
        Me.sptLeft.Size = New System.Drawing.Size(3, 576)
        Me.sptLeft.TabIndex = 4
        Me.sptLeft.TabStop = False
        '
        'pnlLeft
        '
        Me.pnlLeft.BackColor = System.Drawing.Color.Transparent
        Me.pnlLeft.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlLeft.Controls.Add(Me.Panel19)
        Me.pnlLeft.Controls.Add(Me.pnlbtnOrders)
        Me.pnlLeft.Controls.Add(Me.pnlbtnRadiology)
        Me.pnlLeft.Controls.Add(Me.pnlbtnLabs)
        Me.pnlLeft.Controls.Add(Me.pnlbtnCPT)
        Me.pnlLeft.Controls.Add(Me.pnlbtnICD9)
        Me.pnlLeft.Controls.Add(Me.pnlbtnDrugs)
        Me.pnlLeft.Controls.Add(Me.pnlbtnHistory)
        Me.pnlLeft.Controls.Add(Me.pnbtnDemographics)
        Me.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlLeft.Location = New System.Drawing.Point(0, 30)
        Me.pnlLeft.Name = "pnlLeft"
        Me.pnlLeft.Size = New System.Drawing.Size(202, 576)
        Me.pnlLeft.TabIndex = 2
        '
        'Panel19
        '
        Me.Panel19.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Panel19.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel19.Controls.Add(Me.Label174)
        Me.Panel19.Controls.Add(Me.Label175)
        Me.Panel19.Controls.Add(Me.Label176)
        Me.Panel19.Controls.Add(Me.Label177)
        Me.Panel19.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel19.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel19.Location = New System.Drawing.Point(0, 240)
        Me.Panel19.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Panel19.Name = "Panel19"
        Me.Panel19.Padding = New System.Windows.Forms.Padding(3, 0, 0, 3)
        Me.Panel19.Size = New System.Drawing.Size(202, 336)
        Me.Panel19.TabIndex = 33
        '
        'Label174
        '
        Me.Label174.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label174.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label174.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label174.Location = New System.Drawing.Point(4, 332)
        Me.Label174.Name = "Label174"
        Me.Label174.Size = New System.Drawing.Size(197, 1)
        Me.Label174.TabIndex = 8
        Me.Label174.Text = "label2"
        '
        'Label175
        '
        Me.Label175.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label175.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label175.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label175.Location = New System.Drawing.Point(3, 1)
        Me.Label175.Name = "Label175"
        Me.Label175.Size = New System.Drawing.Size(1, 332)
        Me.Label175.TabIndex = 7
        Me.Label175.Text = "label4"
        '
        'Label176
        '
        Me.Label176.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label176.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label176.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label176.Location = New System.Drawing.Point(201, 1)
        Me.Label176.Name = "Label176"
        Me.Label176.Size = New System.Drawing.Size(1, 332)
        Me.Label176.TabIndex = 6
        Me.Label176.Text = "label3"
        '
        'Label177
        '
        Me.Label177.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label177.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label177.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label177.Location = New System.Drawing.Point(3, 0)
        Me.Label177.Name = "Label177"
        Me.Label177.Size = New System.Drawing.Size(199, 1)
        Me.Label177.TabIndex = 5
        Me.Label177.Text = "label1"
        '
        'pnlbtnOrders
        '
        Me.pnlbtnOrders.BackColor = System.Drawing.Color.Transparent
        Me.pnlbtnOrders.Controls.Add(Me.btnOrders)
        Me.pnlbtnOrders.Controls.Add(Me.Label51)
        Me.pnlbtnOrders.Controls.Add(Me.Label52)
        Me.pnlbtnOrders.Controls.Add(Me.Label53)
        Me.pnlbtnOrders.Controls.Add(Me.Label54)
        Me.pnlbtnOrders.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlbtnOrders.Location = New System.Drawing.Point(0, 210)
        Me.pnlbtnOrders.Name = "pnlbtnOrders"
        Me.pnlbtnOrders.Padding = New System.Windows.Forms.Padding(3, 0, 0, 3)
        Me.pnlbtnOrders.Size = New System.Drawing.Size(202, 30)
        Me.pnlbtnOrders.TabIndex = 32
        '
        'btnOrders
        '
        Me.btnOrders.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.btnOrders.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
        Me.btnOrders.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnOrders.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnOrders.FlatAppearance.BorderSize = 0
        Me.btnOrders.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnOrders.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnOrders.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnOrders.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnOrders.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnOrders.Image = CType(resources.GetObject("btnOrders.Image"), System.Drawing.Image)
        Me.btnOrders.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnOrders.Location = New System.Drawing.Point(4, 1)
        Me.btnOrders.Name = "btnOrders"
        Me.btnOrders.Size = New System.Drawing.Size(197, 25)
        Me.btnOrders.TabIndex = 0
        Me.btnOrders.Tag = "UnSelected"
        Me.btnOrders.Text = "      Summary"
        Me.btnOrders.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnOrders.UseVisualStyleBackColor = False
        '
        'Label51
        '
        Me.Label51.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label51.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label51.Location = New System.Drawing.Point(3, 1)
        Me.Label51.Name = "Label51"
        Me.Label51.Size = New System.Drawing.Size(1, 25)
        Me.Label51.TabIndex = 14
        '
        'Label52
        '
        Me.Label52.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label52.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label52.Location = New System.Drawing.Point(201, 1)
        Me.Label52.Name = "Label52"
        Me.Label52.Size = New System.Drawing.Size(1, 25)
        Me.Label52.TabIndex = 13
        '
        'Label53
        '
        Me.Label53.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label53.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label53.Location = New System.Drawing.Point(3, 26)
        Me.Label53.Name = "Label53"
        Me.Label53.Size = New System.Drawing.Size(199, 1)
        Me.Label53.TabIndex = 12
        '
        'Label54
        '
        Me.Label54.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label54.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label54.Location = New System.Drawing.Point(3, 0)
        Me.Label54.Name = "Label54"
        Me.Label54.Size = New System.Drawing.Size(199, 1)
        Me.Label54.TabIndex = 11
        '
        'pnlbtnRadiology
        '
        Me.pnlbtnRadiology.BackColor = System.Drawing.Color.Transparent
        Me.pnlbtnRadiology.Controls.Add(Me.btnRadiology)
        Me.pnlbtnRadiology.Controls.Add(Me.Label47)
        Me.pnlbtnRadiology.Controls.Add(Me.Label48)
        Me.pnlbtnRadiology.Controls.Add(Me.Label49)
        Me.pnlbtnRadiology.Controls.Add(Me.Label50)
        Me.pnlbtnRadiology.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlbtnRadiology.Location = New System.Drawing.Point(0, 180)
        Me.pnlbtnRadiology.Name = "pnlbtnRadiology"
        Me.pnlbtnRadiology.Padding = New System.Windows.Forms.Padding(3, 0, 0, 3)
        Me.pnlbtnRadiology.Size = New System.Drawing.Size(202, 30)
        Me.pnlbtnRadiology.TabIndex = 31
        '
        'btnRadiology
        '
        Me.btnRadiology.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.btnRadiology.BackgroundImage = CType(resources.GetObject("btnRadiology.BackgroundImage"), System.Drawing.Image)
        Me.btnRadiology.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnRadiology.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnRadiology.FlatAppearance.BorderSize = 0
        Me.btnRadiology.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnRadiology.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnRadiology.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRadiology.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRadiology.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnRadiology.Image = CType(resources.GetObject("btnRadiology.Image"), System.Drawing.Image)
        Me.btnRadiology.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnRadiology.Location = New System.Drawing.Point(4, 1)
        Me.btnRadiology.Name = "btnRadiology"
        Me.btnRadiology.Size = New System.Drawing.Size(197, 25)
        Me.btnRadiology.TabIndex = 0
        Me.btnRadiology.Tag = "UnSelected"
        Me.btnRadiology.Text = "      Orders"
        Me.btnRadiology.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnRadiology.UseVisualStyleBackColor = False
        '
        'Label47
        '
        Me.Label47.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label47.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label47.Location = New System.Drawing.Point(3, 1)
        Me.Label47.Name = "Label47"
        Me.Label47.Size = New System.Drawing.Size(1, 25)
        Me.Label47.TabIndex = 14
        '
        'Label48
        '
        Me.Label48.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label48.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label48.Location = New System.Drawing.Point(201, 1)
        Me.Label48.Name = "Label48"
        Me.Label48.Size = New System.Drawing.Size(1, 25)
        Me.Label48.TabIndex = 13
        '
        'Label49
        '
        Me.Label49.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label49.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label49.Location = New System.Drawing.Point(3, 26)
        Me.Label49.Name = "Label49"
        Me.Label49.Size = New System.Drawing.Size(199, 1)
        Me.Label49.TabIndex = 12
        '
        'Label50
        '
        Me.Label50.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label50.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label50.Location = New System.Drawing.Point(3, 0)
        Me.Label50.Name = "Label50"
        Me.Label50.Size = New System.Drawing.Size(199, 1)
        Me.Label50.TabIndex = 11
        '
        'pnlbtnLabs
        '
        Me.pnlbtnLabs.BackColor = System.Drawing.Color.Transparent
        Me.pnlbtnLabs.Controls.Add(Me.btnLabs)
        Me.pnlbtnLabs.Controls.Add(Me.Label43)
        Me.pnlbtnLabs.Controls.Add(Me.Label44)
        Me.pnlbtnLabs.Controls.Add(Me.Label45)
        Me.pnlbtnLabs.Controls.Add(Me.Label46)
        Me.pnlbtnLabs.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlbtnLabs.Location = New System.Drawing.Point(0, 150)
        Me.pnlbtnLabs.Name = "pnlbtnLabs"
        Me.pnlbtnLabs.Padding = New System.Windows.Forms.Padding(3, 0, 0, 3)
        Me.pnlbtnLabs.Size = New System.Drawing.Size(202, 30)
        Me.pnlbtnLabs.TabIndex = 30
        '
        'btnLabs
        '
        Me.btnLabs.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.btnLabs.BackgroundImage = CType(resources.GetObject("btnLabs.BackgroundImage"), System.Drawing.Image)
        Me.btnLabs.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnLabs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnLabs.FlatAppearance.BorderSize = 0
        Me.btnLabs.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnLabs.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnLabs.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnLabs.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnLabs.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnLabs.Image = CType(resources.GetObject("btnLabs.Image"), System.Drawing.Image)
        Me.btnLabs.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnLabs.Location = New System.Drawing.Point(4, 1)
        Me.btnLabs.Name = "btnLabs"
        Me.btnLabs.Size = New System.Drawing.Size(197, 25)
        Me.btnLabs.TabIndex = 0
        Me.btnLabs.Tag = "UnSelected"
        Me.btnLabs.Text = "      Lab"
        Me.btnLabs.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnLabs.UseVisualStyleBackColor = False
        '
        'Label43
        '
        Me.Label43.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label43.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label43.Location = New System.Drawing.Point(3, 1)
        Me.Label43.Name = "Label43"
        Me.Label43.Size = New System.Drawing.Size(1, 25)
        Me.Label43.TabIndex = 14
        '
        'Label44
        '
        Me.Label44.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label44.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label44.Location = New System.Drawing.Point(201, 1)
        Me.Label44.Name = "Label44"
        Me.Label44.Size = New System.Drawing.Size(1, 25)
        Me.Label44.TabIndex = 13
        '
        'Label45
        '
        Me.Label45.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label45.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label45.Location = New System.Drawing.Point(3, 26)
        Me.Label45.Name = "Label45"
        Me.Label45.Size = New System.Drawing.Size(199, 1)
        Me.Label45.TabIndex = 12
        '
        'Label46
        '
        Me.Label46.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label46.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label46.Location = New System.Drawing.Point(3, 0)
        Me.Label46.Name = "Label46"
        Me.Label46.Size = New System.Drawing.Size(199, 1)
        Me.Label46.TabIndex = 11
        '
        'pnlbtnCPT
        '
        Me.pnlbtnCPT.BackColor = System.Drawing.Color.Transparent
        Me.pnlbtnCPT.Controls.Add(Me.btnCPT)
        Me.pnlbtnCPT.Controls.Add(Me.Label39)
        Me.pnlbtnCPT.Controls.Add(Me.Label40)
        Me.pnlbtnCPT.Controls.Add(Me.Label41)
        Me.pnlbtnCPT.Controls.Add(Me.Label42)
        Me.pnlbtnCPT.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlbtnCPT.Location = New System.Drawing.Point(0, 120)
        Me.pnlbtnCPT.Name = "pnlbtnCPT"
        Me.pnlbtnCPT.Padding = New System.Windows.Forms.Padding(3, 0, 0, 3)
        Me.pnlbtnCPT.Size = New System.Drawing.Size(202, 30)
        Me.pnlbtnCPT.TabIndex = 29
        Me.pnlbtnCPT.Visible = False
        '
        'btnCPT
        '
        Me.btnCPT.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.btnCPT.BackgroundImage = CType(resources.GetObject("btnCPT.BackgroundImage"), System.Drawing.Image)
        Me.btnCPT.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnCPT.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnCPT.FlatAppearance.BorderSize = 0
        Me.btnCPT.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnCPT.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnCPT.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCPT.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCPT.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnCPT.Image = CType(resources.GetObject("btnCPT.Image"), System.Drawing.Image)
        Me.btnCPT.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnCPT.Location = New System.Drawing.Point(4, 1)
        Me.btnCPT.Name = "btnCPT"
        Me.btnCPT.Size = New System.Drawing.Size(197, 25)
        Me.btnCPT.TabIndex = 0
        Me.btnCPT.Tag = "UnSelected"
        Me.btnCPT.Text = "      CPT"
        Me.btnCPT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnCPT.UseVisualStyleBackColor = False
        '
        'Label39
        '
        Me.Label39.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label39.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label39.Location = New System.Drawing.Point(3, 1)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(1, 25)
        Me.Label39.TabIndex = 14
        '
        'Label40
        '
        Me.Label40.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label40.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label40.Location = New System.Drawing.Point(201, 1)
        Me.Label40.Name = "Label40"
        Me.Label40.Size = New System.Drawing.Size(1, 25)
        Me.Label40.TabIndex = 13
        '
        'Label41
        '
        Me.Label41.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label41.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label41.Location = New System.Drawing.Point(3, 26)
        Me.Label41.Name = "Label41"
        Me.Label41.Size = New System.Drawing.Size(199, 1)
        Me.Label41.TabIndex = 12
        '
        'Label42
        '
        Me.Label42.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label42.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label42.Location = New System.Drawing.Point(3, 0)
        Me.Label42.Name = "Label42"
        Me.Label42.Size = New System.Drawing.Size(199, 1)
        Me.Label42.TabIndex = 11
        '
        'pnlbtnICD9
        '
        Me.pnlbtnICD9.BackColor = System.Drawing.Color.Transparent
        Me.pnlbtnICD9.Controls.Add(Me.btnICD9)
        Me.pnlbtnICD9.Controls.Add(Me.Label35)
        Me.pnlbtnICD9.Controls.Add(Me.Label36)
        Me.pnlbtnICD9.Controls.Add(Me.Label37)
        Me.pnlbtnICD9.Controls.Add(Me.Label38)
        Me.pnlbtnICD9.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlbtnICD9.Location = New System.Drawing.Point(0, 90)
        Me.pnlbtnICD9.Name = "pnlbtnICD9"
        Me.pnlbtnICD9.Padding = New System.Windows.Forms.Padding(3, 0, 0, 3)
        Me.pnlbtnICD9.Size = New System.Drawing.Size(202, 30)
        Me.pnlbtnICD9.TabIndex = 28
        Me.pnlbtnICD9.Visible = False
        '
        'btnICD9
        '
        Me.btnICD9.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.btnICD9.BackgroundImage = CType(resources.GetObject("btnICD9.BackgroundImage"), System.Drawing.Image)
        Me.btnICD9.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnICD9.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnICD9.FlatAppearance.BorderSize = 0
        Me.btnICD9.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnICD9.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnICD9.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnICD9.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnICD9.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnICD9.Image = CType(resources.GetObject("btnICD9.Image"), System.Drawing.Image)
        Me.btnICD9.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnICD9.Location = New System.Drawing.Point(4, 1)
        Me.btnICD9.Name = "btnICD9"
        Me.btnICD9.Size = New System.Drawing.Size(197, 25)
        Me.btnICD9.TabIndex = 0
        Me.btnICD9.Tag = "UnSelected"
        Me.btnICD9.Text = "      ICD9"
        Me.btnICD9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnICD9.UseVisualStyleBackColor = False
        '
        'Label35
        '
        Me.Label35.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label35.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label35.Location = New System.Drawing.Point(3, 1)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(1, 25)
        Me.Label35.TabIndex = 14
        '
        'Label36
        '
        Me.Label36.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label36.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label36.Location = New System.Drawing.Point(201, 1)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(1, 25)
        Me.Label36.TabIndex = 13
        '
        'Label37
        '
        Me.Label37.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label37.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label37.Location = New System.Drawing.Point(3, 26)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(199, 1)
        Me.Label37.TabIndex = 12
        '
        'Label38
        '
        Me.Label38.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label38.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label38.Location = New System.Drawing.Point(3, 0)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(199, 1)
        Me.Label38.TabIndex = 11
        '
        'pnlbtnDrugs
        '
        Me.pnlbtnDrugs.BackColor = System.Drawing.Color.Transparent
        Me.pnlbtnDrugs.Controls.Add(Me.btnDrugs)
        Me.pnlbtnDrugs.Controls.Add(Me.Label31)
        Me.pnlbtnDrugs.Controls.Add(Me.Label32)
        Me.pnlbtnDrugs.Controls.Add(Me.Label33)
        Me.pnlbtnDrugs.Controls.Add(Me.Label34)
        Me.pnlbtnDrugs.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlbtnDrugs.Location = New System.Drawing.Point(0, 60)
        Me.pnlbtnDrugs.Name = "pnlbtnDrugs"
        Me.pnlbtnDrugs.Padding = New System.Windows.Forms.Padding(3, 0, 0, 3)
        Me.pnlbtnDrugs.Size = New System.Drawing.Size(202, 30)
        Me.pnlbtnDrugs.TabIndex = 27
        '
        'btnDrugs
        '
        Me.btnDrugs.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.btnDrugs.BackgroundImage = CType(resources.GetObject("btnDrugs.BackgroundImage"), System.Drawing.Image)
        Me.btnDrugs.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnDrugs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnDrugs.FlatAppearance.BorderSize = 0
        Me.btnDrugs.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnDrugs.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnDrugs.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDrugs.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDrugs.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnDrugs.Image = CType(resources.GetObject("btnDrugs.Image"), System.Drawing.Image)
        Me.btnDrugs.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnDrugs.Location = New System.Drawing.Point(4, 1)
        Me.btnDrugs.Name = "btnDrugs"
        Me.btnDrugs.Size = New System.Drawing.Size(197, 25)
        Me.btnDrugs.TabIndex = 0
        Me.btnDrugs.Tag = "UnSelected"
        Me.btnDrugs.Text = "      Drugs"
        Me.btnDrugs.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnDrugs.UseVisualStyleBackColor = False
        '
        'Label31
        '
        Me.Label31.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label31.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label31.Location = New System.Drawing.Point(3, 1)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(1, 25)
        Me.Label31.TabIndex = 14
        '
        'Label32
        '
        Me.Label32.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label32.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label32.Location = New System.Drawing.Point(201, 1)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(1, 25)
        Me.Label32.TabIndex = 13
        '
        'Label33
        '
        Me.Label33.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label33.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label33.Location = New System.Drawing.Point(3, 26)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(199, 1)
        Me.Label33.TabIndex = 12
        '
        'Label34
        '
        Me.Label34.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label34.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label34.Location = New System.Drawing.Point(3, 0)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(199, 1)
        Me.Label34.TabIndex = 11
        '
        'pnlbtnHistory
        '
        Me.pnlbtnHistory.BackColor = System.Drawing.Color.Transparent
        Me.pnlbtnHistory.Controls.Add(Me.btnHistory)
        Me.pnlbtnHistory.Controls.Add(Me.Label27)
        Me.pnlbtnHistory.Controls.Add(Me.Label28)
        Me.pnlbtnHistory.Controls.Add(Me.Label29)
        Me.pnlbtnHistory.Controls.Add(Me.Label30)
        Me.pnlbtnHistory.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlbtnHistory.Location = New System.Drawing.Point(0, 30)
        Me.pnlbtnHistory.Name = "pnlbtnHistory"
        Me.pnlbtnHistory.Padding = New System.Windows.Forms.Padding(3, 0, 0, 3)
        Me.pnlbtnHistory.Size = New System.Drawing.Size(202, 30)
        Me.pnlbtnHistory.TabIndex = 26
        '
        'btnHistory
        '
        Me.btnHistory.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.btnHistory.BackgroundImage = CType(resources.GetObject("btnHistory.BackgroundImage"), System.Drawing.Image)
        Me.btnHistory.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnHistory.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnHistory.FlatAppearance.BorderSize = 0
        Me.btnHistory.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnHistory.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnHistory.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnHistory.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnHistory.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnHistory.Image = CType(resources.GetObject("btnHistory.Image"), System.Drawing.Image)
        Me.btnHistory.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnHistory.Location = New System.Drawing.Point(4, 1)
        Me.btnHistory.Name = "btnHistory"
        Me.btnHistory.Size = New System.Drawing.Size(197, 25)
        Me.btnHistory.TabIndex = 0
        Me.btnHistory.Tag = "UnSelected"
        Me.btnHistory.Text = "      History"
        Me.btnHistory.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnHistory.UseVisualStyleBackColor = False
        '
        'Label27
        '
        Me.Label27.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label27.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label27.Location = New System.Drawing.Point(3, 1)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(1, 25)
        Me.Label27.TabIndex = 14
        '
        'Label28
        '
        Me.Label28.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label28.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label28.Location = New System.Drawing.Point(201, 1)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(1, 25)
        Me.Label28.TabIndex = 13
        '
        'Label29
        '
        Me.Label29.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label29.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label29.Location = New System.Drawing.Point(3, 26)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(199, 1)
        Me.Label29.TabIndex = 12
        '
        'Label30
        '
        Me.Label30.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label30.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label30.Location = New System.Drawing.Point(3, 0)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(199, 1)
        Me.Label30.TabIndex = 11
        '
        'pnbtnDemographics
        '
        Me.pnbtnDemographics.BackColor = System.Drawing.Color.Transparent
        Me.pnbtnDemographics.Controls.Add(Me.btnDemographics)
        Me.pnbtnDemographics.Controls.Add(Me.label58)
        Me.pnbtnDemographics.Controls.Add(Me.label59)
        Me.pnbtnDemographics.Controls.Add(Me.label60)
        Me.pnbtnDemographics.Controls.Add(Me.label61)
        Me.pnbtnDemographics.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnbtnDemographics.Location = New System.Drawing.Point(0, 0)
        Me.pnbtnDemographics.Name = "pnbtnDemographics"
        Me.pnbtnDemographics.Padding = New System.Windows.Forms.Padding(3, 0, 0, 3)
        Me.pnbtnDemographics.Size = New System.Drawing.Size(202, 30)
        Me.pnbtnDemographics.TabIndex = 25
        '
        'btnDemographics
        '
        Me.btnDemographics.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.btnDemographics.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongOrange
        Me.btnDemographics.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnDemographics.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnDemographics.FlatAppearance.BorderSize = 0
        Me.btnDemographics.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.btnDemographics.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.btnDemographics.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDemographics.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDemographics.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnDemographics.Image = CType(resources.GetObject("btnDemographics.Image"), System.Drawing.Image)
        Me.btnDemographics.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnDemographics.Location = New System.Drawing.Point(4, 1)
        Me.btnDemographics.Name = "btnDemographics"
        Me.btnDemographics.Size = New System.Drawing.Size(197, 25)
        Me.btnDemographics.TabIndex = 0
        Me.btnDemographics.Tag = "UnSelected"
        Me.btnDemographics.Text = "      Demographics and Vitals"
        Me.btnDemographics.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnDemographics.UseVisualStyleBackColor = False
        '
        'label58
        '
        Me.label58.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label58.Dock = System.Windows.Forms.DockStyle.Left
        Me.label58.Location = New System.Drawing.Point(3, 1)
        Me.label58.Name = "label58"
        Me.label58.Size = New System.Drawing.Size(1, 25)
        Me.label58.TabIndex = 14
        '
        'label59
        '
        Me.label59.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label59.Dock = System.Windows.Forms.DockStyle.Right
        Me.label59.Location = New System.Drawing.Point(201, 1)
        Me.label59.Name = "label59"
        Me.label59.Size = New System.Drawing.Size(1, 25)
        Me.label59.TabIndex = 13
        '
        'label60
        '
        Me.label60.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label60.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.label60.Location = New System.Drawing.Point(3, 26)
        Me.label60.Name = "label60"
        Me.label60.Size = New System.Drawing.Size(199, 1)
        Me.label60.TabIndex = 12
        '
        'label61
        '
        Me.label61.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label61.Dock = System.Windows.Forms.DockStyle.Top
        Me.label61.Location = New System.Drawing.Point(3, 0)
        Me.label61.Name = "label61"
        Me.label61.Size = New System.Drawing.Size(199, 1)
        Me.label61.TabIndex = 11
        '
        'pnlRight
        '
        Me.pnlRight.Controls.Add(Me.pnltrvTriggers)
        Me.pnlRight.Controls.Add(Me.pnltxtSearchOrder)
        Me.pnlRight.Controls.Add(Me.pnlbtnLab)
        Me.pnlRight.Controls.Add(Me.pnlbtnReferrals)
        Me.pnlRight.Controls.Add(Me.pnlbtnRx)
        Me.pnlRight.Controls.Add(Me.pnlbtnRadiologyTest)
        Me.pnlRight.Controls.Add(Me.pnlbtnGuideline)
        Me.pnlRight.Location = New System.Drawing.Point(806, 30)
        Me.pnlRight.Name = "pnlRight"
        Me.pnlRight.Size = New System.Drawing.Size(206, 576)
        Me.pnlRight.TabIndex = 23
        Me.pnlRight.Visible = False
        '
        'pnltrvTriggers
        '
        Me.pnltrvTriggers.BackColor = System.Drawing.Color.Transparent
        Me.pnltrvTriggers.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnltrvTriggers.Controls.Add(Me.trvTriggers)
        Me.pnltrvTriggers.Controls.Add(Me.Label16)
        Me.pnltrvTriggers.Controls.Add(Me.Label74)
        Me.pnltrvTriggers.Controls.Add(Me.Label75)
        Me.pnltrvTriggers.Controls.Add(Me.Label76)
        Me.pnltrvTriggers.Controls.Add(Me.Label77)
        Me.pnltrvTriggers.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnltrvTriggers.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnltrvTriggers.Location = New System.Drawing.Point(0, 56)
        Me.pnltrvTriggers.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.pnltrvTriggers.Name = "pnltrvTriggers"
        Me.pnltrvTriggers.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.pnltrvTriggers.Size = New System.Drawing.Size(206, 400)
        Me.pnltrvTriggers.TabIndex = 23
        '
        'trvTriggers
        '
        Me.trvTriggers.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trvTriggers.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trvTriggers.ForeColor = System.Drawing.Color.Black
        Me.trvTriggers.HideSelection = False
        Me.trvTriggers.ImageIndex = 0
        Me.trvTriggers.ImageList = Me.ImageList1
        Me.trvTriggers.Location = New System.Drawing.Point(1, 5)
        Me.trvTriggers.Name = "trvTriggers"
        Me.trvTriggers.SelectedImageIndex = 0
        Me.trvTriggers.Size = New System.Drawing.Size(201, 391)
        Me.trvTriggers.TabIndex = 3
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.White
        Me.Label16.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label16.Location = New System.Drawing.Point(1, 1)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(201, 4)
        Me.Label16.TabIndex = 38
        '
        'Label74
        '
        Me.Label74.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label74.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label74.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label74.Location = New System.Drawing.Point(1, 396)
        Me.Label74.Name = "Label74"
        Me.Label74.Size = New System.Drawing.Size(201, 1)
        Me.Label74.TabIndex = 8
        Me.Label74.Text = "label2"
        '
        'Label75
        '
        Me.Label75.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label75.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label75.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label75.Location = New System.Drawing.Point(0, 1)
        Me.Label75.Name = "Label75"
        Me.Label75.Size = New System.Drawing.Size(1, 396)
        Me.Label75.TabIndex = 7
        Me.Label75.Text = "label4"
        '
        'Label76
        '
        Me.Label76.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label76.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label76.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label76.Location = New System.Drawing.Point(202, 1)
        Me.Label76.Name = "Label76"
        Me.Label76.Size = New System.Drawing.Size(1, 396)
        Me.Label76.TabIndex = 6
        Me.Label76.Text = "label3"
        '
        'Label77
        '
        Me.Label77.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label77.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label77.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label77.Location = New System.Drawing.Point(0, 0)
        Me.Label77.Name = "Label77"
        Me.Label77.Size = New System.Drawing.Size(203, 1)
        Me.Label77.TabIndex = 5
        Me.Label77.Text = "label1"
        '
        'pnltxtSearchOrder
        '
        Me.pnltxtSearchOrder.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnltxtSearchOrder.Controls.Add(Me.txtSearchOrder)
        Me.pnltxtSearchOrder.Controls.Add(Me.Label20)
        Me.pnltxtSearchOrder.Controls.Add(Me.Label21)
        Me.pnltxtSearchOrder.Controls.Add(Me.PictureBox1)
        Me.pnltxtSearchOrder.Controls.Add(Me.Label15)
        Me.pnltxtSearchOrder.Controls.Add(Me.Label82)
        Me.pnltxtSearchOrder.Controls.Add(Me.Label83)
        Me.pnltxtSearchOrder.Controls.Add(Me.Label84)
        Me.pnltxtSearchOrder.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnltxtSearchOrder.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnltxtSearchOrder.ForeColor = System.Drawing.Color.Black
        Me.pnltxtSearchOrder.Location = New System.Drawing.Point(0, 30)
        Me.pnltxtSearchOrder.Name = "pnltxtSearchOrder"
        Me.pnltxtSearchOrder.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.pnltxtSearchOrder.Size = New System.Drawing.Size(206, 26)
        Me.pnltxtSearchOrder.TabIndex = 16
        '
        'txtSearchOrder
        '
        Me.txtSearchOrder.BackColor = System.Drawing.Color.White
        Me.txtSearchOrder.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtSearchOrder.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtSearchOrder.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSearchOrder.ForeColor = System.Drawing.Color.Black
        Me.txtSearchOrder.Location = New System.Drawing.Point(29, 5)
        Me.txtSearchOrder.Name = "txtSearchOrder"
        Me.txtSearchOrder.Size = New System.Drawing.Size(173, 15)
        Me.txtSearchOrder.TabIndex = 0
        '
        'Label20
        '
        Me.Label20.BackColor = System.Drawing.Color.White
        Me.Label20.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label20.Location = New System.Drawing.Point(29, 1)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(173, 4)
        Me.Label20.TabIndex = 37
        '
        'Label21
        '
        Me.Label21.BackColor = System.Drawing.Color.White
        Me.Label21.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label21.Location = New System.Drawing.Point(29, 20)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(173, 2)
        Me.Label21.TabIndex = 38
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.Color.White
        Me.PictureBox1.Dock = System.Windows.Forms.DockStyle.Left
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(1, 1)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(28, 21)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox1.TabIndex = 9
        Me.PictureBox1.TabStop = False
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label15.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label15.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label15.Location = New System.Drawing.Point(1, 22)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(201, 1)
        Me.Label15.TabIndex = 42
        Me.Label15.Text = "label2"
        '
        'Label82
        '
        Me.Label82.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label82.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label82.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label82.Location = New System.Drawing.Point(0, 1)
        Me.Label82.Name = "Label82"
        Me.Label82.Size = New System.Drawing.Size(1, 22)
        Me.Label82.TabIndex = 41
        Me.Label82.Text = "label4"
        '
        'Label83
        '
        Me.Label83.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label83.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label83.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label83.Location = New System.Drawing.Point(202, 1)
        Me.Label83.Name = "Label83"
        Me.Label83.Size = New System.Drawing.Size(1, 22)
        Me.Label83.TabIndex = 40
        Me.Label83.Text = "label3"
        '
        'Label84
        '
        Me.Label84.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label84.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label84.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label84.Location = New System.Drawing.Point(0, 0)
        Me.Label84.Name = "Label84"
        Me.Label84.Size = New System.Drawing.Size(203, 1)
        Me.Label84.TabIndex = 39
        Me.Label84.Text = "label1"
        '
        'pnlbtnLab
        '
        Me.pnlbtnLab.BackColor = System.Drawing.Color.Transparent
        Me.pnlbtnLab.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlbtnLab.Controls.Add(Me.btnLab)
        Me.pnlbtnLab.Controls.Add(Me.Label78)
        Me.pnlbtnLab.Controls.Add(Me.Label79)
        Me.pnlbtnLab.Controls.Add(Me.Label80)
        Me.pnlbtnLab.Controls.Add(Me.Label81)
        Me.pnlbtnLab.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlbtnLab.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlbtnLab.Location = New System.Drawing.Point(0, 0)
        Me.pnlbtnLab.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.pnlbtnLab.Name = "pnlbtnLab"
        Me.pnlbtnLab.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.pnlbtnLab.Size = New System.Drawing.Size(206, 30)
        Me.pnlbtnLab.TabIndex = 24
        '
        'btnLab
        '
        Me.btnLab.BackColor = System.Drawing.Color.Transparent
        Me.btnLab.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongOrange
        Me.btnLab.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnLab.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnLab.FlatAppearance.BorderSize = 0
        Me.btnLab.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnLab.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnLab.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnLab.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnLab.Location = New System.Drawing.Point(1, 1)
        Me.btnLab.Name = "btnLab"
        Me.btnLab.Size = New System.Drawing.Size(201, 25)
        Me.btnLab.TabIndex = 0
        Me.btnLab.Tag = "Selected"
        Me.btnLab.Text = "&Lab"
        Me.btnLab.UseVisualStyleBackColor = False
        '
        'Label78
        '
        Me.Label78.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label78.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label78.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label78.Location = New System.Drawing.Point(1, 26)
        Me.Label78.Name = "Label78"
        Me.Label78.Size = New System.Drawing.Size(201, 1)
        Me.Label78.TabIndex = 8
        Me.Label78.Text = "label2"
        '
        'Label79
        '
        Me.Label79.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label79.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label79.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label79.Location = New System.Drawing.Point(0, 1)
        Me.Label79.Name = "Label79"
        Me.Label79.Size = New System.Drawing.Size(1, 26)
        Me.Label79.TabIndex = 7
        Me.Label79.Text = "label4"
        '
        'Label80
        '
        Me.Label80.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label80.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label80.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label80.Location = New System.Drawing.Point(202, 1)
        Me.Label80.Name = "Label80"
        Me.Label80.Size = New System.Drawing.Size(1, 26)
        Me.Label80.TabIndex = 6
        Me.Label80.Text = "label3"
        '
        'Label81
        '
        Me.Label81.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label81.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label81.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label81.Location = New System.Drawing.Point(0, 0)
        Me.Label81.Name = "Label81"
        Me.Label81.Size = New System.Drawing.Size(203, 1)
        Me.Label81.TabIndex = 5
        Me.Label81.Text = "label1"
        '
        'pnlbtnReferrals
        '
        Me.pnlbtnReferrals.BackColor = System.Drawing.Color.Transparent
        Me.pnlbtnReferrals.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlbtnReferrals.Controls.Add(Me.btnReferrals)
        Me.pnlbtnReferrals.Controls.Add(Me.Label70)
        Me.pnlbtnReferrals.Controls.Add(Me.Label71)
        Me.pnlbtnReferrals.Controls.Add(Me.Label72)
        Me.pnlbtnReferrals.Controls.Add(Me.Label73)
        Me.pnlbtnReferrals.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlbtnReferrals.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlbtnReferrals.Location = New System.Drawing.Point(0, 456)
        Me.pnlbtnReferrals.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.pnlbtnReferrals.Name = "pnlbtnReferrals"
        Me.pnlbtnReferrals.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.pnlbtnReferrals.Size = New System.Drawing.Size(206, 30)
        Me.pnlbtnReferrals.TabIndex = 22
        '
        'btnReferrals
        '
        Me.btnReferrals.BackgroundImage = CType(resources.GetObject("btnReferrals.BackgroundImage"), System.Drawing.Image)
        Me.btnReferrals.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnReferrals.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnReferrals.FlatAppearance.BorderSize = 0
        Me.btnReferrals.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnReferrals.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnReferrals.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnReferrals.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReferrals.Location = New System.Drawing.Point(1, 1)
        Me.btnReferrals.Name = "btnReferrals"
        Me.btnReferrals.Size = New System.Drawing.Size(201, 25)
        Me.btnReferrals.TabIndex = 5
        Me.btnReferrals.Tag = "UnSelected"
        Me.btnReferrals.Text = "&Referrals"
        Me.btnReferrals.UseVisualStyleBackColor = True
        '
        'Label70
        '
        Me.Label70.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label70.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label70.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label70.Location = New System.Drawing.Point(1, 26)
        Me.Label70.Name = "Label70"
        Me.Label70.Size = New System.Drawing.Size(201, 1)
        Me.Label70.TabIndex = 8
        Me.Label70.Text = "label2"
        '
        'Label71
        '
        Me.Label71.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label71.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label71.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label71.Location = New System.Drawing.Point(0, 1)
        Me.Label71.Name = "Label71"
        Me.Label71.Size = New System.Drawing.Size(1, 26)
        Me.Label71.TabIndex = 7
        Me.Label71.Text = "label4"
        '
        'Label72
        '
        Me.Label72.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label72.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label72.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label72.Location = New System.Drawing.Point(202, 1)
        Me.Label72.Name = "Label72"
        Me.Label72.Size = New System.Drawing.Size(1, 26)
        Me.Label72.TabIndex = 6
        Me.Label72.Text = "label3"
        '
        'Label73
        '
        Me.Label73.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label73.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label73.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label73.Location = New System.Drawing.Point(0, 0)
        Me.Label73.Name = "Label73"
        Me.Label73.Size = New System.Drawing.Size(203, 1)
        Me.Label73.TabIndex = 5
        Me.Label73.Text = "label1"
        '
        'pnlbtnRx
        '
        Me.pnlbtnRx.BackColor = System.Drawing.Color.Transparent
        Me.pnlbtnRx.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlbtnRx.Controls.Add(Me.btnRx)
        Me.pnlbtnRx.Controls.Add(Me.Label66)
        Me.pnlbtnRx.Controls.Add(Me.Label67)
        Me.pnlbtnRx.Controls.Add(Me.Label68)
        Me.pnlbtnRx.Controls.Add(Me.Label69)
        Me.pnlbtnRx.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlbtnRx.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlbtnRx.Location = New System.Drawing.Point(0, 486)
        Me.pnlbtnRx.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.pnlbtnRx.Name = "pnlbtnRx"
        Me.pnlbtnRx.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.pnlbtnRx.Size = New System.Drawing.Size(206, 30)
        Me.pnlbtnRx.TabIndex = 21
        '
        'btnRx
        '
        Me.btnRx.BackgroundImage = CType(resources.GetObject("btnRx.BackgroundImage"), System.Drawing.Image)
        Me.btnRx.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnRx.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnRx.FlatAppearance.BorderSize = 0
        Me.btnRx.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnRx.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnRx.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRx.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRx.Location = New System.Drawing.Point(1, 1)
        Me.btnRx.Name = "btnRx"
        Me.btnRx.Size = New System.Drawing.Size(201, 25)
        Me.btnRx.TabIndex = 4
        Me.btnRx.Tag = "UnSelected"
        Me.btnRx.Text = "R&x"
        Me.btnRx.UseVisualStyleBackColor = True
        '
        'Label66
        '
        Me.Label66.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label66.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label66.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label66.Location = New System.Drawing.Point(1, 26)
        Me.Label66.Name = "Label66"
        Me.Label66.Size = New System.Drawing.Size(201, 1)
        Me.Label66.TabIndex = 8
        Me.Label66.Text = "label2"
        '
        'Label67
        '
        Me.Label67.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label67.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label67.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label67.Location = New System.Drawing.Point(0, 1)
        Me.Label67.Name = "Label67"
        Me.Label67.Size = New System.Drawing.Size(1, 26)
        Me.Label67.TabIndex = 7
        Me.Label67.Text = "label4"
        '
        'Label68
        '
        Me.Label68.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label68.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label68.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label68.Location = New System.Drawing.Point(202, 1)
        Me.Label68.Name = "Label68"
        Me.Label68.Size = New System.Drawing.Size(1, 26)
        Me.Label68.TabIndex = 6
        Me.Label68.Text = "label3"
        '
        'Label69
        '
        Me.Label69.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label69.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label69.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label69.Location = New System.Drawing.Point(0, 0)
        Me.Label69.Name = "Label69"
        Me.Label69.Size = New System.Drawing.Size(203, 1)
        Me.Label69.TabIndex = 5
        Me.Label69.Text = "label1"
        '
        'pnlbtnRadiologyTest
        '
        Me.pnlbtnRadiologyTest.BackColor = System.Drawing.Color.Transparent
        Me.pnlbtnRadiologyTest.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlbtnRadiologyTest.Controls.Add(Me.btnRadiologyTest)
        Me.pnlbtnRadiologyTest.Controls.Add(Me.Label55)
        Me.pnlbtnRadiologyTest.Controls.Add(Me.Label56)
        Me.pnlbtnRadiologyTest.Controls.Add(Me.Label57)
        Me.pnlbtnRadiologyTest.Controls.Add(Me.Label62)
        Me.pnlbtnRadiologyTest.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlbtnRadiologyTest.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlbtnRadiologyTest.Location = New System.Drawing.Point(0, 516)
        Me.pnlbtnRadiologyTest.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.pnlbtnRadiologyTest.Name = "pnlbtnRadiologyTest"
        Me.pnlbtnRadiologyTest.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.pnlbtnRadiologyTest.Size = New System.Drawing.Size(206, 30)
        Me.pnlbtnRadiologyTest.TabIndex = 20
        '
        'btnRadiologyTest
        '
        Me.btnRadiologyTest.BackgroundImage = CType(resources.GetObject("btnRadiologyTest.BackgroundImage"), System.Drawing.Image)
        Me.btnRadiologyTest.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnRadiologyTest.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnRadiologyTest.FlatAppearance.BorderSize = 0
        Me.btnRadiologyTest.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnRadiologyTest.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnRadiologyTest.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRadiologyTest.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRadiologyTest.Location = New System.Drawing.Point(1, 1)
        Me.btnRadiologyTest.Name = "btnRadiologyTest"
        Me.btnRadiologyTest.Size = New System.Drawing.Size(201, 25)
        Me.btnRadiologyTest.TabIndex = 2
        Me.btnRadiologyTest.Tag = "UnSelected"
        Me.btnRadiologyTest.Text = "&Orders"
        Me.btnRadiologyTest.UseVisualStyleBackColor = True
        '
        'Label55
        '
        Me.Label55.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label55.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label55.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label55.Location = New System.Drawing.Point(1, 26)
        Me.Label55.Name = "Label55"
        Me.Label55.Size = New System.Drawing.Size(201, 1)
        Me.Label55.TabIndex = 8
        Me.Label55.Text = "label2"
        '
        'Label56
        '
        Me.Label56.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label56.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label56.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label56.Location = New System.Drawing.Point(0, 1)
        Me.Label56.Name = "Label56"
        Me.Label56.Size = New System.Drawing.Size(1, 26)
        Me.Label56.TabIndex = 7
        Me.Label56.Text = "label4"
        '
        'Label57
        '
        Me.Label57.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label57.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label57.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label57.Location = New System.Drawing.Point(202, 1)
        Me.Label57.Name = "Label57"
        Me.Label57.Size = New System.Drawing.Size(1, 26)
        Me.Label57.TabIndex = 6
        Me.Label57.Text = "label3"
        '
        'Label62
        '
        Me.Label62.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label62.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label62.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label62.Location = New System.Drawing.Point(0, 0)
        Me.Label62.Name = "Label62"
        Me.Label62.Size = New System.Drawing.Size(203, 1)
        Me.Label62.TabIndex = 5
        Me.Label62.Text = "label1"
        '
        'pnlbtnGuideline
        '
        Me.pnlbtnGuideline.BackColor = System.Drawing.Color.Transparent
        Me.pnlbtnGuideline.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlbtnGuideline.Controls.Add(Me.btnGuideline)
        Me.pnlbtnGuideline.Controls.Add(Me.Label17)
        Me.pnlbtnGuideline.Controls.Add(Me.Label18)
        Me.pnlbtnGuideline.Controls.Add(Me.Label19)
        Me.pnlbtnGuideline.Controls.Add(Me.Label22)
        Me.pnlbtnGuideline.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlbtnGuideline.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlbtnGuideline.Location = New System.Drawing.Point(0, 546)
        Me.pnlbtnGuideline.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.pnlbtnGuideline.Name = "pnlbtnGuideline"
        Me.pnlbtnGuideline.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.pnlbtnGuideline.Size = New System.Drawing.Size(206, 30)
        Me.pnlbtnGuideline.TabIndex = 20
        '
        'btnGuideline
        '
        Me.btnGuideline.BackgroundImage = CType(resources.GetObject("btnGuideline.BackgroundImage"), System.Drawing.Image)
        Me.btnGuideline.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnGuideline.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnGuideline.FlatAppearance.BorderSize = 0
        Me.btnGuideline.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnGuideline.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnGuideline.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnGuideline.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGuideline.Location = New System.Drawing.Point(1, 1)
        Me.btnGuideline.Name = "btnGuideline"
        Me.btnGuideline.Size = New System.Drawing.Size(201, 25)
        Me.btnGuideline.TabIndex = 1
        Me.btnGuideline.Tag = "UnSelected"
        Me.btnGuideline.Text = "&Guidelines"
        Me.btnGuideline.UseVisualStyleBackColor = True
        '
        'Label17
        '
        Me.Label17.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label17.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label17.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label17.Location = New System.Drawing.Point(1, 26)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(201, 1)
        Me.Label17.TabIndex = 8
        Me.Label17.Text = "label2"
        '
        'Label18
        '
        Me.Label18.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label18.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label18.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(0, 1)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(1, 26)
        Me.Label18.TabIndex = 7
        Me.Label18.Text = "label4"
        '
        'Label19
        '
        Me.Label19.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label19.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label19.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label19.Location = New System.Drawing.Point(202, 1)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(1, 26)
        Me.Label19.TabIndex = 6
        Me.Label19.Text = "label3"
        '
        'Label22
        '
        Me.Label22.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label22.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label22.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.Location = New System.Drawing.Point(0, 0)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(203, 1)
        Me.Label22.TabIndex = 5
        Me.Label22.Text = "label1"
        '
        'pnlMsgTOP
        '
        Me.pnlMsgTOP.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlMsgTOP.Controls.Add(Me.pnlMsg)
        Me.pnlMsgTOP.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlMsgTOP.Location = New System.Drawing.Point(0, 0)
        Me.pnlMsgTOP.Name = "pnlMsgTOP"
        Me.pnlMsgTOP.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlMsgTOP.Size = New System.Drawing.Size(844, 30)
        Me.pnlMsgTOP.TabIndex = 0
        '
        'pnlMsg
        '
        Me.pnlMsg.BackColor = System.Drawing.Color.Transparent
        Me.pnlMsg.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.pnlMsg.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlMsg.Controls.Add(Me.Label95)
        Me.pnlMsg.Controls.Add(Me.Label14)
        Me.pnlMsg.Controls.Add(Me.txtMessage)
        Me.pnlMsg.Controls.Add(Me.Label4)
        Me.pnlMsg.Controls.Add(Me.txtName)
        Me.pnlMsg.Controls.Add(Me.Label3)
        Me.pnlMsg.Controls.Add(Me.Label23)
        Me.pnlMsg.Controls.Add(Me.Label24)
        Me.pnlMsg.Controls.Add(Me.Label63)
        Me.pnlMsg.Controls.Add(Me.Label25)
        Me.pnlMsg.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMsg.Location = New System.Drawing.Point(3, 3)
        Me.pnlMsg.Name = "pnlMsg"
        Me.pnlMsg.Size = New System.Drawing.Size(838, 24)
        Me.pnlMsg.TabIndex = 1
        '
        'Label95
        '
        Me.Label95.AutoSize = True
        Me.Label95.ForeColor = System.Drawing.Color.Red
        Me.Label95.Location = New System.Drawing.Point(292, 5)
        Me.Label95.Name = "Label95"
        Me.Label95.Size = New System.Drawing.Size(14, 14)
        Me.Label95.TabIndex = 2300
        Me.Label95.Text = "*"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.ForeColor = System.Drawing.Color.Red
        Me.Label14.Location = New System.Drawing.Point(11, 5)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(14, 14)
        Me.Label14.TabIndex = 2200
        Me.Label14.Text = "*"
        '
        'txtMessage
        '
        Me.txtMessage.Dock = System.Windows.Forms.DockStyle.Left
        Me.txtMessage.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMessage.ForeColor = System.Drawing.Color.Black
        Me.txtMessage.Location = New System.Drawing.Point(376, 1)
        Me.txtMessage.Name = "txtMessage"
        Me.txtMessage.Size = New System.Drawing.Size(421, 22)
        Me.txtMessage.TabIndex = 1
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label4.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(291, 1)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(85, 22)
        Me.Label4.TabIndex = 1300
        Me.Label4.Text = "   Message :"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtName
        '
        Me.txtName.Dock = System.Windows.Forms.DockStyle.Left
        Me.txtName.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtName.ForeColor = System.Drawing.Color.Black
        Me.txtName.Location = New System.Drawing.Point(70, 1)
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(221, 22)
        Me.txtName.TabIndex = 0
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label3.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(1, 1)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(69, 22)
        Me.Label3.TabIndex = 1100
        Me.Label3.Text = "  Name :"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label23
        '
        Me.Label23.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label23.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label23.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label23.Location = New System.Drawing.Point(1, 23)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(836, 1)
        Me.Label23.TabIndex = 17
        Me.Label23.Text = "label2"
        '
        'Label24
        '
        Me.Label24.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label24.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label24.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.Location = New System.Drawing.Point(0, 1)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(1, 23)
        Me.Label24.TabIndex = 16
        Me.Label24.Text = "label4"
        '
        'Label63
        '
        Me.Label63.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label63.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label63.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label63.Location = New System.Drawing.Point(837, 1)
        Me.Label63.Name = "Label63"
        Me.Label63.Size = New System.Drawing.Size(1, 23)
        Me.Label63.TabIndex = 15
        Me.Label63.Text = "label3"
        '
        'Label25
        '
        Me.Label25.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label25.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label25.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.Location = New System.Drawing.Point(0, 0)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(838, 1)
        Me.Label25.TabIndex = 21
        Me.Label25.Text = "label1"
        '
        'pnl_tlstrip
        '
        Me.pnl_tlstrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnl_tlstrip.Controls.Add(Me.tlsDM)
        Me.pnl_tlstrip.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_tlstrip.Location = New System.Drawing.Point(0, 0)
        Me.pnl_tlstrip.Name = "pnl_tlstrip"
        Me.pnl_tlstrip.Size = New System.Drawing.Size(844, 54)
        Me.pnl_tlstrip.TabIndex = 3
        '
        'tlsDM
        '
        Me.tlsDM.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tlsDM.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlsDM.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsDM.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tlsDM.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tlsDM_Save, Me.tlsDM_Close})
        Me.tlsDM.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tlsDM.Location = New System.Drawing.Point(0, 0)
        Me.tlsDM.Name = "tlsDM"
        Me.tlsDM.Size = New System.Drawing.Size(844, 53)
        Me.tlsDM.TabIndex = 0
        Me.tlsDM.Text = "ToolStrip1"
        '
        'tlsDM_Save
        '
        Me.tlsDM_Save.Image = CType(resources.GetObject("tlsDM_Save.Image"), System.Drawing.Image)
        Me.tlsDM_Save.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlsDM_Save.Name = "tlsDM_Save"
        Me.tlsDM_Save.Size = New System.Drawing.Size(66, 50)
        Me.tlsDM_Save.Tag = "Save"
        Me.tlsDM_Save.Text = "&Save&&Cls"
        Me.tlsDM_Save.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlsDM_Save.ToolTipText = "Save and Close"
        '
        'tlsDM_Close
        '
        Me.tlsDM_Close.Image = CType(resources.GetObject("tlsDM_Close.Image"), System.Drawing.Image)
        Me.tlsDM_Close.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlsDM_Close.Name = "tlsDM_Close"
        Me.tlsDM_Close.Size = New System.Drawing.Size(43, 50)
        Me.tlsDM_Close.Tag = "Close"
        Me.tlsDM_Close.Text = "&Close"
        Me.tlsDM_Close.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'CntConditions
        '
        Me.CntConditions.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuDelete, Me.mnuReferral, Me.EditReferral, Me.mnuEditTemplate})
        '
        'mnuDelete
        '
        Me.mnuDelete.Index = 0
        Me.mnuDelete.Text = "Delete "
        '
        'mnuReferral
        '
        Me.mnuReferral.Index = 1
        Me.mnuReferral.Text = "Add Referral"
        '
        'EditReferral
        '
        Me.EditReferral.Index = 2
        Me.EditReferral.Text = "Edit Referrals"
        '
        'mnuEditTemplate
        '
        Me.mnuEditTemplate.Index = 3
        Me.mnuEditTemplate.Text = "Edit Template"
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuDeleteDrugs})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(148, 26)
        '
        'mnuDeleteDrugs
        '
        Me.mnuDeleteDrugs.Name = "mnuDeleteDrugs"
        Me.mnuDeleteDrugs.Size = New System.Drawing.Size(147, 22)
        Me.mnuDeleteDrugs.Text = "Delete Drugs"
        '
        'ContextMenuHistory
        '
        Me.ContextMenuHistory.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuDeleteHistory})
        Me.ContextMenuHistory.Name = "ContextMenuStrip1"
        Me.ContextMenuHistory.Size = New System.Drawing.Size(154, 26)
        '
        'mnuDeleteHistory
        '
        Me.mnuDeleteHistory.Name = "mnuDeleteHistory"
        Me.mnuDeleteHistory.Size = New System.Drawing.Size(153, 22)
        Me.mnuDeleteHistory.Text = "Delete History"
        '
        'frmCV_Setup
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(844, 660)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.pnl_tlstrip)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmCV_Setup"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Cardiovascular Risk Setup"
        Me.pnlMain.ResumeLayout(False)
        Me.pnlMiddle.ResumeLayout(False)
        Me.pnlDemoVitals.ResumeLayout(False)
        Me.pnlVitals.ResumeLayout(False)
        Me.pnlVitals.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel7.ResumeLayout(False)
        Me.pnlDemographics.ResumeLayout(False)
        Me.pnlDemographics.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel6.ResumeLayout(False)
        Me.pnlLab.ResumeLayout(False)
        Me.Panel12.ResumeLayout(False)
        CType(Me.C1LabResult, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel13.ResumeLayout(False)
        Me.Panel25.ResumeLayout(False)
        Me.Panel25.PerformLayout()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel14.ResumeLayout(False)
        Me.pnlHistory.ResumeLayout(False)
        Me.Panel22.ResumeLayout(False)
        Me.Panel8.ResumeLayout(False)
        Me.Panel21.ResumeLayout(False)
        Me.Panel9.ResumeLayout(False)
        Me.Panel9.PerformLayout()
        Me.pnlHistoryLeft.ResumeLayout(False)
        Me.Panel23.ResumeLayout(False)
        Me.Panel24.ResumeLayout(False)
        Me.pnlDrugs.ResumeLayout(False)
        Me.pnltrvSelectedDrugs.ResumeLayout(False)
        Me.pnlSelectedDrugLabel.ResumeLayout(False)
        Me.Panel20.ResumeLayout(False)
        Me.Panel11.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlRadiology.ResumeLayout(False)
        Me.Panel18.ResumeLayout(False)
        CType(Me.c1Labs, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel17.ResumeLayout(False)
        Me.Panel17.PerformLayout()
        CType(Me.PictureBox6, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlSummaryOthers.ResumeLayout(False)
        Me.pnlSummary.ResumeLayout(False)
        Me.pnlSummary.PerformLayout()
        Me.pnlSummaryHeader.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.pnlCPT.ResumeLayout(False)
        Me.Panel15.ResumeLayout(False)
        Me.Panel16.ResumeLayout(False)
        Me.Panel16.PerformLayout()
        CType(Me.PictureBox5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlICD9.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        Me.Panel10.ResumeLayout(False)
        Me.Panel10.PerformLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlLeft.ResumeLayout(False)
        Me.Panel19.ResumeLayout(False)
        Me.pnlbtnOrders.ResumeLayout(False)
        Me.pnlbtnRadiology.ResumeLayout(False)
        Me.pnlbtnLabs.ResumeLayout(False)
        Me.pnlbtnCPT.ResumeLayout(False)
        Me.pnlbtnICD9.ResumeLayout(False)
        Me.pnlbtnDrugs.ResumeLayout(False)
        Me.pnlbtnHistory.ResumeLayout(False)
        Me.pnbtnDemographics.ResumeLayout(False)
        Me.pnlRight.ResumeLayout(False)
        Me.pnltrvTriggers.ResumeLayout(False)
        Me.pnltxtSearchOrder.ResumeLayout(False)
        Me.pnltxtSearchOrder.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlbtnLab.ResumeLayout(False)
        Me.pnlbtnReferrals.ResumeLayout(False)
        Me.pnlbtnRx.ResumeLayout(False)
        Me.pnlbtnRadiologyTest.ResumeLayout(False)
        Me.pnlbtnGuideline.ResumeLayout(False)
        Me.pnlMsgTOP.ResumeLayout(False)
        Me.pnlMsg.ResumeLayout(False)
        Me.pnlMsg.PerformLayout()
        Me.pnl_tlstrip.ResumeLayout(False)
        Me.pnl_tlstrip.PerformLayout()
        Me.tlsDM.ResumeLayout(False)
        Me.tlsDM.PerformLayout()
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.ContextMenuHistory.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    ''This object is used for OtherDetails Which has been saved in past, but did not loaded due to changed settings.
    ''We will use this Object to save Remaining Details again in DataBase.
    ''So we will not lose our Invisible Data (Invisible on form).
    Dim oOtherDetailsRemain As New gloStream.CardioVascular.Supporting.OtherDetails
    Dim CurrentParentNode As TreeNode ''To Keep Track of Which Node is Selected / Expanded / Checked ''Mostly used for search History
    Dim dtDrugs As DataTable

    Private Sub btnHistory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHistory.Click
        Try
            'This code is commented by shilpa because currently we are not using tab structure in form
            ''function for display history Tag of the Tag Pages and hide others tags pages
            ''HidenShow(tpHistory)

            'Code added by dipak 20091003 :fills history
            If (isHistoryLoaded = False) Then
                Fill_Histories()
                isHistoryLoaded = True
            End If
            'End Code added by dipak 20091003 
            Cursor = Cursors.WaitCursor

            pnlHistory.Visible = True
            pnlHistory.BringToFront()

            btnHistory.BackgroundImage = Global.gloEMR.My.Resources.Img_LongOrange
            btnHistory.BackgroundImageLayout = ImageLayout.Stretch
            btnHistory.Tag = "Selected"

            btnDemographics.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
            btnDemographics.BackgroundImageLayout = ImageLayout.Stretch
            btnDemographics.Tag = "UnSelected"

            btnRadiology.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
            btnRadiology.BackgroundImageLayout = ImageLayout.Stretch
            btnRadiology.Tag = "UnSelected"

            btnOrders.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
            btnOrders.BackgroundImageLayout = ImageLayout.Stretch
            btnOrders.Tag = "UnSelected"

            btnDrugs.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
            btnDrugs.BackgroundImageLayout = ImageLayout.Stretch
            btnDrugs.Tag = "UnSelected"

            btnICD9.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
            btnICD9.BackgroundImageLayout = ImageLayout.Stretch
            btnICD9.Tag = "UnSelected"

            btnCPT.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
            btnCPT.BackgroundImageLayout = ImageLayout.Stretch
            btnCPT.Tag = "UnSelected"

            btnLabs.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
            btnLabs.BackgroundImageLayout = ImageLayout.Stretch
            btnLabs.Tag = "UnSelected"
            If (cmbHistoryCategory.Items.Count > 0) Then
                cmbHistoryCategory.SelectedIndex = 0
            End If

            cmbHistoryCategory_SelectionChangeCommitted(Nothing, Nothing)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Cursor = Cursors.Default
    End Sub

    Private Sub btnDemographics_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDemographics.Click
        Try
            'This code is commented by shilpa because currently we are not using tab structure in form
            ''function for display Demographic Tag of the Tag Pages and hide others tags pages
            'HidenShow(tpDemographics)

            ''This code is added by shilpa for making visible to the panel
            pnlDemoVitals.Visible = True
            pnlDemoVitals.BringToFront()

            btnDemographics.BackgroundImage = Global.gloEMR.My.Resources.Img_LongOrange
            btnDemographics.BackgroundImageLayout = ImageLayout.Stretch
            btnDemographics.Tag = "Selected"

            btnRadiology.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
            btnRadiology.BackgroundImageLayout = ImageLayout.Stretch
            btnRadiology.Tag = "UnSelected"

            btnOrders.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
            btnOrders.BackgroundImageLayout = ImageLayout.Stretch
            btnOrders.Tag = "UnSelected"

            btnHistory.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
            btnHistory.BackgroundImageLayout = ImageLayout.Stretch
            btnHistory.Tag = "UnSelected"

            btnDrugs.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
            btnDrugs.BackgroundImageLayout = ImageLayout.Stretch
            btnDrugs.Tag = "UnSelected"

            btnICD9.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
            btnICD9.BackgroundImageLayout = ImageLayout.Stretch
            btnICD9.Tag = "UnSelected"

            btnCPT.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
            btnCPT.BackgroundImageLayout = ImageLayout.Stretch
            btnCPT.Tag = "UnSelected"

            btnLabs.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
            btnLabs.BackgroundImageLayout = ImageLayout.Stretch
            btnLabs.Tag = "UnSelected"
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub frmCV_Setup_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        gloC1FlexStyle.Style(c1Labs)
        gloC1FlexStyle.Style(C1LabResult)

        btnDemographics.BackgroundImage = Global.gloEMR.My.Resources.Img_LongOrange
        btnDemographics.BackgroundImageLayout = ImageLayout.Stretch
        btnDemographics.Tag = "Selected"

        Try
            'functions commented by dipak 20091003 because it take time to load form
            ' function move to respective button click ie. Fill_Histories() is call from btnHistory_Click 
            'Fill_Drugs()
            Fill_Age()
            Fill_Gender()

            'Fill_Histories()
            Fill_Orders()
            Fill_Labs()
            'end comment by dipak 20091003

            If m_CriteriaId <> 0 Then
                Fill_EditCriteria(m_CriteriaId)
            End If
            'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.RecordViewed, "Viewed Cardiovascular records", gstrLoginName, gstrClientMachineName, gnPatientID, True, gloAuditTrail.enmOutCome.Success, "gloEMR")
            ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.CardioVascular, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.View, "Viewed CardioVascular Criteria", gloAuditTrail.ActivityOutCome.Success)
            ''Added Rahul P on 20101011
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.CardioVascular, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.View, "Viewed CardioVascular Criteria", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            ''
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CardioVascular, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.View, "Cannot viewed CardioVascular Criteria", gloAuditTrail.ActivityOutCome.Failure)
            'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.RecordViewed, "Could not View Cardiovascular records", gstrLoginName, gstrClientMachineName, gnPatientID, True, gloAuditTrail.enmOutCome.Failure, "gloEMR")
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        txt_summary.ReadOnly = True
        txt_summary.BackColor = Color.White
    End Sub

    Public Sub FillLabTest()
        trvTriggers.Nodes.Clear()
        Dim rootnode As myTreeNode = Nothing
        rootnode = New myTreeNode("Labs", -1)
        rootnode.ImageIndex = 6
        rootnode.SelectedImageIndex = 6
        trvTriggers.Nodes.Add(rootnode)
        Dim _C As Integer
        ''''Create object for the class
        Dim oDM As New gloStream.DiseaseManagement.Common.Criteria
        Dim oLabsModule As gloStream.DiseaseManagement.Supporting.LabModuleTests

        ''''assign Lab Test and Result to collection
        oLabsModule = oDM.LabModuleTests

        If Not oLabsModule Is Nothing Then
            If oLabsModule.Count > 0 Then

                'Fill Test
                For _C = 1 To oLabsModule.Count
                    rootnode = New myTreeNode(oLabsModule(_C).Name, oLabsModule(_C).TestID)
                    rootnode.ImageIndex = 11
                    rootnode.SelectedImageIndex = 11
                    trvTriggers.Nodes.Item(0).Nodes.Add(rootnode)
                Next
                trvTriggers.ExpandAll()
            End If
            oLabsModule.Dispose()
            oLabsModule = Nothing
        End If
        oDM.Dispose()
        oDM = Nothing

    End Sub

    Public Sub PopulateAssocaitedInfo(ByVal ID As Int32)
        pnlbtnLab.Dock = DockStyle.Bottom
        btnLab.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
        btnLab.BackgroundImageLayout = ImageLayout.Stretch
        btnLab.Tag = "UnSelected"

        pnlbtnReferrals.Dock = DockStyle.Bottom
        btnReferrals.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
        btnReferrals.BackgroundImageLayout = ImageLayout.Stretch
        btnReferrals.Tag = "UnSelected"

        pnlbtnRx.Dock = DockStyle.Bottom
        btnRx.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
        btnRx.BackgroundImageLayout = ImageLayout.Stretch
        btnRx.Tag = "UnSelected"

        pnlbtnRadiologyTest.Dock = DockStyle.Bottom
        btnRadiologyTest.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
        btnRadiologyTest.BackgroundImageLayout = ImageLayout.Stretch
        btnRadiologyTest.Tag = "UnSelected"

        pnlbtnGuideline.Dock = DockStyle.Bottom
        btnGuideline.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
        btnGuideline.BackgroundImageLayout = ImageLayout.Stretch
        btnGuideline.Tag = "UnSelected"
        If ID = 1 Then
            With btnLab
                pnlbtnLab.Dock = DockStyle.Top
                .Tag = "Selected"
                '.ForeColor = Color.Black
                .BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongOrange
                .BackgroundImageLayout = ImageLayout.Stretch
                .BringToFront()
            End With
            trvTriggers.BringToFront()
            FillLabTest()
        ElseIf ID = 2 Then

            With btnReferrals
                pnlbtnReferrals.Dock = DockStyle.Top
                .Tag = "Selected"
                '.ForeColor = Color.Black
                .BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongOrange
                .BackgroundImageLayout = ImageLayout.Stretch
                .BringToFront()
            End With
            trvTriggers.BringToFront()
            FillReferrals()
        ElseIf ID = 3 Then
            With btnRx
                pnlbtnRx.Dock = DockStyle.Top
                .Tag = "Selected"
                '.ForeColor = Color.Black
                .BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongOrange
                .BackgroundImageLayout = ImageLayout.Stretch
                .BringToFront()
            End With
            trvTriggers.BringToFront()
            FillRx()
        ElseIf ID = 4 Then
            With btnRadiologyTest
                pnlbtnRadiologyTest.Dock = DockStyle.Top
                .Tag = "Selected"
                '.ForeColor = Color.Black
                .BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongOrange
                .BackgroundImageLayout = ImageLayout.Stretch
                .BringToFront()
            End With
            trvTriggers.BringToFront()
            FillRadiologyTest()
        ElseIf ID = 5 Then
            With btnGuideline
                pnlbtnGuideline.Dock = DockStyle.Top
                .Tag = "Selected"
                '.ForeColor = Color.Black
                .BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongOrange
                .BackgroundImageLayout = ImageLayout.Stretch
                .BringToFront()
            End With
            trvTriggers.BringToFront()
            fill_guideline()
        End If

    End Sub

    Public Sub FillReferrals()
        Try
            trvTriggers.Nodes.Clear()
            Dim rootnode As myTreeNode = Nothing
            'rootnode = New myTreeNode("Templates", -1)
            'rootnode.ImageIndex = 0
            'rootnode.SelectedImageIndex = 0
            ' trvTriggers.Nodes.Add(rootnode)

            Dim newNode As New TreeNode
            Dim objMyTreeView As myTreeNode
            Dim objTemplateGallery As New clsTemplateGallery
            Dim objCategory As myTreeNode
            Dim objTemplate As myTreeNode
            Dim dvTemplate As DataView
            Dim dt_temp As DataTable = objTemplateGallery.GetAllCategory

            Dim j As Integer
            trvTriggers.Nodes.Clear()

            objMyTreeView = New myTreeNode("Referrals", 0)
            objMyTreeView.ImageIndex = 8  '' Category ICon
            objMyTreeView.SelectedImageIndex = 8 '' Category ICon
            trvTriggers.Nodes.Add(objMyTreeView)

            Dim ValueMember As Int64
            Dim DisplayMember As String

            For i As Integer = 0 To dt_temp.Rows.Count - 1
                'Dim ValueMember As Int64
                'Dim DisplayMember As String
                ValueMember = dt_temp.Rows(i)(0)
                DisplayMember = dt_temp.Rows(i)(1)
                If DisplayMember = "Referral Letter" Then
                    objCategory = New myTreeNode(DisplayMember, ValueMember)
                    'objCategory.ImageIndex = 7 '''' Template ICon
                    'objCategory.SelectedImageIndex = 7 '''' Template ICon
                    'objMyTreeView.Nodes.Add(objCategory)

                    dvTemplate = New DataView
                    dvTemplate = objTemplateGallery.GetAllTemplateGallery(ValueMember)
                    For j = 0 To dvTemplate.Table.Rows.Count - 1
                        ''Dim ValueMember As Int64
                        ''Dim DisplayMember As String
                        ValueMember = dvTemplate.Table.Rows(j)(0)
                        DisplayMember = dvTemplate.Table.Rows(j)(1)
                        objTemplate = New myTreeNode(DisplayMember, ValueMember)
                        objTemplate.ImageIndex = 11 '''' Play ICon
                        objTemplate.SelectedImageIndex = 11 '''' Play ICon
                        'objMyTreeView.Nodes.Add(objTemplate)
                        'objCategory.Nodes.Add(objTemplate)
                        'objCategory.EnsureVisible()
                        'objCategory.ExpandAll()
                        objMyTreeView.Nodes.Add(objTemplate)

                    Next
                    objMyTreeView.ExpandAll()
                End If
            Next
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub FillRx()
        trvTriggers.Nodes.Clear()
        Dim rootnode As myTreeNode = Nothing
        rootnode = New myTreeNode("Rx", -1)
        rootnode.ImageIndex = 9
        rootnode.SelectedImageIndex = 9
        trvTriggers.Nodes.Add(rootnode)
        Dim oDrugs As gloStream.DiseaseManagement.Supporting.Drugs
        Dim oDM As New gloStream.DiseaseManagement.Common.Criteria
        Dim oNode As myTreeNode
        Try

            oDrugs = oDM.Drugs(txtDrugSearch.Text.Trim)

            With rootnode
                ' .BeginUpdate()

                If Not oDrugs Is Nothing Then
                    For i As Int64 = 1 To oDrugs.Count
                        'oNode = New myTreeNode(oDrugs(i).Name, oDrugs(i).ID)
                        oNode = New myTreeNode
                        With oNode
                            .Text = oDrugs(i).Name
                            .Key = oDrugs(i).ID
                        End With
                        oNode.ImageIndex = 11
                        oNode.SelectedImageIndex = 11
                        .Nodes.Add(oNode)
                        oNode = Nothing
                    Next
                    oDrugs.Dispose()
                    oDrugs = Nothing
                End If
                ' .EndUpdate()
                .ExpandAll()
            End With
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            oDM.Dispose()
            oDM = Nothing
        End Try
    End Sub

    Public Sub FillRadiologyTest()
        Try
            trvTriggers.Nodes.Clear()
            Dim rootnode As myTreeNode = Nothing

            rootnode = New myTreeNode("Orders", -1)
            rootnode.ImageIndex = 7
            rootnode.SelectedImageIndex = 7
            trvTriggers.Nodes.Add(rootnode)
            Dim _C As Integer, _G As Integer, _T As Integer
            Dim oDM As New gloStream.DiseaseManagement.Common.Criteria
            Dim oLabs As gloStream.DiseaseManagement.Supporting.Orders

            oLabs = oDM.Orders
            If Not oLabs Is Nothing Then
                If oLabs.Count > 0 Then

                    'Fill Category
                    For _C = 1 To oLabs.Count

                        'rootnode = New myTreeNode(oLabs(_C).Category, oLabs(_C).ID)
                        'trAssociates.Nodes.Item(0).Nodes.Add(rootnode)
                        For _G = 1 To oLabs.Item(_C).OrderGroups.Count
                            'Dim mychildnode As myTreeNode
                            'mychildnode = New myTreeNode(oLabs.Item(_C).LabGroups(_G).Name, oLabs.Item(_C).LabGroups(_G).ID)
                            'rootnode.Nodes.Add(mychildnode)
                            'Fill Tests Start
                            For _T = 1 To oLabs.Item(_C).OrderGroups(_G).Tests.Count
                                Dim mychildnode_ As myTreeNode
                                mychildnode_ = New myTreeNode(oLabs.Item(_C).OrderGroups(_G).Tests(_T).Description, oLabs.Item(_C).OrderGroups(_G).Tests(_T).ID)
                                ' rootnode.Nodes.Add(mychildnode_)
                                mychildnode_.ImageIndex = 11
                                mychildnode_.SelectedImageIndex = 11
                                trvTriggers.Nodes.Item(0).Nodes.Add(mychildnode_)
                            Next
                        Next
                        'Fill Tests Finish
                    Next ' For _G = 1 To oLabs.Item(_C).LabGroups.Count
                    'Fill Groups & Category
                    trvTriggers.ExpandAll()
                End If
                oLabs.Dispose()
                oLabs = Nothing
            End If
            oDM.Dispose()
            oDM = Nothing
            
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Fill_Orders()

        Dim _C As Integer
        Dim _G As Integer
        Dim _T As Integer


        With c1Labs
            .Rows.Count = 1
            .Rows.Fixed = 1
            .Cols.Count = COL_COUNT
            .Cols.Fixed = 0
            .Rows(.Rows.Count - 1).Height = 22
            .Cols(COL_NAME).Width = .Width - 20
            .Cols(COL_ID).Width = (.Width / 4) * 1
            .Cols(COL_TESTGROUPFLAG).Width = (.Width / 4) * 1
            .Cols(COL_LEVELNO).Width = (.Width / 4) * 1
            .Cols(COL_GROUPNO).Width = (.Width / 4) * 1
            .Cols(COL_MINVALUE).Width = 0
            .Cols(COL_MAXVALUE).Width = 0
            .Cols(COL_IDENTITY).Width = (.Width / 4) * 1

            .SetData(0, COL_NAME, "Tests")
            .SetData(0, COL_MINVALUE, "Min. Value")
            .SetData(0, COL_MAXVALUE, "Max. Value")

            .Tree.Column = COL_NAME
            .Tree.Style = C1.Win.C1FlexGrid.TreeStyleFlags.Simple
            .Tree.Indent = 15
            .Cols(COL_NAME).AllowEditing = False
            .Cols(COL_ID).AllowEditing = False
            .Cols(COL_TESTGROUPFLAG).AllowEditing = False
            .Cols(COL_LEVELNO).AllowEditing = False
            .Cols(COL_GROUPNO).AllowEditing = False
            .Cols(COL_MINVALUE).AllowEditing = True
            .Cols(COL_MAXVALUE).AllowEditing = True
            .Cols(COL_IDENTITY).AllowEditing = False

            .Cols(COL_NAME).Visible = True
            .Cols(COL_ID).Visible = False
            .Cols(COL_TESTGROUPFLAG).Visible = False
            .Cols(COL_LEVELNO).Visible = False
            .Cols(COL_GROUPNO).Visible = False
            .Cols(COL_MINVALUE).Visible = True
            .Cols(COL_MAXVALUE).Visible = True
            .Cols(COL_IDENTITY).Visible = False

            .Cols(COL_NAME).DataType = GetType(String)
            .Cols(COL_ID).DataType = GetType(Long)
            .Cols(COL_TESTGROUPFLAG).DataType = GetType(String)
            .Cols(COL_LEVELNO).DataType = GetType(String)
            .Cols(COL_GROUPNO).DataType = GetType(String)
            .Cols(COL_MINVALUE).DataType = GetType(Double)
            .Cols(COL_MAXVALUE).DataType = GetType(Double)
            .Cols(COL_IDENTITY).DataType = GetType(String)

            Dim csTest As C1.Win.C1FlexGrid.CellStyle '= .Styles.Add("CS_Test")
            'Dim csLastChild As C1.Win.C1FlexGrid.CellStyle '= C1PatientRisk.Styles.Add("csLastChild")

            Try
                If (.Styles.Contains("CS_Test")) Then
                    csTest = .Styles("CS_Test")
                Else
                    csTest = .Styles.Add("CS_Test")
                    csTest.Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Bold) 'IIf(Font.Height = 8.25F, gFont_SMALL_BOLD, IIf(Font.Height = 9.0F, gFont_BOLD, New Font(Font, FontStyle.Bold))) 'New Font(Font, FontStyle.Bold)
                    csTest.ForeColor = Color.Maroon
                    csTest.BackColor = Color.White
                    csTest.Display = C1.Win.C1FlexGrid.DisplayEnum.Stack
                End If
            Catch ex As Exception
                csTest = .Styles.Add("CS_Test")
                csTest.Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Bold) 'IIf(Font.Height = 8.25F, gFont_SMALL_BOLD, IIf(Font.Height = 9.0F, gFont_BOLD, New Font(Font, FontStyle.Bold))) 'New Font(Font, FontStyle.Bold)
                csTest.ForeColor = Color.Maroon
                csTest.BackColor = Color.White
                csTest.Display = C1.Win.C1FlexGrid.DisplayEnum.Stack
            End Try
          


            Dim oCV As New gloStream.CardioVascular.CardioVascular
            Dim oOrders As Collection

            oOrders = oCV.GetOrders
            If Not oOrders Is Nothing Then
                If oOrders.Count > 0 Then

                    'Fill Category
                    For _C = 1 To oOrders.Count
                        Dim categoryId As Int64 = CType(oOrders(_C), myList).ID
                        Dim categoryName As String = CType(oOrders(_C), myList).Description


                        .Rows.Add()
                        With .Rows(.Rows.Count - 1)
                            .ImageAndText = True
                            .Height = 24
                            .IsNode = True
                            '//.Style = FillControl.Styles("CS_Category")
                            .Node.Level = 0
                            .Node.Data = categoryName
                            .Node.Key = categoryId
                        End With
                        .SetData(.Rows.Count - 1, COL_ID, categoryId)
                        .SetData(.Rows.Count - 1, COL_TESTGROUPFLAG, _LAB_Category)
                        .SetData(.Rows.Count - 1, COL_LEVELNO, 0)
                        .SetData(.Rows.Count - 1, COL_GROUPNO, 0)
                        .SetData(.Rows.Count - 1, COL_IDENTITY, _LAB_Category & categoryId)
                        .Rows(.Rows.Count - 1).AllowEditing = False
                    Next

                    'Fill Groups & Category

                    For _C = 1 To oOrders.Count
                        Dim categoryId As Int64 = CType(oOrders(_C), myList).ID
                        Dim categoryName As String = CType(oOrders(_C), myList).Description

                        For _G = 1 To CType(oOrders.Item(_C), myList).MyCollection.Count
                            Dim groupId As Int64 = CType(CType(oOrders(_C), myList).MyCollection.Item(_G), myList).ID
                            Dim groupName As String = CType(CType(oOrders(_C), myList).MyCollection.Item(_G), myList).Description

                            Dim oFindNode As C1.Win.C1FlexGrid.Node
                            oFindNode = GetC1Node(_LAB_Category & categoryId)

                            If Not oFindNode Is Nothing Then
                                oFindNode.AddNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild, groupName)
                                '//.Style = FillControl.Styles("CS_Category")
                                Dim _tmpRow As Integer = oFindNode.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild).Row.Index
                                If Not _tmpRow = -1 Then
                                    .Rows(_tmpRow).ImageAndText = True
                                    .Rows(_tmpRow).Height = 24
                                    .SetData(_tmpRow, COL_ID, groupId)
                                    .SetData(_tmpRow, COL_TESTGROUPFLAG, _LAB_Group)
                                    .SetData(_tmpRow, COL_LEVELNO, 0)
                                    .SetData(_tmpRow, COL_GROUPNO, 0)
                                    .SetData(_tmpRow, COL_IDENTITY, _LAB_Group & groupId)
                                    .Rows(_tmpRow).AllowEditing = False
                                    _tmpRow = -1
                                End If
                                oFindNode = Nothing

                                'Fill Tests Start
                                For _T = 1 To CType(CType(oOrders.Item(_C), myList).MyCollection.Item(_G), myList).MyCollection.Count
                                    Dim testID As Int64 = CType(CType(CType(oOrders(_C), myList).MyCollection.Item(_G), myList).MyCollection.Item(_T), myList).ID
                                    Dim testName As String = CType(CType(CType(oOrders(_C), myList).MyCollection.Item(_G), myList).MyCollection.Item(_T), myList).Description


                                    Dim oFindNodeTest As C1.Win.C1FlexGrid.Node
                                    oFindNodeTest = GetC1Node(_LAB_Group & groupId)

                                    If Not oFindNodeTest Is Nothing Then
                                        oFindNodeTest.AddNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild, testName)
                                        '//.Style = FillControl.Styles("CS_Category")
                                        Dim _tmpRowTest As Integer = oFindNodeTest.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild).Row.Index
                                        If Not _tmpRowTest = -1 Then
                                            .Rows(_tmpRowTest).ImageAndText = True
                                            .Rows(_tmpRowTest).Height = 24
                                            .SetData(_tmpRowTest, COL_ID, testID)
                                            .SetData(_tmpRowTest, COL_TESTGROUPFLAG, _LAB_Test)
                                            .SetData(_tmpRowTest, COL_LEVELNO, 0)
                                            .SetData(_tmpRowTest, COL_GROUPNO, groupId)
                                            .SetData(_tmpRowTest, COL_IDENTITY, _LAB_Test & testID)
                                            .SetCellCheck(_tmpRowTest, COL_NAME, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
                                            .Rows(_tmpRowTest).AllowEditing = True
                                            .Cols(COL_NAME).AllowEditing = True
                                            _tmpRowTest = -1
                                        End If
                                        oFindNodeTest = Nothing
                                    End If
                                Next
                                'Fill Tests Finish
                            End If
                        Next ' For _G = 1 To oLabs.Item(_C).LabGroups.Count
                    Next

                End If
            End If
            oCV = Nothing
            oOrders = Nothing

        End With

    End Sub

    'code for search in the tree node

    Private Sub txtSearch_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged
        Try
            ''check for text to be search
            'If Trim(txtSearch.Text) <> "" Then
            '    ''CurrentParentNode  Variable To Identify CurrentSelected Sub Tree.. 
            '    ''Will Help to search only in current sub tree.. 
            '    ''else will search in 0th tree.
            '    If Not IsNothing(trvHistoryRight.SelectedNode) And IsNothing(CurrentParentNode) Then
            '        CurrentParentNode = New TreeNode
            '        If trvHistoryRight.SelectedNode.Level = 0 Then
            '            CurrentParentNode = trvHistoryRight.SelectedNode
            '        ElseIf trvHistoryRight.SelectedNode.Level = 1 Then
            '            CurrentParentNode = trvHistoryRight.SelectedNode.Parent
            '        End If
            '    End If

            '    If IsNothing(CurrentParentNode) Then
            '        CurrentParentNode = New TreeNode
            '        CurrentParentNode = trvHistoryRight.Nodes(0)
            '    End If

            '    For i As Integer = 0 To trvHistoryRight.GetNodeCount(False) - 1
            '        For Each ParentNode As TreeNode In trvHistoryRight.Nodes
            '            If ParentNode.Text = CurrentParentNode.Text Then
            '                Dim mychildnode As TreeNode
            '                'child node collection
            '                For Each mychildnode In ParentNode.Nodes
            '                    Dim str As String
            '                    str = UCase(Trim(mychildnode.Text))
            '                    If str.Contains(UCase(Trim(txtSearch.Text))) Then
            '                        '***********code added by sagar to take the selected node to top on 6 july 2007
            '                        trvHistoryRight.SelectedNode = trvHistoryRight.Nodes(trvHistoryRight.Nodes.Count - 1) 'trvHistoryRight.SelectedNode.LastNode
            '                        '***********
            '                        trvHistoryRight.SelectedNode = mychildnode
            '                        txtSearch.Focus()
            '                        Exit Sub
            '                    End If
            '                    ''Commented by sudhir 20090223
            '                    'If Mid(str, 1, Len(Trim(txtSearch.Text))) = UCase(Trim(txtSearch.Text)) Then
            '                    '    '***********code added by sagar to take the selected node to top on 6 july 2007
            '                    '    trvHistoryRight.SelectedNode = trvHistoryRight.Nodes(trvHistoryRight.Nodes.Count - 1) 'trvHistoryRight.SelectedNode.LastNode
            '                    '    '***********
            '                    '    trvHistoryRight.SelectedNode = mychildnode
            '                    '    txtSearch.Focus()
            '                    '    Exit Sub
            '                    'End If
            '                Next
            '            End If
            '        Next
            '    Next
            'End If

            If txtSearch.Text.Trim <> "" Then
                For Each oNode As TreeNode In trvHistoryRight.Nodes
                    Dim NodeText As String = UCase(oNode.Text)
                    If NodeText.Contains(UCase(txtSearch.Text.Trim)) Then
                        trvHistoryRight.SelectedNode = oNode
                        txtSearch.Focus()
                        Exit Sub
                    Else
                        trvHistoryRight.SelectedNode = Nothing
                    End If
                Next
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnDrugs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDrugs.Click
        Try

            Me.Cursor = Cursors.WaitCursor
            ' 'This code is commented by shilpa because currently we are not using tab structure in form
            ''function for display Drugs Tag of the Tag Pages and hide others tags pages
            ''HidenShow(tpDrugs)
            'Code added by dipak 20091003 :fills Drugs
            If (isDrugsLoaded = False) Then
                Fill_Drugs()
                isDrugsLoaded = True
            End If

            'Code added by dipak 20091003

            pnlDrugs.Visible = True
            pnlDrugs.BringToFront()

            btnDrugs.BackgroundImage = Global.gloEMR.My.Resources.Img_LongOrange
            btnDrugs.BackgroundImageLayout = ImageLayout.Stretch
            btnDrugs.Tag = "Selected"

            btnHistory.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
            btnHistory.BackgroundImageLayout = ImageLayout.Stretch
            btnHistory.Tag = "UnSelected"

            btnDemographics.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
            btnDemographics.BackgroundImageLayout = ImageLayout.Stretch
            btnDemographics.Tag = "UnSelected"

            btnRadiology.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
            btnRadiology.BackgroundImageLayout = ImageLayout.Stretch
            btnRadiology.Tag = "UnSelected"

            btnOrders.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
            btnOrders.BackgroundImageLayout = ImageLayout.Stretch
            btnOrders.Tag = "UnSelected"

            btnICD9.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
            btnICD9.BackgroundImageLayout = ImageLayout.Stretch
            btnICD9.Tag = "UnSelected"

            btnCPT.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
            btnCPT.BackgroundImageLayout = ImageLayout.Stretch
            btnCPT.Tag = "UnSelected"

            btnLabs.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
            btnLabs.BackgroundImageLayout = ImageLayout.Stretch
            btnLabs.Tag = "UnSelected"
            'fill_Drugs()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            'To make cursor status Arrow
            Me.Cursor = Cursors.Default
        End Try


    End Sub

    Private Sub btnCPT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCPT.Click
        Try
            ''This code is commented by shilpa because currently we are not using tab structure in form
            ''function for display CPT Tag of the Tag Pages and hide others tags pages
            ''HidenShow(tpCPT)
            pnlCPT.Visible = True
            pnlCPT.BringToFront()

            btnCPT.BackgroundImage = Global.gloEMR.My.Resources.Img_LongOrange
            btnCPT.BackgroundImageLayout = ImageLayout.Stretch
            btnCPT.Tag = "Selected"

            btnDrugs.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
            btnDrugs.BackgroundImageLayout = ImageLayout.Stretch
            btnDrugs.Tag = "UnSelected"

            btnHistory.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
            btnHistory.BackgroundImageLayout = ImageLayout.Stretch
            btnHistory.Tag = "UnSelected"

            btnDemographics.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
            btnDemographics.BackgroundImageLayout = ImageLayout.Stretch
            btnDemographics.Tag = "UnSelected"

            btnRadiology.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
            btnRadiology.BackgroundImageLayout = ImageLayout.Stretch
            btnRadiology.Tag = "UnSelected"

            btnOrders.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
            btnOrders.BackgroundImageLayout = ImageLayout.Stretch
            btnOrders.Tag = "UnSelected"

            btnICD9.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
            btnICD9.BackgroundImageLayout = ImageLayout.Stretch
            btnICD9.Tag = "UnSelected"

            btnLabs.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
            btnLabs.BackgroundImageLayout = ImageLayout.Stretch
            btnLabs.Tag = "UnSelected"
            'fill_Drugs()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnICD9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnICD9.Click
        Try
            ''This code is commented by shilpa because currently we are not using tab structure in form
            ''function for display ICP9 Tag of the Tag Pages and hide others tags pages
            ''HidenShow(tpICD9)
            pnlICD9.Visible = True
            pnlICD9.BringToFront()

            btnICD9.BackgroundImage = Global.gloEMR.My.Resources.Img_LongOrange
            btnICD9.BackgroundImageLayout = ImageLayout.Stretch
            btnICD9.Tag = "Selected"

            btnDrugs.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
            btnDrugs.BackgroundImageLayout = ImageLayout.Stretch
            btnDrugs.Tag = "UnSelected"

            btnHistory.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
            btnHistory.BackgroundImageLayout = ImageLayout.Stretch
            btnHistory.Tag = "UnSelected"

            btnDemographics.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
            btnDemographics.BackgroundImageLayout = ImageLayout.Stretch
            btnDemographics.Tag = "UnSelected"

            btnRadiology.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
            btnRadiology.BackgroundImageLayout = ImageLayout.Stretch
            btnRadiology.Tag = "UnSelected"

            btnOrders.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
            btnOrders.BackgroundImageLayout = ImageLayout.Stretch
            btnOrders.Tag = "UnSelected"

            btnLabs.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
            btnLabs.BackgroundImageLayout = ImageLayout.Stretch
            btnLabs.Tag = "UnSelected"

            btnCPT.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
            btnCPT.BackgroundImageLayout = ImageLayout.Stretch
            btnCPT.Tag = "UnSelected"

            'fill_CPTs()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnRadiology_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRadiology.Click
        Try
            ''This code is commented by shilpa because currently we are not using tab structure in form
            ''function for display Labs Tag of the Tag Pages and hide others tags pages
            'HidenShow(tpRadiology)
            'Code added by dipak 20091003 :fills Orders
            'Fill_Orders()
            'end Code added by dipak 20091003
            pnlRadiology.Visible = True
            pnlRadiology.BringToFront()

            btnRadiology.BackgroundImage = Global.gloEMR.My.Resources.Img_LongOrange
            btnRadiology.BackgroundImageLayout = ImageLayout.Stretch
            btnRadiology.Tag = "Selected"

            btnOrders.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
            btnOrders.BackgroundImageLayout = ImageLayout.Stretch
            btnOrders.Tag = "UnSelected"

            btnDemographics.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
            btnDemographics.BackgroundImageLayout = ImageLayout.Stretch
            btnDemographics.Tag = "UnSelected"

            btnHistory.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
            btnHistory.BackgroundImageLayout = ImageLayout.Stretch
            btnHistory.Tag = "UnSelected"

            btnDrugs.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
            btnDrugs.BackgroundImageLayout = ImageLayout.Stretch
            btnDrugs.Tag = "UnSelected"

            btnICD9.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
            btnICD9.BackgroundImageLayout = ImageLayout.Stretch
            btnICD9.Tag = "UnSelected"

            btnCPT.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
            btnCPT.BackgroundImageLayout = ImageLayout.Stretch
            btnCPT.Tag = "UnSelected"

            btnLabs.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
            btnLabs.BackgroundImageLayout = ImageLayout.Stretch
            btnLabs.Tag = "UnSelected"

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ' Me.Close()
        gloWord.WordDialogBoxBackgroundCloser.Close(Me)
    End Sub

    Private Sub txtLabsSearch_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtLabsSearch.TextChanged
        ' search the data from node in Labs tree view
        ' Search(txtLabsSearch, c1Labs)
        Dim strSearch As String
        With txtLabsSearch
            If Trim(.Text) <> "" Then
                strSearch = Replace(.Text, "'", "''")
            Else
                strSearch = ""
            End If
        End With

        With c1Labs
            .Row = .FindRow(strSearch, 1, COL_NAME, False, False, True)
        End With
    End Sub

    Private Sub txtDrugSearch_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDrugSearch.TextChanged
        Try
            If Len(Trim(txtDrugSearch.Text)) <= 1 Then
                ''Code to Fill drugs Starts from Letter in TextBox
                Fill_Drugs()
            Else
                ''''''''''code to select the drug with name greater than one character string
                Dim MyChildNode As TreeNode
                'child node collection
                For Each MyChildNode In trvDrgs.Nodes
                    Dim str As String
                    str = UCase(MyChildNode.Text)
                    If Mid(str, 1, Len(Trim(txtDrugSearch.Text))) = UCase(Trim(txtDrugSearch.Text)) Then
                        trvDrgs.SelectedNode = trvDrgs.Nodes(trvDrgs.Nodes.Count - 1)
                        trvDrgs.SelectedNode = MyChildNode
                        txtDrugSearch.Focus()
                        Exit Sub
                    End If
                Next
            End If
            '''''''''''''''''''''''''''''''''
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtSearch_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtSearch.KeyUp
        Try
            ' for focus select tree view
            If e.KeyCode = Keys.Enter Then
                trvHistoryRight.Select()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub trvHistory_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If trvHistory.GetNodeCount(False) > 0 Then
            If (e.KeyChar = ChrW(13)) Then
                trvHistory.Select()
                'Else
                '    trvSource.SelectedNode = trvSource.Nodes.Item(0)
            End If
        End If
    End Sub

    Private Sub trvHistoryRight_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles trvHistoryRight.KeyPress
        Try
            If e.KeyChar = Chr(13) Then
                trvSelectedHistory.BeginUpdate()
                Dim SelectedHistoryNode As New TreeNode
                Dim oNode As TreeNode

                SelectedHistoryNode = trvHistoryRight.SelectedNode.Clone

                Dim CategoryFound As Boolean = False
                Dim HistoryFound As Boolean = False

                ''Selected Current Criteria
                For Each CategoryNode As TreeNode In trvSelectedHistory.Nodes
                    If CategoryNode.Text = cmbHistoryCategory.Text Then
                        For Each HistoryNode As TreeNode In CategoryNode.Nodes
                            If HistoryNode.Text = SelectedHistoryNode.Text Then
                                HistoryFound = True
                                Exit For
                            End If
                        Next
                        If Not HistoryFound Then
                            CategoryNode.Nodes.Add(SelectedHistoryNode)
                            CategoryNode.Expand()
                            trvSelectedHistory.Sort()
                        End If
                        CategoryFound = True
                        Exit For
                    End If
                Next

                If Not CategoryFound Then
                    oNode = New TreeNode
                    oNode.Text = cmbHistoryCategory.Text
                    oNode.Tag = cmbHistoryCategory.SelectedValue
                    oNode.ImageIndex = 0
                    oNode.SelectedImageIndex = 0
                    oNode.Nodes.Add(SelectedHistoryNode)
                    trvSelectedHistory.Nodes.Add(oNode)
                    trvSelectedHistory.ExpandAll()
                    oNode = Nothing
                    trvSelectedHistory.Sort()
                End If
                ''
            End If
        Catch ex As Exception
        Finally
            trvSelectedHistory.EndUpdate()
        End Try
    End Sub

    Private Sub trvHistoryRight_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles trvHistoryRight.KeyUp
        Try
            ' for back to search textbox.
            If e.KeyCode = Keys.Back Then
                txtSearch.Select()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtICD9Search_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtICD9Search.TextChanged
        Try
            ' search the data from node in ICD9 tree view
            Search(txtICD9Search, trvICD9)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtSearchOrder_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSearchOrder.KeyPress
        Try
            If (e.KeyChar = ChrW(13)) Then
                trvTriggers.Select()
            Else
                trvTriggers.SelectedNode = trvTriggers.Nodes.Item(0)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtSearchOrder_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSearchOrder.TextChanged
        Try
            ' search the data from node in GuidaLines tree view
            'Search(txtSearchOrder, trvTriggers)
            'check for text to be search
            'If Trim(txtSearchOrder.Text) <> "" Then
            '    For i As Integer = 0 To trvTriggers.GetNodeCount(False) - 1
            '        If trvTriggers.Nodes(i).IsExpanded = True Then
            '            Dim mychildnode As TreeNode
            '            'child node collection
            '            For Each mychildnode In trvTriggers.Nodes(i).Nodes
            '                Dim str As String
            '                str = UCase(Trim(mychildnode.Text))
            '                If Mid(str, 1, Len(Trim(txtSearchOrder.Text))) = UCase(Trim(txtSearchOrder.Text)) Then
            '                    trvTriggers.SelectedNode = trvTriggers.Nodes(trvTriggers.Nodes.Count - 1)
            '                    trvTriggers.SelectedNode = mychildnode
            '                    txtSearchOrder.Focus()
            '                    Exit Sub
            '                End If
            '            Next
            '        End If
            '    Next
            'End If

            Try
                ' search the data from node in Drug tree view
                ' Search(txtDrugSearch, trvDrgs)

                '''''''''Code is modified by Anil on 20071106
                '' Fill Drugs

                If txtSearchOrder.Text.Trim.Length <= 1 Then
                    ''''To get the drugs with first character in search textbox
                    Dim oDM As New gloStream.DiseaseManagement.Common.Criteria
                    oDM.Drugs(txtSearchOrder.Text.Trim.ToLower)
                   
                    fill_Drugs_Trigger()
                    oDM.Dispose()
                    oDM = Nothing
                Else
                    ''''''''''code to select the drug with name greater than one character string
                    Dim mychildnode As TreeNode
                    'child node collection
                    For Each mychildnode In trvTriggers.Nodes.Item(0).Nodes
                        Dim str As String
                        str = UCase(mychildnode.Text)
                        If Mid(str, 1, Len(Trim(txtSearchOrder.Text))) = UCase(Trim(txtSearchOrder.Text)) Then
                            trvTriggers.SelectedNode = trvTriggers.Nodes.Item(0).Nodes(trvTriggers.Nodes.Item(0).Nodes.Count - 1)
                            trvTriggers.SelectedNode = mychildnode
                            txtSearchOrder.Focus()
                            Exit Sub
                        End If
                    Next
                End If
                '''''''''''''''''''''''''''''''''
            Catch ex As Exception
                MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try

            Try
                If Trim(txtSearchOrder.Text) <> "" Then
                    If trvTriggers.Nodes(0).GetNodeCount(False) > 0 Then
                        Dim mychildnode As TreeNode
                        'child node collection

                        For Each mychildnode In trvTriggers.Nodes(0).Nodes
                            Dim str As String
                            str = UCase(Trim(mychildnode.Text))
                            If Mid(str, 1, Len(Trim(txtSearchOrder.Text))) = UCase(Trim(txtSearchOrder.Text)) Then
                                If Not IsNothing(trvTriggers.SelectedNode) Then
                                    If Not IsNothing(trvTriggers.SelectedNode.LastNode) Then
                                        trvTriggers.SelectedNode = trvTriggers.SelectedNode.LastNode
                                    End If
                                End If

                                trvTriggers.SelectedNode = mychildnode
                                'trvCategory.HideSelection = False
                                txtSearchOrder.Focus()
                                Exit Sub
                            Else
                                'trvCategory.HideSelection = True
                            End If
                        Next
                    End If
                End If
            Catch ex As Exception
                MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try


        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtDrugSearch_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtDrugSearch.KeyUp
        Try
            ' for focus select tree view
            If e.KeyCode = Keys.Enter Then
                trvDrgs.Select()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub trvDrgs_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles trvDrgs.KeyUp
        Try
            ' for back to search textbox.
            If e.KeyCode = Keys.Back Then
                txtDrugSearch.Select()
            ElseIf e.KeyCode = Keys.Enter Or e.KeyCode = Keys.Space Then
                ''BELOW CODE COPIED FROM trvDrgs_NodeMouseDoubleClick()
                Try
                    Dim myDrugNode As New TreeNode
                    Dim Ispresent As Boolean = False
                    myDrugNode = trvDrgs.SelectedNode.Clone()
                    For Each myDNode As TreeNode In trvSelectedDrugs.Nodes
                        If myDNode.Tag = myDrugNode.Tag Then
                            Ispresent = True
                            Exit For
                        End If
                    Next
                    If Ispresent = False Then
                        trvSelectedDrugs.Nodes.Add(myDrugNode)
                        myDrugNode.ImageIndex = 0
                        myDrugNode.SelectedImageIndex = 0
                    End If
                    trvSelectedDrugs.ExpandAll()
                Catch ex As Exception
                    MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtCPTsearch_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCPTsearch.KeyUp
        Try
            ' for focus select tree view
            If e.KeyCode = Keys.Enter Then
                trvCPT.Select()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub trvCPT_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles trvCPT.KeyUp
        Try
            ' for back to search textbox.
            If e.KeyCode = Keys.Back Then
                txtCPTsearch.Select()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtCPTsearch_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCPTsearch.TextChanged
        Try
            ' search the data from node in CPT tree view
            Search(txtCPTsearch, trvCPT)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtLabsSearch_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtLabsSearch.KeyUp
        Try
            ' for focus select tree view
            If e.KeyCode = Keys.Enter Then
                c1Labs.Select()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub trvLabs_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        Try
            ' for back to search textbox.
            If e.KeyCode = Keys.Back Then
                txtLabsSearch.Select()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtICD9Search_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtICD9Search.KeyUp
        Try
            ' for focus select tree view
            If e.KeyCode = Keys.Enter Then
                trvICD9.Select()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub trvICD9_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles trvICD9.KeyUp
        Try
            ' for back to search textbox.
            If e.KeyCode = Keys.Back Then
                txtICD9Search.Select()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub trvGuideLineLeft_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        Try
            ' for back to search textbox.
            If e.KeyCode = Keys.Back Then
                txtSearchOrder.Select()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Search(ByVal searchtextbox As TextBox, ByVal searchtreeview As TreeView)
        Try
            'check for text to be search
            If Trim(searchtextbox.Text) <> "" Then
                If (IsNothing(searchtreeview) = False) Then
                    If searchtreeview.GetNodeCount(False) > 0 Then
                        Dim mychildnode As TreeNode
                        'child node collection
                        For i As Integer = 0 To searchtreeview.Nodes.Count - 1
                            For Each mychildnode In searchtreeview.Nodes.Item(i).Nodes
                                Dim str As String
                                str = UCase(Trim(mychildnode.Text))
                                If Mid(str, 1, Len(Trim(searchtextbox.Text))) = UCase(Trim(searchtextbox.Text)) Then
                                    searchtreeview.SelectedNode = searchtreeview.Nodes(searchtreeview.Nodes.Count - 1)
                                    searchtreeview.SelectedNode = mychildnode
                                    searchtextbox.Focus()
                                    Exit Sub
                                End If
                            Next
                        Next
                    End If
                End If

            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Fill_ICD9s()
        Dim oICD9s As gloStream.DiseaseManagement.Supporting.ICD9s
        Dim oDM As New gloStream.DiseaseManagement.Common.Criteria

        Dim oNode As TreeNode

        oICD9s = oDM.ICD9s

        With trvICD9
            .Nodes.Clear()
            If Not oICD9s Is Nothing Then
                For i As Int16 = 1 To oICD9s.Count
                    oNode = New TreeNode
                    With oNode
                        .Text = oICD9s(i).Code & " " & oICD9s(i).Name
                        .Tag = oICD9s(i).ID
                    End With
                    .Nodes.Add(oNode)
                    oNode = Nothing
                Next
                oICD9s.Dispose()
                oICD9s = Nothing
            End If
        End With
        oDM.Dispose()
        oDM = Nothing
    End Sub

    Private Sub Fill_Drugs()


        ''Sandip Darade  20091015
        ''above code commented to replace it with code below to implement the changes done on  treeviev control for drugs 
        Try
            Dim strsearch As String = ""
            Dim dt As New DataTable

            If Not IsNothing(GloUC_trvDrugs.txtsearch.Text) Then
                strsearch = GloUC_trvDrugs.txtsearch.Text
            End If

            Dim obj As New ClsICD9AssociationDBLayer

            dt = obj.FillControls(0, strsearch)

            If Not IsNothing(dt) Then

                GloUC_trvDrugs.Clear()
                GloUC_trvDrugs.DataSource = dt
                GloUC_trvDrugs.IsDrug = True
                GloUC_trvDrugs.DrugFlag = 16 ''For all drugs 
                GloUC_trvDrugs.ValueMember = dt.Columns("DrugsID").ColumnName
                GloUC_trvDrugs.DescriptionMember = dt.Columns("Dosage").ColumnName
                GloUC_trvDrugs.CodeMember = dt.Columns("DrugName").ColumnName
                GloUC_trvDrugs.DrugFormMember = dt.Columns("DrugForm").ColumnName
                GloUC_trvDrugs.RouteMember = Convert.ToString(dt.Columns("sRoute").ColumnName)
                GloUC_trvDrugs.NDCCodeMember = Convert.ToString(dt.Columns(10).ColumnName)
                GloUC_trvDrugs.IsNarcoticsMember = dt.Columns("nIsNarcotics").ColumnName
                GloUC_trvDrugs.FrequencyMember = dt.Columns("sFrequency").ColumnName
                GloUC_trvDrugs.DurationMember = dt.Columns("sDuration").ColumnName
                GloUC_trvDrugs.DrugQtyQualifierMember = dt.Columns("sDrugQtyQualifier").ColumnName
                GloUC_trvDrugs.Search = gloUserControlLibrary.gloUC_TreeView.enumSearchType.Simple
                'Display Type Changed by Mayuri:20091008
                'To display drugs in form of DrugName and Drug Form
                GloUC_trvDrugs.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Code_Description_DrugForm
                GloUC_trvDrugs.FillTreeView()
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub fill_Drugs_Trigger()
        Dim oDrugs As gloStream.DiseaseManagement.Supporting.Drugs
        Dim oDM As New gloStream.DiseaseManagement.Common.Criteria
        Dim oNode As myTreeNode
        Try

            oDrugs = oDM.Drugs(txtSearchOrder.Text.Trim)

            With trvTriggers
                .BeginUpdate()
                .Nodes.Clear()
                Dim rootnode As myTreeNode = Nothing
                rootnode = New myTreeNode("Rx", -1)
                rootnode.ImageIndex = 9
                rootnode.SelectedImageIndex = 9
                .Nodes.Add(rootnode)
                If Not oDrugs Is Nothing Then
                    For i As Int64 = 1 To oDrugs.Count
                        oNode = New myTreeNode
                        With oNode
                            .Text = oDrugs(i).Name
                            .Key = oDrugs(i).ID
                            .ImageIndex = 11
                            .SelectedImageIndex = 11
                        End With
                        .Nodes.Item(0).Nodes.Add(oNode)
                        oNode = Nothing
                    Next
                    oDrugs.Dispose()
                    oDrugs = Nothing
                End If
                .EndUpdate()
                .ExpandAll()
            End With
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        oDM.Dispose()
        oDM = Nothing
    End Sub

    Private Sub fill_CPTs()
        Dim oCPTS As gloStream.DiseaseManagement.Supporting.CPTs
        Dim oDm As New gloStream.DiseaseManagement.Common.Criteria
        Dim oNode As TreeNode

        oCPTS = oDm.CPTs

        With trvCPT
            .Nodes.Clear()
            If Not oCPTS Is Nothing Then
                For i As Int16 = 1 To oCPTS.Count
                    oNode = New TreeNode
                    With oNode
                        .Text = oCPTS(i).Name
                        .Tag = oCPTS(i).ID
                    End With
                    .Nodes.Add(oNode)
                    oNode = Nothing
                Next
                oCPTS.Dispose()
                oCPTS = Nothing
            End If

        End With
        oDm.Dispose()
        oDm = Nothing
    End Sub

    Private Sub Fill_Histories(Optional ByVal CategoryID As Int64 = 0, Optional ByVal HistoryCategory As String = "")
        Dim oHistory As New gloStream.CardioVascular.Supporting.OtherDetails
        Dim oCV As New gloStream.CardioVascular.CardioVascular
        Dim dtCategory As New DataTable
        Dim dtCodeHistory As New DataTable
        Dim oNode As TreeNode
        Try
            If HistoryCategory = "" Then
                dtCategory = oCV.GetHistoryCategory
                If Not IsNothing(dtCategory) Then
                    If dtCategory.Rows.Count > 0 Then
                        cmbHistoryCategory.DataSource = dtCategory
                        cmbHistoryCategory.DisplayMember = dtCategory.Columns("sDescription").ColumnName
                        cmbHistoryCategory.ValueMember = dtCategory.Columns("nCategoryID").ColumnName
                        Fill_Histories(CType(dtCategory.Rows(0)("nCategoryID"), Int64), dtCategory.Rows(0)("sDescription").ToString)
                    End If
                End If
            Else
                trvHistoryRight.Nodes.Clear()

                ''Get History Item of Allergy from HistoryMST
                If HistoryCategory.StartsWith("Aller") Then
                    oHistory = oCV.GetHistoryItems(CategoryID, HistoryCategory)
                    If Not oHistory Is Nothing Then
                        For i As Integer = 1 To oHistory.Count
                            oNode = New TreeNode
                            oNode.Text = oHistory(i).ItemName
                            oNode.Tag = oHistory(i).ItemID
                            trvHistoryRight.Nodes.Add(oNode)
                            oNode = Nothing
                        Next
                    End If
                ElseIf gblnCodedHistory Then
                    ''If CodedHistory Enabled, Fill History from ICD9GALLERY except Allergy
                    ''And Other from ICD9GALLERY                        
                    Dim objclsPatientHistory As New clsPatientHistory
                    dtCodeHistory = objclsPatientHistory.GetAllICD9Gallery
                    objclsPatientHistory.Dispose()
                    objclsPatientHistory = Nothing

                    If Not IsNothing(dtCodeHistory) Then
                        For j As Integer = 0 To dtCodeHistory.Rows.Count - 1
                            oNode = New TreeNode
                            oNode.Text = dtCodeHistory.Rows(j)("Column1").ToString
                            oNode.Tag = dtCodeHistory.Rows(j)("ICD9ID").ToString
                            trvHistoryRight.Nodes.Add(oNode)
                            oNode = Nothing
                        Next
                    End If
                Else
                    ''Fill History Items from HistoryMST
                    oHistory = oCV.GetHistoryItems(CategoryID, HistoryCategory)
                    If Not oHistory Is Nothing Then
                        For i As Integer = 1 To oHistory.Count
                            oNode = New TreeNode
                            oNode.Text = oHistory(i).ItemName
                            oNode.Tag = oHistory(i).ItemID
                            trvHistoryRight.Nodes.Add(oNode)
                            oNode = Nothing
                        Next
                    End If
                End If

            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Fill_Histories_1(Optional ByVal CategoryID As Int64 = 0, Optional ByVal HistoryCategory As String = "")
        Dim oHistory As New gloStream.CardioVascular.Supporting.OtherDetails
        Dim oCV As New gloStream.CardioVascular.CardioVascular
        Dim dtCategory As New DataTable
        Dim dtCodeHistory As New DataTable
        ' Dim oNode As TreeNode
        Dim dt As New DataTable
        Try
            If HistoryCategory = "" Then
                dtCategory = oCV.GetHistoryCategory
                If Not IsNothing(dtCategory) Then
                    If dtCategory.Rows.Count > 0 Then
                        cmbHistoryCategory.DataSource = dtCategory
                        cmbHistoryCategory.DisplayMember = dtCategory.Columns("sDescription").ColumnName
                        cmbHistoryCategory.ValueMember = dtCategory.Columns("nCategoryID").ColumnName
                        Fill_Histories(CType(dtCategory.Rows(0)("nCategoryID"), Int64), dtCategory.Rows(0)("sDescription").ToString)
                    End If
                End If
            Else
                trvHistoryRight.Nodes.Clear()

                ''Get History Item of Allergy from HistoryMST
                If HistoryCategory.StartsWith("Aller") Then
                    dt = oCV.GetHistoryItemsTable(CategoryID, HistoryCategory)
                    If Not dt Is Nothing Then
                        GloUC_trvHistory.DataSource = dt
                        GloUC_trvHistory.CodeMember = Convert.ToString(dt.Columns(1).ColumnName)
                        GloUC_trvHistory.ValueMember = Convert.ToString(dt.Columns(0).ColumnName)
                        GloUC_trvHistory.DescriptionMember = Convert.ToString(dt.Columns(1).ColumnName)
                        GloUC_trvHistory.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Descripation

                        GloUC_trvHistory.FillTreeView()

                    End If

                ElseIf gblnCodedHistory Then
                    ''If CodedHistory Enabled, Fill History from ICD9GALLERY except Allergy
                    ''And Other from ICD9GALLERY                        
                    Dim objclsPatientHistory As New clsPatientHistory
                    dtCodeHistory = objclsPatientHistory.GetAllICD9Gallery
                    objclsPatientHistory.Dispose()
                    objclsPatientHistory = Nothing
                    If Not IsNothing(dtCodeHistory) Then
                        GloUC_trvHistory.DataSource = dtCodeHistory
                        GloUC_trvHistory.CodeMember = Convert.ToString(dtCodeHistory.Columns(1).ColumnName)
                        GloUC_trvHistory.ValueMember = Convert.ToString(dtCodeHistory.Columns(0).ColumnName)
                        GloUC_trvHistory.DescriptionMember = Convert.ToString(dtCodeHistory.Columns(1).ColumnName)
                        GloUC_trvHistory.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Descripation

                        GloUC_trvHistory.FillTreeView()
                    End If
                Else
                    ''Fill History Items from HistoryMST
                    dt = oCV.GetHistoryItemsTable(CategoryID, HistoryCategory)
                    If Not dt Is Nothing Then
                        GloUC_trvHistory.DataSource = dt
                        GloUC_trvHistory.CodeMember = Convert.ToString(dt.Columns(1).ColumnName)
                        GloUC_trvHistory.ValueMember = Convert.ToString(dt.Columns(0).ColumnName)
                        GloUC_trvHistory.DescriptionMember = Convert.ToString(dt.Columns(1).ColumnName)
                        GloUC_trvHistory.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Descripation

                        GloUC_trvHistory.FillTreeView()

                    End If
                End If

            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try


    End Sub

    Private Sub fill_guideline()
        Try
            trvTriggers.Nodes.Clear()
            Dim rootnode As myTreeNode = Nothing
            rootnode = New myTreeNode("Guidelines", -1)
            rootnode.ImageIndex = 8
            rootnode.SelectedImageIndex = 8
            trvTriggers.Nodes.Add(rootnode)
            'This code is commented by shilpa 
            Dim oGuideLines As gloStream.DiseaseManagement.Supporting.ItemDetails
            '  Dim oCategories As New gloStream.DiseaseManagement.Common.GuidelinesType
            Dim oDm As New gloStream.DiseaseManagement.Common.Guidelines

            Dim oNode As myTreeNode
            '    oCategories = oDm.Categories

            'fill category of history

            '.Nodes.Clear()

            '  If Not oCategories Is Nothing Then
            'oNode = New TreeNode
            'With oNode
            '    '.Text = oCategories.PatientEducation
            '    .Text = gloStream.DiseaseManagement.Common.GuidelinesType.PatientEducation
            '    .ImageIndex = 0
            '    .SelectedImageIndex = 0
            'End With
            '.Nodes.Add(oNode)
            '' oNode = Nothing

            'oNode = New TreeNode
            'With oNode
            '    '.Text = oCategories.PatientEducation
            '    .Text = gloStream.DiseaseManagement.Common.GuidelinesType.PatientEducation
            '    .ImageIndex = 0
            '    .SelectedImageIndex = 0
            'End With
            '.Nodes.Add(oNode)
            ''  oNode = Nothing

            'oNode = New TreeNode
            'With oNode
            '    '.Text = oCategories.WellnessGuidelines
            '    .Text = gloStream.DiseaseManagement.Common.GuidelinesType.WellnessGuidelines
            '    .ImageIndex = 0
            '    .SelectedImageIndex = 0
            'End With
            '.Nodes.Add(oNode)
            '.ExpandAll()
            'oNode = Nothing


            With trvTriggers
                'For j As Integer = 0 To .Nodes.Item(0).GetNodeCount(False) - 1

                ' oGuideLines = New gloStream.DiseaseManagement.Supporting.ItemDetails
                oGuideLines = oDm.Guidelines(gloStream.DiseaseManagement.Common.GuidelinesType.PatientEducation)

                For i As Int16 = 1 To oGuideLines.Count
                    oNode = New myTreeNode
                    oNode.Text = oGuideLines(i).Description
                    oNode.Tag = oGuideLines(i).ID
                    'With oNode
                    '    .Text = oGideLines(i).Description
                    '    .Tag = oGideLines(i).ID
                    'End With
                    oNode.ImageIndex = 11
                    oNode.SelectedImageIndex = 11
                    .Nodes.Item(0).Nodes.Add(oNode)
                    oNode = Nothing
                Next
                oGuideLines.Dispose()
                oGuideLines = Nothing

                '               oGuideLines = New gloStream.DiseaseManagement.Supporting.ItemDetails
                oGuideLines = oDm.Guidelines(gloStream.DiseaseManagement.Common.GuidelinesType.WellnessGuidelines)

                For i As Int16 = 1 To oGuideLines.Count
                    oNode = New myTreeNode
                    oNode.Text = oGuideLines(i).Description
                    oNode.Tag = oGuideLines(i).ID
                    'With oNode
                    '    .Text = oGideLines(i).Description
                    '    .Tag = oGideLines(i).ID
                    'End With
                    oNode.ImageIndex = 11
                    oNode.SelectedImageIndex = 11
                    .Nodes.Item(0).Nodes.Add(oNode)
                    oNode = Nothing
                Next
                oGuideLines.Dispose()
                oGuideLines = Nothing

                '              oGuideLines = New gloStream.DiseaseManagement.Supporting.ItemDetails
                oGuideLines = oDm.Guidelines(gloStream.DiseaseManagement.Common.GuidelinesType.PreventiveServices)

                For i As Int16 = 1 To oGuideLines.Count
                    oNode = New myTreeNode
                    oNode.Text = oGuideLines(i).Description
                    oNode.Tag = oGuideLines(i).ID
                    'With oNode
                    '    .Text = oGideLines(i).Description
                    '    .Tag = oGideLines(i).ID
                    'End With
                    oNode.ImageIndex = 11
                    oNode.SelectedImageIndex = 11
                    .Nodes.Item(0).Nodes.Add(oNode)
                    oNode = Nothing
                Next
                oGuideLines.Dispose()
                oGuideLines = Nothing

                trvTriggers.ExpandAll()
                ' Next
            End With

            oDm = Nothing
            ' End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub Fill_Age()
        Dim oCollection As Collection
        Dim oCV As New gloStream.CardioVascular.CardioVascular

        oCollection = oCV.GetAge

        cmbAgeMin.Items.Clear()
        cmbAgeMax.Items.Clear()

        For i As Int16 = 1 To oCollection.Count - 1      '''''To resolve bug no 438 "-1" is added by Anil on 29/10/2007.
            cmbAgeMin.Items.Add(oCollection(i))
            cmbAgeMax.Items.Add(oCollection(i))
        Next

    End Sub

    Private Sub Fill_Gender()
        Dim oCollection As New Collection
        Dim oCV As New gloStream.CardioVascular.CardioVascular

        oCollection = oCV.GetGender

        cmbGender.Items.Clear()
        For i As Int16 = 1 To oCollection.Count
            cmbGender.Items.Add(oCollection(i))
        Next
    End Sub

    'Private Sub Fill_Orders()

    '    With c1Labs
    '        .Rows.Count = 1
    '        .Rows.Fixed = 1
    '        .Cols.Count = COL_COUNT
    '        .Cols.Fixed = 0
    '        .Rows(.Rows.Count - 1).Height = 22

    '        .Cols(COL_NAME).Width = ((.Width / 4) * 2) - 20
    '        .Cols(COL_ID).Width = (.Width / 4) * 1
    '        .Cols(COL_TESTGROUPFLAG).Width = (.Width / 4) * 1
    '        .Cols(COL_LEVELNO).Width = (.Width / 4) * 1
    '        .Cols(COL_GROUPNO).Width = (.Width / 4) * 1
    '        .Cols(COL_MINVALUE).Width = (.Width / 4) * 1
    '        .Cols(COL_MAXVALUE).Width = (.Width / 4) * 1
    '        .Cols(COL_IDENTITY).Width = (.Width / 4) * 1

    '        .SetData(0, COL_NAME, "Tests")
    '        .SetData(0, COL_MINVALUE, "Min. Value")
    '        .SetData(0, COL_MAXVALUE, "Max. Value")

    '        .Tree.Column = COL_NAME
    '        .Tree.Style = C1.Win.C1FlexGrid.TreeStyleFlags.Simple
    '        .Tree.Indent = 15

    '        .Cols(COL_NAME).AllowEditing = False
    '        .Cols(COL_ID).AllowEditing = False
    '        .Cols(COL_TESTGROUPFLAG).AllowEditing = False
    '        .Cols(COL_LEVELNO).AllowEditing = False
    '        .Cols(COL_GROUPNO).AllowEditing = False
    '        .Cols(COL_MINVALUE).AllowEditing = True
    '        .Cols(COL_MAXVALUE).AllowEditing = True
    '        .Cols(COL_IDENTITY).AllowEditing = False

    '        .Cols(COL_NAME).Visible = True
    '        .Cols(COL_ID).Visible = False
    '        .Cols(COL_TESTGROUPFLAG).Visible = False
    '        .Cols(COL_LEVELNO).Visible = False
    '        .Cols(COL_GROUPNO).Visible = False
    '        .Cols(COL_MINVALUE).Visible = True
    '        .Cols(COL_MAXVALUE).Visible = True
    '        .Cols(COL_IDENTITY).Visible = False

    '        .Cols(COL_NAME).DataType = GetType(String)
    '        .Cols(COL_ID).DataType = GetType(Long)
    '        .Cols(COL_TESTGROUPFLAG).DataType = GetType(String)
    '        .Cols(COL_LEVELNO).DataType = GetType(String)
    '        .Cols(COL_GROUPNO).DataType = GetType(String)
    '        .Cols(COL_MINVALUE).DataType = GetType(Double)
    '        .Cols(COL_MAXVALUE).DataType = GetType(Double)
    '        .Cols(COL_IDENTITY).DataType = GetType(String)

    '        Dim csTest As C1.Win.C1FlexGrid.CellStyle = .Styles.Add("CS_Test")

    '        With csTest
    '            .Font = New Font(Font, FontStyle.Bold)
    '            .ForeColor = Color.Maroon
    '            .BackColor = Color.White
    '            .Display = C1.Win.C1FlexGrid.DisplayEnum.Stack
    '        End With

    '        Dim oCV As New gloStream.CardioVascular.CardioVascular
    '        Dim oOrders As New gloStream.CardioVascular.Supporting.OtherDetails
    '        Dim dtOrderGroup As New DataTable


    '        ''Fill TestGroup First.
    '        dtOrderGroup = oCV.GetOrderGroups

    '        If Not IsNothing(dtOrderGroup) Then
    '            For intG As Integer = 0 To dtOrderGroup.Rows.Count - 1
    '                .Rows.Add()
    '                With .Rows(.Rows.Count - 1)
    '                    .ImageAndText = True
    '                    .Height = 24
    '                    .IsNode = True
    '                    .Node.Level = 0
    '                    .Node.Data = dtOrderGroup.Rows(intG)("GroupName")
    '                    .Node.Key = dtOrderGroup.Rows(intG)("GroupID")
    '                End With
    '                .SetData(.Rows.Count - 1, COL_ID, dtOrderGroup.Rows(intG)("GroupID"))
    '                .SetData(.Rows.Count - 1, COL_TESTGROUPFLAG, _LAB_Group)
    '                .SetData(.Rows.Count - 1, COL_LEVELNO, 0)
    '                .SetData(.Rows.Count - 1, COL_GROUPNO, 0)
    '                .SetData(.Rows.Count - 1, COL_IDENTITY, dtOrderGroup.Rows(intG)("GroupName"))
    '                .Rows(.Rows.Count - 1).AllowEditing = False
    '            Next
    '        End If


    '        oOrders = oCV.GetOrdersOld
    '        If Not IsNothing(oOrders) Then
    '            For intT As Integer = 1 To oOrders.Count

    '                Dim ParentRowIndex As Integer = 0

    '                For intG As Integer = 1 To .Rows.Count - 1
    '                    If .Rows(intG).Node.Data = oOrders(intT).CategoryName Then
    '                        ParentRowIndex = intG
    '                        Exit For
    '                    End If
    '                Next

    '                Dim oChild As C1.Win.C1FlexGrid.Node
    '                oChild = .Rows(ParentRowIndex).Node.AddNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild, oOrders(intT).ItemName, oOrders(intT).ItemID, Nothing)
    '                Dim ChildRowIndex As Int32 = oChild.Row.Index

    '                .Rows(ChildRowIndex).ImageAndText = True
    '                .Rows(ChildRowIndex).Height = 24
    '                .SetData(ChildRowIndex, COL_ID, oOrders(intT).ItemID)
    '                .SetData(ChildRowIndex, COL_TESTGROUPFLAG, _LAB_Test)
    '                .SetData(ChildRowIndex, COL_LEVELNO, 0)
    '                .SetData(ChildRowIndex, COL_GROUPNO, oOrders(intT).CategoryName)
    '                .SetData(ChildRowIndex, COL_IDENTITY, 0)
    '                .SetCellCheck(ChildRowIndex, COL_NAME, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
    '                .Rows(ChildRowIndex).AllowEditing = True
    '                .Cols(COL_NAME).AllowEditing = True

    '            Next
    '        End If



    '        oCV = Nothing
    '        oOrders = Nothing


    '    End With

    'End Sub

    Private Sub Fill_Labs()
        Try

            With C1LabResult
                .Rows.Count = 1
                .Rows.Fixed = 1
                .Cols.Fixed = 0

                '''''Set Column Property of flexgrid
                .Cols(COL_TestID).Width = 0
                .SetData(0, COL_TestID, "TestID")
                .Cols(COL_TestID).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

                .Cols(COL_TestName).Width = ((.Width / 3) * 1.4) - 20
                .SetData(0, COL_TestName, "Test-Result")
                .Cols(COL_TestName).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

                .Cols(COL_ResultID).Width = 0
                .SetData(0, COL_ResultID, "ResultID")
                .Cols(COL_ResultID).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

                .Cols(COL_Operator).Width = ((.Width / 3) * 0.7) - 20
                .SetData(0, COL_Operator, "Operator")
                .Cols(COL_Operator).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

                .Cols(COL_ResultValue1).Width = ((.Width / 3) * 0.6) - 20
                .SetData(0, COL_ResultValue1, "Result Value1")
                .Cols(COL_ResultValue1).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

                .Cols(COL_ResultValue2).Width = ((.Width / 3) * 0.6) - 20
                .SetData(0, COL_ResultValue2, "Result Value2")
                .Cols(COL_ResultValue2).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter


                .Cols(COL_IDENTITYModule).Width = 0
                .SetData(0, COL_IDENTITYModule, "Identity")
                .Cols(COL_IDENTITYModule).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter
                .Cols.Count = COL_CountLab

                ''''Set the property for treeview column
                .Tree.Column = COL_TestName
                .Tree.Style = C1.Win.C1FlexGrid.TreeStyleFlags.Simple
                .Tree.Indent = 15



                Dim oCV As New gloStream.CardioVascular.CardioVascular
                Dim oLabs As New gloStream.CardioVascular.Supporting.OtherDetails
                Dim dtLabCategories As DataTable

                dtLabCategories = oCV.GetLabCatagories

                If Not IsNothing(dtLabCategories) Then
                    For intC As Integer = 0 To dtLabCategories.Rows.Count - 1
                        .Rows.Add()
                        With .Rows(.Rows.Count - 1)
                            .ImageAndText = True
                            .Height = 24
                            .IsNode = True
                            .Node.Level = 0
                            .Node.Data = dtLabCategories.Rows(intC)("labtm_Name")
                            .Node.Key = dtLabCategories.Rows(intC)("labtm_id")
                        End With
                        .SetData(.Rows.Count - 1, COL_TestID, dtLabCategories.Rows(intC)("labtm_id"))
                        .SetData(.Rows.Count - 1, COL_IDENTITYModule, "")
                        .Rows(.Rows.Count - 1).AllowEditing = False
                    Next
                End If


                oLabs = oCV.GetLabs

                If Not IsNothing(oLabs) Then
                    For intR As Integer = 1 To oLabs.Count

                        Dim ParentRowIndex As Integer = 0

                        For intT As Integer = 1 To .Rows.Count - 1
                            If .Rows(intT).Node.Data = oLabs(intR).CategoryName Then
                                ParentRowIndex = intT
                                Exit For
                            End If
                        Next

                        Dim oChild As C1.Win.C1FlexGrid.Node
                        oChild = .Rows(ParentRowIndex).Node.AddNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild, oLabs(intR).ItemName, oLabs(intR).ItemID, Nothing)
                        Dim ChildRowIndex As Int32 = oChild.Row.Index

                        .Rows(ChildRowIndex).ImageAndText = True
                        .Rows(ChildRowIndex).Height = 24
                        .Rows(ChildRowIndex).AllowEditing = True
                        Dim strOperator As String
                        Dim cStyle As C1.Win.C1FlexGrid.CellStyle
                        Dim rgOperator As C1.Win.C1FlexGrid.CellRange = .GetCellRange(ChildRowIndex, COL_Operator, ChildRowIndex, COL_Operator)
                        cStyle = .Styles.Add("Operator")
                        Try
                            If (.Styles.Contains("Operator")) Then
                                cStyle = .Styles("Operator")
                            Else
                                cStyle = .Styles.Add("Operator")
                               
                            End If
                        Catch ex As Exception
                            cStyle = .Styles.Add("Operator")
                        
                        End Try
                        strOperator = "Greater Than" & "|" & "Less Than" & "|" & "Between"
                        cStyle.ComboList = strOperator
                        rgOperator.Style = cStyle
                        .SetCellCheck(ChildRowIndex, COL_TestName, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
                        .SetData(ChildRowIndex, COL_TestID, oLabs(intR).CategoryID)
                        .SetData(ChildRowIndex, COL_ResultID, oLabs(intR).ItemID)


                    Next
                End If


                oCV = Nothing
                oLabs = Nothing

            End With

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub fill_race()
        Dim oCollectection As Collection
        Dim oDM As New gloStream.DiseaseManagement.Common.Criteria
        oCollectection = oDM.Race
        cmbRace.Items.Clear()
        For i As Int16 = 1 To oCollectection.Count
            cmbRace.Items.Add(oCollectection(i))
        Next
        oDM.Dispose()
        oDM = Nothing
        oCollectection.Clear()
        oCollectection = Nothing
    End Sub

    Private Sub fill_state()
        Try
            Dim oCollectection1 As Collection
            Dim oDM As New gloStream.DiseaseManagement.Common.Criteria
            oCollectection1 = oDM.State
            cmbState.Items.Clear()
            For i As Integer = 1 To oCollectection1.Count
                cmbState.Items.Add(oCollectection1(i))
            Next
            oDM.Dispose()
            oDM = Nothing
            oCollectection1.Clear()
            oCollectection1 = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub fill_EmpState()
        Try
            Dim oCollectection As Collection
            Dim oDM As New gloStream.DiseaseManagement.Common.Criteria
            oCollectection = oDM.EmploymentStatus
            cmbEmpStatus.Items.Clear()
            For i As Int16 = 1 To oCollectection.Count
                cmbEmpStatus.Items.Add(oCollectection(i))
            Next
            oDM.Dispose()
            oDM = Nothing
            oCollectection.Clear()
            oCollectection = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    'function for the data save into the database for add new or update.

    Private Sub SaveCriteria()
        Try
           

            Dim _result As Boolean = False

            strHeight = txtHeightMin.Text + "'" + txtHeightMinInch.Text + "''"
            strHeightMax = txtHeightMax.Text + "'" + txtHeightMaxInch.Text + "''"

            If txtName.Text = "" Then
                MessageBox.Show("Please enter the Name.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtName.Focus()
                Exit Sub
            End If

            If txtMessage.Text = "" Then
                MessageBox.Show("Please enter the Message.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtMessage.Focus()
                Exit Sub
            End If

            If cmbGender.Text = "" Then
                MessageBox.Show("Please enter Gender.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                cmbGender.Focus()
                Exit Sub
            End If

            If cmbAgeMin.Text = "" Then
                MessageBox.Show("Please enter Minimum Age.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                cmbAgeMin.Focus()
                Exit Sub
            End If

            If cmbAgeMax.Text = "" Then
                MessageBox.Show("Please enter Maximum Age.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                cmbAgeMax.Focus()
                Exit Sub
            End If

            ' check for the enterd Gender for manual data entered
            For i As Integer = 0 To cmbGender.Items.Count - 1
                If cmbGender.Text.ToUpper = cmbGender.Items(i).ToString.ToUpper Then
                    cmbGender.SelectedIndex = i
                    Exit For
                End If
            Next

            ' check for the enterd Employee Age Minimum value for manual data entered
            For i As Integer = 0 To cmbAgeMin.Items.Count - 1
                If cmbAgeMin.Text.ToUpper = cmbAgeMin.Items(i).ToString.ToUpper Then
                    cmbAgeMin.SelectedIndex = i
                    Exit For
                End If
            Next

            ' check for the enterd Employee  Age Maximum value for manual data entered
            For i As Integer = 0 To cmbAgeMax.Items.Count - 1
                If cmbAgeMax.Text.ToUpper = cmbAgeMax.Items(i).ToString.ToUpper Then
                    cmbAgeMax.SelectedIndex = i
                    Exit For
                End If
            Next

            ' minimum maximum Age check
            'Condition changed by Mayuri:20091216-To validate for '0'-Bug ID-#4180
            'If Val(cmbAgeMin.Text) >0 Then
            If Val(cmbAgeMin.Text) >= 0 Then
                If MinMaxValidator(Trim(cmbAgeMin.Text), Trim(cmbAgeMax.Text)) = False Then
                    MessageBox.Show("Please check the Minimum and Maximum values for age.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    cmbAgeMax.Focus()
                    Exit Sub
                End If
            Else
                cmbAgeMin.Text = ""
            End If


            If Val(cmbAgeMin.Text) < 0 Or Val(cmbAgeMin.Text) >= 125 Then
                MessageBox.Show("Please Enter Minimum Age(0 - 124).", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                cmbAgeMin.Focus()
                Exit Sub
            End If

            If Val(cmbAgeMax.Text) < 0 Or Val(cmbAgeMax.Text) >= 125 Then
                MessageBox.Show("Please Enter Maximum Age(0 - 124).", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                cmbAgeMax.Focus()
                Exit Sub
            End If
        
            Dim oCV As New gloStream.CardioVascular.CardioVascular
            If blnModify = False Then
                If oCV.IsExists(txtName.Text.Replace("'", "''")) = True Then
                    MessageBox.Show("Please Enter another Name, Name already exist", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If
            Else
                If UCase(txtName.Text.Trim) <> UCase(m_CriteriaName) Then
                    If oCV.IsExists(txtName.Text.Replace("'", "''")) = True Then
                        MessageBox.Show("Please Enter another Name, Name already exist", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Exit Sub
                    End If
                End If

            End If
            Dim oCriteria As New gloStream.CardioVascular.Supporting.Criteria
            Dim oOtherDetails As New gloStream.CardioVascular.Supporting.OtherDetails
            Dim oOtherDetail As gloStream.CardioVascular.Supporting.OtherDetail
            With oCriteria
                .Name = txtName.Text.Trim
                If Not cmbAgeMin.SelectedItem Is Nothing Then
                    .AgeMinimum = cmbAgeMin.SelectedItem
                End If
                If Not cmbAgeMax.SelectedItem Is Nothing Then
                    .AgeMaximum = cmbAgeMax.SelectedItem
                End If

                If Not cmbGender.SelectedItem Is Nothing Then
                    .Gender = cmbGender.SelectedItem
                End If

                ' changes done by Bipin ON 22/01/2007 for the date format change in to the ft and inch
                .HeightMinimum = strHeight
                .HeightMaximum = strHeightMax
                .BPSittingMinimum = CDbl(Val(txtBPsettingMin.Text.Trim))
                .BPSittingMaximum = CDbl(Val(txtBPsettingMax.Text.Trim))
                .WeightMinimum = CDbl(Val(txtWeightMin.Text.Trim))
                .WeightMaximum = CDbl(Val(txtWeightMax.Text.Trim))
                .BPStandingMinimum = CDbl(Val(txtBPstandingMin.Text.Trim))
                .BPStandingMaximum = CDbl(Val(txtBPstandingMax.Text.Trim))
                .TempratureMinumum = CDbl(Val(txtTemperatureMin.Text.Trim))
                .TempratureMaximum = CDbl(Val(txtTemperatureMax.Text.Trim))
                .PulseMinimum = CDbl(Val(txtPulseMin.Text.Trim))
                .PulseMaximum = CDbl(Val(txtPulseMax.Text.Trim))
                .BMIMinimum = CDbl(Val(txtBMImin.Text.Trim))
                .BMIMaximum = CDbl(Val(txtBMImax.Text.Trim))
                .PulseOXMinimum = CDbl(Val(txtPulseOXmin.Text.Trim))
                .PulseOXMaximum = CDbl(Val(txtPulseOXmax.Text.Trim))
                .DisplayMessage = txtMessage.Text.Trim
            End With

            'data for History
            For i As Integer = 0 To trvSelectedHistory.GetNodeCount(False) - 1
                For j As Integer = 0 To trvSelectedHistory.Nodes(i).GetNodeCount(False) - 1
                    oOtherDetail = New gloStream.CardioVascular.Supporting.OtherDetail
                    'oOtherDetail.ItemID = CType(trvSelectedHistory.Nodes(i).Nodes(j).Tag, Int64)
                    oOtherDetail.CategoryID = CType(trvSelectedHistory.Nodes(i).Tag, Int64)
                    oOtherDetail.ItemName = trvSelectedHistory.Nodes(i).Nodes(j).Text.ToString()
                    oOtherDetail.CategoryName = trvSelectedHistory.Nodes(i).Text.ToString
                    oOtherDetail.DetailType = gloStream.CardioVascular.Supporting.enumDetailType.History.GetHashCode
                    oOtherDetails.Add(oOtherDetail)
                    oOtherDetail = Nothing
                Next
            Next


          
            ''Sandip Darade 20090518
            ''Using tree view control
            For i As Integer = 0 To trvSelectedDrugs.Nodes.Count - 1

                oOtherDetail = New gloStream.CardioVascular.Supporting.OtherDetail
                Dim thisNode As myTreeNode = CType(trvSelectedDrugs.Nodes(i), myTreeNode)
                oOtherDetail.CategoryName = thisNode.DrugName
                'Code Added by Mayuri:20091022
                'After adding drug into selected drugs treeview and if we save data,then DrugName,Dosage and Drug form should save

                'To fix Bug:#4182
                If thisNode.DrugForm.ToString.Trim <> "" Then
                    'If dtdrugs.Rows.Item(i)(3) = "" Then
                    'oOtherDetail1.ItemName = CType(trvSelectedDrugs.Nodes(i), myTreeNode).Dosage & CType(trvSelectedDrugs.Nodes(i), myTreeNode).DrugForm
                    ' Else
                    '  oOtherDetail1.ItemName = CType(trvSelectedDrugs.Nodes(i), myTreeNode).Dosage & " - " & CType(trvSelectedDrugs.Nodes(i), myTreeNode).DrugForm
                    'End If
                    'Else
                    If thisNode.Dosage.Trim <> "" Then
                        oOtherDetail.ItemName = thisNode.Dosage & " - " & thisNode.DrugForm
                    Else
                        oOtherDetail.ItemName = thisNode.DrugForm
                    End If
                ElseIf thisNode.Dosage <> "" Then
                    oOtherDetail.ItemName = thisNode.Dosage    '& " - " & thisNode.DrugForm
                End If

                'End Code Added by Mayuri:20091012


                oOtherDetail.DetailType = gloStream.CardioVascular.Supporting.enumDetailType.Medication.GetHashCode
                oOtherDetails.Add(oOtherDetail)
            Next
            ' ORDER
            For i As Integer = 1 To c1Labs.Rows.Count - 1
                Dim _TestCell As String = c1Labs.GetData(i, COL_IDENTITY) & ""
                If Mid(_TestCell, 1, 1) = "T" Then
                    If c1Labs.GetCellCheck(i, COL_NAME) = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                        oOtherDetail = New gloStream.CardioVascular.Supporting.OtherDetail
                        oOtherDetail.ItemID = c1Labs.GetData(i, COL_ID)
                        oOtherDetail.ItemName = c1Labs.Rows(i).Node.Data
                        oOtherDetail.CategoryName = c1Labs.Rows(i).Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.Root).Data
                        oOtherDetail.OperatorName = c1Labs.Rows(i).Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.Parent).Data
                        oOtherDetail.DetailType = gloStream.CardioVascular.Supporting.enumDetailType.Order.GetHashCode
                        oOtherDetails.Add(oOtherDetail)
                        oOtherDetail = Nothing
                    End If
                End If
            Next


            'For i As Integer = 1 To c1Labs.Rows.Count - 1
            '    If c1Labs.Rows(i).Node.Level <> 0 Then
            '        If c1Labs.GetCellCheck(i, COL_NAME) = C1.Win.C1FlexGrid.CheckEnum.Checked Then
            '            oOtherDetail = New gloStream.CardioVascular.Supporting.OtherDetail
            '            oOtherDetail.ItemID = c1Labs.Rows(i).Node.Key
            '            oOtherDetail.CategoryID = c1Labs.Rows(i).Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.Parent).Key
            '            oOtherDetail.ItemName = c1Labs.Rows(i).Node.Data
            '            oOtherDetail.CategoryName = c1Labs.Rows(i).Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.Parent).Data
            '            If Not IsNothing(c1Labs.GetData(i, COL_MINVALUE)) Then
            '                oOtherDetail.Result1 = c1Labs.GetData(i, COL_MINVALUE)
            '            End If
            '            If Not IsNothing(c1Labs.GetData(i, COL_MAXVALUE)) Then
            '                oOtherDetail.Result2 = c1Labs.GetData(i, COL_MAXVALUE)
            '            End If
            '            oOtherDetail.DetailType = gloStream.CardioVascular.Supporting.enumDetailType.Order.GetHashCode
            '            oOtherDetails.Add(oOtherDetail)
            '            oOtherDetail = Nothing
            '        End If
            '    End If
            'Next

            ''LAB
            For i As Integer = 1 To C1LabResult.Rows.Count - 1
                ' If C1LabResult.Rows(i).Node.Level <> 0 Then
                If C1LabResult.GetCellCheck(i, COL_TestName) = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                    oOtherDetail = New gloStream.CardioVascular.Supporting.OtherDetail
                    oOtherDetail.ItemID = C1LabResult.Rows(i).Node.Key
                    oOtherDetail.CategoryID = C1LabResult.Rows(i).Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.Parent).Key
                    oOtherDetail.ItemName = C1LabResult.Rows(i).Node.Data
                    oOtherDetail.CategoryName = C1LabResult.Rows(i).Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.Parent).Data
                    If Not IsNothing(C1LabResult.GetData(i, COL_Operator)) Then
                        oOtherDetail.OperatorName = C1LabResult.GetData(i, COL_Operator)
                    End If
                    If Not IsNothing(C1LabResult.GetData(i, COL_ResultValue1)) Then
                        oOtherDetail.Result1 = C1LabResult.GetData(i, COL_ResultValue1)
                    End If
                    If Not IsNothing(C1LabResult.GetData(i, COL_ResultValue2)) Then
                        oOtherDetail.Result2 = C1LabResult.GetData(i, COL_ResultValue2)
                    End If
                    oOtherDetail.DetailType = gloStream.CardioVascular.Supporting.enumDetailType.Lab.GetHashCode
                    oOtherDetails.Add(oOtherDetail)
                    oOtherDetail = Nothing
                End If
                'End If
            Next

            ''OtherDetails Remain.
            ''This will Execute while Modify.. 
            ''Store Invisible OtherDetails Back to DataBase.
            If blnModify = True AndAlso oOtherDetailsRemain.Count > 0 Then
                For j As Integer = 1 To oOtherDetailsRemain.Count
                    oOtherDetails.Add(oOtherDetailsRemain(j))
                Next
            End If

            ''Assign All OtherDetails to oCriteria
            oCriteria.OtherDetails = oOtherDetails

            If Not blnModify Then
                _result = oCV.AddCVCriteria(oCriteria)
            Else
                _result = oCV.ModifyCVCriteria(m_CriteriaId, oCriteria)
            End If

            'clear all teh fields for the new entry
            If _result = True Then ' check for result flag
                '   Me.Close()
                gloWord.WordDialogBoxBackgroundCloser.Close(Me)
            End If
            oCriteria.OtherDetails.Dispose()
            oCriteria.OtherDetails = Nothing
            oCriteria.Dispose()
            oCriteria = Nothing
            oCV = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' get data to update field

    Public Sub Fill_EditCriteria(ByVal CriteriaID As Long)
        Dim oCV As New gloStream.CardioVascular.CardioVascular
        Dim oCriteria As gloStream.CardioVascular.Supporting.Criteria
        Dim oOtherDetails As gloStream.CardioVascular.Supporting.OtherDetails

        oCriteria = oCV.GetCriteria(CriteriaID)

        If Not oCriteria Is Nothing Then

            'Demographics & Vitals
            txtName.Text = oCriteria.Name
            txtMessage.Text = oCriteria.DisplayMessage
            cmbAgeMin.Text = oCriteria.AgeMinimum : SetCombiIndex(cmbAgeMin)
            cmbAgeMax.Text = oCriteria.AgeMaximum : SetCombiIndex(cmbAgeMax)
            cmbGender.Text = oCriteria.Gender : SetCombiIndex(cmbGender)
            Dim arrHeight() As String
            arrHeight = GetFtInch(oCriteria.HeightMinimum)
            txtHeightMin.Text = arrHeight(0)
            txtHeightMinInch.Text = arrHeight(1)

            '  Dim arrHeightMax() As String
            arrHeight = GetFtInch(oCriteria.HeightMaximum)
            txtHeightMax.Text = arrHeight(0)
            txtHeightMaxInch.Text = arrHeight(1)

            If oCriteria.BPSittingMinimum = 0.0 Then
                txtBPsettingMin.Text = ""
            Else
                txtBPsettingMin.Text = oCriteria.BPSittingMinimum
            End If

            If oCriteria.BPSittingMaximum = 0.0 Then
                txtBPsettingMax.Text = ""
            Else
                txtBPsettingMax.Text = oCriteria.BPSittingMaximum
            End If

            If oCriteria.WeightMinimum = 0.0 Then
                txtWeightMin.Text = ""
            Else
                txtWeightMin.Text = oCriteria.WeightMinimum
            End If

            If oCriteria.WeightMaximum = 0.0 Then
                txtWeightMax.Text = ""
            Else
                txtWeightMax.Text = oCriteria.WeightMaximum
            End If

            If oCriteria.BPStandingMinimum = 0.0 Then
                txtBPstandingMin.Text = ""
            Else
                txtBPstandingMin.Text = oCriteria.BPStandingMinimum
            End If

            If oCriteria.BPStandingMaximum = 0.0 Then
                txtBPstandingMax.Text = ""
            Else
                txtBPstandingMax.Text = oCriteria.BPStandingMaximum
            End If

            If oCriteria.TempratureMinumum = 0.0 Then
                txtTemperatureMin.Text = ""
            Else
                txtTemperatureMin.Text = oCriteria.TempratureMinumum
            End If

            If oCriteria.TempratureMaximum = 0.0 Then
                txtTemperatureMax.Text = ""
            Else
                txtTemperatureMax.Text = oCriteria.TempratureMaximum
            End If

            If oCriteria.PulseMinimum = 0.0 Then
                txtPulseMin.Text = ""
            Else
                txtPulseMin.Text = oCriteria.PulseMinimum
            End If

            If oCriteria.PulseMaximum = 0.0 Then
                txtPulseMax.Text = ""
            Else
                txtPulseMax.Text = oCriteria.PulseMaximum
            End If
            If oCriteria.BMIMinimum = 0.0 Then
                txtBMImin.Text = ""
            Else
                txtBMImin.Text = oCriteria.BMIMinimum
            End If

            If oCriteria.BMIMaximum = 0.0 Then
                txtBMImax.Text = ""
            Else
                txtBMImax.Text = oCriteria.BMIMaximum
            End If

            If oCriteria.PulseOXMinimum = 0.0 Then
                txtPulseOXmin.Text = ""
            Else
                txtPulseOXmin.Text = oCriteria.PulseOXMinimum
            End If

            If oCriteria.PulseOXMaximum = 0.0 Then
                txtPulseOXmax.Text = ""
            Else
                txtPulseOXmax.Text = oCriteria.PulseOXMaximum
            End If


            ''Fill Other Details

            ''Flag is used to Identify Whether OtherDetails has filled to its location or not.. 
            ''If not filled, We will transfer that detail to oOtherDetailsRemain Object to Save back.
            Dim IsOtherDetailFilled As Boolean = True

            oOtherDetails = oCriteria.OtherDetails
            If Not IsNothing(oOtherDetails) Then
                For i As Integer = 1 To oOtherDetails.Count
                    IsOtherDetailFilled = False

                    Select Case oOtherDetails(i).DetailType

                        Case gloStream.CardioVascular.Supporting.enumDetailType.History ''HISTORY
                            Dim ParentFound As Boolean = False
                            For iParent As Integer = 0 To trvSelectedHistory.Nodes.Count - 1
                                If trvSelectedHistory.Nodes(iParent).Text = oOtherDetails(i).CategoryName Then
                                    Dim onode As TreeNode
                                    Dim HistoryNodefound As Boolean = False
                                    For Each onode In trvSelectedHistory.Nodes(iParent).Nodes
                                        If (onode.Text = oOtherDetails(i).ItemName) Then
                                            HistoryNodefound = True
                                            Exit For
                                        End If
                                    Next

                                    Dim HistoryNode As New TreeNode
                                    HistoryNode.Text = oOtherDetails(i).ItemName
                                    HistoryNode.Tag = oOtherDetails(i).ItemID
                                    If Not HistoryNodefound Then
                                        trvSelectedHistory.Nodes(iParent).Nodes.Add(HistoryNode)
                                    End If
                                    HistoryNode = Nothing
                                    ParentFound = True
                                    Exit For

                                End If
                            Next
                            If Not ParentFound Then
                                Dim CategoryNode As New TreeNode
                                Dim HistoryNode As New TreeNode

                                CategoryNode.Text = oOtherDetails(i).CategoryName
                                CategoryNode.Tag = oOtherDetails(i).CategoryID
                                CategoryNode.ImageIndex = 0
                                CategoryNode.SelectedImageIndex = 0

                                HistoryNode.Text = oOtherDetails(i).ItemName
                                HistoryNode.Tag = oOtherDetails(i).ItemID

                                CategoryNode.Nodes.Add(HistoryNode)
                                trvSelectedHistory.Nodes.Add(CategoryNode)
                                CategoryNode.Expand()

                                HistoryNode = Nothing
                                CategoryNode = Nothing
                            End If

                        Case gloStream.CardioVascular.Supporting.enumDetailType.Lab ''LABS
                            For iLab As Integer = 1 To C1LabResult.Rows.Count - 1
                                If C1LabResult.Rows(iLab).Node.Level = 1 Then
                                    If C1LabResult.Rows(iLab).Node.Data = oOtherDetails(i).ItemName AndAlso C1LabResult.Rows(iLab).Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.Parent).Data = oOtherDetails(i).CategoryName Then
                                        C1LabResult.SetCellCheck(iLab, COL_TestName, C1.Win.C1FlexGrid.CheckEnum.Checked)
                                        C1LabResult.SetData(iLab, COL_Operator, oOtherDetails(i).OperatorName)
                                        C1LabResult.SetData(iLab, COL_ResultValue1, oOtherDetails(i).Result1)
                                        C1LabResult.SetData(iLab, COL_ResultValue2, oOtherDetails(i).Result2)
                                        IsOtherDetailFilled = True
                                        Exit Select
                                    End If
                                End If
                            Next

                        Case gloStream.CardioVascular.Supporting.enumDetailType.Medication  ''DRUGS
                            Dim DrugNode As New myTreeNode
                            DrugNode.Text = oOtherDetails(i).CategoryName
                            DrugNode.DrugName = oOtherDetails(i).CategoryName
                            DrugNode.Dosage = oOtherDetails(i).ItemName
                            trvSelectedDrugs.Nodes.Add(DrugNode)
                            DrugNode = Nothing
                            IsOtherDetailFilled = True

                        Case gloStream.CardioVascular.Supporting.enumDetailType.Order ''ORDERS
                            For iOrder As Integer = 1 To c1Labs.Rows.Count - 1
                                Dim _TestCell As String = c1Labs.GetData(iOrder, COL_IDENTITY) & ""
                                If Mid(_TestCell, 1, 1) = "T" Then
                                    If c1Labs.Rows(iOrder).Node.Data = oOtherDetails(i).ItemName AndAlso c1Labs.Rows(iOrder).Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.Parent).Data = oOtherDetails(i).OperatorName And c1Labs.Rows(iOrder).Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.Root).Data = oOtherDetails(i).CategoryName Then
                                        c1Labs.SetCellCheck(iOrder, COL_NAME, C1.Win.C1FlexGrid.CheckEnum.Checked)
                                        IsOtherDetailFilled = True
                                        Exit Select
                                    End If
                                End If
                            Next

                        Case gloStream.CardioVascular.Supporting.enumDetailType.None

                    End Select

                    ''This will execute when otherdetails(i) has not been filled anywhere.
                    If IsOtherDetailFilled = False Then
                        '   oOtherDetailsRemain.Add(oOtherDetails(i))
                    End If
                Next
            End If
        End If
        If Not oCriteria Is Nothing Then
            If IsNothing(oCriteria.OtherDetails) = False Then
                oCriteria.OtherDetails.Dispose()
                oCriteria.OtherDetails = Nothing
            End If
            oCriteria.Dispose()
        End If
        oCriteria = Nothing
        oCV = Nothing
    End Sub

    Private Sub SetCombiIndex(ByVal ControlCombo As ComboBox)
        For i As Integer = 0 To ControlCombo.Items.Count - 1
            If ControlCombo.Text = ControlCombo.Items(i) Then
                ControlCombo.SelectedIndex = i
                Exit Sub
            End If
        Next
    End Sub

    Private Function GetC1Node(ByVal FindItem As String) As C1.Win.C1FlexGrid.Node
        Dim _Node As C1.Win.C1FlexGrid.Node = Nothing
        Dim _FindRow As Integer = c1Labs.FindRow(FindItem, 0, COL_IDENTITY, True, True, False)
        If _FindRow > 0 Then
            _Node = c1Labs.Rows(_FindRow).Node
        End If
        Return _Node
    End Function

    Private Function GetC1NodeModule(ByVal FindItem As String) As C1.Win.C1FlexGrid.Node
        Dim _Node As C1.Win.C1FlexGrid.Node = Nothing
        Dim _FindRow As Integer = C1LabResult.FindRow(FindItem, 0, COL_IDENTITYModule, True, True, False)
        If _FindRow > 0 Then
            _Node = C1LabResult.Rows(_FindRow).Node
        End If
        Return _Node
    End Function

    Private Sub c1Labs_AfterEdit(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles c1Labs.AfterEdit
        Try
            With c1Labs
                c1Labs.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
                If .GetCellCheck(.Row, COL_NAME) = C1.Win.C1FlexGrid.CheckEnum.Unchecked Then
                    .SetData(.Row, COL_MINVALUE, Nothing)
                    .SetData(.Row, COL_MAXVALUE, Nothing)
                End If
            End With
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtTemperatureMin_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtTemperatureMin.Validating
        Try
            If txtTemperatureMin.Text <> "" Then
                If Val(txtTemperatureMin.Text) > 0 Then
                    If Val(txtTemperatureMin.Text) < 90 Or Val(txtTemperatureMin.Text) > 110 Then
                        MessageBox.Show("Invalid Temperature (between 90-110)", "Disease Management", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        txtTemperatureMin.Focus()
                        Exit Sub
                    End If
                Else
                    txtTemperatureMin.Text = ""
                End If
            End If
            If MinMaxValidator(Trim(txtTemperatureMin.Text), Trim(txtTemperatureMax.Text)) = False Then
                MessageBox.Show("Please check the Minimum and Maximum values for Temperature.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtTemperatureMax.Focus()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtTemperatureMax_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtTemperatureMax.Validating
        Try
            If txtTemperatureMax.Text <> "" Then
                If Val(txtTemperatureMax.Text) > 0 Then
                    If Val(txtTemperatureMax.Text) < 90 Or Val(txtTemperatureMax.Text) > 110 Then
                        MessageBox.Show("Invalid Temperature (between 90-110)", "Disease Management", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        txtTemperatureMax.Focus()
                        Exit Sub
                    End If
                Else
                    txtTemperatureMax.Text = ""
                End If

                If MinMaxValidator(Trim(txtTemperatureMin.Text), Trim(txtTemperatureMax.Text)) = False Then
                    MessageBox.Show("Please check the Minimum and Maximum values for Temperature.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    txtTemperatureMax.Focus()
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtPulseMin_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtPulseMin.Validating
        Try
            If Val(txtPulseMin.Text) > 0 Then
                If MinMaxValidator(Trim(txtPulseMin.Text), Trim(txtPulseMax.Text)) = False Then
                    MessageBox.Show("Please check the Minimum and Maximum values for Pulse.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    txtPulseMax.Focus()
                End If
            Else
                txtPulseMin.Text = ""
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function MinMaxValidator(ByVal MinVal As String, ByVal MaxVal As String) As Boolean
        Dim blnIsValid As Boolean = False

        If MaxVal = "" Then
            Return True
        End If

        If MinVal = "" Then
            Return True
        End If

        'If Val(MinVal) > Val(MaxVal) Or Val(MinVal) = Val(MaxVal) Then
        If Val(MinVal) > Val(MaxVal) Then
            Return False
        Else
            Return True
        End If


    End Function

    Private Sub txtPulseMax_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtPulseMax.Validating
        Try
            If Val(txtPulseMax.Text) > 0 Then
                If MinMaxValidator(Trim(txtPulseMin.Text), Trim(txtPulseMax.Text)) = False Then
                    MessageBox.Show("Please check the Minimum and Maximum values for Pulse.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    txtPulseMax.Focus()
                End If
            Else
                txtPulseMax.Text = ""
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtPulseOXmax_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtPulseOXmax.Validating
        Try
            If Val(Trim(txtPulseOXmax.Text)) > 0 Then
                If MinMaxValidator(Trim(txtPulseOXmin.Text), Trim(txtPulseOXmax.Text)) = False Then
                    MessageBox.Show("Please check the Minimum and Maximum values for Pulse OX.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    txtPulseOXmax.Focus()
                End If
            Else
                txtPulseOXmax.Text = ""
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtPulseOXmin_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtPulseOXmin.Validating
        Try
            If Val(txtPulseOXmin.Text) > 0 Then
                If MinMaxValidator(Trim(txtPulseOXmin.Text), Trim(txtPulseOXmax.Text)) = False Then
                    MessageBox.Show("Please check the Minimum and Maximum values for Pulse OX.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    txtPulseOXmax.Focus()
                End If
            Else
                txtPulseOXmin.Text = ""
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtWeightMax_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtWeightMax.Validating
        Try
            If Val(txtWeightMax.Text) > 0 Then
                If MinMaxValidator(Trim(txtWeightMin.Text), Trim(txtWeightMax.Text)) = False Then
                    MessageBox.Show("Please check the Minimum and Maximum values for Weight.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    txtWeightMax.Focus()
                End If
            Else
                txtWeightMax.Text = ""
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtWeightMin_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtWeightMin.Validating
        Try
            If Val(txtWeightMin.Text) > 0 Then
                If MinMaxValidator(Trim(txtWeightMin.Text), Trim(txtWeightMax.Text)) = False Then
                    MessageBox.Show("Please check the Minimum and Maximum values for Weight.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    txtWeightMax.Focus()
                End If
            Else
                txtWeightMin.Text = ""
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtHeightMin_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtHeightMin.Validating
        Try
            If Val(txtHeightMin.Text) > 0 Then
                'If MinMaxValidator(Trim(txtHeightMin.Text), Trim(txtHeightMax.Text)) = False Then
                If Val(txtHeightMin.Text) > Val(txtHeightMax.Text) Then
                    MessageBox.Show("Please check the Minimum and Maximum values for Height.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    txtHeightMax.Focus()
                End If
            Else
                txtHeightMin.Text = ""
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtHeightMax_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtHeightMax.Validating
        Try
            If Val(txtHeightMax.Text) > 0 Then
                'If MinMaxValidator(Trim(txtHeightMin.Text), Trim(txtHeightMax.Text)) = False Then
                If Val(txtHeightMin.Text) > Val(txtHeightMax.Text) Then
                    MessageBox.Show("Please check the Minimum and Maximum values for Height.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    txtHeightMax.Show()
                End If
            Else
                txtHeightMax.Text = ""
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtBMImin_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtBMImin.Validating
        Try
            If Val(txtBMImin.Text) > 0 Then
                If MinMaxValidator(Trim(txtBMImin.Text), Trim(txtBMImax.Text)) = False Then
                    MessageBox.Show("Please check the Minimum and Maximum values for BMI.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    txtBMImax.Focus()
                End If
            Else
                txtBMImin.Text = ""
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtBMImax_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtBMImax.Validating
        Try
            If Val(txtBMImax.Text) > 0 Then
                If MinMaxValidator(Trim(txtBMImin.Text), Trim(txtBMImax.Text)) = False Then
                    MessageBox.Show("Please check the Minimum and Maximum values for BMI.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    txtBMImax.Focus()
                End If
            Else
                txtBMImax.Text = ""
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtBPsettingMax_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtBPsettingMax.Validating
        Try
            If Val(txtBPsettingMax.Text) > 0 Then
                If MinMaxValidator(Trim(txtBPsettingMin.Text), Trim(txtBPsettingMax.Text)) = False Then
                    MessageBox.Show("Please check the Minimum and Maximum values for BPSitting.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    txtBPsettingMax.Focus()
                End If
                If Val(txtBPsettingMax.Text) < 20 Or Val(txtBPsettingMax.Text) > 281 Then
                    MessageBox.Show("Invalid BP Setting (between 20-280)", "Disease Management", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    txtBPsettingMax.Focus()
                    Exit Sub
                End If
            Else
                txtBPsettingMax.Text = ""
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtBPsettingMin_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtBPsettingMin.Validating
        Try
            If Val(txtBPsettingMin.Text) > 0 Then
                If MinMaxValidator(Trim(txtBPsettingMin.Text), Trim(txtBPsettingMax.Text)) = False Then
                    MessageBox.Show("Please check the Minimum and Maximum values for BPSitting.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    txtBPsettingMax.Focus()
                End If
                If Val(txtBPsettingMin.Text) < 20 Or Val(txtBPsettingMin.Text) > 281 Then
                    MessageBox.Show("Invalid BP Setting (between 20-280)", "Disease Management", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    txtBPsettingMin.Focus()
                    Exit Sub
                End If
            Else
                txtBPsettingMin.Text = ""
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtBPstandingMax_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtBPstandingMax.Validating
        Try
            If Val(txtBPstandingMax.Text) > 0 Then
                If MinMaxValidator(Trim(txtBPstandingMin.Text), Trim(txtBPstandingMax.Text)) = False Then
                    MessageBox.Show("Please check the Minimum and Maximum values for BPStanding.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    txtBPstandingMax.Focus()
                End If
                If Val(txtBPstandingMax.Text) < 20 Or Val(txtBPstandingMax.Text) > 281 Then
                    MessageBox.Show("Invalid BP Setting (between 20-280)", "Disease Management", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    txtBPstandingMax.Focus()
                    Exit Sub
                End If
            Else
                txtBPstandingMax.Text = ""
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtBPstandingMin_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtBPstandingMin.Validating
        Try
            If Val(txtBPstandingMin.Text) > 0 Then
                If MinMaxValidator(Trim(txtBPstandingMin.Text), Trim(txtBPstandingMax.Text)) = False Then
                    MessageBox.Show("Please check the Minimum and Maximum values for BPStanding.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    txtBPstandingMax.Focus()
                End If
                If Val(txtBPstandingMin.Text) < 20 Or Val(txtBPstandingMin.Text) > 281 Then
                    MessageBox.Show("Invalid BP Setting (between 20-280)", "Disease Management", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    txtBPstandingMin.Focus()
                    Exit Sub
                End If
            Else
                txtBPstandingMin.Text = ""
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    ''' <summary>
    ''' 'Allow only numeric and Not decimal point keys
    ''' </summary>
    ''' <param name="Text"></param>
    ''' <param name="e"></param>
    ''' <remarks>added by dipak 20091106</remarks>
    Private Sub AllowNumaric(ByVal Text As String, ByVal e As KeyPressEventArgs)
        If Not ((e.KeyChar >= ChrW(48) AndAlso e.KeyChar <= ChrW(57)) OrElse (e.KeyChar = ChrW(8))) Then
            e.Handled = True
        End If
        Exit Sub
    End Sub

    Private Sub AllowDecimal(ByVal Text As String, ByVal e As KeyPressEventArgs)
        'Allow only numeric and decimal point keys
        If InStr(Trim(Text), ".") <> 0 AndAlso (e.KeyChar = ChrW(46)) Then
            'e.KeyChar.IsDigit(e.KeyChar)
            e.Handled = True
        Else
            If Not ((e.KeyChar >= ChrW(48) AndAlso e.KeyChar <= ChrW(57)) OrElse (e.KeyChar = ChrW(46)) OrElse (e.KeyChar = ChrW(8))) Then
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub txtPulseMin_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPulseMin.KeyPress
        Try
            AllowDecimal(txtPulseMin.Text, e)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtHeightMin_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtHeightMin.KeyPress
        Try
            AllowDecimal(txtHeightMin.Text, e)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtHeightMax_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtHeightMax.KeyPress
        Try
            AllowDecimal(txtHeightMax.Text, e)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtWeightMin_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtWeightMin.KeyPress
        Try
            AllowDecimal(txtWeightMin.Text, e)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtWeightMax_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtWeightMax.KeyPress
        Try
            AllowDecimal(txtWeightMax.Text, e)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtTemperatureMin_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTemperatureMin.KeyPress
        Try
            AllowDecimal(txtTemperatureMin.Text, e)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtTemperatureMax_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTemperatureMax.KeyPress
        Try
            AllowDecimal(txtTemperatureMax.Text, e)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtBMImin_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtBMImin.KeyPress
        Try
            AllowDecimal(txtBMImin.Text, e)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtBMImax_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtBMImax.KeyPress
        Try
            AllowDecimal(txtBMImax.Text, e)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtBPsettingMin_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtBPsettingMin.KeyPress
        Try
            AllowDecimal(txtBPsettingMin.Text, e)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtBPsettingMax_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtBPsettingMax.KeyPress
        Try
            AllowDecimal(txtBPsettingMax.Text, e)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtBPstandingMin_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtBPstandingMin.KeyPress
        Try
            AllowDecimal(txtBPstandingMin.Text, e)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtBPstandingMax_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtBPstandingMax.KeyPress
        Try
            AllowDecimal(txtBPstandingMax.Text, e)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtPulseMax_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPulseMax.KeyPress
        Try
            AllowDecimal(txtPulseMax.Text, e)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtPulseOXmin_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPulseOXmin.KeyPress
        Try
            AllowDecimal(txtPulseOXmin.Text, e)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtPulseOXmax_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPulseOXmax.KeyPress
        Try
            AllowDecimal(txtPulseOXmax.Text, e)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function GetFtInch(ByVal strHeight As String) As Array
        'Dim arrHeight() As String
        strHeight = Mid(strHeight, 1, Len(strHeight) - 1)
        'arrHeight = 
        Return Split(strHeight, "'", , CompareMethod.Text)

        'Return arrHeight
    End Function

    Private Sub txtHeightMaxInch_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtHeightMaxInch.KeyPress
        Try
            AllowDecimal(txtHeightMaxInch.Text, e)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtHeightMinInch_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtHeightMinInch.KeyPress
        Try
            AllowDecimal(txtHeightMinInch.Text, e)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtHeightMinInch_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtHeightMinInch.Validating
        Try
            If Val(txtHeightMin.Text) <= 0 Then
                If Val(txtHeightMinInch.Text) >= 12 AndAlso Val(txtHeightMinInch.Text) <= 84 Then
                    Dim _Ft As Decimal
                    Dim _Inches As Decimal
                    Dim _TotalInches As Decimal = Val(txtHeightMinInch.Text)

                    _Ft = Math.Floor(_TotalInches / 12)
                    _Inches = Math.Round(_TotalInches Mod 12, 2)
                    txtHeightMin.Text = _Ft
                    txtHeightMinInch.Text = _Inches
                    ' Exit Sub
                ElseIf Val(txtHeightMinInch.Text) > 84 Then
                    MessageBox.Show("Invalid value of Inches", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    txtHeightMinInch.Focus()
                End If
            Else
                If Val(txtHeightMinInch.Text) >= 12 Then
                    MessageBox.Show("Invalid value of Inches", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    txtHeightMinInch.Focus()
                End If
            End If

            'If Val(txtHeightMinInch.Text) >= 12 Then
            '    MessageBox.Show("Invalid value of Inches", "Patient Vitals", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    txtHeightMinInch.Focus()
            '    Exit Sub
            'End If

            If Val(txtHeightMinInch.Text) = 0 Then
                txtHeightMinInch.Text = ""
                Exit Sub
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtHeightMaxInch_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtHeightMaxInch.Validating
        Try
            If Val(txtHeightMax.Text) <= 0 Then
                If Val(txtHeightMaxInch.Text) >= 12 AndAlso Val(txtHeightMaxInch.Text) <= 84 Then
                    Dim _Ft As Decimal
                    Dim _Inches As Decimal
                    Dim _TotalInches As Decimal = Val(txtHeightMaxInch.Text)

                    _Ft = Math.Floor(_TotalInches / 12)
                    _Inches = Math.Round(_TotalInches Mod 12, 2)
                    txtHeightMax.Text = _Ft
                    txtHeightMaxInch.Text = _Inches
                    'Exit Sub
                ElseIf Val(txtHeightMaxInch.Text) > 84 Then
                    MessageBox.Show("Invalid value of Inches", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    txtHeightMaxInch.Focus()
                End If
            Else
                If Val(txtHeightMaxInch.Text) >= 12 Then
                    MessageBox.Show("Invalid value of Inches", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    txtHeightMaxInch.Focus()
                End If
            End If
            'If Val(txtHeightMaxInch.Text) >= 12 Then
            '    MessageBox.Show("Invalid value of Inches", "Patient Vitals", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    txtHeightMaxInch.Focus()
            '    ' Exit Sub
            'End If

            If Val(txtHeightMin.Text) > Val(txtHeightMax.Text) Then
                MessageBox.Show("Invalid value of Ft ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtHeightMax.Focus()
            ElseIf Val(txtHeightMin.Text) = Val(txtHeightMax.Text) AndAlso (Val(txtHeightMinInch.Text) > Val(txtHeightMaxInch.Text)) Then
                MessageBox.Show("Invalid value of Inches", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtHeightMaxInch.Focus()
            End If

            If Val(txtHeightMaxInch.Text) = 0 Then
                txtHeightMaxInch.Text = ""
                'Exit Sub
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnLabs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLabs.Click
        Try
            ''This code is commented by shilpa because currently we are not using tab structure in form
            ''function for display Labs Tag of the Tag Pages and hide others tags pages
            ''HidenShow(tpLab)
            'Code added by dipak 20091003 :fills Labs
            'Fill_Labs()
            'End Code added by dipak 20091003 
            pnlLab.Visible = True
            pnlLab.BringToFront()

            btnLabs.BackgroundImage = Global.gloEMR.My.Resources.Img_LongOrange
            btnLabs.BackgroundImageLayout = ImageLayout.Stretch
            btnLabs.Tag = "Selected"

            btnCPT.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
            btnCPT.BackgroundImageLayout = ImageLayout.Stretch
            btnCPT.Tag = "UnSelected"

            btnDrugs.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
            btnDrugs.BackgroundImageLayout = ImageLayout.Stretch
            btnDrugs.Tag = "UnSelected"

            btnHistory.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
            btnHistory.BackgroundImageLayout = ImageLayout.Stretch
            btnHistory.Tag = "UnSelected"

            btnDemographics.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
            btnDemographics.BackgroundImageLayout = ImageLayout.Stretch
            btnDemographics.Tag = "UnSelected"

            btnRadiology.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
            btnRadiology.BackgroundImageLayout = ImageLayout.Stretch
            btnRadiology.Tag = "UnSelected"

            btnOrders.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
            btnOrders.BackgroundImageLayout = ImageLayout.Stretch
            btnOrders.Tag = "UnSelected"

            btnICD9.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
            btnICD9.BackgroundImageLayout = ImageLayout.Stretch
            btnICD9.Tag = "UnSelected"

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    'Private Sub Fill_Labs()
    '    Try
    '        Dim objResult As New gloStream.DiseaseManagement.Common.Criteria
    '        With C1LabResult
    '            .Rows.Count = 1
    '            .Rows.Fixed = 1
    '            .Cols.Fixed = 0

    '            '''''Set Column Property of flexgrid
    '            .Cols(COL_TestID).Width = 0
    '            .SetData(0, COL_TestID, "TestID")
    '            .Cols(COL_TestID).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

    '            .Cols(COL_TestName).Width = ((.Width / 3) * 1.4) - 20
    '            .SetData(0, COL_TestName, "Test-Result")
    '            .Cols(COL_TestName).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

    '            .Cols(COL_ResultID).Width = 0
    '            .SetData(0, COL_ResultID, "ResultID")
    '            .Cols(COL_ResultID).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

    '            .Cols(COL_Operator).Width = ((.Width / 3) * 0.7) - 20
    '            .SetData(0, COL_Operator, "Operator")
    '            .Cols(COL_Operator).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

    '            .Cols(COL_ResultValue1).Width = ((.Width / 3) * 0.6) - 20
    '            .SetData(0, COL_ResultValue1, "Result Value1")
    '            .Cols(COL_ResultValue1).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

    '            .Cols(COL_ResultValue2).Width = ((.Width / 3) * 0.6) - 20
    '            .SetData(0, COL_ResultValue2, "Result Value2")
    '            .Cols(COL_ResultValue2).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter


    '            .Cols(COL_IDENTITYModule).Width = 0
    '            .SetData(0, COL_IDENTITYModule, "Identity")
    '            .Cols(COL_IDENTITYModule).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter
    '            .Cols.Count = COL_CountLab

    '            ''''Set the property for treeview column
    '            .Tree.Column = COL_TestName
    '            .Tree.Style = C1.Win.C1FlexGrid.TreeStyleFlags.Simple
    '            .Tree.Indent = 15

    '            Dim _C As Integer, _G As Integer
    '            ''''Create object for the class
    '            Dim oCV As New gloStream.CardioVascular.CardioVascular
    '            Dim oLabs As New gloStream.CardioVascular.Supporting.OtherDetails

    '            ''''assign Lab Test and Result to collection
    '            oLabs = oCV.GetLabs

    '            If Not oLabsModule Is Nothing Then
    '                If oLabsModule.Count > 0 Then

    '                    'Fill Test
    '                    For _C = 1 To oLabsModule.Count
    '                        .Rows.Add()
    '                        With .Rows(.Rows.Count - 1)
    '                            .ImageAndText = True
    '                            .Height = 24
    '                            .IsNode = True
    '                            '//.Style = FillControl.Styles("CS_Category")
    '                            .Node.Level = 0
    '                            .Node.Data = oLabsModule(_C).Name
    '                            .Node.Key = oLabsModule(_C).TestID
    '                        End With
    '                        .SetData(.Rows.Count - 1, COL_TestID, oLabsModule(_C).TestID)
    '                        .SetData(.Rows.Count - 1, COL_IDENTITYModule, _LabModule_Result & oLabsModule(_C).TestID)
    '                        .Rows(.Rows.Count - 1).AllowEditing = False
    '                    Next

    '                    'Fill Result
    '                    For _C = 1 To oLabsModule.Count
    '                        For _G = 1 To oLabsModule.Item(_C).LabModuleTestResults.Count
    '                            Dim oFindNode As C1.Win.C1FlexGrid.Node
    '                            oFindNode = GetC1NodeModule(_LabModule_Result & oLabsModule.Item(_C).TestID)

    '                            If Not oFindNode Is Nothing Then
    '                                oFindNode.AddNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild, oLabsModule.Item(_C).LabModuleTestResults(_G).ResultName)
    '                                Dim _tmpRow As Integer = oFindNode.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild).Row.Index
    '                                If Not _tmpRow = -1 Then
    '                                    .Rows(_tmpRow).ImageAndText = True
    '                                    .Rows(_tmpRow).Height = 24
    '                                    .Rows(_tmpRow).AllowEditing = True
    '                                    Dim strOperator As String
    '                                    Dim cStyle As C1.Win.C1FlexGrid.CellStyle
    '                                    Dim rgOperator As C1.Win.C1FlexGrid.CellRange = .GetCellRange(_tmpRow, COL_Operator, _tmpRow, COL_Operator)
    '                                    cStyle = .Styles.Add("Operator")
    '                                    strOperator = "Greater Than" & "|" & "Less Than" & "|" & "Between"
    '                                    cStyle.ComboList = strOperator
    '                                    rgOperator.Style = cStyle
    '                                    .SetCellCheck(_tmpRow, COL_TestName, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
    '                                    .SetData(_tmpRow, COL_TestID, oLabsModule(_C).TestID)
    '                                    .SetData(_tmpRow, COL_ResultID, oLabsModule(_C).LabModuleTestResults(_G).ResultID)
    '                                    _tmpRow = -1
    '                                End If
    '                                oFindNode = Nothing
    '                            End If
    '                        Next
    '                    Next
    '                End If
    '            End If
    '            oDM = Nothing
    '            oLabsModule = Nothing
    '        End With
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message)
    '    End Try
    'End Sub

    Private Sub btnRadiologyTest_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRadiologyTest.Click
        txtSearchOrder.Text = ""
        PopulateAssocaitedInfo(4)
    End Sub

    'This code is added by Shilpa on 7th January for changing the Buttons background image MouseHover & MouseLeave Events

    Private Sub btnGuideline_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuideline.Click
        txtSearchOrder.Text = ""
        PopulateAssocaitedInfo(5)
    End Sub

    Private Sub btnGuideline_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGuideline.MouseHover
        btnGuideline.BackgroundImage = Global.gloEMR.My.Resources.Img_LongYellow
        btnGuideline.BackgroundImageLayout = ImageLayout.Stretch
    End Sub

    Private Sub btnGuideline_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGuideline.MouseLeave
        If btnGuideline.Tag = "Selected" Then
            btnGuideline.BackgroundImage = Global.gloEMR.My.Resources.Img_LongOrange
            btnGuideline.BackgroundImageLayout = ImageLayout.Stretch
        Else
            btnGuideline.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
            btnGuideline.BackgroundImageLayout = ImageLayout.Stretch
        End If
    End Sub

    Private Sub btnOrders_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOrders.Click
        Try
            ''This code is commented by shilpa because currently we are not using tab structure in form
            ''function for display Labs Tag of the Tag Pages and hide others tags pages
            ''HidenShow(tpGuideline)
            'Code added by dipak 20091003 :fills Orders
            'Fill_Orders()
            'End Code added by dipak 20091003 
            'Line Commented By Dipak as no need to fill summary from here.
            FillSummery()
            pnlSummaryOthers.Visible = True
            pnlSummaryOthers.BringToFront()

            btnOrders.Tag = "Selected"
            btnOrders.BackgroundImage = Global.gloEMR.My.Resources.Img_LongOrange
            btnOrders.BackgroundImageLayout = ImageLayout.Stretch

            btnDemographics.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
            btnDemographics.BackgroundImageLayout = ImageLayout.Stretch
            btnDemographics.Tag = "UnSelected"

            btnHistory.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
            btnHistory.BackgroundImageLayout = ImageLayout.Stretch
            btnHistory.Tag = "UnSelected"

            btnDrugs.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
            btnDrugs.BackgroundImageLayout = ImageLayout.Stretch
            btnDrugs.Tag = "UnSelected"

            btnICD9.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
            btnICD9.BackgroundImageLayout = ImageLayout.Stretch
            btnICD9.Tag = "UnSelected"

            btnCPT.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
            btnCPT.BackgroundImageLayout = ImageLayout.Stretch
            btnCPT.Tag = "UnSelected"

            btnLabs.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
            btnLabs.BackgroundImageLayout = ImageLayout.Stretch
            btnLabs.Tag = "UnSelected"

            btnRadiology.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
            btnRadiology.BackgroundImageLayout = ImageLayout.Stretch
            btnRadiology.Tag = "UnSelected"
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnRx_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRx.Click
        txtSearchOrder.Text = ""
        PopulateAssocaitedInfo(3)
    End Sub

    Public Sub FillSummery()
        Dim strDemographic As String = ""
        Dim strVitals As String = ""

        txt_summary.Text = ""

        If Not cmbAgeMin.SelectedItem Is Nothing AndAlso Not cmbAgeMax.SelectedItem Is Nothing Then
            strDemographic = vbTab & "Age : " & cmbAgeMin.SelectedItem & " To " & cmbAgeMax.SelectedItem & vbCrLf
        End If

        If Not cmbGender.SelectedItem Is Nothing Then
            strDemographic = strDemographic & vbTab & "Gender : " & cmbGender.SelectedItem & vbCrLf
        End If

        If strHeight <> "" Then
            strVitals = vbTab & "Minimum Height : " & strHeight & vbCrLf
        End If

        If strHeightMax <> "" Then
            strVitals = strVitals & vbTab & "Maximum Height : " & strHeightMax & vbCrLf
        End If

        If txtBPsettingMin.Text <> "" Then
            strVitals = strVitals & vbTab & "Minimum BP Sitting : " & CDbl(Val(txtBPsettingMin.Text.Trim)) & vbCrLf
        End If

        If txtBPsettingMax.Text <> "" Then
            strVitals = strVitals & vbTab & "Maximum BP Sitting : " & CDbl(Val(txtBPsettingMax.Text.Trim)) & vbCrLf
        End If

        If txtWeightMin.Text <> "" Then
            strVitals = strVitals & vbTab & "Weight Minimum : " & CDbl(Val(txtWeightMin.Text.Trim)) & vbCrLf
        End If

        If txtWeightMax.Text <> "" Then
            strVitals = strVitals & vbTab & "Weight Maximum : " & CDbl(Val(txtWeightMax.Text.Trim)) & vbCrLf
        End If

        If txtBPstandingMin.Text <> "" Then
            strVitals = strVitals & vbTab & "Minimum BP Standing : " & CDbl(Val(txtBPstandingMin.Text.Trim)) & vbCrLf
        End If

        If txtBPstandingMax.Text <> "" Then
            strVitals = strVitals & vbTab & "Maximum BP Standing : " & CDbl(Val(txtBPstandingMax.Text.Trim)) & vbCrLf
        End If

        If txtTemperatureMin.Text <> "" Then
            strVitals = strVitals & vbTab & "Minimum Temperature : " & CDbl(Val(txtTemperatureMin.Text.Trim)) & vbCrLf
        End If

        If txtTemperatureMax.Text <> "" Then
            strVitals = strVitals & vbTab & "Maximum Temperature : " & CDbl(Val(txtTemperatureMax.Text.Trim)) & vbCrLf
        End If

        If txtPulseMin.Text <> "" Then
            strVitals = strVitals & vbTab & "Minimum Pulse : " & CDbl(Val(txtPulseMin.Text.Trim)) & vbCrLf
        End If

        If txtPulseMax.Text <> "" Then
            strVitals = strVitals & vbTab & "Maximum Pulse : " & CDbl(Val(txtPulseMax.Text.Trim)) & vbCrLf
        End If

        If txtBMImin.Text <> "" Then
            strVitals = strVitals & vbTab & "Minimum BMI : " & CDbl(Val(txtBMImin.Text.Trim)) & vbCrLf
        End If

        If txtBMImax.Text <> "" Then
            strVitals = strVitals & vbTab & "Maximum BMI : " & CDbl(Val(txtBMImax.Text.Trim)) & vbCrLf
        End If

        If txtPulseOXmin.Text <> "" Then
            strVitals = strVitals & vbTab & "Minimum PulseOX : " & CDbl(Val(txtPulseOXmin.Text.Trim)) & vbCrLf
        End If

        If txtPulseOXmax.Text <> "" Then
            strVitals = strVitals & vbTab & "Maximum PulseOX : " & CDbl(Val(txtPulseOXmax.Text.Trim)) & vbCrLf
        End If

        ''DEMOGRAPHIC
        If strDemographic <> "" Then
            txt_summary.Text = "Demographic : " & vbCrLf & strDemographic & vbCrLf
        End If

        ''VITALS
        If strVitals <> "" Then
            txt_summary.Text = txt_summary.Text & "Vitals : " & vbCrLf & strVitals & vbCrLf
        End If

        Dim strhistory As String = ""
        Dim strDrugs As String = ""
        Dim strICD9 As String = ""
        Dim strCPT As String = ""
        Dim strRadiology As String = ""
        Dim strLab As String = ""
        'data for History
        For i As Integer = 0 To trvSelectedHistory.GetNodeCount(False) - 1
            For j As Integer = 0 To trvSelectedHistory.Nodes(i).GetNodeCount(False) - 1
                If strhistory.Contains("History :") Then

                    If strhistory.Contains(trvSelectedHistory.Nodes(i).Text) Then
                        strhistory = strhistory & ", " & trvSelectedHistory.Nodes(i).Nodes(j).Text
                    Else
                        strhistory = strhistory & vbNewLine & vbTab & trvSelectedHistory.Nodes(i).Text & vbNewLine & vbTab & vbTab & trvSelectedHistory.Nodes(i).Nodes(j).Text
                    End If
                Else
                    strhistory = "History :  " & vbNewLine & vbTab & trvSelectedHistory.Nodes(i).Text & vbNewLine & vbTab & vbTab & trvSelectedHistory.Nodes(i).Nodes(j).Text
                End If
            Next
        Next
        txt_summary.Text = txt_summary.Text & vbNewLine & strhistory


        ' data for drugs
        For i As Integer = 0 To trvSelectedDrugs.GetNodeCount(False) - 1
            If strDrugs.Contains("Drugs :") Then
                strDrugs = strDrugs & ", " & trvSelectedDrugs.Nodes(i).Text
            Else
                txt_summary.Text = txt_summary.Text & vbNewLine & vbNewLine & vbNewLine
                strDrugs = "Drugs :" & vbNewLine & vbTab & trvSelectedDrugs.Nodes(i).Text
            End If
        Next
        txt_summary.Text = txt_summary.Text & strDrugs

        'data for ICD9 
        For i As Integer = 0 To trvICD9.GetNodeCount(False) - 1
            If trvICD9.Nodes(i).Checked = True Then
                If strICD9.Contains("ICD9 :") Then
                    strICD9 = strICD9 & ", " & trvICD9.Nodes(i).Text
                Else
                    txt_summary.Text = txt_summary.Text & vbNewLine & vbNewLine & vbNewLine
                    strICD9 = "ICD9 :" & vbNewLine & vbTab & trvICD9.Nodes(i).Text
                End If
            End If
        Next
        txt_summary.Text = txt_summary.Text & strICD9



        'data for CPT treeview
        For i As Integer = 0 To trvCPT.GetNodeCount(False) - 1
            If trvCPT.Nodes(i).Checked = True Then
                If strCPT.Contains("CPT :") Then
                    strCPT = strCPT & "," & trvCPT.Nodes(i).Text
                Else
                    txt_summary.Text = txt_summary.Text & vbNewLine & vbNewLine & vbNewLine
                    strCPT = "CPT :" & vbNewLine & vbTab & trvCPT.Nodes(i).Text
                End If
            End If
        Next
        txt_summary.Text = txt_summary.Text & strCPT

        'Lab Module  ' Enhancement - 02/08/2007
        For i As Integer = 1 To C1LabResult.Rows.Count - 1
            If C1LabResult.GetCellCheck(i, COL_TestName) = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                With C1LabResult
                    If strLab.Contains("Labs :") Then
                        strLab = strLab & ", " & .GetData(i, COL_TestName)
                    Else
                        txt_summary.Text = txt_summary.Text & vbNewLine & vbNewLine & vbNewLine
                        strLab = "Labs :" & vbNewLine & vbTab & .GetData(i, COL_TestName)
                    End If
                End With
            End If
        Next
        txt_summary.Text = txt_summary.Text & strLab

        ' RadiologyLAB
        For i As Integer = 1 To c1Labs.Rows.Count - 1
            If c1Labs.Rows(i).Node.Level <> 0 Then
                If c1Labs.GetCellCheck(i, COL_NAME) = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                    If strRadiology.Contains("Orders :") Then
                        strRadiology = strRadiology & ", " & c1Labs.Rows(i).Node.Data
                    Else
                        txt_summary.Text = txt_summary.Text & vbNewLine & vbNewLine & vbNewLine
                        strRadiology = "Orders :" & vbNewLine & vbTab & c1Labs.Rows(i).Node.Data
                    End If
                End If
            End If
        Next

        txt_summary.Text = txt_summary.Text & strRadiology

    End Sub

    'This code is added by Shilpa on 7th January for changing the Buttons background image MouseHover & MouseLeave Events

    Private Sub btnRx_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRx.MouseHover
        btnRx.BackgroundImage = Global.gloEMR.My.Resources.Img_LongYellow
        btnRx.BackgroundImageLayout = ImageLayout.Stretch
    End Sub

    Private Sub btnRx_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRx.MouseLeave

        If btnRx.Tag = "Selected" Then
            btnRx.BackgroundImage = Global.gloEMR.My.Resources.Img_LongOrange
            btnRx.BackgroundImageLayout = ImageLayout.Stretch
        Else
            btnRx.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
            btnRx.BackgroundImageLayout = ImageLayout.Stretch
        End If

    End Sub

    Private Sub btnReferrals_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReferrals.Click
        txtSearchOrder.Text = ""
        PopulateAssocaitedInfo(2)
        'If pnlbtnReferrals.Dock = DockStyle.Top Then
        '    btnReferrals.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongOrange
        '    btnReferrals.BackgroundImageLayout = ImageLayout.Stretch
        'Else
        '    btnReferrals.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
        '    btnReferrals.BackgroundImageLayout = ImageLayout.Stretch
        'End If
    End Sub

    Private Sub btnReferrals_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReferrals.MouseLeave

        If btnReferrals.Tag = "Selected" Then
            btnReferrals.BackgroundImage = Global.gloEMR.My.Resources.Img_LongOrange
            btnReferrals.BackgroundImageLayout = ImageLayout.Stretch
        Else
            btnReferrals.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
            btnReferrals.BackgroundImageLayout = ImageLayout.Stretch
        End If
    End Sub

    Private Sub btnOthers_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs)
        btnOrders.BackgroundImage = Global.gloEMR.My.Resources.Img_LongYellow
        btnOrders.BackgroundImageLayout = ImageLayout.Stretch
    End Sub

    Private Sub btnOthers_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub btnRadiology_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRadiology.MouseHover
        btnRadiology.BackgroundImage = Global.gloEMR.My.Resources.Img_LongYellow
        btnRadiology.BackgroundImageLayout = ImageLayout.Stretch
    End Sub

    Private Sub btnRadiology_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRadiology.MouseLeave

        If btnRadiology.Tag = "Selected" Then
            btnRadiology.BackgroundImage = Global.gloEMR.My.Resources.Img_LongOrange
            btnRadiology.BackgroundImageLayout = ImageLayout.Stretch
        Else
            btnRadiology.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
            btnRadiology.BackgroundImageLayout = ImageLayout.Stretch
        End If

    End Sub

    Private Sub btnLabs_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLabs.MouseHover
        btnLabs.BackgroundImage = Global.gloEMR.My.Resources.Img_LongYellow
        btnLabs.BackgroundImageLayout = ImageLayout.Stretch
    End Sub

    Private Sub btnLabs_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLabs.MouseLeave

        If btnLabs.Tag = "Selected" Then
            btnLabs.BackgroundImage = Global.gloEMR.My.Resources.Img_LongOrange
            btnLabs.BackgroundImageLayout = ImageLayout.Stretch
        Else
            btnLabs.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
            btnLabs.BackgroundImageLayout = ImageLayout.Stretch
        End If

    End Sub

    Private Sub btnCPT_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCPT.MouseHover
        btnCPT.BackgroundImage = Global.gloEMR.My.Resources.Img_LongYellow
        btnCPT.BackgroundImageLayout = ImageLayout.Stretch
    End Sub

    Private Sub btnCPT_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCPT.MouseLeave

        If btnCPT.Tag = "Selected" Then
            btnCPT.BackgroundImage = Global.gloEMR.My.Resources.Img_LongOrange
            btnCPT.BackgroundImageLayout = ImageLayout.Stretch
        Else
            btnCPT.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
            btnCPT.BackgroundImageLayout = ImageLayout.Stretch
        End If

    End Sub

    Private Sub btnICD9_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnICD9.MouseHover
        btnICD9.BackgroundImage = Global.gloEMR.My.Resources.Img_LongYellow
        btnICD9.BackgroundImageLayout = ImageLayout.Stretch
    End Sub

    Private Sub btnICD9_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnICD9.MouseLeave

        If btnICD9.Tag = "Selected" Then
            btnICD9.BackgroundImage = Global.gloEMR.My.Resources.Img_LongOrange
            btnICD9.BackgroundImageLayout = ImageLayout.Stretch
        Else
            btnICD9.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
            btnICD9.BackgroundImageLayout = ImageLayout.Stretch
        End If

    End Sub

    Private Sub btnDrugs_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDrugs.MouseHover
        btnDrugs.BackgroundImage = Global.gloEMR.My.Resources.Img_LongYellow
        btnDrugs.BackgroundImageLayout = ImageLayout.Stretch
    End Sub

    Private Sub btnDrugs_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDrugs.MouseLeave

        If btnDrugs.Tag = "Selected" Then
            btnDrugs.BackgroundImage = Global.gloEMR.My.Resources.Img_LongOrange
            btnDrugs.BackgroundImageLayout = ImageLayout.Stretch
        Else
            btnDrugs.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
            btnDrugs.BackgroundImageLayout = ImageLayout.Stretch
        End If

    End Sub

    Private Sub btnHistory_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHistory.MouseHover
        btnHistory.BackgroundImage = Global.gloEMR.My.Resources.Img_LongYellow
        btnHistory.BackgroundImageLayout = ImageLayout.Stretch
    End Sub

    Private Sub btnHistory_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHistory.MouseLeave

        If btnHistory.Tag = "Selected" Then
            btnHistory.BackgroundImage = Global.gloEMR.My.Resources.Img_LongOrange
            btnHistory.BackgroundImageLayout = ImageLayout.Stretch
        Else
            btnHistory.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
            btnHistory.BackgroundImageLayout = ImageLayout.Stretch
        End If

    End Sub

    Private Sub btnDemographics_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDemographics.MouseHover
        btnDemographics.BackgroundImage = Global.gloEMR.My.Resources.Img_LongYellow
        btnDemographics.BackgroundImageLayout = ImageLayout.Stretch
    End Sub

    Private Sub btnDemographics_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDemographics.MouseLeave

        If btnDemographics.Tag = "Selected" Then
            btnDemographics.BackgroundImage = Global.gloEMR.My.Resources.Img_LongOrange
            btnDemographics.BackgroundImageLayout = ImageLayout.Stretch
        Else
            btnDemographics.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
            btnDemographics.BackgroundImageLayout = ImageLayout.Stretch
        End If

    End Sub

    Private Sub btnLab_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLab.Click
        txtSearchOrder.Text = ""
        PopulateAssocaitedInfo(1)
    End Sub

    Private Sub trvTriggers_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles trvTriggers.DoubleClick

    End Sub

    Private Sub trOrderInfo_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs)

    End Sub

    Private Sub tlsDM_ItemClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tlsDM.ItemClicked
        Select Case e.ClickedItem.Tag
            Case "Save"
                Call SaveCriteria()
                ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.CardioVascular, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.Add, "CardioVascular Criteria Saved", gloAuditTrail.ActivityOutCome.Success)
                ''Added Rahul P on 20101011
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.CardioVascular, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.Add, "CardioVascular Criteria Saved", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                ''
                'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Add, "Added records to the database", gstrLoginName, gstrClientMachineName, gnPatientID, True, gloAuditTrail.enmOutCome.Success, "gloEMR")
            Case "Close"
                gloWord.WordDialogBoxBackgroundCloser.Close(Me)
                '   Me.Close()
                ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.CardioVascular, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.Close, "CardioVascular Criteria Setup Closed.", gloAuditTrail.ActivityOutCome.Success)
                ''Added Rahul P on 20101011
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.CardioVascular, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.Close, "CardioVascular Criteria Setup Closed.", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                ''
                'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Other, "Closed Cardio Vascular form", gstrLoginName, gstrClientMachineName, gnPatientID, True, gloAuditTrail.enmOutCome.Success, "gloEMR")
        End Select
    End Sub

    ''' <summary>
    ''' Implmenting the context menu for deleting the selected item
    ''' </summary>
    

    Private Sub UpdateTemplate(ByVal ID As Int64)

        ' Dim objTemplateGallery As New clsTemplateGallery
        Dim objfrmTemplateGallery As frmTemplateGallery
        Try


            blnModify = True



            ''''''''''''''''''
            objfrmTemplateGallery = New frmTemplateGallery(ID)
            With objfrmTemplateGallery
                .Text = "Modify Template"
                '.mycaller = Me
                .MdiParent = Me.ParentForm
                'CType(Me.MdiParent, MainMenu).ShowHideMainMenu(False, False)
                'CType(Me.ParentForm, MainMenu).pnlMainToolBar.Visible = False

                .Show()
                .WindowState = FormWindowState.Maximized
                .BringToFront()

            End With

        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Template Gallery", MessageBoxButtons.OK, MessageBoxIcon.Error)
            CType(Me.MdiParent, MainMenu).ShowHideMainMenu(True, True)
            CType(Me.ParentForm, MainMenu).pnlMainToolBar.Visible = True

            'Finally
            '  objfrmTemplateGallery = Nothing
        End Try
    End Sub

    Private Sub btnLab_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLab.MouseHover
        btnLab.BackgroundImage = Global.gloEMR.My.Resources.Img_LongYellow
        btnLab.BackgroundImageLayout = ImageLayout.Stretch
    End Sub

    Private Sub btnLab_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLab.MouseLeave
        If btnLab.Tag = "Selected" Then
            btnLab.BackgroundImage = Global.gloEMR.My.Resources.Img_LongOrange
            btnLab.BackgroundImageLayout = ImageLayout.Stretch
        Else
            btnLab.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
            btnLab.BackgroundImageLayout = ImageLayout.Stretch
        End If

    End Sub

    Private Sub btnOrders_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOrders.MouseHover
        btnOrders.BackgroundImage = Global.gloEMR.My.Resources.Img_LongYellow
        btnOrders.BackgroundImageLayout = ImageLayout.Stretch
    End Sub

    Private Sub btnOrders_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOrders.MouseLeave
        If btnOrders.Tag = "Selected" Then
            btnOrders.BackgroundImage = Global.gloEMR.My.Resources.Img_LongOrange
            btnOrders.BackgroundImageLayout = ImageLayout.Stretch
        Else
            btnOrders.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
            btnOrders.BackgroundImageLayout = ImageLayout.Stretch
        End If
    End Sub

    Private Sub btnReferrals_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReferrals.MouseHover
        btnReferrals.BackgroundImage = Global.gloEMR.My.Resources.Img_LongYellow
        btnReferrals.BackgroundImageLayout = ImageLayout.Stretch
    End Sub

    Private Sub btnRadiologyTest_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRadiologyTest.MouseHover
        btnRadiologyTest.BackgroundImage = Global.gloEMR.My.Resources.Img_LongYellow
        btnRadiologyTest.BackgroundImageLayout = ImageLayout.Stretch
    End Sub

    Private Sub btnRadiologyTest_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRadiologyTest.MouseLeave

        If btnRadiologyTest.Tag = "Selected" Then
            btnRadiologyTest.BackgroundImage = Global.gloEMR.My.Resources.Img_LongOrange
            btnRadiologyTest.BackgroundImageLayout = ImageLayout.Stretch
        Else
            btnRadiologyTest.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
            btnRadiologyTest.BackgroundImageLayout = ImageLayout.Stretch
        End If

    End Sub

    Private Sub mnuEditTemplate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuEditTemplate.Click

    End Sub

    Private Sub trvDrgs_NodeMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles trvDrgs.NodeMouseDoubleClick
        Try
            trvDrgs.SelectedNode = e.Node


            Dim Ispresent As Boolean = False

            For Each myDNode As TreeNode In trvSelectedDrugs.Nodes
                If myDNode.Tag = trvDrgs.SelectedNode.Tag Then
                    Ispresent = True
                    Exit For
                End If
            Next
            If Ispresent = False Then
                Dim myDrugNode As TreeNode
                myDrugNode = trvDrgs.SelectedNode.Clone()
                trvSelectedDrugs.Nodes.Add(myDrugNode)
                myDrugNode.ImageIndex = 0
                myDrugNode.SelectedImageIndex = 0
            End If
            trvSelectedDrugs.ExpandAll()
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub trvSelectedDrugs_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles trvSelectedDrugs.MouseDown
        Try

            Dim trvnode As TreeNode
            trvnode = trvSelectedDrugs.GetNodeAt(e.X, e.Y)
            trvSelectedDrugs.SelectedNode = trvnode
            If e.Button = Windows.Forms.MouseButtons.Right Then
                Me.TopMost = True
                Me.TopMost = False
                If IsNothing(trvnode) = False Then
                    'If trvnode.Tag <> "" Then
                    If trvnode.Text <> "" Then
                        'Try
                        '    If (IsNothing(trvSelectedDrugs.ContextMenuStrip) = False) Then
                        '        trvSelectedDrugs.ContextMenuStrip.Dispose()
                        '        trvSelectedDrugs.ContextMenuStrip = Nothing
                        '    End If
                        'Catch ex As Exception

                        'End Try
                        trvSelectedDrugs.ContextMenuStrip = ContextMenuStrip1
                    Else
                        'Try
                        '    If (IsNothing(trvSelectedDrugs.ContextMenuStrip) = False) Then
                        '        trvSelectedDrugs.ContextMenuStrip.Dispose()
                        '        trvSelectedDrugs.ContextMenuStrip = Nothing
                        '    End If
                        'Catch ex As Exception

                        'End Try
                        trvSelectedDrugs.ContextMenuStrip = Nothing
                    End If
                Else
                    'Try
                    '    If (IsNothing(trvSelectedDrugs.ContextMenuStrip) = False) Then
                    '        trvSelectedDrugs.ContextMenuStrip.Dispose()
                    '        trvSelectedDrugs.ContextMenuStrip = Nothing
                    '    End If
                    'Catch ex As Exception

                    'End Try
                    trvSelectedDrugs.ContextMenuStrip = Nothing
                End If
            Else
                'Try
                '    If (IsNothing(trvSelectedDrugs.ContextMenuStrip) = False) Then
                '        trvSelectedDrugs.ContextMenuStrip.Dispose()
                '        trvSelectedDrugs.ContextMenuStrip = Nothing
                '    End If
                'Catch ex As Exception

                'End Try
                trvSelectedDrugs.ContextMenuStrip = Nothing
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub mnuDeleteDrugs_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuDeleteDrugs.Click
        Dim trvnode As New TreeNode
        trvnode = trvSelectedDrugs.SelectedNode
        'If trvnode.Tag <> "" Then
        If trvnode.Text <> "" Then
            trvSelectedDrugs.SelectedNode.Remove()
        End If
    End Sub

    Private Sub trvHistoryRight_AfterExpand(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs)
        CurrentParentNode = New TreeNode
        CurrentParentNode = e.Node
    End Sub

    Private Sub cmbHistoryCategory_SelectionChangeCommitted(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbHistoryCategory.SelectionChangeCommitted
        'Fill_Histories(cmbHistoryCategory.SelectedValue, cmbHistoryCategory.Text)
        Fill_Histories_1(cmbHistoryCategory.SelectedValue, cmbHistoryCategory.Text)
    End Sub

    Private Sub trvHistoryRight_NodeMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles trvHistoryRight.NodeMouseDoubleClick
        Try
            trvHistoryRight.SelectedNode = e.Node
            trvSelectedHistory.BeginUpdate()
            Dim SelectedHistoryNode As New TreeNode
            Dim oNode As TreeNode

            SelectedHistoryNode = trvHistoryRight.SelectedNode.Clone

            Dim CategoryFound As Boolean = False
            Dim HistoryFound As Boolean = False

            ''Selected Current Criteria
            For Each CategoryNode As TreeNode In trvSelectedHistory.Nodes
                If CategoryNode.Text = cmbHistoryCategory.Text Then
                    For Each HistoryNode As TreeNode In CategoryNode.Nodes
                        If HistoryNode.Text = SelectedHistoryNode.Text Then
                            HistoryFound = True
                            Exit For
                        End If
                    Next
                    If Not HistoryFound Then
                        CategoryNode.Nodes.Add(SelectedHistoryNode)
                        CategoryNode.Expand()
                        trvSelectedHistory.Sort()
                    End If
                    CategoryFound = True
                    Exit For
                End If
            Next

            If Not CategoryFound Then
                oNode = New TreeNode
                oNode.Text = cmbHistoryCategory.Text
                oNode.Tag = cmbHistoryCategory.SelectedValue
                oNode.ImageIndex = 0
                oNode.SelectedImageIndex = 0
                oNode.Nodes.Add(SelectedHistoryNode)
                trvSelectedHistory.Nodes.Add(oNode)
                trvSelectedHistory.ExpandAll()
                oNode = Nothing
                trvSelectedHistory.Sort()
            End If
            ''
        Catch ex As Exception
        Finally
            trvSelectedHistory.EndUpdate()
        End Try
    End Sub

    Private Sub mnuDeleteHistory_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuDeleteHistory.Click
        If trvSelectedHistory.SelectedNode.Parent.Nodes.Count > 1 Then
            trvSelectedHistory.SelectedNode.Remove()
        Else
            trvSelectedHistory.SelectedNode.Parent.Remove()
        End If
    End Sub

    Private Sub trvSelectedHistory_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles trvSelectedHistory.MouseDown
        Try
            Dim trvNode As TreeNode
            trvNode = trvSelectedHistory.GetNodeAt(e.X, e.Y)

            trvSelectedHistory.SelectedNode = trvNode
            If e.Button = Windows.Forms.MouseButtons.Right Then
                If IsNothing(trvNode) = False Then
                    If trvNode.Level = 1 Then
                        'Try
                        '    If (IsNothing(trvSelectedHistory.ContextMenuStrip) = False) Then
                        '        trvSelectedHistory.ContextMenuStrip.Dispose()
                        '        trvSelectedHistory.ContextMenuStrip = Nothing
                        '    End If
                        'Catch ex As Exception

                        'End Try
                        trvSelectedHistory.ContextMenuStrip = ContextMenuHistory
                    Else
                        'Try
                        '    If (IsNothing(trvSelectedHistory.ContextMenuStrip) = False) Then
                        '        trvSelectedHistory.ContextMenuStrip.Dispose()
                        '        trvSelectedHistory.ContextMenuStrip = Nothing
                        '    End If
                        'Catch ex As Exception

                        'End Try
                        trvSelectedHistory.ContextMenuStrip = Nothing
                    End If
                Else
                    'Try
                    '    If (IsNothing(trvSelectedHistory.ContextMenuStrip) = False) Then
                    '        trvSelectedHistory.ContextMenuStrip.Dispose()
                    '        trvSelectedHistory.ContextMenuStrip = Nothing
                    '    End If
                    'Catch ex As Exception

                    'End Try
                    trvSelectedHistory.ContextMenuStrip = Nothing
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub GloUC_trvDrugs_NodeMouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles GloUC_trvDrugs.NodeMouseDoubleClick
        Try
            Dim oNode As gloUserControlLibrary.myTreeNode = CType(e.Node, gloUserControlLibrary.myTreeNode)
           

            Dim Ispresent As Boolean = False
            For Each myDNode As TreeNode In trvSelectedDrugs.Nodes
                If myDNode.Text = oNode.Text Then
                    Ispresent = True
                    Exit For
                End If
            Next
            If Ispresent = False Then
                Dim oNodeToAdd As New myTreeNode
                oNodeToAdd.Key = oNode.ID
                oNodeToAdd.Text = oNode.Text
                oNodeToAdd.DrugName = oNode.Code
                oNodeToAdd.Dosage = oNode.Description
                oNodeToAdd.DrugForm = oNode.DrugForm
                oNodeToAdd.Route = oNode.Route
                oNodeToAdd.Frequency = oNode.Frequency
                oNodeToAdd.NDCCode = oNode.NDCCode
                oNodeToAdd.IsNarcotics = oNode.IsNarcotics
                oNodeToAdd.Duration = oNode.Duration
                oNodeToAdd.mpid = oNode.mpid
                oNodeToAdd.DrugQtyQualifier = oNode.DrugQtyQualifier
                trvSelectedDrugs.Nodes.Add(oNodeToAdd)
                oNodeToAdd.ImageIndex = 0
                oNodeToAdd.SelectedImageIndex = 0
            End If
            trvSelectedDrugs.ExpandAll()
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub GloUC_trvDrugs_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles GloUC_trvDrugs.KeyPress
        Try
            If e.KeyChar = Chr(13) Then
                Dim oNode As gloUserControlLibrary.myTreeNode = CType(GloUC_trvDrugs.SelectedNode, gloUserControlLibrary.myTreeNode)
                Dim oNodeToAdd As New myTreeNode
                oNodeToAdd.Key = oNode.ID
                oNodeToAdd.Text = oNode.Text
                oNodeToAdd.DrugName = oNode.Code
                oNodeToAdd.Dosage = oNode.Description
                oNodeToAdd.DrugForm = oNode.DrugForm
                oNodeToAdd.Route = oNode.Route
                oNodeToAdd.Frequency = oNode.Frequency
                oNodeToAdd.NDCCode = oNode.NDCCode
                oNodeToAdd.IsNarcotics = oNode.IsNarcotics
                oNodeToAdd.Duration = oNode.Duration
                oNodeToAdd.mpid = oNode.mpid
                oNodeToAdd.DrugQtyQualifier = oNode.DrugQtyQualifier
                Dim myDrugNode As New TreeNode
                Dim Ispresent As Boolean = False
                myDrugNode = oNodeToAdd.Clone()
                For Each myDNode As TreeNode In trvSelectedDrugs.Nodes
                    If myDNode.Text = myDrugNode.Text Then
                        Ispresent = True
                        Exit For
                    End If
                Next
                If Ispresent = False Then
                    trvSelectedDrugs.Nodes.Add(myDrugNode)
                    myDrugNode.ImageIndex = 0
                    myDrugNode.SelectedImageIndex = 0
                End If
                trvSelectedDrugs.ExpandAll()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub GloUC_trvHistory_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles GloUC_trvHistory.KeyPress
        If e.KeyChar = Chr(13) Then
            Dim oNode1 As gloUserControlLibrary.myTreeNode = CType(GloUC_trvHistory.SelectedNode, gloUserControlLibrary.myTreeNode)



           


            Dim CategoryFound As Boolean = False
            Dim HistoryFound As Boolean = False

            ''Selected Current Criteria
            For Each CategoryNode As TreeNode In trvSelectedHistory.Nodes
                If CategoryNode.Text = cmbHistoryCategory.Text Then
                    For Each HistoryNode As TreeNode In CategoryNode.Nodes
                        If HistoryNode.Text = oNode1.Text Then
                            HistoryFound = True
                            Exit For
                        End If
                    Next
                    If Not HistoryFound Then
                        Dim SelectedHistoryNode As New myTreeNode
                        SelectedHistoryNode.Text = oNode1.Text
                        SelectedHistoryNode.Tag = cmbHistoryCategory.Text
                        SelectedHistoryNode.Key = oNode1.ID
                        CategoryNode.Nodes.Add(SelectedHistoryNode)
                        CategoryNode.Expand()
                        trvSelectedHistory.Sort()
                    End If
                    CategoryFound = True
                    Exit For
                End If
            Next

            If Not CategoryFound Then
                Dim oNode As TreeNode
                oNode = New TreeNode
                oNode.Text = cmbHistoryCategory.Text
                oNode.Tag = cmbHistoryCategory.SelectedValue
                oNode.ImageIndex = 0
                oNode.SelectedImageIndex = 0
                Dim SelectedHistoryNode As New myTreeNode
                SelectedHistoryNode.Text = oNode1.Text
                SelectedHistoryNode.Tag = cmbHistoryCategory.Text
                SelectedHistoryNode.Key = oNode1.ID
                oNode.Nodes.Add(SelectedHistoryNode)
                trvSelectedHistory.Nodes.Add(oNode)
                trvSelectedHistory.ExpandAll()
                oNode = Nothing
                trvSelectedHistory.Sort()
            End If
        End If
    End Sub

    Private Sub GloUC_trvHistory_NodeMouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles GloUC_trvHistory.NodeMouseDoubleClick
        Dim oNode1 As gloUserControlLibrary.myTreeNode = CType(e.Node, gloUserControlLibrary.myTreeNode)



       


        Dim CategoryFound As Boolean = False
        Dim HistoryFound As Boolean = False

        ''Selected Current Criteria
        For Each CategoryNode As TreeNode In trvSelectedHistory.Nodes
            If CategoryNode.Text = cmbHistoryCategory.Text Then
                For Each HistoryNode As TreeNode In CategoryNode.Nodes
                    If HistoryNode.Text = oNode1.Text Then
                        HistoryFound = True
                        Exit For
                    End If
                Next
                If Not HistoryFound Then
                    Dim SelectedHistoryNode As New myTreeNode
                    SelectedHistoryNode.Text = oNode1.Text
                    SelectedHistoryNode.Tag = cmbHistoryCategory.Text
                    SelectedHistoryNode.Key = oNode1.ID
                    CategoryNode.Nodes.Add(SelectedHistoryNode)
                    CategoryNode.Expand()
                    trvSelectedHistory.Sort()
                End If
                CategoryFound = True
                Exit For
            End If
        Next

        If Not CategoryFound Then
            Dim oNode As TreeNode
            oNode = New TreeNode
            oNode.Text = cmbHistoryCategory.Text
            oNode.Tag = cmbHistoryCategory.SelectedValue
            oNode.ImageIndex = 0
            oNode.SelectedImageIndex = 0
            Dim SelectedHistoryNode As New myTreeNode
            SelectedHistoryNode.Text = oNode1.Text
            SelectedHistoryNode.Tag = cmbHistoryCategory.Text
            SelectedHistoryNode.Key = oNode1.ID
            oNode.Nodes.Add(SelectedHistoryNode)
            trvSelectedHistory.Nodes.Add(oNode)
            trvSelectedHistory.ExpandAll()
            oNode = Nothing
            trvSelectedHistory.Sort()
        End If
    End Sub

    ''Sandip Darade 20090909
    Private Sub txtLabResultSearch_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtLabResultSearch.KeyUp
        Try
            ' for focus select tree view
            If e.KeyCode = Keys.Enter Then
                C1LabResult.Select()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    ''Sandip Darade 20090909
    Private Sub txtLabResultSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtLabResultSearch.TextChanged

        Dim strSearch As String
        With txtLabResultSearch
            If Trim(.Text) <> "" Then
                strSearch = Replace(.Text, "'", "''")
            Else
                strSearch = ""
            End If
        End With

        With C1LabResult
            .Row = .FindRow(strSearch, 1, COL_TestName, False, False, True)
        End With
    End Sub

    Private Sub cmbAgeMin_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cmbAgeMin.KeyPress
        'code added by dipak 2009 to allow only numaric value
        AllowNumaric(cmbAgeMin.Text.ToString, e)
    End Sub

    Private Sub cmbAgeMax_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cmbAgeMax.KeyPress
        'code added by dipak 2009 to allow only numaric value
        AllowNumaric(cmbAgeMin.Text.ToString, e)
    End Sub

    'Private Sub txtMessage_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMessage.KeyPress
    '    If Char.IsLetterOrDigit(e.KeyChar) Or (e.KeyChar = ChrW(8)) Then
    '        e.Handled = False
    '    Else
    '        e.Handled = True
    '    End If
    'End Sub

    'Private Sub txtName_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtName.KeyPress
    '    If Char.IsLetterOrDigit(e.KeyChar) Or (e.KeyChar = ChrW(8)) Then
    '        e.Handled = False
    '    Else
    '        e.Handled = True
    '    End If
    'End Sub
End Class