Imports gloEMR.gloStream.DiseaseManagement

Public Class frmDM_Setup
    Inherits System.Windows.Forms.Form
    ' page Coded by Bipin On 15/1/2007

    Private m_CriteriaId As Int64
    Private m_CriteriaName As String
    Private blnModify As Boolean = False
    Private blnIsPatientCriteria As Boolean = False
    Private m_PatientID As Int64

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
    Dim dt As New DataTable
    Dim dtdrugs As New DataTable

    Private blnhistory As Boolean = False '' use for checking whether history is opened for snomed or Problem for adding it to history tree or problem tree 
    'sarika DM Denormalization 20090402
    Private _DMNode As myTreeNode = Nothing

    ''Sandip Darade 20090415
    ''Added for ICD9
    Dim objICD9AssociationDBLayer As ClsICD9AssociationDBLayer
    Dim dtICD9 As DataTable
    Dim dtCPT As DataTable
    Private Const strSortByCode As String = "CODE"
    Private Const strSortByDesc As String = "DESC"
    Dim strParentToAssociate As String = "Labs"
    '' chetan added on 09 Oct 2010
    Dim LoadFirst As Boolean = False
    Dim oSnoMed As gloSnoMed.ClsGeneral
    Dim _IsValid As Boolean = False
    Dim _IsValidate As Boolean = True
    Public Property DMSelectedNode() As myTreeNode
        Get
            Return _DMNode
        End Get
        Set(ByVal value As myTreeNode)
            _DMNode = value
        End Set
    End Property
    '---



#Region " Windows Controls "
    Friend WithEvents cntFindingsprb As System.Windows.Forms.ContextMenuStrip
   
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Friend WithEvents btnLabs As System.Windows.Forms.Button
    Friend WithEvents C1LabResult As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents pnl_tlstrip As System.Windows.Forms.Panel
    Friend WithEvents pnlRight As System.Windows.Forms.Panel
    Friend WithEvents btnLab As System.Windows.Forms.Button
    Friend WithEvents btnRadiologyTest As System.Windows.Forms.Button
    Friend WithEvents btnGuideline As System.Windows.Forms.Button
    Friend WithEvents pnlSummary As System.Windows.Forms.Panel
    Friend WithEvents pnlSummaryHeader As System.Windows.Forms.Panel
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txt_summary As System.Windows.Forms.TextBox
    Friend WithEvents pnlGuideline As System.Windows.Forms.Panel
    Friend WithEvents pnl3 As System.Windows.Forms.Panel
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents tlsDM As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents tlsDM_Save As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlsDM_Close As System.Windows.Forms.ToolStripButton
    Friend WithEvents pnlMsg As System.Windows.Forms.Panel
    Friend WithEvents pnlDemoVitals As System.Windows.Forms.Panel
    Friend WithEvents pnlLab As System.Windows.Forms.Panel
    Friend WithEvents btnReferrals As System.Windows.Forms.Button
    Friend WithEvents btnRx As System.Windows.Forms.Button
    Friend WithEvents trOrderInfo As System.Windows.Forms.TreeView
    Friend WithEvents CntConditions As System.Windows.Forms.ContextMenu
    Friend WithEvents mnuDelete As System.Windows.Forms.MenuItem
    Friend WithEvents mnuReferral As System.Windows.Forms.MenuItem
    Friend WithEvents EditReferral As System.Windows.Forms.MenuItem
    Friend WithEvents btnHistorySearch As System.Windows.Forms.Button
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
    Friend WithEvents sptRight As System.Windows.Forms.Splitter
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
    Friend WithEvents pnlGuidelineHeader As System.Windows.Forms.Panel
    Private WithEvents Label95 As System.Windows.Forms.Label
    Private WithEvents Label96 As System.Windows.Forms.Label
    Private WithEvents Label97 As System.Windows.Forms.Label
    Private WithEvents Label98 As System.Windows.Forms.Label
    Private WithEvents Label99 As System.Windows.Forms.Label
    Private WithEvents Label100 As System.Windows.Forms.Label
    Private WithEvents Label101 As System.Windows.Forms.Label
    Private WithEvents Label102 As System.Windows.Forms.Label
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
    Friend WithEvents Panel8 As System.Windows.Forms.Panel
    Private WithEvents Label120 As System.Windows.Forms.Label
    Private WithEvents Label121 As System.Windows.Forms.Label
    Private WithEvents Label122 As System.Windows.Forms.Label
    Private WithEvents Label123 As System.Windows.Forms.Label
    Friend WithEvents Panel9 As System.Windows.Forms.Panel
    Friend WithEvents Panel12 As System.Windows.Forms.Panel
    Private WithEvents Label146 As System.Windows.Forms.Label
    Private WithEvents Label147 As System.Windows.Forms.Label
    Private WithEvents Label148 As System.Windows.Forms.Label
    Private WithEvents Label149 As System.Windows.Forms.Label
    Friend WithEvents Panel13 As System.Windows.Forms.Panel
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
    Friend WithEvents pnltrvSelectedDrugs As System.Windows.Forms.Panel
    Private WithEvents Label86 As System.Windows.Forms.Label
    Private WithEvents Label180 As System.Windows.Forms.Label
    Friend WithEvents trvSelectedDrugs As System.Windows.Forms.TreeView
    Private WithEvents Label181 As System.Windows.Forms.Label
    Private WithEvents Label182 As System.Windows.Forms.Label
    Friend WithEvents pnlSelectedDrugLabel As System.Windows.Forms.Panel
    Private WithEvents Label183 As System.Windows.Forms.Label
    Private WithEvents Label184 As System.Windows.Forms.Label
    Private WithEvents Label185 As System.Windows.Forms.Label
    Private WithEvents Label186 As System.Windows.Forms.Label
    Friend WithEvents Label187 As System.Windows.Forms.Label
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents mnuDeleteDrugs As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents Panel11 As System.Windows.Forms.Panel
    Friend WithEvents cmbHistoryCategory As System.Windows.Forms.ComboBox
    Friend WithEvents Panel20 As System.Windows.Forms.Panel
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
    Friend WithEvents ContextMenuHistory As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents GloUC_trvCPT As gloUserControlLibrary.gloUC_TreeView
    Friend WithEvents GloUC_trvICD9 As gloUserControlLibrary.gloUC_TreeView
    Friend WithEvents pnlSelecteCPTsLabels As System.Windows.Forms.Panel
    Friend WithEvents Panel28 As System.Windows.Forms.Panel
    Private WithEvents Label209 As System.Windows.Forms.Label
    Private WithEvents Label210 As System.Windows.Forms.Label
    Private WithEvents Label211 As System.Windows.Forms.Label
    Friend WithEvents Label212 As System.Windows.Forms.Label
    Private WithEvents Label213 As System.Windows.Forms.Label
    Friend WithEvents pnlSelectedCPTs As System.Windows.Forms.Panel
    Private WithEvents Label205 As System.Windows.Forms.Label
    Private WithEvents Label206 As System.Windows.Forms.Label
    Friend WithEvents trvselectedCPT As System.Windows.Forms.TreeView
    Private WithEvents Label207 As System.Windows.Forms.Label
    Private WithEvents Label208 As System.Windows.Forms.Label
    Friend WithEvents Panel15 As System.Windows.Forms.Panel
    Private WithEvents Label132 As System.Windows.Forms.Label
    Private WithEvents Label133 As System.Windows.Forms.Label
    Friend WithEvents trvselecteICDs As System.Windows.Forms.TreeView
    Private WithEvents Label134 As System.Windows.Forms.Label
    Private WithEvents Label135 As System.Windows.Forms.Label
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents Panel10 As System.Windows.Forms.Panel
    Private WithEvents Label13 As System.Windows.Forms.Label
    Private WithEvents Label128 As System.Windows.Forms.Label
    Private WithEvents Label129 As System.Windows.Forms.Label
    Friend WithEvents Label130 As System.Windows.Forms.Label
    Private WithEvents Label131 As System.Windows.Forms.Label
    Friend WithEvents GloUC_trvDrugs As gloUserControlLibrary.gloUC_TreeView
    Friend WithEvents GloUC_trvAssociates As gloUserControlLibrary.gloUC_TreeView
    Friend WithEvents CmnuStripCPT As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents mnuItem_DeleteCPT As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CmnustripICD As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents mnuItem_DeleteICD As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents GloUC_trvHistory As gloUserControlLibrary.gloUC_TreeView
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
    Friend WithEvents Panel16 As System.Windows.Forms.Panel
    Friend WithEvents Splitter3 As System.Windows.Forms.Splitter
    Friend WithEvents Splitter4 As System.Windows.Forms.Splitter
    Private WithEvents Label8 As System.Windows.Forms.Label
    Private WithEvents Label14 As System.Windows.Forms.Label
    Private WithEvents Label115 As System.Windows.Forms.Label
    Private WithEvents Label116 As System.Windows.Forms.Label
    Friend WithEvents Splitter5 As System.Windows.Forms.Splitter
    Friend WithEvents Label125 As System.Windows.Forms.Label
    Friend WithEvents Label124 As System.Windows.Forms.Label
    Friend WithEvents Label119 As System.Windows.Forms.Label
    Friend WithEvents Label118 As System.Windows.Forms.Label
    Friend WithEvents Label117 As System.Windows.Forms.Label
    Friend WithEvents Panel23 As System.Windows.Forms.Panel
    Friend WithEvents txtLabResultSearch As System.Windows.Forms.TextBox
    Friend WithEvents Label126 As System.Windows.Forms.Label
    Friend WithEvents Label127 As System.Windows.Forms.Label
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Private WithEvents Label136 As System.Windows.Forms.Label
    Private WithEvents Label137 As System.Windows.Forms.Label
    Private WithEvents Label138 As System.Windows.Forms.Label
    Private WithEvents Label139 As System.Windows.Forms.Label
    Friend WithEvents Label140 As System.Windows.Forms.Label
    Friend WithEvents Label141 As System.Windows.Forms.Label
    Friend WithEvents btnLabResultClear As System.Windows.Forms.Button
    Friend WithEvents btnLabClear As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents pnlIM As System.Windows.Forms.Panel
    Friend WithEvents btnIM As System.Windows.Forms.Button
    Private WithEvents Label145 As System.Windows.Forms.Label
    Private WithEvents Label159 As System.Windows.Forms.Label
    Private WithEvents Label160 As System.Windows.Forms.Label
    Private WithEvents Label161 As System.Windows.Forms.Label
    Private WithEvents pnlbtnProblemlist As System.Windows.Forms.Panel
    Friend WithEvents btnproblemlist As System.Windows.Forms.Button
    Private WithEvents Label162 As System.Windows.Forms.Label
    Private WithEvents Label163 As System.Windows.Forms.Label
    Private WithEvents Label164 As System.Windows.Forms.Label
    Private WithEvents Label178 As System.Windows.Forms.Label
    Friend WithEvents PnlProblemList As System.Windows.Forms.Panel
    Friend WithEvents PnlProblemMiddle As System.Windows.Forms.Panel
    Friend WithEvents PnlProblemSearch As System.Windows.Forms.Panel
    Friend WithEvents Panel26 As System.Windows.Forms.Panel
    Friend WithEvents trvprobright As System.Windows.Forms.TreeView
    Friend WithEvents PnlProbLeft As System.Windows.Forms.Panel
    Private WithEvents Label179 As System.Windows.Forms.Label
    Private WithEvents Label197 As System.Windows.Forms.Label
    Private WithEvents Label198 As System.Windows.Forms.Label
    Private WithEvents Label199 As System.Windows.Forms.Label
    Friend WithEvents trvproblem As System.Windows.Forms.TreeView
    Friend WithEvents Pnlsnomedprb As System.Windows.Forms.Panel
    Friend WithEvents pnltrvfinprob As System.Windows.Forms.Panel
    Friend WithEvents trvfinprob As System.Windows.Forms.TreeView
    Friend WithEvents Panel31 As System.Windows.Forms.Panel
    Friend WithEvents Label201 As System.Windows.Forms.Label
    Private WithEvents Label203 As System.Windows.Forms.Label
    Friend WithEvents PnlSrchProb As System.Windows.Forms.Panel
    Friend WithEvents txtsrchprb As System.Windows.Forms.TextBox
    'Friend WithEvents ContextMenuProblem As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents Label215 As System.Windows.Forms.Label
    Friend WithEvents Label216 As System.Windows.Forms.Label
    Friend WithEvents btnclrprb As System.Windows.Forms.Button
    Friend WithEvents PictureBox3 As System.Windows.Forms.PictureBox
    Private WithEvents Label217 As System.Windows.Forms.Label
    Private WithEvents Label218 As System.Windows.Forms.Label
    Private WithEvents Label219 As System.Windows.Forms.Label
    Private WithEvents Label220 As System.Windows.Forms.Label
    Friend WithEvents Splitter6 As System.Windows.Forms.Splitter
    Friend WithEvents pnltrvsubprb As System.Windows.Forms.Panel
    Private WithEvents Label221 As System.Windows.Forms.Label
    Friend WithEvents trvsubprb As System.Windows.Forms.TreeView
    Friend WithEvents Panel34 As System.Windows.Forms.Panel
    Friend WithEvents Label222 As System.Windows.Forms.Label
    Private WithEvents Label223 As System.Windows.Forms.Label
    Private WithEvents Label224 As System.Windows.Forms.Label
    Private WithEvents Label225 As System.Windows.Forms.Label
    Private WithEvents Label226 As System.Windows.Forms.Label
    Friend WithEvents Panel35 As System.Windows.Forms.Panel
    Friend WithEvents Panel36 As System.Windows.Forms.Panel
    Private WithEvents Label227 As System.Windows.Forms.Label
    Private WithEvents Label228 As System.Windows.Forms.Label
    Private WithEvents Label229 As System.Windows.Forms.Label
    Friend WithEvents Label230 As System.Windows.Forms.Label
    Private WithEvents Label231 As System.Windows.Forms.Label
    Friend WithEvents Panel37 As System.Windows.Forms.Panel
    Private WithEvents Label232 As System.Windows.Forms.Label
    Private WithEvents Label233 As System.Windows.Forms.Label
    Friend WithEvents trvselectedprob As System.Windows.Forms.TreeView
    Private WithEvents Label234 As System.Windows.Forms.Label
    Private WithEvents Label235 As System.Windows.Forms.Label
    Friend WithEvents Panel38 As System.Windows.Forms.Panel
    Friend WithEvents Panel39 As System.Windows.Forms.Panel
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Private WithEvents Label237 As System.Windows.Forms.Label
    Private WithEvents Label238 As System.Windows.Forms.Label
    Private WithEvents Label239 As System.Windows.Forms.Label
    Private WithEvents Label240 As System.Windows.Forms.Label
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents mnuDeleteHistory As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cntFindings As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents mnu_AddFindings As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuDeleteProblem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents pnltrvSnowmedOff As System.Windows.Forms.Panel
    Private WithEvents Label142 As System.Windows.Forms.Label
    Friend WithEvents trvSnowmedOff As System.Windows.Forms.TreeView
    Friend WithEvents Panel24 As System.Windows.Forms.Panel
    Friend WithEvents Label143 As System.Windows.Forms.Label
    Private WithEvents Label144 As System.Windows.Forms.Label
    Private WithEvents Label150 As System.Windows.Forms.Label
    Private WithEvents Label151 As System.Windows.Forms.Label
    Private WithEvents Label152 As System.Windows.Forms.Label
    Private WithEvents Label154 As System.Windows.Forms.Label
    Private WithEvents Label153 As System.Windows.Forms.Label
    Friend WithEvents trvselectedhist As System.Windows.Forms.TreeView
    Friend WithEvents cmbhistsnomed As System.Windows.Forms.ComboBox
    Friend WithEvents lblsnohistcat As System.Windows.Forms.Label
    Friend WithEvents cmbAgeMaxMnth As System.Windows.Forms.ComboBox
    Friend WithEvents cmbAgeMinMnth As System.Windows.Forms.ComboBox
    Friend WithEvents ContextMenuProblem As System.Windows.Forms.ContextMenuStrip
    '' chetan integrated for snomed problem list
    ' Friend WithEvents mnuDeleteProblem As System.Windows.Forms.ToolStripMenuItem
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
    Public Sub New(ByVal PatientID As Int64, ByVal IsPatientCriteria As Boolean)
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()
        m_PatientID = PatientID
        blnIsPatientCriteria = IsPatientCriteria
    End Sub


    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            Try
                gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
            Catch
            End Try

            If Not (components Is Nothing) Then
                components.Dispose()
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
    Friend WithEvents pnlHistory As System.Windows.Forms.Panel
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents pnlHistoryLeft As System.Windows.Forms.Panel
    Friend WithEvents trvHistory As System.Windows.Forms.TreeView
    Friend WithEvents trvHistoryRight As System.Windows.Forms.TreeView
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmDM_Setup))
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.pnlMiddle = New System.Windows.Forms.Panel()
        Me.pnlDemoVitals = New System.Windows.Forms.Panel()
        Me.pnlVitals = New System.Windows.Forms.Panel()
        Me.Label111 = New System.Windows.Forms.Label()
        Me.Label112 = New System.Windows.Forms.Label()
        Me.Label113 = New System.Windows.Forms.Label()
        Me.Label114 = New System.Windows.Forms.Label()
        Me.txtHeightMin = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtHeightMaxInch = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtHeightMinInch = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtPulseOXmax = New System.Windows.Forms.TextBox()
        Me.lblPulseOXMax = New System.Windows.Forms.Label()
        Me.txtPulseOXmin = New System.Windows.Forms.TextBox()
        Me.lblPulseOXMin = New System.Windows.Forms.Label()
        Me.txtPulseMax = New System.Windows.Forms.TextBox()
        Me.lblPulseMax = New System.Windows.Forms.Label()
        Me.txtPulseMin = New System.Windows.Forms.TextBox()
        Me.lblPulseMin = New System.Windows.Forms.Label()
        Me.txtTemperatureMax = New System.Windows.Forms.TextBox()
        Me.lblTempratureMax = New System.Windows.Forms.Label()
        Me.txtBMImax = New System.Windows.Forms.TextBox()
        Me.lblBMImax = New System.Windows.Forms.Label()
        Me.txtBMImin = New System.Windows.Forms.TextBox()
        Me.lblBMImin = New System.Windows.Forms.Label()
        Me.txtWeightMax = New System.Windows.Forms.TextBox()
        Me.lblWeightMax = New System.Windows.Forms.Label()
        Me.txtHeightMax = New System.Windows.Forms.TextBox()
        Me.lblHeightMax = New System.Windows.Forms.Label()
        Me.txtBPstandingMax = New System.Windows.Forms.TextBox()
        Me.txtBPstandingMin = New System.Windows.Forms.TextBox()
        Me.txtBPsettingMax = New System.Windows.Forms.TextBox()
        Me.txtBPsettingMin = New System.Windows.Forms.TextBox()
        Me.lblBPStandingMax = New System.Windows.Forms.Label()
        Me.lblBPStandingMin = New System.Windows.Forms.Label()
        Me.lblBPSittingMax = New System.Windows.Forms.Label()
        Me.lblBPSittingMin = New System.Windows.Forms.Label()
        Me.txtTemperatureMin = New System.Windows.Forms.TextBox()
        Me.txtWeightMin = New System.Windows.Forms.TextBox()
        Me.lblWeightMin = New System.Windows.Forms.Label()
        Me.lblTempratureMin = New System.Windows.Forms.Label()
        Me.lblHeightMin = New System.Windows.Forms.Label()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.Label107 = New System.Windows.Forms.Label()
        Me.Label108 = New System.Windows.Forms.Label()
        Me.Label109 = New System.Windows.Forms.Label()
        Me.Label110 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.pnlDemographics = New System.Windows.Forms.Panel()
        Me.cmbAgeMaxMnth = New System.Windows.Forms.ComboBox()
        Me.cmbAgeMinMnth = New System.Windows.Forms.ComboBox()
        Me.Label125 = New System.Windows.Forms.Label()
        Me.Label124 = New System.Windows.Forms.Label()
        Me.Label119 = New System.Windows.Forms.Label()
        Me.Label103 = New System.Windows.Forms.Label()
        Me.Label104 = New System.Windows.Forms.Label()
        Me.Label105 = New System.Windows.Forms.Label()
        Me.Label106 = New System.Windows.Forms.Label()
        Me.lblEmpStatus = New System.Windows.Forms.Label()
        Me.cmbState = New System.Windows.Forms.ComboBox()
        Me.cmbEmpStatus = New System.Windows.Forms.ComboBox()
        Me.txtZip = New System.Windows.Forms.TextBox()
        Me.txtCity = New System.Windows.Forms.TextBox()
        Me.lblZip = New System.Windows.Forms.Label()
        Me.lblState = New System.Windows.Forms.Label()
        Me.lblCity = New System.Windows.Forms.Label()
        Me.cmbMaritalSt = New System.Windows.Forms.ComboBox()
        Me.cmbRace = New System.Windows.Forms.ComboBox()
        Me.cmbAgeMax = New System.Windows.Forms.ComboBox()
        Me.cmbAgeMin = New System.Windows.Forms.ComboBox()
        Me.cmbGender = New System.Windows.Forms.ComboBox()
        Me.lblAgeMax = New System.Windows.Forms.Label()
        Me.lblMaritalStatus = New System.Windows.Forms.Label()
        Me.lblGender = New System.Windows.Forms.Label()
        Me.lblRace = New System.Windows.Forms.Label()
        Me.lblAgeMin = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.Label65 = New System.Windows.Forms.Label()
        Me.Label85 = New System.Windows.Forms.Label()
        Me.Label89 = New System.Windows.Forms.Label()
        Me.Label90 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.PnlProblemList = New System.Windows.Forms.Panel()
        Me.PnlProblemMiddle = New System.Windows.Forms.Panel()
        Me.Pnlsnomedprb = New System.Windows.Forms.Panel()
        Me.pnltrvSnowmedOff = New System.Windows.Forms.Panel()
        Me.Label142 = New System.Windows.Forms.Label()
        Me.trvSnowmedOff = New System.Windows.Forms.TreeView()
        Me.Panel24 = New System.Windows.Forms.Panel()
        Me.Label143 = New System.Windows.Forms.Label()
        Me.Label144 = New System.Windows.Forms.Label()
        Me.Label150 = New System.Windows.Forms.Label()
        Me.Label151 = New System.Windows.Forms.Label()
        Me.Label152 = New System.Windows.Forms.Label()
        Me.pnltrvfinprob = New System.Windows.Forms.Panel()
        Me.trvfinprob = New System.Windows.Forms.TreeView()
        Me.Panel31 = New System.Windows.Forms.Panel()
        Me.Label154 = New System.Windows.Forms.Label()
        Me.Label153 = New System.Windows.Forms.Label()
        Me.Label201 = New System.Windows.Forms.Label()
        Me.Label203 = New System.Windows.Forms.Label()
        Me.PnlSrchProb = New System.Windows.Forms.Panel()
        Me.txtsrchprb = New System.Windows.Forms.TextBox()
        Me.Label215 = New System.Windows.Forms.Label()
        Me.Label216 = New System.Windows.Forms.Label()
        Me.btnclrprb = New System.Windows.Forms.Button()
        Me.PictureBox3 = New System.Windows.Forms.PictureBox()
        Me.Label217 = New System.Windows.Forms.Label()
        Me.Label218 = New System.Windows.Forms.Label()
        Me.Label219 = New System.Windows.Forms.Label()
        Me.Label220 = New System.Windows.Forms.Label()
        Me.Splitter6 = New System.Windows.Forms.Splitter()
        Me.pnltrvsubprb = New System.Windows.Forms.Panel()
        Me.Label221 = New System.Windows.Forms.Label()
        Me.trvsubprb = New System.Windows.Forms.TreeView()
        Me.Panel34 = New System.Windows.Forms.Panel()
        Me.Label222 = New System.Windows.Forms.Label()
        Me.Label223 = New System.Windows.Forms.Label()
        Me.Label224 = New System.Windows.Forms.Label()
        Me.Label225 = New System.Windows.Forms.Label()
        Me.Label226 = New System.Windows.Forms.Label()
        Me.PnlProblemSearch = New System.Windows.Forms.Panel()
        Me.Panel26 = New System.Windows.Forms.Panel()
        Me.trvprobright = New System.Windows.Forms.TreeView()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.PnlProbLeft = New System.Windows.Forms.Panel()
        Me.Label179 = New System.Windows.Forms.Label()
        Me.Label197 = New System.Windows.Forms.Label()
        Me.Label198 = New System.Windows.Forms.Label()
        Me.Label199 = New System.Windows.Forms.Label()
        Me.trvproblem = New System.Windows.Forms.TreeView()
        Me.Panel35 = New System.Windows.Forms.Panel()
        Me.Panel36 = New System.Windows.Forms.Panel()
        Me.Label227 = New System.Windows.Forms.Label()
        Me.Label228 = New System.Windows.Forms.Label()
        Me.Label229 = New System.Windows.Forms.Label()
        Me.Label230 = New System.Windows.Forms.Label()
        Me.Label231 = New System.Windows.Forms.Label()
        Me.Panel37 = New System.Windows.Forms.Panel()
        Me.trvselectedhist = New System.Windows.Forms.TreeView()
        Me.Label232 = New System.Windows.Forms.Label()
        Me.Label233 = New System.Windows.Forms.Label()
        Me.trvselectedprob = New System.Windows.Forms.TreeView()
        Me.Label234 = New System.Windows.Forms.Label()
        Me.Label235 = New System.Windows.Forms.Label()
        Me.Panel38 = New System.Windows.Forms.Panel()
        Me.Panel39 = New System.Windows.Forms.Panel()
        Me.cmbhistsnomed = New System.Windows.Forms.ComboBox()
        Me.lblsnohistcat = New System.Windows.Forms.Label()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.Label237 = New System.Windows.Forms.Label()
        Me.Label238 = New System.Windows.Forms.Label()
        Me.Label239 = New System.Windows.Forms.Label()
        Me.Label240 = New System.Windows.Forms.Label()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.pnlRadiology = New System.Windows.Forms.Panel()
        Me.Panel18 = New System.Windows.Forms.Panel()
        Me.Label170 = New System.Windows.Forms.Label()
        Me.Label171 = New System.Windows.Forms.Label()
        Me.Label172 = New System.Windows.Forms.Label()
        Me.Label173 = New System.Windows.Forms.Label()
        Me.c1Labs = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.Panel17 = New System.Windows.Forms.Panel()
        Me.txtLabsSearch = New System.Windows.Forms.TextBox()
        Me.btnLabClear = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label165 = New System.Windows.Forms.Label()
        Me.PictureBox6 = New System.Windows.Forms.PictureBox()
        Me.Label166 = New System.Windows.Forms.Label()
        Me.Label167 = New System.Windows.Forms.Label()
        Me.Label168 = New System.Windows.Forms.Label()
        Me.Label169 = New System.Windows.Forms.Label()
        Me.pnlHistory = New System.Windows.Forms.Panel()
        Me.Panel22 = New System.Windows.Forms.Panel()
        Me.Label193 = New System.Windows.Forms.Label()
        Me.Label194 = New System.Windows.Forms.Label()
        Me.trvSelectedHistory = New System.Windows.Forms.TreeView()
        Me.Label195 = New System.Windows.Forms.Label()
        Me.Label196 = New System.Windows.Forms.Label()
        Me.Panel20 = New System.Windows.Forms.Panel()
        Me.Panel21 = New System.Windows.Forms.Panel()
        Me.Label188 = New System.Windows.Forms.Label()
        Me.Label189 = New System.Windows.Forms.Label()
        Me.Label190 = New System.Windows.Forms.Label()
        Me.Label191 = New System.Windows.Forms.Label()
        Me.Label192 = New System.Windows.Forms.Label()
        Me.Panel16 = New System.Windows.Forms.Panel()
        Me.Panel9 = New System.Windows.Forms.Panel()
        Me.GloUC_trvHistory = New gloUserControlLibrary.gloUC_TreeView()
        Me.trvHistoryRight = New System.Windows.Forms.TreeView()
        Me.pnlHistoryLeft = New System.Windows.Forms.Panel()
        Me.Label120 = New System.Windows.Forms.Label()
        Me.Label121 = New System.Windows.Forms.Label()
        Me.Label122 = New System.Windows.Forms.Label()
        Me.Label123 = New System.Windows.Forms.Label()
        Me.trvHistory = New System.Windows.Forms.TreeView()
        Me.Panel11 = New System.Windows.Forms.Panel()
        Me.Panel8 = New System.Windows.Forms.Panel()
        Me.cmbHistoryCategory = New System.Windows.Forms.ComboBox()
        Me.Label140 = New System.Windows.Forms.Label()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label115 = New System.Windows.Forms.Label()
        Me.Label116 = New System.Windows.Forms.Label()
        Me.btnHistorySearch = New System.Windows.Forms.Button()
        Me.pnlICD9 = New System.Windows.Forms.Panel()
        Me.Panel15 = New System.Windows.Forms.Panel()
        Me.Label132 = New System.Windows.Forms.Label()
        Me.Label133 = New System.Windows.Forms.Label()
        Me.trvselecteICDs = New System.Windows.Forms.TreeView()
        Me.Label134 = New System.Windows.Forms.Label()
        Me.Label135 = New System.Windows.Forms.Label()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Panel10 = New System.Windows.Forms.Panel()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label128 = New System.Windows.Forms.Label()
        Me.Label129 = New System.Windows.Forms.Label()
        Me.Label130 = New System.Windows.Forms.Label()
        Me.Label131 = New System.Windows.Forms.Label()
        Me.Splitter4 = New System.Windows.Forms.Splitter()
        Me.GloUC_trvICD9 = New gloUserControlLibrary.gloUC_TreeView()
        Me.pnlCPT = New System.Windows.Forms.Panel()
        Me.pnlSelectedCPTs = New System.Windows.Forms.Panel()
        Me.Label205 = New System.Windows.Forms.Label()
        Me.Label206 = New System.Windows.Forms.Label()
        Me.trvselectedCPT = New System.Windows.Forms.TreeView()
        Me.Label207 = New System.Windows.Forms.Label()
        Me.Label208 = New System.Windows.Forms.Label()
        Me.pnlSelecteCPTsLabels = New System.Windows.Forms.Panel()
        Me.Panel28 = New System.Windows.Forms.Panel()
        Me.Label209 = New System.Windows.Forms.Label()
        Me.Label210 = New System.Windows.Forms.Label()
        Me.Label211 = New System.Windows.Forms.Label()
        Me.Label212 = New System.Windows.Forms.Label()
        Me.Label213 = New System.Windows.Forms.Label()
        Me.Splitter1 = New System.Windows.Forms.Splitter()
        Me.GloUC_trvCPT = New gloUserControlLibrary.gloUC_TreeView()
        Me.pnlDrugs = New System.Windows.Forms.Panel()
        Me.pnltrvSelectedDrugs = New System.Windows.Forms.Panel()
        Me.Label86 = New System.Windows.Forms.Label()
        Me.Label180 = New System.Windows.Forms.Label()
        Me.trvSelectedDrugs = New System.Windows.Forms.TreeView()
        Me.Label181 = New System.Windows.Forms.Label()
        Me.Label182 = New System.Windows.Forms.Label()
        Me.pnlSelectedDrugLabel = New System.Windows.Forms.Panel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Label185 = New System.Windows.Forms.Label()
        Me.Label184 = New System.Windows.Forms.Label()
        Me.Label186 = New System.Windows.Forms.Label()
        Me.Label187 = New System.Windows.Forms.Label()
        Me.Label183 = New System.Windows.Forms.Label()
        Me.Splitter3 = New System.Windows.Forms.Splitter()
        Me.GloUC_trvDrugs = New gloUserControlLibrary.gloUC_TreeView()
        Me.pnlLab = New System.Windows.Forms.Panel()
        Me.Panel12 = New System.Windows.Forms.Panel()
        Me.Label146 = New System.Windows.Forms.Label()
        Me.C1LabResult = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.Label147 = New System.Windows.Forms.Label()
        Me.Label148 = New System.Windows.Forms.Label()
        Me.Label149 = New System.Windows.Forms.Label()
        Me.Panel13 = New System.Windows.Forms.Panel()
        Me.Panel23 = New System.Windows.Forms.Panel()
        Me.txtLabResultSearch = New System.Windows.Forms.TextBox()
        Me.btnLabResultClear = New System.Windows.Forms.Button()
        Me.Label126 = New System.Windows.Forms.Label()
        Me.Label127 = New System.Windows.Forms.Label()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.Label136 = New System.Windows.Forms.Label()
        Me.Label137 = New System.Windows.Forms.Label()
        Me.Label138 = New System.Windows.Forms.Label()
        Me.Label139 = New System.Windows.Forms.Label()
        Me.pnlSummaryOthers = New System.Windows.Forms.Panel()
        Me.pnlGuideline = New System.Windows.Forms.Panel()
        Me.Label99 = New System.Windows.Forms.Label()
        Me.Label100 = New System.Windows.Forms.Label()
        Me.Label101 = New System.Windows.Forms.Label()
        Me.Label102 = New System.Windows.Forms.Label()
        Me.trOrderInfo = New System.Windows.Forms.TreeView()
        Me.pnlGuidelineHeader = New System.Windows.Forms.Panel()
        Me.pnl3 = New System.Windows.Forms.Panel()
        Me.Label95 = New System.Windows.Forms.Label()
        Me.Label96 = New System.Windows.Forms.Label()
        Me.Label97 = New System.Windows.Forms.Label()
        Me.Label98 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Splitter5 = New System.Windows.Forms.Splitter()
        Me.pnlSummary = New System.Windows.Forms.Panel()
        Me.Label91 = New System.Windows.Forms.Label()
        Me.Label92 = New System.Windows.Forms.Label()
        Me.Label93 = New System.Windows.Forms.Label()
        Me.Label94 = New System.Windows.Forms.Label()
        Me.txt_summary = New System.Windows.Forms.TextBox()
        Me.pnlSummaryHeader = New System.Windows.Forms.Panel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label64 = New System.Windows.Forms.Label()
        Me.Label87 = New System.Windows.Forms.Label()
        Me.Label88 = New System.Windows.Forms.Label()
        Me.sptLeft = New System.Windows.Forms.Splitter()
        Me.pnlLeft = New System.Windows.Forms.Panel()
        Me.Panel19 = New System.Windows.Forms.Panel()
        Me.Label174 = New System.Windows.Forms.Label()
        Me.Label175 = New System.Windows.Forms.Label()
        Me.Label176 = New System.Windows.Forms.Label()
        Me.Label177 = New System.Windows.Forms.Label()
        Me.pnlbtnOrders = New System.Windows.Forms.Panel()
        Me.btnOrders = New System.Windows.Forms.Button()
        Me.Label51 = New System.Windows.Forms.Label()
        Me.Label52 = New System.Windows.Forms.Label()
        Me.Label53 = New System.Windows.Forms.Label()
        Me.Label54 = New System.Windows.Forms.Label()
        Me.pnlbtnRadiology = New System.Windows.Forms.Panel()
        Me.btnRadiology = New System.Windows.Forms.Button()
        Me.Label47 = New System.Windows.Forms.Label()
        Me.Label48 = New System.Windows.Forms.Label()
        Me.Label49 = New System.Windows.Forms.Label()
        Me.Label50 = New System.Windows.Forms.Label()
        Me.pnlbtnLabs = New System.Windows.Forms.Panel()
        Me.btnLabs = New System.Windows.Forms.Button()
        Me.Label43 = New System.Windows.Forms.Label()
        Me.Label44 = New System.Windows.Forms.Label()
        Me.Label45 = New System.Windows.Forms.Label()
        Me.Label46 = New System.Windows.Forms.Label()
        Me.pnlbtnCPT = New System.Windows.Forms.Panel()
        Me.btnCPT = New System.Windows.Forms.Button()
        Me.Label39 = New System.Windows.Forms.Label()
        Me.Label40 = New System.Windows.Forms.Label()
        Me.Label41 = New System.Windows.Forms.Label()
        Me.Label42 = New System.Windows.Forms.Label()
        Me.pnlbtnICD9 = New System.Windows.Forms.Panel()
        Me.btnICD9 = New System.Windows.Forms.Button()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.pnlbtnDrugs = New System.Windows.Forms.Panel()
        Me.btnDrugs = New System.Windows.Forms.Button()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.pnlbtnProblemlist = New System.Windows.Forms.Panel()
        Me.btnproblemlist = New System.Windows.Forms.Button()
        Me.Label162 = New System.Windows.Forms.Label()
        Me.Label163 = New System.Windows.Forms.Label()
        Me.Label164 = New System.Windows.Forms.Label()
        Me.Label178 = New System.Windows.Forms.Label()
        Me.pnlbtnHistory = New System.Windows.Forms.Panel()
        Me.btnHistory = New System.Windows.Forms.Button()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.pnbtnDemographics = New System.Windows.Forms.Panel()
        Me.btnDemographics = New System.Windows.Forms.Button()
        Me.label58 = New System.Windows.Forms.Label()
        Me.label59 = New System.Windows.Forms.Label()
        Me.label60 = New System.Windows.Forms.Label()
        Me.label61 = New System.Windows.Forms.Label()
        Me.sptRight = New System.Windows.Forms.Splitter()
        Me.pnlRight = New System.Windows.Forms.Panel()
        Me.GloUC_trvAssociates = New gloUserControlLibrary.gloUC_TreeView()
        Me.pnltrvTriggers = New System.Windows.Forms.Panel()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label74 = New System.Windows.Forms.Label()
        Me.Label75 = New System.Windows.Forms.Label()
        Me.Label76 = New System.Windows.Forms.Label()
        Me.Label77 = New System.Windows.Forms.Label()
        Me.pnltxtSearchOrder = New System.Windows.Forms.Panel()
        Me.txtSearchOrder = New System.Windows.Forms.TextBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label82 = New System.Windows.Forms.Label()
        Me.Label83 = New System.Windows.Forms.Label()
        Me.Label84 = New System.Windows.Forms.Label()
        Me.pnlbtnLab = New System.Windows.Forms.Panel()
        Me.btnLab = New System.Windows.Forms.Button()
        Me.Label78 = New System.Windows.Forms.Label()
        Me.Label79 = New System.Windows.Forms.Label()
        Me.Label80 = New System.Windows.Forms.Label()
        Me.Label81 = New System.Windows.Forms.Label()
        Me.pnlbtnReferrals = New System.Windows.Forms.Panel()
        Me.btnReferrals = New System.Windows.Forms.Button()
        Me.Label70 = New System.Windows.Forms.Label()
        Me.Label71 = New System.Windows.Forms.Label()
        Me.Label72 = New System.Windows.Forms.Label()
        Me.Label73 = New System.Windows.Forms.Label()
        Me.pnlbtnRx = New System.Windows.Forms.Panel()
        Me.btnRx = New System.Windows.Forms.Button()
        Me.Label66 = New System.Windows.Forms.Label()
        Me.Label67 = New System.Windows.Forms.Label()
        Me.Label68 = New System.Windows.Forms.Label()
        Me.Label69 = New System.Windows.Forms.Label()
        Me.pnlbtnRadiologyTest = New System.Windows.Forms.Panel()
        Me.btnRadiologyTest = New System.Windows.Forms.Button()
        Me.Label55 = New System.Windows.Forms.Label()
        Me.Label56 = New System.Windows.Forms.Label()
        Me.Label57 = New System.Windows.Forms.Label()
        Me.Label62 = New System.Windows.Forms.Label()
        Me.pnlbtnGuideline = New System.Windows.Forms.Panel()
        Me.btnGuideline = New System.Windows.Forms.Button()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.pnlIM = New System.Windows.Forms.Panel()
        Me.btnIM = New System.Windows.Forms.Button()
        Me.Label145 = New System.Windows.Forms.Label()
        Me.Label159 = New System.Windows.Forms.Label()
        Me.Label160 = New System.Windows.Forms.Label()
        Me.Label161 = New System.Windows.Forms.Label()
        Me.pnlMsgTOP = New System.Windows.Forms.Panel()
        Me.pnlMsg = New System.Windows.Forms.Panel()
        Me.Label141 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label118 = New System.Windows.Forms.Label()
        Me.txtName = New System.Windows.Forms.TextBox()
        Me.txtMessage = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label63 = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.Label117 = New System.Windows.Forms.Label()
        Me.pnl_tlstrip = New System.Windows.Forms.Panel()
        Me.tlsDM = New gloGlobal.gloToolStripIgnoreFocus()
        Me.tlsDM_Save = New System.Windows.Forms.ToolStripButton()
        Me.tlsDM_Close = New System.Windows.Forms.ToolStripButton()
        Me.CntConditions = New System.Windows.Forms.ContextMenu()
        Me.mnuDelete = New System.Windows.Forms.MenuItem()
        Me.mnuReferral = New System.Windows.Forms.MenuItem()
        Me.EditReferral = New System.Windows.Forms.MenuItem()
        Me.mnuEditTemplate = New System.Windows.Forms.MenuItem()
        Me.cntFindingsprb = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mnuDeleteDrugs = New System.Windows.Forms.ToolStripMenuItem()
        Me.ContextMenuHistory = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mnuDeleteHistory = New System.Windows.Forms.ToolStripMenuItem()
        Me.CmnuStripCPT = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mnuItem_DeleteCPT = New System.Windows.Forms.ToolStripMenuItem()
        Me.CmnustripICD = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mnuItem_DeleteICD = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cntFindings = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mnu_AddFindings = New System.Windows.Forms.ToolStripMenuItem()
        Me.ContextMenuProblem = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mnuDeleteProblem = New System.Windows.Forms.ToolStripMenuItem()
        Me.pnlMain.SuspendLayout()
        Me.pnlMiddle.SuspendLayout()
        Me.pnlDemoVitals.SuspendLayout()
        Me.pnlVitals.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.Panel7.SuspendLayout()
        Me.pnlDemographics.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel6.SuspendLayout()
        Me.PnlProblemList.SuspendLayout()
        Me.PnlProblemMiddle.SuspendLayout()
        Me.Pnlsnomedprb.SuspendLayout()
        Me.pnltrvSnowmedOff.SuspendLayout()
        Me.Panel24.SuspendLayout()
        Me.pnltrvfinprob.SuspendLayout()
        Me.Panel31.SuspendLayout()
        Me.PnlSrchProb.SuspendLayout()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnltrvsubprb.SuspendLayout()
        Me.Panel34.SuspendLayout()
        Me.PnlProblemSearch.SuspendLayout()
        Me.Panel26.SuspendLayout()
        Me.PnlProbLeft.SuspendLayout()
        Me.Panel35.SuspendLayout()
        Me.Panel36.SuspendLayout()
        Me.Panel37.SuspendLayout()
        Me.Panel38.SuspendLayout()
        Me.Panel39.SuspendLayout()
        Me.pnlRadiology.SuspendLayout()
        Me.Panel18.SuspendLayout()
        CType(Me.c1Labs, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel17.SuspendLayout()
        CType(Me.PictureBox6, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlHistory.SuspendLayout()
        Me.Panel22.SuspendLayout()
        Me.Panel20.SuspendLayout()
        Me.Panel21.SuspendLayout()
        Me.Panel16.SuspendLayout()
        Me.Panel9.SuspendLayout()
        Me.pnlHistoryLeft.SuspendLayout()
        Me.Panel11.SuspendLayout()
        Me.Panel8.SuspendLayout()
        Me.pnlICD9.SuspendLayout()
        Me.Panel15.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.Panel10.SuspendLayout()
        Me.pnlCPT.SuspendLayout()
        Me.pnlSelectedCPTs.SuspendLayout()
        Me.pnlSelecteCPTsLabels.SuspendLayout()
        Me.Panel28.SuspendLayout()
        Me.pnlDrugs.SuspendLayout()
        Me.pnltrvSelectedDrugs.SuspendLayout()
        Me.pnlSelectedDrugLabel.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.pnlLab.SuspendLayout()
        Me.Panel12.SuspendLayout()
        CType(Me.C1LabResult, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel13.SuspendLayout()
        Me.Panel23.SuspendLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlSummaryOthers.SuspendLayout()
        Me.pnlGuideline.SuspendLayout()
        Me.pnlGuidelineHeader.SuspendLayout()
        Me.pnl3.SuspendLayout()
        Me.pnlSummary.SuspendLayout()
        Me.pnlSummaryHeader.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.pnlLeft.SuspendLayout()
        Me.Panel19.SuspendLayout()
        Me.pnlbtnOrders.SuspendLayout()
        Me.pnlbtnRadiology.SuspendLayout()
        Me.pnlbtnLabs.SuspendLayout()
        Me.pnlbtnCPT.SuspendLayout()
        Me.pnlbtnICD9.SuspendLayout()
        Me.pnlbtnDrugs.SuspendLayout()
        Me.pnlbtnProblemlist.SuspendLayout()
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
        Me.pnlIM.SuspendLayout()
        Me.pnlMsgTOP.SuspendLayout()
        Me.pnlMsg.SuspendLayout()
        Me.pnl_tlstrip.SuspendLayout()
        Me.tlsDM.SuspendLayout()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.ContextMenuHistory.SuspendLayout()
        Me.CmnuStripCPT.SuspendLayout()
        Me.CmnustripICD.SuspendLayout()
        Me.cntFindings.SuspendLayout()
        Me.ContextMenuProblem.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlMain
        '
        Me.pnlMain.BackColor = System.Drawing.Color.Transparent
        Me.pnlMain.Controls.Add(Me.pnlMiddle)
        Me.pnlMain.Controls.Add(Me.sptLeft)
        Me.pnlMain.Controls.Add(Me.pnlLeft)
        Me.pnlMain.Controls.Add(Me.sptRight)
        Me.pnlMain.Controls.Add(Me.pnlRight)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(0, 94)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(1028, 574)
        Me.pnlMain.TabIndex = 0
        '
        'pnlMiddle
        '
        Me.pnlMiddle.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlMiddle.Controls.Add(Me.pnlDemoVitals)
        Me.pnlMiddle.Controls.Add(Me.PnlProblemList)
        Me.pnlMiddle.Controls.Add(Me.pnlRadiology)
        Me.pnlMiddle.Controls.Add(Me.pnlHistory)
        Me.pnlMiddle.Controls.Add(Me.pnlICD9)
        Me.pnlMiddle.Controls.Add(Me.pnlCPT)
        Me.pnlMiddle.Controls.Add(Me.pnlDrugs)
        Me.pnlMiddle.Controls.Add(Me.pnlLab)
        Me.pnlMiddle.Controls.Add(Me.pnlSummaryOthers)
        Me.pnlMiddle.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMiddle.Location = New System.Drawing.Point(205, 0)
        Me.pnlMiddle.Name = "pnlMiddle"
        Me.pnlMiddle.Padding = New System.Windows.Forms.Padding(0, 0, 3, 0)
        Me.pnlMiddle.Size = New System.Drawing.Size(616, 574)
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
        Me.pnlDemoVitals.Size = New System.Drawing.Size(613, 574)
        Me.pnlDemoVitals.TabIndex = 0
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
        Me.pnlVitals.Size = New System.Drawing.Size(613, 366)
        Me.pnlVitals.TabIndex = 1
        '
        'Label111
        '
        Me.Label111.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label111.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label111.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label111.Location = New System.Drawing.Point(1, 362)
        Me.Label111.Name = "Label111"
        Me.Label111.Size = New System.Drawing.Size(611, 1)
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
        Me.Label112.Size = New System.Drawing.Size(1, 362)
        Me.Label112.TabIndex = 46
        Me.Label112.Text = "label4"
        '
        'Label113
        '
        Me.Label113.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label113.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label113.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label113.Location = New System.Drawing.Point(612, 1)
        Me.Label113.Name = "Label113"
        Me.Label113.Size = New System.Drawing.Size(1, 362)
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
        Me.Label114.Size = New System.Drawing.Size(613, 1)
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
        Me.Label11.Location = New System.Drawing.Point(312, 23)
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
        Me.Label10.Location = New System.Drawing.Point(198, 23)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(12, 14)
        Me.Label10.TabIndex = 2
        Me.Label10.Text = """"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtHeightMinInch
        '
        Me.txtHeightMinInch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtHeightMinInch.Location = New System.Drawing.Point(166, 23)
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
        Me.Label9.Location = New System.Drawing.Point(156, 23)
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
        Me.txtTemperatureMax.AcceptsTab = True
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
        Me.Panel3.Size = New System.Drawing.Size(613, 28)
        Me.Panel3.TabIndex = 46
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
        Me.Panel7.Size = New System.Drawing.Size(613, 25)
        Me.Panel7.TabIndex = 44
        '
        'Label107
        '
        Me.Label107.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label107.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label107.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label107.Location = New System.Drawing.Point(1, 24)
        Me.Label107.Name = "Label107"
        Me.Label107.Size = New System.Drawing.Size(611, 1)
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
        Me.Label109.Location = New System.Drawing.Point(612, 1)
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
        Me.Label110.Size = New System.Drawing.Size(613, 1)
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
        Me.Label7.Size = New System.Drawing.Size(613, 25)
        Me.Label7.TabIndex = 9
        Me.Label7.Text = "      Vitals"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnlDemographics
        '
        Me.pnlDemographics.BackColor = System.Drawing.Color.Transparent
        Me.pnlDemographics.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlDemographics.Controls.Add(Me.cmbAgeMaxMnth)
        Me.pnlDemographics.Controls.Add(Me.cmbAgeMinMnth)
        Me.pnlDemographics.Controls.Add(Me.Label125)
        Me.pnlDemographics.Controls.Add(Me.Label124)
        Me.pnlDemographics.Controls.Add(Me.Label119)
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
        Me.pnlDemographics.Size = New System.Drawing.Size(613, 152)
        Me.pnlDemographics.TabIndex = 0
        '
        'cmbAgeMaxMnth
        '
        Me.cmbAgeMaxMnth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbAgeMaxMnth.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbAgeMaxMnth.ForeColor = System.Drawing.Color.Black
        Me.cmbAgeMaxMnth.Location = New System.Drawing.Point(314, 14)
        Me.cmbAgeMaxMnth.Name = "cmbAgeMaxMnth"
        Me.cmbAgeMaxMnth.Size = New System.Drawing.Size(49, 22)
        Me.cmbAgeMaxMnth.TabIndex = 27
        '
        'cmbAgeMinMnth
        '
        Me.cmbAgeMinMnth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbAgeMinMnth.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbAgeMinMnth.ForeColor = System.Drawing.Color.Black
        Me.cmbAgeMinMnth.Location = New System.Drawing.Point(149, 13)
        Me.cmbAgeMinMnth.Name = "cmbAgeMinMnth"
        Me.cmbAgeMinMnth.Size = New System.Drawing.Size(49, 22)
        Me.cmbAgeMinMnth.TabIndex = 26
        '
        'Label125
        '
        Me.Label125.AutoSize = True
        Me.Label125.ForeColor = System.Drawing.Color.Red
        Me.Label125.Location = New System.Drawing.Point(210, 16)
        Me.Label125.Name = "Label125"
        Me.Label125.Size = New System.Drawing.Size(14, 14)
        Me.Label125.TabIndex = 25
        Me.Label125.Text = "*"
        '
        'Label124
        '
        Me.Label124.AutoSize = True
        Me.Label124.ForeColor = System.Drawing.Color.Red
        Me.Label124.Location = New System.Drawing.Point(23, 49)
        Me.Label124.Name = "Label124"
        Me.Label124.Size = New System.Drawing.Size(14, 14)
        Me.Label124.TabIndex = 24
        Me.Label124.Text = "*"
        '
        'Label119
        '
        Me.Label119.AutoSize = True
        Me.Label119.ForeColor = System.Drawing.Color.Red
        Me.Label119.Location = New System.Drawing.Point(11, 17)
        Me.Label119.Name = "Label119"
        Me.Label119.Size = New System.Drawing.Size(14, 14)
        Me.Label119.TabIndex = 23
        Me.Label119.Text = "*"
        '
        'Label103
        '
        Me.Label103.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label103.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label103.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label103.Location = New System.Drawing.Point(1, 148)
        Me.Label103.Name = "Label103"
        Me.Label103.Size = New System.Drawing.Size(611, 1)
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
        Me.Label105.Location = New System.Drawing.Point(612, 1)
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
        Me.Label106.Size = New System.Drawing.Size(613, 1)
        Me.Label106.TabIndex = 19
        Me.Label106.Text = "label1"
        '
        'lblEmpStatus
        '
        Me.lblEmpStatus.AutoSize = True
        Me.lblEmpStatus.BackColor = System.Drawing.Color.Transparent
        Me.lblEmpStatus.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEmpStatus.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblEmpStatus.Location = New System.Drawing.Point(301, 115)
        Me.lblEmpStatus.Name = "lblEmpStatus"
        Me.lblEmpStatus.Size = New System.Drawing.Size(122, 14)
        Me.lblEmpStatus.TabIndex = 13
        Me.lblEmpStatus.Text = "Employment Status :"
        Me.lblEmpStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmbState
        '
        Me.cmbState.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbState.ForeColor = System.Drawing.Color.Black
        Me.cmbState.Location = New System.Drawing.Point(425, 46)
        Me.cmbState.Name = "cmbState"
        Me.cmbState.Size = New System.Drawing.Size(169, 22)
        Me.cmbState.TabIndex = 4
        '
        'cmbEmpStatus
        '
        Me.cmbEmpStatus.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbEmpStatus.ForeColor = System.Drawing.Color.Black
        Me.cmbEmpStatus.Location = New System.Drawing.Point(425, 112)
        Me.cmbEmpStatus.Name = "cmbEmpStatus"
        Me.cmbEmpStatus.Size = New System.Drawing.Size(169, 22)
        Me.cmbEmpStatus.TabIndex = 8
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
        '
        'lblZip
        '
        Me.lblZip.AutoSize = True
        Me.lblZip.BackColor = System.Drawing.Color.Transparent
        Me.lblZip.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblZip.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblZip.Location = New System.Drawing.Point(360, 82)
        Me.lblZip.Name = "lblZip"
        Me.lblZip.Size = New System.Drawing.Size(63, 14)
        Me.lblZip.TabIndex = 12
        Me.lblZip.Text = "Zip Code :"
        Me.lblZip.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblState
        '
        Me.lblState.AutoSize = True
        Me.lblState.BackColor = System.Drawing.Color.Transparent
        Me.lblState.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblState.Location = New System.Drawing.Point(378, 49)
        Me.lblState.Name = "lblState"
        Me.lblState.Size = New System.Drawing.Size(45, 14)
        Me.lblState.TabIndex = 11
        Me.lblState.Text = "State :"
        Me.lblState.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblCity
        '
        Me.lblCity.AutoSize = True
        Me.lblCity.BackColor = System.Drawing.Color.Transparent
        Me.lblCity.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCity.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblCity.Location = New System.Drawing.Point(388, 16)
        Me.lblCity.Name = "lblCity"
        Me.lblCity.Size = New System.Drawing.Size(35, 14)
        Me.lblCity.TabIndex = 10
        Me.lblCity.Text = "City :"
        Me.lblCity.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmbMaritalSt
        '
        Me.cmbMaritalSt.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple
        Me.cmbMaritalSt.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbMaritalSt.ForeColor = System.Drawing.Color.Black
        Me.cmbMaritalSt.Location = New System.Drawing.Point(93, 112)
        Me.cmbMaritalSt.Name = "cmbMaritalSt"
        Me.cmbMaritalSt.Size = New System.Drawing.Size(178, 22)
        Me.cmbMaritalSt.TabIndex = 7
        '
        'cmbRace
        '
        Me.cmbRace.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbRace.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbRace.ForeColor = System.Drawing.Color.Black
        Me.cmbRace.Location = New System.Drawing.Point(93, 79)
        Me.cmbRace.Name = "cmbRace"
        Me.cmbRace.Size = New System.Drawing.Size(178, 22)
        Me.cmbRace.TabIndex = 5
        '
        'cmbAgeMax
        '
        Me.cmbAgeMax.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbAgeMax.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbAgeMax.Location = New System.Drawing.Point(259, 14)
        Me.cmbAgeMax.Name = "cmbAgeMax"
        Me.cmbAgeMax.Size = New System.Drawing.Size(53, 22)
        Me.cmbAgeMax.TabIndex = 1
        '
        'cmbAgeMin
        '
        Me.cmbAgeMin.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbAgeMin.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbAgeMin.ForeColor = System.Drawing.Color.Black
        Me.cmbAgeMin.Location = New System.Drawing.Point(93, 13)
        Me.cmbAgeMin.Name = "cmbAgeMin"
        Me.cmbAgeMin.Size = New System.Drawing.Size(55, 22)
        Me.cmbAgeMin.TabIndex = 0
        '
        'cmbGender
        '
        Me.cmbGender.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbGender.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbGender.ForeColor = System.Drawing.Color.Black
        Me.cmbGender.Location = New System.Drawing.Point(93, 46)
        Me.cmbGender.Name = "cmbGender"
        Me.cmbGender.Size = New System.Drawing.Size(178, 22)
        Me.cmbGender.TabIndex = 3
        '
        'lblAgeMax
        '
        Me.lblAgeMax.AutoSize = True
        Me.lblAgeMax.BackColor = System.Drawing.Color.Transparent
        Me.lblAgeMax.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAgeMax.Location = New System.Drawing.Point(221, 18)
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
        Me.Panel2.Size = New System.Drawing.Size(613, 28)
        Me.Panel2.TabIndex = 45
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
        Me.Panel6.Size = New System.Drawing.Size(613, 25)
        Me.Panel6.TabIndex = 19
        '
        'Label65
        '
        Me.Label65.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label65.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label65.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label65.Location = New System.Drawing.Point(1, 24)
        Me.Label65.Name = "Label65"
        Me.Label65.Size = New System.Drawing.Size(611, 1)
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
        Me.Label89.Location = New System.Drawing.Point(612, 1)
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
        Me.Label90.Size = New System.Drawing.Size(613, 1)
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
        Me.Label2.Size = New System.Drawing.Size(613, 25)
        Me.Label2.TabIndex = 9
        Me.Label2.Text = "      Demographics"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'PnlProblemList
        '
        Me.PnlProblemList.Controls.Add(Me.PnlProblemMiddle)
        Me.PnlProblemList.Controls.Add(Me.Panel35)
        Me.PnlProblemList.Controls.Add(Me.Panel37)
        Me.PnlProblemList.Controls.Add(Me.Panel38)
        Me.PnlProblemList.Controls.Add(Me.Button2)
        Me.PnlProblemList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PnlProblemList.Location = New System.Drawing.Point(0, 0)
        Me.PnlProblemList.Name = "PnlProblemList"
        Me.PnlProblemList.Size = New System.Drawing.Size(613, 574)
        Me.PnlProblemList.TabIndex = 9
        Me.PnlProblemList.Visible = False
        '
        'PnlProblemMiddle
        '
        Me.PnlProblemMiddle.Controls.Add(Me.Pnlsnomedprb)
        Me.PnlProblemMiddle.Controls.Add(Me.PnlProblemSearch)
        Me.PnlProblemMiddle.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PnlProblemMiddle.Location = New System.Drawing.Point(0, 27)
        Me.PnlProblemMiddle.Name = "PnlProblemMiddle"
        Me.PnlProblemMiddle.Size = New System.Drawing.Size(613, 357)
        Me.PnlProblemMiddle.TabIndex = 1
        '
        'Pnlsnomedprb
        '
        Me.Pnlsnomedprb.Controls.Add(Me.pnltrvSnowmedOff)
        Me.Pnlsnomedprb.Controls.Add(Me.pnltrvfinprob)
        Me.Pnlsnomedprb.Controls.Add(Me.PnlSrchProb)
        Me.Pnlsnomedprb.Controls.Add(Me.Splitter6)
        Me.Pnlsnomedprb.Controls.Add(Me.pnltrvsubprb)
        Me.Pnlsnomedprb.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Pnlsnomedprb.Location = New System.Drawing.Point(0, 0)
        Me.Pnlsnomedprb.Name = "Pnlsnomedprb"
        Me.Pnlsnomedprb.Size = New System.Drawing.Size(613, 357)
        Me.Pnlsnomedprb.TabIndex = 1
        '
        'pnltrvSnowmedOff
        '
        Me.pnltrvSnowmedOff.Controls.Add(Me.Label142)
        Me.pnltrvSnowmedOff.Controls.Add(Me.trvSnowmedOff)
        Me.pnltrvSnowmedOff.Controls.Add(Me.Panel24)
        Me.pnltrvSnowmedOff.Controls.Add(Me.Label151)
        Me.pnltrvSnowmedOff.Controls.Add(Me.Label152)
        Me.pnltrvSnowmedOff.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnltrvSnowmedOff.Location = New System.Drawing.Point(0, 26)
        Me.pnltrvSnowmedOff.Name = "pnltrvSnowmedOff"
        Me.pnltrvSnowmedOff.Size = New System.Drawing.Size(613, 166)
        Me.pnltrvSnowmedOff.TabIndex = 20
        '
        'Label142
        '
        Me.Label142.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label142.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label142.Location = New System.Drawing.Point(1, 165)
        Me.Label142.Name = "Label142"
        Me.Label142.Size = New System.Drawing.Size(611, 1)
        Me.Label142.TabIndex = 37
        Me.Label142.Text = "label1"
        '
        'trvSnowmedOff
        '
        Me.trvSnowmedOff.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trvSnowmedOff.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trvSnowmedOff.Location = New System.Drawing.Point(1, 22)
        Me.trvSnowmedOff.Name = "trvSnowmedOff"
        Me.trvSnowmedOff.Size = New System.Drawing.Size(611, 144)
        Me.trvSnowmedOff.TabIndex = 0
        '
        'Panel24
        '
        Me.Panel24.BackgroundImage = CType(resources.GetObject("Panel24.BackgroundImage"), System.Drawing.Image)
        Me.Panel24.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel24.Controls.Add(Me.Label143)
        Me.Panel24.Controls.Add(Me.Label144)
        Me.Panel24.Controls.Add(Me.Label150)
        Me.Panel24.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel24.Location = New System.Drawing.Point(1, 0)
        Me.Panel24.Name = "Panel24"
        Me.Panel24.Size = New System.Drawing.Size(611, 22)
        Me.Panel24.TabIndex = 18
        '
        'Label143
        '
        Me.Label143.BackColor = System.Drawing.Color.Transparent
        Me.Label143.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label143.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label143.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label143.Location = New System.Drawing.Point(0, 1)
        Me.Label143.Name = "Label143"
        Me.Label143.Size = New System.Drawing.Size(611, 20)
        Me.Label143.TabIndex = 39
        Me.Label143.Text = "  Problem List"
        Me.Label143.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label144
        '
        Me.Label144.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label144.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label144.Location = New System.Drawing.Point(0, 21)
        Me.Label144.Name = "Label144"
        Me.Label144.Size = New System.Drawing.Size(611, 1)
        Me.Label144.TabIndex = 38
        Me.Label144.Text = "label1"
        '
        'Label150
        '
        Me.Label150.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label150.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label150.Location = New System.Drawing.Point(0, 0)
        Me.Label150.Name = "Label150"
        Me.Label150.Size = New System.Drawing.Size(611, 1)
        Me.Label150.TabIndex = 37
        Me.Label150.Text = "label1"
        '
        'Label151
        '
        Me.Label151.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label151.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label151.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label151.Location = New System.Drawing.Point(0, 0)
        Me.Label151.Name = "Label151"
        Me.Label151.Size = New System.Drawing.Size(1, 166)
        Me.Label151.TabIndex = 19
        Me.Label151.Text = "label4"
        '
        'Label152
        '
        Me.Label152.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label152.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label152.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label152.Location = New System.Drawing.Point(612, 0)
        Me.Label152.Name = "Label152"
        Me.Label152.Size = New System.Drawing.Size(1, 166)
        Me.Label152.TabIndex = 20
        Me.Label152.Text = "label4"
        '
        'pnltrvfinprob
        '
        Me.pnltrvfinprob.Controls.Add(Me.trvfinprob)
        Me.pnltrvfinprob.Controls.Add(Me.Panel31)
        Me.pnltrvfinprob.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnltrvfinprob.Location = New System.Drawing.Point(0, 26)
        Me.pnltrvfinprob.Name = "pnltrvfinprob"
        Me.pnltrvfinprob.Size = New System.Drawing.Size(613, 166)
        Me.pnltrvfinprob.TabIndex = 1
        '
        'trvfinprob
        '
        Me.trvfinprob.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.trvfinprob.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trvfinprob.Location = New System.Drawing.Point(0, 22)
        Me.trvfinprob.Name = "trvfinprob"
        Me.trvfinprob.Size = New System.Drawing.Size(613, 144)
        Me.trvfinprob.TabIndex = 0
        '
        'Panel31
        '
        Me.Panel31.BackgroundImage = CType(resources.GetObject("Panel31.BackgroundImage"), System.Drawing.Image)
        Me.Panel31.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel31.Controls.Add(Me.Label154)
        Me.Panel31.Controls.Add(Me.Label153)
        Me.Panel31.Controls.Add(Me.Label201)
        Me.Panel31.Controls.Add(Me.Label203)
        Me.Panel31.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel31.Location = New System.Drawing.Point(0, 0)
        Me.Panel31.Name = "Panel31"
        Me.Panel31.Size = New System.Drawing.Size(613, 22)
        Me.Panel31.TabIndex = 18
        '
        'Label154
        '
        Me.Label154.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label154.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label154.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label154.Location = New System.Drawing.Point(612, 1)
        Me.Label154.Name = "Label154"
        Me.Label154.Size = New System.Drawing.Size(1, 21)
        Me.Label154.TabIndex = 41
        Me.Label154.Text = "label4"
        '
        'Label153
        '
        Me.Label153.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label153.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label153.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label153.Location = New System.Drawing.Point(0, 1)
        Me.Label153.Name = "Label153"
        Me.Label153.Size = New System.Drawing.Size(1, 21)
        Me.Label153.TabIndex = 40
        Me.Label153.Text = "label4"
        '
        'Label201
        '
        Me.Label201.BackColor = System.Drawing.Color.Transparent
        Me.Label201.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label201.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label201.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label201.Location = New System.Drawing.Point(0, 1)
        Me.Label201.Name = "Label201"
        Me.Label201.Size = New System.Drawing.Size(613, 21)
        Me.Label201.TabIndex = 39
        Me.Label201.Text = "  Finding"
        Me.Label201.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label203
        '
        Me.Label203.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label203.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label203.Location = New System.Drawing.Point(0, 0)
        Me.Label203.Name = "Label203"
        Me.Label203.Size = New System.Drawing.Size(613, 1)
        Me.Label203.TabIndex = 37
        Me.Label203.Text = "label1"
        '
        'PnlSrchProb
        '
        Me.PnlSrchProb.BackColor = System.Drawing.Color.Transparent
        Me.PnlSrchProb.Controls.Add(Me.txtsrchprb)
        Me.PnlSrchProb.Controls.Add(Me.Label215)
        Me.PnlSrchProb.Controls.Add(Me.Label216)
        Me.PnlSrchProb.Controls.Add(Me.btnclrprb)
        Me.PnlSrchProb.Controls.Add(Me.PictureBox3)
        Me.PnlSrchProb.Controls.Add(Me.Label217)
        Me.PnlSrchProb.Controls.Add(Me.Label218)
        Me.PnlSrchProb.Controls.Add(Me.Label219)
        Me.PnlSrchProb.Controls.Add(Me.Label220)
        Me.PnlSrchProb.Dock = System.Windows.Forms.DockStyle.Top
        Me.PnlSrchProb.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PnlSrchProb.ForeColor = System.Drawing.Color.Black
        Me.PnlSrchProb.Location = New System.Drawing.Point(0, 0)
        Me.PnlSrchProb.Name = "PnlSrchProb"
        Me.PnlSrchProb.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.PnlSrchProb.Size = New System.Drawing.Size(613, 26)
        Me.PnlSrchProb.TabIndex = 0
        '
        'txtsrchprb
        '
        Me.txtsrchprb.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtsrchprb.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtsrchprb.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtsrchprb.ForeColor = System.Drawing.Color.Black
        Me.txtsrchprb.Location = New System.Drawing.Point(29, 4)
        Me.txtsrchprb.MaxLength = 50
        Me.txtsrchprb.Name = "txtsrchprb"
        Me.txtsrchprb.Size = New System.Drawing.Size(562, 15)
        Me.txtsrchprb.TabIndex = 0
        '
        'Label215
        '
        Me.Label215.BackColor = System.Drawing.Color.White
        Me.Label215.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label215.Location = New System.Drawing.Point(29, 1)
        Me.Label215.Name = "Label215"
        Me.Label215.Size = New System.Drawing.Size(562, 3)
        Me.Label215.TabIndex = 37
        '
        'Label216
        '
        Me.Label216.BackColor = System.Drawing.Color.White
        Me.Label216.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label216.Location = New System.Drawing.Point(29, 17)
        Me.Label216.Name = "Label216"
        Me.Label216.Size = New System.Drawing.Size(562, 5)
        Me.Label216.TabIndex = 38
        '
        'btnclrprb
        '
        Me.btnclrprb.BackgroundImage = CType(resources.GetObject("btnclrprb.BackgroundImage"), System.Drawing.Image)
        Me.btnclrprb.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnclrprb.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnclrprb.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnclrprb.FlatAppearance.BorderSize = 0
        Me.btnclrprb.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnclrprb.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnclrprb.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnclrprb.Image = CType(resources.GetObject("btnclrprb.Image"), System.Drawing.Image)
        Me.btnclrprb.Location = New System.Drawing.Point(591, 1)
        Me.btnclrprb.Name = "btnclrprb"
        Me.btnclrprb.Size = New System.Drawing.Size(21, 21)
        Me.btnclrprb.TabIndex = 41
        Me.ToolTip1.SetToolTip(Me.btnclrprb, "Clear search")
        Me.btnclrprb.UseVisualStyleBackColor = True
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
        'Label217
        '
        Me.Label217.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label217.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label217.Location = New System.Drawing.Point(1, 22)
        Me.Label217.Name = "Label217"
        Me.Label217.Size = New System.Drawing.Size(611, 1)
        Me.Label217.TabIndex = 35
        Me.Label217.Text = "label1"
        '
        'Label218
        '
        Me.Label218.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label218.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label218.Location = New System.Drawing.Point(1, 0)
        Me.Label218.Name = "Label218"
        Me.Label218.Size = New System.Drawing.Size(611, 1)
        Me.Label218.TabIndex = 36
        Me.Label218.Text = "label1"
        '
        'Label219
        '
        Me.Label219.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label219.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label219.Location = New System.Drawing.Point(0, 0)
        Me.Label219.Name = "Label219"
        Me.Label219.Size = New System.Drawing.Size(1, 23)
        Me.Label219.TabIndex = 39
        Me.Label219.Text = "label4"
        '
        'Label220
        '
        Me.Label220.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label220.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label220.Location = New System.Drawing.Point(612, 0)
        Me.Label220.Name = "Label220"
        Me.Label220.Size = New System.Drawing.Size(1, 23)
        Me.Label220.TabIndex = 40
        Me.Label220.Text = "label4"
        '
        'Splitter6
        '
        Me.Splitter6.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Splitter6.Location = New System.Drawing.Point(0, 192)
        Me.Splitter6.Name = "Splitter6"
        Me.Splitter6.Size = New System.Drawing.Size(613, 3)
        Me.Splitter6.TabIndex = 19
        Me.Splitter6.TabStop = False
        Me.Splitter6.Visible = False
        '
        'pnltrvsubprb
        '
        Me.pnltrvsubprb.Controls.Add(Me.Label221)
        Me.pnltrvsubprb.Controls.Add(Me.trvsubprb)
        Me.pnltrvsubprb.Controls.Add(Me.Panel34)
        Me.pnltrvsubprb.Controls.Add(Me.Label225)
        Me.pnltrvsubprb.Controls.Add(Me.Label226)
        Me.pnltrvsubprb.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnltrvsubprb.Location = New System.Drawing.Point(0, 195)
        Me.pnltrvsubprb.Name = "pnltrvsubprb"
        Me.pnltrvsubprb.Size = New System.Drawing.Size(613, 162)
        Me.pnltrvsubprb.TabIndex = 2
        Me.pnltrvsubprb.Visible = False
        '
        'Label221
        '
        Me.Label221.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label221.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label221.Location = New System.Drawing.Point(1, 161)
        Me.Label221.Name = "Label221"
        Me.Label221.Size = New System.Drawing.Size(611, 1)
        Me.Label221.TabIndex = 37
        Me.Label221.Text = "label1"
        '
        'trvsubprb
        '
        Me.trvsubprb.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trvsubprb.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trvsubprb.Location = New System.Drawing.Point(1, 22)
        Me.trvsubprb.Name = "trvsubprb"
        Me.trvsubprb.Size = New System.Drawing.Size(611, 140)
        Me.trvsubprb.TabIndex = 0
        '
        'Panel34
        '
        Me.Panel34.BackgroundImage = CType(resources.GetObject("Panel34.BackgroundImage"), System.Drawing.Image)
        Me.Panel34.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel34.Controls.Add(Me.Label222)
        Me.Panel34.Controls.Add(Me.Label223)
        Me.Panel34.Controls.Add(Me.Label224)
        Me.Panel34.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel34.Location = New System.Drawing.Point(1, 0)
        Me.Panel34.Name = "Panel34"
        Me.Panel34.Size = New System.Drawing.Size(611, 22)
        Me.Panel34.TabIndex = 18
        '
        'Label222
        '
        Me.Label222.BackColor = System.Drawing.Color.Transparent
        Me.Label222.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label222.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label222.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label222.Location = New System.Drawing.Point(0, 1)
        Me.Label222.Name = "Label222"
        Me.Label222.Size = New System.Drawing.Size(611, 20)
        Me.Label222.TabIndex = 40
        Me.Label222.Text = "  Sub Type"
        Me.Label222.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label223
        '
        Me.Label223.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label223.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label223.Location = New System.Drawing.Point(0, 0)
        Me.Label223.Name = "Label223"
        Me.Label223.Size = New System.Drawing.Size(611, 1)
        Me.Label223.TabIndex = 38
        Me.Label223.Text = "label1"
        '
        'Label224
        '
        Me.Label224.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label224.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label224.Location = New System.Drawing.Point(0, 21)
        Me.Label224.Name = "Label224"
        Me.Label224.Size = New System.Drawing.Size(611, 1)
        Me.Label224.TabIndex = 37
        Me.Label224.Text = "label1"
        '
        'Label225
        '
        Me.Label225.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label225.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label225.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label225.Location = New System.Drawing.Point(0, 0)
        Me.Label225.Name = "Label225"
        Me.Label225.Size = New System.Drawing.Size(1, 162)
        Me.Label225.TabIndex = 19
        Me.Label225.Text = "label4"
        '
        'Label226
        '
        Me.Label226.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label226.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label226.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label226.Location = New System.Drawing.Point(612, 0)
        Me.Label226.Name = "Label226"
        Me.Label226.Size = New System.Drawing.Size(1, 162)
        Me.Label226.TabIndex = 20
        Me.Label226.Text = "label4"
        '
        'PnlProblemSearch
        '
        Me.PnlProblemSearch.Controls.Add(Me.Panel26)
        Me.PnlProblemSearch.Controls.Add(Me.PnlProbLeft)
        Me.PnlProblemSearch.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PnlProblemSearch.Location = New System.Drawing.Point(0, 0)
        Me.PnlProblemSearch.Name = "PnlProblemSearch"
        Me.PnlProblemSearch.Size = New System.Drawing.Size(613, 357)
        Me.PnlProblemSearch.TabIndex = 2
        '
        'Panel26
        '
        Me.Panel26.Controls.Add(Me.trvprobright)
        Me.Panel26.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel26.Location = New System.Drawing.Point(287, 0)
        Me.Panel26.Name = "Panel26"
        Me.Panel26.Size = New System.Drawing.Size(326, 357)
        Me.Panel26.TabIndex = 0
        '
        'trvprobright
        '
        Me.trvprobright.BackColor = System.Drawing.Color.White
        Me.trvprobright.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trvprobright.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.trvprobright.ForeColor = System.Drawing.Color.Black
        Me.trvprobright.HideSelection = False
        Me.trvprobright.ImageIndex = 0
        Me.trvprobright.ImageList = Me.ImageList1
        Me.trvprobright.ItemHeight = 18
        Me.trvprobright.Location = New System.Drawing.Point(0, 1)
        Me.trvprobright.Name = "trvprobright"
        Me.trvprobright.SelectedImageIndex = 0
        Me.trvprobright.ShowLines = False
        Me.trvprobright.Size = New System.Drawing.Size(326, 274)
        Me.trvprobright.TabIndex = 2
        Me.trvprobright.Visible = False
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
        Me.ImageList1.Images.SetKeyName(12, "Immunization_Old.ico")
        '
        'PnlProbLeft
        '
        Me.PnlProbLeft.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PnlProbLeft.Controls.Add(Me.Label179)
        Me.PnlProbLeft.Controls.Add(Me.Label197)
        Me.PnlProbLeft.Controls.Add(Me.Label198)
        Me.PnlProbLeft.Controls.Add(Me.Label199)
        Me.PnlProbLeft.Controls.Add(Me.trvproblem)
        Me.PnlProbLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.PnlProbLeft.Enabled = False
        Me.PnlProbLeft.Location = New System.Drawing.Point(0, 0)
        Me.PnlProbLeft.Name = "PnlProbLeft"
        Me.PnlProbLeft.Padding = New System.Windows.Forms.Padding(0, 0, 3, 0)
        Me.PnlProbLeft.Size = New System.Drawing.Size(287, 357)
        Me.PnlProbLeft.TabIndex = 2
        Me.PnlProbLeft.Visible = False
        '
        'Label179
        '
        Me.Label179.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label179.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label179.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label179.Location = New System.Drawing.Point(1, 356)
        Me.Label179.Name = "Label179"
        Me.Label179.Size = New System.Drawing.Size(282, 1)
        Me.Label179.TabIndex = 12
        Me.Label179.Text = "label2"
        '
        'Label197
        '
        Me.Label197.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label197.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label197.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label197.Location = New System.Drawing.Point(0, 1)
        Me.Label197.Name = "Label197"
        Me.Label197.Size = New System.Drawing.Size(1, 356)
        Me.Label197.TabIndex = 11
        Me.Label197.Text = "label4"
        '
        'Label198
        '
        Me.Label198.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label198.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label198.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label198.Location = New System.Drawing.Point(283, 1)
        Me.Label198.Name = "Label198"
        Me.Label198.Size = New System.Drawing.Size(1, 356)
        Me.Label198.TabIndex = 10
        Me.Label198.Text = "label3"
        '
        'Label199
        '
        Me.Label199.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label199.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label199.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label199.Location = New System.Drawing.Point(0, 0)
        Me.Label199.Name = "Label199"
        Me.Label199.Size = New System.Drawing.Size(284, 1)
        Me.Label199.TabIndex = 9
        Me.Label199.Text = "label1"
        '
        'trvproblem
        '
        Me.trvproblem.BackColor = System.Drawing.Color.White
        Me.trvproblem.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trvproblem.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trvproblem.ForeColor = System.Drawing.Color.Black
        Me.trvproblem.HideSelection = False
        Me.trvproblem.ItemHeight = 18
        Me.trvproblem.Location = New System.Drawing.Point(0, 0)
        Me.trvproblem.Name = "trvproblem"
        Me.trvproblem.ShowLines = False
        Me.trvproblem.Size = New System.Drawing.Size(284, 357)
        Me.trvproblem.TabIndex = 0
        Me.trvproblem.Visible = False
        '
        'Panel35
        '
        Me.Panel35.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel35.Controls.Add(Me.Panel36)
        Me.Panel35.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel35.Location = New System.Drawing.Point(0, 384)
        Me.Panel35.Name = "Panel35"
        Me.Panel35.Padding = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.Panel35.Size = New System.Drawing.Size(613, 27)
        Me.Panel35.TabIndex = 22
        '
        'Panel36
        '
        Me.Panel36.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Blue2007
        Me.Panel36.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel36.Controls.Add(Me.Label227)
        Me.Panel36.Controls.Add(Me.Label228)
        Me.Panel36.Controls.Add(Me.Label229)
        Me.Panel36.Controls.Add(Me.Label230)
        Me.Panel36.Controls.Add(Me.Label231)
        Me.Panel36.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel36.Location = New System.Drawing.Point(0, 3)
        Me.Panel36.Name = "Panel36"
        Me.Panel36.Size = New System.Drawing.Size(613, 21)
        Me.Panel36.TabIndex = 14
        '
        'Label227
        '
        Me.Label227.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label227.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label227.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label227.Location = New System.Drawing.Point(612, 1)
        Me.Label227.Name = "Label227"
        Me.Label227.Size = New System.Drawing.Size(1, 19)
        Me.Label227.TabIndex = 11
        Me.Label227.Text = "label3"
        '
        'Label228
        '
        Me.Label228.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label228.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label228.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label228.Location = New System.Drawing.Point(0, 1)
        Me.Label228.Name = "Label228"
        Me.Label228.Size = New System.Drawing.Size(1, 19)
        Me.Label228.TabIndex = 12
        Me.Label228.Text = "label4"
        '
        'Label229
        '
        Me.Label229.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label229.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label229.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label229.Location = New System.Drawing.Point(0, 0)
        Me.Label229.Name = "Label229"
        Me.Label229.Size = New System.Drawing.Size(613, 1)
        Me.Label229.TabIndex = 10
        Me.Label229.Text = "label1"
        '
        'Label230
        '
        Me.Label230.BackColor = System.Drawing.Color.Transparent
        Me.Label230.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label230.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label230.ForeColor = System.Drawing.Color.White
        Me.Label230.Image = CType(resources.GetObject("Label230.Image"), System.Drawing.Image)
        Me.Label230.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label230.Location = New System.Drawing.Point(0, 0)
        Me.Label230.Name = "Label230"
        Me.Label230.Size = New System.Drawing.Size(613, 20)
        Me.Label230.TabIndex = 9
        Me.Label230.Text = "      Selected Problem"
        Me.Label230.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label231
        '
        Me.Label231.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label231.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label231.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label231.Location = New System.Drawing.Point(0, 20)
        Me.Label231.Name = "Label231"
        Me.Label231.Size = New System.Drawing.Size(613, 1)
        Me.Label231.TabIndex = 13
        Me.Label231.Text = "label2"
        '
        'Panel37
        '
        Me.Panel37.BackColor = System.Drawing.Color.Transparent
        Me.Panel37.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel37.Controls.Add(Me.trvselectedhist)
        Me.Panel37.Controls.Add(Me.Label232)
        Me.Panel37.Controls.Add(Me.Label233)
        Me.Panel37.Controls.Add(Me.trvselectedprob)
        Me.Panel37.Controls.Add(Me.Label234)
        Me.Panel37.Controls.Add(Me.Label235)
        Me.Panel37.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel37.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel37.Location = New System.Drawing.Point(0, 411)
        Me.Panel37.Margin = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.Panel37.Name = "Panel37"
        Me.Panel37.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.Panel37.Size = New System.Drawing.Size(613, 163)
        Me.Panel37.TabIndex = 3
        '
        'trvselectedhist
        '
        Me.trvselectedhist.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trvselectedhist.ImageIndex = 0
        Me.trvselectedhist.ImageList = Me.ImageList1
        Me.trvselectedhist.Location = New System.Drawing.Point(1, 1)
        Me.trvselectedhist.Name = "trvselectedhist"
        Me.trvselectedhist.SelectedImageIndex = 0
        Me.trvselectedhist.Size = New System.Drawing.Size(611, 158)
        Me.trvselectedhist.TabIndex = 9
        '
        'Label232
        '
        Me.Label232.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label232.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label232.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label232.Location = New System.Drawing.Point(1, 159)
        Me.Label232.Name = "Label232"
        Me.Label232.Size = New System.Drawing.Size(611, 1)
        Me.Label232.TabIndex = 8
        Me.Label232.Text = "label2"
        '
        'Label233
        '
        Me.Label233.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label233.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label233.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label233.Location = New System.Drawing.Point(0, 1)
        Me.Label233.Name = "Label233"
        Me.Label233.Size = New System.Drawing.Size(1, 159)
        Me.Label233.TabIndex = 7
        Me.Label233.Text = "label4"
        '
        'trvselectedprob
        '
        Me.trvselectedprob.BackColor = System.Drawing.Color.White
        Me.trvselectedprob.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trvselectedprob.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trvselectedprob.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.trvselectedprob.ForeColor = System.Drawing.Color.Black
        Me.trvselectedprob.HideSelection = False
        Me.trvselectedprob.ImageIndex = 11
        Me.trvselectedprob.ImageList = Me.ImageList1
        Me.trvselectedprob.ItemHeight = 18
        Me.trvselectedprob.Location = New System.Drawing.Point(0, 1)
        Me.trvselectedprob.Name = "trvselectedprob"
        Me.trvselectedprob.SelectedImageIndex = 11
        Me.trvselectedprob.ShowLines = False
        Me.trvselectedprob.Size = New System.Drawing.Size(612, 159)
        Me.trvselectedprob.TabIndex = 0
        '
        'Label234
        '
        Me.Label234.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label234.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label234.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label234.Location = New System.Drawing.Point(612, 1)
        Me.Label234.Name = "Label234"
        Me.Label234.Size = New System.Drawing.Size(1, 159)
        Me.Label234.TabIndex = 6
        Me.Label234.Text = "label3"
        '
        'Label235
        '
        Me.Label235.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label235.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label235.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label235.Location = New System.Drawing.Point(0, 0)
        Me.Label235.Name = "Label235"
        Me.Label235.Size = New System.Drawing.Size(613, 1)
        Me.Label235.TabIndex = 5
        Me.Label235.Text = "label1"
        '
        'Panel38
        '
        Me.Panel38.Controls.Add(Me.Panel39)
        Me.Panel38.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel38.Location = New System.Drawing.Point(0, 0)
        Me.Panel38.Name = "Panel38"
        Me.Panel38.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.Panel38.Size = New System.Drawing.Size(613, 27)
        Me.Panel38.TabIndex = 0
        '
        'Panel39
        '
        Me.Panel39.BackColor = System.Drawing.Color.Transparent
        Me.Panel39.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.Panel39.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel39.Controls.Add(Me.cmbhistsnomed)
        Me.Panel39.Controls.Add(Me.lblsnohistcat)
        Me.Panel39.Controls.Add(Me.TextBox2)
        Me.Panel39.Controls.Add(Me.Label237)
        Me.Panel39.Controls.Add(Me.Label238)
        Me.Panel39.Controls.Add(Me.Label239)
        Me.Panel39.Controls.Add(Me.Label240)
        Me.Panel39.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel39.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel39.ForeColor = System.Drawing.Color.Black
        Me.Panel39.Location = New System.Drawing.Point(0, 0)
        Me.Panel39.Name = "Panel39"
        Me.Panel39.Size = New System.Drawing.Size(613, 24)
        Me.Panel39.TabIndex = 19
        '
        'cmbhistsnomed
        '
        Me.cmbhistsnomed.Dock = System.Windows.Forms.DockStyle.Left
        Me.cmbhistsnomed.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbhistsnomed.FormattingEnabled = True
        Me.cmbhistsnomed.Location = New System.Drawing.Point(131, 1)
        Me.cmbhistsnomed.Name = "cmbhistsnomed"
        Me.cmbhistsnomed.Size = New System.Drawing.Size(284, 22)
        Me.cmbhistsnomed.TabIndex = 13
        '
        'lblsnohistcat
        '
        Me.lblsnohistcat.BackColor = System.Drawing.Color.Transparent
        Me.lblsnohistcat.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblsnohistcat.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblsnohistcat.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblsnohistcat.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblsnohistcat.Location = New System.Drawing.Point(1, 1)
        Me.lblsnohistcat.Name = "lblsnohistcat"
        Me.lblsnohistcat.Size = New System.Drawing.Size(130, 22)
        Me.lblsnohistcat.TabIndex = 14
        Me.lblsnohistcat.Text = "History Category :"
        Me.lblsnohistcat.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'TextBox2
        '
        Me.TextBox2.BackColor = System.Drawing.Color.White
        Me.TextBox2.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBox2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox2.ForeColor = System.Drawing.Color.Black
        Me.TextBox2.Location = New System.Drawing.Point(507, 5)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(87, 15)
        Me.TextBox2.TabIndex = 1
        Me.TextBox2.Visible = False
        '
        'Label237
        '
        Me.Label237.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label237.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label237.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label237.Location = New System.Drawing.Point(1, 23)
        Me.Label237.Name = "Label237"
        Me.Label237.Size = New System.Drawing.Size(611, 1)
        Me.Label237.TabIndex = 12
        Me.Label237.Text = "label2"
        '
        'Label238
        '
        Me.Label238.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label238.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label238.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label238.Location = New System.Drawing.Point(0, 1)
        Me.Label238.Name = "Label238"
        Me.Label238.Size = New System.Drawing.Size(1, 23)
        Me.Label238.TabIndex = 11
        Me.Label238.Text = "label4"
        '
        'Label239
        '
        Me.Label239.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label239.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label239.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label239.Location = New System.Drawing.Point(612, 1)
        Me.Label239.Name = "Label239"
        Me.Label239.Size = New System.Drawing.Size(1, 23)
        Me.Label239.TabIndex = 10
        Me.Label239.Text = "label3"
        '
        'Label240
        '
        Me.Label240.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label240.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label240.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label240.Location = New System.Drawing.Point(0, 0)
        Me.Label240.Name = "Label240"
        Me.Label240.Size = New System.Drawing.Size(613, 1)
        Me.Label240.TabIndex = 9
        Me.Label240.Text = "label1"
        '
        'Button2
        '
        Me.Button2.BackColor = System.Drawing.Color.Transparent
        Me.Button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.Button2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.Button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button2.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button2.Location = New System.Drawing.Point(254, 134)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(63, 22)
        Me.Button2.TabIndex = 2
        Me.Button2.Text = "Search"
        Me.Button2.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.Button2.UseVisualStyleBackColor = False
        Me.Button2.Visible = False
        '
        'pnlRadiology
        '
        Me.pnlRadiology.Controls.Add(Me.Panel18)
        Me.pnlRadiology.Controls.Add(Me.Panel17)
        Me.pnlRadiology.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlRadiology.Location = New System.Drawing.Point(0, 0)
        Me.pnlRadiology.Name = "pnlRadiology"
        Me.pnlRadiology.Size = New System.Drawing.Size(613, 574)
        Me.pnlRadiology.TabIndex = 1
        '
        'Panel18
        '
        Me.Panel18.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Panel18.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel18.Controls.Add(Me.Label170)
        Me.Panel18.Controls.Add(Me.Label171)
        Me.Panel18.Controls.Add(Me.Label172)
        Me.Panel18.Controls.Add(Me.Label173)
        Me.Panel18.Controls.Add(Me.c1Labs)
        Me.Panel18.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel18.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel18.Location = New System.Drawing.Point(0, 26)
        Me.Panel18.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Panel18.Name = "Panel18"
        Me.Panel18.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.Panel18.Size = New System.Drawing.Size(613, 548)
        Me.Panel18.TabIndex = 20
        '
        'Label170
        '
        Me.Label170.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label170.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label170.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label170.Location = New System.Drawing.Point(1, 544)
        Me.Label170.Name = "Label170"
        Me.Label170.Size = New System.Drawing.Size(611, 1)
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
        Me.Label171.Size = New System.Drawing.Size(1, 544)
        Me.Label171.TabIndex = 7
        Me.Label171.Text = "label4"
        '
        'Label172
        '
        Me.Label172.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label172.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label172.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label172.Location = New System.Drawing.Point(612, 1)
        Me.Label172.Name = "Label172"
        Me.Label172.Size = New System.Drawing.Size(1, 544)
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
        Me.Label173.Size = New System.Drawing.Size(613, 1)
        Me.Label173.TabIndex = 5
        Me.Label173.Text = "label1"
        '
        'c1Labs
        '
        Me.c1Labs.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.c1Labs.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.c1Labs.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.c1Labs.ColumnInfo = "10,0,0,0,0,95,Columns:"
        Me.c1Labs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.c1Labs.ExtendLastCol = True
        Me.c1Labs.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.c1Labs.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.c1Labs.Location = New System.Drawing.Point(0, 0)
        Me.c1Labs.Name = "c1Labs"
        Me.c1Labs.Rows.DefaultSize = 19
        Me.c1Labs.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.c1Labs.ShowCellLabels = True
        Me.c1Labs.Size = New System.Drawing.Size(613, 545)
        Me.c1Labs.StyleInfo = resources.GetString("c1Labs.StyleInfo")
        Me.c1Labs.TabIndex = 0
        Me.c1Labs.Tree.NodeImageCollapsed = CType(resources.GetObject("c1Labs.Tree.NodeImageCollapsed"), System.Drawing.Image)
        Me.c1Labs.Tree.NodeImageExpanded = CType(resources.GetObject("c1Labs.Tree.NodeImageExpanded"), System.Drawing.Image)
        '
        'Panel17
        '
        Me.Panel17.BackColor = System.Drawing.Color.Transparent
        Me.Panel17.Controls.Add(Me.txtLabsSearch)
        Me.Panel17.Controls.Add(Me.btnLabClear)
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
        Me.Panel17.Size = New System.Drawing.Size(613, 26)
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
        Me.txtLabsSearch.Size = New System.Drawing.Size(562, 15)
        Me.txtLabsSearch.TabIndex = 0
        '
        'btnLabClear
        '
        Me.btnLabClear.BackColor = System.Drawing.Color.Transparent
        Me.btnLabClear.BackgroundImage = CType(resources.GetObject("btnLabClear.BackgroundImage"), System.Drawing.Image)
        Me.btnLabClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnLabClear.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnLabClear.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnLabClear.FlatAppearance.BorderSize = 0
        Me.btnLabClear.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnLabClear.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnLabClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnLabClear.Image = CType(resources.GetObject("btnLabClear.Image"), System.Drawing.Image)
        Me.btnLabClear.Location = New System.Drawing.Point(591, 5)
        Me.btnLabClear.Name = "btnLabClear"
        Me.btnLabClear.Size = New System.Drawing.Size(21, 15)
        Me.btnLabClear.TabIndex = 48
        Me.ToolTip1.SetToolTip(Me.btnLabClear, "Clear Search")
        Me.btnLabClear.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.White
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label1.Location = New System.Drawing.Point(29, 1)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(583, 4)
        Me.Label1.TabIndex = 37
        '
        'Label165
        '
        Me.Label165.BackColor = System.Drawing.Color.White
        Me.Label165.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label165.Location = New System.Drawing.Point(29, 20)
        Me.Label165.Name = "Label165"
        Me.Label165.Size = New System.Drawing.Size(583, 2)
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
        Me.Label166.Size = New System.Drawing.Size(611, 1)
        Me.Label166.TabIndex = 35
        Me.Label166.Text = "label1"
        '
        'Label167
        '
        Me.Label167.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label167.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label167.Location = New System.Drawing.Point(1, 0)
        Me.Label167.Name = "Label167"
        Me.Label167.Size = New System.Drawing.Size(611, 1)
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
        Me.Label169.Location = New System.Drawing.Point(612, 0)
        Me.Label169.Name = "Label169"
        Me.Label169.Size = New System.Drawing.Size(1, 23)
        Me.Label169.TabIndex = 40
        Me.Label169.Text = "label4"
        '
        'pnlHistory
        '
        Me.pnlHistory.Controls.Add(Me.Panel22)
        Me.pnlHistory.Controls.Add(Me.Panel20)
        Me.pnlHistory.Controls.Add(Me.Panel16)
        Me.pnlHistory.Controls.Add(Me.Panel11)
        Me.pnlHistory.Controls.Add(Me.btnHistorySearch)
        Me.pnlHistory.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlHistory.Location = New System.Drawing.Point(0, 0)
        Me.pnlHistory.Name = "pnlHistory"
        Me.pnlHistory.Size = New System.Drawing.Size(613, 574)
        Me.pnlHistory.TabIndex = 0
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
        Me.Panel22.Location = New System.Drawing.Point(0, 411)
        Me.Panel22.Margin = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.Panel22.Name = "Panel22"
        Me.Panel22.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.Panel22.Size = New System.Drawing.Size(613, 163)
        Me.Panel22.TabIndex = 23
        '
        'Label193
        '
        Me.Label193.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label193.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label193.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label193.Location = New System.Drawing.Point(1, 159)
        Me.Label193.Name = "Label193"
        Me.Label193.Size = New System.Drawing.Size(611, 1)
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
        Me.Label194.Size = New System.Drawing.Size(1, 159)
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
        Me.trvSelectedHistory.Size = New System.Drawing.Size(612, 159)
        Me.trvSelectedHistory.TabIndex = 0
        '
        'Label195
        '
        Me.Label195.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label195.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label195.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label195.Location = New System.Drawing.Point(612, 1)
        Me.Label195.Name = "Label195"
        Me.Label195.Size = New System.Drawing.Size(1, 159)
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
        Me.Label196.Size = New System.Drawing.Size(613, 1)
        Me.Label196.TabIndex = 5
        Me.Label196.Text = "label1"
        '
        'Panel20
        '
        Me.Panel20.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel20.Controls.Add(Me.Panel21)
        Me.Panel20.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel20.Location = New System.Drawing.Point(0, 384)
        Me.Panel20.Name = "Panel20"
        Me.Panel20.Padding = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.Panel20.Size = New System.Drawing.Size(613, 27)
        Me.Panel20.TabIndex = 22
        '
        'Panel21
        '
        Me.Panel21.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Blue2007
        Me.Panel21.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel21.Controls.Add(Me.Label188)
        Me.Panel21.Controls.Add(Me.Label189)
        Me.Panel21.Controls.Add(Me.Label190)
        Me.Panel21.Controls.Add(Me.Label191)
        Me.Panel21.Controls.Add(Me.Label192)
        Me.Panel21.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel21.Location = New System.Drawing.Point(0, 3)
        Me.Panel21.Name = "Panel21"
        Me.Panel21.Size = New System.Drawing.Size(613, 21)
        Me.Panel21.TabIndex = 14
        '
        'Label188
        '
        Me.Label188.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label188.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label188.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label188.Location = New System.Drawing.Point(612, 1)
        Me.Label188.Name = "Label188"
        Me.Label188.Size = New System.Drawing.Size(1, 19)
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
        Me.Label189.Size = New System.Drawing.Size(1, 19)
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
        Me.Label190.Size = New System.Drawing.Size(613, 1)
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
        Me.Label191.Size = New System.Drawing.Size(613, 20)
        Me.Label191.TabIndex = 9
        Me.Label191.Text = "      Selected History"
        Me.Label191.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label192
        '
        Me.Label192.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label192.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label192.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label192.Location = New System.Drawing.Point(0, 20)
        Me.Label192.Name = "Label192"
        Me.Label192.Size = New System.Drawing.Size(613, 1)
        Me.Label192.TabIndex = 13
        Me.Label192.Text = "label2"
        '
        'Panel16
        '
        Me.Panel16.Controls.Add(Me.Panel9)
        Me.Panel16.Controls.Add(Me.pnlHistoryLeft)
        Me.Panel16.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel16.Location = New System.Drawing.Point(0, 27)
        Me.Panel16.Name = "Panel16"
        Me.Panel16.Size = New System.Drawing.Size(613, 357)
        Me.Panel16.TabIndex = 23
        '
        'Panel9
        '
        Me.Panel9.Controls.Add(Me.GloUC_trvHistory)
        Me.Panel9.Controls.Add(Me.trvHistoryRight)
        Me.Panel9.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel9.Location = New System.Drawing.Point(287, 0)
        Me.Panel9.Name = "Panel9"
        Me.Panel9.Size = New System.Drawing.Size(326, 357)
        Me.Panel9.TabIndex = 20
        '
        'GloUC_trvHistory
        '
        Me.GloUC_trvHistory.BackColor = System.Drawing.Color.Transparent
        Me.GloUC_trvHistory.CheckBoxes = False
        Me.GloUC_trvHistory.CodeMember = Nothing
        Me.GloUC_trvHistory.Comment = Nothing
        Me.GloUC_trvHistory.ConceptID = Nothing
        Me.GloUC_trvHistory.CPT = Nothing

        Me.GloUC_trvHistory.DescriptionMember = Nothing
        Me.GloUC_trvHistory.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Code_Description
        Me.GloUC_trvHistory.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GloUC_trvHistory.DrugFlag = CType(16, Short)
        Me.GloUC_trvHistory.DrugFormMember = Nothing
        Me.GloUC_trvHistory.DrugQtyQualifierMember = Nothing
        Me.GloUC_trvHistory.DurationMember = Nothing
        Me.GloUC_trvHistory.FrequencyMember = Nothing
        Me.GloUC_trvHistory.HistoryType = Nothing
        Me.GloUC_trvHistory.ICD9 = Nothing
        Me.GloUC_trvHistory.ImageIndex = 0
        Me.GloUC_trvHistory.ImageList = Me.ImageList1
        Me.GloUC_trvHistory.ImageObject = Nothing
        Me.GloUC_trvHistory.Indicator = Nothing
        Me.GloUC_trvHistory.IsDrug = False
        Me.GloUC_trvHistory.IsNarcoticsMember = Nothing
        Me.GloUC_trvHistory.IsSystemCategory = Nothing
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
        Me.GloUC_trvHistory.Size = New System.Drawing.Size(326, 357)
        Me.GloUC_trvHistory.Sort = gloUserControlLibrary.gloUC_TreeView.enumSortType.ByDescription
        Me.GloUC_trvHistory.TabIndex = 48
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
        Me.trvHistoryRight.Size = New System.Drawing.Size(326, 274)
        Me.trvHistoryRight.TabIndex = 2
        Me.trvHistoryRight.Visible = False
        '
        'pnlHistoryLeft
        '
        Me.pnlHistoryLeft.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlHistoryLeft.Controls.Add(Me.Label120)
        Me.pnlHistoryLeft.Controls.Add(Me.Label121)
        Me.pnlHistoryLeft.Controls.Add(Me.Label122)
        Me.pnlHistoryLeft.Controls.Add(Me.Label123)
        Me.pnlHistoryLeft.Controls.Add(Me.trvHistory)
        Me.pnlHistoryLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlHistoryLeft.Enabled = False
        Me.pnlHistoryLeft.Location = New System.Drawing.Point(0, 0)
        Me.pnlHistoryLeft.Name = "pnlHistoryLeft"
        Me.pnlHistoryLeft.Padding = New System.Windows.Forms.Padding(0, 0, 3, 0)
        Me.pnlHistoryLeft.Size = New System.Drawing.Size(287, 357)
        Me.pnlHistoryLeft.TabIndex = 1
        Me.pnlHistoryLeft.Visible = False
        '
        'Label120
        '
        Me.Label120.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label120.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label120.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label120.Location = New System.Drawing.Point(1, 356)
        Me.Label120.Name = "Label120"
        Me.Label120.Size = New System.Drawing.Size(282, 1)
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
        Me.Label121.Size = New System.Drawing.Size(1, 356)
        Me.Label121.TabIndex = 11
        Me.Label121.Text = "label4"
        '
        'Label122
        '
        Me.Label122.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label122.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label122.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label122.Location = New System.Drawing.Point(283, 1)
        Me.Label122.Name = "Label122"
        Me.Label122.Size = New System.Drawing.Size(1, 356)
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
        Me.Label123.Size = New System.Drawing.Size(284, 1)
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
        Me.trvHistory.Size = New System.Drawing.Size(284, 357)
        Me.trvHistory.TabIndex = 0
        Me.trvHistory.Visible = False
        '
        'Panel11
        '
        Me.Panel11.Controls.Add(Me.Panel8)
        Me.Panel11.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel11.Location = New System.Drawing.Point(0, 0)
        Me.Panel11.Name = "Panel11"
        Me.Panel11.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.Panel11.Size = New System.Drawing.Size(613, 27)
        Me.Panel11.TabIndex = 21
        '
        'Panel8
        '
        Me.Panel8.BackColor = System.Drawing.Color.Transparent
        Me.Panel8.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.Panel8.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel8.Controls.Add(Me.cmbHistoryCategory)
        Me.Panel8.Controls.Add(Me.Label140)
        Me.Panel8.Controls.Add(Me.txtSearch)
        Me.Panel8.Controls.Add(Me.Label8)
        Me.Panel8.Controls.Add(Me.Label14)
        Me.Panel8.Controls.Add(Me.Label115)
        Me.Panel8.Controls.Add(Me.Label116)
        Me.Panel8.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel8.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel8.ForeColor = System.Drawing.Color.Black
        Me.Panel8.Location = New System.Drawing.Point(0, 0)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(613, 24)
        Me.Panel8.TabIndex = 19
        '
        'cmbHistoryCategory
        '
        Me.cmbHistoryCategory.Dock = System.Windows.Forms.DockStyle.Left
        Me.cmbHistoryCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbHistoryCategory.ForeColor = System.Drawing.Color.Black
        Me.cmbHistoryCategory.FormattingEnabled = True
        Me.cmbHistoryCategory.Location = New System.Drawing.Point(131, 1)
        Me.cmbHistoryCategory.Name = "cmbHistoryCategory"
        Me.cmbHistoryCategory.Size = New System.Drawing.Size(284, 22)
        Me.cmbHistoryCategory.TabIndex = 0
        '
        'Label140
        '
        Me.Label140.BackColor = System.Drawing.Color.Transparent
        Me.Label140.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label140.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label140.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label140.Location = New System.Drawing.Point(1, 1)
        Me.Label140.Name = "Label140"
        Me.Label140.Size = New System.Drawing.Size(130, 22)
        Me.Label140.TabIndex = 13
        Me.Label140.Text = "History Category :"
        Me.Label140.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtSearch
        '
        Me.txtSearch.BackColor = System.Drawing.Color.White
        Me.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtSearch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSearch.ForeColor = System.Drawing.Color.Black
        Me.txtSearch.Location = New System.Drawing.Point(507, 5)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(87, 15)
        Me.txtSearch.TabIndex = 1
        Me.txtSearch.Visible = False
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label8.Location = New System.Drawing.Point(1, 23)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(611, 1)
        Me.Label8.TabIndex = 12
        Me.Label8.Text = "label2"
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label14.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label14.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(0, 1)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(1, 23)
        Me.Label14.TabIndex = 11
        Me.Label14.Text = "label4"
        '
        'Label115
        '
        Me.Label115.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label115.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label115.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label115.Location = New System.Drawing.Point(612, 1)
        Me.Label115.Name = "Label115"
        Me.Label115.Size = New System.Drawing.Size(1, 23)
        Me.Label115.TabIndex = 10
        Me.Label115.Text = "label3"
        '
        'Label116
        '
        Me.Label116.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label116.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label116.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label116.Location = New System.Drawing.Point(0, 0)
        Me.Label116.Name = "Label116"
        Me.Label116.Size = New System.Drawing.Size(613, 1)
        Me.Label116.TabIndex = 9
        Me.Label116.Text = "label1"
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
        'pnlICD9
        '
        Me.pnlICD9.Controls.Add(Me.Panel15)
        Me.pnlICD9.Controls.Add(Me.Panel5)
        Me.pnlICD9.Controls.Add(Me.Splitter4)
        Me.pnlICD9.Controls.Add(Me.GloUC_trvICD9)
        Me.pnlICD9.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlICD9.Location = New System.Drawing.Point(0, 0)
        Me.pnlICD9.Name = "pnlICD9"
        Me.pnlICD9.Size = New System.Drawing.Size(613, 574)
        Me.pnlICD9.TabIndex = 1
        Me.pnlICD9.Visible = False
        '
        'Panel15
        '
        Me.Panel15.BackColor = System.Drawing.Color.Transparent
        Me.Panel15.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel15.Controls.Add(Me.Label132)
        Me.Panel15.Controls.Add(Me.Label133)
        Me.Panel15.Controls.Add(Me.trvselecteICDs)
        Me.Panel15.Controls.Add(Me.Label134)
        Me.Panel15.Controls.Add(Me.Label135)
        Me.Panel15.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel15.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel15.Location = New System.Drawing.Point(0, 420)
        Me.Panel15.Margin = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.Panel15.Name = "Panel15"
        Me.Panel15.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.Panel15.Size = New System.Drawing.Size(613, 154)
        Me.Panel15.TabIndex = 50
        '
        'Label132
        '
        Me.Label132.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label132.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label132.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label132.Location = New System.Drawing.Point(1, 150)
        Me.Label132.Name = "Label132"
        Me.Label132.Size = New System.Drawing.Size(611, 1)
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
        Me.Label133.Size = New System.Drawing.Size(1, 150)
        Me.Label133.TabIndex = 7
        Me.Label133.Text = "label4"
        '
        'trvselecteICDs
        '
        Me.trvselecteICDs.BackColor = System.Drawing.Color.White
        Me.trvselecteICDs.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trvselecteICDs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trvselecteICDs.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.trvselecteICDs.ForeColor = System.Drawing.Color.Black
        Me.trvselecteICDs.HideSelection = False
        Me.trvselecteICDs.ImageIndex = 0
        Me.trvselecteICDs.ImageList = Me.ImageList1
        Me.trvselecteICDs.ItemHeight = 18
        Me.trvselecteICDs.Location = New System.Drawing.Point(0, 1)
        Me.trvselecteICDs.Name = "trvselecteICDs"
        Me.trvselecteICDs.SelectedImageIndex = 0
        Me.trvselecteICDs.ShowLines = False
        Me.trvselecteICDs.Size = New System.Drawing.Size(612, 150)
        Me.trvselecteICDs.TabIndex = 0
        '
        'Label134
        '
        Me.Label134.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label134.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label134.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label134.Location = New System.Drawing.Point(612, 1)
        Me.Label134.Name = "Label134"
        Me.Label134.Size = New System.Drawing.Size(1, 150)
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
        Me.Label135.Size = New System.Drawing.Size(613, 1)
        Me.Label135.TabIndex = 5
        Me.Label135.Text = "label1"
        '
        'Panel5
        '
        Me.Panel5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel5.Controls.Add(Me.Panel10)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel5.Location = New System.Drawing.Point(0, 393)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.Panel5.Size = New System.Drawing.Size(613, 27)
        Me.Panel5.TabIndex = 49
        '
        'Panel10
        '
        Me.Panel10.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Blue2007
        Me.Panel10.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel10.Controls.Add(Me.Label13)
        Me.Panel10.Controls.Add(Me.Label128)
        Me.Panel10.Controls.Add(Me.Label129)
        Me.Panel10.Controls.Add(Me.Label130)
        Me.Panel10.Controls.Add(Me.Label131)
        Me.Panel10.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel10.Location = New System.Drawing.Point(0, 0)
        Me.Panel10.Name = "Panel10"
        Me.Panel10.Size = New System.Drawing.Size(613, 24)
        Me.Panel10.TabIndex = 14
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label13.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label13.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label13.Location = New System.Drawing.Point(612, 1)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(1, 22)
        Me.Label13.TabIndex = 11
        Me.Label13.Text = "label3"
        '
        'Label128
        '
        Me.Label128.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label128.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label128.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label128.Location = New System.Drawing.Point(0, 1)
        Me.Label128.Name = "Label128"
        Me.Label128.Size = New System.Drawing.Size(1, 22)
        Me.Label128.TabIndex = 12
        Me.Label128.Text = "label4"
        '
        'Label129
        '
        Me.Label129.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label129.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label129.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label129.Location = New System.Drawing.Point(0, 0)
        Me.Label129.Name = "Label129"
        Me.Label129.Size = New System.Drawing.Size(613, 1)
        Me.Label129.TabIndex = 10
        Me.Label129.Text = "label1"
        '
        'Label130
        '
        Me.Label130.BackColor = System.Drawing.Color.Transparent
        Me.Label130.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label130.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label130.ForeColor = System.Drawing.Color.White
        Me.Label130.Image = CType(resources.GetObject("Label130.Image"), System.Drawing.Image)
        Me.Label130.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label130.Location = New System.Drawing.Point(0, 0)
        Me.Label130.Name = "Label130"
        Me.Label130.Size = New System.Drawing.Size(613, 23)
        Me.Label130.TabIndex = 9
        Me.Label130.Text = "      Selected ICD9"
        Me.Label130.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label131
        '
        Me.Label131.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label131.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label131.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label131.Location = New System.Drawing.Point(0, 23)
        Me.Label131.Name = "Label131"
        Me.Label131.Size = New System.Drawing.Size(613, 1)
        Me.Label131.TabIndex = 13
        Me.Label131.Text = "label2"
        '
        'Splitter4
        '
        Me.Splitter4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Splitter4.Location = New System.Drawing.Point(0, 390)
        Me.Splitter4.Name = "Splitter4"
        Me.Splitter4.Size = New System.Drawing.Size(613, 3)
        Me.Splitter4.TabIndex = 51
        Me.Splitter4.TabStop = False
        '
        'GloUC_trvICD9
        '
        Me.GloUC_trvICD9.BackColor = System.Drawing.Color.Transparent
        Me.GloUC_trvICD9.CheckBoxes = False
        Me.GloUC_trvICD9.CodeMember = Nothing
        Me.GloUC_trvICD9.Comment = Nothing
        Me.GloUC_trvICD9.ConceptID = Nothing
        Me.GloUC_trvICD9.CPT = Nothing
        Me.GloUC_trvICD9.DescriptionMember = Nothing
        Me.GloUC_trvICD9.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Code_Description
        Me.GloUC_trvICD9.Dock = System.Windows.Forms.DockStyle.Top
        Me.GloUC_trvICD9.DrugFlag = CType(16, Short)
        Me.GloUC_trvICD9.DrugFormMember = Nothing
        Me.GloUC_trvICD9.DrugQtyQualifierMember = Nothing
        Me.GloUC_trvICD9.DurationMember = Nothing
        Me.GloUC_trvICD9.FrequencyMember = Nothing
        Me.GloUC_trvICD9.HistoryType = Nothing
        Me.GloUC_trvICD9.ICD9 = Nothing
        Me.GloUC_trvICD9.ImageIndex = 0
        Me.GloUC_trvICD9.ImageList = Me.ImageList1
        Me.GloUC_trvICD9.ImageObject = Nothing
        Me.GloUC_trvICD9.Indicator = Nothing
        Me.GloUC_trvICD9.IsDrug = False
        Me.GloUC_trvICD9.IsNarcoticsMember = Nothing
        Me.GloUC_trvICD9.IsSystemCategory = Nothing
        Me.GloUC_trvICD9.Location = New System.Drawing.Point(0, 0)
        Me.GloUC_trvICD9.MaximumNodes = 1000
        Me.GloUC_trvICD9.Name = "GloUC_trvICD9"
        Me.GloUC_trvICD9.NDCCodeMember = Nothing
        Me.GloUC_trvICD9.ParentImageIndex = 0
        Me.GloUC_trvICD9.ParentMember = Nothing
        Me.GloUC_trvICD9.RouteMember = Nothing
        Me.GloUC_trvICD9.Search = gloUserControlLibrary.gloUC_TreeView.enumSearchType.Instring
        Me.GloUC_trvICD9.SearchBox = True
        Me.GloUC_trvICD9.SearchText = Nothing
        Me.GloUC_trvICD9.SelectedImageIndex = 0
        Me.GloUC_trvICD9.SelectedNode = Nothing
        Me.GloUC_trvICD9.SelectedNodeIDs = CType(resources.GetObject("GloUC_trvICD9.SelectedNodeIDs"), System.Collections.ArrayList)
        Me.GloUC_trvICD9.SelectedParentImageIndex = 0
        Me.GloUC_trvICD9.Size = New System.Drawing.Size(613, 390)
        Me.GloUC_trvICD9.Sort = gloUserControlLibrary.gloUC_TreeView.enumSortType.ByDescription
        Me.GloUC_trvICD9.TabIndex = 47
        Me.GloUC_trvICD9.Tag = Nothing
        Me.GloUC_trvICD9.UnitMember = Nothing
        Me.GloUC_trvICD9.ValueMember = Nothing
        '
        'pnlCPT
        '
        Me.pnlCPT.Controls.Add(Me.pnlSelectedCPTs)
        Me.pnlCPT.Controls.Add(Me.pnlSelecteCPTsLabels)
        Me.pnlCPT.Controls.Add(Me.Splitter1)
        Me.pnlCPT.Controls.Add(Me.GloUC_trvCPT)
        Me.pnlCPT.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlCPT.Location = New System.Drawing.Point(0, 0)
        Me.pnlCPT.Name = "pnlCPT"
        Me.pnlCPT.Size = New System.Drawing.Size(613, 574)
        Me.pnlCPT.TabIndex = 1
        '
        'pnlSelectedCPTs
        '
        Me.pnlSelectedCPTs.BackColor = System.Drawing.Color.Transparent
        Me.pnlSelectedCPTs.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlSelectedCPTs.Controls.Add(Me.Label205)
        Me.pnlSelectedCPTs.Controls.Add(Me.Label206)
        Me.pnlSelectedCPTs.Controls.Add(Me.trvselectedCPT)
        Me.pnlSelectedCPTs.Controls.Add(Me.Label207)
        Me.pnlSelectedCPTs.Controls.Add(Me.Label208)
        Me.pnlSelectedCPTs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlSelectedCPTs.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlSelectedCPTs.Location = New System.Drawing.Point(0, 422)
        Me.pnlSelectedCPTs.Margin = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.pnlSelectedCPTs.Name = "pnlSelectedCPTs"
        Me.pnlSelectedCPTs.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.pnlSelectedCPTs.Size = New System.Drawing.Size(613, 152)
        Me.pnlSelectedCPTs.TabIndex = 47
        '
        'Label205
        '
        Me.Label205.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label205.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label205.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label205.Location = New System.Drawing.Point(1, 148)
        Me.Label205.Name = "Label205"
        Me.Label205.Size = New System.Drawing.Size(611, 1)
        Me.Label205.TabIndex = 8
        Me.Label205.Text = "label2"
        '
        'Label206
        '
        Me.Label206.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label206.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label206.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label206.Location = New System.Drawing.Point(0, 1)
        Me.Label206.Name = "Label206"
        Me.Label206.Size = New System.Drawing.Size(1, 148)
        Me.Label206.TabIndex = 7
        Me.Label206.Text = "label4"
        '
        'trvselectedCPT
        '
        Me.trvselectedCPT.BackColor = System.Drawing.Color.White
        Me.trvselectedCPT.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trvselectedCPT.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trvselectedCPT.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.trvselectedCPT.ForeColor = System.Drawing.Color.Black
        Me.trvselectedCPT.HideSelection = False
        Me.trvselectedCPT.ImageIndex = 0
        Me.trvselectedCPT.ImageList = Me.ImageList1
        Me.trvselectedCPT.ItemHeight = 18
        Me.trvselectedCPT.Location = New System.Drawing.Point(0, 1)
        Me.trvselectedCPT.Name = "trvselectedCPT"
        Me.trvselectedCPT.SelectedImageIndex = 0
        Me.trvselectedCPT.ShowLines = False
        Me.trvselectedCPT.Size = New System.Drawing.Size(612, 148)
        Me.trvselectedCPT.TabIndex = 0
        '
        'Label207
        '
        Me.Label207.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label207.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label207.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label207.Location = New System.Drawing.Point(612, 1)
        Me.Label207.Name = "Label207"
        Me.Label207.Size = New System.Drawing.Size(1, 148)
        Me.Label207.TabIndex = 6
        Me.Label207.Text = "label3"
        '
        'Label208
        '
        Me.Label208.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label208.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label208.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label208.Location = New System.Drawing.Point(0, 0)
        Me.Label208.Name = "Label208"
        Me.Label208.Size = New System.Drawing.Size(613, 1)
        Me.Label208.TabIndex = 5
        Me.Label208.Text = "label1"
        '
        'pnlSelecteCPTsLabels
        '
        Me.pnlSelecteCPTsLabels.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlSelecteCPTsLabels.Controls.Add(Me.Panel28)
        Me.pnlSelecteCPTsLabels.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlSelecteCPTsLabels.Location = New System.Drawing.Point(0, 395)
        Me.pnlSelecteCPTsLabels.Name = "pnlSelecteCPTsLabels"
        Me.pnlSelecteCPTsLabels.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.pnlSelecteCPTsLabels.Size = New System.Drawing.Size(613, 27)
        Me.pnlSelecteCPTsLabels.TabIndex = 48
        '
        'Panel28
        '
        Me.Panel28.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Blue2007
        Me.Panel28.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel28.Controls.Add(Me.Label209)
        Me.Panel28.Controls.Add(Me.Label210)
        Me.Panel28.Controls.Add(Me.Label211)
        Me.Panel28.Controls.Add(Me.Label212)
        Me.Panel28.Controls.Add(Me.Label213)
        Me.Panel28.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel28.Location = New System.Drawing.Point(0, 0)
        Me.Panel28.Name = "Panel28"
        Me.Panel28.Size = New System.Drawing.Size(613, 24)
        Me.Panel28.TabIndex = 14
        '
        'Label209
        '
        Me.Label209.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label209.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label209.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label209.Location = New System.Drawing.Point(612, 1)
        Me.Label209.Name = "Label209"
        Me.Label209.Size = New System.Drawing.Size(1, 22)
        Me.Label209.TabIndex = 11
        Me.Label209.Text = "label3"
        '
        'Label210
        '
        Me.Label210.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label210.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label210.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label210.Location = New System.Drawing.Point(0, 1)
        Me.Label210.Name = "Label210"
        Me.Label210.Size = New System.Drawing.Size(1, 22)
        Me.Label210.TabIndex = 12
        Me.Label210.Text = "label4"
        '
        'Label211
        '
        Me.Label211.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label211.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label211.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label211.Location = New System.Drawing.Point(0, 0)
        Me.Label211.Name = "Label211"
        Me.Label211.Size = New System.Drawing.Size(613, 1)
        Me.Label211.TabIndex = 10
        Me.Label211.Text = "label1"
        '
        'Label212
        '
        Me.Label212.BackColor = System.Drawing.Color.Transparent
        Me.Label212.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label212.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label212.ForeColor = System.Drawing.Color.White
        Me.Label212.Image = CType(resources.GetObject("Label212.Image"), System.Drawing.Image)
        Me.Label212.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label212.Location = New System.Drawing.Point(0, 0)
        Me.Label212.Name = "Label212"
        Me.Label212.Size = New System.Drawing.Size(613, 23)
        Me.Label212.TabIndex = 9
        Me.Label212.Text = "      Selected CPT"
        Me.Label212.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label213
        '
        Me.Label213.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label213.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label213.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label213.Location = New System.Drawing.Point(0, 23)
        Me.Label213.Name = "Label213"
        Me.Label213.Size = New System.Drawing.Size(613, 1)
        Me.Label213.TabIndex = 13
        Me.Label213.Text = "label2"
        '
        'Splitter1
        '
        Me.Splitter1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Splitter1.Location = New System.Drawing.Point(0, 392)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(613, 3)
        Me.Splitter1.TabIndex = 49
        Me.Splitter1.TabStop = False
        '
        'GloUC_trvCPT
        '
        Me.GloUC_trvCPT.BackColor = System.Drawing.Color.Transparent
        Me.GloUC_trvCPT.CheckBoxes = False
        Me.GloUC_trvCPT.CodeMember = Nothing
        Me.GloUC_trvCPT.Comment = Nothing
        Me.GloUC_trvCPT.ConceptID = Nothing
        Me.GloUC_trvCPT.CPT = Nothing

        Me.GloUC_trvCPT.DescriptionMember = Nothing
        Me.GloUC_trvCPT.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Code_Description
        Me.GloUC_trvCPT.Dock = System.Windows.Forms.DockStyle.Top
        Me.GloUC_trvCPT.DrugFlag = CType(16, Short)
        Me.GloUC_trvCPT.DrugFormMember = Nothing
        Me.GloUC_trvCPT.DrugQtyQualifierMember = Nothing
        Me.GloUC_trvCPT.DurationMember = Nothing
        Me.GloUC_trvCPT.FrequencyMember = Nothing
        Me.GloUC_trvCPT.HistoryType = Nothing
        Me.GloUC_trvCPT.ICD9 = Nothing
        Me.GloUC_trvCPT.ImageIndex = 0
        Me.GloUC_trvCPT.ImageList = Me.ImageList1
        Me.GloUC_trvCPT.ImageObject = Nothing
        Me.GloUC_trvCPT.Indicator = Nothing
        Me.GloUC_trvCPT.IsDrug = False
        Me.GloUC_trvCPT.IsNarcoticsMember = Nothing
        Me.GloUC_trvCPT.IsSystemCategory = Nothing
        Me.GloUC_trvCPT.Location = New System.Drawing.Point(0, 0)
        Me.GloUC_trvCPT.MaximumNodes = 1000
        Me.GloUC_trvCPT.Name = "GloUC_trvCPT"
        Me.GloUC_trvCPT.NDCCodeMember = Nothing
        Me.GloUC_trvCPT.ParentImageIndex = 0
        Me.GloUC_trvCPT.ParentMember = Nothing
        Me.GloUC_trvCPT.RouteMember = Nothing
        Me.GloUC_trvCPT.Search = gloUserControlLibrary.gloUC_TreeView.enumSearchType.Instring
        Me.GloUC_trvCPT.SearchBox = True
        Me.GloUC_trvCPT.SearchText = Nothing
        Me.GloUC_trvCPT.SelectedImageIndex = 0
        Me.GloUC_trvCPT.SelectedNode = Nothing
        Me.GloUC_trvCPT.SelectedNodeIDs = CType(resources.GetObject("GloUC_trvCPT.SelectedNodeIDs"), System.Collections.ArrayList)
        Me.GloUC_trvCPT.SelectedParentImageIndex = 0
        Me.GloUC_trvCPT.Size = New System.Drawing.Size(613, 392)
        Me.GloUC_trvCPT.Sort = gloUserControlLibrary.gloUC_TreeView.enumSortType.ByDescription
        Me.GloUC_trvCPT.TabIndex = 46
        Me.GloUC_trvCPT.Tag = Nothing
        Me.GloUC_trvCPT.UnitMember = Nothing
        Me.GloUC_trvCPT.ValueMember = Nothing
        '
        'pnlDrugs
        '
        Me.pnlDrugs.Controls.Add(Me.pnltrvSelectedDrugs)
        Me.pnlDrugs.Controls.Add(Me.pnlSelectedDrugLabel)
        Me.pnlDrugs.Controls.Add(Me.Splitter3)
        Me.pnlDrugs.Controls.Add(Me.GloUC_trvDrugs)
        Me.pnlDrugs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlDrugs.Location = New System.Drawing.Point(0, 0)
        Me.pnlDrugs.Name = "pnlDrugs"
        Me.pnlDrugs.Size = New System.Drawing.Size(613, 574)
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
        Me.pnltrvSelectedDrugs.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.pnltrvSelectedDrugs.Size = New System.Drawing.Size(613, 152)
        Me.pnltrvSelectedDrugs.TabIndex = 21
        '
        'Label86
        '
        Me.Label86.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label86.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label86.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label86.Location = New System.Drawing.Point(1, 148)
        Me.Label86.Name = "Label86"
        Me.Label86.Size = New System.Drawing.Size(611, 1)
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
        Me.Label180.Size = New System.Drawing.Size(1, 148)
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
        Me.trvSelectedDrugs.Size = New System.Drawing.Size(612, 148)
        Me.trvSelectedDrugs.TabIndex = 0
        '
        'Label181
        '
        Me.Label181.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label181.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label181.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label181.Location = New System.Drawing.Point(612, 1)
        Me.Label181.Name = "Label181"
        Me.Label181.Size = New System.Drawing.Size(1, 148)
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
        Me.Label182.Size = New System.Drawing.Size(613, 1)
        Me.Label182.TabIndex = 5
        Me.Label182.Text = "label1"
        '
        'pnlSelectedDrugLabel
        '
        Me.pnlSelectedDrugLabel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlSelectedDrugLabel.Controls.Add(Me.Panel4)
        Me.pnlSelectedDrugLabel.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlSelectedDrugLabel.Location = New System.Drawing.Point(0, 395)
        Me.pnlSelectedDrugLabel.Name = "pnlSelectedDrugLabel"
        Me.pnlSelectedDrugLabel.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.pnlSelectedDrugLabel.Size = New System.Drawing.Size(613, 27)
        Me.pnlSelectedDrugLabel.TabIndex = 20
        '
        'Panel4
        '
        Me.Panel4.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Blue2007
        Me.Panel4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel4.Controls.Add(Me.Label185)
        Me.Panel4.Controls.Add(Me.Label184)
        Me.Panel4.Controls.Add(Me.Label186)
        Me.Panel4.Controls.Add(Me.Label187)
        Me.Panel4.Controls.Add(Me.Label183)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel4.Location = New System.Drawing.Point(0, 0)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(613, 24)
        Me.Panel4.TabIndex = 14
        '
        'Label185
        '
        Me.Label185.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label185.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label185.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label185.Location = New System.Drawing.Point(612, 1)
        Me.Label185.Name = "Label185"
        Me.Label185.Size = New System.Drawing.Size(1, 22)
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
        Me.Label184.Size = New System.Drawing.Size(1, 22)
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
        Me.Label186.Size = New System.Drawing.Size(613, 1)
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
        Me.Label187.Size = New System.Drawing.Size(613, 23)
        Me.Label187.TabIndex = 9
        Me.Label187.Text = "      Selected Drugs"
        Me.Label187.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label183
        '
        Me.Label183.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label183.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label183.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label183.Location = New System.Drawing.Point(0, 23)
        Me.Label183.Name = "Label183"
        Me.Label183.Size = New System.Drawing.Size(613, 1)
        Me.Label183.TabIndex = 13
        Me.Label183.Text = "label2"
        '
        'Splitter3
        '
        Me.Splitter3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Splitter3.Location = New System.Drawing.Point(0, 392)
        Me.Splitter3.Name = "Splitter3"
        Me.Splitter3.Size = New System.Drawing.Size(613, 3)
        Me.Splitter3.TabIndex = 22
        Me.Splitter3.TabStop = False
        '
        'GloUC_trvDrugs
        '
        Me.GloUC_trvDrugs.BackColor = System.Drawing.Color.Transparent
        Me.GloUC_trvDrugs.CheckBoxes = False
        Me.GloUC_trvDrugs.CodeMember = Nothing
        Me.GloUC_trvDrugs.Comment = Nothing
        Me.GloUC_trvDrugs.ConceptID = Nothing
        Me.GloUC_trvDrugs.CPT = Nothing

        Me.GloUC_trvDrugs.DescriptionMember = Nothing
        Me.GloUC_trvDrugs.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Code_Description
        Me.GloUC_trvDrugs.Dock = System.Windows.Forms.DockStyle.Top
        Me.GloUC_trvDrugs.DrugFlag = CType(16, Short)
        Me.GloUC_trvDrugs.DrugFormMember = Nothing
        Me.GloUC_trvDrugs.DrugQtyQualifierMember = Nothing
        Me.GloUC_trvDrugs.DurationMember = Nothing
        Me.GloUC_trvDrugs.FrequencyMember = Nothing
        Me.GloUC_trvDrugs.HistoryType = Nothing
        Me.GloUC_trvDrugs.ICD9 = Nothing
        Me.GloUC_trvDrugs.ImageIndex = 0
        Me.GloUC_trvDrugs.ImageList = Me.ImageList1
        Me.GloUC_trvDrugs.ImageObject = Nothing
        Me.GloUC_trvDrugs.Indicator = Nothing
        Me.GloUC_trvDrugs.IsDrug = False
        Me.GloUC_trvDrugs.IsNarcoticsMember = Nothing
        Me.GloUC_trvDrugs.IsSystemCategory = Nothing
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
        Me.GloUC_trvDrugs.Size = New System.Drawing.Size(613, 392)
        Me.GloUC_trvDrugs.Sort = gloUserControlLibrary.gloUC_TreeView.enumSortType.ByDescription
        Me.GloUC_trvDrugs.TabIndex = 9
        Me.GloUC_trvDrugs.Tag = Nothing
        Me.GloUC_trvDrugs.UnitMember = Nothing
        Me.GloUC_trvDrugs.ValueMember = Nothing
        '
        'pnlLab
        '
        Me.pnlLab.Controls.Add(Me.Panel12)
        Me.pnlLab.Controls.Add(Me.Panel13)
        Me.pnlLab.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlLab.Location = New System.Drawing.Point(0, 0)
        Me.pnlLab.Name = "pnlLab"
        Me.pnlLab.Size = New System.Drawing.Size(613, 574)
        Me.pnlLab.TabIndex = 2
        Me.pnlLab.Visible = False
        '
        'Panel12
        '
        Me.Panel12.BackColor = System.Drawing.Color.Transparent
        Me.Panel12.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel12.Controls.Add(Me.Label146)
        Me.Panel12.Controls.Add(Me.C1LabResult)
        Me.Panel12.Controls.Add(Me.Label147)
        Me.Panel12.Controls.Add(Me.Label148)
        Me.Panel12.Controls.Add(Me.Label149)
        Me.Panel12.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel12.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel12.Location = New System.Drawing.Point(0, 26)
        Me.Panel12.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Panel12.Name = "Panel12"
        Me.Panel12.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.Panel12.Size = New System.Drawing.Size(613, 548)
        Me.Panel12.TabIndex = 20
        '
        'Label146
        '
        Me.Label146.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label146.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label146.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label146.Location = New System.Drawing.Point(1, 544)
        Me.Label146.Name = "Label146"
        Me.Label146.Size = New System.Drawing.Size(611, 1)
        Me.Label146.TabIndex = 8
        Me.Label146.Text = "label2"
        '
        'C1LabResult
        '
        Me.C1LabResult.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.C1LabResult.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.C1LabResult.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.C1LabResult.ColumnInfo = "10,0,0,0,0,95,Columns:"
        Me.C1LabResult.Dock = System.Windows.Forms.DockStyle.Fill
        Me.C1LabResult.ExtendLastCol = True
        Me.C1LabResult.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.C1LabResult.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.C1LabResult.Location = New System.Drawing.Point(1, 1)
        Me.C1LabResult.Name = "C1LabResult"
        Me.C1LabResult.Rows.DefaultSize = 19
        Me.C1LabResult.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.C1LabResult.ShowCellLabels = True
        Me.C1LabResult.Size = New System.Drawing.Size(611, 544)
        Me.C1LabResult.StyleInfo = resources.GetString("C1LabResult.StyleInfo")
        Me.C1LabResult.TabIndex = 1
        Me.C1LabResult.Tree.NodeImageCollapsed = CType(resources.GetObject("C1LabResult.Tree.NodeImageCollapsed"), System.Drawing.Image)
        Me.C1LabResult.Tree.NodeImageExpanded = CType(resources.GetObject("C1LabResult.Tree.NodeImageExpanded"), System.Drawing.Image)
        '
        'Label147
        '
        Me.Label147.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label147.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label147.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label147.Location = New System.Drawing.Point(0, 1)
        Me.Label147.Name = "Label147"
        Me.Label147.Size = New System.Drawing.Size(1, 544)
        Me.Label147.TabIndex = 7
        Me.Label147.Text = "label4"
        '
        'Label148
        '
        Me.Label148.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label148.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label148.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label148.Location = New System.Drawing.Point(612, 1)
        Me.Label148.Name = "Label148"
        Me.Label148.Size = New System.Drawing.Size(1, 544)
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
        Me.Label149.Size = New System.Drawing.Size(613, 1)
        Me.Label149.TabIndex = 5
        Me.Label149.Text = "label1"
        '
        'Panel13
        '
        Me.Panel13.Controls.Add(Me.Panel23)
        Me.Panel13.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel13.Location = New System.Drawing.Point(0, 0)
        Me.Panel13.Name = "Panel13"
        Me.Panel13.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.Panel13.Size = New System.Drawing.Size(613, 26)
        Me.Panel13.TabIndex = 21
        '
        'Panel23
        '
        Me.Panel23.BackColor = System.Drawing.Color.Transparent
        Me.Panel23.Controls.Add(Me.txtLabResultSearch)
        Me.Panel23.Controls.Add(Me.btnLabResultClear)
        Me.Panel23.Controls.Add(Me.Label126)
        Me.Panel23.Controls.Add(Me.Label127)
        Me.Panel23.Controls.Add(Me.PictureBox2)
        Me.Panel23.Controls.Add(Me.Label136)
        Me.Panel23.Controls.Add(Me.Label137)
        Me.Panel23.Controls.Add(Me.Label138)
        Me.Panel23.Controls.Add(Me.Label139)
        Me.Panel23.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel23.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel23.ForeColor = System.Drawing.Color.Black
        Me.Panel23.Location = New System.Drawing.Point(0, 0)
        Me.Panel23.Name = "Panel23"
        Me.Panel23.Size = New System.Drawing.Size(613, 23)
        Me.Panel23.TabIndex = 21
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
        Me.txtLabResultSearch.Size = New System.Drawing.Size(562, 15)
        Me.txtLabResultSearch.TabIndex = 0
        '
        'btnLabResultClear
        '
        Me.btnLabResultClear.BackColor = System.Drawing.Color.Transparent
        Me.btnLabResultClear.BackgroundImage = CType(resources.GetObject("btnLabResultClear.BackgroundImage"), System.Drawing.Image)
        Me.btnLabResultClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnLabResultClear.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnLabResultClear.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnLabResultClear.FlatAppearance.BorderSize = 0
        Me.btnLabResultClear.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnLabResultClear.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnLabResultClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnLabResultClear.Image = CType(resources.GetObject("btnLabResultClear.Image"), System.Drawing.Image)
        Me.btnLabResultClear.Location = New System.Drawing.Point(591, 5)
        Me.btnLabResultClear.Name = "btnLabResultClear"
        Me.btnLabResultClear.Size = New System.Drawing.Size(21, 15)
        Me.btnLabResultClear.TabIndex = 48
        Me.ToolTip1.SetToolTip(Me.btnLabResultClear, "Clear Search")
        Me.btnLabResultClear.UseVisualStyleBackColor = False
        '
        'Label126
        '
        Me.Label126.BackColor = System.Drawing.Color.White
        Me.Label126.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label126.Location = New System.Drawing.Point(29, 1)
        Me.Label126.Name = "Label126"
        Me.Label126.Size = New System.Drawing.Size(583, 4)
        Me.Label126.TabIndex = 37
        '
        'Label127
        '
        Me.Label127.BackColor = System.Drawing.Color.White
        Me.Label127.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label127.Location = New System.Drawing.Point(29, 20)
        Me.Label127.Name = "Label127"
        Me.Label127.Size = New System.Drawing.Size(583, 2)
        Me.Label127.TabIndex = 38
        '
        'PictureBox2
        '
        Me.PictureBox2.BackColor = System.Drawing.Color.White
        Me.PictureBox2.Dock = System.Windows.Forms.DockStyle.Left
        Me.PictureBox2.Image = CType(resources.GetObject("PictureBox2.Image"), System.Drawing.Image)
        Me.PictureBox2.Location = New System.Drawing.Point(1, 1)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(28, 21)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox2.TabIndex = 9
        Me.PictureBox2.TabStop = False
        '
        'Label136
        '
        Me.Label136.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label136.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label136.Location = New System.Drawing.Point(1, 22)
        Me.Label136.Name = "Label136"
        Me.Label136.Size = New System.Drawing.Size(611, 1)
        Me.Label136.TabIndex = 35
        Me.Label136.Text = "label1"
        '
        'Label137
        '
        Me.Label137.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label137.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label137.Location = New System.Drawing.Point(1, 0)
        Me.Label137.Name = "Label137"
        Me.Label137.Size = New System.Drawing.Size(611, 1)
        Me.Label137.TabIndex = 36
        Me.Label137.Text = "label1"
        '
        'Label138
        '
        Me.Label138.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label138.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label138.Location = New System.Drawing.Point(0, 0)
        Me.Label138.Name = "Label138"
        Me.Label138.Size = New System.Drawing.Size(1, 23)
        Me.Label138.TabIndex = 39
        Me.Label138.Text = "label4"
        '
        'Label139
        '
        Me.Label139.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label139.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label139.Location = New System.Drawing.Point(612, 0)
        Me.Label139.Name = "Label139"
        Me.Label139.Size = New System.Drawing.Size(1, 23)
        Me.Label139.TabIndex = 40
        Me.Label139.Text = "label4"
        '
        'pnlSummaryOthers
        '
        Me.pnlSummaryOthers.Controls.Add(Me.pnlGuideline)
        Me.pnlSummaryOthers.Controls.Add(Me.pnlGuidelineHeader)
        Me.pnlSummaryOthers.Controls.Add(Me.Splitter5)
        Me.pnlSummaryOthers.Controls.Add(Me.pnlSummary)
        Me.pnlSummaryOthers.Controls.Add(Me.pnlSummaryHeader)
        Me.pnlSummaryOthers.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlSummaryOthers.Location = New System.Drawing.Point(0, 0)
        Me.pnlSummaryOthers.Name = "pnlSummaryOthers"
        Me.pnlSummaryOthers.Size = New System.Drawing.Size(613, 574)
        Me.pnlSummaryOthers.TabIndex = 2
        Me.pnlSummaryOthers.Visible = False
        '
        'pnlGuideline
        '
        Me.pnlGuideline.Controls.Add(Me.Label99)
        Me.pnlGuideline.Controls.Add(Me.Label100)
        Me.pnlGuideline.Controls.Add(Me.Label101)
        Me.pnlGuideline.Controls.Add(Me.Label102)
        Me.pnlGuideline.Controls.Add(Me.trOrderInfo)
        Me.pnlGuideline.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlGuideline.Location = New System.Drawing.Point(0, 417)
        Me.pnlGuideline.Name = "pnlGuideline"
        Me.pnlGuideline.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.pnlGuideline.Size = New System.Drawing.Size(613, 157)
        Me.pnlGuideline.TabIndex = 2
        '
        'Label99
        '
        Me.Label99.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label99.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label99.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label99.Location = New System.Drawing.Point(1, 153)
        Me.Label99.Name = "Label99"
        Me.Label99.Size = New System.Drawing.Size(611, 1)
        Me.Label99.TabIndex = 12
        Me.Label99.Text = "label2"
        '
        'Label100
        '
        Me.Label100.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label100.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label100.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label100.Location = New System.Drawing.Point(0, 1)
        Me.Label100.Name = "Label100"
        Me.Label100.Size = New System.Drawing.Size(1, 153)
        Me.Label100.TabIndex = 11
        '
        'Label101
        '
        Me.Label101.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label101.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label101.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label101.Location = New System.Drawing.Point(612, 1)
        Me.Label101.Name = "Label101"
        Me.Label101.Size = New System.Drawing.Size(1, 153)
        Me.Label101.TabIndex = 10
        Me.Label101.Text = "label3"
        '
        'Label102
        '
        Me.Label102.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label102.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label102.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label102.Location = New System.Drawing.Point(0, 0)
        Me.Label102.Name = "Label102"
        Me.Label102.Size = New System.Drawing.Size(613, 1)
        Me.Label102.TabIndex = 9
        Me.Label102.Text = "label1"
        '
        'trOrderInfo
        '
        Me.trOrderInfo.BackColor = System.Drawing.Color.White
        Me.trOrderInfo.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trOrderInfo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trOrderInfo.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.trOrderInfo.ForeColor = System.Drawing.Color.Black
        Me.trOrderInfo.HideSelection = False
        Me.trOrderInfo.ImageIndex = 0
        Me.trOrderInfo.ImageList = Me.ImageList1
        Me.trOrderInfo.ItemHeight = 21
        Me.trOrderInfo.Location = New System.Drawing.Point(0, 0)
        Me.trOrderInfo.Name = "trOrderInfo"
        Me.trOrderInfo.SelectedImageIndex = 0
        Me.trOrderInfo.ShowLines = False
        Me.trOrderInfo.Size = New System.Drawing.Size(613, 154)
        Me.trOrderInfo.TabIndex = 5
        '
        'pnlGuidelineHeader
        '
        Me.pnlGuidelineHeader.Controls.Add(Me.pnl3)
        Me.pnlGuidelineHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlGuidelineHeader.Location = New System.Drawing.Point(0, 390)
        Me.pnlGuidelineHeader.Name = "pnlGuidelineHeader"
        Me.pnlGuidelineHeader.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.pnlGuidelineHeader.Size = New System.Drawing.Size(613, 27)
        Me.pnlGuidelineHeader.TabIndex = 5
        '
        'pnl3
        '
        Me.pnl3.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Blue2007
        Me.pnl3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnl3.Controls.Add(Me.Label95)
        Me.pnl3.Controls.Add(Me.Label96)
        Me.pnl3.Controls.Add(Me.Label97)
        Me.pnl3.Controls.Add(Me.Label98)
        Me.pnl3.Controls.Add(Me.Label6)
        Me.pnl3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnl3.Location = New System.Drawing.Point(0, 0)
        Me.pnl3.Name = "pnl3"
        Me.pnl3.Size = New System.Drawing.Size(613, 24)
        Me.pnl3.TabIndex = 1
        '
        'Label95
        '
        Me.Label95.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label95.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label95.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label95.Location = New System.Drawing.Point(1, 23)
        Me.Label95.Name = "Label95"
        Me.Label95.Size = New System.Drawing.Size(611, 1)
        Me.Label95.TabIndex = 12
        Me.Label95.Text = "label2"
        '
        'Label96
        '
        Me.Label96.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label96.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label96.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label96.Location = New System.Drawing.Point(0, 1)
        Me.Label96.Name = "Label96"
        Me.Label96.Size = New System.Drawing.Size(1, 23)
        Me.Label96.TabIndex = 11
        '
        'Label97
        '
        Me.Label97.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label97.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label97.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label97.Location = New System.Drawing.Point(612, 1)
        Me.Label97.Name = "Label97"
        Me.Label97.Size = New System.Drawing.Size(1, 23)
        Me.Label97.TabIndex = 10
        Me.Label97.Text = "label3"
        '
        'Label98
        '
        Me.Label98.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label98.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label98.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label98.Location = New System.Drawing.Point(0, 0)
        Me.Label98.Name = "Label98"
        Me.Label98.Size = New System.Drawing.Size(613, 1)
        Me.Label98.TabIndex = 9
        Me.Label98.Text = "label1"
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.White
        Me.Label6.Image = CType(resources.GetObject("Label6.Image"), System.Drawing.Image)
        Me.Label6.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label6.Location = New System.Drawing.Point(0, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(613, 24)
        Me.Label6.TabIndex = 0
        Me.Label6.Text = "       Orders to be Given"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Splitter5
        '
        Me.Splitter5.Dock = System.Windows.Forms.DockStyle.Top
        Me.Splitter5.Location = New System.Drawing.Point(0, 387)
        Me.Splitter5.Name = "Splitter5"
        Me.Splitter5.Size = New System.Drawing.Size(613, 3)
        Me.Splitter5.TabIndex = 6
        Me.Splitter5.TabStop = False
        '
        'pnlSummary
        '
        Me.pnlSummary.Controls.Add(Me.Label91)
        Me.pnlSummary.Controls.Add(Me.Label92)
        Me.pnlSummary.Controls.Add(Me.Label93)
        Me.pnlSummary.Controls.Add(Me.Label94)
        Me.pnlSummary.Controls.Add(Me.txt_summary)
        Me.pnlSummary.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlSummary.Location = New System.Drawing.Point(0, 27)
        Me.pnlSummary.Name = "pnlSummary"
        Me.pnlSummary.Size = New System.Drawing.Size(613, 360)
        Me.pnlSummary.TabIndex = 1
        '
        'Label91
        '
        Me.Label91.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label91.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label91.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label91.Location = New System.Drawing.Point(1, 359)
        Me.Label91.Name = "Label91"
        Me.Label91.Size = New System.Drawing.Size(611, 1)
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
        Me.Label92.Size = New System.Drawing.Size(1, 359)
        Me.Label92.TabIndex = 11
        '
        'Label93
        '
        Me.Label93.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label93.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label93.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label93.Location = New System.Drawing.Point(612, 1)
        Me.Label93.Name = "Label93"
        Me.Label93.Size = New System.Drawing.Size(1, 359)
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
        Me.Label94.Size = New System.Drawing.Size(613, 1)
        Me.Label94.TabIndex = 9
        Me.Label94.Text = "label1"
        '
        'txt_summary
        '
        Me.txt_summary.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txt_summary.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txt_summary.ForeColor = System.Drawing.Color.Black
        Me.txt_summary.Location = New System.Drawing.Point(0, 0)
        Me.txt_summary.Multiline = True
        Me.txt_summary.Name = "txt_summary"
        Me.txt_summary.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txt_summary.Size = New System.Drawing.Size(613, 360)
        Me.txt_summary.TabIndex = 1
        '
        'pnlSummaryHeader
        '
        Me.pnlSummaryHeader.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlSummaryHeader.Controls.Add(Me.Panel1)
        Me.pnlSummaryHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlSummaryHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlSummaryHeader.Name = "pnlSummaryHeader"
        Me.pnlSummaryHeader.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.pnlSummaryHeader.Size = New System.Drawing.Size(613, 27)
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
        Me.Panel1.Size = New System.Drawing.Size(613, 24)
        Me.Panel1.TabIndex = 20
        '
        'Label26
        '
        Me.Label26.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label26.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label26.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label26.Location = New System.Drawing.Point(1, 23)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(611, 1)
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
        Me.Label5.Size = New System.Drawing.Size(611, 23)
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
        Me.Label87.Location = New System.Drawing.Point(612, 1)
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
        Me.Label88.Size = New System.Drawing.Size(613, 1)
        Me.Label88.TabIndex = 5
        Me.Label88.Text = "label1"
        '
        'sptLeft
        '
        Me.sptLeft.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.sptLeft.Location = New System.Drawing.Point(202, 0)
        Me.sptLeft.Name = "sptLeft"
        Me.sptLeft.Size = New System.Drawing.Size(3, 574)
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
        Me.pnlLeft.Controls.Add(Me.pnlbtnProblemlist)
        Me.pnlLeft.Controls.Add(Me.pnlbtnHistory)
        Me.pnlLeft.Controls.Add(Me.pnbtnDemographics)
        Me.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlLeft.Location = New System.Drawing.Point(0, 0)
        Me.pnlLeft.Name = "pnlLeft"
        Me.pnlLeft.Size = New System.Drawing.Size(202, 574)
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
        Me.Panel19.Location = New System.Drawing.Point(0, 270)
        Me.Panel19.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Panel19.Name = "Panel19"
        Me.Panel19.Padding = New System.Windows.Forms.Padding(3, 0, 0, 3)
        Me.Panel19.Size = New System.Drawing.Size(202, 304)
        Me.Panel19.TabIndex = 33
        '
        'Label174
        '
        Me.Label174.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label174.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label174.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label174.Location = New System.Drawing.Point(4, 300)
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
        Me.Label175.Size = New System.Drawing.Size(1, 300)
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
        Me.Label176.Size = New System.Drawing.Size(1, 300)
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
        Me.pnlbtnOrders.Location = New System.Drawing.Point(0, 240)
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
        Me.btnOrders.Text = "      Orders to be Given"
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
        Me.pnlbtnRadiology.Location = New System.Drawing.Point(0, 210)
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
        Me.pnlbtnLabs.Location = New System.Drawing.Point(0, 180)
        Me.pnlbtnLabs.Name = "pnlbtnLabs"
        Me.pnlbtnLabs.Padding = New System.Windows.Forms.Padding(3, 0, 0, 3)
        Me.pnlbtnLabs.Size = New System.Drawing.Size(202, 30)
        Me.pnlbtnLabs.TabIndex = 5
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
        Me.pnlbtnCPT.Location = New System.Drawing.Point(0, 150)
        Me.pnlbtnCPT.Name = "pnlbtnCPT"
        Me.pnlbtnCPT.Padding = New System.Windows.Forms.Padding(3, 0, 0, 3)
        Me.pnlbtnCPT.Size = New System.Drawing.Size(202, 30)
        Me.pnlbtnCPT.TabIndex = 4
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
        Me.pnlbtnICD9.Location = New System.Drawing.Point(0, 120)
        Me.pnlbtnICD9.Name = "pnlbtnICD9"
        Me.pnlbtnICD9.Padding = New System.Windows.Forms.Padding(3, 0, 0, 3)
        Me.pnlbtnICD9.Size = New System.Drawing.Size(202, 30)
        Me.pnlbtnICD9.TabIndex = 3
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
        Me.pnlbtnDrugs.Location = New System.Drawing.Point(0, 90)
        Me.pnlbtnDrugs.Name = "pnlbtnDrugs"
        Me.pnlbtnDrugs.Padding = New System.Windows.Forms.Padding(3, 0, 0, 3)
        Me.pnlbtnDrugs.Size = New System.Drawing.Size(202, 30)
        Me.pnlbtnDrugs.TabIndex = 2
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
        'pnlbtnProblemlist
        '
        Me.pnlbtnProblemlist.BackColor = System.Drawing.Color.Transparent
        Me.pnlbtnProblemlist.Controls.Add(Me.btnproblemlist)
        Me.pnlbtnProblemlist.Controls.Add(Me.Label162)
        Me.pnlbtnProblemlist.Controls.Add(Me.Label163)
        Me.pnlbtnProblemlist.Controls.Add(Me.Label164)
        Me.pnlbtnProblemlist.Controls.Add(Me.Label178)
        Me.pnlbtnProblemlist.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlbtnProblemlist.Location = New System.Drawing.Point(0, 60)
        Me.pnlbtnProblemlist.Name = "pnlbtnProblemlist"
        Me.pnlbtnProblemlist.Padding = New System.Windows.Forms.Padding(3, 0, 0, 3)
        Me.pnlbtnProblemlist.Size = New System.Drawing.Size(202, 30)
        Me.pnlbtnProblemlist.TabIndex = 35
        '
        'btnproblemlist
        '
        Me.btnproblemlist.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.btnproblemlist.BackgroundImage = CType(resources.GetObject("btnproblemlist.BackgroundImage"), System.Drawing.Image)
        Me.btnproblemlist.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnproblemlist.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnproblemlist.FlatAppearance.BorderSize = 0
        Me.btnproblemlist.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnproblemlist.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnproblemlist.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnproblemlist.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnproblemlist.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnproblemlist.Image = CType(resources.GetObject("btnproblemlist.Image"), System.Drawing.Image)
        Me.btnproblemlist.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnproblemlist.Location = New System.Drawing.Point(4, 1)
        Me.btnproblemlist.Name = "btnproblemlist"
        Me.btnproblemlist.Size = New System.Drawing.Size(197, 25)
        Me.btnproblemlist.TabIndex = 0
        Me.btnproblemlist.Tag = "UnSelected"
        Me.btnproblemlist.Text = "      Problem List"
        Me.btnproblemlist.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.btnproblemlist.UseVisualStyleBackColor = False
        '
        'Label162
        '
        Me.Label162.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label162.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label162.Location = New System.Drawing.Point(3, 1)
        Me.Label162.Name = "Label162"
        Me.Label162.Size = New System.Drawing.Size(1, 25)
        Me.Label162.TabIndex = 14
        '
        'Label163
        '
        Me.Label163.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label163.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label163.Location = New System.Drawing.Point(201, 1)
        Me.Label163.Name = "Label163"
        Me.Label163.Size = New System.Drawing.Size(1, 25)
        Me.Label163.TabIndex = 13
        '
        'Label164
        '
        Me.Label164.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label164.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label164.Location = New System.Drawing.Point(3, 26)
        Me.Label164.Name = "Label164"
        Me.Label164.Size = New System.Drawing.Size(199, 1)
        Me.Label164.TabIndex = 12
        '
        'Label178
        '
        Me.Label178.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label178.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label178.Location = New System.Drawing.Point(3, 0)
        Me.Label178.Name = "Label178"
        Me.Label178.Size = New System.Drawing.Size(199, 1)
        Me.Label178.TabIndex = 11
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
        Me.pnlbtnHistory.TabIndex = 1
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
        Me.pnbtnDemographics.TabIndex = 0
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
        'sptRight
        '
        Me.sptRight.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.sptRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.sptRight.Location = New System.Drawing.Point(821, 0)
        Me.sptRight.Name = "sptRight"
        Me.sptRight.Size = New System.Drawing.Size(1, 574)
        Me.sptRight.TabIndex = 3
        Me.sptRight.TabStop = False
        '
        'pnlRight
        '
        Me.pnlRight.Controls.Add(Me.GloUC_trvAssociates)
        Me.pnlRight.Controls.Add(Me.pnltrvTriggers)
        Me.pnlRight.Controls.Add(Me.pnltxtSearchOrder)
        Me.pnlRight.Controls.Add(Me.pnlbtnLab)
        Me.pnlRight.Controls.Add(Me.pnlbtnReferrals)
        Me.pnlRight.Controls.Add(Me.pnlbtnRx)
        Me.pnlRight.Controls.Add(Me.pnlbtnRadiologyTest)
        Me.pnlRight.Controls.Add(Me.pnlbtnGuideline)
        Me.pnlRight.Controls.Add(Me.pnlIM)
        Me.pnlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.pnlRight.Location = New System.Drawing.Point(822, 0)
        Me.pnlRight.Name = "pnlRight"
        Me.pnlRight.Size = New System.Drawing.Size(206, 574)
        Me.pnlRight.TabIndex = 333
        '
        'GloUC_trvAssociates
        '
        Me.GloUC_trvAssociates.BackColor = System.Drawing.Color.Transparent
        Me.GloUC_trvAssociates.CheckBoxes = False
        Me.GloUC_trvAssociates.CodeMember = Nothing
        Me.GloUC_trvAssociates.Comment = Nothing
        Me.GloUC_trvAssociates.ConceptID = Nothing
        Me.GloUC_trvAssociates.CPT = Nothing

        Me.GloUC_trvAssociates.DescriptionMember = Nothing
        Me.GloUC_trvAssociates.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Code
        Me.GloUC_trvAssociates.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GloUC_trvAssociates.DrugFlag = CType(16, Short)
        Me.GloUC_trvAssociates.DrugFormMember = Nothing
        Me.GloUC_trvAssociates.DrugQtyQualifierMember = Nothing
        Me.GloUC_trvAssociates.DurationMember = Nothing
        Me.GloUC_trvAssociates.FrequencyMember = Nothing
        Me.GloUC_trvAssociates.HistoryType = Nothing
        Me.GloUC_trvAssociates.ICD9 = Nothing
        Me.GloUC_trvAssociates.ImageIndex = 0
        Me.GloUC_trvAssociates.ImageList = Me.ImageList1
        Me.GloUC_trvAssociates.ImageObject = Nothing
        Me.GloUC_trvAssociates.Indicator = Nothing
        Me.GloUC_trvAssociates.IsDrug = False
        Me.GloUC_trvAssociates.IsNarcoticsMember = Nothing
        Me.GloUC_trvAssociates.IsSystemCategory = Nothing
        Me.GloUC_trvAssociates.Location = New System.Drawing.Point(0, 30)
        Me.GloUC_trvAssociates.MaximumNodes = 1000
        Me.GloUC_trvAssociates.Name = "GloUC_trvAssociates"
        Me.GloUC_trvAssociates.NDCCodeMember = Nothing
        Me.GloUC_trvAssociates.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.GloUC_trvAssociates.ParentImageIndex = 0
        Me.GloUC_trvAssociates.ParentMember = Nothing
        Me.GloUC_trvAssociates.RouteMember = Nothing
        Me.GloUC_trvAssociates.Search = gloUserControlLibrary.gloUC_TreeView.enumSearchType.Instring
        Me.GloUC_trvAssociates.SearchBox = True
        Me.GloUC_trvAssociates.SearchText = Nothing
        Me.GloUC_trvAssociates.SelectedImageIndex = 0
        Me.GloUC_trvAssociates.SelectedNode = Nothing
        Me.GloUC_trvAssociates.SelectedNodeIDs = CType(resources.GetObject("GloUC_trvAssociates.SelectedNodeIDs"), System.Collections.ArrayList)
        Me.GloUC_trvAssociates.SelectedParentImageIndex = 0
        Me.GloUC_trvAssociates.Size = New System.Drawing.Size(206, 394)
        Me.GloUC_trvAssociates.Sort = gloUserControlLibrary.gloUC_TreeView.enumSortType.ByDescription
        Me.GloUC_trvAssociates.TabIndex = 25
        Me.GloUC_trvAssociates.Tag = Nothing
        Me.GloUC_trvAssociates.UnitMember = Nothing
        Me.GloUC_trvAssociates.ValueMember = Nothing
        '
        'pnltrvTriggers
        '
        Me.pnltrvTriggers.BackColor = System.Drawing.Color.Transparent
        Me.pnltrvTriggers.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnltrvTriggers.Controls.Add(Me.Label16)
        Me.pnltrvTriggers.Controls.Add(Me.Label74)
        Me.pnltrvTriggers.Controls.Add(Me.Label75)
        Me.pnltrvTriggers.Controls.Add(Me.Label76)
        Me.pnltrvTriggers.Controls.Add(Me.Label77)
        Me.pnltrvTriggers.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnltrvTriggers.Location = New System.Drawing.Point(0, 56)
        Me.pnltrvTriggers.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.pnltrvTriggers.Name = "pnltrvTriggers"
        Me.pnltrvTriggers.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.pnltrvTriggers.Size = New System.Drawing.Size(206, 400)
        Me.pnltrvTriggers.TabIndex = 23
        Me.pnltrvTriggers.Visible = False
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
        Me.pnltxtSearchOrder.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnltxtSearchOrder.ForeColor = System.Drawing.Color.Black
        Me.pnltxtSearchOrder.Location = New System.Drawing.Point(0, 30)
        Me.pnltxtSearchOrder.Name = "pnltxtSearchOrder"
        Me.pnltxtSearchOrder.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.pnltxtSearchOrder.Size = New System.Drawing.Size(206, 26)
        Me.pnltxtSearchOrder.TabIndex = 16
        Me.pnltxtSearchOrder.Visible = False
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
        Me.btnLab.Text = "Labs"
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
        Me.pnlbtnReferrals.Location = New System.Drawing.Point(0, 424)
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
        Me.btnReferrals.Text = "Referrals"
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
        Me.pnlbtnRx.Location = New System.Drawing.Point(0, 454)
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
        Me.btnRx.Text = "Rx"
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
        Me.pnlbtnRadiologyTest.Location = New System.Drawing.Point(0, 484)
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
        Me.btnRadiologyTest.Text = "Orders"
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
        Me.pnlbtnGuideline.Location = New System.Drawing.Point(0, 514)
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
        Me.btnGuideline.Text = "Guidelines"
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
        'pnlIM
        '
        Me.pnlIM.BackColor = System.Drawing.Color.Transparent
        Me.pnlIM.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlIM.Controls.Add(Me.btnIM)
        Me.pnlIM.Controls.Add(Me.Label145)
        Me.pnlIM.Controls.Add(Me.Label159)
        Me.pnlIM.Controls.Add(Me.Label160)
        Me.pnlIM.Controls.Add(Me.Label161)
        Me.pnlIM.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlIM.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlIM.Location = New System.Drawing.Point(0, 544)
        Me.pnlIM.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.pnlIM.Name = "pnlIM"
        Me.pnlIM.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.pnlIM.Size = New System.Drawing.Size(206, 30)
        Me.pnlIM.TabIndex = 27
        '
        'btnIM
        '
        Me.btnIM.BackgroundImage = CType(resources.GetObject("btnIM.BackgroundImage"), System.Drawing.Image)
        Me.btnIM.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnIM.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnIM.FlatAppearance.BorderSize = 0
        Me.btnIM.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnIM.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnIM.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnIM.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnIM.Location = New System.Drawing.Point(1, 1)
        Me.btnIM.Name = "btnIM"
        Me.btnIM.Size = New System.Drawing.Size(201, 25)
        Me.btnIM.TabIndex = 1
        Me.btnIM.Tag = "UnSelected"
        Me.btnIM.Text = "IM"
        Me.btnIM.UseVisualStyleBackColor = True
        '
        'Label145
        '
        Me.Label145.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label145.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label145.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label145.Location = New System.Drawing.Point(1, 26)
        Me.Label145.Name = "Label145"
        Me.Label145.Size = New System.Drawing.Size(201, 1)
        Me.Label145.TabIndex = 8
        Me.Label145.Text = "label2"
        '
        'Label159
        '
        Me.Label159.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label159.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label159.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label159.Location = New System.Drawing.Point(0, 1)
        Me.Label159.Name = "Label159"
        Me.Label159.Size = New System.Drawing.Size(1, 26)
        Me.Label159.TabIndex = 7
        Me.Label159.Text = "label4"
        '
        'Label160
        '
        Me.Label160.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label160.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label160.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label160.Location = New System.Drawing.Point(202, 1)
        Me.Label160.Name = "Label160"
        Me.Label160.Size = New System.Drawing.Size(1, 26)
        Me.Label160.TabIndex = 6
        Me.Label160.Text = "label3"
        '
        'Label161
        '
        Me.Label161.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label161.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label161.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label161.Location = New System.Drawing.Point(0, 0)
        Me.Label161.Name = "Label161"
        Me.Label161.Size = New System.Drawing.Size(203, 1)
        Me.Label161.TabIndex = 5
        Me.Label161.Text = "label1"
        '
        'pnlMsgTOP
        '
        Me.pnlMsgTOP.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlMsgTOP.Controls.Add(Me.pnlMsg)
        Me.pnlMsgTOP.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlMsgTOP.Location = New System.Drawing.Point(0, 54)
        Me.pnlMsgTOP.Name = "pnlMsgTOP"
        Me.pnlMsgTOP.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlMsgTOP.Size = New System.Drawing.Size(1028, 40)
        Me.pnlMsgTOP.TabIndex = 0
        '
        'pnlMsg
        '
        Me.pnlMsg.BackColor = System.Drawing.Color.Transparent
        Me.pnlMsg.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlMsg.Controls.Add(Me.Label141)
        Me.pnlMsg.Controls.Add(Me.Label4)
        Me.pnlMsg.Controls.Add(Me.Label118)
        Me.pnlMsg.Controls.Add(Me.txtName)
        Me.pnlMsg.Controls.Add(Me.txtMessage)
        Me.pnlMsg.Controls.Add(Me.Label3)
        Me.pnlMsg.Controls.Add(Me.Label23)
        Me.pnlMsg.Controls.Add(Me.Label24)
        Me.pnlMsg.Controls.Add(Me.Label63)
        Me.pnlMsg.Controls.Add(Me.Label25)
        Me.pnlMsg.Controls.Add(Me.Label117)
        Me.pnlMsg.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMsg.Location = New System.Drawing.Point(3, 3)
        Me.pnlMsg.Name = "pnlMsg"
        Me.pnlMsg.Size = New System.Drawing.Size(1022, 34)
        Me.pnlMsg.TabIndex = 0
        '
        'Label141
        '
        Me.Label141.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label141.AutoSize = True
        Me.Label141.ForeColor = System.Drawing.Color.Red
        Me.Label141.Location = New System.Drawing.Point(445, 10)
        Me.Label141.Name = "Label141"
        Me.Label141.Size = New System.Drawing.Size(14, 14)
        Me.Label141.TabIndex = 24
        Me.Label141.Text = "*"
        '
        'Label4
        '
        Me.Label4.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(448, 10)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(79, 14)
        Me.Label4.TabIndex = 13
        Me.Label4.Text = "   Message :"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label118
        '
        Me.Label118.AutoSize = True
        Me.Label118.ForeColor = System.Drawing.Color.Red
        Me.Label118.Location = New System.Drawing.Point(14, 10)
        Me.Label118.Name = "Label118"
        Me.Label118.Size = New System.Drawing.Size(14, 14)
        Me.Label118.TabIndex = 22
        Me.Label118.Text = "*"
        '
        'txtName
        '
        Me.txtName.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtName.ForeColor = System.Drawing.Color.Black
        Me.txtName.Location = New System.Drawing.Point(80, 6)
        Me.txtName.MaxLength = 50
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(321, 22)
        Me.txtName.TabIndex = 0
        '
        'txtMessage
        '
        Me.txtMessage.Dock = System.Windows.Forms.DockStyle.Right
        Me.txtMessage.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMessage.ForeColor = System.Drawing.Color.Black
        Me.txtMessage.Location = New System.Drawing.Point(531, 1)
        Me.txtMessage.MaxLength = 255
        Me.txtMessage.Multiline = True
        Me.txtMessage.Name = "txtMessage"
        Me.txtMessage.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtMessage.Size = New System.Drawing.Size(490, 32)
        Me.txtMessage.TabIndex = 1
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(21, 10)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(56, 14)
        Me.Label3.TabIndex = 11
        Me.Label3.Text = "  Name :"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label23
        '
        Me.Label23.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label23.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label23.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label23.Location = New System.Drawing.Point(1, 33)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(1020, 1)
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
        Me.Label24.Size = New System.Drawing.Size(1, 33)
        Me.Label24.TabIndex = 16
        Me.Label24.Text = "label4"
        '
        'Label63
        '
        Me.Label63.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label63.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label63.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label63.Location = New System.Drawing.Point(1021, 1)
        Me.Label63.Name = "Label63"
        Me.Label63.Size = New System.Drawing.Size(1, 33)
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
        Me.Label25.Size = New System.Drawing.Size(1022, 1)
        Me.Label25.TabIndex = 21
        Me.Label25.Text = "label1"
        '
        'Label117
        '
        Me.Label117.AutoSize = True
        Me.Label117.ForeColor = System.Drawing.Color.Red
        Me.Label117.Location = New System.Drawing.Point(283, 8)
        Me.Label117.Name = "Label117"
        Me.Label117.Size = New System.Drawing.Size(14, 14)
        Me.Label117.TabIndex = 1
        Me.Label117.Text = "*"
        '
        'pnl_tlstrip
        '
        Me.pnl_tlstrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnl_tlstrip.Controls.Add(Me.tlsDM)
        Me.pnl_tlstrip.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_tlstrip.Location = New System.Drawing.Point(0, 0)
        Me.pnl_tlstrip.Name = "pnl_tlstrip"
        Me.pnl_tlstrip.Size = New System.Drawing.Size(1028, 54)
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
        Me.tlsDM.Size = New System.Drawing.Size(1028, 53)
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
        'cntFindingsprb
        '
        Me.cntFindingsprb.Name = "cntFindingsprb"
        Me.cntFindingsprb.Size = New System.Drawing.Size(61, 4)
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuDeleteDrugs})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(148, 26)
        '
        'mnuDeleteDrugs
        '
        Me.mnuDeleteDrugs.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.mnuDeleteDrugs.Image = CType(resources.GetObject("mnuDeleteDrugs.Image"), System.Drawing.Image)
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
        Me.mnuDeleteHistory.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.mnuDeleteHistory.Image = CType(resources.GetObject("mnuDeleteHistory.Image"), System.Drawing.Image)
        Me.mnuDeleteHistory.Name = "mnuDeleteHistory"
        Me.mnuDeleteHistory.Size = New System.Drawing.Size(153, 22)
        Me.mnuDeleteHistory.Text = "Delete History"
        '
        'CmnuStripCPT
        '
        Me.CmnuStripCPT.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuItem_DeleteCPT})
        Me.CmnuStripCPT.Name = "ContextMenuStrip1"
        Me.CmnuStripCPT.Size = New System.Drawing.Size(139, 26)
        '
        'mnuItem_DeleteCPT
        '
        Me.mnuItem_DeleteCPT.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.mnuItem_DeleteCPT.Image = CType(resources.GetObject("mnuItem_DeleteCPT.Image"), System.Drawing.Image)
        Me.mnuItem_DeleteCPT.Name = "mnuItem_DeleteCPT"
        Me.mnuItem_DeleteCPT.Size = New System.Drawing.Size(138, 22)
        Me.mnuItem_DeleteCPT.Text = "Delete CPT"
        '
        'CmnustripICD
        '
        Me.CmnustripICD.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuItem_DeleteICD})
        Me.CmnustripICD.Name = "ContextMenuStrip1"
        Me.CmnustripICD.Size = New System.Drawing.Size(138, 26)
        '
        'mnuItem_DeleteICD
        '
        Me.mnuItem_DeleteICD.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.mnuItem_DeleteICD.Image = CType(resources.GetObject("mnuItem_DeleteICD.Image"), System.Drawing.Image)
        Me.mnuItem_DeleteICD.Name = "mnuItem_DeleteICD"
        Me.mnuItem_DeleteICD.Size = New System.Drawing.Size(137, 22)
        Me.mnuItem_DeleteICD.Text = "Delete ICD"
        '
        'cntFindings
        '
        Me.cntFindings.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnu_AddFindings})
        Me.cntFindings.Name = "cntFindings"
        Me.cntFindings.Size = New System.Drawing.Size(147, 26)
        '
        'mnu_AddFindings
        '
        Me.mnu_AddFindings.Name = "mnu_AddFindings"
        Me.mnu_AddFindings.Size = New System.Drawing.Size(146, 22)
        Me.mnu_AddFindings.Text = "Add Findings"
        '
        'ContextMenuProblem
        '
        Me.ContextMenuProblem.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuDeleteProblem})
        Me.ContextMenuProblem.Name = "ContextMenuStrip1"
        Me.ContextMenuProblem.Size = New System.Drawing.Size(158, 26)
        '
        'mnuDeleteProblem
        '
        Me.mnuDeleteProblem.Name = "mnuDeleteProblem"
        Me.mnuDeleteProblem.Size = New System.Drawing.Size(157, 22)
        Me.mnuDeleteProblem.Text = "Delete Problem"
        Me.mnuDeleteProblem.ToolTipText = "Delete Problem"
        '
        'frmDM_Setup
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(1028, 668)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.pnlMsgTOP)
        Me.Controls.Add(Me.pnl_tlstrip)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmDM_Setup"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Disease Management"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
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
        Me.PnlProblemList.ResumeLayout(False)
        Me.PnlProblemMiddle.ResumeLayout(False)
        Me.Pnlsnomedprb.ResumeLayout(False)
        Me.pnltrvSnowmedOff.ResumeLayout(False)
        Me.Panel24.ResumeLayout(False)
        Me.pnltrvfinprob.ResumeLayout(False)
        Me.Panel31.ResumeLayout(False)
        Me.PnlSrchProb.ResumeLayout(False)
        Me.PnlSrchProb.PerformLayout()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnltrvsubprb.ResumeLayout(False)
        Me.Panel34.ResumeLayout(False)
        Me.PnlProblemSearch.ResumeLayout(False)
        Me.Panel26.ResumeLayout(False)
        Me.PnlProbLeft.ResumeLayout(False)
        Me.Panel35.ResumeLayout(False)
        Me.Panel36.ResumeLayout(False)
        Me.Panel37.ResumeLayout(False)
        Me.Panel38.ResumeLayout(False)
        Me.Panel39.ResumeLayout(False)
        Me.Panel39.PerformLayout()
        Me.pnlRadiology.ResumeLayout(False)
        Me.Panel18.ResumeLayout(False)
        CType(Me.c1Labs, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel17.ResumeLayout(False)
        Me.Panel17.PerformLayout()
        CType(Me.PictureBox6, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlHistory.ResumeLayout(False)
        Me.Panel22.ResumeLayout(False)
        Me.Panel20.ResumeLayout(False)
        Me.Panel21.ResumeLayout(False)
        Me.Panel16.ResumeLayout(False)
        Me.Panel9.ResumeLayout(False)
        Me.pnlHistoryLeft.ResumeLayout(False)
        Me.Panel11.ResumeLayout(False)
        Me.Panel8.ResumeLayout(False)
        Me.Panel8.PerformLayout()
        Me.pnlICD9.ResumeLayout(False)
        Me.Panel15.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        Me.Panel10.ResumeLayout(False)
        Me.pnlCPT.ResumeLayout(False)
        Me.pnlSelectedCPTs.ResumeLayout(False)
        Me.pnlSelecteCPTsLabels.ResumeLayout(False)
        Me.Panel28.ResumeLayout(False)
        Me.pnlDrugs.ResumeLayout(False)
        Me.pnltrvSelectedDrugs.ResumeLayout(False)
        Me.pnlSelectedDrugLabel.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.pnlLab.ResumeLayout(False)
        Me.Panel12.ResumeLayout(False)
        CType(Me.C1LabResult, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel13.ResumeLayout(False)
        Me.Panel23.ResumeLayout(False)
        Me.Panel23.PerformLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlSummaryOthers.ResumeLayout(False)
        Me.pnlGuideline.ResumeLayout(False)
        Me.pnlGuidelineHeader.ResumeLayout(False)
        Me.pnl3.ResumeLayout(False)
        Me.pnlSummary.ResumeLayout(False)
        Me.pnlSummary.PerformLayout()
        Me.pnlSummaryHeader.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.pnlLeft.ResumeLayout(False)
        Me.Panel19.ResumeLayout(False)
        Me.pnlbtnOrders.ResumeLayout(False)
        Me.pnlbtnRadiology.ResumeLayout(False)
        Me.pnlbtnLabs.ResumeLayout(False)
        Me.pnlbtnCPT.ResumeLayout(False)
        Me.pnlbtnICD9.ResumeLayout(False)
        Me.pnlbtnDrugs.ResumeLayout(False)
        Me.pnlbtnProblemlist.ResumeLayout(False)
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
        Me.pnlIM.ResumeLayout(False)
        Me.pnlMsgTOP.ResumeLayout(False)
        Me.pnlMsg.ResumeLayout(False)
        Me.pnlMsg.PerformLayout()
        Me.pnl_tlstrip.ResumeLayout(False)
        Me.pnl_tlstrip.PerformLayout()
        Me.tlsDM.ResumeLayout(False)
        Me.tlsDM.PerformLayout()
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.ContextMenuHistory.ResumeLayout(False)
        Me.CmnuStripCPT.ResumeLayout(False)
        Me.CmnustripICD.ResumeLayout(False)
        Me.cntFindings.ResumeLayout(False)
        Me.ContextMenuProblem.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub btnHistory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHistory.Click
        Try
            'This code is commented by shilpa because currently we are not using tab structure in form
            ''function for display history Tag of the Tag Pages and hide others tags pages
            ''HidenShow(tpHistory)
            FillAllCriteria()
            trvselectedhist.Visible = True
            trvselectedprob.Visible = False
            trvselectedhist.BringToFront()
            txtsrchprb.Text = ""

            trvfinprob.Nodes.Clear()
            trvsubprb.Nodes.Clear()
            cmbhistsnomed.Visible = True
            lblsnohistcat.Visible = True
            sptRight.Visible = False
            pnlRight.Visible = False
            pnlRight.SendToBack()
            Label230.Text = "     Selected History"
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

            btnproblemlist.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
            btnproblemlist.BackgroundImageLayout = ImageLayout.Stretch
            btnproblemlist.Tag = "UnSelected"
            ''Commenetd by Mayuri:20110323-We are not pulling data from snomed in transactions
            'If gblnSMDBSetting = True And gstrSMHistory.Trim() <> "" Then '' for history if snomed setting is on 
            '    Label230.Image = Global.gloEMR.My.Resources.History2.ToBitmap()
            '    PnlProblemList.BringToFront()
            '    pnltrvSnowmedOff.Visible = False
            '    pnltrvfinprob.Visible = True
            '    Splitter6.Visible = True
            '    pnltrvsubprb.Visible = True

            '    PnlProblemList.Visible = True
            '    PnlProblemList.BringToFront()
            '    Dim dtHistoryCategory As DataTable
            '    dtHistoryCategory = GetHistoryCategory()
            '    cmbhistsnomed.DataSource = dtHistoryCategory
            '    cmbhistsnomed.DisplayMember = dtHistoryCategory.Columns("sDescription").ColumnName
            '    cmbhistsnomed.ValueMember = dtHistoryCategory.Columns("nCategoryID").ColumnName
            '    trvselectedhist.Visible = True
            '    trvselectedprob.Visible = False
            '    blnhistory = True
            'Else
            Fill_Histories_1()
            If cmbHistoryCategory.Items.Count >= 1 Then
                cmbHistoryCategory.SelectedIndex = 0
            End If
            cmbHistoryCategory_SelectionChangeCommitted(Nothing, Nothing)
            'End If



        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub btnDemographics_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDemographics.Click
        Try
            'This code is commented by shilpa because currently we are not using tab structure in form
            ''function for display Demographic Tag of the Tag Pages and hide others tags pages
            'HidenShow(tpDemographics)
            sptRight.Visible = False
            pnlRight.Visible = False
            pnlRight.SendToBack()

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


            btnproblemlist.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
            btnproblemlist.BackgroundImageLayout = ImageLayout.Stretch
            btnproblemlist.Tag = "UnSelected"

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''This functions was used for tab sturcture hide & show currently we are not using tab strucutre
    'Private Sub HidenShow(ByVal tpName As TabPage)
    '    ' passing the tabpage Name
    '    For i As Int16 = tcDiseaseSetup.TabPages.Count - 1 To 0 Step -1
    '        tcDiseaseSetup.TabPages.RemoveAt(i)
    '        'remove allthe tag pages
    '    Next
    '    ' add only tag page that comes from Passing parameters.
    '    tcDiseaseSetup.TabPages.Add(tpName)
    '    ' select the pass tab page
    '    tcDiseaseSetup.SelectedTab = tpName
    'End Sub

    Private Sub frmDiseaseManagement_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        txtName.Select()
        gloC1FlexStyle.Style(c1Labs)
        gloC1FlexStyle.Style(C1LabResult)

        btnDemographics.BackgroundImage = Global.gloEMR.My.Resources.Img_LongOrange
        btnDemographics.BackgroundImageLayout = ImageLayout.Stretch
        btnDemographics.Tag = "Selected"
        ''send the right panel ,the panel for associates to back 


        sptRight.Visible = False
        pnlRight.Visible = False
        pnlRight.SendToBack()
        sptRight.SendToBack()

        Try
            'function for display History Tag of the Tag Pages and hide others tags pages
            ' HidenShow(tpDemographics)
            ' fill all teh tree view of the all tag pages
            ' fill all tree views

            '' Fill_ICD9s()  '' commented by Sandip Darade 20090415
            '' replaced by 'Fill_ICD9s(strSortByDesc)'
            ' Sandip Darade Fill_ICD9s(strSortByDesc)
            ' Sandip Darade fill_Drugs()
            ''fill_CPTs() ''commented by Sandip Darade 20090415
            '' replaced by 'fill_CPTs(strSortByDesc)'
            ' Sandip Darade  fill_CPTs(strSortByDesc)
            ' fill data of Demographics
            Fill_Age()
            fill_Maritalst()
            fill_gender()
            fill_race()
            fill_state()
            fill_EmpState()




            '' chetan commented for performance issue on dec 06 2010
            '   fill_labs()
            '  Fill_RadiologyLabsC1()
            '  Fill_OtherInfo()
            ''''Add By Pramod For CCHIT 2007
            '  PopulateAssocaitedInfo(1)
            PopulateAssocaitedInfo(1)
            ' ''
            '' chetan added for problem 09 Oct 2010
            ' fillProblemlistTreeHeader()

            ' If gblnSMDBSetting = False Or gstrSMProblem.Trim() = "" Then
            'fillProblemlistSnomadeoff()
            'End If


            '' chetan commented for performance issue on dec 06 2010

            If m_CriteriaId <> 0 Then
                '  Fill_EditCriteria(m_CriteriaId) chetan commented for performance issue on dec 06 2010
                Fill_EditCriteriaForVitalDemogr(m_CriteriaId)
            End If










        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        txt_summary.ReadOnly = True
        txt_summary.BackColor = Color.White
    End Sub

    Public Sub Fill_EditCriteriaForVitalDemogr(ByVal lCriteriaID As Long)
        Try
            Dim oCriteria As gloStream.DiseaseManagement.Supporting.Criteria
            Dim oDM As New gloStream.DiseaseManagement.DiseaseManagement

            oCriteria = oDM.GetCriteria(lCriteriaID, 0)

            If Not oCriteria Is Nothing Then
                With oCriteria
                    'Demographics & Vitals
                    txtName.Text = .Name
                    txtMessage.Text = .DisplayMessage

                    '.AgeMinimum.ToString("#0.00")
                    If .AgeMinimum.ToString.Contains(".") Then
                        Dim _age() As String = .AgeMinimum.ToString.Split(".")
                        cmbAgeMin.Text = _age(0) : SetCombiIndex(cmbAgeMin)

                        ''GLO2012-0016324 : DM Setup 
                        ''as concatinating the _age(1) with the 4 "0" so as to retrive 4 digit value
                        _age(1) += "0000"
                        _age(1) = _age(1).Substring(0, 4)

                        cmbAgeMinMnth.Text = Format(CDbl((_age(1) / 10000) * 12), "#00") : SetCombiIndex(cmbAgeMinMnth)
                    Else
                        cmbAgeMin.Text = .AgeMinimum : SetCombiIndex(cmbAgeMin)
                        cmbAgeMinMnth.Text = "00" : SetCombiIndex(cmbAgeMinMnth)
                    End If
                    If .AgeMaximum.ToString.Contains(".") Then
                        Dim _age() As String = .AgeMaximum.ToString.Split(".")
                        cmbAgeMax.Text = _age(0) : SetCombiIndex(cmbAgeMax)

                        ''GLO2012-0016324 : DM Setup 
                        ''as concatinating the _age(1) with the 4 "0" so as to retrive 4 digit value
                        _age(1) += "0000"
                        _age(1) = _age(1).Substring(0, 4)

                        cmbAgeMaxMnth.Text = Format(CDbl((_age(1) / 10000) * 12), "#00") : SetCombiIndex(cmbAgeMaxMnth)
                    Else
                        cmbAgeMax.Text = .AgeMaximum : SetCombiIndex(cmbAgeMax)
                        cmbAgeMaxMnth.Text = "00" : SetCombiIndex(cmbAgeMaxMnth)
                    End If


                    txtCity.Text = .City
                    cmbGender.Text = .Gender : SetCombiIndex(cmbGender)
                    cmbState.Text = .State : SetCombiIndex(cmbState)
                    cmbRace.Text = .Race : SetCombiIndex(cmbRace)
                    txtZip.Text = .Zip
                    cmbMaritalSt.Text = .MaritalStatus : SetCombiIndex(cmbMaritalSt)
                    cmbEmpStatus.Text = .EmployeeStatus : SetCombiIndex(cmbEmpStatus)
                    '  txtHeightMin.Text = .HeightMinimum
                    '  txtHeightMax.Text = .HeightMaximum

                    ' changes done by Bipin On 22/01/2007 Date format change into ft and inch.
                    Dim arrHeight() As String
                    arrHeight = GetFtInch(.HeightMinimum)
                    txtHeightMin.Text = arrHeight(0)
                    txtHeightMinInch.Text = arrHeight(1)

                    '  Dim arrHeightMax() As String
                    arrHeight = GetFtInch(.HeightMaximum)
                    txtHeightMax.Text = arrHeight(0)
                    txtHeightMaxInch.Text = arrHeight(1)

                    If .BPSittingMinimum = 0.0 Then
                        txtBPsettingMin.Text = ""
                    Else
                        txtBPsettingMin.Text = .BPSittingMinimum
                    End If

                    If .BPSittingMaximum = 0.0 Then
                        txtBPsettingMax.Text = ""
                    Else
                        txtBPsettingMax.Text = .BPSittingMaximum
                    End If

                    If .WeightMinimum = 0.0 Then
                        txtWeightMin.Text = ""
                    Else
                        txtWeightMin.Text = .WeightMinimum
                    End If

                    If .WeightMaximum = 0.0 Then
                        txtWeightMax.Text = ""
                    Else
                        txtWeightMax.Text = .WeightMaximum
                    End If

                    If .BPStandingMinimum = 0.0 Then
                        txtBPstandingMin.Text = ""
                    Else
                        txtBPstandingMin.Text = .BPStandingMinimum
                    End If

                    If .BPStandingMaximum = 0.0 Then
                        txtBPstandingMax.Text = ""
                    Else
                        txtBPstandingMax.Text = .BPStandingMaximum
                    End If

                    If .TempratureMinumum = 0.0 Then
                        txtTemperatureMin.Text = ""
                    Else
                        txtTemperatureMin.Text = .TempratureMinumum
                    End If

                    If .TempratureMaximum = 0.0 Then
                        txtTemperatureMax.Text = ""
                    Else
                        txtTemperatureMax.Text = .TempratureMaximum
                    End If

                    If .PulseMinimum = 0.0 Then
                        txtPulseMin.Text = ""
                    Else
                        txtPulseMin.Text = .PulseMinimum
                    End If

                    If .PulseMaximum = 0.0 Then
                        txtPulseMax.Text = ""
                    Else
                        txtPulseMax.Text = .PulseMaximum
                    End If
                    If .BMIMinimum = 0.0 Then
                        txtBMImin.Text = ""
                    Else
                        txtBMImin.Text = .BMIMinimum
                    End If

                    If .BMIMaximum = 0.0 Then
                        txtBMImax.Text = ""
                    Else
                        txtBMImax.Text = .BMIMaximum
                    End If

                    If .PulseOXMinimum = 0.0 Then
                        txtPulseOXmin.Text = ""
                    Else
                        txtPulseOXmin.Text = .PulseOXMinimum
                    End If

                    If .PulseOXMaximum = 0.0 Then
                        txtPulseOXmax.Text = ""
                    Else
                        txtPulseOXmax.Text = .PulseOXMaximum
                    End If
                End With
                oCriteria.Dispose()
            End If

            oCriteria = Nothing
            oDM.Dispose()
            oDM = Nothing
        Catch
        End Try
    End Sub


    Private Sub fillProblemlistSnomadeoff(Optional ByVal strsearch As String = "")
        Dim oDM As New gloStream.DiseaseManagement.Common.Criteria

        Dim dtProblemlist As DataTable = Nothing
        dtProblemlist = oDM.GetProblemList(strsearch)

        If Not dtProblemlist Is Nothing Then
            trvSnowmedOff.Nodes.Clear()
            For Each dr As DataRow In dtProblemlist.Rows
                trvSnowmedOff.Nodes.Add(dr(0))
            Next
        End If
        oDM.Dispose()
        oDM = Nothing
    End Sub
    ''' <summary>
    ''' Fill Lab TreeView 
    ''' </summary>
    ''' <remarks></remarks>
    ''' 

    Public Sub FillLabTest()
        ''''Create object for the class
        Dim oDM As New gloStream.DiseaseManagement.Common.Criteria
        '  Dim oLabsModule As New gloStream.DiseaseManagement.Supporting.LabModuleTests

        '''''assign Lab Test and Result to collection
        'oLabsModule = oDM.LabModuleTests

        'If Not oLabsModule Is Nothing Then
        '    If oLabsModule.Count > 0 Then

        '        'Fill Test
        '        For _C = 1 To oLabsModule.Count

        '            rootnode = New myTreeNode(oLabsModule(_C).Name, oLabsModule(_C).TestID)


        '            rootnode.ImageIndex = 11
        '            rootnode.SelectedImageIndex = 11
        '            trvTriggers.Nodes.Item(0).Nodes.Add(rootnode)

        '            'For _G = 1 To oLabsModule.Item(_C).LabModuleTestResults.Count
        '            '    'trAssociates.Nodes.Item(1).Nodes.Add(New myTreeNode(oLabsModule.Item(_C).LabModuleTestResults(_G).ResultName, oLabsModule.Item(_C).LabModuleTestResults(_G).ResultID))
        '            '    Dim mychildnode As myTreeNode
        '            '    mychildnode = New myTreeNode(oLabsModule.Item(_C).LabModuleTestResults(_G).ResultName, oLabsModule.Item(_C).LabModuleTestResults(_G).ResultID)
        '            '    'trAssociates.Nodes.Item(0).Nodes.Add(mychildnode)
        '            '    rootnode.Nodes.Add(mychildnode)

        '            'Next
        '        Next
        '        trvTriggers.ExpandAll()
        '    End If

        'End If

        Dim dtLabsModule As DataTable
        dtLabsModule = oDM.LabModuleTest
        GloUC_trvAssociates.Clear()
        If Not dtLabsModule Is Nothing Then
            GloUC_trvAssociates.DataSource = dtLabsModule
            GloUC_trvAssociates.ValueMember = dtLabsModule.Columns(1).ColumnName
            GloUC_trvAssociates.DescriptionMember = dtLabsModule.Columns(0).ColumnName
            GloUC_trvAssociates.CodeMember = dtLabsModule.Columns(0).ColumnName
            GloUC_trvAssociates.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Code
            GloUC_trvAssociates.Search = gloUserControlLibrary.gloUC_TreeView.enumSearchType.Instring
            GloUC_trvAssociates.FillTreeView()
        End If
        oDM.Dispose()
        oDM = Nothing
        'oLabsModule = Nothing
    End Sub

    ''' <summary>
    ''' Populate the Data as per button selection
    ''' </summary>
    ''' <param name="ID">ID Specify which button is press for Populating value </param>
    ''' <remarks>Pramod</remarks>
    ''' 

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

        '''''''''Added by Ujwala Atre as on 20100907 - for IM in DM Setup
        pnlIM.Dock = DockStyle.Bottom
        btnIM.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
        btnIM.BackgroundImageLayout = ImageLayout.Stretch
        btnIM.Tag = "UnSelected"
        '''''''''Added by Ujwala Atre as on 20100907 - for IM in DM Setup



        If ID = 1 Then
            With btnLab
                pnlbtnLab.Dock = DockStyle.Top
                .Tag = "Selected"
                '.ForeColor = Color.Black
                .BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongOrange
                .BackgroundImageLayout = ImageLayout.Stretch
                .BringToFront()
            End With
            'trvTriggers.BringToFront()
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
            '  trvTriggers.BringToFront()
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
            ' trvTriggers.BringToFront()
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
            '  trvTriggers.BringToFront()
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
            ' trvTriggers.BringToFront()
            fill_guideline()

        ElseIf ID = 6 Then
            With btnIM
                pnlIM.Dock = DockStyle.Top
                .Tag = "Selected"
                .BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongOrange
                .BackgroundImageLayout = ImageLayout.Stretch
                .BringToFront()
            End With
            fill_IM()
            '''''''''Added by Ujwala Atre as on 20100907 - for IM in DM Setup
        End If

    End Sub




    Private Sub fill_IM()
        '''''''''Integrated by Chetan as on 11 Oct 2010- for IM in DM Setup

        Dim oDB As New gloStream.gloDataBase.gloDataBase
        Dim dt As New DataTable
        Dim Query As String = ""

        Try
            Dim rootnode As myTreeNode = Nothing
            rootnode = New myTreeNode("IM", -1)
            rootnode.ImageIndex = 12
            rootnode.SelectedImageIndex = 12

            '''''''''''''
            oDB.Connect(GetConnectionString)
            '''''''''''''
            ''Modfied by Mayuri:20120202-Immnuization new changes
            Query = "SELECT im.im_item_id,dbo.IM_SeparateCodeAndDescription(im.im_sVaccine,'-','Description') AS  im_sVaccine,  isnull(im.im_item_Count,0) AS im_item_Count, " _
                    & "  dbo.IM_SeparateCodeAndDescription(im.im_sVaccine,'-','Code') AS im_vaccine_code,  CASE ISNULL(im_LotNumber,'') WHEN ''  THEN  convert(varchar,im_item_Count ) ELSE (convert(varchar,im_item_Count) + ' - ' + im_LotNumber)  END AS im_LotNumberwithCount,ISNULL(im_sTradeName,'') AS im_sTradeName ,ISNULL(im_sManufacturer,'') AS im_sManufacturer,ISNULL(im_LotNumber,'') AS im_LotNumber,ISNULL(im_sSKU,'') AS im_sSKU   FROM  im_mst im  WHERE im_sActive='Active' ORDER BY im_sVaccine  "
            'Query = "SELECT im.im_item_id,dbo.IM_SeparateCodeAndDescription(im.im_sVaccine,'-','Description') AS  im_sVaccine,  isnull(im.im_item_Count,0) AS im_item_Count, " _
            '     & "  dbo.IM_SeparateCodeAndDescription(im.im_sVaccine,'-','Code') AS im_vaccine_code,  ISNULL(im_LotNumber,'') AS im_LotNumber  FROM  im_mst im  ORDER BY im_sVaccine  "

            dt = oDB.ReadQueryDataTable(Query)
            oDB.Disconnect()

            If Not dt Is Nothing Then
                GloUC_trvAssociates.Clear()
                GloUC_trvAssociates.DataSource = dt

                GloUC_trvAssociates.ValueMember = dt.Columns("im_item_id").ColumnName
                GloUC_trvAssociates.DescriptionMember = dt.Columns("im_LotNumberwithCount").ColumnName
                GloUC_trvAssociates.CodeMember = dt.Columns("im_sVaccine").ColumnName
                GloUC_trvAssociates.DrugFormMember = dt.Columns("im_vaccine_code").ColumnName
                GloUC_trvAssociates.DrugQtyQualifierMember = dt.Columns("im_item_Count").ColumnName 'item_Count

                GloUC_trvAssociates.FrequencyMember = dt.Columns("im_sTradeName").ColumnName 'TradeName

                GloUC_trvAssociates.NDCCodeMember = dt.Columns("im_sManufacturer").ColumnName 'Manufacturer
                GloUC_trvAssociates.DurationMember = dt.Columns("im_LotNumber").ColumnName ''Lot Number
                GloUC_trvAssociates.RouteMember = dt.Columns("im_sSKU").ColumnName
                ''''''''''''''''''''''
                ''GloUC_trvAssociates.ImageObject = dt.Columns("im_item_cnt").ColumnName

                GloUC_trvAssociates.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Code_Description
                GloUC_trvAssociates.Search = gloUserControlLibrary.gloUC_TreeView.enumSearchType.Instring

                GloUC_trvAssociates.FillTreeView()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            dt = Nothing
            oDB = Nothing
        End Try
        '''''''''Integrated by Chetan as on 11 Oct 2010- for IM in DM Setup

    End Sub


    Public Sub FillReferrals()
        Try
            '  trvTriggers.Nodes.Clear()
            Dim rootnode As myTreeNode = Nothing
            'rootnode = New myTreeNode("Templates", -1)
            'rootnode.ImageIndex = 0
            'rootnode.SelectedImageIndex = 0
            ' trvTriggers.Nodes.Add(rootnode)

            Dim newNode As New TreeNode
            '   Dim objMyTreeView As myTreeNode
            Dim objTemplateGallery As New clsTemplateGallery
            Dim objCategory As myTreeNode
            '  Dim objTemplate As myTreeNode
            Dim dvTemplate As DataView
            Dim dt_temp As DataTable = objTemplateGallery.GetAllCategory

            ' Dim j As Integer
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

                    ' dvTemplate = New DataView
                    dvTemplate = objTemplateGallery.GetAllTemplateGallery(ValueMember)
                    Dim dt As DataTable
                    dt = dvTemplate.Table
                    If Not dt Is Nothing Then
                        GloUC_trvAssociates.Clear()
                        GloUC_trvAssociates.DataSource = dt
                        GloUC_trvAssociates.ValueMember = dt.Columns(0).ColumnName
                        GloUC_trvAssociates.DescriptionMember = dt.Columns(1).ColumnName
                        GloUC_trvAssociates.CodeMember = dt.Columns(1).ColumnName
                        GloUC_trvAssociates.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Code
                        GloUC_trvAssociates.Search = gloUserControlLibrary.gloUC_TreeView.enumSearchType.Instring
                        GloUC_trvAssociates.FillTreeView()
                    End If

                End If
            Next
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub FillRx()

       
        Try
            Dim strsearch As String = ""

            If Not IsNothing(GloUC_trvAssociates.txtsearch.Text) Then
                strsearch = GloUC_trvAssociates.txtsearch.Text
            End If

            Dim obj As New ClsICD9AssociationDBLayer
            dt = obj.FillControls(0, strsearch)

            If Not IsNothing(dt) Then
                GloUC_trvAssociates.Clear()
                GloUC_trvAssociates.DataSource = dt
                GloUC_trvAssociates.IsDrug = True
                GloUC_trvAssociates.DrugFlag = 16 ''For all drugs 
                GloUC_trvAssociates.ValueMember = dt.Columns("DrugsID").ColumnName
                GloUC_trvAssociates.DescriptionMember = dt.Columns("Dosage").ColumnName
                GloUC_trvAssociates.CodeMember = dt.Columns("DrugName").ColumnName
                GloUC_trvAssociates.DrugFormMember = dt.Columns("DrugForm").ColumnName
                GloUC_trvAssociates.RouteMember = Convert.ToString(dt.Columns("sRoute").ColumnName)
                GloUC_trvAssociates.NDCCodeMember = Convert.ToString(dt.Columns("sNDCCode").ColumnName)
                GloUC_trvAssociates.IsNarcoticsMember = dt.Columns("nIsNarcotics").ColumnName
                GloUC_trvAssociates.FrequencyMember = dt.Columns("sFrequency").ColumnName
                GloUC_trvAssociates.DurationMember = dt.Columns("sDuration").ColumnName
                GloUC_trvAssociates.DrugQtyQualifierMember = dt.Columns("sDrugQtyQualifier").ColumnName
                GloUC_trvAssociates.mpidmember = dt.Columns("mpid").ColumnName
                GloUC_trvAssociates.Search = gloUserControlLibrary.gloUC_TreeView.enumSearchType.Simple
                'Display Type Changed by Mayuri:20091008
                'To display drugs in form of DrugName and Drug Form
                GloUC_trvAssociates.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Code_Description_DrugForm
                GloUC_trvAssociates.FillTreeView()
            End If

        Catch ex As Exception

        End Try
    End Sub

    Public Sub FillRadiologyTest()
        Try
            Dim dtOrders As New DataTable
            'Dim id As New DataColumn
            'dtOrders.Columns.Add(id)
            'Dim Name As New DataColumn
            'dtOrders.Columns.Add(Name)

            'trvTriggers.Nodes.Clear()
            'Dim rootnode As myTreeNode = Nothing

            'rootnode = New myTreeNode("Orders", -1)
            'rootnode.ImageIndex = 7
            'rootnode.SelectedImageIndex = 7
            'trvTriggers.Nodes.Add(rootnode)
            'Dim _C As Integer, _G As Integer, _T As Integer
            'Dim oDM As New gloStream.DiseaseManagement.Common.Criteria
            'Dim oOrders As New gloStream.DiseaseManagement.Supporting.Orders

            'oOrders = oDM.Orders
            ''Dim i As int = 0
            ''For Each ord As gloStream.DiseaseManagement.Supporting In oOrders
            ''    dtOrders.Rows(i)(0) = ord
            ''    i++
            ''Next

            'If Not oOrders Is Nothing Then
            '    If oOrders.Count > 0 Then

            '        'Fill Category
            '        For _C = 1 To oOrders.Count

            '            'rootnode = New myTreeNode(oLabs(_C).Category, oLabs(_C).ID)
            '            'trAssociates.Nodes.Item(0).Nodes.Add(rootnode)
            '            For _G = 1 To oOrders.Item(_C).OrderGroups.Count
            '                'Dim mychildnode As myTreeNode
            '                'mychildnode = New myTreeNode(oOrders.Item(_C).OrderGroups(_G).Name, oOrders.Item(_C).OrderGroups(_G).ID)
            '                'rootnode.Nodes.Add(mychildnode)
            '                'Fill Tests Start
            '                Dim i As Integer = 0
            '                For _T = 1 To oOrders.Item(_C).OrderGroups(_G).Tests.Count
            '                    Dim mychildnode_ As myTreeNode
            '                    mychildnode_ = New myTreeNode(oOrders.Item(_C).OrderGroups(_G).Tests(_T).Description, oOrders.Item(_C).OrderGroups(_G).Tests(_T).ID)
            '                    ' rootnode.Nodes.Add(mychildnode_)
            '                    mychildnode_.ImageIndex = 11
            '                    mychildnode_.SelectedImageIndex = 11
            '                    trvTriggers.Nodes.Item(0).Nodes.Add(mychildnode_)


            '                    dtOrders.Rows(_C)(0) = oOrders.Item(_C).OrderGroups(_G).Tests(_T).Description
            '                    dtOrders.Rows(_C)(1) = oOrders.Item(_C).OrderGroups(_G).Tests(_T).ID

            '                Next
            '            Next
            '            'Fill Tests Finish
            '        Next ' For _G = 1 To oOrders.Item(_C).OrderGroups.Count
            '        'Fill Groups & Category
            '        trvTriggers.ExpandAll()
            '    End If
            'End If
            'oDM = Nothing
            'oOrders = Nothing


            Dim oCls As New gloStream.DiseaseManagement.Common.Criteria
            Try
                dtOrders = oCls.OrdersTable
                If Not IsNothing(dtOrders) Then
                    GloUC_trvAssociates.Clear()
                    GloUC_trvAssociates.DataSource = dtOrders
                    GloUC_trvAssociates.CodeMember = Convert.ToString(dtOrders.Columns(1).ColumnName)
                    GloUC_trvAssociates.ValueMember = Convert.ToString(dtOrders.Columns(0).ColumnName)
                    GloUC_trvAssociates.DescriptionMember = Convert.ToString(dtOrders.Columns(1).ColumnName)
                    GloUC_trvAssociates.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Code
                    GloUC_trvAssociates.Search = gloUserControlLibrary.gloUC_TreeView.enumSearchType.Instring
                    GloUC_trvAssociates.FillTreeView()
                End If
            Catch ex As Exception
                MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    'Public Sub FillGuidLines()
    '    trvTriggers.Nodes.Clear()
    'End Sub


    'This code is added by shilpa to fill the otherinformation treeview

    Private Sub Fill_OtherInfo()


        Dim associatenode As New myTreeNode("Orders", -1)


        'associatenode.Key = -1
        'associatenode.Text = "Orders"
        'associatenode.ImageIndex = 5
        'associatenode.SelectedImageIndex = 5
        trOrderInfo.Nodes.Add(associatenode)
        'trOrderInfo.Nodes.Add(associatenode)

        Dim MyChild As New myTreeNode
        MyChild.Text = "Labs"
        MyChild.Key = -1
        MyChild.ImageIndex = 6
        MyChild.SelectedImageIndex = 6
        associatenode.Nodes.Add(MyChild)

        MyChild = New myTreeNode
        MyChild.Text = "Orders"
        MyChild.Key = -1
        MyChild.ImageIndex = 7
        MyChild.SelectedImageIndex = 7
        associatenode.Nodes.Add(MyChild)

        MyChild = New myTreeNode
        MyChild.Text = "Guidelines"
        MyChild.Key = -1
        MyChild.ImageIndex = 8
        MyChild.SelectedImageIndex = 8
        associatenode.Nodes.Add(MyChild)

        MyChild = New myTreeNode
        MyChild.Text = "Rx"
        MyChild.Key = -1
        MyChild.ImageIndex = 9
        MyChild.SelectedImageIndex = 9
        associatenode.Nodes.Add(MyChild)

        MyChild = New myTreeNode
        MyChild.Text = "Referrals"
        MyChild.Key = -1
        MyChild.ImageIndex = 10
        MyChild.SelectedImageIndex = 10
        associatenode.Nodes.Add(MyChild)

        MyChild = New myTreeNode
        MyChild.Text = "IM"
        MyChild.Key = -1
        MyChild.ImageIndex = 12
        MyChild.SelectedImageIndex = 12
        associatenode.Nodes.Add(MyChild)


        trOrderInfo.ExpandAll()

    End Sub

    'code for search in the tree node

    Private Sub txtSearch_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged
        Try
            ''COMMENTED BY SUDHIR 20090302 - AS TREE STRUCTURE CHANGED
            'check for text to be search
            'If Trim(txtSearch.Text) <> "" Then
            '    For i As Integer = 0 To trvHistoryRight.GetNodeCount(False) - 1
            '        If trvHistoryRight.Nodes(i).IsExpanded = True Then
            '            Dim mychildnode As TreeNode
            '            'child node collection
            '            For Each mychildnode In trvHistoryRight.Nodes(i).Nodes
            '                Dim str As String
            '                str = UCase(Trim(mychildnode.Text))
            '                If Mid(str, 1, Len(Trim(txtSearch.Text))) = UCase(Trim(txtSearch.Text)) Then
            '                    '***********code added by sagar to take the selected node to top on 6 july 2007
            '                    trvHistoryRight.SelectedNode = trvHistoryRight.Nodes(trvHistoryRight.Nodes.Count - 1) 'trvHistoryRight.SelectedNode.LastNode
            '                    '***********
            '                    trvHistoryRight.SelectedNode = mychildnode
            '                    txtSearch.Focus()
            '                    Exit Sub
            '                End If
            '            Next
            '        End If
            '    Next
            'End If
            ' search the data from node in history tree view
            'Search(txtSearch, trvHistoryRight)




            ''Sandip Darade 20090305
            If txtSearch.Text.Trim <> "" Then
                ''if alllergy to be searched
                If cmbHistoryCategory.Text = "Allergies" Then
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
                ElseIf gblnCodedHistory = True Then
                    Search(txtSearch.Text, dt)
                    ''if Flag  gblnCodedHistory is false
                Else
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
            End If
            ''If search text box is empty 
            If (txtSearch.Text.Trim = "") Then
                Fill_Histories(cmbHistoryCategory.Text)
            End If
            '    For Each oNode As TreeNode In trvHistoryRight.Nodes
            '        Dim NodeText As String = UCase(oNode.Text)
            '        If NodeText.Contains(UCase(txtSearch.Text.Trim)) Then
            '            trvHistoryRight.SelectedNode = oNode
            '            txtSearch.Focus()
            '            Exit Sub
            '        Else
            '            trvHistoryRight.SelectedNode = Nothing
            '        End If
            '    Next


        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try


    End Sub

    Private Sub btnDrugs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDrugs.Click
        Try
            ''This code is commented by shilpa because currently we are not using tab structure in form
            ''function for display Drugs Tag of the Tag Pages and hide others tags pages
            ''HidenShow(tpDrugs)
            FillAllCriteria()
            Me.Cursor = Cursors.WaitCursor
            sptRight.Visible = False
            pnlRight.Visible = False

            pnlRight.SendToBack()
            sptRight.SendToBack()

            pnlDrugs.Visible = True
            pnlDrugs.BringToFront()

            '' Ojeswini
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

            btnproblemlist.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
            btnproblemlist.BackgroundImageLayout = ImageLayout.Stretch
            btnproblemlist.Tag = "UnSelected"

            fill_Drugs()

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub


    Private Sub btnCPT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCPT.Click
        Try
            ''This code is commented by shilpa because currently we are not using tab structure in form
            ''function for display CPT Tag of the Tag Pages and hide others tags pages
            ''HidenShow(tpCPT)
            FillAllCriteria()
            sptRight.Visible = False
            pnlRight.Visible = False

            sptRight.SendToBack()
            pnlRight.SendToBack()

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

            btnproblemlist.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
            btnproblemlist.BackgroundImageLayout = ImageLayout.Stretch
            btnproblemlist.Tag = "UnSelected"
            'fill_Drugs()
            fill_CPTs("")
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnICD9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnICD9.Click
        Try
            ''This code is commented by shilpa because currently we are not using tab structure in form
            ''function for display ICP9 Tag of the Tag Pages and hide others tags pages
            ''HidenShow(tpICD9)
            FillAllCriteria()
            sptRight.Visible = False
            pnlRight.Visible = False
            pnlRight.SendToBack()

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

            btnproblemlist.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
            btnproblemlist.BackgroundImageLayout = ImageLayout.Stretch
            btnproblemlist.Tag = "UnSelected"
            'fill_CPTs()
            Fill_ICD9s("")
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnRadiology_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRadiology.Click
        Try
            ''This code is commented by shilpa because currently we are not using tab structure in form
            ''function for display Labs Tag of the Tag Pages and hide others tags pages
            'HidenShow(tpRadiology)
            FillAllCriteria()
            sptRight.Visible = False
            pnlRight.Visible = False
            pnlRight.SendToBack()

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

            btnproblemlist.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
            btnproblemlist.BackgroundImageLayout = ImageLayout.Stretch
            btnproblemlist.Tag = "UnSelected"

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    'Private Sub btnGuidelines_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOthers.Click
    '    Try
    '        ''This code is commented by shilpa because currently we are not using tab structure in form
    '        ''function for display GuideLine Tag of the Tag Pages and hide others tags pages
    '        ''HidenShow(tpGuideline)
    '        pnlSummaryOthers.Visible = True
    '        pnlSummaryOthers.BringToFront()
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub

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

    Private Sub txtDrugSearch_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        '    Try
        '        ' search the data from node in Drug tree view
        '        ' Search(txtDrugSearch, trvDrgs)

        '        '''''''''Code is modified by Anil on 20071106
        '        '' Fill Drugs
        '        Dim oDM As New gloStream.DiseaseManagement.Common.Criteria
        '        If Len(Trim(txtDrugSearch.Text)) <= 1 Then
        '            ''''To get the drugs with first character in search textbox
        '            oDM.Drugs(LCase(txtDrugSearch.Text.Trim))
        '            fill_Drugs()
        '        Else
        '            ''''''''''code to select the drug with name greater than one character string
        '            Dim mychildnode As TreeNode
        '            'child node collection
        '            For Each mychildnode In trvDrgs.Nodes
        '                Dim str As String
        '                str = UCase(mychildnode.Text)
        '                If Mid(str, 1, Len(Trim(txtDrugSearch.Text))) = UCase(Trim(txtDrugSearch.Text)) Then
        '                    trvDrgs.SelectedNode = trvDrgs.Nodes(trvDrgs.Nodes.Count - 1)
        '                    trvDrgs.SelectedNode = mychildnode
        '                    txtDrugSearch.Focus()
        '                    Exit Sub
        '                End If
        '            Next
        '        End If
        '        '''''''''''''''''''''''''''''''''
        '    Catch ex As Exception
        '        MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    End Try
    End Sub

    Private Sub txtSearch_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtSearch.KeyUp
        Try
            ' for focus select tree view
            If e.KeyCode = Keys.Enter Then
                trvHistoryRight.Select()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub trvHistory_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles trvHistory.KeyPress
        If trvHistory.GetNodeCount(False) > 0 Then
            If (e.KeyChar = ChrW(13)) Then
                trvHistory.Select()
                'Else
                '    trvSource.SelectedNode = trvSource.Nodes.Item(0)
            End If
        End If
    End Sub

    Private Sub trvHistory_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles trvHistory.KeyUp
        Try
            ' for back to search textbox.
            If e.KeyCode = Keys.Back Then
                txtSearch.Select()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtICD9Search_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            ' search the data from node in ICD9 tree view
            'Search(txtICD9Search, trvICD9)
            ''FillICD9TreeView(dtICD9, txtICD9Search.Text)
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    'Private Sub txtSearchOrder_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSearchOrder.TextChanged
    '    Try
    '        ' search the data from node in GuidaLines tree view
    '        'Search(txtSearchOrder, trvTriggers)
    '        'check for text to be search
    '        'If Trim(txtSearchOrder.Text) <> "" Then
    '        '    For i As Integer = 0 To trvTriggers.GetNodeCount(False) - 1
    '        '        If trvTriggers.Nodes(i).IsExpanded = True Then
    '        '            Dim mychildnode As TreeNode
    '        '            'child node collection
    '        '            For Each mychildnode In trvTriggers.Nodes(i).Nodes
    '        '                Dim str As String
    '        '                str = UCase(Trim(mychildnode.Text))
    '        '                If Mid(str, 1, Len(Trim(txtSearchOrder.Text))) = UCase(Trim(txtSearchOrder.Text)) Then
    '        '                    trvTriggers.SelectedNode = trvTriggers.Nodes(trvTriggers.Nodes.Count - 1)
    '        '                    trvTriggers.SelectedNode = mychildnode
    '        '                    txtSearchOrder.Focus()
    '        '                    Exit Sub
    '        '                End If
    '        '            Next
    '        '        End If
    '        '    Next
    '        'End If

    '        Try
    '            ' search the data from node in Drug tree view
    '            ' Search(txtDrugSearch, trvDrgs)

    '            '''''''''Code is modified by Anil on 20071106
    '            '' Fill Drugs
    '            Dim oDM As New gloStream.DiseaseManagement.Common.Criteria
    '            If txtSearchOrder.Text.Trim.Length <= 1 Then
    '                ''''To get the drugs with first character in search textbox
    '                oDM.Drugs(txtSearchOrder.Text.Trim.ToLower)
    '                fill_Drugs_Trigger()
    '            Else
    '                ''''''''''code to select the drug with name greater than one character string
    '                Dim mychildnode As TreeNode
    '                'child node collection
    '                For Each mychildnode In trvTriggers.Nodes.Item(0).Nodes
    '                    Dim str As String
    '                    str = UCase(mychildnode.Text)
    '                    If Mid(str, 1, Len(Trim(txtSearchOrder.Text))) = UCase(Trim(txtSearchOrder.Text)) Then
    '                        trvTriggers.SelectedNode = trvTriggers.Nodes.Item(0).Nodes(trvTriggers.Nodes.Item(0).Nodes.Count - 1)
    '                        trvTriggers.SelectedNode = mychildnode
    '                        txtSearchOrder.Focus()
    '                        Exit Sub
    '                    End If
    '                Next
    '            End If
    '            '''''''''''''''''''''''''''''''''
    '        Catch ex As Exception
    '            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        End Try

    '        Try
    '            If Trim(txtSearchOrder.Text) <> "" Then
    '                If trvTriggers.Nodes(0).GetNodeCount(False) > 0 Then
    '                    Dim mychildnode As TreeNode
    '                    'child node collection

    '                    For Each mychildnode In trvTriggers.Nodes(0).Nodes
    '                        Dim str As String
    '                        str = UCase(Trim(mychildnode.Text))
    '                        If Mid(str, 1, Len(Trim(txtSearchOrder.Text))) = UCase(Trim(txtSearchOrder.Text)) Then
    '                            If Not IsNothing(trvTriggers.SelectedNode) Then
    '                                If Not IsNothing(trvTriggers.SelectedNode.LastNode) Then
    '                                    trvTriggers.SelectedNode = trvTriggers.SelectedNode.LastNode
    '                                End If
    '                            End If

    '                            trvTriggers.SelectedNode = mychildnode
    '                            'trvCategory.HideSelection = False
    '                            txtSearchOrder.Focus()
    '                            Exit Sub
    '                        Else
    '                            'trvCategory.HideSelection = True
    '                        End If
    '                    Next
    '                End If
    '            End If
    '        Catch ex As Exception
    '            MessageBox.Show(ex.Message, "DM Setup", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        End Try


    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub

    Private Sub txtDrugSearch_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        'Try
        '    ' for focus select tree view
        '    If e.KeyCode = Keys.Enter Then
        '        trvDrgs.Select()
        '    End If
        'Catch ex As Exception
        '    MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        'End Try
    End Sub

    Private Sub trvDrgs_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        'Try
        '    ' for back to search textbox.
        '    If e.KeyCode = Keys.Back Then
        '        txtDrugSearch.Select()
        '    End If
        'Catch ex As Exception
        '    MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        'End Try
    End Sub

    Private Sub txtCPTsearch_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        'Try
        '    ' for focus select tree view
        '    If e.KeyCode = Keys.Enter Then
        '        trvCPT.Select()
        '    End If
        'Catch ex As Exception
        '    MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        'End Try
    End Sub

    Private Sub trvCPT_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        'Try
        '    ' for back to search textbox.
        '    If e.KeyCode = Keys.Back Then
        '        txtCPTsearch.Select()
        '    End If
        'Catch ex As Exception
        '    MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        'End Try
    End Sub

    Private Sub txtCPTsearch_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        'Try
        '    ' search the data from node in CPT tree view
        '    ''Sandip Darade 20090415
        '    ''Added   'FillCPTTreeView(dtCPT, txtCPTsearch.Text)' to serch  by code or description 
        '    ''Search(txtCPTsearch, trvCPT)
        '    FillCPTTreeView(dtCPT, txtCPTsearch.Text)
        'Catch ex As Exception
        '    MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        'End Try
    End Sub

    Private Sub txtLabsSearch_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtLabsSearch.KeyUp
        Try
            ' for focus select tree view
            If e.KeyCode = Keys.Enter Then
                c1Labs.Select()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub trvLabs_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        Try
            ' for back to search textbox.
            If e.KeyCode = Keys.Back Then
                txtLabsSearch.Select()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtICD9Search_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        'Try
        '    ' for focus select tree view
        '    If e.KeyCode = Keys.Enter Then
        '        trvICD9.Select()
        '    End If
        'Catch ex As Exception
        '    MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        'End Try
    End Sub

    Private Sub trvICD9_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        'Try
        '    ' for back to search textbox.
        '    If e.KeyCode = Keys.Back Then
        '        txtICD9Search.Select()
        '    End If
        'Catch ex As Exception
        '    MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        'End Try
    End Sub

    Private Sub txtGuideLineSeach_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtSearchOrder.KeyUp
        Try
            ' for focus select tree view
            If e.KeyCode = Keys.Enter Then
                trOrderInfo.Select()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub trvGuideLineLeft_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        Try
            ' for back to search textbox.
            If e.KeyCode = Keys.Back Then
                txtSearchOrder.Select()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
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
                            ''For Each mychildnode In searchtreeview.Nodes.Item(i).Nodes ''Commented Sandip Darade 
                            For Each mychildnode In searchtreeview.Nodes  ''Sandip Darade 
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
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    ''Not in use
    Private Sub Fill_ICD9s_old()
        'Dim oICD9s As New gloStream.DiseaseManagement.Supporting.ICD9s
        'Dim oDM As New gloStream.DiseaseManagement.Common.Criteria

        'Dim oNode As TreeNode

        'oICD9s = oDM.ICD9s

        'With trvICD9
        '    .Nodes.Clear()
        '    If Not oICD9s Is Nothing Then
        '        For i As Int16 = 1 To oICD9s.Count
        '            oNode = New TreeNode
        '            With oNode
        '                .Text = oICD9s(i).Code & "-" & oICD9s(i).Name
        '                ' .Tag = oICD9s(i).ID
        '                .Tag = oICD9s(i).Code
        '            End With
        '            .Nodes.Add(oNode)
        '            oNode = Nothing
        '        Next
        '    End If
        'End With
    End Sub

    ''Sandip Darade 20090415
    ''Added to retrieve ICD9  ordered by code description
    Private Sub Fill_ICD9s(ByVal strSortBy As String)

        objICD9AssociationDBLayer = New ClsICD9AssociationDBLayer

        If IsNothing(dtICD9) = True Then
            dtICD9 = New DataTable
        End If

        dtICD9 = objICD9AssociationDBLayer.FillControls(3, strSortBy)

        ' Dim oNode As TreeNode

        If IsNothing(dtICD9) = False Then
            'With trvICD9
            '    .Nodes.Clear()
            '    For i As Int16 = 0 To dtICD9.Rows.Count - 1
            '        oNode = New TreeNode
            '        With oNode
            '            .Text = Convert.ToString(dtICD9.Rows(i)(1))
            '            .Tag = Convert.ToString(dtICD9.Rows(i)(3))
            '        End With
            '        .Nodes.Add(oNode)
            '        oNode = Nothing
            '    Next
            'End With

            GloUC_trvICD9.DataSource = dtICD9
            GloUC_trvICD9.CodeMember = Convert.ToString(dtICD9.Columns(3).ColumnName)
            GloUC_trvICD9.ValueMember = Convert.ToString(dtICD9.Columns(0).ColumnName)
            GloUC_trvICD9.DescriptionMember = Convert.ToString(dtICD9.Columns(2).ColumnName)
            GloUC_trvICD9.FillTreeView()

        End If

        'trvICD9.ExpandAll()
        'trvICD9.Show()
        'trvICD9.Select()

    End Sub

    Private Sub fill_Drugs()
        

        ''Sandip Darade  20091015
        ''above code commented to replace it with code below to implement the changes done on  treeviev control for drugs 

        Try
            Dim strsearch As String = ""

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
                GloUC_trvDrugs.NDCCodeMember = Convert.ToString(dt.Columns("sNDCCode").ColumnName) ''''bug fix for 6852
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

    'Private Sub fill_Drugs_Trigger()
    '    Dim oDrugs As New gloStream.DiseaseManagement.Supporting.Drugs
    '    Dim oDM As New gloStream.DiseaseManagement.Common.Criteria
    '    Dim oNode As myTreeNode
    '    Try

    '        oDrugs = oDM.Drugs(txtSearchOrder.Text.Trim)

    '        With trvTriggers
    '            .BeginUpdate()
    '            .Nodes.Clear()
    '            Dim rootnode As myTreeNode = Nothing
    '            rootnode = New myTreeNode("Rx", -1)
    '            rootnode.ImageIndex = 9
    '            rootnode.SelectedImageIndex = 9
    '            .Nodes.Add(rootnode)
    '            If Not oDrugs Is Nothing Then
    '                For i As Int64 = 1 To oDrugs.Count
    '                    oNode = New myTreeNode
    '                    With oNode
    '                        .Text = oDrugs(i).Name
    '                        .Key = oDrugs(i).ID
    '                        .ImageIndex = 11
    '                        .SelectedImageIndex = 11
    '                    End With
    '                    .Nodes.Item(0).Nodes.Add(oNode)
    '                    oNode = Nothing
    '                Next
    '            End If
    '            .EndUpdate()
    '            .ExpandAll()
    '        End With
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub

    Private Sub fill_CPTs_old()
        Dim oCPTS As gloStream.DiseaseManagement.Supporting.CPTs
        Dim oDm As New gloStream.DiseaseManagement.Common.Criteria
        ' Dim oNode As TreeNode

        oCPTS = oDm.CPTs
        If Not oCPTS Is Nothing Then
            oCPTS.Dispose()
            oCPTS = Nothing
        End If

        oDm.Dispose()
        oDm = Nothing
        'With trvCPT
        '    .Nodes.Clear()
        '    If Not oCPTS Is Nothing Then
        '        For i As Int16 = 1 To oCPTS.Count
        '            oNode = New TreeNode
        '            With oNode
        '                '.Text = oCPTS(i).Name
        '                '.Tag = oCPTS(i).ID
        '                ''Show CPT code and name as text 
        '                .Text = oCPTS(i).Code + " - " + oCPTS(i).Name
        '                .Tag = oCPTS(i).Code
        '            End With
        '            .Nodes.Add(oNode)
        '            oNode = Nothing
        '        Next
        '    End If

        'End With

    End Sub

    Private Sub fill_CPTs(ByVal strSortBy As String)

        objICD9AssociationDBLayer = New ClsICD9AssociationDBLayer

        If IsNothing(dtCPT) = True Then
            dtCPT = New DataTable
        End If

        dtCPT = objICD9AssociationDBLayer.FillControls(1, strSortBy)

        'Dim oNode As TreeNode

        If IsNothing(dtCPT) = False Then
            'With trvCPT
            '    .Nodes.Clear()
            '    For i As Int16 = 0 To dtCPT.Rows.Count - 1
            '        oNode = New TreeNode
            '        With oNode
            '            .Text = Convert.ToString(dtCPT.Rows(i)(2))
            '            .Tag = Convert.ToString(dtCPT.Rows(i)(3))
            '        End With
            '        .Nodes.Add(oNode)
            '        oNode = Nothing
            '    Next
            'End With
            GloUC_trvCPT.DataSource = dtCPT
            GloUC_trvCPT.CodeMember = Convert.ToString(dtCPT.Columns(3).ColumnName)
            GloUC_trvCPT.ValueMember = Convert.ToString(dtCPT.Columns(0).ColumnName)
            GloUC_trvCPT.DescriptionMember = Convert.ToString(dtCPT.Columns(1).ColumnName)
            GloUC_trvCPT.FillTreeView()

        End If

        'trvCPT.ExpandAll()
        'trvCPT.Show()
        'trvCPT.Select()
    End Sub

    '' Sudhir 20090302 '' 
    Public Function GetHistoryCategory() As DataTable
        Dim oDB As New gloStream.gloDataBase.gloDataBase
        Dim dt As New DataTable
        Dim Query As String = ""
        '  Dim HistoryCategory As Collection
        Try
            oDB.Connect(GetConnectionString)
            Query = "SELECT nCategoryID, sDescription FROM Category_MST WHERE (sCategoryType = 'History') ORDER BY sDescription"
            dt = oDB.ReadQueryDataTable(Query)
            If Not IsNothing(dt) Then
                Return dt
            End If
            Return Nothing
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            oDB.Disconnect()
            oDB.Dispose()
            oDB = Nothing
        End Try
    End Function

    Private Sub Fill_Histories(Optional ByVal HistoryCategory As String = "")
       

        txtSearch.Text = ""
        If HistoryCategory = "" Then
            Dim dtHistoryCategory As DataTable
            dtHistoryCategory = GetHistoryCategory()
            cmbHistoryCategory.DataSource = dtHistoryCategory
            cmbHistoryCategory.DisplayMember = dtHistoryCategory.Columns("sDescription").ColumnName
            cmbHistoryCategory.ValueMember = dtHistoryCategory.Columns("nCategoryID").ColumnName
            Fill_Histories(dtHistoryCategory.Rows(0)("sDescription").ToString)
        Else
            trvHistoryRight.Nodes.Clear()
            Dim oNode As myTreeNode
            Dim oHistory As gloStream.DiseaseManagement.Supporting.History
            '   Dim oCategories As New gloStream.DiseaseManagement.Supporting.Categories
            Dim oDm As New gloStream.DiseaseManagement.Common.Criteria

            If HistoryCategory = "Allergies" Then
                oHistory = oDm.Histories(HistoryCategory)
                If Not oHistory Is Nothing Then
                    For i As Integer = 1 To oHistory.Items.Count
                        oNode = New myTreeNode
                        oNode.Text = oHistory.Items(i).Name
                        oNode.Tag = HistoryCategory
                        oNode.Key = oHistory.Items(i).ID
                        trvHistoryRight.Nodes.Add(oNode)
                        oNode = Nothing
                    Next
                    oHistory.Dispose()
                    oHistory = Nothing
                End If
      
            ElseIf gblnCodedHistory = True Then
                dt = oDm.GetHistoriesDataTable(HistoryCategory)

                If Not dt Is Nothing Then

                    Dim Rowcount As Integer
                    If dt.Rows.Count > 50 Then
                        Rowcount = 50
                    Else
                        Rowcount = dt.Rows.Count - 1
                    End If

                    For i As Integer = 0 To Rowcount
                        oNode = New myTreeNode
                        oNode.Text = dt.Rows(i)("Column1")
                        oNode.Tag = HistoryCategory
                        oNode.Key = dt.Rows(i)("ICD9ID")
                        trvHistoryRight.Nodes.Add(oNode)
                        oNode = Nothing
                    Next
                End If
            Else
                oHistory = oDm.Histories(HistoryCategory)
                If Not oHistory Is Nothing Then
                    For i As Integer = 1 To oHistory.Items.Count
                        oNode = New myTreeNode
                        oNode.Text = oHistory.Items(i).Name
                        oNode.Tag = HistoryCategory
                        oNode.Key = oHistory.Items(i).ID
                        trvHistoryRight.Nodes.Add(oNode)
                        oNode = Nothing
                    Next
                    oHistory.Dispose()
                    oHistory = Nothing
                End If
            End If

            oDm.Dispose()
            oDm = Nothing

            'If gblnCodedHistory = True And HistoryCategory <> "Allergies" Then
            '    If (gblnClinicDIAlert = False) Then
            '        dt = oDm.Histories_new(HistoryCategory)

            '        If Not dt Is Nothing Then

            '            Dim Rowcount As Integer
            '            If dt.Rows.Count > 50 Then
            '                Rowcount = 50
            '            Else
            '                Rowcount = dt.Rows.Count - 1
            '            End If

            '            For i As Integer = 0 To Rowcount
            '                oNode = New myTreeNode
            '                oNode.Text = dt.Rows(i)("Column1")
            '                oNode.Tag = HistoryCategory
            '                oNode.Key = dt.Rows(i)("ICD9ID")
            '                trvHistoryRight.Nodes.Add(oNode)
            '                oNode = Nothing
            '            Next
            '        End If
            '    Else
            '        oHistory = oDm.Histories(HistoryCategory)
            '        If Not oHistory Is Nothing Then
            '            For i As Integer = 1 To oHistory.Items.Count
            '                oNode = New myTreeNode
            '                oNode.Text = oHistory.Items(i).Name
            '                oNode.Tag = HistoryCategory
            '                oNode.Key = oHistory.Items(i).ID
            '                trvHistoryRight.Nodes.Add(oNode)
            '                oNode = Nothing
            '            Next
            '        End If
            '    End If
            'Else


            '    oHistory = oDm.Histories(HistoryCategory)
            '    'Dim Rowcount As Integer
            '    'If oHistory.Items.Count > 50 Then
            '    '    Rowcount = 50
            '    'Else
            '    '    Rowcount = oHistory.Items.Count
            '    'End If
            '    If Not oHistory Is Nothing Then
            '        For i As Integer = 1 To oHistory.Items.Count
            '            oNode = New myTreeNode
            '            oNode.Text = oHistory.Items(i).Name
            '            oNode.Tag = HistoryCategory
            '            oNode.Key = oHistory.Items(i).ID
            '            trvHistoryRight.Nodes.Add(oNode)
            '            oNode = Nothing
            '        Next
            '    End If
            'End If
        End If

    End Sub
    Private Sub Fill_Histories_1(Optional ByVal HistoryCategory As String = "")
      

        txtSearch.Text = ""
        If HistoryCategory = "" Then
            Dim dtHistoryCategory As DataTable
            dtHistoryCategory = GetHistoryCategory()
            cmbHistoryCategory.DataSource = dtHistoryCategory
            cmbHistoryCategory.DisplayMember = dtHistoryCategory.Columns("sDescription").ColumnName
            cmbHistoryCategory.ValueMember = dtHistoryCategory.Columns("nCategoryID").ColumnName
            ' Fill_Histories(dtHistoryCategory.Rows(0)("sDescription").ToString)
            Fill_Histories_1(dtHistoryCategory.Rows(0)("sDescription").ToString)
        Else
            'trvHistoryRight.Nodes.Clear()
            ' Dim oNode As myTreeNode

          
            Dim oDm As New gloStream.DiseaseManagement.Common.Criteria
            dt = oDm.GetHistoriesDataTable(HistoryCategory)
            oDm.Dispose()
            oDm = Nothing
            If Not dt Is Nothing Then
                GloUC_trvHistory.DataSource = dt
                GloUC_trvHistory.CodeMember = Convert.ToString(dt.Columns(1).ColumnName)
                GloUC_trvHistory.ValueMember = Convert.ToString(dt.Columns(0).ColumnName)
                GloUC_trvHistory.DescriptionMember = Convert.ToString(dt.Columns(1).ColumnName)
                GloUC_trvHistory.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Descripation
                GloUC_trvHistory.FillTreeView()
                GloUC_trvHistory.FocusSearchBox()
                'For i As Integer = 0 To Rowcount
                '    oNode = New myTreeNode
                '    oNode.Text = dt.Rows(i)("Column1")
                '    oNode.Tag = HistoryCategory
                '    oNode.Key = dt.Rows(i)("ICD9ID")
                '    trvHistoryRight.Nodes.Add(oNode)
                '    oNode = Nothing
                'Next
            End If





        End If

    End Sub


    Private Sub Fill_Histories_old(Optional ByVal HistoryCategory As String = "")
       


        If HistoryCategory = "" Then
            Dim dtHistoryCategory As DataTable
            dtHistoryCategory = GetHistoryCategory()
            cmbHistoryCategory.DataSource = dtHistoryCategory
            cmbHistoryCategory.DisplayMember = dtHistoryCategory.Columns("sDescription").ColumnName
            cmbHistoryCategory.ValueMember = dtHistoryCategory.Columns("nCategoryID").ColumnName
            Fill_Histories(dtHistoryCategory.Rows(0)("sDescription").ToString)
        Else
            trvHistoryRight.Nodes.Clear()
            Dim oNode As myTreeNode
            Dim oHistory As gloStream.DiseaseManagement.Supporting.History
            ' Dim oCategories As New gloStream.DiseaseManagement.Supporting.Categories
            Dim oDm As New gloStream.DiseaseManagement.Common.Criteria
            oHistory = oDm.Histories(HistoryCategory)
            If Not oHistory Is Nothing Then
                For i As Integer = 1 To oHistory.Items.Count
                    oNode = New myTreeNode
                    oNode.Text = oHistory.Items(i).Name
                    oNode.Tag = HistoryCategory
                    oNode.Key = oHistory.Items(i).ID
                    trvHistoryRight.Nodes.Add(oNode)
                    oNode = Nothing
                Next
                oHistory.Dispose()
                oHistory = Nothing
            End If
            oDm.Dispose()
            oDm = Nothing
        End If
    End Sub
    ''Sandip Darade 20090305
    ''to serach coded history  
    Public Sub Search(ByVal strsearch As String, ByVal dt As DataTable)
        Try
            Dim i As Integer
            Dim tdt As DataTable
            Dim dv As DataView
            If (IsNothing(dt)) Then
                Exit Sub
            End If
            Dim strsearchHistory As String
            If Trim(strsearch) <> "" Then
                strsearchHistory = Replace(strsearch, "'", "''")
                strsearchHistory = Replace(strsearchHistory, "[", "") & ""
                strsearchHistory = mdlGeneral.ReplaceSpecialCharacters(strsearchHistory)
            Else
                strsearchHistory = ""
            End If
            

            dv = New DataView(dt)
            strsearch = trvHistoryRight.Text.Trim
            ''Appply filter to the dataview
            dv.RowFilter = dt.Columns(1).ColumnName & " Like '%" & strsearchHistory & "%'"


            tdt = dv.ToTable
            trvHistoryRight.Nodes.Clear()
            Dim oNode As myTreeNode
            If strsearch <> "" Then

                For Each dtrow As DataRow In tdt.Rows

                    oNode = New myTreeNode
                    oNode.Text = tdt.Rows(i)(0)
                    oNode.Tag = cmbHistoryCategory.Text
                    oNode.Key = tdt.Rows(i)(1)
                    trvHistoryRight.Nodes.Add(oNode)
                    oNode = Nothing


                Next dtrow
            Else

                For i = 0 To tdt.Rows.Count - 1
                    oNode = New myTreeNode
                    oNode.Text = tdt.Rows(i)("Column1")
                    oNode.Tag = cmbHistoryCategory.Text
                    oNode.Key = tdt.Rows(i)("ICD9ID")
                    trvHistoryRight.Nodes.Add(oNode)
                    oNode = Nothing

                Next
            End If
            dv.Dispose()
            dv = Nothing
            tdt.Dispose()
            tdt = Nothing

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub fill_guideline()
        Try
            'trvTriggers.Nodes.Clear()
            Dim rootnode As myTreeNode = Nothing
            rootnode = New myTreeNode("Guidelines", -1)
            rootnode.ImageIndex = 8
            rootnode.SelectedImageIndex = 8
            ' trvTriggers.Nodes.Add(rootnode)
            'This code is commented by shilpa 
            'Dim oGuideLines As New gloStream.DiseaseManagement.Supporting.ItemDetails
            'Dim oCategories As New gloStream.DiseaseManagement.Common.GuidelinesType
            Dim oDm As New gloStream.DiseaseManagement.Common.Guidelines
            Dim dt As DataTable
            dt = oDm.GuidelinesTables("")
            oDm = Nothing
            '   Dim oNode As myTreeNode
            '    oCategories = oDm.Categories

            'fill category of history

            '.Nodes.Clear()

            '     If Not oCategories Is Nothing Then
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


            'With trvTriggers
            '    'For j As Integer = 0 To .Nodes.Item(0).GetNodeCount(False) - 1

            '    oGuideLines = New gloStream.DiseaseManagement.Supporting.ItemDetails
            '    oGuideLines = oDm.Guidelines(gloStream.DiseaseManagement.Common.GuidelinesType.PatientEducation)

            '    For i As Int16 = 1 To oGuideLines.Count
            '        oNode = New myTreeNode
            '        oNode.Text = oGuideLines(i).Description
            '        oNode.Tag = oGuideLines(i).ID
            '        oNode.Key = oGuideLines(i).ID
            '        'sarika DM Denormalization
            '        oNode.DMTemplate = oGuideLines(i).Template
            '        '----
            '        'With oNode
            '        '    .Text = oGideLines(i).Description
            '        '    .Tag = oGideLines(i).ID
            '        'End With
            '        oNode.ImageIndex = 11
            '        oNode.SelectedImageIndex = 11
            '        .Nodes.Item(0).Nodes.Add(oNode)
            '        oNode = Nothing
            '    Next

            '    oGuideLines = New gloStream.DiseaseManagement.Supporting.ItemDetails
            '    oGuideLines = oDm.Guidelines(gloStream.DiseaseManagement.Common.GuidelinesType.WellnessGuidelines)

            '    For i As Int16 = 1 To oGuideLines.Count
            '        oNode = New myTreeNode
            '        oNode.Text = oGuideLines(i).Description
            '        oNode.Tag = oGuideLines(i).ID
            '        oNode.Key = oGuideLines(i).ID
            '        'sarika DM Denormalization
            '        oNode.DMTemplate = oGuideLines(i).Template
            '        '----

            '        'With oNode
            '        '    .Text = oGideLines(i).Description
            '        '    .Tag = oGideLines(i).ID
            '        'End With
            '        oNode.ImageIndex = 11
            '        oNode.SelectedImageIndex = 11
            '        .Nodes.Item(0).Nodes.Add(oNode)
            '        oNode = Nothing
            '    Next
            '    oGuideLines = Nothing

            '    oGuideLines = New gloStream.DiseaseManagement.Supporting.ItemDetails
            '    oGuideLines = oDm.Guidelines(gloStream.DiseaseManagement.Common.GuidelinesType.PreventiveServices)

            '    For i As Int16 = 1 To oGuideLines.Count
            '        oNode = New myTreeNode
            '        oNode.Text = oGuideLines(i).Description
            '        oNode.Tag = oGuideLines(i).ID
            '        oNode.Key = oGuideLines(i).ID
            '        'sarika DM Denormalization
            '        oNode.DMTemplate = oGuideLines(i).Template
            '        '----

            '        'With oNode
            '        '    .Text = oGideLines(i).Description
            '        '    .Tag = oGideLines(i).ID
            '        'End With
            '        oNode.ImageIndex = 11
            '        oNode.SelectedImageIndex = 11
            '        .Nodes.Item(0).Nodes.Add(oNode)
            '        oNode = Nothing
            '    Next
            '    oGuideLines = Nothing

            '    trvTriggers.ExpandAll()
            ' Next
            'End With
            If Not dt Is Nothing Then
                GloUC_trvAssociates.Clear()
                GloUC_trvAssociates.DataSource = dt
                'Changed by Shweta 20100107 
                'Against the bug id:5611
                'bind template to the node 
                GloUC_trvAssociates.ValueMember = dt.Columns("nTemplateID").ColumnName
                GloUC_trvAssociates.DescriptionMember = dt.Columns("sTemplateName").ColumnName
                GloUC_trvAssociates.CodeMember = dt.Columns("sTemplateName").ColumnName
                GloUC_trvAssociates.ImageObject = dt.Columns("sDescription").ColumnName
                'end 20100107
                GloUC_trvAssociates.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Code
                GloUC_trvAssociates.Search = gloUserControlLibrary.gloUC_TreeView.enumSearchType.Instring
                GloUC_trvAssociates.FillTreeView()
            End If
            'End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Fill_Age()
        Dim oCollection As Collection
        Dim oDM As New gloStream.DiseaseManagement.Common.Criteria

        Dim oSett As gloSettings.GeneralSettings = New gloSettings.GeneralSettings(GetConnectionString())
        Dim objVal As Object = New Object()
        oSett.GetSetting("PEDIATRICS", objVal)
        oSett.Dispose()
        oSett = Nothing
        Dim _result As Boolean = False
        If Not IsNothing(objVal) Then
            _result = CType(objVal, Boolean)
        End If
        cmbAgeMinMnth.Visible = _result
        cmbAgeMaxMnth.Visible = _result

        oCollection = oDM.Age
        oDM.Dispose()
        oDM = Nothing

        cmbAgeMin.Items.Clear()
        cmbAgeMax.Items.Clear()
        cmbAgeMinMnth.Items.Clear()
        cmbAgeMaxMnth.Items.Clear()

        For i As Int16 = 1 To oCollection.Count - 1      '''''To resolve bug no 438 "-1" is added by Anil on 29/10/2007.
            cmbAgeMin.Items.Add(oCollection(i))
            cmbAgeMax.Items.Add(oCollection(i))
        Next
        oCollection.Clear()
        oCollection = Nothing

        'For i As Int16 = 0 To 11
        cmbAgeMinMnth.Items.Clear()
        cmbAgeMinMnth.Items.Add("00")
        cmbAgeMinMnth.Items.Add("01")
        cmbAgeMinMnth.Items.Add("02")
        cmbAgeMinMnth.Items.Add("03")
        cmbAgeMinMnth.Items.Add("04")
        cmbAgeMinMnth.Items.Add("05")
        cmbAgeMinMnth.Items.Add("06")
        cmbAgeMinMnth.Items.Add("07")
        cmbAgeMinMnth.Items.Add("08")
        cmbAgeMinMnth.Items.Add("09")
        cmbAgeMinMnth.Items.Add("10")
        cmbAgeMinMnth.Items.Add("11")

        cmbAgeMaxMnth.Items.Clear()
        cmbAgeMaxMnth.Items.Add("00")
        cmbAgeMaxMnth.Items.Add("01")
        cmbAgeMaxMnth.Items.Add("02")
        cmbAgeMaxMnth.Items.Add("03")
        cmbAgeMaxMnth.Items.Add("04")
        cmbAgeMaxMnth.Items.Add("05")
        cmbAgeMaxMnth.Items.Add("06")
        cmbAgeMaxMnth.Items.Add("07")
        cmbAgeMaxMnth.Items.Add("08")
        cmbAgeMaxMnth.Items.Add("09")
        cmbAgeMaxMnth.Items.Add("10")
        cmbAgeMaxMnth.Items.Add("11")
        'Next

    End Sub

    Private Sub fill_Maritalst()
        Dim oCollection As Collection
        Dim oDM As New gloStream.DiseaseManagement.Common.Criteria

        oCollection = oDM.MaritalStatus

        cmbMaritalSt.Items.Clear()
        For i As Int16 = 1 To oCollection.Count
            cmbMaritalSt.Items.Add(oCollection(i))
        Next
        oCollection.Clear()
        oCollection = Nothing
        oDM.Dispose()
        oDM = Nothing
    End Sub

    Private Sub fill_gender()
        Dim oCollection As Collection
        Dim oDM As New gloStream.DiseaseManagement.Common.Criteria

        oCollection = oDM.Gender

        cmbGender.Items.Clear()
        For i As Int16 = 1 To oCollection.Count
            cmbGender.Items.Add(oCollection(i))
        Next
        oCollection.Clear()
        oCollection = Nothing
        oDM.Dispose()
        oDM = Nothing
    End Sub

    Private Sub fill_race()
        Dim oCollectection As Collection
        Dim oDM As New gloStream.DiseaseManagement.Common.Criteria
        oCollectection = oDM.Race
        cmbRace.Items.Clear()
        For i As Int16 = 1 To oCollectection.Count
            cmbRace.Items.Add(oCollectection(i))
        Next
        oCollectection.Clear()
        oCollectection = Nothing
        oDM.Dispose()
        oDM = Nothing
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
            oCollectection1.Clear()
            oCollectection1 = Nothing
            oDM.Dispose()
            oDM = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.Message)
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
            oCollectection.Clear()
            oCollectection = Nothing
            oDM.Dispose()
            oDM = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    'function for the data save into the database for add new or update.

    'Private Sub SaveCriteria_Old()
    '    Try
    '        Dim oCriteria As New gloStream.DiseaseManagement.Supporting.Criteria
    '        Dim oDM As New gloStream.DiseaseManagement.DiseaseManagement




    '        Dim _ResultCriteriaID As Int64 = 0

    '        strHeight = txtHeightMin.Text + "'" + txtHeightMinInch.Text + "''"
    '        strHeightMax = txtHeightMax.Text + "'" + txtHeightMaxInch.Text + "''"

    '        If txtName.Text = "" Then
    '            MessageBox.Show("Please Enter The Name", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
    '            Exit Sub
    '        End If

    '        If txtMessage.Text = "" Then
    '            MessageBox.Show("Please Enter The Message", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
    '            Exit Sub
    '        End If

    '        If cmbGender.Text = "" Then
    '            MessageBox.Show("Please Enter Gender", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
    '            Exit Sub
    '        End If

    '        If cmbAgeMin.Text = "" Then
    '            MessageBox.Show("Please Enter Minimum Age", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
    '            Exit Sub
    '        End If

    '        If cmbAgeMax.Text = "" Then
    '            MessageBox.Show("Please Enter Maximum Age", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
    '            Exit Sub
    '        End If

    '        '' 20070207
    '        ' check for the enterd State value for manual data entered
    '        For i As Integer = 0 To cmbState.Items.Count - 1
    '            If cmbState.Text.ToUpper = cmbState.Items(i).ToString.ToUpper Then
    '                cmbState.SelectedIndex = i
    '                Exit For
    '            End If
    '        Next
    '        ' check for the enterd Gender for manual data entered
    '        For i As Integer = 0 To cmbGender.Items.Count - 1
    '            If cmbGender.Text.ToUpper = cmbGender.Items(i).ToString.ToUpper Then
    '                cmbGender.SelectedIndex = i
    '                Exit For
    '            End If
    '        Next
    '        ' check for the enterd race value for manual data entered
    '        For i As Integer = 0 To cmbRace.Items.Count - 1
    '            If cmbRace.Text.ToUpper = cmbRace.Items(i).ToString.ToUpper Then
    '                cmbRace.SelectedIndex = i
    '                Exit For
    '            End If
    '        Next
    '        ' check for the enterd marital status for manual data entered
    '        For i As Integer = 0 To cmbMaritalSt.Items.Count - 1
    '            If cmbMaritalSt.Text.ToUpper = cmbMaritalSt.Items(i).ToString.ToUpper Then
    '                cmbMaritalSt.SelectedIndex = i
    '                Exit For
    '            End If
    '        Next
    '        ' check for the enterd Employee status value for manual data entered
    '        For i As Integer = 0 To cmbEmpStatus.Items.Count - 1
    '            If cmbEmpStatus.Text.ToUpper = cmbEmpStatus.Items(i).ToString.ToUpper Then
    '                cmbEmpStatus.SelectedIndex = i
    '                Exit For
    '            End If
    '        Next

    '        ' check for the enterd Employee Age Minimum value for manual data entered
    '        For i As Integer = 0 To cmbAgeMin.Items.Count - 1
    '            If cmbAgeMin.Text.ToUpper = cmbAgeMin.Items(i).ToString.ToUpper Then
    '                cmbAgeMin.SelectedIndex = i
    '                Exit For
    '            End If
    '        Next

    '        ' check for the enterd Employee  Age Maximum value for manual data entered
    '        For i As Integer = 0 To cmbAgeMax.Items.Count - 1
    '            If cmbAgeMax.Text.ToUpper = cmbAgeMax.Items(i).ToString.ToUpper Then
    '                cmbAgeMax.SelectedIndex = i
    '                Exit For
    '            End If
    '        Next

    '        ' minimum maximum Age check
    '        If Val(cmbAgeMin.Text) > 0 Then
    '            If MinMaxValidator(Trim(cmbAgeMin.Text), Trim(cmbAgeMax.Text)) = False Then
    '                MessageBox.Show("Please check the Minimum and Maximum values for age.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '                cmbAgeMax.Focus()
    '                Exit Sub
    '            End If
    '        Else
    '            cmbAgeMin.Text = ""
    '        End If


    '        If Val(cmbAgeMin.Text) <= 0 Or Val(cmbAgeMin.Text) >= 125 Then
    '            MessageBox.Show("Please Enter Minimum Age(1 - 124).", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
    '            cmbAgeMin.Focus()
    '            Exit Sub
    '        End If

    '        If Val(cmbAgeMax.Text) <= 0 Or Val(cmbAgeMax.Text) >= 125 Then
    '            MessageBox.Show("Please Enter Maximum Age(1 - 124).", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
    '            cmbAgeMax.Focus()
    '            Exit Sub
    '        End If

    '        '' 20070208
    '        'Dim blnGuideLineExists As Boolean = False
    '        'With trOrderInfo
    '        '    For j As Integer = 0 To .GetNodeCount(False) - 1
    '        '        'oGideLines = New gloStream.DiseaseManagement.Supporting.ItemDetails
    '        '        'oGideLines = oDM.Guidelines(trvGuideLineRight.Nodes(j).Text)
    '        '        For i As Int16 = 0 To .Nodes(j).GetNodeCount(False) - 1  'oGideLines.Count
    '        '            If .Nodes(j).Nodes(i).Checked = True Then
    '        '                blnGuideLineExists = True
    '        '                Exit For
    '        '            End If
    '        '            'oNode = New TreeNode
    '        '            'With oNode

    '        '            '    '.Text = oGideLines(i).Description
    '        '            '    ' .Tag = oGideLines(i).ID
    '        '            'End With
    '        '            '.Nodes(j).Nodes.Add(oNode)
    '        '            'oNode = Nothing 
    '        '        Next

    '        '        If blnGuideLineExists = True Then
    '        '            Exit For
    '        '        End If
    '        '        'oGideLines = Nothing                   
    '        '    Next

    '        '    If blnGuideLineExists = False Then
    '        '        MessageBox.Show("Select atleast one guideline for the patient.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
    '        '        Exit Sub
    '        '    End If
    '        'End With
    '        ''

    '        If blnModify = False Then
    '            If oDM.IsExists(txtName.Text.Replace("'", "''")) = True Then
    '                MessageBox.Show("Please Enter another Name, Name already exist", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
    '                Exit Sub
    '            End If
    '        Else
    '            If UCase(txtName.Text.Trim) <> UCase(m_CriteriaName) Then
    '                If oDM.IsExists(txtName.Text.Replace("'", "''")) = True Then
    '                    MessageBox.Show("Please Enter another Name, Name already exist", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
    '                    Exit Sub
    '                End If
    '            End If

    '        End If

    '        With oCriteria
    '            .Name = txtName.Text.Trim
    '            If Not cmbAgeMin.SelectedItem Is Nothing Then
    '                .AgeMinimum = cmbAgeMin.SelectedItem
    '            End If
    '            If Not cmbAgeMax.SelectedItem Is Nothing Then
    '                .AgeMaximum = cmbAgeMax.SelectedItem
    '            End If
    '            .City = txtCity.Text
    '            If Not cmbGender.SelectedItem Is Nothing Then
    '                .Gender = cmbGender.SelectedItem
    '            End If
    '            If Not cmbState.SelectedItem Is Nothing Then
    '                .State = cmbState.SelectedItem
    '            End If
    '            If Not cmbRace.SelectedItem Is Nothing Then
    '                .Race = cmbRace.SelectedItem
    '            End If
    '            .Zip = txtZip.Text
    '            If Not cmbMaritalSt.SelectedItem Is Nothing Then
    '                .MaritalStatus = cmbMaritalSt.SelectedItem
    '            End If
    '            If Not cmbEmpStatus.SelectedItem Is Nothing Then
    '                .EmployeeStatus = cmbEmpStatus.SelectedItem
    '            End If

    '            '.HeightMinimum = CDbl(Val(txtHeightMin.Text.Trim))
    '            '.HeightMaximum = CDbl(Val(txtHeightMax.Text.Trim))
    '            ' changes done by Bipin ON 22/01/2007 for the date format change in to the ft and inch
    '            .HeightMinimum = strHeight
    '            .HeightMaximum = strHeightMax
    '            .BPSittingMinimum = CDbl(Val(txtBPsettingMin.Text.Trim))
    '            .BPSittingMaximum = CDbl(Val(txtBPsettingMax.Text.Trim))
    '            oCriteria.WeightMinimum = CDbl(Val(txtWeightMin.Text.Trim))
    '            .WeightMaximum = CDbl(Val(txtWeightMax.Text.Trim))
    '            .BPStandingMinimum = CDbl(Val(txtBPstandingMin.Text.Trim))
    '            .BPStandingMaximum = CDbl(Val(txtBPstandingMax.Text.Trim))
    '            .TempratureMinumum = CDbl(Val(txtTemperatureMin.Text.Trim))
    '            .TempratureMaximum = CDbl(Val(txtTemperatureMax.Text.Trim))
    '            .PulseMinimum = CDbl(Val(txtPulseMin.Text.Trim))
    '            .PulseMaximum = CDbl(Val(txtPulseMax.Text.Trim))
    '            .BMIMinimum = CDbl(Val(txtBMImin.Text.Trim))
    '            .BMIMaximum = CDbl(Val(txtBMImax.Text.Trim))
    '            .PulseOXMinimum = CDbl(Val(txtPulseOXmin.Text.Trim))
    '            .PulseOXMaximum = CDbl(Val(txtPulseOXmax.Text.Trim))
    '            .DisplayMessage = txtMessage.Text.Trim
    '        End With

    '        'data for History
    '        ''COMMENT BY SUDHIR 20090302 - UI CHANGED FOR HISTORY, NOW ITS LIKE DRUGS 
    '        'For i As Integer = 0 To trvHistoryRight.GetNodeCount(False) - 1
    '        '    For j As Integer = 0 To trvHistoryRight.Nodes(i).GetNodeCount(False) - 1
    '        '        If trvHistoryRight.Nodes(i).Nodes(j).Checked = True Then
    '        '            Dim strHistoryID As String() = Split(trvHistoryRight.Nodes(i).Nodes(j).Tag, "|")
    '        '            If strHistoryID.Length = 2 Then
    '        '                Dim objNewMember As New gloStream.DiseaseManagement.Supporting.HistoryItem
    '        '                objNewMember.ID = strHistoryID.GetValue(0)
    '        '                objNewMember.CategoryID = strHistoryID.GetValue(1)
    '        '                oCriteria.Histories.Add(objNewMember)
    '        '            End If

    '        '        End If
    '        '    Next
    '        'Next

    '        Dim oHistoryItem As gloStream.DiseaseManagement.Supporting.HistoryItem
    '        For Each CategoryNode As TreeNode In trvSelectedHistory.Nodes
    '            For Each HistoryNode As myTreeNode In CategoryNode.Nodes
    '                oHistoryItem = New gloStream.DiseaseManagement.Supporting.HistoryItem
    '                oHistoryItem.ID = HistoryNode.Key
    '                oHistoryItem.Name = HistoryNode.Text
    '                oHistoryItem.CategoryID = CategoryNode.Tag
    '                oHistoryItem.CategoryName = CategoryNode.Text
    '                oCriteria.Histories.Add(oHistoryItem)
    '                oHistoryItem = Nothing
    '            Next
    '        Next

    '        ''Comment By Sudhir 20090218 - As Interface for Drugs Modified like EMR40 SP2
    '        '' data for drugs
    '        'For i As Integer = 0 To trvDrgs.GetNodeCount(False) - 1
    '        '    If trvDrgs.Nodes(i).Checked = True Then
    '        '        oCriteria.Drugs.Add(trvDrgs.Nodes(i).Tag)
    '        '    End If
    '        'Next

    '        For i As Integer = 0 To trvSelectedDrugs.Nodes.Count - 1
    '            oCriteria.Drugs.Add(trvSelectedDrugs.Nodes(i).Tag)
    '        Next
    '        ''END SUDHIR

    '        'data for ICD9 
    '        ''For i As Integer = 0 To trvICD9.GetNodeCount(False) - 1
    '        ''    If trvICD9.Nodes(i).Checked = True Then
    '        ''        oCriteria.ICD9s.Add(trvICD9.Nodes(i).Tag)
    '        ''    End If
    '        ''Next

    '        'data for CPT treeview
    '        ''For i As Integer = 0 To trvCPT.GetNodeCount(False) - 1
    '        ''    If trvCPT.Nodes(i).Checked = True Then
    '        ''        oCriteria.CPTs.Add(trvCPT.Nodes(i).Tag)
    '        ''    End If
    '        ''Next

    '        ' RadiologyLAB
    '        For i As Integer = 1 To c1Labs.Rows.Count - 1
    '            Dim _TestCell As String = c1Labs.GetData(i, COL_IDENTITY) & ""
    '            If Mid(_TestCell, 1, 1) = "T" Then
    '                If c1Labs.GetCellCheck(i, COL_NAME) = C1.Win.C1FlexGrid.CheckEnum.Checked Then
    '                    oCriteria.Labs.Add(c1Labs.GetData(i, COL_GROUPNO), c1Labs.GetData(i, COL_ID), c1Labs.GetData(i, COL_MINVALUE), c1Labs.GetData(i, COL_MAXVALUE))
    '                End If
    '            End If
    '        Next

    '        'Lab Module  ' Enhancement - 02/08/2007
    '        For i As Integer = 1 To C1LabResult.Rows.Count - 1
    '            If C1LabResult.GetCellCheck(i, COL_TestName) = C1.Win.C1FlexGrid.CheckEnum.Checked Then
    '                With C1LabResult
    '                    oCriteria.LabModuleTests.Add(.GetData(i, COL_TestID), .GetData(i, COL_ResultID), .GetData(i, COL_TestName), gloStream.DiseaseManagement.Supporting.enumTestModuleResultValueType.None, 0, .GetData(i, COL_Operator), .GetData(i, COL_ResultValue1), .GetData(i, COL_ResultValue2))
    '                End With
    '            End If
    '        Next

    '        'data for GuideLines treeview
    '        For i As Integer = 0 To trOrderInfo.GetNodeCount(False) - 1
    '            For j As Integer = 0 To trOrderInfo.Nodes(i).GetNodeCount(False) - 1
    '                'If trOrderInfo.Nodes(i).Nodes(j).Checked = True Then
    '                '    oCriteria.Guidelines.Add(trOrderInfo.Nodes(i).Nodes(j).Tag)
    '                'End If
    '                'If trOrderInfo.Nodes(i).Text = "Labs" Then

    '                'End If
    '                If trOrderInfo.Nodes(i).Nodes.Item(j).Nodes.Count > 0 Then
    '                    For k As Integer = 0 To trOrderInfo.Nodes(i).Nodes.Item(j).Nodes.Count - 1
    '                        If trOrderInfo.Nodes(i).Nodes.Item(j).Text = "Labs" Then
    '                            Dim lst As New myList
    '                            lst.Value = trOrderInfo.Nodes(i).Nodes.Item(j).Nodes.Item(k).Text
    '                            lst.Index = trOrderInfo.Nodes(i).Nodes.Item(j).Nodes.Item(k).Tag
    '                            oCriteria.LabOrders.Add(lst) '(trOrderInfo.Nodes(i).Nodes.Item(j).Nodes.Item(k).Text, trOrderInfo.Nodes(i).Nodes.Item(j).Nodes.Item(k).Tag)
    '                        ElseIf trOrderInfo.Nodes(i).Nodes.Item(j).Text = "Orders" Then
    '                            Dim lst As New myList
    '                            lst.Value = trOrderInfo.Nodes(i).Nodes.Item(j).Nodes.Item(k).Text
    '                            lst.Index = trOrderInfo.Nodes(i).Nodes.Item(j).Nodes.Item(k).Tag
    '                            oCriteria.RadiologyOrders.Add(lst) '(trOrderInfo.Nodes(i).Nodes.Item(j).Nodes.Item(k).Text, trOrderInfo.Nodes(i).Nodes.Item(j).Nodes.Item(k).Tag)
    '                        ElseIf trOrderInfo.Nodes(i).Nodes.Item(j).Text = "Referrals" Then
    '                            Dim lst As New myList
    '                            lst.Value = trOrderInfo.Nodes(i).Nodes.Item(j).Nodes.Item(k).Text
    '                            lst.Index = trOrderInfo.Nodes(i).Nodes.Item(j).Nodes.Item(k).Tag
    '                            oCriteria.Referrals.Add(lst) '(trOrderInfo.Nodes(i).Nodes.Item(j).Nodes.Item(k).Text, trOrderInfo.Nodes(i).Nodes.Item(j).Nodes.Item(k).Tag)
    '                        ElseIf trOrderInfo.Nodes(i).Nodes.Item(j).Text = "Guidelines" Then
    '                            Dim lst As New myList
    '                            lst.Value = trOrderInfo.Nodes(i).Nodes.Item(j).Nodes.Item(k).Text
    '                            lst.Index = trOrderInfo.Nodes(i).Nodes.Item(j).Nodes.Item(k).Tag
    '                            oCriteria.Guidelines.Add(lst) '(trOrderInfo.Nodes(i).Nodes.Item(j).Nodes.Item(k).Text, trOrderInfo.Nodes(i).Nodes.Item(j).Nodes.Item(k).Tag)
    '                        ElseIf trOrderInfo.Nodes(i).Nodes.Item(j).Text = "Rx" Then
    '                            Dim lst As New myList
    '                            lst.Value = trOrderInfo.Nodes(i).Nodes.Item(j).Nodes.Item(k).Text
    '                            lst.Index = trOrderInfo.Nodes(i).Nodes.Item(j).Nodes.Item(k).Tag
    '                            oCriteria.RxDrugs.Add(lst) '(trOrderInfo.Nodes(i).Nodes.Item(j).Nodes.Item(k).Text, trOrderInfo.Nodes(i).Nodes.Item(j).Nodes.Item(k).Tag)
    '                        End If

    '                    Next

    '                End If

    '            Next
    '        Next

    '        'ckech for the flag for 'update' or 'add new'
    '        '////////////////////ADD CODE HERE - PRAMOD ///////////////////////
    '        If Not blnModify Then
    '            _ResultCriteriaID = oDM.Add(oCriteria)
    '        Else
    '            _ResultCriteriaID = oDM.Modify(m_CriteriaId, oCriteria)
    '        End If

    '        '' SUDHIR 20090309 - TO ADD NEW CRITERIA FOR HEALTH PLAN
    '        'If blnIsPatientCriteria = True And _ResultCriteriaID > 0 Then
    '        '    SavePatientCriteria(oCriteria, _ResultCriteriaID)
    '        'End If
    '        '' ''

    '        'clear all teh fields for the new entry
    '        If _ResultCriteriaID > 0 Then ' check for result flag

    '            'code commented by sarika on 5th nov 07
    '            'code commented bcoz after save was clicked it the form was not closed
    '            'If _SaveFlag = True Then ' check for save flag
    '            '    txtName.Text = ""
    '            '    txtMessage.Text = ""
    '            '    cmbAgeMin.Text = ""
    '            '    cmbAgeMax.Text = ""
    '            '    txtCity.Text = ""
    '            '    cmbGender.Text = ""
    '            '    cmbState.Text = ""
    '            '    cmbRace.Text = ""
    '            '    txtZip.Text = ""
    '            '    cmbMaritalSt.Text = ""
    '            '    cmbEmpStatus.Text = ""
    '            '    txtHeightMin.Text = ""
    '            '    txtHeightMinInch.Text = ""
    '            '    txtHeightMax.Text = ""
    '            '    txtHeightMaxInch.Text = ""
    '            '    txtBPsettingMin.Text = ""
    '            '    txtBPsettingMax.Text = ""
    '            '    txtWeightMin.Text = ""
    '            '    txtWeightMax.Text = ""
    '            '    txtBPstandingMin.Text = ""
    '            '    txtBPstandingMax.Text = ""
    '            '    txtTemperatureMin.Text = ""
    '            '    txtTemperatureMax.Text = ""
    '            '    txtPulseMin.Text = ""
    '            '    txtPulseMax.Text = ""
    '            '    txtBMImin.Text = ""
    '            '    txtBMImax.Text = ""
    '            '    txtPulseOXmin.Text = ""
    '            '    txtPulseOXmax.Text = ""
    '            '    HidenShow(tpDemographics)

    '            '    For i As Integer = 0 To trvHistoryRight.GetNodeCount(False) - 1
    '            '        trvHistoryRight.Nodes(i).Checked = False
    '            '        For j As Integer = 0 To trvHistoryRight.Nodes(i).GetNodeCount(False) - 1
    '            '            trvHistoryRight.Nodes(i).Nodes(j).Checked = False
    '            '        Next
    '            '    Next

    '            '    For i As Integer = 0 To trvICD9.GetNodeCount(False) - 1
    '            '        trvICD9.Nodes(i).Checked = False
    '            '    Next

    '            '    For i As Integer = 0 To trvDrgs.GetNodeCount(False) - 1
    '            '        trvDrgs.Nodes(i).Checked = False
    '            '    Next

    '            '    For i As Integer = 0 To trvCPT.GetNodeCount(False) - 1
    '            '        trvCPT.Nodes(i).Checked = False
    '            '    Next

    '            '    For i As Integer = 0 To trvGuideLineRight.GetNodeCount(False) - 1
    '            '        trvGuideLineRight.Nodes(i).Checked = False
    '            '        For j As Integer = 0 To trvGuideLineRight.Nodes(i).GetNodeCount(False) - 1
    '            '            trvGuideLineRight.Nodes(i).Nodes(j).Checked = False
    '            '        Next
    '            '    Next

    '            '    '// Remark
    '            '    'For i As Integer = 0 To trvLabs.GetNodeCount(False) - 1
    '            '    '    trvLabs.Nodes(i).Checked = False
    '            '    '    For j As Integer = 0 To trvLabs.Nodes(i).GetNodeCount(False) - 1
    '            '    '        trvLabs.Nodes(i).Nodes(j).Checked = False
    '            '    '        For k As Integer = 0 To trvLabs.Nodes(i).Nodes(j).GetNodeCount(False) - 1
    '            '    '            trvLabs.Nodes(i).Nodes(j).Nodes(k).Checked = False
    '            '    '        Next
    '            '    '    Next
    '            '    'Next

    '            'Else
    '            '    Me.Close()
    '            'End If


    '            'code added by sarika 5th nov 07
    '            'record would be saved/modified and the form will be closed after save button is clicked
    '            Me.Close()
    '        End If
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub

    Private Sub SaveCriteria()
        Dim oDM As New gloStream.DiseaseManagement.DiseaseManagement
        Dim oCriteria As New gloStream.DiseaseManagement.Supporting.Criteria
        Dim oOtherDetails As New gloStream.DiseaseManagement.Supporting.OtherDetails
        Try





            If oDM.IsExists(txtName.Text.Replace("'", "''"), blnModify, m_CriteriaId) = True Then
                MessageBox.Show("Please enter another name, name already exist.  ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                _IsValid = False
                Exit Sub
            End If





            _IsValid = True

            Dim _ResultCriteriaID As Int64 = 0

            strHeight = txtHeightMin.Text + "'" + txtHeightMinInch.Text + "''"
            strHeightMax = txtHeightMax.Text + "'" + txtHeightMaxInch.Text + "''"

            If Trim(txtName.Text) = "" Then
                MessageBox.Show("Please enter the name.  ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                _IsValid = False
                txtName.Focus()
                Exit Sub
            End If

            If Trim(txtMessage.Text) = "" Then
                MessageBox.Show("Please enter the message.  ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                _IsValid = False
                txtMessage.Focus()
                Exit Sub
            End If

            If cmbGender.Text = "" Then
                'Code Added by Mayuri:20091107
                'To Fix issue:# 4813
                btnDemographics_Click(Nothing, Nothing)
                'End code Added by Mayuri:20091107
                MessageBox.Show("Please enter gender.  ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                _IsValid = False
                cmbGender.Focus()
                Exit Sub
            End If

            If cmbAgeMin.Text = "" Then
                'Code Added by Mayuri:20091107
                'To Fix issue:# 4813
                btnDemographics_Click(Nothing, Nothing)
                'End code Added by Mayuri:20091107
                MessageBox.Show("Please enter minimum age.  ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                _IsValid = False
                cmbAgeMin.Focus()
                Exit Sub
            End If

            If cmbAgeMax.Text = "" Then
                'Code Added by Mayuri:20091107
                'To Fix issue:# 4813
                btnDemographics_Click(Nothing, Nothing)
                'End code Added by Mayuri:20091107
                MessageBox.Show("Please enter maximum age.  ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                _IsValid = False
                cmbAgeMax.Focus()
                Exit Sub
            End If

            '' 20070207
            ' check for the enterd State value for manual data entered
            For i As Integer = 0 To cmbState.Items.Count - 1
                If cmbState.Text.ToUpper = cmbState.Items(i).ToString.ToUpper Then
                    cmbState.SelectedIndex = i
                    Exit For
                End If
            Next
            ' check for the enterd Gender for manual data entered
            For i As Integer = 0 To cmbGender.Items.Count - 1
                If cmbGender.Text.ToUpper = cmbGender.Items(i).ToString.ToUpper Then
                    cmbGender.SelectedIndex = i
                    Exit For
                End If
            Next
            ' check for the enterd race value for manual data entered
            For i As Integer = 0 To cmbRace.Items.Count - 1
                If cmbRace.Text.ToUpper = cmbRace.Items(i).ToString.ToUpper Then
                    cmbRace.SelectedIndex = i
                    Exit For
                End If
            Next
            ' check for the enterd marital status for manual data entered
            For i As Integer = 0 To cmbMaritalSt.Items.Count - 1
                If cmbMaritalSt.Text.ToUpper = cmbMaritalSt.Items(i).ToString.ToUpper Then
                    cmbMaritalSt.SelectedIndex = i
                    Exit For
                End If
            Next
            ' check for the enterd Employee status value for manual data entered
            For i As Integer = 0 To cmbEmpStatus.Items.Count - 1
                If cmbEmpStatus.Text.ToUpper = cmbEmpStatus.Items(i).ToString.ToUpper Then
                    cmbEmpStatus.SelectedIndex = i
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
            'Condition changed by Mayuri:20091216-To validate for '0'-Bug ID-#4166
            'If Val(cmbAgeMin.Text) > 0 Then
            If Val(cmbAgeMin.Text) >= 0 Then
                If MinMaxValidator(Trim(cmbAgeMin.Text), Trim(cmbAgeMax.Text)) = False Then
                    'Code Added by Mayuri:20091107
                    'To Fix issue:# 4813
                    btnDemographics_Click(Nothing, Nothing)
                    'End code Added by Mayuri:20091107
                    MessageBox.Show("Please check the minimum and maximum values for age.  ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    cmbAgeMax.Focus()
                    _IsValid = False
                    Exit Sub
                End If
            Else
                cmbAgeMin.Text = ""
            End If


            If Val(cmbAgeMin.Text) < 0 Or Val(cmbAgeMin.Text) >= 125 Then
                'Code Added by Mayuri:20091107
                'To Fix issue:# 4813
                btnDemographics_Click(Nothing, Nothing)
                'End code Added by Mayuri:20091107
                MessageBox.Show("Please enter minimum age(0 - 124).  ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                cmbAgeMin.Focus()
                _IsValid = False
                Exit Sub
            End If

            If Val(cmbAgeMax.Text) < 0 Or Val(cmbAgeMax.Text) >= 125 Then
                'Code Added by Mayuri:20091107
                'To Fix issue:# 4813
                btnDemographics_Click(Nothing, Nothing)
                'End code Added by Mayuri:20091107
                MessageBox.Show("Please enter maximum age(0 - 124).  ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                cmbAgeMax.Focus()
                _IsValid = False
                Exit Sub
            End If

            With oCriteria
                .Name = txtName.Text.Trim
                If Not cmbAgeMin.SelectedItem Is Nothing Then
                    .AgeMinimum = CType(cmbAgeMin.SelectedItem & "." & cmbAgeMinMnth.SelectedItem, Double)
                End If
                If Not cmbAgeMax.SelectedItem Is Nothing Then
                    .AgeMaximum = CType(cmbAgeMax.SelectedItem & "." & cmbAgeMaxMnth.SelectedItem, Double)
                End If
                .City = txtCity.Text
                If Not cmbGender.SelectedItem Is Nothing Then
                    .Gender = cmbGender.SelectedItem
                End If
                If Not cmbState.SelectedItem Is Nothing Then
                    .State = cmbState.SelectedItem
                End If
                If Not cmbRace.SelectedItem Is Nothing Then
                    .Race = cmbRace.SelectedItem
                End If
                .Zip = txtZip.Text
                If Not cmbMaritalSt.SelectedItem Is Nothing Then
                    .MaritalStatus = cmbMaritalSt.SelectedItem
                End If
                If Not cmbEmpStatus.SelectedItem Is Nothing Then
                    .EmployeeStatus = cmbEmpStatus.SelectedItem
                End If

                '.HeightMinimum = CDbl(Val(txtHeightMin.Text.Trim))
                '.HeightMaximum = CDbl(Val(txtHeightMax.Text.Trim))
                ' changes done by Bipin ON 22/01/2007 for the date format change in to the ft and inch
                .HeightMinimum = strHeight
                .HeightMaximum = strHeightMax
                .BPSittingMinimum = CDbl(Val(txtBPsettingMin.Text.Trim))
                .BPSittingMaximum = CDbl(Val(txtBPsettingMax.Text.Trim))
                oCriteria.WeightMinimum = CDbl(Val(txtWeightMin.Text.Trim))
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

            'If gblnSMDBSetting = True And gstrSMHistory.Trim() <> "" Then
            '    For Each CategoryNode As TreeNode In trvselectedhist.Nodes
            '        For Each HistoryNode As TreeNode In CategoryNode.Nodes
            '            'oCriteria = New gloStream.DiseaseManagement.Supporting.OtherDetails
            '            ''Sandip Darade 20090317
            '            Dim oOtherDetail1 As New gloStream.DiseaseManagement.Supporting.OtherDetail
            '            oOtherDetail1.CategoryName = CategoryNode.Text
            '            oOtherDetail1.DetailType = Supporting.enumDetailType.History.GetHashCode()
            '            oOtherDetail1.ItemName = HistoryNode.Text
            '            oOtherDetails.Add(oOtherDetail1)
            '        Next
            '    Next
            'Else
            For Each CategoryNode As TreeNode In trvSelectedHistory.Nodes
                For Each HistoryNode As myTreeNode In CategoryNode.Nodes
                    'oCriteria = New gloStream.DiseaseManagement.Supporting.OtherDetails
                    ''Sandip Darade 20090317
                    Dim oOtherDetail1 As New gloStream.DiseaseManagement.Supporting.OtherDetail
                    oOtherDetail1.CategoryName = CategoryNode.Text
                    oOtherDetail1.DetailType = Supporting.enumDetailType.History.GetHashCode()
                    oOtherDetail1.ItemName = HistoryNode.Text
                    oOtherDetails.Add(oOtherDetail1)
                Next
            Next

            'End If





            For i As Integer = 0 To trvSelectedDrugs.Nodes.Count - 1

                ''Sandip Darade 20090317
                Dim oOtherDetail1 As New gloStream.DiseaseManagement.Supporting.OtherDetail
                Dim thisNode As myTreeNode = CType(trvSelectedDrugs.Nodes(i), myTreeNode)
                'oOtherDetail1.CategoryName = trvSelectedDrugs.Nodes(i).Text
                oOtherDetail1.CategoryName = thisNode.DrugName
                'Code Added by Mayuri:20091012
                'To display DrugName ,Dosage alongwith DrugForm
                'to check whether DrugForm is blank


                If thisNode.DrugForm.ToString.Trim <> "" Then
                    'If dtdrugs.Rows.Item(i)(3) = "" Then
                    'oOtherDetail1.ItemName = thisNode.Dosage & thisNode.DrugForm
                    ' Else
                    '  oOtherDetail1.ItemName = thisNode.Dosage & " - " & thisNode.DrugForm
                    'End If
                    'Else
                    If thisNode.Dosage.Trim <> "" Then
                        oOtherDetail1.ItemName = thisNode.Dosage & " - " & thisNode.DrugForm
                    Else
                        oOtherDetail1.ItemName = thisNode.DrugForm
                    End If
                Else
                    oOtherDetail1.ItemName = thisNode.Dosage
                    '& " - " & thisNode.DrugForm
                End If
                'End Code added by Mayuri:20091012
                oOtherDetail1.DetailType = Supporting.enumDetailType.Medication.GetHashCode()
                'oOtherDetail1.ItemName = trvSelectedDrugs.Nodes(i).Tag

                'code added by pradeep to save NDCCode on20100812
                oOtherDetail1.Result1 = thisNode.NDCCode

                oOtherDetails.Add(oOtherDetail1)
            Next

            'data for ICD9 
            'For i As Integer = 0 To trvICD9.GetNodeCount(False) - 1

            '    If trvICD9.Nodes(i).Checked = True Then
            '        ''Sandip Darade 20090317

            '        Dim oOtherDetail1 As New gloStream.DiseaseManagement.Supporting.OtherDetail
            '        oOtherDetail1.DetailType = Supporting.enumDetailType.ICD9.GetHashCode()

            '        Dim _arrSpliter As String()
            '        _arrSpliter = trvICD9.Nodes(i).Text.Split("-") ''Split the string 

            '        If _arrSpliter(0).Length > 0 Then
            '            Dim CategoryName As String = _arrSpliter(0) ''Code
            '            oOtherDetail1.CategoryName = CategoryName

            '        End If

            '        If _arrSpliter(1).Length > 0 Then
            '            Dim ItemName As String = _arrSpliter(1) ''Description 
            '            oOtherDetail1.ItemName = ItemName
            '        End If


            '        oOtherDetails.Add(oOtherDetail1)
            '    End If
            'Next

            'data for CPT treeview
            'For i As Integer = 0 To trvCPT.GetNodeCount(False) - 1
            '    If trvCPT.Nodes(i).Checked = True Then
            '        ''Sandip Darade 20090317
            '        Dim oOtherDetail1 As New gloStream.DiseaseManagement.Supporting.OtherDetail
            '        oOtherDetail1.DetailType = Supporting.enumDetailType.CPT.GetHashCode()

            '        Dim _arrSpliter As String()
            '        _arrSpliter = trvCPT.Nodes(i).Text.Split("-") ''Split the string 

            '        If _arrSpliter(0).Length > 0 Then
            '            Dim CategoryName As String = _arrSpliter(0) ''Code
            '            oOtherDetail1.CategoryName = CategoryName

            '        End If

            '        If _arrSpliter(1).Length > 0 Then
            '            Dim ItemName As String = _arrSpliter(1) ''Description 
            '            oOtherDetail1.ItemName = ItemName
            '        End If

            '        oOtherDetails.Add(oOtherDetail1)
            '    End If
            'Next
            For i As Integer = 0 To trvselectedCPT.Nodes.Count - 1

                ''Sandip Darade 20090317
                Dim oOtherDetail1 As New gloStream.DiseaseManagement.Supporting.OtherDetail
                oOtherDetail1.DetailType = Supporting.enumDetailType.CPT.GetHashCode()
                Dim thisNode As myTreeNode = CType(trvselectedCPT.Nodes(i), myTreeNode)
                oOtherDetail1.CategoryName = thisNode.Tag
                oOtherDetail1.ItemName = thisNode.DrugName '' CPT DESCRIPTION ''

                '' SUDHIR 20090623 ''
                'Dim _arrSpliter As String()
                '_arrSpliter = trvselectedCPT.Nodes(i).Text.Split("-") ''Split the string 

                'If _arrSpliter(0).Length > 0 Then
                '    Dim CategoryName As String = _arrSpliter(0) ''Code
                '    oOtherDetail1.CategoryName = CategoryName

                'End If

                ''If _arrSpliter(1).Length > 0 Then
                ''    Dim ItemName As String = _arrSpliter(1) ''Description 
                ''    oOtherDetail1.ItemName = ItemName
                ''End If

                'If trvselectedCPT.Nodes(i).Tag <> "" Then

                '    oOtherDetail1.ItemName = trvselectedCPT.Nodes(i).Tag ''Description  
                'End If
                oOtherDetails.Add(oOtherDetail1)
            Next

            For i As Integer = 0 To trvselecteICDs.Nodes.Count - 1

                ''Sandip Darade 20090317
                Dim oOtherDetail1 As New gloStream.DiseaseManagement.Supporting.OtherDetail
                oOtherDetail1.DetailType = Supporting.enumDetailType.ICD9.GetHashCode()
                Dim thisNode As myTreeNode = CType(trvselecteICDs.Nodes(i), myTreeNode)
                oOtherDetail1.CategoryName = thisNode.Tag
                oOtherDetail1.ItemName = thisNode.DrugName '' ICD9 DESCRIPTION ''

                '' SUDHIR 20090623 ''
                'Dim _arrSpliter As String()
                '_arrSpliter = trvselecteICDs.Nodes(i).Text.Split("-") ''Split the string 

                'If _arrSpliter(0).Length > 0 Then
                '    Dim CategoryName As String = _arrSpliter(0) ''Code
                '    oOtherDetail1.CategoryName = CategoryName

                'End If

                'If _arrSpliter(1).Length > 0 Then
                '    Dim ItemName As String = _arrSpliter(1) ''Description 
                '    oOtherDetail1.ItemName = ItemName
                'End If

                oOtherDetails.Add(oOtherDetail1)
            Next


            ' RadiologyLAB
            For i As Integer = 1 To c1Labs.Rows.Count - 1
                Dim _TestCell As String = c1Labs.GetData(i, COL_IDENTITY) & ""
                If Mid(_TestCell, 1, 1) = "T" Then
                    If c1Labs.GetCellCheck(i, COL_NAME) = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                        ''Sandip Darade 20090317
                        Dim oOtherDetail1 As New gloStream.DiseaseManagement.Supporting.OtherDetail
                        oOtherDetail1.ItemName = c1Labs.Rows(i).Node.Data
                        oOtherDetail1.OperatorName = c1Labs.Rows(i).Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.Parent).Data '' OPERATOR NAME AS GROUP NAME ''
                        oOtherDetail1.CategoryName = c1Labs.Rows(i).Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.Root).Data
                        'oOtherDetail1.Result1 = Convert.ToString(c1Labs.GetData(i, COL_MINVALUE))
                        'oOtherDetail1.Result2 = Convert.ToString(c1Labs.GetData(i, COL_MAXVALUE))
                        'oOtherDetail1.OperatorName = Convert.ToString(c1Labs.GetData(i, COL_Operator))
                        oOtherDetail1.DetailType = Supporting.enumDetailType.Order.GetHashCode()
                        oOtherDetails.Add(oOtherDetail1)
                    End If
                End If
            Next
            'Lab
            For i As Integer = 1 To C1LabResult.Rows.Count - 1

                If C1LabResult.GetCellCheck(i, COL_TestName) = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                    ''Sandip Darade 20090317
                    Dim oOtherDetail1 As New gloStream.DiseaseManagement.Supporting.OtherDetail
                    C1LabResult.Select()
                    oOtherDetail1.ItemName = C1LabResult.Rows(i).Node.Data
                    oOtherDetail1.CategoryName = Convert.ToString(C1LabResult.Rows(i).Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.Parent).Data)
                    oOtherDetail1.Result1 = Convert.ToString(C1LabResult.GetData(i, COL_ResultValue1))
                    oOtherDetail1.Result2 = Convert.ToString(C1LabResult.GetData(i, COL_ResultValue2))
                    oOtherDetail1.OperatorName = C1LabResult.GetData(i, COL_Operator)
                    oOtherDetail1.DetailType = Supporting.enumDetailType.Lab.GetHashCode()
                    oOtherDetails.Add(oOtherDetail1)

                End If
            Next

            'Lab Module  ' Enhancement - 02/08/2007


            'data for GuideLines treeview
            For i As Integer = 0 To trOrderInfo.GetNodeCount(False) - 1
                For j As Integer = 0 To trOrderInfo.Nodes(i).GetNodeCount(False) - 1
                    'If trOrderInfo.Nodes(i).Nodes(j).Checked = True Then
                    '    oCriteria.Guidelines.Add(trOrderInfo.Nodes(i).Nodes(j).Tag)
                    'End If
                    'If trOrderInfo.Nodes(i).Text = "Labs" Then

                    'End If
                    If trOrderInfo.Nodes(i).Nodes.Item(j).Nodes.Count > 0 Then
                        For k As Integer = 0 To trOrderInfo.Nodes(i).Nodes.Item(j).Nodes.Count - 1
                            Dim thisNode As myTreeNode = CType(trOrderInfo.Nodes(i).Nodes.Item(j).Nodes.Item(k), myTreeNode)
                            If trOrderInfo.Nodes(i).Nodes.Item(j).Text = "Labs" Then
                                Dim lst As New myList
                                lst.Value = thisNode.Text
                                lst.Index = thisNode.Tag
                                'sarika DM Denormalization
                                lst.ID = thisNode.Key
                                '--
                                oCriteria.LabOrders.Add(lst) '(trOrderInfo.Nodes(i).Nodes.Item(j).Nodes.Item(k).Text, trOrderInfo.Nodes(i).Nodes.Item(j).Nodes.Item(k).Tag)
                            ElseIf trOrderInfo.Nodes(i).Nodes.Item(j).Text = "Orders" Then
                                Dim lst As New myList
                                lst.Value = thisNode.Text
                                lst.Index = thisNode.Tag
                                'sarika DM Denormalization
                                lst.ID = thisNode.Key
                                '--

                                oCriteria.RadiologyOrders.Add(lst) '(trOrderInfo.Nodes(i).Nodes.Item(j).Nodes.Item(k).Text, trOrderInfo.Nodes(i).Nodes.Item(j).Nodes.Item(k).Tag)
                            ElseIf trOrderInfo.Nodes(i).Nodes.Item(j).Text = "Referrals" Then
                                Dim lst As New myList
                                lst.Value = thisNode.Text
                                lst.Index = thisNode.Tag
                                'sarika DM Denormalization
                                lst.ID = thisNode.Key
                                '--
                                oCriteria.Referrals.Add(lst) '(trOrderInfo.Nodes(i).Nodes.Item(j).Nodes.Item(k).Text, trOrderInfo.Nodes(i).Nodes.Item(j).Nodes.Item(k).Tag)
                            ElseIf trOrderInfo.Nodes(i).Nodes.Item(j).Text = "Guidelines" Then
                                Dim lst As New myList
                                lst.Value = thisNode.Text
                                lst.Index = thisNode.Tag
                                lst.DMTemplateName = thisNode.Text
                                lst.ID = thisNode.Tag
                                'get the template (description from the template_gallery table
                                'lst.DMTemplate = trOrderInfo.Nodes(i).Nodes.Item(j).Nodes.Item(k)
                                If Not IsNothing(thisNode.DMTemplate) Then
                                    lst.DMTemplate = thisNode.DMTemplate    ' GetTemplate(trOrderInfo.Nodes(i).Nodes.Item(j).Nodes.Item(k).Tag)
                                Else
                                    lst.DMTemplate = Nothing
                                End If

                                oCriteria.Guidelines.Add(lst) '(trOrderInfo.Nodes(i).Nodes.Item(j).Nodes.Item(k).Text, trOrderInfo.Nodes(i).Nodes.Item(j).Nodes.Item(k).Tag)
                            ElseIf trOrderInfo.Nodes(i).Nodes.Item(j).Text = "Rx" Then
                                Dim lst As New myList
                                lst.Value = thisNode.Text
                                lst.Index = thisNode.Tag
                                'sarika DM Denormalization
                                lst.ID = thisNode.Key
                                lst.DrugName = thisNode.DrugName
                                lst.Dosage = thisNode.Dosage
                                '--

                                'sarika DM Denormalization for Rx
                                lst.DrugForm = thisNode.DrugForm
                                lst.Route = thisNode.Route
                                lst.Duration = thisNode.Duration
                                lst.Frequency = thisNode.Frequency
                                lst.NDCCode = thisNode.NDCCode
                                lst.mpid = thisNode.mpid
                                lst.IsNarcotic = thisNode.IsNarcotics
                                lst.DrugQtyQualifier = thisNode.DrugQtyQualifier
                                '---

                                oCriteria.RxDrugs.Add(lst) '(trOrderInfo.Nodes(i).Nodes.Item(j).Nodes.Item(k).Text, trOrderInfo.Nodes(i).Nodes.Item(j).Nodes.Item(k).Tag)


                            ElseIf trOrderInfo.Nodes(i).Nodes.Item(j).Text = "IM" Then
                                '''''''''Chetan integrated on 09 OCT 2010  - for IM in DM Setup
                                Dim lst As New myList
                                lst.Value = thisNode.Text
                                lst.ID = thisNode.Key
                                lst.DrugForm = thisNode.DrugForm      'ConceptID
                                lst.Duration = thisNode.Duration      'DescriptionID
                                lst.Frequency = thisNode.Frequency    'TradEName                    
                                lst.DrugQtyQualifier = thisNode.DrugQtyQualifier
                                lst.Index = thisNode.Tag              ' IM ID
                                lst.Route = thisNode.Route              ' Orignal Vaccine name
                                lst.NDCCode = thisNode.NDCCode  '' Manufacturer
                                '---

                                oCriteria.IMlst.Add(lst)
                                '''''''''Chetan integrated on 09 OCT 2010 - for IM in DM Setup




                            End If

                        Next

                    End If

                Next
            Next



            '' chetan integrated for ProblemList


            For Each CategoryNode As TreeNode In trvselectedprob.Nodes
                For Each ProblemNode As TreeNode In CategoryNode.Nodes
                    'oCriteria = New gloStream.DiseaseManagement.Supporting.OtherDetails
                    ''Sandip Darade 20090317
                    Dim oOtherDetail1 As New gloStream.DiseaseManagement.Supporting.OtherDetail
                    oOtherDetail1.CategoryName = CategoryNode.Text
                    oOtherDetail1.DetailType = Supporting.enumDetailType.Problemlist.GetHashCode()
                    oOtherDetail1.ItemName = ProblemNode.Text
                    oOtherDetails.Add(oOtherDetail1)
                Next
            Next



            ''Sandip Darade 20090318
            ''Add Other detail 
            oCriteria.OtherDetails = oOtherDetails
            oDM.SaveCriteria(m_CriteriaId, oCriteria, LoadFirst)

            '' SUDHIR 20090309 - TO ADD NEW CRITERIA FOR HEALTH PLAN
            'If blnIsPatientCriteria = True And _ResultCriteriaID > 0 Then
            '    SavePatientCriteria(oCriteria, _ResultCriteriaID)
            'End If
            '' ''

            'clear all teh fields for the new entry
            If _ResultCriteriaID > 0 Then ' check for result flag


                ' Me.Close()
                gloWord.WordDialogBoxBackgroundCloser.Close(Me)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
         
           

            If (IsNothing(oOtherDetails) = False) Then
                oOtherDetails.Dispose()
                oOtherDetails = Nothing
            End If
            If (IsNothing(oCriteria) = False) Then
                oCriteria.Dispose()
                oCriteria = Nothing
            End If
            If (IsNothing(oDM) = False) Then
                oDM.Dispose()
                oDM = Nothing
            End If

        End Try
    End Sub


    '' chetan added on 09-Oct-2010 for snomed data
    Private Sub filltrvfindProb(ByVal Search As String)
        Try


            ''Start :: Snow Made ConnectionString 
            oSnoMed = New gloSnoMed.ClsGeneral
            gstrSMDBConnstr = String.Empty
            If Not IsNothing(oSnoMed) Then
                gstrSMDBConnstr = GetHybridConnectionString(gstrSMDBServerName, gstrSMDBDatabaseName, gblnSMDBAuthen, gstrSMDBUserID, gstrSMDBPassWord)
                If gstrSMDBConnstr <> String.Empty Then
                    oSnoMed.IsConnect(gstrSMDBConnstr, GetConnectionString())
                End If
            End If
            oSnoMed.ConnectionString = gstrSMDBConnstr
            ''End :: Snow Made ConnectionString 

            'oSnoMed.IsConnect(gstrSMDBConnstr)
            'oSnoMed.ConnectionString = gstrSMDBConnstr

            ' oSnoMed.SearchSnomed_Old(Search, False, trvfinprob)
            If trvfinprob.Nodes.Count > 0 Then
                trvfinprob.SelectedNode = trvfinprob.Nodes(0)
                'trvfinprob.Focus()  'Added by kanchan on 20101112
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    '' chetan integrated on 11-Oct-2010 for snomed data
    Private Sub mnuDeleteProblem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuDeleteProblem.Click
        If Not IsNothing(trvselectedprob) Then
            If trvselectedprob.SelectedNode.Parent.Nodes.Count > 1 Then
                trvselectedprob.SelectedNode.Remove()
            Else
                trvselectedprob.SelectedNode.Remove()
            End If
        End If
    End Sub
    '' chetan added on 09-Oct-2010 for snomed data

    Private Sub fillProblemlistTreeHeader()
        Dim oNode As myTreeNode
        oNode = New myTreeNode
        oNode.Text = "ProblemList"
        oNode.Tag = "-1"
        oNode.ImageIndex = 0
        oNode.SelectedImageIndex = 0


        ''oNode.Nodes.Add(SelectedProbNode)
        trvselectedprob.Nodes.Add(oNode)

        oNode = Nothing
        'trvselectedprob.Sort()

    End Sub
    '' SUDHIR 20090309 - TO SAVE PATIENT CRITERIA FOR HEALTH PLAN , IF FORM OPENED FROM HEALTH PLAN.
    'Private Sub SavePatientCriteria(ByVal oCriteria As gloStream.DiseaseManagement.Supporting.Criteria, ByVal CriteriaID As Int64)
    '    Dim oDM As New clsDM_Template
    '    Dim arrList As New ArrayList
    '    Dim oList As myList
    '    Try
    '        '' LAB ORDER
    '        For i As Integer = 1 To oCriteria.LabOrders.Count
    '            oList = New myList
    '            oList.ID = CType(oCriteria.LabOrders.Item(i), myList).Index
    '            oList.Index = DiseaseManagement.TemplateCategoryID.Labs.GetHashCode
    '            arrList.Add(oList)
    '            oList = Nothing
    '        Next

    '        '' RADIOLOGY ORDER
    '        For i As Integer = 1 To oCriteria.RadiologyOrders.Count
    '            oList = New myList
    '            oList.ID = CType(oCriteria.RadiologyOrders.Item(i), myList).Index
    '            oList.Index = DiseaseManagement.TemplateCategoryID.Radiology.GetHashCode
    '            arrList.Add(oList)
    '            oList = Nothing
    '        Next

    '        '' REFFERALS
    '        For i As Integer = 1 To oCriteria.Referrals.Count
    '            oList = New myList
    '            oList.ID = CType(oCriteria.Referrals.Item(i), myList).Index
    '            oList.Index = DiseaseManagement.TemplateCategoryID.Referrals.GetHashCode
    '            arrList.Add(oList)
    '            oList = Nothing
    '        Next

    '        ''GUIDLINES
    '        For i As Integer = 1 To oCriteria.Guidelines.Count
    '            oList = New myList
    '            oList.ID = CType(oCriteria.Guidelines.Item(i), myList).Index
    '            oList.Index = DiseaseManagement.TemplateCategoryID.Guidelines.GetHashCode
    '            arrList.Add(oList)
    '            oList = Nothing
    '        Next

    '        ''RX DRUGS
    '        For i As Integer = 1 To oCriteria.RxDrugs.Count
    '            oList = New myList
    '            oList.ID = CType(oCriteria.RxDrugs.Item(i), myList).Index
    '            oList.Index = DiseaseManagement.TemplateCategoryID.Rx.GetHashCode
    '            arrList.Add(oList)
    '            oList = Nothing
    '        Next

    '        If arrList.Count > 0 Then
    '            oDM.Save_PatientCriteria(arrList, CriteriaID, m_PatientID, oCriteria.Name, oCriteria.DisplayMessage)
    '        End If

    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub

    '' '' 
    ' get data to update field

    
    Dim blnloadIm As Boolean = False '' use when loading immunization to add all value in IM node

    Public Sub Fill_EditCriteria(ByVal lCriteriaID As Long)
        Try


            Dim oCriteria As gloStream.DiseaseManagement.Supporting.Criteria
            Dim oDM As New gloStream.DiseaseManagement.DiseaseManagement

            oCriteria = oDM.GetCriteria(lCriteriaID, 0)

            If Not oCriteria Is Nothing Then
                With oCriteria
                    'Demographics & Vitals
                    txtName.Text = .Name

                    txtMessage.Text = .DisplayMessage

                    '.AgeMinimum.ToString("#0.00")
                    If .AgeMinimum.ToString.Contains(".") Then
                        Dim _age() As String = .AgeMinimum.ToString.Split(".")
                        cmbAgeMin.Text = _age(0) : SetCombiIndex(cmbAgeMin)
                        'cmbAgeMinMnth.Text = Format(CDbl((_age(1) / 10000) * 12), "#00") : SetCombiIndex(cmbAgeMinMnth)
                        Dim strage() As String
                        Dim strageMin As String

                        strageMin = GetValues(oCriteria.ID, True)
                        strage = strageMin.Split(".")
                        If (strage(1).ToString() = "10") Then
                            cmbAgeMinMnth.Text = "10"
                        Else
                            cmbAgeMinMnth.Text = Format(CDbl((CDbl("." & _age(1))) * 12), "#00")
                        End If
                    Else
                        cmbAgeMin.Text = .AgeMinimum : SetCombiIndex(cmbAgeMin)
                        cmbAgeMinMnth.Text = "00" : SetCombiIndex(cmbAgeMinMnth)
                    End If
                    If .AgeMaximum.ToString.Contains(".") Then
                        Dim _age() As String = .AgeMaximum.ToString.Split(".")
                        cmbAgeMax.Text = _age(0) : SetCombiIndex(cmbAgeMax)
                        'cmbAgeMaxMnth.Text = Format(CDbl((_age(1) / 10000) * 12), "#00") : SetCombiIndex(cmbAgeMaxMnth)
                        Dim strage() As String
                        Dim strageMax As String
                        strageMax = GetValues(oCriteria.ID, False)
                        strage = strageMax.Split(".")
                        If (strage(1).ToString() = "10") Then
                            cmbAgeMaxMnth.Text = "10"
                        Else
                            cmbAgeMaxMnth.Text = Format(CDbl((CDbl("." & _age(1))) * 12), "#00") : SetCombiIndex(cmbAgeMaxMnth)
                        End If
                    Else
                        cmbAgeMax.Text = .AgeMaximum : SetCombiIndex(cmbAgeMax)
                        cmbAgeMaxMnth.Text = "00" : SetCombiIndex(cmbAgeMaxMnth)
                    End If


                    txtCity.Text = .City
                    cmbGender.Text = .Gender : SetCombiIndex(cmbGender)
                    cmbState.Text = .State : SetCombiIndex(cmbState)
                    cmbRace.Text = .Race : SetCombiIndex(cmbRace)
                    txtZip.Text = .Zip
                    cmbMaritalSt.Text = .MaritalStatus : SetCombiIndex(cmbMaritalSt)
                    cmbEmpStatus.Text = .EmployeeStatus : SetCombiIndex(cmbEmpStatus)
                    '  txtHeightMin.Text = .HeightMinimum
                    '  txtHeightMax.Text = .HeightMaximum

                    ' changes done by Bipin On 22/01/2007 Date format change into ft and inch.
                    Dim arrHeight() As String
                    arrHeight = GetFtInch(.HeightMinimum)
                    txtHeightMin.Text = arrHeight(0)
                    txtHeightMinInch.Text = arrHeight(1)

                    '  Dim arrHeightMax() As String
                    arrHeight = GetFtInch(.HeightMaximum)
                    txtHeightMax.Text = arrHeight(0)
                    txtHeightMaxInch.Text = arrHeight(1)

                    If .BPSittingMinimum = 0.0 Then
                        txtBPsettingMin.Text = ""
                    Else
                        txtBPsettingMin.Text = .BPSittingMinimum
                    End If

                    If .BPSittingMaximum = 0.0 Then
                        txtBPsettingMax.Text = ""
                    Else
                        txtBPsettingMax.Text = .BPSittingMaximum
                    End If

                    If .WeightMinimum = 0.0 Then
                        txtWeightMin.Text = ""
                    Else
                        txtWeightMin.Text = .WeightMinimum
                    End If

                    If .WeightMaximum = 0.0 Then
                        txtWeightMax.Text = ""
                    Else
                        txtWeightMax.Text = .WeightMaximum
                    End If

                    If .BPStandingMinimum = 0.0 Then
                        txtBPstandingMin.Text = ""
                    Else
                        txtBPstandingMin.Text = .BPStandingMinimum
                    End If

                    If .BPStandingMaximum = 0.0 Then
                        txtBPstandingMax.Text = ""
                    Else
                        txtBPstandingMax.Text = .BPStandingMaximum
                    End If

                    If .TempratureMinumum = 0.0 Then
                        txtTemperatureMin.Text = ""
                    Else
                        txtTemperatureMin.Text = .TempratureMinumum
                    End If

                    If .TempratureMaximum = 0.0 Then
                        txtTemperatureMax.Text = ""
                    Else
                        txtTemperatureMax.Text = .TempratureMaximum
                    End If

                    If .PulseMinimum = 0.0 Then
                        txtPulseMin.Text = ""
                    Else
                        txtPulseMin.Text = .PulseMinimum
                    End If

                    If .PulseMaximum = 0.0 Then
                        txtPulseMax.Text = ""
                    Else
                        txtPulseMax.Text = .PulseMaximum
                    End If
                    If .BMIMinimum = 0.0 Then
                        txtBMImin.Text = ""
                    Else
                        txtBMImin.Text = .BMIMinimum
                    End If

                    If .BMIMaximum = 0.0 Then
                        txtBMImax.Text = ""
                    Else
                        txtBMImax.Text = .BMIMaximum
                    End If

                    If .PulseOXMinimum = 0.0 Then
                        txtPulseOXmin.Text = ""
                    Else
                        txtPulseOXmin.Text = .PulseOXMinimum
                    End If

                    If .PulseOXMaximum = 0.0 Then
                        txtPulseOXmax.Text = ""
                    Else
                        txtPulseOXmax.Text = .PulseOXMaximum
                    End If


                    ''Sandip Darade 20090317
                    ''Get  drugs,history,ICD9,CPT,Labs,Orders from otherdetails 
                    For i As Integer = 1 To oCriteria.OtherDetails.Count
                        Select Case (oCriteria.OtherDetails.Item(i).DetailType)



                            '' chetan added for problemlist
                            Case Supporting.enumDetailType.Problemlist

                                Dim pnode As myTreeNode = trvselectedprob.Nodes(0)
                                Dim myPrbNode As myTreeNode
                                myPrbNode = New myTreeNode
                                '  myDrugNode.Text = oCriteria.OtherDetails.Item(i).CategoryName & " - " & oCriteria.OtherDetails.Item(i).ItemName
                                myPrbNode.Text = oCriteria.OtherDetails.Item(i).ItemName

                                myPrbNode.Tag = oCriteria.OtherDetails.Item(i).ItemName
                                myPrbNode.DrugName = oCriteria.OtherDetails.Item(i).CategoryName
                                myPrbNode.Dosage = oCriteria.OtherDetails.Item(i).ItemName
                                pnode.Nodes.Add(myPrbNode)
                                trvselectedprob.Nodes.Clear()
                                trvselectedprob.Nodes.Add(pnode)
                                trvselectedprob.ExpandAll()




                            Case Supporting.enumDetailType.Medication
                                '' Get drugs from otherdetail

                                'If (oCriteria.OtherDetails.Item(i).DetailType = Supporting.enumDetailType.Medication.GetHashCode()) Then
                                Dim myDrugNode As myTreeNode
                                myDrugNode = New myTreeNode
                                myDrugNode.Text = oCriteria.OtherDetails.Item(i).CategoryName & " - " & oCriteria.OtherDetails.Item(i).ItemName
                                myDrugNode.Tag = oCriteria.OtherDetails.Item(i).ItemName
                                myDrugNode.DrugName = oCriteria.OtherDetails.Item(i).CategoryName
                                myDrugNode.Dosage = oCriteria.OtherDetails.Item(i).ItemName
                                trvSelectedDrugs.Nodes.Add(myDrugNode)
                                'End If
                                '' Get history from otherdetail
                            Case Supporting.enumDetailType.History
                                'If (oCriteria.OtherDetails.Item(i).DetailType = Supporting.enumDetailType.History.GetHashCode()) Then
                                Dim CategoryFound As Boolean = False
                                Dim HistoryFound As Boolean = False
                                Dim oCategoryNode As TreeNode
                                Dim oHistoryNode As myTreeNode
                                'If gblnSMDBSetting = True And gstrSMHistory.Trim() <> "" Then


                                '    For Each CategoryNode As TreeNode In trvselectedhist.Nodes
                                '        If CategoryNode.Text = oCriteria.OtherDetails.Item(i).CategoryName Then

                                '            oHistoryNode = New myTreeNode

                                '            'oHistoryNode.Text = oCriteria.OtherDetails.Item(i).CategoryName
                                '            'oHistoryNode.Tag = oCriteria.OtherDetails.Item(i).ItemName
                                '            oHistoryNode.Text = oCriteria.OtherDetails.Item(i).ItemName
                                '            oHistoryNode.Tag = oCriteria.OtherDetails.Item(i).CategoryName
                                '            oHistoryNode.ImageIndex = 11
                                '            CategoryNode.Nodes.Add(oHistoryNode)
                                '            CategoryNode.Expand()
                                '            oHistoryNode = Nothing

                                '            CategoryFound = True
                                '            Exit For
                                '        End If
                                '    Next

                                '    If Not CategoryFound Then
                                '        oCategoryNode = New TreeNode
                                '        oHistoryNode = New myTreeNode

                                '        ''To Add Category Node
                                '        oCategoryNode.Text = oCriteria.OtherDetails.Item(i).CategoryName

                                '        oCategoryNode.ImageIndex = 0
                                '        oCategoryNode.SelectedImageIndex = 0

                                '        ''To Add History Node
                                '        oHistoryNode.Text = oCriteria.OtherDetails.Item(i).ItemName
                                '        oHistoryNode.Tag = oCriteria.OtherDetails.Item(i).CategoryName
                                '        oHistoryNode.ImageIndex = 11

                                '        oCategoryNode.Nodes.Add(oHistoryNode)
                                '        trvselectedhist.Nodes.Add(oCategoryNode)

                                '        trvselectedhist.ExpandAll()
                                '        oCategoryNode = Nothing
                                '        oHistoryNode = Nothing
                                '    End If


                                'Else


                                For Each CategoryNode As TreeNode In trvSelectedHistory.Nodes
                                    If CategoryNode.Text = oCriteria.OtherDetails.Item(i).CategoryName Then

                                        oHistoryNode = New myTreeNode

                                        'oHistoryNode.Text = oCriteria.OtherDetails.Item(i).CategoryName
                                        'oHistoryNode.Tag = oCriteria.OtherDetails.Item(i).ItemName
                                        oHistoryNode.Text = oCriteria.OtherDetails.Item(i).ItemName
                                        oHistoryNode.Tag = oCriteria.OtherDetails.Item(i).CategoryName

                                        CategoryNode.Nodes.Add(oHistoryNode)
                                        CategoryNode.Expand()
                                        oHistoryNode = Nothing

                                        CategoryFound = True
                                        Exit For
                                    End If
                                Next

                                If Not CategoryFound Then
                                    oCategoryNode = New TreeNode
                                    oHistoryNode = New myTreeNode

                                    ''To Add Category Node
                                    oCategoryNode.Text = oCriteria.OtherDetails.Item(i).CategoryName

                                    oCategoryNode.ImageIndex = 0
                                    oCategoryNode.SelectedImageIndex = 0

                                    ''To Add History Node
                                    oHistoryNode.Text = oCriteria.OtherDetails.Item(i).ItemName
                                    oHistoryNode.Tag = oCriteria.OtherDetails.Item(i).CategoryName


                                    oCategoryNode.Nodes.Add(oHistoryNode)
                                    trvSelectedHistory.Nodes.Add(oCategoryNode)

                                    trvSelectedHistory.ExpandAll()
                                    oCategoryNode = Nothing
                                    oHistoryNode = Nothing
                                End If

                                'End If

                                'End If
                            Case Supporting.enumDetailType.ICD9
                                'Get  ICD9 from other details
                                'If (oCriteria.OtherDetails.Item(i).DetailType = Supporting.enumDetailType.ICD9.GetHashCode()) Then
                                ''Sandip 
                                ''For j As Integer = 0 To trvICD9.GetNodeCount(False) - 1
                                ''    If trvICD9.Nodes(j).Tag = oCriteria.OtherDetails.Item(i).CategoryName Then
                                ''        trvICD9.Nodes(j).Checked = True
                                ''        Exit For
                                ''    End If
                                ''Next
                                'End If
                                Dim myICDNode As myTreeNode
                                myICDNode = New myTreeNode
                                myICDNode.Text = oCriteria.OtherDetails.Item(i).CategoryName + " - " + oCriteria.OtherDetails.Item(i).ItemName
                                myICDNode.Tag = oCriteria.OtherDetails.Item(i).CategoryName
                                myICDNode.DrugName = oCriteria.OtherDetails.Item(i).ItemName
                                trvselecteICDs.Nodes.Add(myICDNode)

                            Case Supporting.enumDetailType.CPT
                                ' Get CPT from other details
                                'If (oCriteria.OtherDetails.Item(i).DetailType = Supporting.enumDetailType.CPT.GetHashCode()) Then
                                ''Sandip 
                                ''For j As Integer = 0 To trvICD9.GetNodeCount(False) - 1
                                ''    If trvCPT.Nodes(j).Tag = oCriteria.OtherDetails.Item(i).CategoryName.Trim() Then
                                ''        trvCPT.Nodes(j).Checked = True
                                ''        Exit For
                                ''    End If
                                ''Next
                                'End If
                                ''Sandip Darade 20090515
                                ''Select CPTs on the treeview control

                                Dim myCPTNode As myTreeNode
                                myCPTNode = New myTreeNode
                                myCPTNode.Text = oCriteria.OtherDetails.Item(i).CategoryName.Trim() + " - " + oCriteria.OtherDetails.Item(i).ItemName
                                myCPTNode.Tag = oCriteria.OtherDetails.Item(i).CategoryName
                                myCPTNode.DrugName = oCriteria.OtherDetails.Item(i).ItemName

                                trvselectedCPT.Nodes.Add(myCPTNode)

                            Case Supporting.enumDetailType.Order
                                ''Get Radiology Orders from other details
                                'If (oCriteria.OtherDetails.Item(i).DetailType = Supporting.enumDetailType.Order.GetHashCode()) Then

                                For j As Integer = 1 To c1Labs.Rows.Count - 1
                                    Dim _TestCell As String = c1Labs.GetData(j, COL_IDENTITY) & ""
                                    If Mid(_TestCell, 1, 1) = "T" Then
                                        If c1Labs.Rows(j).Node.Data = oCriteria.OtherDetails.Item(i).ItemName And c1Labs.Rows(j).Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.Parent).Data = oCriteria.OtherDetails.Item(i).OperatorName And c1Labs.Rows(j).Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.Root).Data = oCriteria.OtherDetails.Item(i).CategoryName Then
                                            c1Labs.SetCellCheck(j, COL_NAME, C1.Win.C1FlexGrid.CheckEnum.Checked)
                                            'c1Labs.SetData(j, COL_MINVALUE, oCriteria.OtherDetails.Item(i).Result1)
                                            'c1Labs.SetData(j, COL_MAXVALUE, oCriteria.OtherDetails.Item(i).Result2)
                                        End If
                                    End If
                                Next

                                'End If
                            Case Supporting.enumDetailType.Lab
                                ' Get Labs from other details
                                'If (oCriteria.OtherDetails.Item(i).DetailType = Supporting.enumDetailType.Lab.GetHashCode()) Then
                                For j As Integer = 1 To C1LabResult.Rows.Count - 1
                                    If C1LabResult.Rows(j).Node.Level = 1 Then
                                        If C1LabResult.Rows(j).Node.Data = oCriteria.OtherDetails.Item(i).ItemName And C1LabResult.Rows(j).Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.Parent).Data = oCriteria.OtherDetails.Item(i).CategoryName Then
                                            C1LabResult.SetCellCheck(j, COL_TestName, C1.Win.C1FlexGrid.CheckEnum.Checked)
                                            C1LabResult.SetData(j, COL_Operator, oCriteria.OtherDetails.Item(i).OperatorName)
                                            C1LabResult.SetData(j, COL_ResultValue1, oCriteria.OtherDetails.Item(i).Result1)
                                            C1LabResult.SetData(j, COL_ResultValue2, oCriteria.OtherDetails.Item(i).Result2)
                                        End If
                                    End If
                                Next

                                'End If
                        End Select
                    Next

                    ''End other detail

                    'sarika DM Denormalization
                    Dim objList As myList
                    '--

                    For i As Integer = 1 To .LabOrders.Count
                        Try

                            'sarika DM Denormalization 20090404
                            'Dim LabName As String = oDM.GetLabTestName(.LabOrders.Item(i))
                            'Dim mynode As New myTreeNode(LabName, .LabOrders.Item(i))

                            'objList = New myList
                            objList = CType(.LabOrders.Item(i), myList)

                            Dim mynode As New myTreeNode(objList.Value, objList.ID)
                            '-----
                            objList = Nothing
                            'check if selected node is rootnode
                            If Not IsNothing(mynode) Then
                                AddAssociates(mynode, "Labs")
                                mynode.Dispose()
                                mynode = Nothing
                            End If
                        Catch ex As Exception
                            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        End Try
                    Next

                    For i As Integer = 1 To .RadiologyOrders.Count
                        Try

                            'sarika DM Denormalization
                            'Dim RadiologyOrderName As String = oDM.GetRadiologyOrder(.RadiologyOrders.Item(i))
                            'Dim mynode As New myTreeNode(RadiologyOrderName, .RadiologyOrders.Item(i))

                            'objList = New myList
                            objList = CType(.RadiologyOrders.Item(i), myList)

                            Dim mynode As New myTreeNode(objList.Value, objList.ID)
                            objList = Nothing
                            '---

                            'check if selected node is rootnode
                            If Not IsNothing(mynode) Then
                                AddAssociates(mynode, "Orders")
                                mynode.Dispose()
                                mynode = Nothing
                            End If
                        Catch ex As Exception
                            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        End Try
                    Next

                    For i As Integer = 1 To .Referrals.Count
                        Try

                            'sarika DM Denormalization
                            'Dim ReferralsName As String = oDM.GetRefferalName(.Referrals.Item(i))
                            'Dim mynode As New myTreeNode(ReferralsName, .Referrals.Item(i))

                            objList = CType(.Referrals.Item(i), myList)

                            Dim mynode As New myTreeNode(objList.Value, objList.ID)
                            objList = Nothing
                            '---
                            'check if selected node is rootnode
                            If Not IsNothing(mynode) Then
                                AddAssociates(mynode, "Referrals")
                                mynode.Dispose()
                                mynode = Nothing
                            End If
                        Catch ex As Exception
                            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        End Try
                    Next

                    For i As Integer = 1 To .RxDrugs.Count
                        Try
                            'sarika DM Denormalization
                            'Dim DrugName As String = oDM.GetDrugName(.RxDrugs.Item(i))
                            'Dim mynode As New myTreeNode(DrugName, .RxDrugs.Item(i))

                            objList = CType(.RxDrugs.Item(i), myList)

                            'Dim mynode As New myTreeNode(objList.Value, objList.ID)
                            Dim mynode As New myTreeNode(objList.Value, objList.ID, objList.DrugName, objList.Dosage, objList.DrugForm, objList.Route, objList.Frequency, objList.NDCCode, objList.IsNarcotic, objList.Duration, objList.mpid, objList.DrugQtyQualifier)
                            objList = Nothing
                            '---


                            'check if selected node is rootnode
                            If Not IsNothing(mynode) Then
                                AddAssociates(mynode, "Rx")
                                mynode.Dispose()
                                mynode = Nothing
                            End If
                        Catch ex As Exception
                            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        End Try
                    Next

                    For i As Integer = 1 To .Guidelines.Count
                        Try
                            'sarika DM Denormalization 20090331
                            'Dim GuildLine As String = oDM.GetGuidLine(.Guidelines.Item(i))
                            'Dim mynode As New myTreeNode(GuildLine, .Guidelines.Item(i))

                            '    Dim mynode As New myTreeNode(.Guidelines.Item("TemplateName"), .Guidelines.Item("TemplateID"))
                            Dim mynode As New myTreeNode()
                            objList = New myList
                            objList = .Guidelines.Item(i)
                            mynode.Text = objList.DMTemplateName
                            mynode.Tag = objList.ID
                            If Not IsNothing(objList.DMTemplate) Then
                                'Changed by Shweta 20100107 
                                'Against the bug id:5611
                                'bind template to the node 
                                'mynode.DMTemplate = objList.DMTemplate
                                mynode.TemplateResult = objList.DMTemplate
                                'End 20100107
                            Else
                                'Changed by Shweta 20100107 
                                'Against the bug id:5611
                                'bind template to the node 
                                mynode.TemplateResult = Nothing
                                'mynode.DMTemplate = Nothing
                                'end 20100107
                            End If
                            '---

                            'check if selected node is rootnode
                            If Not IsNothing(mynode) Then
                                AddAssociates(mynode, "Guidelines")
                                mynode.Dispose()
                                mynode = Nothing
                            End If

                            objList = Nothing
                        Catch ex As Exception
                            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        End Try
                    Next

                    '''''''''Chetan Integrated  as on 09 oct 2010 - for IM in DM Setup
                    For i As Integer = 1 To .IMlst.Count
                        Try
                            blnloadIm = True
                            Dim mynode As New myTreeNode()
                            objList = New myList
                            objList = .IMlst.Item(i)

                            mynode.Text = objList.DMTemplateName ''Vaccine Name
                            mynode.Tag = objList.ID                  'IM ID
                            mynode.Key = objList.ID                  'IM ID
                            mynode.DrugForm = objList.DrugForm ''Vaccine Code
                            mynode.Route = objList.Route ''SKU

                            mynode.DrugName = objList.Code
                            mynode.Dosage = objList.Description
                            mynode.Frequency = objList.Frequency ''TradeName
                            mynode.NDCCode = objList.NDCCode ''Manufaturer
                            mynode.IsNarcotics = objList.IsNarcotic
                            mynode.Duration = objList.Duration ''Lot Number


                            mynode.TemplateResult = Nothing

                            If Not IsNothing(mynode) Then
                                AddAssociates(mynode, "IM")
                                mynode.Dispose()
                                mynode = Nothing
                            End If

                            objList = Nothing
                        Catch ex As Exception
                            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        End Try
                    Next

                    '''''''''Chetan Integrated  as on 09 oct 2010 - for IM in DM Setup




                End With
            End If
            trOrderInfo.ExpandAll()
            If (IsNothing(oCriteria) = False) Then
                oCriteria.Dispose()
                oCriteria = Nothing
            End If
            If (IsNothing(oDM) = False) Then
                oDM.Dispose()
                oDM = Nothing
            End If

            blnloadIm = False
        Catch ex As Exception
            MessageBox.Show(ex.Message())
        End Try
    End Sub

    'Private Sub fill_labs()

    Dim oLabs As New gloStream.DiseaseManagement.Supporting.Orders

    '    'Dim oDM As New gloStream.DiseaseManagement.Common.Criteria
    '    'Dim oLabNode As TreeNode
    '    'Dim oGroupNode As TreeNode
    '    'Dim oTestNode As TreeNode

    '    'oLabs = oDM.Labs

    '    'With trvLabs
    '    '    .Nodes.Clear()
    '    '    For i As Integer = 1 To oLabs.Count
    '    '        oLabNode = New TreeNode
    '    '        With oLabNode
    '    '            .Text = oLabs(i).Category
    '    '            .Tag = oLabs(i).ID

    '    '            For j As Integer = 1 To oLabs(i).OrderGroups.Count
    '    '                oGroupNode = New TreeNode
    '    '                With oGroupNode
    '    '                    .Text = oLabs(i).OrderGroups(j).Name
    '    '                    .Tag = oLabs(i).OrderGroups(j).ID
    '    '                End With

    '    '                For k As Integer = 1 To oLabs(i).OrderGroups(j).Tests.Count
    '    '                    oTestNode = New TreeNode
    '    '                    With oTestNode
    '    '                        .Text = oLabs(i).OrderGroups(j).Tests(k).Description
    '    '                        .Tag = oLabs(i).OrderGroups(j).Tests(k).ID
    '    '                    End With

    '    '                    oGroupNode.Nodes.Add(oTestNode)

    '    '                    oTestNode = Nothing
    '    '                Next

    '    '                oLabNode.Nodes.Add(oGroupNode)
    '    '                oGroupNode = Nothing
    '    '            Next

    '    '        End With

    '    '        .Nodes.Add(oLabNode)
    '    '        oLabNode = Nothing
    '    '    Next
    '    'End With

    '    'Exit Function

    '    'With trvLabs
    '    '    .Nodes.Clear()
    '    '    For i As Integer = 1 To oLabs.Count
    '    '        oLabNode = New TreeNode
    '    '        With oLabNode
    '    '            .Text = oLabs(i).Category
    '    '            .Tag = oLabs(i).ID
    '    '            For j As Integer = 1 To oLabs(i).OrderGroups.Count
    '    '                oGroupNode = New TreeNode
    '    '                With oGroupNode
    '    '                    .Text = oLabs(i).OrderGroups(j).Name
    '    '                    .Tag = oLabs(i).OrderGroups(j).ID
    '    '                    For k As Integer = 1 To oLabs(i).OrderGroups(j).Tests.Count
    '    '                        oTestNode = New TreeNode
    '    '                        .Text = oLabs(i).OrderGroups(j).Tests(k).Description
    '    '                        .Tag = oLabs(i).OrderGroups(j).Tests(k).ID
    '    '                        oGroupNode.Nodes.Add(oTestNode)
    '    '                        oTestNode = Nothing
    '    '                    Next
    '    '                End With
    '    '                oLabNode.Nodes.Add(oGroupNode)
    '    '                oGroupNode = Nothing
    '    '            Next
    '    '        End With
    '    '        trvLabs.Nodes.Add(oLabNode)
    '    '        oLabNode = Nothing
    '    '    Next
    '    'End With
    'End Sub

    Private Sub SetCombiIndex(ByVal ControlCombo As ComboBox)
        For i As Integer = 0 To ControlCombo.Items.Count - 1
            If ControlCombo.Text = ControlCombo.Items(i) Then
                ControlCombo.SelectedIndex = i
                Exit Sub
            End If
        Next
    End Sub

    Private Sub Fill_RadiologyLabsC1()
        'COL_NAME As Integer = 0
        'COL_ID As Integer = 1
        'COL_TESTGROUPFLAG As Integer = 2
        'COL_LEVELNO As Integer = 3
        'COL_GROUPNO As Integer = 4
        'COL_MINVALUE As Integer = 5
        'COL_MAXVALUE()
        Dim _C As Integer, _G As Integer, _T As Integer


        With c1Labs
            .Rows.Count = 1
            .Rows.Fixed = 1
            .Cols.Count = COL_COUNT
            .Cols.Fixed = 0
            .Rows(.Rows.Count - 1).Height = 22
            .Cols(COL_NAME).Width = ((.Width / 4) * 2) - 20
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
            Try
                If (.Styles.Contains("CS_Test")) Then
                    csTest = .Styles("CS_Test")
                Else
                    csTest = .Styles.Add("CS_Test")
                    With csTest
                        .Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Bold) 'IIf(Font.Height = 8.25F, gFont_SMALL_BOLD, IIf(Font.Height = 9.0F, gFont_BOLD, New Font(Font, FontStyle.Bold)))
                        .ForeColor = Color.Maroon
                        .BackColor = Color.White
                        .Display = C1.Win.C1FlexGrid.DisplayEnum.Stack
                    End With
                End If
            Catch ex As Exception
                csTest = .Styles.Add("CS_Test")
                With csTest
                    .Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Bold) 'IIf(Font.Height = 8.25F, gFont_SMALL_BOLD, IIf(Font.Height = 9.0F, gFont_BOLD, New Font(Font, FontStyle.Bold)))
                    .ForeColor = Color.Maroon
                    .BackColor = Color.White
                    .Display = C1.Win.C1FlexGrid.DisplayEnum.Stack
                End With
            End Try
          


            '.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.Nodes
            '.Redraw = False

            Dim oDM As New gloStream.DiseaseManagement.Common.Criteria
            Dim oOrders As gloStream.DiseaseManagement.Supporting.Orders

            oOrders = oDM.Orders
            If Not oOrders Is Nothing Then
                If oOrders.Count > 0 Then

                    'Fill Category
                    For _C = 1 To oOrders.Count
                        .Rows.Add()
                        With .Rows(.Rows.Count - 1)
                            .ImageAndText = True
                            .Height = 24
                            .IsNode = True
                            '//.Style = FillControl.Styles("CS_Category")
                            .Node.Level = 0
                            .Node.Data = oOrders(_C).Category
                            .Node.Key = oOrders(_C).ID
                        End With
                        .SetData(.Rows.Count - 1, COL_ID, oOrders(_C).ID)
                        .SetData(.Rows.Count - 1, COL_TESTGROUPFLAG, _LAB_Category)
                        .SetData(.Rows.Count - 1, COL_LEVELNO, 0)
                        .SetData(.Rows.Count - 1, COL_GROUPNO, 0)
                        .SetData(.Rows.Count - 1, COL_IDENTITY, _LAB_Category & oOrders(_C).ID)
                        .Rows(.Rows.Count - 1).AllowEditing = False
                    Next

                    'Fill Groups & Category

                    For _C = 1 To oOrders.Count
                        For _G = 1 To oOrders.Item(_C).OrderGroups.Count
                            Dim oFindNode As C1.Win.C1FlexGrid.Node
                            oFindNode = GetC1Node(_LAB_Category & oOrders.Item(_C).ID)

                            If Not oFindNode Is Nothing Then
                                oFindNode.AddNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild, oOrders.Item(_C).OrderGroups(_G).Name)
                                '//.Style = FillControl.Styles("CS_Category")
                                Dim _tmpRow As Integer = oFindNode.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild).Row.Index
                                If Not _tmpRow = -1 Then
                                    .Rows(_tmpRow).ImageAndText = True
                                    .Rows(_tmpRow).Height = 24
                                    .SetData(_tmpRow, COL_ID, oOrders.Item(_C).OrderGroups(_G).ID)
                                    .SetData(_tmpRow, COL_TESTGROUPFLAG, _LAB_Group)
                                    .SetData(_tmpRow, COL_LEVELNO, 0)
                                    .SetData(_tmpRow, COL_GROUPNO, 0)
                                    .SetData(_tmpRow, COL_IDENTITY, _LAB_Group & oOrders.Item(_C).OrderGroups(_G).ID)
                                    .Rows(_tmpRow).AllowEditing = False
                                    _tmpRow = -1
                                End If
                                oFindNode = Nothing

                                'Fill Tests Start
                                For _T = 1 To oOrders.Item(_C).OrderGroups(_G).Tests.Count
                                    Dim oFindNodeTest As C1.Win.C1FlexGrid.Node
                                    oFindNodeTest = GetC1Node(_LAB_Group & oOrders.Item(_C).OrderGroups.Item(_G).ID)

                                    If Not oFindNodeTest Is Nothing Then
                                        oFindNodeTest.AddNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild, oOrders.Item(_C).OrderGroups(_G).Tests(_T).Description)
                                        '//.Style = FillControl.Styles("CS_Category")
                                        Dim _tmpRowTest As Integer = oFindNodeTest.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild).Row.Index
                                        If Not _tmpRowTest = -1 Then
                                            .Rows(_tmpRowTest).ImageAndText = True
                                            .Rows(_tmpRowTest).Height = 24
                                            .SetData(_tmpRowTest, COL_ID, oOrders.Item(_C).OrderGroups(_G).Tests(_T).ID)
                                            .SetData(_tmpRowTest, COL_TESTGROUPFLAG, _LAB_Test)
                                            .SetData(_tmpRowTest, COL_LEVELNO, 0)
                                            .SetData(_tmpRowTest, COL_GROUPNO, oOrders.Item(_C).OrderGroups.Item(_G).ID)
                                            .SetData(_tmpRowTest, COL_IDENTITY, _LAB_Test & oOrders.Item(_C).OrderGroups(_G).Tests(_T).ID)
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
                        Next ' For _G = 1 To oOrders.Item(_C).OrderGroups.Count
                    Next

                End If
            End If
            If (IsNothing(oDM) = False) Then
                oDM.Dispose()
                oDM = Nothing
            End If
            If (IsNothing(oOrders) = False) Then
                oOrders.Dispose()
                oOrders = Nothing
            End If


        End With

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
                If .GetCellCheck(.Row, COL_NAME) = C1.Win.C1FlexGrid.CheckEnum.Unchecked Then
                    .SetData(.Row, COL_MINVALUE, Nothing)
                    .SetData(.Row, COL_MAXVALUE, Nothing)
                End If
            End With
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtTemperatureMin_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtTemperatureMin.Validating
        Try
            If txtTemperatureMin.Text <> "" Then
                If Val(txtTemperatureMin.Text) > 0 Then
                    If Val(txtTemperatureMin.Text) < 90 Or Val(txtTemperatureMin.Text) > 110 Then
                        MessageBox.Show("Invalid Temperature (between 90-110)", "Disease Management", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        txtTemperatureMin.Focus()
                        _IsValid = False
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
            MessageBox.Show(ex.ToString, "Disease Management", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtTemperatureMax_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTemperatureMax.LostFocus
        'btnHistory_Click(sender, e)
    End Sub

    Private Sub txtTemperatureMax_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtTemperatureMax.Validating
        Try
            If txtTemperatureMax.Text <> "" Then
                If Val(txtTemperatureMax.Text) > 0 Then
                    If Val(txtTemperatureMax.Text) < 90 Or Val(txtTemperatureMax.Text) > 110 Then
                        MessageBox.Show("Invalid Temperature (between 90-110)", "Disease Management", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        txtTemperatureMax.Focus()
                        _IsValid = False
                        Exit Sub
                    End If
                Else
                    txtTemperatureMax.Text = ""
                End If

                If MinMaxValidator(Trim(txtTemperatureMin.Text), Trim(txtTemperatureMax.Text)) = False Then
                    MessageBox.Show("Please check the Minimum and Maximum values for Temperature.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    txtTemperatureMax.Focus()
                    _IsValid = False
                    Exit Sub
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
                    'txtPulseMax.Focus()
                    txtPulseMin.Focus()
                    _IsValid = False
                    Exit Sub
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
                    _IsValid = False
                    Exit Sub
                End If
            Else
                txtPulseMax.Text = ""
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtPulseOXmax_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtPulseOXmax.Validating
        Try
            If Val(Trim(txtPulseOXmax.Text)) > 0 Then
                If MinMaxValidator(Trim(txtPulseOXmin.Text), Trim(txtPulseOXmax.Text)) = False Then
                    MessageBox.Show("Please check the Minimum and Maximum values for Pulse OX.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    txtPulseOXmax.Focus()
                    _IsValid = False
                    Exit Sub
                End If
            Else
                txtPulseOXmax.Text = ""
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtPulseOXmin_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtPulseOXmin.Validating
        Try
            If Val(txtPulseOXmin.Text) > 0 Then
                If MinMaxValidator(Trim(txtPulseOXmin.Text), Trim(txtPulseOXmax.Text)) = False Then
                    MessageBox.Show("Please check the Minimum and Maximum values for Pulse OX.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    txtPulseOXmin.Focus()
                    _IsValid = False
                    Exit Sub
                End If
            Else
                txtPulseOXmin.Text = ""
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtWeightMax_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtWeightMax.Validating
        Try
            If Val(txtWeightMax.Text) > 0 Then
                If MinMaxValidator(Trim(txtWeightMin.Text), Trim(txtWeightMax.Text)) = False Then
                    MessageBox.Show("Please check the Minimum and Maximum values for Weight.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    txtWeightMax.Focus()
                    _IsValid = False
                    Exit Sub
                End If
            Else
                txtWeightMax.Text = ""
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtWeightMin_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtWeightMin.Validating
        Try
            If Val(txtWeightMin.Text) > 0 Then
                If MinMaxValidator(Trim(txtWeightMin.Text), Trim(txtWeightMax.Text)) = False Then
                    MessageBox.Show("Please check the Minimum and Maximum values for Weight.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    txtWeightMin.Focus()
                    _IsValid = False
                    Exit Sub
                End If
            Else
                txtWeightMin.Text = ""
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtHeightMin_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtHeightMin.Validating
        Try
            If Val(txtHeightMin.Text) > 0 And Val(txtHeightMax.Text) <> 0 Then
                'If MinMaxValidator(Trim(txtHeightMin.Text), Trim(txtHeightMax.Text)) = False Then
                If Val(txtHeightMin.Text) > Val(txtHeightMax.Text) Then
                    MessageBox.Show("Please check the Minimum and Maximum values for Height.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    txtHeightMin.Focus()
                    _IsValid = False
                    Exit Sub
                End If
            Else
                txtHeightMinInch.Focus()

                ' txtHeightMin.Text = ""
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtHeightMax_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtHeightMax.Validating
        Try
            'If Val(txtHeightMax.Text) > 0 Then
            'If MinMaxValidator(Trim(txtHeightMin.Text), Trim(txtHeightMax.Text)) = False Then
            If Val(txtHeightMin.Text) > Val(txtHeightMax.Text) Then
                MessageBox.Show("Please check the Minimum and Maximum values for Height.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtHeightMax.ResetText()
                txtHeightMax.Focus()
                _IsValid = False
                Exit Sub

            End If
            '  Else
            ' txtHeightMax.Text = ""
            'End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtBMImin_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtBMImin.Validating
        Try
            If Val(txtBMImin.Text) > 0 Then
                If MinMaxValidator(Trim(txtBMImin.Text), Trim(txtBMImax.Text)) = False Then
                    MessageBox.Show("Please check the Minimum and Maximum values for BMI.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    txtBMImin.Focus()
                    _IsValid = False
                    Exit Sub
                End If
            Else
                txtBMImin.Text = ""
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtBMImax_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtBMImax.Validating
        Try
            If Val(txtBMImax.Text) > 0 Then
                If MinMaxValidator(Trim(txtBMImin.Text), Trim(txtBMImax.Text)) = False Then
                    MessageBox.Show("Please check the Minimum and Maximum values for BMI.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    txtBMImax.Focus()
                    _IsValid = False
                    Exit Sub
                End If
            Else
                txtBMImax.Text = ""
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtBPsettingMax_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtBPsettingMax.Validating
        Try
            If Val(txtBPsettingMax.Text) > 0 Then
                If MinMaxValidator(Trim(txtBPsettingMin.Text), Trim(txtBPsettingMax.Text)) = False Then
                    MessageBox.Show("Please check the Minimum and Maximum values for BPSitting.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    txtBPsettingMax.Focus()
                    _IsValid = False
                    Exit Sub
                End If
                If Val(txtBPsettingMax.Text) < 20 Or Val(txtBPsettingMax.Text) > 280 Then
                    MessageBox.Show("Invalid BP Setting (between 20-280)", "Disease Management", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    txtBPsettingMax.Focus()
                    _IsValid = False
                    Exit Sub
                End If
            Else
                txtBPsettingMax.Text = ""
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtBPsettingMin_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtBPsettingMin.Validating
        Try
            If Val(txtBPsettingMin.Text) > 0 Then
                If MinMaxValidator(Trim(txtBPsettingMin.Text), Trim(txtBPsettingMax.Text)) = False Then
                    MessageBox.Show("Please check the Minimum and Maximum values for BPSitting.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    txtBPsettingMin.Focus()
                    _IsValid = False
                    Exit Sub
                End If
                If Val(txtBPsettingMin.Text) < 20 Or Val(txtBPsettingMin.Text) >= 280 Then
                    MessageBox.Show("Invalid BP Setting (between 20-280)", "Disease Management", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    txtBPsettingMin.Focus()
                    _IsValid = False
                    Exit Sub
                End If
            Else
                txtBPsettingMin.Text = ""
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtBPstandingMax_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtBPstandingMax.Validating
        Try
            If Val(txtBPstandingMax.Text) > 0 Then
                If MinMaxValidator(Trim(txtBPstandingMin.Text), Trim(txtBPstandingMax.Text)) = False Then
                    MessageBox.Show("Please check the Minimum and Maximum values for BPStanding.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    txtBPstandingMax.Focus()
                    _IsValid = False
                    Exit Sub
                End If
                If Val(txtBPstandingMax.Text) < 20 Or Val(txtBPstandingMax.Text) >= 280 Then
                    MessageBox.Show("Invalid BP Setting (between 20-280)", "Disease Management", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    txtBPstandingMax.Focus()
                    _IsValid = False
                    Exit Sub
                End If
            Else
                txtBPstandingMax.Text = ""
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtBPstandingMin_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtBPstandingMin.Validating
        Try
            If Val(txtBPstandingMin.Text) > 0 Then
                If MinMaxValidator(Trim(txtBPstandingMin.Text), Trim(txtBPstandingMax.Text)) = False Then
                    MessageBox.Show("Please check the Minimum and Maximum values for BPStanding.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    txtBPstandingMin.Focus()
                    _IsValid = False
                    Exit Sub
                End If
                If Val(txtBPstandingMin.Text) < 20 Or Val(txtBPstandingMin.Text) >= 281 Then
                    MessageBox.Show("Invalid BP Setting (between 20-280)", "Disease Management", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    txtBPstandingMin.Focus()
                    _IsValid = False
                    Exit Sub
                End If
            Else
                txtBPstandingMin.Text = ""
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
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
            MessageBox.Show(ex.ToString, "Disease Management", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtHeightMin_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtHeightMin.KeyPress
        Try
            AllowDecimal(txtHeightMin.Text, e)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Disease Management", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtHeightMax_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtHeightMax.KeyPress
        Try
            AllowDecimal(txtHeightMax.Text, e)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Disease Management", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtWeightMin_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtWeightMin.KeyPress
        Try
            AllowDecimal(txtWeightMin.Text, e)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Disease Management", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtWeightMax_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtWeightMax.KeyPress
        Try
            AllowDecimal(txtWeightMax.Text, e)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Disease Management", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtTemperatureMin_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTemperatureMin.KeyPress
        Try
            AllowDecimal(txtTemperatureMin.Text, e)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Disease Management", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtTemperatureMax_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTemperatureMax.KeyPress
        Try
            AllowDecimal(txtTemperatureMax.Text, e)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Disease Management", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtBMImin_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtBMImin.KeyPress
        Try
            AllowDecimal(txtBMImin.Text, e)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Disease Management", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtBMImax_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtBMImax.KeyPress
        Try
            AllowDecimal(txtBMImax.Text, e)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Disease Management", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtBPsettingMin_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtBPsettingMin.KeyPress
        Try
            AllowDecimal(txtBPsettingMin.Text, e)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Disease Management", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtBPsettingMax_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtBPsettingMax.KeyPress
        Try
            AllowDecimal(txtBPsettingMax.Text, e)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Disease Management", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtBPstandingMin_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtBPstandingMin.KeyPress
        Try
            AllowDecimal(txtBPstandingMin.Text, e)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Disease Management", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtBPstandingMax_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtBPstandingMax.KeyPress
        Try
            AllowDecimal(txtBPstandingMax.Text, e)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Disease Management", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtPulseMax_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPulseMax.KeyPress
        Try
            AllowDecimal(txtPulseMax.Text, e)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Disease Management", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtPulseOXmin_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPulseOXmin.KeyPress
        Try
            AllowDecimal(txtPulseOXmin.Text, e)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Disease Management", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtPulseOXmax_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPulseOXmax.KeyPress
        Try
            AllowDecimal(txtPulseOXmax.Text, e)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Disease Management", MessageBoxButtons.OK, MessageBoxIcon.Error)
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
            MessageBox.Show(ex.ToString, "Disease Management", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtHeightMinInch_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtHeightMinInch.KeyPress
        Try
            AllowDecimal(txtHeightMinInch.Text, e)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Disease Management", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtHeightMinInch_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtHeightMinInch.Validating
        Try
            If Val(txtHeightMin.Text) <= 0 Then
                If Val(txtHeightMinInch.Text) >= 12 And Val(txtHeightMinInch.Text) <= 84 Then
                    Dim _Ft As Decimal
                    Dim _Inches As Decimal
                    Dim _TotalInches As Decimal = Val(txtHeightMinInch.Text)

                    _Ft = Math.Floor(_TotalInches / 12)
                    _Inches = Math.Round(_TotalInches Mod 12, 2)
                    txtHeightMin.Text = _Ft
                    txtHeightMinInch.Text = _Inches
                    ' Exit Sub
                ElseIf Val(txtHeightMinInch.Text) > 84 Then
                    MessageBox.Show("Invalid value of Inches", "Patient Vitals", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    txtHeightMinInch.Focus()
                    _IsValidate = False
                End If
            Else
                If Val(txtHeightMinInch.Text) >= 12 Then
                    MessageBox.Show("Invalid value of Inches", "Patient Vitals", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    txtHeightMinInch.Focus()
                    _IsValidate = False
                End If
            End If

            'If Val(txtHeightMinInch.Text) >= 12 Then
            '    MessageBox.Show("Invalid value of Inches", "Patient Vitals", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    txtHeightMinInch.Focus()
            '    Exit Sub
            'End If

            'If Val(txtHeightMinInch.Text) = 0 Then
            '    txtHeightMinInch.Text = ""
            '    Exit Sub
            'End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtHeightMaxInch_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtHeightMaxInch.Validating
        Try
            If Val(txtHeightMax.Text) <= 0 Then
                If Val(txtHeightMaxInch.Text) >= 12 And Val(txtHeightMaxInch.Text) <= 84 Then
                    Dim _Ft As Decimal
                    Dim _Inches As Decimal
                    Dim _TotalInches As Decimal = Val(txtHeightMaxInch.Text)

                    _Ft = Math.Floor(_TotalInches / 12)
                    _Inches = Math.Round(_TotalInches Mod 12, 2)
                    txtHeightMax.Text = _Ft
                    txtHeightMaxInch.Text = _Inches
                    'Exit Sub
                ElseIf Val(txtHeightMaxInch.Text) > 84 Then
                    MessageBox.Show("Invalid value of Inches", "Patient Vitals", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    txtHeightMaxInch.Focus()
                    _IsValidate = False
                End If
            Else
                If Val(txtHeightMaxInch.Text) >= 12 Then
                    MessageBox.Show("Invalid value of Inches", "Patient Vitals", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    txtHeightMaxInch.Focus()
                    _IsValidate = False
                End If
            End If
            'If Val(txtHeightMaxInch.Text) >= 12 Then
            '    MessageBox.Show("Invalid value of Inches", "Patient Vitals", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    txtHeightMaxInch.Focus()
            '    ' Exit Sub
            'End If

            If Val(txtHeightMin.Text) > Val(txtHeightMax.Text) Then
                MessageBox.Show("Invalid value of Ft ", "Patient Vitals", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtHeightMax.Focus()
                _IsValidate = False
            ElseIf Val(txtHeightMin.Text) = Val(txtHeightMax.Text) And (Val(txtHeightMinInch.Text) > Val(txtHeightMaxInch.Text)) Then
                'MessageBox.Show("Invalid value of Inches", "Patient Vitals", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                MessageBox.Show("Please check the Minimum and Maximum values for Height.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtHeightMaxInch.ResetText()
                txtHeightMaxInch.Focus()
                _IsValidate = False
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
            FillAllCriteria()
            sptRight.Visible = False
            pnlRight.Visible = False
            pnlRight.SendToBack()

            pnlLab.Visible = True
            pnlLab.BringToFront()

            ''ojeswini
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

            btnproblemlist.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
            btnproblemlist.BackgroundImageLayout = ImageLayout.Stretch
            btnproblemlist.Tag = "UnSelected"

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub fill_labs()
        Try
            Dim objResult As New gloStream.DiseaseManagement.Common.Criteria
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



                Dim _C As Integer, _G As Integer
                ''''Create object for the class
                Dim oDM As New gloStream.DiseaseManagement.Common.Criteria
                Dim oLabsModule As gloStream.DiseaseManagement.Supporting.LabModuleTests

                ''''assign Lab Test and Result to collection
                oLabsModule = oDM.LabModuleTests

                If Not oLabsModule Is Nothing Then
                    If oLabsModule.Count > 0 Then

                        'Fill Test
                        For _C = 1 To oLabsModule.Count
                            .Rows.Add()
                            With .Rows(.Rows.Count - 1)
                                .ImageAndText = True
                                .Height = 24
                                .IsNode = True
                                '//.Style = FillControl.Styles("CS_Category")
                                .Node.Level = 0
                                .Node.Data = oLabsModule(_C).Name
                                .Node.Key = oLabsModule(_C).TestID
                            End With
                            .SetData(.Rows.Count - 1, COL_TestID, oLabsModule(_C).TestID)
                            .SetData(.Rows.Count - 1, COL_IDENTITYModule, _LabModule_Result & oLabsModule(_C).TestID)
                            .Rows(.Rows.Count - 1).AllowEditing = False
                        Next

                        'Fill Result

                        For _C = 1 To oLabsModule.Count
                            For _G = 1 To oLabsModule.Item(_C).LabModuleTestResults.Count
                                Dim oFindNode As C1.Win.C1FlexGrid.Node
                                oFindNode = GetC1NodeModule(_LabModule_Result & oLabsModule.Item(_C).TestID)

                                If Not oFindNode Is Nothing Then
                                    oFindNode.AddNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild, oLabsModule.Item(_C).LabModuleTestResults(_G).ResultName)
                                    Dim _tmpRow As Integer = oFindNode.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild).Row.Index
                                    If Not _tmpRow = -1 Then
                                        .Rows(_tmpRow).ImageAndText = True
                                        .Rows(_tmpRow).Height = 24
                                        .Rows(_tmpRow).AllowEditing = True
                                        Dim strOperator As String
                                        Dim cStyle As C1.Win.C1FlexGrid.CellStyle
                                        Dim rgOperator As C1.Win.C1FlexGrid.CellRange = .GetCellRange(_tmpRow, COL_Operator, _tmpRow, COL_Operator)
                                        ' cStyle = .Styles.Add("Operator")
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
                                        .SetCellCheck(_tmpRow, COL_TestName, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
                                        .SetData(_tmpRow, COL_TestID, oLabsModule(_C).TestID)
                                        .SetData(_tmpRow, COL_ResultID, oLabsModule(_C).LabModuleTestResults(_G).ResultID)
                                        _tmpRow = -1
                                    End If
                                    oFindNode = Nothing
                                End If
                            Next
                        Next
                    End If
                End If
                If (IsNothing(oDM) = False) Then
                    oDM.Dispose()
                    oDM = Nothing
                End If
                If (IsNothing(oLabsModule) = False) Then
                    oLabsModule.Dispose()
                    oLabsModule = Nothing
                End If


            End With

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnRadiologyTest_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRadiologyTest.Click
        txtSearchOrder.Text = ""
        PopulateAssocaitedInfo(4)
        strParentToAssociate = btnRadiologyTest.Text

    End Sub

    'This code is added by Shilpa on 7th January for changing the Buttons background image MouseHover & MouseLeave Events


    Private Sub btnGuideline_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuideline.Click
        txtSearchOrder.Text = ""
        PopulateAssocaitedInfo(5)
        strParentToAssociate = btnGuideline.Text
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
            FillAllCriteria()
            FillSummery()
            sptRight.BringToFront()
            sptRight.Visible = True
            pnlRight.Visible = True
            pnlMiddle.BringToFront()
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

            btnproblemlist.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
            btnproblemlist.BackgroundImageLayout = ImageLayout.Stretch
            btnproblemlist.Tag = "UnSelected"

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnRx_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRx.Click
        txtSearchOrder.Text = ""
        PopulateAssocaitedInfo(3)
        strParentToAssociate = btnRx.Text
    End Sub

    Public Sub FillSummery()
        Dim strVitals As String = ""
        txt_summary.Text = ""

        If Not cmbAgeMin.SelectedItem Is Nothing AndAlso Not cmbAgeMax.SelectedItem Is Nothing Then
            txt_summary.Text = txt_summary.Text & "Age = " & cmbAgeMin.SelectedItem & "  To  " & cmbAgeMax.SelectedItem & vbNewLine & vbTab
        End If
        If txtCity.Text <> "" Then
            txt_summary.Text = txt_summary.Text & "City = " & txtCity.Text & vbNewLine & vbTab
        End If

        If Not cmbGender.SelectedItem Is Nothing Then
            txt_summary.Text = txt_summary.Text & "Gender = " & cmbGender.SelectedItem & vbNewLine & vbTab
        End If
        If Not cmbState.SelectedItem Is Nothing Then
            txt_summary.Text = txt_summary.Text & "State = " & cmbState.SelectedItem & vbNewLine & vbTab
        End If
        If Not cmbRace.SelectedItem Is Nothing Then
            txt_summary.Text = txt_summary.Text & "Race = " & cmbRace.SelectedItem & vbNewLine & vbTab
        End If
        If txtZip.Text <> "" Then
            txt_summary.Text = txt_summary.Text & "Zip Code = " & txtZip.Text & vbNewLine & vbTab
        End If

        If Not cmbMaritalSt.SelectedItem Is Nothing Then
            txt_summary.Text = txt_summary.Text & "Marital Status = " & cmbMaritalSt.SelectedItem & vbNewLine & vbTab
        End If
        If Not cmbEmpStatus.SelectedItem Is Nothing Then
            txt_summary.Text = txt_summary.Text & "Employee Status = " & cmbEmpStatus.SelectedItem & vbNewLine & vbTab
        End If

        ''''Added on 20100629 by sanjog for show Demographics
        If txt_summary.Text <> "" Then
            txt_summary.Text = "Demographics:  " & vbNewLine & vbTab & txt_summary.Text
        End If
        ''''Added on 20100629 by sanjog for show Demographics

        Dim _Height As String = ""
        If txtHeightMin.Text <> "" Or txtHeightMinInch.Text <> "" Then
            _Height = Val(txtHeightMin.Text) & "' " & Val(txtHeightMinInch.Text) & "''"
        End If
        If _Height <> "" Then
            strVitals = strVitals & "Minimum Height = " & _Height & vbNewLine & vbTab
        End If

        Dim _HeightMax As String = ""
        If txtHeightMax.Text <> "" Or txtHeightMaxInch.Text <> "" Then
            _HeightMax = Val(txtHeightMax.Text) & "' " & Val(txtHeightMaxInch.Text) & "''"
        End If
        If _HeightMax <> "" Then
            strVitals = strVitals & "Maximum Height = " & _HeightMax & vbNewLine & vbTab
        End If

        If txtBPsettingMin.Text <> "" Then
            strVitals = strVitals & "Minimum BP Sitting = " & CDbl(Val(txtBPsettingMin.Text.Trim)) & vbNewLine & vbTab
        End If

        If txtBPsettingMax.Text <> "" Then
            strVitals = strVitals & "Maximum BP Sitting = " & CDbl(Val(txtBPsettingMax.Text.Trim)) & vbNewLine & vbTab
        End If

        If txtWeightMin.Text <> "" Then
            strVitals = strVitals & "Weight Minimum = " & CDbl(Val(txtWeightMin.Text.Trim)) & vbNewLine & vbTab
        End If

        If txtWeightMax.Text <> "" Then
            strVitals = strVitals & "Weight Maximum = " & CDbl(Val(txtWeightMax.Text.Trim)) & vbNewLine & vbTab
        End If

        If txtBPstandingMin.Text <> "" Then
            strVitals = strVitals & "Minimum BP Standing = " & CDbl(Val(txtBPstandingMin.Text.Trim)) & vbNewLine & vbTab
        End If

        If txtBPstandingMax.Text <> "" Then
            strVitals = strVitals & "Maximum BP Standing = " & CDbl(Val(txtBPstandingMax.Text.Trim)) & vbNewLine & vbTab
        End If

        If txtTemperatureMin.Text <> "" Then
            strVitals = strVitals & "Minimum Temperature = " & CDbl(Val(txtTemperatureMin.Text.Trim)) & vbNewLine & vbTab
        End If

        If txtTemperatureMax.Text <> "" Then
            strVitals = strVitals & "Maximum Temperature = " & CDbl(Val(txtTemperatureMax.Text.Trim)) & vbNewLine & vbTab
        End If

        If txtPulseMin.Text <> "" Then
            strVitals = strVitals & "Minimum Pulse = " & CDbl(Val(txtPulseMin.Text.Trim)) & vbNewLine & vbTab
        End If

        If txtPulseMax.Text <> "" Then
            strVitals = strVitals & "Maximum Pulse = " & CDbl(Val(txtPulseMax.Text.Trim)) & vbNewLine & vbTab
        End If

        If txtBMImin.Text <> "" Then
            strVitals = strVitals & "Minimum BMI = " & CDbl(Val(txtBMImin.Text.Trim)) & vbNewLine & vbTab
        End If

        If txtBMImax.Text <> "" Then
            strVitals = strVitals & "Maximum BMI = " & CDbl(Val(txtBMImax.Text.Trim)) & vbNewLine & vbTab
        End If

        If txtPulseOXmin.Text <> "" Then
            strVitals = strVitals & "Minimum PulseOX = " & CDbl(Val(txtPulseOXmin.Text.Trim)) & vbNewLine & vbTab
        End If

        If txtPulseOXmax.Text <> "" Then
            strVitals = strVitals & "Maximum PulseOX = " & CDbl(Val(txtPulseOXmax.Text.Trim)) & vbNewLine & vbNewLine
        End If
        ''''Added on 20100629 by sanjog for Show Vitals
        If strVitals <> "" Then
            txt_summary.Text = txt_summary.Text & vbNewLine & vbNewLine & vbNewLine & "Vitals:  " & vbNewLine & vbTab & strVitals
        End If
        ''''Added on 20100629 by sanjog for Show Vitals

        Dim strhistory As String = ""
        Dim strDrugs As String = ""
        Dim strICD9 As String = ""
        Dim strCPT As String = ""
        Dim strRadiology As String = ""
        Dim strLab As String = ""

        'Code Start-Added by kanchan on 20101112 to display history summary
        'If gblnSMDBSetting = True And gstrSMHistory.Trim() <> "" Then
        '    For i As Integer = 0 To trvselectedhist.GetNodeCount(False) - 1
        '        For j As Integer = 0 To trvselectedhist.Nodes(i).GetNodeCount(False) - 1
        '            If strhistory.Contains("History:") Then
        '                If strhistory.Contains(trvselectedhist.Nodes(i).Text) Then
        '                    strhistory = strhistory & "," & trvselectedhist.Nodes(i).Nodes(j).Text
        '                Else
        '                    strhistory = strhistory & vbNewLine & vbTab & trvselectedhist.Nodes(i).Text & vbNewLine & vbTab & vbTab & trvselectedhist.Nodes(i).Nodes(j).Text
        '                End If
        '            Else
        '                strhistory = "History:  " & vbNewLine & vbTab & trvselectedhist.Nodes(i).Text & vbNewLine & vbTab & vbTab & trvselectedhist.Nodes(i).Nodes(j).Text
        '            End If
        '        Next
        '    Next
        'Else
        For i As Integer = 0 To trvSelectedHistory.GetNodeCount(False) - 1
            For j As Integer = 0 To trvSelectedHistory.Nodes(i).GetNodeCount(False) - 1
                If strhistory.Contains("History:") Then
                    If strhistory.Contains(trvSelectedHistory.Nodes(i).Text) Then
                        strhistory = strhistory & "," & trvSelectedHistory.Nodes(i).Nodes(j).Text
                    Else
                        strhistory = strhistory & vbNewLine & vbTab & trvSelectedHistory.Nodes(i).Text & vbNewLine & vbTab & vbTab & trvSelectedHistory.Nodes(i).Nodes(j).Text
                    End If
                Else
                    strhistory = "History:  " & vbNewLine & vbTab & trvSelectedHistory.Nodes(i).Text & vbNewLine & vbTab & vbTab & trvSelectedHistory.Nodes(i).Nodes(j).Text
                End If
            Next
        Next
        'End If
        'Code End-Added by kanchan on 20101112 to display history summary

        txt_summary.Text = txt_summary.Text & vbNewLine & strhistory

        'data for drugs
        For i As Integer = 0 To trvSelectedDrugs.GetNodeCount(False) - 1
            If strDrugs.Contains("Drugs:") Then
                strDrugs = strDrugs & "," & trvSelectedDrugs.Nodes(i).Text
            Else
                txt_summary.Text = txt_summary.Text & vbNewLine & vbNewLine & vbNewLine
                strDrugs = "Drugs:" & vbNewLine & vbTab & trvSelectedDrugs.Nodes(i).Text

            End If
        Next
        txt_summary.Text = txt_summary.Text & strDrugs



        For i As Integer = 0 To trvselecteICDs.GetNodeCount(False) - 1
            If strICD9.Contains("ICD9:") Then
                strICD9 = strICD9 & "," & trvselecteICDs.Nodes(i).Text
            Else
                txt_summary.Text = txt_summary.Text & vbNewLine & vbNewLine & vbNewLine
                strICD9 = "ICD9:" & vbNewLine & vbTab & trvselecteICDs.Nodes(i).Text
            End If

        Next
        txt_summary.Text = txt_summary.Text & vbNewLine & strICD9

        For i As Integer = 0 To trvselectedCPT.GetNodeCount(False) - 1
            If strCPT.Contains("CPT:") Then
                strCPT = strCPT & "," & trvselectedCPT.Nodes(i).Text
            Else
                txt_summary.Text = txt_summary.Text & vbNewLine & vbNewLine & vbNewLine
                strCPT = "CPT:" & vbNewLine & vbTab & trvselectedCPT.Nodes(i).Text
            End If
        Next
        txt_summary.Text = txt_summary.Text & vbNewLine & strCPT
        ' RadiologyLAB
        For i As Integer = 1 To c1Labs.Rows.Count - 1
            Dim _TestCell As String = c1Labs.GetData(i, COL_IDENTITY) & ""
            If Mid(_TestCell, 1, 1) = "T" Then
                If c1Labs.GetCellCheck(i, COL_NAME) = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                    If strRadiology.Contains("Orders:") Then
                        strRadiology = strRadiology & "," & c1Labs.GetData(i, COL_NAME)
                    Else
                        txt_summary.Text = txt_summary.Text & vbNewLine & vbNewLine & vbNewLine
                        strRadiology = "Orders:" & vbNewLine & vbTab & c1Labs.GetData(i, COL_NAME)
                    End If
                End If
            End If
        Next

        txt_summary.Text = txt_summary.Text & strRadiology


        'Lab Module  ' Enhancement - 02/08/2007
        For i As Integer = 1 To C1LabResult.Rows.Count - 1
            If C1LabResult.GetCellCheck(i, COL_TestName) = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                With C1LabResult
                    If strLab.Contains("Labs:") Then
                        strLab = strLab & "," & .GetData(i, COL_TestName)
                    Else
                        txt_summary.Text = txt_summary.Text & vbNewLine & vbNewLine & vbNewLine
                        strLab = "Labs:" & vbNewLine & vbTab & .GetData(i, COL_TestName)
                    End If
                End With
            End If
        Next
        txt_summary.Text = txt_summary.Text & strLab


        '' chetan added on 11 oct 2010 for filling probelmlist details 
        Dim strProb As String = ""
        If trvselectedprob.Nodes.Count > 0 Then
            Dim oprobnode As TreeNode = trvselectedprob.Nodes(0)
            If Not IsNothing(oprobnode) Then


                For i As Integer = 0 To oprobnode.Nodes.Count - 1
                    If strProb.Contains("Problem List:") Then
                        strProb = strProb & "," & oprobnode.Nodes(i).Text
                    Else
                        txt_summary.Text = txt_summary.Text & vbNewLine & vbNewLine & vbNewLine
                        strProb = "Problem List:" & vbNewLine & vbTab & oprobnode.Nodes(i).Text
                    End If
                Next

                txt_summary.Text = txt_summary.Text & vbNewLine & strProb
            End If
        End If

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
        strParentToAssociate = btnReferrals.Text
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
        strParentToAssociate = btnLab.Text
    End Sub

    Private Sub trvTriggers_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub AddAssociates(ByVal mynode As myTreeNode, ByVal strType As String, Optional ByVal addTemplate As Boolean = False)

        For Each myRootNode As myTreeNode In trOrderInfo.Nodes(0).Nodes
            If myRootNode.Text = strType Then
                ''Loop for all field nodes in each Root node
                For Each myTargetNode As myTreeNode In myRootNode.Nodes
                    ''Check whether the node already exists
                    '   If myRootNode.Text = "Guidelines" Then
                    If myRootNode.Text = "IM" Then

                    Else
                        If myTargetNode.Text = mynode.Text Then
                            ''If present do nothing
                            Exit Sub
                        End If

                    End If
                    If myRootNode.Text = "IM" Then
                        If myTargetNode.Key = mynode.Key And blnloadIm = False Then
                            Exit Sub
                        End If
                    End If
                   

                Next
                ''Map all the node values to the associated node
                Dim Associatenode As New myTreeNode
                If myRootNode.Text <> "IM" Then
                    Associatenode = mynode.Clone
                    Associatenode.Key = mynode.Key

                    Associatenode.Text = mynode.Text
                    Associatenode.Tag = mynode.Tag
                End If
                'sarika DMDenormalization 20090401
                If myRootNode.Text = "Guidelines" Then
                    
                    Associatenode.DMTemplate = mynode.TemplateResult
                    Associatenode.DMTemplateName = mynode.DrugName
                    'End 20100107

                End If
                If myRootNode.Text = "Rx" Then
                   
                    Associatenode.DMTemplate = mynode.DrugName
                    Associatenode.DrugName = mynode.DrugName
                    Associatenode.Dosage = mynode.Dosage
                    Associatenode.Tag = mynode.Key

                    'sarika DM Denormalization 20090410
                    Associatenode.DrugForm = mynode.DrugForm
                    Associatenode.Duration = mynode.Duration
                    Associatenode.Frequency = mynode.Frequency

                    Associatenode.NDCCode = mynode.NDCCode
                    Associatenode.Route = mynode.Route
                    Associatenode.DrugQtyQualifier = mynode.DrugQtyQualifier
                    Associatenode.IsNarcotics = mynode.IsNarcotics
                    '-----


                End If
                '---
                'Chetan Integrated on  09 Oct 2010  - for IM in DM Setup

                If myRootNode.Text = "IM" Then
                    ''Added by Mayuri:20120202-To replace lot number and count by item name
                    mynode.Text = mynode.Text.Replace(" - " & mynode.Dosage, "")
                    ''''''''''''''''''''''
                    Dim IMnode As New myTreeNode
                    ''Associatenode.DrugQtyQualifier = mynode.DrugQtyQualifier 'item_Count
                    If mynode.DrugQtyQualifier.ToString = "" Or mynode.DrugQtyQualifier.ToString = "0" Then


                        If mynode.Key = 0 And mynode.Tag <> 0 Then
                            mynode.Key = mynode.Tag
                        End If

                        IMnode = mynode.Clone
                        IMnode.Key = mynode.Key

                        IMnode.Text = mynode.Text
                        IMnode.Tag = mynode.Tag
                        IMnode.Route = mynode.Route ' SKU
                        IMnode.DrugForm = mynode.DrugForm    'VaccineCode
                        IMnode.Frequency = mynode.Frequency  'TradeName
                        IMnode.NDCCode = mynode.NDCCode 'Manufacture
                        IMnode.DMTemplateName = mynode.DrugName

                        IMnode.Duration = mynode.Duration ''Lot Number
                        IMnode.DrugName = mynode.DrugName ''Vaccine Name 
                        IMnode.DrugQtyQualifier = mynode.DrugQtyQualifier
                        IMnode.Dosage = mynode.Dosage
                        IMnode.ImageIndex = 0
                        IMnode.SelectedImageIndex = 0
                        myRootNode.Nodes.Add(IMnode)
                    Else
                        For Cnt As Int32 = 1 To mynode.DrugQtyQualifier

                            ''''''''''
                            If mynode.Key <> 0 And mynode.Tag = 0 Then
                                mynode.Tag = mynode.Key
                            End If
                            ''''''''''

                            IMnode = mynode.Clone
                            IMnode.Key = mynode.Key
                            IMnode.Route = mynode.Route 'Orginal Name of Vaccine
                            IMnode.Text = mynode.Text
                            IMnode.Tag = mynode.Tag
                            IMnode.DMTemplateName = mynode.DrugName
                            IMnode.DrugForm = mynode.DrugForm    'Vaccine code
                            IMnode.Duration = mynode.Duration    'Lot Number
                            IMnode.Frequency = mynode.Frequency  'tradEName                    
                            IMnode.NDCCode = mynode.NDCCode 'Manufacture

                            IMnode.DrugName = mynode.DrugName ''Vaccine Name 
                            IMnode.DrugQtyQualifier = mynode.DrugQtyQualifier
                            IMnode.Dosage = mynode.Dosage
                            IMnode.ImageIndex = 0
                            IMnode.SelectedImageIndex = 0
                            myRootNode.Nodes.Add(IMnode)
                        Next
                    End If
                    ''''''''''''''''''''''
                End If
                '''''''''Chetan Integrated on  09 Oct 2010 - for IM in DM Setup


                If myRootNode.Text <> "IM" Then '' chetan Integrated If Condition on 09 oct 2010

                    Associatenode.ImageIndex = 0
                    Associatenode.SelectedImageIndex = 0
                    myRootNode.Nodes.Add(Associatenode)
                End If

            End If
        Next
        trOrderInfo.ExpandAll()
        blnloadIm = False
        ' If Not mynode Is trvTriggers.Nodes.Item(0) Then   'not root node
        'Dim targetnode As New myTreeNode
        ' ''If the Node is root node then do not add CPT or Drug 
        ' ''Added by Anil on 20071220
        'If targetnode1 Is trOrderInfo.Nodes.Item(0) Then
        '    Exit Sub
        'End If
        'If _EditID = 0 Then
        '    Dim arrFullPath() As String = Split(mynode.FullPath, "\")

        '    If targetnode1.Name <> arrFullPath.GetValue(0) Then '  mynode.Parent.Name Then
        '        Exit Sub
        '    End If
        'End If



        ''check if targetnode is node at second level in trICD9Association treeview
        'If targetnode1.Parent Is trOrderInfo.Nodes.Item(0) Or (targetnode1.Key = -1) Then
        '    If targetnode1.Parent Is trOrderInfo.Nodes.Item(0) Then
        '        targetnode = targetnode1
        '    Else

        '        targetnode = targetnode1.Parent
        '    End If
        '    If _EditID <> 0 Then
        '        targetnode = targetnode1
        '    End If
        '    'targetnode = targetnode1
        '    Dim str As Long
        '    str = mynode.Key
        '    Dim mytragetnode As myTreeNode
        '    Dim associatenode As myTreeNode

        '    associatenode = mynode.Clone
        '    associatenode.Key = mynode.Key
        '    associatenode.Text = mynode.Text
        '    associatenode.Tag = mynode.Key

        '    If btnLab.Dock = DockStyle.Top Then

        '        For Each mytragetnode In targetnode.Nodes '.Item(0).Nodes
        '            If mytragetnode.Key = str Then
        '                Exit Sub
        '            End If
        '        Next
        '        targetnode.Nodes.Add(associatenode) 'Item(0).Nodes.Add(associatenode)
        '        'if selected category is Drugs, add node to Drugs child node 
        '        'in trICD9Associates
        '    ElseIf btnRadiologyTest.Dock = DockStyle.Top Then
        '        For Each mytragetnode In targetnode.Nodes '.Item(2).Nodes
        '            If mytragetnode.Key = str Then
        '                Exit Sub
        '            End If
        '        Next
        '        targetnode.Nodes.Add(associatenode) ' .Item(1).Nodes.Add(associatenode)

        '    ElseIf btnReferrals.Dock = DockStyle.Top Then
        '        For Each mytragetnode In targetnode.Nodes '.Item(1).Nodes
        '            If mytragetnode.Key = str Then
        '                Exit Sub
        '            End If
        '        Next
        '        targetnode.Nodes.Add(associatenode) ' .Item(2).Nodes.Add(associatenode)
        '    ElseIf btnGuideline.Dock = DockStyle.Top Then
        '        For Each mytragetnode In targetnode.Nodes '.Item(1).Nodes
        '            If mytragetnode.Key = str Then
        '                Exit Sub
        '            End If
        '        Next
        '        targetnode.Nodes.Add(associatenode) '.Item(3).Nodes.Add(associatenode)
        '    ElseIf btnRx.Dock = DockStyle.Top Then
        '        For Each mytragetnode In targetnode.Nodes '.Item(1).Nodes
        '            If mytragetnode.Key = str Then
        '                Exit Sub
        '            End If
        '        Next
        '        targetnode.Nodes.Add(associatenode) '.Item(4).Nodes.Add(associatenode)
        '    End If
        '    mynode.EnsureVisible()
        '    trOrderInfo.ExpandAll()
        '    trOrderInfo.SelectedNode = mynode
        '    trvTriggers.SelectedNode = trvTriggers.Nodes.Item(0)
        'End If
        ' End If
    End Sub


    Private Function GetTemplate(ByVal TemplateID As Int64) As Object
        Dim oDM As New gloStream.DiseaseManagement.DiseaseManagement
        Dim img As Object

        Try
            img = oDM.GetTemplate(TemplateID)

            Return img
        Catch ex As Exception
            Return Nothing
        Finally
            oDM.Dispose()
            oDM = Nothing
        End Try
    End Function

    Private Sub trOrderInfo_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs)

    End Sub

    Private Sub tlsDM_ItemClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tlsDM.ItemClicked

        Select Case e.ClickedItem.Tag
            Case "Save"
                'ADDED BY SHUBHSNGI 20100630
                '_IsValid = True
                tlsDM.Select()
                If _IsValidate = True Then ''_IsValidate For Invalid Heights & Inches (Bug Id 5677)
                    Call SaveCriteria()
                End If

                If (_IsValid) Then
                    '   Me.Close()
                    gloWord.WordDialogBoxBackgroundCloser.Close(Me)
                End If

            Case "Close"
                ' Me.Close()
                gloWord.WordDialogBoxBackgroundCloser.Close(Me)
        End Select
    End Sub

    'Private Sub trvTriggers_NodeMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) 
    '    Try
    '        trvTriggers.ContextMenu = Nothing
    '        If pnlbtnGuideline.Dock = DockStyle.Top Then
    '            Dim oMenuItem As MenuItem
    '            trvTriggers.SelectedNode = e.Node
    '            _selectedmynode = e.Node
    '            ''If the mouse button clicked is of right one
    '            If e.Button = Windows.Forms.MouseButtons.Right Then
    '                If Not IsNothing(trvTriggers.SelectedNode) Then
    '                    ''Validate the selected node for field node but should not be Parent or table node
    '                    If trvTriggers.Nodes.Item(0) Is trvTriggers.SelectedNode Then
    '                        trvTriggers.ContextMenu = Nothing

    '                    Else
    '                        ''Clear the menu items and add the context menu
    '                        CntConditions.MenuItems.Clear()
    '                        trvTriggers.ContextMenu = CntConditions

    '                        oMenuItem = New MenuItem
    '                        With oMenuItem
    '                            .Text = "Edit Template"
    '                            .Tag = "EditTemplateTrigger"
    '                            .Shortcut = Shortcut.CtrlShiftT
    '                            .ShowShortcut = False
    '                        End With
    '                    End If
    '                    CntConditions.MenuItems.Add(oMenuItem)
    '                    ''set the handler for the menu item
    '                    AddHandler oMenuItem.Click, AddressOf SetMenus

    '                    oMenuItem = Nothing

    '                End If
    '            End If
    '        End If
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message, "Report Designer", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub

    'Private Sub trvTriggers_NodeMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) 
    '    Try
    '        trvTriggers.SelectedNode = e.Node

    '        ''Get the selected node a nd make validation appropriately
    '        Dim myNode As myTreeNode
    '        myNode = CType(trvTriggers.SelectedNode, myTreeNode)
    '        If Not myNode Is Nothing Then
    '            If Not myNode Is trvTriggers.Nodes(0) Then
    '                ''Add nodes to the reuired Orders node
    '                AddAssociates(myNode, myNode.Parent.Text, True)
    '            End If
    '        End If


    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub

    Private Sub trOrderInfo_NodeMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles trOrderInfo.NodeMouseClick
        Try
            trOrderInfo.SelectedNode = e.Node
            _selectednode = e.Node
            ''If the mouse button clicked is of right one
            If e.Button = Windows.Forms.MouseButtons.Right Then
                If Not IsNothing(trOrderInfo.SelectedNode) Then
                    ''Validate the selected node for field node but should not be Parent or table node
                    If trOrderInfo.Nodes.Item(0) Is trOrderInfo.SelectedNode Then
                        'Try
                        '    If (IsNothing(trOrderInfo.ContextMenu) = False) Then
                        '        trOrderInfo.ContextMenu.Dispose()
                        '        trOrderInfo.ContextMenu = Nothing
                        '    End If
                        'Catch ex As Exception

                        'End Try
                        trOrderInfo.ContextMenu = Nothing
                    ElseIf trOrderInfo.SelectedNode.Parent Is trOrderInfo.Nodes.Item(0) Then
                        'Try
                        '    If (IsNothing(trOrderInfo.ContextMenu) = False) Then
                        '        trOrderInfo.ContextMenu.Dispose()
                        '        trOrderInfo.ContextMenu = Nothing
                        '    End If
                        'Catch ex As Exception

                        'End Try
                        trOrderInfo.ContextMenu = Nothing
                    Else
                        ''Clear the menu items and add the context menu
                        CntConditions.MenuItems.Clear()
                        'Try
                        '    If (IsNothing(trOrderInfo.ContextMenu) = False) Then
                        '        trOrderInfo.ContextMenu.Dispose()
                        '        trOrderInfo.ContextMenu = Nothing
                        '    End If
                        'Catch ex As Exception

                        'End Try
                        trOrderInfo.ContextMenu = CntConditions

                        Dim oMenuItem As MenuItem
                        oMenuItem = New MenuItem
                        With oMenuItem
                            .Text = "Delete Item"
                            .Tag = "DeleteItem"
                            .Shortcut = Shortcut.CtrlShiftE
                            .ShowShortcut = False
                        End With
                        CntConditions.MenuItems.Add(oMenuItem)
                        AddHandler oMenuItem.Click, AddressOf SetMenus

                        If trOrderInfo.SelectedNode.Parent.Text = "Guidelines" Then
                            oMenuItem = New MenuItem
                            With oMenuItem
                                .Text = "Edit Template"
                                .Tag = "EditTemplate"
                                .Shortcut = Shortcut.CtrlShiftT
                                .ShowShortcut = False
                            End With
                            CntConditions.MenuItems.Add(oMenuItem)
                            ''set the handler for the menu item
                            AddHandler oMenuItem.Click, AddressOf SetMenus
                            oMenuItem = Nothing
                        End If
                    End If
                End If
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Report Designer", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' Implmenting the context menu for deleting the selected item
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' 

    Public Sub SetMenus(ByVal sender As Object, ByVal e As EventArgs)
        Dim oCurrentMenu As MenuItem = CType(sender, MenuItem)
        ' Dim TemplateID As Int64
        Try
            If (trOrderInfo.SelectedNode.Level <> 0) Then ''Sandip Darade 20090309


                If oCurrentMenu.Tag = "DeleteItem" Then
                    Dim mychildnode As myTreeNode
                    mychildnode = CType(trOrderInfo.SelectedNode, myTreeNode)
                    If Not IsNothing(mychildnode) Then
                        ''If child nodes are more than one delete only the selected item
                        If mychildnode.Parent.Nodes.Count > 0 Then
                            mychildnode.Remove()
                        End If

                        ''Sandip Darade 20090309
                        ''Remove contextmenu if no item to remove
                        If (trOrderInfo.SelectedNode.Level = 0) Then
                            'Try
                            '    If (IsNothing(trOrderInfo.ContextMenu) = False) Then
                            '        trOrderInfo.ContextMenu.Dispose()
                            '        trOrderInfo.ContextMenu = Nothing
                            '    End If
                            'Catch ex As Exception

                            'End Try
                            trOrderInfo.ContextMenu = Nothing
                        End If

                    End If
                ElseIf oCurrentMenu.Tag = "EditTemplate" Then
                    'sarika DM Denormalization
                    'TemplateID = Convert.ToInt64(_selectednode.Tag)
                    'UpdateTemplate(TemplateID)
                    '      TemplateID = Convert.ToInt64(_selectednode.Tag)
                    UpdateTemplate(CType(trOrderInfo.SelectedNode, myTreeNode))

                    '----
                ElseIf oCurrentMenu.Tag = "EditTemplateTrigger" Then
                    'TemplateID = Convert.ToInt64(_selectedmynode.Tag)
                    'UpdateTemplate(TemplateID)
                    UpdateTemplate(CType(trOrderInfo.SelectedNode, myTreeNode))

                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "DM Setup", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            oCurrentMenu = Nothing
        End Try

    End Sub

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

    'sarika DM Denormalization
    Private Sub UpdateTemplate(ByVal TemplateName As String)

        ' Dim objTemplateGallery As New clsTemplateGallery
        Dim objfrmTemplateGallery As frmTemplateGallery
        Try


            blnModify = True



            ''''''''''''''''''
            objfrmTemplateGallery = New frmTemplateGallery(m_CriteriaId, TemplateName)
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

    Private Sub UpdateTemplate(ByVal mySelectedNode As myTreeNode)

        ' Dim objTemplateGallery As New clsTemplateGallery
        Dim objfrmTemplateGallery As frmTemplateGallery
        Try


            blnModify = True



            ''''''''''''''''''
            objfrmTemplateGallery = New frmTemplateGallery(True)
            Me.DMSelectedNode = mySelectedNode
            With objfrmTemplateGallery
                .DMSelectedNode = mySelectedNode
                .Text = "Modify Template"
                '.mycaller = Me
                .MdiParent = Me.ParentForm
                '.Parent = Me
                'CType(Me.MdiParent, MainMenu).ShowHideMainMenu(False, False)
                'CType(Me.ParentForm, MainMenu).pnlMainToolBar.Visible = False

                '.WindowState = FormWindowState.Maximized
                '.BringToFront()
                '.ShowDialog()

                .Show()
                .WindowState = FormWindowState.Maximized
                .BringToFront()


                mySelectedNode = .DMSelectedNode
            End With

        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Template Gallery", MessageBoxButtons.OK, MessageBoxIcon.Error)
            CType(Me.MdiParent, MainMenu).ShowHideMainMenu(True, True)
            CType(Me.ParentForm, MainMenu).pnlMainToolBar.Visible = True

            'Finally
            '  objfrmTemplateGallery = Nothing
        End Try

    End Sub
    '---

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

    Private Sub trvDrgs_NodeMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs)
        'Try
        '    trvDrgs.SelectedNode = e.Node
        '    Dim myDrugNode As New TreeNode
        '    Dim Ispresent As Boolean = False
        '    myDrugNode = trvDrgs.SelectedNode.Clone()
        '    For Each myDNode As TreeNode In trvSelectedDrugs.Nodes
        '        If myDNode.Tag = myDrugNode.Tag Then
        '            Ispresent = True
        '            Exit For
        '        End If
        '    Next
        '    If Ispresent = False Then
        '        trvSelectedDrugs.Nodes.Add(myDrugNode)
        '        myDrugNode.ImageIndex = 0
        '        myDrugNode.SelectedImageIndex = 0
        '    End If
        '    trvSelectedDrugs.ExpandAll()
        'Catch ex As Exception
        '    MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        'End Try
    End Sub

    Private Sub trvSelectedDrugs_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles trvSelectedDrugs.MouseDown
        Try

            Dim trvnode As TreeNode
            trvnode = trvSelectedDrugs.GetNodeAt(e.X, e.Y)
            trvSelectedDrugs.SelectedNode = trvnode
            If e.Button = Windows.Forms.MouseButtons.Right Then

                If IsNothing(trvnode) = False Then
                    If trvnode.Level = 0 Then
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
        Try
            Dim trvnode As New TreeNode
            trvnode = trvSelectedDrugs.SelectedNode
            If trvnode.Level = 0 Then
                trvSelectedDrugs.SelectedNode.Remove()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub cmbHistoryCategory_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbHistoryCategory.SelectedIndexChanged

    End Sub

    Private Sub cmbHistoryCategory_SelectionChangeCommitted(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbHistoryCategory.SelectionChangeCommitted
        'Fill_Histories(cmbHistoryCategory.Text)
        Fill_Histories_1(cmbHistoryCategory.Text)
    End Sub

    Private Sub trvHistoryRight_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles trvHistoryRight.KeyPress
        If e.KeyChar = Chr(13) Then


            Dim oNode As TreeNode
          


            Dim CategoryFound As Boolean = False
            Dim HistoryFound As Boolean = False

            ''Selected Current Criteria
            Dim thisNode As myTreeNode = CType(trvHistoryRight.SelectedNode, myTreeNode).Tag
            For Each CategoryNode As TreeNode In trvSelectedHistory.Nodes
                If CategoryNode.Text = thisNode.Tag Then
                    For Each HistoryNode As TreeNode In CategoryNode.Nodes
                        If HistoryNode.Text = thisNode.Text Then
                            HistoryFound = True
                            Exit For
                        End If
                    Next
                    If Not HistoryFound Then
                        Dim SelectedHistoryNode As New myTreeNode
                        SelectedHistoryNode.Text = thisNode.Text
                        SelectedHistoryNode.Tag = thisNode.Tag
                        SelectedHistoryNode.Key = thisNode.Key
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
                oNode.Text = thisNode.Tag
                oNode.Tag = cmbHistoryCategory.SelectedValue
                oNode.ImageIndex = 0
                oNode.SelectedImageIndex = 0
                Dim SelectedHistoryNode As New myTreeNode
                SelectedHistoryNode.Text = thisNode.Text
                SelectedHistoryNode.Tag = thisNode.Tag
                SelectedHistoryNode.Key = thisNode.Key
                oNode.Nodes.Add(SelectedHistoryNode)
                trvSelectedHistory.Nodes.Add(oNode)
                trvSelectedHistory.ExpandAll()
                oNode = Nothing
                trvSelectedHistory.Sort()
            End If
            ''
        End If
    End Sub

    Private Sub trvHistoryRight_NodeMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles trvHistoryRight.NodeMouseDoubleClick
        trvHistoryRight.SelectedNode = e.Node

        Dim oNode As TreeNode
        


        Dim CategoryFound As Boolean = False
        Dim HistoryFound As Boolean = False

        ''Selected Current Criteria
        Dim thisNode As myTreeNode = CType(trvHistoryRight.SelectedNode, myTreeNode).Tag
        For Each CategoryNode As TreeNode In trvSelectedHistory.Nodes
            If CategoryNode.Text = thisNode.Tag Then
                For Each HistoryNode As TreeNode In CategoryNode.Nodes
                    If HistoryNode.Text = thisNode.Text Then
                        HistoryFound = True
                        Exit For
                    End If
                Next
                If Not HistoryFound Then
                    Dim SelectedHistoryNode As New myTreeNode
                    SelectedHistoryNode.Text = thisNode.Text
                    SelectedHistoryNode.Tag = thisNode.Tag
                    SelectedHistoryNode.Key = thisNode.Key
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
            oNode.Text = thisNode.Tag
            oNode.Tag = cmbHistoryCategory.SelectedValue
            oNode.ImageIndex = 0
            oNode.SelectedImageIndex = 0
            Dim SelectedHistoryNode As New myTreeNode
            SelectedHistoryNode.Text = thisNode.Text
            SelectedHistoryNode.Tag = thisNode.Tag
            SelectedHistoryNode.Key = thisNode.Key
            oNode.Nodes.Add(SelectedHistoryNode)
            trvSelectedHistory.Nodes.Add(oNode)
            trvSelectedHistory.ExpandAll()
            oNode = Nothing
            trvSelectedHistory.Sort()
        End If
        ''
    End Sub

    Private Sub mnuDeleteHistory_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuDeleteHistory.Click
        Try

            'If gblnSMDBSetting = True And gstrSMProblem.Trim() <> "" Then

            '    If trvselectedhist.SelectedNode.Parent.Nodes.Count > 1 Then
            '        trvselectedhist.SelectedNode.Remove()
            '    Else
            '        trvselectedhist.SelectedNode.Parent.Remove()
            '    End If
            'Else

            If trvSelectedHistory.SelectedNode.Parent.Nodes.Count > 1 Then
                trvSelectedHistory.SelectedNode.Remove()
            Else
                trvSelectedHistory.SelectedNode.Parent.Remove()
            End If
            'End If


        Catch ex As Exception

        End Try
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
    '
    ''Sandip Darade 20090415
    ''Search CPT ,ICD9 by code or description
#Region "Search CPT and ICD9 by code or description "

    Private Sub FillICD9TreeView(ByVal dt As DataTable, ByVal strSearchDetails As String)

        'If IsNothing(dt) = False Then
        '    ' ''Populate ICD9 Data
        '    '' 0 = nICD9ID ,
        '    '' 1 = sICD9code+'-'+sDescription, 
        '    '' 2 = sDescription AS sDescription, 
        '    '' 3 = sICD9code AS ICD9code    
        '    strSearchDetails = strSearchDetails.Replace("'", "''").Replace("[", "").Replace("]", "").Replace("%", "").Replace("*", "")

        '    Dim dv As DataView
        '    dv = dt.DefaultView
        '    If rbICD9Code.Checked = True Then
        '        ' Filter on ICD9 Code
        '        dv.RowFilter = dv.Table.Columns("ICD9code").ColumnName & " Like '%" & Trim(strSearchDetails) & "%'"
        '    Else
        '        ' Filter on ICD9 Desc
        '        dv.RowFilter = dv.Table.Columns("sDescription").ColumnName & " Like '%" & Trim(strSearchDetails) & "%'"
        '    End If

        '    Dim dt1 As DataTable
        '    dt1 = dv.ToTable


        '    Dim oNode As TreeNode

        '    If IsNothing(dt1) = False And dt1.Rows.Count > 0 Then
        '        With trvICD9
        '            .Nodes.Clear()
        '            For k As Int16 = 0 To dt1.Rows.Count - 1
        '                oNode = New TreeNode
        '                With oNode
        '                    .Text = Convert.ToString(dt1.Rows(k)(1))
        '                    .Tag = Convert.ToString(dt1.Rows(k)(3))
        '                End With
        '                .Nodes.Add(oNode)
        '                oNode = Nothing
        '            Next
        '        End With

        '    End If

        'End If


    End Sub

    Private Sub FillCPTTreeView(ByVal dt As DataTable, ByVal strSearchDetails As String)

        'If IsNothing(dt) = False Then
        '    ' ''Populate CPT Data
        '    '' 0 = CPTID ,
        '    '' 1 = sDescription
        '    '' 2 = CPTcode+'-'+sDescription as Column1
        '    '' 3 =CPTcode    
        '    strSearchDetails = strSearchDetails.Replace("'", "''").Replace("[", "").Replace("]", "").Replace("%", "").Replace("*", "")
        '    Dim dv As DataView
        '    dv = dt.DefaultView
        '    If RBtn_CPTCode.Checked = True Then
        '        ' Filter on CPT Code
        '        dv.RowFilter = dv.Table.Columns("CPTCode").ColumnName & " Like '%" & Trim(strSearchDetails) & "%'"
        '    Else
        '        ' Filter on CPT Desc
        '        dv.RowFilter = dv.Table.Columns("sDescription").ColumnName & " Like '%" & Trim(strSearchDetails) & "%'"
        '    End If

        '    Dim dt1 As DataTable
        '    dt1 = dv.ToTable


        '    Dim oNode As TreeNode

        '    If IsNothing(dt1) = False And dt1.Rows.Count > 0 Then
        '        With trvCPT
        '            .Nodes.Clear()
        '            For k As Int16 = 0 To dt1.Rows.Count - 1
        '                oNode = New TreeNode
        '                With oNode
        '                    .Text = Convert.ToString(dt1.Rows(k)(2))
        '                    .Tag = Convert.ToString(dt1.Rows(k)(3))
        '                End With
        '                .Nodes.Add(oNode)
        '                oNode = Nothing
        '            Next
        '        End With

        '    End If

        'End If


    End Sub



#End Region 'Search CPT and ICD9 by code or description '

#Region "TreeView User control Events "

    Private Sub GloUC_trvCPT_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles GloUC_trvCPT.KeyPress

        Try
            Dim oNode As gloUserControlLibrary.myTreeNode = CType(GloUC_trvCPT.SelectedNode, gloUserControlLibrary.myTreeNode)

            'Dim oNodeToAdd As New myTreeNode
            'oNodeToAdd.Key = oNode.ID
            'oNodeToAdd.Text = oNode.Text
            'oNodeToAdd.Tag = oNode.ID
            'oNodeToAdd.Name = oNode.Description
            Dim myDrugNode As New TreeNode
            Dim Ispresent As Boolean = False
            'myDrugNode = oNodeToAdd.Clone()
            'myDrugNode.Key = oNode.ID
            myDrugNode.Text = oNode.Text
            myDrugNode.Tag = oNode.Description
            ' myDrugNode.Name = oNode.Description

            For Each myDNode As TreeNode In trvselectedCPT.Nodes
                If myDNode.Text.Replace(" ", "") = myDrugNode.Text.Replace(" ", "") Then
                    'If myDNode.Tag = myDrugNode.Tag Then
                    Ispresent = True
                    Exit For
                End If
            Next
            If Ispresent = False Then
                trvselectedCPT.Nodes.Add(myDrugNode)
                myDrugNode.ImageIndex = 0
                myDrugNode.SelectedImageIndex = 0
            End If
            trvselectedCPT.ExpandAll()
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub GloUC_trvDrugs_NodeMouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles GloUC_trvDrugs.NodeMouseDoubleClick

        Try
            Dim oNode As gloUserControlLibrary.myTreeNode = CType(e.Node, gloUserControlLibrary.myTreeNode)
            Dim oNodeToAdd As New myTreeNode

            oNodeToAdd.Key = oNode.ID
            oNodeToAdd.Tag = oNode.ID
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
            'Dim myDrugNode As New myTreeNode
            Dim Ispresent As Boolean = False
            'myDrugNode = oNodeToAdd.Clone()
            For Each myDNode As TreeNode In trvSelectedDrugs.Nodes
                'If myDNode.Text = oNodeToAdd.Text Then
                If myDNode.Text = oNodeToAdd.Text Then

                    Ispresent = True
                    Exit For
                End If
            Next
            If Ispresent = False Then
                trvSelectedDrugs.Nodes.Add(oNodeToAdd)
                oNodeToAdd.ImageIndex = 0
                oNodeToAdd.SelectedImageIndex = 0
            End If
            trvSelectedDrugs.ExpandAll()
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub GloUC_trvICD9_NodeMouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles GloUC_trvICD9.NodeMouseDoubleClick
        Try
            Dim oNode As gloUserControlLibrary.myTreeNode = CType(e.Node, gloUserControlLibrary.myTreeNode)
            Dim oNodeToAdd As New myTreeNode
            oNodeToAdd.Key = oNode.ID
            oNodeToAdd.Text = oNode.Text
            oNodeToAdd.Tag = oNode.Code.Trim
            oNodeToAdd.DrugName = oNode.Description.Trim
            'Dim myICDNode As New TreeNode
            Dim Ispresent As Boolean = False
            'myICDNode = oNodeToAdd.Clone()
            For Each myDNode As TreeNode In trvselecteICDs.Nodes
                If myDNode.Text = oNodeToAdd.Text Then
                    Ispresent = True
                    Exit For
                End If
            Next
            If Ispresent = False Then
                trvselecteICDs.Nodes.Add(oNodeToAdd)
                oNodeToAdd.ImageIndex = 0
                oNodeToAdd.SelectedImageIndex = 0
            End If
            trvselecteICDs.ExpandAll()
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub GloUC_trvCPT_NodeMouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles GloUC_trvCPT.NodeMouseDoubleClick

        Try
            Dim oNode As gloUserControlLibrary.myTreeNode = CType(e.Node, gloUserControlLibrary.myTreeNode)
            'Dim oNodeToAdd As New myTreeNode
            'oNodeToAdd.Key = oNode.ID
            'oNodeToAdd.Text = oNode.Text
            'oNodeToAdd.Tag = oNode.ID
            'oNodeToAdd.Name = oNode.Description
            Dim myDrugNode As New myTreeNode
            Dim Ispresent As Boolean = False
            'myDrugNode = oNodeToAdd.Clone()
            'myDrugNode.Key = oNode.ID
            myDrugNode.Text = oNode.Text
            myDrugNode.Tag = oNode.Code
            myDrugNode.DrugName = oNode.Description
            ' myDrugNode.Name = oNode.Description

            For Each myDNode As TreeNode In trvselectedCPT.Nodes
                If myDNode.Text.Replace(" ", "") = myDrugNode.Text.Replace(" ", "") Then
                    'If myDNode.Tag = myDrugNode.Tag Then
                    Ispresent = True
                    Exit For
                End If
            Next
            If Ispresent = False Then
                trvselectedCPT.Nodes.Add(myDrugNode)
                myDrugNode.ImageIndex = 0
                myDrugNode.SelectedImageIndex = 0
            End If
            trvselectedCPT.ExpandAll()
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub GloUC_trvAssociates_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles GloUC_trvAssociates.KeyPress
        Try
            If e.KeyChar = Chr(13) Then
                'Dim targetnode1 As myTreeNode = CType(trvCPTAssociation.SelectedNode, myTreeNode)

                Dim oNode As gloUserControlLibrary.myTreeNode = CType(GloUC_trvAssociates.SelectedNode, gloUserControlLibrary.myTreeNode)

                Dim oNodeToAdd As New myTreeNode
                oNodeToAdd.Key = oNode.ID
                oNodeToAdd.Text = oNode.Text
                oNodeToAdd.DrugName = oNode.Code
                oNodeToAdd.Dosage = oNode.Description
                oNodeToAdd.DrugForm = oNode.DrugForm
                oNodeToAdd.Route = oNode.Route ''SKU
                oNodeToAdd.Frequency = oNode.Frequency
                oNodeToAdd.NDCCode = oNode.NDCCode
                oNodeToAdd.IsNarcotics = oNode.IsNarcotics
                oNodeToAdd.Duration = oNode.Duration
                oNodeToAdd.mpid = oNode.mpid
                oNodeToAdd.DrugQtyQualifier = oNode.DrugQtyQualifier
                'Changed by Shweta 20100107 
                'Against the bug id:5611
                'bind template to the node 
                oNodeToAdd.TemplateResult = oNode.TemplateResult
                'end 20100107
                If Not oNodeToAdd Is Nothing Then
                    ''Add nodes to the reuired Orders node
                    'strParentToAssociate = strParentToAssociate.Remove(0, 1)
                    AddAssociates(oNodeToAdd, strParentToAssociate, True)
                    oNodeToAdd.Dispose()
                    oNodeToAdd = Nothing
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartDiagnosis, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, "ICD9 Association", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub GloUC_trvAssociates_NodeMouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles GloUC_trvAssociates.NodeMouseDoubleClick
        Try
            'Dim targetnode1 As myTreeNode = CType(trvCPTAssociation.SelectedNode, myTreeNode)

            Dim oNode As gloUserControlLibrary.myTreeNode = CType(e.Node, gloUserControlLibrary.myTreeNode)

            Dim oNodeToAdd As New myTreeNode
            oNodeToAdd.Key = oNode.ID



            oNodeToAdd.Text = oNode.Text
            '  oNodeToAdd.Text = oNodeToAdd.Text.Substring(0, (Len(oNodeToAdd.Text) - oNodeToAdd.Text.IndexOf("-")))
            '   oNodeToAdd.TemplateResult = Trim(Left(oNodeToAdd.Text, oNodeToAdd.Text.IndexOf("-") - 1))


            oNodeToAdd.DrugName = oNode.Code ''Vaccine name
            oNodeToAdd.Dosage = oNode.Description
            oNodeToAdd.DrugForm = oNode.DrugForm
            oNodeToAdd.Route = oNode.Route ''SKU
            oNodeToAdd.Frequency = oNode.Frequency '' TradEName IM
            oNodeToAdd.NDCCode = oNode.NDCCode ''Manufacturer IM
            oNodeToAdd.IsNarcotics = oNode.IsNarcotics
            oNodeToAdd.Duration = oNode.Duration


            oNodeToAdd.DrugQtyQualifier = oNode.DrugQtyQualifier

            'Changed by Shweta 20100107 
            'Against the bug id:5611
            'bind template to the node 
            oNodeToAdd.TemplateResult = oNode.TemplateResult
            'end 20100107
            If Not oNodeToAdd Is Nothing Then
                ''Add nodes to the reuired Orders node
                'strParentToAssociate = strParentToAssociate.Remove(0, 1)
                AddAssociates(oNodeToAdd, strParentToAssociate, True)
                oNodeToAdd.Dispose()
                oNodeToAdd = Nothing
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub GloUC_trvDrugs_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles GloUC_trvDrugs.KeyPress
        Try
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
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub GloUC_trvICD9_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles GloUC_trvICD9.KeyPress
        Try
            Dim oNode As gloUserControlLibrary.myTreeNode = CType(GloUC_trvICD9.SelectedNode, gloUserControlLibrary.myTreeNode)
            Dim oNodeToAdd As New myTreeNode
            oNodeToAdd.Key = oNode.ID
            oNodeToAdd.Text = oNode.Text
            Dim myICDNode As New TreeNode
            Dim Ispresent As Boolean = False
            myICDNode = oNodeToAdd.Clone()
            For Each myDNode As TreeNode In trvselecteICDs.Nodes
                If myDNode.Text = myICDNode.Text Then
                    Ispresent = True
                    Exit For
                End If
            Next
            If Ispresent = False Then
                trvselecteICDs.Nodes.Add(myICDNode)
                myICDNode.ImageIndex = 0
                myICDNode.SelectedImageIndex = 0
            End If
            trvselecteICDs.ExpandAll()
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub trvselecteICDs_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles trvselecteICDs.MouseDown
        Try
            Dim trvNode As TreeNode
            trvNode = trvselecteICDs.GetNodeAt(e.X, e.Y)

            trvselecteICDs.SelectedNode = trvNode
            If e.Button = Windows.Forms.MouseButtons.Right Then
                If IsNothing(trvNode) = False Then
                    If trvNode.Text <> "" Then
                        'Try
                        '    If (IsNothing(trvselecteICDs.ContextMenuStrip) = False) Then
                        '        trvselecteICDs.ContextMenuStrip.Dispose()
                        '        trvselecteICDs.ContextMenuStrip = Nothing
                        '    End If
                        'Catch ex As Exception

                        'End Try
                        trvselecteICDs.ContextMenuStrip = CmnustripICD
                    Else
                        'Try
                        '    If (IsNothing(trvselecteICDs.ContextMenuStrip) = False) Then
                        '        trvselecteICDs.ContextMenuStrip.Dispose()
                        '        trvselecteICDs.ContextMenuStrip = Nothing
                        '    End If
                        'Catch ex As Exception

                        'End Try
                        trvselecteICDs.ContextMenuStrip = Nothing
                    End If
                Else
                    'Try
                    '    If (IsNothing(trvselecteICDs.ContextMenuStrip) = False) Then
                    '        trvselecteICDs.ContextMenuStrip.Dispose()
                    '        trvselecteICDs.ContextMenuStrip = Nothing
                    '    End If
                    'Catch ex As Exception

                    'End Try
                    trvselecteICDs.ContextMenuStrip = Nothing
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub trvselectedCPT_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles trvselectedCPT.MouseDown
        Try
            Dim trvNode As TreeNode
            trvNode = trvselectedCPT.GetNodeAt(e.X, e.Y)

            trvselectedCPT.SelectedNode = trvNode
            If e.Button = Windows.Forms.MouseButtons.Right Then
                If IsNothing(trvNode) = False Then
                    If trvNode.Text <> "" Then
                        'Try
                        '    If (IsNothing(trvselectedCPT.ContextMenuStrip) = False) Then
                        '        trvselectedCPT.ContextMenuStrip.Dispose()
                        '        trvselectedCPT.ContextMenuStrip = Nothing
                        '    End If
                        'Catch ex As Exception

                        'End Try
                        trvselectedCPT.ContextMenuStrip = CmnuStripCPT
                    Else
                        'Try
                        '    If (IsNothing(trvselectedCPT.ContextMenuStrip) = False) Then
                        '        trvselectedCPT.ContextMenuStrip.Dispose()
                        '        trvselectedCPT.ContextMenuStrip = Nothing
                        '    End If
                        'Catch ex As Exception

                        'End Try
                        trvselectedCPT.ContextMenuStrip = Nothing
                    End If
                Else
                    'Try
                    '    If (IsNothing(trvselectedCPT.ContextMenuStrip) = False) Then
                    '        trvselectedCPT.ContextMenuStrip.Dispose()
                    '        trvselectedCPT.ContextMenuStrip = Nothing
                    '    End If
                    'Catch ex As Exception

                    'End Try
                    trvselectedCPT.ContextMenuStrip = Nothing
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub mnuItem_DeleteCPT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuItem_DeleteCPT.Click
        Dim trvnode As New TreeNode
        trvnode = trvselectedCPT.SelectedNode
        If trvnode.Text <> "" Then
            trvselectedCPT.SelectedNode.Remove()
        End If
    End Sub

    Private Sub mnuItem_DeleteICD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuItem_DeleteICD.Click
        Dim trvnode As New TreeNode
        trvnode = trvselecteICDs.SelectedNode
        If trvnode.Text <> "" Then
            trvselecteICDs.SelectedNode.Remove()
        End If
    End Sub

    Private Sub GloUC_trvHistory_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles GloUC_trvHistory.KeyPress
        If e.KeyChar = Chr(13) Then
            Dim oNode1 As gloUserControlLibrary.myTreeNode = CType(GloUC_trvHistory.SelectedNode, gloUserControlLibrary.myTreeNode)


            Dim oNode As TreeNode
            


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
        ' If e.KeyChar = Chr(13) Then
        Dim oNode1 As gloUserControlLibrary.myTreeNode = CType(e.Node, gloUserControlLibrary.myTreeNode)


        Dim oNode As TreeNode
        


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
        ''
        ' End If
    End Sub

#End Region
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

    Private Sub btnLabResultClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLabResultClear.Click
        'SHUBHANGI 20091001
        'USE TO CLEAR SEARCH TEXT BOX
        txtLabResultSearch.ResetText()
        txtLabResultSearch.Focus()
        C1LabResult.Select(1, 0, 1, 0, True)
    End Sub

    Private Sub btnLabClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLabClear.Click
        'SHUBHANGI 20091001
        'USE TO CLEAR SEARCH TEXT BOX
        txtLabsSearch.ResetText()
        txtLabsSearch.Focus()
        c1Labs.Select(1, 0, 1, 0, True)
    End Sub

    Private Sub btnIM_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnIM.Click
        '''''''''Integrated by chetan as on 11 oct 2010  - for IM in DM Setup
        txtSearchOrder.Text = ""
        PopulateAssocaitedInfo(6)
        strParentToAssociate = btnIM.Text
        '''''''''Integrated by chetan as on 11 oct 2010  - for IM in DM Setup
    End Sub

    Private Sub btnIM_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnIM.MouseHover
        '''''''''Added by Ujwala Atre as on 20100907 - for IM in DM Setup
        btnIM.BackgroundImage = Global.gloEMR.My.Resources.Img_LongYellow
        btnIM.BackgroundImageLayout = ImageLayout.Stretch
        '''''''''Added by Ujwala Atre as on 20100907 - for IM in DM Setup

    End Sub

    Private Sub btnIM_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnIM.MouseLeave
        '''''''''Added by Ujwala Atre as on 20100907 - for IM in DM Setup
        If btnIM.Tag = "Selected" Then
            btnIM.BackgroundImage = Global.gloEMR.My.Resources.Img_LongOrange
            btnIM.BackgroundImageLayout = ImageLayout.Stretch
        Else
            btnIM.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
            btnIM.BackgroundImageLayout = ImageLayout.Stretch
        End If
        '''''''''Added by Ujwala Atre as on 20100907 - for IM in DM Setup

    End Sub

    Private Sub btnproblemlist_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnproblemlist.Click
        FillAllCriteria()
        txtsrchprb.Text = ""
        trvselectedhist.Visible = False
        trvselectedprob.Visible = True
        trvselectedprob.BringToFront()
        trvfinprob.Nodes.Clear()
        trvsubprb.Nodes.Clear()
        blnhistory = False
        Label230.Image = Global.gloEMR.My.Resources.Problem_List01.ToBitmap()
        PnlProblemList.Visible = True
        PnlProblemList.BringToFront()
        Label230.Text = "     Selected Problem"
        cmbhistsnomed.Visible = False
        lblsnohistcat.Visible = False
        'If gblnSMDBSetting = False Then
        '    pnltrvSnowmedOff.Visible = True
        '    pnltrvfinprob.Visible = False
        '    Splitter6.Visible = False
        '    pnltrvsubprb.Visible = False
        '    '  fillProblemlistSnomadeoff()
        'Else
        '    pnltrvSnowmedOff.Visible = False
        '    pnltrvfinprob.Visible = True
        '    Splitter6.Visible = True
        '    pnltrvsubprb.Visible = True
        'End If

        If gblnSMDBSetting = True And gstrSMProblem.Trim() <> "" Then


            pnltrvSnowmedOff.Visible = False
            pnltrvfinprob.Visible = True
            Splitter6.Visible = True
            pnltrvsubprb.Visible = True



        Else
            pnltrvSnowmedOff.Visible = True
            pnltrvfinprob.Visible = False
            Splitter6.Visible = False
            pnltrvsubprb.Visible = False


        End If



        'PnlProblemList.Visible = True
        ''  cmbHistoryCategory.Focus()

        'If gblnSMDBSetting = True Then
        '    ' pnlSnoMed.Visible = True
        '    PnlProblemList.BringToFront()
        '    Pnlsnomedprb.BringToFront()
        '    'pnlHistoryMiddle.SendToBack()
        'Else
        '    PnlProblemList.BringToFront()
        '    Pnlsnomedprb.SendToBack()
        'End If




        '  btnHistory.BackgroundImage = Global.gloEMR.My.Resources.Img_LongOrange
        btnproblemlist.BackgroundImage = Global.gloEMR.My.Resources.Img_LongOrange
        btnproblemlist.BackgroundImageLayout = ImageLayout.Stretch
        btnproblemlist.Tag = "Selected"

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

    End Sub


    Private Sub FillAllCriteria()
        Try


            If LoadFirst = False Then
                fill_labs()
                Fill_RadiologyLabsC1()
                Fill_OtherInfo()
                fillProblemlistTreeHeader()
                If gblnSMDBSetting = False Or gstrSMProblem.Trim() = "" Then
                    fillProblemlistSnomadeoff()
                End If
                Fill_EditCriteria(m_CriteriaId)

            End If
        Catch ex As Exception
        Finally
            LoadFirst = True
        End Try
    End Sub

    Private Sub txtsrchprb_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtsrchprb.TextChanged
        If blnhistory = False Then '' If problem button is clicked 
            If txtsrchprb.Text.Trim.Length > 0 Then
                If gblnSMDBSetting = True And gstrSMProblem.Trim() <> "" Then
                    'Code Start-Added by kanchan on 20101112
                    Try
                        If txtsrchprb.Text.Length > 1 Then
                            If txtsrchprb.Text.Trim() <> "" Then
                                Me.Cursor = Cursors.WaitCursor
                                filltrvfindProb(txtsrchprb.Text.Trim())
                                Me.Cursor = Cursors.Default
                            End If
                        Else
                            trvfinprob.Nodes.Clear()
                        End If
                    Catch ex As Exception

                    Finally
                        Me.Cursor = Cursors.Default
                    End Try
                    'Code End-Added by kanchan on 20101112

                Else
                    fillProblemlistSnomadeoff(txtsrchprb.Text)
                End If
            End If
            'Else '' If History button is clicked  
            'If txtsrchprb.Text.Trim.Length > 0 Then
            'If gblnSMDBSetting = True And gstrSMHistory.Trim() <> "" Then
            '    'Code Start-Added by kanchan on 20101112
            '    Try
            '        If txtsrchprb.Text.Length > 1 Then
            '            If txtsrchprb.Text.Trim() <> "" Then
            '                Me.Cursor = Cursors.WaitCursor
            '                filltrvfindProb(txtsrchprb.Text.Trim())
            '                Me.Cursor = Cursors.Default
            '            End If
            '        Else
            '            trvfinprob.Nodes.Clear()
            '        End If
            '    Catch ex As Exception

            '    Finally
            '        Me.Cursor = Cursors.Default
            '    End Try
            '    'Code End-Added by kanchan on 20101112

            '    'Else
            '    ' fillProblemlistSnomadeoff(txtsrchprb.Text)
            'End If
            'End If
        End If

    End Sub

    Private Sub trvselectedprob_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles trvselectedprob.MouseDown
        '' chetan added 
        Try

            Dim trvnode As TreeNode
            If Not IsNothing(trvselectedprob) Then
                trvnode = trvselectedprob.GetNodeAt(e.X, e.Y)
                trvselectedprob.SelectedNode = trvnode
                If e.Button = Windows.Forms.MouseButtons.Right Then
                    If IsNothing(trvnode) = False Then
                        If Not IsNothing(ContextMenuProblem) Then
                            If trvnode.Level > 0 Then
                                'Try
                                '    If (IsNothing(trvselectedprob.ContextMenuStrip) = False) Then
                                '        trvselectedprob.ContextMenuStrip.Dispose()
                                '        trvselectedprob.ContextMenuStrip = Nothing
                                '    End If
                                'Catch ex As Exception

                                'End Try
                                trvselectedprob.ContextMenuStrip = ContextMenuProblem
                            Else
                                'Try
                                '    If (IsNothing(trvselectedprob.ContextMenuStrip) = False) Then
                                '        trvselectedprob.ContextMenuStrip.Dispose()
                                '        trvselectedprob.ContextMenuStrip = Nothing
                                '    End If
                                'Catch ex As Exception

                                'End Try
                                trvselectedprob.ContextMenuStrip = Nothing
                            End If
                        Else
                            'Try
                            '    If (IsNothing(trvselectedprob.ContextMenuStrip) = False) Then
                            '        trvselectedprob.ContextMenuStrip.Dispose()
                            '        trvselectedprob.ContextMenuStrip = Nothing
                            '    End If
                            'Catch ex As Exception

                            'End Try
                            trvselectedprob.ContextMenuStrip = Nothing
                        End If
                    Else
                        'Try
                        '    If (IsNothing(trvselectedprob.ContextMenuStrip) = False) Then
                        '        trvselectedprob.ContextMenuStrip.Dispose()
                        '        trvselectedprob.ContextMenuStrip = Nothing
                        '    End If
                        'Catch ex As Exception

                        'End Try
                        trvselectedprob.ContextMenuStrip = Nothing
                    End If
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub trvFindings_NodeMouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs)

    End Sub

    Private Sub trvFindings_NodeMouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs)


    End Sub

    Private Sub trvSubType_NodeMouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs)
        'Try

        '    '  Dim oNode1 As gloUserControlLibrary.myTreeNode = CType(e.Node, gloUserControlLibrary.myTreeNode)

        '    trvSubType.SelectedNode = e.Node
        '    trvSelectedHistory.BeginUpdate()
        '    Dim SelectedHistoryNode As New TreeNode
        '    Dim oNode As TreeNode

        '    SelectedHistoryNode = trvSubType.SelectedNode.Clone
        '    SelectedHistoryNode.ImageIndex = 11
        '    SelectedHistoryNode.SelectedImageIndex = 11
        '    Dim CategoryFound As Boolean = False
        '    Dim HistoryFound As Boolean = False

        '    ''Selected Current Criteria
        '    For Each CategoryNode As TreeNode In trvSelectedHistory.Nodes
        '        If CategoryNode.Text = cmbHistoryCategory.Text Then
        '            For Each HistoryNode As TreeNode In CategoryNode.Nodes
        '                If HistoryNode.Text = SelectedHistoryNode.Text Then
        '                    HistoryFound = True
        '                    Exit For
        '                End If
        '            Next
        '            If Not HistoryFound Then
        '                CategoryNode.ImageIndex = 0
        '                CategoryNode.SelectedImageIndex = 0

        '                Dim otempnode As New TreeNode
        '                otempnode.ImageIndex = 11
        '                otempnode.SelectedImageIndex = 11
        '                otempnode.Text = SelectedHistoryNode.Text
        '                CategoryNode.Nodes.Add(otempnode)

        '                ''CategoryNode.Nodes.Add(SelectedHistoryNode)

        '                CategoryNode.Expand()
        '                trvSelectedHistory.Sort()
        '            End If
        '            CategoryFound = True
        '            Exit For
        '        End If
        '    Next

        '    If Not CategoryFound Then
        '        oNode = New TreeNode
        '        oNode.Text = cmbHistoryCategory.Text
        '        oNode.Tag = cmbHistoryCategory.SelectedValue
        '        oNode.ImageIndex = 0
        '        oNode.SelectedImageIndex = 0

        '        Dim otempnode As New TreeNode
        '        otempnode.ImageIndex = 11
        '        otempnode.SelectedImageIndex = 11
        '        otempnode.Text = SelectedHistoryNode.Text
        '        oNode.Nodes.Add(otempnode)
        '        ''oNode.Nodes.Add(SelectedHistoryNode)
        '        trvSelectedHistory.Nodes.Add(oNode)
        '        trvSelectedHistory.ExpandAll()
        '        oNode = Nothing
        '        trvSelectedHistory.Sort()
        '    End If
        '    ''
        'Catch ex As Exception
        'Finally
        '    trvSelectedHistory.EndUpdate()
        'End Try
    End Sub

    Private Sub trvproblem_NodeMouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles trvproblem.NodeMouseClick
        Dim a As Integer = 0
    End Sub

    Private Sub trvproblem_NodeMouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles trvproblem.NodeMouseDoubleClick
        Dim b As Integer = 0
    End Sub

    Private Sub trvprobright_NodeMouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles trvprobright.NodeMouseClick
        Dim a As Integer = 0
    End Sub

    Private Sub trvprobright_NodeMouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles trvprobright.NodeMouseDoubleClick

    End Sub

    Private Sub trvfinprob_NodeMouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles trvfinprob.NodeMouseClick
        If e.Button = Windows.Forms.MouseButtons.Right Then
            With trvfinprob
                Dim r As Integer = .HitTest(e.X, e.Y).Node.Index
                If r >= 0 Then

                    trvfinprob.SelectedNode = trvfinprob.GetNodeAt(e.X, e.Y)
                    'Try
                    '    If (IsNothing(trvfinprob.ContextMenuStrip) = False) Then
                    '        trvfinprob.ContextMenuStrip.Dispose()
                    '        trvfinprob.ContextMenuStrip = Nothing
                    '    End If
                    'Catch ex As Exception

                    'End Try
                    trvfinprob.ContextMenuStrip = cntFindings

                Else
                    'Try
                    '    If (IsNothing(trvfinprob.ContextMenuStrip) = False) Then
                    '        trvfinprob.ContextMenuStrip.Dispose()
                    '        trvfinprob.ContextMenuStrip = Nothing
                    '    End If
                    'Catch ex As Exception

                    'End Try
                    trvfinprob.ContextMenuStrip = Nothing
                End If
            End With
        End If
    End Sub

    Private Sub trvsubprb_NodeMouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles trvsubprb.NodeMouseClick

    End Sub

    Private Sub trvfinprob_NodeMouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles trvfinprob.NodeMouseDoubleClick
        'oSnoMed.FillSubtypeHierarchy(e.Node.Tag, e.Node.Text, trvsubprb)
        'If trvsubprb.Nodes.Count > 0 Then
        '    trvsubprb.SelectedNode = trvsubprb.Nodes(0)
        '    trvsubprb.Focus()
        'End If

    End Sub

    Private Sub mnu_AddFindings_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnu_AddFindings.Click

        Dim oNode As TreeNode = trvfinprob.SelectedNode

        If Not IsNothing(oNode) Then


            ' Dim mynode As New gloSnoMed.myTreeNode
            ' trvSubType.SelectedNode = oNode
            If blnhistory = False Then '' for problem it is checked  
                Try
                    trvselectedprob.Visible = True
                    trvselectedhist.Visible = False
                    trvselectedprob.BeginUpdate()
                    Dim SelectedHistoryNode As New TreeNode
                    ' Dim oNode As TreeNode

                    SelectedHistoryNode = oNode.Clone
                    SelectedHistoryNode.ImageIndex = 11
                    SelectedHistoryNode.SelectedImageIndex = 11
                    Dim CategoryFound As Boolean = False
                    Dim HistoryFound As Boolean = False

                    ''Selected Current Criteria
                    For Each CategoryNode As TreeNode In trvselectedprob.Nodes
                        ' If CategoryNode.Text = cmbHistoryCategory.Text Then
                        For Each HistoryNode As TreeNode In CategoryNode.Nodes
                            If HistoryNode.Text = SelectedHistoryNode.Text Then
                                HistoryFound = True
                                Exit For
                            End If
                        Next
                        If Not HistoryFound Then
                            CategoryNode.ImageIndex = 0
                            CategoryNode.SelectedImageIndex = 0
                            CategoryNode.Nodes.Add(SelectedHistoryNode)
                            CategoryNode.Expand()
                            trvselectedprob.Sort()
                        End If
                        CategoryFound = True
                        Exit For
                        'End If
                    Next

                    If Not CategoryFound Then
                        oNode = New TreeNode
                        oNode.Text = cmbHistoryCategory.Text
                        oNode.Tag = cmbHistoryCategory.SelectedValue
                        oNode.ImageIndex = 0
                        oNode.SelectedImageIndex = 0
                        oNode.Nodes.Add(SelectedHistoryNode)
                        trvselectedprob.Nodes.Add(oNode)
                        trvselectedprob.ExpandAll()
                        oNode = Nothing
                        trvselectedprob.Sort()
                    End If
                    ''
                Catch ex As Exception
                Finally
                    trvselectedprob.EndUpdate()
                End Try

            Else
                '' for history 
                trvselectedprob.Visible = False
                trvselectedhist.Visible = True
                Try
                    trvselectedhist.BeginUpdate()
                    Dim SelectedHistoryNode As New TreeNode
                    ' Dim oNode As TreeNode

                    SelectedHistoryNode = oNode.Clone
                    SelectedHistoryNode.ImageIndex = 11
                    SelectedHistoryNode.SelectedImageIndex = 11
                    Dim CategoryFound As Boolean = False
                    Dim HistoryFound As Boolean = False

                    ''Selected Current Criteria
                    For Each CategoryNode As TreeNode In trvselectedhist.Nodes
                        If CategoryNode.Text = cmbhistsnomed.Text Then
                            For Each HistoryNode As TreeNode In CategoryNode.Nodes
                                If HistoryNode.Text = SelectedHistoryNode.Text Then
                                    HistoryFound = True
                                    Exit For
                                End If
                            Next
                            If Not HistoryFound Then
                                CategoryNode.ImageIndex = 0
                                CategoryNode.SelectedImageIndex = 0
                                CategoryNode.Nodes.Add(SelectedHistoryNode)
                                CategoryNode.Expand()
                                trvselectedhist.Sort()
                            End If
                            CategoryFound = True
                            Exit For
                        End If
                    Next
                  

                    If Not CategoryFound Then
                        oNode = New TreeNode
                        oNode.Text = cmbhistsnomed.Text
                        oNode.Tag = cmbhistsnomed.SelectedValue
                        oNode.ImageIndex = 0
                        oNode.SelectedImageIndex = 0
                        oNode.Nodes.Add(SelectedHistoryNode)
                        trvselectedhist.Nodes.Add(oNode)
                        trvselectedhist.ExpandAll()
                        oNode = Nothing
                        trvselectedhist.Sort()
                    End If
                    ''
                Catch ex As Exception
                Finally
                    trvselectedhist.EndUpdate()
                End Try




            End If


        End If


      
    End Sub

    Private Sub trvsubprb_NodeMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles trvsubprb.NodeMouseDoubleClick


        '  Dim oNode1 As gloUserControlLibrary.myTreeNode = CType(e.Node, gloUserControlLibrary.myTreeNode)

        trvsubprb.SelectedNode = e.Node
        trvselectedprob.BeginUpdate()
        Dim SelectedProbNode As New TreeNode
        Dim oNode As TreeNode

        SelectedProbNode = trvsubprb.SelectedNode.Clone
        SelectedProbNode.ImageIndex = 11
        SelectedProbNode.SelectedImageIndex = 11
        Dim CategoryFound As Boolean = False
        Dim HistoryFound As Boolean = False
        If blnhistory = False Then '' for problem  

            Try
                ''Selected Current Criteria
                For Each CategoryNode As TreeNode In trvselectedprob.Nodes
                    ' If CategoryNode.Text = cmbHistoryCategory.Text Then
                    For Each HistoryNode As TreeNode In CategoryNode.Nodes
                        If HistoryNode.Text = SelectedProbNode.Text Then
                            HistoryFound = True
                            Exit For
                        End If
                    Next
                    If Not HistoryFound Then
                        CategoryNode.ImageIndex = 0
                        CategoryNode.SelectedImageIndex = 0

                        Dim otempnode As New TreeNode
                        otempnode.ImageIndex = 11
                        otempnode.SelectedImageIndex = 11
                        otempnode.Text = SelectedProbNode.Text
                        CategoryNode.Nodes.Add(otempnode)

                        ''CategoryNode.Nodes.Add(SelectedProbNode)

                        CategoryNode.Expand()
                        trvselectedprob.Sort()
                    End If
                    CategoryFound = True
                    Exit For
                    ' End If
                Next

                If Not CategoryFound Then
                    oNode = New TreeNode
                    oNode.Text = "ProblemList"
                    oNode.Tag = "-1"
                    oNode.ImageIndex = 0
                    oNode.SelectedImageIndex = 0

                    Dim otempnode As New TreeNode
                    otempnode.ImageIndex = 11
                    otempnode.SelectedImageIndex = 11
                    otempnode.Text = SelectedProbNode.Text
                    oNode.Nodes.Add(otempnode)
                    ''oNode.Nodes.Add(SelectedProbNode)
                    trvselectedprob.Nodes.Add(oNode)
                    trvselectedprob.ExpandAll()
                    oNode = Nothing
                    trvselectedprob.Sort()
                End If
                ''
            Catch ex As Exception
            Finally
                trvselectedprob.EndUpdate()
            End Try

        Else

            Try
                trvselectedhist.BeginUpdate()
                Dim SelectedHistoryNode As New TreeNode
                ' Dim oNode As TreeNode

                SelectedHistoryNode = e.Node.Clone
                SelectedHistoryNode.ImageIndex = 11
                SelectedHistoryNode.SelectedImageIndex = 11



                ''Selected Current Criteria
                For Each CategoryNode As TreeNode In trvselectedhist.Nodes
                    If CategoryNode.Text = cmbhistsnomed.Text Then
                        For Each HistoryNode As TreeNode In CategoryNode.Nodes
                            If HistoryNode.Text = SelectedHistoryNode.Text Then
                                HistoryFound = True
                                Exit For
                            End If
                        Next
                        If Not HistoryFound Then
                            'CategoryNode.ImageIndex = 11
                            'CategoryNode.SelectedImageIndex = 11
                            Dim otempnode As New TreeNode
                            otempnode.ImageIndex = 11
                            otempnode.SelectedImageIndex = 11
                            otempnode.Text = SelectedProbNode.Text

                            CategoryNode.Nodes.Add(otempnode)
                            CategoryNode.Expand()
                            trvselectedhist.Sort()
                        End If
                        CategoryFound = True
                        Exit For
                    End If
                Next


                If Not CategoryFound Then
                    oNode = New TreeNode
                    oNode.Text = cmbhistsnomed.Text
                    oNode.Tag = cmbhistsnomed.SelectedValue
                    oNode.ImageIndex = 0
                    oNode.SelectedImageIndex = 0
                    Dim otempnode As New TreeNode
                    otempnode.ImageIndex = 11
                    otempnode.SelectedImageIndex = 11
                    otempnode.Text = SelectedProbNode.Text

                    oNode.Nodes.Add(otempnode)
                    trvselectedhist.Nodes.Add(oNode)
                    trvselectedhist.ExpandAll()
                    oNode = Nothing
                    trvselectedhist.Sort()
                End If

            Catch ex As Exception
            Finally
                trvselectedhist.EndUpdate()
            End Try

        End If



    End Sub

    Private Sub btnproblemlist_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnproblemlist.MouseHover
        btnproblemlist.BackgroundImage = Global.gloEMR.My.Resources.Img_LongYellow
        btnproblemlist.BackgroundImageLayout = ImageLayout.Stretch

    End Sub

    Private Sub btnproblemlist_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnproblemlist.MouseLeave
        If btnproblemlist.Tag = "Selected" Then
            btnproblemlist.BackgroundImage = Global.gloEMR.My.Resources.Img_LongOrange
            btnproblemlist.BackgroundImageLayout = ImageLayout.Stretch
        Else
            btnproblemlist.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
            btnproblemlist.BackgroundImageLayout = ImageLayout.Stretch
        End If

    End Sub

    Private Sub trvSnowmedOff_NodeMouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles trvSnowmedOff.NodeMouseDoubleClick
        Try
            trvSnowmedOff.SelectedNode = e.Node
            trvselectedprob.BeginUpdate()
            Dim SelectedProbNode As New TreeNode
            Dim oNode As TreeNode

            SelectedProbNode = trvSnowmedOff.SelectedNode.Clone
            SelectedProbNode.ImageIndex = 11
            SelectedProbNode.SelectedImageIndex = 11
            Dim CategoryFound As Boolean = False
            Dim HistoryFound As Boolean = False

            ''Selected Current Criteria
            ' For Each CategoryNode As TreeNode In trvSnowmedOff.Nodes
            ' If CategoryNode.Text = cmbHistoryCategory.Text Then
            If trvselectedprob.Nodes.Count > 0 Then
                Dim pnode As TreeNode = trvselectedprob.Nodes(0)
                For Each HistoryNode As TreeNode In pnode.Nodes
                    If HistoryNode.Text = SelectedProbNode.Text Then
                        Exit Sub
                    End If
                Next
            End If
            'If Not HistoryFound Then
            '    CategoryNode.ImageIndex = 0
            '    CategoryNode.SelectedImageIndex = 0

            '    Dim otempnode As New TreeNode
            '    otempnode.ImageIndex = 11
            '    otempnode.SelectedImageIndex = 11
            '    otempnode.Text = SelectedProbNode.Text
            '    CategoryNode.Nodes.Add(otempnode)

            '    ''CategoryNode.Nodes.Add(SelectedProbNode)

            '    CategoryNode.Expand()
            '    ' trvselectedprob.Sort()
            'End If
            'CategoryFound = True
            'Exit For
            '' End If
            '  Next
            oNode = New TreeNode
            If trvselectedprob.Nodes.Count = 0 Then

                oNode.Text = "ProblemList"
                oNode.Tag = "-1"
                oNode.ImageIndex = 0
                oNode.SelectedImageIndex = 0

                Dim otempnode As New TreeNode
                otempnode.ImageIndex = 11
                otempnode.SelectedImageIndex = 11
                otempnode.Text = SelectedProbNode.Text
                oNode.Nodes.Add(otempnode)
                ''oNode.Nodes.Add(SelectedProbNode)
                trvselectedprob.Nodes.Add(oNode)
                trvselectedprob.ExpandAll()
                oNode = Nothing
                '  trvselectedprob.Sort()
            Else
                oNode = trvselectedprob.Nodes(0)
            End If
            oNode.Nodes.Add(SelectedProbNode)
            ' trvselectedprob.Nodes.Add(SelectedProbNode)
            trvselectedprob.ExpandAll()
            ''
        Catch ex As Exception
        Finally
            trvselectedprob.EndUpdate()
        End Try
    End Sub

    Private Sub btnclrprb_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclrprb.Click
        txtsrchprb.Text = ""
        txtsrchprb.Focus()
        trvfinprob.Nodes.Clear()
        trvsubprb.Nodes.Clear()
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub cmbhistsnomed_SelectionChangeCommitted(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbhistsnomed.SelectionChangeCommitted
        txtsrchprb.Text = ""
        txtsrchprb.Focus()
        trvfinprob.Nodes.Clear()
        trvsubprb.Nodes.Clear()
    End Sub

    Private Sub trvselectedhist_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles trvselectedhist.MouseDown
        '' chetan added 
        Try

            Dim trvnode As TreeNode
            If Not IsNothing(trvselectedhist) Then
                trvnode = trvselectedhist.GetNodeAt(e.X, e.Y)
                trvselectedhist.SelectedNode = trvnode
                If e.Button = Windows.Forms.MouseButtons.Right Then
                    If IsNothing(trvnode) = False Then
                        If Not IsNothing(ContextMenuProblem) Then
                            If trvnode.Level > 0 Then
                                'Try
                                '    If (IsNothing(trvselectedhist.ContextMenuStrip) = False) Then
                                '        trvselectedhist.ContextMenuStrip.Dispose()
                                '        trvselectedhist.ContextMenuStrip = Nothing
                                '    End If
                                'Catch ex As Exception

                                'End Try
                                trvselectedhist.ContextMenuStrip = ContextMenuHistory
                            Else
                                'Try
                                '    If (IsNothing(trvselectedhist.ContextMenuStrip) = False) Then
                                '        trvselectedhist.ContextMenuStrip.Dispose()
                                '        trvselectedhist.ContextMenuStrip = Nothing
                                '    End If
                                'Catch ex As Exception

                                'End Try
                                trvselectedhist.ContextMenuStrip = Nothing
                            End If
                        Else
                            trvselectedhist.ContextMenuStrip = Nothing
                        End If
                    Else
                        'Try
                        '    If (IsNothing(trvselectedhist.ContextMenuStrip) = False) Then
                        '        trvselectedhist.ContextMenuStrip.Dispose()
                        '        trvselectedhist.ContextMenuStrip = Nothing
                        '    End If
                        'Catch ex As Exception

                        'End Try
                        trvselectedhist.ContextMenuStrip = Nothing
                    End If
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Public Function GetValues(ByVal id As Int64, ByVal min As Boolean) As String
        Dim _strSQL As String = ""
        Dim _Result As String = ""
        Dim oDB As New gloStream.gloDataBase.gloDataBase
        Try
            'Criteria Master Record
            If (min) Then
                _strSQL = " select dm_mst_ageMin from DM_Criteria_MST Where dm_mst_id = " & id & ""
            Else
                _strSQL = " select dm_mst_AgeMax from DM_Criteria_MST Where dm_mst_id = " & id & ""
            End If

            oDB.Connect(GetConnectionString)
            _Result = oDB.ExecuteQueryScaler(_strSQL)
            oDB.Disconnect()
            'Return Object

            Return _Result
        Catch
            Return Nothing
        End Try

    End Function

    Private Sub txtCity_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCity.KeyPress
        If Char.IsLetterOrDigit(e.KeyChar) Or (e.KeyChar = ChrW(8)) Then
            e.Handled = False
        Else
            e.Handled = True
        End If
    End Sub

    Private Sub txtZip_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtZip.KeyPress
        If Char.IsLetterOrDigit(e.KeyChar) Or (e.KeyChar = ChrW(8)) Then
            e.Handled = False
        Else
            e.Handled = True
        End If
    End Sub

    'Private Sub txtName_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtName.KeyPress
    '    If Char.IsLetterOrDigit(e.KeyChar) Or (e.KeyChar = ChrW(8)) Then
    '        e.Handled = False
    '    Else
    '        e.Handled = True
    '    End If
    'End Sub

    'Private Sub txtMessage_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMessage.KeyPress
    '    If Char.IsLetterOrDigit(e.KeyChar) Or (e.KeyChar = ChrW(8)) Then
    '        e.Handled = False
    '    Else
    '        e.Handled = True
    '    End If
    'End Sub

    Private Sub cmbState_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cmbState.KeyPress
        If Char.IsLetterOrDigit(e.KeyChar) Or (e.KeyChar = ChrW(8)) Then
            e.Handled = False
        Else
            e.Handled = True
        End If
    End Sub

    Private Sub cmbEmpStatus_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cmbEmpStatus.KeyPress
        If Char.IsLetterOrDigit(e.KeyChar) Or (e.KeyChar = ChrW(8)) Then
            e.Handled = False
        Else
            e.Handled = True
        End If
    End Sub
End Class
