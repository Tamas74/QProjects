Imports System.IO
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Net
Imports System.Linq
Imports System.ComponentModel
Imports System.Data.SqlClient
Imports System.Runtime.InteropServices
Imports System.Threading.Tasks

Imports gloEMRGeneralLibrary.gloGeneral
Imports gloEMRGeneralLibrary.gloEMRActors
Imports gloEMRGeneralLibrary.gloEMRPrescription
Imports gloEMRGeneralLibrary.gloEMRMedication
Imports gloEMRGeneralLibrary.gloEMRDatabase
Imports gloRxHub
Imports gloUserControlLibrary
Imports gloUIControlLibrary.WPFUserControl

Imports gloGlobal
Imports gloGlobal.Common
Imports gloGlobal.FS3
Imports gloGlobal.DI
Imports gloGlobal.PDR
Imports gloGlobal.Schemas.PDR

Imports gloWord
Imports gloEMR.gloEMRWord
Imports wd = Microsoft.Office.Interop.Word

Imports gloMeds.Core
Imports gloSureScript.PrescriptionMedicationEventArgs
Imports LocalSchema = gloGlobal.SS
Imports schema = gloGlobal.Schemas.Surescript
Imports System.Threading
Imports System.Xml.Linq

Public Class frmPrescription
    Implements IPatientContext

    <DllImport("user32.dll")> _
    Private Shared Function LockWindowUpdate(hWnd As IntPtr) As Boolean
    End Function
    Dim blnFaxLog As Boolean = False ''For Log Implementation Of Fax: Bug #107114 
    Dim IsFormLoaded As Boolean = False
    Dim LoadError As Boolean = False 'added for Prescription Provider issue Bug #46975 in 7022
    Private IsOpenedFromPrescription As Boolean
    Dim IsSendRx As Boolean = False
    Private isbuttonClickSave As Boolean = False
    Public blnFlagIsPrintSuccessFull As Boolean = True ''added to check Print Success or fail
    Public blnOpenFromLetter As Boolean = False
    Public blnOpenFromPTProtocol As Boolean = False
    Public blnOpenFromExam As Boolean = False
    Public blnOpenFromMessage As Boolean = False
    Public blnOpenFromPatientConsent As Boolean = False
    Public blnOpenFromNurseNotes As Boolean = False
    Private blnShowCarryForwardMessage As Boolean = True
    Public IsfrmDM As Boolean = False
    Public IsfrmPatientSavings As Boolean = False
    Private _ismsgdisplay As Boolean = False
    Private _isSupervisormessagedisplay As Boolean = False
    Private _isSaveClicked As Boolean = False
    Dim IsPharmacyEnabled As Boolean = False
    Dim IsPharmacyActivated As Boolean = False
    Public blnOpenPre As Boolean = False
    Dim dtPDMP As DataTable = Nothing
    'Dim isDiscardAllclick As Boolean = False
    Dim ToAttempteRx As Boolean = False
    Dim isUnspecifiedMessageOccured As Boolean = False
    Dim isRxFlexGridClick As Boolean = False

    Private InvalidSurescriptMessage As Boolean = False
    Dim _IsPrintImmediately As Boolean = False
    Dim _IsOpenPrintDialog As Boolean = False

    Private IsFormLocked As Boolean = False
    Private blnRxC1FlexClick As Boolean = True
    Private blnIsJuniorProvider As Boolean = False
    Private blnRequireSupervisingProvider As Boolean = False
    Private blnMultipleSupervisor As Boolean = True
    Private isEndDuedateMessageOccured As Boolean = False
    Private isUnIssuedAlertOccured As Boolean = False
    Private isOverrideAlertOccured As Boolean = False


    'Private ActiveModule As ModuleName
    Dim nRxModulePatientID As Long = 0
    Dim nReconcillationType As Int16 = 2
    Dim str_eLink As String = ""
    Dim WithEvents oViewDocument As gloEDocumentV3.gloEDocV3Management
    Dim lDMSDocumentID As Long, lDMSContainerID As Long
    Dim sDMSScanCategory As String = ""


    Dim NARXScoreControl As gloNARXScores = Nothing
    Dim NARXScore As gloGlobal.PDMP.Meds.NARXScores = Nothing
    Dim AlternativesControl As gloFormularyAlternatives = Nothing
    Dim CopayControl As gloCopayControl = Nothing
    Dim _nAddDrugtoMx As Integer = 0
    Dim nPatientProviderId As Long = 0

    Public myCaller As frmPatientExam
    Public myCaller1 As Object
    Public myLetter As frmPatientLetter
    Public myCallerSynopsis As frmPatientSynopsis
    Public myCallerL As frmPatientLetter
    Public myCallerPT As frmPTProtocols
    Public myCallerM As frmMessages
    Public myCallerC As frmPatientConsent
    Public myCallerN As frmNurseNotes

    Private rowindex As Integer
    Private GridStatus As Int16

    Dim dt As DataTable

    Private sigid As Long = 0


    'Dim strFileName As String = gstrgloEMRStartupPath & "\Temp\PrescriptionReport.xml"
    Friend WithEvents dtpVisitDate As System.Windows.Forms.DateTimePicker

    'Private worker As BackgroundWorker



    'Dim nDeniedWithNewRxItem As Integer = 0
    Private ofrmRxRequest As frmRxRequest
    Private ofrmError As frmVWErrorMessage
    Public mycallerpre As frmProblemList


    'Dim RefillRequestDataset As DataSet = Nothing
    Dim oProvider As clsProvider

    Private strPatientCode As String = ""
    Private strPatientFirstName As String = ""
    Private strPatientMiddleName As String = ""
    Private strPatientLastName As String = ""
    Private strPatientDOB As String = ""
    Private strPatientAge As String = ""
    Private strPatientGender As String = ""
    Private strPatientMaritalStatus As String = ""
    Private strPatientAddress As String = ""
    Private strPatientAddress2 As String = ""
    Private strPatientCity As String = ""
    Private strPatientState As String = ""
    Private strPatientZip As String = ""
    Private strPatientPhone As String = ""
    Private strPatientFax As String = ""
    Private strPatientEmail As String = ""
    Private strPatientSsn As String = ""
    Private strPatientCountry As String = ""
    Private strPatientCommunicationPreference As String = ""

    Private strPrescriberFirstName As String = ""
    Private strPrescriberMiddleName As String = ""
    Private strPrescriberLastName As String = ""
    Private strPrescriberAddress As String = ""
    Private strPrescriberAddress2 As String = ""
    Private strPrescriberCity As String = ""
    Private strPrescriberState As String = ""
    Private strPrescriberCountry As String = ""
    Private strPrescriberZip As String = ""
    Private strPrescriberPhone As String = ""
    Private strPrescriberFax As String = ""
    Private strPrescriberNPI As String = ""
    Private strPrescriberDEA As String = ""
    Private strPrescriberNADEAN As String = ""
    Private strPrescriberSSN As String = ""
    Private strPrescriberSPI As String = ""
    Private strServiceLevel As String = ""
    Private strPrescriberEmail As String = ""
    Private strSupervisingPrescriberFirstName As String = ""
    Private strSupervisingPrescriberMiddleName As String = ""
    Private strSupervisingPrescriberLastName As String = ""
    Private strSupervisingPrescriberAddress As String = ""
    Private strSupervisingPrescriberAddress2 As String = ""
    Private strSupervisingPrescriberCity As String = ""
    Private strSupervisingPrescriberState As String = ""
    Private strSupervisingPrescriberZip As String = ""
    Private strSupervisingPrescriberPhone As String = ""
    Private strSupervisingPrescriberFax As String = ""
    Private strSupervisingPrescriberNPI As String = ""
    Private strSupervisingPrescriberDEA As String = ""
    Private strSupervisingPrescriberSSN As String = ""


    Public WithEvents uiPanSplitScreen_PatientPrescription As Janus.Windows.UI.Dock.UIPanelGroup
    Dim clsSplit_PatientPrescription As gloEMRGeneralLibrary.clsSplitScreen = Nothing
    Dim objCriteria As DocCriteria = Nothing
    Dim ObjWord As clsWordDocument = Nothing
    Dim dtMedRec As DataTable = Nothing  ' Used For Medical reconcilation
    Dim ToolTip2 As ToolTip = Nothing




    Private WithEvents objScreeningResults As ScreeningResults
    Private WithEvents objDrugInteraction As DIToolbar

    Private arrDrugs As ArrayList
    Dim btntype As Int16

    Private WithEvents objChiefcomplaints As gloUC_CustomSearchInC1Flexgrid
    Private ReferralCount As Int64
    Dim dtPharmacyDetails As New DataTable


    Dim nProviderAssociationID As Int64 = 0
    Dim sProviderTaxID As String = ""


    Private LockID As Int64 = 0

    Public blncancel As Boolean 'used from dashboard


    'Public dvNext As DataView

    Dim patientAllergicMeds As List(Of String) = Nothing
    Dim patientDiagnosis As New List(Of gloGlobal.DI.dxItem)
    Dim patientProblems As DataTable = Nothing

    Dim nRxRequestTransactionID As Long = 0

    Private _RxChangeRequest As LocalSchema.RxChangeRequest
    Dim blnmedRecupdated As Boolean = False



    Public Property RxChangeRequest() As LocalSchema.RxChangeRequest
        Get
            Return _RxChangeRequest
        End Get
        Set(ByVal value As LocalSchema.RxChangeRequest)
            If value Is Nothing Then
                Me.RefreshOtherForms()
            End If
            _RxChangeRequest = value
        End Set
    End Property


    Public Enum ModuleName
        Prescription
        Medication
    End Enum

    Public Enum RxCommands
        None
        Print
        Fax
        eRx
        IssueRx
        Save
        SaveNClose
    End Enum

    Public Enum OTCDrugAction
        IncludeOTC
        ExcludeOTC
        Cancel
        None
    End Enum

    Public Enum NewRxType
        NewRx
        Renew
        PrevRx
        AcceptAlt
        External
        ProcessRxElig
        PBMChange
    End Enum

    Private _pharmacyInfo As DataRow = Nothing
    Public Property PharmacyInfo() As DataRow
        Get
            Return _pharmacyInfo
        End Get
        Set(ByVal value As DataRow)
            _pharmacyInfo = value
        End Set
    End Property

    Private _patientInfo As DataRow = Nothing
    Public Property PatientInfo() As DataRow
        Get
            Return _patientInfo
        End Get
        Set(ByVal value As DataRow)
            _patientInfo = value
        End Set
    End Property

    Private _providerInfo As DataRow = Nothing
    Public Property ProviderInfo() As DataRow
        Get
            Return _providerInfo
        End Get
        Set(ByVal value As DataRow)
            _providerInfo = value
        End Set
    End Property

    Private ReadOnly Property IsChangeRequest As Boolean
        Get
            If RxChangeRequest IsNot Nothing Then
                Return True
            Else
                Return False
            End If
        End Get
    End Property

    Private _refRequest As RefRequest
    Private Property RefRequest As RefRequest
        Get
            Return _refRequest
        End Get
        Set(value As RefRequest)
            If value Is Nothing Then
                Me.RefreshOtherForms()
            End If
            _refRequest = value
        End Set

    End Property

    Private ReadOnly Property IsRefRequest As Boolean
        Get
            If Not IsNothing(RefRequest) AndAlso Not String.IsNullOrEmpty(RefRequest.MessageID) Then
                Return True
            Else
                Return False
            End If
        End Get
    End Property

    Private _dtProviderAssociation As DataTable = Nothing
    Public Property dtProviderAssociation() As DataTable
        Get
            If _dtProviderAssociation Is Nothing Then
                _dtProviderAssociation = New DataTable("ProviderAssociation")
            End If

            Return _dtProviderAssociation
        End Get
        Set(ByVal value As DataTable)
            _dtProviderAssociation = value
        End Set
    End Property

    Private _RxOTCAction As OTCDrugAction = OTCDrugAction.None
    Public Property RxOTCActions() As OTCDrugAction
        Get
            Return _RxOTCAction
        End Get
        Set(ByVal value As OTCDrugAction)
            _RxOTCAction = value
        End Set
    End Property

    Private _rxCommand As RxCommands
    Public Property RxCommand() As RxCommands
        Get
            Return _rxCommand
        End Get
        Set(ByVal value As RxCommands)
            _rxCommand = value
        End Set
    End Property

    Private _activeEPAList As List(Of ActiveEPA)
    Public Property ActiveEPAList() As List(Of ActiveEPA)
        Get
            Return _activeEPAList
        End Get
        Set(ByVal value As List(Of ActiveEPA))
            _activeEPAList = value
        End Set
    End Property

    Public WriteOnly Property Setform()
        Set(ByVal value)
            If IsRefRequest Then
                ofrmRxRequest = value
            Else
                ofrmError = value
            End If

        End Set
    End Property

    Public ReadOnly Property GetCurrentPatientID() As Int64 Implements mdlGeneral.IPatientContext.GetCurrentPatientID
        Get
            Return nRxModulePatientID
        End Get
    End Property

    Public _strDefaultPharmacy As String = ""
    Public Property DefaultPharmacy()
        Get
            Return _strDefaultPharmacy
        End Get
        Set(ByVal value)
            _strDefaultPharmacy = value
        End Set
    End Property

    Private _IsDefaultPharmacyChanged As Boolean
    Public Property IsDefaultPharmacyChanged() As Boolean
        Get
            Return _IsDefaultPharmacyChanged
        End Get
        Set(ByVal value As Boolean)
            _IsDefaultPharmacyChanged = value
        End Set
    End Property

    Private _DisableControls As Boolean
    Public Property DisableControls() As Boolean
        Get
            Return _DisableControls
        End Get
        Set(ByVal value As Boolean)
            _DisableControls = value
        End Set
    End Property

    Private _IsPastVisit As Boolean
    Public Property IsPastVisit() As Boolean
        Get
            Return _IsPastVisit
        End Get
        Set(ByVal value As Boolean)
            _IsPastVisit = value
        End Set
    End Property

    Public ReadOnly Property SenderId As String
        Get
            If Not _objFormularyToolBar Is Nothing Then
                Return _objFormularyToolBar.SenderId
            End If
            Return Nothing
        End Get
    End Property

    Public ReadOnly Property FormularyId As String
        Get
            If Not _objFormularyToolBar Is Nothing Then
                Return _objFormularyToolBar.FormularyId
            End If
            Return Nothing
        End Get
    End Property

    Public ReadOnly Property CoverageID As String
        Get
            If Not _objFormularyToolBar Is Nothing Then
                Return _objFormularyToolBar.CoverageID
            End If
            Return Nothing
        End Get
    End Property

    Public ReadOnly Property CopayID As String
        Get
            If Not _objFormularyToolBar Is Nothing Then
                Return _objFormularyToolBar.CopayId
            End If
            Return Nothing
        End Get
    End Property

    Public ReadOnly Property AlternativeId As String
        Get
            If Not _objFormularyToolBar Is Nothing Then
                Return _objFormularyToolBar.AlternativeID
            End If
            Return Nothing
        End Get
    End Property

    Private _LoadProbOnMedication As Boolean = False
    Public Property LoadProbOnMedication() As Boolean
        Get
            Return _LoadProbOnMedication
        End Get
        Set(ByVal value As Boolean)
            _LoadProbOnMedication = value
        End Set
    End Property

    Public Event PerformDrugAlertCheck(ByVal IsPrescribing As Boolean)
    Public Event PerformEPAStatusCheck(ByVal row As Integer?)
    Public Event IseRxed(ByVal row As Integer)

#Region "Prescription UserControls With Events"
    Private WithEvents _RxBusinessLayer As RxBusinesslayer
    Private WithEvents GloRxToolBarUserCtrl1 As gloRxToolbarUserCtrl
    Private WithEvents _RxListUserCtrl As gloDrugListRevised
    Private WithEvents _RxC1Flexgrid As gloRxC1FlexGrdPrescriptionUserCtrl
    Private WithEvents _RxRefillC1Flexgrid As gloRxSearchC1FlexPrescUserCtrl 'Prv Rx
    Private WithEvents _RxHistoryUserCtrl As gloRxHistoryUserCtrl
    Private WithEvents objCustomPrescription As CustomPrescription
    Private WithEvents _RxPatientStrip As gloUC_PatientStrip
    Private WithEvents objSigControl As gloUC_CustomSearchInC1Flexgrid
#End Region

#Region "Medication UserControls With Events"
    Private WithEvents _MedBusinessLayer As MedicationBusinessLayer
    Private WithEvents _MxC1Flexgrid As gloMedicationC1FlexGrdUserCtrl
    Private WithEvents _MedRefillC1FlexGridUserCtrl As gloMedRefillC1FlexGridUserCtrl
    Private WithEvents _MxHistoryUserCtrl As gloMedHistoryUserCtrl
    Private WithEvents objCustomMedication As CustomMedication

#End Region

#Region "Formulary UserControls With Events"
    Private WithEvents _objFormularyToolBar As gloFormularlyToolBarUserCtrl

    Private COL_COUNT_ThpAltSM As Integer = 14
    Private COL_ThpAltSM_Type As Integer = 0
    Private COL_ThpAltSM_NDCCode As Integer = 13
    Private COL_ThpAltSM_DrugName As Integer = 2
    Private COL_ThpAltSM_RxType As Integer = 3
    Private COL_ThpAltSM_Strength As Integer = 4
    Private COL_ThpAltSM_StrengthUnit As Integer = 5
    Private COL_ThpAltSM_DoseCheckUnit As Integer = 6 'DrugForm
    Private COL_ThpAltSM_PreferenceLevel As Integer = 7
    Private COL_ThpAltSM_StepOrder As Integer = 8
    Private COL_ThpAltSM_AbbrevatedFormulary As Integer = 9
    Private COL_ThpAltSM_AbbrevatedCopay As Integer = 10
    Private COL_ThpAltSM_HiddenDrugName As Integer = 11
    Private COL_ThpAltSM_HiddenDrugRoute As Integer = 12
    Private COL_ThpAltSM_DrugNarcoticVal As Integer = 1 ''to store narcotic value for alternative drug, add for 6055 hotfix
    Private _VisibleCount As Int16 = 0
    Private COL_COUNT_FormIndicator As Integer = 2
    Private COL_FormlyIndiIcon As Integer = 0
    Private COL_FormlyIndiType As Integer = 1
#End Region

#Region "Prescription  Constructors"

    Private Sub SetLoginProviderID()
        Try


            _RxBusinessLayer.PatientProviderId = nPatientProviderId

            If gnLoginProviderID <> 0 Then
                _RxBusinessLayer.ProviderID = gnLoginProviderID
            Else
                _RxBusinessLayer.ProviderID = nPatientProviderId
            End If

        Catch ex As Exception

        End Try
    End Sub

    Public Sub New(ByVal PatientID As Long, Optional ByVal _IsOpenedFromPrescription As Boolean = False)
        MyBase.New()
        InitializeComponent()

        nRxModulePatientID = PatientID

        _RxBusinessLayer = New RxBusinesslayer(nRxModulePatientID)
        _RxBusinessLayer.CurrentVisitID = 0
        _RxBusinessLayer.PastVisitID = 0

        _MedBusinessLayer = New MedicationBusinessLayer(nRxModulePatientID)
        _MedBusinessLayer.PastVisitDate = Now
        _MedBusinessLayer.CurrentVisitDate = Now

        Using objProvider As New clsProvider
            nPatientProviderId = objProvider.GetPatientProvider(nRxModulePatientID)
        End Using

        SetLoginProviderID()
        InitialiseControls()

        IsOpenedFromPrescription = _IsOpenedFromPrescription

    End Sub

    Public Sub New(ByVal m_visitid As Long, ByVal PatientID As Int64)
        MyBase.new()
        InitializeComponent()

        nRxModulePatientID = PatientID

        _RxBusinessLayer = New RxBusinesslayer(nRxModulePatientID)
        _RxBusinessLayer.CurrentVisitID = m_visitid
        _RxBusinessLayer.PastVisitID = m_visitid

        _MedBusinessLayer = New MedicationBusinessLayer(nRxModulePatientID)
        _MedBusinessLayer.CurrentVisitID = m_visitid
        _MedBusinessLayer.PastVisitID = m_visitid

        If _MedBusinessLayer.PastVisitID <> 0 Then
            Dim Visitdate As Date
            Visitdate = _MedBusinessLayer.FetchPastVisitDate()
            If Visitdate.ToShortDateString = Now.ToShortDateString Then
                IsPastVisit = False
            Else
                IsPastVisit = True
            End If
        Else
            _MedBusinessLayer.PastVisitDate = Now
            _MedBusinessLayer.CurrentVisitDate = Now
        End If

        Using objProvider As New clsProvider
            nPatientProviderId = objProvider.GetPatientProvider(nRxModulePatientID)
        End Using

        SetLoginProviderID()
        InitialiseControls()

        IsOpenedFromPrescription = True

    End Sub

    Public Sub New(ByVal arrdrug As ArrayList, ByVal ProviderId As Int64, Optional ByVal m_Visitid As Int64 = 0, Optional ByVal m_Patientid As Int64 = 0, Optional ByVal ShowCarryForwardMessage As Boolean = True)
        MyBase.New()
        InitializeComponent()

        nRxModulePatientID = m_Patientid
        blnShowCarryForwardMessage = ShowCarryForwardMessage

        _RxBusinessLayer = New RxBusinesslayer(nRxModulePatientID)
        _RxBusinessLayer.CurrentVisitID = m_Visitid
        _RxBusinessLayer.PastVisitID = m_Visitid

        _MedBusinessLayer = New MedicationBusinessLayer(nRxModulePatientID)
        _MedBusinessLayer.CurrentVisitID = m_Visitid
        _MedBusinessLayer.PastVisitID = m_Visitid

        If _MedBusinessLayer.PastVisitID <> 0 Then
            Dim Visitdate As Date
            Visitdate = _MedBusinessLayer.FetchPastVisitDate()
            If Visitdate.ToShortDateString = Now.ToShortDateString Then
                DisableControls = False
            Else
                DisableControls = True
            End If
        Else
            _MedBusinessLayer.PastVisitDate = Now
            _MedBusinessLayer.CurrentVisitDate = Now
        End If

        Using objProvider As New clsProvider
            nPatientProviderId = objProvider.GetPatientProvider(nRxModulePatientID)
        End Using

        SetLoginProviderID()
        InitialiseControls()
        ShowMedication()

        arrDrugs = arrdrug
        IsOpenedFromPrescription = True

        '' Rx-Meds form is edited and we should prompt the user if he directly Clicks the Close button
        gloEMRGeneralLibrary.gloGeneral.clsgeneral.blnIsRxMedsFormEdited = True

    End Sub

    ''' ChangeRX
    Public Sub New(ByVal ChangeRequest As LocalSchema.RxChangeRequest)
        MyBase.New()
        InitializeComponent()

        Me.nRxModulePatientID = ChangeRequest.PatientID
        Me.RxChangeRequest = ChangeRequest

        _RxBusinessLayer = New RxBusinesslayer(ChangeRequest)

        _MedBusinessLayer = New MedicationBusinessLayer(nRxModulePatientID)
        _MedBusinessLayer.PastVisitDate = Now
        _MedBusinessLayer.CurrentVisitDate = Now

        globalSecurity.gstrLoginName = gstrLoginName
        globalSecurity.gstrClientMachineName = gstrClientMachineName
        globalSecurity.gnUserID = gnLoginID

        Using objProvider As New clsProvider
            nPatientProviderId = objProvider.GetPatientProvider(nRxModulePatientID)
        End Using

        SetLoginProviderID()
        InitialiseControls()
        ShowMedication()

        IsOpenedFromPrescription = True

        ''Rx-Meds form is edited and we should prompt the user if he directly Clicks the Close button
        gloEMRGeneralLibrary.gloGeneral.clsgeneral.blnIsRxMedsFormEdited = True
    End Sub

    '' Refill Request
    Public Sub New(ByVal refRequest As RefRequest)
        MyBase.New()
        InitializeComponent()

        Me.nRxModulePatientID = refRequest.PatientID
        Me.RefRequest = refRequest

        _RxBusinessLayer = New RxBusinesslayer(nRxModulePatientID)
        _RxBusinessLayer.OldPharmacyId = refRequest.PharmacyID

        _MedBusinessLayer = New MedicationBusinessLayer(nRxModulePatientID)
        _MedBusinessLayer.PastVisitDate = Now
        _MedBusinessLayer.CurrentVisitDate = Now

        globalSecurity.gstrLoginName = gstrLoginName
        globalSecurity.gstrClientMachineName = gstrClientMachineName
        globalSecurity.gnUserID = gnLoginID

        Using objProvider As New clsProvider
            nPatientProviderId = objProvider.GetPatientProvider(nRxModulePatientID)
        End Using

        SetLoginProviderID()
        InitialiseControls()
        ShowMedication()

        IsOpenedFromPrescription = True

        ''Rx-Meds form is edited and we should prompt the user if he directly Clicks the Close button
        gloEMRGeneralLibrary.gloGeneral.clsgeneral.blnIsRxMedsFormEdited = True

    End Sub

#End Region

    Public Sub ShowPanel()
        pnlWait.Visible = True

        pnlWait.BringToFront()
        Application.DoEvents()
    End Sub

    Public Sub HidePanel()
        pnlWait.Visible = False
        pnlWait.SendToBack()

    End Sub

    Private Sub frmPrescription_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Try
            If LockID <> 0 Then
                Delete_Lock_FormLevel(LockID, nRxModulePatientID)
            End If

            If Not IsNothing(_MedBusinessLayer) Then
                _MedBusinessLayer.Dispose()
                _MedBusinessLayer = Nothing
            End If
            If Not IsNothing(_RxBusinessLayer) Then
                _RxBusinessLayer.Dispose()
                _RxBusinessLayer = Nothing
            End If
            If Not IsNothing(objDrugInteraction) Then
                objDrugInteraction.MyDispose()
                objDrugInteraction = Nothing
            End If
            If blnOpenFromExam = True Then
                If Not IsNothing(myCaller) Then
                    myCaller.GetdataFromOtherForms(gloEMRWord.enumDocType.Prescription)
                    myCaller.GetdataFromOtherForms(gloEMRWord.enumDocType.Medication)
                End If
            End If

            If blnOpenPre = True Then
                If Not IsNothing(mycallerpre) Then
                    mycallerpre.GetPrescription() 'Prescription
                End If
            End If
            If Not _RxListUserCtrl Is Nothing Then
                pnlcenter.Controls.Remove(_RxListUserCtrl)
                _RxListUserCtrl.Dispose()
                _RxListUserCtrl = Nothing
            End If
            If blnOpenFromMessage = True Then
                If Not IsNothing(myCallerM) Then
                    myCallerM.GetdataFromOtherForms(gloEMRWord.enumDocType.Prescription)
                    myCallerM.GetdataFromOtherForms(gloEMRWord.enumDocType.Medication)
                End If
            End If
            If Not IsNothing(myCaller1) Then
                myCaller1.GetdataFromOtherForms(gloEMRWord.enumDocType.Medication)
                myCaller1.GetdataFromOtherForms(gloEMRWord.enumDocType.Prescription)
            End If
            If Not IsNothing(myCallerN) Then
                myCallerN.GetdataFromOtherForms(gloEMRWord.enumDocType.Medication)
                myCallerN.GetdataFromOtherForms(gloEMRWord.enumDocType.Prescription)
            End If
            If blnOpenFromPatientConsent = True Then
                If Not IsNothing(myCallerC) Then
                    myCallerC.GetdataFromOtherForms(gloEMRWord.enumDocType.Prescription)
                    myCallerC.GetdataFromOtherForms(gloEMRWord.enumDocType.Medication)
                End If
            End If

            If Not _objFormularyToolBar Is Nothing Then
                _objFormularyToolBar.Dispose()
                _objFormularyToolBar = Nothing
            End If

            If blnOpenFromPTProtocol = True Then
                If Not IsNothing(myCallerPT) Then
                    myCallerPT.GetdataFromOtherForms(gloEMRWord.enumDocType.Prescription)
                    myCallerPT.GetdataFromOtherForms(gloEMRWord.enumDocType.Medication)
                End If
            End If

            If blnOpenFromLetter = True Then
                If Not IsNothing(myCallerL) Then
                    myCallerL.GetdataFromOtherForms(gloEMRWord.enumDocType.Prescription)
                    myCallerL.GetdataFromOtherForms(gloEMRWord.enumDocType.Medication)
                End If
            End If

            blnOpenFromExam = False
            blnOpenFromMessage = False
            blnOpenFromPatientConsent = False
            blnOpenFromPTProtocol = False

            Try
                RemoveHandler Me.PerformEPAStatusCheck, AddressOf frmPrescription_PerformEPAStatusCheck

                RemoveHandler Me.PerformDrugAlertCheck, AddressOf frmPrescription_PerformDrugAlertCheck

                RemoveHandler _RxC1Flexgrid.DrugFormularyQueried, AddressOf _RxC1Flexgrid_DrugFormularyQueried
                RemoveHandler _RxC1Flexgrid.PDRProgramsRequested, AddressOf _RxC1Flexgrid_PDRProgramsRequested

                RemoveHandler _RxC1Flexgrid.DrugFormularyRequested, AddressOf _RxC1Flexgrid_DrugFormularyRequested
                RemoveHandler _RxC1Flexgrid.CancelRxRequested, AddressOf _RxC1Flexgrid_CancelRxRequested
                RemoveHandler _RxC1Flexgrid.DrugPARequested, AddressOf _RxC1Flexgrid_DrugPARequested
                'RemoveHandler _RxC1Flexgrid.PDRInfoButtonClicked, AddressOf _RxC1Flexgrid_PDRInfoButtonClicked
            Catch ex As Exception

            End Try

            If Me.AlternativesControl IsNot Nothing Then
                Try
                    RemoveHandler Me.AlternativesControl.DrugAccepted, AddressOf AcceptAlternativeInfo
                    RemoveHandler Me.AlternativesControl.DrugSelectedEvent, AddressOf DrugSelected
                Catch ex As Exception

                End Try
                Me.AlternativesControl = Nothing
            End If

            If Me.CopayControl IsNot Nothing Then
                Me.CopayControl = Nothing
            End If

            If Me.patientDiagnosis IsNot Nothing Then
                Me.patientDiagnosis = Nothing
            End If

            If Me.patientProblems IsNot Nothing Then
                Me.patientProblems = Nothing
            End If

            If Me.patientAllergicMeds IsNot Nothing Then
                Me.patientAllergicMeds = Nothing
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            ex = Nothing
        Finally
            Dispose_Global()
            Try
                'Application.DoEvents()
                Me.Dispose()
            Catch exdispose As Exception

            End Try
        End Try
    End Sub

    Public Sub DeleteLockRecord()
        If LockID <> 0 Then
            Delete_Lock_FormLevel(LockID, nRxModulePatientID)
        End If
    End Sub

    Private Sub Dispose_Global()
        Try

            If Not IsNothing(_RxPatientStrip) Then
                pnlleft.Controls.Remove(_RxPatientStrip)
                _RxPatientStrip.Dispose()
                _RxPatientStrip = Nothing
            End If

            If Not IsNothing(GloRxToolBarUserCtrl1) Then
                pnlmainToolBar.Controls.Remove(GloRxToolBarUserCtrl1)
                GloRxToolBarUserCtrl1.Dispose()
                GloRxToolBarUserCtrl1 = Nothing
            End If

            If Not IsNothing(_RxListUserCtrl) Then
                pnlleft.Controls.Remove(_RxListUserCtrl)
                _RxListUserCtrl.Dispose()
                _RxListUserCtrl = Nothing
            End If


            If Not IsNothing(_RxHistoryUserCtrl) Then
                pnlRxHistory.Controls.Remove(_RxHistoryUserCtrl)
                _RxHistoryUserCtrl.Dispose()
                _RxHistoryUserCtrl = Nothing
            End If

            If Not IsNothing(_RxC1Flexgrid) Then
                pnlPrescriptionDetails.Controls.Remove(_RxC1Flexgrid)
                Try
                    RemoveHandler _RxC1Flexgrid._FlexSelChange, AddressOf _RxC1Flexgrid__FlexSelChange
                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    ex = Nothing
                End Try
                _RxC1Flexgrid.Dispose()
                _RxC1Flexgrid = Nothing
            End If

            If Not IsNothing(_MxC1Flexgrid) Then
                pnlMedicationDetails.Controls.Remove(_MxC1Flexgrid)
                _MxC1Flexgrid.Dispose()
                _MxC1Flexgrid = Nothing
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

#Region "Scan/View Document - Medication Reconcilation"

    Public Sub ScanViewDoucment(ByVal btnScanViewDocumentText As String)
        Try


            Dim _ScanContainerID As Int64 = 0
            Dim _ScanDocumentID As Int64 = 0
            Dim _result As Boolean = False

            If nRxModulePatientID = 0 Then
                MessageBox.Show("Please select patient", "Pull Chart", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            If MainMenu.IsAccess(False, nRxModulePatientID) = False Then
                Exit Sub
            End If

            Dim _ScanDocFlag As Boolean = True
            sDMSScanCategory = gDMSCategory_RxMed

            If (lDMSDocumentID > 0) And btnScanViewDocumentText = "View" Then
                ViewScanDoucment()
            Else
                If _ScanDocFlag = True Then
                    If gloEDocumentV3.eDocManager.eDocValidator.IsCategoryExists(0, sDMSScanCategory, gClinicID) = False Then
                        MessageBox.Show("DMS Category for prescription has not been set, Please set the category", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        _ScanDocFlag = False
                    End If
                End If

                If _ScanDocFlag = True Then
                    _result = Set_ScanDocumentEvent(nRxModulePatientID, sDMSScanCategory, _ScanContainerID, _ScanDocumentID)

                    lDMSDocumentID = _ScanDocumentID
                    lDMSContainerID = _ScanContainerID
                End If

            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try

    End Sub

    Public Sub ViewScanDoucment()
        Try
            ' pnlViewDocument.Visible = True

            If Not IsNothing(oViewDocument) Then
                oViewDocument = Nothing
            End If

            If (lDMSDocumentID > 0) Then
                If IsNothing(oViewDocument) Then
                    oViewDocument = New gloEDocumentV3.gloEDocV3Management()
                End If
                oViewDocument.oPatientExam = New clsPatientExams

                oViewDocument.oPatientMessages = New clsMessage
                oViewDocument.oPatientLetters = New clsPatientLetters
                oViewDocument.oNurseNotes = New clsNurseNotes
                oViewDocument.oHistory = New clsPatientHistory
                oViewDocument.oLabs = New clsLabs
                oViewDocument.oDMS = New gloEDocumentV3.eDocManager.eDocGetList()
                oViewDocument.oRxmed = New clsPatientDetails
                oViewDocument.oOrders = New clsPatientDetails
                oViewDocument.oProblemList = New clsPatientProblemList

                oViewDocument.oCriteria = New DocCriteria
                oViewDocument.oWord = New clsWordDocument
                Dim isItDialog As Boolean = oViewDocument.ShowEDocument(nRxModulePatientID, gloEDocumentV3.Enumeration.enum_OpenEDocumentAs.ViewDocument, Nothing, gloEDocumentV3.Enumeration.enum_OpenExternalSource.RxMeds, lDMSDocumentID)

                If (isItDialog = True) Then


                    If (IsNothing(oViewDocument.oPatientExam) = False) Then
                        DirectCast(oViewDocument.oPatientExam, clsPatientExams).Dispose()
                        oViewDocument.oPatientExam = Nothing
                    End If
                    If (IsNothing(oViewDocument.oPatientMessages) = False) Then
                        DirectCast(oViewDocument.oPatientMessages, clsMessage).Dispose()
                        oViewDocument.oPatientMessages = Nothing
                    End If
                    If (IsNothing(oViewDocument.oPatientLetters) = False) Then
                        DirectCast(oViewDocument.oPatientLetters, clsPatientLetters).Dispose()
                        oViewDocument.oPatientLetters = Nothing
                    End If
                    If (IsNothing(oViewDocument.oNurseNotes) = False) Then
                        DirectCast(oViewDocument.oNurseNotes, clsNurseNotes).Dispose()
                        oViewDocument.oNurseNotes = Nothing
                    End If
                    If (IsNothing(oViewDocument.oHistory) = False) Then
                        DirectCast(oViewDocument.oHistory, clsPatientHistory).Dispose()
                        oViewDocument.oHistory = Nothing
                    End If
                    If (IsNothing(oViewDocument.oLabs) = False) Then
                        DirectCast(oViewDocument.oLabs, clsLabs).Dispose()
                        oViewDocument.oLabs = Nothing
                    End If
                    If (IsNothing(oViewDocument.oDMS) = False) Then
                        DirectCast(oViewDocument.oDMS, gloEDocumentV3.eDocManager.eDocGetList).Dispose()
                        oViewDocument.oDMS = Nothing
                    End If
                    If (IsNothing(oViewDocument.oRxmed) = False) Then
                        DirectCast(oViewDocument.oRxmed, clsPatientDetails).Dispose()
                        oViewDocument.oRxmed = Nothing
                    End If
                    If (IsNothing(oViewDocument.oOrders) = False) Then
                        DirectCast(oViewDocument.oOrders, clsPatientDetails).Dispose()
                        oViewDocument.oOrders = Nothing
                    End If
                    If (IsNothing(oViewDocument.oProblemList) = False) Then
                        DirectCast(oViewDocument.oProblemList, clsPatientProblemList).Dispose()
                        oViewDocument.oProblemList = Nothing
                    End If

                    If (IsNothing(oViewDocument.oCriteria) = False) Then
                        DirectCast(oViewDocument.oCriteria, DocCriteria).Dispose()
                        oViewDocument.oCriteria = Nothing
                    End If

                    oViewDocument.Dispose()
                End If
                oViewDocument = Nothing

            End If

        Catch ex As Exception
            If Not IsNothing(oViewDocument) Then
                oViewDocument.Dispose()
            End If
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Function Set_ScanDocumentEvent(ByVal PatientID As Int64, ByVal PrescriptionCategory As String, ByRef ScanContainerID As Int64, ByRef ScanDocumentID As Int64) As Boolean
        Dim oScanDocument As New gloEDocumentV3.gloEDocV3Management()
        Dim _result As Boolean = False
        Try
            _result = oScanDocument.ShowEScanner(PatientID, PrescriptionCategory, DateTime.Now.Year.ToString(), MonthName(Month(Date.Now)), gClinicID, gloEDocumentV3.Enumeration.enum_DocumentEventType.ScanDocument, ScanContainerID, ScanDocumentID)
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally
            oScanDocument.Dispose()
        End Try
        Return _result
    End Function

#End Region

    Private Sub frmPrescription_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        gloSureScript.gloSurescriptGeneral.gnLoginUserId = gnLoginID ''''''''this wil be saved 

        pnlmainToolBar.Width = 600

        Dim ptdat1 As Date
        Try
            ptdat1 = CType(strPatientDOB, Date) ''Bug #93860: unhandled exception 
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            If Not IsNothing(ex) Then
                ex = Nothing
            End If
        End Try
        gloEMRGeneralLibrary.gloGeneral.clsgeneral.gstrPatientAgeInYears = DateDiff(DateInterval.Year, ptdat1, Now.Date)
        gloEMRGeneralLibrary.gloGeneral.clsgeneral.gstrPatientAgeInMonths = DateDiff(DateInterval.Month, Now.Date, ptdat1)
        gloEMRGeneralLibrary.gloGeneral.clsgeneral.gstrPatientDOB = strPatientDOB
        pnlAllergiesAlerts.Left = pnlmainToolBar.Right
        pnlAllergiesAlerts.Top = pnlMedicationHistory.Height - 100
        If gblnSQLAuthentication = True Then
            gloSureScript.gloSurescriptGeneral.gblnIsSQLAuthentication = True
        End If

        If blncancel = True And IsOpenedFromPrescription = False Then 'this means the medication form was called form other modules of gloEMR. therefore we need to dock the medication grid on top
            Me.Text = "Rx/Meds" & " " & "Mode:- Medication"
            Try
                gloPatient.gloPatient.GetWindowTitle(Me, nRxModulePatientID, GetConnectionString(), gstrMessageBoxCaption)
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                ex = Nothing
            End Try
            blnRxC1FlexClick = False
            '_RxListUserCtrl.IsRxC1FlexClick = blnRxC1FlexClick
            btnMedication.BackgroundImage = Global.gloEMR.My.Resources.Img_Rx_MxGreen
            btnMedication.BackgroundImageLayout = ImageLayout.Stretch

            sptRefill.Enabled = False
            pnlRefill.Visible = False

        Else
            Me.Text = "Rx/Meds" & " " & "Mode:- Prescription"
            Try
                gloPatient.gloPatient.GetWindowTitle(Me, nRxModulePatientID, GetConnectionString(), gstrMessageBoxCaption)
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                ex = Nothing
            End Try
            blnRxC1FlexClick = True
            '_RxListUserCtrl.IsRxC1FlexClick = blnRxC1FlexClick
            btnPrescription.BackgroundImage = Global.gloEMR.My.Resources.Img_Rx_MxGreen
            btnPrescription.BackgroundImageLayout = ImageLayout.Stretch

            sptRefill.Enabled = False
            pnlRefill.Visible = False

        End If

        pnlcenter.Visible = True
        pnlCenter2.Visible = False

        pnlViewDocument.Visible = False
        pnlViewMedicationHistory.Visible = False

        Try
            globalSecurity.gstrDatabaseName = gstrDatabaseName
            globalSecurity.gstrSQLServerName = gstrSQLServerName
            clsgeneral.StartUpPath = System.Windows.Forms.Application.StartupPath
            clsgeneral.gstrTempDirPath = gloSettings.FolderSettings.AppTempFolderPath 'clsgeneral.StartUpPath & gstrgloTempFolder
            clsgeneral.gblnIsStagingServer = gblnStagingServer

            AddRxControls()

            Dim objSettings As New clsSettings
            objSettings.GetSettings_Rx()
            LoadProbOnMedication = objSettings.LoadProbOnMedication
            objSettings.Dispose()
            objSettings = Nothing

            clsgeneral.gblnDisableAllowSubstitution = gblnDisableAllowSubstitution

            If Not IsNothing(_RxPatientStrip) Then
                _RxPatientStrip.Dispose()
                _RxPatientStrip = Nothing
            End If

            _RxPatientStrip = New gloUC_PatientStrip
            _RxPatientStrip.HideButton = False
            _RxPatientStrip.Dock = DockStyle.Top
            _RxPatientStrip.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
            _RxPatientStrip.BringToFront()

            If _RxBusinessLayer.PastVisitID <> 0 Then
                _RxBusinessLayer.FetchPastVisitDate()
                If Not IsNothing(_RxBusinessLayer.PastVisitDate) Then
                    _RxPatientStrip.DTPValue = _RxBusinessLayer.PastVisitDate
                    _RxPatientStrip.DTPEnabled = False
                End If
            Else
                _RxBusinessLayer.PastVisitDate = Now
                _RxBusinessLayer.Visitdate = Now
            End If

            Dim blnisProviderEqual As Boolean = True


            nRxRequestTransactionID = 0

            If IsRefRequest Then
                nRxRequestTransactionID = RefRequest.TransactionID
            ElseIf IsChangeRequest Then
                nRxRequestTransactionID = RxChangeRequest.TransactionID
            End If

            Me.Get_PharmacyDetails()

            If Not IsNothing(arrDrugs) Then
                If arrDrugs.Count > 0 Then
                    _RxBusinessLayer.PharmacyId = _RxBusinessLayer.OldPharmacyId
                    _RxBusinessLayer.FetchDrugDetailsMain(arrDrugs)
                    arrDrugs = Nothing
                Else
                    _RxBusinessLayer.FetchPrescriptionforUpdate(Now, 1, nRxRequestTransactionID) 'Check for an existing prescription,and load it if present
                    If _RxBusinessLayer.OldPharmacyId <> 0 Then
                        If _RxBusinessLayer.OldPharmacyId <> _RxBusinessLayer.PharmacyId Then
                            _RxBusinessLayer.PrescriptionCol.Clear()
                            _RxBusinessLayer.PharmacyId = _RxBusinessLayer.OldPharmacyId
                            If _RxBusinessLayer.PastVisitID <> 0 Then
                                _RxBusinessLayer.CurrentVisitID = _RxBusinessLayer.PastVisitID
                            End If
                        End If
                    End If
                End If
            Else
                _RxBusinessLayer.FetchPrescriptionforUpdate(Now, 1, nRxRequestTransactionID) 'Check for an existing prescription,and load it if present
                If _RxBusinessLayer.OldPharmacyId <> 0 Then
                    If _RxBusinessLayer.OldPharmacyId <> _RxBusinessLayer.PharmacyId Then
                        _RxBusinessLayer.PharmacyId = _RxBusinessLayer.OldPharmacyId
                        If _RxBusinessLayer.PastVisitID <> 0 Then
                            _RxBusinessLayer.CurrentVisitID = _RxBusinessLayer.PastVisitID
                        End If
                    End If
                End If
            End If

            If IsRefRequest Then
                Get_ProviderDetails(nPatientProviderId)
                _RxBusinessLayer.blnIsEPCSEnable = gbIsProviderEPCSEnable
                _RxBusinessLayer.getRxRefillRequest(RefRequest.TransactionID, RefRequest.RefillRequestQty, RefRequest.DateReceived, RefRequest.ReferenceNumber, RefRequest.MessageID, RefRequest.NDCCode)
            ElseIf IsChangeRequest Then
                Get_ProviderDetails(nPatientProviderId)
                _RxBusinessLayer.blnIsEPCSEnable = gbIsProviderEPCSEnable

                Select Case RxChangeRequest.Type
                    Case SS.ChangeRequestType.PriorAuthorizationRequired
                        _RxBusinessLayer.getRxChangeRequest(RxChangeRequest.TransactionID, RxChangeRequest.PharmacyID, RxChangeRequest.MedPrescribed)
                    Case SS.ChangeRequestType.TherapeuticSubstitution
                        If RxChangeRequest.MedRequested Is Nothing Then
                            RxBusinesslayer._RxTransactionID = RxChangeRequest.TransactionID
                        Else
                            _RxBusinessLayer.getRxChangeRequest(RxChangeRequest.TransactionID, RxChangeRequest.PharmacyID, RxChangeRequest.MedRequested)
                        End If
                    Case Else
                        _RxBusinessLayer.getRxChangeRequest(RxChangeRequest.TransactionID, RxChangeRequest.PharmacyID, RxChangeRequest.MedRequested)
                End Select
            End If



            If _RxBusinessLayer.PrescriptionCol.Count > 0 Then
                If Not IsNothing(_RxBusinessLayer.Visitdate) Then
                    If _RxBusinessLayer.Visitdate <> "12:00:00 AM" Then
                        _RxPatientStrip.DTPValue = _RxBusinessLayer.Visitdate
                        _RxPatientStrip.DTPEnabled = False
                    End If
                End If

                If IsRefRequest = True Or IsChangeRequest = True Then
                    If Not IsNothing(ofrmError) Then
                        SetRowstate("M")
                    End If
                End If

                BindFlexGrid_RX()


                For Each Prescription As Prescription In _RxBusinessLayer.PrescriptionCol
                    If Prescription.PCTransactionID = 0 Then
                        Me._RxC1Flexgrid_PDRProgramsRequested(_RxBusinessLayer.PrescriptionCol.IndexOf(Prescription))
                        'Else
                        '    Me.PerformPDRStatusCheck(Prescription.ItemNumber)
                    End If
                Next

            End If

            InitializeDIToolBar()

            If _RxBusinessLayer.PharmacyId <> 0 Then
                Dim NCPDPIDPharmStatus As String = _RxBusinessLayer.GetNCPDPID_PharmStatus(_RxBusinessLayer.PharmacyId.ToString)

                If NCPDPIDPharmStatus <> "" Then
                    Dim Retval() As String
                    Dim NCPDPID_PharmStat As String = NCPDPIDPharmStatus

                    Retval = NCPDPID_PharmStat.Split("|")

                    Dim NCPDPID As String = Retval(0) 'will contain the external code
                    Dim PharmacyStatus As String = Retval(1) ''will contain the Pharmacy Status

                    If NCPDPID = "" Then
                        GloRxToolBarUserCtrl1.tStrpFax.Enabled = True
                        GloRxToolBarUserCtrl1.tStrpERx.Enabled = False 'Dont enable eRx button
                        IsPharmacyEnabled = False
                    Else
                        GloRxToolBarUserCtrl1.tStrpFax.Enabled = gblnIsFaxEnabled
                        GloRxToolBarUserCtrl1.tStrpERx.Enabled = True 'enable eRx button
                        IsPharmacyEnabled = True
                    End If

                    If PharmacyStatus = "D" Then
                        IsPharmacyActivated = False
                    Else
                        IsPharmacyActivated = True
                    End If

                Else
                    GloRxToolBarUserCtrl1.tStrpFax.Enabled = True
                    GloRxToolBarUserCtrl1.tStrpERx.Enabled = False 'Dont enable eRx button
                    IsPharmacyEnabled = False
                End If
            Else
                GloRxToolBarUserCtrl1.tStrpFax.Enabled = True
                GloRxToolBarUserCtrl1.tStrpERx.Enabled = False 'Dont enable eRx button
                IsPharmacyEnabled = False
                IsPharmacyActivated = False
            End If

            'SetLoginProviderID()

            Get_ProviderDetails(_RxBusinessLayer.ProviderID)

            Try
                If Not IsNothing(oProvider) Then
                    oProvider.Dispose()
                    oProvider = Nothing
                End If
                oProvider = New clsProvider
                'Check ProviderType Senior or Junior
                If oProvider.IsProvider_Senior(_RxBusinessLayer.ProviderID) = False Then
                    Dim oclsSetting As New clsSettings
                    Dim objMultipleSupervisorsforPaperRxValue As Object
                    blnIsJuniorProvider = True

                    objMultipleSupervisorsforPaperRxValue = oclsSetting.GetSettingValue("Multiple Supervisors for Paper Rx")

                    blnMultipleSupervisor = objMultipleSupervisorsforPaperRxValue
                    blnRequireSupervisingProvider = oProvider.RequireSupervisingProviderforeRx
                    'SLR: Free objMultipleSupervisorsforPaperRxValue
                    If Not objMultipleSupervisorsforPaperRxValue Is Nothing Then
                        objMultipleSupervisorsforPaperRxValue = Nothing
                    End If

                    Dim _blnShowsupervisingCombo As Boolean = False
                    If blnRequireSupervisingProvider = False And blnMultipleSupervisor = True Then
                        _blnShowsupervisingCombo = False
                        lblSupervisingValidation.Visible = False
                    ElseIf blnRequireSupervisingProvider = False And blnMultipleSupervisor = False Then
                        _blnShowsupervisingCombo = True
                        lblSupervisingValidation.Visible = False
                    ElseIf blnRequireSupervisingProvider = True And blnMultipleSupervisor = True Then
                        _blnShowsupervisingCombo = True
                        lblSupervisingValidation.Visible = True
                    ElseIf blnRequireSupervisingProvider = True And blnMultipleSupervisor = False Then
                        _blnShowsupervisingCombo = True
                        lblSupervisingValidation.Visible = True
                    End If

                    If _blnShowsupervisingCombo = True Then

                        'Get Associated Senior Provider
                        Dim dtSeniorProvider As DataTable
                        dtSeniorProvider = oProvider.GetSeniorProviders(_RxBusinessLayer.ProviderID)

                        'Get Previous Supervising Provider from Visit
                        Dim dtDefaultSupervisingProvider As DataTable
                        dtDefaultSupervisingProvider = oProvider.GetDefaultSupervisingProvider(If(_RxBusinessLayer.CurrentVisitID = 0, _MedBusinessLayer.PrevVisitIDforSupervisingProvider, _RxBusinessLayer.CurrentVisitID), nRxModulePatientID)
                        If Not IsNothing(dtSeniorProvider) Then

                            'If No provider Associated get All Senior Provider
                            If dtSeniorProvider.Rows.Count > 0 Then
                                Dim drBlankRow As DataRow = dtSeniorProvider.NewRow
                                drBlankRow("sProviderName") = ""
                                drBlankRow("nProviderID") = 0

                                dtSeniorProvider.Rows.Add(drBlankRow)
                                cmbSupervisingProvider.DataSource = dtSeniorProvider
                                cmbSupervisingProvider.DisplayMember = dtSeniorProvider.Columns("sProviderName").ColumnName
                                cmbSupervisingProvider.ValueMember = dtSeniorProvider.Columns("nProviderID").ColumnName

                                If dtDefaultSupervisingProvider.Rows.Count > 0 Then
                                    Dim dr As DataRow()
                                    dr = dtSeniorProvider.Select("nProviderID =" & dtDefaultSupervisingProvider.Rows(0)("nSupervisingProviderID"))
                                    If dr.Count > 0 Then
                                        cmbSupervisingProvider.SelectedValue = dtDefaultSupervisingProvider.Rows(0)("nSupervisingProviderID")
                                    Else
                                        cmbSupervisingProvider.SelectedIndex = -1
                                    End If
                                Else
                                    cmbSupervisingProvider.SelectedIndex = -1
                                End If
                            Else
                                'get All Senior Provider
                                dtSeniorProvider = oProvider.GetAllSeniorProviders()

                                Dim drBlankRow As DataRow = dtSeniorProvider.NewRow
                                drBlankRow("sProviderName") = ""
                                drBlankRow("nProviderID") = 0

                                dtSeniorProvider.Rows.Add(drBlankRow)
                                cmbSupervisingProvider.DataSource = dtSeniorProvider
                                cmbSupervisingProvider.DisplayMember = dtSeniorProvider.Columns("sProviderName").ColumnName
                                cmbSupervisingProvider.ValueMember = dtSeniorProvider.Columns("nProviderID").ColumnName
                                If dtDefaultSupervisingProvider.Rows.Count > 0 Then
                                    cmbSupervisingProvider.SelectedValue = dtDefaultSupervisingProvider.Rows(0)("nSupervisingProviderID")
                                Else
                                    cmbSupervisingProvider.SelectedIndex = -1
                                End If
                            End If
                            pnlSupervisingProvider.Dock = DockStyle.Top
                        End If

                        If Not IsNothing(dtDefaultSupervisingProvider) Then ''disposed as per glo Code optimizer tool in 8000 version
                            dtDefaultSupervisingProvider.Dispose()
                            dtDefaultSupervisingProvider = Nothing
                        End If

                    Else
                        pnlSupervisingProvider.Dock = DockStyle.None
                    End If

                    If Not IsNothing(oclsSetting) Then
                        oclsSetting.Dispose()
                        oclsSetting = Nothing
                    End If
                    gblnMultipleSupervisorsforPaperRx = RxBusinesslayer.GetRxProviderAssociationSettings
                    _RxPatientStrip.ShowRxProviderAssociation = gblnMultipleSupervisorsforPaperRx

                Else
                    _RxPatientStrip.ShowRxProviderAssociation = False
                    pnlSupervisingProvider.Dock = DockStyle.None
                End If
            Catch ex As Exception
                MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                LoadError = True 'added for Prescription Provider issue Bug #46975 in 7022
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                ex = Nothing
            End Try
            oProvider.Dispose()
            oProvider = Nothing

            If _RxBusinessLayer.TransactionMode = RxBusinesslayer._TransactionMode.Add Then   '' Assign Patient provider to patient strip
                _RxPatientStrip.ShowDetail(nRxModulePatientID, gloUC_PatientStrip.enumFormName.Prescription, , _RxBusinessLayer.CurrentVisitID, nPatientProviderId, True, True, blnisProviderEqual, _RxBusinessLayer.PharmacyId, IsPharmacyEnabled)
            Else
                If _RxBusinessLayer.PrescriptionCol.Count > 0 Then
                    _RxPatientStrip.ShowDetail(nRxModulePatientID, gloUC_PatientStrip.enumFormName.Prescription, , _RxBusinessLayer.CurrentVisitID, nPatientProviderId, True, False, True, If(_RxBusinessLayer.PharmacyId <> 0, _RxBusinessLayer.PharmacyId, _RxBusinessLayer.PrescriptionCol.Item(0).PrescriptionID.ToString), IsPharmacyEnabled)
                Else
                    _RxPatientStrip.ShowDetail(nRxModulePatientID, gloUC_PatientStrip.enumFormName.Prescription, , _RxBusinessLayer.CurrentVisitID, nPatientProviderId, True, False, True, _RxBusinessLayer.PharmacyId, IsPharmacyEnabled)
                End If
            End If

            '26-Feb-16 Aniket: Resolving Bug #93662: gloEMR >> Patient message icon not getting display in EMR application
            pnlcenter.Controls.Add(_RxPatientStrip)
            pnlleft.SendToBack()
            splleft.SendToBack()


            _RxListUserCtrl.SendToBack()
            pnlFlexGrid.BringToFront()
            pnlFlexGrid.Dock = DockStyle.Fill

            Call SetPatientDiagnosis()

            If LoadProbOnMedication Then
                Dim objProblemList As New clsPatientProblemList
                patientProblems = objProblemList.Get_PatientProblemList(nRxModulePatientID, _RxBusinessLayer.Visitdate)
                '     patientProblems = objProblemList.Get_PatientProblemList(nRxModulePatientID)
                objProblemList.Dispose()
                objProblemList = Nothing
            End If

            _RxBusinessLayer.GetLatestMedication()

            If gblnSurescriptEnabled Then
                GloRxToolBarUserCtrl1.tStrpERx.Visible = True 'enable eRx button                
                GloRxToolBarUserCtrl1.tStrpRxFill.Visible = True
            Else
                GloRxToolBarUserCtrl1.tStrpERx.Visible = False 'Dont enable eRx button                
                GloRxToolBarUserCtrl1.tStrpRxFill.Visible = False
                GloRxToolBarUserCtrl1.tStrpFax.Enabled = True
            End If
            If gblnSurescriptEnabled Then
                If IsRefRequest Or IsChangeRequest Then
                    GloRxToolBarUserCtrl1.tStrpERx.Visible = False 'Dont enable eRx button                    
                    GloRxToolBarUserCtrl1.tStrpRxFill.Visible = False
                    GloRxToolBarUserCtrl1.tStrpFax.Enabled = False
                End If
            End If

            If IsFormLocked Then
                _MedBusinessLayer_Recordlock(IsFormLocked)
            Else
                If blncancel = False Then
                    ShowMedication_presc()
                End If
            End If

            If _MedBusinessLayer.MedicationCol.Count > 0 Then
                _MxC1Flexgrid.BindFlexgrid()
                If IsFormLocked Then
                    _MxC1Flexgrid.cmbMedStatus.Enabled = False
                End If
            End If

            If pnlDI.Visible Then
                pnlDI.BringToFront()
            End If


            If IsFormLocked Then
                If GloRxToolBarUserCtrl1.tStrpFax.Enabled = True Then
                    GloRxToolBarUserCtrl1.tStrpFax.Enabled = False
                End If
            End If

            str_eLink = GetLink() ''''this will return the 'MedicationInfo' setting value

            If str_eLink <> "" Then ''if not blank then visible the button as per conversation
                GloRxToolBarUserCtrl1.tStrpeDrugLink.Visible = True
            Else
                GloRxToolBarUserCtrl1.tStrpeDrugLink.Visible = False
            End If
            'Incident #55315: 00016572 : Carry forward issue
            If IsPastVisit Then
                '' To set the _MostRecentVisitID_Mx which is futher used to show the subsequent visit update message.
                _MedBusinessLayer.FetchMostRecentVisit()

            End If

            DisableItems(_DisableControls) ''''function added to handle the medication carry forward case in 7031. do not allow to add/refill medication when form is opened as per scenarios given in 7031.

            Try
                If Not IsNothing(clsSplit_PatientPrescription) Then
                    clsSplit_PatientPrescription.Dispose()
                    clsSplit_PatientPrescription = Nothing
                End If
                clsSplit_PatientPrescription = New gloEMRGeneralLibrary.clsSplitScreen
                If Not IsNothing(objCriteria) Then
                    objCriteria.Dispose()
                    objCriteria = Nothing
                End If
                objCriteria = New DocCriteria
                ObjWord = New clsWordDocument
                clsSplit_PatientPrescription.clsUCLabControl = New gloUserControlLibrary.gloUC_TransactionHistory()
                clsSplit_PatientPrescription.clsPatientExams = New clsPatientExams()
                clsSplit_PatientPrescription.clsPatientLetters = New clsPatientLetters()
                clsSplit_PatientPrescription.clsPatientMessages = New clsMessage()
                clsSplit_PatientPrescription.clsNurseNotes = New clsNurseNotes()
                clsSplit_PatientPrescription.clsHistory = New clsPatientHistory()
                clsSplit_PatientPrescription.clsLabs = New clsDoctorsDashBoard()
                clsSplit_PatientPrescription.clsDMS = New gloEDocumentV3.eDocManager.eDocGetList()
                clsSplit_PatientPrescription.clsRxmed = New clsPatientDetails()
                clsSplit_PatientPrescription.clsOrders = New clsPatientDetails()
                clsSplit_PatientPrescription.clsProblemList = New clsPatientProblemList()
                clsSplit_PatientPrescription.blnShowSmokingStatusCol = gblnShowSmokingColumn
                uiPanSplitScreen_PatientPrescription = clsSplit_PatientPrescription.LoadSplitControl(Me, nRxModulePatientID, _RxBusinessLayer.CurrentVisitID, "PatientLetter", objCriteria, ObjWord, gnClinicID, gnLoginID)
                uiPanSplitScreen_PatientPrescription.BringToFront()

                Dim _ToolStrip As New List(Of Object)
                _ToolStrip.Add(Me.GloRxToolBarUserCtrl1.tStrpSave)
                _ToolStrip.Add(Me.GloRxToolBarUserCtrl1.tStrpSaveRxMed)
                If gnLoginProviderID = 0 Then
                    MyBase.strProviderID = _RxBusinessLayer.ProviderID
                Else
                    MyBase.strProviderID = String.Empty
                End If
                MyBase.FormControls = Nothing
                MyBase.FormControls = _ToolStrip.ToArray()
                MyBase.SetChildFormControls()
                _ToolStrip = Nothing
            Catch ex As Exception
                MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error) 'added for Prescription Provider issue Bug #46975 in 7022
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                ex = Nothing
            Finally

                If (IsNothing(objCriteria) = False) Then
                    objCriteria = Nothing
                End If
                If (IsNothing(ObjWord) = False) Then
                    ObjWord = Nothing
                End If
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Open, "Rx-Meds Opened", nRxModulePatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)


                If Not IsNothing(dtMedRec) Then
                    dtMedRec.Dispose()
                    dtMedRec = Nothing
                End If
                dtMedRec = New DataTable()


                Dim dPatientID As New DataColumn("PatientID", GetType(Long))
                Dim dVisitID As New DataColumn("VisitID", GetType(Long))
                Dim dSummaryCheckBox As New DataColumn("SummaryCheckBox", GetType(Boolean))
                Dim dMedicationCheckBox As New DataColumn("MedicationCheckBox", GetType(Boolean))
                Dim dMedDate As New DataColumn("MedDate", GetType(DateTime))
                Dim dNotes As New DataColumn("Notes", GetType(String))
                Dim dRowState As New DataColumn("RowState", GetType(String))
                Dim dReconcillationType As New DataColumn("ReconcillationType", GetType(Int16))

                dtMedRec.Columns.Add(dPatientID)
                dtMedRec.Columns.Add(dVisitID)
                dtMedRec.Columns.Add(dSummaryCheckBox)
                dtMedRec.Columns.Add(dMedicationCheckBox)
                dtMedRec.Columns.Add(dMedDate)
                dtMedRec.Columns.Add(dNotes)
                dtMedRec.Columns.Add(dRowState)
                dtMedRec.Columns.Add(dReconcillationType)

                _MxC1Flexgrid.lblMedsReconciliation.Visible = False

                dtMedRec = (GetMedicationReconcillationDetails(_MedBusinessLayer.CurrentVisitID, nRxModulePatientID, nReconcillationType))
                If dtMedRec.Rows.Count > 0 Then
                    If (dtMedRec.Rows(0)("MedicationCheckBox") = True) Then
                        _MxC1Flexgrid.lblMedsReconciliation.Visible = True
                    End If
                End If

                ToolTip2 = New System.Windows.Forms.ToolTip
                ToolTip2.SetToolTip(Me.picInfo, "Allergies Alerts")

            End Try

        Catch ex As PrescriptionException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally
            GloRxToolBarUserCtrl1.tStrpERx.Visible = True

        End Try

        ''''Epcs Read vendor Settings 
        If gloEMRGeneralLibrary.gloGeneral.clsgeneral.gblnIsFormularyServiceEnabled = True And pnlDI.Visible Then
            pnlmainToolBar.Width = 600
        Else
            pnlmainToolBar.Width = 720
        End If

        If pnlFormularyToolBar.Visible Then
            pnlFormularyToolBar.BringToFront()
        End If

        If gloEMRGeneralLibrary.gloGeneral.clsgeneral.gblnIsFormularyServiceEnabled = True Or gblnAdvErxEnabled = True Then

            If Not IsNothing(_objFormularyToolBar) Then
                _objFormularyToolBar.Dispose()
                _objFormularyToolBar = Nothing
            End If

            SetThresholdPeriod()

            Using objEligibility As New ClsRxHubInterface()
                _objFormularyToolBar = New gloFormularlyToolBarUserCtrl(objEligibility.GetEligibilityStatus(nRxModulePatientID, gstrRxEligThresholdvalue), nRxModulePatientID)
            End Using

            gloEMRGeneralLibrary.gloGeneral.clsgeneral.gnFormularyPatientID = nRxModulePatientID
            gloEMRGeneralLibrary.gloGeneral.clsgeneral.gstrSelectedPBM = _objFormularyToolBar.tlStrpPBMCombo.ComboBox.Text

            AddHandler Me.PerformEPAStatusCheck, AddressOf frmPrescription_PerformEPAStatusCheck

            AddHandler Me.PerformDrugAlertCheck, AddressOf frmPrescription_PerformDrugAlertCheck
            AddHandler _RxC1Flexgrid.DrugFormularyQueried, AddressOf _RxC1Flexgrid_DrugFormularyQueried
            AddHandler _RxC1Flexgrid.PDRProgramsRequested, AddressOf _RxC1Flexgrid_PDRProgramsRequested

            AddHandler _RxC1Flexgrid.DrugFormularyRequested, AddressOf _RxC1Flexgrid_DrugFormularyRequested
            AddHandler _RxC1Flexgrid.CancelRxRequested, AddressOf _RxC1Flexgrid_CancelRxRequested
            AddHandler _RxC1Flexgrid.DrugPARequested, AddressOf _RxC1Flexgrid_DrugPARequested
            'AddHandler _RxC1Flexgrid.PDRInfoButtonClicked, AddressOf _RxC1Flexgrid_PDRInfoButtonClicked
        End If

        If gblnAdvErxEnabled Then
            AddFormularlyToolBarControl()
        Else
            pnlFormularyToolBar.Visible = False
        End If

        '' Setup EPA options as per Roles
        Me.LoadEPAUserRoles()

        Me.LoadPDMPRoles()
        '' Get All Active EPA List
        Me.RefreshActiveEPAList()

        If gbIsProviderEPCSEnable Then
            Try
                Dim objSettings As New clsSettings
                objSettings.GetVendorAndUrlInformationForEpcs(gblnStagingServer)
                If Not IsNothing(objSettings) Then
                    objSettings.Dispose()
                    objSettings = Nothing
                End If
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            End Try
        End If

        If gblnEpcsEnabled AndAlso clsgeneral.gblnIsPDMPEnabled AndAlso gblnAutoPDMPEligiblity Then
            Me.ToolTip1.SetToolTip(Me.btnAllergies, "View Allergies\PDMP Programs")
            PDMPProcess()
        Else
            Me.ToolTip1.SetToolTip(Me.btnAllergies, "View Allergies")            
        End If

        Call GetAllergies()
        Call RefreshAgreementIcon()


    End Sub

    Private Sub RefreshActiveEPAList()
        Using _EpaBusineslayer As New EPABusinesslayer()
            Me.ActiveEPAList = _EpaBusineslayer.GetActiveEPAList(nRxModulePatientID, _RxBusinessLayer.ProviderID)
        End Using
    End Sub

    Private Sub BindFlexGrid_RX(Optional ByVal _isGridRowCheck As Boolean = False)
        RemoveHandler _RxC1Flexgrid._FlexSelChange, AddressOf _RxC1Flexgrid__FlexSelChange
        _RxC1Flexgrid.BindFlexgrid(_isGridRowCheck)
        AddHandler _RxC1Flexgrid._FlexSelChange, AddressOf _RxC1Flexgrid__FlexSelChange
    End Sub

    Private Sub DisableItems(DisableControls As Boolean)
        Try
            If DisableControls = True Then '' this flag is set when the form is opened from patient details panel of dashboard, if this flag is ON then the drug control will be disable, the refill menu on Medication grid and PrvRx of Prescription grid will not be shown.
                GloRxToolBarUserCtrl1.tStrpERx.Enabled = False
                GloRxToolBarUserCtrl1.tStrpRxFill.Enabled = False
                GloRxToolBarUserCtrl1.tStrpSendRx.Enabled = False
                _RxListUserCtrl.Enabled = False
                _MxC1Flexgrid.RefillDisable = True
                '_MxC1Flexgrid.cmbMedStatus.Enabled = False
                GloRxToolBarUserCtrl1.tlb_Reconcile.Enabled = False ''do not allow reconcilation for past visit
                If gloEMRGeneralLibrary.gloGeneral.clsgeneral.gblnIsFormularyServiceEnabled = True Then
                    If (Not IsNothing(_objFormularyToolBar)) Then
                        _objFormularyToolBar.tlStrpPBMCombo.Enabled = False
                        _objFormularyToolBar.tStrpMedicationHistory.Enabled = False
                        _objFormularyToolBar.tlbbtn_RxHub.Enabled = False
                    End If
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

#Region "gloList User Control Functionality"
    Private oListControl As gloListControl.gloListControl
    Private oListUsers As gloListControl.gloListControl
    Private ToList As gloGeneralItem.gloItems

    Private Sub oListUsers_ItemSelectedClick(ByVal sender As Object, ByVal e As EventArgs)
        Dim oDataTable As DataTable = Nothing
        Dim dv As DataView = Nothing
        Dim oDB As New DataBaseLayer
        Dim _strSQL As String
        Dim strMessage As String
        Try

            If (oListUsers.dgListView.RowCount > 0) Then
                _strSQL = "Select nContactID,sNCPDPID from Patient_DTL where npatientID=" & nRxModulePatientID & " and nContactFlag=1 "
                oDataTable = oDB.GetDataTable_Query(_strSQL)
                If Not IsNothing(oDataTable) Then
                    If oDataTable.Rows.Count > 0 Then
                        dv = oDataTable.DefaultView
                        Dim _checkField As String ' used to check in dataview
                        If oListUsers.SelectedItems(oListUsers.SelectedItems.Count - 1).Code <> "" Then
                            dv.Sort = "sNCPDPID"
                            _checkField = oListUsers.SelectedItems(oListUsers.SelectedItems.Count - 1).Code
                        Else
                            dv.Sort = "nContactID"
                            _checkField = oListUsers.SelectedItems(oListUsers.SelectedItems.Count - 1).ID
                        End If

                        Dim dRows As DataRowView() = dv.FindRows(_checkField)
                        If dRows.Count = 0 Then
                            strMessage = "Would you like to make " & oListUsers.SelectedItems(oListUsers.SelectedItems.Count - 1).Description.ToString & " the default pharmacy for " & strPatientFirstName & " " & strPatientLastName & "?"
                            Dim dialogResult As DialogResult
                            dialogResult = System.Windows.Forms.MessageBox.Show(strMessage, gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                            If dialogResult = Windows.Forms.DialogResult.Yes Then
                                Set_DefaultPhamacy(nRxModulePatientID, oListUsers.SelectedItems(oListUsers.SelectedItems.Count - 1).ID, 0, gnClinicID)
                                DefaultPharmacy = oListUsers.SelectedItems(oListUsers.SelectedItems.Count - 1).Description
                                _RxBusinessLayer.PharmacyId = oListUsers.SelectedItems(oListUsers.SelectedItems.Count - 1).ID
                                Dim NCPDPIDPharmStatus As String = _RxBusinessLayer.GetNCPDPID_PharmStatus(oListUsers.SelectedItems(oListUsers.SelectedItems.Count - 1).ID.ToString)
                                If NCPDPIDPharmStatus <> "" Then
                                    Dim Retval() As String
                                    Dim NCPDPID_PharmStat As String = NCPDPIDPharmStatus
                                    Retval = NCPDPID_PharmStat.Split("|")
                                    Dim _NCPDPID As String = Retval(0) 'will contain the external code
                                    Dim _PharmacyStatus As String = Retval(1) ''will contain the Pharmacy Status

                                    If _NCPDPID = "" Then
                                        'IsPharmacyEnabled = False
                                        GloRxToolBarUserCtrl1.tStrpERx.Enabled = False 'Developer:Pradeep Date:01/27/2012 Bug ID: 20110
                                    Else
                                        GloRxToolBarUserCtrl1.tStrpERx.Enabled = True
                                        IsPharmacyEnabled = True
                                    End If
                                    If _PharmacyStatus = "D" Then
                                        IsPharmacyActivated = False
                                    Else
                                        IsPharmacyActivated = True
                                    End If
                                    _RxPatientStrip.Rx_RefreshPharmacy(oListUsers.SelectedItems(oListUsers.SelectedItems.Count - 1).Description, oListUsers._phoneNumber, oListUsers._faxNumber, oListUsers._address1, oListUsers._address2, oListUsers._city, oListUsers._state, oListUsers._zip, If(_NCPDPID = "", False, True)) 'Developer:Pradeep Date:01/27/2012 Bug ID: 20110
                                End If
                            Else
                                Set_DefaultPhamacy(nRxModulePatientID, oListUsers.SelectedItems(oListUsers.SelectedItems.Count - 1).ID, 1, gnClinicID)
                                DefaultPharmacy = oListUsers.SelectedItems(oListUsers.SelectedItems.Count - 1).Description
                            End If
                        End If
                    Else
                        Set_DefaultPhamacy(nRxModulePatientID, oListUsers.SelectedItems(oListUsers.SelectedItems.Count - 1).ID, 0, gnClinicID)
                        DefaultPharmacy = oListUsers.SelectedItems(oListUsers.SelectedItems.Count - 1).Description
                        _RxBusinessLayer.PharmacyId = oListUsers.SelectedItems(oListUsers.SelectedItems.Count - 1).ID
                        Dim NCPDPIDPharmStatus As String = _RxBusinessLayer.GetNCPDPID_PharmStatus(oListUsers.SelectedItems(oListUsers.SelectedItems.Count - 1).ID.ToString)
                        If NCPDPIDPharmStatus <> "" Then
                            Dim Retval() As String
                            Dim NCPDPID_PharmStat As String = NCPDPIDPharmStatus
                            Retval = NCPDPID_PharmStat.Split("|")
                            Dim NCPDPID As String = Retval(0) 'will contain the external code
                            Dim PharmacyStatus As String = Retval(1) ''will contain the Pharmacy Status
                            If NCPDPID = "" Then
                                ' IsPharmacyEnabled = False
                                GloRxToolBarUserCtrl1.tStrpERx.Enabled = False 'Developer:Pradeep Date:01/27/2012 Bug ID: 20110
                            Else
                                GloRxToolBarUserCtrl1.tStrpERx.Enabled = True
                                IsPharmacyEnabled = True
                            End If
                            If PharmacyStatus = "D" Then
                                IsPharmacyActivated = False
                            Else
                                IsPharmacyActivated = True
                            End If
                            _RxPatientStrip.Rx_RefreshPharmacy(oListUsers.SelectedItems(oListUsers.SelectedItems.Count - 1).Description, oListUsers._phoneNumber, oListUsers._faxNumber, oListUsers._address1, oListUsers._address2, oListUsers._city, oListUsers._state, oListUsers._zip, If(NCPDPID = "", False, True)) 'Developer:Pradeep Date:01/27/2012 Bug ID: 20110
                        End If
                    End If
                    'SLR: free oDatatable
                    oDataTable.Dispose()
                End If

                Dim dtUsers As New DataTable()
                Dim dcId As New DataColumn("ID")
                Dim dcDescription As New DataColumn("Description")
                dtUsers.Columns.Add(dcId)
                dtUsers.Columns.Add(dcDescription)

                If Not IsNothing(dcId) Then
                    dcId.Dispose()
                    dcId = Nothing
                End If

                If Not IsNothing(dcDescription) Then
                    dcDescription.Dispose()
                    dcDescription = Nothing
                End If
                If (IsNothing(ToList) = False) Then
                    ToList.Dispose()
                    ToList = Nothing
                End If

                ToList = New gloGeneralItem.gloItems()
                Dim ToItem As gloGeneralItem.gloItem

                If oListUsers.SelectedItems.Count > 0 Then
                    For i As Int16 = 0 To oListUsers.SelectedItems.Count - 1
                        Dim drTemp As DataRow = dtUsers.NewRow()
                        drTemp("ID") = oListUsers.SelectedItems(i).ID
                        drTemp("Description") = oListUsers.SelectedItems(i).Description
                        dtUsers.Rows.Add(drTemp)

                        ToItem = New gloGeneralItem.gloItem()

                        ToItem.ID = oListUsers.SelectedItems(i).ID
                        ToItem.Description = oListUsers.SelectedItems(i).Description

                        ToList.Add(ToItem)
                        ToItem.Dispose()
                        ToItem = Nothing
                    Next
                End If
                If Not IsNothing(dtUsers) Then ''disposed as per glo Code optimizer tool in 8000 version
                    dtUsers.Dispose()
                    dtUsers = Nothing
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally
            If Not IsNothing(dv) Then
                dv.Dispose()
                dv = Nothing
            End If
            If Not IsNothing(oDataTable) Then
                oDataTable.Dispose()
                oDataTable = Nothing
            End If
            If Not IsNothing(oDB) Then
                oDB.Dispose()
                oDB = Nothing
            End If

        End Try
        If blnRxC1FlexClick = True Then
            If Not IsNothing(ToList) Then
                Try
                    objCustomPrescription.PharmacyID = ToList.Item(0).ID
                    objCustomPrescription.PharmacyName = ToList.Item(0).Description

                    If objCustomPrescription.PharmacyID <> 0 Then
                        dtPharmacyDetails = _RxBusinessLayer.GetPharmacyDetails(objCustomPrescription.PharmacyID)
                        If Not IsNothing(dtPharmacyDetails.Rows(0)("sNCPDPID")) AndAlso Not IsDBNull(dtPharmacyDetails.Rows(0)("sNCPDPID")) Then
                            _RxBusinessLayer.PhNCPDPID = dtPharmacyDetails.Rows(0)("sNCPDPID")
                        Else
                            _RxBusinessLayer.PhNCPDPID = ""
                        End If
                        If Not IsNothing(dtPharmacyDetails.Rows(0)("nContactID")) AndAlso Not IsDBNull(dtPharmacyDetails.Rows(0)("nContactID")) Then
                            _RxBusinessLayer.PhContactID = dtPharmacyDetails.Rows(0)("nContactID")
                        Else
                            _RxBusinessLayer.PhContactID = 0
                        End If
                        If Not IsNothing(dtPharmacyDetails.Rows(0)("sName")) AndAlso Not IsDBNull(dtPharmacyDetails.Rows(0)("sName")) Then
                            _RxBusinessLayer.PharmacyName = dtPharmacyDetails.Rows(0)("sName")
                        Else
                            _RxBusinessLayer.PharmacyName = ""
                        End If
                        If Not IsNothing(dtPharmacyDetails.Rows(0)("sAddressLine1")) AndAlso Not IsDBNull(dtPharmacyDetails.Rows(0)("sAddressLine1")) Then
                            _RxBusinessLayer.PhAddressline1 = dtPharmacyDetails.Rows(0)("sAddressLine1")
                        Else
                            _RxBusinessLayer.PhAddressline1 = ""
                        End If
                        If Not IsNothing(dtPharmacyDetails.Rows(0)("sAddressLine2")) AndAlso Not IsDBNull(dtPharmacyDetails.Rows(0)("sAddressLine2")) Then
                            _RxBusinessLayer.PhAddressline2 = dtPharmacyDetails.Rows(0)("sAddressLine2")
                        Else
                            _RxBusinessLayer.PhAddressline2 = ""
                        End If

                        If Not IsNothing(dtPharmacyDetails.Rows(0)("sCity")) AndAlso Not IsDBNull(dtPharmacyDetails.Rows(0)("sCity")) Then
                            _RxBusinessLayer.PhCity = dtPharmacyDetails.Rows(0)("sCity")
                        Else
                            _RxBusinessLayer.PhCity = ""
                        End If

                        If Not IsNothing(dtPharmacyDetails.Rows(0)("sState")) AndAlso Not IsDBNull(dtPharmacyDetails.Rows(0)("sState")) Then
                            _RxBusinessLayer.PhState = dtPharmacyDetails.Rows(0)("sState")
                        Else
                            _RxBusinessLayer.PhState = ""
                        End If
                        If Not IsNothing(dtPharmacyDetails.Rows(0)("sZIP")) AndAlso Not IsDBNull(dtPharmacyDetails.Rows(0)("sZIP")) Then
                            _RxBusinessLayer.PhZip = dtPharmacyDetails.Rows(0)("sZIP")
                        Else
                            _RxBusinessLayer.PhZip = ""
                        End If

                        If Not IsNothing(dtPharmacyDetails.Rows(0)("sEmail")) AndAlso Not IsDBNull(dtPharmacyDetails.Rows(0)("sEmail")) Then
                            _RxBusinessLayer.PhEmail = dtPharmacyDetails.Rows(0)("sEmail")
                        Else
                            _RxBusinessLayer.PhEmail = ""
                        End If
                        If Not IsNothing(dtPharmacyDetails.Rows(0)("sFax")) AndAlso Not IsDBNull(dtPharmacyDetails.Rows(0)("sFax")) Then
                            _RxBusinessLayer.PhFax = dtPharmacyDetails.Rows(0)("sFax")
                        Else
                            _RxBusinessLayer.PhFax = ""
                        End If
                        If Not IsNothing(dtPharmacyDetails.Rows(0)("sPhone")) AndAlso Not IsDBNull(dtPharmacyDetails.Rows(0)("sFax")) Then
                            _RxBusinessLayer.PhPhone = dtPharmacyDetails.Rows(0)("sPhone")
                        Else
                            _RxBusinessLayer.PhPhone = ""
                        End If
                        If Not IsNothing(dtPharmacyDetails.Rows(0)("sServiceLevel")) AndAlso Not IsDBNull(dtPharmacyDetails.Rows(0)("sServiceLevel")) Then
                            _RxBusinessLayer.PhServiceLevel = dtPharmacyDetails.Rows(0)("sServiceLevel")
                        Else
                            _RxBusinessLayer.PhServiceLevel = ""
                        End If

                        Dim dtPharma As New DataTable
                        Dim dtRow As DataRow
                        dtPharma.Columns.Add("nContactID")
                        dtPharma.Columns.Add("sName")
                        dtRow = dtPharma.NewRow()
                        dtRow.Item(0) = dtPharmacyDetails.Rows(0)("nContactID")
                        dtRow.Item(1) = dtPharmacyDetails.Rows(0)("sName")
                        dtPharma.Rows.Add(dtRow)

                        objCustomPrescription.SetPharmacyData(dtPharma, nRxModulePatientID)

                        dtPharma.Dispose()
                        dtPharma = Nothing
                    End If

                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.ConfidentialInformation, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    ex = Nothing
                End Try
            End If
        Else
            If Not IsNothing(ToList) Then
                Try
                    objCustomMedication.PharmacyID = ToList.Item(0).ID
                    objCustomMedication.PharmacyName = ToList.Item(0).Description

                    If objCustomMedication.PharmacyID <> 0 Then
                        dtPharmacyDetails = _MedBusinessLayer.GetPharmacyDetails(objCustomMedication.PharmacyID)
                        If Not IsNothing(dtPharmacyDetails.Rows(0)("sNCPDPID")) AndAlso Not IsDBNull(dtPharmacyDetails.Rows(0)("sNCPDPID")) Then
                            _MedBusinessLayer.PhNCPDPID = dtPharmacyDetails.Rows(0)("sNCPDPID")
                        Else
                            _MedBusinessLayer.PhNCPDPID = ""
                        End If
                        If Not IsNothing(dtPharmacyDetails.Rows(0)("nContactID")) AndAlso Not IsDBNull(dtPharmacyDetails.Rows(0)("nContactID")) Then
                            _MedBusinessLayer.PhContactID = dtPharmacyDetails.Rows(0)("nContactID")
                        Else
                            _MedBusinessLayer.PhContactID = 0
                        End If
                        If Not IsNothing(dtPharmacyDetails.Rows(0)("sName")) AndAlso Not IsDBNull(dtPharmacyDetails.Rows(0)("sName")) Then
                            _MedBusinessLayer.PharmacyName = dtPharmacyDetails.Rows(0)("sName")
                        Else
                            _MedBusinessLayer.PharmacyName = ""
                        End If
                        If Not IsNothing(dtPharmacyDetails.Rows(0)("sAddressLine1")) AndAlso Not IsDBNull(dtPharmacyDetails.Rows(0)("sAddressLine1")) Then
                            _MedBusinessLayer.PhAddressline1 = dtPharmacyDetails.Rows(0)("sAddressLine1")
                        Else
                            _MedBusinessLayer.PhAddressline1 = ""
                        End If
                        If Not IsNothing(dtPharmacyDetails.Rows(0)("sAddressLine2")) AndAlso Not IsDBNull(dtPharmacyDetails.Rows(0)("sAddressLine2")) Then
                            _MedBusinessLayer.PhAddressline2 = dtPharmacyDetails.Rows(0)("sAddressLine2")
                        Else
                            _MedBusinessLayer.PhAddressline2 = ""
                        End If

                        If Not IsNothing(dtPharmacyDetails.Rows(0)("sCity")) AndAlso Not IsDBNull(dtPharmacyDetails.Rows(0)("sCity")) Then
                            _MedBusinessLayer.PhCity = dtPharmacyDetails.Rows(0)("sCity")
                        Else
                            _MedBusinessLayer.PhCity = ""
                        End If

                        If Not IsNothing(dtPharmacyDetails.Rows(0)("sState")) AndAlso Not IsDBNull(dtPharmacyDetails.Rows(0)("sState")) Then
                            _MedBusinessLayer.PhState = dtPharmacyDetails.Rows(0)("sState")
                        Else
                            _MedBusinessLayer.PhState = ""
                        End If
                        If Not IsNothing(dtPharmacyDetails.Rows(0)("sZIP")) AndAlso Not IsDBNull(dtPharmacyDetails.Rows(0)("sZIP")) Then
                            _MedBusinessLayer.PhZip = dtPharmacyDetails.Rows(0)("sZIP")
                        Else
                            _MedBusinessLayer.PhZip = ""
                        End If

                        If Not IsNothing(dtPharmacyDetails.Rows(0)("sEmail")) AndAlso Not IsDBNull(dtPharmacyDetails.Rows(0)("sEmail")) Then
                            _MedBusinessLayer.PhEmail = dtPharmacyDetails.Rows(0)("sEmail")
                        Else
                            _MedBusinessLayer.PhEmail = ""
                        End If
                        If Not IsNothing(dtPharmacyDetails.Rows(0)("sFax")) AndAlso Not IsDBNull(dtPharmacyDetails.Rows(0)("sFax")) Then
                            _MedBusinessLayer.PhFax = dtPharmacyDetails.Rows(0)("sFax")
                        Else
                            _MedBusinessLayer.PhFax = ""
                        End If
                        If Not IsNothing(dtPharmacyDetails.Rows(0)("sPhone")) AndAlso Not IsDBNull(dtPharmacyDetails.Rows(0)("sFax")) Then
                            _MedBusinessLayer.PhPhone = dtPharmacyDetails.Rows(0)("sPhone")
                        Else
                            _MedBusinessLayer.PhPhone = ""
                        End If
                        If Not IsNothing(dtPharmacyDetails.Rows(0)("sServiceLevel")) AndAlso Not IsDBNull(dtPharmacyDetails.Rows(0)("sServiceLevel")) Then
                            _MedBusinessLayer.PhServiceLevel = dtPharmacyDetails.Rows(0)("sServiceLevel")
                        Else
                            _MedBusinessLayer.PhServiceLevel = ""
                        End If

                    End If

                    Dim dtPharma As New DataTable
                    Dim dtRow As DataRow
                    dtPharma.Columns.Add("nContactID")
                    dtPharma.Columns.Add("sName")
                    dtRow = dtPharma.NewRow()
                    dtRow.Item(0) = dtPharmacyDetails.Rows(0)("nContactID")
                    dtRow.Item(1) = dtPharmacyDetails.Rows(0)("sName")
                    dtPharma.Rows.Add(dtRow)

                    objCustomMedication.SetPharmacyData(dtPharma, nRxModulePatientID)

                    dtPharma.Dispose()
                    dtPharma = Nothing

                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.ConfidentialInformation, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    MessageBox.Show("Error on UserListControl" & ex.ToString(), "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    ex = Nothing
                End Try
            End If
        End If

    End Sub

    Private Sub oListUsers_ItemClosedClick(ByVal sender As Object, ByVal e As EventArgs)
        If Not IsNothing(oListUsers) Then
            For i As Integer = Me.Controls.Count - 1 To 0 Step -1
                If Me.Controls(i).Name = oListUsers.Name Then
                    'SLR: Remove handler if any before dispose
                    Dim otemplistcontrol As gloListControl.gloListControl
                    otemplistcontrol = Me.Controls(i)

                    Me.Controls.Remove(otemplistcontrol)

                    Exit For
                End If
            Next
            Try
                RemoveHandler oListUsers.ItemSelectedClick, AddressOf oListUsers_ItemSelectedClick
                RemoveHandler oListUsers.ItemClosedClick, AddressOf oListUsers_ItemClosedClick
                RemoveHandler oListUsers.AddFormHandlerClick, AddressOf oListUsers_AddFormHandlerClick
                RemoveHandler oListUsers.ModifyFormHandlerClick, AddressOf oListUsers_ModifyFormHandlerClick
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                ex = Nothing
            End Try
            oListUsers.Dispose()
            oListUsers = Nothing
        End If
    End Sub
    Private Sub oListUsers_AddFormHandlerClick(ByVal sender As Object, ByVal e As EventArgs)
        If oListUsers.ControlHeader = "Users" Then

            Dim ofrmAddContact As New gloContacts.frmSetupPharmacy(GetConnectionString())
            ofrmAddContact.ShowDialog(IIf(IsNothing(ofrmAddContact.Parent), Me, ofrmAddContact.Parent))
            If ofrmAddContact.DialogResult = DialogResult.OK Then
                oListUsers.FillListAsCriteria(ofrmAddContact.ContactID)
            End If
            ofrmAddContact.Dispose()
            ofrmAddContact = Nothing
        End If
    End Sub
    Private Sub oListUsers_ModifyFormHandlerClick(ByVal sender As Object, ByVal e As EventArgs)
        Dim _contactid As Int64 = 0
        If oListUsers.ControlHeader = "Users" Then
            If Not IsNothing(oListUsers.dgListView.CurrentRow) Then
                _contactid = Convert.ToInt64(oListUsers.dgListView("nContactID", oListUsers.dgListView.CurrentRow.Index).Value)
            End If
            If oListUsers.dgListView.Rows.Count <> 0 Then
                Dim ofrmModifyContact As New gloContacts.frmSetupPharmacy(_contactid, GetConnectionString())
                ofrmModifyContact.ShowDialog(IIf(IsNothing(ofrmModifyContact.Parent), Me, ofrmModifyContact.Parent))
                If ofrmModifyContact.DialogResult = DialogResult.OK Then
                    oListUsers.FillListAsCriteria(ofrmModifyContact.ContactID)
                End If
                ofrmModifyContact.Dispose()
                ofrmModifyContact = Nothing
            End If
        End If
    End Sub
#End Region

#Region "General Functions"

    Private Function SaveMedication() As Boolean
        Try
            UpdateLog("********************************Saving MEDICATION****************************")
            Cursor.Current = Cursors.WaitCursor

            UpdateLog("Medication save Started.......................")
            If _MedBusinessLayer.MedicationCol.Count > 0 Then
                If _MedBusinessLayer.SaveMedication() Then
                    UpdateLog("Medication save Completed.......................")
                Else
                    Return False
                End If
            End If

            Return True

        Catch ex As MedicationBusinessLayerException
            Cursor.Current = Cursors.Default
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
            Return False '''''function will return false if there is any error and the form will not close
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
            Cursor.Current = Cursors.Default
            Return False '''''function will return false if there is any error and the form will not close
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Function

    Private Sub InitialiseControls()
        Dim oEligibilityCheck As New clsEligibilityCheckDBLayer()
        Dim dt271Info As DataTable = Nothing 'SLR: new is not needed
        Try
            Call Get_PatientDetails()

            globalSecurity.gnUserID = gnLoginID
            globalSecurity.gstrLoginName = gstrLoginName
            gloSureScript.gloSurescriptGeneral.sUserName = gstrSQLUserEMR
            gloSureScript.gloSurescriptGeneral.sPassword = gstrSQLPasswordEMR

            clsgeneral.gnThresholdSetting = gnThresholdSetting
            clsgeneral.gblnRecordLocking = gblnRecordLocking
            globalSecurity.gstrClientMachineName = gstrClientMachineName

            If Not IsNothing(GloRxToolBarUserCtrl1) Then
                GloRxToolBarUserCtrl1.Dispose()
                GloRxToolBarUserCtrl1 = Nothing
            End If
            GloRxToolBarUserCtrl1 = New gloRxToolbarUserCtrl
            GloRxToolBarUserCtrl1.Dock = DockStyle.Fill
            pnlmainToolBar.Controls.Add(GloRxToolBarUserCtrl1)

            GloRxToolBarUserCtrl1.Visible = True

            'pnlmainToolBar.BringToFront()
            pnlmainToolBar.Width = 767
            _RxBusinessLayer.TransactionMode = RxBusinesslayer._TransactionMode.Add

            ' C1Formulary.Rows.Count = 1 ''''Bug #8261: Rx MEds >> Application Opening Blank form for Alternative Drugs
            If Not IsNothing(_RxListUserCtrl) Then
                _RxListUserCtrl.Dispose()
                _RxListUserCtrl = Nothing
            End If

            _RxListUserCtrl = New gloDrugListRevised(gnDrugListButton, gblnAllowAddDrugs, gblnAllowDrugConfig, _RxBusinessLayer.ProviderID, gnClinicID, gnLoginID, nRxModulePatientID)
            AddHandler _RxListUserCtrl.DrugListClicked, AddressOf _RxListUserCtrl_DrugListClicked

            '_RxListUserCtrl = New gloRxListUserCtrl(nRxModulePatientID, _RxBusinessLayer, gnDrugListButton, _MedBusinessLayer, gnLoginProviderID, gblnAllowAddDrugs, "", "", Nothing, rtfFormularyDescription, pnlFormularyProgress)

            If Not IsNothing(_RxHistoryUserCtrl) Then
                _RxHistoryUserCtrl.Dispose()
                _RxHistoryUserCtrl = Nothing
            End If
            If Not IsNothing(_MxHistoryUserCtrl) Then
                _MxHistoryUserCtrl.Dispose()
                _MxHistoryUserCtrl = Nothing
            End If
            _RxHistoryUserCtrl = New gloRxHistoryUserCtrl(_RxBusinessLayer, nRxModulePatientID)
            _MxHistoryUserCtrl = New gloMedHistoryUserCtrl(_MedBusinessLayer, nRxModulePatientID)
            'commented in 6040 to reduce trips (this will called only show Hide button for history
            '_MxHistoryUserCtrl.RefreshMedicationHistory()
            '_RxHistoryUserCtrl.RefreshPrescriptionHistory()
            If Not IsNothing(_RxC1Flexgrid) Then
                Try
                    RemoveHandler _RxC1Flexgrid._FlexSelChange, AddressOf _RxC1Flexgrid__FlexSelChange
                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    ex = Nothing
                End Try

                _RxC1Flexgrid.Dispose()
                _RxC1Flexgrid = Nothing
            End If
            _RxC1Flexgrid = New gloRxC1FlexGrdPrescriptionUserCtrl(_RxBusinessLayer, nRxModulePatientID, gnLoginProviderID, IsFormLocked)
            _RxC1Flexgrid.EducationMaterialEnabled = gblnEducationMaterialEnabled
            _RxC1Flexgrid.AdvancedReferenceEnabled = gblnAdvancedReferenceEnabled

            If Not IsNothing(_MxC1Flexgrid) Then
                _MxC1Flexgrid.Dispose()
                _MxC1Flexgrid = Nothing
            End If
            _MxC1Flexgrid = New gloMedicationC1FlexGrdUserCtrl(nRxModulePatientID, _MedBusinessLayer, IsFormLocked, _RxBusinessLayer)

            _MxC1Flexgrid.Threshold = gnThresholdSetting
            _MxC1Flexgrid.EducationMaterialEnabled = gblnEducationMaterialEnabled
            _MxC1Flexgrid.AdvancedReferenceEnabled = gblnAdvancedReferenceEnabled


            pnlRefill.Visible = False

            _RxC1Flexgrid.AutoSize = True

            pnlFlexGrid.AutoScroll = True
            pnlFlexGrid.VerticalScroll.Enabled = True
            pnlFlexGrid.HorizontalScroll.Enabled = True

            pnlcenter.VerticalScroll.Enabled = True
            pnlcenter.HorizontalScroll.Enabled = True

            pnlDIScreenResult.AutoScroll = True
            pnlDIScreenResult.HorizontalScroll.Enabled = True
            pnlDIScreenResult.VerticalScroll.Enabled = True

        Catch ex As Exception
            If Not IsNothing(oEligibilityCheck) Then
                oEligibilityCheck.Dispose()
                oEligibilityCheck = Nothing
            End If
            If Not IsNothing(dt271Info) Then
                dt271Info.Dispose()
                dt271Info = Nothing
            End If
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally
            If Not IsNothing(oEligibilityCheck) Then
                oEligibilityCheck.Dispose()
                oEligibilityCheck = Nothing
            End If
            If Not IsNothing(dt271Info) Then
                dt271Info.Dispose()
                dt271Info = Nothing
            End If
        End Try
    End Sub

    Private Sub AddRxControls()
        _RxListUserCtrl.Dock = DockStyle.Left
        pnlcenter.Controls.Add(_RxListUserCtrl)

        pnlRxHistory.Controls.Add(_RxHistoryUserCtrl)
        _RxHistoryUserCtrl.Dock = DockStyle.Fill
        pnlRxHistory.BringToFront()
        Splitter1.BringToFront()


        pnlMxHistory.Controls.Add(_MxHistoryUserCtrl)
        _MxHistoryUserCtrl.Dock = DockStyle.Fill
        pnlMxHistory.BringToFront()

        pnlRight.Visible = False
        splRight.Visible = False

        pnlPrescriptionDetails.Controls.Add(_RxC1Flexgrid)
        _RxC1Flexgrid.Dock = DockStyle.Fill
        _RxC1Flexgrid.BringToFront()
        _MxC1Flexgrid.Dock = DockStyle.Fill

        pnlMedicationDetails.Controls.Add(_MxC1Flexgrid)

        _RxC1Flexgrid.BringToFront()

        pnlRefill.Visible = False

    End Sub

    Public Function LockForm(ByVal nPatientID As Long) As Boolean
        Try
            If IsFormLocked = False Then
                Dim dt As DataTable
                dt = clsgeneral.Scan_n_Lock_FormLevel(nPatientID, 0, 0, "RxMeds")
                If dt.Rows(0)("IsOpen") = 1 Then
                    IsFormLocked = True
                    'from refill request since the variables are not set then exception was given
                    If Not IsNothing(_MxC1Flexgrid) Then
                        _MxC1Flexgrid.formLock = IsFormLocked
                    End If
                    If Not IsNothing(_RxC1Flexgrid) Then
                        _RxC1Flexgrid.formLock = IsFormLocked
                    End If
                    If System.Windows.Forms.MessageBox.Show("Rx-Meds for this patient are currently being modified by " & dt.Rows(0)("UserName").ToString & " on " & dt.Rows(0)("MachineName").ToString & ". Please close out of Rx-Meds from the other session in order to make modifications from this computer." & vbNewLine & vbNewLine & "Would you like to launch Rx-Meds in view only mode?", clsgeneral.gstrMessageBoxCaption, Windows.Forms.MessageBoxButtons.YesNo, Windows.Forms.MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

                        Return True ''open the form
                    Else

                        If LockID <> 0 Then
                            Delete_Lock_FormLevel(LockID, nRxModulePatientID)
                        End If
                        blncancel = True
                        Return False ''Do not open the form
                    End If

                Else

                    IsFormLocked = False
                    LockID = dt.Rows(0)("FormLevelID")
                    'from refill request since the variables are not set then exception was given
                    If Not IsNothing(_MxC1Flexgrid) Then
                        _MxC1Flexgrid.formLock = IsFormLocked
                    End If
                    If Not IsNothing(_RxC1Flexgrid) Then
                        _RxC1Flexgrid.formLock = IsFormLocked
                    End If
                    blncancel = True
                    Return True ''open the form
                End If

                'SLR: Free dt
                If Not dt Is Nothing Then
                    dt.Dispose()
                    dt = Nothing
                End If
            End If
            Return Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
            Return False ''Do not open the form
        Finally

        End Try
    End Function

    Public Sub ShowMedication_presc()
        Try
            Dim _IsViewMode As Boolean
            _IsViewMode = LockForm(nRxModulePatientID)
            '00000153 : No Data Populates in Medication Pane if Open from Prescription Liquid Link
            'Previously All parameters not passed to following function so the values get interchanged.
            _MedBusinessLayer.PopulateMedicationHistory(arrDrugs, blncancel, IsFormLocked, _IsViewMode)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.CreateMedication, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
            blncancel = True
        End Try
    End Sub

    Public Function ShowReconcileMessage()
        If IsFormLocked = False Then
            GloRxToolBarUserCtrl1.tlb_Reconcile.Enabled = False
            Dim _isReadyLists As Boolean = False
            Dim ogloCCDReconcile As New gloCCDLibrary.gloCCDReconcilation
            _isReadyLists = ogloCCDReconcile.IsReadyListsPresent(nRxModulePatientID, "Medication")
            If Not IsNothing(ogloCCDReconcile) Then
                ogloCCDReconcile.Dispose()
                ogloCCDReconcile = Nothing
            End If
            If _isReadyLists = True Then
                GloRxToolBarUserCtrl1.tlb_Reconcile.Enabled = True
                MessageBox.Show("Patient has Pending Clinical Reconciliations. ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2)
            Else
                GloRxToolBarUserCtrl1.tlb_Reconcile.Enabled = False
            End If
        End If
        Return Nothing
    End Function

    Public Function ShowMedication()
        Try
            Dim _IsViewMode As Boolean = LockForm(nRxModulePatientID)

            _MedBusinessLayer.PopulateMedicationHistory(arrDrugs, blncancel, IsFormLocked, _IsViewMode)

            ShowReconcileMessage()

            If Not IsNothing(myCaller1) Then
                myCaller1.GetdataFromOtherForms(gloEMRWord.enumDocType.Medication)
            End If
            If Not IsNothing(myLetter) Then
                myLetter.GetdataFromOtherForms(gloEMRWord.enumDocType.Medication)
            End If

        Catch ex As gloDBException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.CreateMedication, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
            blncancel = True
            Return Nothing
        Catch ex As MedicationDatabaseLayerException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.CreateMedication, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
            blncancel = True
            Return Nothing
        Catch ex As MedicationBusinessLayerException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.CreateMedication, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
            blncancel = True
            Return Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.CreateMedication, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
            blncancel = True
            Return Nothing
        End Try
    End Function



    Private Function SplitDrug(ByVal _strDuration As String) As Array
        Try
            Dim _result As String()
            _result = _strDuration.Split("|")
            Return _result
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
            Return Nothing
        End Try

    End Function

    Private Sub SetSigDetails()
        Dim strCmbDuration As String = ""
        Dim strDuration As String = ""
        Dim strQuantity As String = ""

        If GridStatus = 1 Then
            If dt.Rows.Count > 0 Then
                If rowindex <= ReferralCount Then
                    If Not IsDBNull(objSigControl) Then

                        If Not IsNothing(objCustomPrescription) Then '''''Bug-4506 added the dodage details to Custom Prescription  Control
                            If objCustomPrescription.route = "" Then
                                objCustomPrescription.route = objSigControl._UCflex.GetData(objSigControl._UCflex.Row, "Route")
                            End If
                            objCustomPrescription.Frequency = objSigControl._UCflex.GetData(objSigControl._UCflex.Row, "Frequency")
                            If objSigControl._UCflex.GetData(objSigControl._UCflex.Row, "Refills") <> "" Then
                                objCustomPrescription.Refills = objSigControl._UCflex.GetData(objSigControl._UCflex.Row, "Refills")
                            Else
                                objCustomPrescription.Refills = "0"
                            End If

                            strQuantity = objSigControl._UCflex.GetData(objSigControl._UCflex.Row, "sAmount_SplitChr")
                            objCustomPrescription.Amount = String.Empty

                            If strQuantity <> "" Then
                                Dim retval1 As String() = Split(strQuantity.Trim, "|")
                                If Not IsNothing(retval1) Then
                                    If retval1.Length >= 1 Then
                                        If IsNumeric(retval1(0)) Then
                                            objCustomPrescription.Amount = retval1(0)
                                        End If
                                    End If
                                End If
                            End If

                            strDuration = objSigControl._UCflex.GetData(objSigControl._UCflex.Row, "sDuration_SplitChr")
                            objCustomPrescription.Duration = String.Empty
                            If strDuration <> "" Then
                                Dim retval As String() = SplitDrug(strDuration)
                                If Not IsNothing(retval) Then
                                    If retval.Length >= 1 Then
                                        objCustomPrescription.Duration = retval(0) 'this will assign the duration value to the TxtDuration text box of the CustomPrescription control
                                        strCmbDuration = retval(retval.Length - 1) ' retrieve the values of Days/Weeks/Months accordingly set in the Sig Control
                                    Else
                                        objCustomPrescription.Duration = retval(0)
                                    End If
                                End If
                            End If

                            If strCmbDuration <> "" Then

                                If strCmbDuration.ToUpper() = "DAYS" Then
                                    objCustomPrescription.cmbDurationDyWkMnth = "Days" '0th item is Days
                                ElseIf strCmbDuration.ToUpper() = "WEEKS" Then
                                    objCustomPrescription.cmbDurationDyWkMnth = "Weeks" '1st item is Weeks
                                Else
                                    objCustomPrescription.cmbDurationDyWkMnth = "Months" '2nd item is Months
                                End If
                            End If
                            objCustomPrescription.RemoveAbbrevationCntrl()
                        Else

                            If Not IsNothing(objCustomMedication) Then
                                If objCustomMedication.route = "" Then
                                    objCustomMedication.route = objSigControl._UCflex.GetData(objSigControl._UCflex.Row, "Route")
                                End If
                                objCustomMedication.Frequency = objSigControl._UCflex.GetData(objSigControl._UCflex.Row, "Frequency")
                                If objSigControl._UCflex.GetData(objSigControl._UCflex.Row, "Refills") <> "" Then
                                    objCustomMedication.Refills = objSigControl._UCflex.GetData(objSigControl._UCflex.Row, "Refills")
                                Else
                                    objCustomMedication.Refills = "0"
                                End If


                                ''GLO2011-0014767 : Quantity not being written out on prescriptions

                                strQuantity = objSigControl._UCflex.GetData(objSigControl._UCflex.Row, "sAmount_SplitChr")

                                If strQuantity <> "" Then
                                    Dim retval1 As String() = Split(strQuantity, "|")

                                    If Not IsNothing(retval1) Then
                                        If retval1.Length >= 1 Then
                                            objCustomMedication.Amount = retval1(0)
                                        Else
                                            objCustomMedication.Amount = ""
                                        End If

                                        If retval1.Length > 1 Then
                                            objCustomMedication.DoseUnit = retval1(1) 'this will assign the duration value to the TxtDuration text box of the CustomPrescription control
                                            ''strCmbDuration = retval(retval.Length - 1) ' retrieve the values of Days/Weeks/Months accordingly set in the Sig Control
                                        Else
                                            objCustomMedication.DoseUnit = ""
                                        End If

                                    Else
                                        objCustomMedication.Amount = ""
                                        objCustomMedication.DoseUnit = ""
                                    End If
                                Else
                                    strQuantity = ""

                                    objCustomMedication.Amount = ""
                                    objCustomMedication.DoseUnit = ""
                                End If
                                ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''




                                strDuration = objSigControl._UCflex.GetData(objSigControl._UCflex.Row, "sDuration_SplitChr")

                                If strDuration <> "" Then
                                    Dim retval As String() = SplitDrug(strDuration)

                                    If Not IsNothing(retval) Then
                                        If retval.Length > 1 Then
                                            objCustomMedication.Duration = retval(0) 'this will assign the duration value to the TxtDuration text box of the CustomPrescription control
                                            strCmbDuration = retval(retval.Length - 1) ' retrieve the values of Days/Weeks/Months accordingly set in the Sig Control
                                        Else
                                            objCustomMedication.Duration = retval(0)
                                        End If

                                    Else
                                        objCustomMedication.Duration = retval(0)
                                    End If
                                Else
                                    strDuration = ""
                                    'Developer:Pradeep
                                    'Date:01/12/2012
                                    'Bug ID/PRD Name/Salesforce Case: 18776
                                    'Reason:textbox were not clearing
                                    objCustomMedication.Duration = ""
                                End If

                                If strCmbDuration <> "" Then

                                    If strCmbDuration.ToUpper() = "DAYS" Then
                                        objCustomMedication.cmbDurationDyWkMnth = "Days" '0th item is Days
                                    ElseIf strCmbDuration.ToUpper() = "WEEKS" Then
                                        objCustomMedication.cmbDurationDyWkMnth = "Weeks" '1st item is Weeks
                                    Else
                                        objCustomMedication.cmbDurationDyWkMnth = "Months" '2nd item is Months
                                    End If
                                End If

                                objCustomMedication.RemoveAbbrevationCntrl()
                            End If
                        End If

                    End If
                End If
            End If
            If Not IsNothing(objSigControl) Then
                Me.Controls.Remove(objSigControl)
                'SLR: ALso it was part of panel also. Hence remove from Panel also
                Me.pnlRefill.Controls.Remove(objSigControl)
                objSigControl.Visible = False
                objSigControl.Dispose()
                objSigControl = Nothing
                rowindex = 0
            End If

        ElseIf GridStatus = 3 Then
            If dt.Rows.Count > 0 Then
                If Not IsNothing(objChiefcomplaints) Then
                    objCustomPrescription.ChiefComplaint = objChiefcomplaints._UCflex.GetData(objChiefcomplaints._UCflex.Row, 1) 'chiefcomplaients coloumn
                    objCustomPrescription.Problems.Add(objChiefcomplaints._UCflex.GetData(objChiefcomplaints._UCflex.Row, 0))
                End If
            End If
            If Not IsNothing(objChiefcomplaints) Then
                Me.Controls.Remove(objChiefcomplaints)
                objChiefcomplaints.Visible = False
                objChiefcomplaints.Dispose()
                objChiefcomplaints = Nothing
                rowindex = 0
            End If

        End If

    End Sub

    Private Function BindSigGrid(Optional ByVal DrugName As String = "")

        ReferralCount = 0
        If GridStatus = 1 Then
            If clsgeneral.blnIsProviderSpecificDrugsBtnSelected = True Then ''''''if provider specific drug btn selected then show the drug provider specific sig values when we click on the custom Rx sig btn
                dt = _RxBusinessLayer.FillSigControls_WithDrugProvAsso(nRxModulePatientID, DrugName)
                If Not IsNothing(dt) Then '''''then there are no values in the drugproviderassociation table then fetch the sig details from SigMst table
                    If dt.Rows.Count > 0 Then
                    Else
                        dt = _RxBusinessLayer.FillSigControls(0) '''''then there are no values in the drugproviderassociation table then fetch the sig details from SigMst table
                    End If
                Else
                    dt = _RxBusinessLayer.FillSigControls(0) '''''then there are no values in the drugproviderassociation table then fetch the sig details from SigMst table
                End If
            Else
                dt = _RxBusinessLayer.FillSigControls(0) '''''then there are no values in the drugproviderassociation table then fetch the sig details from SigMst table
            End If

            Return dt

        ElseIf GridStatus = 2 Then

            Using helper As New gloEMRGeneralLibrary.PrescriptionBusinessLayer()
                dt = helper.GetPrescriptionsByPatient(nRxModulePatientID, Now.Date, 0)
            End Using
            'dt = _RxBusinessLayer.FetchPrescriptionforView(0)
            Return dt
        ElseIf GridStatus = 3 Then
            dt = _RxBusinessLayer.FillProblemlist
            Return dt
        End If
        ReferralCount = objSigControl._flexObj.Rows.Count - 1
        Return Nothing
    End Function

    Public Sub CustomGridStyle(ByVal Grid As DataGrid, ByVal dt As DataTable, ByRef _PrinttextFont As Font)

        Dim ts As New clsDataGridTableStyle(dt.TableName)

        Dim i As Integer

        Dim objstyle As MultiLineColumn
        ' Dim _PrinttextFont As New Font(System.Drawing.FontFamily.GenericSansSerif, 8)

        Dim dbwidth As Double
        dbwidth = Grid.Width
        For i = 0 To dt.Columns.Count - 1
            objstyle = New MultiLineColumn
            objstyle.TextBox.Font = _PrinttextFont
            objstyle.MappingName = dt.Columns(i).ColumnName
            objstyle.HeaderText = dt.Columns(i).ColumnName
            objstyle.Alignment = HorizontalAlignment.Left

            If UCase(dt.Columns(i).ColumnName) = UCase("Drug") Then
                objstyle.Width = Grid.Width * 0.3
                objstyle.AutoAdjustHeight = True
                dbwidth = dbwidth - objstyle.Width
            ElseIf UCase(dt.Columns(i).ColumnName) = UCase("SIG") Then
                objstyle.Width = Grid.Width * 0.3
                objstyle.AutoAdjustHeight = True
                dbwidth = dbwidth - objstyle.Width
            ElseIf UCase(dt.Columns(i).ColumnName) = UCase("Dispense") Then
                objstyle.Width = Grid.Width * 0.11
                objstyle.AutoAdjustHeight = False
            ElseIf UCase(dt.Columns(i).ColumnName) = UCase("Refill") Then
                objstyle.Width = Grid.Width * 0.09
                objstyle.AutoAdjustHeight = False
            ElseIf UCase(dt.Columns(i).ColumnName) = UCase("ChiefComplaint") Then
                objstyle.Width = Grid.Width * 0.17
                objstyle.AutoAdjustHeight = False
            Else
                objstyle.Width = Grid.Width * 0.17
                objstyle.AutoAdjustHeight = False
            End If
            ts.GridColumnStyles.Add(objstyle)
            'objstyle.Dispose()
            'objstyle = Nothing
        Next

        Grid.TableStyles.Clear()
        Grid.TableStyles.Add(ts)

    End Sub

    Private Sub _RxBusinessLayer_PrescriptionDeleted() Handles _RxBusinessLayer.PrescriptionDeleted
        Try
            If pnlRight.Visible = True Then
                _RxHistoryUserCtrl.RefreshPrescriptionHistory()
            End If
            If gblnClinicDISetting Then
                UnloadDrugInteractionControl()
                If Not IsNothing(objDrugInteraction) Then
                    objDrugInteraction.RefreshToolBar()
                End If
            End If
            RemoveControl()
            _RxC1Flexgrid.ClearRows()

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.Delete, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub _RxBusinessLayer_PrescriptionSaved(ByVal SaveStatus As Boolean) Handles _RxBusinessLayer.PrescriptionSaved
        Try
            If SaveStatus Then
                _RxPatientStrip.DTPValue = _RxBusinessLayer.Visitdate
                _RxPatientStrip.DTPEnabled = False

            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    'Private Sub DeleteXML()
    '    If File.Exists(strFileName) Then
    '        File.Delete(strFileName)
    '    End If

    'End Sub

#Region "Prescription Print Functions"

    Private Sub PrintFaxPrescription(Optional ByVal blnIsFax As Boolean = False, Optional ByVal flagIsSendRx As Boolean = False)
        Dim strCheckState As String = ""
        Dim strPrescriptionID As String = ""
        Dim strCheckStateforNarcotic As String = ""
        _RxBusinessLayer.blnIsEPCSEnable = gblnEpcsEnabled
        If _RxBusinessLayer.TmpCheckStatesCol.Count > 0 Then
            Dim _ToAllowCSPrint As Boolean
            If blnIsFax = False Then
                For icnt As Int32 = 0 To _RxBusinessLayer.PrescriptionCol.Count - 1

                    If _RxBusinessLayer.CheckIsNarcotics(_RxBusinessLayer.PrescriptionCol.Item(icnt).DrugID) = False Then  ''//Check For Epcs 

                        If _RxBusinessLayer.PrescriptionCol.Item(icnt).Method <> "Print" And (_RxBusinessLayer.PrescriptionCol.Item(icnt).Method <> "OTC" And RxOTCActions = OTCDrugAction.ExcludeOTC) Then
                            Continue For
                        End If

                        If _RxBusinessLayer.PrescriptionCol.Item(icnt).Status <> "D" Then
                            Dim matchfound As Boolean = False
                            Dim k As Int32 = Nothing
                            For i As Int32 = 0 To _RxBusinessLayer.TmpCheckStatesCol.Count - 1
                                If _RxBusinessLayer.TmpCheckStatesCol.Item(i).ItemNumber = _RxBusinessLayer.PrescriptionCol.Item(icnt).ItemNumber Then
                                    matchfound = True
                                    k = i
                                    Exit For
                                End If
                            Next
                            If matchfound = True Then
                                If strCheckState = "" Then
                                    If _RxBusinessLayer.TmpCheckStatesCol.Item(k).IssueMethod = "Print" Or (_RxBusinessLayer.TmpCheckStatesCol.Item(k).IssueMethod = "OTC" And RxOTCActions = OTCDrugAction.IncludeOTC) Then
                                        If _RxBusinessLayer.TmpCheckStatesCol.Item(k).CheckState = True Then
                                            Dim _IsCPOE As DialogResult
                                            If _RxBusinessLayer.TmpCheckStatesCol.Item(k).PrescriptionID <> 0 And _RxBusinessLayer.TmpCheckStatesCol.Item(k).CPOEOrder = True Then
                                                Dim sDrugName As String = _RxBusinessLayer.PrescriptionCol.Item(icnt).Medication
                                                _IsCPOE = MessageBox.Show("This prescription " & sDrugName & " was already ordered. Are you sure you wish to print?", "gloEMR", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                                                If _IsCPOE = DialogResult.Yes Then
                                                    strCheckState = _RxBusinessLayer.PrescriptionCol.Item(icnt).PrescriptionID
                                                End If
                                            Else
                                                strCheckState = _RxBusinessLayer.PrescriptionCol.Item(icnt).PrescriptionID
                                            End If
                                        End If
                                    End If
                                Else
                                    If _RxBusinessLayer.TmpCheckStatesCol.Item(k).IssueMethod = "Print" Or (_RxBusinessLayer.TmpCheckStatesCol.Item(k).IssueMethod = "OTC" And RxOTCActions = OTCDrugAction.IncludeOTC) Then
                                        If _RxBusinessLayer.TmpCheckStatesCol.Item(k).CheckState = True Then
                                            Dim _IsCPOE As DialogResult
                                            If _RxBusinessLayer.TmpCheckStatesCol.Item(k).PrescriptionID <> 0 And _RxBusinessLayer.TmpCheckStatesCol.Item(k).CPOEOrder = True Then
                                                Dim sDrugName As String = _RxBusinessLayer.PrescriptionCol.Item(icnt).Medication
                                                _IsCPOE = MessageBox.Show("This prescription " & sDrugName & " was already ordered. Are you sure you wish to print?", "gloEMR", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                                                If _IsCPOE = DialogResult.Yes Then
                                                    strCheckState = strCheckState & "," & _RxBusinessLayer.PrescriptionCol.Item(icnt).PrescriptionID
                                                End If
                                            Else
                                                strCheckState = strCheckState & "," & _RxBusinessLayer.PrescriptionCol.Item(icnt).PrescriptionID
                                            End If
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    Else
                        'If _RxBusinessLayer.TmpCheckStatesCol.Item(icnt).EPCSeRxStatus <> "" Then ''//check EPCS status 
                        If _RxBusinessLayer.PrescriptionCol.Item(icnt).Method <> "Print" And (_RxBusinessLayer.PrescriptionCol.Item(icnt).Method <> "OTC" And RxOTCActions = OTCDrugAction.ExcludeOTC) Then
                            Continue For
                        End If
                        If _RxBusinessLayer.PrescriptionCol.Item(icnt).Status <> "D" Then
                            Dim matchfound As Boolean = False
                            Dim k As Int32 = Nothing
                            For i As Int32 = 0 To _RxBusinessLayer.TmpCheckStatesCol.Count - 1
                                If _RxBusinessLayer.TmpCheckStatesCol.Item(i).ItemNumber = _RxBusinessLayer.PrescriptionCol.Item(icnt).ItemNumber Then
                                    matchfound = True
                                    k = i
                                    Exit For
                                End If
                            Next
                            If matchfound = True Then
                                _RxBusinessLayer.TmpCheckStatesCol.Item(k).EPCSeRxStatus = _RxBusinessLayer.GetLatestEPCSStatuslabel(_RxBusinessLayer.PrescriptionCol.Item(icnt).PrescriptionID, _RxBusinessLayer.PrescriptionCol.Item(icnt).IsNarcotics) ''Get Latest EPCS Status For Prescription
                                _ToAllowCSPrint = ToAllowCSPrinting(_RxBusinessLayer.TmpCheckStatesCol.Item(k).EPCSeRxStatus, _RxBusinessLayer.PrescriptionCol.Item(icnt).Medication, _RxBusinessLayer.PrescriptionCol.Item(icnt).PhServiceLevel)
                                If _ToAllowCSPrint = True Then  ''--- To allow CS Printing or not  
                                    If strCheckState = "" Then
                                        If _RxBusinessLayer.TmpCheckStatesCol.Item(k).IssueMethod = "Print" Or (_RxBusinessLayer.TmpCheckStatesCol.Item(k).IssueMethod = "OTC" And RxOTCActions = OTCDrugAction.IncludeOTC) Then
                                            If _RxBusinessLayer.TmpCheckStatesCol.Item(k).CheckState = True Then
                                                Dim _IsCPOE As DialogResult
                                                If _RxBusinessLayer.TmpCheckStatesCol.Item(k).PrescriptionID <> 0 And _RxBusinessLayer.TmpCheckStatesCol.Item(k).CPOEOrder = True Then
                                                    Dim sDrugName As String = _RxBusinessLayer.PrescriptionCol.Item(icnt).Medication
                                                    _IsCPOE = MessageBox.Show("This prescription " & sDrugName & " was already ordered. Are you sure you wish to print?", "gloEMR", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                                                    If _IsCPOE = DialogResult.Yes Then
                                                        strCheckState = _RxBusinessLayer.PrescriptionCol.Item(icnt).PrescriptionID
                                                        strCheckStateforNarcotic = _RxBusinessLayer.PrescriptionCol.Item(icnt).PrescriptionID
                                                    End If
                                                Else
                                                    strCheckState = _RxBusinessLayer.PrescriptionCol.Item(icnt).PrescriptionID
                                                    strCheckStateforNarcotic = _RxBusinessLayer.PrescriptionCol.Item(icnt).PrescriptionID
                                                End If
                                            End If
                                        End If
                                    Else
                                        If _RxBusinessLayer.TmpCheckStatesCol.Item(k).IssueMethod = "Print" Or (_RxBusinessLayer.TmpCheckStatesCol.Item(k).IssueMethod = "OTC" And RxOTCActions = OTCDrugAction.IncludeOTC) Then
                                            If _RxBusinessLayer.TmpCheckStatesCol.Item(k).CheckState = True Then
                                                Dim _IsCPOE As DialogResult
                                                If _RxBusinessLayer.TmpCheckStatesCol.Item(k).PrescriptionID <> 0 And _RxBusinessLayer.TmpCheckStatesCol.Item(k).CPOEOrder = True Then
                                                    Dim sDrugName As String = _RxBusinessLayer.PrescriptionCol.Item(icnt).Medication
                                                    _IsCPOE = MessageBox.Show("This prescription " & sDrugName & " was already ordered. Are you sure you wish to print?", "gloEMR", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                                                    If _IsCPOE = DialogResult.Yes Then
                                                        strCheckState = strCheckState & "," & _RxBusinessLayer.PrescriptionCol.Item(icnt).PrescriptionID
                                                        strCheckStateforNarcotic = strCheckStateforNarcotic & "," & _RxBusinessLayer.PrescriptionCol.Item(icnt).PrescriptionID
                                                    End If
                                                Else
                                                    strCheckState = strCheckState & "," & _RxBusinessLayer.PrescriptionCol.Item(icnt).PrescriptionID
                                                    strCheckStateforNarcotic = strCheckStateforNarcotic & "," & _RxBusinessLayer.PrescriptionCol.Item(icnt).PrescriptionID
                                                End If
                                            End If
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    End If
                Next
                If strCheckState.Length > 0 Then
                    '' RxReferenceNumber parameter passed to PrintRxReport()
                    '' Bug #65840: gloEMR - Rx-Meds - While printing refill request it print twice from print button. 
                    ''BDO Audit
                    If IsRefRequest Then
                        PrintRxReport_New(strCheckState, blnIsFax, _RxBusinessLayer.PrescriptionDate, 0, RefRequest.ReferenceNumber, "", strCheckStateforNarcotic)
                    Else
                        PrintRxReport_New(strCheckState, blnIsFax, _RxBusinessLayer.PrescriptionDate, 0, "", "", strCheckStateforNarcotic)
                    End If

                    If Not blnIsFax Then
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.PrintPrescription, gloAuditTrail.ActivityType.Print, "Prescription Printed", nRxModulePatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                    End If
                End If
            Else '' ******** Send for Fax ********
                Dim pdrFiles As New List(Of String)()

                '' create a list
                For i As Int32 = 0 To _RxBusinessLayer.TmpCheckStatesCol.Count - 1

                    Dim matchfound As Boolean = False
                    Dim k As Int32 = Nothing
                    For a As Int32 = 0 To _RxBusinessLayer.TmpCheckStatesCol.Count - 1
                        If _RxBusinessLayer.TmpCheckStatesCol.Item(a).ItemNumber = _RxBusinessLayer.PrescriptionCol.Item(i).ItemNumber Then
                            matchfound = True
                            k = i
                            Exit For
                        End If
                    Next

                    If _RxBusinessLayer.CheckIsNarcotics(_RxBusinessLayer.TmpCheckStatesCol.Item(i).DrugID) Then
                        If gblnEpcsEnabled = False Then '' Do not Faxed CS drug if EPCS is Enabled.
                            'new code added in 6050 for fax changes
                            If flagIsSendRx <> False Then
                                If blnIsFax = False Then
                                    If strCheckStateforNarcotic = "" Then
                                        strCheckStateforNarcotic = _RxBusinessLayer.TmpCheckStatesCol.Item(i).PrescriptionID
                                    Else
                                        strCheckStateforNarcotic = strCheckStateforNarcotic & "," & _RxBusinessLayer.TmpCheckStatesCol.Item(i).PrescriptionID
                                    End If
                                Else
                                    Dim _IsCPOE As DialogResult
                                    If _RxBusinessLayer.TmpCheckStatesCol.Item(i).PrescriptionID <> 0 And _RxBusinessLayer.TmpCheckStatesCol.Item(i).CPOEOrder = True Then
                                        Dim sDrugName As String = _RxBusinessLayer.PrescriptionCol.Item(i).Medication
                                        If _RxBusinessLayer.TmpCheckStatesCol.Item(i).CheckState = True Then
                                            _IsCPOE = MessageBox.Show("This prescription " & sDrugName & " was already ordered. Are you sure you wish to Fax?", "gloEMR", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                                            If _IsCPOE = DialogResult.Yes Then
                                                If _RxBusinessLayer.TmpCheckStatesCol.Item(i).IssueMethod = "Fax" Or (_RxBusinessLayer.TmpCheckStatesCol.Item(i).IssueMethod = "OTC" And RxOTCActions = OTCDrugAction.IncludeOTC) Then
                                                    If MessageBox.Show("A narcotic drug requires a wet signature to be faxed. Do you want to print it?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information) = Windows.Forms.DialogResult.Yes Then
                                                        strPrescriptionID = _RxBusinessLayer.TmpCheckStatesCol.Item(i).PrescriptionID
                                                        If strPrescriptionID <> "" Then
                                                            _RxBusinessLayer.TmpCheckStatesCol.Item(i).PrintedStatus = "P"
                                                            PrintRxReport_New(strPrescriptionID, False, _RxBusinessLayer.PrescriptionDate)
                                                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.PrintPrescription, gloAuditTrail.ActivityType.Print, "Prescription Printed", nRxModulePatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                                                        End If
                                                    End If
                                                End If
                                            End If
                                        End If
                                    Else
                                        If _RxBusinessLayer.TmpCheckStatesCol.Item(i).IssueMethod = "Fax" Or (_RxBusinessLayer.TmpCheckStatesCol.Item(i).IssueMethod = "OTC" And RxOTCActions = OTCDrugAction.IncludeOTC) Then
                                            If MessageBox.Show("A narcotic drug requires a wet signature to be faxed. Do you want to print it?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information) = Windows.Forms.DialogResult.Yes Then
                                                strPrescriptionID = _RxBusinessLayer.TmpCheckStatesCol.Item(i).PrescriptionID
                                                If strPrescriptionID <> "" Then
                                                    _RxBusinessLayer.TmpCheckStatesCol.Item(i).PrintedStatus = "P"
                                                    PrintRxReport_New(strPrescriptionID, False, _RxBusinessLayer.PrescriptionDate)
                                                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.PrintPrescription, gloAuditTrail.ActivityType.Print, "Prescription Printed", nRxModulePatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                                                End If
                                            End If
                                        End If
                                    End If
                                End If
                            Else
                                Dim _IsCPOE As DialogResult
                                If _RxBusinessLayer.TmpCheckStatesCol.Item(i).PrescriptionID <> 0 And _RxBusinessLayer.TmpCheckStatesCol.Item(i).CPOEOrder = True Then
                                    Dim sDrugName As String = _RxBusinessLayer.PrescriptionCol.Item(i).Medication
                                    If _RxBusinessLayer.TmpCheckStatesCol.Item(i).CheckState = True Then
                                        _IsCPOE = MessageBox.Show("This prescription " & sDrugName & " was already ordered. Are you sure you wish to Fax?", "gloEMR", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                                        If _IsCPOE = DialogResult.Yes Then
                                            If MessageBox.Show("A narcotic drug requires a wet signature to be faxed. Do you want to print it?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information) = Windows.Forms.DialogResult.Yes Then
                                                strPrescriptionID = _RxBusinessLayer.TmpCheckStatesCol.Item(i).PrescriptionID
                                                If strPrescriptionID <> "" Then
                                                    _RxBusinessLayer.TmpCheckStatesCol.Item(i).PrintedStatus = "P"
                                                    PrintRxReport_New(strPrescriptionID, False, _RxBusinessLayer.PrescriptionDate)
                                                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.PrintPrescription, gloAuditTrail.ActivityType.Print, "Prescription Printed", nRxModulePatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                                                End If
                                            End If
                                        End If
                                    End If
                                Else
                                    If MessageBox.Show("A narcotic drug requires a wet signature to be faxed. Do you want to print it?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information) = Windows.Forms.DialogResult.Yes Then
                                        strPrescriptionID = _RxBusinessLayer.TmpCheckStatesCol.Item(i).PrescriptionID
                                        If strPrescriptionID <> "" Then
                                            _RxBusinessLayer.TmpCheckStatesCol.Item(i).PrintedStatus = "P"
                                            PrintRxReport_New(strPrescriptionID, False, _RxBusinessLayer.PrescriptionDate)
                                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.PrintPrescription, gloAuditTrail.ActivityType.Print, "Prescription Printed", nRxModulePatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                                        End If
                                    End If
                                End If

                            End If
                            ''BDO Audit
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.Prescription, gloAuditTrail.ActivityType.None, "Prescription Issued", nRxModulePatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                        Else ''BDO Audit
                            If _RxBusinessLayer.TmpCheckStatesCol.Item(i).IssueMethod = "Fax" Or (_RxBusinessLayer.TmpCheckStatesCol.Item(i).IssueMethod = "OTC" And RxOTCActions = OTCDrugAction.IncludeOTC) Then
                                Dim sDrugName As String = _RxBusinessLayer.PrescriptionCol.Item(i).Medication
                                MessageBox.Show("Because the medication " & sDrugName & " is a controlled substance, it cannot be faxed.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            End If
                        End If
                    Else ''Non CS Drug
                        'new code added in 6050 for fax changes
                        If flagIsSendRx <> False Then
                            If blnIsFax = False Then
                                If strCheckState = "" Then
                                    strCheckState = _RxBusinessLayer.TmpCheckStatesCol.Item(i).PrescriptionID
                                Else
                                    strCheckState = strCheckState & "," & _RxBusinessLayer.TmpCheckStatesCol.Item(i).PrescriptionID
                                End If
                            Else

                                If _RxBusinessLayer.TmpCheckStatesCol.Item(i).PrintedStatus <> "P" Then
                                    If _RxBusinessLayer.TmpCheckStatesCol.Item(i).IssueMethod = "Fax" Or (_RxBusinessLayer.TmpCheckStatesCol.Item(i).IssueMethod = "OTC" And RxOTCActions = OTCDrugAction.IncludeOTC) Then
                                        strPrescriptionID = ""
                                        strPrescriptionID = CheckMultiplePharmacyforDrugs(_RxBusinessLayer.TmpCheckStatesCol.Item(i).DrugPharmacyID)
                                        If strPrescriptionID <> "" Then
                                            PrintRxReport_New(strPrescriptionID, blnIsFax, _RxBusinessLayer.PrescriptionDate, _RxBusinessLayer.TmpCheckStatesCol.Item(i).DrugPharmacyID)
                                            If Not blnIsFax Then
                                                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.PrintPrescription, gloAuditTrail.ActivityType.Print, "Prescription Printed", nRxModulePatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                                            End If
                                        End If
                                    End If
                                End If
                            End If
                        Else
                            Dim _IsCPOE As DialogResult
                            If _RxBusinessLayer.TmpCheckStatesCol.Item(i).IssueMethod = "Fax" Or (_RxBusinessLayer.TmpCheckStatesCol.Item(i).IssueMethod = "OTC" And RxOTCActions = OTCDrugAction.IncludeOTC) Then
                                If _RxBusinessLayer.TmpCheckStatesCol.Item(i).PrescriptionID <> 0 And _RxBusinessLayer.TmpCheckStatesCol.Item(i).CPOEOrder = True Then
                                    Dim sDrugName As String = _RxBusinessLayer.PrescriptionCol.Item(i).Medication
                                    If _RxBusinessLayer.TmpCheckStatesCol.Item(i).CheckState = True Then
                                        _IsCPOE = MessageBox.Show("This prescription " & sDrugName & " was already ordered. Are you sure you wish to Fax?", "gloEMR", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                                        If _IsCPOE = DialogResult.Yes Then
                                            If _RxBusinessLayer.TmpCheckStatesCol.Item(i).PrintedStatus <> "P" Then
                                                If _RxBusinessLayer.TmpCheckStatesCol.Item(i).IssueMethod = "Fax" Or (_RxBusinessLayer.TmpCheckStatesCol.Item(i).IssueMethod = "OTC" And RxOTCActions = OTCDrugAction.IncludeOTC) Then
                                                    strPrescriptionID = ""
                                                    strPrescriptionID = CheckMultiplePharmacyforDrugs(_RxBusinessLayer.TmpCheckStatesCol.Item(i).DrugPharmacyID)
                                                    If strPrescriptionID <> "" Then
                                                        PrintRxReport_New(strPrescriptionID, blnIsFax, _RxBusinessLayer.PrescriptionDate, _RxBusinessLayer.TmpCheckStatesCol.Item(i).DrugPharmacyID)


                                                        If Not blnIsFax Then
                                                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.PrintPrescription, gloAuditTrail.ActivityType.Print, "Prescription Printed", nRxModulePatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                                                        End If
                                                    End If
                                                End If
                                            End If
                                        Else
                                            blnFaxLog = False
                                        End If
                                    End If
                                Else
                                    If _RxBusinessLayer.TmpCheckStatesCol.Item(i).PrintedStatus <> "P" Then
                                        If _RxBusinessLayer.TmpCheckStatesCol.Item(i).IssueMethod = "Fax" Or (_RxBusinessLayer.TmpCheckStatesCol.Item(i).IssueMethod = "OTC" And RxOTCActions = OTCDrugAction.IncludeOTC) Then
                                            strPrescriptionID = ""
                                            strPrescriptionID = CheckMultiplePharmacyforDrugs(_RxBusinessLayer.TmpCheckStatesCol.Item(i).DrugPharmacyID)
                                            If strPrescriptionID <> "" Then
                                                PrintRxReport_New(strPrescriptionID, blnIsFax, _RxBusinessLayer.PrescriptionDate, _RxBusinessLayer.TmpCheckStatesCol.Item(i).DrugPharmacyID)

                                                If Not blnIsFax Then
                                                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.PrintPrescription, gloAuditTrail.ActivityType.Print, "Prescription Printed", nRxModulePatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                                                End If
                                            End If
                                        End If
                                    End If
                                End If
                            End If
                        End If
                        ''BDO Audit
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.Prescription, gloAuditTrail.ActivityType.None, "Prescription Issued", nRxModulePatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)

                        'gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.Prescription, gloAuditTrail.ActivityType.None, "Prescription Faxed", nRxModulePatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)

                    End If
                    If _RxBusinessLayer.TmpCheckStatesCol.Item(k).IssueMethod = "Fax" Or (_RxBusinessLayer.TmpCheckStatesCol.Item(k).IssueMethod = "OTC" And RxOTCActions = OTCDrugAction.IncludeOTC) Then
                        If _RxBusinessLayer.PrescriptionCol(k) IsNot Nothing AndAlso _RxBusinessLayer.PrescriptionCol(k).PDRPrograms IsNot Nothing Then
                            For Each element As Program In DirectCast(_RxBusinessLayer.PrescriptionCol(k).PDRPrograms, ProgramResponse).Programs
                                pdrFiles.Add(GetPDRProgramFile(element.image))
                            Next
                        End If
                    End If
                Next
                If pdrFiles.Any() Then
                    If PrintAllPDRPrograms(pdrFiles) Then
                        AcknowledgeToPDR()
                    Else
                        PDRProgramPrintCanceled()
                    End If
                End If

                pdrFiles.Clear()
                pdrFiles = Nothing

                If strCheckState.Length > 0 Then
                    PrintRxReport_New(strCheckState, blnIsFax, _RxBusinessLayer.PrescriptionDate)
                    If Not blnIsFax Then
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.PrintPrescription, gloAuditTrail.ActivityType.Print, "Prescription Printed", nRxModulePatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                    End If

                End If
                If strCheckStateforNarcotic.Length > 0 Then
                    If MessageBox.Show("A narcotic drug requires a wet signature to be faxed. Do you want to print it?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information) = Windows.Forms.DialogResult.Yes Then
                        PrintRxReport_New(strCheckStateforNarcotic, False, _RxBusinessLayer.PrescriptionDate)
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.PrintPrescription, gloAuditTrail.ActivityType.Print, "Prescription Printed", nRxModulePatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                    End If
                End If
            End If
        End If
    End Sub

    Private Function ToAllowCSPrinting(ByVal _EPCSStatus As String, ByVal csDrugName As String, ByVal sPhServicelevel As String) As Boolean
        Dim _Result As Boolean
        Try
            'Because the medication " & csDrugName & " is a controlled substance, it cannot be print. The Rx will be printed and needs a wet signature before it can be faxed to the pharmacy or handed to the patient
            If gblnEpcsEnabled = True Then
                If gblnAllowPrintForCSEnabled = False Then    '' Don't Allow Print if Print is off & EPCS is on
                    _Result = False
                    MessageBox.Show("Because the medication " & csDrugName & " is a controlled substance, it cannot be printed. ", "gloEMR", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information)
                ElseIf gblnAllowPrintForCSEnabled = True And gbIsProviderEPCSEnable = False Then '' Allow Print if Print is ON & Provider EPCS servicelevel is OFF 
                    _Result = True
                ElseIf gblnAllowPrintForCSEnabled = True And IsPhEPCSServicelevelEnabled(sPhServicelevel) = False Then '' Allow Print if Print is ON & Pharmacy EPCS servicelevel is OFF 
                    _Result = True  ''BDO Audit
                ElseIf gblnAllowPrintForCSEnabled = True And gbIsProviderEPCSEnable = True Then '' Allow Print if Print is ON & Provider EPCS servicelevel is ON & EPCS is ON  ''And _EPCSStatus = "FAILURE"
                    _Result = True
                Else
                    _Result = False
                    MessageBox.Show("Because the medication " & csDrugName & " is a controlled substance, it cannot be printed. ", "gloEMR", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information)
                End If
            Else                             '' If EPCS is Off Allow print with Wet signature (Old Work flow)
                _Result = True
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.PrintPrescription, gloAuditTrail.ActivityType.Print, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
        Return _Result
    End Function

    Private Function IsPhEPCSServicelevelEnabled(ByVal sPhServicelevel As String) As Boolean
        Dim _Result As Boolean = False
        Try
            Dim strServiceLevel As String = String.Empty
            If sPhServicelevel <> "" Then
                strServiceLevel = Convert.ToString(Convert.ToInt64(sPhServicelevel), 2)
            Else
                strServiceLevel = "0000000000000000"
            End If
            Dim arr As Char() = strServiceLevel.ToCharArray()
            Array.Reverse(arr)
            strServiceLevel = New String(arr)
            If strServiceLevel.Length >= 12 Then
                If Not Mid(strServiceLevel, 12, 1) = 1 Then
                    _Result = True
                End If
            Else
                _Result = False
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.PrintPrescription, gloAuditTrail.ActivityType.Print, " Function IsPhEPCSServicelevelEnabled : " & ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
        Return _Result
    End Function

    Private Sub PrintRx(Optional ByVal blnIsFax As Boolean = False, Optional ByVal flagIsSendRx As Boolean = False)
        If _isSupervisormessagedisplay = True Then
            Exit Sub
        End If
        If RxOTCActions = OTCDrugAction.Cancel Then
            Exit Sub
        End If

        If isUnspecifiedMessageOccured = False Then
            Exit Sub
        End If

        Try
            '            Dim _Count As Integer

            Cursor.Current = Cursors.WaitCursor

            If _RxBusinessLayer.PrescriptionCol.Count > 0 Then
                If CheckItemsExists() = True Then
                    'If flagIsSendRx = False Then
                    '    'If _RxC1Flexgrid.InsertCheckState Then
                    '    PrintFaxPrescription(blnIsFax, flagIsSendRx)
                    '    'End If
                    'Else
                    '    PrintFaxPrescription(blnIsFax, flagIsSendRx)
                    'End If

                    PrintFaxPrescription(blnIsFax, flagIsSendRx)

                End If
                If IsRefRequest OrElse IsChangeRequest Then 'code added to Delete Prescription if electronic transmission cancel or fail which already save in prescription table ,if refillrequest ,index error issue
                    For icntDelete As Int32 = 0 To _RxBusinessLayer.PrescriptionCol.Count - 1
                        If (_RxBusinessLayer.PrescriptionCol(icntDelete).MessageType = "RefillRequest" OrElse _RxBusinessLayer.PrescriptionCol(icntDelete).MessageType = "RxChangeRequest") And _RxBusinessLayer.PrescriptionCol(icntDelete).Method = "eRx" And _RxBusinessLayer.PrescriptionCol(icntDelete).FlagtoDeletePrescription Then
                            gloEMRGeneralLibrary.gloGeneral.clsgeneral.blnIsRxMedsFormEdited = True
                            gloEMRGeneralLibrary.gloGeneral.clsgeneral.blnRxDeleteFlag = True
                            _RxBusinessLayer.DeletePrescriptionItem(_RxBusinessLayer.PrescriptionCol.Item(icntDelete).PrescriptionID, _RxBusinessLayer.PrescriptionCol.Item(icntDelete).VisitID, nRxModulePatientID)
                            _RxBusinessLayer.PrescriptionCol.Item(icntDelete).State = "A"
                            _MedBusinessLayer.MedicationCol.Clear() '''' issue CAS-02131-V7D4G1 Bug #100985                            
                            _MedBusinessLayer.FetchMedicationforUpdate(False)
                        End If
                    Next
                End If
            End If



        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.Print, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show("Unable to send the Print prescription due to " & ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ex = Nothing
        Finally
            Cursor.Current = Cursors.Default
            'DeleteXML()
        End Try

    End Sub

#End Region

#Region "Printing Rx Report"

    Private Sub PrintRxReport_New(ByVal sCheckstate As String, ByVal blnIsFax As Boolean, ByVal dtPrescriptiondate As DateTime, Optional ByVal _PharmacyID As Int64 = 0, Optional ByVal _rxReferenceNumber As String = "", Optional ByVal _ShowPrintDNTFNote As String = "", Optional ByVal sNarcoticPrescriptionIDs As String = "", Optional ByVal IsChangeRequest As Boolean = False)
        Dim _ShowPrintDialog As Boolean = False

        If _IsOpenPrintDialog = True Then
            _ShowPrintDialog = True
        ElseIf _IsPrintImmediately = True Then
            _ShowPrintDialog = False
        Else
            _ShowPrintDialog = Not gblnUseDefaultPrinter
        End If

        If Not oProvider Is Nothing Then
            oProvider.Dispose()
            oProvider = Nothing
        End If

        oProvider = New clsProvider

        If gblnMultipleSupervisorsforPaperRx Then
            If oProvider.IsProvider_Senior(_RxBusinessLayer.ProviderID) = False Then

                Dim dtAssociation As DataTable
                Dim lnVisitID As Long = GetVisitID(dtPrescriptiondate)

                dtAssociation = RxBusinesslayer.GetRxProviderAssociation(nRxModulePatientID, lnVisitID, dtPrescriptiondate)

                If dtAssociation.Rows.Count = 0 Then
                    If dtProviderAssociation.Rows.Count = 0 Then
                        _RxBusinessLayer.SaveRxProviderAssociation(nRxModulePatientID, lnVisitID, dtPrescriptiondate, _RxBusinessLayer.ProviderID)
                    Else
                        _RxBusinessLayer.SaveRxProviderAssociation(nRxModulePatientID, lnVisitID, dtPrescriptiondate, _RxBusinessLayer.ProviderID, dtProviderAssociation)
                    End If
                End If

                If Not dtAssociation Is Nothing Then
                    dtAssociation.Dispose()
                End If
            End If
        End If

        Try
            Dim strPrescriptiondate As String = GetDatetimeString(dtPrescriptiondate) '_RxBusinessLayer.GetPrescriptiondateString(dtPrescriptiondate)

            If Not IsNothing(strPrescriptiondate) Then
                If strPrescriptiondate <> "" Then

                    Dim bIsprint As Boolean = (Not blnIsFax) ''BDO Audit
                    If gstrPrintMultipleRx_PerScriptPage = True Then
                        If gblnIsCustomizeReport = False Then
                            PrintPrescription_SSRS(bIsprint, "rptMultipleRx", nRxModulePatientID, sCheckstate, strPrescriptiondate, "", _ShowPrintDialog, _PharmacyID, gbIsProviderEPCSEnable, sNarcoticPrescriptionIDs)
                        Else
                            PrintPrescription_SSRS(bIsprint, gstrMultipleRxCustomizeReport, nRxModulePatientID, sCheckstate, strPrescriptiondate, _rxReferenceNumber, _ShowPrintDialog, _PharmacyID, gbIsProviderEPCSEnable, sNarcoticPrescriptionIDs)
                        End If
                    Else
                        If gblnIsCustomizeReport = False Then
                            PrintPrescription_SSRS(bIsprint, "rptSingleRx", nRxModulePatientID, sCheckstate, strPrescriptiondate, _rxReferenceNumber, _ShowPrintDialog, _PharmacyID, gbIsProviderEPCSEnable, sNarcoticPrescriptionIDs)
                        Else
                            PrintPrescription_SSRS(bIsprint, gstrSingleRxCustomizeReport, nRxModulePatientID, sCheckstate, strPrescriptiondate, _rxReferenceNumber, _ShowPrintDialog, _PharmacyID, gbIsProviderEPCSEnable, sNarcoticPrescriptionIDs)
                        End If
                    End If
                End If
            End If

            'If blnIsFax Then
            '    _RxBusinessLayer.SavePrescriptionsIssued(sCheckstate, False, gintLoginSessionID)

            'Else
            '    _RxBusinessLayer.SavePrescriptionsIssued(sCheckstate, False, gintLoginSessionID)
            'End If


        Catch ex As Exception
            MessageBox.Show("Unable to generate Prescription Report" & vbCrLf & ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.PrintPrescription, gloAuditTrail.ActivityType.Print, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally


        End Try
    End Sub

    Private Sub SendToPrinter(ByVal sFileName As String)

        Try
            Dim myLoadWord As gloWord.LoadAndCloseWord = New gloWord.LoadAndCloseWord()
            Try
                Dim oTempDoc As wd.Document = myLoadWord.LoadWordApplication(sFileName)
                gloWord.LoadAndCloseWord.PrintWordDocument(oTempDoc, False, False, nRxModulePatientID)
                myLoadWord.CloseWordOnly(oTempDoc)
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                ex = Nothing
            End Try
            myLoadWord.CloseApplicationOnly()
            myLoadWord = Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally

        End Try


    End Sub

    Public Function GetDatetimeString(ByVal dtDatetime As DateTime) As String
        Dim Strdate As String = String.Empty
        Try
            Strdate = dtDatetime.Month & "/" & dtDatetime.Day & "/" & dtDatetime.Year & " " & dtDatetime.Hour & ":" & dtDatetime.Minute & ":" & dtDatetime.Second & ":" & dtDatetime.Millisecond
        Catch ex As Exception
            MessageBox.Show("Error in convering Datetime to string", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
        Return Strdate
    End Function

    Public Sub PrintPrescription_SSRS(ByVal blnPrint As Boolean, ByVal _ReportName As String, ByVal npatientId As Long, sCheckState As String, ByVal sPrescriptiondate As String, ByVal _RxReffno As String, ByVal IsOpenPrintDialog As Boolean, ByVal _PharmacyID As Int64, ByVal _IsEPCSEnable As Boolean, Optional ByVal sNarcoticPrescriptionIDs As String = "")
        Dim strfilename As String = ""

        Dim _ShowPrintDNTFNote As String = ""

        If IsRefRequest OrElse IsChangeRequest Then
            If IsChangeRequest Then
                _ShowPrintDNTFNote = "This prescription is the response to a change request for a controlled substance because the pharmacy does not support EPCS transaction."
            ElseIf IsRefRequest Then
                _ShowPrintDNTFNote = "This prescription is the response to an electronic refill renewal request for a controlled substance because the pharmacy does not support EPCS transaction."
            End If
        End If



        Try
            Dim _parameterName As String = ""
            Dim _ParameterValue As String = ""

            If _ReportName.Contains("rptSingleRx") Then
                _parameterName = "PatientID,CheckState,strPrescriptiondate,strRxReferenceNumber,IsShowPharmacy,PrintDateTime,IsEPCSEnable,ShowPrintDNTFNote"
                _ParameterValue = npatientId.ToString() & "," & sCheckState.Replace(",", "@") & "," & sPrescriptiondate & "," & _RxReffno & "," & gblnIsPharymacyInclude & "," & DateTime.Now & "," & _IsEPCSEnable & "," & _ShowPrintDNTFNote
            Else
                _parameterName = "PatientID,CheckState,strPrescriptiondate,IsShowPharmacy,PrintDateTime,IsEPCSEnable,ShowPrintDNTFNote"
                _ParameterValue = npatientId.ToString() & "," & sCheckState.Replace(",", "@") & "," & sPrescriptiondate & "," & gblnIsPharymacyInclude & "," & DateTime.Now & "," & _IsEPCSEnable & "," & _ShowPrintDNTFNote
            End If

            mdlFAX.gstrFAXContactPerson = ""
            mdlFAX.gstrFAXContactPersonFAXNo = ""
            mdlFAX.multipleRecipients = False
            mdlFAX.gstrFAXContacts = Nothing
            mdlFAX.gstrFAXType = "Prescription"

            If blnPrint = False Then
                If gblnInternetFax = False Then

                    If isPrinterSettingsSet(True) = False Then
                        Exit Sub
                    End If
                    Try
                        Call MainMenu.SetFAXPrinterDefaultSettings1()
                    Catch ex As Exception
                        MessageBox.Show("Error in medication : " & ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                        ex = Nothing
                    End Try

                    Dim objmytable As mytable
                    Dim objFAX As New clsFAX

                    objmytable = objFAX.GetPharmacyFaxNoForRx(_PharmacyID)

                    If Not IsNothing(objmytable) Then
                        gstrFAXContactPersonFAXNo = objmytable.Description
                        gstrFAXContactPerson = objmytable.Code
                        objmytable.Dispose()
                        objmytable = Nothing
                    End If


                    If Trim(gstrFAXContactPerson) = "" Then
                        gstrFAXContactPerson = InputBox("Please enter the Pharmacy Name", gstrMessageBoxCaption)
                    End If

                    If Trim(gstrFAXContactPersonFAXNo) = "" Then
                        gstrFAXContactPersonFAXNo = InputBox("Please enter the Pharmacy FAX No", gstrMessageBoxCaption)
                    End If

                    If Trim(gstrFAXContactPersonFAXNo) = "" Then
                        MessageBox.Show("You have not entered the FAX No. So FAX will not be send", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If

                    Dim strFAXDocumentName As String
                    strFAXDocumentName = RetrieveFAXDocumentName()

                    If MainMenu.SetFAXPrinterDocumentSettings1(strFAXDocumentName) = False Then Exit Sub

                    objFAX.AddPendingFAX(nRxModulePatientID, gstrFAXContactPerson, gstrMessageBoxCaption, gstrFAXContactPersonFAXNo, gstrLoginName, strFAXDocumentName, System.DateTime.Now, CurrentSendingFAXPriority)
                    objFAX.Dispose()
                    objFAX = Nothing


                    If Not objmytable Is Nothing Then
                        objmytable.Dispose()
                    End If

                    PrintSSRSReport(_ReportName, _parameterName, _ParameterValue, blnPrint, IsOpenPrintDialog)
                Else
                    Dim objmytable As mytable
                    Dim objFAX As New clsFAX

                    objmytable = objFAX.GetPharmacyFaxNoForRx(_PharmacyID)
                    objFAX.Dispose()
                    objFAX = Nothing

                    If Not IsNothing(objmytable) Then
                        gstrFAXContactPersonFAXNo = objmytable.Description
                        gstrFAXContactPerson = objmytable.Code
                    End If

                    If Not objmytable Is Nothing Then
                        objmytable.Dispose()
                    End If
                    If Trim(gstrFAXContactPerson) = "" Then
                        gstrFAXContactPerson = InputBox("Please enter the Pharmacy Name", gstrMessageBoxCaption)
                    End If

                    If Trim(gstrFAXContactPersonFAXNo) = "" Then
                        gstrFAXContactPersonFAXNo = InputBox("Please enter the Pharmacy FAX No", gstrMessageBoxCaption)
                    End If

                    If Trim(gstrFAXContactPersonFAXNo) = "" Then

                        MessageBox.Show("You have not entered the FAX No. So FAX will not be send", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        blnFaxLog = False

                        Exit Sub
                    Else
                        blnFaxLog = True
                    End If

                    strfilename = mdlGeneral.ExamNewFaxFileName(gloSettings.FolderSettings.AppTempFolderPath, ".pdf")
                    Dim ocls As New gloSSRSApplication.clsSSRSRender(gstrSQLServerName, gstrDatabaseName, gblnSQLAuthentication, gstrSQLUserEMR, gstrSQLPasswordEMR)
                    ocls.SSRSGeneratePDF(_ReportName, _parameterName, _ParameterValue, strfilename)

                    ocls = Nothing

                    Dim objFaxReport As New clsPrintFaxReport(nRxModulePatientID)
                    objFaxReport.FaxReport(strfilename)

                    objFaxReport = Nothing
                End If
            Else
                PrintSSRSReport(_ReportName, _parameterName, _ParameterValue, blnPrint, IsOpenPrintDialog)
            End If
            If gblnEpcsEnabled = True Then
                If blnFlagIsPrintSuccessFull = True Then ''BDO Audit
                    If Not String.IsNullOrEmpty(sNarcoticPrescriptionIDs) Then
                        Dim sPrescriptionIDs As String() = Split(sNarcoticPrescriptionIDs, ",")
                        For Each nId As Long In sPrescriptionIDs
                            gloSureScript.gloSurescriptGeneral.UpdatePrescriptionStatusForCS("PRINTED", nId)
                        Next
                        sPrescriptionIDs = Nothing
                    End If
                    'blnFlagIsPrintSuccessFull = False
                End If
            End If

            Dim objAudit As New clsAudit
            If blnPrint = False Then
                objAudit.CreateLog(clsAudit.enmActivityType.PHIExport, "Fax is sent.", gstrLoginName, gstrClientMachineName, 0, True)
            Else
                objAudit.CreateLog(clsAudit.enmActivityType.PHIExport, "Prescription Printed.", gstrLoginName, gstrClientMachineName, 0, True)
            End If
            'SLR:Free objAudit            
            objAudit = Nothing

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.Reports, gloAuditTrail.ActivityType.Print, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try

    End Sub

#End Region

#Region "Medication Print Functions"

    Private Sub Callprint(ByVal blnPrint As Boolean)
        Try
            If _isSupervisormessagedisplay = True Then
                Exit Sub
            End If
            If _MedBusinessLayer.MedicationCol.Count = 0 Then
                Exit Sub
            End If
            Dim _ShowPrintDialog As Boolean = False

            If _IsOpenPrintDialog = True Then
                _ShowPrintDialog = True
            ElseIf _IsPrintImmediately = True Then
                _ShowPrintDialog = False
            Else
                _ShowPrintDialog = Not gblnUseDefaultPrinter
            End If

            Dim _parameterName As String = ""
            Dim _ParameterValue As String = ""
            Dim _ReportName As String = ""
            _ReportName = "rptMedicationList"
            _parameterName = "nPatientID,nVisitID,user"
            _ParameterValue = nRxModulePatientID.ToString().ToString & "," & _MedBusinessLayer.CurrentVisitID & "," & gstrLoginName

            mdlFAX.gstrFAXContactPerson = ""
            mdlFAX.gstrFAXContactPersonFAXNo = ""
            mdlFAX.multipleRecipients = False
            mdlFAX.gstrFAXContacts = Nothing
            'Added code for Fax type against the Problem #00000874
            mdlFAX.gstrFAXType = "Prescription"

            '---------------------------------
            If blnPrint = False Then
                If gblnInternetFax = False Then

                    If isPrinterSettingsSet(True) = False Then
                        Exit Sub
                    End If
                    Try
                        Call MainMenu.SetFAXPrinterDefaultSettings1()
                    Catch ex As Exception
                        MessageBox.Show("Error in medication : " & ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        ex = Nothing
                    End Try

                    ' Dim strFAXTo As String
                    'Dim strFAXNo As String
                    Dim objmytable As mytable
                    Dim objFAX As New clsFAX

                    objmytable = objFAX.GetPharmacyFAXNo(nRxModulePatientID)

                    If Not IsNothing(objmytable) Then
                        gstrFAXContactPersonFAXNo = objmytable.Description
                        gstrFAXContactPerson = objmytable.Code
                    End If

                    If Trim(gstrFAXContactPerson) = "" Then
                        gstrFAXContactPerson = InputBox("Please enter the Pharmacy Name", gstrMessageBoxCaption)
                    End If

                    If Trim(gstrFAXContactPersonFAXNo) = "" Then
                        gstrFAXContactPersonFAXNo = InputBox("Please enter the Pharmacy FAX No", gstrMessageBoxCaption)
                    End If

                    If Trim(gstrFAXContactPersonFAXNo) = "" Then
                        MessageBox.Show("You have not entered the FAX No. So FAX will not be send", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If

                    Dim strFAXDocumentName As String
                    strFAXDocumentName = RetrieveFAXDocumentName()

                    If MainMenu.SetFAXPrinterDocumentSettings1(strFAXDocumentName) = False Then Exit Sub

                    objFAX.AddPendingFAX(nRxModulePatientID, gstrFAXContactPerson, gstrMessageBoxCaption, gstrFAXContactPersonFAXNo, gstrLoginName, strFAXDocumentName, System.DateTime.Now, CurrentSendingFAXPriority)
                    objFAX.Dispose()
                    objFAX = Nothing

                    'SLR: Free objmytable
                    If Not objmytable Is Nothing Then
                        objmytable.Dispose()
                    End If
                    'oRpt.PrintOptions.PrinterName = gstrFAXPrinterName
                    'oRpt.PrintToPrinter(1, False, 0, 0)
                    PrintSSRSReport(_ReportName, _parameterName, _ParameterValue, blnPrint, _ShowPrintDialog)
                Else
                    ' Dim strFAXTo As String
                    'Dim strFAXNo As String
                    Dim objmytable As mytable
                    Dim objFAX As New clsFAX

                    objmytable = objFAX.GetPharmacyFAXNo(nRxModulePatientID)
                    objFAX.Dispose()
                    objFAX = Nothing

                    If Not IsNothing(objmytable) Then
                        gstrFAXContactPersonFAXNo = objmytable.Description
                        gstrFAXContactPerson = objmytable.Code
                    End If
                    'SLR: Free objmytable
                    If Not objmytable Is Nothing Then
                        objmytable.Dispose()
                    End If
                    If Trim(gstrFAXContactPerson) = "" Then
                        gstrFAXContactPerson = InputBox("Please enter the Pharmacy Name", gstrMessageBoxCaption)
                    End If

                    If Trim(gstrFAXContactPersonFAXNo) = "" Then
                        gstrFAXContactPersonFAXNo = InputBox("Please enter the Pharmacy FAX No", gstrMessageBoxCaption)
                    End If

                    If Trim(gstrFAXContactPersonFAXNo) = "" Then
                        MessageBox.Show("You have not entered the FAX No. So FAX will not be send", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        blnFaxLog = False
                        Exit Sub
                    Else

                        blnFaxLog = True
                    End If

                    Dim strfilename As String = mdlGeneral.ExamNewFaxFileName(gloSettings.FolderSettings.AppTempFolderPath, ".pdf")
                    Dim ocls As New gloSSRSApplication.clsSSRSRender(gstrSQLServerName, gstrDatabaseName, gblnSQLAuthentication, gstrSQLUserEMR, gstrSQLPasswordEMR)
                    ocls.SSRSGeneratePDF(_ReportName, _parameterName, _ParameterValue, strfilename)
                    'SLR: Dispose ocls and then
                    ocls = Nothing

                    Dim objFaxReport As New clsPrintFaxReport(nRxModulePatientID)
                    objFaxReport.FaxReport(strfilename)
                    'SLR: Dispose objFaxReport and then
                    objFaxReport = Nothing
                End If
            Else
                PrintSSRSReport(_ReportName, _parameterName, _ParameterValue, blnPrint, _ShowPrintDialog)
            End If

            Dim objAudit As New clsAudit
            objAudit.CreateLog(clsAudit.enmActivityType.PHIExport, "Fax is sent.", gstrLoginName, gstrClientMachineName, 0, True)
            'SLR:Free objAudit            
            objAudit = Nothing

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.Reports, gloAuditTrail.ActivityType.Print, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub PrintSSRSReport(ByVal ReportName As String, ByVal ParameterName As String, ByVal ParameterValue As String, ByVal blnPrint As Boolean, ByVal _ShowPrintDialog As Boolean)
        Dim clsPrntRpt As gloSSRSApplication.clsPrintReport
        Dim _MessageBoxCaption As String = String.Empty
        Dim _databaseConnectionString As String = String.Empty
        Dim _LoginName As String = String.Empty
        Dim gstrSQLServerName As String = String.Empty
        Dim gstrDatabaseName As String = String.Empty
        Dim gblnSQLAuthentication As String = String.Empty
        Dim gstrSQLUserEMR As String = String.Empty
        Dim gstrSQLPasswordEMR As String = String.Empty
        Dim gblnDefaultPrinter As Boolean = False
        Dim appSettings As System.Collections.Specialized.NameValueCollection = System.Configuration.ConfigurationManager.AppSettings

        Try
            If appSettings("DataBaseConnectionString") IsNot Nothing Then
                If appSettings("DataBaseConnectionString") <> "" Then
                    _databaseConnectionString = Convert.ToString(appSettings("DataBaseConnectionString"))
                End If
            End If

            If appSettings("UserName") IsNot Nothing Then
                If appSettings("UserName") <> "" Then
                    _LoginName = Convert.ToString(appSettings("UserName"))
                End If
            End If

            If appSettings("SQLServerName") IsNot Nothing Then
                If appSettings("SQLServerName") <> "" Then
                    gstrSQLServerName = Convert.ToString(appSettings("SQLServerName"))
                End If
            End If

            If appSettings("DatabaseName") IsNot Nothing Then
                If appSettings("DatabaseName") <> "" Then
                    gstrDatabaseName = Convert.ToString(appSettings("DatabaseName"))
                End If
            End If

            If appSettings("SQLLoginName") IsNot Nothing Then
                If appSettings("SQLLoginName") <> "" Then
                    gstrSQLUserEMR = Convert.ToString(appSettings("SQLLoginName"))
                End If
            End If

            If appSettings("SQLPassword") IsNot Nothing Then
                If appSettings("SQLPassword") <> "" Then
                    gstrSQLPasswordEMR = Convert.ToString(appSettings("SQLPassword"))
                End If
            End If

            If appSettings("DefaultPrinter") IsNot Nothing Then
                If appSettings("DefaultPrinter") <> "" Then
                    gblnDefaultPrinter = Not Convert.ToBoolean(appSettings("DefaultPrinter"))
                End If
            End If

            If appSettings("WindowAuthentication") IsNot Nothing Then
                If appSettings("WindowAuthentication") <> "" Then
                    gblnSQLAuthentication = Not Convert.ToBoolean(appSettings("WindowAuthentication"))
                End If
            End If

            Dim pdrFiles As New List(Of String)

            If blnRxC1FlexClick Then
                pdrFiles = GetPDRProgramFiles()
            End If


            clsPrntRpt = New gloSSRSApplication.clsPrintReport(gstrSQLServerName, gstrDatabaseName, gblnSQLAuthentication, gstrSQLUserEMR, gstrSQLPasswordEMR, pdrFiles)

            If blnPrint = False Then
                _ShowPrintDialog = False
                clsPrntRpt.PrintReport(ReportName, ParameterName, ParameterValue, _ShowPrintDialog, gstrFAXPrinterName)
            Else
                clsPrntRpt.PrintReport(ReportName, ParameterName, ParameterValue, _ShowPrintDialog, "")
            End If

            If blnRxC1FlexClick AndAlso clsPrntRpt.IsPrintSuccess Then
                AcknowledgeToPDR()
            ElseIf Not clsPrntRpt.IsPrintSuccess Then
                If pdrFiles.Any() Then
                    PDRProgramPrintCanceled()
                End If
            End If

            blnFlagIsPrintSuccessFull = clsPrntRpt.IsPrintSuccess
            'SLR: dispsoe Con if it is not used again, dispose clsPrntRpt
            If Not clsPrntRpt Is Nothing Then
                clsPrntRpt.Dispose()
                clsPrntRpt = Nothing
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

#End Region

    Private Function CheckMultiplePharmacyforDrugs(ByVal pharmacy As Long) As String
        Dim strcheckstatus As String = ""
        Try
            For i As Int32 = 0 To _RxBusinessLayer.TmpCheckStatesCol.Count - 1
                If _RxBusinessLayer.TmpCheckStatesCol.Item(i).CheckState = True Then
                    If _RxBusinessLayer.CheckIsNarcotics(_RxBusinessLayer.TmpCheckStatesCol.Item(i).DrugID) = False Then
                        If _RxBusinessLayer.TmpCheckStatesCol.Item(i).IssueMethod = "Fax" Or (_RxBusinessLayer.TmpCheckStatesCol.Item(i).IssueMethod = "OTC" And RxOTCActions = OTCDrugAction.IncludeOTC) Then
                            If _RxBusinessLayer.TmpCheckStatesCol.Item(i).DrugPharmacyID = pharmacy Then
                                If strcheckstatus = "" Then
                                    strcheckstatus = _RxBusinessLayer.TmpCheckStatesCol.Item(i).PrescriptionID
                                    _RxBusinessLayer.TmpCheckStatesCol.Item(i).PrintedStatus = "P"
                                Else
                                    strcheckstatus = strcheckstatus & "," & _RxBusinessLayer.TmpCheckStatesCol.Item(i).PrescriptionID
                                    _RxBusinessLayer.TmpCheckStatesCol.Item(i).PrintedStatus = "P"
                                End If
                            End If
                        End If
                    End If
                End If
            Next
            Return strcheckstatus
        Catch ex As Exception
            Return strcheckstatus
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Function

    Private Function CheckItemsExists() As Boolean
        Try
            Dim icnt As Int32 = 0
            For i As Int32 = 0 To _RxBusinessLayer.PrescriptionCol.Count - 1
                If _RxBusinessLayer.PrescriptionCol.Item(i).Status <> "D" Then
                    icnt = icnt + 1
                End If
            Next
            If icnt >= 1 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
            Return Nothing
        End Try
    End Function

    Private Sub SetMethod(Optional ByVal MethodType As String = "")
        Try
            For i As Int16 = 0 To _RxBusinessLayer.PrescriptionCol.Count - 1
                If _RxC1Flexgrid.getCheckedState(i + 1) = True Then
                    If (MethodType <> "") Then
                        '' Skip overriding the method in case of Refill Request & Narcotic drugs
                        '' Bug #65840: gloEMR - Rx-Meds - While printing refill request it print twice from print button. 
                        If ((_RxBusinessLayer.PrescriptionCol.Item(i).IsNarcotics <> 3 Or _RxBusinessLayer.PrescriptionCol.Item(i).IsNarcotics <> 4 Or _RxBusinessLayer.PrescriptionCol.Item(i).IsNarcotics <> 5) And _RxBusinessLayer.PrescriptionCol.Item(i).MessageType <> "RefillRequest") Then
                            If _RxBusinessLayer.PrescriptionCol.Item(i).Method <> "OTC" Then
                                _RxBusinessLayer.PrescriptionCol.Item(i).Method = MethodType
                            End If
                        End If
                        If ((MethodType = "eRx") AndAlso (_RxBusinessLayer.PrescriptionCol.Item(i).MessageType = "")) Then
                            Me._RxBusinessLayer.PrescriptionCol.Item(i).MessageType = "NewRx"
                        End If
                    Else
                        If _RxBusinessLayer.PrescriptionCol.Item(i).Method = "eRx" Then
                            If _RxBusinessLayer.PrescriptionCol.Item(i).MessageType = "" Then
                                _RxBusinessLayer.PrescriptionCol.Item(i).MessageType = "NewRx"
                            End If
                        End If
                    End If
                    _RxBusinessLayer.PrescriptionCol.Item(i).Flag = True
                    If _RxBusinessLayer.PrescriptionCol.Item(i).State = "U" Then
                        _RxBusinessLayer.PrescriptionCol.Item(i).State = "M"
                    End If

                End If
            Next
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub SetFlag()
        Try
            For i As Int16 = 0 To _RxBusinessLayer.PrescriptionCol.Count - 1
                If _RxC1Flexgrid.getCheckedState(i + 1) = True Then
                    _RxBusinessLayer.PrescriptionCol.Item(i).Flag = True
                    If _RxBusinessLayer.PrescriptionCol.Item(i).State = "U" Then
                        _RxBusinessLayer.PrescriptionCol.Item(i).State = "M"
                    End If
                Else
                    _RxBusinessLayer.PrescriptionCol.Item(i).Flag = False
                    If _RxBusinessLayer.PrescriptionCol.Item(i).State = "U" Then
                        _RxBusinessLayer.PrescriptionCol.Item(i).State = "M"
                    End If
                End If
            Next
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub SetRowstate(ByVal Rowstate As String)
        Try
            For i As Int16 = 0 To _RxBusinessLayer.PrescriptionCol.Count - 1

                _RxBusinessLayer.PrescriptionCol.Item(i).State = "M"

            Next
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Public Function SavePrescription() As Int64
        Dim bIsSaved As Boolean = False
        Try
            If gblnSurescriptEnabled Then

                InvalidSurescriptMessage = False
                If IsRefRequest Then
                    'check if drug is denied and send denywnewrxtofollow message
                    Dim Id As Int16 = 0

                    Dim intresult As Int16 = 0
                    intresult = _RxBusinessLayer.CheckForDeniedDrug10dot6(Id)


                    If intresult = 1 Then
                        bIsSaved = _RxBusinessLayer.SavePrescription(gnLoginProviderID)

                        If bIsSaved Then
                            Dim pbmList As New gloSureScript.BenefitsCoordinations
                            If gloEMRGeneralLibrary.gloGeneral.clsgeneral.gblnIsFormularyServiceEnabled = True Then
                                ''if formulary setting is ON fill the Rxeligibility information of patient in prescription collection, this will be used to send the benefit segment in NewRx
                                pbmList = FillERxEligibilityInfo()
                            End If
                            If Not myCaller Is Nothing Then
                                frmPatientExam.blnRxChangesMade = True
                            End If
                            'Now Send a NewRx only for given Prescription ItemIF
                            If isbuttonClickSave = False Then
                                If _RxBusinessLayer.ERxForDeniedPrescriptionItem(RefRequest.MessageID, strPrescriberFirstName, strPrescriberLastName, strPrescriberMiddleName, cmbSupervisingProvider.SelectedValue, pbmList, RefRequest.ReferenceNumber) Then
                                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.eRx, gloAuditTrail.ActivityType.DenyRefillRequest, "Refill request denied ", nRxModulePatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                                End If
                            End If
                        End If
                    ElseIf intresult = 2 Then 'the prescriber decided not to generate any message though substitution code changed
                        InvalidSurescriptMessage = True ''''make this value true becaz we should stay on Rx form. in finally again reset to false
                        SavePrescription = False
                        Exit Function
                    ElseIf intresult = 3 Then
                        MessageBox.Show("Refill response could not be posted, hence prescription will not be saved", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        SavePrescription = False
                        Exit Function
                    ElseIf intresult = 0 Then
                        If Not IsInternetConnectionAvailable Then
                            MessageBox.Show("You are not connected to the internet.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            SavePrescription = False
                            Exit Function
                        End If
                        bIsSaved = _RxBusinessLayer.SavePrescription(gnLoginProviderID)

                        If bIsSaved Then
                            If Not myCaller Is Nothing Then
                                frmPatientExam.blnRxChangesMade = True
                            End If
                        Else
                            MessageBox.Show("You cannot prescribe duplicate drug within the threshold period. Please deny or cancel the request.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If
                    ElseIf intresult = 4 Then
                        'Print the Schedule 3-5 drug and exit sub
                        bIsSaved = _RxBusinessLayer.SavePrescription(gnLoginProviderID)

                        If bIsSaved Then
                            If Not myCaller Is Nothing Then
                                frmPatientExam.blnRxChangesMade = True
                            End If
                            Return 4
                        End If
                    ElseIf intresult = 5 Then
                        'Print the Schedule 3-5 drug and save rest of items and exit sub
                        bIsSaved = _RxBusinessLayer.SavePrescription(gnLoginProviderID)
                        If bIsSaved Then
                            If Not myCaller Is Nothing Then
                                frmPatientExam.blnRxChangesMade = True
                            End If
                            'nDeniedWithNewRxItem = Id
                            Return 5
                        End If
                    ElseIf intresult = -1 Then 'there are no approved drugs in list
                        bIsSaved = _RxBusinessLayer.SavePrescription(gnLoginProviderID)
                        If bIsSaved Then
                            If Not myCaller Is Nothing Then
                                frmPatientExam.blnRxChangesMade = True
                            End If
                        End If
                    End If
                Else
                    bIsSaved = _RxBusinessLayer.SavePrescription(gnLoginProviderID)
                    If bIsSaved Then
                        If Not myCaller Is Nothing Then
                            frmPatientExam.blnRxChangesMade = True
                        End If
                    End If
                End If
            Else
                bIsSaved = _RxBusinessLayer.SavePrescription(gnLoginProviderID)
                If bIsSaved Then
                    If Not myCaller Is Nothing Then
                        frmPatientExam.blnRxChangesMade = True
                    End If
                End If
            End If

            If bIsSaved Then
                If _MedBusinessLayer.CurrentVisitID = 0 Then
                    _MedBusinessLayer.CurrentVisitID = _RxBusinessLayer.CurrentVisitID
                    _MedBusinessLayer.TransactionMode = MedicationBusinessLayer._TransactionMode.Edit
                End If
            End If

            mdlGeneral.gblnIsDrugSave = True
            mdlGeneral.gnPrescriptionVisitID = _RxBusinessLayer.CurrentVisitID
            Return Nothing
        Catch ex As PrescriptionException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
            Return Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
            Return Nothing
        End Try
    End Function

    Private Sub UnloadDrugInteractionControl()
        Try
            If Not IsNothing(objScreeningResults) Then
                pnlDIScreenResult.Controls.Remove(objScreeningResults)
                objScreeningResults.Dispose()
                objScreeningResults = Nothing
            End If

            pnlDIScreenResult.SendToBack()
            pnlcenter.BringToFront()

        Catch ex As Exception
            MessageBox.Show("Error while Unloading Drug interaction control: " & ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function DisplayScreeningResult(ByVal objhash As Hashtable, Optional ByVal m_Drugalert As String = "")
        UnloadDrugInteractionControl()
        objDrugInteraction.RefreshToolBar(objhash, m_Drugalert)
        btntype = objDrugInteraction.Screentype
        Return Nothing
    End Function

#End Region

#Region "Formulary Functions"

    Private Function SplitPBM(ByVal PBMNameHealthPlanName As String) As String
        Try
            Dim _sPBMName As String = ""
            Dim _result As String()
            _result = PBMNameHealthPlanName.Split("-")
            If _result.Length > 1 Then
                _sPBMName = _result(0)
            Else
                _sPBMName = PBMNameHealthPlanName
            End If
            Return _sPBMName
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
            Return Nothing
        End Try

    End Function

    Private Sub FillMenus()
        Try

            Dim oMenuItem As ToolStripMenuItem

            cMnuStrp.Items.Clear()

            oMenuItem = New ToolStripMenuItem
            With oMenuItem
                .Text = "Accept"
                .Tag = "Accept"
            End With

            cMnuStrp.Items.Add(oMenuItem)

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing

        End Try
    End Sub

    Private Function RefreshPBMCombo(Optional ByVal bRefreshFormularyInfo = True)

        Dim dt271Info As DataTable = Nothing
        Dim oEligibilityCheck As New clsEligibilityCheckDBLayer()

        Dim sSelectedPBMMemberID As String = Nothing
        Dim RetRxChangeCOO As RxChangeCOO = Nothing

        Try
            RemoveHandler _objFormularyToolBar.tlStrpPBMCombo.SelectedIndexChanged, AddressOf _objFormularyToolBar.tlStrpPBMCombo_SelectedIndexChanged

            dt271Info = oEligibilityCheck.Get271Information(nRxModulePatientID)

            If Not IsNothing(dt271Info) Then
                If dt271Info.Rows.Count > 0 Then

                    Dim dtPBM As DataTable = New DataTable()

                    With dtPBM.Columns
                        .Add("PBMName")
                        .Add("PBMMemberID")
                    End With

                    _objFormularyToolBar.tlStrpPBMCombo.ComboBox.DataSource = Nothing
                    _objFormularyToolBar.tlStrpPBMCombo.Items.Clear()

                    If (RxChangeRequest IsNot Nothing AndAlso IsChangeRequest = True) Then
                        If RxChangeRequest.Type = SS.ChangeRequestType.PriorAuthorizationRequired Then
                            RetRxChangeCOO = GetRxChangeCOO()
                        End If
                    End If

                    For rwCnt As Integer = 0 To dt271Info.Rows.Count - 1

                        Dim _271FormularyId As String = dt271Info.Rows(rwCnt)("sFormularyListID")
                        Dim _271CoverageId As String = dt271Info.Rows(rwCnt)("sCoverageID")

                        Dim _HealthPlanName As String
                        If dt271Info.Rows(rwCnt)("sHealthPlanBenefitCoverageName") <> "" Then
                            _HealthPlanName = dt271Info.Rows(rwCnt)("sHealthPlanBenefitCoverageName")
                        Else
                            _HealthPlanName = ""
                        End If

                        Dim _PBMName As String = dt271Info.Rows(rwCnt)("sPBM_PayerName")

                        Dim _PBMNameHealthPlnName As String
                        If _HealthPlanName <> "" Then
                            _PBMNameHealthPlnName = _PBMName & "-" & _HealthPlanName
                        Else
                            _PBMNameHealthPlnName = _PBMName
                        End If

                        If dt271Info.Rows(rwCnt)("sRetailPhEligiblityorBenefitInfo") = "Active Coverage" Or dt271Info.Rows(rwCnt)("sMailOrdEligiblityorBenefitInfo") = "Active Coverage" Then
                            dtPBM.Rows.Add()
                            dtPBM.Rows(dtPBM.Rows.Count - 1)("PBMName") = _PBMNameHealthPlnName
                            dtPBM.Rows(dtPBM.Rows.Count - 1)("PBMMemberID") = (dt271Info.Rows(rwCnt)("sPBM_PayerMemberID"))

                            If Not IsNothing(RetRxChangeCOO) Then
                                If (dt271Info.Rows(rwCnt)("BinNumber").ToString().Trim = RetRxChangeCOO.BINLocationNumber _
                                    And dt271Info.Rows(rwCnt)("PayerName").ToString().Trim = RetRxChangeCOO.PayerName _
                                    And dt271Info.Rows(rwCnt)("CardHolderID").ToString().Trim = RetRxChangeCOO.CardholderID _
                                    And dt271Info.Rows(rwCnt)("GroupID").ToString().Trim = RetRxChangeCOO.GroupID) Then
                                    sSelectedPBMMemberID = (dt271Info.Rows(rwCnt)("sPBM_PayerMemberID"))
                                End If
                            End If

                        End If
                    Next
                    If Not IsNothing(dtPBM) Then
                        If dtPBM.Rows.Count > 0 Then
                            Try

                                '00000691: Issues with Rx Eligibility requests
                                _objFormularyToolBar.tlStrpPBMCombo.ComboBox.BindingContext = Me.BindingContext
                                _objFormularyToolBar.tlStrpPBMCombo.ComboBox.DataSource = dtPBM
                                _objFormularyToolBar.tlStrpPBMCombo.ComboBox.DisplayMember = "PBMName"
                                _objFormularyToolBar.tlStrpPBMCombo.ComboBox.ValueMember = "PBMMemberID"
                                If sSelectedPBMMemberID IsNot Nothing Then
                                    _objFormularyToolBar.tlStrpPBMCombo.ComboBox.SelectedValue = sSelectedPBMMemberID
                                End If

                            Catch

                            Finally

                                ''FS3: To upate the PBM info for the first time when it binds
                                ''_objFormularyToolBar.tlStrpPBMCombo_SelectedIndexChanged(Me, Nothing)
                            End Try
                        End If
                    End If

                Else
                    _objFormularyToolBar.tlStrpPBMCombo.Items.Clear()
                    _objFormularyToolBar.tlStrpPBMCombo.ComboBox.DataSource = Nothing
                    _objFormularyToolBar.tlStrpPBMCombo.Items.Clear()
                End If

            Else
                ' _objFormularyToolBar.tlStrpPBMCombo.Items.Clear()
                _objFormularyToolBar.tlStrpPBMCombo.ComboBox.DataSource = Nothing
                _objFormularyToolBar.tlStrpPBMCombo.Items.Clear()

            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally

            AddHandler _objFormularyToolBar.tlStrpPBMCombo.SelectedIndexChanged, AddressOf _objFormularyToolBar.tlStrpPBMCombo_SelectedIndexChanged

            If (_objFormularyToolBar.CurrentEligibilityStatus <> RxBusinesslayer.EligibilityStatus.NotChecked) Then
                _objFormularyToolBar.UpdatePBM()
            End If

            If Not IsNothing(oEligibilityCheck) Then
                oEligibilityCheck.Dispose()
                oEligibilityCheck = Nothing
            End If
            If Not dt271Info Is Nothing Then
                dt271Info.Dispose()
            End If
        End Try
        Return Nothing
    End Function

    Private Sub AdjustFormularyControls()
        Try
            pnlFormulary.Left = pnlcenter.Left + 235

            Try
                pnlFormulary.Top = pnlMedicationGrid.PointToScreen(Point.Empty).Y - 60
            Catch ex As Exception
                pnlFormulary.Top = pnlcenter.Top + CType(_RxC1Flexgrid.Height, Int32) + 90
            End Try

            pnlFormulary.Width = CType(_RxC1Flexgrid.Width, Int32) - 50
            pnlFormulary.BringToFront()

            pnlElementHostCopay.Left = pnlFormulary.Left
            pnlElementHostCopay.Top = pnlFormulary.Bottom
            pnlElementHostCopay.Width = CType(_RxC1Flexgrid.Width, Int32) - 50
            pnlElementHostCopay.BringToFront()

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub AddFormularlyToolBarControl()
        pnlFormularyToolBar.Controls.Clear()
        _objFormularyToolBar.Dock = DockStyle.Fill
        pnlFormularyToolBar.Controls.Add(_objFormularyToolBar)
    End Sub

    ''''this function was especially created to show the Last Eligibility Request Datetime in message box, when the eligibility for patient is expired and user tries to get medication history
    ''''code implemented to resolve residual bug # 42438 added in 7020, function called only when medication history button is clicked 
    Private Function IsEligiblityExpiredCheck() As Boolean
        Dim oEligibilityCheck As New ClsRxHubInterface()
        Dim oClsgloRxHubDBLayer As New gloRxHub.clsgloRxHubDBLayer
        Try

            If gstrRxEligThresholdvalue = "" Then
                Dim objSettings As New clsSettings
                Dim stRxEligThresholdvalue As String = ""
                stRxEligThresholdvalue = objSettings.GetSettingValue("RX ELIGIBILITY THRESHOLD VALUE")
                gstrRxEligThresholdvalue = (Val(stRxEligThresholdvalue) / 24).ToString
                If Not IsNothing(objSettings) Then
                    objSettings.Dispose()
                    objSettings = Nothing
                End If
            End If
            If oEligibilityCheck.IsEligibilitygGenerated_validation(nRxModulePatientID, gstrRxEligThresholdvalue) = True Then
                Dim strBldrMsg = New System.Text.StringBuilder
                strBldrMsg.Append("Eligibility information is not available for this patient. Please generate a eligibility request against this patient" & vbCrLf)
                strBldrMsg.Append("Last eligibility request date time: " & oEligibilityCheck.dtLastEligibilityRequestDatetime)
                MessageBox.Show(strBldrMsg.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                strBldrMsg = Nothing

                Return True ''''eligibility is expired
            Else
                Return False
            End If
            oEligibilityCheck.Dispose()
            oEligibilityCheck = Nothing
            oClsgloRxHubDBLayer.Dispose()
            oClsgloRxHubDBLayer = Nothing
            Return True
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
            Return False
        Finally
            If Not IsNothing(oEligibilityCheck) Then
                oEligibilityCheck.Dispose()
                oEligibilityCheck = Nothing
            End If
            If Not IsNothing(oClsgloRxHubDBLayer) Then
                oClsgloRxHubDBLayer.Dispose()
                oClsgloRxHubDBLayer = Nothing
            End If

        End Try
    End Function

    Private Sub RefreshDrugListControl(ByVal RefReq_Patid As Long)

    End Sub

    Public Sub RefillRequest(ByVal _RefReqPatientId As Long, ByVal nRxTransactionID As Long, ByVal RequestedRefillQty As String, ByVal sRxReferenceNumber As String, ByVal dtdatereceived As DateTime, ByVal sMessageId As String, ByVal NDCCode As String, Optional ByVal ProviderId As String = "", Optional ByVal PharmacyId As String = "")
        Try
            '_RxBusinessLayer.FetchPrescriptionforUpdate(Now, 1)
            '_RxBusinessLayer.getRxRefillRequest(nRxTransactionID, RequestedRefillQty, dtdatereceived, sRxReferenceNumber, sMessageId, 0, NDCCode)
            ''_RxC1Flexgrid.BindFlexgrid(_RxBusinessLayer.PrescriptionCol)

            If (IsDefaultPharmacyChanged = True) Then
                For Each f As Form In Application.OpenForms
                    If f.Name = "frmPrescription" Then
                        If (Not IsNothing(DirectCast(f, gloEMR.frmPrescription).GetCurrentPatientID)) Then
                            Dim nfrmPatId As Long = DirectCast(f, gloEMR.frmPrescription).GetCurrentPatientID
                            If (nfrmPatId = _RefReqPatientId) Then
                                _RxPatientStrip.Rx_RefreshPharmacy(_RxBusinessLayer.PrescriptionCol.Item(0).PharmacyName, _RxBusinessLayer.PrescriptionCol.Item(0).PhPhone, _RxBusinessLayer.PrescriptionCol.Item(0).PhFax, _RxBusinessLayer.PrescriptionCol.Item(0).PhAddressline1, _RxBusinessLayer.PrescriptionCol.Item(0).PhAddressline2, _RxBusinessLayer.PrescriptionCol.Item(0).PhCity, _RxBusinessLayer.PrescriptionCol.Item(0).PhState, _RxBusinessLayer.PrescriptionCol.Item(0).PhZip, True)
                            End If
                        End If
                    End If
                Next
            End If
            '_RxC1Flexgrid.RowIndex = 1

        Catch ex As Exception
            'Bug #60866: 00000589 : system displays a message and locks the RxMeds
            'on exception remove formlevel locking.
            If LockID <> 0 Then
                Delete_Lock_FormLevel(LockID, nRxModulePatientID)
            End If
            Throw ex
        End Try

    End Sub

#End Region

#Region "Prescription Rx-Businesslayer Events"
    Private Sub _RxBusinessLayer_SurescriptMessageInvalidated() Handles _RxBusinessLayer.SurescriptMessageInvalidated
        InvalidSurescriptMessage = True
    End Sub

    Private Sub _RxBusinessLayer_UpdateRx_eRxStatus1(ByVal eRxStatus As String, ByVal _eRxStatusMessage As String, ByVal eRx_DrgNDCCode As String, ByVal eRx_ItemNumber As Integer, ByVal IseRxed As Integer) Handles _RxBusinessLayer.UpdateRx_eRxStatus
        Try
            If _eRxStatusMessage <> "" Then
                _eRxStatusMessage = _eRxStatusMessage.Replace("'", "")
            End If
            _RxC1Flexgrid.SetFlexGridData_eRxStatus(eRxStatus, _eRxStatusMessage, eRx_DrgNDCCode, eRx_ItemNumber, IseRxed)



        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.eRx, gloAuditTrail.ActivityType.Modify, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

#End Region

#Region " Allergy Alert "
    Private blnMoving As Boolean = False
    Private MouseDownX As Integer
    Private MouseDownY As Integer

    Private Function LoadAllergies() As List(Of String)
        Dim returned As List(Of String) = Nothing

        Try
            If _RxBusinessLayer.HistoriesCol IsNot Nothing Then
                returned = _RxBusinessLayer.HistoriesCol.OfType(Of History).Where(Function(p) p.HistoryCategory = "Allergies" AndAlso String.IsNullOrWhiteSpace(p.AllergyClassID) = False AndAlso p.AllergyClassID <> "0").Select(Function(a) a.AllergyClassID).ToList()
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try

        Return returned
    End Function

    Private Sub GetAllergies()
        Try
            Dim strallergies As String

            strallergies = _RxBusinessLayer.GetHistory_CategoryWise()

            patientAllergicMeds = New List(Of String)
            patientAllergicMeds = Me.LoadAllergies()

            lblAlert1.Text = strallergies

            If pnlPDMP.Visible = False Then
                pnlAllergiesAlerts.Height = 104
            End If

            If _RxBusinessLayer.HistoriesCol.Count > 0 Then
                lblAlert1.Height = _RxBusinessLayer.HistoriesCol.Count * 15
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub pnlAllergiesAlerts_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pnlAllergiesAlerts.MouseDown
        Me.Cursor = Cursors.SizeAll
        If e.Button = MouseButtons.Left Then
            blnMoving = True
            MouseDownX = e.X
            MouseDownY = e.Y
        End If
    End Sub

    Private Sub pnlAllergiesAlerts_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pnlAllergiesAlerts.MouseUp
        If e.Button = MouseButtons.Left Then
            blnMoving = False
        End If
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub pnlAllergiesAlerts_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pnlAllergiesAlerts.MouseMove
        If blnMoving Then
            With pnlAllergiesAlerts
                Dim temp As Point = New Point
                temp.X = .Location.X + (e.X - MouseDownX)
                temp.Y = .Location.Y + (e.Y - MouseDownY)
                .Location = temp
                .BringToFront()
            End With
        End If
    End Sub

    Private Sub lblAlert_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lblAlert1.MouseDown
        pnlAllergiesAlerts_MouseDown(sender, e)
    End Sub

    Private Sub lblAlert_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lblAlert1.MouseUp
        pnlAllergiesAlerts_MouseUp(sender, e)
    End Sub

    Private Sub lblAlert_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lblAlert1.MouseMove
        pnlAllergiesAlerts_MouseMove(sender, e)
    End Sub

    Private Sub picTop_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles picTop.MouseDown
        pnlAllergiesAlerts_MouseDown(sender, e)
    End Sub

    Private Sub picTop_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles picTop.MouseUp
        pnlAllergiesAlerts_MouseUp(sender, e)
    End Sub

    Private Sub picTop_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles picTop.MouseMove
        pnlAllergiesAlerts_MouseMove(sender, e)
    End Sub

    Private Sub picMiddle_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles picMiddle.MouseDown
        pnlAllergiesAlerts_MouseDown(sender, e)
    End Sub

    Private Sub picMiddle_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles picMiddle.MouseUp
        pnlAllergiesAlerts_MouseUp(sender, e)
    End Sub

    Private Sub picMiddle_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles picMiddle.MouseMove
        pnlAllergiesAlerts_MouseMove(sender, e)
    End Sub

    Private Sub picBottom_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles picBottom.MouseDown, lblPDMP.MouseDown
        pnlAllergiesAlerts_MouseDown(sender, e)
    End Sub

    Private Sub picBottom_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles picBottom.MouseUp, lblPDMP.MouseUp
        pnlAllergiesAlerts_MouseUp(sender, e)
    End Sub

    Private Sub picBottom_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles picBottom.MouseMove, lblPDMP.MouseMove
        pnlAllergiesAlerts_MouseMove(sender, e)
    End Sub

    Private Sub picAlertClose1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles picAlertClose1.Click
        Try
            pnlAllergiesAlerts.Visible = False
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub frmPrescription_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.F2 Then
            With pnlAllergiesAlerts
                If .Visible = False Then
                    .Visible = True
                    GetAllergies()
                    .BringToFront()
                    .BackColor = Color.LightYellow
                ElseIf .Visible = True Then
                    .Visible = False
                End If
            End With
        End If
    End Sub

    Private Sub picInfo_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles picInfo.MouseHover
        'SLR: It should not be hre, but at form load and should be freed at form close
        'Dim ToolTip1 = New System.Windows.Forms.ToolTip
        'ToolTip1.SetToolTip(Me.picInfo, "Allergies Alerts")
    End Sub

    Private Function RetrieveFAXDocumentName(Optional ByVal _Extension As String = "", Optional ByVal _Path As String = "") As String
        Try
            'Dim _dtCurrentDateTime As DateTime = System.DateTime.Now
            If _Extension = "" Then
                Dim strTIFFFileName As String = ""
                strTIFFFileName = gnClientMachineID & "-" & gloGlobal.clsFileExtensions.GetUniqueDateString("yyyyMMddHHmmssffff") 'Format(_dtCurrentDateTime, "yyyyMMddhhmmss") & _dtCurrentDateTime.Millisecond
                Return strTIFFFileName
            Else
                'Dim _NewDocumentName As [String] = ""
                'Dim i As Integer = 0
                '_NewDocumentName = _dtCurrentDateTime.ToString("MM dd yyyy - hh mm ss tt") + _Extension
                'While File.Exists(Convert.ToString(_Path) & "\" & _NewDocumentName) = True And i < Integer.MaxValue 'SLR: And i < maxint
                '    i = i + 1
                '    _NewDocumentName = _dtCurrentDateTime.ToString("MM dd yyyy - hh mm ss tt") & "-" & i.ToString() & Convert.ToString(_Extension)
                'End While
                'Return Convert.ToString(_Path) & "\" & _NewDocumentName
                Return gloGlobal.clsFileExtensions.NewDocumentName(_Path, _Extension, "MMddyyyyHHmmssffff")
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, False)
            Throw
        End Try
    End Function

#End Region

#Region "Prescription MAIN TOOLBAR User Control"

    Private Sub GloRxToolBarUserCtrl1_tblCCDClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles GloRxToolBarUserCtrl1.tStrpblCCD_Click
        Try
            Dim objfrm As New frmCCDGenerateList(nRxModulePatientID)
            objfrm.ChkMedications.Checked = True

            With objfrm
                .WindowState = FormWindowState.Normal
                .BringToFront()
                .StartPosition = FormStartPosition.CenterParent
                .ShowDialog(IIf(IsNothing(objfrm.Parent), Me, objfrm.Parent))
            End With
            objfrm.Dispose()
            objfrm = Nothing
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.ViewCCD, gloAuditTrail.ActivityType.View, "View CCD", nRxModulePatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    ''' <summary>
    ''' this event is for Save and Close button
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub GloRxToolBarUserCtrl1_tStrpSaveClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles GloRxToolBarUserCtrl1.tStrpSaveClick
        If MyBase.SetChildFormModules("GloRxToolBarUserCtrl1_tStrpSaveClick", "Save medication", MyBase.strProviderID) = True Then
            Exit Sub
        End If
        '' Property Set to control the print behaviour in Save & Close
        '' Bug #65840: gloEMR - Rx-Meds - While printing refill request it print twice from print button. 
        RxCommand = RxCommands.SaveNClose
        RxOTCActions = OTCDrugAction.None
        isOverrideAlertOccured = False
        isbuttonClickSave = True
        Try
            Cursor.Current = Cursors.WaitCursor
            RaiseEvent PerformDrugAlertCheck(False)

            If IsOverrideReason = True Then
                IsOverrideReason = False
                Exit Sub
            End If

            _isSaveClicked = True 'SET THE FLAG WHEN SAVE & CLOSE BUTTON IS CLICKED 
            Dim nProviderID As Long = IIf(gnLoginProviderID <> 0, gnLoginProviderID, nPatientProviderId)
            If Not getProviderTaxID(nProviderID) Then
                Exit Sub
            End If

            If (IsNothing(_RxBusinessLayer) = False) AndAlso IsNothing(_RxBusinessLayer.PrescriptionCol) = False AndAlso (_RxBusinessLayer.PrescriptionCol.Count > 0) Then
                If ErxMultipleDrugs() = False Then
                    Exit Sub
                End If
                If gblnSurescriptEnabled Then
                    If InvalidSurescriptMessage = False Then
                        If _ismsgdisplay = False Then
                            If _isSupervisormessagedisplay = True Then
                                _isSupervisormessagedisplay = False
                                Exit Sub
                            End If
                            If (IsNothing(_MedBusinessLayer) = False) AndAlso IsNothing(_MedBusinessLayer.MedicationCol) = False AndAlso (_MedBusinessLayer.MedicationCol.Count > 0) Then
                                For Each item As Medication In _MedBusinessLayer.MedicationCol
                                    If item.MedicationID <> 0 Then
                                        Dim oclsselectProviderTaxID As New gloGlobal.TIN.clsSelectProviderTaxID(nProviderID)
                                        oclsselectProviderTaxID.InsertProviderTaxID(nProviderAssociationID, item.MedicationID, sProviderTaxID, nProviderID, gnLoginProviderID, gloGlobal.TIN.clsSelectProviderTaxID.TransactionType.Medication.GetHashCode())
                                        oclsselectProviderTaxID = Nothing
                                    End If
                                Next
                            End If
                            For Each item As Prescription In _RxBusinessLayer.PrescriptionCol

                                If item.PrescriptionID <> 0 Then
                                    Dim oclsselectProviderTaxID As New gloGlobal.TIN.clsSelectProviderTaxID(nProviderID)
                                    oclsselectProviderTaxID.InsertProviderTaxID(nProviderAssociationID, item.PrescriptionID, sProviderTaxID, nProviderID, gnLoginProviderID, gloGlobal.TIN.clsSelectProviderTaxID.TransactionType.Prescription.GetHashCode())
                                    oclsselectProviderTaxID = Nothing
                                End If
                            Next

                            If (IsNothing(gloEMRGeneralLibrary.gloGeneral.clsgeneral.ds) = False) AndAlso (gloEMRGeneralLibrary.gloGeneral.clsgeneral.ds.Tables.Contains("MedicationReconcillation")) AndAlso (gloEMRGeneralLibrary.gloGeneral.clsgeneral.ds.Tables("MedicationReconcillation").Rows.Count > 0) Then
                                Dim MedicalReconcilationId As Long = GetMedicalReconcillationID(_RxBusinessLayer.CurrentVisitID)
                                Dim oclsselectProviderTaxID As New gloGlobal.TIN.clsSelectProviderTaxID(nProviderID)
                                oclsselectProviderTaxID.InsertProviderTaxID(nProviderAssociationID, MedicalReconcilationId, sProviderTaxID, nProviderID, gnLoginProviderID, gloGlobal.TIN.clsSelectProviderTaxID.TransactionType.ManualReconciliationMedication.GetHashCode())
                                oclsselectProviderTaxID = Nothing
                            End If
                            Me.Close()
                        End If
                    End If
                Else
                    If _ismsgdisplay = False Then
                        If (IsNothing(_MedBusinessLayer) = False) AndAlso IsNothing(_MedBusinessLayer.MedicationCol) = False AndAlso (_MedBusinessLayer.MedicationCol.Count > 0) Then
                            For Each item As Medication In _MedBusinessLayer.MedicationCol
                                If item.MedicationID <> 0 Then
                                    Dim oclsselectProviderTaxID As New gloGlobal.TIN.clsSelectProviderTaxID(nProviderID)
                                    oclsselectProviderTaxID.InsertProviderTaxID(nProviderAssociationID, item.MedicationID, sProviderTaxID, nProviderID, gnLoginProviderID, gloGlobal.TIN.clsSelectProviderTaxID.TransactionType.Medication.GetHashCode())
                                    oclsselectProviderTaxID = Nothing
                                End If
                            Next
                        End If
                        For Each item As Prescription In _RxBusinessLayer.PrescriptionCol

                            If item.PrescriptionID <> 0 Then
                                Dim oclsselectProviderTaxID As New gloGlobal.TIN.clsSelectProviderTaxID(nProviderID)
                                oclsselectProviderTaxID.InsertProviderTaxID(nProviderAssociationID, item.PrescriptionID, sProviderTaxID, nProviderID, gnLoginProviderID, gloGlobal.TIN.clsSelectProviderTaxID.TransactionType.Prescription.GetHashCode())
                                oclsselectProviderTaxID = Nothing
                            End If
                        Next
                        If (IsNothing(gloEMRGeneralLibrary.gloGeneral.clsgeneral.ds) = False) AndAlso (gloEMRGeneralLibrary.gloGeneral.clsgeneral.ds.Tables.Contains("MedicationReconcillation")) AndAlso (gloEMRGeneralLibrary.gloGeneral.clsgeneral.ds.Tables("MedicationReconcillation").Rows.Count > 0) Then
                            Dim MedicalReconcilationId As Long = GetMedicalReconcillationID(_MedBusinessLayer.CurrentVisitID)
                            Dim oclsselectProviderTaxID As New gloGlobal.TIN.clsSelectProviderTaxID(nProviderID)
                            oclsselectProviderTaxID.InsertProviderTaxID(nProviderAssociationID, MedicalReconcilationId, sProviderTaxID, nProviderID, gnLoginProviderID, gloGlobal.TIN.clsSelectProviderTaxID.TransactionType.ManualReconciliationMedication.GetHashCode())
                            oclsselectProviderTaxID = Nothing
                        End If
                        Me.Close()
                    End If
                End If
            Else
                SaveMeds() 'used to save only and medication
                If _ismsgdisplay = False Then
                    If _isSupervisormessagedisplay = True Then
                        _isSupervisormessagedisplay = False
                        Exit Sub
                    End If
                    If (IsNothing(_MedBusinessLayer) = False) AndAlso IsNothing(_MedBusinessLayer.MedicationCol) = False AndAlso (_MedBusinessLayer.MedicationCol.Count > 0) Then
                        For Each item As Medication In _MedBusinessLayer.MedicationCol
                            If item.MedicationID <> 0 Then
                                Dim oclsselectProviderTaxID As New gloGlobal.TIN.clsSelectProviderTaxID(nProviderID)
                                oclsselectProviderTaxID.InsertProviderTaxID(nProviderAssociationID, item.MedicationID, sProviderTaxID, nProviderID, gnLoginProviderID, gloGlobal.TIN.clsSelectProviderTaxID.TransactionType.Medication.GetHashCode())
                                oclsselectProviderTaxID = Nothing
                            End If
                        Next
                    End If
                    If (IsNothing(_RxBusinessLayer) = False) AndAlso IsNothing(_RxBusinessLayer.PrescriptionCol) = False AndAlso (_RxBusinessLayer.PrescriptionCol.Count > 0) Then
                        For Each item As Prescription In _RxBusinessLayer.PrescriptionCol

                            If item.PrescriptionID <> 0 Then
                                Dim oclsselectProviderTaxID As New gloGlobal.TIN.clsSelectProviderTaxID(nProviderID)
                                oclsselectProviderTaxID.InsertProviderTaxID(nProviderAssociationID, item.PrescriptionID, sProviderTaxID, nProviderID, gnLoginProviderID, gloGlobal.TIN.clsSelectProviderTaxID.TransactionType.Prescription.GetHashCode())
                                oclsselectProviderTaxID = Nothing
                            End If
                        Next
                    End If


                    If (IsNothing(gloEMRGeneralLibrary.gloGeneral.clsgeneral.ds) = False) AndAlso (gloEMRGeneralLibrary.gloGeneral.clsgeneral.ds.Tables.Contains("MedicationReconcillation")) AndAlso (gloEMRGeneralLibrary.gloGeneral.clsgeneral.ds.Tables("MedicationReconcillation").Rows.Count > 0) Then
                        Dim MedicalReconcilationId As Long = GetMedicalReconcillationID(_MedBusinessLayer.CurrentVisitID)
                        Dim oclsselectProviderTaxID As New gloGlobal.TIN.clsSelectProviderTaxID(nProviderID)
                        oclsselectProviderTaxID.InsertProviderTaxID(nProviderAssociationID, MedicalReconcilationId, sProviderTaxID, nProviderID, gnLoginProviderID, gloGlobal.TIN.clsSelectProviderTaxID.TransactionType.ManualReconciliationMedication.GetHashCode())
                        oclsselectProviderTaxID = Nothing
                    End If
                    Me.Close()
                End If
            End If

            If Not arrDrugs Is Nothing Then
                If arrDrugs.Count > 0 Then
                    For i As Integer = 0 To arrDrugs.Count - 1
                        Dim lst As myList 'SLR: new is not needed
                        lst = CType(arrDrugs(i), myList)
                        For j As Integer = 0 To _RxBusinessLayer.PrescriptionCol.Count - 1
                            If lst.ID = _RxBusinessLayer.PrescriptionCol.Item(j).DrugID Then
                                lst.IsFinished = True
                                Exit For
                            End If
                        Next
                    Next
                End If
            End If


            Cursor.Current = Cursors.Default
        Catch ex As Exception
            Cursor.Current = Cursors.Default
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally
            isbuttonClickSave = False
            InvalidSurescriptMessage = False ''''reset the value to false. this was made true to keep stay on the Rx form when we try to approve a narcotic drug. savePrescription() result type returned = 2
            gloEMRGeneralLibrary.gloGeneral.clsgeneral.blnRxDeleteFlag = False
            gloEMRGeneralLibrary.gloGeneral.clsgeneral.gblnCustomPrescEdited = False '''''for cchit11 audit log
            gloEMRGeneralLibrary.gloGeneral.clsgeneral.gblnCustomMediEdited = False '''''for cchit11 audit log
        End Try
    End Sub

    ''' <summary>
    ''' this event is for Save button
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub GloRxToolBarUserCtrl1_tStrpSaveRxMedClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles GloRxToolBarUserCtrl1.tStrpSaveRxMedClick

        '' Property Set to control the print behaviour in Save & Close
        '' Bug #65840: gloEMR - Rx-Meds - While printing refill request it print twice from print button. 
        RxCommand = RxCommands.Save
        RxOTCActions = OTCDrugAction.None
        isbuttonClickSave = True
        Try
            Cursor.Current = Cursors.WaitCursor
            RaiseEvent PerformDrugAlertCheck(False)
            If IsOverrideReason = True Then
                IsOverrideReason = False
                Exit Sub
            End If
            If _MxC1Flexgrid.txtSearch.Text <> "" Then
                _MxC1Flexgrid.txtSearch.Text = ""
            End If
            Dim nProviderID As Long = IIf(gnLoginProviderID <> 0, gnLoginProviderID, nPatientProviderId)
            If Not getProviderTaxID(nProviderID) Then
                Exit Sub
            End If
            If _RxBusinessLayer.PrescriptionCol.Count > 0 Then
                If IsRefRequest OrElse IsChangeRequest Then
                    ErxMultipleDrugs()
                Else
                    SaveRxMeds(True, True) 'used to save both prescription and Medication
                End If
                If gblnSurescriptEnabled Then
                    If InvalidSurescriptMessage = False Then
                    End If
                Else

                End If
            Else
                SaveMeds(True) 'used to save only and medication
            End If
            For Each item As Medication In _MedBusinessLayer.MedicationCol
                If item.MedicationID <> 0 Then
                    Dim oclsselectProviderTaxID As New gloGlobal.TIN.clsSelectProviderTaxID(nProviderID)
                    oclsselectProviderTaxID.InsertProviderTaxID(nProviderAssociationID, item.MedicationID, sProviderTaxID, nProviderID, gnLoginProviderID, gloGlobal.TIN.clsSelectProviderTaxID.TransactionType.Medication.GetHashCode())
                    oclsselectProviderTaxID = Nothing
                End If
            Next
            For Each item As Prescription In _RxBusinessLayer.PrescriptionCol

                If item.PrescriptionID <> 0 Then
                    Dim oclsselectProviderTaxID As New gloGlobal.TIN.clsSelectProviderTaxID(nProviderID)
                    oclsselectProviderTaxID.InsertProviderTaxID(nProviderAssociationID, item.PrescriptionID, sProviderTaxID, nProviderID, gnLoginProviderID, gloGlobal.TIN.clsSelectProviderTaxID.TransactionType.Prescription.GetHashCode())
                    oclsselectProviderTaxID = Nothing
                End If
            Next


            If gloEMRGeneralLibrary.gloGeneral.clsgeneral.ds.Tables("MedicationReconcillation").Rows.Count > 0 Then
                Dim MedicalReconcilationId As Long = 0
                If _RxBusinessLayer.CurrentVisitID <> 0 Then
                    MedicalReconcilationId = GetMedicalReconcillationID(_MedBusinessLayer.CurrentVisitID)
                ElseIf _MedBusinessLayer.CurrentVisitID <> 0 Then
                    MedicalReconcilationId = GetMedicalReconcillationID(_MedBusinessLayer.CurrentVisitID)
                End If
                Dim oclsselectProviderTaxID As New gloGlobal.TIN.clsSelectProviderTaxID(nProviderID)
                oclsselectProviderTaxID.InsertProviderTaxID(nProviderAssociationID, MedicalReconcilationId, sProviderTaxID, nProviderID, gnLoginProviderID, gloGlobal.TIN.clsSelectProviderTaxID.TransactionType.ManualReconciliationMedication.GetHashCode())
                oclsselectProviderTaxID = Nothing
            End If

            If Not arrDrugs Is Nothing Then
                If arrDrugs.Count > 0 Then
                    For i As Integer = 0 To arrDrugs.Count - 1
                        Dim lst As myList 'SLR: new is not needed
                        lst = CType(arrDrugs(i), myList)
                        For j As Integer = 0 To _RxBusinessLayer.PrescriptionCol.Count - 1
                            If lst.ID = _RxBusinessLayer.PrescriptionCol.Item(j).DrugID Then
                                lst.IsFinished = True
                                Exit For
                            End If
                        Next
                    Next
                End If
            End If


            Cursor.Current = Cursors.Default
        Catch ex As Exception
            Cursor.Current = Cursors.Default
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally
            isbuttonClickSave = False
            gloEMRGeneralLibrary.gloGeneral.clsgeneral.blnRxDeleteFlag = False
            gloEMRGeneralLibrary.gloGeneral.clsgeneral.blnIsRxMedsFormEdited = False ''''''''Do not prompt the user with "Do you want to Save changes?" message on close button, if user clicks on Save button directly.
        End Try
    End Sub

    Public Sub SaveRxMeds(Optional ByVal _IsReloadGridFlag As Boolean = False, Optional ByVal _IsCheckStateRefresh As Boolean = False)

        Try
            If MyBase.SetChildFormModules("SaveRxMeds", "Save medication", MyBase.strProviderID) = True Then
                Exit Sub
            End If
            ''code added to fix case GLO2011-0015029 (prescriptionID=0 and Rowstate=m) 
            If Me.pnlRefill.Controls.Contains(objCustomPrescription) = True Then
                RemoveControl()
            End If
            If Me.pnlRefill.Controls.Contains(objCustomMedication) = True Then
                RemoveCustomMedicationControl()
            End If
            If RxCommand <> RxCommands.IssueRx AndAlso isUnspecifiedMessageOccured = False Then
                If CheckUnspecified() = False Then
                    ToAttempteRx = True
                    Exit Sub
                End If
            End If
            If isbuttonClickSave = True Then
                If isUnIssuedAlertOccured = False Then
                    If CheckUnIssuedDrug() = False Then
                        ToAttempteRx = True
                        isUnIssuedAlertOccured = False
                        Exit Sub
                    End If
                End If
            Else
                If blnRxC1FlexClick = False Then
                    If _MedBusinessLayer.MedicationCol.Count = 0 Then
                        Exit Sub
                    End If
                End If
                If RxOTCActions = OTCDrugAction.None Then
                    If CheckOTCDrug(RxCommand.ToString()) = True Then
                        Exit Sub
                    End If
                End If
            End If

            ''end code
            If ValidateSupervisingProvider() = False Then
                Exit Sub
            End If
            If isEndDuedateMessageOccured = False Then
                If CheckMedicationEndDate() Then
                    Exit Sub
                End If
            End If
            If CheckDrugStatus() = True Then
                Exit Sub
            End If

            _RxBusinessLayer.LoadSchema()

            Dim _nRefReqvalue As Integer = 0 'variable used when refill request denied with new Rx to follow 
            _nRefReqvalue = SavePrescription() 'fill prescription collection in dataset

            'it will return 4 or 5 in  denied with new Rx to follow case else 
            SaveMedication()   ''fill Medication collection in dataset

            '' Supervising Code moved after saving rxmeds
            'IF Junior Provider Then only save Supervising Provider
            If blnIsJuniorProvider = True Then
                SaveSupervisingProvider()
            End If

            SaveMedicationReconcillation()

            'Incident #55315: 00016572 : Carry forward issue
            Dim result As DialogResult
            Dim bresult As Boolean = False
            If IsPastVisit And (_MedBusinessLayer._MostRecentVisitID_Mx > 0) Then
                result = MessageBox.Show("There are subsequent visit(s) available for this patient. Do you want to add/update the new entries to subsequent visits?" + vbNewLine + "Yes - Add/update to subsequent visits" + vbNewLine + "No - Do not add/update to subsequent visits", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                If result = vbYes Then
                    bresult = True
                End If
            End If
            'If gloEMRGeneralLibrary.gloGeneral.clsgeneral.ds.Tables("Prescription").Rows.Count > 0 Or gloEMRGeneralLibrary.gloGeneral.clsgeneral.ds.Tables("Medication").Rows.Count > 0 Or gloEMRGeneralLibrary.gloGeneral.clsgeneral.ds.Tables("PrescriptionProvider").Rows.Count > 0 Then
            _RxBusinessLayer.SaveRx_TVP(bresult, IsPastVisit, nRxModulePatientID)
            'gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.Save, "Save Prescription", nRxModulePatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            'using dataset and TVP save prescription and Medication
            'code added to save display settings for split control
            If (IsNothing(clsSplit_PatientPrescription) = False) Then
                If uiPanSplitScreen_PatientPrescription.SelectedPanel.Name <> "uiPanPatientExam" And uiPanSplitScreen_PatientPrescription.SelectedPanel.Name <> "uiPanNurseNotes" And uiPanSplitScreen_PatientPrescription.SelectedPanel.Name <> "uiPanPatientMessages" And uiPanSplitScreen_PatientPrescription.SelectedPanel.Name <> "uiPanPatientLetters" And uiPanSplitScreen_PatientPrescription.SelectedPanel.Name <> "uiPanOrders" Then
                    clsSplit_PatientPrescription.loadSplitControlData(nRxModulePatientID, _RxBusinessLayer.CurrentVisitID, uiPanSplitScreen_PatientPrescription.SelectedPanel.Name, objCriteria, ObjWord, gnClinicID)
                End If
            End If

            'GLO2011-0014554 Error Recieved With Controlled Substance
            If _nRefReqvalue = 4 Or _nRefReqvalue = 5 Then
                If _IsReloadGridFlag = False Then
                    _IsReloadGridFlag = True
                End If
            End If

            If _IsReloadGridFlag = True Then
                _RxBusinessLayer.FetchPrescriptionforUpdate(_RxBusinessLayer.PrescriptionDate, 1, nRxRequestTransactionID)
                '' Execute the following block to print the prescription only in case of Narcotic drugs, refill request 
                '' Bug #65840: gloEMR - Rx-Meds - While printing refill request it print twice from print button. 
                If RxCommand = RxCommands.SaveNClose Or RxCommand = RxCommands.eRx Or RxCommand = RxCommands.IssueRx Then
                    'GLO2011-0014554 Error Recieved With Controlled Substance
                    If _nRefReqvalue = 4 Then 'when only one item is their in prescription collection
                        If IsRefRequest Then
                            PrintRxReport_New(CType(_RxBusinessLayer.PrescriptionCol.Item(0).PrescriptionID, String), False, _RxBusinessLayer.PrescriptionDate, , RefRequest.ReferenceNumber)
                        Else
                            PrintRxReport_New(CType(_RxBusinessLayer.PrescriptionCol.Item(0).PrescriptionID, String), False, _RxBusinessLayer.PrescriptionDate)
                        End If

                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.PrintPrescription, gloAuditTrail.ActivityType.Print, "Prescription printed", nRxModulePatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                        ToAttempteRx = True
                    ElseIf _nRefReqvalue = 5 Then 'when more than one item is in their prescription collection
                        If _RxBusinessLayer.PrescriptionCol.Count > 0 Then
                            For i As Integer = 0 To _RxBusinessLayer.PrescriptionCol.Count - 1
                                If IsRefRequest Then
                                    If _RxBusinessLayer.PrescriptionCol.Item(i).NDCCode = RefRequest.NDCCode And _RxBusinessLayer.PrescriptionCol.Item(i).Method = "Print" And _RxBusinessLayer.PrescriptionCol.Item(i).MessageType = "RefillRequest" Then
                                        If _RxBusinessLayer.TmpCheckStatesCol.Item(i).CheckState = True Then
                                            PrintRxReport_New(CType(_RxBusinessLayer.PrescriptionCol.Item(i).PrescriptionID, String), False, _RxBusinessLayer.PrescriptionDate, , RefRequest.ReferenceNumber)
                                            _RxBusinessLayer.TmpCheckStatesCol.Item(i).CheckState = False
                                        End If
                                    End If
                                End If
                            Next
                        End If

                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.PrintPrescription, gloAuditTrail.ActivityType.Print, "Prescription printed", nRxModulePatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                    End If
                End If

                BindFlexGrid_RX(Not _IsCheckStateRefresh)

                If RxCommand <> RxCommands.eRx Then 'AndAlso RxCommand <> RxCommands.IssueRx
                    _MedBusinessLayer.FetchMedicationforUpdate(False)
                End If
            End If

            If IsfrmPatientSavings Then
                UpdatePatientSavings(arrDrugs)
            End If

            RaiseEvent PerformEPAStatusCheck(Nothing)
            'Me.PerformPDRStatusCheck(Nothing)

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString, False)
            ex = Nothing
        End Try
    End Sub

    Private Function UpdatePatientSavings(ByVal _arrdrugs As ArrayList) As Boolean
        Dim oDB As gloStream.gloDataBase.gloDataBase = Nothing
        Try
            If Not IsNothing(_arrdrugs) Then
                Dim strAltIDs As String = String.Empty
                Dim strIDs As String = String.Empty
                For i As Integer = 0 To _arrdrugs.Count - 1
                    If i = 0 Then
                        strIDs = "'" & DirectCast(_arrdrugs(i), Drug).OpportunityID.ToString() & "'"
                        strAltIDs = "'" & DirectCast(_arrdrugs(i), Drug).AlternateID.ToString() & "'"
                    Else
                        strIDs = strIDs & ",'" & DirectCast(_arrdrugs(i), Drug).OpportunityID.ToString() & "'"
                        strAltIDs = strAltIDs & ",'" & DirectCast(_arrdrugs(i), Drug).AlternateID.ToString() & "'"
                    End If
                Next
                If strIDs.Trim <> "" Then
                    oDB = New gloStream.gloDataBase.gloDataBase
                    oDB.Connect(GetConnectionString)
                    oDB.ExecuteNonSQLQuery("UPDATE PatSavOpportunity_Mst SET IsEPrecribe = 1, DispositionCode='01',DispositionDate='" & DateTime.Now.Date & "'  WHERE PSOppID in (" & strIDs & ")")
                End If
                If strAltIDs.Trim <> "" Then
                    If (IsNothing(oDB)) Then
                        oDB = New gloStream.gloDataBase.gloDataBase
                        oDB.Connect(GetConnectionString)
                    End If
                    oDB.ExecuteNonSQLQuery("UPDATE PatSavOpportunityAlternativeMed_Dtl SET bIsSelected = 1 WHERE PSOppAltMedID in (" & strAltIDs & ")")
                    oDB.Disconnect()
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally
            If Not IsNothing(oDB) Then
                oDB.Disconnect()
                oDB.Dispose()
                oDB = Nothing
            End If
        End Try
        Return Nothing
    End Function

    Public Sub SaveMeds(Optional ByVal _IsReloadGridFlag As Boolean = False)
        Try
            If MyBase.SetChildFormModules("SaveMeds", "Save medication", MyBase.strProviderID) = True Then
                Exit Sub
            End If
            ''code added to fix case GLO2011-0015029 (prescriptionID=0 and Rowstate=m) 
            If Me.pnlRefill.Controls.Contains(objCustomPrescription) = True Then
                RemoveControl()
            End If
            If Me.pnlRefill.Controls.Contains(objCustomMedication) = True Then
                RemoveCustomMedicationControl()
            End If
            If ValidateSupervisingProvider() = False Then
                Exit Sub
            End If
            ''end code
            If CheckMedicationEndDate() Then
                Exit Sub
            End If
            If CheckDrugStatus() Then
                Exit Sub
            End If
            _RxBusinessLayer.LoadSchema()


            SaveMedication()   ''fill Medication collection in dataset

            '' Supervising Code moved after saving rxmeds
            'IF Junior Provider Then only save Supervising Provider
            If blnIsJuniorProvider = True Then
                SaveSupervisingProvider()
            End If

            SaveMedicationReconcillation()

            'Incident #55315: 00016572 : Carry forward issue
            Dim result As DialogResult
            Dim bresult As Boolean = False
            If IsPastVisit And (_MedBusinessLayer._MostRecentVisitID_Mx > 0) Then
                result = MessageBox.Show("There are subsequent visit(s) available for this patient. Do you want to add/update the new entries to subsequent visits?" + vbNewLine + "Yes - Add/update to subsequent visits" + vbNewLine + "No - Do not add/update to subsequent visits", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                If result = vbYes Then
                    bresult = True
                End If
            End If
            'If gloEMRGeneralLibrary.gloGeneral.clsgeneral.ds.Tables("Prescription").Rows.Count > 0 Or gloEMRGeneralLibrary.gloGeneral.clsgeneral.ds.Tables("Medication").Rows.Count > 0 Or gloEMRGeneralLibrary.gloGeneral.clsgeneral.ds.Tables("PrescriptionProvider").Rows.Count > 0 Then
            _RxBusinessLayer.SaveRx_TVP(bresult, IsPastVisit, nRxModulePatientID) 'using dataset and TVP save prescription and Medication
            'gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreateMedication, gloAuditTrail.ActivityType.Save, "Save Medication", nRxModulePatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            '_IsReloadGridFlag used to check whether to reload the grid or not 
            'if true-reload grid 
            If (IsNothing(clsSplit_PatientPrescription) = False) Then
                clsSplit_PatientPrescription.loadSplitControlData(nRxModulePatientID, _RxBusinessLayer.CurrentVisitID, uiPanSplitScreen_PatientPrescription.SelectedPanel.Name, objCriteria, ObjWord, gnClinicID)
            End If

            'End If
            If IsPastVisit And (_MedBusinessLayer._MostRecentVisitID_Mx > 0) Then
                _MedBusinessLayer.CurrentVisitID = _RxBusinessLayer.CurrentVisitID
            End If

            If _IsReloadGridFlag = True Then
                _MedBusinessLayer.FetchMedicationforUpdate(False)
                '_MxC1Flexgrid.BindFlexgrid(True)
                'If _MedBusinessLayer.MedicationCol.Count > 0 Then
                '    _MxC1Flexgrid.BindFlexgrid(True)
                'Else
                '    _MxC1Flexgrid.cmbMedStatus.Enabled = False
                'End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString, False)
            ex = Nothing
        End Try
    End Sub

    Private Sub GloRxToolBarUserCtrl1_tStrpShowHideClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles GloRxToolBarUserCtrl1.tStrpShowHideClick
        GloRxToolBarUserCtrl1.tStrpShowHide.Width = 46
        GloRxToolBarUserCtrl1.tStrpShowHide.AutoSize = False
        Try
            If pnlRight.Visible = False Then
                _MxHistoryUserCtrl.RefreshMedicationHistory()
                _RxHistoryUserCtrl.RefreshPrescriptionHistory()
                GloRxToolBarUserCtrl1.tStrpShowHide.Image = gloEMR.My.Resources.Hide
                GloRxToolBarUserCtrl1.tStrpShowHide.Text = "Hide"
                GloRxToolBarUserCtrl1.tStrpShowHide.ToolTipText = "Hide Prescription History"
                pnlRight.Visible = True
                splRight.Visible = True
            Else
                GloRxToolBarUserCtrl1.tStrpShowHide.Image = gloEMR.My.Resources.Show
                GloRxToolBarUserCtrl1.tStrpShowHide.Text = "Show"
                GloRxToolBarUserCtrl1.tStrpShowHide.ToolTipText = "Show Prescription History"
                pnlRight.Visible = False
                splRight.Visible = False
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            'UpdateLog(ex.Message & ":" & ex.Source)
            ex = Nothing
        End Try
    End Sub

    Private Sub GloRxToolBarUserCtrl1_tStrpDenyButtonClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles GloRxToolBarUserCtrl1.tStrpDenyButtonClick

    End Sub

    Private Sub GloRxToolBarUserCtrl1_tStrpRefillReqButtonClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles GloRxToolBarUserCtrl1.tStrpRefillReqButtonClick


    End Sub

    Private Sub GloRxToolBarUserCtrl1_tStrpPrvRxClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles GloRxToolBarUserCtrl1.tStrpPrvRxClick

        If IsFormLocked Then
            Exit Sub
        End If
        'Developer: Pradeep()
        'Date:12/22/2011
        'Bug ID/PRD Name/Salesforce Case: 17290
        'Reason:shared variable replaced by private
        'If gloEMRGeneralLibrary.glogeneral.clsgeneral.blnRxC1FlexClick = True Then
        If blnRxC1FlexClick = True Then
            Try

                If Me.pnlRefill.Controls.Contains(objCustomPrescription) = True Then
                    RemoveControl()
                    RemoveDrugInfoControl()
                End If

                AddRefillControl()

                sptRefill.Enabled = True
                pnlRefill.Visible = True

                GloRxToolBarUserCtrl1.tStrpShowHide.Text = "Show"
                GloRxToolBarUserCtrl1.tStrpShowHide.ToolTipText = "Show Prescription History"

            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                ex = Nothing
            End Try
        Else
            Try

                If Not IsNothing(objCustomMedication) Then
                    RemoveMedRefillControl()
                End If
                AddMedRefillControl()

                sptRefill.Enabled = True
                pnlRefill.Visible = True

                GloRxToolBarUserCtrl1.tStrpShowHide.Text = "Show"
                GloRxToolBarUserCtrl1.tStrpShowHide.ToolTipText = "Show Prescription History"

            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.CreateMedication, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                ex = Nothing
            End Try
        End If

    End Sub

    Private Sub AddMedRefillControl()
        Try

            If Not IsNothing(_MedRefillC1FlexGridUserCtrl) Then
                RemoveRefillControl() 'SLR: Chcek I think it should be RemoveMedRefillControl
            End If
            splRight.Visible = False
            pnlRight.Visible = False
            'SLR: IF above line is RemoeRefilControl, then Call RemoveMedRefillControl to dispose and then allocate
            If Not IsNothing(_MedRefillC1FlexGridUserCtrl) Then
                _MedRefillC1FlexGridUserCtrl.Dispose()
                _MedRefillC1FlexGridUserCtrl = Nothing
            End If
            _MedRefillC1FlexGridUserCtrl = New gloMedRefillC1FlexGridUserCtrl(_MedBusinessLayer)

            If _DisableControls = True Then ''disable the make current logic as per medication carry forward case in 7031
                _MedRefillC1FlexGridUserCtrl.MakeCurrentDisable = True
            End If

            Me.pnlRefill.Controls.Add(_MedRefillC1FlexGridUserCtrl)



            _MedRefillC1FlexGridUserCtrl.Dock = DockStyle.Fill
            _MedRefillC1FlexGridUserCtrl.Visible = True
            _MedRefillC1FlexGridUserCtrl.BringToFront()
            pnlRefill.Visible = True

            ''''''Code to set the splitters
            If CheckRefillPanelhavingAnyCTRL() = True Then
                sptRefill.Enabled = True
                pnlRefill.Visible = True

            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub RemoveMedRefillControl()
        If Not IsNothing(_MedRefillC1FlexGridUserCtrl) Then
            Me.pnlRefill.Controls.Remove(_MedRefillC1FlexGridUserCtrl)
            _MedRefillC1FlexGridUserCtrl.Visible = False
            _MedRefillC1FlexGridUserCtrl.Dispose()
            _MedRefillC1FlexGridUserCtrl = Nothing

            pnlRefill.Visible = False
        End If
    End Sub

    Private Sub GloRxToolBarUserCtrl1_tStrpPrintClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles GloRxToolBarUserCtrl1.tStrpPrintClick

        RxCommand = RxCommands.Print
        RxOTCActions = OTCDrugAction.None

        isEndDuedateMessageOccured = False

        RaiseEvent PerformDrugAlertCheck(False)

        If IsOverrideReason = True Then
            IsOverrideReason = False
            Exit Sub
        End If
        isUnspecifiedMessageOccured = False
        If blnRxC1FlexClick = True Then
            Try
                Cursor.Current = Cursors.WaitCursor
                _RxBusinessLayer.EpcsGoldUrl = gstrEpcsUrl
                _RxBusinessLayer.VendorName = gstrVendorName 'Convert.ToString(appSettings("VendorName"))
                _RxBusinessLayer.VendorLabel = gstrVendorLabel 'Convert.ToString(appSettings("VendorLabel"))
                _RxBusinessLayer.VendorNodeName = gstrVendorNodeName 'Convert.ToString(appSettings("VendorNodeName"))
                _RxBusinessLayer.VendorNodeLabel = gstrVendorNodeLabel 'Convert.ToString(appSettings("VendorNodeLabel"))
                _RxBusinessLayer.SharedSecretKey = gstrSharedSecret
                _RxBusinessLayer.ApplicationVersion = System.Windows.Forms.Application.ProductVersion

                If _RxBusinessLayer.PrescriptionCol.Count > 0 Then
                    SetMethod("Print") 'since the Print button was clicked so the method type will be "Print"
                    _RxC1Flexgrid.InsertCheckState()
                    SaveRxMeds(True) 'used to save both prescription and Medication
                    Call PrintRx()
                End If
                Cursor.Current = Cursors.Default
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.Print, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                ex = Nothing
                Cursor.Current = Cursors.Default
            End Try

        Else
            Try
                Cursor.Current = Cursors.WaitCursor
                SaveRxMeds(True, True) 'used to save both prescription and Medication
                Callprint(True)

                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.PrintMedication, gloAuditTrail.ActivityType.Print, "Medication Printed", nRxModulePatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)

                Cursor.Current = Cursors.Default
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.Print, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                ex = Nothing
                Cursor.Current = Cursors.Default
            End Try
        End If

    End Sub

    Private Sub GloRxToolBarUserCtrl1_tStrpCloseClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles GloRxToolBarUserCtrl1.tStrpCloseClick
        RxOTCActions = OTCDrugAction.None
        Try
            If (_ismsgdisplay = False) Then
                Me.Close()
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            gloEMRGeneralLibrary.gloGeneral.clsgeneral.blnIsRxMedsFormEdited = False ''''''before closing the Rx-Meds form set the var to false again
            gloEMRGeneralLibrary.gloGeneral.clsgeneral.blnRxDeleteFlag = False
            gloEMRGeneralLibrary.gloGeneral.clsgeneral.gblnCustomPrescEdited = False ''''for CCHIT11 AuditLog
            gloEMRGeneralLibrary.gloGeneral.clsgeneral.gblnCustomMediEdited = False ''''for CCHIT11 AuditLog
            ex = Nothing
        Finally

        End Try

    End Sub

    Private Sub GloRxToolBarUserCtrl1_tStrpFaxButtonClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles GloRxToolBarUserCtrl1.tStrpFaxButtonClick

        '' Property Set to control the print behaviour in Save & Close
        '' Bug #65840: gloEMR - Rx-Meds - While printing refill request it print twice from print button. 
        RxCommand = RxCommands.Fax
        RxOTCActions = OTCDrugAction.None
        isEndDuedateMessageOccured = False

        RaiseEvent PerformDrugAlertCheck(False)

        If IsOverrideReason = True Then
            IsOverrideReason = False
            Exit Sub
        End If
        isUnspecifiedMessageOccured = False
        If blnRxC1FlexClick = True Then
            Try
                Cursor.Current = Cursors.WaitCursor
                If _RxBusinessLayer.PrescriptionCol.Count > 0 Then

                    If IsSendRx <> True Then
                        SetMethod("Fax")
                    End If

                    _RxC1Flexgrid.InsertCheckState()
                    SaveRxMeds(True) 'used to save both prescription and Medication

                    If IsSendRx <> True Then
                        PrintRx(True)
                    Else
                        PrintRx(True, IsSendRx)
                    End If
                    If blnFaxLog = True Then
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.FaxPrescription, gloAuditTrail.ActivityType.Fax, "Prescription(s) faxed", nRxModulePatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success)
                    End If
                End If

                Cursor.Current = Cursors.Default
            Catch ex As Exception
                Cursor.Current = Cursors.Default
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.Fax, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                ex = Nothing
            End Try

        Else

            Try
                Cursor.Current = Cursors.WaitCursor
                SaveRxMeds(True, True) 'used to save both prescription and Medication

                Callprint(False)
                If blnFaxLog = True Then
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.FaxMedication, gloAuditTrail.ActivityType.Fax, "Medication(s) faxed", nRxModulePatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)

                End If

                Cursor.Current = Cursors.Default
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.CreateMedication, gloAuditTrail.ActivityType.Fax, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                Cursor.Current = Cursors.Default
                ex = Nothing
            End Try
        End If



    End Sub

    Private Sub GloRxToolBarUserCtrl1_SendFaxNormalPriorityToolStripMenuItemClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles GloRxToolBarUserCtrl1.SendFaxNormalPriorityToolStripMenuItemClick

        RxCommand = RxCommands.Fax
        RxOTCActions = OTCDrugAction.None

        RaiseEvent PerformDrugAlertCheck(False)

        If IsOverrideReason = True Then
            IsOverrideReason = False
            Exit Sub
        End If

        If blnRxC1FlexClick = True Then
            Try
                Cursor.Current = Cursors.WaitCursor

                If _RxBusinessLayer.PrescriptionCol.Count > 0 Then
                    SetMethod("Fax") 'since the Fax button was clicked so the method type will be "Fax"
                    '_RxBusinessLayer.SavePrescription()
                    _RxC1Flexgrid.InsertCheckState()
                    SaveRxMeds(True) 'used to save both prescription and Medication
                    If gblnInternetFax = False Then
                        If isPrinterSettingsSet(True) = False Then
                            Exit Sub
                        End If
                    End If

                    CurrentSendingFAXPriority = mdlFAX.enmFAXPriority.NormalPriority

                    PrintRx(True)
                    If blnFaxLog = True Then
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.FaxPrescription, gloAuditTrail.ActivityType.Fax, "Prescription(s) Fax Send with Normal Priority", nRxModulePatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success)
                    End If

                End If

                Cursor.Current = Cursors.Default
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.Fax, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                Cursor.Current = Cursors.Default
                ex = Nothing
            End Try
        Else
            Try
                Cursor.Current = Cursors.WaitCursor
                SaveRxMeds(True) 'used to save both prescription and Medication
                If gblnInternetFax = False Then
                    If isPrinterSettingsSet(True) = False Then
                        Exit Sub
                    End If
                End If

                CurrentSendingFAXPriority = mdlFAX.enmFAXPriority.NormalPriority
                Callprint(False)
                If blnFaxLog = True Then
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.FaxMedication, gloAuditTrail.ActivityType.Fax, "Medication(s) Fax Send with Normal Priority", nRxModulePatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                End If

                Cursor.Current = Cursors.Default
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.CreateMedication, gloAuditTrail.ActivityType.Fax, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                Cursor.Current = Cursors.Default
                ex = Nothing
            End Try

        End If

    End Sub

    Private Sub GloRxToolBarUserCtrl1_tStrpeRxClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles GloRxToolBarUserCtrl1.tStrpeRxClick

        RxCommand = RxCommands.eRx
        RxOTCActions = OTCDrugAction.None

        isEndDuedateMessageOccured = False
        isUnspecifiedMessageOccured = False
        Try
            Cursor.Current = Cursors.WaitCursor

            RaiseEvent PerformDrugAlertCheck(False)

            If IsOverrideReason = True Then
                IsOverrideReason = False
                Exit Sub
            End If

            Me.ErxMultipleDrugs()

            Cursor.Current = Cursors.Default
        Catch ex As Exception
            Cursor.Current = Cursors.Default
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.eRx, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Public Function SendDeniedWithNewRxToFollowMessageEPCS(ByVal blnDNTF As Boolean) As Boolean
        Dim blnValid As Boolean = True
        Try
            If gblnSurescriptEnabled Then

                InvalidSurescriptMessage = False
                If IsRefRequest Then
                    'check if drug is denied and send denywnewrxtofollow message
                    Dim Id As Int16 = 0

                    Dim intresult As Int16 = 0
                    'If isbuttonClickSave = False Then
                    intresult = _RxBusinessLayer.CheckForDeniedDrug10dot6(Id, blnDNTF)
                    ' End If
                    'Else
                    '    intresult = _RxBusinessLayer.CheckForDeniedDrug(Id)
                    If intresult = 1 Then
                        If _RxBusinessLayer.SavePrescription(gnLoginProviderID) Then
                            SendDeniedWithNewRxToFollowMessageEPCS = True
                            Exit Function
                        End If
                    ElseIf intresult = 2 Then 'the prescriber decided not to generate any message though substitution code changed
                        InvalidSurescriptMessage = True ''''make this value true becaz we should stay on Rx form. in finally again reset to false
                        SendDeniedWithNewRxToFollowMessageEPCS = False
                        Exit Function
                    ElseIf intresult = 3 Then
                        MessageBox.Show("Refill response could not be posted, hence prescription will not be saved", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        SendDeniedWithNewRxToFollowMessageEPCS = False
                        Exit Function
                    ElseIf intresult = 0 Then
                        If Not IsInternetConnectionAvailable Then
                            MessageBox.Show("You are not connected to the internet.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            SendDeniedWithNewRxToFollowMessageEPCS = False
                            Exit Function
                        End If

                        If _RxBusinessLayer.SavePrescription(gnLoginProviderID) Then
                            If Not myCaller Is Nothing Then
                                frmPatientExam.blnRxChangesMade = True
                            End If




                        Else
                            MessageBox.Show("You cannot prescribe duplicate drug within the threshold period. Please deny or cancel the request.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If
                    ElseIf intresult = -1 Then 'there are no approved
                        If _RxBusinessLayer.SavePrescription(gnLoginProviderID) Then
                            If Not myCaller Is Nothing Then
                                frmPatientExam.blnRxChangesMade = True
                            End If
                        End If
                    End If
                Else
                    If _RxBusinessLayer.SavePrescription(gnLoginProviderID) Then
                        If Not myCaller Is Nothing Then
                            frmPatientExam.blnRxChangesMade = True
                        End If
                    End If
                End If
            Else
                If _RxBusinessLayer.SavePrescription(gnLoginProviderID) Then
                    If Not myCaller Is Nothing Then
                        frmPatientExam.blnRxChangesMade = True
                    End If
                End If
            End If
            mdlGeneral.gblnIsDrugSave = True
            mdlGeneral.gnPrescriptionVisitID = _RxBusinessLayer.CurrentVisitID
            Return Nothing
        Catch ex As PrescriptionException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
            Return Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
            Return Nothing
        End Try

        Return blnValid
    End Function

    Private Function ErxMultipleDrugs() As Boolean

        Dim returnResult As Boolean = True
        Dim pbmList As New gloSureScript.BenefitsCoordinations
        Dim bResetRequest As Boolean = True
        Try
            If IsNothing(_RxBusinessLayer.PrescriptionCol) Then
                ErxMultipleDrugs = False
                Exit Function
            Else
                If _RxBusinessLayer.PrescriptionCol.Count <= 0 Then
                    ErxMultipleDrugs = False
                    Exit Function
                End If
            End If

            If IsSendRx = False Then
                If _isSaveClicked = True Then
                    Me._RxC1Flexgrid.InsertRefillCheckState(True)
                Else
                    SetMethod("eRx")
                    Me._RxC1Flexgrid.InsertCheckState(True)
                End If
            Else
                SetMethod()
            End If

            If _isSupervisormessagedisplay Then
                ErxMultipleDrugs = False
                Exit Function
            End If

            If RxOTCActions = OTCDrugAction.Cancel Then
                ErxMultipleDrugs = False
                Exit Function
            End If

            Dim isPrintAndDeny As Boolean = False

            If Not IsNothing(Me._RxBusinessLayer.TmpCheckStatesCol) AndAlso _RxBusinessLayer.TmpCheckStatesCol.Count > 0 Then
                If (strPrescriberSPI = "") Then
                    MessageBox.Show("The Prescriber not registered on surescripts network.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    ErxMultipleDrugs = False
                    Exit Function
                End If

                If Not ValidateNewRxBeforeDenial(isPrintAndDeny) Then
                    If Not isPrintAndDeny Then
                        ErxMultipleDrugs = False
                        Exit Function
                    End If
                End If

                If Not Validate10dot6Data() Then
                    ErxMultipleDrugs = False
                    Exit Function
                End If

            End If

            '_RxBusinessLayer.ProviderID = gnLoginProviderID
            _RxBusinessLayer.blnIsEPCSEnable = gbIsProviderEPCSEnable

            Me.SaveRxMeds(True, False)

            If (RxCommand = RxCommands.eRx Or RxCommand = RxCommands.IssueRx) OrElse ((RxCommand = RxCommands.Save OrElse RxCommand = RxCommands.SaveNClose) AndAlso (IsChangeRequest OrElse IsRefRequest)) Then
                If isPrintAndDeny Then
                    If _RxBusinessLayer.PrescriptionCol.Count = 1 Then 'when only one item is their in prescription collection
                        _RxBusinessLayer.PrescriptionCol.Item(0).Method = "Print"
                        If IsRefRequest Then
                            PrintRxReport_New(CType(_RxBusinessLayer.PrescriptionCol.Item(0).PrescriptionID, String), False, _RxBusinessLayer.PrescriptionDate, , RefRequest.ReferenceNumber)
                        ElseIf IsChangeRequest Then
                            PrintRxReport_New(CType(_RxBusinessLayer.PrescriptionCol.Item(0).PrescriptionID, String), False, _RxBusinessLayer.PrescriptionDate, , RxChangeRequest.TransactionRefNumber, "", "", True)
                        Else
                            PrintRxReport_New(CType(_RxBusinessLayer.PrescriptionCol.Item(0).PrescriptionID, String), False, _RxBusinessLayer.PrescriptionDate)
                        End If

                        _RxBusinessLayer.PrescriptionCol.Item(0).Status = "Approved"
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.PrintPrescription, gloAuditTrail.ActivityType.Print, "Prescription printed", nRxModulePatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                        'ToAttempteRx = True
                    Else
                        If _RxBusinessLayer.PrescriptionCol.Count > 0 Then
                            For i As Integer = 0 To _RxBusinessLayer.PrescriptionCol.Count - 1
                                If IsRefRequest OrElse IsChangeRequest Then
                                    If (_RxBusinessLayer.PrescriptionCol.Item(i).MessageType = "RefillRequest" AndAlso _RxBusinessLayer.PrescriptionCol.Item(i).NDCCode = RefRequest.NDCCode) OrElse _RxBusinessLayer.PrescriptionCol.Item(i).MessageType = "RxChangeRequest" Then
                                        If _RxBusinessLayer.TmpCheckStatesCol.Item(i).CheckState = True Then
                                            _RxBusinessLayer.PrescriptionCol.Item(i).Method = "Print"
                                            If IsRefRequest Then
                                                PrintRxReport_New(CType(_RxBusinessLayer.PrescriptionCol.Item(i).PrescriptionID, String), False, _RxBusinessLayer.PrescriptionDate, , RefRequest.ReferenceNumber, "", "", False)
                                            ElseIf IsChangeRequest Then
                                                PrintRxReport_New(CType(_RxBusinessLayer.PrescriptionCol.Item(i).PrescriptionID, String), False, _RxBusinessLayer.PrescriptionDate, , RxChangeRequest.TransactionRefNumber, "", "", True)
                                            End If
                                            _RxBusinessLayer.PrescriptionCol.Item(i).Status = "Approved"
                                        End If
                                    End If
                                End If
                            Next
                        End If
                    End If

                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.PrintPrescription, gloAuditTrail.ActivityType.Print, "Prescription printed", nRxModulePatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)

                    If blnFlagIsPrintSuccessFull Then
                        Dim updatePrecription As Boolean = False

                        If IsRefRequest Then
                            updatePrecription = SendDeniedWithNewRxToFollowMessageEPCS(True)
                        ElseIf IsChangeRequest Then
                            updatePrecription = True
                            For i As Integer = 0 To _RxBusinessLayer.PrescriptionCol.Count - 1
                                If _RxBusinessLayer.PrescriptionCol.Item(i).MessageType = "RxChangeRequest" Then
                                    Using p As New gloEMRGeneralLibrary.SurescriptsBusinessLayer()
                                        p.UpdateRxMessageStatus(RxChangeRequest.MessageID, _RxBusinessLayer.PrescriptionCol.Item(i).Status, "RxChange")
                                    End Using
                                    Exit For
                                End If
                            Next

                        End If

                        If updatePrecription Then
                            For row As Integer = 0 To _RxBusinessLayer.PrescriptionCol.Count - 1
                                If IsRefRequest OrElse IsChangeRequest Then
                                    If _RxBusinessLayer.PrescriptionCol.Item(row).Method = "Print" Then
                                        If (_RxBusinessLayer.PrescriptionCol.Item(row).MessageType = "RefillRequest" AndAlso _RxBusinessLayer.PrescriptionCol.Item(row).NDCCode = RefRequest.NDCCode) OrElse _RxBusinessLayer.PrescriptionCol.Item(row).MessageType = "RxChangeRequest" Then
                                            If _RxBusinessLayer.TmpCheckStatesCol.Item(row).CheckState = True Then
                                                _RxBusinessLayer.PrescriptionCol.Item(row).MessageType = ""
                                                _RxBusinessLayer.TmpCheckStatesCol.Item(row).CheckState = False
                                                _RxBusinessLayer.UpdatePrescription("", "", _RxBusinessLayer.PrescriptionCol.Item(row).PrescriptionID, "Print") '
                                            End If
                                        End If
                                    End If
                                End If
                            Next
                            If _isSaveClicked Then
                                Me.Close()
                                ErxMultipleDrugs = False
                                Exit Function
                            End If
                        End If
                    End If
                End If

                If _RxBusinessLayer.PrescriptionCol IsNot Nothing AndAlso _RxBusinessLayer.PrescriptionCol.Count > 0 Then
                    If clsgeneral.gblnIsFormularyServiceEnabled Then
                        pbmList = Me.FillERxEligibilityInfo
                    End If

                    If ToAttempteRx = False Then
                        gloSureScript.gloSurescriptGeneral.gstrAUSIDClinic = gstrAUSID

                        If RxOTCActions = OTCDrugAction.IncludeOTC Then
                            _RxBusinessLayer.blnIncludeOTCDrugs = True
                        Else
                            _RxBusinessLayer.blnIncludeOTCDrugs = False
                        End If

                        _RxBusinessLayer.ProcessERxData(pbmList)

                        If gbIsProviderEPCSEnable = True Then
                            _RxBusinessLayer.VendorName = gstrVendorName
                            _RxBusinessLayer.EpcsGoldUrl = gstrEpcsUrl
                            _RxBusinessLayer.VendorLabel = gstrVendorLabel
                            _RxBusinessLayer.VendorNodeName = gstrVendorNodeName
                            _RxBusinessLayer.VendorNodeLabel = gstrVendorNodeLabel
                            _RxBusinessLayer.SharedSecretKey = gstrSharedSecret
                            _RxBusinessLayer.ApplicationVersion = System.Windows.Forms.Application.ProductVersion
                            _RxBusinessLayer.oProcessERxPrescription.RouterName = gstrRouterName
                            _RxBusinessLayer.AttemptEPCSDrugCheck(_RxBusinessLayer.oProcessERxPrescription, gstrClinicName, strPrescriberNADEAN)
                        End If

                        _RxBusinessLayer.bDiscardAllclick = False
                        _RxBusinessLayer.GenerateFileinQueue(_RxBusinessLayer.oProcessERxPrescription, gstrClinicName, _RxBusinessLayer)

                        If _RxBusinessLayer.bDiscardAllclick = False Then


                            Dim str As String = Nothing
                            If IsRefRequest Then
                                str = "Refill"
                            ElseIf IsChangeRequest Then
                                str = "RxChange"
                            End If

                            _RxBusinessLayer.PostFileinQueue(_RxBusinessLayer.oProcessERxPrescription, str)

                            If (RefRequest IsNot Nothing AndAlso RefRequest.TransactionID <> 0) OrElse IsChangeRequest Then
                                For Each ED As gloSureScript.EDrug In _RxBusinessLayer.oProcessERxPrescription.DrugsCol
                                    If (ED.MessageName.Contains("RxChangeResponse") OrElse ED.MessageName.Contains("RefillResponse")) AndAlso ED.IsERXSuccessful Then
                                        Using DBLayer As New gloEMRGeneralLibrary.PrescriptionBusinessLayer()
                                            DBLayer.UpdateMedicationStatus(nRxModulePatientID, RxBusinesslayer._RxTransactionID, IsChangeRequest, _RxBusinessLayer.CurrentVisitID)
                                        End Using
                                    End If
                                Next
                            End If
                        End If


                        If IsRefRequest OrElse IsChangeRequest Then
                            If _RxBusinessLayer.PrescriptionCol.Count > 0 Then
                                For icntDelete As Int32 = 0 To _RxBusinessLayer.PrescriptionCol.Count - 1

                                    If (_RxBusinessLayer.PrescriptionCol(icntDelete).MessageType = "RefillRequest" OrElse (RxChangeRequest IsNot Nothing AndAlso RxChangeRequest.Type <> SS.ChangeRequestType.PriorAuthorizationRequired AndAlso _RxBusinessLayer.PrescriptionCol(icntDelete).MessageType = "RxChangeRequest")) And _RxBusinessLayer.PrescriptionCol(icntDelete).Method = "eRx" And _RxBusinessLayer.PrescriptionCol(icntDelete).FlagtoDeletePrescription Then
                                        bResetRequest = False
                                        gloEMRGeneralLibrary.gloGeneral.clsgeneral.blnIsRxMedsFormEdited = True
                                        gloEMRGeneralLibrary.gloGeneral.clsgeneral.blnRxDeleteFlag = True
                                        _RxBusinessLayer.DeletePrescriptionItem(_RxBusinessLayer.PrescriptionCol.Item(icntDelete).PrescriptionID, _RxBusinessLayer.PrescriptionCol.Item(icntDelete).VisitID, nRxModulePatientID)
                                        _RxBusinessLayer.PrescriptionCol.Item(icntDelete).State = "A"
                                    End If
                                Next
                            End If

                            If _RxBusinessLayer.bDiscardAllclick = False AndAlso bResetRequest = True Then
                                If IsRefRequest Then
                                    RefRequest = Nothing
                                ElseIf IsChangeRequest Then
                                    RxChangeRequest = Nothing
                                End If
                            End If
                        End If
                    End If
                    ToAttempteRx = False
                End If
            Else
                ErxMultipleDrugs = True
                Exit Function
            End If

            _MedBusinessLayer.FetchMedicationforUpdate(False)
            Return returnResult

        Catch ex As Exception
            ToAttempteRx = False
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
            Return returnResult
        End Try

    End Function

    Private Sub GloRxToolBarUserCtrl1_SendFaxImmediatelyToolStripMenuItemClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles GloRxToolBarUserCtrl1.SendFaxImmediatelyToolStripMenuItemClick

        RxCommand = RxCommands.Fax
        RxOTCActions = OTCDrugAction.None

        RaiseEvent PerformDrugAlertCheck(False)

        If IsOverrideReason = True Then
            IsOverrideReason = False
            Exit Sub
        End If

        If blnRxC1FlexClick = True Then
            Try
                Cursor.Current = Cursors.WaitCursor

                If _RxBusinessLayer.PrescriptionCol.Count > 0 Then
                    SetMethod("Fax")
                    _RxC1Flexgrid.InsertCheckState()
                    SaveRxMeds(True)
                    If gblnInternetFax = False Then
                        If isPrinterSettingsSet(True) = False Then
                            Exit Sub
                        End If
                    End If
                    CurrentSendingFAXPriority = mdlFAX.enmFAXPriority.SendImmediately
                    PrintRx(True)
                    'gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.FaxPrescription, gloAuditTrail.ActivityType.Fax, "Prescription(s) Fax Send Immediately", nRxModulePatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success)

                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.Add, "Prescription saved", nRxModulePatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)

                    If blnFaxLog = True Then
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.FaxPrescription, gloAuditTrail.ActivityType.Fax, "Prescription(s) Fax Send Immediately", nRxModulePatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success)
                    End If
                End If

                Cursor.Current = Cursors.Default
            Catch ex As Exception
                Cursor.Current = Cursors.Default
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.Fax, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                ex = Nothing
            End Try
        Else
            Try
                Cursor.Current = Cursors.WaitCursor

                SaveRxMeds(True)
                If gblnInternetFax = False Then
                    If isPrinterSettingsSet(True) = False Then
                        Exit Sub
                    End If
                End If

                CurrentSendingFAXPriority = mdlFAX.enmFAXPriority.SendImmediately
                Callprint(False)
                'gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.FaxPrescription, gloAuditTrail.ActivityType.Fax, "Prescription faxed", nRxModulePatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success)
                If blnFaxLog = True Then
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.FaxMedication, gloAuditTrail.ActivityType.Fax, "Medication(s) Fax Send Immediately", nRxModulePatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                End If
                Cursor.Current = Cursors.Default
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.CreateMedication, gloAuditTrail.ActivityType.Fax, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                ex = Nothing
                Cursor.Current = Cursors.Default
            End Try
        End If
    End Sub

    Private Sub GloRxToolBarUserCtrl1_tStrpSendRxButtonClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles GloRxToolBarUserCtrl1.tStrpSendRxButtonClick

        '' Property Set to control the print behaviour in Save & Close
        '' Bug #65840: gloEMR - Rx-Meds - While printing refill request it print twice from print button. 
        RxCommand = RxCommands.IssueRx
        RxOTCActions = OTCDrugAction.None
        IsSendRx = True
        Dim IsTrueERX As Boolean = False
        isEndDuedateMessageOccured = False

        Dim pbmList As New gloSureScript.BenefitsCoordinations
        Try
            pnlDI.Width = 370

            RaiseEvent PerformDrugAlertCheck(False)

            If IsOverrideReason = True Then
                IsOverrideReason = False
                Exit Sub
            End If

            If _RxBusinessLayer.PrescriptionCol.Count > 0 Then
                Try
                    For i As Int32 = 0 To _RxBusinessLayer.PrescriptionCol.Count - 1
                        If _RxC1Flexgrid.getCheckedState(i + 1) = True Then  ''check for row is checked selected or not
                            If _RxBusinessLayer.PrescriptionCol.Item(i).Method <> "eRx" Then   ''if not erx [print & fax] continue for next item
                                Continue For
                            ElseIf _RxBusinessLayer.PrescriptionCol.Item(i).Method = "eRx" Then  ''if eRx check for pharmacy enable & internet connection
                                IsTrueERX = True
                            End If
                        End If
                    Next

                    If Not CheckUnspecified() Then
                        Exit Sub
                    End If

                    If IsTrueERX = True Then
                        Cursor.Current = Cursors.WaitCursor
                        If _RxBusinessLayer.PrescriptionCol.Count > 0 Then
                            SetFlag()
                            _RxC1Flexgrid.InsertCheckState(True)
                            SaveRxMeds(True, False) 'used to save both prescription and Medication

                            If Not IsNothing(_RxBusinessLayer.TmpCheckStatesCol) Then
                                If _RxBusinessLayer.TmpCheckStatesCol.Count > 0 Then
                                    ErxMultipleDrugs()
                                End If
                            End If
                        End If
                    End If

                    Cursor.Current = Cursors.Default

                Catch ex As Exception

                    Cursor.Current = Cursors.Default
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.eRx, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    ex = Nothing
                End Try

                Cursor.Current = Cursors.Default

                Try
                    Cursor.Current = Cursors.WaitCursor

                    If _RxBusinessLayer.PrescriptionCol.Count > 0 Then
                        SetFlag() 'Developer:Pradeep 'Date:01/06/2012  'Bug ID: 15997 'Reason:to set the checkstate of grid 
                        _RxC1Flexgrid.InsertCheckState(True)
                        SaveRxMeds(True, False) 'used to save both prescription and Medication
                        Call PrintRx(, IsSendRx)
                    End If

                    Cursor.Current = Cursors.Default

                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.Print, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    ex = Nothing
                    Cursor.Current = Cursors.Default
                End Try

                Try
                    _RxC1Flexgrid.InsertCheckState()
                    Cursor.Current = Cursors.WaitCursor

                    If _RxBusinessLayer.PrescriptionCol.Count > 0 Then
                        SetFlag() 'Developer:Pradeep 'Date:01/06/2012  'Bug ID: 15997 'Reason:to set the checkstate of grid 
                        _RxC1Flexgrid.InsertCheckState(True)
                        SaveRxMeds(True, False) 'used to save both prescription and Medication
                        If gblnInternetFax = False Then
                            If isPrinterSettingsSet(True) = False Then
                                Exit Try
                            End If
                        End If
                        CurrentSendingFAXPriority = mdlFAX.enmFAXPriority.NormalPriority
                        PrintRx(True, IsSendRx)
                    End If

                    Cursor.Current = Cursors.Default
                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.Fax, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    ex = Nothing
                    Cursor.Current = Cursors.Default
                End Try

            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.eRx, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally
            IsSendRx = False
            If Not IsNothing(pbmList) Then
                pbmList.Dispose()
                pbmList = Nothing
            End If
        End Try
    End Sub

    Private Sub GloRxToolBarUserCtrl1_tStrpVwDeniedReportClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles GloRxToolBarUserCtrl1.tStrpVwDeniedReportClick
        Dim odatatable As DataTable = Nothing
        Try
            Dim ofrmDeniedRefReqRept As New frmDeniedRefReqReport(nRxModulePatientID)
            'odatatable = ofrmDeniedRefReqRept.GetDeniedReportSelectedPatient(nRxModulePatientID)
            'Changes done to resolve Bug #84603: 00000931 : Patient Name not displayed in Denied refill request report
            odatatable = ofrmDeniedRefReqRept.GetDeniedRefillRequestList(nRxModulePatientID)

            If Not IsNothing(odatatable) Then
                If odatatable.Rows.Count <= 0 Then
                    MessageBox.Show("No refill requests were denied for this patient", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    ofrmDeniedRefReqRept.Dispose()
                    ofrmDeniedRefReqRept = Nothing
                    'SLR: Dipsoe odatatable
                    odatatable.Dispose()
                    odatatable = Nothing
                    Exit Sub
                End If
            End If

            ofrmDeniedRefReqRept.ShowDialog(IIf(IsNothing(ofrmDeniedRefReqRept.Parent), Me, ofrmDeniedRefReqRept.Parent))
            ofrmDeniedRefReqRept.Dispose()
            ofrmDeniedRefReqRept = Nothing


        Catch ex As Exception
            Cursor.Current = Cursors.Default
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.eRx, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally
            If Not odatatable Is Nothing Then
                odatatable.Dispose()
                odatatable = Nothing
            End If
        End Try
    End Sub

#End Region

#Region "Prescription FORMULARY TOOL BAR User Control"

    Private Sub GetMedicationHistory10Dot6()


        If Not IsValidRxElig() Then
            Exit Sub
        End If

        SaveRxMeds(True, True)

        Dim ds As DataSet = Nothing
        Dim oRxhubInterface As ClsRxHubInterface = Nothing
        Dim objMedHxInterface As New MedHx.MedHxInterface
        Dim lstPreviousRequests As List(Of gloRxHub.ClsPatient) = Nothing
        Dim bDuplicateReqFound As Boolean = False

        Try
            Dim oEligibilityCheck As New clsEligibilityCheckDBLayer()
            oEligibilityCheck.GetEligibilityCheck(nRxModulePatientID)

            If mdlGeneral.MedHxRestriction > 0 Then
                lstPreviousRequests = oEligibilityCheck.GetPreviousMedHxRequest(nRxModulePatientID)

                If lstPreviousRequests IsNot Nothing Then
                    Dim duplicatePatient As gloRxHub.ClsPatient = lstPreviousRequests.OrderByDescending(Function(p) p.TransactionDate).FirstOrDefault(Function(p) p.Equals(oEligibilityCheck.Patient))

                    If duplicatePatient IsNot Nothing Then
                        bDuplicateReqFound = True

                        Dim dtOpeningDate As TimeSpan = duplicatePatient.TransactionDate.AddDays(mdlGeneral.MedHxRestriction).Subtract(DateTime.Now)
                        Dim sStringBuilder As New StringBuilder()

                        sStringBuilder.AppendLine("Medication history for this patient is already requested within the last " + mdlGeneral.MedHxRestriction.ToString() + " day(s). ")
                        sStringBuilder.AppendLine("The new request for this patient can be sent after " + dtOpeningDate.Hours.ToString() + " hour(s).")

                        MessageBox.Show(sStringBuilder.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)

                        sStringBuilder.Clear()
                        sStringBuilder = Nothing

                        duplicatePatient.Dispose()
                        duplicatePatient = Nothing
                    End If
                End If
            End If

            If bDuplicateReqFound = False Then
                ClsgloRxHubGeneral.ConnectionString = GetConnectionString()

                oRxhubInterface = New ClsRxHubInterface()
                ds = oRxhubInterface.GetMedHxRequestParameters(nRxModulePatientID, SelectedPBM, SelectedMemberID, gnLoginProviderID)

                Dim requestId = Convert.ToInt64(DateTime.Now.ToString("yyyyMMddhhmmssfff"))

                If Not IsNothing(ds) Then
                    Dim _TestMessage As String = String.Empty
                    Using ofrmprocessmedhx As New frmMedHxDownload(nRxModulePatientID, objMedHxInterface.GenerateMedHxRequest(ds, requestId, gstrVersion, gnClinicID))
                        ofrmprocessmedhx.RequestID = requestId
                        ofrmprocessmedhx.ShowDialog()
                        If ofrmprocessmedhx.MedicationVisitID <> 0 Then
                            _MedBusinessLayer.CurrentVisitID = ofrmprocessmedhx.MedicationVisitID
                            If _MedBusinessLayer.TransactionMode = MedicationBusinessLayer._TransactionMode.Add Then
                                _MedBusinessLayer.TransactionMode = MedicationBusinessLayer._TransactionMode.Edit
                            End If
                            _MedBusinessLayer.MedicationCol.Clear()
                            _MedBusinessLayer.FilterType = "Active"
                            _MedBusinessLayer.FetchMedicationforUpdate(False) 'SaveMeds(True)

                        End If
                    End Using
                End If
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.MedicationHistory, gloAuditTrail.ActivityType.General, "Error while generating Medication Histroy " & ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally
            objMedHxInterface = Nothing
            ds = Nothing
            oRxhubInterface = Nothing

            If lstPreviousRequests IsNot Nothing Then
                lstPreviousRequests.Clear()
                lstPreviousRequests = Nothing
            End If

        End Try

    End Sub

    Private Sub _objFormularyToolBar_tStrpMedicationHistoryClick(ByVal ButtonType As Short) Handles _objFormularyToolBar.tStrpMedicationHistoryClick
        Try
            _objFormularyToolBar.Enabled = False

            gloRxHub.ClsgloRxHubGeneral.gblnIsRxhubStagingServer = gblnRxhubStagingServer

            Select Case ButtonType
                Case 1
                    GetMedicationHistory10Dot6()
                    Exit Sub
                Case 8
                    ProcessRxEligibility(nRxModulePatientID)
                Case 9

                    Me.SendEPARequest()
                Case 10
                    Me.PDMPProcess()
                Case Else
            End Select
        Catch ex As Exception
            pnlFormularyTransactionMessage.Visible = False
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.eRx, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            _objFormularyToolBar.Enabled = True
            GloRxToolBarUserCtrl1.Enabled = True
            ex = Nothing
        Finally
            pnlFormularyTransactionMessage.Visible = False
            _objFormularyToolBar.Enabled = True
            GloRxToolBarUserCtrl1.Enabled = True
        End Try

    End Sub

    Private Sub ProcessRxEligibility(ByVal PatientID As Long, Optional SupressMessageOnAutoEligibility As Boolean = True)
        Try
            gloRxHub.ClsgloRxHubGeneral.gblnIsRxhubStagingServer = gblnRxhubStagingServer
            Try
                GloRxToolBarUserCtrl1.Enabled = False  ''bug 75465
            Catch ex As Exception

            End Try


            If gstrRxEligThresholdvalue = "" Then
                Dim objSettings As New clsSettings
                Dim stRxEligThresholdvalue As String = ""
                stRxEligThresholdvalue = objSettings.GetSettingValue("RX ELIGIBILITY THRESHOLD VALUE")
                Try
                    If (String.IsNullOrEmpty(stRxEligThresholdvalue)) Then
                        gstrRxEligThresholdvalue = (Val("0") / 24).ToString
                    Else
                        gstrRxEligThresholdvalue = (Val(stRxEligThresholdvalue) / 24).ToString
                    End If
                Catch ex As Exception
                    gstrRxEligThresholdvalue = (Val("0") / 24).ToString
                End Try


                If Not IsNothing(objSettings) Then
                    objSettings.Dispose()
                    objSettings = Nothing
                End If
            End If

            Dim oClsRxHubInterface As New ClsRxHubInterface(PatientID)
            If oClsRxHubInterface.IsEligibilitygGenerated_validation(PatientID, gstrRxEligThresholdvalue) = True Then

                If oClsRxHubInterface.FillPatientInfo_270(PatientID, gnLoginProviderID, SupressMessageOnAutoEligibility) = False Then
                    If Not IsNothing(oClsRxHubInterface) Then ''slr free oClsRxHubInterface
                        oClsRxHubInterface.Dispose()
                        oClsRxHubInterface = Nothing
                    End If
                    Exit Sub
                End If
                Try
                    lblFormularyTransactionMessage.Text = "Checking internet connection....."
                    pnlFormularyTransactionMessage.Visible = True ''''
                    pnlFormularyTransactionMessage.BringToFront()
                    Application.DoEvents()

                Catch ex As Exception

                End Try

                If Not IsInternetConnectionAvailable Then
                    MessageBox.Show("You are not connected to the internet.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Try
                        pnlFormularyTransactionMessage.Visible = False ''''
                    Catch ex As Exception

                    End Try

                    If Not IsNothing(oClsRxHubInterface) Then ''slr free oClsRxHubInterface
                        oClsRxHubInterface.Dispose()
                        oClsRxHubInterface = Nothing
                    End If
                    Exit Sub
                Else
                    Try
                        lblFormularyTransactionMessage.Text = "Internet connection available....."
                        Application.DoEvents()

                    Catch ex As Exception

                    End Try
                End If

                Dim strFileName As String = ""
                If oClsRxHubInterface.CheckOutboxFolder(ClsgloRxHubGeneral.gnstrApplicationFilePath) Then
                    Try
                        lblFormularyTransactionMessage.Text = "Sending eligibility information ...."
                        Application.DoEvents()

                        strFileName = oClsRxHubInterface.Generate270_5010(gstrRxHubParticipantId, gstrRxHubPassword)
                    Catch ex As Exception
                        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.eRx, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                        ex = Nothing
                        If Not IsNothing(oClsRxHubInterface) Then ''slr free oClsRxHubInterface
                            oClsRxHubInterface.Dispose()
                            oClsRxHubInterface = Nothing
                        End If
                        Exit Sub
                    End Try

                    Try
                        'lblFormularyTransactionMessage.Text = "Sending eligibility information....."
                        'Application.DoEvents()

                        If strFileName <> "" Then
                            Dim gnInsuranceId As Long = 0
                            Dim EDI271FilePath As String = ""

                            For postCnt As Integer = 0 To 4
                                Try

                                    lblFormularyTransactionMessage.Text = "Waiting For eligibility response ....." & vbCrLf & "Attempt Number " & (postCnt + 1)
                                    Application.DoEvents()
                                Catch ex As Exception

                                End Try

                                EDI271FilePath = oClsRxHubInterface.PostEDIFile_5010(strFileName, "EDI270")
                                If EDI271FilePath <> "" Then
                                    Exit For
                                Else
                                    Continue For
                                End If
                            Next

                            If EDI271FilePath <> "" Then
                                lblFormularyTransactionMessage.Text = "Eligibility response received, Reading response....."
                                Application.DoEvents()

                                Try
                                    If oClsRxHubInterface.LoadEDIObject_271_5010(EDI271FilePath) = False Then
                                        MessageBox.Show("NAK : TRANSACTION CANNOT BE IDENTIFIED NOR PROCESSED", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                        If Not IsNothing(oClsRxHubInterface) Then ''slr free oClsRxHubInterface
                                            oClsRxHubInterface.Dispose()
                                            oClsRxHubInterface = Nothing
                                        End If
                                        Exit Sub
                                    Else
                                        '//file is valid edi file and has got loaded in the ediDOC object
                                    End If

                                    oClsRxHubInterface.Read271Response_5010()
                                    Try
                                        pnlFormularyTransactionMessage.Visible = False
                                    Catch ex As Exception

                                    End Try


                                    '' Checking & updating Eligibility Status After Eligibility Responce
                                    Using objEligibility As New ClsRxHubInterface
                                        _objFormularyToolBar.CurrentEligibilityStatus = objEligibility.GetEligibilityStatus(PatientID, gstrRxEligThresholdvalue)
                                        _objFormularyToolBar.SetEligibilityStatus()
                                    End Using

                                    RefreshPBMCombo()

                                Catch ex As Exception
                                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.eRx, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                                    ex = Nothing
                                End Try
                            Else
                                MessageBox.Show("No eligibility response file received from surescript, please resend the eligibility request again.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                Dim oClsgloRxHubDBLayer As New gloRxHub.clsgloRxHubDBLayer
                                oClsgloRxHubDBLayer.DeleteRxH_Table("gsp_DeleteRxHTables", PatientID)
                                oClsgloRxHubDBLayer.Dispose()
                                Try
                                    pnlFormularyTransactionMessage.Visible = False ''''
                                Catch ex As Exception

                                End Try

                            End If

                        End If
                    Catch ex As Exception
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.eRx, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                        ex = Nothing
                    End Try
                Else
                    MessageBox.Show("The 270 EDI request cannot be generated since there is no outbox folder.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            End If

            If SupressMessageOnAutoEligibility Then
                Dim dt As DataTable = Nothing 'SLR: new is not neededd
                Try
                    dt = oClsRxHubInterface.Get271EligibilityResponseInformation(PatientID)
                    If Not IsNothing(dt) Then
                        If dt.Rows.Count > 0 Then
                            Dim ofrm As New frmViewEligiblityInformation(dt, False)
                            ofrm.ShowInTaskbar = False
                            ofrm.StartPosition = FormStartPosition.CenterParent
                            ofrm.BringToFront()
                            ofrm.ShowDialog(IIf(IsNothing(ofrm.Parent), Me, ofrm.Parent))
                            'SLR :ofrm.dispose
                            ofrm.Dispose()
                            ofrm = Nothing
                        Else
                            If Not IsNothing(oClsRxHubInterface) Then ''slr free oClsRxHubInterface
                                oClsRxHubInterface.Dispose()
                                oClsRxHubInterface = Nothing
                            End If
                            Exit Sub
                        End If
                    Else
                        MessageBox.Show("The eligibility request for this patient cannot be shown since request is valid only for 72 hours", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        If Not IsNothing(oClsRxHubInterface) Then ''slr free oClsRxHubInterface
                            oClsRxHubInterface.Dispose()
                            oClsRxHubInterface = Nothing
                        End If
                        Exit Sub
                    End If
                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    ex = Nothing
                Finally
                    If Not IsNothing(oClsRxHubInterface) Then
                        oClsRxHubInterface.Dispose()
                        oClsRxHubInterface = Nothing
                    End If
                    If Not IsNothing(dt) Then
                        dt.Dispose()
                        dt = Nothing
                    End If
                End Try
            End If
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.Eligibility, gloAuditTrail.ActivityType.Send, "Eligibility check performed", nRxModulePatientID, 0, _RxBusinessLayer.ProviderID, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
        Catch ex As Exception
            pnlFormularyTransactionMessage.Visible = False
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.eRx, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally
            Try
                GloRxToolBarUserCtrl1.Enabled = True
            Catch ex As Exception

            End Try

        End Try
        Try
            If _RxBusinessLayer.PrescriptionCol.Count > 0 Then
                If (_RxBusinessLayer.PrescriptionCol(_RxC1Flexgrid.SelectedRowIndex - 1).IsFormularyQueried = False) Then
                    _RxC1Flexgrid.ShowFormulary(_RxC1Flexgrid.SelectedRowIndex)
                End If
            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Sub _objFormularyToolBar_tlStrpPBMComboClick(ByVal ButtonType As Short) Handles _objFormularyToolBar.tlStrpPBMComboClick

        RaiseEvent PerformEPAStatusCheck(Nothing)
        'Me.PerformPDRStatusCheck(Nothing)
        Application.DoEvents()

        If clsgeneral.gblnIsFormularyServiceEnabled Then

            '' If RxElig is not done then dont query formulary infomation
            If _objFormularyToolBar.CurrentEligibilityStatus = RxBusinesslayer.EligibilityStatus.NotChecked Then
                Return
            End If

            If _RxBusinessLayer IsNot Nothing AndAlso _RxBusinessLayer.PrescriptionCol IsNot Nothing Then
                If _RxBusinessLayer.PrescriptionCol.Count < 1 Then
                    Return
                End If
            End If

            Dim service As New gloFSHelper(clsgeneral.gstrFormularyServiceURL, GetConnectionString(), clsgeneral.sDIBServiceURL)
            Dim drugList As New List(Of FormularyDrug)
            Dim StatusList As List(Of FormularyStatus)
            Dim CopayList As List(Of FormularyCopay)

            Try

                If _RxBusinessLayer IsNot Nothing AndAlso _RxBusinessLayer.PrescriptionCol IsNot Nothing Then
                    For Each drug As Prescription In _RxBusinessLayer.PrescriptionCol.Where(Function(p) p.mpid > 0 AndAlso p.RxType <> Nothing AndAlso p.RxType.Any())
                        drugList.Add(New FormularyDrug(drug.mpid, drug.RxType))
                    Next


                    StatusList = service.GetFormularyStatusList(SenderId, FormularyId, drugList)
                    Dim rowSelected As Integer

                    If StatusList IsNot Nothing AndAlso StatusList.Count > 0 Then
                        CopayList = service.GetCopayList(SenderId, CopayID, StatusList)
                        For Each status As FormularyStatus In StatusList
                            Dim iStatus As Integer = -1
                            Dim id As Long = status.id
                            For Each Drug As Prescription In _RxBusinessLayer.PrescriptionCol.Where(Function(p) p.mpid = id)
                                If Drug IsNot Nothing Then
                                    rowSelected = Drug.ItemNumber + 1
                                    Dim copay As FormularyCopay = CopayList.FirstOrDefault(Function(p) p.id = id)
                                    If (Integer.TryParse(status.fs, iStatus)) Then
                                        _RxC1Flexgrid.UpdateDrugFormularyInfo(rowSelected, status, copay)
                                    Else
                                        _RxC1Flexgrid.UpdateDrugFormularyInfo(rowSelected, status)
                                    End If
                                End If
                            Next
                        Next
                    End If
                    If _RxBusinessLayer.PrescriptionCol.Count > 0 Then
                        pnlFormulary.Visible = False
                        pnlElementHostCopay.Visible = False
                        _RxC1Flexgrid.ShowFormulary(_RxC1Flexgrid.SelectedRowIndex)
                    End If
                    ''''_RxC1Flexgrid.PrescriptionRowChanged(True, FormularyQueriedFrom.PBMRefresh)
                End If
            Catch wex As WebException
                pnlFormularyProgress.Visible = False
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.Formulary30, gloAuditTrail.ActivityType.None, "FS3_PBMRefresh :" + If(wex.InnerException IsNot Nothing, wex.InnerException, wex).ToString(), gloAuditTrail.ActivityOutCome.Failure)
                If IsFormLoaded Then
                    MessageBox.Show("Unable to retrieve formulary information.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.Formulary30, gloAuditTrail.ActivityType.None, "FS3_PBMRefresh :" + If(ex.InnerException IsNot Nothing, ex.InnerException, ex).ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Finally
                pnlFormularyProgress.Visible = False
                service = Nothing
                drugList = Nothing
                StatusList = Nothing
                CopayList = Nothing
            End Try

        End If

    End Sub



#End Region

#Region "Validate Medhx 10.6 Info"

    Public ReadOnly Property IsValidHxRequest
        Get
            If IsNothing(_objFormularyToolBar) Then
                Return Nothing
            End If

            If _objFormularyToolBar.CurrentEligibilityStatus = RxBusinesslayer.EligibilityStatus.Passed Then
                Return True
            Else
                Return False
            End If
        End Get
    End Property

    Public ReadOnly Property IsPBMSelected
        Get
            If IsNothing(_objFormularyToolBar) Then
                Return Nothing
            End If

            If _objFormularyToolBar.tlStrpPBMCombo.Items.Count = 0 Then
                Return False
            Else
                Return True
            End If
        End Get
    End Property

    Public ReadOnly Property SelectedPBM
        Get
            If IsNothing(_objFormularyToolBar) Then
                Return Nothing
            End If

            If Not _objFormularyToolBar.tlStrpPBMCombo.Items.Count = 0 Then

                'Dim aryPBM As String() = Split(_objFormularyToolBar.tlStrpPBMCombo.ComboBox.Text, "-")
                Dim aryPBM As String() = Split(_objFormularyToolBar.tlStrpPBMCombo.ComboBox.SelectedText, "-")
                If aryPBM.Length >= 1 Then
                    Return Convert.ToString(aryPBM(0))
                End If

                Return Nothing
            Else
                Return Nothing
            End If
        End Get
    End Property

    Public ReadOnly Property SelectedMemberID
        Get
            If IsNothing(_objFormularyToolBar) Then
                Return Nothing
            End If


            If Not _objFormularyToolBar.tlStrpPBMCombo.Items.Count = 0 Then
                Return Convert.ToString(_objFormularyToolBar.tlStrpPBMCombo.ComboBox.SelectedValue)
            Else
                Return ""
            End If
        End Get
    End Property

    Private Function IsValidRxElig() As Boolean

        If Not IsValidHxRequest Then
            MessageBox.Show("There is no active PBM available for this patient. Please check the eligibility again!", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return False
        End If

        If Not IsPBMSelected Then
            MessageBox.Show("There is no active PBM available for this patient. Please check the eligibility again!", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return False
        End If

        Return True

    End Function

#End Region

#Region "Prescription C1 Flex Grid USer Control"

    Private Sub _RxC1Flexgrid_DeleteCorrespondingMedication(ByVal sender As Object, ByVal e As gloSureScript.PrescriptionMedicationEventArgs) Handles _RxC1Flexgrid.DeleteCorrespondingMedication
        Try
            Dim strpresciptionId As Long = 0
            Dim myItemNumber As Integer = -1
            If Not IsNothing(e) Then
                strpresciptionId = e.PrescriptionID
            End If
            For i As Integer = 0 To _MedBusinessLayer.MedicationCol.Count - 1
                If _MedBusinessLayer.MedicationCol.Item(i)._PrescriptionId = strpresciptionId Then
                    _MedBusinessLayer.MedicationCol.Item(i).State = "D"
                    myItemNumber = _MedBusinessLayer.MedicationCol.Item(i).ItemNumber
                    RemoveControl()
                    RemoveDrugInfoControl()
                    Exit For
                End If
            Next
            If (myItemNumber <> -1) Then
                _MxC1Flexgrid.DeleteCorrespondingMedication(myItemNumber)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub _RxC1Flexgrid_InfoButtonDocumentClicked(ByVal templateCode As String, ByVal openFor As String, ByVal TemplateName As String, ByVal sResourceType As String) Handles _RxC1Flexgrid.InfoButtonDocumentClicked
        Dim ofrmPatientEducation As New frmPatientEducationPreview()

        Try
            ofrmPatientEducation.Text = openFor

            GloRxToolBarUserCtrl1.Visible = True
            ofrmPatientEducation.Text = openFor
            ofrmPatientEducation.PATID = GetCurrentPatientID
            ofrmPatientEducation.TempName = TemplateName
            ofrmPatientEducation.Sourc = gloEMRGeneralLibrary.clsInfobutton.enumSource.Medication.GetHashCode()
            ofrmPatientEducation.ResourcCat = gloEMRGeneralLibrary.clsInfobutton.enumResourceCategory.InternalLibrary
            If sResourceType = "Provider Reference Material" Then
                ofrmPatientEducation.ResourcTyp = gloEMRGeneralLibrary.clsInfobutton.enumResourceType.ProviderReferenceMaterial
            Else
                ofrmPatientEducation.ResourcTyp = gloEMRGeneralLibrary.clsInfobutton.enumResourceType.PatientReferenceMaterial
            End If

            ofrmPatientEducation.TMPID = Convert.ToInt64(templateCode)
            ofrmPatientEducation.ISGRID = False
            GloRxToolBarUserCtrl1.Visible = True
            ofrmPatientEducation.ShowDialog()

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            pnlmainToolBar.Visible = True
            ofrmPatientEducation.Dispose()
            ofrmPatientEducation = Nothing
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally
            If (IsNothing(ofrmPatientEducation) = False) Then
                ofrmPatientEducation.Close()
            End If
            If (IsNothing(ofrmPatientEducation) = False) Then
                ofrmPatientEducation.Dispose()
                ofrmPatientEducation = Nothing
            End If

        End Try
    End Sub

    Private Sub _RxC1Flexgrid_PrescriptionItemDeleted() Handles _RxC1Flexgrid.PrescriptionItemDeleted
        Try
            RemoveControl()
            RemoveDrugInfoControl()

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.Delete, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub btnRxDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRxDown.Click
    End Sub

    Private Sub btnRxUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRxUp.Click

    End Sub

    '' TODO : Not in use, to verify & remove _RxC1Flexgrid__FlexSelChange event
    Private Sub _RxC1Flexgrid__FlexSelChange(ByVal sender As System.Object, ByVal e As EventArgs) Handles _RxC1Flexgrid._FlexSelChange
        Dim GridRowNo As Integer = _RxC1Flexgrid.GetRowNoFromGrid(_RxC1Flexgrid.SelectedRowIndex)
        Dim PrescriptionRowNow As Integer = -1
        If _RxBusinessLayer.PrescriptionCol.Count > 0 Then
            For i As Integer = 0 To _RxBusinessLayer.PrescriptionCol.Count - 1
                If _RxBusinessLayer.PrescriptionCol.Item(i).ItemNumber = GridRowNo Then
                    PrescriptionRowNow = i
                    Exit For


                End If
            Next
        End If
        If PrescriptionRowNow = -1 Then
            'Abnormal condition: Exit The procedure
        Else
            GridRowNo = PrescriptionRowNow
        End If
        If GridRowNo <> -1 Then
            If _RxBusinessLayer.PrescriptionCol.Item(GridRowNo).IsFormularyQueried = False Then
                isRxFlexGridClick = True
                isRxFlexGridClick = False
            Else
                'If pnlFormulary.Visible = True Then
                CloseFormularyAndCOVPnl()
                'End If
                'AddControl()
                ' sptRefill.Enabled = True
                'pnlRefill.Visible = True
            End If
            'If _RxBusinessLayer.PrescriptionCol.Item(GridRowNo).IseRxed > 0 Then
            '    GloRxToolBarUserCtrl1.tStrpCancelRx.Enabled = True
            'Else
            '    GloRxToolBarUserCtrl1.tStrpCancelRx.Enabled = False
            'End If
        End If

    End Sub

    Private Sub _RxC1Flexgrid_MouseDoubleClick(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles _RxC1Flexgrid.RowDoubleClicked
        If e.Button = Windows.Forms.MouseButtons.Left Then
            If IsFormLocked Then
                Exit Sub
            End If

            Dim r As Int32
            Dim c As Int32
            r = CType(sender, C1.Win.C1FlexGrid.C1FlexGrid).HitTest(e.X, e.Y).Row
            c = CType(sender, C1.Win.C1FlexGrid.C1FlexGrid).HitTest(e.X, e.Y).Column
            If r > 0 And c <> 5 And c <> 6 Then
                Dim GridRowNo As Integer = _RxC1Flexgrid.GetRowNoFromGrid(_RxC1Flexgrid.SelectedRowIndex)
                If Me.IsChangeRequest AndAlso _RxBusinessLayer.PrescriptionCol(GridRowNo).MessageType = "RxChangeRequest" Then
                    If Me.RxChangeRequest.Type = SS.ChangeRequestType.GenericSubstitution Then
                        MessageBox.Show("This is a RxChange request for Generic Substitution and cannot be edited.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    ElseIf Me.RxChangeRequest.Type = SS.ChangeRequestType.PriorAuthorizationRequired Then
                        MessageBox.Show("This is a RxChange request for Prior Authorization and cannot be edited.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub _RxC1Flexgrid__FlexMouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles _RxC1Flexgrid._FlexMouseDown
        If e.Button = Windows.Forms.MouseButtons.Left Then
            If IsFormLocked Then
                Exit Sub
            End If

            Dim r As Int32
            Dim c As Int32
            r = CType(sender, C1.Win.C1FlexGrid.C1FlexGrid).HitTest(e.X, e.Y).Row
            c = CType(sender, C1.Win.C1FlexGrid.C1FlexGrid).HitTest(e.X, e.Y).Column
            If r > 0 And c <> 5 And c <> 6 Then
                Dim GridRowNo As Integer = _RxC1Flexgrid.GetRowNoFromGrid(_RxC1Flexgrid.SelectedRowIndex)

                isRxFlexGridClick = True
                AddControl()
                sptRefill.Enabled = True
                pnlRefill.Visible = True

                isRxFlexGridClick = False
            End If
        End If
    End Sub

    Private Sub _RxC1Flexgrid__FlexMouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles _RxC1Flexgrid._FlexMouseUp
        If IsFormLocked Then
            Exit Sub
        End If
        _nAddDrugtoMx = 0
        If blnRxC1FlexClick = False Then
            Me.Text = "Rx/Meds" & " " & "Mode:- Prescription"
            blnRxC1FlexClick = True
            '_RxListUserCtrl.IsRxC1FlexClick = blnRxC1FlexClick
            btnMedication.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
            btnMedication.BackgroundImageLayout = ImageLayout.Stretch
            btnPrescription.BackgroundImage = Global.gloEMR.My.Resources.Img_Rx_MxGreen
            btnPrescription.BackgroundImageLayout = ImageLayout.Stretch
            Try
                gloPatient.gloPatient.GetWindowTitle(Me, nRxModulePatientID, GetConnectionString(), gstrMessageBoxCaption)
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                ex = Nothing
            End Try
        End If
    End Sub

#End Region

#Region "CUSTOM Prescription"

    Private Sub objCustomPrescription_PharmacyClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles objCustomPrescription.PharmacyClick
        Try
            If Not IsNothing(oListUsers) Then
                For i As Integer = Me.Controls.Count - 1 To 0 Step -1
                    If Me.Controls(i).Name = oListUsers.Name Then
                        'SLR: Remove handler if any before dispose
                        Dim otemplistcontrol As gloListControl.gloListControl
                        otemplistcontrol = Me.Controls(i)

                        'SLR: Supposed to be Me.Controls(i).dispose and not oListControl.dispose
                        'oListControl.Dispose()
                        Me.Controls.Remove(otemplistcontrol)
                        'otemplistcontrol.Dispose()
                        'oListControl.Dispose() 'SLR: instead it should be olistUsers
                        Exit For
                    End If
                Next

                Try
                    RemoveHandler oListUsers.ItemSelectedClick, AddressOf oListUsers_ItemSelectedClick
                    RemoveHandler oListUsers.ItemClosedClick, AddressOf oListUsers_ItemClosedClick
                    RemoveHandler oListUsers.AddFormHandlerClick, AddressOf oListUsers_AddFormHandlerClick
                    RemoveHandler oListUsers.ModifyFormHandlerClick, AddressOf oListUsers_ModifyFormHandlerClick
                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    ex = Nothing
                End Try

                oListUsers.Dispose()
                oListUsers = Nothing
            End If
            oListUsers = New gloListControl.gloListControl(GetConnectionString, gloListControl.gloListControlType.Pharmacy, False, Me.Width)
            oListUsers.ControlHeader = "Users"

            AddHandler oListUsers.ItemSelectedClick, AddressOf oListUsers_ItemSelectedClick
            AddHandler oListUsers.ItemClosedClick, AddressOf oListUsers_ItemClosedClick
            AddHandler oListUsers.AddFormHandlerClick, AddressOf oListUsers_AddFormHandlerClick
            AddHandler oListUsers.ModifyFormHandlerClick, AddressOf oListUsers_ModifyFormHandlerClick
            oListUsers.Dock = DockStyle.Fill

            If IsNothing(ToList) = False Then
                For i As Integer = 0 To ToList.Count - 1
                    oListUsers.SelectedItems.Add(ToList(i))
                Next
            End If
            ''
            pnlRefill.Controls.Add(oListUsers)
            oListUsers.OpenControl()
            oListUsers.ShowHeaderPanel(False)
            oListUsers.Dock = DockStyle.Fill
            oListUsers.BringToFront()

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.ConfidentialInformation, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub objCustomPrescription_CloseComplaintClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles objCustomPrescription.CloseComplaintClick

        Try
            If Not IsNothing(objChiefcomplaints) Then
                Me.Controls.Remove(objChiefcomplaints)
                objChiefcomplaints.Visible = False
                'SLR: Dipsoe and then
                objChiefcomplaints.Dispose()
                objChiefcomplaints = Nothing
                rowindex = 0
            End If
            GridStatus = 3
            BindSigGrid()

            Dim arrRxProblem As New Dictionary(Of String, String)

            Dim frm As New frmProblemList(0, GetVisitID(DateTime.Now, nRxModulePatientID), nRxModulePatientID, False)
            For i As Integer = 0 To objCustomPrescription.lstChfcomp.Items.Count - 1
                arrRxProblem.Add(objCustomPrescription.Problems(i), objCustomPrescription.lstChfcomp.Items(i))
            Next
            frmProblemList.ArrRx_Problem = arrRxProblem
            frmProblemList.blnRxMedFromExam = True
            frmProblemList.blnRxMedToProblem = True


            frm.StartPosition = FormStartPosition.CenterScreen
            frm.ShowInTaskbar = False
            frm.ShowMessageForPendingReconciliation()
            If frm.ShowDialog(IIf(IsNothing(frm.Parent), Me, frm.Parent)) = Windows.Forms.DialogResult.OK Then
                objCustomPrescription.lstChfcomp.Items.Clear()
                objCustomPrescription.Problems.Clear()
                For Len As Integer = 0 To frmProblemList.ArrRx_Problem.Count - 1
                    objCustomPrescription.lstChfcomp.Items.Add(frmProblemList.ArrRx_Problem.Values(Len))
                    objCustomPrescription.Problems.Add(frmProblemList.ArrRx_Problem.Keys(Len))
                Next
            End If
            frm.Dispose()
            frm = Nothing
            'SLR :Finally clear arrPrxProblem
            If Not arrRxProblem Is Nothing Then
                arrRxProblem = Nothing
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try

    End Sub

    Private Sub objCustomPrescription_SigClick_DrugProvAsso(ByVal sender As Object, ByVal e As System.EventArgs, ByVal sDrugName As String) Handles objCustomPrescription.SigClick_DrugProvAsso
        Try

            If Not IsNothing(objSigControl) Then
                Me.Controls.Remove(objSigControl)
                'SLR: ALso it was part of panel also. Hence remove from Panel also
                Me.pnlRefill.Controls.Remove(objSigControl)
                objSigControl.Visible = False
                'SLR: Dipsoe and tehn
                objSigControl.Dispose()
                objSigControl = Nothing
                rowindex = 0
            End If
            GridStatus = 1

            BindSigGrid(sDrugName)
            objSigControl = New gloUC_CustomSearchInC1Flexgrid(dt, False)

            Me.Controls.Add(objSigControl)
            Me.pnlRefill.Controls.Add(objSigControl)


            objSigControl.Dock = DockStyle.Fill
            objSigControl.Visible = True
            objSigControl.BringToFront()

            Dim searchrow As Integer
            If Not IsNothing(dt) AndAlso dt.Rows.Count > 0 Then
                If sigid <> 0 Then
                    searchrow = objSigControl._UCflex.FindRow(sigid, 0, 0, False, True, False)
                    objSigControl._UCflex.Select(searchrow, 0, True)
                    sigid = 0
                Else
                    objSigControl._UCflex.Select(1, 0, True)
                End If
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub objCustomPrescription_SigClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles objCustomPrescription.SigClick
        Try

            If Not IsNothing(objSigControl) Then
                Me.Controls.Remove(objSigControl)
                'SLR: ALso it was part of panel also. Hence remove from Panel also
                Me.pnlRefill.Controls.Remove(objSigControl)
                objSigControl.Visible = False
                'SLR: Dispose and then
                objSigControl.Dispose()
                objSigControl = Nothing
                rowindex = 0
            End If
            GridStatus = 1

            BindSigGrid()
            objSigControl = New gloUC_CustomSearchInC1Flexgrid(dt, False)

            Me.Controls.Add(objSigControl)
            Me.pnlRefill.Controls.Add(objSigControl)


            objSigControl.Dock = DockStyle.Fill
            objSigControl.Visible = True
            objSigControl.BringToFront()

            Dim searchrow As Integer
            If Not IsNothing(dt) AndAlso dt.Rows.Count > 0 Then
                If sigid <> 0 Then
                    searchrow = objSigControl._UCflex.FindRow(sigid, 0, 0, False, True, False)
                    objSigControl._UCflex.Select(searchrow, 0, True)
                    sigid = 0
                Else
                    objSigControl._UCflex.Select(1, 0, True)
                End If
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub PerformDoseCheck(ByVal ndc As String, ByVal qty As Decimal, ByVal frequency As Int16, ByVal duration As Integer, ByVal durationunit As String)
        Try
            If gblnClinicDISetting = True And gblnAllowUserDISetting = True Then
                If Not String.IsNullOrWhiteSpace(ndc) Then
                    If duration <> 0 Then
                        Dim days As Integer = 0
                        Select Case durationunit.ToUpper
                            Case "MONTHS"
                                days = 30
                            Case "DAYS"
                                days = 1
                            Case "WEEKS"
                                days = 7
                        End Select
                        duration = days * duration
                    End If

                    Dim resp As String = Nothing
                    Using oDIBGSHelper As New gloGlobal.DI.gloDIServiceHelper(gstrDrugInteractionServiceURL)
                        Dim req As New gloGlobal.DI.DoseCheckRequest()
                        req.ndc = ndc
                        req.qty = qty
                        req.freq = frequency
                        req.interval = duration
                        resp = oDIBGSHelper.PerformDoseCheck(req)
                    End Using

                    If resp.Length > 0 Then
                        MessageBox.Show(resp, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                End If
            End If
        Catch ex As Exception
            MessageBox.Show("Unable to retrieve Dose Check information.", "Error Calling Clinical Method!", MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.DrugInteraction, gloAuditTrail.ActivityType.Close, "PerformDoseCheck : " & ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub objCustomPrescription_OKClick(ByVal ndc As String, ByVal amt As Decimal, ByVal frequency As Int16, ByVal duration As Integer, ByVal durationunit As String, ByVal row As Integer) Handles objCustomPrescription.OKClick

        PerformDoseCheck(ndc, amt, frequency, duration, durationunit)

        If Not IsNothing(dtPharmacyDetails) AndAlso dtPharmacyDetails.Rows.Count > 0 Then
            _RxC1Flexgrid.SetFlexGridData(row, dtPharmacyDetails)
        Else
            _RxC1Flexgrid.SetFlexGridData(row, Nothing, _RxBusinessLayer.PrescriptionCol)
        End If

        If cmbSupervisingProvider.Visible = True And cmbSupervisingProvider.SelectedIndex <> -1 Then
            Get_SupervisorDetails()
        End If

        RemoveControl()
        sptRefill.Enabled = False
        pnlRefill.Visible = False
        Dim _enteredPANumber As String = _RxBusinessLayer.PrescriptionCol.Item(row).PriorAuthorizationNumber.Trim()

        RaiseEvent PerformEPAStatusCheck(row)
        If Not String.IsNullOrEmpty(_enteredPANumber) OrElse (Not String.IsNullOrEmpty(_RxBusinessLayer.PrescriptionCol.Item(row).PriorAuthorizationNumber) AndAlso String.IsNullOrEmpty(_enteredPANumber)) Then
            Dim qty As String = String.Empty
            Dim days As String = String.Empty

            If Not String.IsNullOrWhiteSpace(_RxBusinessLayer.PrescriptionCol.Item(row).Duration) Then
                days = _RxBusinessLayer.PrescriptionCol.Item(row).DaysSupply
            End If
            If Not String.IsNullOrWhiteSpace(_RxBusinessLayer.PrescriptionCol.Item(row).Amount) Then
                If IsNumeric(_RxBusinessLayer.PrescriptionCol.Item(row).Amount.Split(" ")(0)) Then
                    qty = _RxBusinessLayer.PrescriptionCol.Item(row).Amount.Split(" ")(0)
                End If
            End If
            Using ePAInsertUpdate As New EPABusinesslayer()
                If String.IsNullOrEmpty(_RxBusinessLayer.PrescriptionCol.Item(row).PAReferenceID) AndAlso Not String.IsNullOrEmpty(_enteredPANumber) Then
                    _RxBusinessLayer.PrescriptionCol.Item(row).PAReferenceID = getUniqueID()
                    _RxBusinessLayer.PrescriptionCol.Item(row).PriorAuthorizationNumber = _enteredPANumber
                    _RxBusinessLayer.PrescriptionCol.Item(row).PriorAuthorizationStatus = ""
                End If
                If ePAInsertUpdate.epa_IsManualPriorAuthorization(nRxModulePatientID, _RxBusinessLayer.PrescriptionCol.Item(row).PAReferenceID) Then
                    ePAInsertUpdate.ePA_INUPMaster(nRxModulePatientID, _RxBusinessLayer.ProviderID, _objFormularyToolBar.PBMMemberID, _RxBusinessLayer.PrescriptionCol.Item(row).mpid, _RxBusinessLayer.PrescriptionCol.Item(row).NDCCode, _RxBusinessLayer.PrescriptionCol.Item(row).PAReferenceID, "", "", days, qty, _enteredPANumber)
                    Me.RefreshActiveEPAList()
                    RaiseEvent PerformEPAStatusCheck(row)
                End If
            End Using
        End If

        'Me.PerformPDRStatusCheck(row)
    End Sub

    Private Sub objCustomPrescription_CloseClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles objCustomPrescription.CloseClick
        Try
            RemoveControl()
            sptRefill.Enabled = False
            pnlRefill.Visible = False

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

#End Region

#Region "Function - Custom Prescription"

    Private Sub AddControl()
        Dim DisableERXFields As Boolean = False

        Me.pnlRefill.Height = 323 ''fixed issue of panel size use to get resized after showing the new drug summary control
        If Not IsNothing(objCustomPrescription) Then
            RemoveControl()
        End If

        splRight.Visible = False
        pnlRight.Visible = False

        GloRxToolBarUserCtrl1.tStrpShowHide.Text = "Show"
        GloRxToolBarUserCtrl1.tStrpShowHide.ToolTipText = "Show Prescription History"

        Dim GridRowNo As Integer = _RxC1Flexgrid.GetRowNoFromGrid(_RxC1Flexgrid.SelectedRowIndex)

        If GridRowNo <> -1 Then

            If _RxBusinessLayer.PrescriptionCol.Count > 0 Then
                For i As Integer = 0 To _RxBusinessLayer.PrescriptionCol.Count - 1
                    If _RxBusinessLayer.PrescriptionCol.Item(i).ItemNumber = GridRowNo Then
                        If _RxBusinessLayer.PrescriptionCol.Item(i).MessageType = "RxChangeRequest" AndAlso (Me.RxChangeRequest.Type = SS.ChangeRequestType.GenericSubstitution Or Me.RxChangeRequest.Type = SS.ChangeRequestType.PriorAuthorizationRequired) Then
                            DisableERXFields = True
                        End If
                        Exit For
                    End If
                Next
            End If

            'CS#377101 integrated from 9000
            If _RxBusinessLayer.PrescriptionCol.Item(GridRowNo).mpid <> 0 And _RxBusinessLayer.PrescriptionCol.Item(GridRowNo).routes Is Nothing Then
                Dim RoutesList As New List(Of String)
                Using objPrescriptioLayer As New gloEMRGeneralLibrary.PrescriptionBusinessLayer()
                    RoutesList = objPrescriptioLayer.GetDrugRoutes(_RxBusinessLayer.PrescriptionCol.Item(GridRowNo).mpid)
                End Using

                If RoutesList IsNot Nothing Then
                    If RoutesList.Count > 2 Then
                        _RxBusinessLayer.PrescriptionCol.Item(GridRowNo).routes = RoutesList
                    End If
                End If
            End If

            objCustomPrescription = New CustomPrescription(_RxBusinessLayer, GridRowNo, DisableERXFields, nRxModulePatientID, "") ''_RxC1Flexgrid.RowIndex
            objCustomPrescription.ShowFrequencyAbbrevationInRxMeds = gblnIncludeFrequencyAbbrevationInRxMeds
            Me.pnlRefill.Controls.Add(objCustomPrescription)

            objCustomPrescription.Dock = DockStyle.Fill
            objCustomPrescription.Visible = True
            objCustomPrescription.BringToFront()
            pnlRefill.Visible = True

            If CheckRefillPanelhavingAnyCTRL() = True Then

            Else
                sptRefill.Enabled = True
                pnlRefill.Visible = True

            End If
        End If

    End Sub

    Private Sub RemoveDrugInfoControl()
        If Not IsNothing(_RxRefillC1Flexgrid) Then
            RemoveRefillControl()
        End If
    End Sub

    Public Sub RemoveChangeMed()
        Try
            For medCount As Integer = 0 To _MedBusinessLayer.MedicationCol.Count - 1
                If Not IsNothing(_MedBusinessLayer.MedicationCol.Item(medCount).CheckFlag) Then
                    If _MedBusinessLayer.MedicationCol.Item(medCount).CheckFlag.flag = gloTmpFlag.RefillMedication Then
                        If _MedBusinessLayer.MedicationCol.Item(medCount).Status.ToString = "Discontinued" Then
                            Dim isPresentInPrescription As Boolean = False
                            For prescriptionCount As Integer = 0 To _RxBusinessLayer.PrescriptionCol.Count - 1
                                If Not IsNothing(_RxBusinessLayer.PrescriptionCol.Item(prescriptionCount).CheckFlag) Then
                                    If _RxBusinessLayer.PrescriptionCol.Item(prescriptionCount).CheckFlag.flag = gloTmpFlag.RefillMedication And (_MedBusinessLayer.MedicationCol.Item(medCount).CheckFlag.keyVal.Key = _RxBusinessLayer.PrescriptionCol.Item(prescriptionCount).CheckFlag.keyVal.Key) Then
                                        isPresentInPrescription = True
                                    End If
                                End If
                            Next
                            If Not isPresentInPrescription Then
                                If _MedBusinessLayer.MedicationCol.Item(medCount).State = "A" Then
                                    _MedBusinessLayer.MedicationCol.Item(medCount).State = "A"
                                    _MedBusinessLayer.MedicationCol.Item(medCount).Status = _MedBusinessLayer.MedicationCol.Item(medCount).CheckFlag.keyVal.Value
                                Else
                                    _MedBusinessLayer.MedicationCol.Item(medCount).State = "M"
                                    _MedBusinessLayer.MedicationCol.Item(medCount).Status = _MedBusinessLayer.MedicationCol.Item(medCount).CheckFlag.keyVal.Value
                                End If
                            End If

                        End If
                    End If
                End If
            Next
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub RemoveControl()

        If Not IsNothing(objCustomPrescription) Then
            'SLR: it should be Me.pnlRefill.Controls...
            Me.pnlRefill.Controls.Remove(objCustomPrescription)
            Me.Controls.Remove(objCustomPrescription)
            objCustomPrescription.Visible = False
            objCustomPrescription.Dispose()
            objCustomPrescription = Nothing
            pnlRefill.Visible = False
        End If
        If Not IsNothing(_RxRefillC1Flexgrid) Then
            RemoveRefillControl()
        End If
    End Sub

#End Region

#Region "Formulary Controls"

    Private Sub C1Formulary_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try

            Dim c1FormlyNDCCode As String = ""
            Dim c1FormlyDrugName As String = ""
            Dim c1FormlyStatus As String = ""


        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.eRx, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub C1Formulary_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs)

    End Sub

    Private Sub btnFormularyGridClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFormularyGridClose.Click
        pnlFormulary.Visible = False
    End Sub

    Private Sub btnFormularyCovPnlClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFormularyCovPnlClose.Click

    End Sub

#Region "DMS panel move events"
    Private blnDMSPnlMoving As Boolean = False
    Private DMSPnlMouseDownX As Integer
    Private DMSPnlMouseDownY As Integer

    Private Sub pnlDMSHeader_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pnlDMSHeader.MouseDown
        Me.Cursor = Cursors.SizeAll
        If e.Button = MouseButtons.Left Then
            blnDMSPnlMoving = True
            DMSPnlMouseDownX = e.X
            DMSPnlMouseDownY = e.Y
        End If
    End Sub

    Private Sub pnlDMSHeader_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pnlDMSHeader.MouseUp
        If e.Button = MouseButtons.Left Then
            blnDMSPnlMoving = False
        End If
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub pnlDMSHeader_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pnlDMSHeader.MouseMove
        If blnDMSPnlMoving Then
            With pnlViewDocument
                Dim temp As Point = New Point
                temp.X = .Location.X + (e.X - DMSPnlMouseDownX)
                temp.Y = .Location.Y + (e.Y - DMSPnlMouseDownY)
                .Location = temp
                .BringToFront()
            End With
        End If
    End Sub

    Private Sub pnlViewDocumentFooter_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pnlViewDocumentFooter.MouseDown
        Me.Cursor = Cursors.SizeAll
        If e.Button = MouseButtons.Left Then
            blnDMSPnlMoving = True
            DMSPnlMouseDownX = e.X
            DMSPnlMouseDownY = e.Y
        End If
    End Sub

    Private Sub pnlViewDocumentFooter_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pnlViewDocumentFooter.MouseUp
        If e.Button = MouseButtons.Left Then
            blnDMSPnlMoving = False
        End If
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub pnlViewDocumentFooter_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pnlViewDocumentFooter.MouseMove
        If blnDMSPnlMoving Then
            With pnlViewDocument
                Dim temp As Point = New Point
                temp.X = .Location.X + (e.X - DMSPnlMouseDownX)
                temp.Y = .Location.Y + (e.Y - DMSPnlMouseDownY)
                .Location = temp
                .BringToFront()
            End With
        End If
    End Sub

#End Region

#Region "Medication History user control"
    Private blnMedHistPnlMoving As Boolean = False
    Private MedHistPnlMouseDownX As Integer
    Private MedHistPnlMouseDownY As Integer

    Private Sub pnlMedicationHistoryHeader_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pnlMedicationHistoryHeader.MouseDown
        Me.Cursor = Cursors.SizeAll
        If e.Button = MouseButtons.Left Then
            blnMedHistPnlMoving = True
            MedHistPnlMouseDownX = e.X
            MedHistPnlMouseDownY = e.Y
        End If
    End Sub

    Private Sub pnlMedicationHistoryHeader_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pnlMedicationHistoryHeader.MouseUp
        If e.Button = MouseButtons.Left Then
            blnMedHistPnlMoving = False
        End If
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub pnlMedicationHistoryHeader_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pnlMedicationHistoryHeader.MouseMove
        If blnMedHistPnlMoving Then
            With pnlViewMedicationHistory
                Dim temp As Point = New Point
                temp.X = .Location.X + (e.X - MedHistPnlMouseDownX)
                temp.Y = .Location.Y + (e.Y - MedHistPnlMouseDownY)
                .Location = temp
                .BringToFront()
            End With
        End If
    End Sub

    Private Sub pnlMedicationHistoryFooter_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pnlMedicationHistoryFooter.MouseDown
        Me.Cursor = Cursors.SizeAll
        If e.Button = MouseButtons.Left Then
            blnMedHistPnlMoving = True
            MedHistPnlMouseDownX = e.X
            MedHistPnlMouseDownY = e.Y
        End If
    End Sub

    Private Sub pnlMedicationHistoryFooter_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pnlMedicationHistoryFooter.MouseUp
        If e.Button = MouseButtons.Left Then
            blnMedHistPnlMoving = False
        End If
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub pnlMedicationHistoryFooter_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pnlMedicationHistoryFooter.MouseMove
        If blnMedHistPnlMoving Then
            With pnlViewMedicationHistory
                Dim temp As Point = New Point
                temp.X = .Location.X + (e.X - MedHistPnlMouseDownX)
                temp.Y = .Location.Y + (e.Y - MedHistPnlMouseDownY)
                .Location = temp
                .BringToFront()
            End With
        End If
    End Sub

#End Region

#Region "Formulary Controls Mouse Move Events"
    Private blnFormularyGrdMoving As Boolean = False
    Private FormularyGrdMouseDownX As Integer
    Private FormularyGrdMouseDownY As Integer

    Private blnFormularyCoveragePnlMoving As Boolean = False
    Private FormularyCovPnlMouseDownX As Integer
    Private FormularyCovPnlMouseDownY As Integer

    Private blnFormularyIndicatorPnlMoving As Boolean = False
    Private FormularyIndicatorPnlMouseDownX As Integer
    Private FormularyIndicatorPnlMouseDownY As Integer

    Private Sub pnlFormularyDrugName_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pnlFormularyDrugName.MouseDown
        Me.Cursor = Cursors.SizeAll
        If e.Button = MouseButtons.Left Then
            blnFormularyGrdMoving = True
            FormularyGrdMouseDownX = e.X
            FormularyGrdMouseDownY = e.Y
        End If
    End Sub

    Private Sub pnlFormularyDrugName_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pnlFormularyDrugName.MouseUp
        If e.Button = MouseButtons.Left Then
            blnFormularyGrdMoving = False
        End If
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub pnlFormularyDrugName_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pnlFormularyDrugName.MouseMove
        If blnFormularyGrdMoving Then
            With pnlFormulary
                Dim temp As Point = New Point
                temp.X = .Location.X + (e.X - FormularyGrdMouseDownX)
                temp.Y = .Location.Y + (e.Y - FormularyGrdMouseDownY)
                .Location = temp
                .BringToFront()
            End With
        End If
    End Sub

    Private Sub lblFormularyDrugName_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lblFormularyDrugName.MouseDown
        Me.Cursor = Cursors.SizeAll
        If e.Button = MouseButtons.Left Then
            blnFormularyGrdMoving = True
            FormularyGrdMouseDownX = e.X
            FormularyGrdMouseDownY = e.Y
        End If
    End Sub

    Private Sub lblFormularyDrugName_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lblFormularyDrugName.MouseUp
        If e.Button = MouseButtons.Left Then
            blnFormularyGrdMoving = False
        End If
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub lblFormularyDrugName_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lblFormularyDrugName.MouseMove
        If blnFormularyGrdMoving Then
            With pnlFormulary
                Dim temp As Point = New Point
                temp.X = .Location.X + (e.X - FormularyGrdMouseDownX)
                temp.Y = .Location.Y + (e.Y - FormularyGrdMouseDownY)
                .Location = temp
                .BringToFront()
            End With
        End If
    End Sub

    Private Sub pnlFormularyCoverageHeading_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pnlElementHostCopay.MouseDown
        Me.Cursor = Cursors.SizeAll
        If e.Button = MouseButtons.Left Then
            blnFormularyCoveragePnlMoving = True
            FormularyCovPnlMouseDownX = e.X
            FormularyCovPnlMouseDownY = e.Y
        End If
    End Sub

    Private Sub pnlFormularyCoverageHeading_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pnlElementHostCopay.MouseUp
        If e.Button = MouseButtons.Left Then
            blnFormularyCoveragePnlMoving = False
        End If
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub pnlFormularyCoverageHeading_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pnlElementHostCopay.MouseMove
        If blnFormularyCoveragePnlMoving Then
            With pnlElementHostCopay
                Dim temp As Point = New Point
                temp.X = .Location.X + (e.X - FormularyCovPnlMouseDownX)
                temp.Y = .Location.Y + (e.Y - FormularyCovPnlMouseDownY)
                .Location = temp
                .BringToFront()
            End With
        End If
    End Sub

    Private Sub lblAlternativeDrugName_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lblCoverageCopayHeading.MouseDown
        Me.Cursor = Cursors.SizeAll
        If e.Button = MouseButtons.Left Then
            blnFormularyCoveragePnlMoving = True
            FormularyCovPnlMouseDownX = e.X
            FormularyCovPnlMouseDownY = e.Y
        End If
    End Sub

    Private Sub lblAlternativeDrugName_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lblCoverageCopayHeading.MouseUp
        If e.Button = MouseButtons.Left Then
            blnFormularyCoveragePnlMoving = False
        End If
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub lblAlternativeDrugName_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lblCoverageCopayHeading.MouseMove
        If blnFormularyCoveragePnlMoving Then
            With pnlElementHostCopay
                Dim temp As Point = New Point
                temp.X = .Location.X + (e.X - FormularyCovPnlMouseDownX)
                temp.Y = .Location.Y + (e.Y - FormularyCovPnlMouseDownY)
                .Location = temp
                .BringToFront()
            End With
        End If
    End Sub

    Private Sub rtfFormularyDescription_LinkClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.LinkClickedEventArgs) Handles rtfFormularyDescription.LinkClicked
        Try
            System.Diagnostics.Process.Start(e.LinkText)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try

    End Sub

#End Region

#End Region

#Region "Prescription HISTORY Treeview User Control"
    Private Sub _RxHistoryUserCtrl_Recordlock(ByVal blnRecordLock As Boolean) Handles _RxHistoryUserCtrl.Recordlock
        If blnRecordLock = True Then
            GloRxToolBarUserCtrl1.tStrpSave.Enabled = False
            GloRxToolBarUserCtrl1.tStrpPrint.Enabled = False
            GloRxToolBarUserCtrl1.tStrpFax.Enabled = False

        Else
            GloRxToolBarUserCtrl1.tStrpSave.Enabled = True
            GloRxToolBarUserCtrl1.tStrpPrint.Enabled = True
            GloRxToolBarUserCtrl1.tStrpFax.Enabled = True
        End If
    End Sub

    Private Sub _RxHistoryUserCtrl_PrescriptionLoaded() Handles _RxHistoryUserCtrl.PrescriptionLoaded
        Try
            If _RxBusinessLayer.PharmacyId <> 0 Then
                Dim NCPDPIDPharmStatus As String = _RxBusinessLayer.GetNCPDPID_PharmStatus(_RxBusinessLayer.PharmacyId.ToString)

                Dim Retval() As String
                Dim NCPDPID_PharmStat As String = NCPDPIDPharmStatus
                Retval = NCPDPID_PharmStat.Split("|")

                Dim NCPDPID As String = Retval(0) 'will contain the external code
                Dim PharmacyStatus As String = Retval(1) ''will contain the Pharmacy Status

                If NCPDPID = "" Then
                    GloRxToolBarUserCtrl1.tStrpFax.Enabled = True
                    GloRxToolBarUserCtrl1.tStrpERx.Enabled = False
                    IsPharmacyEnabled = False
                Else
                    GloRxToolBarUserCtrl1.tStrpFax.Enabled = gblnIsFaxEnabled
                    GloRxToolBarUserCtrl1.tStrpERx.Enabled = True
                    IsPharmacyEnabled = True
                End If

                If PharmacyStatus = "D" Then
                    IsPharmacyActivated = False
                Else
                    IsPharmacyActivated = True
                End If

            Else
                GloRxToolBarUserCtrl1.tStrpFax.Enabled = True
                GloRxToolBarUserCtrl1.tStrpERx.Enabled = False
                IsPharmacyEnabled = False
                IsPharmacyActivated = False
            End If

            If gblnSurescriptEnabled Then
                GloRxToolBarUserCtrl1.tStrpERx.Visible = True
            Else
                GloRxToolBarUserCtrl1.tStrpERx.Visible = False
                GloRxToolBarUserCtrl1.tStrpFax.Enabled = True
            End If


            If gblnClinicDISetting Then
                UnloadDrugInteractionControl()
                If Not IsNothing(objDrugInteraction) Then
                    objDrugInteraction.RefreshToolBar()
                    pnlDI.Width = 370
                End If
            End If

            BindFlexGrid_RX()

            _RxPatientStrip.DTPValue = _RxBusinessLayer.Visitdate
            _RxPatientStrip.DTPEnabled = False
            If Not IsNothing(_RxPatientStrip) Then
                _RxPatientStrip.Dispose()
                _RxPatientStrip = Nothing
            End If
            If Not IsNothing(oProvider) Then
                oProvider.Dispose()
                oProvider = Nothing
            End If
            oProvider = New clsProvider
            If oProvider.IsProvider_Senior(_RxBusinessLayer.ProviderID) = False Then
                _RxPatientStrip.ShowRxProviderAssociation = RxBusinesslayer.GetRxProviderAssociationSettings
            Else
                _RxPatientStrip.ShowRxProviderAssociation = False
            End If
            'oProvider.Dispose()
            'oProvider = Nothing
            _RxPatientStrip.ShowDetail(nRxModulePatientID, gloUC_PatientStrip.enumFormName.Prescription, , _RxBusinessLayer.CurrentVisitID, _RxBusinessLayer.ProviderID, True, False, True, _RxBusinessLayer.PrescriptionCol.Item(0).PrescriptionID.ToString, IsPharmacyEnabled)

            _RxPatientStrip.HideButton = False

            RefRequest = Nothing
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.ModifyPrescription, gloAuditTrail.ActivityType.Modify, "Presciption opened for modification", nRxModulePatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub _RxHistoryUserCtrl_PrescriptionUnLoaded() Handles _RxHistoryUserCtrl.PrescriptionUnLoaded
        Try

            If _RxBusinessLayer.PharmacyId <> 0 Then

                Dim NCPDPIDPharmStatus As String = _RxBusinessLayer.GetNCPDPID_PharmStatus(_RxBusinessLayer.PharmacyId.ToString)

                Dim Retval() As String
                Dim NCPDPID_PharmStat As String = NCPDPIDPharmStatus
                Retval = NCPDPID_PharmStat.Split("|")

                Dim NCPDPID As String = Retval(0)
                Dim PharmacyStatus As String = Retval(1)

                If PharmacyStatus = "D" Then
                    IsPharmacyActivated = False
                Else
                    IsPharmacyActivated = True
                End If

                If NCPDPID = "" Then
                    GloRxToolBarUserCtrl1.tStrpFax.Enabled = True
                    GloRxToolBarUserCtrl1.tStrpERx.Enabled = False
                    IsPharmacyEnabled = False
                Else
                    GloRxToolBarUserCtrl1.tStrpFax.Enabled = gblnIsFaxEnabled
                    GloRxToolBarUserCtrl1.tStrpERx.Enabled = True
                    IsPharmacyEnabled = True
                End If
            Else
                GloRxToolBarUserCtrl1.tStrpFax.Enabled = True
                GloRxToolBarUserCtrl1.tStrpERx.Enabled = False
                IsPharmacyEnabled = False
                IsPharmacyActivated = False
            End If
            If gblnSurescriptEnabled Then
                GloRxToolBarUserCtrl1.tStrpERx.Visible = True
            Else
                GloRxToolBarUserCtrl1.tStrpERx.Visible = False
                GloRxToolBarUserCtrl1.tStrpFax.Enabled = True
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

#End Region

#Region "Drug Interaction User Control"

    Private Sub objDIScreenResults_SavePatientEducation() Handles objScreeningResults.SavePatient_Education
        Try
            Dim _DocumentName As String = ExamNewDocumentName
            Dim oWord As New gloEMRWord.clsWordDocument

            Dim myLoadWord As gloWord.LoadAndCloseWord = New gloWord.LoadAndCloseWord()
            Try
                Dim oTempDoc As wd.Document = myLoadWord.LoadWordApplication(Nothing, False)
                'SLR : Added since setClipboard without getClipboard !
                Try
                    Global.gloWord.gloWord.GetClipboardData()
                Catch ex1 As Exception

                End Try

                Try
                    oTempDoc.Application.Selection.Paste()
                Catch ex2 As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.Close, ex2.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    ex2 = Nothing

                End Try
                'Clipboard.Clear()
                'SLR : Is it needed to setClipboard without getClipboard and why?
                Try
                    Global.gloWord.gloWord.SetClipboardData()
                Catch ex1 As Exception

                End Try

                'wdTemp.Save(_DocumentName, True, "", "")
                'wdTemp.Close()
                oTempDoc.SaveAs(_DocumentName, wd.WdSaveFormat.wdFormatXMLDocument, False, "", False)
                myLoadWord.CloseWordOnly(oTempDoc)
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                ex = Nothing
            End Try
            myLoadWord.CloseApplicationOnly()
            myLoadWord = Nothing

            Dim strDrugName As String = ""
            Dim strDosage As String = ""
            Dim strDrugForm As String = ""
            Dim strNDCCode As String = ""

            If _RxBusinessLayer.PrescriptionCol.Count > 0 Then
                strDrugName = _RxBusinessLayer.PrescriptionCol.Item(_RxC1Flexgrid.SelectedRowIndex - 1).Medication
                strDosage = _RxBusinessLayer.PrescriptionCol.Item(_RxC1Flexgrid.SelectedRowIndex - 1).Dosage
                strDrugForm = _RxBusinessLayer.PrescriptionCol.Item(_RxC1Flexgrid.SelectedRowIndex - 1).DosageForm
                strNDCCode = _RxBusinessLayer.PrescriptionCol.Item(_RxC1Flexgrid.SelectedRowIndex - 1).NDCCode
            End If
            Dim strDrug As String = strDrugName & " " & strDosage & " " & strDrugForm

            Dim oPatientEducation As New clsPatientEducation
            oPatientEducation.SaveExamEducation(GenerateVisitID(nRxModulePatientID), nRxModulePatientID, 0, CType(oWord.ConvertFiletoBinary(_DocumentName), Object), "Medication - " & strDrug, 2, 1, 1, "", "", "")
            'SLR: Dispsoe and then
            oPatientEducation.Dispose()
            oPatientEducation = Nothing
            'SLR: Free oword
            If Not oWord Is Nothing Then
                oWord = Nothing
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing

        Finally

        End Try
    End Sub

#End Region

#Region "gloUC_CustomSearchInC1Flexgrid User Control - CheifComplaints / Sig Control "
    Private Sub objChiefcomplaints_btnUC_Cancelclick(ByVal sender As Object, ByVal e As System.EventArgs) Handles objChiefcomplaints.btnUC_Cancelclick
        Try
            If Not IsNothing(objChiefcomplaints) Then
                Me.Controls.Remove(objChiefcomplaints)
                objChiefcomplaints.Visible = False
                'SLR: Remove handler and then dispose and then
                objChiefcomplaints.Dispose()
                objChiefcomplaints = Nothing
                rowindex = 0
            End If
        Catch ex As Exception

            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try

    End Sub

    Private Sub objChiefcomplaints__FlexDoubleClick(ByVal sender As Object, ByVal e As System.EventArgs, ByVal _bSelectFlag As Boolean) Handles objChiefcomplaints._FlexDoubleClick
        Try
            SetSigDetails()
        Catch ex As Exception

            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub objChiefcomplaints_btnUC_OKclick(ByVal sender As Object, ByVal e As System.EventArgs) Handles objChiefcomplaints.btnUC_OKclick
        Try
            SetSigDetails()
        Catch ex As Exception

            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub objSigControl_btnUC_Modify_click(ByVal sender As Object, ByVal e As System.EventArgs) Handles objSigControl.btnUC_Modify_click

        Dim nSigId As Long

        Try
            If GridStatus = 1 Then 'custom control loaded for Sig data
                If dt.Rows.Count > 0 Then
                    If rowindex <= ReferralCount Then
                        If Not IsDBNull(objSigControl) Then
                            nSigId = objSigControl._UCflex.GetData(objSigControl._UCflex.Row, "nSigId")
                        End If
                    End If
                End If
                Dim objfrmMSTSIG As New frmMSTSIG(nSigId)

                objfrmMSTSIG.Text = "Modify SIG"
                objfrmMSTSIG.ShowDialog(IIf(IsNothing(objfrmMSTSIG.Parent), Me, objfrmMSTSIG.Parent))
                sigid = nSigId
                'SLR: dispose objfrmMSTSIG
                objfrmMSTSIG.Dispose()
                objfrmMSTSIG = Nothing
                'Developer:Pradeep 
                'Date:1/20/2012
                'Bug ID: 19417
                'Reason:exception occured 
                If clsgeneral.blnIsProviderSpecificDrugsBtnSelected = True Then
                    objCustomPrescription_SigClick_DrugProvAsso(sender, e, objCustomPrescription.Caption)
                Else
                    objCustomPrescription_SigClick(sender, e)
                End If


            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.Modify, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub objSigControl__FlexDoubleClick(ByVal sender As Object, ByVal e As System.EventArgs, ByVal _bSelectFlag As Boolean) Handles objSigControl._FlexDoubleClick
        Try
            SetSigDetails()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub objSigControl_btnUC_ADDclick(ByVal sender As Object, ByVal e As System.EventArgs) Handles objSigControl.btnUC_ADDclick
        Dim objfrmMSTSIG As frmMSTSIG

        Try
            If GridStatus = 1 Then
                objfrmMSTSIG = New frmMSTSIG
                objfrmMSTSIG.Text = "Add New SIG"
                objfrmMSTSIG.ShowDialog(IIf(IsNothing(objfrmMSTSIG.Parent), Me, objfrmMSTSIG.Parent))
                sigid = objfrmMSTSIG.SigId
                'SLR: dispose objfrmMSTSIG
                objfrmMSTSIG.Dispose()
                objfrmMSTSIG = Nothing
                'Developer:Pradeep 
                'Date:1/20/2012
                'Bug ID: 19417
                'Reason:exception occured 
                If clsgeneral.blnIsProviderSpecificDrugsBtnSelected = True Then
                    objCustomPrescription_SigClick_DrugProvAsso(sender, e, objCustomPrescription.Caption)
                Else
                    objCustomPrescription_SigClick(sender, e)
                End If

            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally
            objfrmMSTSIG = Nothing
        End Try
    End Sub

    Private Sub objSigControl_btnUC_Cancelclick(ByVal sender As Object, ByVal e As System.EventArgs) Handles objSigControl.btnUC_Cancelclick
        Try

            If Not IsNothing(objSigControl) Then
                Me.Controls.Remove(objSigControl)
                'SLR: ALso it was part of panel also. Hence remove from Panel also
                Me.pnlRefill.Controls.Remove(objSigControl)
                objSigControl.Visible = False
                'SLR: Remove handler and then dispose and then
                objSigControl.Dispose()
                objSigControl = Nothing
                rowindex = 0
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub
#End Region

#Region "Patient Strip User Control"
    Private Sub _RxPatientStrip_ControlSizeChanged() Handles _RxPatientStrip.ControlSizeChanged
        Try
            pnlcentertop.Height = _RxPatientStrip.Height
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try

    End Sub
#End Region

#Region "Prescription Drug Treeview User Control"

    Private Sub UpdateRxPharmacyInfo()
        Dim _PharmacyId As Int64 = _RxBusinessLayer.PharmacyId

        If RxChangeRequest IsNot Nothing AndAlso Me.RxChangeRequest.Type = gloGlobal.SS.ChangeRequestType.TherapeuticSubstitution Then
            If Me.RxChangeRequest.MedRequested Is Nothing Then
                _PharmacyId = Me.RxChangeRequest.PharmacyID
            End If
        End If

        If _PharmacyId <> 0 Then
            Dim _dtPharmDetails As DataTable = _RxBusinessLayer.GetPharmacyDetails(_PharmacyId)
            If Not IsNothing(_dtPharmDetails) Then
                If _dtPharmDetails.Rows.Count > 0 Then

                    If Not IsNothing(_dtPharmDetails.Rows(0)("sNCPDPID")) AndAlso Not IsDBNull(_dtPharmDetails.Rows(0)("sNCPDPID")) Then
                        _RxBusinessLayer.PrescriptionCol.Item(_RxBusinessLayer.PrescriptionCol.Count - 1).PhNCPDPID = _dtPharmDetails.Rows(0)("sNCPDPID")
                    Else
                        _RxBusinessLayer.PrescriptionCol.Item(_RxBusinessLayer.PrescriptionCol.Count - 1).PhNCPDPID = ""
                    End If
                    If Not IsNothing(_dtPharmDetails.Rows(0)("nContactID")) AndAlso Not IsDBNull(_dtPharmDetails.Rows(0)("nContactID")) Then
                        _RxBusinessLayer.PrescriptionCol.Item(_RxBusinessLayer.PrescriptionCol.Count - 1).PhContactID = _dtPharmDetails.Rows(0)("nContactID")
                    Else
                        _RxBusinessLayer.PrescriptionCol.Item(_RxBusinessLayer.PrescriptionCol.Count - 1).PhContactID = 0
                    End If
                    If Not IsNothing(_dtPharmDetails.Rows(0)("sName")) AndAlso Not IsDBNull(_dtPharmDetails.Rows(0)("sName")) Then
                        _RxBusinessLayer.PrescriptionCol.Item(_RxBusinessLayer.PrescriptionCol.Count - 1).PharmacyName = _dtPharmDetails.Rows(0)("sName")
                    Else
                        _RxBusinessLayer.PrescriptionCol.Item(_RxBusinessLayer.PrescriptionCol.Count - 1).PharmacyName = ""
                    End If
                    If Not IsNothing(_dtPharmDetails.Rows(0)("sAddressLine1")) AndAlso Not IsDBNull(_dtPharmDetails.Rows(0)("sAddressLine1")) Then
                        _RxBusinessLayer.PrescriptionCol.Item(_RxBusinessLayer.PrescriptionCol.Count - 1).PhAddressline1 = _dtPharmDetails.Rows(0)("sAddressLine1")
                    Else
                        _RxBusinessLayer.PrescriptionCol.Item(_RxBusinessLayer.PrescriptionCol.Count - 1).PhAddressline1 = ""
                    End If
                    If Not IsNothing(_dtPharmDetails.Rows(0)("sAddressLine2")) AndAlso Not IsDBNull(_dtPharmDetails.Rows(0)("sAddressLine2")) Then
                        _RxBusinessLayer.PrescriptionCol.Item(_RxBusinessLayer.PrescriptionCol.Count - 1).PhAddressline2 = _dtPharmDetails.Rows(0)("sAddressLine2")
                    Else
                        _RxBusinessLayer.PrescriptionCol.Item(_RxBusinessLayer.PrescriptionCol.Count - 1).PhAddressline2 = ""
                    End If

                    If Not IsNothing(_dtPharmDetails.Rows(0)("sCity")) AndAlso Not IsDBNull(_dtPharmDetails.Rows(0)("sCity")) Then
                        _RxBusinessLayer.PrescriptionCol.Item(_RxBusinessLayer.PrescriptionCol.Count - 1).PhCity = _dtPharmDetails.Rows(0)("sCity")
                    Else
                        _RxBusinessLayer.PrescriptionCol.Item(_RxBusinessLayer.PrescriptionCol.Count - 1).PhCity = ""
                    End If

                    If Not IsNothing(_dtPharmDetails.Rows(0)("sState")) AndAlso Not IsDBNull(_dtPharmDetails.Rows(0)("sState")) Then
                        _RxBusinessLayer.PrescriptionCol.Item(_RxBusinessLayer.PrescriptionCol.Count - 1).PhState = _dtPharmDetails.Rows(0)("sState")
                    Else
                        _RxBusinessLayer.PrescriptionCol.Item(_RxBusinessLayer.PrescriptionCol.Count - 1).PhState = ""
                    End If
                    If Not IsNothing(_dtPharmDetails.Rows(0)("sZIP")) AndAlso Not IsDBNull(_dtPharmDetails.Rows(0)("sZIP")) Then
                        _RxBusinessLayer.PrescriptionCol.Item(_RxBusinessLayer.PrescriptionCol.Count - 1).PhZip = _dtPharmDetails.Rows(0)("sZIP")
                    Else
                        _RxBusinessLayer.PrescriptionCol.Item(_RxBusinessLayer.PrescriptionCol.Count - 1).PhZip = ""
                    End If

                    If Not IsNothing(_dtPharmDetails.Rows(0)("sEmail")) AndAlso Not IsDBNull(_dtPharmDetails.Rows(0)("sEmail")) Then
                        _RxBusinessLayer.PrescriptionCol.Item(_RxBusinessLayer.PrescriptionCol.Count - 1).PhEmail = _dtPharmDetails.Rows(0)("sEmail")
                    Else
                        _RxBusinessLayer.PrescriptionCol.Item(_RxBusinessLayer.PrescriptionCol.Count - 1).PhEmail = ""
                    End If
                    If Not IsNothing(_dtPharmDetails.Rows(0)("sFax")) AndAlso Not IsDBNull(_dtPharmDetails.Rows(0)("sFax")) Then
                        _RxBusinessLayer.PrescriptionCol.Item(_RxBusinessLayer.PrescriptionCol.Count - 1).PhFax = _dtPharmDetails.Rows(0)("sFax")
                    Else
                        _RxBusinessLayer.PrescriptionCol.Item(_RxBusinessLayer.PrescriptionCol.Count - 1).PhFax = ""
                    End If
                    If Not IsNothing(_dtPharmDetails.Rows(0)("sPhone")) AndAlso Not IsDBNull(_dtPharmDetails.Rows(0)("sFax")) Then
                        _RxBusinessLayer.PrescriptionCol.Item(_RxBusinessLayer.PrescriptionCol.Count - 1).PhPhone = _dtPharmDetails.Rows(0)("sPhone")
                    Else
                        _RxBusinessLayer.PrescriptionCol.Item(_RxBusinessLayer.PrescriptionCol.Count - 1).PhPhone = ""
                    End If
                    If Not IsNothing(_dtPharmDetails.Rows(0)("sServiceLevel")) AndAlso Not IsDBNull(_dtPharmDetails.Rows(0)("sServiceLevel")) Then
                        _RxBusinessLayer.PrescriptionCol.Item(_RxBusinessLayer.PrescriptionCol.Count - 1).PhServiceLevel = _dtPharmDetails.Rows(0)("sServiceLevel")
                    Else
                        _RxBusinessLayer.PrescriptionCol.Item(_RxBusinessLayer.PrescriptionCol.Count - 1).PhServiceLevel = ""
                    End If

                End If

            End If
            _dtPharmDetails.Dispose()
            _dtPharmDetails = Nothing
        End If
    End Sub

    Private Sub NewPrescriptionAdded()

        RaiseEvent PerformEPAStatusCheck(_RxBusinessLayer.PrescriptionCol.Count - 1)

        RaiseEvent PerformDrugAlertCheck(True)

        UpdateRxPharmacyInfo()

        If _RxBusinessLayer.PrescriptionCol.Count > 0 Then
            RemoveHandler _RxC1Flexgrid._FlexSelChange, AddressOf _RxC1Flexgrid__FlexSelChange

            'Me.CheckChangeRequestDrugAdded()



            'objPrescription.MessageType = "RxChangeRequest"

            If _RxBusinessLayer.PrescriptionCol.Item(_RxBusinessLayer.PrescriptionCol.Count - 1).NDCCode.Contains("GLO") Then
                System.Windows.Forms.MessageBox.Show("Formulary data of the prescribed drug " & _RxBusinessLayer.PrescriptionCol.Item(_RxBusinessLayer.PrescriptionCol.Count - 1).Medication & " will not be shown since the NDCCode " & _RxBusinessLayer.PrescriptionCol.Item(_RxBusinessLayer.PrescriptionCol.Count - 1).NDCCode & " is invalid", "gloEMR", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Information)
            End If


            If _objFormularyToolBar IsNot Nothing Then
                _RxC1Flexgrid.AddNewPrescription(_RxBusinessLayer.PrescriptionCol.Item(_RxBusinessLayer.PrescriptionCol.Count - 1), _objFormularyToolBar.CurrentEligibilityStatus)

            Else
                _RxC1Flexgrid.AddNewPrescription(_RxBusinessLayer.PrescriptionCol.Item(_RxBusinessLayer.PrescriptionCol.Count - 1), RxBusinesslayer.EligibilityStatus.NotChecked)
            End If

            ''TODO:Refactoring needed
            Try
                Dim lstProblems As New ArrayList
                Dim lstChiefComplaints As New ArrayList

                For Each item As DataRow In patientProblems.Rows
                    lstProblems.Add(item(0))
                    lstChiefComplaints.Add(item("Complaint"))
                Next

                _RxBusinessLayer.PrescriptionCol.Item(_RxBusinessLayer.PrescriptionCol.Count - 1).Problems = String.Join("|", lstProblems.ToArray())
                _RxBusinessLayer.PrescriptionCol.Item(_RxBusinessLayer.PrescriptionCol.Count - 1).ChiefComplaint = String.Join("|", lstChiefComplaints.ToArray())

            Catch ex As Exception

            End Try
            'GloRxToolBarUserCtrl1.tStrpCancelRx.Enabled = False

            AddHandler _RxC1Flexgrid._FlexSelChange, AddressOf _RxC1Flexgrid__FlexSelChange

        End If

        If gblnLoadFormularyDrugs = True Then
            lblFormularyDrugName.Text = "Alternatives for " & _RxBusinessLayer.PrescriptionCol.Item(_RxBusinessLayer.PrescriptionCol.Count - 1).Medication
            lblAlternativeDrugName.Text = "Cov/Copay: " & _RxBusinessLayer.PrescriptionCol.Item(_RxBusinessLayer.PrescriptionCol.Count - 1).Medication
        End If

        gloEMRGeneralLibrary.gloGeneral.clsgeneral.blnIsRxMedsFormEdited = True

        If gloEMRGeneralLibrary.gloGeneral.clsgeneral.gblnCustomPrescEdited = False Then
            gloEMRGeneralLibrary.gloGeneral.clsgeneral.gblnCustomPrescEdited = True
        End If

    End Sub

    Private Sub NewMedicationAdded()
        Try


            _MxC1Flexgrid.cmbMedStatus.Text = "Active"

            If blncancel = True And IsOpenedFromPrescription = False Then

                ' ''commented because when if  past med is present and we open the form and add med item  then 
                ' ''if form is kept opened in background and we go to dashboard/exam and try to open the form then it was not opening the form
                ' ''blncancel = False 
                'RaiseEvent PerformDrugAlertCheck(True)

                If _MedBusinessLayer.MedicationCol.Count > 0 Then
                    _MxC1Flexgrid.AddandSetFlexgridData(_MedBusinessLayer.MedicationCol.Item(_MedBusinessLayer.MedicationCol.Count - 1))
                    _nAddDrugtoMx = 1

                End If

                If Not IsNothing(objCustomMedication) Then
                    RemoveControl()
                End If

                gloEMRGeneralLibrary.gloGeneral.clsgeneral.blnIsRxMedsFormEdited = True

                Exit Sub
            End If

            If _nAddDrugtoMx > 0 Then
                '   RaiseEvent PerformDrugAlertCheck(True)

                If _MedBusinessLayer.MedicationCol.Count > 0 Then
                    _MxC1Flexgrid.AddandSetFlexgridData(_MedBusinessLayer.MedicationCol.Item(_MedBusinessLayer.MedicationCol.Count - 1))
                End If

                If Not IsNothing(objCustomMedication) Then
                    RemoveControl()
                End If

                gloEMRGeneralLibrary.gloGeneral.clsgeneral.blnIsRxMedsFormEdited = True

                Exit Sub
            End If

            If MessageBox.Show("Are you sure you want to add this item to the patient’s Medication History?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                '   RaiseEvent PerformDrugAlertCheck(True)
                _nAddDrugtoMx = _nAddDrugtoMx + 1
                If _MedBusinessLayer.MedicationCol.Count > 0 Then ' If Condition Added by chetan to avoid blank row to inserted in flexgrid
                    _MxC1Flexgrid.AddandSetFlexgridData(_MedBusinessLayer.MedicationCol.Item(_MedBusinessLayer.MedicationCol.Count - 1))


                End If

                If Not IsNothing(objCustomMedication) Then
                    RemoveControl()
                End If

                gloEMRGeneralLibrary.gloGeneral.clsgeneral.blnIsRxMedsFormEdited = True ''''''''means Rx-Meds form is edited and we should prompt the user if he directly Clicks the Close button
                If gloEMRGeneralLibrary.gloGeneral.clsgeneral.gblnCustomMediEdited = False Then
                    gloEMRGeneralLibrary.gloGeneral.clsgeneral.gblnCustomMediEdited = True
                End If
            Else
                Dim MxRwCount As Integer = _MxC1Flexgrid.GetMedicationFlexRowCount()
                If MxRwCount > 1 Then
                    _MedBusinessLayer.MedicationCol.Remove(MxRwCount - 1)

                Else
                    _MedBusinessLayer.MedicationCol.Clear()

                    Exit Sub
                End If

            End If



        Catch ex As Exception
        Finally
            SetNKPrescriptionVisibility()
        End Try


    End Sub

    'Private Sub _RxListUserCtrl_trvDoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles _RxListUserCtrl.trvDoubleClick
    '    'Try
    '    '    If FormLock = True Then
    '    '        Exit Sub
    '    '    End If

    '    '    If _RxListUserCtrl.ValidDrug Then

    '    '        If Not IsNothing(objCustomPrescription) Then
    '    '            RemoveControl()
    '    '        End If

    '    '        If Not IsNothing(objCustomMedication) Then
    '    '            RemoveCustomMedicationControl()
    '    '        End If

    '    '        RemoveMedRefillControl()
    '    '        RemoveRefillControl()

    '    '        If blnRxC1FlexClick = True Then
    '    '            NewPrescriptionAdded()
    '    '        Else
    '    '            NewMedicationAdded()
    '    '        End If
    '    '    End If

    '    '    RemoveSplitters_Pres()

    '    '    If pnlPrescriptionGrid.Top > pnlMedicationGrid.Top Then
    '    '        pnlPrescriptionGrid.Dock = DockStyle.Fill
    '    '        pnlMedicationGrid.Dock = DockStyle.Top
    '    '        pnlRefill.SendToBack()
    '    '    Else
    '    '        pnlPrescriptionGrid.Dock = DockStyle.Top
    '    '        pnlMedicationGrid.Dock = DockStyle.Fill
    '    '        pnlRefill.SendToBack()
    '    '    End If

    '    'Catch ex As gloUserControlExceptions
    '    '    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
    '    '    ex = Nothing
    '    'Catch ex As Exception
    '    '    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
    '    '    ex = Nothing
    '    'End Try
    'End Sub

    Private Sub _RxListUserCtrl_cntListmenuStripClick(ByVal selectedButon As Boolean) Handles _RxListUserCtrl.cntListmenuStripClick
        Try
            If selectedButon Then
                Dim objfrmDrugProviderAssociation As New frmDrugProviderAssociation(nRxModulePatientID, _RxBusinessLayer.ProviderID)
                objfrmDrugProviderAssociation.Text = "Add Provider Favorite drugs"
                objfrmDrugProviderAssociation.ShowDialog(IIf(IsNothing(objfrmDrugProviderAssociation.Parent), Me, objfrmDrugProviderAssociation.Parent))
                objfrmDrugProviderAssociation.Dispose()
                objfrmDrugProviderAssociation = Nothing
                _RxListUserCtrl.DisplayDrugList()
            Else
                Dim objfrmMSTDrugs As New frmMSTDrugs
                objfrmMSTDrugs.Text = "Add New Drugs"
                objfrmMSTDrugs.ShowDialog(IIf(IsNothing(objfrmMSTDrugs.Parent), Me, objfrmMSTDrugs.Parent))
                objfrmMSTDrugs.Dispose()
                objfrmMSTDrugs = Nothing
                _RxListUserCtrl.DisplayDrugList()
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.Drugs, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try

    End Sub

#End Region

    Private Sub _RxListUserCtrl_cntListmenuStripAddtoPlannedMedClick(ByVal dRow As DataRow, ByVal sDrugSerach As String, ByVal enmselectedbutton As Integer) Handles _RxListUserCtrl.cntListmenuStripAddtoPlannedMedClick
        Try
            Dim oPlanOfTreatment As New frmTreatmentPlan(nRxModulePatientID, "RxMed", dRow, sDrugSerach, enmselectedbutton)
            ''Dim oPlanOfTreatment As New frmTreatmentPlan(nRxModulePatientID, "RxMed", dRow, sDrugSerach, enmselectedbutton)
            oPlanOfTreatment.ShowInTaskbar = False
            oPlanOfTreatment.ShowDialog(Me)

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CancelRx, gloAuditTrail.ActivityType.Cancle, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing

        End Try

    End Sub

#Region "Prescription Previous Rx PRVRx User Control"
    Private Sub _RxRefillC1Flexgrid_cntListmenuStripClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles _RxRefillC1Flexgrid.cntListmenuStripClick
        Try
            If _RxRefillC1Flexgrid.ValidDrug Then
                If Not IsNothing(objCustomPrescription) Then
                    RemoveControl()
                End If

                RaiseEvent PerformEPAStatusCheck(_RxBusinessLayer.PrescriptionCol.Count - 1)
                'Me.PerformPDRStatusCheck(_RxBusinessLayer.PrescriptionCol.Count - 1)
                RaiseEvent PerformDrugAlertCheck(True)

                _RxC1Flexgrid.AddNewPrescription(_RxBusinessLayer.PrescriptionCol.Item(_RxBusinessLayer.PrescriptionCol.Count - 1), _objFormularyToolBar.CurrentEligibilityStatus)

            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub _RxRefillC1Flexgrid_btnCloseRefillClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles _RxRefillC1Flexgrid.btnCloseRefillClick
        Try
            RemoveRefillControl()

            sptRefill.Enabled = False
            pnlRefill.Visible = False

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

#Region "Functions - Previous Rx User Control"
    Private Sub AddRefillControl()
        If Not IsNothing(_RxRefillC1Flexgrid) Then
            RemoveRefillControl()
        End If
        splRight.Visible = False
        pnlRight.Visible = False

        _RxRefillC1Flexgrid = New gloRxSearchC1FlexPrescUserCtrl(_RxBusinessLayer, nRxModulePatientID)

        If _DisableControls = True Then ''disable the make current logic as per medication carry forward case in 7031
            _RxRefillC1Flexgrid.RefillDisable = True
        End If

        _RxRefillC1Flexgrid.BindGrid()
        Me.pnlRefill.Controls.Add(_RxRefillC1Flexgrid)

        _RxRefillC1Flexgrid.Dock = DockStyle.Fill
        _RxRefillC1Flexgrid.Visible = True
        _RxRefillC1Flexgrid.BringToFront()

        sptRefill.Enabled = True
        pnlRefill.Visible = True

        If CheckRefillPanelhavingAnyCTRL() = True Then

            sptRefill.Enabled = True
            pnlRefill.Visible = True

        End If
    End Sub

    Private Sub RemoveRefillControl()
        If Not IsNothing(_RxRefillC1Flexgrid) Then
            Me.pnlRefill.Controls.Remove(_RxRefillC1Flexgrid)
            _RxRefillC1Flexgrid.Visible = False
            _RxRefillC1Flexgrid.Dispose()
            _RxRefillC1Flexgrid = Nothing
            pnlRefill.Visible = False
        End If
    End Sub
#End Region
#End Region

#Region "Medication User Controls"

#Region "Medication C1 Flexgrid User Control"

    Private Sub MedicationInfoButtonClicked(ByVal NDCCode As String, ByVal NDCCodeDesc As String) Handles _MxC1Flexgrid.InfoButtonClicked, _RxC1Flexgrid.InfoButtonClicked
        Dim clsinfobutton_Medication As New gloEMRGeneralLibrary.clsInfobutton()

        Try
            '_RxPatientStrip.
            Dim patientAgeDetail As gloUserControlLibrary.AgeDetail = _RxPatientStrip.PatientAge
            Dim sAgeUnit As String = ""
            Dim sAgeValue As String = ""

            If patientAgeDetail.Years <> 0 Then
                sAgeUnit = "a"
                sAgeValue = patientAgeDetail.Years
            ElseIf patientAgeDetail.Months <> 0 Then
                sAgeUnit = "mo"
                sAgeValue = patientAgeDetail.Months
            ElseIf patientAgeDetail.Days <> 0 Then
                sAgeUnit = "d"
                sAgeValue = patientAgeDetail.Days
            End If
            If _RxBusinessLayer.CurrentVisitID = 0 Then
                _RxBusinessLayer.CurrentVisitID = GenerateVisitID(nRxModulePatientID)
            End If
            clsinfobutton_Medication.GetEducationMaterial_OpenInfobutton(False, _RxPatientStrip.PatientGender, False, sAgeUnit, sAgeValue, "", NDCCode, "2.16.840.1.113883.6.69", NDCCodeDesc, "Provider", gnLoginProviderID, nRxModulePatientID, GenerateVisitID(nRxModulePatientID), Me)
            ' clsinfobutton_Medication.Openinfosource(NDCCode, "2.16.840.1.113883.6.69", strPatientCommunicationPreference, nRxModulePatientID, gloEMRGeneralLibrary.clsInfobutton.enumSource.Medication.GetHashCode(), _RxBusinessLayer.CurrentVisitID, sAgeValue, sAgeUnit, _RxPatientStrip.PatientGender, gnLoginProviderID)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub


    Private Sub _MxC1Flexgrid__FlexClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles _MxC1Flexgrid._FlexClick
        'Developer: Pradeep()
        'Date:12/22/2011
        'Bug ID/PRD Name/Salesforce Case: 17290
        'Reason:shared variable replaced by private
        'If gloEMRGeneralLibrary.glogeneral.clsgeneral.blnRxC1FlexClick = True Then

        If blnRxC1FlexClick = True Then
            Me.Text = "Rx/Meds" & " " & "Mode:- Medication"
            'gloEMRGeneralLibrary.glogeneral.clsgeneral.blnRxC1FlexClick = False
            blnRxC1FlexClick = False
            'Developer: Pradeep()
            'Date:01/05/2012
            'Bug ID: 18186
            'Reason:valiue for property IsRxC1FlexClick was not set
            '_RxListUserCtrl.IsRxC1FlexClick = blnRxC1FlexClick
            btnPrescription.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
            btnPrescription.BackgroundImageLayout = ImageLayout.Stretch
            btnMedication.BackgroundImage = Global.gloEMR.My.Resources.Img_Rx_MxGreen
            btnMedication.BackgroundImageLayout = ImageLayout.Stretch
            Try
                gloPatient.gloPatient.GetWindowTitle(Me, nRxModulePatientID, GetConnectionString(), gstrMessageBoxCaption)
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                ex = Nothing
            End Try
        Else
            RemoveCustomMedicationControl()
        End If

    End Sub

    Private Sub _MxC1Flexgrid__FlexDoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles _MxC1Flexgrid._FlexDoubleClick, _MxC1Flexgrid.MedicationDoubleClicked
        Try
            If IsFormLocked Then
                Exit Sub
            End If
            If _MxC1Flexgrid.RowIndex > 0 Then
                If _MedBusinessLayer.MedicationCol.Count > 0 Then
                    Dim r As Int32
                    r = _MxC1Flexgrid.RowIndex
                    If r > 0 Then
                        If TypeOf (sender) Is C1.Win.C1FlexGrid.C1FlexGrid Then
                            Dim tempGrid As C1.Win.C1FlexGrid.C1FlexGrid = DirectCast(sender, C1.Win.C1FlexGrid.C1FlexGrid)

                            If tempGrid.GetData(r, 7) <> "Cancelled" And tempGrid.GetData(r, 7) <> "Discontinued" Then
                                If pnlRefill.Controls.Contains(_RxRefillC1Flexgrid) = True Or pnlRefill.Controls.Contains(objCustomPrescription) = True Then
                                    pnlRefill.Controls.Remove(_RxRefillC1Flexgrid)
                                    pnlRefill.Controls.Remove(objCustomPrescription)

                                    sptRefill.Enabled = False
                                    pnlRefill.Visible = False
                                End If

                                AddCustomMedicationControl()

                                sptRefill.Enabled = True
                                pnlRefill.Visible = True
                            End If
                            tempGrid = Nothing
                        End If
                    End If
                End If
            Else
                RemoveCustomMedicationControl()
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub _MxC1Flexgrid_StripItemClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles _MxC1Flexgrid.StripItemClick
        Try
            If IsFormLocked Then
                Exit Sub
            End If
            If _MxC1Flexgrid.ValidDrug Then
                If Not IsNothing(objCustomPrescription) Then
                    RemoveControl()

                    sptRefill.Enabled = False
                    pnlRefill.Visible = False

                End If

                RaiseEvent PerformEPAStatusCheck(_RxBusinessLayer.PrescriptionCol.Count - 1)
                'Me.PerformPDRStatusCheck(_RxBusinessLayer.PrescriptionCol.Count - 1)
                RaiseEvent PerformDrugAlertCheck(True)

                If CType(sender, ToolStripMenuItem).Tag = 3 Then
                    ''To Select Prescription Panel
                    Me.Text = "Rx/Meds" & " " & "Mode:- Prescription"
                    blnRxC1FlexClick = True
                    '_RxListUserCtrl.IsRxC1FlexClick = blnRxC1FlexClick
                    btnMedication.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
                    btnMedication.BackgroundImageLayout = ImageLayout.Stretch
                    btnPrescription.BackgroundImage = Global.gloEMR.My.Resources.Img_Rx_MxGreen
                    btnPrescription.BackgroundImageLayout = ImageLayout.Stretch
                    Try
                        gloPatient.gloPatient.GetWindowTitle(Me, nRxModulePatientID, GetConnectionString(), gstrMessageBoxCaption)
                    Catch ex As Exception
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                        ex = Nothing
                    End Try
                End If

                _RxC1Flexgrid.AddNewPrescription(_RxBusinessLayer.PrescriptionCol.Item(_RxBusinessLayer.PrescriptionCol.Count - 1), _objFormularyToolBar.CurrentEligibilityStatus)

                'Refill Scenario (To show formulary alternatives)
                'If gloEMRGeneralLibrary.gloGeneral.clsgeneral.blnLoadFormularyDrugs = True Then
                '    PrescriptionItemAdded()
                'End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally
            SetNKPrescriptionVisibility()
        End Try
    End Sub

    Private Sub btnMxDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMxDown.Click
        Try

            btnMxUp.Visible = True
            btnMxDown.Visible = False
            btnRxUp.Visible = False
            btnRxDown.Visible = True


            If CheckRefillPanelhavingAnyCTRL() = True Then

                sptRefill.Enabled = True
                pnlRefill.Visible = True

            Else

                sptRefill.Enabled = False
                pnlRefill.Visible = False

            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.DrugInteraction, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub btnMxUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMxUp.Click
        Try
            btnMxUp.Visible = False
            btnMxDown.Visible = True
            btnRxUp.Visible = True
            btnRxDown.Visible = False

            If CheckRefillPanelhavingAnyCTRL() = True Then
                sptRefill.Enabled = True
                pnlRefill.Visible = True
            Else
                sptRefill.Enabled = False
                pnlRefill.Visible = False
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.DrugInteraction, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub
    Public Function CheckRefillPanelhavingAnyCTRL() As Boolean
        Try
            If Not IsNothing(objCustomMedication) Then
                Return True
            ElseIf Not IsNothing(objCustomPrescription) Then
                Return True
            ElseIf Not IsNothing(_RxRefillC1Flexgrid) Then
                Return True
            ElseIf Not IsNothing(_MedRefillC1FlexGridUserCtrl) Then
                Return True
            Else
                Return False
            End If
            Return Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.DrugInteraction, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
            Return Nothing
        End Try
    End Function

#End Region

    Private Sub _MxC1Flexgrid_PrnFaxToggle(ByVal flag As Boolean) Handles _MxC1Flexgrid.PrnFaxToggle
        Try
            If flag = True Then
                GloRxToolBarUserCtrl1.tStrpPrint.Visible = False
                GloRxToolBarUserCtrl1.tStrpFax.Visible = False
            Else
                GloRxToolBarUserCtrl1.tStrpPrint.Visible = True
                GloRxToolBarUserCtrl1.tStrpFax.Visible = True
            End If

        Catch ex As Exception
            UpdateLog(ex.Message & ":" & ex.Source)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub


#Region "Medication History Treeview User Control"
    Private Sub _MxHistoryUserCtrl_cntListmenuStripClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles _MxHistoryUserCtrl.cntListmenuStripClick
        Try
            _MxC1Flexgrid.AddandSetFlexgridData(_MedBusinessLayer.MedicationCol.Item(_MedBusinessLayer.MedicationCol.Count - 1))
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.CreateMedication, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub _MxHistoryUserCtrl_MedicationLoaded() Handles _MxHistoryUserCtrl.MedicationLoaded
        Try
            If gblnClinicDISetting Then
                UnloadDrugInteractionControl()
                If Not IsNothing(objDrugInteraction) Then
                    objDrugInteraction.RefreshToolBar()
                End If
            End If
            _MxC1Flexgrid.BindFlexgrid()
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.ModifyMedication, gloAuditTrail.ActivityType.Modify, "Medication opened for modification", nRxModulePatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.CreateMedication, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub _MxHistoryUserCtrl_Recordlock(ByVal blnRecordLock As Boolean) Handles _MxHistoryUserCtrl.Recordlock
    End Sub
#End Region

#Region "CUSTOM Medication"

    Private Sub objCustomMedication_PharmacyClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles objCustomMedication.PharmacyClick
        Try
            If Not IsNothing(oListUsers) Then
                For i As Integer = Me.Controls.Count - 1 To 0 Step -1
                    If Me.Controls(i).Name = oListUsers.Name Then
                        'SLR: Remove handler if any before dispose
                        Dim otemplistcontrol As gloListControl.gloListControl
                        otemplistcontrol = Me.Controls(i)


                        'SLR: Supposed to be Me.Controls(i).dispose and not oListControl.dispose
                        'oListControl.Dispose() 
                        Me.Controls.Remove(otemplistcontrol)
                        'otemplistcontrol.Dispose()
                        'oListControl.Dispose() 'SLR: instead it should be olistUsers
                        Exit For
                    End If
                Next
            End If
            If Not IsNothing(oListUsers) Then
                Try
                    RemoveHandler oListUsers.ItemSelectedClick, AddressOf oListUsers_ItemSelectedClick
                    RemoveHandler oListUsers.ItemClosedClick, AddressOf oListUsers_ItemClosedClick
                    RemoveHandler oListUsers.AddFormHandlerClick, AddressOf oListUsers_AddFormHandlerClick
                    RemoveHandler oListUsers.ModifyFormHandlerClick, AddressOf oListUsers_ModifyFormHandlerClick
                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    ex = Nothing
                End Try
                oListUsers.Dispose()
                oListUsers = Nothing
            End If
            oListUsers = New gloListControl.gloListControl(GetConnectionString, gloListControl.gloListControlType.Pharmacy, False, Me.Width)
            oListUsers.ControlHeader = "Users"

            AddHandler oListUsers.ItemSelectedClick, AddressOf oListUsers_ItemSelectedClick
            AddHandler oListUsers.ItemClosedClick, AddressOf oListUsers_ItemClosedClick
            AddHandler oListUsers.AddFormHandlerClick, AddressOf oListUsers_AddFormHandlerClick
            AddHandler oListUsers.ModifyFormHandlerClick, AddressOf oListUsers_ModifyFormHandlerClick
            oListUsers.Dock = DockStyle.Fill

            If IsNothing(ToList) = False Then
                For i As Integer = 0 To ToList.Count - 1
                    oListUsers.SelectedItems.Add(ToList(i))
                Next
            End If
            pnlRefill.Controls.Add(oListUsers)
            oListUsers.OpenControl()
            oListUsers.ShowHeaderPanel(False)
            oListUsers.Dock = DockStyle.Fill
            oListUsers.BringToFront()

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show("Error on Medication UserListControl" & ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            ex = Nothing
        End Try
    End Sub
    Private Sub objCustomMedication_SigClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles objCustomMedication.SigClick
        Try

            If Not IsNothing(objSigControl) Then
                Me.Controls.Remove(objSigControl)
                'SLR: ALso it was part of panel also. Hence remove from Panel also
                Me.pnlRefill.Controls.Remove(objSigControl)
                objSigControl.Visible = False
                'SLR: Dispose adn then
                objSigControl.Dispose()
                objSigControl = Nothing
                rowindex = 0
            End If

            GridStatus = 1

            BindSigGrid()
            objSigControl = New gloUC_CustomSearchInC1Flexgrid(dt, False)

            Me.Controls.Add(objSigControl)

            Me.pnlRefill.Controls.Add(objSigControl)

            objSigControl.Dock = DockStyle.Fill
            objSigControl.Visible = True
            objSigControl.BringToFront()

            Dim searchrow As Integer
            If Not IsNothing(dt) AndAlso dt.Rows.Count > 0 Then
                If sigid <> 0 Then
                    searchrow = objSigControl._UCflex.FindRow(sigid, 0, 0, False, True, False)
                    objSigControl._UCflex.Select(searchrow, 0, True)
                    sigid = 0
                Else
                    objSigControl._UCflex.Select(1, 0, True)
                End If
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreateMedication, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ex = Nothing
        End Try
    End Sub

    Private Sub objCustomMedication_OKClick(ByVal sender As Object, ByVal e As System.EventArgs, ByVal SelectedMedColItem As Integer) Handles objCustomMedication.OKClick
        Try
            'Developer:Pradeep
            'Date:12/21/2011
            'Bug ID:17225
            'Reason:commented code because it was updating data with user verification,so disable status combo after modification
            ' _MxC1Flexgrid.cmbMedStatus.Enabled = False

            If Not IsNothing(dtPharmacyDetails) AndAlso dtPharmacyDetails.Rows.Count > 0 Then
                _MxC1Flexgrid.SetFlexGridData(SelectedMedColItem, dtPharmacyDetails)
                RemoveCustomMedicationControl()
            Else
                _MxC1Flexgrid.SetFlexGridData(SelectedMedColItem)
                RemoveCustomMedicationControl()
            End If
        Catch ex As gloUserControlLibrary.gloUserControlExceptions
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.CreateMedication, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.CreateMedication, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub objCustomMedication_CloseClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles objCustomMedication.CloseClick
        Try
            RemoveCustomMedicationControl()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

#Region "Function - Custom Medication"
    Private Sub AddCustomMedicationControl()
        If Not IsNothing(objCustomMedication) Then
            RemoveControl() 'SLR: I think it should be RemoveCustomMedicationCOntrol
        End If

        If Not IsNothing(objCustomPrescription) Then
            RemoveControl()
        End If
        splRight.Visible = False
        pnlRight.Visible = False

        GloRxToolBarUserCtrl1.tStrpShowHide.Text = "Show"
        GloRxToolBarUserCtrl1.tStrpShowHide.ToolTipText = "Show Prescription History"

        Dim GridRowNo As Integer = _MxC1Flexgrid.GetRowNoFromGrid(_MxC1Flexgrid.RowIndex)
        'SLR: If above is only RemoveControl then call RemoveCustomMedicationCOntrol and then
        If Not IsNothing(objCustomMedication) Then
            objCustomMedication.Dispose()
            objCustomMedication = Nothing
        End If
        _MedBusinessLayer.MedicationCol.Item(GridRowNo).Rx_ProviderId = nPatientProviderId

        'CS#377101 integrated from 9000
        If _MedBusinessLayer.MedicationCol.Item(GridRowNo).mpid <> 0 And _MedBusinessLayer.MedicationCol.Item(GridRowNo).routes Is Nothing Then
            Dim RoutesList As New List(Of String)
            Using objPrescriptioLayer As New gloEMRGeneralLibrary.PrescriptionBusinessLayer()
                RoutesList = objPrescriptioLayer.GetDrugRoutes(_MedBusinessLayer.MedicationCol.Item(GridRowNo).mpid)
            End Using

            If RoutesList IsNot Nothing Then
                If RoutesList.Count > 2 Then
                    _MedBusinessLayer.MedicationCol.Item(GridRowNo).routes = RoutesList
                End If
                '        _Medication.Route = dt.Rows(i)(5)
                '    Else
                '        _Medication.Route = dt.Rows(i)(5)
                '    End If
                'Else
            End If
        End If

        objCustomMedication = New CustomMedication(_MedBusinessLayer, GridRowNo, , nRxModulePatientID) ''_MxC1Flexgrid.RowIndex,sNDCMxGrid
        objCustomMedication.IsCQMCypressTesting = gblnEnableCQMCypressTesting
        objCustomMedication.Threshold = gnThresholdSetting
        objCustomMedication.ShowFrequencyAbbrevationInRxMeds = gblnIncludeFrequencyAbbrevationInRxMeds
        Me.pnlRefill.Controls.Add(objCustomMedication)

        objCustomMedication.Dock = DockStyle.Fill
        objCustomMedication.Visible = True
        objCustomMedication.BringToFront()
        sptRefill.Enabled = True
        pnlRefill.Visible = True

        If CheckRefillPanelhavingAnyCTRL() = True Then

        End If
    End Sub

    Private Sub RemoveCustomMedicationControl()
        If Not IsNothing(objCustomMedication) Then
            Me.Controls.Remove(objCustomMedication)
            objCustomMedication.Visible = False
            objCustomMedication.Dispose()
            objCustomMedication = Nothing
            pnlRefill.Visible = False
        End If
        If Not IsNothing(_RxRefillC1Flexgrid) Then
            RemoveRefillControl()
        End If
    End Sub
#End Region

#End Region
#End Region

    Private Sub _RxPatientStrip_RxProviderAssociation_Click() Handles _RxPatientStrip.RxProviderAssociation_Click
        Try
            If _RxBusinessLayer.PrescriptionDate = "12:00:00 AM" Then
                dtProviderAssociation = RxBusinesslayer.GetRxProviderAssociation(_RxBusinessLayer.ProviderID)

                Dim ofrm As New frmRxProviderAssociation(_RxBusinessLayer.ProviderID, dtProviderAssociation)
                ofrm.ShowDialog(IIf(IsNothing(ofrm.Parent), Me, ofrm.Parent))
                If ofrm.ProviderAssociation IsNot Nothing Then
                    dtProviderAssociation = ofrm.ProviderAssociation
                End If
                ofrm.Dispose()
                ofrm = Nothing
            Else

                Dim ofrm As New frmRxProviderAssociation(nRxModulePatientID, GetVisitID(_RxBusinessLayer.PrescriptionDate), _RxBusinessLayer.PrescriptionDate, _RxBusinessLayer.ProviderID)
                ofrm.ShowDialog(IIf(IsNothing(ofrm.Parent), Me, ofrm.Parent))
                ofrm.Dispose()
                ofrm = Nothing
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub btnMedication_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMedication.Click
        Me.Text = "Rx/Meds" & " " & "Mode:- Medication"
        btnPrescription.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
        btnPrescription.BackgroundImageLayout = ImageLayout.Stretch
        btnMedication.BackgroundImage = Global.gloEMR.My.Resources.Img_Rx_MxGreen
        btnMedication.BackgroundImageLayout = ImageLayout.Stretch
        'Developer: Pradeep()
        'Date:12/22/2011
        'Bug ID/PRD Name/Salesforce Case: 17290
        'Reason:shared variable replaced by private
        'If gloEMRGeneralLibrary.glogeneral.clsgeneral.blnRxC1FlexClick = True Then
        '    gloEMRGeneralLibrary.glogeneral.clsgeneral.blnRxC1FlexClick = False
        'End If
        If blnRxC1FlexClick = True Then
            blnRxC1FlexClick = False
            '_RxListUserCtrl.IsRxC1FlexClick = blnRxC1FlexClick
        End If

        Try
            gloPatient.gloPatient.GetWindowTitle(Me, nRxModulePatientID, GetConnectionString(), gstrMessageBoxCaption)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub btnPrescription_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrescription.Click
        Me.Text = "Rx/Meds" & " " & "Mode:- Prescription"
        btnMedication.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
        btnMedication.BackgroundImageLayout = ImageLayout.Stretch
        btnPrescription.BackgroundImage = Global.gloEMR.My.Resources.Img_Rx_MxGreen
        btnPrescription.BackgroundImageLayout = ImageLayout.Stretch
        'Developer: Pradeep()
        'Date:12/22/2011
        'Bug ID/PRD Name/Salesforce Case: 17290
        'Reason:shared variable replaced by private
        'If gloEMRGeneralLibrary.glogeneral.clsgeneral.blnRxC1FlexClick = False Then
        '    gloEMRGeneralLibrary.glogeneral.clsgeneral.blnRxC1FlexClick = True
        'End If
        If blnRxC1FlexClick = False Then
            blnRxC1FlexClick = True
            '_RxListUserCtrl.IsRxC1FlexClick = blnRxC1FlexClick
        End If
        Try
            gloPatient.gloPatient.GetWindowTitle(Me, nRxModulePatientID, GetConnectionString(), gstrMessageBoxCaption)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub _MedRefillC1FlexGridUserCtrl_btnCloseRefillClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles _MedRefillC1FlexGridUserCtrl.btnCloseRefillClick
        Try
            RemoveMedRefillControl()
            sptRefill.Enabled = False
            pnlRefill.Visible = False

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.Finish, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try

    End Sub

    Private Sub _MedRefillC1FlexGridUserCtrl_cntListmenuStripClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles _MedRefillC1FlexGridUserCtrl.cntListmenuStripClick
        Try
            _MxC1Flexgrid.AddandSetFlexgridData(_MedBusinessLayer.MedicationCol.Item(_MedBusinessLayer.MedicationCol.Count - 1))
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub _MedBusinessLayer_DisplayMessage(ByVal strVisitDate As DateTime) Handles _MedBusinessLayer.DisplayMessage
        Try
            If blnShowCarryForwardMessage Then
                Dim strmessage As String = "Medications for " & "'" & strPatientFirstName & If(strPatientMiddleName = "", " ", " " & strPatientMiddleName & " ") & strPatientLastName & "'" & " have not been reviewed for today’s visit. Medication history from " & "'" & strVisitDate & "'" & " will be copied forward to this visit to be verified."
                MessageBox.Show(strmessage, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                blnShowCarryForwardMessage = True
            End If

            IsOpenedFromPrescription = False

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.CreateMedication, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub _MedBusinessLayer_DrugDuplication(ByVal ValidationMessage As String) Handles _MedBusinessLayer.DrugDuplication
        MessageBox.Show(ValidationMessage, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
    End Sub

    Private Sub _MedBusinessLayer_LockWindowForUpdate() Handles _MedBusinessLayer.LockWindowForUpdate
        LockWindowUpdate(Me.Handle)
    End Sub

    Private Sub _MedBusinessLayer_MedicationDeleted() Handles _MedBusinessLayer.MedicationDeleted
        Try
            If pnlRight.Visible = True Then
                _MxHistoryUserCtrl.RefreshMedicationHistory()
            End If
            If gblnClinicDISetting Then
                UnloadDrugInteractionControl()
                If Not IsNothing(objDrugInteraction) Then
                    objDrugInteraction.RefreshToolBar()
                End If
            End If
            RemoveControl()
            _MxC1Flexgrid.ClearRows()

            _MedBusinessLayer.FilterType = "Active"

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.CreateMedication, gloAuditTrail.ActivityType.Delete, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub _MedBusinessLayer_MedicationSaveStatus(ByVal blnsaved As Boolean) Handles _MedBusinessLayer.MedicationSaveStatus
        Try
            If _MxC1Flexgrid.cmbMedStatus.Enabled = False Then ''bug 14034, when we save then only the cmbmedStatus combo will get enabled
                _MxC1Flexgrid.cmbMedStatus.Enabled = True
            End If
            If pnlRight.Visible = True Then
                _MxHistoryUserCtrl.RefreshMedicationHistory()
            End If
            If blnsaved Then
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.CreateMedication, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub _MedBusinessLayer_Recordlock(ByVal blnRecordLock As Boolean) Handles _MedBusinessLayer.Recordlock
        If blnRecordLock = True Then
            If Not IsNothing(GloRxToolBarUserCtrl1) = True Then
                GloRxToolBarUserCtrl1.tStrpSave.Enabled = False
                GloRxToolBarUserCtrl1.tStrpSaveRxMed.Enabled = False ''''for CCHIT11
                GloRxToolBarUserCtrl1.tStrpSendRx.Enabled = False ''''for CCHIT11
                GloRxToolBarUserCtrl1.tStrpPrint.Enabled = False
                GloRxToolBarUserCtrl1.tStrpFax.Enabled = False
                GloRxToolBarUserCtrl1.tStrpERx.Enabled = False
                GloRxToolBarUserCtrl1.tStrpRxFill.Enabled = False
            End If
        Else
            If blnRecordLock = True Then
                GloRxToolBarUserCtrl1.tStrpSave.Enabled = True
                GloRxToolBarUserCtrl1.tStrpSaveRxMed.Enabled = True ''''for CCHIT11
                GloRxToolBarUserCtrl1.tStrpSendRx.Enabled = True ''''for CCHIT11
                GloRxToolBarUserCtrl1.tStrpPrint.Enabled = True
                GloRxToolBarUserCtrl1.tStrpFax.Enabled = True
                GloRxToolBarUserCtrl1.tStrpERx.Enabled = True
                GloRxToolBarUserCtrl1.tStrpRxFill.Enabled = True
            End If
        End If
    End Sub

    Private Sub _MedBusinessLayer_RollRowsCount(ByVal FilterStatus As String) Handles _MedBusinessLayer.RollRowsCount
        Try
            _MxC1Flexgrid.BindFlexgrid()
            _MxC1Flexgrid.cmbMedStatus.Text = FilterStatus

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.CreateMedication, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub frmPrescription_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dim dlgmsg As DialogResult
        Try
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Close, "Rx-Meds Closed", nRxModulePatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)

        Catch ex As Exception
        Finally

        End Try
        Try

            If (IsNothing(ToolTip2) = False) Then
                ToolTip2.Dispose()
                ToolTip2 = Nothing
            End If
            'If Not IsNothing(worker) Then
            '    If Not worker.IsBusy Then
            '        worker.Dispose()
            '        worker = Nothing
            '    Else
            '        e.Cancel = True
            '        Exit Sub
            '    End If
            'End If

            If _isSaveClicked = False Then
                If gloEMRGeneralLibrary.gloGeneral.clsgeneral.blnIsRxMedsFormEdited = True Then ''''''''means Rx-Meds form is edited and we should prompt the user if he directly Clicks the Close button

                    If Me.Visible = False Then
                        'Incident #00013567 : Medication carry forward case
                        'Message box not required if we are not showing prescription form
                        'dlgmsg = MessageBox.Show("Do you want to save the changes?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question)

                        dlgmsg = Windows.Forms.DialogResult.Yes
                        'GloRxToolBarUserCtrl1_tStrpSaveClick(sender, e)
                    Else
                        dlgmsg = MessageBox.Show("Do you want to save the changes?", gstrMessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
                    End If

                    If dlgmsg = Windows.Forms.DialogResult.Yes Then
                        Dim nProviderID As Long = IIf(gnLoginProviderID <> 0, gnLoginProviderID, nPatientProviderId)
                        If Not getProviderTaxID(nProviderID) Then
                            e.Cancel = True
                            Exit Sub
                        End If
                        _ismsgdisplay = True
                        If IsFormLocked Then
                            MessageBox.Show("The record cannot be saved because the record is being accessed at other machine", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Else
                            GloRxToolBarUserCtrl1_tStrpSaveClick(sender, e)
                        End If

                        If isOverrideAlertOccured = True Then
                            e.Cancel = True
                            _ismsgdisplay = False
                            Exit Sub
                        End If

                        For Each item As Medication In _MedBusinessLayer.MedicationCol
                            If item.MedicationID <> 0 Then
                                Dim oclsselectProviderTaxID As New gloGlobal.TIN.clsSelectProviderTaxID(nProviderID)
                                oclsselectProviderTaxID.InsertProviderTaxID(nProviderAssociationID, item.MedicationID, sProviderTaxID, nProviderID, gnLoginProviderID, gloGlobal.TIN.clsSelectProviderTaxID.TransactionType.Medication.GetHashCode())
                                oclsselectProviderTaxID = Nothing
                            End If
                        Next
                        For Each item As Prescription In _RxBusinessLayer.PrescriptionCol

                            If item.PrescriptionID <> 0 Then
                                Dim oclsselectProviderTaxID As New gloGlobal.TIN.clsSelectProviderTaxID(nProviderID)
                                oclsselectProviderTaxID.InsertProviderTaxID(nProviderAssociationID, item.PrescriptionID, sProviderTaxID, nProviderID, gnLoginProviderID, gloGlobal.TIN.clsSelectProviderTaxID.TransactionType.Prescription.GetHashCode())
                                oclsselectProviderTaxID = Nothing
                            End If
                        Next

                        If gloEMRGeneralLibrary.gloGeneral.clsgeneral.ds.Tables("MedicationReconcillation").Rows.Count > 0 Then
                            Dim MedicalReconcilationId As Long = 0
                            If _RxBusinessLayer.CurrentVisitID <> 0 Then
                                MedicalReconcilationId = GetMedicalReconcillationID(_MedBusinessLayer.CurrentVisitID)
                            ElseIf _MedBusinessLayer.CurrentVisitID <> 0 Then
                                MedicalReconcilationId = GetMedicalReconcillationID(_MedBusinessLayer.CurrentVisitID)
                            End If
                            Dim oclsselectProviderTaxID As New gloGlobal.TIN.clsSelectProviderTaxID(nProviderID)
                            oclsselectProviderTaxID.InsertProviderTaxID(nProviderAssociationID, MedicalReconcilationId, sProviderTaxID, nProviderID, gnLoginProviderID, gloGlobal.TIN.clsSelectProviderTaxID.TransactionType.ManualReconciliationMedication.GetHashCode())
                            oclsselectProviderTaxID = Nothing
                        End If

                        Me.Hide()
                        Me.WindowState = FormWindowState.Normal

                    ElseIf dlgmsg = Windows.Forms.DialogResult.No Then
                        gloEMRGeneralLibrary.gloGeneral.clsgeneral.blnIsRxMedsFormEdited = False
                        Me.Hide()
                        Me.WindowState = FormWindowState.Normal

                    ElseIf (dlgmsg = Windows.Forms.DialogResult.Cancel) Then
                        e.Cancel = True
                    End If
                Else
                    gloEMRGeneralLibrary.gloGeneral.clsgeneral.blnIsRxMedsFormEdited = False
                    If isOverrideAlertOccured = True Then
                        e.Cancel = True
                        _ismsgdisplay = False
                        Exit Sub
                    End If
                    Me.Hide()
                    Me.WindowState = FormWindowState.Normal

                End If

            End If

            If Not e.Cancel Then
                'clsSplit_PatientPrescription = New gloEMRGeneralLibrary.clsSplitScreen
                If (IsNothing(clsSplit_PatientPrescription) = False) Then
                    clsSplit_PatientPrescription.SaveControlDisplaySettings()
                End If

                If (IsNothing(uiPanSplitScreen_PatientPrescription) = False) Then
                    uiPanSplitScreen_PatientPrescription.Dispose()
                    uiPanSplitScreen_PatientPrescription = Nothing
                End If

                If (IsNothing(clsSplit_PatientPrescription) = False) Then
                    clsSplit_PatientPrescription.Dispose()
                    clsSplit_PatientPrescription = Nothing
                End If
            End If

            If Not IsNothing(dtMedRec) Then ''disposed as per glo Code optimizer tool in 8000 version
                dtMedRec.Dispose()
                dtMedRec = Nothing
            End If
            If Not IsNothing(dtPharmacyDetails) Then ''disposed as per glo Code optimizer tool in 8000 version
                dtPharmacyDetails.Dispose()
                dtPharmacyDetails = Nothing
            End If

            'If Not IsNothing(RefillRequestDataset) Then ''disposed as per glo Code optimizer tool in 8000 version
            '    RefillRequestDataset.Dispose()
            '    RefillRequestDataset = Nothing
            'End If

            If dtProviderAssociation IsNot Nothing Then
                dtProviderAssociation.Dispose()
                dtProviderAssociation = Nothing
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally
            If dlgmsg <> Windows.Forms.DialogResult.Cancel Then
                If isOverrideAlertOccured = False Then
                    gloEMRGeneralLibrary.gloGeneral.clsgeneral.blnIsRxMedsFormEdited = False
                    gloEMRGeneralLibrary.gloGeneral.clsgeneral.blnRxDeleteFlag = False
                    gloEMRGeneralLibrary.gloGeneral.clsgeneral.gblnCustomPrescEdited = False
                    gloEMRGeneralLibrary.gloGeneral.clsgeneral.gblnCustomMediEdited = False
                End If
            End If

        End Try

    End Sub

    Private Sub GloRxToolBarUserCtrl1_tStrpQuickPrintClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles GloRxToolBarUserCtrl1.tStrpQuickPrintClick
        Try
            _IsPrintImmediately = True
            GloRxToolBarUserCtrl1_tStrpPrintClick(sender, e)
        Catch ex As PrescriptionException
            UpdateLog(ex.ErrMessage.ToString() & ":" & ex.Source)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally
            _IsPrintImmediately = False
        End Try
    End Sub

    Private Sub GloRxToolBarUserCtrl1_tStrpOpenPrintDialogClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles GloRxToolBarUserCtrl1.tStrpOpenPrintDialogClick
        Try
            _IsOpenPrintDialog = True
            GloRxToolBarUserCtrl1_tStrpPrintClick(sender, e)
        Catch ex As PrescriptionException
            UpdateLog(ex.ErrMessage.ToString() & ":" & ex.Source)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally
            _IsOpenPrintDialog = False
        End Try
    End Sub

    Private Sub _MxC1Flexgrid_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles _MxC1Flexgrid.Load

    End Sub

    Private Sub _MxC1Flexgrid_btnScanViewDoc(ByVal MedicationCol As gloEMRGeneralLibrary.gloEMRActors.Medications, ByVal btnScanViewDocumentText As String) Handles _MxC1Flexgrid.btnScanViewDoc
        Try
            If IsFormLocked Then
                Exit Sub
            End If

            gDMSCategory_Labs = ""
            Dim objSettings As New clsSettings
            objSettings.GetSettings()
            If IsNothing(objSettings.DMSCategory_RxMed) = False Then
                gDMSCategory_RxMed = objSettings.DMSCategory_RxMed
            End If
            'SLR: dispose and tehn
            objSettings.Dispose()
            objSettings = Nothing

            If btnScanViewDocumentText = "View" Then
                lDMSDocumentID = _MxC1Flexgrid.GetDocumentId()
            End If

            ScanViewDoucment(btnScanViewDocumentText)



            If MedicationCol.Count > 0 Then
                Dim i As Integer
                For i = 0 To MedicationCol.Count - 1
                    If lDMSDocumentID <> 0 Then
                        MedicationCol.Item(i).RxMedDMSID = lDMSDocumentID
                        If MedicationCol.Item(i).State = "U" Then
                            MedicationCol.Item(i).State = "M"
                        End If
                    Else ''''else send a 0 value
                        MedicationCol.Item(i).RxMedDMSID = 0
                    End If
                Next
                _MxC1Flexgrid.SetFlexGridData(lDMSDocumentID)
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.Scan, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub btnViewDocumentClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnViewDocumentClose.Click
        Try
            If pnlDocument.Controls.Count > 0 Then

                Dim cntrl As Control

                For Each cntrl In pnlDocument.Controls

                    If Not IsNothing(cntrl) Then

                        If cntrl.Name = "DocumentForm" Then
                            CType(cntrl, Form).Dispose()
                        End If

                    End If
                Next

                pnlDocument.Controls.Clear()

            End If
            pnlViewDocument.Visible = False
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.eRx, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub objChiefcomplaints_btnUC_ADDclick(ByVal sender As Object, ByVal e As System.EventArgs) Handles objChiefcomplaints.btnUC_ADDclick
        Dim blnRecordLock As Boolean = False

        If gblnSMDBSetting = False Then

            Dim frm As New frmProblemList(0, _RxBusinessLayer.CurrentVisitID, blnRecordLock)
            frm.ShowInTaskbar = False
            frm.StartPosition = FormStartPosition.CenterScreen
            frm.ShowMessageForPendingReconciliation()
            frm.ShowDialog(IIf(IsNothing(frm.Parent), Me, frm.Parent))
            frm.Dispose()
            frm = Nothing
        Else

            Dim frm As New frmProblemList(0, GetVisitID(DateTime.Now), blnRecordLock)
            frmProblemList.blnRxMedFromExam = True
            frmProblemList.blnRxMedToProblem = True
            frm.StartPosition = FormStartPosition.CenterScreen
            frm.ShowInTaskbar = False
            frm.ShowMessageForPendingReconciliation()
            If frm.ShowDialog(IIf(IsNothing(frm.Parent), Me, frm.Parent)) = Windows.Forms.DialogResult.OK Then
                objCustomPrescription_CloseComplaintClick(sender, e)
            End If
            frm.Dispose()
            frm = Nothing
        End If
    End Sub

    Private Sub GloRxToolBarUserCtrl1_tStrpbleLink_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles GloRxToolBarUserCtrl1.tStrpbleLink_Click
        Try
            If CheckURLAddress(str_eLink) Then
                Process.Start(str_eLink)
            Else
                MessageBox.Show("Invalid medication information url " & str_eLink, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

        Catch ex As Exception
            MessageBox.Show("Invalid medication information url " & str_eLink, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try

    End Sub

    Public Function CheckURLAddress(ByVal strUrlInput As String) As Boolean

        Dim strRegex As String
        strRegex = "^((((ht|f){1}((tp|tps):[/][/]){1})|((www.){1}))[a-zA-Z0-9_\-]+(?:\.[a-zA-Z0-9_\-]+)*\.[a-zA-Z]{2,4}(?:\/[a-zA-Z0-9_]+)*(?:\/[a-zA-Z0-9_]+\.[a-zA-Z]{2,4}(?:\?[a-zA-Z0-9_]+\=[a-zA-Z0-9_]+)?)?(?:\&[a-zA-Z0-9_]+\=[a-zA-Z0-9_]+)*)$"
        If Regex.IsMatch(strUrlInput, strRegex) = False Then
            Return False
        Else
            Return True
        End If
    End Function

    Private Function GetLink() As String
        Dim strLink As String = ""
        Dim conn As SqlConnection = Nothing
        Dim cmd As SqlCommand = Nothing
        Try
            conn = New SqlConnection
            Dim _strSQL As String = ""
            _strSQL = "select ISNULL(sSettingsValue,'') from Settings where UPPER(sSettingsName) = 'MEDICATIONINFO'"
            conn.ConnectionString = GetConnectionString()
            conn.Open()
            cmd = New SqlCommand(_strSQL, conn)
            _strSQL = cmd.ExecuteScalar & ""
            If _strSQL <> "" Then
                strLink = _strSQL
            Else
                strLink = ""
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
        Return strLink
    End Function

    Private Sub frmPrescription_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Try
            'CType(Me.ParentForm, MainMenu).SetGnPatientID = GetCurrentPatientID
            If (IsNothing(Me.ParentForm) = False) Then
                CType(Me.ParentForm, MainMenu).SetGnPatientID = GetCurrentPatientID
            End If


            If Not IsNothing(uiPanSplitScreen_PatientPrescription) Then
                If Not IsNothing(uiPanSplitScreen_PatientPrescription.Parent) Then
                    If uiPanSplitScreen_PatientPrescription.Parent.Text = "Split Screen" Then
                        uiPanSplitScreen_PatientPrescription.Parent.Visible = True
                    ElseIf uiPanSplitScreen_PatientPrescription.Text = "Split Screen" Then
                        uiPanSplitScreen_PatientPrescription.Visible = True

                    End If
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
        For Each myForm As Form In Application.OpenForms
            If (myForm.TopMost) Then
                myForm.TopMost = False
            End If
        Next
        Me.TopMost = True
    End Sub

    Private Sub frmPrescription_Deactivate(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Deactivate
        Me.TopMost = False
        If Not IsNothing(Me.Parent) Then
            If Not IsNothing(uiPanSplitScreen_PatientPrescription) Then
                If Not IsNothing(uiPanSplitScreen_PatientPrescription.Parent) Then
                    If uiPanSplitScreen_PatientPrescription.Parent IsNot Me Then
                        uiPanSplitScreen_PatientPrescription.Parent.Visible = False
                        uiPanSplitScreen_PatientPrescription.Parent.Hide()
                        uiPanSplitScreen_PatientPrescription.Parent.Update()
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub Get_ProviderDetails(ByVal ProviderID As Int64)
        Try
            Using dtProvider As DataTable = _RxBusinessLayer.GetPatientProviderDetails(ProviderID)
                If Not IsNothing(dtProvider) AndAlso dtProvider.Rows.Count > 0 Then
                    _providerInfo = dtProvider.Rows(0)
                End If

                If IsNothing(_providerInfo) = False Then
                    ' If dtProvider.Rows.Count > 0 Then
                    strPrescriberAddress = Convert.ToString(_providerInfo("sPrAddressline"))
                    strPrescriberAddress2 = Convert.ToString(_providerInfo("sPrAddressLine2"))
                    strPrescriberCity = Convert.ToString(_providerInfo("sPrCity"))
                    strPrescriberState = Convert.ToString(_providerInfo("sPrState"))
                    strPrescriberCountry = Convert.ToString(_providerInfo("sCountry"))
                    strPrescriberZip = Convert.ToString(_providerInfo("sPrZip"))
                    strPrescriberPhone = Convert.ToString(_providerInfo("sPrPhone"))
                    strPrescriberFirstName = Convert.ToString(_providerInfo("sPrFirstName"))
                    strPrescriberMiddleName = Convert.ToString(_providerInfo("sPrMiddleName"))
                    strPrescriberLastName = Convert.ToString(_providerInfo("sPrLastName"))
                    strPrescriberFax = Convert.ToString(_providerInfo("sPrFax"))
                    strPrescriberNPI = Convert.ToString(_providerInfo("sPrNPI"))
                    strPrescriberDEA = Convert.ToString(_providerInfo("sPrDEA"))
                    strPrescriberNADEAN = Convert.ToString(_providerInfo("sPrNADEAN"))
                    strPrescriberSSN = Convert.ToString(_providerInfo("sPrSSN"))
                    strPrescriberSPI = Convert.ToString(_providerInfo("sPrSPIID"))
                    strServiceLevel = Convert.ToString(_providerInfo("sServiceLevel"))
                    strPrescriberEmail = Convert.ToString(_providerInfo("sPrEMail"))

                    If gblnEpcsEnabled = True Then  ''Is EPCS Enabled for clinic
                        ''To check Provider is EPCS enabled
                        If strServiceLevel <> "" Then
                            If Mid(strServiceLevel, 5, 1) = 1 Then
                                gbIsProviderEPCSEnable = True
                            Else
                                gbIsProviderEPCSEnable = False
                            End If
                        Else
                            gbIsProviderEPCSEnable = False
                        End If
                    Else
                        gbIsProviderEPCSEnable = False
                    End If

                    clsgeneral.gblnIsPDREnabled = _RxBusinessLayer.IsPDREnabledForProvider(_RxBusinessLayer.ProviderID)
                    clsgeneral.gblnIsPDMPEnabled = _RxBusinessLayer.IsPDMPEnabledForProvider(_RxBusinessLayer.ProviderID)
                   
                    If strServiceLevel Is Nothing OrElse strServiceLevel = "" OrElse Mid(strServiceLevel, 12, 1) <> "1" Then
                        _RxC1Flexgrid.RemoveCancelRxContextMenu()
                    End If

                    '    End If
                End If
            End Using
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub Get_SupervisorDetails()
        Dim dtSupervisingProvider As DataTable = Nothing
        Try
            dtSupervisingProvider = _RxBusinessLayer.GetPatientProviderDetails(cmbSupervisingProvider.SelectedValue)
            If IsNothing(dtSupervisingProvider) = False Then
                If dtSupervisingProvider.Rows.Count > 0 Then
                    strSupervisingPrescriberAddress = Convert.ToString(dtSupervisingProvider.Rows(0)("sPrAddressline"))
                    strSupervisingPrescriberAddress2 = Convert.ToString(dtSupervisingProvider.Rows(0)("sPrAddressLine2"))
                    strSupervisingPrescriberCity = Convert.ToString(dtSupervisingProvider.Rows(0)("sPrCity"))
                    strSupervisingPrescriberState = Convert.ToString(dtSupervisingProvider.Rows(0)("sPrState"))
                    strSupervisingPrescriberZip = Convert.ToString(dtSupervisingProvider.Rows(0)("sPrZip"))
                    strSupervisingPrescriberPhone = Convert.ToString(dtSupervisingProvider.Rows(0)("sPrPhone"))
                    strSupervisingPrescriberFirstName = Convert.ToString(dtSupervisingProvider.Rows(0)("sPrFirstName"))
                    strSupervisingPrescriberMiddleName = Convert.ToString(dtSupervisingProvider.Rows(0)("sPrMiddleName"))
                    strSupervisingPrescriberLastName = Convert.ToString(dtSupervisingProvider.Rows(0)("sPrLastName"))
                    strSupervisingPrescriberFax = Convert.ToString(dtSupervisingProvider.Rows(0)("sPrFax"))
                    strSupervisingPrescriberNPI = Convert.ToString(dtSupervisingProvider.Rows(0)("sPrNPI"))
                    strSupervisingPrescriberDEA = Convert.ToString(dtSupervisingProvider.Rows(0)("sPrDEA"))
                    strSupervisingPrescriberSSN = Convert.ToString(dtSupervisingProvider.Rows(0)("sPrSSN"))
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally
            If IsNothing(dtSupervisingProvider) = False Then
                dtSupervisingProvider.Dispose()
                dtSupervisingProvider = Nothing
            End If
        End Try
    End Sub

    Private Sub Get_PharmacyDetails()
        Try
            _RxBusinessLayer.GetPharmacyID(nRxModulePatientID)

            Using dtPharmacy As DataTable = _RxBusinessLayer.GetPharmacyDetails(_RxBusinessLayer.OldPharmacyId)
                If dtPharmacy IsNot Nothing Then
                    If dtPharmacy.Rows.Count > 0 Then
                        _pharmacyInfo = dtPharmacy.Rows(0)
                    End If
                End If
            End Using

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try

    End Sub

    Private Sub Get_PatientDetails()
        Try
            Using dtPatient As DataTable = GetPatientInfo(nRxModulePatientID)
                If Not IsNothing(dtPatient) AndAlso dtPatient.Rows.Count > 0 Then
                    _patientInfo = dtPatient.Rows(0)
                End If

                If IsNothing(_patientInfo) = False Then
                    'If dtPatient.Rows.Count > 0 Then
                    strPatientCode = Convert.ToString(_patientInfo("sPatientCode"))
                    strPatientFirstName = Convert.ToString(_patientInfo("sFirstName"))
                    strPatientMiddleName = Convert.ToString(_patientInfo("sMiddleName"))
                    strPatientLastName = Convert.ToString(_patientInfo("sLastName"))
                    strPatientDOB = Convert.ToString(_patientInfo("dtDOB"))
                    strPatientAge = GetAge(Convert.ToDateTime(_patientInfo("dtDOB")))
                    strPatientGender = Convert.ToString(_patientInfo("sGender"))
                    strPatientMaritalStatus = Convert.ToString(_patientInfo("sMaritalStatus"))
                    strPatientAddress = Convert.ToString(_patientInfo("sAddressline"))
                    strPatientAddress2 = Convert.ToString(_patientInfo("sAddressline2"))
                    strPatientCity = Convert.ToString(_patientInfo("sCity"))
                    strPatientState = Convert.ToString(_patientInfo("sState"))
                    strPatientZip = Convert.ToString(_patientInfo("sZip"))
                    strPatientPhone = Convert.ToString(_patientInfo("sPhone"))
                    strPatientFax = Convert.ToString(_patientInfo("sFax"))
                    strPatientEmail = Convert.ToString(_patientInfo("sEmail"))
                    strPatientSsn = Convert.ToString(_patientInfo("nSSN"))
                    strPatientCountry = Convert.ToString(_patientInfo("sCountry"))
                    strPatientCommunicationPreference = Convert.ToString(_patientInfo("sCommunicationPreference"))
                    'End If
                End If
            End Using
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub Set_DefaultPhamacy(ByVal npatientid As Int64, ByVal nContactID As Int64, ByVal nsetDefault As Integer, ByVal nclinicID As Int64)
        Dim objCon As New SqlConnection
        Dim objCmd As New SqlCommand
        Try
            objCon.ConnectionString = GetConnectionString()
            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "gsp_UpdateDefaultPharmacy"
            Dim objPara_PatientID As New SqlParameter
            With objPara_PatientID
                .ParameterName = "@npatientid"
                .Value = npatientid
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.BigInt
            End With
            objCmd.Parameters.Add(objPara_PatientID)

            objPara_PatientID = Nothing

            Dim objPara_ContactID As New SqlParameter
            With objPara_ContactID
                .ParameterName = "@nContactID"
                .Value = nContactID
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.BigInt
            End With
            objCmd.Parameters.Add(objPara_ContactID)

            objPara_ContactID = Nothing

            Dim objPara_PharmacyID As New SqlParameter
            With objPara_PharmacyID
                .ParameterName = "@SetDefaultPharmacy"
                .Value = nsetDefault
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.Int
            End With
            objCmd.Parameters.Add(objPara_PharmacyID)

            objPara_PharmacyID = Nothing

            Dim objPara_ClinicID As New SqlParameter
            With objPara_ClinicID
                .ParameterName = "@nClinicID"
                .Value = nclinicID
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.Int
            End With
            objCmd.Parameters.Add(objPara_ClinicID)

            objPara_ClinicID = Nothing

            objCmd.Connection = objCon
            objCon.Open()
            objCmd.ExecuteNonQuery()


        Catch ex As SqlException
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally
            objCon.Close()
            If Not IsNothing(objCon) Then
                objCon.Dispose()
                objCon = Nothing
            End If
            If Not IsNothing(objCmd) Then
                objCmd.Dispose()
                objCmd = Nothing
            End If

        End Try
    End Sub

    ''' <summary>
    ''' this will remove the custom control if any when we change the cmbMedStatus value
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub _MxC1Flexgrid_cmbChangeRemoveControl() Handles _MxC1Flexgrid.cmbChangeRemoveControl
        Try
            RemoveCustomMedicationControl()
            RemoveControl()
            RemoveMedRefillControl()
            RemoveRefillControl()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub


    Private Sub _MxC1Flexgrid__FlexMouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles _MxC1Flexgrid._FlexMouseDown
        RemoveControl()
        RemoveCustomMedicationControl()
        _MxC1Flexgrid.ValidDrug = False
    End Sub

    Private Sub SaveSupervisingProvider()
        Try
            If cmbSupervisingProvider.Visible = True Then
                If cmbSupervisingProvider.SelectedIndex <> -1 Then
                    Dim newRxRow As DataRow = gloEMRGeneralLibrary.gloGeneral.clsgeneral.ds.Tables("PrescriptionProvider").NewRow()
                    newRxRow.Item("nID") = 0
                    newRxRow.Item("nPrescriptionProviderID") = _RxBusinessLayer.ProviderID
                    If _RxBusinessLayer.CurrentVisitID > 0 Then
                        newRxRow.Item("nVisitID") = _RxBusinessLayer.CurrentVisitID
                    Else
                        newRxRow.Item("nVisitID") = _MedBusinessLayer.CurrentVisitID
                    End If
                    newRxRow.Item("nPatientID") = nRxModulePatientID
                    newRxRow.Item("nSupervisingProviderID") = cmbSupervisingProvider.SelectedValue
                    _RxBusinessLayer.SupervisingProviderID = cmbSupervisingProvider.SelectedValue
                    newRxRow.Item("InsertedDateTime") = DateTime.Now
                    gloEMRGeneralLibrary.gloGeneral.clsgeneral.ds.Tables("PrescriptionProvider").Rows.Add(newRxRow)
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Function ValidateSupervisingProvider() As Boolean
        Dim _validationFlag As Boolean = True
        Try
            If blnIsJuniorProvider = True Then
                If cmbSupervisingProvider.Visible = True Then
                    If cmbSupervisingProvider.Text = "" Then
                        If blnRequireSupervisingProvider = True Then
                            _RxBusinessLayer.SupervisingProviderID = cmbSupervisingProvider.SelectedValue
                            If _RxBusinessLayer.PrescriptionCol.Count > 0 Or _MedBusinessLayer.MedicationCol.Count > 0 Then
                                MessageBox.Show("Supervising Provider is required for " & "'" & _RxPatientStrip.Provider & "'", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                _isSupervisormessagedisplay = True
                            End If
                            _validationFlag = False
                        End If
                    Else
                        _isSupervisormessagedisplay = False
                    End If
                End If
            End If
            Return _validationFlag
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
            Return _validationFlag
        End Try
    End Function

    Private Sub GloRxToolBarUserCtrl1_tStrpblReconcile_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles GloRxToolBarUserCtrl1.tStrpblReconcile_Click
        ShowReconciliation()
    End Sub

    Private Sub ShowReconciliation()
        Dim Result As Integer
        Dim ogloCCDReconcile As New gloCCDLibrary.gloCCDReconcilation
        If _MedBusinessLayer.MedicationCol.Count > 0 Then
            For i As Integer = 0 To _MedBusinessLayer.MedicationCol.Count - 1
                If _MedBusinessLayer.MedicationCol.Item(i).State <> "U" Then
                    Result = MessageBox.Show("Reconcile List cannot accessed without saving  Medication(s) changes. Do you want to save Medication(s)?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    If Result = MsgBoxResult.Yes Then
                        SaveMeds(True)
                        Exit For
                    ElseIf Result = MsgBoxResult.No Then
                        If IsNothing(ogloCCDReconcile) = False Then
                            ogloCCDReconcile.Dispose()
                            ogloCCDReconcile = Nothing
                        End If
                        Exit Sub
                    End If
                End If

            Next
        End If
        Dim frmReconcilation As New frmReconcileList(nRxModulePatientID, "Medication")
        frmReconcilation.LoginUser = gstrLoginName
        frmReconcilation.LoginID = gnLoginID
        frmReconcilation.ShowDialog(IIf(IsNothing(frmReconcilation.Parent), Me, frmReconcilation.Parent))

        If frmReconcilation.MedicationVisitID <> 0 Then
            _MedBusinessLayer.CurrentVisitID = frmReconcilation.MedicationVisitID
            SaveMeds(True)

        End If



        If IsNothing(Me.ParentForm) = False Then
            CType(Me.ParentForm, MainMenu).ShowReconciliationAlert()
        End If

        Dim _isReadyLists As Boolean = False

        _isReadyLists = ogloCCDReconcile.IsReadyListsPresent(nRxModulePatientID, "Medication")
        If _isReadyLists = True Then
            GloRxToolBarUserCtrl1.tlb_Reconcile.Enabled = True
        Else
            GloRxToolBarUserCtrl1.tlb_Reconcile.Enabled = False
        End If
        If IsNothing(frmReconcilation) = False Then
            frmReconcilation.Dispose()
            frmReconcilation = Nothing
        End If

        If IsNothing(ogloCCDReconcile) = False Then
            'SLR: Dispose and then
            ogloCCDReconcile.Dispose()
            ogloCCDReconcile = Nothing
        End If
    End Sub

    Public Sub RefreshComboOnLoad()
        IsFormLoaded = True
        If _DisableControls = False Then
            If Not IsNothing(_objFormularyToolBar) Then
                If (_objFormularyToolBar.CurrentEligibilityStatus = RxBusinesslayer.EligibilityStatus.NotChecked) Then
                    If gblnAutoEligibility = True Then
                        ProcessRxEligibility(nRxModulePatientID, False)
                    End If
                ElseIf _objFormularyToolBar.CurrentEligibilityStatus = RxBusinesslayer.EligibilityStatus.Passed Then
                    RefreshPBMCombo()
                End If

            End If
        End If
        If _RxBusinessLayer.PrescriptionCol.Count > 0 Then
            If IsRefRequest Then
                If _objFormularyToolBar.CurrentEligibilityStatus <> RxBusinesslayer.EligibilityStatus.NotChecked Then
                    _RxC1Flexgrid.ShowFormulary(_RxBusinessLayer.PrescriptionCol.Count)

                End If
            Else
                If _objFormularyToolBar.CurrentEligibilityStatus <> RxBusinesslayer.EligibilityStatus.NotChecked Then
                    If (_RxBusinessLayer.PrescriptionCol(0).IsFormularyQueried = False) Then
                        _RxC1Flexgrid.ShowFormulary(1)
                    End If
                End If
            End If
        End If
        LockWindowUpdate(IntPtr.Zero)
        If LoadError = True Then
            Me.Close()
        End If
        If _RxC1Flexgrid.formLock Then
            If Not IsNothing(_objFormularyToolBar) Then
                _objFormularyToolBar.tStrpMedicationHistory.Enabled = False
                _objFormularyToolBar.tlbbtn_RxHub.Enabled = False
            End If
        End If
    End Sub

    Private Sub frmPrescription_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        RefreshComboOnLoad()
        SetNKPrescriptionVisibility()
        RaiseEvent PerformEPAStatusCheck(Nothing)
    End Sub

    Private Function FillERxEligibilityInfo() As gloSureScript.BenefitsCoordinations ' RxHubFormularys
        Try

            Dim oClsRxHubInterface As New ClsRxHubInterface()
            If gstrRxEligThresholdvalue = "" Then
                gstrRxEligThresholdvalue = 3 '''''by default set the threshold value to 3 days i.e. 72 hrs because 4010 is stopped and 5010 need 3 days validation.
            End If
            If oClsRxHubInterface.SS10dot6IsEligibilitygGenerated_validation(nRxModulePatientID, gstrRxEligThresholdvalue) = False Then
                Dim pbmInfo As New gloSureScript.BenefitsCoordination 'RxHubFormulary
                Dim pbmList As New gloSureScript.BenefitsCoordinations 'RxHubFormularys
                Dim dt271RespDetails As DataTable = Nothing
                Dim dtControlNo As DataTable = Nothing
                ' Dim formulary2 As New RxHubFormulary
                Try
                    Dim PBMMemberid As String = ""

                    If gloEMRGeneralLibrary.gloGeneral.clsgeneral.gblnIsFormularyServiceEnabled = True Then
                        PBMMemberid = _objFormularyToolBar.tlStrpPBMCombo.ComboBox.SelectedValue
                    End If

                    dt271RespDetails = oClsRxHubInterface.Get271EligibilityResponseInformation(nRxModulePatientID)
                    If Not IsNothing(dt271RespDetails) Then
                        If dt271RespDetails.Rows.Count > 0 Then
                            Dim refMsgId As String = dt271RespDetails.Rows(0)("sMessageId")
                            dtControlNo = oClsRxHubInterface.Get271ISAControlNumber(refMsgId)
                            Dim ISAControlNo As String = ""

                            For i As Int16 = 0 To dtControlNo.Rows.Count - 1
                                If dtControlNo.Rows.Count > 0 Then
                                    If dtControlNo.Rows(i)("MsgId") <> "" Then
                                        ISAControlNo = dtControlNo.Rows(i)("MsgId")
                                    End If
                                End If
                            Next

                            For i As Int16 = 0 To dt271RespDetails.Rows.Count - 1

                                If dt271RespDetails.Rows(i)("sPBM_PayerMemberID").ToString = PBMMemberid Then
                                    pbmInfo.ISAControlNumber = ISAControlNo
                                    pbmInfo.PBM_PayerMemberId = dt271RespDetails.Rows(i)("sPBM_PayerMemberID")
                                    pbmInfo.PBMPayerParticipantId = dt271RespDetails.Rows(i)("sPBM_PayerParticipantID")
                                    pbmInfo.HealthPlanNumber = dt271RespDetails.Rows(i)("sHealthPlanNumber")
                                    pbmInfo.HealthPlanName = dt271RespDetails.Rows(i)("sHealthPlanName")
                                    pbmInfo.CardHolderId = dt271RespDetails.Rows(i)("sCardHolderID")
                                    pbmInfo.CardHolderName = dt271RespDetails.Rows(i)("sCardHolderName")
                                    pbmInfo.GroupId = dt271RespDetails.Rows(i)("sGroupID")
                                    pbmInfo.GroupName = dt271RespDetails.Rows(i)("sGroupName")
                                    pbmInfo.BINPCNNumber = dt271RespDetails.Rows(i)("sBINNumberPCNNumber")
                                    pbmInfo.PBMPayerName = dt271RespDetails.Rows(i)("sPBM_PayerName")
                                    pbmInfo.SocialSecurityNumber = dt271RespDetails.Rows(i)("sSocialSecurityNumber")
                                    pbmInfo.PatientAccountNumber = dt271RespDetails.Rows(i)("sPatientAccountNumber")
                                    pbmInfo.HealthPlanBenefitCoverageName = dt271RespDetails.Rows(i)("sHealthPlanBenefitCoverageName")
                                    pbmInfo.SubscriberFirstName = dt271RespDetails.Rows(i)("SubFName")
                                    pbmInfo.SubscriberMiddleName = dt271RespDetails.Rows(i)("SubMName")
                                    pbmInfo.SubscriberLastName = dt271RespDetails.Rows(i)("SubLName")
                                    pbmInfo.SubscriberSuffix = dt271RespDetails.Rows(i)("SubscriberSuffix")
                                    pbmInfo.SubscriberGender = dt271RespDetails.Rows(i)("sSubscriberGender")
                                    pbmInfo.SubscriberDOB = dt271RespDetails.Rows(i)("sSubscriberDOB")
                                    pbmInfo.SubscriberAddress1 = dt271RespDetails.Rows(i)("sSubscriberAddress1")
                                    pbmInfo.SubscriberAddress2 = dt271RespDetails.Rows(i)("sSubscriberAddress2")
                                    pbmInfo.SubscriberCity = dt271RespDetails.Rows(i)("sSubscriberCity")
                                    pbmInfo.SubscriberState = dt271RespDetails.Rows(i)("sSubscriberState")
                                    pbmInfo.SubscriberZip = dt271RespDetails.Rows(i)("sSubscriberZip")
                                    pbmInfo.PatientRelationShip = dt271RespDetails.Rows(i)("spersoncode") ''patient relationship to be sent in COB for NewRx

                                    pbmList.Add(pbmInfo)
                                    pbmInfo.Dispose()
                                    pbmInfo = Nothing
                                End If

                            Next

                            Return pbmList

                        Else
                            'MessageBox.Show("The benefit of coordination information for this patient cannot be sent in NewRx because the eligibility information is valid only for 72 hours", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            'Exit Function
                            Return Nothing
                        End If
                    Else
                        MessageBox.Show("The Eligibility request for this patient cannot be shown since request is valid only for 72 hours", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        FillERxEligibilityInfo = Nothing
                        Exit Function
                    End If
                Catch ex As Exception
                    Return Nothing
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.eRx, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    ex = Nothing
                Finally
                    If Not IsNothing(oClsRxHubInterface) Then
                        oClsRxHubInterface.Dispose()
                        oClsRxHubInterface = Nothing
                    End If

                    If Not IsNothing(dt271RespDetails) Then
                        dt271RespDetails.Dispose()
                        dt271RespDetails = Nothing
                    End If
                    If Not IsNothing(dtControlNo) Then
                        dtControlNo.Dispose()
                        dtControlNo = Nothing
                    End If
                End Try
                'Else
                '    MessageBox.Show("Eligibility request is valid only for 72 hours, hence Coordination of Benefit information for this patient will not be sent in eRx.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else ''suppose validation is false for any reason atlease send the ISA control number in the eRx
                Dim pbmInfo As New gloSureScript.BenefitsCoordination 'RxHubFormulary
                Dim pbmList As New gloSureScript.BenefitsCoordinations 'RxHubFormularys
                Dim dt271RespDetails As New DataTable

                Try
                    dt271RespDetails = oClsRxHubInterface.Get271LatestISAControlNumberForPatient(nRxModulePatientID)
                    If Not IsNothing(dt271RespDetails) Then
                        If dt271RespDetails.Rows.Count > 0 Then
                            Dim ISAControlNo As String = ""
                            'Dim ParticipantId As String = ""
                            'Dim PayerName As String = ""

                            ISAControlNo = dt271RespDetails.Rows(0)("MsgId")
                            'ParticipantId = dt271RespDetails.Rows(0)("participantid")
                            'PayerName = dt271RespDetails.Rows(0)("payername")

                            pbmInfo.ISAControlNumber = ISAControlNo
                            'formulary.PBMPayerParticipantId = ParticipantId
                            'formulary.PBMPayerName = PayerName


                            pbmList.Add(pbmInfo)
                            pbmInfo.Dispose()
                            pbmInfo = Nothing
                            Return pbmList
                        Else
                            Return Nothing

                        End If
                    Else
                        Return Nothing
                    End If
                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    ex = Nothing
                    Return Nothing
                Finally


                    If Not IsNothing(oClsRxHubInterface) Then
                        oClsRxHubInterface.Dispose()
                        oClsRxHubInterface = Nothing
                    End If
                    If Not IsNothing(dt271RespDetails) Then
                        dt271RespDetails.Dispose()
                        dt271RespDetails = Nothing
                    End If

                End Try

            End If


        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
            Return Nothing
        Finally

        End Try

    End Function

    Public Function ValidateCancelRxBeforeDenial(ByVal Index As Integer) As Boolean
        Dim blnValid As Boolean = True

        Dim ValidationMessageBuilderforDrug As New System.Text.StringBuilder
        Dim ValidationMessageBuilderForEPA As New System.Text.StringBuilder()
        Dim ValidationMessageBuilderForEPCS As New System.Text.StringBuilder()
        Try


            ValidateCustomPrescription(Index, "cancelrx", ValidationMessageBuilderforDrug, ValidationMessageBuilderForEPCS, ValidationMessageBuilderForEPA)

            If ValidationMessageBuilderforDrug.Length > 0 Then
                MessageBox.Show(ValidationMessageBuilderforDrug.ToString(), "gloEMR", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Information)
                blnValid = False
            End If

            If ValidationMessageBuilderForEPCS.Length > 0 Then
                If MessageBox.Show(ValidationMessageBuilderForEPCS.ToString, "gloEMR", MessageBoxButtons.YesNo, Windows.Forms.MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                    blnValid = False
                Else
                    blnValid = False
                End If
            End If

            If ValidationMessageBuilderForEPA.Length > 0 AndAlso blnValid Then
                Dim msg As String = String.Empty
                msg = "Following drugs have ePA Process pending:" + Environment.NewLine
                msg += ValidationMessageBuilderForEPA.ToString()
                msg += Environment.NewLine + "Do you want to continue doing eRX?"

                If MessageBox.Show(msg, "gloEMR", Windows.Forms.MessageBoxButtons.YesNo, Windows.Forms.MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.No Then
                    blnValid = False
                End If
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
            Return False
        Finally
            If Not IsNothing(ValidationMessageBuilderforDrug) Then
                ValidationMessageBuilderforDrug = Nothing
            End If

            If ValidationMessageBuilderForEPA IsNot Nothing Then
                ValidationMessageBuilderForEPA.Clear()
                ValidationMessageBuilderForEPA = Nothing
            End If


        End Try
        Return blnValid
    End Function

    Public Function ValidateCustomPrescription(ByVal nIndex As Integer, ByVal sRequestType As String, ByRef ValidationMessageBuilderforDrug As System.Text.StringBuilder, ByRef ValidationMessageBuilderForEPCS As System.Text.StringBuilder, ByRef ValidationMessageBuilderForEPA As System.Text.StringBuilder) As Boolean
        'Dim ValidationMessageBuilderforDrug As New System.Text.StringBuilder
        Try
            Dim strQuantity As String = ""
            If Not IsNothing(_RxBusinessLayer.PrescriptionCol.Item(nIndex).Amount) Then
                Dim sdrugAmountArray As String()
                sdrugAmountArray = _RxBusinessLayer.PrescriptionCol.Item(nIndex).Amount.Split(" ")
                If sdrugAmountArray.Length > 0 Then
                    strQuantity = sdrugAmountArray(0).Trim
                End If
            End If

            If IsNothing(_RxBusinessLayer.PrescriptionCol.Item(nIndex).Amount) AndAlso strQuantity.Trim = "" Then
                ValidationMessageBuilderforDrug.Append("This " & sRequestType & " request cannot be sent because the drug quantity is not specified for " & _RxBusinessLayer.PrescriptionCol.Item(nIndex).Medication & ". Please specify the quantity." & vbCrLf)
            ElseIf Not IsNumeric(strQuantity) Then
                ValidationMessageBuilderforDrug.Append("This " & sRequestType & " request cannot be sent because the drug quantity is not a number for " & _RxBusinessLayer.PrescriptionCol.Item(nIndex).Medication & ". Please specify the quantity with a number." & vbCrLf)
            ElseIf Val(strQuantity) = 0 Then
                ValidationMessageBuilderforDrug.Append("This " & sRequestType & " request cannot be sent because the drug quantity is Zero for " & _RxBusinessLayer.PrescriptionCol.Item(nIndex).Medication & ". Please specify the quantity." & vbCrLf)
            End If

            If Not IsNothing(_RxBusinessLayer.PrescriptionCol.Item(nIndex).Frequency) Then
                If _RxBusinessLayer.PrescriptionCol.Item(nIndex).Frequency.Trim = "" Then
                    ValidationMessageBuilderforDrug.Append("This " & sRequestType & " request cannot be sent because the drug directions field is blank for " & _RxBusinessLayer.PrescriptionCol.Item(nIndex).Medication & ". Please enter directions for taking this medication." & vbCrLf)
                End If
            Else
                ValidationMessageBuilderforDrug.Append("This " & sRequestType & " request cannot be sent because the drug directions field is blank for " & _RxBusinessLayer.PrescriptionCol.Item(nIndex).Medication & ". Please enter directions for taking this medication." & vbCrLf)
            End If

            If Not IsNothing(_RxBusinessLayer.PrescriptionCol.Item(nIndex).PotencyCode) Then
                If _RxBusinessLayer.PrescriptionCol.Item(nIndex).PotencyCode.Trim = "" Then
                    ValidationMessageBuilderforDrug.Append("This " & sRequestType & " request cannot be sent because the drug PotencyCode field is blank for " & _RxBusinessLayer.PrescriptionCol.Item(nIndex).Medication & ". Please enter directions for taking this medication." & vbCrLf)
                End If
            Else
                ValidationMessageBuilderforDrug.Append("This " & sRequestType & " request cannot be sent because the drug PotencyCode field is blank for " & _RxBusinessLayer.PrescriptionCol.Item(nIndex).Medication & ". Please enter directions for taking this medication." & vbCrLf)
            End If

            If (_RxBusinessLayer.PrescriptionCol.Item(nIndex).PharmacyName = "") Then
                ValidationMessageBuilderforDrug.Append("This " & sRequestType & " request cannot be sent because the pharmacy field is blank for the drug """ & _RxBusinessLayer.PrescriptionCol.Item(nIndex).Medication & """. Please enter pharmacy for taking this medication." & vbCrLf & "" & vbCrLf)
            End If

            If (_RxBusinessLayer.PrescriptionCol.Item(nIndex).PhNCPDPID = "") Then
                ValidationMessageBuilderforDrug.Append("This " & sRequestType & " request cannot be sent because the pharmacy is not on Surescripts network for the drug """ & _RxBusinessLayer.PrescriptionCol.Item(nIndex).Medication & """. Please select valid pharmacy for taking this medication." & vbCrLf & "" & vbCrLf)
            End If

            If (_RxBusinessLayer.PrescriptionCol.Item(nIndex).PhServiceLevel = "0") Then
                ValidationMessageBuilderforDrug.Append("This " & sRequestType & " request cannot be sent because the pharmacy is inactive for the drug """ & _RxBusinessLayer.PrescriptionCol.Item(nIndex).Medication & """. Please select valid pharmacy for taking this medication." & vbCrLf & "" & vbCrLf)
            End If

            '' If EPCS Enabled than check for pharmacy
            If gbIsProviderEPCSEnable = True Then
                If (_RxBusinessLayer.PrescriptionCol.Item(nIndex).IsNarcotics > 1) Then
                    Dim strServiceLevel As String = String.Empty
                    If _RxBusinessLayer.PrescriptionCol.Item(nIndex).PhServiceLevel <> "" Then
                        strServiceLevel = Convert.ToString(Convert.ToInt64(_RxBusinessLayer.PrescriptionCol.Item(nIndex).PhServiceLevel), 2)
                    Else
                        strServiceLevel = "0000000000000000"
                    End If
                    Dim arr As Char() = strServiceLevel.ToCharArray()
                    Array.Reverse(arr)
                    strServiceLevel = New String(arr)

                    Dim isEPCSPharmacy As Boolean = True
                    If strServiceLevel.Length >= 12 Then
                        If Not Mid(strServiceLevel, 12, 1) = 1 Then
                            isEPCSPharmacy = False
                        End If
                    Else
                        If (_RxBusinessLayer.PrescriptionCol.Item(nIndex).PhServiceLevel <> "0") Then
                            isEPCSPharmacy = False
                        End If
                    End If

                    If Not isEPCSPharmacy Then
                        Dim epcsPharmacyValidationMsg = "This " & sRequestType & " request cannot be sent because the pharmacy does not support EPCS transactions for the drug """ & _RxBusinessLayer.PrescriptionCol.Item(nIndex).Medication

                        If _RxBusinessLayer.PrescriptionCol.Item(nIndex).MessageType = "RefillRequest" Then
                            ValidationMessageBuilderForEPCS.Append(epcsPharmacyValidationMsg)
                            ValidationMessageBuilderForEPCS.Append("""." & vbCrLf & vbCrLf & "This " & sRequestType & " will be printed and will require a wet signature before it can be faxed to the pharmacy or handed over to the patient. Once " & sRequestType & " printed successfully, a DNTF(Denied, New to Follow) message will be sent to the pharmacy. Do you want to continue?" & vbCrLf & "" & vbCrLf)
                        ElseIf _RxBusinessLayer.PrescriptionCol.Item(nIndex).MessageType = "RxChangeRequest" Then
                            ValidationMessageBuilderForEPCS.Append(epcsPharmacyValidationMsg)
                            ValidationMessageBuilderForEPCS.Append("""." & vbCrLf & vbCrLf & "This " & sRequestType & " will be printed and will require a wet signature before it can be faxed to the pharmacy or handed over to the patient." & vbCrLf)
                        Else
                            ValidationMessageBuilderforDrug.Append(epcsPharmacyValidationMsg & """. Please select valid pharmacy for taking this medication." & vbCrLf & "" & vbCrLf)
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
            Return False
        End Try
        Return True

    End Function

    Public Function ValidateNewRxBeforeDenial(ByRef PrintBeforeDeny As Boolean) As Boolean
        Dim ValidationMessageBuilderforDrug As New System.Text.StringBuilder
        Dim ValidationMessageBuilderForEPA As New System.Text.StringBuilder()
        Dim ValidationMessageBuilderForEPCS As New System.Text.StringBuilder()

        Dim blnValid As Boolean = True

        Try
            For icnt As Int32 = 0 To _RxBusinessLayer.PrescriptionCol.Count - 1
                Dim matchfound As Boolean = False
                For j As Int32 = 0 To _RxBusinessLayer.TmpCheckStatesCol.Count - 1
                    matchfound = False
                    If _RxBusinessLayer.TmpCheckStatesCol.Item(j).ItemNumber = _RxBusinessLayer.PrescriptionCol.Item(icnt).ItemNumber.ToString Then
                        matchfound = True
                    End If
                    If matchfound = True Then
                        If _RxBusinessLayer.TmpCheckStatesCol.Item(j).CheckState = True Then
                            If _RxBusinessLayer.TmpCheckStatesCol.Item(j).IssueMethod = "eRx" Or (_RxBusinessLayer.TmpCheckStatesCol.Item(j).IssueMethod = "OTC" And RxOTCActions = OTCDrugAction.IncludeOTC) Then
                                Using prescription As Prescription = _RxBusinessLayer.PrescriptionCol(icnt)
                                    If Not IsNothing(prescription) Then
                                        If Not String.IsNullOrEmpty(prescription.PriorAuthorizationStatus) AndAlso Not String.IsNullOrWhiteSpace(prescription.PriorAuthorizationStatus) AndAlso prescription.PriorAuthorizationStatus.ToLower() = "requested" Then
                                            ValidationMessageBuilderForEPA.AppendLine(prescription.Medication)
                                        End If
                                    End If
                                End Using

                                ValidateCustomPrescription(icnt, "prescription", ValidationMessageBuilderforDrug, ValidationMessageBuilderForEPCS, ValidationMessageBuilderForEPA)

                            End If
                        End If
                    End If
                Next
            Next

            If ValidationMessageBuilderforDrug.Length > 0 Then
                MessageBox.Show(ValidationMessageBuilderforDrug.ToString(), "gloEMR", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Information)
                blnValid = False
            End If

            If ValidationMessageBuilderForEPCS.Length > 0 Then
                If MessageBox.Show(ValidationMessageBuilderForEPCS.ToString, "gloEMR", MessageBoxButtons.YesNo, Windows.Forms.MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                    blnValid = False
                    PrintBeforeDeny = True
                Else
                    blnValid = False
                End If
            End If

            If ValidationMessageBuilderForEPA.Length > 0 AndAlso blnValid Then
                Dim msg As String = String.Empty
                msg = "Following drugs have ePA Process pending:" + Environment.NewLine
                msg += ValidationMessageBuilderForEPA.ToString()
                msg += Environment.NewLine + "Do you want to continue doing eRX?"

                If MessageBox.Show(msg, "gloEMR", Windows.Forms.MessageBoxButtons.YesNo, Windows.Forms.MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.No Then
                    blnValid = False
                End If
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
            Return False
        Finally
            If Not IsNothing(ValidationMessageBuilderforDrug) Then
                ValidationMessageBuilderforDrug = Nothing
            End If

            If ValidationMessageBuilderForEPA IsNot Nothing Then
                ValidationMessageBuilderForEPA.Clear()
                ValidationMessageBuilderForEPA = Nothing
            End If

        End Try
        Return blnValid
    End Function

    Private Function Validate10dot6Data(Optional ByVal selecteditem As Integer = 0) As Boolean
        Dim blnIsValid As Boolean = True
        'Dim ValidationMessageBuilder As New System.Text.StringBuilder
        Dim ValidationMessageBuilderfor10dot6 As New System.Text.StringBuilder
        ' Dim ValidationMessageBuilderforDrug As New System.Text.StringBuilder
        Try
            If Not IsNothing(_RxBusinessLayer.PrescriptionCol) Then
                If _RxBusinessLayer.PrescriptionCol.Count > 0 Then
                    '''''''''''''''''Prescriber Validation------------------------------------------------------------------
                    If Not IsNothing(strPrescriberLastName) Then
                        If strPrescriberLastName.Trim.Length > 35 Then
                            ValidationMessageBuilderfor10dot6.Append("Prescriber LastName(35)")
                        End If
                    End If

                    If Not IsNothing(strPrescriberFirstName) Then
                        If strPrescriberFirstName.Trim.Length > 35 Then
                            ValidationMessageBuilderfor10dot6.Append("Prescriber FirstName(35)")
                        End If
                    End If
                    'Prescriber First Name and Middle Name
                    If Not IsNothing(strPrescriberAddress) Then
                        If strPrescriberAddress.Length > 35 Then
                            ValidationMessageBuilderfor10dot6.Append("Prescriber Address1(35)")
                        End If
                    End If

                    If Not IsNothing(strPrescriberCity) Then
                        If strPrescriberCity.Trim.Length > 35 Then
                            ValidationMessageBuilderfor10dot6.Append("Prescriber City(35)")
                        End If
                    End If
                    If Not IsNothing(strPrescriberState) Then
                        If strPrescriberState.Length > 9 Then
                            ValidationMessageBuilderfor10dot6.Append("Prescriber State(9)")
                        End If
                    End If

                    If Not IsNothing(strPrescriberZip) Then
                        If strPrescriberZip.ToString.Length > 11 Then
                            ValidationMessageBuilderfor10dot6.Append("Prescriber Zip(11)")
                        End If
                    End If
                    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

                    '''''''''''''''''Supervisor Validation------------------------------------------------------------------
                    If cmbSupervisingProvider.Visible = True Then
                        If cmbSupervisingProvider.SelectedIndex <> -1 Then
                            If Not IsNothing(strSupervisingPrescriberLastName) Then
                                If strSupervisingPrescriberLastName.Trim.Length > 35 Then
                                    ValidationMessageBuilderfor10dot6.Append("Supervisor LastName(35)")
                                End If
                            End If

                            If Not IsNothing(strSupervisingPrescriberFirstName) Then
                                If strSupervisingPrescriberFirstName.Trim.Length > 35 Then
                                    ValidationMessageBuilderfor10dot6.Append("Supervisor FirstName(35)")
                                End If
                            End If
                            'Prescriber First Name and Middle Name
                            If Not IsNothing(strSupervisingPrescriberAddress) Then
                                If strSupervisingPrescriberAddress.Length > 35 Then
                                    ValidationMessageBuilderfor10dot6.Append("Supervisor Address1(35)")
                                End If
                            End If

                            If Not IsNothing(strSupervisingPrescriberCity) Then
                                If strSupervisingPrescriberCity.Trim.Length > 35 Then
                                    ValidationMessageBuilderfor10dot6.Append("Supervisor City(35)")
                                End If
                            End If
                            If Not IsNothing(strSupervisingPrescriberState) Then
                                If strSupervisingPrescriberState.Length > 9 Then
                                    ValidationMessageBuilderfor10dot6.Append("Supervisor State(9)")
                                End If
                            End If

                            If Not IsNothing(strSupervisingPrescriberZip) Then
                                If strSupervisingPrescriberZip.ToString.Length > 11 Then
                                    ValidationMessageBuilderfor10dot6.Append("Supervisor Zip(11)")
                                End If
                            End If
                        End If
                    End If
                    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

                    '''''''''Patient Validation------------------------------------------------------------------------------------------------- 

                    If Not IsNothing(strPatientLastName) Then
                        If strPatientLastName.Trim.Length > 35 Then
                            ValidationMessageBuilderfor10dot6.Append("Patient LastName(35)")
                        End If
                    End If

                    If Not IsNothing(strPatientFirstName) Then
                        If strPatientFirstName.Trim.Length > 35 Then
                            ValidationMessageBuilderfor10dot6.Append("Patient FirstName(35)")
                        End If
                    End If


                    '''''10.6 length validations
                    If Not IsNothing(strPatientAddress) Then
                        If strPatientAddress.Trim.Length > 35 Then
                            ValidationMessageBuilderfor10dot6.Append("Patient Address1(35)")
                        End If
                    End If
                    If Not IsNothing(strPatientAddress) Then
                        If strPatientAddress.Trim.Length > 35 Then
                            ValidationMessageBuilderfor10dot6.Append("Patient City(35)")
                        End If
                    End If
                    If Not IsNothing(strPatientState) Then
                        If strPatientState.Trim.Length > 9 Then
                            ValidationMessageBuilderfor10dot6.Append("Patient State(9)")
                        End If
                    End If
                    If Not IsNothing(strPatientZip) Then
                        If strPatientZip.Trim.Length > 11 Then
                            ValidationMessageBuilderfor10dot6.Append("Patient Zip(11)")
                        End If
                    End If

                    '''''10.6 length validations




                    Dim drugname As String = ""


                    ''added in 6060  for multiple pharmacy logic
                    For icnt As Int32 = 0 To _RxBusinessLayer.PrescriptionCol.Count - 1
                        Dim matchfound As Boolean = False
                        For j As Int32 = 0 To _RxBusinessLayer.TmpCheckStatesCol.Count - 1
                            selecteditem = j
                            matchfound = False
                            If _RxBusinessLayer.TmpCheckStatesCol.Item(j).ItemNumber = _RxBusinessLayer.PrescriptionCol.Item(icnt).ItemNumber Then
                                matchfound = True
                            End If
                            If matchfound = True Then
                                If _RxBusinessLayer.TmpCheckStatesCol.Item(selecteditem).CheckState = True Then
                                    If _RxBusinessLayer.TmpCheckStatesCol.Item(selecteditem).IssueMethod = "eRx" Or (_RxBusinessLayer.TmpCheckStatesCol.Item(selecteditem).IssueMethod = "OTC" And RxOTCActions = OTCDrugAction.IncludeOTC) Then
                                        If _RxBusinessLayer.TmpCheckStatesCol.Item(selecteditem).MessageType <> "" Then
                                            If Not IsNothing(_RxBusinessLayer.PrescriptionCol.Item(selecteditem).PharmacyID) Then
                                                If _RxBusinessLayer.PrescriptionCol.Item(selecteditem).PharmacyID.ToString.Length > 35 Then
                                                    ValidationMessageBuilderfor10dot6.Append("Pharmacy NCPDPID(35)")
                                                End If
                                            End If

                                            If Not IsNothing(_RxBusinessLayer.PrescriptionCol.Item(selecteditem).PharmacyName) Then
                                                If _RxBusinessLayer.PrescriptionCol.Item(selecteditem).PharmacyName.Trim.Length > 35 Then
                                                    ValidationMessageBuilderfor10dot6.Append("Pharmacy Name(35)")
                                                End If
                                            End If


                                            If Not IsNothing(_RxBusinessLayer.PrescriptionCol.Item(selecteditem).Frequency) Then
                                                If _RxBusinessLayer.PrescriptionCol.Item(selecteditem).Frequency.Trim.Length > 140 Then
                                                    ValidationMessageBuilderfor10dot6.Append("Drug Directions(140)")
                                                End If
                                            End If



                                            If Not IsNothing(_RxBusinessLayer.PrescriptionCol.Item(selecteditem).Amount) Then
                                                Dim sdrugAmountArray As String()
                                                sdrugAmountArray = _RxBusinessLayer.PrescriptionCol.Item(selecteditem).Amount.Split(" ")
                                                If sdrugAmountArray.Length > 0 Then
                                                    If sdrugAmountArray(0).Trim.Length > 11 Then
                                                        ValidationMessageBuilderfor10dot6.Append("Drug Quantity(11)")
                                                    End If
                                                End If
                                            End If


                                            If _RxBusinessLayer.PrescriptionCol.Item(selecteditem).RefillQualifier <> "PRN" Then
                                                If _RxBusinessLayer.PrescriptionCol.Item(selecteditem).RefillQuantity <> "" Then
                                                    If _RxBusinessLayer.PrescriptionCol.Item(selecteditem).RefillQuantity.Length > 2 Then
                                                        ValidationMessageBuilderfor10dot6.Append("Drug RefillQuantity(2)")
                                                    End If
                                                End If
                                            End If
                                        End If
                                    End If
                                End If
                            End If
                        Next
                    Next
                End If
            End If
            If ValidationMessageBuilderfor10dot6.Length > 0 Then
                If System.Windows.Forms.MessageBox.Show("Following data fields exceed number of characters allowed in Surescripts standards and will therefore be truncated before sending to Surescripts and  (Allowed characters are shown in parenthesis) " + System.Environment.NewLine + System.Environment.NewLine + System.Environment.NewLine + ValidationMessageBuilderfor10dot6.ToString & System.Environment.NewLine + System.Environment.NewLine & System.Environment.NewLine & "Do you want to continue?", "gloEMR", Windows.Forms.MessageBoxButtons.YesNo, Windows.Forms.MessageBoxIcon.Question) = MsgBoxResult.No Then
                    blnIsValid = False
                    Return blnIsValid
                    Exit Function
                End If
            End If
            Return blnIsValid
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
            Return Nothing
        End Try
    End Function

    'Public Sub ReadFileDataInDataset()
    '    Dim ogloInterface As gloSureScript.gloSureScriptInterface = Nothing
    '    RefillRequestDataset = New DataSet
    '    Try
    '        ogloInterface = New gloSureScript.gloSureScriptInterface
    '        ogloInterface.LoadtoDataset(RefillRequestDataset, _RxBusinessLayer.oOldgloPrescription.FileData)
    '    Catch ex As Exception
    '        MessageBox.Show(ex.ToString, gstrMessageBoxCaption)
    '        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
    '        ex = Nothing
    '    Finally
    '        If Not IsNothing(ogloInterface) Then
    '            ogloInterface.Dispose()
    '            ogloInterface = Nothing
    '        End If
    '    End Try
    'End Sub

    Private Sub _MxC1Flexgrid_btnMedRecClick(sender As Object, e As System.EventArgs) Handles _MxC1Flexgrid.btnMedRecClick
        Dim frmMedRec As frmMedReconciliation = Nothing

        Try
            frmMedRec = New frmMedReconciliation(nRxModulePatientID)
            frmMedRec.dtMed = dtMedRec
            'Aniket: Temporary comment to resolve build issue
            frmMedRec.Reconcialtiontype = 2
            frmMedRec.ShowDialog(IIf(IsNothing(frmMedRec.Parent), Me, frmMedRec.Parent))
            blnmedRecupdated = frmMedRec.RecUpdated
            dtMedRec = frmMedRec.dtMed

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally
            If Not IsNothing(frmMedRec) Then
                frmMedRec.Dispose()
                frmMedRec = Nothing
            End If

        End Try

    End Sub
    Private Sub _MxC1Flexgrid_btnSignedAgreementClick(sender As Object, e As System.EventArgs) Handles _MxC1Flexgrid.btnSignedAgreementClick
        Dim frmOpioidAgreement As frmOpioidAgreement = Nothing

        Try
            frmOpioidAgreement = New frmOpioidAgreement(nRxModulePatientID)

            'frmMedRec.dtMed = dtMedRec
            ''Aniket: Temporary comment to resolve build issue
            'frmMedRec.Reconcialtiontype = 2
            frmOpioidAgreement.ShowDialog(IIf(IsNothing(frmOpioidAgreement.Parent), Me, frmOpioidAgreement.Parent))
            'blnmedRecupdated = frmMedRec.RecUpdated
            'dtMedRec = frmMedRec.dtMed

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally
            If Not IsNothing(frmOpioidAgreement) Then
                frmOpioidAgreement.Dispose()
                frmOpioidAgreement = Nothing
            End If
            RefreshAgreementIcon()

        End Try

    End Sub


    Private Sub _MxC1Flexgrid_btnRecHistoryClick(sender As Object, e As System.EventArgs) Handles _MxC1Flexgrid.btnRecHistoryClick
        Dim objrechist As New FrmReconsile_History
        objrechist.PatientID = GetCurrentPatientID
        objrechist.RecType = 2 ''Medication
        objrechist.ShowDialog(Me)
        objrechist.Dispose()
        objrechist = Nothing

    End Sub

    Private Sub _MxC1Flexgrid_InfoButtonDocumentClicked(ByVal templateCode As String, ByVal openFor As String, ByVal TemplateName As String, ByVal sResourceType As String) Handles _MxC1Flexgrid.InfoButtonDocumentClicked
        Dim ofrmPatientEducation As New frmPatientEducationPreview()

        Try
            ofrmPatientEducation.Text = openFor
            ofrmPatientEducation.PATID = GetCurrentPatientID
            ofrmPatientEducation.TempName = TemplateName
            ofrmPatientEducation.Sourc = gloEMRGeneralLibrary.clsInfobutton.enumSource.Medication.GetHashCode()
            ofrmPatientEducation.ResourcCat = gloEMRGeneralLibrary.clsInfobutton.enumResourceCategory.InternalLibrary
            If sResourceType = "Provider Reference Material" Then
                ofrmPatientEducation.ResourcTyp = gloEMRGeneralLibrary.clsInfobutton.enumResourceType.ProviderReferenceMaterial
            Else
                ofrmPatientEducation.ResourcTyp = gloEMRGeneralLibrary.clsInfobutton.enumResourceType.PatientReferenceMaterial
            End If

            ofrmPatientEducation.TMPID = Convert.ToInt64(templateCode)
            ofrmPatientEducation.ISGRID = False
            GloRxToolBarUserCtrl1.Visible = True
            ofrmPatientEducation.ShowDialog()

            ofrmPatientEducation.WindowState = FormWindowState.Maximized
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            pnlmainToolBar.Visible = True
            ofrmPatientEducation.Dispose()
            ofrmPatientEducation = Nothing
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally
            If (IsNothing(ofrmPatientEducation) = False) Then
                ofrmPatientEducation.Close()
            End If
            If (IsNothing(ofrmPatientEducation) = False) Then
                ofrmPatientEducation.Dispose()
                ofrmPatientEducation = Nothing
            End If

        End Try
    End Sub

    Private Sub _MxC1Flexgrid_SaveRxMedStateCheck(ByVal sender As Object, ByVal e As System.EventArgs) Handles _MxC1Flexgrid.SaveRxMedStateCheck
        Try
            SaveRxMeds(True, True)
            _MxC1Flexgrid.VisitIDaftersave = _MedBusinessLayer.CurrentVisitID
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub _MxC1Flexgrid_SaveMedication(ByVal sender As Object, ByVal e As System.EventArgs) Handles _MxC1Flexgrid.SaveMedication
        Try
            SaveRxMeds(True)
            _MxC1Flexgrid.VisitIDaftersave = _MedBusinessLayer.CurrentVisitID
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try

    End Sub

    Private Sub SaveMedicationReconcillation()
        Try
            If Not IsNothing(dtMedRec) Then
                If dtMedRec.Rows.Count > 0 Then
                    Dim newRxRow As DataRow = gloEMRGeneralLibrary.gloGeneral.clsgeneral.ds.Tables("MedicationReconcillation").NewRow()
                    'newRxRow.Item("nPatientID") = dtMedRec.Rows(0)("PatientID")
                    newRxRow.Item("nPatientID") = nRxModulePatientID
                    If _RxBusinessLayer.CurrentVisitID > 0 Then
                        newRxRow.Item("nVisitID") = _RxBusinessLayer.CurrentVisitID
                    Else
                        newRxRow.Item("nVisitID") = _MedBusinessLayer.CurrentVisitID
                    End If

                    'newRxRow.Item("nVisitID") = dtMedRec.Rows(0)("VisitID")
                    newRxRow.Item("bmedicationListProvided") = dtMedRec.Rows(0)("SummaryCheckBox")
                    newRxRow.Item("bMedicationReconcillationPerformed") = dtMedRec.Rows(0)("MedicationCheckBox")
                    newRxRow.Item("dtReconcillationDate") = dtMedRec.Rows(0)("MedDate")
                    newRxRow.Item("snotes") = dtMedRec.Rows(0)("Notes")
                    newRxRow.Item("RowState") = dtMedRec.Rows(0)("RowState")
                    newRxRow.Item("nReconcillationType") = dtMedRec.Rows(0)("ReconcillationType")

                    gloEMRGeneralLibrary.gloGeneral.clsgeneral.ds.Tables("MedicationReconcillation").Rows.Add(newRxRow)
                    If blnmedRecupdated = True Then
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.ClinicalReconciliation, gloAuditTrail.ActivityType.Save, "Medication Reconciliation Added From Medication", nRxModulePatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                    End If
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Function GetMedicationReconcillationDetails(nVisitID As Long, nPatientID As Long, nReconcillationType As Int16) As DataTable
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

    Private Sub _RxC1Flexgrid_SavePrescription(ByVal sender As Object, ByVal e As System.EventArgs) Handles _RxC1Flexgrid.SavePrescription
        Try
            SaveRxMeds(True)
            _RxC1Flexgrid.VisitIDaftersave = _MedBusinessLayer.CurrentVisitID
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Public Function CheckMedicationEndDate() As Boolean
        Dim isEndDateOverDue As Boolean = False
        Try
            Dim resultset As IEnumerable(Of Prescription)
            resultset = _RxBusinessLayer.PrescriptionCol.Where(Function(p) p.Enddate.ToString <> "" And p.Enddate <> "#12:00:00 AM#" And p.Enddate <= Now.Date)

            If resultset.Any Then
                isEndDateOverDue = True
            End If

            If isEndDateOverDue Then
                Dim builder As New System.Text.StringBuilder
                builder.Append("Warning: There are Active Medications with overdue End Dates.")
                builder.AppendLine()
                builder.AppendLine()
                builder.Append("Continue with Save?")
                builder.AppendLine()
                builder.AppendLine()

                ' Get internal String value from StringBuilder
                Dim strmessage As String = builder.ToString
                Dim dgResult As DialogResult
                dgResult = System.Windows.Forms.MessageBox.Show(strmessage, gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
                If dgResult = DialogResult.Yes Then
                    isEndDateOverDue = False
                    isEndDuedateMessageOccured = True
                Else
                    isEndDateOverDue = True
                    isEndDuedateMessageOccured = False
                End If
            End If
        Catch ex As Exception
            MessageBox.Show("ERROR : " & ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
        Return isEndDateOverDue
    End Function

    Public Function CheckDrugStatus() As Boolean
        Dim isUnKnownDrugPresent As Boolean = False
        Try

            For icnt As Integer = 0 To _MedBusinessLayer.MedicationCol.Count - 1
                If _MedBusinessLayer.MedicationCol.Item(icnt).State.ToString <> "U" Then
                    If _MedBusinessLayer.MedicationCol.Item(icnt).Status.ToString = "Unknown" Then
                        isUnKnownDrugPresent = True
                        Exit For
                    End If
                End If
            Next

            If isUnKnownDrugPresent = True Then
                Dim builder As New System.Text.StringBuilder
                builder.Append("Warning: There are Medications with Unknown Status.")
                builder.AppendLine()
                builder.AppendLine()
                builder.Append("Continue with Save?")
                builder.AppendLine()
                builder.AppendLine()
                Dim dgResult As DialogResult
                dgResult = System.Windows.Forms.MessageBox.Show(builder.ToString, gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
                If dgResult = DialogResult.Yes Then
                    isUnKnownDrugPresent = False
                End If
                builder.Clear()
                builder = Nothing
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString, True)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
        Return isUnKnownDrugPresent
    End Function

    Public Sub SetThresholdPeriod()
        Dim stRxEligThresholdvalue As String = ""
        Dim objSettings As New clsSettings

        Try
            If gstrRxEligThresholdvalue = "" Then
                stRxEligThresholdvalue = objSettings.GetSettingValue("RX ELIGIBILITY THRESHOLD VALUE")
                gstrRxEligThresholdvalue = (Val(stRxEligThresholdvalue) / 24).ToString
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.Eligibility, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally
            If Not IsNothing(objSettings) Then
                objSettings.Dispose() : objSettings = Nothing
            End If
        End Try
    End Sub

    Private Sub _RxC1Flexgrid_StripItemClick(sender As Object, e As System.EventArgs) Handles _RxC1Flexgrid.StripItemClick
        Try
            RemoveChangeMed()
            RemoveControl()
            RemoveDrugInfoControl()
            CloseFormularyAndCOVPnl()

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.Close, ex.InnerException.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub CloseFormularyAndCOVPnl()
        Try
            If pnlFormulary IsNot Nothing Then
                pnlFormulary.Visible = False
            End If

            If pnlElementHostCopay IsNot Nothing Then
                pnlElementHostCopay.Visible = False
            End If

            If pnlFormularyCoverage IsNot Nothing Then
                pnlFormularyCoverage.Visible = False
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.Formulary30, gloAuditTrail.ActivityType.None, "FS3_CloseFormularyAndCOVPnl :" + If(ex.InnerException IsNot Nothing, ex.InnerException, ex).ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Function CheckUnspecified() As Boolean
        Dim _CheckUnspecified As Boolean = True
        Try
            Dim resultset As IEnumerable(Of Prescription)
            resultset = _RxBusinessLayer.PrescriptionCol.Where(Function(p) p.Amount.Trim.Contains("Unspecified") And p.State <> "D")
            If resultset.Any Then
                Dim dgresult As DialogResult
                dgresult = MessageBox.Show("There are some drugs with Unspecified Unit of Measure. Do you want to continue? ", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
                If dgresult = Windows.Forms.DialogResult.No Then
                    _CheckUnspecified = False
                    isUnspecifiedMessageOccured = False
                Else
                    isUnspecifiedMessageOccured = True
                End If
            Else
                isUnspecifiedMessageOccured = True
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.Drugs, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
            isUnspecifiedMessageOccured = True
        End Try
        Return _CheckUnspecified
    End Function

    Private Function CheckUnIssuedDrug() As Boolean
        Dim SaveUnIssueDrug As Boolean = True
        isUnIssuedAlertOccured = False
        Try
            Dim resultset As IEnumerable(Of Prescription)
            resultset = _RxBusinessLayer.PrescriptionCol.Where(Function(p) p.State = "A" And (p.Method = "Print" Or (p.Method = "eRx" And p.eRxStatus = "")))
            If resultset.Any Then
                Dim dgresult As DialogResult
                dgresult = MessageBox.Show("There are drugs which are not issued and will be saved. Do you want to continue? ", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                isUnIssuedAlertOccured = True
                If dgresult = Windows.Forms.DialogResult.No Then
                    SaveUnIssueDrug = False
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.Drugs, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
        Return SaveUnIssueDrug
    End Function

    Private Function CheckOTCDrug(ByVal IssueMethod As String) As Boolean
        Dim IsOTCDrugPresent As Boolean = False
        Try
            Dim strOTCDrug As String = ""
            Dim StrMessageMethod As String = ""
            Dim strYesButonCaption As String = ""
            Dim strNoButonCaption As String = ""

            Dim resultset As IEnumerable(Of Prescription)
            resultset = _RxBusinessLayer.PrescriptionCol.Where(Function(p) p.Method <> IssueMethod And p.Method = "OTC")
            For Each Drug As Prescription In resultset
                For i As Int32 = 0 To _RxBusinessLayer.TmpCheckStatesCol.Count - 1
                    If Drug.ItemNumber = _RxBusinessLayer.TmpCheckStatesCol.Item(i).ItemNumber Then
                        If _RxBusinessLayer.TmpCheckStatesCol.Item(i).CheckState = True Then
                            If strOTCDrug = "" Then
                                strOTCDrug = Drug.Medication
                            Else
                                strOTCDrug = strOTCDrug & ", " & Drug.Medication
                            End If
                        End If
                        Exit For
                    End If
                Next
            Next

            Select Case IssueMethod
                Case "Print"
                    StrMessageMethod = "printed"
                    strYesButonCaption = "Print with OTC"
                    strNoButonCaption = "Print without OTC"
                Case "Fax"
                    StrMessageMethod = "faxed"
                    strYesButonCaption = "Fax with OTC"
                    strNoButonCaption = "Fax without OTC"
                Case "eRx"
                    StrMessageMethod = "transmitted"
                    strYesButonCaption = "eRx with OTC"
                    strNoButonCaption = "eRx without OTC"
            End Select

            If IssueMethod = "IssueRx" Then
                If gblnIsOTCIssueWarningEnabled Then
                    If strOTCDrug.Length > 0 Then
                        Dim dialogResult As DialogResult
                        Dim strmessage As String = "Warning - At least one prescription is OTC." & vbNewLine & " OTC prescriptions will not be issued." & vbNewLine & " " & strOTCDrug & "."
                        dialogResult = MessageBox.Show(strmessage, gstrMessageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning)
                        If dialogResult = Windows.Forms.DialogResult.Cancel Then
                            RxOTCActions = OTCDrugAction.Cancel
                        Else
                            RxOTCActions = OTCDrugAction.ExcludeOTC
                        End If
                    End If
                Else
                    RxOTCActions = OTCDrugAction.ExcludeOTC
                End If
            ElseIf IssueMethod = "Print" Or IssueMethod = "Fax" Or IssueMethod = "eRx" Then
                If gblnIsOTCIssueWarningEnabled Then
                    If strOTCDrug.Length > 0 Then
                        Dim strmessage As String = "Warning - At least one prescription is OTC." & vbNewLine & " OTC prescriptions will not be  " & StrMessageMethod & "." & vbNewLine & " " & strOTCDrug & "."
                        Dim msg As New MyCustomMessageBox(strmessage, strYesButonCaption, strNoButonCaption)
                        Dim result = msg.ShowDialog()
                        If result = Windows.Forms.DialogResult.Yes Then
                            RxOTCActions = OTCDrugAction.IncludeOTC
                        ElseIf result = Windows.Forms.DialogResult.No Then
                            RxOTCActions = OTCDrugAction.ExcludeOTC
                        Else
                            IsOTCDrugPresent = True
                            RxOTCActions = OTCDrugAction.Cancel
                        End If
                    End If
                Else
                    RxOTCActions = OTCDrugAction.IncludeOTC
                End If
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.Drugs, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
        Return IsOTCDrugPresent
    End Function

    Private Sub btnAllergies_Click(sender As System.Object, e As System.EventArgs) Handles btnAllergies.Click
        Call GetAllergies()
        If Not pnlAllergiesAlerts.Visible Then
            pnlAllergiesAlerts.Visible = True
            'pnlAllergiesAlerts.BringToFront()
        Else
            pnlAllergiesAlerts.BringToFront()
        End If
    End Sub

    Private Sub btnAllergies_MouseHover(sender As Object, e As System.EventArgs) Handles btnAllergies.MouseHover
        If sender IsNot Nothing Then
            Dim btn As Button = sender
            If btn IsNot Nothing Then
                btn.BackgroundImage = gloEMR.My.Resources.Img_LongOrange()
                btn.BackgroundImageLayout = ImageLayout.Stretch
            End If
        End If
    End Sub

    Private Sub btnAllergies_MouseLeave(sender As Object, e As System.EventArgs) Handles btnAllergies.MouseLeave
        If sender IsNot Nothing Then
            Dim btn As Button = sender
            If btn IsNot Nothing Then
                btn.BackgroundImage = gloEMR.My.Resources.Img_LongButton()
                btn.BackgroundImageLayout = ImageLayout.Stretch
            End If
        End If
    End Sub

    ''' <summary>
    ''' This event is implemented to manage all the formulary 3.0 requirements (including eRx Measure for MU2
    ''' </summary>
    ''' <param name="SelectedDrug"></param>
    ''' <param name="mpid"></param>
    ''' <param name="rxType"></param>
    ''' <param name="IsFormularyQueried"></param>
    ''' <param name="SelectedRow"></param>
    ''' <remarks></remarks>
    Private Sub _RxC1Flexgrid_DrugFormularyQueried(SelectedDrug As String, mpid As Int64, rxType As String, IsFormularyQueried As Boolean, SelectedRow As Integer, ByVal FormularyQueriedFrom As FormularyQueriedFrom)


        Dim service As New gloFSHelper(clsgeneral.gstrFormularyServiceURL, GetConnectionString(), clsgeneral.sDIBServiceURL)
        Dim status As New FormularyStatus(mpid)

        Dim altList As New FormularyAlternatives

        Try
            AdjustFormularyControls()

            pnlFormularyProgress.Visible = True
            pnlFormularyProgress.BringToFront()
            Application.DoEvents()

            If _objFormularyToolBar.CurrentEligibilityStatus <> RxBusinesslayer.EligibilityStatus.NotChecked Then
                _RxBusinessLayer.PrescriptionCol.Item(SelectedRow - 1).IsFormularyQueried = True
            End If

            '' Get formulary status for selected drug
            status = service.GetFormularyStatus(SenderId, FormularyId, New FormularyDrug(mpid, rxType))


           

            If status IsNot Nothing Then

                lblCoverageCopayHeading.Text = "Cov/Copay : " + SelectedDrug
                Call DisplayCopay(status, SelectedRow)

                '' Get the list of alternatives from service
                altList = service.GetAlternativeList(SenderId, FormularyId, CopayID, AlternativeId, mpid, rxType, status.fs)

                If (altList IsNot Nothing AndAlso altList.AlternativeList IsNot Nothing AndAlso altList.AlternativeList.Count > 0) Then

                    If Me.AlternativesControl Is Nothing Then
                        Me.AlternativesControl = New gloFormularyAlternatives(gblnShowNDCInAlternatives)
                        AddHandler Me.AlternativesControl.DrugAccepted, AddressOf AcceptAlternativeInfo
                        AddHandler Me.AlternativesControl.DrugSelectedEvent, AddressOf DrugSelected
                        Me.elementHost.Child = AlternativesControl
                    End If

                    AlternativesControl.DataContext = altList
                    AlternativesControl.RefreshSearch()

                    pnlFormularyProgress.Visible = False

                    If pnlDIScreenResult IsNot Nothing AndAlso pnlDIScreenResult.Controls.Count = 0 Then
                        pnlFormulary.Visible = True
                    Else
                        pnlFormulary.Visible = False
                    End If


                    If altList.ListType = AltListType.Payer Then
                        lblFormularyDrugName.Text = "Payer Alternatives : " + SelectedDrug
                    ElseIf altList.ListType = AltListType.DrugSpecific Then
                        lblFormularyDrugName.Text = "Therapeutic Alternatives : " + SelectedDrug
                    End If
                Else
                    pnlFormulary.Visible = False
                    pnlFormularyProgress.Visible = False

                    If Me.AlternativesControl IsNot Nothing Then
                        Me.AlternativesControl.DataContext = Nothing
                    End If

                    If FormularyQueriedFrom = gloGlobal.FS3.FormularyQueriedFrom.ShowAlternatives Then
                        MessageBox.Show("No alternatives available for " + SelectedDrug, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                End If
            End If
        Catch wex As WebException
            pnlFormularyProgress.Visible = False
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.Formulary30, gloAuditTrail.ActivityType.None, "FS3_PBMRefresh :" + If(wex.InnerException IsNot Nothing, wex.InnerException, wex).ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show("Unable to retrieve formulary information.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.Formulary30, gloAuditTrail.ActivityType.None, "FS3_DrugFormularyQuried : " + If(ex.InnerException IsNot Nothing, ex.InnerException, ex).ToString(), gloAuditTrail.ActivityOutCome.Failure)
        Finally
            pnlFormularyProgress.Visible = False

            If service IsNot Nothing Then
                service = Nothing
            End If
            If status IsNot Nothing Then
                status = Nothing
            End If
            If altList IsNot Nothing Then
                altList = Nothing
            End If
        End Try

    End Sub

    Private Sub _RxC1Flexgrid_CancelRxRequested(ByVal SelectedRow As Integer, ByVal flag As String)

        Dim dtMessages As DataTable = Nothing
        Dim RetMsg As String = Nothing
        Dim ss_helper As gloSureScript.gloSurescriptsHelper = Nothing
        Dim selectedRx As Prescription = _RxBusinessLayer.PrescriptionCol(SelectedRow)

        Try
            If IsNothing(selectedRx) Then
                Exit Sub
            End If

            ss_helper = New gloSureScript.gloSurescriptsHelper(gstrSurescriptServiceURL)

            gloSureScript.gloSurescriptGeneral.ServerName = globalSecurity.gstrSQLServerName
            gloSureScript.gloSurescriptGeneral.DatabaseName = globalSecurity.gstrDatabaseName

            If Not Validate10dot6Data(SelectedRow) Then
                Exit Sub
            End If

            If Not ValidateCancelRxBeforeDenial(SelectedRow) Then
                Exit Sub
            End If

            If selectedRx.IseRxed Then
                Using helper As New gloEMRGeneralLibrary.PrescriptionBusinessLayer()
                    dtMessages = helper.GeteRxDetailsForCancelRx(selectedRx.PrescriptionID)
                End Using
            End If

            Dim sMsg As String = ""

            If flag = "C" Then
                sMsg = "You are about to cancel this prescription : " & selectedRx.Medication & System.Environment.NewLine() & "Are you sure ? "
                lblFormularyTransactionMessage.Text = "Sending Rx cancellation request..."
            ElseIf flag = "D" Then
                sMsg = "You are about to discontinue this prescription : " & selectedRx.Medication & System.Environment.NewLine() & "Are you sure ? "
                lblFormularyTransactionMessage.Text = "Sending Rx discontinue request..."
            End If

            If MessageBox.Show(sMsg, gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = Windows.Forms.DialogResult.No Then
                Exit Sub
            End If


            pnlFormularyTransactionMessage.Visible = True
            pnlFormularyTransactionMessage.BringToFront()
            Application.DoEvents()

            Dim medPrescribed As ServiceObjectBase.MedPrescribed = FillMedPrescribed(selectedRx)

            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CancelRx, gloAuditTrail.ActivityType.CancelOperation, "Sending CancelRx request", nRxModulePatientID, selectedRx.PrescriptionID, gnLoginProviderID, gloAuditTrail.ActivityOutCome.Success)

            If selectedRx.IseRxed Then
                RetMsg = ss_helper.SendCancelRxMessage(selectedRx.PrescriptionID, medPrescribed, _patientInfo, dtMessages, flag, globalSecurity.gstrLoginName)
            Else
                If _pharmacyInfo Is Nothing Then
                    Using dtPharmacy As DataTable = _RxBusinessLayer.GetPharmacyDetails(selectedRx.PharmacyID)
                        If dtPharmacy IsNot Nothing Then
                            If dtPharmacy.Rows.Count > 0 Then
                                _pharmacyInfo = dtPharmacy.Rows(0)
                            End If
                        End If
                    End Using
                End If

                RetMsg = ss_helper.SendCancelRxMessage(selectedRx.PrescriptionID, medPrescribed, _patientInfo, _providerInfo, _pharmacyInfo, flag, globalSecurity.gstrLoginName)
            End If

            If Me.pnlRefill.Controls.Contains(objCustomPrescription) = True Then
                RemoveControl()
            End If

            pnlFormularyTransactionMessage.Visible = False

            If Not String.IsNullOrEmpty(RetMsg) Then
                MessageBox.Show(RetMsg, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)

                _RxBusinessLayer.FetchPrescriptionforUpdate(Now, 1, selectedRx.PrescriptionID)

                BindFlexGrid_RX()

                If _MedBusinessLayer.MedicationCol IsNot Nothing Then
                    _MedBusinessLayer.MedicationCol.Clear()
                End If

                _MedBusinessLayer.FetchMedicationforUpdate()
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CancelRx, gloAuditTrail.ActivityType.None, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        Finally
            pnlFormularyTransactionMessage.Visible = False
        End Try
    End Sub



    Private Function FillMedPrescribed(ByVal selectedRx As Prescription) As ServiceObjectBase.MedPrescribed

        Dim medPrescribed As ServiceObjectBase.MedPrescribed = New ServiceObjectBase.MedPrescribed()
        Try
            If selectedRx IsNot Nothing Then

                medPrescribed.medication = selectedRx.Medication
                medPrescribed.ndc = selectedRx.NDCCode


                If selectedRx.Amount.Trim <> "" Then 'fixed bug 5453
                    Dim strDispense As String() = Split(selectedRx.Amount.Trim, " ")
                    If strDispense.Length > 1 Then
                        medPrescribed.qty = strDispense(0)
                    Else
                        medPrescribed.qty = selectedRx.Amount.Trim
                    End If
                Else
                    medPrescribed.qty = selectedRx.Amount.Trim
                End If

                medPrescribed.qtyUnit = selectedRx.PotencyCode

                Dim nDaysSupply As Integer = 0
                If selectedRx.Duration.Trim.Length > 0 AndAlso Val(selectedRx.Duration) <> 0 Then
                    If IsNumeric(selectedRx.Duration) Then
                        nDaysSupply = Val(selectedRx.Duration)
                    Else
                        Dim nDuration As String() = Nothing
                        Dim numberofDays As Integer
                        nDuration = selectedRx.Duration.Trim.Split(" ")
                        If nDuration.Length > 0 Then
                            Select Case nDuration(1).ToUpper
                                Case "MONTHS"
                                    numberofDays = 30
                                Case "DAYS"
                                    numberofDays = 1
                                Case "WEEKS"
                                    numberofDays = 7
                            End Select
                            nDaysSupply = numberofDays * CType(nDuration(0), Integer)
                        End If
                    End If
                End If

                medPrescribed.days = nDaysSupply

                medPrescribed.direction = selectedRx.Frequency ' DrugRoute

                medPrescribed.refill = selectedRx.Refills
                'medPrescribed.re

                Select Case selectedRx.RefillQualifier
                    Case "PRN"
                        '"PRN"
                    Case "R"
                        ' "R"
                    Case "P"
                        '"P"
                End Select

                medPrescribed.dea = selectedRx.IsNarcotics

                medPrescribed.substitute = selectedRx.Maysubstitute

                medPrescribed.written = selectedRx.Prescriptiondate

                medPrescribed.note = selectedRx.Notes

                medPrescribed.pan = selectedRx.PriorAuthorizationNumber

                medPrescribed.pas = selectedRx.PriorAuthorizationStatus


                'Diagnosis

                Dim sICDRevPrimary As String = Nothing
                Dim sICDCodePrimary As String = Nothing

                Dim sICDRevSecondary As String = Nothing
                Dim sICDCodeSecondary As String = Nothing

                Dim dtDiagnosis As DataTable = Nothing
                If Not String.IsNullOrEmpty(selectedRx.Problems) AndAlso Not String.IsNullOrWhiteSpace(selectedRx.Problems) Then
                    Using oPrescriptionLayer As New gloEMRGeneralLibrary.PrescriptionBusinessLayer()
                        dtDiagnosis = oPrescriptionLayer.GetDiagnosisCodes(selectedRx.Problems)

                        For Each row As DataRow In dtDiagnosis.Rows
                            If IsNothing(sICDCodePrimary) Then
                                sICDRevPrimary = Convert.ToString(row("sICDRevision"))
                                sICDCodePrimary = Convert.ToString(row("sICD9Code"))
                            Else
                                If IsNothing(sICDCodeSecondary) Then
                                    sICDRevSecondary = Convert.ToString(row("sICDRevision"))
                                    sICDCodeSecondary = Convert.ToString(row("sICD9Code"))
                                End If
                            End If
                        Next

                    End Using
                End If
                dtDiagnosis = Nothing

                If Not String.IsNullOrEmpty(sICDRevPrimary) And Not String.IsNullOrEmpty(sICDCodePrimary) Then
                    If sICDRevPrimary = "10" Then
                        medPrescribed.DxQual1 = "ABF"
                    Else
                        medPrescribed.DxQual1 = "DX"
                    End If
                    medPrescribed.DxVal1 = sICDCodePrimary

                End If

                If Not String.IsNullOrEmpty(sICDRevSecondary) And Not String.IsNullOrEmpty(sICDCodeSecondary) Then
                    If sICDRevSecondary = "10" Then
                        medPrescribed.DxQual2 = "ABF"
                    Else
                        medPrescribed.DxQual2 = "DX"
                    End If
                    medPrescribed.DxVal2 = sICDCodeSecondary

                End If
            End If


        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CancelRx, gloAuditTrail.ActivityType.None, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try

        Return medPrescribed

    End Function

    Private Sub _RxC1Flexgrid_DrugFormularyRequested(SelectedRow As Integer)
        If _objFormularyToolBar.CurrentEligibilityStatus <> RxBusinesslayer.EligibilityStatus.NotChecked Then

            _RxC1Flexgrid.ShowFormulary(SelectedRow)
        End If
    End Sub

    'Private Sub _RxC1Flexgrid_PDRInfoButtonClicked(SelectedRow As Integer)
    '    Dim dbLayer As New PatientCommunicationBusinessLayer()
    '    Dim sBuilder As StringBuilder = Nothing

    '    Using rx As Prescription = _RxBusinessLayer.PrescriptionCol(SelectedRow - 1)
    '        If rx.PCTransactionID > 0 Then
    '            rx.PDRPrograms = dbLayer.GetAllPrograms(rx.PCTransactionID, "ALL")
    '            sBuilder = New StringBuilder
    '            If rx.PDRPrograms IsNot Nothing AndAlso DirectCast(rx.PDRPrograms, ProgramResponse).Programs.Count > 0 Then
    '                sBuilder.Append("Available Programs : " + vbNewLine)
    '                sBuilder.Append(_RxBusinessLayer.PrescriptionCol(SelectedRow - 1).Medication + " - ")
    '                For Each p As Program In DirectCast(rx.PDRPrograms, ProgramResponse).Programs
    '                    sBuilder.Append(p.name + " ")
    '                    sBuilder.Append(vbNewLine)
    '                Next
    '                MessageBox.Show(sBuilder.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
    '            End If

    '        End If
    '    End Using
    'End Sub

    Public Sub DisplayCopay(ByVal status As FormularyStatus, Optional ByVal SelectedRow As Integer = -1)

        Dim service As New gloFSHelper(clsgeneral.gstrFormularyServiceURL, GetConnectionString(), clsgeneral.sDIBServiceURL)
        Dim coverage As New CoverageFactor
        Dim copay As FormularyCopay = Nothing

        Try
            ''Get the coverage information
            coverage = service.GetCoverage(SenderId, CoverageID, status.id)

            If coverage IsNot Nothing Then
                If coverage.de Then
                    status.fs = "0"
                End If
            End If

            copay = service.GetCopay(SenderId, CopayID, status)

            If SelectedRow >= 0 Then
                ''Update the prescrion grid with Status & Abbr Copay
                _RxC1Flexgrid.UpdateDrugFormularyInfo(SelectedRow, status, copay)
            End If

            If copay IsNot Nothing OrElse coverage IsNot Nothing Then
                '' Create a group for copay/coverage for control binding
                Dim CopayCoverageGroup As New CopayCoverageGroup
                With CopayCoverageGroup
                    .Copay = copay
                    .Coverage = coverage
                End With

                If Me.CopayControl Is Nothing Then
                    Me.CopayControl = New gloCopayControl()
                    Me.elementHostCopay.Child = CopayControl
                End If

                CopayControl.DataContext = CopayCoverageGroup
                If pnlDIScreenResult IsNot Nothing AndAlso pnlDIScreenResult.Controls.Count = 0 Then
                    pnlElementHostCopay.Visible = True
                Else
                    pnlElementHostCopay.Visible = False
                End If


            Else
                pnlElementHostCopay.Visible = False

                If Me.CopayControl IsNot Nothing Then
                    Me.CopayControl.DataContext = Nothing
                End If
            End If
        Catch wex As WebException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.Formulary30, gloAuditTrail.ActivityType.None, "FS3_PBMRefresh :" + If(wex.InnerException IsNot Nothing, wex.InnerException, wex).ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show("Unable to retrieve formulary information.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.Formulary30, gloAuditTrail.ActivityType.None, "FS3_DisplayCopay : " + If(ex.InnerException IsNot Nothing, ex.InnerException, ex).ToString(), gloAuditTrail.ActivityOutCome.Failure)
        Finally
            If service IsNot Nothing Then
                service = Nothing
            End If
        End Try
    End Sub

    Public Sub DrugSelected(ByVal e As Object)
        Try
            If TypeOf e Is gloGlobal.DIB.AlternativeDrugDetails Then
                Using altDrug As gloGlobal.DIB.AlternativeDrugDetails = DirectCast(e, gloGlobal.DIB.AlternativeDrugDetails)
                    Using status As New FormularyStatus(altDrug.id, altDrug.fs)
                        lblCoverageCopayHeading.Text = "Cov/Copay : " + altDrug.DrugName
                        Call DisplayCopay(status)
                    End Using
                End Using
            End If
        Catch wex As WebException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.Formulary30, gloAuditTrail.ActivityType.None, "FS3_PBMRefresh :" + If(wex.InnerException IsNot Nothing, wex.InnerException, wex).ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show("Unable to retrieve formulary information.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.Formulary30, gloAuditTrail.ActivityType.None, "FS3_DrugSelected : " + If(ex.InnerException IsNot Nothing, ex.InnerException, ex).ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Public Sub AcceptAlternativeInfo(ByVal Argument As Object)

        If TypeOf Argument Is gloGlobal.DIB.AlternativeDrugDetails AndAlso _RxBusinessLayer IsNot Nothing Then

            Dim alternativeObject As gloGlobal.DIB.AlternativeDrugDetails = DirectCast(Argument, gloGlobal.DIB.AlternativeDrugDetails)

            Dim NDCCode As String = alternativeObject.NDC
            Dim FormularyStatus As String = alternativeObject.fs

            Dim objprescription As Prescription = Nothing

            Try
                If IsNothing(NDCCode) Then
                    MessageBox.Show("NDCCode is not present against this drug, the Drug will not be accepted!", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If


                If _RxBusinessLayer.CheckObsoleteDrug(NDCCode) Then
                    System.Windows.Forms.MessageBox.Show("This drug is an obsolete drug and cannot be prescribed!", gstrMessageBoxCaption, Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Warning)
                    If Not objprescription Is Nothing Then
                        objprescription.Dispose()
                    End If
                    Exit Sub
                End If

                Dim AlternativeInfo As DataRow = Nothing
                objprescription = _RxBusinessLayer.FetchDrugDetailsByNDC(NDCCode)

                objprescription.PrescriptionID = 0
                objprescription.IseRxed = 0
                objprescription.VisitID = _RxBusinessLayer.CurrentVisitID   'Set VisitId as passed from Visit form

                If _RxBusinessLayer.TransactionMode = RxBusinesslayer._TransactionMode.Edit Then
                    objprescription.Prescriptiondate = _RxBusinessLayer.PrescriptionDate    'Set date as Old Prescription Date
                End If

                objprescription.PatientID = nRxModulePatientID
                objprescription.Frequency = ""
                objprescription.Refills = ""
                objprescription.Duration = ""
                objprescription.Startdate = Now.Date
                objprescription.UserName = gstrLoginName
                objprescription.Notes = ""
                objprescription.Method = "eRx"
                objprescription.Maysubstitute = True

                objprescription.Amount = ""
                objprescription.DrugID = 0

                objprescription.ProviderID = _RxBusinessLayer.ProviderID
                objprescription.PharmacyID = _RxBusinessLayer.PharmacyId

                objprescription.FormularyStatus = FormularyStatus 'AlternativeInfo("sStatus")            

                Using prescriptionLayer As New gloEMRGeneralLibrary.PrescriptionBusinessLayer()
                    Using DrugMstDetails As DataTable = prescriptionLayer.GetMPIDByNDC(objprescription.NDCCode)
                        If Not IsNothing(DrugMstDetails) AndAlso DrugMstDetails.Rows.Count > 0 Then
                            objprescription.mpid = DrugMstDetails.Rows(0)("mpid")
                            objprescription.DrugID = DrugMstDetails.Rows(0)("DrugId")
                        End If
                    End Using
                End Using

                objprescription.State = "A"
                _RxBusinessLayer.PrescriptionCol.Add(objprescription)


                Try
                    RaiseEvent PerformEPAStatusCheck(_RxBusinessLayer.PrescriptionCol.Count - 1)
                    'Me.PerformPDRStatusCheck(_RxBusinessLayer.PrescriptionCol.Count - 1)
                    RaiseEvent PerformDrugAlertCheck(True)

                    _RxC1Flexgrid.AddNewPrescription(_RxBusinessLayer.PrescriptionCol.Item(_RxBusinessLayer.PrescriptionCol.Count - 1), _objFormularyToolBar.CurrentEligibilityStatus)

                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.Formulary30, gloAuditTrail.ActivityType.None, "FS3_AcceptAlternative :" + If(ex.InnerException IsNot Nothing, ex.InnerException, ex).ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    ex = Nothing
                End Try

            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.Formulary30, gloAuditTrail.ActivityType.None, "FS3_AcceptAlternative :" + If(ex.InnerException IsNot Nothing, ex.InnerException, ex).ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Finally
                If objprescription IsNot Nothing Then
                    objprescription.Dispose()
                    objprescription = Nothing
                End If
            End Try
        End If
    End Sub

    Private Sub btnCloseCopayCoverage_Click(sender As System.Object, e As System.EventArgs) Handles btnCloseCopayCoverage.Click
        pnlElementHostCopay.Visible = False
    End Sub

    Private Sub _RxListUserCtrl_DrugListClicked(Row As DataRow, DrugRoutes As List(Of String))
        Try
            If IsFormLocked Then
                Exit Sub
            End If

            If Not IsNothing(objCustomPrescription) Then
                RemoveControl()
            End If

            If Not IsNothing(objCustomMedication) Then
                RemoveCustomMedicationControl()
            End If

            RemoveMedRefillControl()
            RemoveRefillControl()
            If blnRxC1FlexClick = True Then
                ' _RxBusinessLayer.routes = DrugRoutes
                _RxBusinessLayer.AddNewPrescription(Row, DrugRoutes)
                NewPrescriptionAdded()
            Else
                ' _MedBusinessLayer.routes = DrugRoutes
                _MedBusinessLayer.AddNewMedication(Row, DrugRoutes)
                NewMedicationAdded()
            End If


            If pnlPrescriptionGrid.Top > pnlMedicationGrid.Top Then
                pnlPrescriptionGrid.Dock = DockStyle.Fill
                pnlMedicationGrid.Dock = DockStyle.Top
                pnlRefill.SendToBack()
            Else
                pnlPrescriptionGrid.Dock = DockStyle.Top
                pnlMedicationGrid.Dock = DockStyle.Fill
                pnlRefill.SendToBack()
            End If

        Catch ex As gloUserControlExceptions
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

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
                      "WHERE nVisitId= " & CurrentVisitId & " and nPatientId=" & nRxModulePatientID & " and nReconcillationType=" & 2 & ""
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


    Private Sub GloRxToolBarUserCtrl1_tStrpRxFillClick(sender As Object, e As System.EventArgs) Handles GloRxToolBarUserCtrl1.tStrpRxFillClick
        Try

            Dim frmRxFillNotifications As New frmRxFillNotifications(nRxModulePatientID)
            frmRxFillNotifications.WindowState = FormWindowState.Maximized
            frmRxFillNotifications.ShowDialog(IIf(IsNothing(frmRxFillNotifications.Parent), Me, frmRxFillNotifications.Parent))


            If Not IsNothing(frmRxFillNotifications) Then
                frmRxFillNotifications.Dispose()
                frmRxFillNotifications = Nothing
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CancelRx, gloAuditTrail.ActivityType.Cancle, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing

        End Try
    End Sub

    Private Sub GloRxToolBarUserCtrl1_tStrpPlanOfTreatmentClick(sender As Object, e As System.EventArgs) Handles GloRxToolBarUserCtrl1.tStrpPlanOfTreatment_Click
        Try

            Dim oPlanOfTreatment As New frmTreatmentPlan(nRxModulePatientID, "RxMed")
            oPlanOfTreatment.ShowInTaskbar = False
            oPlanOfTreatment.ShowDialog(Me)

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CancelRx, gloAuditTrail.ActivityType.Cancle, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing

        End Try
    End Sub
    Private Sub GloRxToolBarUserCtrl1_tStrpNKMedication_Click(sender As Object, e As System.EventArgs) Handles GloRxToolBarUserCtrl1.tStrpNKMedication_Click
        Try


            Dim PatspecCDAspc As gloCCDLibrary.frmPatspecCDAspc
            PatspecCDAspc = New gloCCDLibrary.frmPatspecCDAspc()
            PatspecCDAspc.OpenfrmMedication = True
            PatspecCDAspc.patientID = nRxModulePatientID
            PatspecCDAspc.ShowDialog(Me)
            PatspecCDAspc.Dispose()
            PatspecCDAspc = Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CancelRx, gloAuditTrail.ActivityType.Cancle, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing

        End Try
    End Sub
    Private Sub SetNKPrescriptionVisibility()
        Try
            If Not IsNothing(_MxC1Flexgrid) Then
                If (_MxC1Flexgrid.RowsCol <= 1) Then
                    GloRxToolBarUserCtrl1.tStrpNKMedications.Visible = True
                Else
                    GloRxToolBarUserCtrl1.tStrpNKMedications.Visible = False
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub RefreshOtherForms()
        Try
            If IsRefRequest = True Then
                ofrmRxRequest = frmRxRequest.GetInstance(nRxModulePatientID)
                If Not IsNothing(ofrmRxRequest) Then
                    Try
                        ofrmRxRequest.GetPendingRefillRequestFORC1()
                    Catch ex As Exception
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                        ex = Nothing
                    End Try

                End If
            ElseIf IsChangeRequest Then
                Dim frmViewRxRequests As frmViewRxRequests = Application.OpenForms.OfType(Of frmViewRxRequests).FirstOrDefault()

                If frmViewRxRequests IsNot Nothing Then
                    frmViewRxRequests.DisplayRequests()
                    frmViewRxRequests = Nothing
                End If
            Else
                If Not IsNothing(ofrmError) Then
                    Try
                        ofrmError.GetErrorMessages()
                    Catch ex As Exception
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                        ex = Nothing
                    End Try
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CancelRx, gloAuditTrail.ActivityType.Cancle, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub
    Private Sub RefreshAgreementIcon()
        Dim ds As DataSet = _RxBusinessLayer.getOPIDAgreement()
        If Not IsNothing(ds) AndAlso ds.Tables.Count > 0 Then
            ds.Tables(0).TableName = "AgreementVerified"
            ds.Tables(1).TableName = "AgreementScanned"
            Dim AgreementVerified As Boolean = False
            Dim AgreementScanned As Boolean = False
            If ds.Tables("AgreementVerified").Rows.Count > 0 Then
                AgreementVerified = Convert.ToBoolean(ds.Tables("AgreementVerified").Rows(0)("IsSignedAgreement"))
            End If
            If ds.Tables("AgreementScanned").Rows.Count > 0 Then
                If ds.Tables("AgreementScanned").Rows(0)("nPatientid") <> 0 AndAlso IsDBNull(ds.Tables("AgreementScanned").Rows(0)("nPatientid")) = False Then
                    AgreementScanned = True
                End If
            End If
            If AgreementVerified AndAlso AgreementScanned Then
                _MxC1Flexgrid.ChangeAgreementIcon(True)
            End If

        End If
    End Sub

#Region "Drug Interaction GSDD"

    Private Sub InitializeDIToolBar()
        Try
            If gblnClinicDISetting = True And gblnAllowUserDISetting = True Then

                If IsFormLocked Then
                    pnlDI.Visible = False
                Else
                    If Not IsNothing(objDrugInteraction) Then
                        objDrugInteraction.Dispose()
                        objDrugInteraction = Nothing
                    End If

                    objDrugInteraction = New DIToolbar
                    objDrugInteraction.Dock = DockStyle.Left

                    pnlDI.Controls.Clear()
                    pnlDI.Controls.Add(objDrugInteraction)

                    AddHandler objDrugInteraction.PerformDrugScreening, AddressOf objDrugInteraction_PerformDrugScreening
                End If
            Else
                pnlDI.Visible = False
            End If
        Catch ex As Exception
            ''

        End Try
    End Sub

    Private Sub objScreeningResults_CloseScreeningResult()
        UnloadDrugInteractionControl()
    End Sub

    Private Sub DisplayScreeningResultMI()
        Dim NDCCode As String = String.Empty
        Dim Drugname As String = String.Empty

        If blnRxC1FlexClick = True Then
            If Not IsNothing(_RxBusinessLayer.PrescriptionCol) Then
                If _RxBusinessLayer.PrescriptionCol.Count > 0 Then
                    NDCCode = _RxBusinessLayer.PrescriptionCol.Item(_RxC1Flexgrid.SelectedRowIndex - 1).NDCCode
                    Drugname = _RxBusinessLayer.PrescriptionCol.Item(_RxC1Flexgrid.SelectedRowIndex - 1).Medication
                End If
            End If
        Else
            If Not IsNothing(_MedBusinessLayer.MedicationCol) Then
                If _MedBusinessLayer.MedicationCol.Count > 0 Then
                    NDCCode = _MedBusinessLayer.MedicationCol.Item(_MxC1Flexgrid.SelectedRowIndex - 1).NDCCode
                    Drugname = _MedBusinessLayer.MedicationCol.Item(_MxC1Flexgrid.SelectedRowIndex - 1).Medication
                End If
            End If
        End If
        If NDCCode <> "" Then
            Dim resp As String = Nothing
            Using oDIBGSHelper As New gloDIServiceHelper(gstrDrugInteractionServiceURL)
                resp = oDIBGSHelper.GetMedicalInstructions(NDCCode)
            End Using

            If Not String.IsNullOrWhiteSpace(resp) Then
                'Dim disclaimer As String = vbCrLf & vbCrLf & "DISCLAIMER: The information contained in the GSDD database is intended to supplement the knowledge of physicians, pharmacists and other healthcare professionals regarding drug therapy problems and patient consulting information. This information is advisory only and is not intended to replace sound clinical judgment in the delivery of healthcare services. " & _
                '        "GSDD disclaims all warranties, whether expressed or implied including, any warranty as to the quality, accuracy, and suitability of this information for any purpose."
                'resp = resp & "" & Me.GetDisclaimer()
                ScreeningResults.IsDefaultPrinterSet = gblnUseDefaultPrinter
                objScreeningResults = New ScreeningResults(Nothing, "Medical Instruction Screening For " & Drugname, True, True)
                AddHandler objScreeningResults.CloseScreeningResult, AddressOf objScreeningResults_CloseScreeningResult
                CType(objScreeningResults, ScreeningResults).ClinicLogo = _RxBusinessLayer.getClinicLogo
                If CType(objScreeningResults, ScreeningResults).LoadPatientEducation(resp) Then
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Drugs, gloAuditTrail.ActivityCategory.DrugInteraction, gloAuditTrail.ActivityType.DITrigger, "Medical Instructions Screening triggered", nRxModulePatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success)
                    objScreeningResults.Dock = DockStyle.Fill
                    pnlDIScreenResult.Controls.Add(objScreeningResults)
                    pnlDIScreenResult.Visible = True
                    pnlDIScreenResult.BringToFront()
                Else
                    MsgBox("Medical Instructions not available for " & Drugname, MsgBoxStyle.ApplicationModal + MsgBoxStyle.OkOnly + MsgBoxStyle.Information, gstrMessageBoxCaption)
                End If
            Else
                MsgBox("Medical Instructions not available for " & Drugname, MsgBoxStyle.ApplicationModal + MsgBoxStyle.OkOnly + MsgBoxStyle.Information, gstrMessageBoxCaption)
            End If
        End If
    End Sub

    Private Sub DisplayScreeningResultADR(oMedPrescribing As List(Of String), ByVal oMedPrescribed As List(Of String))
        Dim resp As String = Nothing
        Using oDIBGSHelper As New gloDIServiceHelper(gstrDrugInteractionServiceURL)
            Dim req As New gloGlobal.DI.DIRequest()
            req.mx = oMedPrescribed
            req.rx = oMedPrescribing
            req.sev = gstrADESeverityLevel
            req.onset = gstrADEOnsetLevel
            resp = oDIBGSHelper.GetAdverseDrugReactions(req)
        End Using

        If Not String.IsNullOrWhiteSpace(resp) Then
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Drugs, gloAuditTrail.ActivityCategory.DrugInteraction, gloAuditTrail.ActivityType.DITrigger, "Adverse Drug Reaction interaction triggered", nRxModulePatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success)
            objScreeningResults = New ScreeningResults(resp, "Adverse Drug Reaction Screening ", True)
            AddHandler objScreeningResults.CloseScreeningResult, AddressOf objScreeningResults_CloseScreeningResult
            objScreeningResults.Dock = DockStyle.Fill
            pnlDIScreenResult.Controls.Add(objScreeningResults)
            pnlDIScreenResult.Visible = True
            pnlDIScreenResult.BringToFront()
        Else
            MsgBox("No Adverse Drug Reaction returned from GSDD for the entered drugs.", MsgBoxStyle.ApplicationModal + MsgBoxStyle.OkOnly + MsgBoxStyle.Information, gstrMessageBoxCaption)
        End If
    End Sub

    Private Sub DisplayScreeningResultDTD(oMedPrescribing As List(Of String), ByVal oMedPrescribed As List(Of String))
        Dim resp As String = Nothing
        Using oDIBGSHelper As New gloDIServiceHelper(gstrDrugInteractionServiceURL)
            Dim req As New gloGlobal.DI.DIRequest()
            req.mx = oMedPrescribed
            req.rx = oMedPrescribing
            req.sev = gstrDISeverityLevel
            req.dl = gstrDIDocLevel
            req.gender = strPatientGender.ToUpper()
            req.dob = DateTime.Parse(strPatientDOB)
            resp = oDIBGSHelper.GetDrugToDrugInteractions(req)
        End Using

        If Not String.IsNullOrWhiteSpace(resp) Then
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Drugs, gloAuditTrail.ActivityCategory.DrugInteraction, gloAuditTrail.ActivityType.DITrigger, "Drug To Drug interaction triggered", nRxModulePatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success)
            objScreeningResults = New ScreeningResults(resp, "Drug To Drug Screening ", True)
            AddHandler objScreeningResults.CloseScreeningResult, AddressOf objScreeningResults_CloseScreeningResult
            objScreeningResults.Dock = DockStyle.Fill
            pnlDIScreenResult.Controls.Add(objScreeningResults)
            pnlDIScreenResult.Visible = True
            pnlDIScreenResult.BringToFront()
        Else
            MsgBox("No Drug To Drug interaction returned from GSDD for the entered drugs.", MsgBoxStyle.ApplicationModal + MsgBoxStyle.OkOnly + MsgBoxStyle.Information, gstrMessageBoxCaption)
        End If
    End Sub

    Private Sub DisplayScreeningResultDTF(oMedPrescribing As List(Of String), ByVal oMedPrescribed As List(Of String))
        Dim resp As String = Nothing
        Using oDIBGSHelper As New gloDIServiceHelper(gstrDrugInteractionServiceURL)
            Dim req As New gloGlobal.DI.DIRequest()
            req.mx = oMedPrescribed
            req.rx = oMedPrescribing
            req.sev = gstrDISeverityLevel
            req.dl = gstrDIDocLevel
            req.gender = strPatientGender.ToUpper()
            req.dob = DateTime.Parse(strPatientDOB)
            resp = oDIBGSHelper.GetDrugToFoodInteractions(req)
        End Using

        If Not String.IsNullOrWhiteSpace(resp) Then
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Drugs, gloAuditTrail.ActivityCategory.DrugInteraction, gloAuditTrail.ActivityType.DITrigger, "Drug To Food interaction triggered", nRxModulePatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success)
            objScreeningResults = New ScreeningResults(resp, "Drug To Food Screening ", True)
            AddHandler objScreeningResults.CloseScreeningResult, AddressOf objScreeningResults_CloseScreeningResult
            objScreeningResults.Dock = DockStyle.Fill
            pnlDIScreenResult.Controls.Add(objScreeningResults)
            pnlDIScreenResult.Visible = True
            pnlDIScreenResult.BringToFront()
        Else
            MsgBox("No Drug To Food interaction returned from GSDD for the entered drugs.", MsgBoxStyle.ApplicationModal + MsgBoxStyle.OkOnly + MsgBoxStyle.Information, gstrMessageBoxCaption)
        End If
    End Sub

    Private Sub DisplayScreeningResultDrugToDisease(oMedPrescribing As List(Of String), ByVal oMedPrescribed As List(Of String), ByVal dtICD As DataTable)
        Dim resp As String = Nothing
        Using oDIBGSHelper As New gloDIServiceHelper(gstrDrugInteractionServiceURL)
            Dim req As New gloGlobal.DI.DIRequest()
            req.mx = oMedPrescribed
            req.rx = oMedPrescribing
            req.dxl = patientDiagnosis
            req.sev = gstrDISeverityLevel
            req.dl = gstrDIDocLevel
            req.gender = strPatientGender.ToUpper()
            req.dob = DateTime.Parse(strPatientDOB)
            resp = oDIBGSHelper.GetDrugToDiseaseInteractions(req)
        End Using

        If Not String.IsNullOrWhiteSpace(resp) Then
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Drugs, gloAuditTrail.ActivityCategory.DrugInteraction, gloAuditTrail.ActivityType.DITrigger, "Drug to Disease interaction triggered", nRxModulePatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success)
            objScreeningResults = New ScreeningResults(resp, "Drug To Disease Screening ", True)
            AddHandler objScreeningResults.CloseScreeningResult, AddressOf objScreeningResults_CloseScreeningResult
            objScreeningResults.Dock = DockStyle.Fill
            pnlDIScreenResult.Controls.Add(objScreeningResults)
            pnlDIScreenResult.Visible = True
            pnlDIScreenResult.BringToFront()
        Else
            MsgBox("No Drug To Disease returned from GSDD for the entered drugs.", MsgBoxStyle.ApplicationModal + MsgBoxStyle.OkOnly + MsgBoxStyle.Information, gstrMessageBoxCaption)
        End If
    End Sub

    Private Sub DisplayScreeningResultDrugToAllergy(oMedPrescribing As List(Of String), ByVal oMedPrescribed As List(Of String), ByVal allergyList As List(Of String))
        Dim resp As String = Nothing
        Using oDIBGSHelper As New gloDIServiceHelper(gstrDrugInteractionServiceURL)
            Dim req As New gloGlobal.DI.DIRequest(True)
            req.mx = oMedPrescribed
            req.rx = oMedPrescribing
            req.al = allergyList
            req.sev = gstrDISeverityLevel
            req.dl = gstrDIDocLevel
            req.gender = strPatientGender.ToUpper()
            req.dob = DateTime.Parse(strPatientDOB)
            resp = oDIBGSHelper.GetDrugToAllergyInteractions(req)
        End Using

        If Not String.IsNullOrWhiteSpace(resp) Then
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Drugs, gloAuditTrail.ActivityCategory.DrugInteraction, gloAuditTrail.ActivityType.DITrigger, "Drug to Allergy interaction triggered", nRxModulePatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success)
            objScreeningResults = New ScreeningResults(resp, "Drug To Allergy Screening ", True)
            AddHandler objScreeningResults.CloseScreeningResult, AddressOf objScreeningResults_CloseScreeningResult
            objScreeningResults.Dock = DockStyle.Fill
            pnlDIScreenResult.Controls.Add(objScreeningResults)
            pnlDIScreenResult.Visible = True
            pnlDIScreenResult.BringToFront()
        Else
            MsgBox("No allergy interactions returned for the entered drugs.", MsgBoxStyle.ApplicationModal + MsgBoxStyle.OkOnly + MsgBoxStyle.Information, gstrMessageBoxCaption)
        End If
    End Sub

    Private Sub DisplayScreeningResultDT(oMedPrescribing As List(Of String), ByVal oMedPrescribed As List(Of String))

        Dim resp As String = Nothing
        Using oDIBGSHelper As New gloDIServiceHelper(gstrDrugInteractionServiceURL)
            Dim req As New gloGlobal.DI.DIRequest()
            req.mx = oMedPrescribed
            req.rx = oMedPrescribing

            req.gender = strPatientGender.ToUpper()
            req.dob = DateTime.Parse(strPatientDOB)
            resp = oDIBGSHelper.GetDuplicateTherapy(req)
        End Using

        If Not String.IsNullOrWhiteSpace(resp) Then
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Drugs, gloAuditTrail.ActivityCategory.DrugInteraction, gloAuditTrail.ActivityType.DITrigger, "Duplicate Therapy interaction triggered", nRxModulePatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success)
            objScreeningResults = New ScreeningResults(resp, "Duplicate Therapy Screening ", True)
            AddHandler objScreeningResults.CloseScreeningResult, AddressOf objScreeningResults_CloseScreeningResult
            objScreeningResults.Dock = DockStyle.Fill
            pnlDIScreenResult.Controls.Add(objScreeningResults)
            pnlDIScreenResult.Visible = True
            pnlDIScreenResult.BringToFront()
        Else
            MsgBox("No Duplicate Therapy returned from GSDD for the entered drugs.", MsgBoxStyle.ApplicationModal + MsgBoxStyle.OkOnly + MsgBoxStyle.Information, gstrMessageBoxCaption)
        End If

    End Sub

    Private Sub DisplayScreeningResultAlerts(prescribingMeds As List(Of String), prescribedMeds As List(Of String), ByVal IsPrescribing? As Boolean)
        Dim resp As AlertResponse = Nothing
        Try
            pnlDIProgress.Visible = True
            pnlDIProgress.BringToFront()
            Application.DoEvents()
            Using oDIBGSHelper As New gloDIServiceHelper(gstrDrugInteractionServiceURL)
                Dim req As New gloGlobal.DI.AlertRequest(True)
                req.mx = prescribedMeds
                req.rx = prescribingMeds
                req.al = patientAllergicMeds
                req.dxl = patientDiagnosis
                req.gender = strPatientGender.ToUpper()
                req.dob = DateTime.Parse(strPatientDOB)

                Dim reqFilters As New gloGlobal.DI.AlertFilters()
                With reqFilters
                    .ade_sev = gstrADESeverityLevel
                    .onset = gstrADEOnsetLevel
                    .di_sev = gstrDISeverityLevel
                    .di_dl = gstrDIDocLevel
                    .dfa_sev = gstrDFASeverityLevel
                    .dfa_dl = gstrDFADocLevel
                    .is_ade = gblnAdverseDrugEffectAlert
                    .is_dt = gblnDuplicateTherapyInteractionAlert
                    .is_dta = True
                    .is_di = True
                    .is_dtf = gblnDrugToFoodInteractionAlert
                    .is_dtd = gblnDrugToDiseaseInteractionAlert
                End With

                req.filters = reqFilters
                resp = oDIBGSHelper.GetInteractionAlerts(req)
                pnlDIProgress.Visible = False
            End Using

            If Not IsNothing(resp) Then
                Dim result As New StringBuilder

                Dim showdi As DialogResult = Windows.Forms.DialogResult.Cancel
                If (IsPrescribing) Then
                    showdi = MessageBox.Show("Drug interaction found for newly prescribed drug", "gloEMR", MessageBoxButtons.OKCancel, MessageBoxIcon.Information)
                Else
                    showdi = Windows.Forms.DialogResult.OK
                End If

                Dim AuditList As New List(Of String)()
                Dim strauditdesc As String = ""

                If showdi = Windows.Forms.DialogResult.OK Then
                    If Not IsNothing(resp.ade) Then
                        If resp.ade.Count > 0 Then
                            AuditList.Add(" Adverse Drug Reaction")
                            result.Append("Adverse Drug Reaction interaction found for ")
                            For Each ade As AlertMessage In resp.ade
                                If ade.response <> "" Then
                                    result.Append(vbCrLf)
                                    If (IsPrescribing) Then
                                        If ade.response.StartsWith(ade.drug) Then
                                            result.Append(" • ")
                                        Else
                                            If ade.drug <> "" Then
                                                result.Append(" • " & ade.drug)
                                            End If
                                        End If
                                        result.Append(ade.response & vbCrLf)
                                    Else
                                        If ade.drug <> "" Then
                                            result.Append(" • " & ade.drug)
                                        End If
                                    End If
                                End If
                            Next
                        End If
                    End If

                    If Not IsNothing(resp.dta) Then
                        If resp.dta.Count > 0 Then
                            AuditList.Add(" Drug To Allergy")
                            result.Append(vbCrLf & vbCrLf & "Drug To Allergy interaction found for ")
                            For Each dta As AlertMessage In resp.dta
                                If dta.response <> "" Then
                                    result.Append(vbCrLf)
                                    If (IsPrescribing) Then
                                        If dta.response.StartsWith(dta.drug) Then
                                            result.Append(" • ")
                                        Else
                                            If dta.drug <> "" Then
                                                result.Append(" • " & dta.drug)
                                            End If
                                        End If
                                        result.Append(dta.response & vbCrLf)
                                    Else
                                        If dta.drug <> "" Then
                                            result.Append(" • " & dta.drug)
                                        End If
                                    End If
                                End If
                            Next
                        End If
                    End If

                    If Not IsNothing(resp.di) Then
                        If resp.di.Count > 0 Then
                            AuditList.Add("Drug To Drug")
                            result.Append(vbCrLf & vbCrLf & "Drug To Drug interaction found for ")
                            For Each di As AlertMessage In resp.di
                                If di.response <> "" Then
                                    result.Append(vbCrLf)
                                    If (IsPrescribing) Then
                                        If di.response.StartsWith(di.drug) Then
                                            result.Append(" • ")
                                        Else
                                            If di.drug <> "" Then
                                                result.Append(" • " & di.drug)
                                            End If
                                        End If
                                        result.Append(di.response & vbCrLf)
                                    Else
                                        If di.drug <> "" Then
                                            result.Append(" • " & di.drug)
                                        End If
                                    End If
                                End If
                            Next
                        End If
                    End If

                    If Not IsNothing(resp.dt) Then
                        If resp.dt.Count > 0 Then
                            AuditList.Add("Duplicate Therapy")
                            result.Append(vbCrLf & vbCrLf & "Duplicate Therapy interaction found for ")
                            For Each dt As AlertMessage In resp.dt
                                If dt.response <> "" Then
                                    result.Append(vbCrLf)
                                    If (IsPrescribing) Then
                                        If dt.response.StartsWith(dt.drug) Then
                                            result.Append(" • ")
                                        Else
                                            If dt.drug <> "" Then
                                                result.Append(" • " & dt.drug)
                                            End If
                                        End If
                                        result.Append(dt.response & vbCrLf)
                                    Else
                                        If dt.drug <> "" Then
                                            result.Append(" • " & dt.drug)
                                        End If
                                    End If
                                End If
                            Next
                        End If
                    End If
                    If Not IsNothing(resp.dtf) Then
                        If resp.dtf.Count > 0 Then
                            AuditList.Add("Drug To Food")
                            result.Append(vbCrLf & vbCrLf & "Drug To Food interaction found for ")
                            For Each dtf As AlertMessage In resp.dtf
                                If dtf.response <> "" Then
                                    result.Append(vbCrLf)
                                    If (IsPrescribing) Then
                                        If dtf.response.StartsWith(dtf.drug) Then
                                            result.Append(" • ")
                                        Else
                                            If dtf.drug <> "" Then
                                                result.Append(" • " & dtf.drug)
                                            End If
                                        End If
                                        result.Append(dtf.response & vbCrLf)
                                    Else
                                        If dtf.drug <> "" Then
                                            result.Append(" • " & dtf.drug)
                                        End If
                                    End If
                                End If
                            Next
                        End If
                    End If

                    If Not IsNothing(resp.dtd) Then
                        If resp.dtd.Count > 0 Then
                            AuditList.Add("Drug To Disease")
                            result.Append(vbCrLf & vbCrLf & "Drug To Disease interaction found for ")
                            For Each dtd As AlertMessage In resp.dtd
                                If dtd.response <> "" Then
                                    result.Append(vbCrLf)
                                    If (IsPrescribing) Then
                                        If dtd.response.StartsWith(dtd.drug) Then
                                            result.Append(" • " & vbCrLf)
                                        Else
                                            If dtd.drug <> "" Then
                                                result.Append(" • " & dtd.drug & vbCrLf)
                                            End If
                                        End If
                                        result.Append(dtd.response & vbCrLf)
                                    Else
                                        If dtd.drug <> "" Then
                                            result.Append(" • " & dtd.drug)
                                        End If
                                    End If
                                End If
                            Next
                        End If
                    End If

                    If (result.ToString().Trim() <> "") Then
                        If (IsPrescribing) Then
                            strauditdesc = String.Join(",", AuditList)
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Drugs, gloAuditTrail.ActivityCategory.DrugInteraction, gloAuditTrail.ActivityType.DITrigger, strauditdesc + "  triggered", nRxModulePatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success)

                            objScreeningResults = New ScreeningResults(result.ToString(), "Drug Interaction Screening ", True)
                            AddHandler objScreeningResults.CloseScreeningResult, AddressOf objScreeningResults_CloseScreeningResult
                            objScreeningResults.Dock = DockStyle.Fill
                            pnlDIScreenResult.Controls.Add(objScreeningResults)
                            pnlDIScreenResult.Visible = True
                            pnlDIScreenResult.BringToFront()
                        Else
                            gloTransparentScreen.gloCustomMessageBox.MAX_HEIGHT_FACTOR = 0.4
                            gloTransparentScreen.gloCustomMessageBox.Show(result.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            CheckOverrideReason()
                        End If
                    End If
                End If
            Else
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Drugs, gloAuditTrail.ActivityCategory.DrugInteraction, gloAuditTrail.ActivityType.DITrigger, "Drug interaction performed", nRxModulePatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success)
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.DrugInteraction, gloAuditTrail.ActivityType.None, "DisplayScreeningResultAlerts :" + If(ex.InnerException IsNot Nothing, ex.InnerException, ex).ToString(), gloAuditTrail.ActivityOutCome.Failure)
        Finally
            If Not IsNothing(resp) Then
                resp = Nothing
            End If
            pnlDIProgress.Visible = False
        End Try
    End Sub

    Private Function GetPrescribedMeds() As List(Of String)
        Dim ndcList As New List(Of String)
        If Not IsNothing(_RxBusinessLayer.MedicationCol) Then
            Dim ndc As String = String.Empty
            For i As Int16 = 0 To _MedBusinessLayer.MedicationCol.Count - 1
                If _MedBusinessLayer.MedicationCol.Item(i).State <> "D" Then
                    Dim strNDC As String = Convert.ToString(_MedBusinessLayer.MedicationCol.Item(i).NDCCode)
                    If strNDC <> "" And Not strNDC.Contains("GLO") Then
                        ndcList.Add(strNDC.ToString())
                    End If
                End If
            Next
        End If
        Return ndcList.Distinct().ToList()
    End Function

    Private Function GetPrescribingMeds(ByVal screeningtype As gloGlobal.DIB.ScreeningType) As List(Of String)
        Dim ndcList As New List(Of String)
        Try
            If Not IsNothing(_RxBusinessLayer.PrescriptionCol) Then
                Dim ndc As String = String.Empty
                'If type = PrescribingType.GetAll Then
                If screeningtype <> DIB.ScreeningType.Alert Then
                    For Each med As Prescription In _RxBusinessLayer.PrescriptionCol
                        If med.State <> "D" Then
                            If Not String.IsNullOrWhiteSpace(med.NDCCode) Then
                                If Not med.NDCCode.Contains("GLO") Then
                                    ndcList.Add(med.NDCCode)
                                End If
                            End If
                        End If
                    Next
                Else
                    For Each med As Prescription In _RxBusinessLayer.PrescriptionCol
                        If med.State <> "D" Then
                            If Not String.IsNullOrWhiteSpace(med.NDCCode) Then
                                If Not med.NDCCode.Contains("GLO") Then
                                    If gblnDrugAlertMsg = True Then
                                        If Convert.ToString(med.ReasontoOverride).Trim() = "" Then
                                            ndcList.Add(med.NDCCode)
                                        End If
                                    Else
                                        ndcList.Add(med.NDCCode)
                                    End If
                                End If
                            End If
                        End If
                    Next
                End If

                'Else
                '    ndcList.Add(_RxBusinessLayer.PrescriptionCol.Item(_RxBusinessLayer.PrescriptionCol.Count - 1).NDCCode)
                'End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.DrugInteraction, gloAuditTrail.ActivityType.None, "getPrescribedProductIdbyNDC :" + If(ex.InnerException IsNot Nothing, ex.InnerException, ex).ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show("Unable to get Prescribing ProductId.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
        Return ndcList
    End Function

    'Private Sub PerformDrugScreeningRevised(ByVal screeningType As gloGlobal.DIB.ScreeningType, ByVal IsPrescribing? As Boolean)
    '    Dim prescribedMeds As List(Of String) = GetPrescribedMeds()
    '    Dim prescribingMeds As List(Of String) = GetPrescribingMeds()

    '    'Using oDIBGSHelper As New gloGlobal.DIB.gloGSHelper(gstrDIBServiceURL)
    '    '    Dim req As New gloGlobal.DIB.DIAlertRequest()
    '    '    Dim resp As String = oDIBGSHelper.GetDIAlert(req)
    '    'End Using

    '    If gblnDrugToAllergyInteractionAlert Or gblnDrugToDiseaseInteractionAlert Or gblnDrugToFoodInteractionAlert Or gblnDuplicateTherapyInteractionAlert Or gblnDrugToDrugInteractionAlert Or gblnAdverseDrugEffectAlert Then
    '        IsOverrideReason = False
    '        'DisplayInteactionAlertsRevised(prescribingMeds, prescribedMeds, patientAllergicMeds, patientDiagnosis, IsPrescribing)
    '    End If
    'End Sub

    Private Sub PerformDrugScreening(ByVal screeningType As gloGlobal.DIB.ScreeningType, ByVal IsPrescribing? As Boolean)

        Try
            UnloadDrugInteractionControl()
            If screeningType = gloGlobal.DIB.ScreeningType.MI Then
                DisplayScreeningResultMI()
            Else
                Dim prescribedMeds As List(Of String) = GetPrescribedMeds()
                Dim prescribingMeds As List(Of String) = GetPrescribingMeds(screeningType)
                Dim allergiesList As List(Of String) = LoadAllergies()

                If Not (IsNothing(prescribingMeds) And IsNothing(prescribedMeds)) Then
                    Select Case screeningType
                        Case gloGlobal.DIB.ScreeningType.ADE
                            DisplayScreeningResultADR(prescribingMeds, prescribedMeds)
                        Case gloGlobal.DIB.ScreeningType.DT
                            DisplayScreeningResultDT(prescribingMeds, prescribedMeds)
                        Case gloGlobal.DIB.ScreeningType.DI
                            DisplayScreeningResultDTD(prescribingMeds, prescribedMeds)
                        Case gloGlobal.DIB.ScreeningType.DFA
                            DisplayScreeningResultDTF(prescribingMeds, prescribedMeds)
                        Case gloGlobal.DIB.ScreeningType.PRC
                            DisplayScreeningResultDrugToDisease(prescribingMeds, prescribedMeds, Nothing)
                        Case gloGlobal.DIB.ScreeningType.PAR
                            DisplayScreeningResultDrugToAllergy(prescribingMeds, prescribedMeds, allergiesList)
                        Case gloGlobal.DIB.ScreeningType.Alert
                            IsOverrideReason = False
                            DisplayScreeningResultAlerts(prescribingMeds, prescribedMeds, IsPrescribing)
                    End Select
                End If

                'If Not IsNothing(oMedPrescribed) Then
                '    oMedPrescribed = Nothing
                'End If
                'If Not IsNothing(oMedPrescribing) Then
                '    oMedPrescribing = Nothing
                'End If
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.DrugInteraction, gloAuditTrail.ActivityType.None, "objDrugInteraction_PerformDrugScreening :" + If(ex.InnerException IsNot Nothing, ex.InnerException, ex).ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show("Unable to retrieve Drug Interaction information.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            pnlDIProgress.Visible = False
        End Try
    End Sub

    Private Sub objDrugInteraction_PerformDrugScreening(ByVal screeningType As gloGlobal.DIB.ScreeningType)
        PerformDrugScreening(screeningType, Nothing)
    End Sub

    Private Function getPrescribedProductIdbyNDC() As List(Of String)
        Dim lstProductIDs As List(Of String) = Nothing
        Try
            If Not IsNothing(_MedBusinessLayer.MedicationCol) Then
                If _MedBusinessLayer.MedicationCol.Count > 0 Then
                    pnlDIProgress.Visible = True
                    pnlDIProgress.BringToFront()
                    Application.DoEvents()
                    Dim i As Int32

                    Dim lstNDCs As New List(Of String)

                    For i = 0 To _MedBusinessLayer.MedicationCol.Count - 1
                        If _MedBusinessLayer.MedicationCol.Item(i).State <> "D" Then
                            Dim strNDC As String = Convert.ToString(_MedBusinessLayer.MedicationCol.Item(i).NDCCode)
                            If strNDC <> "" And Not strNDC.Contains("GLO") Then
                                lstNDCs.Add(strNDC.ToString())
                            End If
                        End If

                    Next

                    If lstNDCs.Count > 0 Then
                        Using oDIBGSHelper As New gloGlobal.DIB.gloGSHelper(gstrDIBServiceURL)
                            lstProductIDs = oDIBGSHelper.GetnCheckNDCInGSDD(lstNDCs.Distinct().ToList())
                        End Using
                    End If
                    lstNDCs.Clear()
                    lstNDCs = Nothing

                    If Not IsNothing(lstProductIDs) Then
                        If lstProductIDs.Count <= 0 Then
                            lstProductIDs = Nothing
                        End If
                    Else
                        lstProductIDs = Nothing
                    End If
                End If

            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.DrugInteraction, gloAuditTrail.ActivityType.None, "getPrescribedProductIdbyNDC :" + If(ex.InnerException IsNot Nothing, ex.InnerException, ex).ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show("Unable to get Prescribed ProductId.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
        Return lstProductIDs
    End Function

    Private Enum PrescribingType
        GetAll
        GetCurrent
    End Enum

    Private Function getPrescribingProductIdbyNDC(Optional ByVal type As NewRxType = PrescribingType.GetAll) As List(Of String)
        Dim lstProductIDs As List(Of String) = Nothing
        Try
            If Not IsNothing(_RxBusinessLayer.PrescriptionCol) Then
                If _RxBusinessLayer.PrescriptionCol.Count > 0 Then
                    pnlDIProgress.Visible = True
                    pnlDIProgress.BringToFront()
                    Application.DoEvents()
                    Dim lstNDCs As New List(Of String)

                    If type = PrescribingType.GetAll Then
                        For Each Rxdrug As Prescription In _RxBusinessLayer.PrescriptionCol
                            Dim strNDC As String = Convert.ToString(Rxdrug.NDCCode) ''Convert.ToString(_RxBusinessLayer.PrescriptionCol.Item(i).NDCCode)
                            If Rxdrug.Status <> "D" Then
                                If strNDC <> "" And Not strNDC.Contains("GLO") Then
                                    lstNDCs.Add(strNDC.ToString())
                                End If
                            End If
                        Next
                    Else
                        lstNDCs.Add(_RxBusinessLayer.PrescriptionCol.Item(_RxBusinessLayer.PrescriptionCol.Count - 1).NDCCode)
                    End If


                    If lstNDCs.Count > 0 Then
                        Using oDIBGSHelper As New gloGlobal.DIB.gloGSHelper(gstrDIBServiceURL)
                            lstProductIDs = oDIBGSHelper.GetnCheckNDCInGSDD(lstNDCs)
                        End Using
                    End If

                    lstNDCs.Clear()
                    lstNDCs = Nothing

                    If Not IsNothing(lstProductIDs) Then
                        If lstProductIDs.Count <= 0 Then
                            lstProductIDs = Nothing
                        End If
                    End If

                End If
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.DrugInteraction, gloAuditTrail.ActivityType.None, "getPrescribedProductIdbyNDC :" + If(ex.InnerException IsNot Nothing, ex.InnerException, ex).ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show("Unable to get Prescribed ProductId.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
        Return lstProductIDs
    End Function

    Private Function getHistoryAllergyDrugsItem() As List(Of gloGlobal.DIB.PIResp)

        Dim oProductIngredients As List(Of gloGlobal.DIB.PIResp) = Nothing

        ' If _RxBusinessLayer.PrescriptionCol.Count > 0 Then
        Dim i As Int32

        Dim sNDCCode As String = ""
        If Not IsNothing(_RxBusinessLayer.HistoriesCol) Then
            If _RxBusinessLayer.HistoriesCol.Count > 0 Then
                Dim lstNDCs As New List(Of String)
                For i = 0 To _RxBusinessLayer.HistoriesCol.Count - 1
                    sNDCCode = CType(_RxBusinessLayer.HistoriesCol.Item(i), History).NDCCode
                    If sNDCCode <> "" And Not sNDCCode.Contains("GLO") Then
                        lstNDCs.Add(sNDCCode.ToString())
                    End If
                    sNDCCode = ""
                Next

                If lstNDCs.Count > 0 Then
                    Using oDIBGSHelper As New gloGlobal.DIB.gloGSHelper(gstrDIBServiceURL)
                        oProductIngredients = oDIBGSHelper.GetProductIngredients(lstNDCs)
                    End Using
                End If


                lstNDCs.Clear()
            End If
        End If

        ' End If
        Return oProductIngredients
    End Function

    Private Function SetPatientDiagnosis() As DataTable
        Dim ogloEMRDatabase As DataBaseLayer = New DataBaseLayer
        Try
            Dim _strSQl = " SELECT DISTINCT sICD9Code as ICDCode, ISNULL(nICDRevision,9) as ICDType FROM dbo.ExamICD9CPT WHERE isnull(sICD9Code,'') <>'' And nPatientID = " & nRxModulePatientID & ""
            Using dt As DataTable = ogloEMRDatabase.GetDataTable_Query(_strSQl)
                If Not IsNothing(dt) Then
                    For Each row As DataRow In dt.Rows
                        patientDiagnosis.Add(New gloGlobal.DI.dxItem(Convert.ToInt16(row("ICDType")), Convert.ToString(row("ICDCode"))))
                    Next
                End If
            End Using

        Catch ex As Exception
            Dim objex As New PrescriptionException
            Throw objex
        Finally
            If Not IsNothing(ogloEMRDatabase) Then
                ogloEMRDatabase.Dispose()
                ogloEMRDatabase = Nothing
            End If
        End Try
        Return Nothing
    End Function

    Private Sub frmPrescription_PerformDrugAlertCheck(ByVal IsPrescribing As Boolean)
        If gblnClinicDISetting Then
            If _RxBusinessLayer IsNot Nothing AndAlso _RxBusinessLayer.PrescriptionCol IsNot Nothing AndAlso _RxBusinessLayer.PrescriptionCol.Count > 0 Then
                PerformDrugScreening(DIB.ScreeningType.Alert, IsPrescribing)
            End If
        End If
    End Sub

    Private Sub DisplayInteactionAlertsRevised(oMedPrescribing As List(Of String), ByVal oMedPrescribed As List(Of String), oHistoryAllergyDrugs As List(Of String), ByVal dtICD As DataTable, ByVal IsPrescribing? As Boolean)

        'Using oDIHelper As New DI.gloDIHelper(gstrDrugInteractionServiceURL)

        '    AddHandler oDIHelper.HideDiProgressPanel, AddressOf HideDiProgress

        '    Dim objProducts As Object = Nothing

        '    oDIHelper.OnsetLevel = gstrADEOnsetLevel
        '    oDIHelper.SeverityLevel = gstrADESeverityLevel
        '    oDIHelper.DFASeverityLevel = gstrDFASeverityLevel
        '    oDIHelper.DFADocLevel = gstrDFADocLevel
        '    oDIHelper.DISeverityLevel = gstrDISeverityLevel
        '    oDIHelper.DIDocLevel = gstrDIDocLevel

        '    oDIHelper.PatientGender = strPatientGender.ToUpper()
        '    oDIHelper.PatientDOB = DateTime.Parse(strPatientDOB)

        '    oDIHelper.DrugToAllergyEnable = gblnDrugToAllergyInteractionAlert
        '    oDIHelper.DrugToDiseaseEnable = gblnDrugToDiseaseInteractionAlert
        '    oDIHelper.DrugToFoodEnable = gblnDrugToFoodInteractionAlert
        '    oDIHelper.DuplicateTherapyEnable = gblnDuplicateTherapyInteractionAlert
        '    oDIHelper.DrugToDrugEnable = gblnDrugToDrugInteractionAlert
        '    oDIHelper.AdverseDrugEffectEnable = gblnAdverseDrugEffectAlert

        '    If IsPrescribing.HasValue And IsPrescribing Then
        '        Dim result As StringBuilder
        '        result = oDIHelper.PerformDrugAlert(oMedPrescribing, oMedPrescribed, oHistoryAllergyDrugs, dtICD, DI.gloDIHelper.ScreningResultType.Details)
        '        If (Not IsNothing(result)) Then
        '            If Convert.ToString(result).Trim <> "" Then
        '                objScreeningResults = New ScreeningResults(Convert.ToString(result), "Adverse Drug Reaction Screening ", True)
        '                AddHandler objScreeningResults.CloseScreeningResult, AddressOf objScreeningResults_CloseScreeningResult
        '                objScreeningResults.Dock = DockStyle.Fill
        '                pnlDIScreenResult.Controls.Add(objScreeningResults)
        '                pnlDIScreenResult.Visible = True
        '                pnlDIScreenResult.BringToFront()
        '            End If
        '        End If
        '    Else
        '        Dim result As StringBuilder
        '        result = oDIHelper.PerformDrugAlert(oMedPrescribing, oMedPrescribed, oHistoryAllergyDrugs, dtICD, DI.gloDIHelper.ScreningResultType.AlertOnly)
        '        If (Not IsNothing(result)) Then
        '            If Convert.ToString(result).Trim <> "" Then
        '                gloTransparentScreen.gloCustomMessageBox.MAX_HEIGHT_FACTOR = 0.4
        '                gloTransparentScreen.gloCustomMessageBox.Show(result.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        '                CheckOverrideReason()
        '            End If
        '        End If


        '    End If
        'End Using
    End Sub

    Private Sub DisplayInteactionAlerts(oMedPrescribing As List(Of String), ByVal oMedPrescribed As List(Of String), oHistoryAllergyDrugs As List(Of gloGlobal.DIB.PIResp), ByVal dtICD As DataTable, ByVal IsPrescribing? As Boolean)
        'Using oDIHelper As New DI.gloDIHelper(gstrDrugInteractionServiceURL)

        '    AddHandler oDIHelper.HideDiProgressPanel, AddressOf HideDiProgress

        '    Dim objProducts As Object = Nothing

        '    oDIHelper.OnsetLevel = gstrADEOnsetLevel
        '    oDIHelper.SeverityLevel = gstrADESeverityLevel
        '    oDIHelper.DFASeverityLevel = gstrDFASeverityLevel
        '    oDIHelper.DFADocLevel = gstrDFADocLevel
        '    oDIHelper.DISeverityLevel = gstrDISeverityLevel
        '    oDIHelper.DIDocLevel = gstrDIDocLevel

        '    oDIHelper.PatientGender = strPatientGender.ToUpper()
        '    oDIHelper.PatientDOB = DateTime.Parse(strPatientDOB)

        '    oDIHelper.DrugToAllergyEnable = gblnDrugToAllergyInteractionAlert
        '    oDIHelper.DrugToDiseaseEnable = gblnDrugToDiseaseInteractionAlert
        '    oDIHelper.DrugToFoodEnable = gblnDrugToFoodInteractionAlert
        '    oDIHelper.DuplicateTherapyEnable = gblnDuplicateTherapyInteractionAlert
        '    oDIHelper.DrugToDrugEnable = gblnDrugToDrugInteractionAlert
        '    oDIHelper.AdverseDrugEffectEnable = gblnAdverseDrugEffectAlert

        '    If IsPrescribing.HasValue And IsPrescribing Then
        '        Dim result As StringBuilder
        '        result = oDIHelper.PerformDrugAlert(oMedPrescribing, oMedPrescribed, oHistoryAllergyDrugs, dtICD, DI.gloDIHelper.ScreningResultType.Details)
        '        If (Not IsNothing(result)) Then
        '            If Convert.ToString(result).Trim <> "" Then
        '                objScreeningResults = New ScreeningResults(Convert.ToString(result), "Adverse Drug Reaction Screening ", True)
        '                AddHandler objScreeningResults.CloseScreeningResult, AddressOf objScreeningResults_CloseScreeningResult
        '                objScreeningResults.Dock = DockStyle.Fill
        '                pnlDIScreenResult.Controls.Add(objScreeningResults)
        '                pnlDIScreenResult.Visible = True
        '                pnlDIScreenResult.BringToFront()
        '            End If
        '        End If
        '    Else
        '        Dim result As StringBuilder
        '        result = oDIHelper.PerformDrugAlert(oMedPrescribing, oMedPrescribed, oHistoryAllergyDrugs, dtICD, DI.gloDIHelper.ScreningResultType.AlertOnly)
        '        If (Not IsNothing(result)) Then
        '            If Convert.ToString(result).Trim <> "" Then
        '                gloTransparentScreen.gloCustomMessageBox.MAX_HEIGHT_FACTOR = 0.4
        '                gloTransparentScreen.gloCustomMessageBox.Show(result.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        '                CheckOverrideReason()
        '            End If
        '        End If


        '    End If
        'End Using
    End Sub

    Public Property IsOverrideReason() As Boolean

    Private Sub CheckOverrideReason()  '', ByVal products As gloGlobal.GSDD5Service.ClinicalInteractionProductType()
        IsOverrideReason = False
        Try

            Dim IsReason As Boolean = False
            Dim ToSave As Boolean = False
            If gblnDrugAlertMsg = True Then
                If blnRxC1FlexClick = True Then
                    If Not IsNothing(_RxBusinessLayer.PrescriptionCol) Then
                        If _RxBusinessLayer.PrescriptionCol.Count > 0 Then

                            For Each Rxdrug As Prescription In _RxBusinessLayer.PrescriptionCol
                                If Convert.ToString(Rxdrug.ReasontoOverride) = "" Then
                                    IsReason = True
                                    Exit For
                                Else
                                    IsReason = False
                                End If
                            Next

                        End If
                    End If
                Else

                End If

                If IsReason Then
                    MessageBox.Show("Please document reason for overriding", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    IsOverrideReason = True
                Else
                    If (MessageBox.Show("Do you want to override the alert and continue prescribing drugs?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes) Then
                        IsOverrideReason = False
                    Else
                        IsOverrideReason = True
                    End If
                End If
            End If

        Catch ex As Exception
            IsOverrideReason = False
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.DrugInteraction, gloAuditTrail.ActivityType.None, "CheckOverrideReason :" + If(ex.InnerException IsNot Nothing, ex.InnerException, ex).ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub HideDiProgress() ''Handles DIHelper.HideDiProgressPanel
        pnlDIProgress.Visible = False

    End Sub

#End Region

#Region "ePA"

    Private Sub SendEPARequest()

        If Not blnRxC1FlexClick Then
            Exit Sub
        End If

        '' RxElig should be performed to proceed with EPA
        If Not IsValidRxElig() Then
            Exit Sub
        End If

        '' Atlease a Prescription should be selected to proceeed with EPA
        If IsNothing(_RxBusinessLayer.PrescriptionCol) OrElse _RxBusinessLayer.PrescriptionCol.Count <= 0 Then
            Exit Sub
        End If

        '' Validate basic inputs like sig info, pharmacy etc
        If Not ePAValidation() Then
            Exit Sub
        End If

        Try
            '' Initialize EPA Requests for those who does not has PARefID
            InitializeEPARequest()
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.PriorAuthorization, gloAuditTrail.ActivityType.Initialize, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub


#Region "EPAInit Request"

    Private Sub InitializeEPARequest()
        Dim lstMPID As New List(Of Int64)

        Dim req As New EPA.PAInitRequest()
        For row As Int32 = 0 To _RxBusinessLayer.PrescriptionCol.Count - 1
            If _RxBusinessLayer.PrescriptionCol(row).State <> "D" And _RxC1Flexgrid.getCheckedState(row + 1) = True Then

                If String.IsNullOrWhiteSpace(_RxBusinessLayer.PrescriptionCol(row).PAReferenceID) OrElse _RxBusinessLayer.PrescriptionCol(row).PriorAuthorizationStatus = "Cancelled" Then
                    Dim meds As EPA.MedicationPrescribed = New EPA.MedicationPrescribed()
                    If Not String.IsNullOrWhiteSpace(_RxBusinessLayer.PrescriptionCol(row).PharmacyName) OrElse Not String.IsNullOrEmpty(_RxBusinessLayer.PrescriptionCol(row).PharmacyName) Then
                        meds.Pharmacy = Me.GetEPAPharmacy(_RxBusinessLayer.PrescriptionCol(row))
                    Else
                        meds.Pharmacy = Nothing
                    End If
                    meds.Medication = Me.GetEPAMedicationPrescribed(_RxBusinessLayer.PrescriptionCol(row))
                    req.MedicationPrescribed.Add(meds)
                    lstMPID.Add(_RxBusinessLayer.PrescriptionCol(row).mpid)
                End If
            End If
        Next

        If Not IsNothing(req.MedicationPrescribed) AndAlso req.MedicationPrescribed.Count > 0 Then
            req.pbm = Me.GetEPABenefitsCoordination()
            req.patient = Me.GetEPAPatient()
            req.from = strPrescriberSPI

            req.prescriber = Me.GetEPAPrescriber(nPatientProviderId)

            If gnLoginProviderID <> 0 AndAlso req.prescriber.npi.ToLower() <> strPrescriberNPI.ToLower() Then
                req.submitter = Me.GetEPAPrescriber(Nothing)
            End If

            Dim sBuilder As StringBuilder = Nothing

            Using ePARequest As New EPA.gloEPAHelper(gstrEPAServiceURL)
                Dim sCodeMeaning As String = String.Empty
                Dim sCodeDescription As String = String.Empty
                Dim isEPAListUpdated As Boolean = False
                Dim eEPAResponse As New List(Of EPA.PAInitResponse)

                eEPAResponse = ePARequest.SubmitPARequest(req)
                For Each MPID As Int64 In lstMPID
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.Prescription, gloAuditTrail.ActivityType.Send, "Electronic Prior Authorization request sent", nRxModulePatientID, MPID, _RxBusinessLayer.ProviderID, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                Next

                sBuilder = New StringBuilder()
                For Each Response As EPA.PAInitResponse In eEPAResponse

                    If (Response.IsSuccesful) Then
                        isEPAListUpdated = True
                        Using ePAInsertUpdate As New EPABusinesslayer()
                            ePAInsertUpdate.ePA_INUPMaster(req.patient.mrnNumber, _RxBusinessLayer.ProviderID, _objFormularyToolBar.PBMMemberID, Response.Med.mpid, Response.Med.ndc, Response.paRefID, Response.MsgID, Response.sCode, Response.Med.days, Response.Med.qty)
                        End Using
                        sCodeMeaning = "Posted Successfully"
                        If Response.sCode = "000" Then
                            sCodeDescription = "Message transmission successful"
                        ElseIf Response.sCode = "010" Then
                            sCodeDescription = "Message successfully accepted by ultimate receiver"
                        End If

                    Else
                        sCodeMeaning = "Problem in sending Prior Authorization Request"
                        sCodeDescription = Response.sDesc
                    End If

                    With sBuilder
                        .AppendLine("Drug Name: " + Response.Med.medication)
                        .AppendLine("Transaction: ePA")
                        .AppendLine("Status: " + sCodeMeaning)
                        .AppendLine("Status Code: " + Response.sCode)
                        .AppendLine("Description: " + sCodeDescription)
                        .AppendLine("PA ReferenceID: " + Response.paRefID)
                        .AppendLine()
                    End With
                Next
                MessageBox.Show(Convert.ToString(sBuilder), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                If isEPAListUpdated Then
                    Me.RefreshActiveEPAList()
                    RaiseEvent PerformEPAStatusCheck(Nothing)
                End If
            End Using
        End If
        If lstMPID IsNot Nothing Then
            lstMPID.Clear()
            lstMPID = Nothing
        End If
    End Sub

    Private Function GetEPAPrescriber(ByVal PatientProviderID As Int64?) As EPA.PAInitRequest.Prescriber

        Dim strAddress1 As String = String.Empty
        Dim strAddress2 As String = String.Empty
        Dim strCity As String = String.Empty
        Dim strState As String = String.Empty
        Dim strCountry As String = String.Empty
        Dim strZip As String = String.Empty

        Dim strPhone As String = String.Empty
        Dim strFax As String = String.Empty

        Dim strFirstName As String = String.Empty
        Dim strMiddleName As String = String.Empty
        Dim strLastName As String = String.Empty

        Dim strNPI As String = String.Empty
        Dim strDEA As String = String.Empty
        Dim strSSN As String = String.Empty
        Dim strSPI As String = String.Empty

        Try
            If PatientProviderID.HasValue Then
                Using dtProvider As DataTable = _RxBusinessLayer.GetPatientProviderDetails(nPatientProviderId)
                    If IsNothing(dtProvider) = False Then
                        If dtProvider.Rows.Count > 0 Then
                            strAddress1 = Convert.ToString(dtProvider.Rows(0)("sPrAddressline"))
                            strAddress2 = Convert.ToString(dtProvider.Rows(0)("sPrAddressLine2"))
                            strCity = Convert.ToString(dtProvider.Rows(0)("sPrCity"))
                            strState = Convert.ToString(dtProvider.Rows(0)("sPrState"))
                            strCountry = Convert.ToString(dtProvider.Rows(0)("sCountry"))
                            strZip = Convert.ToString(dtProvider.Rows(0)("sPrZip"))

                            strPhone = Convert.ToString(dtProvider.Rows(0)("sPrPhone"))
                            strFax = Convert.ToString(dtProvider.Rows(0)("sPrFax"))

                            strFirstName = Convert.ToString(dtProvider.Rows(0)("sPrFirstName"))
                            strMiddleName = Convert.ToString(dtProvider.Rows(0)("sPrMiddleName"))
                            strLastName = Convert.ToString(dtProvider.Rows(0)("sPrLastName"))

                            strNPI = Convert.ToString(dtProvider.Rows(0)("sPrNPI"))
                            strSSN = Convert.ToString(dtProvider.Rows(0)("sPrSSN"))
                            strSPI = Convert.ToString(dtProvider.Rows(0)("sPrSPIID"))
                        End If
                    End If
                End Using
            Else
                strAddress1 = strPrescriberAddress
                strAddress2 = strPrescriberAddress2
                strCity = strPrescriberCity
                strCountry = strPrescriberCountry
                strState = strPrescriberState
                strZip = strPrescriberZip

                strPhone = strPrescriberPhone
                strFax = strPrescriberFax

                strFirstName = strPrescriberFirstName
                strMiddleName = strPrescriberMiddleName
                strLastName = strPrescriberLastName

                strNPI = strPrescriberNPI
                strSSN = strPrescriberSSN
                strSPI = strPrescriberSPI
            End If

            If strAddress1.Length > 35 Then strAddress1 = strAddress1.Substring(0, 35)
            If strAddress2.Length > 35 Then strAddress2 = strAddress2.Substring(0, 35)
            If strCity.Length > 35 Then strCity = strCity.Substring(0, 35)
            If strCountry.Length > 35 Then strCountry = strCountry.Substring(0, 35)
            If strState.Length > 9 Then strState = strState.Substring(0, 9)
            If strZip.Length > 11 Then strZip = strZip.Substring(0, 11)

            If strFirstName.Length > 35 Then strFirstName = strFirstName.Substring(0, 35)
            If strMiddleName.Length > 35 Then strMiddleName = strMiddleName.Substring(0, 35)
            If strLastName.Length > 35 Then strLastName = strLastName.Substring(0, 35)

            Return ServiceObjectBase.Prescriber.GetPrescriber(strFirstName, strMiddleName, strLastName, strNPI, String.Empty, strAddress1, strAddress2, strCity, strCountry, strState, strZip, strFax, strPhone)

        Catch ex As Exception
            Throw New Exception("Error @GetEPAPrescriber " + ex.Message.ToString())
        End Try

    End Function

    Private Function GetEPAMedicationPrescribed(ByRef Prescription As Prescription) As EPA.PAInitRequest.Medication

        Dim medication As EPA.PAInitRequest.Medication = Nothing

        Try
            If Prescription IsNot Nothing Then
                medication = New EPA.PAInitRequest.Medication()

                Dim DrugDBCode As String = String.Empty
                Dim DrugDBcodeQualifier As String = String.Empty
                Dim dtRxNormCode As DataTable = Nothing
                Dim drDiagnosis As DataRow = Nothing

                Using oGSHelper As New gloGlobal.DIB.gloGSHelper(gloEMRGeneralLibrary.gloGeneral.clsgeneral.sDIBServiceURL)
                    Dim DrugDBCodeResponse As gloGlobal.DIB.RxnormFlagInfo = oGSHelper.GetRxNormCode(Prescription.NDCCode)

                    If DrugDBCodeResponse IsNot Nothing Then
                        medication.drugCode = DrugDBCodeResponse.Code

                        If DrugDBCodeResponse.Type = "CD" Then
                            medication.drugQual = "SCD"
                        Else
                            medication.drugQual = DrugDBCodeResponse.Type
                        End If
                        DrugDBCodeResponse = Nothing
                    End If
                End Using

                medication.medication = Prescription.Medication
                medication.ndc = Prescription.NDCCode
                medication.mpid = Prescription.mpid

                'medication.strength = New gloGlobal.Common.ServiceObjectBase.Strength()
                'medication.strength.form = "C42945"
                'medication.strength.value = "10"
                'medication.strength.unit = "mg"

                If Not String.IsNullOrEmpty(Prescription.Frequency) OrElse Not String.IsNullOrWhiteSpace(Prescription.Frequency) Then
                    If Prescription.Frequency.Length > 140 Then
                        medication.sigText = Prescription.Frequency.Substring(0, 140)
                    Else
                        medication.sigText = Prescription.Frequency
                    End If
                Else
                    medication.sigText = Nothing
                End If

                medication.direction = Prescription.DosageFrequencyText

                If Not String.IsNullOrWhiteSpace(Prescription.Amount) Then
                    If IsNumeric(Prescription.Amount.Split(" ")(0)) Then
                        medication.qty = Prescription.Amount.Split(" ")(0)
                        medication.qtyUnit = Prescription.PotencyCode
                    End If
                End If

                If Not String.IsNullOrWhiteSpace(Prescription.Duration) Then
                    medication.days = Prescription.DaysSupply
                End If

                ''Refill
                'If String.IsNullOrEmpty(Prescription.RefillQualifier) OrElse Prescription.RefillQualifier.Trim.Length <= 0 Then
                '    medication.refillQualifier = "R"
                'Else
                '    If Prescription.RefillQualifier = "P" Then
                '        medication.refillQualifier = "R"
                '    Else
                '        medication.refillQualifier = Prescription.RefillQualifier
                '    End If
                'End If
                'If Prescription.RefillQualifier = "R" OrElse Prescription.RefillQualifier = "P" Then
                '    If Not String.IsNullOrEmpty(Prescription.RefillQuantity) Then
                '        medication.refill = Prescription.RefillQuantity.Trim
                '    End If
                'End If

                'Diagnosis
                If Not String.IsNullOrEmpty(Prescription.Problems) AndAlso Not String.IsNullOrWhiteSpace(Prescription.Problems) Then
                    Using epaDatabaseLayer As New EPABusinesslayer()
                        drDiagnosis = epaDatabaseLayer.GetDiagnosis(Prescription.Problems)

                        If drDiagnosis IsNot Nothing Then
                            Dim sICDCode As String = Convert.ToString(drDiagnosis("sICD9Code"))
                            Dim sDescription As String = Convert.ToString(drDiagnosis("sDescription"))

                            medication.AddDiagnosis(sICDCode, sDescription)
                        End If

                    End Using
                End If
                drDiagnosis = Nothing
            End If
        Catch ex As Exception
            Throw New Exception("Error @GetEPAMedicationPrescribed " + ex.Message.ToString())
        End Try

        Return medication

    End Function

    Private Function GetEPABenefitsCoordination() As EPA.PAInitRequest.BenifitCoordination
        Dim benefits As New EPA.PAInitRequest.BenifitCoordination()

        Try
            Dim PBM As gloUserControlLibrary.gloFormularlyToolBarUserCtrl = _objFormularyToolBar
            benefits = gloGlobal.EPA.PAInitRequest.BenifitCoordination.GetBenefitsCoordination(PBM.PayerID, PBM.ProcessorIdentificationNumber, PBM.BINLocationNumber, PBM.MutuallyDefined, PBM.PayerName, PBM.CardHolderID, strPatientFirstName, strPatientLastName, strPatientMiddleName, PBM.GroupID, PBM.GroupName, PBM.PBMMemberID, PBM.RelatesToMessageID)
            PBM = Nothing
        Catch ex As Exception
            Throw New Exception("Error @GetEPABenefitsCoordination " + ex.Message.ToString())
        End Try

        Return benefits
    End Function

    Private Function GetEPAPatient() As EPA.PAInitRequest.Patient
        Dim gloSureScriptInterface As New gloSureScript.gloSureScriptInterface()
        Dim _patient As New gloGlobal.EPA.PAInitRequest.Patient
        Dim dtPatientDOB As DateTime = DateTime.MinValue
        Try
            Dim sShortGender As String = String.Empty
            Dim sUTCDateOfBirth As String = strPatientDOB

            If DateTime.TryParse(strPatientDOB, dtPatientDOB) Then
                sUTCDateOfBirth = gloSureScriptInterface.Getdatetype(Convert.ToDateTime(dtPatientDOB))
            End If

            If strPatientGender.ToLower().Contains("male") Then
                sShortGender = "M"
            ElseIf strPatientGender.ToLower().Contains("female") Then
                sShortGender = "F"
            End If

            _patient.ssn = strPatientSsn
            _patient.fname = strPatientFirstName
            _patient.mname = strPatientMiddleName
            _patient.lname = strPatientLastName
            _patient.gender = sShortGender
            _patient.dob = sUTCDateOfBirth
            _patient.address.add1 = strPatientAddress
            _patient.address.add2 = strPatientAddress2
            _patient.address.city = strPatientCity
            _patient.address.state = strPatientState
            _patient.address.zip = strPatientZip
            _patient.address.country = strPatientCountry
            _patient.communication.phone = strPatientPhone
            _patient.communication.fax = strPatientFax
            _patient.communication.email = strPatientEmail
            _patient.mrnNumber = nRxModulePatientID

        Catch ex As Exception
            Throw New Exception("Error @GetEPAPatient " + ex.Message.ToString())
        Finally
            If gloSureScriptInterface IsNot Nothing Then
                gloSureScriptInterface.Dispose()
                gloSureScriptInterface = Nothing
            End If
        End Try
        Return _patient

    End Function

    Private Function GetEPAPharmacy(ByRef Prescription As Prescription) As EPA.PAInitRequest.Pharmacy
        Dim ePADatabaseLayer As New RxBusinesslayer(nPatientProviderId)
        Dim _pharmacy As gloGlobal.EPA.PAInitRequest.Pharmacy = Nothing
        Try
            If _RxBusinessLayer.PrescriptionCol.Count > 0 Then
                _pharmacy = New gloGlobal.EPA.PAInitRequest.Pharmacy

                _pharmacy.name = Prescription.PharmacyName
                _pharmacy.ncpdp = Prescription.PhNCPDPID
                _pharmacy.npi = ePADatabaseLayer.GetPharmacyNPIFromNCPDID(Prescription.PhNCPDPID)
                _pharmacy.address.add1 = Prescription.PhAddressline1
                _pharmacy.address.add2 = Prescription.PhAddressline2
                _pharmacy.address.city = Prescription.PhCity
                _pharmacy.address.state = Prescription.PhState
                _pharmacy.address.zip = Prescription.PhZip
                _pharmacy.address.country = "US"
                _pharmacy.communication.phone = Prescription.PhPhone
                _pharmacy.communication.fax = Prescription.PhFax
                _pharmacy.communication.email = Prescription.PhEmail
            End If

        Catch ex As Exception
            Throw New Exception("Error @GetEPAPharmacy " + ex.Message.ToString())
        End Try

        Return _pharmacy
    End Function

#End Region

    Public Function ePAValidation() As Boolean
        Dim ePAValidationMessage As New System.Text.StringBuilder
        Dim blnValid As Boolean = True

        If Not ePALengthValidation() Then
            Return False
        End If
        Try
            For icnt As Int32 = 0 To _RxBusinessLayer.PrescriptionCol.Count - 1
                If _RxBusinessLayer.PrescriptionCol(icnt).State <> "D" AndAlso _RxC1Flexgrid.getCheckedState(icnt + 1) = True Then
                    Using P As Prescription = _RxBusinessLayer.PrescriptionCol(icnt)
                        If P IsNot Nothing Then

                            Dim strQuantity As String = ""
                            If Not IsNothing(P.Amount) Then
                                Dim sdrugAmountArray As String()
                                sdrugAmountArray = P.Amount.Split(" ")
                                If sdrugAmountArray.Length > 0 Then
                                    strQuantity = sdrugAmountArray(0).Trim
                                End If
                            End If
                            If strQuantity.Trim = "" Then
                                ePAValidationMessage.Append("This ePA request cannot be sent because the drug quantity is not specified for " & P.Medication & ". Please specify the quantity." & vbCrLf)
                            ElseIf Not IsNumeric(strQuantity) Then
                                ePAValidationMessage.Append("This ePA request cannot be sent because the drug quantity is not a number for " & P.Medication & ". Please specify the quantity with a number." & vbCrLf)
                            ElseIf Val(strQuantity) = 0 Then
                                ePAValidationMessage.Append("This ePA request cannot be sent because the drug quantity is Zero for " & P.Medication & ". Please specify the quantity." & vbCrLf)
                            End If
                            If Not IsNothing(P.PotencyCode) Then
                                If P.PotencyCode.Trim = "" Then
                                    ePAValidationMessage.Append("This ePA request cannot be sent because the drug PotencyCode field is blank for " & P.Medication & ". Please enter directions for taking this medication." & vbCrLf)
                                End If
                            Else
                                ePAValidationMessage.Append("This ePA request cannot be sent because the drug PotencyCode field is blank for " & P.Medication & ". Please enter directions for taking this medication." & vbCrLf)
                            End If


                            If P.PharmacyID <> 0 Then
                                Dim sNPI As String = String.Empty

                                Using ePADatabaseLayer As New RxBusinesslayer(nPatientProviderId)
                                    sNPI = ePADatabaseLayer.GetPharmacyNPIFromNCPDID(P.PhNCPDPID)
                                End Using

                                If CheckString(P.PharmacyName) = False Then
                                    ePAValidationMessage.Append("This ePA request cannot be sent because the Pharmacy name is blank for " & P.Medication & "." & vbCrLf)
                                End If

                                If CheckString(P.PhNCPDPID) = False Then
                                    ePAValidationMessage.Append("This ePA request cannot be sent because the Pharmacy NCPDPID for " & P.PharmacyName + " is blank for " & P.Medication & "." & vbCrLf)
                                End If

                                If CheckString(P.PhPhone) = False Then
                                    ePAValidationMessage.Append("This ePA request cannot be sent because the Pharmacy Phone for " & P.PharmacyName + " is blank/invalid for " & P.Medication & "." & vbCrLf)
                                End If

                                If CheckString(sNPI) = False Then
                                    ePAValidationMessage.Append("This ePA request cannot be sent because the Pharmacy NPI for " & P.PharmacyName + " is blank for " & P.Medication & "." & vbCrLf)
                                End If
                                sNPI = String.Empty
                            End If
                        End If
                    End Using
                End If
            Next
            If ePAValidationMessage.Length > 0 Then
                System.Windows.Forms.MessageBox.Show(ePAValidationMessage.ToString, "gloEMR", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Information)
                blnValid = False
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
            Return False
        Finally
            If Not IsNothing(ePAValidationMessage) Then
                ePAValidationMessage = Nothing
            End If
        End Try
        Return blnValid
    End Function

    Private Function CheckString(ByVal InputString As String) As Boolean
        Return InputString IsNot Nothing AndAlso Not String.IsNullOrEmpty(InputString) AndAlso Not String.IsNullOrWhiteSpace(InputString)
    End Function

    Private Function ePALengthValidation() As Boolean
        Dim blnIsValid As Boolean = True
        Dim ePALenValidationMessage As New System.Text.StringBuilder
        Try
            If Not IsNothing(_RxBusinessLayer.PrescriptionCol) Then
                If _RxBusinessLayer.PrescriptionCol.Count > 0 Then
                    '''''''''''''''''Prescriber Validation------------------------------------------------------------------
                    If Not IsNothing(strPrescriberLastName) Then
                        If strPrescriberLastName.Trim.Length > 35 Then
                            ePALenValidationMessage.Append("Prescriber LastName(35)")
                        End If
                    End If

                    If Not IsNothing(strPrescriberFirstName) Then
                        If strPrescriberFirstName.Trim.Length > 35 Then
                            ePALenValidationMessage.Append("Prescriber FirstName(35)")
                        End If
                    End If
                    'Prescriber First Name and Middle Name
                    If Not IsNothing(strPrescriberAddress) Then
                        If strPrescriberAddress.Length > 35 Then
                            ePALenValidationMessage.Append("Prescriber Address1(35)")
                        End If
                    End If

                    If Not IsNothing(strPrescriberCity) Then
                        If strPrescriberCity.Trim.Length > 35 Then
                            ePALenValidationMessage.Append("Prescriber City(35)")
                        End If
                    End If
                    If Not IsNothing(strPrescriberState) Then
                        If strPrescriberState.Length > 9 Then
                            ePALenValidationMessage.Append("Prescriber State(9)")
                        End If
                    End If

                    If Not IsNothing(strPrescriberZip) Then
                        If strPrescriberZip.ToString.Length > 11 Then
                            ePALenValidationMessage.Append("Prescriber Zip(11)")
                        End If
                    End If
                    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

                    '''''''''''''''''Supervisor Validation------------------------------------------------------------------
                    If cmbSupervisingProvider.Visible = True Then
                        If cmbSupervisingProvider.SelectedIndex <> -1 Then
                            If Not IsNothing(strSupervisingPrescriberLastName) Then
                                If strSupervisingPrescriberLastName.Trim.Length > 35 Then
                                    ePALenValidationMessage.Append("Supervisor LastName(35)")
                                End If
                            End If

                            If Not IsNothing(strSupervisingPrescriberFirstName) Then
                                If strSupervisingPrescriberFirstName.Trim.Length > 35 Then
                                    ePALenValidationMessage.Append("Supervisor FirstName(35)")
                                End If
                            End If
                            'Prescriber First Name and Middle Name
                            If Not IsNothing(strSupervisingPrescriberAddress) Then
                                If strSupervisingPrescriberAddress.Length > 35 Then
                                    ePALenValidationMessage.Append("Supervisor Address1(35)")
                                End If
                            End If

                            If Not IsNothing(strSupervisingPrescriberCity) Then
                                If strSupervisingPrescriberCity.Trim.Length > 35 Then
                                    ePALenValidationMessage.Append("Supervisor City(35)")
                                End If
                            End If
                            If Not IsNothing(strSupervisingPrescriberState) Then
                                If strSupervisingPrescriberState.Length > 9 Then
                                    ePALenValidationMessage.Append("Supervisor State(9)")
                                End If
                            End If

                            If Not IsNothing(strSupervisingPrescriberZip) Then
                                If strSupervisingPrescriberZip.ToString.Length > 11 Then
                                    ePALenValidationMessage.Append("Supervisor Zip(11)")
                                End If
                            End If
                        End If
                    End If

                    '''''''''Patient Validation------------------------------------------------------------------------------------------------- 

                    If Not IsNothing(strPatientLastName) Then
                        If strPatientLastName.Trim.Length > 35 Then
                            ePALenValidationMessage.Append("Patient LastName(35)")
                        End If
                    End If

                    If Not IsNothing(strPatientFirstName) Then
                        If strPatientFirstName.Trim.Length > 35 Then
                            ePALenValidationMessage.Append("Patient FirstName(35)")
                        End If
                    End If


                    '''''10.6 length validations
                    If Not IsNothing(strPatientAddress) Then
                        If strPatientAddress.Trim.Length > 35 Then
                            ePALenValidationMessage.Append("Patient Address1(35)")
                        End If
                    End If
                    If Not IsNothing(strPatientAddress) Then
                        If strPatientAddress.Trim.Length > 35 Then
                            ePALenValidationMessage.Append("Patient City(35)")
                        End If
                    End If
                    If Not IsNothing(strPatientState) Then
                        If strPatientState.Trim.Length > 9 Then
                            ePALenValidationMessage.Append("Patient State(9)")
                        End If
                    End If
                    If Not IsNothing(strPatientZip) Then
                        If strPatientZip.Trim.Length > 11 Then
                            ePALenValidationMessage.Append("Patient Zip(11)")
                        End If
                    End If

                    '''''10.6 length validations

                    Dim drugname As String = ""
                    ''added in 6060  for multiple pharmacy logic
                    For icnt As Int32 = 0 To _RxBusinessLayer.PrescriptionCol.Count - 1
                        If _RxBusinessLayer.PrescriptionCol(icnt).State <> "D" AndAlso _RxC1Flexgrid.getCheckedState(icnt + 1) = True Then
                            If Not IsNothing(_RxBusinessLayer.PrescriptionCol.Item(icnt).PharmacyID) Then
                                If _RxBusinessLayer.PrescriptionCol.Item(icnt).PharmacyID.ToString.Length > 35 Then
                                    ePALenValidationMessage.Append("Pharmacy NCPDPID(35)")
                                End If
                            End If

                            If Not IsNothing(_RxBusinessLayer.PrescriptionCol.Item(icnt).PharmacyName) Then
                                If _RxBusinessLayer.PrescriptionCol.Item(icnt).PharmacyName.Trim.Length > 35 Then
                                    ePALenValidationMessage.Append("Pharmacy Name(35)")
                                End If
                            End If

                            If Not IsNothing(_RxBusinessLayer.PrescriptionCol.Item(icnt).Frequency) Then
                                If _RxBusinessLayer.PrescriptionCol.Item(icnt).Frequency.Trim.Length > 140 Then
                                    ePALenValidationMessage.Append("Drug Directions(140)")
                                End If
                            End If

                            If Not IsNothing(_RxBusinessLayer.PrescriptionCol.Item(icnt).Amount) Then
                                Dim sdrugAmountArray As String()
                                sdrugAmountArray = _RxBusinessLayer.PrescriptionCol.Item(icnt).Amount.Split(" ")
                                If sdrugAmountArray.Length > 0 Then
                                    If sdrugAmountArray(0).Trim.Length > 11 Then
                                        ePALenValidationMessage.Append("Drug Quantity(11)")
                                    End If
                                End If
                            End If
                            If _RxBusinessLayer.PrescriptionCol.Item(icnt).RefillQualifier <> "PRN" Then
                                If _RxBusinessLayer.PrescriptionCol.Item(icnt).RefillQuantity <> "" Then
                                    If _RxBusinessLayer.PrescriptionCol.Item(icnt).RefillQuantity.Length > 2 Then
                                        ePALenValidationMessage.Append("Drug RefillQuantity(2)")
                                    End If
                                End If
                            End If
                        End If
                    Next
                End If
            End If
            If ePALenValidationMessage.Length > 0 Then
                If System.Windows.Forms.MessageBox.Show("Following data fields exceed number of characters allowed in Surescripts standards and will therefore be truncated before sending to Surescripts and  (Allowed characters are shown in parenthesis) " + System.Environment.NewLine + System.Environment.NewLine + System.Environment.NewLine + ePALenValidationMessage.ToString & System.Environment.NewLine + System.Environment.NewLine & System.Environment.NewLine & "Do you want to continue?", "gloEMR", Windows.Forms.MessageBoxButtons.YesNo, Windows.Forms.MessageBoxIcon.Question) = MsgBoxResult.No Then
                    blnIsValid = False
                    Return blnIsValid
                End If
            End If
            Return blnIsValid
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
            Return Nothing
        End Try
    End Function

    Private Sub _RxC1Flexgrid_DrugPARequested(SelectedRow As Integer)
        Dim frmViewEPAProcess As frmViewEPAProcess = Nothing

        Try
            Dim paRefID As String = Nothing

            If _RxBusinessLayer.PrescriptionCol(SelectedRow) IsNot Nothing Then
                paRefID = Convert.ToString(_RxBusinessLayer.PrescriptionCol(SelectedRow).PAReferenceID)
            End If

            If Not String.IsNullOrWhiteSpace(paRefID) Then
                frmViewEPAProcess = New frmViewEPAProcess(gnLoginID, _RxBusinessLayer.ProviderID, _RxBusinessLayer.PrescriptionCol(SelectedRow).PAReferenceID, GetConnectionString())
                frmViewEPAProcess.ShowDialog(Me)
            Else
                MessageBox.Show("There is no ePA request initiated for this drug.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

            RaiseEvent PerformEPAStatusCheck(SelectedRow)

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally
            If frmViewEPAProcess IsNot Nothing Then
                frmViewEPAProcess.Dispose()
                frmViewEPAProcess = Nothing
            End If
        End Try
    End Sub

    Private Sub frmPrescription_PerformEPAStatusCheck(ByVal row As Integer?)
        Try
            '' Atlease a Prescription should be selected to proceeed with EPA
            If IsNothing(_RxBusinessLayer.PrescriptionCol) OrElse _RxBusinessLayer.PrescriptionCol.Count <= 0 Then
                Exit Sub
            End If

            '' If RxElig is not done then dont do anything
            If IsNothing(_objFormularyToolBar) AndAlso _objFormularyToolBar.CurrentEligibilityStatus = RxBusinesslayer.EligibilityStatus.NotChecked Then
                Return
            End If

            If row.HasValue Then  ''Selected Prescription
                If _RxBusinessLayer.PrescriptionCol(row.Value).State <> "D" Then
                    UpdateEPAReference(row.Value)
                    _RxC1Flexgrid.UpdateEPAStatusRow(row)
                End If
            Else ''Multiple prescription
                For icnt As Int32 = 0 To _RxBusinessLayer.PrescriptionCol.Count - 1
                    If _RxBusinessLayer.PrescriptionCol(icnt).State <> "D" Then
                        UpdateEPAReference(icnt)
                    End If
                Next
                _RxC1Flexgrid.UpdateEPAStatus()
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.DrugInteraction, gloAuditTrail.ActivityType.None, "frmPrescription_PerformEPAStatusCheck :" + If(ex.InnerException IsNot Nothing, ex.InnerException, ex).ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub UpdateEPAReference(ByVal row As Integer)

        If _RxBusinessLayer.PrescriptionCol(row) IsNot Nothing Then
            Dim p As Prescription = _RxBusinessLayer.PrescriptionCol(row)

            Dim Qty As String = String.Empty
            Dim DaySupply As String = String.Empty

            Dim sPBMMemberId As String = _objFormularyToolBar.PBMMemberID
            Dim mpid As Int32 = p.mpid
            Dim ndc As String = p.NDCCode

            Dim activeEPA As ActiveEPA = Nothing

            If Not String.IsNullOrWhiteSpace(p.Amount) Then
                If IsNumeric(p.Amount.Split(" ")(0)) Then
                    Qty = p.Amount.Split(" ")(0)
                End If
            End If

            If Not String.IsNullOrWhiteSpace(p.DaysSupply) Then
                If IsNumeric(p.DaysSupply) Then
                    DaySupply = p.DaysSupply
                End If
            End If

            Dim PAReferenceID As String = String.Empty
            Dim PriorAuthorizationStatus As String = String.Empty
            Dim PriorAuthorizationNumber As String = String.Empty

            If Not IsNothing(Me.ActiveEPAList) AndAlso Me.ActiveEPAList.Count >= 0 Then
                activeEPA = Me.ActiveEPAList.Where(Function(item) (item.mpid = mpid OrElse item.ndc = ndc) AndAlso item.Qty = Qty AndAlso ((item.DaySupply = "" AndAlso DaySupply = 0) OrElse (item.DaySupply = DaySupply)) AndAlso item.PBMMemberId = sPBMMemberId).FirstOrDefault()

                If Not IsNothing(activeEPA) Then
                    Using ePARequest As New EPA.gloEPAHelper(gstrEPAServiceURL)

                        Dim respStatus As EPA.PAStatusResponse = Nothing
                        PAReferenceID = activeEPA.PAReferenceID
                        PriorAuthorizationStatus = activeEPA.Status
                        PriorAuthorizationNumber = activeEPA.PANumber
                        Using ePAInsertUpdate As New EPABusinesslayer()
                            If PriorAuthorizationStatus.ToUpper() <> "APPROVED" AndAlso Not ePAInsertUpdate.epa_IsManualPriorAuthorization(nRxModulePatientID, PAReferenceID) Then
                                respStatus = ePARequest.GetPAStatus(PAReferenceID)
                            End If
                        End Using

                        If Not IsNothing(respStatus) Then

                            If respStatus.stat <> PriorAuthorizationStatus And String.IsNullOrWhiteSpace(respStatus.error) Then
                                Using _EpaBusineslayer As New EPABusinesslayer()
                                    _EpaBusineslayer.UpdateEPAStatusINDB(_RxBusinessLayer.ProviderID, nRxModulePatientID, PAReferenceID, respStatus.exp, respStatus.pan, respStatus.stat)
                                    Me.RefreshActiveEPAList()
                                End Using
                                If Not String.IsNullOrEmpty(respStatus.stat) Then
                                    PriorAuthorizationStatus = respStatus.stat
                                End If
                                If Not String.IsNullOrEmpty(respStatus.pan) Then
                                    PriorAuthorizationNumber = respStatus.pan
                                End If
                            ElseIf Not String.IsNullOrWhiteSpace(respStatus.error) Then
                                PriorAuthorizationStatus = String.Empty
                                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.EPA, gloAuditTrail.ActivityCategory.PriorAuthorization, gloAuditTrail.ActivityType.Send, "Error in ePA Web Service at: " & PriorAuthorizationStatus, gloAuditTrail.ActivityOutCome.Failure)
                            End If
                        End If
                    End Using
                End If
            End If

            If Convert.ToString(p.PAReferenceID) <> PAReferenceID AndAlso p.State = "U" Then
                p.State = "M"
            End If

            p.PAReferenceID = PAReferenceID
            p.PriorAuthorizationStatus = PriorAuthorizationStatus
            p.PriorAuthorizationNumber = PriorAuthorizationNumber

            p = Nothing
        End If
    End Sub

    Public Sub LoadEPAUserRoles()
        Try
            _objFormularyToolBar.tlbbtn_ePA.Enabled = False
            _RxC1Flexgrid.RemoveEPAContextMenu()

            Using _EpaBusineslayer As New EPABusinesslayer()
                Dim DataRow As DataRow = _EpaBusineslayer.GetUserRole(gnLoginID, _RxBusinessLayer.ProviderID)
                If DataRow IsNot Nothing Then
                    Dim EPARole As EPA.RoleType = EPA.RoleType.None
                    If [Enum].TryParse(Convert.ToInt16(DataRow("nPARoleID")), EPARole) AndAlso EPARole <> EPA.RoleType.None Then
                        _RxC1Flexgrid.AddEPAContextMenu()
                        If EPARole = EPA.RoleType.PAPreparer OrElse EPARole = EPA.RoleType.PASubmitter Then
                            _objFormularyToolBar.tlbbtn_ePA.Enabled = True
                        End If
                    End If
                End If
            End Using
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.DrugInteraction, gloAuditTrail.ActivityType.None, "frmPrescription_PerformEPAStatusCheck :" + If(ex.InnerException IsNot Nothing, ex.InnerException, ex).ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Public Sub LoadPDMPRoles()
        Try
            _objFormularyToolBar.tlsPDMP.Enabled = clsgeneral.gblnIsPDMPEnabled
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.DrugInteraction, gloAuditTrail.ActivityType.None, "frmPrescription_PerformEPAStatusCheck :" + If(ex.InnerException IsNot Nothing, ex.InnerException, ex).ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub



    Public Sub CloseEPAWindow()
        Try
            Dim frmViewEPAProcess As frmViewEPAProcess = My.Application.OpenForms.OfType(Of frmViewEPAProcess).FirstOrDefault()

            If frmViewEPAProcess IsNot Nothing Then
                frmViewEPAProcess.Close()
                frmViewEPAProcess = Nothing
            End If
            Me.LoadEPAUserRoles()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.DrugInteraction, gloAuditTrail.ActivityType.None, "frmPrescription_CloseEPAWindow :" + If(ex.InnerException IsNot Nothing, ex.InnerException, ex).ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

#End Region

#Region "PDR"

    Private Shared myPrinterSetting As System.Drawing.Printing.PrinterSettings = Nothing

    Private Function GetPDRProgramFiles() As List(Of String)
        Dim program_files As New List(Of String)
        Try
            Using dbLayer As New PatientCommunicationBusinessLayer()
                For icnt As Int32 = 0 To _RxBusinessLayer.PrescriptionCol.Count - 1
                    If _RxBusinessLayer.PrescriptionCol(icnt).State <> "D" AndAlso _RxC1Flexgrid.getCheckedState(icnt + 1) = True Then
                        ''Skip the drug incase of IssueRx for eRx
                        If _RxBusinessLayer.PrescriptionCol.Item(icnt).Method <> "Print" Then
                            Continue For
                        End If
                        Using P As Prescription = _RxBusinessLayer.PrescriptionCol(icnt)
                            If P.PCTransactionID > 0 Then
                                P.PDRPrograms = dbLayer.GetAllPrograms(P.PCTransactionID)
                                If P.PDRPrograms IsNot Nothing AndAlso TypeOf (P.PDRPrograms) Is ProgramResponse Then
                                    For Each program As Program In DirectCast(P.PDRPrograms, ProgramResponse).Programs
                                        program_files.Add(GetPDRProgramFile(program.image))
                                    Next
                                End If
                            End If
                        End Using
                    End If
                Next
            End Using
            Return program_files

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.PatientCommunication, gloAuditTrail.ActivityType.Print, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Return Nothing
        End Try
    End Function

    Private Function GetPDRProgramFile(ByVal programFile As String) As String
        Dim programFilePath As String = String.Empty
        Dim ogloPrintProgressController As gloPrintDialog.gloPrintProgressController = Nothing

        Try
            programFilePath = gloSettings.FolderSettings.AppTempFolderPath + "PDR_" + gloGlobal.clsFileExtensions.GetUniqueDateString("yyyyMMddHHmmssffff") + ".pdf"

            If File.Exists(programFilePath) Then
                File.Delete(programFilePath)
            End If

            File.WriteAllBytes(programFilePath, Convert.FromBase64String(programFile))

            Return programFilePath
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.PatientCommunication, gloAuditTrail.ActivityType.Print, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Return Nothing
        End Try
    End Function

    Private Function PrintAllPDRPrograms(ByVal PDRFilesToPrint As List(Of String)) As Boolean

        Dim ogloPrintProgressController As gloPrintDialog.gloPrintProgressController = Nothing
        Dim IsPrintSucess As Boolean = True

        Try
            myPrinterSetting = New System.Drawing.Printing.PrinterSettings()
            myPrinterSetting.FromPage = 1
            myPrinterSetting.ToPage = myPrinterSetting.MaximumPage

            Using oDialog As New gloPrintDialog.gloPrintDialog()
                oDialog.ConnectionString = GetConnectionString()
                oDialog.TopMost = False
                oDialog.ShowPrinterProfileDialog = True

                If oDialog IsNot Nothing Then

                    If Not gloGlobal.gloTSPrint.isCopyPrint Then
                        oDialog.PrinterSettings = myPrinterSetting
                    End If
                    Dim result As DialogResult = oDialog.ShowDialog(Me)
                    If result = System.Windows.Forms.DialogResult.OK Then
                        If Not gloGlobal.gloTSPrint.isCopyPrint Then
                            myPrinterSetting = oDialog.PrinterSettings
                        End If

                        Dim lstDocs As New List(Of gloPrintDialog.gloPrintProgressController.DocumentInfo)
                        Dim pdrDocInfo As New gloPrintDialog.gloPrintProgressController.DocumentInfo()

                        For i As Integer = 0 To PDRFilesToPrint.Count - 1
                            pdrDocInfo = New gloPrintDialog.gloPrintProgressController.DocumentInfo()
                            pdrDocInfo.PdfFileName = PDRFilesToPrint(i)
                            pdrDocInfo.SrcFileName = PDRFilesToPrint(i)
                            pdrDocInfo.footerInfo = Nothing
                            lstDocs.Add(pdrDocInfo)
                            pdrDocInfo = Nothing
                        Next

                        ogloPrintProgressController = New gloPrintDialog.gloPrintProgressController(lstDocs, oDialog.PrinterSettings, oDialog.CustomPrinterExtendedSettings, blnUseEMFForSSRS:=False, blnisPDRPrinting:=True)
                        ogloPrintProgressController.ShowProgress(Me)
                        If ogloPrintProgressController.isTSPrintCancelled Then
                            IsPrintSucess = False
                        End If

                    ElseIf result = System.Windows.Forms.DialogResult.Cancel Then
                        IsPrintSucess = False
                    End If


                Else
                    Dim _ErrorMessage As String = "Error in Showing Print Dialog"
                    IsPrintSucess = False
                    If _ErrorMessage.Trim() <> "" Then
                        Dim _MessageString As String = Convert.ToString("Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : ") & _ErrorMessage
                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString)
                        _MessageString = ""
                    End If
                End If


            End Using
            Return IsPrintSucess
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.PatientCommunication, gloAuditTrail.ActivityType.Print, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Return False
        End Try
    End Function

    Private Sub PrintPDRPrograms(ByVal i As List(Of ProgramResponse)) Handles _RxBusinessLayer.PrintPDRPrograms
        Dim lbltxt As String = Label35.Text
        pnlDIProgress.Visible = True
        Label35.Text = "Printing PDR Programs.."
        Application.DoEvents()

        Dim pdrFiles As New List(Of String)

        Dim dtPrograms As New DataTable()
        Dim dRow As DataRow = Nothing
        Dim ackRequest As Acknowledgement.AcknowledgementRequest = Nothing
        Dim ackProgram As Acknowledgement.program = Nothing
        Dim sBuilder As New StringBuilder()

        Try
            With dtPrograms
                .Columns.Add("nTransactionID", Type.GetType("System.Int64"))
                .Columns.Add("nProgramID", Type.GetType("System.Int64"))
            End With

            For Each response As ProgramResponse In i
                If response IsNot Nothing Then
                    For Each program As Program In response.Programs
                        pdrFiles.Add(GetPDRProgramFile(program.image))

                        dRow = dtPrograms.NewRow()
                        dRow("nTransactionID") = response.TransactionID
                        dRow("nProgramID") = Convert.ToInt32(program.id)

                        dtPrograms.Rows.Add(dRow)

                        If ackRequest Is Nothing Then
                            ackRequest = New Acknowledgement.AcknowledgementRequest()
                            ackRequest.transactionID = response.TransactionID
                        End If

                        ackProgram = New Acknowledgement.program(program.id)
                        ackRequest.programs.Add(ackProgram)
                        ackProgram = Nothing
                    Next
                End If
            Next

            ''print all documents
            If pdrFiles.Count > 0 Then
                If PrintAllPDRPrograms(pdrFiles) Then
                    Me.UpdatePrintedAndAck(ackRequest, dtPrograms)
                Else
                    PDRProgramPrintCanceled()
                End If
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.PatientCommunication, gloAuditTrail.ActivityType.Print, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        Finally
            If Not IsNothing(dtPrograms) Then
                dtPrograms.Dispose()
                dtPrograms = Nothing
            End If
            If ackRequest IsNot Nothing Then
                ackRequest.Dispose()
                ackRequest = Nothing
            End If
            pnlDIProgress.Visible = False
            Label35.Text = lbltxt
            Application.DoEvents()
        End Try
    End Sub

    Private Function UpdatePrintedAndAck(ByVal ackRequest As Acknowledgement.AcknowledgementRequest, ByVal dtPrograms As DataTable)
        Dim sBuilder As StringBuilder = Nothing
        Dim dbLayer As PatientCommunicationBusinessLayer = Nothing
        Dim returned As Boolean = False
        Try
            If ackRequest IsNot Nothing Then
                sBuilder = New StringBuilder()
                dbLayer = New PatientCommunicationBusinessLayer()

                If dbLayer.UpdatePrintedStatus(dtPrograms) Then
                    sBuilder.Append("PDR Programs printed successfully for program id: ")
                    For Each element As Acknowledgement.program In ackRequest.programs
                        sBuilder.Append(element.id + " ")
                    Next

                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.PatientCommunication, gloAuditTrail.ActivityType.Print, sBuilder.ToString(), nRxModulePatientID, ackRequest.transactionID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)

                    If Me.SendAcknowledgement(ackRequest, dtPrograms) Then
                        sBuilder.Clear()
                        sBuilder.Append("Acknowledgement sent successfully for program id: ")

                        For Each element As Acknowledgement.program In ackRequest.programs
                            sBuilder.Append(element.id + " ")
                        Next
                        returned = True
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.PatientCommunication, gloAuditTrail.ActivityType.Send, sBuilder.ToString(), nRxModulePatientID, ackRequest.transactionID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                    Else
                        sBuilder.Clear()
                        sBuilder.Append("Acknowledgement failed for program id: ")

                        For Each element As Acknowledgement.program In ackRequest.programs
                            sBuilder.Append(element.id + " ")
                        Next

                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.PatientCommunication, gloAuditTrail.ActivityType.Print, sBuilder.ToString(), nRxModulePatientID, ackRequest.transactionID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                    End If


                Else
                    sBuilder.Append("PDR Programs printing failed for program id: ")

                    For Each element As Acknowledgement.program In ackRequest.programs
                        sBuilder.Append(element.id + " ")
                    Next

                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.PatientCommunication, gloAuditTrail.ActivityType.Print, sBuilder.ToString(), nRxModulePatientID, ackRequest.transactionID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.PatientCommunication, gloAuditTrail.ActivityType.Print, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        Finally

            If sBuilder IsNot Nothing Then
                sBuilder.Clear()
                sBuilder = Nothing
            End If

            If dbLayer IsNot Nothing Then
                dbLayer.Dispose()
                dbLayer = Nothing
            End If
        End Try

        Return returned
    End Function

    Private Function SendAcknowledgement(ByVal Acknowledgement As Acknowledgement.AcknowledgementRequest, ByVal DataOfPrograms As DataTable) As Boolean
        Dim PDRService As gloPDRHelper(Of Acknowledgement.printConfirmationResponse) = Nothing
        Dim dbLayer As PatientCommunicationBusinessLayer = Nothing
        Dim returned As Boolean = False
        Dim ack As Acknowledgement.printConfirmationResponse = Nothing
        Try
            PDRService = New gloPDRHelper(Of gloGlobal.Schemas.PDR.Acknowledgement.printConfirmationResponse)(gstrPDR_URL)
            dbLayer = New PatientCommunicationBusinessLayer()

            If mdlGeneral.gbnlPDR_EnableSerialization Then
                PDRService.Time = gloGlobal.clsFileExtensions.GetUniqueDateString("yyyyMMddHHmmssffff")
                PDRService.filePath = gloSettings.FolderSettings.AppTempFolderPath
            End If

            ack = PDRService.SendPrintConfirmation(Acknowledgement)

            If ack IsNot Nothing Then
                dbLayer.UpdateAcknowledgementStatus(DataOfPrograms)
                returned = True
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.PatientCommunication, gloAuditTrail.ActivityType.Print, "Error in SendAcknowledgement " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        Finally
            If PDRService IsNot Nothing Then
                PDRService.Dispose()
                PDRService = Nothing
            End If

            If dbLayer IsNot Nothing Then
                dbLayer.Dispose()
                dbLayer = Nothing
            End If

        End Try

        Return returned
    End Function

    Private Sub AcknowledgeToPDR()
        Dim dtPrograms As DataTable = Nothing
        Dim dRow As DataRow = Nothing
        Dim ackRequest As Acknowledgement.AcknowledgementRequest = Nothing
        Dim ackProgram As Acknowledgement.program = Nothing


        Try
            dtPrograms = New DataTable()

            With dtPrograms
                .Columns.Add("nTransactionID", Type.GetType("System.Int64"))
                .Columns.Add("nProgramID", Type.GetType("System.Int64"))
            End With

            For icnt As Int32 = 0 To _RxBusinessLayer.PrescriptionCol.Count - 1
                If _RxBusinessLayer.PrescriptionCol(icnt).State <> "D" AndAlso _RxC1Flexgrid.getCheckedState(icnt + 1) = True Then
                    'If _RxBusinessLayer.PrescriptionCol.Item(icnt).Method <> "Print" Then
                    '    Continue For
                    'End If
                    Using P As Prescription = _RxBusinessLayer.PrescriptionCol(icnt)
                        If P.PCTransactionID > 0 Then
                            'P.PDRPrograms = dbLayer.GetAllPrograms(P.PCTransactionID)
                            If ackRequest Is Nothing Then
                                ackRequest = New Acknowledgement.AcknowledgementRequest()
                            End If
                            If P.PDRPrograms IsNot Nothing AndAlso TypeOf (P.PDRPrograms) Is ProgramResponse Then
                                If DirectCast(P.PDRPrograms, ProgramResponse) IsNot Nothing Then
                                    Dim progResponse As ProgramResponse = DirectCast(P.PDRPrograms, ProgramResponse)
                                    ackRequest.transactionID = progResponse.TransactionID

                                    For Each program As Program In progResponse.Programs
                                        dRow = dtPrograms.NewRow()
                                        dRow("nTransactionID") = P.PCTransactionID
                                        dRow("nProgramID") = program.id
                                        dtPrograms.Rows.Add(dRow)

                                        ackProgram = New Acknowledgement.program(program.id)
                                        ackRequest.programs.Add(ackProgram)
                                        ackProgram = Nothing
                                    Next
                                    progResponse = Nothing
                                End If
                            End If

                        End If
                    End Using
                End If
            Next
            If dtPrograms.Rows.Count > 0 Then
                Me.UpdatePrintedAndAck(ackRequest, dtPrograms)
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.PatientCommunication, gloAuditTrail.ActivityType.Print, "Error in AcknowledgeToPDR " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        Finally
            If dtPrograms IsNot Nothing Then
                dtPrograms.Dispose()
                dtPrograms = Nothing
            End If
            dRow = Nothing
            If ackRequest IsNot Nothing Then
                ackRequest.Dispose()
                ackRequest = Nothing
            End If
            If ackProgram IsNot Nothing Then
                ackProgram.Dispose()
                ackProgram = Nothing
            End If
        End Try

    End Sub

    Public Sub PDRProgramPrintCanceled()
        Try
            Dim sBuilder As New StringBuilder
            For icnt As Int32 = 0 To _RxBusinessLayer.PrescriptionCol.Count - 1
                If _RxBusinessLayer.PrescriptionCol(icnt).State <> "D" AndAlso _RxC1Flexgrid.getCheckedState(icnt + 1) = True Then
                    Using P As Prescription = _RxBusinessLayer.PrescriptionCol(icnt)
                        If P.PCTransactionID > 0 Then
                            sBuilder.Clear()
                            sBuilder.Append("PDR Programs print canceled for program id: ")
                            sBuilder.Append(P.PCTransactionID.ToString() + " ")
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.PatientCommunication, gloAuditTrail.ActivityType.Print, sBuilder.ToString(), nRxModulePatientID, P.PCTransactionID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                        End If
                    End Using
                End If
            Next
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.PatientCommunication, gloAuditTrail.ActivityType.Initialize, "Error in AuditIFCanceledPrint " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub LoadPrograms(ByVal TupleObject As Object)
        Dim returned As ProgramResponse = Nothing
        Dim tupleOfRequest As Tuple(Of ProgramRequest, Prescription) = Nothing
        Dim prescription As Prescription = Nothing
        Dim programRequest As ProgramRequest = Nothing
        Try
            If TypeOf (TupleObject) Is Tuple(Of ProgramRequest, Prescription) Then
                tupleOfRequest = DirectCast(TupleObject, Tuple(Of ProgramRequest, Prescription))

                programRequest = tupleOfRequest.Item1
                prescription = tupleOfRequest.Item2

                Using PDRHelper As New gloPDRHelper(Of ProgramResponse)(gstrPDR_URL)

                    If mdlGeneral.gbnlPDR_EnableSerialization Then
                        PDRHelper.Time = gloGlobal.clsFileExtensions.GetUniqueDateString("yyyyMMddHHmmssffff")
                        PDRHelper.filePath = gloSettings.FolderSettings.AppTempFolderPath
                    End If

                    returned = PDRHelper.GetProgrammes(tupleOfRequest.Item1)
                End Using

                If returned.Programs.Any() Then
                    prescription.PCTransactionID = returned.TransactionID
                    If prescription.State.ToUpper() = "U" Then
                        prescription.State = "M"
                    End If

                    If prescription.PDRPrograms IsNot Nothing AndAlso TypeOf (prescription.PDRPrograms) Is ProgramResponse Then
                        DirectCast(prescription.PDRPrograms, ProgramResponse).Dispose()
                        prescription.PDRPrograms = Nothing
                    End If

                    prescription.PDRPrograms = returned
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.PatientCommunication, gloAuditTrail.ActivityType.Initialize, "Error in LoadPrograms " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        Finally
            returned = Nothing
            tupleOfRequest = Nothing
            programRequest = Nothing

            'If prescription IsNot Nothing Then
            '    Me.PerformPDRStatusCheck(prescription.ItemNumber)
            'End If
            prescription = Nothing
        End Try
    End Sub

    Private Sub _RxC1Flexgrid_PDRProgramsRequested(SelectedDrug As Integer?)
        If Not clsgeneral.gblnIsPDREnabled Then
            Exit Sub
        End If

        Dim request As ProgramRequest = Nothing
        Dim tupleOfRequest As Tuple(Of ProgramRequest, Prescription) = Nothing
        Dim prescription As Prescription = Nothing
        Dim action As Action(Of Object) = Nothing

        Try
            prescription = _RxBusinessLayer.PrescriptionCol.Item(SelectedDrug.Value)
            If prescription.MessageType <> "RefillRequest" AndAlso prescription.MessageType <> "RxChangeRequest" Then
                action = New Action(Of Object)(AddressOf Me.LoadPrograms)

                request = GetProgramRequest(prescription)

                tupleOfRequest = Tuple.Create(request, prescription)

                Dim TaskGetPrograms As New Task(action, tupleOfRequest)
                TaskGetPrograms.Start()
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.PatientCommunication, gloAuditTrail.ActivityType.Initialize, "Error in _RxC1Flexgrid_PDRProgramsRequested " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        Finally
            request = Nothing
            tupleOfRequest = Nothing
            prescription = Nothing
            action = Nothing
        End Try
    End Sub

    Private Function GetProgramRequest(ByVal Prescription As Prescription) As ProgramRequest
        Dim returned As New ProgramRequest()
        Dim nVisitID As Int64 = 0
        Dim nRefills As Int32 = 0
        Try
            returned.source = Application.ProductName + "_" + mdlGeneral.gstrVersion
            returned.portalID = mdlGeneral.gstrPDR_PC_PortalID
            returned.pFormat = mdlGeneral.gstrPDR_PC_PFormat
            returned.wFormat = "HTML"
            returned.output = "INLINE".ToLower()

            returned.productNDC = Prescription.NDCCode
            returned.refillsTotal = Prescription.Refills

            returned.rxNumber = mdlGeneral.getUniqueID()
            returned.sig = Prescription.Frequency
            returned.dosageForm = Prescription.DosageForm
            returned.dose = Prescription.Dosage
            returned.frequency = Prescription.Frequency
            returned.routeOfAdministration = Prescription.Route

            If Int32.TryParse(Prescription.Refills, nRefills) Then
                returned.renewalIndicator = If(nRefills = 0, "N", "R")
            Else
                returned.renewalIndicator = "N"
            End If

            returned.priorAuthorization = If(String.IsNullOrEmpty(Prescription.PAReferenceID) = True, "", "PA")

            If Not String.IsNullOrWhiteSpace(Prescription.Amount) AndAlso Prescription.Amount.Contains(" ") Then
                If IsNumeric(Prescription.Amount.Split(" ")(0)) Then
                    returned.quantity = Prescription.Amount.Split(" ")(0)
                End If
            End If

            If Not String.IsNullOrWhiteSpace(Prescription.DaysSupply) Then
                If IsNumeric(Prescription.DaysSupply) Then
                    returned.daysSupplied = Prescription.DaysSupply
                End If
            End If

            Select Case Prescription.Method.ToUpper()
                Case "FAX"
                    returned.prescriptionForm = "F"
                Case "PRINT", "OTC", "SAMPLE", "HANDWRITTEN"
                    returned.prescriptionForm = "H"
                Case "PHONE"
                    returned.prescriptionForm = "P"
                Case "ERX"
                    returned.prescriptionForm = "E"
            End Select
            nVisitID = mdlGenerateVisitID.GetVisitID(Now, Me.nRxModulePatientID)
            returned.patient = GetPatientElement()
            returned.financial.prescriptionBenefit.primary = GetFinancialElement(_objFormularyToolBar)
            returned.location = Me.GetLocationElement()
            returned.prescriber = Me.GetPrescriberElement(nPatientProviderId)
            returned.pharmacy = Me.GetPharmacyElement(nPatientProviderId, Prescription)

            With returned.patient
                .diagnosis = Me.GetDiagnosis(Me.nRxModulePatientID, nVisitID)
                .allergies = Me.GetAllergies(Me.nRxModulePatientID)
                .labs = Me.GetLabs(Me.nRxModulePatientID, nVisitID)
                .immunizations = Me.GetImmunizations(Me.nRxModulePatientID)
            End With

            Me.SetVitals(returned)

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.PatientCommunication, gloAuditTrail.ActivityType.Initialize, "Error in GetProgramRequest " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
        Return returned
    End Function

    Private Function GetPatientElement() As requestPatient
        Dim returned As New requestPatient()
        Try
            returned.code = nRxModulePatientID
            returned.firstname = strPatientFirstName
            returned.lastname = strPatientLastName
            returned.dob = strPatientDOB

            If strPatientGender.ToUpper() = "MALE" Then
                returned.gender = "M"
            ElseIf strPatientGender.ToUpper() = "FEMALE" Then
                returned.gender = "F"
            Else
                returned.gender = "U"
            End If

            returned.address = strPatientAddress + " " + strPatientAddress2
            returned.city = strPatientCity
            returned.state = strPatientState
            returned.zip = strPatientZip
            returned.phone = strPatientPhone
            returned.preference = "P"

            If _objFormularyToolBar IsNot Nothing Then
                Select Case _objFormularyToolBar.CurrentEligibilityStatus
                    Case RxBusinesslayer.EligibilityStatus.Failed
                        returned.eligibility = "0"
                    Case RxBusinesslayer.EligibilityStatus.Passed
                        returned.eligibility = "1"
                    Case Else
                        returned.eligibility = Nothing
                End Select
            Else
                returned.eligibility = Nothing
            End If



        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.PatientCommunication, gloAuditTrail.ActivityType.Initialize, "Error in GetPatientElement " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
        Return returned
    End Function

    Private Function GetFinancialElement(ByVal PBM As gloUserControlLibrary.gloFormularlyToolBarUserCtrl) As requestFinancialPrescriptionBenefitPrimary
        Dim returned As New requestFinancialPrescriptionBenefitPrimary()

        Try
            If _objFormularyToolBar IsNot Nothing AndAlso _objFormularyToolBar.CurrentEligibilityStatus = RxBusinesslayer.EligibilityStatus.Passed Then
                returned.claimType = GetClaimType(nRxModulePatientID)

                If PBM.BINLocationNumber IsNot Nothing Then
                    returned.bin = PBM.BINLocationNumber.Trim()
                End If

                If PBM.ProcessorIdentificationNumber IsNot Nothing Then
                    returned.pcn = PBM.ProcessorIdentificationNumber.Trim()
                End If

                If PBM.PBMMemberID IsNot Nothing Then
                    returned.planID = PBM.PBMMemberID.Trim()
                End If

                If PBM.PersonCode IsNot Nothing Then
                    returned.planCode = PBM.PersonCode.Trim()
                End If

                If PBM.PayerName IsNot Nothing Then
                    returned.planName = PBM.PayerName.Trim()
                End If

                If PBM.GroupID IsNot Nothing Then
                    returned.groupID = PBM.GroupID.Trim()
                End If

                If PBM.CardHolderID IsNot Nothing Then
                    returned.cardHolder = PBM.CardHolderID.Trim()
                End If
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.PatientCommunication, gloAuditTrail.ActivityType.Initialize, "Error in GetFinancialElement " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
        Return returned
    End Function

    Private Function GetPharmacyElement(ByVal PatientProviderID As Int64, ByVal Prescription As Prescription) As requestPharmacy
        Dim returned As New requestPharmacy()

        Try
            Dim sNPI As String = ""
            Using DBLayer As New RxBusinesslayer(nPatientProviderId)
                sNPI = DBLayer.GetPharmacyNPIFromNCPDID(Prescription.PhNCPDPID)
            End Using

            returned.code = sNPI
            returned.ncpdp = Prescription.PhNCPDPID
            returned.name = Prescription.PharmacyName
            returned.address = Prescription.PhAddressline1 + If(String.IsNullOrWhiteSpace(Prescription.PhAddressline2) = True, "", " " + Prescription.PhAddressline2)
            returned.city = Prescription.PhCity
            returned.state = Prescription.PhState
            returned.zip = Prescription.PhZip
            returned.phone = Prescription.PhPhone
            returned.fax = Prescription.PhFax
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.PatientCommunication, gloAuditTrail.ActivityType.Initialize, "Error in GetPharmacyElement " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
        Return returned
    End Function

    Private Function GetLocationElement() As requestLocation
        Dim returned As New requestLocation()

        Try
            returned.code = mdlGeneral.gstrAUSID
            returned.name = mdlGeneral.gstrClinicName
            returned.address = mdlGeneral.gstrClinicAddress
            returned.city = mdlGeneral.gstrClinicCity
            returned.state = mdlGeneral.gstrClinicState
            returned.zip = mdlGeneral.gstrClinicZip
            returned.phone = mdlGeneral.gstrClinicPhone
            returned.fax = mdlGeneral.gstrClinicFax
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.PatientCommunication, gloAuditTrail.ActivityType.Initialize, "Error in GetLocationElement " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
        Return returned
    End Function

    Private Function GetPrescriberElement(ByVal PatientProviderID As Int64?) As requestPrescriber

        Dim returned As New requestPrescriber()

        Dim strAddress1 As String = String.Empty
        Dim strAddress2 As String = String.Empty
        Dim strCity As String = String.Empty
        Dim strState As String = String.Empty
        Dim strCountry As String = String.Empty
        Dim strZip As String = String.Empty

        Dim strPhone As String = String.Empty
        Dim strFax As String = String.Empty

        Dim strFirstName As String = String.Empty
        Dim strMiddleName As String = String.Empty
        Dim strLastName As String = String.Empty

        Dim strNPI As String = String.Empty
        Dim strDEA As String = String.Empty
        Dim strSSN As String = String.Empty
        Dim strSPI As String = String.Empty

        Dim strEmail As String = String.Empty

        Try
            If PatientProviderID.HasValue Then
                Using dtProvider As DataTable = _RxBusinessLayer.GetPatientProviderDetails(nPatientProviderId)
                    If IsNothing(dtProvider) = False Then
                        If dtProvider.Rows.Count > 0 Then
                            strAddress1 = Convert.ToString(dtProvider.Rows(0)("sPrAddressline"))
                            strAddress2 = Convert.ToString(dtProvider.Rows(0)("sPrAddressLine2"))
                            strCity = Convert.ToString(dtProvider.Rows(0)("sPrCity"))
                            strState = Convert.ToString(dtProvider.Rows(0)("sPrState"))
                            strCountry = Convert.ToString(dtProvider.Rows(0)("sCountry"))
                            strZip = Convert.ToString(dtProvider.Rows(0)("sPrZip"))

                            strPhone = Convert.ToString(dtProvider.Rows(0)("sPrPhone"))
                            strFax = Convert.ToString(dtProvider.Rows(0)("sPrFax"))

                            strFirstName = Convert.ToString(dtProvider.Rows(0)("sPrFirstName"))
                            strMiddleName = Convert.ToString(dtProvider.Rows(0)("sPrMiddleName"))
                            strLastName = Convert.ToString(dtProvider.Rows(0)("sPrLastName"))

                            strDEA = Convert.ToString(dtProvider.Rows(0)("sPrDEA"))
                            strNPI = Convert.ToString(dtProvider.Rows(0)("sPrNPI"))
                            strSSN = Convert.ToString(dtProvider.Rows(0)("sPrSSN"))
                            strSPI = Convert.ToString(dtProvider.Rows(0)("sPrSPIID"))

                            strEmail = Convert.ToString(dtProvider.Rows(0)("sPrEmail"))
                        End If
                    End If
                End Using
            Else
                strAddress1 = strPrescriberAddress
                strAddress2 = strPrescriberAddress2
                strCity = strPrescriberCity
                strCountry = strPrescriberCountry
                strState = strPrescriberState
                strZip = strPrescriberZip

                strPhone = strPrescriberPhone
                strFax = strPrescriberFax

                strFirstName = strPrescriberFirstName
                strMiddleName = strPrescriberMiddleName
                strLastName = strPrescriberLastName

                strNPI = strPrescriberNPI
                strSSN = strPrescriberSSN
                strSPI = strPrescriberSPI
                strEmail = strPrescriberEmail
            End If

            If strAddress1.Length > 35 Then strAddress1 = strAddress1.Substring(0, 35)
            If strAddress2.Length > 35 Then strAddress2 = strAddress2.Substring(0, 35)
            If strCity.Length > 35 Then strCity = strCity.Substring(0, 35)
            If strCountry.Length > 35 Then strCountry = strCountry.Substring(0, 35)
            If strState.Length > 9 Then strState = strState.Substring(0, 9)
            If strZip.Length > 11 Then strZip = strZip.Substring(0, 11)

            If strFirstName.Length > 35 Then strFirstName = strFirstName.Substring(0, 35)
            If strMiddleName.Length > 35 Then strMiddleName = strMiddleName.Substring(0, 35)
            If strLastName.Length > 35 Then strLastName = strLastName.Substring(0, 35)

            With returned
                .code = strNPI
                .firstName = strFirstName
                .lastName = strLastName
                .address = strAddress1 + If(String.IsNullOrWhiteSpace(strAddress2) = True, "", strAddress2)
                .city = strCity
                .state = strState
                .zip = strZip
                .phone = strPhone
                .fax = strFax
                .email = strEmail
                .dea = strDEA
            End With

            Return returned

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.PatientCommunication, gloAuditTrail.ActivityType.Initialize, "Error in GetPrescriberElement " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Return Nothing
        End Try

    End Function

    Public Function GetDiagnosis(ByVal PatientID As Int64, ByVal VisitID As Int64) As List(Of requestPatientDiagnosis)
        Dim diagnosisList As New List(Of requestPatientDiagnosis)
        Dim dbLayer As PatientCommunicationBusinessLayer = Nothing
        Try
            dbLayer = New PatientCommunicationBusinessLayer()

            Using dsPatientData As DataSet = dbLayer.GetPatientDiagnosis(PatientID, VisitID) 'nRxModulePatientID)
                If dsPatientData IsNot Nothing AndAlso dsPatientData.Tables.Count > 0 Then
                    Dim dtPatientData As DataTable = dsPatientData.Tables(0)


                    If dtPatientData IsNot Nothing AndAlso dtPatientData.Rows.Count > 0 Then

                        diagnosisList = dtPatientData.AsEnumerable().Select( _
                                                            Function(p) New requestPatientDiagnosis() With _
                                                                        {.value = Convert.ToString(p("sCode")), _
                                                                        .description = Convert.ToString(p("sDescription")), _
                                                                        .codeSystem = Convert.ToString(p("sCodeSystem")), _
                                                                        .valueSpecified = True}).ToList()

                    End If
                End If
            End Using
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.PatientCommunication, gloAuditTrail.ActivityType.Initialize, "Error in GetDiagnosis " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        Finally
            If dbLayer IsNot Nothing Then
                dbLayer.Dispose()
                dbLayer = Nothing
            End If
        End Try
        Return diagnosisList
    End Function

    Public Function GetClaimType(ByVal PatientID As Int64) As String
        Dim sReturned As String = ""
        Dim dbLayer As PatientCommunicationBusinessLayer = Nothing
        Dim dtPatientData As DataTable = Nothing
        Try
            dbLayer = New PatientCommunicationBusinessLayer()

            Using dsPatientData As DataSet = dbLayer.GetClaimType(PatientID)
                If dsPatientData IsNot Nothing AndAlso dsPatientData.Tables.Count > 0 Then
                    dtPatientData = dsPatientData.Tables(0)
                    If dtPatientData IsNot Nothing AndAlso dtPatientData.Rows.Count > 0 Then
                        sReturned = Convert.ToString(dtPatientData.Rows(0)("sClaimType"))
                    End If
                End If
            End Using
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.PatientCommunication, gloAuditTrail.ActivityType.Initialize, "Error in GetClaimType " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        Finally
            If dbLayer IsNot Nothing Then
                dbLayer.Dispose()
                dbLayer = Nothing
            End If

            If dtPatientData IsNot Nothing Then
                dtPatientData.Dispose()
                dtPatientData = Nothing
            End If
        End Try
        Return sReturned
    End Function

    Public Function GetAllergies(ByVal PatientID As Int64) As List(Of requestPatientAllergy)
        Dim allergiesList As New List(Of requestPatientAllergy)

        Try
            For Each History As History In _RxBusinessLayer.HistoriesCol
                Dim localObject As New requestPatientAllergy
                localObject.value = History.DrugName
                allergiesList.Add(localObject)
            Next

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.PatientCommunication, gloAuditTrail.ActivityType.Initialize, "Error in GetAllergies " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try

        Return allergiesList
    End Function

    Public Function GetLabs(ByVal PatientID As Int64, ByVal VisitID As Int64) As List(Of requestPatientLab)
        Dim labsList As New List(Of requestPatientLab)
        Dim dbLayer As New PatientCommunicationBusinessLayer()
        Try
            Using dsPatientData As DataSet = dbLayer.GetPatientLabs(Me.nRxModulePatientID, VisitID)
                If dsPatientData IsNot Nothing AndAlso dsPatientData.Tables.Count > 0 Then
                    Dim dtPatientData As DataTable = dsPatientData.Tables(0)
                    If dtPatientData IsNot Nothing AndAlso dtPatientData.Rows.Count > 0 Then
                        labsList = dtPatientData.AsEnumerable().Select( _
                                                            Function(p) New requestPatientLab() With _
                                                                        {.value = Convert.ToString(p("sCode"))}).ToList()

                    End If
                End If
            End Using
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.PatientCommunication, gloAuditTrail.ActivityType.Initialize, "Error in GetLabs " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        Finally
            If dbLayer IsNot Nothing Then
                dbLayer.Dispose()
                dbLayer = Nothing
            End If
        End Try

        Return labsList
    End Function

    Public Function GetImmunizations(ByVal PatientID As Int64) As List(Of requestPatientImmunization)
        Dim immunizationList As New List(Of requestPatientImmunization)
        Dim dbLayer As New PatientCommunicationBusinessLayer()
        Try
            Using dsPatientData As DataSet = dbLayer.GetPatientImmunizations(PatientID)
                If dsPatientData IsNot Nothing AndAlso dsPatientData.Tables.Count > 0 Then
                    Dim dtPatientData As DataTable = dsPatientData.Tables(0)
                    If dtPatientData IsNot Nothing AndAlso dtPatientData.Rows.Count > 0 Then
                        immunizationList = dtPatientData.AsEnumerable().Select( _
                                                            Function(p) New requestPatientImmunization() With _
                                                                        {.value = Convert.ToString(p("sCode")), .valueSpecified = True}).ToList()

                    End If
                End If
            End Using
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.PatientCommunication, gloAuditTrail.ActivityType.Initialize, "Error in GetImmunizations " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        Finally
            If dbLayer IsNot Nothing Then
                dbLayer.Dispose()
                dbLayer = Nothing
            End If
        End Try

        Return immunizationList
    End Function

    Public Sub SetVitals(ByVal Request As ProgramRequest)
        Dim dbLayer As New PatientCommunicationBusinessLayer()

        Try
            Using dsPatientData As DataSet = dbLayer.GetPatientVitals(nRxModulePatientID)
                If dsPatientData IsNot Nothing AndAlso dsPatientData.Tables.Count > 0 Then
                    Dim dtPatientData As DataTable = dsPatientData.Tables(0)


                    If dtPatientData IsNot Nothing AndAlso dtPatientData.Rows.Count > 0 Then
                        Dim sHeight As String = Convert.ToString(dtPatientData.Rows(0)("dHeightinInch"))
                        Dim sWeight As String = Convert.ToString(dtPatientData.Rows(0)("dWeightinlbs"))

                        If sHeight <> "0" Then
                            Request.patient.height = sHeight
                        End If

                        If sWeight <> "0" Then
                            Request.patient.weight = sWeight
                        End If

                    End If
                End If
            End Using

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.PatientCommunication, gloAuditTrail.ActivityType.Initialize, "Error in SetPatientHeightWeight " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        Finally
            If dbLayer IsNot Nothing Then
                dbLayer.Dispose()
                dbLayer = Nothing
            End If
        End Try
    End Sub

    'Private Sub PerformPDRStatusCheck(ByVal row As Integer?)
    '    Try
    '        If _RxBusinessLayer.PrescriptionCol IsNot Nothing AndAlso _RxBusinessLayer.PrescriptionCol.Count > 0 Then
    '            If row.HasValue Then
    '                _RxC1Flexgrid.UpdatePDRStatusRow(row.Value)
    '            Else
    '                _RxC1Flexgrid.UpdatePDRStatus()
    '            End If
    '        End If
    '    Catch ex As Exception
    '        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.PatientCommunication, gloAuditTrail.ActivityType.Initialize, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
    '    End Try
    'End Sub

    Private Function GetRxChangeCOO() As RxChangeCOO

        Dim sPayerIdentification As schema.PayerIDType()
        Dim bReturn As RxChangeCOO = Nothing

        Try
            If (RxChangeRequest.BenifitsOfCord IsNot Nothing) Then
                bReturn = New RxChangeCOO()

                If (RxChangeRequest.BenifitsOfCord.PayerIdentification IsNot Nothing) Then
                    sPayerIdentification = RxChangeRequest.BenifitsOfCord.PayerIdentification.ToArray()

                    For i As Int16 = 0 To sPayerIdentification.Count - 1
                        If (sPayerIdentification(i).ItemElementName.ToString() = "BINLocationNumber") Then
                            bReturn.BINLocationNumber = sPayerIdentification(i).Item.ToString().Trim
                            Exit For
                        End If
                    Next
                End If

                If (RxChangeRequest.BenifitsOfCord.PayerName IsNot Nothing) Then
                    bReturn.PayerName = RxChangeRequest.BenifitsOfCord.PayerName.Trim
                End If

                If (RxChangeRequest.BenifitsOfCord.CardholderID IsNot Nothing) Then
                    bReturn.CardholderID = RxChangeRequest.BenifitsOfCord.CardholderID.Trim
                End If

                If (RxChangeRequest.BenifitsOfCord.GroupID IsNot Nothing) Then
                    bReturn.GroupID = RxChangeRequest.BenifitsOfCord.GroupID.Trim
                End If
            End If

            Return bReturn

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.PatientCommunication, gloAuditTrail.ActivityType.Initialize, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Return Nothing
        Finally
            sPayerIdentification = Nothing
        End Try

    End Function

#End Region

#Region "RxChange"
    'Private Sub CheckChangeRequestDrugAdded()
    '    Try
    '        If RxChangeRequest IsNot Nothing AndAlso Me.RxChangeRequest.Type = SS.ChangeRequestType.TherapeuticSubstitution Then
    '            If Me.RxChangeRequest.MedRequested Is Nothing AndAlso Not RxChangeRequest.IsChangeDrugAdded Then
    '                Dim p As Prescription = _RxBusinessLayer.PrescriptionCol.Item(_RxBusinessLayer.PrescriptionCol.Count - 1)
    '                p.Status = "ApprovedWithChanges"
    '                p.MessageType = "RxChangeRequest"
    '                p.Renewed = "Changed" & " " & Now & " " & globalSecurity.gstrLoginName
    '                p = Nothing

    '                RxChangeRequest.IsChangeDrugAdded = True
    '            End If
    '        End If
    '    Catch ex As Exception
    '        Cursor.Current = Cursors.Default
    '        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.eRx, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
    '        ex = Nothing
    '    End Try

    'End Sub
#End Region

#Region "PDMP"

    Private Sub PDMP_Link_LinkClicked()
        Dim sPDMPURL As String = ""
        Dim nReportID As Int64 = 0

        Dim frmPDMPWebBrowser As frmPDMPWebBrowser = Nothing
        Try
            frmPDMPWebBrowser = My.Application.OpenForms.OfType(Of frmPDMPWebBrowser).FirstOrDefault()

            If frmPDMPWebBrowser IsNot Nothing Then
                frmPDMPWebBrowser.BringToFront()
            Else
                Dim objurl As New gloRxHub.PDMP.PDMP(PDMPUsername, PDMPPassword) With {.ConnectionString = GetConnectionString(), .WebURL = PDMPServiceURL}

                If bPDMPReportSent = False Then
                    Me.PDMPSendReportRequest()
                    Me.PDMPView()
                End If

                Using dtURL As DataTable = objurl.GetViewableURL(nRxModulePatientID)
                    If dtURL IsNot Nothing AndAlso dtURL.AsEnumerable().Any() Then
                        sPDMPURL = dtURL.Rows(0)("sViewableURL")
                        nReportID = Convert.ToInt64(dtURL.Rows(0)("nReportID"))
                    End If

                    If sPDMPURL <> "" Then
                        frmPDMPWebBrowser = New frmPDMPWebBrowser() With {.viewURL = sPDMPURL, .ReportID = nReportID, .MdiParent = Me.ParentForm, .PatientID = nRxModulePatientID}
                        bPDMPReportSent = False
                        Task.Factory.StartNew(Sub()
                                                  Dim service As New gloRxHub.PDMP.PDMP(PDMPUsername, PDMPPassword) With {.ConnectionString = GetConnectionString(), .WebURL = PDMPServiceURL}
                                                  service.UpdateViewedURL(nReportID)
                                                  service = Nothing
                                              End Sub)

                        frmPDMPWebBrowser.Show()
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.PDMP, gloAuditTrail.ActivityCategory.PDMPView, gloAuditTrail.ActivityType.View, "PDMP Report View", 0, nReportID, nPatientProviderId, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR, gloAuditTrail.SoftwareComponent.gloEMR)
                    End If

                End Using
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CancelRx, gloAuditTrail.ActivityType.Cancle, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub PDMPProcess()
        Try
            pnlPDMP.Visible = True
            pnlAllergiesAlerts.Visible = True

            If NARXScoreControl Is Nothing Then
                NARXScoreControl = New gloUIControlLibrary.WPFUserControl.gloNARXScores()
                AddHandler NARXScoreControl.DrugAccepted, AddressOf Me.PDMP_Link_LinkClicked
            End If

            NARXScore = Nothing
            NARXScore = New gloGlobal.PDMP.Meds.NARXScores()
            elementHostPDMP.Child = NARXScoreControl
            NARXScoreControl.DataContext = NARXScore

            Dim taskPDMP As New Task(AddressOf Me.LoadPDMPData)
            taskPDMP.ContinueWith(AddressOf Me.PDMPSendReportRequest)
            taskPDMP.ContinueWith(AddressOf Me.PDMPView, CancellationToken.None, TaskContinuationOptions.None, TaskScheduler.FromCurrentSynchronizationContext)
            taskPDMP.Start()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, True)
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.PDMP, gloAuditTrail.ActivityCategory.PDMPPost, gloAuditTrail.ActivityType.Send, ex.ToString(), nRxModulePatientID, 0, _RxBusinessLayer.ProviderID, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR)
        End Try
    End Sub

    Private Sub PDMPView()
        Try
            If dtPDMP IsNot Nothing AndAlso dtPDMP.Rows.Count() > 0 Then
                NARXScoreControl.DataContext = Nothing
                NARXScoreControl.DataContext = NARXScore
                NARXScoreControl.BringIntoView()

                'If _RxListUserCtrl IsNot Nothing Then
                '    pnlAllergiesAlerts.Left = _RxListUserCtrl.Right + 400
                'End If

                'If _RxC1Flexgrid IsNot Nothing Then
                '    pnlAllergiesAlerts.Top = _RxC1Flexgrid.Top + 200
                'End If
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.PDMP, gloAuditTrail.ActivityCategory.PDMPView, gloAuditTrail.ActivityType.View, "PDMP data viewed", nRxModulePatientID, 0, _RxBusinessLayer.ProviderID, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR, gloAuditTrail.SoftwareComponent.gloEMR)
            Else
                NARXScore.NoContent = True
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, True)
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.PDMP, gloAuditTrail.ActivityCategory.PDMPView, gloAuditTrail.ActivityType.View, ex.ToString(), nRxModulePatientID, 0, _RxBusinessLayer.ProviderID, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR)
        End Try
    End Sub
    Dim bPDMPReportSent As Boolean = False

    Private Sub PDMPSendReportRequest()
        Dim ResponseID As Long = 0
        Dim ReportID As String = ""
        Dim sResponseContent As String = ""

        Dim xReportResponse As XElement = Nothing
        Dim xNARXScores As XElement = Nothing
        Dim xNarcoticsScore As XElement = Nothing
        Dim xStimulantsScore As XElement = Nothing
        Dim xOverdoseScore As XElement = Nothing
        Dim xSedativesScore As XElement = Nothing
        Dim xReport As XElement = Nothing
        Dim sNarcoticsScore As String = ""
        Dim sStimulantsScore As String = ""
        Dim sSedativesScore As String = ""
        Dim sOverdoseScore As String = ""
        Dim xScore As IEnumerable(Of XElement) = Nothing

        Try
            If NARXScore Is Nothing Then
                NARXScore = New gloGlobal.PDMP.Meds.NARXScores()
            End If

            NARXScore.Loading = False

            If dtPDMP IsNot Nothing AndAlso dtPDMP.Rows.Count() > 0 Then
                Dim pdmpService As New gloRxHub.PDMP.PDMP(PDMPUsername, PDMPPassword) With {.ConnectionString = GetConnectionString(), .WebURL = PDMPServiceURL}

                If Int64.TryParse(Convert.ToString(dtPDMP.Rows(0)("nResponseID")), ResponseID) Then
                    ReportID = Convert.ToString(dtPDMP.Rows(0)("sReportID"))
                    sResponseContent = dtPDMP.Rows(0).Item("sResponse")

                    If sResponseContent IsNot Nothing Then
                        xReport = XElement.Parse(sResponseContent)
                        xReportResponse = xReport.Elements().FirstOrDefault(Function(s) s.Name.LocalName.ToLower() = "report")

                        If xReportResponse IsNot Nothing Then
                            xNARXScores = xReportResponse.Elements().FirstOrDefault(Function(s) s.Name.LocalName.ToLower() = "narxscores")

                            xScore = xNARXScores.Elements().Where(Function(s) s.Name.LocalName.ToLower = "score")

                            If xScore IsNot Nothing Then
                                xNarcoticsScore = xScore.Elements().Where(Function(p) p.Name.LocalName.ToLower = "scoretype" AndAlso p.Value.ToLower() = "narcotics").FirstOrDefault()
                                xStimulantsScore = xScore.Elements().Where(Function(p) p.Name.LocalName.ToLower = "scoretype" AndAlso p.Value.ToLower() = "stimulants").FirstOrDefault()
                                xSedativesScore = xScore.Elements().Where(Function(p) p.Name.LocalName.ToLower = "scoretype" AndAlso p.Value.ToLower() = "sedatives").FirstOrDefault()
                                xOverdoseScore = xScore.Elements().Where(Function(p) p.Name.LocalName.ToLower = "scoretype" AndAlso p.Value.ToLower() = "overdose").FirstOrDefault()

                                If xNarcoticsScore IsNot Nothing Then
                                    sNarcoticsScore = xNarcoticsScore.Parent.Elements().FirstOrDefault(Function(p) p.Name.LocalName.ToLower() = "scorevalue").Value
                                End If

                                If xStimulantsScore IsNot Nothing Then
                                    sStimulantsScore = xStimulantsScore.Parent.Elements().FirstOrDefault(Function(p) p.Name.LocalName.ToLower = "scorevalue").Value
                                End If

                                If xSedativesScore IsNot Nothing Then
                                    sSedativesScore = xSedativesScore.Parent.Elements().FirstOrDefault(Function(p) p.Name.LocalName.ToLower = "scorevalue").Value
                                End If

                                If xOverdoseScore IsNot Nothing Then
                                    sOverdoseScore = xOverdoseScore.Parent.Elements().FirstOrDefault(Function(p) p.Name.LocalName.ToLower = "scorevalue").Value
                                End If

                                With NARXScore
                                    .Narcotic = sNarcoticsScore
                                    .Overdose = sOverdoseScore
                                    .Sedative = sSedativesScore
                                    .Stimulant = sStimulantsScore
                                End With
                            End If
                        Else
                            NARXScore.NoContent = True
                        End If
                    Else
                        NARXScore.NoContent = True
                    End If
                End If
                pdmpService.ReportRequest(nRxModulePatientID, _RxBusinessLayer.ProviderID, ResponseID, ReportID)

                bPDMPReportSent = True
                pdmpService = Nothing
            Else
                NARXScore.NoContent = True
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.PDMP, gloAuditTrail.ActivityCategory.PDMPPost, gloAuditTrail.ActivityType.Send, "PDMP Patient Request", nRxModulePatientID, 0, _RxBusinessLayer.ProviderID, gloAuditTrail.ActivityOutCome.Failure)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PDMP, gloAuditTrail.ActivityCategory.PDMPPost, gloAuditTrail.ActivityType.Send, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        Finally
            xReportResponse = Nothing
            xNARXScores = Nothing
            xNarcoticsScore = Nothing
            xStimulantsScore = Nothing
            xOverdoseScore = Nothing
            xSedativesScore = Nothing
            xReport = Nothing
            ResponseID = Nothing
            ReportID = Nothing
        End Try
    End Sub

    Private Sub LoadPDMPData()
        Dim PDMPService As New gloRxHub.PDMP.PDMP(PDMPUsername, PDMPPassword) With {.ConnectionString = GetConnectionString(), .WebURL = PDMPServiceURL}

        Try
            dtPDMP = PDMPService.GetReportFromDB(nRxModulePatientID, _RxBusinessLayer.ProviderID)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.PDMP, gloAuditTrail.ActivityCategory.PDMPView, gloAuditTrail.ActivityType.View, ex.ToString(), 0, 0, 0, gloAuditTrail.ActivityOutCome.Failure)
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, True)
        Finally
            PDMPService = Nothing
        End Try
    End Sub
#End Region

  
End Class


Public Class RxChangeCOO

    Public Sub New()

    End Sub

    Private _BINLocationNumber As String
    Public Property BINLocationNumber() As String
        Get
            Return _BINLocationNumber
        End Get
        Set(ByVal value As String)
            _BINLocationNumber = value
        End Set
    End Property

    Private _PayerName As String
    Public Property PayerName() As String
        Get
            Return _PayerName
        End Get
        Set(ByVal value As String)
            _PayerName = value
        End Set
    End Property

    Private _CardholderID As String
    Public Property CardholderID() As String
        Get
            Return _CardholderID
        End Get
        Set(ByVal value As String)
            _CardholderID = value
        End Set
    End Property

    Private _GroupID As String
    Public Property GroupID() As String
        Get
            Return _GroupID
        End Get
        Set(ByVal value As String)
            _GroupID = value
        End Set
    End Property

End Class
