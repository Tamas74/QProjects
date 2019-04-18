Imports System.Data.SqlClient
Imports C1.Win.C1FlexGrid
Imports System.IO
Imports System.Transactions
Imports System.Collections.Generic
Imports System.Reflection
Imports System.Xml
Imports System.Text
Imports System.Text.RegularExpressions
Imports gloSettings

Imports System.Net
Imports System.Configuration
Imports System.Web
Imports Microsoft.IdentityModel
Imports Microsoft.IdentityModel.Protocols.WSTrust
Imports System.ServiceModel
Imports System.Security.Principal
Imports System.ServiceModel.Channels
Imports System.Linq
Imports System.Runtime.InteropServices
Imports System.Runtime.Versioning



Public Enum FacilityType
    None = 0
    Facility = 1
    NonFacility = 3
End Enum
Public Enum SettingFlag

    None = 0
    Clinic = 1
    User = 2

End Enum




Public Class frmSettings_New
    Dim flagChk As Boolean = False
    Dim flagPrefixSettingON, flagPrefixSettingOFF As Boolean ''flag to check settings on or off (added by pradeep)
    Dim flagEpcsEnableOnLoad As Boolean
    Dim flagClose As Boolean = False
    Dim trvchkno As Integer = -1
    Dim GENERALMESSAGELOGPAGESIZE As Long = 0 '' add by Manoj Jadhav on 20110314
    Dim sSpirometryDeviceOrderPrefix As String = String.Empty
    Dim blnChk As Boolean = False
    Dim blnAllChkd As Boolean = False
    Dim CCDSection As String
    Dim CCDSectionVisit As String
    Dim MUCCDSection As String
    Dim _CCDSettingsname As String = ""
    Dim nonNumberEntered As Boolean = False
    Dim _blnglocominstalled As Boolean = False
    Dim ServiceNameSpace As String = ""
    Dim isFormLoad As Boolean = False
    Dim sStagingURl As String = String.Empty
    Dim sProductionURl As String = String.Empty
    Dim s10dot6StagingURl As String = String.Empty
    Dim s10dot6ProductionURl As String = String.Empty
    Dim sTempeRxserviceURl As String = String.Empty
    Dim sTemppdmpURL As String = String.Empty
    Dim sSecureMsgStaging As String = String.Empty
    Dim sSecureMsgProduction As String = String.Empty
    Dim sTempeSecureMsgURl As String = String.Empty
    Dim Is8dot1PendingRefReqComplete As Boolean = False
    Dim TempIs8dot1PendingRefReqComplete As Boolean = False
    Dim sMedHxStagingURl As String = String.Empty
    Dim sMedHxProductionURl As String = String.Empty
    Dim sSingleRxStateCustiomizeReport As String = String.Empty
    Dim sMultipleRxStateCustiomizeReport As String = String.Empty
    Private Initializing As Boolean = False 'for avoiding check changed event on load
    ''Start OB Setting
    Dim dtAllMedCat As DataTable = Nothing
    Dim dtCmdMedCat As DataTable = Nothing
    ''Added ServicesDatabaseName by Ujwala on 20022015 to get ServicesDB Name from settings table instead of Hardcoding
    Dim strgloServiceDatabaseName As String = gstrServicesDBName
    ''Added ServicesDatabaseName by Ujwala on 20022015 to get ServicesDB Name from settings table instead of Hardcoding
    'Added EnableStaticColor Setting
    Dim _isEnableStaticColor As Boolean = False
    Dim tmpSelectedCCDAClinical As String = ""
    Dim tmpSelectedCCDAAmbulatory As String = ""
#Region "CCDA for Portal"
    Dim ClinicalCCDASection As String = ""
    Dim AmbulatoryCCDASection As String = ""

#End Region
    Dim CCDAImportCategory As String = ""
#Region "Occmed Portal"
    Private oListControl_DMS As gloListControl.gloListControl
#End Region
    Public Enum enumGrowthChartPercentile
        DontShowPercentile = 0
        ShowPercentile = 1
        ShowPercentileOnMouseHoover = 2
    End Enum
       
    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        cmbCountry.DrawMode = DrawMode.OwnerDrawFixed
        AddHandler cmbCountry.DrawItem, AddressOf ShowTooltipOnWriteOffComboBox

        cmbFutureApptType.DrawMode = DrawMode.OwnerDrawFixed
        AddHandler cmbFutureApptType.DrawItem, AddressOf ShowTooltipOnWriteOffComboBox

        cmbSameDayApptType.DrawMode = DrawMode.OwnerDrawFixed
        AddHandler cmbSameDayApptType.DrawItem, AddressOf ShowTooltipOnWriteOffComboBox
    End Sub
#Region "variables"
    Private _blnValidate As Boolean = False
    Dim optcnt As Integer = 0
    Dim blnadminflag As Boolean
    Dim dtsurgical As New DataTable
    Dim dtFollowupuser As New DataTable
    '******
    Dim colUsers As New Collection
    Dim colUId As New Collection


    Private blnProcessFlag As Boolean
    Private blnMultipleGuarantorsProcessFlag As Boolean


    Private blnSurgicalclick As Boolean 'this flag will keep the trak wether the btnAddFollowup suer is clicked or the btnAddSurgicalAlter user is clicked
    'so when we click the OK button in the C1userList grid that time we will check which of the above button was clicked so depending on that the cmbFollowUpuser or the cmbSurgicalAlertUser will be updated
    'therefore when btnAddFollowUpUser is clicked we make this flag = false and when btnAddSurgicalAlert user is clicked then we make this flag = true

    '******collections for Surgical Alert users
    Dim col_SurgicalUsers As New Collection
    Dim col_SurgicalUId As New Collection
    'Added by Mayuri:20090926
    'To check whether Surgical User button clicked or not
    'If User clicks surgical User button then Users gets added into AddSurgicalUsers combo box else into AddFollowUp user combobox
    Dim _IsSurgicalUser As Boolean = False


    Dim UserCount As Integer
    Private Col_Check As Integer = 0
    Private Col_UserID As Integer = 1
    Private Col_LoginName As Integer = 2
    Private Col_Column1 As Integer = 3
    Private Col_Column2 As Integer = 4
    Private Col_ProviderID As Integer = 5
    Private Col_count As Integer = 6
    '******


    'local variables




    ''' <summary>
    ''' Column Constants for Provider Setting Grid
    ''' </summary>
    ''' <remarks> Added By Anil 200907 For Provider Setting Grid</remarks>
    Private COL_PROVIDER_ID As Integer = 0
    Private COL_PROVIDERNAME As Integer = 1
    Private COL_SUBMITTER As Integer = 2
    Private COL_RENDERING As Integer = 3
    Private COL_BILLING As Integer = 4
    Private COL_COUNTS As Integer = 5




    Private m_numofCapletters As Integer = 0
    Private m_numofLetters As Integer = 0
    Private m_numofspecialchars As Integer = 0
    Private m_numminlength As Integer = 0
    Private m_numofdays As Integer = 0
    Private m_numofdigits As Integer = 0

    'sarika 31st aug 07
    ''''' next 2 arrays are used for any modification on form and Close button click 
    Dim _arrayGetData As ArrayList
    Dim _arraySetData As ArrayList
    Public bModifyData As Boolean = False

    '----------------

    'sarika 26th june 07
    '    Public _sqlstrsettings As String = ""
    Public sqlstrsettings As String = ""
    '---
    '******
    Public blnUserClick As Boolean
    'Private objclsMessage As New clsMessage

    Friend WithEvents pnlFollowUpUser As Windows.Forms.Panel
    '******
    Dim m_strSQL As String = ""
    Dim m_blnSetComplexisityOnSetting As Boolean = False
    '''' column for flexgrid

    ''Sudhir -- Columns for ProviderFlexGrid 20090107
    Private Const col_gloEMR_ProviderName = 0 ' gloEMR_ProviderName
    Private Const col_gloPM_ProviderName = 1 'gloPM_ProviderName
    Private Const col_ExternalID = 2 'ExternalID
    Private Const col_Provider_Count = 3

    Private IsProviderLoading As Boolean = False

    Public bEFaxSettingsModified As Boolean = False
    '----------------
    Dim _ClinicID As Int64 = 1
    Dim blnBtncloseclick As Boolean = False
    Dim blnCloseClicked As Boolean = False
    'Madan added for gloLab_hsi value ckh
    Private gloLab_hsi As String = ""
    Dim _gloLab_defaultID As Int64 = 1
    'Change For Resolving case no :GLO2010-0009760 i.e Lab Setting
    Dim _gloLab_DefaultUserID As Int64 = 0
    Dim _gloLab_defaultUserName As String = ""
    Dim _gloLab_settingsEdited As Boolean = False
    Private oListControl As gloListControl.gloListControl
    Private _CurrentControlType As gloListControl.gloListControlType = gloListControl.gloListControlType.Users
    '----------------
    'End Madan

    Dim _gloHxForecast_defaultUserID As Int64 = 0
    Dim _gloForecastReconcileDone_defaultUserID As Int64 = 0

    Dim _isAdvanceRxChecked As Boolean = False           ''Dhruv

    'Added by kanchan on 20100604 for CCD
    Dim _CCD_DefaultUserID As Int64 = 1
    Dim _CCD_UserName As String = ""
    Private oCCDUserList As gloListControl.gloListControl
    Private _CCDUserListType As gloListControl.gloListControlType = gloListControl.gloListControlType.Users

    ' Dim _blnClearAll As Boolean = False
    Dim _blnSelectAll As Boolean = False
    Dim bSelectAll_CC As Boolean = False
    Dim bOBVitalsSelectAll As Boolean = False

    Dim _SequentialPatientCode As Long
    ''Added by Mayuri:20101211-To maintain check-unchek status of followup and surgical users
    Dim gloItems As New gloGeneralItem.gloItems()
    Dim gloItem As gloGeneralItem.gloItem
    Dim ToList As New gloGeneralItem.gloItems()
    Dim ToItem As gloGeneralItem.gloItem

    ''Added by Abhijeet on 20111122 for Task user selection for Failed Inbound Lab result in HL7
    Dim _gloLabFailure_DefaultUserID As Int64 = 0  ''glolab failure default user
    Dim _gloLabFailure_defaultUserName As String = String.Empty
    Private oListControl_FailedLabTask As gloListControl.gloListControl
    ''End of changes by Abhijeet 20111122 for Task user selection for Failed Inbound Lab result in HL7

#End Region

#Region "properties"
    'Added by Ashish on 2nd March 2015 for Centralized Formulary 3.0 setting
    'Private Property UseFormularyService As Boolean = False
    Private Property FormularyServiceURL As String = String.Empty

    'properties
    Public Property NoofDigits() As Integer
        Get
            Return m_numofdigits
        End Get
        Set(ByVal Value As Integer)
            m_numofdigits = Value
        End Set
    End Property

    Public Property NoofCapitalLetters() As Integer
        Get
            Return m_numofCapletters
        End Get
        Set(ByVal Value As Integer)
            m_numofCapletters = Value
        End Set
    End Property

    Public Property NoofLetters() As Integer
        Get
            Return m_numofLetters
        End Get
        Set(ByVal Value As Integer)
            m_numofLetters = Value
        End Set
    End Property

    Public Property NumMinimumLength() As Integer
        Get
            Return m_numminlength
        End Get
        Set(ByVal Value As Integer)
            m_numminlength = Value
        End Set
    End Property

    Public Property NoofDays() As Integer
        Get
            Return m_numofdays
        End Get
        Set(ByVal Value As Integer)
            m_numofdays = Value
        End Set
    End Property

    Public Property NoOfSpecialChars() As Integer
        Get
            Return m_numofspecialchars
        End Get
        Set(ByVal Value As Integer)
            m_numofspecialchars = Value
        End Set
    End Property

    Public Property strSQL() As String
        Get
            Return m_strSQL
        End Get
        Set(ByVal Value As String)
            m_strSQL = Value
        End Set
    End Property

    Public Property IsAdvanceRxChecked() As String
        Get
            Return _isAdvanceRxChecked
        End Get
        Set(ByVal Value As String)
            _isAdvanceRxChecked = Value
        End Set
    End Property



#End Region
    'Added code to for setting server path only
    <DllImport("shlwapi.dll", CharSet:=CharSet.Unicode)> _
<ResourceExposure(ResourceScope.None)> _
    Friend Shared Function PathIsUNC(<MarshalAsAttribute(UnmanagedType.LPWStr), [In]()> pszPath As String) As <MarshalAsAttribute(UnmanagedType.Bool)> Boolean
    End Function

    Private Sub frmSettings_New_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed


        If (Not dtCmdMedCat Is Nothing) Then ''added for medical category risk functionality
            dtCmdMedCat.Dispose()
            dtCmdMedCat = Nothing
        End If
        If Not dtAllMedCat Is Nothing Then
            dtAllMedCat.Dispose()
            dtAllMedCat = Nothing
        End If
        cmbmedrsk.DataSource = Nothing
        C1MedicalCategory.DataSource = Nothing
        '08-Apr-13 Aniket: Disable Multiple Race if the setting is checked

        If IsNothing(dtsurgical) = False Then
            dtsurgical.Dispose()
            dtsurgical = Nothing
        End If

        If IsNothing(dtFollowupuser) = False Then
            dtFollowupuser.Dispose()
            dtFollowupuser = Nothing
        End If

        RemoveHandler chkMU2Features.CheckedChanged, AddressOf chkMU2Features_CheckedChanged

    End Sub

    Private Sub frmSettings_New_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If _isOkClick = True Then
            Exit Sub
        End If
        Try
            ''Sandip Darade  20100217
            ''Case GLO2010-0004202
            If (blnBtncloseclick = False) Then


                Dim oResult As DialogResult
                oResult = MessageBox.Show("Do you want to save settings?", gstrMessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
                If oResult = Windows.Forms.DialogResult.Yes Then
                    ''Added On 20100630 by sanjog
                    blnCloseClicked = True
                    If btnOk_Click_New() = False Then
                        'blnBtncloseclick = True
                        e.Cancel = True
                    Else

                    End If
                    blnCloseClicked = False
                    'If flagClose = True Then
                    '    flagClose = False
                    '    blnBtncloseclick = False
                    '    e.Cancel = True
                    'End If
                    ''Added On 20100630 by sanjog
                ElseIf oResult = Windows.Forms.DialogResult.Cancel Then
                    e.Cancel = True
                Else

                End If
            End If

            Try
                If _blnValidate = True Then
                    e.Cancel = True
                End If
            Catch ex As Exception
                Me.Cursor = Cursors.Default
                MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try


        Catch objErr As Exception
            Me.Cursor = Cursors.Default
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub frmSettings_New_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Leave

    End Sub
    Private Sub changeHeightAsPerResolution()
        Dim myScreenHeight As Int32 = Convert.ToInt32(Screen.PrimaryScreen.WorkingArea.Height * 0.99)
        If myScreenHeight < Me.Height Then
            Me.Height = myScreenHeight
            Dim myScreenWidth As Int32 = Convert.ToInt32(Screen.PrimaryScreen.WorkingArea.Width * 0.63)
            If myScreenWidth > Me.Width Then
                Me.Width = myScreenWidth
            End If
        End If

    End Sub


    Private Sub frmSettings_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load


        changeHeightAsPerResolution()

        pnlRxNorm.Visible = True
        btnMapping.Visible = False

        gloC1FlexStyle.Style(C1Provider)
        gloC1FlexStyle.Style(c1userList)
        gloC1FlexStyle.Style(C1surgicalUsers)
        gloC1FlexStyle.Style(c1Providers)
        ''OB Setting
        gloC1FlexStyle.Style(C1MedicalCategory)
        'trvVitals.ImageIndex = 0
        'trvVitals.SelectedImageIndex = 0
        'trvSelectedVitals.ImageIndex = 0
        'trvSelectedVitals.SelectedImageIndex = 0
        ''Sandip Darade 20100107 bug ID 5506

        tb_Settings.Controls.Remove(tbp_PMDBSettings)
        Try
            '01-Apr-14 Chetan: Resolving resolution issues for bugid 65031
            'Dim hgth As Integer = System.Windows.SystemParameters.PrimaryScreenHeight
            'Dim wdth As Integer = System.Windows.SystemParameters.PrimaryScreenWidth
            'Dim res As Integer = System.Windows.SystemParameters.FullPrimaryScreenHeight
            'If res < 800 Then
            '    Me.MaximumSize = New System.Drawing.Size(wdth, (hgth - 50))
            '    Me.Height = hgth - 50
            '    Me.AutoScroll = True
            'End If
            lnkDirectAdmin.Links.Add(0, lnkDirectAdmin.Text.Length, "https://admin.glostreamdirect.com/")
            lnkDirectSSO.Links.Add(0, lnkDirectSSO.Text.Length, "https://glostreamdirect.com/signup")

            'Added by Mitesh to disable VIS category
            'If GetVISDocument() = True Then
            cmbVISCategory.Enabled = False
            'Else
            'cmbVISCategory.Enabled = True
            'End If

            'Dim myScreenWidth As Integer = System.Windows.SystemParameters.PrimaryScreenWidth
            'Dim myScreenHeight As Integer = System.Windows.SystemParameters.PrimaryScreenHeight
            'If Me.Width > myScreenWidth Or Me.Height > myScreenHeight Then
            '    Me.MaximumSize = New System.Drawing.Size(myScreenWidth, (myScreenHeight - 50))
            '    Me.AutoScroll = True
            'End If

            Fill_SignatureFormat()

            'trvsnomed.Nodes.Add("History")
            trvsnomed.Nodes.Add("Immunization")
            ''trvsnomed.Nodes.Add("Problem")

            ''

            ''
            'tb_Settings.TabPages.Remove(tbp_PMDBSettings)
            optcnt = 0
            blnadminflag = GetAdminFlag()

            If blnadminflag = True Then
                pnlPwd.Visible = True
                lblPwdComplexity.Visible = True
                btnSetPwdComplexity.Visible = True
                '    txtNoOfAttempts.Visible = True
                numLockOutAttempts.Visible = True
                lblLockOutAttempts.Visible = True
            Else
                pnlPwd.Visible = False
                lblPwdComplexity.Visible = False
                btnSetPwdComplexity.Visible = False
                'txtNoOfAttempts.Visible = False
                numLockOutAttempts.Visible = False
                lblLockOutAttempts.Visible = False
            End If

            Me.Cursor = Cursors.WaitCursor
            Call Fill_FAXCompressions()
            Call Fill_FAXSpeakerVolume()
            SetICD10TransitionTab()

            'Commented by rahul patel on 29-10-2010
            'Call Fill_DMSCategories()

            'sarika 5th sept 07
            Call Fill_FaxUsers()
            '----------------------
            RemoveHandler cmb_InsuranceType.SelectedIndexChanged, AddressOf cmb_InsuranceType_SelectedIndexChanged
            ''Sandip Darade 20091107
            If (gstrAdminFor = "gloPM") Then
                FillInsuranceType()
                GetPaymentSetting()
            End If

            FillCountry()

            'start of code commented by manoj jadhav on 20111017 for New consolidated UI to device settings

            ' ''Added by Abhijeet on 20110407
            'txtAusUserName.Text = GetClinicInformation("sExternalcode")
            ' ''End of changes by Abhijeet on 20110407
            'end of code commented by manoj jadhav on 20111017 for New consolidated UI to device settings


            'Fill Appointment Settings for Future Appointment Type List & Same Appointment Type List
            FillFutureAppointmentTypeList()
            FillSameAppointmentTypeList()

            Dim objSettings As New clsSettings
            If objSettings.GetSettings() = True Then

                txtFormularyURL.Text = objSettings.FormularyServiceURL
                txtFormularyURL.Enabled = objSettings.ClinicFormularySettings
                txtDIBServiceURL.Text = objSettings.DIBServiceURL
                txtePAServiceURL.Text = objSettings.ePAServiceURL

                'AUTO COMPLETE TASKS ON ACKNOWLEDGEMENT
                ChkTasksAcknowledgement.Checked = objSettings.isAutoCompleteTaskAck

                'DMS AUTO COMPLETE TASKS ON ACKNOWLEDGEMENT
                ChkDMSTasksAcknowledgement.Checked = objSettings.isAutoCompleteDMSTaskAck

                'RCM AUTO COMPLETE TASKS ON ACKNOWLEDGEMENT
                ChkRCMTasksAcknowledgement.Checked = objSettings.isAutoCompleteRCMTaskAck

                'EnableStatusColor
                chkEnableStaticColor.Checked = objSettings.isEnableStatusColor

                'Enable CCDA service
                chkEnableCCDAService.Checked = objSettings.CCDADataExportServiceEnabled

                ''Enable MU2 Features Setting
                chkMU2Features.Checked = objSettings.globlnEnableMultipleRaceFeatures

                '04-Dec-14 Aniket: Exclude Non NDC Drugs From Erx Measure. Mail by Phill with subject ' prescriptions without an NCD code: OTC'S and Neutraceuticals'
                chkExcludeNonNDCDrugs.Checked = objSettings.ExcludeNonNDCDrugsFromErxMeasure
                ''
                '' -- Exclude Control Substance Drugs From Erx Measure
                chkExcludeControlSubstanceDrugs.Checked = objSettings.ExcludeControlSubstanceDrugsFromErxMeasure
                ''----X---

                chkEnableSpecializedRegistryReporting.Checked = objSettings.EnableSpecializedRegistryReporting

                ''Added by vijay on 6-Jan-2013  comunication preferances  for Reminder for Unscheduled Care
                ''If (chkMU2Features.Checked) Then
                fillCommunicationPrefence()
                Me.LoadDefaultCQM(objSettings.DefaultSelectedCMS)
                'Else
                '    lblMsgComPref.Visible = False
                '    cmbMsgCommPref.Visible = False
                '    lblLetterComPref.Visible = False
                '    cmbLettersCommPref.Visible = False
                '    lblSecureMsgComPref.Visible = False
                '    CmbSecureMsgComPref.Visible = False
                'End If
                txtInfoButtonURL.Text = objSettings.InfobuttonURL
                txtOpenInfobuttonURL.Text = objSettings.OpenInfobuttonURL
                'dtpICD10DOS.Value = objSettings.dtICD10StartDOS

                Dim oClsICD10Settings As New clsICD10Settings()
                Dim ICDDOS As String = oClsICD10Settings.FetchICDDOS()
                If ICDDOS <> "" Then
                    dtpICD10DOS.Value = CType(ICDDOS, Date)
                End If
                ICDDOS = Nothing
                oClsICD10Settings = Nothing

                If IsNothing(objSettings.nMessageComunicationPrefID) = False Then
                    If objSettings.nMessageComunicationPrefID > 0 Then
                        cmbMsgCommPref.SelectedValue = objSettings.nMessageComunicationPrefID
                    Else
                        cmbMsgCommPref.SelectedValue = -1
                    End If
                End If

                If IsNothing(objSettings.nLetterComunicationPrefID) = False Then
                    If objSettings.nLetterComunicationPrefID > 0 Then
                        cmbLettersCommPref.SelectedValue = objSettings.nLetterComunicationPrefID
                    Else
                        cmbLettersCommPref.SelectedValue = -1
                    End If
                End If

                If IsNothing(objSettings.nSecureMessageComunicationPrefID) = False Then
                    If objSettings.nSecureMessageComunicationPrefID > 0 Then
                        CmbSecureMsgComPref.SelectedValue = objSettings.nSecureMessageComunicationPrefID
                    Else
                        CmbSecureMsgComPref.SelectedValue = -1
                    End If
                End If
                '''END Added by vijay on 6-Jan-2013  comunication preferances  for Reminder for Unscheduled Care
                ''' 
                If gDmsServerName.Trim() <> "" And gDmsDatabaseName.Trim() <> "" Then
                    Call Fill_DMSCategories()
                End If

                If IsNothing(objSettings.AppointmentStartTime) = False Then
                    If objSettings.AppointmentStartTime = "#12:00:00 AM#" Then
                        tmStartTime.Value = CType(Now.Date & " 8:00:00", DateTime)
                    Else
                        tmStartTime.Value = Convert.ToDateTime(objSettings.AppointmentStartTime)
                    End If
                End If
                If IsNothing(objSettings.AppointmentEndTime) = False Then
                    If objSettings.AppointmentStartTime = "#12:00:00 AM#" Then
                        tmEndTime.Value = CType(Now.Date & " 17:00:00", DateTime)
                    Else
                        tmEndTime.Value = Convert.ToDateTime(objSettings.AppointmentEndTime)
                    End If
                End If

                ''Added Rahul for UnAuthenticationBanner on 20101020
                txtUnAuthLogBanner.Text = objSettings.UnAuthLogin
                ''End

                If IsNothing(objSettings.AppointmentInterval) = False Then
                    If objSettings.AppointmentInterval > 0 Then
                        AppointmentInterval.Value = objSettings.AppointmentInterval
                    Else
                        AppointmentInterval.Value = 5
                    End If
                End If

                'added by Amit B
                If objSettings.RestrictedTemplateAppointtment = 1 Then
                    chbox_restrictedTmpAptmnt.Checked = True
                Else
                    chbox_restrictedTmpAptmnt.Checked = False
                End If

                If objSettings.OverlapTemplateAppointment = "Y" Then
                    rdoOverlapYes.Checked = True
                End If

                If objSettings.OverlapTemplateAppointment = "N" Then
                    rdoOverlapNo.Checked = True
                End If

                If objSettings.OverlapTemplateAppointment = "U" Then
                    rdoOverlapUser.Checked = True
                End If

                If IsNothing(objSettings.PULLCHARTSInterval) = False Then
                    If objSettings.PULLCHARTSInterval > 0 Then
                        PullChartsInterval.Value = objSettings.PULLCHARTSInterval
                    Else
                        PullChartsInterval.Value = 5
                    End If
                End If
                If IsNothing(objSettings.MaxNoOfFAXRetries) = False Then
                    If objSettings.MaxNoOfFAXRetries >= 3 Then
                        numMaxNoOfRetries.Value = objSettings.MaxNoOfFAXRetries
                    Else
                        numMaxNoOfRetries.Value = 3
                    End If
                End If
                If IsNothing(objSettings.FAXCompression) = False Then
                    cmbFAXCompression.Text = objSettings.FAXCompression
                Else
                    cmbFAXCompression.Text = "CCITT G3"
                End If
                If IsNothing(objSettings.FAXSpeakerVoulme) = False Then
                    cmbSpeakerVolume.Text = objSettings.FAXSpeakerVoulme
                Else
                    cmbSpeakerVolume.Text = "No Volume"
                End If
                If IsNothing(objSettings.FAXRetryInterval) = False Then
                    If objSettings.FAXRetryInterval > 0 Then
                        numFAXRetryInterval.Value = objSettings.FAXRetryInterval
                    Else
                        numFAXRetryInterval.Value = 5
                    End If
                End If
                optFAXreceiveYes.Checked = objSettings.FAXReceiveEnabled
                optHPIYes.Checked = objSettings.HPIEnabled
                optLocationAddressedYes.Checked = objSettings.LocationAddressed
                '*************
                optPwdComplexYes.Checked = objSettings.blnPwdComplexity

                'sarika 14th june 07
                optClinicDIYes.Checked = objSettings.ClinicDISettings
                '---------------
                ''7022Items: Home Billing- Added to show previously saved settings saved in database.
                Try
                    RemoveHandler rbtn_UseAreaCodeForPatientYes.CheckedChanged, AddressOf rbtn_UseAreaCodeForPatientYes_CheckedChanged
                    rbtn_UseAreaCodeForPatientYes.Checked = objSettings.UseAreaCodeForPatient
                Catch ex As Exception

                Finally
                    AddHandler rbtn_UseAreaCodeForPatientYes.CheckedChanged, AddressOf rbtn_UseAreaCodeForPatientYes_CheckedChanged
                End Try
                ' ''For clinic formulary settings  commented
                ''optClinicFormularyYes.Checked = objSettings.ClinicFormularySettings

                ''If chkAdvanceRx.Checked = True Then
                ''    objSettings.ClinicFormularySettings = True
                ''Else
                ''    objSettings.ClinicFormularySettings = False
                ''End If

                'Code added by Rohit to retrieve Device Database setting on 20110514
                If Not IsNothing(objSettings.DeviceDatabaseName) Then
                    txtDeviceDataBaseName.Text = objSettings.DeviceDatabaseName 'objSettings.DeviceDatabaseName
                Else
                    txtDeviceDataBaseName.Text = String.Empty
                End If
                If Not IsNothing(objSettings.DeviceServerName) Then
                    txtDeviceServerName.Text = objSettings.DeviceServerName  'objSettings.DeviceServerName
                Else
                    txtDeviceServerName.Text = String.Empty
                End If

                'For Recover Exam Module
                If Not IsNothing(objSettings.RecoverExam) Then
                    chkRecoverExam.Checked = Convert.ToBoolean(objSettings.RecoverExam)   'objSettings.DeviceServerName
                Else
                    txtDeviceServerName.Text = False

                End If

                'start of code commented by manoj jadhav on 20111017 for New consolidated UI to device settings
                ''code added by RK on 201100528
                'If objSettings.SpirometryDevicePrefix <> String.Empty Then
                '    txtprefixForSpirometryDevice.Text = objSettings.SpirometryDevicePrefix
                '    sSpirometryDeviceOrderPrefix = objSettings.SpirometryDevicePrefix
                'Else
                '    txtprefixForSpirometryDevice.Text = "SPI"
                '    sSpirometryDeviceOrderPrefix = txtprefixForSpirometryDevice.Text
                'End If
                ''End of Code added by Rohit to retrieve Device Database setting on 20110514
                'end of code commented by manoj jadhav on 20111017 for New consolidated UI to device settings

                If Trim(objSettings.OMRCategoryHistory) <> "" Then
                    cmbOMRCategoryHistory.Text = objSettings.OMRCategoryHistory
                End If
                If Trim(objSettings.OMRCategoryROS) <> "" Then
                    cmbOMRCategoryROS.Text = objSettings.OMRCategoryROS
                End If
                If Trim(objSettings.OMRCategoryPatientRegistration) <> "" Then
                    cmbOMRCategoryPatientRegistration.Text = objSettings.OMRCategoryPatientRegistration
                End If
                If Trim(objSettings.OMRCategoryDirective) <> "" Then
                    cmbCategoryDirective.Text = objSettings.OMRCategoryDirective
                End If
                If Trim(objSettings.Labs) <> "" Then
                    cmbLabCategory.Text = objSettings.Labs
                End If

                If Trim(objSettings.Radiology) <> "" Then
                    cmbRadioCategory.Text = objSettings.Radiology
                End If
                chkEnableEpcs.Checked = objSettings.IsEpcsEnble
                chkPDMPAuto.Checked = objSettings.IsPDMPAuto
                Chkpdmpsavedms.Checked = objSettings.IsPDMPSendToDMS

                If objSettings.PDMPParticipantID <> "" Then
                    txtPDMPUserName.Text = objSettings.PDMPParticipantID
                Else
                    txtPDMPUserName.Text = ""
                End If

                '20090819 RxHUB rxPassword Settings 
                If objSettings.PDMP_password <> "" Then
                 
                    txtPDMPPassword.Text = Encoding.UTF8.GetString(Convert.FromBase64String(objSettings.PDMP_password))
                Else
                    txtPDMPPassword.Text = ""
                End If

                flagEpcsEnableOnLoad = objSettings.IsEpcsEnble
                chkAllowPrintForCS.Checked = objSettings.IsAllowPrintForCS
                'Developer: Mitesh Patel
                'Date:17-Jan-2012
                'PRD: Immunizations
                If Trim(objSettings.CategoryVIS) <> "" Then
                    cmbVISCategory.Text = objSettings.CategoryVIS
                End If

                'Developer: Mitesh Patel
                'Date:27-Jan-2012
                'PRD: signature format
                If Trim(objSettings.SignatureFormat) <> "" Then
                    cmbSignatureFormat.SelectedValue = objSettings.SignatureFormat
                End If


                ''for CCHIT11 
                If Trim(objSettings.RxMeds) <> "" Then
                    cmbRxMedsCategory.Text = objSettings.RxMeds
                End If
                'sarika 31st aug 07
                If Trim(objSettings.OMRCategoryFax) <> "" Then
                    cmbFaxCategory.Text = objSettings.OMRCategoryFax
                End If
                If Trim(objSettings.InboxAttachCategory) <> "" Then
                    cmbInboxCategory.Text = objSettings.InboxAttachCategory
                End If
                '-------------------

                If Not String.IsNullOrEmpty(objSettings.RCMtoDMSCategory) Then
                    cmbRCMtoDMSCategory.Text = objSettings.RCMtoDMSCategory
                End If

                If Not String.IsNullOrEmpty(objSettings.WelchAllynECGCategory) Then
                    cmbWelchAllynECGCategory.Text = objSettings.WelchAllynECGCategory
                End If

                ''//Code commented by Ravikiran on 14/02/2007
                '''' //Code added by Ravikiran on 13/02/2007 for RxReportPath settings
                'If gstrRxReportpath Is Nothing Then
                '    txtRxReportPath.Text = ""
                'Else
                '    txtRxReportPath.Text = gstrRxReportpath
                'End If

                '      txtNoOfAttempts.Text = objSettings.NoOfAttempts
                If (objSettings.NoOfAttempts < numLockOutAttempts.Minimum Or objSettings.NoOfAttempts > numLockOutAttempts.Maximum) Then
                    numLockOutAttempts.Value = numLockOutAttempts.Maximum
                Else
                    numLockOutAttempts.Value = objSettings.NoOfAttempts
                End If
                '   numLockOutAttempts.Value = objSettings.NoOfAttempts


                If IsNothing(objSettings.RecordLevelLocking) = False Then
                    chkRecordLocking.Checked = objSettings.RecordLevelLocking
                Else
                    'code added by dipak 20091021 to fix Bug 3982#Record Level Locking
                    objSettings.RecordLevelLocking = True
                    chkRecordLocking.Checked = True
                End If
                'code added by sagar to access the Threshold value on 31 july 2007
                If IsNothing(objSettings.ThresholdValue) = False Then
                    txtThresholdValue.Text = objSettings.ThresholdValue
                End If

                'Added by Mitesh
                If IsNothing(objSettings.RxEligibilitythresholdValue) = False Then
                    txtRxEligibilitythreshold.Text = objSettings.RxEligibilitythresholdValue
                End If

                'sarika 5th sept 07
                If IsNothing(objSettings.PendingFaxUserID) = False Then
                    cmbPendingFaxUser.Text = GetLoginName(objSettings.PendingFaxUserID)
                End If
                If IsNothing(objSettings.InboxAttacheUserID) = False Then
                    cmbInboxUser.Text = GetLoginName(objSettings.InboxAttacheUserID)
                End If
                If IsNothing(objSettings.RecieveFaxUserID) = False Then
                    cmbRecieveFaxUser.Text = GetLoginName(objSettings.RecieveFaxUserID)
                End If
                If IsNothing(objSettings.EnableLockField) = False Then
                    Chkenlckfield.Checked = objSettings.EnableLockField
                Else
                    Chkenlckfield.Checked = False
                End If

                If IsNothing(objSettings.SaveLiquidData) = False Then
                    ChkSavlqdata.Checked = objSettings.SaveLiquidData
                Else
                    ChkSavlqdata.Checked = False
                End If

                If IsNothing(objSettings.IsVitalRequired) = False Then
                    chkIsVitalRequired.Checked = objSettings.IsVitalRequired
                Else
                    chkIsVitalRequired.Checked = False
                End If

                If IsNothing(objSettings.IsICD9CPTRequired) = False Then
                    chkIsICD9CPTRequired.Checked = objSettings.IsICD9CPTRequired
                Else
                    chkIsICD9CPTRequired.Checked = False
                End If

                '-------------------------
                ''Added by Mayuri:20100608
                ''Added by Sanjog:20100609-Vital customization setting
                Dim SelectedVitals() As String
                If IsNothing(objSettings.VitalSettingsValue) = False Then
                    If objSettings.VitalSettingsValue <> "" Then

                        SelectedVitals = objSettings.VitalSettingsValue.Trim.Split(",")

                        ShowVital_InTrv(SelectedVitals)
                    Else
                        ShowVital_InTrv(SelectedVitals)
                    End If
                Else
                    ShowVital_InTrv(SelectedVitals)
                End If


                'Developer: Mitesh Patel
                'Date:28-Dec-2011'
                'PRD: OB Vitals Customization
                '------
                Dim sSelectedOBVitals() As String = Nothing
                If IsNothing(objSettings.OBVitalSettingsValue) = False Then
                    If objSettings.OBVitalSettingsValue <> "" Then

                        sSelectedOBVitals = objSettings.OBVitalSettingsValue.Trim.Split(",")
                        ShowOBVitals_InTrv(sSelectedOBVitals)
                    Else
                        ShowOBVitals_InTrv(sSelectedOBVitals)
                    End If
                Else
                    ShowOBVitals_InTrv(sSelectedOBVitals)
                End If
                '-----x--------------

                ''Added by Mitesh
                Dim _sClinicalchart() As String
                If IsNothing(objSettings.ClinicalChartSettingsValue) = False Then
                    If objSettings.ClinicalChartSettingsValue <> "" Then

                        _sClinicalchart = objSettings.ClinicalChartSettingsValue.Trim.Split(",")
                        ShowClinicalChart(_sClinicalchart)
                    Else
                        ShowClinicalChart(Nothing)
                    End If
                Else
                    ShowClinicalChart(Nothing)
                End If
                ''---xx---
                ''Added by Mitesh (fax exceeds & allow fax pages settings)
                If IsNothing(objSettings.FaxExceedsValue) = False Then
                    If objSettings.FaxExceedsValue <> "" Then
                        chk_FaxExceeds.Checked = True
                        txtFaxExceeds.Text = objSettings.FaxExceedsValue
                    Else
                        chk_FaxExceeds.Checked = False
                        txtFaxExceeds.Text = ""
                    End If
                Else
                    chk_FaxExceeds.Checked = False
                    txtFaxExceeds.Text = ""
                End If

                If IsNothing(objSettings.AllowFaxesValue) = False Then
                    If objSettings.AllowFaxesValue <> "" Then
                        chk_AllowFaxes.Checked = True
                        txtAllowFaxes.Text = objSettings.AllowFaxesValue
                    Else
                        chk_AllowFaxes.Checked = False
                        txtAllowFaxes.Text = ""
                    End If
                Else
                    chk_AllowFaxes.Checked = False
                    txtAllowFaxes.Text = ""
                End If
                ''---xx------------

                If IsNothing(objSettings.ClinicalSummaryFamilyHistory) = False Then

                    If objSettings.ClinicalSummaryFamilyHistory = clsSettings.ClinicalSummaryHistory.Current Then
                        rb_ClinicalSummFamHst_CurrentHistory.Checked = True
                        rb_ClinicalSummFamHst_VisitSpecific.Checked = False
                    ElseIf objSettings.ClinicalSummaryFamilyHistory = clsSettings.ClinicalSummaryHistory.VisitSpecific Then
                        rb_ClinicalSummFamHst_VisitSpecific.Checked = True
                        rb_ClinicalSummFamHst_CurrentHistory.Checked = False
                    End If

                End If

                If IsNothing(objSettings.ClinicalSummarySocialHistory) = False Then

                    If objSettings.ClinicalSummarySocialHistory = clsSettings.ClinicalSummaryHistory.Current Then
                        rb_ClinicalSummSocialHst_CurrentHistory.Checked = True
                        rb_ClinicalSummSocialHst_VisitSpecific.Checked = False
                    ElseIf objSettings.ClinicalSummarySocialHistory = clsSettings.ClinicalSummaryHistory.VisitSpecific Then
                        rb_ClinicalSummSocialHst_VisitSpecific.Checked = True
                        rb_ClinicalSummSocialHst_CurrentHistory.Checked = False
                    End If

                End If

                If IsNothing(objSettings.ClinicalSummaryProcedure) = False Then
                    If objSettings.ClinicalSummaryProcedure = clsSettings.ClinicalSummaryHistory.Current Then
                        chkVisitProcedure.Checked = False
                    Else
                        chkVisitProcedure.Checked = True
                    End If
                End If


                If IsNothing(objSettings.ClinicalSummaryVital) = False Then
                    If objSettings.ClinicalSummaryVital = clsSettings.ClinicalSummaryHistory.Current Then
                        chkVisitVital.Checked = False
                    Else
                        chkVisitVital.Checked = True
                    End If
                End If



            End If
            Dim SelectedFullCCD() As String
            Dim SelectedVisitCCD() As String

            ''"START CCDA for Portal"
            Dim SelectedCCDAClinical() As String
            Dim SelectedCCDAAmbulatory() As String
           
            ''"END CCDA for Portal"
            Dim SelectedImportCCDACategory() As String

            If IsNothing(objSettings.FullCCDSettingsValue) = False Then
                If objSettings.FullCCDSettingsValue <> "" Then
                    'If objSettings.FullCCDSettingsValue = "Fullvitals,FullFamHis,FullAdDir,FullLabs,FullImmu,FullProc,FullMed,Fullencounter,FullSocHis,FullAllergy,FullProb" Then
                    '    'ChkFullCCD.Checked = True
                    'Else

                    SelectedFullCCD = objSettings.FullCCDSettingsValue.Trim.Split(",")
                    checkFullCCDSections(SelectedFullCCD, "FullCCD")
                    'End If
                Else
                    'ChkAllergy.Checked = True
                    'ChkMedications.Checked = True
                    'chkProblems.Checked = True
                    'ChkResults.Checked = True
                End If
            Else
                ChkAllergy.Checked = True
                ChkMedications.Checked = True
                chkProblems.Checked = True
                ChkResults.Checked = True
            End If
            If IsNothing(objSettings.visitCCDSettings) = False Then
                If objSettings.visitCCDSettings <> "" Then

                    'If objSettings.visitCCDSettings = "VisitVitals,VisitFamHis,VisitAdDir,VisitLabs,visitImmu,VisitProc,VisitMed,VisitEncounter,VisitSocHis,VisitAllegy,VisitProb" Then
                    '    '  ChkVisitCCD.Checked = True
                    'Else
                    SelectedVisitCCD = objSettings.visitCCDSettings.Trim.Split(",")
                    checkFullCCDSections(SelectedVisitCCD, "VisitCCD")
                    'End If
                Else
                    'ChkAllergyVisit.Checked = True
                    'ChkMedicationVisit.Checked = True
                    'ChkProblemVisit.Checked = True
                    'ChkLabsVisit.Checked = True
                End If
            Else
                ChkAllergyVisit.Checked = True
                ChkMedicationVisit.Checked = True
                ChkProblemVisit.Checked = True
                ChkLabsVisit.Checked = True
            End If
            'If IsNothing(objSettings.MUSectionsValue) = False Then
            '    If objSettings.visitCCDSettings <> "" Then

            '        chkMUSections.Checked = objSettings.MUSectionsValue
            '    Else
            '        chkMUSections.Checked = False
            '    End If

            'End If
            If IsNothing(objSettings.MUSectionsValue) = False Then
                If objSettings.MUSectionsValue <> "" Then
                    If objSettings.MUSectionsValue = False Then
                        chkMUSections.Checked = objSettings.MUSectionsValue
                    Else
                        chkMUSections.Checked = True
                    End If
                End If
            End If

            'Added for CCDA AUTO DELETE FILES
            If IsNothing(objSettings.CCDAAutoDelete) = False Then
                If objSettings.CCDAAutoDelete = False Then
                    chkCCDAAutoDelete.Checked = objSettings.CCDAAutoDelete
                Else
                    chkCCDAAutoDelete.Checked = True
                End If
            End If

            ''"START CCDA for Portal"
            If IsNothing(objSettings.clinicalCCDASettings) = False Then
                If objSettings.clinicalCCDASettings <> "" Then
                    SelectedCCDAClinical = objSettings.clinicalCCDASettings.Trim.Split(",")
                    tmpSelectedCCDAClinical = objSettings.clinicalCCDASettings.Trim
                    checkCCDASections(SelectedCCDAClinical, "ClinicalCCDA")
                    chkCOCareTeamMem.Checked = True

                End If
            Else
                ''
            End If
            If IsNothing(objSettings.ambulatoryCCDASettings) = False Then
                If objSettings.ambulatoryCCDASettings <> "" Then
                    SelectedCCDAAmbulatory = objSettings.ambulatoryCCDASettings.Trim.Split(",")
                    tmpSelectedCCDAAmbulatory = objSettings.ambulatoryCCDASettings.Trim
                    '  checkCCDASections(SelectedCCDAAmbulatory, "AmbulatoryCCDA")
                    chkCOCareTeamMem.Checked = True

                End If
            Else
                ''
            End If
            ''"END CCDA for Portal"

            If IsNothing(objSettings.CCDAImportCategorySettings) = False Then
                If objSettings.CCDAImportCategorySettings <> "" Then
                    ''SelectedImportCCDACategory =
                    SetCCDAImportCategory(objSettings.CCDAImportCategorySettings)

                End If
            End If

            If IsNothing(objSettings.CCDAViewerURl) = False Then
                txtCDAViewer.Text = objSettings.CCDAViewerURl
            End If
            If optPwdComplexNo.Checked = True Then
                btnSetPwdComplexity.Visible = False
            End If

            'start of code commented by manoj jadhav on 20120730 for moving service setiings to Respective Service
            ''sarika 11th aug 07
            'txtHL7FilePath.Text = objSettings.HL7SystemPath
            ''******

            'txtSendFacility.Text = objSettings.HL7SendingFacility
            'txtRecAppl.Text = objSettings.HL7ReceivingApplication
            'txtRecFacility.Text = objSettings.HL7ReceivingFacility

            ''******
            ''-------------------
            'end of code commented by manoj jadhav on 20120730 for moving service setiings to Respective Service

            'sarika 31st aug 07
            If Trim(objSettings.DBVersion) <> "" Then
                txtDBVersion.Text = objSettings.DBVersion
            End If
            If Trim(objSettings.AppVersion) <> "" Then
                txtAppVersion.Text = objSettings.AppVersion
                'Added  by Rahul Patel on 28-09-2010
                'resolving issuse menntion by dan
                txtDBVersion.Text = objSettings.AppVersion
            End If
            '-------------

            '' ADDED On 20091012 to show gloAdmin Version
            If Trim(objSettings.gloAdminVersion) <> "" Then
                txtgloEMRAdminVersion.Text = objSettings.gloAdminVersion
            End If
            ''

            'sarika UseFaxNoPrefix 12th april 08
            chkUseFaxNoPrefix.Checked = objSettings.UseFaxNoPrefix
            txtFaxNoPrefix.Text = objSettings.FaxNoPrefix
            '-----sarika UseFaxNoPrefix 12th april 08

            'sarika SendEMail 20090502
            'chkSendMail.Checked = objSettings.IsSendEMail
            'If chkSendMail.Checked = True Then
            '    txtSendMail.Visible = True
            '    lblEnterEmailAddress.Visible = True
            '    txtSendMail.Text = objSettings.SendEmailAddress
            'Else
            '    txtSendMail.Visible = False
            '    lblEnterEmailAddress.Visible = False
            'End If

            '---

            ''AllowRefillCancel
            If objSettings.AllowRefillcancel = False Then
                chk_AllowRefillCancel.Checked = objSettings.AllowRefillcancel
            Else
                chk_AllowRefillCancel.Checked = True
            End If
            '-----


            ''Individual auto eRx Eligibility ON/Off
            If objSettings.AutoEligibility = False Then
                chkAutoEligibilityONorOFF.Checked = objSettings.AutoEligibility
            Else
                chkAutoEligibilityONorOFF.Checked = True
            End If
            '-----




            ''''''''''''''''''Code Added by Anil on 20071119
            If objSettings.AutoGeneratePatientCode = False Then
                chkAutogenerateCode.Checked = objSettings.AutoGeneratePatientCode
            Else
                chkAutogenerateCode.Checked = True
            End If

            ''''''Added by Mitesh on 20120914
            If objSettings.IncludeFrequencyAbbrevationInRxMeds = False Then
                chk_IncludeFrequencyAbbrevationInRxMeds.Checked = objSettings.IncludeFrequencyAbbrevationInRxMeds
            Else
                chk_IncludeFrequencyAbbrevationInRxMeds.Checked = True
            End If
            '-----

            If IsNothing(objSettings.LoadProblemonMeds) = False Then
                ChkLoadProbOnMed.Checked = objSettings.LoadProblemonMeds
            Else
                ChkLoadProbOnMed.Checked = False
            End If

            'Display ICD codes on Medication
            If IsNothing(objSettings.LoadProblemDxCodeonMeds) = False Then
                ChkLoadProbDxCodeOnMed.Checked = objSettings.LoadProblemDxCodeonMeds
            Else
                ChkLoadProbDxCodeOnMed.Checked = False
            End If


            ''Integrated by Mayuri:20101022
            ''Added by Mayuri:20101004-To add functonality of save as copy patient
            If objSettings.AllowEditablePatientCode = False Then
                chkallowEditPatientCode.Checked = objSettings.AllowEditablePatientCode
            Else
                chkallowEditPatientCode.Checked = True
            End If
            If objSettings.ShowDMAlert = False Then
                ChkShowDMAlert.Checked = objSettings.ShowDMAlert
            Else
                ChkShowDMAlert.Checked = True
            End If
            ''End 20101022

            'added by Mitesh
            If objSettings.EnableIntuitFeature = False Then
                chkEnableIntuit.Checked = objSettings.EnableIntuitFeature
            Else
                chkEnableIntuit.Checked = True
            End If

            If objSettings.IncludeOriginalMessage = False Then
                chkIncludeOrignalMessage.Checked = objSettings.IncludeOriginalMessage
            Else
                chkIncludeOrignalMessage.Checked = True
            End If

            TxtPracticeID.Text = objSettings.IntuitPracticeID

            ''-------x---
            ''''''''''''''''''Code Added by Pradeep on 28/06/2010
            If objSettings.UseSitePrefix = True Then
                ChkUseSitePrefix.Checked = objSettings.UseSitePrefix
                flagPrefixSettingON = True
                flagPrefixSettingOFF = True
            Else
                ChkUseSitePrefix.Checked = False
                flagPrefixSettingON = False
                flagPrefixSettingOFF = False
            End If

            If objSettings.EnableSingleSignon = True Then
                chkEnableSingleSignON.Checked = objSettings.EnableSingleSignon
            Else
                chkEnableSingleSignON.Checked = False
            End If

            ''''''''''''''''''''''''''''''''''''
            ''code added by pradeep to suppress task on status change on 02/01/20111
            If objSettings.EnableTasksforPatientStatusChange = True Then
                ChkEnableTasksforPatientStatusChange.Checked = True
            Else
                ChkEnableTasksforPatientStatusChange.Checked = False
            End If
            Try

                Dim oResult As Object
                Dim ogloSettings As gloSettings.GeneralSettings = New gloSettings.GeneralSettings(mdlGeneral.GetConnectionString)
                ogloSettings.GetSetting("SequentialPatientCode", oResult)

                If (oResult <> Nothing And oResult.ToString() <> "") Then
                    _SequentialPatientCode = Convert.ToInt64(oResult.ToString.Trim.ToString())
                    txtSequentialPatientCode.Text = _SequentialPatientCode
                End If

            Catch ex As Exception

            End Try
            ''''code added by pradeep on 12/07/2010 for auto accept task setting
            If objSettings.ExplicitlyAcceptTask = 1 Then
                ChkExplicitlyAcceptTask.Checked = True
            Else
                ChkExplicitlyAcceptTask.Checked = False
            End If
            ''''''''''''''''''''''''''''''''''''''''''''''''
            'code added by suraj on 20080725'''******* for Rxdeclaration value
            If objSettings.RxDeclaration <> "" Then
                txtDeclarartion.Text = objSettings.RxDeclaration
            End If


            '------>Suraj 20080801

            If objSettings.RxHUBDisclaimer <> "" Then
                txtRxHubDisclaimer.Text = objSettings.RxHUBDisclaimer
            End If

            chbImunizationReport.Checked = objSettings.GenerateMic

            'Added by Amit - 7010 to control Vaccine inventory
            chkTrackVaccineInventory.Checked = objSettings.TrackVaccineInventory

            'Added by Amit - 7020 Immunization
            chkIMRequireFunding.Checked = objSettings.RequireFunding

            'Added by Amit - 7020 Enable Copy Exam
            chkEnableCopyExam.Checked = objSettings.EnableCopyExam
            '27-Jan-15 Aniket: Addition of new setting to Show SmartDX screen on SmartDX save
            chkShowDxCPTScreenOnSmartDX.Checked = objSettings.ShowDxCPTScreenOnSmartDX

            cbIsExamPTBillingEnabled.Checked = objSettings.IsExamPTBillingEnabled

            ''Start OB Setting
            chkOBSpeciality.Checked = objSettings.IsOBSpeciality

            If IsNothing(objSettings.AutoCaseCloseDays) = False Then
                txtAutoCaseCloseDays.Text = objSettings.AutoCaseCloseDays
            End If
            ''End OB Setting

            'Added by Amit - 7030 MU Stage 1 2013 changes'
            'CPOE
            If objSettings.CPOE_MU1_Change = "False" Then
                rdoCPOEMU1Current.Checked = True
                rdoCPOEMU1New.Checked = False
            Else
                rdoCPOEMU1Current.Checked = False
                rdoCPOEMU1New.Checked = True
            End If

            'Vital
            If objSettings.Vital_MU1_Change = "False" Then
                rdoVitalMU1Current.Checked = True
                rdoVitalMU1New.Checked = False
                rdoVitalAllRequired.Checked = False
                rdoVitalBPRequired.Checked = False
                rdoVitalHeightWeightRequired.Checked = False
            ElseIf objSettings.Vital_MU1_Change = "True" Then
                rdoVitalMU1New.Checked = True
                If objSettings.VitalAllRequired = "True" Then
                    rdoVitalAllRequired.Checked = True
                    rdoVitalBPRequired.Checked = False
                    rdoVitalHeightWeightRequired.Checked = False
                End If
                If objSettings.VitalBPRequired = "True" Then
                    rdoVitalAllRequired.Checked = False
                    rdoVitalBPRequired.Checked = True
                    rdoVitalHeightWeightRequired.Checked = False
                End If
                If objSettings.VitalHeightWeightRequired = "True" Then
                    rdoVitalAllRequired.Checked = False
                    rdoVitalBPRequired.Checked = False
                    rdoVitalHeightWeightRequired.Checked = True
                End If
            End If

            '23-Sep-13 Aniket: Vital Stage 2 Changes

            If objSettings.VitalAllRequired_Stage2 = "True" Then
                rdoVitalAllRequiredStage2.Checked = True
            Else
                rdoVitalAllRequiredStage2.Checked = False
            End If

            If objSettings.VitalBPRequired_Stage2 = "True" Then
                rdoVitalBPRequiredStage2.Checked = True
            Else
                rdoVitalBPRequiredStage2.Checked = False
            End If

            If objSettings.VitalHeightWeightRequired_Stage2 = "True" Then
                rdoVitalHeightWeightRequiredStage2.Checked = True
            Else
                rdoVitalHeightWeightRequiredStage2.Checked = False
            End If

            '23-Sep-13 Aniket: Vital Stage 2 Changes

            'ELECTRONIC PRESCRIPTION MU 2013'
            If objSettings.eRx_MU1_Change = "False" Then
                rdoeRxReqd.Checked = True
                rdoeRxNotReqd.Checked = False
            Else
                rdoeRxReqd.Checked = False
                rdoeRxNotReqd.Checked = True
            End If

            'REPORT CLINICAL QM MU 2013'
            If objSettings.eRptClinicalQua_MU1_Change = "False" Then
                rdoeRptClinicalQuaReqd.Checked = True
                rdoeRptClinicalQuaNotReqd.Checked = False
            Else
                rdoeRptClinicalQuaReqd.Checked = False
                rdoeRptClinicalQuaNotReqd.Checked = True
            End If

            'ELECTRONIC COPY OF PAT. INFO. MU 2013' 
            If objSettings.eCopyPatHealthInfo_MU1_Change = "False" Then
                rdoeCopyPatHealthInfoReqd.Checked = True
                rdoeCopyPatHealthInfoNotReqd.Checked = False
            Else
                rdoeCopyPatHealthInfoReqd.Checked = False
                rdoeCopyPatHealthInfoNotReqd.Checked = True
            End If

            'ELECTRONICALLY EXCH CLINICAL INFO MU 2013'
            If objSettings.eExchangeClinInfor_MU1_Change = "False" Then
                rdoeExchangeClinInforReqd.Checked = True
                rdoeExchangeClinInforNotReqd.Checked = False
            Else
                rdoeExchangeClinInforReqd.Checked = False
                rdoeExchangeClinInforNotReqd.Checked = True
            End If


            'code added by sarika to fill gloReporting authentication settings
            'Report Username
            If objSettings.ReportingUserName <> "" Then
                txtRptUserName.Text = objSettings.ReportingUserName
            Else
                txtRptUserName.Text = ""
            End If

            'report Password
            If objSettings.ReportingPassword <> "" Then
                'the pwd is stored in encrypted format
                'decrypt the password and display it
                Dim objEncrypt As New clsEncryption

                txtRptPassword.Text = objEncrypt.DecryptFromBase64String(objSettings.ReportingPassword, constEncryptDecryptKey_Services)
            Else
                txtRptPassword.Text = ""
            End If

            'end of code added by sarika to fill gloReporting authentication settings

            'sarika internet fax
            If objSettings.InternetFax = False Then
                chkInternetFax.Checked = False
            Else
                chkInternetFax.Checked = True
            End If

            'eFax Login Key
            'If objSettings.eFaxUserID <> "" Then
            '    txteFaxUserID.Text = objSettings.eFaxUserID
            'Else
            '    txteFaxUserID.Text = ""
            'End If


            'If objSettings.eFaxUserPassword <> "" Then
            '    'the pwd is stored in encrypted format
            '    'decrypt the password and display it
            '    Dim objEncrypt As New clsEncryption

            '    txteFaxPassword.Text = objEncrypt.DecryptFromBase64String(objSettings.eFaxUserPassword, constEncryptDecryptKey)
            'Else
            '    txteFaxPassword.Text = ""
            'End If

            'sarika internet fax

            'code added by supriya 11/7/2008 Surescript Server

            chkSurescript.Checked = objSettings.IsSurescriptEnabled
            'To Show Staging Web service Url
            sStagingURl = objSettings.SurescriptUrlStaging
            'To Show Production Web service Url
            sProductionURl = objSettings.SurescriptUrlProduction
            If chkSurescript.Checked = True Then
                pnlSurescriptServer.Enabled = True
                If objSettings.IsStagingServer = True Then
                    rbStaging.Checked = True
                    ' TxtSurescriptURL.Text = objSettings.SurescriptUrlStaging

                Else
                    rbProduction.Checked = True
                    ' TxtSurescriptURL.Text = objSettings.SurescriptUrlProduction

                End If

                ''Discussion with pravin sir:: surescript true = adv rx enable 
                chkAdvanceRx.Enabled = True

                TxtSurescriptURL.Enabled = True
            Else
                pnlSurescriptServer.Enabled = False
                If objSettings.IsStagingServer = True Then
                    rbStaging.Checked = True
                Else
                    rbProduction.Checked = True
                End If

                ''Discussion with pravin sir:: surescript false = adv rx enable false  
                chkAdvanceRx.Enabled = False
                chkFormularyEnable.Enabled = False
                pnlFormularySetting.Enabled = False
                TxtSurescriptURL.Enabled = False
            End If
            'code added by supriya 11/7/2008 Surescript Server


            'Code added by Pramod 03/14/2013 Secure Messaging server settings Starts
            chkSecureMesaage.Checked = objSettings.IsSecureMessageEnabled
            sSecureMsgStaging = objSettings.SecuremsgUrlStaging
            sSecureMsgProduction = objSettings.SecuremsgUrlProduction
            chkPatientSavings.Checked = objSettings.IsPatientSavingMessageEnabled
            ChkEnableXDMMessage.Checked = objSettings.IsXDMSecureMessageEnable
            'Added by Ashish on 27-Nov-2013 for enabling Patient Savings Inbox
            chkPatientSavingInbox.Checked = objSettings.IsPatientSavingInboxEnabled

            If chkSecureMesaage.Checked = True Then
                rbSecureStaging.Enabled = True
                rbSecureProduction.Enabled = True
                txtSecureMessageURL.Enabled = True
            Else
                rbSecureStaging.Enabled = False
                rbSecureProduction.Enabled = False
                txtSecureMessageURL.Enabled = False
                chkPatientSavings.Checked = False
                chkPatientSavings.Enabled = False
            End If

            If Not chkPatientSavings.Checked Then
                chkPatientSavingInbox.Checked = False
                chkPatientSavingInbox.Enabled = False
            End If

            If chkSecureMesaage.Checked = True Then
                If objSettings.IsSecureStagingServer = True Then
                    rbSecureStaging.Checked = True
                    txtSecureMessageURL.Text = objSettings.SecuremsgUrlStaging
                Else
                    rbSecureProduction.Checked = True
                    txtSecureMessageURL.Text = objSettings.SecuremsgUrlProduction
                End If
            Else
                rbSecureProduction.Checked = True
                txtSecureMessageURL.Text = objSettings.SecuremsgUrlProduction
            End If


            gblnIsSecureMsgEnabled = objSettings.IsSecureMessageEnabled
            gblnIsSecureMsgStagingServer = objSettings.IsSecureStagingServer



            'Code added by Pramod 03/14/2013 Secure Messaging server settings Starts

            'code added to implement NCPDP 10.6
            Is8dot1PendingRefReqComplete = objSettings.Is8dot1PendingRefReqComplete
            TempIs8dot1PendingRefReqComplete = objSettings.Is8dot1PendingRefReqComplete
            RemoveHandler chkNCPDPVer10dot6.CheckedChanged, AddressOf chkNCPDPVer10dot6_CheckedChanged
            chkNCPDPVer10dot6.Checked = objSettings.IsNCPDP10dot6Ver
            AddHandler chkNCPDPVer10dot6.CheckedChanged, AddressOf chkNCPDPVer10dot6_CheckedChanged

            If objSettings.Is8dot1PendingRefReqComplete = True Then
                chkNCPDPVer10dot6.Enabled = False
            End If
            s10dot6StagingURl = objSettings.Surescript10dot6UrlStaging
            s10dot6ProductionURl = objSettings.Surescript10dot6UrlProduction
            If chkNCPDPVer10dot6.Checked = True Then
                If rbStaging.Checked = True = True Then
                    TxtSurescriptURL.Text = s10dot6StagingURl
                ElseIf rbProduction.Checked = True Then
                    TxtSurescriptURL.Text = s10dot6ProductionURl
                End If
            End If

            ''DI Service URL
            txtDIServiceURL.Text = objSettings.DIServiceURL
            '' --- Service URL

            ''new one medhx10.6 histroy url
            'If chkNCPDPVer10dot6.Checked = True Then
            '    RemoveHandler chkMedHistory.CheckedChanged, AddressOf chkMedHistory_CheckedChanged
            '    chkMedHistory.Checked = objSettings.IsMedHistory10Dot6Enable
            '    AddHandler chkMedHistory.CheckedChanged, AddressOf chkMedHistory_CheckedChanged

            '    If chkMedHistory.Checked = True Then
            '        txtMedHistoryPortalURL.Text = objSettings.MedHistoryPortalURL
            '    Else
            '        txtMedHistoryPortalURL.Text = objSettings.MedHistoryPortalURL
            '        txtMedHistoryPortalURL.Enabled = False
            '    End If
            'Else
            '    chkMedHistory.Checked = objSettings.IsMedHistory10Dot6Enable
            '    txtMedHistoryPortalURL.Text = objSettings.MedHistoryPortalURL
            '    chkMedHistory.Enabled = False
            '    txtMedHistoryPortalURL.Enabled = False
            'End If

            ''Medhx10.6 histroy url
            sMedHxStagingURl = objSettings.MedhxSurescriptUrlStaging
            sMedHxProductionURl = objSettings.MedhxSurescriptUrlProduction

            Dim sTempMedhxURL As String = String.Empty
            If rbStaging.Checked = True Then
                sTempMedhxURL = objSettings.MedhxSurescriptUrlStaging
            Else
                sTempMedhxURL = objSettings.MedhxSurescriptUrlProduction
            End If
            If chkNCPDPVer10dot6.Checked = True Then
                RemoveHandler chkMedHistory.CheckedChanged, AddressOf chkMedHistory_CheckedChanged
                chkMedHistory.Checked = objSettings.IsMedHistory10Dot6Enable
                nmMedHxRestriction.Enabled = chkMedHistory.Checked
                AddHandler chkMedHistory.CheckedChanged, AddressOf chkMedHistory_CheckedChanged

                If chkMedHistory.Checked = True Then
                    txtMedHistoryPortalURL.Text = sTempMedhxURL
                Else
                    txtMedHistoryPortalURL.Text = sTempMedhxURL
                    txtMedHistoryPortalURL.Enabled = False
                End If
            Else
                chkMedHistory.Checked = objSettings.IsMedHistory10Dot6Enable
                txtMedHistoryPortalURL.Text = sTempMedhxURL
                chkMedHistory.Enabled = False
                txtMedHistoryPortalURL.Enabled = False
            End If
            'End Medhx

            'sarika RxHUB Server Settings 20090602
            chkAdvanceRx.Checked = objSettings.IsAdvanceRxEnabled
            chkFormularyEnable.Checked = objSettings.ClinicFormularySettings
            If chkAdvanceRx.Checked = True Then
                pnlAdvanceRxServer.Enabled = True
                ''objSettings.ClinicFormularySettings = True

                If objSettings.IsAdvanceRxStagingServer = True Then
                    rbAdvRxStaging.Checked = True
                Else
                    rbAdvRxProduction.Checked = True
                End If
                '\\commented on 20090820: txtEARDirectory.Text = objSettings.EARDirectory
            Else
                pnlAdvanceRxServer.Enabled = False
                ''objSettings.ClinicFormularySettings = False
                If objSettings.IsAdvanceRxStagingServer = True Then
                    rbAdvRxStaging.Checked = True
                Else
                    rbAdvRxProduction.Checked = True
                End If
            End If


            If chkFormularyEnable.Checked = True Then



                rdbSQL.Checked = True
                If chkSurescript.Checked = True Then
                    pnlFormularySetting.Enabled = True
                Else
                    pnlFormularySetting.Enabled = False
                End If

                txtFormularyServerName.Text = gstrFormularySQLServer
                txtFormularyUserId.Text = gstrFormularyUserID
                '' GLO2011-0015850 6052 gloEMR Admin Defect - Formulary database
                '' Database Name Changed
                txtFormularyDataBaseName.Text = gstrFormalayrDatabase
                ' gloFormulary" ''Changed the Default database name instead of  "FormularyDB" ''GLO2011-0015850 6052 gloEMR Admin Defect - Formulary database 
                txtFormularyUserId.Text = gstrFormularyUserID
                txtFormularyPassword.Text = gstrFormularyPassword

            Else

                'txtFormularyServerName.Text = ""
                'txtFormularyDataBaseName.Text = ""
                'txtFormularyUserId.Text = ""
                'txtFormularyPassword.Text = ""



                pnlFormularySetting.Enabled = False


            End If
            '----


            '20090819 RxHUB participantID Settings 
            If objSettings.ParticipantID <> "" Then
                txtParticipantID.Text = objSettings.ParticipantID
            Else
                txtParticipantID.Text = ""
            End If

            '20090819 RxHUB rxPassword Settings 
            If objSettings.RxPassword <> "" Then
                Dim objEncrypt As New clsEncryption

                txtRxPswd.Text = objEncrypt.DecryptFromBase64String(objSettings.RxPassword, constEncryptDecryptKey)
            Else
                txtRxPswd.Text = ""
            End If





            'sarika internet fax'
            If objSettings.eFaxDirDownload <> "" Then
                txteFaxDownloadDir.Text = objSettings.eFaxDirDownload
            Else
                txteFaxDownloadDir.Text = ""
            End If
            'sarika internet fax'


            'sarika SiteID Setting 20090607

            'sarika SiteID Setting 20090607
            txtSiteID.Text = objSettings.SiteID
            '----
            '---

            '' 20080802 Set MCIR Report Path 
            If objSettings.MCIRReportPath <> "" Then
                txtMCIRReportPath.Text = objSettings.MCIRReportPath
            Else
                txtMCIRReportPath.Text = ""
            End If
            ''
            If objSettings.ClinicalDocfilePath <> "" Then
                txtClinicalDocumentsExportPath.Text = objSettings.ClinicalDocfilePath
            Else
                txtClinicalDocumentsExportPath.Text = ""
            End If

            'to save the CCD file path
            If objSettings.CCDfilePath <> "" Then
                txtCCDFilePath.Text = objSettings.CCDfilePath
            Else
                txtCCDFilePath.Text = ""
            End If
            If objSettings.StylesheetfilePath <> "" Then
                txtStyleSheetPath.Text = objSettings.StylesheetfilePath
            Else
                txtStyleSheetPath.Text = ""
            End If
            If objSettings.CDAValidatorUrl <> "" Then
                txtCDAValidatorUrl.Text = objSettings.CDAValidatorUrl
            Else
                txtCDAValidatorUrl.Text = ""
            End If

            '' Added by Ujwala for certification criteria - 170.315(b)(7) Data Segmentation for Privacy – Send - Start
            If objSettings.CDAPrivacyText <> "" Then
                txtCDAPrivacyText.Text = objSettings.CDAPrivacyText
            Else
                txtCDAPrivacyText.Text = ""
            End If
            '' Added by Ujwala for certification criteria - 170.315(b)(7) Data Segmentation for Privacy – Send - End


            ''
            'to save the CDS URL
            'If objSettings.CDS_PES_Url <> "" Then
            '    txtCDS_PESUrl.Text = objSettings.CDS_PES_Url
            'Else
            '    txtCCDFilePath.Text = ""
            'End If

            'If objSettings.CDS_PES_UserName <> "" Then
            '    txtCDS_PESUserName.Text = objSettings.CDS_PES_UserName
            'Else
            '    txtCDS_PESUserName.Text = ""
            'End If

            'If objSettings.CDS_PES_Password <> "" Then
            '    txtCDS_PESPassword.Text = objSettings.CDS_PES_Password
            'Else
            '    txtCDS_PESPassword.Text = ""
            'End If
            'If objSettings.CDS_PES_Enabled <> "" Then
            '    If (objSettings.CDS_PES_Enabled.ToString.Trim.ToUpper() = "TRUE") Then
            '        chkEnabledCDS.Checked = True
            '    Else
            '        chkEnabledCDS.Checked = False
            '    End If
            'Else
            '    chkEnabledCDS.Checked = False
            'End If
            'Code Start-Added by kanchan on 20100604 for CCD user
            If objSettings.CCDDefaultUser = "0" Then
                txtCCDUser.Text = ""
            Else
                _CCD_DefaultUserID = objSettings.CCDDefaultUser
                txtCCDUser.Text = GetDefaultUserName(_CCD_DefaultUserID)
            End If
            'Code Start-Added by kanchan on 20100604 for CCD user

            FillUniqueidentifierPatient()

            '' 20080809 Show Advanced Growth Chart
            If IsNothing(objSettings.ShowAdvancedGrowthChart) = False Then
                chk_GrowthChart.Checked = objSettings.ShowAdvancedGrowthChart
                grbGrowthChartPercentile.Enabled = chk_GrowthChart.Checked
            Else
                chk_GrowthChart.Checked = False
                grbGrowthChartPercentile.Enabled = False
            End If
            ''

            '' 20090721 '' Advanced Growth Chart Percentile
            Select Case objSettings.GrowthChartPercentile
                Case enumGrowthChartPercentile.ShowPercentile
                    rbShowPercentile.Checked = True
                Case enumGrowthChartPercentile.ShowPercentileOnMouseHoover
                    rbShowPercentileOnMouseHoover.Checked = True
                Case enumGrowthChartPercentile.DontShowPercentile
                    rbDontShowPercentile.Checked = True
            End Select
            ''


            ''by sudhir 20081111 
            'Show Age in Days
            If IsNothing(objSettings.ShowAgeInDays) = False Then
                chk_AgeFlag.Checked = objSettings.ShowAgeInDays
            Else
                chk_AgeFlag.Checked = False
            End If


            'PEDIATRICS SETTINGS
            If IsNothing(objSettings.IsPediatrics) = False Then
                chkPediatrics.Checked = objSettings.IsPediatrics
            Else
                chkPediatrics.Checked = False
            End If

            If IsNothing(objSettings.IsrequireSNOMED) = False Then
                chkProblemSNOMED.Checked = objSettings.IsrequireSNOMED
            Else
                chkProblemSNOMED.Checked = False
            End If

            If IsNothing(objSettings.IsAutogeneratedProblemFromExam) = False Then
                chkAutoGenerateProblems.Checked = objSettings.IsAutogeneratedProblemFromExam
            Else
                chkAutoGenerateProblems.Checked = False
            End If

            '''YES/No Labs SETTINGS
            If IsNothing(objSettings.IsYesNoLab) = False Then
                chkYesNoLabs.Checked = objSettings.IsYesNoLab
            Else
                chkYesNoLabs.Checked = False
            End If


            'PATIENT DEMOGRAPHIC MERGE
            If IsNothing(objSettings.IsPatDaemoMerge) = False Then
                chkpatdaemomerg.Checked = objSettings.IsPatDaemoMerge
            Else
                chkpatdaemomerg.Checked = False
            End If
            ''
            '' VITALS HEIGHT COPY FORWARD

            If IsNothing(objSettings.IsVitalsHeightCopyForward) = False Then
                ChkVitalsHeightCopy.Checked = objSettings.IsVitalsHeightCopyForward
            Else
                ChkVitalsHeightCopy.Checked = False
            End If
            'To show AGE LIMIT
            If IsNothing(objSettings.AgeLimit) = False Then
                txtAgeLimitPatientStrip.Text = objSettings.AgeLimit
            Else
                txtAgeLimitPatientStrip.Text = ""
            End If

            If chk_AgeFlag.Checked = True Then
                txtAgeLimitforWeeks.Enabled = True
                txtAgeLimitPatientStrip.Enabled = True
            Else
                txtAgeLimitPatientStrip.Enabled = False
                txtAgeLimitforWeeks.Enabled = False
            End If

            ''by sudhir 20081124 
            'To show AGE LIMIT for Weeks
            If IsNothing(objSettings.AgeLimitForWeeks) = False Then
                txtAgeLimitforWeeks.Text = objSettings.AgeLimitForWeeks
            Else
                txtAgeLimitforWeeks.Text = ""
            End If
            '' end sudhir

            chkSetCPTtoAllICD9.Checked = objSettings.SetCPTtoAllICD9
            chkDefaultProblemDxForSmartDx.Checked = objSettings.SetProblemDxAsDefaultForSmartDx
            ChkDefaultDxCPTPatientPrbDx.Checked = objSettings.DefaultDxCPTPatientProblemDiagnosis
            chkEnableSmartDxReviewScreen.Checked = objSettings.EnableSmartDxReviewScreen
            'sarika 
            'DMS 20080908 -- for Loading no of recieved faxes in DMS
            If IsNothing(objSettings.LoadNoOfFaxes) = False Then
                If objSettings.LoadNoOfFaxes <= 10 Then
                    numLoadNoOfFaxes.Value = 10
                Else
                    numLoadNoOfFaxes.Value = objSettings.LoadNoOfFaxes
                End If
            Else
                numLoadNoOfFaxes.Value = 10
            End If
            '------------------
            ''Sandip Darade 20090731
            If (gstrAdminFor = "gloPM") Then
                tbp_PMDBSettings.Text = "gloEMR Database Settings"
                chk_PMDBSettings.Text = "Add patient to gloEMR"
                lblServerName.Text = "gloEMR Servername"
                lblDatabaseName.Text = "gloEMR Databasename"
                pnl_Migratetype.Visible = True
                RetrievegloEMRDatabaseSettings()
                Set_gloEMRDBsettingControls()
                ''Sandip Darade 20090814
                ''Remove DM setting ,Fax setting and other setting tab for gloPM Admin
                tb_Settings.Controls.Remove(tbp_EMCodeSetting)
                tb_Settings.Controls.Remove(tbp_FaxSettings)
                tb_Settings.Controls.Remove(tbp_OtherSettings)
                tb_Settings.Controls.Remove(tbp_OMRSettings)

            Else
                ''Code for gloPMDatabase setting  commented by Sandip Darade 20100106
                ''Bug ID 5506

                'sarika PM DB Credentials 20081128
                ' ''pnl_Migratetype.Visible = False
                ' ''pnlgloPMDBSettings.Enabled = objSettings.PMAddPatient
                ' ''chk_PMDBSettings.Checked = objSettings.PMAddPatient
                ' '' ''Sandip darade 
                '' '' If (objSettings.PMAddPatient = True) Then
                ' ''txtPMServerName.Text = objSettings.PMServerName
                ' ''txtPMDatabaseName.Text = objSettings.PMDatabaseName
                ' ''If objSettings.PMSQLAuthentication = 0 Then
                ' ''    optSQLAuthentication.Checked = False
                ' ''    pnlSQLCredentials.Enabled = False
                ' ''Else
                ' ''    optSQLAuthentication.Checked = True
                ' ''    pnlSQLCredentials.Enabled = True
                ' ''End If
                '' ''If optSQLAuthentication.Checked = True Then
                ' ''txtSQLUserID.Text = objSettings.PMUserID
                '' ''     Dim objEncryptDecrypt As New clsEncryption
                '' '' txtSQLPassword.Text = objEncryptDecrypt.DecryptFromBase64String(objSettings.PwdStrSQL, constEncryptDecryptKey)
                '' ''   objEncryptDecrypt = Nothing

                ' ''txtSQLPassword.Text = objSettings.PMSQLPwd
                'End If
                ''20091106

                ''Sandip Darade 20090911
                ''Remove Payment,billing,other billing,marital status,alphaII setting tab for gloPM Admin
                tb_Settings.Controls.Remove(tbp_PaymentSettings)
                tb_Settings.Controls.Remove(tbPage_BillingSettings)
                tb_Settings.Controls.Remove(tbpg_MaritalStatus)
                tb_Settings.Controls.Remove(tbPage_OtherBillingSettings)
                tb_Settings.Controls.Remove(tbpg_ProviderSettings)
                tb_Settings.Controls.Remove(tbp_ExchangeServer) '' Exchange Server Tab
                tb_Settings.Controls.Remove(tbpg_AlphaIISettings) '' AlphaII settings  20091107
                Pnl_ReportingBasedon.Visible = False

                'Mitesh Patel 20120125
                'Removed OB Vitals Customization setting tab
                tb_Settings.Controls.Remove(tbpg_OBVitalCustomizationSettings)
                '-----x--------


                ''Sandip Darade 20090911
                ''on tab page  tbpg_AlphaIISettings
                ''Hide gloscrubber setting and below that show only alphaII radio button
                Pnl_gloScrubber.Visible = False
                Pnl_claimValidation.Visible = False
                '' 20091105
                'bCV_Alpha2.Checked = True
            End If

            '--
            txtSignaturetext.Text = objSettings.SignatureText
            txtcoSignaturetext.Text = objSettings.CoSignatureText

            If (objSettings.DMSImageDIP < numDMSImageDPI.Minimum Or objSettings.DMSImageDIP > numDMSImageDPI.Maximum) Then
                numDMSImageDPI.Value = numDMSImageDPI.Maximum
            Else
                numDMSImageDPI.Value = objSettings.DMSImageDIP
            End If

            chkUseFileCompession.Checked = objSettings.UseFileCompression
            'COMMENTED BY SHUBHANGI
            ' chkSplitDoc.Checked = objSettings.SplitDocument
            '\\Suraj 20090128
            chkRecoverDMSV2Doc.Checked = objSettings.RecoveryDMSV2Doc
            txt_DMSV2RecoveryPath.Text = objSettings.RecoveryDMSV2Path

            If chkRecoverDMSV2Doc.Checked = False Then
                '    txt_DMSV2RecoveryPath.Text = objSettings.RecoveryDMSV2Path
                '    'lblDMSV2RecoverPath.Visible = True
                '    'txt_DMSV2RecoveryPath.Visible = True
                '    'btnSelectRecoveryPath.Visible = True
                'Else
                lblDMSV2RecoverPath.Enabled = False
                txt_DMSV2RecoveryPath.Enabled = False
                btnSelectRecoveryPath.Enabled = False
            End If
            '\\ Suraj 20090128 Delete DMS doc after migration
            chk_DeleteDocAfterMigration.Checked = objSettings.DeleteDMSDocAfterMigration
            SetOccMEdDMSCategory(objSettings.OccMedDMSCategory)
            ' Sandip Darade 20090210 Use Coded History
            Chb_UseCodedhistory.Checked = objSettings.Usecodedhistory

            'OTC issue Warrning setting
            chkOTCIssueWarning.Checked = objSettings.OTCIssueWarning

            ' Sandip Darade 20090328 Show Coded History
            If objSettings.ShowCodedHistory = "Code" Then
                Rbtn_showcode.Checked = True
            ElseIf objSettings.ShowCodedHistory = "Description" Then
                Rbtn_showDesc.Checked = True
            Else
                Rbtn_ShowBoth.Checked = True
            End If
            ''Added by Mayuri:20120914-History PRD-Added settings optional,mandatory,Warning
            If objSettings.CodeFieldsinHistory = "CodeOptional" Then
                rbOptional.Checked = True
            ElseIf objSettings.CodeFieldsinHistory = "CodeMandatory" Then
                rbMandatory.Checked = True
            ElseIf objSettings.CodeFieldsinHistory = "CodeWarning" Then
                rbWarning.Checked = True
            Else
                rbOptional.Checked = True
            End If

            If objSettings.ShowSmokingColumn = "1" Then
                chkSmokingStatusColumn.Checked = True
            Else
                chkSmokingStatusColumn.Checked = False
            End If

            If objSettings.IsRetrieveESR = "1" Then
                chkRetrieveESRValue.Checked = True
                txtESRDays.Text = objSettings.ESRDay
            Else
                chkRetrieveESRValue.Checked = False
                txtESRDays.Text = ""
            End If


            If objSettings.IsRetrieveCRP = "1" Then
                chkRetrieveCRPValue.Checked = True
                txtCRPDays.Text = objSettings.CRPDay
            Else
                chkRetrieveCRPValue.Checked = False
                txtCRPDays.Text = ""
            End If


            If (objSettings.Usecodedhistory = True) Then
                Rbtn_showcode.Enabled = True
                Rbtn_showDesc.Enabled = True
                Rbtn_ShowBoth.Enabled = True
            Else
                Rbtn_showcode.Enabled = False
                Rbtn_showDesc.Enabled = False
                Rbtn_ShowBoth.Enabled = False
            End If
            ''''''
            If IsNothing(objSettings.IM_ReminderDays) = False Then
                If objSettings.IM_ReminderDays > 0 Then
                    NumUpDn_ImRminder.Value = objSettings.IM_ReminderDays
                Else
                    NumUpDn_ImRminder.Value = 1
                End If
            End If

            'If rbChiefComplaint.Checked = True Then
            '    objSettings.EMChiefComplaintType = "ChiefComplaint"
            'ElseIf rbProblemList.Checked = True Then
            '    objSettings.EMChiefComplaintType = "ProblemList"
            'End If

            'sarika SiteID 20090708
            If IsNothing(objSettings.SiteID) = False Then
                txtSiteID.Text = objSettings.SiteID
            Else
                txtSiteID.Text = ""
            End If
            '---

            If IsNothing(objSettings.TimeServerName) = False Then
                txttimeserver.Text = objSettings.TimeServerName
            Else
                txttimeserver.Text = ""
            End If

            If objSettings.EMChiefComplaintType = "ChiefComplaint" Then
                rbChiefComplaint.Checked = True
            ElseIf objSettings.EMChiefComplaintType = "ProblemList" Then
                rbProblemList.Checked = True
            ElseIf objSettings.EMChiefComplaintType = "" Then
                rbChiefComplaint.Checked = True
            Else
                rbChiefComplaint.Checked = True
            End If

            If objSettings.EMOnOff = "1" Then
                rbt_EMYes.Checked = True
            ElseIf objSettings.EMOnOff = "0" Then
                rbt_EMNo.Checked = True
            End If
            'sarika 31st aug 07
            'fill the arraylist with fax values at form load
            Dim arrList As ArrayList
            arrList = New ArrayList
            SetData(arrList)

            _arrayGetData = arrList
            '---------

            'Shubhangi 20090603'
            chkOtherPatientType.Checked = objSettings.OtherPatientType
            'Shubhangi 20100105
            'Add E\M Examtype settings in EMsettings
            FillExamTypeCombo()
            'cmbEMExamType.SelectedIndex = -1

            'COMMENTED BY SHUBHANGI 20110606
            'If CType(objSettings.EMExamType, enumExamControlType) = enumExamControlType.None Then
            '    cmbEMExamType.SelectedIndex = -1
            'Else
            '    cmbEMExamType.Text = CType(objSettings.EMExamType, enumExamControlType).ToString
            'End If
            'End Shubhangi

            'End shubhangi'
            ' Sandip Darade 20090622
            cmbGender.SelectedItem = objSettings.DefaultPatientGender

            rbICD9Driven.Checked = objSettings.ICD9Driven
            rbCPTDriven.Checked = Not (objSettings.ICD9Driven)
            rbShow8ICD9.Checked = objSettings.Show8ICD9
            rbShow4ICD9.Checked = Not (objSettings.Show8ICD9)
            rbShow4Modifier.Checked = objSettings.Show4Modifier
            rbShow2Modifier.Checked = Not (objSettings.Show4Modifier)

            '' SUDHIR 20090818 ''
            'chkPrescriptionProvider.Checked = objSettings.PrescriptionProviderAssociation//removed setting in 7020 as per PRD discussion for Incident #00006175
            chkPatientQuestionnaire.Checked = objSettings.PatientQuestionnaire
            '' END SUDHIR ''

            'chkSendUnassociatedDiagnosis.Checked = objSettings.SendUnassociatedDiagnosis 'line of of code commented by manoj jadhav on 20120730 for moving service setiings to Respective Service

            '\chkSplitDoc.Checked = objSettings.SplitDocument
            '******
            Call fill_FollowUpUsers()
            '******
            ''Sandip Darade 20000709
            ''billing settings 
            ''Sandip Darade 20091107
            If (gstrAdminFor = "gloPM") Then
                RemoveHandler Me.cmbProvider.SelectedIndexChanged, AddressOf Me.cmbProvider_SelectedIndexChanged
                FillProviders()
                FillPOS()
                FillTOS()
                FillFacilities()
                FillFeeSchedule()
                FillBillingSettings()

                ''Other billing settings 
                FillFeeSchedules()
                FillSpecialities()
                cmbFacilityType.Items.Add("")
                cmbFacilityType.Items.Add(FacilityType.Facility.ToString())
                cmbFacilityType.Items.Add(FacilityType.NonFacility.ToString())
                ''Retrieve other billing setting 
                If (objSettings.DefaultFeeSpeciality = "0") Then

                    'cmbSpeciality.SelectedIndex = 1
                Else
                    cmbSpeciality.SelectedValue = objSettings.DefaultFeeSpeciality
                End If
                If (objSettings.NoOfClaimPerBatch <> "") Then
                    Dim _maxclaimperbatch As Decimal = Convert.ToDecimal(objSettings.NoOfClaimPerBatch)
                    If _maxclaimperbatch <= numUpDn_NoOfClaims.Maximum AndAlso _maxclaimperbatch >= numUpDn_NoOfClaims.Minimum Then
                        numUpDn_NoOfClaims.Value = Convert.ToDecimal(objSettings.NoOfClaimPerBatch)
                    Else
                        numUpDn_NoOfClaims.Value = numUpDn_NoOfClaims.Maximum
                    End If
                Else
                    numUpDn_NoOfClaims.Value = numUpDn_NoOfClaims.Minimum
                End If

                If (objSettings.NoOfModifiers <> "") Then
                    Dim _maxmodifiers As Decimal = Convert.ToDecimal(objSettings.NoOfModifiers)
                    If _maxmodifiers <= numModifiers.Maximum AndAlso _maxmodifiers >= numModifiers.Minimum Then
                        numModifiers.Value = Convert.ToDecimal(objSettings.NoOfModifiers)
                    Else
                        numModifiers.Value = numModifiers.Maximum
                    End If
                Else
                    numModifiers.Value = numModifiers.Minimum
                End If
                If (objSettings.ShowLabCol <> "") Then
                    If Convert.ToBoolean(Convert.ToInt16(objSettings.ShowLabCol)) = True Then
                        chbox_AddShowLabCol.Checked = True
                    Else
                        chbox_AddShowLabCol.Checked = False
                    End If
                Else
                    chbox_AddShowLabCol.Checked = False
                End If

                If (objSettings.Defaultfeecharges <> "") Then
                    If Convert.ToInt32(objSettings.Defaultfeecharges) = FacilityType.Facility.GetHashCode() Then
                        cmbFacilityType.Text = FacilityType.Facility.ToString()
                    ElseIf Convert.ToInt32(objSettings.Defaultfeecharges) = FacilityType.NonFacility.GetHashCode() Then
                        cmbFacilityType.Text = FacilityType.NonFacility.ToString()
                    Else
                        cmbFacilityType.Text = FacilityType.None.ToString()
                    End If
                Else
                    cmbFacilityType.Text = FacilityType.None.ToString()
                End If


                txtCarrierNumber.Text = objSettings.DefaultCarrierNumber

                txtLocality.Text = objSettings.DefaultLocality
                cmb_Feeschedules.SelectedValue = objSettings.ClinicFeeSchedule

                '' end Retrieve other billing setting 

                ''Sandip Darade 200091109
                cmbAlphaIIAuthentication.Items.Add("Windows")
                cmbAlphaIIAuthentication.Items.Add("SQL")
                If cmbAlphaIIAuthentication.Items.Count > 0 Then
                    cmbAlphaIIAuthentication.SelectedIndex = 0
                End If
                RetrieveAlphaIIDatabaseSettings()
                ''Sandip Darade 20090710
                ''Marital Status setting
                ''Sandip Darade 20091107

                FillMaritalStatusFromBilling()
                FillMaritalStatusFromPatientRegistration()
                GetMaritalStatusSettings()

                ''End Marital Status setting
                ''Exchange Server settings
                GetExServerSetting()
                ''Sandip Darade 20091107

                DesignGridForProviderSetting()
                FillProviderSettings()

            End If

            '' 20091105
            'If (gstrAdminFor = "gloEMR") Then
            '    rbCV_Alpha2.Checked = True
            'End If
            ''''

            ''End Exchange Server settings
            ''get weekdays setting
            GetOtherSettings()
            Call fill_SurgicalAlertUsers()

            ''Start OB Setting
            Call fill_MedicalCategory()
            ''End OB Setting

            ''sudhir 20090107 -- for Provider List on C1Grid
            If chk_PMDBSettings.Checked = True Then
                FillProviderGridAll()
            End If


            ''Sandip Darade 
            ''Geniuos path setting 
            If IsNothing(objSettings.GeniusPath) = False Then
                'CType(cmbGeniusPaths.SelectedItem, ClsGeniusPath).GeniusPath = regKey.GetValue("GeniusPath")
                If objSettings.GeniusPath.Trim <> "" Then
                    'PopulateGeniusPaths()
                    Dim oGenusPaths As New List(Of ClsGeniusPath)
                    Dim objGeniusPath As New ClsGeniusPath
                    objGeniusPath.GeniusPath = objSettings.GeniusPath
                    If IsNothing(objSettings.GeniusCode) = False Then
                        objGeniusPath.GeniusCode = objSettings.GeniusCode
                    End If
                    oGenusPaths.Add(objGeniusPath)
                    cmbGeniusPaths.DataSource = oGenusPaths
                    cmbGeniusPaths.DisplayMember = "GeniusPath"
                    cmbGeniusPaths.ValueMember = "GeniusCode"

                    cmbGeniusPaths.Text = objSettings.GeniusPath
                    gstrGeniusPath = objSettings.GeniusPath
                    GeniusPath = gstrGeniusPath
                End If
                ''Geniuos path setting 
            End If

            If Not IsNothing(cmbCountry.DataSource) Then

                If Trim(objSettings.Country) <> "" Then
                    cmbCountry.SelectedValue = objSettings.Country
                Else
                    cmbCountry.SelectedIndex = 0
                End If
            End If

            If Not IsNothing(cmbFutureApptType.DataSource) Then
                If Trim(objSettings.FutureApptType) <> "" Then
                    cmbFutureApptType.SelectedValue = objSettings.FutureApptType
                Else
                    cmbFutureApptType.SelectedIndex = 0
                End If
            End If

            If Not IsNothing(cmbSameDayApptType.DataSource) Then
                If Trim(objSettings.SameDayApptType) <> "" Then
                    cmbSameDayApptType.SelectedValue = objSettings.SameDayApptType
                Else
                    cmbSameDayApptType.SelectedIndex = 0
                End If
            End If

            'line added by dipak 20091021 to fix bug 4216
            grpReportSettings.Visible = False
            'Shweta 20100125
            If objSettings.EmdeonUserName <> "" Then
                txtEmdeonUserName.Text = objSettings.EmdeonUserName
            Else
                txtEmdeonUserName.Text = ""
            End If

            'sanjog added on 2011 March 2 -condition to load DefaultLabTab setting in db or load default "Results"
            If objSettings.DefaultLabTab <> "" Then
                cmbDefaultLabTab.Text = objSettings.DefaultLabTab
            Else
                cmbDefaultLabTab.Text = "Results"
            End If
            'sanjog
            If objSettings.EmdeonPassword <> "" Then
                Dim objEncrypt As New clsEncryption
                txtEmdeonPassword.Text = objEncrypt.DecryptFromBase64String(objSettings.EmdeonPassword, constEncryptDecryptKey_Services)
            Else
                txtEmdeonPassword.Text = ""
            End If
            If objSettings.EmdeonURL <> "" Then
                txtEmdeonUrl.Text = objSettings.EmdeonURL
            Else
                txtEmdeonUrl.Text = ""
            End If
            If objSettings.EmdeonFacilityCode <> "" Then
                txtEmdeonFacilityCode.Text = objSettings.EmdeonFacilityCode
            Else
                txtEmdeonFacilityCode.Text = ""
            End If
            If objSettings.ParseUDIURL <> "" Then
                txtParseUDIURL.Text = objSettings.ParseUDIURL            
            End If
            If objSettings.GloLab_defaultUserID = "0" Then

                'txtEmdeon_DefaultUser.Text = objSettings.GloLab_defaultUserID
            Else
                _gloLab_DefaultUserID = objSettings.GloLab_defaultUserID
                txtEmdeon_DefaultUser.Text = GetDefaultUserName(_gloLab_DefaultUserID)
            End If

            ''Added by Abhijeet for Failed Lab Task user selection on 20111122
            If objSettings.GloLabFailure_DefaultUserID <> "0" Then
                _gloLabFailure_DefaultUserID = objSettings.GloLabFailure_DefaultUserID
                txtFailedLabTask_DefaultUser.Text = GetDefaultUserName(_gloLabFailure_DefaultUserID)
            End If

            If objSettings.GloHxForecast_defaultUserID <> 0 Then
                _gloHxForecast_defaultUserID = objSettings.GloHxForecast_defaultUserID
                txtHistoryForecastTask.Text = GetDefaultUserName(_gloHxForecast_defaultUserID)
            End If
            If objSettings.GloForecastReconcileDone_defaultUserID <> 0 Then
                _gloForecastReconcileDone_defaultUserID = objSettings.GloForecastReconcileDone_defaultUserID
                txtForecastReconciliationTask.Text = GetDefaultUserName(_gloForecastReconcileDone_defaultUserID)
            End If
            ''End of changes by Abhijeet for Failed Lab Task user selection on 20111122

            'Removed on 22/02/2010 regarding... gloLab settings--Madan
            'If objSettings.EmdeonDescription <> "" Then
            '    txtEmdeonDescription.Text = objSettings.EmdeonDescription
            'Else
            '    txtEmdeonDescription.Text = ""
            'End If
            'End Madan
            'End Shweta
            If objSettings.GloLab_Hsilb <> "" Then
                gloLab_hsi = objSettings.GloLab_Hsilb
            Else
                gloLab_hsi = ""
            End If
            'Madan-- Added for lab settings. 
            'Sandip Deshmukh. 20100827 1657
            'Removed as this is not required to hit URL validation
            'Or txtEmdeon_DefaultUser.Text.Trim() <> ""
            If objSettings.GloLab_Hsilb.Trim() = "" And (txtEmdeonUserName.Text().Trim() <> "" Or txtEmdeonUserName.Text.Trim() <> "" Or txtEmdeonUrl.Text.Trim() <> "" Or txtEmdeonPassword.Text.Trim() <> "" Or txtEmdeonFacilityCode.Text.Trim() <> "") Then
                _gloLab_settingsEdited = True
            Else
                _gloLab_settingsEdited = False
            End If
            '**** Removed as this is not required to hit URL validation
            If objSettings.GloLab_Billing <> "" Then
                If objSettings.GloLab_Billing = "Yes" Then
                    rbEmdeonYes.Checked = True
                ElseIf objSettings.GloLab_Billing = "No" Then
                    rbEmdeonNo.Checked = True
                ElseIf objSettings.GloLab_Billing = "Ask" Then
                    rbEmdeonAsk.Checked = True
                End If

            End If

            numEmdeonGetLabOrdersFromDaysOnReload.Value = objSettings.EmdeonGetLabOrdersFromDaysOnReload

            chkPreselectDiagnosis.Checked = objSettings.PreselectDiagnosisWhilePlacingEMDEONOrders
            chkDMSWin.Checked = objSettings.CloseDmstaskwin ''added to set task screen closed or not while opening dms window from task
            'ADded by madan on 20101112
            If objSettings.gloVaultVisibility Then
                rbHealthVaultOn.Checked = True
                rbHealthVaultOff.Checked = False
            Else
                rbHealthVaultOff.Checked = True
                rbHealthVaultOn.Checked = False
            End If
            'End madan

            ''Added by Abhijeet on 20101120
            If Not IsNothing(objSettings.PatientSpecificResultRange) Then
                If objSettings.PatientSpecificResultRange.Trim() = "1" Then
                    rbActive.Checked = True
                    rbInactive.Checked = False
                Else
                    rbActive.Checked = False
                    rbInactive.Checked = True
                End If
            Else
                rbActive.Checked = False
                rbInactive.Checked = True
            End If
            ''End of changes Abhijeet on 20101120

            'Store in Temp variable 
            sTempeRxserviceURl = TxtSurescriptURL.Text
            sTempeSecureMsgURl = txtSecureMessageURL.Text


            '---
            'start of code commented by manoj jadhav on 20111017 for New consolidated UI to device settings
            ' ''Added by Abhijeet on 20110407
            'If objSettings.UseVitalDevice Then
            '    chkUseVitalDevice.Checked = True
            '    'pnlVitalDeviceSettings.Enabled = True
            'Else
            '    chkUseVitalDevice.Checked = False
            '    chkUseVitalDevice.Enabled = False
            '    'pnlVitalDeviceSettings.Enabled = False
            'End If
            'If objSettings.UseSpirometryDevice Then
            '    chkUseSpirometryDevice.Checked = True
            '    txtprefixForSpirometryDevice.Enabled = True
            'Else
            '    chkUseSpirometryDevice.Checked = False
            '    chkUseSpirometryDevice.Enabled = False
            '    txtprefixForSpirometryDevice.Enabled = False
            'End If

            'Dim strVVitalDevoceKey As String = ""
            'Dim objectEncryption As New clsEncryption
            'If Not IsNothing(objectEncryption) Then
            '    strVVitalDevoceKey = objectEncryption.DecryptFromBase64String(objSettings.VitalDeviceKey, mdlGeneral.constEncryptDecryptKey)
            '    objectEncryption = Nothing
            'End If
            'txtVitalDeviceKey.Text = strVVitalDevoceKey
            'nup_NoofAttemptstoConnectVitalDevice.Value = objSettings.NoofAttempttoConnectVitalDevice
            ' ''End of changes  by Abhijeet on 20110407

            ' ''Added by madan for medfusion on 20110527
            'If objSettings.UseMedfusionInterface Then
            '    chkUseMedfusion.Checked = True
            '    chkUseMedfusion.Enabled = True
            'Else
            '    chkUseMedfusion.Checked = False
            '    chkUseMedfusion.Enabled = False
            'End If
            ' ''End madan changes
            'end of code commented by manoj jadhav on 20111017 for New consolidated UI to device settings

            'start of code commented by manoj jadhav on 20120730 for moving service setiings to Respective Service
            ' ''Added by Abhijeet on 20110422 & 20110425
            'If Not IsNothing(objSettings.IMRegistryHL7Format) Then
            '    If objSettings.IMRegistryHL7Format Then
            '        chkHL7ImmunizationRegistry.Checked = True
            '    Else
            '        chkHL7ImmunizationRegistry.Checked = False
            '    End If
            'Else
            '    chkHL7ImmunizationRegistry.Checked = False
            'End If

            'If chkHL7ImmunizationRegistry.Checked Then

            '    txtRegistryExpFilePath.Text = objSettings.RegistryFileExportPath

            '    If objSettings.RegistryFileIstobeExport Then
            '        chkExportFiletoRegistry.Checked = True
            '        pnlExportFileToRegistry.Enabled = True
            '        txtRegisrtyExportURL.Text = objSettings.RegistryFileExportURL
            '        txtRegisrtyUserId.Text = objSettings.RegistryFileExportUserID

            '        Dim _RegisrtyPassword As String = ""
            '        Dim objRegisrtyPasswordEncryptions As New clsEncryption
            '        If Not IsNothing(objRegisrtyPasswordEncryptions) And objSettings.gloHL7SQLPassword <> vbNullString Then
            '            _RegisrtyPassword = objRegisrtyPasswordEncryptions.DecryptFromBase64String(objSettings.RegistryFileExportPassword, mdlGeneral.constEncryptDecryptKey)
            '            objRegisrtyPasswordEncryptions = Nothing
            '        End If

            '        txtRegisrtyPassword.Text = _RegisrtyPassword  'objSettings.RegistryFileExportPassword

            '        txtRegisrtyFacilityID.Text = objSettings.RegistryFileExportFacilityID
            '        If DateTime.TryParse(objSettings.RegistryFileExportEveryDayAt, Now) Then
            '            dtPickExpFileat.Value = Convert.ToDateTime(objSettings.RegistryFileExportEveryDayAt.Trim)
            '        End If
            '    Else
            '        chkExportFiletoRegistry.Checked = False
            '        pnlExportFileToRegistry.Enabled = False
            '    End If
            'Else
            '    chkExportFiletoRegistry.Enabled = False
            '    chkExportFiletoRegistry.Checked = False
            '    pnlExportFileToRegistry.Enabled = False
            'End If

            ' '' End of changes by Abhijeet on 20110422 & 20110425
            'end of code commented by manoj jadhav on 20120730 for moving service setiings to Respective Service


            'start of code commented by manoj jadhav on 20111017 for New consolidated UI to device settings
            ' ''Added by madan...
            'If objSettings.ECGInterfaceUrl <> "" Then
            '    txtECGInterfaceUrl.Text = objSettings.ECGInterfaceUrl
            'Else
            '    txtECGInterfaceUrl.Text = ""
            'End If

            'If objSettings.ECGInstutionID <> "" Then
            '    txtECGInterfaceId.Text = objSettings.ECGInstutionID
            'Else
            '    txtECGInterfaceId.Text = ""
            'End If
            ' ''End madan  Changes

            'If objSettings.ECGUserProviderID <> "" Then
            '    txtECGUserProviderId.Text = objSettings.ECGUserProviderID
            'Else
            '    txtECGUserProviderId.Text = ""
            'End If

            'If objSettings.ECGEnabled Then
            '    ChkUseCardioScienceECGDevice.Checked = True
            '    txtECGInterfaceId.Enabled = True
            '    txtECGInterfaceUrl.Enabled = True
            '    txtECGUserProviderId.Enabled = True

            'Else
            '    ChkUseCardioScienceECGDevice.Checked = False
            '    txtECGInterfaceId.Enabled = False
            '    txtECGInterfaceUrl.Enabled = False
            '    txtECGUserProviderId.Enabled = False
            'End If
            ''end of code commented by manoj jadhav on 20111017 for New consolidated UI to device settings

            ''dhruv 20100216
            If objSettings.FormularyPassword <> "" Then
                Dim objEncrypt As New clsEncryption
                txtFormularyPassword.Text = objEncrypt.DecryptFromBase64String(objSettings.FormularyPassword, constEncryptDecryptKey)
                gstrFormularyPassword = txtFormularyPassword.Text
            Else
                'txtFormularyPassword.Text = ""
                txtFormularyPassword.Text = gstrSQLPasswordEMR
                gstrFormularyPassword = gstrSQLPasswordEMR
            End If
            ''------------------------------------------password
            If objSettings.FormularyServerName <> "" Then
                txtFormularyServerName.Text = objSettings.FormularyServerName
                gstrFormularySQLServer = objSettings.FormularyServerName
            Else
                'txtFormularyServerName.Text = ""
                txtFormularyServerName.Text = gstrSQLServerName
                gstrFormularySQLServer = gstrSQLServerName
            End If
            ''---------------------------------------------servername
            If objSettings.FormularyDataBaseName <> "" Then
                txtFormularyDataBaseName.Text = objSettings.FormularyDataBaseName
                gstrFormalayrDatabase = objSettings.FormularyDataBaseName
            Else
                'txtFormularyDataBaseName.Text = ""
                '' GLO2011-0015850 6052 gloEMR Admin Defect - Formulary database
                '' Database Name Changed
                txtFormularyDataBaseName.Text = "gloFormulary"  ''Changed the Default database name instead of  "FormularyDB" ''GLO2011-0015850 6052 gloEMR Admin Defect - Formulary database 
                gstrFormalayrDatabase = "gloFormulary"
            End If

            If objSettings.FormularyUserName <> "" Then
                txtFormularyUserId.Text = objSettings.FormularyUserName
                gstrFormularyUserID = objSettings.FormularyUserName
            Else
                'txtFormularyUserId.Text = ""
                txtFormularyUserId.Text = gstrSQLUser
                gstrFormularyUserID = gstrSQLUser
            End If

            If txtFormularyUserId.Text = "" Or txtFormularyPassword.Text = "" Then
                rdbWindows.Checked = True
            Else
                rdbSQL.Checked = True
            End If

            ''---end
            '--Code Added by Shirish 20100614
            If objSettings.GloLab_Provider_Usage <> "" Or objSettings.GloLab_Provider_Usage <> Nothing Then
                If objSettings.GloLab_Provider_Usage.ToUpper() = rbProviderLabUsageAsk.Tag.ToUpper() Then
                    rbProviderLabUsageAsk.Checked = True
                ElseIf objSettings.GloLab_Provider_Usage.ToUpper() = rbProviderLabUsageLabOrder.Tag.ToUpper() Then
                    rbProviderLabUsageLabOrder.Checked = True
                ElseIf objSettings.GloLab_Provider_Usage.ToUpper() = rbProviderLabUsageRecordResults.Tag.ToUpper() Then
                    rbProviderLabUsageRecordResults.Checked = True
                ElseIf objSettings.GloLab_Provider_Usage.ToUpper() = rbProviderLabUsageTask.Tag.ToUpper() Then
                    rbProviderLabUsageTask.Checked = True
                End If
            End If
            'End of code Shirish 20100614
            ''Integrated by Mayuri:20101020-Provider Signature change
            Chk_UseSignatureDelegates.Checked = objSettings.UseSignatureDelegates
            '''''''''''''''Integrated by Mayuri:20100731 - For EM Coding Changes - Admin Settings
            FillEMExamType()
            FillEMVisitType()
            SetEMSettings()


            pnlsnowmade.Visible = True
            Panel119.Size = New System.Drawing.Size(701, 286)


            txtSMServerName.Text = objSettings.SMServerName
            txtSMDatabaseName.Text = objSettings.SMDatabaseName
            txtSMSQLUserID.Text = objSettings.SMSQLUserId

            ''Start ::(Decryption)SnowMadePassword Encryption
            Dim _snowMadePassWord As String = ""
            Dim objEncryption As New clsEncryption
            If Not IsNothing(objEncryption) Then
                _snowMadePassWord = objEncryption.DecryptFromBase64String(objSettings.SMSQLPwd, mdlGeneral.constEncryptDecryptKey)
                'txtSMSQLPassword.Text = objSettings.SMSQLPwd
                objEncryption = Nothing
            End If
            txtSMSQLPassword.Text = _snowMadePassWord
            ''End ::(Decryption)SnowMadePassword Encryption
            ' m_SMblnSQLAuthentication As Boolean
            optSMSQLAuthentication.Checked = objSettings.SMblnSQLAuthentication
            optSMWINAuthentication.Checked = Not optSMSQLAuthentication.Checked
            'chetan

            ''''''''''''''''''''''''''''''

            txtRxNServerName.Text = objSettings.RxNServerName
            txtRxNDatabaseName.Text = objSettings.RxNDatabaseName
            txtRxNSQLUserID.Text = objSettings.RxNSQLUserId

            ''Start ::(Decryption)RxNormPassword Encryption
            Dim _rxNormPassWord As String = ""
            Dim objEncryptions As New clsEncryption
            If Not IsNothing(objEncryptions) Then
                _rxNormPassWord = objEncryptions.DecryptFromBase64String(objSettings.RxNSQLPwd, mdlGeneral.constEncryptDecryptKey)
                'txtRxNSQLPassword.Text = objSettings.RxNSQLPwd
                objEncryptions = Nothing
            End If
            txtRxNSQLPassword.Text = _rxNormPassWord
            ''End ::(Decryption)RxNormPassword Encryption

            optRxNSQLAuthentication.Checked = objSettings.RxNblnSQLAuthentication
            optRxNWINAuthentication.Checked = Not optRxNSQLAuthentication.Checked
            'Code End-Added by kanchan on 20100908 for rxnorm db settings

            '---- Added by Rahul Patel on 26-10-2010  ---'
            '---- For display DMS Database setting ------'
            If objSettings.DMSServerName.Trim <> "" Then
                txtDMSServerName.Text = objSettings.DMSServerName
            End If
            If objSettings.DMSDatabaseName.Trim <> "" Then
                txtDMSDatabaseName.Text = objSettings.DMSDatabaseName
            End If
            If objSettings.DMSSQLUserId.Trim <> "" Then
                txtDMSUserId.Text = objSettings.DMSSQLUserId
            End If

            ''Start ::(Decryption)DMSPassword Encryption
            Dim _DMSPassWord As String = ""
            Dim objDMSEncryptions As New clsEncryption
            If Not IsNothing(objDMSEncryptions) Then
                If objSettings.DMSSQLPwd.Trim <> "" Then
                    _DMSPassWord = objDMSEncryptions.DecryptFromBase64String(objSettings.DMSSQLPwd, mdlGeneral.constEncryptDecryptKey)
                Else
                    _DMSPassWord = ""
                End If
                objDMSEncryptions = Nothing
            End If
            txtDMSPassword.Text = _DMSPassWord
            ''End ::(Decryption)DMSPassword Encryption

            'optDMSSqlAuthentication.Checked = objSettings.DMSblnSQLAuthentication
            optDMSWINAuthentication.Checked = True
            '---- End for displaying database setting ---'
            '-----End of code added by rahul patel on 26-10-2010 --'


            'added by nilesh on date 20101025
            'for display gloHL7 settings
            If Not IsNothing(objSettings.gloHL7ServerName) Then
                txtgloHL7ServerName.Text = objSettings.gloHL7ServerName
            End If
            If Not IsNothing(objSettings.gloHL7DatabaseName) Then
                txtgloHL7DatabaseName.Text = objSettings.gloHL7DatabaseName
            End If
            If Not IsNothing(objSettings.gloHL7SQLUserID) Then
                txtgloHL7UserID.Text = objSettings.gloHL7SQLUserID
            End If

            ''Start ::(Decryption)RxNormPassword Encryption
            Dim _gloHL7PassWord As String = ""
            Dim objgloHL7Encryptions As New clsEncryption
            If Not IsNothing(objgloHL7Encryptions) And objSettings.gloHL7SQLPassword <> vbNullString Then
                _gloHL7PassWord = objgloHL7Encryptions.DecryptFromBase64String(objSettings.gloHL7SQLPassword, mdlGeneral.constEncryptDecryptKey)
                objgloHL7Encryptions = Nothing
            End If
            txtgloHL7Password.Text = _gloHL7PassWord
            ''End ::(Decryption)RxNormPassword Encryption

            optgloHL7SqlAuthentication.Checked = objSettings.gloHL7blnSQLAuthentication
            optgloHL7WinAuthentication.Checked = Not optgloHL7SqlAuthentication.Checked
            'end by nilesh on date 20101025

            '' chetan integrated 04-oct-2010

            '''''''''''''''Integrated by Mayuri:20100731 - For EM Coding Changes - Admin Settings
            If IsNothing(objSettings.SMTrvNode) = False And objSettings.SMTrvNode.Trim() <> "" Then
                Dim str As String() = objSettings.SMTrvNode.Split(",")
                For i As Integer = 0 To trvsnomed.Nodes.Count - 1
                    If Array.IndexOf(str, trvsnomed.Nodes(i).Text.Trim()) >= 0 Then
                        trvsnomed.Nodes(i).Checked = True

                    End If
                Next
            End If

            'gloCommon.Cls_TabIndexSettings.TabScheme.DownFirst()
            'gloAuditTrail.Cls_TabIndexSettings.TabScheme(InlineAssignHelper(scheme, Cls_TabIndexSettings.TabScheme.AcrossFirst))

            Dim scheme As gloCommon.Cls_TabIndexSettings.TabScheme = gloCommon.Cls_TabIndexSettings.TabScheme.AcrossFirst
            Dim tom As New gloCommon.Cls_TabIndexSettings(Me)
            ' This method actually sets the order all the way down the control hierarchy.
            tom.SetTabOrder(scheme)

            'added by pradeep(20110106)for page break setting on pharmacy change in printing prescription report
            Chk_PrintMultipleRx_Per_Script_Page.Checked = objSettings.PrintMultipleRx_PerScriptPage_setting
            ''end of code for pradeep

            '' added by Manoj Jadhav on 20110314
            ChkPrintRxPharmacyOnReport.Checked = objSettings.PrintPharmacyOnRxReportSetting
            If Initializing = False Then
                chkCustomizeReportPrintSetting.Checked = objSettings.CustomizeRxReportPrintSetting
                Initializing = True
            End If

            sSingleRxStateCustiomizeReport = objSettings.SingleRxStateCustiomizeReport
            sMultipleRxStateCustiomizeReport = objSettings.MultipleRxStateCustiomizeReport


            GENERALMESSAGELOGPAGESIZE = objSettings.GENERALMESSAGELOGPAGESIZE
            '' ended by Manoj Jadhav on 20110314


            'added by Ujwala as on 02032015 to Get gloServices DB settings 
            'for display gloServices DB settings
            If Not IsNothing(objSettings.ServicesServerName) Then
                txtSrvcServerName.Text = objSettings.ServicesServerName
            End If
            If Not IsNothing(objSettings.ServicesDatabaseName) Then
                txtSrvcDatabaseName.Text = objSettings.ServicesDatabaseName
            End If
            If Not IsNothing(objSettings.ServicesUserID) Then
                txtSrvcSQLUserID.Text = objSettings.ServicesUserID
            End If

            ''Start ::(Decryption)Services Password Encryption
            Dim _gloServicesPassWord As String = ""
            Dim objgloServicesEncryptions As New clsEncryption
            If Not IsNothing(objgloServicesEncryptions) And objSettings.ServicesPassword <> vbNullString Then
                _gloServicesPassWord = objgloServicesEncryptions.DecryptFromBase64String(objSettings.ServicesPassword, mdlGeneral.constEncryptDecryptKey)
                objgloServicesEncryptions = Nothing
            End If
            txtSrvcSQLPassword.Text = _gloServicesPassWord
            ''End ::(Decryption)Services Password Encryption

            optSrvcSQLAuthentication.Checked = objSettings.ServicesAuthentication
            optSrvcWINAuthentication.Checked = Not optSrvcSQLAuthentication.Checked
            'added by Ujwala as on 02032015 to Get gloServices DB settings 

            FillPatientAccSettings()
            'start of code added by manoj jadhav on 20111017for reading device settings values
            chkUseVitalDevice.Checked = objSettings.UseVitalDevice
            chkUseVitalDevice.Enabled = objSettings.UseVitalDevice
            chkUseSpirometryDevice.Checked = objSettings.UseSpirometryDevice
            chkUseSpirometryDevice.Enabled = objSettings.UseSpirometryDevice
            ChkUseCardioScienceECGDevice.Checked = objSettings.UseCardioScinceECGDevice
            ChkUseCardioScienceECGDevice.Enabled = objSettings.UseCardioScinceECGDevice

            ''Added for MU2 Patient Portal implementation on 20130621
            'chkUseMedfusion.Checked = objSettings.UseMedfusionInterface
            'chkUseMedfusion.Enabled = objSettings.UseMedfusionInterface

            chkAcknowledgeEmailSend.Checked = False
            chkAcknowledgeEmailSend.Enabled = False
            chkNotifyStatement.Checked = False            
            'chkAcknowledgeEmailSend.Visible = False
            txtHoosKoosServiceURL.Text = objSettings.HoosKoosSurveyService

            txtPDMPUrl.Text = objSettings.PDMPService
            sTemppdmpURL = txtPDMPUrl.Text

            nmMedHxRestriction.Value = objSettings.MedHxRestriction

            If objSettings.UseMedfusionInterface = True Then
                chkUseMedfusion.Checked = objSettings.UseMedfusionInterface
                chkUseMedfusion.Enabled = objSettings.UseMedfusionInterface
            ElseIf objSettings.UsePatientPortal = True Then
                chkUseMedfusion.Checked = objSettings.UsePatientPortal
                chkUseMedfusion.Enabled = objSettings.UsePatientPortal
                'chkAcknowledgeEmailSend.Visible = True
                chkAcknowledgeEmailSend.Enabled = True
                chkAcknowledgeEmailSend.Checked = objSettings.PatientPortalLabAckNotification

                chkNotifyStatement.Enabled = True
                chkNotifyStatement.Checked = objSettings.PatientPortalStatementNotification
            Else
                chkUseMedfusion.Checked = False
                chkUseMedfusion.Enabled = False

            End If
            ''End
            'for display sCommonEmailServicePath settings
            If Not IsNothing(objSettings.sCommonEmailServicePath) Then
                txtPatientPortalEmailService.Text = objSettings.sCommonEmailServicePath
            End If
            'for display sCommongloCoreServicePath settings
            If Not IsNothing(objSettings.sCommongloCoreServicePath) Then
                txtPatientPortalgloCoreServicesInstallationPath.Text = objSettings.sCommongloCoreServicePath
            End If
            If objSettings.Signaturepadtouse Then
                rdoNewsignaturepad.Checked = True
            Else
                rdooldSignaturepad.Checked = True
            End If
            ChkUseWelchAllynECGDevice.Checked = objSettings.UseWelchAllynECGDevice
            ChkUseWelchAllynECGDevice.Enabled = objSettings.UseWelchAllynECGDevice
            chkUseMidmarkECGDevice.Enabled = objSettings.UseMidmarkECGDevice
            chkUseMidmarkECGDevice.Checked = objSettings.UseMidmarkECGDevice
            'end of code added by manoj jadhav on 20111017for reading device settings values
            ChkMultipleSupervisorsforPaperRx.Checked = objSettings.MultipleSupervisorsforPaperRx
            ''Added for gloCommunity s setting on 03-jan-2012
            ''  If objSettings.gloCommunityInstalled = True Then
            _blnglocominstalled = objSettings.gloCommunityInstalled

            If objSettings.gloCommunityInstalled = True Then
                isFormLoad = True

                If objSettings.SharepointSrvNm = "" Then
                    txtSPName.Text = "https://"
                Else
                    txtSPName.Text = objSettings.SharepointSrvNm
                End If
                txtSPFolderNm.Text = objSettings.SharepointSiteNm
                txtADFSSrvNm.Text = objSettings.ADFSServerName
                chkglocomm.Checked = objSettings.gloCommunityFeature
                btnSPTestConnection.Enabled = chkglocomm.Checked
                If chkglocomm.Checked = False Then
                    txtSPName.Enabled = False
                    txtSPFolderNm.Enabled = False
                    txtADFSSrvNm.Enabled = False
                End If

                gIscommunitystaging = objSettings.IsCommunitystaging
                If gIscommunitystaging = True Then
                    rb_staging.Checked = True
                    rb_Production.Checked = False
                Else
                    rb_Production.Checked = True
                    rb_staging.Checked = False
                End If

                'Code Start-Added by kanchan on 20120801
                mdlGeneral.SharepointAuthentication = objSettings.SharepointAuthentication
                If Not IsNothing(objSettings.SharepointAuthentication) AndAlso objSettings.SharepointAuthentication.Trim().ToUpper() = "FORM" Then
                    txtADFSSrvNm.Visible = False
                    lblADFS.Visible = False
                Else
                    txtADFSSrvNm.Visible = True
                    lblADFS.Visible = True
                End If
                'Code End-Added by kanchan on 20120801

            Else
                Dim tp As TabPage = CType(tb_Settings.TabPages("tbp_Sharepoint"), TabPage)
                tb_Settings.TabPages.Remove(tp)
                ''("tbp_Sharepoint").re  


            End If
            'txtDomainNm.Text = objSettings.DomainName
            '' '' '' ''txtCommsrv.Text = objSettings.SPCommSrvNm
            ''End


            ''code added on 7-dec-2012 for global hl7outbound and genius setting


            Try
                RemoveHandler chkhl7outb.CheckedChanged, AddressOf chkhl7outb_CheckedChanged
                chkhl7outb.Checked = objSettings.globlnhl7OutBound

            Catch ex As Exception
            Finally
                AddHandler chkhl7outb.CheckedChanged, AddressOf chkhl7outb_CheckedChanged

            End Try
            chkhl7PatientReg.Checked = objSettings.globlnhl7Sendpatientdet
            chkHL7Appointment.Checked = objSettings.globlnhl7Sendapptdet
            chk_HL7_SendCharges_SaveClose.Checked = objSettings.globlnhl7Sendchargeonsacl
            chk_HL7_SendCharges_SaveFinish.Checked = objSettings.globlnhl7Sendchronsafi
            chkHL7Immunization.Checked = objSettings.globlnhl7SendImmudet
            chk_HL7_SendVisitSum_SaveClose.Checked = objSettings.globlnhl7SendVisitsumonsacl
            Chk_HL7_SendVisitSum_SaveFinish.Checked = objSettings.globlnhl7SendVisitsumonsafi
            If ((chkHL7Immunization.Checked = False) Or (chk_HL7_SendCharges_SaveFinish.Checked = False) Or (chk_HL7_SendCharges_SaveClose.Checked = False) Or (chkHL7Appointment.Checked = False) Or (chkhl7PatientReg.Checked = False) _
               Or (chk_HL7_SendVisitSum_SaveClose.Checked = False) Or (Chk_HL7_SendVisitSum_SaveFinish.Checked = False)) Then
                chk_allhl7.Checked = False
            Else
                chk_allhl7.Checked = True
            End If


            Try
                RemoveHandler chkgenoutb.CheckedChanged, AddressOf chkgenoutb_CheckedChanged

                chkgenoutb.Checked = objSettings.globlnGenOutBound

            Catch ex As Exception
            Finally

                AddHandler chkgenoutb.CheckedChanged, AddressOf chkgenoutb_CheckedChanged

            End Try
            chk_Gen_SaveandClose.Checked = objSettings.globlnGenSendchargeonsacl
            chk_Gen_SaveandFinish.Checked = objSettings.globlnGenSendchargeonsafi
            If ((chk_Gen_SaveandClose.Checked = False) Or (chk_Gen_SaveandFinish.Checked = False)) Then
                chk_allGen.Checked = False
            Else
                chk_allGen.Checked = True
            End If


            EnableDisableDefaultInterfaceSettings()

            '08-Apr-13 Aniket: Disable Multiple Race if the setting is checked

            If chkMU2Features.Checked = True Then
                chkMU2Features.Enabled = False
            End If

            If chkNCPDPVer10dot6.Checked = False Then
                pnlSecureMessage.Enabled = False
            End If

            If chkOBSpeciality.Checked Then
                pnlMedicalCategory.Visible = True
                pnl_lblMedicalCategory.Visible = True
            Else
                pnlMedicalCategory.Visible = False
                pnl_lblMedicalCategory.Visible = False
            End If

            chkShowUserNoteFirst.Checked = Convert.ToBoolean(objSettings.showUserNoteFirst)

            AddHandler chkMU2Features.CheckedChanged, AddressOf chkMU2Features_CheckedChanged

            Me.Cursor = Cursors.Default
        Catch objErr As Exception
            Me.Cursor = Cursors.Default
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            isFormLoad = False
            AddHandler Me.cmbProvider.SelectedIndexChanged, AddressOf Me.cmbProvider_SelectedIndexChanged
            AddHandler cmb_InsuranceType.SelectedIndexChanged, AddressOf cmb_InsuranceType_SelectedIndexChanged
            Me.Text = tb_Settings.TabPages(tb_Settings.SelectedIndex).Text
        End Try
    End Sub

    Public Sub EnableDisableDefaultInterfaceSettings()
        '' code added for hl7 outbound and genius 
        If (chkhl7outb.Checked) Then
            chk_allhl7.Enabled = True
            chkhl7PatientReg.Enabled = True
            chk_HL7_SendCharges_SaveClose.Enabled = True
            chk_HL7_SendCharges_SaveFinish.Enabled = True
            chkHL7Immunization.Enabled = True
            chkHL7Appointment.Enabled = True
            chk_HL7_SendVisitSum_SaveClose.Enabled = True
            Chk_HL7_SendVisitSum_SaveFinish.Enabled = True
        Else
            chk_allhl7.Enabled = False
            chkhl7PatientReg.Enabled = False
            chk_HL7_SendCharges_SaveClose.Enabled = False
            chk_HL7_SendCharges_SaveFinish.Enabled = False
            chkHL7Immunization.Enabled = False
            chkHL7Appointment.Enabled = False
            chk_HL7_SendVisitSum_SaveClose.Enabled = False
            Chk_HL7_SendVisitSum_SaveFinish.Enabled = False
        End If


        If (chkgenoutb.Checked) Then
            chk_allGen.Enabled = True
            chk_Gen_SaveandClose.Enabled = True
            chk_Gen_SaveandFinish.Enabled = True
        Else
            chk_allGen.Enabled = False
            chk_Gen_SaveandClose.Enabled = False
            chk_Gen_SaveandFinish.Enabled = False
        End If
    End Sub

    Public Sub FillEMExamType()
        '''''''''''''''Added by Ujwala Atre as on 2010/07/28 - For EM Coding Changes - Admin Settings
        Try
            Dim clist As myList
            Dim arrlist As New List(Of myList)

            clist = New myList
            clist.ID = AlphaII.CodeWizard.Objects.EvaluationManagement.EvalMgmtExamType.Cardiovascular.GetHashCode()
            clist.AssociatedCategory = "Cardiovascular"
            arrlist.Add(clist)

            clist = New myList
            clist.ID = AlphaII.CodeWizard.Objects.EvaluationManagement.EvalMgmtExamType.EarsNoseThroat.GetHashCode()
            clist.AssociatedCategory = "Ears Nose Throat"
            arrlist.Add(clist)

            clist = New myList
            clist.ID = AlphaII.CodeWizard.Objects.EvaluationManagement.EvalMgmtExamType.Eye.GetHashCode()
            clist.AssociatedCategory = "Eye"
            arrlist.Add(clist)

            clist = New myList
            clist.ID = AlphaII.CodeWizard.Objects.EvaluationManagement.EvalMgmtExamType.GeneralMultiSystem.GetHashCode()
            clist.AssociatedCategory = "General Multiple System"
            arrlist.Add(clist)

            clist = New myList
            clist.ID = AlphaII.CodeWizard.Objects.EvaluationManagement.EvalMgmtExamType.Genitourinary.GetHashCode()
            clist.AssociatedCategory = "Genitourinary"
            arrlist.Add(clist)

            clist = New myList
            clist.ID = AlphaII.CodeWizard.Objects.EvaluationManagement.EvalMgmtExamType.HemaLymphImmuno.GetHashCode()
            clist.AssociatedCategory = "HemaLymphImmuno"
            arrlist.Add(clist)

            clist = New myList
            clist.ID = AlphaII.CodeWizard.Objects.EvaluationManagement.EvalMgmtExamType.Musculoskeletal.GetHashCode()
            clist.AssociatedCategory = "Musculoskeletal"
            arrlist.Add(clist)

            clist = New myList
            clist.ID = AlphaII.CodeWizard.Objects.EvaluationManagement.EvalMgmtExamType.Neurological.GetHashCode()
            clist.AssociatedCategory = "Neurological"
            arrlist.Add(clist)

            clist = New myList
            clist.ID = AlphaII.CodeWizard.Objects.EvaluationManagement.EvalMgmtExamType.None.GetHashCode()
            clist.AssociatedCategory = "None"
            arrlist.Add(clist)

            clist = New myList
            clist.ID = AlphaII.CodeWizard.Objects.EvaluationManagement.EvalMgmtExamType.Pre97Guidelines.GetHashCode()
            ''''''''''clist.AssociatedCategory = "Pre97Guidelines"
            clist.AssociatedCategory = "95 Guidelines"
            arrlist.Add(clist)

            clist = New myList
            clist.ID = AlphaII.CodeWizard.Objects.EvaluationManagement.EvalMgmtExamType.Psychiatric.GetHashCode()
            clist.AssociatedCategory = "Psychiatric"
            arrlist.Add(clist)

            clist = New myList
            clist.ID = AlphaII.CodeWizard.Objects.EvaluationManagement.EvalMgmtExamType.Respiratory.GetHashCode()
            clist.AssociatedCategory = "Respiratory"
            arrlist.Add(clist)

            clist = New myList
            clist.ID = AlphaII.CodeWizard.Objects.EvaluationManagement.EvalMgmtExamType.Skin.GetHashCode()
            clist.AssociatedCategory = "Skin"
            arrlist.Add(clist)

            chkEMExamType.DataSource = arrlist
            chkEMExamType.DisplayMember = "AssociatedCategory"
            chkEMExamType.ValueMember = "ID"

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.EnM, gloAuditTrail.ActivityCategory.GenerateCode, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
        '''''''''''''''Added by Ujwala Atre as on 2010/07/28 - For EM Coding Changes - Admin Settings
    End Sub

    Public Sub FillEMVisitType()
        '''''''''''''''Added by Ujwala Atre as on 2010/07/28 - For EM Coding Changes - Admin Settings
        Try
            Dim clist As myList
            Dim arrlist As New List(Of myList)

            clist = New myList
            clist.ID = AlphaII.CodeWizard.Objects.EvaluationManagement.EvalMgmtCodeType.OfficeOutpatientSvcNew.GetHashCode()
            clist.AssociatedCategory = "Office or Other Outpatient Services - NEW"
            arrlist.Add(clist)

            clist = New myList
            clist.ID = AlphaII.CodeWizard.Objects.EvaluationManagement.EvalMgmtCodeType.OfficeOutpatientSvcEstablished.GetHashCode()
            clist.AssociatedCategory = "Office of Other Outpatient Services - ESTABLISHED"
            arrlist.Add(clist)

            clist = New myList
            clist.ID = AlphaII.CodeWizard.Objects.EvaluationManagement.EvalMgmtCodeType.HospObservationSvc.GetHashCode()
            clist.AssociatedCategory = "Hospital Observation Services"
            arrlist.Add(clist)

            clist = New myList
            clist.ID = AlphaII.CodeWizard.Objects.EvaluationManagement.EvalMgmtCodeType.HospObservationSvcWAdmissionDischarge.GetHashCode()
            clist.AssociatedCategory = "Hospital Observation Services w/ Admission and Discharge"
            arrlist.Add(clist)

            clist = New myList
            clist.ID = AlphaII.CodeWizard.Objects.EvaluationManagement.EvalMgmtCodeType.HospInpatientSvcInitialCare.GetHashCode()
            clist.AssociatedCategory = "Hospital Inpatient Services - INITIAL CARE"
            arrlist.Add(clist)

            clist = New myList
            clist.ID = AlphaII.CodeWizard.Objects.EvaluationManagement.EvalMgmtCodeType.HospInpatientSvcSubsequentCare.GetHashCode()
            clist.AssociatedCategory = "Hospital Inpatient Services - SUBSEQUENT CARE"
            arrlist.Add(clist)

            clist = New myList
            clist.ID = AlphaII.CodeWizard.Objects.EvaluationManagement.EvalMgmtCodeType.EmergencyDeptSvc.GetHashCode()
            clist.AssociatedCategory = "Emergency Department Services"
            arrlist.Add(clist)

            clist = New myList
            clist.ID = AlphaII.CodeWizard.Objects.EvaluationManagement.EvalMgmtCodeType.ConsultOfficeOutpatient.GetHashCode()
            clist.AssociatedCategory = "Consultations: Office of Other Outpatient"
            arrlist.Add(clist)

            clist = New myList
            clist.ID = AlphaII.CodeWizard.Objects.EvaluationManagement.EvalMgmtCodeType.ConsultInitialInpatient.GetHashCode()
            clist.AssociatedCategory = "Consultations: Inpatient"
            arrlist.Add(clist)

            clist = New myList
            clist.ID = AlphaII.CodeWizard.Objects.EvaluationManagement.EvalMgmtCodeType.NursingFacilityInitialCompAssesment.GetHashCode()
            clist.AssociatedCategory = "Nursing Facility: INITIAL Comprehensive Assesment"
            arrlist.Add(clist)

            clist = New myList
            clist.ID = AlphaII.CodeWizard.Objects.EvaluationManagement.EvalMgmtCodeType.NursingFacilitySubsequentCompAssessment.GetHashCode()
            clist.AssociatedCategory = "Nursing Facility: SUBSEQUENT"
            arrlist.Add(clist)

            clist = New myList
            clist.ID = AlphaII.CodeWizard.Objects.EvaluationManagement.EvalMgmtCodeType.HomeServicesNew.GetHashCode()
            clist.AssociatedCategory = "Home Services - New"
            arrlist.Add(clist)

            clist = New myList
            clist.ID = AlphaII.CodeWizard.Objects.EvaluationManagement.EvalMgmtCodeType.HomeServicesEstablished.GetHashCode()
            clist.AssociatedCategory = "Home Services - Established"
            arrlist.Add(clist)

            clist = New myList
            clist.ID = AlphaII.CodeWizard.Objects.EvaluationManagement.EvalMgmtCodeType.DomRestHomeCustCareServicesNew.GetHashCode()
            clist.AssociatedCategory = "Domiciliary, Rest Home, Custodial Care Services - New"
            arrlist.Add(clist)

            clist = New myList
            clist.ID = AlphaII.CodeWizard.Objects.EvaluationManagement.EvalMgmtCodeType.DomRestHomeCustCareServicesEstb.GetHashCode()
            clist.AssociatedCategory = "Domiciliary, Rest Home, Custodial Care Services - Estb"
            arrlist.Add(clist)

            chkEMVisitType.DataSource = arrlist
            chkEMVisitType.DisplayMember = "AssociatedCategory"
            chkEMVisitType.ValueMember = "ID"
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.EnM, gloAuditTrail.ActivityCategory.GenerateCode, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
        '''''''''''''''Added by Ujwala Atre as on 2010/07/28 - For EM Coding Changes - Admin Settings
    End Sub

    Private Sub SetEMSettings()
        '''''''''''''''Added by Ujwala Atre as on 2010/07/29 - For EM Coding Changes - Admin Settings
        Dim Obj As New clsSettings
        Dim Dt As DataTable
        Try
            Dim i As Int32
            Dim j As Int32
            Dim sval As String
            '''''''''
            Dt = Obj.GetEMSettings()
            '''''''''
            For i = 0 To Dt.Rows.Count - 1
                If Dt.Rows(i).Item("SettingsName") = "EMEXAMTYPES" Then
                    '''''''''
                    sval = "'" & Dt.Rows(i).Item("SettingsValue").ToString.Replace(",", "','") & "'"
                    For j = 0 To chkEMExamType.Items.Count - 1
                        If sval.Contains("'" & CType(chkEMExamType.Items(j), myList).ID.ToString() & "'") Then
                            chkEMExamType.SetItemChecked(j, True)
                        End If
                    Next
                    '''''''''
                Else
                    '''''''''
                    sval = "'" & Dt.Rows(i).Item("SettingsValue").ToString.Replace(",", "','") & "'"
                    For j = 0 To chkEMVisitType.Items.Count - 1
                        If sval.Contains("'" & CType(chkEMVisitType.Items(j), myList).ID.ToString() & "'") Then
                            chkEMVisitType.SetItemChecked(j, True)
                        End If
                    Next
                    '''''''''
                End If
            Next
            '''''''''           

        Catch ex As Exception
        Finally
            Obj = Nothing
            Dt = Nothing
        End Try
        '''''''''''''''Added by Ujwala Atre as on 2010/07/29 - For EM Coding Changes - Admin Settings
    End Sub
    'Shubhangi 20100105
    'Bind with AlphaII SDK & retrive all data related to ExamType.
    Public Sub FillExamTypeCombo()
        Try
            Dim clist As myList
            Dim arrlist As New List(Of myList)
            clist = New myList
            clist.ExamControlType = AlphaII.CodeWizard.Objects.EvaluationManagement.EvalMgmtExamType.Cardiovascular
            clist.AssociatedCategory = "Cardiovascular"
            arrlist.Add(clist)

            clist = New myList
            clist.ExamControlType = AlphaII.CodeWizard.Objects.EvaluationManagement.EvalMgmtExamType.EarsNoseThroat
            clist.AssociatedCategory = "Ears Nose Throat"
            arrlist.Add(clist)

            clist = New myList
            clist.ExamControlType = AlphaII.CodeWizard.Objects.EvaluationManagement.EvalMgmtExamType.Eye
            clist.AssociatedCategory = "Eye"
            arrlist.Add(clist)

            clist = New myList
            clist.ExamControlType = AlphaII.CodeWizard.Objects.EvaluationManagement.EvalMgmtExamType.GeneralMultiSystem
            clist.AssociatedCategory = "General Multiple System"
            arrlist.Add(clist)

            clist = New myList
            clist.ExamControlType = AlphaII.CodeWizard.Objects.EvaluationManagement.EvalMgmtExamType.Genitourinary
            clist.AssociatedCategory = "Genitourinary"
            arrlist.Add(clist)

            clist = New myList
            clist.ExamControlType = AlphaII.CodeWizard.Objects.EvaluationManagement.EvalMgmtExamType.HemaLymphImmuno
            clist.AssociatedCategory = "HemaLymphImmuno"
            arrlist.Add(clist)

            clist = New myList
            clist.ExamControlType = AlphaII.CodeWizard.Objects.EvaluationManagement.EvalMgmtExamType.Musculoskeletal
            clist.AssociatedCategory = "Musculoskeletal"
            arrlist.Add(clist)

            clist = New myList
            clist.ExamControlType = AlphaII.CodeWizard.Objects.EvaluationManagement.EvalMgmtExamType.Neurological
            clist.AssociatedCategory = "Neurological"
            arrlist.Add(clist)

            clist = New myList
            clist.ExamControlType = AlphaII.CodeWizard.Objects.EvaluationManagement.EvalMgmtExamType.None
            clist.AssociatedCategory = "None"
            arrlist.Add(clist)

            clist = New myList
            clist.ExamControlType = AlphaII.CodeWizard.Objects.EvaluationManagement.EvalMgmtExamType.Pre97Guidelines
            clist.AssociatedCategory = "Pre97Guidelines"
            arrlist.Add(clist)

            clist = New myList
            clist.ExamControlType = AlphaII.CodeWizard.Objects.EvaluationManagement.EvalMgmtExamType.Psychiatric
            clist.AssociatedCategory = "Psychiatric"
            arrlist.Add(clist)

            clist = New myList
            clist.ExamControlType = AlphaII.CodeWizard.Objects.EvaluationManagement.EvalMgmtExamType.Respiratory
            clist.AssociatedCategory = "Respiratory"
            arrlist.Add(clist)

            clist = New myList
            clist.ExamControlType = AlphaII.CodeWizard.Objects.EvaluationManagement.EvalMgmtExamType.Skin
            clist.AssociatedCategory = "Skin"
            arrlist.Add(clist)

            cmbEMExamType.DataSource = arrlist
            cmbEMExamType.DisplayMember = "AssociatedCategory"
            cmbEMExamType.ValueMember = "ExamControlType"
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.EnM, gloAuditTrail.ActivityCategory.GenerateCode, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub
    'End Shubhangi
    Private Function GetLoginName(ByVal UserID As Int64) As String
        Dim conn As New SqlConnection()
        Dim objCmd As SqlCommand
        Dim LoginName As String = ""
        Dim _strSQL As String = ""

        Try
            conn.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()

            conn.Open()
            _strSQL = "select isnull(sLoginName,'') as sLoginName from User_MST where nUserID = " & UserID
            objCmd = New SqlCommand(_strSQL, conn)
            LoginName = objCmd.ExecuteScalar()

            Return LoginName

        Catch ex As Exception

            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return ""
        Finally

            If IsNothing(conn) = False Then
                conn.Close()
                conn.Dispose()
                conn = Nothing
            End If

        End Try
    End Function

    Private Sub Fill_FaxUsers()
        Dim conn As New SqlConnection()
        Dim objdaUsers As SqlDataAdapter
        Dim dtUsers As DataTable
        Dim dtUsers1 As DataTable
        Dim dtUsers2 As DataTable
        Dim _strSQL As String = ""

        Try
            conn.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()
            conn.Open()
            ''Commented By Dhruv '' To only to show the Active users
            '_strSQL = "select nUserID , sLoginName from User_MST"
            _strSQL = "select nUserID , sLoginName from User_MST WHERE nBlockStatus = 0"
            objdaUsers = New SqlDataAdapter(_strSQL, conn)

            dtUsers = New DataTable
            dtUsers1 = New DataTable
            dtUsers2 = New DataTable
            objdaUsers.Fill(dtUsers)
            objdaUsers.Fill(dtUsers1)
            objdaUsers.Fill(dtUsers2)
            ''Added by Mayuri:20100309-To fix case No:#GLO2010-0004417
            ''Once you select a pending fax user and a received fax user you can remove your selection
            Dim blankRow As DataRow
            blankRow = dtUsers.NewRow()
            blankRow("nUserID") = 0
            blankRow("sLoginName") = ""
            dtUsers.Rows.Add(blankRow)

            cmbPendingFaxUser.DataSource = dtUsers
            cmbPendingFaxUser.DisplayMember = "sLoginName"
            cmbPendingFaxUser.ValueMember = "nUserID"


            ''Added by Mayuri:20100309-To fix case No:#GLO2010-0004417
            ''Once you select a pending fax user and a received fax user you can remove your selection
            Dim blankreceiveRow As DataRow
            blankreceiveRow = dtUsers1.NewRow()
            blankreceiveRow("nUserID") = 0
            blankreceiveRow("sLoginName") = ""
            dtUsers1.Rows.Add(blankreceiveRow)
            ''End code Added by Mayuri:20100309

            cmbRecieveFaxUser.DataSource = dtUsers1
            cmbRecieveFaxUser.DisplayMember = "sLoginName"
            cmbRecieveFaxUser.ValueMember = "nUserID"



            Dim blankInboxRow As DataRow
            blankInboxRow = dtUsers2.NewRow()
            blankInboxRow("nUserID") = 0
            blankInboxRow("sLoginName") = ""
            dtUsers2.Rows.Add(blankInboxRow)

            cmbInboxUser.DataSource = dtUsers2
            cmbInboxUser.DisplayMember = "sLoginName"
            cmbInboxUser.ValueMember = "nUserID"

        Catch sqlex As SqlException
            MessageBox.Show(sqlex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If

            If IsNothing(conn) = False Then
                conn.Dispose()
                conn = Nothing
            End If

            objdaUsers = Nothing
            dtUsers = Nothing
        End Try
    End Sub


    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Try
            ''Sandip Darade  20100217
            ''Case GLO2010-0004202

            Dim oResult As DialogResult
            oResult = MessageBox.Show("Do you want to save settings?", gstrMessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
            If oResult = Windows.Forms.DialogResult.Yes Then
                ''Added on 20100630 by sanjog
                'blnBtncloseclick = True
                If btnOk_Click_New() = False Then
                    blnBtncloseclick = False
                Else
                    blnBtncloseclick = True
                    sStagingURl = Nothing
                    sProductionURl = Nothing
                    s10dot6ProductionURl = Nothing
                    s10dot6StagingURl = Nothing
                    Me.Close()
                End If
                'If flagClose = True Then
                '    flagClose = False
                'Else
                '    Me.Close()
                'End If
                ''Added on 20100630 by sanjog
            ElseIf oResult = Windows.Forms.DialogResult.Cancel Then

            Else
                ''Added on 20100729 by sanjog
                blnBtncloseclick = True
                ''Added on 20100729 by sanjog
                Me.Close()
            End If
        Catch objErr As Exception
            Me.Cursor = Cursors.Default
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Dim _isOkClick As Boolean = False

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        Dim _result As Boolean = btnOk_Click_New()
        If _result = True Then
            _isOkClick = True
            Me.Close()
        Else
            _isOkClick = False
            Exit Sub
        End If
    End Sub




    Private Sub SaveEMSettings(ByRef dtsettings As DataTable)
        '''''''''''''''Added by Ujwala Atre as on 2010/07/28 - For EM Coding Changes - Admin Settings
        Dim obj As New clsSettings
        Try
            Dim i As Int16
            Dim ExmTp As String = ""
            '''''''''''
            For i = 0 To chkEMExamType.Items.Count - 1
                If chkEMExamType.GetItemCheckState(i) = CheckState.Checked Then
                    If ExmTp = "" Then
                        ExmTp = CType(chkEMExamType.Items(i), myList).ID
                    Else
                        ExmTp = ExmTp & "," & CType(chkEMExamType.Items(i), myList).ID
                    End If
                End If
            Next
            '  obj.AddEMSettings("EMExamTypes", ExmTp)
            dtsettings.Rows.Add("EMExamTypes", ExmTp, "EM Exam Types")
            '''''''''''            
            Dim VistTp As String = ""
            For i = 0 To chkEMVisitType.Items.Count - 1
                If chkEMVisitType.GetItemCheckState(i) = CheckState.Checked Then
                    If VistTp = "" Then
                        VistTp = CType(chkEMVisitType.Items(i), myList).ID
                    Else
                        VistTp = VistTp & "," & CType(chkEMVisitType.Items(i), myList).ID
                    End If
                End If
            Next
            '  obj.AddEMSettings("EMVisitType", VistTp)
            dtsettings.Rows.Add("EMVisitType", VistTp, "EM Visit Types")
            '''''''''''
        Catch ex As Exception
        Finally
            obj = Nothing
        End Try
        '''''''''''''''Added by Ujwala Atre as on 2010/07/28 - For EM Coding Changes - Admin Settings
    End Sub
    Private Sub chkEMExamType_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles chkEMExamType.MouseMove
        SetListBoxToolTip(chkEMExamType, C1SuperTooltip1, Me.MousePosition)
    End Sub

    Private Sub chkEMVisitType_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles chkEMVisitType.MouseMove
        SetListBoxToolTip(chkEMVisitType, C1SuperTooltip1, Me.MousePosition)
    End Sub

    Private Sub Panel117_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs)

    End Sub


#Region "New Code -- According to change"
    Private Function btnOk_Click_New() As Boolean
        Dim dtSaveSettings As DataTable
        Try
            'blnBtncloseclick = True

            Dim dtStart As Long
            Dim dtEnd As Long
            dtStart = New TimeSpan(tmStartTime.Value.Hour, tmStartTime.Value.Minute, tmStartTime.Value.Second).Ticks
            dtEnd = New TimeSpan(tmEndTime.Value.Hour, tmEndTime.Value.Minute, tmEndTime.Value.Second).Ticks
            If dtStart >= dtEnd Then
                MessageBox.Show("Clinic Start Time must be less than Clinic Closing Time.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '******By Sandip Deshmukh 26 Oct 2007 5.15PM
                '******the loc was added to set the tab item if causes validation
                'tb_Settings.SelectedIndex = 0
                tb_Settings.SelectTab(tb_Settings.TabPages.IndexOf(tbp_ClinicSettings))
                '****** 26 Oct 2007 5.15PM
                tmStartTime.Focus()
                Exit Function
            End If
            If AppointmentInterval.Value <= 0 Then
                MessageBox.Show("Appointment Interval must be greater than 0 minutes.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '******By Sandip Deshmukh 26 Oct 2007 5.15PM
                '******the loc was added to set the tab item if causes validation
                'tb_Settings.SelectedIndex = 0
                tb_Settings.SelectTab(tb_Settings.TabPages.IndexOf(tbp_ClinicSettings))
                '****** 26 Oct 2007 5.15PM
                AppointmentInterval.Focus()
                Exit Function
            End If
            If AppointmentInterval.Value Mod 5 <> 0 Then
                MessageBox.Show("Appointment Interval must be in multiple of 5 minutes.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '******By Sandip Deshmukh 26 Oct 2007 5.15PM
                '******the loc was added to set the tab item if causes validation
                'tb_Settings.SelectedIndex = 0
                tb_Settings.SelectTab(tb_Settings.TabPages.IndexOf(tbp_ClinicSettings))
                '****** 26 Oct 2007 5.15PM
                AppointmentInterval.Focus()
                Exit Function
            End If
            If PullChartsInterval.Value Mod 5 <> 0 Then
                MessageBox.Show("Pull Charts Interval must be in multiple of 5 minutes.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '******By Sandip Deshmukh 26 Oct 2007 5.15PM
                '******the loc was added to set the tab item if causes validation
                'tb_Settings.SelectedIndex = 0
                tb_Settings.SelectTab(tb_Settings.TabPages.IndexOf(tbp_ClinicSettings))
                '****** 26 Oct 2007 5.15PM
                PullChartsInterval.Focus()
                Exit Function
            End If
            If numMaxNoOfRetries.Value < 3 Or numMaxNoOfRetries.Value > 20 Then
                MessageBox.Show("Maximum No of Retries for FAX must be between 3 to 20.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '******By Sandip Deshmukh 26 Oct 2007 5.15PM
                '******the loc was added to set the tab item if causes validation
                'tb_Settings.SelectedIndex = 1
                tb_Settings.SelectTab(tb_Settings.TabPages.IndexOf(tbp_FaxSettings))
                '****** 26 Oct 2007 5.15PM
                numMaxNoOfRetries.Focus()
                Exit Function
            End If
            If numFAXRetryInterval.Value <= 0 Or numFAXRetryInterval.Value >= 500 Then
                MessageBox.Show("FAX Retry Interval must be between 1 to 500 minutes.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '******By Sandip Deshmukh 26 Oct 2007 5.15PM
                '******the loc was added to set the tab item if causes validation
                'tb_Settings.SelectedIndex = 1
                tb_Settings.SelectTab(tb_Settings.TabPages.IndexOf(tbp_FaxSettings))
                '****** 26 Oct 2007 5.15PM
                numFAXRetryInterval.Focus()
                Exit Function
            End If
            ''Start :: Dhruv
            '''''''''''''''''''''
            ''''''Added by Ujwala for Customized SSRS Reports - as on 20101025
            If (txtReportServerName.Text.ToString() = "" Or txtCustomRptFolderNm.Text.ToString() = "") Then
                If (txtReportServerName.Text.ToString() = "") Then
                    MessageBox.Show("Enter Report Server Name.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    tb_Settings.SelectTab(tb_Settings.TabPages.IndexOf(tbpg_ReportServerSettings))
                    txtReportServerName.Focus()
                    Exit Function
                End If
                If (txtCustomRptFolderNm.Text.ToString() = "") Then
                    MessageBox.Show("Enter Customized Report Folder Name.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    tb_Settings.SelectTab(tb_Settings.TabPages.IndexOf(tbpg_ReportServerSettings))
                    txtCustomRptFolderNm.Focus()
                    Exit Function
                End If
            End If
            ''''''Added by Ujwala for Customized SSRS Reports - as on 20101025

            '''''''''''''''''''''
            ''End :: dhruv
            'start of code commented by manoj jadhav on 20111017 for New consolidated UI to device settings
            'If chkUseSpirometryDevice.Checked Then
            '    If txtprefixForSpirometryDevice.Text.Trim = String.Empty Then
            '        MessageBox.Show("Enter Prefix for Spirometry Device Order.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '        tb_Settings.SelectTab(tb_Settings.TabPages.IndexOf(tbpg_InterfaceSettings))
            '        txtprefixForSpirometryDevice.Focus()
            '        Exit Function
            '    End If
            'Else
            '    txtprefixForSpirometryDevice.Text = sSpirometryDeviceOrderPrefix
            'End If
            'end of code commented by manoj jadhav on 20111017 for New consolidated UI to device settings

            'If (chkInternetFax.Checked = True) Then
            '    If txteFaxUserID.Text.Trim = "" Then
            '        MessageBox.Show("Please enter user ID for eFax .", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '        txteFaxUserID.Focus()
            '        tb_Settings.SelectedIndex = 7
            '        Exit Sub
            '    End If
            '    If txteFaxPassword.Text.Trim = "" Then
            '        MessageBox.Show("Please enter Password for eFax .", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '        tb_Settings.SelectedIndex = 7
            '        txteFaxPassword.Focus()
            '        Exit Sub
            '    End If
            'End If

            '//////////previously the threshold value was 420 mins bydefault. 
            '////////////it is changed to 840 min on 31 Oct'08 friday.
            'code added by sagar on 31 july 2007 for threshold value
            If Trim(txtThresholdValue.Text).Length <> 0 Then
                If Val(Trim(txtThresholdValue.Text)) = 0 Then
                    MessageBox.Show("Threshold value should be minimum 1 minute.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '******By Sandip Deshmukh 26 Oct 2007 5.15PM
                    '******the loc was added to set the tab item if causes validation
                    'tb_Settings.SelectedIndex = 0
                    tb_Settings.SelectTab(tb_Settings.TabPages.IndexOf(tbpg_SurescriptSettings))
                    '****** 26 Oct 2007 5.15PM
                    txtThresholdValue.Focus()
                    Exit Function

                    'sarika 2nd june 08 -- threshhold value validation
                Else
                    If Val(Trim(txtThresholdValue.Text)) < 1 Then
                        MessageBox.Show("Threshold value should be minimum 1 minute.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        '******By Sandip Deshmukh 26 Oct 2007 5.15PM
                        '******the loc was added to set the tab item if causes validation
                        'tb_Settings.SelectedIndex = 0
                        tb_Settings.SelectTab(tb_Settings.TabPages.IndexOf(tbpg_SurescriptSettings))
                        '****** 26 Oct 2007 5.15PM
                        txtThresholdValue.Focus()
                        Exit Function
                    End If
                    If Val(Trim(txtThresholdValue.Text)) > 840 Then
                        MessageBox.Show("Threshold value should be maximum 840 minutes.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        '******By Sandip Deshmukh 26 Oct 2007 5.15PM
                        '******the loc was added to set the tab item if causes validation
                        'tb_Settings.SelectedIndex = 0
                        '****** 26 Oct 2007 5.15PM
                        txtThresholdValue.Text = 840
                        tb_Settings.SelectTab(tb_Settings.TabPages.IndexOf(tbpg_SurescriptSettings))
                        txtThresholdValue.Focus()
                        Exit Function
                    End If

                    'sarika 2nd june 08 -- threshhold value validation
                End If

            Else
                MessageBox.Show("Default Threshold value 840 minutes will be saved as no value has been entered.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If

            'Added by mitesh
            If Val(Trim(txtRxEligibilitythreshold.Text)) < 72 Then
                MessageBox.Show("Rx Eligibility Threshold Value should be minimum 72 hours.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                tb_Settings.SelectTab(tb_Settings.TabPages.IndexOf(tbpg_SurescriptSettings))
                txtRxEligibilitythreshold.Focus()
                Exit Function
            End If


            'sarika UseFaxNoPrefix 8th may 08
            If chkUseFaxNoPrefix.Checked = True Then
                If (Trim(txtFaxNoPrefix.Text)) = "" Then
                    MessageBox.Show("Enter the Fax Number Prefix.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    tb_Settings.SelectTab(tb_Settings.TabPages.IndexOf(tbp_FaxSettings))
                    txtFaxNoPrefix.Focus()
                    Exit Function
                End If
            End If
            '---------sarika UseFaxNoPrefix 8th may 08

            ''Start :: Dhruv 
            If (txtSequentialPatientCode.Text.Trim() = "") Then
                MessageBox.Show("Enter sequential patient code.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                tb_Settings.SelectTab(tb_Settings.TabPages.IndexOf(tbp_ClinicSettings))
                txtSequentialPatientCode.Focus()
                Exit Function
            End If

            If chkSurescript.Checked = True Then
                If TxtSurescriptURL.Text.Trim = "" Then
                    MessageBox.Show("Enter eRx Web Service.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    tb_Settings.SelectTab(tb_Settings.TabPages.IndexOf(tbpg_SurescriptSettings))
                    TxtSurescriptURL.Focus()
                    Exit Function
                End If
            End If
            'Added EnableStaticColor Setting
            If chkEnableStaticColor.Checked = True Then
                _isEnableStaticColor = True
            Else
                _isEnableStaticColor = False
            End If
            If optClinicDIYes.Checked = True Then
                If txtDIServiceURL.Text.Trim = "" Then
                    MessageBox.Show("Enter Drug Interaction Web Service URL.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    tb_Settings.SelectTab(tb_Settings.TabPages.IndexOf(tbpg_SurescriptSettings))
                    txtDIServiceURL.Focus()
                    Exit Function
                End If
            End If
            If txtDIBServiceURL.Text.Trim = "" Then
                MessageBox.Show("Enter DIB Web Service URL.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                tb_Settings.SelectTab(tb_Settings.TabPages.IndexOf(tbpg_SurescriptSettings))
                txtDIBServiceURL.Focus()
                Exit Function
            End If

            If String.IsNullOrEmpty(txtePAServiceURL.Text) Then
                MessageBox.Show("Enter ePA Web Service URL.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                tb_Settings.SelectTab(tb_Settings.TabPages.IndexOf(tbpg_SurescriptSettings))
                txtePAServiceURL.Focus()
                Exit Function
            End If

            If chkSecureMesaage.Checked = True Then
                If rbSecureStaging.Checked = False And rbSecureProduction.Checked = False Then
                    MessageBox.Show("Please select which server do you want to connect.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    tb_Settings.SelectTab(tb_Settings.TabPages.IndexOf(tbpg_SurescriptSettings))

                    Exit Function
                End If

                If txtSecureMessageURL.Text.Trim = "" Then
                    MessageBox.Show("Enter Secure Messageing Web Service Url.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    tb_Settings.SelectTab(tb_Settings.TabPages.IndexOf(tbpg_SurescriptSettings))
                    txtSecureMessageURL.Focus()
                    Exit Function
                End If
            End If

            dtSaveSettings = New DataTable
            dtSaveSettings.Columns.Clear()
            dtSaveSettings.Columns.Add("sSettingsName")
            dtSaveSettings.Columns.Add("sSettingsValue")
            dtSaveSettings.Columns.Add("sSettingsUserDescription")
            dtSaveSettings.Columns.Add("nClinicID")
            dtSaveSettings.Columns.Add("nUserID")

            dtSaveSettings.Columns("nUserID").DefaultValue = 0
            dtSaveSettings.Columns("nClinicID").DefaultValue = gnClinicID

            ''End :: Dhruv
            'start of code commented by manoj jadhav on 20111017 for New consolidated UI to device settings
            ''Code Added by Shirish
            'If ChkUseCardioScienceECGDevice.Checked Then
            '    'If ECG device interface has been enabled then check for all ECG settings values are present.
            '    If txtECGInterfaceId.Text.Trim().Length = 0 Then
            '        MessageBox.Show("Please enter ECG interface institution id", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            '        tb_Settings.SelectedTab = tb_Settings.TabPages("tbpg_InterfaceSettings")
            '        txtECGInterfaceId.Focus()
            '        Exit Function
            '    End If
            '    If txtECGInterfaceUrl.Text.Trim().Length = 0 Then
            '        MessageBox.Show("Please enter ECG inerface url", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            '        tb_Settings.SelectedTab = tb_Settings.TabPages("tbpg_InterfaceSettings")
            '        txtECGInterfaceUrl.Focus()
            '        Exit Function
            '    End If
            '    If txtECGUserProviderId.Text.Trim().Length = 0 Then
            '        MessageBox.Show("Please enter ECG User Provider Id ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            '        tb_Settings.SelectedTab = tb_Settings.TabPages("tbpg_InterfaceSettings")
            '        txtECGUserProviderId.Focus()
            '        Exit Function
            '    End If
            'End If
            'end of code commented by manoj jadhav on 20111017 for New consolidated UI to device settings

            'Commented by RK on 20110521 
            ''Added by Abhijeet on  20110407
            'If chkUseVitalDevice.Checked Then
            '    If txtAusUserName.Text.Trim.Length = 0 Then
            '        MessageBox.Show("Please enter AUS user name in clinic settings to proceed interface activation", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            '        tb_Settings.SelectTab(tb_Settings.TabPages.IndexOf(tbpg_InterfaceSettings))
            '        txtAusUserName.Focus()
            '        Exit Function
            '    End If

            '    If txtVitalDeviceKey.Text.Trim.Length = 0 Then
            '        MessageBox.Show("Please enter vital device interface activation key", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            '        tb_Settings.SelectTab(tb_Settings.TabPages.IndexOf(tbpg_InterfaceSettings))
            '        txtVitalDeviceKey.Focus()
            '        Exit Function
            '    Else
            '        Dim objEncr As New clsEncryption()
            '        If Not (objEncr.EncryptToBase64String(String.Concat(txtAusUserName.Text.Trim.ToLower, "gL0@PPs2k9!"), "87654321") = txtVitalDeviceKey.Text.Trim) Then
            '            MessageBox.Show("Please enter valid vital device interface activation key", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            '            tb_Settings.SelectTab(tb_Settings.TabPages.IndexOf(tbpg_InterfaceSettings))
            '            txtVitalDeviceKey.Focus()
            '            Exit Function
            '        End If
            '    End If            
            'End If
            ''End of changes by Abhijeet on 20110407

            'start of code commented by manoj jadhav on 20120730 for moving service setiings to Respective Service
            ' ''Added by Abhijeet on  20110422 & 20110425
            'If chkHL7ImmunizationRegistry.Checked Then
            '    Dim strSiteID As String = GetClinicInformation("sSiteID")
            '    If strSiteID = "" Then
            '        If MessageBox.Show("Enter ‘Site Id’ under clinic information. This is mandatory information to send immunization registry details out in HL7 file format. Do you want to disable immunization registry generation in HL7 format and continue?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information) = Windows.Forms.DialogResult.Yes Then
            '            chkHL7ImmunizationRegistry.Checked = False
            '        Else
            '            tb_Settings.SelectTab(tb_Settings.TabPages.IndexOf(tbp_OtherSettings))
            '            chkHL7ImmunizationRegistry.Focus()
            '            Exit Function
            '        End If
            '    End If

            '    If txtRegistryExpFilePath.Text.Trim = "" Then
            '        MessageBox.Show("Enter immunization registry export file path. ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            '        tb_Settings.SelectTab(tb_Settings.TabPages.IndexOf(tbp_OtherSettings))
            '        txtRegistryExpFilePath.Focus()
            '        Exit Function
            '    End If

            '    If chkExportFiletoRegistry.Checked Then

            '        If txtRegisrtyExportURL.Text.Trim = "" Then
            '            MessageBox.Show("Enter immunization registry URL. ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            '            tb_Settings.SelectTab(tb_Settings.TabPages.IndexOf(tbp_OtherSettings))
            '            txtRegisrtyExportURL.Focus()
            '            Exit Function
            '        End If

            '        If txtRegisrtyUserId.Text.Trim = "" Then
            '            MessageBox.Show("Enter immunization registry user ID. ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            '            tb_Settings.SelectTab(tb_Settings.TabPages.IndexOf(tbp_OtherSettings))
            '            txtRegisrtyUserId.Focus()
            '            Exit Function
            '        End If

            '        If txtRegisrtyPassword.Text.Trim = "" Then
            '            MessageBox.Show("Enter immunization registry password. ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            '            tb_Settings.SelectTab(tb_Settings.TabPages.IndexOf(tbp_OtherSettings))
            '            txtRegisrtyPassword.Focus()
            '            Exit Function
            '        End If

            '        If txtRegisrtyFacilityID.Text.Trim = "" Then
            '            MessageBox.Show("Enter immunization registry facility ID. ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            '            tb_Settings.SelectTab(tb_Settings.TabPages.IndexOf(tbp_OtherSettings))
            '            txtRegisrtyFacilityID.Focus()
            '            Exit Function
            '        End If
            '    End If

            'End If

            ' ''End of changes by Abhijeet on 20110422 & 20110425
            'end of code commented by manoj jadhav on 20120730 for moving service setiings to Respective Service

            'sarika PM Credentials validation 20081128
            Dim objSQLSettings As New clsStartUpSettings
            ''Sandip Darade 20090731
            Dim str = ""
            If (gstrAdminFor = "gloPM") Then
                str = "gloEMR"
            Else
                str = "gloPM"
            End If
            ''Added by Mayuri:20100608
            ''

            ''
            Dim j As Int16
            Dim k As Integer
            Dim _VitalSettingsValue As String = ""
            If Not IsNothing(trvSelectedVitals) Then
                ''Added by Sanjog:20100609
                For j = 0 To trvSelectedVitals.Nodes.Count - 1

                    If trvSelectedVitals.Nodes(j).Nodes.Count <> 0 Then
                        If trvSelectedVitals.Nodes(j).Checked = True Then
                            'If _VitalSettingsValue = "" Then
                            '    _VitalSettingsValue = trvSelectedVitals.Nodes(j).Tag.ToString()
                            'Else
                            '    _VitalSettingsValue = _VitalSettingsValue & "," & trvSelectedVitals.Nodes(j).Tag.ToString()
                            'End If
                            For k = 0 To trvSelectedVitals.Nodes(j).Nodes.Count - 1
                                If (trvSelectedVitals.Nodes(j).Text = "Pulse OX") Then
                                    If (trvSelectedVitals.Nodes(j).Nodes(k).Checked = True) Then
                                        If _VitalSettingsValue = "" Then
                                            _VitalSettingsValue = j & "." & 0 & "-" & trvSelectedVitals.Nodes(j).Tag.ToString()
                                            _VitalSettingsValue = _VitalSettingsValue & "," & j & "." & (k + 1) & "-" & trvSelectedVitals.Nodes(j).Nodes(k).Tag.ToString()
                                        Else
                                            _VitalSettingsValue = _VitalSettingsValue & "," & j & "." & 0 & "-" & trvSelectedVitals.Nodes(j).Tag.ToString()
                                            _VitalSettingsValue = _VitalSettingsValue & "," & j & "." & (k + 1) & "-" & trvSelectedVitals.Nodes(j).Nodes(k).Tag.ToString()
                                        End If
                                    Else
                                        If _VitalSettingsValue = "" Then
                                            _VitalSettingsValue = j & "." & 0 & "-" & trvSelectedVitals.Nodes(j).Tag.ToString()
                                        Else
                                            _VitalSettingsValue = _VitalSettingsValue & "," & j & "." & (0) & "-" & trvSelectedVitals.Nodes(j).Tag.ToString()
                                        End If

                                    End If
                                Else
                                    If _VitalSettingsValue = "" Then
                                        _VitalSettingsValue = j & "." & (k + 1) & "-" & trvSelectedVitals.Nodes(j).Nodes(k).Tag.ToString()
                                    Else
                                        _VitalSettingsValue = _VitalSettingsValue & "," & j & "." & (k + 1) & "-" & trvSelectedVitals.Nodes(j).Nodes(k).Tag.ToString()
                                    End If
                                End If

                            Next
                        Else
                            For k = 0 To trvSelectedVitals.Nodes(j).Nodes.Count - 1
                                If trvSelectedVitals.Nodes(j).Nodes(k).Checked = True Then
                                    If _VitalSettingsValue = "" Then
                                        _VitalSettingsValue = j & "." & (k + 1) & "-" & trvSelectedVitals.Nodes(j).Nodes(k).Tag.ToString()
                                    Else
                                        _VitalSettingsValue = _VitalSettingsValue & "," & j & "." & (k + 1) & "-" & trvSelectedVitals.Nodes(j).Nodes(k).Tag.ToString()
                                    End If
                                End If
                            Next
                        End If
                    Else
                        If trvSelectedVitals.Nodes(j).Checked = True Then
                            If _VitalSettingsValue = "" Then
                                _VitalSettingsValue = j & "." & 0 & "-" & trvSelectedVitals.Nodes(j).Tag.ToString()
                            Else
                                _VitalSettingsValue = _VitalSettingsValue & "," & j & "." & 0 & "-" & trvSelectedVitals.Nodes(j).Tag.ToString()
                            End If
                        End If
                    End If

                Next
                ''Added by Sanjog:20100609
            End If
            If _VitalSettingsValue = "" Then

                ''Added on 20100630 by sanjog
                MessageBox.Show("Select at least one vital item", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                'tb_Settings.SelectedTab = tb_Settings.TabPages("tbpg_VitalsCustomizationSettings")
                tb_Settings.SelectTab(tb_Settings.TabPages.IndexOf(tbpg_VitalsCustomizationSettings))
                flagClose = True
                'blnBtncloseclick = False
                Exit Function
                ''Added on 20100630 by sanjog
            Else
                flagClose = False
            End If
            ''End code Added by Mayuri:20100608
            ''Added by Mayuri:20110912-CCD Settings

            'Developer: Mitesh Patel
            'Date:28-Dec-2011'
            'PRD: OB Vitals Customization
            'Commented on 20120125
            ''--------
            Dim _OBVitalSettingsValue As String = ""
            'If Not IsNothing(trv_OBVitals) Then

            '    For j = 0 To trv_OBVitals.Nodes.Count - 1

            '        If trv_OBVitals.Nodes(j).Nodes.Count <> 0 Then
            '            If trv_OBVitals.Nodes(j).Checked = True Then

            '                For k = 0 To trv_OBVitals.Nodes(j).Nodes.Count - 1
            '                    If _OBVitalSettingsValue = "" Then
            '                        _OBVitalSettingsValue = j & "." & (k + 1) & "#" & trv_OBVitals.Nodes(j).Nodes(k).Tag.ToString()
            '                    Else
            '                        _OBVitalSettingsValue = _OBVitalSettingsValue & "," & j & "." & (k + 1) & "#" & trv_OBVitals.Nodes(j).Nodes(k).Tag.ToString()
            '                    End If

            '                Next
            '            Else
            '                For k = 0 To trv_OBVitals.Nodes(j).Nodes.Count - 1
            '                    If trv_OBVitals.Nodes(j).Nodes(k).Checked = True Then
            '                        If _OBVitalSettingsValue = "" Then
            '                            _OBVitalSettingsValue = j & "." & (k + 1) & "#" & trv_OBVitals.Nodes(j).Nodes(k).Tag.ToString()
            '                        Else
            '                            _OBVitalSettingsValue = _OBVitalSettingsValue & "," & j & "." & (k + 1) & "#" & trv_OBVitals.Nodes(j).Nodes(k).Tag.ToString()
            '                        End If
            '                    End If
            '                Next
            '            End If
            '        Else
            '            If trv_OBVitals.Nodes(j).Checked = True Then
            '                If _OBVitalSettingsValue = "" Then
            '                    _OBVitalSettingsValue = j & "." & 0 & "#" & trv_OBVitals.Nodes(j).Tag.ToString()
            '                Else
            '                    _OBVitalSettingsValue = _OBVitalSettingsValue & "," & j & "." & 0 & "#" & trv_OBVitals.Nodes(j).Tag.ToString()
            '                End If
            '            End If
            '        End If

            '    Next

            'End If
            'If _OBVitalSettingsValue = "" Then

            '    MessageBox.Show("Select at least one OB vital item", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)

            '    tb_Settings.SelectTab(tb_Settings.TabPages.IndexOf(tbpg_OBVitalCustomizationSettings))
            '    flagClose = True

            '    Exit Function

            'Else
            '    flagClose = False
            'End If
            ''----x-----


            ''Clinical chart Setting
            Dim _sClinicalChartValue As String = getClinicalChartvalue()
            ''''---
            If _sClinicalChartValue = "" Then
                MessageBox.Show("Select at least one Clinical Chart item.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                tb_Settings.SelectTab(tb_Settings.TabPages.IndexOf(tbpg_ClinicalChart))
                flagClose = True
                Exit Function
            Else
                flagClose = False
            End If

            If chk_FaxExceeds.Checked = True Then
                If txtFaxExceeds.Text.Trim() = "" Or Val(txtFaxExceeds.Text) = 0 Then
                    MessageBox.Show("'Warn user if Fax exceeds pages' cannot be zero or blank.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    tb_Settings.SelectTab(tb_Settings.TabPages.IndexOf(tbp_FaxSettings))
                    txtFaxExceeds.Focus()
                    flagClose = True
                    Exit Function
                End If

            Else
                flagClose = False
            End If

            If chk_AllowFaxes.Checked = True Then
                If txtAllowFaxes.Text.Trim() = "" Or Val(txtAllowFaxes.Text) = 0 Then
                    MessageBox.Show("'Do not allow Faxes longer than pages' cannot be zero or blank.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    tb_Settings.SelectTab(tb_Settings.TabPages.IndexOf(tbp_FaxSettings))
                    txtAllowFaxes.Focus()
                    flagClose = True
                    Exit Function
                End If

            Else
                flagClose = False
            End If

            If chk_FaxExceeds.Checked = True And chk_AllowFaxes.Checked = True Then
                If Val(txtFaxExceeds.Text) > Val(txtAllowFaxes.Text) Then
                    MessageBox.Show("‘Warn user if Fax exceeds’ cannot be greater than ‘Do not allow faxes longer than’ limit.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    tb_Settings.SelectTab(tb_Settings.TabPages.IndexOf(tbp_FaxSettings))
                    txtFaxExceeds.Focus()
                    flagClose = True
                    Exit Function
                End If

            Else
                flagClose = False
            End If



            CCDSection = checkCCDSections()
            CCDSectionVisit = checkCCDSectionsVisit()

            ''"START CCDA for Portal"

            If rbClinicalSummary.Checked = True Then
                ClinicalCCDASection = checkClinicalCCDASection()
                AmbulatoryCCDASection = tmpSelectedCCDAAmbulatory
            Else
                AmbulatoryCCDASection = checkAmbulatoryCCDASection()
                ClinicalCCDASection = tmpSelectedCCDAClinical
            End If
            'ClinicalCCDASection = checkClinicalCCDASection()
            'AmbulatoryCCDASection = checkAmbulatoryCCDASection()
            ''"END CCDA for Portal"
            CCDAImportCategory = getCCDAImportCategory()
            ''End Added by Mayuri:20110912-CCD Settings
            If chk_PMDBSettings.Checked = True Then

                If txtPMServerName.Text.Trim = "" Then
                    MessageBox.Show("Enter  " + str + " Server Name.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Function
                End If

                If txtPMDatabaseName.Text.Trim = "" Then
                    MessageBox.Show("Enter  " + str + " Database Name.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Function
                End If

                If optSQLAuthentication.Checked = True Then
                    If txtSQLUserID.Text.Trim = "" Then
                        MessageBox.Show("Enter SQL User ID for " + str + ".", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Function
                    End If

                    If txtSQLPassword.Text.Trim = "" Then
                        MessageBox.Show("Enter SQL Password for " + str + ".", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Function
                    End If

                    If objSQLSettings.IsSQLConnect(txtPMServerName.Text.Trim, txtPMDatabaseName.Text.Trim, txtSQLUserID.Text.Trim, txtSQLPassword.Text.Trim) = False Then
                        If MessageBox.Show("Unable to connect to SQL Server." & txtPMServerName.Text.Trim & " and Database " & txtPMDatabaseName.Text.Trim & vbCrLf & "Select valid credentials.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information) Then
                            Exit Function
                        End If
                    End If
                Else
                    If objSQLSettings.IsConnect(txtPMServerName.Text.Trim, txtPMDatabaseName.Text.Trim, False, "", "") = False Then
                        If MessageBox.Show("Unable to connect to SQL Server." & txtPMServerName.Text.Trim & " and Database " & txtPMDatabaseName.Text.Trim & vbCrLf & "Select valid credentials.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information) Then
                            Exit Function
                        End If
                    End If
                End If

            End If


            '---

            'sarika efax SendEMail Setting 20090502
            'If chkSendMail.Checked = True Then
            '    If txtSendMail.Text.Trim = "" Then
            '        MessageBox.Show("Please enter email address for sending failure notifications for faxes send from Internet Fax Service.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '        txtSendMail.Focus()
            '        Exit Sub
            '    End If
            'End If
            '--


            'sarika Advance Rx setting
            If chkAdvanceRx.Checked = True Then

                '\\commented on 20090820:
                ''If txtEARDirectory.Text.Trim() = "" Then
                ''    MessageBox.Show("Please select ePrescribing Report Directory.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                ''    btnBrowseEAR.Focus()
                ''    Exit Sub
                ''Else
                ''    'check whether the directory is valid
                ''    If Directory.Exists(txtEARDirectory.Text.Trim) = False Then
                ''        MessageBox.Show("The directory is Invalid. Please select valid ePrescribing Report Directory.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                ''        btnBrowseEAR.Focus()
                ''        Exit Sub
                ''    End If
                ''End If

                '\\20090819 RxHub participantID & password
                If txtParticipantID.Text.Trim = "" Then
                    MessageBox.Show("Enter RxHUB Participant ID for gloEMR.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    tb_Settings.SelectTab(tb_Settings.TabPages.IndexOf(tbpg_SurescriptSettings))
                    txtParticipantID.Focus()
                    Exit Function
                End If

                If txtRxPswd.Text.Trim = "" Then
                    MessageBox.Show("Enter RxHUB Password for gloEMR.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    tb_Settings.SelectTab(tb_Settings.TabPages.IndexOf(tbpg_SurescriptSettings))
                    txtRxPswd.Focus()
                    Exit Function
                End If
            End If
            '--
            If (gstrAdminFor = "gloPM") Then
                If (ValidateAlphaIISettings() = False) Then
                    Exit Function
                End If
            End If


            If ValidateOBSetting() Then
                Exit Function
            End If




            If (txtPatientCodePrefix.Text.Trim().Length <> 3 And txtPatientCodePrefix.Text.Trim() <> "") Then
                MessageBox.Show("Patient code prefix must be 3 characters.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                tb_Settings.SelectTab(tb_Settings.TabPages.IndexOf(tbp_ClinicSettings))
                txtPatientCodePrefix.Focus()
                Exit Function
            End If
            'Try  ''sudhir 20081124 check for age limit days ''should be greater according to months.
            '    If chk_AgeFlag.Checked = True Then
            '        If Val(txtAgeLimitPatientStrip.Text) < (Val(txtAgeLimitf.orWeeks.Text) * 30.4375) Then
            '            MessageBox.Show("Age Limit for day must be greater than " & CType(CType(Val(txtAgeLimitforWeeks.Text) * 30.4375, Int64), String) & " days", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            '            txtAgeLimitPatientStrip.Focus()
            '            Exit Sub
            '        End If
            '    End If
            'Catch ex As Exception
            'End Try
            ''Sandip Darade 20091107
            If (gstrAdminFor = "gloPM") Then
                SavePaymentSetting()
            End If
            Me.Cursor = Cursors.WaitCursor
            Dim objSettings As New clsSettings

            ''Auto Complete Tasks on Acknowledgement
            objSettings.isAutoCompleteTaskAck = ChkTasksAcknowledgement.Checked

            'DMS Auto Complete Tasks on Acknowledgement
            objSettings.isAutoCompleteDMSTaskAck = ChkDMSTasksAcknowledgement.Checked

            'RCM Auto Complete Tasks on Acknowledgement
            objSettings.isAutoCompleteRCMTaskAck = ChkRCMTasksAcknowledgement.Checked

            'Added EnableStaticColor Setting
            objSettings.isEnableStatusColor = chkEnableStaticColor.Checked



            ''Infobutton URL

            If txtInfoButtonURL.Text.Trim() = "" Then
                MessageBox.Show("Enter Infobutton URL.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                tb_Settings.SelectTab(tb_Settings.TabPages.IndexOf(tbp_OtherSettings))
                txtInfoButtonURL.Focus()
                Exit Function
            Else
                objSettings.InfobuttonURL = txtInfoButtonURL.Text
            End If

            If txtOpenInfobuttonURL.Text.Trim() = "" Then
                MessageBox.Show("Enter Open Infobutton URL.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                tb_Settings.SelectTab(tb_Settings.TabPages.IndexOf(tbp_OtherSettings))
                txtOpenInfobuttonURL.Focus()
                Exit Function
            Else
                objSettings.OpenInfobuttonURL = txtOpenInfobuttonURL.Text
            End If


            ''MU2 Features settings
            objSettings.globlnEnableMultipleRaceFeatures = chkMU2Features.Checked

            '04-Dec-14 Aniket: Exclude Non NDC Drugs From Erx Measure. Mail by Phill with subject ' prescriptions without an NCD code: OTC'S and Neutraceuticals'
            objSettings.ExcludeNonNDCDrugsFromErxMeasure = chkExcludeNonNDCDrugs.Checked
            ''

            '' -- Exclude Control Substance Drugs From Erx Measure
            objSettings.ExcludeControlSubstanceDrugsFromErxMeasure = chkExcludeControlSubstanceDrugs.Checked
            ''----x---

            objSettings.EnableSpecializedRegistryReporting = chkEnableSpecializedRegistryReporting.Checked

            objSettings.VitalSettingsValue = _VitalSettingsValue

            ''OB Vitals Settings
            objSettings.OBVitalSettingsValue = _OBVitalSettingsValue

            ''ClinicalChart Settings
            objSettings.ClinicalChartSettingsValue = _sClinicalChartValue

            objSettings.FaxExceedsValue = txtFaxExceeds.Text
            objSettings.AllowFaxesValue = txtAllowFaxes.Text

            ''------xx---
            objSettings.FullCCDSettingsValue = CCDSection
            objSettings.MUSectionsValue = chkMUSections.Checked

            objSettings.CCDAAutoDelete = chkCCDAAutoDelete.Checked

            objSettings.visitCCDSettings = CCDSectionVisit

            ''"START CCDA for Portal"
            objSettings.clinicalCCDASettings = ClinicalCCDASection
            objSettings.ambulatoryCCDASettings = AmbulatoryCCDASection
            ''"END CCDA for Portal"
            objSettings.CCDAImportCategorySettings = getCCDAImportCategory()
            objSettings.CCDAViewerURl = txtCDAViewer.Text
            objSettings.AppointmentStartTime = tmStartTime.Value.ToShortTimeString()
            objSettings.AppointmentEndTime = tmEndTime.Value.ToShortTimeString()
            objSettings.AppointmentInterval = AppointmentInterval.Value


            'added by Amit B
            If chbox_restrictedTmpAptmnt.Checked = True Then
                objSettings.RestrictedTemplateAppointtment = 1
            Else
                objSettings.RestrictedTemplateAppointtment = 0
            End If

            If rdoOverlapYes.Checked = True Then
                objSettings.OverlapTemplateAppointment = "Y"
            ElseIf rdoOverlapNo.Checked = True Then
                objSettings.OverlapTemplateAppointment = "N"
            ElseIf rdoOverlapUser.Checked = True Then
                objSettings.OverlapTemplateAppointment = "U"
            End If


            objSettings.PULLCHARTSInterval = PullChartsInterval.Value
            objSettings.MaxNoOfFAXRetries = numMaxNoOfRetries.Value
            objSettings.FAXRetryInterval = numFAXRetryInterval.Value
            objSettings.HPIEnabled = optHPIYes.Checked
            objSettings.LocationAddressed = optLocationAddressedYes.Checked
            objSettings.FAXCompression = cmbFAXCompression.Text
            objSettings.FAXSpeakerVoulme = cmbSpeakerVolume.Text
            objSettings.FAXReceiveEnabled = optFAXreceiveYes.Checked

            objSettings.OMRCategoryHistory = cmbOMRCategoryHistory.Text
            objSettings.OMRCategoryROS = cmbOMRCategoryROS.Text
            objSettings.OMRCategoryPatientRegistration = cmbOMRCategoryPatientRegistration.Text
            objSettings.OMRCategoryDirective = cmbCategoryDirective.Text

            UpdateUniqueidentifierPatient()

            'Developer: Mitesh Patel
            'Date:17-Jan-2012
            'PRD: Immunizations
            objSettings.CategoryVIS = cmbVISCategory.Text
            objSettings.Labs = cmbLabCategory.Text
            objSettings.Radiology = cmbRadioCategory.Text
            ''Start :: Dhruv 
            objSettings.RxMeds = cmbRxMedsCategory.Text
            ''End :: Dhruv
            'sarika 31st aug 07
            objSettings.OMRCategoryFax = cmbFaxCategory.Text
            objSettings.InboxAttachCategory = cmbInboxCategory.Text

            objSettings.RCMtoDMSCategory = cmbRCMtoDMSCategory.Text
            objSettings.WelchAllynECGCategory = cmbWelchAllynECGCategory.Text

            ''COMMENTED BY SHUBHANGI 20110606
            'objSettings.EMExamType = cmbEMExamType.SelectedValue 'Shubhangi
            'Shirish 20100614
            If rbProviderLabUsageAsk.Checked = True Then
                objSettings.GloLab_Provider_Usage = rbProviderLabUsageAsk.Tag.ToUpper()
            ElseIf rbProviderLabUsageLabOrder.Checked = True Then
                objSettings.GloLab_Provider_Usage = rbProviderLabUsageLabOrder.Tag.ToUpper()
            ElseIf rbProviderLabUsageRecordResults.Checked = True Then
                objSettings.GloLab_Provider_Usage = rbProviderLabUsageRecordResults.Tag.ToUpper()
            ElseIf rbProviderLabUsageTask.Checked = True Then
                objSettings.GloLab_Provider_Usage = rbProviderLabUsageTask.Tag.ToUpper()
            End If

            '----------------------------
            'objSettings.blnPwdComplexity = optPwdComplexYes.Checked

            '   objSettings.NoOfAttempts = txtNoOfAttempts.Text.Trim
            objSettings.NoOfAttempts = numLockOutAttempts.Value


            'Developer: Mitesh Patel
            'Date:27-Jan-2012
            'PRD: Signature format
            objSettings.SignatureFormat = cmbSignatureFormat.SelectedValue
            ''-----------x-----

            'save the password complexity settings 
            'm_strSQL()
            'm_blnSetComplexisityOnSetting()
            'If m_blnSetComplexisityOnSetting = False And optPwdComplexYes.Checked = True Then
            '    MessageBox.Show("You have not set password complexicity.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            '    frm.ShowDialog(Me)
            '    'optPwdComplexNo.Checked = True
            '    If frm._blnSetComplexisityOnSetting = False Then
            '        optPwdComplexNo.Checked = True
            '    End If
            'End If

            objSettings.blnPwdComplexity = optPwdComplexYes.Checked

            'sarika 14th june 07
            objSettings.ClinicDISettings = optClinicDIYes.Checked

            ' ''For clinic formulary setting  Commeneted
            ''objSettings.ClinicFormularySettings = optClinicFormularyYes.Checked

            'For clinic formulary setting \\new changes according to pravin sir if adv rx settings = true [checked],formulary group box show under surescript tab AND if false [unchecked ]- hide formulary groupbox under surescript tab into gloEMR <<tool settings>>
            objSettings.ClinicFormularySettings = chkFormularyEnable.Checked

            '' Mahesh 20070723 -- Record Level Locking 
            objSettings.RecordLevelLocking = chkRecordLocking.Checked
            objSettings.EnableLockField = Chkenlckfield.Checked
            objSettings.SaveLiquidData = ChkSavlqdata.Checked
            objSettings.IsVitalRequired = chkIsVitalRequired.Checked
            objSettings.IsICD9CPTRequired = chkIsICD9CPTRequired.Checked

            'code added by sagar on 31 july 2007 for threshold value
            If Trim(txtThresholdValue.Text) <> "" Then
                objSettings.ThresholdValue = Trim(txtThresholdValue.Text)
            Else
                objSettings.ThresholdValue = 840 'previously bydefault it was 420 mins. changed to 840 mins on 31 Oct'08 Friday.
            End If


            'code added by Mitesh 
            If Trim(txtRxEligibilitythreshold.Text) <> "" Then
                objSettings.RxEligibilitythresholdValue = Trim(txtRxEligibilitythreshold.Text)
            Else
                objSettings.RxEligibilitythresholdValue = 1
            End If

            ''Added for gloCommunity server setting on 03-jan-2012
            If _blnglocominstalled = True And chkglocomm.Checked = True Then

                'code commented & added by kanchan on 20120802 as per form authentication functionality
                If TestgloCommunityConnection(True) = False Then
                    tb_Settings.SelectTab(tb_Settings.TabPages.IndexOf(tbp_Sharepoint))
                    txtSPName.Focus()
                    Exit Function
                End If

                'If txtSPName.Text.Trim = "" Or txtSPName.Text.Trim = "https://" Then
                '    MessageBox.Show("Enter gloCommunity Server Name.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    tb_Settings.SelectTab(tb_Settings.TabPages.IndexOf(tbp_Sharepoint))

                '    txtSPName.Focus()
                '    Exit Function
                'End If
                'If txtSPFolderNm.Text.Trim = "" Then
                '    MessageBox.Show("Enter gloCommunity Site Name.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    tb_Settings.SelectTab(tb_Settings.TabPages.IndexOf(tbp_Sharepoint))
                '    txtSPFolderNm.Focus()
                '    Exit Function
                'End If

                'If ConnectToSharePoint(txtSPName.Text.Trim(), txtSPFolderNm.Text.Trim(), txtADFSSrvNm.Text.Trim()) = False Then
                '    'If Condition Added by kanchan on 20120802 for claim based authentication only
                '    If Not IsNothing(objSettings.SharepointAuthentication) AndAlso objSettings.SharepointAuthentication.Trim().ToUpper() = "CLAIM" Then
                '        If _blnConfigured = True Then '' if User E-mail configured on AD.
                '            If MessageBox.Show("Unable to connect to gloCommunity Server " & txtSPName.Text.Trim & " and Site " & txtSPFolderNm.Text.Trim, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information) Then
                '                tb_Settings.SelectTab(tb_Settings.TabPages.IndexOf(tbp_Sharepoint))
                '                txtSPFolderNm.Focus()
                '                Exit Function
                '            End If
                '        End If
                '    End If
                'End If

            End If
            objSettings.gloCommunityInstalled = _blnglocominstalled
            objSettings.SharepointSrvNm = txtSPName.Text.Trim()
            objSettings.SharepointSiteNm = txtSPFolderNm.Text.Trim()
            objSettings.ADFSServerName = txtADFSSrvNm.Text.Trim()
            objSettings.gloCommunityFeature = chkglocomm.Checked

            If chkglocomm.Checked Then
                If rb_staging.Checked = True Then
                    objSettings.IsCommunitystaging = True
                Else
                    objSettings.IsCommunitystaging = False
                End If
            End If
            ''ended 

            ''Code added on 7-dec-2012 for global hl7 outbound and genius
            If (chkhl7outb.Checked = False) Then
                objSettings.globlnhl7OutBound = False
                objSettings.globlnhl7Sendpatientdet = False
                objSettings.globlnhl7Sendapptdet = False
                objSettings.globlnhl7Sendchargeonsacl = False
                objSettings.globlnhl7Sendchronsafi = False
                objSettings.globlnhl7SendImmudet = False
                objSettings.globlnhl7SendVisitsumonsacl = False
                objSettings.globlnhl7SendVisitsumonsafi = False

            Else
                objSettings.globlnhl7OutBound = chkhl7outb.Checked
                objSettings.globlnhl7Sendpatientdet = chkhl7PatientReg.Checked
                objSettings.globlnhl7Sendapptdet = chkHL7Appointment.Checked
                objSettings.globlnhl7Sendchargeonsacl = chk_HL7_SendCharges_SaveClose.Checked
                objSettings.globlnhl7Sendchronsafi = chk_HL7_SendCharges_SaveFinish.Checked
                objSettings.globlnhl7SendImmudet = chkHL7Immunization.Checked
                objSettings.globlnhl7SendVisitsumonsacl = chk_HL7_SendVisitSum_SaveClose.Checked
                objSettings.globlnhl7SendVisitsumonsafi = Chk_HL7_SendVisitSum_SaveFinish.Checked
            End If

            If (chkgenoutb.Checked = False) Then
                objSettings.globlnGenOutBound = False
                objSettings.globlnGenSendchargeonsacl = False
                objSettings.globlnGenSendchargeonsafi = False

            Else
                objSettings.globlnGenOutBound = chkgenoutb.Checked
                objSettings.globlnGenSendchargeonsacl = chk_Gen_SaveandClose.Checked
                objSettings.globlnGenSendchargeonsafi = chk_Gen_SaveandFinish.Checked
            End If



            'start of code commented by manoj jadhav on 20120730 for moving service setiings to Respective Service
            ''sarika 11th aug 07
            'objSettings.HL7SystemPath = txtHL7FilePath.Text
            ''-------------------------------------
            ''******

            'objSettings.HL7ReceivingFacility = txtRecFacility.Text
            'objSettings.HL7ReceivingApplication = txtRecAppl.Text
            'objSettings.HL7SendingFacility = txtSendFacility.Text
            ''******
            'end of code commented by manoj jadhav on 20120730 for moving service setiings to Respective Service

            'sarika 31st aug 07
            objSettings.DBVersion = txtDBVersion.Text
            objSettings.AppVersion = txtAppVersion.Text
            objSettings.gloAdminVersion = txtgloEMRAdminVersion.Text

            '-------------------------------------

            'sarika 5th sept 07
            objSettings.PendingFaxUserID = cmbPendingFaxUser.SelectedValue
            objSettings.InboxAttacheUserID = cmbInboxUser.SelectedValue
            objSettings.RecieveFaxUserID = cmbRecieveFaxUser.SelectedValue
            '-----------------
            '''''''''code added by Anil on 20071119
            objSettings.AutoGeneratePatientCode = chkAutogenerateCode.Checked

            '' Code Added by Mitesh on 20120914
            objSettings.IncludeFrequencyAbbrevationInRxMeds = chk_IncludeFrequencyAbbrevationInRxMeds.Checked

            ''Allow Refill Cancel
            objSettings.AllowRefillcancel = chk_AllowRefillCancel.Checked
            ''--------

            ''Set Individual auto eRx Eligibility ON/Off
            objSettings.AutoEligibility = chkAutoEligibilityONorOFF.Checked


            ''start :: dhruv
            ''Added by Mayuri:20101004-To add functonality of save as copy patient
            objSettings.AllowEditablePatientCode = chkallowEditPatientCode.Checked
            objSettings.ShowDMAlert = ChkShowDMAlert.Checked

            '' end :: Dhruv
            '''''''''''''''''''''''''''''''''''''''''''''''''''''

            Dim IsEpcsEnable As Boolean = False
            If chkEnableEpcs.Checked Then
                Dim oraganization As Boolean = GetClinicInformation("bIsEpcsEnable")
                If Not oraganization Then
                    Dim objEpcshelper As New clsEPCSHelper
                    clsEPCSHelper.GetVendorAndUrlInformationForEpcs(gnClinicID, gblnIsStagingServer)
                    IsEpcsEnable = objEpcshelper.SetUpEPCSOrganizationEnable()
                    If Not IsEpcsEnable Then
                        chkEnableEpcs.Checked = False
                        Exit Function
                    Else
                        objSettings.UpdatebIsEpcsEnable(IsEpcsEnable)
                    End If
                Else
                    IsEpcsEnable = chkEnableEpcs.Checked
                End If
            End If
            ''''BDO Changes
            If flagEpcsEnableOnLoad <> IsEpcsEnable Then
                Dim objAudit1 As New clsAudit
                If IsEpcsEnable Then
                    objAudit1.CreateLog(clsAudit.enmActivityType.Modify, "EPCS setting enabled.", gstrLoginName, gstrClientMachineName)
                Else
                    objAudit1.CreateLog(clsAudit.enmActivityType.Modify, "EPCS setting disabled.", gstrLoginName, gstrClientMachineName)
                End If
                objAudit1 = Nothing
            End If
            objSettings.IsEpcsEnble = IsEpcsEnable
            objSettings.IsAllowPrintForCS = chkAllowPrintForCS.Checked

            'Added by ABD

            objSettings.IsPDMPAuto = chkPDMPAuto.Checked

            'pdmpsaveto dms setting
            objSettings.IsPDMPSendToDMS = Chkpdmpsavedms.Checked


            objSettings.PDMPParticipantID = txtPDMPUserName.Text.Trim()
            Dim objEncryptpdmp As New clsEncryption
            objSettings.PDMP_password = Convert.ToBase64String(Encoding.UTF8.GetBytes(txtPDMPPassword.Text.Trim()))




            'Added by Mitesh
            objSettings.EnableIntuitFeature = chkEnableIntuit.Checked
            objSettings.IncludeOriginalMessage = chkIncludeOrignalMessage.Checked

            objSettings.PatientPortalLabAckNotification = chkAcknowledgeEmailSend.Checked
            objSettings.PatientPortalStatementNotification = chkNotifyStatement.Checked
            If chkUseMedfusion.Checked = True Then
                If (txtPatientPortalEmailService.Text.ToString() = "") Then
                    MessageBox.Show("Enter Email service address.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    tb_Settings.SelectTab(tb_Settings.TabPages.IndexOf(tbpg_InterfaceSettings))
                    txtPatientPortalEmailService.Focus()
                    Exit Function
                End If
                If (txtPatientPortalgloCoreServicesInstallationPath.Text.ToString() = "") Then
                    MessageBox.Show("Enter QCore service installation path.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    tb_Settings.SelectTab(tb_Settings.TabPages.IndexOf(tbpg_InterfaceSettings))
                    txtPatientPortalgloCoreServicesInstallationPath.Focus()
                    Exit Function
                End If
            Else
                If (txtPatientPortalEmailService.Text.ToString() = "") Then
                    'If MessageBox.Show("Enter ‘Site Id’ under clinic information. This is mandatory information to send immunization registry details out in HL7 file format. Do you want to disable immunization registry generation in HL7 format and continue?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information) = Windows.Forms.DialogResult.Yes Then

                    If MessageBox.Show("Email service address is blank.Do you want to continue?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) = Windows.Forms.DialogResult.No Then
                        tb_Settings.SelectTab(tb_Settings.TabPages.IndexOf(tbpg_InterfaceSettings))
                        txtPatientPortalEmailService.Focus()
                        Exit Function
                        'Else
                        '    objSettings.sCommonEmailServicePath = txtPatientPortalEmailService.Text.Trim
                        '    'Dim objSettingsNew As New clsSettings
                        '    'Try

                        '    '    objSettingsNew.Add("PatientPortalEmailService", txtPatientPortalEmailService.Text.Trim, 1, 0, gloEMRAdmin.SettingFlag.None)
                        '    'Catch ex As Exception
                        '    '    ex = Nothing
                        '    'End Try
                    End If

                End If
                If (txtPatientPortalgloCoreServicesInstallationPath.Text.ToString() = "") Then
                    If MessageBox.Show("QCore service installation path is blank.Do you want to continue?", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation) = Windows.Forms.DialogResult.No Then
                        tb_Settings.SelectTab(tb_Settings.TabPages.IndexOf(tbpg_InterfaceSettings))
                        txtPatientPortalgloCoreServicesInstallationPath.Focus()
                        Exit Function
                        'Else
                        '    objSettings.sCommongloCoreServicePath = txtPatientPortalgloCoreServicesInstallationPath.Text.Trim

                        '    'Dim objSettingsNew As New clsSettings
                        '    'Try

                        '    '    objSettingsNew.Add("PatientPortalCoreServicePath", txtPatientPortalgloCoreServicesInstallationPath.Text.Trim, 1, 0, gloEMRAdmin.SettingFlag.None)
                        '    'Catch ex As Exception
                        '    '    ex = Nothing
                        '    'End Try
                    End If

                End If
            End If


            objSettings.sCommonEmailServicePath = txtPatientPortalEmailService.Text.Trim
            objSettings.sCommongloCoreServicePath = txtPatientPortalgloCoreServicesInstallationPath.Text.Trim

            If rdoNewsignaturepad.Checked Then
                objSettings.Signaturepadtouse = True
            Else
                objSettings.Signaturepadtouse = False
            End If


            'IntuitPractice ID
            objSettings.IntuitPracticeID = TxtPracticeID.Text

            objSettings.CCDADataExportServiceEnabled = chkEnableCCDAService.Checked
            ''-----xxx----
            'SureScript Staging/Production Url
            If chkNCPDPVer10dot6.Checked = False Then
                If rbStaging.Checked = True Then
                    objSettings.SurescriptUrlStaging = TxtSurescriptURL.Text
                Else
                    objSettings.SurescriptUrlProduction = TxtSurescriptURL.Text
                End If
            End If

            ''---DI Service URL
            objSettings.DIServiceURL = txtDIServiceURL.Text
            ''-- Service URL

            '''''''''code added by Pradeep on 20100627
            objSettings.UseSitePrefix = ChkUseSitePrefix.Checked
            objSettings.EnableSingleSignon = chkEnableSingleSignON.Checked

            '''''''''code added by Pradeep on 03012011 for suppress task on status change
            objSettings.EnableTasksforPatientStatusChange = ChkEnableTasksforPatientStatusChange.Checked
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

            ''Start :: Dhruv
            If (_SequentialPatientCode.ToString() <> txtSequentialPatientCode.Text.ToString.Trim()) Then
                Dim ogloSettings As gloSettings.GeneralSettings = New gloSettings.GeneralSettings(mdlGeneral.GetConnectionString)
                Dim objAudit1 As New clsAudit
                ogloSettings.AddSetting("SequentialPatientCode", txtSequentialPatientCode.Text.ToString.Trim().Replace(" ", ""), _ClinicID, 0, gloSettings.SettingFlag.Clinic)
                objAudit1.CreateLog(clsAudit.enmActivityType.Other, "Sequential patient code saved with value =" & txtSequentialPatientCode.Text.ToString(), gstrLoginName, gstrClientMachineName)
            End If
            ''End :: Dhruv 
            '''''''''''''''''''''''''''''''''''''''''''''''''''''
            '''''''''code added by Pradeep on 20100712
            If ChkExplicitlyAcceptTask.Checked = True Then
                objSettings.ExplicitlyAcceptTask = 1
            Else
                objSettings.ExplicitlyAcceptTask = 0
            End If

            'Recover Exam Module change
            If chkRecoverExam.Checked = True Then
                objSettings.RecoverExam = 1
            Else
                objSettings.RecoverExam = 0
            End If

            'code added by suraj on 20080725'''*******
            objSettings.RxDeclaration = txtDeclarartion.Text

            'RxHubDisclaimer
            objSettings.RxHUBDisclaimer = txtRxHubDisclaimer.Text

            '------>Suraj 20080801
            objSettings.GenerateMic = chbImunizationReport.Checked

            'Added by Amit - 7010 to control Vaccine inventory
            objSettings.TrackVaccineInventory = chkTrackVaccineInventory.Checked


            'Added by Amit - 7020 Immunization Funding field mandatory
            objSettings.RequireFunding = chkIMRequireFunding.Checked


            'Added by Amit - 7020 Enable Copy Exam
            objSettings.EnableCopyExam = chkEnableCopyExam.Checked
            '27-Jan-15 Aniket: Addition of new setting to Show SmartDX screen on SmartDX save
            objSettings.ShowDxCPTScreenOnSmartDX = chkShowDxCPTScreenOnSmartDX.Checked

            objSettings.IsExamPTBillingEnabled = cbIsExamPTBillingEnabled.Checked

            'Added by Amit - 7030 MU Stage 1 2013 changes'
            'CPOE
            If rdoCPOEMU1Current.Checked Then
                objSettings.CPOE_MU1_Change = 0
            Else
                objSettings.CPOE_MU1_Change = 1
            End If

            'Vital
            If rdoVitalMU1Current.Checked Then
                objSettings.Vital_MU1_Change = 0
            Else
                objSettings.Vital_MU1_Change = 1
                If rdoVitalAllRequired.Checked Then
                    objSettings.VitalAllRequired = 1
                Else
                    objSettings.VitalAllRequired = 0
                End If

                If rdoVitalBPRequired.Checked Then
                    objSettings.VitalBPRequired = 1
                Else
                    objSettings.VitalBPRequired = 0
                End If

                If rdoVitalHeightWeightRequired.Checked Then
                    objSettings.VitalHeightWeightRequired = 1
                Else
                    objSettings.VitalHeightWeightRequired = 0
                End If
            End If

            '23-Sep-13 Aniket: Vital Stage 2 Changes

            If rdoVitalAllRequiredStage2.Checked Then
                objSettings.VitalAllRequired_Stage2 = 1
            Else
                objSettings.VitalAllRequired_Stage2 = 0
            End If

            If rdoVitalBPRequiredStage2.Checked Then
                objSettings.VitalBPRequired_Stage2 = 1
            Else
                objSettings.VitalBPRequired_Stage2 = 0
            End If

            If rdoVitalHeightWeightRequiredStage2.Checked Then
                objSettings.VitalHeightWeightRequired_Stage2 = 1
            Else
                objSettings.VitalHeightWeightRequired_Stage2 = 0
            End If

            '23-Sep-13 Aniket: Vital Stage 2 Changes

            'ELECTRONIC PRESCRIPTION MU 2013
            If rdoeRxReqd.Checked Then
                objSettings.eRx_MU1_Change = 0
            Else
                objSettings.eRx_MU1_Change = 1
            End If

            'REPORT CLINICAL QM MU 2013'
            If rdoeRptClinicalQuaReqd.Checked Then
                objSettings.eRptClinicalQua_MU1_Change = 0
            Else
                objSettings.eRptClinicalQua_MU1_Change = 1
            End If

            'ELECTRONIC COPY OF PAT. INFO. MU 2013' 
            If rdoeCopyPatHealthInfoReqd.Checked Then
                objSettings.eCopyPatHealthInfo_MU1_Change = 0
            Else
                objSettings.eCopyPatHealthInfo_MU1_Change = 1
            End If

            'ELECTRONICALLY EXCH cLINICAL INFO MU 2013'
            If rdoeExchangeClinInforReqd.Checked Then
                objSettings.eExchangeClinInfor_MU1_Change = 0
            Else
                objSettings.eExchangeClinInfor_MU1_Change = 1
            End If



            'code added by sarika on 18th jan 08 for gloReporting authentication
            objSettings.ReportingUserName = txtRptUserName.Text.Trim
            'Encrypt the Password and then save it
            Dim objEncrypt As New clsEncryption

            objSettings.ReportingPassword = objEncrypt.EncryptToBase64String(txtRptPassword.Text.Trim, constEncryptDecryptKey_Services)
            '---

            'sarika UseFaxNoPrefix 12th april 08
            objSettings.UseFaxNoPrefix = chkUseFaxNoPrefix.Checked
            objSettings.FaxNoPrefix = txtFaxNoPrefix.Text.Trim()
            '--sarika UseFaxNoPrefix 12th april 08


            'sarika internet fax
            objSettings.InternetFax = chkInternetFax.Checked
            'eFax Login Key
            'objSettings.eFaxUserID = txteFaxUserID.Text.Trim()
            ' '' Encription Key Chaneg to gloEMR Encription Key, As for any user wherther it will be og gloEMR or any outside user like here Fax User we have to have to use gloEMR Encription Key
            'objSettings.eFaxUserPassword = objEncrypt.EncryptToBase64String(txteFaxPassword.Text.Trim(), constEncryptDecryptKey)

            'sarika internet fax

            'code added by supriya 11/7/2008
            'Surescript Server settings
            If chkSurescript.Checked Then
                objSettings.IsSurescriptEnabled = True
                If rbStaging.Checked = True Then
                    objSettings.IsStagingServer = True
                Else
                    objSettings.IsStagingServer = False
                End If
            Else
                objSettings.IsSurescriptEnabled = False
            End If

            gblnIsSurescriptEnabled = objSettings.IsSurescriptEnabled
            gblnIsStagingServer = objSettings.IsStagingServer

            If chkSecureMesaage.Checked Then
                objSettings.IsSecureMessageEnabled = True
                If rbSecureStaging.Checked = True Then
                    objSettings.IsSecureStagingServer = True
                Else
                    objSettings.IsSecureStagingServer = False
                End If
            Else
                objSettings.IsSecureMessageEnabled = False
            End If

            gblnIsSecureMsgEnabled = objSettings.IsSecureMessageEnabled
            gblnIsSecureMsgStagingServer = objSettings.IsSecureStagingServer

            If rbSecureStaging.Checked = True Then
                objSettings.SecuremsgUrlStaging = txtSecureMessageURL.Text
            Else
                objSettings.SecuremsgUrlProduction = txtSecureMessageURL.Text
            End If

            If chkPatientSavings.Checked Then
                objSettings.IsPatientSavingMessageEnabled = True

                If chkPatientSavingInbox.Checked Then
                    objSettings.IsPatientSavingInboxEnabled = True
                Else
                    objSettings.IsPatientSavingInboxEnabled = False
                End If
            Else
                objSettings.IsPatientSavingMessageEnabled = False
                objSettings.IsPatientSavingInboxEnabled = False
            End If

            'XDM Message Settings
            If ChkEnableXDMMessage.Checked Then
                objSettings.IsXDMSecureMessageEnable = True
            Else
                objSettings.IsXDMSecureMessageEnable = False
            End If

            'code added for NCPDP 10.6 changes
            objSettings.IsNCPDP10dot6Ver = chkNCPDPVer10dot6.Checked
            objSettings.Is8dot1PendingRefReqComplete = Is8dot1PendingRefReqComplete
            gbln10dot6Version = objSettings.IsNCPDP10dot6Ver
            If chkNCPDPVer10dot6.Checked = True Then
                If rbStaging.Checked = True Then
                    objSettings.Surescript10dot6UrlStaging = TxtSurescriptURL.Text
                Else
                    objSettings.Surescript10dot6UrlProduction = TxtSurescriptURL.Text
                End If
            End If


            'newly added

            objSettings.IsMedHistory10Dot6Enable = chkMedHistory.Checked
            If rbStaging.Checked = True Then
                objSettings.MedhxSurescriptUrlStaging = txtMedHistoryPortalURL.Text
            Else
                objSettings.MedhxSurescriptUrlStaging = sMedHxStagingURl
            End If
            If rbProduction.Checked = True Then
                objSettings.MedhxSurescriptUrlProduction = txtMedHistoryPortalURL.Text
            Else
                objSettings.MedhxSurescriptUrlProduction = sMedHxProductionURl
            End If
            ' objSettings.MedHistoryPortalURL = txtMedHistoryPortalURL.Text

            If chkMedHistory.Checked = True Then
                If txtMedHistoryPortalURL.Text.Trim = "" Then
                    MessageBox.Show("Enter Medication History 10.6 Portal URL.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    tb_Settings.SelectTab(tb_Settings.TabPages.IndexOf(tbpg_SurescriptSettings))
                    txtMedHistoryPortalURL.Focus()
                    Exit Function
                Else
                    If rbStaging.Checked = True Then
                        objSettings.MedhxSurescriptUrlStaging = txtMedHistoryPortalURL.Text
                    Else
                        objSettings.MedhxSurescriptUrlStaging = sMedHxStagingURl
                    End If
                    If rbProduction.Checked = True Then
                        objSettings.MedhxSurescriptUrlProduction = txtMedHistoryPortalURL.Text
                    Else
                        objSettings.MedhxSurescriptUrlProduction = sMedHxProductionURl
                    End If
                End If
            End If

            'code added by supriya 11/7/2008
            'Surescript Server settings
            ''Start:      Dhruv : 
            '' chetan integrated from Certification  added on 4oct-2010
            'objSettings.SMServerName = txtSMServerName.Text.Trim
            'objSettings.SMDatabaseName = txtSMDatabaseName.Text.Trim
            'objSettings.SMSQLUserId = txtSMSQLUserID.Text.Trim
            'objSettings.SMblnSQLAuthentication = optSMSQLAuthentication.Checked
            'objSettings.SMblnShowsmdbsetting = chkEnshowmade.Checked
            For i As Integer = 0 To trvsnomed.Nodes.Count - 1
                If trvsnomed.Nodes(i).Checked = True Then
                    objSettings.SMTrvNode &= trvsnomed.Nodes(i).Text & ","

                End If
            Next




            If txtSMServerName.Text.Trim = "" Then
                MessageBox.Show("Enter SnoMed database settings Server Name.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                tb_Settings.SelectTab(tb_Settings.TabPages.IndexOf(tbpg_ReportServerSettings))
                txtSMServerName.Focus()
                Exit Function
            End If

            If txtSMDatabaseName.Text.Trim = "" Then
                MessageBox.Show("Enter  SnoMed database settings Database Name.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                tb_Settings.SelectTab(tb_Settings.TabPages.IndexOf(tbpg_ReportServerSettings))
                txtSMDatabaseName.Focus()
                Exit Function
            End If
            If optSMSQLAuthentication.Checked = True Then
                If txtSMSQLUserID.Text.Trim = "" Then
                    MessageBox.Show("Enter SnoMed database settings SQL User ID", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    tb_Settings.SelectTab(tb_Settings.TabPages.IndexOf(tbpg_ReportServerSettings))
                    txtSMSQLUserID.Focus()
                    Exit Function
                End If

                If txtSMSQLPassword.Text.Trim = "" Then
                    MessageBox.Show("Enter SnoMed database settings SQL Password", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    tb_Settings.SelectTab(tb_Settings.TabPages.IndexOf(tbpg_ReportServerSettings))
                    txtSMSQLPassword.Focus()

                    Exit Function
                End If

                '''''''''''''''''''''''
                'Code Start-Added by kanchan on 20100908 
                If objSQLSettings.IsSQLConnect(txtSMServerName.Text.Trim, txtSMDatabaseName.Text.Trim, txtSMSQLUserID.Text.Trim, txtSMSQLPassword.Text.Trim) = False Then
                    If MessageBox.Show("Unable to connect to SnoMed database settings SQL Server " & txtSMServerName.Text.Trim & " and Database " & txtSMDatabaseName.Text.Trim & vbCrLf & "Select valid credentials.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information) Then
                        tb_Settings.SelectTab(tb_Settings.TabPages.IndexOf(tbpg_ReportServerSettings))
                        Exit Function
                    End If
                End If
            Else
                If objSQLSettings.IsConnect(txtSMServerName.Text.Trim, txtSMDatabaseName.Text.Trim, False, "", "") = False Then
                    If MessageBox.Show("Unable to connect to SnoMed database settings SQL Server " & txtSMServerName.Text.Trim & " and Database " & txtSMDatabaseName.Text.Trim & vbCrLf & "Select valid credentials.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information) Then
                        tb_Settings.SelectTab(tb_Settings.TabPages.IndexOf(tbpg_ReportServerSettings))
                        Exit Function
                    End If
                End If
                'Code End-Added by kanchan on 20100908
                '''''''''''''''''''''''
            End If
            'objSettings.SMConnectionString=



            objSettings.SMServerName = txtSMServerName.Text.Trim
            objSettings.SMDatabaseName = txtSMDatabaseName.Text.Trim
            objSettings.SMSQLUserId = txtSMSQLUserID.Text.Trim


            ''Start ::(Encryption)SnowMadePassword Encryption
            Dim _snowmadePassWord As String = String.Empty
            objSettings.SMSQLPwd = txtSMSQLPassword.Text
            Dim objEncryption As New clsEncryption
            If Not IsNothing(objEncryption) Then
                _snowmadePassWord = objEncryption.EncryptToBase64String(txtSMSQLPassword.Text, mdlGeneral.constEncryptDecryptKey)
                'objSettings.SMSQLPwd = txtSMSQLPassword.Text ''OldCode
                objEncryption = Nothing
            End If
            objSettings.SMSQLPwd = _snowmadePassWord
            ''End ::(Encryption)SnowMadePassword Encryption

            objSettings.SMblnSQLAuthentication = optSMSQLAuthentication.Checked
            objSettings.SMblnShowsmdbsetting = True

            ''Start ::[No need to create] SnowMade Connection Stirng 
            'If (chkEnshowmade.Checked = True) Then
            '    If optSMSQLAuthentication.Checked = False Then

            '        objSettings.SMConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString(txtSMServerName.Text.Trim, txtSMDatabaseName.Text.Trim, False, "", "")
            '        'objSettings.PMUserID = ""
            '        'objSettings.PMSQLPwd = ""
            '    Else

            '        objSettings.SMConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString(txtSMServerName.Text.Trim, txtSMDatabaseName.Text.Trim, txtSMSQLUserID.Text.Trim, txtSMSQLPassword.Text)
            '        'Dim objEncryptDecrypt As New clsEncryption
            '        '  objSettings.PMSQLPwd = objEncryptDecrypt.EncryptToBase64String(txtSQLPassword.Text.Trim, constEncryptDecryptKey)
            '        ' objEncryptDecrypt = Nothing
            '    End If
            'Else
            '    objSettings.SMConnectionString = ""

            'End If
            ''End ::[No need to create] SnowMade Connection Stirng 



            'Code Start-Added by kanchan on 20100908 for RxNorm DB settings

            If txtRxNServerName.Text.Trim = "" Then
                MessageBox.Show("Enter RxNorm database settings Server Name.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                tb_Settings.SelectTab(tb_Settings.TabPages.IndexOf(tbpg_ReportServerSettings))
                txtRxNServerName.Focus()
                Exit Function
            End If
            If txtRxNDatabaseName.Text.Trim = "" Then
                MessageBox.Show("Enter RxNorm database settings Database Name.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                tb_Settings.SelectTab(tb_Settings.TabPages.IndexOf(tbpg_ReportServerSettings))
                txtRxNDatabaseName.Focus()
                Exit Function
            End If
            If optRxNSQLAuthentication.Checked = True Then
                If txtRxNSQLUserID.Text.Trim = "" Then
                    MessageBox.Show("Enter RxNorm database settings SQL User ID", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    tb_Settings.SelectTab(tb_Settings.TabPages.IndexOf(tbpg_ReportServerSettings))
                    txtRxNSQLUserID.Focus()
                    Exit Function
                End If
                If txtRxNSQLPassword.Text.Trim = "" Then
                    MessageBox.Show("Enter RxNorm database settings SQL Password", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    tb_Settings.SelectTab(tb_Settings.TabPages.IndexOf(tbpg_ReportServerSettings))
                    txtRxNSQLPassword.Focus()
                    Exit Function
                End If
                If objSQLSettings.IsSQLConnect(txtRxNServerName.Text.Trim, txtRxNDatabaseName.Text.Trim, txtRxNSQLUserID.Text.Trim, txtRxNSQLPassword.Text.Trim) = False Then
                    If MessageBox.Show("Unable to connect to RxNorm database settings SQL Server " & txtRxNServerName.Text.Trim & " and Database " & txtRxNDatabaseName.Text.Trim & vbCrLf & "Select valid credentials.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information) Then
                        tb_Settings.SelectTab(tb_Settings.TabPages.IndexOf(tbpg_ReportServerSettings))
                        Exit Function
                    End If
                End If
            Else
                If objSQLSettings.IsConnect(txtRxNServerName.Text.Trim, txtRxNDatabaseName.Text.Trim, False, "", "") = False Then
                    If MessageBox.Show("Unable to connect to RxNorm database settings SQL Server " & txtRxNServerName.Text.Trim & " and Database " & txtRxNDatabaseName.Text.Trim & vbCrLf & "Select valid credentials.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information) Then
                        tb_Settings.SelectTab(tb_Settings.TabPages.IndexOf(tbpg_ReportServerSettings))
                        Exit Function
                    End If
                End If
            End If

            objSettings.RxNServerName = txtRxNServerName.Text.Trim
            objSettings.RxNDatabaseName = txtRxNDatabaseName.Text.Trim
            objSettings.RxNSQLUserId = txtRxNSQLUserID.Text.Trim

            ''Start ::(Encryption)RxNormPassword Encryption
            Dim _RxNormPassWord As String = String.Empty
            Dim objEncryptions As New clsEncryption
            If Not IsNothing(objEncryptions) Then
                _RxNormPassWord = objEncryptions.EncryptToBase64String(txtRxNSQLPassword.Text, mdlGeneral.constEncryptDecryptKey)
                ' objSettings.RxNSQLPwd = txtRxNSQLPassword.Text.Trim ''OldCode
                objEncryptions = Nothing
            End If
            objSettings.RxNSQLPwd = _RxNormPassWord
            ''End ::(Encryption)RxNormPassword Encryption


            objSettings.RxNblnSQLAuthentication = optRxNSQLAuthentication.Checked
            objSettings.RxNblnShowRxNdbsetting = True



            '---- End Of MMW Database setting -----------'
            '---- End of code added by Rahul Patel on 21-10-2010 --'

            '---- Added by Rahul Patel on 26-10-2010 ----'
            '---- For Saving DMS Database Setting -------'
            If txtDMSServerName.Text.Trim = "" Then
                MessageBox.Show("Enter DMS database settings Server Name.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                tb_Settings.SelectTab(tb_Settings.TabPages.IndexOf(tbpg_ReportServerSettings))
                txtDMSServerName.Focus()
                Exit Function
            End If
            If txtDMSDatabaseName.Text.Trim = "" Then
                MessageBox.Show("Enter DMS database settings Database Name.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                tb_Settings.SelectTab(tb_Settings.TabPages.IndexOf(tbpg_ReportServerSettings))
                txtDMSDatabaseName.Focus()
                Exit Function
            End If
            If optDMSSqlAuthentication.Checked = True Then
                If txtDMSUserId.Text.Trim = "" Then
                    MessageBox.Show("Enter DMS database settings SQL User ID", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    tb_Settings.SelectTab(tb_Settings.TabPages.IndexOf(tbpg_ReportServerSettings))
                    txtDMSUserId.Focus()
                    Exit Function
                End If
                If txtDMSPassword.Text.Trim = "" Then
                    MessageBox.Show("Enter DMS database settings SQL Password", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    tb_Settings.SelectTab(tb_Settings.TabPages.IndexOf(tbpg_ReportServerSettings))
                    txtDMSPassword.Focus()
                    Exit Function
                End If
                If objSQLSettings.IsSQLConnect(txtDMSServerName.Text.Trim, txtDMSDatabaseName.Text.Trim, txtDMSUserId.Text.Trim, txtDMSPassword.Text.Trim) = False Then
                    If MessageBox.Show("Unable to connect to DMS database settings SQL Server " & txtDMSServerName.Text.Trim & " and Database " & txtDMSDatabaseName.Text.Trim & vbCrLf & "Select valid credentials.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information) Then
                        tb_Settings.SelectTab(tb_Settings.TabPages.IndexOf(tbpg_ReportServerSettings))
                        Exit Function
                    End If
                End If
            Else
                If objSQLSettings.IsConnect(txtDMSServerName.Text.Trim, txtDMSDatabaseName.Text.Trim, False, "", "") = False Then
                    If MessageBox.Show("Unable to connect to DMS database settings SQL Server " & txtDMSServerName.Text.Trim & " and Database " & txtDMSDatabaseName.Text.Trim & vbCrLf & "Select valid credentials.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information) Then
                        tb_Settings.SelectTab(tb_Settings.TabPages.IndexOf(tbpg_ReportServerSettings))
                        Exit Function
                    End If
                End If
            End If
            objSettings.DMSServerName = txtDMSServerName.Text.Trim
            objSettings.DMSDatabaseName = txtDMSDatabaseName.Text.Trim
            objSettings.DMSSQLUserId = txtDMSUserId.Text.Trim

            ''Start ::(Encryption)RxNormPassword Encryption
            Dim _DMSPassWord As String = String.Empty
            Dim objDMSEncryptions As New clsEncryption
            If Not IsNothing(objDMSEncryptions) Then
                _DMSPassWord = objDMSEncryptions.EncryptToBase64String(txtDMSPassword.Text, mdlGeneral.constEncryptDecryptKey)
                ' objSettings.RxNSQLPwd = txtRxNSQLPassword.Text.Trim ''OldCode
                objDMSEncryptions = Nothing
            End If
            objSettings.DMSSQLPwd = _DMSPassWord

            objSettings.DMSblnSQLAuthentication = optDMSSqlAuthentication.Checked

            '---- End Of DMS Database setting -----------'
            '---- End of code added by Rahul Patel on 26-10-2010 --'

            'Code added by Rohit for Device Database Setting on 20110514
            If txtDeviceServerName.Text.Trim = String.Empty AndAlso txtDeviceDataBaseName.Text.Trim <> String.Empty Then
                MessageBox.Show("Enter Device database settings Server Name.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                tb_Settings.SelectTab(tb_Settings.TabPages.IndexOf(tbpg_ReportServerSettings))
                txtDeviceServerName.Focus()
                Exit Function
            End If
            If txtDeviceDataBaseName.Text.Trim = String.Empty AndAlso txtDeviceServerName.Text.Trim <> String.Empty Then
                MessageBox.Show("Enter Device database settings Database Name.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                tb_Settings.SelectTab(tb_Settings.TabPages.IndexOf(tbpg_ReportServerSettings))
                txtDeviceDataBaseName.Focus()
                Exit Function
            End If

            If txtDeviceDataBaseName.Text.Trim <> String.Empty AndAlso txtDeviceServerName.Text.Trim <> String.Empty Then
                If objSQLSettings.IsConnect(txtDeviceServerName.Text.Trim, txtDeviceDataBaseName.Text.Trim, False, "", "") = False Then
                    If MessageBox.Show("Unable to connect to Device database settings SQL Server " & txtDeviceServerName.Text.Trim & " and Database " & txtDeviceDataBaseName.Text.Trim & "." & vbCrLf & " Select valid credentials.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information) Then
                        tb_Settings.SelectTab(tb_Settings.TabPages.IndexOf(tbpg_ReportServerSettings))
                        Exit Function
                    End If
                End If
            End If


            objSettings.DeviceServerName = txtDeviceServerName.Text.Trim
            objSettings.DeviceDatabaseName = txtDeviceDataBaseName.Text.Trim

            'End of Code added by Rohit for Device Database Setting on 20110514

            'added by nilesh on 20101025
            'saving gloHL7 database settings
            If txtgloHL7ServerName.Text.Trim = "" Then
                MessageBox.Show("Enter HL7 database settings Server Name.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                tb_Settings.SelectTab(tb_Settings.TabPages.IndexOf(tbpg_ReportServerSettings))
                txtgloHL7ServerName.Focus()
                Exit Function
            End If
            If txtgloHL7DatabaseName.Text.Trim = "" Then
                MessageBox.Show("Enter HL7 database settings Database Name.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                tb_Settings.SelectTab(tb_Settings.TabPages.IndexOf(tbpg_ReportServerSettings))
                txtgloHL7DatabaseName.Focus()
                Exit Function
            End If
            If optgloHL7SqlAuthentication.Checked = True Then
                If txtgloHL7UserID.Text.Trim = "" Then
                    MessageBox.Show("Enter HL7 database settings SQL User ID", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    tb_Settings.SelectTab(tb_Settings.TabPages.IndexOf(tbpg_ReportServerSettings))
                    txtgloHL7UserID.Focus()
                    Exit Function
                End If
                If txtgloHL7Password.Text.Trim = "" Then
                    MessageBox.Show("Enter HL7 database settings SQL Password", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    tb_Settings.SelectTab(tb_Settings.TabPages.IndexOf(tbpg_ReportServerSettings))
                    txtgloHL7Password.Focus()
                    Exit Function
                End If
                If objSQLSettings.IsSQLConnect(txtgloHL7ServerName.Text.Trim, txtgloHL7DatabaseName.Text.Trim, txtgloHL7UserID.Text.Trim, txtgloHL7Password.Text.Trim) = False Then
                    If MessageBox.Show("Unable to connect to HL7 database settings SQL Server " & txtgloHL7ServerName.Text.Trim & " and Database " & txtgloHL7DatabaseName.Text.Trim & vbCrLf & "Select valid credentials.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information) Then
                        tb_Settings.SelectTab(tb_Settings.TabPages.IndexOf(tbpg_ReportServerSettings))
                        Exit Function
                    End If
                End If
            Else
                If objSQLSettings.IsConnect(txtgloHL7ServerName.Text.Trim, txtgloHL7DatabaseName.Text.Trim, False, "", "") = False Then
                    If MessageBox.Show("Unable to connect to HL7 database settings SQL Server " & txtgloHL7ServerName.Text.Trim & " and Database " & txtgloHL7DatabaseName.Text.Trim & vbCrLf & "Select valid credentials.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information) Then
                        tb_Settings.SelectTab(tb_Settings.TabPages.IndexOf(tbpg_ReportServerSettings))
                        Exit Function
                    End If
                End If
            End If

            objSettings.gloHL7ServerName = txtgloHL7ServerName.Text.Trim
            objSettings.gloHL7DatabaseName = txtgloHL7DatabaseName.Text.Trim
            objSettings.gloHL7SQLUserID = txtgloHL7UserID.Text.Trim

            ''Start ::(Encryption)RxNormPassword Encryption
            Dim _gloHL7PassWord As String = String.Empty
            Dim objgloHL7Encryptions As New clsEncryption
            If Not IsNothing(objgloHL7Encryptions) Then
                _gloHL7PassWord = objgloHL7Encryptions.EncryptToBase64String(txtgloHL7Password.Text, mdlGeneral.constEncryptDecryptKey)
                ' objSettings.RxNSQLPwd = txtRxNSQLPassword.Text.Trim ''OldCode
                objgloHL7Encryptions = Nothing
            End If
            objSettings.gloHL7SQLPassword = _gloHL7PassWord

            objSettings.gloHL7blnSQLAuthentication = optgloHL7SqlAuthentication.Checked
            'end by nilesh on 20101025
            ''Start ::[No need to create] RxNorm Connection Stirng 
            'If (chkEnRxNorm.Checked = True) Then
            '    If optRxNSQLAuthentication.Checked = False Then
            '        objSettings.RxNConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString(txtRxNServerName.Text.Trim, txtRxNDatabaseName.Text.Trim, False, "", "")
            '    Else
            '        objSettings.RxNConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString(txtRxNServerName.Text.Trim, txtRxNDatabaseName.Text.Trim, txtRxNSQLUserID.Text.Trim, txtRxNSQLPassword.Text)
            '    End If
            'Else
            '    objSettings.RxNConnectionString = ""
            'End If
            ''End ::[No need to create] RxNorm Connection Stirng 
            'Code End-Added by kanchan on 20100908 for RxNorm DB settings


            '' chetan integrated from Certification  added on 4oct-2010
            ''End :: dhruv 

            If chkAdvanceRx.Checked = True Then
                objSettings.IsAdvanceRxEnabled = True
                If rbAdvRxStaging.Checked = True Then
                    objSettings.IsAdvanceRxStagingServer = True
                Else
                    objSettings.IsAdvanceRxStagingServer = False
                End If
                objSettings.EARDirectory = "" '\\commented on 20090820: txtEARDirectory.Text.Trim

                '20090819 rxhub participantid & rxpassword settings
                objSettings.ParticipantID = txtParticipantID.Text.Trim
                objSettings.RxPassword = objEncrypt.EncryptToBase64String(txtRxPswd.Text.Trim(), constEncryptDecryptKey)

            Else
                objSettings.IsAdvanceRxEnabled = False
                objSettings.EARDirectory = ""

                '20091113 rxhub participantid & rxpassword settings save remains same, Don't save as blank
                objSettings.ParticipantID = txtParticipantID.Text.Trim
                objSettings.RxPassword = objEncrypt.EncryptToBase64String(txtRxPswd.Text.Trim(), constEncryptDecryptKey)

            End If


            'sarika SiteID Setting 20090607
            objSettings.SiteID = txtSiteID.Text.Trim()
            '----



            'sarika internet fax'
            objSettings.eFaxDirDownload = txteFaxDownloadDir.Text.Trim()
            'sarika internet fax'

            '' SagarK 20080802
            '' For MCIR Report Path
            If chbImunizationReport.Checked = True Then
                If txtMCIRReportPath.Text.Trim.Length > 0 Then
                    If Directory.Exists(txtMCIRReportPath.Text.Trim()) = True Then
                        objSettings.MCIRReportPath = txtMCIRReportPath.Text.Trim()
                    Else
                        MessageBox.Show("MCIR Report Path is not valid. Enter the valid Path.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        tb_Settings.SelectTab(tb_Settings.TabPages.IndexOf(tbp_OtherSettings))
                        txtMCIRReportPath.Focus()
                        Exit Function
                    End If
                End If
            End If

            '' Added by Ujwala for certification criteria - 170.315(b)(7) Data Segmentation for Privacy – Send - Start
            If txtCDAPrivacyText.Text.Trim.Length > 0 Then
                objSettings.CDAPrivacyText = txtCDAPrivacyText.Text.Trim
            Else
                MessageBox.Show("CDA Privacy Text is not entered. Enter the CDA Privacy Text.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                tb_Settings.SelectTab(tb_Settings.TabPages.IndexOf(tbpg_CCDSettings))
                txtCDAPrivacyText.Focus()
                Exit Function
            End If
            '' Added by Ujwala for certification criteria - 170.315(b)(7) Data Segmentation for Privacy – Send - End

            ''Start :: dhruv
            ''Added on 20100728 by sanjog for validating the path
            If txtCCDFilePath.Text.Trim.Length > 0 Then
                If Directory.Exists(txtCCDFilePath.Text.Trim()) = True Then
                    Dim _tempstr As String = txtCCDFilePath.Text.Trim
                    If Not _tempstr.EndsWith("\") Then
                        _tempstr = _tempstr & "\"
                        objSettings.CCDfilePath = _tempstr
                    Else
                        objSettings.CCDfilePath = _tempstr
                    End If
                    ''Added on 20100728 by sanjog for validating the path
                Else
                    MessageBox.Show("CCD file path is not valid. Enter the valid Path.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    tb_Settings.SelectTab(tb_Settings.TabPages.IndexOf(tbpg_CCDSettings))
                    txtCCDFilePath.Focus()
                    Exit Function
                End If
            End If
            If txtStyleSheetPath.Text.Trim = "" Then
                MessageBox.Show("Enter CDA style sheet Path.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                tb_Settings.SelectTab(tb_Settings.TabPages.IndexOf(tbpg_CCDSettings))
                txtStyleSheetPath.Focus()
                Exit Function
            Else
                objSettings.StylesheetfilePath = txtStyleSheetPath.Text.Trim
            End If

            If txtCDAValidatorUrl.Text.Trim = "" Then
                MessageBox.Show("Enter CDA validator url.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                tb_Settings.SelectTab(tb_Settings.TabPages.IndexOf(tbpg_CCDSettings))
                txtCDAValidatorUrl.Focus()
                Exit Function
            Else
                objSettings.CDAValidatorUrl = txtCDAValidatorUrl.Text.Trim
            End If
            'Added Code for CLINICAL DOCUMENTS EXPORT PATH
            If txtClinicalDocumentsExportPath.Text.Trim.Length > 0 Then
                If Directory.Exists(txtClinicalDocumentsExportPath.Text.Trim()) = True Then
                    Dim _tempstr As String = txtClinicalDocumentsExportPath.Text.Trim
                    If PathIsUNC(_tempstr) Then
                        objSettings.ClinicalDocfilePath = _tempstr
                    Else
                        MessageBox.Show("Clinical Documents Export Path is not valid. Enter the valid server shared path.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        tb_Settings.SelectTab(tb_Settings.TabPages.IndexOf(tbpg_ClinicalChart))
                        txtClinicalDocumentsExportPath.Focus()
                        Exit Function
                    End If
                Else
                    MessageBox.Show("Clinical Documents Export Path is not valid. Enter the valid server shared path.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    tb_Settings.SelectTab(tb_Settings.TabPages.IndexOf(tbpg_ClinicalChart))
                    txtClinicalDocumentsExportPath.Focus()
                    Exit Function
                End If
            End If



            ''End ::  Dhruv
            'code for CDS validation
            'If (chkEnabledCDS.Checked) Then
            '    If (txtCDS_PESUrl.Text.Trim() = "") Then
            '        MessageBox.Show("Enter CDS URL.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '        tb_Settings.SelectTab(tb_Settings.TabPages.IndexOf(tbpg_CCDSettings))
            '        txtCDS_PESUrl.Focus()
            '        Exit Function
            '    End If
            '    If (txtCDS_PESUserName.Text.Trim() = "") Then
            '        MessageBox.Show("Enter CDS User Name.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '        tb_Settings.SelectTab(tb_Settings.TabPages.IndexOf(tbpg_CCDSettings))
            '        txtCDS_PESUserName.Focus()
            '        Exit Function
            '    End If
            '    If (txtCDS_PESPassword.Text.Trim() = "") Then
            '        MessageBox.Show("Enter CDS Password.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '        tb_Settings.SelectTab(tb_Settings.TabPages.IndexOf(tbpg_CCDSettings))
            '        txtCDS_PESPassword.Focus()
            '        Exit Function
            '    End If
            'End If
            'End code for CDS validation
            'Code Start-Added by kanchan on 20100604 for CCD User
            If txtCCDUser.Text.Trim.Length > 0 Then
                objSettings.CCDDefaultUser = _CCD_DefaultUserID
            End If
            'Code End-Added by kanchan on 20100604 for CCD User

            If rb_ClinicalSummFamHst_CurrentHistory.Checked = True Then
                objSettings.ClinicalSummaryFamilyHistory = clsSettings.ClinicalSummaryHistory.Current
            ElseIf rb_ClinicalSummFamHst_VisitSpecific.Checked = True Then
                objSettings.ClinicalSummaryFamilyHistory = clsSettings.ClinicalSummaryHistory.VisitSpecific
            End If

            If rb_ClinicalSummSocialHst_CurrentHistory.Checked = True Then
                objSettings.ClinicalSummarySocialHistory = clsSettings.ClinicalSummaryHistory.Current
            ElseIf rb_ClinicalSummSocialHst_VisitSpecific.Checked = True Then
                objSettings.ClinicalSummarySocialHistory = clsSettings.ClinicalSummaryHistory.VisitSpecific
            End If

            If chkVisitProcedure.Checked = True Then
                objSettings.ClinicalSummaryProcedure = clsSettings.ClinicalSummaryHistory.VisitSpecific
            Else
                objSettings.ClinicalSummaryProcedure = clsSettings.ClinicalSummaryHistory.Current
            End If
            If chkVisitVital.Checked = True Then
                objSettings.ClinicalSummaryVital = clsSettings.ClinicalSummaryHistory.VisitSpecific
            Else
                objSettings.ClinicalSummaryVital = clsSettings.ClinicalSummaryHistory.Current
            End If
            '' Mahesh 20080809 Advanced Growth Chart
            objSettings.ShowAdvancedGrowthChart = chk_GrowthChart.Checked
            ''
            'If txtCDS_PESUrl.Text.Trim.Length > 0 Then
            '    objSettings.CDS_PES_Url = txtCDS_PESUrl.Text.Trim
            'End If
            'If txtCDS_PESUserName.Text.Trim.Length > 0 Then
            '    objSettings.CDS_PES_UserName = txtCDS_PESUserName.Text.Trim
            'End If
            'If txtCDS_PESPassword.Text.Trim.Length > 0 Then
            '    objSettings.CDS_PES_Password = txtCDS_PESPassword.Text.Trim
            'End If
            'If (chkEnabledCDS.Checked = True) Then
            '    objSettings.CDS_PES_Enabled = "TRUE"
            'Else
            '    objSettings.CDS_PES_Enabled = "FALSE"
            'End If
            '' SUDHIR 20090721 '' GROWTH CHART PERCENTILE ''
            If rbShowPercentile.Checked Then
                objSettings.GrowthChartPercentile = enumGrowthChartPercentile.ShowPercentile
            ElseIf rbShowPercentileOnMouseHoover.Checked Then
                objSettings.GrowthChartPercentile = enumGrowthChartPercentile.ShowPercentileOnMouseHoover
            Else
                objSettings.GrowthChartPercentile = enumGrowthChartPercentile.DontShowPercentile
            End If
            '' END SUDHIR ''

            ''by SUDHIR 20081111 
            'Show Age in Days
            objSettings.ShowAgeInDays = chk_AgeFlag.Checked
            'Age Limit
            objSettings.AgeLimit = CLng(txtAgeLimitPatientStrip.Text)
            ''sudhir 20081124
            'Age limit for weeks in months
            objSettings.AgeLimitForWeeks = CLng(txtAgeLimitforWeeks.Text)
            ''

            ''Start :: dhruv
            objSettings.IsPediatrics = chkPediatrics.Checked  ''PEDIATRIC SETTINGS
            objSettings.IsAutogeneratedProblemFromExam = chkAutoGenerateProblems.Checked
            objSettings.IsrequireSNOMED = chkProblemSNOMED.Checked
            objSettings.IsPatDaemoMerge = chkpatdaemomerg.Checked ''PATIENT DEMOGRAPHIC SETTINGS
            ''End :: Dhruv
            'objSettings.SendUnassociatedDiagnosis = chkSendUnassociatedDiagnosis.Checked 'line of code commented by manoj jadhav on 20120730 for moving service setiings to Respective Service
            ''Start :: dhruv
            objSettings.IsYesNoLab = chkYesNoLabs.Checked '' ''YES/No Labs SETTINGS
            ''End :: Dhruv
            ''VITALS HEIGHT COPY FORWARD
            objSettings.IsVitalsHeightCopyForward = ChkVitalsHeightCopy.Checked
            '' SUDHIR 20090821 '' 
            objSettings.SetCPTtoAllICD9 = chkSetCPTtoAllICD9.Checked
            objSettings.SetProblemDxAsDefaultForSmartDx = chkDefaultProblemDxForSmartDx.Checked
            objSettings.DefaultDxCPTPatientProblemDiagnosis = ChkDefaultDxCPTPatientPrbDx.Checked
            objSettings.EnableSmartDxReviewScreen = chkEnableSmartDxReviewScreen.Checked

            '' END SUDHIR ''
            ''Start :: Dhruv
            ''Added Rahul for Unauthenticated Login Banner on 20101020
            objSettings.UnAuthLogin = txtUnAuthLogBanner.Text.Trim()
            ''End
            ''End :: Dhruv
            'sarika 
            'DMS 20080908 -- for Loading no of recieved faxes in DMS
            objSettings.LoadNoOfFaxes = numLoadNoOfFaxes.Value
            '--------------

            objSettings.SignatureText = Convert.ToString(txtSignaturetext.Text.Trim())
            objSettings.CoSignatureText = Convert.ToString(txtcoSignaturetext.Text.Trim())
            ''Sandip Darade 20090731
            If (gstrAdminFor = "gloPM") Then

                SavegloEMRDatabaseSettings(dtSaveSettings)
                'sarika PM DB Credentials
                'If chk_PMDBSettings.Checked = False Then
                '    objSettings.PMAddPatient = 0
                'Else
                '    objSettings.PMAddPatient = 1
                'End If
            Else
                objSettings.PMAddPatient = chk_PMDBSettings.Checked
                ''Sandip Darade  if not selected to add the setting
                ' If (objSettings.PMAddPatient = True) Then
                objSettings.PMServerName = txtPMServerName.Text.Trim
                objSettings.PMDatabaseName = txtPMDatabaseName.Text.Trim
                objSettings.PMUserID = txtSQLUserID.Text.Trim
                objSettings.PMSQLPwd = txtSQLPassword.Text.Trim
                'End If
            End If
            If rbChiefComplaint.Checked = True Then
                objSettings.EMChiefComplaintType = "ChiefComplaint"
            ElseIf rbProblemList.Checked = True Then
                objSettings.EMChiefComplaintType = "ProblemList"
            End If

            'Sanjog 20100615-start
            If rbt_EMYes.Checked = True Then
                objSettings.EMOnOff = "1"
            ElseIf rbt_EMNo.Checked = True Then
                objSettings.EMOnOff = "0"
            End If


            ''added by Ujwala as on 02032015 to Store gloServices DB settings 
            'saving Services database settings
            If txtSrvcServerName.Text.Trim = "" Then
                MessageBox.Show("Enter Services database settings Server Name.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                tb_Settings.SelectTab(tb_Settings.TabPages.IndexOf(tbpg_ReportServerSettings))
                txtSrvcServerName.Focus()
                Exit Function
            End If
            If txtSrvcDatabaseName.Text.Trim = "" Then
                MessageBox.Show("Enter Services database settings Database Name.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                tb_Settings.SelectTab(tb_Settings.TabPages.IndexOf(tbpg_ReportServerSettings))
                txtSrvcDatabaseName.Focus()
                Exit Function
            End If
            If optSrvcSQLAuthentication.Checked = True Then
                If txtSrvcSQLUserID.Text.Trim = "" Then
                    MessageBox.Show("Enter Services database settings SQL User ID", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    tb_Settings.SelectTab(tb_Settings.TabPages.IndexOf(tbpg_ReportServerSettings))
                    txtSrvcSQLUserID.Focus()
                    Exit Function
                End If
                If txtSrvcSQLPassword.Text.Trim = "" Then
                    MessageBox.Show("Enter Services database settings SQL Password", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    tb_Settings.SelectTab(tb_Settings.TabPages.IndexOf(tbpg_ReportServerSettings))
                    txtSrvcSQLPassword.Focus()
                    Exit Function
                End If
                If objSQLSettings.IsSQLConnect(txtSrvcServerName.Text.Trim, txtSrvcDatabaseName.Text.Trim, txtSrvcSQLUserID.Text.Trim, txtSrvcSQLPassword.Text.Trim) = False Then
                    If MessageBox.Show("Unable to connect to Services database settings SQL Server " & txtSrvcServerName.Text.Trim & " and Database " & txtSrvcDatabaseName.Text.Trim & vbCrLf & "Select valid credentials.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information) Then
                        tb_Settings.SelectTab(tb_Settings.TabPages.IndexOf(tbpg_ReportServerSettings))
                        Exit Function
                    End If
                End If
            Else
                If objSQLSettings.IsConnect(txtSrvcServerName.Text.Trim, txtSrvcDatabaseName.Text.Trim, False, "", "") = False Then
                    If MessageBox.Show("Unable to connect to Services database settings SQL Server " & txtSrvcServerName.Text.Trim & " and Database " & txtSrvcDatabaseName.Text.Trim & vbCrLf & "Select valid credentials.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information) Then
                        tb_Settings.SelectTab(tb_Settings.TabPages.IndexOf(tbpg_ReportServerSettings))
                        Exit Function
                    End If
                End If
            End If

            objSettings.ServicesServerName = txtSrvcServerName.Text.Trim
            objSettings.ServicesDatabaseName = txtSrvcDatabaseName.Text.Trim
            objSettings.ServicesUserID = txtSrvcSQLUserID.Text.Trim
            '''''''''''''Set global variable '''''''''''''''
            gstrServicesDBName = txtSrvcDatabaseName.Text.Trim
            strgloServiceDatabaseName = gstrServicesDBName
            '''''''''''''Set global variable '''''''''''''''

            ''Start ::(Encryption)RxNormPassword Encryption
            Dim _gloServicesPassWord As String = String.Empty
            Dim objgloServicesEncryptions As New clsEncryption
            If Not IsNothing(objgloServicesEncryptions) Then
                _gloServicesPassWord = objgloServicesEncryptions.EncryptToBase64String(txtSrvcSQLPassword.Text, mdlGeneral.constEncryptDecryptKey)
                objgloServicesEncryptions = Nothing
            End If
            objSettings.ServicesPassword = _gloServicesPassWord

            objSettings.ServicesAuthentication = optSrvcSQLAuthentication.Checked

            gstrServicesServerName = objSettings.ServicesServerName
            gstrServicesUserID = objSettings.ServicesUserID
            gstrServicesPassWord = txtSrvcSQLPassword.Text
            gbServicesIsSQLAUTHEN = objSettings.ServicesAuthentication
            'end by Ujwala as on 02032015 to Store gloServices DB settings 


            'Update eRx Web service Setting in gloservices Database
            Dim sgloServiceConnectionpdmp As String = gloEMRAdmin.mdlGeneral.GetConnectionString(gstrServicesServerName, gstrServicesDBName, gbServicesIsSQLAUTHEN, gstrServicesUserID, gstrServicesPassWord)

            If sTemppdmpURL <> txtPDMPUrl.Text Then
                UpdateeRxWebserviceSetting(sgloServiceConnectionpdmp, "PDMPService", txtPDMPUrl.Text)

            End If

            If chkSurescript.Checked = True Then
                Dim strmessage As String = ""
                Dim blnvalueChange As Boolean = False
                If TempIs8dot1PendingRefReqComplete <> Is8dot1PendingRefReqComplete Then
                    strmessage = "eRx web service Url and 4.5 Directory Download settings will be updated in gloServices Database."
                    blnvalueChange = True
                ElseIf sTempeRxserviceURl <> TxtSurescriptURL.Text Then
                    strmessage = "eRx web service Url will be updated in gloServices Database."
                    blnvalueChange = True
                End If

                If blnvalueChange = True Then
                    MessageBox.Show(strmessage, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    ''Added strgloServiceDatabaseName by Ujwala on 20022015 to get ServicesDB Name from settings table            
                    ''Dim sgloServiceConnection As String = gloEMRAdmin.mdlGeneral.GetConnectionString(gstrSQLServerName, "gloServices", gblnSQLAuthentication, gstrSQLUserEMR, gstrSQLPasswordEMR)
                    Dim sgloServiceConnection As String = gloEMRAdmin.mdlGeneral.GetConnectionString(gstrServicesServerName, gstrServicesDBName, gbServicesIsSQLAUTHEN, gstrServicesUserID, gstrServicesPassWord)

                    If TempIs8dot1PendingRefReqComplete <> Is8dot1PendingRefReqComplete Then
                        UpdateeRxWebserviceSetting(sgloServiceConnection, "EnableDirectory4dot4", Is8dot1PendingRefReqComplete)
                    End If
                    If rbStaging.Checked = True Then
                        If chkNCPDPVer10dot6.Checked = True Then
                            UpdateeRxWebserviceSetting(sgloServiceConnection, "eRx10dot6StagingWebserviceURL", TxtSurescriptURL.Text)
                        Else
                            UpdateeRxWebserviceSetting(sgloServiceConnection, "eRxStagingWebserviceURL", TxtSurescriptURL.Text)
                        End If

                    Else
                        If chkNCPDPVer10dot6.Checked = True Then
                            UpdateeRxWebserviceSetting(sgloServiceConnection, "eRx10dot6ProductionWebserviceURL", TxtSurescriptURL.Text)
                        Else
                            UpdateeRxWebserviceSetting(sgloServiceConnection, "eRxProductionWebserviceURL", TxtSurescriptURL.Text)
                        End If

                    End If

                End If
            End If


            ''Added strgloServiceDatabaseName by Ujwala on 20022015 to get ServicesDB Name from settings table            
            ''Dim sgloServiceConnectionSecure As String = gloEMRAdmin.mdlGeneral.GetConnectionString(gstrSQLServerName, "gloServices", gblnSQLAuthentication, gstrSQLUserEMR, gstrSQLPasswordEMR)
            Dim sgloServiceConnectionSecure As String = gloEMRAdmin.mdlGeneral.GetConnectionString(gstrServicesServerName, strgloServiceDatabaseName, gbServicesIsSQLAUTHEN, objSettings.ServicesUserID, txtSrvcSQLPassword.Text)



            If chkNCPDPVer10dot6.Checked Then
                UpdateeRxWebserviceSetting(sgloServiceConnectionSecure, "ENABLE_10DOT6", "True")
            Else
                UpdateeRxWebserviceSetting(sgloServiceConnectionSecure, "ENABLE_10DOT6", "False")
            End If

            If chkNCPDPVer10dot6.Checked And chkSecureMesaage.Checked Then
                UpdateeRxWebserviceSetting(sgloServiceConnectionSecure, "ENABLE_SECUREMESSAGE", "True")
            Else
                UpdateeRxWebserviceSetting(sgloServiceConnectionSecure, "ENABLE_SECUREMESSAGE", "False")
            End If



            '----x--
            '''' Update gloSetting database with new value of secure message Url start
            If sTempeSecureMsgURl <> txtSecureMessageURL.Text Then
                MessageBox.Show("Secure Message Url will be updated in gloServices Database. ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)

                ''Added strgloServiceDatabaseName by Ujwala on 20022015 to get ServicesDB Name from settings table            
                ''Dim sgloServiceConnection As String = gloEMRAdmin.mdlGeneral.GetConnectionString(gstrSQLServerName, "gloServices", gblnSQLAuthentication, gstrSQLUserEMR, gstrSQLPasswordEMR)
                Dim sgloServiceConnection As String = gloEMRAdmin.mdlGeneral.GetConnectionString(gstrServicesServerName, strgloServiceDatabaseName, gbServicesIsSQLAUTHEN, objSettings.ServicesUserID, txtSrvcSQLPassword.Text)

                If rbSecureStaging.Checked = True Then

                    UpdateeRxWebserviceSetting(sgloServiceConnection, "SECUREMSGSTAGINGWEBSERVICEURL", txtSecureMessageURL.Text)
                Else

                    UpdateeRxWebserviceSetting(sgloServiceConnection, "SECUREMSGPRODUCTIONWEBSERVICEURL", txtSecureMessageURL.Text)
                End If
            End If

            '''' Update gloSetting database with new value of secure message Url End
            'Sanjog 20100615-start






            ' ''Sandip Darade 
            'If (objSettings.PMAddPatient = True) Then
            '    If optSQLAuthentication.Checked = False Then
            '        objSettings.PMSQLAuthentication = 0
            objSettings.PMConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString(txtPMServerName.Text.Trim, txtPMDatabaseName.Text.Trim, False, "", "")
            '        'objSettings.PMUserID = ""
            '        'objSettings.PMSQLPwd = ""
            '    Else
            '        objSettings.PMSQLAuthentication = 1
            '        objSettings.PMConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString(txtPMServerName.Text.Trim, txtPMDatabaseName.Text.Trim, txtSQLUserID.Text.Trim, txtSQLPassword.Text.Trim)
            '        'Dim objEncryptDecrypt As New clsEncryption
            '        '  objSettings.PMSQLPwd = objEncryptDecrypt.EncryptToBase64String(txtSQLPassword.Text.Trim, constEncryptDecryptKey)
            '        ' objEncryptDecrypt = Nothing
            '    End If
            'End If

            'sarika SendEMail 20090502
            'If chkSendMail.Checked = True Then
            '    objSettings.IsSendEMail = True
            '    objSettings.SendEmailAddress = txtSendMail.Text.Trim
            'Else
            '    objSettings.IsSendEMail = False
            '    objSettings.SendEmailAddress = ""
            'End If
            '--

            If chk_PMDBSettings.Checked = True Then

                '' Sudhir 20090108 ''
                '' TO SAVE PROVIDER EXTERNAL IDs FOR gloEMR & gloPM FROM c1Provider GRID ''
                'Dim oDB As New gloStream.gloDataBase.gloDataBase

                Dim _sqlEMRConnection As SqlConnection
                Dim _sqlPMConnection As SqlConnection
                Dim _sqlEMRTransaction As SqlTransaction = Nothing
                Dim _sqlPMTransaction As SqlTransaction = Nothing
                Dim _sqlCommand As SqlCommand

                Dim _sqlQuery As String
                Dim _rowDetail As New myList
                Dim _ExternalIDPresent As Boolean = False

                ''Get arraylist contain row detail of c1Provider.
                Dim _oProviderRows As ArrayList = GetC1RowProviderDetail()

                _sqlEMRConnection = New SqlConnection(mdlGeneral.GetConnectionString)
                _sqlPMConnection = New SqlConnection(objSettings.PMConnectionString)



                Try
                    ''VALIDATION FOR CHECKING MULTIPLE EXTERNAL IDs --COMMENTED (NOT USED)
                    'For i As Integer = 0 To _oProviderRows.Count - 1
                    '    _rowDetail = _oProviderRows.Item(i)
                    '    If IsExternalIDPresent(_rowDetail, objSettings.PMConnectionString) = True Then
                    '        MessageBox.Show("External ID cannot be repeated", mdlGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    '        Exit Sub
                    '    End If
                    'Next


                    _sqlEMRConnection.Open()
                    _sqlPMConnection.Open()

                    _sqlEMRTransaction = _sqlEMRConnection.BeginTransaction()
                    _sqlPMTransaction = _sqlPMConnection.BeginTransaction()

                    '' To CLEAN ALL EXTERNAL IDs FROM gloPM PROVIDER MASTER
                    _sqlQuery = "UPDATE Provider_MST SET sExternalCode = ''"
                    _sqlCommand = New SqlCommand(_sqlQuery, _sqlPMConnection, _sqlPMTransaction)
                    _sqlCommand.ExecuteNonQuery()
                    ''

                    _rowDetail = Nothing

                    '' Save Provider External IDs in both gloEMR and gloPM


                    For i As Integer = 0 To _oProviderRows.Count - 1
                        _rowDetail = _oProviderRows.Item(i)
                        _ExternalIDPresent = False

                        If _rowDetail.ExternalID = Nothing Then
                            _rowDetail.ExternalID = ""
                        End If

                        '_sqlQuery = "UPDATE Provider_MST SET sExternalCode='" & _rowDetail.ExternalID & "' WHERE sLastName+'  '+sFirstName = '" & _rowDetail.gloEMR_Provider & "'"
                        _sqlQuery = "UPDATE Provider_MST SET sExternalCode='" & _rowDetail.ExternalID & "' WHERE rtrim(ltrim(sLastName+ SPACE(2) + sFirstName)) = '" & _rowDetail.gloEMR_Provider & "'"
                        _sqlCommand = New SqlCommand(_sqlQuery, _sqlEMRConnection, _sqlEMRTransaction)
                        _sqlCommand.ExecuteNonQuery()

                        If _rowDetail.ExternalID <> "" Then
                            '_sqlQuery = "UPDATE Provider_MST SET sExternalCode='" & _rowDetail.ExternalID & "' WHERE sLastName+'  '+sFirstName = '" & _rowDetail.gloPM_Provider & "'"
                            _sqlQuery = "UPDATE Provider_MST SET sExternalCode='" & _rowDetail.ExternalID & "' WHERE rtrim(ltrim(sLastName + SPACE(2)+ sFirstName)) = '" & _rowDetail.gloPM_Provider & "'"
                            _sqlCommand = New SqlCommand(_sqlQuery, _sqlPMConnection, _sqlPMTransaction)
                            _sqlCommand.ExecuteNonQuery()
                        End If

                        _rowDetail = Nothing
                    Next

                    _sqlEMRTransaction.Commit()
                    _sqlPMTransaction.Commit()

                    _sqlEMRConnection.Close()
                    _sqlEMRConnection.Dispose()
                    _sqlEMRConnection = Nothing

                    _sqlPMConnection.Close()
                    _sqlPMConnection.Dispose()
                    _sqlPMConnection = Nothing

                Catch ex As Exception
                    _sqlEMRTransaction.Rollback()
                    _sqlPMTransaction.Rollback()
                End Try
                '' End Sudhir ''

                'Else
                '    objSettings.PMServerName = ""
                '    objSettings.PMDatabaseName = ""
                '    objSettings.PMSQLAuthentication = 0
                '    objSettings.PMConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString(txtPMServerName.Text.Trim, txtPMDatabaseName.Text.Trim)
                '    objSettings.PMUserID = ""
                '    objSettings.PMSQLPwd = ""
                '    objSettings.PMConnectionString = ""
            End If
            ''Start :: Dhruv
            '''''''''''''''Integrated by Mayuri:20100731 - For EM Coding Changes - Admin Settings
            SaveEMSettings(dtSaveSettings)
            '''''''''''''''Integrated by Mayuri:20100731 - For EM Coding Changes - Admin Settings
            ''End :: Dhruv
            ''Sandip Darade 
            If (objSettings.PMAddPatient = True) Then
                If optSQLAuthentication.Checked = False Then
                    objSettings.PMSQLAuthentication = 0
                    objSettings.PMConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString(txtPMServerName.Text.Trim, txtPMDatabaseName.Text.Trim, False, "", "")
                    'objSettings.PMUserID = ""
                    'objSettings.PMSQLPwd = ""
                Else
                    objSettings.PMSQLAuthentication = 1
                    objSettings.PMConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString(txtPMServerName.Text.Trim, txtPMDatabaseName.Text.Trim, txtSQLUserID.Text.Trim, txtSQLPassword.Text.Trim)
                    'Dim objEncryptDecrypt As New clsEncryption
                    '  objSettings.PMSQLPwd = objEncryptDecrypt.EncryptToBase64String(txtSQLPassword.Text.Trim, constEncryptDecryptKey)
                    ' objEncryptDecrypt = Nothing
                End If
            Else
                objSettings.PMConnectionString = ""

            End If

            objSettings.DMSImageDIP = CType(numDMSImageDPI.Value, Int32)
            objSettings.UseFileCompression = chkUseFileCompession.Checked

            '--

            'COMMENTED BY SHUBHANGI 20100507
            '\\Suraj 20090123

            ' objSettings.SplitDocument = chkSplitDoc.Checked
            '\\suraj 20090128
            objSettings.RecoveryDMSV2Doc = chkRecoverDMSV2Doc.Checked
            'objSettings.RecoveryDMSV2Path = txt_DMSV2RecoveryPath.Text
            If chkRecoverDMSV2Doc.Checked = True Then
                If txt_DMSV2RecoveryPath.Text.Trim.Length > 0 Then
                    If Directory.Exists(txt_DMSV2RecoveryPath.Text.Trim()) = True Then
                        objSettings.RecoveryDMSV2Path = txt_DMSV2RecoveryPath.Text.Trim
                    Else
                        MessageBox.Show("DMS V2 Physical Document Path is not valid. Enter the valid Path.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        tb_Settings.SelectTab(tb_Settings.TabPages.IndexOf(tbp_OMRSettings))
                        txt_DMSV2RecoveryPath.Focus()
                        Exit Function
                    End If
                Else
                    MessageBox.Show("DMS V2 Physical Document Path is not entered.Enter the valid Path.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    tb_Settings.SelectTab(tb_Settings.TabPages.IndexOf(tbp_OMRSettings))
                    txt_DMSV2RecoveryPath.Focus()
                    Exit Function
                End If
            End If
            ''
            '\\suraj 20090128  Delete DMS Doc after Migration
            objSettings.DeleteDMSDocAfterMigration = chk_DeleteDocAfterMigration.Checked
            objSettings.OccMedDMSCategory = ""
            If cmbOccDmsCategory.Items.Count = 1 Then
                If CType(cmbOccDmsCategory.Items(0), DataRowView).Row.ItemArray(0) = "-1" Then
                Else
                    For i As Integer = 0 To cmbOccDmsCategory.Items.Count - 1
                        If i < cmbOccDmsCategory.Items.Count - 1 Then
                            objSettings.OccMedDMSCategory += CType(cmbOccDmsCategory.Items(i), DataRowView).Row.ItemArray(0) + ","
                        Else
                            objSettings.OccMedDMSCategory += CType(cmbOccDmsCategory.Items(i), DataRowView).Row.ItemArray(0)
                        End If
                    Next
                End If
            Else
                For i As Integer = 0 To cmbOccDmsCategory.Items.Count - 1
                    If i < cmbOccDmsCategory.Items.Count - 1 Then
                        objSettings.OccMedDMSCategory += CType(cmbOccDmsCategory.Items(i), DataRowView).Row.ItemArray(0) + ","
                    Else
                        objSettings.OccMedDMSCategory += CType(cmbOccDmsCategory.Items(i), DataRowView).Row.ItemArray(0)
                    End If
                Next
            End If


            'Sandip Darade  20090210 Use Coded History 
            objSettings.Usecodedhistory = Chb_UseCodedhistory.Checked
            'Sandip Darade  20090328 Show Coded History code,Description or both 
            'If (Chb_UseCodedhistory.Checked = True) Then
            If (Rbtn_showcode.Checked = True) Then
                objSettings.ShowCodedHistory = "Code"

            ElseIf (Rbtn_showDesc.Checked = True) Then
                objSettings.ShowCodedHistory = "Description"
            Else

                objSettings.ShowCodedHistory = "Both"
            End If

            If (rbOptional.Checked = True) Then
                objSettings.CodeFieldsinHistory = "CodeOptional"

            ElseIf (rbMandatory.Checked = True) Then
                objSettings.CodeFieldsinHistory = "CodeMandatory"
            ElseIf (rbWarning.Checked = True) Then
                objSettings.CodeFieldsinHistory = "CodeWarning"
            End If

            'OTC Warrning
            objSettings.OTCIssueWarning = chkOTCIssueWarning.Checked

            'Sanjog
            If chkSmokingStatusColumn.Checked Then
                objSettings.ShowSmokingColumn = 1
            Else
                objSettings.ShowSmokingColumn = 0
            End If
            'Sanjog
            'Sanjog DAS Setings'
            If chkRetrieveESRValue.Checked Then
                objSettings.IsRetrieveESR = 1
                objSettings.ESRDay = txtESRDays.Text
            Else
                objSettings.IsRetrieveESR = 0
                objSettings.ESRDay = 0
            End If

            If chkRetrieveCRPValue.Checked Then
                objSettings.IsRetrieveCRP = 1
                objSettings.CRPDay = txtCRPDays.Text
            Else
                objSettings.IsRetrieveCRP = 0
                objSettings.CRPDay = 0
            End If

            'Sanjog DAS Setings'
            'shubhangi 20090306'
            objSettings.OtherPatientType = chkOtherPatientType.Checked


            'End Shubhangi'
            ''IMReminderDays Settings
            objSettings.IM_ReminderDays = NumUpDn_ImRminder.Value

            'Sandip Darade  20090622
            'Default patient gender setting
            objSettings.DefaultPatientGender = cmbGender.Text

            ''Sandip Darade 20090709
            ''Add billing setting 
            ''Sandip Darade 20091107
            If (gstrAdminFor = "gloPM") Then
                Save_BillingSettings()
                ''Add Other billing setting 
                If cmbSpeciality.SelectedIndex > 0 Then
                    objSettings.DefaultFeeSpeciality = cmbSpeciality.SelectedValue
                Else
                    objSettings.DefaultFeeSpeciality = 0

                End If
                If txtCarrierNumber.Text <> "" Then
                    objSettings.DefaultCarrierNumber = Convert.ToString(txtCarrierNumber.Text)
                Else
                    objSettings.DefaultCarrierNumber = Convert.ToString(txtCarrierNumber.Text)

                End If

                If txtCarrierNumber.Text <> "" Then
                    objSettings.DefaultLocality = Convert.ToString(txtLocality.Text)
                Else
                    objSettings.DefaultLocality = 0
                End If

                If cmb_Feeschedules.SelectedIndex <> -1 Then
                    objSettings.ClinicFeeSchedule = Convert.ToString(cmb_Feeschedules.SelectedValue)
                Else
                    objSettings.ClinicFeeSchedule = 0

                End If
                objSettings.NoOfClaimPerBatch = numUpDn_NoOfClaims.Value.ToString()
                objSettings.NoOfModifiers = numModifiers.Value.ToString()

                'objSettings.ShowLabCol = chbox_AddShowLabCol.Checked.ToString()
                If (chbox_AddShowLabCol.Checked = True) Then
                    objSettings.ShowLabCol = "1"
                Else
                    objSettings.ShowLabCol = "0"

                End If

                Dim _defaultFeeSchedule As Integer = 0

                If cmbFacilityType.Text = FacilityType.Facility.ToString() Then
                    _defaultFeeSchedule = FacilityType.Facility.GetHashCode()
                ElseIf cmbFacilityType.Text = FacilityType.NonFacility.ToString() Then
                    _defaultFeeSchedule = FacilityType.NonFacility.GetHashCode()
                End If
                objSettings.Defaultfeecharges = _defaultFeeSchedule.ToString()
                ''End add other billing setting 
                ''Save Mariatal Status Settings
                SaveMaritalStatusSettings(dtSaveSettings)
                ''Save Exchange server settings
                AddExServerSetting(dtSaveSettings)

                ''Save alphaII settings
                SaveAlphaIIDatabaseSettings(dtSaveSettings)
            End If
            'Shubhangi
            ''Start :: Dhruv
            'pradeep 20101011
            objSettings.UseSignatureDelegates = Chk_UseSignatureDelegates.Checked
            ''End :: Dhruv

            'End
            AddOtherSettings(dtSaveSettings)
            ''Add weekdayssetting
            '' Setting Added By Mayuri - 20090827
            '' ICD9-CPT Driven Settings
            objSettings.ICD9Driven = rbICD9Driven.Checked
            objSettings.Show8ICD9 = rbShow8ICD9.Checked
            objSettings.Show4Modifier = rbShow4Modifier.Checked

            '' Precription provider
            'objSettings.PrescriptionProviderAssociation = chkPrescriptionProvider.Checked// removed setting in 7020 as per PRD discussion for Incident #00006175
            '' Patient Questionnaire
            objSettings.PatientQuestionnaire = chkPatientQuestionnaire.Checked
            '' END Setting Added By Mayuri - 20090827
            '''''''''''Genuis Path settings 

            If IsNothing(objSettings.GeniusCode) = False Then
                'CType(cmbGeniusPaths.SelectedItem, ClsGeniusPath).GeniusCode = regKey.GetValue("GeniusCode")
                'cmbGeniusPaths.Text = regKey.GetValue("GeniusCode")
                gstrGeniusCode = objSettings.GeniusCode
            End If
            If Not IsNothing(cmbGeniusPaths.SelectedItem) Then
                'objSettings.GeniusCode = CType(cmbGeniusPaths.SelectedItem, ClsGeniusPath).GeniusCode
                'objSettings.GeniusPath = CType(cmbGeniusPaths.SelectedItem, ClsGeniusPath).GeniusPath
                objSettings.GeniusCode = cmbGeniusPaths.SelectedValue
                objSettings.GeniusPath = CType(cmbGeniusPaths.SelectedItem, ClsGeniusPath).GeniusPath
            Else
                'MessageBox.Show("Please enter Genius Path")
                'Exit Sub
            End If
            Dim appSettings As System.Collections.Specialized.NameValueCollection = System.Configuration.ConfigurationSettings.AppSettings
            'Sandip Darade 2001007
            If Not IsNothing(cmbCountry.SelectedValue) Then
                objSettings.Country = cmbCountry.SelectedValue
            Else
                objSettings.Country = ""
            End If
            gstrCountry = objSettings.Country
            appSettings("Country") = gstrCountry

            If Not IsNothing(cmbSameDayApptType.SelectedValue) Then
                objSettings.SameDayApptType = cmbSameDayApptType.SelectedValue
            Else
                objSettings.SameDayApptType = ""
            End If

            If Not IsNothing(cmbFutureApptType.SelectedValue) Then
                objSettings.FutureApptType = cmbFutureApptType.SelectedValue
            Else
                objSettings.FutureApptType = ""
            End If

            '''''''''''Genuis Path settings 
            ''Shweta 20100125
            'Added Emdeon Settings
            '' GLO2011-0012776
            '' Removed default user from the validation of emdeon settings.
            'If (txtEmdeonUserName.Text <> "" Or txtEmdeonPassword.Text <> "" Or txtEmdeonUrl.Text <> "" Or txtEmdeonFacilityCode.Text <> "" Or txtEmdeon_DefaultUser.Text <> "") = True Then
            If (txtEmdeonUserName.Text.Trim <> "" Or txtEmdeonPassword.Text.Trim <> "" Or txtEmdeonUrl.Text.Trim <> "" Or txtEmdeonFacilityCode.Text.Trim <> "") = True Then
                If _gloLab_settingsEdited Then
                    If Not IsNothing(tb_Settings.SelectedTab.Tag) Then
                        If tb_Settings.SelectedTab.Tag.ToString().ToUpper() <> "LabSettings".ToUpper() Then
                            tb_Settings.SelectTab(tbp_EmdeonSettings.Name)
                        End If
                    Else
                        tb_Settings.SelectTab(tbp_EmdeonSettings.Name)
                    End If
                End If
                If txtEmdeonUserName.Text.Trim = "" Then
                    MessageBox.Show("User name is not entered. Please enter the valid user name in lab settings.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    tb_Settings.SelectTab(tb_Settings.TabPages.IndexOf(tbp_EmdeonSettings))
                    txtEmdeonUserName.Focus()
                    Exit Function
                End If
                If txtEmdeonPassword.Text.Trim = "" Then
                    MessageBox.Show("Password is not entered. Please enter the valid password in lab settings.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    tb_Settings.SelectTab(tb_Settings.TabPages.IndexOf(tbp_EmdeonSettings))
                    txtEmdeonPassword.Focus()
                    Exit Function
                End If
                If txtEmdeonFacilityCode.Text.Trim = "" Then
                    MessageBox.Show("Facility code is not entered. Please enter the valid facility code in lab settings.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    tb_Settings.SelectTab(tb_Settings.TabPages.IndexOf(tbp_EmdeonSettings))
                    txtEmdeonFacilityCode.Focus()
                    Exit Function
                End If
                If txtEmdeonUrl.Text.Trim = "" Then
                    MessageBox.Show("URL is not entered. Please enter the valid URL in lab settings.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    tb_Settings.SelectTab(tb_Settings.TabPages.IndexOf(tbp_EmdeonSettings))
                    txtEmdeonUrl.Focus()
                    Exit Function
                End If
                If txtEmdeon_DefaultUser.Text.Trim = "" Then
                    '' GLO2011-0012776 
                    '' Message box text changed.
                    MessageBox.Show("Please select default user for unmatched lab tasks in lab settings.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                    If Not IsNothing(tb_Settings.SelectedTab.Tag) Then
                        If tb_Settings.SelectedTab.Tag.ToString().ToUpper() <> "LabSettings".ToUpper() Then
                            ' tb_Settings.SelectTab(tbp_EmdeonSettings.Name)
                            tb_Settings.SelectTab(tb_Settings.TabPages.IndexOf(tbp_EmdeonSettings))
                        End If
                    Else
                        'tb_Settings.SelectTab(tbp_EmdeonSettings.Name)
                        tb_Settings.SelectTab(tb_Settings.TabPages.IndexOf(tbp_EmdeonSettings))
                    End If

                    txtEmdeon_DefaultUser.Focus()

                    Exit Function
                End If
            End If

            'Removed on 20100219-Madan
            'objSettings.GloLab_Pat = chk_glolab_pat.CheckState

            If (txtEmdeonUserName.Text.Trim = "" Or txtEmdeonPassword.Text.Trim = "" Or txtEmdeonUrl.Text.Trim = "" Or txtEmdeonFacilityCode.Text.Trim = "" Or txtEmdeon_DefaultUser.Text.Trim = "") = False Then
                objSettings.EmdeonUserName = txtEmdeonUserName.Text.Trim()
                objSettings.EmdeonPassword = objEncrypt.EncryptToBase64String(txtEmdeonPassword.Text.Trim(), constEncryptDecryptKey_Services)
                Dim URL As String
                URL = txtEmdeonUrl.Text.Trim()
                'checks the url ends with "\","/" if it ends it will reomve those symbols.
                While URL.EndsWith("\") Or URL.EndsWith("/")

                    URL = URL.Substring(0, URL.Length - 1)

                End While
                If rbEmdeonAsk.Checked = True Then
                    objSettings.GloLab_Billing = rbEmdeonAsk.Text
                ElseIf rbEmdeonNo.Checked = True Then
                    objSettings.GloLab_Billing = rbEmdeonNo.Text
                ElseIf rbEmdeonYes.Checked = True Then
                    objSettings.GloLab_Billing = rbEmdeonYes.Text

                End If
                ''Start:          Dhruv 
                'Commented by madan on 20100826
                ''objSettings.GloLab_defaultUser = _gloLab_defaultUserName
                'If _gloLab_DefaultUserID <> 0 Then
                '    objSettings.GloLab_defaultUserID = _gloLab_DefaultUserID
                'End If
                'End madan changes.
                ''End :: Dhruv

                objSettings.EmdeonURL = URL
                objSettings.EmdeonFacilityCode = txtEmdeonFacilityCode.Text.Trim()
                'Removed on 22/02/2010 regarding gloLab-by Madan
                'objSettings.EmdeonDescription = txtEmdeonDescription.Text.Trim()
                'End
                If _gloLab_settingsEdited = True Then 'If value is changed then... settings will be downloaded from Emdeon.
                    'Madan- 20100213 Added for gloLab hsi_label retrivel
                    Dim glolab As clsgloLabSettings = New clsgloLabSettings()
                    ' If gloLab_hsi.ToString = "" Then
                    'If GloLab_Hsilb is null then it will retrive hsi label value from emdeon.
                    Dim _gloLabHsi As String = ""
                    _gloLabHsi = glolab.getHsiLabel(txtEmdeonUserName.Text.Trim(), txtEmdeonPassword.Text.Trim(), txtEmdeonFacilityCode.Text.Trim(), txtEmdeonUrl.Text.Trim())

                    If _gloLabHsi = "false" Then
                        'MessageBox.Show("The user name and password entered are not accepted by the Labs service." + vbCrLf + "Please enter a valid user name and password combination.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                        '' GLO2011-0012776 
                        '' Message box text changed
                        'MessageBox.Show("The user name and password entered are not valid to log into the Labs service." + vbCrLf + "Please enter a valid user name and password." + vbCrLf + vbCrLf + "If you have typed the user name and password correctly and they are still not" + vbCrLf + "valid, please check that the facility code has been entered properly before" + vbCrLf + "calling support.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        MessageBox.Show("The user name and password entered are not valid to log into the Labs service." + vbCrLf + "Please enter a valid user name and password." + vbCrLf + vbCrLf + "If you have typed the user name and password correctly, please check that the facility code and emdeon URL has been entered properly before calling support.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        tb_Settings.SelectTab(tb_Settings.TabPages.IndexOf(tbp_EmdeonSettings))
                        txtEmdeonUserName.Focus()
                        Exit Function
                    ElseIf _gloLabHsi = "internet" Then
                        MessageBox.Show("Connection error. Internet connection not available." + vbCrLf + "You must be connected to the Internet to access Lab settings", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        tb_Settings.SelectTab(tb_Settings.TabPages.IndexOf(tbp_EmdeonSettings))
                        txtEmdeonUserName.Focus()
                        Exit Function
                    Else
                        objSettings.GloLab_Hsilb = _gloLabHsi
                    End If
                Else
                    objSettings.GloLab_Hsilb = gloLab_hsi
                End If
            End If

            ''Start:      Dhruv : 
            ''added by madan.. on 20100826
            If _gloLab_DefaultUserID <> 0 Then
                objSettings.GloLab_defaultUserID = _gloLab_DefaultUserID
            End If
            'End Madan 
            ''End :: Dhruv

            'Added by madan on 20101112

            ''Added by Abhijeet for Failed Lab Task user selection on 20111122
            If _gloLabFailure_DefaultUserID <> 0 Then
                objSettings.GloLabFailure_DefaultUserID = _gloLabFailure_DefaultUserID
            End If

            If _gloHxForecast_defaultUserID <> 0 Then
                objSettings.GloHxForecast_defaultUserID = _gloHxForecast_defaultUserID
            End If
            If _gloForecastReconcileDone_defaultUserID <> 0 Then
                objSettings.GloForecastReconcileDone_defaultUserID = _gloForecastReconcileDone_defaultUserID
            End If
            ''End of changes by Abhijeet for Failed Lab Task user selection on 20111122
            objSettings.EmdeonGetLabOrdersFromDaysOnReload = numEmdeonGetLabOrdersFromDaysOnReload.Value
            objSettings.PreselectDiagnosisWhilePlacingEMDEONOrders = chkPreselectDiagnosis.Checked
            objSettings.CloseDmstaskwin = chkDMSWin.Checked
            If rbHealthVaultOn.Checked = True Then
                objSettings.gloVaultVisibility = True
            ElseIf rbHealthVaultOn.Checked = True Then
                objSettings.gloVaultVisibility = False
            End If

            If txtParseUDIURL.Text.Trim = "" Then
                MessageBox.Show("URL is not entered. Please enter the valid URL in Interface settings.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                tb_Settings.SelectTab(tb_Settings.TabPages.IndexOf(tbpg_InterfaceSettings))
                txtParseUDIURL.Focus()
                Exit Function
            Else
                objSettings.ParseUDIURL = txtParseUDIURL.Text.Trim
            End If

            'End madan
            'start of code commented by manoj jadhav on 20111017 for New consolidated UI to device settings

            ' ''Added by madan on 20110801
            'If txtECGInterfaceId.Text.Trim().Length > 0 Then
            '    objSettings.ECGInstutionID = txtECGInterfaceId.Text.Trim()
            'End If

            'If txtECGInterfaceUrl.Text.Trim().Length > 0 Then
            '    objSettings.ECGInterfaceUrl = txtECGInterfaceUrl.Text.Trim()
            'End If
            ' ''End madan       
            'If txtECGUserProviderId.Text.Trim().Length > 0 Then
            '    objSettings.ECGUserProviderID = txtECGUserProviderId.Text.Trim()
            'Else
            '    objSettings.ECGUserProviderID = ""
            'End If

            'If ChkUseCardioScienceECGDevice.Checked Then
            '    objSettings.ECGEnabled = True
            'Else
            '    objSettings.ECGEnabled = False
            'End If
            'end of code commented by manoj jadhav on 20111017 for New consolidated UI to device settings

            ''Added by Abhijeet on 20101120
            If rbActive.Checked = True Then
                objSettings.PatientSpecificResultRange = "1"
            ElseIf rbInactive.Checked = True Then
                objSettings.PatientSpecificResultRange = "0"
            Else
                objSettings.PatientSpecificResultRange = ""
            End If

            ''End of changes by Abhijeet on 20101120

            'start of code commented by manoj jadhav on 20120730 for moving service setiings to Respective Service
            ' ''Added by Abhijeet on 20110422 & 20110425
            'If chkHL7ImmunizationRegistry.Checked Then
            '    objSettings.IMRegistryHL7Format = True

            '    objSettings.RegistryFileExportPath = txtRegistryExpFilePath.Text.Trim()

            '    If chkExportFiletoRegistry.Checked Then
            '        objSettings.RegistryFileIstobeExport = True
            '        objSettings.RegistryFileExportURL = txtRegisrtyExportURL.Text.Trim()
            '        objSettings.RegistryFileExportUserID = txtRegisrtyUserId.Text.Trim()
            '        objSettings.RegistryFileExportFacilityID = txtRegisrtyFacilityID.Text.Trim()

            '        Dim _RegPassWord As String = String.Empty
            '        Dim objRegeexcerpt As New clsEncryption
            '        If Not IsNothing(objRegeexcerpt) Then
            '            _RegPassWord = objRegeexcerpt.EncryptToBase64String(txtRegisrtyPassword.Text.Trim, mdlGeneral.constEncryptDecryptKey)
            '            objRegeexcerpt = Nothing
            '        End If
            '        objSettings.RegistryFileExportPassword = _RegPassWord
            '        objSettings.RegistryFileExportEveryDayAt = dtPickExpFileat.Value.ToString("hh:mm tt")

            '    Else
            '        objSettings.RegistryFileIstobeExport = False
            '    End If

            'Else
            '    objSettings.IMRegistryHL7Format = False
            'End If
            ' ''End of changes by Abhijeet on 20110422 & 20110425
            'end of code commented by manoj jadhav on 20120730 for moving service setiings to Respective Service

            ' '' Added by Abhijeet on 20110407
            'If chkUseVitalDevice.Checked Then
            '    objSettings.UseVitalDevice = True
            'Else
            '    objSettings.UseVitalDevice = False
            'End If
            'If txtVitalDeviceKey.Text.Trim.Length > 0 Then
            '    objSettings.VitalDeviceKey = objEncrypt.EncryptToBase64String(txtVitalDeviceKey.Text.Trim, constEncryptDecryptKey)
            'End If
            'start of code commented by manoj jadhav on 20111017 for New consolidated UI to device settings
            'objSettings.NoofAttempttoConnectVitalDevice = nup_NoofAttemptstoConnectVitalDevice.Value
            'end of code commented by manoj jadhav on 20111017 for New consolidated UI to device settings
            ' ''End of chaanges by Abhijeet on  20110407

            ''code by pradeep(20110106)for page break on pharmacy change in prescription report
            objSettings.PrintMultipleRx_PerScriptPage_setting = Chk_PrintMultipleRx_Per_Script_Page.Checked
            ''end of code pradeep
            objSettings.CustomizeRxReportPrintSetting = chkCustomizeReportPrintSetting.Checked
            ''Dhruv 20100216 
            '' FORMULARY VALIDATIONS '' 
            objSettings.PrintPharmacyOnRxReportSetting = ChkPrintRxPharmacyOnReport.Checked

            ''If chkAdvanceRx.Checked = True Then change done to add new setting for formulary.
            If chkFormularyEnable.Checked = True Then
                If ValidateFormularyConnections(txtFormularyServerName.Text.Trim, txtFormularyDataBaseName.Text.Trim, txtFormularyUserId.Text.Trim(), txtFormularyPassword.Text, rdbSQL.Checked) = False Then
                    Exit Function  '' If there is connection problem it will exit 
                End If

            End If
            objSettings.FormularyServerName = txtFormularyServerName.Text.Trim          ''servername
            objSettings.FormularyDataBaseName = txtFormularyDataBaseName.Text.Trim      ''databasename  
            objSettings.FormularyUserName = txtFormularyUserId.Text.Trim                ''userid
            objSettings.FormularyPassword = txtFormularyPassword.Text                   ''password
            objSettings.FormularyAuthentication = rdbSQL.Checked                        ''authentication
            objSettings.FormularyPassword = objEncrypt.EncryptToBase64String(txtFormularyPassword.Text, constEncryptDecryptKey) ''setting the password as in the encrypted form

            objSettings.FormularyServiceURL = txtFormularyURL.Text
            objSettings.DIBServiceURL = txtDIBServiceURL.Text
            objSettings.ePAServiceURL = txtePAServiceURL.Text
            'sanjog
            If cmbDefaultLabTab.Text.Trim <> "" Then
                objSettings.DefaultLabTab = cmbDefaultLabTab.Text.Trim()
            Else
                objSettings.DefaultLabTab = "Results"
            End If
            objSettings.MultipleSupervisorsforPaperRx = ChkMultipleSupervisorsforPaperRx.CheckState
            objSettings.LoadProblemonMeds = ChkLoadProbOnMed.Checked

            objSettings.LoadProblemDxCodeonMeds = ChkLoadProbDxCodeOnMed.Checked

            'sanjog
            'start of code commented by manoj jadhav on 20111017 for New consolidated UI to device settings

            ' objSettings.SpirometryDevicePrefix = txtprefixForSpirometryDevice.Text.Trim()
            'end of code commented by manoj jadhav on 20111017 for New consolidated UI to device settings
            '' If chkMU2Features.Checked Then
            objSettings.nMessageComunicationPrefID = cmbMsgCommPref.SelectedValue
            objSettings.nLetterComunicationPrefID = cmbLettersCommPref.SelectedValue
            objSettings.nSecureMessageComunicationPrefID = CmbSecureMsgComPref.SelectedValue

            ''   End If

            ''7022Items: Home Billing- Added to save setting for USEAREACODEFORPATIENT in database
            ''Save 0 if No is selected and 1 if Yes is selected
            If rbtn_UseAreaCodeForPatientNo.Checked Then
                objSettings.UseAreaCodeForPatient = False
            End If
            ''7022Items: Home Billing- Added to save setting for USEAREACODEFORPATIENT in database
            ''Save 0 if No is selected and 1 if Yes is selected
            If rbtn_UseAreaCodeForPatientYes.Checked Then
                objSettings.UseAreaCodeForPatient = rbtn_UseAreaCodeForPatientYes.Checked
            End If

            objSettings.dtICD10StartDOS = dtpICD10DOS.Value.Date
            SaveICD10TransitionSettings()
            ''Start OB Setting
            saveOBSpeciality()
            objSettings.IsOBSpeciality = chkOBSpeciality.Checked
            objSettings.AutoCaseCloseDays = Trim(txtAutoCaseCloseDays.Text)

            ''End OB Setting

            If Not clsSettings.AddOrUpdateSettings(sSingleRxStateCustiomizeReport, sMultipleRxStateCustiomizeReport, objSettings.IsEpcsEnble, objSettings.IsAllowPrintForCS, dtSaveSettings) Then
                MessageBox.Show("Unable to update settings.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                tb_Settings.SelectTab(tb_Settings.TabPages.IndexOf(tbp_ClinicSettings))
                tmStartTime.Focus()
                objSettings = Nothing
                Exit Function
            End If


            If objSettings.UpdateSettings(dtSaveSettings) = False Then
                Me.Cursor = Cursors.Default
                MessageBox.Show("Unable to update settings.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                tb_Settings.SelectTab(tb_Settings.TabPages.IndexOf(tbp_ClinicSettings))
                tmStartTime.Focus()
                objSettings = Nothing
                Exit Function
                '******
                'Enter the follow up user in Settings table 
            Else

                Dim oDB As New gloStream.gloDataBase.gloDataBase

                Dim strqry As String = " Delete from settings where sSettingsName='Followup User'"
                oDB.Connect(gstrConnectionString)
                Dim succ As Boolean = oDB.ExecuteNonSQLQuery(strqry)
                'If (oDB.ExecuteNonSQLQuery(strqry) = True) Then
                ''commenetd by Mayuri:20101211
                ''For i As Integer = 1 To colUId.Count
                ''End code commneted by Mayuri:20101211
                If ToList.Count > 0 Then
                    For i As Integer = 0 To ToList.Count - 1
                        strqry = " Select max(nSettingsID) as maxid from settings "

                        Dim maxID As Long = oDB.ExecuteQueryScaler(strqry)

                        strqry = " insert into Settings(nSettingsID, sSettingsName, sSettingsValue,nClinicID,nUserID,nUserClinicFlag)values('" + (maxID + 1).ToString + "','Followup User','" + ToList.Item(i).ID.ToString + "'," & _ClinicID & ",0,1) "

                        If (oDB.ExecuteQueryNonQuery(strqry) = False) Then
                            MessageBox.Show("Unable to update settings.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            tb_Settings.SelectTab(tb_Settings.TabPages.IndexOf(tbp_ClinicSettings))
                            tmStartTime.Focus()
                            objSettings = Nothing
                            Exit Function
                        End If
                    Next
                End If

                'End If
                ''code added by pradeep for audit trail of site prefix
                Dim objAudit1 As New clsAudit
                'If  Then
                If flagPrefixSettingON = flagPrefixSettingOFF Then
                    ' objAudit1.CreateLog(clsAudit.enmActivityType.Other, "Use site prefix setting turn off", gstrLoginName, gstrClientMachineName)
                Else
                    If flagPrefixSettingOFF = True Then
                        objAudit1.CreateLog(clsAudit.enmActivityType.Other, "Use site prefix setting turn ON", gstrLoginName, gstrClientMachineName)
                    Else
                        objAudit1.CreateLog(clsAudit.enmActivityType.Other, "Use site prefix setting turn OFF", gstrLoginName, gstrClientMachineName)
                    End If
                End If

                '******
                objAudit1 = Nothing

                '--------------updating SurgicalAlertUser to settings table
                Dim strSurgicalAlertqry As String = " Delete from settings where sSettingsName='Surgical User'"
                oDB.Connect(gstrConnectionString)
                Dim success As Boolean = oDB.ExecuteNonSQLQuery(strSurgicalAlertqry)
                'If (oDB.ExecuteNonSQLQuery(strqry) = True) Then
                ' For i As Integer = 1 To col_SurgicalUId.Count
                If gloItems.Count > 0 Then
                    For i As Integer = 0 To gloItems.Count - 1
                        strqry = " Select max(nSettingsID) as maxid from settings "

                        ''Start :: Dhruv
                        '' Dim maxID As Integer = oDB.ExecuteQueryScaler(strqry)
                        Dim maxID As Long = oDB.ExecuteQueryScaler(strqry)
                        ''End :: Dhruv

                        'strqry = " insert into Settings(nSettingsID, sSettingsName, sSettingsValue,nClinicID,nUserID,nUserClinicFlag)values('" + (maxID + 1).ToString + "','Surgical User','" + col_SurgicalUId.Item(i).ToString + "'," & _ClinicID & ",0,1) "
                        strqry = " insert into Settings(nSettingsID, sSettingsName, sSettingsValue,nClinicID,nUserID,nUserClinicFlag)values('" + (maxID + 1).ToString + "','Surgical User','" + gloItems.Item(i).ID.ToString + "'," & _ClinicID & ",0,1) "

                        If (oDB.ExecuteQueryNonQuery(strqry) = False) Then
                            MessageBox.Show("Unable to update settings.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            tb_Settings.SelectTab(tb_Settings.TabPages.IndexOf(tbp_ClinicSettings))
                            tmStartTime.Focus()
                            objSettings = Nothing
                            Exit Function
                        End If
                    Next
                End If
                '--------------updating SurgicalAlertUser to settings table

            End If

            Dim objAudit As New clsAudit

            If objSettings.blnPwdComplexity = True Then
                If m_strSQL <> "" Then
                    If objSettings.SetPwdComplexitySettings(m_strSQL) = False Then
                        Me.Cursor = Cursors.Default
                        MessageBox.Show("Unable to update the password complexity settings.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        tb_Settings.SelectTab(tb_Settings.TabPages.IndexOf(tbp_ClinicSettings))
                        'tmStartTime.Focus()
                        objSettings = Nothing
                        Exit Function
                    Else
                        'sarika  21 feb

                        objAudit.CreateLog(clsAudit.enmActivityType.Other, gstrLoginName & " has reset the Password Settings.", gstrLoginName, gstrClientMachineName)
                    End If
                End If

            Else
                If sqlstrsettings <> "" Then
                    If objSettings.SetPwdComplexitySettings(sqlstrsettings) = False Then
                        Me.Cursor = Cursors.Default
                        MessageBox.Show("Unable to update the password complexity settings.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        tb_Settings.SelectTab(tb_Settings.TabPages.IndexOf(tbp_ClinicSettings))
                        tmStartTime.Focus()
                        objSettings = Nothing
                        Exit Function
                    Else
                        'sarika  21 feb
                        objAudit.CreateLog(clsAudit.enmActivityType.Other, gstrLoginName & " has reset the Password Settings.", gstrLoginName, gstrClientMachineName)
                    End If
                    'Else
                    'optPwdComplexNo.Checked = True
                    'objSettings.blnPwdComplexity = False
                End If

                'objAudit = Nothing
                '-------------

            End If

            'start of code commented by manoj jadhav on 20111017 for New consolidated UI to device settings
            ' ''Added by Abhijeet on 20110409
            'If chkUseVitalDevice.Checked = False Then
            '    DeleteSettingsForVitalsDevice()
            '    txtVitalDeviceKey.Text = String.Empty
            'End If
            ' ''End of changes by Abhijeet on 20110409
            'Code added by RK on 20110521
            'If chkUseSpirometryDevice.Checked = False Then
            '    DeleteSettingForSpirometryDevice()
            'Else
            'end of code commented by manoj jadhav on 20111017 for New consolidated UI to device settings
            'added condition by manoj jadhav to check for device database setting on 20111017'
            If chkUseSpirometryDevice.Checked Or chkUseMidmarkECGDevice.Checked Then
                Dim objCheckSettingForDevice As New clsStartUpSettings
                If txtDeviceServerName.Text.Trim = String.Empty Then
                    MessageBox.Show("Enter Device database settings Server Name.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    tb_Settings.SelectTab(tb_Settings.TabPages.IndexOf(tbpg_ReportServerSettings))
                    txtDeviceServerName.Focus()
                    Exit Function
                End If
                If txtDeviceDataBaseName.Text.Trim = String.Empty Then
                    MessageBox.Show("Enter Device database settings Database Name.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    tb_Settings.SelectTab(tb_Settings.TabPages.IndexOf(tbpg_ReportServerSettings))
                    txtDeviceDataBaseName.Focus()
                    Exit Function
                End If
                If objCheckSettingForDevice.IsConnect(txtDeviceServerName.Text.Trim, txtDeviceDataBaseName.Text.Trim, False, "", "") = False Then
                    If MessageBox.Show("Enter the device database setting ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning) Then
                        tb_Settings.SelectTab(tb_Settings.TabPages.IndexOf(tbpg_ReportServerSettings))
                        Exit Function
                    End If
                End If
            End If
            'end of Code added by RK on 20110521
            'sarika  21 feb
            '  Dim objAudit As New clsAudit
            'objAudit.CreateLog(clsAudit.enmActivityType.Other, gstrLoginName & " has reset the Settings.", gstrLoginName, gstrClientMachineName)
            objAudit = Nothing
            '-------------

            'If objSettings.blnPwdComplexity = True Then
            '    If m_strSQL <> "" Then
            '        If objSettings.SetPwdComplexitySettings(m_strSQL) = False Then
            '            Me.Cursor = Cursors.Default
            '            MessageBox.Show("Unable to update the password complexity settings", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '            tmStartTime.Focus()
            '            objSettings = Nothing
            '            Exit Sub
            '        End If
            '    End If

            'Else
            '    If _sqlstrsettings <> "" Then
            '        If objSettings.SetPwdComplexitySettings(_sqlstrsettings) = False Then
            '            Me.Cursor = Cursors.Default
            '            MessageBox.Show("Unable to update the password complexity settings", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '            tmStartTime.Focus()
            '            objSettings = Nothing
            '            Exit Sub
            '        End If
            '        'Else
            '        'optPwdComplexNo.Checked = True
            '        'objSettings.blnPwdComplexity = False
            '    End If

            'End If


            'If _sqlstrsettings <> "" Then
            '    If objSettings.SetPwdComplexitySettings(_sqlstrsettings) = False Then
            '        Me.Cursor = Cursors.Default
            '        MessageBox.Show("Unable to update the password complexity settings", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '        tmStartTime.Focus()
            '        objSettings = Nothing
            '        Exit Sub
            '    End If

            'End If




            ' objSettings = Nothing
            If CheckFaxModifications() = True Then
                MessageBox.Show("To apply the new FAX Settings to the FAX Application, re-run the FAX Application.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
            'If CheckEFaxModifications() = True Then
            '    MessageBox.Show("To apply the new eFAX Settings, please re-start the Internet FAX service", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            'End If

            'If (blnCloseClicked = False) Then
            '' start :: dhruv
            ' blnBtncloseclick = True	
            '    Me.Close()
            '    Me.Cursor = Cursors.Default
            'End If
            ''end :: dhruv

            ''//code commented by Ravikiran on 14/02/2007 
            ''''' Code added for customr Report path settings by Ravikiran on 10/02/2007
            ''If Trim(txtRxReportPath.Text) <> "" Then
            ''    If Directory.Exists(txtRxReportPath.Text) = False Then
            ''        MessageBox.Show(txtRxReportPath.Text & " is not valid path." & vbCrLf & "Please browse for valid RxReport Directory", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ''        txtRxReportPath.Focus()
            ''        Exit Sub
            ''    End If
            ''End If
            ''If Trim(txtRxReportPath.Text) <> "" Then
            ''    gstrRxReportpath = txtRxReportPath.Text
            ''    If Not checkRxReportPath(gstrRxReportpath) Then
            ''        InsertRxReportPath(gstrRxReportpath)
            ''    End If

            ''End If

            ''''Code updation ends

            '''' Added by Anil on 20090717 to Save Provider Settings for EDI generation
            ''Sandip Darade 20091107
            ''Sandip Darade 20091107
            If (gstrAdminFor = "gloPM") Then
                SaveProviderSetting(dtSaveSettings)
            End If

            SavePatientAccountSettingNew()

            If (txtReportServerName.Text.ToString() <> "" And txtReportFolderName.Text.ToString() <> "" And txtCustomRptFolderNm.Text.ToString() <> "") Then

                'Added by Amit 17/04/2012
                If Rdohttp.Checked = True Then
                    AddSettingToDB("ReportProtocol", "http", _ClinicID, gnLoginID, SettingFlag.Clinic, dtSaveSettings)
                Else
                    AddSettingToDB("ReportProtocol", "https", _ClinicID, gnLoginID, SettingFlag.Clinic, dtSaveSettings)
                End If

                AddSettingToDB("ReportServer", txtReportServerName.Text.ToString(), _ClinicID, gnLoginID, SettingFlag.Clinic, dtSaveSettings)
                AddSettingToDB("ReportFolder", txtReportFolderName.Text.ToString(), _ClinicID, gnLoginID, SettingFlag.Clinic, dtSaveSettings)
                AddSettingToDB("CustomizedReportFolder", txtCustomRptFolderNm.Text.ToString(), _ClinicID, gnLoginID, SettingFlag.Clinic, dtSaveSettings)
            ElseIf (txtReportServerName.Text.ToString() = "" And txtReportFolderName.Text.ToString() = "" And txtCustomRptFolderNm.Text.ToString() = "") Then

                'Added by Amit 17/04/2012
                If Rdohttp.Checked = True Then
                    AddSettingToDB("ReportProtocol", "http", _ClinicID, gnLoginID, SettingFlag.Clinic, dtSaveSettings)
                Else
                    AddSettingToDB("ReportProtocol", "https", _ClinicID, gnLoginID, SettingFlag.Clinic, dtSaveSettings)
                End If

                AddSettingToDB("ReportServer", txtReportServerName.Text.ToString(), _ClinicID, gnLoginID, SettingFlag.Clinic, dtSaveSettings)
                AddSettingToDB("ReportFolder", txtReportFolderName.Text.ToString(), _ClinicID, gnLoginID, SettingFlag.Clinic, dtSaveSettings)
                AddSettingToDB("CustomizedReportFolder", txtCustomRptFolderNm.Text.ToString(), _ClinicID, gnLoginID, SettingFlag.Clinic, dtSaveSettings)

            ElseIf (txtReportServerName.Text.ToString() = "" Or txtReportFolderName.Text.ToString() = "" Or txtCustomRptFolderNm.Text.ToString() <> "") Then
                tb_Settings.SelectedTab = tbpg_ReportServerSettings
                _blnValidate = True

                If (txtReportServerName.Text.ToString() = "") Then
                    MessageBox.Show("Enter Report Server Name.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    tb_Settings.SelectTab(tb_Settings.TabPages.IndexOf(tbpg_ReportServerSettings))
                    txtReportServerName.Focus()
                End If

                If (txtReportFolderName.Text.ToString() = "") Then
                    MessageBox.Show("Enter Report Folder Name.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    tb_Settings.SelectTab(tb_Settings.TabPages.IndexOf(tbpg_ReportServerSettings))
                    txtReportFolderName.Focus()
                End If

                If (txtCustomRptFolderNm.Text.ToString() = "") Then
                    MessageBox.Show("Enter Customized Report Folder Name.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    tb_Settings.SelectTab(tb_Settings.TabPages.IndexOf(tbpg_ReportServerSettings))
                    txtCustomRptFolderNm.Focus()
                End If

                Exit Function
            End If

            ''''''Added by Ujwala for Customized SSRS Reports - as on 20101021
            AddSettingToDB("ReportVirtualDirectory", txtReportVirtualDir.Text.ToString(), _ClinicID, gnLoginID, SettingFlag.Clinic, dtSaveSettings)

            '' adedd by Manoj Jadhav on 20110314

            If IsNumeric(GENERALMESSAGELOGPAGESIZE) = False Or GENERALMESSAGELOGPAGESIZE = 0 Then
                GENERALMESSAGELOGPAGESIZE = 1000
            End If
            AddSettingToDB("GENERALMESSAGELOGPAGESIZE", GENERALMESSAGELOGPAGESIZE, _ClinicID, gnLoginID, SettingFlag.Clinic, dtSaveSettings)
            '' ended by Manoj Jadhav on 20110314
            If _blnglocominstalled = True Then
                AddSettingToDB("CommunityEnvironment", gIscommunitystaging, _ClinicID, 0, SettingFlag.Clinic, dtSaveSettings)
            End If

            AddSettingToDB("TimeServer", txttimeserver.Text.Trim(), _ClinicID, gnLoginID, SettingFlag.Clinic, dtSaveSettings)

            AddSettingToDB("ShowUserNoteFirst", chkShowUserNoteFirst.Checked, _ClinicID, gnLoginID, SettingFlag.Clinic, dtSaveSettings)
            AddSettingToDB("SendCDAExamFinish", chkCDASendExamFinish.Checked, _ClinicID, gnLoginID, SettingFlag.Clinic, dtSaveSettings)
            AddSettingToDB("PromptProviderForCDASend", chkPromptCDASend.Checked, _ClinicID, gnLoginID, SettingFlag.Clinic, dtSaveSettings)
            AddSettingToDB("HoosKoosSurveyService", txtHoosKoosServiceURL.Text, _ClinicID, gnLoginID, SettingFlag.Clinic, dtSaveSettings)
            AddSettingToDB("PDMPService", txtPDMPUrl.Text, _ClinicID, gnLoginID, SettingFlag.Clinic, dtSaveSettings)
            AddSettingToDB("MedHxRestriction", nmMedHxRestriction.Value, _ClinicID, gnLoginID, SettingFlag.Clinic, dtSaveSettings)
            If bIsQCheckpointSettingModified Then
                AddSettingToDB("DefaultQCheckpointMeasures", GetCQMSetting(), _ClinicID, gnLoginID, SettingFlag.Clinic, dtSaveSettings)
            End If

            If dtCDAExamProviders IsNot Nothing Then
                If dtCDAExamProviders.Columns.Contains("sProviderName") Then
                    dtCDAExamProviders.Columns.Remove("sProviderName")
                End If
            End If


            objSettings.SaveEmrSettings(dtSaveSettings, dtCDAExamProviders)
            objSettings = Nothing
            Return True
        Catch objErr As Exception
            Me.Cursor = Cursors.Default
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            'sarika  21 feb
            Dim objAudit As New clsAudit
            objAudit.CreateLog(clsAudit.enmActivityType.Modify, " Error occured while modifying the settings.", gstrLoginName, gstrClientMachineName, 0, , clsAudit.enmOutcome.Failure)
            objAudit = Nothing
            '-------------
        Finally
            'Me.DialogRsesult = Windows.Forms.DialogResult.OK
            If (Not IsNothing(dtSaveSettings)) Then
                dtSaveSettings.Dispose()
                dtSaveSettings = Nothing
            End If
            Me.Cursor = Cursors.Default

        End Try
    End Function

    Private Sub SaveICD10TransitionSettings()
        'Dim oClsICD10Settings As clsICD10Settings = Nothing
        'Dim oBL_ICD10TransitionSetting As BL_ICD10TransitionSetting = Nothing
        'Dim _List As IEnumerable = Nothing

        'Try
        '    oBL_ICD10TransitionSetting = New BL_ICD10TransitionSetting()
        '    oBL_ICD10TransitionSetting.nID = 0
        '    oBL_ICD10TransitionSetting.nContactID = 0
        '    oBL_ICD10TransitionSetting.nClinicID = gnClinicID
        '    oBL_ICD10TransitionSetting.dtDOSDate = dtpICD10DOS.Value.Date
        '    oBL_ICD10TransitionSetting.dtCreatedDate = DateTime.Now
        '    oBL_ICD10TransitionSetting.dtModifiedDate = DateTime.Now

        '    If Not IsNothing(oBL_ICD10TransitionSetting) Then
        '        oClsICD10Settings = New clsICD10Settings()
        '        oClsICD10Settings.SaveData(oBL_ICD10TransitionSetting)
        '    End If
        'Catch ex As Exception
        '    UpdateErrorLog(ex.ToString)
        'Finally
        '    _List = Nothing
        '    If Not oClsICD10Settings Is Nothing Then
        '        'dispose
        '        oClsICD10Settings = Nothing
        '    End If
        'End Try


        Dim oClsICD10Settings As clsICD10Settings = Nothing
        Dim oBL_ICD10TransitionSetting As BL_ICD10TransitionSetting = Nothing

        Try
            C1ICD10DOS.Select()
            oClsICD10Settings = New clsICD10Settings()
            oBL_ICD10TransitionSetting = (From Result In oClsICD10Settings.oDataContext.BL_ICD10TransitionSettings
                                                    Where Result.nContactID = 0
                                                    Select Result).FirstOrDefault()
            If Not IsNothing(dtpICD10DOS) AndAlso Not IsNothing(dtpICD10DOS.Value) Then  '03252014  Removed the Check box from gloPM Admin -Transition Settings
                If oBL_ICD10TransitionSetting Is Nothing Then
                    oBL_ICD10TransitionSetting = New BL_ICD10TransitionSetting()
                    oBL_ICD10TransitionSetting.nID = 0
                    oBL_ICD10TransitionSetting.nContactID = 0
                    oBL_ICD10TransitionSetting.nClinicID = gnClinicID
                    oBL_ICD10TransitionSetting.dtDOSDate = dtpICD10DOS.Value.ToShortDateString
                    oBL_ICD10TransitionSetting.dtCreatedDate = DateTime.Now
                    oBL_ICD10TransitionSetting.dtModifiedDate = DateTime.Now
                    oClsICD10Settings.oDataContext.BL_ICD10TransitionSettings.InsertOnSubmit(oBL_ICD10TransitionSetting)
                Else
                    oBL_ICD10TransitionSetting.nClinicID = gnClinicID
                    oBL_ICD10TransitionSetting.dtDOSDate = dtpICD10DOS.Value.ToShortDateString
                    oBL_ICD10TransitionSetting.dtModifiedDate = DateTime.Now
                End If
            Else
                If Not oBL_ICD10TransitionSetting Is Nothing Then
                    oBL_ICD10TransitionSetting.nClinicID = gnClinicID
                    oBL_ICD10TransitionSetting.dtDOSDate = Nothing
                    oBL_ICD10TransitionSetting.dtModifiedDate = DateTime.Now
                End If
            End If

            If Not IsNothing(C1ICD10DOS) AndAlso C1ICD10DOS.Rows.Count > 0 Then
                For i As Int32 = 1 To C1ICD10DOS.Rows.Count - 1
                    Dim _contactID As Int64 = C1ICD10DOS.GetData(i, ICD10Grid.ContactID)
                    oBL_ICD10TransitionSetting = New BL_ICD10TransitionSetting()
                    oBL_ICD10TransitionSetting.nID = 0

                    oBL_ICD10TransitionSetting = (From Result In oClsICD10Settings.oDataContext.BL_ICD10TransitionSettings
                                                     Where Result.nContactID = _contactID
                                                     Select Result).FirstOrDefault()
                    If oBL_ICD10TransitionSetting Is Nothing Then
                        oBL_ICD10TransitionSetting = New BL_ICD10TransitionSetting()
                        oBL_ICD10TransitionSetting.nID = 0
                        oBL_ICD10TransitionSetting.nContactID = C1ICD10DOS.GetData(i, ICD10Grid.ContactID)
                        oBL_ICD10TransitionSetting.nClinicID = gnClinicID
                        oBL_ICD10TransitionSetting.dtDOSDate = C1ICD10DOS.GetData(i, ICD10Grid.DOS)
                        oBL_ICD10TransitionSetting.dtCreatedDate = DateTime.Now
                        oBL_ICD10TransitionSetting.dtModifiedDate = DateTime.Now
                        oClsICD10Settings.oDataContext.BL_ICD10TransitionSettings.InsertOnSubmit(oBL_ICD10TransitionSetting)
                    Else
                        oBL_ICD10TransitionSetting.nClinicID = gnClinicID
                        oBL_ICD10TransitionSetting.dtDOSDate = C1ICD10DOS.GetData(i, ICD10Grid.DOS)
                        oBL_ICD10TransitionSetting.dtModifiedDate = DateTime.Now
                    End If
                Next
            End If
            oClsICD10Settings.getChangeDataforAuditing()
            oClsICD10Settings.oDataContext.SubmitChanges()
        Catch ex As Exception
            UpdateErrorLog(ex.ToString)
        Finally
            If Not oClsICD10Settings Is Nothing Then
                oClsICD10Settings.Dispose()
                oClsICD10Settings = Nothing
            End If
            oBL_ICD10TransitionSetting = Nothing
        End Try

    End Sub

    Private Sub UpdateeRxWebserviceSetting(ByVal strConnection As String, ByVal sSettingname As String, ByVal sSettingValue As String)
        Dim oDbLayer As New gloDatabaseLayer.DBLayer(strConnection)
        Dim dtSettingsTable As New DataTable
        Dim _strQuery As String = String.Empty
        Try

            oDbLayer.Connect(False)

            _strQuery = "Update GLSettings Set sSettingsValue='" & sSettingValue & "'  where nReferenceId='0' and ssettingsname = '" & sSettingname & "'"

            oDbLayer.Execute_Query(_strQuery)
            oDbLayer.Disconnect()

        Catch ex As Exception
            UpdateLog(ex.ToString())
            If IsNothing(dtSettingsTable) = False Then
                dtSettingsTable.Dispose()
                dtSettingsTable = Nothing
            End If

        Finally
            If oDbLayer IsNot Nothing Then
                oDbLayer.Disconnect()
                oDbLayer.Dispose()
            End If
        End Try
    End Sub

#End Region
#Region "CCD Settings"


    Private Function checkCCDSections() As String
        Dim i As Integer
        Dim _cntrl As Control
        Dim _value As String = ""
        Dim _chk As CheckBox
        'If ChkFullCCD.Checked = True Then
        '    _value = "Fullvitals,FullFamHis,FullAdDir,FullLabs,FullImmu,FullProc,FullMed,Fullencounter,FullSocHis,FullAllergy,FullProb"
        '    Return _value
        '    Exit Function
        'End If
        For Each _cntrl In PnlMain.Controls
            If TypeOf _cntrl Is CheckBox Then
                _chk = CType(_cntrl, CheckBox)
                If _chk.Checked = True Then
                    If _value = "" Then
                        _value = _value + _chk.Tag
                    Else
                        _value = _value + "," + _chk.Tag
                    End If

                End If

            End If
        Next
        Return _value

    End Function
    Private Function checkCCDSectionsVisit() As String
        Dim i As Integer
        Dim _cntrl As Control
        Dim _value As String = ""
        Dim _chk As CheckBox
        'If ChkVisitCCD.Checked = True Then
        '    _value = "VisitVitals,VisitFamHis,VisitAdDir,VisitLabs,visitImmu,VisitProc,VisitMed,VisitEncounter,VisitSocHis,VisitAllegy,VisitProb"
        '    Return _value
        '    Exit Function
        'End If
        For Each _cntrl In pnlVisitCCD.Controls
            If TypeOf _cntrl Is CheckBox Then
                _chk = CType(_cntrl, CheckBox)
                If _chk.Checked = True Then
                    If _value = "" Then
                        _value = _value + _chk.Tag
                    Else
                        _value = _value + "," + _chk.Tag
                    End If

                End If

            End If
        Next
        Return _value
    End Function
    'Private Function checkMUCCDSections() As String
    '    Dim i As Integer
    '    Dim _cntrl As Control
    '    Dim _value As String = ""
    '    Dim _chk As CheckBox
    '    'If ChkVisitCCD.Checked = True Then
    '    '    _value = "FullLabs,FullMed,FullAllergy,FullProb,VisitLabs,VisitMed,VisitAllegy,VisitProb"
    '    '    Return _value
    '    '    Exit Function
    '    'End If
    '    For Each _cntrl In pnlFullvisitCCD.Controls
    '        If TypeOf _cntrl Is CheckBox Then
    '            _chk = CType(_cntrl, CheckBox)
    '            If _chk.Tag = "FullLabs" Or _chk.Tag = "FullMed" Or _chk.Tag = "FullAllergy" Or _chk.Tag = "FullProb" Or _chk.Tag = "VisitLabs" Or _chk.Tag = "VisitMed" Or _chk.Tag = "VisitAllegy" Or _chk.Tag = "VisitProb" Then
    '                If _chk.Checked = True Then
    '                    If _value = "" Then
    '                        _value = _value + _chk.Tag
    '                    Else
    '                        _value = _value + "," + _chk.Tag
    '                    End If

    '                End If
    '            End If

    '        End If
    '    Next
    '    Return _value
    'End Function
#End Region
    Private Function CheckFaxModifications() As Boolean
        Try
            If (chkInternetFax.Checked = False) Then


                'chk the current values with the stored vals
                Dim Faxreceive As Integer
                'sarika  UseFaxNoPrefix 12th april 08
                Dim UseFaxPrefix As Integer
                '------ UseFaxNoPrefix 12th april 08


                If optFAXreceiveYes.Checked = True Then
                    Faxreceive = 1
                Else
                    Faxreceive = 0
                End If

                'sarika  UseFaxNoPrefix 12th april 08
                If chkUseFaxNoPrefix.Checked = True Then
                    UseFaxPrefix = 1
                Else
                    UseFaxPrefix = 0
                End If
                '------ UseFaxNoPrefix 12th april 08


                If _arrayGetData(0) <> numMaxNoOfRetries.Value Then
                    bModifyData = True
                    Return bModifyData
                End If
                If _arrayGetData(1) <> numFAXRetryInterval.Value Then
                    bModifyData = True
                    Return bModifyData
                End If
                If _arrayGetData(2) <> cmbFAXCompression.Text Then
                    bModifyData = True
                    Return bModifyData
                End If
                If _arrayGetData(3) <> cmbSpeakerVolume.Text Then
                    bModifyData = True
                    Return bModifyData
                End If
                If _arrayGetData(4) <> Faxreceive Then
                    bModifyData = True
                    Return bModifyData
                End If

                'sarika UseFaxNoPrefix 12th april 08
                If _arrayGetData(5) <> UseFaxPrefix Then
                    bModifyData = True
                    Return bModifyData
                End If
                If _arrayGetData(6) <> txtFaxNoPrefix.Text.Trim Then
                    bModifyData = True
                    Return bModifyData
                End If

                If _arrayGetData(8) <> txteFaxDownloadDir.Text.Trim Then
                    bModifyData = True
                    Return bModifyData
                End If
                '------------sarika UseFaxNoPrefix 12th april 08

                'sarika 
                'DMS 20080908 -- for Loading no of recieved faxes in DMS
                'If _arrayGetData(7) <> numLoadNoOfFaxes.Value Then
                '    bModifyData = True
                '    Return bModifyData
                'End If
                '---------------
            End If
        Catch ex As Exception
            MessageBox.Show("Error in Setdata : " & ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function

    Private Sub SetData(ByVal arrList As ArrayList)
        Try
            arrList.Add(numMaxNoOfRetries.Value)
            arrList.Add(numFAXRetryInterval.Value)
            arrList.Add(cmbFAXCompression.Text)
            arrList.Add(cmbSpeakerVolume.Text)
            If optFAXreceiveYes.Checked = True Then
                arrList.Add(1)
            Else
                arrList.Add(0)
            End If

            'sarika UseFaxNoPrefix 12th april 08
            If chkUseFaxNoPrefix.Checked = True Then
                arrList.Add(1)
            Else
                arrList.Add(0)
            End If
            arrList.Add(txtFaxNoPrefix.Text.Trim)

            '------------sarika UseFaxNoPrefix 12th april 08

            'sarika 
            'DMS 20080908 -- for Loading no of recieved faxes in DMS

            arrList.Add(numLoadNoOfFaxes.Value)
            '-----------


            'sarika SendEMail 20090502
            'If chkSendMail.Checked = True Then
            '    arrList.Add(1)
            'Else
            '    arrList.Add(0)
            'End If
            'arrList.Add(txtSendMail.Text.Trim)


            arrList.Add(txteFaxDownloadDir.Text.Trim)

            If chkInternetFax.Checked = True Then
                arrList.Add(1)
            Else
                arrList.Add(0)
            End If

            '--


        Catch ex As Exception
            MessageBox.Show("Error in Setdata : " & ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

#Region "   Private Methods"
    Private Sub Fill_FAXCompressions()
        With cmbFAXCompression
            .BeginUpdate()
            .Items.Clear()
            .Items.Add("CCITT G3")
            .Items.Add("CCITT G4")
            .Items.Add("Packbits")
            .EndUpdate()
        End With
    End Sub
    Private Sub Fill_FAXSpeakerVolume()
        With cmbSpeakerVolume
            .BeginUpdate()
            .Items.Clear()
            .Items.Add("No Volume")
            .Items.Add("Low Volume")
            .Items.Add("Medium Volume")
            .Items.Add("High Volume")
            .EndUpdate()
        End With
    End Sub

    Private Sub Fill_DMSCategories()
        Try

            Dim clCategories As New Collection
            clCategories = DMSCategories()

            cmbOMRCategoryHistory.BeginUpdate()
            cmbOMRCategoryHistory.Items.Clear()
            cmbOMRCategoryROS.Items.Clear()
            cmbOMRCategoryPatientRegistration.Items.Clear()
            cmbCategoryDirective.Items.Clear()
            cmbLabCategory.Items.Clear()
            cmbVISCategory.Items.Clear()

            cmbRCMtoDMSCategory.Items.Clear()
            cmbWelchAllynECGCategory.Items.Clear()

            'sarika 31st aug 07
            cmbFaxCategory.Items.Clear()
            cmbInboxCategory.Items.Clear()
            '--------------
            cmbOMRCategoryHistory.Items.Add("")
            cmbOMRCategoryROS.Items.Add("")
            cmbOMRCategoryPatientRegistration.Items.Add("")
            cmbCategoryDirective.Items.Add("")
            cmbLabCategory.Items.Add("")

            'sarika 31st aug 07
            cmbFaxCategory.Items.Add("")
            cmbInboxCategory.Items.Add("")
            '---------
            cmbVISCategory.Items.Add("")

            cmbRCMtoDMSCategory.Items.Add("")
            cmbWelchAllynECGCategory.Items.Add("")

            Dim nCount As Int16
            For nCount = 1 To clCategories.Count
                cmbOMRCategoryHistory.Items.Add(clCategories(nCount))
                cmbOMRCategoryROS.Items.Add(clCategories(nCount))
                cmbOMRCategoryPatientRegistration.Items.Add(clCategories(nCount))
                cmbCategoryDirective.Items.Add(clCategories(nCount))
                cmbLabCategory.Items.Add(clCategories(nCount))

                'sarika 31st aug 07
                cmbFaxCategory.Items.Add(clCategories(nCount))
                cmbInboxCategory.Items.Add(clCategories(nCount))
                '---------
                ''''Pramod
                cmbRadioCategory.Items.Add(clCategories(nCount))

                ''''for cchit11
                cmbRxMedsCategory.Items.Add(clCategories(nCount))
                'Developer: Mitesh Patel
                'Date:17-Jan-2012
                'PRD: Immunizations
                cmbVISCategory.Items.Add(clCategories(nCount))

                cmbRCMtoDMSCategory.Items.Add(clCategories(nCount))
                cmbWelchAllynECGCategory.Items.Add(clCategories(nCount))
            Next
            cmbOMRCategoryHistory.EndUpdate()
        Catch ex As Exception

        End Try

    End Sub

#End Region

    Private Sub optPwdComplexYes_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optPwdComplexYes.CheckedChanged

        If optPwdComplexYes.Checked = True Then
            optPwdComplexYes.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            optPwdComplexYes.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If

        Dim conn As New SqlConnection(gloEMRAdmin.mdlGeneral.GetConnectionString)
        Dim cmd As SqlCommand
        Dim cnt As Integer = 0
        Dim _strSQL As String = ""

        Dim blnadmin As Boolean = False
        Dim str As String = ""

        If optPwdComplexYes.Checked = True Then

            btnSetPwdComplexity.Visible = True

        Else
            btnSetPwdComplexity.Visible = False

            conn.Open()

        
            _strSQL = "select count(*) from PwdSettings"
            cmd = New SqlCommand(_strSQL, conn)
            cnt = cmd.ExecuteScalar

            If cnt = 0 Then

                _strSQL = "insert into PwdSettings(ExpCapitalLetters,ExpNoOfLetters,ExpNoOfDigits,ExpNoOfSpecChars,ExpPwdLength,ExpTimeFrameinDays) " & _
                        " values(" & 0 & "," & 0 & "," & 0 & "," & 0 & "," & 1 & "," & 0 & ")"

            Else

                _strSQL = "Update PwdSettings set ExpCapitalLetters = " & 0 & " ,ExpNoOfLetters = " & 0 & " ,ExpNoOfDigits = " & 0 & _
                        ",ExpNoOfSpecChars = " & 0 & ",ExpPwdLength = " & 1 & ",ExpTimeFrameinDays = " & 0
            End If

            sqlstrsettings = _strSQL

            conn.Close()

        End If

        If IsNothing(conn) = False Then
            conn.Dispose()
            conn = Nothing
        End If

    End Sub

    Private Sub btnSetPwdComplexity_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetPwdComplexity.Click
        'Dim conn As New SqlConnection(gloEMRAdmin.mdlGeneral.GetConnectionString)
        'Dim cmd As SqlCommand
        'Dim cnt As Integer = 0
        'Dim _strSQL As String = ""
        'Dim oDataReader As SqlDataReader

        'Try
        '    If optPwdComplexYes.Checked = True Then

        Dim frm As New frmPwdSettings
        frm.ShowDialog(Me)
        m_strSQL = frm.strSQL
        m_blnSetComplexisityOnSetting = frm.blnSetComplexisityOnSetting
        If m_blnSetComplexisityOnSetting = False Then
            MessageBox.Show("You have not set password complexity.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            optPwdComplexNo.Checked = True
        End If
        'Else
        'conn.Open()
        '_strSQL = "select count(*) from PwdSettings"
        'cmd = New SqlCommand(_strSQL, conn)
        'cnt = cmd.ExecuteScalar

        'If cnt = 0 Then
        '    'insert  row
        '    _strSQL = "insert into PwdSettings(ExpCapitalLetters,ExpNoOfLetters,ExpNoOfDigits,ExpNoOfSpecChars,ExpPwdLength,ExpTimeFrameinDays) " & _
        '              " values(" & 0 & "," & 0 & "," & 0 & "," & 0 & "," & 0 & "," & 0 & ")"

        'Else
        '    'update row
        '    _strSQL = "Update PwdSettings set ExpCapitalLetters = " & 0 & " ,ExpNoOfLetters = " & 0 & " ,ExpNoOfDigits = " & 0 & _
        '              ",ExpNoOfSpecChars = " & 0 & ",ExpPwdLength = " & 0 & ",ExpTimeFrameinDays = " & 0
        'End If
        'cmd = New SqlCommand(_strSQL, conn)
        'cmd.ExecuteNonQuery()
        '    End If
        'Catch ex As Exception
        '    MsgBox(ex.Message, MsgBoxStyle.OKOnly, "Password Complexity")
        'Finally

        'End Try
    End Sub

    Public Function GetAdminFlag() As Boolean
        Dim conn As New SqlConnection(gloEMRAdmin.mdlGeneral.GetConnectionString)
        Dim cmd As SqlCommand
        Dim cnt As Integer = 0
        Dim _strSQL As String = ""
        'Dim oDataReader As SqlDataReader
        Dim blnadmin As Boolean = False

        Try
            conn.Open()
            _strSQL = "select nAdministrator from User_MST where sLoginName ='" & gstrLoginName.Replace("'", "''") & "'"
            cmd = New SqlCommand(_strSQL, conn)
            blnadmin = cmd.ExecuteScalar

            Return blnadmin

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            conn.Close()

            If IsNothing(conn) = False Then
                conn.Dispose()
                conn = Nothing
            End If
        End Try
    End Function

    

    Private Sub txtThresholdValue_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        'Allow only numeric and decimal point keys
        If InStr(Trim(Text), ".") <> 0 And (e.KeyChar = ChrW(46)) Then
            e.Handled = True
        Else
            If Not ((e.KeyChar >= ChrW(48) And e.KeyChar <= ChrW(57)) Or (e.KeyChar = ChrW(46)) Or (e.KeyChar = ChrW(8))) Then
                e.Handled = True
            End If
        End If
    End Sub
  

    Private Sub btnNext1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNext1.Click
        tb_Settings.SelectedTab = tbp_FaxSettings
    End Sub

    Private Sub btnPrev2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrev2.Click
        tb_Settings.SelectedTab = tbp_ClinicSettings
    End Sub

    Private Sub btnNext2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNext2.Click
        tb_Settings.SelectedTab = tbp_OMRSettings
    End Sub

    Private Sub btnPrev3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrev3.Click
        tb_Settings.SelectedTab = tbp_FaxSettings
    End Sub

    Private Sub chkAutogenerateCode_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAutogenerateCode.CheckedChanged

    End Sub
    '****** By Sandip Deshmukh 26 Oct 2007 5.15PM
    '****** To add the follow up User DropDown
    Private Sub btnAddFollowUpUser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddFollowUpUser.Click
        Try
            _IsSurgicalUser = False
            ToolStrip1.Visible = True
            PnlUser.Visible = True
            PnlUser.BringToFront()
            'pnlFollowUpUser.Visible = True
            'pnlFollowUpUser.BringToFront()
            'colUsers.Clear()
            'colUId.Clear()

            ' setGridstyle()
            setGridstyle(C1surgicalUsers)
            Dim oDB As New gloStream.gloDataBase.gloDataBase
            ''commented by Mayuri:20101211
            ''Dim dt As New DataTable
            ''End code commented by Mayuri:20101211
            ''Commented By Dhruv20091211'' To Only use the Active User
            'Dim strSelectQry As String = "SELECT nUserID , sLoginName,ISNULL( User_MST.sFirstName,'')+' '+ISNULL(User_MST.sLastName,'') as Name, nProviderID FROM User_MST"
            Dim strSelectQry As String = "SELECT nUserID , sLoginName,ISNULL( User_MST.sFirstName,'')+' '+ISNULL(User_MST.sLastName,'') as Name, nProviderID FROM User_MST WHERE nBlockStatus = 0"

            oDB.Connect(gstrConnectionString)
            dtFollowupuser = oDB.ReadQueryData(strSelectQry)
            ''Added by Mayuri:20101211-
            ''to maintain check-uncheck status of followup users
            Dim dcID1 As New DataColumn("Check1", GetType(Integer))

            dcID1.DefaultValue = 0
            dtFollowupuser.Columns.Add(dcID1)
            Dim pk2 As DataColumn() = New DataColumn() {dtFollowupuser.Columns(0)}
            dtFollowupuser.PrimaryKey = pk2
            ''End code Added by Mayuri:20101211
            With C1surgicalUsers
                For i As Integer = 0 To dtFollowupuser.Rows.Count - 1
                    C1surgicalUsers.Rows.Add()
                    .SetCellCheck(i + 1, Col_Check, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
                    .SetData(i + 1, Col_UserID, dtFollowupuser.Rows(i)("nUserId"))
                    .SetData(i + 1, Col_LoginName, dtFollowupuser.Rows(i)("sLoginName"))
                    .SetData(i + 1, Col_Column1, dtFollowupuser.Rows(i)("Name"))
                    .SetData(i + 1, Col_ProviderID, dtFollowupuser.Rows(i)("nProviderID"))
                    'Dim rgActive As C1.Win.C1FlexGrid.CellRange = .GetCellRange(i + 1, Col_Check, i + 1, Col_Check)

                    'rgActive.StyleNew.DataType = GetType(Boolean)
                    'rgActive.StyleNew.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
                    'rgActive.StyleNew.ImageAlign = C1.Win.C1FlexGrid.ImageAlignEnum.CenterCenter
                Next
            End With
            Dim j As Int16
            Dim k As Int16
            If ToList.Count > 0 Then
                For j = 0 To ToList.Count - 1
                    For k = 0 To dtFollowupuser.Rows.Count - 1
                        If dtFollowupuser.Rows(k)(0).ToString() = ToList.Item(j).ID Then
                            'If dtsurgical.Rows(k)(1).ToString().Trim = gloItems.Item(j).Status.Trim Then
                            C1surgicalUsers.SetCellCheck(k + 1, Col_Check, CheckEnum.Checked)
                            Exit For

                        End If

                    Next
                Next
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnDelFollowUPUser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelFollowUPUser.Click
        Try
            'MessageBox.Show(cmbFollowUpUser.SelectedIndex)
            If cmbFollowUpUser.SelectedIndex = -1 Then

                MessageBox.Show("Select the Follow-Up user to remove.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            Else
                ''Commenetd by Mayuri:20101211
                ''If cmbFollowUpUser.SelectedItem.ToString.Trim <> "" Then
                ''    colUId.Remove(cmbFollowUpUser.SelectedIndex + 1)
                ''    colUsers.Remove(cmbFollowUpUser.SelectedIndex + 1)

                ''    cmbFollowUpUser.Items.Remove(cmbFollowUpUser.SelectedItem)
                ''End If
                ''end code commenetd by Mayuri:20101211
                ''Added by Mayuri:20101211
                If cmbFollowUpUser.SelectedIndex >= 0 Then
                    Dim _user As String = ""
                    _user = cmbFollowUpUser.SelectedItem.ToString.Trim
                    If cmbFollowUpUser.SelectedItem.ToString.Trim <> "" Then
                        Dim i As Int16
                        If ToList.Count > 0 Then
                            For i = 0 To ToList.Count - 1
                                ToItem = New gloGeneralItem.gloItem()
                                ToItem.Status = cmbFollowUpUser.SelectedItem.ToString.Trim()
                                If (ToList(i).Status.Trim = _user.Trim) Then
                                    ToList.RemoveAt(i)
                                    ToItem = Nothing
                                    Exit For
                                End If

                            Next
                        End If
                        cmbFollowUpUser.Items.Remove(cmbFollowUpUser.SelectedItem)
                    End If
                End If
                ''end code Added by Mayuri:20101211
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub setGridstyle(ByVal c1name As C1.Win.C1FlexGrid.C1FlexGrid)
        ' With c1userList
        With c1name


            .Rows.Count = 1
            .Cols.Count = Col_count


            .SetData(0, Col_Check, "Select")
            .Cols(Col_Check).ImageAlign = C1.Win.C1FlexGrid.ImageAlignEnum.CenterCenter
            .Cols(Col_Check).Width = 60
            .Cols(Col_Check).AllowEditing = True

            .SetData(0, Col_UserID, "UserID")
            .Cols(Col_UserID).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter
            .Cols(Col_UserID).Width = 0
            .Cols(Col_UserID).AllowEditing = False

            .SetData(0, Col_LoginName, "Login Name")
            .Cols(Col_LoginName).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter
            .Cols(Col_LoginName).Width = 225
            .Cols(Col_LoginName).AllowEditing = False

            .SetData(0, Col_Column1, "Name")
            .Cols(Col_Column1).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter
            .Cols(Col_Column1).Width = 300
            .Cols(Col_Column1).AllowEditing = False

            .SetData(0, Col_Column2, "Column2")
            .Cols(Col_Column2).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter
            .Cols(Col_Column2).Width = 0
            .Cols(Col_Column2).AllowEditing = False

            .SetData(0, Col_ProviderID, "ProviderID")
            .Cols(Col_ProviderID).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter
            .Cols(Col_ProviderID).Width = 0
            .Cols(Col_ProviderID).AllowEditing = False

        End With
    End Sub


    Private Sub btnc1ok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnc1ok.Click

        Try
            '  colUsers.Clear()
            '  colUId.Clear()
            '241107
            '
            ' If blnSurgicalclick = False Then 'means the btnAddFollowUpUser is clicked so update the cmbFollowUpUser


            cmbFollowUpUser.Items.Clear()
            For i As Integer = 0 To c1userList.Rows.Count - 1
                If C1surgicalUsers.GetCellCheck(i, Col_Check) = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                    'If c1userList.GetCellCheck(i, Col_Check) = C1.Win.C1FlexGrid.CheckEnum.Checked Then

                    ' Dim strItem As String = c1userList.GetData(i, Col_LoginName)
                    Dim strItem As String = C1surgicalUsers.GetData(i, Col_LoginName)
                    Dim blPresent As Boolean = False

                    'chk already in collection
                    For j As Integer = 1 To colUsers.Count
                        If colUsers.Item(j) = strItem Then
                            blPresent = True
                        End If
                    Next

                    'add if not present in collection
                    If Not blPresent Then
                        colUsers.Add(c1userList.GetData(i, Col_LoginName))
                        colUId.Add(c1userList.GetData(i, Col_UserID))
                    End If
                End If
            Next

            For j As Integer = 1 To colUsers.Count
                cmbFollowUpUser.Items.Add(colUsers.Item(j))
            Next
            PnlUser.Visible = False
            '  pnlFollowUpUser.Visible = False
            '  Else 'means the btnAddSurgicalAlertUser is clicked so update the cmbSurgicalAlert
            '    cmbSurgicalAlertUser.Items.Clear()
            '    For i As Integer = 0 To c1userList.Rows.Count - 1
            '        If c1userList.GetCellCheck(i, Col_Check) = C1.Win.C1FlexGrid.CheckEnum.Checked Then

            '            Dim strItem As String = c1userList.GetData(i, Col_LoginName)
            '            Dim blPresent As Boolean = False

            '            'chk already in collection
            '            For j As Integer = 1 To col_SurgicalUsers.Count
            '                If col_SurgicalUsers.Item(j) = strItem Then
            '                    blPresent = True
            '                End If
            '            Next

            '            'add if not present in collection
            '            If Not blPresent Then
            '                col_SurgicalUsers.Add(c1userList.GetData(i, Col_LoginName))
            '                col_SurgicalUId.Add(c1userList.GetData(i, Col_UserID))
            '            End If
            '        End If
            '    Next

            '    For j As Integer = 1 To col_SurgicalUsers.Count
            '        cmbSurgicalAlertUser.Items.Add(col_SurgicalUsers.Item(j))
            '    Next
            '    pnlFollowUpUser.Visible = False
            ' End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub btnclose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnclose.Click
        ' colUsers.Clear()
        ' colUId.Clear()
        PnlUser.Visible = False
        pnlFollowUpUser.Visible = False
    End Sub
    Private Sub fill_FollowUpUsers()
        Try
            Dim strQrey As String = ""
            Dim i As Integer

            colUsers.Clear()
            colUId.Clear()

            strQrey = " SELECT   User_MST.nUserID, User_MST.sLoginName FROM  User_MST INNER JOIN  Settings ON  Convert(varchar(80),(User_MST.nUserID)) = Settings.sSettingsValue  and sSettingsName='Followup User'  "
            Dim oDB As New gloStream.gloDataBase.gloDataBase

            Dim dt As New DataTable
            oDB.Connect(gstrConnectionString)
            dt = oDB.ReadQueryData(strQrey)
            If Not dt.Rows.Count <= 0 Then
                For i = 0 To dt.Rows.Count - 1
                    
                    ToItem = New gloGeneralItem.gloItem()
                    ToItem.ID = dt.Rows(i)("nUserID")
                    ToItem.Status = dt.Rows(i)("sLoginName")
                    ToList.Add(ToItem)
                    ToItem = Nothing

                    cmbFollowUpUser.Items.Add(dt.Rows(i)("sLoginName"))
                Next
            End If

            If IsNothing(dt) = False Then
                dt.Dispose()
                dt = Nothing
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    '****** 21 nov 2007 5.15PM

    'code added by sarika 18th jan 08 for gloReporting settings tab
    Private Sub btnPrev4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrev4.Click
        tb_Settings.SelectedTab = tbp_PMDBSettings
    End Sub

    Private Sub btnPrev5_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrev5.Click
        tb_Settings.SelectedTab = tbp_OMRSettings
    End Sub

    Private Sub btnNext3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNext3.Click
        tb_Settings.SelectedTab = tbp_PMDBSettings
    End Sub

    Private Sub btnNext4_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNext4.Click
        tb_Settings.SelectedTab = tbp_OtherSettings
    End Sub
    '----------------------------------'----------------------------------'----------------------------------

    Private Sub txtRptPassword_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtRptPassword.TextChanged

    End Sub


    'sarika UseFaxNoPrefix 12th april 08
    Private Sub chkUseFaxNoPrefix_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            If chkUseFaxNoPrefix.Checked = True Then
                txtFaxNoPrefix.Enabled = True

            Else
                txtFaxNoPrefix.Enabled = False
                txtFaxNoPrefix.Text = ""
            End If
        Catch ex As Exception

        End Try
    End Sub
    '--------------sarika UseFaxNoPrefix 12th april 08


    Private Sub btnAddSurgicalAlertUser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddSurgicalAlertUser.Click
        Try

            blnSurgicalclick = True 'refer the comment written for this flag.
            ToolStrip1.Visible = True
            PnlUser.Visible = True
            PnlUser.BringToFront()
            PnlUser.Visible = True
            pnlUserButton.Visible = True
            'pnlUserHeader.Visible = True
            'pnlFollowUpUser.Visible = True
            'pnlFollowUpUser.BringToFront()
            'colUsers.Clear()
            'colUId.Clear()


            'setGridstyle()
            setGridstyle(C1surgicalUsers)
            Dim oDB As New gloStream.gloDataBase.gloDataBase

            ''Commented Dhruv 20091211 ''  To only use the Active user
            'Dim strSelectQry As String = "SELECT nUserID , sLoginName,ISNULL( User_MST.sFirstName,'')+' '+ISNULL(User_MST.sLastName,'') as Name, nProviderID FROM User_MST"
            Dim strSelectQry As String = "SELECT nUserID , sLoginName,ISNULL( User_MST.sFirstName,'')+' '+ISNULL(User_MST.sLastName,'') as Name, nProviderID FROM User_MST WHERE nBlockStatus = 0"

            oDB.Connect(gstrConnectionString)
            dtsurgical = oDB.ReadQueryData(strSelectQry)
            Dim dcID1 As New DataColumn("Check1", GetType(Integer))

            dcID1.DefaultValue = 0
            dtsurgical.Columns.Add(dcID1)
            Dim pk2 As DataColumn() = New DataColumn() {dtsurgical.Columns(0)}
            dtsurgical.PrimaryKey = pk2
            With C1surgicalUsers
                For i As Integer = 0 To dtsurgical.Rows.Count - 1
                    .Rows.Add()
                    .SetCellCheck(i + 1, Col_Check, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
                    .SetData(i + 1, Col_UserID, dtsurgical.Rows(i)("nUserId"))
                    .SetData(i + 1, Col_LoginName, dtsurgical.Rows(i)("sLoginName"))
                    .SetData(i + 1, Col_Column1, dtsurgical.Rows(i)("Name"))
                    .SetData(i + 1, Col_ProviderID, dtsurgical.Rows(i)("nProviderID"))
                    'Dim rgActive As C1.Win.C1FlexGrid.CellRange = .GetCellRange(i + 1, Col_Check, i + 1, Col_Check)

                    'rgActive.StyleNew.DataType = GetType(Boolean)
                    'rgActive.StyleNew.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
                    'rgActive.StyleNew.ImageAlign = C1.Win.C1FlexGrid.ImageAlignEnum.CenterCenter
                Next
            End With
            _IsSurgicalUser = True
            Dim j As Int16
            Dim k As Int16
            If gloItems.Count > 0 Then
                For j = 0 To gloItems.Count - 1
                    For k = 0 To dtsurgical.Rows.Count - 1
                        If dtsurgical.Rows(k)(0).ToString() = gloItems.Item(j).ID Then
                            'If dtsurgical.Rows(k)(1).ToString().Trim = gloItems.Item(j).Status.Trim Then
                            C1surgicalUsers.SetCellCheck(k + 1, Col_Check, CheckEnum.Checked)
                            Exit For

                        End If

                    Next
                Next
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnDelSurgicalAlertUser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelSurgicalAlertUser.Click
        Try
            If cmbSurgicalAlertUser.SelectedIndex = -1 Then

                MessageBox.Show("Select the Surgical Alert user to remove.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            Else
                '''If cmbSurgicalAlertUser.SelectedIndex >= 0 Then
                ' ''Dim _userId As Int64 = 0
                ' ''Dim i As Integer
                ' ''_userId = Convert.ToInt64(cmbSurgicalAlertUser.SelectedValue)
                ' ''For i = 0 To gloItems.Count - 1
                ' ''    If (gloItems(i).ID = _userId) Then
                ' ''        gloItems.RemoveAt(i)
                ' ''        Exit For
                ' ''    End If
                ' ''Next

                ' ''Dim dtUsers As DataTable = DirectCast(cmbSurgicalAlertUser.DataSource, DataTable)
                ' ''dtUsers.Rows.RemoveAt(cmbSurgicalAlertUser.SelectedIndex)
                ' ''If cmbSurgicalAlertUser.Items.Count = 0 Then
                ' ''    cmbSurgicalAlertUser.Text = ""
                ' ''Else
                ' ''    cmbSurgicalAlertUser.SelectedIndex = 0
                ' ''End If
                '''End If
                If cmbSurgicalAlertUser.SelectedIndex >= 0 Then
                    Dim _user As String = ""

                    _user = cmbSurgicalAlertUser.SelectedItem.ToString.Trim
                    If cmbSurgicalAlertUser.SelectedItem.ToString.Trim <> "" Then
                        'col_SurgicalUId.Remove(cmbSurgicalAlertUser.SelectedIndex + 1)
                        'col_SurgicalUsers.Remove(cmbSurgicalAlertUser.SelectedIndex + 1)
                        Dim i As Int16
                        If gloItems.Count > 0 Then
                            For i = 0 To gloItems.Count - 1


                                gloItem = New gloGeneralItem.gloItem()
                                'gloItem.ID = cmbSurgicalAlertUser.SelectedIndex + 1
                                'gloItem.ID = cmbSurgicalAlertUser.SelectedIndex + 1
                                gloItem.Status = cmbSurgicalAlertUser.SelectedItem.ToString.Trim()
                                If (gloItems(i).Status.Trim = _user.Trim) Then
                                    gloItems.RemoveAt(i)
                                    gloItem = Nothing
                                    Exit For
                                End If

                            Next
                        End If
                        cmbSurgicalAlertUser.Items.Remove(cmbSurgicalAlertUser.SelectedItem)


                    End If
                End If
                End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub fill_SurgicalAlertUsers()
        Try
            Dim strQrey As String = ""
            Dim i As Integer

            'col_SurgicalUsers.Clear()
            'col_SurgicalUId.Clear()
            gloItems = New gloGeneralItem.gloItems()
            Dim gloItem As gloGeneralItem.gloItem


            strQrey = " SELECT   User_MST.nUserID, User_MST.sLoginName FROM  User_MST INNER JOIN  Settings ON  Convert(varchar(80),(User_MST.nUserID)) = Settings.sSettingsValue  and sSettingsName='Surgical User'  "
            Dim oDB As New gloStream.gloDataBase.gloDataBase

            Dim dt As New DataTable
            oDB.Connect(gstrConnectionString)
            dt = oDB.ReadQueryData(strQrey)
            If Not dt.Rows.Count <= 0 Then
                For i = 0 To dt.Rows.Count - 1
                    gloItem = New gloGeneralItem.gloItem()
                    gloItem.ID = dt.Rows(i)("nUserID")
                    gloItem.Status = dt.Rows(i)("sLoginName")
                    gloItems.Add(gloItem)
                    gloItem = Nothing
                    'col_SurgicalUId.Add(dt.Rows(i)("nUserID"))
                    'col_SurgicalUsers.Add(dt.Rows(i)("sLoginName"))
                    cmbSurgicalAlertUser.Items.Add(dt.Rows(i)("sLoginName"))
                Next
            End If

            If IsNothing(dt) = False Then
                dt.Dispose()
                dt = Nothing
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

   
    Private Sub fill_MedicalCategory()
        dtAllMedCat = New DataTable
        dtCmdMedCat = New DataTable
        Try
            Dim strQreyAllMedCat As String = ""
            Dim strQreySelMedCat As String = ""


            DesignGridForMedicalCategory()

            Dim oDB As New gloStream.gloDataBase.gloDataBase
            ''strQreyAllMedCat = " SELECT nMedicalCategoryID, sMedicalCategory,ISNULL(bIsHighRisk,0) as bIsHighRisk  FROM MedicalCategory_Mst where bIsActive=1 order by sMedicalCategory"
            strQreyAllMedCat = " SELECT  Case  When MedicalCategoryOB.nMedicalCategoryID    IS NULL Then 'False' Else 'True' End as [Select]  , MedicalCategory_Mst.nMedicalCategoryID as nMedicalCategoryID , sMedicalCategory ,ISNULL(bIsHighRisk,0) as bIsHighRisk  FROM MedicalCategory_Mst LEFT Outer JOIN MedicalCategoryOB ON MedicalCategoryOB.nMedicalCategoryID=MedicalCategory_Mst.nMedicalCategoryID  where bIsActive=1 order by sMedicalCategory "

            '' strQreySelMedCat = "select nMedicalCategoryID from MedicalCategoryOB"

            oDB.Connect(gstrConnectionString)
            dtAllMedCat = oDB.ReadQueryData(strQreyAllMedCat)
            C1MedicalCategory.DataSource = dtAllMedCat.DefaultView
            C1MedicalCategory.Cols(0).DataType = GetType(Boolean)
            C1MedicalCategory.SetData(0, 2, "Medical Category")
            C1MedicalCategory.Cols(1).Visible = False
            C1MedicalCategory.Cols(3).Visible = False
            C1MedicalCategory.Cols(2).AllowEditing = False
          
            dtCmdMedCat = dtAllMedCat.Clone()
            Dim dradd As DataRow = dtCmdMedCat.NewRow() ''added for medical category risk functionality
            dradd("Select") = "False"
            dradd("nMedicalCategoryID") = -1
            dradd("sMedicalCategory") = ""
            dradd("bIsHighRisk") = "False"
            dtCmdMedCat.Rows.Add(dradd)
            Dim drr As DataRow() = dtAllMedCat.Select("Select='True'")
            For Each drc As DataRow In drr
                dtCmdMedCat.ImportRow(drc)
            Next
            cmbmedrsk.DataSource = dtCmdMedCat
            cmbmedrsk.DisplayMember = "sMedicalCategory"
            cmbmedrsk.ValueMember = "nMedicalCategoryID"
            cmbmedrsk.Text = ""
            cmbmedrsk.SelectedValue = -1

            Dim dr As DataRow() = dtCmdMedCat.Select("bIsHighRisk=True")
            If (dr.Length > 0) Then
                Dim ind As Integer = dtCmdMedCat.Rows.IndexOf(dr(0))

                cmbmedrsk.SelectedIndex = ind
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
           
        End Try
    End Sub
    ''End OB Setting

    Private Sub chkInternetFax_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkInternetFax.CheckedChanged
        'Try
        '    'If chkInternetFax.Checked = True Then
        '    '    pnleFaxLoginKey.Visible = True
        '    'Else
        '    '    pnleFaxLoginKey.Visible = False
        '    'End If
        'Catch ex As Exception
        '    MessageBox.Show(ex.Message, "Settings", MessageBoxButtons.OK, MessageBoxIcon.Error)
        'End Try
    End Sub

    'code added by supriya 11/7/2008
    Private Sub chkSurescript_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkSurescript.CheckedChanged
        Try
            If chkSurescript.Checked = True Then
                pnlSurescriptServer.Enabled = True

                ''Discussion with Pravin Sir :: surescript true [checked] = adv rx enable [checked] , and viceversa
                chkAdvanceRx.Checked = True
                chkAdvanceRx.Enabled = True
                chkFormularyEnable.Enabled = True
                pnlAdvanceRxServer.Enabled = True

                ''dhruv 20100216
                ''formulary db setting
                pnlFormularySetting.Enabled = True
                ''end
                TxtSurescriptURL.Enabled = True
                pnlSecureMessage.Enabled = True
                pnlMedHistory.Enabled = True
            Else
                pnlSurescriptServer.Enabled = False

                ''Discussion with Pravin Sir :: surescript true [checked] = adv rx enable [checked] , and viceversa
                chkAdvanceRx.Checked = False
                chkAdvanceRx.Enabled = False
                chkFormularyEnable.Enabled = False
                pnlAdvanceRxServer.Enabled = False

                ''dhruv 20100216
                ''formulary db setting
                pnlFormularySetting.Enabled = False
                ''end
                TxtSurescriptURL.Enabled = False

                pnlSecureMessage.Enabled = False
                pnlMedHistory.Enabled = False
                chkSecureMesaage.Checked = False
                chkMedHistory.Checked = False
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnBrowseeFaxDownloadDir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowseeFaxDownloadDir.Click
        Try
            With FolderBrowserDialog1()
                .ShowNewFolderButton = True
                .Description = "Select eFax Download Path"
                If .ShowDialog() = DialogResult.OK Then
                    txteFaxDownloadDir.Text = .SelectedPath
                End If
            End With
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub lblPwdComplexity_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblPwdComplexity.Click

    End Sub

    Private Sub tbp_OtherSettings_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbp_OtherSettings.Click

    End Sub

    Private Sub btnMCIRReportPath_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMCIRReportPath.Click
        Try
            With FolderBrowserDialog1()
                .ShowNewFolderButton = True
                .Description = "Select MCIR Report Path"
                If .ShowDialog() = DialogResult.OK Then
                    txtMCIRReportPath.Text = .SelectedPath
                End If
            End With
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub chk_AgeFlag_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chk_AgeFlag.CheckedChanged
        ''by sudhir 20081111
        If chk_AgeFlag.Checked = True Then
            txtAgeLimitPatientStrip.Enabled = True
            txtAgeLimitforWeeks.Enabled = True
        Else
            txtAgeLimitPatientStrip.Enabled = False
            txtAgeLimitforWeeks.Enabled = False
        End If
        ''end sudhir
    End Sub

    Private Sub txtAgeLimitPatientStrip_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAgeLimitPatientStrip.KeyPress
        ''sudhir 20081112
        If Not ((e.KeyChar >= ChrW(48) And e.KeyChar <= ChrW(57)) Or (e.KeyChar = ChrW(8))) Then
            e.Handled = True
        End If
        ''
    End Sub

    Private Sub txtAgeLimitPatientStrip_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAgeLimitPatientStrip.LostFocus
        If txtAgeLimitPatientStrip.Text = "" Then
            txtAgeLimitPatientStrip.Text = 0
        End If
    End Sub

    Private Sub txtAgeLimitforWeek_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If Not ((e.KeyChar >= ChrW(48) And e.KeyChar <= ChrW(57)) Or (e.KeyChar = ChrW(8))) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtAgeLimitforWeek_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        If txtAgeLimitforWeeks.Text = "" Then
            txtAgeLimitforWeeks.Text = 0
        End If
    End Sub

    Private Sub txtAgeLimitPatientStrip_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtAgeLimitPatientStrip.Validating
        'Try
        '    If Val(txtAgeLimitPatientStrip.Text) < (Val(txtAgeLimitforWeeks.Text) * 30.4375) Then
        '        MessageBox.Show("Age Limit for day must be greater than " & CType(CType(Val(txtAgeLimitforWeeks.Text) * 30.4375, Int64), String) & " days", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        '        txtAgeLimitPatientStrip.Focus()
        '    End If
        'Catch ex As Exception

        'End Try

    End Sub

    Private Sub optSQLAuthentication_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optSQLAuthentication.CheckedChanged
        If optSQLAuthentication.Checked = True Then
            optSQLAuthentication.Font = New Font("Tahoma", 9, FontStyle.Bold)
            pnlSQLCredentials.Enabled = True
        Else
            optSQLAuthentication.Font = New Font("Tahoma", 9, FontStyle.Regular)
            pnlSQLCredentials.Enabled = False
        End If
    End Sub


    Private Sub chk_PMDBSettings_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chk_PMDBSettings.CheckedChanged
        'pnlgloPMDBSettings.Enabled = chk_PMDBSettings.Checked
        Set_gloEMRDBsettingControls()
    End Sub


    Private Sub btnCCDfilePath_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCCDfilePath.Click
        Try
            With FolderBrowserDialog1()
                .ShowNewFolderButton = True
                .Description = "Select CCD File Path"
                If .ShowDialog() = DialogResult.OK Then
                    txtCCDFilePath.Text = .SelectedPath
                End If
            End With
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub grpOtherDMSSettings_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub


    '' Sudhir 20090107 ''
    Public Sub FillProviderGridAll()
        Dim objSettings As New clsSettings
        Dim dt As DataTable
        Dim cStyle As C1.Win.C1FlexGrid.CellStyle
        Dim cRange As C1.Win.C1FlexGrid.CellRange

        IsProviderLoading = True
        DesignProviderGrid()

        dt = Get_gloEMR_ProviderList()
        Try

            ''Fill gloEMR Provider
            If IsNothing(dt) = False Then
                If dt.Rows.Count > 0 Then
                    For i As Integer = 0 To dt.Rows.Count - 1
                        C1Provider.Rows.Add()
                        C1Provider.Rows(i + 1)(col_gloEMR_ProviderName) = dt.Rows(i)("ProviderName").ToString().Trim
                        If IsDBNull(dt.Rows(i)("sExternalCode")) = False Then
                            If dt.Rows(i)("sExternalCode") <> "" Then
                                C1Provider.Rows(i + 1)(col_ExternalID) = dt.Rows(i)("sExternalCode")
                            End If
                        End If
                    Next
                End If
            End If

            If optSQLAuthentication.Checked = False Then
                objSettings.PMSQLAuthentication = 0
                objSettings.PMConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString(txtPMServerName.Text.Trim, txtPMDatabaseName.Text.Trim, False, "", "")
                objSettings.PMUserID = ""
                objSettings.PMSQLPwd = ""
            Else
                objSettings.PMSQLAuthentication = 1
                objSettings.PMUserID = txtSQLUserID.Text.Trim
                objSettings.PMSQLPwd = txtSQLPassword.Text.Trim
                objSettings.PMConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString(txtPMServerName.Text.Trim, txtPMDatabaseName.Text.Trim, txtSQLUserID.Text.Trim, txtSQLPassword.Text.Trim)
            End If

            Dim _strPM_Provider As String = Get_gloPM_ProviderList(objSettings.PMConnectionString)

            If C1Provider.Rows.Count > 1 Then
                Dim _gloPM_provider As String = ""
                Dim _ExternalID As String = ""

                ''Fill Provider from gloPM
                For i As Integer = 1 To C1Provider.Rows.Count - 1
                    cRange = C1Provider.GetCellRange(i, col_gloPM_ProviderName, i, col_gloPM_ProviderName)
                    cStyle = C1Provider.Styles.Add("PMProvider")
                    cStyle.ComboList = _strPM_Provider
                    cRange.Style = cStyle

                    ''Fill gloPM Provider Against External ID
                    _ExternalID = C1Provider.GetData(i, col_ExternalID)
                    If IsNothing(_ExternalID) = False Then
                        _gloPM_provider = Get_gloPM_Provider(objSettings.PMConnectionString, _ExternalID)
                        If _gloPM_provider <> "" Then
                            C1Provider.Rows(i)(col_gloPM_ProviderName) = _gloPM_provider
                            'Else
                            '    C1Provider.Rows(i)(col_ExternalID) = Nothing
                        End If
                    End If
                Next
            End If



        Catch ex As Exception
        Finally
            IsProviderLoading = False
        End Try
    End Sub

    Public Sub DesignProviderGrid()
        With C1Provider
            .AllowSorting = AllowSortingEnum.None
            .Visible = True
            .BringToFront()
            .Cols.Count = col_Provider_Count
            .Rows.Count = 1
            .Rows.Fixed = 1
            .Cols.Fixed = 0

            .Cols(col_gloEMR_ProviderName).Width = .Width * 0.35 '220
            .Cols(col_gloPM_ProviderName).Width = .Width * 0.35
            .Cols(col_ExternalID).Width = .Width * 0.265

            .Cols(col_gloPM_ProviderName).AllowEditing = True
            .Cols(col_gloPM_ProviderName).DataType = GetType(String)

            .Cols(col_ExternalID).AllowEditing = True
            .Cols(col_ExternalID).DataType = GetType(String)
            If (gstrAdminFor = "gloPM") Then
                .SetData(0, col_gloEMR_ProviderName, "gloPM Provider")
                .SetData(0, col_gloPM_ProviderName, "gloEMR Provider")
            Else
                .SetData(0, col_gloEMR_ProviderName, "gloEMR Provider")
                .SetData(0, col_gloPM_ProviderName, "gloPM Provider")
            End If

            .SetData(0, col_ExternalID, "External ID")

            .Cols(col_gloEMR_ProviderName).TextAlign = TextAlignEnum.LeftCenter
            .Cols(col_gloPM_ProviderName).TextAlign = TextAlignEnum.LeftCenter
            .Cols(col_ExternalID).TextAlign = TextAlignEnum.LeftCenter
        End With
    End Sub

    Public Function GetC1RowProviderDetail() As ArrayList
        Dim _list As New ArrayList
        Dim _rowDetail As myList

        Dim _gloEMR_Provider As String
        Dim _gloPM_Provider As String
        Dim _ExternalID As String

        Try
            If IsNothing(C1Provider) = False Then
                If C1Provider.Rows.Count > 1 Then
                    For i As Integer = 1 To C1Provider.Rows.Count - 1

                        _gloEMR_Provider = C1Provider.GetData(i, col_gloEMR_ProviderName)
                        _gloPM_Provider = C1Provider.GetData(i, col_gloPM_ProviderName)
                        _ExternalID = C1Provider.GetData(i, col_ExternalID)

                        'If _gloPM_Provider <> "" And _ExternalID <> "" Then
                        _rowDetail = New myList
                        _rowDetail.gloEMR_Provider = _gloEMR_Provider
                        _rowDetail.gloPM_Provider = _gloPM_Provider
                        _rowDetail.ExternalID = _ExternalID
                        _list.Add(_rowDetail)
                        _rowDetail = Nothing
                        'End If

                        _gloEMR_Provider = ""
                        _gloPM_Provider = ""
                        _ExternalID = ""

                    Next
                End If
            End If
            Return _list
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Private Sub C1Provider_CellChanged(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles C1Provider.CellChanged
        If IsProviderLoading = False Then

            Dim _gloPM_Provider As String = C1Provider.GetData(C1Provider.Row, col_gloPM_ProviderName)
            Dim _ExternalID As String = C1Provider.GetData(C1Provider.Row, col_ExternalID)

            If IsNothing(_gloPM_Provider) = False And _gloPM_Provider <> "" And _gloPM_Provider <> " " Then
                For i As Integer = 1 To C1Provider.Rows.Count - 1
                    If i <> C1Provider.Row Then
                        If _gloPM_Provider = C1Provider.GetData(i, col_gloPM_ProviderName) Then
                            MessageBox.Show("Provider already present.", mdlGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            IsProviderLoading = True
                            C1Provider.SetData(C1Provider.Row, col_gloPM_ProviderName, Nothing)
                            IsProviderLoading = False
                            Exit Sub
                        End If
                    End If
                Next
            End If

            If IsNothing(_ExternalID) = False And _ExternalID <> "" Then
                For i As Integer = 1 To C1Provider.Rows.Count - 1
                    If i <> C1Provider.Row Then
                        If _ExternalID = C1Provider.GetData(i, col_ExternalID) Then
                            MessageBox.Show("External ID already present.", mdlGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            IsProviderLoading = True
                            C1Provider.SetData(C1Provider.Row, col_ExternalID, Nothing)
                            IsProviderLoading = False
                            Exit Sub
                        End If
                    End If
                Next
            End If
        End If
    End Sub
    '' End Sudhir ''

    Private Sub btnConnect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConnect.Click, pnlgloPMDBSettings.Click
        '' Sudhir 20090109 ''
        '' PM connection Validation ''
        ''Sandip Darade 20090731
        Dim objSQLSettings As New clsStartUpSettings
        Dim str = ""
        If (gstrAdminFor = "gloPM") Then
            str = "gloEMR"
        Else
            str = "gloPM"
        End If

        If chk_PMDBSettings.Checked = True Then

            If txtPMServerName.Text.Trim = "" Then
                MessageBox.Show("Enter  " + str + " Server Name.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                C1Provider.Rows.Count = 1
                Exit Sub
            End If

            If txtPMDatabaseName.Text.Trim = "" Then
                MessageBox.Show("Enter  " + str + " Database Name.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                C1Provider.Rows.Count = 1
                Exit Sub
            End If

            If optSQLAuthentication.Checked = True Then
                If txtSQLUserID.Text.Trim = "" Then
                    MessageBox.Show("Enter SQL User ID for  " + str + ".", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    C1Provider.Rows.Count = 1
                    Exit Sub
                End If

                If txtSQLPassword.Text.Trim = "" Then
                    MessageBox.Show("Enter SQL Password for  " + str + ".", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    C1Provider.Rows.Count = 1
                    Exit Sub
                End If

                If objSQLSettings.IsSQLConnect(txtPMServerName.Text.Trim, txtPMDatabaseName.Text.Trim, txtSQLUserID.Text.Trim, txtSQLPassword.Text.Trim) = False Then
                    If MessageBox.Show("Unable to connect to SQL Server " & txtPMServerName.Text.Trim & " and Database " & txtPMDatabaseName.Text.Trim & vbCrLf & "Select valid credentials.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information) Then
                        C1Provider.Rows.Count = 1
                        Exit Sub
                    End If
                End If
            Else
                If objSQLSettings.IsConnect(txtPMServerName.Text.Trim, txtPMDatabaseName.Text.Trim, False, "", "") = False Then
                    If MessageBox.Show("Unable to connect to SQL Server " & txtPMServerName.Text.Trim & " and Database " & txtPMDatabaseName.Text.Trim & vbCrLf & "Select valid credentials.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information) Then
                        C1Provider.Rows.Count = 1
                        Exit Sub
                    End If
                End If
            End If

            FillProviderGridAll()

        End If
    End Sub

    '\\Added by Suraj 20090128
    '\\ For Recovery DMS V2 Document Path
    Private Sub btnSelectRecoveryPath_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelectRecoveryPath.Click
        Try
            With FolderBrDialog_DMSV2RecovryPath()
                .ShowNewFolderButton = True
                .Description = "Select DMS V2 Document Recovery Path"
                If .ShowDialog() = DialogResult.OK Then
                    txt_DMSV2RecoveryPath.Text = .SelectedPath
                End If
            End With
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub chkRecoverDMSV2Doc_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkRecoverDMSV2Doc.CheckedChanged

        Try
            If chkRecoverDMSV2Doc.Checked = True Then
                lblDMSV2RecoverPath.Enabled = True
                txt_DMSV2RecoveryPath.Enabled = True
                btnSelectRecoveryPath.Enabled = True
            Else
                lblDMSV2RecoverPath.Enabled = False
                txt_DMSV2RecoveryPath.Enabled = False
                btnSelectRecoveryPath.Enabled = False
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub TabControl1_SelectedIndexChanged_1(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub
    Private Sub TabControl1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub TabPage17_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabPage17.Click

    End Sub

    Private Sub Button65_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button65.Click
        tb_Settings.SelectedTab = tbp_OtherSettings
    End Sub

    Private Sub Button64_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button64.Click
        tb_Settings.SelectedTab = tbp_EMCodeSetting
    End Sub


    Private Sub Chb_UseCodedhistory_CheckedChanged_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Chb_UseCodedhistory.CheckedChanged
        Try
            If Chb_UseCodedhistory.Checked = True Then
                Rbtn_showcode.Enabled = True
                Rbtn_showDesc.Enabled = True
                Rbtn_ShowBoth.Enabled = True
            Else
                Rbtn_showcode.Enabled = False
                Rbtn_showDesc.Enabled = False
                Rbtn_ShowBoth.Enabled = False
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    'sarika for Internet fax 20090502
    Private Function CheckEFaxModifications() As Boolean
        Dim blnInternetFax As Boolean = False

        Try

            If chkInternetFax.Checked = True Then



                'chk the current values with the stored vals

                'sarika  UseFaxNoPrefix 12th april 08
                Dim UseFaxPrefix As Integer
                '------ UseFaxNoPrefix 12th april 08

                'Dim blnSendEMail As Integer

                'sarika  UseFaxNoPrefix 12th april 08
                If chkUseFaxNoPrefix.Checked = True Then
                    UseFaxPrefix = 1
                Else
                    UseFaxPrefix = 0
                End If
                '------ UseFaxNoPrefix 12th april 08

                'sarika SendEMail 20090502
                'If chkSendMail.Checked = True Then
                '    blnSendEMail = 1
                'Else
                '    blnSendEMail = 0
                'End If
                '---

                'sarika UseFaxNoPrefix 12th april 08
                If _arrayGetData(5) <> UseFaxPrefix Then
                    bEFaxSettingsModified = True
                    Return bEFaxSettingsModified
                End If
                If _arrayGetData(6) <> txtFaxNoPrefix.Text.Trim Then
                    bEFaxSettingsModified = True
                    Return bEFaxSettingsModified
                End If

                '------------sarika UseFaxNoPrefix 12th april 08
                'If _arrayGetData(8) <> blnSendEMail Then
                '    bEFaxSettingsModified = True
                '    Return bEFaxSettingsModified
                'End If
                'If _arrayGetData(9) <> txtSendMail.Text.Trim Then
                '    bEFaxSettingsModified = True
                '    Return bEFaxSettingsModified
                'End If

                If _arrayGetData(10) <> txteFaxDownloadDir.Text.Trim Then
                    bEFaxSettingsModified = True
                    Return bEFaxSettingsModified
                End If

                'sarika SendEMail 20090502

                '--

                'sarika 
                'DMS 20080908 -- for Loading no of recieved faxes in DMS
                'If _arrayGetData(7) <> numLoadNoOfFaxes.Value Then
                '    bModifyData = True
                '    Return bModifyData
                'End If
                '---------------
            End If

        Catch ex As Exception
            MessageBox.Show("Error in Setdata : " & ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function
    '----

    Private Sub chkSendMail_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'Try
        '    If chkSendMail.Checked = True Then
        '        txtSendMail.Visible = True
        '        lblEnterEmailAddress.Visible = True

        '    Else
        '        'txtSendMail.Text = ""
        '        txtSendMail.Visible = False
        '        lblEnterEmailAddress.Visible = False

        '    End If
        'Catch ex As Exception
        '    MessageBox.Show("Error setting email address for sending Fax Failure notification", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        'End Try
    End Sub




    '\\commented on 20090820:
    Private Sub btnBrowseEAR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'Try
        '    FolderBrowserDialog_EAR.Description = "Select ePrescribing Report Directory"

        '    If FolderBrowserDialog_EAR.ShowDialog = Windows.Forms.DialogResult.OK Then
        '        txtEARDirectory.Text = FolderBrowserDialog_EAR.SelectedPath.ToString()
        '    End If
        'Catch ex As Exception
        '    MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        'End Try
    End Sub



    Private Sub btnCancelSurgicalUsers_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelSurgicalUsers.Click
        PnlUser.SendToBack()
        PnlUser.Visible = False

    End Sub




    Private Sub btnOkSurgicalusers_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOkSurgicalusers.Click
        Try
            'Code Added by Mayuri:20090926
            'If we want to save Users into cmbSurgicalUsers 
            ''If _IsSurgicalUser = True Then
            ''Dim dtUsers As New DataTable()
            ''Dim dcId As New DataColumn("ID")
            ''Dim dcDescription As New DataColumn("Description")
            ''dtUsers.Columns.Add(dcId)
            ''dtUsers.Columns.Add(dcDescription)

            ' ''gloItems = New gloGeneralItem.gloItems()
            ' ''Dim gloItem As gloGeneralItem.gloItem

            ''If gloItems.Count > 0 Then
            ''    For i As Int16 = 0 To gloItems.Count - 1
            ''        Dim drTemp As DataRow = dtUsers.NewRow()
            ''        drTemp("ID") = gloItems(i).ID
            ''        drTemp("Description") = gloItems(i).Status
            ''        dtUsers.Rows.Add(drTemp)

            ''        gloItem = New gloGeneralItem.gloItem()

            ''        gloItem.ID = gloItems(i).ID
            ''        gloItem.Description = gloItems(i).Status

            ''        gloItems.Add(gloItem)
            ''        gloItem = Nothing
            ''    Next
            ''End If
            ''cmbSurgicalAlertUser.DataSource = dtUsers

            ''cmbSurgicalAlertUser.SelectedIndex = 0
            ''cmbSurgicalAlertUser.ValueMember = dtUsers.Columns("ID").ColumnName
            ''cmbSurgicalAlertUser.DisplayMember = dtUsers.Columns("Description").ColumnName

            If _IsSurgicalUser = True Then
                cmbSurgicalAlertUser.Items.Clear()
                For i As Integer = 0 To C1surgicalUsers.Rows.Count - 1
                    If C1surgicalUsers.GetCellCheck(i, Col_Check) = C1.Win.C1FlexGrid.CheckEnum.Checked Then

                        Dim strItem As String = C1surgicalUsers.GetData(i, Col_LoginName)
                        Dim blPresent As Boolean = False

                        'chk already in collection
                        'For j As Integer = 1 To col_SurgicalUsers.Count
                        If gloItems.Count > 0 Then
                            For j As Integer = 0 To gloItems.Count - 1
                                ' If col_SurgicalUsers.Item(j) = strItem Then
                                If gloItems.Item(j).Status.ToString() = strItem Then
                                    blPresent = True
                                End If
                            Next
                        End If
                        'add if not present in collection
                        If Not blPresent Then
                            gloItem = New gloGeneralItem.gloItem()
                            gloItem.ID = gloItems(i).ID
                            gloItem.Status = gloItems(i).Status
                            gloItems.Add(gloItem)
                            gloItem = Nothing
                            'col_SurgicalUsers.Add(C1surgicalUsers.GetData(i, Col_LoginName))
                            'col_SurgicalUId.Add(C1surgicalUsers.GetData(i, Col_UserID))
                        End If
                    End If
                Next

                'For j As Integer = 1 To col_SurgicalUsers.Count
                If gloItems.Count > 0 Then
                    For j As Integer = 0 To gloItems.Count - 1
                        cmbSurgicalAlertUser.Items.Add(gloItems.Item(j).Status.ToString().Trim)
                    Next
                End If
                PnlUser.Visible = False
                PnlUser.SendToBack()
                'Code Added by Mayuri:20090926
                'If we want to save Users into cmbFollowUp user
            ElseIf _IsSurgicalUser = False Then
                cmbFollowUpUser.Items.Clear()
                ''commenetd by Mayuri:20101211-To maintain check-uncheck status of follow up users and surgical users
                ''For i As Integer = 0 To C1surgicalUsers.Rows.Count - 1
                ''    If C1surgicalUsers.GetCellCheck(i, Col_Check) = C1.Win.C1FlexGrid.CheckEnum.Checked Then

                ''        Dim strItem As String = C1surgicalUsers.GetData(i, Col_LoginName)
                ''        Dim blPresent As Boolean = False

                ''        'chk already in collection
                ''        'For j As Integer = 1 To col_SurgicalUsers.Count
                ''        '    If col_SurgicalUsers.Item(j) = strItem Then
                ''        For j As Integer = 1 To colUsers.Count
                ''            If colUsers.Item(j) = strItem Then
                ''                blPresent = True
                ''            End If
                ''        Next

                ''        'add if not present in collection
                ''        If Not blPresent Then
                ''            ' col_SurgicalUsers.Add(C1surgicalUsers.GetData(i, Col_LoginName))
                ''            ' col_SurgicalUId.Add(C1surgicalUsers.GetData(i, Col_UserID))
                ''            colUsers.Add(C1surgicalUsers.GetData(i, Col_LoginName))
                ''            colUId.Add(C1surgicalUsers.GetData(i, Col_UserID))
                ''        End If
                ''    End If
                ''Next

                ' ''For j As Integer = 1 To col_SurgicalUsers.Count
                ' ''    cmbFollowUpUser.Items.Add(col_SurgicalUsers.Item(j))
                ''For j As Integer = 1 To colUsers.Count
                ''    cmbFollowUpUser.Items.Add(colUsers.Item(j))
                ''Next
                For i As Integer = 0 To C1surgicalUsers.Rows.Count - 1
                    If C1surgicalUsers.GetCellCheck(i, Col_Check) = C1.Win.C1FlexGrid.CheckEnum.Checked Then

                        Dim strItem As String = C1surgicalUsers.GetData(i, Col_LoginName)
                        Dim blPresent As Boolean = False

                        'chk already in collection
                        'For j As Integer = 1 To col_SurgicalUsers.Count
                        If ToList.Count > 0 Then
                            For j As Integer = 0 To ToList.Count - 1
                                ' If col_SurgicalUsers.Item(j) = strItem Then
                                If ToList.Item(j).Status.ToString() = strItem Then
                                    blPresent = True
                                End If
                            Next
                        End If
                        'add if not present in collection
                        If Not blPresent Then
                            ToItem = New gloGeneralItem.gloItem()
                            ToItem.ID = ToList(i).ID
                            ToItem.Status = ToList(i).Status
                            gloItems.Add(ToItem)
                            ToItem = Nothing
                            'col_SurgicalUsers.Add(C1surgicalUsers.GetData(i, Col_LoginName))
                            'col_SurgicalUId.Add(C1surgicalUsers.GetData(i, Col_UserID))
                        End If
                    End If
                Next

                'For j As Integer = 1 To col_SurgicalUsers.Count
                If ToList.Count > 0 Then
                    For j As Integer = 0 To ToList.Count - 1
                        cmbFollowUpUser.Items.Add(ToList.Item(j).Status.ToString().Trim)
                    Next
                End If
                PnlUser.Visible = False
                PnlUser.SendToBack()
            End If
            'End Code added by Mayuri:20090926
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Sub

    Private Sub grpClinic_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

#Region "Billing Settings"
    Private Sub cmbProvider_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbProvider.SelectedIndexChanged
        Try
            FillBillingSettings()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub FillBillingSettings()
        cmbTOS.SelectedIndex = -1
        cmbPOS.SelectedIndex = -1
        cmbBillingProvider.SelectedIndex = -1
        cmbRenderingProvider.SelectedIndex = -1
        cmbFacility.SelectedIndex = -1
        cmbFeeSchedule.SelectedIndex = -1
        Try
            If cmbProvider.SelectedIndex <> -1 Then
                Dim dt As New DataTable()
                dt = GetProviderSettings(Convert.ToInt64(cmbProvider.SelectedValue))

                For i As Integer = 0 To dt.Rows.Count - 1
                    Select Case Convert.ToString(dt.Rows(i)("sName")).Trim()
                        Case "TypeOfService"
                            cmbTOS.SelectedValue = Convert.ToInt64(dt.Rows(i)("sValue"))
                            Exit Select
                        Case "PlaceOfService"
                            cmbPOS.SelectedValue = Convert.ToInt64(dt.Rows(i)("sValue"))
                            Exit Select
                        Case "BillingProvider"
                            cmbBillingProvider.SelectedValue = Convert.ToInt64(dt.Rows(i)("sValue"))
                            Exit Select
                        Case "RenderingProvider"
                            cmbRenderingProvider.SelectedValue = Convert.ToInt64(dt.Rows(i)("sValue"))
                            Exit Select
                        Case "Facility"
                            'cmbFacility.SelectedValue = Convert.ToInt64(dt.Rows[i]["sValue"]); 
                            cmbFacility.SelectedValue = Convert.ToString(dt.Rows(i)("sValue"))
                            Exit Select
                        Case "Fee Schedule"
                            'cmbFacility.SelectedValue = Convert.ToInt64(dt.Rows[i]["sValue"]); 
                            cmbFeeSchedule.SelectedValue = Convert.ToString(dt.Rows(i)("sValue"))
                            Exit Select
                        Case Else
                            Exit Select
                    End Select
                Next

                If IsNothing(dt) = False Then
                    dt.Dispose()
                    dt = Nothing
                End If

            End If
        Catch ex As Exception
        Finally

        End Try
    End Sub

    Private Function GetProvider() As DataTable
        Try
            Dim odb As New gloStream.gloDataBase.gloDataBase
            Dim _sqlQuery As String = "SELECT ISNULL(nProviderID,0) AS  nProviderID , (ISNULL(sFirstName,'')+ SPACE(1) + ISNULL(sMiddleName,'') + SPACE(1) +ISNULL(sLastName,'')) AS ProviderName FROM  Provider_MST WHERE nClinicID = 1  ORDER BY ProviderName"
            Dim dt As DataTable
            odb.Connect(gstrConnectionString)
            dt = odb.ReadQueryData(_sqlQuery)
            Return dt

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Function

    Private Function GetProviderSettings(ByVal ProviderID As Int64) As DataTable
        Try

            Dim odb As New gloStream.gloDataBase.gloDataBase
            Dim _sqlQuery As String = "SELECT  sName, sValue, nProviderID FROM ProviderSettings WHERE  nProviderID = " & ProviderID & " AND nClinicID =1 "
            Dim dt As DataTable
            odb.Connect(gstrConnectionString)
            dt = odb.ReadQueryData(_sqlQuery)
            Return dt

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Function

    Private Function GetFormularyTables(ByVal FormularyDBConnectionString As String) As DataTable
        Try

            Dim odb As New gloStream.gloDataBase.gloDataBase
            Dim _sqlQuery As String = "select [name] as TableName from sys.tables where [name] like 'RxH%' order by [name] "
            Dim dt As DataTable
            odb.Connect(FormularyDBConnectionString)
            dt = odb.ReadQueryData(_sqlQuery)
            Return dt

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try
    End Function
    Private Function GetPOS() As DataTable
        Try

            Dim odb As New gloStream.gloDataBase.gloDataBase
            Dim _sqlQuery As String = "select nPOSID,sPOSCode + '-' + sPOSName AS sPOSCode,sPOSDescription from BL_POS_MST where bIsBlocked='" & False & "' AND nClinicID=" & _ClinicID & " ORDER BY sPOSCode"
            Dim dt As DataTable
            odb.Connect(gstrConnectionString)
            dt = odb.ReadQueryData(_sqlQuery)
            Return dt

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Function

    Private Function GetTOS() As DataTable
        Try

            Dim odb As New gloStream.gloDataBase.gloDataBase
            Dim _sqlQuery As String = "select nTOSID,sDescription,sTOSCode from BL_TOS_MST where bIsBlocked = '" & False & "' AND nClinicID = " & _ClinicID & " ORDER BY sDescription"
            Dim dt As DataTable
            odb.Connect(gstrConnectionString)
            dt = odb.ReadQueryData(_sqlQuery)
            Return dt

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Function

    Private Function GetFacilities() As DataTable
        Try

            Dim odb As New gloStream.gloDataBase.gloDataBase
            Dim _sqlQuery As String = ""
            _sqlQuery = "SELECT nFacilityID,sFacilityCode,sFacilityName,sNPI,sMedicadID,sBlueShieldID, " _
                    & "sMedicareID,sCity,sPhone FROM   BL_Facility_MST WHERE bIsBlocked = '" & False & "' AND nClinicID = " & _ClinicID & " "

            Dim dt As DataTable
            odb.Connect(gstrConnectionString)
            dt = odb.ReadQueryData(_sqlQuery)
            Return dt

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Function

    Private Sub FillProviders()
        Dim dt As DataTable
        Try
            dt = GetProvider()
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                cmbBillingProvider.DataSource = dt.Copy()
                cmbBillingProvider.ValueMember = dt.Columns("nProviderID").ColumnName
                cmbBillingProvider.DisplayMember = dt.Columns("ProviderName").ColumnName
                cmbBillingProvider.Refresh()
                cmbBillingProvider.SelectedIndex = -1

                cmbRenderingProvider.DataSource = dt.Copy()
                cmbRenderingProvider.ValueMember = dt.Columns("nProviderID").ColumnName
                cmbRenderingProvider.DisplayMember = dt.Columns("ProviderName").ColumnName
                cmbRenderingProvider.Refresh()
                cmbRenderingProvider.SelectedIndex = -1

                cmbProvider.DataSource = dt.Copy()
                cmbProvider.ValueMember = dt.Columns("nProviderID").ColumnName
                cmbProvider.DisplayMember = dt.Columns("ProviderName").ColumnName
                cmbProvider.Refresh()

                cmbProvider.SelectedIndex = 0
            End If
        Catch ex As Exception
        Finally
            If dt IsNot Nothing Then
                dt.Dispose()
                dt = Nothing
            End If


        End Try
    End Sub

    Private Sub FillPOS()
        Dim dt As New DataTable
        Try
            dt = GetPOS()
            If dt IsNot Nothing Then
                ' nPOSID,sPOSCode,sPOSName,sPOSDescription 
                cmbPOS.DataSource = dt.Copy()
                cmbPOS.ValueMember = dt.Columns("nPOSID").ColumnName
                cmbPOS.DisplayMember = dt.Columns("sPOSCode").ColumnName
                cmbPOS.Refresh()
                cmbPOS.SelectedIndex = -1
            End If
            dt = Nothing
        Catch ex As Exception
        Finally

            If dt IsNot Nothing Then
                dt.Dispose()
            End If
        End Try
    End Sub

    Private Sub FillTOS()
        Dim dt As DataTable
        Try
            dt = GetTOS()
            If dt IsNot Nothing Then
                cmbTOS.DataSource = dt.Copy()
                cmbTOS.ValueMember = dt.Columns("nTOSID").ColumnName
                cmbTOS.DisplayMember = dt.Columns("sDescription").ColumnName
                cmbTOS.Refresh()
                cmbTOS.SelectedIndex = -1
            End If
        Catch ex As Exception
        Finally

            If dt IsNot Nothing Then
                dt.Dispose()
                dt = Nothing
            End If
        End Try
    End Sub

    Private Sub FillFacilities()
        Dim dt As New DataTable
        Try
            dt = GetFacilities()
            If dt IsNot Nothing Then
                cmbFacility.DataSource = dt
                cmbFacility.ValueMember = dt.Columns("sFacilityCode").ColumnName
                cmbFacility.DisplayMember = dt.Columns("sFacilityName").ColumnName
                cmbFacility.Refresh()
                cmbFacility.SelectedIndex = -1

            End If
        Catch ex As Exception
        Finally

        End Try
    End Sub


    Private Sub FillFeeSchedule()
        Dim dtStdFeeSchedule As New DataTable()
        cmbFeeSchedule.DataSource = Nothing
        Try
            Dim odb As New gloStream.gloDataBase.gloDataBase

            Dim _sqlQuery As String = "SELECT DISTINCT nFeeScheduleID, sFeeScheduleName FROM BL_FeeSchedule_MST "
            odb.Connect(gstrConnectionString)
            dtStdFeeSchedule = odb.ReadQueryData(_sqlQuery)
            If dtStdFeeSchedule IsNot Nothing AndAlso dtStdFeeSchedule.Rows.Count > 0 Then
                Dim dr As DataRow = dtStdFeeSchedule.NewRow()
                dr(0) = "0"
                dtStdFeeSchedule.Rows.InsertAt(dr, 0)
                dtStdFeeSchedule.AcceptChanges()
                cmbFeeSchedule.DataSource = dtStdFeeSchedule
                cmbFeeSchedule.DisplayMember = dtStdFeeSchedule.Columns("sFeeScheduleName").ColumnName
                cmbFeeSchedule.ValueMember = dtStdFeeSchedule.Columns("nFeeScheduleID").ColumnName

            End If
        Catch dbEx As gloDatabaseLayer.DBException

        Catch ex As Exception

        Finally


        End Try
    End Sub

#End Region
#Region "other billing settings"

    Public Sub FillSpecialities()

        Dim dtSpeciality As New DataTable()
        cmbSpeciality.DataSource = Nothing
        Try

            Dim odb As New gloStream.gloDataBase.gloDataBase
            Dim _sqlQuery As String = "SELECT sCode,(sCode + ' - ' + sDescription) AS SpecCodeName FROM Specialty_MST where " & " sCode IS NOT NULL AND sDescription IS NOT NULL AND bIsBlocked = 0 AND nClinicID = " & _ClinicID & ""
            odb.Connect(gstrConnectionString)
            dtSpeciality = odb.ReadQueryData(_sqlQuery)

            Dim dr As DataRow
            dr = dtSpeciality.NewRow()
            dr(0) = 0
            dr(1) = " "
            dtSpeciality.Rows.InsertAt(dr, 0)

            Dim dr1 As DataRow = dtSpeciality.NewRow()
            dr1("SpecCodeName") = "0 - All"
            dr1("sCode") = "0"
            dtSpeciality.Rows.InsertAt(dr1, 1)
            dtSpeciality.AcceptChanges()

            If dtSpeciality IsNot Nothing AndAlso dtSpeciality.Rows.Count > 0 Then
                cmbSpeciality.DataSource = dtSpeciality
                cmbSpeciality.DisplayMember = dtSpeciality.Columns("SpecCodeName").ColumnName
                cmbSpeciality.ValueMember = dtSpeciality.Columns("sCode").ColumnName
            End If
            cmbSpeciality.SelectedIndex = -1
        Catch dbEx As gloDatabaseLayer.DBException
        Catch ex As Exception
        Finally


        End Try
    End Sub

    Private Sub FillFeeSchedules()
        Dim dtStdFeeSchedule As New DataTable()
        cmb_Feeschedules.DataSource = Nothing
        Try
            Dim odb As New gloStream.gloDataBase.gloDataBase

            Dim _sqlQuery As String = "SELECT DISTINCT nFeeScheduleID, sFeeScheduleName FROM BL_FeeSchedule_MST "
            odb.Connect(gstrConnectionString)
            dtStdFeeSchedule = odb.ReadQueryData(_sqlQuery)
            If dtStdFeeSchedule IsNot Nothing AndAlso dtStdFeeSchedule.Rows.Count > 0 Then
                Dim dr As DataRow = dtStdFeeSchedule.NewRow()
                dr(0) = "0"
                dtStdFeeSchedule.Rows.InsertAt(dr, 0)
                dtStdFeeSchedule.AcceptChanges()
                cmb_Feeschedules.DataSource = dtStdFeeSchedule
                cmb_Feeschedules.DisplayMember = dtStdFeeSchedule.Columns("sFeeScheduleName").ColumnName
                cmb_Feeschedules.ValueMember = dtStdFeeSchedule.Columns("nFeeScheduleID").ColumnName

            End If
        Catch dbEx As gloDatabaseLayer.DBException

        Catch ex As Exception

        Finally


        End Try
    End Sub

    'Private Sub SaveOtherBillingSetting()

    '    Dim objSettings As New clsSettings
    '    If cmbSpeciality.SelectedIndex > 0 Then
    '        objSettings.DefaultFeeSpeciality = cmbSpeciality.SelectedValue
    '    Else
    '        objSettings.DefaultFeeSpeciality = 0

    '    End If
    '    If txtCarrierNumber.Text <> "" Then
    '        objSettings.DefaultCarrierNumber = Convert.ToString(txtCarrierNumber.Text)
    '    Else
    '        objSettings.DefaultCarrierNumber = Convert.ToString(txtCarrierNumber.Text)

    '    End If

    '    If txtCarrierNumber.Text <> "" Then
    '        objSettings.DefaultLocality = Convert.ToString(txtLocality.Text)
    '    Else
    '        objSettings.DefaultLocality = 0
    '    End If

    '    If cmb_Feeschedules.SelectedIndex <> -1 Then
    '        objSettings.ClinicFeeSchedule = Convert.ToString(cmb_Feeschedules.SelectedValue)
    '    Else
    '        objSettings.ClinicFeeSchedule = 0

    '    End If
    '    objSettings.NoOfClaimPerBatch = numUpDn_NoOfClaims.Value.ToString()
    '    objSettings.NoOfModifiers = numModifiers.Value.ToString()

    '    objSettings.ShowLabCol = chbox_AddShowLabCol.Checked.ToString()

    '    Dim _defaultFeeSchedule As Integer = 0

    '    If cmbFacilityType.Text = FacilityType.Facility.ToString() Then
    '        _defaultFeeSchedule = FacilityType.Facility.GetHashCode()
    '    ElseIf cmbFacilityType.Text = FacilityType.NonFacility.ToString() Then
    '        _defaultFeeSchedule = FacilityType.NonFacility.GetHashCode()
    '    End If
    '    objSettings.Defaultfeecharges = _defaultFeeSchedule.ToString()

    'End Sub



    'Save AlphaII Database Settings 
    Private Sub SaveAlphaIIDatabaseSettings(ByRef dtsettings As DataTable )
        Dim _value As Boolean = False
        Try
            'Dim ogloSettings As New clsSettings
            '#Region "Billing Claim Validation Setting"  200091106
            ''Sandip Darade AlhaaII settings will be saved
            '' If rbCV_Alpha2.Checked = True Then
            '   _value = ogloSettings.Add("EMR AlphaII SQL Server Name", txtAlphaIIServerName.Text.Trim(), _ClinicID, 0, SettingFlag.Clinic)
            dtsettings.Rows.Add("EMR AlphaII SQL Server Name", txtAlphaIIServerName.Text.Trim(), "EMR AlphaII SQL Server Name")
            '  _value = ogloSettings.Add("EMR AlphaII Database Name", txtAlphaIIDatabase.Text.Trim(), _ClinicID, 0, SettingFlag.Clinic)
            dtsettings.Rows.Add("EMR AlphaII Database Name", txtAlphaIIDatabase.Text.Trim(), "EMR AlphaII Database Name")

            ' _value = ogloSettings.Add("EMR AlphaII Authentication", cmbAlphaIIAuthentication.Text.Trim(), _ClinicID, 0, SettingFlag.Clinic)
            dtsettings.Rows.Add("EMR AlphaII Authentication", cmbAlphaIIAuthentication.Text.Trim(), "EMR AlphaII Authentication")

            If cmbAlphaIIAuthentication.Text.Trim() = "SQL" Then ''cmbAuthentication
                '  _value = ogloSettings.Add("EMR AlphaII User Name", txtAlphaIIUserName.Text.Trim(), _ClinicID, 0, SettingFlag.Clinic)
                dtsettings.Rows.Add("EMR AlphaII User Name", txtAlphaIIUserName.Text.Trim(), "EMR AlphaII User Name")

                '_value = ogloSettings.Add("EMR AlphaII Password", txtAlphaIIPassword.Text.Trim(), _ClinicID, 0, SettingFlag.Clinic)
                dtsettings.Rows.Add("EMR AlphaII Password", txtAlphaIIPassword.Text.Trim(), "EMR AlphaII Password")

            ElseIf cmbAlphaIIAuthentication.Text.Trim() = "Windows" Then ''cmbAuthentication
                ' _value = ogloSettings.Add("EMR AlphaII User Name", "", _ClinicID, 0, SettingFlag.Clinic)
                dtsettings.Rows.Add("EMR AlphaII User Name", "", "EMR AlphaII User Name")
                dtsettings.Rows.Add("EMR AlphaII Password", "", "EMR AlphaII Password")

                '_value = ogloSettings.Add("EMR AlphaII Password", "", _ClinicID, 0, SettingFlag.Clinic)
            End If





            ' _value = ogloSettings.Add("ClaimValidationSetting", "Alpha2", _ClinicID, 0, SettingFlag.Clinic)
            'ElseIf rbCV_YOST.Checked = True Then
            '    _value = ogloSettings.Add("AlphaII SQL Server Name", "", _ClinicID, 0, SettingFlag.Clinic)
            '    _value = ogloSettings.Add("AlphaII Database Name", "", _ClinicID, 0, SettingFlag.Clinic)
            '    _value = ogloSettings.Add("AlphaII Authentication", "", _ClinicID, 0, SettingFlag.Clinic)
            '    _value = ogloSettings.Add("AlphaII User Name", "", _ClinicID, 0, SettingFlag.Clinic)
            '    _value = ogloSettings.Add("AlphaII Password", "", _ClinicID, 0, SettingFlag.Clinic)
            '    _value = ogloSettings.Add("ClaimValidationSetting", "YOST", _ClinicID, 0, SettingFlag.Clinic)
            'ElseIf rdbNone.Checked = True Then
            '    _value = ogloSettings.Add("AlphaII SQL Server Name", "", _ClinicID, 0, SettingFlag.Clinic)
            '    _value = ogloSettings.Add("AlphaII Database Name", "", _ClinicID, 0, SettingFlag.Clinic)
            '    _value = ogloSettings.Add("AlphaII Authentication", "", _ClinicID, 0, SettingFlag.Clinic)
            '    _value = ogloSettings.Add("AlphaII User Name", "", _ClinicID, 0, SettingFlag.Clinic)
            '    _value = ogloSettings.Add("AlphaII Password", "", _ClinicID, 0, SettingFlag.Clinic)
            '    _value = ogloSettings.Add("ClaimValidationSetting", "None", _ClinicID, 0, SettingFlag.Clinic)
            'End If
            '#End Region 

            'Save Invalid ICD9 Setting 
            If chkInvalidICD9.Checked = True Then
                ' _value = ogloSettings.Add("IsCheckInvalidICD9", "True", _ClinicID, 0, SettingFlag.Clinic)
                dtsettings.Rows.Add("IsCheckInvalidICD9", "True", "CheckInvalidICD9")
            Else
                '_value = ogloSettings.Add("IsCheckInvalidICD9", "False", _ClinicID, 0, SettingFlag.Clinic)
                dtsettings.Rows.Add("IsCheckInvalidICD9", "False", "CheckInvalidICD9")
            End If
            'Save scrubber Setting 
            If chkScrubber.Checked = True Then
                '   _value = ogloSettings.Add("IsUseScrubber", "True", _ClinicID, 0, SettingFlag.Clinic)
                dtsettings.Rows.Add("IsUseScrubber", "True", "IsUseScrubber")

            Else
                ' _value = ogloSettings.Add("IsUseScrubber", "False", _ClinicID, 0, SettingFlag.Clinic)
                dtsettings.Rows.Add("IsUseScrubber", "False", "IsUseScrubber")

            End If
            If chkReferralCPT.Checked = True Then
                '   _value = ogloSettings.Add("IsReferralCPT", "True", _ClinicID, 0, SettingFlag.Clinic)
                dtsettings.Rows.Add("IsReferralCPT", "True", "IsReferralCPT")
            Else
                '   _value = ogloSettings.Add("IsReferralCPT", "False", _ClinicID, 0, SettingFlag.Clinic)
                dtsettings.Rows.Add("IsReferralCPT", "False", "IsReferralCPT")
            End If
            If chkShowMessage.Checked = True Then
                '   _value = ogloSettings.Add("ShowMessageIfNoValidation", "True", _ClinicID, 0, SettingFlag.Clinic)
                dtsettings.Rows.Add("ShowMessageIfNoValidation", "True", "ShowMessageIfNoValidation")

            Else
                '_value = ogloSettings.Add("ShowMessageIfNoValidation", "False", _ClinicID, 0, SettingFlag.Clinic)
                dtsettings.Rows.Add("ShowMessageIfNoValidation", "False", "ShowMessageIfNoValidation")

            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub

    Private Sub RetrieveAlphaIIDatabaseSettings()
        Try
            ' gloEMR Database Settings 
            Dim ogloSettings As New clsSettings
            Dim value As New Object()
            ogloSettings.Get_gloEMRSetting("EMR AlphaII SQL Server Name", value)
            If value IsNot Nothing Then
                txtAlphaIIServerName.Text = Convert.ToString(value)
                value = Nothing
            End If

            ogloSettings.Get_gloEMRSetting("EMR AlphaII Database Name", value)
            If value IsNot Nothing Then
                txtAlphaIIDatabase.Text = Convert.ToString(value)
                value = Nothing
            End If
            ogloSettings.Get_gloEMRSetting("EMR AlphaII Authentication", value)
            If value IsNot Nothing Then
                If Convert.ToString(value) = "SQL" Then
                    cmbAlphaIIAuthentication.SelectedIndex = 1
                    value = Nothing
                    ogloSettings.Get_gloEMRSetting("EMR AlphaII User Name", value)
                    If value IsNot Nothing Then
                        txtAlphaIIUserName.Text = Convert.ToString(value)
                        value = Nothing
                    End If
                    ogloSettings.Get_gloEMRSetting("EMR AlphaII Password", value)
                    If value IsNot Nothing Then
                        txtAlphaIIPassword.Text = Convert.ToString(value)
                        value = Nothing
                    End If
                Else
                    txtAlphaIIUserName.Text = ""
                    txtAlphaIIPassword.Text = ""
                End If
            End If

            ogloSettings.Get_gloEMRSetting("ClaimValidationSetting", value)

            If value IsNot Nothing AndAlso Convert.ToString(value) <> "" Then
                If Convert.ToString(value) = "Alpha2" Then
                    rbCV_Alpha2.Checked = True
                ElseIf Convert.ToString(value) = "YOST" Then
                    rbCV_YOST.Checked = True
                ElseIf Convert.ToString(value) = "None" Then
                    rdbNone.Checked = True
                End If
                value = Nothing
            End If

            ogloSettings.Get_gloEMRSetting("IsCheckInvalidICD9", value)
            If value IsNot Nothing AndAlso Convert.ToString(value) <> "" Then
                chkInvalidICD9.Checked = Convert.ToBoolean(value)
                value = Nothing
            End If
            ogloSettings.Get_gloEMRSetting("IsUseScrubber", value)
            If value IsNot Nothing AndAlso Convert.ToString(value) <> "" Then
                chkScrubber.Checked = Convert.ToBoolean(value)
                value = Nothing
            End If

            ogloSettings.Get_gloEMRSetting("IsReferralCPT", value)
            If value IsNot Nothing AndAlso Convert.ToString(value) <> "" Then
                chkReferralCPT.Checked = Convert.ToBoolean(value)
                value = Nothing
            End If

            ogloSettings.Get_gloEMRSetting("ShowMessageIfNoValidation", value)
            If value IsNot Nothing AndAlso Convert.ToString(value) <> "" Then
                chkShowMessage.Checked = Convert.ToBoolean(value)
                value = Nothing
            End If
            ogloSettings = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub

    Private Function ValidateAlphaIISettings() As Boolean
        Dim con As New SqlConnection()
        If cmbAlphaIIAuthentication.Text = "Windows" AndAlso txtAlphaIIServerName.Text = "" AndAlso txtAlphaIIDatabase.Text = "" Then
            Return True
        End If
        If cmbAlphaIIAuthentication.Text = "SQL" AndAlso txtAlphaIIServerName.Text = "" AndAlso txtAlphaIIDatabase.Text = "" AndAlso txtAlphaIIPassword.Text = "" AndAlso txtAlphaIIUserName.Text = "" Then
            Return True
        Else
            If rbCV_Alpha2.Checked = True Then
                con = CreateAlphaIISqlConection()
                If con Is Nothing Then
                    MessageBox.Show("Connection can not be established with given parameters for Alpha II database, please verify parameters. ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    tb_Settings.SelectedTab = tbpg_AlphaIISettings
                    Return False
                End If
            End If
        End If

        If con.State = ConnectionState.Open Then
            con.Close()
        End If

        If IsNothing(con) = False Then
            con.Dispose()
            con = Nothing
        End If

        Return True
    End Function

    Private Function CreateAlphaIISqlConection() As SqlConnection
        Dim con As New SqlConnection()
        Dim _connstring As String = ""
        Try
            If txtAlphaIIServerName.Text <> "" AndAlso txtAlphaIIDatabase.Text <> "" Then
                If cmbAlphaIIAuthentication.Text = "SQL" Then
                    'SQL authentication 
                    'if (txt_EMRDB_Password.Text != "" && txt_EMRDB_UserName.Text != "") 
                    If True Then
                        _connstring = ((("Server=" & txtAlphaIIServerName.Text.ToString() & ";Database=") + txtAlphaIIDatabase.Text.ToString() & ";Uid=") + txtAlphaIIUserName.Text.ToString() & ";Pwd=") + txtAlphaIIPassword.Text.ToString() & ";"
                    End If
                Else
                    'windows authentication 
                    _connstring = ("Server=" & txtAlphaIIServerName.Text.ToString() & ";Database=") + txtAlphaIIDatabase.Text.ToString() & ";Trusted_Connection=yes;"
                End If
                con.ConnectionString = _connstring
                con.Open()
            Else
                con = Nothing
            End If
        Catch ex As Exception
            'MessageBox.Show(" Invalid credentials, Please try again.", _MessageboxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information); 
            con = Nothing
        Finally
            If con IsNot Nothing Then
                If con.State = ConnectionState.Open Then
                    con.Close()
                End If
            End If

            If IsNothing(con) = False Then
                con.Dispose()
                con = Nothing
            End If

        End Try
        Return con
    End Function

#End Region

   

   

    Private Sub cmbAlphaIIAuthentication_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbAlphaIIAuthentication.SelectedIndexChanged
        If (cmbAlphaIIAuthentication.Text.Trim() = "Windows") Then

            txtAlphaIIUserName.Enabled = False
            txtAlphaIIPassword.Enabled = False

        Else

            txtAlphaIIUserName.Enabled = True
            txtAlphaIIPassword.Enabled = True
        End If
    End Sub

    Private Sub Save_BillingSettings()
        Dim ogloSettings As New clsSettings
        Dim ProviderID As Int64 = 0
        Try
            If cmbProvider.SelectedIndex = -1 Then

            Else
                ProviderID = Convert.ToInt64(cmbProvider.SelectedValue)

                If cmbTOS.SelectedIndex <> -1 Then
                    ogloSettings.AddSettings(ProviderID, "TypeOfService", Convert.ToString(cmbTOS.SelectedValue), gnLoginID, _ClinicID, 0, "")
                Else
                    ogloSettings.AddSettings(ProviderID, "TypeOfService", "0", gnLoginID, _ClinicID, 0, "")
                End If

                If cmbPOS.SelectedIndex <> -1 Then
                    ogloSettings.AddSettings(ProviderID, "PlaceOfService", Convert.ToString(cmbPOS.SelectedValue), gnLoginID, _ClinicID, 0, "")
                Else
                    ogloSettings.AddSettings(ProviderID, "PlaceOfService", "0", gnLoginID, _ClinicID, 0, "")
                End If

                If cmbBillingProvider.SelectedIndex <> -1 Then
                    ogloSettings.AddSettings(ProviderID, "BillingProvider", Convert.ToString(cmbBillingProvider.SelectedValue), gnLoginID, _ClinicID, 0, "")
                Else
                    ogloSettings.AddSettings(ProviderID, "BillingProvider", "0", gnLoginID, _ClinicID, 0, "")
                End If

                If cmbRenderingProvider.SelectedIndex <> -1 Then
                    ogloSettings.AddSettings(ProviderID, "RenderingProvider", Convert.ToString(cmbRenderingProvider.SelectedValue), gnLoginID, _ClinicID, 0, "")
                Else
                    ogloSettings.AddSettings(ProviderID, "RenderingProvider", "0", gnLoginID, _ClinicID, 0, "")
                End If

                If cmbFacility.SelectedIndex <> -1 Then
                    ogloSettings.AddSettings(ProviderID, "Facility", Convert.ToString(cmbFacility.SelectedValue), gnLoginID, _ClinicID, 0, "")
                Else
                    ogloSettings.AddSettings(ProviderID, "Facility", "0", gnLoginID, _ClinicID, 0, "")
                End If

                If cmbFeeSchedule.SelectedIndex <> -1 Then
                    ogloSettings.AddSettings(ProviderID, "Fee Schedule", Convert.ToString(cmbFeeSchedule.SelectedValue), gnLoginID, _ClinicID, 0, "")
                Else
                    ogloSettings.AddSettings(ProviderID, "Fee Schedule", "0", gnLoginID, _ClinicID, 0, "")
                End If

                If cmb_Feeschedules.SelectedIndex <> -1 Then
                    ogloSettings.AddSettings(ProviderID, "ClinicFeeSchedule", Convert.ToString(cmb_Feeschedules.SelectedValue), gnLoginID, _ClinicID, 0, "")
                Else
                    ogloSettings.AddSettings(ProviderID, "ClinicFeeSchedule", "0", gnLoginID, _ClinicID, 0, "")
                End If

                ''Code Added by Shirish 20100614
                'If rbProviderLabUsageAsk.Checked = True Then
                '    ogloSettings.AddSettings(ProviderID, "glolab provider usage", rbProviderLabUsageAsk.Tag.ToUpper(), gnLoginID, _ClinicID, 0, "")
                'ElseIf rbProviderLabUsageLabOrder.Checked = True Then
                '    ogloSettings.AddSettings(ProviderID, "glolab provider usage", rbProviderLabUsageLabOrder.Tag.ToUpper(), gnLoginID, _ClinicID, 0, "")
                'ElseIf rbProviderLabUsageRecordResults.Checked = True Then
                '    ogloSettings.AddSettings(ProviderID, "glolab provider usage", rbProviderLabUsageRecordResults.Tag.ToUpper(), gnLoginID, _ClinicID, 0, "")
                'ElseIf rbProviderLabUsageTask.Checked = True Then
                '    ogloSettings.AddSettings(ProviderID, "glolab provider usage", rbProviderLabUsageTask.Tag.ToUpper(), gnLoginID, _ClinicID, 0, "")
                'End If
                ''End of the Code Shirish20100614
            End If
        Catch ex As Exception
        Finally

        End Try
    End Sub

#Region "Marital Status"

    Private Sub FillMaritalStatusFromPatientRegistration()
        cmbPRMaritalStatus1.Items.Add("UnMarried")
        cmbPRMaritalStatus1.Items.Add("Married")
        cmbPRMaritalStatus1.Items.Add("Single")
        cmbPRMaritalStatus1.Items.Add("Widowed")
        cmbPRMaritalStatus1.Items.Add("Divorced")
        cmbPRMaritalStatus1.Refresh()
        cmbPRMaritalStatus1.SelectedIndex = -1

        cmbPRMaritalStatus2.Items.Add("UnMarried")
        cmbPRMaritalStatus2.Items.Add("Married")
        cmbPRMaritalStatus2.Items.Add("Single")
        cmbPRMaritalStatus2.Items.Add("Widowed")
        cmbPRMaritalStatus2.Items.Add("Divorced")
        cmbPRMaritalStatus2.Refresh()
        cmbPRMaritalStatus2.SelectedIndex = -1

        cmbPRMaritalStatus3.Items.Add("UnMarried")
        cmbPRMaritalStatus3.Items.Add("Married")
        cmbPRMaritalStatus3.Items.Add("Single")
        cmbPRMaritalStatus3.Items.Add("Widowed")
        cmbPRMaritalStatus3.Items.Add("Divorced")
        cmbPRMaritalStatus3.Refresh()
        cmbPRMaritalStatus3.SelectedIndex = -1

        cmbPRMaritalStatus4.Items.Add("UnMarried")
        cmbPRMaritalStatus4.Items.Add("Married")
        cmbPRMaritalStatus4.Items.Add("Single")
        cmbPRMaritalStatus4.Items.Add("Widowed")
        cmbPRMaritalStatus4.Items.Add("Divorced")
        cmbPRMaritalStatus4.Refresh()
        cmbPRMaritalStatus4.SelectedIndex = -1

        cmbPRMaritalStatus5.Items.Add("UnMarried")
        cmbPRMaritalStatus5.Items.Add("Married")
        cmbPRMaritalStatus5.Items.Add("Single")
        cmbPRMaritalStatus5.Items.Add("Widowed")
        cmbPRMaritalStatus5.Items.Add("Divorced")
        cmbPRMaritalStatus5.Refresh()
        cmbPRMaritalStatus5.SelectedIndex = -1
    End Sub

    Private Sub FillMaritalStatusFromBilling()
        cmbBillingMaritalStatus1.Items.Add("Single")
        cmbBillingMaritalStatus1.Items.Add("Married")
        cmbBillingMaritalStatus1.Items.Add("Other")
        cmbBillingMaritalStatus1.Refresh()
        cmbBillingMaritalStatus1.SelectedIndex = -1

        cmbBillingMaritalStatus2.Items.Add("Single")
        cmbBillingMaritalStatus2.Items.Add("Married")
        cmbBillingMaritalStatus2.Items.Add("Other")
        cmbBillingMaritalStatus2.Refresh()
        cmbBillingMaritalStatus2.SelectedIndex = -1

        cmbBillingMaritalStatus3.Items.Add("Single")
        cmbBillingMaritalStatus3.Items.Add("Married")
        cmbBillingMaritalStatus3.Items.Add("Other")
        cmbBillingMaritalStatus3.Refresh()
        cmbBillingMaritalStatus3.SelectedIndex = -1


        cmbBillingMaritalStatus4.Items.Add("Single")
        cmbBillingMaritalStatus4.Items.Add("Married")
        cmbBillingMaritalStatus4.Items.Add("Other")
        cmbBillingMaritalStatus4.Refresh()
        cmbBillingMaritalStatus4.SelectedIndex = -1

        cmbBillingMaritalStatus5.Items.Add("Single")
        cmbBillingMaritalStatus5.Items.Add("Married")
        cmbBillingMaritalStatus5.Items.Add("Other")
        cmbBillingMaritalStatus5.Refresh()

        cmbBillingMaritalStatus5.SelectedIndex = -1
    End Sub

#Region " Marital Status Comboboxes Events "

    Private Sub cmbPRMaritalStatus1_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPRMaritalStatus1.SelectedIndexChanged
        If cmbPRMaritalStatus1.Text = cmbPRMaritalStatus2.Text Then
            cmbPRMaritalStatus2.SelectedIndex = -1
        End If
        If cmbPRMaritalStatus1.Text = cmbPRMaritalStatus3.Text Then
            cmbPRMaritalStatus3.SelectedIndex = -1
        End If
        If cmbPRMaritalStatus1.Text = cmbPRMaritalStatus4.Text Then
            cmbPRMaritalStatus4.SelectedIndex = -1
        End If
        If cmbPRMaritalStatus1.Text = cmbPRMaritalStatus5.Text Then
            cmbPRMaritalStatus5.SelectedIndex = -1
        End If
    End Sub

    Private Sub cmbPRMaritalStatus2_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPRMaritalStatus2.SelectedIndexChanged
        If cmbPRMaritalStatus2.Text = cmbPRMaritalStatus1.Text Then
            cmbPRMaritalStatus1.SelectedIndex = -1
        End If
        If cmbPRMaritalStatus2.Text = cmbPRMaritalStatus3.Text Then
            cmbPRMaritalStatus3.SelectedIndex = -1
        End If
        If cmbPRMaritalStatus2.Text = cmbPRMaritalStatus4.Text Then
            cmbPRMaritalStatus4.SelectedIndex = -1
        End If
        If cmbPRMaritalStatus2.Text = cmbPRMaritalStatus5.Text Then
            cmbPRMaritalStatus5.SelectedIndex = -1
        End If
    End Sub

    Private Sub cmbPRMaritalStatus3_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPRMaritalStatus3.SelectedIndexChanged
        If cmbPRMaritalStatus3.Text = cmbPRMaritalStatus1.Text Then
            cmbPRMaritalStatus1.SelectedIndex = -1
        End If
        If cmbPRMaritalStatus3.Text = cmbPRMaritalStatus2.Text Then
            cmbPRMaritalStatus2.SelectedIndex = -1
        End If
        If cmbPRMaritalStatus3.Text = cmbPRMaritalStatus4.Text Then
            cmbPRMaritalStatus4.SelectedIndex = -1
        End If
        If cmbPRMaritalStatus3.Text = cmbPRMaritalStatus5.Text Then
            cmbPRMaritalStatus5.SelectedIndex = -1
        End If
    End Sub

    Private Sub cmbPRMaritalStatus4_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPRMaritalStatus4.SelectedIndexChanged
        If cmbPRMaritalStatus4.Text = cmbPRMaritalStatus1.Text Then
            cmbPRMaritalStatus1.SelectedIndex = -1
        End If
        If cmbPRMaritalStatus4.Text = cmbPRMaritalStatus2.Text Then
            cmbPRMaritalStatus2.SelectedIndex = -1
        End If
        If cmbPRMaritalStatus4.Text = cmbPRMaritalStatus3.Text Then
            cmbPRMaritalStatus3.SelectedIndex = -1
        End If
        If cmbPRMaritalStatus4.Text = cmbPRMaritalStatus5.Text Then
            cmbPRMaritalStatus5.SelectedIndex = -1
        End If
    End Sub

    Private Sub cmbPRMaritalStatus5_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPRMaritalStatus5.SelectedIndexChanged
        If cmbPRMaritalStatus5.Text = cmbPRMaritalStatus1.Text Then
            cmbPRMaritalStatus1.SelectedIndex = -1
        End If
        If cmbPRMaritalStatus5.Text = cmbPRMaritalStatus2.Text Then
            cmbPRMaritalStatus2.SelectedIndex = -1
        End If
        If cmbPRMaritalStatus5.Text = cmbPRMaritalStatus3.Text Then
            cmbPRMaritalStatus3.SelectedIndex = -1
        End If
        If cmbPRMaritalStatus5.Text = cmbPRMaritalStatus4.Text Then
            cmbPRMaritalStatus4.SelectedIndex = -1
        End If
    End Sub

#End Region

    Private Sub SaveMaritalStatusSettings(ByRef dtsettings As DataTable)
        '  Dim cls As New clsSettings
        Try
            If cmbPRMaritalStatus1.SelectedIndex <> -1 Then
                'cls.Add(Convert.ToString(cmbPRMaritalStatus1.Text), Convert.ToString(cmbBillingMaritalStatus1.Text), _ClinicID, gnLoginID, SettingFlag.Clinic)
                dtsettings.Rows.Add(Convert.ToString(cmbPRMaritalStatus1.Text), Convert.ToString(cmbBillingMaritalStatus1.Text), Convert.ToString(cmbPRMaritalStatus1.Text))
            Else
                'cls.Add(Convert.ToString(cmbPRMaritalStatus1.Text), "", _ClinicID, gnLoginID, SettingFlag.Clinic)
                dtsettings.Rows.Add(Convert.ToString(cmbPRMaritalStatus1.Text), "", Convert.ToString(cmbPRMaritalStatus1.Text))

            End If

            If cmbPRMaritalStatus2.SelectedIndex <> -1 Then
                '   cls.Add(Convert.ToString(cmbPRMaritalStatus2.Text), Convert.ToString(cmbBillingMaritalStatus2.Text), _ClinicID, gnLoginID, SettingFlag.Clinic)
                dtsettings.Rows.Add(Convert.ToString(cmbPRMaritalStatus2.Text), Convert.ToString(cmbBillingMaritalStatus2.Text), Convert.ToString(cmbPRMaritalStatus2.Text))

            Else
                '  cls.Add(Convert.ToString(cmbPRMaritalStatus2.Text), "", _ClinicID, gnLoginID, SettingFlag.Clinic)
                dtsettings.Rows.Add(Convert.ToString(cmbPRMaritalStatus2.Text), "", Convert.ToString(cmbPRMaritalStatus2.Text))

            End If

            If cmbPRMaritalStatus3.SelectedIndex <> -1 Then
                '   cls.Add(Convert.ToString(cmbPRMaritalStatus3.Text), Convert.ToString(cmbBillingMaritalStatus3.Text), _ClinicID, gnLoginID, SettingFlag.Clinic)
                dtsettings.Rows.Add(Convert.ToString(cmbPRMaritalStatus3.Text), Convert.ToString(cmbBillingMaritalStatus3.Text), Convert.ToString(cmbPRMaritalStatus3.Text))

            Else
                'cls.Add(Convert.ToString(cmbPRMaritalStatus3.Text), "", _ClinicID, gnLoginID, SettingFlag.Clinic)
                dtsettings.Rows.Add(Convert.ToString(cmbPRMaritalStatus3.Text), "", Convert.ToString(cmbPRMaritalStatus3.Text))

            End If

            If cmbPRMaritalStatus4.SelectedIndex <> -1 Then
                ' cls.Add(Convert.ToString(cmbPRMaritalStatus4.Text), Convert.ToString(cmbBillingMaritalStatus4.Text), _ClinicID, gnLoginID, SettingFlag.Clinic)
                dtsettings.Rows.Add(Convert.ToString(cmbPRMaritalStatus4.Text), Convert.ToString(cmbBillingMaritalStatus4.Text), Convert.ToString(cmbPRMaritalStatus4.Text))

            Else
                'cls.Add(Convert.ToString(cmbPRMaritalStatus4.Text), "", _ClinicID, gnLoginID, SettingFlag.Clinic)
                dtsettings.Rows.Add(Convert.ToString(cmbPRMaritalStatus4.Text), "", Convert.ToString(cmbPRMaritalStatus4.Text))

            End If

            If cmbPRMaritalStatus5.SelectedIndex <> -1 Then
                ' cls.Add(Convert.ToString(cmbPRMaritalStatus5.Text), Convert.ToString(cmbBillingMaritalStatus5.Text), _ClinicID, gnLoginID, SettingFlag.Clinic)
                dtsettings.Rows.Add(Convert.ToString(cmbPRMaritalStatus5.Text), Convert.ToString(cmbBillingMaritalStatus5.Text), Convert.ToString(cmbPRMaritalStatus5.Text))

            Else
                'cls.Add(Convert.ToString(cmbPRMaritalStatus5.Text), "", _ClinicID, gnLoginID, SettingFlag.Clinic)
                dtsettings.Rows.Add(Convert.ToString(cmbPRMaritalStatus5.Text), "", Convert.ToString(cmbPRMaritalStatus5.Text))

            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub GetMaritalStatusSettings()
        Dim objSetting As New clsSettings
        Dim value As New Object
        Try


            objSetting.GetSetting("UnMarried", gnLoginID, _ClinicID, value)
            If Not IsNothing(value) Then
                If (Convert.ToString(value.ToString().Trim) <> "") Then
                    cmbBillingMaritalStatus1.Text = Convert.ToString(value)
                    cmbPRMaritalStatus1.Text = "UnMarried"
                End If
            End If
            value = Nothing

            objSetting.GetSetting("Married", gnLoginID, _ClinicID, value)
            If Not IsNothing(value) Then
                If (Convert.ToString(value.ToString().Trim) <> "") Then
                    cmbBillingMaritalStatus2.Text = Convert.ToString(value)
                    cmbPRMaritalStatus2.Text = "Married"
                End If
            End If
            value = Nothing

            objSetting.GetSetting("Single", gnLoginID, _ClinicID, value)
            If Not IsNothing(value) Then
                If (Convert.ToString(value.ToString().Trim) <> "") Then
                    cmbBillingMaritalStatus3.Text = Convert.ToString(value)
                    cmbPRMaritalStatus3.Text = "Single"
                End If
            End If
            value = Nothing

            objSetting.GetSetting("Widowed", gnLoginID, _ClinicID, value)
            If Not IsNothing(value) Then
                If (Convert.ToString(value.ToString().Trim) <> "") Then
                    cmbBillingMaritalStatus4.Text = Convert.ToString(value)
                    cmbPRMaritalStatus4.Text = "Widowed"
                End If
            End If
            value = Nothing
            objSetting.GetSetting("Divorced", gnLoginID, _ClinicID, value)
            If Not IsNothing(value) Then
                If (Convert.ToString(value.ToString().Trim) <> "") Then
                    cmbBillingMaritalStatus5.Text = Convert.ToString(value)
                    cmbPRMaritalStatus5.Text = "Divorced"
                End If
            End If
            value = Nothing
        Catch ex As Exception

        End Try

    End Sub
#End Region

#Region "Exchange server Setting"

    Private Sub AddExServerSetting(ByRef dtsettings As DataTable)

        Try
            'Exchange Domain 
            'Dim cls As New clsSettings
            ' cls.Add("ExchangeDomain", txtExchangeDomain.Text.Trim(), _ClinicID, gnLoginID, SettingFlag.Clinic)
            dtsettings.Rows.Add("ExchangeDomain", txtExchangeDomain.Text.Trim(), "ExchangeDomain")
            'Exchange URL 
            '  cls.Add("ExchangeURL", txtExchangeURL.Text.Trim(), _ClinicID, gnLoginID, SettingFlag.Clinic)
            dtsettings.Rows.Add("ExchangeURL", txtExchangeURL.Text.Trim(), "ExchangeURL")

            'Exchange TimeZone 
            Dim strExchangeTimeZone As String = ""
            If cmbExchangeTimeZone.SelectedIndex <> -1 Then
                strExchangeTimeZone = cmbExchangeTimeZone.Text.Trim()
            Else
                strExchangeTimeZone = "+"
            End If

            strExchangeTimeZone += Convert.ToString(numExchangeTimeZoneHour.Value).PadLeft(2, "0") & ":"

            strExchangeTimeZone += Convert.ToString(numExchangeTimeZoneMin.Value).PadLeft(2, "0")
            ' cls.Add("ExchangeTimeZone", strExchangeTimeZone, _ClinicID, gnLoginID, SettingFlag.Clinic)
            dtsettings.Rows.Add("ExchangeTimeZone", strExchangeTimeZone, "ExchangeTimeZone")

        Catch ex As Exception

        End Try
    End Sub

    Private Sub GetExServerSetting()
        Dim objSetting As New clsSettings
        Dim value As New Object
        Try
            objSetting.GetSetting("ExchangeDomain", gnLoginID, _ClinicID, value)
            If Not IsNothing(value) Then
                If (Convert.ToString(value.ToString().Trim) <> "") Then

                    txtExchangeDomain.Text = Convert.ToString(value)
                End If
            End If
            value = Nothing

            objSetting.GetSetting("ExchangeURL", gnLoginID, _ClinicID, value)
            If Not IsNothing(value) Then
                If (Convert.ToString(value.ToString().Trim) <> "") Then

                    txtExchangeURL.Text = Convert.ToString(value)
                End If
            End If
            value = Nothing

            objSetting.GetSetting("ExchangeTimeZone", gnLoginID, _ClinicID, value)
            If Not IsNothing(value) Then
                If (Convert.ToString(value.ToString().Trim) <> "") Then
                    Dim strTimeZone As String = Convert.ToString(value)
                    'i.e strTimeZone = "+05:30" 
                    cmbExchangeTimeZone.Text = strTimeZone.Substring(0, 1)
                    numExchangeTimeZoneHour.Value = Convert.ToDecimal(strTimeZone.Substring(1, 2))
                    numExchangeTimeZoneMin.Value = Convert.ToDecimal(strTimeZone.Substring(4, 2))

                End If
            End If
            value = Nothing

        Catch ex As Exception

        End Try

    End Sub

#End Region

    Private Sub AddOtherSettings(ByRef dtsettings As DataTable)
        Try
            ''Add Week Days settings
            '  Dim objSetting As New clsSettings
            Dim _SettingValue As String = ""

            If ChkRec_Pattern_Weekly_Sunday.Checked = True Then
                _SettingValue += "," & "0"
            End If
            If ChkRec_Pattern_Weekly_Monday.Checked = True Then
                _SettingValue += "," & "1"
            End If
            If ChkRec_Pattern_Weekly_Tuesday.Checked = True Then
                _SettingValue += "," & "2"
            End If
            If ChkRec_Pattern_Weekly_Wednesday.Checked = True Then
                _SettingValue += "," & "3"
            End If
            If ChkRec_Pattern_Weekly_Thursday.Checked = True Then
                _SettingValue += "," & "4"
            End If
            If ChkRec_Pattern_Weekly_Friday.Checked = True Then
                _SettingValue += "," & "5"
            End If
            If ChkRec_Pattern_Weekly_Saturday.Checked = True Then
                _SettingValue += "," & "6"
            End If

            If _SettingValue.Trim() <> "" Then
                _SettingValue = _SettingValue.Substring(1, _SettingValue.Length - 1)
            End If
            '   objSetting.Add("Week Days", _SettingValue, _ClinicID, gnLoginID, SettingFlag.Clinic)
            dtsettings.Rows.Add("Week Days", _SettingValue, "Week Days")

            ''Add Patient Prefix setting
            ' objSetting.Add("PatientCodePrefix", txtPatientCodePrefix.Text.Trim(), _ClinicID, gnLoginID, SettingFlag.Clinic)
            dtsettings.Rows.Add("PatientCodePrefix", txtPatientCodePrefix.Text.Trim(), "Patient CodePrefix")

            ''No Of Appointments Per Slot 
            'objSetting.Add("MaxAppointmentsInSlot", numAppointmentsPerSlot.Value.ToString(), _ClinicID, gnLoginID, SettingFlag.Clinic)
            dtsettings.Rows.Add("MaxAppointmentsInSlot", numAppointmentsPerSlot.Value.ToString(), "MaxAppointmentsInSlot")


            ''Show Allowed Amount

            'objSetting.Add("ShowAllowedAmount", chbox_showAllwdAmount.Checked.ToString(), _ClinicID, 0, SettingFlag.Clinic)
            ''Sandip Darade 20091107
            If (gstrAdminFor = "gloPM") Then
                If (chbox_showAllwdAmount.Checked = True) Then
                    ' objSetting.Add("ShowAllowedAmount", "1", _ClinicID, 0, SettingFlag.Clinic)
                    dtsettings.Rows.Add("ShowAllowedAmount", "1", "ShowAllowedAmount")

                Else
                    'objSetting.Add("ShowAllowedAmount", "0", _ClinicID, 0, SettingFlag.Clinic)
                    dtsettings.Rows.Add("ShowAllowedAmount", "0", "ShowAllowedAmount")

                End If

                If (chbox_showTotalPayment.Checked = True) Then
                    '     objSetting.Add("ShowTotalPaymentColumn", "1", _ClinicID, 0, SettingFlag.Clinic)
                    dtsettings.Rows.Add("ShowTotalPaymentColumn", "1", "ShowTotalPaymentColumn")

                Else
                    '  objSetting.Add("ShowTotalPaymentColumn", "0", _ClinicID, 0, SettingFlag.Clinic)
                    dtsettings.Rows.Add("ShowTotalPaymentColumn", "0", "ShowTotalPaymentColumn")

                End If


                Dim _ReportingBasedon As String = ""
                If (Rb_AllowedAmt.Checked = True) Then
                    _ReportingBasedon = "1"
                End If
                If (RB_ChargeAmt.Checked = True) Then
                    _ReportingBasedon = "0"
                End If
                ' objSetting.Add("ReportingBasedOn", _ReportingBasedon, _ClinicID, 0, SettingFlag.Clinic)
                dtsettings.Rows.Add("ReportingBasedOn", _ReportingBasedon, "ReportingBasedOn")

            End If
            '            objSetting.Add("ServerPath", Convert.ToString(txtServerPath.Text), _ClinicID, gnLoginID, SettingFlag.Clinic)

            dtsettings.Rows.Add("ServerPath", Convert.ToString(txtServerPath.Text), "ServerPath")
            dtsettings.Rows.Add("PatientCodeIncrement", numPatientCodeIncrement.Value.ToString(), "PatientCodeIncrement")

          

        Catch ex As Exception

        End Try
    End Sub

    Private Sub GetOtherSettings()
        Dim objSetting As New clsSettings
        Dim value As New Object
        Try
            ''get Week Days setting
            If value IsNot Nothing Then
                objSetting.GetSetting("Week Days", gnLoginID, _ClinicID, value)
                If Convert.ToString(value).Trim() <> "" Then
                    Dim WeekDays As String() = Convert.ToString(value).Trim().Split(",")
                    For j As Integer = 0 To WeekDays.Length - 1
                        If WeekDays(j).Trim() = "0" Then
                            ChkRec_Pattern_Weekly_Sunday.Checked = True
                        End If
                        If WeekDays(j).Trim() = "1" Then
                            ChkRec_Pattern_Weekly_Monday.Checked = True
                        End If

                        If WeekDays(j).Trim() = "2" Then
                            ChkRec_Pattern_Weekly_Tuesday.Checked = True
                        End If

                        If WeekDays(j).Trim() = "3" Then
                            ChkRec_Pattern_Weekly_Wednesday.Checked = True
                        End If
                        If WeekDays(j).Trim() = "4" Then
                            ChkRec_Pattern_Weekly_Thursday.Checked = True
                        End If

                        If WeekDays(j).Trim() = "5" Then
                            ChkRec_Pattern_Weekly_Friday.Checked = True
                        End If

                        If WeekDays(j).Trim() = "6" Then
                            ChkRec_Pattern_Weekly_Saturday.Checked = True

                        End If
                    Next
                End If
            End If
            value = Nothing

            ''get Patient code prefix
            objSetting.GetSetting("PatientCodePrefix", gnLoginID, _ClinicID, value)
            If Not IsNothing(value) Then
                If (Convert.ToString(value.ToString().Trim) <> "") Then
                    txtPatientCodePrefix.Text = Convert.ToString(value)
                End If
            End If
            value = Nothing

            ''get Max appointment per hour
            objSetting.GetSetting("MaxAppointmentsInSlot", gnLoginID, _ClinicID, value)
            If Not IsNothing(value) Then
                If (Convert.ToString(value.ToString().Trim) <> "") Then
                    Dim _maxAppSlot As Decimal = Convert.ToDecimal(value)
                    If _maxAppSlot <= numAppointmentsPerSlot.Maximum AndAlso _maxAppSlot >= numAppointmentsPerSlot.Minimum Then
                        numAppointmentsPerSlot.Value = Convert.ToDecimal(value)
                    Else
                        numAppointmentsPerSlot.Value = numAppointmentsPerSlot.Maximum
                    End If

                End If
            End If
            value = Nothing
            'ShowAllowedAmount
            objSetting.GetSetting("ShowAllowedAmount", 0, _ClinicID, value)
            If Not IsNothing(value) Then
                If (Convert.ToString(value.ToString().Trim) <> "") Then
                    chbox_showAllwdAmount.Checked = Convert.ToBoolean(Convert.ToInt16(value))
                Else
                    chbox_showAllwdAmount.Checked = False
                End If
            Else

                chbox_showAllwdAmount.Checked = False
            End If
            value = Nothing

            'ShowTotalPaymentColumn
            objSetting.GetSetting("ShowTotalPaymentColumn", 0, _ClinicID, value)
            If Not IsNothing(value) Then
                If (Convert.ToString(value.ToString().Trim) <> "") Then
                    chbox_showTotalPayment.Checked = Convert.ToBoolean(Convert.ToInt16(value))
                Else
                    chbox_showTotalPayment.Checked = False
                End If
            Else

                chbox_showTotalPayment.Checked = False
            End If
            value = Nothing

            objSetting.GetSetting("ReportingBasedOn", 0, _ClinicID, value)
            If Not IsNothing(value) Then
                If (Convert.ToString(value.ToString().Trim) <> "") Then
                    If (Convert.ToString(value) = "0") Then
                        RB_ChargeAmt.Checked = True
                    End If
                    If (Convert.ToString(value) = "1") Then
                        Rb_AllowedAmt.Checked = True
                    End If
                End If
            End If
            value = Nothing

            ''get Server Path
            objSetting.GetSetting("ServerPath", gnLoginID, _ClinicID, value)
            If Not IsNothing(value) Then
                If (Convert.ToString(value.ToString().Trim) <> "") Then
                    txtServerPath.Text = Convert.ToString(value)
                End If
            End If
            value = Nothing

            ''get Report Server Protocol name - Amit - 11 Apr
            objSetting.GetSetting("ReportProtocol", gnLoginID, _ClinicID, value)
            If Not IsNothing(value) Then
                If (Convert.ToString(value.ToString().Trim) <> "") Then
                    If Convert.ToString(value.ToString().Trim) = "http" Then
                        Rdohttp.Checked = True
                    Else
                        Rdohttps.Checked = True
                    End If
                End If
            End If

            value = Nothing


            ''get Report Server Settings Roopali 14 July 2010
            objSetting.GetSetting("ReportServer", gnLoginID, _ClinicID, value)
            If Not IsNothing(value) Then
                If (Convert.ToString(value.ToString().Trim) <> "") Then
                    txtReportServerName.Text = Convert.ToString(value)
                End If
            End If
            value = Nothing

            objSetting.GetSetting("ReportFolder", gnLoginID, _ClinicID, value)
            If Not IsNothing(value) Then
                If (Convert.ToString(value.ToString().Trim) <> "") Then
                    txtReportFolderName.Text = Convert.ToString(value)
                End If
            End If
            value = Nothing

            ''''''Added by Ujwala for Customized SSRS Reports - as on 20101021
            objSetting.GetSetting("CustomizedReportFolder", gnLoginID, _ClinicID, value)
            If Not IsNothing(value) Then
                If (Convert.ToString(value.ToString().Trim) <> "") Then
                    txtCustomRptFolderNm.Text = Convert.ToString(value)
                End If
            End If
            value = Nothing
            ''''''Added by Ujwala for Customized SSRS Reports - as on 20101021

            objSetting.IsSettingExsits("ReportVirtualDirectory", gnLoginID, _ClinicID, value)
            If Not IsNothing(value) Then
                If (value <> 0) Then
                    objSetting.GetSetting("ReportVirtualDirectory", gnLoginID, _ClinicID, value)
                    If Not IsNothing(value) Then
                        If (Convert.ToString(value.ToString().Trim) <> "") Then
                            txtReportVirtualDir.Text = Convert.ToString(value)
                        End If
                    End If
                    value = Nothing
                Else
                    txtReportVirtualDir.Text = "ReportServer"
                End If

            End If
            value = Nothing
















            ''get PatientCodeIncrement
            objSetting.GetSetting("PatientCodeIncrement", gnLoginID, _ClinicID, value)
            If Not IsNothing(value) Then
                If (Convert.ToString(value.ToString().Trim) <> "") Then
                    Dim _Increment As Decimal = Convert.ToDecimal(value)
                    If _Increment <= numPatientCodeIncrement.Maximum AndAlso _Increment >= numPatientCodeIncrement.Minimum Then
                        numPatientCodeIncrement.Value = Convert.ToDecimal(value)
                    Else
                        numPatientCodeIncrement.Value = numPatientCodeIncrement.Maximum
                    End If

                End If
            End If
            value = Nothing

        Catch ex As Exception
        End Try
    End Sub


    Private Sub SavePaymentSetting()
        Try
            Dim obj As New clsSettings
            obj.DeletePaymentsetting(Convert.ToString(cmb_InsuranceType.SelectedValue))
            Dim _InsurancetypeDesc As String = ""
            If (Convert.ToString(cmb_InsuranceType.SelectedText) <> "") Then
                Dim _arrstring As String()
                _arrstring = cmb_InsuranceType.Text.Split("-")
                _InsurancetypeDesc = _arrstring(1)
            End If

            Dim n As TreeNode
            For Each n In trvSelectedRows.Nodes
                obj.AddPaymentSettings(Convert.ToString(cmb_InsuranceType.SelectedValue), _InsurancetypeDesc, n.Text, Convert.ToString(n.Tag))
            Next
        Catch ex As Exception

        End Try
    End Sub

    Private Sub GetPaymentSetting()
        trvSelectedRows.Nodes.Clear()
        Dim oNode As TreeNode
        Dim i As Integer
        Dim dt As New DataTable()

        Dim _InsuranceType As String = ""
        _InsuranceType = Convert.ToString(cmb_InsuranceType.SelectedValue)
        Try
            Dim odb As New gloStream.gloDataBase.gloDataBase

            Dim _sqlQuery As String = "SELECT ISNULL(sRowName,'') AS  Rowname,ISNULL(sRowIndex,'') AS RowIndex  FROM Settings_Payment WHERE  sInsTypeCode = '" & _InsuranceType & "' "
            odb.Connect(gstrConnectionString)
            dt = odb.ReadQueryData(_sqlQuery)
            If Not IsNothing(dt) And dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    oNode = New TreeNode
                    oNode.Text = Convert.ToString(dt.Rows(i)("Rowname"))
                    oNode.Tag = Convert.ToString(dt.Rows(i)("RowIndex"))
                    trvSelectedRows.Nodes.Add(oNode)
                    oNode = Nothing
                Next
            Else
                oNode = New TreeNode
                oNode.Text = "Insurance"
                oNode.Tag = 0
                trvSelectedRows.Nodes.Add(oNode)
                oNode = Nothing

                oNode = New TreeNode
                oNode.Text = "Patient"
                oNode.Tag = 1
                trvSelectedRows.Nodes.Add(oNode)
                oNode = Nothing

                oNode = New TreeNode
                oNode.Text = "Copay"
                oNode.Tag = 2
                trvSelectedRows.Nodes.Add(oNode)
                oNode = Nothing

                oNode = New TreeNode
                oNode.Text = "Deductable"
                oNode.Tag = 3
                trvSelectedRows.Nodes.Add(oNode)
                oNode = Nothing

                oNode = New TreeNode
                oNode.Text = "Adjustment"
                oNode.Tag = 4
                trvSelectedRows.Nodes.Add(oNode)
                oNode = Nothing

                oNode = New TreeNode
                oNode.Text = "Coinsurance"
                oNode.Tag = 5
                trvSelectedRows.Nodes.Add(oNode)
                oNode = Nothing

                oNode = New TreeNode
                oNode.Text = "Refund"
                oNode.Tag = 6
                trvSelectedRows.Nodes.Add(oNode)
                oNode = Nothing

                oNode = New TreeNode
                oNode.Text = "Withhold"
                oNode.Tag = 7
                trvSelectedRows.Nodes.Add(oNode)
                oNode = Nothing
            End If
        Catch ex As Exception

        End Try


    End Sub

    Private Sub btnUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUp.Click
        trvSelectedRows.BeginUpdate()
        Try
            Dim oNode As TreeNode
            Dim prevIndex As Integer
            If IsNothing(trvSelectedRows.SelectedNode) = False Then
                oNode = trvSelectedRows.SelectedNode.Clone
                If IsNothing(oNode) = False Then
                    If trvSelectedRows.SelectedNode.Index <> 0 Then
                        prevIndex = trvSelectedRows.SelectedNode.PrevNode.Index
                        trvSelectedRows.Nodes.Remove(trvSelectedRows.SelectedNode)
                        trvSelectedRows.Nodes.Insert(prevIndex, oNode)
                        trvSelectedRows.SelectedNode = oNode
                    End If
                End If
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        trvSelectedRows.EndUpdate()
        If trvSelectedRows.SelectedNode IsNot Nothing Then
            trvSelectedRows.SelectedNode.EnsureVisible()
        End If
    End Sub

    Private Sub btnDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDown.Click
        trvSelectedRows.BeginUpdate()
        Try
            Dim oNode As TreeNode
            Dim nextIndex As Integer
            If IsNothing(trvSelectedRows.SelectedNode) = False Then
                oNode = trvSelectedRows.SelectedNode.Clone
                If IsNothing(oNode) = False Then
                    If trvSelectedRows.SelectedNode.Index <> trvSelectedRows.Nodes.Count - 1 Then
                        nextIndex = trvSelectedRows.SelectedNode.NextNode.Index
                        trvSelectedRows.Nodes.Remove(trvSelectedRows.SelectedNode)
                        trvSelectedRows.Nodes.Insert(nextIndex, oNode)
                        trvSelectedRows.SelectedNode = oNode
                    End If
                End If
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        trvSelectedRows.EndUpdate()
        If trvSelectedRows.SelectedNode IsNot Nothing Then
            trvSelectedRows.SelectedNode.EnsureVisible()
        End If
    End Sub

    Private Sub FillInsuranceType()
        Dim dt As New DataTable()
        cmb_InsuranceType.DataSource = Nothing
        Try
            Dim odb As New gloStream.gloDataBase.gloDataBase

            Dim _sqlQuery As String = "SELECT ISNULL(sInsuranceTypeCode,'') + '-' + ISNULL(sInsuranceTypeDesc,'')  AS sInsuranceType,ISNULL(sInsuranceTypeCode,'') AS sInsuranceTypeCode  FROM InsuranceType_MST"
            odb.Connect(gstrConnectionString)
            dt = odb.ReadQueryData(_sqlQuery)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                Dim dr As DataRow
                dr = dt.NewRow()
                dr("sInsuranceType") = ""
                dr("sInsuranceTypeCode") = ""
                dt.Rows.InsertAt(dr, 0)
                cmb_InsuranceType.DataSource = dt
                cmb_InsuranceType.DisplayMember = dt.Columns("sInsuranceType").ColumnName
                cmb_InsuranceType.ValueMember = dt.Columns("sInsuranceTypeCode").ColumnName
                cmb_InsuranceType.SelectedIndex = 0
            End If

        Catch ex As Exception

        Finally

        End Try
    End Sub

    Private Sub FillCountry()
        Dim dt As New DataTable()
        cmbCountry.DataSource = Nothing
        Try
            Dim odb As New gloStream.gloDataBase.gloDataBase

            Dim _sqlQuery As String = "SELECT nid,scode,sName FROM Contacts_Country_MST WHERE isnull(bIsBlocked,0)=0"
            odb.Connect(gstrConnectionString)
            dt = odb.ReadQueryData(_sqlQuery)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then


                cmbCountry.DataSource = dt
                cmbCountry.DisplayMember = dt.Columns("sName").ColumnName
                cmbCountry.ValueMember = dt.Columns("scode").ColumnName
                cmbCountry.SelectedIndex = 0
            End If

        Catch ex As Exception

        Finally

        End Try
    End Sub
    'Funtion FillFutureAppointmentTypeList
    Private Sub FillFutureAppointmentTypeList()

        Dim dtFutureDayApptType As DataTable
        Dim _strSQL As String = ""
        Dim odb As New gloStream.gloDataBase.gloDataBase
        Try
            _strSQL = "select 0 as nAppointmentTypeID, '' as sAppointmentType union select nAppointmentTypeID, sAppointmentType from AB_AppointmentType where bIsBlocked = 0 AND nAppProcType = 1 ORDER BY sAppointmentType "
            odb.Connect(gstrConnectionString)

            dtFutureDayApptType = odb.ReadQueryData(_strSQL)
            odb.Disconnect()
            If dtFutureDayApptType IsNot Nothing AndAlso dtFutureDayApptType.Rows.Count > 0 Then
                cmbFutureApptType.DataSource = dtFutureDayApptType
                cmbFutureApptType.ValueMember = dtFutureDayApptType.Columns("nAppointmentTypeID").ColumnName
                cmbFutureApptType.DisplayMember = dtFutureDayApptType.Columns("sAppointmentType").ColumnName
                cmbFutureApptType.SelectedIndex = 0

            End If

        Catch ex As Exception
            MessageBox.Show("Error in Setdata : " & ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            _strSQL = Nothing
            odb = Nothing
        End Try
    End Sub


    'Function for FillSameAppointmentTypeList

    Private Sub FillSameAppointmentTypeList()

        Dim dtSameDayApptType As DataTable
        Dim _strSQL As String = ""
        Dim odb As New gloStream.gloDataBase.gloDataBase
        Try
            _strSQL = "select 0 as nAppointmentTypeID, '' as sAppointmentType union select nAppointmentTypeID, sAppointmentType from AB_AppointmentType where bIsBlocked = 0 AND nAppProcType = 1 ORDER BY sAppointmentType "
            odb.Connect(gstrConnectionString)

            dtSameDayApptType = odb.ReadQueryData(_strSQL)
            odb.Disconnect()
            If dtSameDayApptType IsNot Nothing AndAlso dtSameDayApptType.Rows.Count > 0 Then
                cmbSameDayApptType.DataSource = dtSameDayApptType
                cmbSameDayApptType.ValueMember = dtSameDayApptType.Columns("nAppointmentTypeID").ColumnName
                cmbSameDayApptType.DisplayMember = dtSameDayApptType.Columns("sAppointmentType").ColumnName
                cmbSameDayApptType.SelectedIndex = 0
            End If
        Catch ex As Exception
            MessageBox.Show("Error in Setdata : " & ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            _strSQL = Nothing
            odb = Nothing
        End Try
    End Sub

    ''Added By kishor
    Public Shared Function GetAllClinicInformation(clinicId As Long) As DataTable

        Dim conn As New SqlConnection(gstrConnectionString)
        Dim sqladpt As New SqlDataAdapter
        Dim dtclinic As New DataTable

        conn.Open()
        Dim sql As SqlCommand = Nothing
        Dim _strsql As String
        Try
            sql = New SqlCommand
            sql.CommandType = CommandType.Text
            _strsql = "select * from Clinic_MST where nclinicid = " & clinicId & ""
            sql.CommandText = _strsql
            sql.Connection = conn
            sqladpt.SelectCommand = sql
            sqladpt.Fill(dtclinic)
            Return dtclinic
        Catch
            Return Nothing
        Finally
            If Not IsNothing(sqladpt) Then
                sqladpt.Dispose()
                sqladpt = Nothing
            End If
            If Not IsNothing(conn) Then
                conn.Dispose()
                conn = Nothing
            End If
            If Not IsNothing(sql) Then
                sql.Dispose()
                sql = Nothing
            End If
            If Not IsNothing(dtclinic) Then
                dtclinic.Dispose()
                dtclinic = Nothing
            End If
        End Try
    End Function

    ''Added by Abhijeet 0n 201104007
    Public Shared Function GetClinicInformation(ByVal fieldName As String) As String
        Dim _sqlQuery As String = String.Empty
        Dim _oDBLayer As gloDatabaseLayer.DBLayer = Nothing
        Dim _fieldName As String = String.Empty
        Dim _retObject As Object = Nothing
        Dim _retString As String = String.Empty
        Try
            _fieldName = fieldName.Trim().Replace("'", "''")

            _oDBLayer = New gloDatabaseLayer.DBLayer(gstrConnectionString)
            _oDBLayer.Connect(False)
            _sqlQuery = "select isnull(" & _fieldName & ",'') as [" & _fieldName & "] from Clinic_MST where nclinicid = 1"

            _retObject = _oDBLayer.ExecuteScalar_Query(_sqlQuery)

            If Not IsNothing(_retObject) Then
                _retString = Convert.ToString(_retObject)
            Else
                _retString = ""
            End If

            _oDBLayer.Disconnect()

        Catch
            _retString = ""
        Finally

            _sqlQuery = String.Empty

            If Not IsNothing(_oDBLayer) Then
                _oDBLayer.Disconnect()
                _oDBLayer.Dispose()
                _oDBLayer = Nothing
            End If

            _fieldName = String.Empty
            If Not IsNothing(_retObject) Then
                _retObject = Nothing
            End If

        End Try

        Return _retString
    End Function

    ''End of changes by Abhijeet on 201104078

    Private Sub cmb_InsuranceType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmb_InsuranceType.SelectedIndexChanged
        GetPaymentSetting()
    End Sub

    Private Sub RadioButton51_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'If RadioButton51.Checked = True Then
        '    RadioButton51.Font = New Font("Tahoma", 9, FontStyle.Bold)
        'Else
        '    RadioButton51.Font = New Font("Tahoma", 9, FontStyle.Regular)
        'End If
    End Sub

    Private Sub RB_ChargeAmt_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RB_ChargeAmt.CheckedChanged
        If RB_ChargeAmt.Checked = True Then
            RB_ChargeAmt.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            RB_ChargeAmt.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub GroupBox73_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupBox73.Enter

    End Sub

    ''''Added by Anil 20090717
    ''''Provider Settings

#Region "Provider Settings For EDI generation"

    Private Sub DesignGridForProviderSetting()
        Try
            c1Providers.Rows.Count = 1
            c1Providers.Cols.Count = COL_COUNTS

            c1Providers.SetData(0, COL_PROVIDER_ID, "Provider ID")
            c1Providers.SetData(0, COL_PROVIDERNAME, "Provider Name")
            c1Providers.SetData(0, COL_SUBMITTER, "Submitter")
            c1Providers.SetData(0, COL_RENDERING, "Rendering")
            c1Providers.SetData(0, COL_BILLING, "Billing")

            c1Providers.Cols(COL_PROVIDER_ID).Visible = False
            c1Providers.Cols(COL_PROVIDERNAME).Visible = True
            c1Providers.Cols(COL_SUBMITTER).Visible = True
            c1Providers.Cols(COL_RENDERING).Visible = True
            c1Providers.Cols(COL_BILLING).Visible = True

            c1Providers.AllowEditing = True

            c1Providers.Cols(COL_PROVIDER_ID).AllowEditing = False
            c1Providers.Cols(COL_PROVIDERNAME).AllowEditing = False
            c1Providers.Cols(COL_SUBMITTER).AllowEditing = True
            c1Providers.Cols(COL_RENDERING).AllowEditing = True
            c1Providers.Cols(COL_BILLING).AllowEditing = True

            c1Providers.Cols(COL_SUBMITTER).ComboList = " |Company|Practice|Business|Clinic"
            c1Providers.Cols(COL_BILLING).ComboList = " |Company|Practice|Business|Clinic"
            c1Providers.Cols(COL_RENDERING).ComboList = " |Company|Practice|Business|Clinic"

            Dim _width As Int32 = c1Providers.Width - 5
            c1Providers.Cols(COL_PROVIDERNAME).Width = Convert.ToInt32(_width * 0.4)
            c1Providers.Cols(COL_SUBMITTER).Width = Convert.ToInt32(_width * 0.2)
            c1Providers.Cols(COL_RENDERING).Width = Convert.ToInt32(_width * 0.2)
            c1Providers.Cols(COL_BILLING).Width = Convert.ToInt32(_width * 0.2)


            'Fill Providers To Grid
            Dim dtProviders As DataTable = GetProvidersForProviderSetting()
            If dtProviders IsNot Nothing AndAlso dtProviders.Rows.Count > 0 Then
                For i As Integer = 0 To dtProviders.Rows.Count - 1
                    c1Providers.Rows.Add()
                    Dim RowIndex As Int32 = c1Providers.Rows.Count - 1
                    c1Providers.SetData(RowIndex, COL_PROVIDER_ID, Convert.ToString(dtProviders.Rows(i)("nProviderID")))
                    c1Providers.SetData(RowIndex, COL_PROVIDERNAME, Convert.ToString(dtProviders.Rows(i)("ProviderName")))
                Next
                '----------------
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Function GetProvidersForProviderSetting() As DataTable
        Dim oDB As New gloDatabaseLayer.DBLayer(gloEMRAdmin.mdlGeneral.GetConnectionString())
        Dim _strSQL As [String] = ""
        Dim dtProviderType As New DataTable()
        Try
            If Me._ClinicID = 0 Then
                _strSQL = "SELECT nProviderID , (ISNULL(sFirstName,'')+ SPACE(1) + ISNULL(sMiddleName,'') + SPACE(1) +ISNULL(sLastName,'')) AS ProviderName FROM Provider_MST ORDER BY ProviderName"
            Else
                _strSQL = "SELECT nProviderID , (ISNULL(sFirstName,'')+ SPACE(1) + ISNULL(sMiddleName,'') + SPACE(1) +ISNULL(sLastName,'')) AS ProviderName FROM Provider_MST WHERE nClinicID = " & Me._ClinicID & " ORDER BY ProviderName"
            End If

            oDB.Connect(False)
            oDB.Retrive_Query(_strSQL, dtProviderType)
            oDB.Disconnect()
            If dtProviderType IsNot Nothing AndAlso dtProviderType.Rows.Count > 0 Then
                Return dtProviderType
            Else
                Return Nothing
            End If
        Catch dbex As gloDatabaseLayer.DBException
            dbex.ERROR_Log(dbex.ToString())
            Return Nothing
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If oDB IsNot Nothing Then
                oDB.Dispose()
            End If
        End Try
    End Function

    Private Sub FillProviderSettings()

        Dim value As New Object()
        Try

            Dim dtSubmetter As DataTable = GetSettingFromDB("SubmitterSetting")
            If dtSubmetter IsNot Nothing AndAlso dtSubmetter.Rows.Count > 0 Then
                For i As Integer = 1 To c1Providers.Rows.Count - 1
                    For k As Integer = 0 To dtSubmetter.Rows.Count - 1
                        If Convert.ToInt64(c1Providers.GetData(i, COL_PROVIDER_ID)) = Convert.ToInt64(dtSubmetter.Rows(k)("nUserID")) Then
                            c1Providers.SetData(i, COL_SUBMITTER, Convert.ToString(dtSubmetter.Rows(k)("sSettingsValue")))
                            Exit For
                        End If
                    Next
                Next
            End If

            Dim dtBilling As DataTable = GetSettingFromDB("BillingSetting")
            If dtBilling IsNot Nothing AndAlso dtBilling.Rows.Count > 0 Then
                For i As Integer = 1 To c1Providers.Rows.Count - 1
                    For k As Integer = 0 To dtBilling.Rows.Count - 1
                        If Convert.ToInt64(c1Providers.GetData(i, COL_PROVIDER_ID)) = Convert.ToInt64(dtBilling.Rows(k)("nUserID")) Then
                            c1Providers.SetData(i, COL_BILLING, Convert.ToString(dtBilling.Rows(k)("sSettingsValue")))
                            Exit For
                        End If
                    Next
                Next
            End If

            Dim dtRendering As DataTable = GetSettingFromDB("RenderingSetting")
            If dtRendering IsNot Nothing AndAlso dtRendering.Rows.Count > 0 Then
                For i As Integer = 1 To c1Providers.Rows.Count - 1
                    For k As Integer = 0 To dtRendering.Rows.Count - 1
                        If Convert.ToInt64(c1Providers.GetData(i, COL_PROVIDER_ID)) = Convert.ToInt64(dtRendering.Rows(k)("nUserID")) Then
                            c1Providers.SetData(i, COL_RENDERING, Convert.ToString(dtRendering.Rows(k)("sSettingsValue")))
                            Exit For
                        End If
                    Next
                Next
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

        End Try
    End Sub

    ''' <summary>
    ''' To Get the Value of the Setting from The Database
    ''' </summary>
    ''' <param name="SettingName"> Name of Setting</param>
    ''' <param name="Value"> Gets the Value of Respective Setting if Exits else set to 'null'</param>
    Private Function GetSettingFromDB(ByVal SettingName As String) As DataTable
        Dim Value As DataTable
        Dim oDB As New gloDatabaseLayer.DBLayer(gloEMRAdmin.mdlGeneral.GetConnectionString())
        Dim oDBParameters As New gloDatabaseLayer.DBParameters()
        Try
            oDB.Connect(False)
            Value = New DataTable()
            oDB.Retrive_Query("SELECT ISNULL(sSettingsValue,'') AS sSettingsValue, ISNULL(nUserID,0) AS nUserID FROM Settings WHERE sSettingsName = '" & SettingName & "' AND nClinicID = " & _ClinicID & "", Value)

            Return Value
        Catch DBErr As gloDatabaseLayer.DBException
            Value = Nothing
            DBErr.ERROR_Log(DBErr.Message)
        Catch ex As Exception
            Value = Nothing
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            oDB.Disconnect()
            oDBParameters.Dispose()
            oDB.Dispose()
        End Try

        Return Value
    End Function

    Public Function AddSettingToDB(ByVal Name As String, ByVal Value As String, ByVal ClinicID As Int64, ByVal UserID As Int64, ByVal UserClinicFlag As SettingFlag, ByRef dtSettings As DataTable) As Boolean
        '    Dim oDB As New gloDatabaseLayer.DBLayer(gloEMRAdmin.mdlGeneral.GetConnectionString())
        ' Dim oDBParameters As New gloDatabaseLayer.DBParameters()
        Try
            '  oDB.Connect(False)

            'oDBParameters.Add("@sSettingsName", Name, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar)
            'oDBParameters.Add("@sSettingsValue", Value, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar)
            'oDBParameters.Add("@nClinicID", ClinicID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar)
            'oDBParameters.Add("@nUserID", UserID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar)
            'oDBParameters.Add("@nUserClinicFlag", UserClinicFlag.GetHashCode(), System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar)

            'oDB.Execute("gsp_InUpSettings", oDBParameters)
            dtSettings.Rows.Add(Name, Value, Name, ClinicID, UserID)
            Return True
            ' Catch DBErr As gloDatabaseLayer.DBException
            ' Return False
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        Finally
            '   oDB.Disconnect()
            ' oDBParameters.Dispose()
            ' oDB.Dispose()
        End Try
    End Function

    Private Function SaveProviderSetting(ByRef dtSettings As DataTable)
        Try
            For i As Integer = 1 To c1Providers.Rows.Count - 1
                Dim sSubmitter As String = ""
                Dim sRendering As String = ""
                Dim sBilling As String = ""

                Dim nProviderID As Int64 = 0
                sSubmitter = Convert.ToString(c1Providers.GetData(i, COL_SUBMITTER))
                sRendering = Convert.ToString(c1Providers.GetData(i, COL_RENDERING))
                sBilling = Convert.ToString(c1Providers.GetData(i, COL_BILLING))

                nProviderID = Convert.ToInt64(c1Providers.GetData(i, COL_PROVIDER_ID))
                AddSettingToDB("SubmitterSetting", sSubmitter, _ClinicID, nProviderID, SettingFlag.User, dtSettings)
                AddSettingToDB("BillingSetting", sBilling, _ClinicID, nProviderID, SettingFlag.User, dtSettings)
                AddSettingToDB("RenderingSetting", sRendering, _ClinicID, nProviderID, SettingFlag.User, dtSettings)

            Next
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Function

#End Region

    Private Sub chkUseFileCompession_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkUseFileCompession.CheckedChanged

    End Sub

    Private Sub c1Providers_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles c1Providers.MouseMove

    End Sub

#Region " Growth Chart Percentile Design "
    Private Sub chk_GrowthChart_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chk_GrowthChart.CheckedChanged
        grbGrowthChartPercentile.Enabled = chk_GrowthChart.Checked
    End Sub

    Private Sub rbShowPercentile_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbShowPercentile.CheckedChanged
        If rbShowPercentile.Checked Then
            rbShowPercentile.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            rbShowPercentile.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub rbShowPercentileOnMouseHoover_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbShowPercentileOnMouseHoover.CheckedChanged
        If rbShowPercentileOnMouseHoover.Checked Then
            rbShowPercentileOnMouseHoover.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            rbShowPercentileOnMouseHoover.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub rbDontShowPercentile_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbDontShowPercentile.CheckedChanged
        If rbDontShowPercentile.Checked Then
            rbDontShowPercentile.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            rbDontShowPercentile.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub
#End Region


    Private Sub btnServerPath_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnServerPath.Click
        Dim _SelectedPath As String = ""
        Dim _gloDataPath As String = ""
        Dim _ClaimManagementFolder As String = ""
        Dim _ClaimManagement_InBoxFolder As String = ""
        Dim _ClaimManagement_OutBoxFolder As String = ""
        Dim _ClaimManagement_GeneralFolder As String = ""

        Dim oDialog As New FolderBrowserDialog()
        oDialog.Description = "gloEMR Server Path"
        If oDialog.ShowDialog() = DialogResult.OK Then
            _SelectedPath = oDialog.SelectedPath
            If _SelectedPath.Substring(_SelectedPath.Length - 1, 1) = "\" Then
                _gloDataPath = _SelectedPath & "gloEMR"
            Else
                _gloDataPath = _SelectedPath & "\gloEMR"
            End If

            If System.IO.Directory.Exists(_gloDataPath) = False Then
                If MessageBox.Show("Selected path does not contain gloEMR folder, do you want to create gloData structure on selected path?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    System.IO.Directory.CreateDirectory(_gloDataPath)
                End If
            End If

            '#Region "Check and Create Claim Management Structure" 
            If System.IO.Directory.Exists(_gloDataPath) = True Then
                txtServerPath.Text = _gloDataPath
                _ClaimManagementFolder = _gloDataPath & "\Claim Management"
                _ClaimManagement_InBoxFolder = _ClaimManagementFolder & "\InBox"
                _ClaimManagement_OutBoxFolder = _ClaimManagementFolder & "\OutBox"
                _ClaimManagement_GeneralFolder = _ClaimManagementFolder & "\General"

                If System.IO.Directory.Exists(_ClaimManagementFolder) = False Then
                    System.IO.Directory.CreateDirectory(_ClaimManagementFolder)
                End If
                If System.IO.Directory.Exists(_ClaimManagement_InBoxFolder) = False Then
                    System.IO.Directory.CreateDirectory(_ClaimManagement_InBoxFolder)
                End If
                If System.IO.Directory.Exists(_ClaimManagement_OutBoxFolder) = False Then
                    System.IO.Directory.CreateDirectory(_ClaimManagement_OutBoxFolder)
                End If
                If System.IO.Directory.Exists(_ClaimManagement_GeneralFolder) = False Then
                    System.IO.Directory.CreateDirectory(_ClaimManagement_GeneralFolder)
                End If

                If System.IO.Directory.Exists(_ClaimManagement_InBoxFolder & "\271 Eligibility Response") = False Then
                    System.IO.Directory.CreateDirectory(_ClaimManagement_InBoxFolder & "\271 Eligibility Response")
                End If
                If System.IO.Directory.Exists(_ClaimManagement_InBoxFolder & "\277 Claim Status Response") = False Then
                    System.IO.Directory.CreateDirectory(_ClaimManagement_InBoxFolder & "\277 Claim Status Response")
                End If
                If System.IO.Directory.Exists(_ClaimManagement_InBoxFolder & "\835 Remittance Advice") = False Then
                    System.IO.Directory.CreateDirectory(_ClaimManagement_InBoxFolder & "\835 Remittance Advice")
                End If
                If System.IO.Directory.Exists(_ClaimManagement_InBoxFolder & "\997 Acknowledgement") = False Then
                    System.IO.Directory.CreateDirectory(_ClaimManagement_InBoxFolder & "\997 Acknowledgement")
                End If

                If System.IO.Directory.Exists(_ClaimManagement_OutBoxFolder & "\276 Eligibility Enquiry") = False Then
                    System.IO.Directory.CreateDirectory(_ClaimManagement_OutBoxFolder & "\276 Eligibility Enquiry")
                End If
                If System.IO.Directory.Exists(_ClaimManagement_OutBoxFolder & "\837P Claim submission") = False Then
                    System.IO.Directory.CreateDirectory(_ClaimManagement_OutBoxFolder & "\837P Claim submission")
                End If
                If System.IO.Directory.Exists(_ClaimManagement_OutBoxFolder & "\997 Acknowledgement") = False Then
                    System.IO.Directory.CreateDirectory(_ClaimManagement_OutBoxFolder & "\997 Acknowledgement")
                End If

                If System.IO.Directory.Exists(_ClaimManagement_GeneralFolder & "\CSR Reports") = False Then
                    System.IO.Directory.CreateDirectory(_ClaimManagement_GeneralFolder & "\CSR Reports")
                End If
                If System.IO.Directory.Exists(_ClaimManagement_GeneralFolder & "\Letters") = False Then
                    System.IO.Directory.CreateDirectory(_ClaimManagement_GeneralFolder & "\Letters")
                End If
                If System.IO.Directory.Exists(_ClaimManagement_GeneralFolder & "\Reports") = False Then
                    System.IO.Directory.CreateDirectory(_ClaimManagement_GeneralFolder & "\Reports")
                End If
                If System.IO.Directory.Exists(_ClaimManagement_GeneralFolder & "\Statements") = False Then
                    System.IO.Directory.CreateDirectory(_ClaimManagement_GeneralFolder & "\Statements")
                End If
                If System.IO.Directory.Exists(_ClaimManagement_GeneralFolder & "\Worked Transaction") = False Then
                    System.IO.Directory.CreateDirectory(_ClaimManagement_GeneralFolder & "\Worked Transaction")
                End If

                '#End Region 
            End If
        End If
    End Sub

    Private Sub btnDeleteServerPath_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDeleteServerPath.Click
        txtServerPath.Text = ""
    End Sub




    ''Sandip darade 
    Private Sub SavegloEMRDatabaseSettings(ByRef dtsettingsdata As DataTable)
        Try
            Dim ogloSettings As New clsSettings
            ' ogloSettings.Add("Add Patient To gloEMR", chk_PMDBSettings.Checked.ToString(), gnClinicID, gnLoginID, SettingFlag.Clinic)
           
            dtsettingsdata.Rows.Add("Add Patient To gloEMR", chk_PMDBSettings.Checked.ToString(), "Add Patient To gloEMR")
            Dim MigrateToEMRType As String = ""
            If (rbtn_MigratetogloEMR40.Checked = True) Then
                MigrateToEMRType = "gloEMR40SP2"
            End If
            If (rbtn_MigratetogloEMR50.Checked = True) Then
                MigrateToEMRType = "gloEMR50"
            End If
            ' ogloSettings.Add("MigrateToEMRType", MigrateToEMRType, gnClinicID, gnLoginID, SettingFlag.Clinic)
            dtsettingsdata.Rows.Add("MigrateToEMRType", MigrateToEMRType, "Migrate ToEMRType")

            ' ogloSettings.Add("gloEMR SQL Server Name", txtPMServerName.Text.Trim(), gnClinicID, gnLoginID, SettingFlag.Clinic)
            dtsettingsdata.Rows.Add("gloEMR SQL Server Name", txtPMServerName.Text.Trim(), "gloEMR SQL Server Name")
            'ogloSettings.Add("gloEMR Database Name", txtPMDatabaseName.Text.Trim(), gnClinicID, gnLoginID, SettingFlag.Clinic)
            dtsettingsdata.Rows.Add("gloEMR Database Name", txtPMDatabaseName.Text.Trim(), "gloEMR Database Name")

            If (optSQLAuthentication.Checked = True) Then
                ' ogloSettings.Add("gloEMR Authentication", "SQL", gnClinicID, gnLoginID, SettingFlag.Clinic)
                dtsettingsdata.Rows.Add("gloEMR Authentication", "SQL", "gloEMR Authentication")

            Else
                'ogloSettings.Add("gloEMR Authentication", "Windows", gnClinicID, gnLoginID, SettingFlag.Clinic)
                dtsettingsdata.Rows.Add("gloEMR Authentication", "Windows", "gloEMR Authentication")
            End If
            'ogloSettings.Add("gloEMR User Name", txtSQLUserID.Text.Trim(), gnClinicID, gnLoginID, SettingFlag.Clinic)
            dtsettingsdata.Rows.Add("gloEMR User Name", txtSQLUserID.Text.Trim(), "gloEMR User Name")
            dtsettingsdata.Rows.Add("gloEMR Password", txtSQLPassword.Text.Trim(), "gloEMR User Password")
            'ogloSettings.Add("gloEMR Password", txtSQLPassword.Text.Trim(), gnClinicID, gnLoginID, SettingFlag.Clinic)
        Catch ex As Exception
            MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub


    Private Sub RetrievegloEMRDatabaseSettings()
        Try
            ' gloEMR Database Settings 
            'gloSettings.GeneralSettings ogloSettings = new GeneralSettings(_databaseConnectionString); 
            Dim ogloSettings As New clsSettings

            Dim value As New Object()


            ogloSettings.GetSetting("Add Patient To gloEMR", 0, gnClinicID, value)
            If value IsNot Nothing And Convert.ToString(value) <> "" Then
                If (Convert.ToBoolean(value) = True) Then

                    chk_PMDBSettings.Checked = True

                End If
                ogloSettings.Get_gloEMRSetting("gloEMR SQL Server Name", value)
                If value IsNot Nothing Then
                    txtPMServerName.Text = Convert.ToString(value)
                    If txtPMServerName.Text = "" Then
                        chk_PMDBSettings.Checked = False
                    End If
                    value = Nothing
                End If
                ogloSettings.GetSetting("MigrateToEMRType", 0, _ClinicID, value)
                If Not IsNothing(value) Then
                    If (Convert.ToString(value.ToString().Trim) <> "") Then
                        If (Convert.ToString(value) = "gloEMR40SP2") Then
                            rbtn_MigratetogloEMR40.Checked = True
                        End If
                        If (Convert.ToString(value) = "gloEMR50") Then
                            rbtn_MigratetogloEMR50.Checked = True
                        End If
                    End If
                End If
                value = Nothing

                ogloSettings.Get_gloEMRSetting("gloEMR Database Name", value)
                If value IsNot Nothing Then
                    txtPMDatabaseName.Text = Convert.ToString(value)
                    value = Nothing
                End If
                ogloSettings.Get_gloEMRSetting("gloEMR Authentication", value)
                If value IsNot Nothing Then
                    If (Convert.ToString(value) = "SQL") Then

                        optSQLAuthentication.Checked = True

                        value = Nothing
                        ogloSettings.Get_gloEMRSetting("gloEMR User Name", value)
                        If value IsNot Nothing Then
                            txtSQLUserID.Text = Convert.ToString(value)
                            value = Nothing
                        End If
                        ogloSettings.Get_gloEMRSetting("gloEMR Password", value)
                        If value IsNot Nothing Then
                            txtSQLPassword.Text = Convert.ToString(value)
                            value = Nothing
                        End If
                        If optSQLAuthentication.Checked = True AndAlso chk_PMDBSettings.Checked = True Then
                            txtSQLUserID.Enabled = True
                            txtSQLPassword.Enabled = True
                        Else
                            txtSQLUserID.Enabled = False
                            txtSQLPassword.Enabled = False
                        End If

                    End If
                End If
            Else

                chk_PMDBSettings.Checked = False

            End If

            value = Nothing
            ''  End If



            ' 

            ogloSettings = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub

    Private Sub Set_gloEMRDBsettingControls()
        If chk_PMDBSettings.Checked = False Then
            pnlgloPMDBSettings.Enabled = False
            txtPMDatabaseName.Enabled = False
            txtPMServerName.Enabled = False
            optSQLAuthentication.Enabled = False
            optWindowsAuthentication.Enabled = False
            txtSQLUserID.Enabled = False
            txtSQLPassword.Enabled = False
            C1Provider.Enabled = False
            btnConnect.Enabled = False
            pnl_Migratetype.Enabled = False
        Else
            pnlgloPMDBSettings.Enabled = True
            txtPMDatabaseName.Enabled = True
            txtPMServerName.Enabled = True
            optSQLAuthentication.Enabled = True
            optWindowsAuthentication.Enabled = True
            txtSQLUserID.Enabled = True
            txtSQLPassword.Enabled = True
            C1Provider.Enabled = True
            btnConnect.Enabled = True
            pnl_Migratetype.Enabled = True
        End If
    End Sub

    '<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<ojeswini_4/08/2009>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
    Private Sub optClinicDIYes_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optClinicDIYes.CheckedChanged
        If optClinicDIYes.Checked = True Then
            optClinicDIYes.Font = New Font("Tahoma", 9, FontStyle.Bold)
            txtDIServiceURL.Enabled = True
        Else
            optClinicDIYes.Font = New Font("Tahoma", 9, FontStyle.Regular)
            txtDIServiceURL.Enabled = False
        End If
    End Sub

    Private Sub optClinicDINo_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optClinicDINo.CheckedChanged
        If optClinicDINo.Checked = True Then
            optClinicDINo.Font = New Font("Tahoma", 9, FontStyle.Bold)
            txtDIServiceURL.Enabled = False
        Else
            optClinicDINo.Font = New Font("Tahoma", 9, FontStyle.Regular)
            txtDIServiceURL.Enabled = True
        End If
    End Sub

    Private Sub optClinicFormularyYes_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'If optClinicFormularyYes.Checked = True Then
        '    optClinicFormularyYes.Font = New Font("Tahoma", 9, FontStyle.Bold)
        'Else
        '    optClinicFormularyYes.Font = New Font("Tahoma", 9, FontStyle.Regular)
        'End If
    End Sub

    Private Sub optClinicFormularyNo_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'If optClinicFormularyNo.Checked = True Then
        '    optClinicFormularyNo.Font = New Font("Tahoma", 9, FontStyle.Bold)
        'Else
        '    optClinicFormularyNo.Font = New Font("Tahoma", 9, FontStyle.Regular)
        'End If
    End Sub

    Private Sub optLocationAddressedYes_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optLocationAddressedYes.CheckedChanged
        If optLocationAddressedYes.Checked = True Then
            optLocationAddressedYes.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            optLocationAddressedYes.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub optLocationAddressedNo_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optLocationAddressedNo.CheckedChanged
        If optLocationAddressedNo.Checked = True Then
            optLocationAddressedNo.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            optLocationAddressedNo.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub optHPIYes_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optHPIYes.CheckedChanged
        If optHPIYes.Checked = True Then
            optHPIYes.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            optHPIYes.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub optHPINo_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optHPINo.CheckedChanged
        If optHPINo.Checked = True Then
            optHPINo.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            optHPINo.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub optPwdComplexNo_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optPwdComplexNo.CheckedChanged
        If optPwdComplexNo.Checked = True Then
            optPwdComplexNo.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            optPwdComplexNo.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub rbStaging_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbStaging.CheckedChanged
        If rbStaging.Checked = True Then
            rbStaging.Font = New Font("Tahoma", 9, FontStyle.Bold)
            If chkNCPDPVer10dot6.Checked = True Then
                TxtSurescriptURL.Text = s10dot6StagingURl
            Else
                TxtSurescriptURL.Text = sStagingURl
            End If
            txtMedHistoryPortalURL.Text = sMedHxStagingURl
        Else
            rbStaging.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub rbProduction_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbProduction.CheckedChanged
        If rbProduction.Checked = True Then
            rbProduction.Font = New Font("Tahoma", 9, FontStyle.Bold)
            If chkNCPDPVer10dot6.Checked = True Then
                TxtSurescriptURL.Text = s10dot6ProductionURl
            Else
                TxtSurescriptURL.Text = sProductionURl
            End If
            txtMedHistoryPortalURL.Text = sMedHxProductionURl
        Else
            rbProduction.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub rbAdvRxStaging_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If rbAdvRxStaging.Checked = True Then
            rbAdvRxStaging.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            rbAdvRxStaging.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub rbAdvRxProduction_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If rbAdvRxProduction.Checked = True Then
            rbAdvRxProduction.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            rbAdvRxProduction.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub rdbNone_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbNone.CheckedChanged
        ''Sandip  Darade 200911107
        'If rdbNone.Checked = True Then
        '    rdbNone.Font = New Font("Tahoma", 9, FontStyle.Bold)
        '    txtAlphaIIDatabase.Enabled = False
        '    txtAlphaIIPassword.Enabled = False
        '    txtAlphaIIServerName.Enabled = False
        '    txtAlphaIIUserName.Enabled = False
        '    cmbAlphaIIAuthentication.Enabled = False
        'Else
        '    rdbNone.Font = New Font("Tahoma", 9, FontStyle.Regular)
        'End If
    End Sub

    Private Sub optFAXreceiveYes_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optFAXreceiveYes.CheckedChanged
        If optFAXreceiveYes.Checked = True Then
            optFAXreceiveYes.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            optFAXreceiveYes.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub optFAXreceiveNo_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optFAXreceiveNo.CheckedChanged
        If optFAXreceiveNo.Checked = True Then
            optFAXreceiveNo.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            optFAXreceiveNo.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub optWindowsAuthentication_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optWindowsAuthentication.CheckedChanged
        If optWindowsAuthentication.Checked = True Then
            optWindowsAuthentication.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            optWindowsAuthentication.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub Rbtn_showcode_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Rbtn_showcode.CheckedChanged
        If Rbtn_showcode.Checked = True Then
            Rbtn_showcode.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            Rbtn_showcode.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub Rbtn_showDesc_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Rbtn_showDesc.CheckedChanged
        If Rbtn_showDesc.Checked = True Then
            Rbtn_showDesc.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            Rbtn_showDesc.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub Rbtn_ShowBoth_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Rbtn_ShowBoth.CheckedChanged
        If Rbtn_ShowBoth.Checked = True Then
            Rbtn_ShowBoth.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            Rbtn_ShowBoth.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub rbChiefComplaint_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbChiefComplaint.CheckedChanged
        If rbChiefComplaint.Checked = True Then
            rbChiefComplaint.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            rbChiefComplaint.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub rbProblemList_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbProblemList.CheckedChanged
        If rbProblemList.Checked = True Then
            rbProblemList.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            rbProblemList.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub Rb_AllowedAmt_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Rb_AllowedAmt.CheckedChanged
        If Rb_AllowedAmt.Checked = True Then
            Rb_AllowedAmt.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            Rb_AllowedAmt.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    '<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<ojeswini_4/08/2009>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

    Private Sub btn_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddSurgicalAlertUser.MouseHover, btnDelSurgicalAlertUser.MouseHover, btnServerPath.MouseHover, btnDeleteServerPath.MouseHover, btnDelFollowUPUser.MouseHover, btnBrowseeFaxDownloadDir.MouseHover, btnAddFollowUpUser.MouseHover, btnSetPwdComplexity.MouseHover, btnSelectRecoveryPath.MouseHover, btnConnect.MouseHover
        CType(sender, Button).BackgroundImage = Global.gloEMRAdmin.My.Resources.Resources.Img_LongYellow
        CType(sender, Button).BackgroundImageLayout = ImageLayout.Stretch
    End Sub

    Private Sub btn_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelSurgicalAlertUser.MouseLeave, btnAddSurgicalAlertUser.MouseLeave, btnServerPath.MouseLeave, btnDeleteServerPath.MouseLeave, btnDelFollowUPUser.MouseLeave, btnBrowseeFaxDownloadDir.MouseLeave, btnAddFollowUpUser.MouseLeave, btnSetPwdComplexity.MouseLeave, btnSelectRecoveryPath.MouseLeave, btnConnect.MouseLeave
        CType(sender, Button).BackgroundImage = Global.gloEMRAdmin.My.Resources.Resources.Img_LongButton
        CType(sender, Button).BackgroundImageLayout = ImageLayout.Stretch
    End Sub

    ''Sandip Darade 20090813
    Private Sub rbtn_MigratetogloEMR50_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtn_MigratetogloEMR50.CheckedChanged
        If rbtn_MigratetogloEMR50.Checked = True Then
            rbtn_MigratetogloEMR50.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            rbtn_MigratetogloEMR50.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub rbtn_MigratetogloEMR40_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtn_MigratetogloEMR40.CheckedChanged
        If rbtn_MigratetogloEMR40.Checked = True Then
            rbtn_MigratetogloEMR40.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            rbtn_MigratetogloEMR40.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub rbICD9Driven_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbICD9Driven.CheckedChanged
        If rbICD9Driven.Checked Then
            rbICD9Driven.Font = New Font("Tahoma", 9, FontStyle.Bold)
            If rbICD9Driven.Visible Then
                If MessageBox.Show("Once you changed the interface to ICD9, the CPT interface may not show entries done with ICD9 interface." & vbLf & "Are you sure you want to change this option ? ", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = Windows.Forms.DialogResult.No Then
                    rbICD9Driven.Checked = False

                    rbCPTDriven.Visible = False
                    rbCPTDriven.Checked = True
                    rbCPTDriven.Visible = True
                End If
            End If
        Else
            rbICD9Driven.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
        chkSetCPTtoAllICD9.Enabled = rbICD9Driven.Checked
    End Sub

    Private Sub rbCPTDriven_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbCPTDriven.CheckedChanged
        If rbCPTDriven.Checked Then
            rbCPTDriven.Font = New Font("Tahoma", 9, FontStyle.Bold)
            If rbCPTDriven.Visible Then
                If MessageBox.Show("Once you changed the interface to CPT, the ICD9 interface may not show entries done with CPT interface." & vbLf & "Are you sure you want to change this option ? ", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = Windows.Forms.DialogResult.No Then
                    rbCPTDriven.Checked = False

                    rbICD9Driven.Visible = False
                    rbICD9Driven.Checked = True
                    rbICD9Driven.Visible = True
                End If
            End If
        Else
            rbCPTDriven.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
        pnlICD9Numbers.Enabled = rbCPTDriven.Checked
        pnlModifierNumber.Enabled = rbCPTDriven.Checked
    End Sub

    Private Sub rbShow8ICD9_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbShow8ICD9.CheckedChanged
        If rbShow8ICD9.Checked Then
            rbShow8ICD9.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            rbShow8ICD9.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub rbShow4ICD9_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbShow4ICD9.CheckedChanged
        If rbShow4ICD9.Checked Then
            rbShow4ICD9.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            rbShow4ICD9.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub rbShow4Modifier_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbShow4Modifier.CheckedChanged
        If rbShow4Modifier.Checked Then
            rbShow4Modifier.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            rbShow4Modifier.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub rbShow2Modifier_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbShow2Modifier.CheckedChanged
        If rbShow2Modifier.Checked Then
            rbShow2Modifier.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            rbShow2Modifier.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub


    Private Sub btn_Ok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Me.Cursor = Cursors.WaitCursor
        Dim objSettings As New clsSettings


    End Sub
    ''Sandip Darade 20090830
    ''Added  Genious Path setting
#Region "Genious Path setting"
    Private GeniusPath As String = ""
    Private _oCom As Object
    Private _oPaths As Object
    Private _oExport As Object
    Public Function GetGeniusPaths() As List(Of ClsGeniusPath)
        Dim oGeniusPaths As List(Of ClsGeniusPath) = New List(Of ClsGeniusPath)
        Try
            ' If TypeOf (_oExport) Is System.DBNull Or _oExport Is Nothing Then
            'MsgBox("Login first")
            _oCom = CreateObject("gsexport.gsexport")
            '   populate paths list
            _oPaths = _oCom.GetPaths()

            If Not IsNothing(_oPaths) Then
                If _oPaths.Count > 0 Then
                    '               Dim sCode As String = oPaths.Item(cmbPaths.SelectedIndex + 1).Code
                    Dim sCode As String = ""
                    Dim sPath As String = ""

                    '\\ added on 20090206
                    Dim oGeniusPath As ClsGeniusPath
                    For Each oPath As Object In _oPaths
                        sCode = oPath.Code
                        sPath = oPath.Path
                        oGeniusPath = New ClsGeniusPath
                        oGeniusPath.GeniusCode = sCode
                        oGeniusPath.GeniusPath = sPath.ToUpper()
                        oGeniusPaths.Add(oGeniusPath)
                        sCode = ""
                        sPath = ""
                    Next
                End If
            End If
            ' End If
        Catch ex As Exception
            Throw ex
        End Try
        Return oGeniusPaths
    End Function

    Private Sub btnLoadPath_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoadPath.Click
        Try
            PopulateGeniusPaths()
            If cmbGeniusPaths.Items.Count > 0 Then
                If GeniusPath <> "" Then
                    cmbGeniusPaths.Text = GeniusPath
                Else
                    cmbGeniusPaths.SelectedText = CType(cmbGeniusPaths.Items(0), ClsGeniusPath).GeniusPath
                End If
            End If

        Catch ex As Exception
            MessageBox.Show("Error loading Genius Paths", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub cmbGeniusPaths_SelectionChangeCommitted(ByVal sender As Object, ByVal e As System.EventArgs)

    End Sub
    Private Function PopulateGeniusPaths()

        Dim oGeniusPaths As System.Collections.Generic.List(Of ClsGeniusPath)
        Try

            cmbGeniusPaths.DataSource = Nothing
            cmbGeniusPaths.Items.Clear()
            oGeniusPaths = GetGeniusPaths()

            cmbGeniusPaths.DataSource = oGeniusPaths
            cmbGeniusPaths.DisplayMember = "GeniusPath"
            cmbGeniusPaths.ValueMember = "GeniusCode"
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub getGeniusSettings()

    End Sub
#End Region




    Private Sub btnfollowupUser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            pnlFollowUpUser.Visible = True
            pnlFollowUpUser.BringToFront()
            'colUsers.Clear()
            'colUId.Clear()

            ' setGridstyle()
            setGridstyle(c1userList)
            Dim oDB As New gloStream.gloDataBase.gloDataBase
            Dim dt As New DataTable
            Dim strSelectQry As String = "SELECT nUserID , sLoginName,ISNULL( User_MST.sFirstName,'')+' '+ISNULL(User_MST.sLastName,'') as Name, nProviderID FROM User_MST"
            oDB.Connect(gstrConnectionString)
            dt = oDB.ReadQueryData(strSelectQry)
            With c1userList
                For i As Integer = 0 To dt.Rows.Count - 1
                    c1userList.Rows.Add()
                    .SetCellCheck(i + 1, Col_Check, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
                    .SetData(i + 1, Col_UserID, dt.Rows(i)("nUserId"))
                    .SetData(i + 1, Col_LoginName, dt.Rows(i)("sLoginName"))
                    .SetData(i + 1, Col_Column1, dt.Rows(i)("Name"))
                    .SetData(i + 1, Col_ProviderID, dt.Rows(i)("nProviderID"))
                    'Dim rgActive As C1.Win.C1FlexGrid.CellRange = .GetCellRange(i + 1, Col_Check, i + 1, Col_Check)

                    'rgActive.StyleNew.DataType = GetType(Boolean)
                    'rgActive.StyleNew.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
                    'rgActive.StyleNew.ImageAlign = C1.Win.C1FlexGrid.ImageAlignEnum.CenterCenter
                Next
            End With

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

   
   



#Region "Formulary Database Setting"



    Private Function ValidateFormularyConnections(ByVal sServerName As String, ByVal sDatabaseName As String, ByVal sUserName As String, ByVal sPassword As String, ByVal bSQLAuthentication As Boolean) As Boolean
        Try
            'Added by Ashish on 2nd March for Centralized Formulary 3.0 changes
            Return True

            ''Validating the textboxes 
            If txtFormularyServerName.Text.Trim = "" Then       ''Servername
                MessageBox.Show("Enter the Formulary Database Setting Server Name.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                tb_Settings.SelectTab(tb_Settings.TabPages.IndexOf(tbpg_SurescriptSettings))
                txtFormularyServerName.Focus()
                Return False
            End If
            If txtFormularyDataBaseName.Text.Trim = "" Then     ''Databasebasename
                MessageBox.Show("Enter the Formulary Database Setting Database Name.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                tb_Settings.SelectTab(tb_Settings.TabPages.IndexOf(tbpg_SurescriptSettings))
                txtFormularyDataBaseName.Focus()
                Return False
            End If

            ''''''validation for formulary database name and gloEMR database name
            If txtFormularyDataBaseName.Text.ToUpper = gstrDatabaseName.ToUpper Then
                MessageBox.Show("Formulary database cannot be same as gloEMR database. Enter valid formulary database name.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                tb_Settings.SelectTab(tb_Settings.TabPages.IndexOf(tbpg_SurescriptSettings))
                txtFormularyDataBaseName.Focus()
                Return False
            End If


            If bSQLAuthentication Then                          ''Checking the authentication is sql or window's
                If txtFormularyUserId.Text.Trim = "" Then       ''userid
                    MessageBox.Show("Enter the Formulary Database Setting User Id.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    tb_Settings.SelectTab(tb_Settings.TabPages.IndexOf(tbpg_SurescriptSettings))
                    txtFormularyUserId.Focus()
                    Return False
                End If
                If txtFormularyPassword.Text = "" Then          ''Password
                    MessageBox.Show("Enter the Formulary Database Setting Password.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    tb_Settings.SelectTab(tb_Settings.TabPages.IndexOf(tbpg_SurescriptSettings))
                    txtFormularyPassword.Focus()
                    Return False
                End If
            End If

            Dim oCon As New SqlConnection(mdlGeneral.GetConnectionString(sServerName, sDatabaseName, bSQLAuthentication, sUserName, sPassword)) ''Checking for the sql connection
            Dim sFrmlyConnStr As String = oCon.ConnectionString
            oCon.Open()
            oCon.Close()
            oCon.Dispose()
            oCon = Nothing

            ''''''validation for formuarly table exist in given database
            Dim blnfrmlyTablePresent As Boolean = False
            Dim dtFrmlyTables As New DataTable
            dtFrmlyTables = GetFormularyTables(sFrmlyConnStr)
            If Not IsNothing(dtFrmlyTables) Then
                If dtFrmlyTables.Rows.Count > 0 Then
                    For i As Integer = 0 To dtFrmlyTables.Rows.Count - 1
                        Select Case dtFrmlyTables.Rows(i)("TableName").ToString
                            Case "RxH_CopayInformation"
                                blnfrmlyTablePresent = True
                                Exit For
                            Case "RxH_CopayInformationDDIDrug"
                                blnfrmlyTablePresent = True
                                Exit For
                            Case "RxH_CopayInformationNDCDrug"
                                blnfrmlyTablePresent = True
                                Exit For
                            Case "RxH_CopayInformationSummary"
                                blnfrmlyTablePresent = True
                                Exit For
                            Case "RxH_CoverageInformation"
                                blnfrmlyTablePresent = True
                                Exit For
                            Case "RxH_CoverageInformationDDIDrug"
                                blnfrmlyTablePresent = True
                                Exit For
                            Case "RxH_CoverageInformationNDCDrug"
                                blnfrmlyTablePresent = True
                                Exit For
                            Case "RxH_CoverageInformationResourceLinkDDIDrug"
                                blnfrmlyTablePresent = True
                                Exit For
                            Case "RxH_CoverageInformationResourceLinkNDCDrug"
                                blnfrmlyTablePresent = True
                                Exit For
                            Case "RxH_CoverageInformationResourceLinkSummary"
                                blnfrmlyTablePresent = True
                                Exit For
                            Case "RxH_FormularyAlternatives"
                                blnfrmlyTablePresent = True
                                Exit For
                            Case "RxH_FormularyAlternativesDDIDrug"
                                blnfrmlyTablePresent = True
                                Exit For
                            Case "RxH_FormularyAlternativesNDCDrug"
                                blnfrmlyTablePresent = True
                                Exit For
                            Case "RxH_FormularyStatus"
                                blnfrmlyTablePresent = True
                                Exit For
                            Case "RxH_FormularyStatusDDIDrug"
                                blnfrmlyTablePresent = True
                                Exit For
                            Case "RxH_FormularyStatusNDCDrug"
                                blnfrmlyTablePresent = True
                                Exit For
                            Case Else
                                blnfrmlyTablePresent = False

                        End Select

                    Next
                Else
                    blnfrmlyTablePresent = False
                End If

            Else
                blnfrmlyTablePresent = False
            End If
            If blnfrmlyTablePresent = False Then
                MessageBox.Show("The current database " & """" & txtFormularyDataBaseName.Text.ToUpper & """" & " does not contain fromulary tables. Enter valid formulary database name.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtFormularyDataBaseName.Focus()
                Return False '''''even though the connection is successfully connected, this datbase does not contain valid RxH formulary tables, so return false value
            Else
                Return True                 ''Successfully connected then return true
            End If

        Catch ex As Exception
            MessageBox.Show("Invalid Formulary Database Settings.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return False                ''Unsuccessfully connected return false
        End Try
    End Function
    ''' <summary>
    ''' Windows type
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rdbWindows_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbWindows.CheckedChanged
        If rdbWindows.Checked = True Then                                   ''for true 'Windows type'
            rdbWindows.Font = New Font("Tahoma", 9, FontStyle.Bold)         ''Setting the font when active
            pnlSureScriptLogin.Enabled = False
        Else                                                               ''for false
            rdbWindows.Font = New Font("Tahoma", 9, FontStyle.Regular)      ''Setting the font when inactive
        End If

    End Sub
    ''' <summary>
    ''' SQL authentication
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rdbSQL_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbSQL.CheckedChanged
        If rdbSQL.Checked = True Then                               ''for true ''SQL type
            rdbSQL.Font = New Font("Tahoma", 9, FontStyle.Bold)     ''Setting the font when active
            pnlSureScriptLogin.Enabled = True
        Else                                                        ''false
            rdbSQL.Font = New Font("Tahoma", 9, FontStyle.Regular)  ''Setting the font when inactive
        End If

    End Sub
    Private Sub btnTestConnection_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTestConnection.Click
        If ValidateFormularyConnections(txtFormularyServerName.Text.Trim, txtFormularyDataBaseName.Text.Trim, txtFormularyUserId.Text.Trim(), txtFormularyPassword.Text, rdbSQL.Checked) = False Then
            Exit Sub                                            ''If false then
        Else
            MessageBox.Show("Connection established successfully.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)  '' if true then
        End If
    End Sub
    Private Sub chkAdvanceRx_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAdvanceRx.CheckedChanged
        Try
            If chkAdvanceRx.Checked = True Then
                pnlAdvanceRxServer.Enabled = True

                ''dhruv 20100216
                ''formulary db setting
                'rdbSQL.Checked = True
                'pnlFormularySetting.Enabled = True
                'txtFormularyServerName.Text = gstrSQLServerName
                ' '' GLO2011-0015850 6052 gloEMR Admin Defect - Formulary database
                ' '' Database Name Changed
                'txtFormularyDataBaseName.Text = "gloFormulary" ''Changed the Default database name instead of  "FormularyDB" ''GLO2011-0015850 6052 gloEMR Admin Defect - Formulary database 
                'txtFormularyUserId.Text = gstrSQLUser
                'txtFormularyPassword.Text = gstrSQLPasswordEMR
                ''end
                chkAutoEligibilityONorOFF.Enabled = True
            Else
                pnlAdvanceRxServer.Enabled = False
                'txtFormularyServerName.Text = ""
                'txtFormularyDataBaseName.Text = ""
                'txtFormularyUserId.Text = ""
                'txtFormularyPassword.Text = ""


                ' ''dhruv 20100216
                ' ''formulary db setting
                'pnlFormularySetting.Enabled = False
                ' ''end
                chkAutoEligibilityONorOFF.Checked = False
                chkAutoEligibilityONorOFF.Enabled = False
                chkFormularyEnable.Checked = False

            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


#End Region

    'Added by madan on 20100505
    Private Sub btnEmdeonUserSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEmdeonUserSearch.Click
        Try

            If oListControl IsNot Nothing Then
                For i As Integer = Me.Controls.Count - 1 To 0 Step -1
                    If Me.Controls(i).Name = oListControl.Name Then
                        Me.Controls.Remove(Me.Controls(i))
                        Exit For
                    End If
                Next
            End If

            oListControl = New gloListControl.gloListControl(mdlGeneral.GetConnectionString, gloListControl.gloListControlType.Users, False, Me.Width)
            oListControl.ClinicID = _gloLab_defaultID
            oListControl.ControlHeader = "Users"

            _CurrentControlType = gloListControl.gloListControlType.Users
            AddHandler oListControl.ItemSelectedClick, AddressOf oListControl_ItemSelectedClick
            AddHandler oListControl.ItemClosedClick, AddressOf oListControl_ItemClosedClick

            Me.Controls.Add(oListControl)


            oListControl.OpenControl()
            oListControl.Dock = DockStyle.Fill
            oListControl.BringToFront()
        Catch ex As Exception
            'gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        End Try
    End Sub

    Private Sub btnEmdeonUserDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEmdeonUserDelete.Click
        txtEmdeon_DefaultUser.Text = ""
        _gloLab_DefaultUserID = 0
        _gloLab_defaultUserName = ""
    End Sub

    Private Sub txtEmdeonUserName_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtEmdeonUserName.TextChanged, txtEmdeonUrl.TextChanged, txtEmdeonPassword.TextChanged, txtEmdeonFacilityCode.TextChanged
        _gloLab_settingsEdited = True
    End Sub

    Private Sub txtEmdeonFacilityCode_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtEmdeonFacilityCode.KeyPress
        If Not ((e.KeyChar >= ChrW(48) And e.KeyChar <= ChrW(57)) Or e.KeyChar = ChrW(8)) Then
            e.Handled = True
        End If
    End Sub
    'Retriving default username from database. added on 2010/03/02 by madan
#Region "RetriveDefaultUsernameforgloLab"
    Private Function GetDefaultUserName(ByVal defaultUserid As Int64) As String
        Dim conn As New SqlConnection()
        Dim objCmd As SqlCommand
        Dim DefaultName As String = ""
        Dim _strSQL As String = ""

        Try
            conn.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()

            conn.Open()
            _strSQL = "select sLoginName from User_MST where nUserID = " & defaultUserid
            objCmd = New SqlCommand(_strSQL, conn)
            DefaultName = objCmd.ExecuteScalar()

            Return DefaultName

        Catch ex As Exception

            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return ""
        Finally
            conn.Close()

        
            If IsNothing(conn) = False Then
                conn.Dispose()
                conn = Nothing
            End If

        End Try
    End Function
#End Region

    'Retriving default Unique identifier for a Patient from database. 
#Region "RetrivingdefaultUniqueidentifierPatient"
    Private Sub FillUniqueidentifierPatient()
        Dim dt As New DataTable
        Try
            dt = GetUniqueidentifierPatient()
            If dt IsNot Nothing Then

                cmbUniqueIdentifierPatient.DataSource = dt
                cmbUniqueIdentifierPatient.ValueMember = dt.Columns(1).ColumnName
                cmbUniqueIdentifierPatient.DisplayMember = dt.Columns(0).ColumnName
                cmbUniqueIdentifierPatient.Refresh()
                cmbUniqueIdentifierPatient.SelectedIndex = dt.Rows.IndexOf(dt.Select("IsDefault = 1").FirstOrDefault())

            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function GetUniqueidentifierPatient(Optional ByVal IsDefault As Boolean = 0) As DataTable
        Try
            Dim objCon As New SqlConnection
            objCon.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()
            Dim objCmd As New SqlCommand
            Dim objDA As New SqlDataAdapter(objCmd)
            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "gsp_CCDA_UniquePatientIdentifier"
            objCmd.Connection = objCon
            objCon.Open()
            Dim opar As SqlParameter = New SqlParameter("@IsDefault", SqlDbType.Bit)
            objCmd.Parameters.Add(opar)
            With objCmd
                .Parameters("@IsDefault").Value = IsDefault
            End With
            Dim dsData As New DataSet
            objDA.Fill(dsData)
            objCon.Close()
            objCon.Dispose()
            objCon = Nothing
            Return dsData.Tables(0)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try
    End Function

    Private Function UpdateUniqueidentifierPatient() As Boolean
        Dim oDB As New gloDatabaseLayer.DBLayer(gloEMRAdmin.mdlGeneral.GetConnectionString())
        Dim oDBParameters As New gloDatabaseLayer.DBParameters()
        Try
            oDB.Connect(False)
            oDBParameters.Add("@sShortName", cmbUniqueIdentifierPatient.SelectedValue, System.Data.ParameterDirection.Input, System.Data.SqlDbType.NVarChar)
            oDB.Execute("gsp_UpdateUniqueidentifierPatient", oDBParameters)

            Return True
        Catch DBErr As gloDatabaseLayer.DBException
            Return False
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        Finally
            oDB.Disconnect()
            oDBParameters.Dispose()
            oDB.Dispose()
        End Try
    End Function
#End Region
    Private Sub oListControl_ItemSelectedClick(ByVal sender As Object, ByVal e As EventArgs)
        txtEmdeon_DefaultUser.Clear()


        If oListControl.SelectedItems.Count > 0 Then

            _gloLab_DefaultUserID = oListControl.SelectedItems(0).ID
            _gloLab_defaultUserName = oListControl.SelectedItems(0).Description.ToString()

            '_DefaultUserID = oListControl.SelectedItems(0).ID
            '_DefaultUserName = oListControl.SelectedItems(0).Description.ToString()

            txtEmdeon_DefaultUser.Text = oListControl.SelectedItems(0).Description.ToString() '(oListControl.SelectedItems(0).Code.ToString())

        End If
    End Sub
    Private Sub oListControl_ItemClosedClick(ByVal sender As Object, ByVal e As EventArgs)

    End Sub



    Private Sub frmSettings_New_LocationChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LocationChanged

    End Sub
#Region "Dhruv -> Checked for the fax setting checked or unchecked"
    Private Sub chkUseFaxNoPrefix_CheckedChanged_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkUseFaxNoPrefix.CheckedChanged
        If chkUseFaxNoPrefix.Checked = True Then
            txtFaxNoPrefix.Enabled = True
        Else
            txtFaxNoPrefix.Enabled = False
        End If

    End Sub
#End Region

   
    ''Added by Mayuri:20100608-Vital customization setting
    Private Sub btn_Up_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Up.Click
        Try
            Dim oNode As TreeNode
            Dim prevIndex As Integer



            If IsNothing(trvSelectedVitals.SelectedNode) = False Then
                oNode = trvSelectedVitals.SelectedNode.Clone
                If IsNothing(oNode) = False Then
                    ''Added by Sanjog:20100609-Vital customization setting
                    If IsNothing(trvSelectedVitals.SelectedNode.Parent) Then
                        If trvSelectedVitals.SelectedNode.Index <> 0 Then
                            prevIndex = trvSelectedVitals.SelectedNode.PrevNode.Index
                            trvSelectedVitals.Nodes.Remove(trvSelectedVitals.SelectedNode)
                            trvSelectedVitals.Nodes.Insert(prevIndex, oNode)
                            trvSelectedVitals.SelectedNode = oNode
                        End If
                    Else
                        If trvSelectedVitals.SelectedNode.Parent.Text <> "Obstetric History" Then
                            If trvSelectedVitals.SelectedNode.Index <> 0 Then
                                Dim PNode As TreeNode
                                PNode = trvSelectedVitals.SelectedNode.Parent
                                prevIndex = trvSelectedVitals.SelectedNode.PrevNode.Index
                                PNode.Nodes.Remove(trvSelectedVitals.SelectedNode)
                                PNode.Nodes.Insert(prevIndex, oNode)
                                trvSelectedVitals.SelectedNode = oNode
                                ''Added by Sanjog:20100609-Vital customization setting
                            End If
                        Else
                            MessageBox.Show("Rearranging Obstetric History is restricted.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If
                    End If
                End If
            End If
            trvSelectedVitals.ExpandAll()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    ''Added by Mayuri:20100608-Vital customization setting
    Private Sub btn_Down_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Down.Click
        Try
            Dim oNode As TreeNode
            Dim nextIndex As Integer

            If IsNothing(trvSelectedVitals.SelectedNode) = False Then
                oNode = trvSelectedVitals.SelectedNode.Clone
                If IsNothing(oNode) = False Then
                    ''Added by Sanjog:20100609-Vital customization setting
                    If IsNothing(trvSelectedVitals.SelectedNode.Parent) Then
                        If trvSelectedVitals.SelectedNode.Index <> trvSelectedVitals.Nodes.Count - 1 Then
                            nextIndex = trvSelectedVitals.SelectedNode.NextNode.Index
                            trvSelectedVitals.Nodes.Remove(trvSelectedVitals.SelectedNode)
                            trvSelectedVitals.Nodes.Insert(nextIndex, oNode)
                            trvSelectedVitals.SelectedNode = oNode
                        End If
                    Else
                        If trvSelectedVitals.SelectedNode.Parent.Text <> "Obstetric History" Then
                            If trvSelectedVitals.SelectedNode.Index <> trvSelectedVitals.SelectedNode.Parent.Nodes.Count - 1 Then
                                Dim PNode As TreeNode
                                PNode = trvSelectedVitals.SelectedNode.Parent
                                nextIndex = trvSelectedVitals.SelectedNode.NextNode.Index
                                PNode.Nodes.Remove(trvSelectedVitals.SelectedNode)
                                PNode.Nodes.Insert(nextIndex, oNode)
                                trvSelectedVitals.SelectedNode = oNode
                            End If
                            ''Added by Sanjog:20100609-Vital customization setting
                        Else
                            MessageBox.Show("Rearranging Obstetric History is restricted.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If
                    End If
                End If
            End If
            trvSelectedVitals.ExpandAll()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    ''Added by Sanjog:20100609-Vital customization setting
    Private Sub trvSelectedVitals_AfterCheck(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles trvSelectedVitals.AfterCheck
        Dim blnChecked As Boolean = False
        Dim _Node As New TreeNode
        _Node = e.Node

        If (_Node.Text = "Pulse OX" Or _Node.Text = "Pulse Ox w/Supplemental Oxygen") Then

            If (_Node.Text = "Pulse OX" And _Node.Checked = False) Then
                Dim _chiNode As New TreeNode
                Dim cntPulse As Integer = 0
                For Each _chiNode In _Node.Nodes
                    cntPulse = cntPulse + 1
                    _chiNode.Checked = False
                Next
                If (cntPulse <> 0) Then
                    blnChecked = True
                End If

            End If


            If (_Node.Text = "Pulse OX" And _Node.Checked = True) Then
                Dim _chiNode As New TreeNode
                Dim cntPulse As Integer = 0
                For Each _chiNode In _Node.Nodes
                    'If (_chiNode.Checked = True) Then
                    '    cntPulse = cntPulse + 1
                    'End If
                    cntPulse = cntPulse + 1
                    _chiNode.Checked = True
                Next
                If (cntPulse <> 0) Then
                    blnChecked = False
                Else
                    blnChecked = True
                End If

            End If


            If (_Node.Text = "Pulse Ox w/Supplemental Oxygen" And _Node.Checked = True) Then
                Dim _PNode As New TreeNode
                _PNode = e.Node.Parent
                If (_PNode.Text = "Pulse OX") Then
                    If (_PNode.Checked = False) Then
                        _PNode.Checked = True
                    End If

                    blnChecked = False
                End If
            End If

            If (_Node.Text = "Pulse Ox w/Supplemental Oxygen" And _Node.Checked = False) Then
                blnChecked = True
            End If


        Else

            If _Node.Checked = True And _Node.Text <> "Pulse OX" And _Node.Text <> "Pulse Ox w/Supplemental Oxygen" Then
                If _Node.Nodes.Count <> 0 Then
                    For Each _Node In _Node.Nodes

                        If (_Node.Text <> "Pulse OX" And _Node.Text <> "Pulse Ox w/Supplemental Oxygen") Then
                            _Node.Checked = True
                        End If


                    Next
                Else
                    If IsNothing(_Node.Parent) = False Then
                        Dim cnt As Integer = 0
                        For Each _Node In _Node.Parent.Nodes
                            If _Node.Checked = True Then
                                cnt = cnt + 1
                            End If
                            'blnChecked = True
                        Next
                        If cnt = _Node.Parent.Nodes.Count Then
                            Dim _PNode As New TreeNode
                            _PNode = e.Node.Parent
                            'flagChk = True

                            If _PNode.Checked = False Then
                                '_IsnodeUnchecked = True


                                _PNode.Checked = True
                            End If
                        End If
                    Else

                    End If
                End If
                'blnChecked = True
            Else
                If _Node.Nodes.Count <> 0 Then
                    If flagChk = False Then
                        For Each _Node In _Node.Nodes
                            _Node.Checked = False
                        Next
                    End If
                    flagChk = False
                Else
                    If IsNothing(_Node.Parent) = False Then
                        Dim cnt As Integer = 0
                        For Each _Node In _Node.Parent.Nodes
                            If _Node.Checked = True Then
                                cnt = cnt + 1
                            End If
                        Next
                        If cnt <> _Node.Parent.Nodes.Count Then
                            Dim _PNode As New TreeNode
                            _PNode = e.Node.Parent

                            If _PNode.Checked = True Then

                                flagChk = True
                                _PNode.Checked = False
                            End If
                        End If
                    Else

                    End If
                End If

            End If

        End If
        ''Added by Mayuri:20100626-To check or uncheck select all checkbox after check/uncheck nodes in treeview
        Dim nCount As Int16
        Dim nTotalNodes As Int16
        nTotalNodes = trvSelectedVitals.Nodes.Count - 1
        Dim ncheckedcnt As Int16
        For nCount = 0 To nTotalNodes
            If (trvSelectedVitals.Nodes(nCount).Checked = True) Then
                ncheckedcnt = ncheckedcnt + 1
            End If
        Next
        If ncheckedcnt = nTotalNodes + 1 Then
            If (blnChecked = True) Then
                _blnSelectAll = False
                chkSelectAll.Checked = False
            Else
                _blnSelectAll = True
                chkSelectAll.Checked = True
            End If

        Else
            _blnSelectAll = False
            chkSelectAll.Checked = False
        End If
        ''End code Added by Mayuri:20100626








    End Sub

    ''Added by Sanjog:20100609-Vital customization setting
    Private Sub ShowVital_InTrv(ByVal SelectedV() As String)
        Dim oNode As TreeNode
        Dim oChildNode As TreeNode
        trvSelectedVitals.Nodes.Clear()
        trvSelectedVitals.CheckBoxes = True
        oNode = New TreeNode
        oNode.Text = "Height/Length"
        oNode.Tag = "Height"
        oChildNode = New TreeNode
        oChildNode.Text = "Height/Length (ft & in)"
        oChildNode.Tag = "Height (ft & in)"
        oNode.Nodes.Add(oChildNode)

        oChildNode = New TreeNode
        oChildNode.Text = "Height/Length (in)"
        oChildNode.Tag = "Height (in)"
        oNode.Nodes.Add(oChildNode)

        oChildNode = New TreeNode
        oChildNode.Text = "Height/Length (cm)"
        oChildNode.Tag = "Height (cm)"
        oNode.Nodes.Add(oChildNode)

        trvSelectedVitals.Nodes.Add(oNode)
        oNode = New TreeNode
        oNode.Text = "Weight"
        oNode.Tag = "Weight"

        oChildNode = New TreeNode
        oChildNode.Text = "Weight (lbsoz)"
        oChildNode.Tag = "Weight (lbsoz)"
        oNode.Nodes.Add(oChildNode)

        oChildNode = New TreeNode
        oChildNode.Text = "Weight (lbs)"
        oChildNode.Tag = "Weight (lbs)"
        oNode.Nodes.Add(oChildNode)

        oChildNode = New TreeNode
        oChildNode.Text = "Weight (kg)"
        oChildNode.Tag = "Weight (kg)"
        oNode.Nodes.Add(oChildNode)

        trvSelectedVitals.Nodes.Add(oNode)

        oNode = New TreeNode
        oNode.Text = "Weight Change"
        oNode.Tag = "Weight Change"
        trvSelectedVitals.Nodes.Add(oNode)

        oNode = New TreeNode
        oNode.Text = "BMI"
        oNode.Tag = "BMI"
        trvSelectedVitals.Nodes.Add(oNode)

        oNode = New TreeNode
        oNode.Text = "Respiratory Rate"
        oNode.Tag = "Respiratory Rate"
        trvSelectedVitals.Nodes.Add(oNode)

        oNode = New TreeNode
        oNode.Text = "Pulse Per Min"
        oNode.Tag = "Pulse Per Min"
        trvSelectedVitals.Nodes.Add(oNode)

        oNode = New TreeNode
        oNode.Text = "Pulse OX"
        oNode.Tag = "Pulse OX"

        oChildNode = New TreeNode
        oChildNode.Text = "Pulse Ox w/Supplemental Oxygen"
        oChildNode.Tag = "Pulse Ox w/Supplemental Oxygen"
        oNode.Nodes.Add(oChildNode)

        trvSelectedVitals.Nodes.Add(oNode)

        oNode = New TreeNode
        oNode.Text = "BP Setting"
        oNode.Tag = "BP Setting"

        oChildNode = New TreeNode
        oChildNode.Text = "BP Sitting"
        oChildNode.Tag = "BP Sitting"
        oNode.Nodes.Add(oChildNode)

        oChildNode = New TreeNode
        oChildNode.Text = "BP Standing"
        oChildNode.Tag = "BP Standing"
        oNode.Nodes.Add(oChildNode)

        trvSelectedVitals.Nodes.Add(oNode)

        oNode = New TreeNode
        oNode.Text = "Temperature"
        oNode.Tag = "Temperature"

        oChildNode = New TreeNode
        oChildNode.Text = "Temperature (F)"
        oChildNode.Tag = "Temperature (F)"
        oNode.Nodes.Add(oChildNode)

        oChildNode = New TreeNode
        oChildNode.Text = "Temperature (C)"
        oChildNode.Tag = "Temperature (C)"
        oNode.Nodes.Add(oChildNode)

        trvSelectedVitals.Nodes.Add(oNode)

        oNode = New TreeNode
        oNode.Text = "PEFR"
        oNode.Tag = "PEFR"
        trvSelectedVitals.Nodes.Add(oNode)

        oNode = New TreeNode
        oNode.Text = "Last Menstrual Period"
        oNode.Tag = "Last Menstrual Period"
        trvSelectedVitals.Nodes.Add(oNode)

        oNode = New TreeNode
        oNode.Text = "Head Circumference"
        oNode.Tag = "Head Circumference"

        oChildNode = New TreeNode
        oChildNode.Text = "Head Circumference (in)"
        oChildNode.Tag = "Head Circumference (in)"
        oNode.Nodes.Add(oChildNode)

        oChildNode = New TreeNode
        oChildNode.Text = "Head Circumference (cm)"
        oChildNode.Tag = "Head Circumference (cm)"
        oNode.Nodes.Add(oChildNode)


        trvSelectedVitals.Nodes.Add(oNode)
        ''Added by Sanjog:20100612-Add new Vital

        oNode = New TreeNode
        oNode.Text = "Neck Circumference"
        oNode.Tag = "Neck Circumference"

        oChildNode = New TreeNode
        oChildNode.Text = "Neck Circumference (in)"
        oChildNode.Tag = "Neck Circumference (in)"
        oNode.Nodes.Add(oChildNode)

        oChildNode = New TreeNode
        oChildNode.Text = "Neck Circumference (cm)"
        oChildNode.Tag = "Neck Circumference (cm)"
        oNode.Nodes.Add(oChildNode)

        trvSelectedVitals.Nodes.Add(oNode)
        ''Added by Sanjog:20100612-Add new Vital

        oNode = New TreeNode
        oNode.Text = "Stature"
        oNode.Tag = "Stature"

        oChildNode = New TreeNode
        oChildNode.Text = "Stature (in)"
        oChildNode.Tag = "Stature (in)"
        oNode.Nodes.Add(oChildNode)

        oChildNode = New TreeNode
        oChildNode.Text = "Stature (cm)"
        oChildNode.Tag = "Stature (cm)"
        oNode.Nodes.Add(oChildNode)

        trvSelectedVitals.Nodes.Add(oNode)
        ''Added by Sanjog:20100612-Add new Vital
        oNode = New TreeNode
        oNode.Text = "Left Eye Pressure Over Time"
        oNode.Tag = "Left Eye Pressure Over Time"
        trvSelectedVitals.Nodes.Add(oNode)

        oNode = New TreeNode
        oNode.Text = "Right Eye Pressure Over Time"
        oNode.Tag = "Right Eye Pressure Over Time"
        trvSelectedVitals.Nodes.Add(oNode)
        ''Added by Sanjog:20100612- Add new Vital
        oNode = New TreeNode
        oNode.Text = "Heart Rate"
        oNode.Tag = "Heart Rate"
        trvSelectedVitals.Nodes.Add(oNode)

        oNode = New TreeNode
        oNode.Text = "Pain Level"
        oNode.Tag = "Pain Level"

        ''Added by Mayuri:20101227
        oChildNode = New TreeNode

        oChildNode.Text = "Pain Level : Current"
        oChildNode.Tag = "Pain Level : Current"
        oNode.Nodes.Add(oChildNode)


        oChildNode = New TreeNode
        oChildNode.Text = "Pain Level : With Medication"
        oChildNode.Tag = "Pain Level : With Medication"
        oNode.Nodes.Add(oChildNode)

        oChildNode = New TreeNode
        oChildNode.Text = "Pain Level : Without Medication"
        oChildNode.Tag = "Pain Level : Without Medication"
        oNode.Nodes.Add(oChildNode)

        oChildNode = New TreeNode
        oChildNode.Text = "Pain Level : Worst"
        oChildNode.Tag = "Pain Level : Worst"
        oNode.Nodes.Add(oChildNode)
        trvSelectedVitals.Nodes.Add(oNode)




        oNode = New TreeNode
        oNode.Text = "Obstetric History"
        oNode.Tag = "Obstetric History"

        oChildNode = New TreeNode

        oChildNode.Text = "Total Pregnancies"
        oChildNode.Tag = "Total Pregnancies"
        oNode.Nodes.Add(oChildNode)

        oChildNode = New TreeNode
        oChildNode.Text = "Living"
        oChildNode.Tag = "Living"
        oNode.Nodes.Add(oChildNode)

        oChildNode = New TreeNode
        oChildNode.Text = "Full Term"
        oChildNode.Tag = "Full Term"
        oNode.Nodes.Add(oChildNode)

        oChildNode = New TreeNode
        oChildNode.Text = "Multiple Births"
        oChildNode.Tag = "Multiple Births"
        oNode.Nodes.Add(oChildNode)

        oChildNode = New TreeNode
        oChildNode.Text = "Premature"
        oChildNode.Tag = "Premature"
        oNode.Nodes.Add(oChildNode)

        oChildNode = New TreeNode
        oChildNode.Text = "Aborted (Spontaneous)"
        oChildNode.Tag = "Aborted (Spontaneous)"
        oNode.Nodes.Add(oChildNode)

        oChildNode = New TreeNode
        oChildNode.Text = "Aborted (Induced)"
        oChildNode.Tag = "Aborted (Induced)"
        oNode.Nodes.Add(oChildNode)

        oChildNode = New TreeNode
        oChildNode.Text = "Ectopic"
        oChildNode.Tag = "Ectopic"
        oNode.Nodes.Add(oChildNode)

        trvSelectedVitals.Nodes.Add(oNode)


        oNode = New TreeNode
        oNode.Text = "Past Pregnancies"
        oNode.Tag = "Past Pregnancies"
        trvSelectedVitals.Nodes.Add(oNode)
        'Added for Removing Past Pregnancies
        If (oNode.Text = "Past Pregnancies") Then
            trvSelectedVitals.Nodes.Remove(oNode)
        End If

        oNode = New TreeNode
        oNode.Text = "ODI"
        oNode.Tag = "ODI"
        trvSelectedVitals.Nodes.Add(oNode)
        ''

        oNode = New TreeNode
        oNode.Text = "Comments"
        oNode.Tag = "Comments"
        trvSelectedVitals.Nodes.Add(oNode)

        oNode = New TreeNode
        oNode.Text = "DAS 28"
        oNode.Tag = "DAS 28"
        trvSelectedVitals.Nodes.Add(oNode)

        oNode = New TreeNode
        oNode.Text = "OB Vitals"
        oNode.Tag = "OB Vitals"
        trvSelectedVitals.Nodes.Add(oNode)

        ''Added by Sanjog:20100610-Vital customization setting
        If IsNothing(SelectedV) = False Then
            If SelectedV.Length <> 0 Then
                Dim j As Integer
                Dim m As Integer
                Dim n As Integer
                Dim NodeNo() As String
                Dim RootChild() As String
                Dim CloneNode As TreeNode
                Dim CloneNode1 As TreeNode





                Try

                
                For j = 0 To SelectedV.Length - 1
                    NodeNo = SelectedV.GetValue(j).ToString().Split("-")
                    RootChild = NodeNo.GetValue(0).ToString().Split(".")
                    For m = 0 To trvSelectedVitals.Nodes.Count - 1
                        If trvSelectedVitals.Nodes(m).Tag = NodeNo.GetValue(1) Then
                            trvSelectedVitals.Nodes(m).Checked = True
                        End If
                        If trvSelectedVitals.Nodes(m).Nodes.Count <> 0 Then
                            If NodeNo.GetValue(1).ToString().Trim() = "Pain Level" Then
                                For val As Int16 = 0 To trvSelectedVitals.Nodes.Count - 1
                                    If trvSelectedVitals.Nodes(val).Tag = "Pain Level" Then
                                        trvSelectedVitals.Nodes(val).Nodes(0).Checked = True
                                        trvSelectedVitals.Nodes(val).Nodes(1).Checked = False
                                        trvSelectedVitals.Nodes(val).Nodes(2).Checked = False
                                        trvSelectedVitals.Nodes(val).Nodes(3).Checked = False
                                    End If
                                Next
                            Else
                            End If


                            For n = 0 To trvSelectedVitals.Nodes(m).Nodes.Count - 1
                                If trvSelectedVitals.Nodes(m).Nodes(n).Tag = NodeNo.GetValue(1) Then
                                    trvSelectedVitals.Nodes(m).Nodes(n).Checked = True

                                    If m = RootChild.GetValue(0) Then
                                        ''root node is right
                                        If n = RootChild.GetValue(1) - 1 Then
                                            ''mean node is on correct position
                                        Else
                                            CloneNode = trvSelectedVitals.Nodes(m).Nodes(Convert.ToInt16(RootChild.GetValue(1) - 1)).Clone()
                                            CloneNode1 = trvSelectedVitals.Nodes(m).Nodes(n).Clone()
                                            trvSelectedVitals.Nodes(m).Nodes.Remove(trvSelectedVitals.Nodes(m).Nodes(n))
                                            trvSelectedVitals.Nodes(m).Nodes.Insert(n, CloneNode)
                                            trvSelectedVitals.Nodes(m).Nodes.Remove(trvSelectedVitals.Nodes(m).Nodes(Convert.ToInt16(RootChild.GetValue(1) - 1)))
                                            trvSelectedVitals.Nodes(m).Nodes.Insert(Convert.ToInt16(RootChild.GetValue(1) - 1), CloneNode1)
                                            Exit For
                                            'trvSelectedVitals.SelectedNode = oNode
                                        End If
                                    Else

                                        If n = RootChild.GetValue(1) - 1 Then
                                            ''mean node is on correct position
                                        Else
                                            CloneNode = trvSelectedVitals.Nodes(m).Nodes(Convert.ToInt16(RootChild.GetValue(1) - 1)).Clone()
                                            CloneNode1 = trvSelectedVitals.Nodes(m).Nodes(n).Clone()
                                            trvSelectedVitals.Nodes(m).Nodes.Remove(trvSelectedVitals.Nodes(m).Nodes(n))
                                            trvSelectedVitals.Nodes(m).Nodes.Insert(n, CloneNode)
                                            trvSelectedVitals.Nodes(m).Nodes.Remove(trvSelectedVitals.Nodes(m).Nodes(Convert.ToInt16(RootChild.GetValue(1) - 1)))
                                            trvSelectedVitals.Nodes(m).Nodes.Insert(Convert.ToInt16(RootChild.GetValue(1) - 1), CloneNode1)
                                            'trvSelectedVitals.SelectedNode = oNode
                                        End If

                                        CloneNode = trvSelectedVitals.Nodes(Convert.ToInt16(RootChild.GetValue(0))).Clone()
                                        'delNode = trvSelectedVitals.Nodes(m).Nodes(Convert.ToInt16(RootChild.GetValue(1) - 1)).Clone()
                                        CloneNode1 = trvSelectedVitals.Nodes(m).Clone()
                                        trvSelectedVitals.Nodes.Remove(trvSelectedVitals.Nodes(m))
                                        trvSelectedVitals.Nodes.Insert(m, CloneNode)
                                        trvSelectedVitals.Nodes.Remove(trvSelectedVitals.Nodes(Convert.ToInt16(RootChild.GetValue(0))))
                                        trvSelectedVitals.Nodes.Insert(Convert.ToInt16(RootChild.GetValue(0)), CloneNode1)
                                        Exit For
                                    End If
                                End If
                            Next
                        Else
                            ''
                            If RootChild.GetValue(1) = 0 And trvSelectedVitals.Nodes(m).Tag = NodeNo.GetValue(1) Then
                                    'Added for Removing Past Pregnancies
                                    If m = RootChild.GetValue(0) - 1 Then
                                        ''root is on correct position
                                    Else

                                        CloneNode = trvSelectedVitals.Nodes(Convert.ToInt16(RootChild.GetValue(0))).Clone()
                                        CloneNode1 = trvSelectedVitals.Nodes(m).Clone()
                                        trvSelectedVitals.Nodes.Remove(trvSelectedVitals.Nodes(m))
                                        trvSelectedVitals.Nodes.Insert(m, CloneNode)
                                        trvSelectedVitals.Nodes.Remove(trvSelectedVitals.Nodes(Convert.ToInt16(RootChild.GetValue(0))))
                                        trvSelectedVitals.Nodes.Insert(Convert.ToInt16(RootChild.GetValue(0)), CloneNode1)
                                        Exit For


                                    End If
                            End If
                        End If
                    Next
                Next
                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
                End Try
            End If
        End If


        Dim count As Integer
        Dim CountPulse As Integer = 0
        Dim countPul As Integer
        Dim m1 As Integer


        If (Not IsNothing(SelectedV)) Then
            If (SelectedV.Length > 0) Then
                For count = 0 To SelectedV.Length - 1
                    If (SelectedV(count).ToString().Contains("Pulse Ox w/Supplemental Oxygen")) Then
                        CountPulse = 1
                        Exit For
                    End If
                Next


                For m1 = 0 To trvSelectedVitals.Nodes.Count - 1
                    For countPul = 0 To trvSelectedVitals.Nodes(m1).Nodes.Count - 1
                        If (trvSelectedVitals.Nodes(m1).Text = "Pulse OX") Then
                            If (CountPulse = 1) Then
                                trvSelectedVitals.Nodes(m1).Nodes(countPul).Checked = True
                            Else
                                trvSelectedVitals.Nodes(m1).Nodes(countPul).Checked = False
                            End If
                        End If
                    Next
                Next

            End If
        End If
        ''Added by Sanjog:20100610-Vital customization setting
        trvSelectedVitals.ExpandAll()
    End Sub
    ''Added by Sanjog:20100609-Vital customization setting

    ''Added by Sanjog:20100614-for reset setting

    Private Sub btn_Reset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Reset.Click
        Dim str_nothing() As String
        ShowVital_InTrv(str_nothing)
        Select_all()
    End Sub
    Private Sub Select_all()
        For i As Integer = 0 To trvSelectedVitals.Nodes.Count - 1
            trvSelectedVitals.Nodes(i).Checked = True
        Next
    End Sub
    ''Added by Sanjog:20100615-for reset setting

    ''Added by Sanjog:20100614-for reset setting
    Private Sub rbt_EMYes_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbt_EMYes.CheckedChanged
        If rbt_EMYes.Checked = True Then
            rbt_EMYes.Font = New Font("Tahoma", 9, FontStyle.Bold)
            gp_PatientType.Enabled = True
            Panel102.Enabled = True
            GroupBox71.Enabled = True
        Else
            rbt_EMYes.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub rbt_EMNo_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbt_EMNo.CheckedChanged
        If rbt_EMNo.Checked = True Then
            rbt_EMNo.Font = New Font("Tahoma", 9, FontStyle.Bold)
            ''Enable all details
            gp_PatientType.Enabled = False
            Panel102.Enabled = False
            GroupBox71.Enabled = False
        Else
            rbt_EMNo.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub
    ''Added by Sanjog:20100615-for reset setting
#Region "Select All and Deselect All"
    ''Added by Mayuri:20100621-Added selectAll and DeselectAll facility
    'Private Sub btnSelectAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelectAll.Click
    '    Try
    '        If btnSelectAll.Text = "Select All" Then
    '            SelectAll()
    '            btnSelectAll.Text = "Clear All"
    '        ElseIf btnSelectAll.Text = "Clear All" Then
    '            ClearAll()
    '            btnSelectAll.Text = "Select All"
    '        End If
    '    Catch ex As Exception

    '    End Try
    'End Sub

    Private Sub SelectAll()
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim nCount As Int16
            Dim nTotalNodes As Int16
            Dim n As Int16

            nTotalNodes = trvSelectedVitals.GetNodeCount(False) - 1
            For nCount = 0 To nTotalNodes
                If (trvSelectedVitals.Nodes(nCount).Text = "Pulse OX") Then
                    For n = 0 To trvSelectedVitals.Nodes(nCount).Nodes.Count - 1
                        If trvSelectedVitals.Nodes(nCount).Nodes(0).Text = "Pulse Ox w/Supplemental Oxygen" Then
                            trvSelectedVitals.Nodes(nCount).Nodes(0).Checked = True
                            trvSelectedVitals.Nodes(nCount).Checked = True
                        End If
                    Next
                End If
            Next


            'nTotalNodes = trvSelectedVitals.GetNodeCount(False) - 1
            For nCount = 0 To nTotalNodes
                trvSelectedVitals.Nodes(nCount).Checked = True
            Next
            '_blnSelectAll = True
            '_blnClearAll = False
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ClearAll()
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim nCount As Int16
            Dim nTotalNodes As Int16
            nTotalNodes = trvSelectedVitals.GetNodeCount(False) - 1
            For nCount = 0 To nTotalNodes
                trvSelectedVitals.Nodes(nCount).Checked = False
            Next
            '_blnClearAll = True
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub chkSelectAll_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkSelectAll.CheckedChanged
        Try
            If chkSelectAll.Checked = True Then
                'If _Isnodechecked = False Then
                _blnSelectAll = True
                SelectAll()
                'End If


            Else
                If _blnSelectAll = True Then
                    ClearAll()
                End If


            End If
        Catch ex As Exception

        End Try
    End Sub
#End Region



    Private Sub btn_Up_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Up.MouseHover
        Dim tooltip1 As New System.Windows.Forms.ToolTip
        tooltip1.SetToolTip(Me.btn_Up, "Move Up")
    End Sub

    Private Sub btn_Down_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Down.MouseHover
        Dim tooltip1 As New System.Windows.Forms.ToolTip
        tooltip1.SetToolTip(Me.btn_Down, "Move Down")
    End Sub

    Private Sub btn_Reset_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Reset.MouseHover
        Dim tooltip1 As New System.Windows.Forms.ToolTip
        tooltip1.SetToolTip(Me.btn_Reset, "Reset")
    End Sub

    Private Sub tbpg_VitalsCustomizationSettings_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbpg_VitalsCustomizationSettings.Click

    End Sub

    Private Sub tmEndTime_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmEndTime.ValueChanged

    End Sub

    Private Sub txtServerPath_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtServerPath.TextChanged

    End Sub

#Region "ClinicalChart Customization"

    Private Sub SelectAll_ClearAll_ClinicalChart(ByVal sPara As String)
        Try
            If String.Compare(sPara, "select") = 0 Then
                Me.Cursor = Cursors.WaitCursor
                Dim nCount As Int16
                Dim nTotalNodes As Int16
                nTotalNodes = trvClinicalChart.GetNodeCount(False) - 1
                For nCount = 0 To nTotalNodes
                    trvClinicalChart.Nodes(nCount).Checked = True
                Next
                Me.Cursor = Cursors.Default
            Else
                Me.Cursor = Cursors.WaitCursor
                Dim nCount As Int16
                Dim nTotalNodes As Int16
                nTotalNodes = trvClinicalChart.GetNodeCount(False) - 1
                For nCount = 0 To nTotalNodes
                    trvClinicalChart.Nodes(nCount).Checked = False
                Next
                Me.Cursor = Cursors.Default
            End If

        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ShowClinicalChart(ByVal strClinicalChart() As String)

        Dim oNode As TreeNode
        trvClinicalChart.Nodes.Clear()
        trvClinicalChart.CheckBoxes = True
        Dim _sClinicalchartValue() As String
        Dim iArrayIndex As Integer



        If IsNothing(strClinicalChart) Then
            Dim CCType As enumDefaultClinicalChart
            For Each CCType In [Enum].GetValues(GetType(enumDefaultClinicalChart))
                oNode = New TreeNode
                oNode.Text = Replace(CCType.ToString(), "_", " ")
                oNode.Tag = Replace(CCType.ToString(), "_", " ")
                trvClinicalChart.Nodes.Add(oNode)
            Next
            chk_CC_SelectAll.Checked = True
            Exit Sub
        End If
        Dim flgSelectall As Boolean = True

        For iArrayIndex = 0 To strClinicalChart.Length - 1

            _sClinicalchartValue = strClinicalChart.GetValue(iArrayIndex).ToString().Split(".")

            oNode = New TreeNode
            oNode.Text = _sClinicalchartValue(1)
            oNode.Tag = _sClinicalchartValue(1)
            trvClinicalChart.Nodes.Add(oNode)
            If _sClinicalchartValue(0).ToString().Trim = "C" Then
                oNode.Checked = True
            Else
                flgSelectall = False
                oNode.Checked = False
            End If
            If flgSelectall = True Then
                chk_CC_SelectAll.Checked = True
            Else
                chk_CC_SelectAll.Checked = False
            End If

        Next


    End Sub


#End Region



    Private Sub ChkUseSitePrefix_CheckStateChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkUseSitePrefix.CheckStateChanged
        If ChkUseSitePrefix.CheckState = CheckState.Unchecked Then

            Dim strmessage As String
            strmessage = "You are about to disable Site Prefix setting.Changing the setting could have unpredictable results while replication. It is recommended to enable this setting if you are using replication. Still do you wish to proceed?"
            If MessageBox.Show(strmessage, gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information) = Windows.Forms.DialogResult.No Then
                ChkUseSitePrefix.CheckState = CheckState.Checked
            Else

                flagPrefixSettingOFF = False
            End If
        Else

            flagPrefixSettingOFF = True
        End If
    End Sub

    Private Sub txtReportServerName_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtReportServerName.KeyPress, txtMMWServerName.KeyPress
        If Regex.IsMatch(e.KeyChar, "^[-a-zA-Z0-9\:\b\s\._]") = False And e.KeyChar = "''c" Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtReportFolderName_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtReportFolderName.KeyPress
        If Regex.IsMatch(e.KeyChar, "^[-a-zA-Z0-9\b\s._]") = False And e.KeyChar = "''c" Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtReportVirtualDir_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtReportVirtualDir.KeyPress
        If Regex.IsMatch(e.KeyChar, "^[-a-zA-Z0-9_\b]") = False And e.KeyChar = "''c" Then
            e.Handled = True
        End If
    End Sub





    Private Sub optSMWINAuthentication_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optSMWINAuthentication.CheckedChanged
        If txtSMSQLUserID.Enabled = False Then
            ' txtSMSQLUserID.Text = ""
            ' txtSMSQLPassword.Text = ""

        End If
        txtSMSQLUserID.Enabled = False
        txtSMSQLPassword.Enabled = False
        If optSMWINAuthentication.Checked = True Then
            optSMWINAuthentication.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            optSMWINAuthentication.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If

    End Sub

    Private Sub optSMSQLAuthentication_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optSMSQLAuthentication.CheckedChanged
        txtSMSQLUserID.Enabled = True
        txtSMSQLPassword.Enabled = True
        If optSMSQLAuthentication.Checked = True Then
            optSMSQLAuthentication.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            optSMSQLAuthentication.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If

    End Sub

    Private Sub optRxNWINAuthentication_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optRxNWINAuthentication.CheckedChanged
        If txtRxNSQLUserID.Enabled = False Then
            'txtRxNSQLUserID.Text = ""
            ' txtRxNSQLPassword.Text = ""
        End If
        txtRxNSQLUserID.Enabled = False
        txtRxNSQLPassword.Enabled = False
        If optRxNWINAuthentication.Checked = True Then
            optRxNWINAuthentication.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            optRxNWINAuthentication.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If

    End Sub

    Private Sub optRxNSQLAuthentication_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optRxNSQLAuthentication.CheckedChanged
        txtRxNSQLUserID.Enabled = True
        txtRxNSQLPassword.Enabled = True
        If optRxNSQLAuthentication.Checked = True Then
            optRxNSQLAuthentication.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            optRxNSQLAuthentication.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub optSrvcWINAuthentication_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optSrvcWINAuthentication.CheckedChanged

        txtSrvcSQLUserID.Enabled = False
        txtSrvcSQLPassword.Enabled = False
        If optSrvcWINAuthentication.Checked = True Then
            optSrvcWINAuthentication.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            optSrvcWINAuthentication.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If

    End Sub

    Private Sub optSrvcSQLAuthentication_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optSrvcSQLAuthentication.CheckedChanged

        txtSrvcSQLUserID.Enabled = True
        txtSrvcSQLPassword.Enabled = True
        If optSrvcSQLAuthentication.Checked = True Then
            optSrvcSQLAuthentication.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            optSrvcSQLAuthentication.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If

    End Sub

    Private Sub btnSnodb_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSnodb.Click
        Dim objSQLSettings As New clsStartUpSettings
        'Dim str = ""
        'If (gstrAdminFor = "gloPM") Then
        '    str = "gloEMR"
        'Else
        '    str = "gloPM"
        'End If

        If txtSMServerName.Text.Trim = "" Then
            MessageBox.Show("Enter SnoMed database settings Server Name.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            txtSMServerName.Focus()
            Exit Sub
        End If

        If txtSMDatabaseName.Text.Trim = "" Then
            MessageBox.Show("Enter SnoMed database settings Database Name.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            txtSMDatabaseName.Focus()
            Exit Sub
        End If

        If optSMSQLAuthentication.Checked = True Then
            If txtSMSQLUserID.Text.Trim = "" Then
                MessageBox.Show("Enter SnoMed database settings SQL User ID", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtSMSQLUserID.Focus()
                Exit Sub
            End If

            If txtSMSQLPassword.Text.Trim = "" Then
                MessageBox.Show("Enter SnoMed database settings SQL Password", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtSMSQLPassword.Focus()
                Exit Sub
            End If

            If objSQLSettings.IsSQLConnect(txtSMServerName.Text.Trim, txtSMDatabaseName.Text.Trim, txtSMSQLUserID.Text.Trim, txtSMSQLPassword.Text.Trim) = False Then
                If MessageBox.Show("Unable to connect to SnoMed database settings SQL Server " & txtSMServerName.Text.Trim & " and Database " & txtSMDatabaseName.Text.Trim & vbCrLf & "Select valid credentials.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information) Then

                    Exit Sub
                End If
            Else
                MessageBox.Show("Connection established successfully.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If
        Else
            If objSQLSettings.IsConnect(txtSMServerName.Text.Trim, txtSMDatabaseName.Text.Trim, False, "", "") = False Then
                If MessageBox.Show("Unable to connect to SnoMed database settings SQL Server " & txtSMServerName.Text.Trim & " and Database " & txtSMDatabaseName.Text.Trim & vbCrLf & "Select valid credentials.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information) Then
                    Exit Sub
                End If
            Else

                MessageBox.Show("Connection established successfully.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If
        End If

    End Sub

    Private Sub btnRxNormdb_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRxNormdb.Click
        Dim objSQLSettings As New clsStartUpSettings

        If txtRxNServerName.Text.Trim = "" Then
            MessageBox.Show("Enter RxNorm database settings Server Name.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            txtRxNServerName.Focus()
            Exit Sub
        End If

        If txtRxNDatabaseName.Text.Trim = "" Then
            MessageBox.Show("Enter RxNorm database settings Database Name.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            txtRxNDatabaseName.Focus()
            Exit Sub
        End If

        If optRxNSQLAuthentication.Checked = True Then
            If txtRxNSQLUserID.Text.Trim = "" Then
                MessageBox.Show("Enter RxNorm database settings SQL User ID", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtRxNSQLUserID.Focus()
                Exit Sub
            End If

            If txtRxNSQLPassword.Text.Trim = "" Then
                MessageBox.Show("Enter RxNorm database settings SQL Password", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtRxNSQLPassword.Focus()
                Exit Sub
            End If

            If objSQLSettings.IsSQLConnect(txtRxNServerName.Text.Trim, txtRxNDatabaseName.Text.Trim, txtRxNSQLUserID.Text.Trim, txtRxNSQLPassword.Text.Trim) = False Then
                If MessageBox.Show("Unable to connect to RxNorm database settings SQL Server " & txtRxNServerName.Text.Trim & " and Database " & txtRxNDatabaseName.Text.Trim & vbCrLf & "Select valid credentials.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information) Then
                    Exit Sub
                End If
            Else
                MessageBox.Show("Connection established successfully.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If
        Else
            If objSQLSettings.IsConnect(txtRxNServerName.Text.Trim, txtRxNDatabaseName.Text.Trim, False, "", "") = False Then
                If MessageBox.Show("Unable to connect to RxNorm database settings SQL Server " & txtRxNServerName.Text.Trim & " and Database " & txtRxNDatabaseName.Text.Trim & vbCrLf & "Select valid credentials.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information) Then
                    Exit Sub
                End If
            Else
                MessageBox.Show("Connection established successfully.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If
        End If
    End Sub

    Private Sub trvsnomed_BeforeCheck(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewCancelEventArgs) Handles trvsnomed.BeforeCheck

    End Sub

    Private Sub trvsnomed_AfterCheck(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles trvsnomed.AfterCheck


    End Sub

    Private Sub trvsnomed_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles trvsnomed.Click


        'If node.Checked = True Then
        '    For i As Integer = 0 To trvsnomed.Nodes.Count - 1
        '        If trvsnomed.Nodes(i).Checked = True Then
        '            If trvsnomed.Nodes(i).Text <> node.Text Then
        '                trvsnomed.Nodes(i).Checked = False
        '            End If
        '        End If
        '    Next
        'End If
    End Sub

    ' - Added By Rahul Patel on 21-10-2010   -'
    Private Sub btnMMWDb_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMMWDb.Click
        Dim objSQLSettings As New clsStartUpSettings

        If txtMMWServerName.Text.Trim = "" Then
            MessageBox.Show("Enter Drug database settings Server Name.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            txtMMWServerName.Focus()
            Exit Sub
        End If
        If txtMMWDatabaseName.Text.Trim = "" Then
            MessageBox.Show("Enter Drug database settings Database Name.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            txtMMWDatabaseName.Focus()
            Exit Sub
        End If
        If optMMWSqlAuthentication.Checked = True Then
            If txtMMWUserName.Text.Trim = "" Then
                MessageBox.Show("Enter Drug database settings SQL User ID", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtMMWUserName.Focus()
                Exit Sub
            End If
            If txtMMWPassword.Text.Trim = "" Then
                MessageBox.Show("Enter Drug database settings SQL Password", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtMMWPassword.Focus()
                Exit Sub
            End If
            If objSQLSettings.IsSQLConnect(txtMMWServerName.Text.Trim, txtMMWDatabaseName.Text.Trim, txtMMWUserName.Text.Trim, txtMMWPassword.Text.Trim) = False Then
                If MessageBox.Show("Unable to connect to Drug database settings SQL Server " & txtMMWServerName.Text.Trim & " and Database " & txtMMWDatabaseName.Text.Trim & vbCrLf & "Select valid credentials.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information) Then
                    Exit Sub
                End If
            Else
                MessageBox.Show("Connection established successfully.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If
        Else
            If objSQLSettings.IsConnect(txtMMWServerName.Text.Trim, txtMMWDatabaseName.Text.Trim, False, "", "") = False Then
                If MessageBox.Show("Unable to connect to Drug database settings SQL Server " & txtMMWServerName.Text.Trim & " and Database " & txtMMWDatabaseName.Text.Trim & vbCrLf & "Select valid credentials.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information) Then
                    Exit Sub
                End If
            Else
                MessageBox.Show("Connection established successfully.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If
        End If
    End Sub

    'added by nilesh on 20101025
    Private Sub btngloHL7Connect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btngloHL7Connect.Click
        Dim objSQLSettings As New clsStartUpSettings

        If txtgloHL7ServerName.Text.Trim = "" Then
            MessageBox.Show("Enter HL7 database settings Server Name.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            txtgloHL7ServerName.Focus()
            Exit Sub
        End If
        If txtgloHL7DatabaseName.Text.Trim = "" Then
            MessageBox.Show("Enter HL7 database settings Database Name.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            txtgloHL7DatabaseName.Focus()
            Exit Sub
        End If
        If optgloHL7SqlAuthentication.Checked = True Then
            If txtgloHL7UserID.Text.Trim = "" Then
                MessageBox.Show("Enter HL7 database settings SQL User ID", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtgloHL7UserID.Focus()
                Exit Sub
            End If
            If txtgloHL7Password.Text.Trim = "" Then
                MessageBox.Show("Enter HL7 database settings SQL Password", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtgloHL7Password.Focus()
                Exit Sub
            End If
            If objSQLSettings.IsSQLConnect(txtgloHL7ServerName.Text.Trim, txtgloHL7DatabaseName.Text.Trim, txtgloHL7UserID.Text.Trim, txtgloHL7Password.Text.Trim) = False Then
                If MessageBox.Show("Unable to connect to HL7 database settings SQL Server " & txtgloHL7ServerName.Text.Trim & " and Database " & txtgloHL7DatabaseName.Text.Trim & vbCrLf & "Select valid credentials.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information) Then
                    Exit Sub
                End If
            Else
                MessageBox.Show("Connection established successfully.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If
        Else
            If objSQLSettings.IsConnect(txtgloHL7ServerName.Text.Trim, txtgloHL7DatabaseName.Text.Trim, False, "", "") = False Then
                If MessageBox.Show("Unable to connect to HL7 database settings SQL Server " & txtgloHL7ServerName.Text.Trim & " and Database " & txtgloHL7DatabaseName.Text.Trim & vbCrLf & "Select valid credentials.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information) Then
                    Exit Sub
                End If
            Else
                MessageBox.Show("Connection established successfully.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If
        End If
    End Sub

    'end by nilesh on 20101025

    Private Sub txtCustomRptFolderNm_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCustomRptFolderNm.KeyPress
        ''''''Added by Ujwala for Customized SSRS Reports - as on 20101021
        If Regex.IsMatch(e.KeyChar, "^[-a-zA-Z0-9\b\s._]") = False And e.KeyChar = "''c" Then
            e.Handled = True
        End If
        ''''''Added by Ujwala for Customized SSRS Reports - as on 20101021
    End Sub

    Private Sub txtSequentialPatientCode_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtSequentialPatientCode.Validating
        If (_SequentialPatientCode.ToString.Trim() <> txtSequentialPatientCode.Text.ToString.Trim()) Then
            Dim strmessage As String
            Dim objAudit As New clsAudit
            strmessage = "You changed Patient code sequence number. Changing the setting could have unpredictable results while auto generate patient codes . It is recommended that not to change it. Still do you wish to proceed?"
            If MessageBox.Show(strmessage, gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information) = Windows.Forms.DialogResult.No Then
                txtSequentialPatientCode.Text = _SequentialPatientCode
                objAudit.CreateLog(clsAudit.enmActivityType.Other, "Sequential patient code changed and change rejected by clicking on No", gstrLoginName, gstrClientMachineName)
            Else
                objAudit.CreateLog(clsAudit.enmActivityType.Other, "Sequential patient code changed and change accepted by clicking on Yes", gstrLoginName, gstrClientMachineName)

            End If
        End If
    End Sub

    Private Sub txtSequentialPatientCode_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles txtSequentialPatientCode.MouseDown
        txtSequentialPatientCode.Text = txtSequentialPatientCode.Text.ToString.Trim()
    End Sub

    Private Sub btnDMSConnect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDMSConnect.Click
        Dim objSQLSettings As New clsStartUpSettings

        If txtDMSServerName.Text.Trim = "" Then
            MessageBox.Show("Enter DMS database settings Server Name.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            txtDMSServerName.Focus()
            Exit Sub
        End If
        If txtDMSDatabaseName.Text.Trim = "" Then
            MessageBox.Show("Enter DMS database settings Database Name.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            txtDMSDatabaseName.Focus()
            Exit Sub
        End If
        If optDMSSqlAuthentication.Checked = True Then
            If txtDMSUserId.Text.Trim = "" Then
                MessageBox.Show("Enter DMS database settings SQL User ID", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtDMSUserId.Focus()
                Exit Sub
            End If
            If txtDMSPassword.Text.Trim = "" Then
                MessageBox.Show("Enter DMS database settings SQL Password", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtDMSPassword.Focus()
                Exit Sub
            End If
            If objSQLSettings.IsSQLConnect(txtDMSServerName.Text.Trim, txtDMSDatabaseName.Text.Trim, txtDMSUserId.Text.Trim, txtDMSPassword.Text.Trim) = False Then
                If MessageBox.Show("Unable to connect to DMS database settings SQL Server " & txtDMSServerName.Text.Trim & " and Database " & txtDMSDatabaseName.Text.Trim & vbCrLf & "Select valid credentials.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information) Then
                    Exit Sub
                End If
            Else
                MessageBox.Show("Connection established successfully.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If
        Else
            If objSQLSettings.IsConnect(txtDMSServerName.Text.Trim, txtDMSDatabaseName.Text.Trim, False, "", "") = False Then
                If MessageBox.Show("Unable to connect to DMS database settings SQL Server " & txtDMSServerName.Text.Trim & " and Database " & txtDMSDatabaseName.Text.Trim & vbCrLf & "Select valid credentials.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information) Then
                    Exit Sub
                End If
            Else
                MessageBox.Show("Connection established successfully.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If
        End If
        '---- Added by Rahul Patel on 26-10-2010

    End Sub

    'Code added by Rohit for device database setting
    Private Sub btnDeviceConnect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeviceConnect.Click
        Dim objSQLSettings As New clsStartUpSettings

        Dim _strDeviceServername As String = txtDeviceServerName.Text.Trim
        Dim _strDeviceDataBaseName As String = txtDeviceDataBaseName.Text.Trim()

        Try
            If _strDeviceServername.Length = 0 Then
                MessageBox.Show("Enter Device database settings Server Name.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtDeviceServerName.Focus()
                Exit Sub
            End If

            If _strDeviceDataBaseName.Length = 0 Then
                MessageBox.Show("Enter Device database settings Database Name.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtDeviceDataBaseName.Focus()
                Exit Sub
            End If
            If objSQLSettings.IsConnect(_strDeviceServername, _strDeviceDataBaseName, False, "", "") Then
                MessageBox.Show("Connection established successfully.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            Else
                If MessageBox.Show("Unable to connect to Device database settings SQL Server " & _strDeviceServername & " and Database " & _strDeviceDataBaseName & "." & vbCrLf & " Select valid credentials.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information) Then
                    Exit Sub
                End If

            End If
        Catch ex As Exception
            MessageBox.Show("Unable to connect to Device database settings SQL Server " & _strDeviceServername & " and Database " & _strDeviceDataBaseName & ".", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            If Not IsNothing(objSQLSettings) Then
                objSQLSettings = Nothing
            End If
            _strDeviceServername = String.Empty
            _strDeviceDataBaseName = String.Empty
        End Try
    End Sub
    'End of code added by Rohit

    Private Sub optMMWINAuthentication_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optMMWINAuthentication.CheckedChanged
        If txtMMWUserName.Enabled = False Then
            'txtRxNSQLUserID.Text = ""
            ' txtRxNSQLPassword.Text = ""
        End If
        txtMMWUserName.Enabled = False
        txtMMWPassword.Enabled = False
        If optMMWINAuthentication.Checked = True Then
            optMMWINAuthentication.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            optMMWINAuthentication.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub optMMWSqlAuthentication_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optMMWSqlAuthentication.CheckedChanged
        txtMMWUserName.Enabled = True
        txtMMWPassword.Enabled = True
        If optMMWSqlAuthentication.Checked = True Then
            optMMWSqlAuthentication.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            optMMWSqlAuthentication.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub optgloHL7WinAuthentication_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optgloHL7WinAuthentication.CheckedChanged
        If txtgloHL7UserID.Enabled = False Then
            'txtRxNSQLUserID.Text = ""
            ' txtRxNSQLPassword.Text = ""
        End If
        txtgloHL7UserID.Enabled = False
        txtgloHL7Password.Enabled = False
        If optgloHL7WinAuthentication.Checked = True Then
            optgloHL7WinAuthentication.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            optgloHL7WinAuthentication.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If

    End Sub

    Private Sub optgloHL7SqlAuthentication_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optgloHL7SqlAuthentication.CheckedChanged
        txtgloHL7UserID.Enabled = True
        txtgloHL7Password.Enabled = True
        If optgloHL7SqlAuthentication.Checked = True Then
            optgloHL7SqlAuthentication.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            optgloHL7SqlAuthentication.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If

    End Sub

    Private Sub optDMSWINAuthentication_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optDMSWINAuthentication.CheckedChanged
        If txtDMSUserId.Enabled = False Then
            'txtRxNSQLUserID.Text = ""
            ' txtRxNSQLPassword.Text = ""
        End If
        txtDMSUserId.Enabled = False
        txtDMSPassword.Enabled = False
        If optDMSWINAuthentication.Checked = True Then
            optDMSWINAuthentication.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            optDMSWINAuthentication.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub optDMSSqlAuthentication_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optDMSSqlAuthentication.CheckedChanged
        txtDMSUserId.Enabled = True
        txtDMSPassword.Enabled = True
        If optDMSSqlAuthentication.Checked = True Then
            optDMSSqlAuthentication.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            optDMSSqlAuthentication.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub
    '--- End of Code added by Rahul Patel on 26-10-2010
    'Added by rahul patel on 29-10-2010
    'For Validation of DMS setting.
    Private Sub tb_Settings_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles tb_Settings.Click
        Me.Text = tb_Settings.TabPages(tb_Settings.SelectedIndex).Text
        If Me.Text = "Lab Settings" Then
            tbp_EmdeonSettings.AutoScroll = True
        End If
        Select Case (tb_Settings.TabPages(tb_Settings.SelectedIndex).Name)

            Case "tbp_OMRSettings"
                If (gDmsServerName.Trim() = "" And gDmsDatabaseName.Trim() = "") Then
                    MessageBox.Show("DMS database not set. Please set the dms database settings from admin.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End If
        End Select

    End Sub

    Private Sub btnCCDUser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCCDUser.Click
        Try

            If oCCDUserList IsNot Nothing Then
                For i As Integer = Me.Controls.Count - 1 To 0 Step -1
                    If Me.Controls(i).Name = oCCDUserList.Name Then
                        Me.Controls.Remove(Me.Controls(i))
                        Exit For
                    End If
                Next
            End If

            oCCDUserList = New gloListControl.gloListControl(mdlGeneral.GetConnectionString, gloListControl.gloListControlType.Users, False, Me.Width)
            oCCDUserList.ClinicID = _ClinicID
            oCCDUserList.ControlHeader = "Users"

            _CCDUserListType = gloListControl.gloListControlType.Users
            AddHandler oCCDUserList.ItemSelectedClick, AddressOf oCCDUserList_ItemSelectedClick
            AddHandler oCCDUserList.ItemClosedClick, AddressOf oCCDUserList_ItemClosedClick
            Me.Controls.Add(oCCDUserList)
            oCCDUserList.OpenControl()
            oCCDUserList.Dock = DockStyle.Fill
            oCCDUserList.BringToFront()
        Catch ex As Exception
            'gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        End Try
    End Sub
    'Code Start-Added by kanchan on 20100604 for CCD user
    Private Sub oCCDUserList_ItemSelectedClick(ByVal sender As Object, ByVal e As EventArgs)

        txtCCDUser.Clear()

        If oCCDUserList.SelectedItems.Count > 0 Then

            _CCD_DefaultUserID = oCCDUserList.SelectedItems(0).ID
            _CCD_UserName = oCCDUserList.SelectedItems(0).Description.ToString()

            txtCCDUser.Text = oCCDUserList.SelectedItems(0).Description.ToString()

        End If
    End Sub
    Private Sub oCCDUserList_ItemClosedClick(ByVal sender As Object, ByVal e As EventArgs)

    End Sub

    Private Sub C1surgicalUsers_AfterEdit(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles C1surgicalUsers.AfterEdit
        Try
            If _IsSurgicalUser = True Then


                Dim checkval As C1.Win.C1FlexGrid.CheckEnum
                checkval = C1surgicalUsers.GetCellCheck(e.Row, 0)
                If checkval = CheckEnum.Checked Then
                    dtsurgical.Rows(e.Row - 1)("Check1") = 1
                    gloItem = New gloGeneralItem.gloItem()
                    gloItem.ID = C1surgicalUsers.GetData(e.Row, 1)
                    gloItem.Status = C1surgicalUsers.GetData(e.Row, 2).ToString()
                    gloItems.Add(gloItem)
                    gloItem = Nothing
                Else
                    dtsurgical.Rows(e.Row - 1)("Check1") = 0
                    gloItem = New gloGeneralItem.gloItem()
                    gloItem.ID = C1surgicalUsers.GetData(e.Row, 1)
                    gloItem.Status = C1surgicalUsers.GetData(e.Row, 2)
                    gloItems.Remove(gloItem)
                    gloItem = Nothing
                End If
            ElseIf _IsSurgicalUser = False Then
                Dim checkval As C1.Win.C1FlexGrid.CheckEnum
                checkval = C1surgicalUsers.GetCellCheck(e.Row, 0)
                If checkval = CheckEnum.Checked Then
                    dtFollowupuser.Rows(e.Row - 1)("Check1") = 1
                    ToItem = New gloGeneralItem.gloItem()
                    ToItem.ID = C1surgicalUsers.GetData(e.Row, 1)
                    ToItem.Status = C1surgicalUsers.GetData(e.Row, 2).ToString()
                    ToList.Add(ToItem)
                    ToItem = Nothing
                Else
                    dtFollowupuser.Rows(e.Row - 1)("Check1") = 0
                    ToItem = New gloGeneralItem.gloItem()
                    ToItem.ID = C1surgicalUsers.GetData(e.Row, 1)
                    ToItem.Status = C1surgicalUsers.GetData(e.Row, 2)
                    ToList.Remove(ToItem)
                    ToItem = Nothing
                End If
            End If
            'If e.Row >= 0 Then
            '    Dim chkval As C1
            '    Dim to_edited As DataRow
            '    to_edited = dtsurgical.Rows.Find(C1surgicalUsers.GetData(e.Row, 2))
            '    to_edited=dtsurgical.Rows(

            'End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub C1surgicalUsers_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles C1surgicalUsers.MouseClick

    End Sub




    Private Sub C1surgicalUsers_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles C1surgicalUsers.MouseDown

    End Sub

    Private Sub C1surgicalUsers_MouseEnterCell(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles C1surgicalUsers.MouseEnterCell

    End Sub

    Private Sub ChkEnableECG_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub
    'start of event code commented by manoj jadhav on 20111017 for New consolidated UI to device settings
    'Private Sub ChkEnableECG_CheckedChanged_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkUseCardioScienceECGDevice.CheckedChanged
    '    Try
    '        If ChkUseCardioScienceECGDevice.Checked Then

    '            'pnlECGInterface.Visible = True
    '            'pnlECDeviceInterface.Width = 780
    '            'pnlECDeviceInterface.Height = 128
    '            txtECGInterfaceId.Enabled = True
    '            txtECGInterfaceUrl.Enabled = True
    '            txtECGUserProviderId.Enabled = True
    '        Else
    '            'pnlECGInterface.Visible = False
    '            'pnlECDeviceInterface.Width = 780
    '            'pnlECDeviceInterface.Height = 37
    '            txtECGInterfaceId.Enabled = False
    '            txtECGInterfaceUrl.Enabled = False
    '            txtECGUserProviderId.Enabled = False
    '        End If
    '    Catch ex As Exception

    '    End Try
    'End Sub
    'event code commented by manoj jadhav on 20111017 for New consolidated UI to device settings

    Private Sub ChkShowDMAlert_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkShowDMAlert.CheckedChanged

    End Sub



    Private Sub rbMultipleGuranterAllowYES_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbMultipleGuranterAllowYES.CheckedChanged
        If rbMultipleGuranterAllowYES.Checked = True Then
            rbMultipleGuranterAllowYES.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            rbMultipleGuranterAllowYES.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub rbMultipleGuranterAllowNO_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbMultipleGuranterAllowNO.CheckedChanged

        If rbMultipleGuranterAllowNO.Checked = True Then
            rbMultipleGuranterAllowNO.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            rbMultipleGuranterAllowNO.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If

        If rbMultipleGuranterAllowNO.Checked = False And blnMultipleGuarantorsProcessFlag = False Then
            MessageBox.Show("This feature will be removed in the future. Please contact Customer Care to discuss alternatives.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub



    Private Sub FillPatientAccSettings(ByVal dtPatientAcc As DataTable)
        Try

            'RemoveHandler rbPatientAccountFeatureEnabledYES.CheckedChanged, AddressOf rbPatientAccountFeatureEnabledYES_CheckedChanged
            'RemoveHandler rbPatientAccountFeatureEnabledNO.CheckedChanged, AddressOf rbPatientAccountFeatureEnabledNO_CheckedChanged

            blnProcessFlag = True
            'Me.rbPatientAccountFeatureEnabledYES.Checked = False
            'Me.rbPatientAccountFeatureEnabledNO.Checked = False
            blnProcessFlag = False

            blnMultipleGuarantorsProcessFlag = True
            Me.rbMultipleGuranterAllowYES.Checked = False
            Me.rbMultipleGuranterAllowNO.Checked = False


            If dtPatientAcc IsNot Nothing AndAlso dtPatientAcc.Rows.Count > 0 Then

                Dim _dr() As DataRow

                '_dr = dtPatientAcc.Select("sSettingName='Patient Account Feature'")

                'If Not IsNothing(_dr) And _dr.Length > 0 Then

                '    If (Convert.ToBoolean(_dr(0).Item("sSettingValue"))) Then
                '        Me.rbPatientAccountFeatureEnabledYES.Checked = True
                '    Else
                '        blnProcessFlag = True
                '        Me.rbPatientAccountFeatureEnabledNO.Checked = True
                '        blnProcessFlag = False
                '    End If
                'End If
                '_dr = Nothing


                _dr = dtPatientAcc.Select("sSettingName='Allow Multiple Guarantor'")

                If Not IsNothing(_dr) And _dr.Length > 0 Then

                    If (Convert.ToBoolean(_dr(0).Item("sSettingValue"))) Then
                        Me.rbMultipleGuranterAllowYES.Checked = True
                    Else
                        Me.rbMultipleGuranterAllowNO.Checked = True
                    End If
                End If
                _dr = Nothing

                blnMultipleGuarantorsProcessFlag = False



            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            'AddHandler rbPatientAccountFeatureEnabledYES.CheckedChanged, AddressOf rbPatientAccountFeatureEnabledYES_CheckedChanged
            'AddHandler rbPatientAccountFeatureEnabledNO.CheckedChanged, AddressOf rbPatientAccountFeatureEnabledNO_CheckedChanged

        End Try


    End Sub



    Private Sub SavePatientAccountSettingNew()
        Try
            'Dim ogloSettings As New clsSettings

            Dim patientAccountSetting As String = ""
            Dim multipleGuaranterSeeting As String = ""
            Dim copyAccountSetting As String = ""
            Dim mergeAccountSetting As String = ""

            'If rbPatientAccountFeatureEnabledYES.Checked Then
            '    patientAccountSetting = "True"
            'ElseIf rbPatientAccountFeatureEnabledNO.Checked Then
            '    patientAccountSetting = "False"
            'End If

            If rbMultipleGuranterAllowYES.Checked Then
                multipleGuaranterSeeting = "True"
            ElseIf rbMultipleGuranterAllowNO.Checked Then
                multipleGuaranterSeeting = "False"
            End If


            Dim nProviderID As Int64 = 0


            'If rbPatientAccountFeatureEnabledNO.Checked = True Then
            '    If (Not Validate_PAF()) Then
            '        'Update the changes made in the setting for Patient Account
            '        AddPatAcctSettingToDB("Patient Account Feature", patientAccountSetting, _ClinicID, gnLoginID, SettingFlag.Clinic)
            '        '' ogloSettings.AddPatientAccInTVP("Patient Account Feature", patientAccountSetting, _ClinicID, gnLoginID, SettingFlag.Clinic)
            '    End If
            'Else
            '    AddPatAcctSettingToDB("Patient Account Feature", patientAccountSetting, _ClinicID, gnLoginID, SettingFlag.Clinic)
            '    ''ogloSettings.AddPatientAccInTVP("Patient Account Feature", patientAccountSetting, _ClinicID, gnLoginID, SettingFlag.Clinic)
            'End If


            'Update the changes made in the setting for Patient Account
            AddPatAcctSettingToDB("Allow Multiple Guarantor", multipleGuaranterSeeting, _ClinicID, gnLoginID, SettingFlag.Clinic)
            ''ogloSettings.AddPatientAccInTVP("Allow Multiple Guarantor", multipleGuaranterSeeting, _ClinicID, gnLoginID, SettingFlag.Clinic)

            ''Update the changes made in the setting for Copy Account
            'ogloSettings.AddValueInTVP("Copy Account", copyAccountSetting, _ClinicID, gnLoginID, SettingFlag.Clinic)


            ''Update the changes made in the setting for Merge Account
            'ogloSettings.AddValueInTVP("Merge Account", mergeAccountSetting, _ClinicID, gnLoginID, SettingFlag.Clinic)


        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub



    Public Function AddPatAcctSettingToDB(ByVal Name As String, ByVal Value As String, ByVal ClinicID As Int64, ByVal UserID As Int64, ByVal UserClinicFlag As SettingFlag) As Boolean
        Dim oDB As New gloDatabaseLayer.DBLayer(gloEMRAdmin.mdlGeneral.GetConnectionString())
        Dim oDBParameters As New gloDatabaseLayer.DBParameters()
        Try
            oDB.Connect(False)

            oDBParameters.Add("@sSettingsName", Name, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar)
            oDBParameters.Add("@sSettingsValue", Value, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar)
            ''' oDBParameters.Add("@nClinicID", ClinicID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar)
            oDBParameters.Add("@nUserID", UserID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar)
            '''' oDBParameters.Add("@nUserClinicFlag", UserClinicFlag.GetHashCode(), System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar)

            oDB.Execute("SavePatAcctAdminSettings", oDBParameters)

            Return True
        Catch DBErr As gloDatabaseLayer.DBException
            Return False
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        Finally
            oDB.Disconnect()
            oDBParameters.Dispose()
            oDB.Dispose()
        End Try
    End Function

    Private Sub FillPatientAccSettings()

        Dim objCon As New SqlConnection
        Dim objCmd As New SqlCommand
        Dim _sql As String = ""
        Dim dtPatientAcc As DataTable

        Try

            objCon.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()

            'RemoveHandler rbPatientAccountFeatureEnabledYES.CheckedChanged, AddressOf rbPatientAccountFeatureEnabledYES_CheckedChanged
            'RemoveHandler rbPatientAccountFeatureEnabledNO.CheckedChanged, AddressOf rbPatientAccountFeatureEnabledNO_CheckedChanged

            blnProcessFlag = True
            'Me.rbPatientAccountFeatureEnabledYES.Checked = False
            'Me.rbPatientAccountFeatureEnabledNO.Checked = False
            blnProcessFlag = False

            blnMultipleGuarantorsProcessFlag = True
            Me.rbMultipleGuranterAllowYES.Checked = False
            Me.rbMultipleGuranterAllowNO.Checked = False


            _sql = "select * from Settings_Replication"

            objCmd.CommandType = CommandType.Text
            objCmd.CommandText = _sql
            objCmd.Connection = objCon
            Dim da As SqlDataAdapter

            objCon.Open()
            da = New SqlDataAdapter
            da.SelectCommand = objCmd
            dtPatientAcc = New DataTable
            da.Fill(dtPatientAcc)


            If dtPatientAcc IsNot Nothing AndAlso dtPatientAcc.Rows.Count > 0 Then

                Dim _dr() As DataRow

                '_dr = dtPatientAcc.Select("sSettingName='Patient Account Feature'")

                'If Not IsNothing(_dr) And _dr.Length > 0 Then

                '    If (Convert.ToBoolean(_dr(0).Item("sSettingValue"))) Then
                '        Me.rbPatientAccountFeatureEnabledYES.Checked = True
                '    Else
                '        blnProcessFlag = True
                '        Me.rbPatientAccountFeatureEnabledNO.Checked = True
                '        blnProcessFlag = False
                '    End If
                'End If
                '_dr = Nothing


                _dr = dtPatientAcc.Select("sSettingName='Allow Multiple Guarantor'")

                If Not IsNothing(_dr) And _dr.Length > 0 Then

                    If (Convert.ToBoolean(_dr(0).Item("sSettingValue"))) Then
                        Me.rbMultipleGuranterAllowYES.Checked = True
                    Else
                        Me.rbMultipleGuranterAllowNO.Checked = True
                    End If
                End If
                _dr = Nothing

                blnMultipleGuarantorsProcessFlag = False



            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            'AddHandler rbPatientAccountFeatureEnabledYES.CheckedChanged, AddressOf rbPatientAccountFeatureEnabledYES_CheckedChanged
            'AddHandler rbPatientAccountFeatureEnabledNO.CheckedChanged, AddressOf rbPatientAccountFeatureEnabledNO_CheckedChanged
            If objCon.State = ConnectionState.Open Then
                objCon.Close()
            End If

            If IsNothing(objCon) = False Then
                objCon.Dispose()
                objCon = Nothing
            End If
        End Try


    End Sub

    Private Sub chkMUSections_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMUSections.CheckedChanged
        If chkMUSections.Checked = True Then
            'ChkFullCCD.Checked = True
            'ChkVisitCCD.Checked = True
            ChkMedications.Checked = True
            ChkMedications.Enabled = False
            ChkMedicationVisit.Checked = True
            ChkMedicationVisit.Enabled = False
            ChkResults.Checked = True
            ChkResults.Enabled = False
            ChkLabsVisit.Checked = True
            ChkLabsVisit.Enabled = False
            chkProblems.Checked = True
            chkProblems.Enabled = False
            ChkProblemVisit.Checked = True
            ChkProblemVisit.Enabled = False
            ChkAllergy.Checked = True
            ChkAllergy.Enabled = False
            ChkAllergyVisit.Checked = True
            ChkAllergyVisit.Enabled = False
        Else
            'ChkFullCCD.Checked = False
            'ChkVisitCCD.Checked = False
            '  ChkMedications.Checked = False
            ChkMedications.Enabled = True
            '  ChkMedicationVisit.Checked = False
            ChkMedicationVisit.Enabled = True
            '  ChkResults.Checked = False
            ChkResults.Enabled = True
            ' ChkLabsVisit.Checked = False
            ChkLabsVisit.Enabled = True
            '   chkProblems.Checked = False
            chkProblems.Enabled = True
            '   ChkProblemVisit.Checked = False
            ChkProblemVisit.Enabled = True
            '  ChkAllergy.Checked = False
            ChkAllergy.Enabled = True
            '  ChkAllergyVisit.Checked = False
            ChkAllergyVisit.Enabled = True
        End If
    End Sub


    Private Sub SelectAllFullCCD()

        ChkAllergy.Checked = True
        ChkVitals.Checked = True
        ChkMedications.Checked = True
        ChkResults.Checked = True
        ChkImmunization.Checked = True
        ChkFamilyHistory.Checked = True
        ChkSocialHistory.Checked = True
        ChkProcedures.Checked = True
        ChkEncounter.Checked = True
        ChkAdvanceDirectives.Checked = True
        chkProblems.Checked = True


    End Sub
    Private Sub DeselectAllFullCCD()
        ChkAllergy.Checked = False
        ChkVitals.Checked = False
        ChkMedications.Checked = False
        ChkResults.Checked = False
        ChkImmunization.Checked = False
        ChkFamilyHistory.Checked = False
        ChkSocialHistory.Checked = False
        ChkProcedures.Checked = False
        ChkEncounter.Checked = False
        ChkAdvanceDirectives.Checked = False
        chkProblems.Checked = False

    End Sub



    Private Sub SelectAllVisitCCD()

        ChkAllergyVisit.Checked = True
        ChkVitalsVisit.Checked = True
        ChkMedicationVisit.Checked = True
        ChkLabsVisit.Checked = True
        ChkImmuVisit.Checked = True
        ChkFamilyHisVisit.Checked = True
        ChkSocialHisVisit.Checked = True
        ChkProcVisit.Checked = True
        ChkEncounterVisit.Checked = True
        ChkAdDirectiveVisit.Checked = True
        ChkProblemVisit.Checked = True


    End Sub
    Private Sub DeselectAllVisitCCD()

        ChkAllergyVisit.Checked = False
        ChkVitalsVisit.Checked = False
        ChkMedicationVisit.Checked = False
        ChkLabsVisit.Checked = False
        ChkImmuVisit.Checked = False
        ChkFamilyHisVisit.Checked = False
        ChkSocialHisVisit.Checked = False
        ChkProcVisit.Checked = False
        ChkEncounterVisit.Checked = False
        ChkAdDirectiveVisit.Checked = False
        ChkProblemVisit.Checked = False

    End Sub

    Private Sub ChkFullCCD_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'blnAllChkd = True
        'If ChkFullCCD.Checked = True Then

        '    Call SelectAllFullCCD()
        'Else

        '    If blnChk = False Then
        '        Call DeselectAllFullCCD()
        '    End If
        '    blnChk = False
        'End If
        'blnAllChkd = False
    End Sub

    Private Sub ChkVisitCCD_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'blnAllChkd = True
        'If ChkVisitCCD.Checked = True Then

        '    Call SelectAllVisitCCD()
        'Else

        '    If blnChk = False Then
        '        Call DeselectAllVisitCCD()
        '    End If
        '    blnChk = False
        'End If
        'blnAllChkd = False
    End Sub

    Private Sub ChkAllergy_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkAllergy.CheckedChanged
        '' CheckSelection()
    End Sub
    Private Sub CheckSelection()
        'If blnAllChkd = False Then
        '    If ChkAllergy.Checked = True And ChkEncounter.Checked = True And ChkFamilyHistory.Checked = True And ChkImmunization.Checked = True And ChkMedications.Checked = True And chkProblems.Checked = True And ChkProcedures.Checked = True And ChkResults.Checked = True And ChkSocialHistory.Checked = True And ChkVitals.Checked = True And ChkAdvanceDirectives.Checked = True Then
        '        If ChkFullCCD.Checked = False Then
        '            ''blnChk = True
        '            ChkFullCCD.Checked = True
        '        End If
        '    Else
        '        If ChkFullCCD.Checked = True Then
        '            blnChk = True
        '            ChkFullCCD.Checked = False
        '        End If
        '    End If
        'End If
    End Sub
    Private Sub CheckSelectionVisitCCD()
        'If blnAllChkd = False Then
        '    If ChkAllergyVisit.Checked = True And ChkEncounterVisit.Checked = True And ChkFamilyHisVisit.Checked = True And ChkImmuVisit.Checked = True And ChkMedicationVisit.Checked = True And ChkProblemVisit.Checked = True And ChkProcVisit.Checked = True And ChkLabsVisit.Checked = True And ChkSocialHisVisit.Checked = True And ChkVitalsVisit.Checked = True And ChkAdDirectiveVisit.Checked = True Then
        '        If ChkVisitCCD.Checked = False Then
        '            ''blnChk = True
        '            ChkVisitCCD.Checked = True
        '        End If
        '    Else
        '        If ChkVisitCCD.Checked = True Then
        '            blnChk = True
        '            ChkVisitCCD.Checked = False
        '        End If
        '    End If
        'End If
    End Sub
    Private Sub chkProblems_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkProblems.CheckedChanged
        ' CheckSelection()
    End Sub

    Private Sub ChkMedications_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkMedications.CheckedChanged
        ' CheckSelection()
    End Sub

    Private Sub ChkVitals_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkVitals.CheckedChanged
        ' CheckSelection()
    End Sub

    Private Sub ChkImmunization_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkImmunization.CheckedChanged
        ' CheckSelection()
    End Sub

    Private Sub ChkResults_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkResults.CheckedChanged
        ' CheckSelection()
    End Sub

    Private Sub ChkSocialHistory_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkSocialHistory.CheckedChanged
        ' CheckSelection()
    End Sub

    Private Sub ChkFamilyHistory_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkFamilyHistory.CheckedChanged
        ' CheckSelection()
    End Sub

    Private Sub ChkEncounter_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkEncounter.CheckedChanged
        ' CheckSelection()
    End Sub

    Private Sub ChkProcedures_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkProcedures.CheckedChanged
        ' CheckSelection()
    End Sub

    Private Sub ChkAdvanceDirectives_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkAdvanceDirectives.CheckedChanged
        ' CheckSelection()
    End Sub

    Private Sub ChkAllergyVisit_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkAllergyVisit.CheckedChanged
        'CheckSelectionVisitCCD()
    End Sub

    Private Sub ChkMedicationVisit_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkMedicationVisit.CheckedChanged
        'CheckSelectionVisitCCD()
    End Sub

    Private Sub ChkImmuVisit_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkImmuVisit.CheckedChanged
        'CheckSelectionVisitCCD()
    End Sub

    Private Sub ChkSocialHisVisit_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkSocialHisVisit.CheckedChanged
        'CheckSelectionVisitCCD()
    End Sub

    Private Sub ChkEncounterVisit_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkEncounterVisit.CheckedChanged
        'CheckSelectionVisitCCD()
    End Sub

    Private Sub ChkAdDirectiveVisit_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkAdDirectiveVisit.CheckedChanged
        'CheckSelectionVisitCCD()
    End Sub

    Private Sub ChkProblemVisit_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkProblemVisit.CheckedChanged
        'CheckSelectionVisitCCD()
    End Sub

    Private Sub ChkVitalsVisit_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkVitalsVisit.CheckedChanged
        'CheckSelectionVisitCCD()
    End Sub

    Private Sub ChkLabsVisit_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkLabsVisit.CheckedChanged
        'CheckSelectionVisitCCD()
    End Sub

    Private Sub ChkFamilyHisVisit_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkFamilyHisVisit.CheckedChanged
        'CheckSelectionVisitCCD()
    End Sub

    Private Sub ChkProcVisit_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkProcVisit.CheckedChanged
        'CheckSelectionVisitCCD()
    End Sub
    Private Sub checkFullCCDSections(ByVal _selectedFullCCD() As String, ByVal _CCDSettingsname As String)


        Dim _Section As Int16
        Dim FullCCD() As String
        Dim str1 As String
        Dim k As Int16
        If _CCDSettingsname = "FullCCD" Then
            For _Section = 0 To _selectedFullCCD.Length - 1
                If _selectedFullCCD(_Section) = ChkVitals.Tag Then
                    ChkVitals.Checked = True
                ElseIf _selectedFullCCD(_Section) = ChkFamilyHistory.Tag Then
                    ChkFamilyHistory.Checked = True
                ElseIf _selectedFullCCD(_Section) = ChkAdvanceDirectives.Tag Then
                    ChkAdvanceDirectives.Checked = True
                ElseIf _selectedFullCCD(_Section) = ChkResults.Tag Then
                    ChkResults.Checked = True
                ElseIf _selectedFullCCD(_Section) = ChkImmunization.Tag Then
                    ChkImmunization.Checked = True
                ElseIf _selectedFullCCD(_Section) = ChkProcedures.Tag Then
                    ChkProcedures.Checked = True
                ElseIf _selectedFullCCD(_Section) = ChkMedications.Tag Then
                    ChkMedications.Checked = True
                ElseIf _selectedFullCCD(_Section) = ChkEncounter.Tag Then
                    ChkEncounter.Checked = True
                ElseIf _selectedFullCCD(_Section) = ChkSocialHistory.Tag Then
                    ChkSocialHistory.Checked = True
                ElseIf _selectedFullCCD(_Section) = ChkAllergy.Tag Then
                    ChkAllergy.Checked = True
                ElseIf _selectedFullCCD(_Section) = chkProblems.Tag Then
                    chkProblems.Checked = True
                End If
            Next
        ElseIf _CCDSettingsname = "VisitCCD" Then
            For _Section = 0 To _selectedFullCCD.Length - 1
                If _selectedFullCCD(_Section) = ChkVitalsVisit.Tag Then
                    ChkVitalsVisit.Checked = True
                ElseIf _selectedFullCCD(_Section) = ChkFamilyHisVisit.Tag Then
                    ChkFamilyHisVisit.Checked = True
                ElseIf _selectedFullCCD(_Section) = ChkAdDirectiveVisit.Tag Then
                    ChkAdDirectiveVisit.Checked = True
                ElseIf _selectedFullCCD(_Section) = ChkLabsVisit.Tag Then
                    ChkLabsVisit.Checked = True
                ElseIf _selectedFullCCD(_Section) = ChkImmuVisit.Tag Then
                    ChkImmuVisit.Checked = True
                ElseIf _selectedFullCCD(_Section) = ChkProcVisit.Tag Then
                    ChkProcVisit.Checked = True
                ElseIf _selectedFullCCD(_Section) = ChkMedicationVisit.Tag Then
                    ChkMedicationVisit.Checked = True
                ElseIf _selectedFullCCD(_Section) = ChkEncounterVisit.Tag Then
                    ChkEncounterVisit.Checked = True
                ElseIf _selectedFullCCD(_Section) = ChkSocialHisVisit.Tag Then
                    ChkSocialHisVisit.Checked = True
                ElseIf _selectedFullCCD(_Section) = ChkAllergyVisit.Tag Then
                    ChkAllergyVisit.Checked = True
                ElseIf _selectedFullCCD(_Section) = ChkProblemVisit.Tag Then
                    ChkProblemVisit.Checked = True
                End If
            Next

        End If

    End Sub

    Private Function getClinicalChartvalue() As String
        Dim sClinicalChart As String = String.Empty
        Dim iNodeIndex As Integer
        Dim flg As Boolean = False
        ' j = 0
        For iNodeIndex = 0 To trvClinicalChart.Nodes.Count - 1
            ' j = j + 1
            If trvClinicalChart.Nodes(iNodeIndex).Checked = True Then
                flg = True
                If sClinicalChart <> "" Then
                    sClinicalChart = sClinicalChart & "C" & "." & trvClinicalChart.Nodes(iNodeIndex).Tag.ToString() & ","
                Else
                    sClinicalChart = "C" & "." & trvClinicalChart.Nodes(iNodeIndex).Tag.ToString() & ","
                End If
            Else
                If sClinicalChart <> "" Then
                    sClinicalChart = sClinicalChart & "U" & "." & trvClinicalChart.Nodes(iNodeIndex).Tag.ToString() & ","
                Else
                    sClinicalChart = "U" & "." & trvClinicalChart.Nodes(iNodeIndex).Tag.ToString() & ","
                End If
            End If

        Next
        If flg = False Then
            sClinicalChart = ""
        Else
            If sClinicalChart <> "" Then
                sClinicalChart = sClinicalChart.Remove((sClinicalChart.Length - 1), 1)
            End If
        End If


        Return sClinicalChart
    End Function

    Private Sub btn_CC_Up_Click(sender As System.Object, e As System.EventArgs) Handles btn_CC_Up.Click
        Try
            Dim oNode As TreeNode
            Dim prevIndex As Integer
            If IsNothing(trvClinicalChart.SelectedNode) = False Then
                oNode = trvClinicalChart.SelectedNode.Clone
                If IsNothing(oNode) = False Then

                    If IsNothing(trvClinicalChart.SelectedNode.Parent) Then
                        If trvClinicalChart.SelectedNode.Index <> 0 Then
                            prevIndex = trvClinicalChart.SelectedNode.PrevNode.Index
                            trvClinicalChart.Nodes.Remove(trvClinicalChart.SelectedNode)
                            trvClinicalChart.Nodes.Insert(prevIndex, oNode)
                            trvClinicalChart.SelectedNode = oNode
                        End If
                    Else
                        If trvClinicalChart.SelectedNode.Index <> 0 Then
                            Dim PNode As TreeNode
                            PNode = trvClinicalChart.SelectedNode.Parent
                            prevIndex = trvClinicalChart.SelectedNode.PrevNode.Index
                            PNode.Nodes.Remove(trvClinicalChart.SelectedNode)
                            PNode.Nodes.Insert(prevIndex, oNode)
                            trvClinicalChart.SelectedNode = oNode

                        End If
                    End If
                End If
            End If
            trvClinicalChart.ExpandAll()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btn_CC_Down_Click(sender As System.Object, e As System.EventArgs) Handles btn_CC_Down.Click
        Try
            Dim oNode As TreeNode
            Dim nextIndex As Integer
            If IsNothing(trvClinicalChart.SelectedNode) = False Then
                oNode = trvClinicalChart.SelectedNode.Clone
                If IsNothing(oNode) = False Then

                    If IsNothing(trvClinicalChart.SelectedNode.Parent) Then
                        If trvClinicalChart.SelectedNode.Index <> trvClinicalChart.Nodes.Count - 1 Then
                            nextIndex = trvClinicalChart.SelectedNode.NextNode.Index
                            trvClinicalChart.Nodes.Remove(trvClinicalChart.SelectedNode)
                            trvClinicalChart.Nodes.Insert(nextIndex, oNode)
                            trvClinicalChart.SelectedNode = oNode
                        End If
                    Else
                        If trvClinicalChart.SelectedNode.Index <> trvClinicalChart.SelectedNode.Parent.Nodes.Count - 1 Then
                            Dim PNode As TreeNode
                            PNode = trvClinicalChart.SelectedNode.Parent
                            nextIndex = trvClinicalChart.SelectedNode.NextNode.Index
                            PNode.Nodes.Remove(trvClinicalChart.SelectedNode)
                            PNode.Nodes.Insert(nextIndex, oNode)
                            trvClinicalChart.SelectedNode = oNode
                        End If

                    End If
                End If
            End If
            trvClinicalChart.ExpandAll()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btn_CC_Reset_Click(sender As System.Object, e As System.EventArgs) Handles btn_CC_Reset.Click
        Try
            Dim oNode As TreeNode
            trvClinicalChart.Nodes.Clear()
            trvClinicalChart.CheckBoxes = True

            Dim CCType As enumDefaultClinicalChart
            For Each CCType In [Enum].GetValues(GetType(enumDefaultClinicalChart))
                oNode = New TreeNode
                oNode.Text = Replace(CCType.ToString(), "_", " ")
                oNode.Tag = Replace(CCType.ToString(), "_", " ")
                oNode.Checked = True
                trvClinicalChart.Nodes.Add(oNode)
            Next
            chk_CC_SelectAll.Checked = True
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub chk_CC_SelectAll_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chk_CC_SelectAll.CheckedChanged
        Try
            If chk_CC_SelectAll.Checked = True Then

                bSelectAll_CC = True
                SelectAll_ClearAll_ClinicalChart("select")

            Else
                If bSelectAll_CC = True Then
                    SelectAll_ClearAll_ClinicalChart("clear")
                End If


            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub trvClinicalChart_AfterCheck(sender As System.Object, e As System.Windows.Forms.TreeViewEventArgs) Handles trvClinicalChart.AfterCheck
        Try
            Dim nCount As Int16
            Dim nTotalNodes As Int16
            nTotalNodes = trvClinicalChart.Nodes.Count - 1
            Dim ncheckedcnt As Int16
            For nCount = 0 To nTotalNodes
                If (trvClinicalChart.Nodes(nCount).Checked = True) Then
                    ncheckedcnt = ncheckedcnt + 1
                End If
            Next
            If ncheckedcnt = nTotalNodes + 1 Then
                bSelectAll_CC = True
                chk_CC_SelectAll.Checked = True
            Else
                bSelectAll_CC = False
                chk_CC_SelectAll.Checked = False
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub chk_FaxExceeds_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chk_FaxExceeds.CheckedChanged
        Try
            If chk_FaxExceeds.Checked = True Then
                txtFaxExceeds.Enabled = True
                txtFaxExceeds.Text = 50
            Else
                txtFaxExceeds.Enabled = False
                txtFaxExceeds.Text = String.Empty
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub chk_AllowFaxes_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chk_AllowFaxes.CheckedChanged
        Try
            If chk_AllowFaxes.Checked = True Then
                txtAllowFaxes.Enabled = True
                txtAllowFaxes.Text = 75
            Else
                txtAllowFaxes.Enabled = False
                txtAllowFaxes.Text = String.Empty
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtFaxExceeds_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles txtFaxExceeds.KeyDown
        nonNumberEntered = False

        If e.KeyCode < Keys.D0 OrElse e.KeyCode > Keys.D9 Then

            If e.KeyCode < Keys.NumPad0 OrElse e.KeyCode > Keys.NumPad9 Then

                If e.KeyCode <> Keys.Back Or e.KeyCode = Keys.Decimal Then
                    nonNumberEntered = True
                End If
            End If
        End If

        If Control.ModifierKeys = Keys.Shift Then
            nonNumberEntered = True
        End If
    End Sub

    Private Sub txtFaxExceeds_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtFaxExceeds.KeyPress
        If nonNumberEntered = True Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtAllowFaxes_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles txtAllowFaxes.KeyDown
        nonNumberEntered = False

        If e.KeyCode < Keys.D0 OrElse e.KeyCode > Keys.D9 Then

            If e.KeyCode < Keys.NumPad0 OrElse e.KeyCode > Keys.NumPad9 Then

                If e.KeyCode <> Keys.Back Or e.KeyCode = Keys.Decimal Then

                    nonNumberEntered = True
                End If
            End If
        End If

        If Control.ModifierKeys = Keys.Shift Then
            nonNumberEntered = True
        End If
    End Sub

    Private Sub txtAllowFaxes_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtAllowFaxes.KeyPress
        If nonNumberEntered = True Then
            e.Handled = True
        End If
    End Sub

    Private Sub btnMapping_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMapping.Click
        Try
            Dim ofrm As New frmDASTestsList
            ofrm.StartPosition = FormStartPosition.CenterScreen
            ofrm.ShowDialog()
            ofrm.Dispose()
            ofrm = Nothing
        Catch ex As Exception

        Finally

        End Try
    End Sub

    Private Sub chkRetrieveESRValue_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkRetrieveESRValue.CheckedChanged
        If chkRetrieveESRValue.Checked Then
            lblESRLookBack.Visible = True
            txtESRDays.Visible = True
            If btnMapping.Visible = False Then
                btnMapping.Visible = True
            End If
        Else
            lblESRLookBack.Visible = False
            txtESRDays.Visible = False
            If chkRetrieveCRPValue.Checked = False Then
                btnMapping.Visible = False
            End If
        End If
    End Sub

    Private Sub chkRetrieveCRPValue_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkRetrieveCRPValue.CheckedChanged
        If chkRetrieveCRPValue.Checked Then
            lblCRPLookBack.Visible = True
            txtCRPDays.Visible = True
            If btnMapping.Visible = False Then
                btnMapping.Visible = True
            End If
        Else
            lblCRPLookBack.Visible = False
            txtCRPDays.Visible = False
            If chkRetrieveESRValue.Checked = False Then
                btnMapping.Visible = False
            End If
        End If
    End Sub

    Private Sub txtCRPDays_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCRPDays.KeyPress
        Try
            Dim allowedChars As String = "0123456789"

            If allowedChars.IndexOf(e.KeyChar) = -1 Then
                ' Invalid Character
                e.Handled = True
            End If
            If e.KeyChar = Chr(8) Then e.Handled = False 'allow Backspace

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtESRDays_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtESRDays.KeyPress
        Try
            Dim allowedChars As String = "0123456789"

            If allowedChars.IndexOf(e.KeyChar) = -1 Then
                ' Invalid Character
                e.Handled = True
            End If
            If e.KeyChar = Chr(8) Then e.Handled = False 'allow Backspace

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    'New Event/code added by manoj jadhav on 20111017 for invoke WelchAllyn vital device settings screen
    Private Sub btnWelchAllynVitalSettings_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnWelchAllynVitalSettings.Click
        Try
            Dim _isAUSUserNameConfigured As Boolean = False
            _isAUSUserNameConfigured = CheckAUSUsername()

            If _isAUSUserNameConfigured Then
                Dim objFrmDeviceActivation As FrmDeviceActivation
                Try
                    objFrmDeviceActivation = New FrmDeviceActivation(FrmDeviceActivation.DeviceSettings.WelChAllynVitalDevice)
                    objFrmDeviceActivation.ShowDialog()
                    If objFrmDeviceActivation.IsSettingChanged Then
                        chkUseVitalDevice.Checked = True
                        chkUseVitalDevice.Enabled = True
                    Else
                        chkUseVitalDevice.Checked = False
                        chkUseVitalDevice.Enabled = False
                    End If
                Catch ex As Exception
                    ex = Nothing
                Finally
                    If Not objFrmDeviceActivation Is Nothing Then
                        objFrmDeviceActivation.Dispose()
                        objFrmDeviceActivation = Nothing
                    End If
                End Try
            End If
        Catch ex As Exception
            ex = Nothing
        Finally

        End Try

    End Sub
    'New Event/code added by manoj jadhav on 20111017 for invoke WelchAllyn ECG device settings screen
    Private Sub btnShowWelchAllynECGDeviceSettings_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShowWelchAllynECGDeviceSettings.Click
        Try
            Dim _isAUSUserNameConfigured As Boolean = False
            _isAUSUserNameConfigured = CheckAUSUsername()

            If _isAUSUserNameConfigured Then
                Dim objFrmDeviceActivation As FrmDeviceActivation
                Try
                    objFrmDeviceActivation = New FrmDeviceActivation(FrmDeviceActivation.DeviceSettings.WelchAllynECGDevice)
                    objFrmDeviceActivation.ShowDialog()
                    If objFrmDeviceActivation.IsSettingChanged Then
                        ChkUseWelchAllynECGDevice.Checked = True
                        ChkUseWelchAllynECGDevice.Enabled = True
                    Else
                        ChkUseWelchAllynECGDevice.Checked = False
                        ChkUseWelchAllynECGDevice.Enabled = False
                    End If
                Catch ex As Exception
                    ex = Nothing
                Finally
                    If Not objFrmDeviceActivation Is Nothing Then
                        objFrmDeviceActivation.Dispose()
                        objFrmDeviceActivation = Nothing
                    End If
                End Try
            End If
        Catch ex As Exception
            ex = Nothing
        Finally

        End Try

    End Sub
    'New Event/code added by manoj jadhav on 20111017 for invoke Midamrk spirometry device settings screen
    Private Sub btnShowMidmarkSpirometrySettings_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShowMidmarkSpirometrySettings.Click
        Try
            Dim _isAUSUserNameConfigured As Boolean = False
            _isAUSUserNameConfigured = CheckAUSUsername()

            If _isAUSUserNameConfigured Then
                Dim objFrmDeviceActivation As FrmDeviceActivation
                Try
                    objFrmDeviceActivation = New FrmDeviceActivation(FrmDeviceActivation.DeviceSettings.MidmarkSpirometryDevice)
                    objFrmDeviceActivation.ShowDialog()
                    If objFrmDeviceActivation.IsSettingChanged Then
                        chkUseSpirometryDevice.Checked = True
                        chkUseSpirometryDevice.Enabled = True
                    Else
                        chkUseSpirometryDevice.Checked = False
                        chkUseSpirometryDevice.Enabled = False
                    End If
                Catch ex As Exception
                    ex = Nothing
                Finally
                    If Not objFrmDeviceActivation Is Nothing Then
                        objFrmDeviceActivation.Dispose()
                        objFrmDeviceActivation = Nothing
                    End If
                End Try
            End If
        Catch ex As Exception
            ex = Nothing
        Finally

        End Try
    End Sub
    'New Event/code added by manoj jadhav on 20111017 for invoke Cardio Scince ECG device settings screen
    Private Sub btnShowCardioScienceECGDeviceSettings_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShowCardioScienceECGDeviceSettings.Click
        Try
            Dim _isAUSUserNameConfigured As Boolean = False
            _isAUSUserNameConfigured = CheckAUSUsername()

            If _isAUSUserNameConfigured Then
                Dim objFrmDeviceActivation As FrmDeviceActivation
                Try
                    objFrmDeviceActivation = New FrmDeviceActivation(FrmDeviceActivation.DeviceSettings.CardiacScienceECGDevice)
                    objFrmDeviceActivation.ShowDialog()
                    If objFrmDeviceActivation.IsSettingChanged Then
                        ChkUseCardioScienceECGDevice.Checked = True
                        ChkUseCardioScienceECGDevice.Enabled = True
                    Else
                        ChkUseCardioScienceECGDevice.Checked = False
                        ChkUseCardioScienceECGDevice.Enabled = False
                    End If
                Catch ex As Exception
                    ex = Nothing
                Finally
                    If Not objFrmDeviceActivation Is Nothing Then
                        objFrmDeviceActivation.Dispose()
                        objFrmDeviceActivation = Nothing
                    End If
                End Try
            End If
        Catch ex As Exception
            ex = Nothing
        Finally

        End Try

    End Sub
    'New Event/code added by manoj jadhav on 20111017 for invoke Medfusion health interface setting screen
    Private Sub btnShowMedfusionSettings_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShowMedfusionSettings.Click
        Try
            If txtPatientPortalEmailService.Text.Trim = String.Empty Then
                MessageBox.Show("Enter Email service address", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtPatientPortalEmailService.Focus()
                ' InUP_IntuitHealthInterface = False
                Exit Sub
            End If
            If txtPatientPortalgloCoreServicesInstallationPath.Text.Trim = String.Empty Then
                MessageBox.Show("Enter QCore service installation path", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtPatientPortalgloCoreServicesInstallationPath.Focus()
                ' InUP_IntuitHealthInterface = False
                Exit Sub
            End If

            If (Not (txtPatientPortalgloCoreServicesInstallationPath.Text.Trim = String.Empty)) Then
                If Not System.IO.Directory.Exists(txtPatientPortalgloCoreServicesInstallationPath.Text.Trim) Then
                    MessageBox.Show("Enter valid QCore service installation path", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    txtPatientPortalgloCoreServicesInstallationPath.Focus()
                    ' InUP_IntuitHealthInterface = False
                    Exit Sub
                End If
            End If

            Dim _isAUSUserNameConfigured As Boolean = False
            _isAUSUserNameConfigured = CheckAUSUsername()

            If _isAUSUserNameConfigured Then

                Dim objFrmDeviceActivation As FrmDeviceActivation
                Try
                  
                    objFrmDeviceActivation = New FrmDeviceActivation(FrmDeviceActivation.DeviceSettings.IntuitHealthInterface)
                    objFrmDeviceActivation.sCommonPatPortalEmailService = txtPatientPortalEmailService.Text.Trim
                    objFrmDeviceActivation.sCommonPatientPortalgloCoreServicesIntallationPath = txtPatientPortalgloCoreServicesInstallationPath.Text.Trim

                    objFrmDeviceActivation.ShowDialog()

                    If objFrmDeviceActivation.IsSettingChanged Then
                        chkUseMedfusion.Checked = True
                        chkUseMedfusion.Enabled = True
                    Else
                        chkUseMedfusion.Checked = False
                        chkUseMedfusion.Enabled = False
                    End If

                    If objFrmDeviceActivation.IsPatientPortalActivated = True Then
                        chkAcknowledgeEmailSend.Enabled = True
                        'chkAcknowledgeEmailSend.Visible = True     
                        chkNotifyStatement.Enabled = True
                    Else
                        chkAcknowledgeEmailSend.Checked = False
                        chkAcknowledgeEmailSend.Enabled = False
                        'chkAcknowledgeEmailSend.Visible = False
                        chkNotifyStatement.Checked = False
                        chkNotifyStatement.Enabled = False
                    End If

                Catch ex As Exception
                    ex = Nothing

                Finally
                    If Not objFrmDeviceActivation Is Nothing Then
                        objFrmDeviceActivation.Dispose()
                        objFrmDeviceActivation = Nothing
                    End If

                End Try
            End If
        Catch ex As Exception
            ex = Nothing
        Finally

        End Try

    End Sub
    'New Event/code added by manoj jadhav on 20111017 for diasble WelchAllyn Vital device settings
    Private Sub chkUseVitalDevice_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkUseVitalDevice.Click
        If Not chkUseVitalDevice.Checked Then
            If MessageBox.Show("Are you sure you want to disable WelchAllyn vital device interface?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                If DisableDeviceSettings(FrmDeviceActivation.DeviceSettings.WelChAllynVitalDevice) Then
                    chkUseVitalDevice.Checked = False
                    chkUseVitalDevice.Enabled = False
                End If
            Else
                chkUseVitalDevice.Checked = True
                chkUseVitalDevice.Enabled = True
            End If
        End If
    End Sub
    'New Event/code added by manoj jadhav on 20111017 for diasble WelchAllyn ECG device settings
    Private Sub ChkUseWelchAllynECGDevice_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkUseWelchAllynECGDevice.Click
        If Not ChkUseWelchAllynECGDevice.Checked Then
            If MessageBox.Show("Are you sure you want to disable WelchAllyn ECG device interface?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                If DisableDeviceSettings(FrmDeviceActivation.DeviceSettings.WelchAllynECGDevice) Then
                    ChkUseWelchAllynECGDevice.Checked = False
                    ChkUseWelchAllynECGDevice.Enabled = False
                End If
            Else
                ChkUseWelchAllynECGDevice.Checked = True
                ChkUseWelchAllynECGDevice.Enabled = True
            End If
        End If
    End Sub
    'New Event/code added by manoj jadhav on 20111017 for diasble Midmark Spirometry device settings
    Private Sub chkUseSpirometryDevice_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkUseSpirometryDevice.Click
        If Not chkUseSpirometryDevice.Checked Then
            If MessageBox.Show("Are you sure you want to disable Midmark spirometer device interface?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                If DisableDeviceSettings(FrmDeviceActivation.DeviceSettings.MidmarkSpirometryDevice) Then
                    chkUseSpirometryDevice.Checked = False
                    chkUseSpirometryDevice.Enabled = False
                End If
            Else
                chkUseSpirometryDevice.Checked = True
                chkUseSpirometryDevice.Enabled = True
            End If
        End If
    End Sub
    'New Event/code added by manoj jadhav on 20111017 for diasble Cardio Scince ECG device settings
    Private Sub ChkUseCardioScienceECGDevice_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkUseCardioScienceECGDevice.Click
        If Not ChkUseCardioScienceECGDevice.Checked Then
            If MessageBox.Show("Are you sure you want to disable HeartCentrix ECG device interface?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                If DisableDeviceSettings(FrmDeviceActivation.DeviceSettings.CardiacScienceECGDevice) Then
                    ChkUseCardioScienceECGDevice.Checked = False
                    ChkUseCardioScienceECGDevice.Enabled = False
                End If
            Else
                ChkUseCardioScienceECGDevice.Checked = True
                ChkUseCardioScienceECGDevice.Enabled = True
            End If
        End If
    End Sub
    'New Event/code added by manoj jadhav on 20111017 for diasble Medifusin health interface settings
    Private Sub chkUseMedfusion_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkUseMedfusion.Click
        If Not chkUseMedfusion.Checked Then
            If MessageBox.Show("Are you sure you want to disable portal interface?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                If DisableDeviceSettings(FrmDeviceActivation.DeviceSettings.IntuitHealthInterface) Then
                    chkUseMedfusion.Checked = False
                    chkUseMedfusion.Enabled = False
                    chkAcknowledgeEmailSend.Checked = False
                    chkAcknowledgeEmailSend.Enabled = False
                    'chkAcknowledgeEmailSend.Visible = False

                    chkNotifyStatement.Checked = False
                    chkNotifyStatement.Enabled = False
                End If
            Else
                chkUseMedfusion.Checked = True
                chkUseMedfusion.Enabled = True
            End If
        End If
    End Sub
    ' New funcation/code added by manoj jadhav on 20111017 for diasble Cardio Scince ECG device settings
    Private Function DisableDeviceSettings(ByVal Devicetype As FrmDeviceActivation.DeviceSettings)
        Dim objFrmDeviceActivation As New FrmDeviceActivation()
        Try
            If objFrmDeviceActivation.DiasblDeviceSettings(Devicetype) Then
                DisableDeviceSettings = True
            End If

        Catch ex As Exception
            ex = Nothing
            DisableDeviceSettings = False
        Finally
            If Not objFrmDeviceActivation Is Nothing Then
                objFrmDeviceActivation.Dispose()
                objFrmDeviceActivation = Nothing
            End If
        End Try
    End Function

    'code added by RK on 20120305
    ''' <summary>
    ''' Check if AUS user name is conifgured for a clinic
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CheckAUSUsername() As Boolean
        Dim _bResult As Boolean = False
        Dim strAUSusername As String = String.Empty
        Try
            strAUSusername = GetClinicInformation("sExternalcode")
            If strAUSusername.Trim.Length > 0 Then
                _bResult = True
            Else
                MessageBox.Show("AUS User Name is not configured for a Clinic.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                _bResult = False
            End If
        Catch ex As Exception
            ex = Nothing
            _bResult = False
        Finally
            strAUSusername = String.Empty
        End Try
        Return _bResult
    End Function
    'end of code added by RK on 20120305

    Private Sub btnFailedLabTaskUserSearch_Click(sender As System.Object, e As System.EventArgs) Handles btnFailedLabTaskUserSearch.Click
        Try
            If oListControl_FailedLabTask IsNot Nothing Then
                For i As Integer = Me.Controls.Count - 1 To 0 Step -1
                    If Me.Controls(i).Name = oListControl_FailedLabTask.Name Then
                        Me.Controls.Remove(Me.Controls(i))
                        Exit For
                    End If
                Next
            End If

            oListControl_FailedLabTask = New gloListControl.gloListControl(mdlGeneral.GetConnectionString, gloListControl.gloListControlType.Users, False, Me.Width)
            oListControl_FailedLabTask.ClinicID = _gloLab_defaultID
            oListControl_FailedLabTask.ControlHeader = "Users"

            ''_CurrentControlType = gloListControl.gloListControlType.Users
            AddHandler oListControl_FailedLabTask.ItemSelectedClick, AddressOf oListControl_FailedLabTask_ItemSelectedClick
            AddHandler oListControl_FailedLabTask.ItemClosedClick, AddressOf oListControl_FailedLabTask_ItemClosedClick

            Me.Controls.Add(oListControl_FailedLabTask)

            oListControl_FailedLabTask.OpenControl()
            oListControl_FailedLabTask.Dock = DockStyle.Fill
            oListControl_FailedLabTask.BringToFront()

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        End Try
    End Sub

    Private Sub oListControl_FailedLabTask_itemSelectedClick(ByVal sender As Object, ByVal e As EventArgs)

        txtFailedLabTask_DefaultUser.Clear()

        If oListControl_FailedLabTask.SelectedItems.Count > 0 Then
            _gloLabFailure_DefaultUserID = oListControl_FailedLabTask.SelectedItems(0).ID
            _gloLabFailure_defaultUserName = oListControl_FailedLabTask.SelectedItems(0).Description.ToString()

            txtFailedLabTask_DefaultUser.Text = oListControl_FailedLabTask.SelectedItems(0).Description.ToString()
        End If

    End Sub

    Private Sub oListControl_FailedLabTask_ItemClosedClick(ByVal sender As Object, ByVal e As EventArgs)

    End Sub

    Private Sub btnFailedLabTaskUserDelete_Click(sender As System.Object, e As System.EventArgs) Handles btnFailedLabTaskUserDelete.Click
        txtFailedLabTask_DefaultUser.Text = String.Empty
        _gloLabFailure_DefaultUserID = 0
        _gloLabFailure_defaultUserName = String.Empty
    End Sub

    'Private Sub chkEnabledCDS_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    txtCDS_PESUrl.Enabled = chkEnabledCDS.Checked
    '    txtCDS_PESUserName.Enabled = chkEnabledCDS.Checked
    '    txtCDS_PESPassword.Enabled = chkEnabledCDS.Checked

    'End Sub
    'Developer: Mitesh Patel
    'Date:28-Dec-2011'
    'PRD: OB Vitals Customization
    Private Sub ShowOBVitals_InTrv(ByVal SelectedV() As String)
        Dim oNode As TreeNode
        Dim oChildNode As TreeNode
        trv_OBVitals.Nodes.Clear()
        trv_OBVitals.CheckBoxes = True
        oNode = New TreeNode
        oNode.Text = "Dating Method"
        oNode.Tag = "Dating Method"

        oChildNode = New TreeNode
        oChildNode.Text = "Last Menstrual Period"
        oChildNode.Tag = "Last Menstrual Period"
        oNode.Nodes.Add(oChildNode)

        oChildNode = New TreeNode
        oChildNode.Text = "Est. Date Of Conception"
        oChildNode.Tag = "Est. Date Of Conception"
        oNode.Nodes.Add(oChildNode)

        oChildNode = New TreeNode
        oChildNode.Text = "Ultrasound"
        oChildNode.Tag = "Ultrasound"
        oNode.Nodes.Add(oChildNode)

        trv_OBVitals.Nodes.Add(oNode)
        oNode = New TreeNode
        oNode.Text = "Preferred Gestational Age"
        oNode.Tag = "Preferred Gestational Age"
        trv_OBVitals.Nodes.Add(oNode)

        oNode = New TreeNode
        oNode.Text = "Fundal Height"
        oNode.Tag = "Fundal Height"
        trv_OBVitals.Nodes.Add(oNode)

        oNode = New TreeNode
        oNode.Text = "Presentation"
        oNode.Tag = "Presentation"
        trv_OBVitals.Nodes.Add(oNode)

        oNode = New TreeNode
        oNode.Text = "Fetal Heart Rate"
        oNode.Tag = "Fetal Heart Rate"
        trv_OBVitals.Nodes.Add(oNode)

        oNode = New TreeNode
        oNode.Text = "Fetal Movement"
        oNode.Tag = "Fetal Movement"
        trv_OBVitals.Nodes.Add(oNode)

        oNode = New TreeNode
        oNode.Text = "Pre-Term Labor Signs/Symptoms"
        oNode.Tag = "Pre-Term Labor Signs/Symptoms"
        trv_OBVitals.Nodes.Add(oNode)

        oNode = New TreeNode
        oNode.Text = "Cervix Exam"
        oNode.Tag = "Cervix Exam"

        oChildNode = New TreeNode
        oChildNode.Text = "Dilation"
        oChildNode.Tag = "Dilation"
        oNode.Nodes.Add(oChildNode)

        oChildNode = New TreeNode
        oChildNode.Text = "Effacement"
        oChildNode.Tag = "Effacement"
        oNode.Nodes.Add(oChildNode)

        oChildNode = New TreeNode
        oChildNode.Text = "Station"
        oChildNode.Tag = "Station"
        oNode.Nodes.Add(oChildNode)

        trv_OBVitals.Nodes.Add(oNode)

        oNode = New TreeNode
        oNode.Text = "BP Setting"
        oNode.Tag = "BP Setting"

        oChildNode = New TreeNode
        oChildNode.Text = "BP Sitting"
        oChildNode.Tag = "BP Sitting"
        oNode.Nodes.Add(oChildNode)

        oChildNode = New TreeNode
        oChildNode.Text = "BP Standing"
        oChildNode.Tag = "BP Standing"
        oNode.Nodes.Add(oChildNode)

        trv_OBVitals.Nodes.Add(oNode)

        oNode = New TreeNode
        oNode.Text = "Weight"
        oNode.Tag = "Weight"
        trv_OBVitals.Nodes.Add(oNode)

        oNode = New TreeNode
        oNode.Text = "Weight Change"
        oNode.Tag = "Weight Change"
        trv_OBVitals.Nodes.Add(oNode)

        oNode = New TreeNode
        oNode.Text = "Pre-pregnancy Weight"
        oNode.Tag = "Pre-pregnancy Weight"
        trv_OBVitals.Nodes.Add(oNode)

        oNode = New TreeNode
        oNode.Text = "Urine Albumin/Glucose"
        oNode.Tag = "Urine Albumin/Glucose"
        trv_OBVitals.Nodes.Add(oNode)

        oNode = New TreeNode
        oNode.Text = "Edema"
        oNode.Tag = "Edema"
        trv_OBVitals.Nodes.Add(oNode)


        oNode = New TreeNode
        oNode.Text = "Pain Scale"
        oNode.Tag = "Pain Scale"
        trv_OBVitals.Nodes.Add(oNode)


        oNode = New TreeNode
        oNode.Text = "Comments"
        oNode.Tag = "Comments"
        trv_OBVitals.Nodes.Add(oNode)

        oNode = New TreeNode
        oNode.Text = "Obstetric History"
        oNode.Tag = "Obstetric History"

        oChildNode = New TreeNode
        oChildNode.Text = "Total Pregnancies"
        oChildNode.Tag = "Total Pregnancies"
        oNode.Nodes.Add(oChildNode)

        oChildNode = New TreeNode
        oChildNode.Text = "Full Term"
        oChildNode.Tag = "Full Term"
        oNode.Nodes.Add(oChildNode)

        oChildNode = New TreeNode
        oChildNode.Text = "Premature"
        oChildNode.Tag = "Premature"
        oNode.Nodes.Add(oChildNode)

        oChildNode = New TreeNode
        oChildNode.Text = "Aborted(Induced)"
        oChildNode.Tag = "Aborted(Induced)"
        oNode.Nodes.Add(oChildNode)

        oChildNode = New TreeNode
        oChildNode.Text = "Aborted(Spontaneous)"
        oChildNode.Tag = "Aborted(Spontaneous)"
        oNode.Nodes.Add(oChildNode)

        oChildNode = New TreeNode
        oChildNode.Text = "Ectopic"
        oChildNode.Tag = "Ectopic"
        oNode.Nodes.Add(oChildNode)

        oChildNode = New TreeNode
        oChildNode.Text = "Multiple Births"
        oChildNode.Tag = "Multiple Births"
        oNode.Nodes.Add(oChildNode)

        oChildNode = New TreeNode
        oChildNode.Text = "Living"
        oChildNode.Tag = "Living"
        oNode.Nodes.Add(oChildNode)

        trv_OBVitals.Nodes.Add(oNode)


        If IsNothing(SelectedV) = False Then
            If SelectedV.Length <> 0 Then
                Dim j As Integer
                Dim m As Integer
                Dim n As Integer
                Dim NodeNo() As String
                Dim RootChild() As String
                Dim CloneNode As TreeNode
                Dim CloneNode1 As TreeNode
                For j = 0 To SelectedV.Length - 1
                    NodeNo = SelectedV.GetValue(j).ToString().Split("#")
                    RootChild = NodeNo.GetValue(0).ToString().Split(".")
                    For m = 0 To trv_OBVitals.Nodes.Count - 1
                        If trv_OBVitals.Nodes(m).Tag = NodeNo.GetValue(1) Then
                            trv_OBVitals.Nodes(m).Checked = True
                        End If
                        If trv_OBVitals.Nodes(m).Nodes.Count <> 0 Then

                            For n = 0 To trv_OBVitals.Nodes(m).Nodes.Count - 1
                                If trv_OBVitals.Nodes(m).Nodes(n).Tag = NodeNo.GetValue(1) Then
                                    trv_OBVitals.Nodes(m).Nodes(n).Checked = True

                                    If m = RootChild.GetValue(0) Then

                                        If n = RootChild.GetValue(1) - 1 Then

                                        Else
                                            CloneNode = trv_OBVitals.Nodes(m).Nodes(Convert.ToInt16(RootChild.GetValue(1) - 1)).Clone()
                                            CloneNode1 = trv_OBVitals.Nodes(m).Nodes(n).Clone()
                                            trv_OBVitals.Nodes(m).Nodes.Remove(trv_OBVitals.Nodes(m).Nodes(n))
                                            trv_OBVitals.Nodes(m).Nodes.Insert(n, CloneNode)
                                            trv_OBVitals.Nodes(m).Nodes.Remove(trv_OBVitals.Nodes(m).Nodes(Convert.ToInt16(RootChild.GetValue(1) - 1)))
                                            trv_OBVitals.Nodes(m).Nodes.Insert(Convert.ToInt16(RootChild.GetValue(1) - 1), CloneNode1)
                                            Exit For

                                        End If
                                    Else

                                        If n = RootChild.GetValue(1) - 1 Then

                                        Else
                                            CloneNode = trv_OBVitals.Nodes(m).Nodes(Convert.ToInt16(RootChild.GetValue(1) - 1)).Clone()
                                            CloneNode1 = trv_OBVitals.Nodes(m).Nodes(n).Clone()
                                            trv_OBVitals.Nodes(m).Nodes.Remove(trv_OBVitals.Nodes(m).Nodes(n))
                                            trv_OBVitals.Nodes(m).Nodes.Insert(n, CloneNode)
                                            trv_OBVitals.Nodes(m).Nodes.Remove(trv_OBVitals.Nodes(m).Nodes(Convert.ToInt16(RootChild.GetValue(1) - 1)))
                                            trv_OBVitals.Nodes(m).Nodes.Insert(Convert.ToInt16(RootChild.GetValue(1) - 1), CloneNode1)

                                        End If

                                        CloneNode = trv_OBVitals.Nodes(Convert.ToInt16(RootChild.GetValue(0))).Clone()

                                        CloneNode1 = trv_OBVitals.Nodes(m).Clone()
                                        trv_OBVitals.Nodes.Remove(trv_OBVitals.Nodes(m))
                                        trv_OBVitals.Nodes.Insert(m, CloneNode)
                                        trv_OBVitals.Nodes.Remove(trv_OBVitals.Nodes(Convert.ToInt16(RootChild.GetValue(0))))
                                        trv_OBVitals.Nodes.Insert(Convert.ToInt16(RootChild.GetValue(0)), CloneNode1)
                                        Exit For
                                    End If
                                End If
                            Next
                        Else
                            ''
                            If RootChild.GetValue(1) = 0 And trv_OBVitals.Nodes(m).Tag = NodeNo.GetValue(1) Then
                                If m = RootChild.GetValue(0) Then

                                Else
                                    CloneNode = trv_OBVitals.Nodes(Convert.ToInt16(RootChild.GetValue(0))).Clone()
                                    CloneNode1 = trv_OBVitals.Nodes(m).Clone()
                                    trv_OBVitals.Nodes.Remove(trv_OBVitals.Nodes(m))
                                    trv_OBVitals.Nodes.Insert(m, CloneNode)
                                    trv_OBVitals.Nodes.Remove(trv_OBVitals.Nodes(Convert.ToInt16(RootChild.GetValue(0))))
                                    trv_OBVitals.Nodes.Insert(Convert.ToInt16(RootChild.GetValue(0)), CloneNode1)
                                    Exit For
                                End If
                            End If
                        End If
                    Next
                Next

            End If
        Else
            SelectAll_ClearAll_OBVitals("select")
        End If

        trv_OBVitals.ExpandAll()
    End Sub

    Private Sub btn_OBReset_Click(sender As System.Object, e As System.EventArgs) Handles btn_OBReset.Click
        ShowOBVitals_InTrv(Nothing)
        SelectAll_ClearAll_OBVitals("select")

    End Sub
    Private Sub SelectAll_ClearAll_OBVitals(ByVal sPara As String)
        Try
            If String.Compare(sPara, "select") = 0 Then
                Me.Cursor = Cursors.WaitCursor
                Dim nCount As Int16
                Dim nTotalNodes As Int16
                nTotalNodes = trv_OBVitals.GetNodeCount(False) - 1
                For nCount = 0 To nTotalNodes
                    trv_OBVitals.Nodes(nCount).Checked = True
                Next
                Me.Cursor = Cursors.Default
            Else
                Me.Cursor = Cursors.WaitCursor
                Dim nCount As Int16
                Dim nTotalNodes As Int16
                nTotalNodes = trv_OBVitals.GetNodeCount(False) - 1
                For nCount = 0 To nTotalNodes
                    trv_OBVitals.Nodes(nCount).Checked = False
                Next
                Me.Cursor = Cursors.Default
            End If

        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub btn_OBMoveUp_Click(sender As System.Object, e As System.EventArgs) Handles btn_OBMoveUp.Click
        Try
            Dim oNode As TreeNode
            Dim prevIndex As Integer
            If IsNothing(trv_OBVitals.SelectedNode) = False Then
                oNode = trv_OBVitals.SelectedNode.Clone
                If IsNothing(oNode) = False Then

                    If IsNothing(trv_OBVitals.SelectedNode.Parent) Then
                        If trv_OBVitals.SelectedNode.Index <> 0 Then
                            prevIndex = trv_OBVitals.SelectedNode.PrevNode.Index
                            trv_OBVitals.Nodes.Remove(trv_OBVitals.SelectedNode)
                            trv_OBVitals.Nodes.Insert(prevIndex, oNode)
                            trv_OBVitals.SelectedNode = oNode
                        End If
                    Else
                        If trv_OBVitals.SelectedNode.Index <> 0 Then
                            Dim PNode As TreeNode
                            PNode = trv_OBVitals.SelectedNode.Parent
                            prevIndex = trv_OBVitals.SelectedNode.PrevNode.Index
                            PNode.Nodes.Remove(trv_OBVitals.SelectedNode)
                            PNode.Nodes.Insert(prevIndex, oNode)
                            trv_OBVitals.SelectedNode = oNode

                        End If
                    End If
                End If
            End If
            trv_OBVitals.ExpandAll()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btn_OBMovedown_Click(sender As System.Object, e As System.EventArgs) Handles btn_OBMovedown.Click
        Try
            Dim oNode As TreeNode
            Dim nextIndex As Integer
            If IsNothing(trv_OBVitals.SelectedNode) = False Then
                oNode = trv_OBVitals.SelectedNode.Clone
                If IsNothing(oNode) = False Then

                    If IsNothing(trv_OBVitals.SelectedNode.Parent) Then
                        If trv_OBVitals.SelectedNode.Index <> trv_OBVitals.Nodes.Count - 1 Then
                            nextIndex = trv_OBVitals.SelectedNode.NextNode.Index
                            trv_OBVitals.Nodes.Remove(trv_OBVitals.SelectedNode)
                            trv_OBVitals.Nodes.Insert(nextIndex, oNode)
                            trv_OBVitals.SelectedNode = oNode
                        End If
                    Else
                        If trv_OBVitals.SelectedNode.Index <> trv_OBVitals.SelectedNode.Parent.Nodes.Count - 1 Then
                            Dim PNode As TreeNode
                            PNode = trv_OBVitals.SelectedNode.Parent
                            nextIndex = trv_OBVitals.SelectedNode.NextNode.Index
                            PNode.Nodes.Remove(trv_OBVitals.SelectedNode)
                            PNode.Nodes.Insert(nextIndex, oNode)
                            trv_OBVitals.SelectedNode = oNode
                        End If

                    End If
                End If
            End If
            trv_OBVitals.ExpandAll()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub trv_OBVitals_AfterCheck(sender As System.Object, e As System.Windows.Forms.TreeViewEventArgs) Handles trv_OBVitals.AfterCheck
        Dim blnChecked As Boolean = False
        Dim _Node As New TreeNode
        _Node = e.Node
        If _Node.Checked = True Then
            If _Node.Nodes.Count <> 0 Then
                For Each _Node In _Node.Nodes
                    _Node.Checked = True

                Next
            Else
                If IsNothing(_Node.Parent) = False Then
                    Dim cnt As Integer = 0
                    For Each _Node In _Node.Parent.Nodes
                        If _Node.Checked = True Then
                            cnt = cnt + 1
                        End If

                    Next
                    If cnt = _Node.Parent.Nodes.Count Then
                        Dim _PNode As New TreeNode
                        _PNode = e.Node.Parent

                        If _PNode.Checked = False Then

                            _PNode.Checked = True
                        End If
                    End If
                Else

                End If
            End If

        Else
            If _Node.Nodes.Count <> 0 Then
                If flagChk = False Then
                    For Each _Node In _Node.Nodes
                        _Node.Checked = False
                    Next
                End If
                flagChk = False
            Else
                If IsNothing(_Node.Parent) = False Then
                    Dim cnt As Integer = 0
                    For Each _Node In _Node.Parent.Nodes
                        If _Node.Checked = True Then
                            cnt = cnt + 1
                        End If
                    Next
                    If cnt <> _Node.Parent.Nodes.Count Then
                        Dim _PNode As New TreeNode
                        _PNode = e.Node.Parent

                        If _PNode.Checked = True Then

                            flagChk = True
                            _PNode.Checked = False
                        End If
                    End If
                Else

                End If
            End If

        End If

        Dim nCount As Int16
        Dim nTotalNodes As Int16
        nTotalNodes = trv_OBVitals.Nodes.Count - 1
        Dim ncheckedcnt As Int16
        For nCount = 0 To nTotalNodes
            If (trv_OBVitals.Nodes(nCount).Checked = True) Then
                ncheckedcnt = ncheckedcnt + 1
            End If
        Next
        If ncheckedcnt = nTotalNodes + 1 Then
            bOBVitalsSelectAll = True
            chk_OBSelectAll.Checked = True
        Else
            bOBVitalsSelectAll = False
            chk_OBSelectAll.Checked = False
        End If


    End Sub

    Private Sub chk_OBSelectAll_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chk_OBSelectAll.CheckedChanged
        Try
            If chk_OBSelectAll.Checked = True Then
                bOBVitalsSelectAll = True
                SelectAll_ClearAll_OBVitals("select")
            Else
                If bOBVitalsSelectAll = True Then
                    SelectAll_ClearAll_OBVitals("clear")
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btn_OBReset_MouseHover(sender As System.Object, e As System.EventArgs) Handles btn_OBReset.MouseHover
        Dim tooltip1 As New System.Windows.Forms.ToolTip
        tooltip1.SetToolTip(Me.btn_OBReset, "Reset")
    End Sub

    Private Sub btn_OBMovedown_MouseHover(sender As System.Object, e As System.EventArgs) Handles btn_OBMovedown.MouseHover
        Dim tooltip1 As New System.Windows.Forms.ToolTip
        tooltip1.SetToolTip(Me.btn_OBMovedown, "Move Down")
    End Sub

    Private Sub btn_OBMoveUp_MouseHover(sender As System.Object, e As System.EventArgs) Handles btn_OBMoveUp.MouseHover
        Dim tooltip1 As New System.Windows.Forms.ToolTip
        tooltip1.SetToolTip(Me.btn_OBMoveUp, "Move Up")
    End Sub

    Private Sub btnSPTestConnection_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSPTestConnection.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            TestgloCommunityConnection(False)
        Catch ex As Exception
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Function ConnectToSharePoint(ByVal ServerName As String, ByVal SiteName As String, ByVal AdfsSrvNm As String, Optional ByVal spCommurl As String = "") As Boolean

        Dim websService As New gloWeb.Webs
        Try
            If ConfigurationManager.AppSettings("Environment").ToLower() = "staging" Then
                websService.Url = ServerName & "/" & SiteName & "/_vti_bin/Webs.asmx"
                ''websService.Credentials = New NetworkCredential("Ujwala.atre", "glornd", "glodom")
                Dim strMessage As String = ""
                websService.UseDefaultCredentials = True
                websService.Credentials = CredentialCache.DefaultCredentials

                Dim myNode As XmlNode = websService.GetAllSubWebCollection()

                Dim nodes As XmlNodeList = myNode.SelectNodes("*")

                For Each node As XmlNode In nodes
                    strMessage = strMessage + node.Attributes("Title").Value & vbLf
                Next
                Return True
            Else
                If CheckADUserEmail() = True Then
                    Dim _IsSamalTokenget As Boolean = SetProductionEnvironment(ServerName, SiteName, AdfsSrvNm)
                    Return _IsSamalTokenget
                Else
                    Exit Function
                End If
            End If


            ''websService.Url = "http://" & ServerName & "/" & SiteName & "/_vti_bin/Webs.asmx?WSDL"

            ''MessageBox.Show(strMessage)

            ''readHtmlPage(url As String) 
            ''''''''''''''''''''''''''Commented following code by Ujwala as on 09082011 -  cause we r using one server name variable for all community site.
            '' '' ''If (spCommurl.Trim() <> "") Then
            '' '' ''    Dim objResponse As WebResponse
            '' '' ''    Dim objRequest As WebRequest
            '' '' ''    Dim result As String
            '' '' ''    Dim sCommunityServer As String() = ServerName.Split(":")


            '' '' ''    objRequest = System.Net.HttpWebRequest.Create("http://" & sCommunityServer(0).Trim() & "/my/person.aspx")
            '' '' ''    objRequest.UseDefaultCredentials = True
            '' '' ''    objRequest.Credentials = CredentialCache.DefaultCredentials

            '' '' ''    ''objRequest.Credentials = New NetworkCredential("Ujwala.atre", "glornd", "glodom")
            '' '' ''    '  objRequest.UseDefaultCredentials = True
            '' '' ''    ''("http://" & spCommurl & "/my/person.aspx")

            '' '' ''    objResponse = objRequest.GetResponse()
            '' '' ''    Dim sr As New StreamReader(objResponse.GetResponseStream())
            '' '' ''    result = sr.ReadToEnd()
            '' '' ''    sr.Close()
            '' '' ''End If
            'clean up StreamReader
            ''''''''''''''''''''''''''Commented following code by Ujwala as on 09082011 -  cause we r using one server name variable for all community site.


        Catch generatedExceptionName As Exception
            Return False
        Finally
            websService.Dispose()
        End Try

    End Function

    Private Function TestgloCommunityConnection(ByVal _IsSaveClose As Boolean) As Boolean 'Code Start-Added by kanchan on 20120802 for gloCommunity
        Dim objUserManagementService As UserManagement.UserManagementService = Nothing
        Dim objclsgloCommunityUsers As clsgloCommunityUsers = Nothing
        Try

            If txtSPName.Text.Trim = "" Or txtSPName.Text.Trim = "https://" Then
                MessageBox.Show("Enter gloCommunity Server Name.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtSPName.Focus()
                Return False
            End If
            If txtSPFolderNm.Text.Trim = "" Then
                MessageBox.Show("Enter gloCommunity Site Name.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtSPFolderNm.Focus()
                Return False
            End If

            'Code Start-Added by kanchan on 20120731 for gloCommunity

            objclsgloCommunityUsers = New clsgloCommunityUsers()
            If String.IsNullOrEmpty(mdlGeneral.SharepointAuthentication) = False Then
                If mdlGeneral.SharepointAuthentication.Trim().ToUpper() = "FORM" Then

                    mdlGeneral.gstrSharepointSrvNm = txtSPName.Text.Trim()
                    mdlGeneral.gstrSharepointSiteNm = txtSPFolderNm.Text.Trim()

                    If checkUserInSharepointUsers() Then
                        If objclsgloCommunityUsers.IsSiteExist() Then
                            If _IsSaveClose = False Then
                                MessageBox.Show("Connection established successfully.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            End If
                            Return True
                        Else
                            MessageBox.Show("Unable to connect to gloCommunity Server " & txtSPName.Text.Trim & " and Site " & txtSPFolderNm.Text.Trim, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Return False
                        End If
                    Else
                        MessageBox.Show("Unable to connect to gloCommunity Server " & txtSPName.Text.Trim & " and Site " & txtSPFolderNm.Text.Trim, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Return False
                    End If
                Else
                    If txtADFSSrvNm.Text.Trim = "" Then
                        MessageBox.Show("Enter ADFS Server Name.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        txtADFSSrvNm.Focus()
                        Return False
                    End If

                    If ConnectToSharePoint(txtSPName.Text.Trim(), txtSPFolderNm.Text.Trim(), txtADFSSrvNm.Text.Trim()) = False Then
                        If Not IsNothing(mdlGeneral.SharepointAuthentication) AndAlso mdlGeneral.SharepointAuthentication.Trim().ToUpper() = "CLAIM" Then
                            If _blnConfigured = True Then '' if User E-mail configured on AD.
                                If MessageBox.Show("Unable to connect to gloCommunity Server " & txtSPName.Text.Trim & " and Site " & txtSPFolderNm.Text.Trim, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information) Then
                                    tb_Settings.SelectTab(tb_Settings.TabPages.IndexOf(tbp_Sharepoint))
                                    txtSPFolderNm.Focus()
                                    Return False
                                End If
                            End If
                        End If
                    Else
                        If _IsSaveClose = False Then
                            MessageBox.Show("Connection established successfully.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If
                        Return True
                    End If
                End If
            End If
            'Code End-Added by kanchan on 20120731 for gloCommunity
        Catch ex As Exception
            objclsgloCommunityUsers = Nothing
        End Try
    End Function

#Region "gloCommunity"
    Private Function SetProductionEnvironment(ByVal ServerName As String, ByVal SiteName As String, ByVal AdfsSrvNm As String) As Boolean
        Dim websService As New gloWeb.Webs
        Try
            Dim cc As New CookieContainer()
            UpdateLog("Authenticate User Start")
            Dim samlToken As String = GetSamlToken(ServerName, SiteName, AdfsSrvNm)

            If Not String.IsNullOrEmpty(samlToken) Then
                Return True
                'Dim samlAuth As New Cookie("FedAuth", samlToken)
                'samlAuth.Expires = DateTime.Now.AddHours(1)
                'samlAuth.Path = "/"
                'samlAuth.Secure = True
                'samlAuth.HttpOnly = True
                'Dim samlUri As New Uri(ServerName & "/" & SiteName)
                'samlAuth.Domain = samlUri.Host
                'cc.Add(samlAuth)
            Else
                Return False
            End If

            'websService.Url = ServerName & "/" & SiteName & "/_vti_bin/Webs.asmx"
            'websService.CookieContainer = cc

            'Dim strMessage As String = ""
            'Dim myNode As XmlNode = websService.GetAllSubWebCollection()
            'Dim nodes As XmlNodeList = myNode.SelectNodes("*")
            'For Each node As XmlNode In nodes
            '    strMessage = strMessage + node.Attributes("Title").Value & vbLf
            'Next
            'Return True
        Catch generatedExceptionName As Exception
            Return False
        Finally
            websService.Dispose()
        End Try
    End Function

    Private Function GetSamlToken(ByVal ServerName As String, ByVal SiteName As String, ByVal AdfsSrvNm As String) As String

        Dim ret As String = String.Empty
        ''Dim ACSSiteWctx As String = "pr=wsfederation&rm=" + ConfigurationManager.AppSettings("urn") + "%3a" + ConfigurationManager.AppSettings("SpSiteName") + "%3a" + ConfigurationManager.AppSettings("apps") + "%3a" + ConfigurationManager.AppSettings("RelamSite") + "&ry=&cx=" + ConfigurationManager.AppSettings("https") + "%3a%2f%2f" + ConfigurationManager.AppSettings("SpServerName") + "%3a" + ConfigurationManager.AppSettings("Portno") + "%2f" + SiteName + "%2f_layouts%2fAuthenticate.aspx%3fSource%3d%252F" + SiteName + ""

        Dim strArr As String() = ServerName.Split(":")
        'strArr[0] for https,strArr[1] for SpServerName(Address),strArr[2] for port number
        Dim ACSSiteWctx As String = ""
        If strArr.Length > 0 Then
            If gIscommunitystaging = True Then
                ACSSiteWctx = "pr=wsfederation&rm=" + ConfigurationManager.AppSettings("urn") + "%3a" + ConfigurationManager.AppSettings("SpSiteName") + "%3a" + ConfigurationManager.AppSettings("apps") + "%3a" + ConfigurationManager.AppSettings("RelamSite") + "&ry=&cx=" + strArr(0) + "%3a%2f%2f" + strArr(1).Replace("//", "") + "%3a" + strArr(2) + "%2f" + SiteName + "%2f_layouts%2fAuthenticate.aspx%3fSource%3d%252F" + SiteName + ""
            Else
                ACSSiteWctx = "pr=wsfederation&rm=" + ConfigurationManager.AppSettings("Productionurn") + "%3a" + ConfigurationManager.AppSettings("ProductionSpSiteName") + "%3a" + ConfigurationManager.AppSettings("Productionapps") + "%3a" + ConfigurationManager.AppSettings("ProductionRelamSite") + "&ry=&cx=" + strArr(0) + "%3a%2f%2f" + strArr(1).Replace("//", "") + "%3a" + strArr(2) + "%2f" + SiteName + "%2f_layouts%2fAuthenticate.aspx%3fSource%3d%252F" + SiteName + ""
            End If

        End If

        UpdateLog("Wctx:" & ACSSiteWctx)
        Try
            'Step1. get token from STS (from ADFS, we need to get the SAML token)
            ''Dim stsUrl As String = "https://" + System.Net.Dns.GetHostName + "." + Environment.UserDomainName + ".com/adfs/services/trust/13/windowstransport"  ''Com2.glocom'' 'Complete ADFS end point where we need to send the request
            ''Dim stsUrl As String = "https://" + System.Net.Dns.GetHostByName(System.Net.Dns.GetHostName()).HostName + "/adfs/services/trust/13/windowstransport"  ''Com2.glocom'' 'Complete ADFS end point where we need to send the request


            If gIscommunitystaging = True Then
                ServiceNameSpace = ConfigurationManager.AppSettings("ServiceNameSpace")
            Else
                ServiceNameSpace = ConfigurationManager.AppSettings("ProductionServiceNameSpace")
            End If
            Dim stsUrl As String = "https://" + AdfsSrvNm + "/adfs/services/trust/13/windowstransport"  ''Com2.glocom'' 'Complete ADFS end point where we need to send the request
            UpdateLog("Adfs URL :" & stsUrl)
            UpdateLog("Get Response Start")
            Dim stsResponse As String = GetResponse(stsUrl)
            UpdateLog("Get Response End")
            'Step 2. generate response to ACS (Pass the Saml Token from ADFS to ACS)
            'Wreply: is the URL which ADFS will redirect with the resulting token 

            Dim ACSSite = New With { _
             Key .Wctx = ACSSiteWctx, _
             Key .Wreply = "https://" + ServiceNameSpace + ".accesscontrol.windows.net:443/v2/wsfederation"
            }



            Dim ADFSData As String = [String].Format("wa=wsignin1.0&wctx={0}&wresult={1}", HttpUtility.UrlEncode(ACSSite.Wctx), HttpUtility.UrlEncode(stsResponse))
            Dim ACSRequest As HttpWebRequest = TryCast(HttpWebRequest.Create(ACSSite.Wreply), HttpWebRequest) 'Create the next HTTPRequest to pass the Saml Token to the ACS URL
            ACSRequest.Method = "POST"
            ACSRequest.ContentType = "application/x-www-form-urlencoded"
            ACSRequest.AllowAutoRedirect = False
            Dim newStream As Stream = ACSRequest.GetRequestStream()
            Dim data As Byte() = Encoding.UTF8.GetBytes(ADFSData)   'Pass the SAMl details which we received from ADFS after encoding
            newStream.Write(data, 0, data.Length)
            newStream.Close()
            Dim acsResponse As HttpWebResponse = TryCast(ACSRequest.GetResponse(), HttpWebResponse)  ' Response from ACS


            ' Get the Saml Details from the Response object (of ACS)

            Dim myXMLDocument As XmlDocument = New XmlDocument
            Dim myXMLReader As XmlTextReader = New XmlTextReader(acsResponse.GetResponseStream())
            myXMLDocument.Load(myXMLReader)
            Dim html As String = myXMLDocument.InnerXml
            Dim escapedBody As String = ParseHtmlResponse(html)
            escapedBody = escapedBody.Replace("&amp;", "&")
            escapedBody = escapedBody.Replace("&lt;", "<")
            escapedBody = escapedBody.Replace("&gt;", ">")
            escapedBody = escapedBody.Replace("&apos;", "'")
            escapedBody = escapedBody.Replace("&quot;", """")


            'Step 3. generate response to Sharepoint (Pass the Saml Token to sharepoint & get FEDAuth cookie in response)
            'Wreply: is the URL which Sharepoint will redirect with the FEDAuth Cookie
            Dim sharepointRequest As HttpWebRequest
            Dim sharepointSite = New With { _
                Key .Wctx = ServerName + "/" + SiteName + "/_layouts/Authenticate.aspx?Source=%2F" + SiteName, _
                Key .Wreply = ServerName + "/" + SiteName + "_trust"
             }

            Dim ACSData As String = [String].Format("wa=wsignin1.0&wctx={0}&wresult={1}", HttpUtility.UrlEncode(sharepointSite.Wctx), HttpUtility.UrlEncode(escapedBody))
            sharepointRequest = TryCast(HttpWebRequest.Create(sharepointSite.Wreply), HttpWebRequest) 'Create the next HTTPRequest to pass the Saml Token to the Sharepoint Site
            sharepointRequest.Method = "POST"
            sharepointRequest.ContentType = "application/x-www-form-urlencoded"
            sharepointRequest.CookieContainer = New CookieContainer()
            sharepointRequest.AllowAutoRedirect = False

            Dim newStream_ As Stream = sharepointRequest.GetRequestStream()
            Dim data_ As Byte() = Encoding.UTF8.GetBytes(ACSData) 'Pass the SAMl details which we received from ACS after encoding
            newStream_.Write(data_, 0, data_.Length)
            newStream_.Close()

            Dim SharePointResponse As HttpWebResponse = TryCast(sharepointRequest.GetResponse(), HttpWebResponse)
            'Get FedAuth Cookie
            ret = SharePointResponse.Cookies("FedAuth").Value
        Catch ex As Exception
            '' MessageBox.Show("Error: " + ex.Message)
        End Try
        Return ret

    End Function

    Private Shared Function ParseHtmlResponse(ByVal html As String) As String
        Dim htmlDoc As HtmlAgilityPack.HtmlDocument = New HtmlAgilityPack.HtmlDocument
        htmlDoc.LoadHtml(html)
        Dim inputs As HtmlAgilityPack.HtmlNodeCollection = htmlDoc.DocumentNode.SelectNodes("//input")
        Dim parseResult As String = "missing wresult value"
        For Each htmlNode As HtmlAgilityPack.HtmlNode In inputs
            If htmlNode.OuterHtml.Contains("wresult") Then
                parseResult = htmlNode.GetAttributeValue("value", "missing wresult value")
                Exit For
            End If
        Next
        Return parseResult
    End Function

    Private Function GetResponse(ByVal stsUrl As String) As String
        Try


            Dim rst As New RequestSecurityToken()

            rst.RequestType = WSTrust13Constants.RequestTypes.Issue

            ''
            'bearer token, no encryption
            UpdateLog("Service NameSpace https://" + ServiceNameSpace + ".accesscontrol.windows.net/FederationMetadata/2007-06/FederationMetadata.xml")

            rst.AppliesTo = New EndpointAddress("https://" + ServiceNameSpace + ".accesscontrol.windows.net/FederationMetadata/2007-06/FederationMetadata.xml") ''ConfigurationManager.AppSettings("ACSRelyingPartyurl"
            ''("https://glodemo.accesscontrol.windows.net/FederationMetadata/2007-06/FederationMetadata.xml") ''(realm)

            rst.KeyType = WSTrust13Constants.KeyTypes.Bearer


            Dim trustSerializer As New WSTrust13RequestSerializer()
            Dim binding As New WSHttpBinding()

            binding.Security.Mode = SecurityMode.Transport

            binding.Security.Message.ClientCredentialType = MessageCredentialType.None
            binding.Security.Message.EstablishSecurityContext = False

            binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Windows
            Dim address As New EndpointAddress(stsUrl)
            UpdateLog(address.ToString())
            Dim trustClient As New ClientOmAuth.WSTrust13ContractClient(binding, address)

            trustClient.ClientCredentials.Windows.AllowNtlm = True
            trustClient.ClientCredentials.Windows.AllowedImpersonationLevel = TokenImpersonationLevel.Impersonation

            trustClient.ClientCredentials.Windows.ClientCredential = CredentialCache.DefaultNetworkCredentials

            Dim response As System.ServiceModel.Channels.Message = trustClient.EndIssue(trustClient.BeginIssue(System.ServiceModel.Channels.Message.CreateMessage(MessageVersion.[Default], WSTrust13Constants.Actions.Issue, New ClientOmAuth.RequestBodyWriter(trustSerializer, rst)), Nothing, Nothing))
            trustClient.Close()

            Dim reader As XmlDictionaryReader = response.GetReaderAtBodyContents()
            Return reader.ReadOuterXml()
        Catch ex As Exception
            UpdateLog("Error :" & ex.ToString)
            UpdateLog("Error Message:" & ex.Message)
            UpdateLog("Stack Trace:" & ex.StackTrace)
            UpdateLog("Innner Exception Message:" & ex.InnerException.Message)
            UpdateLog("Innner Exception Stack Trace:" & ex.InnerException.StackTrace)
        End Try
    End Function

    Private Sub chkglocomm_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkglocomm.CheckedChanged
        txtSPName.Enabled = chkglocomm.Checked
        txtSPFolderNm.Enabled = chkglocomm.Checked
        txtADFSSrvNm.Enabled = chkglocomm.Checked
        btnSPTestConnection.Enabled = chkglocomm.Checked
        pnlenvrioment.Enabled = chkglocomm.Checked
    End Sub

#Region "Check AD User E-mail"
    Dim _blnConfigured As Boolean = True
    Public Function CheckADUserEmail() As Boolean
        Dim CHKADResult As Integer = clsGetADUser.CheckADuser()
        If CHKADResult = 0 Then        ''If clsGetADUser.CheckADuser() = False Then
            Dim _Result As Integer = Convert.ToInt32(MessageBox.Show("User E-mail Id is not configured to Active Directory. Do you want to configure?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3))
            If _Result = Convert.ToInt32(DialogResult.Yes) Then
                Dim ofrmEmail As New gloCommunity.frmEmailConfig()
                Dim _frmResult As Integer = Convert.ToInt32(ofrmEmail.ShowDialog())
                If _frmResult = Convert.ToInt32(DialogResult.Cancel) Then
                    _blnConfigured = False
                End If
            Else
                _blnConfigured = False
            End If
        ElseIf CHKADResult = 2 Then
            MessageBox.Show("Windows Login User Does Not have Rights to Add E-mail Address in Active Directory." + vbCrLf + "Please Contact System Administrator.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            _blnConfigured = False
        End If
        Return _blnConfigured
    End Function
#End Region

    Private Function checkUserInSharepointUsers() As Boolean  'Code Start-Added by kanchan on 20120731 for gloCommunity
        Dim oclsencryption As clsEncryption = Nothing
        Dim ObjGCUSer As clsgloCommunityUsers = Nothing
        Try
            ''Added for check Login user available on SharePoint server if not then add 

            ObjGCUSer = New clsgloCommunityUsers()
            Dim dtUSer As DataTable = ObjGCUSer.getGCUser(gnLoginID, gIscommunitystaging)

            If Not IsNothing(dtUSer) And dtUSer.Rows.Count > 0 Then
                ''get gloCommunity username & password as per login id
                oclsencryption = New clsEncryption()
                Dim _encryptionKey As String = "12345678"
                Dim _strEncryptPWD As String = oclsencryption.DecryptFromBase64String(dtUSer.Rows(0)("gc_sPassword").ToString(), _encryptionKey)
                mdlGeneral.gstrGCUserName = dtUSer.Rows(0)("gc_sUserName").ToString()
                mdlGeneral.gstrGCPassword = _strEncryptPWD

            Else
                ''add gloCommunity user
                Dim frmAddUser As frmAddGCUser = New frmAddGCUser()
                frmAddUser.ShowDialog()
                If mdlGeneral.gstrGCUserName = "" Then
                    Return False
                End If

                ''mnuSharepoint.ShowDropDown()
            End If
            Return True
        Catch ex As Exception
            Return False
        Finally
            oclsencryption = Nothing
            ObjGCUSer = Nothing
        End Try
    End Function


#End Region


    Private Sub lnkDirectAdmin_LinkClicked(sender As System.Object, e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkDirectAdmin.LinkClicked
        Dim sInfo As New ProcessStartInfo(e.Link.LinkData.ToString())
        Process.Start(sInfo)
    End Sub

    Private Sub lnkDirectSSO_LinkClicked(sender As System.Object, e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkDirectSSO.LinkClicked
        Dim sInfo As New ProcessStartInfo(e.Link.LinkData.ToString())
        Process.Start(sInfo)
    End Sub

    Private Sub Fill_SignatureFormat()
        Dim dtSignature As New DataTable()
        Dim dcId As New DataColumn("ID")
        Dim dcDescription As New DataColumn("Description")
        dtSignature.Columns.Add(dcId)
        dtSignature.Columns.Add(dcDescription)

        Dim drTemp As DataRow
        drTemp = dtSignature.NewRow()
        drTemp("ID") = "1"
        drTemp("Description") = "Dr. John Smith. M.D. 12/01/2012 03:45:59 PM (jsmith)"
        dtSignature.Rows.Add(drTemp)

        drTemp = dtSignature.NewRow()
        drTemp("ID") = "2"
        drTemp("Description") = "John Smith. M.D. 12/01/2012 03:45:59 PM (jsmith)"
        dtSignature.Rows.Add(drTemp)

        drTemp = dtSignature.NewRow()
        drTemp("ID") = "3"
        drTemp("Description") = "Dr. John Smith. M.D. 12/01/2012 03:45:59 PM"
        dtSignature.Rows.Add(drTemp)

        drTemp = dtSignature.NewRow()
        drTemp("ID") = "4"
        drTemp("Description") = "John Smith. M.D. 12/01/2012 03:45:59 PM"
        dtSignature.Rows.Add(drTemp)


        cmbSignatureFormat.DataSource = dtSignature

        '  cmbSurgicalAlertUser.SelectedIndex = 0
        cmbSignatureFormat.ValueMember = dtSignature.Columns("ID").ColumnName
        cmbSignatureFormat.DisplayMember = dtSignature.Columns("Description").ColumnName
    End Sub

    Private Function GetVISDocument() As Boolean
        Dim oDB As New gloStream.gloDataBase.gloDataBase

        Dim cnt As Integer = 0
        Dim _strSQL As String = ""
        Dim blnFound As Boolean = False

        Try
            oDB.Connect(gstrConnectionString)
            _strSQL = "SELECT count(im_nDocumentID) as DocID FROM IM_MST WHERE im_nDocumentID IS NOT NULL AND im_nDocumentID > 0"

            cnt = oDB.ExecuteQueryScaler(_strSQL)
            If cnt > 0 Then
                blnFound = True
            Else
                _strSQL = "SELECT  count(nDocumentID) as DocID   FROM IM_Trn_Dtl WHERE nDocumentID IS NOT NULL AND nDocumentID > 0"
                cnt = oDB.ExecuteQueryScaler(_strSQL)
                If cnt > 0 Then
                    blnFound = True
                End If
            End If

            Return blnFound

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            oDB.Disconnect()
            If Not IsNothing(oDB) Then
                oDB = Nothing
            End If
        End Try
    End Function

    Private Sub rb_staging_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rb_staging.CheckedChanged
        gIscommunitystaging = True

        If rb_staging.Checked = True Then
            rb_staging.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            rb_staging.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
        If isFormLoad = False Then
            txtSPName.Clear()
            txtSPFolderNm.Clear()
        End If

    End Sub

    Private Sub rb_Production_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rb_Production.CheckedChanged
        gIscommunitystaging = False
        If rb_Production.Checked = True Then
            rb_Production.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            rb_Production.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
        If isFormLoad = False Then
            txtSPName.Clear()
            txtSPFolderNm.Clear()
        End If
    End Sub

    'New Event Added By Manoj Jadhav On 20120419 For Invoke Midmark IQ ECG Settings Screen
    Private Sub btnShowMidmarkMidmarkECGSettings_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShowMidmarkMidmarkECGSettings.Click
        Dim objFrmDeviceActivation As FrmDeviceActivation
        Try
            If CheckAUSUsername() Then
                objFrmDeviceActivation = New FrmDeviceActivation(FrmDeviceActivation.DeviceSettings.MidmarkECGDevice)
                objFrmDeviceActivation.ShowDialog()
                If objFrmDeviceActivation.IsSettingChanged Then
                    chkUseMidmarkECGDevice.Checked = True
                    chkUseMidmarkECGDevice.Enabled = True
                Else
                    chkUseMidmarkECGDevice.Checked = False
                    chkUseMidmarkECGDevice.Enabled = False
                End If
            End If

        Catch ex As Exception
            ex = Nothing
        Finally
            If Not objFrmDeviceActivation Is Nothing Then
                objFrmDeviceActivation.Dispose()
                objFrmDeviceActivation = Nothing
            End If

        End Try
    End Sub


    'New Event Added By Manoj Jadhav On 20120419 For Invoke Midmark IQ ECG Settings Screen Disable Midmark ECG Device Setting 
    Private Sub chkUseMidmarkECGDevice_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkUseMidmarkECGDevice.Click
        If Not chkUseMidmarkECGDevice.Checked Then
            If MessageBox.Show("Are you sure you want to disable Midmark IQ ECG device interface?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                If DisableDeviceSettings(FrmDeviceActivation.DeviceSettings.MidmarkECGDevice) Then
                    chkUseMidmarkECGDevice.Checked = False
                    chkUseMidmarkECGDevice.Enabled = False
                End If
            Else
                chkUseMidmarkECGDevice.Checked = True
                chkUseMidmarkECGDevice.Enabled = True
            End If
        End If
    End Sub

    Private Function getWidthofListItems(ByVal _text As String, ByVal combo As ComboBox) As Integer
        Dim g As Graphics = Me.CreateGraphics()
        Dim s As SizeF = g.MeasureString(_text, combo.Font)
        Dim width As Integer = Convert.ToInt32(s.Width)
        Return width
    End Function

    Dim combo As New ComboBox
    Dim ToolTip1 As New ToolTip

    Private Sub cmbCountry_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbCountry.SelectedIndexChanged
        combo = DirectCast(sender, ComboBox)
        If cmbCountry.SelectedItem IsNot Nothing Then
            If getWidthofListItems(Convert.ToString(DirectCast(cmbCountry.Items(cmbCountry.SelectedIndex), DataRowView)("sName")), cmbCountry) >= cmbCountry.DropDownWidth - 20 Then
                Dim txt As String = Convert.ToString(DirectCast(cmbCountry.Items(cmbCountry.SelectedIndex), DataRowView)("sName"))
                If ToolTip1.GetToolTip(cmbCountry) <> txt Then
                    ToolTip1.SetToolTip(cmbCountry, txt)
                End If
            Else
                Me.ToolTip1.SetToolTip(cmbCountry, "")

            End If
        End If

    End Sub

    Private Sub cmbCountry_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbCountry.MouseEnter
        combo = DirectCast(sender, ComboBox)
        If cmbCountry.SelectedItem IsNot Nothing Then
            If getWidthofListItems(Convert.ToString(DirectCast(cmbCountry.Items(cmbCountry.SelectedIndex), DataRowView)("sName")), cmbCountry) >= cmbCountry.DropDownWidth - 20 Then
                Dim txt As String = Convert.ToString(DirectCast(cmbCountry.Items(cmbCountry.SelectedIndex), DataRowView)("sName"))
                If ToolTip1.GetToolTip(cmbCountry) <> txt Then
                    ToolTip1.SetToolTip(cmbCountry, txt)
                End If
            Else
                Me.ToolTip1.SetToolTip(cmbCountry, "")

            End If
        End If
    End Sub
    Private Sub cmbFutureApptType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbFutureApptType.SelectedIndexChanged
        combo = DirectCast(sender, ComboBox)
        If cmbFutureApptType.SelectedItem IsNot Nothing Then
            If getWidthofListItems(Convert.ToString(DirectCast(cmbFutureApptType.Items(cmbFutureApptType.SelectedIndex), DataRowView)("nAppointmentTypeID")), cmbFutureApptType) >= cmbFutureApptType.DropDownWidth - 20 Then
                Dim txt As String = Convert.ToString(DirectCast(cmbFutureApptType.Items(cmbFutureApptType.SelectedIndex), DataRowView)("nAppointmentTypeID"))
                If ToolTip1.GetToolTip(cmbFutureApptType) <> txt Then
                    ToolTip1.SetToolTip(cmbFutureApptType, txt)
                End If
            Else
                Me.ToolTip1.SetToolTip(cmbFutureApptType, "")

            End If
        End If

    End Sub

    Private Sub cmbFutureApptType_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbFutureApptType.MouseEnter
        combo = DirectCast(sender, ComboBox)
        If cmbFutureApptType.SelectedItem IsNot Nothing Then
            If getWidthofListItems(Convert.ToString(DirectCast(cmbFutureApptType.Items(cmbFutureApptType.SelectedIndex), DataRowView)("nAppointmentTypeID")), cmbFutureApptType) >= cmbFutureApptType.DropDownWidth - 20 Then
                Dim txt As String = Convert.ToString(DirectCast(cmbFutureApptType.Items(cmbFutureApptType.SelectedIndex), DataRowView)("nAppointmentTypeID"))
                If ToolTip1.GetToolTip(cmbFutureApptType) <> txt Then
                    ToolTip1.SetToolTip(cmbFutureApptType, txt)
                End If
            Else
                Me.ToolTip1.SetToolTip(cmbFutureApptType, "")

            End If
        End If
    End Sub
    Private Sub cmbSameDayApptType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbSameDayApptType.SelectedIndexChanged
        combo = DirectCast(sender, ComboBox)
        If cmbSameDayApptType.SelectedItem IsNot Nothing Then
            If getWidthofListItems(Convert.ToString(DirectCast(cmbSameDayApptType.Items(cmbSameDayApptType.SelectedIndex), DataRowView)("nAppointmentTypeID")), cmbSameDayApptType) >= cmbSameDayApptType.DropDownWidth - 20 Then
                Dim txt As String = Convert.ToString(DirectCast(cmbSameDayApptType.Items(cmbSameDayApptType.SelectedIndex), DataRowView)("nAppointmentTypeID"))
                If ToolTip1.GetToolTip(cmbSameDayApptType) <> txt Then
                    ToolTip1.SetToolTip(cmbSameDayApptType, txt)
                End If
            Else
                Me.ToolTip1.SetToolTip(cmbSameDayApptType, "")

            End If
        End If

    End Sub

    Private Sub cmbSameDayApptType_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbSameDayApptType.MouseEnter
        combo = DirectCast(sender, ComboBox)
        If cmbSameDayApptType.SelectedItem IsNot Nothing Then
            If getWidthofListItems(Convert.ToString(DirectCast(cmbSameDayApptType.Items(cmbSameDayApptType.SelectedIndex), DataRowView)("nAppointmentTypeID")), cmbSameDayApptType) >= cmbSameDayApptType.DropDownWidth - 20 Then
                Dim txt As String = Convert.ToString(DirectCast(cmbSameDayApptType.Items(cmbSameDayApptType.SelectedIndex), DataRowView)("nAppointmentTypeID"))
                If ToolTip1.GetToolTip(cmbSameDayApptType) <> txt Then
                    ToolTip1.SetToolTip(cmbSameDayApptType, txt)
                End If
            Else
                Me.ToolTip1.SetToolTip(cmbSameDayApptType, "")

            End If
        End If
    End Sub

    Dim tooltip As New ToolTip

    Private Sub ShowTooltipOnWriteOffComboBox(ByVal sender As Object, ByVal e As DrawItemEventArgs)
        combo = DirectCast(sender, ComboBox)
        If combo.Items.Count > 0 AndAlso e.Index >= 0 Then

            e.DrawBackground()
            Using br As New SolidBrush(e.ForeColor)
                e.Graphics.DrawString(combo.GetItemText(combo.Items(e.Index)).ToString(), e.Font, br, e.Bounds)
            End Using

            If (e.State And DrawItemState.Selected) = DrawItemState.Selected Then
                If combo.DroppedDown Then
                    If getWidthofListItems(combo.GetItemText(combo.Items(e.Index)).ToString(), combo) >= combo.DropDownWidth Then
                        Me.tooltip.Show(combo.GetItemText(combo.Items(e.Index)), combo, e.Bounds.Right - 300, e.Bounds.Top + 4)
                    End If
                Else
                    tooltip.Hide(combo)
                End If
            Else
                tooltip.Hide(combo)
            End If
            e.DrawFocusRectangle()
        End If
    End Sub


    Private Sub chkEnableIntuit_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkEnableIntuit.CheckedChanged
        If chkEnableIntuit.Checked = True Then
            chkIncludeOrignalMessage.Enabled = True
        Else
            chkIncludeOrignalMessage.Enabled = False
        End If
    End Sub

    Private Sub btnStaffIDMapping_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStaffIDMapping.Click
        Try
            Dim ofrm As New frmIntuitStaffMapping
            ofrm.StartPosition = FormStartPosition.CenterScreen
            ofrm.ShowDialog()
            ofrm.Dispose()
            ofrm = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

        End Try
    End Sub

    Private Sub chkUseMedfusion_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkUseMedfusion.CheckedChanged
        If chkUseMedfusion.Checked = True Then
            chkEnableIntuit.Enabled = True
        Else
            chkEnableIntuit.Enabled = False
            chkEnableIntuit.Checked = False
            'chkAcknowledgeEmailSend.Checked = False
            'chkAcknowledgeEmailSend.Enabled = False
        End If
    End Sub

    Private Sub TxtPracticeID_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtPracticeID.KeyPress
        If Regex.IsMatch(e.KeyChar, "[0-9\b]") = False Then    ' "^\d+$\b"
            e.Handled = True
        End If
    End Sub

    Private Sub txtRxEligibilitythreshold_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtRxEligibilitythreshold.KeyPress
        If Not ((e.KeyChar >= ChrW(48) And e.KeyChar <= ChrW(57)) Or e.KeyChar = ChrW(8)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub rbWarning_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbWarning.CheckedChanged
        If rbWarning.Checked = True Then
            rbWarning.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            rbWarning.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub rbMandatory_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbMandatory.CheckedChanged
        If rbMandatory.Checked = True Then
            rbMandatory.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            rbMandatory.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub rbOptional_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbOptional.CheckedChanged
        If rbOptional.Checked = True Then
            rbOptional.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            rbOptional.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub btnProviderMapping_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProviderMapping.Click
        Try
            Dim ofrm As New frmIntuitProviderMapping
            ofrm.StartPosition = FormStartPosition.CenterScreen
            ofrm.ShowDialog()
            ofrm.Dispose()
            ofrm = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnLocationMapping_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLocationMapping.Click
        Try
            Dim ofrm As New frmIntuitLocationMapping
            ofrm.StartPosition = FormStartPosition.CenterScreen
            ofrm.ShowDialog()
            ofrm.Dispose()
            ofrm = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnMessageMapping_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMessageMapping.Click
        Try
            ''User Story #89427: In gloEMR Admin, a new tab in task mapping screen: Online Patient forms should be created same as “Review Portal Users”.
            ''Added new form for portal & call if portal is enable else old form is called.
            If IspatientPortalEnabled() Then
                Dim ofrmIntuitMessageMapping_Portal As New frmIntuitMessageMapping_Portal()
                ofrmIntuitMessageMapping_Portal.StartPosition = FormStartPosition.CenterScreen
                ofrmIntuitMessageMapping_Portal.ShowDialog()
                ofrmIntuitMessageMapping_Portal.Dispose()
                ofrmIntuitMessageMapping_Portal = Nothing
            Else
                Dim ofrmMessageMapping As New frmIntuitMessageMapping()
                ofrmMessageMapping.StartPosition = FormStartPosition.CenterScreen
                ofrmMessageMapping.ShowDialog()
                ofrmMessageMapping.Dispose()
                ofrmMessageMapping = Nothing
            End If

            
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub chk_allGen_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chk_allGen.CheckedChanged
        chk_Gen_SaveandClose.Checked = chk_allGen.Checked
        chk_Gen_SaveandFinish.Checked = chk_allGen.Checked
    End Sub

    Private Sub chk_allhl7_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chk_allhl7.CheckedChanged
        chkhl7PatientReg.Checked = chk_allhl7.Checked
        chk_HL7_SendCharges_SaveClose.Checked = chk_allhl7.Checked
        chk_HL7_SendCharges_SaveFinish.Checked = chk_allhl7.Checked
        chkHL7Immunization.Checked = chk_allhl7.Checked
        chkHL7Appointment.Checked = chk_allhl7.Checked
        chk_HL7_SendVisitSum_SaveClose.Checked = chk_allhl7.Checked
        Chk_HL7_SendVisitSum_SaveFinish.Checked = chk_allhl7.Checked
    End Sub

    Private Sub chkhl7outb_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkhl7outb.CheckedChanged
        Try
            RemoveHandler chkhl7outb.CheckedChanged, AddressOf chkhl7outb_CheckedChanged

            If (chkhl7outb.Checked = True) Then
                Dim dlg As DialogResult = MessageBox.Show(" This will enable HL7 outbound file generation on all client machines.", gstrMessageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Information)
                If (dlg = Windows.Forms.DialogResult.Cancel) Then
                    chkhl7outb.Checked = False

                End If
            Else
                Dim dlg As DialogResult = MessageBox.Show("Are you sure you want to disable HL7 outbound file generation on all client machines?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                If (dlg = Windows.Forms.DialogResult.No) Then
                    chkhl7outb.Checked = True

                End If

            End If
        Catch ex As Exception

        Finally
            AddHandler chkhl7outb.CheckedChanged, AddressOf chkhl7outb_CheckedChanged

        End Try
        EnableDisableDefaultInterfaceSettings()
    End Sub

    Private Sub chkgenoutb_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkgenoutb.CheckedChanged
        Try
            RemoveHandler chkgenoutb.CheckedChanged, AddressOf chkgenoutb_CheckedChanged

            If (chkgenoutb.Checked = True) Then
                Dim dlg As DialogResult = MessageBox.Show(" This will enable Genius outbound charges generation on all client machines.", gstrMessageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Information)
                If (dlg = Windows.Forms.DialogResult.Cancel) Then
                    chkgenoutb.Checked = False

                End If
            Else
                Dim dlg As DialogResult = MessageBox.Show("Are you sure you want to disable Genius outbound charges generation on all client machines?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                If (dlg = Windows.Forms.DialogResult.No) Then
                    chkgenoutb.Checked = True

                End If

            End If
        Catch ex As Exception

        Finally
            AddHandler chkgenoutb.CheckedChanged, AddressOf chkgenoutb_CheckedChanged

        End Try
        EnableDisableDefaultInterfaceSettings()
    End Sub

    Private Sub fillCommunicationPrefence()

        Dim oDB As New gloDatabaseLayer.DBLayer(gstrConnectionString)
        Dim oParamater As New gloDatabaseLayer.DBParameters()
        Dim dtCommunicationPrefence As DataTable
        If oDB IsNot Nothing Then
            If oDB.Connect(False) Then

                Try

                    oParamater.Add("@nClinicID", _ClinicID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt)
                    oDB.Retrive("GetsysCommunicationPrefence", oParamater, dtCommunicationPrefence)

                    If oParamater IsNot Nothing Then
                        oParamater.Dispose()
                        oParamater = Nothing
                    End If
                    If oDB IsNot Nothing Then
                        oDB.Disconnect()
                        oDB.Dispose()
                        oDB = Nothing
                    End If

                    If dtCommunicationPrefence IsNot Nothing Then
                        cmbMsgCommPref.DataSource = Nothing
                        cmbMsgCommPref.DataSource = dtCommunicationPrefence
                        cmbMsgCommPref.DisplayMember = "name"
                        cmbMsgCommPref.ValueMember = "id"

                        'Binding the Datasource to the Letter Comunication Pref Combo
                        cmbLettersCommPref.DataSource = Nothing
                        cmbLettersCommPref.DataSource = dtCommunicationPrefence.Copy()
                        cmbLettersCommPref.DisplayMember = "name"
                        cmbLettersCommPref.ValueMember = "id"

                        'Binding the Datasource to the SecureMessage Comunication Pref Combo
                        CmbSecureMsgComPref.DataSource = Nothing
                        CmbSecureMsgComPref.DataSource = dtCommunicationPrefence.Copy()
                        CmbSecureMsgComPref.DisplayMember = "name"
                        CmbSecureMsgComPref.ValueMember = "id"

                    End If
                Catch ex As gloDatabaseLayer.DBException
                    ex.ERROR_Log(ex.ToString())
                    If oParamater IsNot Nothing Then
                        oParamater.Dispose()
                        oParamater = Nothing
                    End If
                    If oDB IsNot Nothing Then
                        oDB.Disconnect()
                        oDB.Dispose()
                        oDB = Nothing
                    End If
                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
                    If oParamater IsNot Nothing Then
                        oParamater.Dispose()
                        oParamater = Nothing
                    End If
                    If oDB IsNot Nothing Then
                        oDB.Disconnect()
                        oDB.Dispose()
                        oDB = Nothing
                    End If
                Finally
                    '''/cmbCommPref.Text = itm;
                    ''' 
                    If oParamater IsNot Nothing Then
                        oParamater.Dispose()
                        oParamater = Nothing
                    End If
                    If oDB IsNot Nothing Then
                        oDB.Disconnect()
                        oDB.Dispose()
                        oDB = Nothing
                    End If
                End Try
                'odbConnectFalse
            End If
        End If


    End Sub
    Private Function CheckPendingRefillRequest() As String
        Dim strresult As String = ""
        Dim oDB As gloDatabaseLayer.DBLayer = Nothing
        Dim dtProviderNames As DataTable = Nothing
        Try
            oDB = New gloDatabaseLayer.DBLayer(gstrConnectionString)
            If oDB IsNot Nothing Then
                If oDB.Connect(False) Then
                    oDB.Retrive("gsp_CheckPendingRefills", dtProviderNames)
                    If dtProviderNames IsNot Nothing Then
                        If dtProviderNames.Rows.Count > 0 Then
                            strresult = dtProviderNames.Rows(0)(0).ToString
                        End If
                    End If
                End If
            End If
            Return strresult
        Catch ex As Exception
            Return strresult
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        Finally
            If oDB IsNot Nothing Then
                oDB.Disconnect()
                oDB.Dispose()
                oDB = Nothing
            End If
            If dtProviderNames IsNot Nothing Then
                dtProviderNames.Dispose()
                dtProviderNames = Nothing
            End If
        End Try
    End Function

    Private Sub chkNCPDPVer10dot6_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkNCPDPVer10dot6.CheckedChanged

        If chkNCPDPVer10dot6.Checked = True Then
            Dim strProviders As String = Nothing
            strProviders = CheckPendingRefillRequest()
            If Trim(strProviders) <> "" Then
                Dim msg As String = "v10.6 Rx features cannot be enabled while there are pending refill requests. Please work the refill requests before changing this setting." + Environment.NewLine + Environment.NewLine + "Refill Requests are outstanding for the following providers:" + Environment.NewLine + Environment.NewLine + strProviders
                MessageBox.Show(msg, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                RemoveHandler chkNCPDPVer10dot6.CheckedChanged, AddressOf chkNCPDPVer10dot6_CheckedChanged
                chkNCPDPVer10dot6.Checked = False
                AddHandler chkNCPDPVer10dot6.CheckedChanged, AddressOf chkNCPDPVer10dot6_CheckedChanged
                Exit Sub
            Else
                Dim msg As String = " The setting ' NCPDP Script 10.6 ' once enabled and saved, cannot be disabled. Are you sure you want to enable the setting?"
                If MessageBox.Show(msg, gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then
                    chkNCPDPVer10dot6.Enabled = False

                    If chkNCPDPVer10dot6.Checked = True Then
                        If chkMedHistory.Checked = True Then
                            chkMedHistory.Enabled = True
                            txtMedHistoryPortalURL.Enabled = True
                        Else
                            chkMedHistory.Enabled = True
                            txtMedHistoryPortalURL.Enabled = False
                        End If
                    Else
                        chkMedHistory.Enabled = False
                        txtMedHistoryPortalURL.Enabled = False
                    End If

                    Is8dot1PendingRefReqComplete = True
                    If rbStaging.Checked = True Then
                        TxtSurescriptURL.Text = s10dot6StagingURl
                    ElseIf rbProduction.Checked = True Then
                        TxtSurescriptURL.Text = s10dot6ProductionURl
                    End If
                    pnlSecureMessage.Enabled = True
                Else
                    RemoveHandler chkNCPDPVer10dot6.CheckedChanged, AddressOf chkNCPDPVer10dot6_CheckedChanged
                    chkNCPDPVer10dot6.Checked = False
                    AddHandler chkNCPDPVer10dot6.CheckedChanged, AddressOf chkNCPDPVer10dot6_CheckedChanged
                    Exit Sub
                End If
            End If
        Else
            If rbStaging.Checked = True Then
                TxtSurescriptURL.Text = sStagingURl
            Else
                TxtSurescriptURL.Text = sProductionURl
            End If
        End If
    End Sub

    Private Sub rdoVitalMU1New_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoVitalMU1New.CheckedChanged
        If rdoVitalMU1New.Checked = True Then
            rdoVitalAllRequired.Checked = True
            grpVitalSub.Enabled = True
            rdoVitalMU1New.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            rdoVitalAllRequired.Checked = False
            rdoVitalBPRequired.Checked = False
            rdoVitalHeightWeightRequired.Checked = False
            grpVitalSub.Enabled = False
            rdoVitalMU1New.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub rdoVitalMU1Current_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoVitalMU1Current.CheckedChanged
        If rdoVitalMU1Current.Checked = True Then
            rdoVitalMU1Current.Font = New Font("Tahoma", 9, FontStyle.Bold)
            grpVitalSub.Enabled = False
        Else
            rdoVitalMU1Current.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub chkSecureMesaage_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkSecureMesaage.CheckedChanged
        If chkSecureMesaage.Checked = True Then
            rbSecureStaging.Enabled = True
            rbSecureProduction.Enabled = True
            txtSecureMessageURL.Enabled = True
            chkPatientSavings.Enabled = True
        Else
            rbSecureStaging.Enabled = False
            rbSecureProduction.Enabled = False
            txtSecureMessageURL.Enabled = False
            chkPatientSavings.Enabled = False
            chkPatientSavings.Checked = False
        End If
    End Sub

    Private Sub rbSecureStaging_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbSecureStaging.CheckedChanged
        If rbSecureStaging.Checked = True Then
            rbSecureStaging.Font = New Font("Tahoma", 9, FontStyle.Bold)
            If rbSecureStaging.Checked = True Then
                txtSecureMessageURL.Text = sSecureMsgStaging
            Else
                txtSecureMessageURL.Text = sSecureMsgProduction
            End If
        Else
            rbSecureStaging.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub rbSecureProduction_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbSecureProduction.CheckedChanged
        If rbSecureProduction.Checked = True Then
            rbSecureProduction.Font = New Font("Tahoma", 9, FontStyle.Bold)
            If rbSecureProduction.Checked = True Then
                txtSecureMessageURL.Text = sSecureMsgProduction
            Else
                txtSecureMessageURL.Text = sSecureMsgStaging
            End If
        Else
            rbSecureProduction.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    ''7022Items: Home Billing- Added checked changed event to show messagebox as mention in PRD.
    Private Sub rbtn_UseAreaCodeForPatientYes_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbtn_UseAreaCodeForPatientYes.CheckedChanged
        Try
            If rbtn_UseAreaCodeForPatientYes.Checked = True Then
                Dim msg As String = "Before using this setting, discuss with a gloStream representative." + Environment.NewLine + "Continue?"
                If MessageBox.Show(msg, gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.No Then
                    rbtn_UseAreaCodeForPatientNo.Checked = True
                    rbtn_UseAreaCodeForPatientYes.Checked = False
                Else
                    rbtn_UseAreaCodeForPatientNo.Checked = False
                    rbtn_UseAreaCodeForPatientYes.Checked = True
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
            ex = Nothing
        End Try
    End Sub

    Private Sub chkMU2Features_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)


        '08-Apr-13 Aniket: Disable Multiple Race if the setting is checked

        If chkMU2Features.Checked = True Then

            If (MsgBox("The setting 'Enable Multiple Race Features' once enabled and saved, cannot be disabled. Are you sure you want to enable the setting?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No) Then
                RemoveHandler chkMU2Features.CheckedChanged, AddressOf chkMU2Features_CheckedChanged
                chkMU2Features.Checked = False
                AddHandler chkMU2Features.CheckedChanged, AddressOf chkMU2Features_CheckedChanged
            End If

        End If

    End Sub


    Private Sub btnStyleSheetPath_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnStyleSheetPath.Click
        Try
            With FolderBrowserDialog1()
                .ShowNewFolderButton = True
                .Description = "Select Style Sheet Path"
                If .ShowDialog() = DialogResult.OK Then
                    txtStyleSheetPath.Text = .SelectedPath
                End If
            End With
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnAuthenticateCCDAservice_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAuthenticateCCDAservice.Click
        Try
            Dim _isAUSUserNameConfigured As Boolean = False
            _isAUSUserNameConfigured = CheckAUSUsername()

            If _isAUSUserNameConfigured Then

                Dim objFrmDeviceActivation As FrmDeviceActivation
                Try
                    objFrmDeviceActivation = New FrmDeviceActivation(FrmDeviceActivation.DeviceSettings.CCDAService)
                    objFrmDeviceActivation.ShowDialog()

                    If objFrmDeviceActivation.IsSettingChanged Then
                        chkEnableCCDAService.Checked = True
                    Else
                        chkEnableCCDAService.Checked = False
                    End If

                Catch ex As Exception
                    ex = Nothing

                Finally
                    If Not objFrmDeviceActivation Is Nothing Then
                        objFrmDeviceActivation.Dispose()
                        objFrmDeviceActivation = Nothing
                    End If

                End Try
            End If
        Catch ex As Exception
            ex = Nothing
        Finally

        End Try
    End Sub

    Private Sub chkEnableCCDAService_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkEnableCCDAService.CheckedChanged
        If chkEnableCCDAService.Checked = True Then
            chkEnableCCDAService.Enabled = True
        Else
            chkEnableCCDAService.Enabled = False
            chkEnableCCDAService.Checked = False
        End If
    End Sub

    Private Sub chkEnableCCDAService_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkEnableCCDAService.Click
        If Not chkEnableCCDAService.Checked Then
            If MessageBox.Show("Are you sure you want to disable gloCCDA service?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                If DisableDeviceSettings(FrmDeviceActivation.DeviceSettings.CCDAService) Then
                    chkEnableCCDAService.Checked = False
                End If
            Else
                chkEnableCCDAService.Checked = True
            End If
        End If
    End Sub

    Private Sub chkPatientSavings_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkPatientSavings.CheckedChanged
        Dim bIsPatientSavingsChecked As Boolean = chkPatientSavings.Checked

        If Not bIsPatientSavingsChecked Then
            chkPatientSavingInbox.Checked = False
            chkPatientSavingInbox.Enabled = False
        Else
            chkPatientSavingInbox.Enabled = True
        End If

    End Sub

    Private Sub chkICD10PalnOverride_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkICD10PalnOverride.CheckedChanged
        Dim _List As IEnumerable
        Dim oClsICD10Settings As clsICD10Settings = Nothing
        Try
            oClsICD10Settings = New clsICD10Settings()
            If chkICD10PalnOverride.Checked = True Then
                _List = oClsICD10Settings.FetchPlanLevelICDDOS(True)
            Else
                _List = oClsICD10Settings.FetchPlanLevelICDDOS()
            End If

            C1ICD10DOS.DataSource = _List
            C1ICD10DOS.Refresh()
            DesignGridForICD10TransitionSetting(C1ICD10DOS)
        Catch ex As Exception
            UpdateErrorLog(ex.ToString)
        Finally
            _List = Nothing
            If Not IsNothing(oClsICD10Settings) Then
                oClsICD10Settings.Dispose()
                oClsICD10Settings = Nothing
            End If

        End Try
    End Sub

    Public Enum ICD10Grid
        ContactID
        ContactName
        DOS
    End Enum

    Private Sub DesignGridForICD10TransitionSetting(ByRef c1Grid As C1FlexGrid)
        c1Grid.Cols.Count = 3
        c1Grid.Cols(ICD10Grid.ContactID).Visible = False
        c1Grid.Cols(ICD10Grid.ContactName).Visible = True
        c1Grid.Cols(ICD10Grid.DOS).Visible = True

        c1Grid.SetData(0, ICD10Grid.ContactID, "ContactID")
        c1Grid.SetData(0, ICD10Grid.ContactName, "Insurance Plan Overrides")
        c1Grid.SetData(0, ICD10Grid.DOS, "ICD10 DOS")

        c1Grid.Cols(ICD10Grid.ContactID).AllowEditing = False
        c1Grid.Cols(ICD10Grid.ContactName).AllowEditing = False
        c1Grid.Cols(ICD10Grid.DOS).AllowEditing = True

        c1Grid.Cols(ICD10Grid.ContactID).Width = 50
        c1Grid.Cols(ICD10Grid.ContactName).Width = 500
        c1Grid.Cols(ICD10Grid.DOS).Width = 50

        c1Grid.Cols(ICD10Grid.DOS).AllowResizing = False
        c1Grid.Cols(ICD10Grid.ContactName).AllowResizing = False

        c1Grid.Cols(ICD10Grid.DOS).AllowDragging = False
        c1Grid.Cols(ICD10Grid.ContactName).AllowDragging = False

        c1Grid.Cols(ICD10Grid.DOS).DataType = GetType(Date)
        c1Grid.Cols(ICD10Grid.DOS).Format = "MM/dd/yyyy"
        Dim dtp As New DateTimePicker
        dtp.Format = DateTimePickerFormat.Custom
        dtp.CustomFormat = "MM/dd/yyyy"
        c1Grid.Cols(ICD10Grid.DOS).Editor = dtp

        c1Grid.ExtendLastCol = True
    End Sub

    Private Sub SetICD10TransitionTab()
        Dim oClsICD10Settings As ClsICD10Settings = Nothing
        Try

            oClsICD10Settings = New clsICD10Settings()
            dtpICD10DOS.Format = DateTimePickerFormat.Custom
            dtpICD10DOS.CustomFormat = "MM/dd/yyyy"
            Dim _Date As String = oClsICD10Settings.FetchICDDOS()
            If Not IsNothing(_Date) AndAlso _Date <> "" Then
                dtpICD10DOS.Value = _Date
            Else
                dtpICD10DOS.Value = DateTime.Now.ToShortDateString
            End If
            Dim _List As IEnumerable = oClsICD10Settings.FetchPlanLevelICDDOS()
            C1ICD10DOS.DataSource = _List
            DesignGridForICD10TransitionSetting(C1ICD10DOS)
        Catch ex As Exception
            UpdateErrorLog(ex.ToString)
        Finally
            If Not IsNothing(oClsICD10Settings) Then
                oClsICD10Settings.Dispose()
                oClsICD10Settings = Nothing
            End If
        End Try
    End Sub

    Private Sub C1ICD10DOS_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles C1ICD10DOS.KeyDown
        Try
            If e.KeyValue = Keys.Delete Then
                If sender IsNot Nothing Then
                    C1ICD10DOS.SetData(DirectCast(sender, C1.Win.C1FlexGrid.C1FlexGrid).Row, ICD10Grid.DOS, "")
                End If
            End If
        Catch ex As Exception
            UpdateErrorLog(ex.ToString)
        End Try
    End Sub

    Private Sub chkMedHistory_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkMedHistory.CheckedChanged
        If chkMedHistory.Checked = True Then
            txtMedHistoryPortalURL.Enabled = True
            nmMedHxRestriction.Enabled = True
        Else
            txtMedHistoryPortalURL.Enabled = False
            nmMedHxRestriction.Enabled = False
        End If
    End Sub

    ''"START CCDA for Portal"
    Private Function checkClinicalCCDASection() As String
        Dim _cntrl As Control
        Dim _value As String = ""
        Dim _chk As CheckBox
        _value = checkCommanCCDASection()
        For Each _cntrl In pnlClinicalSummary.Controls
            If TypeOf _cntrl Is CheckBox Then
                _chk = CType(_cntrl, CheckBox)
                If _chk.Checked = True Then
                    If _value = "" Then
                        _value = _value + _chk.Tag
                    Else
                        _value = _value + "," + _chk.Tag
                    End If

                End If

            End If
        Next
        Return _value

    End Function
    'Private Function getCCDAImportCategory() As String
    '    Return ""
    'End Function
    Private Function checkAmbulatoryCCDASection() As String
        Dim _cntrl As Control
        Dim _value As String = ""
        Dim _chk As CheckBox

        _value = checkCommanCCDASection()
        For Each _cntrl In pnlAmbulatorySummary.Controls
            If TypeOf _cntrl Is CheckBox Then
                _chk = CType(_cntrl, CheckBox)
                If _chk.Checked = True Then
                    If _value = "" Then
                        _value = _value + _chk.Tag
                    Else
                        _value = _value + "," + _chk.Tag
                    End If

                End If

            End If
        Next
        Return _value

    End Function

    Private Function checkCommanCCDASection() As String
        Dim _cntrl As Control
        Dim _value As String = ""
        Dim _chk As CheckBox

        For Each _cntrl In pnlCommonMUData.Controls
            If TypeOf _cntrl Is CheckBox Then
                _chk = CType(_cntrl, CheckBox)
                If _chk.Checked = True Then
                    If _value = "" Then
                        _value = _value + _chk.Tag
                    Else
                        _value = _value + "," + _chk.Tag
                    End If

                End If

            End If
        Next
        Return _value

    End Function

    Private Sub rbAmbulatorySummary_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbAmbulatorySummary.CheckedChanged


        If rbAmbulatorySummary.Checked Then
            pnlClinicalSummary.Visible = False
            pnlAmbulatorySummary.Visible = True
            rbAmbulatorySummary.Font = New Font("Tahoma", 9, FontStyle.Bold)
            ' rbClinicalSummary.Font = New Font("Tahoma", 9, FontStyle.Regular)
            ClearAllCDA()
            checkCCDASections(tmpSelectedCCDAAmbulatory.Split(","), "AmbulatoryCCDA")
        Else

            pnlClinicalSummary.Visible = True
            pnlAmbulatorySummary.Visible = False
            rbAmbulatorySummary.Font = New Font("Tahoma", 9, FontStyle.Regular)
            ' rbClinicalSummary.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If

    End Sub
    Private Sub ClearAllCDA()

        chkCODemographic.Checked = False
        chkCOProblems.Checked = False
        chkCOAllergy.Checked = False
        chkCOCareTeamMem.Checked = False
        chkCOProcedures.Checked = False
        chkCOCarePlan.Checked = False
        chkCOVitalSigns.Checked = False
        chkCOlabResult.Checked = False
        chkCOLabTest.Checked = False
        chkCOMedication.Checked = False
        chkCOClinicalInstru.Checked = False
        chkCOSocialHistory.Checked = False
        chkCOFamilyHistory.Checked = False
        chkCOAssessments.Checked = False
        chkCOTreatmentPlan.Checked = False
        chkCOGoals.Checked = False
        chkCOHealthConcerns.Checked = False
        chkCSProviderName.Checked = False
        chkCSFutureAppt.Checked = False
        chkCSOfcContact.Checked = False
        chkCSRefOtrProvider.Checked = False
        chkCSVisitInfo.Checked = False
        chkCSDecisionAids.Checked = False
        chkCSVisitMedications.Checked = False
        chkCSVisitImmunization.Checked = False
        chkCSDigTestPending.Checked = False
        chkCSFutureTest.Checked = False
        chkCSVisitReason.Checked = False
        chkAmbImmunization.Checked = False
        chkAmbProviderContact.Checked = False
        chkAmbProviderName.Checked = False
        chkImplants.Checked = False
        ChkAmbMental.Checked = False
        ChkAmbReasonReferral.Checked = False
        ChkAmbFunctionalStatus.Checked = False
        ChkAmbReferring.Checked = False
        chkambDatelocationvisit.Checked = False
        chkambEncounters.Checked = False

    End Sub
    Private Sub rbClinicalSummary_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbClinicalSummary.CheckedChanged
        If rbClinicalSummary.Checked Then
            pnlClinicalSummary.Visible = True
            pnlAmbulatorySummary.Visible = False
            '  rbAmbulatorySummary.Font = New Font("Tahoma", 9, FontStyle.Regular)
            rbClinicalSummary.Font = New Font("Tahoma", 9, FontStyle.Bold)
            ClearAllCDA()
            checkCCDASections(tmpSelectedCCDAClinical.Split(","), "ClinicalCCDA")
        Else
            pnlClinicalSummary.Visible = False
            pnlAmbulatorySummary.Visible = True
            '  rbAmbulatorySummary.Font = New Font("Tahoma", 9, FontStyle.Regular)
            rbClinicalSummary.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub
    Private Sub SetCCDAImportCategory(ByVal SelectedImportCategory As String)
        Dim strCCDACat As String()
        Dim strCCDItem As String()
        strCCDACat = SelectedImportCategory.Split(";")
        Dim objSettings As New clsSettings
        Dim dict As Dictionary(Of String, String) = objSettings.GetCDASectionWithCodes()
        Try
            Dim value As String = String.Empty
            For Len As Integer = 0 To strCCDACat.Length - 1
                strCCDItem = strCCDACat(Len).Split(":")
                Dim trnode As New TreeNode
                If (strCCDItem.Length > 1) Then
                    Try
                        value = String.Empty
                        dict.TryGetValue(strCCDItem(1).Trim(), value)
                        If Not IsNothing(value) AndAlso value <> String.Empty Then
                            trnode.Text = value
                            trnode.Tag = strCCDItem(1).Trim()

                            If (strCCDItem(0) = "1") Then

                                trvCCDAimport.Nodes.Add(trnode)
                            Else

                                trvCCDAImport_unselected.Nodes.Add(trnode)


                            End If
                        End If
                    Catch ex As Exception

                    End Try
                End If
            Next

            dict.Clear()
            dict = Nothing
            objSettings = Nothing
        Catch ex As Exception

        End Try



    End Sub
    Private Function getCCDAImportCategory() As String
        Dim strCCDAImportcat As New StringBuilder
        For len As Integer = 0 To trvCCDAimport.Nodes.Count - 1
            strCCDAImportcat.Append("1:")
           
            strCCDAImportcat.Append(trvCCDAimport.Nodes(len).Tag)
            strCCDAImportcat.Append(";")

        Next

        For len As Integer = 0 To trvCCDAImport_unselected.Nodes.Count - 1
           
            strCCDAImportcat.Append("0:")


            strCCDAImportcat.Append(trvCCDAImport_unselected.Nodes(len).Tag)
            strCCDAImportcat.Append(";")

        Next
        Return strCCDAImportcat.ToString()
    End Function
    Private Sub checkCCDASections(ByVal SelectedCCDA As String(), ByVal _CCDASettingsname As String)


        Dim _Section As Int16

        For _Section = 0 To SelectedCCDA.Length - 1
            If SelectedCCDA(_Section) = chkCODemographic.Tag Then
                chkCODemographic.Checked = True
            ElseIf SelectedCCDA(_Section) = chkCOProblems.Tag Then
                chkCOProblems.Checked = True
            ElseIf SelectedCCDA(_Section) = chkCOAllergy.Tag Then
                chkCOAllergy.Checked = True
            ElseIf SelectedCCDA(_Section) = chkCOCareTeamMem.Tag Then
                chkCOCareTeamMem.Checked = True
            ElseIf SelectedCCDA(_Section) = chkCOProcedures.Tag Then
                chkCOProcedures.Checked = True
            ElseIf SelectedCCDA(_Section) = chkCOCarePlan.Tag Then
                chkCOCarePlan.Checked = True
            ElseIf SelectedCCDA(_Section) = chkCOVitalSigns.Tag Then
                chkCOVitalSigns.Checked = True
            ElseIf SelectedCCDA(_Section) = chkCOlabResult.Tag Then
                chkCOlabResult.Checked = True
            ElseIf SelectedCCDA(_Section) = chkCOLabTest.Tag Then
                chkCOLabTest.Checked = True
            ElseIf SelectedCCDA(_Section) = chkCOMedication.Tag Then
                chkCOMedication.Checked = True
            ElseIf SelectedCCDA(_Section) = chkCOClinicalInstru.Tag Then
                chkCOClinicalInstru.Checked = True
            ElseIf SelectedCCDA(_Section) = chkCOSocialHistory.Tag Then
                chkCOSocialHistory.Checked = True
            ElseIf SelectedCCDA(_Section) = chkCOFamilyHistory.Tag Then
                chkCOFamilyHistory.Checked = True
            ElseIf SelectedCCDA(_Section) = chkCOAssessments.Tag Then ''added for new sections added in CCDA
                chkCOAssessments.Checked = True
            ElseIf SelectedCCDA(_Section) = chkCOTreatmentPlan.Tag Then
                chkCOTreatmentPlan.Checked = True
            ElseIf SelectedCCDA(_Section) = chkCOGoals.Tag Then
                chkCOGoals.Checked = True
            ElseIf SelectedCCDA(_Section) = chkCOHealthConcerns.Tag Then
                chkCOHealthConcerns.Checked = True
            ElseIf SelectedCCDA(_Section) = chkImplants.Tag Then
                chkImplants.Checked = True

            End If
            ''
             

        Next

        If _CCDASettingsname = "ClinicalCCDA" Then
            For _Section = 0 To SelectedCCDA.Length - 1
                If SelectedCCDA(_Section) = chkCSProviderName.Tag Then
                    chkCSProviderName.Checked = True
                ElseIf SelectedCCDA(_Section) = chkCSFutureAppt.Tag Then
                    chkCSFutureAppt.Checked = True
                ElseIf SelectedCCDA(_Section) = chkCSOfcContact.Tag Then
                    chkCSOfcContact.Checked = True
                ElseIf SelectedCCDA(_Section) = chkCSRefOtrProvider.Tag Then
                    chkCSRefOtrProvider.Checked = True
                ElseIf SelectedCCDA(_Section) = chkCSVisitInfo.Tag Then
                    chkCSVisitInfo.Checked = True
                ElseIf SelectedCCDA(_Section) = chkCSDecisionAids.Tag Then
                    chkCSDecisionAids.Checked = True
                ElseIf SelectedCCDA(_Section) = chkCSVisitMedications.Tag Then
                    chkCSVisitMedications.Checked = True
                ElseIf SelectedCCDA(_Section) = chkCSVisitImmunization.Tag Then
                    chkCSVisitImmunization.Checked = True
                ElseIf SelectedCCDA(_Section) = chkCSDigTestPending.Tag Then
                    chkCSDigTestPending.Checked = True
                ElseIf SelectedCCDA(_Section) = chkCSFutureTest.Tag Then
                    chkCSFutureTest.Checked = True
                ElseIf SelectedCCDA(_Section) = chkCSVisitReason.Tag Then
                    chkCSVisitReason.Checked = True
                End If
            Next
        ElseIf _CCDASettingsname = "AmbulatoryCCDA" Then
            For _Section = 0 To SelectedCCDA.Length - 1
                If SelectedCCDA(_Section) = chkAmbImmunization.Tag Then
                    chkAmbImmunization.Checked = True
                ElseIf SelectedCCDA(_Section) = chkAmbProviderContact.Tag Then
                    chkAmbProviderContact.Checked = True
                ElseIf SelectedCCDA(_Section) = chkAmbProviderName.Tag Then
                    chkAmbProviderName.Checked = True
               

                ElseIf SelectedCCDA(_Section) = ChkAmbMental.Tag Then
                    ChkAmbMental.Checked = True
                ElseIf SelectedCCDA(_Section) = ChkAmbReasonReferral.Tag Then
                    ChkAmbReasonReferral.Checked = True
                ElseIf SelectedCCDA(_Section) = ChkAmbFunctionalStatus.Tag Then
                    ChkAmbFunctionalStatus.Checked = True
                ElseIf SelectedCCDA(_Section) = ChkAmbReferring.Tag Then
                    ChkAmbReferring.Checked = True
                ElseIf SelectedCCDA(_Section) = chkambDatelocationvisit.Tag Then
                    chkambDatelocationvisit.Checked = True
                ElseIf SelectedCCDA(_Section) = chkambEncounters.Tag Then
                    chkambEncounters.Checked = True
                End If
            Next

        End If
    End Sub

    Private Sub chkCODemographic_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkCODemographic.CheckedChanged
        chkCODemographic.Checked = True
    End Sub
    ''"END CCDA for Portal"

    Private Sub rb_ClinicalSummFamHst_CurrentHistory_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rb_ClinicalSummFamHst_CurrentHistory.CheckedChanged
        If rb_ClinicalSummFamHst_CurrentHistory.Checked = True Then
            rb_ClinicalSummFamHst_CurrentHistory.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            rb_ClinicalSummFamHst_CurrentHistory.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub rb_ClinicalSummFamHst_VisitSpecific_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rb_ClinicalSummFamHst_VisitSpecific.CheckedChanged
        If rb_ClinicalSummFamHst_VisitSpecific.Checked = True Then
            rb_ClinicalSummFamHst_VisitSpecific.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            rb_ClinicalSummFamHst_VisitSpecific.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub rb_ClinicalSummSocialHst_CurrentHistory_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rb_ClinicalSummSocialHst_CurrentHistory.CheckedChanged
        If rb_ClinicalSummSocialHst_CurrentHistory.Checked = True Then
            rb_ClinicalSummSocialHst_CurrentHistory.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            rb_ClinicalSummSocialHst_CurrentHistory.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub rb_ClinicalSummSocialHst_VisitSpecific_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rb_ClinicalSummSocialHst_VisitSpecific.CheckedChanged
        If rb_ClinicalSummSocialHst_VisitSpecific.Checked = True Then
            rb_ClinicalSummSocialHst_VisitSpecific.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            rb_ClinicalSummSocialHst_VisitSpecific.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub btnMouseLeave(sender As System.Object, e As System.EventArgs) Handles btnStyleSheetPath.MouseLeave, btnCCDUser.MouseLeave, btnCCDfilePath.MouseLeave
        Try
            If Not sender Is Nothing Then
                CType(sender, Button).BackgroundImage = gloEMRAdmin.My.Resources.Img_LongButton
                CType(sender, Button).BackgroundImageLayout = ImageLayout.Stretch
            End If

        Catch ex As Exception
            'Blank
        End Try

    End Sub

    Private Sub btnMouseHover(sender As System.Object, e As System.EventArgs) Handles btnStyleSheetPath.MouseHover, btnCCDUser.MouseHover, btnCCDfilePath.MouseHover
        Try
            If Not sender Is Nothing Then
                CType(sender, Button).BackgroundImage = gloEMRAdmin.My.Resources.Img_LongYellow
                CType(sender, Button).BackgroundImageLayout = ImageLayout.Stretch
            End If

        Catch ex As Exception
            'Blank
        End Try
    End Sub

    Private Sub rdoOverlapYes_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rdoOverlapYes.CheckedChanged
        If rdoOverlapYes.Checked = True Then
            rdoOverlapYes.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            rdoOverlapYes.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub rdoOverlapNo_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rdoOverlapNo.CheckedChanged
        If rdoOverlapNo.Checked = True Then
            rdoOverlapNo.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            rdoOverlapNo.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub rdoOverlapUser_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rdoOverlapUser.CheckedChanged
        If rdoOverlapUser.Checked = True Then
            rdoOverlapUser.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            rdoOverlapUser.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub Rdohttp_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles Rdohttp.CheckedChanged
        If Rdohttp.Checked = True Then
            Rdohttp.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            Rdohttp.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub Rdohttps_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles Rdohttps.CheckedChanged
        If Rdohttps.Checked = True Then
            Rdohttps.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            Rdohttps.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub rbEmdeonYes_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbEmdeonYes.CheckedChanged
        If rbEmdeonYes.Checked = True Then
            rbEmdeonYes.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            rbEmdeonYes.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub rbEmdeonNo_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbEmdeonNo.CheckedChanged
        If rbEmdeonNo.Checked = True Then
            rbEmdeonNo.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            rbEmdeonNo.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub rbEmdeonAsk_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbEmdeonAsk.CheckedChanged
        If rbEmdeonAsk.Checked = True Then
            rbEmdeonAsk.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            rbEmdeonAsk.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub rbProviderLabUsageTask_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbProviderLabUsageTask.CheckedChanged
        If rbProviderLabUsageTask.Checked = True Then
            rbProviderLabUsageTask.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            rbProviderLabUsageTask.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub rbProviderLabUsageLabOrder_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbProviderLabUsageLabOrder.CheckedChanged
        If rbProviderLabUsageLabOrder.Checked = True Then
            rbProviderLabUsageLabOrder.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            rbProviderLabUsageLabOrder.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub rbProviderLabUsageRecordResults_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbProviderLabUsageRecordResults.CheckedChanged
        If rbProviderLabUsageRecordResults.Checked = True Then
            rbProviderLabUsageRecordResults.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            rbProviderLabUsageRecordResults.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub rbProviderLabUsageAsk_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbProviderLabUsageAsk.CheckedChanged
        If rbProviderLabUsageAsk.Checked = True Then
            rbProviderLabUsageAsk.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            rbProviderLabUsageAsk.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub rbActive_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbActive.CheckedChanged
        If rbActive.Checked = True Then
            rbActive.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            rbActive.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub rbInactive_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbInactive.CheckedChanged
        If rbInactive.Checked = True Then
            rbInactive.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            rbInactive.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub rdoCPOEMU1Current_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rdoCPOEMU1Current.CheckedChanged
        If rdoCPOEMU1Current.Checked = True Then
            rdoCPOEMU1Current.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            rdoCPOEMU1Current.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub rdoCPOEMU1New_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rdoCPOEMU1New.CheckedChanged
        If rdoCPOEMU1New.Checked = True Then
            rdoCPOEMU1New.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            rdoCPOEMU1New.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub rdoVitalAllRequired_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rdoVitalAllRequired.CheckedChanged
        If rdoVitalAllRequired.Checked = True Then
            rdoVitalAllRequired.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            rdoVitalAllRequired.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub rdoVitalBPRequired_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rdoVitalBPRequired.CheckedChanged
        If rdoVitalBPRequired.Checked = True Then
            rdoVitalBPRequired.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            rdoVitalBPRequired.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub rdoVitalHeightWeightRequired_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rdoVitalHeightWeightRequired.CheckedChanged
        If rdoVitalHeightWeightRequired.Checked = True Then
            rdoVitalHeightWeightRequired.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            rdoVitalHeightWeightRequired.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub rdoeRxReqd_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rdoeRxReqd.CheckedChanged
        If rdoeRxReqd.Checked = True Then
            rdoeRxReqd.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            rdoeRxReqd.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub rdoeRxNotReqd_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rdoeRxNotReqd.CheckedChanged
        If rdoeRxNotReqd.Checked = True Then
            rdoeRxNotReqd.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            rdoeRxNotReqd.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub rdoeRptClinicalQuaReqd_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rdoeRptClinicalQuaReqd.CheckedChanged
        If rdoeRptClinicalQuaReqd.Checked = True Then
            rdoeRptClinicalQuaReqd.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            rdoeRptClinicalQuaReqd.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub rdoeRptClinicalQuaNotReqd_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rdoeRptClinicalQuaNotReqd.CheckedChanged
        If rdoeRptClinicalQuaNotReqd.Checked = True Then
            rdoeRptClinicalQuaNotReqd.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            rdoeRptClinicalQuaNotReqd.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub rdoeCopyPatHealthInfoReqd_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rdoeCopyPatHealthInfoReqd.CheckedChanged
        If rdoeCopyPatHealthInfoReqd.Checked = True Then
            rdoeCopyPatHealthInfoReqd.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            rdoeCopyPatHealthInfoReqd.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub rdoeCopyPatHealthInfoNotReqd_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rdoeCopyPatHealthInfoNotReqd.CheckedChanged
        If rdoeCopyPatHealthInfoNotReqd.Checked = True Then
            rdoeCopyPatHealthInfoNotReqd.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            rdoeCopyPatHealthInfoNotReqd.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub rdoeExchangeClinInforReqd_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rdoeExchangeClinInforReqd.CheckedChanged
        If rdoeExchangeClinInforReqd.Checked = True Then
            rdoeExchangeClinInforReqd.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            rdoeExchangeClinInforReqd.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub rdoeExchangeClinInforNotReqd_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rdoeExchangeClinInforNotReqd.CheckedChanged
        If rdoeExchangeClinInforNotReqd.Checked = True Then
            rdoeExchangeClinInforNotReqd.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            rdoeExchangeClinInforNotReqd.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub rdoVitalAllRequiredStage2_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rdoVitalAllRequiredStage2.CheckedChanged
        If rdoVitalAllRequiredStage2.Checked = True Then
            rdoVitalAllRequiredStage2.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            rdoVitalAllRequiredStage2.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub rdoVitalBPRequiredStage2_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rdoVitalBPRequiredStage2.CheckedChanged
        If rdoVitalBPRequiredStage2.Checked = True Then
            rdoVitalBPRequiredStage2.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            rdoVitalBPRequiredStage2.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub rdoVitalHeightWeightRequiredStage2_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rdoVitalHeightWeightRequiredStage2.CheckedChanged
        If rdoVitalHeightWeightRequiredStage2.Checked = True Then
            rdoVitalHeightWeightRequiredStage2.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            rdoVitalHeightWeightRequiredStage2.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub rbHealthVaultOff_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbHealthVaultOff.CheckedChanged
        If rbHealthVaultOff.Checked = True Then
            rbHealthVaultOff.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            rbHealthVaultOff.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub rbHealthVaultOn_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbHealthVaultOn.CheckedChanged
        If rbHealthVaultOn.Checked = True Then
            rbHealthVaultOn.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            rbHealthVaultOn.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub rbAdvRxStaging_CheckedChanged_1(sender As System.Object, e As System.EventArgs) Handles rbAdvRxStaging.CheckedChanged
        If rbAdvRxStaging.Checked = True Then
            rbAdvRxStaging.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            rbAdvRxStaging.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub rbAdvRxProduction_CheckedChanged_1(sender As System.Object, e As System.EventArgs) Handles rbAdvRxProduction.CheckedChanged
        If rbAdvRxProduction.Checked = True Then
            rbAdvRxProduction.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            rbAdvRxProduction.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub chkFormularyEnable_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkFormularyEnable.CheckedChanged
        Try
            Me.txtFormularyURL.Enabled = Me.chkFormularyEnable.Checked

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub chkCustomizeReportPrintSetting_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkCustomizeReportPrintSetting.CheckedChanged
        Cursor.Current = Cursors.WaitCursor
        If Initializing Then
            If chkCustomizeReportPrintSetting.Checked Then
                Dim sState As String = String.Empty
                Dim isDeploy As Boolean = False
                Dim strArr() As String = Nothing
                sState = GetClinicInformation("sState")
                If sState = "" Then
                    MessageBox.Show("Please select state from Clinic settings.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    chkCustomizeReportPrintSetting.Checked = False
                    Return
                End If
                If Chk_PrintMultipleRx_Per_Script_Page.Checked Then
                    sMultipleRxStateCustiomizeReport = "rptMultipleRx_" + sState + ".rdl"
                    strArr = sMultipleRxStateCustiomizeReport.Split(".")
                    'objSettings.GetSetting("MULTIPLERXSTATECUSTIOMIZEREPORT", gnLoginID, gnClinicID, ReportSettings)
                Else
                    sSingleRxStateCustiomizeReport = "rptSingleRx_" + sState + ".rdl"
                    strArr = sSingleRxStateCustiomizeReport.Split(".")
                    'objSettings.GetSetting("SINGLERXSTATECUSTIOMIZEREPORT", gnLoginID, gnClinicID, ReportSettings)
                End If

                isDeploy = ClsReportExplorer.IsDeployReport(strArr(0))
                If Not isDeploy Then
                    MessageBox.Show("Customized prescription report is not available." + vbNewLine + "Please contact system administrator.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    chkCustomizeReportPrintSetting.Checked = False
                    Return
                End If
            End If
        End If
        Cursor.Current = Cursors.[Default]
    End Sub

    Private Sub Chk_PrintMultipleRx_Per_Script_Page_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles Chk_PrintMultipleRx_Per_Script_Page.CheckedChanged
        Cursor.Current = Cursors.WaitCursor
        If chkCustomizeReportPrintSetting.Checked Then
            Dim sState As String = String.Empty
            Dim isDeploy As Boolean = False
            Dim strArr() As String = Nothing
            sState = GetClinicInformation("sState")
            If sState = "" Then
                MessageBox.Show("Please select state from Clinic settings.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                chkCustomizeReportPrintSetting.Checked = False
                Return
            End If
            If Chk_PrintMultipleRx_Per_Script_Page.Checked Then
                sMultipleRxStateCustiomizeReport = "rptMultipleRx_" + sState + ".rdl"
                strArr = sMultipleRxStateCustiomizeReport.Split(".")
            Else
                sSingleRxStateCustiomizeReport = "rptSingleRx_" + sState + ".rdl"
                strArr = sSingleRxStateCustiomizeReport.Split(".")
            End If
            isDeploy = ClsReportExplorer.IsDeployReport(strArr(0))
            If Not isDeploy Then
                MessageBox.Show("Customized prescription report is not available." + vbNewLine + "Please contact system administrator.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                chkCustomizeReportPrintSetting.Checked = False
                Return
            End If
        End If
        Cursor.Current = Cursors.[Default]
    End Sub

    ''Start OB Setting
    Private Sub DesignGridForMedicalCategory()
        With C1MedicalCategory
            .Rows.Count = 1
            .Cols.Count = 3

            .SetData(0, 0, "Select")
            .Cols(0).ImageAlign = C1.Win.C1FlexGrid.ImageAlignEnum.CenterCenter
            .Cols(0).Width = 60
            .Cols(0).AllowEditing = True

            .SetData(0, 1, "nMedicalCategoryID")
            .Cols(1).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter
            .Cols(1).Width = 0
            .Cols(1).AllowEditing = False
            .Cols(1).Visible = False

            .SetData(0, 2, "Medical Category")
            .Cols(2).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter
            .Cols(2).Width = 225
            .Cols(2).AllowEditing = False
        End With
    End Sub

    Private Sub saveOBSpeciality()
        Dim strMedicalCatogory As String = ""
        If chkOBSpeciality.Checked Then
            For i As Integer = 0 To C1MedicalCategory.Rows.Count - 1
                If C1MedicalCategory.GetCellCheck(i, 0) = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                    Dim strItem As String = C1MedicalCategory.GetData(i, 1)
                    strMedicalCatogory += "," + strItem
                End If
            Next
        End If

        If strMedicalCatogory.Length <> 0 Then
            strMedicalCatogory = strMedicalCatogory.Remove(0, 1)
        End If

        AddOBSpecialitySettingToDB(strMedicalCatogory)
        If (cmbmedrsk.SelectedIndex <> -1) Then  ''added for medical category risk functionality
            AddOBRiskCategorySettingToDB()
        End If
    End Sub

    Public Function AddOBSpecialitySettingToDB(ByVal MedicalCatogory As String) As Boolean
        Dim oDB As New gloDatabaseLayer.DBLayer(gloEMRAdmin.mdlGeneral.GetConnectionString())
        Dim oDBParameters As New gloDatabaseLayer.DBParameters()
        Try
            oDB.Connect(False)
            oDBParameters.Add("@CategoryIDs", MedicalCatogory, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar)
            oDB.Execute("gsp_Inup_OBMedicalCategory", oDBParameters)

            Return True
        Catch DBErr As gloDatabaseLayer.DBException
            Return False
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        Finally
            oDB.Disconnect()
            oDBParameters.Dispose()
            oDB.Dispose()
        End Try
    End Function
    Public Function AddOBRiskCategorySettingToDB() As Boolean
        Dim oDB As New gloDatabaseLayer.DBLayer(gloEMRAdmin.mdlGeneral.GetConnectionString())
        Dim oDBParameters As New gloDatabaseLayer.DBParameters()
        Try
            oDB.Connect(False)
            oDBParameters.Add("@nMedicalCategoryID", cmbmedrsk.SelectedValue, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt)
            oDB.Execute("gsp_UpdateMedCategoryRsk", oDBParameters)

            Return True
        Catch DBErr As gloDatabaseLayer.DBException
            Return False
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        Finally
            oDB.Disconnect()
            oDBParameters.Dispose()
            oDB.Dispose()
        End Try
    End Function


    Private Sub chkOBSpeciality_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkOBSpeciality.CheckedChanged
        If chkOBSpeciality.Checked Then
            pnlMedicalCategory.Visible = True
            pnl_lblMedicalCategory.Visible = True
            btnOBTemplates.Enabled = True
        Else
            pnlMedicalCategory.Visible = False
            pnl_lblMedicalCategory.Visible = False
            btnOBTemplates.Enabled = False
        End If

    End Sub

    Private Sub txtAutoCaseCloseDays_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtAutoCaseCloseDays.KeyPress
        If Not ((e.KeyChar >= ChrW(48) And e.KeyChar <= ChrW(57)) Or e.KeyChar = ChrW(8)) Then
            e.Handled = True
        End If
    End Sub

    Private Function ValidateOBSetting() As Boolean
        Dim _result As Boolean = False

        If txtAutoCaseCloseDays.Text <> "" Then
            If Convert.ToInt16(txtAutoCaseCloseDays.Text) > 99 Then
                MessageBox.Show("Auto case close days should not be greater than 99.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                tb_Settings.SelectTab(tb_Settings.TabPages.IndexOf(tbp_OBSpeciality))
                txtAutoCaseCloseDays.Text = ""
                txtAutoCaseCloseDays.Focus()
                _result = True
            End If
        End If
        Dim bIsCheck As Boolean = False
        If chkOBSpeciality.Checked And _result = False Then
            For i As Integer = 0 To C1MedicalCategory.Rows.Count - 1
                If C1MedicalCategory.GetCellCheck(i, 0) = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                    bIsCheck = True
                    Exit For
                End If
            Next
            If Not bIsCheck Then
                _result = True
                MessageBox.Show("Select at least one Medical Category.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                tb_Settings.SelectTab(tb_Settings.TabPages.IndexOf(tbp_OBSpeciality))
                C1MedicalCategory.Focus()
            End If
        End If

        Return _result
    End Function

    ''End OB Setting


    Private Sub btnServicesdb_Click(sender As System.Object, e As System.EventArgs) Handles btnServicesdb.Click
        '' 	Changes done by Ujwala as on 02032015 to store gloServices DB settings 

        Dim objSQLSettings As New clsStartUpSettings

        Dim _strSrvicesServername As String = txtSrvcServerName.Text.Trim
        Dim _strSrvicesDataBaseName As String = txtSrvcDatabaseName.Text.Trim()

        Try
            If _strSrvicesServername.Length = 0 Then
                MessageBox.Show("Enter Services database settings Server Name.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtSrvcServerName.Focus()
                Exit Sub
            End If

            If _strSrvicesDataBaseName.Length = 0 Then
                MessageBox.Show("Enter Services database settings Database Name.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtSrvcDatabaseName.Focus()
                Exit Sub
            End If

            If optSrvcSQLAuthentication.Checked = True Then
                If txtSrvcSQLUserID.Text.Trim = "" Then
                    MessageBox.Show("Enter Services database settings SQL User ID", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    txtSrvcSQLUserID.Focus()
                    Exit Sub
                End If

                If txtSrvcSQLPassword.Text.Trim = "" Then
                    MessageBox.Show("Enter Services database settings SQL Password", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    txtSrvcSQLPassword.Focus()
                    Exit Sub
                End If

                If objSQLSettings.IsSQLConnect(txtSrvcServerName.Text.Trim, txtSrvcDatabaseName.Text.Trim, txtSrvcSQLUserID.Text.Trim, txtSrvcSQLPassword.Text.Trim) = False Then
                    If MessageBox.Show("Unable to connect to Services database settings SQL Server " & _strSrvicesServername & " and Database " & _strSrvicesDataBaseName & "." & vbCrLf & "Select valid credentials.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information) Then
                        Exit Sub
                    End If
                Else
                    MessageBox.Show("Connection established successfully.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If
            Else
                If objSQLSettings.IsConnect(_strSrvicesServername, _strSrvicesDataBaseName, False, "", "") Then
                    MessageBox.Show("Connection established successfully.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                Else
                    If MessageBox.Show("Unable to connect to Services database settings SQL Server " & _strSrvicesServername & " and Database " & _strSrvicesDataBaseName & "." & vbCrLf & "Select valid credentials.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information) Then
                        Exit Sub
                    End If

                End If
            End If

        Catch ex As Exception
            MessageBox.Show("Unable to connect to Services database settings SQL Server " & _strSrvicesServername & " and Database " & _strSrvicesDataBaseName & ".", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            If Not IsNothing(objSQLSettings) Then
                objSQLSettings = Nothing
            End If
            _strSrvicesServername = String.Empty
            _strSrvicesDataBaseName = String.Empty
        End Try
        '' 	Changes done by Ujwala as on 02032015 to store gloServices DB settings 
    End Sub

    Private Sub btnOBTemplates_Click(sender As System.Object, e As System.EventArgs) Handles btnOBTemplates.Click
        Try
            Using ofrmOBTemplate As New frmOBTemplates()
                ofrmOBTemplate.ShowDialog()
            End Using
        Catch
        Finally

        End Try

    End Sub

    Private Sub C1MedicalCategory_Click(sender As Object, e As System.EventArgs) Handles C1MedicalCategory.Click
        Try


            If ((C1MedicalCategory.ColSel = 0) And (C1MedicalCategory.RowSel > 0)) Then
                Dim rowno As Integer = C1MedicalCategory.RowSel
                Dim medcatid As Int64 = C1MedicalCategory.Rows(C1MedicalCategory.RowSel)(1)
                Dim bln As Boolean = C1MedicalCategory.Rows(C1MedicalCategory.RowSel)(0)
                Dim dr As DataRow() = dtCmdMedCat.Select("nMedicalCategoryID=" & medcatid)
                If (bln = True) Then
                    If (dr.Length = 0) Then
                        Dim dradd As DataRow = dtCmdMedCat.NewRow() ''added for medical category risk functionality
                        dradd("Select") = bln.ToString()
                        dradd("nMedicalCategoryID") = medcatid
                        dradd("sMedicalCategory") = Convert.ToString(C1MedicalCategory.Rows(C1MedicalCategory.RowSel)(2))
                        dradd("bIsHighRisk") = "False"
                        dtCmdMedCat.Rows.Add(dradd)
                    End If
                End If
                If (bln = False) Then
                    If (dr.Length > 0) Then

                        If (cmbmedrsk.SelectedValue = dr(0)("nMedicalCategoryID")) Then

                            dtCmdMedCat.Rows.Remove(dr(0))
                            cmbmedrsk.SelectedValue = -1
                        Else
                            dtCmdMedCat.Rows.Remove(dr(0))
                        End If
                    End If
                End If


                'Dim drr As DataRow() = dtAllMedCat.Select("Select='True'")
                'For Each drc As DataRow In drr
                '    dtCmdMedCat.ImportRow(drc)
                'Next



            End If
        Catch ex As Exception
            ex = Nothing
        End Try



    End Sub

    Private Sub C1MedicalCategory_RowColChange(sender As Object, e As System.EventArgs) Handles C1MedicalCategory.RowColChange
    End Sub

    Private Sub tbp_ClinicSettings_Click(sender As System.Object, e As System.EventArgs) Handles tbp_ClinicSettings.Click

    End Sub
    ''User Story #89427: In gloEMR Admin, a new tab in task mapping screen: Online Patient forms should be created same as “Review Portal Users”.
    ''Addded to check portal is enable or not.
    Private Function IspatientPortalEnabled() As Boolean
        Dim IsPortalEnabled As Boolean = False
        Try
            Dim objSettings As New clsSettings
            Dim isPortalEnable As Object = Nothing
            objSettings.GetSetting("PatientPortalEnabled", gnLoginID, gnClinicID, isPortalEnable)
            If isPortalEnable IsNot Nothing Then
                If Convert.ToString(isPortalEnable).ToLower() = "true" Then
                    IsPortalEnabled = True
                End If
            End If
            objSettings = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.ToString(), gstrMessageBoxCaption)
            Return IsPortalEnabled
        End Try
        Return IsPortalEnabled
    End Function

    Private Sub chkCCDAAutoDelete_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkCCDAAutoDelete.CheckedChanged

    End Sub
    Private Sub btn_Browse_Click(sender As System.Object, e As System.EventArgs) Handles btn_Browse.Click
        Try
            With FolderBrowserDialog_ClinicalDoc()
                .ShowNewFolderButton = True
                .Description = "Select Clinical Documents Export Path"
                If .ShowDialog() = DialogResult.OK Then
                    txtClinicalDocumentsExportPath.Text = .SelectedPath
                End If
            End With
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub rdooldSignaturepad_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rdooldSignaturepad.CheckedChanged
        If rdooldSignaturepad.Checked = True Then
            rdooldSignaturepad.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            rdooldSignaturepad.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub rdoNewsignaturepad_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rdoNewsignaturepad.CheckedChanged
        If rdoNewsignaturepad.Checked = True Then
            rdoNewsignaturepad.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            rdoNewsignaturepad.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

#Region "OccMed"
    Private Sub btnOccMedDmsCat_Click(sender As System.Object, e As System.EventArgs) Handles btnOccMedDmsCat.Click
        Try
            If oListControl_DMS IsNot Nothing Then
                For i As Integer = Me.Controls.Count - 1 To 0 Step -1
                    If Me.Controls(i).Name = oListControl_DMS.Name Then
                        Me.Controls.Remove(Me.Controls(i))
                        Exit For
                    End If
                Next
            End If

            oListControl_DMS = New gloListControl.gloListControl(gloEMRAdmin.mdlGeneral.GetConnectionString(gDmsServerName, gDmsDatabaseName, gDmsIsSQLAUTHEN, gDmsUserID, gDmsPassWord), gloListControl.gloListControlType.DMSCategory, True, True, Me.Width)
            oListControl_DMS.ControlHeader = "DMS Category"
            AddHandler oListControl_DMS.ItemSelectedClick, AddressOf oListControl_DMS_ItemSelectedClick
            AddHandler oListControl_DMS.ItemClosedClick, AddressOf oListControl_DMS_ItemClosedClick

            Me.Controls.Add(oListControl_DMS)
            'oListControl.SelectedItems.Add(Convert.ToInt64(dtPatientRepresentative.Rows[i]["Id"]), Convert.ToString(dtPatientRepresentative.Rows[i]["Description"]));
            'oListControl.SelectedItems.Add(Convert.ToInt64(dtPatientRepresentative.Rows[i]["Id"]), Convert.ToString(dtPatientRepresentative.Rows[i]["Description"]));
            For i As Integer = 0 To cmbOccDmsCategory.Items.Count - 1
                If Convert.ToInt64(CType(cmbOccDmsCategory.Items(i), DataRowView).Row.ItemArray(0)) <> -1 Then
                    oListControl_DMS.SelectedItems.Add(Convert.ToInt64(CType(cmbOccDmsCategory.Items(i), DataRowView).Row.ItemArray(0)), CType(cmbOccDmsCategory.Items(i), DataRowView).Row.ItemArray(1))
                End If

            Next

            oListControl_DMS.OpenControl()
            oListControl_DMS.Dock = DockStyle.Fill
            oListControl_DMS.BringToFront()
        Catch ex As Exception
            'gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        End Try
    End Sub
    Private Sub oListControl_DMS_ItemSelectedClick(ByVal sender As Object, ByVal e As EventArgs)

        If cmbOccDmsCategory.DataSource IsNot Nothing Then
            Dim dt As DataTable = CType(cmbOccDmsCategory.DataSource, DataTable)
            dt.Rows.Clear()
            Try
                If oListControl_DMS.SelectedItems.Count > 0 Then
                    For j As Integer = 0 To oListControl_DMS.SelectedItems.Count - 1
                        dt.Rows.Add(oListControl_DMS.SelectedItems(j).ID.ToString(), oListControl_DMS.SelectedItems(j).Description.ToString())
                    Next
                Else
                    dt.Rows.Add(-1, "")
                End If

            Catch ex As Exception
                MessageBox.Show(ex.ToString(), gstrMessageBoxCaption)
            End Try
        End If
        If oListControl_DMS IsNot Nothing Then
            oListControl_DMS.SendToBack()
        End If
    End Sub
    Private Sub oListControl_DMS_ItemClosedClick(ByVal sender As Object, ByVal e As EventArgs)
        If oListControl_DMS IsNot Nothing Then
            oListControl_DMS.SendToBack()
        End If
    End Sub
    Private Sub btnOccMedDmsCateDelete_Click(sender As System.Object, e As System.EventArgs) Handles btnOccMedDmsCateDelete.Click
        Dim dt As DataTable = CType(cmbOccDmsCategory.DataSource, DataTable)
        dt.Rows.Clear()
        'dt.Rows.Add(-1, "")
    End Sub
    Private Sub SetOccMEdDMSCategory(OccMedDMSCategory As String)
        Dim objReader As SqlDataReader
        Dim objCon As New SqlConnection
        Try
            If OccMedDMSCategory.Trim() <> "" Then

                'Changed by rahul patel on 29-10-2010
                'Added parameter to getConnectionString() function for Building DMS database Connection string.
                objCon.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString(gDmsServerName, gDmsDatabaseName, gDmsIsSQLAUTHEN, gDmsUserID, gDmsPassWord)
                'End of code rahul patel 
                Dim objcmd As New SqlCommand
                objcmd.Connection = objCon
                'objcmd.CommandText = "select  CategoryId, CategoryName, isnull(IsDeleted,0) as IsDeleted , isnull(ClinicID,0) as ClinicID from eDocument_Category where isnull(CategoryName,'') <> '' and IsDeleted = 0 order by CategoryName"
                objcmd.CommandText = "gsp_GetDMSCategoriesById"
                objcmd.CommandType = CommandType.StoredProcedure
                Dim opar As SqlParameter = New SqlParameter("@DMSCategories", SqlDbType.VarChar)
                objcmd.Parameters.Add(opar)
                With objcmd
                    .Parameters("@DMSCategories").Value = OccMedDMSCategory
                End With
                objCon.Open()
                objReader = objcmd.ExecuteReader()
            End If
            Dim dt As New DataTable
            dt.Columns.Add("CategoryId")
            dt.Columns.Add("CategoryDescription")

            If Not IsNothing(objReader) Then
                If objReader.HasRows = True Then
                    While objReader.Read
                        dt.Rows.Add(objReader(0), objReader(1))
                    End While
                Else
                    dt.Rows.Add(-1, "")
                End If
                objReader.Close()
                objCon.Close()
            Else
                dt.Rows.Add(-1, "")
            End If
            cmbOccDmsCategory.DataSource = dt
            cmbOccDmsCategory.DisplayMember = "CategoryDescription"
            cmbOccDmsCategory.ValueMember = "CategoryId"
        Catch ex As Exception
        Finally
            If Not IsNothing(objCon) Then
                objCon.Dispose()
                objCon = Nothing
            End If
            If Not IsNothing(objReader) Then
                objReader.Dispose()
                objReader = Nothing
            End If

        End Try

    End Sub
#End Region

    Private Sub btnccdaimpdn_Click(sender As System.Object, e As System.EventArgs) Handles btnccdaimpdn.Click
        Try
            Dim oNode As TreeNode
            Dim nextIndex As Integer
            If IsNothing(trvCCDAimport.SelectedNode) = False Then
                oNode = trvCCDAimport.SelectedNode.Clone
                If IsNothing(oNode) = False Then

                    If IsNothing(trvCCDAimport.SelectedNode.Parent) Then
                        If trvCCDAimport.SelectedNode.Index <> trvCCDAimport.Nodes.Count - 1 Then
                            nextIndex = trvCCDAimport.SelectedNode.NextNode.Index
                            trvCCDAimport.Nodes.Remove(trvCCDAimport.SelectedNode)
                            trvCCDAimport.Nodes.Insert(nextIndex, oNode)
                            trvCCDAimport.SelectedNode = oNode
                        End If
                    Else
                        If trvCCDAimport.SelectedNode.Index <> trvCCDAimport.SelectedNode.Parent.Nodes.Count - 1 Then
                            Dim PNode As TreeNode
                            PNode = trvCCDAimport.SelectedNode.Parent
                            nextIndex = trvCCDAimport.SelectedNode.NextNode.Index
                            PNode.Nodes.Remove(trvCCDAimport.SelectedNode)
                            PNode.Nodes.Insert(nextIndex, oNode)
                            trvCCDAimport.SelectedNode = oNode
                        End If

                    End If
                End If
            End If
            trvCCDAimport.ExpandAll()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnccdaimpup_Click(sender As System.Object, e As System.EventArgs) Handles btnccdaimpup.Click
        Try
            Dim oNode As TreeNode
            Dim prevIndex As Integer
            If IsNothing(trvCCDAimport.SelectedNode) = False Then
                oNode = trvCCDAimport.SelectedNode.Clone
                If IsNothing(oNode) = False Then

                    If IsNothing(trvCCDAimport.SelectedNode.Parent) Then
                        If trvCCDAimport.SelectedNode.Index <> 0 Then
                            prevIndex = trvCCDAimport.SelectedNode.PrevNode.Index
                            trvCCDAimport.Nodes.Remove(trvCCDAimport.SelectedNode)
                            trvCCDAimport.Nodes.Insert(prevIndex, oNode)
                            trvCCDAimport.SelectedNode = oNode
                        End If
                    Else
                        If trvCCDAimport.SelectedNode.Index <> 0 Then
                            Dim PNode As TreeNode
                            PNode = trvCCDAimport.SelectedNode.Parent
                            prevIndex = trvCCDAimport.SelectedNode.PrevNode.Index
                            PNode.Nodes.Remove(trvCCDAimport.SelectedNode)
                            PNode.Nodes.Insert(prevIndex, oNode)
                            trvCCDAimport.SelectedNode = oNode

                        End If
                    End If
                End If
            End If
            trvCCDAimport.ExpandAll()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

   

    Private Sub btnCCDAImportLeft_Click(sender As System.Object, e As System.EventArgs) Handles btnCCDAImportLeft.Click
        If (Not IsNothing(trvCCDAimport.SelectedNode)) Then
            Dim onode As TreeNode = trvCCDAimport.SelectedNode
            trvCCDAimport.Nodes.Remove(onode)
            trvCCDAImport_unselected.Nodes.Add(onode)
        End If
    End Sub

    Private Sub btnCCDAImportRight_Click(sender As System.Object, e As System.EventArgs) Handles btnCCDAImportRight.Click
        If (Not IsNothing(trvCCDAImport_unselected.SelectedNode)) Then
            Dim onode As TreeNode = trvCCDAImport_unselected.SelectedNode
            trvCCDAImport_unselected.Nodes.Remove(onode)
            trvCCDAimport.Nodes.Add(onode)
        End If
    End Sub

    Private Sub oListControl_HistoryForecastSelectedClick(ByVal sender As Object, ByVal e As EventArgs)
        txtHistoryForecastTask.Clear()
        If oListControl.SelectedItems.Count > 0 Then
            _gloHxForecast_defaultUserID = oListControl.SelectedItems(0).ID
            txtHistoryForecastTask.Text = oListControl.SelectedItems(0).Description.ToString()
        End If
    End Sub

    Private Sub oListControl_ForecastReconciliationSelectedClick(ByVal sender As Object, ByVal e As EventArgs)
        txtForecastReconciliationTask.Clear()
        If oListControl.SelectedItems.Count > 0 Then
            _gloForecastReconcileDone_defaultUserID = oListControl.SelectedItems(0).ID
            txtForecastReconciliationTask.Text = oListControl.SelectedItems(0).Description.ToString()
        End If
    End Sub

    Private Sub btnBrowseHistoryForecastTask_Click(sender As System.Object, e As System.EventArgs) Handles btnBrowseHistoryForecastTask.Click
        Try

            If oListControl IsNot Nothing Then
                For i As Integer = Me.Controls.Count - 1 To 0 Step -1
                    If Me.Controls(i).Name = oListControl.Name Then
                        Me.Controls.Remove(Me.Controls(i))
                        Exit For
                    End If
                Next
            End If

            oListControl = New gloListControl.gloListControl(mdlGeneral.GetConnectionString, gloListControl.gloListControlType.Users, False, Me.Width)
            oListControl.ClinicID = _gloLab_defaultID
            oListControl.ControlHeader = "Users"

            _CurrentControlType = gloListControl.gloListControlType.Users
            AddHandler oListControl.ItemSelectedClick, AddressOf oListControl_HistoryForecastSelectedClick
            AddHandler oListControl.ItemClosedClick, AddressOf oListControl_ItemClosedClick
            Me.Controls.Add(oListControl)


            oListControl.OpenControl()
            oListControl.Dock = DockStyle.Fill
            oListControl.BringToFront()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnBrowseForecastReconciliationTask_Click(sender As System.Object, e As System.EventArgs) Handles btnBrowseForecastReconciliationTask.Click
        Try

            If oListControl IsNot Nothing Then
                For i As Integer = Me.Controls.Count - 1 To 0 Step -1
                    If Me.Controls(i).Name = oListControl.Name Then
                        Me.Controls.Remove(Me.Controls(i))
                        Exit For
                    End If
                Next
            End If

            oListControl = New gloListControl.gloListControl(mdlGeneral.GetConnectionString, gloListControl.gloListControlType.Users, False, Me.Width)
            oListControl.ClinicID = _gloLab_defaultID
            oListControl.ControlHeader = "Users"

            _CurrentControlType = gloListControl.gloListControlType.Users
            AddHandler oListControl.ItemSelectedClick, AddressOf oListControl_ForecastReconciliationSelectedClick
            AddHandler oListControl.ItemClosedClick, AddressOf oListControl_ItemClosedClick
            Me.Controls.Add(oListControl)


            oListControl.OpenControl()
            oListControl.Dock = DockStyle.Fill
            oListControl.BringToFront()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnClearHistoryForecastTask_Click(sender As System.Object, e As System.EventArgs) Handles btnClearHistoryForecastTask.Click
        txtHistoryForecastTask.Text = String.Empty
        _gloHxForecast_defaultUserID = 0
    End Sub

    Private Sub btnClearForecastReconciliationTask_Click(sender As System.Object, e As System.EventArgs) Handles btnClearForecastReconciliationTask.Click
        txtForecastReconciliationTask.Text = String.Empty
        _gloForecastReconcileDone_defaultUserID = 0
    End Sub

    Private Sub chkCOCareTeamMem_CheckedChanged(sender As Object, e As System.EventArgs) Handles chkCOCareTeamMem.CheckedChanged
        If chkCOCareTeamMem.Checked = False Then
            chkCOCareTeamMem.Checked = True
        End If


    End Sub



    Private Sub btnBrowsePatientPortalgloCoreServicesInstallationPath_Click(sender As System.Object, e As System.EventArgs) Handles btnBrowsePatientPortalgloCoreServicesInstallationPath.Click
        Dim fbdPatientPortalgloCoreServicesInstallationPath As New System.Windows.Forms.FolderBrowserDialog()
        fbdPatientPortalgloCoreServicesInstallationPath.ShowDialog()
        If Not String.IsNullOrEmpty(fbdPatientPortalgloCoreServicesInstallationPath.SelectedPath) Then
            If (Not System.IO.Directory.Exists(fbdPatientPortalgloCoreServicesInstallationPath.SelectedPath)) Then
                MessageBox.Show("Enter valid QCore service installation path", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtPatientPortalgloCoreServicesInstallationPath.Text = ""
            Else
                txtPatientPortalgloCoreServicesInstallationPath.Text = fbdPatientPortalgloCoreServicesInstallationPath.SelectedPath

            End If

        End If
    End Sub


    Private Sub tb_Settings_Selected(sender As System.Object, e As System.Windows.Forms.TabControlEventArgs) Handles tb_Settings.Selected
        Try
            If dgCDAExamFinish.DataSource Is Nothing Then
                Using SqlConnection As New SqlConnection(mdlGeneral.GetConnectionString())
                    Using SqlCommand As New SqlCommand("gsp_GetCDAExamFinishSettings", SqlConnection)                        
                        SqlCommand.CommandType = CommandType.StoredProcedure
                        Using dsProviderSettings As New DataSet()
                            Using sqlAdapter As New SqlDataAdapter(SqlCommand)
                                sqlAdapter.Fill(dsProviderSettings)

                                If dsProviderSettings.Tables.Count() > 0 Then
                                    If dsProviderSettings.Tables(0).Rows.Count() > 0 Then
                                        Dim dRow As DataRow = dsProviderSettings.Tables(0).AsEnumerable().FirstOrDefault(Function(p) Convert.ToString(p("sSettingsName")).ToUpper() = "SendCDAExamFinish".ToUpper())
                                        If dRow IsNot Nothing Then
                                            chkCDASendExamFinish.Checked = Convert.ToBoolean(dRow("sSettingsValue"))
                                        Else
                                            chkCDASendExamFinish.Checked = False
                                        End If

                                        dRow = Nothing
                                        dRow = dsProviderSettings.Tables(0).AsEnumerable().FirstOrDefault(Function(p) Convert.ToString(p("sSettingsName")).ToUpper() = "PromptProviderForCDASend".ToUpper())

                                        If dRow IsNot Nothing Then
                                            chkPromptCDASend.Checked = Convert.ToBoolean(dRow("sSettingsValue"))
                                        Else
                                            chkPromptCDASend.Checked = False
                                        End If
                                        dRow = Nothing
                                    Else
                                        chkCDASendExamFinish.Checked = False
                                        chkPromptCDASend.Checked = True
                                    End If

                                    dgCDAExamFinish.DataSource = dsProviderSettings.Tables(1)
                                End If


                            End Using
                        End Using
                    End Using
                End Using

                dgCDAExamFinish.Columns("nSettingID").Visible = False
                dgCDAExamFinish.Columns("nProviderID").Visible = False

                dgCDAExamFinish.Columns("sProviderName").HeaderText = "Provider Name"
                dgCDAExamFinish.Columns("bitValue").HeaderText = "Enable"

                dgCDAExamFinish.Columns("bitValue").AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                dgCDAExamFinish.Columns("bitValue").Width = 50
                dgCDAExamFinish.Columns("sProviderName").ReadOnly = True


                dgCDAExamFinish.RowHeadersVisible = False                
            End If
            

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        End Try
    End Sub

    Private Sub dgCDAExamFinish_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgCDAExamFinish.CellContentClick
        Try

            If dgCDAExamFinish.IsCurrentCellDirty AndAlso TypeOf (dgCDAExamFinish.CurrentCell) Is DataGridViewCheckBoxCell Then
                BeginInvoke(Sub()
                                If e.ColumnIndex = dgCDAExamFinish.Columns("bitValue").Index AndAlso e.RowIndex >= 0 Then
                                    If dtCDAExamProviders Is Nothing Then
                                        dtCDAExamProviders = CType(dgCDAExamFinish.DataSource, DataTable).Clone()
                                    End If

                                    Dim nScrollingIndex As Int32 = dgCDAExamFinish.FirstDisplayedScrollingRowIndex

                                    dgCDAExamFinish.CommitEdit(DataGridViewDataErrorContexts.Commit)

                                    If dgCDAExamFinish.DataSource IsNot Nothing AndAlso TypeOf (dgCDAExamFinish.DataSource) Is DataTable Then
                                        If dgCDAExamFinish.Rows(e.RowIndex).DataBoundItem IsNot Nothing AndAlso TypeOf (dgCDAExamFinish.Rows(e.RowIndex).DataBoundItem) Is DataRowView Then
                                            Dim dRow As DataRow = DirectCast(dgCDAExamFinish.Rows(e.RowIndex).DataBoundItem, System.Data.DataRowView).Row

                                            Dim dAddedRow As DataRow = dtCDAExamProviders.AsEnumerable().FirstOrDefault(Function(p) p("nProviderID") = dRow("nProviderID"))

                                            If dAddedRow IsNot Nothing Then
                                                dtCDAExamProviders.Rows.Remove(dAddedRow)
                                            End If

                                            dRow("bitValue") = Convert.ToBoolean(dgCDAExamFinish(e.ColumnIndex, e.RowIndex).Value)

                                            DirectCast(dgCDAExamFinish.DataSource, DataTable).AcceptChanges()


                                            dtCDAExamProviders.ImportRow(dRow)
                                            dRow = Nothing
                                            dAddedRow = Nothing
                                            dgCDAExamFinish.FirstDisplayedScrollingRowIndex = nScrollingIndex
                                        End If
                                    End If
                                End If
                            End Sub)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        End Try
    End Sub



    Private _dtCDAExamProviders As DataTable
    Public Property dtCDAExamProviders() As DataTable
        Get
            Return _dtCDAExamProviders
        End Get
        Set(ByVal value As DataTable)
            _dtCDAExamProviders = value
        End Set
    End Property

    Private Sub chkCDASendExamFinish_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkCDASendExamFinish.CheckedChanged
        Try
            chkPromptCDASend.Enabled = Not chkCDASendExamFinish.Checked
            If chkCDASendExamFinish.Checked Then
                chkPromptCDASend.Checked = False
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        End Try
    End Sub


    Private Sub LoadDefaultCQM(CQMValues As String)
        Dim CQMDatatable As New DataTable()

        With CQMDatatable
            .Columns.Add("colSelect", Type.GetType("System.Int16"))
            .Columns.Add("colCMSID", Type.GetType("System.String"))
            .Columns.Add("colDescription", Type.GetType("System.String"))
        End With

        Try
            If CQMValues IsNot Nothing Then
                Dim sCQMArray As String() = CQMValues.Split(New String() {"|"}, StringSplitOptions.None)
                Dim sCQMValue As String() = Nothing

                If sCQMArray IsNot Nothing Then
                    For Each KeyValue As String In sCQMArray.ToList()
                        sCQMValue = KeyValue.Split(New String() {"#"}, StringSplitOptions.None)
                        If sCQMValue IsNot Nothing Then
                            Dim dRow As DataRow = CQMDatatable.NewRow()

                            dRow("colCMSID") = sCQMValue(0)
                            dRow("colSelect") = sCQMValue(1)
                            dRow("colDescription") = Me.GetCQMDescription(sCQMValue(0))

                            CQMDatatable.Rows.Add(dRow)
                            dRow = Nothing
                        End If
                        sCQMValue = Nothing
                    Next
                End If
            End If
            dgCQM.AutoGenerateColumns = False
            dgCQM.DataSource = CQMDatatable            
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        End Try
    End Sub

    Private Function GetCQMDescription(ByVal CMSID As String) As String
        Try
            Dim sReturned As String = ""
            Select Case CMSID.Trim(New Char() {" "}).ToUpper()
                Case "CMS146"
                    sReturned = "CMS146/NQF 0002: Appropriate Testing for Children with Pharyngitis"
                Case "CMS165"
                    sReturned = "CMS165/NQF 0018: Controlling High Blood Pressure"
                Case "CMS138"
                    sReturned = "CMS138/NQF 0028: Preventive Care and Screening: Tobacco Use: Screening and Cessation Intervention"
                Case "CMS124"
                    sReturned = "CMS124/NQF 0032: Cervical Cancer Screening"
                Case "CMS153"
                    sReturned = "CMS153/NQF 0033: Chlamydia Screening for Women"
                Case "CMS130"
                    sReturned = "CMS130/NQF 0034: Colorectal Cancer Screening"
                Case "CMS117"
                    sReturned = "CMS117/NQF 0038: Childhood Immunization Status"
                Case "CMS147"
                    sReturned = "CMS147/NQF 0041: Preventive Care and Screening: Influenza Immunization"
                Case "CMS127"
                    sReturned = "CMS127/NQF 0043: Pneumonia Vaccination Status for Older Adults"
                Case "CMS166"
                    sReturned = "CMS166/NQF 0052: Use of Imaging Studies for Low Back Pain"
                Case "CMS131"
                    sReturned = "CMS131/NQF 0055: Diabetes: Eye Exam"
                Case "CMS123"
                    sReturned = "CMS123/NQF 0056: Diabetes: Foot Exam"
                Case "CMS122"
                    sReturned = "CMS122/NQF 0059: Diabetes: Hemoglobin A1c (HbA1c) Poor Control (> 9%)"
                Case "CMS134"
                    sReturned = "CMS134/NQF 0062: Diabetes: Medical Attention for Nephropathy"
                Case "CMS164"
                    sReturned = "CMS164/NQF 0068: Ischemic Vascular Disease (IVD): Use of Aspirin or Another Antiplatelet"
                Case "CMS139"
                    sReturned = "CMS139/NQF 0101: Falls: Screening for Future Fall Risk"
                Case "CMS2"
                    sReturned = "CMS2/NQF 0418: Preventive Care and Screening: Screening for Clinical Depression and Follow-Up Plan"
                Case "CMS68"
                    sReturned = "CMS68/NQF 0419: Documentation of Current Medications in the Medical Record"
                Case "CMS69"
                    sReturned = "CMS69/NQF 0421: Preventive Care and Screening: Body Mass Index (BMI) Screening and Follow-Up Plan"
                Case "CMS22"
                    sReturned = "CMS22: Preventive Care and Screening: Screening for High Blood Pressure and Follow-Up Documented"
                Case "CMS56"
                    sReturned = "CMS56:  Functional Status Assessment for Total Hip Replacement"
                Case "CMS66"
                    sReturned = "CMS66: Functional Status Assessment for Total Knee Replacement"
                Case "CMS90"
                    sReturned = "CMS90: Functional Status Assessments for Congestive Heart Failure"
                Case "CMS125"
                    sReturned = "CMS125: Breast Cancer Screening"
            End Select

            Return sReturned
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        End Try
    End Function
    Private bIsQCheckpointSettingModified As Boolean = False

    Private Sub dgCQM_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgCQM.CellContentClick
        bIsQCheckpointSettingModified = True
    End Sub

    Private Function GetCQMSetting() As String
        Dim sReturned As String = ""
        Dim sBuilder As New StringBuilder()
        Try
            For Each Row As DataGridViewRow In dgCQM.Rows
                sBuilder.Append(Convert.ToString(Row.Cells("colCMSID").Value) & "#" & Convert.ToString(Row.Cells("colSelect").Value) & "|")
            Next
            sReturned = sBuilder.ToString()
            If sReturned.Length > 1 Then
                sReturned = sReturned.Substring(0, sReturned.Length - 1)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        End Try
        Return sReturned
    End Function

    Private Sub chkEnableEpcs_CheckedChanged(sender As Object, e As System.EventArgs) Handles chkEnableEpcs.CheckedChanged
        If chkEnableEpcs.Checked = True Then
            txtPDMPUrl.Enabled = True
        Else
            txtPDMPUrl.Enabled = False
        End If
    End Sub
End Class




