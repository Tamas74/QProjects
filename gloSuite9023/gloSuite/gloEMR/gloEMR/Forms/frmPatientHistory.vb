Imports System.IO
Imports C1.Win.C1FlexGrid
Imports gloEMRGeneralLibrary.gloEMRActors
Imports gloEMRGeneralLibrary.gloEMRPrescription
Imports gloEMRGeneralLibrary.gloEMRMedication
Imports gloEMR.gloEMRWord
Imports System.Data.SqlClient


Public Class frmHistory

    Inherits System.Windows.Forms.Form
    Implements gloVoice
    Implements IPatientContext

    '19-Apr-13 Aniket: Resolving Memory Leak Issues
    Dim ofrmDiagnosisList As frmViewListControl
    Dim ofrmCPTList As frmViewListControl

    Private oDiagnosisListControl As gloListControl.gloListControl
    Private oCPTListControl As gloListControl.gloListControl
    Dim sLoginName As String
    Dim blnIsLoad As Boolean = False
    Public blncancel As Boolean
    Private voicecol As DNSTools.DgnStrings
    Dim objclsPatientHistory As New clsPatientHistory
    Dim oSnoMed As gloSnoMed.ClsGeneral
    Private WithEvents frmRxMeds As frmPrescription
    Private HistoryVoiceCol As DNSTools.DgnStrings
    Dim dsHistory As DataSet = Nothing
    Dim _dsTemp As DataSet = Nothing
    '''' Category Name
    Private BtnText As String
    '''' CategoryID
    Private BtnTag As Long
    Dim dt As DataTable
    Private m_VisitID As Long
    Private m_VisitDate As DateTime
    Private m_strLoginName As String

    Dim m_PatientID As Long
    Public gblnIsFindingAdd As Boolean = False
    Public Shared IsOpen As Boolean = False
    '' Flag sets when Privious Histories are open 
    Public Shared blnModify As Boolean = False

    '' Sets if Changes Made In History
    Public Shared blnChangesMade As Boolean = False

    '' Button of Categories which are dynamically going to add
    Friend WithEvents objBtn As System.Windows.Forms.Button

    '' Dim oCurDoc As Word.Document
    Dim _Defination1 As String
    Dim _ICD91 As String
    Dim intCheck As Integer = 0

    '' 20070720
    Dim _blnRecordLock As Boolean '' To the set record lock
    Dim _RecordLock As Boolean
    ''  
    Public myLetter As frmPatientLetter
    Public myNurse As frmNurseNotes
    '<Code commented and Added by dipak 20090917 for changing datatype of myCaller
    ' because my caller may contain object of other forms as requirement for implementing 
    Public myCaller As frmPatientExam
    Public myCaller1 As Object
    'end added by dipak 20090917

    ''''''''''Added by Ujwala - for Snomed Implementation From Immunization in History - as on 20101008
    Public mycallerImm As frmImTransaction
    Private _sReaction As String = ""
    ''''''''''Added by Ujwala - for Snomed Implementation From Immunization in History - as on 20101008

    Public myCallerSynopsis As frmPatientSynopsis
    '''' Boolean variable to check that, form is open from Main form or from Patient Exam
    '''' This variable is used for voice purpose
    Public blnOpenFromExam As Boolean = False

    Public blnShowMessageBox As Boolean = True
    Public blnShowAddModeMessageBox As Boolean = True
    Private FormLevelLockID As Long = -1 '' To the set record lock

    ' For opening CategoryMST form     
    Dim _IsFrmImm As Boolean = False

    '19-Apr-13 Aniket: Resolving Memory Leak Issues
    Dim dtReview As DataTable

    ''Added on 20101006
    Dim _strAllergy As String
    Dim _strSnoMedID As String
    Dim _strConceptID As String
    Dim _strDescriptionID As String
    Dim _strICD9 As String
    Dim _strSnomedDefination As String
    Dim _RxNormID As String
    Dim _NDCCode As String
    ''
    Private strPatientCode As String
    Private strPatientFirstName As String
    Private strPatientLastName As String
    Private strPatientDOB As String
    Private strPatientAge As String
    Private strPatientGender As String
    Private strPatientMaritalStatus As String

    Dim oclsLogin As clsLogin
    Dim strLoginNameDetails As String = ""
    Public strbuttonStatus As String = ""
    Dim _HxVisitID As Long
    Dim _isMakeAsCurrent As Boolean = False

    Dim _isMedicationHistoryModify As Boolean = False
    Dim dsCurrent As DataSet
    Dim _isMessage As Boolean = False
    Dim _isDoubleclickPrevHistory As Boolean = False
    Dim IsCloseClickFlagForCommentValidation As Boolean = False
    Dim bln_Loadcategory As Boolean

    '' SUDHIR 20090422 '' 
    Private isHistoryModified As Boolean = False
    Private isHistoryLoading As Boolean = True

    Private arrDataDictionary As New ArrayList ''Contains DataDictionary Items to be DELETED.

    Dim blnDIbtnClick As Boolean = False
    Dim RXnormAlergicDrugNDCCode As String = ""
    Dim btntype As Int16
    Private WithEvents _RxBusinessLayer As RxBusinesslayer
    Private WithEvents _MedBusinessLayer As MedicationBusinessLayer
    Dim strUncodedDrugs As New System.Text.StringBuilder

    Dim mydtButton As DataTable
    ''chetan added 
    Private WithEvents dgCustomGrid As CustomTask

    ''Split Screen project 20121108 Mayuri
    'Friend WithEvents UiPanelManager1 As Janus.Windows.UI.Dock.UIPanelManager
    Friend WithEvents uiPanSplitScreen_History As New Janus.Windows.UI.Dock.UIPanelGroup
    Dim allocatedInGlobalForuiPanelSplitScreen As Boolean = True
    Dim clsSplit_History As New gloEMRGeneralLibrary.clsSplitScreen
    Dim _isPanelClick As Boolean = False
    ''end 20121108
    Dim objCriteria As New DocCriteria
    Dim objWord As New clsWordDocument

    Private Const strHistorySource As String = "gloEMR"

    Dim nProviderAssociationID As Int64 = 0
    Dim sProviderTaxID As String = ""
    Dim dtcategoryType As DataTable = Nothing
    Dim blnmedRecupdated As Boolean = False
    ''''''''''Added by Ujwala - for Snomed Implementation From Immunization in History - as on 20101008
    Public Property Reaction() As String
        Get
            Return _sReaction
        End Get
        Set(ByVal value As String)
            _sReaction = value
        End Set
    End Property
    Private _AllergyIntelorenceType As String = ""
    Public Property AllergyIntelorenceType As String
        Get
            Return _AllergyIntelorenceType
        End Get
        Set(ByVal value As String)
            _AllergyIntelorenceType = value
        End Set
    End Property
    Dim _IsOBHistory As Boolean = False
    ''Track Which Category Modified
    Dim _isOBHistoryModified As Boolean = False


    Dim arrOB As ArrayList
    ''''''''''Added by Ujwala - for Snomed Implementation From Immunization in History - as on 20101008

#Region " C1 Constants "
    Dim Col_CategoryID As Integer = 0
    Dim Col_DrugID As Integer = 1 '' Need For Allergy
    Dim Col_VisitID As Integer = 2
    Dim Col_UserID As Integer = 3
    Dim col_UserName As Integer = 4
    Dim Col_HistoryCategory As Integer = 5
    Dim Col_HistoryCategory_Hidden As Integer = 6
    Dim Col_HistoryItem As Integer = 7
    Dim Col_Comments As Integer = 8
    Dim Col_Reaction As Integer = 9
    '' Chetan Added 
    Dim Col_Button As Integer = 10
    '' Chetan Added 
    ''Added Rahul on 20101004
    Dim Col_SomkingStatus As Integer = 11
    Dim Col_SmokingButton = 12
    ''
    Dim Col_Active As Integer = 13
    Dim Col_MedicalConditionID As Integer = 14

    Dim Col_Dosage As Integer = 15 '' Need For Allergy
    Dim Col_NDCCode As Integer = 16 '' Need For Allergy
    Dim Col_MPID As Integer = 17 '' Need For Allergy
    Dim Col_DOE_Allergy As Integer = 18

    Dim Col_ConceptId As Integer = 19 '' Need For Allergy
    Dim Col_DescId As Integer = 20 '' Need For Allergy
    Dim Col_SnoMedID As Integer = 21
    Dim Col_Description As Integer = 22

    Dim Col_ICD9 As Integer = 23    'Added by kanchan on 20100526
    Dim Col_RxNorm As Integer = 24 'Added by kanchan on 20100828 for RXNorm

    Dim Col_COUNT As Integer = 25

    'Chetan Added  Setting Value form Custom Grid open on button click
    Private Col_Check As Integer = 0
    Private Col_Name As Integer = 1
    Private Col_DGCustCnt As Integer = 2
    'Chetan Added Setting Value form Custom Grid  open on button click
    ''

    Private Col_ICD9CPTCode As Integer = 0
    Private Col_ICD9CPTDEscription As Integer = 1
    Private Col_CustCnt As Integer = 2
    ''
#End Region
    ''New Variables-20110623    
    Dim Col_HCategory As Integer = 0
    Dim Col_HHidden As Integer = 1
    Dim Col_HsHistoryItem As Integer = 2
    Dim Col_HsComments As Integer = 3
    Dim Col_HsReaction As Integer = 4
    Dim Col_HButton As Integer = 5
    Dim Col_HSmokingStatus As Integer = 6
    Dim Col_HSmokeButton As Integer = 7
    Dim Col_HsActive As Integer = 8
    Dim Col_HnVisitID As Integer = 9
    Dim Col_HdtVisitDate As Integer = 10
    Dim Col_HnDrugID As Integer = 11
    Dim Col_HMedicalConditionID As Integer = 12
    Dim Col_HsDrugName As Integer = 13
    Dim Col_HsDosage As Integer = 14
    Dim Col_HsNDCCode As Integer = 15
    Dim Col_Hnmpid As Integer = 16
    Dim col_HOnsetDate As Integer = 17
    Dim Col_DateResolved As Integer = 18

    Dim Col_HDOE_Allergy As Integer = 19
    Dim Col_HsConceptID As Integer = 20
    Dim Col_HsDescriptionID As Integer = 21
    Dim Col_HsSnomedID As Integer = 22
    Dim Col_HsDescription As Integer = 23
    Dim Col_HsICD9 As Integer = 24

    Dim col_HCPT As Integer = 25


    Dim Col_HsRxNormID As Integer = 26
    Dim Col_HnHistoryID As Integer = 27

    Dim Col_LoincCode As Integer = 37
    Dim Col_LoincDescr As Integer = 38

    ''Dim Col_HCOUNT As Integer = 34
    'Dim Col_HCOUNT As Integer = 37

    Dim Col_AllergyClsID As Integer = 39 'added for 2015 certification

    Dim Col_CQMCode As Integer = 40
    Dim Col_CQMDesc As Integer = 41
    Dim Col_DeviceList_ID As Integer = 42
    Dim COl_sProcStatus As Integer = 43

    Dim Col_AllergySeverity As Integer = 44 'added for 2015 certification
    Dim Col_SnoMedCode As Integer = 45 'added for 2015 certification

    Dim Col_ResolvedEndDate As Integer = 46 'added for 2015 certification
    Dim Col_AllergyIntelorenceCode As Integer = 47 'added for 2015 certification

    Dim Col_CVXCode As Integer = 49
    Dim Col_CVXDesc As Integer = 50

    Dim Col_HCOUNT As Integer = 51
    Const MAX_COMMENT_LENGHT As Integer = 760
    'sarika 27th sept 07
    Dim dtSource As DataTable
    Public srCategory As String = ""
    '''' Add by Sagar for Coded history    

    '19-Apr-13 Aniket: Resolving Memory Leak Issues
    Dim ToolTipbtn_CPTCode As System.Windows.Forms.ToolTip
    Dim ToolTipbtn_ICD9Code As System.Windows.Forms.ToolTip
    Dim ToolTipbtnConceptID As System.Windows.Forms.ToolTip
    Dim ToolTipbtnUp As System.Windows.Forms.ToolTip
    Dim ToolTipbtnDown As System.Windows.Forms.ToolTip
    Private oRefusalListControl As gloListControl.gloListControl
    Private ofrmRefusalList As frmViewListControl
    Private strRefusalCode As String = ""
    Private strRefusalDescription As String = ""

    Private oCQMListControl As gloListControl.gloListControl
    Private ofrmCQMList As frmViewListControl
    Private ofrmList As frmViewListControl
    Private strCqmCode As String = ""
    Private strCqmDescription As String = ""
    Private nDeviceList_ID As Long = 0
    Private sProcStatus As String = ""
    Private sIntelorenceCode As String = ""
    Private sDeviceID As String
    Private sBrandName As String
    Dim nReconcillationType As Int16 = 3
    Dim dtPHistoryMedRec As DataTable = Nothing  ' Used For Medical reconcilation

#Region "System Table For History Standard Types Constants"
    Dim Col_Standard_HistoryType As Integer = 0
    Dim Col_Standard_bIsActive As Integer = 1
    Dim Col_Standard_bIsOnsetDate As Integer = 2
    Dim Col_Standard_sCCDSection As Integer = 3

#End Region
#Region "System Table For Catgeory Standard Types Constants"
    Dim Col_Category_sDescription As Integer = 0
    Dim Col_Category_sHistoryType As Integer = 1
#End Region
#Region " Windows Controls "
    Friend WithEvents lblReviewed As System.Windows.Forms.Label
    Friend WithEvents tblHistory As gloToolStrip.gloToolStrip
    Friend WithEvents tblNew As System.Windows.Forms.ToolStripButton
    Friend WithEvents tblSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents tblClose As System.Windows.Forms.ToolStripButton
    Friend WithEvents tblShowNarrative As System.Windows.Forms.ToolStripButton
    Friend WithEvents tblReviewofHistory As System.Windows.Forms.ToolStripButton
    Friend WithEvents tblHistoryofHistory As System.Windows.Forms.ToolStripButton
    Friend WithEvents tblShow As System.Windows.Forms.ToolStripButton
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents pnlreview As System.Windows.Forms.Panel
    Friend WithEvents mnuAddReaction As System.Windows.Forms.MenuItem
    Friend WithEvents mnuEditHistoryItem As System.Windows.Forms.MenuItem
    Friend WithEvents tlbViewReview As System.Windows.Forms.ToolStripButton
    Friend WithEvents pnlSearch As System.Windows.Forms.Panel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Private WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents Label7 As System.Windows.Forms.Label
    Private WithEvents Label8 As System.Windows.Forms.Label
    Private WithEvents Label9 As System.Windows.Forms.Label
    Private WithEvents pnl_Base As System.Windows.Forms.Panel
    Private WithEvents lbl_BottomBrd As System.Windows.Forms.Label
    Private WithEvents lbl_LeftBrd As System.Windows.Forms.Label
    Private WithEvents lbl_RightBrd As System.Windows.Forms.Label
    Private WithEvents lbl_TopBrd As System.Windows.Forms.Label
    Private WithEvents Label27 As System.Windows.Forms.Label
    Private WithEvents Label21 As System.Windows.Forms.Label
    Private WithEvents Label14 As System.Windows.Forms.Label
    Private WithEvents Label15 As System.Windows.Forms.Label
    Private WithEvents Label16 As System.Windows.Forms.Label
    Private WithEvents Label17 As System.Windows.Forms.Label
    Private WithEvents Label26 As System.Windows.Forms.Label
    Private WithEvents Label22 As System.Windows.Forms.Label
    Private WithEvents Label23 As System.Windows.Forms.Label
    Private WithEvents Label24 As System.Windows.Forms.Label
    Private WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents Splitter2 As System.Windows.Forms.Splitter
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Private WithEvents C1SuperTooltip1 As C1.Win.C1SuperTooltip.C1SuperTooltip
    Friend WithEvents GloUC_trvHistory As gloUserControlLibrary.gloUC_TreeView
    Friend WithEvents pnlTopButtons As System.Windows.Forms.Panel
    Friend WithEvents pnlBottomButtons As System.Windows.Forms.Panel
    Private WithEvents lblHeightMeter As System.Windows.Forms.Label
    Friend WithEvents tblCCD As System.Windows.Forms.ToolStripButton
    Friend WithEvents PnlCustomTask As System.Windows.Forms.Panel
    Friend WithEvents cmbAllergyType As System.Windows.Forms.ComboBox
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents pnlSnoMedtrvSource As System.Windows.Forms.Panel
    Friend WithEvents pnlSubtype As System.Windows.Forms.Panel
    Friend WithEvents trvSubType As System.Windows.Forms.TreeView
    Friend WithEvents Splitter4 As System.Windows.Forms.Splitter
    Friend WithEvents pnlSMFindings As System.Windows.Forms.Panel
    Friend WithEvents trvFindings As System.Windows.Forms.TreeView
    Friend WithEvents pnlSMSearch As System.Windows.Forms.Panel
    Friend WithEvents txtSMSearch As gloUserControlLibrary.gloSearchTextBox 'System.Windows.Forms.TextBox
    Friend WithEvents lbl_WhiteSpaceTop As System.Windows.Forms.Label
    Friend WithEvents lbl_WhiteSpaceBottom As System.Windows.Forms.Label
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents PicBx_Search As System.Windows.Forms.PictureBox
    Private WithEvents lbl_pnlSearchBottomBrd As System.Windows.Forms.Label
    Private WithEvents lbl_pnlSearchTopBrd As System.Windows.Forms.Label
    Private WithEvents lbl_pnlSearchLeftBrd As System.Windows.Forms.Label
    Private WithEvents lbl_pnlSearchRightBrd As System.Windows.Forms.Label
    Friend WithEvents pnlsnomadedetail As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents lblSnomedID As System.Windows.Forms.Label
    Friend WithEvents lblNDCid As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Private WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents lbldescid As System.Windows.Forms.Label
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents lblRxNorm As System.Windows.Forms.Label
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents cntFindings As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents pnlDI As System.Windows.Forms.Panel
    Friend WithEvents pnlmonograph As System.Windows.Forms.Panel
    Private WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents pnlDIScreenResult As System.Windows.Forms.Panel
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Private WithEvents Label36 As System.Windows.Forms.Label
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Private WithEvents Label35 As System.Windows.Forms.Label
    Friend WithEvents trvSubTypeDefinition As System.Windows.Forms.TreeView
    Friend WithEvents trvDefinitionSubTypetionSubTypetionSubTypetionSubTypetionSubtionSub As System.Windows.Forms.TreeView
    Friend WithEvents trvDefSubType1 As System.Windows.Forms.TreeView
    Friend WithEvents Splitter6 As System.Windows.Forms.Splitter
    Friend WithEvents Splitter7 As System.Windows.Forms.Splitter
    Friend WithEvents pnlSubTypeDefinition As System.Windows.Forms.Panel
    Friend WithEvents trvDefSubType As System.Windows.Forms.TreeView
    Friend WithEvents trvDefinitionSubType As System.Windows.Forms.TreeView
    Friend WithEvents Splitter8 As System.Windows.Forms.Splitter
    Friend WithEvents mnu_AddFindings As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Label39 As System.Windows.Forms.Label
    Friend WithEvents Label37 As System.Windows.Forms.Label
    Friend WithEvents btnConceptID As System.Windows.Forms.Button
    Friend WithEvents btn_CPTCode As System.Windows.Forms.Button
    Friend WithEvents btn_ICD9Code As System.Windows.Forms.Button
    Friend WithEvents lblconcptid As System.Windows.Forms.Label
    Friend WithEvents linkLblICD9code As System.Windows.Forms.Label
    Friend WithEvents lnlLbllCPTCode As System.Windows.Forms.Label
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Private WithEvents Label49 As System.Windows.Forms.Label
    Private WithEvents Label50 As System.Windows.Forms.Label
    Private WithEvents Label51 As System.Windows.Forms.Label
    Private WithEvents Label52 As System.Windows.Forms.Label
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Private WithEvents Label44 As System.Windows.Forms.Label
    Private WithEvents Label45 As System.Windows.Forms.Label
    Private WithEvents Label46 As System.Windows.Forms.Label
    Private WithEvents Label47 As System.Windows.Forms.Label
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Private WithEvents Label42 As System.Windows.Forms.Label
    Private WithEvents Label41 As System.Windows.Forms.Label
    Private WithEvents Label40 As System.Windows.Forms.Label
    Private WithEvents Label38 As System.Windows.Forms.Label
    Friend WithEvents btnDown As System.Windows.Forms.Button
    Friend WithEvents btnUp As System.Windows.Forms.Button
    Friend WithEvents lblHistoryTypelabel As System.Windows.Forms.Label
    Friend WithEvents lblHistoryType As System.Windows.Forms.Label
    Friend WithEvents Label48 As System.Windows.Forms.Label
    Friend WithEvents Label43 As System.Windows.Forms.Label
    Friend WithEvents tlbbtn_Reconcile As System.Windows.Forms.ToolStripButton
    Private WithEvents Label53 As System.Windows.Forms.Label
    Friend WithEvents Panel8 As System.Windows.Forms.Panel
    Private WithEvents Label13 As System.Windows.Forms.Label
    Private WithEvents Label11 As System.Windows.Forms.Label
    Private WithEvents Label20 As System.Windows.Forms.Label
    Private WithEvents Label57 As System.Windows.Forms.Label
    Private WithEvents Label55 As System.Windows.Forms.Label
    Private WithEvents Label56 As System.Windows.Forms.Label
    Private WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents btnClearSearchHistory As System.Windows.Forms.Button
    Friend WithEvents tblPastPregnancies As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnClearSNOMEDRefusedCode As System.Windows.Forms.Button
    Friend WithEvents Label59SNOMEDRefusedCode As System.Windows.Forms.Button
    Friend WithEvents Label54 As System.Windows.Forms.Label
    Friend WithEvents btnbrloinc As System.Windows.Forms.Button
    Friend WithEvents Label58 As System.Windows.Forms.Label
    Friend WithEvents btnclloinc As System.Windows.Forms.Button
    Friend WithEvents Panel9 As System.Windows.Forms.Panel
    Friend WithEvents txtSNOMEDRefusedCode As System.Windows.Forms.Label
    Private WithEvents Label60 As System.Windows.Forms.Label
    Private WithEvents Label61 As System.Windows.Forms.Label
    Private WithEvents Label62 As System.Windows.Forms.Label
    Private WithEvents Label63 As System.Windows.Forms.Label
    Friend WithEvents Panel10 As System.Windows.Forms.Panel
    Private WithEvents Label65 As System.Windows.Forms.Label
    Private WithEvents Label66 As System.Windows.Forms.Label
    Private WithEvents Label67 As System.Windows.Forms.Label
    Private WithEvents Label68 As System.Windows.Forms.Label
    Friend WithEvents Panel11 As System.Windows.Forms.Panel
    Friend WithEvents txtCqm As System.Windows.Forms.Label
    Private WithEvents Label64 As System.Windows.Forms.Label
    Private WithEvents Label69 As System.Windows.Forms.Label
    Private WithEvents Label70 As System.Windows.Forms.Label
    Private WithEvents Label71 As System.Windows.Forms.Label
    Friend WithEvents btnClearCqm As System.Windows.Forms.Button
    Friend WithEvents btnBrowseCqm As System.Windows.Forms.Button
    Friend WithEvents Label72 As System.Windows.Forms.Label
    Friend WithEvents Label83 As System.Windows.Forms.Label
    Friend WithEvents btnBrowseUDI As System.Windows.Forms.Button
    Friend WithEvents Panel12 As System.Windows.Forms.Panel
    Friend WithEvents lblUDI As System.Windows.Forms.Label
    Private WithEvents Label73 As System.Windows.Forms.Label
    Private WithEvents Label74 As System.Windows.Forms.Label
    Private WithEvents Label75 As System.Windows.Forms.Label
    Private WithEvents Label76 As System.Windows.Forms.Label
    Friend WithEvents Label77 As System.Windows.Forms.Label
    Friend WithEvents cmbStatus As System.Windows.Forms.ComboBox
    Friend WithEvents cmbAllergySeverity As System.Windows.Forms.ComboBox
    Friend WithEvents Label59 As System.Windows.Forms.Label
    Friend WithEvents tblHistoryReconcillation As System.Windows.Forms.ToolStripButton
    Friend WithEvents ChkResolvedEnddate As System.Windows.Forms.CheckBox
    Friend WithEvents Label78 As System.Windows.Forms.Label
    Friend WithEvents ResolvedEndDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtloinc As System.Windows.Forms.Label
    Friend WithEvents lblAllergyIntelorenceType As System.Windows.Forms.Label
    Friend WithEvents cmbAllergyIntelorenceType As System.Windows.Forms.ComboBox
    Friend WithEvents tblNKAllergies As System.Windows.Forms.ToolStripButton
    Friend WithEvents pnlShowCQM As System.Windows.Forms.Panel
    Friend WithEvents Panel14 As System.Windows.Forms.Panel
    Friend WithEvents btnShowCQM As System.Windows.Forms.Button
    Friend WithEvents tblRecHistory As System.Windows.Forms.ToolStripButton
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents btnClsCVXCode As System.Windows.Forms.Button
    Friend WithEvents btnCVXCode As System.Windows.Forms.Button
    Friend WithEvents pnltxtCVXCode As System.Windows.Forms.Panel
    Private WithEvents Label80 As System.Windows.Forms.Label
    Private WithEvents Label81 As System.Windows.Forms.Label
    Private WithEvents Label82 As System.Windows.Forms.Label
    Private WithEvents Label84 As System.Windows.Forms.Label
    Friend WithEvents txtCVXCode As System.Windows.Forms.Label
    Friend WithEvents Splitter3 As System.Windows.Forms.Splitter
    ''added to show snomed description 8020 changes
#End Region

#Region " Windows Form Designer generated code "
    Public Sub New(ByVal VisitID As Long, ByVal VisitDate As Date, ByVal PatientID As Long, Optional ByVal blnRecordLock As Boolean = False, Optional ByVal RecordLock As Boolean = False, Optional ByVal IsOBHistory As Boolean = False)
        MyBase.New()
        m_VisitID = VisitID
        m_VisitDate = VisitDate.Date
        m_strLoginName = gstrLoginName
        _blnRecordLock = blnRecordLock
        _RecordLock = RecordLock
        m_PatientID = PatientID
        _IsOBHistory = IsOBHistory
        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        SetStyle(ControlStyles.UserPaint, True)
        SetStyle(ControlStyles.AllPaintingInWmPaint, True)
        SetStyle(ControlStyles.DoubleBuffer, True)
        Me.WindowState = FormWindowState.Maximized
        InitializeToolStrip()

        Call Set_PatientDetailStrip()

    End Sub

    Public Sub New(ByVal VisitID As Long, ByVal VisitDate As Date, ByVal strAllergy As String, ByVal strConceptID As String, ByVal strSnoMedID As String, ByVal strDescriptionID As String, ByVal strICD9 As String, ByVal strSnomedDefination As String, ByVal strRxNormID As String, ByVal strNDCCode As String, ByVal IsFrmImm As Boolean, ByVal PatientID As Long, Optional ByVal blnRecordLock As Boolean = False, Optional ByVal RecordLock As Boolean = False)
        MyBase.New()
        m_VisitID = VisitID
        m_VisitDate = VisitDate.Date
        m_strLoginName = gstrLoginName
        '   m_OnsetDate = OnsetDate
        _blnRecordLock = blnRecordLock
        _RecordLock = RecordLock
        m_PatientID = PatientID

        ''''''''''Added by Ujwala - for Snomed Implementation from Immunization in History - as on 20101008
        _IsFrmImm = IsFrmImm
        _strAllergy = strAllergy
        _strConceptID = strConceptID
        _strSnoMedID = strSnoMedID
        _strDescriptionID = strDescriptionID
        _strICD9 = strICD9
        _strSnomedDefination = strSnomedDefination
        _RxNormID = strRxNormID
        _NDCCode = strNDCCode
        ''''''''''Added by Ujwala - for Snomed Implementation from Immunization in History - as on 20101008

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        SetStyle(ControlStyles.UserPaint, True)
        SetStyle(ControlStyles.AllPaintingInWmPaint, True)
        SetStyle(ControlStyles.DoubleBuffer, True)
        Me.WindowState = FormWindowState.Maximized
        InitializeToolStrip()
        Call Set_PatientDetailStrip()

    End Sub

#Region " TO Check the Multiple instances Of Form "

    '' TO Keep track that the Form's Instance is Disposed or not
    Private blnDisposed As Boolean
    Private Shared frm As frmHistory

    ''Form overrides dispose to clean up the component list.
    Protected Overridable Overloads Sub Dispose(ByVal disposing As Boolean)
        ' Check to see if Dispose has already been called.
        If Not (Me.blnDisposed) Then
            ' If disposing equals true, dispose all managed
            ' and unmanaged resources.
            If (disposing) Then
                If Not (components Is Nothing) Then
                    components.Dispose()
                    Try
                        If (IsNothing(gloUC_PatientStrip1) = False) Then
                            gloUC_PatientStrip1.Dispose()
                            gloUC_PatientStrip1 = Nothing
                        End If
                    Catch ex As Exception

                    End Try
                End If
                Try
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
                Catch
                End Try
                Dim dtpContextMenu As ContextMenu() = {ContextMenuC1History, ContextMenutrvPrevHistory, cntCategory}
                Try
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(dtpContextMenu)

                Catch ex As Exception

                End Try
                Dim dtpContextMenustrip As ContextMenuStrip() = {cntFindings}
                Try
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(dtpContextMenustrip)

                Catch ex As Exception

                End Try
                Try
                    gloGlobal.cEventHelper.DisposeContextMenu(dtpContextMenu)
                    gloGlobal.cEventHelper.DisposeContextMenuStrip(dtpContextMenustrip)
                Catch ex As Exception

                End Try
                ' Dispose managed resources.

            End If

            DisposeDeclaredObject()

            ' Release unmanaged resources. If disposing is false,
            ' only the following code is executed.

            ' Note that this is not thread safe.
            ' Another thread could start disposing the object
            ' after the managed resources are disposed,
            ' but before the disposed flag is set to true.
            ' If thread safety is necessary, it must be
            ' implemented by the client.
        End If
        ''Commeneted By Mayuri:20120725-If we open multiple instances of form and if user closes one instance it is closing all instances
        ''Do not dispose or close "frm" object 
        'Change made to solve memory Leak and word crash issue
        If Not frm Is Nothing Then
            '   frm.Close()
            frm = Nothing
        End If
        Me.blnDisposed = True

    End Sub

    Public Overloads Sub Dispose()
        Dispose(True)
        'If Not IsNothing(ofrmPrescMed) Then
        '    ofrmPrescMed.Dispose()
        '    ofrmPrescMed = Nothing
        'End If
        ' Take yourself off of the finalization queue
        ' to prevent finalization code for this object
        ' from executing a second time.
        GC.SuppressFinalize(Me)
    End Sub

    Protected Overrides Sub Finalize()
        Dispose(False)
    End Sub

    Private Sub DisposeDeclaredObject()

        'If IsNothing(ofrmDiagnosisList) = False Then
        '    ofrmDiagnosisList = Nothing
        'End If

        'If IsNothing(ofrmCPTList) = False Then
        '    ofrmCPTList = Nothing
        'End If

        'If IsNothing(ofrmPrescMed) = False Then
        '    ofrmPrescMed.Dispose() : ofrmPrescMed = Nothing
        'End If

        'If IsNothing(myLetter) = False Then
        '    myLetter.Dispose() : myLetter = Nothing
        'End If

        'If IsNothing(myNurse) = False Then
        '    myNurse.Dispose() : myNurse = Nothing
        'End If

        'If IsNothing(myCaller) = False Then
        '    myCaller.Dispose() : myCaller = Nothing
        'End If

        'If IsNothing(mycallerImm) = False Then
        '    mycallerImm.Dispose() : mycallerImm = Nothing
        'End If

        'If IsNothing(myCallerSynopsis) = False Then
        '    myCallerSynopsis.Dispose() : myCallerSynopsis = Nothing
        'End If

        If IsNothing(objWord) = False Then
            objWord = Nothing
        End If

        If IsNothing(objclsPatientHistory) = False Then
            objclsPatientHistory.Dispose()
            objclsPatientHistory = Nothing
        End If

        If IsNothing(oSnoMed) = False Then
            oSnoMed.Dispose() : oSnoMed = Nothing
        End If

        If IsNothing(oclsLogin) = False Then
            oclsLogin.Dispose()
            oclsLogin = Nothing
        End If


        sLoginName = Nothing
        BtnText = Nothing
        m_strLoginName = Nothing
        _Defination1 = Nothing
        _ICD91 = Nothing
        _strAllergy = Nothing
        _strSnoMedID = Nothing
        _strConceptID = Nothing
        _strDescriptionID = Nothing
        _strICD9 = Nothing
        _strSnomedDefination = Nothing
        _RxNormID = Nothing
        _NDCCode = Nothing
        strPatientCode = Nothing
        strPatientFirstName = Nothing
        strPatientLastName = Nothing
        strPatientDOB = Nothing
        strPatientAge = Nothing
        strPatientGender = Nothing
        strPatientMaritalStatus = Nothing
        strLoginNameDetails = Nothing
        strbuttonStatus = Nothing
        _sReaction = Nothing
        RXnormAlergicDrugNDCCode = Nothing


        blnIsLoad = Nothing
        blncancel = Nothing
        gblnIsFindingAdd = Nothing
        IsOpen = Nothing
        blnModify = Nothing
        blnChangesMade = Nothing
        _blnRecordLock = Nothing  '' To the set record lock
        _RecordLock = Nothing
        _IsFrmImm = Nothing
        'blnOpenFromExam = Nothing
        'blnShowMessageBox = Nothing
        'blnShowAddModeMessageBox = Nothing
        _isMakeAsCurrent = Nothing
        _isMedicationHistoryModify = Nothing
        _isMessage = Nothing
        _isDoubleclickPrevHistory = Nothing
        IsCloseClickFlagForCommentValidation = Nothing
        bln_Loadcategory = Nothing
        isHistoryModified = Nothing
        isHistoryLoading = Nothing
        _isPanelClick = Nothing
        blnDIbtnClick = Nothing

        'Code Commented To resolve Bug no 49551::Application is throwing excpetion while user tries to open Patient History from Synospsis.::20130423
        'If IsNothing(dsHistory) = False Then
        '    dsHistory.Dispose() : dsHistory = Nothing
        'End If

        If IsNothing(_dsTemp) = False Then
            _dsTemp.Dispose() : _dsTemp = Nothing
        End If

        If IsNothing(dsCurrent) = False Then
            dsCurrent.Dispose() : dsCurrent = Nothing
        End If


        If IsNothing(dt) = False Then
            dt.Dispose() : dt = Nothing
        End If

        If IsNothing(dtReview) = False Then
            dtReview.Dispose() : dtReview = Nothing
        End If

        'Code Commented To resolve Bug no 49551::Application is throwing excpetion while user tries to open Patient History from Synospsis.::20130423
        'If IsNothing(mydtButton) = False Then
        '    mydtButton.Dispose() : mydtButton = Nothing
        'End If


        If IsNothing(oDiagnosisListControl) = False Then
            oDiagnosisListControl.Dispose() : oDiagnosisListControl = Nothing
        End If

        If IsNothing(oCPTListControl) = False Then
            oCPTListControl.Dispose() : oCPTListControl = Nothing
        End If

        If IsNothing(voicecol) = False Then
            voicecol = Nothing
        End If

        If IsNothing(HistoryVoiceCol) = False Then
            HistoryVoiceCol = Nothing
        End If


        BtnTag = Nothing
        m_VisitID = Nothing
        'm_PatientID = Nothing
        _HxVisitID = Nothing
        FormLevelLockID = Nothing

        intCheck = Nothing
        btntype = Nothing
        m_VisitDate = Nothing

        'If IsNothing(objBtn) = False Then
        '    objBtn.Dispose() : objBtn = Nothing
        'End If


        arrDataDictionary.Clear()
        arrDataDictionary = Nothing

        If IsNothing(_RxBusinessLayer) = False Then
            _RxBusinessLayer.Dispose() : _RxBusinessLayer = Nothing
        End If

        If IsNothing(_MedBusinessLayer) = False Then
            _MedBusinessLayer.Dispose() : _MedBusinessLayer = Nothing
        End If

        strUncodedDrugs = Nothing


        If IsNothing(dgCustomGrid) = False Then
            dgCustomGrid.Dispose() : dgCustomGrid = Nothing
        End If
        If (allocatedInGlobalForuiPanelSplitScreen) Then
            If IsNothing(uiPanSplitScreen_History) = False Then
                uiPanSplitScreen_History.Dispose() : uiPanSplitScreen_History = Nothing
            End If
        End If

        If (IsNothing(objCriteria) = False) Then
            objCriteria.Dispose()
            objCriteria = Nothing
        End If


        '  objCriteria = Nothing

        If IsNothing(clsSplit_History) = False Then
            clsSplit_History.Dispose()
            clsSplit_History = Nothing
        End If

        'If IsNothing(uiPanSplitScreen_History) = False Then
        '    uiPanSplitScreen_History.Dispose()
        '    uiPanSplitScreen_History = Nothing
        'End If

    End Sub

    Public Shared Function GetInstance(ByVal VisitID As Long, ByVal VisitDate As Date, ByVal PatientID As Long, Optional ByVal blnRecordLock As Boolean = False, Optional ByVal _RecordLock As Boolean = False) As frmHistory
        '_mu.WaitOne()
        Try
            IsOpen = False
            ''If frm Is Nothing Then

            For Each f As Form In Application.OpenForms
                If f.Name = "frmHistory" Then
                    If CType(f, frmHistory).m_PatientID = PatientID And CType(f, frmHistory).m_VisitID = VisitID Then
                        IsOpen = True
                        frm = f
                    End If

                End If
            Next
            If (IsOpen = False) Then
                frm = New frmHistory(VisitID, VisitDate, PatientID, blnRecordLock, _RecordLock)
            End If

        Finally

        End Try
        Return frm
    End Function

#End Region

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents pnlOuter As System.Windows.Forms.Panel
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
    Friend WithEvents ContextMenuC1History As System.Windows.Forms.ContextMenu
    Friend WithEvents mnuRemove As System.Windows.Forms.MenuItem
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents pnlCatBtn As System.Windows.Forms.Panel
    Friend WithEvents pnlPrevHistory As System.Windows.Forms.Panel
    Friend WithEvents PnlRight As System.Windows.Forms.Panel
    Friend WithEvents pnlToolStrip As System.Windows.Forms.Panel
    Friend WithEvents pnlWordComp As System.Windows.Forms.Panel
    Friend WithEvents pnlPrevSearch As System.Windows.Forms.Panel
    Friend WithEvents trvPrevHistory As System.Windows.Forms.TreeView
    Friend WithEvents pnltrvSource As System.Windows.Forms.Panel
    Friend WithEvents pnlPatientHeader As System.Windows.Forms.Panel
    Friend WithEvents lblVisitDate As System.Windows.Forms.Label
    Friend WithEvents btnPrevHistory As System.Windows.Forms.Button
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents lblVisitID As System.Windows.Forms.Label
    Friend WithEvents lblPatientName As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblPatientCode As System.Windows.Forms.Label
    Friend WithEvents lblPatient As System.Windows.Forms.Label
    Friend WithEvents pnltrvTarget As System.Windows.Forms.Panel
    Friend WithEvents txtsearchhistory As System.Windows.Forms.TextBox
    Friend WithEvents cmbsearchHistory As System.Windows.Forms.ComboBox
    Friend WithEvents ContextMenutrvPrevHistory As System.Windows.Forms.ContextMenu
    Friend WithEvents mnuDeleteHistory As System.Windows.Forms.MenuItem
    Friend WithEvents mnuMakeCurrent As System.Windows.Forms.MenuItem
    Friend WithEvents wdNarration As System.Windows.Forms.RichTextBox
    Friend WithEvents cntCategory As System.Windows.Forms.ContextMenu
    Friend WithEvents mnuAddHistoryItem As System.Windows.Forms.MenuItem
    Friend WithEvents imgTreeVIew As System.Windows.Forms.ImageList
    Friend WithEvents ImgPatientHistory1 As System.Windows.Forms.ImageList
    Friend WithEvents C1HistoryDetails As C1.Win.C1FlexGrid.C1FlexGrid

    Private Sub InitializeToolStrip()
        tblHistory.ConnectionString = GetConnectionString()
        tblHistory.ModuleName = Me.Name
        tblHistory.UserID = gnLoginID
        tblHistory.ButtonsToHide.Add(tblNew.Name)
        'tblHistory.ButtonsToHide.Add(tsbtn_Export.Name)
        'tblHistory.ButtonsToHide.Add(tsbtn_Delete.Name)
    End Sub
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmHistory))
        Me.pnlOuter = New System.Windows.Forms.Panel()
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.PnlRight = New System.Windows.Forms.Panel()
        Me.pnltrvTarget = New System.Windows.Forms.Panel()
        Me.Panel8 = New System.Windows.Forms.Panel()
        Me.C1HistoryDetails = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.pnlmonograph = New System.Windows.Forms.Panel()
        Me.trvDefinitionSubType = New System.Windows.Forms.TreeView()
        Me.imgTreeVIew = New System.Windows.Forms.ImageList(Me.components)
        Me.pnlSubTypeDefinition = New System.Windows.Forms.Panel()
        Me.trvDefSubType1 = New System.Windows.Forms.TreeView()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.lblSnomedID = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label53 = New System.Windows.Forms.Label()
        Me.pnlDIScreenResult = New System.Windows.Forms.Panel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.pnlsnomadedetail = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.pnlShowCQM = New System.Windows.Forms.Panel()
        Me.Panel11 = New System.Windows.Forms.Panel()
        Me.txtCqm = New System.Windows.Forms.Label()
        Me.Label64 = New System.Windows.Forms.Label()
        Me.Label69 = New System.Windows.Forms.Label()
        Me.Label70 = New System.Windows.Forms.Label()
        Me.Label71 = New System.Windows.Forms.Label()
        Me.Label72 = New System.Windows.Forms.Label()
        Me.lblAllergyIntelorenceType = New System.Windows.Forms.Label()
        Me.btnBrowseCqm = New System.Windows.Forms.Button()
        Me.cmbAllergyIntelorenceType = New System.Windows.Forms.ComboBox()
        Me.btnClearCqm = New System.Windows.Forms.Button()
        Me.Label78 = New System.Windows.Forms.Label()
        Me.Label77 = New System.Windows.Forms.Label()
        Me.ChkResolvedEnddate = New System.Windows.Forms.CheckBox()
        Me.Panel12 = New System.Windows.Forms.Panel()
        Me.lblUDI = New System.Windows.Forms.Label()
        Me.Label73 = New System.Windows.Forms.Label()
        Me.Label74 = New System.Windows.Forms.Label()
        Me.Label75 = New System.Windows.Forms.Label()
        Me.Label76 = New System.Windows.Forms.Label()
        Me.ResolvedEndDate = New System.Windows.Forms.DateTimePicker()
        Me.btnBrowseUDI = New System.Windows.Forms.Button()
        Me.cmbAllergySeverity = New System.Windows.Forms.ComboBox()
        Me.Label83 = New System.Windows.Forms.Label()
        Me.Label59 = New System.Windows.Forms.Label()
        Me.cmbStatus = New System.Windows.Forms.ComboBox()
        Me.btnClsCVXCode = New System.Windows.Forms.Button()
        Me.btnCVXCode = New System.Windows.Forms.Button()
        Me.Panel10 = New System.Windows.Forms.Panel()
        Me.txtloinc = New System.Windows.Forms.Label()
        Me.Label65 = New System.Windows.Forms.Label()
        Me.Label66 = New System.Windows.Forms.Label()
        Me.Label67 = New System.Windows.Forms.Label()
        Me.Label68 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Panel9 = New System.Windows.Forms.Panel()
        Me.txtSNOMEDRefusedCode = New System.Windows.Forms.Label()
        Me.Label60 = New System.Windows.Forms.Label()
        Me.Label61 = New System.Windows.Forms.Label()
        Me.Label62 = New System.Windows.Forms.Label()
        Me.Label63 = New System.Windows.Forms.Label()
        Me.btnclloinc = New System.Windows.Forms.Button()
        Me.btnbrloinc = New System.Windows.Forms.Button()
        Me.Label58 = New System.Windows.Forms.Label()
        Me.btnClearSNOMEDRefusedCode = New System.Windows.Forms.Button()
        Me.Label59SNOMEDRefusedCode = New System.Windows.Forms.Button()
        Me.Label54 = New System.Windows.Forms.Label()
        Me.btn_ICD9Code = New System.Windows.Forms.Button()
        Me.btnConceptID = New System.Windows.Forms.Button()
        Me.btn_CPTCode = New System.Windows.Forms.Button()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label57 = New System.Windows.Forms.Label()
        Me.lblHistoryTypelabel = New System.Windows.Forms.Label()
        Me.lblHistoryType = New System.Windows.Forms.Label()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.lnlLbllCPTCode = New System.Windows.Forms.Label()
        Me.Label49 = New System.Windows.Forms.Label()
        Me.Label50 = New System.Windows.Forms.Label()
        Me.Label51 = New System.Windows.Forms.Label()
        Me.Label52 = New System.Windows.Forms.Label()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.linkLblICD9code = New System.Windows.Forms.Label()
        Me.Label44 = New System.Windows.Forms.Label()
        Me.Label45 = New System.Windows.Forms.Label()
        Me.Label46 = New System.Windows.Forms.Label()
        Me.Label47 = New System.Windows.Forms.Label()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.lblconcptid = New System.Windows.Forms.Label()
        Me.Label42 = New System.Windows.Forms.Label()
        Me.Label41 = New System.Windows.Forms.Label()
        Me.Label40 = New System.Windows.Forms.Label()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.Label39 = New System.Windows.Forms.Label()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.lblNDCid = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.lbldescid = New System.Windows.Forms.Label()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.lblRxNorm = New System.Windows.Forms.Label()
        Me.Panel14 = New System.Windows.Forms.Panel()
        Me.btnShowCQM = New System.Windows.Forms.Button()
        Me.PnlCustomTask = New System.Windows.Forms.Panel()
        Me.pnlreview = New System.Windows.Forms.Panel()
        Me.lblReviewed = New System.Windows.Forms.Label()
        Me.Label43 = New System.Windows.Forms.Label()
        Me.btnUp = New System.Windows.Forms.Button()
        Me.Label48 = New System.Windows.Forms.Label()
        Me.btnDown = New System.Windows.Forms.Button()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.cmbAllergyType = New System.Windows.Forms.ComboBox()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.Label55 = New System.Windows.Forms.Label()
        Me.Label56 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.pnlPatientHeader = New System.Windows.Forms.Panel()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.lblVisitDate = New System.Windows.Forms.Label()
        Me.btnPrevHistory = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lblVisitID = New System.Windows.Forms.Label()
        Me.lblPatientName = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblPatientCode = New System.Windows.Forms.Label()
        Me.lblPatient = New System.Windows.Forms.Label()
        Me.Splitter3 = New System.Windows.Forms.Splitter()
        Me.pnlWordComp = New System.Windows.Forms.Panel()
        Me.wdNarration = New System.Windows.Forms.RichTextBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.Splitter2 = New System.Windows.Forms.Splitter()
        Me.pnlPrevHistory = New System.Windows.Forms.Panel()
        Me.pnl_Base = New System.Windows.Forms.Panel()
        Me.trvPrevHistory = New System.Windows.Forms.TreeView()
        Me.ImgPatientHistory1 = New System.Windows.Forms.ImageList(Me.components)
        Me.Label34 = New System.Windows.Forms.Label()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.lbl_BottomBrd = New System.Windows.Forms.Label()
        Me.lbl_LeftBrd = New System.Windows.Forms.Label()
        Me.lbl_RightBrd = New System.Windows.Forms.Label()
        Me.lbl_TopBrd = New System.Windows.Forms.Label()
        Me.pnlPrevSearch = New System.Windows.Forms.Panel()
        Me.cmbsearchHistory = New System.Windows.Forms.ComboBox()
        Me.pnlSearch = New System.Windows.Forms.Panel()
        Me.txtsearchhistory = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnClearSearchHistory = New System.Windows.Forms.Button()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Splitter1 = New System.Windows.Forms.Splitter()
        Me.pnlCatBtn = New System.Windows.Forms.Panel()
        Me.pnltrvSource = New System.Windows.Forms.Panel()
        Me.Splitter7 = New System.Windows.Forms.Splitter()
        Me.Splitter8 = New System.Windows.Forms.Splitter()
        Me.GloUC_trvHistory = New gloUserControlLibrary.gloUC_TreeView()
        Me.pnlSnoMedtrvSource = New System.Windows.Forms.Panel()
        Me.pnlSMSearch = New System.Windows.Forms.Panel()
        Me.txtSMSearch = New gloUserControlLibrary.gloSearchTextBox()
        Me.lbl_WhiteSpaceTop = New System.Windows.Forms.Label()
        Me.lbl_WhiteSpaceBottom = New System.Windows.Forms.Label()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.PicBx_Search = New System.Windows.Forms.PictureBox()
        Me.lbl_pnlSearchBottomBrd = New System.Windows.Forms.Label()
        Me.lbl_pnlSearchTopBrd = New System.Windows.Forms.Label()
        Me.lbl_pnlSearchLeftBrd = New System.Windows.Forms.Label()
        Me.lbl_pnlSearchRightBrd = New System.Windows.Forms.Label()
        Me.pnlSubtype = New System.Windows.Forms.Panel()
        Me.trvSubType = New System.Windows.Forms.TreeView()
        Me.Splitter4 = New System.Windows.Forms.Splitter()
        Me.pnlSMFindings = New System.Windows.Forms.Panel()
        Me.trvFindings = New System.Windows.Forms.TreeView()
        Me.pnlBottomButtons = New System.Windows.Forms.Panel()
        Me.lblHeightMeter = New System.Windows.Forms.Label()
        Me.pnlTopButtons = New System.Windows.Forms.Panel()
        Me.pnlToolStrip = New System.Windows.Forms.Panel()
        Me.pnlDI = New System.Windows.Forms.Panel()
        Me.tblHistory = New gloToolStrip.gloToolStrip()
        Me.tblNew = New System.Windows.Forms.ToolStripButton()
        Me.tblShowNarrative = New System.Windows.Forms.ToolStripButton()
        Me.tblReviewofHistory = New System.Windows.Forms.ToolStripButton()
        Me.tlbViewReview = New System.Windows.Forms.ToolStripButton()
        Me.tblHistoryofHistory = New System.Windows.Forms.ToolStripButton()
        Me.tblShow = New System.Windows.Forms.ToolStripButton()
        Me.tblCCD = New System.Windows.Forms.ToolStripButton()
        Me.tlbbtn_Reconcile = New System.Windows.Forms.ToolStripButton()
        Me.tblPastPregnancies = New System.Windows.Forms.ToolStripButton()
        Me.tblHistoryReconcillation = New System.Windows.Forms.ToolStripButton()
        Me.tblRecHistory = New System.Windows.Forms.ToolStripButton()
        Me.tblNKAllergies = New System.Windows.Forms.ToolStripButton()
        Me.tblSave = New System.Windows.Forms.ToolStripButton()
        Me.tblClose = New System.Windows.Forms.ToolStripButton()
        Me.ContextMenuC1History = New System.Windows.Forms.ContextMenu()
        Me.mnuRemove = New System.Windows.Forms.MenuItem()
        Me.mnuAddReaction = New System.Windows.Forms.MenuItem()
        Me.ContextMenutrvPrevHistory = New System.Windows.Forms.ContextMenu()
        Me.mnuDeleteHistory = New System.Windows.Forms.MenuItem()
        Me.mnuMakeCurrent = New System.Windows.Forms.MenuItem()
        Me.cntCategory = New System.Windows.Forms.ContextMenu()
        Me.mnuAddHistoryItem = New System.Windows.Forms.MenuItem()
        Me.mnuEditHistoryItem = New System.Windows.Forms.MenuItem()
        Me.C1SuperTooltip1 = New C1.Win.C1SuperTooltip.C1SuperTooltip(Me.components)
        Me.cntFindings = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mnu_AddFindings = New System.Windows.Forms.ToolStripMenuItem()
        Me.pnltxtCVXCode = New System.Windows.Forms.Panel()
        Me.Label80 = New System.Windows.Forms.Label()
        Me.Label81 = New System.Windows.Forms.Label()
        Me.Label82 = New System.Windows.Forms.Label()
        Me.Label84 = New System.Windows.Forms.Label()
        Me.txtCVXCode = New System.Windows.Forms.Label()
        Me.pnlOuter.SuspendLayout()
        Me.pnlMain.SuspendLayout()
        Me.PnlRight.SuspendLayout()
        Me.pnltrvTarget.SuspendLayout()
        Me.Panel8.SuspendLayout()
        CType(Me.C1HistoryDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlmonograph.SuspendLayout()
        Me.pnlDIScreenResult.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.pnlsnomadedetail.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.pnlShowCQM.SuspendLayout()
        Me.Panel11.SuspendLayout()
        Me.Panel12.SuspendLayout()
        Me.Panel10.SuspendLayout()
        Me.Panel9.SuspendLayout()
        Me.Panel7.SuspendLayout()
        Me.Panel6.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.Panel14.SuspendLayout()
        Me.pnlreview.SuspendLayout()
        Me.pnlPatientHeader.SuspendLayout()
        Me.pnlWordComp.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.pnlPrevHistory.SuspendLayout()
        Me.pnl_Base.SuspendLayout()
        Me.pnlPrevSearch.SuspendLayout()
        Me.pnlSearch.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlCatBtn.SuspendLayout()
        Me.pnltrvSource.SuspendLayout()
        Me.pnlSnoMedtrvSource.SuspendLayout()
        Me.pnlSMSearch.SuspendLayout()
        CType(Me.PicBx_Search, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlSubtype.SuspendLayout()
        Me.pnlSMFindings.SuspendLayout()
        Me.pnlBottomButtons.SuspendLayout()
        Me.pnlToolStrip.SuspendLayout()
        Me.tblHistory.SuspendLayout()
        Me.cntFindings.SuspendLayout()
        Me.pnltxtCVXCode.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlOuter
        '
        Me.pnlOuter.Controls.Add(Me.pnlMain)
        Me.pnlOuter.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlOuter.Location = New System.Drawing.Point(0, 0)
        Me.pnlOuter.Name = "pnlOuter"
        Me.pnlOuter.Size = New System.Drawing.Size(1184, 836)
        Me.pnlOuter.TabIndex = 0
        '
        'pnlMain
        '
        Me.pnlMain.Controls.Add(Me.PnlRight)
        Me.pnlMain.Controls.Add(Me.Splitter2)
        Me.pnlMain.Controls.Add(Me.pnlPrevHistory)
        Me.pnlMain.Controls.Add(Me.Splitter1)
        Me.pnlMain.Controls.Add(Me.pnlCatBtn)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(0, 0)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(1184, 836)
        Me.pnlMain.TabIndex = 1
        '
        'PnlRight
        '
        Me.PnlRight.BackColor = System.Drawing.Color.Transparent
        Me.PnlRight.Controls.Add(Me.pnltrvTarget)
        Me.PnlRight.Controls.Add(Me.pnlPatientHeader)
        Me.PnlRight.Controls.Add(Me.Splitter3)
        Me.PnlRight.Controls.Add(Me.pnlWordComp)
        Me.PnlRight.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PnlRight.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PnlRight.Location = New System.Drawing.Point(244, 0)
        Me.PnlRight.Name = "PnlRight"
        Me.PnlRight.Size = New System.Drawing.Size(706, 836)
        Me.PnlRight.TabIndex = 1
        '
        'pnltrvTarget
        '
        Me.pnltrvTarget.Controls.Add(Me.Panel8)
        Me.pnltrvTarget.Controls.Add(Me.pnlmonograph)
        Me.pnltrvTarget.Controls.Add(Me.Label20)
        Me.pnltrvTarget.Controls.Add(Me.Label53)
        Me.pnltrvTarget.Controls.Add(Me.pnlDIScreenResult)
        Me.pnltrvTarget.Controls.Add(Me.pnlsnomadedetail)
        Me.pnltrvTarget.Controls.Add(Me.PnlCustomTask)
        Me.pnltrvTarget.Controls.Add(Me.pnlreview)
        Me.pnltrvTarget.Controls.Add(Me.Label21)
        Me.pnltrvTarget.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnltrvTarget.Location = New System.Drawing.Point(0, 53)
        Me.pnltrvTarget.Name = "pnltrvTarget"
        Me.pnltrvTarget.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.pnltrvTarget.Size = New System.Drawing.Size(706, 596)
        Me.pnltrvTarget.TabIndex = 0
        '
        'Panel8
        '
        Me.Panel8.Controls.Add(Me.C1HistoryDetails)
        Me.Panel8.Controls.Add(Me.Label13)
        Me.Panel8.Controls.Add(Me.Label11)
        Me.Panel8.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel8.Location = New System.Drawing.Point(1, 33)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(704, 258)
        Me.Panel8.TabIndex = 60
        '
        'C1HistoryDetails
        '
        Me.C1HistoryDetails.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.C1HistoryDetails.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None
        Me.C1HistoryDetails.AutoGenerateColumns = False
        Me.C1HistoryDetails.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.C1HistoryDetails.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.C1HistoryDetails.ColumnInfo = resources.GetString("C1HistoryDetails.ColumnInfo")
        Me.C1HistoryDetails.DataMember = "History"
        Me.C1HistoryDetails.Dock = System.Windows.Forms.DockStyle.Fill
        Me.C1HistoryDetails.ExtendLastCol = True
        Me.C1HistoryDetails.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.C1HistoryDetails.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.C1HistoryDetails.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross
        Me.C1HistoryDetails.Location = New System.Drawing.Point(0, 1)
        Me.C1HistoryDetails.Name = "C1HistoryDetails"
        Me.C1HistoryDetails.Rows.Count = 13
        Me.C1HistoryDetails.Rows.DefaultSize = 19
        Me.C1HistoryDetails.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.C1HistoryDetails.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        Me.C1HistoryDetails.ShowCellLabels = True
        Me.C1HistoryDetails.ShowSortPosition = C1.Win.C1FlexGrid.ShowSortPositionEnum.None
        Me.C1HistoryDetails.Size = New System.Drawing.Size(704, 256)
        Me.C1HistoryDetails.StyleInfo = resources.GetString("C1HistoryDetails.StyleInfo")
        Me.C1HistoryDetails.TabIndex = 0
        Me.C1SuperTooltip1.SetToolTip(Me.C1HistoryDetails, "None")
        Me.C1HistoryDetails.Tree.NodeImageCollapsed = CType(resources.GetObject("C1HistoryDetails.Tree.NodeImageCollapsed"), System.Drawing.Image)
        Me.C1HistoryDetails.Tree.NodeImageExpanded = CType(resources.GetObject("C1HistoryDetails.Tree.NodeImageExpanded"), System.Drawing.Image)
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label13.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label13.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label13.Location = New System.Drawing.Point(0, 0)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(704, 1)
        Me.Label13.TabIndex = 13
        Me.Label13.Text = "label2"
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label11.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label11.Location = New System.Drawing.Point(0, 257)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(704, 1)
        Me.Label11.TabIndex = 12
        Me.Label11.Text = "label2"
        '
        'pnlmonograph
        '
        Me.pnlmonograph.AutoScroll = True
        Me.pnlmonograph.AutoScrollMargin = New System.Drawing.Size(2, 2)
        Me.pnlmonograph.AutoSize = True
        Me.pnlmonograph.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(227, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.pnlmonograph.Controls.Add(Me.trvDefinitionSubType)
        Me.pnlmonograph.Controls.Add(Me.pnlSubTypeDefinition)
        Me.pnlmonograph.Controls.Add(Me.trvDefSubType1)
        Me.pnlmonograph.Controls.Add(Me.Label31)
        Me.pnlmonograph.Controls.Add(Me.Label32)
        Me.pnlmonograph.Controls.Add(Me.lblSnomedID)
        Me.pnlmonograph.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlmonograph.Location = New System.Drawing.Point(1, 33)
        Me.pnlmonograph.Name = "pnlmonograph"
        Me.pnlmonograph.Size = New System.Drawing.Size(704, 258)
        Me.pnlmonograph.TabIndex = 16
        '
        'trvDefinitionSubType
        '
        Me.trvDefinitionSubType.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.trvDefinitionSubType.ForeColor = System.Drawing.SystemColors.WindowText
        Me.trvDefinitionSubType.ImageIndex = 0
        Me.trvDefinitionSubType.ImageList = Me.imgTreeVIew
        Me.trvDefinitionSubType.Indent = 20
        Me.trvDefinitionSubType.ItemHeight = 20
        Me.trvDefinitionSubType.Location = New System.Drawing.Point(389, 321)
        Me.trvDefinitionSubType.Name = "trvDefinitionSubType"
        Me.trvDefinitionSubType.SelectedImageIndex = 0
        Me.trvDefinitionSubType.Size = New System.Drawing.Size(77, 35)
        Me.trvDefinitionSubType.TabIndex = 59
        Me.trvDefinitionSubType.Visible = False
        '
        'imgTreeVIew
        '
        Me.imgTreeVIew.ImageStream = CType(resources.GetObject("imgTreeVIew.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imgTreeVIew.TransparentColor = System.Drawing.Color.Transparent
        Me.imgTreeVIew.Images.SetKeyName(0, "Category_New.ico")
        Me.imgTreeVIew.Images.SetKeyName(1, "Drugs.ico")
        Me.imgTreeVIew.Images.SetKeyName(2, "Radiology_01.ico")
        Me.imgTreeVIew.Images.SetKeyName(3, "Small Arrow.ico")
        Me.imgTreeVIew.Images.SetKeyName(4, "Bullet06.ico")
        Me.imgTreeVIew.Images.SetKeyName(5, "Browse.ico")
        Me.imgTreeVIew.Images.SetKeyName(6, "ICD 09.ico")
        Me.imgTreeVIew.Images.SetKeyName(7, "Defination.ico")
        '
        'pnlSubTypeDefinition
        '
        Me.pnlSubTypeDefinition.Location = New System.Drawing.Point(386, 85)
        Me.pnlSubTypeDefinition.Name = "pnlSubTypeDefinition"
        Me.pnlSubTypeDefinition.Size = New System.Drawing.Size(227, 245)
        Me.pnlSubTypeDefinition.TabIndex = 59
        '
        'trvDefSubType1
        '
        Me.trvDefSubType1.BackColor = System.Drawing.SystemColors.Window
        Me.trvDefSubType1.Location = New System.Drawing.Point(287, 321)
        Me.trvDefSubType1.Name = "trvDefSubType1"
        Me.trvDefSubType1.Size = New System.Drawing.Size(77, 35)
        Me.trvDefSubType1.TabIndex = 50
        Me.trvDefSubType1.Visible = False
        '
        'Label31
        '
        Me.Label31.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label31.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label31.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label31.Location = New System.Drawing.Point(0, 358)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(687, 1)
        Me.Label31.TabIndex = 11
        Me.Label31.Text = "label2"
        '
        'Label32
        '
        Me.Label32.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label32.AutoSize = True
        Me.Label32.Location = New System.Drawing.Point(204, 359)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(75, 14)
        Me.Label32.TabIndex = 17
        Me.Label32.Text = "SnoMed ID :"
        Me.Label32.Visible = False
        '
        'lblSnomedID
        '
        Me.lblSnomedID.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblSnomedID.AutoSize = True
        Me.lblSnomedID.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSnomedID.Location = New System.Drawing.Point(278, 359)
        Me.lblSnomedID.Name = "lblSnomedID"
        Me.lblSnomedID.Size = New System.Drawing.Size(0, 14)
        Me.lblSnomedID.TabIndex = 18
        Me.lblSnomedID.Visible = False
        '
        'Label20
        '
        Me.Label20.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label20.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label20.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label20.Location = New System.Drawing.Point(705, 33)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(1, 258)
        Me.Label20.TabIndex = 61
        Me.Label20.Text = "label3"
        '
        'Label53
        '
        Me.Label53.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label53.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label53.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label53.Location = New System.Drawing.Point(0, 33)
        Me.Label53.Name = "Label53"
        Me.Label53.Size = New System.Drawing.Size(1, 258)
        Me.Label53.TabIndex = 17
        Me.Label53.Text = "label4"
        '
        'pnlDIScreenResult
        '
        Me.pnlDIScreenResult.AutoScroll = True
        Me.pnlDIScreenResult.AutoScrollMargin = New System.Drawing.Size(2, 2)
        Me.pnlDIScreenResult.AutoSize = True
        Me.pnlDIScreenResult.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(227, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.pnlDIScreenResult.Controls.Add(Me.Panel4)
        Me.pnlDIScreenResult.Controls.Add(Me.Panel3)
        Me.pnlDIScreenResult.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlDIScreenResult.Location = New System.Drawing.Point(0, 33)
        Me.pnlDIScreenResult.Name = "pnlDIScreenResult"
        Me.pnlDIScreenResult.Size = New System.Drawing.Size(706, 258)
        Me.pnlDIScreenResult.TabIndex = 14
        '
        'Panel4
        '
        Me.Panel4.AutoScroll = True
        Me.Panel4.AutoScrollMargin = New System.Drawing.Size(2, 2)
        Me.Panel4.AutoSize = True
        Me.Panel4.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(227, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.Panel4.Controls.Add(Me.Label36)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel4.Location = New System.Drawing.Point(0, 0)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(706, 258)
        Me.Panel4.TabIndex = 14
        '
        'Label36
        '
        Me.Label36.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label36.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label36.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label36.Location = New System.Drawing.Point(0, 257)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(706, 1)
        Me.Label36.TabIndex = 11
        Me.Label36.Text = "label2"
        '
        'Panel3
        '
        Me.Panel3.AutoScroll = True
        Me.Panel3.AutoScrollMargin = New System.Drawing.Size(2, 2)
        Me.Panel3.AutoSize = True
        Me.Panel3.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(227, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.Panel3.Controls.Add(Me.Label35)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel3.Location = New System.Drawing.Point(0, 0)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(706, 258)
        Me.Panel3.TabIndex = 13
        '
        'Label35
        '
        Me.Label35.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label35.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label35.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label35.Location = New System.Drawing.Point(0, 257)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(706, 1)
        Me.Label35.TabIndex = 11
        Me.Label35.Text = "label2"
        '
        'pnlsnomadedetail
        '
        Me.pnlsnomadedetail.Controls.Add(Me.Panel2)
        Me.pnlsnomadedetail.Controls.Add(Me.Panel14)
        Me.pnlsnomadedetail.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlsnomadedetail.Location = New System.Drawing.Point(0, 291)
        Me.pnlsnomadedetail.Name = "pnlsnomadedetail"
        Me.pnlsnomadedetail.Size = New System.Drawing.Size(706, 305)
        Me.pnlsnomadedetail.TabIndex = 12
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.pnltxtCVXCode)
        Me.Panel2.Controls.Add(Me.pnlShowCQM)
        Me.Panel2.Controls.Add(Me.btnClsCVXCode)
        Me.Panel2.Controls.Add(Me.btnCVXCode)
        Me.Panel2.Controls.Add(Me.Panel10)
        Me.Panel2.Controls.Add(Me.Label18)
        Me.Panel2.Controls.Add(Me.Panel9)
        Me.Panel2.Controls.Add(Me.btnclloinc)
        Me.Panel2.Controls.Add(Me.btnbrloinc)
        Me.Panel2.Controls.Add(Me.Label58)
        Me.Panel2.Controls.Add(Me.btnClearSNOMEDRefusedCode)
        Me.Panel2.Controls.Add(Me.Label59SNOMEDRefusedCode)
        Me.Panel2.Controls.Add(Me.Label54)
        Me.Panel2.Controls.Add(Me.btn_ICD9Code)
        Me.Panel2.Controls.Add(Me.btnConceptID)
        Me.Panel2.Controls.Add(Me.btn_CPTCode)
        Me.Panel2.Controls.Add(Me.Label19)
        Me.Panel2.Controls.Add(Me.Label57)
        Me.Panel2.Controls.Add(Me.lblHistoryTypelabel)
        Me.Panel2.Controls.Add(Me.lblHistoryType)
        Me.Panel2.Controls.Add(Me.Panel7)
        Me.Panel2.Controls.Add(Me.Panel6)
        Me.Panel2.Controls.Add(Me.Panel5)
        Me.Panel2.Controls.Add(Me.Label39)
        Me.Panel2.Controls.Add(Me.Label37)
        Me.Panel2.Controls.Add(Me.lblNDCid)
        Me.Panel2.Controls.Add(Me.Label10)
        Me.Panel2.Controls.Add(Me.Label12)
        Me.Panel2.Controls.Add(Me.Label28)
        Me.Panel2.Controls.Add(Me.lbldescid)
        Me.Panel2.Controls.Add(Me.Label30)
        Me.Panel2.Controls.Add(Me.lblRxNorm)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.Panel2.Size = New System.Drawing.Size(706, 280)
        Me.Panel2.TabIndex = 1
        '
        'pnlShowCQM
        '
        Me.pnlShowCQM.Controls.Add(Me.Panel11)
        Me.pnlShowCQM.Controls.Add(Me.Label72)
        Me.pnlShowCQM.Controls.Add(Me.lblAllergyIntelorenceType)
        Me.pnlShowCQM.Controls.Add(Me.btnBrowseCqm)
        Me.pnlShowCQM.Controls.Add(Me.cmbAllergyIntelorenceType)
        Me.pnlShowCQM.Controls.Add(Me.btnClearCqm)
        Me.pnlShowCQM.Controls.Add(Me.Label78)
        Me.pnlShowCQM.Controls.Add(Me.Label77)
        Me.pnlShowCQM.Controls.Add(Me.ChkResolvedEnddate)
        Me.pnlShowCQM.Controls.Add(Me.Panel12)
        Me.pnlShowCQM.Controls.Add(Me.ResolvedEndDate)
        Me.pnlShowCQM.Controls.Add(Me.btnBrowseUDI)
        Me.pnlShowCQM.Controls.Add(Me.cmbAllergySeverity)
        Me.pnlShowCQM.Controls.Add(Me.Label83)
        Me.pnlShowCQM.Controls.Add(Me.Label59)
        Me.pnlShowCQM.Controls.Add(Me.cmbStatus)
        Me.pnlShowCQM.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlShowCQM.Location = New System.Drawing.Point(1, 145)
        Me.pnlShowCQM.Name = "pnlShowCQM"
        Me.pnlShowCQM.Size = New System.Drawing.Size(704, 135)
        Me.pnlShowCQM.TabIndex = 249
        Me.pnlShowCQM.Visible = False
        '
        'Panel11
        '
        Me.Panel11.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.Panel11.Controls.Add(Me.txtCqm)
        Me.Panel11.Controls.Add(Me.Label64)
        Me.Panel11.Controls.Add(Me.Label69)
        Me.Panel11.Controls.Add(Me.Label70)
        Me.Panel11.Controls.Add(Me.Label71)
        Me.Panel11.Location = New System.Drawing.Point(161, 3)
        Me.Panel11.Name = "Panel11"
        Me.Panel11.Size = New System.Drawing.Size(445, 23)
        Me.Panel11.TabIndex = 235
        '
        'txtCqm
        '
        Me.txtCqm.AutoEllipsis = True
        Me.txtCqm.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCqm.ForeColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.txtCqm.Location = New System.Drawing.Point(1, 1)
        Me.txtCqm.Name = "txtCqm"
        Me.txtCqm.Padding = New System.Windows.Forms.Padding(2, 2, 0, 0)
        Me.txtCqm.Size = New System.Drawing.Size(443, 21)
        Me.txtCqm.TabIndex = 209
        '
        'Label64
        '
        Me.Label64.BackColor = System.Drawing.Color.FromArgb(CType(CType(171, Byte), Integer), CType(CType(173, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.Label64.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label64.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label64.Location = New System.Drawing.Point(444, 1)
        Me.Label64.Name = "Label64"
        Me.Label64.Size = New System.Drawing.Size(1, 21)
        Me.Label64.TabIndex = 15
        Me.Label64.Text = "label2"
        '
        'Label69
        '
        Me.Label69.BackColor = System.Drawing.Color.FromArgb(CType(CType(171, Byte), Integer), CType(CType(173, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.Label69.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label69.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label69.Location = New System.Drawing.Point(0, 1)
        Me.Label69.Name = "Label69"
        Me.Label69.Size = New System.Drawing.Size(1, 21)
        Me.Label69.TabIndex = 14
        Me.Label69.Text = "label2"
        '
        'Label70
        '
        Me.Label70.BackColor = System.Drawing.Color.FromArgb(CType(CType(171, Byte), Integer), CType(CType(173, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.Label70.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label70.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label70.Location = New System.Drawing.Point(0, 0)
        Me.Label70.Name = "Label70"
        Me.Label70.Size = New System.Drawing.Size(445, 1)
        Me.Label70.TabIndex = 13
        Me.Label70.Text = "label2"
        '
        'Label71
        '
        Me.Label71.BackColor = System.Drawing.Color.FromArgb(CType(CType(171, Byte), Integer), CType(CType(173, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.Label71.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label71.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label71.Location = New System.Drawing.Point(0, 22)
        Me.Label71.Name = "Label71"
        Me.Label71.Size = New System.Drawing.Size(445, 1)
        Me.Label71.TabIndex = 12
        Me.Label71.Text = "label2"
        '
        'Label72
        '
        Me.Label72.AutoSize = True
        Me.Label72.Location = New System.Drawing.Point(57, 7)
        Me.Label72.Name = "Label72"
        Me.Label72.Size = New System.Drawing.Size(101, 14)
        Me.Label72.TabIndex = 232
        Me.Label72.Text = "CQM Categories :"
        '
        'lblAllergyIntelorenceType
        '
        Me.lblAllergyIntelorenceType.AutoSize = True
        Me.lblAllergyIntelorenceType.Location = New System.Drawing.Point(9, 114)
        Me.lblAllergyIntelorenceType.Name = "lblAllergyIntelorenceType"
        Me.lblAllergyIntelorenceType.Size = New System.Drawing.Size(149, 14)
        Me.lblAllergyIntelorenceType.TabIndex = 247
        Me.lblAllergyIntelorenceType.Text = "Allergy Intolerance Type :"
        '
        'btnBrowseCqm
        '
        Me.btnBrowseCqm.BackColor = System.Drawing.Color.Transparent
        Me.btnBrowseCqm.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.btnBrowseCqm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnBrowseCqm.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnBrowseCqm.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnBrowseCqm.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnBrowseCqm.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBrowseCqm.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBrowseCqm.Image = CType(resources.GetObject("btnBrowseCqm.Image"), System.Drawing.Image)
        Me.btnBrowseCqm.Location = New System.Drawing.Point(612, 2)
        Me.btnBrowseCqm.Name = "btnBrowseCqm"
        Me.btnBrowseCqm.Size = New System.Drawing.Size(22, 23)
        Me.btnBrowseCqm.TabIndex = 233
        Me.btnBrowseCqm.UseVisualStyleBackColor = False
        '
        'cmbAllergyIntelorenceType
        '
        Me.cmbAllergyIntelorenceType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbAllergyIntelorenceType.FormattingEnabled = True
        Me.cmbAllergyIntelorenceType.Location = New System.Drawing.Point(161, 110)
        Me.cmbAllergyIntelorenceType.Name = "cmbAllergyIntelorenceType"
        Me.cmbAllergyIntelorenceType.Size = New System.Drawing.Size(445, 22)
        Me.cmbAllergyIntelorenceType.TabIndex = 246
        '
        'btnClearCqm
        '
        Me.btnClearCqm.BackColor = System.Drawing.Color.Transparent
        Me.btnClearCqm.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.btnClearCqm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClearCqm.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnClearCqm.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnClearCqm.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnClearCqm.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClearCqm.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClearCqm.Image = CType(resources.GetObject("btnClearCqm.Image"), System.Drawing.Image)
        Me.btnClearCqm.Location = New System.Drawing.Point(638, 2)
        Me.btnClearCqm.Name = "btnClearCqm"
        Me.btnClearCqm.Size = New System.Drawing.Size(22, 23)
        Me.btnClearCqm.TabIndex = 234
        Me.btnClearCqm.UseVisualStyleBackColor = False
        '
        'Label78
        '
        Me.Label78.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label78.AutoSize = True
        Me.Label78.Location = New System.Drawing.Point(352, 61)
        Me.Label78.Name = "Label78"
        Me.Label78.Size = New System.Drawing.Size(119, 14)
        Me.Label78.TabIndex = 245
        Me.Label78.Text = "Resolved/End Date :"
        '
        'Label77
        '
        Me.Label77.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label77.AutoSize = True
        Me.Label77.Location = New System.Drawing.Point(123, 34)
        Me.Label77.Name = "Label77"
        Me.Label77.Size = New System.Drawing.Size(35, 14)
        Me.Label77.TabIndex = 236
        Me.Label77.Text = "UDI :"
        Me.Label77.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'ChkResolvedEnddate
        '
        Me.ChkResolvedEnddate.AutoSize = True
        Me.ChkResolvedEnddate.Location = New System.Drawing.Point(476, 61)
        Me.ChkResolvedEnddate.Name = "ChkResolvedEnddate"
        Me.ChkResolvedEnddate.Size = New System.Drawing.Size(15, 14)
        Me.ChkResolvedEnddate.TabIndex = 244
        Me.ChkResolvedEnddate.UseVisualStyleBackColor = True
        '
        'Panel12
        '
        Me.Panel12.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.Panel12.Controls.Add(Me.lblUDI)
        Me.Panel12.Controls.Add(Me.Label73)
        Me.Panel12.Controls.Add(Me.Label74)
        Me.Panel12.Controls.Add(Me.Label75)
        Me.Panel12.Controls.Add(Me.Label76)
        Me.Panel12.Location = New System.Drawing.Point(161, 30)
        Me.Panel12.Name = "Panel12"
        Me.Panel12.Size = New System.Drawing.Size(445, 23)
        Me.Panel12.TabIndex = 238
        '
        'lblUDI
        '
        Me.lblUDI.AutoEllipsis = True
        Me.lblUDI.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUDI.ForeColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.lblUDI.Location = New System.Drawing.Point(1, 1)
        Me.lblUDI.Name = "lblUDI"
        Me.lblUDI.Padding = New System.Windows.Forms.Padding(2, 2, 0, 0)
        Me.lblUDI.Size = New System.Drawing.Size(443, 21)
        Me.lblUDI.TabIndex = 209
        '
        'Label73
        '
        Me.Label73.BackColor = System.Drawing.Color.FromArgb(CType(CType(171, Byte), Integer), CType(CType(173, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.Label73.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label73.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label73.Location = New System.Drawing.Point(444, 1)
        Me.Label73.Name = "Label73"
        Me.Label73.Size = New System.Drawing.Size(1, 21)
        Me.Label73.TabIndex = 15
        Me.Label73.Text = "label2"
        '
        'Label74
        '
        Me.Label74.BackColor = System.Drawing.Color.FromArgb(CType(CType(171, Byte), Integer), CType(CType(173, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.Label74.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label74.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label74.Location = New System.Drawing.Point(0, 1)
        Me.Label74.Name = "Label74"
        Me.Label74.Size = New System.Drawing.Size(1, 21)
        Me.Label74.TabIndex = 14
        Me.Label74.Text = "label2"
        '
        'Label75
        '
        Me.Label75.BackColor = System.Drawing.Color.FromArgb(CType(CType(171, Byte), Integer), CType(CType(173, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.Label75.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label75.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label75.Location = New System.Drawing.Point(0, 0)
        Me.Label75.Name = "Label75"
        Me.Label75.Size = New System.Drawing.Size(445, 1)
        Me.Label75.TabIndex = 13
        Me.Label75.Text = "label2"
        '
        'Label76
        '
        Me.Label76.BackColor = System.Drawing.Color.FromArgb(CType(CType(171, Byte), Integer), CType(CType(173, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.Label76.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label76.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label76.Location = New System.Drawing.Point(0, 22)
        Me.Label76.Name = "Label76"
        Me.Label76.Size = New System.Drawing.Size(445, 1)
        Me.Label76.TabIndex = 12
        Me.Label76.Text = "label2"
        '
        'ResolvedEndDate
        '
        Me.ResolvedEndDate.CustomFormat = "    MM/dd/yyyy"
        Me.ResolvedEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.ResolvedEndDate.Location = New System.Drawing.Point(474, 57)
        Me.ResolvedEndDate.Name = "ResolvedEndDate"
        Me.ResolvedEndDate.Size = New System.Drawing.Size(132, 22)
        Me.ResolvedEndDate.TabIndex = 242
        Me.ResolvedEndDate.Value = New Date(2017, 8, 28, 18, 24, 0, 0)
        '
        'btnBrowseUDI
        '
        Me.btnBrowseUDI.BackColor = System.Drawing.Color.Transparent
        Me.btnBrowseUDI.BackgroundImage = CType(resources.GetObject("btnBrowseUDI.BackgroundImage"), System.Drawing.Image)
        Me.btnBrowseUDI.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnBrowseUDI.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnBrowseUDI.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnBrowseUDI.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnBrowseUDI.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnBrowseUDI.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBrowseUDI.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBrowseUDI.Image = CType(resources.GetObject("btnBrowseUDI.Image"), System.Drawing.Image)
        Me.btnBrowseUDI.Location = New System.Drawing.Point(612, 29)
        Me.btnBrowseUDI.Name = "btnBrowseUDI"
        Me.btnBrowseUDI.Size = New System.Drawing.Size(22, 23)
        Me.btnBrowseUDI.TabIndex = 237
        Me.btnBrowseUDI.UseVisualStyleBackColor = False
        '
        'cmbAllergySeverity
        '
        Me.cmbAllergySeverity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbAllergySeverity.FormattingEnabled = True
        Me.cmbAllergySeverity.Location = New System.Drawing.Point(161, 84)
        Me.cmbAllergySeverity.Name = "cmbAllergySeverity"
        Me.cmbAllergySeverity.Size = New System.Drawing.Size(445, 22)
        Me.cmbAllergySeverity.TabIndex = 243
        '
        'Label83
        '
        Me.Label83.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label83.AutoSize = True
        Me.Label83.Location = New System.Drawing.Point(59, 61)
        Me.Label83.Name = "Label83"
        Me.Label83.Size = New System.Drawing.Size(99, 14)
        Me.Label83.TabIndex = 239
        Me.Label83.Text = "Concern Status :"
        Me.Label83.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label59
        '
        Me.Label59.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label59.AutoSize = True
        Me.Label59.Location = New System.Drawing.Point(59, 88)
        Me.Label59.Name = "Label59"
        Me.Label59.Size = New System.Drawing.Size(99, 14)
        Me.Label59.TabIndex = 241
        Me.Label59.Text = "Allergy Severity :"
        Me.Label59.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmbStatus
        '
        Me.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbStatus.FormattingEnabled = True
        Me.cmbStatus.Location = New System.Drawing.Point(161, 57)
        Me.cmbStatus.Name = "cmbStatus"
        Me.cmbStatus.Size = New System.Drawing.Size(188, 22)
        Me.cmbStatus.TabIndex = 240
        '
        'btnClsCVXCode
        '
        Me.btnClsCVXCode.BackColor = System.Drawing.Color.Transparent
        Me.btnClsCVXCode.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.btnClsCVXCode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClsCVXCode.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnClsCVXCode.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnClsCVXCode.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnClsCVXCode.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClsCVXCode.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClsCVXCode.Image = CType(resources.GetObject("btnClsCVXCode.Image"), System.Drawing.Image)
        Me.btnClsCVXCode.Location = New System.Drawing.Point(639, 117)
        Me.btnClsCVXCode.Name = "btnClsCVXCode"
        Me.btnClsCVXCode.Size = New System.Drawing.Size(22, 23)
        Me.btnClsCVXCode.TabIndex = 255
        Me.btnClsCVXCode.UseVisualStyleBackColor = False
        '
        'btnCVXCode
        '
        Me.btnCVXCode.BackColor = System.Drawing.Color.Transparent
        Me.btnCVXCode.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.btnCVXCode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnCVXCode.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnCVXCode.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnCVXCode.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnCVXCode.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCVXCode.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCVXCode.Image = CType(resources.GetObject("btnCVXCode.Image"), System.Drawing.Image)
        Me.btnCVXCode.Location = New System.Drawing.Point(613, 117)
        Me.btnCVXCode.Name = "btnCVXCode"
        Me.btnCVXCode.Size = New System.Drawing.Size(22, 23)
        Me.btnCVXCode.TabIndex = 254
        Me.btnCVXCode.UseVisualStyleBackColor = False
        '
        'Panel10
        '
        Me.Panel10.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.Panel10.Controls.Add(Me.txtloinc)
        Me.Panel10.Controls.Add(Me.Label65)
        Me.Panel10.Controls.Add(Me.Label66)
        Me.Panel10.Controls.Add(Me.Label67)
        Me.Panel10.Controls.Add(Me.Label68)
        Me.Panel10.Location = New System.Drawing.Point(162, 90)
        Me.Panel10.Name = "Panel10"
        Me.Panel10.Size = New System.Drawing.Size(445, 23)
        Me.Panel10.TabIndex = 227
        '
        'txtloinc
        '
        Me.txtloinc.AutoEllipsis = True
        Me.txtloinc.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtloinc.ForeColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.txtloinc.Location = New System.Drawing.Point(1, 1)
        Me.txtloinc.Name = "txtloinc"
        Me.txtloinc.Padding = New System.Windows.Forms.Padding(2, 2, 0, 0)
        Me.txtloinc.Size = New System.Drawing.Size(443, 21)
        Me.txtloinc.TabIndex = 209
        '
        'Label65
        '
        Me.Label65.BackColor = System.Drawing.Color.FromArgb(CType(CType(171, Byte), Integer), CType(CType(173, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.Label65.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label65.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label65.Location = New System.Drawing.Point(444, 1)
        Me.Label65.Name = "Label65"
        Me.Label65.Size = New System.Drawing.Size(1, 21)
        Me.Label65.TabIndex = 15
        Me.Label65.Text = "label2"
        '
        'Label66
        '
        Me.Label66.BackColor = System.Drawing.Color.FromArgb(CType(CType(171, Byte), Integer), CType(CType(173, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.Label66.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label66.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label66.Location = New System.Drawing.Point(0, 1)
        Me.Label66.Name = "Label66"
        Me.Label66.Size = New System.Drawing.Size(1, 21)
        Me.Label66.TabIndex = 14
        Me.Label66.Text = "label2"
        '
        'Label67
        '
        Me.Label67.BackColor = System.Drawing.Color.FromArgb(CType(CType(171, Byte), Integer), CType(CType(173, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.Label67.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label67.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label67.Location = New System.Drawing.Point(0, 0)
        Me.Label67.Name = "Label67"
        Me.Label67.Size = New System.Drawing.Size(445, 1)
        Me.Label67.TabIndex = 13
        Me.Label67.Text = "label2"
        '
        'Label68
        '
        Me.Label68.BackColor = System.Drawing.Color.FromArgb(CType(CType(171, Byte), Integer), CType(CType(173, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.Label68.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label68.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label68.Location = New System.Drawing.Point(0, 22)
        Me.Label68.Name = "Label68"
        Me.Label68.Size = New System.Drawing.Size(445, 1)
        Me.Label68.TabIndex = 12
        Me.Label68.Text = "label2"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(90, 121)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(69, 14)
        Me.Label18.TabIndex = 249
        Me.Label18.Text = "CVX Code :"
        '
        'Panel9
        '
        Me.Panel9.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.Panel9.Controls.Add(Me.txtSNOMEDRefusedCode)
        Me.Panel9.Controls.Add(Me.Label60)
        Me.Panel9.Controls.Add(Me.Label61)
        Me.Panel9.Controls.Add(Me.Label62)
        Me.Panel9.Controls.Add(Me.Label63)
        Me.Panel9.Location = New System.Drawing.Point(162, 63)
        Me.Panel9.Name = "Panel9"
        Me.Panel9.Size = New System.Drawing.Size(445, 23)
        Me.Panel9.TabIndex = 226
        '
        'txtSNOMEDRefusedCode
        '
        Me.txtSNOMEDRefusedCode.AutoEllipsis = True
        Me.txtSNOMEDRefusedCode.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtSNOMEDRefusedCode.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSNOMEDRefusedCode.ForeColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.txtSNOMEDRefusedCode.Location = New System.Drawing.Point(1, 1)
        Me.txtSNOMEDRefusedCode.Name = "txtSNOMEDRefusedCode"
        Me.txtSNOMEDRefusedCode.Padding = New System.Windows.Forms.Padding(2, 2, 0, 0)
        Me.txtSNOMEDRefusedCode.Size = New System.Drawing.Size(443, 21)
        Me.txtSNOMEDRefusedCode.TabIndex = 209
        '
        'Label60
        '
        Me.Label60.BackColor = System.Drawing.Color.FromArgb(CType(CType(171, Byte), Integer), CType(CType(173, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.Label60.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label60.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label60.Location = New System.Drawing.Point(444, 1)
        Me.Label60.Name = "Label60"
        Me.Label60.Size = New System.Drawing.Size(1, 21)
        Me.Label60.TabIndex = 15
        Me.Label60.Text = "label2"
        '
        'Label61
        '
        Me.Label61.BackColor = System.Drawing.Color.FromArgb(CType(CType(171, Byte), Integer), CType(CType(173, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.Label61.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label61.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label61.Location = New System.Drawing.Point(0, 1)
        Me.Label61.Name = "Label61"
        Me.Label61.Size = New System.Drawing.Size(1, 21)
        Me.Label61.TabIndex = 14
        Me.Label61.Text = "label2"
        '
        'Label62
        '
        Me.Label62.BackColor = System.Drawing.Color.FromArgb(CType(CType(171, Byte), Integer), CType(CType(173, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.Label62.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label62.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label62.Location = New System.Drawing.Point(0, 0)
        Me.Label62.Name = "Label62"
        Me.Label62.Size = New System.Drawing.Size(445, 1)
        Me.Label62.TabIndex = 13
        Me.Label62.Text = "label2"
        '
        'Label63
        '
        Me.Label63.BackColor = System.Drawing.Color.FromArgb(CType(CType(171, Byte), Integer), CType(CType(173, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.Label63.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label63.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label63.Location = New System.Drawing.Point(0, 22)
        Me.Label63.Name = "Label63"
        Me.Label63.Size = New System.Drawing.Size(445, 1)
        Me.Label63.TabIndex = 12
        Me.Label63.Text = "label2"
        '
        'btnclloinc
        '
        Me.btnclloinc.BackColor = System.Drawing.Color.Transparent
        Me.btnclloinc.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.btnclloinc.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnclloinc.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnclloinc.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnclloinc.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnclloinc.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnclloinc.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclloinc.Image = CType(resources.GetObject("btnclloinc.Image"), System.Drawing.Image)
        Me.btnclloinc.Location = New System.Drawing.Point(638, 89)
        Me.btnclloinc.Name = "btnclloinc"
        Me.btnclloinc.Size = New System.Drawing.Size(22, 23)
        Me.btnclloinc.TabIndex = 225
        Me.btnclloinc.UseVisualStyleBackColor = False
        '
        'btnbrloinc
        '
        Me.btnbrloinc.BackColor = System.Drawing.Color.Transparent
        Me.btnbrloinc.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.btnbrloinc.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnbrloinc.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnbrloinc.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnbrloinc.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnbrloinc.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnbrloinc.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnbrloinc.Image = CType(resources.GetObject("btnbrloinc.Image"), System.Drawing.Image)
        Me.btnbrloinc.Location = New System.Drawing.Point(612, 89)
        Me.btnbrloinc.Name = "btnbrloinc"
        Me.btnbrloinc.Size = New System.Drawing.Size(22, 23)
        Me.btnbrloinc.TabIndex = 224
        Me.btnbrloinc.UseVisualStyleBackColor = False
        '
        'Label58
        '
        Me.Label58.AutoSize = True
        Me.Label58.Location = New System.Drawing.Point(78, 94)
        Me.Label58.Name = "Label58"
        Me.Label58.Size = New System.Drawing.Size(81, 14)
        Me.Label58.TabIndex = 222
        Me.Label58.Text = "LOINC Code :"
        '
        'btnClearSNOMEDRefusedCode
        '
        Me.btnClearSNOMEDRefusedCode.BackColor = System.Drawing.Color.Transparent
        Me.btnClearSNOMEDRefusedCode.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.btnClearSNOMEDRefusedCode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClearSNOMEDRefusedCode.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnClearSNOMEDRefusedCode.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnClearSNOMEDRefusedCode.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnClearSNOMEDRefusedCode.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClearSNOMEDRefusedCode.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClearSNOMEDRefusedCode.Image = CType(resources.GetObject("btnClearSNOMEDRefusedCode.Image"), System.Drawing.Image)
        Me.btnClearSNOMEDRefusedCode.Location = New System.Drawing.Point(638, 61)
        Me.btnClearSNOMEDRefusedCode.Name = "btnClearSNOMEDRefusedCode"
        Me.btnClearSNOMEDRefusedCode.Size = New System.Drawing.Size(22, 23)
        Me.btnClearSNOMEDRefusedCode.TabIndex = 221
        Me.btnClearSNOMEDRefusedCode.UseVisualStyleBackColor = False
        '
        'Label59SNOMEDRefusedCode
        '
        Me.Label59SNOMEDRefusedCode.BackColor = System.Drawing.Color.Transparent
        Me.Label59SNOMEDRefusedCode.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.Label59SNOMEDRefusedCode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Label59SNOMEDRefusedCode.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Label59SNOMEDRefusedCode.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label59SNOMEDRefusedCode.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.Label59SNOMEDRefusedCode.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label59SNOMEDRefusedCode.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label59SNOMEDRefusedCode.Image = CType(resources.GetObject("Label59SNOMEDRefusedCode.Image"), System.Drawing.Image)
        Me.Label59SNOMEDRefusedCode.Location = New System.Drawing.Point(612, 61)
        Me.Label59SNOMEDRefusedCode.Name = "Label59SNOMEDRefusedCode"
        Me.Label59SNOMEDRefusedCode.Size = New System.Drawing.Size(22, 23)
        Me.Label59SNOMEDRefusedCode.TabIndex = 220
        Me.Label59SNOMEDRefusedCode.UseVisualStyleBackColor = False
        '
        'Label54
        '
        Me.Label54.AutoSize = True
        Me.Label54.Location = New System.Drawing.Point(30, 67)
        Me.Label54.Name = "Label54"
        Me.Label54.Size = New System.Drawing.Size(129, 14)
        Me.Label54.TabIndex = 218
        Me.Label54.Text = "Refusal/Reason Code :"
        '
        'btn_ICD9Code
        '
        Me.btn_ICD9Code.BackColor = System.Drawing.Color.Transparent
        Me.btn_ICD9Code.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.btn_ICD9Code.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btn_ICD9Code.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btn_ICD9Code.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btn_ICD9Code.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btn_ICD9Code.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_ICD9Code.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_ICD9Code.Image = CType(resources.GetObject("btn_ICD9Code.Image"), System.Drawing.Image)
        Me.btn_ICD9Code.Location = New System.Drawing.Point(356, 8)
        Me.btn_ICD9Code.Name = "btn_ICD9Code"
        Me.btn_ICD9Code.Size = New System.Drawing.Size(22, 23)
        Me.btn_ICD9Code.TabIndex = 206
        Me.btn_ICD9Code.UseVisualStyleBackColor = False
        '
        'btnConceptID
        '
        Me.btnConceptID.BackColor = System.Drawing.Color.Transparent
        Me.btnConceptID.BackgroundImage = CType(resources.GetObject("btnConceptID.BackgroundImage"), System.Drawing.Image)
        Me.btnConceptID.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnConceptID.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnConceptID.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnConceptID.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnConceptID.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnConceptID.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnConceptID.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnConceptID.Image = CType(resources.GetObject("btnConceptID.Image"), System.Drawing.Image)
        Me.btnConceptID.Location = New System.Drawing.Point(612, 34)
        Me.btnConceptID.Name = "btnConceptID"
        Me.btnConceptID.Size = New System.Drawing.Size(22, 23)
        Me.btnConceptID.TabIndex = 208
        Me.btnConceptID.UseVisualStyleBackColor = False
        '
        'btn_CPTCode
        '
        Me.btn_CPTCode.BackColor = System.Drawing.Color.Transparent
        Me.btn_CPTCode.BackgroundImage = CType(resources.GetObject("btn_CPTCode.BackgroundImage"), System.Drawing.Image)
        Me.btn_CPTCode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btn_CPTCode.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btn_CPTCode.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btn_CPTCode.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btn_CPTCode.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_CPTCode.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_CPTCode.Image = CType(resources.GetObject("btn_CPTCode.Image"), System.Drawing.Image)
        Me.btn_CPTCode.Location = New System.Drawing.Point(612, 6)
        Me.btn_CPTCode.Name = "btn_CPTCode"
        Me.btn_CPTCode.Size = New System.Drawing.Size(22, 23)
        Me.btn_CPTCode.TabIndex = 207
        Me.btn_CPTCode.UseVisualStyleBackColor = False
        '
        'Label19
        '
        Me.Label19.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label19.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label19.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(0, 4)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(1, 276)
        Me.Label19.TabIndex = 217
        Me.Label19.Text = "label4"
        '
        'Label57
        '
        Me.Label57.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label57.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label57.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label57.Location = New System.Drawing.Point(705, 4)
        Me.Label57.Name = "Label57"
        Me.Label57.Size = New System.Drawing.Size(1, 276)
        Me.Label57.TabIndex = 216
        Me.Label57.Text = "label3"
        '
        'lblHistoryTypelabel
        '
        Me.lblHistoryTypelabel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblHistoryTypelabel.AutoSize = True
        Me.lblHistoryTypelabel.Location = New System.Drawing.Point(660, 15)
        Me.lblHistoryTypelabel.Name = "lblHistoryTypelabel"
        Me.lblHistoryTypelabel.Size = New System.Drawing.Size(84, 14)
        Me.lblHistoryTypelabel.TabIndex = 215
        Me.lblHistoryTypelabel.Text = "History Type :"
        '
        'lblHistoryType
        '
        Me.lblHistoryType.AutoEllipsis = True
        Me.lblHistoryType.BackColor = System.Drawing.Color.Transparent
        Me.lblHistoryType.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHistoryType.Location = New System.Drawing.Point(746, 15)
        Me.lblHistoryType.Name = "lblHistoryType"
        Me.lblHistoryType.Size = New System.Drawing.Size(122, 14)
        Me.lblHistoryType.TabIndex = 214
        '
        'Panel7
        '
        Me.Panel7.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.Panel7.Controls.Add(Me.lnlLbllCPTCode)
        Me.Panel7.Controls.Add(Me.Label49)
        Me.Panel7.Controls.Add(Me.Label50)
        Me.Panel7.Controls.Add(Me.Label51)
        Me.Panel7.Controls.Add(Me.Label52)
        Me.Panel7.Location = New System.Drawing.Point(433, 9)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(174, 23)
        Me.Panel7.TabIndex = 213
        '
        'lnlLbllCPTCode
        '
        Me.lnlLbllCPTCode.AutoEllipsis = True
        Me.lnlLbllCPTCode.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lnlLbllCPTCode.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lnlLbllCPTCode.ForeColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.lnlLbllCPTCode.Location = New System.Drawing.Point(1, 1)
        Me.lnlLbllCPTCode.Name = "lnlLbllCPTCode"
        Me.lnlLbllCPTCode.Padding = New System.Windows.Forms.Padding(2, 2, 0, 0)
        Me.lnlLbllCPTCode.Size = New System.Drawing.Size(172, 21)
        Me.lnlLbllCPTCode.TabIndex = 211
        '
        'Label49
        '
        Me.Label49.BackColor = System.Drawing.Color.FromArgb(CType(CType(171, Byte), Integer), CType(CType(173, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.Label49.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label49.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label49.Location = New System.Drawing.Point(173, 1)
        Me.Label49.Name = "Label49"
        Me.Label49.Size = New System.Drawing.Size(1, 21)
        Me.Label49.TabIndex = 15
        Me.Label49.Text = "label2"
        '
        'Label50
        '
        Me.Label50.BackColor = System.Drawing.Color.FromArgb(CType(CType(171, Byte), Integer), CType(CType(173, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.Label50.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label50.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label50.Location = New System.Drawing.Point(0, 1)
        Me.Label50.Name = "Label50"
        Me.Label50.Size = New System.Drawing.Size(1, 21)
        Me.Label50.TabIndex = 14
        Me.Label50.Text = "label2"
        '
        'Label51
        '
        Me.Label51.BackColor = System.Drawing.Color.FromArgb(CType(CType(171, Byte), Integer), CType(CType(173, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.Label51.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label51.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label51.Location = New System.Drawing.Point(0, 0)
        Me.Label51.Name = "Label51"
        Me.Label51.Size = New System.Drawing.Size(174, 1)
        Me.Label51.TabIndex = 13
        Me.Label51.Text = "label2"
        '
        'Label52
        '
        Me.Label52.BackColor = System.Drawing.Color.FromArgb(CType(CType(171, Byte), Integer), CType(CType(173, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.Label52.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label52.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label52.Location = New System.Drawing.Point(0, 22)
        Me.Label52.Name = "Label52"
        Me.Label52.Size = New System.Drawing.Size(174, 1)
        Me.Label52.TabIndex = 12
        Me.Label52.Text = "label2"
        '
        'Panel6
        '
        Me.Panel6.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.Panel6.Controls.Add(Me.linkLblICD9code)
        Me.Panel6.Controls.Add(Me.Label44)
        Me.Panel6.Controls.Add(Me.Label45)
        Me.Panel6.Controls.Add(Me.Label46)
        Me.Panel6.Controls.Add(Me.Label47)
        Me.Panel6.Location = New System.Drawing.Point(162, 9)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(188, 23)
        Me.Panel6.TabIndex = 213
        '
        'linkLblICD9code
        '
        Me.linkLblICD9code.AutoEllipsis = True
        Me.linkLblICD9code.BackColor = System.Drawing.Color.Transparent
        Me.linkLblICD9code.Dock = System.Windows.Forms.DockStyle.Fill
        Me.linkLblICD9code.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.linkLblICD9code.ForeColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.linkLblICD9code.Location = New System.Drawing.Point(1, 1)
        Me.linkLblICD9code.Name = "linkLblICD9code"
        Me.linkLblICD9code.Padding = New System.Windows.Forms.Padding(2, 2, 0, 0)
        Me.linkLblICD9code.Size = New System.Drawing.Size(186, 21)
        Me.linkLblICD9code.TabIndex = 210
        '
        'Label44
        '
        Me.Label44.BackColor = System.Drawing.Color.FromArgb(CType(CType(171, Byte), Integer), CType(CType(173, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.Label44.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label44.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label44.Location = New System.Drawing.Point(187, 1)
        Me.Label44.Name = "Label44"
        Me.Label44.Size = New System.Drawing.Size(1, 21)
        Me.Label44.TabIndex = 15
        Me.Label44.Text = "label2"
        '
        'Label45
        '
        Me.Label45.BackColor = System.Drawing.Color.FromArgb(CType(CType(171, Byte), Integer), CType(CType(173, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.Label45.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label45.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label45.Location = New System.Drawing.Point(0, 1)
        Me.Label45.Name = "Label45"
        Me.Label45.Size = New System.Drawing.Size(1, 21)
        Me.Label45.TabIndex = 14
        Me.Label45.Text = "label2"
        '
        'Label46
        '
        Me.Label46.BackColor = System.Drawing.Color.FromArgb(CType(CType(171, Byte), Integer), CType(CType(173, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.Label46.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label46.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label46.Location = New System.Drawing.Point(0, 0)
        Me.Label46.Name = "Label46"
        Me.Label46.Size = New System.Drawing.Size(188, 1)
        Me.Label46.TabIndex = 13
        Me.Label46.Text = "label2"
        '
        'Label47
        '
        Me.Label47.BackColor = System.Drawing.Color.FromArgb(CType(CType(171, Byte), Integer), CType(CType(173, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.Label47.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label47.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label47.Location = New System.Drawing.Point(0, 22)
        Me.Label47.Name = "Label47"
        Me.Label47.Size = New System.Drawing.Size(188, 1)
        Me.Label47.TabIndex = 12
        Me.Label47.Text = "label2"
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.Panel5.Controls.Add(Me.lblconcptid)
        Me.Panel5.Controls.Add(Me.Label42)
        Me.Panel5.Controls.Add(Me.Label41)
        Me.Panel5.Controls.Add(Me.Label40)
        Me.Panel5.Controls.Add(Me.Label38)
        Me.Panel5.Location = New System.Drawing.Point(162, 36)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(445, 23)
        Me.Panel5.TabIndex = 212
        '
        'lblconcptid
        '
        Me.lblconcptid.AutoEllipsis = True
        Me.lblconcptid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblconcptid.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblconcptid.ForeColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.lblconcptid.Location = New System.Drawing.Point(1, 1)
        Me.lblconcptid.Name = "lblconcptid"
        Me.lblconcptid.Padding = New System.Windows.Forms.Padding(2, 2, 0, 0)
        Me.lblconcptid.Size = New System.Drawing.Size(443, 21)
        Me.lblconcptid.TabIndex = 209
        '
        'Label42
        '
        Me.Label42.BackColor = System.Drawing.Color.FromArgb(CType(CType(171, Byte), Integer), CType(CType(173, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.Label42.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label42.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label42.Location = New System.Drawing.Point(444, 1)
        Me.Label42.Name = "Label42"
        Me.Label42.Size = New System.Drawing.Size(1, 21)
        Me.Label42.TabIndex = 15
        Me.Label42.Text = "label2"
        '
        'Label41
        '
        Me.Label41.BackColor = System.Drawing.Color.FromArgb(CType(CType(171, Byte), Integer), CType(CType(173, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.Label41.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label41.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label41.Location = New System.Drawing.Point(0, 1)
        Me.Label41.Name = "Label41"
        Me.Label41.Size = New System.Drawing.Size(1, 21)
        Me.Label41.TabIndex = 14
        Me.Label41.Text = "label2"
        '
        'Label40
        '
        Me.Label40.BackColor = System.Drawing.Color.FromArgb(CType(CType(171, Byte), Integer), CType(CType(173, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.Label40.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label40.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label40.Location = New System.Drawing.Point(0, 0)
        Me.Label40.Name = "Label40"
        Me.Label40.Size = New System.Drawing.Size(445, 1)
        Me.Label40.TabIndex = 13
        Me.Label40.Text = "label2"
        '
        'Label38
        '
        Me.Label38.BackColor = System.Drawing.Color.FromArgb(CType(CType(171, Byte), Integer), CType(CType(173, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.Label38.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label38.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label38.Location = New System.Drawing.Point(0, 22)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(445, 1)
        Me.Label38.TabIndex = 12
        Me.Label38.Text = "label2"
        '
        'Label39
        '
        Me.Label39.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label39.AutoSize = True
        Me.Label39.Location = New System.Drawing.Point(393, 14)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(37, 14)
        Me.Label39.TabIndex = 21
        Me.Label39.Text = "CPT :"
        '
        'Label37
        '
        Me.Label37.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label37.AutoSize = True
        Me.Label37.Location = New System.Drawing.Point(99, 12)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(60, 14)
        Me.Label37.TabIndex = 19
        Me.Label37.Text = "ICD9/10 :"
        Me.Label37.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblNDCid
        '
        Me.lblNDCid.AutoEllipsis = True
        Me.lblNDCid.BackColor = System.Drawing.Color.Transparent
        Me.lblNDCid.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNDCid.Location = New System.Drawing.Point(746, 65)
        Me.lblNDCid.Name = "lblNDCid"
        Me.lblNDCid.Size = New System.Drawing.Size(122, 14)
        Me.lblNDCid.TabIndex = 16
        '
        'Label10
        '
        Me.Label10.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(674, 65)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(70, 14)
        Me.Label10.TabIndex = 15
        Me.Label10.Text = "NDC Code :"
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label12.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label12.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label12.Location = New System.Drawing.Point(0, 3)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(706, 1)
        Me.Label12.TabIndex = 12
        Me.Label12.Text = "label2"
        '
        'Label28
        '
        Me.Label28.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label28.AutoSize = True
        Me.Label28.Location = New System.Drawing.Point(96, 39)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(63, 14)
        Me.Label28.TabIndex = 0
        Me.Label28.Text = "SNOMED :"
        Me.Label28.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lbldescid
        '
        Me.lbldescid.AutoEllipsis = True
        Me.lbldescid.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbldescid.Location = New System.Drawing.Point(145, 10)
        Me.lbldescid.Name = "lbldescid"
        Me.lbldescid.Size = New System.Drawing.Size(14, 20)
        Me.lbldescid.TabIndex = 1
        Me.lbldescid.Visible = False
        '
        'Label30
        '
        Me.Label30.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label30.AutoSize = True
        Me.Label30.Location = New System.Drawing.Point(671, 40)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(73, 14)
        Me.Label30.TabIndex = 4
        Me.Label30.Text = "RxNorm ID :"
        '
        'lblRxNorm
        '
        Me.lblRxNorm.AutoEllipsis = True
        Me.lblRxNorm.BackColor = System.Drawing.Color.Transparent
        Me.lblRxNorm.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRxNorm.Location = New System.Drawing.Point(746, 40)
        Me.lblRxNorm.Name = "lblRxNorm"
        Me.lblRxNorm.Size = New System.Drawing.Size(122, 14)
        Me.lblRxNorm.TabIndex = 5
        '
        'Panel14
        '
        Me.Panel14.Controls.Add(Me.btnShowCQM)
        Me.Panel14.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel14.Location = New System.Drawing.Point(0, 280)
        Me.Panel14.Name = "Panel14"
        Me.Panel14.Size = New System.Drawing.Size(706, 25)
        Me.Panel14.TabIndex = 250
        '
        'btnShowCQM
        '
        Me.btnShowCQM.BackColor = System.Drawing.Color.Transparent
        Me.btnShowCQM.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.btnShowCQM.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnShowCQM.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnShowCQM.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnShowCQM.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnShowCQM.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnShowCQM.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnShowCQM.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnShowCQM.Location = New System.Drawing.Point(0, 0)
        Me.btnShowCQM.Name = "btnShowCQM"
        Me.btnShowCQM.Size = New System.Drawing.Size(706, 25)
        Me.btnShowCQM.TabIndex = 235
        Me.btnShowCQM.Tag = "Expand"
        Me.btnShowCQM.Text = "Expand"
        Me.btnShowCQM.UseVisualStyleBackColor = False
        '
        'PnlCustomTask
        '
        Me.PnlCustomTask.Location = New System.Drawing.Point(105, 80)
        Me.PnlCustomTask.Name = "PnlCustomTask"
        Me.PnlCustomTask.Size = New System.Drawing.Size(215, 108)
        Me.PnlCustomTask.TabIndex = 11
        '
        'pnlreview
        '
        Me.pnlreview.BackColor = System.Drawing.Color.Transparent
        Me.pnlreview.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.pnlreview.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlreview.Controls.Add(Me.lblReviewed)
        Me.pnlreview.Controls.Add(Me.Label43)
        Me.pnlreview.Controls.Add(Me.btnUp)
        Me.pnlreview.Controls.Add(Me.Label48)
        Me.pnlreview.Controls.Add(Me.btnDown)
        Me.pnlreview.Controls.Add(Me.Label29)
        Me.pnlreview.Controls.Add(Me.cmbAllergyType)
        Me.pnlreview.Controls.Add(Me.Label27)
        Me.pnlreview.Controls.Add(Me.Label55)
        Me.pnlreview.Controls.Add(Me.Label56)
        Me.pnlreview.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlreview.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlreview.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.pnlreview.Location = New System.Drawing.Point(0, 4)
        Me.pnlreview.Name = "pnlreview"
        Me.pnlreview.Padding = New System.Windows.Forms.Padding(0, 2, 0, 2)
        Me.pnlreview.Size = New System.Drawing.Size(706, 29)
        Me.pnlreview.TabIndex = 6
        '
        'lblReviewed
        '
        Me.lblReviewed.AutoEllipsis = True
        Me.lblReviewed.BackColor = System.Drawing.Color.Transparent
        Me.lblReviewed.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblReviewed.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblReviewed.ImageAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.lblReviewed.Location = New System.Drawing.Point(1, 2)
        Me.lblReviewed.Name = "lblReviewed"
        Me.lblReviewed.Padding = New System.Windows.Forms.Padding(0, 4, 0, 0)
        Me.lblReviewed.Size = New System.Drawing.Size(198, 24)
        Me.lblReviewed.TabIndex = 17
        Me.lblReviewed.Text = "  Reviewed "
        Me.lblReviewed.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label43
        '
        Me.Label43.AutoSize = True
        Me.Label43.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label43.Location = New System.Drawing.Point(199, 2)
        Me.Label43.Name = "Label43"
        Me.Label43.Padding = New System.Windows.Forms.Padding(0, 5, 0, 0)
        Me.Label43.Size = New System.Drawing.Size(70, 19)
        Me.Label43.TabIndex = 210
        Me.Label43.Text = "Move Up :"
        '
        'btnUp
        '
        Me.btnUp.BackColor = System.Drawing.Color.Transparent
        Me.btnUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnUp.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnUp.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnUp.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnUp.FlatAppearance.BorderSize = 0
        Me.btnUp.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnUp.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnUp.Image = CType(resources.GetObject("btnUp.Image"), System.Drawing.Image)
        Me.btnUp.Location = New System.Drawing.Point(269, 2)
        Me.btnUp.Name = "btnUp"
        Me.btnUp.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.btnUp.Size = New System.Drawing.Size(25, 24)
        Me.btnUp.TabIndex = 208
        Me.btnUp.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnUp.UseVisualStyleBackColor = False
        Me.btnUp.Visible = False
        '
        'Label48
        '
        Me.Label48.AutoSize = True
        Me.Label48.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label48.Location = New System.Drawing.Point(294, 2)
        Me.Label48.Name = "Label48"
        Me.Label48.Padding = New System.Windows.Forms.Padding(0, 5, 0, 0)
        Me.Label48.Size = New System.Drawing.Size(89, 19)
        Me.Label48.TabIndex = 211
        Me.Label48.Text = "Move Down :"
        '
        'btnDown
        '
        Me.btnDown.BackColor = System.Drawing.Color.Transparent
        Me.btnDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnDown.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnDown.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnDown.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnDown.FlatAppearance.BorderSize = 0
        Me.btnDown.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnDown.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDown.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDown.Image = CType(resources.GetObject("btnDown.Image"), System.Drawing.Image)
        Me.btnDown.Location = New System.Drawing.Point(383, 2)
        Me.btnDown.Name = "btnDown"
        Me.btnDown.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.btnDown.Size = New System.Drawing.Size(25, 24)
        Me.btnDown.TabIndex = 209
        Me.btnDown.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnDown.UseVisualStyleBackColor = False
        Me.btnDown.Visible = False
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label29.Location = New System.Drawing.Point(408, 2)
        Me.Label29.Name = "Label29"
        Me.Label29.Padding = New System.Windows.Forms.Padding(0, 5, 0, 0)
        Me.Label29.Size = New System.Drawing.Size(169, 19)
        Me.Label29.TabIndex = 21
        Me.Label29.Text = "Allergy/Diagnosis Status :"
        '
        'cmbAllergyType
        '
        Me.cmbAllergyType.Dock = System.Windows.Forms.DockStyle.Right
        Me.cmbAllergyType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbAllergyType.FormattingEnabled = True
        Me.cmbAllergyType.Location = New System.Drawing.Point(577, 2)
        Me.cmbAllergyType.Name = "cmbAllergyType"
        Me.cmbAllergyType.Size = New System.Drawing.Size(128, 22)
        Me.cmbAllergyType.TabIndex = 22
        '
        'Label27
        '
        Me.Label27.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label27.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label27.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label27.Location = New System.Drawing.Point(1, 26)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(704, 1)
        Me.Label27.TabIndex = 18
        Me.Label27.Text = "label1"
        '
        'Label55
        '
        Me.Label55.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label55.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label55.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label55.Location = New System.Drawing.Point(0, 2)
        Me.Label55.Name = "Label55"
        Me.Label55.Size = New System.Drawing.Size(1, 25)
        Me.Label55.TabIndex = 212
        Me.Label55.Text = "label4"
        '
        'Label56
        '
        Me.Label56.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label56.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label56.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label56.Location = New System.Drawing.Point(705, 2)
        Me.Label56.Name = "Label56"
        Me.Label56.Size = New System.Drawing.Size(1, 25)
        Me.Label56.TabIndex = 213
        Me.Label56.Text = "label3"
        '
        'Label21
        '
        Me.Label21.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label21.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label21.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.Location = New System.Drawing.Point(0, 3)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(706, 1)
        Me.Label21.TabIndex = 7
        Me.Label21.Text = "label1"
        '
        'pnlPatientHeader
        '
        Me.pnlPatientHeader.BackColor = System.Drawing.Color.Transparent
        Me.pnlPatientHeader.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlPatientHeader.Controls.Add(Me.Label14)
        Me.pnlPatientHeader.Controls.Add(Me.Label15)
        Me.pnlPatientHeader.Controls.Add(Me.Label16)
        Me.pnlPatientHeader.Controls.Add(Me.Label17)
        Me.pnlPatientHeader.Controls.Add(Me.lblVisitDate)
        Me.pnlPatientHeader.Controls.Add(Me.btnPrevHistory)
        Me.pnlPatientHeader.Controls.Add(Me.Label4)
        Me.pnlPatientHeader.Controls.Add(Me.lblVisitID)
        Me.pnlPatientHeader.Controls.Add(Me.lblPatientName)
        Me.pnlPatientHeader.Controls.Add(Me.Label1)
        Me.pnlPatientHeader.Controls.Add(Me.lblPatientCode)
        Me.pnlPatientHeader.Controls.Add(Me.lblPatient)
        Me.pnlPatientHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlPatientHeader.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlPatientHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlPatientHeader.Name = "pnlPatientHeader"
        Me.pnlPatientHeader.Padding = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.pnlPatientHeader.Size = New System.Drawing.Size(706, 53)
        Me.pnlPatientHeader.TabIndex = 23
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label14.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label14.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label14.Location = New System.Drawing.Point(1, 49)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(704, 1)
        Me.Label14.TabIndex = 20
        Me.Label14.Text = "label2"
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label15.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label15.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(0, 4)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(1, 46)
        Me.Label15.TabIndex = 19
        Me.Label15.Text = "label4"
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label16.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label16.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label16.Location = New System.Drawing.Point(705, 4)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(1, 46)
        Me.Label16.TabIndex = 18
        Me.Label16.Text = "label3"
        '
        'Label17
        '
        Me.Label17.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label17.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label17.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(0, 3)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(706, 1)
        Me.Label17.TabIndex = 17
        Me.Label17.Text = "label1"
        '
        'lblVisitDate
        '
        Me.lblVisitDate.AutoSize = True
        Me.lblVisitDate.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVisitDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblVisitDate.Location = New System.Drawing.Point(270, 9)
        Me.lblVisitDate.Name = "lblVisitDate"
        Me.lblVisitDate.Size = New System.Drawing.Size(73, 14)
        Me.lblVisitDate.TabIndex = 13
        Me.lblVisitDate.Text = "08/29/2005"
        Me.lblVisitDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnPrevHistory
        '
        Me.btnPrevHistory.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnPrevHistory.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.btnPrevHistory.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnPrevHistory.FlatAppearance.BorderSize = 0
        Me.btnPrevHistory.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.btnPrevHistory.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.btnPrevHistory.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPrevHistory.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrevHistory.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnPrevHistory.Location = New System.Drawing.Point(600, 23)
        Me.btnPrevHistory.Name = "btnPrevHistory"
        Me.btnPrevHistory.Size = New System.Drawing.Size(95, 23)
        Me.btnPrevHistory.TabIndex = 16
        Me.btnPrevHistory.Text = "Prev History"
        Me.btnPrevHistory.Visible = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Location = New System.Drawing.Point(200, 9)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(67, 14)
        Me.Label4.TabIndex = 14
        Me.Label4.Text = "Visit Date :"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblVisitID
        '
        Me.lblVisitID.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblVisitID.AutoSize = True
        Me.lblVisitID.BackColor = System.Drawing.Color.Transparent
        Me.lblVisitID.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVisitID.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblVisitID.Location = New System.Drawing.Point(597, 9)
        Me.lblVisitID.Name = "lblVisitID"
        Me.lblVisitID.Size = New System.Drawing.Size(14, 14)
        Me.lblVisitID.TabIndex = 12
        Me.lblVisitID.Text = "1"
        Me.lblVisitID.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblVisitID.Visible = False
        '
        'lblPatientName
        '
        Me.lblPatientName.AutoSize = True
        Me.lblPatientName.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPatientName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblPatientName.Location = New System.Drawing.Point(99, 29)
        Me.lblPatientName.Name = "lblPatientName"
        Me.lblPatientName.Size = New System.Drawing.Size(71, 14)
        Me.lblPatientName.TabIndex = 11
        Me.lblPatientName.Text = "Mike Dodge"
        Me.lblPatientName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(9, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(86, 14)
        Me.Label1.TabIndex = 10
        Me.Label1.Text = "Patient Code :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblPatientCode
        '
        Me.lblPatientCode.AutoSize = True
        Me.lblPatientCode.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPatientCode.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblPatientCode.Location = New System.Drawing.Point(99, 9)
        Me.lblPatientCode.Name = "lblPatientCode"
        Me.lblPatientCode.Size = New System.Drawing.Size(35, 14)
        Me.lblPatientCode.TabIndex = 9
        Me.lblPatientCode.Text = "1001"
        Me.lblPatientCode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblPatient
        '
        Me.lblPatient.AutoSize = True
        Me.lblPatient.BackColor = System.Drawing.Color.Transparent
        Me.lblPatient.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPatient.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblPatient.Location = New System.Drawing.Point(6, 29)
        Me.lblPatient.Name = "lblPatient"
        Me.lblPatient.Size = New System.Drawing.Size(89, 14)
        Me.lblPatient.TabIndex = 8
        Me.lblPatient.Text = "Patient Name :"
        Me.lblPatient.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Splitter3
        '
        Me.Splitter3.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Splitter3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Splitter3.Location = New System.Drawing.Point(0, 649)
        Me.Splitter3.Name = "Splitter3"
        Me.Splitter3.Size = New System.Drawing.Size(706, 3)
        Me.Splitter3.TabIndex = 21
        Me.Splitter3.TabStop = False
        '
        'pnlWordComp
        '
        Me.pnlWordComp.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlWordComp.Controls.Add(Me.wdNarration)
        Me.pnlWordComp.Controls.Add(Me.Panel1)
        Me.pnlWordComp.Controls.Add(Me.Label22)
        Me.pnlWordComp.Controls.Add(Me.Label23)
        Me.pnlWordComp.Controls.Add(Me.Label24)
        Me.pnlWordComp.Controls.Add(Me.Label25)
        Me.pnlWordComp.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlWordComp.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlWordComp.Location = New System.Drawing.Point(0, 652)
        Me.pnlWordComp.Name = "pnlWordComp"
        Me.pnlWordComp.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.pnlWordComp.Size = New System.Drawing.Size(706, 184)
        Me.pnlWordComp.TabIndex = 1
        Me.pnlWordComp.Visible = False
        '
        'wdNarration
        '
        Me.wdNarration.BackColor = System.Drawing.Color.White
        Me.wdNarration.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.wdNarration.Dock = System.Windows.Forms.DockStyle.Fill
        Me.wdNarration.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.wdNarration.ForeColor = System.Drawing.Color.Black
        Me.wdNarration.Location = New System.Drawing.Point(1, 25)
        Me.wdNarration.MaxLength = 5000
        Me.wdNarration.Name = "wdNarration"
        Me.wdNarration.Size = New System.Drawing.Size(704, 155)
        Me.wdNarration.TabIndex = 0
        Me.wdNarration.Text = ""
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Transparent
        Me.Panel1.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel1.Controls.Add(Me.Label26)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel1.Location = New System.Drawing.Point(1, 1)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(704, 24)
        Me.Panel1.TabIndex = 4
        '
        'Label26
        '
        Me.Label26.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label26.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label26.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.Location = New System.Drawing.Point(0, 23)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(704, 1)
        Me.Label26.TabIndex = 6
        Me.Label26.Text = "label1"
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Location = New System.Drawing.Point(0, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(704, 24)
        Me.Label5.TabIndex = 0
        Me.Label5.Text = "  History Narrative"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label22
        '
        Me.Label22.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label22.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label22.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label22.Location = New System.Drawing.Point(1, 180)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(704, 1)
        Me.Label22.TabIndex = 8
        Me.Label22.Text = "label2"
        '
        'Label23
        '
        Me.Label23.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label23.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label23.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.Location = New System.Drawing.Point(0, 1)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(1, 180)
        Me.Label23.TabIndex = 7
        Me.Label23.Text = "label4"
        '
        'Label24
        '
        Me.Label24.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label24.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label24.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label24.Location = New System.Drawing.Point(705, 1)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(1, 180)
        Me.Label24.TabIndex = 6
        Me.Label24.Text = "label3"
        '
        'Label25
        '
        Me.Label25.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label25.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label25.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.Location = New System.Drawing.Point(0, 0)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(706, 1)
        Me.Label25.TabIndex = 5
        Me.Label25.Text = "label1"
        '
        'Splitter2
        '
        Me.Splitter2.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Splitter2.Dock = System.Windows.Forms.DockStyle.Right
        Me.Splitter2.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Splitter2.Location = New System.Drawing.Point(950, 0)
        Me.Splitter2.Name = "Splitter2"
        Me.Splitter2.Size = New System.Drawing.Size(4, 836)
        Me.Splitter2.TabIndex = 14
        Me.Splitter2.TabStop = False
        '
        'pnlPrevHistory
        '
        Me.pnlPrevHistory.Controls.Add(Me.pnl_Base)
        Me.pnlPrevHistory.Controls.Add(Me.pnlPrevSearch)
        Me.pnlPrevHistory.Controls.Add(Me.pnlSearch)
        Me.pnlPrevHistory.Dock = System.Windows.Forms.DockStyle.Right
        Me.pnlPrevHistory.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlPrevHistory.Location = New System.Drawing.Point(954, 0)
        Me.pnlPrevHistory.Name = "pnlPrevHistory"
        Me.pnlPrevHistory.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.pnlPrevHistory.Size = New System.Drawing.Size(230, 836)
        Me.pnlPrevHistory.TabIndex = 2
        '
        'pnl_Base
        '
        Me.pnl_Base.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnl_Base.Controls.Add(Me.trvPrevHistory)
        Me.pnl_Base.Controls.Add(Me.Label34)
        Me.pnl_Base.Controls.Add(Me.Label33)
        Me.pnl_Base.Controls.Add(Me.lbl_BottomBrd)
        Me.pnl_Base.Controls.Add(Me.lbl_LeftBrd)
        Me.pnl_Base.Controls.Add(Me.lbl_RightBrd)
        Me.pnl_Base.Controls.Add(Me.lbl_TopBrd)
        Me.pnl_Base.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnl_Base.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnl_Base.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.pnl_Base.Location = New System.Drawing.Point(0, 54)
        Me.pnl_Base.Name = "pnl_Base"
        Me.pnl_Base.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.pnl_Base.Size = New System.Drawing.Size(230, 782)
        Me.pnl_Base.TabIndex = 2
        '
        'trvPrevHistory
        '
        Me.trvPrevHistory.BackColor = System.Drawing.Color.White
        Me.trvPrevHistory.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trvPrevHistory.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trvPrevHistory.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.trvPrevHistory.ForeColor = System.Drawing.Color.Black
        Me.trvPrevHistory.HideSelection = False
        Me.trvPrevHistory.ImageIndex = 0
        Me.trvPrevHistory.ImageList = Me.ImgPatientHistory1
        Me.trvPrevHistory.Indent = 20
        Me.trvPrevHistory.ItemHeight = 20
        Me.trvPrevHistory.Location = New System.Drawing.Point(6, 9)
        Me.trvPrevHistory.Name = "trvPrevHistory"
        Me.trvPrevHistory.SelectedImageIndex = 0
        Me.trvPrevHistory.ShowLines = False
        Me.trvPrevHistory.ShowRootLines = False
        Me.trvPrevHistory.Size = New System.Drawing.Size(220, 769)
        Me.trvPrevHistory.TabIndex = 0
        '
        'ImgPatientHistory1
        '
        Me.ImgPatientHistory1.ImageStream = CType(resources.GetObject("ImgPatientHistory1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImgPatientHistory1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImgPatientHistory1.Images.SetKeyName(0, "Patient History.ico")
        Me.ImgPatientHistory1.Images.SetKeyName(1, "Bullet06.ico")
        Me.ImgPatientHistory1.Images.SetKeyName(2, "")
        Me.ImgPatientHistory1.Images.SetKeyName(3, "")
        Me.ImgPatientHistory1.Images.SetKeyName(4, "")
        Me.ImgPatientHistory1.Images.SetKeyName(5, "Current.ico")
        Me.ImgPatientHistory1.Images.SetKeyName(6, "Current_Disable.ico")
        Me.ImgPatientHistory1.Images.SetKeyName(7, "Yesterdays.ico")
        Me.ImgPatientHistory1.Images.SetKeyName(8, "Yesterdays_Disable.ico")
        Me.ImgPatientHistory1.Images.SetKeyName(9, "Last Week.ico")
        Me.ImgPatientHistory1.Images.SetKeyName(10, "Last Week_Disable.ico")
        Me.ImgPatientHistory1.Images.SetKeyName(11, "LastMonth.ico")
        Me.ImgPatientHistory1.Images.SetKeyName(12, "LastMonth_Disable.ico")
        Me.ImgPatientHistory1.Images.SetKeyName(13, "Older.ico")
        Me.ImgPatientHistory1.Images.SetKeyName(14, "Older_Disable.ico")
        Me.ImgPatientHistory1.Images.SetKeyName(15, "date.ico")
        Me.ImgPatientHistory1.Images.SetKeyName(16, "Small Arrow.ico")
        '
        'Label34
        '
        Me.Label34.BackColor = System.Drawing.Color.White
        Me.Label34.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label34.Location = New System.Drawing.Point(1, 9)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(5, 769)
        Me.Label34.TabIndex = 40
        '
        'Label33
        '
        Me.Label33.BackColor = System.Drawing.Color.White
        Me.Label33.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label33.Location = New System.Drawing.Point(1, 1)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(225, 8)
        Me.Label33.TabIndex = 39
        '
        'lbl_BottomBrd
        '
        Me.lbl_BottomBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_BottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_BottomBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_BottomBrd.Location = New System.Drawing.Point(1, 778)
        Me.lbl_BottomBrd.Name = "lbl_BottomBrd"
        Me.lbl_BottomBrd.Size = New System.Drawing.Size(225, 1)
        Me.lbl_BottomBrd.TabIndex = 4
        Me.lbl_BottomBrd.Text = "label2"
        '
        'lbl_LeftBrd
        '
        Me.lbl_LeftBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_LeftBrd.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_LeftBrd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_LeftBrd.Location = New System.Drawing.Point(0, 1)
        Me.lbl_LeftBrd.Name = "lbl_LeftBrd"
        Me.lbl_LeftBrd.Size = New System.Drawing.Size(1, 778)
        Me.lbl_LeftBrd.TabIndex = 3
        Me.lbl_LeftBrd.Text = "label4"
        '
        'lbl_RightBrd
        '
        Me.lbl_RightBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_RightBrd.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_RightBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_RightBrd.Location = New System.Drawing.Point(226, 1)
        Me.lbl_RightBrd.Name = "lbl_RightBrd"
        Me.lbl_RightBrd.Size = New System.Drawing.Size(1, 778)
        Me.lbl_RightBrd.TabIndex = 2
        Me.lbl_RightBrd.Text = "label3"
        '
        'lbl_TopBrd
        '
        Me.lbl_TopBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_TopBrd.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_TopBrd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_TopBrd.Location = New System.Drawing.Point(0, 0)
        Me.lbl_TopBrd.Name = "lbl_TopBrd"
        Me.lbl_TopBrd.Size = New System.Drawing.Size(227, 1)
        Me.lbl_TopBrd.TabIndex = 0
        Me.lbl_TopBrd.Text = "label1"
        '
        'pnlPrevSearch
        '
        Me.pnlPrevSearch.BackColor = System.Drawing.Color.Transparent
        Me.pnlPrevSearch.Controls.Add(Me.cmbsearchHistory)
        Me.pnlPrevSearch.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlPrevSearch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlPrevSearch.Location = New System.Drawing.Point(0, 29)
        Me.pnlPrevSearch.Name = "pnlPrevSearch"
        Me.pnlPrevSearch.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.pnlPrevSearch.Size = New System.Drawing.Size(230, 25)
        Me.pnlPrevSearch.TabIndex = 1
        '
        'cmbsearchHistory
        '
        Me.cmbsearchHistory.Dock = System.Windows.Forms.DockStyle.Top
        Me.cmbsearchHistory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbsearchHistory.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbsearchHistory.ForeColor = System.Drawing.Color.Black
        Me.cmbsearchHistory.Location = New System.Drawing.Point(0, 0)
        Me.cmbsearchHistory.Name = "cmbsearchHistory"
        Me.cmbsearchHistory.Size = New System.Drawing.Size(227, 22)
        Me.cmbsearchHistory.TabIndex = 0
        '
        'pnlSearch
        '
        Me.pnlSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlSearch.Controls.Add(Me.txtsearchhistory)
        Me.pnlSearch.Controls.Add(Me.Label3)
        Me.pnlSearch.Controls.Add(Me.Label2)
        Me.pnlSearch.Controls.Add(Me.btnClearSearchHistory)
        Me.pnlSearch.Controls.Add(Me.PictureBox1)
        Me.pnlSearch.Controls.Add(Me.Label6)
        Me.pnlSearch.Controls.Add(Me.Label7)
        Me.pnlSearch.Controls.Add(Me.Label8)
        Me.pnlSearch.Controls.Add(Me.Label9)
        Me.pnlSearch.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlSearch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlSearch.ForeColor = System.Drawing.Color.Black
        Me.pnlSearch.Location = New System.Drawing.Point(0, 3)
        Me.pnlSearch.Name = "pnlSearch"
        Me.pnlSearch.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.pnlSearch.Size = New System.Drawing.Size(230, 26)
        Me.pnlSearch.TabIndex = 0
        '
        'txtsearchhistory
        '
        Me.txtsearchhistory.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtsearchhistory.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtsearchhistory.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtsearchhistory.ForeColor = System.Drawing.Color.Black
        Me.txtsearchhistory.Location = New System.Drawing.Point(29, 5)
        Me.txtsearchhistory.Name = "txtsearchhistory"
        Me.txtsearchhistory.Size = New System.Drawing.Size(176, 15)
        Me.txtsearchhistory.TabIndex = 0
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.White
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label3.Location = New System.Drawing.Point(29, 20)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(176, 2)
        Me.Label3.TabIndex = 38
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.White
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label2.Location = New System.Drawing.Point(29, 1)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(176, 4)
        Me.Label2.TabIndex = 37
        '
        'btnClearSearchHistory
        '
        Me.btnClearSearchHistory.BackgroundImage = CType(resources.GetObject("btnClearSearchHistory.BackgroundImage"), System.Drawing.Image)
        Me.btnClearSearchHistory.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClearSearchHistory.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnClearSearchHistory.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnClearSearchHistory.FlatAppearance.BorderSize = 0
        Me.btnClearSearchHistory.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnClearSearchHistory.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnClearSearchHistory.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClearSearchHistory.Image = CType(resources.GetObject("btnClearSearchHistory.Image"), System.Drawing.Image)
        Me.btnClearSearchHistory.Location = New System.Drawing.Point(205, 1)
        Me.btnClearSearchHistory.Name = "btnClearSearchHistory"
        Me.btnClearSearchHistory.Size = New System.Drawing.Size(21, 21)
        Me.btnClearSearchHistory.TabIndex = 42
        Me.btnClearSearchHistory.UseVisualStyleBackColor = True
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
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label6.Location = New System.Drawing.Point(1, 22)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(225, 1)
        Me.Label6.TabIndex = 35
        Me.Label6.Text = "label1"
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label7.Location = New System.Drawing.Point(1, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(225, 1)
        Me.Label7.TabIndex = 36
        Me.Label7.Text = "label1"
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label8.Location = New System.Drawing.Point(0, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(1, 23)
        Me.Label8.TabIndex = 39
        Me.Label8.Text = "label4"
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label9.Location = New System.Drawing.Point(226, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(1, 23)
        Me.Label9.TabIndex = 40
        Me.Label9.Text = "label4"
        '
        'Splitter1
        '
        Me.Splitter1.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Splitter1.Location = New System.Drawing.Point(240, 0)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(4, 836)
        Me.Splitter1.TabIndex = 1
        Me.Splitter1.TabStop = False
        '
        'pnlCatBtn
        '
        Me.pnlCatBtn.AutoScroll = True
        Me.pnlCatBtn.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlCatBtn.Controls.Add(Me.pnltrvSource)
        Me.pnlCatBtn.Controls.Add(Me.pnlSnoMedtrvSource)
        Me.pnlCatBtn.Controls.Add(Me.pnlBottomButtons)
        Me.pnlCatBtn.Controls.Add(Me.pnlTopButtons)
        Me.pnlCatBtn.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlCatBtn.Location = New System.Drawing.Point(0, 0)
        Me.pnlCatBtn.Name = "pnlCatBtn"
        Me.pnlCatBtn.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.pnlCatBtn.Size = New System.Drawing.Size(240, 836)
        Me.pnlCatBtn.TabIndex = 0
        '
        'pnltrvSource
        '
        Me.pnltrvSource.BackColor = System.Drawing.Color.Transparent
        Me.pnltrvSource.Controls.Add(Me.Splitter7)
        Me.pnltrvSource.Controls.Add(Me.Splitter8)
        Me.pnltrvSource.Controls.Add(Me.GloUC_trvHistory)
        Me.pnltrvSource.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnltrvSource.Location = New System.Drawing.Point(0, 31)
        Me.pnltrvSource.Name = "pnltrvSource"
        Me.pnltrvSource.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.pnltrvSource.Size = New System.Drawing.Size(240, 665)
        Me.pnltrvSource.TabIndex = 18
        '
        'Splitter7
        '
        Me.Splitter7.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Splitter7.Dock = System.Windows.Forms.DockStyle.Top
        Me.Splitter7.Location = New System.Drawing.Point(3, 0)
        Me.Splitter7.Name = "Splitter7"
        Me.Splitter7.Size = New System.Drawing.Size(237, 3)
        Me.Splitter7.TabIndex = 58
        Me.Splitter7.TabStop = False
        '
        'Splitter8
        '
        Me.Splitter8.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Splitter8.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Splitter8.Location = New System.Drawing.Point(3, 662)
        Me.Splitter8.Name = "Splitter8"
        Me.Splitter8.Size = New System.Drawing.Size(237, 3)
        Me.Splitter8.TabIndex = 60
        Me.Splitter8.TabStop = False
        '
        'GloUC_trvHistory
        '
        Me.GloUC_trvHistory.AllergyClassID = Nothing
        Me.GloUC_trvHistory.BackColor = System.Drawing.Color.White
        Me.GloUC_trvHistory.CheckBoxes = False
        Me.GloUC_trvHistory.CodeMember = Nothing
        Me.GloUC_trvHistory.ColonAsSeparator = False
        Me.GloUC_trvHistory.Comment = Nothing
        Me.GloUC_trvHistory.ConceptID = Nothing
        Me.GloUC_trvHistory.CPT = Nothing
        Me.GloUC_trvHistory.CQMDESC = Nothing
        Me.GloUC_trvHistory.CQMID = Nothing
        Me.GloUC_trvHistory.DDIDMember = Nothing
        Me.GloUC_trvHistory.DescriptionMember = Nothing
        Me.GloUC_trvHistory.DisplayContextMenuStrip = Nothing
        Me.GloUC_trvHistory.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Code_Description
        Me.GloUC_trvHistory.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GloUC_trvHistory.DrugFlag = CType(16, Short)
        Me.GloUC_trvHistory.DrugFormMember = Nothing
        Me.GloUC_trvHistory.DrugQtyQualifierMember = Nothing
        Me.GloUC_trvHistory.DurationMember = Nothing
        Me.GloUC_trvHistory.EducationMappingSearchType = 1
        Me.GloUC_trvHistory.FrequencyMember = Nothing
        Me.GloUC_trvHistory.HistoryType = Nothing
        Me.GloUC_trvHistory.ICD9 = Nothing
        Me.GloUC_trvHistory.ICDRevision = Nothing
        Me.GloUC_trvHistory.ImageIndex = 4
        Me.GloUC_trvHistory.ImageList = Me.imgTreeVIew
        Me.GloUC_trvHistory.ImageObject = Nothing
        Me.GloUC_trvHistory.Indicator = Nothing
        Me.GloUC_trvHistory.IsCPTSearch = False
        Me.GloUC_trvHistory.IsDiagnosisSearch = False
        Me.GloUC_trvHistory.IsDrug = False
        Me.GloUC_trvHistory.IsNarcoticsMember = Nothing
        Me.GloUC_trvHistory.IsSearchForEducationMapping = False
        Me.GloUC_trvHistory.IsSystemCategory = Nothing
        Me.GloUC_trvHistory.Location = New System.Drawing.Point(3, 0)
        Me.GloUC_trvHistory.MaximumNodes = 1100
        Me.GloUC_trvHistory.mpidmember = Nothing
        Me.GloUC_trvHistory.Name = "GloUC_trvHistory"
        Me.GloUC_trvHistory.NDCCodeMember = Nothing
        Me.GloUC_trvHistory.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.GloUC_trvHistory.ParentImageIndex = 0
        Me.GloUC_trvHistory.ParentMember = Nothing
        Me.GloUC_trvHistory.RouteMember = Nothing
        Me.GloUC_trvHistory.RowOrderMember = Nothing
        Me.GloUC_trvHistory.RxNormCode = Nothing
        Me.GloUC_trvHistory.Search = gloUserControlLibrary.gloUC_TreeView.enumSearchType.Instring
        Me.GloUC_trvHistory.SearchBox = True
        Me.GloUC_trvHistory.SearchText = Nothing
        Me.GloUC_trvHistory.SelectedImageIndex = 4
        Me.GloUC_trvHistory.SelectedNode = Nothing
        Me.GloUC_trvHistory.SelectedNodeIDs = CType(resources.GetObject("GloUC_trvHistory.SelectedNodeIDs"), System.Collections.ArrayList)
        Me.GloUC_trvHistory.SelectedParentImageIndex = 0
        Me.GloUC_trvHistory.Size = New System.Drawing.Size(237, 665)
        Me.GloUC_trvHistory.SmartTreatmentId = Nothing
        Me.GloUC_trvHistory.Sort = gloUserControlLibrary.gloUC_TreeView.enumSortType.ByDescription
        Me.GloUC_trvHistory.TabIndex = 49
        Me.GloUC_trvHistory.Tag = Nothing
        Me.GloUC_trvHistory.UnitMember = Nothing
        Me.GloUC_trvHistory.ValueMember = Nothing
        '
        'pnlSnoMedtrvSource
        '
        Me.pnlSnoMedtrvSource.BackColor = System.Drawing.Color.Transparent
        Me.pnlSnoMedtrvSource.Controls.Add(Me.pnlSMSearch)
        Me.pnlSnoMedtrvSource.Controls.Add(Me.pnlSubtype)
        Me.pnlSnoMedtrvSource.Controls.Add(Me.Splitter4)
        Me.pnlSnoMedtrvSource.Controls.Add(Me.pnlSMFindings)
        Me.pnlSnoMedtrvSource.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlSnoMedtrvSource.Location = New System.Drawing.Point(0, 31)
        Me.pnlSnoMedtrvSource.Name = "pnlSnoMedtrvSource"
        Me.pnlSnoMedtrvSource.Padding = New System.Windows.Forms.Padding(3, 3, 0, 0)
        Me.pnlSnoMedtrvSource.Size = New System.Drawing.Size(240, 665)
        Me.pnlSnoMedtrvSource.TabIndex = 53
        '
        'pnlSMSearch
        '
        Me.pnlSMSearch.BackColor = System.Drawing.Color.Transparent
        Me.pnlSMSearch.Controls.Add(Me.txtSMSearch)
        Me.pnlSMSearch.Controls.Add(Me.lbl_WhiteSpaceTop)
        Me.pnlSMSearch.Controls.Add(Me.lbl_WhiteSpaceBottom)
        Me.pnlSMSearch.Controls.Add(Me.btnClear)
        Me.pnlSMSearch.Controls.Add(Me.PicBx_Search)
        Me.pnlSMSearch.Controls.Add(Me.lbl_pnlSearchBottomBrd)
        Me.pnlSMSearch.Controls.Add(Me.lbl_pnlSearchTopBrd)
        Me.pnlSMSearch.Controls.Add(Me.lbl_pnlSearchLeftBrd)
        Me.pnlSMSearch.Controls.Add(Me.lbl_pnlSearchRightBrd)
        Me.pnlSMSearch.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlSMSearch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlSMSearch.ForeColor = System.Drawing.Color.Black
        Me.pnlSMSearch.Location = New System.Drawing.Point(3, 195)
        Me.pnlSMSearch.Name = "pnlSMSearch"
        Me.pnlSMSearch.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.pnlSMSearch.Size = New System.Drawing.Size(237, 26)
        Me.pnlSMSearch.TabIndex = 58
        '
        'txtSMSearch
        '
        Me.txtSMSearch.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtSMSearch.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtSMSearch.Location = New System.Drawing.Point(29, 4)
        Me.txtSMSearch.Name = "txtSMSearch"
        Me.txtSMSearch.Size = New System.Drawing.Size(186, 15)
        Me.txtSMSearch.TabIndex = 0
        '
        'lbl_WhiteSpaceTop
        '
        Me.lbl_WhiteSpaceTop.BackColor = System.Drawing.Color.White
        Me.lbl_WhiteSpaceTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_WhiteSpaceTop.Location = New System.Drawing.Point(29, 1)
        Me.lbl_WhiteSpaceTop.Name = "lbl_WhiteSpaceTop"
        Me.lbl_WhiteSpaceTop.Size = New System.Drawing.Size(186, 3)
        Me.lbl_WhiteSpaceTop.TabIndex = 37
        '
        'lbl_WhiteSpaceBottom
        '
        Me.lbl_WhiteSpaceBottom.BackColor = System.Drawing.Color.White
        Me.lbl_WhiteSpaceBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_WhiteSpaceBottom.Location = New System.Drawing.Point(29, 17)
        Me.lbl_WhiteSpaceBottom.Name = "lbl_WhiteSpaceBottom"
        Me.lbl_WhiteSpaceBottom.Size = New System.Drawing.Size(186, 5)
        Me.lbl_WhiteSpaceBottom.TabIndex = 38
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
        Me.btnClear.Location = New System.Drawing.Point(215, 1)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(21, 21)
        Me.btnClear.TabIndex = 41
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'PicBx_Search
        '
        Me.PicBx_Search.BackColor = System.Drawing.Color.White
        Me.PicBx_Search.Dock = System.Windows.Forms.DockStyle.Left
        Me.PicBx_Search.Image = CType(resources.GetObject("PicBx_Search.Image"), System.Drawing.Image)
        Me.PicBx_Search.Location = New System.Drawing.Point(1, 1)
        Me.PicBx_Search.Name = "PicBx_Search"
        Me.PicBx_Search.Size = New System.Drawing.Size(28, 21)
        Me.PicBx_Search.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PicBx_Search.TabIndex = 9
        Me.PicBx_Search.TabStop = False
        '
        'lbl_pnlSearchBottomBrd
        '
        Me.lbl_pnlSearchBottomBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlSearchBottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_pnlSearchBottomBrd.Location = New System.Drawing.Point(1, 22)
        Me.lbl_pnlSearchBottomBrd.Name = "lbl_pnlSearchBottomBrd"
        Me.lbl_pnlSearchBottomBrd.Size = New System.Drawing.Size(235, 1)
        Me.lbl_pnlSearchBottomBrd.TabIndex = 35
        Me.lbl_pnlSearchBottomBrd.Text = "label1"
        '
        'lbl_pnlSearchTopBrd
        '
        Me.lbl_pnlSearchTopBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlSearchTopBrd.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_pnlSearchTopBrd.Location = New System.Drawing.Point(1, 0)
        Me.lbl_pnlSearchTopBrd.Name = "lbl_pnlSearchTopBrd"
        Me.lbl_pnlSearchTopBrd.Size = New System.Drawing.Size(235, 1)
        Me.lbl_pnlSearchTopBrd.TabIndex = 36
        Me.lbl_pnlSearchTopBrd.Text = "label1"
        '
        'lbl_pnlSearchLeftBrd
        '
        Me.lbl_pnlSearchLeftBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlSearchLeftBrd.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_pnlSearchLeftBrd.Location = New System.Drawing.Point(0, 0)
        Me.lbl_pnlSearchLeftBrd.Name = "lbl_pnlSearchLeftBrd"
        Me.lbl_pnlSearchLeftBrd.Size = New System.Drawing.Size(1, 23)
        Me.lbl_pnlSearchLeftBrd.TabIndex = 39
        Me.lbl_pnlSearchLeftBrd.Text = "label4"
        '
        'lbl_pnlSearchRightBrd
        '
        Me.lbl_pnlSearchRightBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlSearchRightBrd.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_pnlSearchRightBrd.Location = New System.Drawing.Point(236, 0)
        Me.lbl_pnlSearchRightBrd.Name = "lbl_pnlSearchRightBrd"
        Me.lbl_pnlSearchRightBrd.Size = New System.Drawing.Size(1, 23)
        Me.lbl_pnlSearchRightBrd.TabIndex = 40
        Me.lbl_pnlSearchRightBrd.Text = "label4"
        '
        'pnlSubtype
        '
        Me.pnlSubtype.BackColor = System.Drawing.Color.Transparent
        Me.pnlSubtype.Controls.Add(Me.trvSubType)
        Me.pnlSubtype.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlSubtype.Location = New System.Drawing.Point(3, 195)
        Me.pnlSubtype.Name = "pnlSubtype"
        Me.pnlSubtype.Size = New System.Drawing.Size(237, 470)
        Me.pnlSubtype.TabIndex = 55
        '
        'trvSubType
        '
        Me.trvSubType.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trvSubType.Indent = 20
        Me.trvSubType.ItemHeight = 20
        Me.trvSubType.Location = New System.Drawing.Point(0, 0)
        Me.trvSubType.Name = "trvSubType"
        Me.trvSubType.Size = New System.Drawing.Size(237, 470)
        Me.trvSubType.TabIndex = 58
        '
        'Splitter4
        '
        Me.Splitter4.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Splitter4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Splitter4.Location = New System.Drawing.Point(3, 191)
        Me.Splitter4.Name = "Splitter4"
        Me.Splitter4.Size = New System.Drawing.Size(237, 4)
        Me.Splitter4.TabIndex = 57
        Me.Splitter4.TabStop = False
        '
        'pnlSMFindings
        '
        Me.pnlSMFindings.BackColor = System.Drawing.Color.Transparent
        Me.pnlSMFindings.Controls.Add(Me.trvFindings)
        Me.pnlSMFindings.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlSMFindings.Location = New System.Drawing.Point(3, 3)
        Me.pnlSMFindings.Name = "pnlSMFindings"
        Me.pnlSMFindings.Size = New System.Drawing.Size(237, 188)
        Me.pnlSMFindings.TabIndex = 54
        '
        'trvFindings
        '
        Me.trvFindings.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trvFindings.Indent = 20
        Me.trvFindings.ItemHeight = 20
        Me.trvFindings.Location = New System.Drawing.Point(0, 0)
        Me.trvFindings.Name = "trvFindings"
        Me.trvFindings.Size = New System.Drawing.Size(237, 188)
        Me.trvFindings.TabIndex = 0
        '
        'pnlBottomButtons
        '
        Me.pnlBottomButtons.AutoScroll = True
        Me.pnlBottomButtons.Controls.Add(Me.lblHeightMeter)
        Me.pnlBottomButtons.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlBottomButtons.Location = New System.Drawing.Point(0, 696)
        Me.pnlBottomButtons.Name = "pnlBottomButtons"
        Me.pnlBottomButtons.Padding = New System.Windows.Forms.Padding(3, 0, 0, 3)
        Me.pnlBottomButtons.Size = New System.Drawing.Size(240, 140)
        Me.pnlBottomButtons.TabIndex = 50
        '
        'lblHeightMeter
        '
        Me.lblHeightMeter.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblHeightMeter.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeightMeter.Location = New System.Drawing.Point(105, 2)
        Me.lblHeightMeter.Name = "lblHeightMeter"
        Me.lblHeightMeter.Size = New System.Drawing.Size(0, 17)
        Me.lblHeightMeter.TabIndex = 8
        Me.lblHeightMeter.Text = "label4"
        '
        'pnlTopButtons
        '
        Me.pnlTopButtons.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlTopButtons.Location = New System.Drawing.Point(0, 3)
        Me.pnlTopButtons.Name = "pnlTopButtons"
        Me.pnlTopButtons.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.pnlTopButtons.Size = New System.Drawing.Size(240, 28)
        Me.pnlTopButtons.TabIndex = 51
        '
        'pnlToolStrip
        '
        Me.pnlToolStrip.AutoSize = True
        Me.pnlToolStrip.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlToolStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlToolStrip.Controls.Add(Me.pnlDI)
        Me.pnlToolStrip.Controls.Add(Me.tblHistory)
        Me.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlToolStrip.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.pnlToolStrip.Name = "pnlToolStrip"
        Me.pnlToolStrip.Size = New System.Drawing.Size(1184, 56)
        Me.pnlToolStrip.TabIndex = 0
        '
        'pnlDI
        '
        Me.pnlDI.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlDI.BackColor = System.Drawing.Color.Transparent
        Me.pnlDI.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlDI.Location = New System.Drawing.Point(795, 0)
        Me.pnlDI.Name = "pnlDI"
        Me.pnlDI.Size = New System.Drawing.Size(66, 53)
        Me.pnlDI.TabIndex = 3
        Me.pnlDI.Visible = False
        '
        'tblHistory
        '
        Me.tblHistory.AddSeparatorsBetweenEachButton = False
        Me.tblHistory.BackColor = System.Drawing.Color.Transparent
        Me.tblHistory.BackgroundImage = CType(resources.GetObject("tblHistory.BackgroundImage"), System.Drawing.Image)
        Me.tblHistory.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tblHistory.ButtonsToHide = CType(resources.GetObject("tblHistory.ButtonsToHide"), System.Collections.ArrayList)
        Me.tblHistory.ConnectionString = Nothing
        Me.tblHistory.CustomizeButtonNameType = gloToolStrip.gloToolStrip.enumButtonNameType.ShowToolTipText
        Me.tblHistory.FinishTemplate = False
        Me.tblHistory.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblHistory.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.tblHistory.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tblHistory.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tblNew, Me.tblShowNarrative, Me.tblReviewofHistory, Me.tlbViewReview, Me.tblHistoryofHistory, Me.tblShow, Me.tblCCD, Me.tlbbtn_Reconcile, Me.tblPastPregnancies, Me.tblHistoryReconcillation, Me.tblRecHistory, Me.tblNKAllergies, Me.tblSave, Me.tblClose})
        Me.tblHistory.Location = New System.Drawing.Point(0, 0)
        Me.tblHistory.ModuleName = Nothing
        Me.tblHistory.Name = "tblHistory"
        Me.tblHistory.Size = New System.Drawing.Size(1184, 53)
        Me.tblHistory.TabIndex = 1
        Me.tblHistory.Text = "ToolStrip1"
        Me.tblHistory.UserID = CType(0, Long)
        '
        'tblNew
        '
        Me.tblNew.BackColor = System.Drawing.Color.Transparent
        Me.tblNew.BackgroundImage = CType(resources.GetObject("tblNew.BackgroundImage"), System.Drawing.Image)
        Me.tblNew.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tblNew.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblNew.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tblNew.Image = CType(resources.GetObject("tblNew.Image"), System.Drawing.Image)
        Me.tblNew.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblNew.Name = "tblNew"
        Me.tblNew.Size = New System.Drawing.Size(37, 50)
        Me.tblNew.Tag = "New"
        Me.tblNew.Text = "&New"
        Me.tblNew.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tblNew.ToolTipText = "New"
        Me.tblNew.Visible = False
        '
        'tblShowNarrative
        '
        Me.tblShowNarrative.BackColor = System.Drawing.Color.Transparent
        Me.tblShowNarrative.BackgroundImage = CType(resources.GetObject("tblShowNarrative.BackgroundImage"), System.Drawing.Image)
        Me.tblShowNarrative.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tblShowNarrative.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblShowNarrative.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tblShowNarrative.Image = CType(resources.GetObject("tblShowNarrative.Image"), System.Drawing.Image)
        Me.tblShowNarrative.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblShowNarrative.Name = "tblShowNarrative"
        Me.tblShowNarrative.Size = New System.Drawing.Size(66, 50)
        Me.tblShowNarrative.Tag = "Narrative"
        Me.tblShowNarrative.Text = "&Narrative"
        Me.tblShowNarrative.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tblShowNarrative.ToolTipText = "History Narrative"
        '
        'tblReviewofHistory
        '
        Me.tblReviewofHistory.BackColor = System.Drawing.Color.Transparent
        Me.tblReviewofHistory.BackgroundImage = CType(resources.GetObject("tblReviewofHistory.BackgroundImage"), System.Drawing.Image)
        Me.tblReviewofHistory.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tblReviewofHistory.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblReviewofHistory.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tblReviewofHistory.Image = CType(resources.GetObject("tblReviewofHistory.Image"), System.Drawing.Image)
        Me.tblReviewofHistory.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblReviewofHistory.Name = "tblReviewofHistory"
        Me.tblReviewofHistory.Size = New System.Drawing.Size(55, 50)
        Me.tblReviewofHistory.Tag = "Review"
        Me.tblReviewofHistory.Text = "&Review"
        Me.tblReviewofHistory.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tlbViewReview
        '
        Me.tlbViewReview.BackColor = System.Drawing.Color.Transparent
        Me.tlbViewReview.BackgroundImage = CType(resources.GetObject("tlbViewReview.BackgroundImage"), System.Drawing.Image)
        Me.tlbViewReview.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlbViewReview.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlbViewReview.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tlbViewReview.Image = CType(resources.GetObject("tlbViewReview.Image"), System.Drawing.Image)
        Me.tlbViewReview.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbViewReview.Name = "tlbViewReview"
        Me.tlbViewReview.Size = New System.Drawing.Size(88, 50)
        Me.tlbViewReview.Tag = "ViewReview"
        Me.tlbViewReview.Text = "&View Review"
        Me.tlbViewReview.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tblHistoryofHistory
        '
        Me.tblHistoryofHistory.BackColor = System.Drawing.Color.Transparent
        Me.tblHistoryofHistory.BackgroundImage = CType(resources.GetObject("tblHistoryofHistory.BackgroundImage"), System.Drawing.Image)
        Me.tblHistoryofHistory.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tblHistoryofHistory.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblHistoryofHistory.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tblHistoryofHistory.Image = CType(resources.GetObject("tblHistoryofHistory.Image"), System.Drawing.Image)
        Me.tblHistoryofHistory.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblHistoryofHistory.Name = "tblHistoryofHistory"
        Me.tblHistoryofHistory.Size = New System.Drawing.Size(36, 50)
        Me.tblHistoryofHistory.Tag = "Hx"
        Me.tblHistoryofHistory.Text = "&Hx"
        Me.tblHistoryofHistory.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tblHistoryofHistory.ToolTipText = "History Audit"
        '
        'tblShow
        '
        Me.tblShow.BackColor = System.Drawing.Color.Transparent
        Me.tblShow.BackgroundImage = CType(resources.GetObject("tblShow.BackgroundImage"), System.Drawing.Image)
        Me.tblShow.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tblShow.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblShow.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tblShow.Image = CType(resources.GetObject("tblShow.Image"), System.Drawing.Image)
        Me.tblShow.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblShow.Name = "tblShow"
        Me.tblShow.Size = New System.Drawing.Size(46, 50)
        Me.tblShow.Tag = "Show"
        Me.tblShow.Text = "Sh&ow"
        Me.tblShow.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tblShow.ToolTipText = "Show"
        '
        'tblCCD
        '
        Me.tblCCD.BackgroundImage = CType(resources.GetObject("tblCCD.BackgroundImage"), System.Drawing.Image)
        Me.tblCCD.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tblCCD.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblCCD.Image = CType(resources.GetObject("tblCCD.Image"), System.Drawing.Image)
        Me.tblCCD.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.tblCCD.Name = "tblCCD"
        Me.tblCCD.Size = New System.Drawing.Size(63, 50)
        Me.tblCCD.Tag = "Generate CCD"
        Me.tblCCD.Text = "&Gen CCD"
        Me.tblCCD.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tblCCD.ToolTipText = "Generate CCD"
        '
        'tlbbtn_Reconcile
        '
        Me.tlbbtn_Reconcile.BackgroundImage = CType(resources.GetObject("tlbbtn_Reconcile.BackgroundImage"), System.Drawing.Image)
        Me.tlbbtn_Reconcile.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlbbtn_Reconcile.Enabled = False
        Me.tlbbtn_Reconcile.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.tlbbtn_Reconcile.Image = CType(resources.GetObject("tlbbtn_Reconcile.Image"), System.Drawing.Image)
        Me.tlbbtn_Reconcile.Name = "tlbbtn_Reconcile"
        Me.tlbbtn_Reconcile.Size = New System.Drawing.Size(68, 50)
        Me.tlbbtn_Reconcile.Tag = "Reconcile"
        Me.tlbbtn_Reconcile.Text = "&Reconcile"
        Me.tlbbtn_Reconcile.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tblPastPregnancies
        '
        Me.tblPastPregnancies.BackColor = System.Drawing.Color.Transparent
        Me.tblPastPregnancies.BackgroundImage = CType(resources.GetObject("tblPastPregnancies.BackgroundImage"), System.Drawing.Image)
        Me.tblPastPregnancies.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tblPastPregnancies.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblPastPregnancies.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tblPastPregnancies.Image = CType(resources.GetObject("tblPastPregnancies.Image"), System.Drawing.Image)
        Me.tblPastPregnancies.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblPastPregnancies.Name = "tblPastPregnancies"
        Me.tblPastPregnancies.Size = New System.Drawing.Size(115, 50)
        Me.tblPastPregnancies.Tag = "Past Pregnancies"
        Me.tblPastPregnancies.Text = "&Past Pregnancies"
        Me.tblPastPregnancies.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tblPastPregnancies.ToolTipText = "Past Pregnancies"
        '
        'tblHistoryReconcillation
        '
        Me.tblHistoryReconcillation.BackColor = System.Drawing.Color.Transparent
        Me.tblHistoryReconcillation.BackgroundImage = CType(resources.GetObject("tblHistoryReconcillation.BackgroundImage"), System.Drawing.Image)
        Me.tblHistoryReconcillation.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tblHistoryReconcillation.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblHistoryReconcillation.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tblHistoryReconcillation.Image = CType(resources.GetObject("tblHistoryReconcillation.Image"), System.Drawing.Image)
        Me.tblHistoryReconcillation.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblHistoryReconcillation.Name = "tblHistoryReconcillation"
        Me.tblHistoryReconcillation.Size = New System.Drawing.Size(151, 50)
        Me.tblHistoryReconcillation.Tag = "Reconcillation"
        Me.tblHistoryReconcillation.Text = "&Allergies Reconciliation"
        Me.tblHistoryReconcillation.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tblHistoryReconcillation.ToolTipText = "Allergies Reconciliation"
        '
        'tblRecHistory
        '
        Me.tblRecHistory.BackColor = System.Drawing.Color.Transparent
        Me.tblRecHistory.BackgroundImage = CType(resources.GetObject("tblRecHistory.BackgroundImage"), System.Drawing.Image)
        Me.tblRecHistory.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tblRecHistory.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblRecHistory.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tblRecHistory.Image = CType(resources.GetObject("tblRecHistory.Image"), System.Drawing.Image)
        Me.tblRecHistory.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblRecHistory.Name = "tblRecHistory"
        Me.tblRecHistory.Size = New System.Drawing.Size(177, 50)
        Me.tblRecHistory.Tag = "RecHist"
        Me.tblRecHistory.Text = "V&iew Reconciliation History"
        Me.tblRecHistory.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tblRecHistory.ToolTipText = "View Reconciliation History"
        '
        'tblNKAllergies
        '
        Me.tblNKAllergies.BackColor = System.Drawing.Color.Transparent
        Me.tblNKAllergies.BackgroundImage = CType(resources.GetObject("tblNKAllergies.BackgroundImage"), System.Drawing.Image)
        Me.tblNKAllergies.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tblNKAllergies.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblNKAllergies.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tblNKAllergies.Image = CType(resources.GetObject("tblNKAllergies.Image"), System.Drawing.Image)
        Me.tblNKAllergies.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblNKAllergies.Name = "tblNKAllergies"
        Me.tblNKAllergies.Size = New System.Drawing.Size(90, 50)
        Me.tblNKAllergies.Tag = "NKAllergies"
        Me.tblNKAllergies.Text = "&N.K. Allergies"
        Me.tblNKAllergies.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tblNKAllergies.ToolTipText = "No Known Allergies"
        '
        'tblSave
        '
        Me.tblSave.BackColor = System.Drawing.Color.Transparent
        Me.tblSave.BackgroundImage = CType(resources.GetObject("tblSave.BackgroundImage"), System.Drawing.Image)
        Me.tblSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tblSave.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblSave.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tblSave.Image = CType(resources.GetObject("tblSave.Image"), System.Drawing.Image)
        Me.tblSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblSave.Name = "tblSave"
        Me.tblSave.Size = New System.Drawing.Size(66, 50)
        Me.tblSave.Tag = "Save"
        Me.tblSave.Text = "&Save&&Cls"
        Me.tblSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tblSave.ToolTipText = "Save and Close "
        '
        'tblClose
        '
        Me.tblClose.BackColor = System.Drawing.Color.Transparent
        Me.tblClose.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tblClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tblClose.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblClose.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tblClose.Image = CType(resources.GetObject("tblClose.Image"), System.Drawing.Image)
        Me.tblClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblClose.Name = "tblClose"
        Me.tblClose.Size = New System.Drawing.Size(43, 50)
        Me.tblClose.Tag = "Close"
        Me.tblClose.Text = "&Close"
        Me.tblClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tblClose.ToolTipText = "Close "
        '
        'ContextMenuC1History
        '
        Me.ContextMenuC1History.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuRemove, Me.mnuAddReaction})
        '
        'mnuRemove
        '
        Me.mnuRemove.Index = 0
        Me.mnuRemove.Text = "Remove History"
        '
        'mnuAddReaction
        '
        Me.mnuAddReaction.Index = 1
        Me.mnuAddReaction.Text = "Add Reaction"
        '
        'ContextMenutrvPrevHistory
        '
        Me.ContextMenutrvPrevHistory.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuDeleteHistory, Me.mnuMakeCurrent})
        '
        'mnuDeleteHistory
        '
        Me.mnuDeleteHistory.Index = 0
        Me.mnuDeleteHistory.Text = "&Delete History"
        '
        'mnuMakeCurrent
        '
        Me.mnuMakeCurrent.Index = 1
        Me.mnuMakeCurrent.Text = "Make as Current History"
        '
        'cntCategory
        '
        Me.cntCategory.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuAddHistoryItem, Me.mnuEditHistoryItem})
        '
        'mnuAddHistoryItem
        '
        Me.mnuAddHistoryItem.Index = 0
        Me.mnuAddHistoryItem.Text = "Add History Item"
        '
        'mnuEditHistoryItem
        '
        Me.mnuEditHistoryItem.Index = 1
        Me.mnuEditHistoryItem.Text = "Edit History Item"
        '
        'C1SuperTooltip1
        '
        Me.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None
        Me.C1SuperTooltip1.Font = New System.Drawing.Font("Tahoma", 8.0!)
        '
        'cntFindings
        '
        Me.cntFindings.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnu_AddFindings})
        Me.cntFindings.Name = "cntFindings"
        Me.cntFindings.Size = New System.Drawing.Size(145, 26)
        '
        'mnu_AddFindings
        '
        Me.mnu_AddFindings.Name = "mnu_AddFindings"
        Me.mnu_AddFindings.Size = New System.Drawing.Size(144, 22)
        Me.mnu_AddFindings.Text = "Add Findings"
        '
        'pnltxtCVXCode
        '
        Me.pnltxtCVXCode.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.pnltxtCVXCode.Controls.Add(Me.txtCVXCode)
        Me.pnltxtCVXCode.Controls.Add(Me.Label80)
        Me.pnltxtCVXCode.Controls.Add(Me.Label81)
        Me.pnltxtCVXCode.Controls.Add(Me.Label82)
        Me.pnltxtCVXCode.Controls.Add(Me.Label84)
        Me.pnltxtCVXCode.Location = New System.Drawing.Point(162, 117)
        Me.pnltxtCVXCode.Name = "pnltxtCVXCode"
        Me.pnltxtCVXCode.Size = New System.Drawing.Size(445, 23)
        Me.pnltxtCVXCode.TabIndex = 256
        '
        'Label80
        '
        Me.Label80.BackColor = System.Drawing.Color.FromArgb(CType(CType(171, Byte), Integer), CType(CType(173, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.Label80.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label80.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label80.Location = New System.Drawing.Point(444, 1)
        Me.Label80.Name = "Label80"
        Me.Label80.Size = New System.Drawing.Size(1, 21)
        Me.Label80.TabIndex = 15
        Me.Label80.Text = "label2"
        '
        'Label81
        '
        Me.Label81.BackColor = System.Drawing.Color.FromArgb(CType(CType(171, Byte), Integer), CType(CType(173, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.Label81.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label81.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label81.Location = New System.Drawing.Point(0, 1)
        Me.Label81.Name = "Label81"
        Me.Label81.Size = New System.Drawing.Size(1, 21)
        Me.Label81.TabIndex = 14
        Me.Label81.Text = "label2"
        '
        'Label82
        '
        Me.Label82.BackColor = System.Drawing.Color.FromArgb(CType(CType(171, Byte), Integer), CType(CType(173, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.Label82.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label82.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label82.Location = New System.Drawing.Point(0, 0)
        Me.Label82.Name = "Label82"
        Me.Label82.Size = New System.Drawing.Size(445, 1)
        Me.Label82.TabIndex = 13
        Me.Label82.Text = "label2"
        '
        'Label84
        '
        Me.Label84.BackColor = System.Drawing.Color.FromArgb(CType(CType(171, Byte), Integer), CType(CType(173, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.Label84.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label84.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label84.Location = New System.Drawing.Point(0, 22)
        Me.Label84.Name = "Label84"
        Me.Label84.Size = New System.Drawing.Size(445, 1)
        Me.Label84.TabIndex = 12
        Me.Label84.Text = "label2"
        '
        'txtCVXCode
        '
        Me.txtCVXCode.AutoEllipsis = True
        Me.txtCVXCode.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCVXCode.ForeColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.txtCVXCode.Location = New System.Drawing.Point(1, 1)
        Me.txtCVXCode.Name = "txtCVXCode"
        Me.txtCVXCode.Padding = New System.Windows.Forms.Padding(2, 2, 0, 0)
        Me.txtCVXCode.Size = New System.Drawing.Size(443, 21)
        Me.txtCVXCode.TabIndex = 254
        '
        'frmHistory
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(1184, 836)
        Me.Controls.Add(Me.pnlToolStrip)
        Me.Controls.Add(Me.pnlOuter)
        Me.DoubleBuffered = True
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmHistory"
        Me.ShowInTaskbar = False
        Me.Text = "Patient History"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.pnlOuter.ResumeLayout(False)
        Me.pnlMain.ResumeLayout(False)
        Me.PnlRight.ResumeLayout(False)
        Me.pnltrvTarget.ResumeLayout(False)
        Me.pnltrvTarget.PerformLayout()
        Me.Panel8.ResumeLayout(False)
        CType(Me.C1HistoryDetails, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlmonograph.ResumeLayout(False)
        Me.pnlmonograph.PerformLayout()
        Me.pnlDIScreenResult.ResumeLayout(False)
        Me.pnlDIScreenResult.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.pnlsnomadedetail.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.pnlShowCQM.ResumeLayout(False)
        Me.pnlShowCQM.PerformLayout()
        Me.Panel11.ResumeLayout(False)
        Me.Panel12.ResumeLayout(False)
        Me.Panel10.ResumeLayout(False)
        Me.Panel9.ResumeLayout(False)
        Me.Panel7.ResumeLayout(False)
        Me.Panel6.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        Me.Panel14.ResumeLayout(False)
        Me.pnlreview.ResumeLayout(False)
        Me.pnlreview.PerformLayout()
        Me.pnlPatientHeader.ResumeLayout(False)
        Me.pnlPatientHeader.PerformLayout()
        Me.pnlWordComp.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.pnlPrevHistory.ResumeLayout(False)
        Me.pnl_Base.ResumeLayout(False)
        Me.pnlPrevSearch.ResumeLayout(False)
        Me.pnlSearch.ResumeLayout(False)
        Me.pnlSearch.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlCatBtn.ResumeLayout(False)
        Me.pnltrvSource.ResumeLayout(False)
        Me.pnlSnoMedtrvSource.ResumeLayout(False)
        Me.pnlSMSearch.ResumeLayout(False)
        Me.pnlSMSearch.PerformLayout()
        CType(Me.PicBx_Search, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlSubtype.ResumeLayout(False)
        Me.pnlSMFindings.ResumeLayout(False)
        Me.pnlBottomButtons.ResumeLayout(False)
        Me.pnlToolStrip.ResumeLayout(False)
        Me.pnlToolStrip.PerformLayout()
        Me.tblHistory.ResumeLayout(False)
        Me.tblHistory.PerformLayout()
        Me.cntFindings.ResumeLayout(False)
        Me.pnltxtCVXCode.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

#Region " Patient Details Strip "
    Private WithEvents gloUC_PatientStrip1 As gloUserControlLibrary.gloUC_PatientStrip

    Private Sub GloUC_PatientStrip1_ControlSizeChanged() Handles gloUC_PatientStrip1.ControlSizeChanged
        Try
            '' pnlPatientHeader.Height = gloUC_PatientStrip1.Height
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Set_PatientDetailStrip()

        'SLR: Free previous memory
        If Not IsNothing(gloUC_PatientStrip1) Then
            If (pnlOuter.Controls.Contains(gloUC_PatientStrip1)) Then
                pnlOuter.Controls.Remove(gloUC_PatientStrip1)
            End If
            gloUC_PatientStrip1.Dispose()
            gloUC_PatientStrip1 = Nothing
        End If
        gloUC_PatientStrip1 = New gloUserControlLibrary.gloUC_PatientStrip

        With gloUC_PatientStrip1
            .Dock = DockStyle.Top
            .Padding = New Padding(3, 0, 3, 0)
            .ShowDetail(m_PatientID, gloUserControlLibrary.gloUC_PatientStrip.enumFormName.History)
            .BringToFront()
            If gblnEnableCQMCypressTesting Then ''Bug #109480:Resolved
                gloUC_PatientStrip1.DTP.CustomFormat = "MM/dd/yyyy hh:mm:ss tt"

            Else
                gloUC_PatientStrip1.DTP.CustomFormat = "MM/dd/yyyy"

            End If

            .DTPFormat = DateTimePickerFormat.Short
            .DTPEnabled = False
        End With
        pnlOuter.Controls.Add(gloUC_PatientStrip1)
        pnlMain.BringToFront()
        C1HistoryDetails.BringToFront()
        pnlPatientHeader.Visible = False
    End Sub

#End Region

    Private Sub frmHistory_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Try
            CType(Me.ParentForm, MainMenu).SetGnPatientID = GetCurrentPatientID
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try

        If Not IsNothing(uiPanSplitScreen_History) Then
            If Not IsNothing(uiPanSplitScreen_History.Parent) Then
                If uiPanSplitScreen_History.Parent.Text = "Split Screen" Then
                    uiPanSplitScreen_History.Parent.Visible = True
                ElseIf uiPanSplitScreen_History.Text = "Split Screen" Then
                    uiPanSplitScreen_History.Visible = True
                End If

            End If

        End If


    End Sub
    'Bug #53045: 00000471 : History
    'while resolving the bug this function added to keep code common for validation
    Private Function ValidatePatientHistory() As Boolean
        Try
            If IsNothing(C1HistoryDetails.Editor) = False Then
                If (C1HistoryDetails.Col = Col_HsComments) And (C1HistoryDetails.Editor.Visible) And (C1HistoryDetails.Editor.Text.Length > MAX_COMMENT_LENGHT) Then
                    MessageBox.Show("Comment should be less than equal to 760 characters.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Return True
                End If
            End If
            '' SaveHistory()
            If gstrCodeFieldsinHistory = "CodeMandatory" Then
                For m As Integer = 0 To C1HistoryDetails.Rows.Count - 1
                    'Bug #61329: 00000599 : patient has multiple history items in different casing the system will throw an error.
                    If C1HistoryDetails.Rows(m)(Col_HHidden).ToString.ToUpper <> C1HistoryDetails.Rows(m)(Col_HCategory).ToString.ToUpper Then
                        If Date.Parse(C1HistoryDetails.Rows(m)(Col_HDOE_Allergy)).Date = Date.Now.Date Then

                            If Convert.ToString(C1HistoryDetails.Rows(m)(Col_HsActive)).Trim <> "False" Then
                                If ((Convert.ToString(C1HistoryDetails.Rows(m)(Col_HsICD9)) = "") And (Convert.ToString(C1HistoryDetails.Rows(m)(Col_HsConceptID)) = "") And (Convert.ToString(C1HistoryDetails.Rows(m)(col_HCPT)) = "")) Then
                                    MessageBox.Show("There are selected history items that are missing a SNOMED " & vbNewLine & "Concept ID, ICD9, or CPT code." & vbNewLine & vbNewLine & "Please enter in the appropriate codes before saving this transaction. ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                    C1HistoryDetails.Row = m
                                    Return True
                                End If
                            End If
                        End If
                    End If

                Next
            ElseIf gstrCodeFieldsinHistory = "CodeWarning" Then
                For m As Integer = 0 To C1HistoryDetails.Rows.Count - 1
                    'Bug #61329: 00000599 : patient has multiple history items in different casing the system will throw an error.
                    If C1HistoryDetails.Rows(m)(Col_HHidden).ToString.ToUpper <> C1HistoryDetails.Rows(m)(Col_HCategory).ToString.ToUpper Then
                        If Date.Parse(C1HistoryDetails.Rows(m)(Col_HDOE_Allergy)).Date = Date.Now.Date Then

                            If Convert.ToString(C1HistoryDetails.Rows(m)(Col_HsActive)).Trim <> "False" Then
                                If ((Convert.ToString(C1HistoryDetails.Rows(m)(Col_HsICD9)) = "") And (Convert.ToString(C1HistoryDetails.Rows(m)(Col_HsConceptID)) = "") And (Convert.ToString(C1HistoryDetails.Rows(m)(col_HCPT)) = "")) Then
                                    Dim oResult As Windows.Forms.DialogResult
                                    oResult = MessageBox.Show("There are selected history items that are missing a SNOMED" & vbNewLine & "Concept ID, ICD9, or CPT code." & vbNewLine & "Please enter in the appropriate codes before saving this transaction." & vbNewLine & vbNewLine & "Review now? ", gstrMessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                                    If oResult = Windows.Forms.DialogResult.Cancel Then
                                        Return True
                                    ElseIf oResult = Windows.Forms.DialogResult.Yes Then
                                        C1HistoryDetails.Row = m
                                        Return True
                                    ElseIf oResult = Windows.Forms.DialogResult.No Then
                                        Exit For
                                    End If
                                End If
                            End If
                        End If
                    End If
                Next
                Return Nothing
            End If
            Return Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Return True
        End Try
    End Function

    Private Sub frmHistory_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing


        Dim blnCancelClicked As Boolean


        Try
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Close, "History Form Closed", m_PatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)

        Catch ex As Exception

        End Try

        Try


            IsCloseClickFlagForCommentValidation = True
            Dim IsValidateCommentCancled As Boolean = False
            If IsNothing(C1HistoryDetails.Editor) = False Then
                If (C1HistoryDetails.Col = Col_HsComments) And (C1HistoryDetails.Editor.Visible) And (C1HistoryDetails.Editor.Text.Length > MAX_COMMENT_LENGHT) Then 'And (e.Cancel)
                    IsValidateCommentCancled = True
                End If
            End If
            Me.BindingContext(dsHistory, "History").EndCurrentEdit()
            If (IsNothing(dsHistory.GetChanges) = False AndAlso _blnRecordLock = False) OrElse (_blnRecordLock = False AndAlso IsValidateCommentCancled = True AndAlso _isMakeAsCurrent = False) Then
                Dim oResult As Windows.Forms.DialogResult
                oResult = MessageBox.Show("Do you want to save the changes to history?", gstrMessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)

                If oResult = Windows.Forms.DialogResult.Cancel Then
                    '08-Oct-14 Aniket: Resolving Bug #74861: gloEMR - History - Application is throwing Null Reference exception.
                    blnCancelClicked = True
                    e.Cancel = True
                ElseIf oResult = Windows.Forms.DialogResult.Yes Then
                    'Bug #53045: 00000471 : History
                    'Validation code removed and validation function called
                    If ValidatePatientHistory() Then
                        e.Cancel = True
                        Exit Sub
                    End If
                    If Not dtPHistoryMedRec Is Nothing Then
                        If dtPHistoryMedRec.Rows.Count > 0 Then

                            If Not getProviderTaxID(gnPatientProviderID) Then
                                e.Cancel = True
                                Exit Sub
                            End If
                        End If
                    End If
                    Call SaveHistoryData()
                    If Not dtPHistoryMedRec Is Nothing Then
                        If dtPHistoryMedRec.Rows.Count > 0 Then
                            Dim MedicalReconcilationId As Long = GetMedicalReconcillationID(m_VisitID)
                            If MedicalReconcilationId > 0 Then
                                Dim oclsselectProviderTaxID As New gloGlobal.TIN.clsSelectProviderTaxID(gnPatientProviderID)
                                oclsselectProviderTaxID.InsertProviderTaxID(nProviderAssociationID, MedicalReconcilationId, sProviderTaxID, gnPatientProviderID, gnLoginProviderID, gloGlobal.TIN.clsSelectProviderTaxID.TransactionType.ManualReconciliationHistory.GetHashCode())
                                oclsselectProviderTaxID = Nothing
                            End If
                        End If
                    End If
                    If _IsFrmImm = True Then
                        Dim StrItem As String
                        Dim strReaction As String
                        With C1HistoryDetails
                            .Row = 0
                            For i As Integer = 1 To .Rows.Count - 1
                                StrItem = .GetData(i, Col_HsHistoryItem) & ""
                                strReaction = .GetData(i, Col_HsReaction) & ""

                                If _IsFrmImm = True And StrItem = _strAllergy Then
                                    _sReaction = strReaction
                                End If
                            Next
                        End With
                    End If
                ElseIf oResult = Windows.Forms.DialogResult.No Then
                    If IsNothing(C1HistoryDetails.Editor) = False Then
                        If (C1HistoryDetails.Col = Col_HsComments) And (C1HistoryDetails.Editor.Visible) And (C1HistoryDetails.Editor.Text.Length > MAX_COMMENT_LENGHT) Then
                            C1HistoryDetails.Editor.Text = ""
                        End If
                    End If
                    If _isMakeAsCurrent = True Then
                        '  Dim i As Integer
                        If IsNothing(dsCurrent) = False Then
                            SaveDatasetHistory_new(dsCurrent)
                        End If
                    End If
                    e.Cancel = False
                End If
            Else
                If _IsFrmImm = True Then
                    Dim StrItem As String
                    Dim strReaction As String
                    With C1HistoryDetails
                        .Row = 0
                        For i As Integer = 1 To .Rows.Count - 1
                            StrItem = .GetData(i, Col_HsHistoryItem) & ""
                            strReaction = .GetData(i, Col_HsReaction) & ""

                            If _IsFrmImm = True And StrItem = _strAllergy Then
                                _sReaction = strReaction
                            End If
                        Next
                    End With
                End If
            End If
            If _isMakeAsCurrent = True AndAlso IsNothing(dsHistory.GetChanges) = True AndAlso _isMessage = False Then
                Dim oResult As Windows.Forms.DialogResult
                oResult = MessageBox.Show("Do you want to save the changes to history?", gstrMessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
                If oResult = Windows.Forms.DialogResult.Cancel Then
                    e.Cancel = True
                ElseIf oResult = Windows.Forms.DialogResult.Yes Then
                    If IsNothing(C1HistoryDetails.Editor) = False Then
                        If (C1HistoryDetails.Col = Col_HsComments) And (C1HistoryDetails.Editor.Visible) And (C1HistoryDetails.Editor.Text.Length > MAX_COMMENT_LENGHT) Then
                            MessageBox.Show("Comment should be less than equal to 760 characters.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            e.Cancel = True
                            Exit Sub
                        End If
                    End If
                    Call SaveHistoryData()
                ElseIf oResult = Windows.Forms.DialogResult.No Then
                    If IsNothing(C1HistoryDetails.Editor) = False Then
                        If (C1HistoryDetails.Col = Col_HsComments) And (C1HistoryDetails.Editor.Visible) And (C1HistoryDetails.Editor.Text.Length > MAX_COMMENT_LENGHT) Then
                            C1HistoryDetails.Editor.Text = ""
                        End If
                    End If
                    e.Cancel = False
                End If
            End If
            If (e.Cancel = False) Then
                Me.Visible = False
                Me.WindowState = FormWindowState.Normal
            End If

            If Not e.Cancel Then
                If IsNothing(clsSplit_History) = False Then
                    clsSplit_History.SaveControlDisplaySettings()
                End If

            End If
            If Not IsNothing(dtPHistoryMedRec) Then ''disposed 
                dtPHistoryMedRec.Dispose()
                dtPHistoryMedRec = Nothing
            End If


        Catch ex As Exception

        Finally

            '08-Oct-14 Aniket: Resolving Bug #74861: gloEMR - History - Application is throwing Null Reference exception.
            If blnCancelClicked = False Then

                IsCloseClickFlagForCommentValidation = False
                'SLR: Free objclsPatientHistory, uiPanSplitScreen_History, objCriteria, objWord
                'If Not IsNothing(objclsPatientHistory) Then
                '    objclsPatientHistory = Nothing
                'End If
                'If Not IsNothing(uiPanSplitScreen_History) Then
                '    uiPanSplitScreen_History = Nothing
                'End If

                If Not IsNothing(objCriteria) Then
                    objCriteria.Dispose()
                    objCriteria = Nothing
                End If

                If Not IsNothing(objWord) Then
                    objWord = Nothing
                End If

            End If

        End Try

    End Sub

    Private Sub SaveHistoryData()
        If (IsNothing(dsHistory) = False) Then
            If ChkResolvedEnddate.Checked = True Then
                If dsHistory.Tables("History").Rows.Count >= C1HistoryDetails.Row - 1 Then
                    dsHistory.Tables("History").Rows(C1HistoryDetails.Row - 1)("dtObservationEndDate") = ResolvedEndDate.Value

                End If
            

            End If
            Dim dsModify As DataSet = Nothing
            Dim ds As DataSet = Nothing
            Try

                If _isMakeAsCurrent = True Then
                    Dim i As Integer
                    _isMessage = True

                    dsModify = dsHistory.Copy()
                    objclsPatientHistory.DeleteHistory(m_VisitID, m_PatientID)
                    For i = 0 To dsModify.Tables("History").Rows.Count - 1
                        If dsModify.Tables("History").Rows(i).RowState <> DataRowState.Deleted Then
                            dsModify.Tables("history").Rows(i)("RowState") = "Added"
                        Else
                            dsModify.Tables("History").Rows(i).RejectChanges()
                            dsModify.Tables("History").Rows(i)("RowState") = "Deleted"
                        End If
                    Next
                    Call SaveDatasetHistory_new(dsModify)
                    dsHistory.AcceptChanges()

                Else
                    ''For normal save and close operation
                    Me.BindingContext(dsHistory, "History").EndCurrentEdit()
                    'objclsPatientHistory.DeleteHistory(m_VisitID, m_PatientID)
                    'Call SaveDatasetHistory_new(dsHistory)
                    ''Codition Modified by MAYURI:20120913- _dsTemp as used for delete rows collection
                    If IsNothing(dsHistory.GetChanges) = False OrElse _dsTemp.Tables("History").Rows.Count > 0 Then
                        ds = dsHistory.Copy()
                        If _isMedicationHistoryModify = True Then
                            If _isDoubleclickPrevHistory = True Then
                                Dim l As Int16
                                For l = 0 To ds.Tables("History").Rows.Count - 1
                                    If ds.Tables("History").Rows(l).RowState <> DataRowState.Deleted Then
                                        If Convert.ToString(ds.Tables("History").Rows(l)("nHistoryID")) = "0" Then
                                            ds.Tables("History").Rows(l)("RowState") = "Added"
                                        End If
                                    Else
                                        ds.Tables("History").Rows(l).RejectChanges()
                                        ds.Tables("History").Rows(l)("RowState") = "Deleted"
                                    End If
                                Next
                            Else
                                Dim l As Int16
                                For l = 0 To ds.Tables("History").Rows.Count - 1
                                    If ds.Tables("History").Rows(l).RowState <> DataRowState.Deleted Then
                                        If Convert.ToString(ds.Tables("History").Rows(l)("nHistoryID")) = "0" Then
                                            ds.Tables("History").Rows(l)("RowState") = "Added"
                                        End If
                                    Else
                                        ds.Tables("History").Rows(l).RejectChanges()
                                    End If
                                Next
                            End If

                            Call SaveDatasetHistory_new(ds)
                            If Not IsNothing(dsHistory) Then
                                dsHistory.AcceptChanges()
                            End If

                        Else
                            dsHistory = SetRowState(dsHistory.GetChanges)
                            Call SaveDatasetHistory_new(dsHistory)
                            If Not IsNothing(dsHistory) Then
                                dsHistory.AcceptChanges()
                            End If
                        End If


                    Else '' For Medication Reconciliation  when no history Present
                        SavePHistoryMedicationReconcillation()
                        objclsPatientHistory.UpdateMedicalReconcillation(m_PatientID, m_VisitID, nReconcillationType, dtPHistoryMedRec)



                    End If

                End If
                If Trim(wdNarration.Text) <> "" Then
                    Dim strTempFileName1 As String = gloSettings.FolderSettings.AppTempFolderPath & getUniqueID() & "temp5.txt" 'SLR: Changed temp5 to uniqueID

                    wdNarration.SaveFile(strTempFileName1)

                    objclsPatientHistory.SaveNarration(lblVisitDate.Tag, lblPatientCode.Tag, strTempFileName1)
                Else

                    objclsPatientHistory.DeleteNarration(lblVisitDate.Tag, lblPatientCode.Tag)
                    wdNarration.Text = ""
                End If
                If blnmedRecupdated = True Then
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.ClinicalReconciliation, gloAuditTrail.ActivityType.Save, "Allergy Reconciliation Added From History", m_PatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)

                End If
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

            Finally
                If IsNothing(ds) = False Then
                    ds.Dispose()
                    ds = Nothing
                End If
                If IsNothing(dsModify) = False Then
                    dsModify.Dispose()
                    dsModify = Nothing
                End If
            End Try
        End If

    End Sub
    Private Function Get_PatientDetails()
        Dim dsPatient As DataSet = Nothing

        Try
            dsPatient = dsHistory.Copy()
            'dtPatient = New DataTable
            'dtPatient = GetPatientInfo(m_PatientID)
            With dsPatient.Tables("Patient")
                If IsNothing(dsPatient.Tables("Patient")) = False Then
                    If .Rows.Count > 0 Then
                        strPatientCode = Convert.ToString(.Rows(0)("sPatientCode"))
                        strPatientFirstName = Convert.ToString(.Rows(0)("sFirstName"))
                        strPatientLastName = Convert.ToString(.Rows(0)("sLastName"))
                        strPatientDOB = Convert.ToString(.Rows(0)("dtDOB"))
                        strPatientAge = GetAge(Convert.ToDateTime(.Rows(0)("dtDOB")))
                        strPatientGender = Convert.ToString(.Rows(0)("sGender"))
                        strPatientMaritalStatus = Convert.ToString(.Rows(0)("sMaritalStatus"))

                    End If
                End If
            End With
        Catch ex As Exception

        Finally
            If IsNothing(dsPatient) = False Then
                dsPatient.Dispose()
                dsPatient = Nothing
            End If


        End Try
        Return Nothing
    End Function

    Private Function AllergySeverity() As DataTable

        Dim dtAllergyTable As DataTable = Nothing
        Dim oclsPatientHistory As clsPatientHistory = Nothing

        Try
            oclsPatientHistory = New clsPatientHistory
            dtAllergyTable = oclsPatientHistory.GetAllergyServerity()
            oclsPatientHistory.Dispose()
            oclsPatientHistory = Nothing

            Return dtAllergyTable
        Catch ex As Exception
            Return dtAllergyTable
        Finally
            If Not IsNothing(dtAllergyTable) = False Then
                dtAllergyTable.Dispose()
                dtAllergyTable = Nothing
            End If
            If Not IsNothing(oclsPatientHistory) = False Then
                'oclsPatientHistory.Dispose()
                oclsPatientHistory = Nothing
            End If


        End Try
    End Function

    Private Sub frmPatientHistory_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ''Added buttons by MAYURI:20120913- To move items up and down within category

        pnlsnomadedetail.Height = 170
        pnlShowCQM.Dock = DockStyle.None
        pnlShowCQM.Visible = False


        ResolvedEndDate.Checked = False

        ResolvedEndDate.Value = DateTime.Now
        ResolvedEndDate.Enabled = False
        btnUp.Visible = True
        btnDown.Visible = True
        btnUp.Enabled = False
        btnDown.Enabled = False
        Me.SuspendLayout()
        gloC1FlexStyle.Style(C1HistoryDetails, True)
        cmbAllergyType.Items.Add("All")
        cmbAllergyType.Items.Add("Active")
        cmbAllergyType.Items.Add("Inactive")
        cmbAllergyType.SelectedIndex = 0
        C1HistoryDetails.ShowCellLabels = False
        Fill_HistoryConcernStatus()
        Fill_AllergyIntelorenceType()

        Dim dtAllergySeverity As DataTable = Nothing
        ''
        Try
            Call Get_PatientDetails()

            dtAllergySeverity = AllergySeverity()
            If Not IsNothing(dtAllergySeverity) Then
                cmbAllergySeverity.DataSource = dtAllergySeverity
                cmbAllergySeverity.DisplayMember = dtAllergySeverity.Columns("AllergySeverityName").ToString()
                cmbAllergySeverity.ValueMember = dtAllergySeverity.Columns("AllergySeverityCode").ToString()
            End If


            If gblnVoiceEnabled = True And gblnSpeakerExists = True Then
                Try
                    ''slr free previous memory
                    If Not IsNothing(HistoryVoiceCol) Then
                        HistoryVoiceCol = Nothing
                    End If
                    HistoryVoiceCol = New DNSTools.DgnStrings
                    voicecol = New DNSTools.DgnStrings
                    Call AddBasicVoiceCommands()
                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.VoiceCommands, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                End Try
            End If
            lblPatientCode.Text = strPatientCode
            lblPatientCode.Tag = m_PatientID
            lblPatientName.Text = strPatientFirstName & " " & strPatientLastName
            lblVisitDate.Text = m_VisitDate
            lblVisitDate.Tag = m_VisitID
            lblReviewed.Visible = False
            Dim dt As DataTable
            dt = dsHistory.Tables("Category")
            'dt = FillHistoryCategory()
            mydtButton = dt

            Dim i As Integer = 0
            Dim myInternalView As New DataView(dt, "", "sDescription Desc", DataViewRowState.CurrentRows)
            If Not IsNothing(myInternalView) Then
                dt = myInternalView.ToTable()
                If Not IsNothing(dt) Then
                    lblHeightMeter.Height = 0
                    pnlBottomButtons.Parent.Controls.Add(Splitter8)
                    Splitter8.Dock = DockStyle.Bottom
                    pnlBottomButtons.SendToBack()
                    pnlBottomButtons.SuspendLayout()

                    For Each historyRow As DataRow In dt.Rows
                        Dim blnAllowMedicalCondition As Boolean = True
                        If historyRow(1).ToString = "Medical Condition" Then
                            blnAllowMedicalCondition = False
                            'GLO2012-0016280 : History throws an error
                            'check clinic level and machine level setting for Drug Interaction
                            ' If both are off then Hide Medical Condition Button

                        End If
                        ''
                        If blnAllowMedicalCondition = True Then
                            objBtn = New Button
                            objBtn.BackColor = System.Drawing.Color.FromArgb(102, 153, 255)
                            objBtn.ForeColor = Color.White
                            objBtn.Tag = CType(historyRow(0), Long) ''  Category ID
                            objBtn.Text = historyRow(1).ToString  '' Category Name
                            objBtn.Dock = DockStyle.Top
                            objBtn.FlatStyle = FlatStyle.Flat
                            objBtn.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
                            objBtn.BackgroundImageLayout = ImageLayout.Stretch
                            objBtn.ForeColor = Color.FromArgb(31, 73, 125)
                            objBtn.Font = gloGlobal.clsgloFont.gFont_BOLD
                            objBtn.Height = 28
                            If gblnVoiceEnabled = True And gblnSpeakerExists = True Then
                                Try
                                    HistoryVoiceCol.Add(historyRow(1).ToString)
                                Catch ex As Exception
                                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.VoiceCommands, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                                End Try
                            End If


                            If historyRow(1).ToString = "Allergies" Then '' FIRST BUTTON PUT TO TOP ''
                                Me.pnlTopButtons.Controls.Add(objBtn)
                                BtnText = objBtn.Text
                                BtnTag = objBtn.Tag
                                objBtn.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongOrange
                                objBtn.BackgroundImageLayout = ImageLayout.Stretch
                                srCategory = historyRow(1).ToString
                                i = i + 1
                                blnIsLoad = True
                                objBtn_Click(objBtn, Nothing)

                            ElseIf _IsOBHistory = True And historyRow(1).ToString = "OB Medical History" Then
                                Me.pnlTopButtons.Controls.Add(objBtn)
                                BtnText = objBtn.Text
                                BtnTag = objBtn.Tag
                                objBtn.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongOrange
                                objBtn.BackgroundImageLayout = ImageLayout.Stretch
                                srCategory = historyRow(1).ToString
                                i = i + 1
                                blnIsLoad = True
                                objBtn_Click(objBtn, Nothing)
                            Else
                                '' OTHER BUTTONS IN BOTTOM PANEL ''
                                Me.pnlBottomButtons.Controls.Add(objBtn)
                                lblHeightMeter.Height = lblHeightMeter.Height + objBtn.Height
                            End If

                            '''' Add the one event handler for ButtonClick event

                            blnIsLoad = False

                            AddHandler objBtn.Click, AddressOf objBtn_Click

                            AddHandler objBtn.MouseHover, AddressOf objBtn_MouseHover
                            AddHandler objBtn.MouseLeave, AddressOf objBtn_MouseLeave

                        End If

                    Next historyRow
                    pnlBottomButtons.ResumeLayout()

                    Dim _nButtonsToShow As Integer = 6 '' THIS VARIABLE SET PANEL SIZE TO REQUIRED BUTTON NUMBERS ''
                    pnlBottomButtons.Height = (_nButtonsToShow * 28) + 3  '' SET BOTTOM PANEL HEIGHT ACCORDING TO AVAILABLE BUTTONS ''


                    If lblHeightMeter.Height < ((_nButtonsToShow * 28) + 3) Then
                        '' IF BUTTONS ARE LESS THAT PANEL HEIGHT, THEN ADJUST PANEL HEIGHT TO FIT BUTTONS ''
                        pnlBottomButtons.Height = lblHeightMeter.Height + 3
                        lblHeightMeter.Top = 0
                        lblHeightMeter.Height = 0
                        lblHeightMeter.Dock = DockStyle.Top
                    Else
                        '' IF BOTTONS ARE OVERFLOWING PANEL, THEN TO ENABLE PANEL SCROLLBAR, WE NEED TO ADD LABLE AT TOP ''
                        lblHeightMeter.Top = -1
                        lblHeightMeter.Height = 1
                        lblHeightMeter.Dock = DockStyle.Top
                    End If

                End If ''dt
            End If ''myInternalView 
            If Not IsNothing(myInternalView) Then
                myInternalView.Dispose()
                myInternalView = Nothing
            End If
            ''slr free dt
            If Not IsNothing(dt) Then
                dt.Dispose()
                dt = Nothing
            End If
            pnlBottomButtons.AutoScroll = True

            ' pnlCatSearch.Dock = DockStyle.Top

            pnlPrevHistory.Visible = False
            btnPrevHistory.Text = "Show Prev History"
            cmbsearchHistory.Items.Add("History Date")
            cmbsearchHistory.Items.Add("Category")
            cmbsearchHistory.Text = "History Date"
            ' trvSource.ContextMenu = Nothing




            '''''' TextBox for Narrations
            Dim mstream As ADODB.Stream
            Dim strFileName As String
            mstream = New ADODB.Stream

            mstream.Type = ADODB.StreamTypeEnum.adTypeBinary
            mstream.Open()
            strFileName = gloSettings.FolderSettings.AppTempFolderPath & getUniqueID() & "temp5.txt" 'SLR: Changed temp5 to uniqueID
            '' SUDHIR 20100102 '' READONLY FILE WAS GIVING WRITE ERROR ''
            If File.Exists(strFileName) Then
                Dim oFileInfo As New FileInfo(strFileName)
                oFileInfo.IsReadOnly = False
                oFileInfo = Nothing
            End If
            '' END SUDHIR ''
            mstream.SaveToFile(strFileName, ADODB.SaveOptionsEnum.adSaveCreateOverWrite)
            mstream.Close()
            ''slr free memory stream
            If Not IsNothing(mstream) Then
                mstream = Nothing
            End If


            'Dim oDB As New gloStream.gloDataBase.gloDataBase
            'Dim strSelect = "SELECT ReviewHistory.nUserID,ReviewHistory.sComments,ReviewHistory.dtReviewDate,User_MST.sLoginName,ISNULL(User_MST.sFirstName,'') as sFirstName,ISNULL(sMiddleName,'') as sMiddleName, ISNULL(User_MST.sLastName,'') as sLastName FROM ReviewHistory INNER JOIN User_MST ON ReviewHistory.nUserID = User_MST.nUserID where ReviewHistory.nPatientID= " & m_PatientID & " AND ReviewHistory.nVisitID =" & lblVisitDate.Tag & " Order By ReviewHistory.dtReviewDate DESC"
            Dim strdtReviewDate As String
            'oDB.Connect(GetConnectionString)

            '19-Apr-13 Aniket: Resolving Memory Leak Issues
            If IsNothing(dtReview) = False Then
                dtReview.Dispose()
                dtReview = Nothing
            End If

            dtReview = dsHistory.Tables("Review")
            'oDB.Disconnect()


            If IsNothing(dtReview) = False Then
                If dtReview.Rows.Count > 0 Then
                    lblReviewed.Visible = True
                    strdtReviewDate = dtReview.Rows(0)("dtReviewDate")

                    If dtReview.Rows(0)("sFirstName") <> "" And dtReview.Rows(0)("sMiddleName") <> "" And dtReview.Rows(0)("sLastName") <> "" Then
                        lblReviewed.Text = "History Reviewed By " + dtReview.Rows(0)("sFirstName") + " " + dtReview.Rows(0)("sMiddleName") + " " + dtReview.Rows(0)("sLastName") + " on " + strdtReviewDate
                        sLoginName = dtReview.Rows(0)("sFirstName") + " " + dtReview.Rows(0)("sLastName")
                    Else
                        lblReviewed.Text = "History Reviewed By " + dtReview.Rows(0)("sLoginName") + " on " + strdtReviewDate
                        sLoginName = dtReview.Rows(0)("sLoginName")
                    End If

                    intCheck = 1
                    'tlbViewReview.Visible = True
                Else
                    lblReviewed.Visible = False
                    'tlbViewReview.Visible = False
                End If
            Else
                lblReviewed.Visible = False
                'tlbViewReview.Visible = False
            End If

            ' If _IsFrmImm = True Then
            If _IsFrmImm = True Then
                FillAllergy(_IsFrmImm) ''bug 6616
            End If
            '   trvICD9.Width = (pnlRelationship.Width / 2)
            '   trvDefination.Width = (pnlRelationship.Width / 2)
            '  End If
            ''---
            pnlSnoMedtrvSource.Visible = False
            pnlsnomadedetail.Visible = True
            pnltrvSource.Visible = True
            pnltrvSource.Dock = DockStyle.Fill
            GloUC_trvHistory.Dock = DockStyle.Fill
            pnlSubTypeDefinition.Visible = False
            gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Add, "Patient History Opened", gloAuditTrail.ActivityOutCome.Success)

            Try
                gloPatient.gloPatient.GetWindowTitle(Me, m_PatientID, GetConnectionString(), gstrMessageBoxCaption)
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            End Try
            Me.Visible = True

            If IsNothing(clsSplit_History) = False Then
                clsSplit_History.clsUCLabControl = New gloUserControlLibrary.gloUC_TransactionHistory()
                clsSplit_History.clsPatientExams = New clsPatientExams()
                clsSplit_History.clsPatientLetters = New clsPatientLetters()
                clsSplit_History.clsPatientMessages = New clsMessage()
                clsSplit_History.clsNurseNotes = New clsNurseNotes()
                clsSplit_History.clsHistory = New clsPatientHistory()
                clsSplit_History.clsLabs = New clsDoctorsDashBoard()
                clsSplit_History.clsDMS = New gloEDocumentV3.eDocManager.eDocGetList()
                clsSplit_History.clsRxmed = New clsPatientDetails()
                clsSplit_History.clsOrders = New clsPatientDetails()
                clsSplit_History.clsProblemList = New clsPatientProblemList()
                clsSplit_History.blnShowSmokingStatusCol = gblnShowSmokingColumn
                If IsNothing(uiPanSplitScreen_History) = False Then
                    uiPanSplitScreen_History.Dispose() : uiPanSplitScreen_History = Nothing
                End If
                allocatedInGlobalForuiPanelSplitScreen = False
                uiPanSplitScreen_History = clsSplit_History.LoadSplitControl(Me, m_PatientID, m_VisitID, "History", objCriteria, objWord, gnClinicID, gnLoginID)
                uiPanSplitScreen_History.BringToFront()
            End If


            'If IsNothing(Me.Parent) Then
            '    uiPanSplitScreen_History.AutoHide = False
            '    'Else
            '    '    'If clsSplit_History.dck = 0 Then
            '    '    uiPanSplitScreen_History.AutoHide = True
            '    '    'End If
            'End If


            ''slr free previous memory
            If Not IsNothing(ToolTipbtn_CPTCode) Then
                ToolTipbtn_CPTCode.Dispose()
                ToolTipbtn_CPTCode = Nothing
            End If
            ''slr free previous memory
            If Not IsNothing(ToolTipbtn_ICD9Code) Then
                ToolTipbtn_ICD9Code.Dispose()
                ToolTipbtn_ICD9Code = Nothing
            End If
            ''slr free previous memory
            If Not IsNothing(ToolTipbtnConceptID) Then
                ToolTipbtnConceptID.Dispose()
                ToolTipbtnConceptID = Nothing
            End If
            ''slr free previous memory
            If Not IsNothing(ToolTipbtnUp) Then
                ToolTipbtnUp.Dispose()
                ToolTipbtnUp = Nothing
            End If
            ''slr free previous memory

            If Not IsNothing(ToolTipbtnDown) Then
                ToolTipbtnDown.Dispose()
                ToolTipbtnDown = Nothing
            End If




            '19-Apr-13 Aniket: Resolving Memory Leak Issues
            ToolTipbtn_CPTCode = New System.Windows.Forms.ToolTip
            ToolTipbtn_ICD9Code = New System.Windows.Forms.ToolTip
            ToolTipbtnConceptID = New System.Windows.Forms.ToolTip
            ToolTipbtnUp = New System.Windows.Forms.ToolTip
            ToolTipbtnDown = New System.Windows.Forms.ToolTip

            ToolTipbtn_CPTCode.SetToolTip(Me.btn_CPTCode, "Browse CPT")
            ToolTipbtn_ICD9Code.SetToolTip(Me.btn_ICD9Code, "Browse ICD9/ICD10")
            ToolTipbtnConceptID.SetToolTip(Me.btnConceptID, "Browse Concept ID")
            ToolTipbtnUp.SetToolTip(Me.btnUp, "Move Up")
            ToolTipbtnDown.SetToolTip(Me.btnDown, "Move Down")

            If _IsOBHistory Then
                tblHistory.ButtonsToHide.Remove(tblPastPregnancies.Name)
            Else
                tblHistory.ButtonsToHide.Add(tblPastPregnancies.Name)
            End If
            '   txtloinc.Enabled = False
            ' txtSNOMEDRefusedCode.Enabled = False

            If Not IsNothing(dtPHistoryMedRec) Then
                dtPHistoryMedRec.Dispose()
                dtPHistoryMedRec = Nothing
            End If
            dtPHistoryMedRec = New DataTable()




            Dim dPatientID As New DataColumn("PatientID", GetType(Long))
            Dim dVisitID As New DataColumn("VisitID", GetType(Long))

            Dim dSummaryCheckBox As New DataColumn("SummaryCheckBox", GetType(Boolean))
            Dim dMedicationCheckBox As New DataColumn("MedicationCheckBox", GetType(Boolean))
            Dim dMedDate As New DataColumn("MedDate", GetType(DateTime))
            Dim dNotes As New DataColumn("Notes", GetType(String))
            Dim dRowState As New DataColumn("RowState", GetType(String))
            Dim dReconcillationType As New DataColumn("ReconcillationType", GetType(Int16))

            dtPHistoryMedRec.Columns.Add(dPatientID)
            dtPHistoryMedRec.Columns.Add(dVisitID)

            dtPHistoryMedRec.Columns.Add(dSummaryCheckBox)
            dtPHistoryMedRec.Columns.Add(dMedicationCheckBox)
            dtPHistoryMedRec.Columns.Add(dMedDate)
            dtPHistoryMedRec.Columns.Add(dNotes)
            dtPHistoryMedRec.Columns.Add(dRowState)
            dtPHistoryMedRec.Columns.Add(dReconcillationType)



            dtPHistoryMedRec = (GetPHistoryMedicationReconcillationDetails(m_VisitID, m_PatientID, nReconcillationType))
            If dtPHistoryMedRec.Rows.Count > 0 Then
                If (dtPHistoryMedRec.Rows(0)("MedicationCheckBox") = True) Then

                End If
            End If
            If (Not IsNothing(dsHistory)) Then
                If (dsHistory.Tables.Contains("GetCategory")) Then
                    dtcategoryType = dsHistory.Tables("GetCategory").Copy()
                End If
            End If





        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            ChkResolvedEnddate.Enabled = False
            Set_RecordLock(_blnRecordLock)
            Me.ResumeLayout(True)
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Open, "History Form Opened", m_PatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)

        End Try
    End Sub

    Public Sub CheckReview()
        Dim oDB As New gloStream.gloDataBase.gloDataBase
        Try
            Dim strSelect As String = "SELECT ReviewHistory.nUserID,ReviewHistory.sComments,ReviewHistory.dtReviewDate,User_MST.sLoginName FROM ReviewHistory INNER JOIN User_MST ON ReviewHistory.nUserID = User_MST.nUserID where ReviewHistory.nPatientID= " & m_PatientID & " Order By ReviewHistory.dtReviewDate DESC"

            '19-Apr-13 Aniket: Resolving Memory Leak Issues

            'If IsNothing(dtReview) = True Then
            '    dtReview = New DataTable
            'End If


            oDB.Connect(GetConnectionString)

            '19-Apr-13 Aniket: Resolving Memory Leak Issues
            If IsNothing(dtReview) = False Then
                dtReview.Dispose()
                dtReview = Nothing
            End If

            dtReview = oDB.ReadQueryDataTable(strSelect)
            oDB.Disconnect()

            If IsNothing(dtReview) = False Then
                If dtReview.Rows.Count > 0 Then
                    tlbViewReview.Visible = True
                Else
                    tlbViewReview.Visible = False
                End If
            Else
                tlbViewReview.Visible = False
            End If

        Catch ex As Exception

        Finally

            '19-Apr-13 Aniket: Resolving Memory Leak Issues
            If IsNothing(dtReview) = False Then
                dtReview.Dispose()
                dtReview = Nothing
            End If

            If IsNothing(oDB) = False Then
                oDB.Dispose() : oDB = Nothing
            End If
        End Try

    End Sub

    Private Function CheckHistoryType(ByVal HistoryType As String, ByVal strCategory As String) As String
        Dim IsOnsetDate As Boolean = False
        Dim IsActive As Boolean = False
        Dim strOnsetActiveStatus As String = ""
        If IsNothing(dsHistory.Tables("HistoryTypes")) = False Then
            If dsHistory.Tables("HistoryTypes").Rows.Count > 0 Then
                For h As Integer = 0 To dsHistory.Tables("HistoryTypes").Rows.Count - 1
                    If HistoryType <> "" Then
                        If dsHistory.Tables("HistoryTypes").Rows(h)(Col_Standard_HistoryType).ToString().Trim = HistoryType Then
                            If Convert.ToBoolean(dsHistory.Tables("HistoryTypes").Rows(h)(Col_Standard_bIsOnsetDate)) = True Then
                                IsOnsetDate = True

                            End If
                            If Convert.ToBoolean(dsHistory.Tables("HistoryTypes").Rows(h)(Col_Standard_bIsActive)) = True Then
                                IsActive = True
                            End If
                            Exit For
                        End If
                    Else
                        If IsNothing(dsHistory.Tables("CategoryType")) = False Then
                            If dsHistory.Tables("CategoryType").Rows.Count > 0 Then
                                For r As Integer = 0 To dsHistory.Tables("CategoryType").Rows.Count - 1
                                    If dsHistory.Tables("CategoryType").Rows(r)(Col_Category_sHistoryType).ToString().Trim = strCategory Then
                                        For m As Integer = 0 To dsHistory.Tables("HistoryTypes").Rows.Count - 1
                                            If dsHistory.Tables("HistoryTypes").Rows(m)(Col_Standard_HistoryType).ToString().Trim = strCategory Then
                                                If Convert.ToBoolean(dsHistory.Tables("HistoryTypes").Rows(m)(Col_Standard_bIsOnsetDate)) = True Then
                                                    IsOnsetDate = True
                                                End If
                                                If Convert.ToBoolean(dsHistory.Tables("HistoryTypes").Rows(m)(Col_Standard_bIsActive)) = True Then
                                                    IsActive = True
                                                End If
                                                strOnsetActiveStatus = IsOnsetDate & "," & IsActive
                                                Return strOnsetActiveStatus
                                            End If

                                        Next

                                    End If
                                Next
                            End If
                        End If
                    End If


                Next
            End If
        End If
        strOnsetActiveStatus = IsOnsetDate & "," & IsActive
        Return strOnsetActiveStatus
    End Function
    Private Function getHistoryTypefromcategorymaster(ByVal _categoryType As String)
        Dim _CategoryTypeFromMaster As String = ""
        If IsNothing(dsHistory.Tables("CategoryType")) = False Then

            If dsHistory.Tables("CategoryType").Rows.Count > 0 Then


                For i As Integer = 0 To dsHistory.Tables("CategoryType").Rows.Count - 1
                    If dsHistory.Tables("CategoryType").Rows(i)(Col_Category_sDescription) = _categoryType Then
                        _CategoryTypeFromMaster = dsHistory.Tables("CategoryType").Rows(i)(Col_Category_sHistoryType)
                        Return _CategoryTypeFromMaster
                    End If
                Next
                Return ""
            End If
        End If
        Return ""
    End Function
    Private Function CheckHistoryTypeinStandardTable(ByVal CategoryType As String) As String
        Dim IsOnsetDate As Boolean = False
        Dim IsActive As Boolean = False
        Dim strOnsetActiveStatus As String = ""
        If IsNothing(dsHistory.Tables("HistoryTypes")) = False Then
            If dsHistory.Tables("HistoryTypes").Rows.Count > 0 Then
                For h As Integer = 0 To dsHistory.Tables("HistoryTypes").Rows.Count - 1

                    If dsHistory.Tables("HistoryTypes").Rows(h)("sShortDescription").ToString().Trim = CategoryType Then
                        If Convert.ToBoolean(dsHistory.Tables("HistoryTypes").Rows(h)(Col_Standard_bIsOnsetDate)) = True Then
                            IsOnsetDate = True

                        End If
                        If Convert.ToBoolean(dsHistory.Tables("HistoryTypes").Rows(h)(Col_Standard_bIsActive)) = True Then
                            IsActive = True
                        End If
                        Exit For
                    End If




                Next
            End If
        End If
        strOnsetActiveStatus = IsOnsetDate & "," & IsActive
        Return strOnsetActiveStatus
    End Function
    Private Function getHistoryTypefromcategorymaster_Status(ByVal _categoryType As String, ByVal dsStatus As DataSet)
        If IsNothing(dsStatus) = False Then


            Dim _CategoryTypeFromMaster As String = ""
            If IsNothing(dsStatus.Tables("CategoryType")) = False Then

                If dsStatus.Tables("CategoryType").Rows.Count > 0 Then

                    For i As Integer = 0 To dsStatus.Tables("CategoryType").Rows.Count - 1
                        If dsStatus.Tables("CategoryType").Rows(i)(Col_Category_sDescription) = _categoryType Then
                            _CategoryTypeFromMaster = dsStatus.Tables("CategoryType").Rows(i)(Col_Category_sHistoryType)
                            Return _CategoryTypeFromMaster
                        End If
                    Next
                End If
            End If
        End If
        Return ""
    End Function
    Private Function CheckHistoryTypeinStandardTable_status(ByVal CategoryType As String, ByVal dsStatus As DataSet) As String
        Dim IsOnsetDate As Boolean = False
        Dim IsActive As Boolean = False
        Dim strOnsetActiveStatus As String = ""
        If IsNothing(dsStatus) = False Then


            If IsNothing(dsStatus.Tables("HistoryTypes")) = False Then
                If dsStatus.Tables("HistoryTypes").Rows.Count > 0 Then
                    For h As Integer = 0 To dsStatus.Tables("HistoryTypes").Rows.Count - 1

                        If dsStatus.Tables("HistoryTypes").Rows(h)("sShortDescription").ToString().Trim = CategoryType Then
                            If Convert.ToBoolean(dsStatus.Tables("HistoryTypes").Rows(h)(Col_Standard_bIsOnsetDate)) = True Then
                                IsOnsetDate = True

                            End If
                            If Convert.ToBoolean(dsStatus.Tables("HistoryTypes").Rows(h)(Col_Standard_bIsActive)) = True Then
                                IsActive = True
                            End If
                            Exit For
                        End If




                    Next
                End If
            End If
        End If
        strOnsetActiveStatus = IsOnsetDate & "," & IsActive
        Return strOnsetActiveStatus
    End Function

    Public Sub PopulatePatientHistory_Final()
        ''Added  by MAYURI:20120913- Added onset date column
        C1HistoryDetails.Cols.Count = 51
        btnConceptID.Enabled = False
        btn_CPTCode.Enabled = False
        btn_ICD9Code.Enabled = False
        Label59SNOMEDRefusedCode.Enabled = False
        btnClearSNOMEDRefusedCode.Enabled = False
        btnbrloinc.Enabled = False
        btnclloinc.Enabled = False
        btnBrowseCqm.Enabled = False
        btnClearCqm.Enabled = False
        btnBrowseUDI.Enabled = False
        cmbStatus.Enabled = False
        cmbAllergySeverity.Enabled = False
        cmbAllergyIntelorenceType.Enabled = False
        btnCVXCode.Enabled = False
        btnClsCVXCode.Enabled = False
        C1HistoryDetails.Cols(col_HOnsetDate).AllowEditing = True
        C1HistoryDetails.Cols(Col_DateResolved).AllowEditing = True
        C1HistoryDetails.Cols(Col_ResolvedEndDate).AllowEditing = True
        isHistoryLoading = True
        lblReviewed.Visible = False
        setStyleGridWidth()

        C1HistoryDetails.Rows.Count = 1


        Dim ds As DataSet = Nothing

        '19-Apr-13 Aniket: Resolving Memory Leak Issues
        Dim dtPatientsHistory As DataTable = Nothing
        ''slr free prev memory
        If Not IsNothing(_dsTemp) Then
            _dsTemp.Dispose()
            _dsTemp = Nothing
        End If
        _dsTemp = New DataSet ''Deleted rows collection
        Try

            dsHistory = objclsPatientHistory.GetHistory_optimize(m_PatientID, 0, m_VisitDate, "History", 1, m_VisitID, _IsOBHistory)

            dsHistory.Tables("History").Columns("nHistoryID").AutoIncrement = True
            dsHistory.Tables("History").Columns("nHistoryID").AutoIncrementSeed = 1

            dsHistory.Tables("History").Columns.Add("Hidden").SetOrdinal(1)
            dsHistory.Tables("History").Columns.Add("Button").SetOrdinal(5)
            dsHistory.Tables("History").Columns.Add("SmokingStatus").SetOrdinal(6)
            dsHistory.Tables("History").Columns.Add("SmokeButton").SetOrdinal(7)
            dsHistory.Tables("History").Columns.Add("sActive").SetOrdinal(8)

            dsHistory.Tables("History").Columns.Add("RowID")
            dsHistory.Tables("History").Columns.Add("nMemberId")
            ds = dsHistory.Copy()
            dsHistory.Tables("History").Clear()
            ' _dsTemp = New DataSet
            ''Added  by MAYURI:20120913- Deleted rows collection
            _dsTemp.Tables.Add("History")
            _dsTemp.Tables(0).Merge(dsHistory.Tables("History").Copy)

            Dim IsOnsetDate As Boolean = False
            Dim IsActive As Boolean = False
            Dim stronsetActiveStatus As String = ""
            ' Dim _arrOnsetActive() As String
            Dim strCategory As String
            Dim strHistory As String
            Dim strComment As String
            ' Dim strReaction As String
            '  Dim strActive As String
            Dim strRection_Status As String
            Dim intDrugID As Long
            Dim intMedicalConditionId As Long ' variable added by sagar to fill te medical condition id
            'For History Denormalization
            Dim strHxDrugName As String
            Dim strHxDosage As String
            Dim strHxNDCCode As String
            Dim intHxMPID As Int32
            ''Added Rahul on 20101004
            Dim strDOE_Allergy As String
            Dim sConCeptID As String
            Dim sDescriptionID As String
            Dim sSnoMedId As String
            Dim sSnoDescription As String
            Dim sICD9 As String
            Dim sHistoryType As String

            Dim sRefusalCode As String = ""
            Dim sRefusalDesc As String = ""
            Dim sLoincCode As String = ""
            Dim sLoincDescr As String = ""
            Dim sValueSetOID As String = ""
            Dim sValueSetName As String = ""
            Dim nDeviceListID As Long = 0
            Dim sProcStatus As String = ""

            '29-Mar-13 Aniket: Addition of source column on the History screen
            Dim sHistorySource As String
            Dim nICDRevision As Int16 = 0 ''added for ICD10 implementation
            Dim CPT As String
            Dim sRxNormID As String
            Dim nHistoryID As Long
            ''

            Dim strOnsetDate As String
            Dim strResolvedEndDate As String ''added for 2015 certification
            Dim nRowOrder As Int64

            Dim sAllergyClsID As String = "" ''added for 2015 certification

            Dim sAllergySeverity As String = "" ''added for 2015 certification
            Dim sAllergyIntelorenceCode As String = "" ''added for 2015 certification
            'For History Denormalization
            ''Fire For Add Mode & Edit Mode
            Dim CVXCode As String = ""
            Dim CVXDesc As String = ""
            If FormLevelLock() = False Then
                Exit Sub
            End If

            If ds.Tables("History").Rows.Count > 0 Then
                '' current history
                Dim visitid As Long
                visitid = CType(ds.Tables("History").Rows.Item(0).Item(9), Long)
                _HxVisitID = visitid
                lblVisitDate.Tag = visitid
                lblVisitDate.Text = CType(ds.Tables("History").Rows.Item(0).Item(10), DateTime).Date

                Dim strdtReviewDate As String
                ''dtReview=  objclsPatientHistory.GetReviewHistory(m_PatientID, visitid)
                dtReview = dsHistory.Tables("ReviewHistory")
                If IsNothing(dtReview) = False Then
                    If dtReview.Rows.Count > 0 Then
                        strdtReviewDate = dtReview.Rows(0)("dtReviewDate")
                        lblReviewed.Visible = True
                        lblReviewed.Text = "History Reviewed By " + gstrLoginName + " on " + strdtReviewDate
                        intCheck = 1
                    Else
                        lblReviewed.Visible = False
                    End If
                Else
                    lblReviewed.Visible = False
                End If
                lblVisitDate.Tag = visitid
                For Each drHistory As DataRow In ds.Tables("History").Rows
                    strCategory = drHistory(HistoryEnum.sHistoryCategory)
                    strHistory = drHistory(HistoryEnum.sHistoryItem)
                    strComment = drHistory(HistoryEnum.sComments)
                    strRection_Status = drHistory(HistoryEnum.sReaction)
                    intDrugID = drHistory(HistoryEnum.nDrugID)
                    intMedicalConditionId = drHistory(HistoryEnum.MedicalCondition_Id) 'return the medical condition id
                    'For History Denormalization
                    strHxDrugName = drHistory(HistoryEnum.sDrugName)
                    strHxDosage = drHistory(HistoryEnum.sDosage)
                    strHxNDCCode = drHistory(HistoryEnum.sNDCCode)
                    intHxMPID = drHistory(HistoryEnum.mpid)
                    strDOE_Allergy = Convert.ToString(drHistory(HistoryEnum.DOE_Allergy)).Replace("NULL", "")
                    sConCeptID = Convert.ToString(drHistory(HistoryEnum.sConceptID)).Replace("NULL", "")
                    sDescriptionID = Convert.ToString(drHistory(HistoryEnum.sDescriptionID)).Replace("NULL", "")
                    sSnoMedId = Convert.ToString(drHistory(HistoryEnum.sSnoMedID)).Replace("NULL", "")
                    sSnoDescription = Convert.ToString(drHistory(HistoryEnum.sDescription)).Replace("NULL", "")
                    sICD9 = drHistory(HistoryEnum.sICD9)
                    ''''Added  by MAYURI:20120913- Added onset date,CPT,HistoryType,RowOrder column
                    ''RowOrder is for mainttaing track of items move up and down
                    CPT = drHistory(HistoryEnum.CPT)
                    sRxNormID = drHistory(HistoryEnum.sRxNormID)
                    nHistoryID = drHistory(HistoryEnum.nHistoryID)
                    strOnsetDate = Convert.ToString(drHistory(HistoryEnum.OnsetDate)).Replace("NULL", "")
                    nRowOrder = drHistory(HistoryEnum.nRowOrder)
                    sHistoryType = Convert.ToString(drHistory(HistoryEnum.sHistoryType)).Replace("NULL", "")

                    '29-Mar-13 Aniket: Addition of source column on the History screen
                    sHistorySource = Convert.ToString(drHistory("sHistorySource")).Replace("NULL", "")
                    nICDRevision = Convert.ToInt16(drHistory("nICDRevision")) ''added for ICD10 implementation
                    ' strDateResolved = Convert.ToString(drHistory(HistoryEnum.DateResolved)).Replace("NULL", "")
                    'For History Denormalization
                    ''''''''

                    sRefusalCode = Convert.ToString(drHistory("sRefusalCode")).Replace("NULL", "")
                    sRefusalDesc = Convert.ToString(drHistory("sRefusalDesc")).Replace("NULL", "")

                    sLoincCode = Convert.ToString(drHistory("sLoincCode")).Replace("NULL", "")
                    sLoincDescr = Convert.ToString(drHistory("sLoincDescr")).Replace("NULL", "")


                    sAllergyClsID = Convert.ToString(drHistory("AllergyClassID")).Replace("NULL", "") ''added for 2015 certification

                    sValueSetOID = Convert.ToString(drHistory("sValueSetOID")).Replace("NULL", "")
                    sValueSetName = Convert.ToString(drHistory("sValueSetName")).Replace("NULL", "")
                    nDeviceListID = Convert.ToInt64(drHistory("nDeviceList_ID"))
                    sProcStatus = Convert.ToString(drHistory("sProcStatus"))

                    If Convert.ToString(drHistory("AllergySeverity")).Replace("NULL", "") <> "0" Then
                        sAllergySeverity = Convert.ToString(drHistory("AllergySeverity")).Replace("NULL", "") ''added for 2015 certification
                    Else
                        sAllergySeverity = ""
                    End If

                    strResolvedEndDate = Convert.ToString(drHistory("dtObservationEndDate")).Replace("NULL", "") ''added for 2015 certification

                    sAllergyIntelorenceCode = Convert.ToString(drHistory("sAllergyIntelorenceCode")).Replace("NULL", "")
                    CVXCode = Convert.ToString(drHistory("CVXCode")).Replace("NULL", "")
                    CVXDesc = Convert.ToString(drHistory("CVXDesc")).Replace("NULL", "")

                    ''''''''
                    '29-Mar-13 Aniket: Addition of source column on the History screen
                    ''ICDRevision added for ICD10 implementation
                    Call FillGrid(strCategory, strHistory, strComment, strRection_Status, intDrugID, intMedicalConditionId, strDOE_Allergy, sConCeptID, sDescriptionID, sSnoMedId, sSnoDescription, sRxNormID, "", "", sICD9, "", strHxNDCCode, strHxDosage, intHxMPID, nHistoryID, strOnsetDate, CPT, nRowOrder, sHistoryType, sHistorySource, nICDRevision, sRefusalCode, sRefusalDesc, sLoincCode, sLoincDescr, sAllergyClsID, sValueSetOID, sValueSetName, nDeviceListID, sProcStatus, sAllergySeverity, ResolvedEndDate:=strResolvedEndDate, sAllergyIntelorenceCode:=sAllergyIntelorenceCode, CVxCode:=CVXCode, CVXDesc:=CVXDesc)
                Next

                C1HistoryDetails.Select(1, 0, 1, 1, True)
                gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.View, "Patient History Viewed", gloAuditTrail.ActivityOutCome.Success)
                PopulateNarration()
                blncancel = True
                blnModify = True
                ShowPendingReconcileMessage()
            Else
                ''Fire oNly For Add Mode Not For Edit Mode



                '' dsHistory = objclsPatientHistory.GetHistory_New(m_PatientID, 1, m_VisitDate, "History", 1, m_VisitID)

                '19-Apr-13 Aniket: Resolving Memory Leak Issues
                dtPatientsHistory = objclsPatientHistory.GetHistory(m_PatientID, 1, m_VisitDate)

                Dim blnCloseHistory As Boolean
                'if record present againt y'day or before then show msgbox
                If dtPatientsHistory.Rows.Count > 0 Then
                    If blnShowMessageBox = True And _blnRecordLock = False Then
                        'Old Message

                        Dim dtName As DataTable
                        dtName = objclsPatientHistory.GetPatName(m_PatientID)

                        'If MessageBox.Show("History for <Patient Name> has not been reviewed for today’s visit.  Would you like to review the patient’s history at this time" & Convert.ToDateTime(dt.Rows(0)("dtvisitdate")).Date & ".  Update patient history now?  ", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.Yes Then
                        'New Message Suggested by Drew
                        Dim strMessage As String
                        strMessage = "History for '" & dtName.Rows(0)("PatientName") & "' has not been reviewed for today’s visit. Would you like to review the patient’s history at this time? " & vbNewLine & vbNewLine & "YES - Review history now.  Previous history from '" & Convert.ToDateTime(dtPatientsHistory.Rows(0)("dtvisitDate")).ToShortDateString() & "' will be copied forward to this visit to be verified. " & vbNewLine & vbNewLine & "NO  - Do not review history now."

                        If MessageBox.Show(strMessage, gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.Yes Then
                            ShowPendingReconcileMessage()
                            blnCloseHistory = False
                            lblReviewed.Visible = False
                            Dim visitid As Long
                            visitid = CType(dtPatientsHistory.Rows.Item(0).Item(0), Long)
                            _HxVisitID = visitid
                            lblVisitDate.Tag = 0
                            lblVisitDate.Text = Now.Date

                            '19-Apr-13 Aniket: Resolving Memory Leak Issues
                            If IsNothing(dtPatientsHistory) = False Then
                                dtPatientsHistory.Dispose()
                                dtPatientsHistory = Nothing
                            End If


                            dsHistory.Tables("History").Clear()
                            dsHistory = objclsPatientHistory.GetHistory_optimize(m_PatientID, 2, m_VisitDate, "History", 1, visitid, _IsOBHistory)
                            dsHistory.Tables("History").Columns.Add("Hidden").SetOrdinal(1)
                            dsHistory.Tables("History").Columns.Add("Button").SetOrdinal(5)
                            dsHistory.Tables("History").Columns.Add("SmokingStatus").SetOrdinal(6)
                            dsHistory.Tables("History").Columns.Add("SmokeButton").SetOrdinal(7)
                            dsHistory.Tables("History").Columns.Add("sActive").SetOrdinal(8)
                            dsHistory.Tables("History").Columns.Add("RowID")
                            dsHistory.Tables("History").Columns.Add("nMemberId")
                            'dsHistory.Tables("History").Columns.Add("ICD9Button").SetOrdinal(25)
                            'dsHistory.Tables("History").Columns.Add("CPTButton").SetOrdinal(27)
                            '  dsHistory.AcceptChanges()
                            ds = dsHistory.Copy()
                            dsHistory.Tables("History").Clear()
                            dsHistory.AcceptChanges()
                            If IsNothing(ds.Tables("History")) = False Then
                                If ds.Tables("History").Rows.Count > 0 Then

                                    ''Use datarow for performance
                                    For Each drHistory As DataRow In ds.Tables("History").Rows
                                        strCategory = drHistory(HistoryEnum.sHistoryCategory)
                                        strHistory = drHistory(HistoryEnum.sHistoryItem)
                                        strComment = drHistory(HistoryEnum.sComments)
                                        strRection_Status = drHistory(HistoryEnum.sReaction)
                                        intDrugID = drHistory(HistoryEnum.nDrugID)
                                        intMedicalConditionId = drHistory(HistoryEnum.MedicalCondition_Id) 'return the medical condition id
                                        'For History Denormalization
                                        strHxDrugName = drHistory(HistoryEnum.sDrugName)
                                        strHxDosage = drHistory(HistoryEnum.sDosage)
                                        strHxNDCCode = drHistory(HistoryEnum.sNDCCode)
                                        intHxMPID = drHistory(HistoryEnum.mpid)
                                        strDOE_Allergy = Convert.ToString(drHistory(HistoryEnum.DOE_Allergy)).Replace("NULL", "")
                                        sConCeptID = Convert.ToString(drHistory(HistoryEnum.sConceptID)).Replace("NULL", "")
                                        sDescriptionID = Convert.ToString(drHistory(HistoryEnum.sDescriptionID)).Replace("NULL", "")
                                        sSnoMedId = Convert.ToString(drHistory(HistoryEnum.sSnoMedID)).Replace("NULL", "")
                                        sSnoDescription = Convert.ToString(drHistory(HistoryEnum.sDescription)).Replace("NULL", "")
                                        sICD9 = drHistory(HistoryEnum.sICD9)
                                        CPT = drHistory(HistoryEnum.CPT)
                                        sRxNormID = drHistory(HistoryEnum.sRxNormID)
                                        nHistoryID = drHistory(HistoryEnum.nHistoryID)
                                        strOnsetDate = Convert.ToString(drHistory(HistoryEnum.OnsetDate)).Replace("NULL", "")
                                        nRowOrder = drHistory(HistoryEnum.nRowOrder)
                                        sHistoryType = Convert.ToString(drHistory(HistoryEnum.sHistoryType)).Replace("NULL", "")

                                        '29-Mar-13 Aniket: Addition of source column on the History screen
                                        sHistorySource = Convert.ToString(drHistory("sHistorySource")).Replace("NULL", "")

                                        nICDRevision = Convert.ToInt16(drHistory("nICDRevision"))   ''added for ICD10 implementation
                                        sRefusalCode = Convert.ToString(drHistory("sRefusalCode")).Replace("NULL", "")
                                        sRefusalDesc = Convert.ToString(drHistory("sRefusalDesc")).Replace("NULL", "")
                                        ''added loinc code and descr for bugid 106176
                                        sLoincCode = Convert.ToString(drHistory("sLoincCode")).Replace("NULL", "")
                                        sLoincDescr = Convert.ToString(drHistory("sLoincDescr")).Replace("NULL", "")


                                        sAllergyClsID = Convert.ToString(drHistory("AllergyClassID")).Replace("NULL", "") ''added for 2015 certification
                                        sValueSetOID = Convert.ToString(drHistory("sValueSetOID")).Replace("NULL", "")
                                        sValueSetName = Convert.ToString(drHistory("sValueSetName")).Replace("NULL", "")
                                        nDeviceListID = Convert.ToInt64(drHistory("nDeviceList_ID"))
                                        sProcStatus = Convert.ToString(drHistory("sProcStatus"))

                                        sAllergySeverity = Convert.ToString(drHistory("AllergySeverity")).Replace("NULL", "") ''added for 2015 certification
                                        strResolvedEndDate = Convert.ToString(drHistory("dtObservationEndDate")).Replace("NULL", "") ''added for 2015 certification
                                        sAllergyIntelorenceCode = Convert.ToString(drHistory("sAllergyIntelorenceCode")).Replace("NULL", "") ''added for 2015 certification
                                        CVXCode = Convert.ToString(drHistory("CVXCode")).Replace("NULL", "")
                                        CVXDesc = Convert.ToString(drHistory("CVXDesc")).Replace("NULL", "")
                                        Call FillGrid(strCategory, strHistory, strComment, strRection_Status, intDrugID, intMedicalConditionId, strDOE_Allergy, sConCeptID, sDescriptionID, sSnoMedId, sSnoDescription, sRxNormID, "", "", sICD9, "", strHxNDCCode, strHxDosage, intHxMPID, nHistoryID, strOnsetDate, CPT, nRowOrder, sHistoryType, sHistorySource, nICDRevision, sRefusalCode, sRefusalDesc, sLoincCode:=sLoincCode, sLoincDescr:=sLoincDescr, AllergyClsId:=sAllergyClsID, sValueSetOID:=sValueSetOID, sValueSetName:=sValueSetName, nDeviceList_ID:=nDeviceListID, sProcStatus:=sProcStatus, AllergySeverity:=sAllergySeverity, ResolvedEndDate:=strResolvedEndDate, sAllergyIntelorenceCode:=sAllergyIntelorenceCode, CVxCode:=CVXCode, CVXDesc:=CVXDesc)
                                    Next

                                    C1HistoryDetails.Select(1, 0, 1, 1, True)
                                    _isMedicationHistoryModify = True
                                    Dim l As Int16
                                    For l = 0 To dsHistory.Tables("History").Rows.Count - 1
                                        If dsHistory.Tables("History").Rows(l).RowState <> DataRowState.Deleted Then
                                            dsHistory.Tables("History").Rows(l)("RowState") = "Added"
                                        End If
                                    Next


                                    PopulateNarration()
                                    blncancel = True
                                    blnModify = False
                                End If
                            End If

                            dtName.Dispose()
                            dtName = Nothing

                        Else
                            ''Added by MAYURI:20120730-Word crash issue:From nurse notes
                            If IsNothing(txtSMSearch) = False Then
                                txtSMSearch.Dispose()
                                txtSMSearch = Nothing
                            End If
                            If Not IsNothing(gloUC_PatientStrip1) Then
                                'If (pnlOuter.Controls.Contains(gloUC_PatientStrip1)) Then
                                '    pnlOuter.Controls.Remove(gloUC_PatientStrip1)
                                'End If
                                gloUC_PatientStrip1.Dispose()
                                gloUC_PatientStrip1 = Nothing
                            End If
                            If IsNothing(GloUC_trvHistory) = False Then
                                GloUC_trvHistory.Dispose()
                                GloUC_trvHistory = Nothing
                            End If
                            If IsNothing(tblHistory) = False Then
                                tblHistory.Dispose()
                                tblHistory = Nothing
                            End If
                            ''To Delete FormLevel Locking user dnt want to open the this form
                            If FormLevelLockID > 0 Then
                                Delete_Lock_FormLevel(FormLevelLockID, m_PatientID)
                            End If

                        End If


                    Else ''If blnShowMessage is false, means don't show the message box and take action of "Yes" click
                        ''Sanjog added on 2011 Nov.3 - To Shows another message only for button click suggested by Drew
                        If _blnRecordLock = False And blnShowAddModeMessageBox = True Then
                            Dim dtName As DataTable
                            dtName = objclsPatientHistory.GetPatName(m_PatientID)
                            MessageBox.Show("History for '" & dtName.Rows(0)("PatientName") & "' has not been reviewed for today’s visit. History from '" & Convert.ToDateTime(dtPatientsHistory.Rows(0)("dtvisitDate")).ToShortDateString() & "' will be copied forward to this visit to be verified.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2)
                            dtName.Dispose()
                            dtName = Nothing
                        End If
                        ''Sanjog added on 2011 Nov.3 - To Shows another message only for button click suggested by Drew
                        ShowPendingReconcileMessage()
                        blnCloseHistory = False
                        lblReviewed.Visible = False
                        Dim visitid As Long
                        visitid = CType(dtPatientsHistory.Rows.Item(0).Item(0), Long)
                        _HxVisitID = visitid
                        lblVisitDate.Tag = 0
                        lblVisitDate.Text = Now.Date

                        '19-Apr-13 Aniket: Resolving Memory Leak Issues
                        If IsNothing(dtPatientsHistory) = False Then
                            dtPatientsHistory.Dispose()
                            dtPatientsHistory = Nothing
                        End If


                        dsHistory.Tables("History").Clear()
                        dsHistory = objclsPatientHistory.GetHistory_optimize(m_PatientID, 2, m_VisitDate, "History", 1, visitid, _IsOBHistory)
                        dsHistory.Tables("History").Columns.Add("Hidden").SetOrdinal(1)
                        dsHistory.Tables("History").Columns.Add("Button").SetOrdinal(5)
                        dsHistory.Tables("History").Columns.Add("SmokingStatus").SetOrdinal(6)
                        dsHistory.Tables("History").Columns.Add("SmokeButton").SetOrdinal(7)
                        dsHistory.Tables("History").Columns.Add("sActive").SetOrdinal(8)
                        dsHistory.Tables("History").Columns.Add("RowID")
                        dsHistory.Tables("History").Columns.Add("nMemberId")
                        'dsHistory.Tables("History").Columns.Add("ICD9Button").SetOrdinal(25)
                        'dsHistory.Tables("History").Columns.Add("CPTButton").SetOrdinal(27)
                        '  dsHistory.AcceptChanges()
                        ds = dsHistory.Copy()
                        dsHistory.Tables("History").Clear()
                        dsHistory.AcceptChanges()
                        If IsNothing(ds.Tables("History")) = False Then
                            If ds.Tables("History").Rows.Count > 0 Then



                                ''Use datarow for performance
                                For Each drHistory As DataRow In ds.Tables("History").Rows
                                    strCategory = drHistory(HistoryEnum.sHistoryCategory)
                                    strHistory = drHistory(HistoryEnum.sHistoryItem)
                                    strComment = drHistory(HistoryEnum.sComments)
                                    strRection_Status = drHistory(HistoryEnum.sReaction)
                                    intDrugID = drHistory(HistoryEnum.nDrugID)
                                    intMedicalConditionId = drHistory(HistoryEnum.MedicalCondition_Id) 'return the medical condition id
                                    'For History Denormalization
                                    strHxDrugName = drHistory(HistoryEnum.sDrugName)
                                    strHxDosage = drHistory(HistoryEnum.sDosage)
                                    strHxNDCCode = drHistory(HistoryEnum.sNDCCode)
                                    intHxMPID = drHistory(HistoryEnum.mpid)
                                    strDOE_Allergy = Convert.ToString(drHistory(HistoryEnum.DOE_Allergy)).Replace("NULL", "")
                                    sConCeptID = Convert.ToString(drHistory(HistoryEnum.sConceptID)).Replace("NULL", "")
                                    sDescriptionID = Convert.ToString(drHistory(HistoryEnum.sDescriptionID)).Replace("NULL", "")
                                    sSnoMedId = Convert.ToString(drHistory(HistoryEnum.sSnoMedID)).Replace("NULL", "")
                                    sSnoDescription = Convert.ToString(drHistory(HistoryEnum.sDescription)).Replace("NULL", "")
                                    sICD9 = drHistory(HistoryEnum.sICD9)
                                    CPT = drHistory(HistoryEnum.CPT)
                                    sRxNormID = drHistory(HistoryEnum.sRxNormID)
                                    nHistoryID = drHistory(HistoryEnum.nHistoryID)
                                    strOnsetDate = Convert.ToString(drHistory(HistoryEnum.OnsetDate)).Replace("NULL", "")
                                    nRowOrder = drHistory(HistoryEnum.nRowOrder)
                                    sHistoryType = Convert.ToString(drHistory(HistoryEnum.sHistoryType)).Replace("NULL", "")
                                    '29-Mar-13 Aniket: Addition of source column on the History screen
                                    sHistorySource = Convert.ToString(drHistory("sHistorySource")).Replace("NULL", "")
                                    nICDRevision = Convert.ToInt16(drHistory("nICDRevision"))    ''added for ICD10 implementation
                                    sRefusalCode = Convert.ToString(drHistory("sRefusalCode")).Replace("NULL", "")
                                    sRefusalDesc = Convert.ToString(drHistory("sRefusalDesc")).Replace("NULL", "")
                                    sLoincCode = Convert.ToString(drHistory("sLoincCode")).Replace("NULL", "")
                                    sLoincDescr = Convert.ToString(drHistory("sLoincDescr")).Replace("NULL", "")
                                    sAllergyClsID = Convert.ToString(drHistory("AllergyClassID")).Replace("NULL", "")
                                    sValueSetOID = Convert.ToString(drHistory("sValueSetOID")).Replace("NULL", "")
                                    sValueSetName = Convert.ToString(drHistory("sValueSetName")).Replace("NULL", "") ''added for 2015 certification
                                    nDeviceListID = Convert.ToInt64(drHistory("nDeviceList_ID"))
                                    sProcStatus = Convert.ToString(drHistory("sProcStatus"))
                                    sAllergySeverity = Convert.ToString(drHistory("AllergySeverity")).Replace("NULL", "")
                                    strResolvedEndDate = Convert.ToString(drHistory("dtObservationEndDate")).Replace("NULL", "")
                                    sAllergyIntelorenceCode = Convert.ToString(drHistory("sAllergyIntelorenceCode")).Replace("NULL", "")
                                    CVXCode = Convert.ToString(drHistory("CVXCode")).Replace("NULL", "")
                                    CVXDesc = Convert.ToString(drHistory("CVXDesc")).Replace("NULL", "")
                                    Call FillGrid(strCategory, strHistory, strComment, strRection_Status, intDrugID, intMedicalConditionId, strDOE_Allergy, sConCeptID, sDescriptionID, sSnoMedId, sSnoDescription, sRxNormID, "", "", sICD9, "", strHxNDCCode, strHxDosage, intHxMPID, nHistoryID, strOnsetDate, CPT, nRowOrder, sHistoryType, sHistorySource, nICDRevision, sRefusalCode, sRefusalDesc, sLoincCode:=sLoincCode, sLoincDescr:=sLoincDescr, AllergyClsId:=sAllergyClsID, sValueSetOID:=sValueSetOID, sValueSetName:=sValueSetName, nDeviceList_ID:=nDeviceListID, sProcStatus:=sProcStatus, AllergySeverity:=sAllergySeverity, ResolvedEndDate:=strResolvedEndDate, sAllergyIntelorenceCode:=sAllergyIntelorenceCode, CVxCode:=CVXCode, CVXDesc:=CVXDesc)
                                Next

                                C1HistoryDetails.Select(1, 0, 1, 1, True)
                                _isMedicationHistoryModify = True
                                Dim l As Int16
                                For l = 0 To dsHistory.Tables("History").Rows.Count - 1
                                    If dsHistory.Tables("History").Rows(l).RowState <> DataRowState.Deleted Then
                                        dsHistory.Tables("History").Rows(l)("RowState") = "Added"
                                    End If
                                Next


                                PopulateNarration()
                                blncancel = True
                                blnModify = False
                            End If
                        End If
                    End If
                Else
                    ShowPendingReconcileMessage()
                    dsHistory.AcceptChanges()
                    C1HistoryDetails.SetDataBinding(dsHistory, "History")
                    'End If
                    blncancel = True
                End If
            End If

            ' dsHistory.Tables(0).Rows.Add()




        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If IsNothing(ds) = False Then
                ds.Dispose()
                ds = Nothing
            End If

            '19-Apr-13 Aniket: Resolving Memory Leak Issues
            If IsNothing(dtPatientsHistory) = False Then
                dtPatientsHistory.Dispose()
                dtPatientsHistory = Nothing
            End If
        End Try
        isHistoryLoading = False
    End Sub
    Private Sub ShowPendingReconcileMessage()
        If _blnRecordLock = False Then
            tlbbtn_Reconcile.Enabled = False
            Dim _isReadyLists As Boolean = False
            Dim ogloCCDReconcile As New gloCCDLibrary.gloCCDReconcilation
            _isReadyLists = ogloCCDReconcile.IsReadyListsPresent(m_PatientID, "Allergy")
            If _isReadyLists = True Then
                tlbbtn_Reconcile.Enabled = True
                MessageBox.Show("Patient has Pending Clinical Reconciliations. ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2)
            Else
                tlbbtn_Reconcile.Enabled = False
            End If
            ''slr free 
            If Not IsNothing(ogloCCDReconcile) Then
                ogloCCDReconcile.Dispose()
                ogloCCDReconcile = Nothing
            End If
        End If
    End Sub
    'Bug #53045: 00000471 : History
    'Function added while doing changes to resolve the above bug.
    Private Sub RefershHistoryAfterReview()
        Try
            btnConceptID.Enabled = False
            btn_CPTCode.Enabled = False
            btn_ICD9Code.Enabled = False
            Label59SNOMEDRefusedCode.Enabled = False
            btnClearSNOMEDRefusedCode.Enabled = False
            btnbrloinc.Enabled = False
            btnclloinc.Enabled = False
            btnBrowseCqm.Enabled = False
            btnClearCqm.Enabled = False
            btnCVXCode.Enabled = False
            btnClsCVXCode.Enabled = False
            C1HistoryDetails.Cols(col_HOnsetDate).AllowEditing = True
            C1HistoryDetails.Cols(Col_DateResolved).AllowEditing = True
            C1HistoryDetails.Cols(Col_ResolvedEndDate).AllowEditing = True
            isHistoryLoading = True
            setStyleGridWidth()

            Dim ds As DataSet
            'Dim dt As New DataTable
            ''slr free _dstemp
            If Not IsNothing(_dsTemp) Then
                _dsTemp.Dispose()
                _dsTemp = Nothing
            End If
            _dsTemp = New DataSet ''Deleted rows collection


            dsHistory = objclsPatientHistory.GetHistory_optimize(m_PatientID, 0, m_VisitDate, "History", 1, m_VisitID)

            dsHistory.Tables("History").Columns("nHistoryID").AutoIncrement = True
            dsHistory.Tables("History").Columns("nHistoryID").AutoIncrementSeed = 1

            dsHistory.Tables("History").Columns.Add("Hidden").SetOrdinal(1)
            dsHistory.Tables("History").Columns.Add("Button").SetOrdinal(5)
            dsHistory.Tables("History").Columns.Add("SmokingStatus").SetOrdinal(6)
            dsHistory.Tables("History").Columns.Add("SmokeButton").SetOrdinal(7)
            dsHistory.Tables("History").Columns.Add("sActive").SetOrdinal(8)

            dsHistory.Tables("History").Columns.Add("RowID")
            dsHistory.Tables("History").Columns.Add("nMemberId")
            ds = dsHistory.Copy()
            dsHistory.Tables("History").Clear()
            ' _dsTemp = New DataSet
            ''Added  by MAYURI:20120913- Deleted rows collection
            _dsTemp.Tables.Add("History")
            _dsTemp.Tables(0).Merge(dsHistory.Tables("History").Copy)

            Dim IsOnsetDate As Boolean = False
            Dim IsActive As Boolean = False
            Dim stronsetActiveStatus As String = ""
            ' Dim _arrOnsetActive() As String
            Dim strCategory As String
            Dim strHistory As String
            Dim strComment As String
            ' Dim strReaction As String
            '  Dim strActive As String
            Dim strRection_Status As String
            Dim intDrugID As Long
            Dim intMedicalConditionId As Long ' variable added by sagar to fill te medical condition id
            'For History Denormalization
            Dim strHxDrugName As String
            Dim strHxDosage As String
            Dim strHxNDCCode As String
            Dim intHxMPID As Int32
            ''Added Rahul on 20101004
            Dim strDOE_Allergy As String
            Dim sConCeptID As String
            Dim sDescriptionID As String
            Dim sSnoMedId As String
            Dim sSnoDescription As String
            Dim sICD9 As String
            Dim sHistoryType As String

            '29-Mar-13 Aniket: Addition of source column on the History screen
            Dim sHistorySource As String
            Dim nICDRevision As Integer = 0   ''added for ICD10 implementation
            Dim CPT As String
            Dim sRxNormID As String
            Dim nHistoryID As Long
            ''

            Dim strOnsetDate As String
            Dim strResolvedEndDate As String
            Dim strAllergyIntelorenceCode As String
            Dim strAllergySeverity As String
            Dim nRowOrder As Int64
            Dim CVXCode As String = ""
            Dim CVXDesc As String = ""


            If ds.Tables("History").Rows.Count > 0 Then
                '' current history
                Dim visitid As Long
                visitid = CType(ds.Tables("History").Rows.Item(0).Item(9), Long)
                _HxVisitID = visitid
                lblVisitDate.Tag = visitid
                lblVisitDate.Text = CType(ds.Tables("History").Rows.Item(0).Item(10), DateTime).Date

                Dim strdtReviewDate As String
                ''dtReview=  objclsPatientHistory.GetReviewHistory(m_PatientID, visitid)
                dtReview = dsHistory.Tables("ReviewHistory")
                If IsNothing(dtReview) = False Then
                    If dtReview.Rows.Count > 0 Then
                        strdtReviewDate = dtReview.Rows(0)("dtReviewDate")
                        lblReviewed.Visible = True
                        lblReviewed.Text = "History Reviewed By " + gstrLoginName + " on " + strdtReviewDate
                        intCheck = 1
                    Else
                        lblReviewed.Visible = False
                    End If
                Else
                    lblReviewed.Visible = False
                End If
                lblVisitDate.Tag = visitid
                For Each drHistory As DataRow In ds.Tables("History").Rows
                    strCategory = drHistory(HistoryEnum.sHistoryCategory)
                    strHistory = drHistory(HistoryEnum.sHistoryItem)
                    strComment = drHistory(HistoryEnum.sComments)
                    strRection_Status = drHistory(HistoryEnum.sReaction)
                    intDrugID = drHistory(HistoryEnum.nDrugID)
                    intMedicalConditionId = drHistory(HistoryEnum.MedicalCondition_Id) 'return the medical condition id

                    strHxDrugName = drHistory(HistoryEnum.sDrugName)
                    strHxDosage = drHistory(HistoryEnum.sDosage)
                    strHxNDCCode = drHistory(HistoryEnum.sNDCCode)
                    intHxMPID = drHistory(HistoryEnum.mpid)
                    strDOE_Allergy = Convert.ToString(drHistory(HistoryEnum.DOE_Allergy)).Replace("NULL", "")
                    sConCeptID = Convert.ToString(drHistory(HistoryEnum.sConceptID)).Replace("NULL", "")
                    sDescriptionID = Convert.ToString(drHistory(HistoryEnum.sDescriptionID)).Replace("NULL", "")
                    sSnoMedId = Convert.ToString(drHistory(HistoryEnum.sSnoMedID)).Replace("NULL", "")
                    sSnoDescription = Convert.ToString(drHistory(HistoryEnum.sDescription)).Replace("NULL", "")
                    sICD9 = drHistory(HistoryEnum.sICD9)
                    CPT = drHistory(HistoryEnum.CPT)
                    sRxNormID = drHistory(HistoryEnum.sRxNormID)
                    nHistoryID = drHistory(HistoryEnum.nHistoryID)
                    strOnsetDate = Convert.ToString(drHistory(HistoryEnum.OnsetDate)).Replace("NULL", "")
                    nRowOrder = drHistory(HistoryEnum.nRowOrder)
                    sHistoryType = Convert.ToString(drHistory(HistoryEnum.sHistoryType)).Replace("NULL", "")

                    '29-Mar-13 Aniket: Addition of source column on the History screen
                    sHistorySource = Convert.ToString(drHistory("sHistorySource")).Replace("NULL", "")
                    nICDRevision = Convert.ToInt16(drHistory("nICDRevision"))    ''added for ICD10 implementation
                    strResolvedEndDate = Convert.ToString(drHistory("dtObservationEndDate")).Replace("NULL", "")
                    strAllergyIntelorenceCode = Convert.ToString(drHistory("sAllergyIntelorenceCode")).Replace("NULL", "")
                    strAllergySeverity = Convert.ToString(drHistory("AllergySeverity")).Replace("NULL", "")
                    CVXCode = Convert.ToString(drHistory("CVXCode")).Replace("NULL", "")
                    CVXDesc = Convert.ToString(drHistory("CVXDesc")).Replace("NULL", "")
                    Call FillGrid(strCategory, strHistory, strComment, strRection_Status, intDrugID, intMedicalConditionId, strDOE_Allergy, sConCeptID, sDescriptionID, sSnoMedId, sSnoDescription, sRxNormID, "", "", sICD9, "", strHxNDCCode, strHxDosage, intHxMPID, nHistoryID, strOnsetDate, CPT, nRowOrder, sHistoryType, sHistorySource, nICDRevision, ResolvedEndDate:=strResolvedEndDate, AllergySeverity:=strAllergySeverity, sAllergyIntelorenceCode:=strAllergyIntelorenceCode, CVxCode:=CVXCode, CVXDesc:=CVXDesc)
                Next

                C1HistoryDetails.Select(1, 0, 1, 1, True)
                gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.View, "Patient History Viewed", gloAuditTrail.ActivityOutCome.Success)
                PopulateNarration()
                blncancel = True
                blnModify = True
            Else
                dsHistory.AcceptChanges()
                C1HistoryDetails.SetDataBinding(dsHistory, "History")
            End If

        Catch ex As Exception

            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Sub

    Private Sub RefershHistoryAfterReconciliation()
        Try
            btnConceptID.Enabled = False
            btn_CPTCode.Enabled = False
            btn_ICD9Code.Enabled = False
            Label59SNOMEDRefusedCode.Enabled = False
            btnClearSNOMEDRefusedCode.Enabled = False
            btnbrloinc.Enabled = False
            btnclloinc.Enabled = False
            btnBrowseCqm.Enabled = False
            btnClearCqm.Enabled = False
            btnCVXCode.Enabled = False
            btnClsCVXCode.Enabled = False
            C1HistoryDetails.Cols(col_HOnsetDate).AllowEditing = True
            C1HistoryDetails.Cols(Col_DateResolved).AllowEditing = True
            C1HistoryDetails.Cols(Col_ResolvedEndDate).AllowEditing = True
            isHistoryLoading = True
            lblReviewed.Visible = False
            C1HistoryDetails.Width = 785
            setStyleGridWidth()


            Dim ds As DataSet
            'Dim dt As New DataTable
            ''slr free _dstemp
            If Not IsNothing(_dsTemp) Then
                _dsTemp.Dispose()
                _dsTemp = Nothing
            End If
            _dsTemp = New DataSet ''Deleted rows collection


            dsHistory = objclsPatientHistory.GetHistory_optimize(m_PatientID, 0, m_VisitDate, "History", 1, m_VisitID)

            dsHistory.Tables("History").Columns("nHistoryID").AutoIncrement = True
            dsHistory.Tables("History").Columns("nHistoryID").AutoIncrementSeed = 1

            dsHistory.Tables("History").Columns.Add("Hidden").SetOrdinal(1)
            dsHistory.Tables("History").Columns.Add("Button").SetOrdinal(5)
            dsHistory.Tables("History").Columns.Add("SmokingStatus").SetOrdinal(6)
            dsHistory.Tables("History").Columns.Add("SmokeButton").SetOrdinal(7)
            dsHistory.Tables("History").Columns.Add("sActive").SetOrdinal(8)

            dsHistory.Tables("History").Columns.Add("RowID")
            dsHistory.Tables("History").Columns.Add("nMemberId")
            ds = dsHistory.Copy()
            dsHistory.Tables("History").Clear()
            ' _dsTemp = New DataSet
            ''Added  by MAYURI:20120913- Deleted rows collection
            _dsTemp.Tables.Add("History")
            _dsTemp.Tables(0).Merge(dsHistory.Tables("History").Copy)

            Dim IsOnsetDate As Boolean = False

            Dim IsActive As Boolean = False
            Dim stronsetActiveStatus As String = ""
            '   Dim _arrOnsetActive() As String
            Dim strCategory As String
            Dim strHistory As String
            Dim strComment As String
            ' Dim strReaction As String
            '  Dim strActive As String
            Dim strRection_Status As String
            Dim intDrugID As Long
            Dim intMedicalConditionId As Long ' variable added by sagar to fill te medical condition id
            'For History Denormalization
            Dim strHxDrugName As String
            Dim strHxDosage As String
            Dim strHxNDCCode As String
            Dim intHxMPID As Int32
            ''Added Rahul on 20101004
            Dim strDOE_Allergy As String
            Dim sConCeptID As String
            Dim sDescriptionID As String
            Dim sSnoMedId As String
            Dim sSnoDescription As String
            Dim sICD9 As String
            Dim sHistoryType As String

            '29-Mar-13 Aniket: Addition of source column on the History screen
            Dim sHistorySource As String
            Dim nICDRevision As Int32 = 0   ''added for ICD10 implementation
            Dim CPT As String
            Dim sRxNormID As String
            Dim nHistoryID As Long
            ''

            Dim strOnsetDate As String
            Dim nRowOrder As Int64
            Dim strResolvedEndDate As String

            Dim strAllergyIntelorenceCode As String
            Dim strAllergySeverity As String
            Dim CVXCode As String = ""
            Dim CVXDesc As String = ""

            If ds.Tables("History").Rows.Count > 0 Then
                '' current history
                Dim visitid As Long
                visitid = CType(ds.Tables("History").Rows.Item(0).Item(9), Long)
                _HxVisitID = visitid
                lblVisitDate.Tag = visitid
                lblVisitDate.Text = CType(ds.Tables("History").Rows.Item(0).Item(10), DateTime).Date

                Dim strdtReviewDate As String
                ''dtReview=  objclsPatientHistory.GetReviewHistory(m_PatientID, visitid)
                dtReview = dsHistory.Tables("ReviewHistory")
                If IsNothing(dtReview) = False Then
                    If dtReview.Rows.Count > 0 Then
                        strdtReviewDate = dtReview.Rows(0)("dtReviewDate")
                        lblReviewed.Visible = True
                        lblReviewed.Text = "History Reviewed By " + gstrLoginName + " on " + strdtReviewDate
                        intCheck = 1
                    Else
                        lblReviewed.Visible = False
                    End If
                Else
                    lblReviewed.Visible = False
                End If
                lblVisitDate.Tag = visitid
                For Each drHistory As DataRow In ds.Tables("History").Rows
                    strCategory = drHistory(HistoryEnum.sHistoryCategory)
                    strHistory = drHistory(HistoryEnum.sHistoryItem)
                    strComment = drHistory(HistoryEnum.sComments)
                    strRection_Status = drHistory(HistoryEnum.sReaction)
                    intDrugID = drHistory(HistoryEnum.nDrugID)
                    intMedicalConditionId = drHistory(HistoryEnum.MedicalCondition_Id) 'return the medical condition id

                    strHxDrugName = drHistory(HistoryEnum.sDrugName)
                    strHxDosage = drHistory(HistoryEnum.sDosage)
                    strHxNDCCode = drHistory(HistoryEnum.sNDCCode)
                    intHxMPID = drHistory(HistoryEnum.mpid)
                    strDOE_Allergy = Convert.ToString(drHistory(HistoryEnum.DOE_Allergy)).Replace("NULL", "")
                    sConCeptID = Convert.ToString(drHistory(HistoryEnum.sConceptID)).Replace("NULL", "")
                    sDescriptionID = Convert.ToString(drHistory(HistoryEnum.sDescriptionID)).Replace("NULL", "")
                    sSnoMedId = Convert.ToString(drHistory(HistoryEnum.sSnoMedID)).Replace("NULL", "")
                    sSnoDescription = Convert.ToString(drHistory(HistoryEnum.sDescription)).Replace("NULL", "")
                    sICD9 = drHistory(HistoryEnum.sICD9)
                    CPT = drHistory(HistoryEnum.CPT)
                    sRxNormID = drHistory(HistoryEnum.sRxNormID)
                    nHistoryID = drHistory(HistoryEnum.nHistoryID)
                    strOnsetDate = Convert.ToString(drHistory(HistoryEnum.OnsetDate)).Replace("NULL", "")
                    nRowOrder = drHistory(HistoryEnum.nRowOrder)
                    sHistoryType = Convert.ToString(drHistory(HistoryEnum.sHistoryType)).Replace("NULL", "")

                    '29-Mar-13 Aniket: Addition of source column on the History screen
                    sHistorySource = Convert.ToString(drHistory("sHistorySource")).Replace("NULL", "")
                    nICDRevision = Convert.ToInt16(drHistory("nICDRevision"))   ''added for ICD10 implementation
                    strResolvedEndDate = Convert.ToString(drHistory("dtObservationEndDate")).Replace("NULL", "")
                    strAllergyIntelorenceCode = Convert.ToString(drHistory("sAllergyIntelorenceCode")).Replace("NULL", "")
                    strAllergySeverity = Convert.ToString(drHistory("AllergySeverity")).Replace("NULL", "")
                    CVXCode = Convert.ToString(drHistory("CVXCode")).Replace("NULL", "")
                    CVXDesc = Convert.ToString(drHistory("CVXDesc")).Replace("NULL", "")
                    Call FillGrid(strCategory, strHistory, strComment, strRection_Status, intDrugID, intMedicalConditionId, strDOE_Allergy, sConCeptID, sDescriptionID, sSnoMedId, sSnoDescription, sRxNormID, "", "", sICD9, "", strHxNDCCode, strHxDosage, intHxMPID, nHistoryID, strOnsetDate, CPT, nRowOrder, sHistoryType, sHistorySource, nICDRevision, ResolvedEndDate:=strResolvedEndDate, AllergySeverity:=strAllergySeverity, sAllergyIntelorenceCode:=strAllergyIntelorenceCode, CVxCode:=CVXCode, CVXDesc:=CVXDesc)
                Next

                C1HistoryDetails.Select(1, 0, 1, 1, True)
                gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.View, "Patient History Viewed", gloAuditTrail.ActivityOutCome.Success)
                PopulateNarration()
                blncancel = True
                blnModify = True
            Else
                dsHistory.AcceptChanges()
                C1HistoryDetails.SetDataBinding(dsHistory, "History")
            End If

        Catch ex As Exception

            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Sub
    Public Sub setStyleGridWidth()
        With C1HistoryDetails

            Dim _TotalWidth As Single = .Width - 5
            .Cols(Col_HCategory).Width = _TotalWidth * 0.15
            .Cols(Col_HHidden).Width = _TotalWidth * 0
            .Cols(Col_HsHistoryItem).Width = _TotalWidth * 0.19
            .Cols(Col_HsComments).Width = _TotalWidth * 0.26
            .Cols(Col_HsReaction).Width = _TotalWidth * 0.15
            .Cols(Col_HButton).Width = _TotalWidth * 0.03
            If gblnShowSmokingColumn = True Then
                .Cols(Col_HSmokingStatus).Width = _TotalWidth * 0.15
                .Cols(Col_HSmokeButton).Width = _TotalWidth * 0.03
            Else
                .Cols(Col_HSmokingStatus).Width = _TotalWidth * 0
                .Cols(Col_HSmokeButton).Width = _TotalWidth * 0
            End If
            .Cols(Col_HsActive).Width = _TotalWidth * 0.055
            .Cols(Col_HMedicalConditionID).Width = _TotalWidth * 0
            .Cols(Col_HsDosage).Width = _TotalWidth * 0
            .Cols(Col_HsNDCCode).Width = _TotalWidth * 0
            .Cols(Col_Hnmpid).Width = _TotalWidth * 0
            .Cols(Col_HDOE_Allergy).Width = _TotalWidth * 0.11
            .Cols(Col_HsConceptID).Width = _TotalWidth * 0
            .Cols(Col_HsDescriptionID).Width = _TotalWidth * 0
            .Cols(Col_HsSnomedID).Width = _TotalWidth * 0
            .Cols(Col_HsDescription).Width = _TotalWidth * 0
            '  .Cols(Col_HsICD9).Width = _TotalWidth * 0.18

            .Cols(Col_HsRxNormID).Width = _TotalWidth * 0

            .Cols(Col_HsICD9).Width = 0

            ' .Cols(Col_HsICD9).Visible = True


            .Cols(col_HCPT).Width = 0
            .Cols("sRefusalCode").Width = 0
            .Cols("sRefusalDesc").Width = 0
            .Cols(Col_DateResolved).Width = 0

            '  .Cols(col_HCPT).Visible = True
            .Cols(Col_LoincCode).Width = 0
            .Cols(Col_LoincDescr).Width = 0
            .Cols(Col_LoincCode).Caption = "sLoincCode"
            .Cols(Col_LoincDescr).Caption = "sLoincDescr"
            .Cols(Col_CVXCode).Width = 0
            .Cols(Col_CVXDesc).Width = 0
            .Cols(Col_CVXCode).Caption = "CVXCode"
            .Cols(Col_CVXDesc).Caption = "CVXDesc"
            .Cols(Col_CVXCode).Name = "CVXCode"
            .Cols(Col_CVXDesc).Name = "CVXDesc"





            C1HistoryDetails.Cols(col_HOnsetDate).Caption = "Occur Date" '25-Jun-13 Aniket: Change caption to Occur Date
            If (gblnEnableCQMCypressTesting) Then
                C1HistoryDetails.Cols(col_HOnsetDate).Width = _TotalWidth * 0.2
            Else
                C1HistoryDetails.Cols(col_HOnsetDate).Width = _TotalWidth * 0.15
            End If
            C1HistoryDetails.Cols("nRowOrder").Width = 0
            C1HistoryDetails.Cols("nMemberid").Width = 0

            '08-Apr-14 Aniket: Resolving Bug #66749:
            C1HistoryDetails.Cols("nICDRevision").Width = 0

            C1HistoryDetails.Cols(Col_AllergyClsID).Width = 0 ''added for 2015 certification
            C1HistoryDetails.Cols(Col_AllergyClsID).Caption = "AllergyClsID"

            C1HistoryDetails.Cols(Col_CQMCode).Width = 0
            C1HistoryDetails.Cols(Col_CQMDesc).Width = 0
            C1HistoryDetails.Cols(Col_CQMCode).Caption = "sValueSetOID"
            C1HistoryDetails.Cols(Col_CQMDesc).Caption = "sValueSetName"
            C1HistoryDetails.Cols(Col_DeviceList_ID).Width = 0
            C1HistoryDetails.Cols(Col_DeviceList_ID).Caption = "Device List ID"
            C1HistoryDetails.Cols(COl_sProcStatus).Width = 0
            C1HistoryDetails.Cols(COl_sProcStatus).Caption = "Proc. Status"

            C1HistoryDetails.Cols(Col_AllergySeverity).Width = 0 ''added for 2015 certification
            C1HistoryDetails.Cols(Col_AllergySeverity).Caption = "AllergySeverity"
            C1HistoryDetails.Cols(Col_SnoMedCode).Width = 0 ''added for 2015 certification
            C1HistoryDetails.Cols(Col_SnoMedCode).Caption = "SnoMedCode"

            C1HistoryDetails.Cols(Col_ResolvedEndDate).Caption = "Resolved/End Date"
            C1HistoryDetails.Cols(Col_ResolvedEndDate).Width = 0 ''added for 2015 certification

            C1HistoryDetails.Cols(Col_AllergyIntelorenceCode).Width = 0 ''added for 2015 certification
            C1HistoryDetails.Cols(Col_AllergyIntelorenceCode).Caption = "sAllergyIntelorenceCode"



            C1HistoryDetails.ExtendLastCol = True

        End With
    End Sub


    Private Sub FillPrevHistory()
        If trvPrevHistory.GetNodeCount(False) <= 0 Then

            trvPrevHistory.Nodes.Add("Patient History")
            trvPrevHistory.Nodes.Item(0).ImageIndex = 0
            trvPrevHistory.Nodes.Item(0).SelectedImageIndex = 0
            With trvPrevHistory.Nodes.Item(0)
                .Nodes.Clear()

                Dim mychild As myTreeNode
                mychild = New myTreeNode("Current", 0)
                mychild.ForeColor = Color.Blue
                .Nodes.Add(mychild)
                mychild = Nothing 'Change made to solve memory Leak and word crash issue

                mychild = New myTreeNode("Yesterday", 1)
                mychild.ForeColor = System.Drawing.Color.FromArgb(46, 14, 140)
                .Nodes.Add(mychild)
                mychild = Nothing 'Change made to solve memory Leak and word crash issue

                mychild = New myTreeNode("Last Week", 2)
                mychild.ForeColor = System.Drawing.Color.FromArgb(188, 0, 169)
                .Nodes.Add(mychild)
                mychild = Nothing 'Change made to solve memory Leak and word crash issue

                mychild = New myTreeNode("Last Month", 3)
                mychild.ForeColor = System.Drawing.Color.FromArgb(25, 142, 255)
                .Nodes.Add(mychild)
                mychild = Nothing 'Change made to solve memory Leak and word crash issue

                mychild = New myTreeNode("Older", 4)
                mychild.ForeColor = System.Drawing.Color.FromArgb(39, 69, 100)
                .Nodes.Add(mychild)
                mychild = Nothing 'Change made to solve memory Leak and word crash issue
            End With
        End If

        Call RefreshHistory()
    End Sub

    Public Sub objBtn_Click(ByVal sender As Object, ByVal e As System.EventArgs) 'Handles objBtn.Click
        'GLO2012-0016280 : History throws an error
        'If Process is running then bypass the button click

        If bln_Loadcategory Then
            Exit Sub
        End If
        Cursor = Cursors.WaitCursor
        Try

            'GLO2012-0016280 : History throws an error
            'If function returns True then proceed further
            If FillHistoryCategory1(sender.Text) Then
                For Each btncnrtol As Control In pnltrvSource.Controls
                    If TypeOf btncnrtol Is Button Then
                        If UCase(btncnrtol.Text) = UCase(CType(sender, Button).Text) Then
                            sender = btncnrtol
                            CType(btncnrtol, Button).BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongYellow
                        End If
                    End If
                Next btncnrtol


                BtnTag = sender.tag
                BtnText = sender.Text


                Dim _Button As Button '' TEMP BUTTON ''
                Dim myColumnName As String = "sDescription"
                Dim myInternalDt As DataTable = Nothing
                '' PUT TOP BUTTON TO BOTTOM PANEL ''
                For Each oButton As Control In pnlTopButtons.Controls
                    If TypeOf oButton Is Button Then
                        '' NOW CREATE A COPY OF THIS BUTTON AND REMOVE IT FROM TOP PANEL AND PASTE IT TO BOTTOM PANEL ''
                        _Button = oButton
                        _Button.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
                        pnlBottomButtons.Controls.Add(_Button)
                        For Each _btn As Control In oButton.Controls
                            If TypeOf _btn Is Button Then
                                _btn.Dispose()

                                oButton.Controls.Remove(_btn)
                            End If
                        Next
                        pnlTopButtons.Controls.Remove(oButton)
                        Exit For
                    End If
                Next

                '' NOW PUT CLICKED BUTTON IN TOP PANEL ''
                _Button = CType(sender, Button)
                pnlTopButtons.Controls.Add(_Button)
                If sender.Text = "OB Medical History" Or sender.Text = "OB Genetic History" Or sender.Text = "OB Infection History" Or sender.text = "OB Initial Physical Examination" Then
                    Dim _btnimage As New Button
                    _btnimage.Dock = DockStyle.Right
                    _btnimage.Size = New Drawing.Size(22, 22)
                    _btnimage.Image = imgTreeVIew.Images(4)
                    _btnimage.BackColor = Color.Transparent
                    _btnimage.BackgroundImageLayout = ImageLayout.Stretch
                    _btnimage.FlatStyle = FlatStyle.Flat
                    _btnimage.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(176, Byte), Integer))
                    _btnimage.FlatAppearance.BorderSize = 0
                    _btnimage.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
                    _btnimage.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
                    _btnimage.Cursor = System.Windows.Forms.Cursors.Hand

                    _Button.Controls.Add(_btnimage)
                    RemoveHandler _btnimage.Click, AddressOf _btnimage_Click
                    RemoveHandler _btnimage.MouseHover, AddressOf _btnimage_MouseHover
                    AddHandler _btnimage.Click, AddressOf _btnimage_Click
                    AddHandler _btnimage.MouseHover, AddressOf _btnimage_MouseHover
                End If

                _Button.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongOrange
                _Button.BackgroundImageLayout = ImageLayout.Stretch
                '' REMOVE CLICKED BUTTON FROM BOTTOM PANEL ''
                pnlBottomButtons.Controls.Remove(CType(sender, Button))

                ''---
                If blnIsLoad = False Then
                    myInternalDt = mydtButton.Copy()

                    If Not IsNothing(myInternalDt) Then
                        For j As Integer = 0 To myInternalDt.Rows.Count - 1
                            If Convert.ToString(myInternalDt.Rows(j)(myColumnName)) = _Button.Text Then
                                myInternalDt.Rows(j).Delete()
                                myInternalDt.AcceptChanges()
                                Exit For
                            End If
                        Next
                    End If  ''myInternalDt
                End If ''blnIsLoad
                If Not IsNothing(myInternalDt) Then
                    myInternalDt.Dispose()
                    myInternalDt = Nothing
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            pnltrvSource.Visible = True
        End Try
        Cursor = Cursors.Default
    End Sub
    Public Sub _btnimage_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Me.Cursor = Cursors.WaitCursor
        C1HistoryDetails.BeginUpdate()
        Dim dv As DataView = dsHistory.Tables("History").Copy.DefaultView
        For Each _node As gloUserControlLibrary.myTreeNode In GloUC_trvHistory.Nodes

            FillDataInGrid(dv, _node)
        Next
        C1HistoryDetails.EndUpdate()
        Me.Cursor = Cursors.Default
        If Not IsNothing(dv) Then
            dv.Dispose()
            dv = Nothing
        End If
    End Sub
    Private Sub _btnimage_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs)
        C1SuperTooltip1.SetToolTip(sender, "Add All Items")
    End Sub
    Public Sub NewHistory()
        dsHistory.Tables("History").Clear()
        btnUp.Enabled = False
        btnDown.Enabled = False
        lblVisitDate.Tag = 0
        blnModify = False
        lblVisitDate.Text = Now.Date
        pnlWordComp.Visible = False
        tblShow.Checked = False
    End Sub

    Public Sub CloseHistory()
        'Me.Close()
        gloWord.WordDialogBoxBackgroundCloser.Close(Me)
    End Sub

    Private Function SetRowState(ByVal dsMain As DataSet) As DataSet
        Try
            If (IsNothing(dsMain) = False) Then


                For intTables As Integer = 0 To dsMain.Tables.Count - 1

                    For intCount As Integer = 0 To dsMain.Tables(intTables).Rows.Count - 1
                        '   If dsMain.Tables(intTables).Rows(intCount).RowState.ToString = "" Then


                        If dsMain.Tables(intTables).Rows(intCount).RowState.ToString = "Added" Then
                            dsMain.Tables(intTables).Rows(intCount)("RowState") = "Added"
                        ElseIf dsMain.Tables(intTables).Rows(intCount).RowState.ToString = "Modified" Then
                            dsMain.Tables(intTables).Rows(intCount)("RowState") = "Modified"
                        ElseIf dsMain.Tables(intTables).Rows(intCount).RowState.ToString = "Deleted" Then
                            dsMain.Tables(intTables).Rows(intCount).RejectChanges()
                            dsMain.Tables(intTables).Rows(intCount)("RowState") = "Deleted"
                        End If
                        ' End If
                    Next

                Next

            End If
            Return dsMain
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.Category, gloAuditTrail.ActivityType.Select, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            ' MessageBox.Show(ex.ToString, "Patient History", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try
    End Function
    Public Sub SaveDatasetHistory_new(ByVal dsHistory As DataSet)
        Dim ds As DataSet = Nothing
        Dim dsStatus As DataSet = Nothing ''slr new not needed 
        Dim objclsDB As New clsDoctorsDashBoard
        arrOB = New ArrayList
        Try

            dsStatus = objclsDB.Fill_StandardHistoryTypes()

            ''Added  by MAYURI:20120913- Added if condition if user opens screen and only deleted record asd just save and close then dshistory is nothing and data in _dsTemp
            If IsNothing(dsHistory) = False Then
                ds = dsHistory.Copy()
                ds.Tables("History").Merge(_dsTemp.Tables("History").Copy())
            Else
                ds = New DataSet
                ds.Tables.Add("History")
                ds.Tables("History").Merge(_dsTemp.Tables("History").Copy())
            End If

            Dim i As Integer
            Dim k As Integer

            If lblVisitDate.Tag = 0 Then
                If (m_VisitDate = DateTime.Now.Date) Then
                    m_VisitDate = DateTime.Now
                End If
                lblVisitDate.Tag = GenerateVisitID(m_VisitDate, m_PatientID)
            End If

            Dim _categorytype As String = ""
            Dim stronsetActiveStatus As String = ""
            Dim _arrOnsetActive() As String
            Dim IsActive As Boolean = False
            Dim IsOnsetDate As Boolean = False
            With ds.Tables("History")
                For i = .Rows.Count - 1 To 0 Step -1
                    ''  If .Rows(i).RowState <> DataRowState.Deleted Then
                    '  If .Rows(i)("RowState") <> "Deleted" Then
                    .Rows(i)(Col_HCategory) = .Rows(i)(Col_HHidden)
                    If Convert.ToString(.Rows(i)(Col_HsHistoryItem)) = "" Then
                        .Rows(i).Delete()
                    End If
                    ' End If
                Next




                .Columns.Add("nPatientID")
                .Columns.Add("sTransUser")
                ''Added on 20150511-To keep auditlog for each historyitem(Added/Deleted/Modified)
                .Columns.Add("nUserID")
                .Columns.Add("nClinicID")
                .Columns.Add("sMachineName")
                ''End code Added on 20150511-To keep auditlog for each historyitem(Added/Deleted/Modified)
                ds.AcceptChanges()
                _isOBHistoryModified = True
                ' Dim _isAllstatesAdded As String
                Dim _smokingstat As String = String.Empty
                For k = 0 To .Rows.Count - 1
                    ' .Rows(k)("nID") = k + 1
                    '' If .Rows(k).RowState <> DataRowState.Deleted Then
                    _categorytype = Convert.ToString(.Rows(k)(Col_HHidden)).Trim
                    _smokingstat = Convert.ToString(.Rows(k)("SmokingStatus"))
                    If (_smokingstat <> "") Then
                        If Not IsNothing(dtcategoryType) Then
                            Dim drr As DataRow() = dtcategoryType.Select("sDescription='" & _smokingstat & "'")
                            If (drr.Length > 0) Then
                                .Rows(k)("sSnomedCode") = Convert.ToString(drr(0)("nCategoryID"))
                            End If
                        End If
                    End If


                    If _categorytype = "OB Medical History" Then

                        If Not arrOB.Contains("OBMedical") Then
                            arrOB.Add("OBMedical")
                        End If

                    ElseIf _categorytype = "OB Genetic History" Then

                        If Not arrOB.Contains("OBGenetic") Then
                            arrOB.Add("OBGenetic")
                        End If

                    ElseIf _categorytype = "OB Initial Physical Examination" Then

                        If Not arrOB.Contains("OBExam") Then
                            arrOB.Add("OBExam")
                        End If

                    ElseIf _categorytype = "OB Infection History" Then

                        If Not arrOB.Contains("OBInfection") Then
                            arrOB.Add("OBInfection")
                        End If


                    End If
                    If _categorytype = "OB Medical History" Or _categorytype = "OB Genetic History" Or _categorytype = "OB Infection History" Or _categorytype = "OB Initial Physical Examination" Then
                    Else
                        If Convert.ToString(.Rows(k)("sHistoryType")).Trim = "" Then
                            _categorytype = Convert.ToString(.Rows(k)(Col_HHidden)).Trim
                            _categorytype = getHistoryTypefromcategorymaster_Status(_categorytype, dsStatus)
                        Else
                            _categorytype = Convert.ToString(.Rows(k)("sHistoryType")).Trim
                        End If
                    End If

                    IsActive = False
                    If _categorytype <> "" Then
                        If _categorytype.Length > 2 Then
                            If _categorytype = "OB Medical History" Or _categorytype = "OB Genetic History" Or _categorytype = "OB Infection History" Or _categorytype = "OB Initial Physical Examination" Then
                                IsActive = True
                            Else
                                stronsetActiveStatus = CheckHistoryTypeinStandardTable_status(_categorytype, dsStatus)
                                _arrOnsetActive = stronsetActiveStatus.Split(",")
                                If IsNothing(_arrOnsetActive) = False Then
                                    If _arrOnsetActive.Length >= 1 Then
                                        IsActive = _arrOnsetActive.GetValue(1)
                                    End If
                                End If
                            End If

                        End If
                    End If
                    If Convert.ToString(.Rows(k)(Col_HsActive)) = "True" Then
                        .Rows(k)(Col_HsActive) = "Active"
                    ElseIf IsActive = False Then
                        .Rows(k)(Col_HsActive) = "Active"
                    ElseIf Convert.ToString(.Rows(k)(Col_HsActive)) = "False" Then

                        .Rows(k)(Col_HsActive) = "InActive"
                    End If

                    If _categorytype = "Fam" Then
                        .Rows(k)(Col_HsReaction) = .Rows(k)(Col_HsReaction) & ":" & .Rows(k)("nMemberId") & "|" & .Rows(k)(Col_HsActive)
                    ElseIf _categorytype = "OB Initial Physical Examination" Then
                        .Rows(k)(Col_HsReaction) = .Rows(k)(Col_HsReaction) & "|" & "Active"
                    Else

                        .Rows(k)(Col_HsReaction) = .Rows(k)(Col_HsReaction) & "|" & .Rows(k)(Col_HsActive)
                    End If

                    .Rows(k)(Col_HnVisitID) = lblVisitDate.Tag

                    .Rows(k)("sTransUser") = gstrLoginName
                    .Rows(k)("nPatientID") = lblPatientCode.Tag

                    ''Added on 20150511-To keep auditlog for each historyitem(Added/Deleted/Modified)
                    .Rows(k)("nUserID") = gnLoginID
                    .Rows(k)("nClinicID") = gnClinicID
                    .Rows(k)("sMachineName") = gstrClientMachineName
                    ''End code Added on 20150511-To keep auditlog for each historyitem(Added/Deleted/Modified)
                    If .Rows(k)(Col_HCategory) = "Smoking Status" Then
                        .Rows(k)(Col_HsReaction) = .Rows(k)(Col_HSmokingStatus) & "|" & .Rows(k)(Col_HsActive)
                    End If
                    If _IsFrmImm = True And .Rows(k)(Col_HsHistoryItem) = _strAllergy Then
                        .Rows(k)(Col_HsReaction) = .Rows(k)(Col_HsReaction)
                    End If
                    '  .Rows(k)("RowState") = "Added"
                Next

                'For g As Integer = 0 To .Rows.Count - 1
                '    _isAllstatesAdded = .Rows(g)("RowState")
                '    If _isAllstatesAdded <> "Added" Then
                '        Exit For
                '    End If
                'Next
                'If _isAllstatesAdded = "Added" Then
                '    objclsPatientHistory.DeleteHistory(m_VisitID, m_PatientID)
                'End If
                .Columns.Remove("Hidden")
                .Columns.Remove("Button")
                .Columns.Remove("SmokingStatus")
                .Columns.Remove("dtVisitDate")
                .Columns.Remove("SmokeButton")
                .Columns.Remove("sActive")
                .Columns.Remove("RowID")
                .Columns.Remove("DateResolved")
                .Columns.Remove("nMemberId")
                '.Columns.Remove("CPTButton")

            End With
            ''Added  by MAYURI:20120913- Insert new data if not present against current visit
            'Dim IsHistoryPresent As Boolean = True
            'IsHistoryPresent = objclsPatientHistory.CheckHistoryforCurrentVisit(m_PatientID, m_VisitID)

            'If IsHistoryPresent = False Or _dsTemp.Tables(0).Rows.Count > 0 Then
            '    For h As Integer = 0 To ds.Tables("History").Rows.Count - 1
            '        If ds.Tables("History").Rows(h)("RowState") <> "Deleted" Then
            '            ds.Tables("History").Rows(h)("nRowOrder") = h + 1
            '        Else
            '            ds.Tables("History").Rows(h)("nRowOrder") = 0
            '        End If
            '    Next
            'End If

            Call SavePHistoryMedicationReconcillation()
            If m_VisitID = 0 Then

                If ds.Tables.Contains("History") AndAlso Not dtPHistoryMedRec Is Nothing Then

                    If ds.Tables("History").Rows.Count > 0 AndAlso dtPHistoryMedRec.Rows.Count > 0 Then

                        dtPHistoryMedRec.Rows(0)("VisitID") = ds.Tables("History").Rows(0)("nVisitID")
                        dtPHistoryMedRec.AcceptChanges()
                    End If

                End If
            End If
            Dim resultDialog As DialogResult
            Dim bsubsequentResult As Boolean = False

            Dim IsPassVisitVariable As Boolean = IsPastVisit()
            If IsPassVisitVariable Then
                resultDialog = MessageBox.Show("There are subsequent visit(s) available for this patient. Do you want to add/update the new entries to subsequent visits?" + vbNewLine + "Yes - Add/update to subsequent visits" + vbNewLine + "No - Do not add/update to subsequent visits", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                If resultDialog = vbYes Then
                    bsubsequentResult = True
                End If
            End If

            If objclsPatientHistory.AddNewHistoryDataset(IsPassVisitVariable, bsubsequentResult, m_PatientID, blnModify, ds, "@SaveHistory", dtPHistoryMedRec, "@Rx_HistoryMedicationReconcillation") = False Then


                Exit Sub

            End If

            blnChangesMade = False
            isHistoryModified = False
            blnChangesMade = False
            isHistoryModified = False


            If arrDataDictionary.Count > 0 Then
                DeleteHistoryDataDictionary()
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If IsNothing(ds) = False Then
                ds.Dispose()
                ds = Nothing
            End If
            'SLR: FRee dsstatus, objclsdb
            If Not IsNothing(dsStatus) Then
                dsStatus.Dispose()
                dsStatus = Nothing
            End If
            If Not IsNothing(objclsDB) Then

                objclsDB = Nothing
            End If
        End Try
    End Sub

    Private Function IsPastVisit() As Boolean
        If m_VisitDate.ToShortDateString = Now.ToShortDateString Then
            Return False
        Else
            Return True
        End If
    End Function

    Private Sub OpenMedication(Optional ByVal intstat As Int16 = 0)
        '' --- FOR MEDICATION
        Dim visitid As Long
        visitid = lblVisitDate.Tag
        Dim frmRxMeds As frmPrescription = Nothing

        Try

            frmRxMeds = frmPrescription.GetInstance(visitid, m_PatientID)

            Try
                RemoveHandler frmRxMeds.FormClosed, AddressOf On_frmRxMeds_Closed
            Catch ex As Exception

            End Try
            If blnOpenFromExam Then
                If Not IsNothing(arrOB) Then
                    If arrOB.Count > 0 Then
                        frmRxMeds.ShowPanel()
                    End If

                End If

            End If

            AddHandler frmRxMeds.FormClosed, AddressOf On_frmRxMeds_Closed 'myEvent            
            If IsNothing(frmRxMeds) = True Then
                Exit Sub
            End If


            If Not myCaller1 Is Nothing Then
                frmRxMeds.myCaller1 = Me.myCaller1
            End If
            If Not myCaller Is Nothing Then
                frmRxMeds.myCaller = Me.myCaller
            End If
            If Not myLetter Is Nothing Then
                frmRxMeds.myLetter = Me.myLetter
            End If
            If myCallerSynopsis IsNot Nothing Then
                frmRxMeds.myCallerSynopsis = Me.myCallerSynopsis
            End If

            If frmPrescription.IsOpen = False Then
                frmRxMeds.Hide()
                frmRxMeds.WindowState = FormWindowState.Maximized
                frmRxMeds.MdiParent = Me.MdiParent
                frmRxMeds.blnOpenFromExam = True
                frmRxMeds.ShowMedication()
            End If


            If frmRxMeds.blncancel Then
                If frmPatientExam.intflag = 1 Then
                    frmPatientExam.intflag = 2
                End If
                If frmPatientLetter.intflag = 1 Then
                    frmPatientLetter.intflag = 2
                End If

                If IsNothing(Me.MdiParent) Then
                    If frmPrescription.IsOpen = False Then
                        frmRxMeds.Hide()
                        frmRxMeds.WindowState = FormWindowState.Maximized
                        frmRxMeds.MdiParent = Me.MdiParent
                        frmRxMeds.blnOpenFromExam = True
                        frmRxMeds.ShowDialog(IIf(IsNothing(frmRxMeds.Parent), Me, frmRxMeds.Parent))
                        If (IsNothing(frmRxMeds) = False) Then
                            frmRxMeds.Close()
                        End If
                        If (IsNothing(frmRxMeds) = False) Then
                            frmRxMeds.Dispose()
                            frmRxMeds = Nothing
                        End If

                    Else
                        MessageBox.Show("Rx/Meds screen cannot be opened as it is already open in the background.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                Else
                    frmRxMeds.Hide()
                    frmRxMeds.WindowState = FormWindowState.Maximized
                    frmRxMeds.MdiParent = Me.MdiParent
                    frmRxMeds.blnOpenFromExam = True
                    frmRxMeds.Show()
                    frmRxMeds.RefreshComboOnLoad() ''''''''prescription shown event not raised on show bug 93866
                End If

                gloWord.WordDialogBoxBackgroundCloser.Close(Me)
            Else
                If intstat = 1 Then
                    If blnOpenFromExam = True Then
                        If Not IsNothing(myCaller) Then
                            myCaller.GetdataFromOtherForms(gloEMRWord.enumDocType.History)

                            '07-May-15 Aniket: 8050 OB PRD Changes
                            If Not IsNothing(arrOB) Then

                                If arrOB.Contains("OBGenetic") Then
                                    myCaller.GetdataFromOtherForms(gloEMRWord.enumDocType.OBGeneticHistory)
                                End If
                                If arrOB.Contains("OBInfection") Then
                                    myCaller.GetdataFromOtherForms(gloEMRWord.enumDocType.OBInfectionHistory)
                                End If
                                If arrOB.Contains("OBExam") Then
                                    myCaller.GetdataFromOtherForms(gloEMRWord.enumDocType.OBInitialPhysicalExamination)
                                End If
                                If arrOB.Contains("OBMedical") Then
                                    myCaller.GetdataFromOtherForms(gloEMRWord.enumDocType.OBMedicalHistory)
                                End If
                            End If

                            If wdNarration.Text <> "" Then
                                myCaller.GetdataFromOtherForms(gloEMRWord.enumDocType.Narration)
                            End If

                        End If
                        If Not IsNothing(myLetter) Then
                            myLetter.GetdataFromOtherForms(gloEMRWord.enumDocType.History)
                            If wdNarration.Text <> "" Then
                                myLetter.GetdataFromOtherForms(gloEMRWord.enumDocType.Narration)
                            End If
                        End If
                        blnOpenFromExam = False
                    End If
                    If (IsNothing(frmRxMeds) = False) Then
                        frmRxMeds.Close()
                    End If
                    If (IsNothing(frmRxMeds) = False) Then
                        frmRxMeds.Dispose()
                        frmRxMeds = Nothing
                    End If
                    ' Me.Close()
                    gloWord.WordDialogBoxBackgroundCloser.Close(Me)
                Else
                    lblVisitDate.Text = Now.Date
                    pnlWordComp.Visible = False
                    NewHistory()
                    If pnlPrevHistory.Visible = True Then
                        RefreshHistory()
                    End If
                End If

            End If
            'frmPatMed.HidePanel()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

            'If IsNothing(frmPatMed) = False Then
            '    frmPatMed.Dispose()
            '    frmPatMed = Nothing
            'End If
            'If Not IsNothing(frmPatMed) Then
            '    frmPatMed.HidePanel()
            'End If


        End Try
    End Sub

    '19-Apr-13 Aniket: Not used function. To be deleted
    ' ''Procedure to fill all history categories in targetTreeView
    'Private Function FillHistoryCategory() As DataTable
    '    Try

    '        Dim CategoryTable As New DataTable
    '        CategoryTable = objclsPatientHistory.GetAllCategory("History")
    '        Return CategoryTable

    '    Catch ex As Exception
    '        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
    '        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    Finally

    '    End Try
    'End Function

    Public Sub ShowHidePreviousHistory(ByVal strText As String)
        If pnlPrevHistory.Visible = False Then
            If strText = "Show" Then

                Call FillPrevHistory()
                trvPrevHistory.SelectedNode = trvPrevHistory.Nodes(0)
                pnlPrevHistory.Visible = True
                'btnPrevHistory.Text = "Hide Prev History"
                tblShow.Text = "Hi&de"
                tblShow.Image = Global.gloEMR.My.Resources.Resources.Hide
                tblShow.ImageAlign = ContentAlignment.MiddleCenter

                tblShow.ToolTipText = "Hide Patient History"
                trvPrevHistory.ExpandAll()
                txtsearchhistory.Select()
            End If
        Else
            If strText = "Hide" Then
                pnlPrevHistory.Visible = False
                'btnPrevHistory.Text = "Show Prev History"
                tblShow.Text = "Sh&ow"
                tblShow.Image = Global.gloEMR.My.Resources.Resources.Show
                tblShow.ImageAlign = ContentAlignment.MiddleCenter

                tblShow.ToolTipText = "Show Patient History"

                '19-Apr-13 Aniket: Resolving Memory Leak Issues
                trvPrevHistory.Nodes.Item(0).Nodes.Clear()
                trvPrevHistory.Nodes.Clear()

            End If
        End If
    End Sub

    Public Sub ShowHideHistoryNarrative(ByVal blnPushed As Boolean)
        If pnlWordComp.Visible = False Then
            If blnPushed = True Then
                pnlWordComp.Visible = True
            End If
        Else
            If blnPushed = False Then
                pnlWordComp.Visible = False
            End If
        End If

    End Sub

    Private Sub trvPrevHistory_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles trvPrevHistory.AfterSelect
        Dim dt As DataTable = Nothing
        Try

            If trvPrevHistory.SelectedNode.Text = "Patient History" Then
                Exit Sub
            End If
            If IsNothing(trvPrevHistory.SelectedNode.Parent) = True Then
                Exit Sub
            End If

            If trvPrevHistory.SelectedNode.Parent Is trvPrevHistory.Nodes(0) Then
                Exit Sub
            End If

            Dim ChildNode As myTreeNode
            If InStr(trvPrevHistory.SelectedNode.Text, ":-") = 0 And InStr(trvPrevHistory.SelectedNode.Text, ":") = 0 Then
                '''' If is Visit Node is Selected
                ChildNode = trvPrevHistory.SelectedNode
                ChildNode.Nodes.Clear()
                dt = objclsPatientHistory.SelectPatientHistory(ChildNode.Key, lblPatientCode.Tag)
                Dim _categorytype As String = ""
                Dim stronsetActiveStatus As String = ""
                '   Dim _arrOnsetActive() As String
                Dim IsActive As Boolean = False
                Dim IsOnsetDate As Boolean = False
                Dim _HistoryType As String = ""
                For Each historyRow As DataRow In dt.Rows
                    Dim HistoryNode As New myTreeNode
                    HistoryNode.ForeColor = ChildNode.ForeColor
                    HistoryNode.ImageIndex = 16
                    HistoryNode.SelectedImageIndex = 16
                    HistoryNode.OrderTime = historyRow.Item("DOEAllergy").ToString ''bind DOEAllergy Date.
                    HistoryNode.NDCCode = historyRow.Item("sNDCCode").ToString ''bind sNDCCode.
                    HistoryNode.ConceptCode = historyRow.Item("sConceptID").ToString ''bind sConceptID.
                    HistoryNode.DMTemplateName = historyRow.Item("sDescriptionID").ToString ''bind sDescriptionID.
                    HistoryNode.NodeName = historyRow.Item("sSnoMedID").ToString ''bind sSnoMedID.
                    HistoryNode.Duration = historyRow.Item("sDescription").ToString ''bind sDescription.
                    HistoryNode.Route = historyRow.Item("sTranID1").ToString ''bind sTranID1.---Rxnorm
                    HistoryNode.DrugQtyQualifier = historyRow.Item("sTranID2").ToString ''bind sTranID2.
                    HistoryNode.DrugName = historyRow.Item("sTranID3").ToString ''bind sTranID3.
                    HistoryNode.DrugForm = historyRow.Item("sICD9").ToString ''bind sICD9.
                    HistoryNode.CPT = historyRow.Item("CPT").ToString ''bind sICD9.
                    HistoryNode.Dosage = historyRow.Item("sTranUser").ToString ''bind sTranUser.
                    HistoryNode.nHistoryID = historyRow.Item("nHistoryID").ToString ''bind sTranUser.
                    HistoryNode.OnsetDate = historyRow.Item("dtOnsetDate").ToString ''bind Onset.
                    HistoryNode.nRowOrder = historyRow.Item("nRowOrder").ToString ''bind nID.
                    HistoryNode.HistoryType = historyRow.Item("sHistoryType").ToString ''bind sHistoryType.

                    '29-Mar-13 Aniket: Addition of source column on the History screen
                    HistoryNode.HistorySource = historyRow.Item("sHistorySource").ToString ''bind sHistoryType.
                    HistoryNode.nICDRevision = Convert.ToInt16(historyRow.Item("nICDRevision"))     ''added for ICD10 implementation
                    HistoryNode.ReasonConceptCode = Convert.ToString(historyRow.Item("sReasonConceptID"))
                    HistoryNode.ReasonConceptDesc = Convert.ToString(historyRow.Item("sReasonConceptDesc"))
                    HistoryNode.LoincCode = Convert.ToString(historyRow.Item("sLoincCode"))
                    HistoryNode.LoincDescr = Convert.ToString(historyRow.Item("sLoincDescr"))
                    ' HistoryNode.RefusalCode = Convert.ToString(historyRow.Item("srefusalcode"))
                    '  HistoryNode.RefusalDescr = Convert.ToString(historyRow.Item("srefusalDescr"))

                    HistoryNode.CQMCode = Convert.ToString(historyRow.Item("sValueSetOID"))
                    HistoryNode.CQMDesc = Convert.ToString(historyRow.Item("sValueSetName"))
                    HistoryNode.nDeviceListID = Convert.ToString(historyRow.Item("nDeviceList_ID"))
                    HistoryNode.sProcStatus = Convert.ToString(historyRow.Item("sProcStatus"))
                    HistoryNode.ResolvedEndDate = Convert.ToString(historyRow.Item("dtObservationEndDate"))
                    HistoryNode.AllergyIntelorenceCode = Convert.ToString(historyRow.Item("sAllergyIntelorenceCode"))
                    HistoryNode.sAllergySeverity = Convert.ToString(historyRow.Item("sAllergySeverity"))
                    HistoryNode.CVXCode = Convert.ToString(historyRow.Item("CVXCode"))
                    HistoryNode.CVXDesc = Convert.ToString(historyRow.Item("CVXDesc"))
                    IsActive = False
                    IsOnsetDate = False
                    _categorytype = Convert.ToString(historyRow.Item("sHistoryCategory").ToString.Trim).Trim

                    _HistoryType = Convert.ToString(HistoryNode.HistoryType).Trim
                    _categorytype = GetCategorynStatus(_HistoryType, _categorytype, IsActive, IsOnsetDate)
                    'If _categorytype = "OB Medical History" Or _categorytype = "OB Genetic History" Or _categorytype = "OB Infection History" Or _categorytype = "OB Initial Physical Examination" Then

                    'Else
                    '    If HistoryNode.HistoryType = "" Then
                    '        _categorytype = Convert.ToString(historyRow.Item("sHistoryCategory").ToString.Trim).Trim
                    '        _categorytype = getHistoryTypefromcategorymaster(_categorytype)
                    '    Else
                    '        _categorytype = Convert.ToString(HistoryNode.HistoryType).Trim
                    '    End If
                    'End If

                    'If _categorytype <> "" Then
                    '    If _categorytype.Length > 2 Then
                    '        If _categorytype = "OB Medical History" Or _categorytype = "OB Genetic History" Or _categorytype = "OB Infection History" Or _categorytype = "OB Initial Physical Examination" Then
                    '            IsActive = True
                    '            IsOnsetDate = False
                    '        Else
                    '            stronsetActiveStatus = CheckHistoryTypeinStandardTable(_categorytype)
                    '            _arrOnsetActive = stronsetActiveStatus.Split(",")
                    '            If IsNothing(_arrOnsetActive) = False Then
                    '                If _arrOnsetActive.Length >= 1 Then
                    '                    IsOnsetDate = _arrOnsetActive.GetValue(0)
                    '                    IsActive = _arrOnsetActive.GetValue(1)
                    '                End If
                    '            End If
                    '        End If


                    '    End If
                    'End If

                    ChildNode.Nodes.Add(HistoryNode)
                    If IsActive And _categorytype = "All" Then
                        Dim arr() As String = Split(historyRow.Item("sReaction"), "|")
                        Dim sAllergies As String = ""
                        If arr.Length > 0 Then
                            If arr.GetValue(0) <> "" Then
                                If arr.GetValue(1) = "InActive" Then
                                    sAllergies = arr.GetValue(0)
                                    sAllergies = sAllergies.Replace(vbNewLine, " : ")
                                    HistoryNode.Text = historyRow.Item("sHistoryCategory").ToString & " ~ " & historyRow.Item("sHistoryItem").ToString & "~" & historyRow.Item("sComments").ToString & "~ " & sAllergies 'arr.GetValue(0)
                                Else
                                    sAllergies = historyRow.Item("sReaction").ToString()
                                    sAllergies = sAllergies.Replace(vbNewLine, " : ")
                                    HistoryNode.Text = historyRow.Item("sHistoryCategory").ToString & " ~ " & historyRow.Item("sHistoryItem").ToString & "~" & historyRow.Item("sComments").ToString & "~ " & sAllergies 'historyRow.Item("sReaction")
                                End If
                            Else
                                'BELOW IF STATEMENT BY SUDHIR 20090203
                                If arr.GetValue(1) = "InActive" Then
                                    sAllergies = arr.GetValue(0)
                                    sAllergies = sAllergies.Replace(vbNewLine, " : ")
                                    HistoryNode.Text = historyRow.Item("sHistoryCategory").ToString & " ~ " & historyRow.Item("sHistoryItem").ToString & "~" & historyRow.Item("sComments").ToString & "~ " & sAllergies 'arr.GetValue(0)
                                Else
                                    sAllergies = historyRow.Item("sReaction").ToString()
                                    sAllergies = sAllergies.Replace(vbNewLine, " : ")
                                    HistoryNode.Text = historyRow.Item("sHistoryCategory").ToString & " ~ " & historyRow.Item("sHistoryItem").ToString & "~" & historyRow.Item("sComments").ToString & "~ " & historyRow.Item("sReaction")
                                End If
                            End If
                        End If
                    ElseIf _categorytype = "Fam" Then
                        Dim arr() As String = Split(historyRow.Item("sReaction"), "|")
                        Dim sAllergies As String = ""
                        If arr.Length > 0 Then
                            If arr.GetValue(0) <> "" Then
                                If arr.GetValue(1) = "InActive" Then
                                    sAllergies = arr.GetValue(0)
                                    sAllergies = sAllergies.Replace(vbNewLine, " : ")
                                    HistoryNode.Text = historyRow.Item("sHistoryCategory").ToString & " ~ " & historyRow.Item("sHistoryItem").ToString & "~" & historyRow.Item("sComments").ToString & "~ " & sAllergies 'arr.GetValue(0)
                                Else
                                    sAllergies = historyRow.Item("sReaction").ToString()
                                    sAllergies = sAllergies.Replace(vbNewLine, " : ")
                                    HistoryNode.Text = historyRow.Item("sHistoryCategory").ToString & " ~ " & historyRow.Item("sHistoryItem").ToString & "~" & historyRow.Item("sComments").ToString & "~ " & sAllergies 'historyRow.Item("sReaction")
                                End If
                            Else
                                'BELOW IF STATEMENT BY SUDHIR 20090203
                                If arr.GetValue(1) = "InActive" Then
                                    sAllergies = arr.GetValue(0)
                                    sAllergies = sAllergies.Replace(vbNewLine, " : ")
                                    HistoryNode.Text = historyRow.Item("sHistoryCategory").ToString & " ~ " & historyRow.Item("sHistoryItem").ToString & "~" & historyRow.Item("sComments").ToString & "~ " & sAllergies 'arr.GetValue(0)
                                Else
                                    sAllergies = historyRow.Item("sReaction").ToString()
                                    sAllergies = sAllergies.Replace(vbNewLine, " : ")
                                    HistoryNode.Text = historyRow.Item("sHistoryCategory").ToString & " ~ " & historyRow.Item("sHistoryItem").ToString & "~" & historyRow.Item("sComments").ToString & "~ " & historyRow.Item("sReaction")
                                End If
                            End If
                        End If

                    ElseIf historyRow.Item("sHistoryCategory").ToString.Trim = "Smoking Status" Then
                        Dim arr() As String = Split(historyRow.Item("sReaction"), "|")
                        Dim sAllergies As String = ""
                        If arr.Length > 0 Then
                            If arr.GetValue(0) <> "" Then

                                If arr.GetValue(1) = "InActive" Then
                                    sAllergies = arr.GetValue(0)
                                    sAllergies = sAllergies.Replace(vbNewLine, " : ")
                                    HistoryNode.Text = historyRow.Item("sHistoryCategory").ToString & " ~ " & historyRow.Item("sHistoryItem").ToString & "~" & historyRow.Item("sComments").ToString & "~ " & sAllergies 'arr.GetValue(0)
                                Else
                                    sAllergies = historyRow.Item("sReaction").ToString()
                                    sAllergies = sAllergies.Replace(vbNewLine, " : ")
                                    HistoryNode.Text = historyRow.Item("sHistoryCategory").ToString & " ~ " & historyRow.Item("sHistoryItem").ToString & "~" & historyRow.Item("sComments").ToString & "~ " & sAllergies 'historyRow.Item("sReaction")
                                End If
                            Else
                                'BELOW IF STATEMENT BY SUDHIR 20090203
                                If arr.GetValue(1) = "InActive" Then
                                    sAllergies = arr.GetValue(0)
                                    sAllergies = sAllergies.Replace(vbNewLine, " : ")
                                    HistoryNode.Text = historyRow.Item("sHistoryCategory").ToString & " ~ " & historyRow.Item("sHistoryItem").ToString & "~" & historyRow.Item("sComments").ToString & "~ " & sAllergies 'arr.GetValue(0)
                                Else
                                    sAllergies = historyRow.Item("sReaction").ToString()
                                    sAllergies = sAllergies.Replace(vbNewLine, " : ")
                                    HistoryNode.Text = historyRow.Item("sHistoryCategory").ToString & " ~ " & historyRow.Item("sHistoryItem").ToString & "~" & historyRow.Item("sComments").ToString & "~ " & sAllergies 'historyRow.Item("sReaction")
                                End If
                            End If
                            'Else
                            '    HistoryNode.Text = historyRow.Item("sHistoryCategory").ToString & " ~ " & historyRow.Item("sHistoryItem").ToString & "~" & historyRow.Item("sComments").ToString & "~ " & arr.GetValue(0)

                        End If
                    ElseIf IsActive Then
                        Dim arr() As String = Split(historyRow.Item("sReaction"), "|")
                        Dim sAllergies As String = ""
                        If arr.Length > 0 Then
                            If arr.GetValue(0) <> "" Then
                                If arr.GetValue(1) = "InActive" Then
                                    sAllergies = arr.GetValue(0)
                                    sAllergies = sAllergies.Replace(vbNewLine, " : ")
                                    HistoryNode.Text = historyRow.Item("sHistoryCategory").ToString & " ~ " & historyRow.Item("sHistoryItem").ToString & "~" & historyRow.Item("sComments").ToString & "~ " & sAllergies 'arr.GetValue(0)
                                Else
                                    sAllergies = historyRow.Item("sReaction").ToString()
                                    sAllergies = sAllergies.Replace(vbNewLine, " : ")
                                    HistoryNode.Text = historyRow.Item("sHistoryCategory").ToString & " ~ " & historyRow.Item("sHistoryItem").ToString & "~" & historyRow.Item("sComments").ToString & "~ " & sAllergies 'historyRow.Item("sReaction")
                                End If
                            Else
                                'BELOW IF STATEMENT BY SUDHIR 20090203
                                If arr.Length > 1 Then


                                    If arr.GetValue(1) = "InActive" Then
                                        sAllergies = arr.GetValue(0)
                                        sAllergies = sAllergies.Replace(vbNewLine, " : ")
                                        HistoryNode.Text = historyRow.Item("sHistoryCategory").ToString & " ~ " & historyRow.Item("sHistoryItem").ToString & "~" & historyRow.Item("sComments").ToString & "~ " & sAllergies 'arr.GetValue(0)
                                    Else
                                        sAllergies = historyRow.Item("sReaction").ToString()
                                        sAllergies = sAllergies.Replace(vbNewLine, " : ")
                                        HistoryNode.Text = historyRow.Item("sHistoryCategory").ToString & " ~ " & historyRow.Item("sHistoryItem").ToString & "~" & historyRow.Item("sComments").ToString & "~ " & historyRow.Item("sReaction")
                                    End If
                                Else
                                    sAllergies = historyRow.Item("sReaction").ToString()
                                    sAllergies = sAllergies.Replace(vbNewLine, " : ")
                                    HistoryNode.Text = historyRow.Item("sHistoryCategory").ToString & " ~ " & historyRow.Item("sHistoryItem").ToString & "~" & historyRow.Item("sComments").ToString & "~ " & historyRow.Item("sReaction")

                                End If
                            End If
                        End If
                    Else

                        HistoryNode.Text = historyRow.Item("sHistoryCategory").ToString & " ~ " & historyRow.Item("sHistoryItem").ToString & "~ " & historyRow.Item("sComments").ToString
                    End If
                    HistoryNode.Tag = historyRow.Item("MedicalCondition_Id")
                    If Convert.ToInt64(historyRow.Item("nDrugID")) = 0 Then
                        HistoryNode.Key = 0
                    Else
                        HistoryNode.Key = Convert.ToInt64(historyRow.Item("nDrugID"))
                    End If
                    HistoryNode = Nothing 'Change made to solve memory Leak and word crash issue
                Next historyRow
            End If
            trvPrevHistory.ExpandAll()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "Patient History", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            'Change made to solve memory Leak and word crash issue
            If Not dt Is Nothing Then
                dt.Dispose()
                dt = Nothing
            End If
        End Try
    End Sub

    Private Sub trvPrevHistory_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles trvPrevHistory.DoubleClick
        Try
            Call trvPrevHistory_DblClick()
            cmbAllergyType_SelectedIndexChanged(Nothing, Nothing)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Set_RecordLock(ByVal locked As Boolean)
        If locked = True Then
            tblSave.Enabled = False
            tblReviewofHistory.Enabled = False

        Else
            tblSave.Enabled = True
            'Bug #55283: History - When We Click on Review Button It is prompt Message for saving
            If C1HistoryDetails.Rows.Count > 1 Then
                tblReviewofHistory.Enabled = True
            Else
                tblReviewofHistory.Enabled = False
            End If
        End If
    End Sub

    Private Sub trvPrevHistory_DblClick()
        If trvPrevHistory.SelectedNode.Text = "Patient History" Then
            Exit Sub
        End If

        If trvPrevHistory.SelectedNode.Parent Is trvPrevHistory.Nodes(0) Then
            Exit Sub
        End If

        If trvPrevHistory.SelectedNode.GetNodeCount(False) = 0 Then
            Exit Sub
        End If
        Dim strloincCode As String = ""
        Dim strloincDesc As String = ""



        Dim i As Integer ', j, k As Integer
        Dim DateNode As myTreeNode
        DateNode = trvPrevHistory.SelectedNode
        If (IsNothing(gloUC_PatientStrip1) = False) Then
            gloUC_PatientStrip1.DTP.Value = Convert.ToDateTime(trvPrevHistory.SelectedNode.Text)
        End If


        If IsNothing(trvPrevHistory.SelectedNode.Parent) = False Then
            If trvPrevHistory.SelectedNode.GetNodeCount(False) > 0 Then
                dsHistory.Tables("History").Clear()
                dsHistory.AcceptChanges()
            End If
        End If
        lblVisitDate.Tag = DateNode.Key()
        lblVisitDate.Text = DateNode.Text

        '''''''' Check If History Is Reviewed Or NOT
        lblReviewed.Visible = False
        Dim strdtReviewDate As String
        'SLR:Free previous memory
        If Not IsNothing(dtReview) Then
            dtReview.Dispose()
            dtReview = Nothing
        End If
        dtReview = objclsPatientHistory.GetReviewHistory(lblPatientCode.Tag, lblVisitDate.Tag)

        If IsNothing(dtReview) = False Then

            If dtReview.Rows.Count > 0 Then
                strdtReviewDate = dtReview.Rows(0)("dtReviewDate")
                lblReviewed.Visible = True
                'SLR:Free previous memory
                If Not IsNothing(oclsLogin) Then
                    oclsLogin.Dispose()
                    oclsLogin = Nothing
                End If
                oclsLogin = New clsLogin
                Dim strLoginNameDetails As String = oclsLogin.GetLoginFullName(gstrLoginName)

                If strLoginNameDetails.Trim <> "" Then
                    lblReviewed.Text = "History Reviewed By " + strLoginNameDetails + " on " + strdtReviewDate
                Else
                    lblReviewed.Text = "History Reviewed By " + gstrLoginName + " on " + strdtReviewDate
                End If
                oclsLogin = Nothing 'Change made to solve memory Leak and word crash issue
            End If
        End If
        _isDoubleclickPrevHistory = True
        ' Dim arrSplit() As String

        For i = 0 To trvPrevHistory.SelectedNode.GetNodeCount(False) - 1
            blnModify = True
            Dim _categorytype As String = ""
            Dim stronsetActiveStatus As String = ""
            '  Dim _arrOnsetActive() As String
            Dim IsActive As Boolean = False
            Dim IsOnsetDate As Boolean = False
            Dim strCategory As String
            Dim strHistory As String
            Dim strComment As String
            Dim strRection_Status As String
            Dim intDrugID As TreeNode = DateNode.Nodes(i)
            Dim intMedicalConditionId As Integer = DateNode.Nodes.Item(i).Tag
            Dim ndrugid As Integer = CType(intDrugID, myTreeNode).Key()
            Dim arr() As String 'Srting Array
            arr = Split(Trim(DateNode.Nodes.Item(i).Text), "~")
            Dim thisNode As myTreeNode = CType(DateNode.Nodes.Item(i), myTreeNode)
            ''Added Rahul on 20101007
            Dim DOEAllergy As String = thisNode.OrderTime.ToString()
            If DOEAllergy = "1/1/1900 12:00:00 AM" Then
                DOEAllergy = ""
            End If

            Dim sConceptID As String = thisNode.ConceptCode.ToString()
            Dim sNDCCode As String = thisNode.NDCCode.ToString()
            Dim sDescriptionID As String = thisNode.DMTemplateName.ToString()
            Dim sSnoMedID As String = thisNode.NodeName.ToString()
            Dim sDescription As String = thisNode.Duration.ToString()
            Dim sTranID1 As String = thisNode.Route.ToString() ''''Rxnorm
            Dim sTranID2 As String = thisNode.DrugQtyQualifier.ToString()
            Dim sTranID3 As String = thisNode.DrugName.ToString()
            Dim sICD9 As String = thisNode.DrugForm.ToString()
            Dim CPT As String = thisNode.CPT.ToString()
            Dim sTranUser As String = thisNode.Dosage.ToString()
            Dim nHistoryID As Int64 = thisNode.nHistoryID
            Dim strOnsetDate As String = thisNode.OnsetDate.ToString()
            '  Dim strDateResolved As String = thisNode.DateResolved.ToString()
            Dim nRowOrder As Int64 = thisNode.nRowOrder
            Dim sHistoryType As String = thisNode.HistoryType.ToString()

            '29-Mar-13 Aniket: Addition of source column on the History screen
            Dim sHistorySource As String = thisNode.HistorySource.ToString()
            ''
            Dim nICDRevision As Int16 = thisNode.nICDRevision     ''added for ICD10 implementation
            Dim sReasonConceptCode As String = thisNode.ReasonConceptCode.ToString()
            Dim sReasonConceptDesc As String = thisNode.ReasonConceptDesc.ToString()
            Dim nDeviceList_ID As Long = thisNode.nDeviceListID
            Dim sProcStatus As String = thisNode.sProcStatus
            strCategory = CStr(arr.GetValue(0)).Trim
            strHistory = CStr(arr.GetValue(1)).Trim
            strComment = CStr(arr.GetValue(2)).Trim
            _categorytype = strCategory
            Dim _HistoryType As String = ""
            _HistoryType = Convert.ToString(sHistoryType).Trim
            _categorytype = GetCategorynStatus(_HistoryType, _categorytype, IsActive, IsOnsetDate)

            If IsActive And _categorytype = "All" Then
                strRection_Status = CStr(arr.GetValue(3)).Trim
                strRection_Status = strRection_Status.Replace(":", vbNewLine)
            ElseIf _categorytype = "Fam" Then

                strRection_Status = ""
                Dim Rel() As String = arr.GetValue(3).ToString().Split(":")
                For ii As Integer = 0 To Rel.Length - 2
                    If strRection_Status = "" Then
                        strRection_Status = Rel(ii)
                    Else
                        strRection_Status = strRection_Status + ":" + Rel(ii)
                    End If
                Next
                'strRection_Status = CStr(arr.GetValue(3)).Trim
                strRection_Status = strRection_Status.Replace(":", vbNewLine)

            ElseIf strCategory.ToString.Trim = "Smoking Status" Then
                strRection_Status = CStr(arr.GetValue(3)).Trim
                strRection_Status = strRection_Status.Replace(":", vbNewLine)
            ElseIf IsActive Then
                strRection_Status = CStr(arr.GetValue(3)).Trim
            Else
                strRection_Status = ""
            End If
            strloincCode = Convert.ToString(thisNode.LoincCode)
            strloincDesc = Convert.ToString(thisNode.LoincDescr)

            'sReasonConceptCode = Convert.ToString(thisNode.ReasonConceptCode)
            'sReasonConceptDesc = Convert.ToString(thisNode.ReasonConceptDesc)
            Dim strCQMCode As String = thisNode.CQMCode.ToString()
            Dim strCQMDescription As String = thisNode.CQMDesc.ToString()

            Dim strResolvedEndDate As String = Convert.ToString(thisNode.ResolvedEndDate)
            Dim strAllergyIntelorenceCode As String = Convert.ToString(thisNode.AllergyIntelorenceCode)
            Dim strAllergySeverity As String = Convert.ToString(thisNode.sAllergySeverity)
            Dim sCVXcode As String = Convert.ToString(thisNode.CVXCode)
            Dim sCVXDesc As String = Convert.ToString(thisNode.CVXDesc)

            Call FillGrid(strCategory, strHistory, strComment, strRection_Status, ndrugid, intMedicalConditionId, DOEAllergy, sConceptID, sDescriptionID, sSnoMedID, sDescription, sTranID1, sTranID2, sTranID3, sICD9, sTranUser, sNDCCode, "", 0, nHistoryID, strOnsetDate, CPT, nRowOrder, sHistoryType, sHistorySource, nICDRevision, sReasonConceptCode, sReasonConceptDesc, strloincCode, strloincDesc, "", strCQMCode, strCQMDescription, nDeviceList_ID:=nDeviceList_ID, sProcStatus:=sProcStatus, ResolvedEndDate:=strResolvedEndDate, AllergySeverity:=strAllergySeverity, sAllergyIntelorenceCode:=strAllergyIntelorenceCode, CVxCode:=sCVXcode, CVXDesc:=sCVXDesc)
        Next

        C1HistoryDetails.Select(1, Col_CategoryID, 1, Col_DrugID, True)
        C1HistoryDetails.Focus()
        C1HistoryDetails.Refresh()

        Dim dt As DataTable
        dt = objclsPatientHistory.SelectNarration(lblVisitDate.Tag, lblPatientCode.Tag)

        ' Word Object
        Dim mstream As ADODB.Stream
        Dim strFileName As String
        mstream = New ADODB.Stream
        mstream.Type = ADODB.StreamTypeEnum.adTypeBinary
        '        wdNarration.Close()
        mstream.Open()
        If dt.Rows.Count > 0 Then
            mstream.Write(dt.Rows(0)(0))
            strFileName = gloSettings.FolderSettings.AppTempFolderPath & getUniqueID() & "temp5.txt" 'SLR: Changed temp5 to uniqueID
            mstream.SaveToFile(strFileName, ADODB.SaveOptionsEnum.adSaveCreateOverWrite)
            wdNarration.LoadFile(strFileName)

            mstream.Close()
            mstream = Nothing 'Change made to solve memory Leak and word crash issue
            If wdNarration.TextLength > 0 Then
                pnlWordComp.Visible = True
            Else
                pnlWordComp.Visible = False
            End If
        Else
            wdNarration.Text = ""
            pnlWordComp.Visible = False
        End If

        'SLR: Free dt
        If Not IsNothing(dt) Then
            dt.Dispose()
            dt = Nothing
        End If
    End Sub

    Private Sub PopulateNarration()
        Dim dt As DataTable
        dt = dsHistory.Tables("Narration")
        Dim mstream As ADODB.Stream
        Dim strFileName As String
        mstream = New ADODB.Stream
        mstream.Type = ADODB.StreamTypeEnum.adTypeBinary
        mstream.Open()
        If dt.Rows.Count > 0 Then
            mstream.Write(dt.Rows(0)(0))
            strFileName = gloSettings.FolderSettings.AppTempFolderPath & getUniqueID() & "temp5.txt" 'SLR: Changed temp5 to uniqueID
            If File.Exists(strFileName) Then
                Dim oFileInfo As New FileInfo(strFileName)
                oFileInfo.IsReadOnly = False
                oFileInfo = Nothing
            End If
            mstream.SaveToFile(strFileName, ADODB.SaveOptionsEnum.adSaveCreateOverWrite)
            wdNarration.LoadFile(strFileName)
            mstream.Close()
            mstream = Nothing 'Change made to solve memory Leak and word crash issue
            If wdNarration.TextLength > 0 Then
                pnlWordComp.Visible = True
            Else
                pnlWordComp.Visible = False
            End If
        Else
            pnlWordComp.Visible = False
        End If
        'SLR: Free dt
        If Not IsNothing(dt) Then
            dt.Dispose()
            dt = Nothing
        End If
    End Sub

    Public Sub mnuRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuRemove.Click


        Try

            Dim i As Integer
            i = C1HistoryDetails.Row
            If i > 0 Then

                If Not IsNothing(C1HistoryDetails.Rows.Item(0)) Then

                    Try
                        Dim cnt As Int16 = 0
                        Dim _selRow As Integer = C1HistoryDetails.Row

                        Dim newRow As DataRow = Nothing

                        With C1HistoryDetails
                            If Convert.ToString(C1HistoryDetails.GetData(_selRow, Col_HsHistoryItem)) = "" Then
                                ValidateDataDictionary(.GetData(_selRow, Col_HHidden))
                                For i = C1HistoryDetails.Rows.Count - 1 To _selRow Step -1
                                    If cnt = _selRow Then
                                        Exit For
                                    End If
                                    'Bug #61329: 00000599 : patient has multiple history items in different casing the system will throw an error.
                                    If C1HistoryDetails.GetData(_selRow, Col_HHidden).ToString.ToUpper = C1HistoryDetails.GetData(_selRow + 1, Col_HHidden).ToString.ToUpper Then
                                        cnt = i - 1
                                        ' .Rows.Remove(_selRow + 1)
                                        newRow = _dsTemp.Tables("History").NewRow()
                                        newRow.ItemArray = dsHistory.Tables("History").Rows(_selRow).ItemArray
                                        newRow.Item(27) = "Deleted"
                                        _dsTemp.Tables("History").Rows.Add(newRow)

                                        dsHistory.Tables("History").Rows.RemoveAt(_selRow)

                                    End If
                                Next
                                newRow = _dsTemp.Tables("History").NewRow()
                                newRow.ItemArray = dsHistory.Tables("History").Rows(_selRow - 1).ItemArray
                                newRow.Item(27) = "Deleted"
                                _dsTemp.Tables("History").Rows.Add(newRow)
                                dsHistory.Tables("History").Rows.RemoveAt(_selRow - 1)

                            Else
                                Dim k As Integer = 0
                                For j As Integer = 0 To C1HistoryDetails.Rows.Count - 1
                                    'Bug #61329: 00000599 : patient has multiple history items in different casing the system will throw an error.
                                    If C1HistoryDetails.GetData(j, Col_HHidden).ToString.ToUpper = C1HistoryDetails.GetData(_selRow, Col_HHidden).ToString.ToUpper Then
                                        k = k + 1
                                    End If
                                Next

                                If k = 2 Then
                                    ValidateDataDictionary(C1HistoryDetails.GetData(_selRow - 1, Col_HHidden))
                                    '  .Rows.Remove(_selRow)
                                    ' .Rows.Remove(_selRow - 1)
                                    newRow = _dsTemp.Tables("History").NewRow()
                                    newRow.ItemArray = dsHistory.Tables("History").Rows(_selRow - 1).ItemArray
                                    newRow.Item(27) = "Deleted"
                                    _dsTemp.Tables("History").Rows.Add(newRow)
                                    newRow = _dsTemp.Tables("History").NewRow()
                                    newRow.ItemArray = dsHistory.Tables("History").Rows(_selRow - 2).ItemArray
                                    newRow.Item(27) = "Deleted"
                                    _dsTemp.Tables("History").Rows.Add(newRow)
                                    dsHistory.Tables("History").Rows.RemoveAt(_selRow - 1)
                                    dsHistory.Tables("History").Rows.RemoveAt(_selRow - 2)
                                Else
                                    ' .Rows.Remove(_selRow)
                                    newRow = _dsTemp.Tables("History").NewRow()
                                    newRow.ItemArray = dsHistory.Tables("History").Rows(_selRow - 1).ItemArray
                                    newRow.Item(27) = "Deleted"
                                    _dsTemp.Tables("History").Rows.Add(newRow)

                                    dsHistory.Tables("History").Rows.RemoveAt(_selRow - 1)

                                End If

                            End If

                        End With
                        ' dsHistory.Tables("History").AcceptChanges()
                        gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Delete, "History Item Deleted", gloAuditTrail.ActivityOutCome.Success)
                    Catch ex As Exception
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Remove, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                        C1HistoryDetails.Rows.Remove(C1HistoryDetails.Row)
                    Finally
                        SetNKHistoryVisibility()
                    End Try
                End If
            End If
            isHistoryModified = True
            C1HistoryDetails_EnterCell(Nothing, Nothing)

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Remove, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "Patient History", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If C1HistoryDetails.Rows.Count = 1 Then
                ' trvDefination.Nodes.Clear()
                'trvICD9.Nodes.Clear()
            End If
            C1HistoryDetails.ContextMenu = Nothing
        End Try


    End Sub

    Private Sub AddBasicVoiceCommands()
        Try
            voicecol.Clear()
            voicecol.Add("Save it")
            voicecol.Add("Close form")
            voicecol.Add("New form")
            voicecol.Add("Previous Item")
            voicecol.Add("Next Item")
            voicecol.Add("Select Item")

            HistoryVoiceCol.Clear()
            HistoryVoiceCol.Add("Show Previous")
            HistoryVoiceCol.Add("Show Current")
            HistoryVoiceCol.Add("Show Yesterday")
            HistoryVoiceCol.Add("Show LastWeek")
            HistoryVoiceCol.Add("Show LastMonth")
            HistoryVoiceCol.Add("Show Older")
            HistoryVoiceCol.Add("Finish History")
            HistoryVoiceCol.Add("Show Narrative")
            HistoryVoiceCol.Add("Previous History Item")
            HistoryVoiceCol.Add("Next History Item")
            HistoryVoiceCol.Add("Hide Previous")
            HistoryVoiceCol.Add("Previous Patient History")
            HistoryVoiceCol.Add("Next Patient History")
            HistoryVoiceCol.Add("Delete Patient History")
            HistoryVoiceCol.Add("Modify Patient History")
            HistoryVoiceCol.Add("Save and Close it")
            HistoryVoiceCol.Add("Hide Narrative")  'Hide Narrative
            HistoryVoiceCol.Add("Delete History Item")
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.VoiceCommands, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Public Sub PreviousHistory(ByVal intnode As Int16)
        Dim e As New System.Windows.Forms.TreeViewEventArgs(trvPrevHistory.Nodes.Item(0).Nodes.Item(intnode), TreeViewAction.ByMouse)
        Dim objSender As Object = Nothing
        Call trvPrevHistory_AfterSelect(objSender, e)

        trvPrevHistory.SelectedNode = trvPrevHistory.Nodes.Item(0).Nodes.Item(intnode)
        e = Nothing
    End Sub


    Public Sub tblHistory_ButtonClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolBarButtonClickEventArgs)
        Try
            Select Case e.Button.Text
                Case "&New"
                    NewHistory()
                Case "&Save"
                    If IsNothing(C1HistoryDetails.Editor) = False Then
                        If (C1HistoryDetails.Col = Col_HsComments) And (C1HistoryDetails.Editor.Visible) And (C1HistoryDetails.Editor.Text.Length > MAX_COMMENT_LENGHT) Then
                            MessageBox.Show("Comment should be less than equal to 760 characters.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Exit Sub
                        End If
                    End If
                    Call SaveHistoryData()


                    OpenMedication()
                Case "&Finish"
                Case "Narrative"
                    ShowHideHistoryNarrative(e.Button.Pushed)
                Case "Show", "Hide"
                    ShowHidePreviousHistory(e.Button.Text)
                Case "&Close"
                    Call CloseHistory()
                Case "&Review"
                    Call ShowReview()
                Case "&Hx"
                    Call ShowHistoryOfHistory()


            End Select
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Public Sub NavigatePreviousPatientHistory(ByVal blnKeyValue As Boolean)
        If Not IsNothing(trvPrevHistory.SelectedNode) Then
            If blnKeyValue Then


                If trvPrevHistory.SelectedNode Is trvPrevHistory.Nodes.Item(0) Then
                    trvPrevHistory.Select()
                    trvPrevHistory.SelectedNode = CType(trvPrevHistory.Nodes.Item(0).Nodes.Item(0).FirstNode, myTreeNode)


                ElseIf trvPrevHistory.SelectedNode.Parent Is trvPrevHistory.Nodes.Item(0) Then
                    trvPrevHistory.Select()
                    trvPrevHistory.SelectedNode = CType(trvPrevHistory.SelectedNode.FirstNode, myTreeNode)


                ElseIf trvPrevHistory.SelectedNode.Parent.Parent Is trvPrevHistory.Nodes(0) Then
                    If Not trvPrevHistory.SelectedNode Is trvPrevHistory.SelectedNode.Parent.LastNode Then
                        trvPrevHistory.Select()
                        trvPrevHistory.SelectedNode = CType(trvPrevHistory.SelectedNode.NextNode, myTreeNode)
                    Else


                        trvPrevHistory.Select()
                    End If
                End If
            Else

                If Not trvPrevHistory.SelectedNode Is trvPrevHistory.Nodes.Item(0) Then

                    If trvPrevHistory.SelectedNode.Parent Is trvPrevHistory.Nodes.Item(0) Then
                        trvPrevHistory.Select()
                        trvPrevHistory.SelectedNode = CType(trvPrevHistory.SelectedNode.PrevNode.FirstNode, myTreeNode)

                    ElseIf trvPrevHistory.SelectedNode.Parent.Parent Is trvPrevHistory.Nodes.Item(0) Then
                        If Not trvPrevHistory.SelectedNode Is trvPrevHistory.Nodes.Item(0).Nodes.Item(0).FirstNode Then
                            trvPrevHistory.Select()
                            trvPrevHistory.SelectedNode = CType(trvPrevHistory.SelectedNode.PrevNode, myTreeNode)

                        Else
                            trvPrevHistory.Select()
                        End If
                    End If
                End If
            End If
        Else
            If trvPrevHistory.GetNodeCount(False) > 0 Then
                trvPrevHistory.SelectedNode = trvPrevHistory.Nodes.Item(0)
            End If
        End If
    End Sub
    Private Sub txtsearchhistory_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtsearchhistory.Validating
        Try
            If cmbsearchHistory.Text = "History Date" Then
                If IsDate(txtsearchhistory.Text) Then
                    Dim mynode As myTreeNode
                    'Root node collection
                    For Each mynode In trvPrevHistory.Nodes.Item(0).Nodes
                        Dim mychildnode As myTreeNode
                        'child node collection
                        For Each mychildnode In mynode.Nodes
                            If CType(mychildnode.Text, DateTime).Date = Trim(txtsearchhistory.Text) Then
                                mynode.Parent.ExpandAll()
                                trvPrevHistory.SelectedNode = mychildnode
                                Exit Sub
                            End If
                        Next
                    Next
                End If
            Else
                If Trim(txtsearchhistory.Text) <> "" Then
                    Dim mynode As myTreeNode
                    'Root node collection
                    'mynode is current/yesterday/last week/last month
                    For Each mynode In trvPrevHistory.Nodes.Item(0).Nodes

                        Dim mychildnode As myTreeNode
                        'mychildnode is prescriptiondate annd prescription
                        For Each mychildnode In mynode.Nodes
                            Dim myHistoryNode As TreeNode
                            For Each myHistoryNode In mychildnode.Nodes
                                If Trim(myHistoryNode.Text) <> "" Then
                                    Dim arrstring() As String
                                    arrstring = Split(myHistoryNode.Text, "~")
                                    'If Len(arrstring) > 0 Then
                                    Dim str As String
                                    str = UCase(CType(arrstring.GetValue(0), String))
                                    If Mid(str, 1, Len(UCase(Trim(txtsearchhistory.Text)))) = UCase(Trim(txtsearchhistory.Text)) Then
                                        mynode.Parent.ExpandAll()
                                        trvPrevHistory.SelectedNode = myHistoryNode
                                        Exit Sub
                                    End If
                                    'End If
                                End If
                            Next
                        Next
                    Next
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, "Patient History", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtsearchhistory_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtsearchhistory.KeyPress
        If (e.KeyChar = ChrW(13)) Then
            trvPrevHistory.Select()
        Else
            trvPrevHistory.SelectedNode = trvPrevHistory.Nodes.Item(0)
        End If
    End Sub

    Private Sub mnuMakeCurrent_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuMakeCurrent.Click
        Try

            If trvPrevHistory.SelectedNode.Text = "Patient History" Then
                Exit Sub
            End If

            '
            If IsNothing(trvPrevHistory.SelectedNode.Parent) = True Then
                Exit Sub
            End If

            If trvPrevHistory.SelectedNode.Text = "Current" Then
                Exit Sub
            ElseIf trvPrevHistory.SelectedNode.Text = "Yesterday" Then
                Exit Sub
            ElseIf trvPrevHistory.SelectedNode.Text = "Last Week" Then
                Exit Sub
            ElseIf trvPrevHistory.SelectedNode.Text = "Last Month" Then
                Exit Sub
            ElseIf trvPrevHistory.SelectedNode.Text = "Older" Then
                Exit Sub
            End If

            If trvPrevHistory.SelectedNode.Parent.Parent.Text = "Patient History" Then

                If MessageBox.Show("Are you sure, you want to make this history as current history?", "Patient History", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = DialogResult.Yes Then
                    ' Dim i As Integer

                    Dim blntrvTargetFull As Boolean

                    If C1HistoryDetails.Rows.Count >= 1 Then
                        blntrvTargetFull = True
                    End If
                    If blntrvTargetFull = True Then

                        If MessageBox.Show("Do you want to save currently opened History?", "Patient History", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = DialogResult.Yes Then
                            If IsNothing(dsHistory.GetChanges) = False Then
                                dsCurrent = dsHistory.Copy()
                                Dim s As Int16

                                For s = 0 To dsCurrent.Tables("History").Rows.Count - 1
                                    If dsCurrent.Tables("History").Rows(s).RowState <> DataRowState.Deleted Then
                                        dsCurrent.Tables("history").Rows(s)("RowState") = "Added"
                                    Else

                                        dsCurrent.Tables("History").Rows(s).RejectChanges()
                                    End If
                                Next
                                objclsPatientHistory.DeleteHistory(m_VisitID, m_PatientID)
                                Call SaveDatasetHistory_new(dsCurrent)
                            End If
                            Call MakeCurrentHistory()
                        Else
                            ''''directly open selected record for making changes
                            MakeCurrentHistory()
                        End If
                    Else
                        ''''directly open selected record for making changes
                        MakeCurrentHistory()
                    End If
                    _isMakeAsCurrent = True
                End If
            End If

            trvPrevHistory.SelectedNode = trvPrevHistory.SelectedNode.Parent
            trvPrevHistory.Select()
            ''Showing Allergies as per Allergy type when "Make as Current History".
            cmbAllergyType_SelectedIndexChanged(Nothing, Nothing)

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, "Patient History", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub MakeCurrentHistory()
        'to move History Details of that particular Date in Target TreeView  for Edit        
        Dim DateNode As myTreeNode  'SLR: new is not needed
        Dim i As Integer
        DateNode = CType(trvPrevHistory.SelectedNode, myTreeNode)


        lblVisitDate.Text = Now.Date
        lblVisitID.Tag = 0
        lblReviewed.Visible = False

        dsHistory.Tables("History").Clear()

        For i = 0 To trvPrevHistory.SelectedNode.GetNodeCount(False) - 1

            blnModify = True
            Dim _categorytype As String = ""
            Dim stronsetActiveStatus As String = ""
            ' Dim _arrOnsetActive() As String
            Dim IsActive As Boolean = False
            Dim IsOnsetDate As Boolean = False
            Dim strCategory As String
            Dim strHistory As String
            Dim strComment As String
            Dim strRection_Status As String
            Dim intDrugID As TreeNode = DateNode.Nodes(i)
            Dim ndrugid As Integer = CType(intDrugID, myTreeNode).Key()
            Dim intMedicalConditionId As Integer = DateNode.Nodes.Item(i).Tag
            Dim thisNode As myTreeNode = CType(DateNode.Nodes.Item(i), myTreeNode)
            Dim DOEAllergy As String = thisNode.OrderTime.ToString()
            If DOEAllergy = "1/1/1900 12:00:00 AM" Then
                DOEAllergy = ""
            End If

            Dim sConceptID As String = thisNode.ConceptCode.ToString()
            Dim sNDCCode As String = thisNode.NDCCode.ToString()
            Dim sDescriptionID As String = thisNode.DMTemplateName.ToString()
            Dim sSnoMedID As String = thisNode.NodeName.ToString()
            Dim sDescription As String = thisNode.Duration.ToString()
            Dim sTranID1 As String = thisNode.Route.ToString()
            Dim sTranID2 As String = thisNode.DrugQtyQualifier.ToString()
            Dim sTranID3 As String = thisNode.DrugName.ToString()
            Dim sICD9 As String = thisNode.DrugForm.ToString()
            Dim CPT As String = thisNode.CPT.ToString()
            Dim sTranUser As String = thisNode.Dosage.ToString()
            Dim nHistoryID As Int64 = thisNode.nHistoryID
            Dim strOnsetDate As String = thisNode.OnsetDate.ToString()
            Dim nRowOrder As Int64 = thisNode.nRowOrder
            Dim sHistoryType As String = thisNode.HistoryType.ToString()

            '29-Mar-13 Aniket: Addition of source column on the History screen
            Dim sHistorySource As String = thisNode.HistorySource.ToString()
            ' Dim strDateResolved As String = thisNode.DateResolved.ToString()
            Dim nICDRevision As Int32 = thisNode.nICDRevision    ''added for ICD10 implementation
            Dim sReasonConceptCode As String = thisNode.ReasonConceptCode.ToString()
            Dim sReasonConceptDesc As String = thisNode.ReasonConceptDesc.ToString()
            Dim sLoincCode As String = Convert.ToString(thisNode.LoincCode)
            Dim sLoincDesc As String = Convert.ToString(thisNode.LoincDescr)
            '  Dim sRefusalCode As String = Convert.ToString(thisNode.RefusalCode)
            '    Dim sRefusalDesc As String = Convert.ToString(thisNode.RefusalDescr)
            Dim sValueSetOID As String = Convert.ToString(thisNode.CQMCode)

            Dim sValueSetName As String = Convert.ToString(thisNode.CQMDesc)
            Dim nDeviceListID As Long = thisNode.nDeviceListID
            Dim sProcStatus As String = Convert.ToString(thisNode.sProcStatus)
            Dim strResolvedEndDate As String = Convert.ToString(thisNode.ResolvedEndDate)
            Dim strAllergyIntelorenceCode As String = Convert.ToString(thisNode.AllergyIntelorenceCode)
            Dim scvxcode As String = Convert.ToString(thisNode.CVXCode)
            Dim sCVXDesc As String = Convert.ToString(thisNode.CVXDesc)
            IsOnsetDate = False
            IsActive = False
            Dim arr() As String 'Srting Array
            arr = Split(Trim(DateNode.Nodes.Item(i).Text), "~")
            strCategory = CStr(arr.GetValue(0)).Trim
            strHistory = CStr(arr.GetValue(1)).Trim
            strComment = CStr(arr.GetValue(2)).Trim
            _categorytype = strCategory.Trim
            Dim _HistoryType As String = ""
            _HistoryType = sHistoryType.Trim
            GetCategorynStatus(_HistoryType, _categorytype, IsActive, IsOnsetDate)
            'If _categorytype = "OB Medical History" Or _categorytype = "OB Genetic History" Or _categorytype = "OB Infection History" Or _categorytype = "OB Initial Physical Examination" Then
            'Else
            '    If sHistoryType = "" Then
            '        _categorytype = strCategory.Trim
            '        _categorytype = getHistoryTypefromcategorymaster(_categorytype)
            '    Else
            '        _categorytype = sHistoryType.Trim
            '    End If
            'End If

            'If _categorytype <> "" Then
            '    If _categorytype.Length > 2 Then

            '        If _categorytype = "OB Medical History" Or _categorytype = "OB Genetic History" Or _categorytype = "OB Infection History" Or _categorytype = "OB Initial Physical Examination" Then
            '            IsActive = True
            '            IsOnsetDate = False
            '        Else
            '            stronsetActiveStatus = CheckHistoryTypeinStandardTable(_categorytype)
            '            _arrOnsetActive = stronsetActiveStatus.Split(",")
            '            If IsNothing(_arrOnsetActive) = False Then
            '                If _arrOnsetActive.Length >= 1 Then
            '                    IsOnsetDate = _arrOnsetActive.GetValue(0)
            '                    IsActive = _arrOnsetActive.GetValue(1)
            '                End If
            '            End If
            '        End If

            '    End If
            'End If
            If IsActive And _categorytype = "All" Then
                strRection_Status = CStr(arr.GetValue(3)).Trim
                strRection_Status = strRection_Status.Replace(":", vbNewLine)
            ElseIf strCategory = "Smoking Status" Then
                strRection_Status = CStr(arr.GetValue(3)).Trim
                strRection_Status = strRection_Status.Replace(":", vbNewLine)
            ElseIf IsActive Then
                strRection_Status = CStr(arr.GetValue(3)).Trim
            Else

                strRection_Status = ""
            End If



            Call FillGrid(strCategory, strHistory, strComment, strRection_Status, ndrugid, intMedicalConditionId, DOEAllergy, sConceptID, sDescriptionID, sSnoMedID, sDescription, sTranID1, sTranID2, sTranID3, sICD9, sTranUser, sNDCCode, "", 0, nHistoryID, strOnsetDate, CPT, nRowOrder, sHistoryType, sHistorySource, nICDRevision, sReasonConceptCode, sReasonConceptDesc, sLoincCode, sLoincDesc, sValueSetOID, sValueSetName, nDeviceList_ID:=nDeviceListID, sProcStatus:=sProcStatus, ResolvedEndDate:=strResolvedEndDate, sAllergyIntelorenceCode:=strAllergyIntelorenceCode, CVxCode:=scvxcode, CVXDesc:=sCVXDesc)
        Next
        C1HistoryDetails.Select(1, 0, 1, 1, True)

        Dim dt As DataTable
        dt = objclsPatientHistory.SelectNarration(DateNode.Key, lblPatientCode.Tag)

        Dim mstream As ADODB.Stream
        Dim strFileName As String
        mstream = New ADODB.Stream
        mstream.Type = ADODB.StreamTypeEnum.adTypeBinary
        '        wdNarration.Close()
        mstream.Open()
        If dt.Rows.Count > 0 Then
            mstream.Write(dt.Rows(0)(0))
            strFileName = gloSettings.FolderSettings.AppTempFolderPath & getUniqueID() & "temp5.txt" 'SLR: Changed temp5 to uniqueID
            '' SUDHIR 20100102 '' READONLY FILE WAS GIVING WRITE ERROR ''
            If File.Exists(strFileName) Then
                Dim oFileInfo As New FileInfo(strFileName)
                oFileInfo.IsReadOnly = False
                oFileInfo = Nothing
            End If
            '' END SUDHIR ''
            mstream.SaveToFile(strFileName, ADODB.SaveOptionsEnum.adSaveCreateOverWrite)

            wdNarration.LoadFile(strFileName)

            mstream.Close()

            If wdNarration.TextLength > 0 Then
                pnlWordComp.Visible = True
            Else
                pnlWordComp.Visible = False
            End If

        Else
            pnlWordComp.Visible = False
        End If

        lblVisitDate.Tag = 0
        If Not IsNothing(dt) Then  ''slr free dt
            dt.Dispose()
            dt = Nothing
        End If
        If Not IsNothing(mstream) Then  ''slr free mstream

            mstream = Nothing
        End If
    End Sub

    Public Sub mnuDeleteHistory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuDeleteHistory.Click
        Try
            If trvPrevHistory.SelectedNode.Text = "Patient History" Then
                Exit Sub
            End If
            If IsNothing(trvPrevHistory.SelectedNode.Parent) = True Then
                Exit Sub
            End If

            If trvPrevHistory.SelectedNode.Text = "Current" Then
                Exit Sub
            ElseIf trvPrevHistory.SelectedNode.Text = "Yesterday" Then
                Exit Sub
            ElseIf trvPrevHistory.SelectedNode.Text = "Last Week" Then
                Exit Sub
            ElseIf trvPrevHistory.SelectedNode.Text = "Last Month" Then
                Exit Sub
            ElseIf trvPrevHistory.SelectedNode.Text = "Older" Then
                Exit Sub
            End If

            If trvPrevHistory.SelectedNode.Parent.Parent.Text = "Patient History" Then
                Dim DateNode As myTreeNode 'SLR: new is not needed
                DateNode = CType(trvPrevHistory.SelectedNode, myTreeNode)
                If MessageBox.Show("Are you sure to delete this History?", "Patient History", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = DialogResult.Yes Then
                    objclsPatientHistory.DeleteHistory(DateNode.Key, lblPatientCode.Tag)

                    Dim oDB As New gloStream.gloDataBase.gloDataBase

                    Dim strdeleteHistory As String = "Delete ReviewHistory where nvisitID= " & lblVisitDate.Tag & " AND nPatientID = " & lblPatientCode.Tag & " "
                    oDB.Connect(GetConnectionString)
                    oDB.ExecuteQueryNonQuery(strdeleteHistory)
                    lblReviewed.Visible = False
                    oDB.Disconnect()

                    oDB.Dispose() : oDB = Nothing

                    objclsPatientHistory.DeleteNarration(DateNode.Key, lblPatientCode.Tag)
                    gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Delete, DateNode.Text & " Hisitory Deleted", gloAuditTrail.ActivityOutCome.Success)

                    If lblVisitDate.Tag <= 0 Then
                        lblVisitDate.Tag = DateNode.Key
                    End If

                    If DateNode.Key = lblVisitDate.Tag Then
                        '' If Currently Open History is Deleted
                        NewHistory()
                    End If

                    trvPrevHistory.Nodes.Clear()

                    trvPrevHistory.Select()
                    pnlPrevHistory.Visible = False
                    If pnlPrevHistory.Visible = True Then
                        ShowHidePreviousHistory("Hide")
                    Else
                        ShowHidePreviousHistory("Show")
                    End If

                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Remove, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub RefreshPrevHistory(ByVal objSender As Object)
        Dim obje As EventArgs = Nothing
        trvPrevHistory_AfterSelect(objSender, obje)
    End Sub

    '19-Apr-13 Aniket: Unused code. To be deleted
    'Public Sub AddSrSource_CodedHistory(ByVal strsearch As String, ByVal dt As DataTable)
    '    Try
    '        Dim tdt As DataTable
    '        Dim dv As New DataView(dt)

    '        Dim strsearchCategoryItem As String
    '        If Trim(strsearch) <> "" Then
    '            strsearchCategoryItem = Replace(strsearch, "'", "''")
    '            ''''Code line below is added on 29/10/2007 by Anil, to resolve the bug, which was giving error for special characters in search.
    '            strsearchCategoryItem = Replace(strsearchCategoryItem, "[", "") & ""
    '            strsearchCategoryItem = mdlGeneral.ReplaceSpecialCharacters(strsearchCategoryItem)
    '        Else
    '            strsearchCategoryItem = ""
    '        End If


    '        dv.RowFilter = dt.Columns(1).ColumnName & " Like '%" & strsearchCategoryItem & "%'"

    '        tdt = New DataTable
    '        tdt = dv.ToTable

    '        For Each historyRow As DataRow In tdt.Rows
    '            Dim objTreeNode As New myTreeNode
    '            objTreeNode.Text = historyRow.Item("Description") '' History Name/ Drug Name
    '            objTreeNode.Key = historyRow.Item("ICD9ID") '' HistoryID / Drug ID
    '            objTreeNode.Tag = CType(historyRow.Item("ICD9CODE"), String) '' 0/1

    '            objTreeNode.ImageIndex = 4
    '            objTreeNode.SelectedImageIndex = 4
    '            objTreeNode = Nothing 'Change made to solve memory Leak and word crash issue
    '        Next historyRow

    '    Catch ex As Exception
    '        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
    '        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub

    Private Sub pnlWordComp_VisibleChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles pnlWordComp.VisibleChanged
        Try
            If pnlWordComp.Visible = True Then
                tblShowNarrative.Checked = True
            Else
                tblShowNarrative.Checked = False
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, "Patient History", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub trvPrevHistory_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles trvPrevHistory.MouseDown
        Try

            If CType(sender, TreeView) Is trvPrevHistory Then
                If e.Button = MouseButtons.Right Then
                    If _blnRecordLock = True Then
                        'Try
                        '    If (IsNothing(trvPrevHistory.ContextMenu) = False) Then
                        '        trvPrevHistory.ContextMenu.Dispose()
                        '        trvPrevHistory.ContextMenu = Nothing
                        '    End If
                        'Catch ex As Exception

                        'End Try
                        trvPrevHistory.ContextMenu = Nothing
                        Exit Sub
                    End If
                    Dim trvNode As TreeNode
                    trvNode = trvPrevHistory.GetNodeAt(e.X, e.Y)
                    If IsNothing(trvNode) = False Then
                        trvPrevHistory.SelectedNode = trvNode
                    End If

                    If Not trvPrevHistory.SelectedNode Is trvPrevHistory.Nodes.Item(0) Then
                        If Not trvPrevHistory.SelectedNode.Parent Is trvPrevHistory.Nodes.Item(0) Then
                            If trvPrevHistory.SelectedNode.GetNodeCount(False) > 0 Then
                                If trvPrevHistory.SelectedNode.Parent.Text = "Current" Then
                                    mnuMakeCurrent.Visible = False
                                Else
                                    mnuMakeCurrent.Visible = True
                                End If
                                ''
                                'Try
                                '    If (IsNothing(trvPrevHistory.ContextMenu) = False) Then
                                '        trvPrevHistory.ContextMenu.Dispose()
                                '        trvPrevHistory.ContextMenu = Nothing
                                '    End If
                                'Catch ex As Exception

                                'End Try
                                trvPrevHistory.ContextMenu = ContextMenutrvPrevHistory
                                ContextMenutrvPrevHistory.GetContextMenu()
                            Else
                                'Try
                                '    If (IsNothing(trvPrevHistory.ContextMenu) = False) Then
                                '        trvPrevHistory.ContextMenu.Dispose()
                                '        trvPrevHistory.ContextMenu = Nothing
                                '    End If
                                'Catch ex As Exception

                                'End Try
                                trvPrevHistory.ContextMenu = Nothing
                            End If
                        Else
                            'Try
                            '    If (IsNothing(trvPrevHistory.ContextMenu) = False) Then
                            '        trvPrevHistory.ContextMenu.Dispose()
                            '        trvPrevHistory.ContextMenu = Nothing
                            '    End If
                            'Catch ex As Exception

                            'End Try
                            trvPrevHistory.ContextMenu = Nothing
                        End If
                    Else
                        'Try
                        '    If (IsNothing(trvPrevHistory.ContextMenu) = False) Then
                        '        trvPrevHistory.ContextMenu.Dispose()
                        '        trvPrevHistory.ContextMenu = Nothing
                        '    End If
                        'Catch ex As Exception

                        'End Try
                        trvPrevHistory.ContextMenu = Nothing
                    End If
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub


    Private Sub txtsearchhistory_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtsearchhistory.TextChanged
        Try
            If cmbsearchHistory.Text = "History Date" Then
                If IsDate(txtsearchhistory.Text) Then
                    Dim mynode As myTreeNode
                    'Root node collection
                    For Each mynode In trvPrevHistory.Nodes.Item(0).Nodes
                        Dim mychildnode As myTreeNode
                        'child node collection
                        For Each mychildnode In mynode.Nodes
                            If CType(mychildnode.Text, DateTime).Date = Trim(txtsearchhistory.Text) Then
                                mynode.Parent.ExpandAll()
                                '*************code added by sagar for showing the selected drug at the top on 4 july 2007
                                trvPrevHistory.SelectedNode = trvPrevHistory.SelectedNode.LastNode
                                '*************
                                trvPrevHistory.SelectedNode = mychildnode
                                txtsearchhistory.Focus()
                                Exit Sub
                            End If
                        Next
                    Next
                End If
            Else
                If Trim(txtsearchhistory.Text) <> "" Then
                    Dim mynode As myTreeNode
                    'Root node collection
                    'mynode is current/yesterday/last week/last month
                    For Each mynode In trvPrevHistory.Nodes.Item(0).Nodes

                        Dim mychildnode As myTreeNode
                        'mychildnode is prescriptiondate annd prescription
                        For Each mychildnode In mynode.Nodes
                            Dim myHistoryNode As TreeNode
                            For Each myHistoryNode In mychildnode.Nodes
                                If Trim(myHistoryNode.Text) <> "" Then
                                    Dim arrstring() As String
                                    arrstring = Split(myHistoryNode.Text, "~")
                                    'If Len(arrstring) > 0 Then
                                    Dim str As String
                                    str = UCase(CType(arrstring.GetValue(0), String))
                                    If Mid(str, 1, Len(UCase(Trim(txtsearchhistory.Text)))) = UCase(Trim(txtsearchhistory.Text)) Then
                                        mynode.Parent.ExpandAll()
                                        '*************code added by sagar for showing the selected drug at the top on 4 july 2007
                                        trvPrevHistory.SelectedNode = trvPrevHistory.SelectedNode.LastNode
                                        '*************
                                        trvPrevHistory.SelectedNode = myHistoryNode
                                        txtsearchhistory.Focus()
                                        Exit Sub
                                    End If
                                    'End If
                                End If
                            Next
                        Next
                    Next
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, "Patient History", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Public Sub trvPrevHistory_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles trvPrevHistory.KeyPress
        Try
            If e.KeyChar = ChrW(13) Then
                trvPrevHistory_DblClick()
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub frmHistory_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
       
        Try

            If blnOpenFromExam = True Then


                If Not IsNothing(myCaller) Then
                    If _isOBHistoryModified Then
                        myCaller.GetdataFromOtherForms(gloEMRWord.enumDocType.History)
                    End If
                    '07-May-15 Aniket: 8050 OB PRD Changes
                    If Not IsNothing(arrOB) Then

                        If arrOB.Contains("OBGenetic") Then
                            myCaller.GetdataFromOtherForms(gloEMRWord.enumDocType.OBGeneticHistory)
                        End If
                        If arrOB.Contains("OBInfection") Then
                            myCaller.GetdataFromOtherForms(gloEMRWord.enumDocType.OBInfectionHistory)
                        End If
                        If arrOB.Contains("OBExam") Then
                            myCaller.GetdataFromOtherForms(gloEMRWord.enumDocType.OBInitialPhysicalExamination)
                        End If
                        If arrOB.Contains("OBMedical") Then
                            myCaller.GetdataFromOtherForms(gloEMRWord.enumDocType.OBMedicalHistory)
                        End If
                    End If

                    If wdNarration.Text <> "" Then
                        myCaller.GetdataFromOtherForms(gloEMRWord.enumDocType.Narration)
                    End If
                    If Not IsNothing(arrOB) Then
                        arrOB.Clear()
                        arrOB = Nothing
                    End If

                    If _isOBHistoryModified Then
                        myCaller.FillHistoryPanel()
                    End If
                End If
                If Not IsNothing(myLetter) Then
                    myLetter.GetdataFromOtherForms(gloEMRWord.enumDocType.History)
                    If wdNarration.Text <> "" Then
                        myLetter.GetdataFromOtherForms(gloEMRWord.enumDocType.Narration)
                    End If
                End If
                If Not IsNothing(Me.MdiParent) Then
                    If Me.MdiParent.MdiChildren.Count > 0 Then
                        For Each frm As Object In Me.MdiParent.MdiChildren
                            If Not IsNothing(frm) Then
                                If frm.Name = "frmPrescription" Then
                                    frm.hidepanel()
                                    Exit For
                                End If

                            End If

                        Next

                    End If
                End If
                blnOpenFromExam = False
            End If


            If _IsFrmImm = True Then
                mycallerImm.SetReaction()
            End If

            If Not IsNothing(myCaller1) Then
                myCaller1.GetdataFromOtherForms(gloEMRWord.enumDocType.History)
                If wdNarration.Text <> "" Then
                    myCaller1.GetdataFromOtherForms(gloEMRWord.enumDocType.Narration)
                End If
            End If

            If _blnRecordLock = False Then

                UnLock_Transaction(TrnType.PatientHistory, m_PatientID, m_VisitID, m_VisitDate)

            End If
            'Change made to solve memory Leak and word crash issue
            'If Not objclsPatientHistory Is Nothing Then
            '    objclsPatientHistory = Nothing
            'End If
            If Not IsNothing(frmRxMeds) Then
                frmRxMeds.Dispose()
                frmRxMeds = Nothing
            End If
            ' Me.Close()
            If IsNothing(mydtButton) = False Then
                mydtButton.Dispose()
                mydtButton = Nothing
            End If
            If IsNothing(dsHistory) = False Then
                dsHistory.Dispose()
                dsHistory = Nothing
            End If


            '13-May-13 Aniket: Do not uncomment the following lines as it causes Rx/Meds form to be minimized. Resolving Bug 50515
            '19-Apr-13 Aniket: Resolving Memory Leak Issues
            'pnlOuter.Controls.Remove(pnlMain)
            If Not IsNothing(gloUC_PatientStrip1) Then
                'If (pnlOuter.Controls.Contains(gloUC_PatientStrip1)) Then
                '    pnlOuter.Controls.Remove(gloUC_PatientStrip1)
                'End If
                gloUC_PatientStrip1.Dispose()
                gloUC_PatientStrip1 = Nothing
            End If

            If IsNothing(txtSMSearch) = False Then
                txtSMSearch.Dispose()
                txtSMSearch = Nothing
            End If

            '19-Apr-13 Aniket: Resolving Memory Leak Issues
            If IsNothing(ToolTipbtn_CPTCode) = False Then
                ToolTipbtn_CPTCode.RemoveAll()
                ToolTipbtn_CPTCode.Dispose()
            End If

            If IsNothing(ToolTipbtn_ICD9Code) = False Then
                ToolTipbtn_ICD9Code.RemoveAll()
                ToolTipbtn_ICD9Code.Dispose()
            End If


            If IsNothing(ToolTipbtnConceptID) = False Then
                ToolTipbtnConceptID.RemoveAll()
                ToolTipbtnConceptID.Dispose()
            End If


            If IsNothing(ToolTipbtnUp) = False Then
                ToolTipbtnUp.RemoveAll()
                ToolTipbtnUp.Dispose()
            End If


            If IsNothing(ToolTipbtnDown) = False Then
                ToolTipbtnDown.RemoveAll()
                ToolTipbtnDown.Dispose()
            End If


            If Not IsNothing(dtcategoryType) Then
                dtcategoryType.Dispose()
                dtcategoryType = Nothing
            End If





            ToolTipbtn_CPTCode = Nothing
            ToolTipbtn_ICD9Code = Nothing
            ToolTipbtnConceptID = Nothing
            ToolTipbtnUp = Nothing
            ToolTipbtnDown = Nothing
            Try
                'Application.DoEvents()
                Me.Dispose()
            Catch exdispose As Exception

            End Try

            ''To Delete FormLevel Locking 
            If FormLevelLockID > 0 Then
                Delete_Lock_FormLevel(FormLevelLockID, m_PatientID)
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
       
        End Try
    End Sub

    Private Sub RefreshHistory()
        Try
            Dim objHistory As New clsPatientHistory

            '19-Apr-13 Aniket: Resolving Memory Leak Issues
            Dim dtRefreshHistory As DataTable


            dtRefreshHistory = objHistory.GetPrevHistory("A", m_PatientID, Now)

            Dim oCurrentNode As myTreeNode = trvPrevHistory.Nodes(0).Nodes(0)
            Dim oYesterdayNode As myTreeNode = trvPrevHistory.Nodes(0).Nodes.Item(1)
            Dim oLastWeekNode As myTreeNode = trvPrevHistory.Nodes(0).Nodes(2)
            Dim oLastMonthNode As myTreeNode = trvPrevHistory.Nodes(0).Nodes(3)
            Dim oOlderNode As myTreeNode = trvPrevHistory.Nodes(0).Nodes(4)
            Dim IscurrentHistory As Boolean = False
            Dim IsYesterdayHistory As Boolean = False
            Dim IsLastWeekHistory As Boolean = False
            Dim IsLastMonthHistory As Boolean = False
            Dim IsOlderHistory As Boolean = False

            For i As Integer = 0 To dtRefreshHistory.Rows.Count - 1
                If GetDateCategory(Format(dtRefreshHistory.Rows(i)("dtVisitDate"), "MM/dd/yyyy")) = DateCategory.Today Then

                    Dim objTreeNode As New myTreeNode(Format(dtRefreshHistory.Rows(i)("dtVisitDate"), "MM/dd/yyyy"), dtRefreshHistory.Rows(i)("nVisitID"))
                    objTreeNode.ForeColor = Color.Blue
                    objTreeNode.ImageIndex = 15
                    objTreeNode.SelectedImageIndex = 15
                    oCurrentNode.Nodes.Add(objTreeNode)
                    oCurrentNode.Key = dtRefreshHistory.Rows(i)("nVisitID")
                    IscurrentHistory = True
                    objTreeNode = Nothing

                ElseIf GetDateCategory(Format(dtRefreshHistory.Rows(i)("dtVisitDate"), "MM/dd/yyyy")) = DateCategory.Yesterday Then
                    Dim objTreeNode As New myTreeNode(Format(dtRefreshHistory.Rows(i)("dtVisitDate"), "MM/dd/yyyy"), dtRefreshHistory.Rows(i)("nVisitID"))
                    objTreeNode.ForeColor = Color.Blue
                    objTreeNode.ImageIndex = 15
                    objTreeNode.SelectedImageIndex = 15
                    oYesterdayNode.Nodes.Add(objTreeNode)
                    oYesterdayNode.Key = dtRefreshHistory.Rows(i)("nVisitID")
                    IsYesterdayHistory = True
                    objTreeNode = Nothing

                ElseIf GetDateCategory(Format(dtRefreshHistory.Rows(i)("dtVisitDate"), "MM/dd/yyyy")) = DateCategory.LastWeek Then
                    Dim objTreeNode As New myTreeNode(Format(dtRefreshHistory.Rows(i)("dtVisitDate"), "MM/dd/yyyy"), dtRefreshHistory.Rows(i)("nVisitID"))
                    objTreeNode.ForeColor = Color.Blue
                    objTreeNode.ImageIndex = 15
                    objTreeNode.SelectedImageIndex = 15
                    oLastWeekNode.Nodes.Add(objTreeNode)
                    oLastMonthNode.Key = dtRefreshHistory.Rows(i)("nVisitID")
                    IsLastWeekHistory = True
                    objTreeNode = Nothing

                ElseIf GetDateCategory(Format(dtRefreshHistory.Rows(i)("dtVisitDate"), "MM/dd/yyyy")) = DateCategory.LastMonth Then
                    Dim objTreeNode As New myTreeNode(Format(dtRefreshHistory.Rows(i)("dtVisitDate"), "MM/dd/yyyy"), dtRefreshHistory.Rows(i)("nVisitID"))
                    objTreeNode.ForeColor = Color.Blue
                    objTreeNode.ImageIndex = 15
                    objTreeNode.SelectedImageIndex = 15
                    oLastMonthNode.Nodes.Add(objTreeNode)
                    oLastMonthNode.Key = dtRefreshHistory.Rows(i)("nVisitID")
                    IsLastMonthHistory = True
                    objTreeNode = Nothing

                ElseIf GetDateCategory(Format(dtRefreshHistory.Rows(i)("dtVisitDate"), "MM/dd/yyyy")) = DateCategory.Older Then
                    Dim objTreeNode As New myTreeNode(Format(dtRefreshHistory.Rows(i)("dtVisitDate"), "MM/dd/yyyy"), dtRefreshHistory.Rows(i)("nVisitID"))
                    objTreeNode.ForeColor = Color.Blue
                    objTreeNode.ImageIndex = 15
                    objTreeNode.SelectedImageIndex = 15
                    oOlderNode.Nodes.Add(objTreeNode)
                    oOlderNode.Key = dtRefreshHistory.Rows(i)("nVisitID")
                    IsOlderHistory = True
                    objTreeNode = Nothing

                End If

            Next


            '''' 
            If IscurrentHistory Then
                trvPrevHistory.Nodes.Item(0).Nodes.Item(0).ImageIndex = 5
                trvPrevHistory.Nodes.Item(0).Nodes.Item(0).SelectedImageIndex = 5
            Else
                trvPrevHistory.Nodes.Item(0).Nodes.Item(0).ImageIndex = 6
                trvPrevHistory.Nodes.Item(0).Nodes.Item(0).SelectedImageIndex = 6
            End If
            If IsYesterdayHistory Then
                trvPrevHistory.Nodes.Item(0).Nodes.Item(1).ImageIndex = 7
                trvPrevHistory.Nodes.Item(0).Nodes.Item(1).SelectedImageIndex = 7
            Else
                trvPrevHistory.Nodes.Item(0).Nodes.Item(1).ImageIndex = 8
                trvPrevHistory.Nodes.Item(0).Nodes.Item(1).SelectedImageIndex = 8
            End If
            If IsLastWeekHistory Then
                trvPrevHistory.Nodes.Item(0).Nodes.Item(2).ImageIndex = 9
                trvPrevHistory.Nodes.Item(0).Nodes.Item(2).SelectedImageIndex = 9
            Else
                trvPrevHistory.Nodes.Item(0).Nodes.Item(2).ImageIndex = 10
                trvPrevHistory.Nodes.Item(0).Nodes.Item(2).SelectedImageIndex = 10
            End If
            If IsLastMonthHistory Then
                trvPrevHistory.Nodes.Item(0).Nodes.Item(3).ImageIndex = 11
                trvPrevHistory.Nodes.Item(0).Nodes.Item(3).SelectedImageIndex = 11
            Else
                trvPrevHistory.Nodes.Item(0).Nodes.Item(3).ImageIndex = 12
                trvPrevHistory.Nodes.Item(0).Nodes.Item(3).SelectedImageIndex = 12
            End If
            If IsOlderHistory Then
                trvPrevHistory.Nodes.Item(0).Nodes.Item(4).ImageIndex = 13
                trvPrevHistory.Nodes.Item(0).Nodes.Item(4).SelectedImageIndex = 13
            Else
                trvPrevHistory.Nodes.Item(0).Nodes.Item(4).ImageIndex = 14
                trvPrevHistory.Nodes.Item(0).Nodes.Item(4).SelectedImageIndex = 14
            End If
            'Change made to solve memory Leak and word crash issue
            If Not objHistory Is Nothing Then
                objHistory = Nothing
            End If

            '19-Apr-13 Aniket: Resolving Memory Leak Issues
            If IsNothing(dtRefreshHistory) = False Then
                dtRefreshHistory.Dispose()
                dtRefreshHistory = Nothing
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Refresh, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub mnuHistoryItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuAddHistoryItem.Click

        'btntag contains categoryid for which we are adding History Item
        Dim objfrmMSTHistory As New HistoryMaster(BtnTag)


        Try
            objfrmMSTHistory.Text = "Add History"
            objfrmMSTHistory._SelectedCategoty = BtnText
            objfrmMSTHistory.ShowDialog(IIf(IsNothing(objfrmMSTHistory.Parent), Me, objfrmMSTHistory.Parent))

            'btntext contains the description of selected category
            FillHistoryCategory1(BtnText)
            UpdateHistoryAnswers(BtnText)


        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "Patient History", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            'Change made to solve memory Leak and word crash issue
            objfrmMSTHistory.Close()
            objfrmMSTHistory.Dispose()
            objfrmMSTHistory = Nothing
        End Try

    End Sub

    Private Sub UpdateHistoryAnswers(CategoryName As String)

        Dim dtHistoryAnswers As DataTable = Nothing

        Try

            If CategoryName = "OB Initial Physical Examination" Then

                If IsNothing(objclsPatientHistory) = False Then
                    dtHistoryAnswers = objclsPatientHistory.UpdateHistoryAnswers()
                End If

                If IsNothing(dtHistoryAnswers) = False Then
                    dsHistory.Tables("InitialExam").Clear()
                    dsHistory.Tables("InitialExam").Merge(dtHistoryAnswers)
                End If
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Refresh, ex, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Public Sub ActivateBasicVoiceCmds(ByVal VoiceCol As DNSTools.DgnStrings) Implements mdlgloVoice.gloVoice.ActivateBasicVoiceCmds
        If VoiceCol.Count > 0 Then
            Dim objtblbtn As New ToolBarButton
            Dim objsender As Object = Nothing
            Select Case VoiceCol.Item(1)
                Case "Save it"
                    objtblbtn.Text = "&Save"
                    Dim objtbl As New System.Windows.Forms.ToolBarButtonClickEventArgs(objtblbtn)
                    tblHistory_ButtonClick(objsender, objtbl)
                    objtbl = Nothing
                Case "Close form"
                    objtblbtn.Text = "&Close"
                    Dim objtbl As New System.Windows.Forms.ToolBarButtonClickEventArgs(objtblbtn)
                    tblHistory_ButtonClick(objsender, objtbl)
                    objtbl = Nothing
                Case "New form"
                    objtblbtn.Text = "&New"
                    Dim objtbl As New System.Windows.Forms.ToolBarButtonClickEventArgs(objtblbtn)
                    tblHistory_ButtonClick(objsender, objtbl)
                    objtbl = Nothing
            End Select
            objtblbtn.Dispose()
            objtblbtn = Nothing
        End If
    End Sub

    Public Sub ActivateVoiceCmds(ByVal VoiceCol As DNSTools.DgnStrings) Implements mdlgloVoice.gloVoice.ActivateVoiceCmds

        If VoiceCol.Count > 0 Then
            Dim objSender As Object = Nothing
            Dim obje As EventArgs = Nothing
            Dim objKeye As KeyEventArgs = Nothing
            Dim objtblbtn As New ToolBarButton

            Select Case VoiceCol.Item(1)
                Case "Show Previous"
                    objtblbtn.Text = "Show"
                    Dim objtbl As New System.Windows.Forms.ToolBarButtonClickEventArgs(objtblbtn)
                    tblHistory_ButtonClick(objSender, objtbl)
                    objtbl = Nothing
                Case "Show Current"
                    PreviousHistory(0)
                Case "Show Yesterday"
                    PreviousHistory(1)
                Case "Show LastWeek"
                    PreviousHistory(2)
                Case "Show LastMonth"
                    PreviousHistory(3)
                Case "Show Older"
                    PreviousHistory(4)
                Case "Finish History"
                    objtblbtn.Text = "&Finish"
                    Dim objtbl As New System.Windows.Forms.ToolBarButtonClickEventArgs(objtblbtn)
                    tblHistory_ButtonClick(objSender, objtbl)
                    objtbl = Nothing
                Case "Show Narrative"
                    objtblbtn.Text = "Narrative"
                    objtblbtn.Pushed = True
                    Dim objtbl As New System.Windows.Forms.ToolBarButtonClickEventArgs(objtblbtn)
                    tblHistory_ButtonClick(objSender, objtbl)
                    objtbl = Nothing
                Case "Hide Previous"
                    objtblbtn.Text = "Hide"
                    Dim objtbl As New System.Windows.Forms.ToolBarButtonClickEventArgs(objtblbtn)
                    tblHistory_ButtonClick(objSender, objtbl)
                    objtbl = Nothing
                Case "Previous Patient History"
                    NavigatePreviousPatientHistory(False)
                Case "Next Patient History"
                    NavigatePreviousPatientHistory(True)
                Case "Delete Patient History"
                    mnuDeleteHistory_Click(objSender, obje)
                Case "Modify Patient History"
                    Dim objkeyex As New KeyPressEventArgs(ChrW(13))
                    trvPrevHistory_KeyPress(objSender, objkeyex)
                    objkeyex = Nothing
                Case "Save and Close it"
                    objtblbtn.Text = "&Finish"
                    Dim objtbl As New System.Windows.Forms.ToolBarButtonClickEventArgs(objtblbtn)
                    tblHistory_ButtonClick(objSender, objtbl)
                    objtbl = Nothing
                Case "Hide Narrative"  'Hide Narrative
                    objtblbtn.Text = "Narrative"
                    objtblbtn.Pushed = False
                    Dim objtbl As New System.Windows.Forms.ToolBarButtonClickEventArgs(objtblbtn)
                    tblHistory_ButtonClick(objSender, objtbl)
                    objtbl = Nothing
                Case "Delete History Item"
                    mnuRemove_Click(objSender, obje)
                Case Else
                    Dim objbtnSender As New Button
                    objbtnSender.Text = Trim(VoiceCol.Item(1))
                    objBtn_Click(CType(objbtnSender, Button), obje)
                    objbtnSender.Dispose()
                    objbtnSender = Nothing
            End Select
            objtblbtn.Dispose()
            objtblbtn = Nothing
        End If
    End Sub

    Public Sub AddVoiceCommands() Implements mdlgloVoice.gloVoice.AddVoiceCommands
        vVoiceMenu.Remove(1)
        vVoiceMenu.ListSetStrings("BasicVoiceCommands", voicecol)
        vVoiceMenu.Add(1, "<BasicVoiceCommands>", "", "")

        vVoiceMenu.Remove(2)
        vVoiceMenu.ListSetStrings("MyHistory", HistoryVoiceCol)
        vVoiceMenu.Add(2, "<MyHistory>", "", "")
    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Public Sub CustomGetchanges(ByVal e As AxDNSTools._DDgnDictCustomEvents_GetChangesEvent) Implements mdlgloVoice.gloVoice.CustomGetchanges

    End Sub

    Public Sub CustomMakechanges(ByVal e As AxDNSTools._DDgnDictCustomEvents_MakeChangesEvent) Implements mdlgloVoice.gloVoice.CustomMakechanges

    End Sub

    Private Sub C1HistoryDetails_BeforeEdit(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles C1HistoryDetails.BeforeEdit
        Try

            Dim stronsetActiveStatus As String = ""
            ' Dim _arrOnsetActive() As String
            Dim IsActive As Boolean = False
            Dim IsOnsetDate As Boolean = False
            If isHistoryLoading = False Then
                Dim _categorytype As String = ""
                Dim _HistoryType As String = ""
                Dim _HeaderCategory As String = ""
                _HistoryType = Convert.ToString(C1HistoryDetails.GetData(e.Row, "sHistoryType")).Trim

                _categorytype = Convert.ToString(C1HistoryDetails.GetData(e.Row, Col_HHidden)).Trim
                _HeaderCategory = Convert.ToString(C1HistoryDetails.GetData(e.Row, Col_HCategory)).Trim
                If _HeaderCategory = "OB Medical History" Or _HeaderCategory = "OB Genetic History" Or _HeaderCategory = "OB Infection History" Or _HeaderCategory = "OB Initial Physical Examination" Then
                    IsActive = False
                    IsOnsetDate = False
                Else
                    GetCategorynStatus(_HistoryType, _categorytype, IsActive, IsOnsetDate)
                End If



                'If _categorytype = "OB Medical History" Or _categorytype = "OB Genetic History" Or _categorytype = "OB Infection History" Or _categorytype = "OB Initial Physical Examination" Then

                'Else
                '    If Convert.ToString(C1HistoryDetails.GetData(e.Row, "sHistoryType")).Trim = "" Then

                '        _categorytype = Convert.ToString(C1HistoryDetails.GetData(e.Row, Col_HHidden)).Trim
                '        _categorytype = getHistoryTypefromcategorymaster(_categorytype)


                '    Else
                '        _categorytype = Convert.ToString(C1HistoryDetails.GetData(e.Row, "sHistoryType")).Trim
                '    End If
                'End If

                'If _categorytype <> "" Then
                '    If _categorytype.Length > 2 Then
                '        If _categorytype = "OB Medical History" Or _categorytype = "OB Genetic History" Or _categorytype = "OB Infection History" Or _categorytype = "OB Initial Physical Examination" Then
                '            IsActive = True
                '            IsOnsetDate = False
                '        Else

                '            stronsetActiveStatus = CheckHistoryTypeinStandardTable(_categorytype)
                '            _arrOnsetActive = stronsetActiveStatus.Split(",")
                '            If IsNothing(_arrOnsetActive) = False Then
                '                If _arrOnsetActive.Length >= 1 Then
                '                    IsOnsetDate = _arrOnsetActive.GetValue(0)
                '                    IsActive = _arrOnsetActive.GetValue(1)
                '                End If
                '            End If
                '        End If

                '    End If
                'End If
                '  stronsetActiveStatus = CheckHistoryTypeinStandardTable(_categorytype.Substring(0, 3))
                '_arrOnsetActive = stronsetActiveStatus.Split(",")
                'If IsNothing(_arrOnsetActive) = False Then
                '    If _arrOnsetActive.Length >= 1 Then
                '        IsOnsetDate = _arrOnsetActive.GetValue(0)
                '        IsActive = _arrOnsetActive.GetValue(1)
                '    End If
                'End If
                If Convert.ToString(C1HistoryDetails.GetData(e.Row, Col_HCategory)) <> "" Then
                    e.Cancel = True
                End If
                If IsActive = True And IsOnsetDate = False And _categorytype = "All" Then
                    If e.Col = col_HOnsetDate Then
                        e.Cancel = True
                    End If
                ElseIf IsActive = True And IsOnsetDate = False Then
                    If e.Col = Col_HsReaction Or e.Col = col_HOnsetDate Then
                        e.Cancel = True
                    End If
                ElseIf IsActive = False And IsOnsetDate = False Then
                    If e.Col = Col_HsReaction Or e.Col = col_HOnsetDate Or e.Col = Col_HsActive Then
                        e.Cancel = True
                    End If
                ElseIf IsActive = False And IsOnsetDate = True Then
                    If e.Col = Col_HsReaction Or e.Col = Col_HsActive Then
                        e.Cancel = True
                    End If

                End If
                'If Convert.ToString(C1HistoryDetails.GetData(e.Row, Col_HHidden)) = Convert.ToString(C1HistoryDetails.GetData(e.Row, Col_HCategory)) Then

                '    If e.Col = Col_HsReaction Or e.Col = Col_HsActive Or e.Col = col_HOnsetDate Then
                '        e.Cancel = True
                '    End If
                'End If
                'If Convert.ToString(C1HistoryDetails.GetData(e.Row, "sHistoryType")) = "Allergies" Or Convert.ToString(C1HistoryDetails.GetData(e.Row, "sHistoryType")) = "Problem" Then
                '    If e.Col = col_HOnsetDate Then
                '        e.Cancel = True
                '    End If
                'End If
                'If Convert.ToString(C1HistoryDetails.GetData(e.Row, Col_HHidden)) = "Smoking Status" Or Convert.ToString(C1HistoryDetails.GetData(e.Row, "sHistoryType")) = "Procedures" Then
                '    If e.Col = Col_HsActive Then
                '        e.Cancel = True
                '    End If
                'End If

                'If Convert.ToString(C1HistoryDetails.GetData(e.Row, "sHistoryType")) = "Family History" Or Convert.ToString(C1HistoryDetails.GetData(e.Row, "sHistoryType")) = "Social History" Then
                '    If e.Col = Col_HsReaction Or e.Col = Col_HsActive Or e.Col = col_HOnsetDate Then
                '        e.Cancel = True
                '    End If
                'End If
                'If Convert.ToString(C1HistoryDetails.GetData(e.Row, Col_HsHistoryItem)) = "" Then
                '    If e.Col = Col_HsComments Then
                '        e.Cancel = True
                '    End If
                'End If
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub C1HistoryDetails_BeforeRowColChange(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.RangeEventArgs) Handles C1HistoryDetails.BeforeRowColChange
        If PnlCustomTask.Visible = True Then
            PnlCustomTask.Visible = False
        End If
    End Sub

    Private Sub C1HistoryDetails_BeforeSort(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.SortColEventArgs) Handles C1HistoryDetails.BeforeSort

    End Sub

    Private Sub C1HistoryDetails_CellChanged(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles C1HistoryDetails.CellChanged
        If isHistoryLoading = False Then
            isHistoryModified = True
        End If
    End Sub
    Dim Defination As String = ""
    Dim ConceptDesc As String = ""
    Dim ICD9 As String = ""
    Dim HistoryType As String = ""
    Dim strHeader() As String
    Dim strDefination() As String
    Dim strHead As String

    Dim oIsNode As myTreeNode
    Dim oDescr As myTreeNode
    Private Sub C1HistoryDetails_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles C1HistoryDetails.Click
        'If Not IsNothing(C1HistoryDetails) Then
        '    Dim r As Integer = C1HistoryDetails.RowSel
        '    Dim _categorytype As String = ""
        '    Dim stronsetActiveStatus As String = ""
        '    '  Dim _arrOnsetActive() As String
        '    Dim IsActive As Boolean = False


        '    Try
        '        If r >= 0 Then
        '            Dim _HistoryType As String = ""
        '            _HistoryType = Convert.ToString(C1HistoryDetails.GetData(r, "sHistoryType")).Trim

        '            _categorytype = Convert.ToString(C1HistoryDetails.GetData(r, Col_HHidden)).Trim
        '            GetCategorynStatus(_HistoryType, _categorytype, IsActive, False)


        '            If IsActive = True And _categorytype = "All" And Convert.ToString(C1HistoryDetails.GetData(r, Col_HsHistoryItem)) <> "" Then
        '                If C1HistoryDetails.ColSel = Col_HButton Then
        '                    strbuttonStatus = "Reaction"
        '                    LoadUserGrid()
        '                End If
        '            ElseIf _categorytype = "OB Initial Physical Examination" Then
        '                If C1HistoryDetails.ColSel = Col_HButton Then
        '                    strbuttonStatus = "Initial Exam"
        '                    LoadUserGridIntialExam()
        '                End If
        '            ElseIf _categorytype = "Fam" And Convert.ToString(C1HistoryDetails.GetData(r, Col_HsHistoryItem)) <> "" Then
        '                If C1HistoryDetails.ColSel = Col_HButton Then
        '                    strbuttonStatus = "Family Member"
        '                    LoadUserGridFamily()
        '                End If
        '                'Bug #61329: 00000599 : patient has multiple history items in different casing the system will throw an error.
        '            ElseIf Convert.ToString(C1HistoryDetails.GetData(r, Col_HHidden)).ToUpper = "SMOKING STATUS" And Convert.ToString(C1HistoryDetails.GetData(r, Col_HsHistoryItem)) <> "" Then
        '                If C1HistoryDetails.ColSel = Col_HSmokeButton Then
        '                    strbuttonStatus = "Smoking"
        '                    LoadUserGridSmoking()
        '                End If
        '            End If

        '        End If

        '    Catch ex As Exception
        '        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        '    End Try
        'End If

    End Sub

    Private Sub C1HistoryDetails_DockChanged(sender As Object, e As System.EventArgs) Handles C1HistoryDetails.DockChanged

    End Sub

    Private Sub C1HistoryDetails_EnterCell(ByVal sender As Object, ByVal e As System.EventArgs) Handles C1HistoryDetails.EnterCell

        '19-Apr-13 Aniket: Resolving Memory Leak Issues
        Dim ICD9Codetbl As DataSet = Nothing
        Dim objPatientHistory As New clsPatientHistory()
        RemoveHandler ChkResolvedEnddate.CheckedChanged, AddressOf ChkResolvedEnddate_CheckedChanged
        ChkResolvedEnddate.Checked = False
        ResolvedEndDate.Enabled = False
        AddHandler ChkResolvedEnddate.CheckedChanged, AddressOf ChkResolvedEnddate_CheckedChanged
        Try
            Dim _selrow1 As Integer = 0
            Dim _NewRow As Integer = 0
            If C1HistoryDetails.Rows.Count > 0 Then
                Dim _rowsel As Integer = C1HistoryDetails.RowSel
                If _rowsel > 0 Then
                    ''Added by Mayuri:20120903
                    'Bug #61329: 00000599 : patient has multiple history items in different casing the system will throw an error.
                    If Convert.ToString(C1HistoryDetails.GetData(_rowsel, Col_HCategory)).ToUpper = Convert.ToString(C1HistoryDetails.GetData(_rowsel, Col_HHidden)).ToUpper Then
                        btnConceptID.Enabled = False
                        btn_CPTCode.Enabled = False
                        btn_ICD9Code.Enabled = False
                        Label59SNOMEDRefusedCode.Enabled = False
                        btnClearSNOMEDRefusedCode.Enabled = False
                        btnbrloinc.Enabled = False
                        btnclloinc.Enabled = False
                        btnBrowseCqm.Enabled = False
                        btnClearCqm.Enabled = False
                        btnCVXCode.Enabled = False
                        btnClsCVXCode.Enabled = False
                    Else
                        btnConceptID.Enabled = True
                        btn_CPTCode.Enabled = True
                        btn_ICD9Code.Enabled = True
                        Label59SNOMEDRefusedCode.Enabled = True
                        btnClearSNOMEDRefusedCode.Enabled = True
                        btnbrloinc.Enabled = True
                        btnclloinc.Enabled = True
                        btnBrowseCqm.Enabled = True
                        btnClearCqm.Enabled = True
                        btnCVXCode.Enabled = True
                        btnClsCVXCode.Enabled = True

                    End If
                    btnUp.Enabled = False
                    btnDown.Enabled = False

                    If Convert.ToString(C1HistoryDetails.GetData(_rowsel, "sHistoryType")).Trim = "Sur" Or Convert.ToString(C1HistoryDetails.GetData(_rowsel, "sHistoryType")).Trim = "Pro" Then
                        btnBrowseUDI.Enabled = True
                        cmbStatus.Enabled = True
                    ElseIf Convert.ToString(C1HistoryDetails.GetData(_rowsel, Col_HHidden)).ToUpper = "ALLERGIES" AndAlso Convert.ToString(C1HistoryDetails.GetData(_rowsel, Col_HCategory)).ToUpper <> "ALLERGIES" Then ''Status Is Enabled When History/Category Type Is Allergies 
                        cmbStatus.Enabled = True
                    Else

                        btnBrowseUDI.Enabled = False
                        cmbStatus.Enabled = False

                    End If

                    If Convert.ToString(C1HistoryDetails.GetData(_rowsel, Col_HHidden)).ToUpper = "ALLERGIES" Then
                        If Convert.ToString(C1HistoryDetails.GetData(_rowsel, Col_HCategory)).ToUpper = Convert.ToString(C1HistoryDetails.GetData(_rowsel, Col_HHidden)).ToUpper Then
                            cmbAllergySeverity.Enabled = False
                            cmbAllergySeverity.Text = ""
                        Else
                            cmbAllergySeverity.Enabled = True
                            cmbAllergySeverity.Text = C1HistoryDetails.GetData(C1HistoryDetails.Row, "AllergySeverity").ToString()
                        End If
                    Else
                        cmbAllergySeverity.Enabled = False
                        cmbAllergySeverity.Text = ""
                    End If

                    Try


                        If Convert.ToString(C1HistoryDetails.GetData(_rowsel, Col_HCategory)) = "" Then
                            Dim _selrow As Integer = C1HistoryDetails.Row + 1
                            Dim _strcategory As String = ""
                            _strcategory = Convert.ToString(C1HistoryDetails.Rows(_rowsel)(Col_HHidden))
                            If _selrow <> C1HistoryDetails.Rows.Count Then
                                If Convert.ToString(C1HistoryDetails.GetData(_rowsel + 1, Col_HCategory)) = "" Then
                                    _selrow1 = C1HistoryDetails.Row
                                    _NewRow = C1HistoryDetails.Row + 1

                                    For j As Integer = C1HistoryDetails.Row To C1HistoryDetails.Rows.Count - 1
                                        If _NewRow <> C1HistoryDetails.Rows.Count Then

                                            'Bug #61329: 00000599 : patient has multiple history items in different casing the system will throw an error.
                                            If _strcategory.ToUpper = Convert.ToString(C1HistoryDetails.Rows(_NewRow)(Col_HHidden)).ToUpper Then
                                                If _NewRow > 1 Then
                                                    If C1HistoryDetails.Rows(_NewRow).IsVisible Then
                                                        If Convert.ToString(C1HistoryDetails.GetData(_NewRow, Col_HCategory)) = "" Then
                                                            btnDown.Enabled = True
                                                            Exit For
                                                        End If
                                                    Else
                                                        _NewRow = _NewRow + 1
                                                    End If
                                                End If
                                            Else
                                                btnDown.Enabled = False
                                                Exit For
                                            End If
                                        End If
                                    Next
                                Else
                                    btnDown.Enabled = False

                                End If
                            End If
                            If Convert.ToString(C1HistoryDetails.GetData(_rowsel - 1, Col_HCategory)) = "" Then
                                _selrow1 = C1HistoryDetails.Row
                                '_NewRow = C1HistoryDetails.Row - 2
                                _NewRow = _selrow1 - 1
                                For j As Integer = C1HistoryDetails.Row To 1 Step -1
                                    If _NewRow > 1 Then
                                        If C1HistoryDetails.Rows(_NewRow).IsVisible Then
                                            If Convert.ToString(C1HistoryDetails.GetData(_NewRow, Col_HCategory)) = "" Then
                                                btnUp.Enabled = True
                                                Exit For
                                            End If

                                        Else
                                            _NewRow = _NewRow - 1
                                        End If
                                    End If
                                Next

                            Else

                                btnUp.Enabled = False
                            End If
                            '  mnuMoveUp.Visible = True

                        Else
                            btnUp.Enabled = False
                            btnDown.Enabled = False
                        End If
                    Catch ex As Exception
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.History, "C1HistoryDetails_EnterCell : " & gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    End Try
                    ''End code Added by Mayuri:20120903
                    Defination = Convert.ToString(C1HistoryDetails.GetData(_rowsel, Col_HsDescription))
                    'lblsnodesc.Text = Defination
                    If IsNothing(C1HistoryDetails.GetData(_rowsel, Col_HsConceptID)) = False Then


                        If Convert.ToString(C1HistoryDetails.GetData(_rowsel, Col_HsConceptID)).Trim = "0" Then
                            lblconcptid.Text = ""
                        Else
                            lblconcptid.Text = Convert.ToString(C1HistoryDetails.GetData(_rowsel, Col_HsConceptID)) '.ToString()
                        End If
                    Else
                        lblconcptid.Text = C1HistoryDetails.GetData(_rowsel, Col_HsConceptID)
                    End If
                    If lblconcptid.Text.Trim() <> "" Then    ''changes done for 8020 snomed prd
                        If Defination.Trim() <> "" Then
                            lblconcptid.Text = lblconcptid.Text + "-" + Defination
                        End If
                    End If

                    lbldescid.Text = Convert.ToString(C1HistoryDetails.GetData(_rowsel, Col_HsDescriptionID)) '.ToString()
                    lblSnomedID.Text = Convert.ToString(C1HistoryDetails.GetData(_rowsel, Col_HsSnomedID)) '.ToString()
                    Dim strloinc As String = Convert.ToString(C1HistoryDetails.GetData(_rowsel, Col_LoincCode)) & " : " & Convert.ToString(C1HistoryDetails.GetData(_rowsel, Col_LoincDescr))
                    If (strloinc.Trim().Length > 3) Then
                        txtloinc.Text = strloinc
                    Else
                        txtloinc.Text = ""
                    End If
                    sDeviceID = ""
                    sBrandName = ""
                    For u As Integer = 0 To dsHistory.Tables("UDI").Rows.Count - 1
                        If Not IsDBNull(C1HistoryDetails.GetData(_rowsel, Col_DeviceList_ID)) Then
                            If dsHistory.Tables("UDI").Rows(u)("nDevicelist_Id") = C1HistoryDetails.GetData(_rowsel, Col_DeviceList_ID) Then
                                sDeviceID = dsHistory.Tables("UDI").Rows(u)("sDeviceID").ToString().Trim
                                sBrandName = dsHistory.Tables("UDI").Rows(u)("sBrandName").ToString().Trim
                                Exit For
                            End If
                        End If
                    Next
                    If sDeviceID <> "" And sBrandName <> "" Then
                        lblUDI.Text = sDeviceID + "-" + sBrandName
                    Else
                        lblUDI.Text = ""
                    End If
                    Dim strresolveddate As String = String.Empty
                    cmbStatus.Text = Convert.ToString(C1HistoryDetails.GetData(_rowsel, COl_sProcStatus))



                    If Convert.ToString(C1HistoryDetails.GetData(_rowsel, Col_HCategory)) = "" Then
                        strresolveddate = Convert.ToString(C1HistoryDetails.GetData(_rowsel, Col_ResolvedEndDate))

                        If (strresolveddate.Trim().Length = 0) Then
                            ResolvedEndDate.Value = DateTime.Now
                            ResolvedEndDate.Enabled = False
                            ChkResolvedEnddate.Checked = False
                        Else
                            If strresolveddate <> "1/1/1900 12:00:00 AM" Then
                                ResolvedEndDate.Enabled = True
                                ResolvedEndDate.Value = strresolveddate
                                ChkResolvedEnddate.Checked = True
                            Else
                                ResolvedEndDate.Value = DateTime.Now

                            End If

                        End If
                    Else
                        ResolvedEndDate.Value = DateTime.Now
                        ChkResolvedEnddate.Enabled = False
                    End If

                    AllergyIntelorenceType = Convert.ToString(C1HistoryDetails.GetData(_rowsel, Col_AllergyIntelorenceCode))

                    If Not IsNothing(cmbAllergyIntelorenceType.DataSource) Then
                        cmbAllergyIntelorenceType.SelectedValue = AllergyIntelorenceType
                    End If

                    'Code Start-Added by kanchan on 20100526 for ICD9
                    ConceptDesc = Convert.ToString(C1HistoryDetails.GetData(_rowsel, Col_HsHistoryItem))
                    Dim strCVX As String = Convert.ToString(C1HistoryDetails.GetData(_rowsel, Col_CVXCode)) & " : " & Convert.ToString(C1HistoryDetails.GetData(_rowsel, Col_CVXDesc))
                    If strCVX.Trim().Length() > 2 Then
                        txtCVXCode.Text = strCVX
                    Else
                        txtCVXCode.Text = ""
                    End If


                    'linkLblICD9code.Text = ICD9 
                    HistoryType = ""
                    For m As Integer = 0 To dsHistory.Tables("HistoryTypes").Rows.Count - 1
                        If dsHistory.Tables("HistoryTypes").Rows(m)("sShortDescription").ToString().Trim = Convert.ToString(C1HistoryDetails.GetData(_rowsel, "sHistoryType")).Trim Then
                            HistoryType = dsHistory.Tables("HistoryTypes").Rows(m)(Col_Standard_HistoryType).ToString().Trim
                            Exit For
                        End If
                    Next

                    If HistoryType <> "" Then
                        lblHistoryType.Text = HistoryType
                    Else
                        lblHistoryType.Text = ""
                    End If
                    ICD9 = Convert.ToString(C1HistoryDetails.GetData(_rowsel, Col_HsICD9))
                    If ICD9 <> "" Or Convert.ToString(C1HistoryDetails.GetData(_rowsel, col_HCPT)) <> "" Then

                        ICD9Codetbl = objPatientHistory.Fill_ICD9Code(Convert.ToString(C1HistoryDetails.GetData(_rowsel, Col_HsICD9)), Convert.ToString(C1HistoryDetails.GetData(_rowsel, col_HCPT)))
                        If IsNothing(ICD9Codetbl) = False Then
                            If ICD9Codetbl.Tables("ICD9").Rows.Count > 0 Then
                                linkLblICD9code.Text = ICD9Codetbl.Tables("ICD9").Rows(0)(0)
                            End If
                            If ICD9Codetbl.Tables("CPT").Rows.Count > 0 Then
                                lnlLbllCPTCode.Text = ICD9Codetbl.Tables("CPT").Rows(0)(0)
                            End If
                        End If


                    Else
                        If ICD9 = "" Then
                            linkLblICD9code.Text = ""
                        End If
                        If Convert.ToString(C1HistoryDetails.GetData(_rowsel, col_HCPT)) = "" Then
                            lnlLbllCPTCode.Text = ""
                        End If

                    End If
                    Dim sRefusalCode As String = Convert.ToString(C1HistoryDetails.GetData(_rowsel, "sRefusalCode"))
                    Dim sRefusalDesc As String = Convert.ToString(C1HistoryDetails.GetData(_rowsel, "sRefusalDesc"))
                    If sRefusalCode <> "" And sRefusalDesc <> "" Then
                        txtSNOMEDRefusedCode.Text = sRefusalCode + " ; " + sRefusalDesc
                    Else
                        txtSNOMEDRefusedCode.Text = ""
                    End If
                    '////-------------



                    Dim cqmcode As String = Convert.ToString(C1HistoryDetails.GetData(_rowsel, Col_CQMDesc))
                    If cqmcode <> "" Then
                        txtCqm.Text = cqmcode
                    Else
                        txtCqm.Text = ""
                    End If



                    '---------------------------------------------------
                    'If RefusalcodeDescription <> "" And RefusalCode <> "" Then
                    '    txtSNOMEDRefusedCode.Text = RefusalCode + " - " + RefusalcodeDescription
                    'Else
                    '    txtSNOMEDRefusedCode.Text = ""

                    'End If

                    lblRxNorm.Text = Convert.ToString(C1HistoryDetails.GetData(_rowsel, Col_HsRxNormID))
                    lblNDCid.Text = Convert.ToString(C1HistoryDetails.GetData(_rowsel, Col_HsNDCCode))
                    'If ICD9 <> "" Then  ''commented 8020 snomed changes
                    'FillICD9(ICD9, ConceptDesc)
                    'Else
                    ' trvICD9.Nodes.Clear()
                    'End If
                    'If trvICD9.Nodes.Count > 0 Then
                    'trvICD9.SelectedNode = trvICD9.Nodes(0)
                    'trvICD9.Focus()
                    'End If
                    'trvICD9.ExpandAll()

                End If



                ''AllergyIntelorenceType for 2015 Certification
                Dim _AllergyClsID As String = ""
                Dim _categorytype As String = ""
                Dim _HistoryType As String = ""
                If C1HistoryDetails.Row > 0 Then
                    If Convert.ToString(C1HistoryDetails.GetData(_rowsel, Col_HCategory)) = "" Then
                        If Convert.ToString(C1HistoryDetails.GetData(_rowsel, Col_AllergyIntelorenceCode)) = "" Then
                            If Not IsNothing(cmbAllergyIntelorenceType.DataSource) Then

                                _HistoryType = Convert.ToString(C1HistoryDetails.GetData(_rowsel, "sHistoryType")).Trim
                                _categorytype = Convert.ToString(C1HistoryDetails.GetData(_rowsel, Col_HHidden)).Trim
                                GetCategorynStatus(_HistoryType, _categorytype, False, False)
                                _AllergyClsID = Convert.ToString(C1HistoryDetails.GetData(_rowsel, Col_AllergyClsID)).Trim
                                If Not String.IsNullOrWhiteSpace(_AllergyClsID) And _categorytype = "All" Then

                                    cmbAllergyIntelorenceType.Enabled = True
                                    cmbAllergyIntelorenceType.Text = "Drug allergy (disorder)"
                                ElseIf String.IsNullOrWhiteSpace(_AllergyClsID) And _categorytype = "All" Then
                                    cmbAllergyIntelorenceType.Enabled = True

                                Else

                                    cmbAllergyIntelorenceType.Enabled = False
                                    cmbAllergyIntelorenceType.Text = ""
                                End If
                            Else

                                cmbAllergyIntelorenceType.Text = ""

                            End If ''
                        ElseIf Convert.ToString(C1HistoryDetails.GetData(_rowsel, Col_AllergyIntelorenceCode)) <> "" Then
                            cmbAllergyIntelorenceType.Enabled = True
                            cmbAllergyIntelorenceType.SelectedValue = AllergyIntelorenceType
                        End If
                    End If
                Else

                End If
                ''End  Of AllergyIntelorence Code

            End If



            If C1HistoryDetails.RowSel <= 1 Then

                cmbAllergySeverity.Enabled = False
                cmbAllergyIntelorenceType.Enabled = False
                ChkResolvedEnddate.Enabled = False
            End If



            '19-Apr-13 Aniket: Resolving Memory Leak Issues
            objPatientHistory = Nothing

            If IsNothing(ICD9Codetbl) = False Then
                ICD9Codetbl.Dispose()
                ICD9Codetbl = Nothing
            End If




        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.History, "C1HistoryDetails_EnterCell : " & gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    'Public Sub FillDefination(ByVal strDescription As String)  ''commented for 8020 snomed changes
    '    If strDescription <> "" Then
    '        '  trvDefination.Nodes.Clear()
    '        ' trvDefination.ImageList = Nothing
    '        ' trvDefination.ImageList = imgTreeVIew
    '        strHeader = Split(strDescription, "|")
    '        If strHeader.Length > 0 Then
    '            strHead = strHeader.GetValue(0)
    '            Dim oParenetNode As New myTreeNode
    '            oParenetNode.Text = strHead
    '            oParenetNode.ImageIndex = 7
    '            oParenetNode.SelectedImageIndex = 7
    '            trvDefination.Nodes.Add(oParenetNode)
    '            For i As Integer = 1 To strHeader.Length - 1
    '                strDefination = Split(strHeader.GetValue(i), ":")
    '                If strDefination.Length > 1 Then


    '                    oIsNode = New myTreeNode
    '                    oIsNode.Text = strDefination.GetValue(0)
    '                    oIsNode.ImageIndex = 4
    '                    oIsNode.SelectedImageIndex = 4
    '                    oParenetNode.Nodes.Add(oIsNode)
    '                    oDescr = New myTreeNode
    '                    oDescr.Text = strDefination.GetValue(1)
    '                    oDescr.ImageIndex = 3
    '                    oDescr.SelectedImageIndex = 3
    '                    oIsNode.Nodes.Add(oDescr)
    '                End If
    '            Next
    '        End If
    '    End If
    'End Sub

    'Public Sub FillICD9(ByVal sICD9 As String, ByVal ConceptDesc As String)
    '    If sICD9 <> "" Then
    '        trvICD9.Nodes.Clear()
    '        trvICD9.ImageList = Nothing
    '        trvICD9.ImageList = imgTreeVIew
    '        Dim oParenetNode As New myTreeNode
    '        oParenetNode.Text = ConceptDesc
    '        oParenetNode.ImageIndex = 6
    '        oParenetNode.SelectedImageIndex = 6
    '        oIsNode = New myTreeNode
    '        oIsNode.Text = sICD9.Replace("-", ":")
    '        oIsNode.ImageIndex = 4
    '        oIsNode.SelectedImageIndex = 4
    '        oParenetNode.Nodes.Add(oIsNode)
    '        trvICD9.Nodes.Add(oParenetNode)
    '    End If
    'End Sub

    Private Sub C1HistoryDetails_KeyPressEdit(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.KeyPressEditEventArgs) Handles C1HistoryDetails.KeyPressEdit
        If e.Col = Col_HsComments Then
            Try
                If (Char.IsControl(e.KeyChar)) Then
                    Exit Sub
                End If
                If (C1HistoryDetails.Editor.Text.Length >= MAX_COMMENT_LENGHT) Then
                    e.Handled = True
                End If
            Catch ex As Exception

            End Try
        End If
    End Sub


    Private Sub C1HistoryDetails_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles C1HistoryDetails.MouseDown
        Try
            ''#Resolved issue #82806

            If C1HistoryDetails.HitTest(e.X, e.Y).Row <= 0 Then
                If PnlCustomTask.Visible Then
                    PnlCustomTask.Visible = False
                End If
            End If
            If (pnlsnomadedetail.Top - e.Y) < 200 Then
                Dim y As Integer = pnlsnomadedetail.Top - PnlCustomTask.Height
                PnlCustomTask.Top = y
            Else
                PnlCustomTask.Top = e.Y
            End If
            PnlCustomTask.Left = e.X
            C1HistoryDetails.ContextMenu = Nothing
            If e.Button = MouseButtons.Right Then
                With C1HistoryDetails
                    Dim r As Integer = .HitTest(e.X, e.Y).Row
                    If r > 0 Then
                        .Select(r, True)
                        mnuAddReaction.Visible = False


                        C1HistoryDetails.ContextMenu = ContextMenuC1History
                        If Convert.ToString(C1HistoryDetails.GetData(r, Col_HsHistoryItem)) = "" Then
                            mnuRemove.Text = "Remove History Category"
                            mnuAddReaction.Visible = False
                            ChkResolvedEnddate.Enabled = False

                        Else
                            Dim IsOnsetDate As Boolean = False
                            Dim IsActive As Boolean = False
                            Dim stronsetActiveStatus As String = ""

                            Dim _categorytype As String = ""
                            Dim _HistoryType As String = ""
                            _HistoryType = Convert.ToString(C1HistoryDetails.GetData(r, "sHistoryType")).Trim

                            _categorytype = Convert.ToString(C1HistoryDetails.GetData(r, Col_HHidden)).Trim
                            GetCategorynStatus(_HistoryType, _categorytype, IsActive, IsOnsetDate)

                            If _categorytype = "All" Then
                                mnuRemove.Text = "Remove History Item"
                                mnuAddReaction.Text = "Add Reaction"
                                mnuAddReaction.Visible = True

                                ChkResolvedEnddate.Enabled = True
                            ElseIf _categorytype = "Fam" Then
                                mnuRemove.Text = "Remove History Item"
                                mnuAddReaction.Text = "Add Family Member"
                                mnuAddReaction.Visible = True

                                ChkResolvedEnddate.Enabled = True
                            Else
                                mnuRemove.Text = "Remove History Item"
                                mnuAddReaction.Visible = False
                                ChkResolvedEnddate.Enabled = False
                            End If

                        End If

                    Else

                        C1HistoryDetails.ContextMenu = Nothing
                    End If
                End With
            Else
                If Not IsNothing(C1HistoryDetails) Then
                    Dim r As Integer = C1HistoryDetails.HitTest(e.X, e.Y).Row
                    Dim _categorytype As String = ""
                    Dim stronsetActiveStatus As String = ""
                    '  Dim _arrOnsetActive() As String
                    Dim IsActive As Boolean = False


                    Try
                        If r >= 0 Then
                            Dim _HistoryType As String = ""
                            If Convert.ToString(C1HistoryDetails.GetData(r, Col_HCategory)).Trim <> "" Then
                                Exit Sub
                            End If
                            _HistoryType = Convert.ToString(C1HistoryDetails.GetData(r, "sHistoryType")).Trim

                            _categorytype = Convert.ToString(C1HistoryDetails.GetData(r, Col_HHidden)).Trim
                            GetCategorynStatus(_HistoryType, _categorytype, IsActive, False)

                            If _HistoryType = "Sur" Or _HistoryType = "Pro" Then
                                btnBrowseUDI.Enabled = True
                                cmbStatus.Enabled = True
                            ElseIf (_HistoryType = "All" Or _categorytype = "All") Then ''Status Is Enabled When History/Category Type Is Allergies
                                cmbStatus.Enabled = True
                            Else
                                btnBrowseUDI.Enabled = False
                                cmbStatus.Enabled = False
                            End If

                            If IsActive = True And _categorytype = "All" And Convert.ToString(C1HistoryDetails.GetData(r, Col_HsHistoryItem)) <> "" Then
                                If C1HistoryDetails.ColSel = Col_HButton Then
                                    strbuttonStatus = "Reaction"
                                    LoadUserGrid()
                                End If
                            ElseIf _categorytype = "OB Initial Physical Examination" Then
                                If C1HistoryDetails.ColSel = Col_HButton Then
                                    strbuttonStatus = "Initial Exam"
                                    LoadUserGridIntialExam()
                                End If
                            ElseIf _categorytype = "Fam" And Convert.ToString(C1HistoryDetails.GetData(r, Col_HsHistoryItem)) <> "" Then
                                If C1HistoryDetails.ColSel = Col_HButton Then
                                    strbuttonStatus = "Family Member"
                                    LoadUserGridFamily()
                                End If
                                'Bug #61329: 00000599 : patient has multiple history items in different casing the system will throw an error.
                            ElseIf Convert.ToString(C1HistoryDetails.GetData(r, Col_HHidden)).ToUpper = "SMOKING STATUS" And Convert.ToString(C1HistoryDetails.GetData(r, Col_HsHistoryItem)) <> "" Then
                                If C1HistoryDetails.ColSel = Col_HSmokeButton Then
                                    strbuttonStatus = "Smoking"
                                    LoadUserGridSmoking()
                                End If
                            End If

                        End If

                    Catch ex As Exception
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    End Try
                End If
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    'Pramod to show review of history form
    Public Sub ShowReview()

        'To check already review or not
        Dim oDB As New gloStream.gloDataBase.gloDataBase
        Dim strSelect = "Select count(nHistoryID) from History where nPatientID= " & lblPatientCode.Tag & " AND nVisitID =" & lblVisitDate.Tag & ""
        Dim strComment As String

        oDB.Connect(GetConnectionString)
        strComment = oDB.ExecuteQueryScaler(strSelect)
        strComment = strComment & ""
        oDB.Disconnect()

        If Val(strComment) = 0 Then
            'Bug #53045: 00000471 : History
            'Following code added to achieve functionality based on the request given in this bug.
            If MessageBox.Show(" History for patient must be saved before reviewing. Would you like to save patient's history now?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) = vbYes Then
                If ValidatePatientHistory() Then
                    Exit Sub
                End If
                Call SaveHistoryData()
                Call RefershHistoryAfterReview()
            Else
                Exit Sub
            End If
        End If
        ''''''''''''''''
        'create object to show Review of history and pass necessary field to new form 
        '27-May-15 Aniket: Resolving Bug #83627 ( Modified): EMR: OB history- In View Review, Usr name application does not display
        Dim objReviewHistory As New frmReviewOfHistory(lblVisitDate.Tag, lblVisitDate.Text, gstrLoginName, m_PatientID, intCheck)

        With objReviewHistory
            .ShowInTaskbar = False
            .StartPosition = FormStartPosition.CenterParent
            .ShowDialog(IIf(IsNothing(objReviewHistory.Parent), Me, objReviewHistory.Parent))


            Dim strSelectQry As String
            strSelectQry = "Select sComments,dtReviewDate from ReviewHistory where nPatientID = " & lblPatientCode.Tag & " AND nVisitID = " & lblVisitDate.Tag & " Order By ReviewHistory.dtReviewDate DESC"

            '19-Apr-13 Aniket: Resolving Memory Leak Issues
            Dim dtReviewHistory As DataTable
            Dim strdtReviewDate As String = ""
            oDB.Connect(GetConnectionString)
            dtReviewHistory = oDB.ReadQueryDataTable(strSelectQry)
            oDB.Disconnect()

            If IsNothing(dtReviewHistory) = False Then
                If dtReviewHistory.Rows.Count > 0 Then
                    strdtReviewDate = dtReviewHistory.Rows(0)("dtReviewDate")
                Else
                    strdtReviewDate = ""
                End If
            End If
            If .DialogResult = Windows.Forms.DialogResult.OK Then
                If .CommentsEntered = True Then
                    lblReviewed.Visible = True
                    'SLR:Free previous memory
                    If Not IsNothing(oclsLogin) Then
                        oclsLogin.Dispose()
                        oclsLogin = Nothing
                    End If
                    oclsLogin = New clsLogin
                    strLoginNameDetails = oclsLogin.GetLoginFullName(gstrLoginName)

                    If strLoginNameDetails.Trim = "" Then
                        lblReviewed.Text = "History Reviewed By " + gstrLoginName + " on " + strdtReviewDate
                        sLoginName = gstrLoginName
                    Else
                        lblReviewed.Text = "History Reviewed By " + strLoginNameDetails + " on " + strdtReviewDate
                        sLoginName = strLoginNameDetails
                    End If
                    oclsLogin = Nothing 'Change made to solve memory Leak and word crash issue
                End If
                CheckReview()
            End If

        End With

        If IsNothing(oDB) = False Then
            oDB.Dispose() : oDB = Nothing
        End If

        If IsNothing(objReviewHistory) = False Then
            objReviewHistory.Close() 'Change made to solve memory Leak and word crash issue
            objReviewHistory.Dispose()
            objReviewHistory = Nothing
        End If

    End Sub


    Public Sub FillGrid(ByVal strCategory As String, ByVal strHistory As String, ByVal strComment As String, ByVal strRection_Status As String, ByVal intDrugID As Integer, ByVal intMedicalConditionId As Long, ByVal DOEAllergy As String, ByVal sConceptID As String, ByVal sDescriptionID As String, ByVal sSnoMedID As String, ByVal sDescription As String, ByVal sTranID1 As String, ByVal sTranID2 As String, ByVal sTranID3 As String, ByVal sICD9 As String, ByVal sTranUser As String, ByVal sNDCCode As String, ByVal strHxDosage As String, ByVal intHxmpid As Int32, ByVal nHistoryID As Long, ByVal OnsetDate As String, ByVal CPT As String, ByVal nRowOrder As Int64, ByVal HistoryType As String, ByVal HistorySource As String, Optional ByVal nICDRevision As Int32 = 9, Optional ByVal sRefusalCode As String = "", Optional ByVal sRefusalDesc As String = "", Optional ByVal sLoincCode As String = "", Optional ByVal sLoincDescr As String = "", Optional ByVal AllergyClsId As String = "", Optional ByVal sValueSetOID As String = "", Optional ByVal sValueSetName As String = "", Optional ByVal nDeviceList_ID As Long = 0, Optional ByVal sProcStatus As String = "", Optional ByVal AllergySeverity As String = "", Optional ByVal ResolvedEndDate As String = "", Optional ByVal sAllergyIntelorenceCode As String = "", Optional ByVal CVxCode As String = "", Optional ByVal CVXDesc As String = "")
        Dim j As Integer
        Dim IsOnsetDate As Boolean = False

        Dim IsActive As Boolean = False
        Dim stronsetActiveStatus As String = ""

        ' Dim _arrOnsetActive() As String
        Dim _categorytype As String = ""
        With dsHistory.Tables("History")
            C1HistoryDetails.SetDataBinding(dsHistory, "History")
            Dim _Row As Integer = 0
            For j = 0 To .Rows.Count - 1
                'Bug #61329: 00000599 : patient has multiple history items in different casing the system will throw an error.
                If .Rows(j)(Col_HHidden).ToString().ToUpper() = strCategory.ToUpper() Then
                    '' TO Insert the New Item At the END of the CAtegory
                    Try
                        'Bug #61329: 00000599 : patient has multiple history items in different casing the system will throw an error.
                        If .Rows(j)(Col_HHidden).ToString().ToUpper() <> .Rows(j + 1)(Col_HHidden).ToString().ToUpper() Then
                            .Rows.Add(j + 1)

                            _Row = j + 1
                            Exit For
                        End If
                    Catch ex As Exception
                        .Rows.Add(j + 1)

                        _Row = j + 1
                        Exit For
                    End Try
                End If
            Next
            If _Row = 0 Then ''  Category Is Not exists
                .Rows.Add()
                _Row = .Rows.Count - 1
                .Rows(_Row)(Col_HCategory) = ""
                .Rows(_Row)(Col_HHidden) = ""
                .Rows(_Row)(Col_HCategory) = strCategory
                .Rows(_Row)(Col_HHidden) = strCategory



                If strCategory = "Family History" Then
                    .Rows(_Row)(Col_HsReaction) = "Family Member"
                ElseIf strCategory = "Allergies" Then
                    .Rows(_Row)(Col_HsReaction) = "Reaction"
                ElseIf strCategory = "OB Initial Physical Examination" Then
                    .Rows(_Row)(Col_HsReaction) = "Initial Physical Exam"
                Else
                    .Rows(_Row)(Col_HsReaction) = ""
                End If


                .Rows(_Row)("nRowOrder") = 0
                C1HistoryDetails.Rows(_Row + 1).AllowEditing = False
                .Rows.Add(_Row + 1)
                _Row = _Row + 1
            End If

            .Rows(_Row)(Col_HCategory) = ""
            .Rows(_Row)(Col_HHidden) = ""
            .Rows(_Row)(Col_HsHistoryItem) = ""
            .Rows(_Row)(Col_HsComments) = ""
            .Rows(_Row)(Col_HsReaction) = ""
            .Rows(_Row)(Col_HsConceptID) = ""
            .Rows(_Row)(Col_HsDescriptionID) = ""
            .Rows(_Row)(Col_HsNDCCode) = ""
            .Rows(_Row)(Col_HsSnomedID) = ""
            .Rows(_Row)(Col_HsDescription) = ""
            .Rows(_Row)(Col_HsICD9) = ""
            .Rows(_Row)(col_HCPT) = ""
            .Rows(_Row)("sRefusalCode") = ""
            .Rows(_Row)("sRefusalDesc") = ""
            .Rows(_Row)(Col_HsRxNormID) = ""
            .Rows(_Row)(Col_HsDrugName) = ""
            .Rows(_Row)(Col_AllergyClsID) = "" ''added for 2015 certification
            .Rows(_Row)("sValueSetOID") = ""
            .Rows(_Row)(Col_CQMDesc) = ""
            .Rows(_Row)(Col_DeviceList_ID) = 0
            .Rows(_Row)(COl_sProcStatus) = ""



            .Rows(_Row)(Col_HHidden) = strCategory
            .Rows(_Row)(Col_HsHistoryItem) = strHistory
            .Rows(_Row)(Col_HsDrugName) = strHistory
            .Rows(_Row)(Col_HsComments) = strComment
            .Rows(_Row)(Col_HsReaction) = ""
            .Rows(_Row)(Col_HDOE_Allergy) = DOEAllergy
            .Rows(_Row)(Col_HsConceptID) = sConceptID
            .Rows(_Row)(Col_HsDescriptionID) = sDescriptionID
            .Rows(_Row)(Col_HsNDCCode) = sNDCCode
            .Rows(_Row)(Col_Hnmpid) = intHxmpid
            .Rows(_Row)(Col_HsSnomedID) = sSnoMedID
            .Rows(_Row)(Col_HsDescription) = sDescription
            .Rows(_Row)(Col_HsICD9) = sICD9
            .Rows(_Row)(col_HCPT) = CPT
            .Rows(_Row)("sRefusalCode") = sRefusalCode
            .Rows(_Row)("sRefusalDesc") = sRefusalDesc
            .Rows(_Row)("sHistoryType") = HistoryType
            .Rows(_Row)(Col_HsRxNormID) = sTranID1
            .Rows(_Row)("nHistoryID") = nHistoryID
            .Rows(_Row)("nRowOrder") = nRowOrder
            '29-Mar-13 Aniket: Addition of source column on the History screen
            .Rows(_Row)("sHistorySource") = HistorySource
            ''Added on 20120906
            .Rows(_Row)("nICDRevision") = nICDRevision    ''added for ICD10 implementation
            .Rows(_Row)("sLoincDescr") = sLoincDescr
            .Rows(_Row)("sLoincCode") = sLoincCode
            .Rows(_Row)("AllergyClassID") = AllergyClsId ''added for 2015 certification
            .Rows(_Row)("sValueSetOID") = sValueSetOID
            .Rows(_Row)("sValueSetName") = sValueSetName
            .Rows(_Row)("nDeviceList_ID") = nDeviceList_ID
            .Rows(_Row)("sProcStatus") = sProcStatus
            .Rows(_Row)("AllergySeverity") = AllergySeverity ''added for 2015 certification
            .Rows(_Row)("dtObservationEndDate") = ResolvedEndDate ''added for 2015 certification
            .Rows(_Row)("sAllergyIntelorenceCode") = sAllergyIntelorenceCode ''added for 2015 certification
            .Rows(_Row)("CVXCode") = CVxCode
            .Rows(_Row)("CVXDesc") = CVXDesc

            dsHistory.AcceptChanges()

            C1HistoryDetails.SetDataBinding(dsHistory, "History")
            Dim _HistoryType As String = ""
            _HistoryType = .Rows(_Row)("sHistoryType")

            _categorytype = .Rows(_Row)(Col_HHidden)
            GetCategorynStatus(_HistoryType, _categorytype, IsActive, IsOnsetDate)
            '_categorytype = .Rows(_Row)(Col_HHidden)
            'If _categorytype = "OB Medical History" Or _categorytype = "OB Genetic History" Or _categorytype = "OB Infection History" Or _categorytype = "OB Initial Physical Examination" Then

            'Else

            '    If .Rows(_Row)("sHistoryType") = "" Then
            '        _categorytype = .Rows(_Row)(Col_HHidden)
            '        _categorytype = getHistoryTypefromcategorymaster(_categorytype)

            '    Else
            '        _categorytype = .Rows(_Row)("sHistoryType")

            '    End If
            'End If


            'If _categorytype <> "" Then
            '    If _categorytype = "OB Medical History" Or _categorytype = "OB Genetic History" Or _categorytype = "OB Infection History" Or _categorytype = "OB Initial Physical Examination" Then
            '        IsActive = True
            '        IsOnsetDate = False
            '    Else

            '        If _categorytype.Length > 2 Then
            '            stronsetActiveStatus = CheckHistoryTypeinStandardTable(_categorytype)
            '            _arrOnsetActive = stronsetActiveStatus.Split(",")
            '            If IsNothing(_arrOnsetActive) = False Then
            '                If _arrOnsetActive.Length >= 1 Then
            '                    IsOnsetDate = _arrOnsetActive.GetValue(0)
            '                    IsActive = _arrOnsetActive.GetValue(1)
            '                End If
            '            End If
            '        End If
            '    End If
            'End If
            If OnsetDate <> "" Then
                If _categorytype.Contains("Smoking Status") = True OrElse _categorytype = "Sur" OrElse _categorytype = "Pro" OrElse _categorytype = "All" OrElse _categorytype = "Fam" OrElse _categorytype = "Dia" OrElse _categorytype = "Soc" Then
                    .Rows(_Row)(col_HOnsetDate) = OnsetDate
                End If

            End If
            '_arrOnsetActive = stronsetActiveStatus.Split(",")
            'If IsNothing(_arrOnsetActive) = False Then
            '    If _arrOnsetActive.Length >= 1 Then
            '        IsOnsetDate = _arrOnsetActive.GetValue(0)
            '        IsActive = _arrOnsetActive.GetValue(1)
            '    End If
            'End If




            If IsOnsetDate = True Then




                Dim cs As CellStyle '= C1HistoryDetails.Styles.Add("DateTime")
                Try
                    If (C1HistoryDetails.Styles.Contains("DateTime")) Then
                        cs = C1HistoryDetails.Styles("DateTime")
                    Else
                        cs = C1HistoryDetails.Styles.Add("DateTime")

                    End If
                Catch ex As Exception
                    cs = C1HistoryDetails.Styles.Add("DateTime")

                End Try
                Dim rgDTP As C1.Win.C1FlexGrid.CellRange = C1HistoryDetails.GetCellRange(_Row + 1, col_HOnsetDate, _Row + 1, col_HOnsetDate)
                rgDTP.Style = cs
                rgDTP.StyleNew.DataType = System.Type.GetType("System.DateTime")
                '' GetType(DateTime)
                If (gblnEnableCQMCypressTesting) Then
                    rgDTP.StyleNew.Format = "MM/dd/yyyy hh:mm tt"
                Else
                    rgDTP.StyleNew.Format = "MM/dd/yyyy"
                End If
                rgDTP.StyleNew.TextAlign = TextAlignEnum.CenterCenter
                rgDTP.StyleNew.ImageAlign = ImageAlignEnum.CenterCenter

            End If


            Dim cStyle As C1.Win.C1FlexGrid.CellStyle = Nothing
            Dim rgReaction As C1.Win.C1FlexGrid.CellRange = C1HistoryDetails.GetCellRange(_Row + 1, Col_HButton, _Row + 1, Col_HButton)
            Dim rgActive As C1.Win.C1FlexGrid.CellRange = C1HistoryDetails.GetCellRange(_Row + 1, Col_HsActive, _Row + 1, Col_HsActive)

            Dim strReaction As String
            Dim strActive As String
            If IsActive And _categorytype = "All" Then
                Dim arr() As String 'Srting Array

                arr = Split(strRection_Status, "|")
                If arr.Length = 2 Then
                    strReaction = arr.GetValue(0)
                    strActive = arr.GetValue(1)
                Else
                    strReaction = strRection_Status
                    strActive = False
                End If

                Dim arrReaction As String()
                arrReaction = strReaction.Split(vbNewLine)
                dsHistory.Tables("History").Rows(_Row)(Col_HsReaction) = strReaction

                If strActive = "Active" Then
                    dsHistory.Tables("History").Rows(_Row)(Col_HsActive) = True
                Else
                    dsHistory.Tables("History").Rows(_Row)(Col_HsActive) = False
                End If
                rgReaction.Style = cStyle
                rgActive.StyleNew.DataType = GetType(Boolean)
                rgActive.StyleNew.TextAlign = TextAlignEnum.CenterCenter
                rgActive.StyleNew.ImageAlign = ImageAlignEnum.CenterCenter
                C1HistoryDetails.Rows(_Row + 1).Height = C1HistoryDetails.Rows.DefaultSize * arrReaction.Length - 1
                C1HistoryDetails.SetCellImage(_Row + 1, Col_HButton, imgTreeVIew.Images(5)) '' -- Setted the Alergic button
            ElseIf _categorytype = "OB Initial Physical Examination" Then
                Dim arr() As String 'Srting Array

                arr = Split(strRection_Status, "|")
                If arr.Length = 2 Then
                    strReaction = arr.GetValue(0)
                    strActive = arr.GetValue(1)
                Else
                    strReaction = strRection_Status
                    strActive = False
                End If

                Dim arrReaction As String()
                arrReaction = strReaction.Split(vbNewLine)
                dsHistory.Tables("History").Rows(_Row)(Col_HsReaction) = strReaction

                'If strActive = "Active" Then
                '    dsHistory.Tables("History").Rows(_Row)(Col_HsActive) = True
                'Else
                '    dsHistory.Tables("History").Rows(_Row)(Col_HsActive) = False
                'End If
                C1HistoryDetails.SetCellImage(_Row + 1, Col_HButton, imgTreeVIew.Images(5)) '' -- Setted the Alergic button
                'rgReaction.Style = cStyle
                'rgActive.StyleNew.DataType = GetType(Boolean)
                'rgActive.StyleNew.TextAlign = TextAlignEnum.CenterCenter
                'rgActive.StyleNew.ImageAlign = ImageAlignEnum.CenterCenter
                'C1HistoryDetails.Rows(_Row + 1).Height = C1HistoryDetails.Rows.DefaultSize * arrReaction.Length - 1
                'C1HistoryDetails.SetCellImage(_Row + 1, Col_HButton, imgTreeVIew.Images(5)) '' -- Setted the Alergic button
            ElseIf _categorytype = "Fam" Then
                Dim arr() As String 'Srting Array

                arr = Split(strRection_Status, "|")
                If arr.Length = 2 Then
                    strReaction = arr.GetValue(0)
                    strActive = arr.GetValue(1)
                Else
                    strReaction = strRection_Status
                    strActive = False
                End If

                Dim arrReaction As String()
                arrReaction = strReaction.Split(vbNewLine)
                Dim strMember As String() = strReaction.Split(":")
                If strMember.Length > 1 Then ''condition change for bugid 80991 if family member contains :
                    strMember(0) = strReaction.Substring(0, strReaction.LastIndexOf(":"))
                    strMember(1) = strReaction.Substring(strReaction.LastIndexOf(":") + 1)
                End If
                dsHistory.Tables("History").Rows(_Row)(Col_HsReaction) = strMember(0) 'strReaction
                If strMember.Length > 1 Then
                    dsHistory.Tables("History").Rows(_Row)("nMemberId") = strMember(1)
                End If



                rgReaction.Style = cStyle

                If IsActive Then
                    If strActive = "Active" Then
                        dsHistory.Tables("History").Rows(_Row)(Col_HsActive) = True
                    Else
                        dsHistory.Tables("History").Rows(_Row)(Col_HsActive) = False
                    End If
                    rgActive.StyleNew.DataType = GetType(Boolean)
                    rgActive.StyleNew.TextAlign = TextAlignEnum.CenterCenter
                    rgActive.StyleNew.ImageAlign = ImageAlignEnum.CenterCenter
                End If

                C1HistoryDetails.Rows(_Row + 1).Height = C1HistoryDetails.Rows.DefaultSize * arrReaction.Length - 1
                C1HistoryDetails.SetCellImage(_Row + 1, Col_HButton, imgTreeVIew.Images(5)) '' -- Setted the Alergic button

            ElseIf strCategory.Trim() = "Smoking Status" Then
                Dim arr() As String 'Srting Array
                arr = Split(strRection_Status, "|")
                If arr.Length = 2 Then
                    strReaction = arr.GetValue(0)
                    strActive = arr.GetValue(1)
                Else
                    strReaction = strRection_Status
                    strActive = ""
                End If


                Dim strReactions As String = " "
                Dim arrSomkingStatusReaction As String()
                If strReaction <> String.Empty Then
                    arrSomkingStatusReaction = strReaction.Split(vbNewLine)
                    C1HistoryDetails.Rows(_Row + 1).Height = C1HistoryDetails.Rows.DefaultSize * arrSomkingStatusReaction.Length - 1
                    .Rows(_Row)(Col_HSmokingStatus) = strReaction
                Else
                    .Rows(_Row)(Col_HSmokingStatus) = ""
                End If
                C1HistoryDetails.SetCellImage(_Row + 1, Col_HSmokeButton, imgTreeVIew.Images(5)) ''Added Rahul on 20100906
            ElseIf IsActive = True Then
                Dim arr() As String 'Srting Array

                arr = Split(strRection_Status, "|")
                If arr.Length = 2 Then
                    strReaction = arr.GetValue(0)
                    strActive = arr.GetValue(1)
                Else
                    strReaction = strRection_Status
                    strActive = False
                End If



                If strActive = "Active" Then
                    dsHistory.Tables("History").Rows(_Row)(Col_HsActive) = True
                Else
                    dsHistory.Tables("History").Rows(_Row)(Col_HsActive) = False
                End If

                rgActive.StyleNew.DataType = GetType(Boolean)
                rgActive.StyleNew.TextAlign = TextAlignEnum.CenterCenter
                rgActive.StyleNew.ImageAlign = ImageAlignEnum.CenterCenter

            End If
            .Rows(_Row)(Col_HnDrugID) = 0
            .Rows(_Row)(Col_HMedicalConditionID) = 0
            .Rows(_Row)(Col_HsDosage) = ""
            .Rows(_Row)(Col_HsNDCCode) = ""
            .Rows(_Row)(Col_Hnmpid) = 0

            .Rows(_Row)(Col_HnDrugID) = intDrugID
            .Rows(_Row)(Col_HMedicalConditionID) = intMedicalConditionId
            .Rows(_Row)(Col_HsDosage) = strHxDosage
            .Rows(_Row)(Col_HsNDCCode) = sNDCCode
            .Rows(_Row)(Col_Hnmpid) = intHxmpid
            ''.Rows(_Row)(Col_AllergyClsID) = AllergyClsId
        End With

        dsHistory.AcceptChanges()


        For i As Integer = 0 To C1HistoryDetails.Rows.Count - 1
            If C1HistoryDetails.Rows(i)(Col_HCategory) <> "" And C1HistoryDetails.Rows(i)(Col_HsReaction) <> "" Then
                Dim asgTask1 As C1.Win.C1FlexGrid.CellStyle '= C1HistoryDetails.Styles.Add("asgTask")
                Try
                    If (C1HistoryDetails.Styles.Contains("asgTask")) Then
                        asgTask1 = C1HistoryDetails.Styles("asgTask")
                    Else
                        asgTask1 = C1HistoryDetails.Styles.Add("asgTask")
                        asgTask1.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD ' New System.Drawing.Font(C1HistoryDetails.Font.FontFamily.Name, 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(0))

                    End If
                Catch ex As Exception
                    asgTask1 = C1HistoryDetails.Styles.Add("asgTask")
                    asgTask1.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD 'New System.Drawing.Font(C1HistoryDetails.Font.FontFamily.Name, 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(0))

                End Try
                C1HistoryDetails.SetCellStyle(i, Col_HsReaction, asgTask1)
            End If
        Next
    End Sub

    Public Sub ShowHistoryOfHistory()
        Dim objHistoryOfHistory As New frmHistoryOfHistory(m_PatientID)
        With objHistoryOfHistory
            .ShowInTaskbar = False
            .StartPosition = FormStartPosition.CenterParent
            .ShowDialog(IIf(IsNothing(objHistoryOfHistory.Parent), Me, objHistoryOfHistory.Parent))
            'Change made to solve memory Leak and word crash issue
            .Close()
            .Dispose()
        End With
        objHistoryOfHistory = Nothing
    End Sub
    Private Sub objBtn_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles objBtn.MouseHover
        CType(sender, Button).BackColor = Color.FromArgb(254, 207, 102)
        CType(sender, Button).BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongYellow
    End Sub


    Public Sub objBtn_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs)
        If Not IsNothing(CType(sender, Button).Parent) Then
            If CType(sender, Button).Parent.Name <> "" Then
                If CType(sender, Button).Parent.Name = "pnlTopButtons" Then
                    CType(sender, Button).BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongOrange
                    CType(sender, Button).BackColor = Color.FromArgb(207, 224, 248)
                Else
                    CType(sender, Button).BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
                    CType(sender, Button).BackColor = Color.FromArgb(207, 224, 248)
                End If
            End If
        End If
    End Sub

    Private Sub tblHistory_ItemClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tblHistory.ItemClicked
        Try
            If (e.ClickedItem.Tag = "Close") Then
                IsCloseClickFlagForCommentValidation = True
            End If
            tblHistory.Select()
            If (e.ClickedItem.Tag = "Close") Then
                IsCloseClickFlagForCommentValidation = False
            Else
                If IsNothing(C1HistoryDetails.Editor) = False Then
                    If (C1HistoryDetails.Col = Col_HsComments) And (C1HistoryDetails.Editor.Visible) And (C1HistoryDetails.Editor.Text.Length > MAX_COMMENT_LENGHT) Then
                        Exit Sub
                    End If
                End If
            End If
        Catch ex As Exception

        End Try

        Select Case e.ClickedItem.Tag
            Case "New"
                NewHistory()
            Case "Reconcile"
                ShowReconciliation()
            Case "Save"
                clsSplit_History.loadSplitControlData(m_PatientID, m_VisitID, uiPanSplitScreen_History.SelectedPanel.Name, objCriteria, objWord, gnClinicID)
                'Bug #53045: 00000471 : History
                'Validation code removed and validation function added.
                If ValidatePatientHistory() Then
                    Exit Sub
                End If

                If Not dtPHistoryMedRec Is Nothing Then
                    If dtPHistoryMedRec.Rows.Count > 0 Then

                        If Not getProviderTaxID(gnPatientProviderID) Then
                            Exit Sub
                        End If
                    End If
                End If
                Call SaveHistoryData()

                If Not dtPHistoryMedRec Is Nothing Then
                    If dtPHistoryMedRec.Rows.Count > 0 Then
                        Dim MedicalReconcilationId As Long = GetMedicalReconcillationID(m_VisitID)
                        If MedicalReconcilationId > 0 Then
                            Dim oclsselectProviderTaxID As New gloGlobal.TIN.clsSelectProviderTaxID(gnPatientProviderID)
                            oclsselectProviderTaxID.InsertProviderTaxID(nProviderAssociationID, MedicalReconcilationId, sProviderTaxID, gnPatientProviderID, gnLoginProviderID, gloGlobal.TIN.clsSelectProviderTaxID.TransactionType.ManualReconciliationHistory.GetHashCode())
                            oclsselectProviderTaxID = Nothing
                        End If
                    End If
                End If

                clsSplit_History.SaveControlDisplaySettings()
                If _IsOBHistory = False Then
                    Call OpenMedication(1)
                End If

                If (Me.IsDisposed = False) Then
                    '  Me.Close()
                    gloWord.WordDialogBoxBackgroundCloser.Close(Me)
                End If
            Case "Finish"
            Case "Close"
                Call CloseHistory()
            Case "Narrative"
                If pnlWordComp.Visible = False Then
                    ShowHideHistoryNarrative(True)
                Else
                    ShowHideHistoryNarrative(False)
                End If
            Case "Review"
                Call ShowReview()
            Case "ViewReview"
                Call ShowViewReview()
            Case "Hx"
                Call ShowHistoryOfHistory()
            Case "Show"
                If pnlPrevHistory.Visible = True Then
                    ShowHidePreviousHistory("Hide")
                Else
                    ShowHidePreviousHistory("Show")
                End If
            Case "Generate CCD"
                Dim objfrm As New frmCCDGenerateList(m_PatientID)
                objfrm.ChkAllergy.Checked = True
                objfrm.ChkFamilyHistory.Checked = True
                objfrm.ChkSocialHistory.Checked = True

                With objfrm
                    .WindowState = FormWindowState.Normal
                    .BringToFront()
                    .StartPosition = FormStartPosition.CenterParent
                    .ShowDialog(IIf(IsNothing(objfrm.Parent), Me, objfrm.Parent))
                    'Change made to solve memory Leak and word crash issue
                    .Close()
                    .Dispose()
                End With
                objfrm = Nothing
            Case "Past Pregnancies"
                Call ShowPastPregnancies()
            Case "RecHist"
                Dim objrechist As New FrmReconsile_History
                objrechist.PatientID = m_PatientID
                objrechist.RecType = 3 ''History
                objrechist.ShowDialog(Me)
                objrechist.Dispose()
                objrechist = Nothing
        End Select
    End Sub

    Private Sub ShowPastPregnancies()
        Try
            Using ofrmPastPregnancies As New frmPastPregnancies(m_PatientID)
                ofrmPastPregnancies.StartPosition = FormStartPosition.CenterParent
                ofrmPastPregnancies.ShowDialog(IIf(IsNothing(ofrmPastPregnancies.Parent), Me, ofrmPastPregnancies.Parent))
                ofrmPastPregnancies.Close()
                ofrmPastPregnancies.Dispose()
            End Using

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            ex = Nothing
        End Try

    End Sub
    Private Sub ShowReconciliation()
        Dim ogloCCDReconcile As New gloCCDLibrary.gloCCDReconcilation
        Dim frmReconcilation As New frmReconcileList
        Try
            If SaveReconcileHistoryData() = True Then


                frmReconcilation = New frmReconcileList(m_PatientID, "Allergy")
                frmReconcilation.LoginUser = gstrLoginName
                frmReconcilation.LoginID = gnLoginID


                frmReconcilation.ShowDialog(IIf(IsNothing(frmReconcilation.Parent), Me, frmReconcilation.Parent))

                RefershHistoryAfterReconciliation()
                cmbAllergyType_SelectedIndexChanged(Nothing, Nothing)
                If IsNothing(Me.ParentForm) = False Then
                    CType(Me.ParentForm, MainMenu).ShowReconciliationAlert()
                End If
                Dim _isReadyLists As Boolean = False

                _isReadyLists = ogloCCDReconcile.IsReadyListsPresent(m_PatientID, "Allergy")
                If _isReadyLists = True Then
                    tlbbtn_Reconcile.Enabled = True
                Else
                    tlbbtn_Reconcile.Enabled = False
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

            If IsNothing(frmReconcilation) = False Then
                frmReconcilation.Dispose()
                frmReconcilation = Nothing
            End If

            If IsNothing(ogloCCDReconcile) = False Then
                ogloCCDReconcile = Nothing
            End If
        End Try

    End Sub
    Private Function SaveReconcileHistoryData() As Boolean
        Dim dsModify As DataSet = Nothing
        Dim ds As DataSet = Nothing
        Dim Result As Integer
        Try

            If _isMakeAsCurrent = True Then
                Dim i As Integer
                _isMessage = True

                dsModify = dsHistory.Copy()
                objclsPatientHistory.DeleteHistory(m_VisitID, m_PatientID)
                For i = 0 To dsModify.Tables("History").Rows.Count - 1
                    If dsModify.Tables("History").Rows(i).RowState <> DataRowState.Deleted Then
                        dsModify.Tables("history").Rows(i)("RowState") = "Added"
                    Else
                        dsModify.Tables("History").Rows(i).RejectChanges()
                        dsModify.Tables("History").Rows(i)("RowState") = "Deleted"
                    End If
                Next


                If dsModify.Tables("History").Rows.Count > 0 Then

                    If dsModify.Tables("History").Rows.Count > 0 Then
                        Result = MessageBox.Show("Reconcile List cannot accessed without saving History. Do you want to save History?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                        If Result = MsgBoxResult.Yes Then

                        ElseIf Result = MsgBoxResult.No Then
                            Return False

                        End If

                    End If

                End If

                Call SaveDatasetHistory_new(dsModify)
                If Not IsNothing(dsHistory) Then
                    dsHistory.AcceptChanges()
                End If


            Else
                ''For normal save and close operation
                Me.BindingContext(dsHistory, "History").EndCurrentEdit()

                If IsNothing(dsHistory.GetChanges) = False OrElse _dsTemp.Tables("History").Rows.Count > 0 Then
                    ds = dsHistory.Copy()
                    If _isMedicationHistoryModify = True Then
                        If _isDoubleclickPrevHistory = True Then
                            Dim l As Int16
                            For l = 0 To ds.Tables("History").Rows.Count - 1
                                If ds.Tables("History").Rows(l).RowState <> DataRowState.Deleted Then
                                    If Convert.ToString(ds.Tables("History").Rows(l)("nHistoryID")) = "0" Then
                                        ds.Tables("History").Rows(l)("RowState") = "Added"
                                    End If
                                Else
                                    ds.Tables("History").Rows(l).RejectChanges()
                                    ds.Tables("History").Rows(l)("RowState") = "Deleted"
                                End If
                            Next
                        Else
                            Dim l As Int16
                            For l = 0 To ds.Tables("History").Rows.Count - 1
                                If ds.Tables("History").Rows(l).RowState <> DataRowState.Deleted Then
                                    If Convert.ToString(ds.Tables("History").Rows(l)("nHistoryID")) = "0" Then
                                        ds.Tables("History").Rows(l)("RowState") = "Added"
                                    End If
                                Else
                                    ds.Tables("History").Rows(l).RejectChanges()
                                End If
                            Next
                        End If




                        If ds.Tables("History").Rows.Count > 0 Then
                            Result = MessageBox.Show("Reconcile List cannot accessed without saving the History. Do you want to save the History?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                            If Result = MsgBoxResult.Yes Then

                            ElseIf Result = MsgBoxResult.No Then
                                Return False

                            End If

                        End If


                        Call SaveDatasetHistory_new(ds)
                        If Not IsNothing(dsHistory) Then
                            dsHistory.AcceptChanges()
                        End If


                    Else




                        If dsHistory.Tables("History").Rows.Count > 0 Then
                            Result = MessageBox.Show("Reconcile List cannot accessed without saving the History. Do you want to save the History?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                            If Result = MsgBoxResult.Yes Then

                            ElseIf Result = MsgBoxResult.No Then
                                Return False

                            End If

                        End If

                        dsHistory = SetRowState(dsHistory.GetChanges)
                        Call SaveDatasetHistory_new(dsHistory)
                        If Not IsNothing(dsHistory) Then
                            dsHistory.AcceptChanges()
                        End If


                    End If
                End If

            End If
            If Trim(wdNarration.Text) <> "" Then
                Dim strTempFileName1 As String = gloSettings.FolderSettings.AppTempFolderPath & getUniqueID() & "temp5.txt" 'SLR: Changed temp5 to uniqueID

                wdNarration.SaveFile(strTempFileName1)

                objclsPatientHistory.SaveNarration(lblVisitDate.Tag, lblPatientCode.Tag, strTempFileName1)
            Else

                objclsPatientHistory.DeleteNarration(lblVisitDate.Tag, lblPatientCode.Tag)
                wdNarration.Text = ""
            End If
            Return True
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Return Nothing
        Finally
            If IsNothing(ds) = False Then
                ds.Dispose()
                ds = Nothing
            End If
            If IsNothing(dsModify) = False Then
                dsModify.Dispose()
                dsModify = Nothing
            End If
        End Try
    End Function



    Public Sub ShowViewReview()
        Try
            Dim frm As New frmDMS_ViewReview(True, m_PatientID)
            With frm
                .WindowState = FormWindowState.Normal
                .ShowInTaskbar = False
                .StartPosition = FormStartPosition.CenterParent
                .ShowDialog(IIf(IsNothing(frm.Parent), Me, frm.Parent))
                'Change made to solve memory Leak and word crash issue
                .Close()
                .Dispose()
            End With
            frm = Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.Review, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub mnuAddReaction_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuAddReaction.Click
        Try


            Dim r As Integer = C1HistoryDetails.RowSel
            Dim _categorytype As String = ""

            If Convert.ToString(C1HistoryDetails.GetData(r, "sHistoryType")) = "" Then
                _categorytype = Convert.ToString(C1HistoryDetails.GetData(r, Col_HHidden))
                _categorytype = getHistoryTypefromcategorymaster(_categorytype)
            Else
                _categorytype = Convert.ToString(C1HistoryDetails.GetData(r, "sHistoryType"))
            End If
            If _categorytype = "All" Then



                Dim objfrmMstCategory As New CategoryMaster
                objfrmMstCategory.IsfromHistory = True
                objfrmMstCategory.ShowDialog(IIf(IsNothing(objfrmMstCategory.Parent), Me, objfrmMstCategory.Parent))
                'Change made to solve memory Leak and word crash issue
                objfrmMstCategory.Close()
                objfrmMstCategory.Dispose()
                objfrmMstCategory = Nothing
                Dim rgReaction As C1.Win.C1FlexGrid.CellRange = C1HistoryDetails.GetCellRange(C1HistoryDetails.Row, Col_Reaction, C1HistoryDetails.Row, Col_Reaction)
                Dim strReactions As String = " "

                Dim AnotherobjclsPatientHistory As New clsPatientHistory

                Dim dtReaction As DataTable
                dtReaction = AnotherobjclsPatientHistory.GetAllCategory("Reaction")

                If IsNothing(dtReaction) = False Then
                    For k As Integer = 0 To dtReaction.Rows.Count - 1
                        strReactions = strReactions & "|" & dtReaction.Rows(k)(1)
                    Next
                End If
                ''slr free dtreaction
                If Not IsNothing(dtReaction) Then
                    dtReaction.Dispose()
                    dtReaction = Nothing
                End If
                AnotherobjclsPatientHistory.Dispose()
                AnotherobjclsPatientHistory = Nothing

                Dim cstyle As C1.Win.C1FlexGrid.CellStyle
                ' cstyle = C1HistoryDetails.Styles.Add("Reaction")
                Try
                    If (C1HistoryDetails.Styles.Contains("Reaction")) Then
                        cstyle = C1HistoryDetails.Styles("Reaction")
                    Else
                        cstyle = C1HistoryDetails.Styles.Add("Reaction")
                    End If
                Catch ex As Exception
                    cstyle = C1HistoryDetails.Styles.Add("Reaction")
                End Try
                cstyle.ComboList = strReactions
                rgReaction.Style = cstyle
            ElseIf _categorytype = "Fam" Then
                Dim ofamily As New frmFamilyMemberSettings()
                ofamily.ShowDialog(IIf(IsNothing(ofamily.Parent), Me, ofamily.Parent))
                ofamily.Close()
                ofamily.Dispose()
                ofamily = Nothing
                Dim rgReaction As C1.Win.C1FlexGrid.CellRange = C1HistoryDetails.GetCellRange(C1HistoryDetails.Row, Col_Reaction, C1HistoryDetails.Row, Col_Reaction)
                Dim strReactions As String = " "
                Dim AnotherobjclsPatientHistory As New clsPatientHistory

                Dim dtReaction As DataTable
                dtReaction = AnotherobjclsPatientHistory.getFamilyMember()

                If IsNothing(dtReaction) = False Then
                    For k As Integer = 0 To dtReaction.Rows.Count - 1
                        strReactions = strReactions & "|" & dtReaction.Rows(k)(1)
                    Next
                End If

                ''slr free dtreaction
                If Not IsNothing(dtReaction) Then
                    dtReaction.Dispose()
                    dtReaction = Nothing
                End If
                AnotherobjclsPatientHistory.Dispose()
                AnotherobjclsPatientHistory = Nothing

                Dim cstyle As C1.Win.C1FlexGrid.CellStyle
                '  cstyle = C1HistoryDetails.Styles.Add("Reaction")
                Try
                    If (C1HistoryDetails.Styles.Contains("Reaction")) Then
                        cstyle = C1HistoryDetails.Styles("Reaction")
                    Else
                        cstyle = C1HistoryDetails.Styles.Add("Reaction")
                    End If
                Catch ex As Exception
                    cstyle = C1HistoryDetails.Styles.Add("Reaction")
                End Try
                cstyle.ComboList = strReactions
                rgReaction.Style = cstyle
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK)
        Finally
            C1HistoryDetails.ContextMenu = Nothing
        End Try
    End Sub

    Private Sub mnuEditHistoryItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuEditHistoryItem.Click
        ''btntag contains categoryid for which we are adding History Item
        Dim trvnode As gloUserControlLibrary.myTreeNode = CType(GloUC_trvHistory.SelectedNode, gloUserControlLibrary.myTreeNode)
        If IsNothing(trvnode) = False Then
            Dim ID As Long = trvnode.ID
            Dim objfrmMSTHistory As New HistoryMaster(BtnTag, ID) ' trvPrevHistory.SelectedNode.te
            Try
                objfrmMSTHistory.Text = "Update History"
                objfrmMSTHistory._SelectedCategoty = BtnText
                If BtnText = "OB Genetic History" Or BtnText = "OB Medical History" Or BtnText = "OB Infection History" Or BtnText = "OB Initial Physical Examination" Then
                    objfrmMSTHistory.txtDescription.Enabled = False
                    objfrmMSTHistory.cmb_HistoryCategory.Enabled = False
                Else

                End If
                objfrmMSTHistory.ShowDialog(IIf(IsNothing(objfrmMSTHistory.Parent), Me, objfrmMSTHistory.Parent))
                'Change made to solve memory Leak and word crash issue
                objfrmMSTHistory.Close()
                objfrmMSTHistory.Dispose()
                objfrmMSTHistory = Nothing
                'btntext contains the description of selected category
                FillHistoryCategory1(BtnText)
                UpdateHistoryAnswers(BtnText)

            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Modify, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                objfrmMSTHistory = Nothing
            End Try
        End If
        'Try
        '    If (IsNothing(GloUC_trvHistory.ContextMenu) = False) Then
        '        GloUC_trvHistory.ContextMenu.Dispose()
        '        GloUC_trvHistory.ContextMenu = Nothing
        '    End If
        'Catch ex As Exception

        'End Try
        GloUC_trvHistory.ContextMenu = Nothing
    End Sub

    Private Sub DeleteHistoryDataDictionary()
        Try
            If arrDataDictionary.Count > 0 Then
                Dim oDictionary As New clsDataDictionary
                For i As Integer = 0 To arrDataDictionary.Count - 1
                    '' IF CATEGORY NOT PRESSENT IN BUTTONS, THEN FIND WHETHER IT IS IN TRANSACTION OR NOT ''
                    Dim oDBLayer As New ClsDBLayer
                    If oDBLayer.IsCategoryUsedInHistory(arrDataDictionary(i)) = False Then
                        oDictionary.DeleteDataDictionary("History.sHistoryItem+History.sComments|" & arrDataDictionary(i))
                    End If

                    oDBLayer = Nothing 'Change made to solve memory Leak and word crash issue
                Next

                oDictionary = Nothing 'Change made to solve memory Leak and word crash issue
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ValidateDataDictionary(ByVal categoryName As String)
        Try
            '' IF CATEGORY PRESENT, THEN DON'T DELETE DATADICTIONARY.
            '' WE ARE SEARCHING FOR CATEGORY IN BUTTONS. IF CATEGORY BUTTON PRESENT THEN EXIT.
            For Each oButton As Control In Me.pnltrvSource.Controls
                If TypeOf oButton Is Button Then
                    If oButton.Text = categoryName Then
                        Exit Sub
                    End If
                End If
            Next

            arrDataDictionary.Add(categoryName)
        Catch ex As Exception
        End Try
    End Sub



    Private Sub C1HistoryDetails_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles C1HistoryDetails.MouseMove

        gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, sender, e.Location) ''added for bugid 92178
    End Sub

    Private Sub wdNarration_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles wdNarration.TextChanged
        If isHistoryLoading = False Then
            isHistoryModified = True
        End If
    End Sub

    Public Sub ShowMicroPhone() Implements mdlgloVoice.gloVoice.ShowMicroPhone

    End Sub

    Public Sub TurnoffMicrophone() Implements mdlgloVoice.gloVoice.TurnoffMicrophone

    End Sub

    Public ReadOnly Property MyParent() As MainMenu Implements mdlgloVoice.gloVoice.MyParent
        Get
            Return MyMDIParent
        End Get
    End Property


    ''Sandip Darade 20090622
    Private Function FillHistoryCategory1(ByVal CategoryName As String) As Boolean

        Try
            'GLO2012-0016280 : History throws an error
            'bln_Loadcategory set to True to consider process is running
            bln_Loadcategory = True

            If CategoryName = "Medical Condition" Then 'here we check whether the medical condition button is click and then call the respective function

            Else
                'if the coded History value is true then get thh data from the ICD9Gallery table, 
                'else pull it from individual category tables
                If gblnCodedHistory = True Then
                    'fill the treeview with ICD 9 Codes and Description As a History Item of given Category EXCEPT Allergy
                    If CategoryName.StartsWith("Aller") = True Then
                        Dim historyTable As DataTable  ''slr new not needed 
                        'fill the treview with the data for Allery category from History_MST
                        Dim AnotherobjclsPatientHistory As New clsPatientHistory
                        historyTable = AnotherobjclsPatientHistory.GetAllAllergies()
                        AnotherobjclsPatientHistory.Dispose()
                        AnotherobjclsPatientHistory = Nothing
                        ' dtSource = New DataTable  ''slr new not needed 
                        dtSource = historyTable
                        Dim dt As DataTable  ''slr new not needed 
                        dt = dtSource
                        GloUC_trvHistory.Clear()
                        If Not dt Is Nothing Then
                            GloUC_trvHistory.DataSource = dt
                            ''Isdrug value mapped to code member proprty of the tree view control 
                            'Line Comented and added  by dipak 20090907 for show History as code-Description format
                            GloUC_trvHistory.CodeMember = Convert.ToString(dt.Columns("Description").ColumnName)
                            'GloUC_trvHistory.CodeMember = Convert.ToString(dt.Columns("Dosage").ColumnName)
                            'end dipak
                            GloUC_trvHistory.Sort = gloUserControlLibrary.gloUC_TreeView.enumSortType.ByDescription
                            GloUC_trvHistory.ValueMember = Convert.ToString(dt.Columns("ID").ColumnName)
                            'Line Comented and added  by dipak 20090907 for show History as code-Description format
                            GloUC_trvHistory.DescriptionMember = Convert.ToString(dt.Columns("Dosage").ColumnName)
                            'GloUC_trvHistory.DescriptionMember = Convert.ToString(dt.Columns("Description").ColumnName)
                            'end dipak
                            GloUC_trvHistory.Tag = Convert.ToString(dt.Columns("IsDrug").ColumnName)
                            GloUC_trvHistory.ConceptID = Convert.ToString(dt.Columns("sConceptID").ColumnName)
                            GloUC_trvHistory.ICD9 = Convert.ToString(dt.Columns("ICD9").ColumnName)
                            GloUC_trvHistory.CPT = Convert.ToString(dt.Columns("sCPT").ColumnName)
                            GloUC_trvHistory.HistoryType = Convert.ToString(dt.Columns("sHistoryType").ColumnName)
                            GloUC_trvHistory.mpidmember = Convert.ToString(dt.Columns("mpid").ColumnName)
                            GloUC_trvHistory.NDCCodeMember = Convert.ToString(dt.Columns("NDCCode").ColumnName)
                            'Line Comented and added  by dipak 20090907 for show History as code-Description format
                            GloUC_trvHistory.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Code_Description
                            'GloUC_trvHistory.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Descripation

                            'end dipak 
                            GloUC_trvHistory.IsSystemCategory = Convert.ToString(dt.Columns("SystemCategory").ColumnName)

                            GloUC_trvHistory.AllergyClassID = Convert.ToString(dt.Columns("AllergyClassID").ColumnName)
                            GloUC_trvHistory.CQMID = Convert.ToString(dt.Columns("sValueSetOID").ColumnName)
                            GloUC_trvHistory.CQMDESC = Convert.ToString(dt.Columns("sValueSetName").ColumnName)
                            ''added for 2015 certification
                            GloUC_trvHistory.FillTreeView()
                        End If
                    Else
                        ' Dim ICD9GalleryCount As Integer
                        ' dtSource = New DataTable  ''slr new not needed 
                        Dim AnotherobjclsPatientHistory As New clsPatientHistory
                        dtSource = AnotherobjclsPatientHistory.GetAllICD9Gallery()
                        AnotherobjclsPatientHistory.Dispose()
                        AnotherobjclsPatientHistory = Nothing
                        Dim dt As DataTable ''slr new not needed 
                        dt = dtSource
                        GloUC_trvHistory.Clear()
                        If Not dt Is Nothing Then
                            GloUC_trvHistory.DataSource = dt
                            'Line Comented and added  by dipak 20090907 for show History as code-Description format
                            'GloUC_trvHistory.CodeMember = Convert.ToString(dt.Columns("Description").ColumnName)
                            GloUC_trvHistory.CodeMember = Convert.ToString(dt.Columns(2).ColumnName)
                            'end dipak
                            GloUC_trvHistory.ConceptID = ""
                            GloUC_trvHistory.ICD9 = ""
                            GloUC_trvHistory.CPT = ""
                            GloUC_trvHistory.HistoryType = ""
                            GloUC_trvHistory.ValueMember = Convert.ToString(dt.Columns("ICD9ID").ColumnName)
                            GloUC_trvHistory.Sort = gloUserControlLibrary.gloUC_TreeView.enumSortType.ByDescription
                            'Line Comented and added  by dipak 20090907 for show History as code-Description format
                            ' GloUC_trvHistory.DescriptionMember = Convert.ToString(dt.Columns(2).ColumnName)
                            GloUC_trvHistory.DescriptionMember = Convert.ToString(dt.Columns("Description").ColumnName)
                            'end dipak
                            GloUC_trvHistory.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Code_Description
                            GloUC_trvHistory.IsSystemCategory = ""
                            GloUC_trvHistory.FillTreeView()


                        End If

                    End If
                Else

                    'pull the data from the histroy MST
                    'fill the treview with the data from History_MST
                    '  Dim AllHistoryCount As Integer
                    Dim historyTable As DataTable  ''slr new not needed 

                    Dim AnotherobjclsPatientHistory As New clsPatientHistory
                    If CategoryName.StartsWith("Aller") = True Then
                        historyTable = AnotherobjclsPatientHistory.GetAllAllergies()
                    Else
                        historyTable = AnotherobjclsPatientHistory.GetAllHistory(CategoryName)
                    End If

                    AnotherobjclsPatientHistory.Dispose()
                    AnotherobjclsPatientHistory = Nothing


                    Dim dt As DataTable ''slr new not needed 
                    dt = historyTable
                    GloUC_trvHistory.Clear()
                    If Not dt Is Nothing Then
                        GloUC_trvHistory.DataSource = dt
                        'If CategoryName = "OB Medical History" Or CategoryName = "OB Infection History" Or CategoryName = "OB Genetic History" Then
                        '    GloUC_trvHistory.CodeMember = Convert.ToString(dt.Columns("RowOrder").ColumnName)
                        'Else
                        '    GloUC_trvHistory.CodeMember = Convert.ToString(dt.Columns("Description").ColumnName)
                        'End If
                        If CategoryName = "OB Medical History" Or CategoryName = "OB Genetic History" Or CategoryName = "OB Infection History" Then
                            GloUC_trvHistory.Sort = gloUserControlLibrary.gloUC_TreeView.enumSortType.RowOrder
                        End If
                        GloUC_trvHistory.RowOrderMember = Convert.ToString(dt.Columns("RowOrder").ColumnName)
                        GloUC_trvHistory.CodeMember = Convert.ToString(dt.Columns("Dosage").ColumnName)
                        GloUC_trvHistory.ValueMember = Convert.ToString(dt.Columns("ID").ColumnName)
                        GloUC_trvHistory.DescriptionMember = Convert.ToString(dt.Columns("Description").ColumnName)
                        GloUC_trvHistory.Tag = Convert.ToString(dt.Columns("IsDrug").ColumnName)
                        GloUC_trvHistory.ConceptID = Convert.ToString(dt.Columns("sConceptID").ColumnName)
                        GloUC_trvHistory.ICD9 = Convert.ToString(dt.Columns("ICD9").ColumnName)
                        GloUC_trvHistory.CPT = Convert.ToString(dt.Columns("sCPT").ColumnName)
                        GloUC_trvHistory.HistoryType = Convert.ToString(dt.Columns("sHistoryType").ColumnName)
                        GloUC_trvHistory.mpidmember = Convert.ToString(dt.Columns("mpid").ColumnName)
                        GloUC_trvHistory.NDCCodeMember = Convert.ToString(dt.Columns("NDCCode").ColumnName)
                        GloUC_trvHistory.IsSystemCategory = Convert.ToString(dt.Columns("SystemCategory").ColumnName)
                        GloUC_trvHistory.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Code_Description

                        GloUC_trvHistory.AllergyClassID = Convert.ToString(dt.Columns("AllergyClassID").ColumnName) ''added for 2015 certification
                        GloUC_trvHistory.RxNormCode = Convert.ToString(dt.Columns("RxNormCode").ColumnName) ''added for 2015 certification
                        GloUC_trvHistory.CQMID = Convert.ToString(dt.Columns("sValueSetOID").ColumnName)
                        GloUC_trvHistory.CQMDESC = Convert.ToString(dt.Columns("sValueSetName").ColumnName)
                        GloUC_trvHistory.FillTreeView()

                    End If
                End If
            End If

            Return True
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.Category, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        Finally
            'GLO2012-0016280 : History throws an error
            'bln_Loadcategory Set to False to consider process complete
            bln_Loadcategory = False
        End Try
    End Function

    Private Sub GloUC_trvHistory_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles GloUC_trvHistory.KeyPress
        Dim dv As DataView = dsHistory.Tables("History").Copy.DefaultView
        FillDataInGrid(dv)
        dv.Dispose()
        dv = Nothing
    End Sub
    Private Sub GloUC_trvHistory_NodeMouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles GloUC_trvHistory.NodeMouseDoubleClick
        If e.Button = Windows.Forms.MouseButtons.Left Then
            Dim dv As DataView = dsHistory.Tables("History").Copy.DefaultView
            FillDataInGrid(dv)
            If Not IsNothing(dv) Then
                dv.Dispose()
                dv = Nothing
            End If
        End If
    End Sub

    Private Sub GloUC_trvHistory_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles GloUC_trvHistory.MouseDown
        Try
            'Try
            '    If (IsNothing(GloUC_trvHistory.ContextMenu) = False) Then
            '        GloUC_trvHistory.ContextMenu.Dispose()
            '        GloUC_trvHistory.ContextMenu = Nothing
            '    End If
            'Catch ex As Exception

            'End Try
            GloUC_trvHistory.ContextMenu = Nothing
            If BtnText <> "Medical Condition" And BtnText <> "Coded History" Then
                If gblnCodedHistory = False Or BtnText.StartsWith("Aller") Then
                    If e.Button = MouseButtons.Right Then
                        If _RecordLock = True Then
                            'Try
                            '    If (IsNothing(GloUC_trvHistory.ContextMenu) = False) Then
                            '        GloUC_trvHistory.ContextMenu.Dispose()
                            '        GloUC_trvHistory.ContextMenu = Nothing
                            '    End If
                            'Catch ex As Exception

                            'End Try
                            GloUC_trvHistory.ContextMenu = Nothing
                            Exit Sub
                        End If
                        Dim trvnode As gloUserControlLibrary.myTreeNode = CType(GloUC_trvHistory.SelectedNode, gloUserControlLibrary.myTreeNode)
                        If IsNothing(trvnode) = False Then
                            GloUC_trvHistory.SelectedNode = trvnode
                            If trvnode.IsSystemCategory = "True" Then
                                If BtnText = "OB Genetic History" Or BtnText = "OB Medical History" Or BtnText = "OB Infection History" Then
                                    cntCategory.MenuItems.Item(0).Visible = True
                                    cntCategory.MenuItems.Item(1).Visible = True
                                Else
                                    cntCategory.MenuItems.Item(0).Visible = True
                                    cntCategory.MenuItems.Item(1).Visible = False
                                End If

                                'Try
                                '    If (IsNothing(GloUC_trvHistory.ContextMenu) = False) Then
                                '        GloUC_trvHistory.ContextMenu.Dispose()
                                '        GloUC_trvHistory.ContextMenu = Nothing
                                '    End If
                                'Catch ex As Exception

                                'End Try

                                GloUC_trvHistory.ContextMenu = cntCategory
                            ElseIf IsNothing(trvnode.Tag) = False Then
                                If trvnode.Tag = 1 Then
                                    cntCategory.MenuItems.Item(0).Visible = True
                                    cntCategory.MenuItems.Item(1).Visible = False
                                    'Try
                                    '    If (IsNothing(GloUC_trvHistory.ContextMenu) = False) Then
                                    '        GloUC_trvHistory.ContextMenu.Dispose()
                                    '        GloUC_trvHistory.ContextMenu = Nothing
                                    '    End If
                                    'Catch ex As Exception

                                    'End Try
                                    GloUC_trvHistory.ContextMenu = cntCategory
                                Else
                                    If Not IsNothing(trvnode) Then
                                        If Not String.IsNullOrWhiteSpace(trvnode.AllergyClassID) Then
                                            cntCategory.MenuItems.Item(0).Visible = True
                                            cntCategory.MenuItems.Item(1).Visible = False
                                        Else
                                            cntCategory.MenuItems.Item(0).Visible = True
                                            cntCategory.MenuItems.Item(1).Visible = True
                                        End If

                                        'Try
                                        '    If (IsNothing(GloUC_trvHistory.ContextMenu) = False) Then
                                        '        GloUC_trvHistory.ContextMenu.Dispose()
                                        '        GloUC_trvHistory.ContextMenu = Nothing
                                        '    End If
                                        'Catch ex As Exception

                                        'End Try
                                        GloUC_trvHistory.ContextMenu = cntCategory
                                    Else
                                        cntCategory.MenuItems.Item(0).Visible = True
                                        cntCategory.MenuItems.Item(1).Visible = False
                                        'Try
                                        '    If (IsNothing(GloUC_trvHistory.ContextMenu) = False) Then
                                        '        GloUC_trvHistory.ContextMenu.Dispose()
                                        '        GloUC_trvHistory.ContextMenu = Nothing
                                        '    End If
                                        'Catch ex As Exception

                                        'End Try
                                        GloUC_trvHistory.ContextMenu = cntCategory
                                    End If
                                End If
                            Else

                                If Not IsNothing(trvnode) Then
                                    cntCategory.MenuItems.Item(0).Visible = True
                                    cntCategory.MenuItems.Item(1).Visible = True
                                    'Try
                                    '    If (IsNothing(GloUC_trvHistory.ContextMenu) = False) Then
                                    '        GloUC_trvHistory.ContextMenu.Dispose()
                                    '        GloUC_trvHistory.ContextMenu = Nothing
                                    '    End If
                                    'Catch ex As Exception

                                    'End Try
                                    GloUC_trvHistory.ContextMenu = cntCategory
                                Else
                                    cntCategory.MenuItems.Item(0).Visible = True
                                    cntCategory.MenuItems.Item(1).Visible = False
                                    'Try
                                    '    If (IsNothing(GloUC_trvHistory.ContextMenu) = False) Then
                                    '        GloUC_trvHistory.ContextMenu.Dispose()
                                    '        GloUC_trvHistory.ContextMenu = Nothing
                                    '    End If
                                    'Catch ex As Exception

                                    'End Try
                                    GloUC_trvHistory.ContextMenu = cntCategory
                                End If
                            End If
                        Else
                            cntCategory.MenuItems.Item(0).Visible = True
                            cntCategory.MenuItems.Item(1).Visible = False
                            'Try
                            '    If (IsNothing(GloUC_trvHistory.ContextMenu) = False) Then
                            '        GloUC_trvHistory.ContextMenu.Dispose()
                            '        GloUC_trvHistory.ContextMenu = Nothing
                            '    End If
                            'Catch ex As Exception

                            'End Try
                            GloUC_trvHistory.ContextMenu = cntCategory
                        End If
                    Else
                        'Try
                        '    If (IsNothing(GloUC_trvHistory.ContextMenu) = False) Then
                        '        GloUC_trvHistory.ContextMenu.Dispose()
                        '        GloUC_trvHistory.ContextMenu = Nothing
                        '    End If
                        'Catch ex As Exception

                        'End Try
                        GloUC_trvHistory.ContextMenu = Nothing
                    End If
                Else
                    'Try
                    '    If (IsNothing(GloUC_trvHistory.ContextMenu) = False) Then
                    '        GloUC_trvHistory.ContextMenu.Dispose()
                    '        GloUC_trvHistory.ContextMenu = Nothing
                    '    End If
                    'Catch ex As Exception

                    'End Try
                    GloUC_trvHistory.ContextMenu = Nothing
                End If

            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, "Patient History", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub GloUC_trvHistory_NodeAdded(ByVal ChildNode As gloUserControlLibrary.myTreeNode) Handles GloUC_trvHistory.NodeAdded
        If Not String.IsNullOrWhiteSpace(ChildNode.AllergyClassID) Then  ''to identify allergies having codes                        
            ChildNode.ImageIndex = 3
            ChildNode.SelectedImageIndex = 3
        End If
    End Sub

    ''Added Rahul on 20101004
    Private Sub BindUserGridSmoking()
        Try

            'Dim objclsPatientHistory As New clsPatientHistory

            Dim dtsmoke As DataTable

            Dim dv As DataView = Nothing  ''slr new not needed 

            If IsNothing(dsHistory) = False Then
                If dsHistory.Tables("GetCategory").Rows.Count > 0 Then
                    dv = dsHistory.Tables("GetCategory").DefaultView
                    dv.RowFilter = "sCategoryType='Smoking Status Type'"
                End If
            End If
            'dt = objclsPatientHistory.GetAllCategory("Reaction")
            dtsmoke = dv.ToTable()
            CustomDrugsGridStyle()
            Dim col As New DataColumn
            col.ColumnName = "Select"
            col.DataType = System.Type.GetType("System.Boolean")

            col.DefaultValue = CBool("False")
            dtsmoke.Columns.Add(col)

            If Not IsNothing(dtsmoke) Then
                dtsmoke.Columns("sDescription").Caption = "Description"
                dgCustomGrid.datasource(dtsmoke.DefaultView)
            End If
            ''Reset the grid
            Dim _TotalWidth As Single = dgCustomGrid.C1Task.Width - 5
            dgCustomGrid.C1Task.Cols.Move(dgCustomGrid.C1Task.Cols.Count - 1, 0)
            dgCustomGrid.C1Task.AllowEditing = True
            dgCustomGrid.C1Task.Cols(0).AllowEditing = True
            dgCustomGrid.C1Task.Cols(0).Width = _TotalWidth * 0.1
            dgCustomGrid.C1Task.Cols(1).Visible = False
            dgCustomGrid.C1Task.Cols(2).AllowEditing = False
            dgCustomGrid.C1Task.Cols(2).Width = _TotalWidth * 0.85
            dgCustomGrid.Visible = True
            Dim r As Integer = C1HistoryDetails.RowSel
            If Not IsNothing(C1HistoryDetails.GetData(r, Col_HSmokingStatus)) Then
                If C1HistoryDetails.GetData(r, Col_HSmokingStatus).ToString().Trim() <> "" Then

                    CheckDGCustomGridSmoking()
                End If
            End If
            ' objclsPatientHistory = Nothing 'Change made to solve memory Leak and word crash issue
        Catch ex As SqlClient.SqlException
            MessageBox.Show(ex.Message, "Show Reports", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Show Reports", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub BindUserGridInitialExam()
        Try

            'Dim objclsPatientHistory As New clsPatientHistory
            Dim _CurrentItem As String = Convert.ToString(C1HistoryDetails.GetData(C1HistoryDetails.Row, Col_HsHistoryItem))
            Dim dtExam As DataTable

            Dim dv As DataView = Nothing  ''slr new not needed 

            If IsNothing(dsHistory) = False Then
                If dsHistory.Tables("InitialExam").Rows.Count > 0 Then
                    dv = dsHistory.Tables("InitialExam").DefaultView
                    dv.RowFilter = "Historyitem=" & "'" & _CurrentItem & "'"

                End If
            End If
            ' dtExam = objclsPatientHistory.GetInitialPhysicalExaminationAnswers("Reaction")
            dtExam = dv.ToTable()
            CustomDrugsGridStyle()
            'Dim col As New DataColumn
            'col.ColumnName = "Select"
            'col.DataType = System.Type.GetType("System.Boolean")

            'col.DefaultValue = CBool("False")
            'dtExam.Columns.Add(col)

            If Not IsNothing(dtExam) Then
                '  dtExam.Columns("sDescription").Caption = "Description"
                dgCustomGrid.datasource(dtExam.DefaultView)
            End If
            ''Reset the grid
            Dim _TotalWidth As Single = dgCustomGrid.C1Task.Width - 5
            '  dgCustomGrid.C1Task.Cols.Move(dgCustomGrid.C1Task.Cols.Count - 1, 0)
            dgCustomGrid.C1Task.AllowEditing = True
            ' dgCustomGrid.C1Task.Cols(0).AllowEditing = True
            dgCustomGrid.C1Task.Cols(1).Visible = False
            ' dgCustomGrid.C1Task.Cols(1).Visible = False
            dgCustomGrid.C1Task.Cols(0).AllowEditing = False
            dgCustomGrid.C1Task.Cols(0).Width = _TotalWidth * 0.85
            dgCustomGrid.Visible = True

            ' objclsPatientHistory = Nothing 'Change made to solve memory Leak and word crash issue
        Catch ex As SqlClient.SqlException
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Public Sub CheckDGCustomGrid()
        Dim r As Integer = C1HistoryDetails.RowSel
        Dim StrData As System.Array = C1HistoryDetails.GetData(r, Col_HsReaction).ToString().Split(vbNewLine)

        For Len As Integer = 0 To StrData.Length - 1
            StrData(Len) = StrData(Len).Trim()
        Next
        For i As Integer = 0 To dgCustomGrid.C1Task.Rows.Count - 1
            If Array.IndexOf(StrData, (dgCustomGrid.GetItem(i, 2).ToString.Trim())) >= 0 Then
                dgCustomGrid.C1Task.SetCellCheck(i, 0, C1.Win.C1FlexGrid.CheckEnum.Checked)

            End If
        Next
    End Sub
    Dim blnchksmk As Boolean = False
    Public Sub CheckDGCustomGridSmoking()
        Dim r As Integer = C1HistoryDetails.RowSel
        Dim StrData As System.Array = C1HistoryDetails.GetData(r, Col_HSmokingStatus).ToString().Split(vbNewLine)

        For Len As Integer = 0 To StrData.Length - 1
            StrData(Len) = StrData(Len).Trim()
        Next
        For i As Integer = 0 To dgCustomGrid.C1Task.Rows.Count - 1
            If Array.IndexOf(StrData, (dgCustomGrid.GetItem(i, 2).ToString.Trim())) >= 0 And (blnchksmk = False) Then
                blnchksmk = True
                dgCustomGrid.C1Task.SetCellCheck(i, 0, C1.Win.C1FlexGrid.CheckEnum.Checked)
            End If
        Next
        blnchksmk = False
    End Sub

    Public Sub CustomDrugsGridStyle_ICD9CPT(ByVal Type As String)
        If Type = "ICD9" Then
            dgCustomGrid.tsbtn_New.Visible = False

            Dim _TotalWidth As Single = dgCustomGrid.C1Task.Width - 5

            ' '' Show Drugs Info
            With dgCustomGrid.C1Task
                .Cols.Fixed = 0
                .Rows.Fixed = 1
                .Cols.Count = Col_CustCnt
                .AllowEditing = True
                .ExtendLastCol = True
                '.SetData(0, Col_Select, "Select")
                '.Cols(Col_Select).Width = _TotalWidth * 0.1
                '.Cols(Col_Select).AllowEditing = True
                '.Cols(Col_Select).DataType = System.Type.GetType("System.Boolean")

                .SetData(0, Col_ICD9CPTCode, "sICD9Code")
                .Cols(Col_ICD9CPTCode).Width = _TotalWidth * 0.85

                .SetData(0, Col_ICD9CPTDEscription, "sDescription")
                .Cols(Col_ICD9CPTDEscription).Width = _TotalWidth * 0.85
            End With
        ElseIf Type = "CPT" Then
            dgCustomGrid.tsbtn_New.Visible = False

            Dim _TotalWidth As Single = dgCustomGrid.C1Task.Width - 5

            ' '' Show Drugs Info
            With dgCustomGrid.C1Task
                .Cols.Fixed = 0
                .Rows.Fixed = 1
                .Cols.Count = Col_CustCnt
                .AllowEditing = True
                .ExtendLastCol = True
                '.SetData(0, Col_Select, "Select")
                '.Cols(Col_Select).Width = _TotalWidth * 0.1
                '.Cols(Col_Select).AllowEditing = True
                '.Cols(Col_Select).DataType = System.Type.GetType("System.Boolean")

                .SetData(0, Col_ICD9CPTCode, "sCPTCode")
                .Cols(Col_ICD9CPTCode).Width = _TotalWidth * 0.85

                .SetData(0, Col_ICD9CPTDEscription, "sDescription")
                .Cols(Col_ICD9CPTDEscription).Width = _TotalWidth * 0.85
            End With
        End If

    End Sub
    Public Sub CustomDrugsGridStyle()

        dgCustomGrid.tsbtn_New.Visible = False

        Dim _TotalWidth As Single = dgCustomGrid.C1Task.Width - 5

        ' '' Show Drugs Info
        With dgCustomGrid.C1Task
            .Cols.Fixed = 0
            .Rows.Fixed = 1
            .Cols.Count = Col_DGCustCnt
            .AllowEditing = True
            .ExtendLastCol = True
            .SetData(0, Col_Check, "Select")
            .Cols(Col_Check).Width = _TotalWidth * 0.1
            .Cols(Col_Check).AllowEditing = True
            .Cols(Col_Check).DataType = System.Type.GetType("System.Boolean")

            .SetData(0, Col_Name, "sDescription")
            .Cols(Col_Name).Width = _TotalWidth * 0.85



        End With
    End Sub
    Private Sub dgCustomGrid_CloseClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgCustomGrid.CloseClick
        PnlCustomTask.Visible = False
        If C1HistoryDetails.RowSel >= 0 Then
            C1HistoryDetails.Select(C1HistoryDetails.RowSel, Col_HSmokingStatus)
        End If
    End Sub

    Private Sub dgCustomGrid_OKClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgCustomGrid.OKClick
        Dim r As Integer = C1HistoryDetails.RowSel

        Dim Strdata As String = ""
        Dim Strcode As String = ""

        Dim cnt As Integer = 0
        If strbuttonStatus = "Smoking" Then
            For i As Integer = 0 To dgCustomGrid.C1Task.Rows.Count - 1
                If dgCustomGrid.C1Task.GetCellCheck(i, 0) = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                    ''Modified on 20110330-Extra new line was getting added after last reaction in string-so problem was coming in Chk_Modification()
                    If Strdata.Trim = "" Then
                        Strdata = dgCustomGrid.GetItem(i, 2).ToString
                        Strcode = dgCustomGrid.GetItem(i, 1).ToString
                    Else
                        Strdata &= vbNewLine & dgCustomGrid.GetItem(i, 2).ToString
                        Strcode = Strcode & "," & dgCustomGrid.GetItem(i, 1).ToString
                    End If
                    cnt = cnt + 1
                End If
            Next
        ElseIf strbuttonStatus = "Initial Exam" Then
            If dgCustomGrid.C1Task.Row > 0 Then
                Strdata = dgCustomGrid.GetItem(dgCustomGrid.C1Task.Row, 0).ToString
            End If

        ElseIf strbuttonStatus = "Family Member" Then
            If IsNothing(dt) = False Then
                If dt.Rows.Count > 0 Then
                    For i As Integer = 0 To dt.Rows.Count - 1
                        If dt.Rows(i)(7) = "True" Then
                            ''Modified on 20110330-Extra new line was getting added after last reaction in string-so problem was coming in Chk_Modification()
                            If Strdata.Trim = "" Then
                                Strdata = dt.Rows(i)(1).ToString()
                                Strcode = dt.Rows(i)(0).ToString()
                            Else
                                Strdata &= vbNewLine & dt.Rows(i)(1).ToString()
                                Strcode &= "," & dt.Rows(i)(0).ToString()
                            End If

                            cnt = cnt + 1
                        End If
                    Next
                    'Bug No 50165 Resolved::History-Application is showing an exception::20130506
                    If (Strdata.Length + Strcode.Length + 10) > 255 Then
                        MessageBox.Show("Too many Family Member's are selected. Select only the onces which are needed.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Exit Sub
                    End If
                End If
            End If

            ''Modified by Mayuri:To fix issue related to check unchek reaction after search-20111216
        ElseIf strbuttonStatus = "Reaction" Then
            If IsNothing(dt) = False Then
                If dt.Rows.Count > 0 Then
                    For i As Integer = 0 To dt.Rows.Count - 1
                        If dt.Rows(i)(2) = "True" Then
                            ''Modified on 20110330-Extra new line was getting added after last reaction in string-so problem was coming in Chk_Modification()
                            If Strdata.Trim = "" Then
                                Strdata = dt.Rows(i)(1).ToString()
                            Else
                                Strdata &= vbNewLine & dt.Rows(i)(1).ToString()
                            End If

                            cnt = cnt + 1
                        End If
                    Next
                    'Bug No 50165 Resolved::History-Application is showing an exception::20130506
                    If Strdata.Length > 255 Then
                        MessageBox.Show("Too many Reaction's are selected. Select only the onces which are needed.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Exit Sub
                    End If
                End If
            End If
        ElseIf strbuttonStatus = "ICD9" Then
            '   For i As Integer = 0 To dgCustomGrid.C1Task.Rows.Count - 1
            If Strdata.Trim = "" Then
                If dgCustomGrid.C1Task.Row > 0 Then


                    Strdata = dgCustomGrid.GetItem(dgCustomGrid.C1Task.Row, 0).ToString & " : " & dgCustomGrid.GetItem(dgCustomGrid.C1Task.Row, 1).ToString
                End If
            End If
            '  cnt = cnt + 1
            ' End If
            ' Next
        ElseIf strbuttonStatus = "CPT" Then
            ' For i As Integer = 0 To dgCustomGrid.C1Task.Rows.Count - 1
            If Strdata.Trim = "" Then
                Strdata = dgCustomGrid.GetItem(dgCustomGrid.C1Task.Row, 0).ToString & " : " & dgCustomGrid.GetItem(dgCustomGrid.C1Task.Row, 1).ToString
            End If
            '  cnt = cnt + 1
            ' End If
            '  Next
        End If

        If r >= 0 Then
            If strbuttonStatus = "Smoking" Then
                If Strdata <> String.Empty Then
                    C1HistoryDetails.Rows(r).Height = C1HistoryDetails.Rows.DefaultSize * cnt
                    C1HistoryDetails.SetData(r, Col_HSmokingStatus, Strdata)
                Else
                    C1HistoryDetails.SetData(r, Col_HSmokingStatus, Strdata)
                    C1HistoryDetails.Rows(r).Height = C1HistoryDetails.Rows.DefaultSize * 1     ''For resetting the height 
                End If
                C1HistoryDetails.SetData(r, Col_SnoMedCode, Strcode)
            ElseIf strbuttonStatus = "Initial Exam" Then
                C1HistoryDetails.SetData(r, Col_HsReaction, Strdata)
            ElseIf strbuttonStatus = "Family Member" Then
                If Strdata <> String.Empty Then
                    C1HistoryDetails.Rows(r).Height = C1HistoryDetails.Rows.DefaultSize * cnt
                    C1HistoryDetails.SetData(r, Col_HsReaction, Strdata)
                    C1HistoryDetails.SetData(r, "nMemberId", Strcode)
                Else
                    C1HistoryDetails.SetData(r, Col_HsReaction, Strdata)
                    C1HistoryDetails.SetData(r, "nMemberId", Strcode)
                    ''for clearing the data 
                    C1HistoryDetails.Rows(r).Height = C1HistoryDetails.Rows.DefaultSize * 1     ''For resetting the height 
                End If
            ElseIf strbuttonStatus = "Reaction" Then
                If Strdata <> String.Empty Then
                    C1HistoryDetails.Rows(r).Height = C1HistoryDetails.Rows.DefaultSize * cnt
                    C1HistoryDetails.SetData(r, Col_HsReaction, Strdata)
                Else
                    C1HistoryDetails.SetData(r, Col_HsReaction, Strdata)
                    ''for clearing the data 
                    C1HistoryDetails.Rows(r).Height = C1HistoryDetails.Rows.DefaultSize * 1     ''For resetting the height 
                End If
            ElseIf strbuttonStatus = "ICD9" Then

                C1HistoryDetails.SetData(r, Col_HsICD9, Strdata)

            ElseIf strbuttonStatus = "CPT" Then

                C1HistoryDetails.SetData(r, col_HCPT, Strdata)
            End If
        End If

        C1HistoryDetails.Select(r, Col_HSmokingStatus)
        PnlCustomTask.Visible = False
    End Sub

    Private Sub LoadUserGrid()
        Try
            AddControl()
            If Not IsNothing(dgCustomGrid) Then
                dgCustomGrid.Visible = True
                PnlCustomTask.Width = 320
                PnlCustomTask.Height = 220
                dgCustomGrid.txtsearch.Width = 120

                dgCustomGrid.BringToFront()
                BindUserGrid()
                dgCustomGrid.Selectsearch(CustomDataGrid.enmcontrol.Search)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Patient Messages", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LoadUserGridFamily()
        Try
            AddControlfamily()
            If Not IsNothing(dgCustomGrid) Then
                dgCustomGrid.Visible = True

                PnlCustomTask.Width = 320
                PnlCustomTask.Height = 220
                dgCustomGrid.txtsearch.Width = 120
                dgCustomGrid.BringToFront()
                BindUserGridFamily()
                dgCustomGrid.Selectsearch(CustomDataGrid.enmcontrol.Search)
                PnlCustomTask.Width = 360  ''added for bugid 70930
                dgCustomGrid.Width = PnlCustomTask.Width
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Patient Messages", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    'Private Sub LoadUserGridICD9()
    '    Try
    '        AddControlICD9()
    '        If Not IsNothing(dgCustomGrid) Then
    '            dgCustomGrid.Visible = True
    '            PnlCustomTask.Width = 300
    '            PnlCustomTask.Height = 220
    '            dgCustomGrid.txtsearch.Width = 120

    '            dgCustomGrid.BringToFront()
    '            BindUserGridICD9()
    '            dgCustomGrid.Selectsearch(CustomDataGrid.enmcontrol.Search)
    '        End If
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message, "Patient Messages", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub
    'Private Sub LoadUserGridCPT()
    '    Try
    '        AddControlCPT()
    '        If Not IsNothing(dgCustomGrid) Then
    '            dgCustomGrid.Visible = True
    '            PnlCustomTask.Width = 300
    '            PnlCustomTask.Height = 220
    '            dgCustomGrid.txtsearch.Width = 120

    '            dgCustomGrid.BringToFront()
    '            BindUserGridCPT()
    '            dgCustomGrid.Selectsearch(CustomDataGrid.enmcontrol.Search)
    '        End If
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message, "Patient Messages", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub
    ''Added Rahul on 20100906
    Private Sub LoadUserGridSmoking()
        Try
            AddControlSmoking()
            If Not IsNothing(dgCustomGrid) Then
                dgCustomGrid.Visible = True
                PnlCustomTask.Width = 360
                PnlCustomTask.Height = 220
                dgCustomGrid.txtsearch.Width = 120

                dgCustomGrid.BringToFront()
                BindUserGridSmoking()
                dgCustomGrid.Width = PnlCustomTask.Width
                dgCustomGrid.Selectsearch(CustomDataGrid.enmcontrol.Search)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Patient Messages", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    ''
    Private Sub LoadUserGridIntialExam()
        Try
            AddControlInitialExam()
            If Not IsNothing(dgCustomGrid) Then
                dgCustomGrid.Visible = True
                PnlCustomTask.Width = 360
                PnlCustomTask.Height = 220
                dgCustomGrid.txtsearch.Width = 120

                dgCustomGrid.BringToFront()
                BindUserGridInitialExam()
                dgCustomGrid.Width = PnlCustomTask.Width
                dgCustomGrid.Selectsearch(CustomDataGrid.enmcontrol.Search)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Patient Messages", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub BindUserGrid()
        Try

            Dim AnotherobjclsPatientHistory As New clsPatientHistory
            ''slr free prev memory
            If Not IsNothing(dt) Then
                dt.Dispose()
                dt = Nothing
            End If
            dt = AnotherobjclsPatientHistory.GetAllCategory("Reaction")

            CustomDrugsGridStyle()
            Dim col As New DataColumn
            col.ColumnName = "Select"
            col.DataType = System.Type.GetType("System.Boolean")

            col.DefaultValue = CBool("False")
            dt.Columns.Add(col)

            If Not IsNothing(dt) Then
                dt.Columns("sDescription").Caption = "Description"
                dgCustomGrid.datasource(dt.DefaultView)
            End If
            ''Reset the grid
            Dim _TotalWidth As Single = dgCustomGrid.C1Task.Width - 5
            dgCustomGrid.C1Task.Cols.Move(dgCustomGrid.C1Task.Cols.Count - 1, 0)
            dgCustomGrid.C1Task.AllowEditing = True
            dgCustomGrid.C1Task.Cols(0).AllowEditing = True
            dgCustomGrid.C1Task.Cols(0).Width = _TotalWidth * 0.1
            dgCustomGrid.C1Task.Cols(1).Visible = False
            dgCustomGrid.C1Task.Cols(2).Width = _TotalWidth * 0.85
            dgCustomGrid.C1Task.Cols(2).AllowEditing = False


            dgCustomGrid.Visible = True
            Dim r As Integer = C1HistoryDetails.RowSel
            If Not IsNothing(C1HistoryDetails.GetData(r, Col_HsReaction)) Then
                If C1HistoryDetails.GetData(r, Col_HsReaction).ToString().Trim() <> "" Then

                    CheckDGCustomGrid()
                End If
            End If

            AnotherobjclsPatientHistory.Dispose()
            AnotherobjclsPatientHistory = Nothing 'Change made to solve memory Leak and word crash issue

        Catch ex As SqlClient.SqlException
            MessageBox.Show(ex.Message, "Show Reports", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Show Reports", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub BindUserGridFamily()
        Try

            Dim AnotehrobjclsPatientHistory As New clsPatientHistory

            ''slr free prev memory
            If Not IsNothing(dt) Then
                dt.Dispose()
                dt = Nothing
            End If
            dt = AnotehrobjclsPatientHistory.getFamilyMember()

            CustomDrugsGridStyle()
            Dim col As New DataColumn
            col.ColumnName = "Select"
            col.DataType = System.Type.GetType("System.Boolean")

            col.DefaultValue = CBool("False")
            dt.Columns.Add(col)

            If Not IsNothing(dt) Then
                dgCustomGrid.datasource(dt.DefaultView)
            End If
            ''Reset the grid
            Dim _TotalWidth As Single = dgCustomGrid.C1Task.Width - 5
            dgCustomGrid.C1Task.Cols.Move(dgCustomGrid.C1Task.Cols.Count - 1, 0)
            dgCustomGrid.C1Task.AllowEditing = True
            dgCustomGrid.C1Task.Cols(0).AllowEditing = True
            dgCustomGrid.C1Task.Cols(0).Width = _TotalWidth * 0.1
            dgCustomGrid.C1Task.Cols(1).Visible = False
            dgCustomGrid.C1Task.Cols(2).Width = _TotalWidth * 0.85
            dgCustomGrid.C1Task.Cols(2).AllowEditing = False
            dgCustomGrid.C1Task.Cols(3).Visible = False
            dgCustomGrid.C1Task.Cols(4).Visible = False
            dgCustomGrid.C1Task.Cols(5).Visible = False
            dgCustomGrid.C1Task.Cols(6).Visible = False
            dgCustomGrid.C1Task.Cols(7).Visible = False


            dgCustomGrid.Visible = True
            Dim r As Integer = C1HistoryDetails.RowSel
            If Not IsNothing(C1HistoryDetails.GetData(r, Col_HsReaction)) Then
                If C1HistoryDetails.GetData(r, Col_HsReaction).ToString().Trim() <> "" Then

                    CheckDGCustomGrid()
                End If
            End If
            AnotehrobjclsPatientHistory.Dispose()
            AnotehrobjclsPatientHistory = Nothing 'Change made to solve memory Leak and word crash issue

        Catch ex As SqlClient.SqlException
            MessageBox.Show(ex.Message, "Show Reports", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Show Reports", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    'Private Sub BindUserGridICD9()
    '    Try

    '        Dim objclsPatientHistory As New clsPatientHistory
    '        dt = objclsPatientHistory.GetICD9ORCPTCode(0)
    '        CustomDrugsGridStyle_ICD9CPT("ICD9")
    '        'Dim col As New DataColumn
    '        'col.ColumnName = "Select"
    '        'col.DataType = System.Type.GetType("System.Boolean")

    '        'col.DefaultValue = CBool("False")
    '        'dt.Columns.Add(col)

    '        If Not IsNothing(dt) Then
    '            dt.Columns("sDescription").Caption = "Description"
    '            dt.Columns("sICD9Code").Caption = "ICD9 Code"
    '            dgCustomGrid.datasource(dt.DefaultView)
    '        End If
    '        ''Reset the grid
    '        ' Dim _TotalWidth As Single = dgCustomGrid.C1Task.Width - 5
    '        dgCustomGrid.C1Task.AllowEditing = False
    '        'dgCustomGrid.C1Task.Cols(0).AllowEditing = False
    '        '' dgCustomGrid.C1Task.Cols(0).Width = _TotalWidth * 0.2
    '        'dgCustomGrid.C1Task.Cols(1).Visible = False
    '        'dgCustomGrid.C1Task.Cols(1).Width = _TotalWidth * 0.85
    '        ' dgCustomGrid.C1Task.Cols(2).AllowEditing = False


    '        dgCustomGrid.Visible = True
    '        'Dim r As Integer = C1HistoryDetails.RowSel
    '        'If Not IsNothing(C1HistoryDetails.GetData(r, Col_HsReaction)) Then
    '        '    If C1HistoryDetails.GetData(r, Col_HsReaction).ToString().Trim() <> "" Then

    '        '        CheckDGCustomGrid()
    '        '    End If
    '        'End If
    '        objclsPatientHistory = Nothing 'Change made to solve memory Leak and word crash issue

    '    Catch ex As SqlClient.SqlException
    '        MessageBox.Show(ex.Message, "Show Reports", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message, "Show Reports", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub
    'Private Sub BindUserGridCPT()
    '    Try

    '        Dim objclsPatientHistory As New clsPatientHistory
    '        dt = objclsPatientHistory.GetICD9ORCPTCode(1)
    '        CustomDrugsGridStyle_ICD9CPT("CPT")
    '        'Dim col As New DataColumn
    '        'col.ColumnName = "Select"
    '        'col.DataType = System.Type.GetType("System.Boolean")

    '        'col.DefaultValue = CBool("False")
    '        'dt.Columns.Add(col)

    '        If Not IsNothing(dt) Then
    '            dt.Columns("sDescription").Caption = "Description"
    '            dt.Columns("sCPTCode").Caption = "CPT Code"
    '            dgCustomGrid.datasource(dt.DefaultView)
    '        End If
    '        dgCustomGrid.C1Task.AllowEditing = False
    '        ''Reset the grid
    '        'Dim _TotalWidth As Single = dgCustomGrid.C1Task.Width - 5
    '        ''   dgCustomGrid.C1Task.Cols.Move(dgCustomGrid.C1Task.Cols.Count - 1, 0)
    '        ''   dgCustomGrid.C1Task.AllowEditing = True
    '        'dgCustomGrid.C1Task.Cols(0).AllowEditing = False
    '        ''dgCustomGrid.C1Task.Cols(0).Width = _TotalWidth * 0.2
    '        'dgCustomGrid.C1Task.Cols(1).Visible = False
    '        'dgCustomGrid.C1Task.Cols(1).Width = _TotalWidth * 0.85
    '        '  dgCustomGrid.C1Task.Cols(2).AllowEditing = False


    '        dgCustomGrid.Visible = True
    '        'Dim r As Integer = C1HistoryDetails.RowSel
    '        'If Not IsNothing(C1HistoryDetails.GetData(r, Col_HsReaction)) Then
    '        '    If C1HistoryDetails.GetData(r, Col_HsReaction).ToString().Trim() <> "" Then

    '        '        CheckDGCustomGrid()
    '        '    End If
    '        'End If
    '        objclsPatientHistory = Nothing 'Change made to solve memory Leak and word crash issue

    '    Catch ex As SqlClient.SqlException
    '        MessageBox.Show(ex.Message, "Show Reports", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message, "Show Reports", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub
    Private Sub RemoveControl()
        If Not IsNothing(dgCustomGrid) Then
            'pnlWordObj.Controls.Remove(dgCustomGrid)
            PnlCustomTask.Controls.Remove(dgCustomGrid)
            ' dgCustomGrid.Visible = False
            ''slr free dgCustomGrid
            If Not IsNothing(dgCustomGrid) Then
                dgCustomGrid.Dispose()
            End If
            dgCustomGrid = Nothing
        End If
    End Sub
    Private Sub AddControl()

        If Not IsNothing(dgCustomGrid) Then
            RemoveControl()
        End If

        dgCustomGrid = New CustomTask
        dgCustomGrid.Dock = DockStyle.Fill
        PnlCustomTask.Controls.Add(dgCustomGrid)
        PnlCustomTask.BringToFront()

        Dim y As Int64
        Dim x As Int64
        x = 300
        y = 250

        '  PnlCustomTask.Location = New Point(600, PnlCustomTask.Location.Y)
        PnlCustomTask.Visible = True
        dgCustomGrid.Visible = True
        PnlCustomTask.BringToFront()
        dgCustomGrid.BringToFront()


    End Sub


    Private Sub AddControlfamily()

        If Not IsNothing(dgCustomGrid) Then
            RemoveControl()
        End If

        dgCustomGrid = New CustomTask
        dgCustomGrid.Dock = DockStyle.Fill
        PnlCustomTask.Controls.Add(dgCustomGrid)
        PnlCustomTask.BringToFront()

        Dim y As Int64
        Dim x As Int64
        x = 300
        y = 250

        '  PnlCustomTask.Location = New Point(600, PnlCustomTask.Location.Y)
        PnlCustomTask.Visible = True
        dgCustomGrid.Visible = True
        PnlCustomTask.BringToFront()
        dgCustomGrid.BringToFront()


    End Sub

    Private Sub AddControlICD9()

        If Not IsNothing(dgCustomGrid) Then
            RemoveControl()
        End If

        dgCustomGrid = New CustomTask
        dgCustomGrid.Dock = DockStyle.Fill
        PnlCustomTask.Controls.Add(dgCustomGrid)
        PnlCustomTask.BringToFront()

        Dim y As Int64
        Dim x As Int64
        x = 300
        y = 250

        '  PnlCustomTask.Location = New Point(600, PnlCustomTask.Location.Y)
        PnlCustomTask.Visible = True
        dgCustomGrid.Visible = True
        PnlCustomTask.BringToFront()
        dgCustomGrid.BringToFront()


    End Sub
    Private Sub AddControlCPT()

        If Not IsNothing(dgCustomGrid) Then
            RemoveControl()
        End If

        dgCustomGrid = New CustomTask
        dgCustomGrid.Dock = DockStyle.Fill
        PnlCustomTask.Controls.Add(dgCustomGrid)
        PnlCustomTask.BringToFront()

        Dim y As Int64
        Dim x As Int64
        x = 300
        y = 250

        ' PnlCustomTask.Location = New Point(600, PnlCustomTask.Location.Y)
        PnlCustomTask.Visible = True
        dgCustomGrid.Visible = True
        PnlCustomTask.BringToFront()
        dgCustomGrid.BringToFront()


    End Sub
    ''Added Rahul on 20100906
    Private Sub AddControlInitialExam()

        If Not IsNothing(dgCustomGrid) Then
            RemoveControl()
        End If

        dgCustomGrid = New CustomTask
        RemoveHandler dgCustomGrid.C1Task.MouseDoubleClick, AddressOf Grid_DoubleClick
        AddHandler dgCustomGrid.C1Task.MouseDoubleClick, AddressOf Grid_DoubleClick
        ' dgCustomGrid.Dock = DockStyle.Fill
        PnlCustomTask.Controls.Add(dgCustomGrid)
        PnlCustomTask.BringToFront()

        Dim y As Int64
        Dim x As Int64
        x = 300
        y = 250
        ''''''''''''''''''''''
        PnlCustomTask.Width = 360  ''width change to solve design issue bugid 71130
        dgCustomGrid.Width = PnlCustomTask.Width
        '   PnlCustomTask.Location = New Point(500, PnlCustomTask.Location.Y)
        PnlCustomTask.Visible = True
        dgCustomGrid.Visible = True
        PnlCustomTask.BringToFront()
        dgCustomGrid.BringToFront()
        PnlCustomTask.Height = 220  ''added to resolve bugid 71130
        dgCustomGrid.Height = 215
    End Sub
    Private Sub AddControlSmoking()

        If Not IsNothing(dgCustomGrid) Then
            RemoveControl()
        End If

        dgCustomGrid = New CustomTask

        ' dgCustomGrid.Dock = DockStyle.Fill
        PnlCustomTask.Controls.Add(dgCustomGrid)
        PnlCustomTask.BringToFront()

        Dim y As Int64
        Dim x As Int64
        x = 300
        y = 250
        ''''''''''''''''''''''
        PnlCustomTask.Width = 360  ''width change to solve design issue bugid 71130
        dgCustomGrid.Width = PnlCustomTask.Width
        '   PnlCustomTask.Location = New Point(500, PnlCustomTask.Location.Y)
        PnlCustomTask.Visible = True
        dgCustomGrid.Visible = True
        PnlCustomTask.BringToFront()
        dgCustomGrid.BringToFront()
        PnlCustomTask.Height = 220  ''added to resolve bugid 71130
        dgCustomGrid.Height = 215
    End Sub
    Private Sub Grid_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs)
        dgCustomGrid_OKClick(Nothing, Nothing)
    End Sub
    Private Sub dgCustomGrid_AfterSelChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgCustomGrid.AfterSelChanged
        Dim ind As Integer = dgCustomGrid.GetCurrentrowIndex
        ''change made for bugid 109101
        If (DirectCast(e, C1.Win.C1FlexGrid.RangeEventArgs).NewRange.r1 > -1) AndAlso (DirectCast(e, C1.Win.C1FlexGrid.RangeEventArgs).NewRange.r2 > -1) Then
            Dim strdata As Object = DirectCast(DirectCast(e, C1.Win.C1FlexGrid.RangeEventArgs).NewRange.Data, Object)



            Try
                If Not IsNothing(strdata) Then
                    If (Convert.ToString(strdata).Contains("True") OrElse Convert.ToString(strdata).Contains("False")) Then
                        If strbuttonStatus = "Smoking" And blnchksmk = False Then
                            For i As Integer = 0 To dgCustomGrid.C1Task.Rows.Count - 1
                                If dgCustomGrid.C1Task.GetCellCheck(i, 0) = C1.Win.C1FlexGrid.CheckEnum.Checked AndAlso i <> ind Then
                                    blnchksmk = True
                                    dgCustomGrid.C1Task.SetCellCheck(i, 0, CheckEnum.Unchecked)

                                End If
                            Next

                            blnchksmk = False
                        End If

                    End If
                End If
            Catch ex As Exception
            Finally

            End Try
        End If

    End Sub
    Private Sub cmbAllergyType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbAllergyType.SelectedIndexChanged
        Dim myCount As Integer = 0
        Dim myArrLst As New ArrayList()
        myArrLst.Clear()
        '' SUDHIR 20100914 '' TO SHOW ACTIVE/INACTIVE/ALL ALLERGIES WITH COMBO CHANGE ''
        Try
            If C1HistoryDetails.Rows.Count <= 1 Then
                Exit Sub
            End If
            Select Case cmbAllergyType.SelectedItem.ToString().ToUpper()
                Case "ALL"

                    For iRow As Integer = 1 To C1HistoryDetails.Rows.Count - 1
                        ' If C1HistoryDetails.GetData(iRow, Col_HHidden).ToString() = "Allergies" Then
                        C1HistoryDetails.Rows(iRow).Visible = True
                        ' End If
                    Next

                Case "ACTIVE"
                    For iRow As Integer = 1 To C1HistoryDetails.Rows.Count - 1
                        ' If C1HistoryDetails.GetData(iRow, Col_HHidden).ToString() = "Allergies" Then
                        If C1HistoryDetails.GetCellCheck(iRow, Col_HsActive) = CheckEnum.Unchecked Then
                            C1HistoryDetails.Rows(iRow).Visible = False
                        Else
                            C1HistoryDetails.Rows(iRow).Visible = True
                            ''Adding Active Allegies count.
                            myArrLst.Add(iRow)
                            myCount = iRow
                        End If
                        '  End If

                    Next
                Case "INACTIVE"
                    For iRow As Integer = 1 To C1HistoryDetails.Rows.Count - 1
                        ' If C1HistoryDetails.GetData(iRow, Col_HHidden).ToString() = "Allergies" Then
                        If C1HistoryDetails.GetCellCheck(iRow, Col_HsActive) = CheckEnum.Checked Then
                            C1HistoryDetails.Rows(iRow).Visible = False
                        Else
                            C1HistoryDetails.Rows(iRow).Visible = True
                        End If
                        ' End If
                    Next

            End Select
            C1HistoryDetails.Row = 1
            If myCount = 1 Then
                ''If All Allergies are InActive then don't show the Allergy(Text) Category.
                C1HistoryDetails.Rows(myArrLst.Item(0)).Visible = False
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        End Try
    End Sub

    ''Added Rahul for Snomed on 20101006
    Public Sub FillAllergy(ByVal IsOpenedFromImmz As Boolean)
        If C1HistoryDetails.Rows.Count > 1 Then
            Dim isFound As Boolean = False
            For i As Integer = 0 To dsHistory.Tables("History").Rows.Count - 1
                'Bug #61329: 00000599 : patient has multiple history items in different casing the system will throw an error.
                If Convert.ToString(dsHistory.Tables("History").Rows(i)(Col_HsHistoryItem)) = _strAllergy And Convert.ToString(dsHistory.Tables("History").Rows(i)(Col_HHidden)).ToUpper = "ALLERGIES" Then
                    C1HistoryDetails.Select(i + 1, Col_HistoryItem, i + 1, Col_HistoryItem, True)
                    isFound = True
                    Exit For
                End If
            Next
            If isFound = False Then
                Dim mynode As New gloSnoMed.myTreeNode
                mynode.Text = _strAllergy
                mynode.Tag = _strConceptID
                mynode.ConceptID = _strConceptID
                mynode.DescriptionID = _strDescriptionID
                mynode.SnoMedID = _strSnoMedID
                mynode.NDCCod = _NDCCode
                mynode.RxNormID = _RxNormID
                ' FillICD9InDetailFromImmunization(_strICD9, _strConceptID, _strAllergy)
                ' FillDefinitionInDetailFromimm(_strSnomedDefination)
                FillHistoryFromImmu(mynode, IsOpenedFromImmz)
            End If
        Else
            Dim mynode As New gloSnoMed.myTreeNode
            mynode.Text = _strAllergy
            mynode.Tag = _strConceptID
            mynode.ConceptID = _strConceptID
            mynode.DescriptionID = _strDescriptionID
            mynode.SnoMedID = _strSnoMedID
            ''Added by Mayuri:20110322
            mynode.NDCCod = _NDCCode
            mynode.RxNormID = _RxNormID
            ' FillICD9InDetailFromImmunization(_strICD9, _strConceptID, _strAllergy)
            'FillDefinitionInDetailFromimm(_strSnomedDefination)
            ''End 20110322
            FillHistoryFromImmu(mynode, IsOpenedFromImmz)

        End If
    End Sub

    Public Sub FillHistory(ByVal mynode As gloSnoMed.myTreeNode, Optional ByVal IsOpenFromImmzn As Boolean = False)
        Dim i, j As Integer
        'Code Start-Added by kanchan on 20100826 for rxnorm & ndc code functionality
        Dim _RxNormCode As String = ""
        Dim _NDCCode As String = ""
        Dim strDefination As String = ""
        _NDCCode = mynode.NDCCod
        _RxNormCode = mynode.RxNormID

        RXnormAlergicDrugNDCCode = _NDCCode

        If IsOpenFromImmzn = False Then
            If _NDCCode <> "" Then

                'If gblnClinicDIAlert Then
                '    'DI enabled at machine level
                '    If gblnDIAlert Then
                '        'Auto Drug ALert Enabled
                '        If gblnRxAlert Then
                '            Try
                '                If PerformAutoScreening(_NDCCode) Then
                '                    Dim sndr As Object = Nothing
                '                    Dim evntargs As System.EventArgs = Nothing
                '                    Dim tempNDCLong As Long = CType(_NDCCode, Long)
                '                    blnDIbtnClick = False ''''''send false since this variable will be set to true only when we want to check the DI from PnlDI btn click
                '                    objDrugInteraction_DIScreen_Click1(sndr, evntargs, btntype, tempNDCLong)
                '                    blnDIbtnClick = True
                '                End If
                '                DisplayAlertForUncodedDrugs()
                '            Catch ex As Exception
                '                blnDIbtnClick = True '''''''set the value to by default val
                '                MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                '            Finally
                '                blnDIbtnClick = True '''''''set the value to by default val
                '            End Try
                '        End If
                '        'if  there is no count associated with the DI button then increase the size of pnlDI =340  else keep it 310
                '        '  pnlDI.Width = 370
                '    End If
                'End If
            End If
        End If
        With C1HistoryDetails
            Dim _Row As Integer = 0
            For i = 1 To C1HistoryDetails.Rows.Count - 1

                If C1HistoryDetails.GetData(i, Col_HistoryCategory_Hidden) = BtnText Then                    '' TO Check Duplicate History Item in A Category
                    ''''20072109 Index is change Previously it was i for second If condition 
                    If .GetData(j, Col_HistoryItem) = mynode.Text And .GetData(j, Col_HistoryCategory_Hidden) = BtnText Then
                        Exit Sub
                    End If
                    '' TO Insert the New Item At the END of the CAtegory
                    Try
                        If .GetData(i, Col_HistoryCategory_Hidden) <> .GetData(i + 1, Col_HistoryCategory_Hidden) Then
                            '''' If The Current Category ID Is Not Matchs with the thw Category ID  at Next ROW 
                            '' Then Add new Row at Just After the Current Row i.i At the END of the Category
                            .Rows.Insert(i + 1)
                            _Row = i + 1
                            Exit For
                        End If
                    Catch ex As Exception
                        '''' If The System Does Not Get the ROW At (i+1) Position then it Throws the Exception
                        '' i.e we ahve to add the Row at the End 
                        .Rows.Insert(i + 1)
                        _Row = i + 1
                        Exit For
                    End Try
                End If
            Next
            ''to check
            'For Each oParentNodeNode As TreeNode In trvDefination.Nodes
            '    strDefination = oParentNodeNode.Text
            '    For Each oISANode As TreeNode In oParentNodeNode.Nodes
            '        strDefination = strDefination & "|" & oISANode.Text
            '        For Each oDefinNode As TreeNode In oISANode.Nodes
            '            strDefination = strDefination & ":" & oDefinNode.Text
            '        Next
            '    Next
            'Next
            Dim sICD9 As String = ""
            'For Each oNode As TreeNode In trvICD9.Nodes  '' to check
            '    If oNode.Nodes.Count > 0 Then
            '        For Each oChildNode As TreeNode In oNode.Nodes
            '            sICD9 = oChildNode.Text.Replace(":", "-")
            '        Next
            '    Else
            '        sICD9 = oNode.Text
            '    End If
            'Next
            Dim AllergyNode As gloSnoMed.myTreeNode ''slr new not needed 
            AllergyNode = mynode
            AllergyNode.NDCCode = _NDCCode
            If _Row = 0 Then
                ''  Category Is Not exists
                .Rows.Add()
                _Row = .Rows.Count - 1
                .SetData(_Row, Col_CategoryID, BtnTag)
                .SetData(_Row, Col_HistoryCategory, BtnText)
                .SetData(_Row, Col_HistoryCategory_Hidden, BtnText)
                .Rows.Insert(_Row + 1)
                _Row = _Row + 1
            End If

            .SetData(_Row, Col_CategoryID, BtnTag)
            .SetData(_Row, Col_HistoryCategory_Hidden, BtnText)
            .SetData(_Row, Col_HistoryItem, AllergyNode.Text)
            .SetData(_Row, Col_Dosage, AllergyNode.Dosage)
            .SetData(_Row, Col_NDCCode, AllergyNode.NDCCode)
            .SetData(_Row, Col_MPID, AllergyNode.mpid)
            .SetData(_Row, Col_DOE_Allergy, DateTime.Now.ToString())
            .SetData(_Row, Col_ConceptId, mynode.ConceptID)
            .SetData(_Row, Col_DescId, mynode.DescriptionID)
            .SetData(_Row, Col_SnoMedID, mynode.SnoMedID)
            .SetData(_Row, Col_ICD9, sICD9)
            .SetData(_Row, Col_Description, strDefination)
            '.SetData(_Row, Col_ResolvedEndDate, DateTime.Now.ToString())
            If mynode.ConceptID.Trim = "0" Then
                lblconcptid.Text = ""
            Else
                lblconcptid.Text = mynode.ConceptID
            End If


            lbldescid.Text = mynode.DescriptionID
            'Code Start-Added by kanchan on 20100826 for rxnorm & ndc code functionality
            lblSnomedID.Text = mynode.SnoMedID
            lblRxNorm.Text = mynode.RxNormID
            lblNDCid.Text = mynode.NDCCod
            .SetData(_Row, Col_NDCCode, _NDCCode)
            .SetData(_Row, Col_RxNorm, _RxNormCode)
            'Code End-Added by kanchan on 20100826 for rxnorm & ndc code functionality

            Dim rgActive As C1.Win.C1FlexGrid.CellRange = .GetCellRange(_Row, Col_Active, _Row, Col_Active)
            '' Chetan Added 
            Dim rgBtn As C1.Win.C1FlexGrid.CellRange = .GetCellRange(_Row, Col_Button, _Row, Col_Button)
            '' Chetan Added 

            ''Added Rahul on 20100906
            Dim rgBrowseBtn As C1.Win.C1FlexGrid.CellRange = .GetCellRange(_Row, Col_SmokingButton, _Row, Col_SmokingButton)
            ''

            If InStr(BtnText, "Allerg", CompareMethod.Text) = 1 Then
                '' Category is Allergy Then Only we have to show Reactions & Active/Inactive CheckBOX

                rgActive.StyleNew.DataType = GetType(Boolean)
                rgActive.StyleNew.TextAlign = TextAlignEnum.CenterCenter
                rgActive.StyleNew.ImageAlign = ImageAlignEnum.CenterCenter
                rgActive.Checkbox = CheckEnum.Checked       ''To Check the Checkbox at Runtime(true) - While adding

                ' Chetan Added for button row
                .SetCellImage(_Row, Col_Button, imgTreeVIew.Images(5))

            ElseIf BtnText.ToString.Trim = "Smoking Status" Then '' CODE FOR THE '' SMOKING ''
                .SetCellImage(_Row, Col_SmokingButton, imgTreeVIew.Images(5))
            Else
                rgActive.Style = Nothing
                rgBtn.Style = Nothing
                rgBrowseBtn.Style = Nothing
            End If
            'code added by sagar 
            'here we check if the button click is Medical Condition and save the medical condition id in the flex grid whose colum us hide from the user
            If InStr(BtnText, "Medical Condition", CompareMethod.Text) = 1 Then
                .SetData(_Row, Col_MedicalConditionID, mynode.Key)  '' Medical condition Id
            Else 'if button tis other that medical condition then we save 0 in the database and flex grid
                .SetData(_Row, Col_MedicalConditionID, 0)
            End If

            '' By MAhesh ''20070124
            If Not IsNothing(mynode.Tag) Then
                mynode.Tag = Convert.ToString(mynode.Tag)
                If mynode.Tag <> "" Then
                    If mynode.Tag = 1 Then '
                        '' Is Drug 
                        .SetData(_Row, Col_DrugID, mynode.Key)  '' DrugID
                    Else
                        .SetData(_Row, Col_DrugID, 0)
                    End If
                Else
                    .SetData(_Row, Col_DrugID, 0)
                End If
            Else
                .SetData(_Row, Col_DrugID, 0)
            End If
            .Row = _Row
            gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Add, "New History Item Added", gloAuditTrail.ActivityOutCome.Success)
        End With
        If mynode.ConceptID.Trim = "0" Then
            lblconcptid.Text = ""
        Else
            lblconcptid.Text = mynode.ConceptID
        End If
        ''changes done for 8020 snomed prd
        If lblconcptid.Text.Trim() <> "" Then
            If strDefination.Trim() <> "" Then
                lblconcptid.Text = lblconcptid.Text + "-" + strDefination
            End If
        End If
        'Shubhangi 20091209
        'Check the setting Reset search text box after assiging category
        If gblnResetSearchTextBox = True Then
            GloUC_trvHistory.txtsearch.ResetText()
        End If
    End Sub
    Public Sub FillHistoryFromImmu(ByVal mynode As gloSnoMed.myTreeNode, Optional ByVal IsOpenFromImmzn As Boolean = False)
        Dim i, j As Integer
        Dim _RxNormCode As String = ""
        Dim _NDCCode As String = ""
        _NDCCode = mynode.NDCCod
        _RxNormCode = mynode.RxNormID
        Dim itemnumber As Int64 = 0
        RXnormAlergicDrugNDCCode = _NDCCode
        Dim IsOnsetDate As Boolean = False
        Dim IsActive As Boolean = False
        Dim stronsetActiveStatus As String = ""
        'Dim _arrOnsetActive() As String


        Dim _categorytype As String = ""
        If IsOpenFromImmzn = False Then
            If _NDCCode <> "" Then

                'If gblnClinicDIAlert Then
                '    'DI enabled at machine level
                '    If gblnDIAlert Then
                '        'Auto Drug ALert Enabled
                '        If gblnRxAlert Then
                '            Try
                '                If PerformAutoScreening(_NDCCode) Then
                '                    Dim sndr As Object = Nothing
                '                    Dim evntargs As System.EventArgs = Nothing
                '                    Dim tempNDCLong As Long = CType(_NDCCode, Long)
                '                    blnDIbtnClick = False ''''''send false since this variable will be set to true only when we want to check the DI from PnlDI btn click
                '                    objDrugInteraction_DIScreen_Click1(sndr, evntargs, btntype, tempNDCLong)
                '                    blnDIbtnClick = True
                '                End If
                '                DisplayAlertForUncodedDrugs()
                '            Catch ex As Exception
                '                blnDIbtnClick = True '''''''set the value to by default val
                '                MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                '            Finally
                '                blnDIbtnClick = True '''''''set the value to by default val
                '            End Try
                '        End If
                '        'if  there is no count associated with the DI button then increase the size of pnlDI =340  else keep it 310
                '        '  pnlDI.Width = 370

                '    End If

                'End If
            End If
        End If

        With dsHistory.Tables("History")
            Dim _Row As Integer = 0
            For i = 0 To .Rows.Count - 1
                'Bug #61329: 00000599 : patient has multiple history items in different casing the system will throw an error.
                If Convert.ToString(.Rows(i)(Col_HHidden)).ToUpper = BtnText.ToUpper Then                    '' TO Check Duplicate History Item in A Category
                    If Convert.ToString(.Rows(j)(Col_HsHistoryItem)) = mynode.Text And Convert.ToString(.Rows(j)(Col_HHidden)).ToUpper = BtnText.ToUpper Then
                        Exit Sub
                    End If
                    Try
                        'Bug #61329: 00000599 : patient has multiple history items in different casing the system will throw an error.
                        If .Rows(i)(Col_HHidden).ToString.ToUpper <> .Rows(i + 1)(Col_HHidden).ToString.ToUpper Then
                            Dim row As DataRow = dsHistory.Tables("History").NewRow()
                            .Rows.InsertAt(row, (i + 1))
                            _Row = i + 1
                            Exit For
                        End If
                    Catch ex As Exception
                        Dim row As DataRow = dsHistory.Tables("History").NewRow()
                        .Rows.InsertAt(row, (i + 1))
                        _Row = i + 1
                        Exit For
                    End Try
                End If
            Next
            Dim strDefination As String = ""  '' to check
            'For Each oParentNodeNode As TreeNode In trvDefination.Nodes  ''commented for 8020 snomed changes
            '    strDefination = oParentNodeNode.Text
            '    For Each oISANode As TreeNode In oParentNodeNode.Nodes
            '        strDefination = strDefination & "|" & oISANode.Text
            '        For Each oDefinNode As TreeNode In oISANode.Nodes
            '            strDefination = strDefination & ":" & oDefinNode.Text
            '        Next
            '    Next
            'Next
            Dim sICD9 As String = ""
            'For Each oNode As TreeNode In trvICD9.Nodes '' to check
            '    If oNode.Nodes.Count > 0 Then
            '        For Each oChildNode As TreeNode In oNode.Nodes
            '            sICD9 = oChildNode.Text.Replace(":", "-")
            '        Next
            '    Else
            '        sICD9 = oNode.Text
            '    End If
            'Next

            Dim AllergyNode As gloSnoMed.myTreeNode ''slr new not needed 
            AllergyNode = mynode
            AllergyNode.NDCCode = _NDCCode
            If _Row = 0 Then
                ''  Category Is Not exists
                ''
                .Rows.Add()

                _Row = .Rows.Count - 1

                .Rows(_Row)(Col_HCategory) = ""
                .Rows(_Row)(Col_HHidden) = ""
                .Rows(_Row)(Col_HsComments) = ""
                .Rows(_Row)(Col_HCategory) = BtnText
                .Rows(_Row)(Col_HHidden) = BtnText

                If BtnText = "Family History" Then
                    .Rows(_Row)(Col_HsReaction) = "Family Member"
                ElseIf BtnText = "Allergies" Then
                    .Rows(_Row)(Col_HsReaction) = "Reaction"
                ElseIf BtnText = "OB Initial Physical Examination" Then
                    .Rows(_Row)(Col_HsReaction) = "Initial Physical Exam"
                Else
                    .Rows(_Row)(Col_HsReaction) = ""
                End If


                .Rows(_Row)("nRowOrder") = 0
                .Rows.Add(_Row + 1)
                _Row = _Row + 1
            End If
            ''''
            .Rows(_Row)(Col_HHidden) = ""
            .Rows(_Row)(Col_HsHistoryItem) = ""
            .Rows(_Row)(Col_HsDosage) = ""
            .Rows(_Row)(Col_HsNDCCode) = ""
            .Rows(_Row)(Col_Hnmpid) = 0


            .Rows(_Row)(Col_HsConceptID) = ""
            .Rows(_Row)(Col_HsDescriptionID) = ""
            .Rows(_Row)(Col_HsSnomedID) = ""
            .Rows(_Row)(Col_HsICD9) = ""
            .Rows(_Row)(Col_HsDescription) = ""
            .Rows(_Row)(Col_HsDrugName) = ""

            .Rows(_Row)(Col_HCategory) = ""
            .Rows(_Row)(Col_HHidden) = BtnText
            .Rows(_Row)(Col_HsHistoryItem) = AllergyNode.Text
            .Rows(_Row)(Col_HsDrugName) = AllergyNode.Text
            .Rows(_Row)(Col_HsDosage) = AllergyNode.Dosage
            .Rows(_Row)(Col_HsNDCCode) = AllergyNode.NDCCode
            .Rows(_Row)(Col_Hnmpid) = AllergyNode.mpid

            .Rows(_Row)(Col_HDOE_Allergy) = DateTime.Now.ToString()
            .Rows(_Row)(Col_HsConceptID) = mynode.ConceptID
            .Rows(_Row)(Col_HsDescriptionID) = mynode.DescriptionID
            .Rows(_Row)(Col_HsSnomedID) = mynode.SnoMedID
            .Rows(_Row)(Col_HsICD9) = sICD9
            .Rows(_Row)(Col_HsDescription) = strDefination
            .Rows(_Row)("nHistoryID") = 0
            .Rows(_Row)(Col_HsComments) = ""
            .Rows(_Row)("sHistoryType") = "All"

            '01-Apr-13 Aniket: Addition of History Source
            .Rows(_Row)("sHistorySource") = strHistorySource

            'If m_OnsetDate <> "" Then
            '    .Rows(_Row)(col_HOnsetDate) = m_OnsetDate
            'End If
            ''''
            If mynode.ConceptID.Trim = "0" Then
                lblconcptid.Text = ""
            Else
                lblconcptid.Text = mynode.ConceptID
            End If
            lbldescid.Text = mynode.DescriptionID

            lblSnomedID.Text = mynode.SnoMedID
            lblRxNorm.Text = mynode.RxNormID
            lblNDCid.Text = mynode.NDCCod
            .Rows(_Row)(Col_HsNDCCode) = _NDCCode
            .Rows(_Row)(Col_HsRxNormID) = _RxNormCode

            Dim dv As DataView  ''slr new not needed 
            If dsHistory.Tables("History").Rows.Count > 0 Then
                dv = dsHistory.Tables("History").DefaultView
                dv.Sort = "nRowOrder ASC"
                If dv.ToTable.Rows.Count > 0 Then

                    itemnumber = dv.ToTable.Rows(dv.ToTable.Rows.Count - 1)("nRowOrder")


                End If

                ' maxID = dsHistory.Tables("History").AsEnumerable().Max(Function(a) Convert.ToInt32(a("nRowOrder")))
            End If
            dsHistory.Tables("History").Rows(_Row)("nRowOrder") = itemnumber + 1
            Dim _HistoryType As String = ""
            _HistoryType = Convert.ToString(.Rows(_Row)("sHistoryType")).Trim

            _categorytype = Convert.ToString(.Rows(_Row)(Col_HHidden)).Trim
            GetCategorynStatus(_HistoryType, _categorytype, IsActive, IsOnsetDate)
            '_categorytype = Convert.ToString(.Rows(_Row)(Col_HHidden)).Trim
            'If _categorytype = "OB Medical History" Or _categorytype = "OB Genetic History" Or _categorytype = "OB Infection History" Or _categorytype = "OB Initial Physical Examination" Then
            'Else
            '    If Convert.ToString(.Rows(_Row)("sHistoryType")).Trim = "" Then
            '        _categorytype = Convert.ToString(.Rows(_Row)(Col_HHidden)).Trim
            '        _categorytype = getHistoryTypefromcategorymaster(_categorytype)
            '    Else
            '        _categorytype = Convert.ToString(.Rows(_Row)("sHistoryType")).Trim
            '    End If
            'End If

            'If _categorytype <> "" Then
            '    If _categorytype.Length > 2 Then

            '        If _categorytype = "OB Medical History" Or _categorytype = "OB Genetic History" Or _categorytype = "OB Infection History" Or _categorytype = "OB Initial Physical Examination" Then
            '            IsActive = True
            '            IsOnsetDate = False
            '        Else
            '            stronsetActiveStatus = CheckHistoryTypeinStandardTable(_categorytype)
            '            _arrOnsetActive = stronsetActiveStatus.Split(",")
            '            If IsNothing(_arrOnsetActive) = False Then
            '                If _arrOnsetActive.Length >= 1 Then
            '                    IsOnsetDate = _arrOnsetActive.GetValue(0)
            '                    IsActive = _arrOnsetActive.GetValue(1)
            '                End If
            '            End If
            '        End If
            '    End If
            'End If


            'If BtnText.ToString.Trim = "Past Medical History" Then
            '    _categorytype = "Problem"
            'ElseIf BtnText.ToString.Trim = "Surgical History" Then
            '    _categorytype = "Procedures"
            'Else
            '    _categorytype = BtnText.ToString.Trim
            'End If
            'stronsetActiveStatus = CheckHistoryType(.Rows(_Row)("sHistoryType"), _categorytype)
            '_arrOnsetActive = stronsetActiveStatus.Split(",")
            'If IsNothing(_arrOnsetActive) = False Then
            '    If _arrOnsetActive.Length >= 1 Then
            '        IsOnsetDate = _arrOnsetActive.GetValue(0)
            '        IsActive = _arrOnsetActive.GetValue(1)
            '    End If
            'End If


            'If IsOnsetDate = True Then
            '    Dim cs As CellStyle = C1HistoryDetails.Styles.Add("DateTime")
            '    cs.DataType = GetType(DateTime)
            '    cs.TextAlign = TextAlignEnum.CenterCenter
            '    cs.ImageAlign = ImageAlignEnum.CenterCenter
            '    C1HistoryDetails.SetCellStyle(_Row + 1, col_HOnsetDate, cs)
            'End If

            '''''
            If _categorytype = "All" Then

                '  If InStr(BtnText, "Allerg", CompareMethod.Text) = 1 Then
                '' Category is Allergy Then Only we have to show Reactions & Active/Inactive CheckBOX
                Dim cStyle As C1.Win.C1FlexGrid.CellStyle = Nothing
                Dim rgReaction As C1.Win.C1FlexGrid.CellRange = C1HistoryDetails.GetCellRange(_Row + 1, Col_HButton, _Row + 1, Col_HButton)
                Dim rgActive As C1.Win.C1FlexGrid.CellRange = C1HistoryDetails.GetCellRange(_Row + 1, Col_HsActive, _Row + 1, Col_HsActive)

                rgReaction.Style = cStyle
                rgActive.StyleNew.DataType = GetType(Boolean)
                rgActive.StyleNew.TextAlign = TextAlignEnum.CenterCenter
                rgActive.StyleNew.ImageAlign = ImageAlignEnum.CenterCenter
                rgActive.Checkbox = CheckEnum.Checked
                C1HistoryDetails.SetCellCheck(_Row + 1, Col_HsActive, CheckEnum.Checked)
                .Rows(_Row)(Col_HsActive) = True
                C1HistoryDetails.SetCellImage(_Row + 1, Col_HButton, imgTreeVIew.Images(5)) '' Chetan Added

            ElseIf BtnText.ToString.Trim = "Smoking Status" Then '' CODE FOR THE '' SMOKING ''

                Dim rgBrowseBtn As C1.Win.C1FlexGrid.CellRange = C1HistoryDetails.GetCellRange(_Row + 1, Col_HSmokeButton, _Row + 1, Col_HSmokeButton)
                C1HistoryDetails.SetCellImage(_Row + 1, Col_HSmokeButton, imgTreeVIew.Images(5))
                'ElseIf IsActive = True Then
                '    Dim cStyle As C1.Win.C1FlexGrid.CellStyle

                '    Dim rgActive As C1.Win.C1FlexGrid.CellRange = C1HistoryDetails.GetCellRange(_Row + 1, Col_HsActive, _Row + 1, Col_HsActive)
                '    rgActive.StyleNew.DataType = GetType(Boolean)
                '    rgActive.StyleNew.TextAlign = TextAlignEnum.CenterCenter
                '    rgActive.StyleNew.ImageAlign = ImageAlignEnum.CenterCenter
                '    rgActive.Checkbox = CheckEnum.Checked
                '    C1HistoryDetails.SetCellCheck(_Row + 1, Col_HsActive, CheckEnum.Checked)

            ElseIf IsActive Then
                'Dim cStyle As C1.Win.C1FlexGrid.CellStyle

                'Dim rgActive As C1.Win.C1FlexGrid.CellRange = C1HistoryDetails.GetCellRange(_Row + 1, Col_HsActive, _Row + 1, Col_HsActive)


                'rgActive.StyleNew.DataType = GetType(Boolean)
                'rgActive.StyleNew.TextAlign = TextAlignEnum.CenterCenter
                'rgActive.StyleNew.ImageAlign = ImageAlignEnum.CenterCenter
                'rgActive.Checkbox = CheckEnum.Checked
                'C1HistoryDetails.SetCellCheck(_Row + 1, Col_HsActive, CheckEnum.Checked)

            End If








            If InStr(BtnText, "Medical Condition", CompareMethod.Text) = 1 Then

                .Rows(_Row)(Col_HMedicalConditionID) = 0
                .Rows(_Row)(Col_HMedicalConditionID) = mynode.Key

            Else 'if button tis other that medical condition then we save 0 in the database and flex grid
                .Rows(_Row)(Col_HMedicalConditionID) = 0

            End If

            If Not IsNothing(mynode.Tag) Then
                mynode.Tag = Convert.ToString(mynode.Tag)
                If mynode.Tag <> "" Then
                    If mynode.Tag = 1 Then '
                        '' Is Drug 
                        .Rows(_Row)(Col_HnDrugID) = 0
                        .Rows(_Row)(Col_HnDrugID) = mynode.Key

                    Else
                        .Rows(_Row)(Col_HnDrugID) = 0

                    End If
                Else
                    .Rows(_Row)(Col_HnDrugID) = 0

                End If
            Else
                .Rows(_Row)(Col_HnDrugID) = 0

            End If
            '''''





            C1HistoryDetails.Row = _Row + 1




            gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Add, "New History Item Added", gloAuditTrail.ActivityOutCome.Success)
        End With



        If mynode.ConceptID.Trim = "0" Then
            lblconcptid.Text = ""
        Else
            lblconcptid.Text = mynode.ConceptID
        End If

        If gblnResetSearchTextBox = True Then
            GloUC_trvHistory.txtsearch.ResetText()
        End If




        For k As Integer = 0 To C1HistoryDetails.Rows.Count - 1
            If Convert.ToString(C1HistoryDetails.Rows(k)(Col_HCategory)) <> "" And Convert.ToString(C1HistoryDetails.Rows(k)(Col_HsReaction)) <> "" Then
                Dim asgTask1 As C1.Win.C1FlexGrid.CellStyle '= C1HistoryDetails.Styles.Add("asgTask")
                Try
                    If (C1HistoryDetails.Styles.Contains("asgTask")) Then
                        asgTask1 = C1HistoryDetails.Styles("asgTask")
                    Else
                        asgTask1 = C1HistoryDetails.Styles.Add("asgTask")
                        asgTask1.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD 'New System.Drawing.Font(C1HistoryDetails.Font.FontFamily.Name, 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(0))

                    End If
                Catch ex As Exception
                    asgTask1 = C1HistoryDetails.Styles.Add("asgTask")
                    asgTask1.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD 'New System.Drawing.Font(C1HistoryDetails.Font.FontFamily.Name, 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(0))

                End Try
                C1HistoryDetails.SetCellStyle(k, Col_HsReaction, asgTask1)
            End If
        Next



    End Sub

    Public Sub FillHistoryondoubleclick_New(ByVal mynode As gloSnoMed.myTreeNode, ByVal NodeID As Int16, Optional ByVal IsOpenFromImmzn As Boolean = False)
        Dim i, j As Integer ', k As Integer
        Dim _RxNormCode As String = ""
        Dim _NDCCode As String = ""
        Dim rownum As Integer = 0
        Dim IsOnsetDate As Boolean = False


        Dim IsActive As Boolean = False
        Dim stronsetActiveStatus As String = ""
        '  Dim _arrOnsetActive() As String
        _NDCCode = mynode.NDCCod
        _RxNormCode = mynode.RxNormID

        RXnormAlergicDrugNDCCode = _NDCCode

        If IsOpenFromImmzn = False Then
            If _NDCCode <> "" Then
                'If gblnClinicDIAlert Then
                '    If gblnDIAlert Then
                '        If gblnRxAlert Then
                '            Try
                '                If PerformAutoScreening(_NDCCode) Then
                '                    Dim sndr As Object = Nothing
                '                    Dim evntargs As System.EventArgs = Nothing
                '                    Dim tempNDCLong As Long = CType(_NDCCode, Long)
                '                    blnDIbtnClick = False ''''''send false since this variable will be set to true only when we want to check the DI from PnlDI btn click
                '                    objDrugInteraction_DIScreen_Click1(sndr, evntargs, btntype, tempNDCLong)
                '                    blnDIbtnClick = True
                '                End If
                '                DisplayAlertForUncodedDrugs()
                '            Catch ex As Exception
                '                blnDIbtnClick = True '''''''set the value to by default val
                '                MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                '            Finally
                '                blnDIbtnClick = True '''''''set the value to by default val
                '            End Try
                '        End If
                '        'if  there is no count associated with the DI button then increase the size of pnlDI =340  else keep it 310
                '        'pnlDI.Width = 370
                '    End If
                'End If
            End If
        End If
        With dsHistory.Tables("History")
            Dim _Row As Integer = 0
            Try

                For i = 0 To .Rows.Count - 1
                    If dsHistory.Tables("History").Rows(i).RowState = DataRowState.Deleted Then
                    Else
                        'Bug #61329: 00000599 : patient has multiple history items in different casing the system will throw an error.
                        If .Rows(i)(Col_HHidden).ToString.ToUpper = BtnText.ToUpper Then                    '' TO Check Duplicate History Item in A Category
                            'Bug #61329: 00000599 : patient has multiple history items in different casing the system will throw an error.
                            If C1HistoryDetails.Rows(j)(Col_HsHistoryItem) = mynode.Text And C1HistoryDetails.Rows(j)(Col_HHidden).ToString.ToUpper = BtnText.ToUpper Then
                                Exit Sub
                            End If

                            Try
                                'Bug #61329: 00000599 : patient has multiple history items in different casing the system will throw an error.
                                If .Rows(i)(Col_HHidden).ToString.ToUpper <> .Rows(i + 1)(Col_HHidden).ToString.ToUpper Then
                                    Dim row As DataRow = dsHistory.Tables("History").NewRow()
                                    .Rows.InsertAt(row, (i + 1))
                                    _Row = i + 1
                                    Exit For
                                End If


                            Catch ex As Exception
                                Dim row As DataRow = dsHistory.Tables("History").NewRow()
                                .Rows.InsertAt(row, (i + 1))
                                _Row = i + 1
                                Exit For
                            End Try
                        End If
                    End If
                Next

            Catch
            End Try
            Dim AllergyNode As gloSnoMed.myTreeNode  ''slr new not needed 
            ' Dim nHistoryID As Int64

            AllergyNode = mynode
            AllergyNode.NDCCode = _NDCCode

            AllergyNode.AllergyClassID = mynode.AllergyClassID
            'Dim _dataRow As DataRow
            If _Row = 0 Then

                dsHistory.Tables("History").Rows.Add()
                C1HistoryDetails.Rows(_Row + 1).AllowEditing = False
                _Row = .Rows.Count - 1
                .Rows(_Row)(Col_HCategory) = ""
                .Rows(_Row)(Col_HHidden) = ""
                .Rows(_Row)(Col_HsComments) = ""
                .Rows(_Row)(Col_HCategory) = BtnText
                .Rows(_Row)(Col_HHidden) = BtnText


                If BtnText = "Family History" Then
                    .Rows(_Row)(Col_HsReaction) = "Family Member"
                ElseIf BtnText = "Allergies" Then
                    .Rows(_Row)(Col_HsReaction) = "Reaction"
                ElseIf BtnText = "OB Initial Physical Examination" Then
                    .Rows(_Row)(Col_HsReaction) = "Initial Physical Exam"
                Else
                    .Rows(_Row)(Col_HsReaction) = ""
                End If


                .Rows(_Row)("nRowOrder") = 0
                dsHistory.Tables("History").Rows.Add(_Row + 1)

                _Row = _Row + 1
            End If
            Dim itemnumber As Int64 = 0
            .Rows(_Row)(Col_HHidden) = ""
            .Rows(_Row)(Col_HsHistoryItem) = ""
            .Rows(_Row)(Col_HsDosage) = ""
            .Rows(_Row)(Col_HsNDCCode) = ""

            .Rows(_Row)(Col_Hnmpid) = 0


            .Rows(_Row)(Col_HsConceptID) = ""
            .Rows(_Row)(Col_HsDescriptionID) = ""
            .Rows(_Row)(Col_HsSnomedID) = ""
            .Rows(_Row)(Col_HsICD9) = ""
            .Rows(_Row)(col_HCPT) = ""
            .Rows(_Row)(Col_HsDescription) = ""
            .Rows(_Row)(Col_HsDrugName) = ""
            .Rows(_Row)(Col_DeviceList_ID) = 0
            .Rows(_Row)(COl_sProcStatus) = ""
            .Rows(_Row)(Col_HCategory) = ""
            .Rows(_Row)(Col_HHidden) = BtnText
            .Rows(_Row)(Col_HsHistoryItem) = AllergyNode.Text
            .Rows(_Row)(Col_HsDrugName) = AllergyNode.Text
            .Rows(_Row)(Col_HsDosage) = AllergyNode.Dosage
            .Rows(_Row)(Col_HsNDCCode) = AllergyNode.NDCCode
            .Rows(_Row)(Col_Hnmpid) = AllergyNode.mpid


            .Rows(_Row)(Col_HDOE_Allergy) = DateTime.Now.ToString()


            .Rows(_Row)(Col_HsConceptID) = mynode.ConceptID
            .Rows(_Row)(Col_HsDescriptionID) = mynode.DescriptionID
            .Rows(_Row)(Col_HsSnomedID) = mynode.SnoMedID
            '.Rows(_Row)(Col_HsICD9) = _ICD91
            .Rows(_Row)(Col_HsDescription) = _Defination1
            .Rows(_Row)("nHistoryID") = 0
            .Rows(_Row)(Col_HsComments) = mynode.Comments
            .Rows(_Row)(Col_HsICD9) = mynode.ICD9
            .Rows(_Row)(col_HCPT) = mynode.CPT
            .Rows(_Row)("sHistoryType") = mynode.HistoryType

            '29-Mar-13 Aniket: Addition of source column on the History screen
            .Rows(_Row)("sHistorySource") = strHistorySource
            .Rows(_Row)("nICDRevision") = mynode.nICDRevision      ''Bug #65177: added for ICD10 implementation
            If mynode.ConceptID.Trim = "0" Then
                lblconcptid.Text = ""
            Else
                lblconcptid.Text = mynode.ConceptID
            End If
            lbldescid.Text = mynode.DescriptionID
            'Code Start-Added by kanchan on 20100826 for rxnorm & ndc code functionality
            lblSnomedID.Text = mynode.SnoMedID



            lblRxNorm.Text = mynode.RxNormID
            lblNDCid.Text = mynode.NDCCod
            .Rows(_Row)(Col_HsNDCCode) = _NDCCode
            .Rows(_Row)(Col_HsRxNormID) = _RxNormCode
            .Rows(_Row)("sLoincCode") = Convert.ToString(mynode.LoincCode)
            .Rows(_Row)("sLoincDescr") = Convert.ToString(mynode.LoincDescription)

            .Rows(_Row)("srefusalcode") = Convert.ToString(mynode.ReasonConceptCode)
            .Rows(_Row)("srefusalDesc") = Convert.ToString(mynode.ReasonConceptDesc)

            .Rows(_Row)("AllergyClassId") = Convert.ToString(mynode.AllergyClassID) ''added for 2015 certification
            .Rows(_Row)("sValueSetOID") = Convert.ToString(mynode.CQMId)
            .Rows(_Row)("sValueSetName") = Convert.ToString(mynode.CQMDesc)
            .Rows(_Row)(Col_DeviceList_ID) = nDeviceList_ID
            .Rows(_Row)("sProcStatus") = Convert.ToString(mynode.sProcStatus) ''Issue Resolved For column data Mismatch

            .Rows(_Row)("AllergySeverity") = "" ''added for 2015 certification. when added new allergy item from tree not set the allergy severity to blank
            .Rows(_Row)(Col_ResolvedEndDate) = "" ''added for 2015 certification
            .Rows(_Row)(Col_AllergyIntelorenceCode) = ""
            .Rows(_Row)("RowID") = NodeID ''resolved bug Bug #108777
            .Rows(_Row)("CVXCode") = Convert.ToString(mynode.CVXCode)
            .Rows(_Row)("CVXDesc") = Convert.ToString(mynode.CVXDesc)
            Dim s As Integer
            Dim _currentrowitem As String
            Dim _rowcount As Integer

            _currentrowitem = Convert.ToString(dsHistory.Tables("History").Rows(_Row)(Col_HsHistoryItem))
            For s = 0 To C1HistoryDetails.Rows.Count - 1
                If Convert.ToString(C1HistoryDetails.Rows(s)(Col_HsHistoryItem)) = _currentrowitem And Convert.ToString(C1HistoryDetails.Rows(s)("RowID")) = "1" Then
                    _rowcount = s
                    .Rows(_Row)("RowID") = 0
                    NodeID = 0
                    Exit For
                End If
            Next

            Dim dv As DataView  ''slr new not needed 
            If dsHistory.Tables("History").Rows.Count > 0 Then
                dv = dsHistory.Tables("History").DefaultView
                dv.Sort = "nRowOrder ASC"
                If dv.ToTable.Rows.Count > 0 Then

                    itemnumber = dv.ToTable.Rows(dv.ToTable.Rows.Count - 1)("nRowOrder")


                End If


            End If


            dsHistory.Tables("History").Rows(_Row)("nRowOrder") = itemnumber + 1
            Dim _categorytype As String = ""
            Dim _HistoryType As String = ""
            _HistoryType = Convert.ToString(.Rows(_Row)("sHistoryType")).Trim

            _categorytype = Convert.ToString(.Rows(_Row)(Col_HHidden)).Trim
            GetCategorynStatus(_HistoryType, _categorytype, IsActive, IsOnsetDate)
            '_categorytype = Convert.ToString(.Rows(_Row)(Col_HHidden)).Trim
            'If _categorytype = "OB Medical History" Or _categorytype = "OB Genetic History" Or _categorytype = "OB Infection History" Or _categorytype = "OB Initial Physical Examination" Then

            'Else

            '    If Convert.ToString(.Rows(_Row)("sHistoryType")).Trim = "" Then
            '        _categorytype = Convert.ToString(.Rows(_Row)(Col_HHidden)).Trim

            '        _categorytype = getHistoryTypefromcategorymaster(_categorytype)
            '    Else
            '        _categorytype = Convert.ToString(.Rows(_Row)("sHistoryType")).Trim
            '    End If
            'End If

            'If _categorytype <> "" Then
            '    If _categorytype = "OB Medical History" Or _categorytype = "OB Genetic History" Or _categorytype = "OB Infection History" Or _categorytype = "OB Initial Physical Examination" Then
            '        IsActive = True
            '        IsOnsetDate = False
            '    Else
            '        If _categorytype.Length > 2 Then


            '            stronsetActiveStatus = CheckHistoryTypeinStandardTable(_categorytype)
            '            _arrOnsetActive = stronsetActiveStatus.Split(",")
            '            If IsNothing(_arrOnsetActive) = False Then
            '                If _arrOnsetActive.Length >= 1 Then
            '                    IsOnsetDate = _arrOnsetActive.GetValue(0)
            '                    IsActive = _arrOnsetActive.GetValue(1)
            '                End If
            '            End If
            '        End If
            '    End If

            'End If

            ' stronsetActiveStatus = CheckHistoryType(Convert.ToString(.Rows(_Row)("sHistoryType")).Trim, _categorytype)



            '  If BtnText.ToString.Trim <> "Allergies" And BtnText.ToString.Trim <> "Family History" And BtnText.ToString.Trim <> "Social History" Then
            If IsOnsetDate = True Then


                Dim cs As CellStyle '= C1HistoryDetails.Styles.Add("DateTime")
                Try
                    If (C1HistoryDetails.Styles.Contains("DateTime")) Then
                        cs = C1HistoryDetails.Styles("DateTime")
                    Else
                        cs = C1HistoryDetails.Styles.Add("DateTime")

                    End If
                Catch ex As Exception
                    cs = C1HistoryDetails.Styles.Add("DateTime")

                End Try
                cs.DataType = GetType(DateTime)
                ''
                If (gblnEnableCQMCypressTesting) Then
                    cs.Format = "MM/dd/yyyy hh:mm tt"
                Else
                    cs.Format = "MM/dd/yyyy"
                End If
                cs.TextAlign = TextAlignEnum.CenterCenter
                cs.ImageAlign = ImageAlignEnum.CenterCenter
                C1HistoryDetails.SetCellStyle(_rowcount, col_HOnsetDate, cs)
                '  End If
            End If









            'If InStr(BtnText, "Allerg", CompareMethod.Text) = 1 Then
            If IsActive = True And _categorytype = "All" Then
                '' Category is Allergy Then Only we have to show Reactions & Active/Inactive CheckBOX                           
                Dim cStyle As C1.Win.C1FlexGrid.CellStyle = Nothing
                Dim rgReaction As C1.Win.C1FlexGrid.CellRange = C1HistoryDetails.GetCellRange(_rowcount, Col_HButton, _rowcount, Col_HButton)
                Dim rgActive As C1.Win.C1FlexGrid.CellRange = C1HistoryDetails.GetCellRange(_rowcount, Col_HsActive, _rowcount, Col_HsActive)

                rgReaction.Style = cStyle
                rgActive.StyleNew.DataType = GetType(Boolean)
                rgActive.StyleNew.TextAlign = TextAlignEnum.CenterCenter
                rgActive.StyleNew.ImageAlign = ImageAlignEnum.CenterCenter
                rgActive.Checkbox = CheckEnum.Checked
                C1HistoryDetails.SetCellCheck(_rowcount, Col_HsActive, CheckEnum.Checked)
                .Rows(_Row)(Col_HsActive) = True
                C1HistoryDetails.SetCellImage(_rowcount, Col_HButton, imgTreeVIew.Images(5)) '' Chetan Added
            ElseIf _categorytype = "OB Initial Physical Examination" Then

                Dim cStyle As C1.Win.C1FlexGrid.CellStyle = Nothing
                Dim rgReaction As C1.Win.C1FlexGrid.CellRange = C1HistoryDetails.GetCellRange(_rowcount, Col_HButton, _rowcount, Col_HButton)
                ' Dim rgActive As C1.Win.C1FlexGrid.CellRange = C1HistoryDetails.GetCellRange(_rowcount, Col_HsActive, _rowcount, Col_HsActive)

                rgReaction.Style = cStyle
                'rgActive.StyleNew.DataType = GetType(Boolean)
                'rgActive.StyleNew.TextAlign = TextAlignEnum.CenterCenter
                'rgActive.StyleNew.ImageAlign = ImageAlignEnum.CenterCenter
                'rgActive.Checkbox = CheckEnum.Checked
                ' C1HistoryDetails.SetCellCheck(_rowcount, Col_HsActive, CheckEnum.Checked)

                ' .Rows(_Row)(Col_HsActive) = True
                C1HistoryDetails.SetCellStyle(_rowcount, Col_HsActive, "")
                C1HistoryDetails.SetCellImage(_rowcount, Col_HButton, imgTreeVIew.Images(5)) '' Chetan Added
            ElseIf _categorytype = "Fam" Then
                '' Category is Allergy Then Only we have to show Reactions & Active/Inactive CheckBOX                           
                Dim cStyle As C1.Win.C1FlexGrid.CellStyle = Nothing
                Dim rgReaction As C1.Win.C1FlexGrid.CellRange = C1HistoryDetails.GetCellRange(_rowcount, Col_HButton, _rowcount, Col_HButton)
                Dim rgActive As C1.Win.C1FlexGrid.CellRange = C1HistoryDetails.GetCellRange(_rowcount, Col_HsActive, _rowcount, Col_HsActive)
                If IsActive Then


                    rgReaction.Style = cStyle
                    rgActive.StyleNew.DataType = GetType(Boolean)
                    rgActive.StyleNew.TextAlign = TextAlignEnum.CenterCenter
                    rgActive.StyleNew.ImageAlign = ImageAlignEnum.CenterCenter
                    rgActive.Checkbox = CheckEnum.Checked
                    C1HistoryDetails.SetCellCheck(_rowcount, Col_HsActive, CheckEnum.Checked)
                    .Rows(_Row)(Col_HsActive) = True
                End If

                C1HistoryDetails.SetCellImage(_rowcount, Col_HButton, imgTreeVIew.Images(5)) '' Chetan Added
            ElseIf BtnText.ToString.Trim = "Smoking Status" Then '' CODE FOR THE '' SMOKING ''

                'Dim cStyle As C1.Win.C1FlexGrid.CellStyle




                Dim strReactions As String = ""
                Dim AnotehrobjclsPatientHistory As New clsPatientHistory
                strReactions = AnotehrobjclsPatientHistory.GetSmokingStatus(mynode.Key)
                .Rows(_Row)(Col_HSmokingStatus) = strReactions

                Dim rgBrowseBtn As C1.Win.C1FlexGrid.CellRange = C1HistoryDetails.GetCellRange(_rowcount, Col_HSmokeButton, _rowcount, Col_HSmokeButton)
                C1HistoryDetails.SetCellImage(_rowcount, Col_HSmokeButton, imgTreeVIew.Images(5))
                C1HistoryDetails.SetData(_rowcount, Col_HSmokeButton, strReactions)
                AnotehrobjclsPatientHistory.Dispose()
                AnotehrobjclsPatientHistory = Nothing

            ElseIf IsActive = True Then
                'Dim cStyle As C1.Win.C1FlexGrid.CellStyle
                Dim rgActive As C1.Win.C1FlexGrid.CellRange = C1HistoryDetails.GetCellRange(_rowcount, Col_HsActive, _rowcount, Col_HsActive)
                rgActive.StyleNew.DataType = GetType(Boolean)
                rgActive.StyleNew.TextAlign = TextAlignEnum.CenterCenter
                rgActive.StyleNew.ImageAlign = ImageAlignEnum.CenterCenter
                rgActive.Checkbox = CheckEnum.Checked
                C1HistoryDetails.SetCellCheck(_rowcount, Col_HsActive, CheckEnum.Checked)
            End If

            'code added by sagar 
            'here we check if the button click is Medical Condition and save the medical condition id in the flex grid whose colum us hide from the user
            If InStr(BtnText, "Medical Condition", CompareMethod.Text) = 1 Then
                .Rows(_Row)(Col_HMedicalConditionID) = 0
                .Rows(_Row)(Col_HMedicalConditionID) = mynode.Key

            Else 'if button tis other that medical condition then we save 0 in the database and flex grid
                .Rows(_Row)(Col_HMedicalConditionID) = 0

            End If

            '' By MAhesh ''20070124
            If Not IsNothing(mynode.Tag) Then
                mynode.Tag = Convert.ToString(mynode.Tag)
                If mynode.Tag <> "" Then
                    If mynode.Tag = 1 Then '
                        '' Is Drug 
                        .Rows(_Row)(Col_HnDrugID) = 0
                        .Rows(_Row)(Col_HnDrugID) = mynode.Key
                    Else
                        .Rows(_Row)(Col_HnDrugID) = 0
                    End If
                Else
                    .Rows(_Row)(Col_HnDrugID) = 0
                End If
            Else
                .Rows(_Row)(Col_HnDrugID) = 0
            End If
            C1HistoryDetails.Row = _rowcount
            gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Add, "New History Item Added", gloAuditTrail.ActivityOutCome.Success)
        End With

        For iRow As Integer = 0 To C1HistoryDetails.Rows.Count - 1
            If Convert.ToString(C1HistoryDetails.Rows(iRow)(Col_HCategory)) <> "" And Convert.ToString(C1HistoryDetails.Rows(iRow)(Col_HsReaction)) <> "" Then
                Dim asgTask1 As C1.Win.C1FlexGrid.CellStyle '= C1HistoryDetails.Styles.Add("asgTask")
                Try
                    If (C1HistoryDetails.Styles.Contains("asgTask")) Then
                        asgTask1 = C1HistoryDetails.Styles("asgTask")
                    Else
                        asgTask1 = C1HistoryDetails.Styles.Add("asgTask")
                        asgTask1.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD 'New System.Drawing.Font(C1HistoryDetails.Font.FontFamily.Name, 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(0))

                    End If
                Catch ex As Exception
                    asgTask1 = C1HistoryDetails.Styles.Add("asgTask")
                    asgTask1.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD 'New System.Drawing.Font(C1HistoryDetails.Font.FontFamily.Name, 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(0))

                End Try
                C1HistoryDetails.SetCellStyle(iRow, Col_HsReaction, asgTask1)
            End If
        Next

        If mynode.ConceptID.Trim = "0" Then
            lblconcptid.Text = ""
        Else
            If Not IsNothing(_Defination1) AndAlso mynode.ConceptID.Trim() <> "" Then
                If _Defination1.Trim() <> "" Then
                    lblconcptid.Text = mynode.ConceptID + "-" + _Defination1
                End If
            End If

        End If




        'Shubhangi 20091209
        'Check the setting Reset search text box after assiging category
        If gblnResetSearchTextBox = True Then
            GloUC_trvHistory.txtsearch.ResetText()
        End If
    End Sub
    ''Event Changed by kanchan 201009 for MU 
    ''text box changed to gloSearchTextBox
    Private Sub txtSMSearch_TextChanged() Handles txtSMSearch.SearchFired
        ''END-Changed by kanchan 201009 for MU 
        'Try
        '    If txtSMSearch.Text.Trim() <> "" Then
        '        Me.Cursor = Cursors.WaitCursor
        '        If IsNothing(oSnoMed) Then
        '            Return
        '        End If
        '        oSnoMed.SearchSnomed(txtSMSearch.Text.Trim(), False, trvFindings)
        '        If trvFindings.Nodes.Count > 0 Then
        '            trvFindings.SelectedNode = trvFindings.Nodes(0)
        '            txtSMSearch.Focus()
        '        End If
        '        Me.Cursor = Cursors.Default
        '    End If
        'Catch ex As Exception

        'Finally
        '    Me.Cursor = Cursors.Default
        'End Try
    End Sub
    'Private Sub trvFindings_NodeMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles trvFindings.NodeMouseClick
    '    If e.Button = Windows.Forms.MouseButtons.Right Then
    '        With trvFindings
    '            Dim r As Integer = .HitTest(e.X, e.Y).Node.Index
    '            If r >= 0 Then
    '                trvFindings.SelectedNode = trvFindings.GetNodeAt(e.X, e.Y)
    '                trvFindings.ContextMenuStrip = cntFindings

    '            Else
    '                trvFindings.ContextMenuStrip = Nothing
    '            End If
    '        End With
    '    End If
    'End Sub

    'Private Sub trvFindings_NodeMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles trvFindings.NodeMouseDoubleClick
    '    oSnoMed.FillSubtypeHierarchy(e.Node.Tag, e.Node.Text, trvSubType)
    '    If trvSubType.Nodes.Count > 0 Then
    '        trvSubType.SelectedNode = trvSubType.Nodes(0)
    '        trvSubType.Focus()
    '    End If
    '    Dim ConceptID As String = e.Node.Tag.ToString()
    '    Dim DescriptionID As String = oSnoMed.Fill_DescriptionID(ConceptID)
    '    Dim SnoMedID As String = oSnoMed.Fill_SnoMedID(ConceptID)
    'End Sub

    'Private Sub mnu_AddFindings_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnu_AddFindings.Click
    '    Try
    '        gblnIsFindingAdd = True
    '        Dim oNode As TreeNode = trvFindings.SelectedNode

    '        If Not IsNothing(oNode) Then
    '            Dim mynode As New gloSnoMed.myTreeNode
    '            mynode.Text = oNode.Text
    '            mynode.Tag = oNode.Tag
    '            mynode.ConceptID = oNode.Tag
    '            mynode.DescriptionID = oSnoMed.Fill_DescriptionID(oNode.Tag)
    '            mynode.SnoMedID = oSnoMed.Fill_SnoMedID(oNode.Tag)
    '            oSnoMed.Fill_Snomed_ISA_Definition(oNode.Tag, trvDefination)
    '            oSnoMed.Fill_ICD9(oNode.Tag, oNode.Text, trvICD9)
    '            FillHistory(mynode, False) ''bug 6616''when we Add a allergic drug send false value because we need to show DI during this event

    '            If trvDefination.Nodes.Count > 0 Then
    '                trvDefination.SelectedNode = trvDefination.Nodes(0)
    '                trvDefination.Focus()
    '            End If
    '        End If
    '    Catch ex As Exception
    '    Finally
    '        gblnIsFindingAdd = False
    '    End Try
    'End Sub
    Private Sub FillDataInGrid(Optional ByVal dv As DataView = Nothing, Optional ByVal OBNode As gloUserControlLibrary.myTreeNode = Nothing)
        Try
            Dim oNode As gloUserControlLibrary.myTreeNode = Nothing
            If Not IsNothing(OBNode) Then
                oNode = OBNode
            Else
                If Not IsNothing(GloUC_trvHistory.SelectedNode) Then
                    oNode = CType(GloUC_trvHistory.SelectedNode, gloUserControlLibrary.myTreeNode)
                End If

            End If

            Dim dt As DataTable  ''slr new not needed 

            If Not IsNothing(oNode) Then
                If BtnText = "OB Medical History" Or BtnText = "OB Genetic History" Or BtnText = "OB Infection History" Or BtnText = "OB Initial Physical Examination" Then
                    If CheckHistoryItemalreadyexists(oNode, dv) = True Then
                        Exit Sub
                    End If
                End If

                'If BtnText = "Allergies" Then
                '    cmbAllergySeverity.Enabled = True
                'Else
                cmbAllergySeverity.Enabled = False
                'End If

                Dim mynode As New gloSnoMed.myTreeNode
                mynode.Text = oNode.Text
                mynode.Tag = oNode.ConceptID
                mynode.ICD9 = oNode.ICD9
                mynode.CPT = oNode.CPT
                mynode.HistoryType = oNode.HistoryType
                mynode.Key = oNode.ID
                mynode.LoincCode = Convert.ToString(oNode.sLoincCode)
                mynode.LoincDescription = Convert.ToString(oNode.sLoincDescr)
                mynode.ReasonConceptCode = Convert.ToString(oNode.ReasonConceptCode)
                mynode.ReasonConceptDesc = Convert.ToString(oNode.ReasonConceptDesc)
                mynode.AllergyClassID = oNode.AllergyClassID ''added for 2015 certification
                mynode.RxNormID = oNode.RxNormCode
                mynode.CVXCode = oNode.sCVXCode
                mynode.CVXDesc = oNode.sCVXDesc
                mynode.CQMId = Convert.ToString(oNode.CQMId)
                mynode.CQMDesc = Convert.ToString(oNode.CQMDesc)

                If oNode.NDCCode <> "" Then
                    mynode.NDCCod = oNode.NDCCode
                    mynode.mpid = oNode.mpid
                    If gblnCodedHistory = True Then
                        mynode.Dosage = oNode.Description
                    Else
                        mynode.Dosage = oNode.Code
                    End If
                Else
                    If IsNothing(oNode.ConceptID) = False Then

                        If oNode.ConceptID <> "" Then




                            dt = objclsPatientHistory.FillDetailsFromMaster(oNode.ConceptID, oNode.Text)
                            If IsNothing(dt) = False Then
                                If dt.Rows.Count > 0 Then
                                    mynode.Comments = dt.Rows(0)("sComments")
                                    If IsNothing(oNode.ConceptID) = False Then
                                        If Convert.ToString(oNode.ConceptID) <> "" Then
                                            mynode.ConceptID = Convert.ToString(oNode.ConceptID)
                                            mynode.nICDRevision = Convert.ToInt16(dt.Rows(0)("nICDRevision"))  ''Bug #65177: added for ICD10 implementation

                                            mynode.DescriptionID = dt.Rows(0)("sDESCRIPTIONID")
                                            mynode.SnoMedID = dt.Rows(0)("sSnoMedID")
                                            mynode.RxNormID = dt.Rows(0)("sTranID1")
                                            mynode.NDCCod = dt.Rows(0)("sTranID2")
                                            FillDefinitionInDetail(dt)
                                            FillICD9InDetail(dt)
                                        Else
                                            _ICD91 = ""
                                        End If
                                    End If
                                End If

                            End If
                        End If
                    End If
                End If
                FillHistoryondoubleclick_New(mynode, 1, False)
                If _blnRecordLock Then

                End If
            End If
            C1HistoryDetails.SetDataBinding(dsHistory, "History")
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            SetNKHistoryVisibility()
            ''slr free dt
            If Not IsNothing(dt) Then
                dt.Dispose()
                dt = Nothing
            End If
        End Try
    End Sub
    Private Function CheckHistoryItemalreadyexists(ByVal oNode As gloUserControlLibrary.myTreeNode, ByVal dv As DataView) As Boolean
        ' Dim dv As DataView = dsHistory.Tables("History").Copy.DefaultView
        dv.RowFilter = "sHistoryItem = '" & oNode.Text.Trim.Replace("'", "''") & "' and Hidden='" & BtnText.Trim.Replace("'", "''") & "'"
        If dv.Count > 0 Then
            Return True
        Else
            Return False
        End If

    End Function
    Public Function FillDefinitionInDetail(ByVal Definition As DataTable) As Boolean

        Dim _Description As String = ""
        Try
            '  trvDefination.Nodes.Clear()

            If IsNothing(Definition) = False AndAlso Definition.Rows.Count > 0 Then

                If IsDBNull(Definition.Rows(0).Item("sSnomedDefination")) = False Then
                    _Description = CType(Definition.Rows(0).Item("sSnomedDefination"), String)
                    _Defination1 = CType(Definition.Rows(0).Item("sSnomedDescription"), String)
                Else
                    _Description = ""
                    _Defination1 = ""
                End If
                strHeader = Split(_Description, "|")
                If strHeader.Length > 1 Then
                    strHead = strHeader.GetValue(0)
                    Dim oParenetNode As New myTreeNode
                    oParenetNode.Text = strHead ''"Definition : Fully Defined as..."
                    oParenetNode.ImageIndex = 7
                    oParenetNode.SelectedImageIndex = 7
                    ' trvDefination.Nodes.Add(oParenetNode)
                    For i As Integer = 1 To strHeader.Length - 1
                        strDefination = Split(strHeader.GetValue(i), ":")
                        oIsNode = New myTreeNode
                        oIsNode.Text = strDefination.GetValue(0)
                        oIsNode.ImageIndex = 4
                        oIsNode.SelectedImageIndex = 4
                        oParenetNode.Nodes.Add(oIsNode)
                        oDescr = New myTreeNode
                        oDescr.Text = strDefination.GetValue(1)
                        oDescr.ImageIndex = 3
                        oDescr.SelectedImageIndex = 3
                        oIsNode.Nodes.Add(oDescr)
                    Next
                End If
                Return Nothing
            End If
            Return Nothing
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try
    End Function
    'Public Function FillDefinitionInDetailFromimm(ByVal SnomedDfinition As String) As Boolean '' commented for snomed 8020 changes
    '    Try
    '        trvDefination.Nodes.Clear()
    '        strHeader = Split(SnomedDfinition, "|")
    '        If strHeader.Length > 1 Then
    '            strHead = strHeader.GetValue(0)
    '            Dim oParenetNode As New myTreeNode
    '            oParenetNode.Text = strHead ''"Definition : Fully Defined as..."
    '            oParenetNode.ImageIndex = 7
    '            oParenetNode.SelectedImageIndex = 7
    '            trvDefination.Nodes.Add(oParenetNode)
    '            For i As Integer = 1 To strHeader.Length - 1
    '                strDefination = Split(strHeader.GetValue(i), ":")
    '                oIsNode = New myTreeNode
    '                oIsNode.Text = strDefination.GetValue(0)
    '                oIsNode.ImageIndex = 4
    '                oIsNode.SelectedImageIndex = 4
    '                oParenetNode.Nodes.Add(oIsNode)
    '                oDescr = New myTreeNode
    '                oDescr.Text = strDefination.GetValue(1)
    '                oDescr.ImageIndex = 3
    '                oDescr.SelectedImageIndex = 3
    '                oIsNode.Nodes.Add(oDescr)
    '            Next
    '        End If
    '        Return Nothing
    '    Catch ex As Exception
    '        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        Return Nothing
    '    End Try
    'End Function
    Private Function FillICD9InDetail(ByVal dt As DataTable) As Boolean

        ' Dim oChilNode As TreeNode
        Try

            ' trvICD9.Nodes.Clear()
            'trvICD9.Nodes.Add(GloUC_trvHistory.SelectedNode.Text) '' commented for snomed 8020 changes
            'trvICD9.ImageIndex = 4
            ' trvICD9.SelectedImageIndex = 4
            'If IsNothing(dt) = False Then
            '    If dt.Rows.Count > 0 Then
            '        For i As Integer = 0 To dt.Rows.Count - 1
            '            If Convert.ToString(dt.Rows(i)("sICD9")) <> "" Then
            '                oChilNode = New TreeNode()

            '                oChilNode.Text = dt.Rows(i)("sICD9")
            '                oChilNode.Tag = dt.Rows(i)("sConceptID")
            '                oChilNode.ImageIndex = 0
            '                oChilNode.SelectedImageIndex = 0
            '                oChilNode.Collapse()
            '                trvICD9.Nodes(0).Nodes.Add(oChilNode)
            '                oChilNode = Nothing 'Change made to solve memory Leak and word crash issue
            '            End If
            '        Next
            '    End If
            'End If

            'For Each oNode As TreeNode In trvICD9.Nodes '' to check 
            '    If oNode.Nodes.Count > 0 Then
            '        For Each oChildNode As TreeNode In oNode.Nodes
            '            _ICD91 = oChildNode.Text.Replace(":", "-")
            '        Next
            '    Else
            '        _ICD91 = oNode.Text
            '    End If
            'Next
            Return Nothing
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    'Private Function FillICD9InDetailFromImmunization(ByVal ICD9 As String, ByVal ConceptID As String, ByVal strAllergy As String) As Boolean  ''8020 snomed changes
    '    ''Dim dt As DataTable ''slr new not needed 
    '    Dim oChilNode As TreeNode
    '    Dim _ICD9Code As String = ""
    '    Dim _ICD9DEscription As String = ""

    '    Try
    '        trvICD9.Nodes.Clear()
    '        trvICD9.Nodes.Add(strAllergy)
    '        trvICD9.ImageIndex = 4
    '        trvICD9.SelectedImageIndex = 4


    '        If ICD9 <> "" Then
    '            oChilNode = New TreeNode()
    '            oChilNode.Text = ICD9
    '            oChilNode.Tag = ConceptID
    '            oChilNode.ImageIndex = 0
    '            oChilNode.SelectedImageIndex = 0
    '            oChilNode.Collapse()
    '            trvICD9.Nodes(0).Nodes.Add(oChilNode)
    '            oChilNode = Nothing 'Change made to solve memory Leak and word crash issue
    '        End If
    '        Return Nothing
    '    Catch ex As Exception
    '        Return Nothing
    '    End Try
    'End Function

    'Private Sub trvSubType_NodeMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles trvSubType.NodeMouseDoubleClick
    '    Dim mynode As gloSnoMed.myTreeNode = CType(e.Node, gloSnoMed.myTreeNode)
    '    oSnoMed.Fill_Snomed_ISA_Definition(e.Node.Tag, trvDefination)
    '    oSnoMed.Fill_ICD9(e.Node.Tag, e.Node.Text, trvICD9)
    '    FillHistory(mynode, False) ''bug 6616''when we Add a allergic drug send false value because we need to show DI during this event
    '    If trvDefination.Nodes.Count > 0 Then
    '        trvDefination.SelectedNode = trvDefination.Nodes(0)
    '        trvDefination.Focus()
    '    End If
    'End Sub

    Private Sub dgCustomGrid_SearchChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgCustomGrid.SearchChanged
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim dvPatient As DataView  ''slr new not needed 
            dvPatient = CType(dgCustomGrid.C1Task.DataSource(), DataView) '' (CType(dt.DefaultView, DataView))
            If IsNothing(dvPatient) Then
                Me.Cursor = Cursors.Default
                Exit Sub
            End If

            Dim strPatientSearchDetails As String
            If Trim(dgCustomGrid.txtsearch.Text) <> "" Then
                strPatientSearchDetails = Replace(dgCustomGrid.txtsearch.Text, "'", "''")
                strPatientSearchDetails = Replace(strPatientSearchDetails, "[", "") & ""
                strPatientSearchDetails = mdlGeneral.ReplaceSpecialCharacters(strPatientSearchDetails)
            Else
                strPatientSearchDetails = ""
            End If
            If strbuttonStatus = "ICD9" Or strbuttonStatus = "CPT" Then
                dvPatient.RowFilter = "[" & dvPatient.Table.Columns(0).ColumnName & "] Like '%" & strPatientSearchDetails & "%' OR [" & dvPatient.Table.Columns(1).ColumnName & "] Like '%" & strPatientSearchDetails & "%' "
            ElseIf strbuttonStatus = "Initial Exam" Then
                dvPatient.RowFilter = "[" & dvPatient.Table.Columns(0).ColumnName & "] Like '%" & strPatientSearchDetails & "%' "
            Else
                dvPatient.RowFilter = "[" & dvPatient.Table.Columns(1).ColumnName & "] Like '%" & strPatientSearchDetails & "%' "
            End If
            dgCustomGrid.Enabled = False
            dgCustomGrid.datasource(dvPatient)
            dgCustomGrid.Enabled = True
            Me.Cursor = Cursors.Default
            If strbuttonStatus = "ICD9" Or strbuttonStatus = "CPT" Then
            Else
                Dim _TotalWidth As Single = dgCustomGrid.C1Task.Width - 5
                dgCustomGrid.C1Task.Cols(Col_Check).Width = _TotalWidth * 0.1
                dgCustomGrid.C1Task.Cols(Col_Name).Width = _TotalWidth * 0.85
            End If

            dgCustomGrid.txtsearch.Focus()
        Catch objErr As Exception
            Me.Cursor = Cursors.Default
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.Select, objErr.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub btnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear.Click
        txtSMSearch.Text = ""
    End Sub

    Private Sub frmHistory_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try
            Me.Activate()
            SetNKHistoryVisibility()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Enum HistoryEnum
        sHistoryCategory
        Hidden
        sHistoryItem
        sComments
        sReaction
        Button
        SmokingStatus
        SmokeButton
        sActive
        nVisitID
        dtVisitDate
        nDrugID
        MedicalCondition_Id
        sDrugName
        sDosage
        sNDCCode
        mpid
        OnsetDate
        DateResolved
        DOE_Allergy
        sConceptID
        sDescriptionID
        sSnoMedID
        sDescription
        sICD9

        CPT

        sRxNormID
        RowState
        nHistoryID
        nRowOrder
        sHistoryType
        RowID

        '29-Mar-13 Aniket: Addition of source column on the History screen
        sHistorySource
        nICDRevision    ''added for ICD10 implementation
        sReasonConceptID
        sReasonConceptDesc
        sAllergyClsId ''added for 2015 certification
        ResolvedEndDate ''added for 2015 certification
    End Enum

    Private Sub frmHistory_MaximizedBoundsChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.MaximizedBoundsChanged

    End Sub
    ''' <summary>
    ''' Property writen for Patient Context
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property GetCurrentPatientID() As Int64 Implements mdlGeneral.IPatientContext.GetCurrentPatientID
        Get
            Return m_PatientID    'Curent patient variable(Local variable) for this module 
        End Get
    End Property
    Private Sub On_frmRxMeds_Closed(ByVal sender As Object, ByVal e As FormClosedEventArgs)
        frmRxMeds_Closed()
        Dim frmRxMeds As frmPrescription = Nothing

        Try
            frmRxMeds = DirectCast(sender, frmPrescription)
        Catch ex As Exception

        End Try
        Try
            If (IsNothing(frmRxMeds) = False) Then
                RemoveHandler frmRxMeds.FormClosed, AddressOf On_frmRxMeds_Closed
            End If
            If (IsNothing(frmRxMeds) = False) Then
                frmRxMeds.Close()
            End If
            If (IsNothing(frmRxMeds) = False) Then
                frmRxMeds.Dispose()
                frmRxMeds = Nothing
            End If

        Catch ex As Exception

        End Try

    End Sub
    Public Sub frmRxMeds_Closed()
        If Me.myCallerSynopsis IsNot Nothing Then
            Dim ofrm As frmPatientSynopsis
            ofrm = CType(Me.myCallerSynopsis, Form)
            ofrm.frmPrecription_Closed(Nothing, Nothing)
        End If
    End Sub

    Public Function FormLevelLock() As Boolean
        Try
            Dim dtLock As DataTable ''slr new not needed
            dtLock = Scan_n_Lock_FormLevel(m_PatientID, m_VisitID, 0, "History")
            FormLevelLockID = Convert.ToInt64(dtLock.Rows(0)("FormLevelID"))
            If dtLock.Rows.Count > 0 Then
                If Convert.ToString(dtLock.Rows(0)("IsOpen")) = "1" Then ''This means form is allready open 
                    'New One
                    If MessageBox.Show("History for this patient is currently being modified by " & Convert.ToString(dtLock.Rows(0)("UserName")) & " on " & Convert.ToString(dtLock.Rows(0)("MachineName")) & ". Please close out of History from the other session in order to make modifications from this computer." & vbNewLine & vbNewLine & "Would you like to launch History in view only mode?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                        _blnRecordLock = True
                    Else
                        blncancel = False
                        If Not IsNothing(dtLock) Then ''slr free dtlock
                            dtLock.Dispose()
                            dtLock = Nothing
                        End If
                        FormLevelLock = Nothing
                        Exit Function
                    End If
                    Call Set_RecordLock(_blnRecordLock)
                Else
                End If
            End If
            If Not IsNothing(dtLock) Then ''slr free dtlock
                dtLock.Dispose()
                dtLock = Nothing
            End If
            Return True
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function
    ''Added  by MAYURI:20120913- Added To show Checkbox for onset date datetimepicker control
    Private Sub C1HistoryDetails_SetupEditor(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles C1HistoryDetails.SetupEditor
        Dim dtp As DateTimePicker = TryCast(C1HistoryDetails.Editor, DateTimePicker)
        If IsNothing(dtp) = False Then
            dtp.ShowCheckBox = True

        End If
    End Sub

    Private Sub C1HistoryDetails_ValidateEdit(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.ValidateEditEventArgs) Handles C1HistoryDetails.ValidateEdit
        If IsNothing(C1HistoryDetails.Editor) = False Then


            Dim dtp As DateTimePicker = TryCast(C1HistoryDetails.Editor, DateTimePicker)
            If IsNothing(dtp) = False Then


                If dtp.Checked = True Then
                    C1HistoryDetails.Editor.Text = C1HistoryDetails.Editor.Text
                Else
                    C1HistoryDetails.Editor.Text = Nothing
                End If
            End If
        End If
        If (IsCloseClickFlagForCommentValidation) Then
            If IsNothing(C1HistoryDetails.Editor) = False Then
                If (C1HistoryDetails.Editor.Text.Length > MAX_COMMENT_LENGHT) Then
                    e.Cancel = True
                End If
            End If
            Exit Sub
        End If
        If IsNothing(C1HistoryDetails.Editor) = True Then
            Exit Sub
        End If
        If (C1HistoryDetails.Editor.Text.Length > MAX_COMMENT_LENGHT) Then
            MessageBox.Show("Comment should be less than equal to 760 characters.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            e.Cancel = True
        End If
    End Sub

    'Private Sub dgCustomGrid_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgCustomGrid.MouseDoubleClick
    '    Dim r As Integer = C1HistoryDetails.RowSel

    '    Dim Strdata As String = ""

    '    If strbuttonStatus = "ICD9" Then

    '        If Strdata.Trim = "" Then
    '            If dgCustomGrid.C1Task.Row >= 0 Then


    '                Strdata = dgCustomGrid.GetItem(dgCustomGrid.C1Task.Row, 0).ToString & " : " & dgCustomGrid.GetItem(dgCustomGrid.C1Task.Row, 1).ToString
    '            End If
    '        End If

    '    ElseIf strbuttonStatus = "CPT" Then

    '        If Strdata.Trim = "" Then
    '            If dgCustomGrid.C1Task.Row >= 0 Then


    '                Strdata = dgCustomGrid.GetItem(dgCustomGrid.C1Task.Row, 0).ToString & " : " & dgCustomGrid.GetItem(dgCustomGrid.C1Task.Row, 1).ToString
    '            End If
    '        End If

    '    End If

    '    If r >= 0 Then

    '        If strbuttonStatus = "ICD9" Then

    '            C1HistoryDetails.SetData(r, Col_HsICD9, Strdata)

    '        ElseIf strbuttonStatus = "CPT" Then

    '            C1HistoryDetails.SetData(r, col_HCPT, Strdata)
    '        End If
    '    End If

    '    C1HistoryDetails.Select(r, Col_HSmokingStatus)
    '    PnlCustomTask.Visible = False
    'End Sub




    Private Sub oCPTListControl_ItemSelectedClick(ByVal sender As Object, ByVal e As EventArgs)
        Dim Strdata As String = ""
        Try
            If oCPTListControl.SelectedItems.Count > 0 Then
                For i As Int16 = 0 To oCPTListControl.SelectedItems.Count - 1
                    lnlLbllCPTCode.Text = oCPTListControl.SelectedItems(i).Code
                    Strdata = oCPTListControl.SelectedItems(i).Code & " : " & oCPTListControl.SelectedItems(i).Description

                    C1HistoryDetails.SetData(C1HistoryDetails.Row, col_HCPT, Strdata)

                Next
                ofrmCPTList.Close()
            Else
                lnlLbllCPTCode.Text = ""
                C1HistoryDetails.SetData(C1HistoryDetails.Row, col_HCPT, "")
                ofrmCPTList.Close()
            End If


        Catch ex As Exception
            MessageBox.Show("Error on User List Control." & ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub
    Private Sub oCPTListControl_ItemClosedClick(ByVal sender As Object, ByVal e As EventArgs)
        ofrmCPTList.Close()
        If IsNothing(ofrmCPTList) = False Then
            ofrmCPTList = Nothing
        End If
    End Sub



    Private Sub oDiagnosisListControl_ItemSelectedClick(ByVal sender As Object, ByVal e As EventArgs)
        Dim Strdata As String = ""

        ''   Dim objPatientHistory As New clsPatientHistory()  slr not used
        Try
            If oDiagnosisListControl.SelectedItems.Count > 0 Then
                For i As Int16 = 0 To oDiagnosisListControl.SelectedItems.Count - 1
                    linkLblICD9code.Text = oDiagnosisListControl.SelectedItems(i).Code
                    Strdata = oDiagnosisListControl.SelectedItems(i).Code & " : " & oDiagnosisListControl.SelectedItems(i).Description

                    C1HistoryDetails.SetData(C1HistoryDetails.Row, Col_HsICD9, Strdata)

                Next
                C1HistoryDetails.SetData(C1HistoryDetails.Row, "nICDRevision", oDiagnosisListControl.IsICD9_10)    ''added for ICD10 implementation
                ofrmDiagnosisList.Close()
            Else
                linkLblICD9code.Text = ""
                C1HistoryDetails.SetData(C1HistoryDetails.Row, Col_HsICD9, "")
                C1HistoryDetails.SetData(C1HistoryDetails.Row, "nICDRevision", 0)   ''added for ICD10 implementation
                ofrmDiagnosisList.Close()
            End If
            ConceptDesc = Convert.ToString(C1HistoryDetails.GetData(C1HistoryDetails.Row, Col_HsHistoryItem))
            Dim ICD9Codetbl As New DataSet
            ' If Strdata <> "" Then   commented 8020 snomed changes
            'FillICD9(Strdata, ConceptDesc)
            'If trvICD9.Nodes.Count > 0 Then
            'trvICD9.SelectedNode = trvICD9.Nodes(0)
            'trvICD9.Focus()
            'End If
            'trvICD9.ExpandAll()
            'Else
            '   trvICD9.Nodes.Clear()
            ' End If

        Catch ex As Exception
            MessageBox.Show("Error on User List Control." & ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub
    Private Sub oDiagnosisListControl_ItemClosedClick(ByVal sender As Object, ByVal e As EventArgs)
        ofrmDiagnosisList.Close()
        If IsNothing(ofrmDiagnosisList) = False Then
            ofrmDiagnosisList = Nothing
        End If
    End Sub



    Private Sub btnConceptID_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConceptID.Click
        gstrSMDBConnstr = GetHybridConnectionString(gstrSMDBServerName, gstrSMDBDatabaseName, gblnSMDBAuthen, gstrSMDBUserID, gstrSMDBPassWord)
        Dim frm As New gloSnoMed.FrmSelectProblem("History", gstrSMDBConnstr, GetConnectionString())
        '  Dim Hs_StrSnoMedID As String
        Dim str As String = ""
        Dim objPatientHistory As New clsPatientHistory()
        Dim ICD9Codetbl As DataSet 'SLR: new is not needed
        Try

            ''Code changed by Mayuri:20130125-To show Conceptid in search window in modify mode else shoe conceptDescription
            'If lblconcptid.Text.Trim <> "" Then
            '    frm.txtSMSearch.Text = lblconcptid.Text.Trim
            'Else
            '    frm.txtSMSearch.Text = ConceptDesc
            'End If
            frm.strConceptDesc = ConceptDesc
            frm.strDescriptionID = lbldescid.Text.Trim
            Dim splconceptid As String() = lblconcptid.Text.Trim().Split("-")
            If (splconceptid.Length > 1) Then
                frm.strConceptID = splconceptid(0)
                frm.txtSMSearch.Text = splconceptid(0)
            Else
                frm.strConceptID = lblconcptid.Text.Trim
                frm.txtSMSearch.Text = lblconcptid.Text.Trim
            End If
            If lblconcptid.Text.Trim = "" Then
                frm.txtSMSearch.Text = ConceptDesc
            End If

            frm.ShowDialog(IIf(IsNothing(frm.Parent), Me, frm.Parent))


            If frm._DialogResult Then

                If IsNothing(frm.strConceptID) = False Then


                    If Convert.ToString(frm.strConceptID).Trim = "0" Then

                        lblconcptid.Text = ""
                        C1HistoryDetails.SetData(C1HistoryDetails.Row, Col_HsConceptID, lblconcptid.Text)

                    Else
                        lblconcptid.Text = Convert.ToString(frm.strConceptID) '.ToString()
                        C1HistoryDetails.SetData(C1HistoryDetails.Row, Col_HsConceptID, frm.strConceptID)
                    End If
                Else
                    lblconcptid.Text = frm.strConceptID
                    C1HistoryDetails.SetData(C1HistoryDetails.Row, Col_HsConceptID, frm.strConceptID)
                End If

                lbldescid.Text = frm.strDescriptionID
                lblSnomedID.Text = frm.StrSnoMedID
                Defination = frm.strDefination
                If lblconcptid.Text.Trim() <> "" Then    ''changes done for 8020 snomed prd
                    If frm.strSelectedDescription.Trim() <> "" Then
                        lblconcptid.Text = lblconcptid.Text + "-" + frm.strSelectedDescription
                    End If
                End If


                ' lblsnodesc.Text = frm.strSelectedDescription   ''8020 snomed changes
                ' ConceptDesc = Convert.ToString(C1HistoryDetails.GetData(_rowsel, Col_HsHistoryItem))
                'linkLblICD9code.Text = ICD9 
                If frm.strICD9 <> "" Then
                    ICD9 = frm.strICD9
                ElseIf frm.strICD10 <> "" Then
                    ICD9 = frm.strICD10

                End If
                '  ICD9 = frm.strICD9
                C1HistoryDetails.SetData(C1HistoryDetails.Row, "nICDRevision", gloGlobal.gloICD.CodeRevision.ICD9)
                If ICD9 <> "" Then
                    C1HistoryDetails.SetData(C1HistoryDetails.Row, Col_HsICD9, frm.strICD9)
                    If ICD9 <> "" Then
                        ICD9Codetbl = objPatientHistory.Fill_ICD9Code(ICD9, "")
                        If IsNothing(ICD9Codetbl) = False Then
                            If ICD9Codetbl.Tables("ICD9").Rows.Count > 0 Then
                                linkLblICD9code.Text = ICD9Codetbl.Tables("ICD9").Rows(0)(0)
                            End If

                        End If
                    End If

                    ' FillICD9(ICD9, ConceptDesc) commented 8020 snomed changes
                    'Else
                    '   trvICD9.Nodes.Clear()
                End If

                ConceptDesc = Convert.ToString(C1HistoryDetails.GetData(C1HistoryDetails.Row, Col_HsHistoryItem))
                If frm.strRxNormCode <> "" Then

                    lblRxNorm.Text = frm.strRxNormCode
                End If

                lblNDCid.Text = frm.strNDCCode

                C1HistoryDetails.SetData(C1HistoryDetails.Row, Col_HsDescriptionID, lbldescid.Text)
                C1HistoryDetails.SetData(C1HistoryDetails.Row, Col_HsDescription, frm.strSelectedDescription) ''8020 snomed changes
                C1HistoryDetails.SetData(C1HistoryDetails.Row, Col_HsSnomedID, lblSnomedID.Text)
                C1HistoryDetails.SetData(C1HistoryDetails.Row, Col_HsNDCCode, lblNDCid.Text)
                C1HistoryDetails.SetData(C1HistoryDetails.Row, Col_HsRxNormID, lblRxNorm.Text)
                ' If lblconcptid.Text <> "" Then  ''commented for 8020 snomed changes
                'If Defination <> "" Then  
                'FillDefinitionInDetail(Defination)
                'FillDefination(Defination)
                ' Else
                '    trvDefination.Nodes.Clear()
                'End If
                ''Added Rahul on 20100918 ICD9 <> ""


                ''End
                '''''''oSnoMed.Fill_Snomed_ISA_Definition(lblconcptid.Text, trvDefination)
                'If trvDefination.Nodes.Count > 0 Then  ''commented for 8020 snomed changes
                '    trvDefination.SelectedNode = trvDefination.Nodes(0)
                '    '''''''' trvDefination.TopNode.EnsureVisible()
                '    trvDefination.Focus()
                'End If
                ''Code Start-Added by kanchan on 20100526 for ICD9
                'If trvICD9.Nodes.Count > 0 Then
                '    trvICD9.SelectedNode = trvICD9.Nodes(0)
                '    trvICD9.Focus()
                'End If
                'trvICD9.ExpandAll()
                'Code End-Added by kanchan on 20100526 for ICD9
                'trvDefination.ExpandAll()

                ' Else

                '' ''If gblnIsFindingAdd = False Then
                '' ''    ''Commented for Fixed BugID 6560 on 20101123
                '' ''    '' For NonSnomed History item Defination & its ICD9 details will not be displayed 
                ' trvDefination.Nodes.Clear()
                ' trvICD9.Nodes.Clear()
                'End If
                'End If

            End If
            objPatientHistory = Nothing ''slr free  objPatientHistory
        Catch ex As Exception

            frm.Dispose()
        End Try
    End Sub

    Private Sub btn_ICD9Code_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_ICD9Code.Click
        Try
            ''slr free previous memory
            If IsNothing(ofrmDiagnosisList) = False Then
                ofrmDiagnosisList.Dispose()
                ofrmDiagnosisList = Nothing

            End If
            ofrmDiagnosisList = New frmViewListControl
            ' Dim arrCPTTextSplit As String()
            oDiagnosisListControl = New gloListControl.gloListControl(GetConnectionString(), gloListControl.gloListControlType.Diagnosis, False, Me.Width)
            oDiagnosisListControl.ControlHeader = "Diagnosis"
            oDiagnosisListControl.gblnIcd10Transition = gblnIcd10Transition ''If true then ICD10 gets selected 
            AddHandler oDiagnosisListControl.ItemSelectedClick, AddressOf oDiagnosisListControl_ItemSelectedClick
            AddHandler oDiagnosisListControl.ItemClosedClick, AddressOf oDiagnosisListControl_ItemClosedClick
            ofrmDiagnosisList.Controls.Add(oDiagnosisListControl)
            oDiagnosisListControl.Dock = DockStyle.Fill
            oDiagnosisListControl.BringToFront()
            'code make all CPT Code Selected in ListControl  Which are present in cmbCPT
            'For i As Integer = 0 To cmbDiagnosis.Items.Count - 1
            '    cmbDiagnosis.SelectedIndex = i
            '    oDiagnosisListControl.SelectedItems.Add(0, txtICD9Code.Text, "")
            'Next
            oDiagnosisListControl.ShowHeaderPanel(False)
            oDiagnosisListControl.OpenControl()
            ofrmDiagnosisList.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
            ofrmDiagnosisList.Text = "Diagnosis"
            ofrmDiagnosisList.ShowDialog(IIf(IsNothing(ofrmDiagnosisList.Parent), Me, ofrmDiagnosisList.Parent))

            If IsNothing(ofrmDiagnosisList) = False Then
                RemoveHandler oDiagnosisListControl.ItemSelectedClick, AddressOf oDiagnosisListControl_ItemSelectedClick
                RemoveHandler oDiagnosisListControl.ItemClosedClick, AddressOf oDiagnosisListControl_ItemClosedClick
                ofrmDiagnosisList.Controls.Remove(oDiagnosisListControl)
                oDiagnosisListControl.Dispose()
                oDiagnosisListControl = Nothing
                ofrmDiagnosisList.Dispose()
                ofrmDiagnosisList = Nothing
                'End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btn_CPTCode_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_CPTCode.Click
        Try

            ofrmCPTList = New frmViewListControl
            '  Dim arrCPTTextSplit As String()
            oCPTListControl = New gloListControl.gloListControl(GetConnectionString(), gloListControl.gloListControlType.CPT, False, Me.Width)
            oCPTListControl.ControlHeader = "CPT"
            AddHandler oCPTListControl.ItemSelectedClick, AddressOf oCPTListControl_ItemSelectedClick
            AddHandler oCPTListControl.ItemClosedClick, AddressOf oCPTListControl_ItemClosedClick
            ofrmCPTList.Controls.Add(oCPTListControl)
            oCPTListControl.Dock = DockStyle.Fill
            oCPTListControl.BringToFront()

            oCPTListControl.ShowHeaderPanel(False)
            oCPTListControl.OpenControl()
            ofrmCPTList.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
            ofrmCPTList.Text = "CPT"
            ofrmCPTList.ShowDialog(IIf(IsNothing(ofrmCPTList.Parent), Me, ofrmCPTList.Parent))

            If IsNothing(ofrmCPTList) = False Then
                RemoveHandler oCPTListControl.ItemSelectedClick, AddressOf oCPTListControl_ItemSelectedClick
                RemoveHandler oCPTListControl.ItemClosedClick, AddressOf oCPTListControl_ItemClosedClick
                ofrmCPTList.Controls.Remove(oCPTListControl)
                oCPTListControl.Dispose()
                oCPTListControl = Nothing
                ofrmCPTList.Dispose()
                ofrmCPTList = Nothing
                'End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub btnUp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUp.Click




        Dim _categorytype As String = ""
        Dim _ID1 As Int64
        Dim _ID As Int64
        Dim _selrow As Integer = 0
        Dim _NewRow As Integer = 0
        Dim stronsetActiveStatus As String = ""
        '   Dim _arrOnsetActive() As String
        Dim IsOnsetDate As Boolean = False

        Dim IsActive As Boolean = False
        Try
            _selrow = C1HistoryDetails.Row
            '_NewRow = C1HistoryDetails.Row - 2
            _NewRow = _selrow - 1
            For j As Integer = C1HistoryDetails.Row To 1 Step -1
                If _NewRow > 1 Then
                    If C1HistoryDetails.Rows(_NewRow).IsVisible Then
                        Exit For
                    Else
                        _NewRow = _NewRow - 1
                    End If
                End If
            Next

            _selrow = _selrow - 1
            _NewRow = _NewRow - 1
            If _NewRow > 0 Then
            Else
                Exit Sub
            End If
            _ID = dsHistory.Tables("History").Rows(_selrow)("nRowOrder")
            _ID1 = dsHistory.Tables("History").Rows(_NewRow)("nRowOrder")
            dsHistory.Tables("History").Rows(_NewRow)("nRowOrder") = _ID
            dsHistory.Tables("History").Rows(_selrow)("nRowOrder") = _ID1




            If C1HistoryDetails.Row > 0 Then

                If dsHistory.Tables("History").Rows(_NewRow)(Col_HCategory) = "" Then

                    Dim moveow As DataRow = dsHistory.Tables("History").Rows(_NewRow)
                    Dim selectedRow As DataRow = dsHistory.Tables("History").Rows(_selrow)

                    Dim OldRow As DataRow = dsHistory.Tables("History").NewRow()
                    OldRow.ItemArray = moveow.ItemArray
                    dsHistory.Tables("History").Rows.InsertAt(OldRow, _selrow)

                    Dim newRow As DataRow = dsHistory.Tables("History").NewRow()
                    newRow.ItemArray = selectedRow.ItemArray
                    dsHistory.Tables("History").Rows.InsertAt(newRow, _NewRow)


                    dsHistory.Tables("History").Rows.Remove(moveow)
                    dsHistory.Tables("History").Rows.Remove(selectedRow)
                    _selrow = _selrow + 1
                    For f As Integer = 0 To 1
                        Dim _HistoryType As String = ""
                        _HistoryType = Convert.ToString(C1HistoryDetails.GetData(_selrow, "sHistoryType")).Trim

                        _categorytype = Convert.ToString(C1HistoryDetails.GetData(_selrow, Col_HHidden)).Trim
                        GetCategorynStatus(_HistoryType, _categorytype, IsActive, IsOnsetDate)
                        '_categorytype = Convert.ToString(C1HistoryDetails.GetData(_selrow, Col_HHidden)).Trim
                        'If _categorytype = "OB Medical History" Or _categorytype = "OB Genetic History" Or _categorytype = "OB Infection History" Or _categorytype = "OB Initial Physical Examination" Then
                        'Else
                        '    If Convert.ToString(C1HistoryDetails.GetData(_selrow, "sHistoryType")).Trim = "" Then

                        '        _categorytype = Convert.ToString(C1HistoryDetails.GetData(_selrow, Col_HHidden)).Trim
                        '        _categorytype = getHistoryTypefromcategorymaster(_categorytype)


                        '    Else
                        '        _categorytype = Convert.ToString(C1HistoryDetails.GetData(_selrow, "sHistoryType")).Trim

                        '    End If
                        'End If

                        'If _categorytype <> "" Then
                        '    If _categorytype.Length > 2 Then
                        '        If _categorytype = "OB Medical History" Or _categorytype = "OB Genetic History" Or _categorytype = "OB Infection History" Or _categorytype = "OB Initial Physical Examination" Then
                        '            IsActive = True
                        '            IsOnsetDate = False
                        '        Else

                        '            stronsetActiveStatus = CheckHistoryTypeinStandardTable(_categorytype)
                        '            _arrOnsetActive = stronsetActiveStatus.Split(",")
                        '            If IsNothing(_arrOnsetActive) = False Then
                        '                If _arrOnsetActive.Length >= 1 Then
                        '                    IsOnsetDate = _arrOnsetActive.GetValue(0)
                        '                    IsActive = _arrOnsetActive.GetValue(1)
                        '                End If
                        '            End If
                        '        End If

                        '    End If
                        'End If
                        If IsOnsetDate = True Then


                            Dim cs As CellStyle '= C1HistoryDetails.Styles.Add("DateTime")
                            Try
                                If (C1HistoryDetails.Styles.Contains("DateTime")) Then
                                    cs = C1HistoryDetails.Styles("DateTime")
                                Else
                                    cs = C1HistoryDetails.Styles.Add("DateTime")

                                End If
                            Catch ex As Exception
                                cs = C1HistoryDetails.Styles.Add("DateTime")

                            End Try
                            cs.DataType = GetType(DateTime)
                            If (gblnEnableCQMCypressTesting) Then
                                cs.Format = "MM/dd/yyyy hh:mm tt"
                            Else
                                cs.Format = "MM/dd/yyyy"
                            End If
                            cs.TextAlign = TextAlignEnum.CenterCenter
                            cs.ImageAlign = ImageAlignEnum.CenterCenter
                            C1HistoryDetails.SetCellStyle(_selrow, col_HOnsetDate, cs)
                            'End If
                        End If

                        'If InStr(C1HistoryDetails.GetData(_selrow, Col_HHidden), "Allerg", CompareMethod.Text) = 1 Then
                        If IsActive = True And _categorytype = "All" Then

                            Dim cStyle As C1.Win.C1FlexGrid.CellStyle = Nothing
                            Dim rgReaction As C1.Win.C1FlexGrid.CellRange = C1HistoryDetails.GetCellRange(_selrow, Col_HButton, _selrow, Col_HButton)
                            Dim strReaction As String
                            Dim strActive As String
                            Dim rgActive As C1.Win.C1FlexGrid.CellRange = C1HistoryDetails.GetCellRange(_selrow, Col_HsActive, _selrow, Col_HsActive)
                            Dim arr() As String 'Srting Array
                            arr = Split(Convert.ToString(C1HistoryDetails.Rows(_selrow)(Col_HsReaction)), "|")
                            If arr.Length = 2 Then
                                strReaction = arr.GetValue(0)
                                strActive = Convert.ToString(C1HistoryDetails.Rows(_selrow)(Col_HsActive))
                            Else
                                strReaction = Convert.ToString(C1HistoryDetails.Rows(_selrow)(Col_HsReaction))
                                strActive = Convert.ToString(C1HistoryDetails.Rows(_selrow)(Col_HsActive))
                            End If

                            Dim arrReaction As String()
                            arrReaction = strReaction.Split(vbNewLine)
                            rgReaction.Style = cStyle
                            rgActive.StyleNew.DataType = GetType(Boolean)
                            rgActive.StyleNew.TextAlign = TextAlignEnum.CenterCenter
                            rgActive.StyleNew.ImageAlign = ImageAlignEnum.CenterCenter
                            rgActive.Checkbox = CheckEnum.Checked
                            If strActive = "True" Then
                                C1HistoryDetails.SetCellCheck(_selrow, Col_HsActive, CheckEnum.Checked)
                            Else
                                C1HistoryDetails.SetCellCheck(_selrow, Col_HsActive, CheckEnum.Unchecked)
                            End If
                            '  .Rows(_Row)(Col_HsActive) = True
                            C1HistoryDetails.SetCellImage(_selrow, Col_HButton, imgTreeVIew.Images(5)) '' Chetan Added
                            C1HistoryDetails.Rows(_selrow).Height = C1HistoryDetails.Rows.DefaultSize * arrReaction.Length - 1
                        ElseIf _categorytype = "OB Initial Physical Examination" Then
                            Dim arr() As String 'Srting Array
                            Dim cStyle As C1.Win.C1FlexGrid.CellStyle = Nothing
                            Dim rgReaction As C1.Win.C1FlexGrid.CellRange = C1HistoryDetails.GetCellRange(_selrow, Col_HButton, _selrow, Col_HButton)
                            Dim strReaction As String
                            Dim strActive As String
                            arr = Split(Convert.ToString(C1HistoryDetails.Rows(_selrow)(Col_HsReaction)), "|")
                            If arr.Length = 2 Then
                                strReaction = arr.GetValue(0)
                                strActive = Convert.ToString(C1HistoryDetails.Rows(_selrow)(Col_HsActive))
                            Else
                                strReaction = Convert.ToString(C1HistoryDetails.Rows(_selrow)(Col_HsReaction))
                                strActive = Convert.ToString(C1HistoryDetails.Rows(_selrow)(Col_HsActive))
                            End If

                            Dim arrReaction As String()
                            arrReaction = strReaction.Split(vbNewLine)

                            C1HistoryDetails.SetCellImage(_selrow, Col_HButton, imgTreeVIew.Images(5))
                        ElseIf _categorytype = "Fam" Then

                            Dim cStyle As C1.Win.C1FlexGrid.CellStyle = Nothing
                            Dim rgReaction As C1.Win.C1FlexGrid.CellRange = C1HistoryDetails.GetCellRange(_selrow, Col_HButton, _selrow, Col_HButton)
                            Dim strReaction As String
                            Dim strActive As String
                            Dim rgActive As C1.Win.C1FlexGrid.CellRange = C1HistoryDetails.GetCellRange(_selrow, Col_HsActive, _selrow, Col_HsActive)
                            Dim arr() As String 'Srting Array
                            arr = Split(Convert.ToString(C1HistoryDetails.Rows(_selrow)(Col_HsReaction)), "|")
                            If arr.Length = 2 Then
                                strReaction = arr.GetValue(0)
                                strActive = Convert.ToString(C1HistoryDetails.Rows(_selrow)(Col_HsActive))
                            Else
                                strReaction = Convert.ToString(C1HistoryDetails.Rows(_selrow)(Col_HsReaction))
                                strActive = Convert.ToString(C1HistoryDetails.Rows(_selrow)(Col_HsActive))
                            End If

                            Dim arrReaction As String()
                            arrReaction = strReaction.Split(vbNewLine)
                            rgReaction.Style = cStyle

                            If IsActive Then
                                rgActive.StyleNew.DataType = GetType(Boolean)
                                rgActive.StyleNew.TextAlign = TextAlignEnum.CenterCenter
                                rgActive.StyleNew.ImageAlign = ImageAlignEnum.CenterCenter
                                rgActive.Checkbox = CheckEnum.Checked
                                If strActive = "True" Then
                                    C1HistoryDetails.SetCellCheck(_selrow, Col_HsActive, CheckEnum.Checked)
                                Else
                                    C1HistoryDetails.SetCellCheck(_selrow, Col_HsActive, CheckEnum.Unchecked)
                                End If
                                '  .Rows(_Row)(Col_HsActive) = True
                            End If

                            C1HistoryDetails.SetCellImage(_selrow, Col_HButton, imgTreeVIew.Images(5)) '' Chetan Added
                            C1HistoryDetails.Rows(_selrow).Height = C1HistoryDetails.Rows.DefaultSize * arrReaction.Length - 1

                            'Bug #61329: 00000599 : patient has multiple history items in different casing the system will throw an error.
                        ElseIf C1HistoryDetails.GetData(_selrow, Col_HHidden).ToString.ToUpper = "SMOKING STATUS" Then '' CODE FOR THE '' SMOKING ''
                            'Dim cStyle As C1.Win.C1FlexGrid.CellStyle

                            Dim strReaction As String
                            Dim strActive As String
                            Dim rgActive As C1.Win.C1FlexGrid.CellRange = C1HistoryDetails.GetCellRange(_selrow, Col_HsActive, _selrow, Col_HsActive)
                            Dim arr() As String 'Srting Array
                            arr = Split(Convert.ToString(C1HistoryDetails.Rows(_selrow)(Col_HsReaction)), "|")
                            If arr.Length = 2 Then
                                strReaction = arr.GetValue(0)
                                strActive = Convert.ToString(C1HistoryDetails.Rows(_selrow)(Col_HsActive))
                            Else
                                strReaction = Convert.ToString(C1HistoryDetails.Rows(_selrow)(Col_HsReaction))
                                strActive = Convert.ToString(C1HistoryDetails.Rows(_selrow)(Col_HsActive))
                            End If

                            Dim strReactions As String = ""
                            '  Dim objclsPatientHistory As New clsPatientHistory

                            Dim rgBrowseBtn As C1.Win.C1FlexGrid.CellRange = C1HistoryDetails.GetCellRange(_selrow, Col_HSmokeButton, _selrow, Col_HSmokeButton)
                            C1HistoryDetails.SetCellImage(_selrow, Col_HSmokeButton, imgTreeVIew.Images(5))
                            C1HistoryDetails.SetData(_selrow, Col_HSmokeButton, strReactions)
                            'objclsPatientHistory = Nothing
                        ElseIf IsActive = True Then
                            ' Dim cStyle As C1.Win.C1FlexGrid.CellStyle

                            Dim strReaction As String
                            Dim strActive As String
                            Dim rgActive As C1.Win.C1FlexGrid.CellRange = C1HistoryDetails.GetCellRange(_selrow, Col_HsActive, _selrow, Col_HsActive)
                            Dim arr() As String 'Srting Array
                            arr = Split(Convert.ToString(C1HistoryDetails.Rows(_selrow)(Col_HsReaction)), "|")
                            If arr.Length = 2 Then
                                strReaction = arr.GetValue(0)
                                strActive = Convert.ToString(C1HistoryDetails.Rows(_selrow)(Col_HsActive))
                            Else
                                strReaction = Convert.ToString(C1HistoryDetails.Rows(_selrow)(Col_HsReaction))
                                strActive = Convert.ToString(C1HistoryDetails.Rows(_selrow)(Col_HsActive))
                            End If


                            rgActive.StyleNew.DataType = GetType(Boolean)
                            rgActive.StyleNew.TextAlign = TextAlignEnum.CenterCenter
                            rgActive.StyleNew.ImageAlign = ImageAlignEnum.CenterCenter
                            rgActive.Checkbox = CheckEnum.Checked
                            If strActive = "True" Then
                                C1HistoryDetails.SetCellCheck(_selrow, Col_HsActive, CheckEnum.Checked)
                            Else
                                C1HistoryDetails.SetCellCheck(_selrow, Col_HsActive, CheckEnum.Unchecked)
                            End If

                        End If
                        _selrow = _NewRow + 1
                    Next
                    C1HistoryDetails.Row = _selrow
                End If

            End If

            For k As Integer = 0 To dsHistory.Tables("History").Rows.Count - 1
                If dsHistory.Tables("History").Rows(k).RowState <> DataRowState.Deleted Then
                    dsHistory.Tables("History").Rows(k)("RowState") = "Added"
                End If
            Next

            C1HistoryDetails_EnterCell(Nothing, Nothing)

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Remove, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            '  MessageBox.Show(ex.ToString, "Patient History", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnDown_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDown.Click

        Try
            Dim stronsetActiveStatus As String = ""
            '   Dim _arrOnsetActive() As String
            Dim IsOnsetDate As Boolean = False

            Dim IsActive As Boolean = False
            Dim _selrow As Integer = 0
            Dim _newRow As Integer = 0
            Dim _ID1 As Int64
            Dim _ID As Int64
            Dim _categorytype As String = ""
            ' For i As Integer = 0 To dsHistory.Tables("History").Rows.Count - 1
            '_selrow = C1HistoryDetails.Row - 1
            '_newRow = C1HistoryDetails.Row
            _selrow = C1HistoryDetails.Row
            _newRow = C1HistoryDetails.Row + 1
            Dim _strcategory As String = ""
            _strcategory = Convert.ToString(C1HistoryDetails.Rows(_selrow)(Col_HHidden))
            For j As Integer = C1HistoryDetails.Row To C1HistoryDetails.Rows.Count - 1
                If _strcategory = Convert.ToString(C1HistoryDetails.Rows(_newRow)(Col_HHidden)) Then
                    If _newRow > 1 Then
                        If C1HistoryDetails.Rows(_newRow).IsVisible Then
                            Exit For
                        Else
                            _newRow = _newRow + 1
                        End If
                    End If
                Else
                    Exit Sub
                End If
            Next

            _selrow = _selrow - 1
            _newRow = _newRow - 1
            If _newRow > 0 Then
            Else
                Exit Sub
            End If
            _ID = dsHistory.Tables("History").Rows(_selrow)("nRowOrder")
            _ID1 = dsHistory.Tables("History").Rows(_newRow)("nRowOrder")
            dsHistory.Tables("History").Rows(_newRow)("nRowOrder") = _ID
            dsHistory.Tables("History").Rows(_selrow)("nRowOrder") = _ID1

            If C1HistoryDetails.Row > 0 Then
                '   _selrow = C1HistoryDetails.Row - 1
                'Bug #61329: 00000599 : patient has multiple history items in different casing the system will throw an error.
                If dsHistory.Tables("History").Rows(_selrow)(Col_HHidden).ToString.ToUpper <> dsHistory.Tables("History").Rows(_selrow + 1)(Col_HCategory).ToString.ToUpper Then

                    Dim moverow As DataRow = dsHistory.Tables("History").Rows(_newRow)
                    Dim selectedRow As DataRow = dsHistory.Tables("History").Rows(_selrow)
                    Dim newRow As DataRow = dsHistory.Tables("History").NewRow()
                    newRow.ItemArray = selectedRow.ItemArray
                    Dim OldRow As DataRow = dsHistory.Tables("History").NewRow()
                    OldRow.ItemArray = moverow.ItemArray


                    dsHistory.Tables("History").Rows.InsertAt(newRow, _newRow)
                    dsHistory.Tables("History").Rows.InsertAt(OldRow, _selrow)
                    dsHistory.Tables("History").Rows.Remove(selectedRow)
                    dsHistory.Tables("History").Rows.Remove(moverow)
                    _selrow = _selrow + 1
                    For f As Integer = 0 To 1
                        Dim _HistoryType As String = ""
                        _HistoryType = Convert.ToString(C1HistoryDetails.GetData(_selrow, "sHistoryType")).Trim

                        _categorytype = Convert.ToString(C1HistoryDetails.GetData(_selrow, Col_HHidden)).Trim
                        GetCategorynStatus(_HistoryType, _categorytype, IsActive, IsOnsetDate)
                        '_categorytype = Convert.ToString(C1HistoryDetails.GetData(_selrow, Col_HHidden)).Trim
                        'If _categorytype = "OB Medical History" Or _categorytype = "OB Genetic History" Or _categorytype = "OB Infection History" Or _categorytype = "OB Initial Physical Examination" Then
                        'Else
                        '    If Convert.ToString(C1HistoryDetails.GetData(_selrow, "sHistoryType")).Trim = "" Then


                        '        _categorytype = Convert.ToString(C1HistoryDetails.GetData(_selrow, Col_HHidden)).Trim
                        '        _categorytype = getHistoryTypefromcategorymaster(_categorytype)
                        '    Else
                        '        _categorytype = Convert.ToString(C1HistoryDetails.GetData(_selrow, "sHistoryType")).Trim

                        '    End If
                        'End If

                        'If _categorytype <> "" Then
                        '    If _categorytype.Length > 2 Then
                        '        If _categorytype = "OB Medical History" Or _categorytype = "OB Genetic History" Or _categorytype = "OB Infection History" Or _categorytype = "OB Initial Physical Examination" Then
                        '            IsActive = True
                        '            IsOnsetDate = False
                        '        Else
                        '            stronsetActiveStatus = CheckHistoryTypeinStandardTable(_categorytype)
                        '            _arrOnsetActive = stronsetActiveStatus.Split(",")
                        '            If IsNothing(_arrOnsetActive) = False Then
                        '                If _arrOnsetActive.Length >= 1 Then
                        '                    IsOnsetDate = _arrOnsetActive.GetValue(0)
                        '                    IsActive = _arrOnsetActive.GetValue(1)
                        '                End If
                        '            End If
                        '        End If
                        '    End If
                        'End If
                        If IsOnsetDate = True Then
                            Dim cs As CellStyle '= C1HistoryDetails.Styles.Add("DateTime")
                            Try
                                If (C1HistoryDetails.Styles.Contains("DateTime")) Then
                                    cs = C1HistoryDetails.Styles("DateTime")
                                Else
                                    cs = C1HistoryDetails.Styles.Add("DateTime")

                                End If
                            Catch ex As Exception
                                cs = C1HistoryDetails.Styles.Add("DateTime")

                            End Try
                            cs.DataType = GetType(DateTime)
                            If (gblnEnableCQMCypressTesting) Then
                                cs.Format = "MM/dd/yyyy hh:mm tt"
                            Else

                                cs.Format = "MM/dd/yyyy"
                            End If
                            cs.TextAlign = TextAlignEnum.CenterCenter
                            cs.ImageAlign = ImageAlignEnum.CenterCenter
                            C1HistoryDetails.SetCellStyle(_selrow, col_HOnsetDate, cs)
                        End If

                        'End If
                        '  If InStr(C1HistoryDetails.GetData(C1HistoryDetails.Row + 1, Col_HHidden), "Allerg", CompareMethod.Text) = 1 Then
                        If IsActive = True And _categorytype = "All" Then
                            '' Category is Allergy Then Only we have to show Reactions & Active/Inactive CheckBOX                           
                            Dim cStyle As C1.Win.C1FlexGrid.CellStyle = Nothing
                            Dim rgReaction As C1.Win.C1FlexGrid.CellRange = C1HistoryDetails.GetCellRange(_selrow, Col_HButton, _selrow, Col_HButton)
                            Dim strReaction As String
                            Dim strActive As String
                            Dim rgActive As C1.Win.C1FlexGrid.CellRange = C1HistoryDetails.GetCellRange(_selrow, Col_HsActive, _selrow, Col_HsActive)
                            Dim arr() As String 'Srting Array
                            arr = Split(Convert.ToString(C1HistoryDetails.Rows(_selrow)(Col_HsReaction)), "|")
                            If arr.Length = 2 Then
                                strReaction = arr.GetValue(0)
                                strActive = Convert.ToString(C1HistoryDetails.Rows(_selrow)(Col_HsActive))
                            Else
                                strReaction = Convert.ToString(C1HistoryDetails.Rows(_selrow)(Col_HsReaction))
                                strActive = Convert.ToString(C1HistoryDetails.Rows(_selrow)(Col_HsActive))
                            End If

                            Dim arrReaction As String()
                            arrReaction = strReaction.Split(vbNewLine)
                            rgReaction.Style = cStyle
                            rgActive.StyleNew.DataType = GetType(Boolean)
                            rgActive.StyleNew.TextAlign = TextAlignEnum.CenterCenter
                            rgActive.StyleNew.ImageAlign = ImageAlignEnum.CenterCenter
                            rgActive.Checkbox = CheckEnum.Checked
                            If strActive = "True" Then
                                C1HistoryDetails.SetCellCheck(_selrow, Col_HsActive, CheckEnum.Checked)
                            Else
                                C1HistoryDetails.SetCellCheck(_selrow, Col_HsActive, CheckEnum.Unchecked)
                            End If
                            '  .Rows(_Row)(Col_HsActive) = True
                            C1HistoryDetails.SetCellImage(_selrow, Col_HButton, imgTreeVIew.Images(5)) '' Chetan Added
                            C1HistoryDetails.Rows(_selrow).Height = C1HistoryDetails.Rows.DefaultSize * arrReaction.Length - 1
                        ElseIf _categorytype = "OB Initial Physical Examination" Then
                            '' Category is Allergy Then Only we have to show Reactions & Active/Inactive CheckBOX                           
                            Dim cStyle As C1.Win.C1FlexGrid.CellStyle = Nothing
                            Dim rgReaction As C1.Win.C1FlexGrid.CellRange = C1HistoryDetails.GetCellRange(_selrow, Col_HButton, _selrow, Col_HButton)
                            Dim strReaction As String
                            Dim strActive As String
                            Dim rgActive As C1.Win.C1FlexGrid.CellRange = C1HistoryDetails.GetCellRange(_selrow, Col_HsActive, _selrow, Col_HsActive)
                            Dim arr() As String 'Srting Array
                            arr = Split(Convert.ToString(C1HistoryDetails.Rows(_selrow)(Col_HsReaction)), "|")
                            If arr.Length = 2 Then
                                strReaction = arr.GetValue(0)
                                strActive = Convert.ToString(C1HistoryDetails.Rows(_selrow)(Col_HsActive))
                            Else
                                strReaction = Convert.ToString(C1HistoryDetails.Rows(_selrow)(Col_HsReaction))
                                strActive = Convert.ToString(C1HistoryDetails.Rows(_selrow)(Col_HsActive))
                            End If


                            C1HistoryDetails.SetCellImage(_selrow, Col_HButton, imgTreeVIew.Images(5)) '' Chetan Added


                        ElseIf _categorytype = "Fam" Then
                            '' Category is Allergy Then Only we have to show Reactions & Active/Inactive CheckBOX                           
                            Dim cStyle As C1.Win.C1FlexGrid.CellStyle = Nothing
                            Dim rgReaction As C1.Win.C1FlexGrid.CellRange = C1HistoryDetails.GetCellRange(_selrow, Col_HButton, _selrow, Col_HButton)
                            Dim strReaction As String
                            Dim strActive As String
                            Dim rgActive As C1.Win.C1FlexGrid.CellRange = C1HistoryDetails.GetCellRange(_selrow, Col_HsActive, _selrow, Col_HsActive)
                            Dim arr() As String 'Srting Array
                            arr = Split(Convert.ToString(C1HistoryDetails.Rows(_selrow)(Col_HsReaction)), "|")
                            If arr.Length = 2 Then
                                strReaction = arr.GetValue(0)
                                strActive = Convert.ToString(C1HistoryDetails.Rows(_selrow)(Col_HsActive))
                            Else
                                strReaction = Convert.ToString(C1HistoryDetails.Rows(_selrow)(Col_HsReaction))
                                strActive = Convert.ToString(C1HistoryDetails.Rows(_selrow)(Col_HsActive))
                            End If

                            Dim arrReaction As String()
                            arrReaction = strReaction.Split(vbNewLine)
                            rgReaction.Style = cStyle
                            If IsActive Then


                                rgActive.StyleNew.DataType = GetType(Boolean)
                                rgActive.StyleNew.TextAlign = TextAlignEnum.CenterCenter
                                rgActive.StyleNew.ImageAlign = ImageAlignEnum.CenterCenter
                                rgActive.Checkbox = CheckEnum.Checked
                                If strActive = "True" Then
                                    C1HistoryDetails.SetCellCheck(_selrow, Col_HsActive, CheckEnum.Checked)
                                Else
                                    C1HistoryDetails.SetCellCheck(_selrow, Col_HsActive, CheckEnum.Unchecked)
                                End If
                                '  .Rows(_Row)(Col_HsActive) = True
                            End If
                            C1HistoryDetails.SetCellImage(_selrow, Col_HButton, imgTreeVIew.Images(5)) '' Chetan Added
                            C1HistoryDetails.Rows(_selrow).Height = C1HistoryDetails.Rows.DefaultSize * arrReaction.Length - 1
                            'Bug #61329: 00000599 : patient has multiple history items in different casing the system will throw an error.
                        ElseIf C1HistoryDetails.GetData(_selrow, Col_HHidden).ToString.ToUpper = "SMOKING STATUS" Then '' CODE FOR THE '' SMOKING ''
                            'Dim cStyle As C1.Win.C1FlexGrid.CellStyle

                            Dim strReaction As String
                            Dim strActive As String
                            Dim rgActive As C1.Win.C1FlexGrid.CellRange = C1HistoryDetails.GetCellRange(_selrow, Col_HsActive, _selrow, Col_HsActive)
                            Dim arr() As String 'Srting Array
                            arr = Split(Convert.ToString(C1HistoryDetails.Rows(_selrow)(Col_HsReaction)), "|")
                            If arr.Length = 2 Then
                                strReaction = arr.GetValue(0)
                                strActive = Convert.ToString(C1HistoryDetails.Rows(_selrow)(Col_HsActive))
                            Else
                                strReaction = Convert.ToString(C1HistoryDetails.Rows(_selrow)(Col_HsReaction))
                                strActive = Convert.ToString(C1HistoryDetails.Rows(_selrow)(Col_HsActive))
                            End If

                            Dim arrReaction As String()
                            arrReaction = strReaction.Split(vbNewLine)



                            Dim strReactions As String = ""
                            ' Dim objclsPatientHistory As New clsPatientHistory

                            Dim rgBrowseBtn As C1.Win.C1FlexGrid.CellRange = C1HistoryDetails.GetCellRange(_selrow, Col_HSmokeButton, _selrow, Col_HSmokeButton)
                            C1HistoryDetails.SetCellImage(_selrow, Col_HSmokeButton, imgTreeVIew.Images(5))
                            C1HistoryDetails.SetData(_selrow, Col_HSmokeButton, strReactions)
                            'objclsPatientHistory = Nothing

                        ElseIf IsActive = True Then
                            ' Dim cStyle As C1.Win.C1FlexGrid.CellStyle

                            Dim strReaction As String
                            Dim strActive As String
                            Dim rgActive As C1.Win.C1FlexGrid.CellRange = C1HistoryDetails.GetCellRange(_selrow, Col_HsActive, _selrow, Col_HsActive)
                            Dim arr() As String 'Srting Array
                            arr = Split(Convert.ToString(C1HistoryDetails.Rows(_selrow)(Col_HsReaction)), "|")
                            If arr.Length = 2 Then
                                strReaction = arr.GetValue(0)
                                strActive = Convert.ToString(C1HistoryDetails.Rows(_selrow)(Col_HsActive))
                            Else
                                strReaction = Convert.ToString(C1HistoryDetails.Rows(_selrow)(Col_HsReaction))
                                strActive = Convert.ToString(C1HistoryDetails.Rows(_selrow)(Col_HsActive))
                            End If


                            rgActive.StyleNew.DataType = GetType(Boolean)
                            rgActive.StyleNew.TextAlign = TextAlignEnum.CenterCenter
                            rgActive.StyleNew.ImageAlign = ImageAlignEnum.CenterCenter
                            rgActive.Checkbox = CheckEnum.Checked
                            If strActive = "True" Then
                                C1HistoryDetails.SetCellCheck(_selrow, Col_HsActive, CheckEnum.Checked)
                            Else
                                C1HistoryDetails.SetCellCheck(_selrow, Col_HsActive, CheckEnum.Unchecked)
                            End If


                        End If
                        ' _selrow = _newRow
                        _selrow = _newRow + 1
                    Next
                    C1HistoryDetails.Row = _selrow
                End If
            End If
            For k As Integer = 0 To dsHistory.Tables("History").Rows.Count - 1
                If dsHistory.Tables("History").Rows(k).RowState <> DataRowState.Deleted Then
                    dsHistory.Tables("History").Rows(k)("RowState") = "Added"
                End If

            Next
            C1HistoryDetails_EnterCell(Nothing, Nothing)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Remove, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ' MessageBox.Show(ex.ToString, "Patient History", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub frmHistory_Deactivate(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Deactivate

        If Not IsNothing(Me.Parent) Then
            If Not IsNothing(uiPanSplitScreen_History) Then
                If Not IsNothing(uiPanSplitScreen_History.Parent) Then
                    If uiPanSplitScreen_History.Parent IsNot Me Then
                        uiPanSplitScreen_History.Parent.Visible = False
                        uiPanSplitScreen_History.Parent.Hide()
                        uiPanSplitScreen_History.Parent.Update()
                    End If
                End If
            End If
        End If


    End Sub

    Private Sub btnClearSearchHistory_Click(sender As System.Object, e As System.EventArgs) Handles btnClearSearchHistory.Click
        txtsearchhistory.ResetText()
        txtsearchhistory.Focus()
    End Sub

    Private Function GetCategorynStatus(ByVal _HistoryType As String, ByRef _CategoryType As String, ByRef IsActive As Boolean, ByRef IsOnsetDate As Boolean)

        Dim stronsetActiveStatus As String = ""
        Dim _arrOnsetActive() As String

        Try


            If _CategoryType = "OB Medical History" Or _CategoryType = "OB Genetic History" Or _CategoryType = "OB Infection History" Then

            ElseIf _CategoryType.Contains("Smoking Status") = True Then

                IsActive = False
                IsOnsetDate = True
                Return _CategoryType

            ElseIf _CategoryType = "OB Initial Physical Examination" Then
                IsActive = False
                IsOnsetDate = False

            Else
                If _HistoryType = "" Then
                    _CategoryType = getHistoryTypefromcategorymaster(_CategoryType)
                Else
                    _CategoryType = _HistoryType
                End If
            End If

            If _CategoryType <> "" Then
                If _CategoryType = "OB Medical History" Or _CategoryType = "OB Genetic History" Or _CategoryType = "OB Infection History" Then
                    IsActive = True
                    IsOnsetDate = False

                ElseIf _CategoryType = "OB Initial Physical Examination" Then
                    IsActive = False
                    IsOnsetDate = False


                Else

                    If _CategoryType.Length > 2 Then
                        stronsetActiveStatus = CheckHistoryTypeinStandardTable(_CategoryType)
                        _arrOnsetActive = stronsetActiveStatus.Split(",")
                        If IsNothing(_arrOnsetActive) = False Then
                            If _arrOnsetActive.Length >= 1 Then
                                IsOnsetDate = _arrOnsetActive.GetValue(0)
                                IsActive = _arrOnsetActive.GetValue(1)
                            End If
                        End If
                    End If
                End If
            End If

            Return _CategoryType

        Catch ex As Exception
            Return Nothing
        Finally

        End Try

    End Function

#Region "Refusal Code Changes"

    Private Sub btnBrowseSNOMEDRefusedCode_Click(sender As System.Object, e As System.EventArgs) Handles Label59SNOMEDRefusedCode.Click
        Try
            ofrmRefusalList = New frmViewListControl
            oRefusalListControl = New gloListControl.gloListControl(GetConnectionString(), gloListControl.gloListControlType.MUCQMRefusedCode, False, Me.Width)
            oRefusalListControl.ControlHeader = "Refusal Reason Code"
            'set the property true for refused code you want 
            oRefusalListControl.bShowNotTakenCodes = True
            oRefusalListControl.bShowAttributeCodes = True
            '' oRefusalListControl.strSearchText = strRefusalCode
            AddHandler oRefusalListControl.ItemSelectedClick, AddressOf oRefusalListControl_ItemSelectedClick
            AddHandler oRefusalListControl.ItemClosedClick, AddressOf oRefusalListControl_ItemClosedClick
            ofrmRefusalList.Controls.Add(oRefusalListControl)
            oRefusalListControl.Dock = DockStyle.Fill
            oRefusalListControl.BringToFront()

            oRefusalListControl.ShowHeaderPanel(False)
            'oDiagnosisListControl.OpenControl()
            ofrmRefusalList.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
            ofrmRefusalList.Text = "Refusal Reason Code"
            ofrmRefusalList.ShowDialog(IIf(IsNothing(CType(ofrmRefusalList, Control).Parent), Me, CType(ofrmRefusalList, Control).Parent))
            If (IsNothing(oDiagnosisListControl) = False) Then
                ofrmRefusalList.Controls.Remove(oDiagnosisListControl)
                RemoveHandler oRefusalListControl.ItemSelectedClick, AddressOf oRefusalListControl_ItemSelectedClick
                RemoveHandler oRefusalListControl.ItemClosedClick, AddressOf oRefusalListControl_ItemClosedClick
                oRefusalListControl.Dispose()
                oRefusalListControl = Nothing
            End If

            If IsNothing(ofrmRefusalList) = False Then
                ofrmRefusalList.Dispose()
                ofrmRefusalList = Nothing
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.ProblemList, gloAuditTrail.ActivityCategory.ProblemList, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnClearSNOMEDRefusedCode_Click(sender As System.Object, e As System.EventArgs) Handles btnClearSNOMEDRefusedCode.Click
        txtSNOMEDRefusedCode.Text = ""
        strRefusalCode = String.Empty
        strRefusalDescription = String.Empty
        C1HistoryDetails.SetData(C1HistoryDetails.Row, "sRefusalCode", "")
        C1HistoryDetails.SetData(C1HistoryDetails.Row, "sRefusalDesc", "")
    End Sub

    Private Sub oRefusalListControl_ItemClosedClick(sender As Object, e As EventArgs)
        ofrmRefusalList.Close()
    End Sub

    Private Sub oRefusalListControl_ItemSelectedClick(sender As Object, e As EventArgs)
        Try
            If oRefusalListControl.SelectedItems.Count > 0 Then
                For i As Int16 = 0 To oRefusalListControl.SelectedItems.Count - 1
                    txtSNOMEDRefusedCode.Text = oRefusalListControl.SelectedItems(i).Code + " ; " + oRefusalListControl.SelectedItems(i).Description
                    strRefusalCode = Convert.ToString(oRefusalListControl.SelectedItems(i).Code)
                    strRefusalDescription = Convert.ToString(oRefusalListControl.SelectedItems(i).Description)
                    C1HistoryDetails.SetData(C1HistoryDetails.Row, "sRefusalCode", strRefusalCode)
                    C1HistoryDetails.SetData(C1HistoryDetails.Row, "sRefusalDesc", strRefusalDescription)
                Next
                ofrmRefusalList.Close()
            Else
                txtSNOMEDRefusedCode.Text = ""
                C1HistoryDetails.SetData(C1HistoryDetails.Row, "sRefusalCode", "")
                C1HistoryDetails.SetData(C1HistoryDetails.Row, "sRefusalDesc", "")
                ofrmRefusalList.Close()
            End If

        Catch ex As Exception
            MessageBox.Show("Error on User List Control." & ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
#End Region
    ' Dim ofrmCPTList As frmViewListControl
    Private oListControl As gloListControl.gloListControl
    Private Sub btnbrloinc_Click(sender As System.Object, e As System.EventArgs) Handles btnbrloinc.Click

        Try

            ofrmCPTList = New frmViewListControl
            ' Dim arrCPTTextSplit As String()
            oListControl = New gloListControl.gloListControl(GetConnectionString(), gloListControl.gloListControlType.HistoryLOINC, False, Me.Width)
            oListControl.ControlHeader = "LOINC"
            AddHandler oListControl.ItemSelectedClick, AddressOf oListControl_ItemSelectedClick
            AddHandler oListControl.ItemClosedClick, AddressOf oListControl_ItemClosedClick
            ofrmCPTList.Controls.Add(oListControl)
            oListControl.Dock = DockStyle.Fill
            oListControl.BringToFront()

            oListControl.ShowHeaderPanel(False)
            oListControl.OpenControl()
            ofrmCPTList.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
            ofrmCPTList.Text = "LOINC"
            ofrmCPTList.ShowDialog(IIf(IsNothing(CType(ofrmCPTList, Control).Parent), Me, CType(ofrmCPTList, Control).Parent))

            If IsNothing(ofrmCPTList) = False Then
                RemoveHandler oListControl.ItemSelectedClick, AddressOf oListControl_ItemSelectedClick
                RemoveHandler oListControl.ItemClosedClick, AddressOf oListControl_ItemClosedClick
                ofrmCPTList.Controls.Remove(oListControl)
                oListControl.Dispose()
                oListControl = Nothing
                ofrmCPTList.Dispose()
                ofrmCPTList = Nothing
                'End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        'oListControl = New gloListControl.gloListControl(GetConnectionString, gloListControl.gloListControlType.CQMOrders, True, Me.Width)
        ''oListControl.CMSID = cmb_measure.SelectedValue.ToString()
        'oListControl.ClinicID = 1
        'oListControl.ControlHeader = "Orders"

        '' AddHandler oListControl.ItemSelectedClick, AddressOf oListControl_ItemSelectedClick
        '' AddHandler oListControl.ItemClosedClick, AddressOf oListControl_ItemClosedClick

        'Me.Controls.Add(oListControl)
        ''    pnlICD9.Visible = True
        ''    pnlICD9.BringToFront()
        ''if (cmbInsCompany.DataSource != null)
        ''{
        ''    for (int i = 0; i < cmbInsCompany.Items.Count; i++)
        ''    {
        ''        cmbInsCompany.SelectedIndex = i;
        ''        cmbInsCompany.Refresh();
        ''        oListControl.SelectedItems.Add(Convert.ToInt64(cmbInsCompany.SelectedValue), cmbInsCompany.Text);
        ''    }
        ''}
        ''   AddSelectedItemstoListControl(lstVw_Lab)
        'oListControl.OpenControl()
        'oListControl.Dock = DockStyle.Fill
        'oListControl.BringToFront()
    End Sub
    Private Sub oListControl_ItemSelectedClick(sender As System.Object, e As System.EventArgs)
        Dim strloinc As String = ""
        If oListControl.SelectedItems.Count > 0 Then
            For i As Int16 = 0 To oListControl.SelectedItems.Count - 1
                strloinc = oListControl.SelectedItems(i).Code + " : " + oListControl.SelectedItems(i).Description
                If (strloinc.Trim().Length > 3) Then
                    txtloinc.Text = strloinc
                Else
                    txtloinc.Text = ""
                End If
                ' strICD9 = txtICD9Code.Text
                '   txtICD9Code.Tag = oDiagnosisListControl.SelectedItems(i).ID
                C1HistoryDetails.SetData(C1HistoryDetails.Row, Col_LoincCode, oListControl.SelectedItems(i).Code)
                C1HistoryDetails.SetData(C1HistoryDetails.Row, Col_LoincDescr, oListControl.SelectedItems(i).Description)

                ' C1HistoryDetails.SetData(2, col_HCPT, "33")
            Next
            ofrmCPTList.Close()
        Else
            txtloinc.Text = ""
            C1HistoryDetails.SetData(C1HistoryDetails.Row, Col_LoincCode, "")
            C1HistoryDetails.SetData(C1HistoryDetails.Row, Col_LoincDescr, "")
            ' txtICD9Code.Tag = ""
            ofrmCPTList.Close()
        End If



    End Sub
    Private Sub oListControl_ItemClosedClick(sender As System.Object, e As System.EventArgs)

        '

        ' AddItems(lstVw_Lab)
        ofrmCPTList.Close()
        If IsNothing(ofrmCPTList) = False Then
            ofrmCPTList = Nothing
        End If


    End Sub

    Private Sub btnclloinc_Click(sender As System.Object, e As System.EventArgs) Handles btnclloinc.Click
        txtloinc.Text = ""
        C1HistoryDetails.SetData(C1HistoryDetails.Row, Col_LoincCode, "")
        C1HistoryDetails.SetData(C1HistoryDetails.Row, Col_LoincDescr, "")
    End Sub
    Dim ToolTiptxtSmokingStatus As ToolTip = Nothing

    Private Sub lblconcptid_MouseHover(sender As Object, e As System.EventArgs) Handles lblconcptid.MouseHover
        If (ToolTiptxtSmokingStatus Is Nothing) Then
            ToolTiptxtSmokingStatus = New System.Windows.Forms.ToolTip
        End If
        ToolTiptxtSmokingStatus.AutomaticDelay = 1000
        ToolTiptxtSmokingStatus.ReshowDelay = 1000
        ToolTiptxtSmokingStatus.SetToolTip(Me.lblconcptid, lblconcptid.Text)


    End Sub

    Private Sub lblconcptid_MouseLeave(sender As Object, e As System.EventArgs) Handles lblconcptid.MouseLeave

    End Sub
    Private Sub lblconcptid_MouseMove(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles lblconcptid.MouseMove
        'If Not ToolTiptxtSmokingStatus Is Nothing Then
        '    ToolTiptxtSmokingStatus.Dispose()
        '    ToolTiptxtSmokingStatus = Nothing
        'End If
       

    End Sub

    Private Sub txtSNOMEDRefusedCode_MouseHover(sender As Object, e As System.EventArgs) Handles txtSNOMEDRefusedCode.MouseHover
        If (ToolTiptxtSmokingStatus Is Nothing) Then
            ToolTiptxtSmokingStatus = New System.Windows.Forms.ToolTip
        End If
        ToolTiptxtSmokingStatus.AutomaticDelay = 1000
        ToolTiptxtSmokingStatus.ReshowDelay = 1000
        ToolTiptxtSmokingStatus.SetToolTip(Me.txtSNOMEDRefusedCode, txtSNOMEDRefusedCode.Text)
    End Sub

    

    Private Sub txtSNOMEDRefusedCode_MouseMove(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles txtSNOMEDRefusedCode.MouseMove
       
    End Sub

    Private Sub txtloinc_MouseHover(sender As Object, e As System.EventArgs) Handles txtloinc.MouseHover
        If (ToolTiptxtSmokingStatus Is Nothing) Then
            ToolTiptxtSmokingStatus = New System.Windows.Forms.ToolTip
        End If
        ToolTiptxtSmokingStatus.AutomaticDelay = 1000
        ToolTiptxtSmokingStatus.ReshowDelay = 1000
        ToolTiptxtSmokingStatus.SetToolTip(Me.txtloinc, txtloinc.Text)
    End Sub

    Private Sub txtloinc_MouseMove(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles txtloinc.MouseMove

    End Sub

    Private Sub btnBrowseCqm_Click(sender As System.Object, e As System.EventArgs) Handles btnBrowseCqm.Click
        Try
            ofrmCQMList = New frmViewListControl
            oCQMListControl = New gloListControl.gloListControl(GetConnectionString(), gloListControl.gloListControlType.CQMCategoriesValueSet, False, Me.Width)
            oCQMListControl.ControlHeader = "CQM Categories"
            'set the property true for refused code you want 
            oCQMListControl.bShowNotTakenCodes = True
            oCQMListControl.bShowAttributeCodes = True

            AddHandler oCQMListControl.ItemSelectedClick, AddressOf oCQMListControl_ItemSelectedClick
            AddHandler oCQMListControl.ItemClosedClick, AddressOf oCQMListControl_ItemClosedClick
            ofrmCQMList.Controls.Add(oCQMListControl)
            oCQMListControl.Dock = DockStyle.Fill
            oCQMListControl.BringToFront()

            oCQMListControl.ShowHeaderPanel(False)

            ofrmCQMList.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
            ofrmCQMList.Text = "CQM Categories"
            ofrmCQMList.ShowDialog(IIf(IsNothing(CType(ofrmCQMList, Control).Parent), Me, CType(ofrmCQMList, Control).Parent))
            If (IsNothing(oDiagnosisListControl) = False) Then
                ofrmCQMList.Controls.Remove(oDiagnosisListControl)
                RemoveHandler oCQMListControl.ItemSelectedClick, AddressOf oCQMListControl_ItemSelectedClick
                RemoveHandler oCQMListControl.ItemClosedClick, AddressOf oCQMListControl_ItemClosedClick
                oCQMListControl.Dispose()
                oCQMListControl = Nothing
            End If

            If IsNothing(ofrmCQMList) = False Then
                ofrmCQMList.Dispose()
                ofrmCQMList = Nothing
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.ProblemList, gloAuditTrail.ActivityCategory.ProblemList, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub btnBrowseUDI_Click(sender As System.Object, e As System.EventArgs) Handles btnBrowseUDI.Click
        Try
            ofrmList = New frmViewListControl
            oListControl = New gloListControl.gloListControl(GetConnectionString(), gloListControl.gloListControlType.ImplantableDevices, False, Me.Width)

            AddHandler oListControl.ItemSelectedClick, AddressOf ofrmListControl_ItemSelectedClick
            AddHandler oListControl.ItemClosedClick, AddressOf ofrmListControl_ItemClosedClick
            ofrmList.Controls.Add(oListControl)
            oListControl.Dock = DockStyle.Fill
            oListControl.BringToFront()
            'For i As Integer = 0 To lstviewPatient.Items.Count - 1
            '    oListControl.SelectedItems.Add(Convert.ToInt64(lstviewPatient.Items(i).Tag), lstviewPatient.Items(i).Text)
            'Next
            oListControl.PatientID = m_PatientID
            oListControl.ShowHeaderPanel(False)
            ofrmList.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
            ofrmList.Text = "Patient Implantable Devices"
            ofrmList.ShowDialog(IIf(IsNothing(ofrmList.Parent), Me, ofrmList.Parent))
            RemoveHandler oListControl.ItemSelectedClick, AddressOf ofrmListControl_ItemSelectedClick
            RemoveHandler oListControl.ItemClosedClick, AddressOf ofrmListControl_ItemClosedClick
            ofrmList.Controls.Remove(oListControl)
            oListControl.Dispose()
            oListControl = Nothing
            If IsNothing(ofrmList) = False Then
                ofrmList.Dispose()
                ofrmList = Nothing
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.ImplantableDevice, gloAuditTrail.ActivityCategory.ImplantDevices, gloAuditTrail.ActivityType.Select, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub
    Private Sub ofrmListControl_ItemClosedClick(sender As Object, e As EventArgs)
        ofrmList.Close()
    End Sub
    Private Sub ofrmListControl_ItemSelectedClick(sender As Object, e As EventArgs)
        Try
            If oListControl.SelectedItems.Count > 0 Then
                For i As Int16 = 0 To oListControl.SelectedItems.Count - 1
                    nDeviceList_ID = Convert.ToString(oListControl.SelectedItems(i).ID)
                    sDeviceID = Convert.ToString(oListControl.SelectedItems(i).Code)
                    sBrandName = Convert.ToString(oListControl.SelectedItems(i).Description)
                    C1HistoryDetails.SetData(C1HistoryDetails.Row, Col_DeviceList_ID, nDeviceList_ID)
                    dsHistory.Tables("History").Rows(C1HistoryDetails.Row - 1)("nDeviceList_ID") = nDeviceList_ID
                    Dim dr() As System.Data.DataRow = dsHistory.Tables("UDI").Select("nDevicelist_Id=" + nDeviceList_ID.ToString)
                    If dr.Length <= 0 Then
                        Dim nrows As Integer = dsHistory.Tables("UDI").Rows.Count
                        dsHistory.Tables("UDI").Rows.Add()

                        dsHistory.Tables("UDI").Rows(nrows)("nDeviceList_ID") = nDeviceList_ID
                        dsHistory.Tables("UDI").Rows(nrows)("sDeviceID") = sDeviceID
                        dsHistory.Tables("UDI").Rows(nrows)("sBrandName") = sBrandName
                        dsHistory.Tables("UDI").AcceptChanges()
                    End If
                    If sDeviceID <> "" And sBrandName <> "" Then
                        lblUDI.Text = sDeviceID + "-" + sBrandName
                    Else
                        lblUDI.Text = ""
                    End If

                Next
                ofrmList.Close()
            Else
                lblUDI.Text = ""
                C1HistoryDetails.SetData(C1HistoryDetails.Row, Col_DeviceList_ID, "")
                ofrmCQMList.Close()
            End If

        Catch ex As Exception
            MessageBox.Show("Error on User List Control." & ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub btnClearCqm_Click(sender As System.Object, e As System.EventArgs) Handles btnClearCqm.Click
        txtCqm.Text = ""
        strCqmCode = String.Empty
        strCqmDescription = String.Empty
        C1HistoryDetails.SetData(C1HistoryDetails.Row, "sValueSetOID", "")
        C1HistoryDetails.SetData(C1HistoryDetails.Row, Col_CQMDesc, "")
    End Sub
    Private Sub oCQMListControl_ItemClosedClick(sender As Object, e As EventArgs)
        ofrmCQMList.Close()
    End Sub

    Private Sub oCQMListControl_ItemSelectedClick(sender As Object, e As EventArgs)
        Try
            If oCQMListControl.SelectedItems.Count > 0 Then
                For i As Int16 = 0 To oCQMListControl.SelectedItems.Count - 1
                    txtCqm.Text = oCQMListControl.SelectedItems(i).Description
                    strCqmCode = Convert.ToString(oCQMListControl.SelectedItems(i).Code)
                    strCqmDescription = Convert.ToString(oCQMListControl.SelectedItems(i).Description)
                    dsHistory.Tables("History").Rows(C1HistoryDetails.Row - 1)("sValueSetOID") = strCqmCode

                    C1HistoryDetails.SetData(C1HistoryDetails.Row, "sValueSetOID", strCqmCode)
                    C1HistoryDetails.SetData(C1HistoryDetails.Row, Col_CQMDesc, strCqmDescription)

                Next
                ofrmCQMList.Close()
            Else
                txtCqm.Text = ""
                C1HistoryDetails.SetData(C1HistoryDetails.Row, "sValueSetOID", "")
                C1HistoryDetails.SetData(C1HistoryDetails.Row, Col_CQMDesc, "")
                ofrmCQMList.Close()
            End If

        Catch ex As Exception
            MessageBox.Show("Error on User List Control." & ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub cmbStatus_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbStatus.SelectedIndexChanged
        Try
            If C1HistoryDetails.Row > 0 Then
                sProcStatus = cmbStatus.Text

                C1HistoryDetails.SetData(C1HistoryDetails.Row, COl_sProcStatus, sProcStatus)
                dsHistory.Tables("History").Rows(C1HistoryDetails.Row - 1)("sProcStatus") = sProcStatus
                If sProcStatus = "Completed" Then
                    dsHistory.Tables("History").Rows(C1HistoryDetails.Row - 1)("dtConcernEndDate") = DateTime.Now
                Else

                    dsHistory.Tables("History").Rows(C1HistoryDetails.Row - 1)("dtConcernEndDate") = DBNull.Value

                End If


            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Sub cmbAllergySeverity_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbAllergySeverity.SelectedIndexChanged
        Try
            If C1HistoryDetails.Row > 0 Then
                'C1HistoryDetails.SetData(C1HistoryDetails.Row, "AllergySeverity", cmbAllergySeverity.Text.ToString() & "|" & cmbAllergySeverity.SelectedValue.ToString())
                'dsHistory.Tables("History").Rows(C1HistoryDetails.Row - 1)("AllergySeverity") = cmbAllergySeverity.Text.ToString() & "|" & cmbAllergySeverity.SelectedValue.ToString()
                C1HistoryDetails.SetData(C1HistoryDetails.Row, "AllergySeverity", cmbAllergySeverity.Text.ToString())
                dsHistory.Tables("History").Rows(C1HistoryDetails.Row - 1)("AllergySeverity") = cmbAllergySeverity.Text.ToString()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub


    Private Sub tblHistoryReconcillation_Click(sender As System.Object, e As System.EventArgs) Handles tblHistoryReconcillation.Click
        Dim frmMedRec As frmMedReconciliation = Nothing

        Try
            frmMedRec = New frmMedReconciliation(m_PatientID)
            frmMedRec.dtMed = dtPHistoryMedRec
            frmMedRec.Reconcialtiontype = 3
            If frmMedRec.Reconcialtiontype = 3 Then
                frmMedRec.Text = "Allergies Reconciliation"
                frmMedRec.ShowDialog(IIf(IsNothing(frmMedRec.Parent), Me, frmMedRec.Parent))
                blnmedRecupdated = frmMedRec.RecUpdated
                dtPHistoryMedRec = frmMedRec.dtMed
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally
            If Not IsNothing(frmMedRec) Then
                frmMedRec.Dispose()
                frmMedRec = Nothing
            End If

        End Try
    End Sub
    Private Sub SavePHistoryMedicationReconcillation() ''MedicationReconcillation for History (2015 Certification)
        Try
            If Not IsNothing(dtPHistoryMedRec) Then
                If dtPHistoryMedRec.Rows.Count > 0 Then

                    dtPHistoryMedRec.Rows(0)("PatientID") = m_PatientID


                    dtPHistoryMedRec.Rows(0)("VisitID") = m_VisitID
                    dtPHistoryMedRec.AcceptChanges()
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub


    Private Function GetPHistoryMedicationReconcillationDetails(nVisitID As Long, nPatientID As Long, nReconcillationType As Int16) As DataTable
        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString)
        Dim oParameters As New gloDatabaseLayer.DBParameters()
        Dim dt As New DataTable()

        Try
            oDB.Connect(False)
            oParameters.Add("@VisitID", nVisitID, ParameterDirection.Input, SqlDbType.BigInt)
            oParameters.Add("@PatientID", nPatientID, ParameterDirection.Input, SqlDbType.BigInt)
            oParameters.Add("@ReconcillationType", nReconcillationType, ParameterDirection.Input, SqlDbType.SmallInt) ''Medication Reconcillation for problem list( 2015 certification code)
            oDB.Retrive("GetMedicationReconcillation", oParameters, dt)
            Return dt
        Catch dbEx As gloDatabaseLayer.DBException
            dbEx.ERROR_Log(dbEx.ToString())
            dbEx = Nothing
            Return Nothing
        Catch ex As Exception
            MessageBox.Show("ERROR : " & ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
            Return Nothing
        Finally
            If Not IsNothing(dt) Then
                dt.Dispose()
                dt = Nothing
            End If
            If Not IsNothing(oParameters) Then
                oParameters.Dispose()
                oParameters = Nothing
            End If
            If oDB IsNot Nothing Then
                oDB.Disconnect()
            End If
            If oDB IsNot Nothing Then
                oDB.Dispose()
                oDB = Nothing
            End If
        End Try


    End Function



    Public Function getProviderTaxID(Optional ByVal nProviderID As Int64 = 0) As Boolean
        sProviderTaxID = ""
        nProviderAssociationID = 0
        Try
            Dim oResult As DialogResult = System.Windows.Forms.DialogResult.OK
            Dim oForm As New gloGlobal.frmSelectProviderTaxID(Convert.ToInt64(nProviderID))
            If oForm.dtProviderTaxIDs IsNot Nothing AndAlso oForm.dtProviderTaxIDs.Rows.Count > 1 Then
                oForm.ShowDialog(Me)
                oResult = oForm.DialogResult
                nProviderAssociationID = oForm.nAssociationID
                sProviderTaxID = oForm.sProviderTaxID

                oForm = Nothing
            ElseIf oForm.dtProviderTaxIDs IsNot Nothing AndAlso oForm.dtProviderTaxIDs.Rows.Count = 1 Then
                ''oResult = oForm.DialogResult
                nProviderAssociationID = Convert.ToInt64(oForm.dtProviderTaxIDs.Rows(0)("nAssociationID"))
                sProviderTaxID = Convert.ToString(oForm.dtProviderTaxIDs.Rows(0)("sTIN"))
                oForm = Nothing
            Else
                nProviderAssociationID = 0
                sProviderTaxID = ""
            End If

            If oResult = Windows.Forms.DialogResult.OK Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
            Return False

        Finally
        End Try
    End Function

    Private Function GetMedicalReconcillationID(ByVal CurrentVisitId As Long) As Long
        Dim MedicalReconcillationId As Long = 0
        Dim conn As SqlConnection = Nothing
        Dim cmd As SqlCommand = Nothing
        Try
            conn = New SqlConnection
            Dim _strSQL As String = ""
            _strSQL = "SELECT ISNULL(MedicationReconcillationId,0) FROM MedicationReconcillation " &
                      "WHERE nVisitId= " & CurrentVisitId & " and nPatientId=" & m_PatientID & " and nReconcillationType=" & 3 & ""
            conn.ConnectionString = GetConnectionString()
            conn.Open()
            cmd = New SqlCommand(_strSQL, conn)
            _strSQL = cmd.ExecuteScalar & ""
            If _strSQL <> "" Then
                MedicalReconcillationId = Convert.ToInt64(_strSQL)
            End If
            conn.Close()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally
            If Not IsNothing(cmd) Then
                cmd.Dispose()
                cmd = Nothing
            End If
            If Not IsNothing(conn) Then
                conn.Dispose()
                conn = Nothing
            End If
        End Try
        Return MedicalReconcillationId
    End Function

    Private Sub ResolvedEndDate_ValueChanged(sender As System.Object, e As System.EventArgs) Handles ResolvedEndDate.ValueChanged
        If C1HistoryDetails.Row > -1 Then

            If Not dsHistory Is Nothing Then

                ChkResolvedEnddate.Enabled = True
                If dsHistory.Tables.Contains("History") Then
                    If ResolvedEndDate.Checked = True Then
                        C1HistoryDetails.SetData(C1HistoryDetails.Row, Col_ResolvedEndDate, ResolvedEndDate.Value)
                        dsHistory.Tables("History").Rows(C1HistoryDetails.Row - 1)("dtObservationEndDate") = ResolvedEndDate.Value
                    Else
                        dsHistory.Tables("History").Rows(C1HistoryDetails.Row - 1)("dtObservationEndDate") = DBNull.Value

                    End If
                End If

            End If
        
        Else
            ChkResolvedEnddate.Enabled = False ''Bug #109096:For no History item ResolvedEnddate issue Resolved
        End If
    End Sub

    Private Sub ChkResolvedEnddate_CheckedChanged(sender As Object, e As System.EventArgs) Handles ChkResolvedEnddate.CheckedChanged
        If C1HistoryDetails.RowSel > 0 Then
            If ChkResolvedEnddate.Checked Then

                ResolvedEndDate.Enabled = True
                dsHistory.Tables("History").Rows(C1HistoryDetails.Row - 1)("dtObservationEndDate") = ResolvedEndDate.Value
            Else
                ResolvedEndDate.Enabled = False
                dsHistory.Tables("History").Rows(C1HistoryDetails.Row - 1)("dtObservationEndDate") = DBNull.Value

            End If
          
        End If


    End Sub
    Public Sub Fill_HistoryConcernStatus()
        Dim objclsProblist As New clsPatientProblemList
        Dim dtConcernStatus As DataTable = Nothing
        Try
            dtConcernStatus = objclsProblist.Get_CDACodeSystemtype("concernstatus")
            If dtConcernStatus IsNot Nothing Then
                If dtConcernStatus.Rows.Count > 0 Then
                    For Each dr As DataRow In dtConcernStatus.Rows
                        cmbStatus.Items.Add(dr("sEMRDisplayName"))
                    Next

                End If
            End If
            cmbStatus.Items.Add("")
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.History, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            objclsProblist.Dispose()
            objclsProblist = Nothing
            If dtConcernStatus IsNot Nothing Then
                dtConcernStatus.Dispose()
                dtConcernStatus = Nothing
            End If
        End Try
    End Sub
    Dim dtIntelorenceType As DataTable = Nothing

    Public Sub Fill_AllergyIntelorenceType()
        Dim objclsProblist As New clsPatientProblemList
        Try
            dtIntelorenceType = objclsProblist.Get_CDACodeSystemtype("Allergy-IntelorenceType")
            If dtIntelorenceType IsNot Nothing Then
                If dtIntelorenceType.Rows.Count > 0 Then

                    RemoveHandler cmbAllergyIntelorenceType.SelectedIndexChanged, AddressOf cmbAllergyIntelorenceType_SelectedIndexChanged
                    cmbAllergyIntelorenceType.DataSource = dtIntelorenceType
                    cmbAllergyIntelorenceType.DisplayMember = "sDisplayName"
                    cmbAllergyIntelorenceType.ValueMember = "sCode"
                    cmbAllergyIntelorenceType.SelectedValue = AllergyIntelorenceType
                    AddHandler cmbAllergyIntelorenceType.SelectedIndexChanged, AddressOf cmbAllergyIntelorenceType_SelectedIndexChanged

                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.History, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            objclsProblist.Dispose()
            objclsProblist = Nothing

        End Try
    End Sub

    Private Sub cmbAllergyIntelorenceType_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbAllergyIntelorenceType.SelectedIndexChanged
        If C1HistoryDetails.Row > 0 Then

            sIntelorenceCode = cmbAllergyIntelorenceType.SelectedValue
            C1HistoryDetails.SetData(C1HistoryDetails.Row, Col_AllergyIntelorenceCode, sIntelorenceCode)
            dsHistory.Tables("History").Rows(C1HistoryDetails.Row - 1)("sAllergyIntelorenceCode") = cmbAllergyIntelorenceType.SelectedValue
        End If
    End Sub
    Private Sub SetNKHistoryVisibility()
        Try
            If (C1HistoryDetails.Rows.Count <= 1) Then
                tblNKAllergies.Visible = True
            Else
                tblNKAllergies.Visible = False
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub tblNKAllergies_Click(sender As Object, e As System.EventArgs) Handles tblNKAllergies.Click
        Dim PatspecCDAspc As gloCCDLibrary.frmPatspecCDAspc
        PatspecCDAspc = New gloCCDLibrary.frmPatspecCDAspc()
        PatspecCDAspc.OpenfrmHistory = True
        PatspecCDAspc.patientID = m_PatientID
        PatspecCDAspc.ShowDialog(Me)
        PatspecCDAspc.Dispose()
        PatspecCDAspc = Nothing
    End Sub


    Private Sub btnShowCQM_Click(sender As System.Object, e As System.EventArgs) Handles btnShowCQM.Click
        If btnShowCQM.Tag = "Expand" Then
            pnlsnomadedetail.Height = 300
            pnlShowCQM.Visible = True
            pnlShowCQM.Dock = DockStyle.Bottom

            btnShowCQM.Tag = "Collapse"
            btnShowCQM.Text = "Collapse"

        ElseIf btnShowCQM.Tag = "Collapse" Then
            pnlsnomadedetail.Height = 170
            pnlShowCQM.Dock = DockStyle.None
            pnlShowCQM.Visible = False

            btnShowCQM.Tag = "Expand"
            btnShowCQM.Text = "Expand"
        End If
    End Sub

    Private Sub btnShowCQM_MouseEnter(sender As System.Object, e As System.EventArgs) Handles btnShowCQM.MouseEnter
        Try
            btnShowCQM.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Orange
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.History, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnShowCQM_MouseLeave(sender As System.Object, e As System.EventArgs) Handles btnShowCQM.MouseLeave
        Try
            btnShowCQM.BackgroundImage = gloEMR.My.Resources.Resources.Img_Button
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.History, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Dim CVXListControl As gloListControl.gloListControl
    Private Sub btnCVXCode_Click(sender As System.Object, e As System.EventArgs) Handles btnCVXCode.Click
        Try
            ofrmCPTList = New frmViewListControl
            CVXListControl = New gloListControl.gloListControl(GetConnectionString(), gloListControl.gloListControlType.CQMCVXCOdes, False, Me.Width)
            CVXListControl.ControlHeader = "CVX"
            'set the property true for refused code you want 
            CVXListControl.bShowNotTakenCodes = True
            CVXListControl.bShowAttributeCodes = True
            '' oRefusalListControl.strSearchText = strRefusalCode
            AddHandler CVXListControl.ItemSelectedClick, AddressOf CVXListControl_ItemSelectedClick
            AddHandler CVXListControl.ItemClosedClick, AddressOf CVXListControl_ItemClosedClick
            ofrmCPTList.Controls.Add(CVXListControl)
            CVXListControl.Dock = DockStyle.Fill
            CVXListControl.BringToFront()

            CVXListControl.ShowHeaderPanel(False)
            'oDiagnosisListControl.OpenControl()
            ofrmCPTList.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
            ofrmCPTList.Text = "CVX Code"
            ofrmCPTList.ShowDialog(IIf(IsNothing(CType(ofrmCPTList, Control).Parent), Me, CType(ofrmCPTList, Control).Parent))

            If IsNothing(ofrmCPTList) = False Then
                RemoveHandler CVXListControl.ItemSelectedClick, AddressOf CVXListControl_ItemSelectedClick
                RemoveHandler CVXListControl.ItemClosedClick, AddressOf CVXListControl_ItemClosedClick
                ofrmCPTList.Controls.Remove(CVXListControl)
                CVXListControl.Dispose()
                CVXListControl = Nothing
                ofrmCPTList.Dispose()
                ofrmCPTList = Nothing
                'End If
            End If

            'If (IsNothing(CVXListControl) = False) Then
            '    ofrmCPTList.Controls.Remove(CVXListControl)
            '    RemoveHandler CVXListControl.ItemSelectedClick, AddressOf CVXListControl_ItemSelectedClick
            '    RemoveHandler CVXListControl.ItemClosedClick, AddressOf CVXListControl_ItemClosedClick
            '    CVXListControl.Dispose()
            '    CVXListControl = Nothing
            'End If

            'If IsNothing(ofrmCPTList) = False Then
            '    ofrmCPTList.Dispose()
            '    ofrmCPTList = Nothing
            'End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.History, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub CVXListControl_ItemSelectedClick(sender As Object, e As EventArgs)
        Try
            If CVXListControl.SelectedItems.Count > 0 Then
                For i As Int16 = 0 To CVXListControl.SelectedItems.Count - 1
                    txtCVXCode.Text = CVXListControl.SelectedItems(i).Code + " : " + CVXListControl.SelectedItems(i).Description

                    C1HistoryDetails.SetData(C1HistoryDetails.Row, Col_CVXCode, CVXListControl.SelectedItems(i).Code)
                    C1HistoryDetails.SetData(C1HistoryDetails.Row, Col_CVXDesc, CVXListControl.SelectedItems(i).Description)
                    dsHistory.Tables("History").Rows(C1HistoryDetails.Row - 1)("CVXCode") = CVXListControl.SelectedItems(i).Code
                    dsHistory.Tables("History").Rows(C1HistoryDetails.Row - 1)("CVXDesc") = CVXListControl.SelectedItems(i).Description

                Next
                ofrmCPTList.Close()
            Else
                txtCVXCode.Text = ""
                C1HistoryDetails.SetData(C1HistoryDetails.Row, Col_CVXCode, "")
                C1HistoryDetails.SetData(C1HistoryDetails.Row, Col_CVXDesc, "")
                ofrmCPTList.Close()
            End If

        Catch ex As Exception
            MessageBox.Show("Error on User List Control." & ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub CVXListControl_ItemClosedClick(sender As System.Object, e As EventArgs)
      ofrmCPTList.Close()
        If IsNothing(ofrmCPTList) = False Then
            ofrmCPTList = Nothing
        End If



    End Sub


    Private Sub btnClsCVXCode_Click(sender As Object, e As System.EventArgs) Handles btnClsCVXCode.Click
        txtCVXCode.Text = ""
        C1HistoryDetails.SetData(C1HistoryDetails.Row, Col_CVXCode, "")
        C1HistoryDetails.SetData(C1HistoryDetails.Row, Col_CVXDesc, "")
    End Sub

  
End Class
