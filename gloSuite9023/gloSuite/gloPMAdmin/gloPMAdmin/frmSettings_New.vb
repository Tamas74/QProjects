Imports System.Data.SqlClient
Imports C1.Win.C1FlexGrid
Imports System.IO
Imports System.Transactions
Imports System.Collections.Generic
Imports Trinet.Networking
Imports System.Text.RegularExpressions
Imports gloSettings
Imports gloAuditTrail
Imports System.Linq

Imports System.Data



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

Public Enum MidLevelGridCol

    ProviderID
    ProviderName
    SettingsID
    Type
    SettingsName

End Enum
''Added By Pramod Nair For Billing Id Qualifier
Public Enum BillingGridColumn
    ProviderID
    ProviderName
    ServiceFacilitySource
    BillingProviderSource
End Enum

Public Enum enum_CASReasonType
    None
    Coins
    Copay
    Deduct
    PrevPaid
    WH
    WO
    Other
End Enum


Public Class frmSettings_New
    Dim _SequentialPatientCode As Long
    Public Enum enumGrowthChartPercentile
        DontShowPercentile = 0
        ShowPercentile = 1
        ShowPercentileOnMouseHoover = 2
    End Enum

#Region "variables"
    Private _blnValidate As Boolean = False
    Dim flagPrefixSettingON, flagPrefixSettingOFF As Boolean ''flag to check settings on or off (added by pradeep)
    Dim tooltip As New ToolTip
    Dim combo As New ComboBox
    Dim optcnt As Integer = 0
    Dim blnadminflag As Boolean

    Dim htMidLevel As Hashtable
    Dim htANSI As Hashtable
    Dim htPaperFormFormat As Hashtable
    '******
    Dim colUsers As New Collection
    Dim colUId As New Collection
    Dim isSaved As Boolean = False

    Private blnSurgicalclick As Boolean 'this flag will keep the trak wether the btnAddFollowup suer is clicked or the btnAddSurgicalAlter user is clicked
    'so when we click the OK button in the C1userList grid that time we will check which of the above button was clicked so depending on that the cmbFollowUpuser or the cmbSurgicalAlertUser will be updated
    'therefore when btnAddFollowUpUser is clicked we make this flag = false and when btnAddSurgicalAlert user is clicked then we make this flag = true

    '******collections for Surgical Alert users
    Dim col_SurgicalUsers As New Collection
    Dim col_SurgicalUId As New Collection



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


    ''' <summary>
    ''' Column Constants for BillingQualifier Provider Setting Grid
    ''' </summary>
    ''' <remarks> ''Added By Pramod Nair For Billing Id Qualifier
    Private COL_QAPROVIDER_ID As Integer = 0
    Private COL_QAPROVIDERNAME As Integer = 1
    Private COL_FACILITYSOURCE As Integer = 2
    Private COL_PROVIDERSOURCE As Integer = 3
    Private COL_QACOUNTS As Integer = 4



    ''Column constants for billing grid 
    Dim COL_BPROVIDERID As Integer = 0
    Dim COL_PROVIDER_NAME As Integer = 1
    Dim COL_TOS As Integer = 2
    Dim COL_POS As Integer = 3
    Dim COL_BillProvider As Integer = 4
    Dim COL_RenProvider As Integer = 5
    Dim COL_Facility As Integer = 6
    Dim COL_DFS As Integer = 7
    Dim COL_COLCOUNT As Integer = 8
    ''

    ''Column constants for Appointment Type 
    Dim COL_APPOINTMENTTYPEID As Integer = 0
    Dim COL_APPOINTMENTTYPE As Integer = 1
    Dim COL_DOS As Integer = 2
    Dim COL_DFacility As Integer = 3
    Dim COL_RenderProvider As Integer = 4
    Dim COL_DACOUNT As Integer = 5
    ''

    Public Enum ANSIGrid
        ContactID
        ContactName
        ClaimBatchSettings
        EligiblityRequestSettings
    End Enum
    Public Enum ICD10Grid
        ContactID
        ContactName
        DOS
    End Enum
    Public Enum gloCollectGrid  'Code Added for New Tab gloCollect
        bIsGloCollect
        nUserID
        sLoginName
        sFirstName
        sMiddleName
        sLastName
        nBlockStatus
    End Enum

    ''Column For expanded claim Settings
    Private COL_ENUMID As Integer = 0
    Private COL_SETTINGTYPE As Integer = 1
    Private COL_CLAIMPERCHARGES As Integer = 2
    Private COL_DIAGNOSISPERCHARGES As Integer = 3
    Private COL_CNT As Integer = 4

    Private nonNumberEntered As Boolean = False

    ''----


    Private m_numofCapletters As Integer = 0
    Private m_numofLetters As Integer = 0
    Private m_numofspecialchars As Integer = 0
    Private m_numminlength As Integer = 0
    Private m_numofdays As Integer = 0
    Private m_numofdigits As Integer = 0
    Private m_sNDCSettings As String = String.Empty

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

    Private _Modified As Boolean = False

    Private htSourceSettings As New Hashtable
    Dim m_dtANSISettings As New DataTable
    Dim m_dtPaperFormSettings As New DataTable

    Private blnProcessFlag As Boolean
    Private blnMultipleGuarantorsProcessFlag As Boolean

    Private _dtRevenueCode As DataTable
    Private _dtProviders As DataTable
    Private _dtFeeschedules As DataTable

    'Private odsAdminSettingsTVP As dsAdminSettingsTVP

    Dim ogloSettings As New clsSettings

    Dim isSaveAndClose As Boolean = False


#End Region

#Region "properties"


    'properties
    Public Property NoofDigits() As Integer
        Get
            Return m_numofdigits
        End Get
        Set(ByVal Value As Integer)
            m_numofdigits = Value
        End Set
    End Property

    Public Property NDCSettings() As String
        Get
            Return m_sNDCSettings
        End Get
        Set(ByVal value As String)
            m_sNDCSettings = value
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

    Dim _completePaymentBeforeDailyCloseLoadValue As Boolean = False
    Public Property CompletePaymentBeforeDailyCloseLoadValue As Boolean
        Get
            Return _completePaymentBeforeDailyCloseLoadValue
        End Get
        Set(value As Boolean)
            _completePaymentBeforeDailyCloseLoadValue = value
        End Set
    End Property

#End Region


    Private Sub frmSettings_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load

        gloC1FlexStyle.Style(C1Provider)
        gloC1FlexStyle.Style(c1userList)
        gloC1FlexStyle.Style(C1surgicalUsers)
        gloC1FlexStyle.Style(c1Providers)
        tb_Settings.TabPages.RemoveAt(12)
        'tb_Settings.TabPages.Remove(tbpg_CleargageConfigurationSettings)
        Dim tabSettings As gloCommon.Cls_TabIndexSettings = Nothing
        Dim cm As ContextMenu = New ContextMenu
        txtPmntPlanDefFUActionDays.ContextMenu = cm
        txtPatAccNoOfDaysAfterStmnt.ContextMenu = cm
        txtPatAccFUBeginsAfterNoOfStmnt.ContextMenu = cm
        txtInsClmRebillFilingDays.ContextMenu = cm
        txtInsClmStartFilingDays.ContextMenu = cm
        Try

            RemoveHandler cmb_InsuranceType.SelectedIndexChanged, AddressOf cmb_InsuranceType_SelectedIndexChanged
            SetANSIAndPaperTab()
            FillCountry()
            GetFormData()

            optcnt = 0
            'Commented By Pramod Nair
            'blnadminflag = GetAdminFlag()

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

            'Commented By Pramod Nair
            'FillInsuranceType()
            'GetPaymentSetting()

            'Appointment settings : Filling combox box
            FillFutureAppointmentTypeList()
            FillSameAppointmentTypeList()

            Dim objSettings As New clsSettings
            If objSettings.GetSettings() = True Then

                If objSettings.OCPPortalEnable = True Then
                    rdbYesOCP.Checked = True
                ElseIf objSettings.OCPPortalEnable = False Then
                    rdbNoOCP.Checked = True
                End If
                If objSettings.IsCentralizedRuleEngineEnable Then
                    rdbEnableCentralizedRE.Checked = True
                    txtQCommunicationURL.Enabled = True
                Else
                    rdbDisableCentralizedRE.Checked = True
                    txtQCommunicationURL.Enabled = False
                End If
                txtQCommunicationURL.Text = objSettings.sCentralizedQCommunicationServiceURL
                cmbOCPDMSCategory.SelectedText = objSettings.OCPRCMDefaultCategory

                chkCapatalizeUB04Data.Checked = objSettings.UB04_Capitalizedata
                chkCapatalizeCMSData.Checked = objSettings.CMS1500_Capitalize0212data

                chkEnableCMSfontsizeselection.Checked = objSettings.EnableCMSFontSizeSelection
                ChkEnableUB04FontSelection.Checked = objSettings.EnableUBFontSizeSelection


                If Convert.ToString(objSettings.CMS1500_Font) <> "" And Convert.ToString(objSettings.CMS1500_FontSize) <> "" Then
                    txtFont_Cms.Text = Convert.ToString(objSettings.CMS1500_Font) + "," + Convert.ToString(objSettings.CMS1500_FontSize)
                End If

                If Convert.ToString(objSettings.UB04_Font) <> "" And Convert.ToString(objSettings.UB04_FontSize) <> "" Then
                    txtFont_Ub.Text = Convert.ToString(objSettings.UB04_Font) + "," + Convert.ToString(objSettings.UB04_FontSize)
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
                If IsNothing(objSettings.AppointmentInterval) = False Then
                    If objSettings.AppointmentInterval > 0 Then
                        AppointmentInterval.Value = objSettings.AppointmentInterval
                    Else
                        AppointmentInterval.Value = 5
                    End If
                End If

                ''Added on 7-dec-2012 for global hl7 outbound settings
                Try

                    RemoveHandler chkhl7outb.CheckedChanged, AddressOf chkhl7outb_CheckedChanged
                    chkhl7outb.Checked = objSettings.globlnhl7OutBound

                Catch ex As Exception
                Finally
                    AddHandler chkhl7outb.CheckedChanged, AddressOf chkhl7outb_CheckedChanged
                End Try

                chkNPOILibrary.Checked = objSettings.UseNPOIForExcelIntegration

                chkhl7PatientReg.Checked = objSettings.globlnhl7Sendpatientdet
                chkHL7Appointment.Checked = objSettings.globlnhl7Sendapptdet
                If ((chkhl7PatientReg.Checked = False) Or (chkHL7Appointment.Checked = False)) Then
                    chk_allhl7.Checked = False
                Else
                    chk_allhl7.Checked = True

                End If
                ''Added on 7-dec-2012 for global hl7 outbound settings

                'Changes Made by Subashish date 05-01-2011---------------------Start------ Change No SB0004------------
                'For adding/updating  the Patient Account Setting in setting table


                'blnProcessFlag = True
                'Me.rbPatientAccountFeatureEnabledYES.Checked = False
                'Me.rbPatientAccountFeatureEnabledNO.Checked = False
                'blnProcessFlag = False

                'If objSettings.IsPatientAccountFeatureEnabled = True Then
                '    Me.rbPatientAccountFeatureEnabledYES.Checked = True
                'Else
                '    blnProcessFlag = True
                '    Me.rbPatientAccountFeatureEnabledNO.Checked = True
                '    blnProcessFlag = False
                'End If

                'blnMultipleGuarantorsProcessFlag = True

                'Me.rbMultipleGuranterAllowYES.Checked = False
                'Me.rbMultipleGuranterAllowNO.Checked = False

                'If objSettings.IsAllowMultipleGuranterEnabled = True Then
                '    Me.rbMultipleGuranterAllowYES.Checked = True
                'Else
                '    Me.rbMultipleGuranterAllowNO.Checked = True
                'End If
                'blnMultipleGuarantorsProcessFlag = False



                'Me.rbCopyAccountYES.Checked = False
                'Me.rbCopyAccountNo.Checked = False

                'If objSettings.IsAllowCopyAccountEnabled = True Then
                '    Me.rbCopyAccountYES.Checked = True
                'Else
                '    Me.rbCopyAccountNo.Checked = True
                'End If


                'Me.rbMergeAccountYes.Checked = False
                'Me.rbMergeAccountNo.Checked = False

                'If objSettings.IsAllowMergeAccountEnabled = True Then
                '    Me.rbMergeAccountYes.Checked = True
                'Else
                '    Me.rbMergeAccountNo.Checked = True
                'End If


                optPwdComplexYes.Checked = objSettings.blnPwdComplexity


                '      txtNoOfAttempts.Text = objSettings.NoOfAttempts
                If (objSettings.NoOfAttempts < numLockOutAttempts.Minimum Or objSettings.NoOfAttempts > numLockOutAttempts.Maximum) Then
                    numLockOutAttempts.Value = numLockOutAttempts.Maximum
                Else
                    numLockOutAttempts.Value = objSettings.NoOfAttempts
                End If


                '-------------------------
            End If
                ''7022Items: Home Billing- Added to show previously saved settings saved in database.
                Try
                    RemoveHandler rbtn_UseAreaCodeForPatientYes.CheckedChanged, AddressOf rbtn_UseAreaCodeForPatientYes_CheckedChanged
                    rbtn_UseAreaCodeForPatientYes.Checked = objSettings.UseAreaCodeForPatient
                Catch ex As Exception

                Finally
                    AddHandler rbtn_UseAreaCodeForPatientYes.CheckedChanged, AddressOf rbtn_UseAreaCodeForPatientYes_CheckedChanged
                End Try



                If optPwdComplexNo.Checked = True Then
                    btnSetPwdComplexity.Visible = False
                End If

                'sarika 11th aug 07
                txtHL7FilePath.Text = objSettings.HL7SystemPath
                '******

                'txtSendFacility.Text = objSettings.HL7SendingFacility
                'txtRecAppl.Text = objSettings.HL7ReceivingApplication
                'txtRecFacility.Text = objSettings.HL7ReceivingFacility

                '******
                '-------------------

                'sarika 31st aug 07
                If Trim(objSettings.DBVersion) <> "" Then
                    txtDBVersion.Text = objSettings.DBVersion
                End If
                If Trim(objSettings.AppVersion) <> "" Then
                    txtAppVersion.Text = objSettings.AppVersion

                    'Added by Rahul Patel on 28-09-2010
                    'Resolving issuse mention by Dan of not displaying the db version
                    txtDBVersion.Text = objSettings.AppVersion
                End If
                '-------------

                If objSettings.ExplicitlyAcceptTask = 1 Then
                    ChkExplicitlyAcceptTask.Checked = True
                Else
                    ChkExplicitlyAcceptTask.Checked = False
                End If


                ''''''''''''''''''Code Added by Anil on 20071119
                If objSettings.AutoGeneratePatientCode = False Then
                    chkAutogenerateCode.Checked = objSettings.AutoGeneratePatientCode
                Else
                    chkAutogenerateCode.Checked = True
                End If
                ''Added by Mayuri:20101006-To add functonality of save as copy patient
                If objSettings.AllowEditablePatientCode = False Then
                    chkallowEditPatientCode.Checked = objSettings.AllowEditablePatientCode
                Else
                    chkallowEditPatientCode.Checked = True
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




                If objSettings.GenerateUnclosedDayStatement = True Then
                    chkUnclosedDayStatement.Checked = objSettings.GenerateUnclosedDayStatement
                Else
                    chkUnclosedDayStatement.Checked = False
                End If

                If objSettings.MultipleClearingHouse = True Then
                    chkIsMultipleClearingHouse.Checked = objSettings.MultipleClearingHouse
                Else
                    chkIsMultipleClearingHouse.Checked = False
                End If

                If objSettings.FutureCloseDateDays <> 0 Then
                    numFutureCloseDateDays.Value = objSettings.FutureCloseDateDays
                Else
                    numFutureCloseDateDays.Value = 7
                End If

                txtStatementMinPay.Text = "$" + objSettings.StatementMinPay

                txtSiteID.Text = objSettings.SiteID
                ''by sudhir 20081111 
                'Show Age in Days
                If IsNothing(objSettings.ShowAgeInDays) = False Then
                    chk_AgeFlag.Checked = objSettings.ShowAgeInDays
                Else
                    chk_AgeFlag.Checked = False
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


                '------------------
                ''Sandip Darade 20090731
                If (gstrAdminFor = "gloPM") Then

                    tbp_PMDBSettings.Text = "gloEMR Database Settings"
                    chk_PMDBSettings.Text = "Add patient to gloEMR"
                    lblServerName.Text = "gloEMR Servername :"
                    lblDatabaseName.Text = "gloEMR Databasename :"
                    pnl_Migratetype.Visible = True
                    SavegloEMRDatabaseSettings()
                    RetrievegloEMRDatabaseSettings()
                    Set_gloEMRDBsettingControls()
                    ''Sandip Darade 20090814
                    ''Remove DM setting ,Fax setting and other setting tab for gloPM Admin
                    tb_Settings.Controls.Remove(tbp_EMCodeSetting)
                    tb_Settings.Controls.Remove(tbp_FaxSettings)
                    tb_Settings.Controls.Remove(tbp_OtherSettings)
                    tb_Settings.Controls.Remove(tbp_OMRSettings)

                    tb_Settings.Controls.Remove(tbp_PMDBSettings)

                    'Added by Rahul Patel on 13/09/2010
                    'For Hiding "Exchange Server Setting Tab
                    tb_Settings.Controls.Remove(TabPage18)
                    tb_Settings.Controls.Remove(tbpg_AlphaIISettings)

                Else
                    'sarika PM DB Credentials 20081128
                    pnl_Migratetype.Visible = False
                    pnlgloPMDBSettings.Enabled = objSettings.PMAddPatient
                    chk_PMDBSettings.Checked = objSettings.PMAddPatient
                    ''Sandip darade 
                    If (objSettings.PMAddPatient = True) Then
                        txtPMServerName.Text = objSettings.PMServerName
                        txtPMDatabaseName.Text = objSettings.PMDatabaseName
                        If objSettings.PMSQLAuthentication = 0 Then
                            optSQLAuthentication.Checked = False
                            pnlSQLCredentials.Enabled = False
                        Else
                            optSQLAuthentication.Checked = True
                            pnlSQLCredentials.Enabled = True
                        End If
                        'If optSQLAuthentication.Checked = True Then
                        txtSQLUserID.Text = objSettings.PMUserID
                        '     Dim objEncryptDecrypt As New clsEncryption
                        ' txtSQLPassword.Text = objEncryptDecrypt.DecryptFromBase64String(objSettings.PwdStrSQL, constEncryptDecryptKey)
                        '   objEncryptDecrypt = Nothing

                        txtSQLPassword.Text = objSettings.PMSQLPwd
                    End If
                End If

                ''''Payment setting and exam setting removed for gloPM 
                tb_Settings.Controls.Remove(tbp_PaymentSettings)
                tb_Settings.Controls.Remove(tbpg_ExamSettings)

                '--
                txtSignaturetext.Text = objSettings.SignatureText
                txtcoSignaturetext.Text = objSettings.CoSignatureText

                If (objSettings.DMSImageDIP < numDMSImageDPI.Minimum Or objSettings.DMSImageDIP > numDMSImageDPI.Maximum) Then
                    numDMSImageDPI.Value = numDMSImageDPI.Maximum
                Else
                    numDMSImageDPI.Value = objSettings.DMSImageDIP
                End If

                chkUseFileCompession.Checked = objSettings.UseFileCompression

                chkSplitDoc.Checked = objSettings.SplitDocument

                chkRecoverDMSV2Doc.Checked = objSettings.RecoveryDMSV2Doc
                txt_DMSV2RecoveryPath.Text = objSettings.RecoveryDMSV2Path

                If chkRecoverDMSV2Doc.Checked = False Then

                    lblDMSV2RecoverPath.Enabled = False
                    txt_DMSV2RecoveryPath.Enabled = False
                    btnSelectRecoveryPath.Enabled = False
                End If

                chk_DeleteDocAfterMigration.Checked = objSettings.DeleteDMSDocAfterMigration

                Chb_UseCodedhistory.Checked = objSettings.Usecodedhistory


                If objSettings.ShowCodedHistory = "Code" Then
                    Rbtn_showcode.Checked = True
                ElseIf objSettings.ShowCodedHistory = "Description" Then
                    Rbtn_showDesc.Checked = True
                Else
                    Rbtn_ShowBoth.Checked = True
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



                'sarika SiteID 20090708
                If IsNothing(objSettings.SiteID) = False Then
                    txtSiteID.Text = objSettings.SiteID
                Else
                    txtSiteID.Text = ""
                End If
                '---


                If objSettings.EMChiefComplaintType = "ChiefComplaint" Then
                    rbChiefComplaint.Checked = True
                ElseIf objSettings.EMChiefComplaintType = "ProblemList" Then
                    rbProblemList.Checked = True
                ElseIf objSettings.EMChiefComplaintType = "" Then
                    rbChiefComplaint.Checked = True
                Else
                    rbChiefComplaint.Checked = True
                End If
                'sarika 31st aug 07
                'fill the arraylist with fax values at form load
                Dim arrList As ArrayList
                arrList = New ArrayList
                SetData(arrList)

                _arrayGetData = arrList
                '---------


                cmbGender.SelectedItem = objSettings.DefaultPatientGender


                ''billing settings 
                ''RemoveHandler Me.cmbProvider.SelectedIndexChanged, AddressOf Me.cmbProvider_SelectedIndexChanged

                'Commented By Pramod Nair
                'FillProviders()
                'FillPOS()
                'FillTOS()
                'FillFacilities()
                'FillFeeSchedule()
                'FillBillingSettings()

                ''Added By Debasish Das on 18th March 2010
                ' FillOtherBillingSettings()
                'GetOtherBillings()

                GetOtherBillings(objSettings)
                ''*****************************************

                'Other billing settings 
                'FillFeeSchedules()
                'FillSpecialities()
                cmbFacilityType.Items.Add("")
                cmbFacilityType.Items.Add(FacilityType.Facility.ToString())
                cmbFacilityType.Items.Add(FacilityType.NonFacility.ToString())




                'FillMidLevelSettings()
                ''Sandip Darade 20091211
                'DesignGridForBillingSetting()

                ''Design Claim
                'DesignGridForExpandedClaimSettings()
                ''

                ''Design Mid Level Billing Grid and fill Data
                'Call DesignGridForMidLevelBillingSetting()

                'GetMidLevelBillingSettings()

                'GetBillingSettings()

                ''Setting ANSI Tab
                'Dim objGenSettings As New GeneralSettings(mdlGeneral.GetConnectionString)
                'Dim oDatatable As DataTable = objGenSettings.GetEnumItems(gloSettings.ANSIVersions.ANSI_4010, True)
                'If Not oDatatable Is Nothing AndAlso oDatatable.Rows.Count > 0 Then
                '    cmbANSIClaimSettings.DataSource = oDatatable.Copy()
                '    cmbANSIClaimSettings.DisplayMember = "sDescription"
                '    cmbANSIClaimSettings.ValueMember = "nID"
                '    cmbANSIClaimSettings.Update()

                '    cmbANSIEligiblitySettings.DataSource = oDatatable.Copy()
                '    cmbANSIEligiblitySettings.DisplayMember = "sDescription"
                '    cmbANSIEligiblitySettings.ValueMember = "nID"
                '    cmbANSIEligiblitySettings.Update()
                'End If


                'DesignGridForANSIVersionSetting()
                'Dim clsANSIversion As New CLS_ANSIversion()
                'Dim _dt As DataTable = clsANSIversion.FetchClinicLevelRecords()

                'If _dt.Rows.Count > 0 Then
                '    cmbANSIClaimSettings.SelectedValue = Convert.ToInt64(_dt.Rows(0)("nClaimVersion"))
                '    cmbANSIEligiblitySettings.SelectedValue = Convert.ToInt64(_dt.Rows(0)("nEligVersion"))
                'End If

                'm_dtANSISettings = clsANSIversion.FetchPlanLevelRecords()
                'GetANSISettings(m_dtANSISettings, False)

                SetICD10TransitionTab()
                ''***

                '' end Sandip Darade 20091211

                ''Retrieve other billing setting 
                If (objSettings.DefaultSelfPayAllowed = "") Then
                    cmbSlfPayAllwdAmnts.SelectedValue = objSettings.ClinicFeeSchedule
                Else
                    cmbSlfPayAllwdAmnts.SelectedValue = objSettings.DefaultSelfPayAllowed
            End If

            If (objSettings.DefaultSelfPayFeeSchedule = "") Then
                cmb_SlfPayDefaultFeeSchedule.SelectedValue = objSettings.ClinicFeeSchedule
            Else
                cmb_SlfPayDefaultFeeSchedule.SelectedValue = objSettings.DefaultSelfPayFeeSchedule
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
                    cmbFacilityType.Text = FacilityType.NonFacility.ToString()
                End If

                ''Sandip Darade 20091107
                If (Not IsNothing(objSettings.ClinicFeeSchedule) And objSettings.ClinicFeeSchedule <> "") Then
                    cmb_Feeschedules.SelectedValue = objSettings.ClinicFeeSchedule
                End If



                cmbpatientaccountcode.Items.Clear()
                cmbpatientaccountcode.Items.Add("")
                cmbpatientaccountcode.Items.Add("Patient Code")
                cmbpatientaccountcode.Items.Add("Claim Number")
                If Convert.ToString(objSettings.PatientAccountCode.ToString() <> "") Then
                    If (objSettings.PatientAccountCode.ToString() = "Patient Code") Then
                        cmbpatientaccountcode.SelectedIndex = 1
                    ElseIf (objSettings.PatientAccountCode.ToString() = "Claim Number") Then
                        cmbpatientaccountcode.SelectedIndex = 2
                    Else
                        cmbpatientaccountcode.SelectedIndex = 0
                    End If

                End If

                ''MaheshB 20091127

                If Convert.ToString(objSettings.IncludeFacilitieswithPOS11onClaim) = "True" Then

                    rbPrintFacilityYes.Checked = True
                    rbPrintFacilityNo.Checked = False
                Else
                    rbPrintFacilityNo.Checked = True
                    rbPrintFacilityYes.Checked = False
                End If

                'Add by  for getting UB04Billing Previous settings vijay20100813
                If Convert.ToString(objSettings.UB04_EnableBilling) = "True" Then
                    'Dim _dtRevenueCode As DataTable
                    RbUbBilling_yes.Checked = True
                    RbUbBilling_No.Checked = False
                    '_dtRevenueCode = GetRevenueCode()
                    CmbRevenueCode.Items.Clear()
                    CmbRevenueCode.Items.Add("")
                    For index As Integer = 0 To _dtRevenueCode.Rows.Count - 1
                        CmbRevenueCode.Items.Add(_dtRevenueCode.Rows(index)("sRevenueCode"))
                    Next

                    CmbRevenueCode.Enabled = True

                    TxtTypeBill.Enabled = True

                    TxtAdmissionType.Enabled = True

                    TxtAdmisionSource.Enabled = True

                    txtDischargeStatus.Enabled = True


                Else
                    'Dim _dtRevenueCode As DataTable
                    '_dtRevenueCode = GetRevenueCode()
                    CmbRevenueCode.Items.Clear()
                    CmbRevenueCode.Items.Add("")
                    For index As Integer = 0 To _dtRevenueCode.Rows.Count - 1
                        CmbRevenueCode.Items.Add(_dtRevenueCode.Rows(index)("sRevenueCode"))
                    Next
                    CmbRevenueCode.SelectedItem = ""
                    CmbRevenueCode.Enabled = False
                    TxtTypeBill.Text = ""
                    TxtTypeBill.Enabled = False
                    TxtAdmissionType.Text = ""
                    TxtAdmissionType.Enabled = False
                    TxtAdmisionSource.Text = ""
                    TxtAdmisionSource.Enabled = False
                    txtDischargeStatus.Text = ""
                    txtDischargeStatus.Enabled = False


                End If

                If Convert.ToString(objSettings.UB04_RevenueCode) <> "" Then
                    CmbRevenueCode.SelectedItem = objSettings.UB04_RevenueCode
                Else
                    CmbRevenueCode.SelectedValue = ""

                End If



                If Convert.ToString(objSettings.UB04_TypeOfBilling) <> "" Then
                    TxtTypeBill.Text = objSettings.UB04_TypeOfBilling
                Else
                    TxtTypeBill.Text = ""
                End If
                If Convert.ToString(objSettings.UB04_AdmisionType) <> "" Then
                    TxtAdmissionType.Text = objSettings.UB04_AdmisionType
                Else
                    'If (Convert.ToString(objSettings.UB04_EnableBilling) = "True") Then
                    'TxtAdmissionType.Text = "3"
                    ' Else
                    TxtAdmissionType.Text = ""
                    ' End If
                End If
                If Convert.ToString(objSettings.UB04_AdmisionSource) <> "" Then
                    TxtAdmisionSource.Text = objSettings.UB04_AdmisionSource
                Else
                    ' If (Convert.ToString(objSettings.UB04_EnableBilling) = "True") Then
                    TxtAdmisionSource.Text = "2"
                    ' Else
                    TxtAdmisionSource.Text = ""
                    ' End If


                End If
                If Convert.ToString(objSettings.UB04_DischargeStatus) <> "" Then
                    txtDischargeStatus.Text = objSettings.UB04_DischargeStatus
                Else
                    'If (Convert.ToString(objSettings.UB04_EnableBilling) = "True") Then
                    'txtDischargeStatus.Text = "01"
                    ' Else
                    txtDischargeStatus.Text = ""
                    ' End If
                End If

                'End vijay20100813

                'Mahesh Nawal   20100407
                chkcopyclaimstoserver.Checked = True
                If Convert.ToString(objSettings.CopyClaimtoServer) = "0" Then
                    chkcopyclaimstoserver.Checked = False
                End If

                '' end Retrieve other billing setting 
                cmbAlphaIIAuthentication.Items.Add("Windows")
                cmbAlphaIIAuthentication.Items.Add("SQL")
                If cmbAlphaIIAuthentication.Items.Count > 0 Then
                    cmbAlphaIIAuthentication.SelectedIndex = 0
                End If

                RetrieveAlphaIIDatabaseSettings()
                ''''
                ''Sandip Darade 20090710
                ''Marital Status setting
            FillMaritalStatusFromBilling()
                FillMaritalStatusFromPatientRegistration()
                GetMaritalStatusSettings()

            'Setting for ClaimPrefix'
            GetClaimPrefixSettings()

                'FillPaperBilling()  'vijay for Paper Billing Setting'
                ''End Marital Status setting
                ''Exchange Server settings
                GetExServerSetting()
                ''End Exchange Server settings
                ''get weekdays setting
                GetOtherSettings()
                ''  Call fill_SurgicalAlertUsers()

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


                ''sudhir 20090107 -- for Provider List on C1Grid
                If chk_PMDBSettings.Checked = True Then
                    FillProviderGridAll()
                End If

                If (cmbCountry.Items.Count > 0) Then
                    If Trim(objSettings.Country) <> "" Then
                        cmbCountry.SelectedValue = objSettings.Country
                    Else
                        cmbCountry.SelectedIndex = 0
                    End If
                End If

                If (cmbFutureApptType.Items.Count > 0) Then
                    If Trim(objSettings.FutureApptType) <> "" Then
                        cmbFutureApptType.SelectedValue = objSettings.FutureApptType
                    Else
                        cmbFutureApptType.SelectedIndex = 0
                    End If
                End If

                If (cmbSameDayApptType.Items.Count > 0) Then
                    If Trim(objSettings.FutureApptType) <> "" Then
                        cmbSameDayApptType.SelectedValue = objSettings.SameDayApptType
                    Else
                        cmbSameDayApptType.SelectedIndex = 0
                    End If
                End If

                chkEnablePatientAppointmentsLinkingToCharges.Checked = objSettings.EnablePatientAppointmentsLinkingToCharges
                ' DesignGridForProviderSetting()



                ''Fill the Clinic Level Setitings Combo
                'FillIDQualifiersAssociation(cmbPaperRendering)
                'FillIDQualifiersAssociation(cmbElectronicRendering)

                'FillServiceFacility()
                'FillServiceFacilityForSubmitter(cmbSubmitter)
                ''FillServiceFacility(cmbBillingProvSource)

                'FillBilingIDQualifier()
                ''End

                'FillProviderSettings()

                Try


                    Dim oResult As Object
                    Dim ogloSettings As gloSettings.GeneralSettings = New gloSettings.GeneralSettings(mdlGeneral.GetConnectionString)
                    ogloSettings.GetSetting("Complete Payments before Daily Close", oResult)

                    CompletePaymentBeforeDailyCloseLoadValue = False
                    If (oResult <> Nothing And oResult.ToString() <> "") Then
                        chkPaymentBeforeDailyClose.Checked = oResult
                        CompletePaymentBeforeDailyCloseLoadValue = Convert.ToBoolean(oResult)
                    End If
                    oResult = Nothing

                    ogloSettings.GetSetting("NDCUnitPricing", oResult)
                    If (oResult <> Nothing And oResult.ToString() <> "") Then
                        NDCSettings = oResult.ToString()

                        If oResult.ToString() = "Calculated" Then
                            rbCalculated.Checked = True
                        ElseIf oResult.ToString() = "Zero" Then
                            rbZero.Checked = True
                        End If
                    End If

                    oResult = Nothing
                    ogloSettings.GetSetting("Enable Insurance Plan 5010", oResult)
                    If oResult.ToString().ToUpper() = "FALSE" Then
                        chkPlan5010.Checked = False
                    ElseIf oResult.ToString().ToUpper() = "TRUE" Then
                        chkPlan5010.Checked = True
                    End If


                    ''Global Periods Settings 
                    oResult = Nothing
                    ogloSettings.GetSetting("SkipZero_GlobalPeriods_CPT", oResult)
                    If (oResult <> Nothing And oResult.ToString() <> "") Then
                        chkSkipZeroGlobalPeriods.Checked = oResult
                    Else
                        chkSkipZeroGlobalPeriods.Checked = False
                    End If

                    oResult = Nothing
                    ogloSettings.GetSetting("ShowInitialTreatmentDate", oResult)
                    If (oResult <> Nothing And oResult.ToString() <> "") Then
                        If (Convert.ToBoolean(oResult) = True) Then
                            chkInitialTreatmentDate.Checked = True
                        Else
                            chkInitialTreatmentDate.Checked = False
                        End If
                    Else
                        chkInitialTreatmentDate.Checked = False
                    End If


                    RemoveHandler rb_BC_PatAcc_Yes.CheckedChanged, AddressOf rb_BC_PatAcc_Yes_CheckedChanged

                    oResult = Nothing
                    ogloSettings.GetSetting("BusinessCenter_PatientAccount", oResult)
                    If (oResult <> Nothing And oResult.ToString() <> "") Then
                        If (oResult = "True") Then
                            rb_BC_PatAcc_Yes.Checked = True
                            rb_BC_PatAcc_No.Checked = False
                            pnl_BC_PatAccSettings.Enabled = True
                        Else
                            rb_BC_PatAcc_Yes.Checked = False
                            rb_BC_PatAcc_No.Checked = True
                            pnl_BC_PatAccSettings.Enabled = False
                        End If
                    Else
                        rb_BC_PatAcc_Yes.Checked = False
                        rb_BC_PatAcc_No.Checked = False
                        pnl_BC_PatAccSettings.Enabled = False
                    End If

                    AddHandler rb_BC_PatAcc_Yes.CheckedChanged, AddressOf rb_BC_PatAcc_Yes_CheckedChanged

                    oResult = Nothing
                    ogloSettings.GetSetting("BusinessCenter_Feature", oResult)
                    If (oResult <> Nothing And oResult.ToString() <> "") Then
                        If (oResult = "True") Then

                            rb_BusinessCenter.Checked = True

                            pnl_BC_PatAcc.Enabled = True
                            If rb_BC_PatAcc_No.Checked = True Then
                                pnl_BC_PatAccSettings.Enabled = False
                            Else
                                pnl_BC_PatAccSettings.Enabled = True
                            End If

                        Else
                            rb_BusinessCenter.Checked = False
                            pnl_BC_PatAcc.Enabled = False
                            pnl_BC_PatAccSettings.Enabled = False
                        End If
                    Else
                        rb_BusinessCenter.Checked = False
                        pnl_BC_PatAcc.Enabled = False
                        pnl_BC_PatAccSettings.Enabled = False
                    End If


                    oResult = Nothing
                    ogloSettings.GetSetting("BusinessCenter_Statment", oResult)
                    If (oResult <> Nothing And oResult.ToString() <> "") Then
                        If (oResult = "True") Then
                            rb_BC_PatAcc_Statment_Yes.Checked = True
                            rb_BC_PatAcc_Statment_No.Checked = False
                        Else
                            rb_BC_PatAcc_Statment_Yes.Checked = False
                            rb_BC_PatAcc_Statment_No.Checked = True
                        End If
                    Else
                        rb_BC_PatAcc_Statment_Yes.Checked = False
                        rb_BC_PatAcc_Statment_No.Checked = False
                    End If

                    oResult = Nothing
                    ogloSettings.GetSetting("BusinessCenter_FollowupQueue", oResult)
                    If (oResult <> Nothing And oResult.ToString() <> "") Then
                        If (oResult = "True") Then
                            rb_BC_PatAcc_FollowUp_Yes.Checked = True
                            rb_BC_PatAcc_FollowUp_No.Checked = False
                        Else
                            rb_BC_PatAcc_FollowUp_Yes.Checked = False
                            rb_BC_PatAcc_FollowUp_No.Checked = True
                        End If
                    Else
                        rb_BC_PatAcc_FollowUp_Yes.Checked = False
                        rb_BC_PatAcc_FollowUp_No.Checked = False
                    End If

                    oResult = Nothing
                    ogloSettings.GetSetting("BusinessCenter_ClaimBatch", oResult)
                    If (oResult <> Nothing And oResult.ToString() <> "") Then
                        If (oResult = "True") Then
                            rb_BC_PatAcc_ClaimBatch_Yes.Checked = True
                            rb_BC_PatAcc_ClaimBatch_No.Checked = False
                        Else
                            rb_BC_PatAcc_ClaimBatch_Yes.Checked = False
                            rb_BC_PatAcc_ClaimBatch_No.Checked = True
                        End If
                    Else
                        rb_BC_PatAcc_ClaimBatch_Yes.Checked = False
                        rb_BC_PatAcc_ClaimBatch_No.Checked = False
                    End If




                    oResult = Nothing
                    ogloSettings.GetSetting("BusinessCenter_Mismatch_ChargeSaveAlert", oResult)
                    If (oResult <> Nothing And oResult.ToString() <> "") Then
                        If (oResult = "Warn") Then
                            rb_BC_PatAcc_ChargeMismatch_Warn.Checked = True
                            'rb_BC_PatAcc_ChargeMismatch_Restrict.Checked = False
                            rb_BC_PatAcc_ChargeMismatch_None.Checked = False
                        ElseIf (oResult = "Restrict") Then
                            rb_BC_PatAcc_ChargeMismatch_Warn.Checked = False
                            'rb_BC_PatAcc_ChargeMismatch_Restrict.Checked = True
                            rb_BC_PatAcc_ChargeMismatch_None.Checked = False
                        Else
                            rb_BC_PatAcc_ChargeMismatch_Warn.Checked = False
                            'rb_BC_PatAcc_ChargeMismatch_Restrict.Checked = False
                            rb_BC_PatAcc_ChargeMismatch_None.Checked = True
                        End If
                    Else
                        rb_BC_PatAcc_ChargeMismatch_Warn.Checked = False
                        'rb_BC_PatAcc_ChargeMismatch_Restrict.Checked = False
                        rb_BC_PatAcc_ChargeMismatch_None.Checked = False
                    End If

                    oResult = Nothing
                    ogloSettings.GetSetting("SplitClaimToPatient", oResult)

                    RemoveHandler chkSplitClaimToPatient.CheckedChanged, AddressOf chkSplitClaimToPatient_CheckedChanged
                    If (oResult <> Nothing And oResult.ToString() <> "") Then
                        If (oResult = "True") Then
                            chkSplitClaimToPatient.Checked = True
                        Else
                            chkSplitClaimToPatient.Checked = False
                        End If
                    Else
                        chkSplitClaimToPatient.Checked = False
                    End If
                    AddHandler chkSplitClaimToPatient.CheckedChanged, AddressOf chkSplitClaimToPatient_CheckedChanged


                    'oResult = Nothing
                    'ogloSettings.GetSetting("BusinessCenter_Mismatch_OfferNewAccount", oResult)
                    'If (oResult <> Nothing And oResult.ToString() <> "") Then
                    '    If (oResult = "True") Then
                    '        rb_BC_PatAcc_ChargeMismatch_NewAcc_Yes.Checked = True
                    '        rb_BC_PatAcc_ChargeMismatch_NewAcc_No.Checked = False
                    '    Else
                    '        rb_BC_PatAcc_ChargeMismatch_NewAcc_Yes.Checked = False
                    '        rb_BC_PatAcc_ChargeMismatch_NewAcc_No.Checked = True
                    '    End If
                    'Else
                    '    rb_BC_PatAcc_ChargeMismatch_NewAcc_Yes.Checked = False
                    '    rb_BC_PatAcc_ChargeMismatch_NewAcc_No.Checked = False
                    'End If
                    oResult = Nothing
                    ogloSettings.GetSetting("bDuplicateClaimWarning", oResult)
                    If (oResult <> Nothing And oResult.ToString() <> "") Then
                        If (Convert.ToBoolean(oResult) = True) Then
                            chkDuplicateClaimWarning.Checked = True
                        Else
                            chkDuplicateClaimWarning.Checked = False
                        End If
                    Else
                        chkDuplicateClaimWarning.Checked = False

                    End If


                    'hemant added below

                    oResult = Nothing
                    ogloSettings.GetSetting("bEnableclaimRule", oResult)
                    If (oResult <> Nothing And oResult.ToString() <> "") Then
                        If (Convert.ToBoolean(oResult) = True) Then
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ChargeRule, gloAuditTrail.ActivityCategory.ChargeRuleSetting, gloAuditTrail.ActivityType.Save, "Claim rule setting turn ON", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPMAdmin, True)
                            chkEnableclaimRule.Checked = True
                        Else

                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ChargeRule, gloAuditTrail.ActivityCategory.ChargeRuleSetting, gloAuditTrail.ActivityType.Save, "Claim rule setting turn OFF", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPMAdmin, True)
                            chkEnableclaimRule.Checked = False
                        End If
                    Else
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ChargeRule, gloAuditTrail.ActivityCategory.ChargeRuleSetting, gloAuditTrail.ActivityType.Save, "Claim rule setting turn OFF", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPMAdmin, True)
                        chkEnableclaimRule.Checked = False

                    End If
                    'hemant added above

                    oResult = Nothing
                    ogloSettings.GetSetting("bViewDocumentsOnCharges", oResult)
                    If (oResult <> Nothing And oResult.ToString() <> "") Then
                        If (Convert.ToBoolean(oResult) = True) Then
                            chkViewDocumentsOnCharges.Checked = True
                        Else
                            chkViewDocumentsOnCharges.Checked = False
                        End If
                    Else
                        chkViewDocumentsOnCharges.Checked = False
                    End If

                    ogloSettings.Dispose()
                    EnableDisableDefaultInterfaceSettings()

                oResult = Nothing
                ogloSettings.GetSetting("EnableEstablishedNonEstablishedPatientWarning", oResult)
                If (oResult <> Nothing And oResult.ToString() <> "") Then
                    If (Convert.ToBoolean(oResult) = True) Then
                        chkEnableEstablishNonEstablPatWarning.Checked = True
                    Else
                        chkEnableEstablishNonEstablPatWarning.Checked = False
                    End If
                Else
                    chkEnableEstablishNonEstablPatWarning.Checked = False
                End If

                oResult = Nothing
                ogloSettings.GetSetting("EnableDublicateCPTsWarning", oResult)
                If (oResult <> Nothing And oResult.ToString() <> "") Then
                    If (Convert.ToBoolean(oResult) = True) Then
                        chkDuplicateCPTDXWarning.Checked = True
                    Else
                        chkDuplicateCPTDXWarning.Checked = False
                    End If
                Else
                    chkDuplicateCPTDXWarning.Checked = False
                End If

                Catch ex As Exception

                End Try

                RemoveHandler Me.C1ExpandedClaimSettings.CellChanged, AddressOf Me.C1ExpandedClaimSettings_CellChanged
                'GetExpandedClaimSettingsValue(gloSettings.ExpandedClaimSettingLevel.Clinic.GetHashCode())
                AddHandler Me.C1ExpandedClaimSettings.CellChanged, AddressOf Me.C1ExpandedClaimSettings_CellChanged
                Me.Cursor = Cursors.Default

                'Check gloTSprint Variable
                If (gloGlobal.gloTSPrint.TerminalServer() <> "RDP") OrElse Not gloGlobal.gloTSPrint.isMapped() Then
                    chkEnableLocalPrinter.Enabled = False
                    pnlPrintClaims.Enabled = False
                Else
                    If gloGlobal.gloTSPrint.isCopyPrint Then
                        chkEnableLocalPrinter.Checked = True
                        pnlPrintClaims.Enabled = True
                    Else
                        chkEnableLocalPrinter.Checked = False
                        pnlPrintClaims.Enabled = False
                    End If
                    If gloGlobal.gloTSPrint.UseEMFForClaims Then
                        rbPrintClaimsEMF.Checked = True
                        rbPrintClaimsPDF.Checked = False
                    Else
                        rbPrintClaimsEMF.Checked = False
                        rbPrintClaimsPDF.Checked = True
                    End If
            End If
            chkCorrectRemittance.Checked = objSettings.CorrectRemittance

            ' ''Hide changes for Font for CMS and UB
            'chkCapatalizeCMSData.Visible = False
            'chkCapatalizeUB04Data.Visible = False
            'txtFont_Cms.Visible = False
            'txtFont_Ub.Visible = False
            'btnBrowseFont_Cms.Visible = False
            'btnClearFont_Cms.Visible = False
            'btnBrowseFont_Ub.Visible = False
            'btnClearFont_Ub.Visible = False
            'Label873.Visible = False
            'Panel189.Visible = False
            GetRCM_DOCCategory()
            Dim objOCPSettings As New clsSettings
            Dim OCPDefaultRCMCategory As New Object
            objOCPSettings.GetSetting("OCPDEFAULTRCMCATEGORY", 0, gnClinicID, OCPDefaultRCMCategory)
            objOCPSettings = Nothing
            If OCPDefaultRCMCategory <> "" Then
                cmbOCPDMSCategory.Text = OCPDefaultRCMCategory
            Else
                cmbOCPDMSCategory.Text = ""
            End If
        Catch objErr As Exception
            Me.Cursor = Cursors.Default
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.Cursor = Cursors.Default
            AddHandler Me.cmbProvider.SelectedIndexChanged, AddressOf Me.cmbProvider_SelectedIndexChanged
            AddHandler cmb_InsuranceType.SelectedIndexChanged, AddressOf cmb_InsuranceType_SelectedIndexChanged
            _Modified = False
            Me.Text = tb_Settings.TabPages(tb_Settings.SelectedIndex).Text

            tabSettings = New gloCommon.Cls_TabIndexSettings(Me)
            tabSettings.SetTabOrder(gloCommon.Cls_TabIndexSettings.TabScheme.AcrossFirst)

        End Try
    End Sub

    Private Sub SetANSIAndPaperTab()

        Dim objGenSettings As GeneralSettings = Nothing
        Dim oDatatable As DataTable = Nothing
        Dim clsANSIversion As CLS_ANSIversion = Nothing
        Dim _dt As DataTable = Nothing

        Try

            objGenSettings = New GeneralSettings(mdlGeneral.GetConnectionString)

            'Set ANSI tab first
            oDatatable = objGenSettings.GetEnumItems(gloSettings.ANSIVersions.ANSI_4010, True)
            If Not oDatatable Is Nothing AndAlso oDatatable.Rows.Count > 0 Then
                cmbANSIClaimSettings.DataSource = oDatatable.Copy()
                cmbANSIClaimSettings.DisplayMember = "sDescription"
                cmbANSIClaimSettings.ValueMember = "nID"
                cmbANSIClaimSettings.Update()

                cmbANSIEligiblitySettings.DataSource = oDatatable.Copy()
                cmbANSIEligiblitySettings.DisplayMember = "sDescription"
                cmbANSIEligiblitySettings.ValueMember = "nID"
                cmbANSIEligiblitySettings.Update()
            End If

            DesignGridForANSIVersionSetting(C1ANSIVersionSettings)

            clsANSIversion = New CLS_ANSIversion()
            _dt = clsANSIversion.FetchClinicLevelRecords()

            If Not IsNothing(_dt) And _dt.Rows.Count > 0 Then
                cmbANSIClaimSettings.SelectedValue = Convert.ToInt64(_dt.Rows(0)("nClaimVersion"))
                cmbANSIEligiblitySettings.SelectedValue = Convert.ToInt64(_dt.Rows(0)("nEligVersion"))
            End If

            m_dtANSISettings = clsANSIversion.FetchPlanLevelRecords()
            GetANSISettings(m_dtANSISettings, False)


            'Set Paper tab
            oDatatable = Nothing
            oDatatable = objGenSettings.GetEnumItemsDescriptionPaperFormVersion(gloSettings.PaperFormVersion.CMS1500, False)


            If Not IsNothing(objGenSettings) AndAlso oDatatable.Rows.Count > 0 Then

                cmbPaperVersion.DataSource = oDatatable.Copy()
                cmbPaperVersion.DisplayMember = "sDescription"
                cmbPaperVersion.ValueMember = "nID"
                cmbPaperVersion.Update()

            End If

            DesignGridForANSIVersionSetting(c1CMSVersionPlanSetup)
            _dt = Nothing
            _dt = clsANSIversion.FetchPaperClinicLevelRecords()

            If Not IsNothing(_dt) And _dt.Rows.Count > 0 Then
                cmbPaperVersion.SelectedValue = Convert.ToInt64(_dt.Rows(0)("nClaimVersion"))
            End If

            m_dtPaperFormSettings = clsANSIversion.FetchPaperPlanLevelRecords()
            SetPaperFormSettings(m_dtPaperFormSettings, False)
            oDatatable = objGenSettings.GetEnumItemsDescriptionPaperFormVersion(gloSettings.PaperFormVersion.CMS1500, True)

            If Not IsNothing(m_dtPaperFormSettings) AndAlso m_dtPaperFormSettings.Rows.Count > 0 Then
                cmbExternalCollectionInsPlan.DataSource = m_dtPaperFormSettings.Copy()
                cmbExternalCollectionInsPlan.DisplayMember = m_dtPaperFormSettings.Columns("sName").ColumnName
                cmbExternalCollectionInsPlan.ValueMember = m_dtPaperFormSettings.Columns("nContactID").ColumnName
                cmbExternalCollectionInsPlan.SelectedIndex = -1
                cmbExternalCollectionInsPlan.Refresh()

            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Sub EnableDisableDefaultInterfaceSettings()
        '' code added for hl7 outbound s 
        If (chkhl7outb.Checked) Then
            chk_allhl7.Enabled = True
            chkhl7PatientReg.Enabled = True
            chkHL7Appointment.Enabled = True
        Else
            chk_allhl7.Enabled = False
            chkhl7PatientReg.Enabled = False
            chkHL7Appointment.Enabled = False
        End If

    End Sub
    ''Private Sub RemoveFormControlsHandler()
    ''    Try
    ''        RemoveHandler cmb_InsuranceType.SelectedIndexChanged, AddressOf commonTextBox_TextChanged
    ''        RemoveHandler txtAppVersion.TextChanged, AddressOf commonTextBox_TextChanged
    ''        RemoveHandler txtThresholdValue.TextChanged, AddressOf commonTextBox_TextChanged
    ''        RemoveHandler txtServerPath.TextChanged, AddressOf commonTextBox_TextChanged
    ''        RemoveHandler txtSendFacility.TextChanged, AddressOf commonTextBox_TextChanged
    ''        RemoveHandler txtRxPswd.TextChanged, AddressOf commonTextBox_TextChanged
    ''        RemoveHandler txtRecFacility.TextChanged, AddressOf commonTextBox_TextChanged
    ''        RemoveHandler txtRecAppl.TextChanged, AddressOf commonTextBox_TextChanged
    ''        RemoveHandler txtPatientCodePrefix.TextChanged, AddressOf commonTextBox_TextChanged
    ''        RemoveHandler txtParticipantID.TextChanged, AddressOf commonTextBox_TextChanged
    ''        RemoveHandler txtHL7FilePath.TextChanged, AddressOf commonTextBox_TextChanged
    ''        RemoveHandler txtEARDirectory.TextChanged, AddressOf commonTextBox_TextChanged
    ''        RemoveHandler txtDBVersion.TextChanged, AddressOf commonTextBox_TextChanged
    ''        RemoveHandler tmStartTime.ValueChanged, AddressOf commonTextBox_TextChanged
    ''        RemoveHandler tmEndTime.ValueChanged, AddressOf commonTextBox_TextChanged
    ''        RemoveHandler PullChartsInterval.ValueChanged, AddressOf commonTextBox_TextChanged
    ''        RemoveHandler numPatientCodeIncrement.ValueChanged, AddressOf commonTextBox_TextChanged
    ''        RemoveHandler numLockOutAttempts.ValueChanged, AddressOf commonTextBox_TextChanged
    ''        RemoveHandler numAppointmentsPerSlot.ValueChanged, AddressOf commonTextBox_TextChanged
    ''        RemoveHandler cmbGender.SelectedIndexChanged, AddressOf commonTextBox_TextChanged
    ''        RemoveHandler rbStaging.CheckedChanged, AddressOf commonTextBox_TextChanged
    ''        RemoveHandler rbProduction.CheckedChanged, AddressOf commonTextBox_TextChanged
    ''        RemoveHandler rbAdvRxStaging.CheckedChanged, AddressOf commonTextBox_TextChanged
    ''        RemoveHandler rbAdvRxProduction.CheckedChanged, AddressOf commonTextBox_TextChanged
    ''        RemoveHandler RB_ChargeAmt.CheckedChanged, AddressOf commonTextBox_TextChanged
    ''        RemoveHandler Rb_AllowedAmt.CheckedChanged, AddressOf commonTextBox_TextChanged
    ''        RemoveHandler optPwdComplexYes.CheckedChanged, AddressOf commonTextBox_TextChanged
    ''        RemoveHandler optPwdComplexNo.CheckedChanged, AddressOf commonTextBox_TextChanged
    ''        RemoveHandler chkSurescript.CheckedChanged, AddressOf commonTextBox_TextChanged
    ''        RemoveHandler chkSendUnassociatedDiagnosis.CheckedChanged, AddressOf commonTextBox_TextChanged
    ''        RemoveHandler ChkRec_Pattern_Weekly_Wednesday.CheckedChanged, AddressOf commonTextBox_TextChanged
    ''        RemoveHandler ChkRec_Pattern_Weekly_Tuesday.CheckedChanged, AddressOf commonTextBox_TextChanged
    ''        RemoveHandler ChkRec_Pattern_Weekly_Thursday.CheckedChanged, AddressOf commonTextBox_TextChanged
    ''        RemoveHandler ChkRec_Pattern_Weekly_Sunday.CheckedChanged, AddressOf commonTextBox_TextChanged
    ''        RemoveHandler ChkRec_Pattern_Weekly_Saturday.CheckedChanged, AddressOf commonTextBox_TextChanged
    ''        RemoveHandler ChkRec_Pattern_Weekly_Monday.CheckedChanged, AddressOf commonTextBox_TextChanged
    ''        RemoveHandler ChkRec_Pattern_Weekly_Friday.CheckedChanged, AddressOf commonTextBox_TextChanged
    ''        RemoveHandler chkAdvanceRx.CheckedChanged, AddressOf commonTextBox_TextChanged
    ''        RemoveHandler cmb_InsuranceType.SelectedIndexChanged, AddressOf commonTextBox_TextChanged
    ''        RemoveHandler btnUp.Click, AddressOf commonTextBox_TextChanged
    ''        RemoveHandler btnDown.Click, AddressOf commonTextBox_TextChanged
    ''        RemoveHandler txtExchangeURL.TextChanged, AddressOf commonTextBox_TextChanged
    ''        RemoveHandler txtExchangeDomain.TextChanged, AddressOf commonTextBox_TextChanged
    ''        RemoveHandler cmbExchangeTimeZone.SelectedIndexChanged, AddressOf commonTextBox_TextChanged
    ''        RemoveHandler numExchangeTimeZoneMin.ValueChanged, AddressOf commonTextBox_TextChanged
    ''        RemoveHandler numExchangeTimeZoneHour.ValueChanged, AddressOf commonTextBox_TextChanged
    ''        RemoveHandler cmbPRMaritalStatus5.SelectedIndexChanged, AddressOf commonTextBox_TextChanged
    ''        RemoveHandler cmbPRMaritalStatus4.SelectedIndexChanged, AddressOf commonTextBox_TextChanged
    ''        RemoveHandler cmbPRMaritalStatus3.SelectedIndexChanged, AddressOf commonTextBox_TextChanged
    ''        RemoveHandler cmbPRMaritalStatus2.SelectedIndexChanged, AddressOf commonTextBox_TextChanged
    ''        RemoveHandler cmbPRMaritalStatus1.SelectedIndexChanged, AddressOf commonTextBox_TextChanged
    ''        RemoveHandler cmbBillingMaritalStatus5.SelectedIndexChanged, AddressOf commonTextBox_TextChanged
    ''        RemoveHandler cmbBillingMaritalStatus4.SelectedIndexChanged, AddressOf commonTextBox_TextChanged
    ''        RemoveHandler cmbBillingMaritalStatus3.SelectedIndexChanged, AddressOf commonTextBox_TextChanged
    ''        RemoveHandler cmbBillingMaritalStatus2.SelectedIndexChanged, AddressOf commonTextBox_TextChanged
    ''        RemoveHandler cmbBillingMaritalStatus1.SelectedIndexChanged, AddressOf commonTextBox_TextChanged
    ''        RemoveHandler txtAlphaIIUserName.TextChanged, AddressOf commonTextBox_TextChanged
    ''        RemoveHandler txtAlphaIIServerName.TextChanged, AddressOf commonTextBox_TextChanged
    ''        RemoveHandler txtAlphaIIPassword.TextChanged, AddressOf commonTextBox_TextChanged
    ''        RemoveHandler txtAlphaIIDatabase.TextChanged, AddressOf commonTextBox_TextChanged
    ''        RemoveHandler rdbNone.CheckedChanged, AddressOf commonTextBox_TextChanged
    ''        RemoveHandler rbCV_YOST.CheckedChanged, AddressOf commonTextBox_TextChanged
    ''        RemoveHandler rbCV_Alpha2.CheckedChanged, AddressOf commonTextBox_TextChanged
    ''        RemoveHandler chkShowMessage.CheckedChanged, AddressOf commonTextBox_TextChanged
    ''        RemoveHandler chkScrubber.CheckedChanged, AddressOf commonTextBox_TextChanged
    ''        RemoveHandler chkReferralCPT.CheckedChanged, AddressOf commonTextBox_TextChanged
    ''        RemoveHandler chkInvalidICD9.CheckedChanged, AddressOf commonTextBox_TextChanged
    ''        RemoveHandler cmbAlphaIIAuthentication.SelectedIndexChanged, AddressOf commonTextBox_TextChanged
    ''        RemoveHandler txtLocality.TextChanged, AddressOf commonTextBox_TextChanged
    ''        RemoveHandler txtCarrierNumber.TextChanged, AddressOf commonTextBox_TextChanged
    ''        RemoveHandler cmbSpeciality.SelectedIndexChanged, AddressOf commonTextBox_TextChanged
    ''        RemoveHandler cmbpatientaccountcode.SelectedIndexChanged, AddressOf commonTextBox_TextChanged
    ''        RemoveHandler cmbFacilityType.SelectedIndexChanged, AddressOf commonTextBox_TextChanged
    ''        RemoveHandler cmb_Feeschedules.SelectedIndexChanged, AddressOf commonTextBox_TextChanged
    ''        RemoveHandler numUpDn_NoOfClaims.ValueChanged, AddressOf commonTextBox_TextChanged
    ''        RemoveHandler numModifiers.ValueChanged, AddressOf commonTextBox_TextChanged
    ''        RemoveHandler rbPrintFacilityYes.CheckedChanged, AddressOfcommonTextBox_TextChanged
    ''        RemoveHandler rbPrintFacilityNo.CheckedChanged, AddressOf commonTextBox_TextChanged
    '' RemoveHandler       chbox_showTotalPayment.CheckedChanged, AddressOf
    ''RemoveHandler        chbox_showPOS.CheckedChanged, AddressOf
    '' RemoveHandler       chbox_showAllwdAmount.CheckedChanged, AddressOf
    ''RemoveHandler        chbox_AddShowLabCol.CheckedChanged, AddressOf
    '' RemoveHandler       C1BillingSettings.TextChanged, AddressOf
    ''RemoveHandler        cmbSpeakerVolume.SelectedIndexChanged, AddressOf
    '' RemoveHandler       cmbRecieveFaxUser.SelectedIndexChanged, AddressOf
    '' RemoveHandler       cmbPendingFaxUser.SelectedIndexChanged, AddressOf
    '' RemoveHandler       cmbFollowUpUser.SelectedIndexChanged, AddressOf
    '' RemoveHandler       cmbFAXCompression.SelectedIndexChanged, AddressOf
    '' RemoveHandler       txtSendMail.TextChanged, AddressOf
    '' RemoveHandler       txtFaxNoPrefix.TextChanged, AddressOf
    '' RemoveHandler       txteFaxUserID.TextChanged, AddressOf
    '' RemoveHandler       txteFaxPassword.TextChanged, AddressOf
    '' RemoveHandler       txteFaxDownloadDir.TextChanged, AddressOf
    '' RemoveHandler       optFAXreceiveYes.CheckedChanged, AddressOf
    '' RemoveHandler       optFAXreceiveNo.CheckedChanged, AddressOf
    '' RemoveHandler       numMaxNoOfRetries.ValueChanged, AddressOf
    '' RemoveHandler       numLoadNoOfFaxes.ValueChanged, AddressOf
    '' RemoveHandler       numFAXRetryInterval.ValueChanged, AddressOf
    '' RemoveHandler       chkUseFaxNoPrefix.CheckedChanged, AddressOf
    '' RemoveHandler       chkSendMail.CheckedChanged, AddressOf
    ''  RemoveHandler      chkInternetFax.CheckedChanged, AddressOf
    '' RemoveHandler       txt_DMSV2RecoveryPath.TextChanged, AddressOf
    '' RemoveHandler       numDMSImageDPI.ValueChanged, AddressOf
    '' RemoveHandler       cmbRadioCategory.SelectedIndexChanged,AddressOf 
    '' RemoveHandler       cmbLabCategory.SelectedIndexChanged, AddressOf
    '' RemoveHandler       cmbFaxCategory.SelectedIndexChanged, AddressOf
    '' RemoveHandler       cmbCategoryDirective.SelectedIndexChanged, AddressOf
    '' RemoveHandler       chkUseFileCompession.CheckedChanged, AddressOf
    ''  RemoveHandler      chkSplitDoc.CheckedChanged, AddressOf
    ''  RemoveHandler      chkRecoverDMSV2Doc.CheckedChanged, AddressOf
    '' RemoveHandler       chk_DeleteDocAfterMigration.CheckedChanged, AddressOf
    ''   RemoveHandler     txtSQLUserID.TextChanged, AddressOf
    ''  RemoveHandler      txtSQLPassword.TextChanged, AddressOf
    ''RemoveHandler        txtPMServerName.TextChanged, AddressOf
    ''  RemoveHandler      txtPMDatabaseName.TextChanged, AddressOf
    '' RemoveHandler       rbtn_MigratetogloEMR50.CheckedChanged, AddressOf
    ''   RemoveHandler     rbtn_MigratetogloEMR40.CheckedChanged, AddressOf
    ''  RemoveHandler      rbtn_MigratetogloEMR28.CheckedChanged, AddressOf
    ''   RemoveHandler     optWindowsAuthentication.CheckedChanged, AddressOf
    ''  RemoveHandler      optSQLAuthentication.CheckedChanged, AddressOf
    ''  RemoveHandler      chk_PMDBSettings.CheckedChanged, AddressOf
    ''  RemoveHandler      C1Provider.TextChanged, AddressOf
    ''  RemoveHandler      txtSignaturetext.TextChanged, AddressOf
    ''  RemoveHandler      txtRptUserName.TextChanged, AddressOf
    ''  RemoveHandler      txtRptPassword.TextChanged, AddressOf
    '' RemoveHandler       txtMCIRReportPath.TextChanged, AddressOf
    ''  RemoveHandler      txtDeclarartion.TextChanged, AddressOf
    ''  RemoveHandler      txtcoSignaturetext.TextChanged, AddressOf
    '' RemoveHandler       txtCCDFilePath.TextChanged, AddressOf
    ''    RemoveHandler    txtAgeLimitPatientStrip.TextChanged, AddressOf
    ''   RemoveHandler     txtAgeLimitforWeeks.TextChanged, AddressOf
    ''   RemoveHandler     Rbtn_showDesc.CheckedChanged, AddressOf
    ''  RemoveHandler      Rbtn_showcode.CheckedChanged, AddressOf
    ''  RemoveHandler      Rbtn_ShowBoth.CheckedChanged, AddressOf
    ''  RemoveHandler      rbShowPercentileOnMouseHoover.CheckedChanged, AddressOf 
    ''   RemoveHandler     rbShowPercentile.CheckedChanged, AddressOf
    ''   RemoveHandler     rbDontShowPercentile.CheckedChanged, AddressOf
    ''   RemoveHandler     NumUpDn_ImRminder.ValueChanged, AddressOf
    ''   RemoveHandler     cmbGeniusPaths.SelectedIndexChanged, AddressOf
    ''   RemoveHandler     chk_GrowthChart.CheckedChanged, AddressOf
    ''   RemoveHandler     chk_AgeFlag.CheckedChanged, AddressOf
    ''   RemoveHandler     chbImunizationReport.CheckedChanged, AddressOf
    ''    RemoveHandler    Chb_UseCodedhistory.CheckedChanged, AddressOf
    ''    RemoveHandler    txtRxHubDisclaimer.TextChanged, AddressOf
    ''    RemoveHandler    rbProblemList.CheckedChanged, AddressOf
    ''    RemoveHandler    rbChiefComplaint.CheckedChanged, AddressOf
    ''    RemoveHandler    chkOtherPatientType.CheckedChanged,AddressOf
    ''   RemoveHandler     rbShow8ICD9.CheckedChanged, AddressOf
    ''    RemoveHandler    rbShow4Modifier.CheckedChanged, AddressOf
    ''     RemoveHandler   rbShow4ICD9.CheckedChanged,AddressOf
    ''    RemoveHandler    rbShow2Modifier.CheckedChanged, AddressOf
    ''    RemoveHandler    rbICD9Driven.CheckedChanged, AddressOf
    ''    RemoveHandler    rbCPTDriven.CheckedChanged, AddressOf
    ''   RemoveHandler     chkSetCPTtoAllICD9.CheckedChanged, AddressOf
    ''     RemoveHandler   chkPrescriptionProvider.CheckedChanged, AddressOf
    ''    RemoveHandler    chkPatientQuestionnaire.CheckedChanged, AddressOf
    ''      RemoveHandler  c1Providers.TextChanged,AddressOf
    ''    Catch ex As Exception
    ''        Me.Cursor = Cursors.Default
    ''        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    ''    End Try
    ''End Sub

    Private Function GetLoginName(ByVal UserID As Int64) As String
        Dim conn As New SqlConnection()
        Dim objCmd As SqlCommand
        Dim LoginName As String = ""
        Dim _strSQL As String = ""

        Try
            conn.ConnectionString = gloPMAdmin.mdlGeneral.GetConnectionString()

            conn.Open()
            _strSQL = "select isnull(sLoginName,'') as sLoginName from User_MST where nUserID = " & UserID
            objCmd = New SqlCommand(_strSQL, conn)
            LoginName = objCmd.ExecuteScalar()

            Return LoginName

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return ""
        Finally
            conn.Close()
        End Try
    End Function
    Private Sub Fill_FaxUsers()
        Dim conn As New SqlConnection()
        Dim objdaUsers As SqlDataAdapter
        Dim dtUsers As DataTable
        Dim dtUsers1 As DataTable
        Dim _strSQL As String = ""

        Try
            conn.ConnectionString = gloPMAdmin.mdlGeneral.GetConnectionString()
            conn.Open()

            _strSQL = "select nUserID , sLoginName from User_MST"
            objdaUsers = New SqlDataAdapter(_strSQL, conn)

            dtUsers = New DataTable
            dtUsers1 = New DataTable

            objdaUsers.Fill(dtUsers)
            objdaUsers.Fill(dtUsers1)

            cmbPendingFaxUser.DataSource = dtUsers
            cmbPendingFaxUser.DisplayMember = "sLoginName"
            cmbPendingFaxUser.ValueMember = "nUserID"

            cmbRecieveFaxUser.DataSource = dtUsers1
            cmbRecieveFaxUser.DisplayMember = "sLoginName"
            cmbRecieveFaxUser.ValueMember = "nUserID"

        Catch sqlex As SqlException
            MessageBox.Show(sqlex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
            conn = Nothing
            objdaUsers = Nothing
            dtUsers = Nothing
        End Try
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Try
            isSaveAndClose = True

            'If _Modified = True Then
            Dim res As DialogResult = MessageBox.Show("Do you want to save settings? ", gstrMessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information)
            If res = Windows.Forms.DialogResult.Yes Then
                Call saveData()
            ElseIf res = Windows.Forms.DialogResult.Cancel Then

            ElseIf res = Windows.Forms.DialogResult.No Then
                Me.Dispose()
            End If
            'Else
            '    Me.Dispose()
            'End If

        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        Try
            isSaveAndClose = True

            tmEndTime.Focus()

            Call saveData()

            If (isSaved) Then
                Me.DialogResult = Windows.Forms.DialogResult.OK
                isSaved = False
            End If

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
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Dim objAudit As New clsAudit
            objAudit.CreateLog(clsAudit.enmActivityType.Modify, " Error occured while modifying the settings.", gstrLoginName, gstrClientMachineName, 0, , clsAudit.enmOutcome.Failure)
            objAudit = Nothing
        End Try
    End Sub

    Private Sub saveDataOld()
        Try
            Dim dtStart As Long
            Dim dtEnd As Long
            dtStart = New TimeSpan(tmStartTime.Value.Hour, tmStartTime.Value.Minute, tmStartTime.Value.Second).Ticks
            dtEnd = New TimeSpan(tmEndTime.Value.Hour, tmEndTime.Value.Minute, tmEndTime.Value.Second).Ticks
            If dtStart >= dtEnd Then
                MessageBox.Show("Clinic Start Time must be less than Clinic Closing Time", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '******By Sandip Deshmukh 26 Oct 2007 5.15PM
                '******the loc was added to set the tab item if causes validation
                tb_Settings.SelectedIndex = 0
                '****** 26 Oct 2007 5.15PM
                tmStartTime.Focus()
                Exit Sub
            End If
            If AppointmentInterval.Value <= 0 Then
                MessageBox.Show("Appointment Interval must be greater than 0 minutes.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '******By Sandip Deshmukh 26 Oct 2007 5.15PM
                '******the loc was added to set the tab item if causes validation
                tb_Settings.SelectedIndex = 0
                '****** 26 Oct 2007 5.15PM
                AppointmentInterval.Focus()
                Exit Sub
            End If
            If AppointmentInterval.Value Mod 5 <> 0 Then
                MessageBox.Show("Appointment Interval must be in multiple of 5 minutes.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '******By Sandip Deshmukh 26 Oct 2007 5.15PM
                '******the loc was added to set the tab item if causes validation
                tb_Settings.SelectedIndex = 0
                '****** 26 Oct 2007 5.15PM
                AppointmentInterval.Focus()
                Exit Sub
            End If
            If (txtSequentialPatientCode.Text.Trim() = "") Then
                MessageBox.Show("Enter sequential patient code.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtSequentialPatientCode.Focus()
                Exit Sub
            End If

            'If isSaved = False Then
            '    ValidateExpClaimData()
            '    Exit Sub
            'End If
            'RemoveHandler Me.C1ExpandedClaimSettings.CellChanged, AddressOf Me.C1ExpandedClaimSettings_CellChanged
            C1ExpandedClaimSettings.FinishEditing()
            If ValidateExpClaimData() = False Then
                Exit Sub
            End If
            'AddHandler Me.C1ExpandedClaimSettings.CellChanged, AddressOf Me.C1ExpandedClaimSettings_CellChanged


            'If ValidateExpClaimData() = False Then
            '    Exit Sub
            'End If


            '' START commnnted by  Sandip Darade 200091214  as setting not used in gloPMAdmin
            'If PullChartsInterval.Value Mod 5 <> 0 Then
            '    MessageBox.Show("Pull Charts Interval must be in multiple of 5 minutes.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    '******By Sandip Deshmukh 26 Oct 2007 5.15PM
            '    '******the loc was added to set the tab item if causes validation
            '    tb_Settings.SelectedIndex = 0
            '    '****** 26 Oct 2007 5.15PM
            '    PullChartsInterval.Focus()
            '    Exit Sub
            'End If

            'If numMaxNoOfRetries.Value <= 0 Or numMaxNoOfRetries.Value >= 500 Then
            '    MessageBox.Show("Maximum No of Retries for FAX must be between 1 to 500.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    '******By Sandip Deshmukh 26 Oct 2007 5.15PM
            '    '******the loc was added to set the tab item if causes validation
            '    tb_Settings.SelectedIndex = 1
            '    '****** 26 Oct 2007 5.15PM
            '    numMaxNoOfRetries.Focus()
            '    Exit Sub
            'End If
            'If numFAXRetryInterval.Value <= 0 Or numFAXRetryInterval.Value >= 500 Then
            '    MessageBox.Show("FAX Retry Interval must be between 1 to 500 minutes.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    '******By Sandip Deshmukh 26 Oct 2007 5.15PM
            '    '******the loc was added to set the tab item if causes validation
            '    tb_Settings.SelectedIndex = 1
            '    '****** 26 Oct 2007 5.15PM
            '    numFAXRetryInterval.Focus()
            '    Exit Sub
            'End If


            ''Sandip Darade 20091214 ''Va
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

            '' END  commnnted by  Sandip Darade 200091214  as setting not used in gloPMAdmin


            '//////////previously the threshold value was 420 mins bydefault. 
            '////////////it is changed to 840 min on 31 Oct'08 friday.
            'code added by sagar on 31 july 2007 for threshold value

            '' START commnnted by  Sandip Darade 200091214  as setting not used in gloPMAdmin

            'If Trim(txtThresholdValue.Text).Length <> 0 Then
            '    If Val(Trim(txtThresholdValue.Text)) = 0 Then
            '        MessageBox.Show("Threshold value should be minimum 1 minute", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '        '******By Sandip Deshmukh 26 Oct 2007 5.15PM
            '        '******the loc was added to set the tab item if causes validation
            '        tb_Settings.SelectedIndex = 0
            '        '****** 26 Oct 2007 5.15PM
            '        txtThresholdValue.Focus()
            '        Exit Sub

            '        'sarika 2nd june 08 -- threshhold value validation
            '    Else
            '        If Val(Trim(txtThresholdValue.Text)) < 1 Then
            '            MessageBox.Show("Threshold value should be minimum 1 minute", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '            '******By Sandip Deshmukh 26 Oct 2007 5.15PM
            '            '******the loc was added to set the tab item if causes validation
            '            tb_Settings.SelectedIndex = 0
            '            '****** 26 Oct 2007 5.15PM
            '            txtThresholdValue.Focus()
            '            Exit Sub
            '        End If
            '        If Val(Trim(txtThresholdValue.Text)) > 840 Then
            '            MessageBox.Show("Threshold value should be maximum 840 minutes", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '            '******By Sandip Deshmukh 26 Oct 2007 5.15PM
            '            '******the loc was added to set the tab item if causes validation
            '            tb_Settings.SelectedIndex = 0
            '            '****** 26 Oct 2007 5.15PM
            '            txtThresholdValue.Text = 840
            '            txtThresholdValue.Focus()
            '            Exit Sub
            '        End If

            '        'sarika 2nd june 08 -- threshhold value validation
            '    End If

            'Else
            '    MessageBox.Show("Default Threshold value 840 minutes will be saved as no value has been entered", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            'End If


            'sarika UseFaxNoPrefix 8th may 08
            'If chkUseFaxNoPrefix.Checked = True Then
            '    If (Trim(txtFaxNoPrefix.Text)) = "" Then
            '        MessageBox.Show("Please enter the Fax Number Prefix.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '        Exit Sub
            '    End If
            'End If
            '---------sarika UseFaxNoPrefix 8th may 08



            '' END  commnnted by  Sandip Darade 200091214  as setting not used in gloPMAdmin


            'sarika PM Credentials validation 20081128
            Dim objSQLSettings As New clsStartUpSettings
            ''Sandip Darade 20090731
            Dim str = ""
            If (gstrAdminFor = "gloPM") Then
                str = "gloEMR"
            Else
                str = "gloPM"
            End If




            If chk_PMDBSettings.Checked = True Then

                If txtPMServerName.Text.Trim = "" Then
                    MessageBox.Show("Please enter  " + str + " Server Name.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If

                If txtPMDatabaseName.Text.Trim = "" Then
                    MessageBox.Show("Please enter  " + str + " Database Name.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If

                If optSQLAuthentication.Checked = True Then
                    If txtSQLUserID.Text.Trim = "" Then
                        MessageBox.Show("Please enter SQL User ID for " + str + ".", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If

                    If txtSQLPassword.Text.Trim = "" Then
                        MessageBox.Show("Please enter SQL Password for " + str + ".", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If

                    If objSQLSettings.IsSQLConnect(txtPMServerName.Text.Trim, txtPMDatabaseName.Text.Trim, txtSQLUserID.Text.Trim, txtSQLPassword.Text.Trim) = False Then
                        If MessageBox.Show("Unable to connect to SQL Server " & txtPMServerName.Text.Trim & " and Database " & txtPMDatabaseName.Text.Trim & vbCrLf & "Please select valid credentials.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information) Then
                            Exit Sub
                        End If
                    End If
                Else
                    If objSQLSettings.IsConnect(txtPMServerName.Text.Trim, txtPMDatabaseName.Text.Trim, False, "", "") = False Then
                        If MessageBox.Show("Unable to connect to SQL Server " & txtPMServerName.Text.Trim & " and Database " & txtPMDatabaseName.Text.Trim & vbCrLf & "Please select valid credentials.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information) Then
                            Exit Sub
                        End If
                    End If
                End If

            End If


            '---
            '' sTART   commnnted by  Sandip Darade 200091214  as setting not used in gloPMAdmin

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
            'If chkAdvanceRx.Checked = True Then

            '    '\\commented on 20090820:
            '    ''If txtEARDirectory.Text.Trim() = "" Then
            '    ''    MessageBox.Show("Please select ePrescribing Report Directory.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    ''    btnBrowseEAR.Focus()
            '    ''    Exit Sub
            '    ''Else
            '    ''    'check whether the directory is valid
            '    ''    If Directory.Exists(txtEARDirectory.Text.Trim) = False Then
            '    ''        MessageBox.Show("The directory is Invalid. Please select valid ePrescribing Report Directory.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    ''        btnBrowseEAR.Focus()
            '    ''        Exit Sub
            '    ''    End If
            '    ''End If

            '    '\\20090819 RxHub participantID & password
            '    If txtParticipantID.Text.Trim = "" Then
            '        MessageBox.Show("Please enter RxHUB Participant ID for " + str + ".", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '        Exit Sub
            '    End If

            '    If txtRxPswd.Text.Trim = "" Then
            '        MessageBox.Show("Please enter RxHUB Password for " + str + ".", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '        Exit Sub
            '    End If
            'End If
            '--
            '' END  commnnted by  Sandip Darade 200091214  as setting not used in gloPMAdmin

            'If (ValidateAlphaIISettings() = False) Then
            '    Exit Sub
            'End If

            If (txtPatientCodePrefix.Text.Trim().Length <> 3 And txtPatientCodePrefix.Text.Trim() <> "") Then
                MessageBox.Show("Patient code prefix must be 3 characters.  ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtPatientCodePrefix.Focus()
                Exit Sub
            End If
            'Try  ''sudhir 20081124 check for age limit days ''should be greater according to months.
            '    If chk_AgeFlag.Checked = True Then
            '        If Val(txtAgeLimitPatientStrip.Text) < (Val(txtAgeLimitforWeeks.Text) * 30.4375) Then
            '            MessageBox.Show("Age Limit for day must be greater than " & CType(CType(Val(txtAgeLimitforWeeks.Text) * 30.4375, Int64), String) & " days", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            '            txtAgeLimitPatientStrip.Focus()
            '            Exit Sub
            '        End If
            '    End If
            'Catch ex As Exception
            'End Try
            SavePaymentSetting()
            Me.Cursor = Cursors.WaitCursor
            Dim objSettings As New clsSettings

            objSettings.WriteOff = cmbWriteOff.SelectedValue
            objSettings.Copay = cmbCopay.SelectedValue
            objSettings.Deductible = cmbDeductible.SelectedValue
            objSettings.CoInsurance = cmbCoInsurance.SelectedValue
            objSettings.Withhold = cmbWithHold.SelectedValue

            objSettings.AppointmentStartTime = tmStartTime.Value.ToShortTimeString()
            objSettings.AppointmentEndTime = tmEndTime.Value.ToShortTimeString()
            objSettings.AppointmentInterval = AppointmentInterval.Value
            ''
            '' START   commnnted by  Sandip Darade 200091214  as setting not used in gloPMAdmin

            'objSettings.PULLCHARTSInterval = PullChartsInterval.Value
            'objSettings.MaxNoOfFAXRetries = numMaxNoOfRetries.Value
            'objSettings.FAXRetryInterval = numFAXRetryInterval.Value
            'objSettings.HPIEnabled = optHPIYes.Checked
            'objSettings.LocationAddressed = optLocationAddressedYes.Checked
            'objSettings.FAXCompression = cmbFAXCompression.Text
            'objSettings.FAXSpeakerVoulme = cmbSpeakerVolume.Text
            'objSettings.FAXReceiveEnabled = optFAXreceiveYes.Checked
            'objSettings.OMRCategoryHistory = cmbOMRCategoryHistory.Text
            'objSettings.OMRCategoryROS = cmbOMRCategoryROS.Text
            'objSettings.OMRCategoryPatientRegistration = cmbOMRCategoryPatientRegistration.Text
            'objSettings.OMRCategoryDirective = cmbCategoryDirective.Text
            'objSettings.Labs = cmbLabCategory.Text
            'objSettings.Radiology = cmbRadioCategory.Text
            ''sarika 31st aug 07
            'objSettings.OMRCategoryFax = cmbFaxCategory.Text

            '' END  commnnted by  Sandip Darade 200091214  as setting not used in gloPMAdmin


            '----------------------------
            'objSettings.blnPwdComplexity = optPwdComplexYes.Checked

            '   objSettings.NoOfAttempts = txtNoOfAttempts.Text.Trim
            objSettings.NoOfAttempts = numLockOutAttempts.Value '' lock out attempts 

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
            '' START   commnnted by  Sandip Darade 200091214  as setting not used in gloPMAdmin

            'sarika 14th june 07
            'objSettings.ClinicDISettings = optClinicDIYes.Checked

            ''For clinic formulary setting
            'objSettings.ClinicFormularySettings = optClinicFormularyYes.Checked

            ' '' Mahesh 20070723 -- Record Level Locking 
            'objSettings.RecordLevelLocking = chkRecordLocking.Checked

            ''code added by sagar on 31 july 2007 for threshold value
            'If Trim(txtThresholdValue.Text) <> "" Then
            '    objSettings.ThresholdValue = Trim(txtThresholdValue.Text)
            'Else
            '    objSettings.ThresholdValue = 840 'previously bydefault it was 420 mins. changed to 840 mins on 31 Oct'08 Friday.
            'End If

            '' END   commnnted by  Sandip Darade 200091214  as setting not used in gloPMAdmin


            'sarika 11th aug 07
            objSettings.HL7SystemPath = txtHL7FilePath.Text
            '-------------------------------------
            '******

            'objSettings.HL7ReceivingFacility = txtRecFacility.Text
            'objSettings.HL7ReceivingApplication = txtRecAppl.Text
            'objSettings.HL7SendingFacility = txtSendFacility.Text
            '******

            'sarika 31st aug 07
            objSettings.DBVersion = txtDBVersion.Text
            objSettings.AppVersion = txtAppVersion.Text
            objSettings.GenerateUnclosedDayStatement = chkUnclosedDayStatement.Checked
            '-------------------------------------
            objSettings.MultipleClearingHouse = chkIsMultipleClearingHouse.Checked
            'sarika 5th sept 07
            '' START   commnnted by  Sandip Darade 200091214  as setting not used in gloPMAdmin

            'objSettings.PendingFaxUserID = cmbPendingFaxUser.SelectedValue
            'objSettings.RecieveFaxUserID = cmbRecieveFaxUser.SelectedValue
            '' END    commnnted by  Sandip Darade 200091214  as setting not used in gloPMAdmin

            '-----------------
            '''''''''code added by Anil on 20071119
            objSettings.AutoGeneratePatientCode = chkAutogenerateCode.Checked
            ''Added by Mayuri:20101006-To add functonality of save as copy patient
            objSettings.AllowEditablePatientCode = chkallowEditPatientCode.Checked

            If (_SequentialPatientCode.ToString() <> txtSequentialPatientCode.Text.ToString.Trim()) Then
                Dim objAudit1 As New clsAudit
                Dim ogloSettings As gloSettings.GeneralSettings = New gloSettings.GeneralSettings(mdlGeneral.GetConnectionString)
                ogloSettings.AddSetting("SequentialPatientCode", txtSequentialPatientCode.Text.ToString.Trim().Replace(" ", ""), _ClinicID, 0, gloSettings.SettingFlag.Clinic)
                objAudit1.CreateLog(clsAudit.enmActivityType.Other, "Sequential patient code saved with value =" & txtSequentialPatientCode.Text.ToString(), gstrLoginName, gstrClientMachineName)
            End If

            '''''''''''''''''''''''''''''''''''''''''''''''''''''
            '''''''''code added by pradeep on 20100629
            objSettings.UseSitePrefix = ChkUseSitePrefix.Checked
            '''''''''''''''''''''''''''''''''''''''''''''''''''''
            'code added by suraj on 20080725'''*******
            'objSettings.RxDeclaration = txtDeclarartion.Text

            ''RxHubDisclaimer
            'objSettings.RxHUBDisclaimer = txtRxHubDisclaimer.Text

            '------>Suraj 20080801
            '' START   commnnted by  Sandip Darade 200091214  as setting not used in gloPMAdmin

            'objSettings.GenerateMic = chbImunizationReport.Checked

            ''code added by sarika on 18th jan 08 for gloReporting authentication
            'objSettings.ReportingUserName = txtRptUserName.Text.Trim
            'Encrypt the Password and then save it
            Dim objEncrypt As New clsEncryption

            'objSettings.ReportingPassword = objEncrypt.EncryptToBase64String(txtRptPassword.Text.Trim, constEncryptDecryptKey_Services)
            '---

            'sarika UseFaxNoPrefix 12th april 08
            'objSettings.UseFaxNoPrefix = chkUseFaxNoPrefix.Checked
            'objSettings.FaxNoPrefix = txtFaxNoPrefix.Text.Trim()
            '--sarika UseFaxNoPrefix 12th april 08



            'sarika internet fax
            '' objSettings.InternetFax = chkInternetFax.Checked''  commented by Sandip Darade 20091212 
            'eFax Login Key
            ''objSettings.eFaxUserID = txteFaxUserID.Text.Trim()''  commented by Sandip Darade 20091212 
            ''objSettings.eFaxUserPassword = objEncrypt.EncryptToBase64String(txteFaxPassword.Text.Trim(), constEncryptDecryptKey_Services)''  commented by Sandip Darade 20091212 

            'sarika internet fax
            ''Start Sandip Darade 20091127
            ''
            'code added by supriya 11/7/2008
            'Surescript Server settings
            'If chkSurescript.Checked Then
            '    objSettings.IsSurescriptEnabled = True
            '    If rbStaging.Checked = True Then
            '        objSettings.IsStagingServer = True
            '    Else
            '        objSettings.IsStagingServer = False
            '    End If
            'Else
            '    objSettings.IsSurescriptEnabled = False
            'End If
            'gblnIsSurescriptEnabled = objSettings.IsSurescriptEnabled
            'gblnIsStagingServer = objSettings.IsStagingServer
            ''code added by supriya 11/7/2008
            ''Surescript Server settings


            'If chkAdvanceRx.Checked = True Then
            '    objSettings.IsAdvanceRxEnabled = True
            '    If rbAdvRxStaging.Checked = True Then
            '        objSettings.IsAdvanceRxStagingServer = True
            '    Else
            '        objSettings.IsAdvanceRxStagingServer = False
            '    End If
            '    objSettings.EARDirectory = "" '\\commented on 20090820: txtEARDirectory.Text.Trim

            '    '20090819 rxhub participantid & rxpassword settings

            '    objSettings.ParticipantID = txtParticipantID.Text.Trim
            '    objSettings.RxPassword = objEncrypt.EncryptToBase64String(txtRxPswd.Text.Trim(), constEncryptDecryptKey_Services)

            'Else
            '    objSettings.IsAdvanceRxEnabled = False
            '    objSettings.EARDirectory = ""

            '    'objSettings.ParticipantID = ""
            'End If


            'sarika SiteID Setting 20090607
            'objSettings.SiteID = txtSiteID.Text.Trim()
            '----

            ''END Sandip Darade 20091127

            'sarika internet fax'
            'objSettings.eFaxDirDownload = txteFaxDownloadDir.Text.Trim()
            'sarika internet fax'

            '' SagarK 20080802
            '' For MCIR Report Path
            'If chbImunizationReport.Checked = True Then
            '    If txtMCIRReportPath.Text.Trim.Length > 0 Then
            '        If Directory.Exists(txtMCIRReportPath.Text.Trim()) = True Then
            '            objSettings.MCIRReportPath = txtMCIRReportPath.Text.Trim()
            '        Else
            '            MessageBox.Show("MCIR Report Path is not valid. Please enter the valid Path.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '            Exit Sub
            '        End If
            '    End If
            'End If


            'If txtCCDFilePath.Text.Trim.Length > 0 Then
            '    If Directory.Exists(txtCCDFilePath.Text.Trim()) = True Then
            '        objSettings.CCDfilePath = txtCCDFilePath.Text.Trim
            '    Else
            '        MessageBox.Show("CCD file path is not valid. Please enter the valid Path.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '        Exit Sub
            '    End If
            'End If



            '' Mahesh 20080809 Advanced Growth Chart
            'objSettings.ShowAdvancedGrowthChart = chk_GrowthChart.Checked
            ' ''

            ' '' SUDHIR 20090721 '' GROWTH CHART PERCENTILE ''
            'If rbShowPercentile.Checked Then
            '    objSettings.GrowthChartPercentile = enumGrowthChartPercentile.ShowPercentile
            'ElseIf rbShowPercentileOnMouseHoover.Checked Then
            '    objSettings.GrowthChartPercentile = enumGrowthChartPercentile.ShowPercentileOnMouseHoover
            'Else
            '    objSettings.GrowthChartPercentile = enumGrowthChartPercentile.DontShowPercentile
            'End If
            '' END SUDHIR ''
            '' END    commnnted by  Sandip Darade 200091214  as setting not used in gloPMAdmin


            ''by SUDHIR 20081111 
            'Show Age in Days
            'objSettings.ShowAgeInDays = chk_AgeFlag.Checked
            ''Age Limit
            'objSettings.AgeLimit = CLng(txtAgeLimitPatientStrip.Text)
            ' ''sudhir 20081124
            ''Age limit for weeks in months
            'objSettings.AgeLimitForWeeks = CLng(txtAgeLimitforWeeks.Text)
            ' ''

            'objSettings.SendUnassociatedDiagnosis = chkSendUnassociatedDiagnosis.Checked


            ' '' SUDHIR 20090821 '' 
            'objSettings.SetCPTtoAllICD9 = chkSetCPTtoAllICD9.Checked
            ' '' END SUDHIR ''

            'sarika 
            'DMS 20080908 -- for Loading no of recieved faxes in DMS
            'objSettings.LoadNoOfFaxes = numLoadNoOfFaxes.Value
            '--------------

            objSettings.SignatureText = Convert.ToString(txtSignaturetext.Text.Trim())
            objSettings.CoSignatureText = Convert.ToString(txtcoSignaturetext.Text.Trim())
            ''Sandip Darade 20090731
            If (gstrAdminFor = "gloPM") Then

                SavegloEMRDatabaseSettings()
                'sarika PM DB Credentials
                'If chk_PMDBSettings.Checked = False Then
                '    objSettings.PMAddPatient = 0
                'Else
                '    objSettings.PMAddPatient = 1
                'End If
            Else
                objSettings.PMAddPatient = chk_PMDBSettings.Checked
                ''Sandip Darade  if not selected to add the setting
                If (objSettings.PMAddPatient = True) Then
                    objSettings.PMServerName = txtPMServerName.Text.Trim
                    objSettings.PMDatabaseName = txtPMDatabaseName.Text.Trim
                    objSettings.PMUserID = txtSQLUserID.Text.Trim
                    objSettings.PMSQLPwd = txtSQLPassword.Text.Trim
                End If
            End If
            '' START   commnnted by  Sandip Darade 200091214  as setting not used in gloPMAdmin

            'If rbChiefComplaint.Checked = True Then
            '    objSettings.EMChiefComplaintType = "ChiefComplaint"
            'ElseIf rbProblemList.Checked = True Then
            '    objSettings.EMChiefComplaintType = "ProblemList"
            'End If

            '' END    commnnted by  Sandip Darade 200091214  as setting not used in gloPMAdmin

            ' ''Sandip Darade 
            'If (objSettings.PMAddPatient = True) Then
            '    If optSQLAuthentication.Checked = False Then
            '        objSettings.PMSQLAuthentication = 0
            objSettings.PMConnectionString = gloPMAdmin.mdlGeneral.GetConnectionString(txtPMServerName.Text.Trim, txtPMDatabaseName.Text.Trim, False, "", "")
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
            '' START   commnnted by  Sandip Darade 200091214  as setting not used in gloPMAdmin

            'If chkSendMail.Checked = True Then
            '    objSettings.IsSendEMail = True
            '    objSettings.SendEmailAddress = txtSendMail.Text.Trim
            'Else
            '    objSettings.IsSendEMail = False
            '    objSettings.SendEmailAddress = ""
            'End If
            '--
            '' END    commnnted by  Sandip Darade 200091214  as setting not used in gloPMAdmin

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
                    _sqlPMConnection.Close()
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
            ''Sandip Darade 
            If (objSettings.PMAddPatient = True) Then
                If optSQLAuthentication.Checked = False Then
                    objSettings.PMSQLAuthentication = 0
                    objSettings.PMConnectionString = gloPMAdmin.mdlGeneral.GetConnectionString(txtPMServerName.Text.Trim, txtPMDatabaseName.Text.Trim, False, "", "")
                    'objSettings.PMUserID = ""
                    'objSettings.PMSQLPwd = ""
                Else
                    objSettings.PMSQLAuthentication = 1
                    objSettings.PMConnectionString = gloPMAdmin.mdlGeneral.GetConnectionString(txtPMServerName.Text.Trim, txtPMDatabaseName.Text.Trim, txtSQLUserID.Text.Trim, txtSQLPassword.Text.Trim)
                    'Dim objEncryptDecrypt As New clsEncryption
                    '  objSettings.PMSQLPwd = objEncryptDecrypt.EncryptToBase64String(txtSQLPassword.Text.Trim, constEncryptDecryptKey)
                    ' objEncryptDecrypt = Nothing
                End If
            Else
                objSettings.PMConnectionString = ""

            End If

            '' START   commnnted by  Sandip Darade 200091214  as setting not used in gloPMAdmin

            'objSettings.DMSImageDIP = CType(numDMSImageDPI.Value, Int32)
            'objSettings.UseFileCompression = chkUseFileCompession.Checked

            ''--

            ''\\Suraj 20090123
            'objSettings.SplitDocument = chkSplitDoc.Checked
            ''\\suraj 20090128
            'objSettings.RecoveryDMSV2Doc = chkRecoverDMSV2Doc.Checked
            ''objSettings.RecoveryDMSV2Path = txt_DMSV2RecoveryPath.Text
            'If chkRecoverDMSV2Doc.Checked = True Then
            '    If txt_DMSV2RecoveryPath.Text.Trim.Length > 0 Then
            '        If Directory.Exists(txt_DMSV2RecoveryPath.Text.Trim()) = True Then
            '            objSettings.RecoveryDMSV2Path = txt_DMSV2RecoveryPath.Text.Trim
            '        Else
            '            MessageBox.Show("DMS V2 Physical Document Path is not valid. Please enter the valid Path.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '            txt_DMSV2RecoveryPath.Focus()
            '            Exit Sub
            '        End If
            '    Else
            '        MessageBox.Show("DMS V2 Physical Document Path is not entered. Please enter the valid Path.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '        txt_DMSV2RecoveryPath.Focus()
            '        Exit Sub
            '    End If
            'End If
            ' ''
            ''\\suraj 20090128  Delete DMS Doc after Migration
            'objSettings.DeleteDMSDocAfterMigration = chk_DeleteDocAfterMigration.Checked

            ''Sandip Darade  20090210 Use Coded History 
            'objSettings.Usecodedhistory = Chb_UseCodedhistory.Checked
            ''Sandip Darade  20090328 Show Coded History code,Description or both 
            ''If (Chb_UseCodedhistory.Checked = True) Then
            'If (Rbtn_showcode.Checked = True) Then
            '    objSettings.ShowCodedHistory = "Code"

            'ElseIf (Rbtn_showDesc.Checked = True) Then
            '    objSettings.ShowCodedHistory = "Description"
            'Else

            '    objSettings.ShowCodedHistory = "Both"
            'End If

            '' START   commnnted by  Sandip Darade 200091214  as setting not used in gloPMAdmin

            'shubhangi 20090306'
            '' objSettings.OtherPatientType = chkOtherPatientType.Checked

            'End Shubhangi'
            ''IMReminderDays Settings
            'objSettings.IM_ReminderDays = NumUpDn_ImRminder.Value

            'Sandip Darade  20090622
            'Default patient gender setting
            objSettings.DefaultPatientGender = cmbGender.Text

            ''Sandip Darade 20090709
            ''Add billing setting 
            '' Save_BillingSettings()

            SaveBillingSetting()

            ''Add Mid Level billing setting 
            Call SaveMidLevelBillingSetting()

            '' Save Expanded Claim Settings
            Call SaveExpandedClaimSettings()
            ''

            ''Add Other billing setting 
            If cmbSlfPayAllwdAmnts.SelectedIndex > 0 Then
                objSettings.DefaultSelfPayAllowed = cmbSlfPayAllwdAmnts.SelectedValue
            Else
                objSettings.DefaultSelfPayAllowed = 0

            End If

            If cmb_SlfPayDefaultFeeSchedule.SelectedIndex > 0 Then
                objSettings.DefaultSelfPayFeeSchedule = cmb_SlfPayDefaultFeeSchedule.SelectedValue
            Else
                objSettings.DefaultSelfPayFeeSchedule = 0

            End If

            If cmb_Feeschedules.SelectedIndex <> -1 Then
                objSettings.ClinicFeeSchedule = Convert.ToString(cmb_Feeschedules.SelectedValue)
            Else
                objSettings.ClinicFeeSchedule = 0

            End If
            objSettings.NoOfClaimPerBatch = numUpDn_NoOfClaims.Value.ToString()

            'Added by mitesh
            'objSettings.NoOfServiceLine = numup_dn_ChargesPerClaim.Value.ToString()
            'objSettings.NoOfDiagnosisPerClaim = numup_dn_DiagnosisPerClaim.Value.ToString()


            '--------
            'Commented by Rahul Patel on 13/09/2010
            'For hiding the no of modifiers field
            'objSettings.NoOfModifiers = numModifiers.Value.ToString()

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
            ''Save alphaII settings
            SaveAlphaIIDatabaseSettings()
            ''Save Mariatal Status Settings
            SaveMaritalStatusSettings()
            ''Save Exchange server settings
            SavePaperBillingSetting()
            ''Save Paper Billing Setting
            AddExServerSetting()
            ''Add weekdayssetting
            AddOtherSettings()

            'Save "Complete Payments before Daily Close" 
            SaveInsuranceCorrection_Refund()
            '-------

            '' Setting Added By Mayuri - 20090827
            '' ICD9-CPT Driven Settings
            'objSettings.ICD9Driven = rbICD9Driven.Checked
            'objSettings.Show8ICD9 = rbShow8ICD9.Checked
            'objSettings.Show4Modifier = rbShow4Modifier.Checked

            '' Precription provider
            ''   objSettings.PrescriptionProviderAssociation = chkPrescriptionProvider.Checked
            '' Patient Questionnaire
            objSettings.PatientQuestionnaire = chkPatientQuestionnaire.Checked
            '' END Setting Added By Mayuri - 20090827
            '''''''''''Genuis Path settings 

            'If IsNothing(objSettings.GeniusCode) = False Then
            '    'CType(cmbGeniusPaths.SelectedItem, ClsGeniusPath).GeniusCode = regKey.GetValue("GeniusCode")
            '    'cmbGeniusPaths.Text = regKey.GetValue("GeniusCode")
            '    gstrGeniusCode = objSettings.GeniusCode
            'End If
            'If Not IsNothing(cmbGeniusPaths.SelectedItem) Then
            '    'objSettings.GeniusCode = CType(cmbGeniusPaths.SelectedItem, ClsGeniusPath).GeniusCode
            '    'objSettings.GeniusPath = CType(cmbGeniusPaths.SelectedItem, ClsGeniusPath).GeniusPath
            '    objSettings.GeniusCode = cmbGeniusPaths.SelectedValue
            '    objSettings.GeniusPath = CType(cmbGeniusPaths.SelectedItem, ClsGeniusPath).GeniusPath
            'Else
            '    'MessageBox.Show("Please enter Genius Path")
            '    'Exit Sub
            'End If
            '''''''''''Genuis Path settings 
            If objSettings.UpdateSettings() = False Then
                Me.Cursor = Cursors.Default
                MessageBox.Show("Unable to update settings", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                tmStartTime.Focus()
                objSettings = Nothing
                Exit Sub
                '******
                'Enter the follow up user in Settings table 
            Else
                Dim oDB As New gloStream.gloDataBase.gloDataBase

                Dim strqry As String = " Delete from settings where sSettingsName='Followup User'"
                oDB.Connect(gstrConnectionString)
                Dim succ As Boolean = oDB.ExecuteNonSQLQuery(strqry)
                'If (oDB.ExecuteNonSQLQuery(strqry) = True) Then
                For i As Integer = 1 To colUId.Count
                    strqry = " Select max(nSettingsID) as maxid from settings "

                    Dim maxID As Integer = oDB.ExecuteQueryScaler(strqry)

                    strqry = " insert into Settings(nSettingsID, sSettingsName, sSettingsValue,nClinicID,nUserID,nUserClinicFlag)values('" + (maxID + 1).ToString + "','Followup User','" + colUId.Item(i).ToString + "'," & _ClinicID & ",0,1) "

                    If (oDB.ExecuteQueryNonQuery(strqry) = False) Then
                        MessageBox.Show("Unable to update settings", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        tmStartTime.Focus()
                        objSettings = Nothing
                        Exit Sub
                    End If
                Next
                'End If

                '******


                '--------------updating SurgicalAlertUser to settings table
                Dim strSurgicalAlertqry As String = " Delete from settings where sSettingsName='Surgical User'"
                oDB.Connect(gstrConnectionString)
                Dim success As Boolean = oDB.ExecuteNonSQLQuery(strSurgicalAlertqry)
                'If (oDB.ExecuteNonSQLQuery(strqry) = True) Then
                For i As Integer = 1 To col_SurgicalUId.Count
                    strqry = " Select max(nSettingsID) as maxid from settings "

                    Dim maxID As Integer = oDB.ExecuteQueryScaler(strqry)

                    strqry = " insert into Settings(nSettingsID, sSettingsName, sSettingsValue,nClinicID,nUserID,nUserClinicFlag)values('" + (maxID + 1).ToString + "','Surgical User','" + col_SurgicalUId.Item(i).ToString + "'," & _ClinicID & ",0,1) "

                    If (oDB.ExecuteQueryNonQuery(strqry) = False) Then
                        MessageBox.Show("Unable to update settings", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        tmStartTime.Focus()
                        objSettings = Nothing
                        Exit Sub
                    End If
                Next
                '--------------updating SurgicalAlertUser to settings table

            End If

            Dim objAudit As New clsAudit

            If objSettings.blnPwdComplexity = True Then
                If m_strSQL <> "" Then
                    If objSettings.SetPwdComplexitySettings(m_strSQL) = False Then
                        Me.Cursor = Cursors.Default
                        MessageBox.Show("Unable to update the password complexity settings", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        tmStartTime.Focus()
                        objSettings = Nothing
                        Exit Sub
                    Else
                        'sarika  21 feb

                        objAudit.CreateLog(clsAudit.enmActivityType.Other, gstrLoginName & " has reset the Password Settings.", gstrLoginName, gstrClientMachineName)
                    End If
                End If

            Else
                If sqlstrsettings <> "" Then
                    If objSettings.SetPwdComplexitySettings(sqlstrsettings) = False Then
                        Me.Cursor = Cursors.Default
                        MessageBox.Show("Unable to update the password complexity settings", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        tmStartTime.Focus()
                        objSettings = Nothing
                        Exit Sub
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



            'sarika  21 feb
            '  Dim objAudit As New clsAudit
            objAudit.CreateLog(clsAudit.enmActivityType.Other, gstrLoginName & " has reset the Settings.", gstrLoginName, gstrClientMachineName)
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

            objSettings = Nothing
            '' START   commnnted by  Sandip Darade 200091214  as setting not used in gloPMAdmin

            'If CheckFaxModifications() = True Then
            '    MessageBox.Show("To apply the new FAX Settings to the FAX Application, please re-run the FAX Application", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            'End If
            'If CheckEFaxModifications() = True Then
            '    MessageBox.Show("To apply the new eFAX Settings, please re-start the Internet FAX service", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            'End If

            '' END    commnnted by  Sandip Darade 200091214  as setting not used in gloPMAdmin



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
            SaveProviderSetting()

            ''Added By MaheshB 20091006
            If (cmbpatientaccountcode.SelectedIndex >= 0) Then
                AddSettingToDB("PatientAccountCode", "Claim Number", _ClinicID, 0, SettingFlag.Clinic)
            End If

            ''Added By MaheshB 20091127

            AddSettingToDB("IncludeFacilitieswithPOS11onClaim", rbPrintFacilityYes.Checked.ToString(), _ClinicID, 0, SettingFlag.Clinic)

            'Added by vijay for insert/add  Ub04 billing setting
            AddSettingToDB("UB04_EnableBilling", RbUbBilling_yes.Checked.ToString(), _ClinicID, 0, SettingFlag.Clinic)
            If (RbUbBilling_yes.Checked) Then
                AddSettingToDB("UB04_RevenueCode", CmbRevenueCode.Text.ToString(), _ClinicID, 0, SettingFlag.Clinic)
                AddSettingToDB("UB04_TypeOfBill", TxtTypeBill.Text.ToString().Trim(), _ClinicID, 0, SettingFlag.Clinic)
                AddSettingToDB("UB04_AdmisionType", TxtAdmissionType.Text.ToString().Trim(), _ClinicID, 0, SettingFlag.Clinic)
                AddSettingToDB("UB04_AdmisionSource", TxtAdmisionSource.Text.ToString().Trim(), _ClinicID, 0, SettingFlag.Clinic)
                AddSettingToDB("UB04_DischargeStatus", txtDischargeStatus.Text.ToString().Trim(), _ClinicID, 0, SettingFlag.Clinic)

            Else
                AddSettingToDB("UB04_RevenueCode", "", _ClinicID, 0, SettingFlag.Clinic)
                AddSettingToDB("UB04_TypeOfBill", "", _ClinicID, 0, SettingFlag.Clinic)
                AddSettingToDB("UB04_AdmisionType", "", _ClinicID, 0, SettingFlag.Clinic)
                AddSettingToDB("UB04_AdmisionSource", "", _ClinicID, 0, SettingFlag.Clinic)
                AddSettingToDB("UB04_DischargeStatus", "", _ClinicID, 0, SettingFlag.Clinic)


            End If


            'Statement minimum Payment
            AddSettingToDB("Statement Minimum Payment", txtStatementMinPay.Text.Trim().Replace("$", ""), _ClinicID, 0, SettingFlag.Clinic)


            'end code 


            Dim oDBClearingHouse As New gloDatabaseLayer.DBLayer(gstrConnectionString)
            Dim strClearingHouseqry As String = "Select ISNULL(Count(nClearingHouseID),0) from BL_ClearingHouse_MST"
            oDBClearingHouse.Connect(False)
            Dim _ClearingHouseCount As Integer = oDBClearingHouse.ExecuteScalar_Query(strClearingHouseqry)


            If _ClearingHouseCount > 1 And chkIsMultipleClearingHouse.Checked = False Then
                MessageBox.Show("Multiple Clearinghouses exist.  Please delete the unused clearinghouses. ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            Else

                If chkIsMultipleClearingHouse.Checked Then
                    AddSettingToDB("ISMULTIPLECLEARINGHOUSE", "1", _ClinicID, 0, SettingFlag.Clinic)
                Else
                    AddSettingToDB("ISMULTIPLECLEARINGHOUSE", "0", _ClinicID, 0, SettingFlag.Clinic)
                End If
                oDBClearingHouse.Disconnect()
                oDBClearingHouse.Dispose()
            End If

            ''Added By MaheshN 20100407
            If (chkcopyclaimstoserver.Checked) Then
                AddSettingToDB("COPY_EDI_FILES", "1", _ClinicID, 0, SettingFlag.Clinic)
            Else
                AddSettingToDB("COPY_EDI_FILES", "0", _ClinicID, 0, SettingFlag.Clinic)
            End If


            'Added by Roopali 14 July 2010 for SSRS reports
            If (txtReportServerName.Text.ToString() <> "" And txtReportFolderName.Text.ToString() <> "") Then
                AddSettingToDB("ReportServer", txtReportServerName.Text.ToString(), _ClinicID, gnLoginID, SettingFlag.Clinic)
                AddSettingToDB("ReportFolder", txtReportFolderName.Text.ToString(), _ClinicID, gnLoginID, SettingFlag.Clinic)
            ElseIf (txtReportServerName.Text.ToString() = "" And txtReportFolderName.Text.ToString() = "") Then
                AddSettingToDB("ReportServer", txtReportServerName.Text.ToString(), _ClinicID, gnLoginID, SettingFlag.Clinic)
                AddSettingToDB("ReportFolder", txtReportFolderName.Text.ToString(), _ClinicID, gnLoginID, SettingFlag.Clinic)
            ElseIf (txtReportServerName.Text.ToString() = "" Or txtReportFolderName.Text.ToString() = "") Then
                tb_Settings.SelectedTab = tbpg_ReportServerSettings
                _blnValidate = True
                If (txtReportServerName.Text.ToString() = "") Then
                    MessageBox.Show("Enter Report Server Name.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    txtReportServerName.Focus()
                End If
                If (txtReportFolderName.Text.ToString() = "") Then
                    MessageBox.Show("Enter Report Folder Name.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    txtReportFolderName.Focus()
                End If
                Exit Sub
            End If
            AddSettingToDB("ReportVirtualDirectory", txtReportVirtualDir.Text.ToString(), _ClinicID, gnLoginID, SettingFlag.Clinic)

            'Changes Made by Subashish date 03-01-2011---------------------Start------ Change No SB0001------------
            'For adding/updating  the Patient Account Setting in setting table
            SavePatientAccountSetting()
            'Changes Made by Subashish date 03-01-2011---------------------Close------------------


            SaveBillingIDQualifier()

            If rbZero.Checked Then

                If NDCSettings <> "Zero" Then

                    AddSettingToDB("NDCUnitPricing", "Zero", _ClinicID, 0, SettingFlag.Clinic)
                    Dim objSetting As New clsSettings
                    objSetting.UpdateNDCUnitPrice("Zero")

                End If

            ElseIf rbCalculated.Checked Then

                If NDCSettings <> "Calculated" Then

                    AddSettingToDB("NDCUnitPricing", "Calculated", _ClinicID, 0, SettingFlag.Clinic)
                    Dim objSetting As New clsSettings
                    objSetting.UpdateNDCUnitPrice("Calculated")

                End If

            End If

            ''Save ANSI Versions settings
            SaveANSIVersions()
            If chkPlan5010.Checked = True Then
                AddSettingToDB("Enable Insurance Plan 5010", "True", _ClinicID, 0, SettingFlag.Clinic)
            ElseIf chkPlan5010.Checked = False Then
                AddSettingToDB("Enable Insurance Plan 5010", "False", _ClinicID, 0, SettingFlag.Clinic)
            End If

            ''Save Paper Form Versions settings
            SavePaperFormVersions()


            Me.Cursor = Cursors.Default
            Me.Dispose()
            isSaved = True
        Catch objErr As Exception
            Me.Cursor = Cursors.Default
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            'sarika  21 feb
            Dim objAudit As New clsAudit
            objAudit.CreateLog(clsAudit.enmActivityType.Modify, " Error occured while modifying the settings.", gstrLoginName, gstrClientMachineName, 0, , clsAudit.enmOutcome.Failure)
            objAudit = Nothing
            '-------------
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Function ValidateFOLLOWUPSettings() As Boolean
        If (txtInsClmStartFilingDays.Text.Trim() <> String.Empty) Then
            If (txtInsClmStartFilingDays.Text > 200) Then
                If (MessageBox.Show("Are you sure you want the days setting to be that large: " & txtInsClmStartFilingDays.Text.Trim() & "?", gstrMessageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) = DialogResult.Cancel) Then
                    Return True
                End If
            End If
        End If
        If (txtInsClmRebillFilingDays.Text.Trim() <> String.Empty) Then
            If (txtInsClmRebillFilingDays.Text > 200) Then
                If (MessageBox.Show("Are you sure you want the days setting to be that large: " & txtInsClmRebillFilingDays.Text.Trim() & "?", gstrMessageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) = DialogResult.Cancel) Then
                    Return True
                End If
            End If
        End If
        If (txtPatAccNoOfDaysAfterStmnt.Text.Trim() <> String.Empty) Then
            If (txtPatAccNoOfDaysAfterStmnt.Text > 200) Then
                If (MessageBox.Show("Are you sure you want the days setting to be that large: " & txtPatAccNoOfDaysAfterStmnt.Text.Trim() & "?", gstrMessageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) = DialogResult.Cancel) Then
                    Return True
                End If
            End If
        End If
        If (txtPmntPlanDefFUActionDays.Text.Trim() <> String.Empty) Then
            If (txtPmntPlanDefFUActionDays.Text > 200) Then
                If (MessageBox.Show("Are you sure you want the days setting to be that large: " & txtPmntPlanDefFUActionDays.Text.Trim() & "?", gstrMessageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) = DialogResult.Cancel) Then
                    Return True
                End If
            End If
        End If


        If (rbExternalCollectionYes.Checked = True) Then
            'If (cmbExternalCollectionInsPlan.Text.Trim() = "") Then
            '    tb_Settings.SelectedTab = tbpg_FollowUp
            '    MessageBox.Show("You have enabled External Collection feature." + Environment.NewLine + "Configure External Collection Insurance Plan settings.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            '    Return True
            'End If

            If (cmbExternalCollectionFUAction.Text.Trim() = "") Then
                tb_Settings.SelectedTab = tbpg_FollowUp
                MessageBox.Show("You have enabled External Collection feature." + Environment.NewLine + "Configure External Collection Follow-UP Action settings.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return True
            End If

            'If (Convert.ToString(cmbExtCollectionStartFUAction.Text).Trim() = "") Then
            '    tb_Settings.SelectedTab = tbpg_FollowUp
            '    MessageBox.Show("You have enabled External Collection feature." + Environment.NewLine + "Configure External Collection start Follow-up action settings.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            '    Return True
            'End If
        End If

    End Function

    Private Function ValidateNoProviderSettings() As Boolean
        Dim oDB As gloDatabaseLayer.DBLayer = New gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString)

        Try
            oDB.Connect(False)
            Dim Value As Object = oDB.ExecuteScalar_Query("SELECT CASE WHEN  MAX(ISNULL(nProviderCompanyIndex,0)) >=  MAX(ISNULL(nOtherIDProviderCompanyIndex,0))  THEN ISNULL(MAX(nProviderCompanyIndex),0) ELSE  ISNULL(MAX(nOtherIDProviderCompanyIndex),0) END  FROM dbo.BL_AlternateID_Settings")
            Dim nInsuranceProviderCount As Int16 = Convert.ToInt16(Value)
            Dim oclsSettings As clsSettings = New clsSettings()
            Dim nAdminProviderCount As Int16 = NUpDownProviderCompnay.Value
            If (nInsuranceProviderCount > nAdminProviderCount) Then
                MessageBox.Show("You may not change the number of Provider Companies to " + Convert.ToString(nAdminProviderCount) + "." + Environment.NewLine +
                "There are insurances plans referencing Provider Company " + Convert.ToString(nInsuranceProviderCount) + ".", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return True
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            oDB.Disconnect()
            If oDB IsNot Nothing Then
                oDB.Dispose()
            End If
        End Try
        Return False
    End Function
    Private Sub saveData()
        Try
            Dim nUpDownProviderCompnayCount As Int16 = 1
            If (ValidateFOLLOWUPSettings()) Then
                Exit Sub
            End If
            If (ValidateNoProviderSettings()) Then
                Exit Sub
            End If
            Dim dtStart As Long
            Dim dtEnd As Long
            dtStart = New TimeSpan(tmStartTime.Value.Hour, tmStartTime.Value.Minute, tmStartTime.Value.Second).Ticks
            dtEnd = New TimeSpan(tmEndTime.Value.Hour, tmEndTime.Value.Minute, tmEndTime.Value.Second).Ticks
            If dtStart >= dtEnd Then
                MessageBox.Show("Clinic Start Time must be less than Clinic Closing Time", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                tb_Settings.SelectedIndex = 0
                tmStartTime.Focus()
                Exit Sub
            End If
            If AppointmentInterval.Value <= 0 Then
                MessageBox.Show("Appointment Interval must be greater than 0 minutes.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                tb_Settings.SelectedIndex = 0
                AppointmentInterval.Focus()
                Exit Sub
            End If
            If AppointmentInterval.Value Mod 5 <> 0 Then
                MessageBox.Show("Appointment Interval must be in multiple of 5 minutes.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                tb_Settings.SelectedIndex = 0
                AppointmentInterval.Focus()
                Exit Sub
            End If
            If (txtSequentialPatientCode.Text.Trim() = "") Then
                MessageBox.Show("Enter sequential patient code.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtSequentialPatientCode.Focus()
                Exit Sub
            End If


            C1ExpandedClaimSettings.FinishEditing()
            If ValidateExpClaimData() = False Then
                Exit Sub
            End If

            If ValidateTypeOfBill() = False Then
                Exit Sub
            End If

            Dim objSQLSettings As New clsStartUpSettings
            Dim str = ""
            If (gstrAdminFor = "gloPM") Then
                str = "gloEMR"
            Else
                str = "gloPM"
            End If


            If chk_PMDBSettings.Checked = True Then

                If txtPMServerName.Text.Trim = "" Then
                    MessageBox.Show("Please enter  " + str + " Server Name.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If

                If txtPMDatabaseName.Text.Trim = "" Then
                    MessageBox.Show("Please enter  " + str + " Database Name.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If

                If optSQLAuthentication.Checked = True Then
                    If txtSQLUserID.Text.Trim = "" Then
                        MessageBox.Show("Please enter SQL User ID for " + str + ".", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If

                    If txtSQLPassword.Text.Trim = "" Then
                        MessageBox.Show("Please enter SQL Password for " + str + ".", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If

                    If objSQLSettings.IsSQLConnect(txtPMServerName.Text.Trim, txtPMDatabaseName.Text.Trim, txtSQLUserID.Text.Trim, txtSQLPassword.Text.Trim) = False Then
                        If MessageBox.Show("Unable to connect to SQL Server " & txtPMServerName.Text.Trim & " and Database " & txtPMDatabaseName.Text.Trim & vbCrLf & "Please select valid credentials.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information) Then
                            Exit Sub
                        End If
                    End If
                Else
                    If objSQLSettings.IsConnect(txtPMServerName.Text.Trim, txtPMDatabaseName.Text.Trim, False, "", "") = False Then
                        If MessageBox.Show("Unable to connect to SQL Server " & txtPMServerName.Text.Trim & " and Database " & txtPMDatabaseName.Text.Trim & vbCrLf & "Please select valid credentials.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information) Then
                            Exit Sub
                        End If
                    End If
                End If

            End If

            Dim objSettings As New clsSettings
            If objSettings.GetSettings() = True Then
                If IsNothing(objSettings.FutureCloseDateDays) = False Then
                    If numFutureCloseDateDays.Value > objSettings.FutureCloseDateDays Then
                        If numFutureCloseDateDays.Value <= numFutureCloseDateDays.Maximum Then
                            If MessageBox.Show("Warning  – Increasing Close Dates in the future - # Days setting may allow transactions to be assigned to the future accidentally.  This can lead to accidental closes before the practice is ready." + Environment.NewLine +
                                               "Are you sure you want to increase the # Days?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.No Then
                                If objSettings.FutureCloseDateDays > 0 Then
                                    numFutureCloseDateDays.Value = objSettings.FutureCloseDateDays
                                Else
                                    numFutureCloseDateDays.Value = 7
                                End If

                                Exit Sub
                            End If
                        End If
                    End If
                End If
            End If

            'If (ValidateAlphaIISettings() = False) Then
            '    Exit Sub
            'End If

            If (txtPatientCodePrefix.Text.Trim().Length <> 3 And txtPatientCodePrefix.Text.Trim() <> "") Then
                MessageBox.Show("Patient code prefix must be 3 characters.  ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtPatientCodePrefix.Focus()
                Exit Sub
            End If


            SavePaymentSetting()
            Me.Cursor = Cursors.WaitCursor

            ogloSettings.ClearValuesFromTVP()
            ''objSettings.Country = cmbCountry.SelectedValue
            objSettings.WriteOff = cmbWriteOff.SelectedValue
            objSettings.Copay = cmbCopay.SelectedValue
            objSettings.Deductible = cmbDeductible.SelectedValue
            objSettings.CoInsurance = cmbCoInsurance.SelectedValue
            objSettings.Withhold = cmbWithHold.SelectedValue
            objSettings.AppointmentStartTime = tmStartTime.Value.ToShortTimeString()
            objSettings.AppointmentEndTime = tmEndTime.Value.ToShortTimeString()
            objSettings.AppointmentInterval = AppointmentInterval.Value
            objSettings.NoOfAttempts = numLockOutAttempts.Value '' lock out attempts 
            objSettings.blnPwdComplexity = optPwdComplexYes.Checked
            objSettings.HL7SystemPath = txtHL7FilePath.Text
            'objSettings.HL7ReceivingFacility = txtRecFacility.Text
            'objSettings.HL7ReceivingApplication = txtRecAppl.Text
            'objSettings.HL7SendingFacility = txtSendFacility.Text
            objSettings.DBVersion = txtDBVersion.Text
            objSettings.AppVersion = txtAppVersion.Text
            objSettings.GenerateUnclosedDayStatement = chkUnclosedDayStatement.Checked
            objSettings.FutureCloseDateDays = numFutureCloseDateDays.Value
            '-------------------------------------
            objSettings.MultipleClearingHouse = chkIsMultipleClearingHouse.Checked

            objSettings.AutoGeneratePatientCode = chkAutogenerateCode.Checked
            ''Added by Mayuri:20101006-To add functonality of save as copy patient
            objSettings.AllowEditablePatientCode = chkallowEditPatientCode.Checked

            If (_SequentialPatientCode.ToString() <> txtSequentialPatientCode.Text.ToString.Trim()) Then
                Dim objAudit1 As New clsAudit
                Dim ogloSettings As gloSettings.GeneralSettings = New gloSettings.GeneralSettings(mdlGeneral.GetConnectionString)
                ogloSettings.AddSetting("SequentialPatientCode", txtSequentialPatientCode.Text.ToString.Trim().Replace(" ", ""), _ClinicID, 0, gloSettings.SettingFlag.Clinic)
                objAudit1.CreateLog(clsAudit.enmActivityType.Other, "Sequential patient code saved with value =" & txtSequentialPatientCode.Text.ToString(), gstrLoginName, gstrClientMachineName)
            End If
            objSettings.UseSitePrefix = ChkUseSitePrefix.Checked
            Dim objEncrypt As New clsEncryption
            objSettings.SignatureText = Convert.ToString(txtSignaturetext.Text.Trim())
            objSettings.CoSignatureText = Convert.ToString(txtcoSignaturetext.Text.Trim())
            If (gstrAdminFor = "gloPM") Then

                SavegloEMRDatabaseSettingsNew()
            Else
                objSettings.PMAddPatient = chk_PMDBSettings.Checked
                If (objSettings.PMAddPatient = True) Then
                    objSettings.PMServerName = txtPMServerName.Text.Trim
                    objSettings.PMDatabaseName = txtPMDatabaseName.Text.Trim
                    objSettings.PMUserID = txtSQLUserID.Text.Trim
                    objSettings.PMSQLPwd = txtSQLPassword.Text.Trim
                End If
            End If

            objSettings.UB04_Capitalizedata = chkCapatalizeUB04Data.Checked
            Dim ubFont() As String = txtFont_Ub.Text.Split(",")
            If ubFont.Length = 2 Then
                objSettings.UB04_Font = ubFont(0)
                objSettings.UB04_FontSize = ubFont(1)
            Else
                objSettings.UB04_Font = ""
                objSettings.UB04_FontSize = ""
            End If

            objSettings.CMS1500_Capitalize0212data = chkCapatalizeCMSData.Checked
            objSettings.EnableCMSFontSizeSelection = chkEnableCMSfontsizeselection.Checked
            objSettings.EnableUBFontSizeSelection = ChkEnableUB04FontSelection.Checked
            Dim cmsFont() As String = txtFont_Cms.Text.Split(",")
            If cmsFont.Length = 2 Then
                objSettings.CMS1500_Font = cmsFont(0)
                objSettings.CMS1500_FontSize = cmsFont(1)
            Else
                objSettings.CMS1500_Font = ""
                objSettings.CMS1500_FontSize = ""
            End If

           


            objSettings.PMConnectionString = gloPMAdmin.mdlGeneral.GetConnectionString(txtPMServerName.Text.Trim, txtPMDatabaseName.Text.Trim, False, "", "")

            ''setting added for global hl7outbound on 07-dec-2012
            If (chkhl7outb.Checked = False) Then
                objSettings.globlnhl7OutBound = False
                objSettings.globlnhl7Sendpatientdet = False
                objSettings.globlnhl7Sendapptdet = False

            Else
                objSettings.globlnhl7OutBound = chkhl7outb.Checked
                objSettings.globlnhl7Sendpatientdet = chkhl7PatientReg.Checked
                objSettings.globlnhl7Sendapptdet = chkHL7Appointment.Checked

            End If




            If chk_PMDBSettings.Checked = True Then
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


                    _sqlEMRConnection.Open()
                    _sqlPMConnection.Open()
                    _sqlEMRTransaction = _sqlEMRConnection.BeginTransaction()
                    _sqlPMTransaction = _sqlPMConnection.BeginTransaction()

                    '' To CLEAN ALL EXTERNAL IDs FROM gloPM PROVIDER MASTER
                    _sqlQuery = "UPDATE Provider_MST SET sExternalCode = ''"
                    _sqlCommand = New SqlCommand(_sqlQuery, _sqlPMConnection, _sqlPMTransaction)
                    _sqlCommand.ExecuteNonQuery()

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
                    _sqlPMConnection.Close()
                Catch ex As Exception
                    _sqlEMRTransaction.Rollback()
                    _sqlPMTransaction.Rollback()
                End Try

            End If

            ''Sandip Darade 
            If (objSettings.PMAddPatient = True) Then
                If optSQLAuthentication.Checked = False Then
                    objSettings.PMSQLAuthentication = 0
                    objSettings.PMConnectionString = gloPMAdmin.mdlGeneral.GetConnectionString(txtPMServerName.Text.Trim, txtPMDatabaseName.Text.Trim, False, "", "")

                Else
                    objSettings.PMSQLAuthentication = 1
                    objSettings.PMConnectionString = gloPMAdmin.mdlGeneral.GetConnectionString(txtPMServerName.Text.Trim, txtPMDatabaseName.Text.Trim, txtSQLUserID.Text.Trim, txtSQLPassword.Text.Trim)

                End If
            Else
                objSettings.PMConnectionString = ""

            End If


            objSettings.DefaultPatientGender = cmbGender.Text

            If cmb_Feeschedules.SelectedIndex <> -1 Then
                objSettings.ClinicFeeSchedule = Convert.ToString(cmb_Feeschedules.SelectedValue)
            Else
                objSettings.ClinicFeeSchedule = 0

            End If

            SaveBillingSettingNew()

            Call SaveMidLevelBillingSettingNew()
            Call SaveExpandedClaimSettings()

            ''Add Other billing setting 
            If cmbSlfPayAllwdAmnts.SelectedIndex <> -1 Then
                objSettings.DefaultSelfPayAllowed = cmbSlfPayAllwdAmnts.SelectedValue
            Else
                objSettings.DefaultSelfPayAllowed = ""

            End If

            If cmb_SlfPayDefaultFeeSchedule.SelectedIndex <> -1 Then
                objSettings.DefaultSelfPayFeeSchedule = cmb_SlfPayDefaultFeeSchedule.SelectedValue
            Else
                objSettings.DefaultSelfPayFeeSchedule = ""
            End If

            If ChkExplicitlyAcceptTask.Checked = True Then
                objSettings.ExplicitlyAcceptTask = 1
            Else
                objSettings.ExplicitlyAcceptTask = 0
            End If


            objSettings.NoOfClaimPerBatch = numUpDn_NoOfClaims.Value.ToString()


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

            SaveAlphaIIDatabaseSettingsNew()
            SaveMaritalStatusSettingsNew()
            SavePaperBillingSetting()
            AddExServerSettingNew()
            AddOtherSettingsNew()
            SaveInsuranceCorrection_RefundNew()
            '-------

            'Save bUsePrefixforBatch and bUsePrefixforClaims Settings
            SaveClaimPrefixSettings()

            ''7022Items: Home Billing- Added to save setting for USEAREACODEFORPATIENT in database
            ''Save 0 if No is selected and 1 if Yes is selected
            If rbtn_UseAreaCodeForPatientNo.Checked Then
                ogloSettings.AddValueInTVP("USEAREACODEFORPATIENT", "0", _ClinicID, 0, SettingFlag.Clinic)
            End If
            ''7022Items: Home Billing- Added to save setting for USEAREACODEFORPATIENT in database
            ''Save 0 if No is selected and 1 if Yes is selected
            If rbtn_UseAreaCodeForPatientYes.Checked Then
                ogloSettings.AddValueInTVP("USEAREACODEFORPATIENT", "1", _ClinicID, 0, SettingFlag.Clinic)
            End If

            If cmbCountry.SelectedIndex <> -1 Then
                ogloSettings.AddValueInTVP("Country", cmbCountry.SelectedValue, _ClinicID, 0, 1)
            Else
                ogloSettings.AddValueInTVP("Country", "", _ClinicID, 0, 1)

            End If

            If cmbSameDayApptType.SelectedIndex <> -1 Then
                ogloSettings.AddValueInTVP("Default AppointmentType for Same Day", Convert.ToString(cmbSameDayApptType.SelectedValue), _ClinicID, 0, 1)
            Else
                ogloSettings.AddValueInTVP("Default AppointmentType for Same Day", "", _ClinicID, 0, 1)

            End If

            If cmbFutureApptType.SelectedIndex <> -1 Then
                ogloSettings.AddValueInTVP("Default AppointmentType for Future", Convert.ToString(cmbFutureApptType.SelectedValue), _ClinicID, 0, 1)
            Else
                ogloSettings.AddValueInTVP("Default AppointmentType for Future", "", _ClinicID, 0, 1)

            End If


            ''added by Ujwala as on 02032015 to Store gloServices DB settings 
            'saving Services database settings
            If txtSrvcServerName.Text.Trim = "" Then
                MessageBox.Show("Enter Services database settings Server Name.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                tb_Settings.SelectTab(tb_Settings.TabPages.IndexOf(tbpg_ReportServerSettings))
                txtSrvcServerName.Focus()
                Exit Sub
            End If
            If txtSrvcDatabaseName.Text.Trim = "" Then
                MessageBox.Show("Enter Services database settings Database Name.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                tb_Settings.SelectTab(tb_Settings.TabPages.IndexOf(tbpg_ReportServerSettings))
                txtSrvcDatabaseName.Focus()
                Exit Sub
            End If
            If optSrvcSQLAuthentication.Checked = True Then
                If txtSrvcSQLUserID.Text.Trim = "" Then
                    MessageBox.Show("Enter Services database settings SQL User ID", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    tb_Settings.SelectTab(tb_Settings.TabPages.IndexOf(tbpg_ReportServerSettings))
                    txtSrvcSQLUserID.Focus()
                    Exit Sub
                End If
                If txtSrvcSQLPassword.Text.Trim = "" Then
                    MessageBox.Show("Enter Services database settings SQL Password", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    tb_Settings.SelectTab(tb_Settings.TabPages.IndexOf(tbpg_ReportServerSettings))
                    txtSrvcSQLPassword.Focus()
                    Exit Sub
                End If
                If objSQLSettings.IsSQLConnect(txtSrvcServerName.Text.Trim, txtSrvcDatabaseName.Text.Trim, txtSrvcSQLUserID.Text.Trim, txtSrvcSQLPassword.Text.Trim) = False Then
                    If MessageBox.Show("Unable to connect to Services database settings SQL Server " & txtSrvcServerName.Text.Trim & " and Database " & txtSrvcDatabaseName.Text.Trim & vbCrLf & "Select valid credentials.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information) Then
                        tb_Settings.SelectTab(tb_Settings.TabPages.IndexOf(tbpg_ReportServerSettings))
                        Exit Sub
                    End If
                End If
            Else
                If objSQLSettings.IsConnect(txtSrvcServerName.Text.Trim, txtSrvcDatabaseName.Text.Trim, False, "", "") = False Then
                    If MessageBox.Show("Unable to connect to Services database settings SQL Server " & txtSrvcServerName.Text.Trim & " and Database " & txtSrvcDatabaseName.Text.Trim & vbCrLf & "Select valid credentials.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information) Then
                        tb_Settings.SelectTab(tb_Settings.TabPages.IndexOf(tbpg_ReportServerSettings))
                        Exit Sub
                    End If
                End If
            End If

            objSettings.ServicesServerName = txtSrvcServerName.Text.Trim
            objSettings.ServicesDatabaseName = txtSrvcDatabaseName.Text.Trim
            objSettings.ServicesUserID = txtSrvcSQLUserID.Text.Trim

            '''''''''''''Set global variable '''''''''''''''
            gstrServicesDBName = txtSrvcDatabaseName.Text.Trim
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

            'end by Ujwala as on 02032015 to Store gloServices DB settings 
            gstrServicesServerName = objSettings.ServicesServerName
            gstrServicesUserID = objSettings.ServicesUserID
            gstrServicesPassWord = txtSrvcSQLPassword.Text
            gbServicesIsSQLAUTHEN = objSettings.ServicesAuthentication
            If rdbYesOCP.Checked = True Then
                If Convert.ToString(cmbOCPDMSCategory.Text) = "" Then
                    MessageBox.Show("Please select RCM Document Category", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    cmbOCPDMSCategory.Focus()
                    Exit Sub
                End If

                objSettings.OCPPortalEnable = True
            ElseIf rdbNoOCP.Checked = True Then
                objSettings.OCPPortalEnable = False
            End If
            objSettings.OCPRCMDefaultCategory = Convert.ToString(cmbOCPDMSCategory.Text)

            If rdbEnableCentralizedRE.Checked Then
                objSettings.IsCentralizedRuleEngineEnable = True
            Else
                objSettings.IsCentralizedRuleEngineEnable = False
            End If
            objSettings.sCentralizedQCommunicationServiceURL = txtQCommunicationURL.Text.Trim()
            ''Genuis Path settings 
            If objSettings.UpdateSettings() = False Then
                Me.Cursor = Cursors.Default
                MessageBox.Show("Unable to update settings", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                tmStartTime.Focus()
                objSettings = Nothing
                Exit Sub

            Else
                ''Follow Up USer
                For i As Integer = 1 To colUId.Count
                    ogloSettings.AddValueInTVP("Followup User", colUId.Item(i).ToString, _ClinicID, 0, 1)
                Next

                ''Surgical User
                For i As Integer = 1 To col_SurgicalUId.Count
                    ogloSettings.AddValueInTVP("Surgical User", col_SurgicalUId.Item(i).ToString, _ClinicID, 0, 1)
                Next

            End If

            Dim objAudit As New clsAudit

            If objSettings.blnPwdComplexity = True Then
                If m_strSQL <> "" Then
                    If objSettings.SetPwdComplexitySettings(m_strSQL) = False Then
                        Me.Cursor = Cursors.Default
                        MessageBox.Show("Unable to update the password complexity settings", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        tmStartTime.Focus()
                        objSettings = Nothing
                        Exit Sub
                    Else
                        'sarika  21 feb

                        objAudit.CreateLog(clsAudit.enmActivityType.Other, gstrLoginName & " has reset the Password Settings.", gstrLoginName, gstrClientMachineName)
                    End If
                End If

            Else
                If sqlstrsettings <> "" Then
                    If objSettings.SetPwdComplexitySettings(sqlstrsettings) = False Then
                        Me.Cursor = Cursors.Default
                        MessageBox.Show("Unable to update the password complexity settings", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        tmStartTime.Focus()
                        objSettings = Nothing
                        Exit Sub
                    Else
                        'sarika  21 feb
                        objAudit.CreateLog(clsAudit.enmActivityType.Other, gstrLoginName & " has reset the Password Settings.", gstrLoginName, gstrClientMachineName)
                    End If
                End If

            End If


            objAudit.CreateLog(clsAudit.enmActivityType.Other, gstrLoginName & " has reset the Settings.", gstrLoginName, gstrClientMachineName)
            objAudit = Nothing
            '-------------

            'objSettings = Nothing

            SaveProviderSettingNew()


            If (rdoV1.Checked) Then
                ogloSettings.AddValueInTVP("STATEMENT_VERSION", "1", _ClinicID, 0, SettingFlag.Clinic)
            ElseIf (rdoV2.Checked) Then
                ogloSettings.AddValueInTVP("STATEMENT_VERSION", "2", _ClinicID, 0, SettingFlag.Clinic)
            Else
                ogloSettings.AddValueInTVP("STATEMENT_VERSION", "", _ClinicID, 0, SettingFlag.Clinic)
            End If

            If (rdoIscYes.Checked) Then
                ogloSettings.AddValueInTVP("INCLUDE_SATISFIED_CHARGES", "true", _ClinicID, 0, SettingFlag.Clinic)
            ElseIf (rdoIscNo.Checked) Then
                ogloSettings.AddValueInTVP("INCLUDE_SATISFIED_CHARGES", "false", _ClinicID, 0, SettingFlag.Clinic)
            Else
                ogloSettings.AddValueInTVP("INCLUDE_SATISFIED_CHARGES", "false", _ClinicID, 0, SettingFlag.Clinic)
            End If

            If (rdProviderMandatoryYes.Checked) Then
                ogloSettings.AddValueInTVP("PATRSV_PROVIDERMANDATORY", "true", _ClinicID, 0, SettingFlag.Clinic)
            Else
                ogloSettings.AddValueInTVP("PATRSV_PROVIDERMANDATORY", "false", _ClinicID, 0, SettingFlag.Clinic)
            End If

            If (rdProviderDefaultYes.Checked) Then
                ogloSettings.AddValueInTVP("PATRSV_DEFAULTPROVIDER", "true", _ClinicID, 0, SettingFlag.Clinic)
            Else
                ogloSettings.AddValueInTVP("PATRSV_DEFAULTPROVIDER", "false", _ClinicID, 0, SettingFlag.Clinic)
            End If

            If (rdProviderTrackingYes.Checked) Then
                ogloSettings.AddValueInTVP("INSRSV_ENABLEPROVIDER", "true", _ClinicID, 0, SettingFlag.Clinic)
            Else
                ogloSettings.AddValueInTVP("INSRSV_ENABLEPROVIDER", "false", _ClinicID, 0, SettingFlag.Clinic)
            End If

            If (rdFollowUpFeatureYes.Checked) Then
                ogloSettings.AddValueInTVP("FOLLOWUP_FEATURE", "true", _ClinicID, 0, SettingFlag.Clinic)
            Else
                ogloSettings.AddValueInTVP("FOLLOWUP_FEATURE", "false", _ClinicID, 0, SettingFlag.Clinic)
            End If


            If (cmbpatientaccountcode.SelectedIndex >= 0) Then
                ogloSettings.AddValueInTVP("PatientAccountCode", "Claim Number", _ClinicID, 0, SettingFlag.Clinic)
            End If


            ogloSettings.AddValueInTVP("IncludeFacilitieswithPOS11onClaim", rbPrintFacilityYes.Checked.ToString(), _ClinicID, 0, SettingFlag.Clinic)


            'Collection module settings
            ogloSettings.AddValueInTVP("CL_PATACCT_FUBEGINS_AFTERNOSTMNTS", IIf(txtPatAccFUBeginsAfterNoOfStmnt.Text.Trim() = "", 2, txtPatAccFUBeginsAfterNoOfStmnt.Text), _ClinicID, 0, SettingFlag.Clinic)
            ogloSettings.AddValueInTVP("CL_PATACCT_NOOFDAYSAFTERSTMT", IIf(txtPatAccNoOfDaysAfterStmnt.Text.Trim() = "", 15, txtPatAccNoOfDaysAfterStmnt.Text), _ClinicID, 0, SettingFlag.Clinic)
            ogloSettings.AddValueInTVP("CL_PATACCT_START_FUACTION", cmbPatAccStartFUAction.SelectedValue, _ClinicID, 0, SettingFlag.Clinic)
            ogloSettings.AddValueInTVP("CL_PMNTPLAN_DEF_FUACTION", cmbPmntPlanFUAction.SelectedValue, _ClinicID, 0, SettingFlag.Clinic)

            ogloSettings.AddValueInTVP("CL_PMNTPLAN_DEF_FUACTIONDAYS", IIf(txtPmntPlanDefFUActionDays.Text.Trim() = "", 45, txtPmntPlanDefFUActionDays.Text), _ClinicID, 0, SettingFlag.Clinic)

            ogloSettings.AddValueInTVP("CL_INSCLM_START_DEFFUACTION", cmbInsClmStartFUAction.SelectedValue, _ClinicID, 0, SettingFlag.Clinic)
            ogloSettings.AddValueInTVP("CL_INSCLM_START_DEFFUACTIONDAYS", IIf(txtInsClmStartFilingDays.Text.Trim() = "", 15, txtInsClmStartFilingDays.Text), _ClinicID, 0, SettingFlag.Clinic)
            ogloSettings.AddValueInTVP("CL_INSCLM_REBILL_DEFFUACTION", cmbInsClmRebillFUAction.SelectedValue, _ClinicID, 0, SettingFlag.Clinic)
            ogloSettings.AddValueInTVP("CL_INSCLM_REBILL_DEFFUACTIONDAYS", IIf(txtInsClmRebillFilingDays.Text.Trim() = "", 15, txtInsClmRebillFilingDays.Text), _ClinicID, 0, SettingFlag.Clinic)
            ' ogloSettings.AddValueInTVP("CL_BADDEBT_START_FUACTION", cmbExtCollectionStartFUAction.SelectedValue, _ClinicID, 0, SettingFlag.Clinic)

            ogloSettings.AddValueInTVP("EnableEPSDTFamilyPlanning", IIf(chkEPSDTFamPlanFeature.Checked = False, False, True), _ClinicID, 0, SettingFlag.Clinic)
            ogloSettings.AddValueInTVP("EnableAnesthesiaBilling", IIf(chkAnesthesiaBilling.Checked = False, False, True), _ClinicID, 0, SettingFlag.Clinic)
            ogloSettings.AddValueInTVP("EnableWorkersCompBilling", IIf(chkEnableWorkersCompBilling.Checked = False, False, True), _ClinicID, 0, SettingFlag.Clinic)
            ogloSettings.AddValueInTVP("EnableAutoEligibilityInsurance", IIf(ChkAutoInsuranceEligibility.Checked = False, False, True), _ClinicID, 0, SettingFlag.Clinic)
            ogloSettings.AddValueInTVP("EnableAutoEligibilityAppointment", IIf(ChkAutoAppointmentEligibility.Checked = False, False, True), _ClinicID, 0, SettingFlag.Clinic)

            ogloSettings.AddValueInTVP("bIsCaptionize", IIf(ChkCapitalizeEDI.Checked = False, False, True), _ClinicID, 0, SettingFlag.Clinic)
            ogloSettings.AddValueInTVP("bIsCapitalizeInsuranceID", IIf(chkCapitalizeInsID.Checked = False, False, True), _ClinicID, 0, SettingFlag.Clinic)

            nUpDownProviderCompnayCount = NUpDownProviderCompnay.Value
            ogloSettings.AddValueInTVP("NoOfProviderCompany", nUpDownProviderCompnayCount, _ClinicID, 0, SettingFlag.Clinic)

            ogloSettings.AddValueInTVP("UB04_EnableBilling", RbUbBilling_yes.Checked.ToString(), _ClinicID, 0, SettingFlag.Clinic)
            If (RbUbBilling_yes.Checked) Then
                ogloSettings.AddValueInTVP("UB04_RevenueCode", CmbRevenueCode.Text.ToString(), _ClinicID, 0, SettingFlag.Clinic)
                ogloSettings.AddValueInTVP("UB04_TypeOfBill", TxtTypeBill.Text.ToString().Trim(), _ClinicID, 0, SettingFlag.Clinic)
                ogloSettings.AddValueInTVP("UB04_AdmisionType", TxtAdmissionType.Text.ToString().Trim(), _ClinicID, 0, SettingFlag.Clinic)
                ogloSettings.AddValueInTVP("UB04_AdmisionSource", TxtAdmisionSource.Text.ToString().Trim(), _ClinicID, 0, SettingFlag.Clinic)
                ogloSettings.AddValueInTVP("UB04_DischargeStatus", txtDischargeStatus.Text.ToString().Trim(), _ClinicID, 0, SettingFlag.Clinic)

            Else
                ogloSettings.AddValueInTVP("UB04_RevenueCode", "", _ClinicID, 0, SettingFlag.Clinic)
                ogloSettings.AddValueInTVP("UB04_TypeOfBill", "", _ClinicID, 0, SettingFlag.Clinic)
                ogloSettings.AddValueInTVP("UB04_AdmisionType", "", _ClinicID, 0, SettingFlag.Clinic)
                ogloSettings.AddValueInTVP("UB04_AdmisionSource", "", _ClinicID, 0, SettingFlag.Clinic)
                ogloSettings.AddValueInTVP("UB04_DischargeStatus", "", _ClinicID, 0, SettingFlag.Clinic)


            End If



            'Statement minimum Payment
            ogloSettings.AddValueInTVP("Statement Minimum Payment", txtStatementMinPay.Text.Trim().Replace("$", ""), _ClinicID, 0, SettingFlag.Clinic)

            Dim oDBClearingHouse As New gloDatabaseLayer.DBLayer(gstrConnectionString)
            Dim strClearingHouseqry As String = "Select ISNULL(Count(nClearingHouseID),0) from BL_ClearingHouse_MST"
            oDBClearingHouse.Connect(False)
            Dim _ClearingHouseCount As Integer = oDBClearingHouse.ExecuteScalar_Query(strClearingHouseqry)
            oDBClearingHouse.Disconnect()
            oDBClearingHouse.Dispose()


            If _ClearingHouseCount > 1 And chkIsMultipleClearingHouse.Checked = False Then
                MessageBox.Show("Multiple Clearinghouses exist.  Please delete the unused clearinghouses. ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            Else

                If chkIsMultipleClearingHouse.Checked Then
                    ogloSettings.AddValueInTVP("ISMULTIPLECLEARINGHOUSE", "1", _ClinicID, 0, SettingFlag.Clinic)
                Else
                    ogloSettings.AddValueInTVP("ISMULTIPLECLEARINGHOUSE", "0", _ClinicID, 0, SettingFlag.Clinic)
                End If

            End If

            ''Added By MaheshN 20100407
            If (chkcopyclaimstoserver.Checked) Then
                ogloSettings.AddValueInTVP("COPY_EDI_FILES", "1", _ClinicID, 0, SettingFlag.Clinic)
            Else
                ogloSettings.AddValueInTVP("COPY_EDI_FILES", "0", _ClinicID, 0, SettingFlag.Clinic)
            End If


            If (rdoOverlapYes.Checked) Then
                ogloSettings.AddValueInTVP("OVERLAPTEMPLATEAPPOINTMENT", "Y", _ClinicID, 0, SettingFlag.Clinic)
            ElseIf (rdoOverlapNo.Checked) Then
                ogloSettings.AddValueInTVP("OVERLAPTEMPLATEAPPOINTMENT", "N", _ClinicID, 0, SettingFlag.Clinic)
            ElseIf (rdoOverlapUser.Checked) Then
                ogloSettings.AddValueInTVP("OVERLAPTEMPLATEAPPOINTMENT", "U", _ClinicID, 0, SettingFlag.Clinic)
            End If

            If (chbox_restrictedTmpAptmnt.Checked) Then
                ogloSettings.AddValueInTVP("RegisterTemplateAppointmentOnly", "1", _ClinicID, 0, SettingFlag.Clinic)
            Else
                ogloSettings.AddValueInTVP("RegisterTemplateAppointmentOnly", "0", _ClinicID, 0, SettingFlag.Clinic)
            End If
            ''End by pranit on 24 sep 2011"


            'Added by Roopali 14 July 2010 for SSRS reports
            If (txtReportServerName.Text.ToString() <> "" And txtReportFolderName.Text.ToString() <> "") Then
                ogloSettings.AddValueInTVP("ReportServer", txtReportServerName.Text.ToString(), _ClinicID, gnLoginID, SettingFlag.Clinic)
                ogloSettings.AddValueInTVP("ReportFolder", txtReportFolderName.Text.ToString(), _ClinicID, gnLoginID, SettingFlag.Clinic)
            ElseIf (txtReportServerName.Text.ToString() = "" And txtReportFolderName.Text.ToString() = "") Then
                ogloSettings.AddValueInTVP("ReportServer", txtReportServerName.Text.ToString(), _ClinicID, gnLoginID, SettingFlag.Clinic)
                ogloSettings.AddValueInTVP("ReportFolder", txtReportFolderName.Text.ToString(), _ClinicID, gnLoginID, SettingFlag.Clinic)
            ElseIf (txtReportServerName.Text.ToString() = "" Or txtReportFolderName.Text.ToString() = "") Then
                tb_Settings.SelectedTab = tbpg_ReportServerSettings
                _blnValidate = True
                'If (txtReportProtocol.Text.ToString() = "") Then
                '    MessageBox.Show("Enter Report Server Protocol Name.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    tb_Settings.SelectTab(tb_Settings.TabPages.IndexOf(tbpg_ReportServerSettings))
                '    txtReportProtocol.Focus()
                'End If

                If (txtReportServerName.Text.ToString() = "") Then
                    MessageBox.Show("Enter Report Server Name.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    txtReportServerName.Focus()
                End If
                If (txtReportFolderName.Text.ToString() = "") Then
                    MessageBox.Show("Enter Report Folder Name.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    txtReportFolderName.Focus()
                End If
                Exit Sub
            End If
            ogloSettings.AddValueInTVP("ReportVirtualDirectory", txtReportVirtualDir.Text.ToString(), _ClinicID, gnLoginID, SettingFlag.Clinic)


            If (Rdohttp.Checked = False And Rdohttps.Checked = False) Then
                MessageBox.Show("Select Report Server Protocol.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            If Rdohttp.Checked = True Then
                ogloSettings.AddValueInTVP("ReportProtocol", "http", _ClinicID, gnLoginID, SettingFlag.Clinic)
            Else
                ogloSettings.AddValueInTVP("ReportProtocol", "https", _ClinicID, gnLoginID, SettingFlag.Clinic)
            End If


            'For adding/updating  the Patient Account Setting in setting table
            SavePatientAccountSettingNew()

            SaveBillingIDQualifier()
            SaveAppointmentDefaultSetting()

            If rbZero.Checked Then

                If NDCSettings <> "Zero" Then

                    ogloSettings.AddValueInTVP("NDCUnitPricing", "Zero", _ClinicID, 0, SettingFlag.Clinic)
                    Dim objSetting As New clsSettings
                    objSetting.UpdateNDCUnitPrice("Zero")

                End If

            ElseIf rbCalculated.Checked Then

                If NDCSettings <> "Calculated" Then

                    ogloSettings.AddValueInTVP("NDCUnitPricing", "Calculated", _ClinicID, 0, SettingFlag.Clinic)
                    Dim objSetting As New clsSettings
                    objSetting.UpdateNDCUnitPrice("Calculated")

                End If

            End If

            ''Save ANSI Versions settings
            SaveANSIVersions()

            If chkPlan5010.Checked = True Then
                ogloSettings.AddValueInTVP("Enable Insurance Plan 5010", "True", _ClinicID, 0, SettingFlag.Clinic)
            ElseIf chkPlan5010.Checked = False Then
                ogloSettings.AddValueInTVP("Enable Insurance Plan 5010", "False", _ClinicID, 0, SettingFlag.Clinic)
            End If

            ''Save Paper Form Versions settings
            SavePaperFormVersions()

            Try
                SaveICD10TransitionSettings()
            Catch ex As Exception

            End Try



            If (cmbChrgSource IsNot Nothing) Then
                ogloSettings.AddValueInTVP("EMRTreatmentSource", Convert.ToInt64(cmbChrgSource.SelectedValue), _ClinicID, 0, SettingFlag.Clinic)
            End If

            If (cmbDefaultProviderType IsNot Nothing) Then
                ogloSettings.AddValueInTVP("DefaultProviderType", Convert.ToString(cmbDefaultProviderType.SelectedValue), _ClinicID, 0, SettingFlag.Clinic)
            End If

            ''ogloSettings.AddValueInTVP("EnableSupervisorOption", Convert.ToBoolean(rbSupervisorSettingEnabledYES.Checked), _ClinicID, 0, SettingFlag.Clinic)
            ogloSettings.AddValueInTVP("EnableSupervisorOption", Convert.ToBoolean(chkShowRefProvAsSupervisor.Checked), _ClinicID, 0, SettingFlag.Clinic)

            ''Global Periods Settings 
            ogloSettings.AddValueInTVP("SkipZero_GlobalPeriods_CPT", Convert.ToBoolean(chkSkipZeroGlobalPeriods.Checked), _ClinicID, 0, SettingFlag.Clinic)


            ''Charge Entry  Defaults for Next Patient Settings 
            If (rdoDefaultBatch.Checked) Then
                ogloSettings.AddValueInTVP("CHRG_ENTRYDEFAULTS", "Batch", _ClinicID, 0, SettingFlag.Clinic)
                ogloSettings.AddValueInTVP("CHRG_DEFAULTS_DOS", Convert.ToBoolean(chkDOS.Checked), _ClinicID, 0, SettingFlag.Clinic)
                ogloSettings.AddValueInTVP("CHRG_DEFAULTS_FACILITY", Convert.ToBoolean(chkFacility.Checked), _ClinicID, 0, SettingFlag.Clinic)
                ogloSettings.AddValueInTVP("CHRG_DEFAULTS_BILLINGPROVIDER", Convert.ToBoolean(chkBillingProvider.Checked), _ClinicID, 0, SettingFlag.Clinic)
            Else
                ogloSettings.AddValueInTVP("CHRG_ENTRYDEFAULTS", "None", _ClinicID, 0, SettingFlag.Clinic)
            End If

            ogloSettings.AddValueInTVP("ShowInitialTreatmentDate", Convert.ToBoolean(chkInitialTreatmentDate.Checked), _ClinicID, 0, SettingFlag.Clinic)

            'Charge Entry Default for Appointment
            ogloSettings.AddValueInTVP("CHRGAPPT_DEFAULTS_DOS", Convert.ToBoolean(chkApptDOS.Checked), _ClinicID, 0, SettingFlag.Clinic)
            ogloSettings.AddValueInTVP("CHRGAPPT_DEFAULTS_FACILITY", Convert.ToBoolean(chkApptFacility.Checked), _ClinicID, 0, SettingFlag.Clinic)
            ogloSettings.AddValueInTVP("CHRGAPPT_DEFAULTS_RENDERRINGPROVIDER", Convert.ToBoolean(chkApptRenderringProvider.Checked), _ClinicID, 0, SettingFlag.Clinic)



            If (rb_BusinessCenter.Checked = False And rb_BC_PatAcc_Yes.Checked = False And rb_BC_PatAcc_No.Checked = False) = False Then

                If (ValidateBusinessCenterSettings()) Then
                    Exit Sub
                End If


                If (rb_BusinessCenter.Checked = True) Then
                    ogloSettings.AddValueInTVP("BusinessCenter_Feature", "True", _ClinicID, 0, SettingFlag.Clinic)
                Else
                    ogloSettings.AddValueInTVP("BusinessCenter_Feature", "False", _ClinicID, 0, SettingFlag.Clinic)
                End If


                If (rb_BC_PatAcc_Yes.Checked = False And rb_BC_PatAcc_No.Checked = False) = False Then
                    If (rb_BC_PatAcc_Yes.Checked = True) Then
                        ogloSettings.AddValueInTVP("BusinessCenter_PatientAccount", "True", _ClinicID, 0, SettingFlag.Clinic)
                    Else
                        ogloSettings.AddValueInTVP("BusinessCenter_PatientAccount", "False", _ClinicID, 0, SettingFlag.Clinic)
                    End If
                End If


                If (rb_BC_PatAcc_Statment_Yes.Checked = False And rb_BC_PatAcc_Statment_No.Checked = False) = False Then
                    If (rb_BC_PatAcc_Statment_Yes.Checked = True) Then
                        ogloSettings.AddValueInTVP("BusinessCenter_Statment", "True", _ClinicID, 0, SettingFlag.Clinic)
                    Else
                        ogloSettings.AddValueInTVP("BusinessCenter_Statment", "False", _ClinicID, 0, SettingFlag.Clinic)
                    End If
                End If


                If (rb_BC_PatAcc_FollowUp_Yes.Checked = False And rb_BC_PatAcc_FollowUp_No.Checked = False) = False Then
                    If (rb_BC_PatAcc_FollowUp_Yes.Checked = True) Then
                        ogloSettings.AddValueInTVP("BusinessCenter_FollowupQueue", "True", _ClinicID, 0, SettingFlag.Clinic)
                    Else
                        ogloSettings.AddValueInTVP("BusinessCenter_FollowupQueue", "False", _ClinicID, 0, SettingFlag.Clinic)
                    End If
                End If


                If (rb_BC_PatAcc_ClaimBatch_Yes.Checked = False And rb_BC_PatAcc_ClaimBatch_No.Checked = False) = False Then
                    If (rb_BC_PatAcc_ClaimBatch_Yes.Checked = True) Then
                        ogloSettings.AddValueInTVP("BusinessCenter_ClaimBatch", "True", _ClinicID, 0, SettingFlag.Clinic)
                    Else
                        ogloSettings.AddValueInTVP("BusinessCenter_ClaimBatch", "False", _ClinicID, 0, SettingFlag.Clinic)
                    End If
                End If


                If (rb_BC_PatAcc_ChargeMismatch_Warn.Checked = False And rb_BC_PatAcc_ChargeMismatch_None.Checked = False) = False Then
                    If (rb_BC_PatAcc_ChargeMismatch_Warn.Checked = True) Then
                        ogloSettings.AddValueInTVP("BusinessCenter_Mismatch_ChargeSaveAlert", "Warn", _ClinicID, 0, SettingFlag.Clinic)
                        'ElseIf (rb_BC_PatAcc_ChargeMismatch_Restrict.Checked = True) Then
                        '    ogloSettings.AddValueInTVP("BusinessCenter_Mismatch_ChargeSaveAlert", "Restrict", _ClinicID, 0, SettingFlag.Clinic)
                    ElseIf (rb_BC_PatAcc_ChargeMismatch_None.Checked = True) Then
                        ogloSettings.AddValueInTVP("BusinessCenter_Mismatch_ChargeSaveAlert", "None", _ClinicID, 0, SettingFlag.Clinic)
                    Else
                        ogloSettings.AddValueInTVP("BusinessCenter_Mismatch_ChargeSaveAlert", "None", _ClinicID, 0, SettingFlag.Clinic)
                    End If
                End If



                'If (rb_BC_PatAcc_ChargeMismatch_NewAcc_Yes.Checked = False And rb_BC_PatAcc_ChargeMismatch_NewAcc_No.Checked = False) = False Then
                '    If (rb_BC_PatAcc_ChargeMismatch_NewAcc_Yes.Checked = True) Then
                '        ogloSettings.AddValueInTVP("BusinessCenter_Mismatch_OfferNewAccount", "True", _ClinicID, 0, SettingFlag.Clinic)
                '    Else
                '        ogloSettings.AddValueInTVP("BusinessCenter_Mismatch_OfferNewAccount", "False", _ClinicID, 0, SettingFlag.Clinic)
                '    End If
                'End If

            End If

            If chkSplitClaimToPatient.Checked = True Then
                ogloSettings.AddValueInTVP("SplitClaimToPatient", "True", _ClinicID, 0, SettingFlag.Clinic)
            Else
                ogloSettings.AddValueInTVP("SplitClaimToPatient", "False", _ClinicID, 0, SettingFlag.Clinic)
            End If
            ' added on 12182013 sameer for Default Date Qualifier setting
            If (cmbDefaultDateQualifier IsNot Nothing) Then
                ogloSettings.AddValueInTVP("DefaultDateQualifier", Convert.ToString(cmbDefaultDateQualifier.SelectedValue), _ClinicID, 0, SettingFlag.Clinic)
            End If

            ogloSettings.AddValueInTVP("bIsSkipZeroBillingClaimForERA", IIf(ChkSkipZeroBillingClaimForERA.Checked = False, False, True), _ClinicID, 0, SettingFlag.Clinic)
            ogloSettings.AddValueInTVP("bIsSkipZeroBillingClaimIPP", IIf(chkIppSkippZeroClaimbilling.Checked = False, False, True), _ClinicID, 0, SettingFlag.Clinic)
            ogloSettings.AddValueInTVP("IPShowChargeUnit", IIf(chkIPUnits.Checked = False, False, True), _ClinicID, 0, SettingFlag.Clinic)
            ogloSettings.AddValueInTVP("Restrict user to correct the remittance", IIf(chkCorrectRemittance.Checked = False, False, True), _ClinicID, 0, SettingFlag.Clinic)

            ogloSettings.AddValueInTVP("bDuplicateClaimWarning", IIf(chkDuplicateClaimWarning.Checked = False, False, True), _ClinicID, 0, SettingFlag.Clinic)

            'Hemant
            ogloSettings.AddValueInTVP("bEnableclaimRule", IIf(chkEnableclaimRule.Checked = False, False, True), _ClinicID, 0, SettingFlag.Clinic)
            'Hemant
            ogloSettings.AddValueInTVP("bIsAutoDistributePatientCopay", IIf(chkDistributeCopay.Checked = False, False, True), _ClinicID, 0, SettingFlag.Clinic)
            ogloSettings.AddValueInTVP("bViewDocumentsOnCharges", IIf(chkViewDocumentsOnCharges.Checked = False, False, True), _ClinicID, 0, SettingFlag.Clinic)
            savegloCollect()

            ogloSettings.AddValueInTVP("EnableWorkersCompForms", IIf(chkEnableWorkersCompForms.Checked = False, False, True), _ClinicID, 0, SettingFlag.Clinic)


            If (rbExternalCollectionYes.Checked = False And rbExternalCollectionNo.Checked = False) = False Then
                If (rbExternalCollectionYes.Checked = True) Then
                    ogloSettings.AddValueInTVP("ExternalCollectionfeature", "True", _ClinicID, 0, SettingFlag.Clinic)
                Else
                    ogloSettings.AddValueInTVP("ExternalCollectionfeature", "False", _ClinicID, 0, SettingFlag.Clinic)
                End If
            End If

            ogloSettings.AddValueInTVP("ExternalCollectionInsPlan", cmbExternalCollectionInsPlan.SelectedValue, _ClinicID, 0, SettingFlag.Clinic)
            ogloSettings.AddValueInTVP("ExternalCollectionFUAction", cmbExternalCollectionFUAction.SelectedValue, _ClinicID, 0, SettingFlag.Clinic)

            ogloSettings.AddValueInTVP("ShowEMRAlertsOnPMPatientBanner", chkDisplayEMRAlert.Checked, _ClinicID, 0, SettingFlag.Clinic)
            ogloSettings.AddValueInTVP("EnableAppointmentLinkingToCharges", chkEnablePatientAppointmentsLinkingToCharges.Checked, _ClinicID, 0, SettingFlag.Clinic)
            ogloSettings.AddValueInTVP("bIsSendPriorPatPtmToAMTSegment", IIf(chkSendPriorPatPtmToAMTSegment.Checked = False, False, True), _ClinicID, 0, SettingFlag.Clinic)
            ogloSettings.AddValueInTVP("Use NPOI Library for Excel Integration", IIf(chkNPOILibrary.Checked = False, False, True), _ClinicID, 0, SettingFlag.Clinic)
            If chkEnableSingleSignON.Checked Then
                ogloSettings.AddValueInTVP("ENABLESINGLESIGNON", 1, _ClinicID, 0, SettingFlag.Clinic)
            Else
                ogloSettings.AddValueInTVP("ENABLESINGLESIGNON", 0, _ClinicID, 0, SettingFlag.Clinic)
            End If



            ogloSettings.AddValueInTVP("UB04 Font", objSettings.UB04_Font, _ClinicID, 0, SettingFlag.Clinic)
            ogloSettings.AddValueInTVP("UB04 Font Size", objSettings.UB04_FontSize, _ClinicID, 0, SettingFlag.Clinic)
            ogloSettings.AddValueInTVP("Capatalize UB04 data", objSettings.UB04_Capitalizedata.ToString(), _ClinicID, 0, SettingFlag.Clinic)
            ogloSettings.AddValueInTVP("CMS1500 Font", objSettings.CMS1500_Font, _ClinicID, 0, SettingFlag.Clinic)
            ogloSettings.AddValueInTVP("CMS1500 Font Size", objSettings.CMS1500_FontSize, _ClinicID, 0, SettingFlag.Clinic)
            ogloSettings.AddValueInTVP("Capatalize CMS1500 02/12 data", objSettings.CMS1500_Capitalize0212data.ToString(), _ClinicID, 0, SettingFlag.Clinic)

            ogloSettings.AddValueInTVP("MergeScheduledActions", IIf(chkMergeScheduleAction.Checked = False, False, True), _ClinicID, 0, SettingFlag.Clinic)
            ogloSettings.AddValueInTVP("EnableUB04FontSizeSelection", objSettings.EnableUBFontSizeSelection, _ClinicID, 0, SettingFlag.Clinic)
            ogloSettings.AddValueInTVP("EnableCMSFontSizeSelection", objSettings.EnableCMSFontSizeSelection, _ClinicID, 0, SettingFlag.Clinic)
            ogloSettings.AddValueInTVP("EnableEstablishedNonEstablishedPatientWarning", IIf(chkEnableEstablishNonEstablPatWarning.Checked = False, False, True), _ClinicID, 0, SettingFlag.Clinic)
            ogloSettings.AddValueInTVP("EnableDublicateCPTsWarning", IIf(chkDuplicateCPTDXWarning.Checked = False, False, True), _ClinicID, 0, SettingFlag.Clinic)
            'InsurancePayerSetup() 
            Dim nInsurancePayer As Integer = 0
            Dim strInsurancePayer As String = ""
            If rbInsStandardSetup.Checked Then
                nInsurancePayer = 1
                strInsurancePayer = "Standard"
            ElseIf rbInsPayerSetup.Checked Then
                nInsurancePayer = 2
                strInsurancePayer = "Payer"
            ElseIf rbInsManualSetup.Checked Then
                nInsurancePayer = 3
                strInsurancePayer = "Manual"
            End If

            If nInsurancePayer <> 0 Then

                ogloSettings.AddValueInTVP("InsurancePaymentResoneCodeSetup", Convert.ToInt64(nInsurancePayer), _ClinicID, 0, SettingFlag.Clinic)

                If nInsurancePayer <> gloGlobal.gloPMGlobal.ReasonCodeSetup Then
                    Dim strInsurancePayer_Old As String = ""
                    If gloGlobal.gloPMGlobal.ReasonCodeSetup = 1 Then
                        strInsurancePayer_Old = "Standard"
                    ElseIf gloGlobal.gloPMGlobal.ReasonCodeSetup = 2 Then
                        strInsurancePayer_Old = "Payer"
                    ElseIf gloGlobal.gloPMGlobal.ReasonCodeSetup = 3 Then
                        strInsurancePayer_Old = "Manual"
                    End If

                    Dim _strReasonCodeSetupDesc As String = "Reason code posting setup setting value changed from " & strInsurancePayer_Old & " to " & strInsurancePayer & "."
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Admin, ActivityCategory.Settings, ActivityType.Modify, _strReasonCodeSetupDesc, ActivityOutCome.Success, SoftwareComponent.gloPMAdmin, 0, ActivityOutCome.Success, SoftwareComponent.gloPMAdmin, True)
                End If
                ''Save Insurance Payer setup
                If nInsurancePayer = 1 Then
                    If SaveInsurancePayerSetup() = False Then
                        Exit Sub
                    End If
                End If
            End If

            'Cleargage Configuration Setting'
            If Rb_CleargageYes.Checked Then
                If rd_Cleargage_Demo.Checked = False And rd_Cleargage_Live.Checked = False Then
                    MessageBox.Show("Please select cleargage mode", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                ElseIf txtCGClientID.Text.Trim() = "" Then
                    MessageBox.Show("Please enter cleargage client id", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                ElseIf txtCGKey.Text.Trim() = "" Then
                    MessageBox.Show("Please enter cleargage key", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                ElseIf txtCGName.Text.Trim() = "" Then
                    MessageBox.Show("Please enter cleargage Name", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                ElseIf txtCGUserName.Text.Trim() = "" Then
                    MessageBox.Show("Please enter cleargage user name", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                ElseIf txtCGPassword.Text.Trim() = "" Then
                    MessageBox.Show("Please enter cleargage password", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End If
            Dim objEncryptPassword As New clsEncryption
            Dim Encrypted_Cleargage_Pwd As String
            If (txtCGPassword.Text.Trim()) <> "" Then
                Encrypted_Cleargage_Pwd = objEncryptPassword.EncryptToBase64String(txtCGPassword.Text.Trim(), mdlGeneral.constEncryptDecryptKey)
            Else
                Encrypted_Cleargage_Pwd = ""
            End If
            Dim IsDemoMode As Boolean = True

            If rd_Cleargage_Demo.Checked Then
                IsDemoMode = True
            ElseIf rd_Cleargage_Live.Checked Then
                IsDemoMode = False
            End If
            If IsDemoMode Then
                ogloSettings.AddValueInTVP("Cleargage_ClientID", (txtCGClientID.Text.Trim()), _ClinicID, 0, SettingFlag.Clinic)
                ogloSettings.AddValueInTVP("Cleargage_Host", (txtCGHost.Text.Trim()), _ClinicID, 0, SettingFlag.Clinic)
                ogloSettings.AddValueInTVP("Cleargage_Key", (txtCGKey.Text.Trim()), _ClinicID, 0, SettingFlag.Clinic)
                ogloSettings.AddValueInTVP("Cleargage_Name", (txtCGName.Text.Trim()), _ClinicID, 0, SettingFlag.Clinic)
                ogloSettings.AddValueInTVP("Cleargage_Password", Encrypted_Cleargage_Pwd, _ClinicID, 0, SettingFlag.Clinic)
                ogloSettings.AddValueInTVP("Cleargage_FirstName", (txtCGFirstName.Text.Trim()), _ClinicID, 0, SettingFlag.Clinic)
                ogloSettings.AddValueInTVP("Cleargage_LastName", (txtCGLastName.Text.Trim()), _ClinicID, 0, SettingFlag.Clinic)
                ogloSettings.AddValueInTVP("Cleargage_PatientID", (txtCGPatientID.Text.Trim()), _ClinicID, 0, SettingFlag.Clinic)
                ogloSettings.AddValueInTVP("Cleargage_PlanID", (txtCGPlanID.Text.Trim()), _ClinicID, 0, SettingFlag.Clinic)
                ogloSettings.AddValueInTVP("Cleargage_SubscriptionID", (txtCGSubscriberID.Text.Trim()), _ClinicID, 0, SettingFlag.Clinic)
                ogloSettings.AddValueInTVP("Cleargage_UserName", (txtCGUserName.Text.Trim()), _ClinicID, 0, SettingFlag.Clinic)

            Else
                ogloSettings.AddValueInTVP("CleargageLive_ClientID", (txtCGClientID.Text.Trim()), _ClinicID, 0, SettingFlag.Clinic)
                ogloSettings.AddValueInTVP("CleargageLive_Host", (txtCGHost.Text.Trim()), _ClinicID, 0, SettingFlag.Clinic)
                ogloSettings.AddValueInTVP("CleargageLive_Key", (txtCGKey.Text.Trim()), _ClinicID, 0, SettingFlag.Clinic)
                ogloSettings.AddValueInTVP("CleargageLive_Name", (txtCGName.Text.Trim()), _ClinicID, 0, SettingFlag.Clinic)
                ogloSettings.AddValueInTVP("CleargageLive_Password", Encrypted_Cleargage_Pwd, _ClinicID, 0, SettingFlag.Clinic)
                ogloSettings.AddValueInTVP("CleargageLive_FirstName", (txtCGFirstName.Text.Trim()), _ClinicID, 0, SettingFlag.Clinic)
                ogloSettings.AddValueInTVP("CleargageLive_LastName", (txtCGLastName.Text.Trim()), _ClinicID, 0, SettingFlag.Clinic)
                ogloSettings.AddValueInTVP("CleargageLive_PatientID", (txtCGPatientID.Text.Trim()), _ClinicID, 0, SettingFlag.Clinic)
                ogloSettings.AddValueInTVP("CleargageLive_PlanID", (txtCGPlanID.Text.Trim()), _ClinicID, 0, SettingFlag.Clinic)
                ogloSettings.AddValueInTVP("CleargageLive_SubscriptionID", (txtCGSubscriberID.Text.Trim()), _ClinicID, 0, SettingFlag.Clinic)
                ogloSettings.AddValueInTVP("CleargageLive_UserName", (txtCGUserName.Text.Trim()), _ClinicID, 0, SettingFlag.Clinic)
            End If

            ogloSettings.AddValueInTVP("EnableCleargageFeature", IIf(Rb_CleargageYes.Checked = True, True, False), _ClinicID, 0, SettingFlag.Clinic)
            ogloSettings.AddValueInTVP("Cleargage_Mode", IIf(IsDemoMode = True, "Demo", "Live"), _ClinicID, 0, SettingFlag.Clinic)
            ogloSettings.AddValueInTVP("Cleargage_DefaultAdjCodeForDiscount", cmbCG_DiscountAdjCode.SelectedValue, _ClinicID, 0, SettingFlag.Clinic)


            Dim _Result As Boolean

            _Result = ogloSettings.SaveAdminSettings()

            'Sagar Ghodke 2013 Dec 16: Add audit log for CompletePaymentBeforeDailyClose
            If (CompletePaymentBeforeDailyCloseLoadValue <> chkPaymentBeforeDailyClose.Checked) Then
                Dim _strLogEntryDescription As String = "Complete payments before daily close setting value changed from " & Convert.ToString(CompletePaymentBeforeDailyCloseLoadValue) & " to " & Convert.ToString(chkPaymentBeforeDailyClose.Checked) & "."
                gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Admin, ActivityCategory.Settings, ActivityType.Modify, _strLogEntryDescription, ActivityOutCome.Success, SoftwareComponent.gloPMAdmin, 0, ActivityOutCome.Success, SoftwareComponent.gloPMAdmin, True)
                _strLogEntryDescription = Nothing
            End If
            '

            'Save Remote printer settings to registry
            gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftPM, True)
            If chkEnableLocalPrinter.Checked = True Then
                gloGlobal.gloTSPrint.isCopyPrint = True
                gloRegistrySetting.SetRegistryValue("EnableLocalPrinter", "1")
            Else
                gloGlobal.gloTSPrint.isCopyPrint = False
                gloRegistrySetting.SetRegistryValue("EnableLocalPrinter", "0")
            End If
            If rbPrintClaimsEMF.Checked = True Then
                gloGlobal.gloTSPrint.UseEMFForClaims = True
                gloRegistrySetting.SetRegistryValue("UseEMFForClaims", "1")
            Else
                gloGlobal.gloTSPrint.UseEMFForClaims = False
                gloRegistrySetting.SetRegistryValue("UseEMFForClaims", "0")
            End If
            gloRegistrySetting.CloseRegistryKey()
            '
            Me.Cursor = Cursors.Default
            Me.Dispose()
            isSaved = True
        Catch objErr As Exception
            Me.Cursor = Cursors.Default
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            'sarika  21 feb
            Dim objAudit As New clsAudit
            objAudit.CreateLog(clsAudit.enmActivityType.Modify, " Error occured while modifying the settings.", gstrLoginName, gstrClientMachineName, 0, , clsAudit.enmOutcome.Failure)
            objAudit = Nothing
            '-------------
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

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

                If _arrayGetData(10) <> txteFaxDownloadDir.Text.Trim Then
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
            If chkSendMail.Checked = True Then
                arrList.Add(1)
            Else
                arrList.Add(0)
            End If
            arrList.Add(txtSendMail.Text.Trim)


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

            'sarika 31st aug 07
            cmbFaxCategory.Items.Clear()
            '--------------
            cmbOMRCategoryHistory.Items.Add("")
            cmbOMRCategoryROS.Items.Add("")
            cmbOMRCategoryPatientRegistration.Items.Add("")
            cmbCategoryDirective.Items.Add("")
            cmbLabCategory.Items.Add("")

            'sarika 31st aug 07
            cmbFaxCategory.Items.Add("")
            '---------

            Dim nCount As Int16
            For nCount = 1 To clCategories.Count
                cmbOMRCategoryHistory.Items.Add(clCategories(nCount))
                cmbOMRCategoryROS.Items.Add(clCategories(nCount))
                cmbOMRCategoryPatientRegistration.Items.Add(clCategories(nCount))
                cmbCategoryDirective.Items.Add(clCategories(nCount))
                cmbLabCategory.Items.Add(clCategories(nCount))

                'sarika 31st aug 07
                cmbFaxCategory.Items.Add(clCategories(nCount))
                '---------
                ''''Pramod
                cmbRadioCategory.Items.Add(clCategories(nCount))
            Next
            cmbOMRCategoryHistory.EndUpdate()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

#End Region

    'Private Sub optPwdComplexYes_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    If optPwdComplexYes.Checked = True Then
    '        optPwdComplexYes.Font = New Font("Tahoma", 9, FontStyle.Bold)
    '    Else
    '        optPwdComplexYes.Font = New Font("Tahoma", 9, FontStyle.Regular)
    '    End If



    '    'Try
    '    '    If optcnt <> 0 Then
    '    '        If optPwdComplexYes.Checked = True Then
    '    '            Dim frm As New frmPwdSettings
    '    '            frm.ShowDialog()
    '    '        End If
    '    '    End If
    '    '    'cmbOMRCategoryHistory.Focus()
    '    'Catch ex As Exception
    '    '    MsgBox(ex.Message, MsgBoxStyle.OKOnly, "Password Complexity")
    '    'Finally
    '    'End Try
    '    'optcnt = 1
    '    Dim conn As New SqlConnection(gloPMAdmin.mdlGeneral.GetConnectionString)
    '    Dim cmd As SqlCommand
    '    Dim cnt As Integer = 0
    '    Dim _strSQL As String = ""
    '    'Dim oDataReader As SqlDataReader
    '    Dim blnadmin As Boolean = False
    '    Dim str As String = ""

    '    If optPwdComplexYes.Checked = True Then
    '        '   _strSQL = ""
    '        btnSetPwdComplexity.Visible = True
    '        '_strSQL = ""
    '    Else
    '        btnSetPwdComplexity.Visible = False
    '        ' btnSetPwdComplexity.Visible = False
    '        conn.Open()

    '        'str = "Password Complexity"
    '        '_strSQL = "update Settings set sSettingsValue = " & 0 & " where sSettingsName ='" & str & "'"
    '        'cmd = New SqlCommand(_strSQL, conn)
    '        'cmd.ExecuteNonQuery()

    '        _strSQL = "select count(*) from PwdSettings"
    '        cmd = New SqlCommand(_strSQL, conn)
    '        cnt = cmd.ExecuteScalar

    '        If cnt = 0 Then
    '            'insert  row
    '            _strSQL = "insert into PwdSettings(ExpCapitalLetters,ExpNoOfLetters,ExpNoOfDigits,ExpNoOfSpecChars,ExpPwdLength,ExpTimeFrameinDays) " & _
    '                    " values(" & 0 & "," & 0 & "," & 0 & "," & 0 & "," & 1 & "," & 0 & ")"

    '        Else
    '            'update row
    '            _strSQL = "Update PwdSettings set ExpCapitalLetters = " & 0 & " ,ExpNoOfLetters = " & 0 & " ,ExpNoOfDigits = " & 0 & _
    '                    ",ExpNoOfSpecChars = " & 0 & ",ExpPwdLength = " & 1 & ",ExpTimeFrameinDays = " & 0
    '        End If
    '        'cmd = New SqlCommand(_strSQL, conn)
    '        'cmd.ExecuteNonQuery()

    '        'Dim frmSettings As New frmSettings
    '        'frmSettings.strSQL = _strSQL
    '        sqlstrsettings = _strSQL
    '        'm_strSQL = _strSQL

    '    End If

    'End Sub

    ' issue no : 1553 
    Private Sub optPwdComplexYes_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optPwdComplexYes.CheckedChanged

        If optPwdComplexYes.Checked = True Then
            optPwdComplexYes.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            optPwdComplexYes.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If



        'Try
        '    If optcnt <> 0 Then
        '        If optPwdComplexYes.Checked = True Then
        '            Dim frm As New frmPwdSettings
        '            frm.ShowDialog()
        '        End If
        '    End If
        '    'cmbOMRCategoryHistory.Focus()
        'Catch ex As Exception
        '    MsgBox(ex.Message, MsgBoxStyle.OKOnly, "Password Complexity")
        'Finally
        'End Try
        'optcnt = 1
        Dim conn As New SqlConnection(gloPMAdmin.mdlGeneral.GetConnectionString)
        Dim cmd As SqlCommand
        Dim cnt As Integer = 0
        Dim _strSQL As String = ""
        'Dim oDataReader As SqlDataReader
        Dim blnadmin As Boolean = False
        Dim str As String = ""

        If optPwdComplexYes.Checked = True Then
            '   _strSQL = ""
            btnSetPwdComplexity.Visible = True
            '_strSQL = ""
        Else
            btnSetPwdComplexity.Visible = False
            ' btnSetPwdComplexity.Visible = False
            conn.Open()

            'str = "Password Complexity"
            '_strSQL = "update Settings set sSettingsValue = " & 0 & " where sSettingsName ='" & str & "'"
            'cmd = New SqlCommand(_strSQL, conn)
            'cmd.ExecuteNonQuery()

            _strSQL = "select count(*) from PwdSettings"
            cmd = New SqlCommand(_strSQL, conn)
            cnt = cmd.ExecuteScalar

            If cnt = 0 Then
                'insert  row
                _strSQL = "insert into PwdSettings(ExpCapitalLetters,ExpNoOfLetters,ExpNoOfDigits,ExpNoOfSpecChars,ExpPwdLength,ExpTimeFrameinDays) " & _
                        " values(" & 0 & "," & 0 & "," & 0 & "," & 0 & "," & 1 & "," & 0 & ")"

            Else
                'update row
                _strSQL = "Update PwdSettings set ExpCapitalLetters = " & 0 & " ,ExpNoOfLetters = " & 0 & " ,ExpNoOfDigits = " & 0 & _
                        ",ExpNoOfSpecChars = " & 0 & ",ExpPwdLength = " & 1 & ",ExpTimeFrameinDays = " & 0
            End If
            'cmd = New SqlCommand(_strSQL, conn)
            'cmd.ExecuteNonQuery()

            'Dim frmSettings As New frmSettings
            'frmSettings.strSQL = _strSQL
            sqlstrsettings = _strSQL
            'm_strSQL = _strSQL

        End If


        '---------------

        commonTextBox_TextChanged(Nothing, Nothing)

    End Sub

    Private Sub btnSetPwdComplexity_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetPwdComplexity.Click
        'Dim conn As New SqlConnection(gloEMRAdmin.mdlGeneral.GetConnectionString)
        'Dim cmd As SqlCommand
        'Dim cnt As Integer = 0
        'Dim _strSQL As String = ""
        'Dim oDataReader As SqlDataReader

        'Try
        If m_blnSetComplexisityOnSetting = True Then

            Dim frm1 As New frmPwdSettings(m_blnSetComplexisityOnSetting, m_numofCapletters, m_numofLetters, m_numofdigits, m_numofspecialchars, m_numofdays, m_numminlength)
            frm1.ShowDialog(Me)
            m_strSQL = frm1.strSQL
            m_blnSetComplexisityOnSetting = frm1.blnSetComplexisityOnSetting
            'issue no: 1553
            m_numofCapletters = frm1.nNoofCapitalLetters
            m_numofLetters = frm1.nNoofLetters
            m_numofdigits = frm1.nNoofDigits
            m_numofspecialchars = frm1.nNoOfSpecialChars
            m_numofdays = frm1.nNoofDays
            m_numminlength = frm1.nPSWNumMinLength

            Exit Sub

        End If

        Dim frm As New frmPwdSettings
        frm.ShowDialog(Me)
        m_strSQL = frm.strSQL
        m_blnSetComplexisityOnSetting = frm.blnSetComplexisityOnSetting
        'issue no: 1553
        m_numofCapletters = frm.nNoofCapitalLetters
        m_numofLetters = frm.nNoofLetters
        m_numofdigits = frm.nNoofDigits
        m_numofspecialchars = frm.nNoOfSpecialChars
        m_numofdays = frm.nNoofDays
        m_numminlength = frm.nPSWNumMinLength


        If m_blnSetComplexisityOnSetting = False Then
            MessageBox.Show("You have not set password complexicity.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
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
        Dim conn As New SqlConnection(gloPMAdmin.mdlGeneral.GetConnectionString)
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
        End Try
    End Function

    'Private Sub btnBrowseRxReportPath_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Try
    '        With FolderBrowserDialog1()
    '            .ShowNewFolderButton = True
    '            .Description = "Select Rx Report Directory"
    '            If .ShowDialog() = DialogResult.OK Then
    '                txtRxReportPath.Text = .SelectedPath
    '            End If
    '        End With
    '    Catch objErr As Exception
    '        MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try

    'End Sub
    '//code commented by Ravikiran on 14/02/2007
    ' // Setting Report path Settings in Database
    'Private Function InsertRxReportPath(ByVal strPath As String)
    '    Dim objConn As New SqlConnection
    '    Dim objcmd As New SqlCommand
    '    Try


    '        Dim _strSQL As String = ""
    '        objConn.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()
    '        objcmd.Connection = objConn
    '        If objConn.State = ConnectionState.Open Then
    '            objConn.Close()
    '        Else
    '            objConn.Open()
    '            _strSQL = "Select max(nSettingsID) from Settings"
    '            objcmd.CommandText = _strSQL
    '            Dim RxID = objcmd.ExecuteScalar
    '            objcmd.Cancel()
    '            _strSQL = Nothing
    '            If Not IsDBNull(RxID) Then
    '                RxID += 1
    '                _strSQL = "Insert into Settings(nSettingsID,sSettingsName,sSettingsValue) values(" & RxID & ",'RxReportPath','" & strPath & "')"
    '                objcmd.CommandText = _strSQL
    '                ' objcmd.Connection = objConn
    '                objcmd.ExecuteNonQuery()
    '                objcmd.Cancel()
    '            End If


    '        End If
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message)
    '    Finally
    '        objConn.Close()
    '    End Try
    'End Function

    'Private Function checkRxReportPath(ByVal gstrRxReportpath As String) As Boolean
    '    Dim objConn As New SqlConnection
    '    Dim objcmd As New SqlCommand
    '    Dim objReader As SqlDataReader
    '    Try


    '        Dim _strSQL As String = ""
    '        objConn.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()
    '        objcmd.Connection = objConn
    '        If objConn.State = ConnectionState.Open Then
    '            objConn.Close()
    '        Else
    '            objConn.Open()
    '            _strSQL = "Select nSettingsID from Settings where sSettingsName='RxReportPath'"
    '            objcmd.CommandText = _strSQL
    '            Dim RxPath As Long
    '            objReader = objcmd.ExecuteReader
    '            If Not IsDBNull(objReader) Then
    '                If objReader.HasRows Then
    '                    objReader.Read()
    '                    RxPath = objReader(0)
    '                    objReader.Close()
    '                    objcmd.Cancel()
    '                Else
    '                    Return False
    '                End If

    '            Else
    '                Return False

    '            End If

    '            If RxPath <> 0 Then
    '                objcmd.CommandText = "Update Settings set sSettingsValue='" & gstrRxReportpath & "' where nSettingsID=" & RxPath

    '                objcmd.ExecuteNonQuery()
    '                objcmd.Cancel()
    '                Return True
    '            Else
    '                Return False
    '            End If
    '        End If


    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message)
    '        Return False
    '    Finally
    '        objConn.Close()
    '    End Try
    'End Function

    'Private Sub txtNoOfAttempts_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtNoOfAttempts.KeyPress
    '    Try
    '        If Not ((e.KeyChar >= ChrW(48) And e.KeyChar <= ChrW(57)) Or (e.KeyChar = ChrW(8))) Then
    '            e.Handled = True
    '        Else
    '            If (e.KeyChar = ChrW(8)) Then
    '                Exit Sub
    '            Else
    '                txtNoOfAttempts.Focus()
    '            End If
    '        End If
    '    Catch ex As Exception
    '        MessageBox.Show(ex.ToString, "Patient Vitals", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub

    'Private Sub txtNoOfAttempts_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtNoOfAttempts.Validating
    '    Try
    '        If CInt(Val(txtNoOfAttempts.Text.Trim) < 1) Then
    '            MessageBox.Show("LockOut Attempts cannot be 0. It must be atleast 1.", "gloEMR Admin", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '            txtNoOfAttempts.Text = ""
    '            txtNoOfAttempts.Focus()
    '        End If
    '    Catch ex As Exception
    '    End Try
    'End Sub

    Private Sub txtThresholdValue_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtThresholdValue.KeyPress
        'Allow only numeric and decimal point keys
        If InStr(Trim(Text), ".") <> 0 And (e.KeyChar = ChrW(46)) Then
            e.Handled = True
        Else
            If Not ((e.KeyChar >= ChrW(48) And e.KeyChar <= ChrW(57)) Or (e.KeyChar = ChrW(46)) Or (e.KeyChar = ChrW(8))) Then
                e.Handled = True
            End If
        End If
    End Sub
    'code added by sarika on 11th aug 07
    '-------------
    Private Sub btnHL7FilePath_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHL7FilePath.Click

        Try
            With FolderBrowserDialog1()
                .ShowNewFolderButton = True
                .Description = "Select HL7 System Path"
                If .ShowDialog() = DialogResult.OK Then
                    txtHL7FilePath.Text = .SelectedPath
                End If
            End With
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    '-------------

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
        ' ''Added by Mayuri:20101004-To add functonality of save as copy patient
        'If chkAutogenerateCode.Checked = True Then
        '    chkallowEditPatientCode.Enabled = True

        'Else
        '    chkallowEditPatientCode.Enabled = False
        '    '    chkallowEditPatientCode.Checked = False
        'End If
    End Sub
    '****** By Sandip Deshmukh 26 Oct 2007 5.15PM
    '****** To add the follow up User DropDown
    Private Sub btnAddFollowUpUser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddFollowUpUser.Click
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
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnDelFollowUPUser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelFollowUPUser.Click
        Try
            'MessageBox.Show(cmbFollowUpUser.SelectedIndex)
            If cmbFollowUpUser.SelectedIndex = -1 Then

                MessageBox.Show("Please select the Follow-Up user to remove", "Settings", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            Else
                If cmbFollowUpUser.SelectedItem.ToString.Trim <> "" Then
                    colUId.Remove(cmbFollowUpUser.SelectedIndex + 1)
                    colUsers.Remove(cmbFollowUpUser.SelectedIndex + 1)

                    cmbFollowUpUser.Items.Remove(cmbFollowUpUser.SelectedItem)
                End If
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Settings", MessageBoxButtons.OK, MessageBoxIcon.Error)
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
                If c1userList.GetCellCheck(i, Col_Check) = C1.Win.C1FlexGrid.CheckEnum.Checked Then

                    Dim strItem As String = c1userList.GetData(i, Col_LoginName)
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
            pnlFollowUpUser.Visible = False
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
            MessageBox.Show(ex.Message, "Settings", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub btnclose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnclose.Click
        ' colUsers.Clear()
        ' colUId.Clear()

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
                    colUId.Add(dt.Rows(i)("nUserID"))
                    colUsers.Add(dt.Rows(i)("sLoginName"))
                    cmbFollowUpUser.Items.Add(dt.Rows(i)("sLoginName"))
                Next
            End If
            'cmbFollowUpUser.Items.Add()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Settings", MessageBoxButtons.OK, MessageBoxIcon.Error)
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

    Private Sub txtRptPassword_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

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
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    '--------------sarika UseFaxNoPrefix 12th april 08


    Private Sub btnAddSurgicalAlertUser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddSurgicalAlertUser.Click
        Try
            blnSurgicalclick = True 'refer the comment written for this flag.

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
            Dim dt As New DataTable
            Dim strSelectQry As String = "SELECT nUserID , sLoginName,ISNULL( User_MST.sFirstName,'')+' '+ISNULL(User_MST.sLastName,'') as Name, nProviderID FROM User_MST"
            oDB.Connect(gstrConnectionString)
            dt = oDB.ReadQueryData(strSelectQry)
            With C1surgicalUsers
                For i As Integer = 0 To dt.Rows.Count - 1
                    .Rows.Add()
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
            MessageBox.Show(ex.Message, "Patient Messages", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnDelSurgicalAlertUser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelSurgicalAlertUser.Click
        Try
            If cmbSurgicalAlertUser.SelectedIndex = -1 Then

                MessageBox.Show("Please select the Surgical Alert user to remove", "Settings", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            Else
                If cmbSurgicalAlertUser.SelectedItem.ToString.Trim <> "" Then
                    col_SurgicalUId.Remove(cmbSurgicalAlertUser.SelectedIndex + 1)
                    col_SurgicalUsers.Remove(cmbSurgicalAlertUser.SelectedIndex + 1)

                    cmbSurgicalAlertUser.Items.Remove(cmbSurgicalAlertUser.SelectedItem)
                End If
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Settings", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub fill_SurgicalAlertUsers()
        Try
            Dim strQrey As String = ""
            Dim i As Integer

            col_SurgicalUsers.Clear()
            col_SurgicalUId.Clear()



            strQrey = " SELECT   User_MST.nUserID, User_MST.sLoginName FROM  User_MST INNER JOIN  Settings ON  Convert(varchar(80),(User_MST.nUserID)) = Settings.sSettingsValue  and sSettingsName='Surgical User'  "
            Dim oDB As New gloStream.gloDataBase.gloDataBase

            Dim dt As New DataTable
            oDB.Connect(gstrConnectionString)
            dt = oDB.ReadQueryData(strQrey)
            If Not dt.Rows.Count <= 0 Then
                For i = 0 To dt.Rows.Count - 1
                    col_SurgicalUId.Add(dt.Rows(i)("nUserID"))
                    col_SurgicalUsers.Add(dt.Rows(i)("sLoginName"))
                    cmbSurgicalAlertUser.Items.Add(dt.Rows(i)("sLoginName"))
                Next
            End If
            'cmbFollowUpUser.Items.Add()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Settings", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub chkInternetFax_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If chkInternetFax.Checked = True Then
                pnleFaxLoginKey.Visible = True
            Else
                pnleFaxLoginKey.Visible = False
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Settings", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    'code added by supriya 11/7/2008
    Private Sub chkSurescript_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If chkSurescript.Checked = True Then
                pnlSurescriptServer.Enabled = True
            Else
                pnlSurescriptServer.Enabled = False
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
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


    Private Sub chk_AgeFlag_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
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

    Private Sub txtAgeLimitforWeek_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAgeLimitforWeeks.KeyPress
        If Not ((e.KeyChar >= ChrW(48) And e.KeyChar <= ChrW(57)) Or (e.KeyChar = ChrW(8))) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtAgeLimitforWeek_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAgeLimitforWeeks.LostFocus
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

    Private Sub optSQLAuthentication_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If optSQLAuthentication.Checked = True Then
            optSQLAuthentication.Font = New Font("Tahoma", 9, FontStyle.Bold)
            pnlSQLCredentials.Enabled = True
        Else
            optSQLAuthentication.Font = New Font("Tahoma", 9, FontStyle.Regular)
            pnlSQLCredentials.Enabled = False
        End If
    End Sub


    Private Sub chk_PMDBSettings_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
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

    '' Sudhir 20090107 ''
    Public Sub FillProviderGridAll()
        Dim objSettings As New clsSettings
        'Dim _dtProviders As DataTable
        Dim cStyle As C1.Win.C1FlexGrid.CellStyle
        Dim cRange As C1.Win.C1FlexGrid.CellRange

        IsProviderLoading = True
        DesignProviderGrid()
        ''_dtProviders = Get_gloEMR_ProviderList()

        Try

            ''Fill gloEMR Provider
            If IsNothing(_dtProviders) = False Then
                If _dtProviders.Rows.Count > 0 Then
                    For i As Integer = 0 To _dtProviders.Rows.Count - 1
                        C1Provider.Rows.Add()
                        C1Provider.Rows(i + 1)(col_gloEMR_ProviderName) = _dtProviders.Rows(i)("ProviderName").ToString().Trim
                        If IsDBNull(_dtProviders.Rows(i)("sExternalCode")) = False Then
                            If _dtProviders.Rows(i)("sExternalCode") <> "" Then
                                C1Provider.Rows(i + 1)(col_ExternalID) = _dtProviders.Rows(i)("sExternalCode")
                            End If
                        End If
                    Next
                End If
            End If

            If optSQLAuthentication.Checked = False Then
                objSettings.PMSQLAuthentication = 0
                objSettings.PMConnectionString = gloPMAdmin.mdlGeneral.GetConnectionString(txtPMServerName.Text.Trim, txtPMDatabaseName.Text.Trim, False, "", "")
                objSettings.PMUserID = ""
                objSettings.PMSQLPwd = ""
            Else
                objSettings.PMSQLAuthentication = 1
                objSettings.PMUserID = txtSQLUserID.Text.Trim
                objSettings.PMSQLPwd = txtSQLPassword.Text.Trim
                objSettings.PMConnectionString = gloPMAdmin.mdlGeneral.GetConnectionString(txtPMServerName.Text.Trim, txtPMDatabaseName.Text.Trim, txtSQLUserID.Text.Trim, txtSQLPassword.Text.Trim)
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
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
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
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
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
                            MessageBox.Show("Provider already present", mdlGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
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
                            MessageBox.Show("External ID already present", mdlGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
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

    Private Sub btnConnect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConnect.Click
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
                MessageBox.Show("Please enter  " + str + " Server Name.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                C1Provider.Rows.Count = 1
                Exit Sub
            End If

            If txtPMDatabaseName.Text.Trim = "" Then
                MessageBox.Show("Please enter  " + str + " Database Name.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                C1Provider.Rows.Count = 1
                Exit Sub
            End If

            If optSQLAuthentication.Checked = True Then
                If txtSQLUserID.Text.Trim = "" Then
                    MessageBox.Show("Please enter SQL User ID for  " + str + ".", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    C1Provider.Rows.Count = 1
                    Exit Sub
                End If

                If txtSQLPassword.Text.Trim = "" Then
                    MessageBox.Show("Please enter SQL Password for  " + str + ".", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    C1Provider.Rows.Count = 1
                    Exit Sub
                End If

                If objSQLSettings.IsSQLConnect(txtPMServerName.Text.Trim, txtPMDatabaseName.Text.Trim, txtSQLUserID.Text.Trim, txtSQLPassword.Text.Trim) = False Then
                    If MessageBox.Show("Unable to connect to SQL Server " & txtPMServerName.Text.Trim & " and Database " & txtPMDatabaseName.Text.Trim & vbCrLf & "Please select valid credentials.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information) Then
                        C1Provider.Rows.Count = 1
                        Exit Sub
                    End If
                End If
            Else
                If objSQLSettings.IsConnect(txtPMServerName.Text.Trim, txtPMDatabaseName.Text.Trim, False, "", "") = False Then
                    If MessageBox.Show("Unable to connect to SQL Server " & txtPMServerName.Text.Trim & " and Database " & txtPMDatabaseName.Text.Trim & vbCrLf & "Please select valid credentials.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information) Then
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

    Private Sub chkRecoverDMSV2Doc_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)

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
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Button65_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button65.Click
        tb_Settings.SelectedTab = tbp_OtherSettings
    End Sub

    Private Sub Button64_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button64.Click
        tb_Settings.SelectedTab = tbp_EMCodeSetting
    End Sub


    Private Sub Chb_UseCodedhistory_CheckedChanged_1(ByVal sender As System.Object, ByVal e As System.EventArgs)
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
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
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

                Dim blnSendEMail As Integer

                'sarika  UseFaxNoPrefix 12th april 08
                If chkUseFaxNoPrefix.Checked = True Then
                    UseFaxPrefix = 1
                Else
                    UseFaxPrefix = 0
                End If
                '------ UseFaxNoPrefix 12th april 08

                'sarika SendEMail 20090502
                If chkSendMail.Checked = True Then
                    blnSendEMail = 1
                Else
                    blnSendEMail = 0
                End If
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
                If _arrayGetData(8) <> blnSendEMail Then
                    bEFaxSettingsModified = True
                    Return bEFaxSettingsModified
                End If
                If _arrayGetData(9) <> txtSendMail.Text.Trim Then
                    bEFaxSettingsModified = True
                    Return bEFaxSettingsModified
                End If

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
        Try
            If chkSendMail.Checked = True Then
                txtSendMail.Visible = True
                lblEnterEmailAddress.Visible = True

            Else
                'txtSendMail.Text = ""
                txtSendMail.Visible = False
                lblEnterEmailAddress.Visible = False

            End If
        Catch ex As Exception
            MessageBox.Show("Error setting email address for sending Fax Failure notification", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub chkAdvanceRx_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            If chkAdvanceRx.Checked = True Then
                pnlAdvanceRxServer.Enabled = True
            Else
                pnlAdvanceRxServer.Enabled = False
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    '\\commented on 20090820:
    Private Sub btnBrowseEAR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowseEAR.Click
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
            cmbSurgicalAlertUser.Items.Clear()
            For i As Integer = 0 To C1surgicalUsers.Rows.Count - 1
                If C1surgicalUsers.GetCellCheck(i, Col_Check) = C1.Win.C1FlexGrid.CheckEnum.Checked Then

                    Dim strItem As String = C1surgicalUsers.GetData(i, Col_LoginName)
                    Dim blPresent As Boolean = False

                    'chk already in collection
                    For j As Integer = 1 To col_SurgicalUsers.Count
                        If col_SurgicalUsers.Item(j) = strItem Then
                            blPresent = True
                        End If
                    Next

                    'add if not present in collection
                    If Not blPresent Then
                        col_SurgicalUsers.Add(C1surgicalUsers.GetData(i, Col_LoginName))
                        col_SurgicalUId.Add(C1surgicalUsers.GetData(i, Col_UserID))
                    End If
                End If
            Next

            For j As Integer = 1 To col_SurgicalUsers.Count
                cmbSurgicalAlertUser.Items.Add(col_SurgicalUsers.Item(j))
            Next
            PnlUser.Visible = False
            PnlUser.SendToBack()
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub grpClinic_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

#Region "Billing Settings"
    Private Sub cmbProvider_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbProvider.SelectedIndexChanged
        Try
            FillBillingSettings()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
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

            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub FillBillingSettings(ByVal dt As DataTable)
        cmbTOS.SelectedIndex = -1
        cmbPOS.SelectedIndex = -1
        cmbBillingProvider.SelectedIndex = -1
        cmbRenderingProvider.SelectedIndex = -1
        cmbFacility.SelectedIndex = -1
        cmbFeeSchedule.SelectedIndex = -1
        Try
            If cmbProvider.SelectedIndex <> -1 Then

                Dim _dr() As DataRow

                _dr = dt.Select("nProviderID= " & Convert.ToInt64(cmbProvider.SelectedValue))

                If Not IsNothing(_dr) And _dr.Length > 0 Then

                    For Each dataRow As DataRow In _dr

                        Select Case Convert.ToString(dataRow("sName")).Trim()
                            Case "TypeOfService"
                                cmbTOS.SelectedValue = Convert.ToInt64(dataRow("sValue"))
                                Exit Select
                            Case "PlaceOfService"
                                cmbPOS.SelectedValue = Convert.ToInt64(dataRow("sValue"))
                                Exit Select
                            Case "BillingProvider"
                                cmbBillingProvider.SelectedValue = Convert.ToInt64(dataRow("sValue"))
                                Exit Select
                            Case "RenderingProvider"
                                cmbRenderingProvider.SelectedValue = Convert.ToInt64(dataRow("sValue"))
                                Exit Select
                            Case "Facility"
                                cmbFacility.SelectedValue = Convert.ToString(dataRow("sValue"))
                                Exit Select
                            Case "Fee Schedule"
                                cmbFeeSchedule.SelectedValue = Convert.ToString(dataRow("sValue"))
                                Exit Select
                            Case Else
                                Exit Select
                        End Select

                    Next

                End If

            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Function GetProvider() As DataTable
        Try
            Dim odb As New gloStream.gloDataBase.gloDataBase
            Dim _sqlQuery As String = "SELECT ISNULL(nProviderID,0) AS  nProviderID , (ISNULL(sFirstName,'')+ SPACE(1) + ISNULL(sMiddleName,'') + SPACE(1) +ISNULL(sLastName,'')) AS ProviderName FROM  Provider_MST WHERE nClinicID = 1  ORDER BY ProviderName"
            Dim dt As New DataTable
            odb.Connect(gstrConnectionString)
            dt = odb.ReadQueryData(_sqlQuery)
            Return dt

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Function

    Private Function GetReasonCodes() As DataTable
        Try
            Dim odb As New gloStream.gloDataBase.gloDataBase

            Dim _sqlQuery As String = "select nReasonID,isnull(sGroupCode,'') + isnull(sCode,'') as ReasonCode ,isnull(sGroupCode,'') + isnull(sCode,'') + ' - ' + isnull(sDescription,'') as sDescription "
            _sqlQuery += " from BL_ReasonCodes_MST where (bIsBlock IS NULL OR bIsBlock = 'false') "
            _sqlQuery += " AND nClinicID = 1 ORDER BY sDescription "

            Dim dt As New DataTable
            odb.Connect(gstrConnectionString)
            dt = odb.ReadQueryData(_sqlQuery)
            Return dt

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Function

    Private Function GetProviderSettings(ByVal ProviderID As Int64) As DataTable
        Try

            Dim odb As New gloStream.gloDataBase.gloDataBase
            Dim _sqlQuery As String = "SELECT  sName, sValue, nProviderID FROM ProviderSettings WHERE  nProviderID = " & ProviderID & " AND nClinicID =1 "
            Dim dt As New DataTable
            odb.Connect(gstrConnectionString)
            dt = odb.ReadQueryData(_sqlQuery)
            Return dt

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Function


    Private Function GetPOS() As DataTable
        Try

            Dim odb As New gloStream.gloDataBase.gloDataBase
            Dim _sqlQuery As String = "select nPOSID,sPOSCode + '-' + sPOSName AS sPOSCode,sPOSDescription from BL_POS_MST where bIsBlocked='" & False & "' AND nClinicID=" & _ClinicID & " ORDER BY sPOSCode"
            Dim dt As New DataTable
            odb.Connect(gstrConnectionString)
            dt = odb.ReadQueryData(_sqlQuery)
            Return dt

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Function

    Private Function GetTOS() As DataTable
        Try

            Dim odb As New gloStream.gloDataBase.gloDataBase
            Dim _sqlQuery As String = "select nTOSID,sDescription,sTOSCode from BL_TOS_MST where bIsBlocked = '" & False & "' AND nClinicID = " & _ClinicID & " ORDER BY sDescription"
            Dim dt As New DataTable
            odb.Connect(gstrConnectionString)
            dt = odb.ReadQueryData(_sqlQuery)
            Return dt

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Function

    Private Function GetFacilities() As DataTable
        Try

            Dim odb As New gloStream.gloDataBase.gloDataBase
            Dim _sqlQuery As String = ""
            _sqlQuery = "SELECT nFacilityID,sFacilityCode,sFacilityName,sNPI,sMedicadID,sBlueShieldID, " _
                    & "sMedicareID,sCity,sPhone FROM   BL_Facility_MST WHERE bIsBlocked = '" & False & "' AND nClinicID = " & _ClinicID & " "

            Dim dt As New DataTable
            odb.Connect(gstrConnectionString)
            dt = odb.ReadQueryData(_sqlQuery)
            Return dt

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Function

    Private Sub FillProviders()
        Dim dt As New DataTable
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
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If dt IsNot Nothing Then
                dt.Dispose()
            End If
        End Try
    End Sub

    Private Sub FillProviders(ByVal dt As DataTable)
        Try

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
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If dt IsNot Nothing Then
                dt.Dispose()
            End If
        End Try
    End Sub


    Private Sub FillOtherBillingSettings()
        Dim dt As New DataTable
        Try
            dt = GetReasonCodes()
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                cmbWriteOff.DataSource = dt.Copy()
                cmbWriteOff.ValueMember = dt.Columns("ReasonCode").ColumnName
                cmbWriteOff.DisplayMember = dt.Columns("sDescription").ColumnName
                cmbWriteOff.Refresh()
                cmbWriteOff.SelectedIndex = -1

                cmbCopay.DataSource = dt.Copy()
                cmbCopay.ValueMember = dt.Columns("ReasonCode").ColumnName
                cmbCopay.DisplayMember = dt.Columns("sDescription").ColumnName
                cmbCopay.Refresh()
                cmbCopay.SelectedIndex = -1

                cmbDeductible.DataSource = dt.Copy()
                cmbDeductible.DataSource = dt.Copy()
                cmbDeductible.ValueMember = dt.Columns("ReasonCode").ColumnName
                cmbDeductible.DisplayMember = dt.Columns("sDescription").ColumnName
                cmbDeductible.Refresh()
                cmbDeductible.SelectedIndex = -1

                cmbCoInsurance.DataSource = dt.Copy()
                cmbCoInsurance.DataSource = dt.Copy()
                cmbCoInsurance.ValueMember = dt.Columns("ReasonCode").ColumnName
                cmbCoInsurance.DisplayMember = dt.Columns("sDescription").ColumnName
                cmbCoInsurance.Refresh()
                cmbCoInsurance.SelectedIndex = -1

                cmbWithHold.DataSource = dt.Copy()
                cmbWithHold.DataSource = dt.Copy()
                cmbWithHold.ValueMember = dt.Columns("ReasonCode").ColumnName
                cmbWithHold.DisplayMember = dt.Columns("sDescription").ColumnName
                cmbWithHold.Refresh()
                cmbWithHold.SelectedIndex = -1


            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If dt IsNot Nothing Then
                dt.Dispose()
            End If
        End Try
    End Sub


    Private Sub FillOtherBillingSettings(ByVal dt As DataTable)

        Try
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                cmbWriteOff.DataSource = dt.Copy()
                cmbWriteOff.ValueMember = dt.Columns("ReasonCode").ColumnName
                cmbWriteOff.DisplayMember = dt.Columns("sDescription").ColumnName
                cmbWriteOff.Refresh()
                cmbWriteOff.SelectedIndex = -1

                cmbCopay.DataSource = dt.Copy()
                cmbCopay.ValueMember = dt.Columns("ReasonCode").ColumnName
                cmbCopay.DisplayMember = dt.Columns("sDescription").ColumnName
                cmbCopay.Refresh()
                cmbCopay.SelectedIndex = -1

                cmbDeductible.DataSource = dt.Copy()
                cmbDeductible.DataSource = dt.Copy()
                cmbDeductible.ValueMember = dt.Columns("ReasonCode").ColumnName
                cmbDeductible.DisplayMember = dt.Columns("sDescription").ColumnName
                cmbDeductible.Refresh()
                cmbDeductible.SelectedIndex = -1

                cmbCoInsurance.DataSource = dt.Copy()
                cmbCoInsurance.DataSource = dt.Copy()
                cmbCoInsurance.ValueMember = dt.Columns("ReasonCode").ColumnName
                cmbCoInsurance.DisplayMember = dt.Columns("sDescription").ColumnName
                cmbCoInsurance.Refresh()
                cmbCoInsurance.SelectedIndex = -1

                cmbWithHold.DataSource = dt.Copy()
                cmbWithHold.DataSource = dt.Copy()
                cmbWithHold.ValueMember = dt.Columns("ReasonCode").ColumnName
                cmbWithHold.DisplayMember = dt.Columns("sDescription").ColumnName
                cmbWithHold.Refresh()
                cmbWithHold.SelectedIndex = -1


            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If dt IsNot Nothing Then
                dt.Dispose()
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
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

            If dt IsNot Nothing Then
                dt.Dispose()
            End If
        End Try
    End Sub

    Private Sub FillPOS(ByVal dt As DataTable)
        Try
            'dt = GetPOS()
            If dt IsNot Nothing Then
                cmbPOS.DataSource = dt.Copy()
                cmbPOS.ValueMember = dt.Columns("nPOSID").ColumnName
                cmbPOS.DisplayMember = dt.Columns("sPOSCode").ColumnName
                cmbPOS.Refresh()
                cmbPOS.SelectedIndex = -1
            End If
            dt = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

            If dt IsNot Nothing Then
                dt.Dispose()
            End If
        End Try
    End Sub


    Private Sub FillTOS()
        Dim dt As New DataTable
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
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If dt IsNot Nothing Then
                dt.Dispose()
            End If
        End Try
    End Sub

    Private Sub FillTOS(ByVal dt As DataTable)
        Try
            'dt = GetTOS()
            If dt IsNot Nothing Then
                cmbTOS.DataSource = dt.Copy()
                cmbTOS.ValueMember = dt.Columns("nTOSID").ColumnName
                cmbTOS.DisplayMember = dt.Columns("sDescription").ColumnName
                cmbTOS.Refresh()
                cmbTOS.SelectedIndex = -1
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If dt IsNot Nothing Then
                dt.Dispose()
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
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub FillFacilities(ByVal dt As DataTable)

        Try
            'dt = GetFacilities()
            If dt IsNot Nothing Then
                cmbFacility.DataSource = dt
                cmbFacility.ValueMember = dt.Columns("sFacilityCode").ColumnName
                cmbFacility.DisplayMember = dt.Columns("sFacilityName").ColumnName
                cmbFacility.Refresh()
                cmbFacility.SelectedIndex = -1

            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub FillPatAccFollowUpActions(ByVal dt As DataTable)

        Try

            If dt IsNot Nothing Then
                Dim dtnew As New DataTable
                dtnew = dt.Copy()
                cmbPatAccStartFUAction.DataSource = dt
                cmbPatAccStartFUAction.ValueMember = dt.Columns("sFollowUpActionCode").ColumnName
                cmbPatAccStartFUAction.DisplayMember = dt.Columns("sFollowUpDesc").ColumnName
                cmbPatAccStartFUAction.Refresh()
                cmbPatAccStartFUAction.SelectedValue = "Review"

                cmbPmntPlanFUAction.DataSource = dtnew
                cmbPmntPlanFUAction.ValueMember = dtnew.Columns("sFollowUpActionCode").ColumnName
                cmbPmntPlanFUAction.DisplayMember = dtnew.Columns("sFollowUpDesc").ColumnName
                cmbPmntPlanFUAction.Refresh()
                cmbPmntPlanFUAction.SelectedValue = "PayPlanFU"

                cmbExternalCollectionFUAction.DataSource = dtnew.Copy()
                cmbExternalCollectionFUAction.ValueMember = dtnew.Columns("sFollowUpActionCode").ColumnName
                cmbExternalCollectionFUAction.DisplayMember = dtnew.Columns("sFollowUpDesc").ColumnName
                cmbExternalCollectionFUAction.Refresh()
                cmbExternalCollectionFUAction.SelectedIndex = -1
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    'Private Sub FillBadDebtAccFollowUpActions(ByVal dt As DataTable)

    '    Try

    '        If dt IsNot Nothing Then
    '            Dim dtnew As New DataTable
    '            dtnew = dt.Copy()
    '            cmbExtCollectionStartFUAction.DataSource = dt
    '            cmbExtCollectionStartFUAction.ValueMember = dt.Columns("sFollowUpActionCode").ColumnName
    '            cmbExtCollectionStartFUAction.DisplayMember = dt.Columns("sFollowUpDesc").ColumnName
    '            cmbExtCollectionStartFUAction.Refresh()
    '            cmbExtCollectionStartFUAction.SelectedValue = "Review"

    '        End If
    '    Catch ex As Exception
    '        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub

    Private Sub FillAnesthesiaBillingDefaultsSettings(ByVal dt As DataTable)

        Try
            If dt IsNot Nothing Then
                If (dt.Rows.Count > 0) Then
                    Dim oResult As Boolean = False
                    Boolean.TryParse(Convert.ToString(dt.Rows(0)("sSettingsValue")), oResult)
                    chkAnesthesiaBilling.Checked = oResult
                Else
                    chkAnesthesiaBilling.Checked = False
                End If
            Else
                chkAnesthesiaBilling.Checked = False
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub FillAutoInsuranceEligibilityDefaultSettings(ByVal dt As DataTable)
        Try
            If dt IsNot Nothing Then
                If (dt.Rows.Count > 0) Then
                    Dim oResult As Boolean = False
                    Boolean.TryParse(Convert.ToString(dt.Rows(0)("sSettingsValue")), oResult)
                    ChkAutoInsuranceEligibility.Checked = oResult
                Else
                    ChkAutoInsuranceEligibility.Checked = False
                End If
            Else
                ChkAutoInsuranceEligibility.Checked = False
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub FillAutoAppointmentEligibilityDefaultSettings(ByVal dt As DataTable)
        Try
            If dt IsNot Nothing Then
                If (dt.Rows.Count > 0) Then
                    Dim oResult As Boolean = False
                    Boolean.TryParse(Convert.ToString(dt.Rows(0)("sSettingsValue")), oResult)
                    ChkAutoAppointmentEligibility.Checked = oResult
                Else
                    ChkAutoAppointmentEligibility.Checked = False
                End If
            Else
                ChkAutoAppointmentEligibility.Checked = False
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub FillCapitalizeInsuranceIDDefaultSettings(ByVal dt As DataTable)
        Try
            If dt IsNot Nothing Then
                If (dt.Rows.Count > 0) Then
                    Dim oResult As Boolean = False
                    Boolean.TryParse(Convert.ToString(dt.Rows(0)("sSettingsValue")), oResult)
                    chkCapitalizeInsID.Checked = oResult
                Else
                    chkCapitalizeInsID.Checked = False
                End If
            Else
                chkCapitalizeInsID.Checked = False
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub FillCleargageConfigurationSettings(ByVal dt As DataTable)
        Try
            Dim i As Integer
            Dim objDecryptPassword As New clsEncryption
            Dim Decrypt_Cleargage_Pwd As String
            Dim bIsCleargageEnable As Boolean = False
            Dim bIsDemoMode As Boolean = True
            If dt IsNot Nothing Then
                If (dt.Rows.Count > 0) Then

                    For i = 0 To dt.Rows.Count - 1
                        If (dt.Rows(i).Item("sSettingsName").ToString().ToUpper = "ENABLECLEARGAGEFEATURE") Then
                            If (dt.Rows(i).Item("sSettingsValue").ToString().ToLower() = "true") Then
                                Rb_CleargageYes.Checked = True
                                bIsCleargageEnable = True

                            Else
                                Rb_CleargageNo.Checked = True
                                bIsCleargageEnable = False
                            End If
                        End If
                        If (dt.Rows(i).Item("sSettingsName").ToString().ToUpper = "CLEARGAGE_MODE") Then
                            RemoveHandler rd_Cleargage_Demo.CheckedChanged, AddressOf rd_Cleargage_Demo_CheckedChanged
                            RemoveHandler rd_Cleargage_Live.CheckedChanged, AddressOf rd_Cleargage_Live_CheckedChanged

                            If (dt.Rows(i).Item("sSettingsValue").ToString().ToLower() = "") Then
                                rd_Cleargage_Demo.Checked = True
                                bIsDemoMode = True
                            ElseIf (dt.Rows(i).Item("sSettingsValue").ToString().ToLower() = "demo") Then
                                rd_Cleargage_Demo.Checked = True
                                bIsDemoMode = True
                            ElseIf (dt.Rows(i).Item("sSettingsValue").ToString().ToLower() = "live") Then
                                rd_Cleargage_Live.Checked = True
                                bIsDemoMode = False
                            End If
                            AddHandler rd_Cleargage_Demo.CheckedChanged, AddressOf rd_Cleargage_Demo_CheckedChanged
                            AddHandler rd_Cleargage_Live.CheckedChanged, AddressOf rd_Cleargage_Live_CheckedChanged

                        End If
                    Next
                    If bIsCleargageEnable Then
                        If bIsDemoMode Then
                            For i = 0 To dt.Rows.Count - 1
                                If (dt.Rows(i).Item("sSettingsName").ToString().ToUpper = "CLEARGAGE_CLIENTID") Then
                                    txtCGClientID.Text = dt.Rows(i).Item("sSettingsValue").ToString()
                                End If
                                If (dt.Rows(i).Item("sSettingsName").ToString().ToUpper = "CLEARGAGE_FIRSTNAME") Then
                                    txtCGFirstName.Text = dt.Rows(i).Item("sSettingsValue").ToString()
                                End If
                                If (dt.Rows(i).Item("sSettingsName").ToString().ToUpper = "CLEARGAGE_LASTNAME") Then
                                    txtCGLastName.Text = dt.Rows(i).Item("sSettingsValue").ToString()
                                End If
                                If (dt.Rows(i).Item("sSettingsName").ToString().ToUpper = "CLEARGAGE_HOST") Then
                                    txtCGHost.Text = dt.Rows(i).Item("sSettingsValue").ToString()
                                End If
                                If (dt.Rows(i).Item("sSettingsName").ToString().ToUpper = "CLEARGAGE_KEY") Then
                                    txtCGKey.Text = dt.Rows(i).Item("sSettingsValue").ToString()
                                End If
                                If (dt.Rows(i).Item("sSettingsName").ToString().ToUpper = "CLEARGAGE_NAME") Then
                                    txtCGName.Text = dt.Rows(i).Item("sSettingsValue").ToString()
                                End If
                                If (dt.Rows(i).Item("sSettingsName").ToString().ToUpper = "CLEARGAGE_PASSWORD") Then
                                    If (dt.Rows(i).Item("sSettingsValue").ToString() <> "") Then
                                        Decrypt_Cleargage_Pwd = objDecryptPassword.DecryptFromBase64String(dt.Rows(i).Item("sSettingsValue").ToString(), mdlGeneral.constEncryptDecryptKey)
                                        txtCGPassword.Text = Decrypt_Cleargage_Pwd
                                    Else
                                        txtCGPassword.Text = ""
                                    End If
                                    'dt.Rows(i).Item("sSettingsValue").ToString()
                                End If
                                If (dt.Rows(i).Item("sSettingsName").ToString().ToUpper = "CLEARGAGE_PATIENTID") Then
                                    txtCGPatientID.Text = dt.Rows(i).Item("sSettingsValue").ToString()
                                End If
                                If (dt.Rows(i).Item("sSettingsName").ToString().ToUpper = "CLEARGAGE_PLANID") Then
                                    txtCGPlanID.Text = dt.Rows(i).Item("sSettingsValue").ToString()
                                End If
                                If (dt.Rows(i).Item("sSettingsName").ToString().ToUpper = "CLEARGAGE_USERNAME") Then
                                    txtCGUserName.Text = dt.Rows(i).Item("sSettingsValue").ToString()
                                End If
                                If (dt.Rows(i).Item("sSettingsName").ToString().ToUpper = "CLEARGAGE_SUBSCRIPTIONID") Then
                                    txtCGSubscriberID.Text = dt.Rows(i).Item("sSettingsValue").ToString()
                                End If
                            Next
                        Else
                            For i = 0 To dt.Rows.Count - 1
                                If (dt.Rows(i).Item("sSettingsName").ToString().ToUpper = "CLEARGAGELIVE_CLIENTID") Then
                                    txtCGClientID.Text = dt.Rows(i).Item("sSettingsValue").ToString()
                                End If
                                If (dt.Rows(i).Item("sSettingsName").ToString().ToUpper = "CLEARGAGELIVE_FIRSTNAME") Then
                                    txtCGFirstName.Text = dt.Rows(i).Item("sSettingsValue").ToString()
                                End If
                                If (dt.Rows(i).Item("sSettingsName").ToString().ToUpper = "CLEARGAGELIVE_LASTNAME") Then
                                    txtCGLastName.Text = dt.Rows(i).Item("sSettingsValue").ToString()
                                End If
                                If (dt.Rows(i).Item("sSettingsName").ToString().ToUpper = "CLEARGAGELIVE_HOST") Then
                                    txtCGHost.Text = dt.Rows(i).Item("sSettingsValue").ToString()
                                End If
                                If (dt.Rows(i).Item("sSettingsName").ToString().ToUpper = "CLEARGAGELIVE_KEY") Then
                                    txtCGKey.Text = dt.Rows(i).Item("sSettingsValue").ToString()
                                End If
                                If (dt.Rows(i).Item("sSettingsName").ToString().ToUpper = "CLEARGAGELIVE_NAME") Then
                                    txtCGName.Text = dt.Rows(i).Item("sSettingsValue").ToString()
                                End If
                                If (dt.Rows(i).Item("sSettingsName").ToString().ToUpper = "CLEARGAGELIVE_PASSWORD") Then
                                    If (dt.Rows(i).Item("sSettingsValue").ToString() <> "") Then
                                        Decrypt_Cleargage_Pwd = objDecryptPassword.DecryptFromBase64String(dt.Rows(i).Item("sSettingsValue").ToString(), mdlGeneral.constEncryptDecryptKey)
                                        txtCGPassword.Text = Decrypt_Cleargage_Pwd
                                    Else
                                        txtCGPassword.Text = ""
                                    End If
                                    'dt.Rows(i).Item("sSettingsValue").ToString()
                                End If
                                If (dt.Rows(i).Item("sSettingsName").ToString().ToUpper = "CLEARGAGELIVE_PATIENTID") Then
                                    txtCGPatientID.Text = dt.Rows(i).Item("sSettingsValue").ToString()
                                End If
                                If (dt.Rows(i).Item("sSettingsName").ToString().ToUpper = "CLEARGAGELIVE_PLANID") Then
                                    txtCGPlanID.Text = dt.Rows(i).Item("sSettingsValue").ToString()
                                End If
                                If (dt.Rows(i).Item("sSettingsName").ToString().ToUpper = "CLEARGAGELIVE_USERNAME") Then
                                    txtCGUserName.Text = dt.Rows(i).Item("sSettingsValue").ToString()
                                End If
                                If (dt.Rows(i).Item("sSettingsName").ToString().ToUpper = "CLEARGAGELIVE_SUBSCRIPTIONID") Then
                                    txtCGSubscriberID.Text = dt.Rows(i).Item("sSettingsValue").ToString()
                                End If
                            Next
                        End If


                    End If

                    

                End If

            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub FillNoofProviderCompany(ByVal dt As DataTable)

        Try

            If dt IsNot Nothing Then
                If (dt.Rows.Count > 0) Then
                    Dim oResult As Integer = 1
                    Integer.TryParse(Convert.ToString(dt.Rows(0)("sSettingsValue")), oResult)
                    NUpDownProviderCompnay.Value = oResult
                Else
                    NUpDownProviderCompnay.Value = 1
                End If
            Else
                NUpDownProviderCompnay.Value = 1
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub FillEnableWorkersCompBillingDefaultsSettings(ByVal dt As DataTable)

        Try
            If dt IsNot Nothing Then
                If (dt.Rows.Count > 0) Then
                    Dim oResult As Boolean = False
                    Boolean.TryParse(Convert.ToString(dt.Rows(0)("sSettingsValue")), oResult)
                    chkEnableWorkersCompBilling.Checked = oResult
                Else
                    chkEnableWorkersCompBilling.Checked = False
                End If
            Else
                chkEnableWorkersCompBilling.Checked = False
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub SetEnableWorkersCompFormsSetting(ByVal dt As DataTable)

        Try
            If dt IsNot Nothing Then
                If (dt.Rows.Count > 0) Then
                    Dim oResult As Boolean = False
                    Boolean.TryParse(Convert.ToString(dt.Rows(0)("sSettingsValue")), oResult)
                    chkEnableWorkersCompForms.Checked = oResult
                Else
                    chkEnableWorkersCompForms.Checked = False
                End If
            Else
                chkEnableWorkersCompForms.Checked = False
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub FillCapitalizeEDIClaimFileDefaultsSettings(ByVal dt As DataTable)

        Try

            If dt IsNot Nothing Then
                If (dt.Rows.Count > 0) Then
                    Dim oResult As Boolean = False
                    Boolean.TryParse(Convert.ToString(dt.Rows(0)("sSettingsValue")), oResult)
                    ChkCapitalizeEDI.Checked = oResult
                Else
                    ChkCapitalizeEDI.Checked = False
                End If
            Else
                ChkCapitalizeEDI.Checked = False
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub FillEPSDTFamilyPlanningDefaultsSettings(ByVal dt As DataTable)

        Try
            If dt IsNot Nothing Then
                If (dt.Rows.Count > 0) Then
                    Dim oResult As Boolean = False
                    Boolean.TryParse(Convert.ToString(dt.Rows(0)("sSettingsValue")), oResult)
                    chkEPSDTFamPlanFeature.Checked = oResult
                Else
                    chkEPSDTFamPlanFeature.Checked = False
                End If
            Else
                chkEPSDTFamPlanFeature.Checked = False
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub FillInsClaimFollowUpActions(ByVal dt As DataTable)

        Try

            If dt IsNot Nothing Then
                Dim dtnew As New DataTable
                dtnew = dt.Copy()
                cmbInsClmStartFUAction.DataSource = dt
                cmbInsClmStartFUAction.ValueMember = dt.Columns("sFollowUpActionCode").ColumnName
                cmbInsClmStartFUAction.DisplayMember = dt.Columns("sFollowUpDesc").ColumnName
                cmbInsClmStartFUAction.Refresh()
                cmbInsClmStartFUAction.SelectedValue = "Review"

                cmbInsClmRebillFUAction.DataSource = dtnew
                cmbInsClmRebillFUAction.ValueMember = dtnew.Columns("sFollowUpActionCode").ColumnName
                cmbInsClmRebillFUAction.DisplayMember = dtnew.Columns("sFollowUpDesc").ColumnName
                cmbInsClmRebillFUAction.Refresh()
                cmbInsClmRebillFUAction.SelectedValue = "Review"

            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub FillCleargageDefaultDiscountAdjCode(ByVal dt As DataTable)
        
        Dim dtCGAdjCode As New DataTable()
        cmbCG_DiscountAdjCode.DataSource = Nothing
        Dim odb As New gloStream.gloDataBase.gloDataBase
        Try

            Dim _sqlQuery As String = " SELECT ISNULL(sAdjustmentTypeCode,'') AS Code,UPPER(ISNULL(sAdjustmentTypeCode,'')+'-'+ISNULL(sAdjustmentTypeDesc,'')) AS Description " +
                " FROM BL_AdjustmentType_MST WITH (NOLOCK) " +
                " WHERE " +
                " nClinicID = 1 AND bIsBlocked = '" & False & "'"
            odb.Connect(gstrConnectionString)
            dtCGAdjCode = odb.ReadQueryData(_sqlQuery)
            If dtCGAdjCode IsNot Nothing AndAlso dtCGAdjCode.Rows.Count > 0 Then
                Dim dr As DataRow = dtCGAdjCode.NewRow()
                dr(0) = "0"
                dtCGAdjCode.Rows.InsertAt(dr, 0)
                dtCGAdjCode.AcceptChanges()
                cmbCG_DiscountAdjCode.DataSource = dtCGAdjCode
                cmbCG_DiscountAdjCode.DisplayMember = dtCGAdjCode.Columns("Description").ColumnName
                cmbCG_DiscountAdjCode.ValueMember = dtCGAdjCode.Columns("Code").ColumnName


                If dt IsNot Nothing And dt.Rows.Count > 0 Then

                    If (dt.Rows(0).Item("sSettingsName").ToString().ToUpper = "CLEARGAGE_DEFAULTADJCODEFORDISCOUNT") Then
                        cmbCG_DiscountAdjCode.SelectedValue = dt.Rows(0).Item("sSettingsValue")
                    End If
                End If
            End If
        Catch dbEx As gloDatabaseLayer.DBException
            MessageBox.Show(dbEx.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            odb.Disconnect()

        End Try
    End Sub



    Private Sub FillCollectionSettings(ByVal dt As DataTable)

        Try
            Dim cnt As Integer
            If dt IsNot Nothing And dt.Rows.Count > 0 Then
                For cnt = 0 To dt.Rows.Count - 1
                    If (dt.Rows(cnt).Item("sSettingsName").ToString().ToUpper = "CL_INSCLM_START_DEFFUACTION") Then
                        cmbInsClmStartFUAction.SelectedValue = dt.Rows(cnt).Item("sSettingsValue")
                    ElseIf (dt.Rows(cnt).Item("sSettingsName").ToString().ToUpper = "CL_INSCLM_START_DEFFUACTIONDAYS") Then
                        txtInsClmStartFilingDays.Text = dt.Rows(cnt).Item("sSettingsValue")
                    ElseIf (dt.Rows(cnt).Item("sSettingsName").ToString().ToUpper = "CL_INSCLM_REBILL_DEFFUACTION") Then
                        cmbInsClmRebillFUAction.SelectedValue = dt.Rows(cnt).Item("sSettingsValue")
                    ElseIf (dt.Rows(cnt).Item("sSettingsName").ToString().ToUpper = "CL_INSCLM_REBILL_DEFFUACTIONDAYS") Then
                        txtInsClmRebillFilingDays.Text = dt.Rows(cnt).Item("sSettingsValue")
                    ElseIf (dt.Rows(cnt).Item("sSettingsName").ToString().ToUpper = "CL_PATACCT_FUBEGINS_AFTERNOSTMNTS") Then
                        txtPatAccFUBeginsAfterNoOfStmnt.Text = dt.Rows(cnt).Item("sSettingsValue")
                    ElseIf (dt.Rows(cnt).Item("sSettingsName").ToString().ToUpper = "CL_PATACCT_NOOFDAYSAFTERSTMT") Then
                        txtPatAccNoOfDaysAfterStmnt.Text = dt.Rows(cnt).Item("sSettingsValue")
                    ElseIf (dt.Rows(cnt).Item("sSettingsName").ToString().ToUpper = "CL_PATACCT_START_FUACTION") Then
                        cmbPatAccStartFUAction.SelectedValue = dt.Rows(cnt).Item("sSettingsValue")
                    ElseIf (dt.Rows(cnt).Item("sSettingsName").ToString().ToUpper = "CL_PMNTPLAN_DEF_FUACTION") Then
                        cmbPmntPlanFUAction.SelectedValue = dt.Rows(cnt).Item("sSettingsValue")
                    ElseIf (dt.Rows(cnt).Item("sSettingsName").ToString().ToUpper = "CL_PMNTPLAN_DEF_FUACTIONDAYS") Then
                        txtPmntPlanDefFUActionDays.Text = dt.Rows(cnt).Item("sSettingsValue")
                        'ElseIf (dt.Rows(cnt).Item("sSettingsName").ToString().ToUpper = "CL_BADDEBT_START_FUACTION") Then
                        '    cmbExtCollectionStartFUAction.SelectedValue = dt.Rows(cnt).Item("sSettingsValue")
                    End If

                Next

            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub FillChargeEntryDefaultsSettings(ByVal dt As DataTable)

        Try
            Dim cnt As Integer
            pnlChrgDefaultsSettings.Visible = False
            If dt IsNot Nothing And dt.Rows.Count > 0 Then
                For cnt = 0 To dt.Rows.Count - 1
                    If (dt.Rows(cnt).Item("sSettingsName").ToString().ToUpper = "CHRG_ENTRYDEFAULTS") Then
                        If (dt.Rows(cnt).Item("sSettingsValue") = "Batch") Then
                            rdoDefaultBatch.Checked = True
                            rdoDefaultNone.Checked = False
                            pnlChrgDefaultsSettings.Visible = True

                        Else
                            rdoDefaultNone.Checked = True
                            rdoDefaultBatch.Checked = False
                        End If
                    ElseIf (dt.Rows(cnt).Item("sSettingsName").ToString().ToUpper = "CHRG_DEFAULTS_DOS") Then
                        chkDOS.Checked = dt.Rows(cnt).Item("sSettingsValue")
                    ElseIf (dt.Rows(cnt).Item("sSettingsName").ToString().ToUpper = "CHRG_DEFAULTS_FACILITY") Then
                        chkFacility.Checked = dt.Rows(cnt).Item("sSettingsValue")
                    ElseIf (dt.Rows(cnt).Item("sSettingsName").ToString().ToUpper = "CHRG_DEFAULTS_BILLINGPROVIDER") Then
                        chkBillingProvider.Checked = dt.Rows(cnt).Item("sSettingsValue")

                    End If

                Next
            Else
                rdoDefaultBatch.Checked = True
                pnlChrgDefaultsSettings.Visible = True
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub FillChargeEntryDefaultForAppointment(ByVal dt As DataTable)
        Try
            Dim cnt As Integer
            If dt IsNot Nothing And dt.Rows.Count > 0 Then
                For cnt = 0 To dt.Rows.Count - 1
                    If (dt.Rows(cnt).Item("sSettingsName").ToString().ToUpper = "CHRGAPPT_DEFAULTS_DOS") Then
                        chkApptDOS.Checked = dt.Rows(cnt).Item("sSettingsValue")

                    ElseIf (dt.Rows(cnt).Item("sSettingsName").ToString().ToUpper = "CHRGAPPT_DEFAULTS_FACILITY") Then
                        'RemoveHandler chkApptFacility.CheckedChanged, New System.EventHandler(AddressOf chkApptFacility_CheckedChanged)
                        chkApptFacility.Checked = dt.Rows(cnt).Item("sSettingsValue")
                        ' AddHandler chkApptFacility.CheckedChanged, New System.EventHandler(AddressOf chkApptFacility_CheckedChanged)
                    ElseIf (dt.Rows(cnt).Item("sSettingsName").ToString().ToUpper = "CHRGAPPT_DEFAULTS_RENDERRINGPROVIDER") Then
                        chkApptRenderringProvider.Checked = dt.Rows(cnt).Item("sSettingsValue")

                    End If
                Next
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try


    End Sub

    Private Sub FillFeeSchedule()
        Dim dtStdFeeSchedule As New DataTable()
        cmbFeeSchedule.DataSource = Nothing
        Dim odb As New gloStream.gloDataBase.gloDataBase
        Try

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
            MessageBox.Show(dbEx.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            odb.Disconnect()
            ''dtStdFeeSchedule.Dispose()
        End Try
    End Sub


    Private Sub FillFeeSchedule(ByVal dtStdFeeSchedule As DataTable)
        cmbFeeSchedule.DataSource = Nothing
        Try
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
            MessageBox.Show(dbEx.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

        End Try
    End Sub
    Private Sub FillSkipZeroBillingClaimForERA(ByVal dt As DataTable)

        Try

            If dt IsNot Nothing Then
                If (dt.Rows.Count > 0) Then
                    Dim oResult As Boolean = False
                    Boolean.TryParse(Convert.ToString(dt.Rows(0)("sSettingsValue")), oResult)
                    ChkSkipZeroBillingClaimForERA.Checked = oResult
                Else
                    ChkSkipZeroBillingClaimForERA.Checked = False
                End If
            Else
                ChkSkipZeroBillingClaimForERA.Checked = False
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub FillSkipZeroBillingClaimForIPP(ByVal dt As DataTable)

        Try

            If dt IsNot Nothing Then
                If (dt.Rows.Count > 0) Then
                    Dim oResult As Boolean = False
                    Boolean.TryParse(Convert.ToString(dt.Rows(0)("sSettingsValue")), oResult)
                    chkIppSkippZeroClaimbilling.Checked = oResult
                Else
                    chkIppSkippZeroClaimbilling.Checked = False
                End If
            Else
                chkIppSkippZeroClaimbilling.Checked = False
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub FillShowChargeUnitIP(ByVal dt As DataTable)

        Try

            If dt IsNot Nothing Then
                If (dt.Rows.Count > 0) Then
                    Dim oResult As Boolean = False
                    Boolean.TryParse(Convert.ToString(dt.Rows(0)("sSettingsValue")), oResult)
                    chkIPUnits.Checked = oResult
                Else
                    chkIPUnits.Checked = False
                End If
            Else
                chkIPUnits.Checked = False
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub FillAutoDistributePatientCopay(ByVal dt As DataTable)

        Try

            If dt IsNot Nothing Then
                If (dt.Rows.Count > 0) Then
                    Dim oResult As Boolean = False
                    Boolean.TryParse(Convert.ToString(dt.Rows(0)("sSettingsValue")), oResult)
                    chkDistributeCopay.Checked = oResult
                Else
                    chkDistributeCopay.Checked = False
                End If
            Else
                chkDistributeCopay.Checked = False
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub GetDisplayEMRAlertsSetting(ByVal dt As DataTable)

        Try

            If dt IsNot Nothing Then
                If (dt.Rows.Count > 0) Then
                    Dim oResult As Boolean = False
                    Boolean.TryParse(Convert.ToString(dt.Rows(0)("sSettingsValue")), oResult)
                    chkDisplayEMRAlert.Checked = oResult
                Else
                    chkDisplayEMRAlert.Checked = False
                End If
            Else
                chkDisplayEMRAlert.Checked = False
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub FillExternalCollectionSettings(ByVal dtfeature As DataTable, ByVal dtInsPlan As DataTable, ByVal dtFUAction As DataTable)

        Try

            If dtfeature IsNot Nothing Then
                If (dtfeature.Rows.Count > 0) Then
                    Dim oResult As Boolean = False
                    Boolean.TryParse(Convert.ToString(dtfeature.Rows(0)("sSettingsValue")), oResult)
                    If oResult = True Then
                        rbExternalCollectionYes.Checked = True
                        rbExternalCollectionNo.Checked = False
                    Else
                        rbExternalCollectionYes.Checked = False
                        rbExternalCollectionNo.Checked = True
                    End If
                Else
                    rbExternalCollectionYes.Checked = False
                    rbExternalCollectionNo.Checked = True
                End If
            Else
                rbExternalCollectionYes.Checked = False
                rbExternalCollectionNo.Checked = True
            End If

            If dtInsPlan IsNot Nothing Then
                If (dtInsPlan.Rows.Count > 0) Then
                    Dim oResult As Int64 = False
                    Int64.TryParse(Convert.ToString(dtInsPlan.Rows(0)("sSettingsValue")), oResult)
                    If oResult > 0 Then
                        cmbExternalCollectionInsPlan.SelectedValue = oResult
                    End If
                End If
            End If

            If dtFUAction IsNot Nothing Then
                If (dtFUAction.Rows.Count > 0) Then
                    Dim oResult As String = Convert.ToString(dtFUAction.Rows(0)("sSettingsValue"))
                    If oResult <> "" Then
                        cmbExternalCollectionFUAction.SelectedValue = oResult
                    End If
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub SetEnableSingleSignON(ByVal dt As DataTable)

        Try

            If dt IsNot Nothing Then
                If (dt.Rows.Count > 0) Then
                    ''Dim oResult As Boolean = False
                    '' Boolean.TryParse(Convert.ToString(dt.Rows(0)("sSettingsValue")), oResult)
                    chkEnableSingleSignON.Checked = CType(dt.Rows(0)("sSettingsValue"), Boolean)
                Else
                    chkEnableSingleSignON.Checked = False
                End If
            Else
                chkEnableSingleSignON.Checked = False
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
#End Region
#Region "other billing settings"

    Public Sub FillSpecialities()
        Dim dtSpeciality As New DataTable()
        cmbSlfPayAllwdAmnts.DataSource = Nothing
        Dim odb As New gloStream.gloDataBase.gloDataBase
        Try

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
                cmbSlfPayAllwdAmnts.DataSource = dtSpeciality
                cmbSlfPayAllwdAmnts.DisplayMember = dtSpeciality.Columns("SpecCodeName").ColumnName
                cmbSlfPayAllwdAmnts.ValueMember = dtSpeciality.Columns("sCode").ColumnName
            End If
            cmbSlfPayAllwdAmnts.SelectedIndex = -1
        Catch dbEx As gloDatabaseLayer.DBException
            MessageBox.Show(dbEx.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            odb.Disconnect()
            ''dtSpeciality.Dispose()
        End Try
    End Sub

    Public Sub FillSelfPayAllowedAmounts(ByVal dtSelfPayAllowedAmounts As DataTable)

        cmbSlfPayAllwdAmnts.DataSource = Nothing
        Try
            If dtSelfPayAllowedAmounts IsNot Nothing AndAlso dtSelfPayAllowedAmounts.Rows.Count > 0 Then
                Dim dr As DataRow = dtSelfPayAllowedAmounts.NewRow()
                dr(0) = "0"
                dtSelfPayAllowedAmounts.Rows.InsertAt(dr, 0)
                cmbSlfPayAllwdAmnts.DataSource = dtSelfPayAllowedAmounts
                cmbSlfPayAllwdAmnts.DisplayMember = dtSelfPayAllowedAmounts.Columns("sFeeScheduleName").ColumnName
                cmbSlfPayAllwdAmnts.ValueMember = dtSelfPayAllowedAmounts.Columns("nFeeScheduleID").ColumnName
            End If
            cmbSlfPayAllwdAmnts.SelectedIndex = -1
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

        End Try
    End Sub

    Public Sub FillSelfPayDefaultFeeSchedule(ByVal dtSelfPayDefaultFeeSchedule As DataTable)

        cmb_SlfPayDefaultFeeSchedule.DataSource = Nothing
        Try
            If dtSelfPayDefaultFeeSchedule IsNot Nothing AndAlso dtSelfPayDefaultFeeSchedule.Rows.Count > 0 Then
                Dim dr As DataRow = dtSelfPayDefaultFeeSchedule.NewRow()
                dr(0) = "0"
                dtSelfPayDefaultFeeSchedule.Rows.InsertAt(dr, 0)
                cmb_SlfPayDefaultFeeSchedule.DataSource = dtSelfPayDefaultFeeSchedule
                cmb_SlfPayDefaultFeeSchedule.DisplayMember = dtSelfPayDefaultFeeSchedule.Columns("sFeeScheduleName").ColumnName
                cmb_SlfPayDefaultFeeSchedule.ValueMember = dtSelfPayDefaultFeeSchedule.Columns("nFeeScheduleID").ColumnName
            End If
            cmb_SlfPayDefaultFeeSchedule.SelectedIndex = -1
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

        End Try
    End Sub


    Public Sub GetOtherBillings()
        Dim dtSpeciality As New DataTable()
        cmbSlfPayAllwdAmnts.DataSource = Nothing
        Dim odb As New gloStream.gloDataBase.gloDataBase
        Try

            Dim _sqlQuery As String = "Select sSettingsName,sSettingsValue from dbo.Settings where sSettingsName in('RCODEWRITEOFF','RCODECOPAY','RCODEDEDUCTIBLE','RCODECOINSURANCE','RCODEWITHHOLD')"
            odb.Connect(gstrConnectionString)
            dtSpeciality = odb.ReadQueryData(_sqlQuery)

            If Not dtSpeciality.Rows(0)(1) Is Nothing Then
                cmbWriteOff.SelectedValue = dtSpeciality.Rows(0)(1)
            End If
            If Not dtSpeciality.Rows.Item(1)(1) Is Nothing Then
                cmbCopay.SelectedValue = dtSpeciality.Rows.Item(1)(1)
            End If
            If Not dtSpeciality.Rows.Item(2)(1) Is Nothing Then
                cmbDeductible.SelectedValue = dtSpeciality.Rows.Item(2)(1)
            End If
            If Not dtSpeciality.Rows.Item(3)(1) Is Nothing Then
                cmbCoInsurance.SelectedValue = dtSpeciality.Rows.Item(3)(1)
            End If
            If Not dtSpeciality.Rows.Item(4)(1) Is Nothing Then
                cmbWithHold.SelectedValue = dtSpeciality.Rows.Item(4)(1)
            End If

        Catch dbEx As gloDatabaseLayer.DBException
            MessageBox.Show(dbEx.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            odb.Disconnect()
            ''dtSpeciality.Dispose()
        End Try
    End Sub

    Public Sub GetOtherBillings(ByVal oSettings As clsSettings)
        Try
            cmbWriteOff.SelectedValue = oSettings.WriteOff
            cmbCopay.SelectedValue = oSettings.Copay
            cmbDeductible.SelectedValue = oSettings.Deductible
            cmbCoInsurance.SelectedValue = oSettings.CoInsurance
            cmbWithHold.SelectedValue = oSettings.Withhold

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
        End Try
    End Sub


    Private Sub FillFeeSchedules()
        Dim dtStdFeeSchedule As New DataTable()
        cmb_Feeschedules.DataSource = Nothing
        Dim odb As New gloStream.gloDataBase.gloDataBase
        Try


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
            MessageBox.Show(dbEx.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            odb.Disconnect()
            ''dtStdFeeSchedule.Dispose()
        End Try
    End Sub

    Private Sub FillFeeSchedules(ByVal dtStdFeeSchedule As DataTable)
        cmb_Feeschedules.DataSource = Nothing
        Try
            If dtStdFeeSchedule IsNot Nothing AndAlso dtStdFeeSchedule.Rows.Count > 0 Then
                Dim dr As DataRow = dtStdFeeSchedule.NewRow()
                dr(0) = "0"
                dtStdFeeSchedule.Rows.InsertAt(dr, 0)
                dtStdFeeSchedule.AcceptChanges()
                cmb_Feeschedules.DataSource = dtStdFeeSchedule
                cmb_Feeschedules.DisplayMember = dtStdFeeSchedule.Columns("sFeeScheduleName").ColumnName
                cmb_Feeschedules.ValueMember = dtStdFeeSchedule.Columns("nFeeScheduleID").ColumnName

            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
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

    Private Sub SaveAlphaIIDatabaseSettings()
        Dim _value As Boolean = False
        Try
            Dim ogloSettings As New clsSettings
            ''#Region "Billing Claim Validation Setting" 
            '' If rbCV_Alpha2.Checked = True Then
            '_value = ogloSettings.Add("AlphaII SQL Server Name", txtAlphaIIServerName.Text.Trim(), _ClinicID, 0, SettingFlag.Clinic)
            '_value = ogloSettings.Add("AlphaII Database Name", txtAlphaIIDatabase.Text.Trim(), _ClinicID, 0, SettingFlag.Clinic)
            '_value = ogloSettings.Add("AlphaII Authentication", cmbAlphaIIAuthentication.Text.Trim(), _ClinicID, 0, SettingFlag.Clinic)
            'If cmbAlphaIIAuthentication.Text.Trim() = "SQL" Then ''cmbAuthentication
            '    _value = ogloSettings.Add("AlphaII User Name", txtAlphaIIUserName.Text.Trim(), _ClinicID, 0, SettingFlag.Clinic)
            '    _value = ogloSettings.Add("AlphaII Password", txtAlphaIIPassword.Text.Trim(), _ClinicID, 0, SettingFlag.Clinic)
            'ElseIf cmbAlphaIIAuthentication.Text.Trim() = "Windows" Then ''cmbAuthentication
            '    _value = ogloSettings.Add("AlphaII User Name", "", _ClinicID, 0, SettingFlag.Clinic)
            '    _value = ogloSettings.Add("AlphaII Password", "", _ClinicID, 0, SettingFlag.Clinic)
            'End If

            ''_value = ogloSettings.Add("ClaimValidationSetting", "Alpha2", _ClinicID, 0, SettingFlag.Clinic)
            ' ''Sandip Darade 20091107 
            ' ''server,db info saved in every case 
            'If rbCV_Alpha2.Checked = True Then
            '    _value = ogloSettings.Add("ClaimValidationSetting", "Alpha2", _ClinicID, 0, SettingFlag.Clinic)

            'ElseIf rbCV_YOST.Checked = True Then
            '    '_value = ogloSettings.Add("AlphaII SQL Server Name", "", _ClinicID, 0, SettingFlag.Clinic)
            '    '_value = ogloSettings.Add("AlphaII Database Name", "", _ClinicID, 0, SettingFlag.Clinic)
            '    '_value = ogloSettings.Add("AlphaII Authentication", "", _ClinicID, 0, SettingFlag.Clinic)
            '    '_value = ogloSettings.Add("AlphaII User Name", "", _ClinicID, 0, SettingFlag.Clinic)
            '    '_value = ogloSettings.Add("AlphaII Password", "", _ClinicID, 0, SettingFlag.Clinic)
            '    _value = ogloSettings.Add("ClaimValidationSetting", "YOST", _ClinicID, 0, SettingFlag.Clinic)
            'ElseIf rdbNone.Checked = True Then
            '    '_value = ogloSettings.Add("AlphaII SQL Server Name", "", _ClinicID, 0, SettingFlag.Clinic)
            '    '_value = ogloSettings.Add("AlphaII Database Name", "", _ClinicID, 0, SettingFlag.Clinic)
            '    '_value = ogloSettings.Add("AlphaII Authentication", "", _ClinicID, 0, SettingFlag.Clinic)
            '    '_value = ogloSettings.Add("AlphaII User Name", "", _ClinicID, 0, SettingFlag.Clinic)
            '    '_value = ogloSettings.Add("AlphaII Password", "", _ClinicID, 0, SettingFlag.Clinic)
            '    _value = ogloSettings.Add("ClaimValidationSetting", "None", _ClinicID, 0, SettingFlag.Clinic)
            'End If
            ''#End Region 

            'Save Invalid ICD9 Setting 
            If chkInvalidICD9.Checked = True Then
                _value = ogloSettings.Add("IsCheckInvalidICD9", "True", _ClinicID, 0, SettingFlag.Clinic)
            Else
                _value = ogloSettings.Add("IsCheckInvalidICD9", "False", _ClinicID, 0, SettingFlag.Clinic)
            End If
            'Save scrubber Setting 
            If chkScrubber.Checked = True Then
                _value = ogloSettings.Add("IsUseScrubber", "True", _ClinicID, 0, SettingFlag.Clinic)
            Else
                _value = ogloSettings.Add("IsUseScrubber", "False", _ClinicID, 0, SettingFlag.Clinic)
            End If
            If chkReferralCPT.Checked = True Then
                _value = ogloSettings.Add("IsReferralCPT", "True", _ClinicID, 0, SettingFlag.Clinic)
            Else
                _value = ogloSettings.Add("IsReferralCPT", "False", _ClinicID, 0, SettingFlag.Clinic)
            End If

            'If chkShowMessage.Checked = True Then
            '    _value = ogloSettings.Add("ShowMessageIfNoValidation", "True", _ClinicID, 0, SettingFlag.Clinic)
            'Else
            '    _value = ogloSettings.Add("ShowMessageIfNoValidation", "False", _ClinicID, 0, SettingFlag.Clinic)

            'End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub


    Private Sub SaveAlphaIIDatabaseSettingsNew()
        Dim _value As Boolean = False
        Try
            'Dim ogloSettings As New clsSettings

            '_value = ogloSettings.AddValueInTVP("AlphaII SQL Server Name", txtAlphaIIServerName.Text.Trim(), _ClinicID, 0, SettingFlag.Clinic)
            '_value = ogloSettings.AddValueInTVP("AlphaII Database Name", txtAlphaIIDatabase.Text.Trim(), _ClinicID, 0, SettingFlag.Clinic)
            '_value = ogloSettings.AddValueInTVP("AlphaII Authentication", cmbAlphaIIAuthentication.Text.Trim(), _ClinicID, 0, SettingFlag.Clinic)
            'If cmbAlphaIIAuthentication.Text.Trim() = "SQL" Then ''cmbAuthentication
            '    _value = ogloSettings.AddValueInTVP("AlphaII User Name", txtAlphaIIUserName.Text.Trim(), _ClinicID, 0, SettingFlag.Clinic)
            '    _value = ogloSettings.AddValueInTVP("AlphaII Password", txtAlphaIIPassword.Text.Trim(), _ClinicID, 0, SettingFlag.Clinic)
            'ElseIf cmbAlphaIIAuthentication.Text.Trim() = "Windows" Then ''cmbAuthentication
            '    _value = ogloSettings.AddValueInTVP("AlphaII User Name", "", _ClinicID, 0, SettingFlag.Clinic)
            '    _value = ogloSettings.AddValueInTVP("AlphaII Password", "", _ClinicID, 0, SettingFlag.Clinic)
            'End If

            'If rbCV_Alpha2.Checked = True Then
            '    _value = ogloSettings.AddValueInTVP("ClaimValidationSetting", "Alpha2", _ClinicID, 0, SettingFlag.Clinic)

            'ElseIf rbCV_YOST.Checked = True Then
            '    _value = ogloSettings.AddValueInTVP("ClaimValidationSetting", "YOST", _ClinicID, 0, SettingFlag.Clinic)
            'ElseIf rdbNone.Checked = True Then
            '    _value = ogloSettings.AddValueInTVP("ClaimValidationSetting", "None", _ClinicID, 0, SettingFlag.Clinic)
            'End If

            If chkInvalidICD9.Checked = True Then
                _value = ogloSettings.AddValueInTVP("IsCheckInvalidICD9", "True", _ClinicID, 0, SettingFlag.Clinic)
            Else
                _value = ogloSettings.AddValueInTVP("IsCheckInvalidICD9", "False", _ClinicID, 0, SettingFlag.Clinic)
            End If
            'Save scrubber Setting 
            If chkScrubber.Checked = True Then
                _value = ogloSettings.AddValueInTVP("IsUseScrubber", "True", _ClinicID, 0, SettingFlag.Clinic)
            Else
                _value = ogloSettings.AddValueInTVP("IsUseScrubber", "False", _ClinicID, 0, SettingFlag.Clinic)
            End If
            If chkReferralCPT.Checked = True Then
                _value = ogloSettings.AddValueInTVP("IsReferralCPT", "True", _ClinicID, 0, SettingFlag.Clinic)
            Else
                _value = ogloSettings.AddValueInTVP("IsReferralCPT", "False", _ClinicID, 0, SettingFlag.Clinic)
            End If

            'If chkShowMessage.Checked = True Then
            '    _value = ogloSettings.AddValueInTVP("ShowMessageIfNoValidation", "True", _ClinicID, 0, SettingFlag.Clinic)
            'Else
            '    _value = ogloSettings.AddValueInTVP("ShowMessageIfNoValidation", "False", _ClinicID, 0, SettingFlag.Clinic)

            'End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub


    Private Sub RetrieveAlphaIIDatabaseSettings()
        Try
            ' gloEMR Database Settings 
            Dim ogloSettings As New clsSettings
            Dim value As New Object()
            'ogloSettings.Get_gloEMRSetting("AlphaII SQL Server Name", value)
            'If value IsNot Nothing Then
            '    txtAlphaIIServerName.Text = Convert.ToString(value)
            '    value = Nothing
            'End If

            'ogloSettings.Get_gloEMRSetting("AlphaII Database Name", value)
            'If value IsNot Nothing Then
            '    txtAlphaIIDatabase.Text = Convert.ToString(value)
            '    value = Nothing
            'End If
            'ogloSettings.Get_gloEMRSetting("AlphaII Authentication", value)
            'If value IsNot Nothing Then
            '    If Convert.ToString(value) = "SQL" Then
            '        cmbAlphaIIAuthentication.SelectedIndex = 1
            '        value = Nothing
            '        ogloSettings.Get_gloEMRSetting("AlphaII User Name", value)
            '        If value IsNot Nothing Then
            '            txtAlphaIIUserName.Text = Convert.ToString(value)
            '            value = Nothing
            '        End If
            '        ogloSettings.Get_gloEMRSetting("AlphaII Password", value)
            '        If value IsNot Nothing Then
            '            txtAlphaIIPassword.Text = Convert.ToString(value)
            '            value = Nothing
            '        End If
            '    Else
            '        txtAlphaIIUserName.Text = ""
            '        txtAlphaIIPassword.Text = ""
            '    End If
            'End If

            'ogloSettings.Get_gloEMRSetting("ClaimValidationSetting", value)

            'If value IsNot Nothing AndAlso Convert.ToString(value) <> "" Then
            '    If Convert.ToString(value) = "Alpha2" Then
            '        rbCV_Alpha2.Checked = True
            '    ElseIf Convert.ToString(value) = "YOST" Then
            '        rbCV_YOST.Checked = True
            '    ElseIf Convert.ToString(value) = "None" Then
            '        rdbNone.Checked = True
            '        rdbNone_CheckedChanged(Nothing, Nothing)
            '    End If
            '    value = Nothing
            'End If

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

            'ogloSettings.Get_gloEMRSetting("ShowMessageIfNoValidation", value)
            'If value IsNot Nothing AndAlso Convert.ToString(value) <> "" Then
            '    chkShowMessage.Checked = Convert.ToBoolean(value)
            '    value = Nothing
            'End If
            ogloSettings = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub

    'Private Function ValidateAlphaIISettings() As Boolean
    '    Dim con As New SqlConnection()
    '    If cmbAlphaIIAuthentication.Text = "Windows" AndAlso txtAlphaIIServerName.Text = "" AndAlso txtAlphaIIDatabase.Text = "" Then
    '        Return True
    '    End If
    '    If cmbAlphaIIAuthentication.Text = "SQL" AndAlso txtAlphaIIServerName.Text = "" AndAlso txtAlphaIIDatabase.Text = "" AndAlso txtAlphaIIPassword.Text = "" AndAlso txtAlphaIIUserName.Text = "" Then
    '        Return True
    '    Else
    '        If rbCV_Alpha2.Checked = True Then
    '            con = CreateAlphaIISqlConection()
    '            If con Is Nothing Then
    '                MessageBox.Show("Connection can not be established with given parameters for Alpha II database, please verify parameters. ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
    '                tb_Settings.SelectedTab = tbpg_AlphaIISettings
    '                Return False
    '            End If
    '        End If
    '    End If

    '    Return True
    'End Function

    'Private Function CreateAlphaIISqlConection() As SqlConnection
    '    Dim con As New SqlConnection()
    '    Dim _connstring As String = ""
    '    Try
    '        If txtAlphaIIServerName.Text <> "" AndAlso txtAlphaIIDatabase.Text <> "" Then
    '            If cmbAlphaIIAuthentication.Text = "SQL" Then
    '                'SQL authentication 
    '                'if (txt_EMRDB_Password.Text != "" && txt_EMRDB_UserName.Text != "") 
    '                If True Then
    '                    _connstring = ((("Server=" & txtAlphaIIServerName.Text.ToString() & ";Database=") + txtAlphaIIDatabase.Text.ToString() & ";Uid=") + txtAlphaIIUserName.Text.ToString() & ";Pwd=") + txtAlphaIIPassword.Text.ToString() & ";"
    '                End If
    '            Else
    '                'windows authentication 
    '                _connstring = ("Server=" & txtAlphaIIServerName.Text.ToString() & ";Database=") + txtAlphaIIDatabase.Text.ToString() & ";Trusted_Connection=yes;"
    '            End If
    '            con.ConnectionString = _connstring
    '            con.Open()
    '        Else
    '            con = Nothing
    '        End If
    '    Catch ex As Exception
    '        'MessageBox.Show(" Invalid credentials, Please try again.", _MessageboxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information); 
    '        con = Nothing
    '    Finally
    '        If con IsNot Nothing Then
    '            If con.State = ConnectionState.Open Then
    '                con.Close()
    '            End If
    '        End If
    '    End Try
    '    Return con
    'End Function

#End Region

    Private Sub rbCV_YOST_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If rbCV_YOST.Checked = True Then
            rbCV_YOST.Font = New Font("Tahoma", 9, FontStyle.Bold)
            txtAlphaIIDatabase.Enabled = False
            txtAlphaIIPassword.Enabled = False
            txtAlphaIIServerName.Enabled = False
            txtAlphaIIUserName.Enabled = False
            cmbAlphaIIAuthentication.Enabled = False
        Else
            rbCV_YOST.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If

    End Sub

    Private Sub rbCV_Alpha2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If rbCV_Alpha2.Checked = True Then
            rbCV_Alpha2.Font = New Font("Tahoma", 9, FontStyle.Bold)
            txtAlphaIIDatabase.Enabled = True
            txtAlphaIIPassword.Enabled = True
            txtAlphaIIServerName.Enabled = True
            txtAlphaIIUserName.Enabled = True
            cmbAlphaIIAuthentication.Enabled = True
        Else
            rbCV_Alpha2.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If

    End Sub

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

            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
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

    Private Sub FillPaperBilling()
        Dim conn As New SqlConnection()
        Dim _strSQL As String = ""
        Dim oDB As New gloDatabaseLayer.DBLayer(gloPMAdmin.mdlGeneral.GetConnectionString())
        Dim dt As New DataTable()

        Try


            oDB.Connect(False)

            Dim _sqlQuery As String = "SELECT ISNULL(nID,0) AS nID ,ISNULL(sBox29,'') AS sBox29 ,ISNULL(sBox30,'') AS sBox30 from BL_PaperBillingdefaultSetting WHERE nID<>0"
            oDB.Retrive_Query(_sqlQuery, dt)

            If dt IsNot Nothing Then
                cmbBox29.DataSource = dt.Copy()
                cmbBox29.ValueMember = "nID"
                cmbBox29.DisplayMember = "sBox29"

                ' cmbBox29.SelectedIndex = -1;
                cmbBox29.Refresh()
                cmbBox30.DataSource = dt.Copy()
                cmbBox30.ValueMember = "nID"
                cmbBox30.DisplayMember = "sBox30"

                'cmbBox30.SelectedIndex = -1;
                cmbBox30.Refresh()
            End If


            _sqlQuery = " SELECT ISNULL(nSettingValue,0) AS nSettingValue" & " FROM BL_PaperBillingSetting WHERE nSettingLevel=10 And nSettingType=29 And nClinicID = " & _ClinicID & " order by nSettingType"

            oDB.Retrive_Query(_sqlQuery, dt)
            oDB.Disconnect()

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                cmbBox29.SelectedValue = Convert.ToInt16(dt.Rows(0)("nSettingValue").ToString())
                'cmbFeeSchedules.SelectedValue   
                ' cmbFeeSchedules.Refresh();
                '' cmbBox30.SelectedValue = Convert.ToInt16(dt.Rows(1)("nSettingValue").ToString())
            End If
            _sqlQuery = " SELECT ISNULL(nSettingValue,0) AS nSettingValue" & " FROM BL_PaperBillingSetting WHERE nSettingLevel=10 And nSettingType=30 And nClinicID = " & _ClinicID & "  order by nSettingType"

            oDB.Retrive_Query(_sqlQuery, dt)
            oDB.Disconnect()

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                ''cmbBox29.SelectedValue = Convert.ToInt16(dt.Rows(0)("nSettingValue").ToString())
                'cmbFeeSchedules.SelectedValue   
                ' cmbFeeSchedules.Refresh();
                cmbBox30.SelectedValue = Convert.ToInt16(dt.Rows(0)("nSettingValue").ToString())
            End If
            oDB.Disconnect()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        Finally
            oDB.Dispose()
        End Try

    End Sub

    Private Sub FillPaperBilling(ByVal dt As DataTable, ByVal dtBox29 As DataTable, ByVal dtBox30 As DataTable)
        Try



            If dt IsNot Nothing Then
                cmbBox29.DataSource = dt.Copy()
                cmbBox29.ValueMember = "nID"
                cmbBox29.DisplayMember = "sBox29"

                cmbBox29.Refresh()
                cmbBox30.DataSource = dt.Copy()
                cmbBox30.ValueMember = "nID"
                cmbBox30.DisplayMember = "sBox30"

                cmbBox30.Refresh()
            End If




            If dtBox29 IsNot Nothing AndAlso dtBox29.Rows.Count > 0 Then
                cmbBox29.SelectedValue = Convert.ToInt16(dtBox29.Rows(0)("nSettingValue").ToString())
            End If


            If dtBox30 IsNot Nothing AndAlso dtBox30.Rows.Count > 0 Then
                cmbBox30.SelectedValue = Convert.ToInt16(dtBox30.Rows(0)("nSettingValue").ToString())
            End If


        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        Finally

        End Try

    End Sub

    Private Sub FillDefaultProviderType(ByVal dt As DataTable)
        Try
            cmbDefaultProviderType.DataSource = gloGlobal.gloPMMasters.GetProviderReportingQualifier()
            cmbDefaultProviderType.ValueMember = "sQualifier"
            cmbDefaultProviderType.DisplayMember = "sDescription"
            cmbDefaultProviderType.Refresh()


            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                cmbDefaultProviderType.SelectedValue = Convert.ToString(dt.Rows(0)("sSettingsValue"))
            End If


        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        Finally

        End Try

    End Sub
    Private Sub FillDefaultDateQualifier(ByVal dt As DataTable)
        Try

            cmbDefaultDateQualifier.DataSource = gloGlobal.gloPMMasters.GetClaimDatesQualifiers()
            cmbDefaultDateQualifier.ValueMember = "sQualifier"
            cmbDefaultDateQualifier.DisplayMember = "sDescription"
            cmbDefaultDateQualifier.Refresh()


            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                cmbDefaultDateQualifier.SelectedValue = Convert.ToString(dt.Rows(0)("sSettingsValue"))
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        Finally

        End Try

    End Sub


    Private Sub FillIncludeSatisfiedChargesOnStatement(ByVal dt As DataTable)
        Try


            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                If Convert.ToString(dt.Rows(0)("sSettingsValue")).ToLower() = "true" Then
                    rdoIscYes.Checked = True
                Else
                    rdoIscNo.Checked = True
                End If

            Else
                rdoIscNo.Checked = True

            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

        End Try


    End Sub
#Region " Marital Status Comboboxes Events "

    Private Sub cmbPRMaritalStatus1_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
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

    Private Sub cmbPRMaritalStatus2_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
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

    Private Sub cmbPRMaritalStatus3_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
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

    Private Sub cmbPRMaritalStatus4_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
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

    Private Sub cmbPRMaritalStatus5_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
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

    Private Sub SaveMaritalStatusSettings()
        Dim cls As New clsSettings
        Try
            If cmbPRMaritalStatus1.SelectedIndex <> -1 Then
                cls.Add(Convert.ToString(cmbPRMaritalStatus1.Text), Convert.ToString(cmbBillingMaritalStatus1.Text), _ClinicID, gnLoginID, SettingFlag.Clinic)
            Else
                cls.Add(Convert.ToString(cmbPRMaritalStatus1.Text), "", _ClinicID, gnLoginID, SettingFlag.Clinic)
            End If

            If cmbPRMaritalStatus2.SelectedIndex <> -1 Then
                cls.Add(Convert.ToString(cmbPRMaritalStatus2.Text), Convert.ToString(cmbBillingMaritalStatus2.Text), _ClinicID, gnLoginID, SettingFlag.Clinic)
            Else
                cls.Add(Convert.ToString(cmbPRMaritalStatus2.Text), "", _ClinicID, gnLoginID, SettingFlag.Clinic)
            End If

            If cmbPRMaritalStatus3.SelectedIndex <> -1 Then
                cls.Add(Convert.ToString(cmbPRMaritalStatus3.Text), Convert.ToString(cmbBillingMaritalStatus3.Text), _ClinicID, gnLoginID, SettingFlag.Clinic)
            Else
                cls.Add(Convert.ToString(cmbPRMaritalStatus3.Text), "", _ClinicID, gnLoginID, SettingFlag.Clinic)
            End If

            If cmbPRMaritalStatus4.SelectedIndex <> -1 Then
                cls.Add(Convert.ToString(cmbPRMaritalStatus4.Text), Convert.ToString(cmbBillingMaritalStatus4.Text), _ClinicID, gnLoginID, SettingFlag.Clinic)
            Else
                cls.Add(Convert.ToString(cmbPRMaritalStatus4.Text), "", _ClinicID, gnLoginID, SettingFlag.Clinic)
            End If

            If cmbPRMaritalStatus5.SelectedIndex <> -1 Then
                cls.Add(Convert.ToString(cmbPRMaritalStatus5.Text), Convert.ToString(cmbBillingMaritalStatus5.Text), _ClinicID, gnLoginID, SettingFlag.Clinic)
            Else
                cls.Add(Convert.ToString(cmbPRMaritalStatus5.Text), "", _ClinicID, gnLoginID, SettingFlag.Clinic)

            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub SaveMaritalStatusSettingsNew()

        ''Dim oClssettings As New clsSettings
        Try
            If cmbPRMaritalStatus1.SelectedIndex <> -1 Then
                ogloSettings.AddValueInTVP(Convert.ToString(cmbPRMaritalStatus1.Text), Convert.ToString(cmbBillingMaritalStatus1.Text), _ClinicID, gnLoginID, SettingFlag.Clinic)
            Else
                ogloSettings.AddValueInTVP(Convert.ToString(cmbPRMaritalStatus1.Text), "", _ClinicID, gnLoginID, SettingFlag.Clinic)
            End If

            If cmbPRMaritalStatus2.SelectedIndex <> -1 Then
                ogloSettings.AddValueInTVP(Convert.ToString(cmbPRMaritalStatus2.Text), Convert.ToString(cmbBillingMaritalStatus2.Text), _ClinicID, gnLoginID, SettingFlag.Clinic)
            Else
                ogloSettings.AddValueInTVP(Convert.ToString(cmbPRMaritalStatus2.Text), "", _ClinicID, gnLoginID, SettingFlag.Clinic)
            End If

            If cmbPRMaritalStatus3.SelectedIndex <> -1 Then
                ogloSettings.AddValueInTVP(Convert.ToString(cmbPRMaritalStatus3.Text), Convert.ToString(cmbBillingMaritalStatus3.Text), _ClinicID, gnLoginID, SettingFlag.Clinic)
            Else
                ogloSettings.AddValueInTVP(Convert.ToString(cmbPRMaritalStatus3.Text), "", _ClinicID, gnLoginID, SettingFlag.Clinic)
            End If

            If cmbPRMaritalStatus4.SelectedIndex <> -1 Then
                ogloSettings.AddValueInTVP(Convert.ToString(cmbPRMaritalStatus4.Text), Convert.ToString(cmbBillingMaritalStatus4.Text), _ClinicID, gnLoginID, SettingFlag.Clinic)
            Else
                ogloSettings.AddValueInTVP(Convert.ToString(cmbPRMaritalStatus4.Text), "", _ClinicID, gnLoginID, SettingFlag.Clinic)
            End If

            If cmbPRMaritalStatus5.SelectedIndex <> -1 Then
                ogloSettings.AddValueInTVP(Convert.ToString(cmbPRMaritalStatus5.Text), Convert.ToString(cmbBillingMaritalStatus5.Text), _ClinicID, gnLoginID, SettingFlag.Clinic)
            Else
                ogloSettings.AddValueInTVP(Convert.ToString(cmbPRMaritalStatus5.Text), "", _ClinicID, gnLoginID, SettingFlag.Clinic)

            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub SaveClaimPrefixSettings()
        Try
            Dim value As New Object
            Dim objSetting As New clsSettings
            ogloSettings.AddValueInTVP(Convert.ToString("sClaimPrefix"), Convert.ToString(txtClaimPrefixValue.Text), _ClinicID, gnLoginID, SettingFlag.Clinic)
            objSetting.GetSetting("sClaimPrefix", gnLoginID, _ClinicID, value)
            If txtClaimPrefixValue.Text.Trim <> value Then
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Batch, gloAuditTrail.ActivityCategory.Batch, gloAuditTrail.ActivityType.Save, "ClaimPrefix Changed from '" + value + "' to '" + txtClaimPrefixValue.Text.Trim() + "'", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPMAdmin, True)
            End If

            If chkUseClaim.Checked <> True Then
                ogloSettings.AddValueInTVP(Convert.ToString("bUsePrefixforClaims"), Convert.ToString("False"), _ClinicID, gnLoginID, SettingFlag.Clinic)
            Else
                ogloSettings.AddValueInTVP(Convert.ToString("bUsePrefixforClaims"), Convert.ToString("True"), _ClinicID, gnLoginID, SettingFlag.Clinic)
            End If

            If chkUseBatch.Checked <> True Then
                ogloSettings.AddValueInTVP(Convert.ToString("bUsePrefixforBatch"), Convert.ToString("False"), _ClinicID, gnLoginID, SettingFlag.Clinic)
            Else
                ogloSettings.AddValueInTVP(Convert.ToString("bUsePrefixforBatch"), Convert.ToString("True"), _ClinicID, gnLoginID, SettingFlag.Clinic)
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
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
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub GetClaimPrefixSettings()
        Dim objSetting As New clsSettings
        Dim value As New Object
        Try
            objSetting.GetSetting("sClaimPrefix", gnLoginID, _ClinicID, value)
            If Not IsNothing(value) Then
                If (Convert.ToString(value.ToString().Trim) <> "") Then
                    txtClaimPrefixValue.Text = value.ToString().Trim
                Else
                    Return
                End If
            End If
            value = Nothing

            objSetting.GetSetting("bUsePrefixforClaims", gnLoginID, _ClinicID, value)
            If Not IsNothing(value) Then
                If (Convert.ToString(value.ToString().Trim) <> "True") Then
                    chkUseClaim.Checked = False
                Else
                    chkUseClaim.Checked = True
                End If
            End If
            value = Nothing

            objSetting.GetSetting("bUsePrefixforBatch", gnLoginID, _ClinicID, value)
            If Not IsNothing(value) Then
                If (Convert.ToString(value.ToString().Trim) <> "True") Then
                    chkUseBatch.Checked = False
                Else
                    chkUseBatch.Checked = True
                End If
            End If
            value = Nothing

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub SavePaperBillingSetting()
        Dim oDB As New gloDatabaseLayer.DBLayer(gloPMAdmin.mdlGeneral.GetConnectionString())
        Dim oDBParameters As New gloDatabaseLayer.DBParameters()


        Try
            'If cmbBox29.SelectedIndex = -1 Or cmbBox29.SelectedIndex = 0 Then
            '    Return
            'End If

            oDB.Connect(False)
            ' solving salesforce GLO2010-0007633
            ' string _sqlQuery = " DELETE FROM BL_InsuranceFeeSchedule_Allocation WHERE nFeeScheduleID = " + Convert.ToInt64(cmbFeeSchedules.SelectedValue) + " AND nClinicID = " + _ClinicID;

            Dim _sqlQuery As String = " DELETE FROM BL_PaperBillingSetting WHERE  nSettingLevel=10 And nClinicID = " & _ClinicID
            oDB.Execute_Query(_sqlQuery)

            Try

                If cmbBox29.SelectedIndex = -1 Then
                Else


                    oDBParameters.Add("@nCompanyID", 0, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt)
                    oDBParameters.Add("@nContactID", 0, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt)
                    oDBParameters.Add("@nSettingLevel", PaperBillingSettingLevel.Clinic.GetHashCode(), System.Data.ParameterDirection.Input, System.Data.SqlDbType.Int)
                    oDBParameters.Add("@nSettingType", PaperBillingBoxtype.Box29.GetHashCode(), System.Data.ParameterDirection.Input, System.Data.SqlDbType.Int)
                    oDBParameters.Add("@nSettingValue", Convert.ToInt16(cmbBox29.SelectedValue), System.Data.ParameterDirection.Input, System.Data.SqlDbType.Int)
                    oDBParameters.Add("@nClinicID", Convert.ToInt64(gloSettings.AppSettings.ClinicID), System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt)
                    oDBParameters.Add("@nUserID", Convert.ToInt64(gloSettings.AppSettings.UserID), System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt)
                    oDB.Execute("BL_INUP_PaperBillingSettings", oDBParameters)
                End If
                If cmbBox30.SelectedIndex = -1 Then
                    oDB.Disconnect()
                    Return
                Else
                    oDBParameters.Clear()
                    oDBParameters.Add("@nCompanyID", 0, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt)
                    oDBParameters.Add("@nContactID", 0, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt)
                    oDBParameters.Add("@nSettingLevel", PaperBillingSettingLevel.Clinic.GetHashCode(), System.Data.ParameterDirection.Input, System.Data.SqlDbType.Int)
                    oDBParameters.Add("@nSettingType", PaperBillingBoxtype.Box30.GetHashCode(), System.Data.ParameterDirection.Input, System.Data.SqlDbType.Int)
                    oDBParameters.Add("@nSettingValue", Convert.ToInt16(cmbBox30.SelectedValue), System.Data.ParameterDirection.Input, System.Data.SqlDbType.Int)
                    oDBParameters.Add("@nClinicID", Convert.ToInt64(gloSettings.AppSettings.ClinicID), System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt)
                    oDBParameters.Add("@nUserID", Convert.ToInt64(gloSettings.AppSettings.UserID), System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt)
                    oDB.Execute("BL_INUP_PaperBillingSettings", oDBParameters)
                End If


                oDB.Disconnect()
            Catch dbEx As gloDatabaseLayer.DBException

                dbEx.ERROR_Log(dbEx.ToString())
            Catch ex As Exception

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
            Finally
                oDB.Dispose()
            End Try
        Catch ex As Exception

            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        Finally
            oDB.Dispose()
        End Try
    End Sub

#End Region

#Region "Exchange server Setting"

    Private Sub AddExServerSetting()

        Try
            'Exchange Domain 
            Dim cls As New clsSettings
            cls.Add("ExchangeDomain", txtExchangeDomain.Text.Trim(), _ClinicID, gnLoginID, SettingFlag.Clinic)

            'Exchange URL 
            cls.Add("ExchangeURL", txtExchangeURL.Text.Trim(), _ClinicID, gnLoginID, SettingFlag.Clinic)

            'Exchange TimeZone 
            Dim strExchangeTimeZone As String = ""
            If cmbExchangeTimeZone.SelectedIndex <> -1 Then
                strExchangeTimeZone = cmbExchangeTimeZone.Text.Trim()
            Else
                strExchangeTimeZone = "+"
            End If

            strExchangeTimeZone += Convert.ToString(numExchangeTimeZoneHour.Value).PadLeft(2, "0") & ":"

            strExchangeTimeZone += Convert.ToString(numExchangeTimeZoneMin.Value).PadLeft(2, "0")
            cls.Add("ExchangeTimeZone", strExchangeTimeZone, _ClinicID, gnLoginID, SettingFlag.Clinic)

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub AddExServerSettingNew()

        Try
            'Exchange Domain 
            'Dim cls As New clsSettings
            ogloSettings.AddValueInTVP("ExchangeDomain", txtExchangeDomain.Text.Trim(), _ClinicID, gnLoginID, SettingFlag.Clinic)

            'Exchange URL 
            ogloSettings.AddValueInTVP("ExchangeURL", txtExchangeURL.Text.Trim(), _ClinicID, gnLoginID, SettingFlag.Clinic)

            'Exchange TimeZone 
            Dim strExchangeTimeZone As String = ""
            If cmbExchangeTimeZone.SelectedIndex <> -1 Then
                strExchangeTimeZone = cmbExchangeTimeZone.Text.Trim()
            Else
                strExchangeTimeZone = "+"
            End If

            strExchangeTimeZone += Convert.ToString(numExchangeTimeZoneHour.Value).PadLeft(2, "0") & ":"

            strExchangeTimeZone += Convert.ToString(numExchangeTimeZoneMin.Value).PadLeft(2, "0")
            ogloSettings.AddValueInTVP("ExchangeTimeZone", strExchangeTimeZone, _ClinicID, gnLoginID, SettingFlag.Clinic)

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
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
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

#End Region

    Private Sub AddOtherSettings()
        Try
            ''Add Week Days settings
            Dim objSetting As New clsSettings
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
            objSetting.Add("Week Days", _SettingValue, _ClinicID, gnLoginID, SettingFlag.Clinic)


            ''Add Patient Prefix setting
            objSetting.Add("PatientCodePrefix", txtPatientCodePrefix.Text.Trim(), _ClinicID, gnLoginID, SettingFlag.Clinic)

            ''No Of Appointments Per Slot 
            objSetting.Add("MaxAppointmentsInSlot", numAppointmentsPerSlot.Value.ToString(), _ClinicID, gnLoginID, SettingFlag.Clinic)


            ''Show Allowed Amount
            'objSetting.Add("ShowAllowedAmount", chbox_showAllwdAmount.Checked.ToString(), _ClinicID, 0, SettingFlag.Clinic)
            If (chbox_showAllwdAmount.Checked = True) Then
                objSetting.Add("ShowAllowedAmount", "1", _ClinicID, 0, SettingFlag.Clinic)
            Else
                objSetting.Add("ShowAllowedAmount", "0", _ClinicID, 0, SettingFlag.Clinic)
            End If

            If (chbox_showTotalPayment.Checked = True) Then
                objSetting.Add("ShowTotalPaymentColumn", "1", _ClinicID, 0, SettingFlag.Clinic)
            Else
                objSetting.Add("ShowTotalPaymentColumn", "0", _ClinicID, 0, SettingFlag.Clinic)
            End If

            If (chbox_showPOS.Checked = True) Then
                objSetting.Add("ShowPOSColumn", "1", _ClinicID, 0, SettingFlag.Clinic)
            Else
                objSetting.Add("ShowPOSColumn", "0", _ClinicID, 0, SettingFlag.Clinic)
            End If

            If (chkEMG.Checked = True) Then
                objSetting.Add("ShowEMGColumn", "1", _ClinicID, 0, SettingFlag.Clinic)
            Else
                objSetting.Add("ShowEMGColumn", "0", _ClinicID, 0, SettingFlag.Clinic)
            End If

            If (chkDosTo.Checked = True) Then
                objSetting.Add("ShowTillDateColumn", "1", _ClinicID, 0, SettingFlag.Clinic)
            Else
                objSetting.Add("ShowTillDateColumn", "0", _ClinicID, 0, SettingFlag.Clinic)
            End If

            If (chkShowClaimReportingCategory.Checked = True) Then
                objSetting.Add("ShowClaimReportingCategory", "1", _ClinicID, 0, SettingFlag.Clinic)
            Else
                objSetting.Add("ShowClaimReportingCategory", "0", _ClinicID, 0, SettingFlag.Clinic)
            End If

            Dim _ReportingBasedon As String = ""
            If (Rb_AllowedAmt.Checked = True) Then
                _ReportingBasedon = "1"
            End If
            If (RB_ChargeAmt.Checked = True) Then
                _ReportingBasedon = "0"
            End If
            objSetting.Add("ReportingBasedOn", _ReportingBasedon, _ClinicID, 0, SettingFlag.Clinic)

            objSetting.Add("ServerPath", Convert.ToString(txtServerPath.Text), _ClinicID, gnLoginID, SettingFlag.Clinic)

            objSetting.Add("PatientCodeIncrement", numPatientCodeIncrement.Value.ToString(), _ClinicID, gnLoginID, SettingFlag.Clinic)

            objSetting.Add("Eligibility Request Provider ID", Convert.ToString(txtEligibilityID.Text), _ClinicID, gnLoginID, SettingFlag.Clinic)




        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub AddOtherSettingsNew()
        Try

            'Dim objSetting As New clsSettings
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
            ogloSettings.AddValueInTVP("Week Days", _SettingValue, _ClinicID, 0, SettingFlag.Clinic)

            ogloSettings.AddValueInTVP("PatientCodePrefix", txtPatientCodePrefix.Text.Trim(), _ClinicID, gnLoginID, SettingFlag.Clinic)
            ogloSettings.AddValueInTVP("MaxAppointmentsInSlot", numAppointmentsPerSlot.Value.ToString(), _ClinicID, 0, SettingFlag.Clinic)

            If (chbox_showAllwdAmount.Checked = True) Then
                ogloSettings.AddValueInTVP("ShowAllowedAmount", "1", _ClinicID, 0, SettingFlag.Clinic)
            Else
                ogloSettings.AddValueInTVP("ShowAllowedAmount", "0", _ClinicID, 0, SettingFlag.Clinic)
            End If

            If (chbox_showTotalPayment.Checked = True) Then
                ogloSettings.AddValueInTVP("ShowTotalPaymentColumn", "1", _ClinicID, 0, SettingFlag.Clinic)
            Else
                ogloSettings.AddValueInTVP("ShowTotalPaymentColumn", "0", _ClinicID, 0, SettingFlag.Clinic)
            End If

            If (chbox_showPOS.Checked = True) Then
                ogloSettings.AddValueInTVP("ShowPOSColumn", "1", _ClinicID, 0, SettingFlag.Clinic)
            Else
                ogloSettings.AddValueInTVP("ShowPOSColumn", "0", _ClinicID, 0, SettingFlag.Clinic)
            End If

            If (chkEMG.Checked = True) Then
                ogloSettings.AddValueInTVP("ShowEMGColumn", "1", _ClinicID, 0, SettingFlag.Clinic)
            Else
                ogloSettings.AddValueInTVP("ShowEMGColumn", "0", _ClinicID, 0, SettingFlag.Clinic)
            End If

            If (chkDosTo.Checked = True) Then
                ogloSettings.AddValueInTVP("ShowTillDateColumn", "1", _ClinicID, 0, SettingFlag.Clinic)
            Else
                ogloSettings.AddValueInTVP("ShowTillDateColumn", "0", _ClinicID, 0, SettingFlag.Clinic)
            End If

            If (chkShowClaimReportingCategory.Checked = True) Then
                ogloSettings.AddValueInTVP("ShowClaimReportingCategory", "1", _ClinicID, 0, SettingFlag.Clinic)
            Else
                ogloSettings.AddValueInTVP("ShowClaimReportingCategory", "0", _ClinicID, 0, SettingFlag.Clinic)
            End If

            Dim _ReportingBasedon As String = ""
            If (Rb_AllowedAmt.Checked = True) Then
                _ReportingBasedon = "1"
            End If
            If (RB_ChargeAmt.Checked = True) Then
                _ReportingBasedon = "0"
            End If
            ogloSettings.AddValueInTVP("ReportingBasedOn", _ReportingBasedon, _ClinicID, 0, SettingFlag.Clinic)

            ogloSettings.AddValueInTVP("ServerPath", Convert.ToString(txtServerPath.Text), _ClinicID, gnLoginID, SettingFlag.Clinic)

            ogloSettings.AddValueInTVP("PatientCodeIncrement", numPatientCodeIncrement.Value.ToString(), _ClinicID, gnLoginID, SettingFlag.Clinic)

            ogloSettings.AddValueInTVP("Eligibility Request Provider ID", Convert.ToString(txtEligibilityID.Text), _ClinicID, gnLoginID, SettingFlag.Clinic)




        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
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

            ''ShowPOSColumn
            objSetting.GetSetting("ShowPOSColumn", 0, _ClinicID, value)
            If Not IsNothing(value) Then
                If (Convert.ToString(value.ToString().Trim) <> "") Then
                    chbox_showPOS.Checked = Convert.ToBoolean(Convert.ToInt16(value))
                Else
                    chbox_showPOS.Checked = False
                End If
            Else
                chbox_showPOS.Checked = False
            End If
            value = Nothing

            ''ShowEMGColumn
            objSetting.GetSetting("ShowEMGColumn", 0, _ClinicID, value)
            If Not IsNothing(value) Then
                If (Convert.ToString(value.ToString().Trim) <> "") Then
                    chkEMG.Checked = Convert.ToBoolean(Convert.ToInt16(value))
                Else
                    chkEMG.Checked = False
                End If
            Else
                chkEMG.Checked = False
            End If
            value = Nothing

            ''ShowDosToColumn
            objSetting.GetSetting("ShowTillDateColumn", 0, _ClinicID, value)
            If Not IsNothing(value) Then
                If (Convert.ToString(value.ToString().Trim) <> "") Then
                    chkDosTo.Checked = Convert.ToBoolean(Convert.ToInt16(value))
                Else
                    chkDosTo.Checked = False
                End If
            Else
                chkDosTo.Checked = False
            End If
            value = Nothing

            ''ShowClaimReportingCategory
            objSetting.GetSetting("ShowClaimReportingCategory", 0, _ClinicID, value)
            If Not IsNothing(value) Then
                If (Convert.ToString(value.ToString().Trim) <> "") Then
                    chkShowClaimReportingCategory.Checked = Convert.ToBoolean(Convert.ToInt16(value))
                Else
                    chkShowClaimReportingCategory.Checked = False
                End If
            Else
                chkShowClaimReportingCategory.Checked = False
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

            ''get Server Path
            objSetting.GetSetting("Eligibility Request Provider ID", gnLoginID, _ClinicID, value)
            If Not IsNothing(value) Then
                If (Convert.ToString(value.ToString().Trim) <> "") Then
                    txtEligibilityID.Text = Convert.ToString(value)
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

            objSetting.GetSetting("ReportFolder", gnLoginID, _ClinicID, value)
            If Not IsNothing(value) Then
                If (Convert.ToString(value.ToString().Trim) <> "") Then
                    txtReportFolderName.Text = Convert.ToString(value)
                End If
            End If
            value = Nothing


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
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
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
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
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
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try


    End Sub

    Private Sub GetPaymentSetting(ByVal dt As DataTable)
        trvSelectedRows.Nodes.Clear()
        Dim oNode As TreeNode
        Dim i As Integer
        'Dim dt As New DataTable()

        Dim _InsuranceType As String = ""
        _InsuranceType = Convert.ToString(cmb_InsuranceType.SelectedValue)
        Try
            If (_InsuranceType <> String.Empty) Then
                dt.Select("sInsTypeCode= '" & _InsuranceType & "'")
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

            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
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
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
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


    Private Sub FillInsuranceType(ByVal dtInsuranceType As DataTable)
        cmb_InsuranceType.DataSource = Nothing
        Try

            If dtInsuranceType IsNot Nothing AndAlso dtInsuranceType.Rows.Count > 0 Then

                Dim dr As DataRow
                dr = dtInsuranceType.NewRow()
                dr("sInsuranceType") = ""
                dr("sInsuranceTypeCode") = ""
                dtInsuranceType.Rows.InsertAt(dr, 0)
                cmb_InsuranceType.DataSource = dtInsuranceType
                cmb_InsuranceType.DisplayMember = dtInsuranceType.Columns("sInsuranceType").ColumnName
                cmb_InsuranceType.ValueMember = dtInsuranceType.Columns("sInsuranceTypeCode").ColumnName
                cmb_InsuranceType.SelectedIndex = 0

            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub FillPatientAccSettings(ByVal dtPatientAcc As DataTable)
        Try

            RemoveHandler rbPatientAccountFeatureEnabledYES.CheckedChanged, AddressOf rbPatientAccountFeatureEnabledYES_CheckedChanged
            RemoveHandler rbPatientAccountFeatureEnabledNO.CheckedChanged, AddressOf rbPatientAccountFeatureEnabledNO_CheckedChanged

            blnProcessFlag = True
            Me.rbPatientAccountFeatureEnabledYES.Checked = False
            Me.rbPatientAccountFeatureEnabledNO.Checked = False
            blnProcessFlag = False

            blnMultipleGuarantorsProcessFlag = True
            Me.rbMultipleGuranterAllowYES.Checked = False
            Me.rbMultipleGuranterAllowNO.Checked = False


            If dtPatientAcc IsNot Nothing AndAlso dtPatientAcc.Rows.Count > 0 Then

                Dim _dr() As DataRow

                _dr = dtPatientAcc.Select("sSettingName='Patient Account Feature'")

                If Not IsNothing(_dr) And _dr.Length > 0 Then

                    If (Convert.ToBoolean(_dr(0).Item("sSettingValue"))) Then
                        Me.rbPatientAccountFeatureEnabledYES.Checked = True
                    Else
                        blnProcessFlag = True
                        Me.rbPatientAccountFeatureEnabledNO.Checked = True
                        blnProcessFlag = False
                    End If
                End If
                _dr = Nothing


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
            AddHandler rbPatientAccountFeatureEnabledYES.CheckedChanged, AddressOf rbPatientAccountFeatureEnabledYES_CheckedChanged
            AddHandler rbPatientAccountFeatureEnabledNO.CheckedChanged, AddressOf rbPatientAccountFeatureEnabledNO_CheckedChanged

        End Try


    End Sub


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

    Private Sub RB_ChargeAmt_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If RB_ChargeAmt.Checked = True Then
            RB_ChargeAmt.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            RB_ChargeAmt.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub GroupBox73_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs)

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
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub DesignBillingIDQAProviderSetting()
        Try
            c1IDQFProviderSettings.Rows.Count = 1
            c1IDQFProviderSettings.Cols.Count = COL_QACOUNTS


            c1IDQFProviderSettings.SetData(0, COL_QAPROVIDER_ID, "Provider ID")
            c1IDQFProviderSettings.SetData(0, COL_QAPROVIDERNAME, "Provider Name")
            c1IDQFProviderSettings.SetData(0, COL_FACILITYSOURCE, "Service Facility Source")
            c1IDQFProviderSettings.SetData(0, COL_PROVIDERSOURCE, "Billing Provider Source")


            c1IDQFProviderSettings.Cols(COL_QAPROVIDER_ID).Visible = False
            c1IDQFProviderSettings.Cols(COL_QAPROVIDERNAME).Visible = True
            c1IDQFProviderSettings.Cols(COL_FACILITYSOURCE).Visible = True
            c1IDQFProviderSettings.Cols(COL_PROVIDERSOURCE).Visible = True


            c1IDQFProviderSettings.AllowEditing = True

            c1IDQFProviderSettings.Cols(COL_QAPROVIDER_ID).AllowEditing = False
            c1IDQFProviderSettings.Cols(COL_QAPROVIDERNAME).AllowEditing = False
            c1IDQFProviderSettings.Cols(COL_FACILITYSOURCE).AllowEditing = True
            c1IDQFProviderSettings.Cols(COL_PROVIDERSOURCE).AllowEditing = True


            'c1IDQFProviderSettings.Cols(COL_FACILITYSOURCE).ComboList = " |Billing Provider|Billing Provider Company|Claim Facility|Clinic"
            'c1IDQFProviderSettings.Cols(COL_PROVIDERSOURCE).ComboList = " |Billing Provider|Billing Provider Company|Claim Facility|Clinic"


            Dim _width As Int32 = c1IDQFProviderSettings.Width - 5
            c1IDQFProviderSettings.Cols(COL_QAPROVIDERNAME).Width = Convert.ToInt32(_width * 0.4)
            c1IDQFProviderSettings.Cols(COL_FACILITYSOURCE).Width = Convert.ToInt32(_width * 0.29)
            c1IDQFProviderSettings.Cols(COL_PROVIDERSOURCE).Width = Convert.ToInt32(_width * 0.29)


            'Fill Providers To Grid
            Dim dtProviders As DataTable = GetProvidersForProviderSetting()
            If dtProviders IsNot Nothing AndAlso dtProviders.Rows.Count > 0 Then
                For i As Integer = 0 To dtProviders.Rows.Count - 1
                    c1IDQFProviderSettings.Rows.Add()
                    Dim RowIndex As Int32 = c1IDQFProviderSettings.Rows.Count - 1
                    c1IDQFProviderSettings.SetData(RowIndex, COL_QAPROVIDER_ID, Convert.ToString(dtProviders.Rows(i)("nProviderID")))
                    c1IDQFProviderSettings.SetData(RowIndex, COL_QAPROVIDERNAME, Convert.ToString(dtProviders.Rows(i)("ProviderName")))
                Next
                '----------------
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Function GetProvidersForProviderSetting() As DataTable
        Dim oDB As New gloDatabaseLayer.DBLayer(gloPMAdmin.mdlGeneral.GetConnectionString())
        Dim _strSQL As [String] = ""
        Dim dtProviderType As New DataTable()
        Try
            If Me._ClinicID = 0 Then
                _strSQL = "SELECT nProviderID , dbo.GET_NAME(sFirstName,sMiddleName,sLastName)ProviderName , " _
                          & " ISNULL(ProviderType_MST.sProviderType,'') AS ProviderType FROM Provider_MST LEFT JOIN ProviderType_MST ON Provider_MST.nProviderType = ProviderType_MST.nProviderTypeID FROM Provider_MST ORDER BY ProviderName"
            Else
                _strSQL = "SELECT nProviderID , dbo.GET_NAME(sFirstName,sMiddleName,sLastName)ProviderName , " _
                          & " ISNULL(ProviderType_MST.sProviderType,'') AS ProviderType FROM Provider_MST LEFT JOIN ProviderType_MST ON Provider_MST.nProviderType = ProviderType_MST.nProviderTypeID WHERE nClinicID = " & Me._ClinicID & " ORDER BY ProviderName"
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
            MessageBox.Show(dbex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If oDB IsNot Nothing Then
                oDB.Dispose()
            End If
        End Try
    End Function

    Public Function GetAppointmentTypeForAppointmentDefault() As DataTable
        Dim oDB As New gloDatabaseLayer.DBLayer(gloPMAdmin.mdlGeneral.GetConnectionString())
        Dim _strSQL As [String] = ""
        Dim dtAppointmentType As New DataTable()
        Try

            _strSQL = "  SELECT [nAppointmentTypeID],[sAppointmentType] FROM [AB_AppointmentType] where nAppProcType=1 and bIsBlocked=0 "
            oDB.Connect(False)
            oDB.Retrive_Query(_strSQL, dtAppointmentType)
            oDB.Disconnect()
            If dtAppointmentType IsNot Nothing AndAlso dtAppointmentType.Rows.Count > 0 Then
                Return dtAppointmentType
            Else
                Return Nothing
            End If
        Catch dbex As gloDatabaseLayer.DBException
            dbex.ERROR_Log(dbex.ToString())
            MessageBox.Show(dbex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If oDB IsNot Nothing Then
                oDB.Dispose()
            End If
        End Try
    End Function

    Public Function GetContactsList() As DataTable
        Dim oDB As New gloDatabaseLayer.DBLayer(gloPMAdmin.mdlGeneral.GetConnectionString())
        Dim _strSQL As [String] = ""
        Dim dtContacts As New DataTable()
        Try
            If Me._ClinicID = 0 Then
                _strSQL = "SELECT CM.nContactID,CM.sName FROM  Contacts_MST CM WHERE CM.sContactType = 'Insurance'"
            Else
                _strSQL = "SELECT CM.nContactID,CM.sName FROM  Contacts_MST CM WHERE CM.sContactType = 'Insurance' AND CM.nClinicID = " & Me._ClinicID
            End If

            oDB.Connect(False)
            oDB.Retrive_Query(_strSQL, dtContacts)
            oDB.Disconnect()
            If dtContacts IsNot Nothing AndAlso dtContacts.Rows.Count > 0 Then
                Return dtContacts
            Else
                Return Nothing
            End If
        Catch dbex As gloDatabaseLayer.DBException
            dbex.ERROR_Log(dbex.ToString())
            MessageBox.Show(dbex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If oDB IsNot Nothing Then
                oDB.Dispose()
            End If
        End Try
    End Function

    Public Function GetExpandedClaimSetting() As DataTable
        Dim oDB As New gloDatabaseLayer.DBLayer(gloPMAdmin.mdlGeneral.GetConnectionString())
        Dim _strSQL As [String] = ""
        Dim dtClaimSetting As New DataTable()
        Try

            _strSQL = "select nSettinglevel,nSettingType,nServiceLines,ndiagnosis from BL_ExpandedClaimSettings "
            oDB.Connect(False)
            oDB.Retrive_Query(_strSQL, dtClaimSetting)
            oDB.Disconnect()
            If dtClaimSetting IsNot Nothing AndAlso dtClaimSetting.Rows.Count > 0 Then
                Return dtClaimSetting
            Else
                Return Nothing
            End If
        Catch dbex As gloDatabaseLayer.DBException
            dbex.ERROR_Log(dbex.ToString())
            MessageBox.Show(dbex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
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
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub FillProviderSettings(ByVal dtSubmetter As DataTable, ByVal dtBilling As DataTable, ByVal dtRendering As DataTable)
        Try

            'Dim dtSubmetter As DataTable = GetSettingFromDB("SubmitterSetting")
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

            'Dim dtBilling As DataTable = GetSettingFromDB("BillingSetting")
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

            'Dim dtRendering As DataTable = GetSettingFromDB("RenderingSetting")
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
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub FillMidLevelSettings()
        Dim dtMidlevelSettings As New DataTable()
        CmbMidLevelSettings.DataSource = Nothing
        Dim odb As New gloStream.gloDataBase.gloDataBase
        Try

            Dim _sqlQuery As String = "SELECT nSettingsID,sMidLevelBillingType FROM dbo.BL_MidLevelSettings_MST "
            odb.Connect(gstrConnectionString)
            dtMidlevelSettings = odb.ReadQueryData(_sqlQuery)

            'Dim dr As DataRow
            'dr = dtMidlevelSettings.NewRow()
            'dr(0) = 0
            'dr(1) = " "
            'dtMidlevelSettings.Rows.InsertAt(dr, 0)

            'Dim dr1 As DataRow = dtMidlevelSettings.NewRow()
            'dr1("sMidLevelBillingType") = "0 - All"
            'dr1("nSettingsID") = "0"
            'dtMidlevelSettings.Rows.InsertAt(dr1, 1)
            'dtMidlevelSettings.AcceptChanges()

            If dtMidlevelSettings IsNot Nothing AndAlso dtMidlevelSettings.Rows.Count > 0 Then
                CmbMidLevelSettings.DataSource = dtMidlevelSettings
                CmbMidLevelSettings.DisplayMember = dtMidlevelSettings.Columns("sMidLevelBillingType").ColumnName
                CmbMidLevelSettings.ValueMember = dtMidlevelSettings.Columns("nSettingsID").ColumnName
                CmbMidLevelSettings.SelectedIndex = 1
            End If

        Catch dbEx As gloDatabaseLayer.DBException
            MessageBox.Show(dbEx.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            odb.Disconnect()
        End Try
    End Sub

    Private Sub FillMidLevelSettings(ByVal dtMidlevelSettings As DataTable)

        CmbMidLevelSettings.DataSource = Nothing
        Try
            If dtMidlevelSettings IsNot Nothing AndAlso dtMidlevelSettings.Rows.Count > 0 Then
                CmbMidLevelSettings.DataSource = dtMidlevelSettings
                CmbMidLevelSettings.DisplayMember = dtMidlevelSettings.Columns("sMidLevelBillingType").ColumnName
                CmbMidLevelSettings.ValueMember = dtMidlevelSettings.Columns("nSettingsID").ColumnName
                CmbMidLevelSettings.SelectedIndex = 1
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

        End Try
    End Sub

    Private Sub FillIDQualifiersAssociation(ByVal oComboBox As ComboBox)
        Dim dtIDQualifiersAssociation As DataTable
        Dim oSettings As New gloSettings.GeneralSettings(gloPMAdmin.mdlGeneral.GetConnectionString())

        Try
            dtIDQualifiersAssociation = oSettings.getIDQualifiersAssociation(False, True)


            oComboBox.Items.Clear()
            'Dim dr As DataRow = dtIDQualifiersAssociation.NewRow()
            'dr(0) = "0"
            'dtIDQualifiersAssociation.Rows.InsertAt(dr, 0)

            oComboBox.DataSource = dtIDQualifiersAssociation
            oComboBox.DisplayMember = "sAdditionalDescription"
            oComboBox.ValueMember = "nQualifierID"
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub FillServiceFacility()
        Dim dtSources As New DataTable
        Dim oSettings As New gloSettings.GeneralSettings(gstrConnectionString)
        Dim sSourceComboString As String = ""

        Try
            dtSources = oSettings.getSources(False)

            If dtSources.Rows.Count > 0 AndAlso Not IsNothing(dtSources) Then

                cmbServiceFacilitySource.DataSource = dtSources.Copy()
                cmbServiceFacilitySource.DisplayMember = "sDescription"
                cmbServiceFacilitySource.ValueMember = "nID"
                cmbServiceFacilitySource.Refresh()

                cmbBillingProvSource.DataSource = dtSources.Copy()
                cmbBillingProvSource.DisplayMember = "sDescription"
                cmbBillingProvSource.ValueMember = "nID"
                cmbServiceFacilitySource.Refresh()

                htSourceSettings = New Hashtable()

                For iCount As Integer = 0 To dtSources.Rows.Count - 1

                    htSourceSettings.Add(Convert.ToString(dtSources.Rows(iCount)("sDescription")), Convert.ToString(dtSources.Rows(iCount)("nID")))

                    If sSourceComboString <> String.Empty Then
                        sSourceComboString += "||" & Convert.ToString(dtSources.Rows(iCount)("sDescription"))
                    Else
                        sSourceComboString = " " & "||" & Convert.ToString(dtSources.Rows(iCount)("sDescription"))
                    End If

                Next

                c1IDQFProviderSettings.Cols(COL_FACILITYSOURCE).ComboList = sSourceComboString
                c1IDQFProviderSettings.Cols(COL_PROVIDERSOURCE).ComboList = sSourceComboString

            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            dtSources.Dispose()
        End Try
    End Sub

    Private Sub FillServiceFacilityForSubmitter(ByVal oComboBox As ComboBox)

        Try

            oComboBox.Items.Clear()
            oComboBox.Items.Add("")
            oComboBox.Items.Add("Billing Provider")
            oComboBox.Items.Add("Clinic")

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' To Get the Value of the Setting from The Database
    ''' </summary>
    ''' <param name="SettingName"> Name of Setting</param>
    ''' <param name="Value"> Gets the Value of Respective Setting if Exits else set to 'null'</param>
    Private Function GetSettingFromDB(ByVal SettingName As String) As DataTable
        Dim Value As DataTable
        Dim oDB As New gloDatabaseLayer.DBLayer(gloPMAdmin.mdlGeneral.GetConnectionString())
        Dim oDBParameters As New gloDatabaseLayer.DBParameters()
        Try
            oDB.Connect(False)
            Value = New DataTable()
            oDB.Retrive_Query("SELECT ISNULL(sSettingsValue,'') AS sSettingsValue, ISNULL(nUserID,0) AS nUserID FROM Settings WHERE sSettingsName = '" & SettingName & "' AND nClinicID = " & _ClinicID & "", Value)

            Return Value
        Catch DBErr As gloDatabaseLayer.DBException
            Value = Nothing
            DBErr.ERROR_Log(DBErr.Message)
            MessageBox.Show(DBErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            Value = Nothing
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            oDB.Disconnect()
            oDBParameters.Dispose()
            oDB.Dispose()
        End Try

        Return Value
    End Function

    Public Function AddSettingToDB(ByVal Name As String, ByVal Value As String, ByVal ClinicID As Int64, ByVal UserID As Int64, ByVal UserClinicFlag As SettingFlag) As Boolean
        Dim oDB As New gloDatabaseLayer.DBLayer(gloPMAdmin.mdlGeneral.GetConnectionString())
        Dim oDBParameters As New gloDatabaseLayer.DBParameters()
        Try
            oDB.Connect(False)

            oDBParameters.Add("@sSettingsName", Name, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar)
            oDBParameters.Add("@sSettingsValue", Value, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar)
            oDBParameters.Add("@nClinicID", ClinicID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar)
            oDBParameters.Add("@nUserID", UserID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar)
            oDBParameters.Add("@nUserClinicFlag", UserClinicFlag.GetHashCode(), System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar)

            oDB.Execute("gsp_InUpSettings", oDBParameters)

            Return True
        Catch DBErr As gloDatabaseLayer.DBException
            MessageBox.Show(DBErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        Finally
            oDB.Disconnect()
            oDBParameters.Dispose()
            oDB.Dispose()
        End Try
    End Function

    'Changes Made by Subashish date 03-01-2011---------------------start---------Change No SB0001---------
    'For adding the Patient Account Setting

    Private Sub SavePatientAccountSetting()
        Try

            Dim patientAccountSetting As String = ""
            Dim multipleGuaranterSeeting As String = ""
            Dim copyAccountSetting As String = ""
            'Added By Mahesh S(Apollo) on 1-Feb-2011 For MergeAccount
            Dim mergeAccountSetting As String = ""

            If rbPatientAccountFeatureEnabledYES.Checked Then
                patientAccountSetting = "True"
            ElseIf rbPatientAccountFeatureEnabledNO.Checked Then
                patientAccountSetting = "False"
            End If

            If rbMultipleGuranterAllowYES.Checked Then
                multipleGuaranterSeeting = "True"
            ElseIf rbMultipleGuranterAllowNO.Checked Then
                multipleGuaranterSeeting = "False"
            End If

            If rbCopyAccountYES.Checked Then
                copyAccountSetting = "True"
            ElseIf rbCopyAccountNo.Checked Then
                copyAccountSetting = "False"
            End If

            If rbMergeAccountYes.Checked Then
                mergeAccountSetting = "True"
            ElseIf rbMergeAccountNo.Checked Then
                mergeAccountSetting = "False"
            End If


            Dim nProviderID As Int64 = 0


            If rbPatientAccountFeatureEnabledNO.Checked = True Then
                If (Not Validate_PAF()) Then
                    'Update the changes made in the setting for Patient Account
                    AddSettingToDB("Patient Account Feature", patientAccountSetting, _ClinicID, gnLoginID, SettingFlag.Clinic)

                End If
            Else
                AddSettingToDB("Patient Account Feature", patientAccountSetting, _ClinicID, gnLoginID, SettingFlag.Clinic)
            End If


            'Update the changes made in the setting for Patient Account
            AddSettingToDB("Allow Multiple Guarantor", multipleGuaranterSeeting, _ClinicID, gnLoginID, SettingFlag.Clinic)

            ''Update the changes made in the setting for Copy Account
            'AddSettingToDB("Copy Account", copyAccountSetting, _ClinicID, gnLoginID, SettingFlag.Clinic)

            ''Addded By Mahesh S(Apollo)
            ''Update the changes made in the setting for Merge Account
            'AddSettingToDB("Merge Account", mergeAccountSetting, _ClinicID, gnLoginID, SettingFlag.Clinic)


        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub SavePatientAccountSettingNew()
        Try
            'Dim ogloSettings As New clsSettings

            Dim patientAccountSetting As String = ""
            Dim multipleGuaranterSeeting As String = ""
            Dim copyAccountSetting As String = ""
            Dim mergeAccountSetting As String = ""

            If rbPatientAccountFeatureEnabledYES.Checked Then
                patientAccountSetting = "True"
            ElseIf rbPatientAccountFeatureEnabledNO.Checked Then
                patientAccountSetting = "False"
            End If

            If rbMultipleGuranterAllowYES.Checked Then
                multipleGuaranterSeeting = "True"
            ElseIf rbMultipleGuranterAllowNO.Checked Then
                multipleGuaranterSeeting = "False"
            End If

            If rbCopyAccountYES.Checked Then
                copyAccountSetting = "True"
            ElseIf rbCopyAccountNo.Checked Then
                copyAccountSetting = "False"
            End If

            If rbMergeAccountYes.Checked Then
                mergeAccountSetting = "True"
            ElseIf rbMergeAccountNo.Checked Then
                mergeAccountSetting = "False"
            End If


            Dim nProviderID As Int64 = 0


            If rbPatientAccountFeatureEnabledNO.Checked = True Then
                If (Not Validate_PAF()) Then
                    'Update the changes made in the setting for Patient Account
                    ogloSettings.AddPatientAccInTVP("Patient Account Feature", patientAccountSetting, _ClinicID, gnLoginID, SettingFlag.Clinic)
                End If
            Else
                ogloSettings.AddPatientAccInTVP("Patient Account Feature", patientAccountSetting, _ClinicID, gnLoginID, SettingFlag.Clinic)
            End If


            'Update the changes made in the setting for Patient Account
            ogloSettings.AddPatientAccInTVP("Allow Multiple Guarantor", multipleGuaranterSeeting, _ClinicID, gnLoginID, SettingFlag.Clinic)

            ''Update the changes made in the setting for Copy Account
            'ogloSettings.AddValueInTVP("Copy Account", copyAccountSetting, _ClinicID, gnLoginID, SettingFlag.Clinic)


            ''Update the changes made in the setting for Merge Account
            'ogloSettings.AddValueInTVP("Merge Account", mergeAccountSetting, _ClinicID, gnLoginID, SettingFlag.Clinic)


        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    'Changes Made by Subashish date 03-01-2011---------------------start------------------

    Private Function Validate_PAF() As Boolean
        Dim _sqlQuery As String = String.Empty
        Dim oDB As New gloDatabaseLayer.DBLayer(gloPMAdmin.mdlGeneral.GetConnectionString())
        Dim dt As New DataTable()
        Dim blnFlag As Boolean
        Try

            oDB.Connect(False)

            _sqlQuery = "Select top 1 Count(nPAccountID), nPatientID as ID from PA_Accounts_Patients Group by nPatientID Having(Count(nPAccountID) > 1)"
            _sqlQuery = _sqlQuery + " Union Select top 1 Count(nPatientID), nPAccountID as ID from PA_Accounts_Patients Group By nPAccountID Having(Count(nPatientID) > 1)"

            oDB.Retrive_Query(_sqlQuery, dt)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                MessageBox.Show("Application contains patient with multiple accounts or account with multiple patients. Cannot Turn Off Patient Account Feature.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                blnFlag = True
            ElseIf (isSaveAndClose = False) Then
                Dim res As DialogResult = MessageBox.Show("Warning – After turning on or off the Patient Account Feature, you must close each running gloEMR and gloPM desktop for all users.  Leaving a desktop running might cause conflicts.  Continue?", gstrMessageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning)
                If res = Windows.Forms.DialogResult.OK Then
                    rbPatientAccountFeatureEnabledNO.Checked = True
                    rbPatientAccountFeatureEnabledYES.Checked = False
                ElseIf res = Windows.Forms.DialogResult.Cancel Then
                    rbPatientAccountFeatureEnabledNO.Checked = False
                    rbPatientAccountFeatureEnabledYES.Checked = True
                End If
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Return blnFlag

    End Function

    Private Function SaveProviderSetting()
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
                AddSettingToDB("SubmitterSetting", sSubmitter, _ClinicID, nProviderID, SettingFlag.User)
                AddSettingToDB("BillingSetting", sBilling, _ClinicID, nProviderID, SettingFlag.User)
                AddSettingToDB("RenderingSetting", sRendering, _ClinicID, nProviderID, SettingFlag.User)

            Next
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Function

    Private Function SaveProviderSettingNew() As Boolean
        Try
            'Dim ogloSettings As New clsSettings
            For i As Integer = 1 To c1Providers.Rows.Count - 1
                Dim sSubmitter As String = ""
                Dim sRendering As String = ""
                Dim sBilling As String = ""

                Dim nProviderID As Int64 = 0
                sSubmitter = Convert.ToString(c1Providers.GetData(i, COL_SUBMITTER))
                sRendering = Convert.ToString(c1Providers.GetData(i, COL_RENDERING))

                If (c1Providers.GetData(i, COL_BILLING) <> Nothing) Then
                    sBilling = Convert.ToString(c1Providers.GetData(i, COL_BILLING))
                End If

                If (c1Providers.GetData(i, COL_PROVIDER_ID) <> Nothing) Then
                    nProviderID = Convert.ToInt64(c1Providers.GetData(i, COL_PROVIDER_ID))
                    ogloSettings.AddValueInTVP("SubmitterSetting", sSubmitter, _ClinicID, nProviderID, SettingFlag.User)
                    ogloSettings.AddValueInTVP("BillingSetting", sBilling, _ClinicID, nProviderID, SettingFlag.User)
                    ogloSettings.AddValueInTVP("RenderingSetting", sRendering, _ClinicID, nProviderID, SettingFlag.User)
                End If

            Next
            Return True
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function


    Private Function SaveANSIVersions()

        Try
            Dim oANSIversion As New CLS_ANSIversion


            ''If Not cmbANSIClaimSettings.Text.Trim() = Nothing Or Not cmbANSIEligiblitySettings.Text.Trim() = Nothing Then

            oANSIversion.ID = 0
            oANSIversion.ClinicID = _ClinicID
            oANSIversion.ContactID = 0
            oANSIversion.CreatedDate = DateTime.Now
            oANSIversion.ModifiedDate = DateTime.Now
            oANSIversion.ClaimVersion = cmbANSIClaimSettings.SelectedValue
            oANSIversion.EligVersion = cmbANSIEligiblitySettings.SelectedValue
            oANSIversion.ClaimVersionName = cmbANSIClaimSettings.Text
            oANSIversion.EligVersionName = cmbANSIEligiblitySettings.Text
            oANSIversion.SaveRecords()

            ''End If


            For index As Integer = 1 To C1ANSIVersionSettings.Rows.Count - 1

                ''If Not Convert.ToString(C1ANSIVersionSettings.GetData(index, ANSIGrid.ClaimBatchSettings)).Trim() = Nothing Or Not Convert.ToString(C1ANSIVersionSettings.GetData(index, ANSIGrid.EligiblityRequestSettings)).Trim() = Nothing Then

                oANSIversion.ID = 0
                oANSIversion.ClinicID = _ClinicID
                oANSIversion.ContactID = Convert.ToInt64(C1ANSIVersionSettings.GetData(index, ANSIGrid.ContactID))
                oANSIversion.CreatedDate = DateTime.Now
                oANSIversion.ModifiedDate = DateTime.Now
                oANSIversion.ClaimVersion = htANSI(Convert.ToString(C1ANSIVersionSettings.GetData(index, ANSIGrid.ClaimBatchSettings)).Trim())
                oANSIversion.EligVersion = htANSI(Convert.ToString(C1ANSIVersionSettings.GetData(index, ANSIGrid.EligiblityRequestSettings)).Trim())
                oANSIversion.ClaimVersionName = Convert.ToString(C1ANSIVersionSettings.GetData(index, ANSIGrid.ClaimBatchSettings)).Trim()
                oANSIversion.EligVersionName = Convert.ToString(C1ANSIVersionSettings.GetData(index, ANSIGrid.EligiblityRequestSettings)).Trim()
                oANSIversion.SaveRecords()

                ''End If

            Next

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Function

    Private Function SavePaperFormVersions()

        Try
            Dim oANSIversion As New CLS_ANSIversion


            ''If Not cmbANSIClaimSettings.Text.Trim() = Nothing Or Not cmbANSIEligiblitySettings.Text.Trim() = Nothing Then

            oANSIversion.ID = 0
            oANSIversion.ClinicID = _ClinicID
            oANSIversion.ContactID = 0
            oANSIversion.CreatedDate = DateTime.Now
            oANSIversion.ModifiedDate = DateTime.Now
            oANSIversion.PaperClaimVersion = cmbPaperVersion.SelectedValue
            oANSIversion.PaperClaimVersionName = cmbPaperVersion.Text
            oANSIversion.SavePaperClaimSettings()

            ''End If


            For index As Integer = 1 To c1CMSVersionPlanSetup.Rows.Count - 1

                ''If Not Convert.ToString(c1CMSVersionPlanSetup.GetData(index, ANSIGrid.ClaimBatchSettings)).Trim() = Nothing Or Not Convert.ToString(c1CMSVersionPlanSetup.GetData(index, ANSIGrid.EligiblityRequestSettings)).Trim() = Nothing Then

                oANSIversion.ID = 0
                oANSIversion.ClinicID = _ClinicID
                oANSIversion.ContactID = Convert.ToInt64(c1CMSVersionPlanSetup.GetData(index, ANSIGrid.ContactID))
                oANSIversion.CreatedDate = DateTime.Now
                oANSIversion.ModifiedDate = DateTime.Now
                oANSIversion.PaperClaimVersion = htPaperFormFormat(Convert.ToString(c1CMSVersionPlanSetup.GetData(index, ANSIGrid.ClaimBatchSettings)).Trim())
                oANSIversion.PaperClaimVersionName = Convert.ToString(c1CMSVersionPlanSetup.GetData(index, ANSIGrid.ClaimBatchSettings)).Trim()
                oANSIversion.SavePaperClaimSettings()

                ''End If

            Next

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Function

    Private Function SaveBillingIDQualifier()

        Dim oDB As New gloDatabaseLayer.DBLayer(gloPMAdmin.mdlGeneral.GetConnectionString())
        Dim oSettings As New gloSettings.GeneralSettings(gloPMAdmin.mdlGeneral.GetConnectionString())
        Dim _sFacilitySource As String
        Dim _sProviderSource As String
        Dim _nProviderID As Int64


        Try

            oDB.Connect(False)
            oDB.Execute_Query("DELETE FROM BL_AlternateID_Settings WHERE nLevel IN(" & gloSettings.AlternateIDSettingLevel.Clinic.GetHashCode() & "," & gloSettings.AlternateIDSettingLevel.Clinic.GetHashCode() & ")  AND  nclinicID= " & _ClinicID)


            If (cmbPaperRendering.SelectedIndex > 0) Then
                oSettings.SaveBillingQualifierIDSettings("Paper Rendering Provider ID Type", Convert.ToInt64(cmbPaperRendering.SelectedValue), Convert.ToString(cmbPaperRendering.Text), False, "", False, gloSettings.AlternateIDSettingLevel.Clinic, 0, 0, _ClinicID)
            End If

            If (cmbElectronicRendering.SelectedIndex > 0) Then
                oSettings.SaveBillingQualifierIDSettings("Electronic Rendering Provider ID Type", Convert.ToInt64(cmbElectronicRendering.SelectedValue), Convert.ToString(cmbElectronicRendering.Text), False, "", False, gloSettings.AlternateIDSettingLevel.Clinic, 0, _nProviderID, _ClinicID)
            End If

            If (cmbServiceFacilitySource.SelectedIndex > 0) Then
                oSettings.SaveBillingQualifierIDSettings("Service Facility Source", Convert.ToInt64(cmbServiceFacilitySource.SelectedValue), Convert.ToString(cmbServiceFacilitySource.Text), False, "", False, gloSettings.AlternateIDSettingLevel.Clinic, 0, 0, _ClinicID)
            End If

            If (cmbBillingProvSource.SelectedIndex > 0) Then
                oSettings.SaveBillingQualifierIDSettings("Billing Provider Source", Convert.ToInt64(cmbBillingProvSource.SelectedValue), Convert.ToString(cmbBillingProvSource.Text), False, "", False, gloSettings.AlternateIDSettingLevel.Clinic, 0, 0, _ClinicID)
            End If

            If (cmbSubmitter.SelectedIndex > 0) Then
                oSettings.SaveBillingQualifierIDSettings("Submitter", 0, Convert.ToString(cmbSubmitter.Text), False, "", False, gloSettings.AlternateIDSettingLevel.Clinic, 0, 0, _ClinicID)
            End If

            'If (txtEligReqProvID.Text <> "") Then
            '    oSettings.SaveBillingQualifierIDSettings("Eligibility Request Provider ID", 0, Convert.ToString(txtEligReqProvID.Text), False, "", False, gloSettings.AlternateIDSettingLevel.Clinic, 0, 0, _ClinicID)
            'End If

            oDB.Execute_Query("Delete from BL_AlternateID_Settings where nLevel=" & gloSettings.AlternateIDSettingLevel.ClinicProvider.GetHashCode() & "  AND  nclinicID= " & _ClinicID & "")


            For i As Integer = 1 To c1IDQFProviderSettings.Rows.Count - 1

                _sFacilitySource = ""
                _sProviderSource = ""
                _sFacilitySource = Convert.ToString(c1IDQFProviderSettings.GetData(i, COL_FACILITYSOURCE)).Trim()
                _sProviderSource = Convert.ToString(c1IDQFProviderSettings.GetData(i, COL_PROVIDERSOURCE)).Trim()
                _nProviderID = Convert.ToString(c1IDQFProviderSettings.GetData(i, COL_QAPROVIDER_ID))

                If (_sFacilitySource <> "") Then
                    oSettings.SaveBillingQualifierIDSettings("Service Facility Source", Convert.ToInt64(htSourceSettings(_sFacilitySource)), _sFacilitySource, False, "", False, gloSettings.AlternateIDSettingLevel.ClinicProvider, 0, _nProviderID, _ClinicID)
                End If

                If (_sProviderSource <> "") Then
                    oSettings.SaveBillingQualifierIDSettings("Billing Provider Source", Convert.ToInt64(htSourceSettings(_sProviderSource)), _sProviderSource, False, "", False, gloSettings.AlternateIDSettingLevel.ClinicProvider, 0, _nProviderID, _ClinicID)
                End If

            Next
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Function

    Private Sub FillBilingIDQualifier()

        Dim oDB As New gloDatabaseLayer.DBLayer(gloPMAdmin.mdlGeneral.GetConnectionString())
        Dim dtAlternateIDSettings As New DataTable()

        Dim _sqlQuery As String = String.Empty
        Dim iRow As Integer = 0
        Try

            oDB.Connect(False)

            _sqlQuery = " select * from BL_AlternateID_Settings WHERE nclinicID = " & _ClinicID & " AND (nLevel=20 or nLevel=10) "

            oDB.Retrive_Query(_sqlQuery, dtAlternateIDSettings)

            If dtAlternateIDSettings IsNot Nothing AndAlso dtAlternateIDSettings.Rows.Count > 0 Then

                Dim oRow As DataRow() = dtAlternateIDSettings.[Select]("sSettingName = 'Paper Rendering Provider ID Type' AND nLevel=10")
                If oRow.Length > 0 Then
                    cmbPaperRendering.SelectedValue = oRow(0)("nQualifierID")
                    oRow = Nothing
                End If

                oRow = dtAlternateIDSettings.[Select]("sSettingName = 'Electronic Rendering Provider ID Type' AND nLevel=10")
                If oRow.Length > 0 Then
                    cmbElectronicRendering.SelectedValue = oRow(0)("nQualifierID")
                    oRow = Nothing
                End If

                oRow = dtAlternateIDSettings.[Select]("sSettingName = 'Service Facility Source' AND nLevel=10")
                If oRow.Length > 0 Then
                    cmbServiceFacilitySource.SelectedValue = oRow(0)("nQualifierID")
                    oRow = Nothing
                End If

                oRow = dtAlternateIDSettings.[Select]("sSettingName = 'Billing Provider Source' AND nLevel=10")
                If oRow.Length > 0 Then
                    cmbBillingProvSource.SelectedValue = oRow(0)("nQualifierID")
                    oRow = Nothing
                End If

                oRow = dtAlternateIDSettings.[Select]("sSettingName = 'Submitter' AND nLevel=10")
                If oRow.Length > 0 Then
                    cmbSubmitter.SelectedItem = oRow(0)("sValue")
                    oRow = Nothing
                End If

                'oRow = dtAlternateIDSettings.[Select]("sSettingName = 'Eligibility Request Provider ID' AND nLevel=10")
                'If oRow.Length > 0 Then
                '    txtEligReqProvID.Text = oRow(0)("sValue")
                '    oRow = Nothing
                'End If



                oRow = dtAlternateIDSettings.[Select]("nLevel =20 AND nClinicID= " & _ClinicID & "")

                For Each dr As DataRow In oRow
                    iRow = c1IDQFProviderSettings.FindRow(Convert.ToString(dr("nProviderID")), 0, CInt(BillingGridColumn.ProviderID), True)

                    If iRow > 0 Then
                        If Convert.ToString(dr("sSettingName")) = "Billing Provider Source" Then
                            c1IDQFProviderSettings.SetData(iRow, CInt(BillingGridColumn.BillingProviderSource), Convert.ToString(dr("sValue")))
                        ElseIf Convert.ToString(dr("sSettingName")) = "Service Facility Source" Then
                            c1IDQFProviderSettings.SetData(iRow, CInt(BillingGridColumn.ServiceFacilitySource), Convert.ToString(dr("sValue")))
                        End If

                    End If

                Next
            End If
        Catch ex As gloDatabaseLayer.DBException
            ex.ERROR_Log(ex.ToString())
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        Finally
            oDB.Disconnect()
            oDB.Dispose()
        End Try


    End Sub

    Private Sub FillBilingIDQualifier(ByVal dtAlternateIDSettings As DataTable)


        Dim iRow As Integer = 0
        Try

            If dtAlternateIDSettings IsNot Nothing AndAlso dtAlternateIDSettings.Rows.Count > 0 Then

                Dim oRow As DataRow() = dtAlternateIDSettings.[Select]("sSettingName = 'Paper Rendering Provider ID Type' AND nLevel=10")
                If oRow.Length > 0 Then
                    cmbPaperRendering.SelectedValue = oRow(0)("nQualifierID")
                    oRow = Nothing
                End If

                oRow = dtAlternateIDSettings.[Select]("sSettingName = 'Electronic Rendering Provider ID Type' AND nLevel=10")
                If oRow.Length > 0 Then
                    cmbElectronicRendering.SelectedValue = oRow(0)("nQualifierID")
                    oRow = Nothing
                End If

                oRow = dtAlternateIDSettings.[Select]("sSettingName = 'Service Facility Source' AND nLevel=10")
                If oRow.Length > 0 Then
                    cmbServiceFacilitySource.SelectedValue = oRow(0)("nQualifierID")
                    oRow = Nothing
                End If

                oRow = dtAlternateIDSettings.[Select]("sSettingName = 'Billing Provider Source' AND nLevel=10")
                If oRow.Length > 0 Then
                    cmbBillingProvSource.SelectedValue = oRow(0)("nQualifierID")
                    oRow = Nothing
                End If

                oRow = dtAlternateIDSettings.[Select]("sSettingName = 'Submitter' AND nLevel=10")
                If oRow.Length > 0 Then
                    cmbSubmitter.SelectedItem = oRow(0)("sValue")
                    oRow = Nothing
                End If




                oRow = dtAlternateIDSettings.[Select]("nLevel =20 AND nClinicID= " & _ClinicID & "")

                For Each dr As DataRow In oRow
                    iRow = c1IDQFProviderSettings.FindRow(Convert.ToString(dr("nProviderID")), 0, CInt(BillingGridColumn.ProviderID), True)

                    If iRow > 0 Then
                        If Convert.ToString(dr("sSettingName")) = "Billing Provider Source" Then
                            c1IDQFProviderSettings.SetData(iRow, CInt(BillingGridColumn.BillingProviderSource), Convert.ToString(dr("sValue")))
                        ElseIf Convert.ToString(dr("sSettingName")) = "Service Facility Source" Then
                            c1IDQFProviderSettings.SetData(iRow, CInt(BillingGridColumn.ServiceFacilitySource), Convert.ToString(dr("sValue")))
                        End If

                    End If

                Next
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        Finally

        End Try


    End Sub


#End Region


    Private Sub FillAppointmentTypeForDefaultAppointment(ByVal dtAppointmentType As DataTable)
        Try
            If (dtAppointmentType IsNot Nothing AndAlso dtAppointmentType.Rows.Count > 0) Then
                C1AppointmentDefault.DataSource = dtAppointmentType
            End If
            DesignGridForAppointmentDefault()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        Finally
        End Try
    End Sub

#Region " Growth Chart Percentile Design "
    Private Sub chk_GrowthChart_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        grbGrowthChartPercentile.Enabled = chk_GrowthChart.Checked
    End Sub

    Private Sub rbShowPercentile_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        If rbShowPercentile.Checked Then
            rbShowPercentile.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            rbShowPercentile.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub rbShowPercentileOnMouseHoover_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        If rbShowPercentileOnMouseHoover.Checked Then
            rbShowPercentileOnMouseHoover.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            rbShowPercentileOnMouseHoover.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub rbDontShowPercentile_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
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
            ''If _SelectedPath.Substring(_SelectedPath.Length - 1, 1) = "\" Then
            ''    _gloDataPath = _SelectedPath & "gloEMR"
            ''Else
            ''    _gloDataPath = _SelectedPath & "\gloEMR"
            ''End If
            _gloDataPath = _SelectedPath
            'If System.IO.Directory.Exists(_gloDataPath) = False Then
            '    If MessageBox.Show("Selected path does not contain gloEMR folder, do you want to create gloData structure on selected path?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            '        System.IO.Directory.CreateDirectory(_gloDataPath)
            '    End If
            'End If
            ''Sandip Darade 200091107
            ''set the whole path for server path field 


            '#Region "Check and Create Claim Management Structure" 
            If System.IO.Directory.Exists(_gloDataPath) = True Then
                txtServerPath.Text = _gloDataPath

                Dim fileName As String = Nothing
                Do
                    fileName = txtServerPath.Text.Trim()
                    If fileName <> "" And fileName.Length > 0 Then
                        txtServerPath.Text = ShareCollection.PathToUnc(fileName)
                        fileName = ""
                    End If
                Loop While (fileName <> "" And fileName.Length > 0)

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
    Private Sub SavegloEMRDatabaseSettings()
        Try
            Dim ogloSettings As New clsSettings
            ogloSettings.Add("Add Patient To gloEMR", "false", gnClinicID, 0, SettingFlag.Clinic)

            Dim MigrateToEMRType As String = ""
            MigrateToEMRType = "gloEMR50"
            ogloSettings.Add("MigrateToEMRType", MigrateToEMRType, _ClinicID, 0, SettingFlag.Clinic)

            If gloPMAdmin.mdlGeneral.gblnSQLAuthentication = False Then
                ogloSettings.Add_gloEMRSetting("gloEMR SQL Server Name", gloPMAdmin.mdlGeneral.gstrSQLServerName)
                ogloSettings.Add_gloEMRSetting("gloEMR Database Name", gloPMAdmin.mdlGeneral.gstrDatabaseName)
                ogloSettings.Add_gloEMRSetting("gloEMR Authentication", "Windows")
            Else
                ogloSettings.Add_gloEMRSetting("gloEMR SQL Server Name", gloPMAdmin.mdlGeneral.gstrSQLServerName)
                ogloSettings.Add_gloEMRSetting("gloEMR Database Name", gloPMAdmin.mdlGeneral.gstrDatabaseName)
                ogloSettings.Add_gloEMRSetting("gloEMR Authentication", "SQL")
                ogloSettings.Add_gloEMRSetting("gloEMR User Name", gloPMAdmin.mdlGeneral.gstrSQLUserEMR)
                Dim oEncryption As New clsEncryption
                ogloSettings.Add_gloEMRSetting("gloEMR Password", oEncryption.EncryptToBase64String(gloPMAdmin.mdlGeneral.gstrSQLPasswordEMR, gloPMAdmin.mdlGeneral.constEncryptDecryptKey_Services))
            End If




        Catch ex As Exception
            MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub


    Private Sub SavegloEMRDatabaseSettingsNew()
        Try

            'Dim ogloSettings As New clsSettings
            ogloSettings.AddValueInTVP("Add Patient To gloEMR", "false", gnClinicID, 0, SettingFlag.Clinic)

            Dim MigrateToEMRType As String = ""
            MigrateToEMRType = "gloEMR50"
            ogloSettings.AddValueInTVP("MigrateToEMRType", MigrateToEMRType, _ClinicID, 0, SettingFlag.Clinic)

            If gloPMAdmin.mdlGeneral.gblnSQLAuthentication = False Then
                ogloSettings.AddValueInTVP("gloEMR SQL Server Name", gloPMAdmin.mdlGeneral.gstrSQLServerName, gnClinicID, 0, SettingFlag.Clinic)
                ogloSettings.AddValueInTVP("gloEMR Database Name", gloPMAdmin.mdlGeneral.gstrDatabaseName, gnClinicID, 0, SettingFlag.Clinic)
                ogloSettings.AddValueInTVP("gloEMR Authentication", "Windows", gnClinicID, 0, SettingFlag.Clinic)
            Else
                ogloSettings.AddValueInTVP("gloEMR SQL Server Name", gloPMAdmin.mdlGeneral.gstrSQLServerName, gnClinicID, 0, SettingFlag.Clinic)
                ogloSettings.AddValueInTVP("gloEMR Database Name", gloPMAdmin.mdlGeneral.gstrDatabaseName, gnClinicID, 0, SettingFlag.Clinic)
                ogloSettings.AddValueInTVP("gloEMR Authentication", "SQL", gnClinicID, 0, SettingFlag.Clinic)
                ogloSettings.AddValueInTVP("gloEMR User Name", gloPMAdmin.mdlGeneral.gstrSQLUserEMR, gnClinicID, 0, SettingFlag.Clinic)
                Dim oEncryption As New clsEncryption
                ogloSettings.AddValueInTVP("gloEMR Password", oEncryption.EncryptToBase64String(gloPMAdmin.mdlGeneral.gstrSQLPasswordEMR, gloPMAdmin.mdlGeneral.constEncryptDecryptKey_Services), gnClinicID, 0, SettingFlag.Clinic)
            End If




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
                    ''Sandip Darade 20091106
                    'ogloSettings.Get_gloEMRSetting("gloEMR SQL Server Name", value)
                    'If value IsNot Nothing Then
                    '    txtPMServerName.Text = Convert.ToString(value)
                    '    If txtPMServerName.Text = "" Then
                    '        chk_PMDBSettings.Checked = False
                    '    End If
                    '    value = Nothing
                    'End If
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
                        'If (Convert.ToString(value) = "gloEMR40SP2") Then
                        '    rbtn_MigratetogloEMR40.Checked = True
                        'End If
                        'If (Convert.ToString(value) = "gloEMR50") Then
                        '    rbtn_MigratetogloEMR50.Checked = True
                        'End If
                        'If (Convert.ToString(value) = "gloEMR2.8") Then
                        '    rbtn_MigratetogloEMR28.Checked = True
                        'End If
                        rbtn_MigratetogloEMR50.Checked = True
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
            '' End If



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
        Else
            optClinicDIYes.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub optClinicDINo_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optClinicDINo.CheckedChanged
        If optClinicDINo.Checked = True Then
            optClinicDINo.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            optClinicDINo.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub optClinicFormularyYes_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optClinicFormularyYes.CheckedChanged
        If optClinicFormularyYes.Checked = True Then
            optClinicFormularyYes.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            optClinicFormularyYes.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub optClinicFormularyNo_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optClinicFormularyNo.CheckedChanged
        If optClinicFormularyNo.Checked = True Then
            optClinicFormularyNo.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            optClinicFormularyNo.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
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

    Private Sub optPwdComplexNo_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If optPwdComplexNo.Checked = True Then
            optPwdComplexNo.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            optPwdComplexNo.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub rbStaging_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If rbStaging.Checked = True Then
            rbStaging.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            rbStaging.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub rbProduction_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If rbProduction.Checked = True Then
            rbProduction.Font = New Font("Tahoma", 9, FontStyle.Bold)
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

    Private Sub rdbNone_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If rdbNone.Checked = True Then
            rdbNone.Font = New Font("Tahoma", 9, FontStyle.Bold)
            txtAlphaIIDatabase.Enabled = False
            txtAlphaIIPassword.Enabled = False
            txtAlphaIIServerName.Enabled = False
            txtAlphaIIUserName.Enabled = False
            cmbAlphaIIAuthentication.Enabled = False
        Else
            rdbNone.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub optFAXreceiveYes_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If optFAXreceiveYes.Checked = True Then
            optFAXreceiveYes.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            optFAXreceiveYes.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub optFAXreceiveNo_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If optFAXreceiveNo.Checked = True Then
            optFAXreceiveNo.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            optFAXreceiveNo.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub optWindowsAuthentication_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If optWindowsAuthentication.Checked = True Then
            optWindowsAuthentication.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            optWindowsAuthentication.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub Rbtn_showcode_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If Rbtn_showcode.Checked = True Then
            Rbtn_showcode.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            Rbtn_showcode.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub Rbtn_showDesc_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If Rbtn_showDesc.Checked = True Then
            Rbtn_showDesc.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            Rbtn_showDesc.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub Rbtn_ShowBoth_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If Rbtn_ShowBoth.Checked = True Then
            Rbtn_ShowBoth.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            Rbtn_ShowBoth.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub rbChiefComplaint_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If rbChiefComplaint.Checked = True Then
            rbChiefComplaint.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            rbChiefComplaint.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub rbProblemList_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If rbProblemList.Checked = True Then
            rbProblemList.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            rbProblemList.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub Rb_AllowedAmt_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If Rb_AllowedAmt.Checked = True Then
            Rb_AllowedAmt.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            Rb_AllowedAmt.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    '<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<ojeswini_4/08/2009>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

    Private Sub btn_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddSurgicalAlertUser.MouseHover, btnDelSurgicalAlertUser.MouseHover, btnServerPath.MouseHover, btnHL7FilePath.MouseHover, btnDeleteServerPath.MouseHover, btnBrowseEAR.MouseHover, btnDelFollowUPUser.MouseHover, btnBrowseeFaxDownloadDir.MouseHover, btnAddFollowUpUser.MouseHover, btnSetPwdComplexity.MouseHover, btnSelectRecoveryPath.MouseHover, btnMCIRReportPath.MouseHover, btnConnect.MouseHover, btnCCDfilePath.MouseHover, btnLoadPath.MouseHover
        CType(sender, Button).BackgroundImage = Global.gloPMAdmin.My.Resources.Resources.Img_LongYellow
        CType(sender, Button).BackgroundImageLayout = ImageLayout.Stretch
    End Sub

    Private Sub btn_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelSurgicalAlertUser.MouseLeave, btnAddSurgicalAlertUser.MouseLeave, btnServerPath.MouseLeave, btnHL7FilePath.MouseLeave, btnDeleteServerPath.MouseLeave, btnBrowseEAR.MouseLeave, btnDelFollowUPUser.MouseLeave, btnBrowseeFaxDownloadDir.MouseLeave, btnAddFollowUpUser.MouseLeave, btnSetPwdComplexity.MouseLeave, btnSelectRecoveryPath.MouseLeave, btnMCIRReportPath.MouseLeave, btnConnect.MouseLeave, btnCCDfilePath.MouseLeave, btnLoadPath.MouseLeave
        CType(sender, Button).BackgroundImage = Global.gloPMAdmin.My.Resources.Resources.Img_LongButton
        CType(sender, Button).BackgroundImageLayout = ImageLayout.Stretch
    End Sub

    ''Sandip Darade 20090813
    Private Sub rbtn_MigratetogloEMR50_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If rbtn_MigratetogloEMR50.Checked = True Then
            rbtn_MigratetogloEMR50.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            rbtn_MigratetogloEMR50.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub rbtn_MigratetogloEMR40_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If rbtn_MigratetogloEMR40.Checked = True Then
            rbtn_MigratetogloEMR40.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            rbtn_MigratetogloEMR40.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub rbtn_MigratetogloEMR28_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If rbtn_MigratetogloEMR28.Checked = True Then
            rbtn_MigratetogloEMR28.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            rbtn_MigratetogloEMR28.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub rbICD9Driven_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If rbICD9Driven.Checked Then
            rbICD9Driven.Font = New Font("Tahoma", 9, FontStyle.Bold)
            If rbICD9Driven.Visible Then
                If MessageBox.Show("Once you changed the interface to ICD9, The CPT interface may not show entries done with ICD9 interface." & vbLf & "Are you sure you want to change this option ? ", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = Windows.Forms.DialogResult.No Then
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

    Private Sub rbCPTDriven_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If rbCPTDriven.Checked Then
            rbCPTDriven.Font = New Font("Tahoma", 9, FontStyle.Bold)
            If rbCPTDriven.Visible Then
                If MessageBox.Show("Once you changed the interface to CPT, The ICD9 interface may not show entries done with CPT interface." & vbLf & "Are you sure you want to change this option ? ", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = Windows.Forms.DialogResult.No Then
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

    Private Sub rbShow8ICD9_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If rbShow8ICD9.Checked Then
            rbShow8ICD9.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            rbShow8ICD9.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub rbShow4ICD9_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If rbShow4ICD9.Checked Then
            rbShow4ICD9.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            rbShow4ICD9.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub rbShow4Modifier_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If rbShow4Modifier.Checked Then
            rbShow4Modifier.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            rbShow4Modifier.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub rbShow2Modifier_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
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
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
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

    Private Sub cmbGeniusPaths_SelectionChangeCommitted(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbGeniusPaths.SelectionChangeCommitted

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
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Throw ex
        End Try
    End Function
    Private Sub getGeniusSettings()

    End Sub
#End Region

    '''Sandip Darade 20091211

    Private Sub DesignGridForBillingSetting()



        C1BillingSettings.Rows.Count = 1
        C1BillingSettings.Cols.Count = COL_COLCOUNT

        C1BillingSettings.SetData(0, COL_BPROVIDERID, "Provider ID")
        C1BillingSettings.SetData(0, COL_PROVIDER_NAME, "Provider Name")
        C1BillingSettings.SetData(0, COL_TOS, "TOS")
        C1BillingSettings.SetData(0, COL_POS, "POS")
        C1BillingSettings.SetData(0, COL_BillProvider, "Billing Provider")
        C1BillingSettings.SetData(0, COL_RenProvider, "Rendering Provider")
        C1BillingSettings.SetData(0, COL_Facility, "Facility")
        C1BillingSettings.SetData(0, COL_DFS, "Default Fee Schedule")

        C1BillingSettings.Cols(COL_BPROVIDERID).Visible = False
        C1BillingSettings.Cols(COL_PROVIDER_NAME).Visible = True
        C1BillingSettings.Cols(COL_TOS).Visible = False
        C1BillingSettings.Cols(COL_POS).Visible = False
        C1BillingSettings.Cols(COL_BillProvider).Visible = True
        C1BillingSettings.Cols(COL_RenProvider).Visible = True
        C1BillingSettings.Cols(COL_Facility).Visible = True
        C1BillingSettings.Cols(COL_DFS).Visible = True


        C1BillingSettings.AllowEditing = True

        C1BillingSettings.Cols(COL_BPROVIDERID).AllowEditing = False
        C1BillingSettings.Cols(COL_PROVIDER_NAME).AllowEditing = False
        C1BillingSettings.Cols(COL_TOS).AllowEditing = True
        C1BillingSettings.Cols(COL_POS).AllowEditing = True
        C1BillingSettings.Cols(COL_BillProvider).AllowEditing = True
        C1BillingSettings.Cols(COL_RenProvider).AllowEditing = True
        C1BillingSettings.Cols(COL_Facility).AllowEditing = True
        C1BillingSettings.Cols(COL_DFS).AllowEditing = True



        Dim dtTOS As DataTable = GetTOS()
        Dim _strTOS As String
        If Not dtTOS Is Nothing Then
            If dtTOS.Rows.Count > 0 Then
                For i As Integer = 0 To dtTOS.Rows.Count - 1

                    _strTOS = _strTOS & "|" & dtTOS.Rows(i)("sDescription").ToString()

                Next
            End If
        End If

        Dim dtPOS As DataTable = GetPOS()
        Dim _strPOS As String
        If Not dtPOS Is Nothing Then
            If dtPOS.Rows.Count > 0 Then
                For i As Integer = 0 To dtPOS.Rows.Count - 1

                    _strPOS = _strPOS & "|" & dtPOS.Rows(i)("sPOScode").ToString()

                Next
            End If
        End If



        Dim dtfacility As DataTable = GetFacilities()

        Dim _strFacility As String
        If Not dtfacility Is Nothing Then
            If dtfacility.Rows.Count > 0 Then
                For i As Integer = 0 To dtfacility.Rows.Count - 1

                    _strFacility = _strFacility & "|" & dtfacility.Rows(i)("sfacilityname").ToString()

                Next
            End If
        End If

        Dim dtDefaultFeeSchedule As DataTable = GetDefaultFeeSchedule()
        Dim _strFeeschedule As String
        If Not dtDefaultFeeSchedule Is Nothing Then
            If dtfacility.Rows.Count > 0 Then
                For i As Integer = 0 To dtDefaultFeeSchedule.Rows.Count - 1

                    _strFeeschedule = _strFeeschedule & "|" & dtDefaultFeeSchedule.Rows(i)("sFeeScheduleName").ToString()

                Next
            End If
        End If
        Dim dtProviders As DataTable = GetProvidersForProviderSetting()

        Dim _strProviders As String
        If Not dtProviders Is Nothing Then
            If dtProviders.Rows.Count > 0 Then
                For i As Integer = 0 To dtProviders.Rows.Count - 1

                    _strProviders = _strProviders & "|" & dtProviders.Rows(i)("ProviderName").ToString()

                Next
            End If
        End If

        C1BillingSettings.Cols(COL_TOS).ComboList = " |" & _strTOS
        C1BillingSettings.Cols(COL_POS).ComboList = " |" & _strPOS
        C1BillingSettings.Cols(COL_BillProvider).ComboList = " |" & _strProviders
        C1BillingSettings.Cols(COL_RenProvider).ComboList = " |" & _strProviders
        C1BillingSettings.Cols(COL_Facility).ComboList = " |" & _strFacility
        C1BillingSettings.Cols(COL_DFS).ComboList = " |" & _strFeeschedule


        Dim _width As Int32 = C1BillingSettings.Width - 2
        C1BillingSettings.Cols(COL_BPROVIDERID).Width = 0
        C1BillingSettings.Cols(COL_PROVIDER_NAME).Width = Convert.ToInt32(_width * 0.2)
        C1BillingSettings.Cols(COL_TOS).Width = Convert.ToInt32(_width * 0.2)
        C1BillingSettings.Cols(COL_POS).Width = Convert.ToInt32(_width * 0.2)
        C1BillingSettings.Cols(COL_RenProvider).Width = Convert.ToInt32(_width * 0.2)
        C1BillingSettings.Cols(COL_BillProvider).Width = Convert.ToInt32(_width * 0.2)
        C1BillingSettings.Cols(COL_Facility).Width = Convert.ToInt32(_width * 0.2)
        C1BillingSettings.Cols(COL_DFS).Width = Convert.ToInt32(_width * 0.2)

        C1BillingSettings.Cols(COL_TOS).TextAlign = TextAlignEnum.LeftCenter
        C1BillingSettings.Cols(COL_POS).TextAlign = TextAlignEnum.LeftCenter
        C1BillingSettings.Cols(COL_BillProvider).TextAlign = TextAlignEnum.LeftCenter
        C1BillingSettings.Cols(COL_RenProvider).TextAlign = TextAlignEnum.LeftCenter
        C1BillingSettings.Cols(COL_Facility).TextAlign = TextAlignEnum.LeftCenter
        C1BillingSettings.Cols(COL_DFS).TextAlign = TextAlignEnum.LeftCenter



        If dtProviders IsNot Nothing AndAlso dtProviders.Rows.Count > 0 Then
            For i As Integer = 0 To dtProviders.Rows.Count - 1
                C1BillingSettings.Rows.Add()
                Dim RowIndex As Int32 = C1BillingSettings.Rows.Count - 1
                C1BillingSettings.SetData(RowIndex, COL_BPROVIDERID, Convert.ToString(dtProviders.Rows(i)("nProviderID")))
                C1BillingSettings.SetData(RowIndex, COL_PROVIDER_NAME, Convert.ToString(dtProviders.Rows(i)("ProviderName")))
            Next

        End If
        '' Fill Providers To Grid
    End Sub


    Private Sub DesignGridForAppointmentDefault()

        C1AppointmentDefault.Cols.Count = COL_DACOUNT

        C1AppointmentDefault.Cols(COL_APPOINTMENTTYPEID).Caption = "AppointmentTypeID"
        C1AppointmentDefault.Cols(COL_APPOINTMENTTYPE).Caption = "Appointment Type"
        C1AppointmentDefault.Cols(COL_DOS).Caption = "Date of Service"
        C1AppointmentDefault.Cols(COL_DFacility).Caption = "Facility"
        C1AppointmentDefault.Cols(COL_RenderProvider).Caption = "Rendering Provider"

        C1AppointmentDefault.Cols(COL_APPOINTMENTTYPEID).Visible = False
        C1AppointmentDefault.Cols(COL_APPOINTMENTTYPE).Visible = True
        C1AppointmentDefault.Cols(COL_DOS).Visible = True
        C1AppointmentDefault.Cols(COL_DFacility).Visible = True
        C1AppointmentDefault.Cols(COL_RenderProvider).Visible = True


        C1AppointmentDefault.AllowEditing = True
        C1AppointmentDefault.ExtendLastCol = True

        C1AppointmentDefault.Cols(COL_APPOINTMENTTYPEID).AllowEditing = False
        C1AppointmentDefault.Cols(COL_APPOINTMENTTYPE).AllowEditing = False
        C1AppointmentDefault.Cols(COL_DOS).AllowEditing = True
        C1AppointmentDefault.Cols(COL_DFacility).AllowEditing = True
        C1AppointmentDefault.Cols(COL_RenderProvider).AllowEditing = True
        C1AppointmentDefault.Cols(COL_APPOINTMENTTYPEID).AllowSorting = False
        C1AppointmentDefault.Cols(COL_APPOINTMENTTYPE).AllowSorting = True
        C1AppointmentDefault.Cols(COL_DOS).AllowSorting = False
        C1AppointmentDefault.Cols(COL_DFacility).AllowSorting = False
        C1AppointmentDefault.Cols(COL_RenderProvider).AllowSorting = False

        C1AppointmentDefault.Cols(COL_DOS).ComboList = " |" & "Yes"
        C1AppointmentDefault.Cols(COL_DFacility).ComboList = " |" & "Yes"
        C1AppointmentDefault.Cols(COL_RenderProvider).ComboList = " |" & "Yes"

        Dim _width As Int32 = C1AppointmentDefault.Width - 2
        C1AppointmentDefault.Cols(COL_APPOINTMENTTYPE).Width = Convert.ToInt32(_width * 0.4)
        C1AppointmentDefault.Cols(COL_DOS).Width = Convert.ToInt32(_width * 0.2)
        C1AppointmentDefault.Cols(COL_RenderProvider).Width = Convert.ToInt32(_width * 0.2)
        C1AppointmentDefault.Cols(COL_DFacility).Width = Convert.ToInt32(_width * 0.2)

        C1AppointmentDefault.Cols(COL_APPOINTMENTTYPE).TextAlign = TextAlignEnum.LeftCenter
        C1AppointmentDefault.Cols(COL_DOS).TextAlign = TextAlignEnum.LeftCenter
        C1AppointmentDefault.Cols(COL_DFacility).TextAlign = TextAlignEnum.LeftCenter
        C1AppointmentDefault.Cols(COL_RenderProvider).TextAlign = TextAlignEnum.LeftCenter

    End Sub

    Private Sub DesignGridForMidLevelBillingSetting()

        Dim dtMidlevelSettings As New DataTable()
        'CmbMidLevelSettings.DataSource = Nothing
        Dim odb As New gloStream.gloDataBase.gloDataBase


        C1MidLevelSettings.Rows.Count = 1
        C1MidLevelSettings.Cols.Count = 5

        C1MidLevelSettings.SetData(0, MidLevelGridCol.ProviderID, "Provider ID")
        C1MidLevelSettings.SetData(0, MidLevelGridCol.ProviderName, "Provider")
        C1MidLevelSettings.SetData(0, MidLevelGridCol.SettingsID, "SettingsID")
        C1MidLevelSettings.SetData(0, MidLevelGridCol.Type, "Type")
        C1MidLevelSettings.SetData(0, MidLevelGridCol.SettingsName, "Report Rendering")

        C1MidLevelSettings.Cols(MidLevelGridCol.ProviderID).Visible = False
        C1MidLevelSettings.Cols(MidLevelGridCol.ProviderName).Visible = True
        C1MidLevelSettings.Cols(MidLevelGridCol.SettingsID).Visible = False
        C1MidLevelSettings.Cols(MidLevelGridCol.Type).Visible = True
        C1MidLevelSettings.Cols(MidLevelGridCol.SettingsName).Visible = True

        C1MidLevelSettings.AllowEditing = True

        C1MidLevelSettings.Cols(MidLevelGridCol.ProviderID).AllowEditing = False
        C1MidLevelSettings.Cols(MidLevelGridCol.ProviderName).AllowEditing = False
        C1MidLevelSettings.Cols(MidLevelGridCol.SettingsID).AllowEditing = False
        C1MidLevelSettings.Cols(MidLevelGridCol.Type).AllowEditing = False
        C1MidLevelSettings.Cols(MidLevelGridCol.SettingsName).AllowEditing = True

        Dim dtProviders As DataTable = GetProvidersForProviderSetting()

        Dim _sqlQuery As String = "SELECT nSettingsID,sMidLevelBillingType FROM dbo.BL_MidLevelSettings_MST "
        odb.Connect(gstrConnectionString)
        dtMidlevelSettings = odb.ReadQueryData(_sqlQuery)

        'Dim dr As DataRow
        'dr = dtMidlevelSettings.NewRow()
        'dr(0) = 0
        'dr(1) = " "
        'dtMidlevelSettings.Rows.InsertAt(dr, 0)

        Dim _strMidlevelSettings As String = String.Empty
        If Not dtMidlevelSettings Is Nothing Then
            If dtMidlevelSettings.Rows.Count > 0 Then
                htMidLevel = New Hashtable()
                For i As Integer = 0 To dtMidlevelSettings.Rows.Count - 1
                    htMidLevel.Add(dtMidlevelSettings.Rows(i)("sMidLevelBillingType").ToString(), dtMidlevelSettings.Rows(i)("nSettingsID").ToString())
                    _strMidlevelSettings = _strMidlevelSettings & "|" & dtMidlevelSettings.Rows(i)("sMidLevelBillingType").ToString()

                Next
            End If
        End If

        C1MidLevelSettings.Cols(MidLevelGridCol.SettingsName).ComboList = " |" & _strMidlevelSettings


        C1MidLevelSettings.Cols(MidLevelGridCol.ProviderID).Width = 50
        C1MidLevelSettings.Cols(MidLevelGridCol.ProviderName).Width = 275
        C1MidLevelSettings.Cols(MidLevelGridCol.SettingsID).Width = 50
        C1MidLevelSettings.Cols(MidLevelGridCol.Type).Width = 100
        C1MidLevelSettings.Cols(MidLevelGridCol.SettingsName).Width = 280

        C1MidLevelSettings.ExtendLastCol = True

        'C1MidLevelSettings.Cols(MidLevelGridCol.ProviderName).AllowSorting = False
        C1MidLevelSettings.Cols(MidLevelGridCol.ProviderName).AllowResizing = False

        'C1MidLevelSettings.Cols(MidLevelGridCol.Type).AllowSorting = False
        C1MidLevelSettings.Cols(MidLevelGridCol.Type).AllowResizing = False

        'C1MidLevelSettings.Cols(MidLevelGridCol.SettingsName).AllowSorting = False
        C1MidLevelSettings.Cols(MidLevelGridCol.SettingsName).AllowResizing = False

        C1MidLevelSettings.Styles.Fixed.Font = New Font("Tahoma", 9, FontStyle.Bold)
        If dtProviders IsNot Nothing AndAlso dtProviders.Rows.Count > 0 Then
            For i As Integer = 0 To dtProviders.Rows.Count - 1
                C1MidLevelSettings.Rows.Add()
                Dim RowIndex As Int32 = C1MidLevelSettings.Rows.Count - 1
                C1MidLevelSettings.SetData(RowIndex, MidLevelGridCol.ProviderID, Convert.ToString(dtProviders.Rows(i)("nProviderID")))
                C1MidLevelSettings.SetData(RowIndex, MidLevelGridCol.ProviderName, Convert.ToString(dtProviders.Rows(i)("ProviderName")))
                C1MidLevelSettings.SetData(RowIndex, MidLevelGridCol.Type, Convert.ToString(dtProviders.Rows(i)("ProviderType")))
            Next

        End If
        '' Fill Providers To Grid
    End Sub

    Private Sub DesignGridForANSIVersionSetting(ByRef c1Grid As C1FlexGrid)

        Dim objSettings As New GeneralSettings(gloPMAdmin.mdlGeneral.GetConnectionString())
        Dim oDatatable As New DataTable
        Dim _strANSI As String = ""

        c1Grid.Rows.Count = 1
        c1Grid.Cols.Count = 4

        c1Grid.SetData(0, ANSIGrid.ContactID, "ContactID#")
        c1Grid.SetData(0, ANSIGrid.ContactName, "Insurance Plan Overrides")
        If c1Grid.Name = C1ANSIVersionSettings.Name Then
            c1Grid.SetData(0, ANSIGrid.ClaimBatchSettings, "Claims ANSI Version")
        ElseIf c1Grid.Name = c1CMSVersionPlanSetup.Name Then
            c1Grid.SetData(0, ANSIGrid.ClaimBatchSettings, "Claims CMS1500 Version")
        End If

        c1Grid.SetData(0, ANSIGrid.EligiblityRequestSettings, "Eligibility Req. ANSI Version")


        c1Grid.Cols(ANSIGrid.ContactID).Visible = False
        c1Grid.Cols(ANSIGrid.ContactName).Visible = True
        c1Grid.Cols(ANSIGrid.ClaimBatchSettings).Visible = True
        c1Grid.Cols(ANSIGrid.EligiblityRequestSettings).Visible = True


        c1Grid.AllowEditing = True

        c1Grid.Cols(ANSIGrid.ContactID).AllowEditing = False
        c1Grid.Cols(ANSIGrid.ContactName).AllowEditing = False
        c1Grid.Cols(ANSIGrid.ClaimBatchSettings).AllowEditing = True
        c1Grid.Cols(ANSIGrid.EligiblityRequestSettings).AllowEditing = True

        c1Grid.ExtendLastCol = True

        c1Grid.Cols(ANSIGrid.ContactID).Width = 50
        c1Grid.Cols(ANSIGrid.ContactName).Width = 330
        c1Grid.Cols(ANSIGrid.ClaimBatchSettings).Width = 140
        c1Grid.Cols(ANSIGrid.EligiblityRequestSettings).Width = 175

        'c1Grid.Cols(ANSIGrid.ClaimBatchSettings).AllowSorting = False
        'c1Grid.Cols(ANSIGrid.EligiblityRequestSettings).AllowSorting = False

        c1Grid.Cols(ANSIGrid.ClaimBatchSettings).AllowResizing = False
        c1Grid.Cols(ANSIGrid.EligiblityRequestSettings).AllowResizing = False
        c1Grid.Cols(ANSIGrid.ContactName).AllowResizing = False


        c1Grid.Cols(ANSIGrid.ClaimBatchSettings).AllowDragging = False
        c1Grid.Cols(ANSIGrid.EligiblityRequestSettings).AllowDragging = False
        c1Grid.Cols(ANSIGrid.ContactName).AllowDragging = False

        If c1Grid.Name = C1ANSIVersionSettings.Name Then

            oDatatable = objSettings.GetEnumItems(gloSettings.ANSIVersions.ANSI_4010, True)

            htANSI = New Hashtable

            If Not oDatatable Is Nothing AndAlso oDatatable.Rows.Count > 0 Then
                _strANSI = String.Empty
                For i As Integer = 0 To oDatatable.Rows.Count - 1
                    If oDatatable.Rows(i)("sDescription").ToString() <> String.Empty Then
                        _strANSI = _strANSI & "|" & oDatatable.Rows(i)("sDescription").ToString()
                        htANSI.Add(oDatatable.Rows(i)("sDescription").ToString(), oDatatable.Rows(i)("nID").ToString())
                    End If
                Next
            End If

            _strANSI = " |" & _strANSI

            c1Grid.Cols(ANSIGrid.ClaimBatchSettings).ComboList = _strANSI
            c1Grid.Cols(ANSIGrid.EligiblityRequestSettings).ComboList = _strANSI

        ElseIf c1Grid.Name = c1CMSVersionPlanSetup.Name Then

            oDatatable = objSettings.GetEnumItemsDescriptionPaperFormVersion(gloSettings.PaperFormVersion.CMS1500, True)

            htPaperFormFormat = New Hashtable

            If Not oDatatable Is Nothing AndAlso oDatatable.Rows.Count > 0 Then
                _strANSI = String.Empty
                For i As Integer = 0 To oDatatable.Rows.Count - 1
                    If oDatatable.Rows(i)("sDescription").ToString() <> String.Empty Then
                        _strANSI = _strANSI & "|" & oDatatable.Rows(i)("sDescription").ToString()
                        htPaperFormFormat.Add(oDatatable.Rows(i)("sDescription").ToString(), oDatatable.Rows(i)("nID").ToString())
                    End If
                Next
            End If

            _strANSI = " |" & _strANSI

            c1Grid.Cols(ANSIGrid.ClaimBatchSettings).ComboList = _strANSI
            c1Grid.Cols(ANSIGrid.EligiblityRequestSettings).Visible = False

        End If





    End Sub

    Private Function GetDefaultFeeSchedule() As DataTable
        Try

            Dim odb As New gloStream.gloDataBase.gloDataBase

            Dim _sqlQuery As String = "SELECT DISTINCT nFeeScheduleID, sFeeScheduleName FROM BL_FeeSchedule_MST "
            odb.Connect(gstrConnectionString)
            Dim dt As DataTable = odb.ReadQueryData(_sqlQuery)
            Return dt

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Function

    Private Sub SaveMidLevelBillingSetting()
        Try

            Dim odb As New gloStream.gloDataBase.gloDataBase
            Dim ogloSettings As New clsSettings
            Dim _sqlQuery As String = "DELETE from BL_MidLevelBilling_Settings"
            odb.Connect(gstrConnectionString)
            odb.ExecuteQueryScaler(_sqlQuery)
            odb.Disconnect()

            If CmbMidLevelSettings.Text IsNot String.Empty And CmbMidLevelSettings.Text <> "" Then
                ogloSettings.AddMidLevelBillingSettings(0, Convert.ToInt64(CmbMidLevelSettings.SelectedValue), gnLoginID, _ClinicID, clsSettings.MidLevelSettingsType.AllProvidersAllPlans)
            End If

            For i As Integer = 1 To C1MidLevelSettings.Rows.Count - 1
                If Not IsNothing(C1MidLevelSettings.GetData(i, MidLevelGridCol.SettingsName)) AndAlso C1MidLevelSettings.GetData(i, MidLevelGridCol.SettingsName) <> " " Then
                    ogloSettings.AddMidLevelBillingSettings(C1MidLevelSettings.GetData(i, MidLevelGridCol.ProviderID), htMidLevel(C1MidLevelSettings.GetData(i, MidLevelGridCol.SettingsName)), gnLoginID, _ClinicID, clsSettings.MidLevelSettingsType.SpecificProviderAllPlans)
                End If

            Next
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub SaveMidLevelBillingSettingNew()
        Try
            'Dim ogloSettings As New clsSettings

            If CmbMidLevelSettings.Text IsNot String.Empty And CmbMidLevelSettings.Text <> "" Then
                ogloSettings.AddMidLevelBillingSettingsInTVP(0, Convert.ToInt64(CmbMidLevelSettings.SelectedValue), gnLoginID, _ClinicID, clsSettings.MidLevelSettingsType.AllProvidersAllPlans)
            End If

            For i As Integer = 1 To C1MidLevelSettings.Rows.Count - 1
                If Not IsNothing(C1MidLevelSettings.GetData(i, MidLevelGridCol.SettingsName)) AndAlso C1MidLevelSettings.GetData(i, MidLevelGridCol.SettingsName) <> " " Then
                    ogloSettings.AddMidLevelBillingSettingsInTVP(C1MidLevelSettings.GetData(i, MidLevelGridCol.ProviderID), htMidLevel(C1MidLevelSettings.GetData(i, MidLevelGridCol.SettingsName)), gnLoginID, _ClinicID, clsSettings.MidLevelSettingsType.SpecificProviderAllPlans)
                End If

            Next
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub SaveBillingSetting()
        Try

            Dim odb As New gloStream.gloDataBase.gloDataBase

            Dim _sqlQuery As String = "DELETE Providersettings where sSettingstype = ''"
            ''20100626=> check settingtype blank which is added from gloPM not from gloEMR, So that delete only PM provider setting , not EMR provider setting check for blank or not
            odb.Connect(gstrConnectionString)
            odb.ExecuteQueryScaler(_sqlQuery)
            odb.Disconnect()
            For i As Integer = 1 To C1BillingSettings.Rows.Count - 1

                Dim nProviderID As Int64 = 0

                nProviderID = Convert.ToInt64(C1BillingSettings.GetData(i, COL_BPROVIDERID))

                Dim ogloSettings As New clsSettings
                Dim ProviderID As Int64 = 0



                ProviderID = Convert.ToInt64(nProviderID)

                '' If cmbTOS.SelectedIndex <> -1 Then
                If Not IsNothing(C1BillingSettings.GetData(i, COL_TOS)) Then

                    If (C1BillingSettings.GetData(i, COL_TOS).ToString().Trim <> "") Then
                        cmbTOS.Text = Convert.ToString(C1BillingSettings.GetData(i, COL_TOS))

                        Dim _TOSID As String = Convert.ToString(cmbTOS.SelectedValue)

                        ogloSettings.AddSettings(ProviderID, "TypeOfService", _TOSID, gnLoginID, _ClinicID, 0, "")
                    Else
                        ogloSettings.AddSettings(ProviderID, "TypeOfService", "0", gnLoginID, _ClinicID, 0, "")

                    End If

                Else
                    ogloSettings.AddSettings(ProviderID, "TypeOfService", "0", gnLoginID, _ClinicID, 0, "")
                End If

                If Not IsNothing(C1BillingSettings.GetData(i, COL_POS)) Then
                    If (C1BillingSettings.GetData(i, COL_POS).ToString().Trim <> "") Then
                        cmbPOS.Text = Convert.ToString(C1BillingSettings.GetData(i, COL_POS))

                        Dim _POSID As String = Convert.ToString(cmbPOS.SelectedValue)

                        ogloSettings.AddSettings(ProviderID, "PlaceOfService", _POSID, gnLoginID, _ClinicID, 0, "")
                    Else
                        ogloSettings.AddSettings(ProviderID, "PlaceOfService", "0", gnLoginID, _ClinicID, 0, "")

                    End If

                Else
                    ogloSettings.AddSettings(ProviderID, "PlaceOfService", "0", gnLoginID, _ClinicID, 0, "")
                End If

                'If cmbBillingProvider.SelectedIndex <> -1 Then
                If Not IsNothing(C1BillingSettings.GetData(i, COL_BillProvider)) Then

                    If (C1BillingSettings.GetData(i, COL_BillProvider).ToString().Trim <> "") Then
                        cmbBillingProvider.Text = Convert.ToString(C1BillingSettings.GetData(i, COL_BillProvider))

                        Dim _ID As String = Convert.ToString(cmbBillingProvider.SelectedValue)

                        ogloSettings.AddSettings(ProviderID, "BillingProvider", _ID, gnLoginID, _ClinicID, 0, "")
                    Else
                        ogloSettings.AddSettings(ProviderID, "BillingProvider", "0", gnLoginID, _ClinicID, 0, "")

                    End If

                Else
                    ogloSettings.AddSettings(ProviderID, "BillingProvider", "0", gnLoginID, _ClinicID, 0, "")
                End If

                'If cmbRenderingProvider.SelectedIndex <> -1 Then
                If Not IsNothing(C1BillingSettings.GetData(i, COL_RenProvider)) Then
                    If (C1BillingSettings.GetData(i, COL_RenProvider).ToString().Trim <> "") Then
                        cmbRenderingProvider.Text = Convert.ToString(C1BillingSettings.GetData(i, COL_RenProvider))

                        Dim _ID As String = Convert.ToString(cmbRenderingProvider.SelectedValue)

                        ogloSettings.AddSettings(ProviderID, "RenderingProvider", _ID, gnLoginID, _ClinicID, 0, "")
                    Else
                        ogloSettings.AddSettings(ProviderID, "RenderingProvider", "0", gnLoginID, _ClinicID, 0, "")

                    End If

                Else
                    ogloSettings.AddSettings(ProviderID, "RenderingProvider", "0", gnLoginID, _ClinicID, 0, "")
                End If

                'If cmbFacility.SelectedIndex <> -1 Then
                If Not IsNothing(C1BillingSettings.GetData(i, COL_Facility)) Then
                    If (C1BillingSettings.GetData(i, COL_Facility).ToString().Trim <> "") Then
                        cmbFacility.Text = Convert.ToString(C1BillingSettings.GetData(i, COL_Facility))

                        Dim _ID As String = Convert.ToString(cmbFacility.SelectedValue)

                        ogloSettings.AddSettings(ProviderID, "Facility", _ID, gnLoginID, _ClinicID, 0, "")
                    Else
                        ogloSettings.AddSettings(ProviderID, "Facility", "0", gnLoginID, _ClinicID, 0, "")

                    End If

                Else
                    ogloSettings.AddSettings(ProviderID, "Facility", "0", gnLoginID, _ClinicID, 0, "")
                End If

                'If cmbFeeSchedule.SelectedIndex <> -1 Then
                If Not IsNothing(C1BillingSettings.GetData(i, COL_DFS)) Then

                    If (C1BillingSettings.GetData(i, COL_DFS).ToString().Trim <> "") Then
                        cmbFeeSchedule.Text = Convert.ToString(C1BillingSettings.GetData(i, COL_DFS))
                        Dim _ID As String = Convert.ToString(cmbFeeSchedule.SelectedValue)
                        ogloSettings.AddSettings(ProviderID, "Fee Schedule", _ID, gnLoginID, _ClinicID, 0, "")
                    Else
                        ogloSettings.AddSettings(ProviderID, "Fee Schedule", "0", gnLoginID, _ClinicID, 0, "")

                    End If


                Else
                    ogloSettings.AddSettings(ProviderID, "Fee Schedule", "0", gnLoginID, _ClinicID, 0, "")
                End If

                If cmb_Feeschedules.SelectedIndex <> -1 Then
                    ogloSettings.AddSettings(ProviderID, "ClinicFeeSchedule", Convert.ToString(cmb_Feeschedules.SelectedValue), gnLoginID, _ClinicID, 0, "")
                Else
                    ogloSettings.AddSettings(ProviderID, "ClinicFeeSchedule", "0", gnLoginID, _ClinicID, 0, "")
                End If
            Next
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub SaveBillingSettingNew()
        Try


            For i As Integer = 1 To C1BillingSettings.Rows.Count - 1

                Dim nProviderID As Int64 = 0
                nProviderID = Convert.ToInt64(C1BillingSettings.GetData(i, COL_BPROVIDERID))
                'Dim ogloSettings As New clsSettings
                Dim ProviderID As Int64 = 0
                ProviderID = Convert.ToInt64(nProviderID)


                If Not IsNothing(C1BillingSettings.GetData(i, COL_TOS)) Then

                    If (C1BillingSettings.GetData(i, COL_TOS).ToString().Trim <> "") Then
                        cmbTOS.Text = Convert.ToString(C1BillingSettings.GetData(i, COL_TOS))
                        Dim _TOSID As String = Convert.ToString(cmbTOS.SelectedValue)
                        ogloSettings.AddProviderSettingsInTVP(ProviderID, "TypeOfService", _TOSID, gnLoginID, _ClinicID, 0, "")
                    Else
                        ogloSettings.AddProviderSettingsInTVP(ProviderID, "TypeOfService", "0", gnLoginID, _ClinicID, 0, "")

                    End If

                Else
                    ogloSettings.AddProviderSettingsInTVP(ProviderID, "TypeOfService", "0", gnLoginID, _ClinicID, 0, "")
                End If



                If Not IsNothing(C1BillingSettings.GetData(i, COL_POS)) Then
                    If (C1BillingSettings.GetData(i, COL_POS).ToString().Trim <> "") Then
                        cmbPOS.Text = Convert.ToString(C1BillingSettings.GetData(i, COL_POS))

                        Dim _POSID As String = Convert.ToString(cmbPOS.SelectedValue)

                        ogloSettings.AddProviderSettingsInTVP(ProviderID, "PlaceOfService", _POSID, gnLoginID, _ClinicID, 0, "")
                    Else
                        ogloSettings.AddProviderSettingsInTVP(ProviderID, "PlaceOfService", "0", gnLoginID, _ClinicID, 0, "")

                    End If

                Else
                    ogloSettings.AddProviderSettingsInTVP(ProviderID, "PlaceOfService", "0", gnLoginID, _ClinicID, 0, "")
                End If


                If Not IsNothing(C1BillingSettings.GetData(i, COL_BillProvider)) Then
                    If (C1BillingSettings.GetData(i, COL_BillProvider).ToString().Trim <> "") Then

                        ''cmbBillingProvider.Text = Convert.ToString(C1BillingSettings.GetData(i, COL_BillProvider))
                        'Dim _ID As String = Convert.ToString(cmbBillingProvider.SelectedValue)
                        Dim _ID As String = ""
                        Dim _dr() As DataRow
                        _dr = _dtProviders.[Select]("ProviderName = '" & Convert.ToString(C1BillingSettings.GetData(i, COL_BillProvider)).Replace("'", "''") & "'")
                        If Not IsNothing(_dr) And _dr.Length > 0 Then
                            _ID = Convert.ToInt64(_dr(0).Item("nProviderID"))
                        End If
                        ogloSettings.AddProviderSettingsInTVP(ProviderID, "BillingProvider", _ID, gnLoginID, _ClinicID, 0, "")
                    Else
                        ogloSettings.AddProviderSettingsInTVP(ProviderID, "BillingProvider", "0", gnLoginID, _ClinicID, 0, "")

                    End If

                Else
                    ogloSettings.AddProviderSettingsInTVP(ProviderID, "BillingProvider", "0", gnLoginID, _ClinicID, 0, "")
                End If


                If Not IsNothing(C1BillingSettings.GetData(i, COL_RenProvider)) Then
                    If (C1BillingSettings.GetData(i, COL_RenProvider).ToString().Trim <> "") Then
                        cmbRenderingProvider.Text = Convert.ToString(C1BillingSettings.GetData(i, COL_RenProvider))

                        Dim _ID As String = Convert.ToString(cmbRenderingProvider.SelectedValue)

                        ogloSettings.AddProviderSettingsInTVP(ProviderID, "RenderingProvider", _ID, gnLoginID, _ClinicID, 0, "")
                    Else
                        ogloSettings.AddProviderSettingsInTVP(ProviderID, "RenderingProvider", "0", gnLoginID, _ClinicID, 0, "")

                    End If

                Else
                    ogloSettings.AddProviderSettingsInTVP(ProviderID, "RenderingProvider", "0", gnLoginID, _ClinicID, 0, "")
                End If



                If Not IsNothing(C1BillingSettings.GetData(i, COL_Facility)) Then
                    If (C1BillingSettings.GetData(i, COL_Facility).ToString().Trim <> "") Then
                        cmbFacility.Text = Convert.ToString(C1BillingSettings.GetData(i, COL_Facility))

                        Dim _ID As String = Convert.ToString(cmbFacility.SelectedValue)

                        ogloSettings.AddProviderSettingsInTVP(ProviderID, "Facility", _ID, gnLoginID, _ClinicID, 0, "")
                    Else
                        ogloSettings.AddProviderSettingsInTVP(ProviderID, "Facility", "0", gnLoginID, _ClinicID, 0, "")

                    End If

                Else
                    ogloSettings.AddProviderSettingsInTVP(ProviderID, "Facility", "0", gnLoginID, _ClinicID, 0, "")
                End If


                If Not IsNothing(C1BillingSettings.GetData(i, COL_DFS)) Then

                    If (C1BillingSettings.GetData(i, COL_DFS).ToString().Trim <> "") Then

                        Dim _ID As String = ""
                        Dim _dr() As DataRow
                        _dr = _dtFeeschedules.[Select]("sFeeScheduleName = '" & Convert.ToString(C1BillingSettings.GetData(i, COL_DFS)).Replace("'", "''") & "'")
                        'cmbFeeSchedule.Text = Convert.ToString(C1BillingSettings.GetData(i, COL_DFS))
                        'Dim _ID As String = Convert.ToString(cmbFeeSchedule.SelectedValue)
                        If Not IsNothing(_dr) And _dr.Length > 0 Then
                            _ID = Convert.ToInt64(_dr(0).Item("nFeeScheduleID"))
                        End If
                        ogloSettings.AddProviderSettingsInTVP(ProviderID, "Fee Schedule", _ID, gnLoginID, _ClinicID, 0, "")
                    Else
                        ogloSettings.AddProviderSettingsInTVP(ProviderID, "Fee Schedule", "0", gnLoginID, _ClinicID, 0, "")
                    End If

                Else
                    ogloSettings.AddProviderSettingsInTVP(ProviderID, "Fee Schedule", "0", gnLoginID, _ClinicID, 0, "")
                End If


                If cmb_Feeschedules.SelectedIndex <> -1 Then
                    ogloSettings.AddProviderSettingsInTVP(ProviderID, "ClinicFeeSchedule", Convert.ToString(cmb_Feeschedules.SelectedValue), gnLoginID, _ClinicID, 0, "")
                Else
                    ogloSettings.AddProviderSettingsInTVP(ProviderID, "ClinicFeeSchedule", "0", gnLoginID, _ClinicID, 0, "")
                End If
            Next

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub GetBillingSettings()
        cmbTOS.SelectedIndex = -1
        cmbPOS.SelectedIndex = -1
        cmbBillingProvider.SelectedIndex = -1
        cmbRenderingProvider.SelectedIndex = -1
        cmbFacility.SelectedIndex = -1
        cmbFeeSchedule.SelectedIndex = -1
        Dim dt As New DataTable
        Try
            If cmbProvider.SelectedIndex <> -1 Then


                Dim clsSettings As New clsSettings()

                dt = clsSettings.GetBillingSettings()

                For j As Integer = 1 To C1BillingSettings.Rows.Count - 1
                    For i As Integer = 0 To dt.Rows.Count - 1
                        If (Convert.ToInt64(C1BillingSettings.GetData(j, COL_BPROVIDERID)) = Convert.ToInt64(dt.Rows(i)("nProviderID"))) Then

                            Select Case Convert.ToString(dt.Rows(i)("sName")).Trim()
                                Case "TypeOfService"
                                    cmbTOS.SelectedValue = Convert.ToInt64(dt.Rows(i)("sValue"))
                                    C1BillingSettings.SetData(j, COL_TOS, cmbTOS.Text)
                                    Exit Select
                                Case "PlaceOfService"
                                    cmbPOS.SelectedValue = Convert.ToInt64(dt.Rows(i)("sValue"))
                                    C1BillingSettings.SetData(j, COL_POS, cmbPOS.Text)
                                    Exit Select
                                Case "BillingProvider"
                                    cmbBillingProvider.SelectedValue = Convert.ToInt64(dt.Rows(i)("sValue"))
                                    C1BillingSettings.SetData(j, COL_BillProvider, cmbBillingProvider.Text)
                                    Exit Select
                                Case "RenderingProvider"
                                    cmbRenderingProvider.SelectedValue = Convert.ToInt64(dt.Rows(i)("sValue"))
                                    C1BillingSettings.SetData(j, COL_RenProvider, cmbRenderingProvider.Text)
                                    Exit Select
                                Case "Facility"
                                    'cmbFacility.SelectedValue = Convert.ToInt64(dt.Rows[i]["sValue"]); 
                                    cmbFacility.SelectedValue = Convert.ToString(dt.Rows(i)("sValue"))
                                    C1BillingSettings.SetData(j, COL_Facility, cmbFacility.Text)

                                    Exit Select
                                Case "Fee Schedule"
                                    'cmbFacility.SelectedValue = Convert.ToInt64(dt.Rows[i]["sValue"]); 
                                    cmbFeeSchedule.SelectedValue = Convert.ToString(dt.Rows(i)("sValue"))
                                    C1BillingSettings.SetData(j, COL_DFS, cmbFeeSchedule.Text)

                                    Exit Select
                                Case Else
                                    Exit Select
                            End Select
                        End If
                    Next
                Next

            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub GetBillingSettings(ByVal dt As DataTable)
        cmbTOS.SelectedIndex = -1
        cmbPOS.SelectedIndex = -1
        cmbBillingProvider.SelectedIndex = -1
        cmbRenderingProvider.SelectedIndex = -1
        cmbFacility.SelectedIndex = -1
        cmbFeeSchedule.SelectedIndex = -1
        Try
            If cmbProvider.SelectedIndex <> -1 Then

                For j As Integer = 1 To C1BillingSettings.Rows.Count - 1
                    For i As Integer = 0 To dt.Rows.Count - 1
                        If (Convert.ToInt64(C1BillingSettings.GetData(j, COL_BPROVIDERID)) = Convert.ToInt64(dt.Rows(i)("nProviderID"))) Then

                            Select Case Convert.ToString(dt.Rows(i)("sName")).Trim()
                                Case "TypeOfService"
                                    cmbTOS.SelectedValue = Convert.ToInt64(dt.Rows(i)("sValue"))
                                    C1BillingSettings.SetData(j, COL_TOS, cmbTOS.Text)
                                    Exit Select
                                Case "PlaceOfService"
                                    cmbPOS.SelectedValue = Convert.ToInt64(dt.Rows(i)("sValue"))
                                    C1BillingSettings.SetData(j, COL_POS, cmbPOS.Text)
                                    Exit Select
                                Case "BillingProvider"
                                    cmbBillingProvider.SelectedValue = Convert.ToInt64(dt.Rows(i)("sValue"))
                                    C1BillingSettings.SetData(j, COL_BillProvider, cmbBillingProvider.Text)
                                    Exit Select
                                Case "RenderingProvider"
                                    cmbRenderingProvider.SelectedValue = Convert.ToInt64(dt.Rows(i)("sValue"))
                                    C1BillingSettings.SetData(j, COL_RenProvider, cmbRenderingProvider.Text)
                                    Exit Select
                                Case "Facility"
                                    cmbFacility.SelectedValue = Convert.ToString(dt.Rows(i)("sValue"))
                                    C1BillingSettings.SetData(j, COL_Facility, cmbFacility.Text)

                                    Exit Select
                                Case "Fee Schedule"
                                    cmbFeeSchedule.SelectedValue = Convert.ToString(dt.Rows(i)("sValue"))
                                    C1BillingSettings.SetData(j, COL_DFS, cmbFeeSchedule.Text)

                                    Exit Select
                                Case Else
                                    Exit Select
                            End Select
                        End If
                    Next
                Next

            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub GetMidLevelBillingSettings()
        Dim clsSettings As New clsSettings()
        Dim dtMidlevelSettings As New DataTable
        Dim iRow As Integer

        Try
            dtMidlevelSettings = clsSettings.GetMidLevelBillingSettings()
            For i As Integer = 0 To dtMidlevelSettings.Rows.Count - 1
                If (dtMidlevelSettings.Rows(i)("nBillingSettingsType") = clsSettings.MidLevelSettingsType.SpecificProviderAllPlans.GetHashCode()) Then
                    iRow = C1MidLevelSettings.FindRow(Convert.ToString(dtMidlevelSettings.Rows(i)("nProviderID")), 1, MidLevelGridCol.ProviderID, True)
                    If (iRow <> -1) Then
                        C1MidLevelSettings.SetData(iRow, MidLevelGridCol.SettingsName, dtMidlevelSettings.Rows(i)("sMidLevelBillingType"))
                    End If
                    iRow = -1

                ElseIf (dtMidlevelSettings.Rows(i)("nBillingSettingsType") = clsSettings.MidLevelSettingsType.AllProvidersAllPlans.GetHashCode()) Then
                    CmbMidLevelSettings.Text = Convert.ToString(dtMidlevelSettings.Rows(i)("sMidLevelBillingType"))
                End If
            Next
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub GetMidLevelBillingSettings(ByVal dtMidlevelSettings As DataTable)
        Dim iRow As Integer

        Try
            For i As Integer = 0 To dtMidlevelSettings.Rows.Count - 1
                If (dtMidlevelSettings.Rows(i)("nBillingSettingsType") = clsSettings.MidLevelSettingsType.SpecificProviderAllPlans.GetHashCode()) Then
                    iRow = C1MidLevelSettings.FindRow(Convert.ToString(dtMidlevelSettings.Rows(i)("nProviderID")), 1, MidLevelGridCol.ProviderID, True)
                    If (iRow <> -1) Then
                        C1MidLevelSettings.SetData(iRow, MidLevelGridCol.SettingsName, dtMidlevelSettings.Rows(i)("sMidLevelBillingType"))
                    End If
                    iRow = -1

                ElseIf (dtMidlevelSettings.Rows(i)("nBillingSettingsType") = clsSettings.MidLevelSettingsType.AllProvidersAllPlans.GetHashCode()) Then
                    CmbMidLevelSettings.Text = Convert.ToString(dtMidlevelSettings.Rows(i)("sMidLevelBillingType"))
                End If
            Next
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub


    Private Sub SaveAppointmentDefaultSetting()

        Dim oSettings As New gloSettings.GeneralSettings(gloPMAdmin.mdlGeneral.GetConnectionString())
        Dim ogloSettings As New clsSettings
        Dim _DefaultDOS As Boolean
        Dim _DefaultFacility As Boolean
        Dim _DefaultRenderingProvider As Boolean

        Try
            For i As Integer = 1 To C1AppointmentDefault.Rows.Count - 1

                Dim nAppointmentTypeID As Int64 = 0
                nAppointmentTypeID = Convert.ToInt64(C1AppointmentDefault.GetData(i, COL_APPOINTMENTTYPEID))

                If Convert.ToString(C1AppointmentDefault.GetData(i, COL_DOS)).Trim() <> String.Empty AndAlso Convert.ToString(C1AppointmentDefault.GetData(i, COL_DOS)).Trim() <> "" Then
                    _DefaultDOS = 1
                Else
                    _DefaultDOS = 0
                End If

                If Convert.ToString(C1AppointmentDefault.GetData(i, COL_DFacility)).Trim() <> String.Empty AndAlso Convert.ToString(C1AppointmentDefault.GetData(i, COL_DFacility)).Trim() <> "" Then
                    _DefaultFacility = 1
                Else
                    _DefaultFacility = 0
                End If

                If Convert.ToString(C1AppointmentDefault.GetData(i, COL_RenderProvider)).Trim() <> String.Empty AndAlso Convert.ToString(C1AppointmentDefault.GetData(i, COL_RenderProvider)).Trim() <> "" Then
                    _DefaultRenderingProvider = 1
                Else
                    _DefaultRenderingProvider = 0
                End If

                ogloSettings.AddAppointmentDefaultSettings(nAppointmentTypeID, _DefaultDOS, _DefaultFacility, _DefaultRenderingProvider)
            Next
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Appointment, gloAuditTrail.ActivityCategory.AppointmentDefault, gloAuditTrail.ActivityType.Save, "Default Appointment Status Saved ", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPMAdmin, True)
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        End Try
    End Sub



    Private Sub GetANSISettings(ByVal _dt As DataTable, ByVal _bFilter As Boolean)
        Dim iRow As Integer

        Try
            C1ANSIVersionSettings.Rows.Count = 1

            For i As Integer = 0 To _dt.Rows.Count - 1
                If Convert.ToInt64(_dt.Rows(i)("nContactID")) <> 0 Then

                    If _bFilter Then
                        If Not IsDBNull(_dt.Rows(i)("sClaimVersion")) Or Not IsDBNull(_dt.Rows(i)("sEligVersion")) Then
                            C1ANSIVersionSettings.Rows.Add()
                            C1ANSIVersionSettings.SetData(C1ANSIVersionSettings.Rows.Count - 1, ANSIGrid.ContactID, _dt.Rows(i)("nContactID"))
                            C1ANSIVersionSettings.SetData(C1ANSIVersionSettings.Rows.Count - 1, ANSIGrid.ContactName, _dt.Rows(i)("sName"))
                            C1ANSIVersionSettings.SetData(C1ANSIVersionSettings.Rows.Count - 1, ANSIGrid.ClaimBatchSettings, _dt.Rows(i)("sClaimVersion"))
                            C1ANSIVersionSettings.SetData(C1ANSIVersionSettings.Rows.Count - 1, ANSIGrid.EligiblityRequestSettings, _dt.Rows(i)("sEligVersion"))
                        End If
                    Else
                        C1ANSIVersionSettings.Rows.Add()
                        C1ANSIVersionSettings.SetData(C1ANSIVersionSettings.Rows.Count - 1, ANSIGrid.ContactID, _dt.Rows(i)("nContactID"))
                        C1ANSIVersionSettings.SetData(C1ANSIVersionSettings.Rows.Count - 1, ANSIGrid.ContactName, _dt.Rows(i)("sName"))
                        C1ANSIVersionSettings.SetData(C1ANSIVersionSettings.Rows.Count - 1, ANSIGrid.ClaimBatchSettings, _dt.Rows(i)("sClaimVersion"))
                        C1ANSIVersionSettings.SetData(C1ANSIVersionSettings.Rows.Count - 1, ANSIGrid.EligiblityRequestSettings, _dt.Rows(i)("sEligVersion"))
                    End If
                End If
            Next

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub SetPaperFormSettings(ByVal _dt As DataTable, ByVal _bFilter As Boolean)
        Dim iRow As Integer

        Try
            c1CMSVersionPlanSetup.Rows.Count = 1

            For i As Integer = 0 To _dt.Rows.Count - 1
                If Convert.ToInt64(_dt.Rows(i)("nContactID")) <> 0 Then

                    If _bFilter Then
                        If Not IsDBNull(_dt.Rows(i)("sClaimVersion")) Then
                            c1CMSVersionPlanSetup.Rows.Add()
                            c1CMSVersionPlanSetup.SetData(c1CMSVersionPlanSetup.Rows.Count - 1, ANSIGrid.ContactID, _dt.Rows(i)("nContactID"))
                            c1CMSVersionPlanSetup.SetData(c1CMSVersionPlanSetup.Rows.Count - 1, ANSIGrid.ContactName, _dt.Rows(i)("sName"))
                            c1CMSVersionPlanSetup.SetData(c1CMSVersionPlanSetup.Rows.Count - 1, ANSIGrid.ClaimBatchSettings, _dt.Rows(i)("sClaimVersion"))
                        End If
                    Else
                        c1CMSVersionPlanSetup.Rows.Add()
                        c1CMSVersionPlanSetup.SetData(c1CMSVersionPlanSetup.Rows.Count - 1, ANSIGrid.ContactID, _dt.Rows(i)("nContactID"))
                        c1CMSVersionPlanSetup.SetData(c1CMSVersionPlanSetup.Rows.Count - 1, ANSIGrid.ContactName, _dt.Rows(i)("sName"))
                        c1CMSVersionPlanSetup.SetData(c1CMSVersionPlanSetup.Rows.Count - 1, ANSIGrid.ClaimBatchSettings, _dt.Rows(i)("sClaimVersion"))
                    End If
                End If
            Next

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub ModifyAlphaIISettingsControls(ByVal bAction As Boolean)
        Try
            Select Case bAction
                Case True
                    txtAlphaIIServerName.Enabled = True
                    txtAlphaIIDatabase.Enabled = True
                    cmbAlphaIIAuthentication.Enabled = True
                    txtAlphaIIUserName.Enabled = True
                    txtAlphaIIPassword.Enabled = True
                Case False
                    txtAlphaIIServerName.Enabled = False
                    txtAlphaIIDatabase.Enabled = False
                    cmbAlphaIIAuthentication.Enabled = False
                    txtAlphaIIUserName.Enabled = False
                    txtAlphaIIPassword.Enabled = False
            End Select
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    ''' '''


    Private Sub commonTextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtAppVersion.TextChanged, txtThresholdValue.TextChanged, txtServerPath.TextChanged, txtSendFacility.TextChanged, txtRxPswd.TextChanged, txtRecFacility.TextChanged, txtRecAppl.TextChanged, txtParticipantID.TextChanged, txtHL7FilePath.TextChanged, txtEARDirectory.TextChanged, txtDBVersion.TextChanged, tmStartTime.ValueChanged, tmEndTime.ValueChanged, PullChartsInterval.ValueChanged, numPatientCodeIncrement.ValueChanged, numLockOutAttempts.ValueChanged, cmbGender.SelectedIndexChanged, rbStaging.CheckedChanged, rbProduction.CheckedChanged, rbAdvRxStaging.CheckedChanged, rbAdvRxProduction.CheckedChanged, optPwdComplexNo.CheckedChanged, chkSurescript.CheckedChanged, chkSendUnassociatedDiagnosis.CheckedChanged, chkAdvanceRx.CheckedChanged, cmb_InsuranceType.SelectedIndexChanged, btnUp.Click, btnDown.Click, txtExchangeURL.TextChanged, txtExchangeDomain.TextChanged, cmbExchangeTimeZone.SelectedIndexChanged, numExchangeTimeZoneMin.ValueChanged, numExchangeTimeZoneHour.ValueChanged, cmbPRMaritalStatus5.SelectedIndexChanged, cmbPRMaritalStatus4.SelectedIndexChanged, cmbPRMaritalStatus3.SelectedIndexChanged, cmbPRMaritalStatus2.SelectedIndexChanged, cmbPRMaritalStatus1.SelectedIndexChanged, cmbBillingMaritalStatus5.SelectedIndexChanged, cmbBillingMaritalStatus4.SelectedIndexChanged, cmbBillingMaritalStatus3.SelectedIndexChanged, cmbBillingMaritalStatus2.SelectedIndexChanged, cmbBillingMaritalStatus1.SelectedIndexChanged, txtAlphaIIUserName.TextChanged, txtAlphaIIServerName.TextChanged, txtAlphaIIPassword.TextChanged, txtAlphaIIDatabase.TextChanged, rdbNone.CheckedChanged, rbCV_YOST.CheckedChanged, rbCV_Alpha2.CheckedChanged, chkShowMessage.CheckedChanged, cmbAlphaIIAuthentication.SelectedIndexChanged, cmbSlfPayAllwdAmnts.SelectedIndexChanged, cmbpatientaccountcode.SelectedIndexChanged, cmbFacilityType.SelectedIndexChanged, cmb_Feeschedules.SelectedIndexChanged, numUpDn_NoOfClaims.ValueChanged, numModifiers.ValueChanged, rbPrintFacilityYes.CheckedChanged, rbPrintFacilityNo.CheckedChanged, chbox_showTotalPayment.CheckedChanged, chbox_showPOS.CheckedChanged, chkEMG.CheckedChanged, chbox_showAllwdAmount.CheckedChanged, chbox_AddShowLabCol.CheckedChanged, chkDosTo.CheckedChanged, C1BillingSettings.TextChanged, cmbSpeakerVolume.SelectedIndexChanged, cmbRecieveFaxUser.SelectedIndexChanged, cmbPendingFaxUser.SelectedIndexChanged, cmbFollowUpUser.SelectedIndexChanged, cmbFAXCompression.SelectedIndexChanged, txtSendMail.TextChanged, txtFaxNoPrefix.TextChanged, txteFaxUserID.TextChanged, txteFaxPassword.TextChanged, txteFaxDownloadDir.TextChanged, optFAXreceiveYes.CheckedChanged, optFAXreceiveNo.CheckedChanged, numMaxNoOfRetries.ValueChanged, numLoadNoOfFaxes.ValueChanged, numFAXRetryInterval.ValueChanged, chkUseFaxNoPrefix.CheckedChanged, chkSendMail.CheckedChanged, chkInternetFax.CheckedChanged, txt_DMSV2RecoveryPath.TextChanged, numDMSImageDPI.ValueChanged, cmbRadioCategory.SelectedIndexChanged, cmbLabCategory.SelectedIndexChanged, cmbFaxCategory.SelectedIndexChanged, cmbCategoryDirective.SelectedIndexChanged, chkUseFileCompession.CheckedChanged, chkSplitDoc.CheckedChanged, chkRecoverDMSV2Doc.CheckedChanged, chk_DeleteDocAfterMigration.CheckedChanged, txtSQLUserID.TextChanged, txtSQLPassword.TextChanged, txtPMServerName.TextChanged, txtPMDatabaseName.TextChanged, rbtn_MigratetogloEMR50.CheckedChanged, rbtn_MigratetogloEMR40.CheckedChanged, rbtn_MigratetogloEMR28.CheckedChanged, optWindowsAuthentication.CheckedChanged, optSQLAuthentication.CheckedChanged, chk_PMDBSettings.CheckedChanged, C1Provider.TextChanged, txtSignaturetext.TextChanged, txtRptUserName.TextChanged, txtRptPassword.TextChanged, txtMCIRReportPath.TextChanged, txtDeclarartion.TextChanged, txtcoSignaturetext.TextChanged, txtCCDFilePath.TextChanged, txtAgeLimitPatientStrip.TextChanged, txtAgeLimitforWeeks.TextChanged, Rbtn_showDesc.CheckedChanged, Rbtn_showcode.CheckedChanged, Rbtn_ShowBoth.CheckedChanged, rbShowPercentileOnMouseHoover.CheckedChanged, rbShowPercentile.CheckedChanged, rbDontShowPercentile.CheckedChanged, NumUpDn_ImRminder.ValueChanged, cmbGeniusPaths.SelectedIndexChanged, chk_GrowthChart.CheckedChanged, chk_AgeFlag.CheckedChanged, chbImunizationReport.CheckedChanged, Chb_UseCodedhistory.CheckedChanged, txtRxHubDisclaimer.TextChanged, rbProblemList.CheckedChanged, rbChiefComplaint.CheckedChanged, chkOtherPatientType.CheckedChanged, rbShow8ICD9.CheckedChanged, rbShow4Modifier.CheckedChanged, rbShow4ICD9.CheckedChanged, rbShow2Modifier.CheckedChanged, rbICD9Driven.CheckedChanged, rbCPTDriven.CheckedChanged, chkSetCPTtoAllICD9.CheckedChanged, chkPrescriptionProvider.CheckedChanged, chkPatientQuestionnaire.CheckedChanged, c1Providers.TextChanged, cmbWriteOff.SelectedIndexChanged, cmbWithHold.SelectedIndexChanged, cmbDeductible.SelectedIndexChanged, cmbCopay.SelectedIndexChanged, cmbCoInsurance.SelectedIndexChanged, cmb_SlfPayDefaultFeeSchedule.SelectedIndexChanged
        Try
            If rdbNone.Checked Then
                Call ModifyAlphaIISettingsControls(False)
            ElseIf rbCV_YOST.Checked Then
                Call ModifyAlphaIISettingsControls(True)
            ElseIf rbCV_Alpha2.Checked Then
                Call ModifyAlphaIISettingsControls(True)
            End If
            If Not _Modified = True Then
                _Modified = True
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub frmSettings_New_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        Try
            If _blnValidate = True Then
                e.Cancel = True

            End If
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    '    Private Sub CloseForm()
    '        Try
    '            If _Modified = True Then
    '                Dim res As DialogResult = MessageBox.Show("Do you want to save changes to this record? ", gstrMessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information)
    '                If res = Windows.Forms.DialogResult.Yes Then
    '                    Call saveData()
    '                ElseIf res = Windows.Forms.DialogResult.Cancel Then
    'e.CancelButton=
    '                Else
    '                    Me.Close()
    '                End If
    '            Else
    '                Me.Close()
    '            End If
    '        Catch ex As Exception
    '            Me.Cursor = Cursors.Default
    '            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        End Try
    '    End Sub

    Private Sub c1Providers_AfterEdit(ByVal sender As System.Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles c1Providers.AfterEdit, C1BillingSettings.AfterEdit
        Try
            If Not _Modified = True Then
                _Modified = True
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub cmbWriteOff_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbWriteOff.MouseHover
        Try
            combo = DirectCast(sender, ComboBox)
            If cmbWriteOff.SelectedItem IsNot Nothing Then
                If getWidthofListItems(Convert.ToString(DirectCast(cmbWriteOff.Items(cmbWriteOff.SelectedIndex), DataRowView)("sDescription")), cmbWriteOff) >= cmbWriteOff.DropDownWidth Then
                    tooltip.Show(Convert.ToString(DirectCast(cmbWriteOff.Items(cmbWriteOff.SelectedIndex), DataRowView)("sDescription")), cmbWriteOff, cmbWriteOff.Right - 600, cmbWriteOff.Top - 50)
                End If
            End If

        Catch Ex As Exception
            MessageBox.Show(Ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

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

    Private Sub ShowTooltipOnCopayComboBox(ByVal sender As Object, ByVal e As DrawItemEventArgs)
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

    Private Sub ShowTooltipOnDeductibleComboBox(ByVal sender As Object, ByVal e As DrawItemEventArgs)
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

    Private Sub ShowTooltipOnCoInsComboBox(ByVal sender As Object, ByVal e As DrawItemEventArgs)
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

    Private Sub ShowTooltipOnWithHoldComboBox(ByVal sender As Object, ByVal e As DrawItemEventArgs)
        combo = DirectCast(sender, ComboBox)
        If combo.Items.Count > 0 AndAlso e.Index >= 0 Then
            Dim window As System.Windows.Forms.IWin32Window

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

    Private Sub ShowTooltipOnMidLevelBillingCombo(ByVal sender As Object, ByVal e As DrawItemEventArgs)
        combo = DirectCast(sender, ComboBox)
        If combo.Items.Count > 0 AndAlso e.Index >= 0 Then

            e.DrawBackground()
            Using br As New SolidBrush(e.ForeColor)
                e.Graphics.DrawString(combo.GetItemText(combo.Items(e.Index)).ToString(), e.Font, br, e.Bounds)
            End Using

            If (e.State And DrawItemState.Selected) = DrawItemState.Selected Then
                If combo.DroppedDown Then
                    If getWidthofListItems(combo.GetItemText(combo.Items(e.Index)).ToString(), combo) >= combo.DropDownWidth Then
                        Me.tooltip.Show(combo.GetItemText(combo.Items(e.Index)), combo, e.Bounds.Right - 240, e.Bounds.Top + 4)
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

    Private Sub ShowTooltipOnInsClmStartFilingCombo(ByVal sender As Object, ByVal e As DrawItemEventArgs)
        combo = DirectCast(sender, ComboBox)
        If combo.Items.Count > 0 AndAlso e.Index >= 0 Then

            e.DrawBackground()
            Using br As New SolidBrush(e.ForeColor)
                e.Graphics.DrawString(combo.GetItemText(combo.Items(e.Index)).ToString(), e.Font, br, e.Bounds)
            End Using

            If (e.State And DrawItemState.Selected) = DrawItemState.Selected Then
                If combo.DroppedDown Then
                    If getWidthofListItems(combo.GetItemText(combo.Items(e.Index)).ToString(), combo) >= combo.DropDownWidth Then
                        Me.tooltip.Show(combo.GetItemText(combo.Items(e.Index)), combo, e.Bounds.Right - 240, e.Bounds.Top + 4)
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

    Private Sub ShowTooltipOnInsClmRebillFilingCombo(ByVal sender As Object, ByVal e As DrawItemEventArgs)
        combo = DirectCast(sender, ComboBox)
        If combo.Items.Count > 0 AndAlso e.Index >= 0 Then

            e.DrawBackground()
            Using br As New SolidBrush(e.ForeColor)
                e.Graphics.DrawString(combo.GetItemText(combo.Items(e.Index)).ToString(), e.Font, br, e.Bounds)
            End Using

            If (e.State And DrawItemState.Selected) = DrawItemState.Selected Then
                If combo.DroppedDown Then
                    If getWidthofListItems(combo.GetItemText(combo.Items(e.Index)).ToString(), combo) >= combo.DropDownWidth Then
                        Me.tooltip.Show(combo.GetItemText(combo.Items(e.Index)), combo, e.Bounds.Right - 240, e.Bounds.Top + 4)
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

    Private Sub ShowTooltipOnPatAccFilingCombo(ByVal sender As Object, ByVal e As DrawItemEventArgs)
        combo = DirectCast(sender, ComboBox)
        If combo.Items.Count > 0 AndAlso e.Index >= 0 Then

            e.DrawBackground()
            Using br As New SolidBrush(e.ForeColor)
                e.Graphics.DrawString(combo.GetItemText(combo.Items(e.Index)).ToString(), e.Font, br, e.Bounds)
            End Using

            If (e.State And DrawItemState.Selected) = DrawItemState.Selected Then
                If combo.DroppedDown Then
                    If getWidthofListItems(combo.GetItemText(combo.Items(e.Index)).ToString(), combo) >= combo.DropDownWidth Then
                        Me.tooltip.Show(combo.GetItemText(combo.Items(e.Index)), combo, e.Bounds.Right - 240, e.Bounds.Top + 4)
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

    Private Sub ShowTooltipOnPmntPlanFilingCombo(ByVal sender As Object, ByVal e As DrawItemEventArgs)
        combo = DirectCast(sender, ComboBox)
        If combo.Items.Count > 0 AndAlso e.Index >= 0 Then

            e.DrawBackground()
            Using br As New SolidBrush(e.ForeColor)
                e.Graphics.DrawString(combo.GetItemText(combo.Items(e.Index)).ToString(), e.Font, br, e.Bounds)
            End Using

            If (e.State And DrawItemState.Selected) = DrawItemState.Selected Then
                If combo.DroppedDown Then
                    If getWidthofListItems(combo.GetItemText(combo.Items(e.Index)).ToString(), combo) >= combo.DropDownWidth Then
                        Me.tooltip.Show(combo.GetItemText(combo.Items(e.Index)), combo, e.Bounds.Right - 240, e.Bounds.Top + 4)
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

    Private Function getWidthofListItems(ByVal _text As String, ByVal combo As ComboBox) As Integer

        Dim g As Graphics = Me.CreateGraphics()
        Dim s As SizeF = g.MeasureString(_text, combo.Font)
        Dim width As Integer = Convert.ToInt32(s.Width)
        Return width
    End Function


    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.


        cmbWriteOff.DrawMode = DrawMode.OwnerDrawFixed
        AddHandler cmbWriteOff.DrawItem, AddressOf ShowTooltipOnWriteOffComboBox
        cmbCopay.DrawMode = DrawMode.OwnerDrawFixed
        AddHandler cmbCopay.DrawItem, AddressOf ShowTooltipOnCopayComboBox
        cmbDeductible.DrawMode = DrawMode.OwnerDrawFixed
        AddHandler cmbDeductible.DrawItem, AddressOf ShowTooltipOnDeductibleComboBox
        cmbCoInsurance.DrawMode = DrawMode.OwnerDrawFixed
        AddHandler cmbCoInsurance.DrawItem, AddressOf ShowTooltipOnCoInsComboBox
        cmbWithHold.DrawMode = DrawMode.OwnerDrawFixed
        AddHandler cmbWithHold.DrawItem, AddressOf ShowTooltipOnWithHoldComboBox
        CmbMidLevelSettings.DrawMode = DrawMode.OwnerDrawFixed
        AddHandler CmbMidLevelSettings.DrawItem, AddressOf ShowTooltipOnMidLevelBillingCombo
        cmbInsClmStartFUAction.DrawMode = DrawMode.OwnerDrawFixed
        AddHandler cmbInsClmStartFUAction.DrawItem, AddressOf ShowTooltipOnInsClmStartFilingCombo
        cmbInsClmRebillFUAction.DrawMode = DrawMode.OwnerDrawFixed
        AddHandler cmbInsClmRebillFUAction.DrawItem, AddressOf ShowTooltipOnInsClmRebillFilingCombo
        cmbPatAccStartFUAction.DrawMode = DrawMode.OwnerDrawFixed
        AddHandler cmbPatAccStartFUAction.DrawItem, AddressOf ShowTooltipOnPatAccFilingCombo
        cmbPmntPlanFUAction.DrawMode = DrawMode.OwnerDrawFixed
        AddHandler cmbPmntPlanFUAction.DrawItem, AddressOf ShowTooltipOnPmntPlanFilingCombo

        cmbCountry.DrawMode = DrawMode.OwnerDrawFixed
        AddHandler cmbCountry.DrawItem, AddressOf ShowTooltipOnWriteOffComboBox

        cmbFutureApptType.DrawMode = DrawMode.OwnerDrawFixed
        AddHandler cmbFutureApptType.DrawItem, AddressOf ShowTooltipOnWriteOffComboBox

        cmbSameDayApptType.DrawMode = DrawMode.OwnerDrawFixed
        AddHandler cmbSameDayApptType.DrawItem, AddressOf ShowTooltipOnWriteOffComboBox

        cmbExternalCollectionFUAction.DrawMode = DrawMode.OwnerDrawFixed
        AddHandler cmbExternalCollectionFUAction.DrawItem, AddressOf ShowTooltipOnWriteOffComboBox
    End Sub

    Private Sub cmbWriteOff_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbWriteOff.MouseLeave
        tooltip.Hide(combo)
    End Sub

    Private Sub cmbCopay_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbCopay.MouseLeave
        tooltip.Hide(cmbCopay)
    End Sub

    Private Sub cmbDeductible_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbDeductible.MouseLeave
        tooltip.Hide(combo)
    End Sub

    Private Sub cmbCoInsurance_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbCoInsurance.MouseLeave
        tooltip.Hide(combo)
    End Sub

    Private Sub cmbWithHold_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbWithHold.MouseLeave
        tooltip.Hide(combo)
    End Sub

    Private Sub cmbInsClmStartFUAction_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs)
        tooltip.Hide(combo)
    End Sub

    Private Sub cmbInsClmRebillFUAction_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs)
        tooltip.Hide(combo)
    End Sub

    Private Sub cmbPatAccStartFUAction_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs)
        tooltip.Hide(combo)
    End Sub

    Private Sub cmbPmntPlanFUAction_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs)
        tooltip.Hide(combo)
    End Sub

    Private Sub cmbCopay_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbCopay.MouseHover
        Try
            combo = DirectCast(sender, ComboBox)
            If cmbCopay.SelectedItem IsNot Nothing Then
                If getWidthofListItems(Convert.ToString(DirectCast(cmbCopay.Items(cmbCopay.SelectedIndex), DataRowView)("sDescription")), cmbCopay) >= cmbCopay.DropDownWidth Then
                    tooltip.Show(Convert.ToString(DirectCast(cmbCopay.Items(cmbCopay.SelectedIndex), DataRowView)("sDescription")), cmbCopay, cmbCopay.Right - 600, cmbCopay.Top - 85)
                End If
            End If
        Catch Ex As Exception
            MessageBox.Show(Ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub cmbDeductible_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbDeductible.MouseHover
        Try
            combo = DirectCast(sender, ComboBox)
            If cmbDeductible.SelectedItem IsNot Nothing Then
                If getWidthofListItems(Convert.ToString(DirectCast(cmbDeductible.Items(cmbDeductible.SelectedIndex), DataRowView)("sDescription")), cmbDeductible) >= cmbDeductible.DropDownWidth Then
                    tooltip.Show(Convert.ToString(DirectCast(cmbDeductible.Items(cmbDeductible.SelectedIndex), DataRowView)("sDescription")), cmbDeductible, cmbDeductible.Right - 600, cmbDeductible.Top - 118)
                End If
            End If
        Catch Ex As Exception
            MessageBox.Show(Ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub cmbCoInsurance_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbCoInsurance.MouseHover
        Try

            combo = DirectCast(sender, ComboBox)
            If cmbCoInsurance.SelectedItem IsNot Nothing Then
                If getWidthofListItems(Convert.ToString(DirectCast(cmbCoInsurance.Items(cmbCoInsurance.SelectedIndex), DataRowView)("sDescription")), cmbCoInsurance) >= cmbCoInsurance.DropDownWidth Then
                    tooltip.Show(Convert.ToString(DirectCast(cmbCoInsurance.Items(cmbCoInsurance.SelectedIndex), DataRowView)("sDescription")), cmbCoInsurance, cmbCoInsurance.Right - 600, cmbCoInsurance.Top - 153)
                End If
            End If
        Catch Ex As Exception
            MessageBox.Show(Ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub cmbWithHold_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbWithHold.MouseHover
        Try
            combo = DirectCast(sender, ComboBox)
            If cmbWithHold.SelectedItem IsNot Nothing Then
                If getWidthofListItems(Convert.ToString(DirectCast(cmbWithHold.Items(cmbWithHold.SelectedIndex), DataRowView)("sDescription")), cmbWithHold) >= cmbWithHold.DropDownWidth Then
                    tooltip.Show(Convert.ToString(DirectCast(cmbWithHold.Items(cmbWithHold.SelectedIndex), DataRowView)("sDescription")), cmbWithHold, cmbWithHold.Right - 600, cmbWithHold.Top - 186)
                End If
            End If
        Catch Ex As Exception
            MessageBox.Show(Ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub cmbInsClmStartFUAction_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            combo = DirectCast(sender, ComboBox)
            If cmbInsClmStartFUAction.SelectedItem IsNot Nothing Then
                If getWidthofListItems(Convert.ToString(DirectCast(cmbInsClmStartFUAction.Items(cmbInsClmStartFUAction.SelectedIndex), DataRowView)("sFollowUpDesc")), cmbInsClmStartFUAction) >= cmbInsClmStartFUAction.DropDownWidth Then
                    tooltip.Show(Convert.ToString(DirectCast(cmbInsClmStartFUAction.Items(cmbInsClmStartFUAction.SelectedIndex), DataRowView)("sFollowUpDesc")), cmbInsClmStartFUAction, cmbInsClmStartFUAction.Right - 600, cmbInsClmStartFUAction.Top - 45)
                End If
            End If
        Catch Ex As Exception
            MessageBox.Show(Ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub cmbInsClmRebillFUAction_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            combo = DirectCast(sender, ComboBox)
            If cmbInsClmRebillFUAction.SelectedItem IsNot Nothing Then
                If getWidthofListItems(Convert.ToString(DirectCast(cmbInsClmRebillFUAction.Items(cmbInsClmRebillFUAction.SelectedIndex), DataRowView)("sFollowUpDesc")), cmbInsClmRebillFUAction) >= cmbInsClmRebillFUAction.DropDownWidth Then
                    tooltip.Show(Convert.ToString(DirectCast(cmbInsClmRebillFUAction.Items(cmbInsClmRebillFUAction.SelectedIndex), DataRowView)("sFollowUpDesc")), cmbInsClmRebillFUAction, cmbInsClmRebillFUAction.Right - 600, cmbInsClmRebillFUAction.Top - 100)
                End If
            End If
        Catch Ex As Exception
            MessageBox.Show(Ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub cmbPatAccStartFUAction_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            combo = DirectCast(sender, ComboBox)
            If cmbPatAccStartFUAction.SelectedItem IsNot Nothing Then
                If getWidthofListItems(Convert.ToString(DirectCast(cmbPatAccStartFUAction.Items(cmbPatAccStartFUAction.SelectedIndex), DataRowView)("sFollowUpDesc")), cmbPatAccStartFUAction) >= cmbPatAccStartFUAction.DropDownWidth Then
                    tooltip.Show(Convert.ToString(DirectCast(cmbPatAccStartFUAction.Items(cmbPatAccStartFUAction.SelectedIndex), DataRowView)("sFollowUpDesc")), cmbPatAccStartFUAction, cmbPatAccStartFUAction.Right - 600, cmbPatAccStartFUAction.Top - 100)
                End If
            End If
        Catch Ex As Exception
            MessageBox.Show(Ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub cmbPmntPlanFUAction_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            combo = DirectCast(sender, ComboBox)
            If cmbPmntPlanFUAction.SelectedItem IsNot Nothing Then
                If getWidthofListItems(Convert.ToString(DirectCast(cmbPmntPlanFUAction.Items(cmbPmntPlanFUAction.SelectedIndex), DataRowView)("sFollowUpDesc")), cmbPmntPlanFUAction) >= cmbPmntPlanFUAction.DropDownWidth Then
                    tooltip.Show(Convert.ToString(DirectCast(cmbPmntPlanFUAction.Items(cmbPmntPlanFUAction.SelectedIndex), DataRowView)("sFollowUpDesc")), cmbPmntPlanFUAction, cmbPmntPlanFUAction.Right - 600, cmbPmntPlanFUAction.Top - 40)
                End If
            End If
        Catch Ex As Exception
            MessageBox.Show(Ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    'code added by pradeep 
    Private Sub ChkUseSitePrefix_CheckStateChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkUseSitePrefix.CheckStateChanged
        If ChkUseSitePrefix.CheckState = CheckState.Unchecked Then

            Dim strmessage As String
            strmessage = "You are about to disable Site Prefix setting. Changing the setting could have unpredictable results while replication. It is recommended to enable this setting if you are using replication. Still do you wish to proceed?"
            If MessageBox.Show(strmessage, gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information) = Windows.Forms.DialogResult.No Then
                ChkUseSitePrefix.CheckState = CheckState.Checked
            Else

                flagPrefixSettingOFF = False
            End If
        Else

            flagPrefixSettingOFF = True
        End If
    End Sub

    Private Sub txtReportServerName_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtReportServerName.KeyPress
        If Regex.IsMatch(e.KeyChar, "^[-a-zA-Z0-9\:\b\s\._]") = False Then '"^[a-zA-Z0-9\:\b\s\.]"
            e.Handled = True
        End If
    End Sub

    Private Sub txtReportFolderName_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtReportFolderName.KeyPress
        If Regex.IsMatch(e.KeyChar, "^[-a-zA-Z0-9\b\s._]") = False Then '"^[a-zA-Z0-9\b\s]"
            e.Handled = True
        End If
    End Sub

    Private Sub txtReportVirtualDir_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtReportVirtualDir.KeyPress
        If Regex.IsMatch(e.KeyChar, "^[-a-zA-Z0-9_\b]") = False Then
            e.Handled = True
        End If
    End Sub


    'Private Sub VisibilityRevenueCode(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles RbUbBilling_No.MouseClick
    '    If (RbUbBilling_No.Checked) Then
    '        TxtRevenuecode.Visible = False
    '        btnBrwsrRevenuecode.Visible = False
    '        BtnDellRevenueCode.Visible = False
    '        Label549.Visible = False
    '    End If


    'End Sub


    'Private Sub Revenue(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles RbUbBilling_yes.MouseClick
    '    If (RbUbBilling_yes.Checked) Then
    '        TxtRevenuecode.Visible = True
    '        btnBrwsrRevenuecode.Visible = True
    '        BtnDellRevenueCode.Visible = True
    '        Label549.Visible = True
    '    End If
    'End Sub

    '20100820 vijay geting revenue code

    Private Function GetRevenueCode() As DataTable
        Dim conn As New SqlConnection()
        Dim objCmd As SqlCommand
        Dim oDtadptr As New SqlDataAdapter()
        Dim _dtRevenueCode As New DataTable()
        Dim _strSQL As String = ""
        Try
            conn.ConnectionString = gloPMAdmin.mdlGeneral.GetConnectionString()

            conn.Open()
            _strSQL = "select isnull(sRevenueCode,'') as sRevenueCode from BL_UB_RevenueCode_MST where ISNULL(bIsActive,0) =1 union select ISNULL(sSettingsValue,'') as sRevenueCode from settings where sSettingsName='UB04_RevenueCode'  and sSettingsValue <> '' "

            objCmd = New SqlCommand(_strSQL, conn)
            oDtadptr = New SqlDataAdapter(objCmd)
            oDtadptr.Fill(_dtRevenueCode)
            Return _dtRevenueCode
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            'Return 
        Finally
            conn.Close()
        End Try

    End Function

    Private Sub RbUbBilling_yes_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RbUbBilling_yes.CheckedChanged
        Dim objSetting As New clsSettings
        If (objSetting.GetSettings() = True) Then
            If (RbUbBilling_yes.Checked) Then

                CmbRevenueCode.SelectedItem = objSetting.UB04_RevenueCode
                CmbRevenueCode.Enabled = True
                TxtTypeBill.Text = objSetting.UB04_TypeOfBilling
                TxtTypeBill.Enabled = True
                If (objSetting.UB04_AdmisionType <> "") Then
                    TxtAdmissionType.Text = objSetting.UB04_AdmisionType
                Else
                    TxtAdmissionType.Text = ""
                End If
                TxtAdmissionType.Enabled = True
                If (objSetting.UB04_AdmisionSource <> "") Then
                    TxtAdmisionSource.Text = objSetting.UB04_AdmisionSource
                Else
                    TxtAdmisionSource.Text = ""
                End If
                TxtAdmisionSource.Enabled = True
                If (objSetting.UB04_DischargeStatus <> "") Then
                    txtDischargeStatus.Text = objSetting.UB04_DischargeStatus
                Else
                    txtDischargeStatus.Text = ""
                End If

                txtDischargeStatus.Enabled = True
                '' Label549.Visible = True
                RbUbBilling_yes.Font = New Font("Tahoma", 9, FontStyle.Bold)

                ''Expanded claim Structure
                C1ExpandedClaimSettings.Rows(1).Visible = True
                C1ExpandedClaimSettings.Rows(2).Visible = True
                C1ExpandedClaimSettings.Rows(3).Visible = True
                C1ExpandedClaimSettings.Rows(4).Visible = True
                ''
            Else
                RbUbBilling_yes.Font = New Font("Tahoma", 9, FontStyle.Regular)
                C1ExpandedClaimSettings.Rows(1).Visible = True
                C1ExpandedClaimSettings.Rows(2).Visible = True
                C1ExpandedClaimSettings.Rows(3).Visible = False
                C1ExpandedClaimSettings.Rows(4).Visible = False
            End If
        End If
    End Sub

    Private Sub RbUbBilling_No_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RbUbBilling_No.CheckedChanged
        If (RbUbBilling_No.Checked) Then
            CmbRevenueCode.SelectedItem = ""
            CmbRevenueCode.Enabled = False
            TxtTypeBill.Text = ""
            TxtTypeBill.Enabled = False
            TxtAdmissionType.Text = ""
            TxtAdmissionType.Enabled = False
            TxtAdmisionSource.Text = ""
            TxtAdmisionSource.Enabled = False
            txtDischargeStatus.Text = ""
            txtDischargeStatus.Enabled = False
            'Panel103.Visible = False
            RbUbBilling_No.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            RbUbBilling_No.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub


    Private Sub CmbMidLevelSettings_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmbMidLevelSettings.MouseHover
        Try
            combo = DirectCast(sender, ComboBox)
            If CmbMidLevelSettings.SelectedItem IsNot Nothing Then
                If getWidthofListItems(Convert.ToString(DirectCast(CmbMidLevelSettings.Items(CmbMidLevelSettings.SelectedIndex), DataRowView)("sMidLevelBillingType")), CmbMidLevelSettings) >= CmbMidLevelSettings.DropDownWidth Then
                    tooltip.Show(Convert.ToString(DirectCast(CmbMidLevelSettings.Items(CmbMidLevelSettings.SelectedIndex), DataRowView)("sMidLevelBillingType")), CmbMidLevelSettings, CmbMidLevelSettings.Right - 500, CmbMidLevelSettings.Top + 20)
                End If
            End If
        Catch Ex As Exception
            MessageBox.Show(Ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
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
                objAudit.CreateLog(clsAudit.enmActivityType.Other, "Sequential patient code changed and change accepted by clicking on yes", gstrLoginName, gstrClientMachineName)

            End If
        End If
    End Sub

    Private Sub txtSequentialPatientCode_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles txtSequentialPatientCode.MouseDown

    End Sub

    Private Sub txtSequentialPatientCode_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtSequentialPatientCode.KeyDown

    End Sub

    Private Sub txtSequentialPatientCode_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtSequentialPatientCode.KeyUp
        txtSequentialPatientCode.Text = txtSequentialPatientCode.Text.ToString.Replace(" ", "")
    End Sub

    Private Sub rbPatientAccountFeatureEnabledNO_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbPatientAccountFeatureEnabledNO.CheckedChanged
        RemoveHandler rbPatientAccountFeatureEnabledYES.CheckedChanged, AddressOf rbPatientAccountFeatureEnabledYES_CheckedChanged
        RemoveHandler rbPatientAccountFeatureEnabledNO.CheckedChanged, AddressOf rbPatientAccountFeatureEnabledNO_CheckedChanged
        If rbPatientAccountFeatureEnabledNO.Checked = True And blnProcessFlag = False Then
            If Validate_PAF() Then
                blnProcessFlag = True
                rbPatientAccountFeatureEnabledNO.Checked = False
                rbPatientAccountFeatureEnabledYES.Checked = True
                blnProcessFlag = False
            End If
        End If
        AddHandler rbPatientAccountFeatureEnabledYES.CheckedChanged, AddressOf rbPatientAccountFeatureEnabledYES_CheckedChanged
        AddHandler rbPatientAccountFeatureEnabledNO.CheckedChanged, AddressOf rbPatientAccountFeatureEnabledNO_CheckedChanged

    End Sub

    Private Sub rbPatientAccountFeatureEnabledYES_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbPatientAccountFeatureEnabledYES.CheckedChanged

        Try
            RemoveHandler rbPatientAccountFeatureEnabledYES.CheckedChanged, AddressOf rbPatientAccountFeatureEnabledYES_CheckedChanged
            RemoveHandler rbPatientAccountFeatureEnabledNO.CheckedChanged, AddressOf rbPatientAccountFeatureEnabledNO_CheckedChanged

            If rbPatientAccountFeatureEnabledYES.Checked = True Then
                If rdoV2.Checked Then
                    Dim res As DialogResult = MessageBox.Show("Warning – After turning on or off the Patient Account Feature, you must close each running gloEMR and gloPM desktop for all users.  Leaving a desktop running might cause conflicts.  Continue?", gstrMessageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning)
                    If res = Windows.Forms.DialogResult.OK Then
                        rbPatientAccountFeatureEnabledYES.Checked = True
                        rbPatientAccountFeatureEnabledNO.Checked = False
                    ElseIf res = Windows.Forms.DialogResult.Cancel Then
                        rbPatientAccountFeatureEnabledYES.Checked = False
                        rbPatientAccountFeatureEnabledNO.Checked = True
                    End If
                Else
                    MessageBox.Show("You cannot enable the Patient Account Feature while using electronic statement ‘V1’.   Please adjust your electronic statement version first. ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    rbPatientAccountFeatureEnabledYES.Checked = False
                    rbPatientAccountFeatureEnabledNO.Checked = True
                End If

            End If
        Catch ex As Exception
        Finally

            AddHandler rbPatientAccountFeatureEnabledYES.CheckedChanged, AddressOf rbPatientAccountFeatureEnabledYES_CheckedChanged
            AddHandler rbPatientAccountFeatureEnabledNO.CheckedChanged, AddressOf rbPatientAccountFeatureEnabledNO_CheckedChanged
        End Try

    End Sub


    Private Sub rdoV1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            RemoveHandler rdoV1.CheckedChanged, AddressOf rdoV1_CheckedChanged
            RemoveHandler rdoV2.CheckedChanged, AddressOf rdoV2_CheckedChanged

            If rdoV1.Checked Then

                If rbPatientAccountFeatureEnabledYES.Checked Then

                    Dim res As DialogResult = MessageBox.Show("You cannot use statement layout ‘V1’ because the Patient Account Feature is enabled.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    rdoV1.Checked = False
                    rdoV2.Checked = True

                Else

                    Dim _msgStr As String
                    _msgStr = "Warning – " & Environment.NewLine &
                    "Make sure your  clearinghouse supports the new statement file version you are selecting.   gloStream Supported clearinghouses should be fine. " & Environment.NewLine & "If you use an unsupported clearinghouse, you need to coordinate with them first."
                    Dim res As DialogResult = MessageBox.Show(_msgStr, gstrMessageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning)
                    If res = Windows.Forms.DialogResult.OK Then
                        rdoV1.Checked = True
                        rdoV2.Checked = False
                    ElseIf res = Windows.Forms.DialogResult.Cancel Then
                        rdoV1.Checked = False
                        rdoV2.Checked = True
                    End If

                End If

            End If

        Catch ex As Exception
        Finally

            AddHandler rdoV1.CheckedChanged, AddressOf rdoV1_CheckedChanged
            AddHandler rdoV2.CheckedChanged, AddressOf rdoV2_CheckedChanged
        End Try
    End Sub

    Private Sub rdoV2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            RemoveHandler rdoV1.CheckedChanged, AddressOf rdoV1_CheckedChanged
            RemoveHandler rdoV2.CheckedChanged, AddressOf rdoV2_CheckedChanged

            If rdoV2.Checked Then
                Dim _msgStr As String
                _msgStr = "Warning – " & Environment.NewLine &
                "Make sure your  clearinghouse supports the new statement file version you are selecting.   gloStream Supported clearinghouses should be fine. " & Environment.NewLine & "If you use an unsupported clearinghouse, you need to coordinate with them first."
                Dim res As DialogResult = MessageBox.Show(_msgStr, gstrMessageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning)
                If res = Windows.Forms.DialogResult.OK Then
                    rdoV2.Checked = True
                    rdoV1.Checked = False
                ElseIf res = Windows.Forms.DialogResult.Cancel Then
                    rdoV2.Checked = False
                    rdoV1.Checked = True
                End If

            End If


        Catch ex As Exception
        Finally

            AddHandler rdoV1.CheckedChanged, AddressOf rdoV1_CheckedChanged
            AddHandler rdoV2.CheckedChanged, AddressOf rdoV2_CheckedChanged
        End Try
    End Sub

    Private Sub rbMultipleGuranterAllowNO_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbMultipleGuranterAllowNO.CheckedChanged
        If rbMultipleGuranterAllowNO.Checked = False And blnMultipleGuarantorsProcessFlag = False Then
            'MessageBox.Show("This feature will be removed in the future. Please contact Customer Care to discuss alternatives.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            MessageBox.Show("Warning - In a future version, this feature will be removed.  Refer to the Guarantors Quick Guide.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub


    'Private Function AddExpandedClaimSettings(ByVal nSettingsID As Int64, ByVal nCompanyID As Int64, ByVal nContactID As Int64, ByVal nSettingsLevel As Int16, ByVal nSettingType As Int16, ByVal nServiceLines As Int16, ByVal nDiagnosis As Int16, ByVal ClinicID As Int64, ByVal UserID As Int64) As Boolean
    '    Dim oDB As New gloDatabaseLayer.DBLayer(gloPMAdmin.mdlGeneral.GetConnectionString())
    '    Dim oDBParameters As New gloDatabaseLayer.DBParameters()
    '    Try
    '        oDB.Connect(False)

    '        oDBParameters.Add("@nSettingsID", nSettingsID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt)
    '        oDBParameters.Add("@nCompanyID", nCompanyID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt)
    '        oDBParameters.Add("@nContactID", nContactID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt)
    '        oDBParameters.Add("@nSettingLevel", nSettingsLevel, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Int)
    '        oDBParameters.Add("@nSettingType", nSettingType, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Int)
    '        oDBParameters.Add("@nServiceLines", nServiceLines, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Int)
    '        oDBParameters.Add("@nDiagnosis", nDiagnosis, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Int)
    '        oDBParameters.Add("@nClinicID", ClinicID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar)
    '        oDBParameters.Add("@nUserID", UserID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar)
    '        ' oDBParameters.Add("@nUserClinicFlag", UserClinicFlag.GetHashCode(), System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar)

    '        oDB.Execute("BL_INUP_ExpandedClaimSettings", oDBParameters)


    '        Return True
    '    Catch DBErr As gloDatabaseLayer.DBException
    '        MessageBox.Show(DBErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        Return False
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        Return False
    '    Finally
    '        oDB.Disconnect()
    '        oDBParameters.Dispose()
    '        oDB.Dispose()
    '    End Try
    'End Function

    Private Function GetExpandedClaimSettingsValue(ByVal nSettingsLevel As Int16) As String

        Dim dtClaim As DataTable

        Dim _strSQL As String = ""
        Dim oDB As New gloDatabaseLayer.DBLayer(gloPMAdmin.mdlGeneral.GetConnectionString())
        Try

            If (RbUbBilling_yes.Checked) Then
                C1ExpandedClaimSettings.Rows(1).Visible = True
                C1ExpandedClaimSettings.Rows(2).Visible = True
                C1ExpandedClaimSettings.Rows(3).Visible = True
                C1ExpandedClaimSettings.Rows(4).Visible = True
            Else
                C1ExpandedClaimSettings.Rows(1).Visible = True
                C1ExpandedClaimSettings.Rows(2).Visible = True
                C1ExpandedClaimSettings.Rows(3).Visible = False
                C1ExpandedClaimSettings.Rows(4).Visible = False
            End If


            oDB.Connect(False)

            _strSQL = "select isnull(nServiceLines,0) as ServiceLines,isnull(nDiagnosis,0) as Diagnosis,nSettingType  from BL_ExpandedClaimSettings where nSettingLevel= " & nSettingsLevel & " Order by nSettingType "
            oDB.Retrive_Query(_strSQL, dtClaim)
            Dim sCharges As String = String.Empty
            Dim sDiagnoses As String = String.Empty

            If dtClaim.Rows.Count > 0 Then

                For i As Int16 = 0 To dtClaim.Rows.Count - 1
                    sCharges = IIf(dtClaim.Rows(i)(0) = "0", 0, dtClaim.Rows(i)(0))
                    sDiagnoses = IIf(dtClaim.Rows(i)(1) = "0", 0, dtClaim.Rows(i)(1))

                    If gloSettings.TypeOfBilling.Paper.GetHashCode() = dtClaim.Rows(i)(2) Then
                        C1ExpandedClaimSettings.SetData((i + 1), COL_CLAIMPERCHARGES, sCharges)
                        C1ExpandedClaimSettings.SetData((i + 1), COL_DIAGNOSISPERCHARGES, sDiagnoses)
                    ElseIf gloSettings.TypeOfBilling.Electronic.GetHashCode() = dtClaim.Rows(i)(2) Then
                        C1ExpandedClaimSettings.SetData((i + 1), COL_CLAIMPERCHARGES, sCharges)
                        C1ExpandedClaimSettings.SetData((i + 1), COL_DIAGNOSISPERCHARGES, sDiagnoses)
                    ElseIf gloSettings.TypeOfBilling.UB04Paper.GetHashCode() = dtClaim.Rows(i)(2) Then
                        C1ExpandedClaimSettings.SetData((i + 1), COL_CLAIMPERCHARGES, sCharges)
                        C1ExpandedClaimSettings.SetData((i + 1), COL_DIAGNOSISPERCHARGES, sDiagnoses)
                    ElseIf gloSettings.TypeOfBilling.UB04Electronic.GetHashCode() = dtClaim.Rows(i)(2) Then
                        C1ExpandedClaimSettings.SetData((i + 1), COL_CLAIMPERCHARGES, sCharges)
                        C1ExpandedClaimSettings.SetData((i + 1), COL_DIAGNOSISPERCHARGES, sDiagnoses)
                    End If

                Next
            End If

            oDB.Disconnect()
            oDB.Dispose()

            Return ""

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return ""
        Finally

        End Try
    End Function


    Private Function GetExpandedClaimSettingsValue(ByVal nSettingsLevel As Int16, ByVal dtClaim As DataTable) As String


        Try

            If (RbUbBilling_yes.Checked) Then
                C1ExpandedClaimSettings.Rows(1).Visible = True
                C1ExpandedClaimSettings.Rows(2).Visible = True
                C1ExpandedClaimSettings.Rows(3).Visible = True
                C1ExpandedClaimSettings.Rows(4).Visible = True
            Else
                C1ExpandedClaimSettings.Rows(1).Visible = True
                C1ExpandedClaimSettings.Rows(2).Visible = True
                C1ExpandedClaimSettings.Rows(3).Visible = False
                C1ExpandedClaimSettings.Rows(4).Visible = False
            End If

            Dim _dr() As DataRow

            _dr = dtClaim.Select("nSettingLevel= " & nSettingsLevel, "nSettingType asc")
            Dim sCharges As String = String.Empty
            Dim sDiagnoses As String = String.Empty

            If Not IsNothing(_dr) And _dr.Length > 0 Then
                Dim i As Integer = 0
                For Each dataRow As DataRow In _dr
                    sCharges = IIf(dataRow("ServiceLines") = "0", 0, dataRow("ServiceLines"))
                    sDiagnoses = IIf(dataRow("Diagnosis") = "0", 0, dataRow("Diagnosis"))

                    If gloSettings.TypeOfBilling.Paper.GetHashCode() = dataRow("nSettingType") Then
                        C1ExpandedClaimSettings.SetData((i + 1), COL_CLAIMPERCHARGES, sCharges)
                        C1ExpandedClaimSettings.SetData((i + 1), COL_DIAGNOSISPERCHARGES, sDiagnoses)
                    ElseIf gloSettings.TypeOfBilling.Electronic.GetHashCode() = dataRow("nSettingType") Then
                        C1ExpandedClaimSettings.SetData((i + 1), COL_CLAIMPERCHARGES, sCharges)
                        C1ExpandedClaimSettings.SetData((i + 1), COL_DIAGNOSISPERCHARGES, sDiagnoses)
                    ElseIf gloSettings.TypeOfBilling.UB04Paper.GetHashCode() = dataRow("nSettingType") Then
                        C1ExpandedClaimSettings.SetData((i + 1), COL_CLAIMPERCHARGES, sCharges)
                        C1ExpandedClaimSettings.SetData((i + 1), COL_DIAGNOSISPERCHARGES, sDiagnoses)
                    ElseIf gloSettings.TypeOfBilling.UB04Electronic.GetHashCode() = dataRow("nSettingType") Then
                        C1ExpandedClaimSettings.SetData((i + 1), COL_CLAIMPERCHARGES, sCharges)
                        C1ExpandedClaimSettings.SetData((i + 1), COL_DIAGNOSISPERCHARGES, sDiagnoses)
                    End If
                    i = i + 1
                Next

            End If

            Return ""

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return ""
        Finally

        End Try
    End Function


    Private Sub DesignGridForExpandedClaimSettings()
        Try
            'Dim numINUP As New NumericUpDown
            'numINUP.Maximum = 30
            'numINUP.Minimum = 6
            'numINUP.Value = 6
            'numINUP.ReadOnly = True
            'numINUP.TextAlign = HorizontalAlignment.Left


            'Dim numINUPdig As New NumericUpDown
            'numINUPdig.Maximum = 8
            'numINUPdig.Minimum = 4
            'numINUPdig.Value = 4
            'numINUPdig.ReadOnly = True
            'numINUPdig.TextAlign = HorizontalAlignment.Left

            C1ExpandedClaimSettings.Rows.Count = 1
            C1ExpandedClaimSettings.Cols.Count = COL_CNT

            C1ExpandedClaimSettings.SetData(0, COL_ENUMID, "ID")
            C1ExpandedClaimSettings.SetData(0, COL_SETTINGTYPE, "Claim Settings Type")
            C1ExpandedClaimSettings.SetData(0, COL_CLAIMPERCHARGES, "Max Charges Per Claim")
            C1ExpandedClaimSettings.SetData(0, COL_DIAGNOSISPERCHARGES, "Max Diagnoses Per Claim")



            C1ExpandedClaimSettings.Cols(COL_ENUMID).Visible = False
            C1ExpandedClaimSettings.Cols(COL_SETTINGTYPE).Visible = True
            C1ExpandedClaimSettings.Cols(COL_CLAIMPERCHARGES).Visible = True
            C1ExpandedClaimSettings.Cols(COL_DIAGNOSISPERCHARGES).Visible = True



            C1ExpandedClaimSettings.AllowEditing = True
            C1ExpandedClaimSettings.AllowSorting = False
            C1ExpandedClaimSettings.SelectionMode = SelectionModeEnum.Row

            C1ExpandedClaimSettings.Cols(COL_ENUMID).AllowEditing = False
            C1ExpandedClaimSettings.Cols(COL_SETTINGTYPE).AllowEditing = False
            C1ExpandedClaimSettings.Cols(COL_CLAIMPERCHARGES).AllowEditing = True
            C1ExpandedClaimSettings.Cols(COL_DIAGNOSISPERCHARGES).AllowEditing = True




            C1ExpandedClaimSettings.Cols(COL_CLAIMPERCHARGES).DataType = GetType(Int32)
            C1ExpandedClaimSettings.Cols(COL_DIAGNOSISPERCHARGES).DataType = GetType(Int32)

            'C1ExpandedClaimSettings.Cols(COL_CLAIMPERCHARGES).Editor = numINUP
            'C1ExpandedClaimSettings.Cols(COL_DIAGNOSISPERCHARGES).Editor = numINUPdig



            C1ExpandedClaimSettings.Cols(COL_CLAIMPERCHARGES).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
            C1ExpandedClaimSettings.Cols(COL_DIAGNOSISPERCHARGES).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter



            Dim _width As Int32 = C1ExpandedClaimSettings.Width - 5
            C1ExpandedClaimSettings.Cols(COL_SETTINGTYPE).Width = 300
            C1ExpandedClaimSettings.Cols(COL_CLAIMPERCHARGES).Width = 150
            C1ExpandedClaimSettings.Cols(COL_DIAGNOSISPERCHARGES).Width = 150



            C1ExpandedClaimSettings.Rows.Add()
            'Dim RowIndex As Int32 = C1ExpandedClaimSettings.Rows.Count - 1
            C1ExpandedClaimSettings.SetData(1, COL_ENUMID, gloSettings.TypeOfBilling.Electronic)
            C1ExpandedClaimSettings.SetData(1, COL_SETTINGTYPE, "EDI professional")
            C1ExpandedClaimSettings.SetData(1, COL_CLAIMPERCHARGES, 50)
            C1ExpandedClaimSettings.SetData(1, COL_DIAGNOSISPERCHARGES, 8)


            C1ExpandedClaimSettings.Rows.Add()
            'Dim RowIndex As Int32 = C1ExpandedClaimSettings.Rows.Count - 1
            C1ExpandedClaimSettings.SetData(2, COL_ENUMID, gloSettings.TypeOfBilling.Paper)
            C1ExpandedClaimSettings.SetData(2, COL_SETTINGTYPE, "Paper professional")
            C1ExpandedClaimSettings.SetData(2, COL_CLAIMPERCHARGES, 6)
            C1ExpandedClaimSettings.SetData(2, COL_DIAGNOSISPERCHARGES, 4)


            C1ExpandedClaimSettings.Rows.Add()
            'Dim RowIndex As Int32 = C1ExpandedClaimSettings.Rows.Count - 1
            C1ExpandedClaimSettings.SetData(3, COL_ENUMID, gloSettings.TypeOfBilling.UB04Electronic)
            C1ExpandedClaimSettings.SetData(3, COL_SETTINGTYPE, "EDI institutional")
            C1ExpandedClaimSettings.SetData(3, COL_CLAIMPERCHARGES, 999)
            C1ExpandedClaimSettings.SetData(3, COL_DIAGNOSISPERCHARGES, 13)


            C1ExpandedClaimSettings.Rows.Add()
            'Dim RowIndex As Int32 = C1ExpandedClaimSettings.Rows.Count - 1
            C1ExpandedClaimSettings.SetData(4, COL_ENUMID, gloSettings.TypeOfBilling.UB04Paper)
            C1ExpandedClaimSettings.SetData(4, COL_SETTINGTYPE, "Paper institutional")
            C1ExpandedClaimSettings.SetData(4, COL_CLAIMPERCHARGES, 22)
            C1ExpandedClaimSettings.SetData(4, COL_DIAGNOSISPERCHARGES, 18)


            Dim csNumber As C1.Win.C1FlexGrid.CellStyle = C1ExpandedClaimSettings.Styles.Add("cs_Number")
            csNumber.DataType = GetType(Int16)
            csNumber.Format = "0"

            C1ExpandedClaimSettings.Cols(COL_CLAIMPERCHARGES).Style = csNumber
            C1ExpandedClaimSettings.Cols(COL_DIAGNOSISPERCHARGES).Style = csNumber


            'Fill Settings To Grid
            'Dim dtClaimSettings As DataTable = GetExpandedClaimSetting()
            'If dtClaimSettings IsNot Nothing AndAlso dtClaimSettings.Rows.Count > 0 Then
            '    For i As Integer = 0 To dtClaimSettings.Rows.Count - 1
            '        C1ExpandedClaimSettings.Rows.Add()
            '        Dim RowIndex As Int32 = C1ExpandedClaimSettings.Rows.Count - 1
            '        'C1ExpandedClaimSettings.SetData(RowIndex, COL_CLAIMPERCHARGES, Convert.ToString(dtClaimSettings.Rows(i)("nProviderID")))
            '        'C1ExpandedClaimSettings.SetData(RowIndex, COL_DIAGNOSISPERCHARGES, Convert.ToString(dtClaimSettings.Rows(i)("ProviderName")))
            '    Next
            '    '----------------
            'End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function SaveExpandedClaimSettings() As String

        Dim _strSQL As String = ""
        Dim oDB As New gloDatabaseLayer.DBLayer(gloPMAdmin.mdlGeneral.GetConnectionString())
        Try
            oDB.Connect(False)

            _strSQL = "delete from BL_ExpandedClaimSettings where nSettingLevel= " & gloSettings.ExpandedClaimSettingLevel.Clinic.GetHashCode()
            oDB.Execute_Query(_strSQL)

            oDB.Disconnect()
            oDB.Dispose()



            Dim oglosetting As gloSettings.GeneralSettings = New GeneralSettings(gloPMAdmin.mdlGeneral.GetConnectionString())
            Dim nClaim As Int32
            Dim nDiagnosis As Int32
            ''Save Claim Structure Settings

            For i As Integer = 1 To C1ExpandedClaimSettings.Rows.Count - 1
                If C1ExpandedClaimSettings.Rows(i).Visible = True Then
                    If (C1ExpandedClaimSettings.GetData(i, COL_CLAIMPERCHARGES) = Nothing) Then
                        nClaim = 0
                    Else
                        nClaim = C1ExpandedClaimSettings.GetData(i, COL_CLAIMPERCHARGES)
                    End If
                    If (C1ExpandedClaimSettings.GetData(i, COL_DIAGNOSISPERCHARGES) = Nothing) Then
                        nDiagnosis = 0
                    Else
                        nDiagnosis = C1ExpandedClaimSettings.GetData(i, COL_DIAGNOSISPERCHARGES)
                    End If
                    oglosetting.AddExpandedClaimSettings(0, 0, 0, gloSettings.ExpandedClaimSettingLevel.Clinic.GetHashCode(), C1ExpandedClaimSettings.GetData(i, COL_ENUMID), nClaim, nDiagnosis, _ClinicID, SettingFlag.User)
                End If
            Next

            ''-------


            Return ""

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return ""
        Finally

        End Try
    End Function

    Private Sub C1ExpandedClaimSettings_CellChanged(ByVal sender As System.Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles C1ExpandedClaimSettings.CellChanged
        'Try
        '    'And C1ExpandedClaimSettings.GetData(e.Row, COL_CLAIMPERCHARGES) <> 0
        '    Dim _regEx As String = "0123456789"
        '    Dim _currParty As Int16 = 0
        '    If e.Col = COL_CLAIMPERCHARGES And e.Row > 0 Then

        '        If (Int16.TryParse(C1ExpandedClaimSettings.GetData(e.Row, COL_CLAIMPERCHARGES).ToString(), _currParty) = True) Then

        '            If C1ExpandedClaimSettings.GetData(e.Row, COL_CLAIMPERCHARGES) < 6 Or C1ExpandedClaimSettings.GetData(e.Row, COL_CLAIMPERCHARGES) > 30 Then
        '                'C1ExpandedClaimSettings.SetData(e.Row, COL_CLAIMPERCHARGES, 6)
        '                MessageBox.Show("Max charges per claim should not be less than 6 and greater than 30.  ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)

        '            ElseIf C1ExpandedClaimSettings.GetData(e.Row, COL_CLAIMPERCHARGES).ToString() = "" Then
        '                C1ExpandedClaimSettings.SetData(e.Row, COL_CLAIMPERCHARGES, "")
        '                'MessageBox.Show("Max charges per claim should not be blank or zero.  ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        '            End If

        '        Else
        '            C1ExpandedClaimSettings.SetData(e.Row, COL_CLAIMPERCHARGES, "")
        '        End If

        '    End If

        '    If e.Col = COL_DIAGNOSISPERCHARGES And e.Row > 0 Then
        '        If (Int16.TryParse(C1ExpandedClaimSettings.GetData(e.Row, COL_DIAGNOSISPERCHARGES).ToString(), _currParty) = True) Then

        '            If C1ExpandedClaimSettings.GetData(e.Row, COL_DIAGNOSISPERCHARGES) < 4 Or C1ExpandedClaimSettings.GetData(e.Row, COL_DIAGNOSISPERCHARGES) > 8 Then
        '                'C1ExpandedClaimSettings.SetData(e.Row, COL_DIAGNOSISPERCHARGES, 4)
        '                MessageBox.Show("Max diagnosis per claim should not be less than 4 and greater than 8.  ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)

        '            ElseIf C1ExpandedClaimSettings.GetData(e.Row, COL_DIAGNOSISPERCHARGES).ToString() = "" Then
        '                C1ExpandedClaimSettings.SetData(e.Row, COL_DIAGNOSISPERCHARGES, "")
        '                'MessageBox.Show("Max diagnosis per claim should not be blank or zero.  ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        '            End If
        '        Else
        '            C1ExpandedClaimSettings.SetData(e.Row, COL_DIAGNOSISPERCHARGES, "")

        '        End If
        '    End If


        'Catch ex As Exception
        '    MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        'End Try

        If e.Col = COL_CLAIMPERCHARGES And e.Row > 0 Then
            If C1ExpandedClaimSettings.GetData(e.Row, COL_CLAIMPERCHARGES) = 0 Then
                MessageBox.Show("Max charges per claim should not be zero. ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                C1ExpandedClaimSettings.SetData(e.Row, COL_CLAIMPERCHARGES, 1)
            End If
        End If

        If e.Col = COL_DIAGNOSISPERCHARGES And e.Row > 0 Then
            If C1ExpandedClaimSettings.GetData(e.Row, COL_DIAGNOSISPERCHARGES) = 0 Then
                MessageBox.Show("Max diagnoses per claim should not be zero. ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                C1ExpandedClaimSettings.SetData(e.Row, COL_DIAGNOSISPERCHARGES, 1)
            End If
        End If
    End Sub

    Private Sub C1ExpandedClaimSettings_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles C1ExpandedClaimSettings.KeyDown
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

    Private Sub C1ExpandedClaimSettings_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles C1ExpandedClaimSettings.KeyPress
        If nonNumberEntered = True Then
            e.Handled = True
        End If

    End Sub

    Private Function ValidateExpClaimData() As Boolean

        C1ExpandedClaimSettings.FinishEditing()
        Dim bClaimVersion4010_5010 As Boolean = False
        Dim sMessage As String = String.Empty

        If cmbANSIClaimSettings.SelectedValue = 2 Then
            bClaimVersion4010_5010 = True
        End If

        'Professional EDI
        If C1ExpandedClaimSettings.Rows(1).Visible = True Then

            If bClaimVersion4010_5010 = False Then
                If (C1ExpandedClaimSettings.GetData(1, COL_DIAGNOSISPERCHARGES) > 8) Then

                    sMessage = "Electronic Claims (837P 4010) may only display up to 8 diagnoses.  "

                    If DialogResult.Cancel = (MessageBox.Show(sMessage, gstrMessageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2)) Then
                        Return False
                    End If
                End If
            Else
                If (C1ExpandedClaimSettings.GetData(1, COL_DIAGNOSISPERCHARGES) > 12) Then

                    sMessage = "Electronic Claims (837P 5010) may only display up to 12 diagnoses.  "

                    If DialogResult.Cancel = (MessageBox.Show(sMessage, gstrMessageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2)) Then
                        Return False
                    End If
                End If
            End If


            'If (C1ExpandedClaimSettings.GetData(1, COL_DIAGNOSISPERCHARGES) > 8) Then
            '    If bClaimVersion4010_5010 = False Then
            '        sMessage = "Electronic Claims (837P 4010) may only display up to 8 diagnoses.  "
            '        'Else
            '        '    sMessage = "Electronic Claims (837P 5010) may only display up to 8 diagnoses.  "
            '    End If

            '    If DialogResult.Cancel = (MessageBox.Show(sMessage, gstrMessageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2)) Then
            '        Return False
            '    End If
            'End If
            'If (C1ExpandedClaimSettings.GetData(1, COL_DIAGNOSISPERCHARGES) > 12) Then
            '    If bClaimVersion4010_5010 = True Then
            '        sMessage = "Electronic Claims (837P 5010) may only display up to 8 diagnoses.  "
            '    End If
            '    If DialogResult.Cancel = (MessageBox.Show(sMessage, gstrMessageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2)) Then
            '        Return False
            '    End If
            'End If


        End If
        'Professional Paper
        If C1ExpandedClaimSettings.Rows(2).Visible = True Then
            If (C1ExpandedClaimSettings.GetData(2, COL_CLAIMPERCHARGES) > 6) Then
                If DialogResult.Cancel = (MessageBox.Show("CMS1500 may only display up to 6 service lines. ", gstrMessageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2)) Then
                    Return False
                End If
            End If
            'Message removed for diagnoses limits on 10-06-2014  
            'If (C1ExpandedClaimSettings.GetData(2, COL_DIAGNOSISPERCHARGES) > 4) Then
            '    If DialogResult.Cancel = (MessageBox.Show("CMS1500 may only display up to 4 diagnoses. ", gstrMessageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2)) Then
            '        Return False
            '    End If
            'End If
        End If

        'Institutional Edi
        If C1ExpandedClaimSettings.Rows(3).Visible = True Then
            If (C1ExpandedClaimSettings.GetData(3, COL_CLAIMPERCHARGES) > 999) Then
                If DialogResult.Cancel = (MessageBox.Show("System limits Institutional Electronic Claims (837I 4010) to 999 service lines. ", gstrMessageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2)) Then
                    Return False
                End If
            End If

            If (C1ExpandedClaimSettings.GetData(3, COL_DIAGNOSISPERCHARGES) > 18) Then
                If DialogResult.Cancel = (MessageBox.Show("System limits Institutional Electronic Claims (837I 4010) to 18 diagnoses. ", gstrMessageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2)) Then
                    Return False
                End If
            End If
        End If

        'Institutional paper
        If C1ExpandedClaimSettings.Rows(4).Visible = True Then
            If (C1ExpandedClaimSettings.GetData(4, COL_CLAIMPERCHARGES) > 22) Then
                If DialogResult.Cancel = (MessageBox.Show("System limits UB04 to 22 service lines. ", gstrMessageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2)) Then
                    Return False
                End If
            End If

            If (C1ExpandedClaimSettings.GetData(4, COL_DIAGNOSISPERCHARGES) > 18) Then
                If DialogResult.Cancel = (MessageBox.Show("System limits UB04 to 18 diagnoses. ", gstrMessageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2)) Then
                    Return False
                End If
            End If
        End If


        Return True


    End Function

    Private Function ValidateTypeOfBill() As Boolean

        If TxtTypeBill.Text.Length = 4 Then
            If Not TxtTypeBill.Text.StartsWith("0") Then
                MessageBox.Show("Invalid Type of Bill." & vbCrLf & "Length must be 3 or 4 digits." & vbCrLf & "Type of Bill must begin with zero if 4 digits are entered.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
                TxtTypeBill.Focus()
                Return False
            End If
        End If

        If RbUbBilling_yes.Checked = True And TxtTypeBill.Text.Trim().Length > 0 Then
            If TxtTypeBill.Text.Trim().Length > 4 Or TxtTypeBill.Text.Trim().Length < 3 Then
                MessageBox.Show("Invalid Type of Bill." & vbCrLf & "Length must be 3 or 4 digits." & vbCrLf & "Type of Bill must begin with zero if 4 digits are entered.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
                TxtTypeBill.Focus()
                Return False
            End If
        End If


        Return True


    End Function

    Private Sub C1ExpandedClaimSettings_StartEdit(ByVal sender As System.Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles C1ExpandedClaimSettings.StartEdit
        C1ExpandedClaimSettings.Editor = CType(C1ExpandedClaimSettings.Editor, TextBox)
    End Sub

    Private Sub C1ExpandedClaimSettings_SetupEditor(ByVal sender As System.Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles C1ExpandedClaimSettings.SetupEditor
        CType(C1ExpandedClaimSettings.Editor, TextBox).MaxLength = 4
    End Sub

    Private Sub C1ExpandedClaimSettings_KeyPressEdit(ByVal sender As System.Object, ByVal e As C1.Win.C1FlexGrid.KeyPressEditEventArgs) Handles C1ExpandedClaimSettings.KeyPressEdit
        If nonNumberEntered = True Then
            e.Handled = True
        End If

    End Sub

    Private Sub C1ExpandedClaimSettings_KeyDownEdit(ByVal sender As System.Object, ByVal e As C1.Win.C1FlexGrid.KeyEditEventArgs) Handles C1ExpandedClaimSettings.KeyDownEdit
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

    Private Sub SaveInsuranceCorrection_Refund()
        Dim _value As Boolean = False
        Try
            Dim ogloSettings As New clsSettings
            If chkPaymentBeforeDailyClose.Checked = True Then
                _value = ogloSettings.Add("Complete Payments before Daily Close", "True", _ClinicID, 0, SettingFlag.Clinic)
            Else
                _value = ogloSettings.Add("Complete Payments before Daily Close", "False", _ClinicID, 0, SettingFlag.Clinic)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub

    Private Sub SaveInsuranceCorrection_RefundNew()
        Dim _value As Boolean = False
        Try
            'Dim ogloSettings As New clsSettings
            If chkPaymentBeforeDailyClose.Checked = True Then
                _value = ogloSettings.AddValueInTVP("Complete Payments before Daily Close", "True", _ClinicID, 0, SettingFlag.Clinic)
            Else
                _value = ogloSettings.AddValueInTVP("Complete Payments before Daily Close", "False", _ClinicID, 0, SettingFlag.Clinic)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub


    Private Sub chkIsMultipleClearingHouse_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkIsMultipleClearingHouse.CheckedChanged

    End Sub

    Private Sub txtStatementPay_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtEligibilityID.KeyPress

    End Sub

    Private Sub txtStatementMinPay_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtStatementMinPay.KeyPress
        'If Not ((e.KeyChar >= ChrW(48) And e.KeyChar <= ChrW(57)) Or (e.KeyChar = ChrW(8))) And e.KeyChar <> ChrW(46)  Then
        '    e.Handled = True
        'End If

        Dim IndexofDecimal As Integer = (If(txtStatementMinPay.Text.Contains(".") = False, -1, txtStatementMinPay.Text.Trim().Substring(txtStatementMinPay.Text.Trim().IndexOf(".")).Length))
        If ((txtStatementMinPay.Text.Contains(".")) And (e.KeyChar = ChrW(46))) Then
            e.Handled = True
        Else

            e.Handled = Not [Char].IsDigit(e.KeyChar) And e.KeyChar <> ControlChars.Back And e.KeyChar <> ChrW(46)

        End If

    End Sub


    Private Sub txtStatementMinPay_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtStatementMinPay.Leave
        Try
            If txtStatementMinPay.Text <> "" Then
                If txtStatementMinPay.Text.Contains("$") Then
                    txtStatementMinPay.Text = "$" + Format(Convert.ToDecimal(txtStatementMinPay.Text.Replace("$", "").Trim()), "0.00")
                Else
                    txtStatementMinPay.Text = "$" + Format(Convert.ToDecimal(txtStatementMinPay.Text), "0.00")
                End If

            Else
                ' txtStatementMinPay.Text = "$0.00"
            End If
        Catch ex As Exception
            txtStatementMinPay.Text = "$0.00"
        End Try



    End Sub

    Private Sub txtStatementMinPay_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtStatementMinPay.Enter
        If txtStatementMinPay.Text <> "" Then
            If txtStatementMinPay.Text.Contains("$") Then
                txtStatementMinPay.Text = txtStatementMinPay.Text.Replace("$", "")
            End If
        End If
    End Sub

    Private Sub chkFilterOverridedPlan_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkFilterOverridedPlan.CheckedChanged
        Try
            'If chkFilterOverridedPlan.Checked Then
            '    For index As Integer = 1 To C1ANSIVersionSettings.Rows.Count - 1

            '        If Convert.ToString(C1ANSIVersionSettings.GetData(index, ANSIGrid.ClaimBatchSettings)).Trim() = String.Empty AndAlso Convert.ToString(C1ANSIVersionSettings.GetData(index, ANSIGrid.EligiblityRequestSettings)).Trim() = String.Empty Then
            '            C1ANSIVersionSettings.Rows(index).Visible = False
            '        Else
            '            C1ANSIVersionSettings.Rows(index).Visible = True
            '        End If

            '    Next
            'Else
            '    For index As Integer = 1 To C1ANSIVersionSettings.Rows.Count - 1
            '        C1ANSIVersionSettings.Rows(index).Visible = True
            '    Next
            'End If
            GetANSISettings(m_dtANSISettings, IIf(chkFilterOverridedPlan.Checked, True, False))
        Catch ex As Exception
            MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub

    Private Sub GetFormData()

        Dim oDBLayer As New gloDatabaseLayer.DBLayer(gstrConnectionString)
        Dim oParamerters As gloDatabaseLayer.DBParameters = Nothing
        Dim _dsFormData As DataSet = Nothing

        Try

            DesignGridForExpandedClaimSettings()
            DesignGridForBillingSetting()
            ''DesignGridForAppointmentDefault()
            Call DesignGridForMidLevelBillingSetting()
            RemoveHandler Me.cmbProvider.SelectedIndexChanged, AddressOf Me.cmbProvider_SelectedIndexChanged
            DesignGridForProviderSetting()
            DesignBillingIDQAProviderSetting()
            'InsurancePayerSetup'DesignInsurancePayerStandardSetup()
            DesignInsurancePayerStandardSetup()
            oParamerters = New gloDatabaseLayer.DBParameters()
            oParamerters.Add("@sLoginName", gstrLoginName, ParameterDirection.Input, SqlDbType.VarChar)
            oParamerters.Add("@nClinicId", _ClinicID, ParameterDirection.Input, SqlDbType.BigInt)
            oDBLayer.Connect(False)
            oDBLayer.Retrive("Get_AdminSettingsData", oParamerters, _dsFormData)
            oDBLayer.Disconnect()

            If _dsFormData IsNot Nothing AndAlso _dsFormData.Tables.Count > 0 Then


                If (_dsFormData.Tables(0).Rows.Count > 0) Then
                    blnadminflag = True
                Else
                    blnadminflag = False
                End If


                FillInsuranceType(_dsFormData.Tables(1))
                GetPaymentSetting(_dsFormData.Tables(2))
                FillProviders(_dsFormData.Tables(3))
                FillPOS(_dsFormData.Tables(4))
                FillTOS(_dsFormData.Tables(5))
                FillFacilities(_dsFormData.Tables(6))
                FillFeeSchedule(_dsFormData.Tables(7).Copy())
                FillBillingSettings(_dsFormData.Tables(8))
                FillFeeSchedules(_dsFormData.Tables(7).Copy())
                _dtFeeschedules = _dsFormData.Tables(7).Copy()
                FillOtherBillingSettings(_dsFormData.Tables(9))
                FillSelfPayAllowedAmounts(_dsFormData.Tables(7).Copy())
                FillSelfPayDefaultFeeSchedule(_dsFormData.Tables(7).Copy())
                FillMidLevelSettings(_dsFormData.Tables(11))
                GetMidLevelBillingSettings(_dsFormData.Tables(12))
                GetBillingSettings(_dsFormData.Tables(13))
                _dtRevenueCode = _dsFormData.Tables(14)
                FillPaperBilling(_dsFormData.Tables(15), _dsFormData.Tables(16), _dsFormData.Tables(17))
                _dtProviders = _dsFormData.Tables(3)
                FillIDQualifiersAssociation(cmbPaperRendering)
                FillIDQualifiersAssociation(cmbElectronicRendering)
                FillServiceFacility()
                FillServiceFacilityForSubmitter(cmbSubmitter)
                FillBilingIDQualifier(_dsFormData.Tables(18))
                GetExpandedClaimSettingsValue(gloSettings.ExpandedClaimSettingLevel.Clinic.GetHashCode(), _dsFormData.Tables(19))
                FillProviderSettings(_dsFormData.Tables(20), _dsFormData.Tables(21), _dsFormData.Tables(22))
                FillPatientAccSettings(_dsFormData.Tables(23))
                FillStatementVersion(_dsFormData.Tables(24))
                ' By Pranit on 26 sep 2011 to check appointment related checkbox
                GetGlobalAppointmentSettings(_dsFormData.Tables(26), _dsFormData.Tables(25))

                FillChargeSource(_dsFormData.Tables(27))

                SetSupervisorOptionSetting(_dsFormData.Tables(28))
                SetFollowUpSettings(_dsFormData.Tables(29))
                SetProviderisMandatory(_dsFormData.Tables(30))
                SetDefaultProvider(_dsFormData.Tables(31))
                SetEnableProviderTracking(_dsFormData.Tables(32))
                FillPatAccFollowUpActions(_dsFormData.Tables(33))
                FillInsClaimFollowUpActions(_dsFormData.Tables(34))
                '' FillBadDebtAccFollowUpActions(_dsFormData.Tables(56))
                FillCollectionSettings(_dsFormData.Tables(35))
                FillChargeEntryDefaultsSettings(_dsFormData.Tables(36))
                FillEPSDTFamilyPlanningDefaultsSettings(_dsFormData.Tables(37))
                FillAnesthesiaBillingDefaultsSettings(_dsFormData.Tables(38))
                FillEnableWorkersCompBillingDefaultsSettings(_dsFormData.Tables(39))
                FillCapitalizeEDIClaimFileDefaultsSettings(_dsFormData.Tables(40))
                FillNoofProviderCompany(_dsFormData.Tables(41))
                FillDefaultProviderType(_dsFormData.Tables(42))
                FillDefaultDateQualifier(_dsFormData.Tables(43))
                FillIncludeSatisfiedChargesOnStatement(_dsFormData.Tables(44))
                FillSkipZeroBillingClaimForERA(_dsFormData.Tables(45))
                SetgloCollect(_dsFormData.Tables(46))
                FillSkipZeroBillingClaimForIPP(_dsFormData.Tables(47))
                FillShowChargeUnitIP(_dsFormData.Tables(48))
                FillAutoDistributePatientCopay(_dsFormData.Tables(49))
                SetEnableWorkersCompFormsSetting(_dsFormData.Tables(50))
                FillExternalCollectionSettings(_dsFormData.Tables(51), _dsFormData.Tables(52), _dsFormData.Tables(53))
                GetDisplayEMRAlertsSetting(_dsFormData.Tables(54))
                FillSendPriorPatientPaymenAMTSetting(_dsFormData.Tables(55))
                SetEnableSingleSignON(_dsFormData.Tables(57))
                FillInsurancePayerStandardSetup()
                FillInsurancePaymentResoneCodeSetup(_dsFormData.Tables(58))

                FillMergeScheduledAction(_dsFormData.Tables(59))
                FillChargeEntryDefaultForAppointment(_dsFormData.Tables(60))
                FillAppointmentTypeForDefaultAppointment(_dsFormData.Tables(61))
                FillAutoInsuranceEligibilityDefaultSettings(_dsFormData.Tables(62))
                FillAutoAppointmentEligibilityDefaultSettings(_dsFormData.Tables(63))
                FillCapitalizeInsuranceIDDefaultSettings(_dsFormData.Tables(64))
                FillCleargageConfigurationSettings(_dsFormData.Tables(65))
                FillCleargageDefaultDiscountAdjCode(_dsFormData.Tables(66))
            End If



        Catch dbEx As gloDatabaseLayer.DBException
            gloAuditTrail.gloAuditTrail.ExceptionLog(dbEx.ToString(), False)
        Finally
            oParamerters.Dispose()
            oDBLayer.Disconnect()
            oDBLayer.Dispose()
            _dsFormData.Dispose()
            _dsFormData = Nothing
        End Try
    End Sub

    Private Sub GetGlobalAppointmentSettings(ByVal dtRestricted As DataTable, ByVal dtOverlap As DataTable)
        Try

            If dtRestricted IsNot Nothing AndAlso dtRestricted.Rows.Count > 0 Then
                If Convert.ToString(dtRestricted.Rows(0)("sSettingsValue")) = "1" Then
                    chbox_restrictedTmpAptmnt.Checked = True
                Else
                    chbox_restrictedTmpAptmnt.Checked = False
                End If
            Else
                chbox_restrictedTmpAptmnt.Checked = False
            End If

            If dtOverlap IsNot Nothing AndAlso dtOverlap.Rows.Count > 0 Then
                If Convert.ToString(dtOverlap.Rows(0)("sSettingsValue")) = "Y" Then
                    rdoOverlapYes.Checked = True
                ElseIf Convert.ToString(dtOverlap.Rows(0)("sSettingsValue")) = "N" Then
                    rdoOverlapNo.Checked = True
                Else
                    rdoOverlapUser.Checked = True
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub tb_Settings_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tb_Settings.Click
        'Me.Text = tb_Settings.TabPages(tb_Settings.SelectedIndex).Text
    End Sub

    Private Sub FillStatementVersion(ByVal dtStatementVersion As DataTable)

        Try
            RemoveHandler rdoV1.CheckedChanged, AddressOf rdoV1_CheckedChanged
            RemoveHandler rdoV2.CheckedChanged, AddressOf rdoV2_CheckedChanged

            If dtStatementVersion IsNot Nothing AndAlso dtStatementVersion.Rows.Count > 0 Then
                If Convert.ToString(dtStatementVersion.Rows(0)("sSettingsValue")) = "1" Then
                    rdoV1.Checked = True
                ElseIf Convert.ToString(dtStatementVersion.Rows(0)("sSettingsValue")) = "2" Then
                    rdoV2.Checked = True
                Else
                    rdoV1.Checked = True
                End If

            Else
                rdoV1.Checked = True

            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            AddHandler rdoV1.CheckedChanged, AddressOf rdoV1_CheckedChanged
            AddHandler rdoV2.CheckedChanged, AddressOf rdoV2_CheckedChanged
        End Try
    End Sub

    Private Sub SetFollowUpSettings(ByVal dtFollowUp As DataTable)

        Try

            If dtFollowUp IsNot Nothing AndAlso dtFollowUp.Rows.Count > 0 Then
                Dim oValue As Boolean
                Boolean.TryParse(Convert.ToString(dtFollowUp.Rows(0)("sSettingsValue")), oValue)

                If oValue Then
                    rdFollowUpFeatureYes.Checked = True
                Else
                    rdFollowUpFeatureNo.Checked = True
                End If
            Else
                rdFollowUpFeatureYes.Checked = False
                rdFollowUpFeatureNo.Checked = True
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub SetProviderisMandatory(ByVal dtProviderisMandatory As DataTable)

        Try

            If dtProviderisMandatory IsNot Nothing AndAlso dtProviderisMandatory.Rows.Count > 0 Then
                Dim oValue As Boolean
                Boolean.TryParse(Convert.ToString(dtProviderisMandatory.Rows(0)("sSettingsValue")), oValue)

                If oValue Then
                    rdProviderMandatoryYes.Checked = True
                Else
                    rdProviderMandatoryNo.Checked = True
                End If
            Else
                rdProviderMandatoryYes.Checked = True

            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub SetDefaultProvider(ByVal dtDefaultProvider As DataTable)

        Try

            If dtDefaultProvider IsNot Nothing AndAlso dtDefaultProvider.Rows.Count > 0 Then
                Dim oValue As Boolean
                Boolean.TryParse(Convert.ToString(dtDefaultProvider.Rows(0)("sSettingsValue")), oValue)

                If oValue Then
                    rdProviderDefaultYes.Checked = True
                Else
                    rdProviderDefaultNo.Checked = True
                End If
            Else
                rdProviderDefaultYes.Checked = True

            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub SetEnableProviderTracking(ByVal dtProviderTracking As DataTable)

        Try

            If dtProviderTracking IsNot Nothing AndAlso dtProviderTracking.Rows.Count > 0 Then
                Dim oValue As Boolean
                Boolean.TryParse(Convert.ToString(dtProviderTracking.Rows(0)("sSettingsValue")), oValue)

                If oValue Then
                    rdProviderTrackingYes.Checked = True
                Else
                    rdProviderTrackingNo.Checked = True
                End If
            Else
                rdProviderTrackingYes.Checked = True

            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub SetSupervisorOptionSetting(ByVal dtSupervisor As DataTable)

        Try
            If dtSupervisor IsNot Nothing AndAlso dtSupervisor.Rows.Count > 0 Then
                If Convert.ToBoolean(dtSupervisor.Rows(0)("sSettingsValue")) Then
                    ''rbSupervisorSettingEnabledYES.Checked = True
                    ''rbSupervisorSettingEnabledNo.Checked = False
                    chkShowRefProvAsSupervisor.Checked = True

                Else
                    'rbSupervisorSettingEnabledNo.Checked = True
                    'rbSupervisorSettingEnabledYES.Checked = False
                    chkShowRefProvAsSupervisor.Checked = False
                End If

            Else
                ''rbSupervisorSettingEnabledNo.Checked = True
                chkShowRefProvAsSupervisor.Checked = False

            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

        End Try
    End Sub

    Private Sub FillChargeSource(ByVal dtChargeSource As DataTable)

        Try

            Dim _DataTable As New DataTable
            _DataTable.Columns.Add("ID")
            _DataTable.Columns.Add("Desc")
            _DataTable.Rows.Add(1, "gloEMR")
            _DataTable.Rows.Add(2, "Inbound Charges")
            _DataTable.Rows.Add(3, "Both")
            _DataTable.AcceptChanges()

            cmbChrgSource.DataSource = _DataTable
            cmbChrgSource.DisplayMember = _DataTable.Columns("Desc").ColumnName
            cmbChrgSource.ValueMember = _DataTable.Columns("ID").ColumnName


            If dtChargeSource IsNot Nothing AndAlso dtChargeSource.Rows.Count > 0 Then
                cmbChrgSource.SelectedValue = Convert.ToInt64(dtChargeSource.Rows(0)("sSettingsValue"))
            Else
                cmbChrgSource.SelectedIndex = 0
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

        End Try
    End Sub


    Private Sub numFutureCloseDateDays_KeyPress(ByVal sender As System.Object, _
    ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles NumericUpDown1.KeyPress
        If (e.KeyChar = "." _
         Or e.KeyChar = "'" _
         Or e.KeyChar = "," _
         Or e.KeyChar = "-") Then
            e.Handled = True
        End If
    End Sub

    'Private _objNudCtrl As NumericUpDown

    'Private _objNudTextBox As TextBox

    'Private _strValue As String

    '' Code added by Rahul Patil for validating future close date numeric up down control
    'Private Sub numFutureCloseDateDays_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles numFutureCloseDateDays.KeyDown

    '    Try
    '        Dim objSettings As New clsSettings
    '        _objNudCtrl = CType(sender, NumericUpDown)

    '        _objNudTextBox = CType(_objNudCtrl.Controls(1), TextBox)

    '        _strValue = _objNudCtrl.Text

    '        If e.KeyValue >= 48 And e.KeyValue < 58 Then

    '            If _objNudTextBox.SelectionLength > 0 Then

    '                'Text before the selection + entered key + text after the selection

    '                _strValue = _strValue.Substring(0, _objNudTextBox.SelectionStart) + ChrW(e.KeyValue).ToString() + _strValue.Substring(_objNudTextBox.SelectionStart + _objNudTextBox.SelectionLength, _strValue.Length - (_objNudTextBox.SelectionStart + _objNudTextBox.SelectionLength))

    '            Else

    '                'insert the value

    '                _strValue = _strValue.Insert(_objNudTextBox.SelectionStart, ChrW(e.KeyValue).ToString())

    '            End If

    '            'validate the value with maximum and minimum values of numFutureCloseDateDays.

    '            If Decimal.Parse(_strValue) > CType(sender, NumericUpDown).Maximum Then

    '                e.SuppressKeyPress = True

    '            ElseIf Decimal.Parse(_strValue) < CType(sender, NumericUpDown).Minimum Then

    '                e.SuppressKeyPress = True

    '            End If

    '        End If

    '        'Check if the end user clicks the delete key or back key

    '        If e.KeyValue = Keys.Delete Or e.KeyValue = Keys.Back Then

    '            Dim index As Integer = _objNudTextBox.Text.IndexOf(".")

    '            'check if the end user would like to delete the decimal "." and supress the key

    '            If (_objNudTextBox.SelectionStart = index And Not e.KeyValue = Keys.Back) Then

    '                e.SuppressKeyPress = True
    '                If objSettings.GetSettings() = True Then
    '                    numFutureCloseDateDays.Value = objSettings.FutureCloseDateDays
    '                    numFutureCloseDateDays.Minimum = objSettings.FutureCloseDateDays
    '                End If
    '            ElseIf (_objNudTextBox.SelectionStart = index + 1 And e.KeyValue = Keys.Back) Then

    '                e.SuppressKeyPress = True
    '                If objSettings.GetSettings() = True Then
    '                    numFutureCloseDateDays.Value = objSettings.FutureCloseDateDays
    '                    numFutureCloseDateDays.Minimum = objSettings.FutureCloseDateDays
    '                End If
    '            End If

    '            If objSettings.GetSettings() = True Then
    '                numFutureCloseDateDays.Value = objSettings.FutureCloseDateDays
    '                numFutureCloseDateDays.Minimum = objSettings.FutureCloseDateDays
    '            End If

    '        End If

    '    Catch ex As FormatException

    '        numFutureCloseDateDays.Value = numFutureCloseDateDays.Value

    '    Catch ex2 As Exception

    '    End Try

    'End Sub
    'Private Sub numFutureCloseDateDays_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles numFutureCloseDateDays.KeyUp

    '    Try
    '        Dim objSettings As New clsSettings

    '        _objNudCtrl = CType(sender, NumericUpDown)

    '        _objNudTextBox = CType(_objNudCtrl.Controls(1), TextBox)

    '        _strValue = _objNudCtrl.Value.ToString()

    '        'check if the end user selects the entire text in the numFutureCloseDateDays control and clicks on the enter key

    '        'then assign the numFutureCloseDateDays with minimum value.

    '        If e.KeyValue = Keys.Delete Or e.KeyValue = Keys.Back Then

    '            If _objNudTextBox.SelectionLength = _strValue.Length Or _objNudTextBox.Text = "" Or _objNudTextBox.Text = "-" Then

    '                numFutureCloseDateDays.Text = 0

    '                numFutureCloseDateDays.Value = 0
    '                If objSettings.GetSettings() = True Then
    '                    numFutureCloseDateDays.Value = objSettings.FutureCloseDateDays
    '                    numFutureCloseDateDays.Minimum = objSettings.FutureCloseDateDays
    '                End If

    '            End If

    '        End If

    '    Catch ex As Exception

    '    End Try

    'End Sub
    '' Code added by Rahul Patil for validating future close date numeric up down control


    Private Sub frmSettings_New_HandleDestroyed(sender As Object, e As System.EventArgs) Handles Me.HandleDestroyed

    End Sub

    Private Sub txtInsClmStartFilingDays_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs)
        If (Not Char.IsDigit(e.KeyChar) AndAlso (e.KeyChar <> CType(ChrW(8), Char))) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtInsClmRebillFilingDays_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs)
        If (Not Char.IsDigit(e.KeyChar) AndAlso (e.KeyChar <> CType(ChrW(8), Char))) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtPatAccFUBeginsAfterNoOfStmnt_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs)
        If (Not Char.IsDigit(e.KeyChar) AndAlso (e.KeyChar <> CType(ChrW(8), Char))) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtPatAccNoOfDaysAfterStmnt_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs)
        If (Not Char.IsDigit(e.KeyChar) AndAlso (e.KeyChar <> CType(ChrW(8), Char))) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtPmntPlanDefFUActionDays_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs)
        If (Not Char.IsDigit(e.KeyChar) AndAlso (e.KeyChar <> CType(ChrW(8), Char))) Then
            e.Handled = True
        End If
    End Sub


    'Private Sub btnFollowup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFollowup.Click
    '    Dim objSettings As New clsSettings
    '    Dim value As New Object
    '    objSettings.GetSetting("CALCULATEFOLLOWUP", gnLoginID, _ClinicID, value)
    '    If Not IsNothing(value) And value.ToString() <> "" Then
    '        If (Convert.ToBoolean(value.ToString().Trim) = True) Then
    '            MessageBox.Show("Accounts and Claims Follow-up Dates and Actions are allready set.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
    '        Else
    '            Dim res As DialogResult = MessageBox.Show("All Patient Accounts and Claims will be reviewed by the system to calculate the starting Follow-up Dates and Actions." + Environment.NewLine + "This may take awhile." + Environment.NewLine + "Continue?", gstrMessageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Information)
    '            If res = Windows.Forms.DialogResult.OK Then
    '                Dim AutoFollowupUtility As frmAutoFollowupUtility = New frmAutoFollowupUtility()
    '                AutoFollowupUtility.ShowDialog()
    '            End If
    '        End If
    '    Else
    '        Dim res As DialogResult = MessageBox.Show("All Patient Accounts and Claims will be reviewed by the system to calculate the starting Follow-up Dates and Actions." + Environment.NewLine + "This may take awhile." + Environment.NewLine + "Continue?", gstrMessageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Information)
    '        If res = Windows.Forms.DialogResult.OK Then
    '            Dim AutoFollowupUtility As frmAutoFollowupUtility = New frmAutoFollowupUtility()
    '            AutoFollowupUtility.ShowDialog()
    '        End If
    '    End If
    '    value = Nothing

    'End Sub

    Private Sub rdoDefaultBatch_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoDefaultBatch.CheckedChanged
        pnlChrgDefaultsSettings.Visible = True
    End Sub

    Private Sub rdoDefaultNone_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoDefaultNone.CheckedChanged
        pnlChrgDefaultsSettings.Visible = False
    End Sub

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
    Private Sub chkEnableWorkersCompBilling_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkEnableWorkersCompBilling.CheckedChanged
        If chkEnableWorkersCompBilling.Checked = False Then
            If IsWorkCompOrCompayOn() Then
                MessageBox.Show("Warning:" + Environment.NewLine + "Patient Insurances associated to Insurance Plan has Worker Comp information filled out.Turning the setting " + "OFF" + " will not show " + Environment.NewLine + "the Worker Comp details and will keep reporting the worker comp information on claims.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End If
    End Sub

    Private Function IsWorkCompOrCompayOn() As Boolean
        Dim oDb As New gloDatabaseLayer.DBLayer(gstrConnectionString)
        Dim _Query As String = ""
        Dim _dt As New DataTable()
        Dim Result As Object = 0
        Try
            oDb.Connect(False)
            _Query = "SELECT COUNT(nPatientID) FROM dbo.PatientInsurance_DTL WHERE bIsCompnay=1 OR bworkerscomp=1 "
            Result = oDb.ExecuteScalar_Query(_Query)

            If Convert.ToInt16(Result) > 0 Then
                Return True
            Else
                _Query = "SELECT COUNT(nContactID) FROM   dbo.Contacts_Insurance_DTL WHERE bIsWorkerComp=1 "
                Result = oDb.ExecuteScalar_Query(_Query)

                If Convert.ToInt16(Result) > 0 Then
                    Return True
                Else
                    Return False
                End If
                Return False
            End If

        Catch Ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), True)
            Ex = Nothing
        End Try
        Return False
    End Function


    Private Sub rb_BC_PatAcc_Yes_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rb_BC_PatAcc_Yes.CheckedChanged


        If (rb_BC_PatAcc_No.Checked = False) Then
            pnl_BC_PatAccSettings.Enabled = True
            If (rb_BC_PatAcc_Statment_Yes.Checked = False And rb_BC_PatAcc_Statment_No.Checked = False) = True Then
                rb_BC_PatAcc_Statment_Yes.Checked = True
            End If
            If ((rbPatientAccountFeatureEnabledNO.Checked = True) Or (rbPatientAccountFeatureEnabledNO.Checked = False And rbPatientAccountFeatureEnabledYES.Checked = False)) Then
                MessageBox.Show("Patient Account Feature is OFF.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        Else
            pnl_BC_PatAccSettings.Enabled = False
        End If
        If (rb_BusinessCenter.Checked = True) Then
            pnl_BC_PatAcc.Enabled = True
        Else
            pnl_BC_PatAcc.Enabled = False
        End If

    End Sub

    Private Sub rb_BC_PatAcc_No_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rb_BC_PatAcc_No.CheckedChanged
        If (rb_BC_PatAcc_No.Checked = False) Then
            pnl_BC_PatAccSettings.Enabled = True
        Else
            pnl_BC_PatAccSettings.Enabled = False
        End If
        If (rb_BusinessCenter.Checked = True) Then
            pnl_BC_PatAcc.Enabled = True
        Else
            pnl_BC_PatAcc.Enabled = False
        End If
    End Sub

    Private Sub Label727_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label727.Click

    End Sub



    Private Sub rb_BusinessCenter_No_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rb_BusinessCenter.CheckedChanged
        If (rb_BusinessCenter.Checked = True) Then
            pnl_BC_PatAcc.Enabled = True
            pnl_BC_PatAccSettings.Enabled = True
            If (rb_BC_PatAcc_No.Checked = False) Then
                pnl_BC_PatAccSettings.Enabled = True
            Else
                pnl_BC_PatAccSettings.Enabled = False
            End If

            If (rb_BC_PatAcc_Yes.Checked = False And rb_BC_PatAcc_No.Checked = False) = True Then
                rb_BC_PatAcc_Yes.Checked = True
            End If

        Else
            pnl_BC_PatAcc.Enabled = False
            pnl_BC_PatAccSettings.Enabled = False
        End If

    End Sub

    Private Function ValidateBusinessCenterSettings() As Boolean
        'If (rb_BusinessCenter.Checked = True And rb_BC_PatAcc_Yes.Checked = False And rb_BC_PatAcc_No.Checked = False) Then
        '    MessageBox.Show("Configure Business Center settings.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
        '    Return True
        'End If

        If (rb_BC_PatAcc_Yes.Checked = False And rb_BC_PatAcc_No.Checked = False) = False Then
            If (rb_BC_PatAcc_Yes.Checked = True) Then


                If (rb_BC_PatAcc_Statment_Yes.Checked = False And rb_BC_PatAcc_Statment_No.Checked = False) Then
                    MessageBox.Show("Configure Business Center settings.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return True
                End If

                If (rb_BC_PatAcc_FollowUp_Yes.Checked = False And rb_BC_PatAcc_FollowUp_No.Checked = False) Then
                    MessageBox.Show("Configure Business Center settings.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return True
                End If

                If (rb_BC_PatAcc_ClaimBatch_Yes.Checked = False And rb_BC_PatAcc_ClaimBatch_No.Checked = False) Then
                    MessageBox.Show("Configure Business Center settings.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return True
                End If

                If (rb_BC_PatAcc_ChargeMismatch_Warn.Checked = False And rb_BC_PatAcc_ChargeMismatch_None.Checked = False) Then
                    MessageBox.Show("Configure Business Center settings.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return True
                End If

                'If (rb_BC_PatAcc_ChargeMismatch_NewAcc_Yes.Checked = False And rb_BC_PatAcc_ChargeMismatch_NewAcc_No.Checked = False) Then
                '    MessageBox.Show("Configure Business Center settings.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                '    Return True
                'End If
            End If
        End If


    End Function

    Private Sub chk_allhl7_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chk_allhl7.CheckedChanged
        chkhl7PatientReg.Checked = chk_allhl7.Checked
        chkHL7Appointment.Checked = chk_allhl7.Checked
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


    Private Sub chkShowPaperPlanOverrides_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkShowPaperPlanOverrides.CheckedChanged
        Try
            SetPaperFormSettings(m_dtPaperFormSettings, IIf(chkShowPaperPlanOverrides.Checked, True, False))
        Catch ex As Exception
            MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub

    Private Sub chkSplitClaimToPatient_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkSplitClaimToPatient.CheckedChanged
        MessageBox.Show("Warning – After turning on or off the Split Claim to Patient, you must close and re-open each running gloPM desktop Charge entry screen for all users.  Leaving a desktop running might cause conflicts.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
    End Sub
    Private Sub SetICD10TransitionTab()
        Dim oClsICD10Settings As ClsICD10Settings = Nothing
        Try
            oClsICD10Settings = New ClsICD10Settings()
            dtpICD10.Format = DateTimePickerFormat.Custom
            dtpICD10.CustomFormat = "MM/dd/yyyy"
            Dim _Date As String = oClsICD10Settings.FetchICDDOS()
            If Not IsNothing(_Date) AndAlso _Date <> "" Then
                dtpICD10.Value = _Date
            Else
                dtpICD10.Value = DateTime.Now.ToShortDateString
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

    Dim oDataContext As DataClassICD10DataContext = New DataClassICD10DataContext(mdlGeneral.gstrConnectionString)
    Private Sub SetgloCollect(ByVal dt As DataTable) 'Code Added for New Tab gloCollect
        Try
            If dt IsNot Nothing Then
                If (dt.Rows.Count > 0) Then
                    If IsDate(Convert.ToString(dt.Rows(0)("sSettingsValue"))) Then
                        dtpGloCollectResDOS.Checked = True
                        dtpGloCollectResDOS.Value = Convert.ToDateTime(dt.Rows(0)("sSettingsValue"))
                    Else
                        dtpGloCollectResDOS.Checked = False
                    End If
                Else
                    dtpGloCollectResDOS.Checked = False
                End If
            Else
                dtpGloCollectResDOS.Checked = False
            End If




            Dim _List As IEnumerable = (From c In oDataContext.User_MSTs
                                         Where (c.nBlockStatus = 0)
                                         Select New With {c.bIsGloCollect, c.nUserID, c.sLoginName, c.sFirstName, c.sMiddleName, c.sLastName, c.nBlockStatus}).AsEnumerable()

            C1gloCollect.DataSource = _List
            designGridForgloCollect()

            Dim count As Integer = (From c In oDataContext.User_MSTs Where (c.bIsGloCollect.Equals(Nothing) Or c.bIsGloCollect = False) And c.nBlockStatus = 0).Count()
            If count = 0 Then
                C1gloCollect.SetCellCheck(0, 0, CheckEnum.Checked)
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub savegloCollect() 'Code Added for New Tab gloCollect
        If dtpGloCollectResDOS.Checked = True AndAlso Not IsNothing(dtpGloCollectResDOS.Value) Then
            ogloSettings.AddValueInTVP("gloCollectResponsibilityDOS", dtpGloCollectResDOS.Value.ToShortDateString, _ClinicID, 0, SettingFlag.Clinic)
        Else
            ogloSettings.AddValueInTVP("gloCollectResponsibilityDOS", "", _ClinicID, 0, SettingFlag.Clinic)
        End If


        Dim _List As List(Of User_MST) = (From Result In oDataContext.User_MSTs Select Result).ToList()
        Dim itemNew As User_MST
        For i As Integer = 1 To C1gloCollect.Rows.Count - 1
            itemNew = (From Result In _List Where Result.nUserID = C1gloCollect.Item(i, gloCollectGrid.nUserID) Select Result).FirstOrDefault()
            itemNew.bIsGloCollect = C1gloCollect.Item(i, gloCollectGrid.bIsGloCollect)
        Next
        oDataContext.SubmitChanges()
    End Sub

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
        dtp.CalendarFont = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        dtp.CalendarForeColor = System.Drawing.Color.Maroon
        dtp.CalendarMonthBackground = System.Drawing.Color.White
        dtp.CalendarTitleBackColor = System.Drawing.Color.Orange
        dtp.CalendarTitleForeColor = System.Drawing.Color.Brown
        dtp.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        dtp.CustomFormat = "MM/dd/yyyy"
        c1Grid.Cols(ICD10Grid.DOS).Editor = dtp
        c1Grid.ExtendLastCol = True
    End Sub
    Private Sub designGridForgloCollect() 'Code Added for New Tab gloCollect
        C1gloCollect.Cols.Count = 7
        C1gloCollect.Cols(gloCollectGrid.bIsGloCollect).DataType = GetType(Boolean)
        C1gloCollect.Cols(gloCollectGrid.bIsGloCollect).Visible = True
        C1gloCollect.Cols(gloCollectGrid.bIsGloCollect).Name = "bIsGloCollect"


        C1gloCollect.Cols(gloCollectGrid.nUserID).Visible = False

        C1gloCollect.Cols(gloCollectGrid.sLoginName).Visible = True
        C1gloCollect.Cols(gloCollectGrid.sFirstName).Visible = True
        C1gloCollect.Cols(gloCollectGrid.sMiddleName).Visible = True
        C1gloCollect.Cols(gloCollectGrid.sLastName).Visible = True
        C1gloCollect.Cols(gloCollectGrid.nBlockStatus).Visible = False

        C1gloCollect.Cols(0).DataType = GetType(Boolean)
        C1gloCollect.SetCellCheck(0, 0, CheckEnum.Unchecked)
        C1gloCollect.SetData(0, 0, "Select All", False)
        C1gloCollect.Cols(0).ImageAlign = ImageAlignEnum.CenterCenter
        C1gloCollect.Cols(0).TextAlign = TextAlignEnum.CenterCenter
        C1gloCollect.SetData(0, gloCollectGrid.sLoginName, "User Name")
        C1gloCollect.SetData(0, gloCollectGrid.sFirstName, "First Name")
        C1gloCollect.SetData(0, gloCollectGrid.sMiddleName, "Middle Name")
        C1gloCollect.SetData(0, gloCollectGrid.sLastName, "Last Name")
        C1gloCollect.SetData(0, gloCollectGrid.nUserID, "USER ID")
        C1gloCollect.SetData(0, gloCollectGrid.nBlockStatus, "Block Status")

        C1gloCollect.Cols(gloCollectGrid.sLoginName).AllowEditing = False
        C1gloCollect.Cols(gloCollectGrid.sFirstName).AllowEditing = False
        C1gloCollect.Cols(gloCollectGrid.sMiddleName).AllowEditing = False
        C1gloCollect.Cols(gloCollectGrid.sLastName).AllowEditing = False

        C1gloCollect.Cols(gloCollectGrid.sFirstName).Width = 170
        C1gloCollect.Cols(gloCollectGrid.sMiddleName).Width = 165
        C1gloCollect.Cols(gloCollectGrid.sLastName).Width = 170
        C1gloCollect.Cols(gloCollectGrid.bIsGloCollect).Width = 80
    End Sub
    Private Sub SaveICD10TransitionSettings()
        Dim oClsICD10Settings As ClsICD10Settings = Nothing
        Dim oBL_ICD10TransitionSetting As BL_ICD10TransitionSetting = Nothing
        'Dim listBL_ICD10TransitionSetting As List(Of BL_ICD10TransitionSetting) = Nothing
        'Dim _List As IEnumerable = Nothing

        Try
            C1ICD10DOS.Select()
            oClsICD10Settings = New ClsICD10Settings()
            '_List = DirectCast(C1ICD10DOS.DataSource, IEnumerable)
            'oClsICD10Settings = New ClsICD10Settings()
            'oClsICD10Settings.SaveData(_List)
            '_List = (From c In DirectCast(C1ICD10DOS.DataSource, IEnumerable) Select c)
            '
            'listBL_ICD10TransitionSetting = New List(Of BL_ICD10TransitionSetting)
            oBL_ICD10TransitionSetting = (From Result In oClsICD10Settings.oDataContext.BL_ICD10TransitionSettings
                                                    Where Result.nContactID = 0
                                                    Select Result).FirstOrDefault()
            If Not IsNothing(dtpICD10) AndAlso Not IsNothing(dtpICD10.Value) Then  '03252014  Removed the Check box from gloPM Admin -Transition Settings
                If oBL_ICD10TransitionSetting Is Nothing Then
                    oBL_ICD10TransitionSetting = New BL_ICD10TransitionSetting()
                    oBL_ICD10TransitionSetting.nID = 0
                    oBL_ICD10TransitionSetting.nContactID = 0
                    oBL_ICD10TransitionSetting.nClinicID = gnClinicID
                    oBL_ICD10TransitionSetting.dtDOSDate = dtpICD10.Value.ToShortDateString
                    oBL_ICD10TransitionSetting.dtCreatedDate = DateTime.Now
                    oBL_ICD10TransitionSetting.dtModifiedDate = DateTime.Now
                    oClsICD10Settings.oDataContext.BL_ICD10TransitionSettings.InsertOnSubmit(oBL_ICD10TransitionSetting)
                Else
                    oBL_ICD10TransitionSetting.nClinicID = gnClinicID
                    oBL_ICD10TransitionSetting.dtDOSDate = dtpICD10.Value.ToShortDateString
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
            '_List = Nothing
            If Not oClsICD10Settings Is Nothing Then
                oClsICD10Settings.Dispose()
                oClsICD10Settings = Nothing
            End If
            oBL_ICD10TransitionSetting = Nothing
        End Try
    End Sub

    Private Sub chkICD10PalnOverride_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkICD10PalnOverride.CheckedChanged
        Dim _List As IEnumerable
        Dim oClsICD10Settings As ClsICD10Settings = Nothing
        Try
            oClsICD10Settings = New ClsICD10Settings()
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

    Private Sub C1ICD10DOS_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles C1ICD10DOS.KeyDown
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

    Private Sub tb_Settings_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles tb_Settings.SelectedIndexChanged
        Try
            Me.Text = tb_Settings.TabPages(tb_Settings.SelectedIndex).Text
        Catch ex As Exception
            UpdateErrorLog(ex.ToString)
        End Try
    End Sub

    'Code Added for New Tab gloCollect
    Private Sub C1gloCollect_AfterEdit(sender As System.Object, e As C1.Win.C1FlexGrid.RowColEventArgs) Handles C1gloCollect.AfterEdit
        If e.Col = gloCollectGrid.bIsGloCollect And e.Row = 0 Then

            If C1gloCollect.GetCellCheck(e.Row, e.Col) = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                SelectALL(True)
            Else
                SelectALL(False)
            End If

        End If
    End Sub
    Private Sub SelectALL(ByVal _Select As Boolean)
        For i As Integer = 1 To C1gloCollect.Rows.Count - 1
            If C1gloCollect.Rows(i).IsVisible = True Then
                C1gloCollect.SetData(i, gloCollectGrid.bIsGloCollect, _Select)
            End If
        Next
    End Sub

    Private Sub txtsearch_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtsearch.TextChanged

        Dim strSearch As String = txtsearch.Text.Trim()
        'Replace ',%,[,* from search string
        strSearch = strSearch.Replace("'", "").Replace("%", "").Replace("*", "").Replace("[", "")
        C1gloCollect.AllowFiltering = True

        If strSearch.Trim() <> String.Empty Then
            Dim filter As ConditionFilter = New ConditionFilter()
            filter.AndConditions = False
            C1gloCollect.Cols(gloCollectGrid.sLoginName).AllowFiltering = AllowFiltering.ByCondition
            filter.Condition1.Operator = ConditionOperator.Contains
            filter.Condition1.Parameter = ""
            filter.Condition1.Parameter = Convert.ToString(strSearch)
            filter.GetEditor()
            C1gloCollect.Cols(gloCollectGrid.sLoginName).Filter = filter
        Else
            C1gloCollect.Cols(gloCollectGrid.sLoginName).Filter = Nothing
        End If
        C1gloCollect.ApplyFilters()
        txtsearch.Focus()

    End Sub


    Private Sub C1gloCollect_MouseDown(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles C1gloCollect.MouseDown
        If C1gloCollect.HitTest(e.X, e.Y).Type = C1.Win.C1FlexGrid.HitTestTypeEnum.FilterIcon Then
            For Each f As Form In Application.OpenForms
                If f.Name = "FilterEditorForm" AndAlso f.[GetType]().ToString() = "C1.Win.C1FlexGrid.FilterEditorForm" Then
                    f.Close()
                    Exit For
                End If
            Next
        End If
    End Sub

    Private Sub btnClear_Click(sender As System.Object, e As System.EventArgs) Handles btnClear.Click
        txtsearch.Clear()
    End Sub

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

    Private Sub rbExternalCollectionNo_CheckedChanged(sender As Object, e As System.EventArgs) Handles rbExternalCollectionNo.CheckedChanged
        If rbExternalCollectionNo.Checked = True Then
            Dim obj As New clsSettings
            If obj.IsanyBadDebtPatient() Then
                MessageBox.Show("Application contains patient with Bad Debt status. Cannot Turn Off External Collection Feature.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                rbExternalCollectionYes.Checked = True
            Else
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.CollectionAgency, gloAuditTrail.ActivityType.Save, "External Collection Feature setting is turn off ", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPMAdmin, True)
            End If
            If Not IsDBNull(obj) Then
                obj = Nothing
            End If
        End If

    End Sub

    Private Sub rbExternalCollectionYes_CheckedChanged(sender As Object, e As System.EventArgs) Handles rbExternalCollectionYes.CheckedChanged
        If rbExternalCollectionYes.Checked Then
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.CollectionAgency, gloAuditTrail.ActivityType.Save, "External Collection Feature setting is turn on ", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPMAdmin, True)
        End If
    End Sub

    Private Sub FillSendPriorPatientPaymenAMTSetting(ByVal dt As DataTable)
        Try
            If dt IsNot Nothing Then
                If (dt.Rows.Count > 0) Then
                    Dim oResult As Boolean = False
                    Boolean.TryParse(Convert.ToString(dt.Rows(0)("sSettingsValue")), oResult)
                    chkSendPriorPatPtmToAMTSegment.Checked = oResult
                Else
                    chkSendPriorPatPtmToAMTSegment.Checked = False
                End If
            Else
                chkSendPriorPatPtmToAMTSegment.Checked = False
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    'InsurancePayerSetup'
#Region "Insurance Payer Setup Section"

    Private Sub rbInsStandardSetup_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbInsStandardSetup.CheckedChanged
        EnableDisablePanel()
        If rbInsStandardSetup.Checked = True Then
            rbInsStandardSetup.Font = New Font("Tahoma", 9, FontStyle.Bold)
            C1StandardCAS.Enabled = True
            C1StandardCAS.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))

            ''pnlC1StandardCAS.Visible = True
            If gloGlobal.gloPMGlobal.ReasonCodeSetup <> 1 Then
                MessageBox.Show("Adjustment code posting setup changes done require a re-start to gloPM application for changes to take effect for all logged in users.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Else
            rbInsStandardSetup.Font = New Font("Tahoma", 9, FontStyle.Regular)
            C1StandardCAS.Enabled = False
            C1StandardCAS.ForeColor = Color.Gray

            ''pnlC1StandardCAS.Visible = False
        End If


    End Sub

    Private Sub rbInsPayerSetup_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbInsPayerSetup.CheckedChanged
        EnableDisablePanel()
        If rbInsPayerSetup.Checked = True Then
            rbInsPayerSetup.Font = New Font("Tahoma", 9, FontStyle.Bold)
            C1StandardCAS.Enabled = False
            C1StandardCAS.ForeColor = Color.Gray
            If gloGlobal.gloPMGlobal.ReasonCodeSetup <> 2 Then
                MessageBox.Show("Adjustment code posting setup changes done require a re-start to gloPM application for changes to take effect for all logged in users.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Else
            rbInsPayerSetup.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub rbInsManualSetup_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbInsManualSetup.CheckedChanged
        EnableDisablePanel()
        If rbInsManualSetup.Checked = True Then
            rbInsManualSetup.Font = New Font("Tahoma", 9, FontStyle.Bold)
            C1StandardCAS.Enabled = False
            C1StandardCAS.ForeColor = Color.Gray
            If gloGlobal.gloPMGlobal.ReasonCodeSetup <> 3 Then
                MessageBox.Show("Adjustment code posting setup changes done require a re-start to gloPM application for changes to take effect for all logged in users.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Else
            rbInsManualSetup.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Function EnableDisablePanel()
        If rbInsStandardSetup.Checked Then
            pnlC1StandardCAS.Enabled = True
        ElseIf rbInsPayerSetup.Checked Then
            pnlC1StandardCAS.Enabled = False
            C1StandardCAS.Select(1, 0, 1, C1StandardCAS.Cols.Count - 1, True)
        ElseIf rbInsManualSetup.Checked Then
            pnlC1StandardCAS.Enabled = False
            C1StandardCAS.Select(1, 0, 1, C1StandardCAS.Cols.Count - 1, True)
        End If

    End Function
    Dim oC1Font As Font = gloGlobal.clsgloFont.gFont

#Region " C1 Constants"
    Private Const COL_STD_REASONCODEID = 0
    Private Const COL_STD_GROUP = 1
    Private Const COL_STD_REASON = 2
    Private Const COL_STD_CASTYPE = 3
    Private Const COL_STD_CASDESC = 4
    Private Const COL_STD_COUNT = 5
#End Region

    Private Sub DesignInsurancePayerStandardSetup()
        Try
            Dim csStdGroupCode As C1.Win.C1FlexGrid.CellStyle
            Try
                If C1StandardCAS.Styles.Contains("csGroupCodes") Then
                    csStdGroupCode = C1StandardCAS.Styles("csGroupCodes")
                Else
                    csStdGroupCode = C1StandardCAS.Styles.Add("csGroupCodes")
                    csStdGroupCode.DataType = GetType(String)
                    csStdGroupCode.Font = oC1Font
                    csStdGroupCode.BackColor = Color.White
                End If

            Catch ex As Exception
                csStdGroupCode = C1StandardCAS.Styles.Add("csGroupCodes")
                csStdGroupCode.DataType = GetType(String)
                csStdGroupCode.Font = oC1Font
                csStdGroupCode.BackColor = Color.White
            End Try

            csStdGroupCode.ComboList = GetGroupCodeString()

            C1StandardCAS.Rows.Count = 1
            C1StandardCAS.Rows.Fixed = 1
            C1StandardCAS.Cols.Count = COL_STD_COUNT

            C1StandardCAS.SetData(0, COL_STD_GROUP, "Group")
            C1StandardCAS.SetData(0, COL_STD_REASON, "Reason")
            C1StandardCAS.SetData(0, COL_STD_CASDESC, "Type")

            C1StandardCAS.Cols(COL_STD_REASONCODEID).Visible = False
            C1StandardCAS.Cols(COL_STD_CASTYPE).Visible = False

            Dim _Width As Integer = C1StandardCAS.Width
            C1StandardCAS.Cols(COL_STD_GROUP).Width = _Width * 0.3
            C1StandardCAS.Cols(COL_STD_REASON).Width = _Width * 0.3
            C1StandardCAS.Cols(COL_STD_CASDESC).Width = (_Width * 0.3) + 60

            C1StandardCAS.Cols(COL_STD_GROUP).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
            C1StandardCAS.Cols(COL_STD_REASON).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
            C1StandardCAS.Cols(COL_STD_CASDESC).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter

            C1StandardCAS.Cols(COL_STD_CASDESC).AllowEditing = False
            C1StandardCAS.AllowSorting = False
        Catch ex As Exception
            MessageBox.Show("Error in Insurance payer setup: " + ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        End Try
    End Sub
    Private Sub FillInsurancePayerStandardSetup()
        Try
            Dim _dt As DataTable
            _dt = GetInsurancePayerStandardSetup()
            If Not IsNothing(_dt) Then
                Dim _RowIndex As Integer = -1
                For iRow As Integer = 0 To _dt.Rows.Count - 1
                    C1StandardCAS.Rows.Add()
                    _RowIndex = C1StandardCAS.Rows.Count - 1

                    C1StandardCAS.SetCellStyle(_RowIndex, COL_STD_GROUP, C1StandardCAS.Styles("csGroupCodes"))

                    C1StandardCAS.SetData(_RowIndex, COL_STD_REASONCODEID, _dt.Rows(iRow)("nReasonCodeID").ToString())
                    C1StandardCAS.SetData(_RowIndex, COL_STD_GROUP, _dt.Rows(iRow)("GroupCode").ToString())
                    C1StandardCAS.SetData(_RowIndex, COL_STD_REASON, _dt.Rows(iRow)("ReasonCode").ToString())
                    C1StandardCAS.SetData(_RowIndex, COL_STD_CASTYPE, _dt.Rows(iRow)("CASType").ToString())
                    C1StandardCAS.SetData(_RowIndex, COL_STD_CASDESC, _dt.Rows(iRow)("CASTypeDesc"))
                Next
            End If
        Catch ex As Exception
            MessageBox.Show("Error in Insurance payer setup: " + ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        End Try
    End Sub
    Private Function SaveInsurancePayerSetup() As Boolean
        Dim _result As Boolean = False
        Dim _CASLines As InsuranceCASLines = Nothing
        Dim _InsuranceCASLines As List(Of InsuranceCASLines) = New List(Of InsuranceCASLines)
        Try
            If ValidateCASLines() Then
                For iRow As Integer = 1 To C1StandardCAS.Rows.Count - 1
                    _CASLines = New InsuranceCASLines()

                    If Not IsNothing(C1StandardCAS.GetData(iRow, COL_STD_REASONCODEID)) Then
                        _CASLines.CASID = Convert.ToInt64(C1StandardCAS.GetData(iRow, COL_STD_REASONCODEID))
                    End If

                    If Not IsNothing(C1StandardCAS.GetData(iRow, COL_STD_GROUP)) Then
                        _CASLines.GroupCode = Convert.ToString(C1StandardCAS.GetData(iRow, COL_STD_GROUP))
                    End If
                    If Not IsNothing(C1StandardCAS.GetData(iRow, COL_STD_REASON)) Then
                        _CASLines.ReasonCode = Convert.ToString(C1StandardCAS.GetData(iRow, COL_STD_REASON))
                    End If
                    If Not IsNothing(C1StandardCAS.GetData(iRow, COL_STD_CASTYPE)) Then
                        _CASLines.CASReasonType = Convert.ToString(C1StandardCAS.GetData(iRow, COL_STD_CASTYPE))
                    End If
                    If Not IsNothing(C1StandardCAS.GetData(iRow, COL_STD_CASDESC)) Then
                        _CASLines.CASReasonTypeDesc = Convert.ToString(C1StandardCAS.GetData(iRow, COL_STD_CASDESC))
                    End If
                    _CASLines.ClinicID = Convert.ToInt64(gloPMAdmin.gnClinicID)
                    _CASLines.UserID = Convert.ToInt64(gloPMAdmin.gnLoginID)
                    _InsuranceCASLines.Add(_CASLines)
                Next

                _result = SaveInsurancePaymentReasonCodeSetup(_InsuranceCASLines)
            End If

        Catch ex As Exception
            MessageBox.Show("Error in Insurance payer setup: " + ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        End Try


        Return _result
    End Function
    Private Function SaveInsurancePaymentReasonCodeSetup(ByVal _ReasonCodeLines As List(Of InsuranceCASLines)) As Boolean
        Dim _result As Boolean = False
        Dim oDB As New gloDatabaseLayer.DBLayer(gloPMAdmin.mdlGeneral.GetConnectionString())
        Dim oDBParameters As New gloDatabaseLayer.DBParameters
        Try
            If Not IsNothing(_ReasonCodeLines) Then
                For i As Integer = 0 To _ReasonCodeLines.Count - 1
                    oDB.Connect(False)
                    oDBParameters.Clear()
                    oDBParameters.Add("@nReasonCodeID", _ReasonCodeLines(i).CASID, ParameterDirection.Input, SqlDbType.BigInt)
                    oDBParameters.Add("@sGroupCode", _ReasonCodeLines(i).GroupCode, ParameterDirection.Input, SqlDbType.VarChar)
                    oDBParameters.Add("@sReasonCode", _ReasonCodeLines(i).ReasonCode, ParameterDirection.Input, SqlDbType.VarChar)
                    oDBParameters.Add("@nCASType", _ReasonCodeLines(i).CASReasonType, ParameterDirection.Input, SqlDbType.Int)
                    oDBParameters.Add("@sCASTypeDesc", _ReasonCodeLines(i).CASReasonTypeDesc, ParameterDirection.Input, SqlDbType.VarChar)
                    oDBParameters.Add("@nClinicID", _ReasonCodeLines(i).ClinicID, ParameterDirection.Input, SqlDbType.BigInt)
                    oDBParameters.Add("@nUserID", _ReasonCodeLines(i).UserID, ParameterDirection.Input, SqlDbType.BigInt)
                    oDB.Execute("gsp_INUP_InsurancePaymentReasonCode", oDBParameters)
                    oDB.Disconnect()
                Next
                _result = True
            End If
        Catch ex As Exception
            oDB.Disconnect()
            MessageBox.Show("Error in Insurance payer setup: " + ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
            _result = False
        Finally
            If Not IsNothing(oDB) Then
                oDB.Dispose()
                oDB = Nothing
            End If
            If Not IsNothing(oDBParameters) Then
                oDBParameters.Dispose()
                oDBParameters = Nothing
            End If
        End Try
        Return _result
    End Function
    Private Function GetInsurancePayerStandardSetup() As DataTable
        Dim _dtCodes As DataTable
        Dim oDB As New gloDatabaseLayer.DBLayer(gloPMAdmin.mdlGeneral.GetConnectionString())
        Dim oDBParameters As New gloDatabaseLayer.DBParameters
        Try
            oDB.Connect(False)
            'oDBParameters.Add("@nUserID", gloPMAdmin.mdlGeneral.gnLoginID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt)
            oDBParameters.Add("@nClinicID", gloPMAdmin.mdlGeneral.gnClinicID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt)
            oDB.Retrive("gsp_Get_DefaultInsuranceReasonCode", oDBParameters, _dtCodes)
            oDB.Disconnect()
        Catch ex As Exception
            oDB.Disconnect()
            MessageBox.Show("Error in Insurance payer setup: " + ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        Finally
            If Not IsNothing(oDB) Then
                oDB.Dispose()
                oDB = Nothing
            End If
            If Not IsNothing(oDBParameters) Then
                oDBParameters.Dispose()
                oDBParameters = Nothing
            End If
        End Try
        Return _dtCodes
    End Function

    Private Function GetGroupCodeString() As String
        Dim oResult As Object = Nothing
        Dim sResult As String = String.Empty
        Dim oDB As New gloDatabaseLayer.DBLayer(gloPMAdmin.mdlGeneral.GetConnectionString())
        Dim oDBParameters As New gloDatabaseLayer.DBParameters
        Try
            oDB.Connect(False)
            oDBParameters.Add("@GroupString", "", System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar)
            oResult = oDB.ExecuteScalar("ERA_GetGroupCodeString", oDBParameters)
            If Not IsNothing(oResult) And Convert.ToString(oResult) <> "" Then
                sResult = Convert.ToString(oResult)
            End If
            oDB.Disconnect()
        Catch ex As Exception
            oDB.Disconnect()
            MessageBox.Show("Error in Insurance payer setup: " + ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        Finally
            If Not IsNothing(oDB) Then
                oDB.Dispose()
                oDB = Nothing
            End If
            If Not IsNothing(oDBParameters) Then
                oDBParameters.Dispose()
                oDBParameters = Nothing
            End If
        End Try
        Return sResult
    End Function

    Private Function ValidateCASLines() As Boolean
        For iRow As Integer = 1 To C1StandardCAS.Rows.Count - 1
            If C1StandardCAS.GetData(iRow, COL_STD_GROUP) = Nothing Or C1StandardCAS.GetData(iRow, COL_STD_GROUP).ToString() = "" Then
                C1StandardCAS.Select(iRow, COL_STD_GROUP)
                C1StandardCAS.Select()
                MessageBox.Show("Enter complete standard reason code parameters.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Return False
            End If
            If C1StandardCAS.GetData(iRow, COL_STD_REASON) = Nothing Or C1StandardCAS.GetData(iRow, COL_STD_REASON).ToString() = "" Then
                C1StandardCAS.Select(iRow, COL_STD_REASON)
                C1StandardCAS.Select()
                MessageBox.Show("Enter complete standard reason code parameters.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Return False
            End If
        Next
        Return True
    End Function

    Private Sub FillInsurancePaymentResoneCodeSetup(ByVal dt As DataTable)
        Try
            Dim oResult As Integer = 0
            If dt IsNot Nothing Then
                If (dt.Rows.Count > 0) Then
                    oResult = Convert.ToInt16(dt.Rows(0)("sSettingsValue"))
                Else
                    oResult = 0
                End If
            Else
                oResult = 0
            End If

            gloGlobal.gloPMGlobal.ReasonCodeSetup = oResult

            If oResult <> 0 Then
                If oResult = 1 Then
                    rbInsStandardSetup.Checked = True
                ElseIf oResult = 2 Then
                    rbInsPayerSetup.Checked = True
                ElseIf oResult = 3 Then
                    rbInsManualSetup.Checked = True
                End If
            End If

        Catch ex As Exception
            MessageBox.Show("Error in Insurance payer setup: " + ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        End Try
    End Sub
#End Region

    Private Sub chkEnableLocalPrinter_CheckedChanged(sender As Object, e As System.EventArgs) Handles chkEnableLocalPrinter.CheckedChanged
        If chkEnableLocalPrinter.Checked = False Then
            pnlPrintClaims.Enabled = False
        Else
            pnlPrintClaims.Enabled = True
        End If
    End Sub

    Private Sub rbPrintClaimsPDF_CheckedChanged(sender As Object, e As System.EventArgs) Handles rbPrintClaimsPDF.CheckedChanged
        If rbPrintClaimsPDF.Checked = True Then
            rbPrintClaimsPDF.Font = gloGlobal.clsgloFont.gFont_BOLD
        Else
            rbPrintClaimsPDF.Font = gloGlobal.clsgloFont.gFont
        End If
    End Sub

    Private Sub rbPrintClaimsEMF_CheckedChanged(sender As Object, e As System.EventArgs) Handles rbPrintClaimsEMF.CheckedChanged
        If rbPrintClaimsEMF.Checked = True Then
            rbPrintClaimsEMF.Font = gloGlobal.clsgloFont.gFont_BOLD
        Else
            rbPrintClaimsEMF.Font = gloGlobal.clsgloFont.gFont
        End If
    End Sub

    Private Sub btnClearFont_Cms_Click(sender As System.Object, e As System.EventArgs) Handles btnClearFont_Cms.Click
        txtFont_Cms.Text = ""
    End Sub

    Private Sub btnBrowseFont_Cms_Click(sender As System.Object, e As System.EventArgs) Handles btnBrowseFont_Cms.Click
        'Dim fDialoug As New FontDialog()
        'Dim cFont As Font
        'Try
        '    If txtFont_Cms.Text <> "" Then
        '        Dim cmsFont() As String = txtFont_Cms.Text.Split(",")
        '        If (cmsFont.Length > 0) Then
        '            cFont = New Font(Convert.ToString(cmsFont(0)), Convert.ToSingle(cmsFont(1)))
        '            fDialoug.Font = cFont
        '        End If
        '    End If
        '    fDialoug.ShowEffects = False
        '    If fDialoug.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
        '        txtFont_Cms.Text = fDialoug.Font.FontFamily.Name.ToString() + "," + Convert.ToSingle(fDialoug.Font.Size).ToString()
        '    Else
        '        If txtFont_Cms.Text = "" Then
        '            txtFont_Cms.Text = ""
        '        End If
        '    End If
        If txtFont_Cms.Text <> "" Then
            Dim cmsFont() As String = txtFont_Cms.Text.Split(",")
            Dim fDialoug As New frmCMS_UBFontSetup(Convert.ToString(cmsFont(0)), Convert.ToString(cmsFont(1)))

            Try
                fDialoug.ShowDialog()
                If fDialoug.bIsSave Then
                    txtFont_Cms.Text = fDialoug.sSelectedfont + "," + fDialoug.sSelectedfontSize
                End If
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
            Finally
                fDialoug.Dispose()
                fDialoug = Nothing
            End Try
        End If


    End Sub

    Private Sub btnBrowseFont_Ub_Click(sender As System.Object, e As System.EventArgs) Handles btnBrowseFont_Ub.Click
        If txtFont_Ub.Text <> "" Then
            Dim ubFont() As String = txtFont_Ub.Text.Split(",")
            Dim fDialoug As New frmCMS_UBFontSetup(Convert.ToString(ubFont(0)), Convert.ToString(ubFont(1)))

            Try
                fDialoug.ShowDialog()
                If fDialoug.bIsSave Then
                    txtFont_Ub.Text = fDialoug.sSelectedfont + "," + fDialoug.sSelectedfontSize
                End If
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
            Finally
                fDialoug.Dispose()
                fDialoug = Nothing
            End Try
        End If


    End Sub

    Private Sub btnClearFont_Ub_Click(sender As System.Object, e As System.EventArgs) Handles btnClearFont_Ub.Click
        txtFont_Ub.Text = ""
    End Sub

    Private Sub FillMergeScheduledAction(dt As DataTable)
        Try
            If dt IsNot Nothing Then
                If (dt.Rows.Count > 0) Then
                    Dim oResult As Boolean = False
                    Boolean.TryParse(Convert.ToString(dt.Rows(0)("sSettingsValue")), oResult)
                    chkMergeScheduleAction.Checked = oResult
                Else
                    chkMergeScheduleAction.Checked = False
                End If
            Else
                chkMergeScheduleAction.Checked = False
            End If

        Catch ex As Exception
            MessageBox.Show("Error in Fill Merge Scheduled Action: " + ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        End Try
    End Sub

    Private Sub chkEnableCMSfontsizeselection_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkEnableCMSfontsizeselection.CheckedChanged
        If chkEnableCMSfontsizeselection.Checked Then
            txtFont_Cms.Enabled = True
            btnBrowseFont_Cms.Enabled = True
        Else
            txtFont_Cms.Enabled = False
            btnBrowseFont_Cms.Enabled = False
        End If
    End Sub

    Private Sub ChkEnableUB04FontSelection_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles ChkEnableUB04FontSelection.CheckedChanged
        If ChkEnableUB04FontSelection.Checked Then
            txtFont_Ub.Enabled = True
            btnBrowseFont_Ub.Enabled = True
        Else
            txtFont_Ub.Enabled = False
            btnBrowseFont_Ub.Enabled = False
        End If

    End Sub

    Private Sub C1StandardCAS_AfterEdit(sender As System.Object, e As C1.Win.C1FlexGrid.RowColEventArgs) Handles C1StandardCAS.AfterEdit
        Dim _sReasonCode As String = String.Empty
        Dim _sGroupCode As String = String.Empty

        If Not IsNothing(C1StandardCAS.GetData(e.Row, COL_STD_GROUP)) Then
            _sGroupCode = Convert.ToString(C1StandardCAS.GetData(e.Row, COL_STD_GROUP)).ToLower()
        End If
        If Not IsNothing(C1StandardCAS.GetData(e.Row, COL_STD_REASON)) Then
            _sReasonCode = Convert.ToString(C1StandardCAS.GetData(e.Row, COL_STD_REASON)).ToLower()
        End If
        For i As Integer = 1 To C1StandardCAS.Rows.Count - 1
            If i <> e.Row Then
                If (Convert.ToString(C1StandardCAS.GetData(i, COL_STD_GROUP)).ToLower() = _sGroupCode AndAlso Convert.ToString(C1StandardCAS.GetData(i, COL_STD_REASON)).ToLower() = _sReasonCode) Then
                    MessageBox.Show(_sGroupCode + _sReasonCode + " Already Exists. Enter unique Reason Code and Group Code.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    C1StandardCAS.SetData(e.Row, COL_STD_REASON, "")
                End If
            End If
        Next
        If _sReasonCode <> "" And _sGroupCode <> "" Then
            If IsReasonCodePresent(_sReasonCode, _sGroupCode) = False Then
                MessageBox.Show(String.Format("Selected Group & Reason code ""{0} {1}"" is not present in Reason Code Master.", _sGroupCode.ToUpper(), _sReasonCode.ToUpper()), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Question)
                C1StandardCAS.SetData(e.Row, COL_STD_REASON, "")
                Return
            End If
        End If

    End Sub

    Public Function IsReasonCodePresent(_sReasonCode As String, _sGroupCode As String) As Boolean
        Dim _Result As Boolean = False
        Dim _dt As DataTable = Nothing
        Dim oDB As New gloDatabaseLayer.DBLayer(gloPMAdmin.mdlGeneral.GetConnectionString())
        Dim oDBParameters As New gloDatabaseLayer.DBParameters()
        Try
            oDB.Connect(False)

            oDBParameters.Add("@sReasonCode", _sReasonCode, ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@sReasonGroupCode", _sGroupCode, ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@nClinicID", _ClinicID, ParameterDirection.Input, SqlDbType.BigInt)
            oDB.Retrive("ERA_GetReasonCode", oDBParameters, _dt)
            If _dt IsNot Nothing AndAlso _dt.Rows.Count > 0 Then
                _Result = True
            End If
        Catch DBErr As gloDatabaseLayer.DBException
            MessageBox.Show(DBErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            _Result = False
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            _Result = False
        Finally
            oDB.Disconnect()
            oDBParameters.Dispose()
            oDB.Dispose()
        End Try

        Return _Result
    End Function

    '    Public Sub chkApptFacility_CheckedChanged(sender As System.Object, e As System.EventArgs)
    '        Dim _dtResult As DataTable = Nothing
    '        Dim oDB As New gloDatabaseLayer.DBLayer(gloPMAdmin.mdlGeneral.GetConnectionString())
    '        Try
    '            oDB.Connect(False)
    '            oDB.Retrive("Get_FacilityStatus", _dtResult)
    '            If _dtResult.Rows.Count > 0 Then
    '                If chkApptFacility.Checked = True Then
    '                    MessageBox.Show("Same Location uses More than one Facility", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
    '                    chkApptFacility.Checked = False
    '                    Return
    '                End If
    '            End If
    '        Catch ex As Exception
    '            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        End Try

    'End Sub

    Private Sub rdbYesOCP_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rdbYesOCP.CheckedChanged
        If rdbYesOCP.Checked = True Then
            rdbYesOCP.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            rdbYesOCP.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub rdbNoOCP_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rdbNoOCP.CheckedChanged
        If rdbNoOCP.Checked = True Then
            rdbNoOCP.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            rdbNoOCP.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub GetRCM_DOCCategory()
        Dim oDB As New gloDatabaseLayer.DBLayer(GetDMSConnectionString(gloPMAdmin.mdlGeneral.GetConnectionString))
        Dim dtCategory As New DataTable()
        Try
            oDB.Connect(False)
            oDB.Retrive_Query("SELECT CategoryId,CategoryName FROM eDocument_Category_V3_RCM WITH(NOLOCK) WHERE isnull(CategoryName,'') <> '' ORDER BY CategoryName", dtCategory)

            Dim dr As DataRow = dtCategory.NewRow()
            dr("CategoryId") = 0
            dr("CategoryName") = ""
            dtCategory.Rows.InsertAt(dr, 0)
            cmbOCPDMSCategory.DataSource = dtCategory
            cmbOCPDMSCategory.DisplayMember = "CategoryName"
            cmbOCPDMSCategory.ValueMember = "CategoryId"
        Catch ex As Exception
            MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not IsNothing(oDB) Then
                oDB.Dispose()
                oDB = Nothing
            End If
            If Not IsNothing(dtCategory) Then
                dtCategory = Nothing
            End If
        End Try
    End Sub

    Public Function GetDMSConnectionString(ByVal strSQLServerName As String, ByVal strDatabase As String, ByVal isSQLAuthentication As Boolean, ByVal sUserName As String, ByVal sPassword As String) As String
        Dim strConnectionString As String = Nothing
        If isSQLAuthentication = False Then
            strConnectionString = "SERVER=" & strSQLServerName & ";DATABASE=" & strDatabase & ";Integrated Security=SSPI"
        Else
            strConnectionString = "SERVER=" & strSQLServerName & ";DATABASE=" & strDatabase & ";User ID=" & sUserName & ";Password=" & sPassword & ""
        End If

        Return strConnectionString
    End Function

    Public Function GetDMSConnectionString(ByVal DatabaseConnectionString As String) As String
        Dim oSetting As gloSettings.GeneralSettings = New gloSettings.GeneralSettings(DatabaseConnectionString)
        Dim sDmsServerName As String = ""
        Dim sDmsDatabaseName As String = ""
        Dim sDmsUserId As String = ""
        Dim sDmsPassword As String = ""
        Dim sDmsIsSqlAuthentication As Boolean = False
        Dim oValue As Object = Nothing
        oSetting.GetSetting("GLODMSSERVERNAME", oValue)
        If oValue IsNot Nothing Then
            sDmsServerName = oValue.ToString()
            oValue = Nothing
        End If

        oSetting.GetSetting("GLODMSDBNAME", oValue)
        If oValue IsNot Nothing Then
            sDmsDatabaseName = oValue.ToString()
            oValue = Nothing
        End If

        oSetting.GetSetting("GLODMSUSERID", oValue)
        If oValue IsNot Nothing Then
            sDmsUserId = oValue.ToString()
            oValue = Nothing
        End If

        oSetting.GetSetting("GLODMSPASSWORD", oValue)
        If oValue IsNot Nothing Then
            sDmsPassword = oValue.ToString()
            oValue = Nothing
        End If

        oSetting.GetSetting("GLODMSAUTHEN", oValue)
        If oValue IsNot Nothing Then
            sDmsIsSqlAuthentication = Convert.ToBoolean(oValue)
            oValue = Nothing
        End If

        oSetting.Dispose()
        oSetting = Nothing
        Return GetDMSConnectionString(sDmsServerName, sDmsDatabaseName, sDmsIsSqlAuthentication, sDmsUserId, sDmsPassword)
    End Function

    Private Sub chkUseBatch_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkUseBatch.CheckedChanged
        If (txtClaimPrefixValue.Text.Trim) = "" And chkUseBatch.Checked <> False Then
            chkUseBatch.Checked = False
            MessageBox.Show("Please First Enter the ClaimPrefix", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub chkUseClaim_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkUseClaim.CheckedChanged
        If (txtClaimPrefixValue.Text.Trim) = "" And chkUseClaim.Checked <> False Then
            chkUseClaim.Checked = False
            MessageBox.Show("Please First Enter the ClaimPrefix", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub txtClaimPrefixValue_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtClaimPrefixValue.TextChanged
        If txtClaimPrefixValue.Text = "" Then
            chkUseClaim.Checked = False
            chkUseBatch.Checked = False
        End If
    End Sub

    Private Sub txtClaimPrefixValue_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtClaimPrefixValue.KeyPress
        If Not Char.IsLetterOrDigit(e.KeyChar) Then
            If Asc(e.KeyChar) <> 45 And Asc(e.KeyChar) <> 8 Then
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub txtClaimPrefixValue_MouseDown(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles txtClaimPrefixValue.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Right Then
            Clipboard.Clear()
        End If
    End Sub

    
    Private Sub rdbEnableCentralizedRE_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rdbEnableCentralizedRE.CheckedChanged
        If rdbEnableCentralizedRE.Checked = True Then
            rdbEnableCentralizedRE.Font = New Font("Tahoma", 9, FontStyle.Bold)
            txtQCommunicationURL.Enabled = True
        Else
            rdbEnableCentralizedRE.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub rdbDisableCentralizedRE_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rdbDisableCentralizedRE.CheckedChanged
        If rdbDisableCentralizedRE.Checked = True Then
            rdbDisableCentralizedRE.Font = New Font("Tahoma", 9, FontStyle.Bold)
            txtQCommunicationURL.Enabled = False
        Else
            rdbDisableCentralizedRE.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub Rb_CleargageYes_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles Rb_CleargageYes.CheckedChanged
        Try
            If Rb_CleargageYes.Checked Then
                pnlEnableCleargage.Visible = True
            Else
                pnlEnableCleargage.Visible = False
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub Rb_CleargageNo_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles Rb_CleargageNo.CheckedChanged
        Try
            If Rb_CleargageYes.Checked Then
                pnlEnableCleargage.Visible = True
            Else
                pnlEnableCleargage.Visible = False
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub rd_Cleargage_Demo_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rd_Cleargage_Demo.CheckedChanged
        Dim objDecryptPassword As New clsEncryption
        Dim Decrypt_Cleargage_Pwd As String
        Try
            If rd_Cleargage_Demo.Checked Then
                ClearCleargageValues()
                Dim objResult As Object = Nothing

                Dim ogloSetting As gloSettings.GeneralSettings = New gloSettings.GeneralSettings(mdlGeneral.GetConnectionString)
                ogloSetting.GetSetting("Cleargage_ClientID", objResult)
                If Convert.ToString(objResult) <> "" Then
                    txtCGClientID.Text = Convert.ToString(objResult)
                    objResult = Nothing
                End If
                ogloSetting.GetSetting("Cleargage_FirstName", objResult)
                If Convert.ToString(objResult) <> "" Then
                    txtCGFirstName.Text = Convert.ToString(objResult)
                    objResult = Nothing
                End If
                ogloSetting.GetSetting("Cleargage_Host", objResult)
                If Convert.ToString(objResult) <> "" Then
                    txtCGHost.Text = Convert.ToString(objResult)
                    objResult = Nothing
                End If
                ogloSetting.GetSetting("Cleargage_Key", objResult)
                If Convert.ToString(objResult) <> "" Then
                    txtCGKey.Text = Convert.ToString(objResult)
                    objResult = Nothing
                End If
                ogloSetting.GetSetting("Cleargage_LastName", objResult)
                If Convert.ToString(objResult) <> "" Then
                    txtCGLastName.Text = Convert.ToString(objResult)
                    objResult = Nothing
                End If
                ogloSetting.GetSetting("Cleargage_Name", objResult)
                If Convert.ToString(objResult) <> "" Then
                    txtCGName.Text = Convert.ToString(objResult)
                    objResult = Nothing
                End If
                ogloSetting.GetSetting("Cleargage_Password", objResult)
                If Convert.ToString(objResult) <> "" Then
                    Decrypt_Cleargage_Pwd = objDecryptPassword.DecryptFromBase64String(Convert.ToString(objResult), mdlGeneral.constEncryptDecryptKey)
                    txtCGPassword.Text = Decrypt_Cleargage_Pwd
                    objResult = Nothing
                End If
                ogloSetting.GetSetting("Cleargage_PatientID", objResult)
                If Convert.ToString(objResult) <> "" Then
                    txtCGPatientID.Text = Convert.ToString(objResult)
                    objResult = Nothing
                End If
                ogloSetting.GetSetting("Cleargage_PlanID", objResult)
                If Convert.ToString(objResult) <> "" Then
                    txtCGPlanID.Text = Convert.ToString(objResult)
                    objResult = Nothing
                End If
                ogloSetting.GetSetting("Cleargage_SubscriptionID", objResult)
                If Convert.ToString(objResult) <> "" Then
                    txtCGSubscriberID.Text = Convert.ToString(objResult)
                    objResult = Nothing
                End If
                ogloSetting.GetSetting("Cleargage_UserName", objResult)
                If Convert.ToString(objResult) <> "" Then
                    txtCGUserName.Text = Convert.ToString(objResult)
                    objResult = Nothing
                End If
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Cleargage, gloAuditTrail.ActivityCategory.Settings, gloAuditTrail.ActivityType.Save, "Cleargage setting changed to DEMO.", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPMAdmin, True)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub rd_Cleargage_Live_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rd_Cleargage_Live.CheckedChanged
        Dim objDecryptPassword As New clsEncryption
        Dim Decrypt_Cleargage_Pwd As String
        Try
            If rd_Cleargage_Live.Checked Then
                ClearCleargageValues()
                Dim objResult As Object = Nothing

                Dim ogloSetting As gloSettings.GeneralSettings = New gloSettings.GeneralSettings(mdlGeneral.GetConnectionString)
                ogloSetting.GetSetting("CleargageLive_ClientID", objResult)
                If Convert.ToString(objResult) <> "" Then
                    txtCGClientID.Text = Convert.ToString(objResult)
                    objResult = Nothing
                End If
                ogloSetting.GetSetting("CleargageLive_FirstName", objResult)
                If Convert.ToString(objResult) <> "" Then
                    txtCGFirstName.Text = Convert.ToString(objResult)
                    objResult = Nothing
                End If
                ogloSetting.GetSetting("CleargageLive_Host", objResult)
                If Convert.ToString(objResult) <> "" Then
                    txtCGHost.Text = Convert.ToString(objResult)
                    objResult = Nothing
                End If
                ogloSetting.GetSetting("CleargageLive_Key", objResult)
                If Convert.ToString(objResult) <> "" Then
                    txtCGKey.Text = Convert.ToString(objResult)
                    objResult = Nothing
                End If
                ogloSetting.GetSetting("CleargageLive_LastName", objResult)
                If Convert.ToString(objResult) <> "" Then
                    txtCGLastName.Text = Convert.ToString(objResult)
                    objResult = Nothing
                End If
                ogloSetting.GetSetting("CleargageLive_Name", objResult)
                If Convert.ToString(objResult) <> "" Then
                    txtCGName.Text = Convert.ToString(objResult)
                    objResult = Nothing
                End If
                ogloSetting.GetSetting("CleargageLive_Password", objResult)
                If Convert.ToString(objResult) <> "" Then
                    Decrypt_Cleargage_Pwd = objDecryptPassword.DecryptFromBase64String(Convert.ToString(objResult), mdlGeneral.constEncryptDecryptKey)
                    txtCGPassword.Text = Decrypt_Cleargage_Pwd
                    objResult = Nothing
                End If
                ogloSetting.GetSetting("CleargageLive_PatientID", objResult)
                If Convert.ToString(objResult) <> "" Then
                    txtCGPatientID.Text = Convert.ToString(objResult)
                    objResult = Nothing
                End If
                ogloSetting.GetSetting("CleargageLive_PlanID", objResult)
                If Convert.ToString(objResult) <> "" Then
                    txtCGPlanID.Text = Convert.ToString(objResult)
                    objResult = Nothing
                End If
                ogloSetting.GetSetting("CleargageLive_SubscriptionID", objResult)
                If Convert.ToString(objResult) <> "" Then
                    txtCGSubscriberID.Text = Convert.ToString(objResult)
                    objResult = Nothing
                End If
                ogloSetting.GetSetting("CleargageLive_UserName", objResult)
                If Convert.ToString(objResult) <> "" Then
                    txtCGUserName.Text = Convert.ToString(objResult)
                    objResult = Nothing
                End If
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Cleargage, gloAuditTrail.ActivityCategory.Settings, gloAuditTrail.ActivityType.Save, "Cleargage setting changed to LIVE.", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPMAdmin, True)

            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ClearCleargageValues()
        txtCGClientID.Clear()
        txtCGHost.Clear()
        txtCGKey.Clear()
        txtCGName.Clear()
        txtCGPassword.Clear()
        txtCGFirstName.Clear()
        txtCGLastName.Clear()
        txtCGPatientID.Clear()
        txtCGPlanID.Clear()
        txtCGSubscriberID.Clear()
        txtCGUserName.Clear()
    End Sub

End Class


'InsurancePayerSetup'
Public Class InsuranceCASLines
#Region "Properties"
    Private nCASID As Long
    Public Property CASID() As Long

        Get
            Return nCASID
        End Get
        Set(ByVal value As Long
)
            nCASID = value
        End Set
    End Property
    Private sGroupCode As String
    Public Property GroupCode() As String
        Get
            Return sGroupCode
        End Get
        Set(ByVal value As String)
            sGroupCode = value
        End Set
    End Property
    Private sReasonCode As String
    Public Property ReasonCode() As String
        Get
            Return sReasonCode
        End Get
        Set(ByVal value As String)
            sReasonCode = value
        End Set
    End Property
    Private _CASReasonType As enum_CASReasonType
    Public Property CASReasonType() As enum_CASReasonType
        Get
            Return _CASReasonType
        End Get
        Set(ByVal value As enum_CASReasonType)
            _CASReasonType = value
        End Set
    End Property
    Private sCASReasonTypeDesc As String
    Public Property CASReasonTypeDesc() As String
        Get
            Return sCASReasonTypeDesc
        End Get
        Set(ByVal value As String)
            sCASReasonTypeDesc = value
        End Set
    End Property
    Private nClinicID As Long
    Public Property ClinicID() As Long
        Get
            Return nClinicID
        End Get
        Set(ByVal value As Long)
            nClinicID = value
        End Set
    End Property
    Private nUserID As Long
    Public Property UserID() As Long
        Get
            Return nUserID
        End Get
        Set(ByVal value As Long)
            nUserID = value
        End Set
    End Property
    Private _InsCASLines As List(Of InsuranceCASLines)
    Public Property InsCASLine() As List(Of InsuranceCASLines)
        Get
            Return _InsCASLines
        End Get
        Set(ByVal value As List(Of InsuranceCASLines))
            _InsCASLines = value
        End Set
    End Property


#End Region

End Class
