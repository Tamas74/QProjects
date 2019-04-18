#Region " Imports "

Imports System.IO
Imports C1.Win.C1FlexGrid
Imports gloEMRGeneralLibrary.gloEMRActors
Imports gloEMRGeneralLibrary.gloEMRPrescription
Imports gloEMRGeneralLibrary.gloEMRMedication
Imports gloEMR.gloEMRWord

#End Region

#Region " Class frmPatientOBPlan "


Public Class frmPatientOBPlan


#Region " Variable Declaration "

    Inherits System.Windows.Forms.Form
    Implements IPatientContext

    Dim ofrmDiagnosisList As frmViewListControl
    Dim ofrmCPTList As frmViewListControl

    Private oDiagnosisListControl As gloListControl.gloListControl
    Private oCPTListControl As gloListControl.gloListControl
    Dim sLoginName As String
    Dim blnIsLoad As Boolean = False
    Public blncancel As Boolean

    Dim objclsPatientOBPlan As New clsPatientOBPlan
    Dim oSnoMed As gloSnoMed.ClsGeneral


    Dim dsOBPlan As DataSet = Nothing
    Dim _dsTemp As DataSet = Nothing
    '''' Category Name
    Private BtnText As String
    '''' CategoryID
    Private BtnTag As Long
    Dim dt As DataTable
    Private m_VisitID As Long
    Private m_VisitDate As Date
    Private m_strLoginName As String

    Dim m_PatientID As Long
    Public gblnIsFindingAdd As Boolean = False
    Public Shared IsOpen As Boolean = False

    Public Shared blnModify As Boolean = False

    '' Sets if Changes Made In OB Plan
    Public Shared blnChangesMade As Boolean = False

    Friend WithEvents objBtn As System.Windows.Forms.Button

    Dim _Defination1 As String
    Dim _ICD91 As String
    Dim intCheck As Integer = 0

    Dim _blnRecordLock As Boolean '' To the set record lock
    Dim _RecordLock As Boolean
    ''  
    Public myLetter As frmPatientLetter

    Public myCaller As frmPatientExam
    Public myCaller1 As Object

    Public mycallerImm As frmImTransaction
    Private _sReaction As String = ""

    Public myCallerSynopsis As frmPatientSynopsis
    '''' Boolean variable to check that, form is open from Main form or from Patient Exam
    '''' This variable is used for voice purpose
    Public blnOpenFromExam As Boolean = False

    Public blnShowMessageBox As Boolean = True
    Public blnShowAddModeMessageBox As Boolean = True
    Private FormLevelLockID As Long = -1 '' To the set record lock

    Dim _IsFrmImm As Boolean = False

    Dim dtReview As DataTable

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

    Private isOBPlanModified As Boolean = False
    Private isOBPlanLoading As Boolean = True

    Private arrDataDictionary As New ArrayList ''Contains DataDictionary Items to be DELETED.

    Dim blnDIbtnClick As Boolean = False
    Dim RXnormAlergicDrugNDCCode As String = ""
    Dim btntype As Int16

    Dim strUncodedDrugs As New System.Text.StringBuilder
   
    Dim mydtButton As DataTable

    Private WithEvents dgCustomGrid As CustomTask

    Friend WithEvents uiPanSplitScreen_OBPlan As New Janus.Windows.UI.Dock.UIPanelGroup
    Dim clsSplit_OBPlan As New gloEMRGeneralLibrary.clsSplitScreen
    Dim _isPanelClick As Boolean = False

    Dim objCriteria As New DocCriteria
    Dim objWord As New clsWordDocument

    Private Const strOBPlanSource As String = "gloEMR"

    Dim dtSource As DataTable
    Public srCategory As String = ""

    Dim ToolTipbtn_CPTCode As System.Windows.Forms.ToolTip
    Dim ToolTipbtn_ICD9Code As System.Windows.Forms.ToolTip
    Dim ToolTipbtnConceptID As System.Windows.Forms.ToolTip
    Dim ToolTipbtnUp As System.Windows.Forms.ToolTip
    Dim ToolTipbtnDown As System.Windows.Forms.ToolTip

#End Region

#Region " Properties "

    Public Property Reaction() As String
        Get
            Return _sReaction
        End Get
        Set(ByVal value As String)
            _sReaction = value
        End Set
    End Property
    Dim _IsOBHistory As Boolean = False

#End Region

#Region " C1 Constants 1 "
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

    Dim Col_Button As Integer = 10

    Dim Col_SomkingStatus As Integer = 11
    Dim Col_SmokingButton = 12
    ''
    Dim Col_Active As Integer = 13
    Dim Col_MedicalConditionID As Integer = 14

    Dim Col_Dosage As Integer = 15 '' Need For Allergy
    Dim Col_NDCCode As Integer = 16 '' Need For Allergy
    Dim Col_DOE_Allergy As Integer = 18

    Dim Col_ConceptId As Integer = 19 '' Need For Allergy
    Dim Col_DescId As Integer = 20 '' Need For Allergy
    Dim Col_SnoMedID As Integer = 21
    Dim Col_Description As Integer = 22

    Dim Col_ICD9 As Integer = 23
    Dim Col_RxNorm As Integer = 24

    Dim Col_COUNT As Integer = 25

    Private Col_Check As Integer = 0
    Private Col_Name As Integer = 1
    Private Col_DGCustCnt As Integer = 2

    Private Col_ICD9CPTCode As Integer = 0
    Private Col_ICD9CPTDEscription As Integer = 1
    Private Col_CustCnt As Integer = 2
    ''
#End Region

#Region " C1 Constants 2 "

    Dim Col_HCategory As Integer = 0
    Dim Col_HHidden As Integer = 1
    Dim Col_HsOBPlanItem As Integer = 2
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
    Dim Col_HnOBPlanID As Integer = 27

    Dim Col_HCOUNT As Integer = 32

    Dim Col_CREATEDBY As Integer = 35
    Dim Col_UPDATEBY As Integer = 36

    Const MAX_COMMENT_LENGHT As Integer = 760

#End Region

#Region "System Table For OBPlan Standard Types Constants"
    Dim Col_Standard_OBPlanType As Integer = 0
    Dim Col_Standard_bIsActive As Integer = 1
    Dim Col_Standard_bIsOnsetDate As Integer = 2
    Dim Col_Standard_sCCDSection As Integer = 3

#End Region

#Region "System Table For Catgeory Standard Types Constants"
    Dim Col_Category_sDescription As Integer = 0
    Dim Col_Category_sOBPlanType As Integer = 1
#End Region

#Region " Windows Controls "
    Friend WithEvents lblReviewed As System.Windows.Forms.Label
    Friend WithEvents tblOBPlan As gloToolStrip.gloToolStrip
    Friend WithEvents tblNew As System.Windows.Forms.ToolStripButton
    Friend WithEvents tblSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents tblClose As System.Windows.Forms.ToolStripButton
    Friend WithEvents pnlreview As System.Windows.Forms.Panel
    Friend WithEvents mnuAddReaction As System.Windows.Forms.MenuItem
    Friend WithEvents mnuEditOBPlanItem As System.Windows.Forms.MenuItem
    Private WithEvents Label27 As System.Windows.Forms.Label
    Private WithEvents Label18 As System.Windows.Forms.Label
    Private WithEvents Label21 As System.Windows.Forms.Label
    Private WithEvents Label14 As System.Windows.Forms.Label
    Private WithEvents Label15 As System.Windows.Forms.Label
    Private WithEvents Label16 As System.Windows.Forms.Label
    Private WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Splitter2 As System.Windows.Forms.Splitter
    Private WithEvents C1SuperTooltip1 As C1.Win.C1SuperTooltip.C1SuperTooltip
    Friend WithEvents GloUC_trvOBPlan As gloUserControlLibrary.gloUC_TreeView
    Friend WithEvents pnlTopButtons As System.Windows.Forms.Panel
    Friend WithEvents pnlBottomButtons As System.Windows.Forms.Panel
    Private WithEvents lblHeightMeter As System.Windows.Forms.Label
    Friend WithEvents PnlCustomTask As System.Windows.Forms.Panel
    Friend WithEvents cmbAllergyType As System.Windows.Forms.ComboBox
    Friend WithEvents Label29 As System.Windows.Forms.Label
    'System.Windows.Forms.TextBox
    Friend WithEvents pnlsnomadedetail As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents lblNDCid As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Private WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents lbldescid As System.Windows.Forms.Label
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents lblRxNorm As System.Windows.Forms.Label
    Friend WithEvents cntFindings As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents trvSubTypeDefinition As System.Windows.Forms.TreeView
    Friend WithEvents trvDefinitionSubTypetionSubTypetionSubTypetionSubTypetionSubtionSub As System.Windows.Forms.TreeView
    Friend WithEvents Splitter6 As System.Windows.Forms.Splitter
    Friend WithEvents Splitter7 As System.Windows.Forms.Splitter
    Friend WithEvents trvDefSubType As System.Windows.Forms.TreeView
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
    Friend WithEvents lblOBPlanTypelabel As System.Windows.Forms.Label
    Friend WithEvents lblOBPlanType As System.Windows.Forms.Label
    Friend WithEvents Label48 As System.Windows.Forms.Label
    Friend WithEvents Label43 As System.Windows.Forms.Label
    Private WithEvents Label53 As System.Windows.Forms.Label
    Friend WithEvents Panel8 As System.Windows.Forms.Panel
    Private WithEvents Label13 As System.Windows.Forms.Label
    Private WithEvents Label20 As System.Windows.Forms.Label
    Private WithEvents Label57 As System.Windows.Forms.Label
    Private WithEvents Label55 As System.Windows.Forms.Label
    Private WithEvents Label56 As System.Windows.Forms.Label
    Private WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Splitter3 As System.Windows.Forms.Splitter
    ''added to show snomed description 8020 changes
#End Region

#Region " Windows Form Designer generated code "

#Region " Constructor "

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

        _IsFrmImm = IsFrmImm
        _strAllergy = strAllergy
        _strConceptID = strConceptID
        _strSnoMedID = strSnoMedID
        _strDescriptionID = strDescriptionID
        _strICD9 = strICD9
        _strSnomedDefination = strSnomedDefination
        _RxNormID = strRxNormID
        _NDCCode = strNDCCode

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

#End Region

#Region " TO Check the Multiple instances Of Form "

    '' TO Keep track that the Form's Instance is Disposed or not
    Private blnDisposed As Boolean
    Private Shared frm As frmPatientOBPlan

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
                Dim dtpContextMenu As ContextMenu() = {ContextMenuC1OBPlan, ContextMenutrvPrevOBPlan, cntCategory}
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

        If (IsNothing(objCriteria) = False) Then
            objCriteria.Dispose()
            objCriteria = Nothing
        End If

        If IsNothing(objWord) = False Then
            objWord = Nothing
        End If

        If IsNothing(objclsPatientOBPlan) = False Then
            objclsPatientOBPlan.Dispose()
            objclsPatientOBPlan = Nothing
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
      
        _isMakeAsCurrent = Nothing
        _isMedicationHistoryModify = Nothing
        _isMessage = Nothing
        _isDoubleclickPrevHistory = Nothing
        IsCloseClickFlagForCommentValidation = Nothing
        bln_Loadcategory = Nothing
        isOBPlanModified = Nothing
        isOBPlanLoading = Nothing
        _isPanelClick = Nothing
        blnDIbtnClick = Nothing
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

        If IsNothing(oDiagnosisListControl) = False Then
            oDiagnosisListControl.Dispose() : oDiagnosisListControl = Nothing
        End If

        If IsNothing(oCPTListControl) = False Then
            oCPTListControl.Dispose() : oCPTListControl = Nothing
        End If


        BtnTag = Nothing
        m_VisitID = Nothing
        m_PatientID = Nothing
        _HxVisitID = Nothing
        FormLevelLockID = Nothing

        intCheck = Nothing
        btntype = Nothing
        m_VisitDate = Nothing


        arrDataDictionary.Clear()
        arrDataDictionary = Nothing

        strUncodedDrugs = Nothing

        If IsNothing(dgCustomGrid) = False Then
            dgCustomGrid.Dispose() : dgCustomGrid = Nothing
        End If

      
        objCriteria = Nothing

        If IsNothing(clsSplit_OBPlan) = False Then
            clsSplit_OBPlan.Dispose()
            clsSplit_OBPlan = Nothing
        End If

    End Sub

    Public Shared Function GetInstance(ByVal VisitID As Long, ByVal VisitDate As Date, ByVal PatientID As Long, Optional ByVal blnRecordLock As Boolean = False, Optional ByVal _RecordLock As Boolean = False) As frmPatientOBPlan
        '_mu.WaitOne()
        Try
            IsOpen = False
            ''If frm Is Nothing Then

            For Each f As Form In Application.OpenForms
                If f.Name = "frmPatientOBPlan" Then
                    If CType(f, frmPatientOBPlan).m_PatientID = PatientID And CType(f, frmPatientOBPlan).m_VisitID = VisitID Then
                        IsOpen = True
                        frm = f
                    End If

                End If
            Next
            If (IsOpen = False) Then
                frm = New frmPatientOBPlan(VisitID, VisitDate, PatientID, blnRecordLock, _RecordLock)
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
    Friend WithEvents ContextMenuC1OBPlan As System.Windows.Forms.ContextMenu
    Friend WithEvents mnuRemove As System.Windows.Forms.MenuItem
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents pnlCatBtn As System.Windows.Forms.Panel
    Friend WithEvents PnlRight As System.Windows.Forms.Panel
    Friend WithEvents pnlToolStrip As System.Windows.Forms.Panel
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
    Friend WithEvents ContextMenutrvPrevOBPlan As System.Windows.Forms.ContextMenu
    Friend WithEvents mnuDeleteOBPlan As System.Windows.Forms.MenuItem
    Friend WithEvents mnuMakeCurrent As System.Windows.Forms.MenuItem
    Friend WithEvents cntCategory As System.Windows.Forms.ContextMenu
    Friend WithEvents mnuAddOBPlanItem As System.Windows.Forms.MenuItem
    Friend WithEvents imgTreeVIew As System.Windows.Forms.ImageList
    Friend WithEvents ImgPatientOBPlan1 As System.Windows.Forms.ImageList
    Friend WithEvents C1PatientOBPlan As C1.Win.C1FlexGrid.C1FlexGrid

    Private Sub InitializeToolStrip()
        tblOBPlan.ConnectionString = GetConnectionString()
        tblOBPlan.ModuleName = Me.Name
        tblOBPlan.UserID = gnLoginID
        '  tblOBPlan.ButtonsToHide.Add(tblNew.Name)
        'tblHistory.ButtonsToHide.Add(tsbtn_Export.Name)
        'tblHistory.ButtonsToHide.Add(tsbtn_Delete.Name)
    End Sub
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPatientOBPlan))
        Me.pnlOuter = New System.Windows.Forms.Panel()
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.PnlRight = New System.Windows.Forms.Panel()
        Me.pnltrvTarget = New System.Windows.Forms.Panel()
        Me.Panel8 = New System.Windows.Forms.Panel()
        Me.C1PatientOBPlan = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label53 = New System.Windows.Forms.Label()
        Me.pnlsnomadedetail = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.btn_ICD9Code = New System.Windows.Forms.Button()
        Me.btnConceptID = New System.Windows.Forms.Button()
        Me.btn_CPTCode = New System.Windows.Forms.Button()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label57 = New System.Windows.Forms.Label()
        Me.lblOBPlanTypelabel = New System.Windows.Forms.Label()
        Me.lblOBPlanType = New System.Windows.Forms.Label()
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
        Me.Label18 = New System.Windows.Forms.Label()
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
        Me.Splitter2 = New System.Windows.Forms.Splitter()
        Me.Splitter1 = New System.Windows.Forms.Splitter()
        Me.pnlCatBtn = New System.Windows.Forms.Panel()
        Me.pnltrvSource = New System.Windows.Forms.Panel()
        Me.Splitter7 = New System.Windows.Forms.Splitter()
        Me.Splitter8 = New System.Windows.Forms.Splitter()
        Me.GloUC_trvOBPlan = New gloUserControlLibrary.gloUC_TreeView()
        Me.imgTreeVIew = New System.Windows.Forms.ImageList(Me.components)
        Me.pnlBottomButtons = New System.Windows.Forms.Panel()
        Me.lblHeightMeter = New System.Windows.Forms.Label()
        Me.pnlTopButtons = New System.Windows.Forms.Panel()
        Me.ImgPatientOBPlan1 = New System.Windows.Forms.ImageList(Me.components)
        Me.pnlToolStrip = New System.Windows.Forms.Panel()
        Me.tblOBPlan = New gloToolStrip.gloToolStrip()
        Me.tblSave = New System.Windows.Forms.ToolStripButton()
        Me.tblClose = New System.Windows.Forms.ToolStripButton()
        Me.tblNew = New System.Windows.Forms.ToolStripButton()
        Me.ContextMenuC1OBPlan = New System.Windows.Forms.ContextMenu()
        Me.mnuRemove = New System.Windows.Forms.MenuItem()
        Me.mnuAddReaction = New System.Windows.Forms.MenuItem()
        Me.ContextMenutrvPrevOBPlan = New System.Windows.Forms.ContextMenu()
        Me.mnuDeleteOBPlan = New System.Windows.Forms.MenuItem()
        Me.mnuMakeCurrent = New System.Windows.Forms.MenuItem()
        Me.cntCategory = New System.Windows.Forms.ContextMenu()
        Me.mnuAddOBPlanItem = New System.Windows.Forms.MenuItem()
        Me.mnuEditOBPlanItem = New System.Windows.Forms.MenuItem()
        Me.C1SuperTooltip1 = New C1.Win.C1SuperTooltip.C1SuperTooltip(Me.components)
        Me.cntFindings = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mnu_AddFindings = New System.Windows.Forms.ToolStripMenuItem()
        Me.pnlOuter.SuspendLayout()
        Me.pnlMain.SuspendLayout()
        Me.PnlRight.SuspendLayout()
        Me.pnltrvTarget.SuspendLayout()
        Me.Panel8.SuspendLayout()
        CType(Me.C1PatientOBPlan, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlsnomadedetail.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel7.SuspendLayout()
        Me.Panel6.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.pnlreview.SuspendLayout()
        Me.pnlPatientHeader.SuspendLayout()
        Me.pnlCatBtn.SuspendLayout()
        Me.pnltrvSource.SuspendLayout()
        Me.pnlBottomButtons.SuspendLayout()
        Me.pnlToolStrip.SuspendLayout()
        Me.tblOBPlan.SuspendLayout()
        Me.cntFindings.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlOuter
        '
        Me.pnlOuter.Controls.Add(Me.pnlMain)
        Me.pnlOuter.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlOuter.Location = New System.Drawing.Point(0, 0)
        Me.pnlOuter.Name = "pnlOuter"
        Me.pnlOuter.Size = New System.Drawing.Size(1240, 874)
        Me.pnlOuter.TabIndex = 0
        '
        'pnlMain
        '
        Me.pnlMain.Controls.Add(Me.PnlRight)
        Me.pnlMain.Controls.Add(Me.Splitter2)
        Me.pnlMain.Controls.Add(Me.Splitter1)
        Me.pnlMain.Controls.Add(Me.pnlCatBtn)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(0, 0)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(1240, 874)
        Me.pnlMain.TabIndex = 1
        '
        'PnlRight
        '
        Me.PnlRight.BackColor = System.Drawing.Color.Transparent
        Me.PnlRight.Controls.Add(Me.pnltrvTarget)
        Me.PnlRight.Controls.Add(Me.pnlPatientHeader)
        Me.PnlRight.Controls.Add(Me.Splitter3)
        Me.PnlRight.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PnlRight.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PnlRight.Location = New System.Drawing.Point(214, 0)
        Me.PnlRight.Name = "PnlRight"
        Me.PnlRight.Size = New System.Drawing.Size(1022, 874)
        Me.PnlRight.TabIndex = 1
        '
        'pnltrvTarget
        '
        Me.pnltrvTarget.Controls.Add(Me.Panel8)
        Me.pnltrvTarget.Controls.Add(Me.Label20)
        Me.pnltrvTarget.Controls.Add(Me.Label53)
        Me.pnltrvTarget.Controls.Add(Me.pnlsnomadedetail)
        Me.pnltrvTarget.Controls.Add(Me.PnlCustomTask)
        Me.pnltrvTarget.Controls.Add(Me.pnlreview)
        Me.pnltrvTarget.Controls.Add(Me.Label18)
        Me.pnltrvTarget.Controls.Add(Me.Label21)
        Me.pnltrvTarget.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnltrvTarget.Location = New System.Drawing.Point(0, 53)
        Me.pnltrvTarget.Name = "pnltrvTarget"
        Me.pnltrvTarget.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.pnltrvTarget.Size = New System.Drawing.Size(1022, 818)
        Me.pnltrvTarget.TabIndex = 0
        '
        'Panel8
        '
        Me.Panel8.Controls.Add(Me.C1PatientOBPlan)
        Me.Panel8.Controls.Add(Me.Label13)
        Me.Panel8.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel8.Location = New System.Drawing.Point(1, 33)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(1020, 784)
        Me.Panel8.TabIndex = 60
        '
        'C1PatientOBPlan
        '
        Me.C1PatientOBPlan.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.C1PatientOBPlan.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None
        Me.C1PatientOBPlan.AutoGenerateColumns = False
        Me.C1PatientOBPlan.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.C1PatientOBPlan.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.C1PatientOBPlan.ColumnInfo = resources.GetString("C1PatientOBPlan.ColumnInfo")
        Me.C1PatientOBPlan.DataMember = "OBPlan"
        Me.C1PatientOBPlan.Dock = System.Windows.Forms.DockStyle.Fill
        Me.C1PatientOBPlan.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.C1PatientOBPlan.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.C1PatientOBPlan.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross
        Me.C1PatientOBPlan.Location = New System.Drawing.Point(0, 1)
        Me.C1PatientOBPlan.Name = "C1PatientOBPlan"
        Me.C1PatientOBPlan.Rows.Count = 13
        Me.C1PatientOBPlan.Rows.DefaultSize = 19
        Me.C1PatientOBPlan.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.C1PatientOBPlan.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        Me.C1PatientOBPlan.ShowCellLabels = True
        Me.C1PatientOBPlan.ShowSortPosition = C1.Win.C1FlexGrid.ShowSortPositionEnum.None
        Me.C1PatientOBPlan.Size = New System.Drawing.Size(1020, 783)
        Me.C1PatientOBPlan.StyleInfo = resources.GetString("C1PatientOBPlan.StyleInfo")
        Me.C1PatientOBPlan.TabIndex = 0
        Me.C1PatientOBPlan.Tree.NodeImageCollapsed = CType(resources.GetObject("C1PatientOBPlan.Tree.NodeImageCollapsed"), System.Drawing.Image)
        Me.C1PatientOBPlan.Tree.NodeImageExpanded = CType(resources.GetObject("C1PatientOBPlan.Tree.NodeImageExpanded"), System.Drawing.Image)
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label13.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label13.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label13.Location = New System.Drawing.Point(0, 0)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(1020, 1)
        Me.Label13.TabIndex = 13
        Me.Label13.Text = "label2"
        '
        'Label20
        '
        Me.Label20.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label20.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label20.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label20.Location = New System.Drawing.Point(1021, 33)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(1, 784)
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
        Me.Label53.Size = New System.Drawing.Size(1, 784)
        Me.Label53.TabIndex = 17
        Me.Label53.Text = "label4"
        '
        'pnlsnomadedetail
        '
        Me.pnlsnomadedetail.Controls.Add(Me.Panel2)
        Me.pnlsnomadedetail.Location = New System.Drawing.Point(0, 558)
        Me.pnlsnomadedetail.Name = "pnlsnomadedetail"
        Me.pnlsnomadedetail.Size = New System.Drawing.Size(792, 75)
        Me.pnlsnomadedetail.TabIndex = 12
        Me.pnlsnomadedetail.Visible = False
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.btn_ICD9Code)
        Me.Panel2.Controls.Add(Me.btnConceptID)
        Me.Panel2.Controls.Add(Me.btn_CPTCode)
        Me.Panel2.Controls.Add(Me.Label19)
        Me.Panel2.Controls.Add(Me.Label57)
        Me.Panel2.Controls.Add(Me.lblOBPlanTypelabel)
        Me.Panel2.Controls.Add(Me.lblOBPlanType)
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
        Me.Panel2.Size = New System.Drawing.Size(792, 75)
        Me.Panel2.TabIndex = 1
        Me.Panel2.Visible = False
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
        Me.btn_ICD9Code.Location = New System.Drawing.Point(281, 12)
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
        Me.btnConceptID.Location = New System.Drawing.Point(554, 41)
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
        Me.btn_CPTCode.Location = New System.Drawing.Point(554, 11)
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
        Me.Label19.Size = New System.Drawing.Size(1, 71)
        Me.Label19.TabIndex = 217
        Me.Label19.Text = "label4"
        '
        'Label57
        '
        Me.Label57.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label57.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label57.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label57.Location = New System.Drawing.Point(791, 4)
        Me.Label57.Name = "Label57"
        Me.Label57.Size = New System.Drawing.Size(1, 71)
        Me.Label57.TabIndex = 216
        Me.Label57.Text = "label3"
        '
        'lblOBPlanTypelabel
        '
        Me.lblOBPlanTypelabel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblOBPlanTypelabel.AutoSize = True
        Me.lblOBPlanTypelabel.Location = New System.Drawing.Point(596, 18)
        Me.lblOBPlanTypelabel.Name = "lblOBPlanTypelabel"
        Me.lblOBPlanTypelabel.Size = New System.Drawing.Size(85, 14)
        Me.lblOBPlanTypelabel.TabIndex = 215
        Me.lblOBPlanTypelabel.Text = "OBPlan Type :"
        '
        'lblOBPlanType
        '
        Me.lblOBPlanType.AutoEllipsis = True
        Me.lblOBPlanType.BackColor = System.Drawing.Color.Transparent
        Me.lblOBPlanType.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblOBPlanType.Location = New System.Drawing.Point(686, 18)
        Me.lblOBPlanType.Name = "lblOBPlanType"
        Me.lblOBPlanType.Size = New System.Drawing.Size(122, 14)
        Me.lblOBPlanType.TabIndex = 214
        '
        'Panel7
        '
        Me.Panel7.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.Panel7.Controls.Add(Me.lnlLbllCPTCode)
        Me.Panel7.Controls.Add(Me.Label49)
        Me.Panel7.Controls.Add(Me.Label50)
        Me.Panel7.Controls.Add(Me.Label51)
        Me.Panel7.Controls.Add(Me.Label52)
        Me.Panel7.Location = New System.Drawing.Point(376, 12)
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
        Me.Panel6.Location = New System.Drawing.Point(90, 12)
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
        Me.Panel5.Location = New System.Drawing.Point(90, 42)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(460, 23)
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
        Me.lblconcptid.Size = New System.Drawing.Size(458, 21)
        Me.lblconcptid.TabIndex = 209
        '
        'Label42
        '
        Me.Label42.BackColor = System.Drawing.Color.FromArgb(CType(CType(171, Byte), Integer), CType(CType(173, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.Label42.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label42.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label42.Location = New System.Drawing.Point(459, 1)
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
        Me.Label40.Size = New System.Drawing.Size(460, 1)
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
        Me.Label38.Size = New System.Drawing.Size(460, 1)
        Me.Label38.TabIndex = 12
        Me.Label38.Text = "label2"
        '
        'Label39
        '
        Me.Label39.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label39.AutoSize = True
        Me.Label39.Location = New System.Drawing.Point(336, 16)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(37, 14)
        Me.Label39.TabIndex = 21
        Me.Label39.Text = "CPT :"
        '
        'Label37
        '
        Me.Label37.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label37.Location = New System.Drawing.Point(21, 16)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(68, 14)
        Me.Label37.TabIndex = 19
        Me.Label37.Text = "ICD9/10 :"
        Me.Label37.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblNDCid
        '
        Me.lblNDCid.AutoEllipsis = True
        Me.lblNDCid.BackColor = System.Drawing.Color.Transparent
        Me.lblNDCid.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNDCid.Location = New System.Drawing.Point(901, 45)
        Me.lblNDCid.Name = "lblNDCid"
        Me.lblNDCid.Size = New System.Drawing.Size(122, 14)
        Me.lblNDCid.TabIndex = 16
        '
        'Label10
        '
        Me.Label10.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(829, 45)
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
        Me.Label12.Size = New System.Drawing.Size(792, 1)
        Me.Label12.TabIndex = 12
        Me.Label12.Text = "label2"
        '
        'Label28
        '
        Me.Label28.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label28.Location = New System.Drawing.Point(21, 46)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(68, 14)
        Me.Label28.TabIndex = 0
        Me.Label28.Text = "SNOMED :"
        Me.Label28.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lbldescid
        '
        Me.lbldescid.AutoEllipsis = True
        Me.lbldescid.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbldescid.Location = New System.Drawing.Point(1, 26)
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
        Me.Label30.Location = New System.Drawing.Point(607, 45)
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
        Me.lblRxNorm.Location = New System.Drawing.Point(686, 45)
        Me.lblRxNorm.Name = "lblRxNorm"
        Me.lblRxNorm.Size = New System.Drawing.Size(122, 14)
        Me.lblRxNorm.TabIndex = 5
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
        Me.pnlreview.Size = New System.Drawing.Size(1022, 29)
        Me.pnlreview.TabIndex = 6
        '
        'lblReviewed
        '
        Me.lblReviewed.AutoEllipsis = True
        Me.lblReviewed.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblReviewed.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblReviewed.ImageAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.lblReviewed.Location = New System.Drawing.Point(1, 2)
        Me.lblReviewed.Name = "lblReviewed"
        Me.lblReviewed.Padding = New System.Windows.Forms.Padding(0, 4, 0, 0)
        Me.lblReviewed.Size = New System.Drawing.Size(626, 24)
        Me.lblReviewed.TabIndex = 17
        Me.lblReviewed.Text = "  Reviewed "
        Me.lblReviewed.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label43
        '
        Me.Label43.AutoSize = True
        Me.Label43.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label43.Location = New System.Drawing.Point(627, 2)
        Me.Label43.Name = "Label43"
        Me.Label43.Padding = New System.Windows.Forms.Padding(0, 5, 0, 0)
        Me.Label43.Size = New System.Drawing.Size(70, 19)
        Me.Label43.TabIndex = 210
        Me.Label43.Text = "Move Up :"
        '
        'btnUp
        '
        Me.btnUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnUp.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnUp.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnUp.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnUp.FlatAppearance.BorderSize = 0
        Me.btnUp.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnUp.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnUp.Image = CType(resources.GetObject("btnUp.Image"), System.Drawing.Image)
        Me.btnUp.Location = New System.Drawing.Point(697, 2)
        Me.btnUp.Name = "btnUp"
        Me.btnUp.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.btnUp.Size = New System.Drawing.Size(25, 24)
        Me.btnUp.TabIndex = 208
        Me.btnUp.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnUp.UseVisualStyleBackColor = False
        '
        'Label48
        '
        Me.Label48.AutoSize = True
        Me.Label48.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label48.Location = New System.Drawing.Point(722, 2)
        Me.Label48.Name = "Label48"
        Me.Label48.Padding = New System.Windows.Forms.Padding(0, 5, 0, 0)
        Me.Label48.Size = New System.Drawing.Size(89, 19)
        Me.Label48.TabIndex = 211
        Me.Label48.Text = "Move Down :"
        '
        'btnDown
        '
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
        Me.btnDown.Location = New System.Drawing.Point(811, 2)
        Me.btnDown.Name = "btnDown"
        Me.btnDown.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.btnDown.Size = New System.Drawing.Size(25, 24)
        Me.btnDown.TabIndex = 209
        Me.btnDown.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnDown.UseVisualStyleBackColor = False
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label29.Location = New System.Drawing.Point(836, 2)
        Me.Label29.Name = "Label29"
        Me.Label29.Padding = New System.Windows.Forms.Padding(0, 5, 0, 0)
        Me.Label29.Size = New System.Drawing.Size(57, 19)
        Me.Label29.TabIndex = 21
        Me.Label29.Text = "Status :"
        '
        'cmbAllergyType
        '
        Me.cmbAllergyType.Dock = System.Windows.Forms.DockStyle.Right
        Me.cmbAllergyType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbAllergyType.FormattingEnabled = True
        Me.cmbAllergyType.Location = New System.Drawing.Point(893, 2)
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
        Me.Label27.Size = New System.Drawing.Size(1020, 1)
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
        Me.Label56.Location = New System.Drawing.Point(1021, 2)
        Me.Label56.Name = "Label56"
        Me.Label56.Size = New System.Drawing.Size(1, 25)
        Me.Label56.TabIndex = 213
        Me.Label56.Text = "label3"
        '
        'Label18
        '
        Me.Label18.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label18.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label18.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label18.Location = New System.Drawing.Point(0, 817)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(1022, 1)
        Me.Label18.TabIndex = 10
        Me.Label18.Text = "label2"
        '
        'Label21
        '
        Me.Label21.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label21.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label21.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.Location = New System.Drawing.Point(0, 3)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(1022, 1)
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
        Me.pnlPatientHeader.Size = New System.Drawing.Size(1022, 53)
        Me.pnlPatientHeader.TabIndex = 23
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label14.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label14.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label14.Location = New System.Drawing.Point(1, 49)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(1020, 1)
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
        Me.Label16.Location = New System.Drawing.Point(1021, 4)
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
        Me.Label17.Size = New System.Drawing.Size(1022, 1)
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
        Me.btnPrevHistory.Location = New System.Drawing.Point(916, 23)
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
        Me.lblVisitID.Location = New System.Drawing.Point(913, 9)
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
        Me.Splitter3.Location = New System.Drawing.Point(0, 871)
        Me.Splitter3.Name = "Splitter3"
        Me.Splitter3.Size = New System.Drawing.Size(1022, 3)
        Me.Splitter3.TabIndex = 21
        Me.Splitter3.TabStop = False
        '
        'Splitter2
        '
        Me.Splitter2.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Splitter2.Dock = System.Windows.Forms.DockStyle.Right
        Me.Splitter2.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Splitter2.Location = New System.Drawing.Point(1236, 0)
        Me.Splitter2.Name = "Splitter2"
        Me.Splitter2.Size = New System.Drawing.Size(4, 874)
        Me.Splitter2.TabIndex = 14
        Me.Splitter2.TabStop = False
        '
        'Splitter1
        '
        Me.Splitter1.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Splitter1.Location = New System.Drawing.Point(210, 0)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(4, 874)
        Me.Splitter1.TabIndex = 1
        Me.Splitter1.TabStop = False
        '
        'pnlCatBtn
        '
        Me.pnlCatBtn.AutoScroll = True
        Me.pnlCatBtn.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlCatBtn.Controls.Add(Me.pnltrvSource)
        Me.pnlCatBtn.Controls.Add(Me.pnlBottomButtons)
        Me.pnlCatBtn.Controls.Add(Me.pnlTopButtons)
        Me.pnlCatBtn.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlCatBtn.Location = New System.Drawing.Point(0, 0)
        Me.pnlCatBtn.Name = "pnlCatBtn"
        Me.pnlCatBtn.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.pnlCatBtn.Size = New System.Drawing.Size(210, 874)
        Me.pnlCatBtn.TabIndex = 0
        '
        'pnltrvSource
        '
        Me.pnltrvSource.BackColor = System.Drawing.Color.Transparent
        Me.pnltrvSource.Controls.Add(Me.Splitter7)
        Me.pnltrvSource.Controls.Add(Me.Splitter8)
        Me.pnltrvSource.Controls.Add(Me.GloUC_trvOBPlan)
        Me.pnltrvSource.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnltrvSource.Location = New System.Drawing.Point(0, 31)
        Me.pnltrvSource.Name = "pnltrvSource"
        Me.pnltrvSource.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.pnltrvSource.Size = New System.Drawing.Size(210, 703)
        Me.pnltrvSource.TabIndex = 18
        '
        'Splitter7
        '
        Me.Splitter7.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Splitter7.Dock = System.Windows.Forms.DockStyle.Top
        Me.Splitter7.Location = New System.Drawing.Point(3, 0)
        Me.Splitter7.Name = "Splitter7"
        Me.Splitter7.Size = New System.Drawing.Size(207, 3)
        Me.Splitter7.TabIndex = 58
        Me.Splitter7.TabStop = False
        '
        'Splitter8
        '
        Me.Splitter8.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Splitter8.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Splitter8.Location = New System.Drawing.Point(3, 700)
        Me.Splitter8.Name = "Splitter8"
        Me.Splitter8.Size = New System.Drawing.Size(207, 3)
        Me.Splitter8.TabIndex = 60
        Me.Splitter8.TabStop = False
        '
        'GloUC_trvOBPlan
        '
        Me.GloUC_trvOBPlan.BackColor = System.Drawing.Color.White
        Me.GloUC_trvOBPlan.CheckBoxes = False
        Me.GloUC_trvOBPlan.CodeMember = Nothing
        Me.GloUC_trvOBPlan.ColonAsSeparator = False
        Me.GloUC_trvOBPlan.Comment = Nothing
        Me.GloUC_trvOBPlan.ConceptID = Nothing
        Me.GloUC_trvOBPlan.CPT = Nothing
        Me.GloUC_trvOBPlan.DescriptionMember = Nothing
        Me.GloUC_trvOBPlan.DisplayContextMenuStrip = Nothing
        Me.GloUC_trvOBPlan.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Code_Description
        Me.GloUC_trvOBPlan.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GloUC_trvOBPlan.DrugFlag = CType(16, Short)
        Me.GloUC_trvOBPlan.DrugFormMember = Nothing
        Me.GloUC_trvOBPlan.DrugQtyQualifierMember = Nothing
        Me.GloUC_trvOBPlan.DurationMember = Nothing
        Me.GloUC_trvOBPlan.EducationMappingSearchType = 1
        Me.GloUC_trvOBPlan.FrequencyMember = Nothing
        Me.GloUC_trvOBPlan.HistoryType = Nothing
        Me.GloUC_trvOBPlan.ICD9 = Nothing
        Me.GloUC_trvOBPlan.ICDRevision = Nothing
        Me.GloUC_trvOBPlan.ImageIndex = 4
        Me.GloUC_trvOBPlan.ImageList = Me.imgTreeVIew
        Me.GloUC_trvOBPlan.ImageObject = Nothing
        Me.GloUC_trvOBPlan.Indicator = Nothing
        Me.GloUC_trvOBPlan.IsCPTSearch = False
        Me.GloUC_trvOBPlan.IsDiagnosisSearch = False
        Me.GloUC_trvOBPlan.IsDrug = False
        Me.GloUC_trvOBPlan.IsNarcoticsMember = Nothing
        Me.GloUC_trvOBPlan.IsSearchForEducationMapping = False
        Me.GloUC_trvOBPlan.IsSystemCategory = Nothing
        Me.GloUC_trvOBPlan.Location = New System.Drawing.Point(3, 0)
        Me.GloUC_trvOBPlan.MaximumNodes = 1000
        Me.GloUC_trvOBPlan.Name = "GloUC_trvOBPlan"
        Me.GloUC_trvOBPlan.NDCCodeMember = Nothing
        Me.GloUC_trvOBPlan.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.GloUC_trvOBPlan.ParentImageIndex = 0
        Me.GloUC_trvOBPlan.ParentMember = Nothing
        Me.GloUC_trvOBPlan.RouteMember = Nothing
        Me.GloUC_trvOBPlan.RowOrderMember = Nothing
        Me.GloUC_trvOBPlan.Search = gloUserControlLibrary.gloUC_TreeView.enumSearchType.Instring
        Me.GloUC_trvOBPlan.SearchBox = True
        Me.GloUC_trvOBPlan.SearchText = Nothing
        Me.GloUC_trvOBPlan.SelectedImageIndex = 4
        Me.GloUC_trvOBPlan.SelectedNode = Nothing
        Me.GloUC_trvOBPlan.SelectedNodeIDs = CType(resources.GetObject("GloUC_trvOBPlan.SelectedNodeIDs"), System.Collections.ArrayList)
        Me.GloUC_trvOBPlan.SelectedParentImageIndex = 0
        Me.GloUC_trvOBPlan.Size = New System.Drawing.Size(207, 703)
        Me.GloUC_trvOBPlan.SmartTreatmentId = Nothing
        Me.GloUC_trvOBPlan.Sort = gloUserControlLibrary.gloUC_TreeView.enumSortType.ByDescription
        Me.GloUC_trvOBPlan.TabIndex = 49
        Me.GloUC_trvOBPlan.Tag = Nothing
        Me.GloUC_trvOBPlan.UnitMember = Nothing
        Me.GloUC_trvOBPlan.ValueMember = Nothing
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
        'pnlBottomButtons
        '
        Me.pnlBottomButtons.AutoScroll = True
        Me.pnlBottomButtons.Controls.Add(Me.lblHeightMeter)
        Me.pnlBottomButtons.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlBottomButtons.Location = New System.Drawing.Point(0, 734)
        Me.pnlBottomButtons.Name = "pnlBottomButtons"
        Me.pnlBottomButtons.Padding = New System.Windows.Forms.Padding(3, 0, 0, 3)
        Me.pnlBottomButtons.Size = New System.Drawing.Size(210, 140)
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
        Me.pnlTopButtons.Size = New System.Drawing.Size(210, 28)
        Me.pnlTopButtons.TabIndex = 51
        '
        'ImgPatientOBPlan1
        '
        Me.ImgPatientOBPlan1.ImageStream = CType(resources.GetObject("ImgPatientOBPlan1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImgPatientOBPlan1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImgPatientOBPlan1.Images.SetKeyName(0, "Patient History.ico")
        Me.ImgPatientOBPlan1.Images.SetKeyName(1, "Bullet06.ico")
        Me.ImgPatientOBPlan1.Images.SetKeyName(2, "")
        Me.ImgPatientOBPlan1.Images.SetKeyName(3, "")
        Me.ImgPatientOBPlan1.Images.SetKeyName(4, "")
        Me.ImgPatientOBPlan1.Images.SetKeyName(5, "Current.ico")
        Me.ImgPatientOBPlan1.Images.SetKeyName(6, "Current_Disable.ico")
        Me.ImgPatientOBPlan1.Images.SetKeyName(7, "Yesterdays.ico")
        Me.ImgPatientOBPlan1.Images.SetKeyName(8, "Yesterdays_Disable.ico")
        Me.ImgPatientOBPlan1.Images.SetKeyName(9, "Last Week.ico")
        Me.ImgPatientOBPlan1.Images.SetKeyName(10, "Last Week_Disable.ico")
        Me.ImgPatientOBPlan1.Images.SetKeyName(11, "LastMonth.ico")
        Me.ImgPatientOBPlan1.Images.SetKeyName(12, "LastMonth_Disable.ico")
        Me.ImgPatientOBPlan1.Images.SetKeyName(13, "Older.ico")
        Me.ImgPatientOBPlan1.Images.SetKeyName(14, "Older_Disable.ico")
        Me.ImgPatientOBPlan1.Images.SetKeyName(15, "date.ico")
        Me.ImgPatientOBPlan1.Images.SetKeyName(16, "Small Arrow.ico")
        '
        'pnlToolStrip
        '
        Me.pnlToolStrip.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlToolStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlToolStrip.Controls.Add(Me.tblOBPlan)
        Me.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlToolStrip.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.pnlToolStrip.Name = "pnlToolStrip"
        Me.pnlToolStrip.Size = New System.Drawing.Size(1240, 56)
        Me.pnlToolStrip.TabIndex = 0
        '
        'tblOBPlan
        '
        Me.tblOBPlan.AddSeparatorsBetweenEachButton = False
        Me.tblOBPlan.BackColor = System.Drawing.Color.Transparent
        Me.tblOBPlan.BackgroundImage = CType(resources.GetObject("tblOBPlan.BackgroundImage"), System.Drawing.Image)
        Me.tblOBPlan.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tblOBPlan.ButtonsToHide = Nothing
        Me.tblOBPlan.ConnectionString = Nothing
        Me.tblOBPlan.CustomizeButtonNameType = gloToolStrip.gloToolStrip.enumButtonNameType.ShowToolTipText
        Me.tblOBPlan.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblOBPlan.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.tblOBPlan.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tblOBPlan.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tblSave, Me.tblClose})
        Me.tblOBPlan.Location = New System.Drawing.Point(0, 0)
        Me.tblOBPlan.ModuleName = Nothing
        Me.tblOBPlan.Name = "tblOBPlan"
        Me.tblOBPlan.Size = New System.Drawing.Size(1240, 53)
        Me.tblOBPlan.TabIndex = 1
        Me.tblOBPlan.Text = "ToolStrip1"
        Me.tblOBPlan.UserID = CType(0, Long)
        '
        'tblSave
        '
        Me.tblSave.BackColor = System.Drawing.Color.Transparent
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
        'tblNew
        '
        Me.tblNew.BackColor = System.Drawing.Color.Transparent
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
        'ContextMenuC1OBPlan
        '
        Me.ContextMenuC1OBPlan.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuRemove, Me.mnuAddReaction})
        '
        'mnuRemove
        '
        Me.mnuRemove.Index = 0
        Me.mnuRemove.Text = "Remove OB Plan"
        '
        'mnuAddReaction
        '
        Me.mnuAddReaction.Index = 1
        Me.mnuAddReaction.Text = "Add Reaction"
        '
        'ContextMenutrvPrevOBPlan
        '
        Me.ContextMenutrvPrevOBPlan.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuDeleteOBPlan, Me.mnuMakeCurrent})
        '
        'mnuDeleteOBPlan
        '
        Me.mnuDeleteOBPlan.Index = 0
        Me.mnuDeleteOBPlan.Text = "&Delete OB Plan"
        '
        'mnuMakeCurrent
        '
        Me.mnuMakeCurrent.Index = 1
        Me.mnuMakeCurrent.Text = "Make as Current OB Plan"
        '
        'cntCategory
        '
        Me.cntCategory.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuAddOBPlanItem, Me.mnuEditOBPlanItem})
        '
        'mnuAddOBPlanItem
        '
        Me.mnuAddOBPlanItem.Index = 0
        Me.mnuAddOBPlanItem.Text = "Add OB Plan Item"
        '
        'mnuEditOBPlanItem
        '
        Me.mnuEditOBPlanItem.Index = 1
        Me.mnuEditOBPlanItem.Text = "Edit OB Plan Item"
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
        'frmPatientOBPlan
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(1240, 874)
        Me.Controls.Add(Me.pnlToolStrip)
        Me.Controls.Add(Me.pnlOuter)
        Me.DoubleBuffered = True
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmPatientOBPlan"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "OB Plan"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.pnlOuter.ResumeLayout(False)
        Me.pnlMain.ResumeLayout(False)
        Me.PnlRight.ResumeLayout(False)
        Me.pnltrvTarget.ResumeLayout(False)
        Me.Panel8.ResumeLayout(False)
        CType(Me.C1PatientOBPlan, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlsnomadedetail.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel7.ResumeLayout(False)
        Me.Panel6.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        Me.pnlreview.ResumeLayout(False)
        Me.pnlreview.PerformLayout()
        Me.pnlPatientHeader.ResumeLayout(False)
        Me.pnlPatientHeader.PerformLayout()
        Me.pnlCatBtn.ResumeLayout(False)
        Me.pnltrvSource.ResumeLayout(False)
        Me.pnlBottomButtons.ResumeLayout(False)
        Me.pnlToolStrip.ResumeLayout(False)
        Me.pnlToolStrip.PerformLayout()
        Me.tblOBPlan.ResumeLayout(False)
        Me.tblOBPlan.PerformLayout()
        Me.cntFindings.ResumeLayout(False)
        Me.ResumeLayout(False)

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
            .ShowDetail(m_PatientID, gloUserControlLibrary.gloUC_PatientStrip.enumFormName.OBPlan)
            .BringToFront()
            .DTPValue = Format(m_VisitDate, "MM/dd/yyyy")
            .DTPFormat = DateTimePickerFormat.Short
            .DTPEnabled = False
        End With
        pnlOuter.Controls.Add(gloUC_PatientStrip1)
        pnlMain.BringToFront()
        C1PatientOBPlan.BringToFront()
        pnlPatientHeader.Visible = False
    End Sub

#End Region

#Region " Form Events "

    Private Sub frmPatientOBPlan_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Try
            If Not IsNothing(Me.ParentForm) Then
                CType(Me.ParentForm, MainMenu).SetGnPatientID = GetCurrentPatientID
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.OBPlan, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try

        If Not IsNothing(uiPanSplitScreen_OBPlan) Then
            If Not IsNothing(uiPanSplitScreen_OBPlan.Parent) Then
                If uiPanSplitScreen_OBPlan.Parent.Text = "Split Screen" Then
                    uiPanSplitScreen_OBPlan.Parent.Visible = True
                ElseIf uiPanSplitScreen_OBPlan.Text = "Split Screen" Then
                    uiPanSplitScreen_OBPlan.Visible = True
                End If

            End If

        End If
    End Sub

    Private Sub frmPatientOBPlan_Deactivate(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Deactivate

        If Not IsNothing(Me.Parent) Then
            If Not IsNothing(uiPanSplitScreen_OBPlan) Then
                If Not IsNothing(uiPanSplitScreen_OBPlan.Parent) Then
                    If uiPanSplitScreen_OBPlan.Parent IsNot Me Then
                        uiPanSplitScreen_OBPlan.Parent.Visible = False
                        uiPanSplitScreen_OBPlan.Parent.Hide()
                        uiPanSplitScreen_OBPlan.Parent.Update()
                    End If
                End If
            End If
        End If

    End Sub

    Private Sub frmPatientOBPlan_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        Try

            If blnOpenFromExam = True Then
                If Not IsNothing(myCaller) Then
                    myCaller.GetdataFromOtherForms(gloEMRWord.enumDocType.OBPlan)
                   

                    myCaller.FillHistoryPanel()

                End If
                If Not IsNothing(myLetter) Then
                    myLetter.GetdataFromOtherForms(gloEMRWord.enumDocType.OBPlan)
                 
                End If
                blnOpenFromExam = False
            End If


            If Not IsNothing(myCaller1) Then
                myCaller1.GetdataFromOtherForms(gloEMRWord.enumDocType.OBPlan)
              
            End If

            If _blnRecordLock = False Then

                UnLock_Transaction(TrnType.PatientOBPlan, m_PatientID, m_VisitID, m_VisitDate)

            End If
            'Change made to solve memory Leak and word crash issue
            If Not objclsPatientOBPlan Is Nothing Then
                objclsPatientOBPlan = Nothing
            End If
            
            ' Me.Close()
            If IsNothing(mydtButton) = False Then
                mydtButton.Dispose()
                mydtButton = Nothing
            End If
            If IsNothing(dsOBPlan) = False Then
                dsOBPlan.Dispose()
                dsOBPlan = Nothing
            End If


            If Not IsNothing(gloUC_PatientStrip1) Then
                gloUC_PatientStrip1.Dispose()
                gloUC_PatientStrip1 = Nothing
            End If


            ToolTipbtn_CPTCode.RemoveAll()
            ToolTipbtn_ICD9Code.RemoveAll()
            ToolTipbtnConceptID.RemoveAll()
            ToolTipbtnUp.RemoveAll()
            ToolTipbtnDown.RemoveAll()

            ToolTipbtn_CPTCode.Dispose()
            ToolTipbtn_ICD9Code.Dispose()
            ToolTipbtnConceptID.Dispose()
            ToolTipbtnUp.Dispose()
            ToolTipbtnDown.Dispose()

            ToolTipbtn_CPTCode = Nothing
            ToolTipbtn_ICD9Code = Nothing
            ToolTipbtnConceptID = Nothing
            ToolTipbtnUp = Nothing
            ToolTipbtnDown = Nothing

            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.OBPlan, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Close, "Patient OB Plan Closed", m_PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR, True)

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
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.OBPlan, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub frmPatientOBPlan_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

        Dim blnCancelClicked As Boolean

        Try

            IsCloseClickFlagForCommentValidation = True
            Dim IsValidateCommentCancled As Boolean = False
            If IsNothing(C1PatientOBPlan.Editor) = False Then
                If (C1PatientOBPlan.Col = Col_HsComments) And (C1PatientOBPlan.Editor.Visible) And (C1PatientOBPlan.Editor.Text.Length > MAX_COMMENT_LENGHT) Then 'And (e.Cancel)
                    IsValidateCommentCancled = True
                End If
            End If
            Me.BindingContext(dsOBPlan, "OBPlan").EndCurrentEdit()
            If (IsNothing(dsOBPlan.GetChanges) = False AndAlso _blnRecordLock = False) OrElse (_blnRecordLock = False AndAlso IsValidateCommentCancled = True AndAlso _isMakeAsCurrent = False) Then
                Dim oResult As Windows.Forms.DialogResult
                oResult = MessageBox.Show("Do you want to save the changes to OB Plan?", gstrMessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)

                If oResult = Windows.Forms.DialogResult.Cancel Then
                    blnCancelClicked = True
                    e.Cancel = True
                ElseIf oResult = Windows.Forms.DialogResult.Yes Then
                    If ValidatePatientOBPlan() Then
                        e.Cancel = True
                        Exit Sub
                    End If
                    Call SaveOBPlanData()
                    If _IsFrmImm = True Then
                        Dim StrItem As String
                        Dim strReaction As String
                        With C1PatientOBPlan
                            .Row = 0
                            For i As Integer = 1 To .Rows.Count - 1
                                StrItem = .GetData(i, Col_HsOBPlanItem) & ""
                                strReaction = .GetData(i, Col_HsReaction) & ""

                                If _IsFrmImm = True And StrItem = _strAllergy Then
                                    _sReaction = strReaction
                                End If
                            Next
                        End With
                    End If
                ElseIf oResult = Windows.Forms.DialogResult.No Then
                    If IsNothing(C1PatientOBPlan.Editor) = False Then
                        If (C1PatientOBPlan.Col = Col_HsComments) And (C1PatientOBPlan.Editor.Visible) And (C1PatientOBPlan.Editor.Text.Length > MAX_COMMENT_LENGHT) Then
                            C1PatientOBPlan.Editor.Text = ""
                        End If
                    End If
                    If _isMakeAsCurrent = True Then
                        If IsNothing(dsCurrent) = False Then
                            SaveDatasetOBPlan_new(dsCurrent)
                        End If
                    End If
                    e.Cancel = False
                End If
            Else
                If _IsFrmImm = True Then
                    Dim StrItem As String
                    Dim strReaction As String
                    With C1PatientOBPlan
                        .Row = 0
                        For i As Integer = 1 To .Rows.Count - 1
                            StrItem = .GetData(i, Col_HsOBPlanItem) & ""
                            strReaction = .GetData(i, Col_HsReaction) & ""

                            If _IsFrmImm = True And StrItem = _strAllergy Then
                                _sReaction = strReaction
                            End If
                        Next
                    End With
                End If
            End If
            If _isMakeAsCurrent = True AndAlso IsNothing(dsOBPlan.GetChanges) = True AndAlso _isMessage = False Then
                Dim oResult As Windows.Forms.DialogResult
                oResult = MessageBox.Show("Do you want to save the changes to OB Plan?", gstrMessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
                If oResult = Windows.Forms.DialogResult.Cancel Then
                    e.Cancel = True
                ElseIf oResult = Windows.Forms.DialogResult.Yes Then
                    If IsNothing(C1PatientOBPlan.Editor) = False Then
                        If (C1PatientOBPlan.Col = Col_HsComments) And (C1PatientOBPlan.Editor.Visible) And (C1PatientOBPlan.Editor.Text.Length > MAX_COMMENT_LENGHT) Then
                            MessageBox.Show("Comment should be less than equal to 760 characters.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            e.Cancel = True
                            Exit Sub
                        End If
                    End If
                    Call SaveOBPlanData()
                ElseIf oResult = Windows.Forms.DialogResult.No Then
                    If IsNothing(C1PatientOBPlan.Editor) = False Then
                        If (C1PatientOBPlan.Col = Col_HsComments) And (C1PatientOBPlan.Editor.Visible) And (C1PatientOBPlan.Editor.Text.Length > MAX_COMMENT_LENGHT) Then
                            C1PatientOBPlan.Editor.Text = ""
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
                If IsNothing(clsSplit_OBPlan) = False Then
                    clsSplit_OBPlan.SaveControlDisplaySettings()
                End If

            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.OBPlan, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        Finally

            If blnCancelClicked = False Then

                IsCloseClickFlagForCommentValidation = False
                If Not IsNothing(objclsPatientOBPlan) Then
                    objclsPatientOBPlan = Nothing
                End If
                If Not IsNothing(uiPanSplitScreen_OBPlan) Then
                    uiPanSplitScreen_OBPlan = Nothing
                End If

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

    Private Sub frmPatientOBPlan_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        btnUp.Image = gloEMR.My.Resources.Up24
        btnDown.Image = gloEMR.My.Resources.Down24
        btnUp.Visible = True
        btnDown.Visible = True
        btnUp.Enabled = False
        btnDown.Enabled = False
        Me.SuspendLayout()
        gloC1FlexStyle.Style(C1PatientOBPlan, True)  ' True for tooltip
        cmbAllergyType.Items.Add("All")
        cmbAllergyType.Items.Add("Active")
        cmbAllergyType.Items.Add("Inactive")
        cmbAllergyType.SelectedIndex = 0


        ''
        Try
            Call Get_PatientDetails()

            lblPatientCode.Text = strPatientCode
            lblPatientCode.Tag = m_PatientID
            lblPatientName.Text = strPatientFirstName & " " & strPatientLastName
            lblVisitDate.Text = m_VisitDate
            lblVisitDate.Tag = m_VisitID
            lblReviewed.Visible = False
            Dim dt As DataTable
            dt = dsOBPlan.Tables("Category")
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

                    For Each OBPlanRow As DataRow In dt.Rows
                        Dim blnAllowMedicalCondition As Boolean = True
                        If OBPlanRow(1).ToString = "Medical Condition" Then
                            blnAllowMedicalCondition = False
                        End If
                ''
                If blnAllowMedicalCondition = True Then
                    objBtn = New Button
                    objBtn.BackColor = System.Drawing.Color.FromArgb(102, 153, 255)
                    objBtn.ForeColor = Color.White
                    objBtn.Tag = CType(OBPlanRow(0), Long) ''  Category ID
                    objBtn.Text = OBPlanRow(1).ToString  '' Category Name
                    objBtn.Dock = DockStyle.Top
                    objBtn.FlatStyle = FlatStyle.Flat
                    objBtn.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
                    objBtn.BackgroundImageLayout = ImageLayout.Stretch
                    objBtn.ForeColor = Color.FromArgb(31, 73, 125)
                    objBtn.Font = gloGlobal.clsgloFont.gFont_BOLD
                    objBtn.Height = 28


                    If OBPlanRow(1).ToString = "First Trimester" Then '' FIRST BUTTON PUT TO TOP ''
                        Me.pnlTopButtons.Controls.Add(objBtn)
                        BtnText = objBtn.Text
                        BtnTag = objBtn.Tag
                        objBtn.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongOrange
                        objBtn.BackgroundImageLayout = ImageLayout.Stretch
                        srCategory = OBPlanRow(1).ToString
                        i = i + 1
                        blnIsLoad = True
                        objBtn_Click(objBtn, Nothing)

                    Else '' OTHER BUTTONS IN BOTTOM PANEL ''
                        Me.pnlBottomButtons.Controls.Add(objBtn)
                        lblHeightMeter.Height = lblHeightMeter.Height + objBtn.Height
                    End If

                    '''' Add the one event handler for ButtonClick event

                    blnIsLoad = False

                    AddHandler objBtn.Click, AddressOf objBtn_Click

                    AddHandler objBtn.MouseHover, AddressOf objBtn_MouseHover
                    AddHandler objBtn.MouseLeave, AddressOf objBtn_MouseLeave

                End If

                    Next OBPlanRow
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

            If Not IsNothing(dt) Then
                dt.Dispose()
                dt = Nothing
            End If
            pnlBottomButtons.AutoScroll = True




            pnlsnomadedetail.Visible = True
            pnltrvSource.Visible = True
            pnltrvSource.Dock = DockStyle.Fill
            GloUC_trvOBPlan.Dock = DockStyle.Fill

            gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.OBPlan, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Add, "Patient OBPlan Opened", gloAuditTrail.ActivityOutCome.Success)

            Try
                gloPatient.gloPatient.GetWindowTitle(Me, m_PatientID, GetConnectionString(), gstrMessageBoxCaption)
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            End Try
            Me.Visible = True

            If IsNothing(clsSplit_OBPlan) = False Then
                clsSplit_OBPlan.clsUCLabControl = New gloUserControlLibrary.gloUC_TransactionHistory()
                clsSplit_OBPlan.clsPatientExams = New clsPatientExams()
                clsSplit_OBPlan.clsPatientLetters = New clsPatientLetters()
                clsSplit_OBPlan.clsPatientMessages = New clsMessage()
                clsSplit_OBPlan.clsNurseNotes = New clsNurseNotes()
                clsSplit_OBPlan.clsHistory = New clsPatientHistory()
                clsSplit_OBPlan.clsLabs = New clsDoctorsDashBoard()
                clsSplit_OBPlan.clsDMS = New gloEDocumentV3.eDocManager.eDocGetList()
                clsSplit_OBPlan.clsRxmed = New clsPatientDetails()
                clsSplit_OBPlan.clsOrders = New clsPatientDetails()
                clsSplit_OBPlan.clsProblemList = New clsPatientProblemList()
                clsSplit_OBPlan.blnShowSmokingStatusCol = gblnShowSmokingColumn

                uiPanSplitScreen_OBPlan = clsSplit_OBPlan.LoadSplitControl(Me, m_PatientID, m_VisitID, "OBPlan", objCriteria, objWord, gnClinicID, gnLoginID)
                uiPanSplitScreen_OBPlan.BringToFront()
            End If


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

            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.OBPlan, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, "Patient OB Plan Loaded", m_PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR, True)

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.OBPlan, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Set_RecordLock(_blnRecordLock)
            Me.ResumeLayout(True)
        End Try
    End Sub

    Private Sub frmPatientOBPlan_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try
            Me.Activate()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region

#Region " Functions "

    Public Function FormLevelLock() As Boolean
        Try
            Dim dtLock As DataTable ''slr new not needed
            dtLock = Scan_n_Lock_FormLevel(m_PatientID, m_VisitID, 0, "OBPlan")
            FormLevelLockID = Convert.ToInt64(dtLock.Rows(0)("FormLevelID"))
            If dtLock.Rows.Count > 0 Then
                If Convert.ToString(dtLock.Rows(0)("IsOpen")) = "1" Then ''This means form is allready open 
                    'New One
                    If MessageBox.Show("OB Plan for this patient is currently being modified by " & Convert.ToString(dtLock.Rows(0)("UserName")) & " on " & Convert.ToString(dtLock.Rows(0)("MachineName")) & ". Please close out of OB Plan from the other session in order to make modifications from this computer." & vbNewLine & vbNewLine & "Would you like to launch OB Plan in view only mode?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
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

    Private Function CheckOBPlanItemAlreadyExists(ByVal oNode As gloUserControlLibrary.myTreeNode, ByVal dv As DataView) As Boolean
        Dim _retValue As Boolean = False
        Try
            If Not IsNothing(oNode) Then

                dv.RowFilter = "sOBPlanItem = '" & oNode.Text.Trim.Replace("'", "''") & "' and Hidden='" & BtnText.Trim.Replace("'", "''") & "'"
                If dv.Count > 0 Then
                    _retValue = True
                Else
                    _retValue = False
                End If
            Else
                _retValue = True
            End If

        Catch
            _retValue = False
        Finally

        End Try

        Return _retValue
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

    Private Function FillOBPlanCategory1(ByVal CategoryName As String) As Boolean
        Try
            Dim OBPlanTable As DataTable
       
            Dim objclsPatientHistory As New clsPatientHistory
            Dim objclsPatientOBPlan As New clsPatientOBPlan

            bln_Loadcategory = True

            OBPlanTable = objclsPatientOBPlan.GetAllOBPlan(CategoryName)

            Dim dt As DataTable ''slr new not needed 
            dt = OBPlanTable
            GloUC_trvOBPlan.Clear()
            If Not dt Is Nothing Then
                GloUC_trvOBPlan.DataSource = dt
                GloUC_trvOBPlan.Sort = gloUserControlLibrary.gloUC_TreeView.enumSortType.RowOrder

                GloUC_trvOBPlan.RowOrderMember = Convert.ToString(dt.Columns("RowOrder").ColumnName)
                GloUC_trvOBPlan.CodeMember = Convert.ToString(dt.Columns("Dosage").ColumnName)
                GloUC_trvOBPlan.ValueMember = Convert.ToString(dt.Columns("ID").ColumnName)
                GloUC_trvOBPlan.DescriptionMember = Convert.ToString(dt.Columns("Description").ColumnName)
                GloUC_trvOBPlan.Tag = Convert.ToString(dt.Columns("IsDrug").ColumnName)
                GloUC_trvOBPlan.ConceptID = Convert.ToString(dt.Columns("sConceptID").ColumnName)
                GloUC_trvOBPlan.ICD9 = Convert.ToString(dt.Columns("ICD9").ColumnName)
                GloUC_trvOBPlan.CPT = Convert.ToString(dt.Columns("sCPT").ColumnName)
                GloUC_trvOBPlan.HistoryType = Convert.ToString(dt.Columns("sOBPlanType").ColumnName)
                GloUC_trvOBPlan.NDCCodeMember = Convert.ToString(dt.Columns("NDCCode").ColumnName)
                GloUC_trvOBPlan.IsSystemCategory = Convert.ToString(dt.Columns("SystemCategory").ColumnName)
                GloUC_trvOBPlan.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Code_Description
                GloUC_trvOBPlan.FillTreeView()

            End If

            Return True
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.OBPlan, gloAuditTrail.ActivityCategory.Category, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        Finally
            bln_Loadcategory = False
        End Try
    End Function

    Private Function ValidatePatientOBPlan() As Boolean
        Try
            If IsNothing(C1PatientOBPlan.Editor) = False Then
                If (C1PatientOBPlan.Col = Col_HsComments) And (C1PatientOBPlan.Editor.Visible) And (C1PatientOBPlan.Editor.Text.Length > MAX_COMMENT_LENGHT) Then
                    MessageBox.Show("Comment should be less than equal to 760 characters.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Return True
                End If
            End If
            If gstrCodeFieldsinOBPlan = "CodeMandatory" Then
                For m As Integer = 0 To C1PatientOBPlan.Rows.Count - 1
                    If C1PatientOBPlan.Rows(m)(Col_HHidden).ToString.ToUpper <> C1PatientOBPlan.Rows(m)(Col_HCategory).ToString.ToUpper Then
                        If Date.Parse(C1PatientOBPlan.Rows(m)(Col_HDOE_Allergy)).Date = Date.Now.Date Then

                            If Convert.ToString(C1PatientOBPlan.Rows(m)(Col_HsActive)).Trim <> "False" Then
                                If ((Convert.ToString(C1PatientOBPlan.Rows(m)(Col_HsICD9)) = "") And (Convert.ToString(C1PatientOBPlan.Rows(m)(Col_HsConceptID)) = "") And (Convert.ToString(C1PatientOBPlan.Rows(m)(col_HCPT)) = "")) Then
                                    MessageBox.Show("There are selected OB Plan items that are missing a SNOMED " & vbNewLine & "Concept ID, ICD9, or CPT code." & vbNewLine & vbNewLine & "Please enter in the appropriate codes before saving this transaction. ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                    C1PatientOBPlan.Row = m
                                    Return True
                                End If
                            End If
                        End If
                    End If

                Next
            ElseIf gstrCodeFieldsinOBPlan = "CodeWarning" Then
                For m As Integer = 0 To C1PatientOBPlan.Rows.Count - 1
                    If C1PatientOBPlan.Rows(m)(Col_HHidden).ToString.ToUpper <> C1PatientOBPlan.Rows(m)(Col_HCategory).ToString.ToUpper Then
                        If Date.Parse(C1PatientOBPlan.Rows(m)(Col_HDOE_Allergy)).Date = Date.Now.Date Then

                            If Convert.ToString(C1PatientOBPlan.Rows(m)(Col_HsActive)).Trim <> "False" Then
                                If ((Convert.ToString(C1PatientOBPlan.Rows(m)(Col_HsICD9)) = "") And (Convert.ToString(C1PatientOBPlan.Rows(m)(Col_HsConceptID)) = "") And (Convert.ToString(C1PatientOBPlan.Rows(m)(col_HCPT)) = "")) Then
                                    Dim oResult As Windows.Forms.DialogResult
                                    oResult = MessageBox.Show("There are selected OB Plan items that are missing a SNOMED" & vbNewLine & "Concept ID, ICD9, or CPT code." & vbNewLine & "Please enter in the appropriate codes before saving this transaction." & vbNewLine & vbNewLine & "Review now? ", gstrMessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                                    If oResult = Windows.Forms.DialogResult.Cancel Then
                                        Return True
                                    ElseIf oResult = Windows.Forms.DialogResult.Yes Then
                                        C1PatientOBPlan.Row = m
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
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.OBPlan, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Return True
        End Try
    End Function

    Private Function Get_PatientDetails()
        Dim dsPatient As DataSet = Nothing

        Try
            dsPatient = dsOBPlan.Copy()
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
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.OBPlan, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        Finally
            If IsNothing(dsPatient) = False Then
                dsPatient.Dispose()
                dsPatient = Nothing
            End If
        End Try

        Return Nothing

    End Function

    Private Function SetRowState(ByVal dsMain As DataSet) As DataSet
        Try
            If (IsNothing(dsMain) = False) Then


                For intTables As Integer = 0 To dsMain.Tables.Count - 1

                    For intCount As Integer = 0 To dsMain.Tables(intTables).Rows.Count - 1
                        '   If dsMain.Tables(intTables).Rows(intCount).RowState.ToString = "" Then


                        If dsMain.Tables(intTables).Rows(intCount).RowState.ToString = "Added" Then
                            dsMain.Tables(intTables).Rows(intCount)("RowState") = "Added"
                            dsMain.Tables(intTables).Rows(intCount)("sCreatedBy") = gstrLoginName
                            dsMain.Tables(intTables).Rows(intCount)("sUpdatedBy") = gstrLoginName
                        ElseIf dsMain.Tables(intTables).Rows(intCount).RowState.ToString = "Modified" Then
                            dsMain.Tables(intTables).Rows(intCount)("RowState") = "Modified"
                            dsMain.Tables(intTables).Rows(intCount)("sUpdatedBy") = gstrLoginName
                        ElseIf dsMain.Tables(intTables).Rows(intCount).RowState.ToString = "Deleted" Then
                            dsMain.Tables(intTables).Rows(intCount).RejectChanges()
                            dsMain.Tables(intTables).Rows(intCount)("RowState") = "Deleted"
                            dsMain.Tables(intTables).Rows(intCount)("sUpdatedBy") = gstrLoginName
                        End If
                        ' End If
                    Next

                Next

            End If
            Return dsMain
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.OBPlan, gloAuditTrail.ActivityCategory.Category, gloAuditTrail.ActivityType.Select, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            ' MessageBox.Show(ex.ToString, "Patient OBPlan", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try

    End Function

#End Region

#Region " Subroutines "

    Private Sub SaveOBPlanData()
        If (IsNothing(dsOBPlan) = False) Then

            Dim dsModify As DataSet = Nothing
            Dim ds As DataSet = Nothing
            Try

                ''For normal save and close operation
                Me.BindingContext(dsOBPlan, "OBPlan").EndCurrentEdit()
                If IsNothing(dsOBPlan.GetChanges) = False OrElse _dsTemp.Tables("OBPlan").Rows.Count > 0 Then
                    ds = dsOBPlan.Copy()
                    If _isMedicationHistoryModify = True Then
                        If _isDoubleclickPrevHistory = True Then
                            Dim l As Int16
                            For l = 0 To ds.Tables("OBPlan").Rows.Count - 1
                                If ds.Tables("OBPlan").Rows(l).RowState <> DataRowState.Deleted Then
                                    If Convert.ToString(ds.Tables("OBPlan").Rows(l)("nOBPlanID")) = "0" Then
                                        ds.Tables("OBPlan").Rows(l)("RowState") = "Added"
                                    End If
                                Else
                                    ds.Tables("OBPlan").Rows(l).RejectChanges()
                                    ds.Tables("OBPlan").Rows(l)("RowState") = "Deleted"
                                End If
                            Next
                        Else
                            Dim l As Int16
                            For l = 0 To ds.Tables("OBPlan").Rows.Count - 1
                                If ds.Tables("OBPlan").Rows(l).RowState <> DataRowState.Deleted Then
                                    If Convert.ToString(ds.Tables("OBPlan").Rows(l)("nOBPlanID")) = "0" Then
                                        ds.Tables("OBPlan").Rows(l)("RowState") = "Added"
                                    End If
                                Else
                                    ds.Tables("OBPlan").Rows(l).RejectChanges()
                                End If
                            Next
                        End If

                        Call SaveDatasetOBPlan_new(ds)
                        dsOBPlan.AcceptChanges()
                    Else
                        dsOBPlan = SetRowState(dsOBPlan.GetChanges)
                        Call SaveDatasetOBPlan_new(dsOBPlan)
                        dsOBPlan.AcceptChanges()
                    End If
                End If


            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.OBPlan, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
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

    Public Sub PopulatePatientOBPlan_Final()

        btnConceptID.Enabled = False
        btn_CPTCode.Enabled = False
        btn_ICD9Code.Enabled = False
        C1PatientOBPlan.Cols(col_HOnsetDate).AllowEditing = True
        C1PatientOBPlan.Cols(Col_DateResolved).AllowEditing = True
        isOBPlanLoading = True
        lblReviewed.Visible = False
        setStyleGridWidth()

        C1PatientOBPlan.Rows.Count = 1


        Dim ds As DataSet = Nothing

        Dim dtPatientsOBPlan As DataTable = Nothing
        ''slr free prev memory
        If Not IsNothing(_dsTemp) Then
            _dsTemp.Dispose()
            _dsTemp = Nothing
        End If
        _dsTemp = New DataSet ''Deleted rows collection
        Try

            dsOBPlan = objclsPatientOBPlan.GetPatientOBPlan_Optimize(m_PatientID, 0, m_VisitDate, "OB Plan", 1, m_VisitID)

            dsOBPlan.Tables("OBPlan").Columns("nOBPlanID").AutoIncrement = True
            dsOBPlan.Tables("OBPlan").Columns("nOBPlanID").AutoIncrementSeed = 1

            'OB Plan Category manipulation based on User Actions
            dsOBPlan.Tables("OBPlan").Columns.Add("Hidden").SetOrdinal(1)
            dsOBPlan.Tables("OBPlan").Columns.Add("Button").SetOrdinal(5)
            dsOBPlan.Tables("OBPlan").Columns.Add("SmokingStatus").SetOrdinal(6)
            dsOBPlan.Tables("OBPlan").Columns.Add("SmokeButton").SetOrdinal(7)
            dsOBPlan.Tables("OBPlan").Columns.Add("sActive").SetOrdinal(8)

            dsOBPlan.Tables("OBPlan").Columns.Add("RowID")
            dsOBPlan.Tables("OBPlan").Columns.Add("nMemberId")
            ds = dsOBPlan.Copy()
            dsOBPlan.Tables("OBPlan").Clear()

            _dsTemp.Tables.Add("OBPlan")
            _dsTemp.Tables(0).Merge(dsOBPlan.Tables("OBPlan").Copy)

            Dim IsOnsetDate As Boolean = False
            Dim IsActive As Boolean = False
            Dim stronsetActiveStatus As String = ""
            Dim strCategory As String
            Dim strOBPlan As String
            Dim strComment As String
            Dim strRection_Status As String
            Dim intDrugID As Long
            Dim intMedicalConditionId As Long

            Dim strHxDrugName As String
            Dim strHxDosage As String
            Dim strHxNDCCode As String
            Dim strDOE_Allergy As String
            Dim sConCeptID As String
            Dim sDescriptionID As String
            Dim sSnoMedId As String
            Dim sSnoDescription As String
            Dim sICD9 As String
            Dim sOBPlanType As String

            Dim sOBPlanSource As String
            Dim nICDRevision As Int16 = 0 ''added for ICD10 implementation
            Dim CPT As String
            Dim sRxNormID As String
            Dim nOBPlanID As Long
            ''
            Dim strOnsetDate As String
            Dim nRowOrder As Int64

            Dim sCreatedBy As String
            Dim sUpdatedBy As String

            If FormLevelLock() = False Then
                Exit Sub
            End If

            If ds.Tables("OBPlan").Rows.Count > 0 Then

                Dim visitid As Long
                visitid = CType(ds.Tables("OBPlan").Rows.Item(0).Item(9), Long)
                _HxVisitID = visitid
                lblVisitDate.Tag = visitid
                lblVisitDate.Text = CType(ds.Tables("OBPlan").Rows.Item(0).Item(10), DateTime).Date
                lblVisitDate.Tag = visitid

                For Each drOBPlan As DataRow In ds.Tables("OBPlan").Rows
                    strCategory = drOBPlan(OBPlanEnum.sOBPlanCategory)
                    strOBPlan = drOBPlan(OBPlanEnum.sOBPlanItem)
                    strComment = drOBPlan(OBPlanEnum.sComments)
                    strRection_Status = drOBPlan(OBPlanEnum.sReaction)
                    intDrugID = drOBPlan(OBPlanEnum.nDrugID)
                    intMedicalConditionId = drOBPlan(OBPlanEnum.MedicalCondition_Id) 'return the medical condition id

                    strHxDrugName = drOBPlan(OBPlanEnum.sDrugName)
                    strHxDosage = drOBPlan(OBPlanEnum.sDosage)
                    strHxNDCCode = drOBPlan(OBPlanEnum.sNDCCode)
                    strDOE_Allergy = Convert.ToString(drOBPlan(OBPlanEnum.DOE_Allergy)).Replace("NULL", "")
                    sConCeptID = Convert.ToString(drOBPlan(OBPlanEnum.sConceptID)).Replace("NULL", "")
                    sDescriptionID = Convert.ToString(drOBPlan(OBPlanEnum.sDescriptionID)).Replace("NULL", "")
                    sSnoMedId = Convert.ToString(drOBPlan(OBPlanEnum.sSnoMedID)).Replace("NULL", "")
                    sSnoDescription = Convert.ToString(drOBPlan(OBPlanEnum.sDescription)).Replace("NULL", "")
                    sICD9 = drOBPlan(OBPlanEnum.sICD9)

                    ''RowOrder is for mainttaing track of items move up and down
                    CPT = drOBPlan(OBPlanEnum.CPT)
                    sRxNormID = drOBPlan(OBPlanEnum.sRxNormID)
                    nOBPlanID = drOBPlan(OBPlanEnum.nOBPlanID)
                    strOnsetDate = Convert.ToString(drOBPlan(OBPlanEnum.OnsetDate)).Replace("NULL", "")
                    nRowOrder = drOBPlan(OBPlanEnum.nRowOrder)
                    sOBPlanType = Convert.ToString(drOBPlan(OBPlanEnum.sOBPlanType)).Replace("NULL", "")

                    sOBPlanSource = Convert.ToString(drOBPlan("sOBPlanSource")).Replace("NULL", "")
                    nICDRevision = Convert.ToInt16(drOBPlan("nICDRevision")) ''added for ICD10 implementation


                    sCreatedBy = Convert.ToString(drOBPlan("sCreatedBy")).Replace("NULL", "")
                    sUpdatedBy = Convert.ToString(drOBPlan("sUpdatedBy")).Replace("NULL", "")

                    Call FillGrid(strCategory, strOBPlan, strComment, strRection_Status, intDrugID, intMedicalConditionId, strDOE_Allergy, sConCeptID, sDescriptionID, sSnoMedId, sSnoDescription, sRxNormID, "", "", sICD9, "", strHxNDCCode, strHxDosage, nOBPlanID, strOnsetDate, CPT, nRowOrder, sOBPlanType, sOBPlanSource, sCreatedBy, sUpdatedBy, nICDRevision)

                Next

                C1PatientOBPlan.Select(1, 0, 1, 1, True)
                gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.OBPlan, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.View, "Patient OBPlan Viewed", gloAuditTrail.ActivityOutCome.Success)
                blncancel = True
                blnModify = True

            Else

                dtPatientsOBPlan = objclsPatientOBPlan.GetOBPlan(m_PatientID, 1, m_VisitDate)

                Dim blnCloseOBPlan As Boolean

                If dtPatientsOBPlan.Rows.Count > 0 Then
                    If blnShowMessageBox = True And _blnRecordLock = False Then

                        Dim dtName As DataTable
                        dtName = objclsPatientOBPlan.GetPatName(m_PatientID)

                        Dim strMessage As String
                        strMessage = "OB Plan for '" & dtName.Rows(0)("PatientName") & "' has not been reviewed for today’s visit. Would you like to review the patient’s OB Plan at this time? " & vbNewLine & vbNewLine & "YES - Review OB Plan now.  Previous OB Plan from '" & Convert.ToDateTime(dtPatientsOBPlan.Rows(0)("dtvisitDate")).ToShortDateString() & "' will be copied forward to this visit to be verified. " & vbNewLine & vbNewLine & "NO  - Do not review OB Plan now."

                        If MessageBox.Show(strMessage, gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.Yes Then
                            blnCloseOBPlan = False
                            lblReviewed.Visible = False
                            Dim visitid As Long
                            visitid = CType(dtPatientsOBPlan.Rows.Item(0).Item(0), Long)
                            _HxVisitID = visitid
                            lblVisitDate.Tag = 0
                            lblVisitDate.Text = Now.Date

                            If IsNothing(dtPatientsOBPlan) = False Then
                                dtPatientsOBPlan.Dispose()
                                dtPatientsOBPlan = Nothing
                            End If


                            dsOBPlan.Tables("OBPlan").Clear()
                            dsOBPlan = objclsPatientOBPlan.GetPatientOBPlan_Optimize(m_PatientID, 2, m_VisitDate, "OB Plan", 1, visitid)
                            dsOBPlan.Tables("OBPlan").Columns.Add("Hidden").SetOrdinal(1)
                            dsOBPlan.Tables("OBPlan").Columns.Add("Button").SetOrdinal(5)
                            dsOBPlan.Tables("OBPlan").Columns.Add("SmokingStatus").SetOrdinal(6)
                            dsOBPlan.Tables("OBPlan").Columns.Add("SmokeButton").SetOrdinal(7)
                            dsOBPlan.Tables("OBPlan").Columns.Add("sActive").SetOrdinal(8)
                            dsOBPlan.Tables("OBPlan").Columns.Add("RowID")
                            dsOBPlan.Tables("OBPlan").Columns.Add("nMemberId")

                            ds = dsOBPlan.Copy()
                            dsOBPlan.Tables("OBPlan").Clear()
                            dsOBPlan.AcceptChanges()
                            If IsNothing(ds.Tables("OBPlan")) = False Then
                                If ds.Tables("OBPlan").Rows.Count > 0 Then

                                    For Each drOBPlan As DataRow In ds.Tables("OBPlan").Rows
                                        strCategory = drOBPlan(OBPlanEnum.sOBPlanCategory)
                                        strOBPlan = drOBPlan(OBPlanEnum.sOBPlanItem)
                                        strComment = drOBPlan(OBPlanEnum.sComments)
                                        strRection_Status = drOBPlan(OBPlanEnum.sReaction)
                                        intDrugID = drOBPlan(OBPlanEnum.nDrugID)
                                        intMedicalConditionId = drOBPlan(OBPlanEnum.MedicalCondition_Id) 'return the medical condition id

                                        strHxDrugName = drOBPlan(OBPlanEnum.sDrugName)
                                        strHxDosage = drOBPlan(OBPlanEnum.sDosage)
                                        strHxNDCCode = drOBPlan(OBPlanEnum.sNDCCode)
                                        strDOE_Allergy = Convert.ToString(drOBPlan(OBPlanEnum.DOE_Allergy)).Replace("NULL", "")
                                        sConCeptID = Convert.ToString(drOBPlan(OBPlanEnum.sConceptID)).Replace("NULL", "")
                                        sDescriptionID = Convert.ToString(drOBPlan(OBPlanEnum.sDescriptionID)).Replace("NULL", "")
                                        sSnoMedId = Convert.ToString(drOBPlan(OBPlanEnum.sSnoMedID)).Replace("NULL", "")
                                        sSnoDescription = Convert.ToString(drOBPlan(OBPlanEnum.sDescription)).Replace("NULL", "")
                                        sICD9 = drOBPlan(OBPlanEnum.sICD9)
                                        CPT = drOBPlan(OBPlanEnum.CPT)
                                        sRxNormID = drOBPlan(OBPlanEnum.sRxNormID)
                                        nOBPlanID = drOBPlan(OBPlanEnum.nOBPlanID)
                                        strOnsetDate = Convert.ToString(drOBPlan(OBPlanEnum.OnsetDate)).Replace("NULL", "")
                                        nRowOrder = drOBPlan(OBPlanEnum.nRowOrder)
                                        sOBPlanType = Convert.ToString(drOBPlan(OBPlanEnum.sOBPlanType)).Replace("NULL", "")

                                        sOBPlanSource = Convert.ToString(drOBPlan("sOBPlanSource")).Replace("NULL", "")

                                        sCreatedBy = Convert.ToString(drOBPlan("sCreatedBy")).Replace("NULL", "")
                                        sUpdatedBy = Convert.ToString(drOBPlan("sUpdatedBy")).Replace("NULL", "")


                                        nICDRevision = Convert.ToInt16(drOBPlan("nICDRevision"))
                                        Call FillGrid(strCategory, strOBPlan, strComment, strRection_Status, intDrugID, intMedicalConditionId, strDOE_Allergy, sConCeptID, sDescriptionID, sSnoMedId, sSnoDescription, sRxNormID, "", "", sICD9, "", strHxNDCCode, strHxDosage, nOBPlanID, strOnsetDate, CPT, nRowOrder, sOBPlanType, sOBPlanSource, sCreatedBy, sUpdatedBy, nICDRevision)

                                    Next

                                    C1PatientOBPlan.Select(1, 0, 1, 1, True)
                                    _isMedicationHistoryModify = True
                                    Dim l As Int16
                                    For l = 0 To dsOBPlan.Tables("OBPlan").Rows.Count - 1
                                        If dsOBPlan.Tables("OBPlan").Rows(l).RowState <> DataRowState.Deleted Then
                                            dsOBPlan.Tables("OBPlan").Rows(l)("RowState") = "Added"
                                        End If
                                    Next

                                    blncancel = True
                                    blnModify = False
                                End If
                            End If

                            dtName.Dispose()
                            dtName = Nothing

                        Else
                           
                            If Not IsNothing(gloUC_PatientStrip1) Then
                                gloUC_PatientStrip1.Dispose()
                                gloUC_PatientStrip1 = Nothing
                            End If
                            If IsNothing(GloUC_trvOBPlan) = False Then
                                GloUC_trvOBPlan.Dispose()
                                GloUC_trvOBPlan = Nothing
                            End If
                            If IsNothing(tblOBPlan) = False Then
                                tblOBPlan.Dispose()
                                tblOBPlan = Nothing
                            End If

                            If FormLevelLockID > 0 Then
                                Delete_Lock_FormLevel(FormLevelLockID, m_PatientID)
                            End If

                        End If


                    Else
                        ''If blnShowMessage is false, means don't show the message box and take action of "Yes" click
                        If _blnRecordLock = False And blnShowAddModeMessageBox = True Then
                            Dim dtName As DataTable
                            dtName = objclsPatientOBPlan.GetPatName(m_PatientID)
                            MessageBox.Show("OBPlan for '" & dtName.Rows(0)("PatientName") & "' has not been reviewed for today’s visit. OBPlan from '" & Convert.ToDateTime(dtPatientsOBPlan.Rows(0)("dtvisitDate")).ToShortDateString() & "' will be copied forward to this visit to be verified.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2)
                            dtName.Dispose()
                            dtName = Nothing
                        End If
                        ''To Shows another message only for button click 
                        blnCloseOBPlan = False
                        lblReviewed.Visible = False
                        Dim visitid As Long
                        visitid = CType(dtPatientsOBPlan.Rows.Item(0).Item(0), Long)
                        _HxVisitID = visitid
                        lblVisitDate.Tag = 0
                        lblVisitDate.Text = Now.Date

                        If IsNothing(dtPatientsOBPlan) = False Then
                            dtPatientsOBPlan.Dispose()
                            dtPatientsOBPlan = Nothing
                        End If

                        dsOBPlan.Tables("OBPlan").Clear()
                        dsOBPlan = objclsPatientOBPlan.GetPatientOBPlan_Optimize(m_PatientID, 2, m_VisitDate, "OB Plan", 1, visitid)
                        dsOBPlan.Tables("OBPlan").Columns.Add("Hidden").SetOrdinal(1)
                        dsOBPlan.Tables("OBPlan").Columns.Add("Button").SetOrdinal(5)
                        dsOBPlan.Tables("OBPlan").Columns.Add("SmokingStatus").SetOrdinal(6)
                        dsOBPlan.Tables("OBPlan").Columns.Add("SmokeButton").SetOrdinal(7)
                        dsOBPlan.Tables("OBPlan").Columns.Add("sActive").SetOrdinal(8)
                        dsOBPlan.Tables("OBPlan").Columns.Add("RowID")
                        dsOBPlan.Tables("OBPlan").Columns.Add("nMemberId")

                        ds = dsOBPlan.Copy()
                        dsOBPlan.Tables("OBPlan").Clear()
                        dsOBPlan.AcceptChanges()
                        If IsNothing(ds.Tables("OBPlan")) = False Then
                            If ds.Tables("OBPlan").Rows.Count > 0 Then

                                For Each drOBPlan As DataRow In ds.Tables("OBPlan").Rows
                                    strCategory = drOBPlan(OBPlanEnum.sOBPlanCategory)
                                    strOBPlan = drOBPlan(OBPlanEnum.sOBPlanItem)
                                    strComment = drOBPlan(OBPlanEnum.sComments)
                                    strRection_Status = drOBPlan(OBPlanEnum.sReaction)
                                    intDrugID = drOBPlan(OBPlanEnum.nDrugID)
                                    intMedicalConditionId = drOBPlan(OBPlanEnum.MedicalCondition_Id)

                                    strHxDrugName = drOBPlan(OBPlanEnum.sDrugName)
                                    strHxDosage = drOBPlan(OBPlanEnum.sDosage)
                                    strHxNDCCode = drOBPlan(OBPlanEnum.sNDCCode)
                                    strDOE_Allergy = Convert.ToString(drOBPlan(OBPlanEnum.DOE_Allergy)).Replace("NULL", "")
                                    sConCeptID = Convert.ToString(drOBPlan(OBPlanEnum.sConceptID)).Replace("NULL", "")
                                    sDescriptionID = Convert.ToString(drOBPlan(OBPlanEnum.sDescriptionID)).Replace("NULL", "")
                                    sSnoMedId = Convert.ToString(drOBPlan(OBPlanEnum.sSnoMedID)).Replace("NULL", "")
                                    sSnoDescription = Convert.ToString(drOBPlan(OBPlanEnum.sDescription)).Replace("NULL", "")
                                    sICD9 = drOBPlan(OBPlanEnum.sICD9)
                                    CPT = drOBPlan(OBPlanEnum.CPT)
                                    sRxNormID = drOBPlan(OBPlanEnum.sRxNormID)
                                    nOBPlanID = drOBPlan(OBPlanEnum.nOBPlanID)
                                    strOnsetDate = Convert.ToString(drOBPlan(OBPlanEnum.OnsetDate)).Replace("NULL", "")
                                    nRowOrder = drOBPlan(OBPlanEnum.nRowOrder)
                                    sOBPlanType = Convert.ToString(drOBPlan(OBPlanEnum.sOBPlanType)).Replace("NULL", "")

                                    sOBPlanSource = Convert.ToString(drOBPlan("sOBPlanSource")).Replace("NULL", "")
                                    nICDRevision = Convert.ToInt16(drOBPlan("nICDRevision"))

                                    sCreatedBy = Convert.ToString(drOBPlan("sCreatedBy")).Replace("NULL", "")
                                    sUpdatedBy = Convert.ToString(drOBPlan("sUpdatedBy")).Replace("NULL", "")

                                    Call FillGrid(strCategory, strOBPlan, strComment, strRection_Status, intDrugID, intMedicalConditionId, strDOE_Allergy, sConCeptID, sDescriptionID, sSnoMedId, sSnoDescription, sRxNormID, "", "", sICD9, "", strHxNDCCode, strHxDosage, nOBPlanID, strOnsetDate, CPT, nRowOrder, sOBPlanType, sOBPlanSource, sCreatedBy, sUpdatedBy, nICDRevision)

                                Next

                                C1PatientOBPlan.Select(1, 0, 1, 1, True)
                                _isMedicationHistoryModify = True
                                Dim l As Int16
                                For l = 0 To dsOBPlan.Tables("OBPlan").Rows.Count - 1
                                    If dsOBPlan.Tables("OBPlan").Rows(l).RowState <> DataRowState.Deleted Then
                                        dsOBPlan.Tables("OBPlan").Rows(l)("RowState") = "Added"
                                    End If
                                Next

                                blncancel = True
                                blnModify = False
                            End If
                        End If
                    End If
                Else

                    dsOBPlan.AcceptChanges()
                    C1PatientOBPlan.SetDataBinding(dsOBPlan, "OBPlan")
                    blncancel = True
                End If
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.OBPlan, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If IsNothing(ds) = False Then
                ds.Dispose()
                ds = Nothing
            End If

            If IsNothing(dtPatientsOBPlan) = False Then
                dtPatientsOBPlan.Dispose()
                dtPatientsOBPlan = Nothing
            End If
        End Try
        isOBPlanLoading = False

    End Sub

    Public Sub setStyleGridWidth()

        With C1PatientOBPlan
            Dim _TotalWidth As Single = .Width - 5
            .Cols(Col_HCategory).Width = _TotalWidth * 0.15
            .Cols(Col_HHidden).Width = _TotalWidth * 0
            .Cols(Col_HsOBPlanItem).Width = _TotalWidth * 0.19
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
            .Cols(Col_DateResolved).Width = 0

            '  .Cols(col_HCPT).Visible = True


            C1PatientOBPlan.Cols(col_HOnsetDate).Caption = "Completed Date"
            C1PatientOBPlan.Cols(col_HOnsetDate).Width = _TotalWidth * 0.15
            C1PatientOBPlan.Cols("nRowOrder").Width = 0
            C1PatientOBPlan.Cols("nMemberid").Width = 0

            C1PatientOBPlan.Cols("nICDRevision").Width = 0

            C1PatientOBPlan.ExtendLastCol = True
        End With

    End Sub

   
    'Protected Sub NewOBPlan()
    '    dsOBPlan.Tables("OBPlan").Clear()
    '    btnUp.Enabled = False
    '    btnDown.Enabled = False
    '    lblVisitDate.Tag = 0
    '    blnModify = False
    '    lblVisitDate.Text = Now.Date

    'End Sub

    Public Sub CloseOBPlan()
        ' Me.Close()
        gloWord.WordDialogBoxBackgroundCloser.Close(Me)
    End Sub

    Public Sub SaveDatasetOBPlan_new(ByVal dsOBPlan As DataSet)
        Dim ds As DataSet = Nothing
        Dim objclsDB As New clsDoctorsDashBoard

        Try

            If IsNothing(dsOBPlan) = False Then
                ds = dsOBPlan.Copy()
                ds.Tables("OBPlan").Merge(_dsTemp.Tables("OBPlan").Copy())
            Else
                ds = New DataSet
                ds.Tables.Add("OBPlan")
                ds.Tables("OBPlan").Merge(_dsTemp.Tables("OBPlan").Copy())
            End If

            Dim i As Integer
            Dim k As Integer

            If lblVisitDate.Tag = 0 Then
                lblVisitDate.Tag = GenerateVisitID(m_VisitDate, m_PatientID)
            End If

            Dim _categorytype As String = ""
            Dim stronsetActiveStatus As String = ""
            'Dim _arrOnsetActive() As String
            Dim IsActive As Boolean = False
            Dim IsOnsetDate As Boolean = False
            With ds.Tables("OBPlan")
                For i = .Rows.Count - 1 To 0 Step -1
                    .Rows(i)(Col_HCategory) = .Rows(i)(Col_HHidden)
                    If Convert.ToString(.Rows(i)(Col_HsOBPlanItem)) = "" Then
                        .Rows(i).Delete()
                    End If

                Next

                .Columns.Add("nPatientID")
                .Columns.Add("sTransUser")
                ds.AcceptChanges()

                For k = 0 To .Rows.Count - 1

                    _categorytype = Convert.ToString(.Rows(k)(Col_HHidden)).Trim
                    IsActive = True

                    If Convert.ToString(.Rows(k)(Col_HsActive)) = "True" Then
                        .Rows(k)(Col_HsActive) = "Active"
                    ElseIf IsActive = False Then
                        .Rows(k)(Col_HsActive) = "Active"
                    ElseIf Convert.ToString(.Rows(k)(Col_HsActive)) = "False" Then

                        .Rows(k)(Col_HsActive) = "InActive"
                    End If

                    .Rows(k)(Col_HsReaction) = .Rows(k)(Col_HsReaction) & "|" & .Rows(k)(Col_HsActive)
                    .Rows(k)(Col_HnVisitID) = lblVisitDate.Tag

                    .Rows(k)("sTransUser") = gstrLoginName
                    .Rows(k)("nPatientID") = lblPatientCode.Tag


                    If _IsFrmImm = True And .Rows(k)(Col_HsOBPlanItem) = _strAllergy Then
                        .Rows(k)(Col_HsReaction) = .Rows(k)(Col_HsReaction)
                    End If

                Next

                .Columns.Remove("Hidden")
                .Columns.Remove("Button")
                .Columns.Remove("SmokingStatus")
                .Columns.Remove("dtVisitDate")
                .Columns.Remove("SmokeButton")
                .Columns.Remove("sActive")
                .Columns.Remove("RowID")
                .Columns.Remove("DateResolved")
                .Columns.Remove("nMemberId")

            End With

            If objclsPatientOBPlan.AddNewOBPlanDataset(m_PatientID, blnModify, ds, "@SaveOBPlan") = False Then

                Exit Sub

            End If

            blnChangesMade = False
            isOBPlanModified = False
            blnChangesMade = False
            isOBPlanModified = False


            If arrDataDictionary.Count > 0 Then
                DeleteHistoryDataDictionary()
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.OBPlan, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If IsNothing(ds) = False Then
                ds.Dispose()
                ds = Nothing
            End If
            If Not IsNothing(objclsDB) Then

                objclsDB = Nothing
            End If
        End Try
    End Sub

    'Protected Sub OpenMedication(Optional ByVal intstat As Int16 = 0)

    'End Sub

    'Protected Sub ShowHidePreviousOBPlan(ByVal strText As String)

    'End Sub

    Private Sub Set_RecordLock(ByVal locked As Boolean)
        If locked = True Then
            tblSave.Enabled = False

        Else
            tblSave.Enabled = True
            If C1PatientOBPlan.Rows.Count > 1 Then
            Else
            End If
        End If
    End Sub

    

    'Protected Sub NavigatePreviousPatientHistory(ByVal blnKeyValue As Boolean)

    'End Sub

#End Region

#Region " mnuRemove Events "

    Public Sub mnuRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuRemove.Click

        Try

            Dim i As Integer
            i = C1PatientOBPlan.Row
            If i > 0 Then

                If Not IsNothing(C1PatientOBPlan.Rows.Item(0)) Then

                    Try
                        Dim cnt As Int16 = 0
                        Dim _selRow As Integer = C1PatientOBPlan.Row

                        Dim newRow As DataRow = Nothing

                        With C1PatientOBPlan
                            If Convert.ToString(C1PatientOBPlan.GetData(_selRow, Col_HsOBPlanItem)) = "" Then
                                ValidateDataDictionary(.GetData(_selRow, Col_HHidden))
                                For i = C1PatientOBPlan.Rows.Count - 1 To _selRow Step -1
                                    If cnt = _selRow Then
                                        Exit For
                                    End If
                                    If C1PatientOBPlan.GetData(_selRow, Col_HHidden).ToString.ToUpper = C1PatientOBPlan.GetData(_selRow + 1, Col_HHidden).ToString.ToUpper Then
                                        cnt = i - 1
                                        newRow = _dsTemp.Tables("OBPlan").NewRow()
                                        newRow.ItemArray = dsOBPlan.Tables("OBPlan").Rows(_selRow).ItemArray
                                        newRow.Item(27) = "Deleted"
                                        _dsTemp.Tables("OBPlan").Rows.Add(newRow)

                                        dsOBPlan.Tables("OBPlan").Rows.RemoveAt(_selRow)

                                    End If
                                Next
                                newRow = _dsTemp.Tables("OBPlan").NewRow()
                                newRow.ItemArray = dsOBPlan.Tables("OBPlan").Rows(_selRow - 1).ItemArray
                                newRow.Item(27) = "Deleted"
                                _dsTemp.Tables("OBPlan").Rows.Add(newRow)
                                dsOBPlan.Tables("OBPlan").Rows.RemoveAt(_selRow - 1)

                            Else
                                Dim k As Integer = 0
                                For j As Integer = 0 To C1PatientOBPlan.Rows.Count - 1
                                    If C1PatientOBPlan.GetData(j, Col_HHidden).ToString.ToUpper = C1PatientOBPlan.GetData(_selRow, Col_HHidden).ToString.ToUpper Then
                                        k = k + 1
                                    End If
                                Next

                                If k = 2 Then
                                    ValidateDataDictionary(C1PatientOBPlan.GetData(_selRow - 1, Col_HHidden))
                                    newRow = _dsTemp.Tables("OBPlan").NewRow()
                                    newRow.ItemArray = dsOBPlan.Tables("OBPlan").Rows(_selRow - 1).ItemArray
                                    newRow.Item(27) = "Deleted"
                                    _dsTemp.Tables("OBPlan").Rows.Add(newRow)
                                    newRow = _dsTemp.Tables("OBPlan").NewRow()
                                    newRow.ItemArray = dsOBPlan.Tables("OBPlan").Rows(_selRow - 2).ItemArray
                                    newRow.Item(27) = "Deleted"
                                    _dsTemp.Tables("OBPlan").Rows.Add(newRow)
                                    dsOBPlan.Tables("OBPlan").Rows.RemoveAt(_selRow - 1)
                                    dsOBPlan.Tables("OBPlan").Rows.RemoveAt(_selRow - 2)
                                Else
                                    newRow = _dsTemp.Tables("OBPlan").NewRow()
                                    newRow.ItemArray = dsOBPlan.Tables("OBPlan").Rows(_selRow - 1).ItemArray
                                    newRow.Item(27) = "Deleted"
                                    _dsTemp.Tables("OBPlan").Rows.Add(newRow)

                                    dsOBPlan.Tables("OBPlan").Rows.RemoveAt(_selRow - 1)
                                End If

                            End If

                        End With
                        gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.OBPlan, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Delete, "History Item Deleted", gloAuditTrail.ActivityOutCome.Success)
                    Catch ex As Exception
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.OBPlan, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Remove, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                        C1PatientOBPlan.Rows.Remove(C1PatientOBPlan.Row)
                    End Try
                End If
            End If
            isOBPlanModified = True
            C1PatientOBPlan_EnterCell(Nothing, Nothing)

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.OBPlan, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Remove, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "Patient OBPlan", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If C1PatientOBPlan.Rows.Count = 1 Then

            End If
        End Try


    End Sub

#End Region

#Region " _btnimage Events "

    Public Sub _btnimage_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Me.Cursor = Cursors.WaitCursor
        C1PatientOBPlan.BeginUpdate()
        Dim dv As DataView = dsOBPlan.Tables("OBPlan").Copy.DefaultView
        For Each _node As gloUserControlLibrary.myTreeNode In GloUC_trvOBPlan.Nodes
            FillDataInGrid(dv, _node)
        Next

        C1PatientOBPlan.EndUpdate()
        Me.Cursor = Cursors.Default
        If Not IsNothing(dv) Then
            dv.Dispose()
            dv = Nothing
        End If
    End Sub

    Private Sub _btnimage_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs)
        C1SuperTooltip1.SetToolTip(sender, "Add All Items")
    End Sub

#End Region

#Region " objBtn Events "

    Public Sub objBtn_Click(ByVal sender As Object, ByVal e As System.EventArgs) 'Handles objBtn.Click

        If bln_Loadcategory Then
            Exit Sub
        End If
        Cursor = Cursors.WaitCursor
        Try

            If FillOBPlanCategory1(sender.Text) Then
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
                'End If

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


                        Dim myInternalView As New DataView(myInternalDt, "", "sDescription Desc", DataViewRowState.CurrentRows)
                        If Not IsNothing(myInternalView) Then
                            pnlBottomButtons.Controls.Clear() ''First Clear The Bottom Panel then Add Buttons in Descending order.

                            For Each dtrow As DataRow In myInternalView.ToTable.Rows

                                Dim blnshowMedicalCondbtn As Boolean = True
                                If dtrow(1).ToString = "Medical Condition" Then
                                    blnshowMedicalCondbtn = False
                                End If
                        If blnshowMedicalCondbtn Then
                            objBtn = New Button
                            objBtn.BackColor = System.Drawing.Color.FromArgb(102, 153, 255)
                            objBtn.ForeColor = Color.White
                            objBtn.Tag = CType(dtrow(0), Long) ''  Category ID
                            objBtn.Text = dtrow(1).ToString  '' Category Name
                            objBtn.Dock = DockStyle.Top
                            objBtn.FlatStyle = FlatStyle.Flat
                            objBtn.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
                            objBtn.BackgroundImageLayout = ImageLayout.Stretch
                            objBtn.ForeColor = Color.FromArgb(31, 73, 125)
                            objBtn.Font = gloGlobal.clsgloFont.gFont_BOLD
                            objBtn.Height = 28

                            pnlBottomButtons.Controls.Add(objBtn)

                            AddHandler objBtn.Click, AddressOf objBtn_Click

                            AddHandler objBtn.MouseHover, AddressOf objBtn_MouseHover
                            AddHandler objBtn.MouseLeave, AddressOf objBtn_MouseLeave
                        End If
                            Next
                        End If ''myInternalView
                        If Not IsNothing(myInternalView) Then
                            myInternalView.Dispose()
                            myInternalView = Nothing
                        End If
                    End If  ''myInternalDt
                End If ''blnIsLoad
                If Not IsNothing(myInternalDt) Then
                    myInternalDt.Dispose()
                    myInternalDt = Nothing
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.OBPlan, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            pnltrvSource.Visible = True
        End Try

        Cursor = Cursors.Default

    End Sub

#End Region

#Region " tblHistory Events "

    'Public Sub tblHistory_ButtonClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolBarButtonClickEventArgs)
    '    Try
    '        Select Case e.Button.Text
    '            Case "&New"
    '                NewOBPlan()
    '            Case "&Save"
    '                If IsNothing(C1PatientOBPlan.Editor) = False Then
    '                    If (C1PatientOBPlan.Col = Col_HsComments) And (C1PatientOBPlan.Editor.Visible) And (C1PatientOBPlan.Editor.Text.Length > MAX_COMMENT_LENGHT) Then
    '                        MessageBox.Show("Comment should be less than equal to 760 characters.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
    '                        Exit Sub
    '                    End If
    '                End If
    '                Call SaveOBPlanData()

    '                OpenMedication()
    '            Case "&Finish"
    '            Case "Narrative"
    '                ' ShowHideHistoryNarrative(e.Button.Pushed)
    '            Case "Show", "Hide"
    '                ShowHidePreviousOBPlan(e.Button.Text)
    '            Case "&Close"
    '                Call CloseOBPlan()
    '            Case "&Review"
    '                'Call ShowReview()
    '            Case "&Hx"
    '                ' Call ShowHistoryOfHistory()


    '        End Select
    '    Catch ex As Exception
    '        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.OBPlan, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
    '        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub

#End Region

#Region " txtsearchhistory Events "

    'Protected Sub txtsearchhistory_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)

    'End Sub

    'Protected Sub txtsearchhistory_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)

    'End Sub

#End Region

#Region " C1PatientOBPlan Events "

    Dim Defination As String = ""
    Dim ConceptDesc As String = ""
    Dim ICD9 As String = ""
    Dim OBPlanType As String = ""
    Dim strHeader() As String
    Dim strDefination() As String
    Dim strHead As String
    Dim oIsNode As myTreeNode
    Dim oDescr As myTreeNode



    Private Sub C1PatientOBPlan_SetupEditor(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles C1PatientOBPlan.SetupEditor
        Dim dtp As DateTimePicker = TryCast(C1PatientOBPlan.Editor, DateTimePicker)
        If IsNothing(dtp) = False Then
            dtp.ShowCheckBox = True

        End If
    End Sub

    Private Sub C1PatientOBPlan_ValidateEdit(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.ValidateEditEventArgs) Handles C1PatientOBPlan.ValidateEdit
        If IsNothing(C1PatientOBPlan.Editor) = False Then


            Dim dtp As DateTimePicker = TryCast(C1PatientOBPlan.Editor, DateTimePicker)
            If IsNothing(dtp) = False Then


                If dtp.Checked = True Then
                    C1PatientOBPlan.Editor.Text = C1PatientOBPlan.Editor.Text
                Else
                    C1PatientOBPlan.Editor.Text = Nothing
                End If
            End If
        End If
        If (IsCloseClickFlagForCommentValidation) Then
            If IsNothing(C1PatientOBPlan.Editor) = False Then
                If (C1PatientOBPlan.Editor.Text.Length > MAX_COMMENT_LENGHT) Then
                    e.Cancel = True
                End If
            End If
            Exit Sub
        End If
        If IsNothing(C1PatientOBPlan.Editor) = True Then
            Exit Sub
        End If
        If (C1PatientOBPlan.Editor.Text.Length > MAX_COMMENT_LENGHT) Then
            MessageBox.Show("Comment should be less than equal to 760 characters.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            e.Cancel = True
        End If
    End Sub

    'Private Sub C1PatientOBPlan_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles C1PatientOBPlan.MouseMove
    '    'gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, sender, e.Location)
    'End Sub

    Private Sub C1PatientOBPlan_BeforeEdit(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles C1PatientOBPlan.BeforeEdit
        Try

            Dim IsActive As Boolean = False
            Dim IsOnsetDate As Boolean = False
            If isOBPlanLoading = False Then
                IsActive = True
                IsOnsetDate = True



                If (Convert.ToString(C1PatientOBPlan.GetData(e.Row, Col_HCategory)) <> "") Then
                    e.Cancel = True
                End If

                If IsActive = True And IsOnsetDate = False Then
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
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub C1PatientOBPlan_BeforeRowColChange(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.RangeEventArgs) Handles C1PatientOBPlan.BeforeRowColChange
        If PnlCustomTask.Visible = True Then
            PnlCustomTask.Visible = False
        End If
    End Sub

    Private Sub C1PatientOBPlan_CellChanged(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles C1PatientOBPlan.CellChanged
        If isOBPlanLoading = False Then
            isOBPlanModified = True
        End If
    End Sub

    Private Sub C1PatientOBPlan_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles C1PatientOBPlan.Click
        If Not IsNothing(C1PatientOBPlan) Then
            Dim r As Integer = C1PatientOBPlan.RowSel
            Dim _categorytype As String = ""
            Dim stronsetActiveStatus As String = ""

            Dim IsActive As Boolean = False
            Try
                If r >= 0 Then
                    _categorytype = C1PatientOBPlan.GetData(r, Col_HHidden)
                    IsActive = True

                End If

            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.OBPlan, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            End Try
        End If
    End Sub

    Private Sub C1PatientOBPlan_EnterCell(ByVal sender As Object, ByVal e As System.EventArgs) Handles C1PatientOBPlan.EnterCell

        Dim ICD9Codetbl As DataSet = Nothing
        Dim objPatientOBPlan As New clsPatientOBPlan()

        Try
            Dim _selrow1 As Integer = 0
            Dim _NewRow As Integer = 0
            If C1PatientOBPlan.Rows.Count > 0 Then
                Dim _rowsel As Integer = C1PatientOBPlan.RowSel
                If _rowsel > 0 Then
                    If Convert.ToString(C1PatientOBPlan.GetData(_rowsel, Col_HCategory)).ToUpper = Convert.ToString(C1PatientOBPlan.GetData(_rowsel, Col_HHidden)).ToUpper Then
                        btnConceptID.Enabled = False
                        btn_CPTCode.Enabled = False
                        btn_ICD9Code.Enabled = False
                    Else
                        btnConceptID.Enabled = True
                        btn_CPTCode.Enabled = True
                        btn_ICD9Code.Enabled = True
                    End If
                    btnUp.Enabled = False
                    btnDown.Enabled = False
                    Try


                        If Convert.ToString(C1PatientOBPlan.GetData(_rowsel, Col_HCategory)) = "" Then
                            Dim _selrow As Integer = C1PatientOBPlan.Row + 1
                            Dim _strcategory As String = ""
                            _strcategory = Convert.ToString(C1PatientOBPlan.Rows(_rowsel)(Col_HHidden))
                            If _selrow <> C1PatientOBPlan.Rows.Count Then
                                If Convert.ToString(C1PatientOBPlan.GetData(_rowsel + 1, Col_HCategory)) = "" Then
                                    _selrow1 = C1PatientOBPlan.Row
                                    _NewRow = C1PatientOBPlan.Row + 1

                                    For j As Integer = C1PatientOBPlan.Row To C1PatientOBPlan.Rows.Count - 1
                                        If _NewRow <> C1PatientOBPlan.Rows.Count Then

                                            If _strcategory.ToUpper = Convert.ToString(C1PatientOBPlan.Rows(_NewRow)(Col_HHidden)).ToUpper Then
                                                If _NewRow > 1 Then
                                                    If C1PatientOBPlan.Rows(_NewRow).IsVisible Then
                                                        If Convert.ToString(C1PatientOBPlan.GetData(_NewRow, Col_HCategory)) = "" Then
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
                            If Convert.ToString(C1PatientOBPlan.GetData(_rowsel - 1, Col_HCategory)) = "" Then
                                _selrow1 = C1PatientOBPlan.Row
                                '_NewRow = C1HistoryDetails.Row - 2
                                _NewRow = _selrow1 - 1
                                For j As Integer = C1PatientOBPlan.Row To 1 Step -1
                                    If _NewRow > 1 Then
                                        If C1PatientOBPlan.Rows(_NewRow).IsVisible Then
                                            If Convert.ToString(C1PatientOBPlan.GetData(_NewRow, Col_HCategory)) = "" Then
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
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.OBPlan, "C1HistoryDetails_EnterCell : " & gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    End Try

                    Defination = Convert.ToString(C1PatientOBPlan.GetData(_rowsel, Col_HsDescription))
                    If IsNothing(C1PatientOBPlan.GetData(_rowsel, Col_HsConceptID)) = False Then


                        If Convert.ToString(C1PatientOBPlan.GetData(_rowsel, Col_HsConceptID)).Trim = "0" Then
                            lblconcptid.Text = ""
                        Else
                            lblconcptid.Text = Convert.ToString(C1PatientOBPlan.GetData(_rowsel, Col_HsConceptID)) '.ToString()
                        End If
                    Else
                        lblconcptid.Text = C1PatientOBPlan.GetData(_rowsel, Col_HsConceptID)
                    End If
                    If lblconcptid.Text.Trim() <> "" Then
                        If Defination.Trim() <> "" Then
                            lblconcptid.Text = lblconcptid.Text + "-" + Defination
                        End If
                    End If

                    lbldescid.Text = Convert.ToString(C1PatientOBPlan.GetData(_rowsel, Col_HsDescriptionID)) '.ToString()
                    ' lblSnomedID.Text = Convert.ToString(C1PatientOBPlan.GetData(_rowsel, Col_HsSnomedID)) '.ToString()

                    ConceptDesc = Convert.ToString(C1PatientOBPlan.GetData(_rowsel, Col_HsOBPlanItem))

                    OBPlanType = ""

                    ICD9 = Convert.ToString(C1PatientOBPlan.GetData(_rowsel, Col_HsICD9))
                    If ICD9 <> "" Or Convert.ToString(C1PatientOBPlan.GetData(_rowsel, col_HCPT)) <> "" Then

                        ICD9Codetbl = objPatientOBPlan.Fill_ICD9Code(Convert.ToString(C1PatientOBPlan.GetData(_rowsel, Col_HsICD9)), Convert.ToString(C1PatientOBPlan.GetData(_rowsel, col_HCPT)))
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
                        If Convert.ToString(C1PatientOBPlan.GetData(_rowsel, col_HCPT)) = "" Then
                            lnlLbllCPTCode.Text = ""
                        End If

                    End If

                    lblRxNorm.Text = Convert.ToString(C1PatientOBPlan.GetData(_rowsel, Col_HsRxNormID))
                    lblNDCid.Text = Convert.ToString(C1PatientOBPlan.GetData(_rowsel, Col_HsNDCCode))
                End If
            End If

            objPatientOBPlan = Nothing

            If IsNothing(ICD9Codetbl) = False Then
                ICD9Codetbl.Dispose()
                ICD9Codetbl = Nothing
            End If



        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.OBPlan, "C1PatientOBPlan_EnterCell : " & gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub C1PatientOBPlan_KeyPressEdit(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.KeyPressEditEventArgs) Handles C1PatientOBPlan.KeyPressEdit
        If e.Col = Col_HsComments Then
            Try
                If (Char.IsControl(e.KeyChar)) Then
                    Exit Sub
                End If
                If (C1PatientOBPlan.Editor.Text.Length >= MAX_COMMENT_LENGHT) Then
                    e.Handled = True
                End If
            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Sub C1PatientOBPlan_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles C1PatientOBPlan.MouseDown
        Try
            If e.Button = MouseButtons.Right Then
                With C1PatientOBPlan
                    Dim r As Integer = .HitTest(e.X, e.Y).Row
                    If r > 0 Then
                        .Select(r, True)
                        mnuAddReaction.Visible = False

                        C1PatientOBPlan.ContextMenu = ContextMenuC1OBPlan
                        If Convert.ToString(C1PatientOBPlan.GetData(r, Col_HsOBPlanItem)) = "" Then
                            mnuRemove.Text = "Remove OB Plan Category"
                            mnuAddReaction.Visible = False
                        Else
                            mnuRemove.Text = "Remove OB Plan Item"
                            mnuAddReaction.Visible = False
                        End If

                    Else

                        C1PatientOBPlan.ContextMenu = Nothing

                    End If
                End With
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.OBPlan, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Public Sub FillGrid(ByVal strCategory As String, ByVal strOBPlan As String, ByVal strComment As String, ByVal strRection_Status As String, ByVal intDrugID As Integer, ByVal intMedicalConditionId As Long, ByVal DOEAllergy As String, ByVal sConceptID As String, ByVal sDescriptionID As String, ByVal sSnoMedID As String, ByVal sDescription As String, ByVal sTranID1 As String, ByVal sTranID2 As String, ByVal sTranID3 As String, ByVal sICD9 As String, ByVal sTranUser As String, ByVal sNDCCode As String, ByVal strHxDosage As String, ByVal nOBPlanID As Long, ByVal OnsetDate As String, ByVal CPT As String, ByVal nRowOrder As Int64, ByVal OBPlanType As String, ByVal OBPlanSource As String, ByVal sCreatedBy As String, ByVal sUpdatedBy As String, Optional ByVal nICDRevision As Int32 = 9)
        Dim j As Integer
        Dim IsOnsetDate As Boolean = False
        Dim IsActive As Boolean = False
        Dim stronsetActiveStatus As String = ""
        Dim _categorytype As String = ""

        With dsOBPlan.Tables("OBPlan")
            C1PatientOBPlan.SetDataBinding(dsOBPlan, "OBPlan")
            Dim _Row As Integer = 0
            For j = 0 To .Rows.Count - 1

                If .Rows(j)(Col_HHidden).ToString().ToUpper() = strCategory.ToUpper() Then
                    Try

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
                Else
                    .Rows(_Row)(Col_HsReaction) = ""
                End If


                .Rows(_Row)("nRowOrder") = 0
                C1PatientOBPlan.Rows(_Row + 1).AllowEditing = False
                .Rows.Add(_Row + 1)
                _Row = _Row + 1
            End If

            .Rows(_Row)(Col_HCategory) = ""
            .Rows(_Row)(Col_HHidden) = ""
            .Rows(_Row)(Col_HsOBPlanItem) = ""
            .Rows(_Row)(Col_HsComments) = ""
            .Rows(_Row)(Col_HsReaction) = ""
            .Rows(_Row)(Col_HsConceptID) = ""
            .Rows(_Row)(Col_HsDescriptionID) = ""
            .Rows(_Row)(Col_HsNDCCode) = ""
            .Rows(_Row)(Col_HsSnomedID) = ""
            .Rows(_Row)(Col_HsDescription) = ""
            .Rows(_Row)(Col_HsICD9) = ""
            .Rows(_Row)(col_HCPT) = ""

            .Rows(_Row)(Col_HsRxNormID) = ""
            .Rows(_Row)(Col_HsDrugName) = ""

            .Rows(_Row)(Col_HHidden) = strCategory
            .Rows(_Row)(Col_HsOBPlanItem) = strOBPlan
            .Rows(_Row)(Col_HsDrugName) = strOBPlan
            .Rows(_Row)(Col_HsComments) = strComment
            .Rows(_Row)(Col_HsReaction) = ""
            .Rows(_Row)(Col_HDOE_Allergy) = DOEAllergy
            .Rows(_Row)(Col_HsConceptID) = sConceptID
            .Rows(_Row)(Col_HsDescriptionID) = sDescriptionID
            .Rows(_Row)(Col_HsNDCCode) = sNDCCode
            .Rows(_Row)(Col_HsSnomedID) = sSnoMedID
            .Rows(_Row)(Col_HsDescription) = sDescription
            .Rows(_Row)(Col_HsICD9) = sICD9
            .Rows(_Row)(col_HCPT) = CPT
            .Rows(_Row)("sOBPlanType") = OBPlanType
            .Rows(_Row)(Col_HsRxNormID) = sTranID1
            .Rows(_Row)("nOBPlanID") = nOBPlanID
            .Rows(_Row)("nRowOrder") = nRowOrder

            .Rows(_Row)("sOBPlanSource") = OBPlanSource

            .Rows(_Row)("nICDRevision") = nICDRevision    ''added for ICD10 implementation

            .Rows(_Row)("sCreatedBy") = sCreatedBy
            .Rows(_Row)("sUpdatedBy") = sUpdatedBy


            dsOBPlan.AcceptChanges()

            C1PatientOBPlan.SetDataBinding(dsOBPlan, "OBPlan")
            _categorytype = .Rows(_Row)(Col_HHidden)




            IsActive = True
            IsOnsetDate = True

            If Not String.IsNullOrEmpty(OnsetDate) Then
                .Rows(_Row)(col_HOnsetDate) = OnsetDate
            Else
                '.Rows(_Row)(col_HOnsetDate) = ""
            End If



            If IsOnsetDate = True Then

                Dim cs As CellStyle '= C1HistoryDetails.Styles.Add("DateTime")
                Try
                    If (C1PatientOBPlan.Styles.Contains("DateTime")) Then
                        cs = C1PatientOBPlan.Styles("DateTime")
                    Else
                        cs = C1PatientOBPlan.Styles.Add("DateTime")

                    End If
                Catch ex As Exception
                    cs = C1PatientOBPlan.Styles.Add("DateTime")

                End Try
                Dim rgDTP As C1.Win.C1FlexGrid.CellRange = C1PatientOBPlan.GetCellRange(_Row + 1, col_HOnsetDate, _Row + 1, col_HOnsetDate)
                rgDTP.Style = cs
                rgDTP.StyleNew.DataType = GetType(DateTime)
                rgDTP.StyleNew.TextAlign = TextAlignEnum.CenterCenter
                rgDTP.StyleNew.ImageAlign = ImageAlignEnum.CenterCenter

            End If



            Dim cStyle As C1.Win.C1FlexGrid.CellStyle = Nothing
            Dim rgReaction As C1.Win.C1FlexGrid.CellRange = C1PatientOBPlan.GetCellRange(_Row + 1, Col_HButton, _Row + 1, Col_HButton)
            Dim rgActive As C1.Win.C1FlexGrid.CellRange = C1PatientOBPlan.GetCellRange(_Row + 1, Col_HsActive, _Row + 1, Col_HsActive)

            Dim strReaction As String
            Dim strActive As String
            If IsActive = True Then
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
                    dsOBPlan.Tables("OBPlan").Rows(_Row)(Col_HsActive) = True
                Else
                    dsOBPlan.Tables("OBPlan").Rows(_Row)(Col_HsActive) = False
                End If

                rgActive.StyleNew.DataType = GetType(Boolean)
                rgActive.StyleNew.TextAlign = TextAlignEnum.CenterCenter
                rgActive.StyleNew.ImageAlign = ImageAlignEnum.CenterCenter

            End If

            .Rows(_Row)(Col_HnDrugID) = 0
            .Rows(_Row)(Col_HMedicalConditionID) = 0
            .Rows(_Row)(Col_HsDosage) = ""
            .Rows(_Row)(Col_HsNDCCode) = ""

            .Rows(_Row)(Col_HnDrugID) = intDrugID
            .Rows(_Row)(Col_HMedicalConditionID) = intMedicalConditionId
            .Rows(_Row)(Col_HsDosage) = strHxDosage
            .Rows(_Row)(Col_HsNDCCode) = sNDCCode
        End With

        dsOBPlan.AcceptChanges()


        For i As Integer = 0 To C1PatientOBPlan.Rows.Count - 1
            If C1PatientOBPlan.Rows(i)(Col_HCategory) <> "" And C1PatientOBPlan.Rows(i)(Col_HsReaction) <> "" Then
                Dim asgTask1 As C1.Win.C1FlexGrid.CellStyle '= C1HistoryDetails.Styles.Add("asgTask")
                Try
                    If (C1PatientOBPlan.Styles.Contains("asgTask")) Then
                        asgTask1 = C1PatientOBPlan.Styles("asgTask")
                    Else
                        asgTask1 = C1PatientOBPlan.Styles.Add("asgTask")
                        asgTask1.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD ' New System.Drawing.Font(C1HistoryDetails.Font.FontFamily.Name, 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(0))

                    End If
                Catch ex As Exception
                    asgTask1 = C1PatientOBPlan.Styles.Add("asgTask")
                    asgTask1.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD 'New System.Drawing.Font(C1HistoryDetails.Font.FontFamily.Name, 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(0))

                End Try
                C1PatientOBPlan.SetCellStyle(i, Col_HsReaction, asgTask1)
            End If
        Next
    End Sub


#End Region


#Region " objBtn Events "

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

#End Region


#Region " GloUC_trvOBPlan Events "

    Private Sub GloUC_trvOBPlan_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles GloUC_trvOBPlan.KeyPress
        Dim dv As DataView = dsOBPlan.Tables("OBPlan").Copy.DefaultView
        FillDataInGrid(dv)
        dv.Dispose()
        dv = Nothing
    End Sub

    Private Sub GloUC_trvOBPlan_NodeMouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles GloUC_trvOBPlan.NodeMouseDoubleClick
        If e.Button = Windows.Forms.MouseButtons.Left Then
            Dim dv As DataView = dsOBPlan.Tables("OBPlan").Copy.DefaultView
            FillDataInGrid(dv)
            dv.Dispose()
            dv = Nothing
        End If
    End Sub

    Private Sub GloUC_trvOBPlan_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles GloUC_trvOBPlan.MouseDown
        Try

            GloUC_trvOBPlan.ContextMenu = Nothing
            If BtnText <> "Medical Condition" And BtnText <> "Coded History" Then
                If gblnCodedHistory = False Or BtnText.StartsWith("Aller") Then
                    If e.Button = MouseButtons.Right Then
                        If _RecordLock = True Then

                            GloUC_trvOBPlan.ContextMenu = Nothing
                            Exit Sub
                        End If
                        Dim trvnode As gloUserControlLibrary.myTreeNode = CType(GloUC_trvOBPlan.SelectedNode, gloUserControlLibrary.myTreeNode)
                        If IsNothing(trvnode) = False Then
                            GloUC_trvOBPlan.SelectedNode = trvnode
                            If trvnode.IsSystemCategory = "True" Then
                                cntCategory.MenuItems.Item(0).Visible = True
                                cntCategory.MenuItems.Item(1).Visible = False


                                GloUC_trvOBPlan.ContextMenu = cntCategory
                            ElseIf IsNothing(trvnode.Tag) = False Then
                                If trvnode.Tag = 1 Then
                                    cntCategory.MenuItems.Item(0).Visible = True
                                    cntCategory.MenuItems.Item(1).Visible = False

                                    GloUC_trvOBPlan.ContextMenu = cntCategory
                                Else
                                    If Not IsNothing(trvnode) Then
                                        cntCategory.MenuItems.Item(0).Visible = True
                                        cntCategory.MenuItems.Item(1).Visible = True

                                        GloUC_trvOBPlan.ContextMenu = cntCategory
                                    Else
                                        cntCategory.MenuItems.Item(0).Visible = True
                                        cntCategory.MenuItems.Item(1).Visible = False

                                        GloUC_trvOBPlan.ContextMenu = cntCategory
                                    End If
                                End If
                            Else

                                If Not IsNothing(trvnode) Then
                                    cntCategory.MenuItems.Item(0).Visible = True
                                    cntCategory.MenuItems.Item(1).Visible = True

                                    GloUC_trvOBPlan.ContextMenu = cntCategory
                                Else
                                    cntCategory.MenuItems.Item(0).Visible = True
                                    cntCategory.MenuItems.Item(1).Visible = False

                                    GloUC_trvOBPlan.ContextMenu = cntCategory
                                End If
                            End If
                        Else
                            cntCategory.MenuItems.Item(0).Visible = True
                            cntCategory.MenuItems.Item(1).Visible = False

                            GloUC_trvOBPlan.ContextMenu = cntCategory
                        End If
                    Else

                        GloUC_trvOBPlan.ContextMenu = Nothing
                    End If
                Else

                    GloUC_trvOBPlan.ContextMenu = Nothing
                End If

            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.OBPlan, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, "Patient OBPlan", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub GloUC_trvOBPlan_NodeAdded(ByVal ChildNode As gloUserControlLibrary.myTreeNode) Handles GloUC_trvOBPlan.NodeAdded
        If IsNothing(ChildNode.Tag) = False Then
            If Convert.ToString(ChildNode.Tag) <> "" Then
                If ChildNode.Tag = 1 Then
                    ChildNode.ImageIndex = 3
                    ChildNode.SelectedImageIndex = 3
                End If
            End If
        End If
    End Sub

#End Region


 


    Private Sub tblOBPlan_ItemClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tblOBPlan.ItemClicked
        Try
            If (e.ClickedItem.Tag = "Close") Then
                IsCloseClickFlagForCommentValidation = True
            End If
            tblOBPlan.Select()
            If (e.ClickedItem.Tag = "Close") Then
                IsCloseClickFlagForCommentValidation = False
            Else
                If IsNothing(C1PatientOBPlan.Editor) = False Then
                    If (C1PatientOBPlan.Col = Col_HsComments) And (C1PatientOBPlan.Editor.Visible) And (C1PatientOBPlan.Editor.Text.Length > MAX_COMMENT_LENGHT) Then
                        Exit Sub
                    End If
                End If
            End If
        Catch ex As Exception

        End Try

        Select Case e.ClickedItem.Tag

            Case "Save"
                clsSplit_OBPlan.loadSplitControlData(m_PatientID, m_VisitID, uiPanSplitScreen_OBPlan.SelectedPanel.Name, objCriteria, objWord, gnClinicID)
                If ValidatePatientOBPlan() Then
                    Exit Sub
                End If
                Call SaveOBPlanData()

                clsSplit_OBPlan.SaveControlDisplaySettings()

                If (Me.IsDisposed = False) Then
                    ' Me.Close()
                    gloWord.WordDialogBoxBackgroundCloser.Close(Me)
                End If

            Case "Close"
                Call CloseOBPlan()
        End Select
    End Sub

    Private Sub mnuOBPlanItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuAddOBPlanItem.Click
        Try
            ' Calling OB plan master form to add new item under the selected category in control
            Using objfrmOBPlanMaster As New frmMSTOBPlan(BtnTag)

                objfrmOBPlanMaster.Text = "Add OB Plan Item"
                objfrmOBPlanMaster._SelectedCategoty = BtnText

                objfrmOBPlanMaster.ShowDialog(IIf(IsNothing(objfrmOBPlanMaster.Parent), Me, objfrmOBPlanMaster.Parent))

                If objfrmOBPlanMaster._DialogResult = Windows.Forms.DialogResult.OK Then

                    'Refresh the tree control with changes added by itmes list.
                    FillOBPlanCategory1(BtnText)
                End If

                objfrmOBPlanMaster.Close()

            End Using

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.OBPlan, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Refresh, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
        End Try

    End Sub

    Private Sub mnuEditHistoryItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuEditOBPlanItem.Click
        ''btntag contains categoryid for which we are adding History Item
        Dim trvnode As gloUserControlLibrary.myTreeNode = CType(GloUC_trvOBPlan.SelectedNode, gloUserControlLibrary.myTreeNode)

        If IsNothing(trvnode) = False Then
            Dim ID As Long = trvnode.ID

            Dim objfrmMSTOBPlan As New frmMSTOBPlan(BtnTag, ID)

            Try
                objfrmMSTOBPlan.Text = "Edit OB Plan Item"
                objfrmMSTOBPlan._SelectedCategoty = BtnText
                objfrmMSTOBPlan.ShowDialog(IIf(IsNothing(objfrmMSTOBPlan.Parent), Me, objfrmMSTOBPlan.Parent))
                'Change made to solve memory Leak and word crash issue
                objfrmMSTOBPlan.Close()
                objfrmMSTOBPlan.Dispose()
                objfrmMSTOBPlan = Nothing
                'btntext contains the description of selected category
                FillOBPlanCategory1(BtnText)
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.OBPlan, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Modify, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                objfrmMSTOBPlan = Nothing
            End Try
        End If
        'Try
        '    If (IsNothing(GloUC_trvHistory.ContextMenu) = False) Then
        '        GloUC_trvHistory.ContextMenu.Dispose()
        '        GloUC_trvHistory.ContextMenu = Nothing
        '    End If
        'Catch ex As Exception

        'End Try
        GloUC_trvOBPlan.ContextMenu = Nothing
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

    Dim blnchksmk As Boolean = False

    Private Sub dgCustomGrid_CloseClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgCustomGrid.CloseClick
        PnlCustomTask.Visible = False
        C1PatientOBPlan.Select(1, Col_HSmokingStatus)
    End Sub

    Private Sub dgCustomGrid_OKClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgCustomGrid.OKClick
        Dim r As Integer = C1PatientOBPlan.RowSel

        Dim Strdata As String = ""
        Dim Strcode As String = ""

        Dim cnt As Integer = 0
        If strbuttonStatus = "Smoking" Then
            For i As Integer = 0 To dgCustomGrid.C1Task.Rows.Count - 1
                If dgCustomGrid.C1Task.GetCellCheck(i, 0) = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                    ''Modified on 20110330-Extra new line was getting added after last reaction in string-so problem was coming in Chk_Modification()
                    If Strdata.Trim = "" Then
                        Strdata = dgCustomGrid.GetItem(i, 2).ToString
                    Else
                        Strdata &= vbNewLine & dgCustomGrid.GetItem(i, 2).ToString
                    End If
                    cnt = cnt + 1
                End If
            Next
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
                    C1PatientOBPlan.Rows(r).Height = C1PatientOBPlan.Rows.DefaultSize * cnt
                    C1PatientOBPlan.SetData(r, Col_HSmokingStatus, Strdata)
                Else
                    C1PatientOBPlan.SetData(r, Col_HSmokingStatus, Strdata)
                    C1PatientOBPlan.Rows(r).Height = C1PatientOBPlan.Rows.DefaultSize * 1     ''For resetting the height 
                End If
            ElseIf strbuttonStatus = "Family Member" Then
                If Strdata <> String.Empty Then
                    C1PatientOBPlan.Rows(r).Height = C1PatientOBPlan.Rows.DefaultSize * cnt
                    C1PatientOBPlan.SetData(r, Col_HsReaction, Strdata)
                    C1PatientOBPlan.SetData(r, "nMemberId", Strcode)
                Else
                    C1PatientOBPlan.SetData(r, Col_HsReaction, Strdata)
                    C1PatientOBPlan.SetData(r, "nMemberId", Strcode)
                    ''for clearing the data 
                    C1PatientOBPlan.Rows(r).Height = C1PatientOBPlan.Rows.DefaultSize * 1     ''For resetting the height 
                End If
            ElseIf strbuttonStatus = "Reaction" Then
                If Strdata <> String.Empty Then
                    C1PatientOBPlan.Rows(r).Height = C1PatientOBPlan.Rows.DefaultSize * cnt
                    C1PatientOBPlan.SetData(r, Col_HsReaction, Strdata)
                Else
                    C1PatientOBPlan.SetData(r, Col_HsReaction, Strdata)
                    ''for clearing the data 
                    C1PatientOBPlan.Rows(r).Height = C1PatientOBPlan.Rows.DefaultSize * 1     ''For resetting the height 
                End If
            ElseIf strbuttonStatus = "ICD9" Then

                C1PatientOBPlan.SetData(r, Col_HsICD9, Strdata)

            ElseIf strbuttonStatus = "CPT" Then

                C1PatientOBPlan.SetData(r, col_HCPT, Strdata)
            End If
        End If

        C1PatientOBPlan.Select(r, Col_HSmokingStatus)
        PnlCustomTask.Visible = False
    End Sub




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


    Private Sub dgCustomGrid_AfterSelChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgCustomGrid.AfterSelChanged
        Dim ind As Integer = dgCustomGrid.GetCurrentrowIndex
        Try
            If strbuttonStatus = "Smoking" And blnchksmk = False Then
                For i As Integer = 0 To dgCustomGrid.C1Task.Rows.Count - 1
                    If dgCustomGrid.C1Task.GetCellCheck(i, 0) = C1.Win.C1FlexGrid.CheckEnum.Checked And i <> ind Then
                        blnchksmk = True
                        dgCustomGrid.C1Task.SetCellCheck(i, 0, CheckEnum.Unchecked)

                    End If
                Next
                blnchksmk = False
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub cmbAllergyType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbAllergyType.SelectedIndexChanged
        Dim myCount As Integer = 0
        Dim myArrLst As New ArrayList()
        myArrLst.Clear()
        '' SUDHIR 20100914 '' TO SHOW ACTIVE/INACTIVE/ALL ALLERGIES WITH COMBO CHANGE ''
        Try
            If C1PatientOBPlan.Rows.Count <= 1 Then
                Exit Sub
            End If
            Select Case cmbAllergyType.SelectedItem.ToString().ToUpper()
                Case "ALL"

                    For iRow As Integer = 1 To C1PatientOBPlan.Rows.Count - 1
                        ' If C1HistoryDetails.GetData(iRow, Col_HHidden).ToString() = "Allergies" Then
                        C1PatientOBPlan.Rows(iRow).Visible = True
                        ' End If
                    Next

                Case "ACTIVE"
                    For iRow As Integer = 1 To C1PatientOBPlan.Rows.Count - 1
                        ' If C1HistoryDetails.GetData(iRow, Col_HHidden).ToString() = "Allergies" Then
                        If C1PatientOBPlan.GetCellCheck(iRow, Col_HsActive) = CheckEnum.Unchecked Then
                            C1PatientOBPlan.Rows(iRow).Visible = False
                        Else
                            C1PatientOBPlan.Rows(iRow).Visible = True
                            ''Adding Active Allegies count.
                            myArrLst.Add(iRow)
                            myCount = iRow
                        End If
                        '  End If

                    Next
                Case "INACTIVE"
                    For iRow As Integer = 1 To C1PatientOBPlan.Rows.Count - 1
                        ' If C1HistoryDetails.GetData(iRow, Col_HHidden).ToString() = "Allergies" Then
                        If C1PatientOBPlan.GetCellCheck(iRow, Col_HsActive) = CheckEnum.Checked Then
                            C1PatientOBPlan.Rows(iRow).Visible = False
                        Else
                            C1PatientOBPlan.Rows(iRow).Visible = True
                        End If
                        ' End If
                    Next

            End Select
            C1PatientOBPlan.Row = 1
            If myCount = 1 Then
                ''If All Allergies are InActive then don't show the Allergy(Text) Category.
                C1PatientOBPlan.Rows(myArrLst.Item(0)).Visible = False
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        End Try
    End Sub


    Public Sub FillOBPlanOnDoubleClick_New(ByVal mynode As gloSnoMed.myTreeNode, ByVal NodeID As Int16, Optional ByVal IsOpenFromImmzn As Boolean = False)
        Dim i, j As Integer ', k As Integer
        Dim _RxNormCode As String = ""
        Dim _NDCCode As String = ""
        Dim rownum As Integer = 0
        Dim IsOnsetDate As Boolean = False
        Dim IsActive As Boolean = False
        Dim stronsetActiveStatus As String = ""
        'Dim _arrOnsetActive() As String
        _NDCCode = mynode.NDCCod
        _RxNormCode = mynode.RxNormID

        RXnormAlergicDrugNDCCode = _NDCCode

        With dsOBPlan.Tables("OBPlan")
            Dim _Row As Integer = 0
            Try

                For i = 0 To .Rows.Count - 1
                    If dsOBPlan.Tables("OBPlan").Rows(i).RowState = DataRowState.Deleted Then
                    Else
                        If .Rows(i)(Col_HHidden).ToString.ToUpper = BtnText.ToUpper Then                    '' TO Check Duplicate History Item in A Category
                            If C1PatientOBPlan.Rows(j)(Col_HsOBPlanItem) = mynode.Text And C1PatientOBPlan.Rows(j)(Col_HHidden).ToString.ToUpper = BtnText.ToUpper Then
                                Exit Sub
                            End If

                            Try
                                If .Rows(i)(Col_HHidden).ToString.ToUpper <> .Rows(i + 1)(Col_HHidden).ToString.ToUpper Then
                                    Dim row As DataRow = dsOBPlan.Tables("OBPlan").NewRow()
                                    .Rows.InsertAt(row, (i + 1))
                                    _Row = i + 1
                                    Exit For
                                End If


                            Catch ex As Exception
                                Dim row As DataRow = dsOBPlan.Tables("OBPlan").NewRow()
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

            AllergyNode = mynode
            AllergyNode.NDCCode = _NDCCode
            'Dim _dataRow As DataRow
            If _Row = 0 Then

                dsOBPlan.Tables("OBPlan").Rows.Add()
                C1PatientOBPlan.Rows(_Row + 1).AllowEditing = False
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
                Else
                    .Rows(_Row)(Col_HsReaction) = ""
                End If


                .Rows(_Row)("nRowOrder") = 0
                dsOBPlan.Tables("OBPlan").Rows.Add(_Row + 1)

                _Row = _Row + 1
            End If
            Dim itemnumber As Int64 = 0
            .Rows(_Row)(Col_HHidden) = ""
            .Rows(_Row)(Col_HsOBPlanItem) = ""
            .Rows(_Row)(Col_HsDosage) = ""
            .Rows(_Row)(Col_HsNDCCode) = ""

            .Rows(_Row)("RowID") = NodeID

            .Rows(_Row)(Col_HsConceptID) = ""
            .Rows(_Row)(Col_HsDescriptionID) = ""
            .Rows(_Row)(Col_HsSnomedID) = ""
            .Rows(_Row)(Col_HsICD9) = ""
            .Rows(_Row)(col_HCPT) = ""
            .Rows(_Row)(Col_HsDescription) = ""
            .Rows(_Row)(Col_HsDrugName) = ""

            .Rows(_Row)(Col_HCategory) = ""
            .Rows(_Row)(Col_HHidden) = BtnText
            .Rows(_Row)(Col_HsOBPlanItem) = AllergyNode.Text
            .Rows(_Row)(Col_HsDrugName) = AllergyNode.Text
            .Rows(_Row)(Col_HsDosage) = AllergyNode.Dosage
            .Rows(_Row)(Col_HsNDCCode) = AllergyNode.NDCCode
            .Rows(_Row)("RowID") = NodeID
            .Rows(_Row)(Col_HDOE_Allergy) = DateTime.Now.ToString()


            .Rows(_Row)(Col_HsConceptID) = mynode.ConceptID
            .Rows(_Row)(Col_HsDescriptionID) = mynode.DescriptionID
            .Rows(_Row)(Col_HsSnomedID) = mynode.SnoMedID
            '.Rows(_Row)(Col_HsICD9) = _ICD91
            .Rows(_Row)(Col_HsDescription) = _Defination1
            .Rows(_Row)("nOBPlanID") = 0
            .Rows(_Row)(Col_HsComments) = mynode.Comments
            .Rows(_Row)(Col_HsICD9) = mynode.ICD9
            .Rows(_Row)(col_HCPT) = mynode.CPT
            .Rows(_Row)("sOBPlanType") = mynode.HistoryType

            '29-Mar-13 Aniket: Addition of source column on the History screen
            .Rows(_Row)("sOBPlanSource") = strOBPlanSource
            .Rows(_Row)("nICDRevision") = mynode.nICDRevision      ''Bug #65177: added for ICD10 implementation
            If mynode.ConceptID.Trim = "0" Then
                lblconcptid.Text = ""
            Else
                lblconcptid.Text = mynode.ConceptID
            End If
            lbldescid.Text = mynode.DescriptionID
            'Code Start-Added by kanchan on 20100826 for rxnorm & ndc code functionality
            'lblSnomedID.Text = mynode.SnoMedID
            lblRxNorm.Text = mynode.RxNormID
            lblNDCid.Text = mynode.NDCCod
            .Rows(_Row)(Col_HsNDCCode) = _NDCCode
            .Rows(_Row)(Col_HsRxNormID) = _RxNormCode
            Dim s As Integer
            Dim _currentrowitem As String
            Dim _rowcount As Integer

            _currentrowitem = Convert.ToString(dsOBPlan.Tables("OBPlan").Rows(_Row)(Col_HsOBPlanItem))
            For s = 0 To C1PatientOBPlan.Rows.Count - 1
                If Convert.ToString(C1PatientOBPlan.Rows(s)(Col_HsOBPlanItem)) = _currentrowitem And Convert.ToString(C1PatientOBPlan.Rows(s)("RowID")) = "1" Then
                    _rowcount = s
                    .Rows(_Row)("RowID") = 0
                    NodeID = 0
                    Exit For
                End If
            Next

            Dim dv As DataView  ''slr new not needed 
            If dsOBPlan.Tables("OBPlan").Rows.Count > 0 Then
                dv = dsOBPlan.Tables("OBPlan").DefaultView
                dv.Sort = "nRowOrder ASC"
                If dv.ToTable.Rows.Count > 0 Then

                    itemnumber = dv.ToTable.Rows(dv.ToTable.Rows.Count - 1)("nRowOrder")


                End If


            End If


            dsOBPlan.Tables("OBPlan").Rows(_Row)("nRowOrder") = itemnumber + 1
            Dim _categorytype As String = ""
            _categorytype = Convert.ToString(.Rows(_Row)(Col_HHidden)).Trim
            IsOnsetDate = True
            IsActive = True
            If IsOnsetDate = True Then


                Dim cs As CellStyle '= C1HistoryDetails.Styles.Add("DateTime")
                Try
                    If (C1PatientOBPlan.Styles.Contains("DateTime")) Then
                        cs = C1PatientOBPlan.Styles("DateTime")
                    Else
                        cs = C1PatientOBPlan.Styles.Add("DateTime")

                    End If
                Catch ex As Exception
                    cs = C1PatientOBPlan.Styles.Add("DateTime")

                End Try
                cs.DataType = GetType(DateTime)
                cs.TextAlign = TextAlignEnum.CenterCenter
                cs.ImageAlign = ImageAlignEnum.CenterCenter
                C1PatientOBPlan.SetCellStyle(_rowcount, col_HOnsetDate, cs)
                '  End If
            End If



            'If InStr(BtnText, "Allerg", CompareMethod.Text) = 1 Then
            If IsActive = True And _categorytype = "All" Then
                '' Category is Allergy Then Only we have to show Reactions & Active/Inactive CheckBOX                           
                Dim cStyle As C1.Win.C1FlexGrid.CellStyle = Nothing
                Dim rgReaction As C1.Win.C1FlexGrid.CellRange = C1PatientOBPlan.GetCellRange(_rowcount, Col_HButton, _rowcount, Col_HButton)
                Dim rgActive As C1.Win.C1FlexGrid.CellRange = C1PatientOBPlan.GetCellRange(_rowcount, Col_HsActive, _rowcount, Col_HsActive)

                rgReaction.Style = cStyle
                rgActive.StyleNew.DataType = GetType(Boolean)
                rgActive.StyleNew.TextAlign = TextAlignEnum.CenterCenter
                rgActive.StyleNew.ImageAlign = ImageAlignEnum.CenterCenter
                rgActive.Checkbox = CheckEnum.Checked
                C1PatientOBPlan.SetCellCheck(_rowcount, Col_HsActive, CheckEnum.Checked)
                .Rows(_Row)(Col_HsActive) = True
                C1PatientOBPlan.SetCellImage(_rowcount, Col_HButton, imgTreeVIew.Images(5)) '' Chetan Added
            ElseIf _categorytype = "Fam" Then
                '' Category is Allergy Then Only we have to show Reactions & Active/Inactive CheckBOX                           
                Dim cStyle As C1.Win.C1FlexGrid.CellStyle = Nothing
                Dim rgReaction As C1.Win.C1FlexGrid.CellRange = C1PatientOBPlan.GetCellRange(_rowcount, Col_HButton, _rowcount, Col_HButton)
                Dim rgActive As C1.Win.C1FlexGrid.CellRange = C1PatientOBPlan.GetCellRange(_rowcount, Col_HsActive, _rowcount, Col_HsActive)
                If IsActive Then


                    rgReaction.Style = cStyle
                    rgActive.StyleNew.DataType = GetType(Boolean)
                    rgActive.StyleNew.TextAlign = TextAlignEnum.CenterCenter
                    rgActive.StyleNew.ImageAlign = ImageAlignEnum.CenterCenter
                    rgActive.Checkbox = CheckEnum.Checked
                    C1PatientOBPlan.SetCellCheck(_rowcount, Col_HsActive, CheckEnum.Checked)
                    .Rows(_Row)(Col_HsActive) = True
                End If

                C1PatientOBPlan.SetCellImage(_rowcount, Col_HButton, imgTreeVIew.Images(5)) '' Chetan Added
            ElseIf BtnText.ToString.Trim = "Smoking Status" Then '' CODE FOR THE '' SMOKING ''


                Dim strReactions As String = ""
                Dim objclsPatientHistory As New clsPatientHistory
                strReactions = objclsPatientHistory.GetSmokingStatus(mynode.Key)
                .Rows(_Row)(Col_HSmokingStatus) = strReactions

                Dim rgBrowseBtn As C1.Win.C1FlexGrid.CellRange = C1PatientOBPlan.GetCellRange(_rowcount, Col_HSmokeButton, _rowcount, Col_HSmokeButton)
                C1PatientOBPlan.SetCellImage(_rowcount, Col_HSmokeButton, imgTreeVIew.Images(5))
                C1PatientOBPlan.SetData(_rowcount, Col_HSmokeButton, strReactions)
                objclsPatientHistory = Nothing

            ElseIf IsActive = True Then
                'Dim cStyle As C1.Win.C1FlexGrid.CellStyle
                Dim rgActive As C1.Win.C1FlexGrid.CellRange = C1PatientOBPlan.GetCellRange(_rowcount, Col_HsActive, _rowcount, Col_HsActive)
                rgActive.StyleNew.DataType = GetType(Boolean)
                rgActive.StyleNew.TextAlign = TextAlignEnum.CenterCenter
                rgActive.StyleNew.ImageAlign = ImageAlignEnum.CenterCenter
                rgActive.Checkbox = CheckEnum.Checked
                .Rows(_Row)(Col_HsActive) = True
                C1PatientOBPlan.SetCellCheck(_rowcount, Col_HsActive, CheckEnum.Checked)
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
            C1PatientOBPlan.Row = _rowcount
            gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.OBPlan, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Add, "New History Item Added", gloAuditTrail.ActivityOutCome.Success)
        End With

        For iRow As Integer = 0 To C1PatientOBPlan.Rows.Count - 1
            If Convert.ToString(C1PatientOBPlan.Rows(iRow)(Col_HCategory)) <> "" And Convert.ToString(C1PatientOBPlan.Rows(iRow)(Col_HsReaction)) <> "" Then
                Dim asgTask1 As C1.Win.C1FlexGrid.CellStyle '= C1HistoryDetails.Styles.Add("asgTask")
                Try
                    If (C1PatientOBPlan.Styles.Contains("asgTask")) Then
                        asgTask1 = C1PatientOBPlan.Styles("asgTask")
                    Else
                        asgTask1 = C1PatientOBPlan.Styles.Add("asgTask")
                        asgTask1.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD 'New System.Drawing.Font(C1HistoryDetails.Font.FontFamily.Name, 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(0))

                    End If
                Catch ex As Exception
                    asgTask1 = C1PatientOBPlan.Styles.Add("asgTask")
                    asgTask1.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD 'New System.Drawing.Font(C1HistoryDetails.Font.FontFamily.Name, 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(0))

                End Try
                C1PatientOBPlan.SetCellStyle(iRow, Col_HsReaction, asgTask1)
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


        If gblnResetSearchTextBox = True Then
            GloUC_trvOBPlan.txtsearch.ResetText()
        End If
    End Sub


    Private Sub FillDataInGrid(Optional ByVal dv As DataView = Nothing, Optional ByVal OBNode As gloUserControlLibrary.myTreeNode = Nothing)
        Try
            Dim oNode As gloUserControlLibrary.myTreeNode
            If Not IsNothing(OBNode) Then
                oNode = OBNode
            Else
                oNode = CType(GloUC_trvOBPlan.SelectedNode, gloUserControlLibrary.myTreeNode)
            End If
            If CheckOBPlanItemAlreadyExists(oNode, dv) = True Then
                Exit Sub
            End If

            Dim dt As DataTable  ''slr new not needed 

            If Not IsNothing(oNode) Then
                Dim mynode As New gloSnoMed.myTreeNode
                mynode.Text = oNode.Text
                mynode.Tag = oNode.ConceptID
                mynode.ICD9 = oNode.ICD9
                mynode.CPT = oNode.CPT
                mynode.HistoryType = oNode.HistoryType
                mynode.Key = oNode.ID


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


                            dt = objclsPatientOBPlan.FillDetailsFromMaster(oNode.ConceptID, GloUC_trvOBPlan.SelectedNode.Text)
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
                                            '  FillICD9InDetail(dt)
                                        Else
                                            _ICD91 = ""
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If
                FillOBPlanOnDoubleClick_New(mynode, 1, False)
            End If
            C1PatientOBPlan.SetDataBinding(dsOBPlan, "OBPlan")
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

            ''slr free dt
            If Not IsNothing(dt) Then
                dt.Dispose()
                dt = Nothing
            End If
        End Try
    End Sub



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

    
    Private Enum OBPlanEnum
        sOBPlanCategory
        Hidden
        sOBPlanItem
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
        nOBPlanID
        nRowOrder
        sOBPlanType
        RowID

        sOBPlanSource
        nICDRevision

    End Enum

    Public ReadOnly Property GetCurrentPatientID() As Int64 Implements mdlGeneral.IPatientContext.GetCurrentPatientID
        Get
            Return m_PatientID    'Curent patient variable(Local variable) for this module 
        End Get
    End Property

    Private Sub On_frmPrescription_Closed(ByVal sender As Object, ByVal e As FormClosedEventArgs)
        frmPrescription_Closed()
        Dim frm As frmPrescription = Nothing

        Try
            frm = DirectCast(sender, frmPrescription)
        Catch ex As Exception

        End Try
        Try
            If (IsNothing(frm) = False) Then
                RemoveHandler frm.FormClosed, AddressOf On_frmPrescription_Closed
            End If
            If (IsNothing(frm) = False) Then
                frm.Close()
            End If
            If (IsNothing(frm) = False) Then
                frm.Dispose()
                frm = Nothing
            End If

        Catch ex As Exception

        End Try

    End Sub

    Public Sub frmPrescription_Closed()
        If Me.myCallerSynopsis IsNot Nothing Then
            Dim ofrm As frmPatientSynopsis
            ofrm = CType(Me.myCallerSynopsis, Form)
            ofrm.frmPrecription_Closed(Nothing, Nothing)
        End If
    End Sub


    Private Sub oCPTListControl_ItemSelectedClick(ByVal sender As Object, ByVal e As EventArgs)
        Dim Strdata As String = ""
        Try
            If oCPTListControl.SelectedItems.Count > 0 Then
                For i As Int16 = 0 To oCPTListControl.SelectedItems.Count - 1
                    lnlLbllCPTCode.Text = oCPTListControl.SelectedItems(i).Code
                    Strdata = oCPTListControl.SelectedItems(i).Code & " : " & oCPTListControl.SelectedItems(i).Description

                    C1PatientOBPlan.SetData(C1PatientOBPlan.Row, col_HCPT, Strdata)

                Next
                ofrmCPTList.Close()
            Else
                lnlLbllCPTCode.Text = ""
                C1PatientOBPlan.SetData(C1PatientOBPlan.Row, col_HCPT, "")
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

                    C1PatientOBPlan.SetData(C1PatientOBPlan.Row, Col_HsICD9, Strdata)

                Next
                C1PatientOBPlan.SetData(C1PatientOBPlan.Row, "nICDRevision", oDiagnosisListControl.IsICD9_10)    ''added for ICD10 implementation
                ofrmDiagnosisList.Close()
            Else
                linkLblICD9code.Text = ""
                C1PatientOBPlan.SetData(C1PatientOBPlan.Row, Col_HsICD9, "")
                C1PatientOBPlan.SetData(C1PatientOBPlan.Row, "nICDRevision", 0)   ''added for ICD10 implementation
                ofrmDiagnosisList.Close()
            End If
            ConceptDesc = Convert.ToString(C1PatientOBPlan.GetData(C1PatientOBPlan.Row, Col_HsOBPlanItem))
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
        Dim frm As New gloSnoMed.FrmSelectProblem("OBPlan", gstrSMDBConnstr, GetConnectionString())
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
                        C1PatientOBPlan.SetData(C1PatientOBPlan.Row, Col_HsConceptID, lblconcptid.Text)

                    Else
                        lblconcptid.Text = Convert.ToString(frm.strConceptID) '.ToString()
                        C1PatientOBPlan.SetData(C1PatientOBPlan.Row, Col_HsConceptID, frm.strConceptID)
                    End If
                Else
                    lblconcptid.Text = frm.strConceptID
                    C1PatientOBPlan.SetData(C1PatientOBPlan.Row, Col_HsConceptID, frm.strConceptID)
                End If

                lbldescid.Text = frm.strDescriptionID
                'lblSnomedID.Text = frm.StrSnoMedID
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
                C1PatientOBPlan.SetData(C1PatientOBPlan.Row, "nICDRevision", gloGlobal.gloICD.CodeRevision.ICD9)
                If ICD9 <> "" Then
                    C1PatientOBPlan.SetData(C1PatientOBPlan.Row, Col_HsICD9, frm.strICD9)
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

                ConceptDesc = Convert.ToString(C1PatientOBPlan.GetData(C1PatientOBPlan.Row, Col_HsOBPlanItem))
                lblRxNorm.Text = frm.strRxNormCode
                lblNDCid.Text = frm.strNDCCode

                C1PatientOBPlan.SetData(C1PatientOBPlan.Row, Col_HsDescriptionID, lbldescid.Text)
                C1PatientOBPlan.SetData(C1PatientOBPlan.Row, Col_HsDescription, frm.strSelectedDescription) ''8020 snomed changes
                ' C1PatientOBPlan.SetData(C1PatientOBPlan.Row, Col_HsSnomedID, lblSnomedID.Text)
                C1PatientOBPlan.SetData(C1PatientOBPlan.Row, Col_HsNDCCode, lblNDCid.Text)
                C1PatientOBPlan.SetData(C1PatientOBPlan.Row, Col_HsRxNormID, lblRxNorm.Text)
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
        'Dim _arrOnsetActive() As String
        Dim IsOnsetDate As Boolean = False
        Dim IsActive As Boolean = False
        Try
            _selrow = C1PatientOBPlan.Row
            '_NewRow = C1HistoryDetails.Row - 2
            _NewRow = _selrow - 1
            For j As Integer = C1PatientOBPlan.Row To 1 Step -1
                If _NewRow > 1 Then
                    If C1PatientOBPlan.Rows(_NewRow).IsVisible Then
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
            _ID = dsOBPlan.Tables("OBPlan").Rows(_selrow)("nRowOrder")
            _ID1 = dsOBPlan.Tables("OBPlan").Rows(_NewRow)("nRowOrder")
            dsOBPlan.Tables("OBPlan").Rows(_NewRow)("nRowOrder") = _ID
            dsOBPlan.Tables("OBPlan").Rows(_selrow)("nRowOrder") = _ID1




            If C1PatientOBPlan.Row > 0 Then

                If dsOBPlan.Tables("OBPlan").Rows(_NewRow)(Col_HCategory) = "" Then

                    Dim moveow As DataRow = dsOBPlan.Tables("OBPlan").Rows(_NewRow)
                    Dim selectedRow As DataRow = dsOBPlan.Tables("OBPlan").Rows(_selrow)

                    Dim OldRow As DataRow = dsOBPlan.Tables("OBPlan").NewRow()
                    OldRow.ItemArray = moveow.ItemArray
                    dsOBPlan.Tables("OBPlan").Rows.InsertAt(OldRow, _selrow)

                    Dim newRow As DataRow = dsOBPlan.Tables("OBPlan").NewRow()
                    newRow.ItemArray = selectedRow.ItemArray
                    dsOBPlan.Tables("OBPlan").Rows.InsertAt(newRow, _NewRow)


                    dsOBPlan.Tables("OBPlan").Rows.Remove(moveow)
                    dsOBPlan.Tables("OBPlan").Rows.Remove(selectedRow)
                    _selrow = _selrow + 1
                    For f As Integer = 0 To 1
                        _categorytype = Convert.ToString(C1PatientOBPlan.GetData(_selrow, Col_HHidden)).Trim

                        IsActive = True
                        IsOnsetDate = True
                        If IsOnsetDate = True Then


                            Dim cs As CellStyle '= C1HistoryDetails.Styles.Add("DateTime")
                            Try
                                If (C1PatientOBPlan.Styles.Contains("DateTime")) Then
                                    cs = C1PatientOBPlan.Styles("DateTime")
                                Else
                                    cs = C1PatientOBPlan.Styles.Add("DateTime")

                                End If
                            Catch ex As Exception
                                cs = C1PatientOBPlan.Styles.Add("DateTime")

                            End Try
                            cs.DataType = GetType(DateTime)
                            cs.TextAlign = TextAlignEnum.CenterCenter
                            cs.ImageAlign = ImageAlignEnum.CenterCenter
                            C1PatientOBPlan.SetCellStyle(_selrow, col_HOnsetDate, cs)
                            'End If
                        End If

                        'If InStr(C1HistoryDetails.GetData(_selrow, Col_HHidden), "Allerg", CompareMethod.Text) = 1 Then
                        If IsActive = True Then
                            ' Dim cStyle As C1.Win.C1FlexGrid.CellStyle

                            Dim strReaction As String
                            Dim strActive As String
                            Dim rgActive As C1.Win.C1FlexGrid.CellRange = C1PatientOBPlan.GetCellRange(_selrow, Col_HsActive, _selrow, Col_HsActive)
                            Dim arr() As String 'Srting Array
                            arr = Split(Convert.ToString(C1PatientOBPlan.Rows(_selrow)(Col_HsReaction)), "|")
                            If arr.Length = 2 Then
                                strReaction = arr.GetValue(0)
                                strActive = Convert.ToString(C1PatientOBPlan.Rows(_selrow)(Col_HsActive))
                            Else
                                strReaction = Convert.ToString(C1PatientOBPlan.Rows(_selrow)(Col_HsReaction))
                                strActive = Convert.ToString(C1PatientOBPlan.Rows(_selrow)(Col_HsActive))
                            End If


                            rgActive.StyleNew.DataType = GetType(Boolean)
                            rgActive.StyleNew.TextAlign = TextAlignEnum.CenterCenter
                            rgActive.StyleNew.ImageAlign = ImageAlignEnum.CenterCenter
                            rgActive.Checkbox = CheckEnum.Checked
                            If strActive = "True" Then
                                C1PatientOBPlan.SetCellCheck(_selrow, Col_HsActive, CheckEnum.Checked)
                            Else
                                C1PatientOBPlan.SetCellCheck(_selrow, Col_HsActive, CheckEnum.Unchecked)
                            End If

                        End If
                        _selrow = _NewRow + 1
                    Next
                    C1PatientOBPlan.Row = _selrow
                End If

            End If
            For k As Integer = 0 To dsOBPlan.Tables("OBPlan").Rows.Count - 1
                If dsOBPlan.Tables("OBPlan").Rows(k).RowState <> DataRowState.Deleted Then
                    dsOBPlan.Tables("OBPlan").Rows(k)("RowState") = "Added"
                End If
            Next

            C1PatientOBPlan_EnterCell(Nothing, Nothing)

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.OBPlan, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Remove, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            '  MessageBox.Show(ex.ToString, "Patient OBPlan", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnDown_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDown.Click

        Try
            Dim stronsetActiveStatus As String = ""
            'Dim _arrOnsetActive() As String
            Dim IsOnsetDate As Boolean = False
            Dim IsActive As Boolean = False
            Dim _selrow As Integer = 0
            Dim _newRow As Integer = 0
            Dim _ID1 As Int64
            Dim _ID As Int64
            Dim _categorytype As String = ""
            ' For i As Integer = 0 To dsHistory.Tables("OBPlan").Rows.Count - 1
            '_selrow = C1HistoryDetails.Row - 1
            '_newRow = C1HistoryDetails.Row
            _selrow = C1PatientOBPlan.Row
            _newRow = C1PatientOBPlan.Row + 1
            Dim _strcategory As String = ""
            _strcategory = Convert.ToString(C1PatientOBPlan.Rows(_selrow)(Col_HHidden))
            For j As Integer = C1PatientOBPlan.Row To C1PatientOBPlan.Rows.Count - 1
                If _strcategory = Convert.ToString(C1PatientOBPlan.Rows(_newRow)(Col_HHidden)) Then
                    If _newRow > 1 Then
                        If C1PatientOBPlan.Rows(_newRow).IsVisible Then
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
            _ID = dsOBPlan.Tables("OBPlan").Rows(_selrow)("nRowOrder")
            _ID1 = dsOBPlan.Tables("OBPlan").Rows(_newRow)("nRowOrder")
            dsOBPlan.Tables("OBPlan").Rows(_newRow)("nRowOrder") = _ID
            dsOBPlan.Tables("OBPlan").Rows(_selrow)("nRowOrder") = _ID1

            If C1PatientOBPlan.Row > 0 Then
                If dsOBPlan.Tables("OBPlan").Rows(_selrow)(Col_HHidden).ToString.ToUpper <> dsOBPlan.Tables("OBPlan").Rows(_selrow + 1)(Col_HCategory).ToString.ToUpper Then

                    Dim moverow As DataRow = dsOBPlan.Tables("OBPlan").Rows(_newRow)
                    Dim selectedRow As DataRow = dsOBPlan.Tables("OBPlan").Rows(_selrow)
                    Dim newRow As DataRow = dsOBPlan.Tables("OBPlan").NewRow()
                    newRow.ItemArray = selectedRow.ItemArray
                    Dim OldRow As DataRow = dsOBPlan.Tables("OBPlan").NewRow()
                    OldRow.ItemArray = moverow.ItemArray


                    dsOBPlan.Tables("OBPlan").Rows.InsertAt(newRow, _newRow)
                    dsOBPlan.Tables("OBPlan").Rows.InsertAt(OldRow, _selrow)
                    dsOBPlan.Tables("OBPlan").Rows.Remove(selectedRow)
                    dsOBPlan.Tables("OBPlan").Rows.Remove(moverow)
                    _selrow = _selrow + 1
                    For f As Integer = 0 To 1
                        _categorytype = Convert.ToString(C1PatientOBPlan.GetData(_selrow, Col_HHidden)).Trim

                        IsActive = True
                        IsOnsetDate = True

                        If IsOnsetDate = True Then
                            Dim cs As CellStyle '= C1HistoryDetails.Styles.Add("DateTime")
                            Try
                                If (C1PatientOBPlan.Styles.Contains("DateTime")) Then
                                    cs = C1PatientOBPlan.Styles("DateTime")
                                Else
                                    cs = C1PatientOBPlan.Styles.Add("DateTime")

                                End If
                            Catch ex As Exception
                                cs = C1PatientOBPlan.Styles.Add("DateTime")

                            End Try
                            cs.DataType = GetType(DateTime)
                            cs.TextAlign = TextAlignEnum.CenterCenter
                            cs.ImageAlign = ImageAlignEnum.CenterCenter
                            C1PatientOBPlan.SetCellStyle(_selrow, col_HOnsetDate, cs)
                        End If

                        If IsActive = True Then

                            Dim strReaction As String
                            Dim strActive As String
                            Dim rgActive As C1.Win.C1FlexGrid.CellRange = C1PatientOBPlan.GetCellRange(_selrow, Col_HsActive, _selrow, Col_HsActive)
                            Dim arr() As String 'Srting Array
                            arr = Split(Convert.ToString(C1PatientOBPlan.Rows(_selrow)(Col_HsReaction)), "|")
                            If arr.Length = 2 Then
                                strReaction = arr.GetValue(0)
                                strActive = Convert.ToString(C1PatientOBPlan.Rows(_selrow)(Col_HsActive))
                            Else
                                strReaction = Convert.ToString(C1PatientOBPlan.Rows(_selrow)(Col_HsReaction))
                                strActive = Convert.ToString(C1PatientOBPlan.Rows(_selrow)(Col_HsActive))
                            End If


                            rgActive.StyleNew.DataType = GetType(Boolean)
                            rgActive.StyleNew.TextAlign = TextAlignEnum.CenterCenter
                            rgActive.StyleNew.ImageAlign = ImageAlignEnum.CenterCenter
                            rgActive.Checkbox = CheckEnum.Checked
                            If strActive = "True" Then
                                C1PatientOBPlan.SetCellCheck(_selrow, Col_HsActive, CheckEnum.Checked)
                            Else
                                C1PatientOBPlan.SetCellCheck(_selrow, Col_HsActive, CheckEnum.Unchecked)
                            End If


                        End If
                        ' _selrow = _newRow
                        _selrow = _newRow + 1
                    Next
                    C1PatientOBPlan.Row = _selrow
                End If
            End If
            For k As Integer = 0 To dsOBPlan.Tables("OBPlan").Rows.Count - 1
                If dsOBPlan.Tables("OBPlan").Rows(k).RowState <> DataRowState.Deleted Then
                    dsOBPlan.Tables("OBPlan").Rows(k)("RowState") = "Added"
                End If

            Next
            C1PatientOBPlan_EnterCell(Nothing, Nothing)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.OBPlan, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Remove, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ' MessageBox.Show(ex.ToString, "Patient OBPlan", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

End Class

#End Region
