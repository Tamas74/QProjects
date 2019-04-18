<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MainMenu

    '' Inherits System.Windows.Forms.Form
    Inherits gloAUSLibrary.MasterForm


    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            Dim dtpControls() As System.Windows.Forms.DateTimePicker = {dtpToDate, dtpFromDate}
            Dim cntControls() As System.Windows.Forms.Control = {dtpToDate, dtpFromDate}
            Dim CmpControls() As System.Windows.Forms.ContextMenuStrip = {cMnuPatient, cmnuToolStripCustomize, cmnuPatientDetails, cmnu_Tasks, cmnu_messages, CmnuPatientstatus}


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

                If (IsNothing(GloUC_TransactionHistory1) = False) Then
                    Try
                        gloGlobal.cEventHelper.RemoveAllEventHandlers(GloUC_TransactionHistory1)
                        GloUC_TransactionHistory1.Dispose()
                        GloUC_TransactionHistory1 = Nothing
                    Catch ex As Exception

                    End Try
                End If
                Try
                    If (IsNothing(PrintDialog1) = False) Then
                        PrintDialog1.Dispose()
                        PrintDialog1 = Nothing
                    End If
                Catch ex As Exception

                End Try
                'Try
                '    If (IsNothing(ColorDialog1) = False) Then
                '        ColorDialog1.Dispose()
                '        ColorDialog1 = Nothing
                '    End If
                'Catch ex As Exception

                'End Try
                Try
                    If (IsNothing(printDoc) = False) Then
                        gloGlobal.cEventHelper.RemoveAllEventHandlers(printDoc)
                        printDoc.Dispose()
                        printDoc = Nothing
                    End If
                Catch ex As Exception

                End Try
                Try
                    If (IsNothing(_flexGroup) = False) Then
                        gloGlobal.cEventHelper.RemoveAllEventHandlers(_flexGroup)
                        _flexGroup.Dispose()
                        _flexGroup = Nothing
                    End If
                Catch ex As Exception

                End Try
                Try
                    If (IsNothing(HelpComponent1) = False) Then
                        gloGlobal.cEventHelper.RemoveAllEventHandlers(HelpComponent1)
                        HelpComponent1.Dispose()
                        HelpComponent1 = Nothing
                    End If

                Catch ex As Exception

                End Try
                objWord = Nothing
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
        Dim Label82 As System.Windows.Forms.Label
        Dim Label29 As System.Windows.Forms.Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainMenu))
        Me.ToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.btnModifyPatientAlert = New System.Windows.Forms.Button()
        Me.btnClosePatientAlert = New System.Windows.Forms.Button()
        Me.btnPC_MoveFirst = New System.Windows.Forms.Button()
        Me.btnPC_MovePrevious = New System.Windows.Forms.Button()
        Me.btnPC_MoveNext = New System.Windows.Forms.Button()
        Me.btnPC_MoveLast = New System.Windows.Forms.Button()
        Me.btnPC_PrintCards = New System.Windows.Forms.Button()
        Me.btnPC_DeleteCard = New System.Windows.Forms.Button()
        Me.btnPC_ScanCard = New System.Windows.Forms.Button()
        Me.btnReset = New System.Windows.Forms.Button()
        Me.btnIntuitMsg = New System.Windows.Forms.Button()
        Me.picLockScreen = New System.Windows.Forms.PictureBox()
        Me.pnlMainToolBar = New System.Windows.Forms.Panel()
        Me.tlbStripMain = New gloGlobal.gloToolStripIgnoreFocus()
        Me.tlbbtn_Microphone = New System.Windows.Forms.ToolStripButton()
        Me.tlbbtn_NewPatient = New System.Windows.Forms.ToolStripButton()
        Me.tlbbtn_ModifyPatient = New System.Windows.Forms.ToolStripButton()
        Me.tlbbtn_ScanCard = New System.Windows.Forms.ToolStripButton()
        Me.tlsPatRegSep = New System.Windows.Forms.ToolStripSeparator()
        Me.tlbbtn_Vitals = New System.Windows.Forms.ToolStripButton()
        Me.tlbbtn_History = New System.Windows.Forms.ToolStripButton()
        Me.tlbbtn_Prescription = New System.Windows.Forms.ToolStripButton()
        Me.tlbbtn_gloLabOrders = New System.Windows.Forms.ToolStripButton()
        Me.tlbbtn_LabOrders = New System.Windows.Forms.ToolStripButton()
        Me.tlsMainSep = New System.Windows.Forms.ToolStripSeparator()
        Me.tlbbtn_NewExam = New System.Windows.Forms.ToolStripButton()
        Me.tlbbtn_PastExam = New System.Windows.Forms.ToolStripButton()
        Me.tlbbtn_UnFinishedExams = New System.Windows.Forms.ToolStripButton()
        Me.tlsExamSep = New System.Windows.Forms.ToolStripSeparator()
        Me.tlbbtn_Calender = New System.Windows.Forms.ToolStripButton()
        Me.tlbbtn_Sechedule = New System.Windows.Forms.ToolStripButton()
        Me.tlsCalenderSep = New System.Windows.Forms.ToolStripSeparator()
        Me.tlbbtn_RCMDocs = New System.Windows.Forms.ToolStripButton()
        Me.tlbbtn_ScanDocs = New System.Windows.Forms.ToolStripButton()
        Me.tlsDOCmgntSep = New System.Windows.Forms.ToolStripSeparator()
        Me.tlbbtn_FormGallery = New System.Windows.Forms.ToolStripButton()
        Me.tlbbtn_PatientSynopsis = New System.Windows.Forms.ToolStripButton()
        Me.tlsFormGalerySep = New System.Windows.Forms.ToolStripSeparator()
        Me.tlbbtn_RxHub = New System.Windows.Forms.ToolStripButton()
        Me.tlbbtn_LockScreen = New System.Windows.Forms.ToolStripButton()
        Me.tlbbtn_Close = New System.Windows.Forms.ToolStripButton()
        Me.tlbbtnSaveCDS = New System.Windows.Forms.ToolStripButton()
        Me.tlbbtn_GenerateCDA = New System.Windows.Forms.ToolStripButton()
        Me.tlbbtn_GenerateCCD = New System.Windows.Forms.ToolStripButton()
        Me.tlbbtn_Timeline = New System.Windows.Forms.ToolStripButton()
        Me.tlbbtn_Inbox = New System.Windows.Forms.ToolStripButton()
        Me.tlbbtn_Orders = New System.Windows.Forms.ToolStripButton()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.pnlMainMenu = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.mnuFile = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnu_DashBoard = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuFileImport = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuFileImport_Genius = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuFileExit = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMasters = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMastersDrugs = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMaster_DrugProviderAssociation = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMastersSIG = New System.Windows.Forms.ToolStripMenuItem()
        Me.sep2 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuMastersTemplates = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuLiquidData = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMastersCategory = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMedicalCategory = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMastersDMSCategory = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMastersRCMCategory = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuEducationMapping = New System.Windows.Forms.ToolStripMenuItem()
        Me.sep3 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuMasterROS = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMastersHistory = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMastersOBPlan = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMastersFamilyMember = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuVital = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuTableTemplate = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuVitalNorms = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuOBVital = New System.Windows.Forms.ToolStripMenuItem()
        Me.sep4 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuMastersContacts = New System.Windows.Forms.ToolStripMenuItem()
        Me.sep5 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuMastersLabSetup = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMastersRadiology = New System.Windows.Forms.ToolStripMenuItem()
        Me.sep6 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuMastersFlowsheet = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMastersSpeciality = New System.Windows.Forms.ToolStripMenuItem()
        Me.sep7 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnu_DM_Setup = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnu_IM_Setup = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnu_CV_Setup = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnu_TaxID_Setup = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator22 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuMaster_ClinicalInstructions = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMaster_CarePlan = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnu_ImplantableDevice_Setup = New System.Windows.Forms.ToolStripMenuItem()
        Me.sep8 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuMaster_SmartDiagnosis = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMaster_SmartTreatment = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMaster_SmartOrder = New System.Windows.Forms.ToolStripMenuItem()
        Me.sep9 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuMaster_FormGallery = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMaster_StatusUsers = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMaster_PatientSummaryScreen = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMaster_AppointmentBook = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnu_BillingConfiguration = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnu_TaskMailConfiguration = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMaster_ICDCPTGallery = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMaster_ICD9Gallery = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMaster_ICD10Gallery = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMaster_CPTGallery = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMastersDisclosuerSet = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnu_TemplateAssociation = New System.Windows.Forms.ToolStripMenuItem()
        Me.ZipToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuRights = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuPatient = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuPatientRegistration = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuModifyPatient = New System.Windows.Forms.ToolStripMenuItem()
        Me.sep10 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuPatientROS = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuPatientHistory = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuPatientVitals = New System.Windows.Forms.ToolStripMenuItem()
        Me.sep11 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuPatientMessages = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuPatientPrescription = New System.Windows.Forms.ToolStripMenuItem()
        Me.sep12 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuPatient_LabOrder = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuPatient_RadiologyOrders = New System.Windows.Forms.ToolStripMenuItem()
        Me.sep13 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuPatientLetters = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuPatientPTProtocols = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuPatientConcent = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuNurseNotes = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuDisclosureMgmt = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuPatientEducation = New System.Windows.Forms.ToolStripMenuItem()
        Me.sep14 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuPatientFlowSheet = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuPatient_FormGallery = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuPatient_Tasks = New System.Windows.Forms.ToolStripMenuItem()
        Me.sep15 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuProblemList = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuPlanOfTreatment = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnu_IM_Transaction = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuPatient_FindHealthPlan = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnu_ViewRecommendation = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnu_UploadVideo = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnu_ScanDocs = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnu_ImplantableDevices = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnu_ScreeningTools = New System.Windows.Forms.ToolStripMenuItem()
        Me.HOOSJRToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.KOOSJRKNEESurveyToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.HOOSTotalAssesmentToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.KOOSKNEESurveyToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PROMIS10ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PROMIS29ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.VETERANSRAND12ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.VETERANSRAND36SurveyToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PHQ2ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnu_BillingCharges = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnu_BillingBatch = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnu_BillingPayment = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnu_BillingAdvPayment = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnu_BillingRemittance = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnu_BillingBalance = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnu_BillingLedger = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnu_Appointment = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnu_Schedule = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnu_ClosedJournals = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuView_Triage1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnu_ClinicalChartPrintQueue = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnu_SocPsycBehobs = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnu_PortalPHI = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnu_APIHarness = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnu_APIHarness_Roles = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnu_APIHarness_Users = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnu_CCDAPatientConsent = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuView = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuView_PatientVitals = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuView_OBVitals = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuView_Tasks = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuView_Mails = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuView_Messages = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuView_PatientEducation = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuView_FormGallery = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuView_Referrals = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuView_PatientLetters = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuView_PTProtocol = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuView_PatientConcent = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuConsentTracking = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuView_NurseNotes = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuView_DisclosureMgmt = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuView_PatientVideo = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuViewReceivedFaxes = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuOutStandingOrders = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuViewPatientSummaryScreen = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator18 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuViewPrescriptions = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuRefillRequest = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuVwDeniedRefillReq = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator21 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuPendingRxChangeRequest = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuVwDeniedChangeReq = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuRxFillNotifications = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuVwErrorMessages = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator20 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnu_ViewPendingLabOrders = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuDicomViewer = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnu_ViewPatientSynopsis = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuView_PatientConfidential = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuView_CardioVascularRisk = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuView_PatientChiefComplaints = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuClinicalInstruction = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuCarePlan = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuView_Triage = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuViewCCDFiles = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuVwRECList = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuVwSummaryCareRecord = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuViewCDSFiles = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnu_ViewDocument = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnu_ViewSchedule = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuVwHL7MessageQueue = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuGeneralMessageQueue = New System.Windows.Forms.ToolStripMenuItem()
        Me.ViewScreenings = New System.Windows.Forms.ToolStripMenuItem()
        Me.MessageQueueToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuVwEARdata = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuViewAmendments = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuePARequests = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuPDRPrograms = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuPDMPPrograms = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuTools = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuSettings = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuChangePass = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuTools_CardScanner = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuTools_RefreshDevicesPrinters = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuTools_CardImage = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuTools_DefaultDisplaySettings = New System.Windows.Forms.ToolStripMenuItem()
        Me.sep16 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuTools_Customization = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuTools_PrescriptionProviderAssociation = New System.Windows.Forms.ToolStripMenuItem()
        Me.sep17 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuToolsVoiceCenter = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuDoctorSpeakerConfiguration = New System.Windows.Forms.ToolStripMenuItem()
        Me.sep18 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuExportTemplates = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuImportTemplates = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuUpdateExistingTemplates = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuTool_UpdateOtherTemplates = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuTool_UpgradeTemplates = New System.Windows.Forms.ToolStripMenuItem()
        Me.sep19 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuMergePatRecords = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnu_ClearPatientDocuments = New System.Windows.Forms.ToolStripMenuItem()
        Me.sep20 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuUnlockExams = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuImportVitalGraphData = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnu_TimeSynchronization = New System.Windows.Forms.ToolStripMenuItem()
        Me.sep21 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuUpdateExam = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuUpdateTemplates = New System.Windows.Forms.ToolStripMenuItem()
        Me.sep22 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuImportCCD = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuGenerateCDA = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuGenerateCCD = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuToolsCDAPatientInfoStatus = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuTools_SendSecureMessage = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem3 = New System.Windows.Forms.ToolStripMenuItem()
        Me.HealthVaultSendRequestAccess = New System.Windows.Forms.ToolStripMenuItem()
        Me.HealthVaultToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuCustomLink = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator9 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuICDAnalysis = New System.Windows.Forms.ToolStripMenuItem()
        Me.tls_CodeGuide = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuToolsToolbar = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuToolsStatusbar = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuTools_RecoverExam = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuTools_DirectInbox = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuTools_SecureMsg = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuTools_ExportSummary = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuTools_CCDASchedule = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuTools_IntuitPatient = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuTools_QRDAImport = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuTools_ProviderEducation = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuReports = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuReportUnfinishedExam = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuSep_UnfinishedExam = New System.Windows.Forms.ToolStripSeparator()
        Me.mnu_MISReports = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnu_MISReports_PatientPaymentHistory = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnu_MISReports_PatientTransactionHistory = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnu_MISReports_Patientstatement = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator12 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnu_MISReports_ProductionByDoctor = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnu_MISReports_ProductionByFacility = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnu_MISReports_ProductionByDate = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnu_MISReports_ProductionByMonth = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnu_MISReports_ProductionByProcedureGroup = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnu_MISReports_ProductionByProcedureCode = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnu_MISReports_ProductionByInsuranceCarrier = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnu_MISReports_ProductionByFacilityByPatient = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnu_MISReports_ProductionByFacilityByPatientDetail = New System.Windows.Forms.ToolStripMenuItem()
        Me.toolStripSeparator16 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnu_MISReports_ReimbursementByMonth = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnu_MISReports_ReimbursementByMonthDetail = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnu_MISReports_ReimbursementByInsuranceCarrier = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnu_MISReports_ReimbursementByInsuranceByCPT = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnu_MISReports_ReimbursementByCPTByInsurance = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnu_MISReports_ReimbursementByDoctorByInsurance = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnu_MISReports_ReimbursementByInsuranceForCPT = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnu_MISReports_ReimbursementDetailsByAccount = New System.Windows.Forms.ToolStripMenuItem()
        Me.toolStripSeparator26 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnu_MISReports_AgingAnalysis = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnu_MISReports_AgingSummaryByPatient = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnu_MISReports_AgingSummaryByInsuranceCarrier = New System.Windows.Forms.ToolStripMenuItem()
        Me.toolStripSeparator27 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnu_MISReports_ProductionByPhysicianGroup = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnu_MISReports_ProductionAnalysisByFacility = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnu_MISReports_ProductionAnalysisByprocedureGroup = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnu_MISReports_ProductionAnalysisandTrendsByMonth = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnu_MISReports_ProductionTrendsByProcedureGrop = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuFAXReports = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuFAXReport = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuPendingFAXesWithMaxAttempts = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuPendingFAXesWithoutTIFF = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuReportExamPrintFAX = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuReviewExams = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuBatchReferrals = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuBatchPrintTemplates = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuClinicalChart = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuExamFinishReport = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem2 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnu_rpt_Appointments = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnu_rpt_ConfirmAppointments = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnu_rpt_NoShowAppointments = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnu_rpt_CensusAppointments = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnu_MIPS = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnu_MIPS_Quality_2019 = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnu_MIPS_Quality = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnu_MIPS_Quality_2017 = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnu_MIPSACI_2019 = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnu_MIPS_ACI = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMUDashboard_2017_ACI = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator17 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuMUDashboardMainMenu = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMUDashboard_Stage1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMUDashboard_Stage2 = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMUDashboard_Stage2_Mod = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMUDashboard_Stage3 = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMUDashboard = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnu_Stage3_2019 = New System.Windows.Forms.ToolStripMenuItem()
        Me.QualityMeasureDashboardToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MedicaidCensusReportDashboardToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.sep23 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuPatientDemographics = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnurptHCFAReport = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuRptDignosisLabResult = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuRpt_HealthPlan = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuRpt_OpenRecommendations = New System.Windows.Forms.ToolStripMenuItem()
        Me.sep24 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuViewAllCCDCCRFiles = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuUnfinishedReconciliationLists = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuUnfinishedFiles = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnu_DM_Rpt_DueGuideline = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuRptIMDueReport = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuRpt_VaccineInventoryReport = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuRpt_LabGraph = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuRpt_PatientReminderLetters = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnurpt_PatientICD9CPT = New System.Windows.Forms.ToolStripMenuItem()
        Me.CCHIT11ReportToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuReportICD9Rx = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnu_Patient_list_report = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuPatientReminder = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuPatientVitalUsageReport = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuAllergyUsageReport = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuProblemUsageReport = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuDemographicUsageReport = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMedicationUsagereport = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuEprescribingreport = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuHistoryUsageReport = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuReportPatientsAlerts = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuReportClinicalDecision = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuReportBPMeasurement = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuReportPatientHistory = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuPatientBMIReport = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuPatientRxReport = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuOBReport = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuPDRProgramsReports = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuEPCS = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuEPCSAuditTrail = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuControlledSubstanceERX = New System.Windows.Forms.ToolStripMenuItem()
        Me.CustomizableReportsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuRpt_LabManifest = New System.Windows.Forms.ToolStripMenuItem()
        Me.InterfaceReportsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ReviewIntuitPatientsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PatientActivationReportsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuReports_InactiveCPTSReport = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuReports_DrugMigrationReport = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuReports_PortalPHIReview = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnu_APIHarness_Reports = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuWindow = New System.Windows.Forms.ToolStripMenuItem()
        Me.CloseToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuSharepoint = New System.Windows.Forms.ToolStripMenuItem()
        Me.ConnectCommunityToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.JoinExistingGroupToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CreateNewGroupToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CreateProfileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.NewProfileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ModiyProfileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MyCommunityToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ShareToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem22 = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuShareTemplate = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuShareTemplateUpload = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuShareTemplateDownload = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuShareLiquid = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuShareLiquidDataUplaod = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuShareLiquidDataDownload = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuShareHistory = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuShareHistoryUpload = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuShareHistoryDownload = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem6 = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuShareUploadFlowsheet = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuShareddlFlowsheet = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem7 = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuShareDmSetupUpload = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuShareDmSetupDownload = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem8 = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuShareUploadIMSetup = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuShareDownloadIMSetup = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem9 = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuShareUploadCVSetup = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuShareDownloadCVSetup = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuShareSmartDx = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuShareUploadSmartDx = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuShareDownloadSmartDx = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuShareShareSmartDx = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuShareUploadSmTreatment = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuShareDownloadSmTreatment = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem12 = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuShareUploadSmOrder = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuShareDownloadSmOrder = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem13 = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuShareUploadformglry = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuShareDlformglry = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem15 = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnushupappconf = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnushdlappconf = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem16 = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnushupblconf = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnushdlblconf = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnushtaskmail = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnushuptaskmail = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnushdntaskmail = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuSPReports = New System.Windows.Forms.ToolStripMenuItem()
        Me.CToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnutemplate = New System.Windows.Forms.ToolStripMenuItem()
        Me.LiquidDataToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.HistoryToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.FlowsheetToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DMSetupToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.IMSetupToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CVSetupToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuSmartDiag = New System.Windows.Forms.ToolStripMenuItem()
        Me.SmartTreatmentToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SmartOrderToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.FormGalleryToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PatientSummeryToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AppointmentConfigurationToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BillingConfigurationToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TaskMailConfigurationToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.GloSkypeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.VoiceCallToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.VideoCallToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TextMessageToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ConferenceToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CommunityTimeLineToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.JoinGloTimeLineToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PatientCollaborationToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CareExchangeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.GloTimeLineDashboardToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SelectedPatientToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MultiplePatientsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ClinicalExchangeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem19 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem18 = New System.Windows.Forms.ToolStripMenuItem()
        Me.MicrosoftHealthValtToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem20 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem21 = New System.Windows.Forms.ToolStripMenuItem()
        Me.SurescriptsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.GloAnalyticsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.HowAmIDoingToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.HowAreMyPatientsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuChangePwd = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuHelp = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuUserGuide = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuSearch = New System.Windows.Forms.ToolStripMenuItem()
        Me.ContentsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator14 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuSupport = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuAboutUs = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem26 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem27 = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuView_PatientTemplates = New System.Windows.Forms.ToolStripMenuItem()
        Me.pnlMainStatusBar = New System.Windows.Forms.Panel()
        Me.Vcmd = New AxDNSTools.AxDgnVoiceCmd()
        Me.DgnEngineControl1 = New AxDNSTools.AxDgnEngineControl()
        Me.StatusStrip = New System.Windows.Forms.StatusStrip()
        Me.sslbl_Login = New System.Windows.Forms.ToolStripStatusLabel()
        Me.sslbl_SQLServerDatabase = New System.Windows.Forms.ToolStripStatusLabel()
        Me.sslbl_SingleSignOn = New System.Windows.Forms.ToolStripStatusLabel()
        Me.sslbl_MessagePriority = New System.Windows.Forms.ToolStripStatusLabel()
        Me.sslbl_Version = New System.Windows.Forms.ToolStripStatusLabel()
        Me.sslbl_VoiceInfo = New System.Windows.Forms.ToolStripStatusLabel()
        Me.sslbl_CurrentSpeaker = New System.Windows.Forms.ToolStripStatusLabel()
        Me.sslbl_LastModifiedDate = New System.Windows.Forms.ToolStripStatusLabel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.DgnMicBtn1 = New AxDNSTools.AxDgnMicBtn()
        Me.uiPanelManager1 = New Janus.Windows.UI.Dock.UIPanelManager(Me.components)
        Me.pnlPatientCheckInStatus = New Janus.Windows.UI.Dock.UIPanel()
        Me.pnlPatientCheckInStatusContainer = New Janus.Windows.UI.Dock.UIPanelInnerContainer()
        Me.c1PatientCheckInStatus = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.pnlLeft_Nav = New Janus.Windows.UI.Dock.UIPanelGroup()
        Me.pnl_Messages = New Janus.Windows.UI.Dock.UIPanel()
        Me.pnl_MessagesContainer = New Janus.Windows.UI.Dock.UIPanelInnerContainer()
        Me.C1Mesages = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.pnlTasks = New Janus.Windows.UI.Dock.UIPanel()
        Me.pnlTasksContainer = New Janus.Windows.UI.Dock.UIPanelInnerContainer()
        Me.Panel12 = New System.Windows.Forms.Panel()
        Me._flexGroup = New gloEMR.FlexGroupControl()
        Me.Panel9 = New System.Windows.Forms.Panel()
        Me.Panel10 = New System.Windows.Forms.Panel()
        Me.txtTaskSearch = New gloUserControlLibrary.gloSearchTextBox()
        Me.Label75 = New System.Windows.Forms.Label()
        Me.pictureBox2 = New System.Windows.Forms.PictureBox()
        Me.Label76 = New System.Windows.Forms.Label()
        Me.Label77 = New System.Windows.Forms.Label()
        Me.Label78 = New System.Windows.Forms.Label()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.Label79 = New System.Windows.Forms.Label()
        Me.Label80 = New System.Windows.Forms.Label()
        Me.Label81 = New System.Windows.Forms.Label()
        Me.Label70 = New System.Windows.Forms.Label()
        Me.cmbFilters = New System.Windows.Forms.ComboBox()
        Me.Label65 = New System.Windows.Forms.Label()
        Me.pnlViewMoreTask = New System.Windows.Forms.Panel()
        Me.lblLinkViewMoreTask = New System.Windows.Forms.LinkLabel()
        Me.C1UserTasks = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.pnlNav_UnfinishedExams = New Janus.Windows.UI.Dock.UIPanel()
        Me.pnlNav_UnfinishedExams_Container = New Janus.Windows.UI.Dock.UIPanelInnerContainer()
        Me.C1UnfinishedExam = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.pnllblLinkUnfinishedexam = New System.Windows.Forms.Panel()
        Me.lblLinkUnfinishedAll = New System.Windows.Forms.LinkLabel()
        Me.pnlNavigator = New Janus.Windows.UI.Dock.UIPanelGroup()
        Me.pnlNav_Appointments = New Janus.Windows.UI.Dock.UIPanel()
        Me.pnlNav_AppointmentsContainer = New Janus.Windows.UI.Dock.UIPanelInnerContainer()
        Me.C1Appointments = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.pnlNav_Calendar = New Janus.Windows.UI.Dock.UIPanel()
        Me.pnlNav_CalendarContainer = New Janus.Windows.UI.Dock.UIPanelInnerContainer()
        Me.pnlNav_Myday = New Janus.Windows.UI.Dock.UIPanel()
        Me.pnlNav_MydayContainer = New Janus.Windows.UI.Dock.UIPanelInnerContainer()
        Me.pnlNav_Triage = New Janus.Windows.UI.Dock.UIPanel()
        Me.pnlNav_TriageContainer = New Janus.Windows.UI.Dock.UIPanelInnerContainer()
        Me.C1Triage = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.pnlPatientDetail = New Janus.Windows.UI.Dock.UIPanel()
        Me.pnlPatientDetailContainer = New Janus.Windows.UI.Dock.UIPanelInnerContainer()
        Me.pnlPatientDetails = New System.Windows.Forms.Panel()
        Me.C1dgPatientDetails = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.SplitterOrderComments = New System.Windows.Forms.Splitter()
        Me.txtOrderComment = New System.Windows.Forms.RichTextBox()
        Me.pnlArchiveInfo = New System.Windows.Forms.Panel()
        Me.lblInfo = New System.Windows.Forms.Label()
        Me.pnlSearchFilter = New System.Windows.Forms.Panel()
        Me.pnlCancelRx = New System.Windows.Forms.Panel()
        Me.chkCancelRx = New System.Windows.Forms.CheckBox()
        Me.pnlCase = New System.Windows.Forms.Panel()
        Me.Label83 = New System.Windows.Forms.Label()
        Me.cmbCase = New System.Windows.Forms.ComboBox()
        Me.pnlSpeciality = New System.Windows.Forms.Panel()
        Me.Label64 = New System.Windows.Forms.Label()
        Me.cmbTemplateSpeciality = New System.Windows.Forms.ComboBox()
        Me.Panel13 = New System.Windows.Forms.Panel()
        Me.cmbProvider = New System.Windows.Forms.ComboBox()
        Me.lblProvider = New System.Windows.Forms.Label()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.chkGetLatestActive = New System.Windows.Forms.CheckBox()
        Me.Panel14 = New System.Windows.Forms.Panel()
        Me.txtSearchIntuit = New System.Windows.Forms.TextBox()
        Me.cmbStatus = New System.Windows.Forms.ComboBox()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.Panel16 = New System.Windows.Forms.Panel()
        Me.dtpToDate = New System.Windows.Forms.DateTimePicker()
        Me.lblto = New System.Windows.Forms.Label()
        Me.dtpFromDate = New System.Windows.Forms.DateTimePicker()
        Me.chkenbdate = New System.Windows.Forms.CheckBox()
        Me.lblFrom = New System.Windows.Forms.Label()
        Me.Label44 = New System.Windows.Forms.Label()
        Me.Label54 = New System.Windows.Forms.Label()
        Me.Label62 = New System.Windows.Forms.Label()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.pnlPatientDetailsHeaders = New System.Windows.Forms.Panel()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.ts_PatientDetails = New gloGlobal.gloToolStripIgnoreFocus()
        Me.tsbtn_History = New System.Windows.Forms.ToolStripButton()
        Me.tlsHistorySep = New System.Windows.Forms.ToolStripSeparator()
        Me.tsbtn_Insurance = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsbtn_Billing = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsbtn_Medication = New System.Windows.Forms.ToolStripButton()
        Me.tlsMedSep = New System.Windows.Forms.ToolStripSeparator()
        Me.tsbtn_Prescription = New System.Windows.Forms.ToolStripButton()
        Me.tlsRxSep = New System.Windows.Forms.ToolStripSeparator()
        Me.tsbtn_ProblemList = New System.Windows.Forms.ToolStripButton()
        Me.tlsProblemSep = New System.Windows.Forms.ToolStripSeparator()
        Me.tsbtn_ViewDocs = New System.Windows.Forms.ToolStripButton()
        Me.tlsViewDocsSep = New System.Windows.Forms.ToolStripSeparator()
        Me.tsbtn_Orders = New System.Windows.Forms.ToolStripButton()
        Me.tlsOrdersSep = New System.Windows.Forms.ToolStripSeparator()
        Me.tsbtn_Messages = New System.Windows.Forms.ToolStripButton()
        Me.tlsMessageSep = New System.Windows.Forms.ToolStripSeparator()
        Me.tsbtn_PastExam = New System.Windows.Forms.ToolStripButton()
        Me.tlsPastExamSep = New System.Windows.Forms.ToolStripSeparator()
        Me.tsbtn_Vital = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator13 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsbtn_Referrals = New System.Windows.Forms.ToolStripButton()
        Me.tlsNewExamSep = New System.Windows.Forms.ToolStripSeparator()
        Me.tsbtn_PendingFax = New System.Windows.Forms.ToolStripButton()
        Me.tlsPendingFaxSep = New System.Windows.Forms.ToolStripSeparator()
        Me.tsbtn_Sentfax = New System.Windows.Forms.ToolStripButton()
        Me.tlsSentFaxSep = New System.Windows.Forms.ToolStripSeparator()
        Me.tsbtn_Labs = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsbtn_Balance = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsbtn_NurseNotes = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator7 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsbtn_PatientConsent = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator8 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsbtn_PatientLetters = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator19 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsbtn_DisclosureMgt = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator6 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsbtn_AuditTrail = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator15 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsbtn_Appointments = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator11 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsbtn_Selected = New System.Windows.Forms.ToolStripButton()
        Me.tsbtn_Hover = New System.Windows.Forms.ToolStripButton()
        Me.tsbtn_Normal = New System.Windows.Forms.ToolStripButton()
        Me.tsbtn_EligbilityInfo = New System.Windows.Forms.ToolStripButton()
        Me.tsbtn_Triage = New System.Windows.Forms.ToolStripButton()
        Me.tsbtn_PatientEducation = New System.Windows.Forms.ToolStripButton()
        Me.tsbtn_NewExam = New System.Windows.Forms.ToolStripButton()
        Me.tsbtn_PriorAuthorization = New System.Windows.Forms.ToolStripButton()
        Me.tsbtn_PatientTasks = New System.Windows.Forms.ToolStripButton()
        Me.tsbtn_PatientCases = New System.Windows.Forms.ToolStripButton()
        Me.tsbtn_IntuitCommunication = New System.Windows.Forms.ToolStripButton()
        Me.tsbtn_PatientCommunication = New System.Windows.Forms.ToolStripButton()
        Me.tsbtn_Order = New System.Windows.Forms.ToolStripButton()
        Me.tsbtn_NYWCForms = New System.Windows.Forms.ToolStripButton()
        Me.tsbtn_SPB = New System.Windows.Forms.ToolStripButton()
        Me.pnlTask = New Janus.Windows.UI.Dock.UIPanelGroup()
        Me.pnlTasks_MYTask = New Janus.Windows.UI.Dock.UIPanel()
        Me.pnlTasks_MYTaskContainer = New Janus.Windows.UI.Dock.UIPanelInnerContainer()
        Me.pnlTask_TaskRequests = New Janus.Windows.UI.Dock.UIPanel()
        Me.pnlTask_TaskRequestsContainer = New Janus.Windows.UI.Dock.UIPanelInnerContainer()
        Me.pnlTask_RequestsSent = New Janus.Windows.UI.Dock.UIPanel()
        Me.pnlTask_RequestsSentContainer = New Janus.Windows.UI.Dock.UIPanelInnerContainer()
        Me.pnlGrPatientDemoCardsStatus = New Janus.Windows.UI.Dock.UIPanelGroup()
        Me.pnlPatientDemographics = New Janus.Windows.UI.Dock.UIPanel()
        Me.pnlPatientDemographicsContainer = New Janus.Windows.UI.Dock.UIPanelInnerContainer()
        Me.pnlRightFill = New System.Windows.Forms.Panel()
        Me.pnl_workphone = New System.Windows.Forms.Panel()
        Me.lblworkphone = New System.Windows.Forms.Label()
        Me.Label89 = New System.Windows.Forms.Label()
        Me.pnl_Businesscenter = New System.Windows.Forms.Panel()
        Me.lblbusinesscenter = New System.Windows.Forms.Label()
        Me.Label88 = New System.Windows.Forms.Label()
        Me.pnl_Occupation = New System.Windows.Forms.Panel()
        Me.lbloccupation = New System.Windows.Forms.Label()
        Me.Label87 = New System.Windows.Forms.Label()
        Me.pnl_MedicalCategory = New System.Windows.Forms.Panel()
        Me.lblMedicalCategory = New System.Windows.Forms.Label()
        Me.Label69 = New System.Windows.Forms.Label()
        Me.pnl_Language = New System.Windows.Forms.Panel()
        Me.lblLanguage = New System.Windows.Forms.Label()
        Me.Label66 = New System.Windows.Forms.Label()
        Me.pnl_Ethinicity = New System.Windows.Forms.Panel()
        Me.lblEthinicity = New System.Windows.Forms.Label()
        Me.Label68 = New System.Windows.Forms.Label()
        Me.pnl_Race = New System.Windows.Forms.Panel()
        Me.lblRace = New System.Windows.Forms.Label()
        Me.Label67 = New System.Windows.Forms.Label()
        Me.pnl_LabStatus = New System.Windows.Forms.Panel()
        Me.lbl_ShowLabStatus = New System.Windows.Forms.Label()
        Me.lbl_LabStatus = New System.Windows.Forms.Label()
        Me.Pnl_PatPhone = New System.Windows.Forms.Panel()
        Me.lb_PatPhone = New System.Windows.Forms.Label()
        Me.Label56 = New System.Windows.Forms.Label()
        Me.Pnl_PCPComPhone = New System.Windows.Forms.Panel()
        Me.lbl_ShowPCPCpmPhone = New System.Windows.Forms.Label()
        Me.lbl_PCPComPhone = New System.Windows.Forms.Label()
        Me.Pnl_PCPPracPhone = New System.Windows.Forms.Panel()
        Me.lbl_ShowPCPPracPhone = New System.Windows.Forms.Label()
        Me.lbl_PCPPracPhone = New System.Windows.Forms.Label()
        Me.pnl_TertiaryInsurance = New System.Windows.Forms.Panel()
        Me.lbl_TertiaryInsurance = New System.Windows.Forms.Label()
        Me.Label90 = New System.Windows.Forms.Label()
        Me.pnl_SecondaryInsurance = New System.Windows.Forms.Panel()
        Me.lblSecondaryInsurance = New System.Windows.Forms.Label()
        Me.Label91 = New System.Windows.Forms.Label()
        Me.pnl_PrimaryInsurance = New System.Windows.Forms.Panel()
        Me.lblPrimaryInsurance = New System.Windows.Forms.Label()
        Me.Label92 = New System.Windows.Forms.Label()
        Me.Pnl_PCPBusPhone = New System.Windows.Forms.Panel()
        Me.lbl_ShowPCPbusPhone = New System.Windows.Forms.Label()
        Me.lbl_PCPbusPhone = New System.Windows.Forms.Label()
        Me.Pnl_Status = New System.Windows.Forms.Panel()
        Me.lbl_ShowStatus = New System.Windows.Forms.Label()
        Me.lbl_Patientstatus = New System.Windows.Forms.Label()
        Me.Pnl_PCP = New System.Windows.Forms.Panel()
        Me.lblPD_PriCarePhysician = New System.Windows.Forms.Label()
        Me.Label60 = New System.Windows.Forms.Label()
        Me.Pnl_Pharmacy = New System.Windows.Forms.Panel()
        Me.lblPD_Pharmacy = New System.Windows.Forms.Label()
        Me.Label39 = New System.Windows.Forms.Label()
        Me.Pnl_PharmacyAddress = New System.Windows.Forms.Panel()
        Me.lblPD_PharmacyAddress = New System.Windows.Forms.Label()
        Me.lblPD_PharmacyAddr = New System.Windows.Forms.Label()
        Me.Pnl_PharmacyPhone = New System.Windows.Forms.Panel()
        Me.lblPD_PharmacyPhone = New System.Windows.Forms.Label()
        Me.Label61 = New System.Windows.Forms.Label()
        Me.pnl_Provider = New System.Windows.Forms.Panel()
        Me.lblPD_Provider = New System.Windows.Forms.Label()
        Me.Label93 = New System.Windows.Forms.Label()
        Me.Pnl_EmMobile = New System.Windows.Forms.Panel()
        Me.lbl_EmMobile = New System.Windows.Forms.Label()
        Me.Label57 = New System.Windows.Forms.Label()
        Me.Pnl_EmPhone = New System.Windows.Forms.Panel()
        Me.lbl_EmPhone = New System.Windows.Forms.Label()
        Me.Label47 = New System.Windows.Forms.Label()
        Me.Pnl_EmContact = New System.Windows.Forms.Panel()
        Me.lbl_Emcontact = New System.Windows.Forms.Label()
        Me.Label40 = New System.Windows.Forms.Label()
        Me.Pnl_Email = New System.Windows.Forms.Panel()
        Me.lblPD_Email = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Pnl_Fax = New System.Windows.Forms.Panel()
        Me.lblPD_Fax = New System.Windows.Forms.Label()
        Me.label23 = New System.Windows.Forms.Label()
        Me.Pnl_Home = New System.Windows.Forms.Panel()
        Me.lblPD_HomePhone = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Pnl_Mobile = New System.Windows.Forms.Panel()
        Me.lblPD_Mobile = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Pnl_Gender = New System.Windows.Forms.Panel()
        Me.lblPD_Gender = New System.Windows.Forms.Label()
        Me.Label46 = New System.Windows.Forms.Label()
        Me.pnlAddress = New System.Windows.Forms.Panel()
        Me.lblPD_Address = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.pnlLeft = New System.Windows.Forms.Panel()
        Me.picConfidentialInfo = New System.Windows.Forms.PictureBox()
        Me.pnlReconciliationAlert = New System.Windows.Forms.Panel()
        Me.PicReconciliationAlert = New System.Windows.Forms.PictureBox()
        Me.pnlPatientSavings = New System.Windows.Forms.Panel()
        Me.lblPatientSavings = New System.Windows.Forms.Label()
        Me.picPatientSavings = New System.Windows.Forms.PictureBox()
        Me.lblBadDebt = New System.Windows.Forms.Label()
        Me.lnklblAmendmentsAlert = New System.Windows.Forms.LinkLabel()
        Me.lblPD_Age = New System.Windows.Forms.Label()
        Me.lblPD_DateOfBirth = New System.Windows.Forms.Label()
        Me.lblPD_Name = New System.Windows.Forms.Label()
        Me.lblPD_Code = New System.Windows.Forms.Label()
        Me.pnlPatientPhoto = New System.Windows.Forms.Panel()
        Me.pnlPhotoBorder = New System.Windows.Forms.Panel()
        Me.picPD_Photo = New System.Windows.Forms.PictureBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.pnlPatientCard = New Janus.Windows.UI.Dock.UIPanel()
        Me.pnlPatientCardContainer = New Janus.Windows.UI.Dock.UIPanelInnerContainer()
        Me.pnlMainPatientCardButton = New System.Windows.Forms.Panel()
        Me.pnlPatientCardButtonContainer = New System.Windows.Forms.Panel()
        Me.Panel8 = New System.Windows.Forms.Panel()
        Me.picPC_Cards = New System.Windows.Forms.PictureBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label41 = New System.Windows.Forms.Label()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.lblScandate = New System.Windows.Forms.Label()
        Me.Label63 = New System.Windows.Forms.Label()
        Me.pnlPatientCardsButton = New System.Windows.Forms.Panel()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.label20 = New System.Windows.Forms.Label()
        Me.label21 = New System.Windows.Forms.Label()
        Me.label24 = New System.Windows.Forms.Label()
        Me.label22 = New System.Windows.Forms.Label()
        Me.Label55 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label71 = New System.Windows.Forms.Label()
        Me.Label72 = New System.Windows.Forms.Label()
        Me.Label73 = New System.Windows.Forms.Label()
        Me.Label74 = New System.Windows.Forms.Label()
        Me.pnlYESLAB = New System.Windows.Forms.Panel()
        Me.PictureBox3 = New System.Windows.Forms.PictureBox()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.btnYesClose = New System.Windows.Forms.Button()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.pnlJPatientStatus = New Janus.Windows.UI.Dock.UIPanel()
        Me.pnlJPatientStatusContainer = New Janus.Windows.UI.Dock.UIPanelInnerContainer()
        Me.pnlPatientStatus = New System.Windows.Forms.Panel()
        Me.pnlPAtientStaturEnvironment = New System.Windows.Forms.Panel()
        Me.pnlPatientStatusGrid = New System.Windows.Forms.Panel()
        Me.c1PatientStatus = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.Label58 = New System.Windows.Forms.Label()
        Me.pnlPatientStatusColor = New System.Windows.Forms.Panel()
        Me.lblENV_05 = New System.Windows.Forms.Label()
        Me.lblENV_03 = New System.Windows.Forms.Label()
        Me.lblENV_02 = New System.Windows.Forms.Label()
        Me.lblENV_04 = New System.Windows.Forms.Label()
        Me.lblENV_06 = New System.Windows.Forms.Label()
        Me.lblENV_01 = New System.Windows.Forms.Label()
        Me.Label42 = New System.Windows.Forms.Label()
        Me.pnlPatientAlert = New System.Windows.Forms.Panel()
        Me.txtPatientAlert = New System.Windows.Forms.TextBox()
        Me.Label43 = New System.Windows.Forms.Label()
        Me.pnlPatientAlertHeader = New System.Windows.Forms.Panel()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label45 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.uiPanel0 = New Janus.Windows.UI.Dock.UIPanel()
        Me.uiPanel0Container = New Janus.Windows.UI.Dock.UIPanelInnerContainer()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.pnlEligibilityCheck = New System.Windows.Forms.Panel()
        Me.c1EligibilityCheck = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.pnlUnappliedCopay = New System.Windows.Forms.Panel()
        Me.c1UnappliedCopayAlert = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.pnlCopayAlert = New System.Windows.Forms.Panel()
        Me.c1CopayAlert = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.c1Mails = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.pnlPatientDemo = New Janus.Windows.UI.Dock.UIPanel()
        Me.pnlPatientDemoContainer = New Janus.Windows.UI.Dock.UIPanelInnerContainer()
        Me.pnlPatientCards = New Janus.Windows.UI.Dock.UIPanel()
        Me.pnlPatientCardsContainer = New Janus.Windows.UI.Dock.UIPanelInnerContainer()
        Me.oPatientListControl = New gloPatient.PatientListControl()
        Me.pnlPatientList = New System.Windows.Forms.Panel()
        Me.pnlPatientListHeader = New System.Windows.Forms.Panel()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.Label59 = New System.Windows.Forms.Label()
        Me.lblPatientListHeader = New System.Windows.Forms.Label()
        Me.cmnuProblemList = New System.Windows.Forms.ContextMenu()
        Me.mnuAddProblemList = New System.Windows.Forms.MenuItem()
        Me.ChartPull = New System.Windows.Forms.ContextMenu()
        Me.cmnuShortCut = New System.Windows.Forms.ContextMenu()
        Me.cmnuTasks = New System.Windows.Forms.ContextMenu()
        Me.cmnuTask_Delete = New System.Windows.Forms.MenuItem()
        Me.cmnuTask_Complete = New System.Windows.Forms.MenuItem()
        Me.cmnuTask_Add = New System.Windows.Forms.MenuItem()
        Me.cmnuTask_AcceptTask = New System.Windows.Forms.MenuItem()
        Me.FileSystemWatcher1 = New System.IO.FileSystemWatcher()
        Me.fsw_RecieveFAX = New System.IO.FileSystemWatcher()
        Me.timerLockScreen = New System.Windows.Forms.Timer(Me.components)
        Me.tmrBirthDayReminder = New System.Windows.Forms.Timer(Me.components)
        Me.tmrshowsurepnl = New System.Windows.Forms.Timer(Me.components)
        Me.tmrsurescriptAlert = New System.Windows.Forms.Timer(Me.components)
        Me.ToolTip2 = New System.Windows.Forms.ToolTip(Me.components)
        Me.tmrMessages = New System.Windows.Forms.Timer(Me.components)
        Me.pnlBirthDayReminder = New System.Windows.Forms.Panel()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.picBirthReminderClose = New System.Windows.Forms.PictureBox()
        Me.lblBirthDayMessage = New System.Windows.Forms.Label()
        Me.pnlSurescriptAlert = New System.Windows.Forms.Panel()
        Me.pnlsright = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Label52 = New System.Windows.Forms.Label()
        Me.Label53 = New System.Windows.Forms.Label()
        Me.C1AlertMessages = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.Panel18 = New System.Windows.Forms.Panel()
        Me.Panel19 = New System.Windows.Forms.Panel()
        Me.Label51 = New System.Windows.Forms.Label()
        Me.pnlsTop = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.btnSRefresh = New System.Windows.Forms.Button()
        Me.btnSurescriptClose = New System.Windows.Forms.Button()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.Label48 = New System.Windows.Forms.Label()
        Me.Label50 = New System.Windows.Forms.Label()
        Me.Label49 = New System.Windows.Forms.Label()
        Me.cMnuPatient = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.pnlTAsk_TaskSentToMe = New Janus.Windows.UI.Dock.UIPanel()
        Me.pnlTAsk_TaskSentToMeContainer = New Janus.Windows.UI.Dock.UIPanelInnerContainer()
        Me.pnlTask_TaskSentByMe = New Janus.Windows.UI.Dock.UIPanel()
        Me.pnlTask_TaskSentByMeContainer = New Janus.Windows.UI.Dock.UIPanelInnerContainer()
        Me.pnlTask_MyTasks = New Janus.Windows.UI.Dock.UIPanel()
        Me.pnlTask_MyTasksContainer = New Janus.Windows.UI.Dock.UIPanelInnerContainer()
        Me.cmnuToolStripCustomize = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.CustomizeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuPatientDetails = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.AddTriageToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.imgList_Common = New System.Windows.Forms.ImageList(Me.components)
        Me.cmnu_Tasks = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.cmnuItem_Priority = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuItem_NewTask = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuItem_AcceptTask = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuItem_RejectTask = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuItem_CompleteTask = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuItem_CompleteAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmunItem_Completed = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuItem_0Percent = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuItem_25Percent = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuItem_50Percent = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuItem_75Percent = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuItem_100Percent = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuItem_TaskSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.cmnuItem_TrackTasks = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuItem_TaskTake = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuItem_DeleteTask = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuItem_FollowUpTask = New System.Windows.Forms.ToolStripMenuItem()
        Me.C1SuperTooltip1 = New C1.Win.C1SuperTooltip.C1SuperTooltip(Me.components)
        Me.Imgts_PatientDetails = New System.Windows.Forms.ImageList(Me.components)
        Me.cmnu_messages = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ToolStripSeparator10 = New System.Windows.Forms.ToolStripSeparator()
        Me.tmrCopayAlert = New System.Windows.Forms.Timer(Me.components)
        Me.pnlFormularyTransactionMessage = New System.Windows.Forms.Panel()
        Me.lblFormularyTransactionMessage = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.PrintDialog1 = New System.Windows.Forms.PrintDialog()
        Me.printDoc = New System.Drawing.Printing.PrintDocument()
        Me.CmnuPatientstatus = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.BackgroundWorker_MessageTimer = New System.ComponentModel.BackgroundWorker()
        Me.BackgroundWorker_InitiliseFaxSetting = New System.ComponentModel.BackgroundWorker()
        Me.HelpComponent1 = New gloEMR.Help.HelpComponent(Me.components)
        Me.pnlWait = New System.Windows.Forms.Panel()
        Me.Label84 = New System.Windows.Forms.Label()
        Me.Label85 = New System.Windows.Forms.Label()
        Me.notifyInbox = New System.Windows.Forms.NotifyIcon(Me.components)
        Label82 = New System.Windows.Forms.Label()
        Label29 = New System.Windows.Forms.Label()
        CType(Me.picLockScreen,System.ComponentModel.ISupportInitialize).BeginInit
        Me.pnlMainToolBar.SuspendLayout
        Me.tlbStripMain.SuspendLayout
        Me.pnlMainMenu.SuspendLayout
        Me.MenuStrip1.SuspendLayout
        Me.pnlMainStatusBar.SuspendLayout
        CType(Me.Vcmd,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.DgnEngineControl1,System.ComponentModel.ISupportInitialize).BeginInit
        Me.StatusStrip.SuspendLayout
        CType(Me.DgnMicBtn1,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.uiPanelManager1,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.pnlPatientCheckInStatus,System.ComponentModel.ISupportInitialize).BeginInit
        Me.pnlPatientCheckInStatus.SuspendLayout
        Me.pnlPatientCheckInStatusContainer.SuspendLayout
        CType(Me.c1PatientCheckInStatus,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.pnlLeft_Nav,System.ComponentModel.ISupportInitialize).BeginInit
        Me.pnlLeft_Nav.SuspendLayout
        CType(Me.pnl_Messages,System.ComponentModel.ISupportInitialize).BeginInit
        Me.pnl_Messages.SuspendLayout
        Me.pnl_MessagesContainer.SuspendLayout
        CType(Me.C1Mesages,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.pnlTasks,System.ComponentModel.ISupportInitialize).BeginInit
        Me.pnlTasks.SuspendLayout
        Me.pnlTasksContainer.SuspendLayout
        Me.Panel12.SuspendLayout
        CType(Me._flexGroup, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me._flexGroup.Grid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel9.SuspendLayout
        Me.Panel10.SuspendLayout
        CType(Me.pictureBox2,System.ComponentModel.ISupportInitialize).BeginInit
        Me.pnlViewMoreTask.SuspendLayout
        CType(Me.C1UserTasks,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.pnlNav_UnfinishedExams,System.ComponentModel.ISupportInitialize).BeginInit
        Me.pnlNav_UnfinishedExams.SuspendLayout
        Me.pnlNav_UnfinishedExams_Container.SuspendLayout
        CType(Me.C1UnfinishedExam,System.ComponentModel.ISupportInitialize).BeginInit
        Me.pnllblLinkUnfinishedexam.SuspendLayout
        CType(Me.pnlNavigator,System.ComponentModel.ISupportInitialize).BeginInit
        Me.pnlNavigator.SuspendLayout
        CType(Me.pnlNav_Appointments,System.ComponentModel.ISupportInitialize).BeginInit
        Me.pnlNav_Appointments.SuspendLayout
        Me.pnlNav_AppointmentsContainer.SuspendLayout
        CType(Me.C1Appointments,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.pnlNav_Calendar,System.ComponentModel.ISupportInitialize).BeginInit
        Me.pnlNav_Calendar.SuspendLayout
        CType(Me.pnlNav_Myday,System.ComponentModel.ISupportInitialize).BeginInit
        Me.pnlNav_Myday.SuspendLayout
        CType(Me.pnlNav_Triage,System.ComponentModel.ISupportInitialize).BeginInit
        Me.pnlNav_Triage.SuspendLayout
        Me.pnlNav_TriageContainer.SuspendLayout
        CType(Me.C1Triage,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.pnlPatientDetail,System.ComponentModel.ISupportInitialize).BeginInit
        Me.pnlPatientDetail.SuspendLayout
        Me.pnlPatientDetailContainer.SuspendLayout
        Me.pnlPatientDetails.SuspendLayout
        CType(Me.C1dgPatientDetails,System.ComponentModel.ISupportInitialize).BeginInit
        Me.pnlArchiveInfo.SuspendLayout
        Me.pnlSearchFilter.SuspendLayout
        Me.pnlCancelRx.SuspendLayout
        Me.pnlCase.SuspendLayout
        Me.pnlSpeciality.SuspendLayout
        Me.Panel13.SuspendLayout
        Me.Panel5.SuspendLayout
        Me.Panel14.SuspendLayout
        Me.Panel16.SuspendLayout
        Me.pnlPatientDetailsHeaders.SuspendLayout
        Me.ts_PatientDetails.SuspendLayout
        CType(Me.pnlTask,System.ComponentModel.ISupportInitialize).BeginInit
        Me.pnlTask.SuspendLayout
        CType(Me.pnlTasks_MYTask,System.ComponentModel.ISupportInitialize).BeginInit
        Me.pnlTasks_MYTask.SuspendLayout
        CType(Me.pnlTask_TaskRequests,System.ComponentModel.ISupportInitialize).BeginInit
        Me.pnlTask_TaskRequests.SuspendLayout
        CType(Me.pnlTask_RequestsSent,System.ComponentModel.ISupportInitialize).BeginInit
        Me.pnlTask_RequestsSent.SuspendLayout
        CType(Me.pnlGrPatientDemoCardsStatus,System.ComponentModel.ISupportInitialize).BeginInit
        Me.pnlGrPatientDemoCardsStatus.SuspendLayout
        CType(Me.pnlPatientDemographics,System.ComponentModel.ISupportInitialize).BeginInit
        Me.pnlPatientDemographics.SuspendLayout
        Me.pnlPatientDemographicsContainer.SuspendLayout
        Me.pnlRightFill.SuspendLayout
        Me.pnl_workphone.SuspendLayout
        Me.pnl_Businesscenter.SuspendLayout
        Me.pnl_Occupation.SuspendLayout
        Me.pnl_MedicalCategory.SuspendLayout
        Me.pnl_Language.SuspendLayout
        Me.pnl_Ethinicity.SuspendLayout
        Me.pnl_Race.SuspendLayout
        Me.pnl_LabStatus.SuspendLayout
        Me.Pnl_PatPhone.SuspendLayout
        Me.Pnl_PCPComPhone.SuspendLayout
        Me.Pnl_PCPPracPhone.SuspendLayout
        Me.pnl_TertiaryInsurance.SuspendLayout
        Me.pnl_SecondaryInsurance.SuspendLayout
        Me.pnl_PrimaryInsurance.SuspendLayout
        Me.Pnl_PCPBusPhone.SuspendLayout
        Me.Pnl_Status.SuspendLayout
        Me.Pnl_PCP.SuspendLayout
        Me.Pnl_Pharmacy.SuspendLayout
        Me.Pnl_PharmacyAddress.SuspendLayout
        Me.Pnl_PharmacyPhone.SuspendLayout
        Me.pnl_Provider.SuspendLayout
        Me.Pnl_EmMobile.SuspendLayout
        Me.Pnl_EmPhone.SuspendLayout
        Me.Pnl_EmContact.SuspendLayout
        Me.Pnl_Email.SuspendLayout
        Me.Pnl_Fax.SuspendLayout
        Me.Pnl_Home.SuspendLayout
        Me.Pnl_Mobile.SuspendLayout
        Me.Pnl_Gender.SuspendLayout
        Me.pnlAddress.SuspendLayout
        Me.pnlLeft.SuspendLayout
        CType(Me.picConfidentialInfo,System.ComponentModel.ISupportInitialize).BeginInit
        Me.pnlReconciliationAlert.SuspendLayout
        CType(Me.PicReconciliationAlert,System.ComponentModel.ISupportInitialize).BeginInit
        Me.pnlPatientSavings.SuspendLayout
        CType(Me.picPatientSavings,System.ComponentModel.ISupportInitialize).BeginInit
        Me.pnlPatientPhoto.SuspendLayout
        Me.pnlPhotoBorder.SuspendLayout
        CType(Me.picPD_Photo,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.pnlPatientCard,System.ComponentModel.ISupportInitialize).BeginInit
        Me.pnlPatientCard.SuspendLayout
        Me.pnlPatientCardContainer.SuspendLayout
        Me.pnlMainPatientCardButton.SuspendLayout
        Me.pnlPatientCardButtonContainer.SuspendLayout
        Me.Panel8.SuspendLayout
        CType(Me.picPC_Cards,System.ComponentModel.ISupportInitialize).BeginInit
        Me.Panel7.SuspendLayout
        Me.pnlPatientCardsButton.SuspendLayout
        Me.pnlYESLAB.SuspendLayout
        CType(Me.PictureBox3,System.ComponentModel.ISupportInitialize).BeginInit
        Me.Panel6.SuspendLayout
        CType(Me.pnlJPatientStatus,System.ComponentModel.ISupportInitialize).BeginInit
        Me.pnlJPatientStatus.SuspendLayout
        Me.pnlJPatientStatusContainer.SuspendLayout
        Me.pnlPatientStatus.SuspendLayout
        Me.pnlPAtientStaturEnvironment.SuspendLayout
        Me.pnlPatientStatusGrid.SuspendLayout
        CType(Me.c1PatientStatus,System.ComponentModel.ISupportInitialize).BeginInit
        Me.pnlPatientStatusColor.SuspendLayout
        Me.pnlPatientAlert.SuspendLayout
        Me.pnlPatientAlertHeader.SuspendLayout
        CType(Me.uiPanel0,System.ComponentModel.ISupportInitialize).BeginInit
        Me.uiPanel0.SuspendLayout
        Me.Panel4.SuspendLayout
        Me.pnlEligibilityCheck.SuspendLayout
        CType(Me.c1EligibilityCheck,System.ComponentModel.ISupportInitialize).BeginInit
        Me.pnlUnappliedCopay.SuspendLayout
        CType(Me.c1UnappliedCopayAlert,System.ComponentModel.ISupportInitialize).BeginInit
        Me.pnlCopayAlert.SuspendLayout
        CType(Me.c1CopayAlert,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.c1Mails,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.pnlPatientDemo,System.ComponentModel.ISupportInitialize).BeginInit
        Me.pnlPatientDemo.SuspendLayout
        CType(Me.pnlPatientCards,System.ComponentModel.ISupportInitialize).BeginInit
        Me.pnlPatientCards.SuspendLayout
        Me.pnlPatientList.SuspendLayout
        Me.pnlPatientListHeader.SuspendLayout
        CType(Me.FileSystemWatcher1,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.fsw_RecieveFAX,System.ComponentModel.ISupportInitialize).BeginInit
        Me.pnlBirthDayReminder.SuspendLayout
        CType(Me.picBirthReminderClose,System.ComponentModel.ISupportInitialize).BeginInit
        Me.pnlSurescriptAlert.SuspendLayout
        Me.pnlsright.SuspendLayout
        Me.Panel3.SuspendLayout
        CType(Me.C1AlertMessages,System.ComponentModel.ISupportInitialize).BeginInit
        Me.Panel18.SuspendLayout
        Me.pnlsTop.SuspendLayout
        Me.Panel2.SuspendLayout
        CType(Me.pnlTAsk_TaskSentToMe,System.ComponentModel.ISupportInitialize).BeginInit
        Me.pnlTAsk_TaskSentToMe.SuspendLayout
        CType(Me.pnlTask_TaskSentByMe,System.ComponentModel.ISupportInitialize).BeginInit
        Me.pnlTask_TaskSentByMe.SuspendLayout
        CType(Me.pnlTask_MyTasks,System.ComponentModel.ISupportInitialize).BeginInit
        Me.pnlTask_MyTasks.SuspendLayout
        Me.cmnuToolStripCustomize.SuspendLayout
        Me.cmnuPatientDetails.SuspendLayout
        Me.cmnu_Tasks.SuspendLayout
        Me.cmnu_messages.SuspendLayout
        Me.pnlFormularyTransactionMessage.SuspendLayout
        Me.pnlWait.SuspendLayout
        Me.SuspendLayout
        '
        'Label82
        '
        Label82.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Label82.Dock = System.Windows.Forms.DockStyle.Top
        Label82.Location = New System.Drawing.Point(0, 0)
        Label82.Name = "Label82"
        Label82.Size = New System.Drawing.Size(272, 1)
        Label82.TabIndex = 1
        '
        'Label29
        '
        Label29.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Label29.Dock = System.Windows.Forms.DockStyle.Top
        Label29.Location = New System.Drawing.Point(0, 0)
        Label29.Name = "Label29"
        Label29.Size = New System.Drawing.Size(272, 1)
        Label29.TabIndex = 1
        '
        'btnModifyPatientAlert
        '
        Me.btnModifyPatientAlert.AutoEllipsis = true
        Me.btnModifyPatientAlert.BackColor = System.Drawing.Color.Transparent
        Me.btnModifyPatientAlert.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnModifyPatientAlert.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnModifyPatientAlert.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(64,Byte),Integer), CType(CType(114,Byte),Integer), CType(CType(176,Byte),Integer))
        Me.btnModifyPatientAlert.FlatAppearance.BorderSize = 0
        Me.btnModifyPatientAlert.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnModifyPatientAlert.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnModifyPatientAlert.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnModifyPatientAlert.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btnModifyPatientAlert.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.btnModifyPatientAlert.Image = CType(resources.GetObject("btnModifyPatientAlert.Image"),System.Drawing.Image)
        Me.btnModifyPatientAlert.Location = New System.Drawing.Point(208, 0)
        Me.btnModifyPatientAlert.Name = "btnModifyPatientAlert"
        Me.btnModifyPatientAlert.Size = New System.Drawing.Size(22, 21)
        Me.btnModifyPatientAlert.TabIndex = 111
        Me.ToolTip.SetToolTip(Me.btnModifyPatientAlert, "Modify Patient Alert")
        Me.btnModifyPatientAlert.UseVisualStyleBackColor = false
        '
        'btnClosePatientAlert
        '
        Me.btnClosePatientAlert.AutoEllipsis = true
        Me.btnClosePatientAlert.BackColor = System.Drawing.Color.Transparent
        Me.btnClosePatientAlert.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClosePatientAlert.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnClosePatientAlert.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(64,Byte),Integer), CType(CType(114,Byte),Integer), CType(CType(176,Byte),Integer))
        Me.btnClosePatientAlert.FlatAppearance.BorderSize = 0
        Me.btnClosePatientAlert.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnClosePatientAlert.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnClosePatientAlert.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClosePatientAlert.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btnClosePatientAlert.ForeColor = System.Drawing.Color.Transparent
        Me.btnClosePatientAlert.Image = CType(resources.GetObject("btnClosePatientAlert.Image"),System.Drawing.Image)
        Me.btnClosePatientAlert.Location = New System.Drawing.Point(230, 0)
        Me.btnClosePatientAlert.Name = "btnClosePatientAlert"
        Me.btnClosePatientAlert.Size = New System.Drawing.Size(22, 21)
        Me.btnClosePatientAlert.TabIndex = 110
        Me.ToolTip.SetToolTip(Me.btnClosePatientAlert, "Close Patient Alert")
        Me.btnClosePatientAlert.UseVisualStyleBackColor = false
        '
        'btnPC_MoveFirst
        '
        Me.btnPC_MoveFirst.BackColor = System.Drawing.Color.Transparent
        Me.btnPC_MoveFirst.BackgroundImage = CType(resources.GetObject("btnPC_MoveFirst.BackgroundImage"),System.Drawing.Image)
        Me.btnPC_MoveFirst.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnPC_MoveFirst.Dock = System.Windows.Forms.DockStyle.Top
        Me.btnPC_MoveFirst.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPC_MoveFirst.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btnPC_MoveFirst.Location = New System.Drawing.Point(0, 182)
        Me.btnPC_MoveFirst.Name = "btnPC_MoveFirst"
        Me.btnPC_MoveFirst.Size = New System.Drawing.Size(34, 23)
        Me.btnPC_MoveFirst.TabIndex = 107
        Me.btnPC_MoveFirst.Text = "|<"
        Me.ToolTip.SetToolTip(Me.btnPC_MoveFirst, "Move First")
        Me.btnPC_MoveFirst.UseVisualStyleBackColor = false
        '
        'btnPC_MovePrevious
        '
        Me.btnPC_MovePrevious.BackColor = System.Drawing.Color.Transparent
        Me.btnPC_MovePrevious.BackgroundImage = CType(resources.GetObject("btnPC_MovePrevious.BackgroundImage"),System.Drawing.Image)
        Me.btnPC_MovePrevious.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnPC_MovePrevious.Dock = System.Windows.Forms.DockStyle.Top
        Me.btnPC_MovePrevious.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPC_MovePrevious.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btnPC_MovePrevious.Location = New System.Drawing.Point(0, 154)
        Me.btnPC_MovePrevious.Name = "btnPC_MovePrevious"
        Me.btnPC_MovePrevious.Size = New System.Drawing.Size(34, 23)
        Me.btnPC_MovePrevious.TabIndex = 109
        Me.btnPC_MovePrevious.Text = "<"
        Me.ToolTip.SetToolTip(Me.btnPC_MovePrevious, "Move Previous")
        Me.btnPC_MovePrevious.UseVisualStyleBackColor = false
        '
        'btnPC_MoveNext
        '
        Me.btnPC_MoveNext.BackColor = System.Drawing.Color.Transparent
        Me.btnPC_MoveNext.BackgroundImage = CType(resources.GetObject("btnPC_MoveNext.BackgroundImage"),System.Drawing.Image)
        Me.btnPC_MoveNext.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnPC_MoveNext.Dock = System.Windows.Forms.DockStyle.Top
        Me.btnPC_MoveNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPC_MoveNext.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btnPC_MoveNext.Location = New System.Drawing.Point(0, 126)
        Me.btnPC_MoveNext.Name = "btnPC_MoveNext"
        Me.btnPC_MoveNext.Size = New System.Drawing.Size(34, 23)
        Me.btnPC_MoveNext.TabIndex = 111
        Me.btnPC_MoveNext.Text = ">"
        Me.ToolTip.SetToolTip(Me.btnPC_MoveNext, "Move Next")
        Me.btnPC_MoveNext.UseVisualStyleBackColor = false
        '
        'btnPC_MoveLast
        '
        Me.btnPC_MoveLast.BackColor = System.Drawing.Color.Transparent
        Me.btnPC_MoveLast.BackgroundImage = CType(resources.GetObject("btnPC_MoveLast.BackgroundImage"),System.Drawing.Image)
        Me.btnPC_MoveLast.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnPC_MoveLast.Dock = System.Windows.Forms.DockStyle.Top
        Me.btnPC_MoveLast.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPC_MoveLast.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btnPC_MoveLast.Location = New System.Drawing.Point(0, 98)
        Me.btnPC_MoveLast.Name = "btnPC_MoveLast"
        Me.btnPC_MoveLast.Size = New System.Drawing.Size(34, 23)
        Me.btnPC_MoveLast.TabIndex = 113
        Me.btnPC_MoveLast.Text = ">|"
        Me.ToolTip.SetToolTip(Me.btnPC_MoveLast, "Move Last")
        Me.btnPC_MoveLast.UseVisualStyleBackColor = false
        '
        'btnPC_PrintCards
        '
        Me.btnPC_PrintCards.BackColor = System.Drawing.Color.Transparent
        Me.btnPC_PrintCards.BackgroundImage = CType(resources.GetObject("btnPC_PrintCards.BackgroundImage"),System.Drawing.Image)
        Me.btnPC_PrintCards.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnPC_PrintCards.Dock = System.Windows.Forms.DockStyle.Top
        Me.btnPC_PrintCards.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPC_PrintCards.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btnPC_PrintCards.Image = CType(resources.GetObject("btnPC_PrintCards.Image"),System.Drawing.Image)
        Me.btnPC_PrintCards.Location = New System.Drawing.Point(0, 70)
        Me.btnPC_PrintCards.Name = "btnPC_PrintCards"
        Me.btnPC_PrintCards.Size = New System.Drawing.Size(34, 23)
        Me.btnPC_PrintCards.TabIndex = 114
        Me.ToolTip.SetToolTip(Me.btnPC_PrintCards, "Print Scan Card")
        Me.btnPC_PrintCards.UseVisualStyleBackColor = false
        '
        'btnPC_DeleteCard
        '
        Me.btnPC_DeleteCard.BackColor = System.Drawing.Color.Transparent
        Me.btnPC_DeleteCard.BackgroundImage = CType(resources.GetObject("btnPC_DeleteCard.BackgroundImage"),System.Drawing.Image)
        Me.btnPC_DeleteCard.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnPC_DeleteCard.Dock = System.Windows.Forms.DockStyle.Top
        Me.btnPC_DeleteCard.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPC_DeleteCard.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btnPC_DeleteCard.Image = CType(resources.GetObject("btnPC_DeleteCard.Image"),System.Drawing.Image)
        Me.btnPC_DeleteCard.Location = New System.Drawing.Point(0, 42)
        Me.btnPC_DeleteCard.Name = "btnPC_DeleteCard"
        Me.btnPC_DeleteCard.Size = New System.Drawing.Size(34, 23)
        Me.btnPC_DeleteCard.TabIndex = 116
        Me.ToolTip.SetToolTip(Me.btnPC_DeleteCard, "Delete Scan Card")
        Me.btnPC_DeleteCard.UseVisualStyleBackColor = false
        '
        'btnPC_ScanCard
        '
        Me.btnPC_ScanCard.BackColor = System.Drawing.Color.Transparent
        Me.btnPC_ScanCard.BackgroundImage = CType(resources.GetObject("btnPC_ScanCard.BackgroundImage"),System.Drawing.Image)
        Me.btnPC_ScanCard.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnPC_ScanCard.Dock = System.Windows.Forms.DockStyle.Top
        Me.btnPC_ScanCard.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.btnPC_ScanCard.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPC_ScanCard.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btnPC_ScanCard.Image = CType(resources.GetObject("btnPC_ScanCard.Image"),System.Drawing.Image)
        Me.btnPC_ScanCard.Location = New System.Drawing.Point(0, 12)
        Me.btnPC_ScanCard.Name = "btnPC_ScanCard"
        Me.btnPC_ScanCard.Size = New System.Drawing.Size(34, 25)
        Me.btnPC_ScanCard.TabIndex = 120
        Me.ToolTip.SetToolTip(Me.btnPC_ScanCard, "Scan Card")
        Me.btnPC_ScanCard.UseVisualStyleBackColor = false
        '
        'btnReset
        '
        Me.btnReset.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnReset.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnReset.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.btnReset.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnReset.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnReset.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.btnReset.Image = CType(resources.GetObject("btnReset.Image"),System.Drawing.Image)
        Me.btnReset.Location = New System.Drawing.Point(929, 2)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(22, 22)
        Me.btnReset.TabIndex = 15
        Me.ToolTip.SetToolTip(Me.btnReset, "Reset")
        Me.btnReset.UseVisualStyleBackColor = true
        Me.btnReset.Visible = false
        '
        'btnIntuitMsg
        '
        Me.btnIntuitMsg.FlatAppearance.BorderSize = 0
        Me.btnIntuitMsg.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnIntuitMsg.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnIntuitMsg.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnIntuitMsg.Image = CType(resources.GetObject("btnIntuitMsg.Image"),System.Drawing.Image)
        Me.btnIntuitMsg.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnIntuitMsg.Location = New System.Drawing.Point(122, 8)
        Me.btnIntuitMsg.Name = "btnIntuitMsg"
        Me.btnIntuitMsg.Size = New System.Drawing.Size(38, 23)
        Me.btnIntuitMsg.TabIndex = 146
        Me.ToolTip.SetToolTip(Me.btnIntuitMsg, "Send Patient Portal Message")
        Me.btnIntuitMsg.UseVisualStyleBackColor = true
        '
        'picLockScreen
        '
        Me.picLockScreen.BackColor = System.Drawing.Color.Transparent
        Me.picLockScreen.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.picLockScreen.Dock = System.Windows.Forms.DockStyle.Right
        Me.picLockScreen.Image = CType(resources.GetObject("picLockScreen.Image"),System.Drawing.Image)
        Me.picLockScreen.Location = New System.Drawing.Point(1222, 0)
        Me.picLockScreen.Name = "picLockScreen"
        Me.picLockScreen.Size = New System.Drawing.Size(43, 61)
        Me.picLockScreen.TabIndex = 51
        Me.picLockScreen.TabStop = false
        Me.C1SuperTooltip1.SetToolTip(Me.picLockScreen, "Lock Screen")
        '
        'pnlMainToolBar
        '
        Me.pnlMainToolBar.BackColor = System.Drawing.Color.FromArgb(CType(CType(207,Byte),Integer), CType(CType(227,Byte),Integer), CType(CType(255,Byte),Integer))
        Me.pnlMainToolBar.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.pnlMainToolBar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlMainToolBar.Controls.Add(Me.tlbStripMain)
        Me.pnlMainToolBar.Controls.Add(Me.picLockScreen)
        Me.pnlMainToolBar.Controls.Add(Me.Label3)
        Me.pnlMainToolBar.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlMainToolBar.Location = New System.Drawing.Point(0, 28)
        Me.pnlMainToolBar.Name = "pnlMainToolBar"
        Me.pnlMainToolBar.Size = New System.Drawing.Size(1265, 62)
        Me.pnlMainToolBar.TabIndex = 9
        '
        'tlbStripMain
        '
        Me.tlbStripMain.BackColor = System.Drawing.Color.Transparent
        Me.tlbStripMain.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlbStripMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlbStripMain.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.tlbStripMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.tlbStripMain.ImageScalingSize = New System.Drawing.Size(42, 42)
        Me.tlbStripMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tlbbtn_Microphone, Me.tlbbtn_NewPatient, Me.tlbbtn_ModifyPatient, Me.tlbbtn_ScanCard, Me.tlsPatRegSep, Me.tlbbtn_Vitals, Me.tlbbtn_History, Me.tlbbtn_Prescription, Me.tlbbtn_gloLabOrders, Me.tlbbtn_LabOrders, Me.tlsMainSep, Me.tlbbtn_NewExam, Me.tlbbtn_PastExam, Me.tlbbtn_UnFinishedExams, Me.tlsExamSep, Me.tlbbtn_Calender, Me.tlbbtn_Sechedule, Me.tlsCalenderSep, Me.tlbbtn_RCMDocs, Me.tlbbtn_ScanDocs, Me.tlsDOCmgntSep, Me.tlbbtn_FormGallery, Me.tlbbtn_PatientSynopsis, Me.tlsFormGalerySep, Me.tlbbtn_RxHub, Me.tlbbtn_LockScreen, Me.tlbbtn_Close, Me.tlbbtnSaveCDS, Me.tlbbtn_GenerateCDA, Me.tlbbtn_GenerateCCD, Me.tlbbtn_Timeline, Me.tlbbtn_Inbox, Me.tlbbtn_Orders})
        Me.tlbStripMain.Location = New System.Drawing.Point(0, 0)
        Me.tlbStripMain.Name = "tlbStripMain"
        Me.tlbStripMain.Size = New System.Drawing.Size(1222, 61)
        Me.tlbStripMain.TabIndex = 53
        '
        'tlbbtn_Microphone
        '
        Me.tlbbtn_Microphone.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tlbbtn_Microphone.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlbbtn_Microphone.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.tlbbtn_Microphone.Image = CType(resources.GetObject("tlbbtn_Microphone.Image"),System.Drawing.Image)
        Me.tlbbtn_Microphone.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbbtn_Microphone.Name = "tlbbtn_Microphone"
        Me.tlbbtn_Microphone.Size = New System.Drawing.Size(49, 58)
        Me.tlbbtn_Microphone.Tag = "Microphone"
        Me.tlbbtn_Microphone.Text = "Mic Off"
        Me.tlbbtn_Microphone.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlbbtn_Microphone.Visible = false
        '
        'tlbbtn_NewPatient
        '
        Me.tlbbtn_NewPatient.BackColor = System.Drawing.Color.Transparent
        Me.tlbbtn_NewPatient.BackgroundImage = CType(resources.GetObject("tlbbtn_NewPatient.BackgroundImage"),System.Drawing.Image)
        Me.tlbbtn_NewPatient.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlbbtn_NewPatient.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.tlbbtn_NewPatient.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.tlbbtn_NewPatient.Image = CType(resources.GetObject("tlbbtn_NewPatient.Image"),System.Drawing.Image)
        Me.tlbbtn_NewPatient.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbbtn_NewPatient.Name = "tlbbtn_NewPatient"
        Me.tlbbtn_NewPatient.Size = New System.Drawing.Size(56, 58)
        Me.tlbbtn_NewPatient.Tag = "New Patient"
        Me.tlbbtn_NewPatient.Text = "&New Pat"
        Me.tlbbtn_NewPatient.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlbbtn_NewPatient.ToolTipText = "Add New Patient"
        '
        'tlbbtn_ModifyPatient
        '
        Me.tlbbtn_ModifyPatient.BackColor = System.Drawing.Color.Transparent
        Me.tlbbtn_ModifyPatient.BackgroundImage = CType(resources.GetObject("tlbbtn_ModifyPatient.BackgroundImage"),System.Drawing.Image)
        Me.tlbbtn_ModifyPatient.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlbbtn_ModifyPatient.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.tlbbtn_ModifyPatient.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.tlbbtn_ModifyPatient.Image = CType(resources.GetObject("tlbbtn_ModifyPatient.Image"),System.Drawing.Image)
        Me.tlbbtn_ModifyPatient.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbbtn_ModifyPatient.Name = "tlbbtn_ModifyPatient"
        Me.tlbbtn_ModifyPatient.Size = New System.Drawing.Size(57, 58)
        Me.tlbbtn_ModifyPatient.Tag = "Modify Patient"
        Me.tlbbtn_ModifyPatient.Text = "&Mod Pat"
        Me.tlbbtn_ModifyPatient.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlbbtn_ModifyPatient.ToolTipText = "Modify Patient"
        '
        'tlbbtn_ScanCard
        '
        Me.tlbbtn_ScanCard.BackColor = System.Drawing.Color.Transparent
        Me.tlbbtn_ScanCard.BackgroundImage = CType(resources.GetObject("tlbbtn_ScanCard.BackgroundImage"),System.Drawing.Image)
        Me.tlbbtn_ScanCard.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlbbtn_ScanCard.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.tlbbtn_ScanCard.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.tlbbtn_ScanCard.Image = CType(resources.GetObject("tlbbtn_ScanCard.Image"),System.Drawing.Image)
        Me.tlbbtn_ScanCard.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbbtn_ScanCard.Name = "tlbbtn_ScanCard"
        Me.tlbbtn_ScanCard.Size = New System.Drawing.Size(67, 58)
        Me.tlbbtn_ScanCard.Tag = "Scan Card"
        Me.tlbbtn_ScanCard.Text = "&Scan Card"
        Me.tlbbtn_ScanCard.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlbbtn_ScanCard.ToolTipText = "Scan Card"
        '
        'tlsPatRegSep
        '
        Me.tlsPatRegSep.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.tlsPatRegSep.Name = "tlsPatRegSep"
        Me.tlsPatRegSep.Size = New System.Drawing.Size(6, 61)
        '
        'tlbbtn_Vitals
        '
        Me.tlbbtn_Vitals.BackColor = System.Drawing.Color.Transparent
        Me.tlbbtn_Vitals.BackgroundImage = CType(resources.GetObject("tlbbtn_Vitals.BackgroundImage"),System.Drawing.Image)
        Me.tlbbtn_Vitals.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlbbtn_Vitals.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.tlbbtn_Vitals.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.tlbbtn_Vitals.Image = CType(resources.GetObject("tlbbtn_Vitals.Image"),System.Drawing.Image)
        Me.tlbbtn_Vitals.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbbtn_Vitals.Name = "tlbbtn_Vitals"
        Me.tlbbtn_Vitals.Size = New System.Drawing.Size(46, 58)
        Me.tlbbtn_Vitals.Tag = "Vitals"
        Me.tlbbtn_Vitals.Text = "&Vitals"
        Me.tlbbtn_Vitals.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlbbtn_Vitals.ToolTipText = "Vitals"
        '
        'tlbbtn_History
        '
        Me.tlbbtn_History.BackColor = System.Drawing.Color.Transparent
        Me.tlbbtn_History.BackgroundImage = CType(resources.GetObject("tlbbtn_History.BackgroundImage"),System.Drawing.Image)
        Me.tlbbtn_History.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlbbtn_History.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.tlbbtn_History.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.tlbbtn_History.Image = CType(resources.GetObject("tlbbtn_History.Image"),System.Drawing.Image)
        Me.tlbbtn_History.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbbtn_History.Name = "tlbbtn_History"
        Me.tlbbtn_History.Size = New System.Drawing.Size(52, 58)
        Me.tlbbtn_History.Tag = "History"
        Me.tlbbtn_History.Text = "&History"
        Me.tlbbtn_History.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tlbbtn_Prescription
        '
        Me.tlbbtn_Prescription.BackColor = System.Drawing.Color.Transparent
        Me.tlbbtn_Prescription.BackgroundImage = CType(resources.GetObject("tlbbtn_Prescription.BackgroundImage"),System.Drawing.Image)
        Me.tlbbtn_Prescription.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlbbtn_Prescription.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.tlbbtn_Prescription.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.tlbbtn_Prescription.Image = CType(resources.GetObject("tlbbtn_Prescription.Image"),System.Drawing.Image)
        Me.tlbbtn_Prescription.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbbtn_Prescription.Name = "tlbbtn_Prescription"
        Me.tlbbtn_Prescription.Size = New System.Drawing.Size(61, 58)
        Me.tlbbtn_Prescription.Tag = "Prescription & Medication"
        Me.tlbbtn_Prescription.Text = "&Rx-Meds"
        Me.tlbbtn_Prescription.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlbbtn_Prescription.ToolTipText = "Prescription and Medication"
        '
        'tlbbtn_gloLabOrders
        '
        Me.tlbbtn_gloLabOrders.BackColor = System.Drawing.Color.Transparent
        Me.tlbbtn_gloLabOrders.BackgroundImage = CType(resources.GetObject("tlbbtn_gloLabOrders.BackgroundImage"),System.Drawing.Image)
        Me.tlbbtn_gloLabOrders.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlbbtn_gloLabOrders.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.tlbbtn_gloLabOrders.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.tlbbtn_gloLabOrders.Image = CType(resources.GetObject("tlbbtn_gloLabOrders.Image"),System.Drawing.Image)
        Me.tlbbtn_gloLabOrders.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbbtn_gloLabOrders.Name = "tlbbtn_gloLabOrders"
        Me.tlbbtn_gloLabOrders.Size = New System.Drawing.Size(106, 58)
        Me.tlbbtn_gloLabOrders.Tag = "Orders & Results"
        Me.tlbbtn_gloLabOrders.Text = "&Orders && Results"
        Me.tlbbtn_gloLabOrders.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlbbtn_gloLabOrders.ToolTipText = "Orders & Results"
        '
        'tlbbtn_LabOrders
        '
        Me.tlbbtn_LabOrders.BackColor = System.Drawing.Color.Transparent
        Me.tlbbtn_LabOrders.BackgroundImage = CType(resources.GetObject("tlbbtn_LabOrders.BackgroundImage"),System.Drawing.Image)
        Me.tlbbtn_LabOrders.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlbbtn_LabOrders.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.tlbbtn_LabOrders.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.tlbbtn_LabOrders.Image = CType(resources.GetObject("tlbbtn_LabOrders.Image"),System.Drawing.Image)
        Me.tlbbtn_LabOrders.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbbtn_LabOrders.Name = "tlbbtn_LabOrders"
        Me.tlbbtn_LabOrders.Size = New System.Drawing.Size(49, 58)
        Me.tlbbtn_LabOrders.Tag = "Labs Orders Old"
        Me.tlbbtn_LabOrders.Text = "&Orders"
        Me.tlbbtn_LabOrders.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlbbtn_LabOrders.ToolTipText = "Orders"
        Me.tlbbtn_LabOrders.Visible = false
        '
        'tlsMainSep
        '
        Me.tlsMainSep.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.tlsMainSep.Name = "tlsMainSep"
        Me.tlsMainSep.Size = New System.Drawing.Size(6, 61)
        '
        'tlbbtn_NewExam
        '
        Me.tlbbtn_NewExam.BackColor = System.Drawing.Color.Transparent
        Me.tlbbtn_NewExam.BackgroundImage = CType(resources.GetObject("tlbbtn_NewExam.BackgroundImage"),System.Drawing.Image)
        Me.tlbbtn_NewExam.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlbbtn_NewExam.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.tlbbtn_NewExam.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.tlbbtn_NewExam.Image = CType(resources.GetObject("tlbbtn_NewExam.Image"),System.Drawing.Image)
        Me.tlbbtn_NewExam.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbbtn_NewExam.Name = "tlbbtn_NewExam"
        Me.tlbbtn_NewExam.Size = New System.Drawing.Size(68, 58)
        Me.tlbbtn_NewExam.Tag = "New Exam"
        Me.tlbbtn_NewExam.Text = "New &Exam"
        Me.tlbbtn_NewExam.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlbbtn_NewExam.Visible = false
        '
        'tlbbtn_PastExam
        '
        Me.tlbbtn_PastExam.BackColor = System.Drawing.Color.Transparent
        Me.tlbbtn_PastExam.BackgroundImage = CType(resources.GetObject("tlbbtn_PastExam.BackgroundImage"),System.Drawing.Image)
        Me.tlbbtn_PastExam.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlbbtn_PastExam.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.tlbbtn_PastExam.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.tlbbtn_PastExam.Image = CType(resources.GetObject("tlbbtn_PastExam.Image"),System.Drawing.Image)
        Me.tlbbtn_PastExam.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbbtn_PastExam.Name = "tlbbtn_PastExam"
        Me.tlbbtn_PastExam.Size = New System.Drawing.Size(76, 58)
        Me.tlbbtn_PastExam.Tag = "Past Exams"
        Me.tlbbtn_PastExam.Text = "&Past Exams"
        Me.tlbbtn_PastExam.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlbbtn_PastExam.ToolTipText = "Past Exams"
        '
        'tlbbtn_UnFinishedExams
        '
        Me.tlbbtn_UnFinishedExams.BackColor = System.Drawing.Color.Transparent
        Me.tlbbtn_UnFinishedExams.BackgroundImage = CType(resources.GetObject("tlbbtn_UnFinishedExams.BackgroundImage"),System.Drawing.Image)
        Me.tlbbtn_UnFinishedExams.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlbbtn_UnFinishedExams.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.tlbbtn_UnFinishedExams.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.tlbbtn_UnFinishedExams.Image = CType(resources.GetObject("tlbbtn_UnFinishedExams.Image"),System.Drawing.Image)
        Me.tlbbtn_UnFinishedExams.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbbtn_UnFinishedExams.Name = "tlbbtn_UnFinishedExams"
        Me.tlbbtn_UnFinishedExams.Size = New System.Drawing.Size(70, 58)
        Me.tlbbtn_UnFinishedExams.Tag = "Unfinished Exams"
        Me.tlbbtn_UnFinishedExams.Text = "&Unfinished"
        Me.tlbbtn_UnFinishedExams.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlbbtn_UnFinishedExams.ToolTipText = "Unfinished Exams"
        '
        'tlsExamSep
        '
        Me.tlsExamSep.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.tlsExamSep.Name = "tlsExamSep"
        Me.tlsExamSep.Size = New System.Drawing.Size(6, 61)
        '
        'tlbbtn_Calender
        '
        Me.tlbbtn_Calender.BackColor = System.Drawing.Color.Transparent
        Me.tlbbtn_Calender.BackgroundImage = CType(resources.GetObject("tlbbtn_Calender.BackgroundImage"),System.Drawing.Image)
        Me.tlbbtn_Calender.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlbbtn_Calender.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.tlbbtn_Calender.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.tlbbtn_Calender.Image = CType(resources.GetObject("tlbbtn_Calender.Image"),System.Drawing.Image)
        Me.tlbbtn_Calender.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbbtn_Calender.Name = "tlbbtn_Calender"
        Me.tlbbtn_Calender.Size = New System.Drawing.Size(61, 58)
        Me.tlbbtn_Calender.Tag = "Calendar"
        Me.tlbbtn_Calender.Text = "&Calendar"
        Me.tlbbtn_Calender.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tlbbtn_Sechedule
        '
        Me.tlbbtn_Sechedule.BackColor = System.Drawing.Color.Transparent
        Me.tlbbtn_Sechedule.BackgroundImage = CType(resources.GetObject("tlbbtn_Sechedule.BackgroundImage"),System.Drawing.Image)
        Me.tlbbtn_Sechedule.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlbbtn_Sechedule.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.tlbbtn_Sechedule.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.tlbbtn_Sechedule.Image = CType(resources.GetObject("tlbbtn_Sechedule.Image"),System.Drawing.Image)
        Me.tlbbtn_Sechedule.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbbtn_Sechedule.Name = "tlbbtn_Sechedule"
        Me.tlbbtn_Sechedule.Size = New System.Drawing.Size(62, 58)
        Me.tlbbtn_Sechedule.Tag = "Schedule"
        Me.tlbbtn_Sechedule.Text = "&Schedule"
        Me.tlbbtn_Sechedule.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tlsCalenderSep
        '
        Me.tlsCalenderSep.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.tlsCalenderSep.Name = "tlsCalenderSep"
        Me.tlsCalenderSep.Size = New System.Drawing.Size(6, 61)
        '
        'tlbbtn_RCMDocs
        '
        Me.tlbbtn_RCMDocs.BackColor = System.Drawing.Color.Transparent
        Me.tlbbtn_RCMDocs.BackgroundImage = CType(resources.GetObject("tlbbtn_RCMDocs.BackgroundImage"),System.Drawing.Image)
        Me.tlbbtn_RCMDocs.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlbbtn_RCMDocs.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.tlbbtn_RCMDocs.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.tlbbtn_RCMDocs.Image = CType(resources.GetObject("tlbbtn_RCMDocs.Image"),System.Drawing.Image)
        Me.tlbbtn_RCMDocs.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbbtn_RCMDocs.Name = "tlbbtn_RCMDocs"
        Me.tlbbtn_RCMDocs.Size = New System.Drawing.Size(66, 58)
        Me.tlbbtn_RCMDocs.Tag = "RCM Documents"
        Me.tlbbtn_RCMDocs.Text = "&RCM Docs"
        Me.tlbbtn_RCMDocs.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlbbtn_RCMDocs.ToolTipText = "RCM Documents"
        '
        'tlbbtn_ScanDocs
        '
        Me.tlbbtn_ScanDocs.BackColor = System.Drawing.Color.Transparent
        Me.tlbbtn_ScanDocs.BackgroundImage = CType(resources.GetObject("tlbbtn_ScanDocs.BackgroundImage"),System.Drawing.Image)
        Me.tlbbtn_ScanDocs.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlbbtn_ScanDocs.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.tlbbtn_ScanDocs.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.tlbbtn_ScanDocs.Image = CType(resources.GetObject("tlbbtn_ScanDocs.Image"),System.Drawing.Image)
        Me.tlbbtn_ScanDocs.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbbtn_ScanDocs.Name = "tlbbtn_ScanDocs"
        Me.tlbbtn_ScanDocs.Size = New System.Drawing.Size(68, 58)
        Me.tlbbtn_ScanDocs.Tag = "Scan Documents"
        Me.tlbbtn_ScanDocs.Text = "&Scan Docs"
        Me.tlbbtn_ScanDocs.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlbbtn_ScanDocs.ToolTipText = "Scan Documents"
        '
        'tlsDOCmgntSep
        '
        Me.tlsDOCmgntSep.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.tlsDOCmgntSep.Name = "tlsDOCmgntSep"
        Me.tlsDOCmgntSep.Size = New System.Drawing.Size(6, 61)
        '
        'tlbbtn_FormGallery
        '
        Me.tlbbtn_FormGallery.BackColor = System.Drawing.Color.Transparent
        Me.tlbbtn_FormGallery.BackgroundImage = CType(resources.GetObject("tlbbtn_FormGallery.BackgroundImage"),System.Drawing.Image)
        Me.tlbbtn_FormGallery.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlbbtn_FormGallery.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.tlbbtn_FormGallery.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.tlbbtn_FormGallery.Image = CType(resources.GetObject("tlbbtn_FormGallery.Image"),System.Drawing.Image)
        Me.tlbbtn_FormGallery.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbbtn_FormGallery.Name = "tlbbtn_FormGallery"
        Me.tlbbtn_FormGallery.Size = New System.Drawing.Size(83, 58)
        Me.tlbbtn_FormGallery.Tag = "Form Gallery"
        Me.tlbbtn_FormGallery.Text = "&Form Gallery"
        Me.tlbbtn_FormGallery.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlbbtn_FormGallery.ToolTipText = "Form Gallery"
        '
        'tlbbtn_PatientSynopsis
        '
        Me.tlbbtn_PatientSynopsis.BackColor = System.Drawing.Color.Transparent
        Me.tlbbtn_PatientSynopsis.BackgroundImage = CType(resources.GetObject("tlbbtn_PatientSynopsis.BackgroundImage"),System.Drawing.Image)
        Me.tlbbtn_PatientSynopsis.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlbbtn_PatientSynopsis.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.tlbbtn_PatientSynopsis.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.tlbbtn_PatientSynopsis.Image = CType(resources.GetObject("tlbbtn_PatientSynopsis.Image"),System.Drawing.Image)
        Me.tlbbtn_PatientSynopsis.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbbtn_PatientSynopsis.Name = "tlbbtn_PatientSynopsis"
        Me.tlbbtn_PatientSynopsis.Size = New System.Drawing.Size(61, 58)
        Me.tlbbtn_PatientSynopsis.Tag = "Patient Synopsis"
        Me.tlbbtn_PatientSynopsis.Text = "&Synopsis"
        Me.tlbbtn_PatientSynopsis.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlbbtn_PatientSynopsis.ToolTipText = "Patient Synopsis"
        '
        'tlsFormGalerySep
        '
        Me.tlsFormGalerySep.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.tlsFormGalerySep.Name = "tlsFormGalerySep"
        Me.tlsFormGalerySep.Size = New System.Drawing.Size(6, 61)
        '
        'tlbbtn_RxHub
        '
        Me.tlbbtn_RxHub.BackColor = System.Drawing.Color.Transparent
        Me.tlbbtn_RxHub.BackgroundImage = CType(resources.GetObject("tlbbtn_RxHub.BackgroundImage"),System.Drawing.Image)
        Me.tlbbtn_RxHub.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlbbtn_RxHub.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.tlbbtn_RxHub.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.tlbbtn_RxHub.Image = CType(resources.GetObject("tlbbtn_RxHub.Image"),System.Drawing.Image)
        Me.tlbbtn_RxHub.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbbtn_RxHub.Name = "tlbbtn_RxHub"
        Me.tlbbtn_RxHub.Size = New System.Drawing.Size(46, 58)
        Me.tlbbtn_RxHub.Tag = "RxElig"
        Me.tlbbtn_RxHub.Text = "&RxElig"
        Me.tlbbtn_RxHub.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlbbtn_RxHub.ToolTipText = "Rx Eligibility"
        '
        'tlbbtn_LockScreen
        '
        Me.tlbbtn_LockScreen.BackColor = System.Drawing.Color.Transparent
        Me.tlbbtn_LockScreen.BackgroundImage = CType(resources.GetObject("tlbbtn_LockScreen.BackgroundImage"),System.Drawing.Image)
        Me.tlbbtn_LockScreen.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlbbtn_LockScreen.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.tlbbtn_LockScreen.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.tlbbtn_LockScreen.Image = CType(resources.GetObject("tlbbtn_LockScreen.Image"),System.Drawing.Image)
        Me.tlbbtn_LockScreen.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbbtn_LockScreen.Name = "tlbbtn_LockScreen"
        Me.tlbbtn_LockScreen.Size = New System.Drawing.Size(54, 58)
        Me.tlbbtn_LockScreen.Tag = "LogOut"
        Me.tlbbtn_LockScreen.Text = "&Log Out"
        Me.tlbbtn_LockScreen.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlbbtn_LockScreen.ToolTipText = "Log Out"
        '
        'tlbbtn_Close
        '
        Me.tlbbtn_Close.BackColor = System.Drawing.Color.Transparent
        Me.tlbbtn_Close.BackgroundImage = CType(resources.GetObject("tlbbtn_Close.BackgroundImage"),System.Drawing.Image)
        Me.tlbbtn_Close.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlbbtn_Close.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.tlbbtn_Close.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.tlbbtn_Close.Image = CType(resources.GetObject("tlbbtn_Close.Image"),System.Drawing.Image)
        Me.tlbbtn_Close.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbbtn_Close.Name = "tlbbtn_Close"
        Me.tlbbtn_Close.Size = New System.Drawing.Size(46, 58)
        Me.tlbbtn_Close.Tag = "Close"
        Me.tlbbtn_Close.Text = "&Exit"
        Me.tlbbtn_Close.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlbbtn_Close.ToolTipText = "Exit Application"
        '
        'tlbbtnSaveCDS
        '
        Me.tlbbtnSaveCDS.BackColor = System.Drawing.Color.Transparent
        Me.tlbbtnSaveCDS.BackgroundImage = CType(resources.GetObject("tlbbtnSaveCDS.BackgroundImage"),System.Drawing.Image)
        Me.tlbbtnSaveCDS.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlbbtnSaveCDS.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.tlbbtnSaveCDS.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.tlbbtnSaveCDS.Image = CType(resources.GetObject("tlbbtnSaveCDS.Image"),System.Drawing.Image)
        Me.tlbbtnSaveCDS.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbbtnSaveCDS.Name = "tlbbtnSaveCDS"
        Me.tlbbtnSaveCDS.Size = New System.Drawing.Size(46, 59)
        Me.tlbbtnSaveCDS.Tag = "Clinical Decision Support"
        Me.tlbbtnSaveCDS.Text = "&CDS"
        Me.tlbbtnSaveCDS.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlbbtnSaveCDS.ToolTipText = "Clinical Decision Support"
        '
        'tlbbtn_GenerateCDA
        '
        Me.tlbbtn_GenerateCDA.BackColor = System.Drawing.Color.Transparent
        Me.tlbbtn_GenerateCDA.BackgroundImage = CType(resources.GetObject("tlbbtn_GenerateCDA.BackgroundImage"),System.Drawing.Image)
        Me.tlbbtn_GenerateCDA.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlbbtn_GenerateCDA.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.tlbbtn_GenerateCDA.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.tlbbtn_GenerateCDA.Image = Global.gloEMR.My.Resources.Resources.Import_CDA
        Me.tlbbtn_GenerateCDA.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbbtn_GenerateCDA.Name = "tlbbtn_GenerateCDA"
        Me.tlbbtn_GenerateCDA.Size = New System.Drawing.Size(46, 59)
        Me.tlbbtn_GenerateCDA.Tag = "CDA"
        Me.tlbbtn_GenerateCDA.Text = "CD&A"
        Me.tlbbtn_GenerateCDA.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlbbtn_GenerateCDA.ToolTipText = "Generate CDA"
        '
        'tlbbtn_GenerateCCD
        '
        Me.tlbbtn_GenerateCCD.BackColor = System.Drawing.Color.Transparent
        Me.tlbbtn_GenerateCCD.BackgroundImage = CType(resources.GetObject("tlbbtn_GenerateCCD.BackgroundImage"),System.Drawing.Image)
        Me.tlbbtn_GenerateCCD.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlbbtn_GenerateCCD.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.tlbbtn_GenerateCCD.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.tlbbtn_GenerateCCD.Image = CType(resources.GetObject("tlbbtn_GenerateCCD.Image"),System.Drawing.Image)
        Me.tlbbtn_GenerateCCD.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbbtn_GenerateCCD.Name = "tlbbtn_GenerateCCD"
        Me.tlbbtn_GenerateCCD.Size = New System.Drawing.Size(46, 59)
        Me.tlbbtn_GenerateCCD.Tag = "CCD"
        Me.tlbbtn_GenerateCCD.Text = "CC&D"
        Me.tlbbtn_GenerateCCD.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlbbtn_GenerateCCD.ToolTipText = "Generate CCD (old)"
        '
        'tlbbtn_Timeline
        '
        Me.tlbbtn_Timeline.BackColor = System.Drawing.Color.Transparent
        Me.tlbbtn_Timeline.BackgroundImage = CType(resources.GetObject("tlbbtn_Timeline.BackgroundImage"),System.Drawing.Image)
        Me.tlbbtn_Timeline.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlbbtn_Timeline.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.tlbbtn_Timeline.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.tlbbtn_Timeline.Image = CType(resources.GetObject("tlbbtn_Timeline.Image"),System.Drawing.Image)
        Me.tlbbtn_Timeline.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbbtn_Timeline.Name = "tlbbtn_Timeline"
        Me.tlbbtn_Timeline.Size = New System.Drawing.Size(59, 59)
        Me.tlbbtn_Timeline.Tag = "Timeline"
        Me.tlbbtn_Timeline.Text = "&Timeline"
        Me.tlbbtn_Timeline.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlbbtn_Timeline.ToolTipText = "Timeline"
        '
        'tlbbtn_Inbox
        '
        Me.tlbbtn_Inbox.BackColor = System.Drawing.Color.Transparent
        Me.tlbbtn_Inbox.BackgroundImage = CType(resources.GetObject("tlbbtn_Inbox.BackgroundImage"),System.Drawing.Image)
        Me.tlbbtn_Inbox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlbbtn_Inbox.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.tlbbtn_Inbox.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.tlbbtn_Inbox.Image = CType(resources.GetObject("tlbbtn_Inbox.Image"),System.Drawing.Image)
        Me.tlbbtn_Inbox.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbbtn_Inbox.Name = "tlbbtn_Inbox"
        Me.tlbbtn_Inbox.Size = New System.Drawing.Size(46, 59)
        Me.tlbbtn_Inbox.Tag = "Inbox"
        Me.tlbbtn_Inbox.Text = "&Inbox"
        Me.tlbbtn_Inbox.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlbbtn_Inbox.ToolTipText = "Provider DIRECT Message Inbox"
        '
        'tlbbtn_Orders
        '
        Me.tlbbtn_Orders.BackColor = System.Drawing.Color.Transparent
        Me.tlbbtn_Orders.BackgroundImage = CType(resources.GetObject("tlbbtn_Orders.BackgroundImage"),System.Drawing.Image)
        Me.tlbbtn_Orders.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlbbtn_Orders.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.tlbbtn_Orders.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.tlbbtn_Orders.Image = CType(resources.GetObject("tlbbtn_Orders.Image"),System.Drawing.Image)
        Me.tlbbtn_Orders.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbbtn_Orders.Name = "tlbbtn_Orders"
        Me.tlbbtn_Orders.Size = New System.Drawing.Size(106, 59)
        Me.tlbbtn_Orders.Tag = "Order Templates"
        Me.tlbbtn_Orders.Text = "&Order Templates"
        Me.tlbbtn_Orders.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlbbtn_Orders.ToolTipText = "Order Templates"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(0,Byte),Integer), CType(CType(45,Byte),Integer), CType(CType(150,Byte),Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label3.Location = New System.Drawing.Point(0, 61)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(1265, 1)
        Me.Label3.TabIndex = 65
        Me.Label3.Text = "Label3"
        '
        'pnlMainMenu
        '
        Me.pnlMainMenu.BackColor = System.Drawing.Color.FromArgb(CType(CType(207,Byte),Integer), CType(CType(227,Byte),Integer), CType(CType(255,Byte),Integer))
        Me.pnlMainMenu.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.pnlMainMenu.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlMainMenu.Controls.Add(Me.Label1)
        Me.pnlMainMenu.Controls.Add(Me.MenuStrip1)
        Me.pnlMainMenu.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlMainMenu.Location = New System.Drawing.Point(0, 0)
        Me.pnlMainMenu.Name = "pnlMainMenu"
        Me.pnlMainMenu.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.pnlMainMenu.Size = New System.Drawing.Size(1265, 28)
        Me.pnlMainMenu.TabIndex = 64
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(0,Byte),Integer), CType(CType(45,Byte),Integer), CType(CType(150,Byte),Integer))
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label1.Location = New System.Drawing.Point(0, 24)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(1265, 1)
        Me.Label1.TabIndex = 64
        Me.Label1.Text = "Label1"
        '
        'MenuStrip1
        '
        Me.MenuStrip1.AccessibleRole = System.Windows.Forms.AccessibleRole.MenuBar
        Me.MenuStrip1.BackColor = System.Drawing.Color.Transparent
        Me.MenuStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.MenuStrip1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MenuStrip1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.MenuStrip1.ImeMode = System.Windows.Forms.ImeMode.Off
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuFile, Me.mnuMasters, Me.mnuPatient, Me.mnuView, Me.mnuTools, Me.mnuReports, Me.mnuWindow, Me.mnuSharepoint, Me.mnuHelp})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional
        Me.MenuStrip1.ShowItemToolTips = true
        Me.MenuStrip1.Size = New System.Drawing.Size(1265, 25)
        Me.MenuStrip1.TabIndex = 63
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'mnuFile
        '
        Me.mnuFile.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnu_DashBoard, Me.mnuFileImport, Me.mnuFileExit})
        Me.mnuFile.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuFile.Name = "mnuFile"
        Me.mnuFile.Size = New System.Drawing.Size(35, 21)
        Me.mnuFile.Text = "File"
        '
        'mnu_DashBoard
        '
        Me.mnu_DashBoard.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnu_DashBoard.Image = CType(resources.GetObject("mnu_DashBoard.Image"),System.Drawing.Image)
        Me.mnu_DashBoard.Name = "mnu_DashBoard"
        Me.mnu_DashBoard.Size = New System.Drawing.Size(126, 22)
        Me.mnu_DashBoard.Text = "&Dashboard"
        '
        'mnuFileImport
        '
        Me.mnuFileImport.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuFileImport_Genius})
        Me.mnuFileImport.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuFileImport.Image = CType(resources.GetObject("mnuFileImport.Image"),System.Drawing.Image)
        Me.mnuFileImport.Name = "mnuFileImport"
        Me.mnuFileImport.Size = New System.Drawing.Size(126, 22)
        Me.mnuFileImport.Text = "&Import"
        Me.mnuFileImport.Visible = false
        '
        'mnuFileImport_Genius
        '
        Me.mnuFileImport_Genius.Image = CType(resources.GetObject("mnuFileImport_Genius.Image"),System.Drawing.Image)
        Me.mnuFileImport_Genius.Name = "mnuFileImport_Genius"
        Me.mnuFileImport_Genius.Size = New System.Drawing.Size(106, 22)
        Me.mnuFileImport_Genius.Text = "Genius"
        '
        'mnuFileExit
        '
        Me.mnuFileExit.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuFileExit.Image = CType(resources.GetObject("mnuFileExit.Image"),System.Drawing.Image)
        Me.mnuFileExit.Name = "mnuFileExit"
        Me.mnuFileExit.Size = New System.Drawing.Size(126, 22)
        Me.mnuFileExit.Text = "E&xit"
        '
        'mnuMasters
        '
        Me.mnuMasters.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuMastersDrugs, Me.mnuMaster_DrugProviderAssociation, Me.mnuMastersSIG, Me.sep2, Me.mnuMastersTemplates, Me.mnuLiquidData, Me.mnuMastersCategory, Me.mnuMedicalCategory, Me.mnuMastersDMSCategory, Me.mnuMastersRCMCategory, Me.mnuEducationMapping, Me.sep3, Me.mnuMasterROS, Me.mnuMastersHistory, Me.mnuMastersOBPlan, Me.mnuMastersFamilyMember, Me.mnuVital, Me.sep4, Me.mnuMastersContacts, Me.sep5, Me.mnuMastersLabSetup, Me.mnuMastersRadiology, Me.sep6, Me.mnuMastersFlowsheet, Me.mnuMastersSpeciality, Me.sep7, Me.mnu_DM_Setup, Me.mnu_IM_Setup, Me.mnu_CV_Setup, Me.mnu_TaxID_Setup, Me.ToolStripSeparator22, Me.mnuMaster_ClinicalInstructions, Me.mnuMaster_CarePlan, Me.mnu_ImplantableDevice_Setup, Me.sep8, Me.mnuMaster_SmartDiagnosis, Me.mnuMaster_SmartTreatment, Me.mnuMaster_SmartOrder, Me.sep9, Me.mnuMaster_FormGallery, Me.mnuMaster_StatusUsers, Me.mnuMaster_PatientSummaryScreen, Me.mnuMaster_AppointmentBook, Me.mnu_BillingConfiguration, Me.mnu_TaskMailConfiguration, Me.mnuMaster_ICDCPTGallery, Me.mnuMastersDisclosuerSet, Me.mnu_TemplateAssociation, Me.ZipToolStripMenuItem, Me.mnuRights})
        Me.mnuMasters.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuMasters.Name = "mnuMasters"
        Me.mnuMasters.Size = New System.Drawing.Size(37, 21)
        Me.mnuMasters.Text = "&Edit"
        '
        'mnuMastersDrugs
        '
        Me.mnuMastersDrugs.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuMastersDrugs.Image = CType(resources.GetObject("mnuMastersDrugs.Image"),System.Drawing.Image)
        Me.mnuMastersDrugs.Name = "mnuMastersDrugs"
        Me.mnuMastersDrugs.Size = New System.Drawing.Size(223, 22)
        Me.mnuMastersDrugs.Text = "&Drugs"
        '
        'mnuMaster_DrugProviderAssociation
        '
        Me.mnuMaster_DrugProviderAssociation.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuMaster_DrugProviderAssociation.Image = CType(resources.GetObject("mnuMaster_DrugProviderAssociation.Image"),System.Drawing.Image)
        Me.mnuMaster_DrugProviderAssociation.Name = "mnuMaster_DrugProviderAssociation"
        Me.mnuMaster_DrugProviderAssociation.Size = New System.Drawing.Size(223, 22)
        Me.mnuMaster_DrugProviderAssociation.Text = "Drugs Configuration"
        '
        'mnuMastersSIG
        '
        Me.mnuMastersSIG.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuMastersSIG.Image = CType(resources.GetObject("mnuMastersSIG.Image"),System.Drawing.Image)
        Me.mnuMastersSIG.Name = "mnuMastersSIG"
        Me.mnuMastersSIG.Size = New System.Drawing.Size(223, 22)
        Me.mnuMastersSIG.Text = "&SIG"
        '
        'sep2
        '
        Me.sep2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.sep2.Name = "sep2"
        Me.sep2.Size = New System.Drawing.Size(220, 6)
        '
        'mnuMastersTemplates
        '
        Me.mnuMastersTemplates.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuMastersTemplates.Image = CType(resources.GetObject("mnuMastersTemplates.Image"),System.Drawing.Image)
        Me.mnuMastersTemplates.Name = "mnuMastersTemplates"
        Me.mnuMastersTemplates.Size = New System.Drawing.Size(223, 22)
        Me.mnuMastersTemplates.Text = "&Templates"
        '
        'mnuLiquidData
        '
        Me.mnuLiquidData.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuLiquidData.Image = CType(resources.GetObject("mnuLiquidData.Image"),System.Drawing.Image)
        Me.mnuLiquidData.Name = "mnuLiquidData"
        Me.mnuLiquidData.Size = New System.Drawing.Size(223, 22)
        Me.mnuLiquidData.Text = "Liquid Data"
        '
        'mnuMastersCategory
        '
        Me.mnuMastersCategory.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuMastersCategory.Image = CType(resources.GetObject("mnuMastersCategory.Image"),System.Drawing.Image)
        Me.mnuMastersCategory.Name = "mnuMastersCategory"
        Me.mnuMastersCategory.Size = New System.Drawing.Size(223, 22)
        Me.mnuMastersCategory.Text = "&Category"
        '
        'mnuMedicalCategory
        '
        Me.mnuMedicalCategory.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuMedicalCategory.Image = CType(resources.GetObject("mnuMedicalCategory.Image"),System.Drawing.Image)
        Me.mnuMedicalCategory.Name = "mnuMedicalCategory"
        Me.mnuMedicalCategory.Size = New System.Drawing.Size(223, 22)
        Me.mnuMedicalCategory.Text = "&Medical Category"
        '
        'mnuMastersDMSCategory
        '
        Me.mnuMastersDMSCategory.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuMastersDMSCategory.Image = CType(resources.GetObject("mnuMastersDMSCategory.Image"),System.Drawing.Image)
        Me.mnuMastersDMSCategory.Name = "mnuMastersDMSCategory"
        Me.mnuMastersDMSCategory.Size = New System.Drawing.Size(223, 22)
        Me.mnuMastersDMSCategory.Text = "&DMS Category"
        '
        'mnuMastersRCMCategory
        '
        Me.mnuMastersRCMCategory.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuMastersRCMCategory.Image = CType(resources.GetObject("mnuMastersRCMCategory.Image"),System.Drawing.Image)
        Me.mnuMastersRCMCategory.Name = "mnuMastersRCMCategory"
        Me.mnuMastersRCMCategory.Size = New System.Drawing.Size(223, 22)
        Me.mnuMastersRCMCategory.Text = "&RCM Category"
        '
        'mnuEducationMapping
        '
        Me.mnuEducationMapping.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuEducationMapping.Image = CType(resources.GetObject("mnuEducationMapping.Image"),System.Drawing.Image)
        Me.mnuEducationMapping.Name = "mnuEducationMapping"
        Me.mnuEducationMapping.Size = New System.Drawing.Size(223, 22)
        Me.mnuEducationMapping.Text = "Education Material Mapping"
        '
        'sep3
        '
        Me.sep3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.sep3.Name = "sep3"
        Me.sep3.Size = New System.Drawing.Size(220, 6)
        '
        'mnuMasterROS
        '
        Me.mnuMasterROS.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuMasterROS.Image = CType(resources.GetObject("mnuMasterROS.Image"),System.Drawing.Image)
        Me.mnuMasterROS.Name = "mnuMasterROS"
        Me.mnuMasterROS.Size = New System.Drawing.Size(223, 22)
        Me.mnuMasterROS.Text = "&ROS"
        '
        'mnuMastersHistory
        '
        Me.mnuMastersHistory.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuMastersHistory.Image = CType(resources.GetObject("mnuMastersHistory.Image"),System.Drawing.Image)
        Me.mnuMastersHistory.Name = "mnuMastersHistory"
        Me.mnuMastersHistory.Size = New System.Drawing.Size(223, 22)
        Me.mnuMastersHistory.Text = "&History"
        '
        'mnuMastersOBPlan
        '
        Me.mnuMastersOBPlan.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuMastersOBPlan.Image = CType(resources.GetObject("mnuMastersOBPlan.Image"),System.Drawing.Image)
        Me.mnuMastersOBPlan.Name = "mnuMastersOBPlan"
        Me.mnuMastersOBPlan.Size = New System.Drawing.Size(223, 22)
        Me.mnuMastersOBPlan.Text = "&OB Plan"
        '
        'mnuMastersFamilyMember
        '
        Me.mnuMastersFamilyMember.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuMastersFamilyMember.Image = CType(resources.GetObject("mnuMastersFamilyMember.Image"),System.Drawing.Image)
        Me.mnuMastersFamilyMember.Name = "mnuMastersFamilyMember"
        Me.mnuMastersFamilyMember.Size = New System.Drawing.Size(223, 22)
        Me.mnuMastersFamilyMember.Text = "Family Member Relation Master"
        '
        'mnuVital
        '
        Me.mnuVital.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuTableTemplate, Me.mnuVitalNorms, Me.mnuOBVital})
        Me.mnuVital.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuVital.Image = CType(resources.GetObject("mnuVital.Image"),System.Drawing.Image)
        Me.mnuVital.Name = "mnuVital"
        Me.mnuVital.Size = New System.Drawing.Size(223, 22)
        Me.mnuVital.Text = "Vitals"
        '
        'mnuTableTemplate
        '
        Me.mnuTableTemplate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuTableTemplate.Image = CType(resources.GetObject("mnuTableTemplate.Image"),System.Drawing.Image)
        Me.mnuTableTemplate.Name = "mnuTableTemplate"
        Me.mnuTableTemplate.Size = New System.Drawing.Size(164, 22)
        Me.mnuTableTemplate.Text = "Table Template"
        '
        'mnuVitalNorms
        '
        Me.mnuVitalNorms.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuVitalNorms.Image = CType(resources.GetObject("mnuVitalNorms.Image"),System.Drawing.Image)
        Me.mnuVitalNorms.Name = "mnuVitalNorms"
        Me.mnuVitalNorms.Size = New System.Drawing.Size(164, 22)
        Me.mnuVitalNorms.Text = "Vital Norms"
        '
        'mnuOBVital
        '
        Me.mnuOBVital.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuOBVital.Image = CType(resources.GetObject("mnuOBVital.Image"),System.Drawing.Image)
        Me.mnuOBVital.Name = "mnuOBVital"
        Me.mnuOBVital.Size = New System.Drawing.Size(164, 22)
        Me.mnuOBVital.Text = "OB Vital Comments"
        '
        'sep4
        '
        Me.sep4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.sep4.Name = "sep4"
        Me.sep4.Size = New System.Drawing.Size(220, 6)
        '
        'mnuMastersContacts
        '
        Me.mnuMastersContacts.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuMastersContacts.Image = CType(resources.GetObject("mnuMastersContacts.Image"),System.Drawing.Image)
        Me.mnuMastersContacts.Name = "mnuMastersContacts"
        Me.mnuMastersContacts.Size = New System.Drawing.Size(223, 22)
        Me.mnuMastersContacts.Text = "&Contacts"
        '
        'sep5
        '
        Me.sep5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.sep5.Name = "sep5"
        Me.sep5.Size = New System.Drawing.Size(220, 6)
        '
        'mnuMastersLabSetup
        '
        Me.mnuMastersLabSetup.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuMastersLabSetup.Image = CType(resources.GetObject("mnuMastersLabSetup.Image"),System.Drawing.Image)
        Me.mnuMastersLabSetup.Name = "mnuMastersLabSetup"
        Me.mnuMastersLabSetup.Size = New System.Drawing.Size(223, 22)
        Me.mnuMastersLabSetup.Text = "&Orders && Results Setup"
        '
        'mnuMastersRadiology
        '
        Me.mnuMastersRadiology.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuMastersRadiology.Image = CType(resources.GetObject("mnuMastersRadiology.Image"),System.Drawing.Image)
        Me.mnuMastersRadiology.Name = "mnuMastersRadiology"
        Me.mnuMastersRadiology.Size = New System.Drawing.Size(223, 22)
        Me.mnuMastersRadiology.Text = "&Order Templates"
        '
        'sep6
        '
        Me.sep6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.sep6.Name = "sep6"
        Me.sep6.Size = New System.Drawing.Size(220, 6)
        '
        'mnuMastersFlowsheet
        '
        Me.mnuMastersFlowsheet.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuMastersFlowsheet.Image = CType(resources.GetObject("mnuMastersFlowsheet.Image"),System.Drawing.Image)
        Me.mnuMastersFlowsheet.Name = "mnuMastersFlowsheet"
        Me.mnuMastersFlowsheet.Size = New System.Drawing.Size(223, 22)
        Me.mnuMastersFlowsheet.Text = "&Flowsheet"
        '
        'mnuMastersSpeciality
        '
        Me.mnuMastersSpeciality.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuMastersSpeciality.Image = CType(resources.GetObject("mnuMastersSpeciality.Image"),System.Drawing.Image)
        Me.mnuMastersSpeciality.Name = "mnuMastersSpeciality"
        Me.mnuMastersSpeciality.Size = New System.Drawing.Size(223, 22)
        Me.mnuMastersSpeciality.Text = "&Specialty"
        '
        'sep7
        '
        Me.sep7.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.sep7.Name = "sep7"
        Me.sep7.Size = New System.Drawing.Size(220, 6)
        '
        'mnu_DM_Setup
        '
        Me.mnu_DM_Setup.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnu_DM_Setup.Image = CType(resources.GetObject("mnu_DM_Setup.Image"),System.Drawing.Image)
        Me.mnu_DM_Setup.Name = "mnu_DM_Setup"
        Me.mnu_DM_Setup.Size = New System.Drawing.Size(223, 22)
        Me.mnu_DM_Setup.Text = "DM Setup"
        '
        'mnu_IM_Setup
        '
        Me.mnu_IM_Setup.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnu_IM_Setup.Image = CType(resources.GetObject("mnu_IM_Setup.Image"),System.Drawing.Image)
        Me.mnu_IM_Setup.Name = "mnu_IM_Setup"
        Me.mnu_IM_Setup.Size = New System.Drawing.Size(223, 22)
        Me.mnu_IM_Setup.Text = "IM Setup"
        '
        'mnu_CV_Setup
        '
        Me.mnu_CV_Setup.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnu_CV_Setup.Image = CType(resources.GetObject("mnu_CV_Setup.Image"),System.Drawing.Image)
        Me.mnu_CV_Setup.Name = "mnu_CV_Setup"
        Me.mnu_CV_Setup.Size = New System.Drawing.Size(223, 22)
        Me.mnu_CV_Setup.Text = "CV Setup"
        '
        'mnu_TaxID_Setup
        '
        Me.mnu_TaxID_Setup.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnu_TaxID_Setup.Image = CType(resources.GetObject("mnu_TaxID_Setup.Image"),System.Drawing.Image)
        Me.mnu_TaxID_Setup.Name = "mnu_TaxID_Setup"
        Me.mnu_TaxID_Setup.Size = New System.Drawing.Size(223, 22)
        Me.mnu_TaxID_Setup.Text = "TaxID Setup"
        '
        'ToolStripSeparator22
        '
        Me.ToolStripSeparator22.Name = "ToolStripSeparator22"
        Me.ToolStripSeparator22.Size = New System.Drawing.Size(220, 6)
        '
        'mnuMaster_ClinicalInstructions
        '
        Me.mnuMaster_ClinicalInstructions.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuMaster_ClinicalInstructions.Image = Global.gloEMR.My.Resources.Resources.Patient_Specific
        Me.mnuMaster_ClinicalInstructions.Name = "mnuMaster_ClinicalInstructions"
        Me.mnuMaster_ClinicalInstructions.Size = New System.Drawing.Size(223, 22)
        Me.mnuMaster_ClinicalInstructions.Text = "Clinical Instructions"
        '
        'mnuMaster_CarePlan
        '
        Me.mnuMaster_CarePlan.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuMaster_CarePlan.Image = CType(resources.GetObject("mnuMaster_CarePlan.Image"),System.Drawing.Image)
        Me.mnuMaster_CarePlan.Name = "mnuMaster_CarePlan"
        Me.mnuMaster_CarePlan.Size = New System.Drawing.Size(223, 22)
        Me.mnuMaster_CarePlan.Text = "Legacy Care Plan"
        '
        'mnu_ImplantableDevice_Setup
        '
        Me.mnu_ImplantableDevice_Setup.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnu_ImplantableDevice_Setup.Image = CType(resources.GetObject("mnu_ImplantableDevice_Setup.Image"),System.Drawing.Image)
        Me.mnu_ImplantableDevice_Setup.Name = "mnu_ImplantableDevice_Setup"
        Me.mnu_ImplantableDevice_Setup.Size = New System.Drawing.Size(223, 22)
        Me.mnu_ImplantableDevice_Setup.Text = "Implantable Devices"
        '
        'sep8
        '
        Me.sep8.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.sep8.Name = "sep8"
        Me.sep8.Size = New System.Drawing.Size(220, 6)
        '
        'mnuMaster_SmartDiagnosis
        '
        Me.mnuMaster_SmartDiagnosis.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuMaster_SmartDiagnosis.Image = CType(resources.GetObject("mnuMaster_SmartDiagnosis.Image"),System.Drawing.Image)
        Me.mnuMaster_SmartDiagnosis.Name = "mnuMaster_SmartDiagnosis"
        Me.mnuMaster_SmartDiagnosis.Size = New System.Drawing.Size(223, 22)
        Me.mnuMaster_SmartDiagnosis.Text = "Smart Diagnosis"
        '
        'mnuMaster_SmartTreatment
        '
        Me.mnuMaster_SmartTreatment.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuMaster_SmartTreatment.Image = CType(resources.GetObject("mnuMaster_SmartTreatment.Image"),System.Drawing.Image)
        Me.mnuMaster_SmartTreatment.Name = "mnuMaster_SmartTreatment"
        Me.mnuMaster_SmartTreatment.Size = New System.Drawing.Size(223, 22)
        Me.mnuMaster_SmartTreatment.Text = "Smart Treatment"
        '
        'mnuMaster_SmartOrder
        '
        Me.mnuMaster_SmartOrder.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuMaster_SmartOrder.Image = CType(resources.GetObject("mnuMaster_SmartOrder.Image"),System.Drawing.Image)
        Me.mnuMaster_SmartOrder.Name = "mnuMaster_SmartOrder"
        Me.mnuMaster_SmartOrder.Size = New System.Drawing.Size(223, 22)
        Me.mnuMaster_SmartOrder.Text = "Smart Order"
        '
        'sep9
        '
        Me.sep9.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.sep9.Name = "sep9"
        Me.sep9.Size = New System.Drawing.Size(220, 6)
        '
        'mnuMaster_FormGallery
        '
        Me.mnuMaster_FormGallery.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuMaster_FormGallery.Image = CType(resources.GetObject("mnuMaster_FormGallery.Image"),System.Drawing.Image)
        Me.mnuMaster_FormGallery.Name = "mnuMaster_FormGallery"
        Me.mnuMaster_FormGallery.Size = New System.Drawing.Size(223, 22)
        Me.mnuMaster_FormGallery.Text = "Form Gallery"
        '
        'mnuMaster_StatusUsers
        '
        Me.mnuMaster_StatusUsers.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuMaster_StatusUsers.Image = CType(resources.GetObject("mnuMaster_StatusUsers.Image"),System.Drawing.Image)
        Me.mnuMaster_StatusUsers.Name = "mnuMaster_StatusUsers"
        Me.mnuMaster_StatusUsers.Size = New System.Drawing.Size(223, 22)
        Me.mnuMaster_StatusUsers.Text = "Status Users"
        '
        'mnuMaster_PatientSummaryScreen
        '
        Me.mnuMaster_PatientSummaryScreen.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuMaster_PatientSummaryScreen.Image = CType(resources.GetObject("mnuMaster_PatientSummaryScreen.Image"),System.Drawing.Image)
        Me.mnuMaster_PatientSummaryScreen.Name = "mnuMaster_PatientSummaryScreen"
        Me.mnuMaster_PatientSummaryScreen.Size = New System.Drawing.Size(223, 22)
        Me.mnuMaster_PatientSummaryScreen.Text = "Patient Summary"
        '
        'mnuMaster_AppointmentBook
        '
        Me.mnuMaster_AppointmentBook.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuMaster_AppointmentBook.Image = CType(resources.GetObject("mnuMaster_AppointmentBook.Image"),System.Drawing.Image)
        Me.mnuMaster_AppointmentBook.Name = "mnuMaster_AppointmentBook"
        Me.mnuMaster_AppointmentBook.Size = New System.Drawing.Size(223, 22)
        Me.mnuMaster_AppointmentBook.Text = "Appointment Configuration"
        '
        'mnu_BillingConfiguration
        '
        Me.mnu_BillingConfiguration.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnu_BillingConfiguration.Image = CType(resources.GetObject("mnu_BillingConfiguration.Image"),System.Drawing.Image)
        Me.mnu_BillingConfiguration.Name = "mnu_BillingConfiguration"
        Me.mnu_BillingConfiguration.Size = New System.Drawing.Size(223, 22)
        Me.mnu_BillingConfiguration.Text = "Billing Configuration"
        '
        'mnu_TaskMailConfiguration
        '
        Me.mnu_TaskMailConfiguration.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnu_TaskMailConfiguration.Image = CType(resources.GetObject("mnu_TaskMailConfiguration.Image"),System.Drawing.Image)
        Me.mnu_TaskMailConfiguration.Name = "mnu_TaskMailConfiguration"
        Me.mnu_TaskMailConfiguration.Size = New System.Drawing.Size(223, 22)
        Me.mnu_TaskMailConfiguration.Text = "Task/Mail Configuration"
        '
        'mnuMaster_ICDCPTGallery
        '
        Me.mnuMaster_ICDCPTGallery.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuMaster_ICD9Gallery, Me.mnuMaster_ICD10Gallery, Me.mnuMaster_CPTGallery})
        Me.mnuMaster_ICDCPTGallery.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuMaster_ICDCPTGallery.Image = CType(resources.GetObject("mnuMaster_ICDCPTGallery.Image"),System.Drawing.Image)
        Me.mnuMaster_ICDCPTGallery.Name = "mnuMaster_ICDCPTGallery"
        Me.mnuMaster_ICDCPTGallery.Size = New System.Drawing.Size(223, 22)
        Me.mnuMaster_ICDCPTGallery.Text = "Code Gallery"
        '
        'mnuMaster_ICD9Gallery
        '
        Me.mnuMaster_ICD9Gallery.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuMaster_ICD9Gallery.Image = CType(resources.GetObject("mnuMaster_ICD9Gallery.Image"),System.Drawing.Image)
        Me.mnuMaster_ICD9Gallery.Name = "mnuMaster_ICD9Gallery"
        Me.mnuMaster_ICD9Gallery.Size = New System.Drawing.Size(140, 22)
        Me.mnuMaster_ICD9Gallery.Text = "ICD9 Gallery"
        '
        'mnuMaster_ICD10Gallery
        '
        Me.mnuMaster_ICD10Gallery.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuMaster_ICD10Gallery.Image = CType(resources.GetObject("mnuMaster_ICD10Gallery.Image"),System.Drawing.Image)
        Me.mnuMaster_ICD10Gallery.Name = "mnuMaster_ICD10Gallery"
        Me.mnuMaster_ICD10Gallery.Size = New System.Drawing.Size(140, 22)
        Me.mnuMaster_ICD10Gallery.Text = "ICD10 Gallery"
        '
        'mnuMaster_CPTGallery
        '
        Me.mnuMaster_CPTGallery.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuMaster_CPTGallery.Image = CType(resources.GetObject("mnuMaster_CPTGallery.Image"),System.Drawing.Image)
        Me.mnuMaster_CPTGallery.Name = "mnuMaster_CPTGallery"
        Me.mnuMaster_CPTGallery.Size = New System.Drawing.Size(140, 22)
        Me.mnuMaster_CPTGallery.Text = "CPT Gallery"
        '
        'mnuMastersDisclosuerSet
        '
        Me.mnuMastersDisclosuerSet.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuMastersDisclosuerSet.Image = CType(resources.GetObject("mnuMastersDisclosuerSet.Image"),System.Drawing.Image)
        Me.mnuMastersDisclosuerSet.Name = "mnuMastersDisclosuerSet"
        Me.mnuMastersDisclosuerSet.Size = New System.Drawing.Size(223, 22)
        Me.mnuMastersDisclosuerSet.Text = "Disclosure Set"
        '
        'mnu_TemplateAssociation
        '
        Me.mnu_TemplateAssociation.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnu_TemplateAssociation.Image = CType(resources.GetObject("mnu_TemplateAssociation.Image"),System.Drawing.Image)
        Me.mnu_TemplateAssociation.Name = "mnu_TemplateAssociation"
        Me.mnu_TemplateAssociation.Size = New System.Drawing.Size(223, 22)
        Me.mnu_TemplateAssociation.Text = "Template Association"
        '
        'ZipToolStripMenuItem
        '
        Me.ZipToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.ZipToolStripMenuItem.Image = CType(resources.GetObject("ZipToolStripMenuItem.Image"),System.Drawing.Image)
        Me.ZipToolStripMenuItem.Name = "ZipToolStripMenuItem"
        Me.ZipToolStripMenuItem.Size = New System.Drawing.Size(223, 22)
        Me.ZipToolStripMenuItem.Text = "Zip"
        '
        'mnuRights
        '
        Me.mnuRights.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuRights.Image = CType(resources.GetObject("mnuRights.Image"),System.Drawing.Image)
        Me.mnuRights.Name = "mnuRights"
        Me.mnuRights.Size = New System.Drawing.Size(223, 22)
        Me.mnuRights.Text = "Rights"
        Me.mnuRights.Visible = false
        '
        'mnuPatient
        '
        Me.mnuPatient.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuPatientRegistration, Me.mnuModifyPatient, Me.sep10, Me.mnuPatientROS, Me.mnuPatientHistory, Me.mnuPatientVitals, Me.sep11, Me.mnuPatientMessages, Me.mnuPatientPrescription, Me.sep12, Me.mnuPatient_LabOrder, Me.mnuPatient_RadiologyOrders, Me.sep13, Me.mnuPatientLetters, Me.mnuPatientPTProtocols, Me.mnuPatientConcent, Me.mnuNurseNotes, Me.mnuDisclosureMgmt, Me.mnuPatientEducation, Me.sep14, Me.mnuPatientFlowSheet, Me.mnuPatient_FormGallery, Me.mnuPatient_Tasks, Me.sep15, Me.mnuProblemList, Me.mnuPlanOfTreatment, Me.mnu_IM_Transaction, Me.mnuPatient_FindHealthPlan, Me.mnu_ViewRecommendation, Me.mnu_UploadVideo, Me.mnu_ScanDocs, Me.mnu_ImplantableDevices, Me.ToolStripSeparator1, Me.mnu_ScreeningTools, Me.mnu_BillingCharges, Me.mnu_BillingBatch, Me.mnu_BillingPayment, Me.mnu_BillingAdvPayment, Me.mnu_BillingRemittance, Me.mnu_BillingBalance, Me.mnu_BillingLedger, Me.mnu_Appointment, Me.mnu_Schedule, Me.mnu_ClosedJournals, Me.mnuView_Triage1, Me.mnu_ClinicalChartPrintQueue, Me.mnu_SocPsycBehobs, Me.mnu_PortalPHI, Me.mnu_APIHarness, Me.mnu_CCDAPatientConsent})
        Me.mnuPatient.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuPatient.Name = "mnuPatient"
        Me.mnuPatient.Size = New System.Drawing.Size(32, 21)
        Me.mnuPatient.Text = "&Go"
        '
        'mnuPatientRegistration
        '
        Me.mnuPatientRegistration.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuPatientRegistration.Image = CType(resources.GetObject("mnuPatientRegistration.Image"),System.Drawing.Image)
        Me.mnuPatientRegistration.Name = "mnuPatientRegistration"
        Me.mnuPatientRegistration.Size = New System.Drawing.Size(287, 22)
        Me.mnuPatientRegistration.Text = "Patient Registration"
        '
        'mnuModifyPatient
        '
        Me.mnuModifyPatient.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuModifyPatient.Image = CType(resources.GetObject("mnuModifyPatient.Image"),System.Drawing.Image)
        Me.mnuModifyPatient.Name = "mnuModifyPatient"
        Me.mnuModifyPatient.Size = New System.Drawing.Size(287, 22)
        Me.mnuModifyPatient.Text = "Modify Patient"
        '
        'sep10
        '
        Me.sep10.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.sep10.Name = "sep10"
        Me.sep10.Size = New System.Drawing.Size(284, 6)
        '
        'mnuPatientROS
        '
        Me.mnuPatientROS.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuPatientROS.Image = CType(resources.GetObject("mnuPatientROS.Image"),System.Drawing.Image)
        Me.mnuPatientROS.Name = "mnuPatientROS"
        Me.mnuPatientROS.Size = New System.Drawing.Size(287, 22)
        Me.mnuPatientROS.Text = "Patient ROS"
        '
        'mnuPatientHistory
        '
        Me.mnuPatientHistory.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuPatientHistory.Image = CType(resources.GetObject("mnuPatientHistory.Image"),System.Drawing.Image)
        Me.mnuPatientHistory.Name = "mnuPatientHistory"
        Me.mnuPatientHistory.Size = New System.Drawing.Size(287, 22)
        Me.mnuPatientHistory.Text = "Patient History"
        '
        'mnuPatientVitals
        '
        Me.mnuPatientVitals.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuPatientVitals.Image = CType(resources.GetObject("mnuPatientVitals.Image"),System.Drawing.Image)
        Me.mnuPatientVitals.Name = "mnuPatientVitals"
        Me.mnuPatientVitals.Size = New System.Drawing.Size(287, 22)
        Me.mnuPatientVitals.Text = "Vitals"
        '
        'sep11
        '
        Me.sep11.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.sep11.Name = "sep11"
        Me.sep11.Size = New System.Drawing.Size(284, 6)
        '
        'mnuPatientMessages
        '
        Me.mnuPatientMessages.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuPatientMessages.Image = CType(resources.GetObject("mnuPatientMessages.Image"),System.Drawing.Image)
        Me.mnuPatientMessages.Name = "mnuPatientMessages"
        Me.mnuPatientMessages.Size = New System.Drawing.Size(287, 22)
        Me.mnuPatientMessages.Text = "Patient Messages"
        '
        'mnuPatientPrescription
        '
        Me.mnuPatientPrescription.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuPatientPrescription.Image = CType(resources.GetObject("mnuPatientPrescription.Image"),System.Drawing.Image)
        Me.mnuPatientPrescription.Name = "mnuPatientPrescription"
        Me.mnuPatientPrescription.Size = New System.Drawing.Size(287, 22)
        Me.mnuPatientPrescription.Text = "Rx-Meds"
        '
        'sep12
        '
        Me.sep12.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.sep12.Name = "sep12"
        Me.sep12.Size = New System.Drawing.Size(284, 6)
        '
        'mnuPatient_LabOrder
        '
        Me.mnuPatient_LabOrder.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuPatient_LabOrder.Image = CType(resources.GetObject("mnuPatient_LabOrder.Image"),System.Drawing.Image)
        Me.mnuPatient_LabOrder.Name = "mnuPatient_LabOrder"
        Me.mnuPatient_LabOrder.Size = New System.Drawing.Size(287, 22)
        Me.mnuPatient_LabOrder.Text = "Orders && Results"
        '
        'mnuPatient_RadiologyOrders
        '
        Me.mnuPatient_RadiologyOrders.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuPatient_RadiologyOrders.Image = CType(resources.GetObject("mnuPatient_RadiologyOrders.Image"),System.Drawing.Image)
        Me.mnuPatient_RadiologyOrders.Name = "mnuPatient_RadiologyOrders"
        Me.mnuPatient_RadiologyOrders.Size = New System.Drawing.Size(287, 22)
        Me.mnuPatient_RadiologyOrders.Text = "Order Templates"
        '
        'sep13
        '
        Me.sep13.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.sep13.Name = "sep13"
        Me.sep13.Size = New System.Drawing.Size(284, 6)
        '
        'mnuPatientLetters
        '
        Me.mnuPatientLetters.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuPatientLetters.Image = CType(resources.GetObject("mnuPatientLetters.Image"),System.Drawing.Image)
        Me.mnuPatientLetters.Name = "mnuPatientLetters"
        Me.mnuPatientLetters.Size = New System.Drawing.Size(287, 22)
        Me.mnuPatientLetters.Text = "Patient Letters"
        '
        'mnuPatientPTProtocols
        '
        Me.mnuPatientPTProtocols.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuPatientPTProtocols.Image = CType(resources.GetObject("mnuPatientPTProtocols.Image"),System.Drawing.Image)
        Me.mnuPatientPTProtocols.Name = "mnuPatientPTProtocols"
        Me.mnuPatientPTProtocols.Size = New System.Drawing.Size(287, 22)
        Me.mnuPatientPTProtocols.Text = "PT Protocols"
        '
        'mnuPatientConcent
        '
        Me.mnuPatientConcent.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuPatientConcent.Image = CType(resources.GetObject("mnuPatientConcent.Image"),System.Drawing.Image)
        Me.mnuPatientConcent.Name = "mnuPatientConcent"
        Me.mnuPatientConcent.Size = New System.Drawing.Size(287, 22)
        Me.mnuPatientConcent.Text = "Patient Consent"
        '
        'mnuNurseNotes
        '
        Me.mnuNurseNotes.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuNurseNotes.Image = CType(resources.GetObject("mnuNurseNotes.Image"),System.Drawing.Image)
        Me.mnuNurseNotes.Name = "mnuNurseNotes"
        Me.mnuNurseNotes.Size = New System.Drawing.Size(287, 22)
        Me.mnuNurseNotes.Text = "Nurses Notes"
        '
        'mnuDisclosureMgmt
        '
        Me.mnuDisclosureMgmt.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuDisclosureMgmt.Image = CType(resources.GetObject("mnuDisclosureMgmt.Image"),System.Drawing.Image)
        Me.mnuDisclosureMgmt.Name = "mnuDisclosureMgmt"
        Me.mnuDisclosureMgmt.Size = New System.Drawing.Size(287, 22)
        Me.mnuDisclosureMgmt.Text = "Disclosure Management"
        '
        'mnuPatientEducation
        '
        Me.mnuPatientEducation.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuPatientEducation.Image = CType(resources.GetObject("mnuPatientEducation.Image"),System.Drawing.Image)
        Me.mnuPatientEducation.Name = "mnuPatientEducation"
        Me.mnuPatientEducation.Size = New System.Drawing.Size(287, 22)
        Me.mnuPatientEducation.Text = "Patient Education"
        '
        'sep14
        '
        Me.sep14.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.sep14.Name = "sep14"
        Me.sep14.Size = New System.Drawing.Size(284, 6)
        '
        'mnuPatientFlowSheet
        '
        Me.mnuPatientFlowSheet.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuPatientFlowSheet.Image = CType(resources.GetObject("mnuPatientFlowSheet.Image"),System.Drawing.Image)
        Me.mnuPatientFlowSheet.Name = "mnuPatientFlowSheet"
        Me.mnuPatientFlowSheet.Size = New System.Drawing.Size(287, 22)
        Me.mnuPatientFlowSheet.Text = "Flowsheet"
        '
        'mnuPatient_FormGallery
        '
        Me.mnuPatient_FormGallery.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuPatient_FormGallery.Image = CType(resources.GetObject("mnuPatient_FormGallery.Image"),System.Drawing.Image)
        Me.mnuPatient_FormGallery.Name = "mnuPatient_FormGallery"
        Me.mnuPatient_FormGallery.Size = New System.Drawing.Size(287, 22)
        Me.mnuPatient_FormGallery.Text = "Form Gallery"
        '
        'mnuPatient_Tasks
        '
        Me.mnuPatient_Tasks.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuPatient_Tasks.Image = CType(resources.GetObject("mnuPatient_Tasks.Image"),System.Drawing.Image)
        Me.mnuPatient_Tasks.Name = "mnuPatient_Tasks"
        Me.mnuPatient_Tasks.Size = New System.Drawing.Size(287, 22)
        Me.mnuPatient_Tasks.Text = "Tasks"
        '
        'sep15
        '
        Me.sep15.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.sep15.Name = "sep15"
        Me.sep15.Size = New System.Drawing.Size(284, 6)
        '
        'mnuProblemList
        '
        Me.mnuProblemList.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuProblemList.Image = CType(resources.GetObject("mnuProblemList.Image"),System.Drawing.Image)
        Me.mnuProblemList.Name = "mnuProblemList"
        Me.mnuProblemList.Size = New System.Drawing.Size(287, 22)
        Me.mnuProblemList.Text = "Problem List"
        '
        'mnuPlanOfTreatment
        '
        Me.mnuPlanOfTreatment.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuPlanOfTreatment.Image = CType(resources.GetObject("mnuPlanOfTreatment.Image"),System.Drawing.Image)
        Me.mnuPlanOfTreatment.Name = "mnuPlanOfTreatment"
        Me.mnuPlanOfTreatment.Size = New System.Drawing.Size(287, 22)
        Me.mnuPlanOfTreatment.Text = "Plan of Treatment"
        '
        'mnu_IM_Transaction
        '
        Me.mnu_IM_Transaction.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnu_IM_Transaction.Image = CType(resources.GetObject("mnu_IM_Transaction.Image"),System.Drawing.Image)
        Me.mnu_IM_Transaction.Name = "mnu_IM_Transaction"
        Me.mnu_IM_Transaction.Size = New System.Drawing.Size(287, 22)
        Me.mnu_IM_Transaction.Text = "Immunization"
        '
        'mnuPatient_FindHealthPlan
        '
        Me.mnuPatient_FindHealthPlan.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuPatient_FindHealthPlan.Image = CType(resources.GetObject("mnuPatient_FindHealthPlan.Image"),System.Drawing.Image)
        Me.mnuPatient_FindHealthPlan.Name = "mnuPatient_FindHealthPlan"
        Me.mnuPatient_FindHealthPlan.Size = New System.Drawing.Size(287, 22)
        Me.mnuPatient_FindHealthPlan.Text = "Find Health Plan"
        '
        'mnu_ViewRecommendation
        '
        Me.mnu_ViewRecommendation.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnu_ViewRecommendation.Image = CType(resources.GetObject("mnu_ViewRecommendation.Image"),System.Drawing.Image)
        Me.mnu_ViewRecommendation.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.mnu_ViewRecommendation.Name = "mnu_ViewRecommendation"
        Me.mnu_ViewRecommendation.Size = New System.Drawing.Size(287, 22)
        Me.mnu_ViewRecommendation.Text = "Recommendations"
        '
        'mnu_UploadVideo
        '
        Me.mnu_UploadVideo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnu_UploadVideo.Image = CType(resources.GetObject("mnu_UploadVideo.Image"),System.Drawing.Image)
        Me.mnu_UploadVideo.Name = "mnu_UploadVideo"
        Me.mnu_UploadVideo.Size = New System.Drawing.Size(287, 22)
        Me.mnu_UploadVideo.Text = "Upload Video"
        '
        'mnu_ScanDocs
        '
        Me.mnu_ScanDocs.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnu_ScanDocs.Image = CType(resources.GetObject("mnu_ScanDocs.Image"),System.Drawing.Image)
        Me.mnu_ScanDocs.Name = "mnu_ScanDocs"
        Me.mnu_ScanDocs.Size = New System.Drawing.Size(287, 22)
        Me.mnu_ScanDocs.Text = "Scan Documents"
        '
        'mnu_ImplantableDevices
        '
        Me.mnu_ImplantableDevices.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnu_ImplantableDevices.Image = CType(resources.GetObject("mnu_ImplantableDevices.Image"),System.Drawing.Image)
        Me.mnu_ImplantableDevices.Name = "mnu_ImplantableDevices"
        Me.mnu_ImplantableDevices.Size = New System.Drawing.Size(287, 22)
        Me.mnu_ImplantableDevices.Text = "Implantable Devices"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(284, 6)
        '
        'mnu_ScreeningTools
        '
        Me.mnu_ScreeningTools.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.HOOSJRToolStripMenuItem, Me.KOOSJRKNEESurveyToolStripMenuItem, Me.HOOSTotalAssesmentToolStripMenuItem, Me.KOOSKNEESurveyToolStripMenuItem, Me.PROMIS10ToolStripMenuItem, Me.PROMIS29ToolStripMenuItem, Me.VETERANSRAND12ToolStripMenuItem, Me.VETERANSRAND36SurveyToolStripMenuItem, Me.PHQ2ToolStripMenuItem})
        Me.mnu_ScreeningTools.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnu_ScreeningTools.Image = CType(resources.GetObject("mnu_ScreeningTools.Image"),System.Drawing.Image)
        Me.mnu_ScreeningTools.Name = "mnu_ScreeningTools"
        Me.mnu_ScreeningTools.Size = New System.Drawing.Size(287, 22)
        Me.mnu_ScreeningTools.Text = "Screening Tools"
        '
        'HOOSJRToolStripMenuItem
        '
        Me.HOOSJRToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.HOOSJRToolStripMenuItem.Image = CType(resources.GetObject("HOOSJRToolStripMenuItem.Image"),System.Drawing.Image)
        Me.HOOSJRToolStripMenuItem.Name = "HOOSJRToolStripMenuItem"
        Me.HOOSJRToolStripMenuItem.Size = New System.Drawing.Size(208, 22)
        Me.HOOSJRToolStripMenuItem.Text = "HOOS, JR. HIP Survey"
        '
        'KOOSJRKNEESurveyToolStripMenuItem
        '
        Me.KOOSJRKNEESurveyToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.KOOSJRKNEESurveyToolStripMenuItem.Image = CType(resources.GetObject("KOOSJRKNEESurveyToolStripMenuItem.Image"),System.Drawing.Image)
        Me.KOOSJRKNEESurveyToolStripMenuItem.Name = "KOOSJRKNEESurveyToolStripMenuItem"
        Me.KOOSJRKNEESurveyToolStripMenuItem.Size = New System.Drawing.Size(208, 22)
        Me.KOOSJRKNEESurveyToolStripMenuItem.Text = "KOOS, JR. KNEE Survey"
        '
        'HOOSTotalAssesmentToolStripMenuItem
        '
        Me.HOOSTotalAssesmentToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.HOOSTotalAssesmentToolStripMenuItem.Image = CType(resources.GetObject("HOOSTotalAssesmentToolStripMenuItem.Image"),System.Drawing.Image)
        Me.HOOSTotalAssesmentToolStripMenuItem.Name = "HOOSTotalAssesmentToolStripMenuItem"
        Me.HOOSTotalAssesmentToolStripMenuItem.Size = New System.Drawing.Size(208, 22)
        Me.HOOSTotalAssesmentToolStripMenuItem.Text = "HOOS HIP Survey"
        '
        'KOOSKNEESurveyToolStripMenuItem
        '
        Me.KOOSKNEESurveyToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.KOOSKNEESurveyToolStripMenuItem.Image = CType(resources.GetObject("KOOSKNEESurveyToolStripMenuItem.Image"),System.Drawing.Image)
        Me.KOOSKNEESurveyToolStripMenuItem.Name = "KOOSKNEESurveyToolStripMenuItem"
        Me.KOOSKNEESurveyToolStripMenuItem.Size = New System.Drawing.Size(208, 22)
        Me.KOOSKNEESurveyToolStripMenuItem.Text = "KOOS KNEE Survey"
        '
        'PROMIS10ToolStripMenuItem
        '
        Me.PROMIS10ToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.PROMIS10ToolStripMenuItem.Image = CType(resources.GetObject("PROMIS10ToolStripMenuItem.Image"),System.Drawing.Image)
        Me.PROMIS10ToolStripMenuItem.Name = "PROMIS10ToolStripMenuItem"
        Me.PROMIS10ToolStripMenuItem.Size = New System.Drawing.Size(208, 22)
        Me.PROMIS10ToolStripMenuItem.Text = "PROMIS 10 Survey"
        '
        'PROMIS29ToolStripMenuItem
        '
        Me.PROMIS29ToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.PROMIS29ToolStripMenuItem.Image = CType(resources.GetObject("PROMIS29ToolStripMenuItem.Image"),System.Drawing.Image)
        Me.PROMIS29ToolStripMenuItem.Name = "PROMIS29ToolStripMenuItem"
        Me.PROMIS29ToolStripMenuItem.Size = New System.Drawing.Size(208, 22)
        Me.PROMIS29ToolStripMenuItem.Text = "PROMIS 29 Survey"
        '
        'VETERANSRAND12ToolStripMenuItem
        '
        Me.VETERANSRAND12ToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.VETERANSRAND12ToolStripMenuItem.Image = CType(resources.GetObject("VETERANSRAND12ToolStripMenuItem.Image"),System.Drawing.Image)
        Me.VETERANSRAND12ToolStripMenuItem.Name = "VETERANSRAND12ToolStripMenuItem"
        Me.VETERANSRAND12ToolStripMenuItem.Size = New System.Drawing.Size(208, 22)
        Me.VETERANSRAND12ToolStripMenuItem.Text = "VETERANS RAND 12 Survey"
        '
        'VETERANSRAND36SurveyToolStripMenuItem
        '
        Me.VETERANSRAND36SurveyToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.VETERANSRAND36SurveyToolStripMenuItem.Image = CType(resources.GetObject("VETERANSRAND36SurveyToolStripMenuItem.Image"),System.Drawing.Image)
        Me.VETERANSRAND36SurveyToolStripMenuItem.Name = "VETERANSRAND36SurveyToolStripMenuItem"
        Me.VETERANSRAND36SurveyToolStripMenuItem.Size = New System.Drawing.Size(208, 22)
        Me.VETERANSRAND36SurveyToolStripMenuItem.Text = "VETERANS RAND 36 Survey"
        '
        'PHQ2ToolStripMenuItem
        '
        Me.PHQ2ToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.PHQ2ToolStripMenuItem.Image = CType(resources.GetObject("PHQ2ToolStripMenuItem.Image"),System.Drawing.Image)
        Me.PHQ2ToolStripMenuItem.Name = "PHQ2ToolStripMenuItem"
        Me.PHQ2ToolStripMenuItem.Size = New System.Drawing.Size(208, 22)
        Me.PHQ2ToolStripMenuItem.Text = "PHQ2 Survey"
        '
        'mnu_BillingCharges
        '
        Me.mnu_BillingCharges.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.mnu_BillingCharges.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnu_BillingCharges.Image = CType(resources.GetObject("mnu_BillingCharges.Image"),System.Drawing.Image)
        Me.mnu_BillingCharges.Name = "mnu_BillingCharges"
        Me.mnu_BillingCharges.Size = New System.Drawing.Size(287, 22)
        Me.mnu_BillingCharges.Text = "Charges"
        '
        'mnu_BillingBatch
        '
        Me.mnu_BillingBatch.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.mnu_BillingBatch.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnu_BillingBatch.Image = CType(resources.GetObject("mnu_BillingBatch.Image"),System.Drawing.Image)
        Me.mnu_BillingBatch.Name = "mnu_BillingBatch"
        Me.mnu_BillingBatch.Size = New System.Drawing.Size(287, 22)
        Me.mnu_BillingBatch.Text = "Batch"
        '
        'mnu_BillingPayment
        '
        Me.mnu_BillingPayment.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.mnu_BillingPayment.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnu_BillingPayment.Image = CType(resources.GetObject("mnu_BillingPayment.Image"),System.Drawing.Image)
        Me.mnu_BillingPayment.Name = "mnu_BillingPayment"
        Me.mnu_BillingPayment.Size = New System.Drawing.Size(287, 22)
        Me.mnu_BillingPayment.Text = "Payment"
        '
        'mnu_BillingAdvPayment
        '
        Me.mnu_BillingAdvPayment.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnu_BillingAdvPayment.Image = CType(resources.GetObject("mnu_BillingAdvPayment.Image"),System.Drawing.Image)
        Me.mnu_BillingAdvPayment.Name = "mnu_BillingAdvPayment"
        Me.mnu_BillingAdvPayment.Size = New System.Drawing.Size(287, 22)
        Me.mnu_BillingAdvPayment.Text = "Advanced Payment"
        '
        'mnu_BillingRemittance
        '
        Me.mnu_BillingRemittance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.mnu_BillingRemittance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnu_BillingRemittance.Image = CType(resources.GetObject("mnu_BillingRemittance.Image"),System.Drawing.Image)
        Me.mnu_BillingRemittance.Name = "mnu_BillingRemittance"
        Me.mnu_BillingRemittance.Size = New System.Drawing.Size(287, 22)
        Me.mnu_BillingRemittance.Text = "Remittance"
        Me.mnu_BillingRemittance.Visible = false
        '
        'mnu_BillingBalance
        '
        Me.mnu_BillingBalance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.mnu_BillingBalance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnu_BillingBalance.Image = CType(resources.GetObject("mnu_BillingBalance.Image"),System.Drawing.Image)
        Me.mnu_BillingBalance.Name = "mnu_BillingBalance"
        Me.mnu_BillingBalance.Size = New System.Drawing.Size(287, 22)
        Me.mnu_BillingBalance.Text = "Balance"
        '
        'mnu_BillingLedger
        '
        Me.mnu_BillingLedger.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.mnu_BillingLedger.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnu_BillingLedger.Image = CType(resources.GetObject("mnu_BillingLedger.Image"),System.Drawing.Image)
        Me.mnu_BillingLedger.Name = "mnu_BillingLedger"
        Me.mnu_BillingLedger.Size = New System.Drawing.Size(287, 22)
        Me.mnu_BillingLedger.Text = "Ledger"
        '
        'mnu_Appointment
        '
        Me.mnu_Appointment.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnu_Appointment.Image = CType(resources.GetObject("mnu_Appointment.Image"),System.Drawing.Image)
        Me.mnu_Appointment.Name = "mnu_Appointment"
        Me.mnu_Appointment.Size = New System.Drawing.Size(287, 22)
        Me.mnu_Appointment.Text = "Appointment"
        '
        'mnu_Schedule
        '
        Me.mnu_Schedule.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnu_Schedule.Image = CType(resources.GetObject("mnu_Schedule.Image"),System.Drawing.Image)
        Me.mnu_Schedule.Name = "mnu_Schedule"
        Me.mnu_Schedule.Size = New System.Drawing.Size(287, 22)
        Me.mnu_Schedule.Text = "Schedule"
        '
        'mnu_ClosedJournals
        '
        Me.mnu_ClosedJournals.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnu_ClosedJournals.Image = CType(resources.GetObject("mnu_ClosedJournals.Image"),System.Drawing.Image)
        Me.mnu_ClosedJournals.Name = "mnu_ClosedJournals"
        Me.mnu_ClosedJournals.Size = New System.Drawing.Size(287, 22)
        Me.mnu_ClosedJournals.Text = "Payment Tray"
        '
        'mnuView_Triage1
        '
        Me.mnuView_Triage1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuView_Triage1.Image = CType(resources.GetObject("mnuView_Triage1.Image"),System.Drawing.Image)
        Me.mnuView_Triage1.Name = "mnuView_Triage1"
        Me.mnuView_Triage1.Size = New System.Drawing.Size(287, 22)
        Me.mnuView_Triage1.Text = "Triage"
        '
        'mnu_ClinicalChartPrintQueue
        '
        Me.mnu_ClinicalChartPrintQueue.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnu_ClinicalChartPrintQueue.Image = CType(resources.GetObject("mnu_ClinicalChartPrintQueue.Image"),System.Drawing.Image)
        Me.mnu_ClinicalChartPrintQueue.Name = "mnu_ClinicalChartPrintQueue"
        Me.mnu_ClinicalChartPrintQueue.Size = New System.Drawing.Size(287, 22)
        Me.mnu_ClinicalChartPrintQueue.Tag = "ClinicalChartPrintQueue"
        Me.mnu_ClinicalChartPrintQueue.Text = "Clinical Chart Print Queue"
        '
        'mnu_SocPsycBehobs
        '
        Me.mnu_SocPsycBehobs.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnu_SocPsycBehobs.Image = CType(resources.GetObject("mnu_SocPsycBehobs.Image"),System.Drawing.Image)
        Me.mnu_SocPsycBehobs.Name = "mnu_SocPsycBehobs"
        Me.mnu_SocPsycBehobs.Size = New System.Drawing.Size(287, 22)
        Me.mnu_SocPsycBehobs.Tag = "SocialPsychologicalBehavioralObservations"
        Me.mnu_SocPsycBehobs.Text = "Social Psychological Behavioral Observations"
        '
        'mnu_PortalPHI
        '
        Me.mnu_PortalPHI.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnu_PortalPHI.Image = CType(resources.GetObject("mnu_PortalPHI.Image"),System.Drawing.Image)
        Me.mnu_PortalPHI.Name = "mnu_PortalPHI"
        Me.mnu_PortalPHI.Size = New System.Drawing.Size(287, 22)
        Me.mnu_PortalPHI.Text = "Portal PHI"
        '
        'mnu_APIHarness
        '
        Me.mnu_APIHarness.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnu_APIHarness_Roles, Me.mnu_APIHarness_Users})
        Me.mnu_APIHarness.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnu_APIHarness.Image = CType(resources.GetObject("mnu_APIHarness.Image"),System.Drawing.Image)
        Me.mnu_APIHarness.Name = "mnu_APIHarness"
        Me.mnu_APIHarness.Size = New System.Drawing.Size(287, 22)
        Me.mnu_APIHarness.Text = "API Harness"
        Me.mnu_APIHarness.Visible = false
        '
        'mnu_APIHarness_Roles
        '
        Me.mnu_APIHarness_Roles.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnu_APIHarness_Roles.Image = CType(resources.GetObject("mnu_APIHarness_Roles.Image"),System.Drawing.Image)
        Me.mnu_APIHarness_Roles.Name = "mnu_APIHarness_Roles"
        Me.mnu_APIHarness_Roles.Size = New System.Drawing.Size(121, 22)
        Me.mnu_APIHarness_Roles.Text = "API Roles"
        '
        'mnu_APIHarness_Users
        '
        Me.mnu_APIHarness_Users.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnu_APIHarness_Users.Image = CType(resources.GetObject("mnu_APIHarness_Users.Image"),System.Drawing.Image)
        Me.mnu_APIHarness_Users.Name = "mnu_APIHarness_Users"
        Me.mnu_APIHarness_Users.Size = New System.Drawing.Size(121, 22)
        Me.mnu_APIHarness_Users.Text = "API Users"
        '
        'mnu_CCDAPatientConsent
        '
        Me.mnu_CCDAPatientConsent.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnu_CCDAPatientConsent.Image = CType(resources.GetObject("mnu_CCDAPatientConsent.Image"),System.Drawing.Image)
        Me.mnu_CCDAPatientConsent.Name = "mnu_CCDAPatientConsent"
        Me.mnu_CCDAPatientConsent.Size = New System.Drawing.Size(287, 22)
        Me.mnu_CCDAPatientConsent.Text = "Patient CCDA Consent"
        '
        'mnuView
        '
        Me.mnuView.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuView_PatientVitals, Me.mnuView_OBVitals, Me.mnuView_Tasks, Me.mnuView_Mails, Me.mnuView_Messages, Me.mnuView_PatientEducation, Me.mnuView_FormGallery, Me.mnuView_Referrals, Me.mnuView_PatientLetters, Me.mnuView_PTProtocol, Me.mnuView_PatientConcent, Me.mnuConsentTracking, Me.mnuView_NurseNotes, Me.mnuView_DisclosureMgmt, Me.mnuView_PatientVideo, Me.mnuViewReceivedFaxes, Me.mnuOutStandingOrders, Me.mnuViewPatientSummaryScreen, Me.ToolStripSeparator18, Me.mnuViewPrescriptions, Me.mnuRefillRequest, Me.mnuVwDeniedRefillReq, Me.ToolStripSeparator21, Me.mnuPendingRxChangeRequest, Me.mnuVwDeniedChangeReq, Me.mnuRxFillNotifications, Me.mnuVwErrorMessages, Me.ToolStripSeparator20, Me.mnu_ViewPendingLabOrders, Me.mnuDicomViewer, Me.mnu_ViewPatientSynopsis, Me.mnuView_PatientConfidential, Me.mnuView_CardioVascularRisk, Me.mnuView_PatientChiefComplaints, Me.mnuClinicalInstruction, Me.mnuCarePlan, Me.mnuView_Triage, Me.mnuViewCCDFiles, Me.mnuVwRECList, Me.mnuVwSummaryCareRecord, Me.mnuViewCDSFiles, Me.mnu_ViewDocument, Me.mnu_ViewSchedule, Me.mnuVwHL7MessageQueue, Me.mnuGeneralMessageQueue, Me.ViewScreenings, Me.MessageQueueToolStripMenuItem1, Me.mnuVwEARdata, Me.mnuViewAmendments, Me.mnuePARequests, Me.mnuPDRPrograms, Me.mnuPDMPPrograms})
        Me.mnuView.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuView.Name = "mnuView"
        Me.mnuView.Size = New System.Drawing.Size(41, 21)
        Me.mnuView.Text = "&View"
        '
        'mnuView_PatientVitals
        '
        Me.mnuView_PatientVitals.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuView_PatientVitals.Image = CType(resources.GetObject("mnuView_PatientVitals.Image"),System.Drawing.Image)
        Me.mnuView_PatientVitals.Name = "mnuView_PatientVitals"
        Me.mnuView_PatientVitals.Size = New System.Drawing.Size(264, 22)
        Me.mnuView_PatientVitals.Text = "Vitals"
        '
        'mnuView_OBVitals
        '
        Me.mnuView_OBVitals.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuView_OBVitals.Image = CType(resources.GetObject("mnuView_OBVitals.Image"),System.Drawing.Image)
        Me.mnuView_OBVitals.Name = "mnuView_OBVitals"
        Me.mnuView_OBVitals.Size = New System.Drawing.Size(264, 22)
        Me.mnuView_OBVitals.Text = "OB Vitals"
        '
        'mnuView_Tasks
        '
        Me.mnuView_Tasks.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuView_Tasks.Image = CType(resources.GetObject("mnuView_Tasks.Image"),System.Drawing.Image)
        Me.mnuView_Tasks.Name = "mnuView_Tasks"
        Me.mnuView_Tasks.Size = New System.Drawing.Size(264, 22)
        Me.mnuView_Tasks.Text = "Tasks"
        '
        'mnuView_Mails
        '
        Me.mnuView_Mails.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuView_Mails.Image = CType(resources.GetObject("mnuView_Mails.Image"),System.Drawing.Image)
        Me.mnuView_Mails.Name = "mnuView_Mails"
        Me.mnuView_Mails.Size = New System.Drawing.Size(264, 22)
        Me.mnuView_Mails.Text = "Mails"
        Me.mnuView_Mails.Visible = false
        '
        'mnuView_Messages
        '
        Me.mnuView_Messages.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuView_Messages.Image = CType(resources.GetObject("mnuView_Messages.Image"),System.Drawing.Image)
        Me.mnuView_Messages.Name = "mnuView_Messages"
        Me.mnuView_Messages.Size = New System.Drawing.Size(264, 22)
        Me.mnuView_Messages.Text = "Messages"
        '
        'mnuView_PatientEducation
        '
        Me.mnuView_PatientEducation.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuView_PatientEducation.Image = CType(resources.GetObject("mnuView_PatientEducation.Image"),System.Drawing.Image)
        Me.mnuView_PatientEducation.Name = "mnuView_PatientEducation"
        Me.mnuView_PatientEducation.Size = New System.Drawing.Size(264, 22)
        Me.mnuView_PatientEducation.Text = "Patient Education"
        '
        'mnuView_FormGallery
        '
        Me.mnuView_FormGallery.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuView_FormGallery.Image = CType(resources.GetObject("mnuView_FormGallery.Image"),System.Drawing.Image)
        Me.mnuView_FormGallery.Name = "mnuView_FormGallery"
        Me.mnuView_FormGallery.Size = New System.Drawing.Size(264, 22)
        Me.mnuView_FormGallery.Text = "Form Gallery"
        '
        'mnuView_Referrals
        '
        Me.mnuView_Referrals.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuView_Referrals.Image = CType(resources.GetObject("mnuView_Referrals.Image"),System.Drawing.Image)
        Me.mnuView_Referrals.Name = "mnuView_Referrals"
        Me.mnuView_Referrals.Size = New System.Drawing.Size(264, 22)
        Me.mnuView_Referrals.Text = "Referrals"
        '
        'mnuView_PatientLetters
        '
        Me.mnuView_PatientLetters.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuView_PatientLetters.Image = CType(resources.GetObject("mnuView_PatientLetters.Image"),System.Drawing.Image)
        Me.mnuView_PatientLetters.Name = "mnuView_PatientLetters"
        Me.mnuView_PatientLetters.Size = New System.Drawing.Size(264, 22)
        Me.mnuView_PatientLetters.Text = "Patient Letters"
        '
        'mnuView_PTProtocol
        '
        Me.mnuView_PTProtocol.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuView_PTProtocol.Image = CType(resources.GetObject("mnuView_PTProtocol.Image"),System.Drawing.Image)
        Me.mnuView_PTProtocol.Name = "mnuView_PTProtocol"
        Me.mnuView_PTProtocol.Size = New System.Drawing.Size(264, 22)
        Me.mnuView_PTProtocol.Text = "PT Protocol"
        '
        'mnuView_PatientConcent
        '
        Me.mnuView_PatientConcent.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuView_PatientConcent.Image = CType(resources.GetObject("mnuView_PatientConcent.Image"),System.Drawing.Image)
        Me.mnuView_PatientConcent.Name = "mnuView_PatientConcent"
        Me.mnuView_PatientConcent.Size = New System.Drawing.Size(264, 22)
        Me.mnuView_PatientConcent.Text = "Patient Consent"
        '
        'mnuConsentTracking
        '
        Me.mnuConsentTracking.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuConsentTracking.Image = CType(resources.GetObject("mnuConsentTracking.Image"),System.Drawing.Image)
        Me.mnuConsentTracking.Name = "mnuConsentTracking"
        Me.mnuConsentTracking.Size = New System.Drawing.Size(264, 22)
        Me.mnuConsentTracking.Text = "Patient Consent Tracking"
        '
        'mnuView_NurseNotes
        '
        Me.mnuView_NurseNotes.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuView_NurseNotes.Image = CType(resources.GetObject("mnuView_NurseNotes.Image"),System.Drawing.Image)
        Me.mnuView_NurseNotes.Name = "mnuView_NurseNotes"
        Me.mnuView_NurseNotes.Size = New System.Drawing.Size(264, 22)
        Me.mnuView_NurseNotes.Text = "Nurses Notes"
        '
        'mnuView_DisclosureMgmt
        '
        Me.mnuView_DisclosureMgmt.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuView_DisclosureMgmt.Image = CType(resources.GetObject("mnuView_DisclosureMgmt.Image"),System.Drawing.Image)
        Me.mnuView_DisclosureMgmt.Name = "mnuView_DisclosureMgmt"
        Me.mnuView_DisclosureMgmt.Size = New System.Drawing.Size(264, 22)
        Me.mnuView_DisclosureMgmt.Text = "Disclosure Management"
        '
        'mnuView_PatientVideo
        '
        Me.mnuView_PatientVideo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuView_PatientVideo.Image = CType(resources.GetObject("mnuView_PatientVideo.Image"),System.Drawing.Image)
        Me.mnuView_PatientVideo.Name = "mnuView_PatientVideo"
        Me.mnuView_PatientVideo.Size = New System.Drawing.Size(264, 22)
        Me.mnuView_PatientVideo.Text = "Patient Video"
        '
        'mnuViewReceivedFaxes
        '
        Me.mnuViewReceivedFaxes.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuViewReceivedFaxes.Image = CType(resources.GetObject("mnuViewReceivedFaxes.Image"),System.Drawing.Image)
        Me.mnuViewReceivedFaxes.Name = "mnuViewReceivedFaxes"
        Me.mnuViewReceivedFaxes.Size = New System.Drawing.Size(264, 22)
        Me.mnuViewReceivedFaxes.Text = "Received  Faxes"
        '
        'mnuOutStandingOrders
        '
        Me.mnuOutStandingOrders.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuOutStandingOrders.Image = CType(resources.GetObject("mnuOutStandingOrders.Image"),System.Drawing.Image)
        Me.mnuOutStandingOrders.Name = "mnuOutStandingOrders"
        Me.mnuOutStandingOrders.Size = New System.Drawing.Size(264, 22)
        Me.mnuOutStandingOrders.Text = "Outstanding Orders"
        '
        'mnuViewPatientSummaryScreen
        '
        Me.mnuViewPatientSummaryScreen.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuViewPatientSummaryScreen.Image = CType(resources.GetObject("mnuViewPatientSummaryScreen.Image"),System.Drawing.Image)
        Me.mnuViewPatientSummaryScreen.Name = "mnuViewPatientSummaryScreen"
        Me.mnuViewPatientSummaryScreen.Size = New System.Drawing.Size(264, 22)
        Me.mnuViewPatientSummaryScreen.Text = "Patient Summary"
        '
        'ToolStripSeparator18
        '
        Me.ToolStripSeparator18.Name = "ToolStripSeparator18"
        Me.ToolStripSeparator18.Size = New System.Drawing.Size(261, 6)
        '
        'mnuViewPrescriptions
        '
        Me.mnuViewPrescriptions.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuViewPrescriptions.Image = CType(resources.GetObject("mnuViewPrescriptions.Image"),System.Drawing.Image)
        Me.mnuViewPrescriptions.Name = "mnuViewPrescriptions"
        Me.mnuViewPrescriptions.Size = New System.Drawing.Size(264, 22)
        Me.mnuViewPrescriptions.Text = "Prescriptions"
        '
        'mnuRefillRequest
        '
        Me.mnuRefillRequest.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuRefillRequest.Image = CType(resources.GetObject("mnuRefillRequest.Image"),System.Drawing.Image)
        Me.mnuRefillRequest.Name = "mnuRefillRequest"
        Me.mnuRefillRequest.Size = New System.Drawing.Size(264, 22)
        Me.mnuRefillRequest.Text = "Pending Refill Request"
        '
        'mnuVwDeniedRefillReq
        '
        Me.mnuVwDeniedRefillReq.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuVwDeniedRefillReq.Image = CType(resources.GetObject("mnuVwDeniedRefillReq.Image"),System.Drawing.Image)
        Me.mnuVwDeniedRefillReq.Name = "mnuVwDeniedRefillReq"
        Me.mnuVwDeniedRefillReq.Size = New System.Drawing.Size(264, 22)
        Me.mnuVwDeniedRefillReq.Text = "Denied Refill Request"
        '
        'ToolStripSeparator21
        '
        Me.ToolStripSeparator21.Name = "ToolStripSeparator21"
        Me.ToolStripSeparator21.Size = New System.Drawing.Size(261, 6)
        '
        'mnuPendingRxChangeRequest
        '
        Me.mnuPendingRxChangeRequest.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuPendingRxChangeRequest.Image = CType(resources.GetObject("mnuPendingRxChangeRequest.Image"),System.Drawing.Image)
        Me.mnuPendingRxChangeRequest.Name = "mnuPendingRxChangeRequest"
        Me.mnuPendingRxChangeRequest.Size = New System.Drawing.Size(264, 22)
        Me.mnuPendingRxChangeRequest.Text = "Pending RxChange Request"
        '
        'mnuVwDeniedChangeReq
        '
        Me.mnuVwDeniedChangeReq.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuVwDeniedChangeReq.Image = CType(resources.GetObject("mnuVwDeniedChangeReq.Image"),System.Drawing.Image)
        Me.mnuVwDeniedChangeReq.Name = "mnuVwDeniedChangeReq"
        Me.mnuVwDeniedChangeReq.Size = New System.Drawing.Size(264, 22)
        Me.mnuVwDeniedChangeReq.Text = "Denied RxChange Request"
        '
        'mnuRxFillNotifications
        '
        Me.mnuRxFillNotifications.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuRxFillNotifications.Image = CType(resources.GetObject("mnuRxFillNotifications.Image"),System.Drawing.Image)
        Me.mnuRxFillNotifications.Name = "mnuRxFillNotifications"
        Me.mnuRxFillNotifications.Size = New System.Drawing.Size(264, 22)
        Me.mnuRxFillNotifications.Text = "RxFill Notifications"
        '
        'mnuVwErrorMessages
        '
        Me.mnuVwErrorMessages.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuVwErrorMessages.Image = CType(resources.GetObject("mnuVwErrorMessages.Image"),System.Drawing.Image)
        Me.mnuVwErrorMessages.Name = "mnuVwErrorMessages"
        Me.mnuVwErrorMessages.Size = New System.Drawing.Size(264, 22)
        Me.mnuVwErrorMessages.Text = "Error Messages "
        '
        'ToolStripSeparator20
        '
        Me.ToolStripSeparator20.Name = "ToolStripSeparator20"
        Me.ToolStripSeparator20.Size = New System.Drawing.Size(261, 6)
        '
        'mnu_ViewPendingLabOrders
        '
        Me.mnu_ViewPendingLabOrders.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnu_ViewPendingLabOrders.Image = CType(resources.GetObject("mnu_ViewPendingLabOrders.Image"),System.Drawing.Image)
        Me.mnu_ViewPendingLabOrders.Name = "mnu_ViewPendingLabOrders"
        Me.mnu_ViewPendingLabOrders.Size = New System.Drawing.Size(264, 22)
        Me.mnu_ViewPendingLabOrders.Text = "Pending Lab Order"
        '
        'mnuDicomViewer
        '
        Me.mnuDicomViewer.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuDicomViewer.Image = CType(resources.GetObject("mnuDicomViewer.Image"),System.Drawing.Image)
        Me.mnuDicomViewer.Name = "mnuDicomViewer"
        Me.mnuDicomViewer.Size = New System.Drawing.Size(264, 22)
        Me.mnuDicomViewer.Text = "DICOM"
        '
        'mnu_ViewPatientSynopsis
        '
        Me.mnu_ViewPatientSynopsis.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnu_ViewPatientSynopsis.Image = CType(resources.GetObject("mnu_ViewPatientSynopsis.Image"),System.Drawing.Image)
        Me.mnu_ViewPatientSynopsis.Name = "mnu_ViewPatientSynopsis"
        Me.mnu_ViewPatientSynopsis.Size = New System.Drawing.Size(264, 22)
        Me.mnu_ViewPatientSynopsis.Text = "Patient Synopsis"
        '
        'mnuView_PatientConfidential
        '
        Me.mnuView_PatientConfidential.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuView_PatientConfidential.Image = CType(resources.GetObject("mnuView_PatientConfidential.Image"),System.Drawing.Image)
        Me.mnuView_PatientConfidential.Name = "mnuView_PatientConfidential"
        Me.mnuView_PatientConfidential.Size = New System.Drawing.Size(264, 22)
        Me.mnuView_PatientConfidential.Text = "Patient Confidential"
        '
        'mnuView_CardioVascularRisk
        '
        Me.mnuView_CardioVascularRisk.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuView_CardioVascularRisk.Image = CType(resources.GetObject("mnuView_CardioVascularRisk.Image"),System.Drawing.Image)
        Me.mnuView_CardioVascularRisk.Name = "mnuView_CardioVascularRisk"
        Me.mnuView_CardioVascularRisk.Size = New System.Drawing.Size(264, 22)
        Me.mnuView_CardioVascularRisk.Text = "Cardiovascular Risk"
        '
        'mnuView_PatientChiefComplaints
        '
        Me.mnuView_PatientChiefComplaints.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuView_PatientChiefComplaints.Image = CType(resources.GetObject("mnuView_PatientChiefComplaints.Image"),System.Drawing.Image)
        Me.mnuView_PatientChiefComplaints.Name = "mnuView_PatientChiefComplaints"
        Me.mnuView_PatientChiefComplaints.Size = New System.Drawing.Size(264, 22)
        Me.mnuView_PatientChiefComplaints.Text = "Patient Chief Complaints"
        '
        'mnuClinicalInstruction
        '
        Me.mnuClinicalInstruction.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuClinicalInstruction.Image = Global.gloEMR.My.Resources.Resources.Patient_Specific
        Me.mnuClinicalInstruction.Name = "mnuClinicalInstruction"
        Me.mnuClinicalInstruction.Size = New System.Drawing.Size(264, 22)
        Me.mnuClinicalInstruction.Text = "Patient Clinical Instructions"
        '
        'mnuCarePlan
        '
        Me.mnuCarePlan.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuCarePlan.Image = CType(resources.GetObject("mnuCarePlan.Image"),System.Drawing.Image)
        Me.mnuCarePlan.Name = "mnuCarePlan"
        Me.mnuCarePlan.Size = New System.Drawing.Size(264, 22)
        Me.mnuCarePlan.Text = "Patient Care Plan"
        '
        'mnuView_Triage
        '
        Me.mnuView_Triage.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuView_Triage.Image = CType(resources.GetObject("mnuView_Triage.Image"),System.Drawing.Image)
        Me.mnuView_Triage.Name = "mnuView_Triage"
        Me.mnuView_Triage.Size = New System.Drawing.Size(264, 22)
        Me.mnuView_Triage.Text = "Triage"
        '
        'mnuViewCCDFiles
        '
        Me.mnuViewCCDFiles.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuViewCCDFiles.Image = CType(resources.GetObject("mnuViewCCDFiles.Image"),System.Drawing.Image)
        Me.mnuViewCCDFiles.Name = "mnuViewCCDFiles"
        Me.mnuViewCCDFiles.Size = New System.Drawing.Size(264, 22)
        Me.mnuViewCCDFiles.Text = "CDA Files"
        '
        'mnuVwRECList
        '
        Me.mnuVwRECList.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.mnuVwRECList.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuVwRECList.Image = CType(resources.GetObject("mnuVwRECList.Image"),System.Drawing.Image)
        Me.mnuVwRECList.Name = "mnuVwRECList"
        Me.mnuVwRECList.Size = New System.Drawing.Size(264, 22)
        Me.mnuVwRECList.Text = "Reconciliation List"
        '
        'mnuVwSummaryCareRecord
        '
        Me.mnuVwSummaryCareRecord.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.mnuVwSummaryCareRecord.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuVwSummaryCareRecord.Image = CType(resources.GetObject("mnuVwSummaryCareRecord.Image"),System.Drawing.Image)
        Me.mnuVwSummaryCareRecord.Name = "mnuVwSummaryCareRecord"
        Me.mnuVwSummaryCareRecord.Size = New System.Drawing.Size(264, 22)
        Me.mnuVwSummaryCareRecord.Text = "Summary Care Record"
        '
        'mnuViewCDSFiles
        '
        Me.mnuViewCDSFiles.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuViewCDSFiles.Image = CType(resources.GetObject("mnuViewCDSFiles.Image"),System.Drawing.Image)
        Me.mnuViewCDSFiles.Name = "mnuViewCDSFiles"
        Me.mnuViewCDSFiles.Size = New System.Drawing.Size(264, 22)
        Me.mnuViewCDSFiles.Text = "CDS Files"
        '
        'mnu_ViewDocument
        '
        Me.mnu_ViewDocument.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnu_ViewDocument.Image = CType(resources.GetObject("mnu_ViewDocument.Image"),System.Drawing.Image)
        Me.mnu_ViewDocument.Name = "mnu_ViewDocument"
        Me.mnu_ViewDocument.Size = New System.Drawing.Size(264, 22)
        Me.mnu_ViewDocument.Text = "Document"
        '
        'mnu_ViewSchedule
        '
        Me.mnu_ViewSchedule.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnu_ViewSchedule.Image = CType(resources.GetObject("mnu_ViewSchedule.Image"),System.Drawing.Image)
        Me.mnu_ViewSchedule.Name = "mnu_ViewSchedule"
        Me.mnu_ViewSchedule.Size = New System.Drawing.Size(264, 22)
        Me.mnu_ViewSchedule.Text = "Schedule"
        '
        'mnuVwHL7MessageQueue
        '
        Me.mnuVwHL7MessageQueue.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuVwHL7MessageQueue.Image = CType(resources.GetObject("mnuVwHL7MessageQueue.Image"),System.Drawing.Image)
        Me.mnuVwHL7MessageQueue.Name = "mnuVwHL7MessageQueue"
        Me.mnuVwHL7MessageQueue.Size = New System.Drawing.Size(264, 22)
        Me.mnuVwHL7MessageQueue.Text = "HL7 Message Queue"
        '
        'mnuGeneralMessageQueue
        '
        Me.mnuGeneralMessageQueue.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuGeneralMessageQueue.Image = CType(resources.GetObject("mnuGeneralMessageQueue.Image"),System.Drawing.Image)
        Me.mnuGeneralMessageQueue.Name = "mnuGeneralMessageQueue"
        Me.mnuGeneralMessageQueue.Size = New System.Drawing.Size(264, 22)
        Me.mnuGeneralMessageQueue.Text = "General Message Queue"
        '
        'ViewScreenings
        '
        Me.ViewScreenings.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.ViewScreenings.Image = CType(resources.GetObject("ViewScreenings.Image"),System.Drawing.Image)
        Me.ViewScreenings.Name = "ViewScreenings"
        Me.ViewScreenings.Size = New System.Drawing.Size(264, 22)
        Me.ViewScreenings.Text = "Screening Tools"
        '
        'MessageQueueToolStripMenuItem1
        '
        Me.MessageQueueToolStripMenuItem1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.MessageQueueToolStripMenuItem1.Image = CType(resources.GetObject("MessageQueueToolStripMenuItem1.Image"),System.Drawing.Image)
        Me.MessageQueueToolStripMenuItem1.Name = "MessageQueueToolStripMenuItem1"
        Me.MessageQueueToolStripMenuItem1.Size = New System.Drawing.Size(264, 22)
        Me.MessageQueueToolStripMenuItem1.Text = "Message Queue"
        '
        'mnuVwEARdata
        '
        Me.mnuVwEARdata.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuVwEARdata.Image = CType(resources.GetObject("mnuVwEARdata.Image"),System.Drawing.Image)
        Me.mnuVwEARdata.Name = "mnuVwEARdata"
        Me.mnuVwEARdata.Size = New System.Drawing.Size(264, 22)
        Me.mnuVwEARdata.Text = "View EAR Data"
        Me.mnuVwEARdata.Visible = false
        '
        'mnuViewAmendments
        '
        Me.mnuViewAmendments.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuViewAmendments.Image = CType(resources.GetObject("mnuViewAmendments.Image"),System.Drawing.Image)
        Me.mnuViewAmendments.Name = "mnuViewAmendments"
        Me.mnuViewAmendments.Size = New System.Drawing.Size(264, 22)
        Me.mnuViewAmendments.Text = "All Health Record Amendment Requests"
        '
        'mnuePARequests
        '
        Me.mnuePARequests.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuePARequests.Image = CType(resources.GetObject("mnuePARequests.Image"),System.Drawing.Image)
        Me.mnuePARequests.Name = "mnuePARequests"
        Me.mnuePARequests.Size = New System.Drawing.Size(264, 22)
        Me.mnuePARequests.Text = "ePA Requests"
        '
        'mnuPDRPrograms
        '
        Me.mnuPDRPrograms.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuPDRPrograms.Image = CType(resources.GetObject("mnuPDRPrograms.Image"),System.Drawing.Image)
        Me.mnuPDRPrograms.Name = "mnuPDRPrograms"
        Me.mnuPDRPrograms.Size = New System.Drawing.Size(264, 22)
        Me.mnuPDRPrograms.Text = "PDR Programs"
        '
        'mnuPDMPPrograms
        '
        Me.mnuPDMPPrograms.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuPDMPPrograms.Image = CType(resources.GetObject("mnuPDMPPrograms.Image"),System.Drawing.Image)
        Me.mnuPDMPPrograms.Name = "mnuPDMPPrograms"
        Me.mnuPDMPPrograms.Size = New System.Drawing.Size(264, 22)
        Me.mnuPDMPPrograms.Text = "PDMP Programs"
        '
        'mnuTools
        '
        Me.mnuTools.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuSettings, Me.mnuChangePass, Me.mnuTools_CardScanner, Me.mnuTools_RefreshDevicesPrinters, Me.mnuTools_CardImage, Me.mnuTools_DefaultDisplaySettings, Me.sep16, Me.mnuTools_Customization, Me.mnuTools_PrescriptionProviderAssociation, Me.sep17, Me.mnuToolsVoiceCenter, Me.mnuDoctorSpeakerConfiguration, Me.sep18, Me.mnuExportTemplates, Me.mnuImportTemplates, Me.mnuUpdateExistingTemplates, Me.mnuTool_UpdateOtherTemplates, Me.mnuTool_UpgradeTemplates, Me.sep19, Me.mnuMergePatRecords, Me.mnu_ClearPatientDocuments, Me.sep20, Me.mnuUnlockExams, Me.mnuImportVitalGraphData, Me.mnu_TimeSynchronization, Me.sep21, Me.mnuUpdateExam, Me.mnuUpdateTemplates, Me.sep22, Me.mnuImportCCD, Me.mnuGenerateCDA, Me.mnuGenerateCCD, Me.mnuToolsCDAPatientInfoStatus, Me.mnuTools_SendSecureMessage, Me.ToolStripMenuItem3, Me.mnuCustomLink, Me.ToolStripSeparator9, Me.mnuICDAnalysis, Me.tls_CodeGuide, Me.mnuToolsToolbar, Me.mnuToolsStatusbar, Me.mnuTools_RecoverExam, Me.mnuTools_DirectInbox, Me.mnuTools_SecureMsg, Me.mnuTools_ExportSummary, Me.mnuTools_CCDASchedule, Me.mnuTools_IntuitPatient, Me.mnuTools_QRDAImport, Me.mnuTools_ProviderEducation})
        Me.mnuTools.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuTools.Name = "mnuTools"
        Me.mnuTools.Size = New System.Drawing.Size(44, 21)
        Me.mnuTools.Text = "&Tools"
        '
        'mnuSettings
        '
        Me.mnuSettings.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuSettings.Image = CType(resources.GetObject("mnuSettings.Image"),System.Drawing.Image)
        Me.mnuSettings.Name = "mnuSettings"
        Me.mnuSettings.Size = New System.Drawing.Size(264, 22)
        Me.mnuSettings.Text = "Settings"
        '
        'mnuChangePass
        '
        Me.mnuChangePass.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuChangePass.Image = CType(resources.GetObject("mnuChangePass.Image"),System.Drawing.Image)
        Me.mnuChangePass.Name = "mnuChangePass"
        Me.mnuChangePass.Size = New System.Drawing.Size(264, 22)
        Me.mnuChangePass.Text = "Change Password"
        '
        'mnuTools_CardScanner
        '
        Me.mnuTools_CardScanner.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuTools_CardScanner.Image = CType(resources.GetObject("mnuTools_CardScanner.Image"),System.Drawing.Image)
        Me.mnuTools_CardScanner.Name = "mnuTools_CardScanner"
        Me.mnuTools_CardScanner.Size = New System.Drawing.Size(264, 22)
        Me.mnuTools_CardScanner.Tag = "CardScanner"
        Me.mnuTools_CardScanner.Text = "Scan Card"
        '
        'mnuTools_RefreshDevicesPrinters
        '
        Me.mnuTools_RefreshDevicesPrinters.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuTools_RefreshDevicesPrinters.Image = CType(resources.GetObject("mnuTools_RefreshDevicesPrinters.Image"),System.Drawing.Image)
        Me.mnuTools_RefreshDevicesPrinters.Name = "mnuTools_RefreshDevicesPrinters"
        Me.mnuTools_RefreshDevicesPrinters.Size = New System.Drawing.Size(264, 22)
        Me.mnuTools_RefreshDevicesPrinters.Tag = "CardScanner"
        Me.mnuTools_RefreshDevicesPrinters.Text = "Refresh Local Devices and Printers"
        '
        'mnuTools_CardImage
        '
        Me.mnuTools_CardImage.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuTools_CardImage.Image = CType(resources.GetObject("mnuTools_CardImage.Image"),System.Drawing.Image)
        Me.mnuTools_CardImage.Name = "mnuTools_CardImage"
        Me.mnuTools_CardImage.Size = New System.Drawing.Size(264, 22)
        Me.mnuTools_CardImage.Tag = "CardImage"
        Me.mnuTools_CardImage.Text = "Card Image"
        '
        'mnuTools_DefaultDisplaySettings
        '
        Me.mnuTools_DefaultDisplaySettings.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuTools_DefaultDisplaySettings.Image = CType(resources.GetObject("mnuTools_DefaultDisplaySettings.Image"),System.Drawing.Image)
        Me.mnuTools_DefaultDisplaySettings.Name = "mnuTools_DefaultDisplaySettings"
        Me.mnuTools_DefaultDisplaySettings.Size = New System.Drawing.Size(264, 22)
        Me.mnuTools_DefaultDisplaySettings.Text = "Load Default Display Settings"
        '
        'sep16
        '
        Me.sep16.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.sep16.Name = "sep16"
        Me.sep16.Size = New System.Drawing.Size(261, 6)
        '
        'mnuTools_Customization
        '
        Me.mnuTools_Customization.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuTools_Customization.Image = CType(resources.GetObject("mnuTools_Customization.Image"),System.Drawing.Image)
        Me.mnuTools_Customization.Name = "mnuTools_Customization"
        Me.mnuTools_Customization.Size = New System.Drawing.Size(264, 22)
        Me.mnuTools_Customization.Text = "Patient Control Customization"
        '
        'mnuTools_PrescriptionProviderAssociation
        '
        Me.mnuTools_PrescriptionProviderAssociation.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuTools_PrescriptionProviderAssociation.Image = CType(resources.GetObject("mnuTools_PrescriptionProviderAssociation.Image"),System.Drawing.Image)
        Me.mnuTools_PrescriptionProviderAssociation.Name = "mnuTools_PrescriptionProviderAssociation"
        Me.mnuTools_PrescriptionProviderAssociation.Size = New System.Drawing.Size(264, 22)
        Me.mnuTools_PrescriptionProviderAssociation.Text = "Prescription Provider Association"
        '
        'sep17
        '
        Me.sep17.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.sep17.Name = "sep17"
        Me.sep17.Size = New System.Drawing.Size(261, 6)
        '
        'mnuToolsVoiceCenter
        '
        Me.mnuToolsVoiceCenter.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuToolsVoiceCenter.Image = CType(resources.GetObject("mnuToolsVoiceCenter.Image"),System.Drawing.Image)
        Me.mnuToolsVoiceCenter.Name = "mnuToolsVoiceCenter"
        Me.mnuToolsVoiceCenter.Size = New System.Drawing.Size(264, 22)
        Me.mnuToolsVoiceCenter.Text = "Voice Center"
        '
        'mnuDoctorSpeakerConfiguration
        '
        Me.mnuDoctorSpeakerConfiguration.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuDoctorSpeakerConfiguration.Image = CType(resources.GetObject("mnuDoctorSpeakerConfiguration.Image"),System.Drawing.Image)
        Me.mnuDoctorSpeakerConfiguration.Name = "mnuDoctorSpeakerConfiguration"
        Me.mnuDoctorSpeakerConfiguration.Size = New System.Drawing.Size(264, 22)
        Me.mnuDoctorSpeakerConfiguration.Text = "Doctor Speaker Configuration"
        '
        'sep18
        '
        Me.sep18.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.sep18.Name = "sep18"
        Me.sep18.Size = New System.Drawing.Size(261, 6)
        '
        'mnuExportTemplates
        '
        Me.mnuExportTemplates.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuExportTemplates.Image = CType(resources.GetObject("mnuExportTemplates.Image"),System.Drawing.Image)
        Me.mnuExportTemplates.Name = "mnuExportTemplates"
        Me.mnuExportTemplates.Size = New System.Drawing.Size(264, 22)
        Me.mnuExportTemplates.Text = "Export Templates"
        '
        'mnuImportTemplates
        '
        Me.mnuImportTemplates.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuImportTemplates.Image = CType(resources.GetObject("mnuImportTemplates.Image"),System.Drawing.Image)
        Me.mnuImportTemplates.Name = "mnuImportTemplates"
        Me.mnuImportTemplates.Size = New System.Drawing.Size(264, 22)
        Me.mnuImportTemplates.Text = "Import Templates"
        '
        'mnuUpdateExistingTemplates
        '
        Me.mnuUpdateExistingTemplates.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuUpdateExistingTemplates.Image = CType(resources.GetObject("mnuUpdateExistingTemplates.Image"),System.Drawing.Image)
        Me.mnuUpdateExistingTemplates.Name = "mnuUpdateExistingTemplates"
        Me.mnuUpdateExistingTemplates.Size = New System.Drawing.Size(264, 22)
        Me.mnuUpdateExistingTemplates.Text = "Update Existing Templates"
        Me.mnuUpdateExistingTemplates.ToolTipText = "Upgrade templates from older version to latest version"
        '
        'mnuTool_UpdateOtherTemplates
        '
        Me.mnuTool_UpdateOtherTemplates.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuTool_UpdateOtherTemplates.Image = CType(resources.GetObject("mnuTool_UpdateOtherTemplates.Image"),System.Drawing.Image)
        Me.mnuTool_UpdateOtherTemplates.Name = "mnuTool_UpdateOtherTemplates"
        Me.mnuTool_UpdateOtherTemplates.Size = New System.Drawing.Size(264, 22)
        Me.mnuTool_UpdateOtherTemplates.Text = "Update Other Templates"
        Me.mnuTool_UpdateOtherTemplates.ToolTipText = "Update Templates from Other Systems"
        '
        'mnuTool_UpgradeTemplates
        '
        Me.mnuTool_UpgradeTemplates.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuTool_UpgradeTemplates.Image = CType(resources.GetObject("mnuTool_UpgradeTemplates.Image"),System.Drawing.Image)
        Me.mnuTool_UpgradeTemplates.Name = "mnuTool_UpgradeTemplates"
        Me.mnuTool_UpgradeTemplates.Size = New System.Drawing.Size(264, 22)
        Me.mnuTool_UpgradeTemplates.Text = "Upgrade Templates"
        Me.mnuTool_UpgradeTemplates.ToolTipText = "Upgrade Templates to Open XML Standard"
        '
        'sep19
        '
        Me.sep19.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.sep19.Name = "sep19"
        Me.sep19.Size = New System.Drawing.Size(261, 6)
        '
        'mnuMergePatRecords
        '
        Me.mnuMergePatRecords.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuMergePatRecords.Image = CType(resources.GetObject("mnuMergePatRecords.Image"),System.Drawing.Image)
        Me.mnuMergePatRecords.Name = "mnuMergePatRecords"
        Me.mnuMergePatRecords.Size = New System.Drawing.Size(264, 22)
        Me.mnuMergePatRecords.Text = "Merge Patient Records"
        Me.mnuMergePatRecords.Visible = false
        '
        'mnu_ClearPatientDocuments
        '
        Me.mnu_ClearPatientDocuments.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnu_ClearPatientDocuments.Image = CType(resources.GetObject("mnu_ClearPatientDocuments.Image"),System.Drawing.Image)
        Me.mnu_ClearPatientDocuments.Name = "mnu_ClearPatientDocuments"
        Me.mnu_ClearPatientDocuments.Size = New System.Drawing.Size(264, 22)
        Me.mnu_ClearPatientDocuments.Text = "Clear Patient Documents"
        Me.mnu_ClearPatientDocuments.Visible = false
        '
        'sep20
        '
        Me.sep20.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.sep20.Name = "sep20"
        Me.sep20.Size = New System.Drawing.Size(261, 6)
        Me.sep20.Visible = false
        '
        'mnuUnlockExams
        '
        Me.mnuUnlockExams.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuUnlockExams.Image = CType(resources.GetObject("mnuUnlockExams.Image"),System.Drawing.Image)
        Me.mnuUnlockExams.Name = "mnuUnlockExams"
        Me.mnuUnlockExams.Size = New System.Drawing.Size(264, 22)
        Me.mnuUnlockExams.Text = "Unlock Records"
        '
        'mnuImportVitalGraphData
        '
        Me.mnuImportVitalGraphData.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuImportVitalGraphData.Image = CType(resources.GetObject("mnuImportVitalGraphData.Image"),System.Drawing.Image)
        Me.mnuImportVitalGraphData.Name = "mnuImportVitalGraphData"
        Me.mnuImportVitalGraphData.Size = New System.Drawing.Size(264, 22)
        Me.mnuImportVitalGraphData.Text = "Import Vital Graph Data"
        '
        'mnu_TimeSynchronization
        '
        Me.mnu_TimeSynchronization.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnu_TimeSynchronization.Image = CType(resources.GetObject("mnu_TimeSynchronization.Image"),System.Drawing.Image)
        Me.mnu_TimeSynchronization.Name = "mnu_TimeSynchronization"
        Me.mnu_TimeSynchronization.Size = New System.Drawing.Size(264, 22)
        Me.mnu_TimeSynchronization.Text = "Synchronize Time "
        Me.mnu_TimeSynchronization.ToolTipText = "Synchronize the local system time with the Server time"
        '
        'sep21
        '
        Me.sep21.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.sep21.Name = "sep21"
        Me.sep21.Size = New System.Drawing.Size(261, 6)
        '
        'mnuUpdateExam
        '
        Me.mnuUpdateExam.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuUpdateExam.Image = CType(resources.GetObject("mnuUpdateExam.Image"),System.Drawing.Image)
        Me.mnuUpdateExam.Name = "mnuUpdateExam"
        Me.mnuUpdateExam.Size = New System.Drawing.Size(264, 22)
        Me.mnuUpdateExam.Text = "Update Unfinished Exams"
        Me.mnuUpdateExam.Visible = false
        '
        'mnuUpdateTemplates
        '
        Me.mnuUpdateTemplates.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuUpdateTemplates.Image = CType(resources.GetObject("mnuUpdateTemplates.Image"),System.Drawing.Image)
        Me.mnuUpdateTemplates.Name = "mnuUpdateTemplates"
        Me.mnuUpdateTemplates.Size = New System.Drawing.Size(264, 22)
        Me.mnuUpdateTemplates.Text = "Update Templates"
        Me.mnuUpdateTemplates.Visible = false
        '
        'sep22
        '
        Me.sep22.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.sep22.Name = "sep22"
        Me.sep22.Size = New System.Drawing.Size(261, 6)
        Me.sep22.Visible = false
        '
        'mnuImportCCD
        '
        Me.mnuImportCCD.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuImportCCD.Image = CType(resources.GetObject("mnuImportCCD.Image"),System.Drawing.Image)
        Me.mnuImportCCD.Name = "mnuImportCCD"
        Me.mnuImportCCD.Size = New System.Drawing.Size(264, 22)
        Me.mnuImportCCD.Text = "CDA Files"
        '
        'mnuGenerateCDA
        '
        Me.mnuGenerateCDA.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuGenerateCDA.Image = Global.gloEMR.My.Resources.Resources.Generate_CDA1
        Me.mnuGenerateCDA.Name = "mnuGenerateCDA"
        Me.mnuGenerateCDA.Size = New System.Drawing.Size(264, 22)
        Me.mnuGenerateCDA.Text = "Generate CDA"
        '
        'mnuGenerateCCD
        '
        Me.mnuGenerateCCD.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuGenerateCCD.Image = CType(resources.GetObject("mnuGenerateCCD.Image"),System.Drawing.Image)
        Me.mnuGenerateCCD.Name = "mnuGenerateCCD"
        Me.mnuGenerateCCD.Size = New System.Drawing.Size(264, 22)
        Me.mnuGenerateCCD.Text = "Generate CCD (old)"
        '
        'mnuToolsCDAPatientInfoStatus
        '
        Me.mnuToolsCDAPatientInfoStatus.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuToolsCDAPatientInfoStatus.Image = CType(resources.GetObject("mnuToolsCDAPatientInfoStatus.Image"),System.Drawing.Image)
        Me.mnuToolsCDAPatientInfoStatus.Name = "mnuToolsCDAPatientInfoStatus"
        Me.mnuToolsCDAPatientInfoStatus.Size = New System.Drawing.Size(264, 22)
        Me.mnuToolsCDAPatientInfoStatus.Text = "CDA Patient Info. Status"
        '
        'mnuTools_SendSecureMessage
        '
        Me.mnuTools_SendSecureMessage.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuTools_SendSecureMessage.Image = CType(resources.GetObject("mnuTools_SendSecureMessage.Image"),System.Drawing.Image)
        Me.mnuTools_SendSecureMessage.Name = "mnuTools_SendSecureMessage"
        Me.mnuTools_SendSecureMessage.Size = New System.Drawing.Size(264, 22)
        Me.mnuTools_SendSecureMessage.Text = "Send Secure Message to Patient"
        '
        'ToolStripMenuItem3
        '
        Me.ToolStripMenuItem3.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.HealthVaultSendRequestAccess, Me.HealthVaultToolStripMenuItem})
        Me.ToolStripMenuItem3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.ToolStripMenuItem3.Image = CType(resources.GetObject("ToolStripMenuItem3.Image"),System.Drawing.Image)
        Me.ToolStripMenuItem3.Name = "ToolStripMenuItem3"
        Me.ToolStripMenuItem3.Size = New System.Drawing.Size(264, 22)
        Me.ToolStripMenuItem3.Text = "Connect with Microsoft HealthVault"
        Me.ToolStripMenuItem3.ToolTipText = "Connect with Microsoft HealthVault"
        '
        'HealthVaultSendRequestAccess
        '
        Me.HealthVaultSendRequestAccess.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.HealthVaultSendRequestAccess.Image = CType(resources.GetObject("HealthVaultSendRequestAccess.Image"),System.Drawing.Image)
        Me.HealthVaultSendRequestAccess.Name = "HealthVaultSendRequestAccess"
        Me.HealthVaultSendRequestAccess.Size = New System.Drawing.Size(200, 22)
        Me.HealthVaultSendRequestAccess.Text = "Request Access to Patient"
        Me.HealthVaultSendRequestAccess.ToolTipText = "Request Access to Patient"
        '
        'HealthVaultToolStripMenuItem
        '
        Me.HealthVaultToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.HealthVaultToolStripMenuItem.Image = CType(resources.GetObject("HealthVaultToolStripMenuItem.Image"),System.Drawing.Image)
        Me.HealthVaultToolStripMenuItem.Name = "HealthVaultToolStripMenuItem"
        Me.HealthVaultToolStripMenuItem.Size = New System.Drawing.Size(200, 22)
        Me.HealthVaultToolStripMenuItem.Text = "Update Patient Record"
        Me.HealthVaultToolStripMenuItem.ToolTipText = "Update Patient Record"
        '
        'mnuCustomLink
        '
        Me.mnuCustomLink.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuCustomLink.Image = CType(resources.GetObject("mnuCustomLink.Image"),System.Drawing.Image)
        Me.mnuCustomLink.Name = "mnuCustomLink"
        Me.mnuCustomLink.Size = New System.Drawing.Size(264, 22)
        Me.mnuCustomLink.Text = "Custom Links"
        Me.mnuCustomLink.ToolTipText = "Custom Links"
        '
        'ToolStripSeparator9
        '
        Me.ToolStripSeparator9.Name = "ToolStripSeparator9"
        Me.ToolStripSeparator9.Size = New System.Drawing.Size(261, 6)
        '
        'mnuICDAnalysis
        '
        Me.mnuICDAnalysis.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuICDAnalysis.Image = CType(resources.GetObject("mnuICDAnalysis.Image"),System.Drawing.Image)
        Me.mnuICDAnalysis.Name = "mnuICDAnalysis"
        Me.mnuICDAnalysis.Size = New System.Drawing.Size(264, 22)
        Me.mnuICDAnalysis.Text = "ICD9 Usage and ICD10 Mapping Report"
        '
        'tls_CodeGuide
        '
        Me.tls_CodeGuide.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.tls_CodeGuide.Image = CType(resources.GetObject("tls_CodeGuide.Image"),System.Drawing.Image)
        Me.tls_CodeGuide.Name = "tls_CodeGuide"
        Me.tls_CodeGuide.Size = New System.Drawing.Size(264, 22)
        Me.tls_CodeGuide.Text = "Code Guide"
        '
        'mnuToolsToolbar
        '
        Me.mnuToolsToolbar.Checked = true
        Me.mnuToolsToolbar.CheckState = System.Windows.Forms.CheckState.Checked
        Me.mnuToolsToolbar.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuToolsToolbar.Name = "mnuToolsToolbar"
        Me.mnuToolsToolbar.Size = New System.Drawing.Size(264, 22)
        Me.mnuToolsToolbar.Text = "Toolbar"
        '
        'mnuToolsStatusbar
        '
        Me.mnuToolsStatusbar.Checked = true
        Me.mnuToolsStatusbar.CheckState = System.Windows.Forms.CheckState.Checked
        Me.mnuToolsStatusbar.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuToolsStatusbar.Name = "mnuToolsStatusbar"
        Me.mnuToolsStatusbar.Size = New System.Drawing.Size(264, 22)
        Me.mnuToolsStatusbar.Text = "Statusbar"
        '
        'mnuTools_RecoverExam
        '
        Me.mnuTools_RecoverExam.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuTools_RecoverExam.Image = CType(resources.GetObject("mnuTools_RecoverExam.Image"),System.Drawing.Image)
        Me.mnuTools_RecoverExam.Name = "mnuTools_RecoverExam"
        Me.mnuTools_RecoverExam.Size = New System.Drawing.Size(264, 22)
        Me.mnuTools_RecoverExam.Text = "Recover Exam"
        '
        'mnuTools_DirectInbox
        '
        Me.mnuTools_DirectInbox.Checked = true
        Me.mnuTools_DirectInbox.CheckState = System.Windows.Forms.CheckState.Checked
        Me.mnuTools_DirectInbox.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuTools_DirectInbox.Image = Global.gloEMR.My.Resources.Resources.Direct_mail_Inbox
        Me.mnuTools_DirectInbox.Name = "mnuTools_DirectInbox"
        Me.mnuTools_DirectInbox.Size = New System.Drawing.Size(264, 22)
        Me.mnuTools_DirectInbox.Text = "DIRECT Inbox"
        Me.mnuTools_DirectInbox.Visible = false
        '
        'mnuTools_SecureMsg
        '
        Me.mnuTools_SecureMsg.Checked = true
        Me.mnuTools_SecureMsg.CheckState = System.Windows.Forms.CheckState.Checked
        Me.mnuTools_SecureMsg.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuTools_SecureMsg.Image = Global.gloEMR.My.Resources.Resources.Inbox
        Me.mnuTools_SecureMsg.Name = "mnuTools_SecureMsg"
        Me.mnuTools_SecureMsg.Size = New System.Drawing.Size(264, 22)
        Me.mnuTools_SecureMsg.Text = "Provider DIRECT Message Inbox"
        '
        'mnuTools_ExportSummary
        '
        Me.mnuTools_ExportSummary.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuTools_ExportSummary.Image = CType(resources.GetObject("mnuTools_ExportSummary.Image"),System.Drawing.Image)
        Me.mnuTools_ExportSummary.Name = "mnuTools_ExportSummary"
        Me.mnuTools_ExportSummary.Size = New System.Drawing.Size(264, 22)
        Me.mnuTools_ExportSummary.Text = "Export Summary"
        '
        'mnuTools_CCDASchedule
        '
        Me.mnuTools_CCDASchedule.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuTools_CCDASchedule.Image = CType(resources.GetObject("mnuTools_CCDASchedule.Image"),System.Drawing.Image)
        Me.mnuTools_CCDASchedule.Name = "mnuTools_CCDASchedule"
        Me.mnuTools_CCDASchedule.Size = New System.Drawing.Size(264, 22)
        Me.mnuTools_CCDASchedule.Text = "CCDA Schedule"
        '
        'mnuTools_IntuitPatient
        '
        Me.mnuTools_IntuitPatient.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuTools_IntuitPatient.Image = CType(resources.GetObject("mnuTools_IntuitPatient.Image"),System.Drawing.Image)
        Me.mnuTools_IntuitPatient.Name = "mnuTools_IntuitPatient"
        Me.mnuTools_IntuitPatient.Size = New System.Drawing.Size(264, 22)
        Me.mnuTools_IntuitPatient.Text = "Download Intuit Forms"
        '
        'mnuTools_QRDAImport
        '
        Me.mnuTools_QRDAImport.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuTools_QRDAImport.Image = CType(resources.GetObject("mnuTools_QRDAImport.Image"),System.Drawing.Image)
        Me.mnuTools_QRDAImport.Name = "mnuTools_QRDAImport"
        Me.mnuTools_QRDAImport.Size = New System.Drawing.Size(264, 22)
        Me.mnuTools_QRDAImport.Text = "QRDA Import"
        '
        'mnuTools_ProviderEducation
        '
        Me.mnuTools_ProviderEducation.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuTools_ProviderEducation.Image = CType(resources.GetObject("mnuTools_ProviderEducation.Image"),System.Drawing.Image)
        Me.mnuTools_ProviderEducation.Name = "mnuTools_ProviderEducation"
        Me.mnuTools_ProviderEducation.Size = New System.Drawing.Size(264, 22)
        Me.mnuTools_ProviderEducation.Text = "Provider Education"
        Me.mnuTools_ProviderEducation.Visible = false
        '
        'mnuReports
        '
        Me.mnuReports.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuReportUnfinishedExam, Me.mnuSep_UnfinishedExam, Me.mnu_MISReports, Me.mnuFAXReports, Me.mnuReportExamPrintFAX, Me.mnuReviewExams, Me.mnuBatchReferrals, Me.mnuBatchPrintTemplates, Me.MnuClinicalChart, Me.MnuExamFinishReport, Me.ToolStripMenuItem2, Me.mnu_rpt_Appointments, Me.mnu_rpt_ConfirmAppointments, Me.mnu_rpt_NoShowAppointments, Me.mnu_rpt_CensusAppointments, Me.mnu_MIPS, Me.ToolStripSeparator17, Me.mnuMUDashboardMainMenu, Me.QualityMeasureDashboardToolStripMenuItem, Me.MedicaidCensusReportDashboardToolStripMenuItem, Me.sep23, Me.mnuPatientDemographics, Me.mnurptHCFAReport, Me.mnuRptDignosisLabResult, Me.mnuRpt_HealthPlan, Me.mnuRpt_OpenRecommendations, Me.sep24, Me.mnuViewAllCCDCCRFiles, Me.mnuUnfinishedReconciliationLists, Me.mnuUnfinishedFiles, Me.mnu_DM_Rpt_DueGuideline, Me.mnuRptIMDueReport, Me.mnuRpt_VaccineInventoryReport, Me.mnuRpt_LabGraph, Me.mnuRpt_PatientReminderLetters, Me.mnurpt_PatientICD9CPT, Me.CCHIT11ReportToolStripMenuItem, Me.CustomizableReportsToolStripMenuItem, Me.mnuRpt_LabManifest, Me.InterfaceReportsToolStripMenuItem, Me.ReviewIntuitPatientsToolStripMenuItem, Me.PatientActivationReportsToolStripMenuItem, Me.mnuReports_InactiveCPTSReport, Me.mnuReports_DrugMigrationReport, Me.mnuReports_PortalPHIReview, Me.mnu_APIHarness_Reports})
        Me.mnuReports.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuReports.Name = "mnuReports"
        Me.mnuReports.Size = New System.Drawing.Size(57, 21)
        Me.mnuReports.Text = "&Reports"
        '
        'mnuReportUnfinishedExam
        '
        Me.mnuReportUnfinishedExam.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuReportUnfinishedExam.Image = CType(resources.GetObject("mnuReportUnfinishedExam.Image"),System.Drawing.Image)
        Me.mnuReportUnfinishedExam.Name = "mnuReportUnfinishedExam"
        Me.mnuReportUnfinishedExam.Size = New System.Drawing.Size(230, 22)
        Me.mnuReportUnfinishedExam.Text = "Unfinished Exams"
        '
        'mnuSep_UnfinishedExam
        '
        Me.mnuSep_UnfinishedExam.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuSep_UnfinishedExam.Name = "mnuSep_UnfinishedExam"
        Me.mnuSep_UnfinishedExam.Size = New System.Drawing.Size(227, 6)
        '
        'mnu_MISReports
        '
        Me.mnu_MISReports.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnu_MISReports_PatientPaymentHistory, Me.mnu_MISReports_PatientTransactionHistory, Me.mnu_MISReports_Patientstatement, Me.ToolStripSeparator12, Me.mnu_MISReports_ProductionByDoctor, Me.mnu_MISReports_ProductionByFacility, Me.mnu_MISReports_ProductionByDate, Me.mnu_MISReports_ProductionByMonth, Me.mnu_MISReports_ProductionByProcedureGroup, Me.mnu_MISReports_ProductionByProcedureCode, Me.mnu_MISReports_ProductionByInsuranceCarrier, Me.mnu_MISReports_ProductionByFacilityByPatient, Me.mnu_MISReports_ProductionByFacilityByPatientDetail, Me.toolStripSeparator16, Me.mnu_MISReports_ReimbursementByMonth, Me.mnu_MISReports_ReimbursementByMonthDetail, Me.mnu_MISReports_ReimbursementByInsuranceCarrier, Me.mnu_MISReports_ReimbursementByInsuranceByCPT, Me.mnu_MISReports_ReimbursementByCPTByInsurance, Me.mnu_MISReports_ReimbursementByDoctorByInsurance, Me.mnu_MISReports_ReimbursementByInsuranceForCPT, Me.mnu_MISReports_ReimbursementDetailsByAccount, Me.toolStripSeparator26, Me.mnu_MISReports_AgingAnalysis, Me.mnu_MISReports_AgingSummaryByPatient, Me.mnu_MISReports_AgingSummaryByInsuranceCarrier, Me.toolStripSeparator27, Me.mnu_MISReports_ProductionByPhysicianGroup, Me.mnu_MISReports_ProductionAnalysisByFacility, Me.mnu_MISReports_ProductionAnalysisByprocedureGroup, Me.mnu_MISReports_ProductionAnalysisandTrendsByMonth, Me.mnu_MISReports_ProductionTrendsByProcedureGrop})
        Me.mnu_MISReports.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnu_MISReports.Image = CType(resources.GetObject("mnu_MISReports.Image"),System.Drawing.Image)
        Me.mnu_MISReports.Name = "mnu_MISReports"
        Me.mnu_MISReports.Size = New System.Drawing.Size(230, 22)
        Me.mnu_MISReports.Text = "MIS Reports"
        '
        'mnu_MISReports_PatientPaymentHistory
        '
        Me.mnu_MISReports_PatientPaymentHistory.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnu_MISReports_PatientPaymentHistory.Image = CType(resources.GetObject("mnu_MISReports_PatientPaymentHistory.Image"),System.Drawing.Image)
        Me.mnu_MISReports_PatientPaymentHistory.Name = "mnu_MISReports_PatientPaymentHistory"
        Me.mnu_MISReports_PatientPaymentHistory.Size = New System.Drawing.Size(319, 22)
        Me.mnu_MISReports_PatientPaymentHistory.Text = "Patient Payment History"
        '
        'mnu_MISReports_PatientTransactionHistory
        '
        Me.mnu_MISReports_PatientTransactionHistory.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnu_MISReports_PatientTransactionHistory.Image = CType(resources.GetObject("mnu_MISReports_PatientTransactionHistory.Image"),System.Drawing.Image)
        Me.mnu_MISReports_PatientTransactionHistory.Name = "mnu_MISReports_PatientTransactionHistory"
        Me.mnu_MISReports_PatientTransactionHistory.Size = New System.Drawing.Size(319, 22)
        Me.mnu_MISReports_PatientTransactionHistory.Text = "Patient Transaction History"
        '
        'mnu_MISReports_Patientstatement
        '
        Me.mnu_MISReports_Patientstatement.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnu_MISReports_Patientstatement.Image = CType(resources.GetObject("mnu_MISReports_Patientstatement.Image"),System.Drawing.Image)
        Me.mnu_MISReports_Patientstatement.Name = "mnu_MISReports_Patientstatement"
        Me.mnu_MISReports_Patientstatement.Size = New System.Drawing.Size(319, 22)
        Me.mnu_MISReports_Patientstatement.Text = "Patient Statement"
        '
        'ToolStripSeparator12
        '
        Me.ToolStripSeparator12.Name = "ToolStripSeparator12"
        Me.ToolStripSeparator12.Size = New System.Drawing.Size(316, 6)
        '
        'mnu_MISReports_ProductionByDoctor
        '
        Me.mnu_MISReports_ProductionByDoctor.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnu_MISReports_ProductionByDoctor.Image = CType(resources.GetObject("mnu_MISReports_ProductionByDoctor.Image"),System.Drawing.Image)
        Me.mnu_MISReports_ProductionByDoctor.Name = "mnu_MISReports_ProductionByDoctor"
        Me.mnu_MISReports_ProductionByDoctor.Size = New System.Drawing.Size(319, 22)
        Me.mnu_MISReports_ProductionByDoctor.Text = "Production By Doctor"
        '
        'mnu_MISReports_ProductionByFacility
        '
        Me.mnu_MISReports_ProductionByFacility.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnu_MISReports_ProductionByFacility.Image = CType(resources.GetObject("mnu_MISReports_ProductionByFacility.Image"),System.Drawing.Image)
        Me.mnu_MISReports_ProductionByFacility.Name = "mnu_MISReports_ProductionByFacility"
        Me.mnu_MISReports_ProductionByFacility.Size = New System.Drawing.Size(319, 22)
        Me.mnu_MISReports_ProductionByFacility.Text = "Production By Facility"
        '
        'mnu_MISReports_ProductionByDate
        '
        Me.mnu_MISReports_ProductionByDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnu_MISReports_ProductionByDate.Image = CType(resources.GetObject("mnu_MISReports_ProductionByDate.Image"),System.Drawing.Image)
        Me.mnu_MISReports_ProductionByDate.Name = "mnu_MISReports_ProductionByDate"
        Me.mnu_MISReports_ProductionByDate.Size = New System.Drawing.Size(319, 22)
        Me.mnu_MISReports_ProductionByDate.Text = "Production By Date"
        '
        'mnu_MISReports_ProductionByMonth
        '
        Me.mnu_MISReports_ProductionByMonth.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnu_MISReports_ProductionByMonth.Image = CType(resources.GetObject("mnu_MISReports_ProductionByMonth.Image"),System.Drawing.Image)
        Me.mnu_MISReports_ProductionByMonth.Name = "mnu_MISReports_ProductionByMonth"
        Me.mnu_MISReports_ProductionByMonth.Size = New System.Drawing.Size(319, 22)
        Me.mnu_MISReports_ProductionByMonth.Text = "Production By Month"
        '
        'mnu_MISReports_ProductionByProcedureGroup
        '
        Me.mnu_MISReports_ProductionByProcedureGroup.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnu_MISReports_ProductionByProcedureGroup.Image = CType(resources.GetObject("mnu_MISReports_ProductionByProcedureGroup.Image"),System.Drawing.Image)
        Me.mnu_MISReports_ProductionByProcedureGroup.Name = "mnu_MISReports_ProductionByProcedureGroup"
        Me.mnu_MISReports_ProductionByProcedureGroup.Size = New System.Drawing.Size(319, 22)
        Me.mnu_MISReports_ProductionByProcedureGroup.Text = "Production By Procedure Group"
        '
        'mnu_MISReports_ProductionByProcedureCode
        '
        Me.mnu_MISReports_ProductionByProcedureCode.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnu_MISReports_ProductionByProcedureCode.Image = CType(resources.GetObject("mnu_MISReports_ProductionByProcedureCode.Image"),System.Drawing.Image)
        Me.mnu_MISReports_ProductionByProcedureCode.Name = "mnu_MISReports_ProductionByProcedureCode"
        Me.mnu_MISReports_ProductionByProcedureCode.Size = New System.Drawing.Size(319, 22)
        Me.mnu_MISReports_ProductionByProcedureCode.Text = "Production By Procedure Code"
        '
        'mnu_MISReports_ProductionByInsuranceCarrier
        '
        Me.mnu_MISReports_ProductionByInsuranceCarrier.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnu_MISReports_ProductionByInsuranceCarrier.Image = CType(resources.GetObject("mnu_MISReports_ProductionByInsuranceCarrier.Image"),System.Drawing.Image)
        Me.mnu_MISReports_ProductionByInsuranceCarrier.Name = "mnu_MISReports_ProductionByInsuranceCarrier"
        Me.mnu_MISReports_ProductionByInsuranceCarrier.Size = New System.Drawing.Size(319, 22)
        Me.mnu_MISReports_ProductionByInsuranceCarrier.Text = "Production By Insurance Carrier"
        '
        'mnu_MISReports_ProductionByFacilityByPatient
        '
        Me.mnu_MISReports_ProductionByFacilityByPatient.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnu_MISReports_ProductionByFacilityByPatient.Image = CType(resources.GetObject("mnu_MISReports_ProductionByFacilityByPatient.Image"),System.Drawing.Image)
        Me.mnu_MISReports_ProductionByFacilityByPatient.Name = "mnu_MISReports_ProductionByFacilityByPatient"
        Me.mnu_MISReports_ProductionByFacilityByPatient.Size = New System.Drawing.Size(319, 22)
        Me.mnu_MISReports_ProductionByFacilityByPatient.Text = "Production By Facility By Patient - Summary"
        '
        'mnu_MISReports_ProductionByFacilityByPatientDetail
        '
        Me.mnu_MISReports_ProductionByFacilityByPatientDetail.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnu_MISReports_ProductionByFacilityByPatientDetail.Image = CType(resources.GetObject("mnu_MISReports_ProductionByFacilityByPatientDetail.Image"),System.Drawing.Image)
        Me.mnu_MISReports_ProductionByFacilityByPatientDetail.Name = "mnu_MISReports_ProductionByFacilityByPatientDetail"
        Me.mnu_MISReports_ProductionByFacilityByPatientDetail.Size = New System.Drawing.Size(319, 22)
        Me.mnu_MISReports_ProductionByFacilityByPatientDetail.Text = "Production By Facility By Patient - Detail"
        '
        'toolStripSeparator16
        '
        Me.toolStripSeparator16.Name = "toolStripSeparator16"
        Me.toolStripSeparator16.Size = New System.Drawing.Size(316, 6)
        '
        'mnu_MISReports_ReimbursementByMonth
        '
        Me.mnu_MISReports_ReimbursementByMonth.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnu_MISReports_ReimbursementByMonth.Image = CType(resources.GetObject("mnu_MISReports_ReimbursementByMonth.Image"),System.Drawing.Image)
        Me.mnu_MISReports_ReimbursementByMonth.Name = "mnu_MISReports_ReimbursementByMonth"
        Me.mnu_MISReports_ReimbursementByMonth.Size = New System.Drawing.Size(319, 22)
        Me.mnu_MISReports_ReimbursementByMonth.Text = "Reimbursement By Month"
        '
        'mnu_MISReports_ReimbursementByMonthDetail
        '
        Me.mnu_MISReports_ReimbursementByMonthDetail.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnu_MISReports_ReimbursementByMonthDetail.Image = CType(resources.GetObject("mnu_MISReports_ReimbursementByMonthDetail.Image"),System.Drawing.Image)
        Me.mnu_MISReports_ReimbursementByMonthDetail.Name = "mnu_MISReports_ReimbursementByMonthDetail"
        Me.mnu_MISReports_ReimbursementByMonthDetail.Size = New System.Drawing.Size(319, 22)
        Me.mnu_MISReports_ReimbursementByMonthDetail.Text = "Reimbursement By Month By Account - Detail"
        '
        'mnu_MISReports_ReimbursementByInsuranceCarrier
        '
        Me.mnu_MISReports_ReimbursementByInsuranceCarrier.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnu_MISReports_ReimbursementByInsuranceCarrier.Image = CType(resources.GetObject("mnu_MISReports_ReimbursementByInsuranceCarrier.Image"),System.Drawing.Image)
        Me.mnu_MISReports_ReimbursementByInsuranceCarrier.Name = "mnu_MISReports_ReimbursementByInsuranceCarrier"
        Me.mnu_MISReports_ReimbursementByInsuranceCarrier.Size = New System.Drawing.Size(319, 22)
        Me.mnu_MISReports_ReimbursementByInsuranceCarrier.Text = "Reimbursement By Insurance Carrier"
        '
        'mnu_MISReports_ReimbursementByInsuranceByCPT
        '
        Me.mnu_MISReports_ReimbursementByInsuranceByCPT.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnu_MISReports_ReimbursementByInsuranceByCPT.Image = CType(resources.GetObject("mnu_MISReports_ReimbursementByInsuranceByCPT.Image"),System.Drawing.Image)
        Me.mnu_MISReports_ReimbursementByInsuranceByCPT.Name = "mnu_MISReports_ReimbursementByInsuranceByCPT"
        Me.mnu_MISReports_ReimbursementByInsuranceByCPT.Size = New System.Drawing.Size(319, 22)
        Me.mnu_MISReports_ReimbursementByInsuranceByCPT.Text = "Reimbursement By Insurance Carrier By CPT Code"
        '
        'mnu_MISReports_ReimbursementByCPTByInsurance
        '
        Me.mnu_MISReports_ReimbursementByCPTByInsurance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnu_MISReports_ReimbursementByCPTByInsurance.Image = CType(resources.GetObject("mnu_MISReports_ReimbursementByCPTByInsurance.Image"),System.Drawing.Image)
        Me.mnu_MISReports_ReimbursementByCPTByInsurance.Name = "mnu_MISReports_ReimbursementByCPTByInsurance"
        Me.mnu_MISReports_ReimbursementByCPTByInsurance.Size = New System.Drawing.Size(319, 22)
        Me.mnu_MISReports_ReimbursementByCPTByInsurance.Text = "Reimbursement By CPT Code By Insurance Carrier"
        '
        'mnu_MISReports_ReimbursementByDoctorByInsurance
        '
        Me.mnu_MISReports_ReimbursementByDoctorByInsurance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnu_MISReports_ReimbursementByDoctorByInsurance.Image = CType(resources.GetObject("mnu_MISReports_ReimbursementByDoctorByInsurance.Image"),System.Drawing.Image)
        Me.mnu_MISReports_ReimbursementByDoctorByInsurance.Name = "mnu_MISReports_ReimbursementByDoctorByInsurance"
        Me.mnu_MISReports_ReimbursementByDoctorByInsurance.Size = New System.Drawing.Size(319, 22)
        Me.mnu_MISReports_ReimbursementByDoctorByInsurance.Text = "Reimbursement By Doctor By Insurance Carrier"
        '
        'mnu_MISReports_ReimbursementByInsuranceForCPT
        '
        Me.mnu_MISReports_ReimbursementByInsuranceForCPT.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnu_MISReports_ReimbursementByInsuranceForCPT.Image = CType(resources.GetObject("mnu_MISReports_ReimbursementByInsuranceForCPT.Image"),System.Drawing.Image)
        Me.mnu_MISReports_ReimbursementByInsuranceForCPT.Name = "mnu_MISReports_ReimbursementByInsuranceForCPT"
        Me.mnu_MISReports_ReimbursementByInsuranceForCPT.Size = New System.Drawing.Size(319, 22)
        Me.mnu_MISReports_ReimbursementByInsuranceForCPT.Text = "Reimbursement By Insurance Carrier For CPT Code"
        '
        'mnu_MISReports_ReimbursementDetailsByAccount
        '
        Me.mnu_MISReports_ReimbursementDetailsByAccount.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnu_MISReports_ReimbursementDetailsByAccount.Image = CType(resources.GetObject("mnu_MISReports_ReimbursementDetailsByAccount.Image"),System.Drawing.Image)
        Me.mnu_MISReports_ReimbursementDetailsByAccount.Name = "mnu_MISReports_ReimbursementDetailsByAccount"
        Me.mnu_MISReports_ReimbursementDetailsByAccount.Size = New System.Drawing.Size(319, 22)
        Me.mnu_MISReports_ReimbursementDetailsByAccount.Text = "Reimbursement Details By Account"
        '
        'toolStripSeparator26
        '
        Me.toolStripSeparator26.Name = "toolStripSeparator26"
        Me.toolStripSeparator26.Size = New System.Drawing.Size(316, 6)
        '
        'mnu_MISReports_AgingAnalysis
        '
        Me.mnu_MISReports_AgingAnalysis.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnu_MISReports_AgingAnalysis.Image = CType(resources.GetObject("mnu_MISReports_AgingAnalysis.Image"),System.Drawing.Image)
        Me.mnu_MISReports_AgingAnalysis.Name = "mnu_MISReports_AgingAnalysis"
        Me.mnu_MISReports_AgingAnalysis.Size = New System.Drawing.Size(319, 22)
        Me.mnu_MISReports_AgingAnalysis.Text = "Aging Analysis"
        '
        'mnu_MISReports_AgingSummaryByPatient
        '
        Me.mnu_MISReports_AgingSummaryByPatient.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnu_MISReports_AgingSummaryByPatient.Image = CType(resources.GetObject("mnu_MISReports_AgingSummaryByPatient.Image"),System.Drawing.Image)
        Me.mnu_MISReports_AgingSummaryByPatient.Name = "mnu_MISReports_AgingSummaryByPatient"
        Me.mnu_MISReports_AgingSummaryByPatient.Size = New System.Drawing.Size(319, 22)
        Me.mnu_MISReports_AgingSummaryByPatient.Text = "Aging Summary By Patient"
        '
        'mnu_MISReports_AgingSummaryByInsuranceCarrier
        '
        Me.mnu_MISReports_AgingSummaryByInsuranceCarrier.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnu_MISReports_AgingSummaryByInsuranceCarrier.Image = CType(resources.GetObject("mnu_MISReports_AgingSummaryByInsuranceCarrier.Image"),System.Drawing.Image)
        Me.mnu_MISReports_AgingSummaryByInsuranceCarrier.Name = "mnu_MISReports_AgingSummaryByInsuranceCarrier"
        Me.mnu_MISReports_AgingSummaryByInsuranceCarrier.Size = New System.Drawing.Size(319, 22)
        Me.mnu_MISReports_AgingSummaryByInsuranceCarrier.Text = "Aging Summary By Insurance Carrier"
        '
        'toolStripSeparator27
        '
        Me.toolStripSeparator27.Name = "toolStripSeparator27"
        Me.toolStripSeparator27.Size = New System.Drawing.Size(316, 6)
        '
        'mnu_MISReports_ProductionByPhysicianGroup
        '
        Me.mnu_MISReports_ProductionByPhysicianGroup.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnu_MISReports_ProductionByPhysicianGroup.Image = CType(resources.GetObject("mnu_MISReports_ProductionByPhysicianGroup.Image"),System.Drawing.Image)
        Me.mnu_MISReports_ProductionByPhysicianGroup.Name = "mnu_MISReports_ProductionByPhysicianGroup"
        Me.mnu_MISReports_ProductionByPhysicianGroup.Size = New System.Drawing.Size(319, 22)
        Me.mnu_MISReports_ProductionByPhysicianGroup.Text = "Production By Physician Group"
        '
        'mnu_MISReports_ProductionAnalysisByFacility
        '
        Me.mnu_MISReports_ProductionAnalysisByFacility.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnu_MISReports_ProductionAnalysisByFacility.Image = CType(resources.GetObject("mnu_MISReports_ProductionAnalysisByFacility.Image"),System.Drawing.Image)
        Me.mnu_MISReports_ProductionAnalysisByFacility.Name = "mnu_MISReports_ProductionAnalysisByFacility"
        Me.mnu_MISReports_ProductionAnalysisByFacility.Size = New System.Drawing.Size(319, 22)
        Me.mnu_MISReports_ProductionAnalysisByFacility.Text = "Production Analysis By Facility"
        '
        'mnu_MISReports_ProductionAnalysisByprocedureGroup
        '
        Me.mnu_MISReports_ProductionAnalysisByprocedureGroup.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnu_MISReports_ProductionAnalysisByprocedureGroup.Image = CType(resources.GetObject("mnu_MISReports_ProductionAnalysisByprocedureGroup.Image"),System.Drawing.Image)
        Me.mnu_MISReports_ProductionAnalysisByprocedureGroup.Name = "mnu_MISReports_ProductionAnalysisByprocedureGroup"
        Me.mnu_MISReports_ProductionAnalysisByprocedureGroup.Size = New System.Drawing.Size(319, 22)
        Me.mnu_MISReports_ProductionAnalysisByprocedureGroup.Text = "Production Analysis By Procedure Group"
        '
        'mnu_MISReports_ProductionAnalysisandTrendsByMonth
        '
        Me.mnu_MISReports_ProductionAnalysisandTrendsByMonth.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnu_MISReports_ProductionAnalysisandTrendsByMonth.Image = CType(resources.GetObject("mnu_MISReports_ProductionAnalysisandTrendsByMonth.Image"),System.Drawing.Image)
        Me.mnu_MISReports_ProductionAnalysisandTrendsByMonth.Name = "mnu_MISReports_ProductionAnalysisandTrendsByMonth"
        Me.mnu_MISReports_ProductionAnalysisandTrendsByMonth.Size = New System.Drawing.Size(319, 22)
        Me.mnu_MISReports_ProductionAnalysisandTrendsByMonth.Text = "Production Analysis And Trends By Month"
        '
        'mnu_MISReports_ProductionTrendsByProcedureGrop
        '
        Me.mnu_MISReports_ProductionTrendsByProcedureGrop.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnu_MISReports_ProductionTrendsByProcedureGrop.Image = CType(resources.GetObject("mnu_MISReports_ProductionTrendsByProcedureGrop.Image"),System.Drawing.Image)
        Me.mnu_MISReports_ProductionTrendsByProcedureGrop.Name = "mnu_MISReports_ProductionTrendsByProcedureGrop"
        Me.mnu_MISReports_ProductionTrendsByProcedureGrop.Size = New System.Drawing.Size(319, 22)
        Me.mnu_MISReports_ProductionTrendsByProcedureGrop.Text = "Production Trends By Procedure Group"
        '
        'mnuFAXReports
        '
        Me.mnuFAXReports.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuFAXReport, Me.mnuPendingFAXesWithMaxAttempts, Me.mnuPendingFAXesWithoutTIFF})
        Me.mnuFAXReports.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuFAXReports.Image = CType(resources.GetObject("mnuFAXReports.Image"),System.Drawing.Image)
        Me.mnuFAXReports.Name = "mnuFAXReports"
        Me.mnuFAXReports.Size = New System.Drawing.Size(230, 22)
        Me.mnuFAXReports.Text = "Fax"
        '
        'mnuFAXReport
        '
        Me.mnuFAXReport.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuFAXReport.Image = CType(resources.GetObject("mnuFAXReport.Image"),System.Drawing.Image)
        Me.mnuFAXReport.Name = "mnuFAXReport"
        Me.mnuFAXReport.Size = New System.Drawing.Size(208, 22)
        Me.mnuFAXReport.Text = "&Fax Status Report"
        '
        'mnuPendingFAXesWithMaxAttempts
        '
        Me.mnuPendingFAXesWithMaxAttempts.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuPendingFAXesWithMaxAttempts.Image = CType(resources.GetObject("mnuPendingFAXesWithMaxAttempts.Image"),System.Drawing.Image)
        Me.mnuPendingFAXesWithMaxAttempts.Name = "mnuPendingFAXesWithMaxAttempts"
        Me.mnuPendingFAXesWithMaxAttempts.Size = New System.Drawing.Size(208, 22)
        Me.mnuPendingFAXesWithMaxAttempts.Text = "Failed Faxes"
        '
        'mnuPendingFAXesWithoutTIFF
        '
        Me.mnuPendingFAXesWithoutTIFF.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuPendingFAXesWithoutTIFF.Image = CType(resources.GetObject("mnuPendingFAXesWithoutTIFF.Image"),System.Drawing.Image)
        Me.mnuPendingFAXesWithoutTIFF.Name = "mnuPendingFAXesWithoutTIFF"
        Me.mnuPendingFAXesWithoutTIFF.Size = New System.Drawing.Size(208, 22)
        Me.mnuPendingFAXesWithoutTIFF.Text = "Pending Faxes without TIFF"
        '
        'mnuReportExamPrintFAX
        '
        Me.mnuReportExamPrintFAX.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuReportExamPrintFAX.Image = CType(resources.GetObject("mnuReportExamPrintFAX.Image"),System.Drawing.Image)
        Me.mnuReportExamPrintFAX.Name = "mnuReportExamPrintFAX"
        Me.mnuReportExamPrintFAX.Size = New System.Drawing.Size(230, 22)
        Me.mnuReportExamPrintFAX.Text = "Exams Print / Fax"
        '
        'mnuReviewExams
        '
        Me.mnuReviewExams.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuReviewExams.Image = CType(resources.GetObject("mnuReviewExams.Image"),System.Drawing.Image)
        Me.mnuReviewExams.Name = "mnuReviewExams"
        Me.mnuReviewExams.Size = New System.Drawing.Size(230, 22)
        Me.mnuReviewExams.Text = "Review Exams"
        Me.mnuReviewExams.Visible = false
        '
        'mnuBatchReferrals
        '
        Me.mnuBatchReferrals.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuBatchReferrals.Image = CType(resources.GetObject("mnuBatchReferrals.Image"),System.Drawing.Image)
        Me.mnuBatchReferrals.Name = "mnuBatchReferrals"
        Me.mnuBatchReferrals.Size = New System.Drawing.Size(230, 22)
        Me.mnuBatchReferrals.Text = "Batch Referral Letters"
        '
        'mnuBatchPrintTemplates
        '
        Me.mnuBatchPrintTemplates.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuBatchPrintTemplates.Image = CType(resources.GetObject("mnuBatchPrintTemplates.Image"),System.Drawing.Image)
        Me.mnuBatchPrintTemplates.Name = "mnuBatchPrintTemplates"
        Me.mnuBatchPrintTemplates.Size = New System.Drawing.Size(230, 22)
        Me.mnuBatchPrintTemplates.Text = "Batch Print Template"
        '
        'MnuClinicalChart
        '
        Me.MnuClinicalChart.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.MnuClinicalChart.Image = CType(resources.GetObject("MnuClinicalChart.Image"),System.Drawing.Image)
        Me.MnuClinicalChart.Name = "MnuClinicalChart"
        Me.MnuClinicalChart.Size = New System.Drawing.Size(230, 22)
        Me.MnuClinicalChart.Text = "Clinical Chart"
        '
        'MnuExamFinishReport
        '
        Me.MnuExamFinishReport.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.MnuExamFinishReport.Image = CType(resources.GetObject("MnuExamFinishReport.Image"),System.Drawing.Image)
        Me.MnuExamFinishReport.Name = "MnuExamFinishReport"
        Me.MnuExamFinishReport.Size = New System.Drawing.Size(230, 22)
        Me.MnuExamFinishReport.Text = "Exam Finish Rate Report"
        '
        'ToolStripMenuItem2
        '
        Me.ToolStripMenuItem2.Name = "ToolStripMenuItem2"
        Me.ToolStripMenuItem2.Size = New System.Drawing.Size(227, 6)
        '
        'mnu_rpt_Appointments
        '
        Me.mnu_rpt_Appointments.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnu_rpt_Appointments.Image = CType(resources.GetObject("mnu_rpt_Appointments.Image"),System.Drawing.Image)
        Me.mnu_rpt_Appointments.Name = "mnu_rpt_Appointments"
        Me.mnu_rpt_Appointments.Size = New System.Drawing.Size(230, 22)
        Me.mnu_rpt_Appointments.Text = "Appointments"
        '
        'mnu_rpt_ConfirmAppointments
        '
        Me.mnu_rpt_ConfirmAppointments.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnu_rpt_ConfirmAppointments.Image = CType(resources.GetObject("mnu_rpt_ConfirmAppointments.Image"),System.Drawing.Image)
        Me.mnu_rpt_ConfirmAppointments.Name = "mnu_rpt_ConfirmAppointments"
        Me.mnu_rpt_ConfirmAppointments.Size = New System.Drawing.Size(230, 22)
        Me.mnu_rpt_ConfirmAppointments.Text = "Confirm Appointments"
        '
        'mnu_rpt_NoShowAppointments
        '
        Me.mnu_rpt_NoShowAppointments.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnu_rpt_NoShowAppointments.Image = CType(resources.GetObject("mnu_rpt_NoShowAppointments.Image"),System.Drawing.Image)
        Me.mnu_rpt_NoShowAppointments.Name = "mnu_rpt_NoShowAppointments"
        Me.mnu_rpt_NoShowAppointments.Size = New System.Drawing.Size(230, 22)
        Me.mnu_rpt_NoShowAppointments.Text = "Cancel Appointments"
        '
        'mnu_rpt_CensusAppointments
        '
        Me.mnu_rpt_CensusAppointments.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnu_rpt_CensusAppointments.Image = CType(resources.GetObject("mnu_rpt_CensusAppointments.Image"),System.Drawing.Image)
        Me.mnu_rpt_CensusAppointments.Name = "mnu_rpt_CensusAppointments"
        Me.mnu_rpt_CensusAppointments.Size = New System.Drawing.Size(230, 22)
        Me.mnu_rpt_CensusAppointments.Text = "Appointments Census Report"
        '
        'mnu_MIPS
        '
        Me.mnu_MIPS.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnu_MIPS_Quality_2019, Me.mnu_MIPS_Quality, Me.mnu_MIPS_Quality_2017, Me.mnu_MIPSACI_2019, Me.mnu_MIPS_ACI, Me.mnuMUDashboard_2017_ACI})
        Me.mnu_MIPS.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnu_MIPS.Image = CType(resources.GetObject("mnu_MIPS.Image"),System.Drawing.Image)
        Me.mnu_MIPS.Name = "mnu_MIPS"
        Me.mnu_MIPS.Size = New System.Drawing.Size(230, 22)
        Me.mnu_MIPS.Text = "MIPS Dashboard"
        '
        'mnu_MIPS_Quality_2019
        '
        Me.mnu_MIPS_Quality_2019.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnu_MIPS_Quality_2019.Image = CType(resources.GetObject("mnu_MIPS_Quality_2019.Image"),System.Drawing.Image)
        Me.mnu_MIPS_Quality_2019.Name = "mnu_MIPS_Quality_2019"
        Me.mnu_MIPS_Quality_2019.Size = New System.Drawing.Size(204, 22)
        Me.mnu_MIPS_Quality_2019.Text = "Quality Dashboard 2019"
        '
        'mnu_MIPS_Quality
        '
        Me.mnu_MIPS_Quality.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnu_MIPS_Quality.Image = CType(resources.GetObject("mnu_MIPS_Quality.Image"),System.Drawing.Image)
        Me.mnu_MIPS_Quality.Name = "mnu_MIPS_Quality"
        Me.mnu_MIPS_Quality.Size = New System.Drawing.Size(204, 22)
        Me.mnu_MIPS_Quality.Text = "Quality Dashboard 2018"
        '
        'mnu_MIPS_Quality_2017
        '
        Me.mnu_MIPS_Quality_2017.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnu_MIPS_Quality_2017.Image = CType(resources.GetObject("mnu_MIPS_Quality_2017.Image"),System.Drawing.Image)
        Me.mnu_MIPS_Quality_2017.Name = "mnu_MIPS_Quality_2017"
        Me.mnu_MIPS_Quality_2017.Size = New System.Drawing.Size(204, 22)
        Me.mnu_MIPS_Quality_2017.Text = "Quality Dashboard 2017"
        '
        'mnu_MIPSACI_2019
        '
        Me.mnu_MIPSACI_2019.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnu_MIPSACI_2019.Image = CType(resources.GetObject("mnu_MIPSACI_2019.Image"),System.Drawing.Image)
        Me.mnu_MIPSACI_2019.Name = "mnu_MIPSACI_2019"
        Me.mnu_MIPSACI_2019.Size = New System.Drawing.Size(204, 22)
        Me.mnu_MIPSACI_2019.Text = "2019 PI Dashboard"
        '
        'mnu_MIPS_ACI
        '
        Me.mnu_MIPS_ACI.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnu_MIPS_ACI.Image = CType(resources.GetObject("mnu_MIPS_ACI.Image"),System.Drawing.Image)
        Me.mnu_MIPS_ACI.Name = "mnu_MIPS_ACI"
        Me.mnu_MIPS_ACI.Size = New System.Drawing.Size(204, 22)
        Me.mnu_MIPS_ACI.Text = "2018 PI Dashboard"
        '
        'mnuMUDashboard_2017_ACI
        '
        Me.mnuMUDashboard_2017_ACI.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuMUDashboard_2017_ACI.Image = CType(resources.GetObject("mnuMUDashboard_2017_ACI.Image"),System.Drawing.Image)
        Me.mnuMUDashboard_2017_ACI.Name = "mnuMUDashboard_2017_ACI"
        Me.mnuMUDashboard_2017_ACI.Size = New System.Drawing.Size(204, 22)
        Me.mnuMUDashboard_2017_ACI.Text = "PI TRANSITION Dashboard"
        '
        'ToolStripSeparator17
        '
        Me.ToolStripSeparator17.Name = "ToolStripSeparator17"
        Me.ToolStripSeparator17.Size = New System.Drawing.Size(227, 6)
        '
        'mnuMUDashboardMainMenu
        '
        Me.mnuMUDashboardMainMenu.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuMUDashboard_Stage1, Me.mnuMUDashboard_Stage2, Me.mnuMUDashboard_Stage2_Mod, Me.mnuMUDashboard_Stage3, Me.mnuMUDashboard, Me.mnu_Stage3_2019})
        Me.mnuMUDashboardMainMenu.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuMUDashboardMainMenu.Image = Global.gloEMR.My.Resources.Resources.MUGroup1
        Me.mnuMUDashboardMainMenu.Name = "mnuMUDashboardMainMenu"
        Me.mnuMUDashboardMainMenu.Size = New System.Drawing.Size(230, 22)
        Me.mnuMUDashboardMainMenu.Text = "MU Dashboard"
        '
        'mnuMUDashboard_Stage1
        '
        Me.mnuMUDashboard_Stage1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuMUDashboard_Stage1.Image = Global.gloEMR.My.Resources.Resources.Stage1_2014
        Me.mnuMUDashboard_Stage1.Name = "mnuMUDashboard_Stage1"
        Me.mnuMUDashboard_Stage1.Size = New System.Drawing.Size(189, 22)
        Me.mnuMUDashboard_Stage1.Text = "Stage 1 2014+"
        '
        'mnuMUDashboard_Stage2
        '
        Me.mnuMUDashboard_Stage2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuMUDashboard_Stage2.Image = Global.gloEMR.My.Resources.Resources.Stage2_20141
        Me.mnuMUDashboard_Stage2.Name = "mnuMUDashboard_Stage2"
        Me.mnuMUDashboard_Stage2.Size = New System.Drawing.Size(189, 22)
        Me.mnuMUDashboard_Stage2.Text = "Stage 2 2014+"
        '
        'mnuMUDashboard_Stage2_Mod
        '
        Me.mnuMUDashboard_Stage2_Mod.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuMUDashboard_Stage2_Mod.Image = CType(resources.GetObject("mnuMUDashboard_Stage2_Mod.Image"),System.Drawing.Image)
        Me.mnuMUDashboard_Stage2_Mod.Name = "mnuMUDashboard_Stage2_Mod"
        Me.mnuMUDashboard_Stage2_Mod.Size = New System.Drawing.Size(189, 22)
        Me.mnuMUDashboard_Stage2_Mod.Text = "Stage 2 Modified 2015+"
        '
        'mnuMUDashboard_Stage3
        '
        Me.mnuMUDashboard_Stage3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuMUDashboard_Stage3.Image = CType(resources.GetObject("mnuMUDashboard_Stage3.Image"),System.Drawing.Image)
        Me.mnuMUDashboard_Stage3.Name = "mnuMUDashboard_Stage3"
        Me.mnuMUDashboard_Stage3.Size = New System.Drawing.Size(189, 22)
        Me.mnuMUDashboard_Stage3.Text = "Stage 3 2017+"
        '
        'mnuMUDashboard
        '
        Me.mnuMUDashboard.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuMUDashboard.Image = Global.gloEMR.My.Resources.Resources.Stage0_20111
        Me.mnuMUDashboard.Name = "mnuMUDashboard"
        Me.mnuMUDashboard.Size = New System.Drawing.Size(189, 22)
        Me.mnuMUDashboard.Text = "2011+"
        '
        'mnu_Stage3_2019
        '
        Me.mnu_Stage3_2019.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.mnu_Stage3_2019.Image = CType(resources.GetObject("mnu_Stage3_2019.Image"), System.Drawing.Image)
        Me.mnu_Stage3_2019.Name = "mnu_Stage3_2019"
        Me.mnu_Stage3_2019.Size = New System.Drawing.Size(189, 22)
        Me.mnu_Stage3_2019.Text = "Stage 3 2019"
        '
        'QualityMeasureDashboardToolStripMenuItem
        '
        Me.QualityMeasureDashboardToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.QualityMeasureDashboardToolStripMenuItem.Image = Global.gloEMR.My.Resources.Resources.Quality_Measure_Dashboard
        Me.QualityMeasureDashboardToolStripMenuItem.Name = "QualityMeasureDashboardToolStripMenuItem"
        Me.QualityMeasureDashboardToolStripMenuItem.Size = New System.Drawing.Size(230, 22)
        Me.QualityMeasureDashboardToolStripMenuItem.Text = "Quality Measure Dashboard"
        '
        'MedicaidCensusReportDashboardToolStripMenuItem
        '
        Me.MedicaidCensusReportDashboardToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.MedicaidCensusReportDashboardToolStripMenuItem.Image = CType(resources.GetObject("MedicaidCensusReportDashboardToolStripMenuItem.Image"),System.Drawing.Image)
        Me.MedicaidCensusReportDashboardToolStripMenuItem.Name = "MedicaidCensusReportDashboardToolStripMenuItem"
        Me.MedicaidCensusReportDashboardToolStripMenuItem.Size = New System.Drawing.Size(230, 22)
        Me.MedicaidCensusReportDashboardToolStripMenuItem.Text = "Medicaid Census Report"
        '
        'sep23
        '
        Me.sep23.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.sep23.Name = "sep23"
        Me.sep23.Size = New System.Drawing.Size(227, 6)
        '
        'mnuPatientDemographics
        '
        Me.mnuPatientDemographics.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuPatientDemographics.Image = CType(resources.GetObject("mnuPatientDemographics.Image"),System.Drawing.Image)
        Me.mnuPatientDemographics.Name = "mnuPatientDemographics"
        Me.mnuPatientDemographics.Size = New System.Drawing.Size(230, 22)
        Me.mnuPatientDemographics.Text = "Patient Demographics"
        '
        'mnurptHCFAReport
        '
        Me.mnurptHCFAReport.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnurptHCFAReport.Image = CType(resources.GetObject("mnurptHCFAReport.Image"),System.Drawing.Image)
        Me.mnurptHCFAReport.Name = "mnurptHCFAReport"
        Me.mnurptHCFAReport.Size = New System.Drawing.Size(230, 22)
        Me.mnurptHCFAReport.Text = "HCFA Report"
        '
        'mnuRptDignosisLabResult
        '
        Me.mnuRptDignosisLabResult.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuRptDignosisLabResult.Image = CType(resources.GetObject("mnuRptDignosisLabResult.Image"),System.Drawing.Image)
        Me.mnuRptDignosisLabResult.Name = "mnuRptDignosisLabResult"
        Me.mnuRptDignosisLabResult.Size = New System.Drawing.Size(230, 22)
        Me.mnuRptDignosisLabResult.Text = "Diagnosis Lab Result"
        '
        'mnuRpt_HealthPlan
        '
        Me.mnuRpt_HealthPlan.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuRpt_HealthPlan.Image = CType(resources.GetObject("mnuRpt_HealthPlan.Image"),System.Drawing.Image)
        Me.mnuRpt_HealthPlan.Name = "mnuRpt_HealthPlan"
        Me.mnuRpt_HealthPlan.Size = New System.Drawing.Size(230, 22)
        Me.mnuRpt_HealthPlan.Text = "Health Plan"
        '
        'mnuRpt_OpenRecommendations
        '
        Me.mnuRpt_OpenRecommendations.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuRpt_OpenRecommendations.Image = CType(resources.GetObject("mnuRpt_OpenRecommendations.Image"),System.Drawing.Image)
        Me.mnuRpt_OpenRecommendations.Name = "mnuRpt_OpenRecommendations"
        Me.mnuRpt_OpenRecommendations.Size = New System.Drawing.Size(230, 22)
        Me.mnuRpt_OpenRecommendations.Tag = "OpenRecommendations"
        Me.mnuRpt_OpenRecommendations.Text = "Open Recommendations"
        '
        'sep24
        '
        Me.sep24.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.sep24.Name = "sep24"
        Me.sep24.Size = New System.Drawing.Size(227, 6)
        '
        'mnuViewAllCCDCCRFiles
        '
        Me.mnuViewAllCCDCCRFiles.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuViewAllCCDCCRFiles.Image = CType(resources.GetObject("mnuViewAllCCDCCRFiles.Image"),System.Drawing.Image)
        Me.mnuViewAllCCDCCRFiles.Name = "mnuViewAllCCDCCRFiles"
        Me.mnuViewAllCCDCCRFiles.Size = New System.Drawing.Size(230, 22)
        Me.mnuViewAllCCDCCRFiles.Text = "View CDA Files Reports"
        '
        'mnuUnfinishedReconciliationLists
        '
        Me.mnuUnfinishedReconciliationLists.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuUnfinishedReconciliationLists.Image = CType(resources.GetObject("mnuUnfinishedReconciliationLists.Image"),System.Drawing.Image)
        Me.mnuUnfinishedReconciliationLists.Name = "mnuUnfinishedReconciliationLists"
        Me.mnuUnfinishedReconciliationLists.Size = New System.Drawing.Size(230, 22)
        Me.mnuUnfinishedReconciliationLists.Text = "Unextracted Clinical Files"
        '
        'mnuUnfinishedFiles
        '
        Me.mnuUnfinishedFiles.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuUnfinishedFiles.Image = CType(resources.GetObject("mnuUnfinishedFiles.Image"),System.Drawing.Image)
        Me.mnuUnfinishedFiles.Name = "mnuUnfinishedFiles"
        Me.mnuUnfinishedFiles.Size = New System.Drawing.Size(230, 22)
        Me.mnuUnfinishedFiles.Text = "Incomplete Clinical Reconciliation"
        '
        'mnu_DM_Rpt_DueGuideline
        '
        Me.mnu_DM_Rpt_DueGuideline.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnu_DM_Rpt_DueGuideline.Image = CType(resources.GetObject("mnu_DM_Rpt_DueGuideline.Image"),System.Drawing.Image)
        Me.mnu_DM_Rpt_DueGuideline.Name = "mnu_DM_Rpt_DueGuideline"
        Me.mnu_DM_Rpt_DueGuideline.Size = New System.Drawing.Size(230, 22)
        Me.mnu_DM_Rpt_DueGuideline.Text = "Guideline Reports"
        '
        'mnuRptIMDueReport
        '
        Me.mnuRptIMDueReport.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuRptIMDueReport.Image = CType(resources.GetObject("mnuRptIMDueReport.Image"),System.Drawing.Image)
        Me.mnuRptIMDueReport.Name = "mnuRptIMDueReport"
        Me.mnuRptIMDueReport.Size = New System.Drawing.Size(230, 22)
        Me.mnuRptIMDueReport.Text = "Immunization Due Report"
        '
        'mnuRpt_VaccineInventoryReport
        '
        Me.mnuRpt_VaccineInventoryReport.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuRpt_VaccineInventoryReport.Image = CType(resources.GetObject("mnuRpt_VaccineInventoryReport.Image"),System.Drawing.Image)
        Me.mnuRpt_VaccineInventoryReport.Name = "mnuRpt_VaccineInventoryReport"
        Me.mnuRpt_VaccineInventoryReport.Size = New System.Drawing.Size(230, 22)
        Me.mnuRpt_VaccineInventoryReport.Text = "Vaccine Inventory Report"
        '
        'mnuRpt_LabGraph
        '
        Me.mnuRpt_LabGraph.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuRpt_LabGraph.Image = CType(resources.GetObject("mnuRpt_LabGraph.Image"),System.Drawing.Image)
        Me.mnuRpt_LabGraph.Name = "mnuRpt_LabGraph"
        Me.mnuRpt_LabGraph.Size = New System.Drawing.Size(230, 22)
        Me.mnuRpt_LabGraph.Text = "Lab Graph"
        '
        'mnuRpt_PatientReminderLetters
        '
        Me.mnuRpt_PatientReminderLetters.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuRpt_PatientReminderLetters.Image = CType(resources.GetObject("mnuRpt_PatientReminderLetters.Image"),System.Drawing.Image)
        Me.mnuRpt_PatientReminderLetters.Name = "mnuRpt_PatientReminderLetters"
        Me.mnuRpt_PatientReminderLetters.Size = New System.Drawing.Size(230, 22)
        Me.mnuRpt_PatientReminderLetters.Text = "Patient Reminder Letters"
        '
        'mnurpt_PatientICD9CPT
        '
        Me.mnurpt_PatientICD9CPT.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnurpt_PatientICD9CPT.Image = CType(resources.GetObject("mnurpt_PatientICD9CPT.Image"),System.Drawing.Image)
        Me.mnurpt_PatientICD9CPT.Name = "mnurpt_PatientICD9CPT"
        Me.mnurpt_PatientICD9CPT.Size = New System.Drawing.Size(230, 22)
        Me.mnurpt_PatientICD9CPT.Text = "Patient ICD9/10-CPT"
        '
        'CCHIT11ReportToolStripMenuItem
        '
        Me.CCHIT11ReportToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuReportICD9Rx, Me.mnu_Patient_list_report, Me.mnuPatientReminder, Me.mnuPatientVitalUsageReport, Me.mnuAllergyUsageReport, Me.mnuProblemUsageReport, Me.mnuDemographicUsageReport, Me.mnuMedicationUsagereport, Me.mnuEprescribingreport, Me.mnuHistoryUsageReport, Me.mnuReportPatientsAlerts, Me.mnuReportClinicalDecision, Me.mnuReportBPMeasurement, Me.mnuReportPatientHistory, Me.mnuPatientBMIReport, Me.mnuPatientRxReport, Me.mnuOBReport, Me.mnuPDRProgramsReports, Me.mnuEPCS})
        Me.CCHIT11ReportToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.CCHIT11ReportToolStripMenuItem.Image = CType(resources.GetObject("CCHIT11ReportToolStripMenuItem.Image"),System.Drawing.Image)
        Me.CCHIT11ReportToolStripMenuItem.Name = "CCHIT11ReportToolStripMenuItem"
        Me.CCHIT11ReportToolStripMenuItem.Size = New System.Drawing.Size(230, 22)
        Me.CCHIT11ReportToolStripMenuItem.Text = "Advanced Reports"
        Me.CCHIT11ReportToolStripMenuItem.ToolTipText = "Advanced Reports"
        '
        'mnuReportICD9Rx
        '
        Me.mnuReportICD9Rx.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuReportICD9Rx.Image = Global.gloEMR.My.Resources.Resources.ICD_09_Report
        Me.mnuReportICD9Rx.Name = "mnuReportICD9Rx"
        Me.mnuReportICD9Rx.Size = New System.Drawing.Size(205, 22)
        Me.mnuReportICD9Rx.Text = "Patient List Report"
        '
        'mnu_Patient_list_report
        '
        Me.mnu_Patient_list_report.Name = "mnu_Patient_list_report"
        Me.mnu_Patient_list_report.Size = New System.Drawing.Size(205, 22)
        Me.mnu_Patient_list_report.Text = "Patient List Report"
        Me.mnu_Patient_list_report.Visible = false
        '
        'mnuPatientReminder
        '
        Me.mnuPatientReminder.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuPatientReminder.Image = Global.gloEMR.My.Resources.Resources.Patient_Reminder_Report
        Me.mnuPatientReminder.Name = "mnuPatientReminder"
        Me.mnuPatientReminder.Size = New System.Drawing.Size(205, 22)
        Me.mnuPatientReminder.Tag = "PatientReminder"
        Me.mnuPatientReminder.Text = "Patient Reminder"
        '
        'mnuPatientVitalUsageReport
        '
        Me.mnuPatientVitalUsageReport.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuPatientVitalUsageReport.Image = CType(resources.GetObject("mnuPatientVitalUsageReport.Image"),System.Drawing.Image)
        Me.mnuPatientVitalUsageReport.Name = "mnuPatientVitalUsageReport"
        Me.mnuPatientVitalUsageReport.Size = New System.Drawing.Size(205, 22)
        Me.mnuPatientVitalUsageReport.Text = "Vital Usage Report"
        '
        'mnuAllergyUsageReport
        '
        Me.mnuAllergyUsageReport.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuAllergyUsageReport.Image = CType(resources.GetObject("mnuAllergyUsageReport.Image"),System.Drawing.Image)
        Me.mnuAllergyUsageReport.Name = "mnuAllergyUsageReport"
        Me.mnuAllergyUsageReport.Size = New System.Drawing.Size(205, 22)
        Me.mnuAllergyUsageReport.Text = "Allergy Usage Report"
        '
        'mnuProblemUsageReport
        '
        Me.mnuProblemUsageReport.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuProblemUsageReport.Image = CType(resources.GetObject("mnuProblemUsageReport.Image"),System.Drawing.Image)
        Me.mnuProblemUsageReport.Name = "mnuProblemUsageReport"
        Me.mnuProblemUsageReport.Size = New System.Drawing.Size(205, 22)
        Me.mnuProblemUsageReport.Text = "Problem Usage Report"
        '
        'mnuDemographicUsageReport
        '
        Me.mnuDemographicUsageReport.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuDemographicUsageReport.Image = CType(resources.GetObject("mnuDemographicUsageReport.Image"),System.Drawing.Image)
        Me.mnuDemographicUsageReport.Name = "mnuDemographicUsageReport"
        Me.mnuDemographicUsageReport.Size = New System.Drawing.Size(205, 22)
        Me.mnuDemographicUsageReport.Text = "Demographic Usage Report"
        '
        'mnuMedicationUsagereport
        '
        Me.mnuMedicationUsagereport.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuMedicationUsagereport.Image = CType(resources.GetObject("mnuMedicationUsagereport.Image"),System.Drawing.Image)
        Me.mnuMedicationUsagereport.Name = "mnuMedicationUsagereport"
        Me.mnuMedicationUsagereport.Size = New System.Drawing.Size(205, 22)
        Me.mnuMedicationUsagereport.Text = "Medication Usage Report"
        '
        'mnuEprescribingreport
        '
        Me.mnuEprescribingreport.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuEprescribingreport.Image = CType(resources.GetObject("mnuEprescribingreport.Image"),System.Drawing.Image)
        Me.mnuEprescribingreport.Name = "mnuEprescribingreport"
        Me.mnuEprescribingreport.Size = New System.Drawing.Size(205, 22)
        Me.mnuEprescribingreport.Text = "ePrescribing Usage Report"
        '
        'mnuHistoryUsageReport
        '
        Me.mnuHistoryUsageReport.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuHistoryUsageReport.Image = CType(resources.GetObject("mnuHistoryUsageReport.Image"),System.Drawing.Image)
        Me.mnuHistoryUsageReport.Name = "mnuHistoryUsageReport"
        Me.mnuHistoryUsageReport.Size = New System.Drawing.Size(205, 22)
        Me.mnuHistoryUsageReport.Text = "History Usage Report"
        '
        'mnuReportPatientsAlerts
        '
        Me.mnuReportPatientsAlerts.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuReportPatientsAlerts.Image = CType(resources.GetObject("mnuReportPatientsAlerts.Image"),System.Drawing.Image)
        Me.mnuReportPatientsAlerts.Name = "mnuReportPatientsAlerts"
        Me.mnuReportPatientsAlerts.Size = New System.Drawing.Size(205, 22)
        Me.mnuReportPatientsAlerts.Text = "Prescription Alert History"
        Me.mnuReportPatientsAlerts.ToolTipText = "Alert Report"
        '
        'mnuReportClinicalDecision
        '
        Me.mnuReportClinicalDecision.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuReportClinicalDecision.Image = Global.gloEMR.My.Resources.Resources.DM_Report
        Me.mnuReportClinicalDecision.Name = "mnuReportClinicalDecision"
        Me.mnuReportClinicalDecision.Size = New System.Drawing.Size(205, 22)
        Me.mnuReportClinicalDecision.Text = "Clinical Decision Report"
        '
        'mnuReportBPMeasurement
        '
        Me.mnuReportBPMeasurement.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuReportBPMeasurement.Image = CType(resources.GetObject("mnuReportBPMeasurement.Image"),System.Drawing.Image)
        Me.mnuReportBPMeasurement.Name = "mnuReportBPMeasurement"
        Me.mnuReportBPMeasurement.Size = New System.Drawing.Size(205, 22)
        Me.mnuReportBPMeasurement.Text = "BP Measurement Report"
        '
        'mnuReportPatientHistory
        '
        Me.mnuReportPatientHistory.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuReportPatientHistory.Image = Global.gloEMR.My.Resources.Resources.HX_Report
        Me.mnuReportPatientHistory.Name = "mnuReportPatientHistory"
        Me.mnuReportPatientHistory.Size = New System.Drawing.Size(205, 22)
        Me.mnuReportPatientHistory.Text = "Patient History Report"
        '
        'mnuPatientBMIReport
        '
        Me.mnuPatientBMIReport.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuPatientBMIReport.Image = CType(resources.GetObject("mnuPatientBMIReport.Image"),System.Drawing.Image)
        Me.mnuPatientBMIReport.Name = "mnuPatientBMIReport"
        Me.mnuPatientBMIReport.Size = New System.Drawing.Size(205, 22)
        Me.mnuPatientBMIReport.Text = "Patient BMI Report"
        '
        'mnuPatientRxReport
        '
        Me.mnuPatientRxReport.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuPatientRxReport.Image = CType(resources.GetObject("mnuPatientRxReport.Image"),System.Drawing.Image)
        Me.mnuPatientRxReport.Name = "mnuPatientRxReport"
        Me.mnuPatientRxReport.Size = New System.Drawing.Size(205, 22)
        Me.mnuPatientRxReport.Text = "Patient Rx Report"
        '
        'mnuOBReport
        '
        Me.mnuOBReport.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuOBReport.Image = CType(resources.GetObject("mnuOBReport.Image"),System.Drawing.Image)
        Me.mnuOBReport.Name = "mnuOBReport"
        Me.mnuOBReport.Size = New System.Drawing.Size(205, 22)
        Me.mnuOBReport.Text = "OB Report"
        '
        'mnuPDRProgramsReports
        '
        Me.mnuPDRProgramsReports.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuPDRProgramsReports.Image = CType(resources.GetObject("mnuPDRProgramsReports.Image"),System.Drawing.Image)
        Me.mnuPDRProgramsReports.Name = "mnuPDRProgramsReports"
        Me.mnuPDRProgramsReports.Size = New System.Drawing.Size(205, 22)
        Me.mnuPDRProgramsReports.Text = "PDR Programs Report"
        '
        'mnuEPCS
        '
        Me.mnuEPCS.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuEPCSAuditTrail, Me.mnuControlledSubstanceERX})
        Me.mnuEPCS.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuEPCS.Image = CType(resources.GetObject("mnuEPCS.Image"),System.Drawing.Image)
        Me.mnuEPCS.Name = "mnuEPCS"
        Me.mnuEPCS.Size = New System.Drawing.Size(205, 22)
        Me.mnuEPCS.Text = "EPCS"
        '
        'mnuEPCSAuditTrail
        '
        Me.mnuEPCSAuditTrail.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuEPCSAuditTrail.Image = CType(resources.GetObject("mnuEPCSAuditTrail.Image"),System.Drawing.Image)
        Me.mnuEPCSAuditTrail.Name = "mnuEPCSAuditTrail"
        Me.mnuEPCSAuditTrail.Size = New System.Drawing.Size(201, 22)
        Me.mnuEPCSAuditTrail.Tag = "EPCS Audit Trail"
        Me.mnuEPCSAuditTrail.Text = "EPCS Audit Report"
        '
        'mnuControlledSubstanceERX
        '
        Me.mnuControlledSubstanceERX.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuControlledSubstanceERX.Image = CType(resources.GetObject("mnuControlledSubstanceERX.Image"),System.Drawing.Image)
        Me.mnuControlledSubstanceERX.Name = "mnuControlledSubstanceERX"
        Me.mnuControlledSubstanceERX.Size = New System.Drawing.Size(201, 22)
        Me.mnuControlledSubstanceERX.Tag = "ePrescribing Report(EPCS)"
        Me.mnuControlledSubstanceERX.Text = "ePrescribing Report(EPCS)"
        '
        'CustomizableReportsToolStripMenuItem
        '
        Me.CustomizableReportsToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.CustomizableReportsToolStripMenuItem.Image = CType(resources.GetObject("CustomizableReportsToolStripMenuItem.Image"),System.Drawing.Image)
        Me.CustomizableReportsToolStripMenuItem.Name = "CustomizableReportsToolStripMenuItem"
        Me.CustomizableReportsToolStripMenuItem.Size = New System.Drawing.Size(230, 22)
        Me.CustomizableReportsToolStripMenuItem.Text = "Customizable Reports"
        '
        'mnuRpt_LabManifest
        '
        Me.mnuRpt_LabManifest.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuRpt_LabManifest.Image = CType(resources.GetObject("mnuRpt_LabManifest.Image"),System.Drawing.Image)
        Me.mnuRpt_LabManifest.Name = "mnuRpt_LabManifest"
        Me.mnuRpt_LabManifest.Size = New System.Drawing.Size(230, 22)
        Me.mnuRpt_LabManifest.Tag = "Lab Manifest"
        Me.mnuRpt_LabManifest.Text = "Lab Manifest"
        '
        'InterfaceReportsToolStripMenuItem
        '
        Me.InterfaceReportsToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.InterfaceReportsToolStripMenuItem.Image = CType(resources.GetObject("InterfaceReportsToolStripMenuItem.Image"),System.Drawing.Image)
        Me.InterfaceReportsToolStripMenuItem.Name = "InterfaceReportsToolStripMenuItem"
        Me.InterfaceReportsToolStripMenuItem.Size = New System.Drawing.Size(230, 22)
        Me.InterfaceReportsToolStripMenuItem.Text = "Interface Reports"
        '
        'ReviewIntuitPatientsToolStripMenuItem
        '
        Me.ReviewIntuitPatientsToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.ReviewIntuitPatientsToolStripMenuItem.Image = CType(resources.GetObject("ReviewIntuitPatientsToolStripMenuItem.Image"),System.Drawing.Image)
        Me.ReviewIntuitPatientsToolStripMenuItem.Name = "ReviewIntuitPatientsToolStripMenuItem"
        Me.ReviewIntuitPatientsToolStripMenuItem.Size = New System.Drawing.Size(230, 22)
        Me.ReviewIntuitPatientsToolStripMenuItem.Text = "Review Portal Patients"
        '
        'PatientActivationReportsToolStripMenuItem
        '
        Me.PatientActivationReportsToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.PatientActivationReportsToolStripMenuItem.Image = CType(resources.GetObject("PatientActivationReportsToolStripMenuItem.Image"),System.Drawing.Image)
        Me.PatientActivationReportsToolStripMenuItem.Name = "PatientActivationReportsToolStripMenuItem"
        Me.PatientActivationReportsToolStripMenuItem.Size = New System.Drawing.Size(230, 22)
        Me.PatientActivationReportsToolStripMenuItem.Text = "Patient Activation Report"
        '
        'mnuReports_InactiveCPTSReport
        '
        Me.mnuReports_InactiveCPTSReport.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuReports_InactiveCPTSReport.Image = CType(resources.GetObject("mnuReports_InactiveCPTSReport.Image"),System.Drawing.Image)
        Me.mnuReports_InactiveCPTSReport.Name = "mnuReports_InactiveCPTSReport"
        Me.mnuReports_InactiveCPTSReport.Size = New System.Drawing.Size(230, 22)
        Me.mnuReports_InactiveCPTSReport.Text = "Inactive CPTs Report "
        '
        'mnuReports_DrugMigrationReport
        '
        Me.mnuReports_DrugMigrationReport.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuReports_DrugMigrationReport.Image = CType(resources.GetObject("mnuReports_DrugMigrationReport.Image"),System.Drawing.Image)
        Me.mnuReports_DrugMigrationReport.Name = "mnuReports_DrugMigrationReport"
        Me.mnuReports_DrugMigrationReport.Size = New System.Drawing.Size(230, 22)
        Me.mnuReports_DrugMigrationReport.Text = "Drug Migration Report"
        '
        'mnuReports_PortalPHIReview
        '
        Me.mnuReports_PortalPHIReview.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuReports_PortalPHIReview.Image = CType(resources.GetObject("mnuReports_PortalPHIReview.Image"),System.Drawing.Image)
        Me.mnuReports_PortalPHIReview.Name = "mnuReports_PortalPHIReview"
        Me.mnuReports_PortalPHIReview.Size = New System.Drawing.Size(230, 22)
        Me.mnuReports_PortalPHIReview.Text = "Portal PHI Review"
        '
        'mnu_APIHarness_Reports
        '
        Me.mnu_APIHarness_Reports.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnu_APIHarness_Reports.Image = CType(resources.GetObject("mnu_APIHarness_Reports.Image"),System.Drawing.Image)
        Me.mnu_APIHarness_Reports.Name = "mnu_APIHarness_Reports"
        Me.mnu_APIHarness_Reports.Size = New System.Drawing.Size(230, 22)
        Me.mnu_APIHarness_Reports.Text = "API Access Activation Report"
        '
        'mnuWindow
        '
        Me.mnuWindow.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CloseToolStripMenuItem})
        Me.mnuWindow.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuWindow.Name = "mnuWindow"
        Me.mnuWindow.Size = New System.Drawing.Size(57, 21)
        Me.mnuWindow.Text = "&Window"
        '
        'CloseToolStripMenuItem
        '
        Me.CloseToolStripMenuItem.Name = "CloseToolStripMenuItem"
        Me.CloseToolStripMenuItem.Size = New System.Drawing.Size(100, 22)
        Me.CloseToolStripMenuItem.Text = "Close"
        Me.CloseToolStripMenuItem.Visible = false
        '
        'mnuSharepoint
        '
        Me.mnuSharepoint.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ConnectCommunityToolStripMenuItem, Me.MyCommunityToolStripMenuItem, Me.ShareToolStripMenuItem, Me.CToolStripMenuItem, Me.GloSkypeToolStripMenuItem, Me.CommunityTimeLineToolStripMenuItem, Me.ClinicalExchangeToolStripMenuItem, Me.GloAnalyticsToolStripMenuItem, Me.mnuChangePwd})
        Me.mnuSharepoint.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuSharepoint.Name = "mnuSharepoint"
        Me.mnuSharepoint.Size = New System.Drawing.Size(86, 21)
        Me.mnuSharepoint.Text = "glo&Community"
        '
        'ConnectCommunityToolStripMenuItem
        '
        Me.ConnectCommunityToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.JoinExistingGroupToolStripMenuItem, Me.CreateNewGroupToolStripMenuItem, Me.CreateProfileToolStripMenuItem})
        Me.ConnectCommunityToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.ConnectCommunityToolStripMenuItem.Image = CType(resources.GetObject("ConnectCommunityToolStripMenuItem.Image"),System.Drawing.Image)
        Me.ConnectCommunityToolStripMenuItem.Name = "ConnectCommunityToolStripMenuItem"
        Me.ConnectCommunityToolStripMenuItem.Size = New System.Drawing.Size(170, 22)
        Me.ConnectCommunityToolStripMenuItem.Tag = "gloCommunity"
        Me.ConnectCommunityToolStripMenuItem.Text = "Community Connect"
        '
        'JoinExistingGroupToolStripMenuItem
        '
        Me.JoinExistingGroupToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.JoinExistingGroupToolStripMenuItem.Image = CType(resources.GetObject("JoinExistingGroupToolStripMenuItem.Image"),System.Drawing.Image)
        Me.JoinExistingGroupToolStripMenuItem.Name = "JoinExistingGroupToolStripMenuItem"
        Me.JoinExistingGroupToolStripMenuItem.Size = New System.Drawing.Size(176, 22)
        Me.JoinExistingGroupToolStripMenuItem.Text = "Join Existing Group"
        Me.JoinExistingGroupToolStripMenuItem.Visible = false
        '
        'CreateNewGroupToolStripMenuItem
        '
        Me.CreateNewGroupToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.CreateNewGroupToolStripMenuItem.Image = CType(resources.GetObject("CreateNewGroupToolStripMenuItem.Image"),System.Drawing.Image)
        Me.CreateNewGroupToolStripMenuItem.Name = "CreateNewGroupToolStripMenuItem"
        Me.CreateNewGroupToolStripMenuItem.Size = New System.Drawing.Size(176, 22)
        Me.CreateNewGroupToolStripMenuItem.Text = "Create New Group"
        Me.CreateNewGroupToolStripMenuItem.Visible = false
        '
        'CreateProfileToolStripMenuItem
        '
        Me.CreateProfileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.NewProfileToolStripMenuItem, Me.ModiyProfileToolStripMenuItem})
        Me.CreateProfileToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.CreateProfileToolStripMenuItem.Image = CType(resources.GetObject("CreateProfileToolStripMenuItem.Image"),System.Drawing.Image)
        Me.CreateProfileToolStripMenuItem.Name = "CreateProfileToolStripMenuItem"
        Me.CreateProfileToolStripMenuItem.Size = New System.Drawing.Size(176, 22)
        Me.CreateProfileToolStripMenuItem.Text = "Create/Modify Profile"
        Me.CreateProfileToolStripMenuItem.Visible = false
        '
        'NewProfileToolStripMenuItem
        '
        Me.NewProfileToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.NewProfileToolStripMenuItem.Image = CType(resources.GetObject("NewProfileToolStripMenuItem.Image"),System.Drawing.Image)
        Me.NewProfileToolStripMenuItem.Name = "NewProfileToolStripMenuItem"
        Me.NewProfileToolStripMenuItem.Size = New System.Drawing.Size(139, 22)
        Me.NewProfileToolStripMenuItem.Text = "New Profile"
        '
        'ModiyProfileToolStripMenuItem
        '
        Me.ModiyProfileToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.ModiyProfileToolStripMenuItem.Image = CType(resources.GetObject("ModiyProfileToolStripMenuItem.Image"),System.Drawing.Image)
        Me.ModiyProfileToolStripMenuItem.Name = "ModiyProfileToolStripMenuItem"
        Me.ModiyProfileToolStripMenuItem.Size = New System.Drawing.Size(139, 22)
        Me.ModiyProfileToolStripMenuItem.Text = "Modify Profile"
        '
        'MyCommunityToolStripMenuItem
        '
        Me.MyCommunityToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.MyCommunityToolStripMenuItem.Image = CType(resources.GetObject("MyCommunityToolStripMenuItem.Image"),System.Drawing.Image)
        Me.MyCommunityToolStripMenuItem.Name = "MyCommunityToolStripMenuItem"
        Me.MyCommunityToolStripMenuItem.Size = New System.Drawing.Size(170, 22)
        Me.MyCommunityToolStripMenuItem.Tag = "MySite"
        Me.MyCommunityToolStripMenuItem.Text = "My Community"
        '
        'ShareToolStripMenuItem
        '
        Me.ShareToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem22, Me.mnuShareTemplate, Me.mnuShareLiquid, Me.mnuShareHistory, Me.ToolStripMenuItem6, Me.ToolStripMenuItem7, Me.ToolStripMenuItem8, Me.ToolStripMenuItem9, Me.mnuShareSmartDx, Me.mnuShareShareSmartDx, Me.ToolStripMenuItem12, Me.ToolStripMenuItem13, Me.ToolStripMenuItem15, Me.ToolStripMenuItem16, Me.mnushtaskmail, Me.mnuSPReports})
        Me.ShareToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.ShareToolStripMenuItem.Image = CType(resources.GetObject("ShareToolStripMenuItem.Image"),System.Drawing.Image)
        Me.ShareToolStripMenuItem.Name = "ShareToolStripMenuItem"
        Me.ShareToolStripMenuItem.Size = New System.Drawing.Size(170, 22)
        Me.ShareToolStripMenuItem.Text = "Share"
        '
        'ToolStripMenuItem22
        '
        Me.ToolStripMenuItem22.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.ToolStripMenuItem22.Image = CType(resources.GetObject("ToolStripMenuItem22.Image"),System.Drawing.Image)
        Me.ToolStripMenuItem22.Name = "ToolStripMenuItem22"
        Me.ToolStripMenuItem22.Size = New System.Drawing.Size(203, 22)
        Me.ToolStripMenuItem22.Text = "S&end Documents"
        Me.ToolStripMenuItem22.Visible = false
        '
        'mnuShareTemplate
        '
        Me.mnuShareTemplate.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuShareTemplateUpload, Me.mnuShareTemplateDownload})
        Me.mnuShareTemplate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuShareTemplate.Image = CType(resources.GetObject("mnuShareTemplate.Image"),System.Drawing.Image)
        Me.mnuShareTemplate.Name = "mnuShareTemplate"
        Me.mnuShareTemplate.Size = New System.Drawing.Size(203, 22)
        Me.mnuShareTemplate.Text = "Template"
        '
        'mnuShareTemplateUpload
        '
        Me.mnuShareTemplateUpload.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuShareTemplateUpload.Image = CType(resources.GetObject("mnuShareTemplateUpload.Image"),System.Drawing.Image)
        Me.mnuShareTemplateUpload.Name = "mnuShareTemplateUpload"
        Me.mnuShareTemplateUpload.Size = New System.Drawing.Size(216, 22)
        Me.mnuShareTemplateUpload.Text = "Upload to gloCommunity"
        '
        'mnuShareTemplateDownload
        '
        Me.mnuShareTemplateDownload.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuShareTemplateDownload.Image = CType(resources.GetObject("mnuShareTemplateDownload.Image"),System.Drawing.Image)
        Me.mnuShareTemplateDownload.Name = "mnuShareTemplateDownload"
        Me.mnuShareTemplateDownload.Size = New System.Drawing.Size(216, 22)
        Me.mnuShareTemplateDownload.Text = "Download from gloCommunity"
        '
        'mnuShareLiquid
        '
        Me.mnuShareLiquid.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuShareLiquidDataUplaod, Me.mnuShareLiquidDataDownload})
        Me.mnuShareLiquid.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuShareLiquid.Image = CType(resources.GetObject("mnuShareLiquid.Image"),System.Drawing.Image)
        Me.mnuShareLiquid.Name = "mnuShareLiquid"
        Me.mnuShareLiquid.Size = New System.Drawing.Size(203, 22)
        Me.mnuShareLiquid.Text = "Liquid Data"
        '
        'mnuShareLiquidDataUplaod
        '
        Me.mnuShareLiquidDataUplaod.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuShareLiquidDataUplaod.Image = CType(resources.GetObject("mnuShareLiquidDataUplaod.Image"),System.Drawing.Image)
        Me.mnuShareLiquidDataUplaod.Name = "mnuShareLiquidDataUplaod"
        Me.mnuShareLiquidDataUplaod.Size = New System.Drawing.Size(210, 22)
        Me.mnuShareLiquidDataUplaod.Text = "Upload to gloCommunity"
        '
        'mnuShareLiquidDataDownload
        '
        Me.mnuShareLiquidDataDownload.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuShareLiquidDataDownload.Image = CType(resources.GetObject("mnuShareLiquidDataDownload.Image"),System.Drawing.Image)
        Me.mnuShareLiquidDataDownload.Name = "mnuShareLiquidDataDownload"
        Me.mnuShareLiquidDataDownload.Size = New System.Drawing.Size(210, 22)
        Me.mnuShareLiquidDataDownload.Text = "Dowload from gloCommunity"
        '
        'mnuShareHistory
        '
        Me.mnuShareHistory.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuShareHistoryUpload, Me.mnuShareHistoryDownload})
        Me.mnuShareHistory.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuShareHistory.Image = CType(resources.GetObject("mnuShareHistory.Image"),System.Drawing.Image)
        Me.mnuShareHistory.Name = "mnuShareHistory"
        Me.mnuShareHistory.Size = New System.Drawing.Size(203, 22)
        Me.mnuShareHistory.Text = "History"
        '
        'mnuShareHistoryUpload
        '
        Me.mnuShareHistoryUpload.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuShareHistoryUpload.Image = CType(resources.GetObject("mnuShareHistoryUpload.Image"),System.Drawing.Image)
        Me.mnuShareHistoryUpload.Name = "mnuShareHistoryUpload"
        Me.mnuShareHistoryUpload.Size = New System.Drawing.Size(216, 22)
        Me.mnuShareHistoryUpload.Text = "Upload to gloCommunity"
        '
        'mnuShareHistoryDownload
        '
        Me.mnuShareHistoryDownload.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuShareHistoryDownload.Image = CType(resources.GetObject("mnuShareHistoryDownload.Image"),System.Drawing.Image)
        Me.mnuShareHistoryDownload.Name = "mnuShareHistoryDownload"
        Me.mnuShareHistoryDownload.Size = New System.Drawing.Size(216, 22)
        Me.mnuShareHistoryDownload.Text = "Download from gloCommunity"
        '
        'ToolStripMenuItem6
        '
        Me.ToolStripMenuItem6.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuShareUploadFlowsheet, Me.mnuShareddlFlowsheet})
        Me.ToolStripMenuItem6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.ToolStripMenuItem6.Image = CType(resources.GetObject("ToolStripMenuItem6.Image"),System.Drawing.Image)
        Me.ToolStripMenuItem6.Name = "ToolStripMenuItem6"
        Me.ToolStripMenuItem6.Size = New System.Drawing.Size(203, 22)
        Me.ToolStripMenuItem6.Text = "Flowsheet"
        '
        'mnuShareUploadFlowsheet
        '
        Me.mnuShareUploadFlowsheet.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuShareUploadFlowsheet.Image = CType(resources.GetObject("mnuShareUploadFlowsheet.Image"),System.Drawing.Image)
        Me.mnuShareUploadFlowsheet.Name = "mnuShareUploadFlowsheet"
        Me.mnuShareUploadFlowsheet.Size = New System.Drawing.Size(216, 22)
        Me.mnuShareUploadFlowsheet.Text = "Upload to gloCommunity"
        '
        'mnuShareddlFlowsheet
        '
        Me.mnuShareddlFlowsheet.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuShareddlFlowsheet.Image = CType(resources.GetObject("mnuShareddlFlowsheet.Image"),System.Drawing.Image)
        Me.mnuShareddlFlowsheet.Name = "mnuShareddlFlowsheet"
        Me.mnuShareddlFlowsheet.Size = New System.Drawing.Size(216, 22)
        Me.mnuShareddlFlowsheet.Text = "Download from gloCommunity"
        '
        'ToolStripMenuItem7
        '
        Me.ToolStripMenuItem7.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuShareDmSetupUpload, Me.mnuShareDmSetupDownload})
        Me.ToolStripMenuItem7.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.ToolStripMenuItem7.Image = CType(resources.GetObject("ToolStripMenuItem7.Image"),System.Drawing.Image)
        Me.ToolStripMenuItem7.Name = "ToolStripMenuItem7"
        Me.ToolStripMenuItem7.Size = New System.Drawing.Size(203, 22)
        Me.ToolStripMenuItem7.Text = "DM Setup"
        '
        'mnuShareDmSetupUpload
        '
        Me.mnuShareDmSetupUpload.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuShareDmSetupUpload.Image = CType(resources.GetObject("mnuShareDmSetupUpload.Image"),System.Drawing.Image)
        Me.mnuShareDmSetupUpload.Name = "mnuShareDmSetupUpload"
        Me.mnuShareDmSetupUpload.Size = New System.Drawing.Size(204, 22)
        Me.mnuShareDmSetupUpload.Text = "Upload to gloCommunity"
        '
        'mnuShareDmSetupDownload
        '
        Me.mnuShareDmSetupDownload.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuShareDmSetupDownload.Image = CType(resources.GetObject("mnuShareDmSetupDownload.Image"),System.Drawing.Image)
        Me.mnuShareDmSetupDownload.Name = "mnuShareDmSetupDownload"
        Me.mnuShareDmSetupDownload.Size = New System.Drawing.Size(204, 22)
        Me.mnuShareDmSetupDownload.Text = "Download to gloCommunity"
        '
        'ToolStripMenuItem8
        '
        Me.ToolStripMenuItem8.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuShareUploadIMSetup, Me.mnuShareDownloadIMSetup})
        Me.ToolStripMenuItem8.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.ToolStripMenuItem8.Image = CType(resources.GetObject("ToolStripMenuItem8.Image"),System.Drawing.Image)
        Me.ToolStripMenuItem8.Name = "ToolStripMenuItem8"
        Me.ToolStripMenuItem8.Size = New System.Drawing.Size(203, 22)
        Me.ToolStripMenuItem8.Text = "IM Setup"
        '
        'mnuShareUploadIMSetup
        '
        Me.mnuShareUploadIMSetup.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuShareUploadIMSetup.Image = CType(resources.GetObject("mnuShareUploadIMSetup.Image"),System.Drawing.Image)
        Me.mnuShareUploadIMSetup.Name = "mnuShareUploadIMSetup"
        Me.mnuShareUploadIMSetup.Size = New System.Drawing.Size(216, 22)
        Me.mnuShareUploadIMSetup.Text = "Upload to gloCommunity"
        '
        'mnuShareDownloadIMSetup
        '
        Me.mnuShareDownloadIMSetup.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuShareDownloadIMSetup.Image = CType(resources.GetObject("mnuShareDownloadIMSetup.Image"),System.Drawing.Image)
        Me.mnuShareDownloadIMSetup.Name = "mnuShareDownloadIMSetup"
        Me.mnuShareDownloadIMSetup.Size = New System.Drawing.Size(216, 22)
        Me.mnuShareDownloadIMSetup.Text = "Download from gloCommunity"
        '
        'ToolStripMenuItem9
        '
        Me.ToolStripMenuItem9.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuShareUploadCVSetup, Me.mnuShareDownloadCVSetup})
        Me.ToolStripMenuItem9.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.ToolStripMenuItem9.Image = CType(resources.GetObject("ToolStripMenuItem9.Image"),System.Drawing.Image)
        Me.ToolStripMenuItem9.Name = "ToolStripMenuItem9"
        Me.ToolStripMenuItem9.Size = New System.Drawing.Size(203, 22)
        Me.ToolStripMenuItem9.Text = "CV Setup"
        '
        'mnuShareUploadCVSetup
        '
        Me.mnuShareUploadCVSetup.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuShareUploadCVSetup.Image = CType(resources.GetObject("mnuShareUploadCVSetup.Image"),System.Drawing.Image)
        Me.mnuShareUploadCVSetup.Name = "mnuShareUploadCVSetup"
        Me.mnuShareUploadCVSetup.Size = New System.Drawing.Size(216, 22)
        Me.mnuShareUploadCVSetup.Text = "Upload to gloCommunity"
        '
        'mnuShareDownloadCVSetup
        '
        Me.mnuShareDownloadCVSetup.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuShareDownloadCVSetup.Image = CType(resources.GetObject("mnuShareDownloadCVSetup.Image"),System.Drawing.Image)
        Me.mnuShareDownloadCVSetup.Name = "mnuShareDownloadCVSetup"
        Me.mnuShareDownloadCVSetup.Size = New System.Drawing.Size(216, 22)
        Me.mnuShareDownloadCVSetup.Text = "Download from gloCommunity"
        '
        'mnuShareSmartDx
        '
        Me.mnuShareSmartDx.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuShareUploadSmartDx, Me.mnuShareDownloadSmartDx})
        Me.mnuShareSmartDx.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuShareSmartDx.Image = CType(resources.GetObject("mnuShareSmartDx.Image"),System.Drawing.Image)
        Me.mnuShareSmartDx.Name = "mnuShareSmartDx"
        Me.mnuShareSmartDx.Size = New System.Drawing.Size(203, 22)
        Me.mnuShareSmartDx.Text = "Smart Diagnosis"
        '
        'mnuShareUploadSmartDx
        '
        Me.mnuShareUploadSmartDx.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuShareUploadSmartDx.Image = CType(resources.GetObject("mnuShareUploadSmartDx.Image"),System.Drawing.Image)
        Me.mnuShareUploadSmartDx.Name = "mnuShareUploadSmartDx"
        Me.mnuShareUploadSmartDx.Size = New System.Drawing.Size(216, 22)
        Me.mnuShareUploadSmartDx.Text = "Upload to gloCommunity"
        '
        'mnuShareDownloadSmartDx
        '
        Me.mnuShareDownloadSmartDx.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuShareDownloadSmartDx.Image = CType(resources.GetObject("mnuShareDownloadSmartDx.Image"),System.Drawing.Image)
        Me.mnuShareDownloadSmartDx.Name = "mnuShareDownloadSmartDx"
        Me.mnuShareDownloadSmartDx.Size = New System.Drawing.Size(216, 22)
        Me.mnuShareDownloadSmartDx.Text = "Download from gloCommunity"
        '
        'mnuShareShareSmartDx
        '
        Me.mnuShareShareSmartDx.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuShareUploadSmTreatment, Me.mnuShareDownloadSmTreatment})
        Me.mnuShareShareSmartDx.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuShareShareSmartDx.Image = CType(resources.GetObject("mnuShareShareSmartDx.Image"),System.Drawing.Image)
        Me.mnuShareShareSmartDx.Name = "mnuShareShareSmartDx"
        Me.mnuShareShareSmartDx.Size = New System.Drawing.Size(203, 22)
        Me.mnuShareShareSmartDx.Text = "Smart Treatment"
        '
        'mnuShareUploadSmTreatment
        '
        Me.mnuShareUploadSmTreatment.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuShareUploadSmTreatment.Image = CType(resources.GetObject("mnuShareUploadSmTreatment.Image"),System.Drawing.Image)
        Me.mnuShareUploadSmTreatment.Name = "mnuShareUploadSmTreatment"
        Me.mnuShareUploadSmTreatment.Size = New System.Drawing.Size(216, 22)
        Me.mnuShareUploadSmTreatment.Text = "Upload to gloCommunity"
        '
        'mnuShareDownloadSmTreatment
        '
        Me.mnuShareDownloadSmTreatment.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuShareDownloadSmTreatment.Image = CType(resources.GetObject("mnuShareDownloadSmTreatment.Image"),System.Drawing.Image)
        Me.mnuShareDownloadSmTreatment.Name = "mnuShareDownloadSmTreatment"
        Me.mnuShareDownloadSmTreatment.Size = New System.Drawing.Size(216, 22)
        Me.mnuShareDownloadSmTreatment.Text = "Download from gloCommunity"
        '
        'ToolStripMenuItem12
        '
        Me.ToolStripMenuItem12.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuShareUploadSmOrder, Me.mnuShareDownloadSmOrder})
        Me.ToolStripMenuItem12.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.ToolStripMenuItem12.Image = CType(resources.GetObject("ToolStripMenuItem12.Image"),System.Drawing.Image)
        Me.ToolStripMenuItem12.Name = "ToolStripMenuItem12"
        Me.ToolStripMenuItem12.Size = New System.Drawing.Size(203, 22)
        Me.ToolStripMenuItem12.Text = "Smart Order"
        '
        'mnuShareUploadSmOrder
        '
        Me.mnuShareUploadSmOrder.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuShareUploadSmOrder.Image = CType(resources.GetObject("mnuShareUploadSmOrder.Image"),System.Drawing.Image)
        Me.mnuShareUploadSmOrder.Name = "mnuShareUploadSmOrder"
        Me.mnuShareUploadSmOrder.Size = New System.Drawing.Size(216, 22)
        Me.mnuShareUploadSmOrder.Text = "Upload to gloCommunity"
        '
        'mnuShareDownloadSmOrder
        '
        Me.mnuShareDownloadSmOrder.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuShareDownloadSmOrder.Image = CType(resources.GetObject("mnuShareDownloadSmOrder.Image"),System.Drawing.Image)
        Me.mnuShareDownloadSmOrder.Name = "mnuShareDownloadSmOrder"
        Me.mnuShareDownloadSmOrder.Size = New System.Drawing.Size(216, 22)
        Me.mnuShareDownloadSmOrder.Text = "Download from gloCommunity"
        '
        'ToolStripMenuItem13
        '
        Me.ToolStripMenuItem13.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuShareUploadformglry, Me.mnuShareDlformglry})
        Me.ToolStripMenuItem13.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.ToolStripMenuItem13.Image = CType(resources.GetObject("ToolStripMenuItem13.Image"),System.Drawing.Image)
        Me.ToolStripMenuItem13.Name = "ToolStripMenuItem13"
        Me.ToolStripMenuItem13.Size = New System.Drawing.Size(203, 22)
        Me.ToolStripMenuItem13.Text = "Form Gallery"
        '
        'mnuShareUploadformglry
        '
        Me.mnuShareUploadformglry.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuShareUploadformglry.Image = CType(resources.GetObject("mnuShareUploadformglry.Image"),System.Drawing.Image)
        Me.mnuShareUploadformglry.Name = "mnuShareUploadformglry"
        Me.mnuShareUploadformglry.Size = New System.Drawing.Size(204, 22)
        Me.mnuShareUploadformglry.Text = "Upload to gloCommunity"
        '
        'mnuShareDlformglry
        '
        Me.mnuShareDlformglry.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuShareDlformglry.Image = CType(resources.GetObject("mnuShareDlformglry.Image"),System.Drawing.Image)
        Me.mnuShareDlformglry.Name = "mnuShareDlformglry"
        Me.mnuShareDlformglry.Size = New System.Drawing.Size(204, 22)
        Me.mnuShareDlformglry.Text = "Download to gloCommunity"
        '
        'ToolStripMenuItem15
        '
        Me.ToolStripMenuItem15.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnushupappconf, Me.mnushdlappconf})
        Me.ToolStripMenuItem15.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.ToolStripMenuItem15.Image = CType(resources.GetObject("ToolStripMenuItem15.Image"),System.Drawing.Image)
        Me.ToolStripMenuItem15.Name = "ToolStripMenuItem15"
        Me.ToolStripMenuItem15.Size = New System.Drawing.Size(203, 22)
        Me.ToolStripMenuItem15.Text = "Appointment Configuration"
        '
        'mnushupappconf
        '
        Me.mnushupappconf.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnushupappconf.Image = CType(resources.GetObject("mnushupappconf.Image"),System.Drawing.Image)
        Me.mnushupappconf.Name = "mnushupappconf"
        Me.mnushupappconf.Size = New System.Drawing.Size(219, 22)
        Me.mnushupappconf.Text = "Upload to gloCommunity"
        '
        'mnushdlappconf
        '
        Me.mnushdlappconf.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnushdlappconf.Image = CType(resources.GetObject("mnushdlappconf.Image"),System.Drawing.Image)
        Me.mnushdlappconf.Name = "mnushdlappconf"
        Me.mnushdlappconf.Size = New System.Drawing.Size(219, 22)
        Me.mnushdlappconf.Text = "Download from gloCommunity "
        '
        'ToolStripMenuItem16
        '
        Me.ToolStripMenuItem16.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnushupblconf, Me.mnushdlblconf})
        Me.ToolStripMenuItem16.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.ToolStripMenuItem16.Image = CType(resources.GetObject("ToolStripMenuItem16.Image"),System.Drawing.Image)
        Me.ToolStripMenuItem16.Name = "ToolStripMenuItem16"
        Me.ToolStripMenuItem16.Size = New System.Drawing.Size(203, 22)
        Me.ToolStripMenuItem16.Text = "Billing Configuration"
        '
        'mnushupblconf
        '
        Me.mnushupblconf.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnushupblconf.Image = CType(resources.GetObject("mnushupblconf.Image"),System.Drawing.Image)
        Me.mnushupblconf.Name = "mnushupblconf"
        Me.mnushupblconf.Size = New System.Drawing.Size(219, 22)
        Me.mnushupblconf.Text = "Upload to gloCommunity"
        '
        'mnushdlblconf
        '
        Me.mnushdlblconf.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnushdlblconf.Image = CType(resources.GetObject("mnushdlblconf.Image"),System.Drawing.Image)
        Me.mnushdlblconf.Name = "mnushdlblconf"
        Me.mnushdlblconf.Size = New System.Drawing.Size(219, 22)
        Me.mnushdlblconf.Text = "Download from gloCommunity "
        '
        'mnushtaskmail
        '
        Me.mnushtaskmail.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnushuptaskmail, Me.mnushdntaskmail})
        Me.mnushtaskmail.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnushtaskmail.Image = CType(resources.GetObject("mnushtaskmail.Image"),System.Drawing.Image)
        Me.mnushtaskmail.Name = "mnushtaskmail"
        Me.mnushtaskmail.Size = New System.Drawing.Size(203, 22)
        Me.mnushtaskmail.Text = "Task/Mail Configuration"
        '
        'mnushuptaskmail
        '
        Me.mnushuptaskmail.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnushuptaskmail.Image = CType(resources.GetObject("mnushuptaskmail.Image"),System.Drawing.Image)
        Me.mnushuptaskmail.Name = "mnushuptaskmail"
        Me.mnushuptaskmail.Size = New System.Drawing.Size(216, 22)
        Me.mnushuptaskmail.Text = "Upload to gloCommunity"
        '
        'mnushdntaskmail
        '
        Me.mnushdntaskmail.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnushdntaskmail.Image = CType(resources.GetObject("mnushdntaskmail.Image"),System.Drawing.Image)
        Me.mnushdntaskmail.Name = "mnushdntaskmail"
        Me.mnushdntaskmail.Size = New System.Drawing.Size(216, 22)
        Me.mnushdntaskmail.Text = "Download from gloCommunity"
        '
        'mnuSPReports
        '
        Me.mnuSPReports.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuSPReports.Image = CType(resources.GetObject("mnuSPReports.Image"),System.Drawing.Image)
        Me.mnuSPReports.Name = "mnuSPReports"
        Me.mnuSPReports.Size = New System.Drawing.Size(203, 22)
        Me.mnuSPReports.Text = "Reports"
        Me.mnuSPReports.Visible = false
        '
        'CToolStripMenuItem
        '
        Me.CToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnutemplate, Me.LiquidDataToolStripMenuItem, Me.HistoryToolStripMenuItem, Me.FlowsheetToolStripMenuItem, Me.DMSetupToolStripMenuItem, Me.IMSetupToolStripMenuItem, Me.CVSetupToolStripMenuItem, Me.mnuSmartDiag, Me.SmartTreatmentToolStripMenuItem, Me.SmartOrderToolStripMenuItem, Me.FormGalleryToolStripMenuItem, Me.PatientSummeryToolStripMenuItem, Me.AppointmentConfigurationToolStripMenuItem, Me.BillingConfigurationToolStripMenuItem, Me.TaskMailConfigurationToolStripMenuItem})
        Me.CToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.CToolStripMenuItem.Image = CType(resources.GetObject("CToolStripMenuItem.Image"),System.Drawing.Image)
        Me.CToolStripMenuItem.Name = "CToolStripMenuItem"
        Me.CToolStripMenuItem.Size = New System.Drawing.Size(170, 22)
        Me.CToolStripMenuItem.Text = "Collaborate"
        Me.CToolStripMenuItem.Visible = false
        '
        'mnutemplate
        '
        Me.mnutemplate.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.mnutemplate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnutemplate.Image = CType(resources.GetObject("mnutemplate.Image"),System.Drawing.Image)
        Me.mnutemplate.Name = "mnutemplate"
        Me.mnutemplate.Size = New System.Drawing.Size(203, 22)
        Me.mnutemplate.Text = "&Template"
        '
        'LiquidDataToolStripMenuItem
        '
        Me.LiquidDataToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.LiquidDataToolStripMenuItem.Image = CType(resources.GetObject("LiquidDataToolStripMenuItem.Image"),System.Drawing.Image)
        Me.LiquidDataToolStripMenuItem.Name = "LiquidDataToolStripMenuItem"
        Me.LiquidDataToolStripMenuItem.Size = New System.Drawing.Size(203, 22)
        Me.LiquidDataToolStripMenuItem.Text = "Liquid Data"
        '
        'HistoryToolStripMenuItem
        '
        Me.HistoryToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.HistoryToolStripMenuItem.Image = CType(resources.GetObject("HistoryToolStripMenuItem.Image"),System.Drawing.Image)
        Me.HistoryToolStripMenuItem.Name = "HistoryToolStripMenuItem"
        Me.HistoryToolStripMenuItem.Size = New System.Drawing.Size(203, 22)
        Me.HistoryToolStripMenuItem.Text = "History"
        '
        'FlowsheetToolStripMenuItem
        '
        Me.FlowsheetToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.FlowsheetToolStripMenuItem.Image = CType(resources.GetObject("FlowsheetToolStripMenuItem.Image"),System.Drawing.Image)
        Me.FlowsheetToolStripMenuItem.Name = "FlowsheetToolStripMenuItem"
        Me.FlowsheetToolStripMenuItem.Size = New System.Drawing.Size(203, 22)
        Me.FlowsheetToolStripMenuItem.Text = "Flowsheet"
        '
        'DMSetupToolStripMenuItem
        '
        Me.DMSetupToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.DMSetupToolStripMenuItem.Image = CType(resources.GetObject("DMSetupToolStripMenuItem.Image"),System.Drawing.Image)
        Me.DMSetupToolStripMenuItem.Name = "DMSetupToolStripMenuItem"
        Me.DMSetupToolStripMenuItem.Size = New System.Drawing.Size(203, 22)
        Me.DMSetupToolStripMenuItem.Text = "DM Setup"
        '
        'IMSetupToolStripMenuItem
        '
        Me.IMSetupToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.IMSetupToolStripMenuItem.Image = CType(resources.GetObject("IMSetupToolStripMenuItem.Image"),System.Drawing.Image)
        Me.IMSetupToolStripMenuItem.Name = "IMSetupToolStripMenuItem"
        Me.IMSetupToolStripMenuItem.Size = New System.Drawing.Size(203, 22)
        Me.IMSetupToolStripMenuItem.Text = "IM Setup"
        '
        'CVSetupToolStripMenuItem
        '
        Me.CVSetupToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.CVSetupToolStripMenuItem.Image = CType(resources.GetObject("CVSetupToolStripMenuItem.Image"),System.Drawing.Image)
        Me.CVSetupToolStripMenuItem.Name = "CVSetupToolStripMenuItem"
        Me.CVSetupToolStripMenuItem.Size = New System.Drawing.Size(203, 22)
        Me.CVSetupToolStripMenuItem.Text = "CV Setup"
        '
        'mnuSmartDiag
        '
        Me.mnuSmartDiag.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.mnuSmartDiag.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuSmartDiag.Image = CType(resources.GetObject("mnuSmartDiag.Image"),System.Drawing.Image)
        Me.mnuSmartDiag.Name = "mnuSmartDiag"
        Me.mnuSmartDiag.Size = New System.Drawing.Size(203, 22)
        Me.mnuSmartDiag.Text = "Smart Diagnosis"
        '
        'SmartTreatmentToolStripMenuItem
        '
        Me.SmartTreatmentToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.SmartTreatmentToolStripMenuItem.Image = CType(resources.GetObject("SmartTreatmentToolStripMenuItem.Image"),System.Drawing.Image)
        Me.SmartTreatmentToolStripMenuItem.Name = "SmartTreatmentToolStripMenuItem"
        Me.SmartTreatmentToolStripMenuItem.Size = New System.Drawing.Size(203, 22)
        Me.SmartTreatmentToolStripMenuItem.Text = "Smart Treatment"
        '
        'SmartOrderToolStripMenuItem
        '
        Me.SmartOrderToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.SmartOrderToolStripMenuItem.Image = CType(resources.GetObject("SmartOrderToolStripMenuItem.Image"),System.Drawing.Image)
        Me.SmartOrderToolStripMenuItem.Name = "SmartOrderToolStripMenuItem"
        Me.SmartOrderToolStripMenuItem.Size = New System.Drawing.Size(203, 22)
        Me.SmartOrderToolStripMenuItem.Text = "Smart Order"
        '
        'FormGalleryToolStripMenuItem
        '
        Me.FormGalleryToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.FormGalleryToolStripMenuItem.Image = CType(resources.GetObject("FormGalleryToolStripMenuItem.Image"),System.Drawing.Image)
        Me.FormGalleryToolStripMenuItem.Name = "FormGalleryToolStripMenuItem"
        Me.FormGalleryToolStripMenuItem.Size = New System.Drawing.Size(203, 22)
        Me.FormGalleryToolStripMenuItem.Text = "Form Gallery"
        '
        'PatientSummeryToolStripMenuItem
        '
        Me.PatientSummeryToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.PatientSummeryToolStripMenuItem.Image = CType(resources.GetObject("PatientSummeryToolStripMenuItem.Image"),System.Drawing.Image)
        Me.PatientSummeryToolStripMenuItem.Name = "PatientSummeryToolStripMenuItem"
        Me.PatientSummeryToolStripMenuItem.Size = New System.Drawing.Size(203, 22)
        Me.PatientSummeryToolStripMenuItem.Text = "Patient Summery"
        '
        'AppointmentConfigurationToolStripMenuItem
        '
        Me.AppointmentConfigurationToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.AppointmentConfigurationToolStripMenuItem.Image = CType(resources.GetObject("AppointmentConfigurationToolStripMenuItem.Image"),System.Drawing.Image)
        Me.AppointmentConfigurationToolStripMenuItem.Name = "AppointmentConfigurationToolStripMenuItem"
        Me.AppointmentConfigurationToolStripMenuItem.Size = New System.Drawing.Size(203, 22)
        Me.AppointmentConfigurationToolStripMenuItem.Text = "Appointment Configuration"
        '
        'BillingConfigurationToolStripMenuItem
        '
        Me.BillingConfigurationToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.BillingConfigurationToolStripMenuItem.Image = CType(resources.GetObject("BillingConfigurationToolStripMenuItem.Image"),System.Drawing.Image)
        Me.BillingConfigurationToolStripMenuItem.Name = "BillingConfigurationToolStripMenuItem"
        Me.BillingConfigurationToolStripMenuItem.Size = New System.Drawing.Size(203, 22)
        Me.BillingConfigurationToolStripMenuItem.Text = "Billing Configuration"
        '
        'TaskMailConfigurationToolStripMenuItem
        '
        Me.TaskMailConfigurationToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.TaskMailConfigurationToolStripMenuItem.Image = CType(resources.GetObject("TaskMailConfigurationToolStripMenuItem.Image"),System.Drawing.Image)
        Me.TaskMailConfigurationToolStripMenuItem.Name = "TaskMailConfigurationToolStripMenuItem"
        Me.TaskMailConfigurationToolStripMenuItem.Size = New System.Drawing.Size(203, 22)
        Me.TaskMailConfigurationToolStripMenuItem.Text = "Task/Mail Configuration"
        '
        'GloSkypeToolStripMenuItem
        '
        Me.GloSkypeToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.VoiceCallToolStripMenuItem, Me.VideoCallToolStripMenuItem, Me.TextMessageToolStripMenuItem, Me.ConferenceToolStripMenuItem})
        Me.GloSkypeToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.GloSkypeToolStripMenuItem.Image = CType(resources.GetObject("GloSkypeToolStripMenuItem.Image"),System.Drawing.Image)
        Me.GloSkypeToolStripMenuItem.Name = "GloSkypeToolStripMenuItem"
        Me.GloSkypeToolStripMenuItem.Size = New System.Drawing.Size(170, 22)
        Me.GloSkypeToolStripMenuItem.Text = "gloSkype"
        Me.GloSkypeToolStripMenuItem.Visible = false
        '
        'VoiceCallToolStripMenuItem
        '
        Me.VoiceCallToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.VoiceCallToolStripMenuItem.Image = CType(resources.GetObject("VoiceCallToolStripMenuItem.Image"),System.Drawing.Image)
        Me.VoiceCallToolStripMenuItem.Name = "VoiceCallToolStripMenuItem"
        Me.VoiceCallToolStripMenuItem.Size = New System.Drawing.Size(130, 22)
        Me.VoiceCallToolStripMenuItem.Text = "Voice Call"
        '
        'VideoCallToolStripMenuItem
        '
        Me.VideoCallToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.VideoCallToolStripMenuItem.Image = CType(resources.GetObject("VideoCallToolStripMenuItem.Image"),System.Drawing.Image)
        Me.VideoCallToolStripMenuItem.Name = "VideoCallToolStripMenuItem"
        Me.VideoCallToolStripMenuItem.Size = New System.Drawing.Size(130, 22)
        Me.VideoCallToolStripMenuItem.Text = "Video Call"
        '
        'TextMessageToolStripMenuItem
        '
        Me.TextMessageToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.TextMessageToolStripMenuItem.Image = CType(resources.GetObject("TextMessageToolStripMenuItem.Image"),System.Drawing.Image)
        Me.TextMessageToolStripMenuItem.Name = "TextMessageToolStripMenuItem"
        Me.TextMessageToolStripMenuItem.Size = New System.Drawing.Size(130, 22)
        Me.TextMessageToolStripMenuItem.Text = "Chat"
        '
        'ConferenceToolStripMenuItem
        '
        Me.ConferenceToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.ConferenceToolStripMenuItem.Image = CType(resources.GetObject("ConferenceToolStripMenuItem.Image"),System.Drawing.Image)
        Me.ConferenceToolStripMenuItem.Name = "ConferenceToolStripMenuItem"
        Me.ConferenceToolStripMenuItem.Size = New System.Drawing.Size(130, 22)
        Me.ConferenceToolStripMenuItem.Text = "Conference"
        '
        'CommunityTimeLineToolStripMenuItem
        '
        Me.CommunityTimeLineToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.JoinGloTimeLineToolStripMenuItem, Me.PatientCollaborationToolStripMenuItem, Me.CareExchangeToolStripMenuItem, Me.GloTimeLineDashboardToolStripMenuItem})
        Me.CommunityTimeLineToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.CommunityTimeLineToolStripMenuItem.Image = CType(resources.GetObject("CommunityTimeLineToolStripMenuItem.Image"),System.Drawing.Image)
        Me.CommunityTimeLineToolStripMenuItem.Name = "CommunityTimeLineToolStripMenuItem"
        Me.CommunityTimeLineToolStripMenuItem.Size = New System.Drawing.Size(170, 22)
        Me.CommunityTimeLineToolStripMenuItem.Text = "gloTimeLine"
        Me.CommunityTimeLineToolStripMenuItem.Visible = false
        '
        'JoinGloTimeLineToolStripMenuItem
        '
        Me.JoinGloTimeLineToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.JoinGloTimeLineToolStripMenuItem.Image = CType(resources.GetObject("JoinGloTimeLineToolStripMenuItem.Image"),System.Drawing.Image)
        Me.JoinGloTimeLineToolStripMenuItem.Name = "JoinGloTimeLineToolStripMenuItem"
        Me.JoinGloTimeLineToolStripMenuItem.Size = New System.Drawing.Size(184, 22)
        Me.JoinGloTimeLineToolStripMenuItem.Text = "Join gloTimeLine"
        '
        'PatientCollaborationToolStripMenuItem
        '
        Me.PatientCollaborationToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.PatientCollaborationToolStripMenuItem.Image = CType(resources.GetObject("PatientCollaborationToolStripMenuItem.Image"),System.Drawing.Image)
        Me.PatientCollaborationToolStripMenuItem.Name = "PatientCollaborationToolStripMenuItem"
        Me.PatientCollaborationToolStripMenuItem.Size = New System.Drawing.Size(184, 22)
        Me.PatientCollaborationToolStripMenuItem.Text = "Patient Collaboration"
        '
        'CareExchangeToolStripMenuItem
        '
        Me.CareExchangeToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.CareExchangeToolStripMenuItem.Image = CType(resources.GetObject("CareExchangeToolStripMenuItem.Image"),System.Drawing.Image)
        Me.CareExchangeToolStripMenuItem.Name = "CareExchangeToolStripMenuItem"
        Me.CareExchangeToolStripMenuItem.Size = New System.Drawing.Size(184, 22)
        Me.CareExchangeToolStripMenuItem.Text = "Patient Care Exchange"
        '
        'GloTimeLineDashboardToolStripMenuItem
        '
        Me.GloTimeLineDashboardToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SelectedPatientToolStripMenuItem, Me.MultiplePatientsToolStripMenuItem})
        Me.GloTimeLineDashboardToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.GloTimeLineDashboardToolStripMenuItem.Image = CType(resources.GetObject("GloTimeLineDashboardToolStripMenuItem.Image"),System.Drawing.Image)
        Me.GloTimeLineDashboardToolStripMenuItem.Name = "GloTimeLineDashboardToolStripMenuItem"
        Me.GloTimeLineDashboardToolStripMenuItem.Size = New System.Drawing.Size(184, 22)
        Me.GloTimeLineDashboardToolStripMenuItem.Text = "gloTimeLine Dashboard"
        '
        'SelectedPatientToolStripMenuItem
        '
        Me.SelectedPatientToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.SelectedPatientToolStripMenuItem.Image = CType(resources.GetObject("SelectedPatientToolStripMenuItem.Image"),System.Drawing.Image)
        Me.SelectedPatientToolStripMenuItem.Name = "SelectedPatientToolStripMenuItem"
        Me.SelectedPatientToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.SelectedPatientToolStripMenuItem.Text = "Selected Patient"
        '
        'MultiplePatientsToolStripMenuItem
        '
        Me.MultiplePatientsToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.MultiplePatientsToolStripMenuItem.Image = CType(resources.GetObject("MultiplePatientsToolStripMenuItem.Image"),System.Drawing.Image)
        Me.MultiplePatientsToolStripMenuItem.Name = "MultiplePatientsToolStripMenuItem"
        Me.MultiplePatientsToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.MultiplePatientsToolStripMenuItem.Text = "Multiple Patients"
        '
        'ClinicalExchangeToolStripMenuItem
        '
        Me.ClinicalExchangeToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem19, Me.ToolStripMenuItem18, Me.MicrosoftHealthValtToolStripMenuItem, Me.SurescriptsToolStripMenuItem})
        Me.ClinicalExchangeToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.ClinicalExchangeToolStripMenuItem.Image = CType(resources.GetObject("ClinicalExchangeToolStripMenuItem.Image"),System.Drawing.Image)
        Me.ClinicalExchangeToolStripMenuItem.Name = "ClinicalExchangeToolStripMenuItem"
        Me.ClinicalExchangeToolStripMenuItem.Size = New System.Drawing.Size(170, 22)
        Me.ClinicalExchangeToolStripMenuItem.Text = "Clinical Exchange"
        Me.ClinicalExchangeToolStripMenuItem.Visible = false
        '
        'ToolStripMenuItem19
        '
        Me.ToolStripMenuItem19.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.ToolStripMenuItem19.Image = CType(resources.GetObject("ToolStripMenuItem19.Image"),System.Drawing.Image)
        Me.ToolStripMenuItem19.Name = "ToolStripMenuItem19"
        Me.ToolStripMenuItem19.Size = New System.Drawing.Size(179, 22)
        Me.ToolStripMenuItem19.Text = "Generate CCD"
        '
        'ToolStripMenuItem18
        '
        Me.ToolStripMenuItem18.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.ToolStripMenuItem18.Image = CType(resources.GetObject("ToolStripMenuItem18.Image"),System.Drawing.Image)
        Me.ToolStripMenuItem18.Name = "ToolStripMenuItem18"
        Me.ToolStripMenuItem18.Size = New System.Drawing.Size(179, 22)
        Me.ToolStripMenuItem18.Text = "Import CCD-CCR Files"
        '
        'MicrosoftHealthValtToolStripMenuItem
        '
        Me.MicrosoftHealthValtToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem20, Me.ToolStripMenuItem21})
        Me.MicrosoftHealthValtToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.MicrosoftHealthValtToolStripMenuItem.Image = CType(resources.GetObject("MicrosoftHealthValtToolStripMenuItem.Image"),System.Drawing.Image)
        Me.MicrosoftHealthValtToolStripMenuItem.Name = "MicrosoftHealthValtToolStripMenuItem"
        Me.MicrosoftHealthValtToolStripMenuItem.Size = New System.Drawing.Size(179, 22)
        Me.MicrosoftHealthValtToolStripMenuItem.Text = "Microsoft HealthVault"
        '
        'ToolStripMenuItem20
        '
        Me.ToolStripMenuItem20.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.ToolStripMenuItem20.Image = CType(resources.GetObject("ToolStripMenuItem20.Image"),System.Drawing.Image)
        Me.ToolStripMenuItem20.Name = "ToolStripMenuItem20"
        Me.ToolStripMenuItem20.Size = New System.Drawing.Size(200, 22)
        Me.ToolStripMenuItem20.Text = "Request Access to Patient"
        Me.ToolStripMenuItem20.ToolTipText = "Request Access to Patient"
        '
        'ToolStripMenuItem21
        '
        Me.ToolStripMenuItem21.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.ToolStripMenuItem21.Image = CType(resources.GetObject("ToolStripMenuItem21.Image"),System.Drawing.Image)
        Me.ToolStripMenuItem21.Name = "ToolStripMenuItem21"
        Me.ToolStripMenuItem21.Size = New System.Drawing.Size(200, 22)
        Me.ToolStripMenuItem21.Text = "Update Patient Record"
        Me.ToolStripMenuItem21.ToolTipText = "Update Patient Record"
        '
        'SurescriptsToolStripMenuItem
        '
        Me.SurescriptsToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.SurescriptsToolStripMenuItem.Image = CType(resources.GetObject("SurescriptsToolStripMenuItem.Image"),System.Drawing.Image)
        Me.SurescriptsToolStripMenuItem.Name = "SurescriptsToolStripMenuItem"
        Me.SurescriptsToolStripMenuItem.Size = New System.Drawing.Size(179, 22)
        Me.SurescriptsToolStripMenuItem.Text = "Surescripts Exchange"
        '
        'GloAnalyticsToolStripMenuItem
        '
        Me.GloAnalyticsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.HowAmIDoingToolStripMenuItem, Me.HowAreMyPatientsToolStripMenuItem})
        Me.GloAnalyticsToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.GloAnalyticsToolStripMenuItem.Image = CType(resources.GetObject("GloAnalyticsToolStripMenuItem.Image"),System.Drawing.Image)
        Me.GloAnalyticsToolStripMenuItem.Name = "GloAnalyticsToolStripMenuItem"
        Me.GloAnalyticsToolStripMenuItem.Size = New System.Drawing.Size(170, 22)
        Me.GloAnalyticsToolStripMenuItem.Text = "gloAnalytics"
        Me.GloAnalyticsToolStripMenuItem.Visible = false
        '
        'HowAmIDoingToolStripMenuItem
        '
        Me.HowAmIDoingToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.HowAmIDoingToolStripMenuItem.Image = CType(resources.GetObject("HowAmIDoingToolStripMenuItem.Image"),System.Drawing.Image)
        Me.HowAmIDoingToolStripMenuItem.Name = "HowAmIDoingToolStripMenuItem"
        Me.HowAmIDoingToolStripMenuItem.Size = New System.Drawing.Size(178, 22)
        Me.HowAmIDoingToolStripMenuItem.Text = "How am I doing?"
        '
        'HowAreMyPatientsToolStripMenuItem
        '
        Me.HowAreMyPatientsToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.HowAreMyPatientsToolStripMenuItem.Image = CType(resources.GetObject("HowAreMyPatientsToolStripMenuItem.Image"),System.Drawing.Image)
        Me.HowAreMyPatientsToolStripMenuItem.Name = "HowAreMyPatientsToolStripMenuItem"
        Me.HowAreMyPatientsToolStripMenuItem.Size = New System.Drawing.Size(178, 22)
        Me.HowAreMyPatientsToolStripMenuItem.Text = "How are my Patients?"
        '
        'mnuChangePwd
        '
        Me.mnuChangePwd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuChangePwd.Image = CType(resources.GetObject("mnuChangePwd.Image"),System.Drawing.Image)
        Me.mnuChangePwd.Name = "mnuChangePwd"
        Me.mnuChangePwd.Size = New System.Drawing.Size(170, 22)
        Me.mnuChangePwd.Text = "Change Password"
        '
        'mnuHelp
        '
        Me.mnuHelp.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuUserGuide, Me.mnuSearch, Me.ContentsToolStripMenuItem, Me.ToolStripSeparator14, Me.mnuSupport, Me.mnuAboutUs})
        Me.mnuHelp.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuHelp.Name = "mnuHelp"
        Me.mnuHelp.Size = New System.Drawing.Size(40, 21)
        Me.mnuHelp.Text = "&Help"
        '
        'mnuUserGuide
        '
        Me.mnuUserGuide.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuUserGuide.Image = Global.gloEMR.My.Resources.Resources.User_manul
        Me.mnuUserGuide.Name = "mnuUserGuide"
        Me.mnuUserGuide.Size = New System.Drawing.Size(126, 22)
        Me.mnuUserGuide.Text = "User Guide"
        '
        'mnuSearch
        '
        Me.mnuSearch.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuSearch.Image = CType(resources.GetObject("mnuSearch.Image"),System.Drawing.Image)
        Me.mnuSearch.Name = "mnuSearch"
        Me.mnuSearch.Size = New System.Drawing.Size(126, 22)
        Me.mnuSearch.Text = "Search"
        '
        'ContentsToolStripMenuItem
        '
        Me.ContentsToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.ContentsToolStripMenuItem.Image = CType(resources.GetObject("ContentsToolStripMenuItem.Image"),System.Drawing.Image)
        Me.ContentsToolStripMenuItem.Name = "ContentsToolStripMenuItem"
        Me.ContentsToolStripMenuItem.Size = New System.Drawing.Size(126, 22)
        Me.ContentsToolStripMenuItem.Text = "Contents"
        '
        'ToolStripSeparator14
        '
        Me.ToolStripSeparator14.Name = "ToolStripSeparator14"
        Me.ToolStripSeparator14.Size = New System.Drawing.Size(123, 6)
        '
        'mnuSupport
        '
        Me.mnuSupport.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuSupport.Image = CType(resources.GetObject("mnuSupport.Image"),System.Drawing.Image)
        Me.mnuSupport.Name = "mnuSupport"
        Me.mnuSupport.Size = New System.Drawing.Size(126, 22)
        Me.mnuSupport.Text = "Support"
        '
        'mnuAboutUs
        '
        Me.mnuAboutUs.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuAboutUs.Image = CType(resources.GetObject("mnuAboutUs.Image"),System.Drawing.Image)
        Me.mnuAboutUs.Name = "mnuAboutUs"
        Me.mnuAboutUs.ShortcutKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.U),System.Windows.Forms.Keys)
        Me.mnuAboutUs.ShowShortcutKeys = false
        Me.mnuAboutUs.Size = New System.Drawing.Size(126, 22)
        Me.mnuAboutUs.Text = "About &Us"
        '
        'ToolStripMenuItem26
        '
        Me.ToolStripMenuItem26.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.ToolStripMenuItem26.Image = CType(resources.GetObject("ToolStripMenuItem26.Image"),System.Drawing.Image)
        Me.ToolStripMenuItem26.Name = "ToolStripMenuItem26"
        Me.ToolStripMenuItem26.Size = New System.Drawing.Size(208, 22)
        Me.ToolStripMenuItem26.Text = "PHQ9 Survey"
        '
        'ToolStripMenuItem27
        '
        Me.ToolStripMenuItem27.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.ToolStripMenuItem27.Image = CType(resources.GetObject("ToolStripMenuItem27.Image"),System.Drawing.Image)
        Me.ToolStripMenuItem27.Name = "ToolStripMenuItem27"
        Me.ToolStripMenuItem27.Size = New System.Drawing.Size(208, 22)
        Me.ToolStripMenuItem27.Text = "PHQ2 Survey"
        '
        'mnuView_PatientTemplates
        '
        Me.mnuView_PatientTemplates.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuView_PatientTemplates.Image = CType(resources.GetObject("mnuView_PatientTemplates.Image"),System.Drawing.Image)
        Me.mnuView_PatientTemplates.Name = "mnuView_PatientTemplates"
        Me.mnuView_PatientTemplates.Size = New System.Drawing.Size(201, 22)
        Me.mnuView_PatientTemplates.Text = "Patient Forms"
        '
        'pnlMainStatusBar
        '
        Me.pnlMainStatusBar.BackColor = System.Drawing.Color.FromArgb(CType(CType(207,Byte),Integer), CType(CType(227,Byte),Integer), CType(CType(255,Byte),Integer))
        Me.pnlMainStatusBar.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.pnlMainStatusBar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlMainStatusBar.Controls.Add(Me.Vcmd)
        Me.pnlMainStatusBar.Controls.Add(Me.DgnEngineControl1)
        Me.pnlMainStatusBar.Controls.Add(Me.StatusStrip)
        Me.pnlMainStatusBar.Controls.Add(Me.Label2)
        Me.pnlMainStatusBar.Controls.Add(Me.Label4)
        Me.pnlMainStatusBar.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlMainStatusBar.Location = New System.Drawing.Point(0, 718)
        Me.pnlMainStatusBar.Name = "pnlMainStatusBar"
        Me.pnlMainStatusBar.Size = New System.Drawing.Size(1265, 24)
        Me.pnlMainStatusBar.TabIndex = 66
        '
        'Vcmd
        '
        Me.Vcmd.Enabled = true
        Me.Vcmd.Location = New System.Drawing.Point(491, 5)
        Me.Vcmd.Name = "Vcmd"
        Me.Vcmd.OcxState = CType(resources.GetObject("Vcmd.OcxState"),System.Windows.Forms.AxHost.State)
        Me.Vcmd.Size = New System.Drawing.Size(16, 15)
        Me.Vcmd.TabIndex = 73
        Me.Vcmd.Visible = false
        '
        'DgnEngineControl1
        '
        Me.DgnEngineControl1.Enabled = true
        Me.DgnEngineControl1.Location = New System.Drawing.Point(1135, 6)
        Me.DgnEngineControl1.Name = "DgnEngineControl1"
        Me.DgnEngineControl1.OcxState = CType(resources.GetObject("DgnEngineControl1.OcxState"),System.Windows.Forms.AxHost.State)
        Me.DgnEngineControl1.Size = New System.Drawing.Size(16, 15)
        Me.DgnEngineControl1.TabIndex = 71
        Me.DgnEngineControl1.Visible = false
        '
        'StatusStrip
        '
        Me.StatusStrip.BackColor = System.Drawing.Color.Transparent
        Me.StatusStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.StatusStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.sslbl_Login, Me.sslbl_SQLServerDatabase, Me.sslbl_SingleSignOn, Me.sslbl_MessagePriority, Me.sslbl_Version, Me.sslbl_VoiceInfo, Me.sslbl_CurrentSpeaker, Me.sslbl_LastModifiedDate})
        Me.StatusStrip.Location = New System.Drawing.Point(0, 1)
        Me.StatusStrip.Name = "StatusStrip"
        Me.StatusStrip.Size = New System.Drawing.Size(1265, 22)
        Me.StatusStrip.TabIndex = 70
        Me.StatusStrip.Text = "StatusStrip1"
        '
        'sslbl_Login
        '
        Me.sslbl_Login.Image = CType(resources.GetObject("sslbl_Login.Image"),System.Drawing.Image)
        Me.sslbl_Login.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.sslbl_Login.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.sslbl_Login.Name = "sslbl_Login"
        Me.sslbl_Login.Size = New System.Drawing.Size(295, 17)
        Me.sslbl_Login.Spring = true
        Me.sslbl_Login.Text = "Admin"
        '
        'sslbl_SQLServerDatabase
        '
        Me.sslbl_SQLServerDatabase.Image = CType(resources.GetObject("sslbl_SQLServerDatabase.Image"),System.Drawing.Image)
        Me.sslbl_SQLServerDatabase.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.sslbl_SQLServerDatabase.Name = "sslbl_SQLServerDatabase"
        Me.sslbl_SQLServerDatabase.Size = New System.Drawing.Size(295, 17)
        Me.sslbl_SQLServerDatabase.Spring = true
        Me.sslbl_SQLServerDatabase.Text = "Server Database"
        Me.sslbl_SQLServerDatabase.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'sslbl_SingleSignOn
        '
        Me.sslbl_SingleSignOn.AutoToolTip = true
        Me.sslbl_SingleSignOn.ForeColor = System.Drawing.Color.Red
        Me.sslbl_SingleSignOn.Image = CType(resources.GetObject("sslbl_SingleSignOn.Image"),System.Drawing.Image)
        Me.sslbl_SingleSignOn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.sslbl_SingleSignOn.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.sslbl_SingleSignOn.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.sslbl_SingleSignOn.LinkColor = System.Drawing.Color.Red
        Me.sslbl_SingleSignOn.Name = "sslbl_SingleSignOn"
        Me.sslbl_SingleSignOn.Size = New System.Drawing.Size(306, 17)
        Me.sslbl_SingleSignOn.Text = "The user has been auto logged in with Single sign-on."
        Me.sslbl_SingleSignOn.Visible = false
        Me.sslbl_SingleSignOn.VisitedLinkColor = System.Drawing.Color.Red
        '
        'sslbl_MessagePriority
        '
        Me.sslbl_MessagePriority.AutoToolTip = true
        Me.sslbl_MessagePriority.ForeColor = System.Drawing.Color.Red
        Me.sslbl_MessagePriority.Image = CType(resources.GetObject("sslbl_MessagePriority.Image"),System.Drawing.Image)
        Me.sslbl_MessagePriority.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.sslbl_MessagePriority.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.sslbl_MessagePriority.IsLink = true
        Me.sslbl_MessagePriority.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.sslbl_MessagePriority.LinkColor = System.Drawing.Color.Red
        Me.sslbl_MessagePriority.Name = "sslbl_MessagePriority"
        Me.sslbl_MessagePriority.Size = New System.Drawing.Size(295, 17)
        Me.sslbl_MessagePriority.Spring = true
        Me.sslbl_MessagePriority.Text = "You have high priority message(s)"
        Me.sslbl_MessagePriority.VisitedLinkColor = System.Drawing.Color.Red
        '
        'sslbl_Version
        '
        Me.sslbl_Version.AutoSize = false
        Me.sslbl_Version.Image = CType(resources.GetObject("sslbl_Version.Image"),System.Drawing.Image)
        Me.sslbl_Version.Name = "sslbl_Version"
        Me.sslbl_Version.Size = New System.Drawing.Size(85, 17)
        Me.sslbl_Version.Text = "5.0.1.0"
        '
        'sslbl_VoiceInfo
        '
        Me.sslbl_VoiceInfo.AutoSize = false
        Me.sslbl_VoiceInfo.Image = CType(resources.GetObject("sslbl_VoiceInfo.Image"),System.Drawing.Image)
        Me.sslbl_VoiceInfo.Name = "sslbl_VoiceInfo"
        Me.sslbl_VoiceInfo.Size = New System.Drawing.Size(140, 17)
        Me.sslbl_VoiceInfo.Text = "Voice Not Available"
        '
        'sslbl_CurrentSpeaker
        '
        Me.sslbl_CurrentSpeaker.Image = CType(resources.GetObject("sslbl_CurrentSpeaker.Image"),System.Drawing.Image)
        Me.sslbl_CurrentSpeaker.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.sslbl_CurrentSpeaker.Name = "sslbl_CurrentSpeaker"
        Me.sslbl_CurrentSpeaker.Size = New System.Drawing.Size(16, 17)
        '
        'sslbl_LastModifiedDate
        '
        Me.sslbl_LastModifiedDate.Image = CType(resources.GetObject("sslbl_LastModifiedDate.Image"),System.Drawing.Image)
        Me.sslbl_LastModifiedDate.Name = "sslbl_LastModifiedDate"
        Me.sslbl_LastModifiedDate.Size = New System.Drawing.Size(122, 17)
        Me.sslbl_LastModifiedDate.Text = "Last Modified Date"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(0,Byte),Integer), CType(CType(45,Byte),Integer), CType(CType(150,Byte),Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label2.Location = New System.Drawing.Point(0, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(1265, 1)
        Me.Label2.TabIndex = 65
        Me.Label2.Text = "Label2"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(0,Byte),Integer), CType(CType(45,Byte),Integer), CType(CType(150,Byte),Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label4.Location = New System.Drawing.Point(0, 23)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(1265, 1)
        Me.Label4.TabIndex = 66
        Me.Label4.Text = "Label4"
        '
        'DgnMicBtn1
        '
        Me.DgnMicBtn1.Enabled = true
        Me.DgnMicBtn1.Location = New System.Drawing.Point(42, 104)
        Me.DgnMicBtn1.Name = "DgnMicBtn1"
        Me.DgnMicBtn1.OcxState = CType(resources.GetObject("DgnMicBtn1.OcxState"),System.Windows.Forms.AxHost.State)
        Me.DgnMicBtn1.Size = New System.Drawing.Size(85, 32)
        Me.DgnMicBtn1.TabIndex = 72
        Me.DgnMicBtn1.Visible = false
        '
        'uiPanelManager1
        '
        Me.uiPanelManager1.ContainerControl = Me
        Me.uiPanelManager1.DefaultPanelSettings.CaptionFormatStyle.ForeColor = System.Drawing.Color.DarkBlue
        Me.uiPanelManager1.DefaultPanelSettings.DarkCaptionFormatStyle.ForeColor = System.Drawing.Color.White
        Me.uiPanelManager1.DefaultTabGroupStyle = Janus.Windows.UI.Dock.TabGroupStyle.None
        Me.uiPanelManager1.PanelPadding.Bottom = 2
        Me.uiPanelManager1.PanelPadding.Left = 2
        Me.uiPanelManager1.PanelPadding.Right = 2
        Me.uiPanelManager1.PanelPadding.Top = 2
        Me.uiPanelManager1.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2007
        Me.pnlPatientCheckInStatus.Id = New System.Guid("f87a46d1-170b-41dd-befe-e6c814037c6e")
        Me.uiPanelManager1.Panels.Add(Me.pnlPatientCheckInStatus)
        Me.pnlLeft_Nav.Id = New System.Guid("cd93dadf-3067-4964-b42a-40d4cf93e3cf")
        Me.pnl_Messages.Id = New System.Guid("ab8cfe32-e33c-44e0-844f-b92a8f63f134")
        Me.pnlLeft_Nav.Panels.Add(Me.pnl_Messages)
        Me.pnlTasks.Id = New System.Guid("684af76f-6efb-425a-bfaf-00c05ac672c6")
        Me.pnlLeft_Nav.Panels.Add(Me.pnlTasks)
        Me.pnlNav_UnfinishedExams.Id = New System.Guid("9238349a-b1bd-4de6-b43b-6bab002e4020")
        Me.pnlLeft_Nav.Panels.Add(Me.pnlNav_UnfinishedExams)
        Me.pnlNavigator.Id = New System.Guid("7522baac-a47c-4861-a019-9448740582c2")
        Me.pnlNavigator.StaticGroup = true
        Me.pnlNav_Appointments.Id = New System.Guid("a0c0516c-25a3-4994-a68f-3a0eaafe6ad7")
        Me.pnlNavigator.Panels.Add(Me.pnlNav_Appointments)
        Me.pnlNav_Calendar.Id = New System.Guid("92e7b59b-b439-43a5-8eb8-b58b08305f9f")
        Me.pnlNavigator.Panels.Add(Me.pnlNav_Calendar)
        Me.pnlNav_Myday.Id = New System.Guid("68bb636b-0346-4da1-89cd-c61c24e24446")
        Me.pnlNavigator.Panels.Add(Me.pnlNav_Myday)
        Me.pnlNav_Triage.Id = New System.Guid("c129b0f4-997c-4f05-a91a-0b8f58df4e5c")
        Me.pnlNavigator.Panels.Add(Me.pnlNav_Triage)
        Me.pnlLeft_Nav.Panels.Add(Me.pnlNavigator)
        Me.uiPanelManager1.Panels.Add(Me.pnlLeft_Nav)
        Me.pnlPatientDetail.Id = New System.Guid("7da36751-4760-4ea4-a57e-29f7b68ed61b")
        Me.uiPanelManager1.Panels.Add(Me.pnlPatientDetail)
        Me.pnlTask.Id = New System.Guid("db080988-a441-416c-a7a9-240c68f83e8f")
        Me.pnlTask.StaticGroup = true
        Me.pnlTasks_MYTask.Id = New System.Guid("4bfba7e0-ffe7-4d33-9b13-122ec0eeca11")
        Me.pnlTask.Panels.Add(Me.pnlTasks_MYTask)
        Me.pnlTask_TaskRequests.Id = New System.Guid("74f876f4-0dae-4ce3-820c-b6de6d7fe362")
        Me.pnlTask.Panels.Add(Me.pnlTask_TaskRequests)
        Me.pnlTask_RequestsSent.Id = New System.Guid("97973618-605d-42b0-8c0d-a9eea7a76e58")
        Me.pnlTask.Panels.Add(Me.pnlTask_RequestsSent)
        Me.uiPanelManager1.Panels.Add(Me.pnlTask)
        Me.pnlGrPatientDemoCardsStatus.Id = New System.Guid("5db55736-beba-40da-b586-b0a964b855f8")
        Me.pnlPatientDemographics.Id = New System.Guid("bc334deb-1289-4b21-86b9-708eab631375")
        Me.pnlGrPatientDemoCardsStatus.Panels.Add(Me.pnlPatientDemographics)
        Me.pnlPatientCard.Id = New System.Guid("c54e9cf7-f22d-433f-9ebc-edfdf008e46c")
        Me.pnlGrPatientDemoCardsStatus.Panels.Add(Me.pnlPatientCard)
        Me.pnlJPatientStatus.Id = New System.Guid("21b713bb-7064-47b4-8de9-c56e60fc3488")
        Me.pnlGrPatientDemoCardsStatus.Panels.Add(Me.pnlJPatientStatus)
        Me.uiPanelManager1.Panels.Add(Me.pnlGrPatientDemoCardsStatus)
        Me.uiPanel0.Id = New System.Guid("f45037cc-255e-4d47-8d7a-5cf412f077d8")
        Me.uiPanelManager1.Panels.Add(Me.uiPanel0)
        '
        'Design Time Panel Info:
        '
        Me.uiPanelManager1.BeginPanelInfo
        Me.uiPanelManager1.AddDockPanelInfo(New System.Guid("f87a46d1-170b-41dd-befe-e6c814037c6e"), Janus.Windows.UI.Dock.PanelDockStyle.Right, New System.Drawing.Size(235, 626), true)
        Me.uiPanelManager1.AddDockPanelInfo(New System.Guid("cd93dadf-3067-4964-b42a-40d4cf93e3cf"), Janus.Windows.UI.Dock.PanelGroupStyle.HorizontalTiles, Janus.Windows.UI.Dock.PanelDockStyle.Left, false, New System.Drawing.Size(278, 624), true)
        Me.uiPanelManager1.AddDockPanelInfo(New System.Guid("ab8cfe32-e33c-44e0-844f-b92a8f63f134"), New System.Guid("cd93dadf-3067-4964-b42a-40d4cf93e3cf"), 122, true)
        Me.uiPanelManager1.AddDockPanelInfo(New System.Guid("684af76f-6efb-425a-bfaf-00c05ac672c6"), New System.Guid("cd93dadf-3067-4964-b42a-40d4cf93e3cf"), 120, true)
        Me.uiPanelManager1.AddDockPanelInfo(New System.Guid("9238349a-b1bd-4de6-b43b-6bab002e4020"), New System.Guid("cd93dadf-3067-4964-b42a-40d4cf93e3cf"), 96, true)
        Me.uiPanelManager1.AddDockPanelInfo(New System.Guid("7522baac-a47c-4861-a019-9448740582c2"), New System.Guid("cd93dadf-3067-4964-b42a-40d4cf93e3cf"), Janus.Windows.UI.Dock.PanelGroupStyle.OutlookNavigator, true, 274, true)
        Me.uiPanelManager1.AddDockPanelInfo(New System.Guid("a0c0516c-25a3-4994-a68f-3a0eaafe6ad7"), New System.Guid("7522baac-a47c-4861-a019-9448740582c2"), 195, true)
        Me.uiPanelManager1.AddDockPanelInfo(New System.Guid("92e7b59b-b439-43a5-8eb8-b58b08305f9f"), New System.Guid("7522baac-a47c-4861-a019-9448740582c2"), 195, true)
        Me.uiPanelManager1.AddDockPanelInfo(New System.Guid("68bb636b-0346-4da1-89cd-c61c24e24446"), New System.Guid("7522baac-a47c-4861-a019-9448740582c2"), -1, true)
        Me.uiPanelManager1.AddDockPanelInfo(New System.Guid("c129b0f4-997c-4f05-a91a-0b8f58df4e5c"), New System.Guid("7522baac-a47c-4861-a019-9448740582c2"), -1, true)
        Me.uiPanelManager1.AddDockPanelInfo(New System.Guid("7da36751-4760-4ea4-a57e-29f7b68ed61b"), Janus.Windows.UI.Dock.PanelDockStyle.Bottom, New System.Drawing.Size(957, 235), true)
        Me.uiPanelManager1.AddDockPanelInfo(New System.Guid("db080988-a441-416c-a7a9-240c68f83e8f"), Janus.Windows.UI.Dock.PanelGroupStyle.Tab, Janus.Windows.UI.Dock.PanelDockStyle.Right, true, New System.Drawing.Size(200, 200), true)
        Me.uiPanelManager1.AddDockPanelInfo(New System.Guid("4bfba7e0-ffe7-4d33-9b13-122ec0eeca11"), New System.Guid("db080988-a441-416c-a7a9-240c68f83e8f"), -1, true)
        Me.uiPanelManager1.AddDockPanelInfo(New System.Guid("74f876f4-0dae-4ce3-820c-b6de6d7fe362"), New System.Guid("db080988-a441-416c-a7a9-240c68f83e8f"), -1, true)
        Me.uiPanelManager1.AddDockPanelInfo(New System.Guid("97973618-605d-42b0-8c0d-a9eea7a76e58"), New System.Guid("db080988-a441-416c-a7a9-240c68f83e8f"), -1, true)
        Me.uiPanelManager1.AddDockPanelInfo(New System.Guid("5db55736-beba-40da-b586-b0a964b855f8"), Janus.Windows.UI.Dock.PanelGroupStyle.VerticalTiles, Janus.Windows.UI.Dock.PanelDockStyle.Bottom, false, New System.Drawing.Size(957, 332), true)
        Me.uiPanelManager1.AddDockPanelInfo(New System.Guid("bc334deb-1289-4b21-86b9-708eab631375"), New System.Guid("5db55736-beba-40da-b586-b0a964b855f8"), 372, true)
        Me.uiPanelManager1.AddDockPanelInfo(New System.Guid("c54e9cf7-f22d-433f-9ebc-edfdf008e46c"), New System.Guid("5db55736-beba-40da-b586-b0a964b855f8"), 322, true)
        Me.uiPanelManager1.AddDockPanelInfo(New System.Guid("21b713bb-7064-47b4-8de9-c56e60fc3488"), New System.Guid("5db55736-beba-40da-b586-b0a964b855f8"), 254, true)
        Me.uiPanelManager1.AddDockPanelInfo(New System.Guid("f45037cc-255e-4d47-8d7a-5cf412f077d8"), Janus.Windows.UI.Dock.PanelDockStyle.Left, New System.Drawing.Size(200, 190), true)
        Me.uiPanelManager1.AddFloatingPanelInfo(New System.Guid("7522baac-a47c-4861-a019-9448740582c2"), Janus.Windows.UI.Dock.PanelGroupStyle.OutlookNavigator, true, New System.Drawing.Point(-1, -1), New System.Drawing.Size(-1, -1), false)
        Me.uiPanelManager1.AddFloatingPanelInfo(New System.Guid("a0c0516c-25a3-4994-a68f-3a0eaafe6ad7"), New System.Drawing.Point(-1, -1), New System.Drawing.Size(-1, -1), false)
        Me.uiPanelManager1.AddFloatingPanelInfo(New System.Guid("92e7b59b-b439-43a5-8eb8-b58b08305f9f"), New System.Drawing.Point(-1, -1), New System.Drawing.Size(-1, -1), false)
        Me.uiPanelManager1.AddFloatingPanelInfo(New System.Guid("e34ba4d4-a557-42e8-8071-b4579bd8bed8"), New System.Drawing.Point(-1, -1), New System.Drawing.Size(-1, -1), false)
        Me.uiPanelManager1.AddFloatingPanelInfo(New System.Guid("92eed83e-f9ed-47a6-8f15-b913a5155303"), New System.Drawing.Point(748, 203), New System.Drawing.Size(200, 200), false)
        Me.uiPanelManager1.AddFloatingPanelInfo(New System.Guid("7da36751-4760-4ea4-a57e-29f7b68ed61b"), New System.Drawing.Point(-1, -1), New System.Drawing.Size(-1, -1), false)
        Me.uiPanelManager1.AddFloatingPanelInfo(New System.Guid("db080988-a441-416c-a7a9-240c68f83e8f"), Janus.Windows.UI.Dock.PanelGroupStyle.Tab, true, New System.Drawing.Point(-1, -1), New System.Drawing.Size(-1, -1), false)
        Me.uiPanelManager1.AddFloatingPanelInfo(New System.Guid("4bfba7e0-ffe7-4d33-9b13-122ec0eeca11"), New System.Drawing.Point(-1, -1), New System.Drawing.Size(-1, -1), false)
        Me.uiPanelManager1.AddFloatingPanelInfo(New System.Guid("74f876f4-0dae-4ce3-820c-b6de6d7fe362"), New System.Drawing.Point(-1, -1), New System.Drawing.Size(-1, -1), false)
        Me.uiPanelManager1.AddFloatingPanelInfo(New System.Guid("97973618-605d-42b0-8c0d-a9eea7a76e58"), New System.Drawing.Point(-1, -1), New System.Drawing.Size(-1, -1), false)
        Me.uiPanelManager1.AddFloatingPanelInfo(New System.Guid("b10628da-67c8-4623-abfd-de974ec887b1"), Janus.Windows.UI.Dock.PanelGroupStyle.VerticalTiles, true, New System.Drawing.Point(-1, -1), New System.Drawing.Size(-1, -1), false)
        Me.uiPanelManager1.AddFloatingPanelInfo(New System.Guid("706887a5-a6fe-43c9-8867-3f7070fada72"), New System.Drawing.Point(-1, -1), New System.Drawing.Size(-1, -1), false)
        Me.uiPanelManager1.AddFloatingPanelInfo(New System.Guid("ac8de7cb-8e67-4964-b3d4-77f142be1964"), New System.Drawing.Point(-1, -1), New System.Drawing.Size(-1, -1), false)
        Me.uiPanelManager1.AddFloatingPanelInfo(New System.Guid("d8e82135-aca2-42ab-ad20-be7d1bdf36ff"), New System.Drawing.Point(-1, -1), New System.Drawing.Size(-1, -1), false)
        Me.uiPanelManager1.AddFloatingPanelInfo(New System.Guid("11f13315-fbb9-43b0-a116-8477335f6d66"), Janus.Windows.UI.Dock.PanelGroupStyle.VerticalTiles, true, New System.Drawing.Point(-1, -1), New System.Drawing.Size(-1, -1), false)
        Me.uiPanelManager1.AddFloatingPanelInfo(New System.Guid("1531c35a-8d16-4772-8515-864972aa23ee"), New System.Drawing.Point(-1, -1), New System.Drawing.Size(-1, -1), false)
        Me.uiPanelManager1.AddFloatingPanelInfo(New System.Guid("b4aec342-b1b4-4f9a-a179-7f3d7be9544b"), New System.Drawing.Point(-1, -1), New System.Drawing.Size(-1, -1), false)
        Me.uiPanelManager1.AddFloatingPanelInfo(New System.Guid("540106a7-ccbb-45a9-94fb-28eb51cba759"), New System.Drawing.Point(-1, -1), New System.Drawing.Size(-1, -1), false)
        Me.uiPanelManager1.AddFloatingPanelInfo(New System.Guid("f93661e7-ca1d-4d57-971f-dcfef4a0c319"), New System.Drawing.Point(593, 419), New System.Drawing.Size(200, 200), false)
        Me.uiPanelManager1.AddFloatingPanelInfo(New System.Guid("74489c99-4492-4102-8a3d-0c8397640b0e"), New System.Drawing.Point(416, 554), New System.Drawing.Size(200, 200), false)
        Me.uiPanelManager1.AddFloatingPanelInfo(New System.Guid("cd5448d8-2cd7-47f6-ad29-36eb4be65bc1"), New System.Drawing.Point(401, 560), New System.Drawing.Size(200, 200), false)
        Me.uiPanelManager1.AddFloatingPanelInfo(New System.Guid("7258fa87-6d66-4987-a466-b63f61a7236b"), New System.Drawing.Point(-1, -1), New System.Drawing.Size(-1, -1), false)
        Me.uiPanelManager1.AddFloatingPanelInfo(New System.Guid("c54e9cf7-f22d-433f-9ebc-edfdf008e46c"), New System.Drawing.Point(542, 579), New System.Drawing.Size(200, 200), false)
        Me.uiPanelManager1.AddFloatingPanelInfo(New System.Guid("bc334deb-1289-4b21-86b9-708eab631375"), New System.Drawing.Point(-1, -1), New System.Drawing.Size(-1, -1), false)
        Me.uiPanelManager1.AddFloatingPanelInfo(New System.Guid("21b713bb-7064-47b4-8de9-c56e60fc3488"), New System.Drawing.Point(-1, -1), New System.Drawing.Size(-1, -1), false)
        Me.uiPanelManager1.AddFloatingPanelInfo(New System.Guid("bac59022-0917-45e1-b1ab-b2d7c942625a"), New System.Drawing.Point(-1, -1), New System.Drawing.Size(-1, -1), false)
        Me.uiPanelManager1.AddFloatingPanelInfo(New System.Guid("ca48bcd4-d76b-41ea-a3d2-bd26fa579146"), New System.Drawing.Point(-1, -1), New System.Drawing.Size(-1, -1), false)
        Me.uiPanelManager1.AddFloatingPanelInfo(New System.Guid("c129b0f4-997c-4f05-a91a-0b8f58df4e5c"), New System.Drawing.Point(-1, -1), New System.Drawing.Size(-1, -1), false)
        Me.uiPanelManager1.AddFloatingPanelInfo(New System.Guid("f2fc0db4-b617-4127-b5ab-6bdc9981e986"), New System.Drawing.Point(-1, -1), New System.Drawing.Size(-1, -1), false)
        Me.uiPanelManager1.AddFloatingPanelInfo(New System.Guid("68bb636b-0346-4da1-89cd-c61c24e24446"), New System.Drawing.Point(-1, -1), New System.Drawing.Size(-1, -1), false)
        Me.uiPanelManager1.AddFloatingPanelInfo(New System.Guid("72204bca-e0ed-4218-a48a-a15632564f6d"), New System.Drawing.Point(-1, -1), New System.Drawing.Size(-1, -1), false)
        Me.uiPanelManager1.AddFloatingPanelInfo(New System.Guid("f87a46d1-170b-41dd-befe-e6c814037c6e"), New System.Drawing.Point(945, 494), New System.Drawing.Size(200, 200), false)
        Me.uiPanelManager1.AddFloatingPanelInfo(New System.Guid("3f763c0a-36b4-4ef5-b1be-e7a6a940519a"), New System.Drawing.Point(-1, -1), New System.Drawing.Size(-1, -1), false)
        Me.uiPanelManager1.AddFloatingPanelInfo(New System.Guid("f45037cc-255e-4d47-8d7a-5cf412f077d8"), New System.Drawing.Point(-1, -1), New System.Drawing.Size(-1, -1), false)
        Me.uiPanelManager1.AddFloatingPanelInfo(New System.Guid("684af76f-6efb-425a-bfaf-00c05ac672c6"), New System.Drawing.Point(85, 303), New System.Drawing.Size(200, 200), false)
        Me.uiPanelManager1.AddFloatingPanelInfo(New System.Guid("ab8cfe32-e33c-44e0-844f-b92a8f63f134"), New System.Drawing.Point(280, 376), New System.Drawing.Size(200, 200), false)
        Me.uiPanelManager1.AddFloatingPanelInfo(New System.Guid("9238349a-b1bd-4de6-b43b-6bab002e4020"), New System.Drawing.Point(270, 580), New System.Drawing.Size(200, 200), false)
        Me.uiPanelManager1.EndPanelInfo
        '
        'pnlPatientCheckInStatus
        '
        Me.pnlPatientCheckInStatus.AutoHide = true
        Me.pnlPatientCheckInStatus.CaptionFormatStyle.FontBold = Janus.Windows.UI.TriState.[True]
        Me.pnlPatientCheckInStatus.CaptionStyle = Janus.Windows.UI.Dock.PanelCaptionStyle.Dark
        Me.pnlPatientCheckInStatus.CloseButtonVisible = Janus.Windows.UI.InheritableBoolean.[False]
        Me.pnlPatientCheckInStatus.FloatingLocation = New System.Drawing.Point(945, 494)
        Me.pnlPatientCheckInStatus.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.pnlPatientCheckInStatus.ForeColor = System.Drawing.Color.White
        Me.pnlPatientCheckInStatus.Icon = CType(resources.GetObject("pnlPatientCheckInStatus.Icon"),System.Drawing.Icon)
        Me.pnlPatientCheckInStatus.InnerContainer = Me.pnlPatientCheckInStatusContainer
        Me.pnlPatientCheckInStatus.Location = New System.Drawing.Point(912, 92)
        Me.pnlPatientCheckInStatus.Name = "pnlPatientCheckInStatus"
        Me.pnlPatientCheckInStatus.Size = New System.Drawing.Size(235, 626)
        Me.pnlPatientCheckInStatus.TabIndex = 4
        Me.pnlPatientCheckInStatus.Text = "Patient Status"
        '
        'pnlPatientCheckInStatusContainer
        '
        Me.pnlPatientCheckInStatusContainer.Controls.Add(Me.c1PatientCheckInStatus)
        Me.pnlPatientCheckInStatusContainer.Location = New System.Drawing.Point(5, 23)
        Me.pnlPatientCheckInStatusContainer.Name = "pnlPatientCheckInStatusContainer"
        Me.pnlPatientCheckInStatusContainer.Size = New System.Drawing.Size(229, 602)
        Me.pnlPatientCheckInStatusContainer.TabIndex = 0
        '
        'c1PatientCheckInStatus
        '
        Me.c1PatientCheckInStatus.BackColor = System.Drawing.Color.FromArgb(CType(CType(240,Byte),Integer), CType(CType(247,Byte),Integer), CType(CType(255,Byte),Integer))
        Me.c1PatientCheckInStatus.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.c1PatientCheckInStatus.ColumnInfo = "0,0,0,0,0,90,Columns:"
        Me.c1PatientCheckInStatus.Dock = System.Windows.Forms.DockStyle.Fill
        Me.c1PatientCheckInStatus.ExtendLastCol = true
        Me.c1PatientCheckInStatus.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.c1PatientCheckInStatus.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.c1PatientCheckInStatus.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.None
        Me.c1PatientCheckInStatus.Location = New System.Drawing.Point(0, 0)
        Me.c1PatientCheckInStatus.Name = "c1PatientCheckInStatus"
        Me.c1PatientCheckInStatus.Rows.Count = 5
        Me.c1PatientCheckInStatus.Rows.DefaultSize = 18
        Me.c1PatientCheckInStatus.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.c1PatientCheckInStatus.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        Me.c1PatientCheckInStatus.Size = New System.Drawing.Size(229, 602)
        Me.c1PatientCheckInStatus.StyleInfo = resources.GetString("c1PatientCheckInStatus.StyleInfo")
        Me.c1PatientCheckInStatus.TabIndex = 33
        '
        'pnlLeft_Nav
        '
        Me.pnlLeft_Nav.Location = New System.Drawing.Point(2, 92)
        Me.pnlLeft_Nav.Name = "pnlLeft_Nav"
        Me.pnlLeft_Nav.Size = New System.Drawing.Size(278, 624)
        Me.pnlLeft_Nav.TabIndex = 87
        '
        'pnl_Messages
        '
        Me.pnl_Messages.CaptionFormatStyle.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold)
        Me.pnl_Messages.CaptionStyle = Janus.Windows.UI.Dock.PanelCaptionStyle.Dark
        Me.pnl_Messages.CloseButtonVisible = Janus.Windows.UI.InheritableBoolean.[False]
        Me.pnl_Messages.FloatingLocation = New System.Drawing.Point(280, 376)
        Me.pnl_Messages.Image = CType(resources.GetObject("pnl_Messages.Image"),System.Drawing.Image)
        Me.pnl_Messages.InnerContainer = Me.pnl_MessagesContainer
        Me.pnl_Messages.InnerContainerFormatStyle.BackColor = System.Drawing.Color.White
        Me.pnl_Messages.Location = New System.Drawing.Point(0, 0)
        Me.pnl_Messages.Name = "pnl_Messages"
        Me.pnl_Messages.Size = New System.Drawing.Size(274, 122)
        Me.pnl_Messages.TabIndex = 4
        Me.pnl_Messages.Text = "Messages"
        '
        'pnl_MessagesContainer
        '
        Me.pnl_MessagesContainer.BackColor = System.Drawing.Color.White
        Me.pnl_MessagesContainer.Controls.Add(Me.C1Mesages)
        Me.pnl_MessagesContainer.Location = New System.Drawing.Point(1, 23)
        Me.pnl_MessagesContainer.Name = "pnl_MessagesContainer"
        Me.pnl_MessagesContainer.Size = New System.Drawing.Size(272, 98)
        Me.pnl_MessagesContainer.TabIndex = 0
        '
        'C1Mesages
        '
        Me.C1Mesages.BackColor = System.Drawing.Color.FromArgb(CType(CType(240,Byte),Integer), CType(CType(247,Byte),Integer), CType(CType(255,Byte),Integer))
        Me.C1Mesages.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.C1Mesages.ColumnInfo = "0,0,0,0,0,95,Columns:"
        Me.C1Mesages.Dock = System.Windows.Forms.DockStyle.Fill
        Me.C1Mesages.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.C1Mesages.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.C1Mesages.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut
        Me.C1Mesages.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut
        Me.C1Mesages.Location = New System.Drawing.Point(0, 0)
        Me.C1Mesages.Name = "C1Mesages"
        Me.C1Mesages.Rows.Count = 5
        Me.C1Mesages.Rows.DefaultSize = 19
        Me.C1Mesages.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.C1Mesages.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        Me.C1Mesages.Size = New System.Drawing.Size(272, 98)
        Me.C1Mesages.StyleInfo = resources.GetString("C1Mesages.StyleInfo")
        Me.C1Mesages.TabIndex = 34
        '
        'pnlTasks
        '
        Me.pnlTasks.CaptionFormatStyle.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold)
        Me.pnlTasks.CaptionStyle = Janus.Windows.UI.Dock.PanelCaptionStyle.Dark
        Me.pnlTasks.CloseButtonVisible = Janus.Windows.UI.InheritableBoolean.[False]
        Me.pnlTasks.FloatingLocation = New System.Drawing.Point(85, 303)
        Me.pnlTasks.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.pnlTasks.Image = CType(resources.GetObject("pnlTasks.Image"),System.Drawing.Image)
        Me.pnlTasks.InnerContainer = Me.pnlTasksContainer
        Me.pnlTasks.InnerContainerFormatStyle.BackColor = System.Drawing.Color.White
        Me.pnlTasks.Location = New System.Drawing.Point(0, 126)
        Me.pnlTasks.Name = "pnlTasks"
        Me.pnlTasks.Size = New System.Drawing.Size(274, 120)
        Me.pnlTasks.TabIndex = 4
        Me.pnlTasks.Text = "Tasks"
        '
        'pnlTasksContainer
        '
        Me.pnlTasksContainer.Controls.Add(Me.Panel12)
        Me.pnlTasksContainer.Controls.Add(Me.pnlViewMoreTask)
        Me.pnlTasksContainer.Controls.Add(Me.C1UserTasks)
        Me.pnlTasksContainer.Location = New System.Drawing.Point(1, 23)
        Me.pnlTasksContainer.Name = "pnlTasksContainer"
        Me.pnlTasksContainer.Size = New System.Drawing.Size(272, 96)
        Me.pnlTasksContainer.TabIndex = 0
        '
        'Panel12
        '
        Me.Panel12.Controls.Add(Me._flexGroup)
        Me.Panel12.Controls.Add(Me.Panel9)
        Me.Panel12.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel12.Location = New System.Drawing.Point(0, 0)
        Me.Panel12.Name = "Panel12"
        Me.Panel12.Size = New System.Drawing.Size(272, 76)
        Me.Panel12.TabIndex = 35
        '
        '_flexGroup
        '
        Me._flexGroup.BackColor = System.Drawing.Color.FromArgb(CType(CType(240,Byte),Integer), CType(CType(247,Byte),Integer), CType(CType(255,Byte),Integer))
        Me._flexGroup.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me._flexGroup.Dock = System.Windows.Forms.DockStyle.Fill
        Me._flexGroup.FilterRow = true
        Me._flexGroup.FlexColumnInfo = Nothing
        Me._flexGroup.FlexColumnInfoCustom = Nothing
        Me._flexGroup.FlexColumnWidthInfoCustom = "56:75:141:47:47:47:56:56:30"
        Me._flexGroup.ForeColor = System.Drawing.SystemColors.ControlLightLight
        '
        '
        '
        Me._flexGroup.Grid.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.Nodes
        Me._flexGroup.Grid.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None
        Me._flexGroup.Grid.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me._flexGroup.Grid.ColumnInfo = resources.GetString("_flexGroup.Grid.ColumnInfo")
        Me._flexGroup.Grid.Dock = System.Windows.Forms.DockStyle.Bottom
        Me._flexGroup.Grid.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw
        Me._flexGroup.Grid.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me._flexGroup.Grid.Location = New System.Drawing.Point(0, 0)
        Me._flexGroup.Grid.Name = ""
        Me._flexGroup.Grid.Rows.DefaultSize = 20
        Me._flexGroup.Grid.Rows.Fixed = 2
        Me._flexGroup.Grid.ShowCursor = true
        Me._flexGroup.Grid.Size = New System.Drawing.Size(268, 43)
        Me._flexGroup.Grid.StyleInfo = resources.GetString("_flexGroup.Grid.StyleInfo")
        Me._flexGroup.Grid.TabIndex = 1
        Me._flexGroup.Grid.Tree.Column = 1
        Me._flexGroup.Grid.Tree.Style = C1.Win.C1FlexGrid.TreeStyleFlags.Symbols
        Me._flexGroup.Image = Nothing
        Me._flexGroup.Location = New System.Drawing.Point(0, 29)
        Me._flexGroup.Name = "_flexGroup"
        Me._flexGroup.ShowGroups = false
        Me._flexGroup.Size = New System.Drawing.Size(272, 47)
        Me._flexGroup.TabIndex = 34
        Me._flexGroup.TabStop = false
        '
        'Panel9
        '
        Me.Panel9.Controls.Add(Me.Panel10)
        Me.Panel9.Controls.Add(Me.Label70)
        Me.Panel9.Controls.Add(Me.cmbFilters)
        Me.Panel9.Controls.Add(Me.Label65)
        Me.Panel9.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel9.Location = New System.Drawing.Point(0, 0)
        Me.Panel9.Name = "Panel9"
        Me.Panel9.Size = New System.Drawing.Size(272, 29)
        Me.Panel9.TabIndex = 33
        '
        'Panel10
        '
        Me.Panel10.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.Panel10.Controls.Add(Me.txtTaskSearch)
        Me.Panel10.Controls.Add(Me.Label75)
        Me.Panel10.Controls.Add(Me.pictureBox2)
        Me.Panel10.Controls.Add(Me.Label76)
        Me.Panel10.Controls.Add(Me.Label77)
        Me.Panel10.Controls.Add(Me.Label78)
        Me.Panel10.Controls.Add(Me.btnClear)
        Me.Panel10.Controls.Add(Me.Label79)
        Me.Panel10.Controls.Add(Me.Label80)
        Me.Panel10.Controls.Add(Me.Label81)
        Me.Panel10.Location = New System.Drawing.Point(178, 3)
        Me.Panel10.Name = "Panel10"
        Me.Panel10.Size = New System.Drawing.Size(87, 23)
        Me.Panel10.TabIndex = 154
        '
        'txtTaskSearch
        '
        Me.txtTaskSearch.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtTaskSearch.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtTaskSearch.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtTaskSearch.Location = New System.Drawing.Point(26, 4)
        Me.txtTaskSearch.Name = "txtTaskSearch"
        Me.txtTaskSearch.Size = New System.Drawing.Size(39, 14)
        Me.txtTaskSearch.TabIndex = 42
        '
        'Label75
        '
        Me.Label75.BackColor = System.Drawing.Color.White
        Me.Label75.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label75.Location = New System.Drawing.Point(26, 17)
        Me.Label75.Name = "Label75"
        Me.Label75.Size = New System.Drawing.Size(39, 5)
        Me.Label75.TabIndex = 43
        '
        'pictureBox2
        '
        Me.pictureBox2.BackColor = System.Drawing.Color.White
        Me.pictureBox2.Dock = System.Windows.Forms.DockStyle.Left
        Me.pictureBox2.Image = CType(resources.GetObject("pictureBox2.Image"),System.Drawing.Image)
        Me.pictureBox2.Location = New System.Drawing.Point(4, 4)
        Me.pictureBox2.Name = "pictureBox2"
        Me.pictureBox2.Size = New System.Drawing.Size(22, 18)
        Me.pictureBox2.TabIndex = 46
        Me.pictureBox2.TabStop = false
        '
        'Label76
        '
        Me.Label76.BackColor = System.Drawing.Color.White
        Me.Label76.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label76.Location = New System.Drawing.Point(1, 4)
        Me.Label76.Name = "Label76"
        Me.Label76.Size = New System.Drawing.Size(3, 18)
        Me.Label76.TabIndex = 38
        '
        'Label77
        '
        Me.Label77.BackColor = System.Drawing.Color.White
        Me.Label77.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label77.Location = New System.Drawing.Point(1, 1)
        Me.Label77.Name = "Label77"
        Me.Label77.Size = New System.Drawing.Size(64, 3)
        Me.Label77.TabIndex = 37
        '
        'Label78
        '
        Me.Label78.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label78.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label78.Location = New System.Drawing.Point(0, 1)
        Me.Label78.Name = "Label78"
        Me.Label78.Size = New System.Drawing.Size(1, 21)
        Me.Label78.TabIndex = 39
        Me.Label78.Text = "label4"
        '
        'btnClear
        '
        Me.btnClear.BackgroundImage = CType(resources.GetObject("btnClear.BackgroundImage"),System.Drawing.Image)
        Me.btnClear.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnClear.FlatAppearance.BorderSize = 0
        Me.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClear.Image = CType(resources.GetObject("btnClear.Image"),System.Drawing.Image)
        Me.btnClear.Location = New System.Drawing.Point(65, 1)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(21, 21)
        Me.btnClear.TabIndex = 41
        Me.btnClear.UseVisualStyleBackColor = true
        '
        'Label79
        '
        Me.Label79.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label79.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label79.Location = New System.Drawing.Point(86, 1)
        Me.Label79.Name = "Label79"
        Me.Label79.Size = New System.Drawing.Size(1, 21)
        Me.Label79.TabIndex = 40
        Me.Label79.Text = "label4"
        '
        'Label80
        '
        Me.Label80.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label80.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label80.Location = New System.Drawing.Point(0, 22)
        Me.Label80.Name = "Label80"
        Me.Label80.Size = New System.Drawing.Size(87, 1)
        Me.Label80.TabIndex = 45
        Me.Label80.Text = "label1"
        '
        'Label81
        '
        Me.Label81.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label81.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label81.Location = New System.Drawing.Point(0, 0)
        Me.Label81.Name = "Label81"
        Me.Label81.Size = New System.Drawing.Size(87, 1)
        Me.Label81.TabIndex = 44
        Me.Label81.Text = "label1"
        '
        'Label70
        '
        Me.Label70.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label70.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label70.Location = New System.Drawing.Point(0, 28)
        Me.Label70.Name = "Label70"
        Me.Label70.Size = New System.Drawing.Size(272, 1)
        Me.Label70.TabIndex = 153
        '
        'cmbFilters
        '
        Me.cmbFilters.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.cmbFilters.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbFilters.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.cmbFilters.FormattingEnabled = true
        Me.cmbFilters.Items.AddRange(New Object() {"", "Due Date", "Subject", "Patient Name", "Task Type", "Priority", "Status", "SSN", "Date Of Birth", "Resp"})
        Me.cmbFilters.Location = New System.Drawing.Point(73, 4)
        Me.cmbFilters.Name = "cmbFilters"
        Me.cmbFilters.Size = New System.Drawing.Size(98, 21)
        Me.cmbFilters.TabIndex = 1
        '
        'Label65
        '
        Me.Label65.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.Label65.AutoSize = true
        Me.Label65.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label65.Location = New System.Drawing.Point(10, 8)
        Me.Label65.Name = "Label65"
        Me.Label65.Size = New System.Drawing.Size(64, 13)
        Me.Label65.TabIndex = 0
        Me.Label65.Text = "Group By :"
        '
        'pnlViewMoreTask
        '
        Me.pnlViewMoreTask.Controls.Add(Label82)
        Me.pnlViewMoreTask.Controls.Add(Me.lblLinkViewMoreTask)
        Me.pnlViewMoreTask.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlViewMoreTask.Location = New System.Drawing.Point(0, 76)
        Me.pnlViewMoreTask.Name = "pnlViewMoreTask"
        Me.pnlViewMoreTask.Size = New System.Drawing.Size(272, 20)
        Me.pnlViewMoreTask.TabIndex = 35
        '
        'lblLinkViewMoreTask
        '
        Me.lblLinkViewMoreTask.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblLinkViewMoreTask.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblLinkViewMoreTask.Location = New System.Drawing.Point(0, 0)
        Me.lblLinkViewMoreTask.Name = "lblLinkViewMoreTask"
        Me.lblLinkViewMoreTask.Size = New System.Drawing.Size(272, 20)
        Me.lblLinkViewMoreTask.TabIndex = 0
        Me.lblLinkViewMoreTask.TabStop = true
        Me.lblLinkViewMoreTask.Text = "Click here to view more Task"
        Me.lblLinkViewMoreTask.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'C1UserTasks
        '
        Me.C1UserTasks.AutoGenerateColumns = false
        Me.C1UserTasks.BackColor = System.Drawing.Color.FromArgb(CType(CType(240,Byte),Integer), CType(CType(247,Byte),Integer), CType(CType(255,Byte),Integer))
        Me.C1UserTasks.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.C1UserTasks.ColumnInfo = resources.GetString("C1UserTasks.ColumnInfo")
        Me.C1UserTasks.Dock = System.Windows.Forms.DockStyle.Fill
        Me.C1UserTasks.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.C1UserTasks.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.C1UserTasks.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut
        Me.C1UserTasks.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut
        Me.C1UserTasks.Location = New System.Drawing.Point(0, 0)
        Me.C1UserTasks.Name = "C1UserTasks"
        Me.C1UserTasks.Rows.Count = 5
        Me.C1UserTasks.Rows.DefaultSize = 19
        Me.C1UserTasks.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.C1UserTasks.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        Me.C1UserTasks.Size = New System.Drawing.Size(272, 96)
        Me.C1UserTasks.StyleInfo = resources.GetString("C1UserTasks.StyleInfo")
        Me.C1UserTasks.TabIndex = 32
        '
        'pnlNav_UnfinishedExams
        '
        Me.pnlNav_UnfinishedExams.ActiveCaptionFormatStyle.FontBold = Janus.Windows.UI.TriState.[True]
        Me.pnlNav_UnfinishedExams.CaptionFormatStyle.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.pnlNav_UnfinishedExams.CaptionStyle = Janus.Windows.UI.Dock.PanelCaptionStyle.Dark
        Me.pnlNav_UnfinishedExams.CloseButtonVisible = Janus.Windows.UI.InheritableBoolean.[False]
        Me.pnlNav_UnfinishedExams.FloatingLocation = New System.Drawing.Point(270, 580)
        Me.pnlNav_UnfinishedExams.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.pnlNav_UnfinishedExams.Icon = CType(resources.GetObject("pnlNav_UnfinishedExams.Icon"),System.Drawing.Icon)
        Me.pnlNav_UnfinishedExams.InfoTextFormatStyle.FontBold = Janus.Windows.UI.TriState.[True]
        Me.pnlNav_UnfinishedExams.InnerContainer = Me.pnlNav_UnfinishedExams_Container
        Me.pnlNav_UnfinishedExams.InnerContainerFormatStyle.BackColor = System.Drawing.Color.White
        Me.pnlNav_UnfinishedExams.Location = New System.Drawing.Point(0, 250)
        Me.pnlNav_UnfinishedExams.Name = "pnlNav_UnfinishedExams"
        Me.pnlNav_UnfinishedExams.Size = New System.Drawing.Size(274, 96)
        Me.pnlNav_UnfinishedExams.TabIndex = 4
        Me.pnlNav_UnfinishedExams.TabStateStyles.DisabledFormatStyle.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.pnlNav_UnfinishedExams.TabStateStyles.DisabledFormatStyle.FontBold = Janus.Windows.UI.TriState.[True]
        Me.pnlNav_UnfinishedExams.TabStateStyles.FormatStyle.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.pnlNav_UnfinishedExams.TabStateStyles.FormatStyle.FontBold = Janus.Windows.UI.TriState.[True]
        Me.pnlNav_UnfinishedExams.TabStateStyles.HotFormatStyle.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.pnlNav_UnfinishedExams.TabStateStyles.HotFormatStyle.FontBold = Janus.Windows.UI.TriState.[True]
        Me.pnlNav_UnfinishedExams.TabStateStyles.PressedFormatStyle.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.pnlNav_UnfinishedExams.TabStateStyles.PressedFormatStyle.FontBold = Janus.Windows.UI.TriState.[False]
        Me.pnlNav_UnfinishedExams.TabStateStyles.SelectedFormatStyle.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.pnlNav_UnfinishedExams.TabStateStyles.SelectedFormatStyle.FontBold = Janus.Windows.UI.TriState.[True]
        Me.pnlNav_UnfinishedExams.Text = "Unfinished Exam"
        Me.pnlNav_UnfinishedExams.Visible = false
        '
        'pnlNav_UnfinishedExams_Container
        '
        Me.pnlNav_UnfinishedExams_Container.Controls.Add(Me.C1UnfinishedExam)
        Me.pnlNav_UnfinishedExams_Container.Controls.Add(Me.pnllblLinkUnfinishedexam)
        Me.pnlNav_UnfinishedExams_Container.Location = New System.Drawing.Point(1, 23)
        Me.pnlNav_UnfinishedExams_Container.Name = "pnlNav_UnfinishedExams_Container"
        Me.pnlNav_UnfinishedExams_Container.Size = New System.Drawing.Size(272, 72)
        Me.pnlNav_UnfinishedExams_Container.TabIndex = 0
        '
        'C1UnfinishedExam
        '
        Me.C1UnfinishedExam.BackColor = System.Drawing.Color.FromArgb(CType(CType(240,Byte),Integer), CType(CType(247,Byte),Integer), CType(CType(255,Byte),Integer))
        Me.C1UnfinishedExam.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.C1UnfinishedExam.ColumnInfo = "0,0,0,0,0,95,Columns:"
        Me.C1UnfinishedExam.Dock = System.Windows.Forms.DockStyle.Fill
        Me.C1UnfinishedExam.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.C1UnfinishedExam.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.C1UnfinishedExam.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut
        Me.C1UnfinishedExam.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut
        Me.C1UnfinishedExam.Location = New System.Drawing.Point(0, 0)
        Me.C1UnfinishedExam.Name = "C1UnfinishedExam"
        Me.C1UnfinishedExam.Rows.Count = 5
        Me.C1UnfinishedExam.Rows.DefaultSize = 19
        Me.C1UnfinishedExam.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.C1UnfinishedExam.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        Me.C1UnfinishedExam.Size = New System.Drawing.Size(272, 52)
        Me.C1UnfinishedExam.StyleInfo = resources.GetString("C1UnfinishedExam.StyleInfo")
        Me.C1UnfinishedExam.TabIndex = 33
        '
        'pnllblLinkUnfinishedexam
        '
        Me.pnllblLinkUnfinishedexam.Controls.Add(Label29)
        Me.pnllblLinkUnfinishedexam.Controls.Add(Me.lblLinkUnfinishedAll)
        Me.pnllblLinkUnfinishedexam.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnllblLinkUnfinishedexam.Location = New System.Drawing.Point(0, 52)
        Me.pnllblLinkUnfinishedexam.Name = "pnllblLinkUnfinishedexam"
        Me.pnllblLinkUnfinishedexam.Size = New System.Drawing.Size(272, 20)
        Me.pnllblLinkUnfinishedexam.TabIndex = 36
        '
        'lblLinkUnfinishedAll
        '
        Me.lblLinkUnfinishedAll.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblLinkUnfinishedAll.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblLinkUnfinishedAll.Location = New System.Drawing.Point(0, 0)
        Me.lblLinkUnfinishedAll.Name = "lblLinkUnfinishedAll"
        Me.lblLinkUnfinishedAll.Size = New System.Drawing.Size(272, 20)
        Me.lblLinkUnfinishedAll.TabIndex = 0
        Me.lblLinkUnfinishedAll.TabStop = true
        Me.lblLinkUnfinishedAll.Text = "Click here to view all Unfinished Exam"
        Me.lblLinkUnfinishedAll.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pnlNavigator
        '
        Me.pnlNavigator.ActiveCaptionFormatStyle.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.pnlNavigator.CaptionFormatStyle.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.pnlNavigator.CloseButtonVisible = Janus.Windows.UI.InheritableBoolean.[False]
        Me.pnlNavigator.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.pnlNavigator.GroupStyle = Janus.Windows.UI.Dock.PanelGroupStyle.OutlookNavigator
        Me.pnlNavigator.InfoTextFormatStyle.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.pnlNavigator.LargeIcon = CType(resources.GetObject("pnlNavigator.LargeIcon"),System.Drawing.Icon)
        Me.pnlNavigator.Location = New System.Drawing.Point(0, 350)
        Me.pnlNavigator.Name = "pnlNavigator"
        Me.pnlNavigator.SelectedPanel = Me.pnlNav_Appointments
        Me.pnlNavigator.Size = New System.Drawing.Size(274, 274)
        Me.pnlNavigator.TabIndex = 4
        Me.pnlNavigator.TabStateStyles.DisabledFormatStyle.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.pnlNavigator.TabStateStyles.DisabledFormatStyle.FontBold = Janus.Windows.UI.TriState.[True]
        Me.pnlNavigator.TabStateStyles.FormatStyle.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.pnlNavigator.TabStateStyles.FormatStyle.FontBold = Janus.Windows.UI.TriState.[True]
        Me.pnlNavigator.TabStateStyles.HotFormatStyle.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.pnlNavigator.TabStateStyles.HotFormatStyle.FontBold = Janus.Windows.UI.TriState.[True]
        Me.pnlNavigator.TabStateStyles.PressedFormatStyle.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.pnlNavigator.TabStateStyles.PressedFormatStyle.FontBold = Janus.Windows.UI.TriState.[True]
        Me.pnlNavigator.TabStateStyles.SelectedFormatStyle.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.pnlNavigator.TabStateStyles.SelectedFormatStyle.FontBold = Janus.Windows.UI.TriState.[True]
        Me.pnlNavigator.Visible = false
        '
        'pnlNav_Appointments
        '
        Me.pnlNav_Appointments.AutoHideButtonVisible = Janus.Windows.UI.InheritableBoolean.[True]
        Me.pnlNav_Appointments.AutoHideTabDisplay = Janus.Windows.UI.Dock.TabDisplayMode.ImageAndTextOnSelected
        Me.pnlNav_Appointments.CaptionFormatStyle.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.pnlNav_Appointments.CaptionFormatStyle.FontBold = Janus.Windows.UI.TriState.[True]
        Me.pnlNav_Appointments.CaptionStyle = Janus.Windows.UI.Dock.PanelCaptionStyle.Dark
        Me.pnlNav_Appointments.CloseButtonVisible = Janus.Windows.UI.InheritableBoolean.[False]
        Me.pnlNav_Appointments.Icon = CType(resources.GetObject("pnlNav_Appointments.Icon"),System.Drawing.Icon)
        Me.pnlNav_Appointments.InnerContainer = Me.pnlNav_AppointmentsContainer
        Me.pnlNav_Appointments.InnerContainerFormatStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(250,Byte),Integer), CType(CType(251,Byte),Integer), CType(CType(254,Byte),Integer))
        Me.pnlNav_Appointments.InnerContainerFormatStyle.BackColorGradient = System.Drawing.Color.FromArgb(CType(CType(227,Byte),Integer), CType(CType(241,Byte),Integer), CType(CType(255,Byte),Integer))
        Me.pnlNav_Appointments.InnerContainerFormatStyle.BackgroundGradientMode = Janus.Windows.UI.BackgroundGradientMode.Vertical
        Me.pnlNav_Appointments.InnerContainerFormatStyle.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.pnlNav_Appointments.LargeIcon = CType(resources.GetObject("pnlNav_Appointments.LargeIcon"),System.Drawing.Icon)
        Me.pnlNav_Appointments.Location = New System.Drawing.Point(0, 0)
        Me.pnlNav_Appointments.Name = "pnlNav_Appointments"
        Me.pnlNav_Appointments.Size = New System.Drawing.Size(274, 106)
        Me.pnlNav_Appointments.TabIndex = 4
        Me.pnlNav_Appointments.TabStateStyles.DisabledFormatStyle.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.pnlNav_Appointments.TabStateStyles.DisabledFormatStyle.FontBold = Janus.Windows.UI.TriState.[True]
        Me.pnlNav_Appointments.TabStateStyles.FormatStyle.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.pnlNav_Appointments.TabStateStyles.HotFormatStyle.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.pnlNav_Appointments.TabStateStyles.PressedFormatStyle.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.pnlNav_Appointments.TabStateStyles.PressedFormatStyle.FontBold = Janus.Windows.UI.TriState.[True]
        Me.pnlNav_Appointments.TabStateStyles.SelectedFormatStyle.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.pnlNav_Appointments.TabStateStyles.SelectedFormatStyle.FontBold = Janus.Windows.UI.TriState.[True]
        Me.pnlNav_Appointments.Text = "Appointments"
        '
        'pnlNav_AppointmentsContainer
        '
        Me.pnlNav_AppointmentsContainer.BackColor = System.Drawing.Color.White
        Me.pnlNav_AppointmentsContainer.Controls.Add(Me.C1Appointments)
        Me.pnlNav_AppointmentsContainer.Location = New System.Drawing.Point(1, 24)
        Me.pnlNav_AppointmentsContainer.Name = "pnlNav_AppointmentsContainer"
        Me.pnlNav_AppointmentsContainer.Size = New System.Drawing.Size(272, 82)
        Me.pnlNav_AppointmentsContainer.TabIndex = 0
        '
        'C1Appointments
        '
        Me.C1Appointments.BackColor = System.Drawing.Color.FromArgb(CType(CType(240,Byte),Integer), CType(CType(247,Byte),Integer), CType(CType(255,Byte),Integer))
        Me.C1Appointments.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.C1Appointments.ColumnInfo = "0,0,0,0,0,95,Columns:"
        Me.C1Appointments.Dock = System.Windows.Forms.DockStyle.Fill
        Me.C1Appointments.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.C1Appointments.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.C1Appointments.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut
        Me.C1Appointments.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut
        Me.C1Appointments.Location = New System.Drawing.Point(0, 0)
        Me.C1Appointments.Name = "C1Appointments"
        Me.C1Appointments.Rows.Count = 5
        Me.C1Appointments.Rows.DefaultSize = 19
        Me.C1Appointments.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.C1Appointments.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        Me.C1Appointments.Size = New System.Drawing.Size(272, 82)
        Me.C1Appointments.StyleInfo = resources.GetString("C1Appointments.StyleInfo")
        Me.C1Appointments.TabIndex = 35
        '
        'pnlNav_Calendar
        '
        Me.pnlNav_Calendar.BackColor = System.Drawing.Color.FromArgb(CType(CType(239,Byte),Integer), CType(CType(246,Byte),Integer), CType(CType(255,Byte),Integer))
        Me.pnlNav_Calendar.CaptionFormatStyle.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.pnlNav_Calendar.CaptionStyle = Janus.Windows.UI.Dock.PanelCaptionStyle.Dark
        Me.pnlNav_Calendar.CloseButtonVisible = Janus.Windows.UI.InheritableBoolean.[False]
        Me.pnlNav_Calendar.ForeColor = System.Drawing.SystemColors.WindowText
        Me.pnlNav_Calendar.Icon = CType(resources.GetObject("pnlNav_Calendar.Icon"),System.Drawing.Icon)
        Me.pnlNav_Calendar.InnerContainer = Me.pnlNav_CalendarContainer
        Me.pnlNav_Calendar.InnerContainerFormatStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(250,Byte),Integer), CType(CType(251,Byte),Integer), CType(CType(254,Byte),Integer))
        Me.pnlNav_Calendar.InnerContainerFormatStyle.BackColorGradient = System.Drawing.Color.FromArgb(CType(CType(227,Byte),Integer), CType(CType(241,Byte),Integer), CType(CType(255,Byte),Integer))
        Me.pnlNav_Calendar.InnerContainerFormatStyle.BackgroundGradientMode = Janus.Windows.UI.BackgroundGradientMode.Vertical
        Me.pnlNav_Calendar.InnerContainerFormatStyle.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.pnlNav_Calendar.LargeIcon = CType(resources.GetObject("pnlNav_Calendar.LargeIcon"),System.Drawing.Icon)
        Me.pnlNav_Calendar.Location = New System.Drawing.Point(0, 0)
        Me.pnlNav_Calendar.Name = "pnlNav_Calendar"
        Me.pnlNav_Calendar.Size = New System.Drawing.Size(274, 106)
        Me.pnlNav_Calendar.TabIndex = 4
        Me.pnlNav_Calendar.TabStateStyles.DisabledFormatStyle.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.pnlNav_Calendar.TabStateStyles.DisabledFormatStyle.ForeColor = System.Drawing.Color.Red
        Me.pnlNav_Calendar.TabStateStyles.FormatStyle.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.pnlNav_Calendar.TabStateStyles.HotFormatStyle.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.pnlNav_Calendar.TabStateStyles.PressedFormatStyle.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.pnlNav_Calendar.TabStateStyles.SelectedFormatStyle.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.pnlNav_Calendar.Text = "Calendar"
        '
        'pnlNav_CalendarContainer
        '
        Me.pnlNav_CalendarContainer.Location = New System.Drawing.Point(1, 24)
        Me.pnlNav_CalendarContainer.Name = "pnlNav_CalendarContainer"
        Me.pnlNav_CalendarContainer.Size = New System.Drawing.Size(272, 82)
        Me.pnlNav_CalendarContainer.TabIndex = 0
        '
        'pnlNav_Myday
        '
        Me.pnlNav_Myday.CaptionFormatStyle.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.pnlNav_Myday.CaptionStyle = Janus.Windows.UI.Dock.PanelCaptionStyle.Dark
        Me.pnlNav_Myday.Icon = CType(resources.GetObject("pnlNav_Myday.Icon"),System.Drawing.Icon)
        Me.pnlNav_Myday.InnerContainer = Me.pnlNav_MydayContainer
        Me.pnlNav_Myday.InnerContainerFormatStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(250,Byte),Integer), CType(CType(254,Byte),Integer), CType(CType(254,Byte),Integer))
        Me.pnlNav_Myday.InnerContainerFormatStyle.BackColorGradient = System.Drawing.Color.FromArgb(CType(CType(227,Byte),Integer), CType(CType(241,Byte),Integer), CType(CType(255,Byte),Integer))
        Me.pnlNav_Myday.InnerContainerFormatStyle.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.pnlNav_Myday.InnerContainerFormatStyle.FontBold = Janus.Windows.UI.TriState.[False]
        Me.pnlNav_Myday.LargeIcon = CType(resources.GetObject("pnlNav_Myday.LargeIcon"),System.Drawing.Icon)
        Me.pnlNav_Myday.Location = New System.Drawing.Point(0, 0)
        Me.pnlNav_Myday.Name = "pnlNav_Myday"
        Me.pnlNav_Myday.Size = New System.Drawing.Size(274, 106)
        Me.pnlNav_Myday.TabIndex = 4
        Me.pnlNav_Myday.TabStateStyles.DisabledFormatStyle.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.pnlNav_Myday.TabStateStyles.FormatStyle.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.pnlNav_Myday.TabStateStyles.HotFormatStyle.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.pnlNav_Myday.TabStateStyles.PressedFormatStyle.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.pnlNav_Myday.TabStateStyles.SelectedFormatStyle.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.pnlNav_Myday.Text = "My Day"
        '
        'pnlNav_MydayContainer
        '
        Me.pnlNav_MydayContainer.Location = New System.Drawing.Point(1, 24)
        Me.pnlNav_MydayContainer.Name = "pnlNav_MydayContainer"
        Me.pnlNav_MydayContainer.Size = New System.Drawing.Size(272, 82)
        Me.pnlNav_MydayContainer.TabIndex = 0
        '
        'pnlNav_Triage
        '
        Me.pnlNav_Triage.CaptionFormatStyle.FontBold = Janus.Windows.UI.TriState.[True]
        Me.pnlNav_Triage.CaptionStyle = Janus.Windows.UI.Dock.PanelCaptionStyle.Dark
        Me.pnlNav_Triage.CaptionVisible = Janus.Windows.UI.InheritableBoolean.[True]
        Me.pnlNav_Triage.Icon = CType(resources.GetObject("pnlNav_Triage.Icon"),System.Drawing.Icon)
        Me.pnlNav_Triage.InnerContainer = Me.pnlNav_TriageContainer
        Me.pnlNav_Triage.InnerContainerFormatStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(250,Byte),Integer), CType(CType(251,Byte),Integer), CType(CType(254,Byte),Integer))
        Me.pnlNav_Triage.InnerContainerFormatStyle.BackColorGradient = System.Drawing.Color.FromArgb(CType(CType(227,Byte),Integer), CType(CType(241,Byte),Integer), CType(CType(255,Byte),Integer))
        Me.pnlNav_Triage.InnerContainerFormatStyle.BackgroundGradientMode = Janus.Windows.UI.BackgroundGradientMode.Vertical
        Me.pnlNav_Triage.LargeIcon = CType(resources.GetObject("pnlNav_Triage.LargeIcon"),System.Drawing.Icon)
        Me.pnlNav_Triage.Location = New System.Drawing.Point(0, 0)
        Me.pnlNav_Triage.Name = "pnlNav_Triage"
        Me.pnlNav_Triage.Size = New System.Drawing.Size(274, 106)
        Me.pnlNav_Triage.TabIndex = 4
        Me.pnlNav_Triage.TabStateStyles.DisabledFormatStyle.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.pnlNav_Triage.TabStateStyles.FormatStyle.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.pnlNav_Triage.TabStateStyles.HotFormatStyle.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.pnlNav_Triage.TabStateStyles.PressedFormatStyle.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.pnlNav_Triage.TabStateStyles.SelectedFormatStyle.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.pnlNav_Triage.Text = "Triage"
        '
        'pnlNav_TriageContainer
        '
        Me.pnlNav_TriageContainer.Controls.Add(Me.C1Triage)
        Me.pnlNav_TriageContainer.Location = New System.Drawing.Point(1, 24)
        Me.pnlNav_TriageContainer.Name = "pnlNav_TriageContainer"
        Me.pnlNav_TriageContainer.Size = New System.Drawing.Size(272, 82)
        Me.pnlNav_TriageContainer.TabIndex = 0
        '
        'C1Triage
        '
        Me.C1Triage.BackColor = System.Drawing.Color.FromArgb(CType(CType(240,Byte),Integer), CType(CType(247,Byte),Integer), CType(CType(255,Byte),Integer))
        Me.C1Triage.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.C1Triage.ColumnInfo = "0,0,0,0,0,95,Columns:"
        Me.C1Triage.Dock = System.Windows.Forms.DockStyle.Fill
        Me.C1Triage.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.C1Triage.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.C1Triage.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut
        Me.C1Triage.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut
        Me.C1Triage.Location = New System.Drawing.Point(0, 0)
        Me.C1Triage.Name = "C1Triage"
        Me.C1Triage.Rows.Count = 5
        Me.C1Triage.Rows.DefaultSize = 19
        Me.C1Triage.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.C1Triage.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        Me.C1Triage.Size = New System.Drawing.Size(272, 82)
        Me.C1Triage.StyleInfo = resources.GetString("C1Triage.StyleInfo")
        Me.C1Triage.TabIndex = 33
        '
        'pnlPatientDetail
        '
        Me.pnlPatientDetail.BackColor = System.Drawing.Color.FromArgb(CType(CType(239,Byte),Integer), CType(CType(246,Byte),Integer), CType(CType(255,Byte),Integer))
        Me.pnlPatientDetail.CaptionFormatStyle.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.pnlPatientDetail.CaptionStyle = Janus.Windows.UI.Dock.PanelCaptionStyle.Dark
        Me.pnlPatientDetail.CloseButtonVisible = Janus.Windows.UI.InheritableBoolean.[False]
        Me.pnlPatientDetail.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.pnlPatientDetail.Image = CType(resources.GetObject("pnlPatientDetail.Image"),System.Drawing.Image)
        Me.pnlPatientDetail.InnerContainer = Me.pnlPatientDetailContainer
        Me.pnlPatientDetail.InnerContainerFormatStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(250,Byte),Integer), CType(CType(251,Byte),Integer), CType(CType(254,Byte),Integer))
        Me.pnlPatientDetail.InnerContainerFormatStyle.BackColorGradient = System.Drawing.Color.FromArgb(CType(CType(227,Byte),Integer), CType(CType(241,Byte),Integer), CType(CType(255,Byte),Integer))
        Me.pnlPatientDetail.InnerContainerFormatStyle.BackgroundGradientMode = Janus.Windows.UI.BackgroundGradientMode.Vertical
        Me.pnlPatientDetail.Location = New System.Drawing.Point(280, 481)
        Me.pnlPatientDetail.Name = "pnlPatientDetail"
        Me.pnlPatientDetail.Size = New System.Drawing.Size(957, 235)
        Me.pnlPatientDetail.TabIndex = 4
        Me.pnlPatientDetail.Text = "Patient Details"
        '
        'pnlPatientDetailContainer
        '
        Me.pnlPatientDetailContainer.BackColor = System.Drawing.Color.FromArgb(CType(CType(204,Byte),Integer), CType(CType(224,Byte),Integer), CType(CType(248,Byte),Integer))
        Me.pnlPatientDetailContainer.Controls.Add(Me.pnlPatientDetails)
        Me.pnlPatientDetailContainer.Controls.Add(Me.pnlPatientDetailsHeaders)
        Me.pnlPatientDetailContainer.ForeColor = System.Drawing.Color.DarkBlue
        Me.pnlPatientDetailContainer.Location = New System.Drawing.Point(1, 27)
        Me.pnlPatientDetailContainer.Name = "pnlPatientDetailContainer"
        Me.pnlPatientDetailContainer.Size = New System.Drawing.Size(955, 207)
        Me.pnlPatientDetailContainer.TabIndex = 0
        '
        'pnlPatientDetails
        '
        Me.pnlPatientDetails.BackColor = System.Drawing.Color.Transparent
        Me.pnlPatientDetails.Controls.Add(Me.C1dgPatientDetails)
        Me.pnlPatientDetails.Controls.Add(Me.DgnMicBtn1)
        Me.pnlPatientDetails.Controls.Add(Me.SplitterOrderComments)
        Me.pnlPatientDetails.Controls.Add(Me.txtOrderComment)
        Me.pnlPatientDetails.Controls.Add(Me.pnlArchiveInfo)
        Me.pnlPatientDetails.Controls.Add(Me.pnlSearchFilter)
        Me.pnlPatientDetails.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlPatientDetails.Location = New System.Drawing.Point(0, 25)
        Me.pnlPatientDetails.Name = "pnlPatientDetails"
        Me.pnlPatientDetails.Size = New System.Drawing.Size(955, 182)
        Me.pnlPatientDetails.TabIndex = 0
        '
        'C1dgPatientDetails
        '
        Me.C1dgPatientDetails.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.C1dgPatientDetails.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None
        Me.C1dgPatientDetails.BackColor = System.Drawing.Color.White
        Me.C1dgPatientDetails.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.C1dgPatientDetails.ColumnInfo = resources.GetString("C1dgPatientDetails.ColumnInfo")
        Me.C1dgPatientDetails.Dock = System.Windows.Forms.DockStyle.Fill
        Me.C1dgPatientDetails.EditOptions = C1.Win.C1FlexGrid.EditFlags.None
        Me.C1dgPatientDetails.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.C1dgPatientDetails.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.C1dgPatientDetails.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross
        Me.C1dgPatientDetails.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross
        Me.C1dgPatientDetails.Location = New System.Drawing.Point(0, 25)
        Me.C1dgPatientDetails.Name = "C1dgPatientDetails"
        Me.C1dgPatientDetails.Rows.Count = 1
        Me.C1dgPatientDetails.Rows.DefaultSize = 19
        Me.C1dgPatientDetails.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.C1dgPatientDetails.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        Me.C1dgPatientDetails.ShowSortPosition = C1.Win.C1FlexGrid.ShowSortPositionEnum.None
        Me.C1dgPatientDetails.Size = New System.Drawing.Size(955, 53)
        Me.C1dgPatientDetails.StyleInfo = resources.GetString("C1dgPatientDetails.StyleInfo")
        Me.C1dgPatientDetails.TabIndex = 19
        Me.C1dgPatientDetails.Tree.NodeImageCollapsed = CType(resources.GetObject("C1dgPatientDetails.Tree.NodeImageCollapsed"),System.Drawing.Image)
        Me.C1dgPatientDetails.Tree.NodeImageExpanded = CType(resources.GetObject("C1dgPatientDetails.Tree.NodeImageExpanded"),System.Drawing.Image)
        '
        'SplitterOrderComments
        '
        Me.SplitterOrderComments.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.SplitterOrderComments.Location = New System.Drawing.Point(0, 78)
        Me.SplitterOrderComments.Name = "SplitterOrderComments"
        Me.SplitterOrderComments.Size = New System.Drawing.Size(955, 3)
        Me.SplitterOrderComments.TabIndex = 35
        Me.SplitterOrderComments.TabStop = false
        Me.SplitterOrderComments.Visible = false
        '
        'txtOrderComment
        '
        Me.txtOrderComment.BackColor = System.Drawing.Color.White
        Me.txtOrderComment.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.txtOrderComment.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtOrderComment.ForeColor = System.Drawing.Color.Black
        Me.txtOrderComment.Location = New System.Drawing.Point(0, 81)
        Me.txtOrderComment.Name = "txtOrderComment"
        Me.txtOrderComment.ReadOnly = true
        Me.txtOrderComment.Size = New System.Drawing.Size(955, 81)
        Me.txtOrderComment.TabIndex = 36
        Me.txtOrderComment.Text = ""
        Me.txtOrderComment.Visible = false
        '
        'pnlArchiveInfo
        '
        Me.pnlArchiveInfo.Controls.Add(Me.lblInfo)
        Me.pnlArchiveInfo.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlArchiveInfo.Location = New System.Drawing.Point(0, 162)
        Me.pnlArchiveInfo.Name = "pnlArchiveInfo"
        Me.pnlArchiveInfo.Size = New System.Drawing.Size(955, 20)
        Me.pnlArchiveInfo.TabIndex = 23
        Me.pnlArchiveInfo.Visible = false
        '
        'lblInfo
        '
        Me.lblInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblInfo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblInfo.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblInfo.ForeColor = System.Drawing.Color.Green
        Me.lblInfo.Location = New System.Drawing.Point(0, 0)
        Me.lblInfo.Name = "lblInfo"
        Me.lblInfo.Size = New System.Drawing.Size(955, 20)
        Me.lblInfo.TabIndex = 0
        Me.lblInfo.Text = "DMS Archive Information"
        Me.lblInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pnlSearchFilter
        '
        Me.pnlSearchFilter.BackColor = System.Drawing.Color.Transparent
        Me.pnlSearchFilter.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
        Me.pnlSearchFilter.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlSearchFilter.Controls.Add(Me.pnlCancelRx)
        Me.pnlSearchFilter.Controls.Add(Me.pnlCase)
        Me.pnlSearchFilter.Controls.Add(Me.pnlSpeciality)
        Me.pnlSearchFilter.Controls.Add(Me.Panel13)
        Me.pnlSearchFilter.Controls.Add(Me.Panel5)
        Me.pnlSearchFilter.Controls.Add(Me.Panel14)
        Me.pnlSearchFilter.Controls.Add(Me.Panel16)
        Me.pnlSearchFilter.Controls.Add(Me.Label44)
        Me.pnlSearchFilter.Controls.Add(Me.btnReset)
        Me.pnlSearchFilter.Controls.Add(Me.Label54)
        Me.pnlSearchFilter.Controls.Add(Me.Label62)
        Me.pnlSearchFilter.Controls.Add(Me.Label37)
        Me.pnlSearchFilter.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlSearchFilter.Location = New System.Drawing.Point(0, 0)
        Me.pnlSearchFilter.Name = "pnlSearchFilter"
        Me.pnlSearchFilter.Size = New System.Drawing.Size(955, 25)
        Me.pnlSearchFilter.TabIndex = 24
        Me.pnlSearchFilter.Visible = false
        '
        'pnlCancelRx
        '
        Me.pnlCancelRx.BackColor = System.Drawing.Color.Transparent
        Me.pnlCancelRx.Controls.Add(Me.chkCancelRx)
        Me.pnlCancelRx.Dock = System.Windows.Forms.DockStyle.Right
        Me.pnlCancelRx.Location = New System.Drawing.Point(338, 2)
        Me.pnlCancelRx.Name = "pnlCancelRx"
        Me.pnlCancelRx.Padding = New System.Windows.Forms.Padding(0, 0, 6, 0)
        Me.pnlCancelRx.Size = New System.Drawing.Size(97, 22)
        Me.pnlCancelRx.TabIndex = 24
        Me.pnlCancelRx.Visible = false
        '
        'chkCancelRx
        '
        Me.chkCancelRx.AutoSize = true
        Me.chkCancelRx.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkCancelRx.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.chkCancelRx.Location = New System.Drawing.Point(7, 3)
        Me.chkCancelRx.Name = "chkCancelRx"
        Me.chkCancelRx.Size = New System.Drawing.Size(74, 17)
        Me.chkCancelRx.TabIndex = 0
        Me.chkCancelRx.Text = "Cancel Rx"
        Me.chkCancelRx.UseVisualStyleBackColor = true
        '
        'pnlCase
        '
        Me.pnlCase.Controls.Add(Me.Label83)
        Me.pnlCase.Controls.Add(Me.cmbCase)
        Me.pnlCase.Dock = System.Windows.Forms.DockStyle.Right
        Me.pnlCase.Location = New System.Drawing.Point(435, 2)
        Me.pnlCase.Name = "pnlCase"
        Me.pnlCase.Padding = New System.Windows.Forms.Padding(0, 0, 6, 0)
        Me.pnlCase.Size = New System.Drawing.Size(126, 22)
        Me.pnlCase.TabIndex = 23
        '
        'Label83
        '
        Me.Label83.BackColor = System.Drawing.Color.Transparent
        Me.Label83.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label83.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label83.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label83.Location = New System.Drawing.Point(0, 0)
        Me.Label83.Name = "Label83"
        Me.Label83.Size = New System.Drawing.Size(41, 22)
        Me.Label83.TabIndex = 6
        Me.Label83.Text = "Case :"
        Me.Label83.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cmbCase
        '
        Me.cmbCase.Dock = System.Windows.Forms.DockStyle.Right
        Me.cmbCase.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCase.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.cmbCase.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbCase.FormattingEnabled = true
        Me.cmbCase.Location = New System.Drawing.Point(41, 0)
        Me.cmbCase.Name = "cmbCase"
        Me.cmbCase.Size = New System.Drawing.Size(79, 21)
        Me.cmbCase.TabIndex = 1
        '
        'pnlSpeciality
        '
        Me.pnlSpeciality.Controls.Add(Me.Label64)
        Me.pnlSpeciality.Controls.Add(Me.cmbTemplateSpeciality)
        Me.pnlSpeciality.Dock = System.Windows.Forms.DockStyle.Right
        Me.pnlSpeciality.Location = New System.Drawing.Point(561, 2)
        Me.pnlSpeciality.Name = "pnlSpeciality"
        Me.pnlSpeciality.Padding = New System.Windows.Forms.Padding(0, 0, 6, 0)
        Me.pnlSpeciality.Size = New System.Drawing.Size(172, 22)
        Me.pnlSpeciality.TabIndex = 20
        '
        'Label64
        '
        Me.Label64.BackColor = System.Drawing.Color.Transparent
        Me.Label64.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label64.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label64.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label64.Location = New System.Drawing.Point(4, 0)
        Me.Label64.Name = "Label64"
        Me.Label64.Size = New System.Drawing.Size(57, 22)
        Me.Label64.TabIndex = 6
        Me.Label64.Text = "Specialty :"
        Me.Label64.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cmbTemplateSpeciality
        '
        Me.cmbTemplateSpeciality.Dock = System.Windows.Forms.DockStyle.Right
        Me.cmbTemplateSpeciality.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbTemplateSpeciality.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.cmbTemplateSpeciality.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbTemplateSpeciality.FormattingEnabled = true
        Me.cmbTemplateSpeciality.Location = New System.Drawing.Point(61, 0)
        Me.cmbTemplateSpeciality.Name = "cmbTemplateSpeciality"
        Me.cmbTemplateSpeciality.Size = New System.Drawing.Size(105, 21)
        Me.cmbTemplateSpeciality.TabIndex = 1
        '
        'Panel13
        '
        Me.Panel13.Controls.Add(Me.cmbProvider)
        Me.Panel13.Controls.Add(Me.lblProvider)
        Me.Panel13.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel13.Location = New System.Drawing.Point(733, 2)
        Me.Panel13.Name = "Panel13"
        Me.Panel13.Padding = New System.Windows.Forms.Padding(0, 0, 5, 0)
        Me.Panel13.Size = New System.Drawing.Size(196, 22)
        Me.Panel13.TabIndex = 13
        Me.Panel13.Visible = false
        '
        'cmbProvider
        '
        Me.cmbProvider.Dock = System.Windows.Forms.DockStyle.Right
        Me.cmbProvider.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbProvider.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.cmbProvider.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbProvider.FormattingEnabled = true
        Me.cmbProvider.Location = New System.Drawing.Point(54, 0)
        Me.cmbProvider.Name = "cmbProvider"
        Me.cmbProvider.Size = New System.Drawing.Size(137, 21)
        Me.cmbProvider.TabIndex = 8
        '
        'lblProvider
        '
        Me.lblProvider.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.lblProvider.BackColor = System.Drawing.Color.Transparent
        Me.lblProvider.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblProvider.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.lblProvider.Location = New System.Drawing.Point(0, 0)
        Me.lblProvider.Name = "lblProvider"
        Me.lblProvider.Size = New System.Drawing.Size(54, 22)
        Me.lblProvider.TabIndex = 7
        Me.lblProvider.Text = "Provider :"
        Me.lblProvider.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.chkGetLatestActive)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel5.Location = New System.Drawing.Point(632, 2)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.Panel5.Size = New System.Drawing.Size(154, 22)
        Me.Panel5.TabIndex = 17
        Me.Panel5.Visible = false
        '
        'chkGetLatestActive
        '
        Me.chkGetLatestActive.Dock = System.Windows.Forms.DockStyle.Fill
        Me.chkGetLatestActive.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkGetLatestActive.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.chkGetLatestActive.Location = New System.Drawing.Point(5, 0)
        Me.chkGetLatestActive.Name = "chkGetLatestActive"
        Me.chkGetLatestActive.Size = New System.Drawing.Size(149, 22)
        Me.chkGetLatestActive.TabIndex = 16
        Me.chkGetLatestActive.Text = "Latest Active Medication"
        Me.chkGetLatestActive.UseVisualStyleBackColor = true
        '
        'Panel14
        '
        Me.Panel14.Controls.Add(Me.txtSearchIntuit)
        Me.Panel14.Controls.Add(Me.cmbStatus)
        Me.Panel14.Controls.Add(Me.lblStatus)
        Me.Panel14.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel14.Location = New System.Drawing.Point(337, 2)
        Me.Panel14.Name = "Panel14"
        Me.Panel14.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.Panel14.Size = New System.Drawing.Size(295, 22)
        Me.Panel14.TabIndex = 12
        Me.Panel14.Visible = false
        '
        'txtSearchIntuit
        '
        Me.txtSearchIntuit.Dock = System.Windows.Forms.DockStyle.Left
        Me.txtSearchIntuit.Location = New System.Drawing.Point(150, 0)
        Me.txtSearchIntuit.Name = "txtSearchIntuit"
        Me.txtSearchIntuit.Size = New System.Drawing.Size(133, 21)
        Me.txtSearchIntuit.TabIndex = 7
        Me.txtSearchIntuit.Visible = false
        '
        'cmbStatus
        '
        Me.cmbStatus.Dock = System.Windows.Forms.DockStyle.Left
        Me.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbStatus.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.cmbStatus.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbStatus.FormattingEnabled = true
        Me.cmbStatus.Location = New System.Drawing.Point(50, 0)
        Me.cmbStatus.Name = "cmbStatus"
        Me.cmbStatus.Size = New System.Drawing.Size(100, 21)
        Me.cmbStatus.TabIndex = 1
        '
        'lblStatus
        '
        Me.lblStatus.AutoSize = true
        Me.lblStatus.BackColor = System.Drawing.Color.Transparent
        Me.lblStatus.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblStatus.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblStatus.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.lblStatus.Location = New System.Drawing.Point(5, 0)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Padding = New System.Windows.Forms.Padding(0, 4, 0, 0)
        Me.lblStatus.Size = New System.Drawing.Size(45, 17)
        Me.lblStatus.TabIndex = 6
        Me.lblStatus.Text = "Status :"
        Me.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Panel16
        '
        Me.Panel16.Controls.Add(Me.dtpToDate)
        Me.Panel16.Controls.Add(Me.lblto)
        Me.Panel16.Controls.Add(Me.dtpFromDate)
        Me.Panel16.Controls.Add(Me.chkenbdate)
        Me.Panel16.Controls.Add(Me.lblFrom)
        Me.Panel16.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel16.Location = New System.Drawing.Point(57, 2)
        Me.Panel16.Name = "Panel16"
        Me.Panel16.Size = New System.Drawing.Size(280, 22)
        Me.Panel16.TabIndex = 11
        Me.Panel16.Visible = false
        '
        'dtpToDate
        '
        Me.dtpToDate.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtpToDate.CalendarMonthBackground = System.Drawing.Color.White
        Me.dtpToDate.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtpToDate.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtpToDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtpToDate.Dock = System.Windows.Forms.DockStyle.Left
        Me.dtpToDate.Enabled = false
        Me.dtpToDate.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpToDate.Location = New System.Drawing.Point(175, 0)
        Me.dtpToDate.Name = "dtpToDate"
        Me.dtpToDate.Size = New System.Drawing.Size(99, 21)
        Me.dtpToDate.TabIndex = 5
        '
        'lblto
        '
        Me.lblto.BackColor = System.Drawing.Color.Transparent
        Me.lblto.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblto.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblto.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblto.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.lblto.Location = New System.Drawing.Point(148, 0)
        Me.lblto.Name = "lblto"
        Me.lblto.Size = New System.Drawing.Size(27, 22)
        Me.lblto.TabIndex = 2
        Me.lblto.Text = "To"
        Me.lblto.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dtpFromDate
        '
        Me.dtpFromDate.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtpFromDate.CalendarMonthBackground = System.Drawing.Color.White
        Me.dtpFromDate.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtpFromDate.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtpFromDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtpFromDate.Dock = System.Windows.Forms.DockStyle.Left
        Me.dtpFromDate.Enabled = false
        Me.dtpFromDate.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFromDate.Location = New System.Drawing.Point(49, 0)
        Me.dtpFromDate.Name = "dtpFromDate"
        Me.dtpFromDate.Size = New System.Drawing.Size(99, 21)
        Me.dtpFromDate.TabIndex = 3
        '
        'chkenbdate
        '
        Me.chkenbdate.AutoSize = true
        Me.chkenbdate.Dock = System.Windows.Forms.DockStyle.Left
        Me.chkenbdate.Location = New System.Drawing.Point(34, 0)
        Me.chkenbdate.Name = "chkenbdate"
        Me.chkenbdate.Size = New System.Drawing.Size(15, 22)
        Me.chkenbdate.TabIndex = 20
        Me.chkenbdate.UseVisualStyleBackColor = true
        '
        'lblFrom
        '
        Me.lblFrom.BackColor = System.Drawing.Color.Transparent
        Me.lblFrom.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblFrom.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblFrom.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.lblFrom.Location = New System.Drawing.Point(0, 0)
        Me.lblFrom.Name = "lblFrom"
        Me.lblFrom.Size = New System.Drawing.Size(34, 22)
        Me.lblFrom.TabIndex = 4
        Me.lblFrom.Text = "From"
        Me.lblFrom.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label44
        '
        Me.Label44.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label44.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label44.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label44.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label44.Location = New System.Drawing.Point(0, 2)
        Me.Label44.Name = "Label44"
        Me.Label44.Size = New System.Drawing.Size(57, 22)
        Me.Label44.TabIndex = 22
        Me.Label44.Text = "Search :"
        Me.Label44.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label54
        '
        Me.Label54.BackColor = System.Drawing.Color.FromArgb(CType(CType(101,Byte),Integer), CType(CType(145,Byte),Integer), CType(CType(205,Byte),Integer))
        Me.Label54.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label54.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label54.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label54.Location = New System.Drawing.Point(0, 24)
        Me.Label54.Name = "Label54"
        Me.Label54.Size = New System.Drawing.Size(951, 1)
        Me.Label54.TabIndex = 14
        Me.Label54.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label62
        '
        Me.Label62.BackColor = System.Drawing.Color.Transparent
        Me.Label62.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label62.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label62.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label62.Location = New System.Drawing.Point(951, 2)
        Me.Label62.Name = "Label62"
        Me.Label62.Size = New System.Drawing.Size(4, 23)
        Me.Label62.TabIndex = 19
        Me.Label62.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label37
        '
        Me.Label37.BackColor = System.Drawing.Color.FromArgb(CType(CType(101,Byte),Integer), CType(CType(145,Byte),Integer), CType(CType(205,Byte),Integer))
        Me.Label37.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label37.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label37.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label37.Location = New System.Drawing.Point(0, 0)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(955, 2)
        Me.Label37.TabIndex = 18
        Me.Label37.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnlPatientDetailsHeaders
        '
        Me.pnlPatientDetailsHeaders.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.pnlPatientDetailsHeaders.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlPatientDetailsHeaders.Controls.Add(Me.Label38)
        Me.pnlPatientDetailsHeaders.Controls.Add(Me.ts_PatientDetails)
        Me.pnlPatientDetailsHeaders.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlPatientDetailsHeaders.Location = New System.Drawing.Point(0, 0)
        Me.pnlPatientDetailsHeaders.Name = "pnlPatientDetailsHeaders"
        Me.pnlPatientDetailsHeaders.Size = New System.Drawing.Size(955, 25)
        Me.pnlPatientDetailsHeaders.TabIndex = 7
        '
        'Label38
        '
        Me.Label38.BackColor = System.Drawing.Color.FromArgb(CType(CType(101,Byte),Integer), CType(CType(147,Byte),Integer), CType(CType(207,Byte),Integer))
        Me.Label38.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label38.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label38.ForeColor = System.Drawing.Color.Black
        Me.Label38.Location = New System.Drawing.Point(0, 24)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(955, 1)
        Me.Label38.TabIndex = 69
        Me.Label38.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ts_PatientDetails
        '
        Me.ts_PatientDetails.BackColor = System.Drawing.Color.Transparent
        Me.ts_PatientDetails.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.ts_PatientDetails.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ts_PatientDetails.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.ts_PatientDetails.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ts_PatientDetails.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsbtn_History, Me.tlsHistorySep, Me.tsbtn_Insurance, Me.ToolStripSeparator3, Me.tsbtn_Billing, Me.ToolStripSeparator2, Me.tsbtn_Medication, Me.tlsMedSep, Me.tsbtn_Prescription, Me.tlsRxSep, Me.tsbtn_ProblemList, Me.tlsProblemSep, Me.tsbtn_ViewDocs, Me.tlsViewDocsSep, Me.tsbtn_Orders, Me.tlsOrdersSep, Me.tsbtn_Messages, Me.tlsMessageSep, Me.tsbtn_PastExam, Me.tlsPastExamSep, Me.tsbtn_Vital, Me.ToolStripSeparator13, Me.tsbtn_Referrals, Me.tlsNewExamSep, Me.tsbtn_PendingFax, Me.tlsPendingFaxSep, Me.tsbtn_Sentfax, Me.tlsSentFaxSep, Me.tsbtn_Labs, Me.ToolStripSeparator4, Me.tsbtn_Balance, Me.ToolStripSeparator5, Me.tsbtn_NurseNotes, Me.ToolStripSeparator7, Me.tsbtn_PatientConsent, Me.ToolStripSeparator8, Me.tsbtn_PatientLetters, Me.ToolStripSeparator19, Me.tsbtn_DisclosureMgt, Me.ToolStripSeparator6, Me.tsbtn_AuditTrail, Me.ToolStripSeparator15, Me.tsbtn_Appointments, Me.ToolStripSeparator11, Me.tsbtn_Selected, Me.tsbtn_Hover, Me.tsbtn_Normal, Me.tsbtn_EligbilityInfo, Me.tsbtn_Triage, Me.tsbtn_PatientEducation, Me.tsbtn_NewExam, Me.tsbtn_PriorAuthorization, Me.tsbtn_PatientTasks, Me.tsbtn_PatientCases, Me.tsbtn_IntuitCommunication, Me.tsbtn_PatientCommunication, Me.tsbtn_Order, Me.tsbtn_NYWCForms, Me.tsbtn_SPB})
        Me.ts_PatientDetails.Location = New System.Drawing.Point(0, 0)
        Me.ts_PatientDetails.Name = "ts_PatientDetails"
        Me.ts_PatientDetails.Size = New System.Drawing.Size(955, 25)
        Me.ts_PatientDetails.TabIndex = 19
        Me.ts_PatientDetails.Text = "ToolStrip1"
        '
        'tsbtn_History
        '
        Me.tsbtn_History.BackColor = System.Drawing.Color.Transparent
        Me.tsbtn_History.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tsbtn_History.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.tsbtn_History.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.tsbtn_History.Image = CType(resources.GetObject("tsbtn_History.Image"),System.Drawing.Image)
        Me.tsbtn_History.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.tsbtn_History.Name = "tsbtn_History"
        Me.tsbtn_History.Size = New System.Drawing.Size(68, 22)
        Me.tsbtn_History.Tag = "History"
        Me.tsbtn_History.Text = "History"
        '
        'tlsHistorySep
        '
        Me.tlsHistorySep.AutoSize = false
        Me.tlsHistorySep.Name = "tlsHistorySep"
        Me.tlsHistorySep.Size = New System.Drawing.Size(6, 25)
        '
        'tsbtn_Insurance
        '
        Me.tsbtn_Insurance.BackColor = System.Drawing.Color.Transparent
        Me.tsbtn_Insurance.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tsbtn_Insurance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.tsbtn_Insurance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.tsbtn_Insurance.Image = CType(resources.GetObject("tsbtn_Insurance.Image"),System.Drawing.Image)
        Me.tsbtn_Insurance.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.tsbtn_Insurance.Name = "tsbtn_Insurance"
        Me.tsbtn_Insurance.Size = New System.Drawing.Size(84, 22)
        Me.tsbtn_Insurance.Tag = "Insurance"
        Me.tsbtn_Insurance.Text = "Insurance"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(6, 25)
        '
        'tsbtn_Billing
        '
        Me.tsbtn_Billing.BackColor = System.Drawing.Color.Transparent
        Me.tsbtn_Billing.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tsbtn_Billing.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.tsbtn_Billing.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.tsbtn_Billing.Image = CType(resources.GetObject("tsbtn_Billing.Image"),System.Drawing.Image)
        Me.tsbtn_Billing.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.tsbtn_Billing.Name = "tsbtn_Billing"
        Me.tsbtn_Billing.Size = New System.Drawing.Size(60, 22)
        Me.tsbtn_Billing.Tag = "Billing"
        Me.tsbtn_Billing.Text = "Billing"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 25)
        '
        'tsbtn_Medication
        '
        Me.tsbtn_Medication.BackColor = System.Drawing.Color.Transparent
        Me.tsbtn_Medication.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tsbtn_Medication.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.tsbtn_Medication.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.tsbtn_Medication.Image = CType(resources.GetObject("tsbtn_Medication.Image"),System.Drawing.Image)
        Me.tsbtn_Medication.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.tsbtn_Medication.Name = "tsbtn_Medication"
        Me.tsbtn_Medication.Size = New System.Drawing.Size(89, 22)
        Me.tsbtn_Medication.Tag = "Medication"
        Me.tsbtn_Medication.Text = "Medication"
        '
        'tlsMedSep
        '
        Me.tlsMedSep.AutoSize = false
        Me.tlsMedSep.Name = "tlsMedSep"
        Me.tlsMedSep.Size = New System.Drawing.Size(6, 25)
        '
        'tsbtn_Prescription
        '
        Me.tsbtn_Prescription.BackColor = System.Drawing.Color.Transparent
        Me.tsbtn_Prescription.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tsbtn_Prescription.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.tsbtn_Prescription.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.tsbtn_Prescription.Image = CType(resources.GetObject("tsbtn_Prescription.Image"),System.Drawing.Image)
        Me.tsbtn_Prescription.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.tsbtn_Prescription.Name = "tsbtn_Prescription"
        Me.tsbtn_Prescription.Size = New System.Drawing.Size(95, 22)
        Me.tsbtn_Prescription.Tag = "Prescription"
        Me.tsbtn_Prescription.Text = "Prescription"
        '
        'tlsRxSep
        '
        Me.tlsRxSep.AutoSize = false
        Me.tlsRxSep.Name = "tlsRxSep"
        Me.tlsRxSep.Size = New System.Drawing.Size(6, 25)
        '
        'tsbtn_ProblemList
        '
        Me.tsbtn_ProblemList.BackColor = System.Drawing.Color.Transparent
        Me.tsbtn_ProblemList.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tsbtn_ProblemList.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.tsbtn_ProblemList.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.tsbtn_ProblemList.Image = CType(resources.GetObject("tsbtn_ProblemList.Image"),System.Drawing.Image)
        Me.tsbtn_ProblemList.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.tsbtn_ProblemList.Name = "tsbtn_ProblemList"
        Me.tsbtn_ProblemList.Size = New System.Drawing.Size(97, 22)
        Me.tsbtn_ProblemList.Tag = "Problem List"
        Me.tsbtn_ProblemList.Text = "Problem List"
        '
        'tlsProblemSep
        '
        Me.tlsProblemSep.AutoSize = false
        Me.tlsProblemSep.Name = "tlsProblemSep"
        Me.tlsProblemSep.Size = New System.Drawing.Size(6, 25)
        '
        'tsbtn_ViewDocs
        '
        Me.tsbtn_ViewDocs.BackColor = System.Drawing.Color.Transparent
        Me.tsbtn_ViewDocs.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tsbtn_ViewDocs.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.tsbtn_ViewDocs.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.tsbtn_ViewDocs.Image = CType(resources.GetObject("tsbtn_ViewDocs.Image"),System.Drawing.Image)
        Me.tsbtn_ViewDocs.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.tsbtn_ViewDocs.Name = "tsbtn_ViewDocs"
        Me.tsbtn_ViewDocs.Size = New System.Drawing.Size(120, 22)
        Me.tsbtn_ViewDocs.Tag = "View Documents"
        Me.tsbtn_ViewDocs.Text = "View Documents"
        Me.tsbtn_ViewDocs.Visible = false
        '
        'tlsViewDocsSep
        '
        Me.tlsViewDocsSep.AutoSize = false
        Me.tlsViewDocsSep.Name = "tlsViewDocsSep"
        Me.tlsViewDocsSep.Size = New System.Drawing.Size(6, 25)
        Me.tlsViewDocsSep.Visible = false
        '
        'tsbtn_Orders
        '
        Me.tsbtn_Orders.BackColor = System.Drawing.Color.Transparent
        Me.tsbtn_Orders.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tsbtn_Orders.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.tsbtn_Orders.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.tsbtn_Orders.Image = CType(resources.GetObject("tsbtn_Orders.Image"),System.Drawing.Image)
        Me.tsbtn_Orders.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.tsbtn_Orders.Name = "tsbtn_Orders"
        Me.tsbtn_Orders.Size = New System.Drawing.Size(122, 22)
        Me.tsbtn_Orders.Tag = "Order Templates"
        Me.tsbtn_Orders.Text = "Order Templates"
        '
        'tlsOrdersSep
        '
        Me.tlsOrdersSep.AutoSize = false
        Me.tlsOrdersSep.Name = "tlsOrdersSep"
        Me.tlsOrdersSep.Size = New System.Drawing.Size(6, 25)
        '
        'tsbtn_Messages
        '
        Me.tsbtn_Messages.BackColor = System.Drawing.Color.Transparent
        Me.tsbtn_Messages.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tsbtn_Messages.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.tsbtn_Messages.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.tsbtn_Messages.Image = CType(resources.GetObject("tsbtn_Messages.Image"),System.Drawing.Image)
        Me.tsbtn_Messages.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.tsbtn_Messages.Name = "tsbtn_Messages"
        Me.tsbtn_Messages.Size = New System.Drawing.Size(83, 22)
        Me.tsbtn_Messages.Tag = "Messages"
        Me.tsbtn_Messages.Text = "Messages"
        '
        'tlsMessageSep
        '
        Me.tlsMessageSep.AutoSize = false
        Me.tlsMessageSep.Name = "tlsMessageSep"
        Me.tlsMessageSep.Size = New System.Drawing.Size(6, 25)
        '
        'tsbtn_PastExam
        '
        Me.tsbtn_PastExam.BackColor = System.Drawing.Color.Transparent
        Me.tsbtn_PastExam.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tsbtn_PastExam.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.tsbtn_PastExam.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.tsbtn_PastExam.Image = CType(resources.GetObject("tsbtn_PastExam.Image"),System.Drawing.Image)
        Me.tsbtn_PastExam.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.tsbtn_PastExam.Name = "tsbtn_PastExam"
        Me.tsbtn_PastExam.Size = New System.Drawing.Size(92, 22)
        Me.tsbtn_PastExam.Tag = "Past Exams"
        Me.tsbtn_PastExam.Text = "Past Exams"
        '
        'tlsPastExamSep
        '
        Me.tlsPastExamSep.AutoSize = false
        Me.tlsPastExamSep.Name = "tlsPastExamSep"
        Me.tlsPastExamSep.Size = New System.Drawing.Size(6, 25)
        '
        'tsbtn_Vital
        '
        Me.tsbtn_Vital.BackColor = System.Drawing.Color.Transparent
        Me.tsbtn_Vital.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tsbtn_Vital.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.tsbtn_Vital.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.tsbtn_Vital.Image = CType(resources.GetObject("tsbtn_Vital.Image"),System.Drawing.Image)
        Me.tsbtn_Vital.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.tsbtn_Vital.Name = "tsbtn_Vital"
        Me.tsbtn_Vital.Size = New System.Drawing.Size(58, 22)
        Me.tsbtn_Vital.Tag = "Vitals"
        Me.tsbtn_Vital.Text = "Vitals"
        '
        'ToolStripSeparator13
        '
        Me.ToolStripSeparator13.Name = "ToolStripSeparator13"
        Me.ToolStripSeparator13.Size = New System.Drawing.Size(6, 25)
        '
        'tsbtn_Referrals
        '
        Me.tsbtn_Referrals.BackColor = System.Drawing.Color.Transparent
        Me.tsbtn_Referrals.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tsbtn_Referrals.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.tsbtn_Referrals.Image = CType(resources.GetObject("tsbtn_Referrals.Image"),System.Drawing.Image)
        Me.tsbtn_Referrals.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.tsbtn_Referrals.Name = "tsbtn_Referrals"
        Me.tsbtn_Referrals.Size = New System.Drawing.Size(79, 20)
        Me.tsbtn_Referrals.Tag = "Referrals"
        Me.tsbtn_Referrals.Text = "Referrals"
        '
        'tlsNewExamSep
        '
        Me.tlsNewExamSep.AutoSize = false
        Me.tlsNewExamSep.Name = "tlsNewExamSep"
        Me.tlsNewExamSep.Size = New System.Drawing.Size(6, 25)
        '
        'tsbtn_PendingFax
        '
        Me.tsbtn_PendingFax.BackColor = System.Drawing.Color.Transparent
        Me.tsbtn_PendingFax.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tsbtn_PendingFax.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.tsbtn_PendingFax.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.tsbtn_PendingFax.Image = CType(resources.GetObject("tsbtn_PendingFax.Image"),System.Drawing.Image)
        Me.tsbtn_PendingFax.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.tsbtn_PendingFax.Name = "tsbtn_PendingFax"
        Me.tsbtn_PendingFax.Size = New System.Drawing.Size(95, 20)
        Me.tsbtn_PendingFax.Tag = "Pending Fax"
        Me.tsbtn_PendingFax.Text = "Pending Fax"
        '
        'tlsPendingFaxSep
        '
        Me.tlsPendingFaxSep.AutoSize = false
        Me.tlsPendingFaxSep.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.tlsPendingFaxSep.Name = "tlsPendingFaxSep"
        Me.tlsPendingFaxSep.Size = New System.Drawing.Size(6, 25)
        '
        'tsbtn_Sentfax
        '
        Me.tsbtn_Sentfax.BackColor = System.Drawing.Color.Transparent
        Me.tsbtn_Sentfax.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tsbtn_Sentfax.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.tsbtn_Sentfax.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.tsbtn_Sentfax.Image = CType(resources.GetObject("tsbtn_Sentfax.Image"),System.Drawing.Image)
        Me.tsbtn_Sentfax.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.tsbtn_Sentfax.Name = "tsbtn_Sentfax"
        Me.tsbtn_Sentfax.Size = New System.Drawing.Size(76, 20)
        Me.tsbtn_Sentfax.Tag = "Sent Fax"
        Me.tsbtn_Sentfax.Text = "Sent Fax"
        '
        'tlsSentFaxSep
        '
        Me.tlsSentFaxSep.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.tlsSentFaxSep.Name = "tlsSentFaxSep"
        Me.tlsSentFaxSep.Size = New System.Drawing.Size(6, 25)
        '
        'tsbtn_Labs
        '
        Me.tsbtn_Labs.BackColor = System.Drawing.Color.Transparent
        Me.tsbtn_Labs.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tsbtn_Labs.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.tsbtn_Labs.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.tsbtn_Labs.Image = CType(resources.GetObject("tsbtn_Labs.Image"),System.Drawing.Image)
        Me.tsbtn_Labs.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.tsbtn_Labs.Name = "tsbtn_Labs"
        Me.tsbtn_Labs.Size = New System.Drawing.Size(107, 20)
        Me.tsbtn_Labs.Tag = "Orders & Results"
        Me.tsbtn_Labs.Text = "Order  Results"
        Me.tsbtn_Labs.ToolTipText = "Order  Results"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(6, 25)
        '
        'tsbtn_Balance
        '
        Me.tsbtn_Balance.BackColor = System.Drawing.Color.Transparent
        Me.tsbtn_Balance.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tsbtn_Balance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.tsbtn_Balance.Image = CType(resources.GetObject("tsbtn_Balance.Image"),System.Drawing.Image)
        Me.tsbtn_Balance.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbtn_Balance.Name = "tsbtn_Balance"
        Me.tsbtn_Balance.Size = New System.Drawing.Size(71, 20)
        Me.tsbtn_Balance.Tag = "Balance"
        Me.tsbtn_Balance.Text = "Balance"
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(6, 6)
        '
        'tsbtn_NurseNotes
        '
        Me.tsbtn_NurseNotes.BackColor = System.Drawing.Color.Transparent
        Me.tsbtn_NurseNotes.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tsbtn_NurseNotes.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.tsbtn_NurseNotes.Image = CType(resources.GetObject("tsbtn_NurseNotes.Image"),System.Drawing.Image)
        Me.tsbtn_NurseNotes.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbtn_NurseNotes.Name = "tsbtn_NurseNotes"
        Me.tsbtn_NurseNotes.Size = New System.Drawing.Size(94, 20)
        Me.tsbtn_NurseNotes.Tag = "Nurse Notes"
        Me.tsbtn_NurseNotes.Text = "Nurse Notes"
        '
        'ToolStripSeparator7
        '
        Me.ToolStripSeparator7.Name = "ToolStripSeparator7"
        Me.ToolStripSeparator7.Size = New System.Drawing.Size(6, 6)
        '
        'tsbtn_PatientConsent
        '
        Me.tsbtn_PatientConsent.BackColor = System.Drawing.Color.Transparent
        Me.tsbtn_PatientConsent.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tsbtn_PatientConsent.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.tsbtn_PatientConsent.Image = CType(resources.GetObject("tsbtn_PatientConsent.Image"),System.Drawing.Image)
        Me.tsbtn_PatientConsent.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbtn_PatientConsent.Name = "tsbtn_PatientConsent"
        Me.tsbtn_PatientConsent.Size = New System.Drawing.Size(117, 20)
        Me.tsbtn_PatientConsent.Tag = "Patient Consent"
        Me.tsbtn_PatientConsent.Text = "Patient Consent"
        '
        'ToolStripSeparator8
        '
        Me.ToolStripSeparator8.Name = "ToolStripSeparator8"
        Me.ToolStripSeparator8.Size = New System.Drawing.Size(6, 6)
        '
        'tsbtn_PatientLetters
        '
        Me.tsbtn_PatientLetters.BackColor = System.Drawing.Color.Transparent
        Me.tsbtn_PatientLetters.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tsbtn_PatientLetters.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.tsbtn_PatientLetters.Image = CType(resources.GetObject("tsbtn_PatientLetters.Image"),System.Drawing.Image)
        Me.tsbtn_PatientLetters.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbtn_PatientLetters.Name = "tsbtn_PatientLetters"
        Me.tsbtn_PatientLetters.Size = New System.Drawing.Size(112, 20)
        Me.tsbtn_PatientLetters.Tag = "Patient Letters"
        Me.tsbtn_PatientLetters.Text = "Patient Letters"
        '
        'ToolStripSeparator19
        '
        Me.ToolStripSeparator19.Name = "ToolStripSeparator19"
        Me.ToolStripSeparator19.Size = New System.Drawing.Size(6, 6)
        '
        'tsbtn_DisclosureMgt
        '
        Me.tsbtn_DisclosureMgt.BackColor = System.Drawing.Color.Transparent
        Me.tsbtn_DisclosureMgt.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tsbtn_DisclosureMgt.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.tsbtn_DisclosureMgt.Image = CType(resources.GetObject("tsbtn_DisclosureMgt.Image"),System.Drawing.Image)
        Me.tsbtn_DisclosureMgt.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbtn_DisclosureMgt.Name = "tsbtn_DisclosureMgt"
        Me.tsbtn_DisclosureMgt.Size = New System.Drawing.Size(163, 20)
        Me.tsbtn_DisclosureMgt.Tag = "Disclosure Management"
        Me.tsbtn_DisclosureMgt.Text = "Disclosure Management"
        '
        'ToolStripSeparator6
        '
        Me.ToolStripSeparator6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.ToolStripSeparator6.Name = "ToolStripSeparator6"
        Me.ToolStripSeparator6.Size = New System.Drawing.Size(6, 25)
        '
        'tsbtn_AuditTrail
        '
        Me.tsbtn_AuditTrail.BackColor = System.Drawing.Color.Transparent
        Me.tsbtn_AuditTrail.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tsbtn_AuditTrail.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.tsbtn_AuditTrail.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.tsbtn_AuditTrail.Image = CType(resources.GetObject("tsbtn_AuditTrail.Image"),System.Drawing.Image)
        Me.tsbtn_AuditTrail.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.tsbtn_AuditTrail.Name = "tsbtn_AuditTrail"
        Me.tsbtn_AuditTrail.Size = New System.Drawing.Size(80, 20)
        Me.tsbtn_AuditTrail.Tag = "Audit Trail"
        Me.tsbtn_AuditTrail.Text = "Audit Log"
        Me.tsbtn_AuditTrail.ToolTipText = "Audit Log"
        '
        'ToolStripSeparator15
        '
        Me.ToolStripSeparator15.Name = "ToolStripSeparator15"
        Me.ToolStripSeparator15.Size = New System.Drawing.Size(6, 6)
        '
        'tsbtn_Appointments
        '
        Me.tsbtn_Appointments.BackColor = System.Drawing.Color.Transparent
        Me.tsbtn_Appointments.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tsbtn_Appointments.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.tsbtn_Appointments.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.tsbtn_Appointments.Image = CType(resources.GetObject("tsbtn_Appointments.Image"),System.Drawing.Image)
        Me.tsbtn_Appointments.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbtn_Appointments.Name = "tsbtn_Appointments"
        Me.tsbtn_Appointments.Size = New System.Drawing.Size(107, 20)
        Me.tsbtn_Appointments.Tag = "Appointments"
        Me.tsbtn_Appointments.Text = "Appointments"
        '
        'ToolStripSeparator11
        '
        Me.ToolStripSeparator11.Name = "ToolStripSeparator11"
        Me.ToolStripSeparator11.Size = New System.Drawing.Size(6, 6)
        Me.ToolStripSeparator11.Visible = false
        '
        'tsbtn_Selected
        '
        Me.tsbtn_Selected.BackColor = System.Drawing.Color.Transparent
        Me.tsbtn_Selected.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Orange
        Me.tsbtn_Selected.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tsbtn_Selected.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbtn_Selected.Image = Global.gloEMR.My.Resources.Resources.Img_Orange
        Me.tsbtn_Selected.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.tsbtn_Selected.Name = "tsbtn_Selected"
        Me.tsbtn_Selected.Size = New System.Drawing.Size(23, 20)
        Me.tsbtn_Selected.Text = "ToolStripButton1"
        Me.tsbtn_Selected.Visible = false
        '
        'tsbtn_Hover
        '
        Me.tsbtn_Hover.BackColor = System.Drawing.Color.Transparent
        Me.tsbtn_Hover.BackgroundImage = CType(resources.GetObject("tsbtn_Hover.BackgroundImage"),System.Drawing.Image)
        Me.tsbtn_Hover.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tsbtn_Hover.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbtn_Hover.Image = CType(resources.GetObject("tsbtn_Hover.Image"),System.Drawing.Image)
        Me.tsbtn_Hover.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.tsbtn_Hover.Name = "tsbtn_Hover"
        Me.tsbtn_Hover.Size = New System.Drawing.Size(23, 20)
        Me.tsbtn_Hover.Text = "ToolStripButton1"
        Me.tsbtn_Hover.Visible = false
        '
        'tsbtn_Normal
        '
        Me.tsbtn_Normal.BackColor = System.Drawing.Color.Transparent
        Me.tsbtn_Normal.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tsbtn_Normal.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbtn_Normal.Image = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tsbtn_Normal.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.tsbtn_Normal.Name = "tsbtn_Normal"
        Me.tsbtn_Normal.Size = New System.Drawing.Size(23, 20)
        Me.tsbtn_Normal.Text = "ToolStripButton1"
        Me.tsbtn_Normal.Visible = false
        '
        'tsbtn_EligbilityInfo
        '
        Me.tsbtn_EligbilityInfo.BackColor = System.Drawing.Color.Transparent
        Me.tsbtn_EligbilityInfo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tsbtn_EligbilityInfo.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.tsbtn_EligbilityInfo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.tsbtn_EligbilityInfo.Image = CType(resources.GetObject("tsbtn_EligbilityInfo.Image"),System.Drawing.Image)
        Me.tsbtn_EligbilityInfo.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.tsbtn_EligbilityInfo.Name = "tsbtn_EligbilityInfo"
        Me.tsbtn_EligbilityInfo.Size = New System.Drawing.Size(148, 20)
        Me.tsbtn_EligbilityInfo.Tag = "Eligibility Information"
        Me.tsbtn_EligbilityInfo.Text = "Eligibility Information"
        '
        'tsbtn_Triage
        '
        Me.tsbtn_Triage.BackColor = System.Drawing.Color.Transparent
        Me.tsbtn_Triage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tsbtn_Triage.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.tsbtn_Triage.Image = CType(resources.GetObject("tsbtn_Triage.Image"),System.Drawing.Image)
        Me.tsbtn_Triage.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.tsbtn_Triage.Name = "tsbtn_Triage"
        Me.tsbtn_Triage.Size = New System.Drawing.Size(63, 20)
        Me.tsbtn_Triage.Tag = "Triage"
        Me.tsbtn_Triage.Text = "Triage"
        '
        'tsbtn_PatientEducation
        '
        Me.tsbtn_PatientEducation.BackColor = System.Drawing.Color.Transparent
        Me.tsbtn_PatientEducation.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tsbtn_PatientEducation.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.tsbtn_PatientEducation.Image = CType(resources.GetObject("tsbtn_PatientEducation.Image"),System.Drawing.Image)
        Me.tsbtn_PatientEducation.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbtn_PatientEducation.Name = "tsbtn_PatientEducation"
        Me.tsbtn_PatientEducation.Size = New System.Drawing.Size(126, 20)
        Me.tsbtn_PatientEducation.Tag = "Patient Education"
        Me.tsbtn_PatientEducation.Text = "Patient Education"
        '
        'tsbtn_NewExam
        '
        Me.tsbtn_NewExam.BackColor = System.Drawing.Color.Transparent
        Me.tsbtn_NewExam.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tsbtn_NewExam.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.tsbtn_NewExam.Image = CType(resources.GetObject("tsbtn_NewExam.Image"),System.Drawing.Image)
        Me.tsbtn_NewExam.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbtn_NewExam.Name = "tsbtn_NewExam"
        Me.tsbtn_NewExam.Size = New System.Drawing.Size(84, 20)
        Me.tsbtn_NewExam.Tag = "New Exam"
        Me.tsbtn_NewExam.Text = "New Exam"
        '
        'tsbtn_PriorAuthorization
        '
        Me.tsbtn_PriorAuthorization.BackColor = System.Drawing.Color.Transparent
        Me.tsbtn_PriorAuthorization.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tsbtn_PriorAuthorization.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.tsbtn_PriorAuthorization.Image = CType(resources.GetObject("tsbtn_PriorAuthorization.Image"),System.Drawing.Image)
        Me.tsbtn_PriorAuthorization.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbtn_PriorAuthorization.Name = "tsbtn_PriorAuthorization"
        Me.tsbtn_PriorAuthorization.Size = New System.Drawing.Size(134, 20)
        Me.tsbtn_PriorAuthorization.Tag = "PriorAuthorization"
        Me.tsbtn_PriorAuthorization.Text = "Prior Authorization"
        '
        'tsbtn_PatientTasks
        '
        Me.tsbtn_PatientTasks.BackColor = System.Drawing.Color.Transparent
        Me.tsbtn_PatientTasks.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tsbtn_PatientTasks.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.tsbtn_PatientTasks.Image = CType(resources.GetObject("tsbtn_PatientTasks.Image"),System.Drawing.Image)
        Me.tsbtn_PatientTasks.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbtn_PatientTasks.Name = "tsbtn_PatientTasks"
        Me.tsbtn_PatientTasks.Size = New System.Drawing.Size(60, 20)
        Me.tsbtn_PatientTasks.Tag = "Tasks"
        Me.tsbtn_PatientTasks.Text = "Tasks"
        '
        'tsbtn_PatientCases
        '
        Me.tsbtn_PatientCases.BackColor = System.Drawing.Color.Transparent
        Me.tsbtn_PatientCases.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tsbtn_PatientCases.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.tsbtn_PatientCases.Image = CType(resources.GetObject("tsbtn_PatientCases.Image"),System.Drawing.Image)
        Me.tsbtn_PatientCases.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbtn_PatientCases.Name = "tsbtn_PatientCases"
        Me.tsbtn_PatientCases.Size = New System.Drawing.Size(60, 20)
        Me.tsbtn_PatientCases.Tag = "Cases"
        Me.tsbtn_PatientCases.Text = "Cases"
        Me.tsbtn_PatientCases.ToolTipText = "Cases"
        '
        'tsbtn_IntuitCommunication
        '
        Me.tsbtn_IntuitCommunication.BackColor = System.Drawing.Color.Transparent
        Me.tsbtn_IntuitCommunication.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tsbtn_IntuitCommunication.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.tsbtn_IntuitCommunication.Image = CType(resources.GetObject("tsbtn_IntuitCommunication.Image"),System.Drawing.Image)
        Me.tsbtn_IntuitCommunication.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbtn_IntuitCommunication.Name = "tsbtn_IntuitCommunication"
        Me.tsbtn_IntuitCommunication.Size = New System.Drawing.Size(202, 20)
        Me.tsbtn_IntuitCommunication.Tag = "Patient Portal Communications"
        Me.tsbtn_IntuitCommunication.Text = "Patient Portal Communications"
        '
        'tsbtn_PatientCommunication
        '
        Me.tsbtn_PatientCommunication.BackColor = System.Drawing.Color.Transparent
        Me.tsbtn_PatientCommunication.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tsbtn_PatientCommunication.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.tsbtn_PatientCommunication.Image = CType(resources.GetObject("tsbtn_PatientCommunication.Image"),System.Drawing.Image)
        Me.tsbtn_PatientCommunication.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbtn_PatientCommunication.Name = "tsbtn_PatientCommunication"
        Me.tsbtn_PatientCommunication.Size = New System.Drawing.Size(159, 20)
        Me.tsbtn_PatientCommunication.Tag = "Patient Communication"
        Me.tsbtn_PatientCommunication.Text = "Patient Communication"
        Me.tsbtn_PatientCommunication.ToolTipText = "Patient Communication"
        '
        'tsbtn_Order
        '
        Me.tsbtn_Order.BackColor = System.Drawing.Color.Transparent
        Me.tsbtn_Order.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.tsbtn_Order.Image = CType(resources.GetObject("tsbtn_Order.Image"),System.Drawing.Image)
        Me.tsbtn_Order.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbtn_Order.Name = "tsbtn_Order"
        Me.tsbtn_Order.Size = New System.Drawing.Size(65, 20)
        Me.tsbtn_Order.Tag = "Order"
        Me.tsbtn_Order.Text = "Orders"
        Me.tsbtn_Order.ToolTipText = "Orders"
        '
        'tsbtn_NYWCForms
        '
        Me.tsbtn_NYWCForms.BackColor = System.Drawing.Color.Transparent
        Me.tsbtn_NYWCForms.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.tsbtn_NYWCForms.Image = CType(resources.GetObject("tsbtn_NYWCForms.Image"),System.Drawing.Image)
        Me.tsbtn_NYWCForms.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbtn_NYWCForms.Name = "tsbtn_NYWCForms"
        Me.tsbtn_NYWCForms.Size = New System.Drawing.Size(100, 20)
        Me.tsbtn_NYWCForms.Tag = "NY WC Forms"
        Me.tsbtn_NYWCForms.Text = "NY WC Forms"
        '
        'tsbtn_SPB
        '
        Me.tsbtn_SPB.BackColor = System.Drawing.Color.Transparent
        Me.tsbtn_SPB.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.tsbtn_SPB.Image = CType(resources.GetObject("tsbtn_SPB.Image"),System.Drawing.Image)
        Me.tsbtn_SPB.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbtn_SPB.Name = "tsbtn_SPB"
        Me.tsbtn_SPB.Size = New System.Drawing.Size(48, 20)
        Me.tsbtn_SPB.Tag = "SPB"
        Me.tsbtn_SPB.Text = "SPB"
        Me.tsbtn_SPB.ToolTipText = "Social Psychological and Behavioral Observations"
        '
        'pnlTask
        '
        Me.pnlTask.AutoHide = true
        Me.pnlTask.Closed = true
        Me.pnlTask.GroupStyle = Janus.Windows.UI.Dock.PanelGroupStyle.Tab
        Me.pnlTask.Location = New System.Drawing.Point(0, 0)
        Me.pnlTask.Name = "pnlTask"
        Me.pnlTask.SelectedPanel = Me.pnlTask_RequestsSent
        Me.pnlTask.Size = New System.Drawing.Size(200, 200)
        Me.pnlTask.TabIndex = 4
        Me.pnlTask.Text = "Task"
        '
        'pnlTasks_MYTask
        '
        Me.pnlTasks_MYTask.Closed = true
        Me.pnlTasks_MYTask.InnerContainer = Me.pnlTasks_MYTaskContainer
        Me.pnlTasks_MYTask.Location = New System.Drawing.Point(4, 0)
        Me.pnlTasks_MYTask.Name = "pnlTasks_MYTask"
        Me.pnlTasks_MYTask.Size = New System.Drawing.Size(196, 181)
        Me.pnlTasks_MYTask.TabIndex = 4
        Me.pnlTasks_MYTask.Text = "My Tasks"
        '
        'pnlTasks_MYTaskContainer
        '
        Me.pnlTasks_MYTaskContainer.Location = New System.Drawing.Point(0, 0)
        Me.pnlTasks_MYTaskContainer.Name = "pnlTasks_MYTaskContainer"
        Me.pnlTasks_MYTaskContainer.Size = New System.Drawing.Size(196, 181)
        Me.pnlTasks_MYTaskContainer.TabIndex = 0
        '
        'pnlTask_TaskRequests
        '
        Me.pnlTask_TaskRequests.Closed = true
        Me.pnlTask_TaskRequests.InnerContainer = Me.pnlTask_TaskRequestsContainer
        Me.pnlTask_TaskRequests.Location = New System.Drawing.Point(4, 0)
        Me.pnlTask_TaskRequests.Name = "pnlTask_TaskRequests"
        Me.pnlTask_TaskRequests.Size = New System.Drawing.Size(196, 181)
        Me.pnlTask_TaskRequests.TabIndex = 4
        Me.pnlTask_TaskRequests.Text = "Task Requests"
        '
        'pnlTask_TaskRequestsContainer
        '
        Me.pnlTask_TaskRequestsContainer.Location = New System.Drawing.Point(0, 0)
        Me.pnlTask_TaskRequestsContainer.Name = "pnlTask_TaskRequestsContainer"
        Me.pnlTask_TaskRequestsContainer.Size = New System.Drawing.Size(196, 181)
        Me.pnlTask_TaskRequestsContainer.TabIndex = 0
        '
        'pnlTask_RequestsSent
        '
        Me.pnlTask_RequestsSent.Closed = true
        Me.pnlTask_RequestsSent.InnerContainer = Me.pnlTask_RequestsSentContainer
        Me.pnlTask_RequestsSent.Location = New System.Drawing.Point(4, 0)
        Me.pnlTask_RequestsSent.Name = "pnlTask_RequestsSent"
        Me.pnlTask_RequestsSent.Size = New System.Drawing.Size(196, 200)
        Me.pnlTask_RequestsSent.TabIndex = 4
        Me.pnlTask_RequestsSent.Text = "Request Sent"
        '
        'pnlTask_RequestsSentContainer
        '
        Me.pnlTask_RequestsSentContainer.Location = New System.Drawing.Point(0, 0)
        Me.pnlTask_RequestsSentContainer.Name = "pnlTask_RequestsSentContainer"
        Me.pnlTask_RequestsSentContainer.Size = New System.Drawing.Size(196, 200)
        Me.pnlTask_RequestsSentContainer.TabIndex = 0
        '
        'pnlGrPatientDemoCardsStatus
        '
        Me.pnlGrPatientDemoCardsStatus.GroupStyle = Janus.Windows.UI.Dock.PanelGroupStyle.VerticalTiles
        Me.pnlGrPatientDemoCardsStatus.Location = New System.Drawing.Point(280, 149)
        Me.pnlGrPatientDemoCardsStatus.Name = "pnlGrPatientDemoCardsStatus"
        Me.pnlGrPatientDemoCardsStatus.Size = New System.Drawing.Size(957, 332)
        Me.pnlGrPatientDemoCardsStatus.TabIndex = 78
        '
        'pnlPatientDemographics
        '
        Me.pnlPatientDemographics.CaptionFormatStyle.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.pnlPatientDemographics.CaptionStyle = Janus.Windows.UI.Dock.PanelCaptionStyle.Dark
        Me.pnlPatientDemographics.CloseButtonVisible = Janus.Windows.UI.InheritableBoolean.[False]
        Me.pnlPatientDemographics.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.pnlPatientDemographics.Icon = CType(resources.GetObject("pnlPatientDemographics.Icon"),System.Drawing.Icon)
        Me.pnlPatientDemographics.InnerContainer = Me.pnlPatientDemographicsContainer
        Me.pnlPatientDemographics.InnerContainerFormatStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(250,Byte),Integer), CType(CType(251,Byte),Integer), CType(CType(254,Byte),Integer))
        Me.pnlPatientDemographics.InnerContainerFormatStyle.BackColorGradient = System.Drawing.Color.FromArgb(CType(CType(227,Byte),Integer), CType(CType(241,Byte),Integer), CType(CType(255,Byte),Integer))
        Me.pnlPatientDemographics.InnerContainerFormatStyle.BackgroundGradientMode = Janus.Windows.UI.BackgroundGradientMode.Vertical
        Me.pnlPatientDemographics.Location = New System.Drawing.Point(0, 4)
        Me.pnlPatientDemographics.Name = "pnlPatientDemographics"
        Me.pnlPatientDemographics.Size = New System.Drawing.Size(373, 328)
        Me.pnlPatientDemographics.TabIndex = 4
        Me.pnlPatientDemographics.Text = "Patient Demographics"
        '
        'pnlPatientDemographicsContainer
        '
        Me.pnlPatientDemographicsContainer.AutoScroll = true
        Me.pnlPatientDemographicsContainer.BackColor = System.Drawing.Color.FromArgb(CType(CType(207,Byte),Integer), CType(CType(224,Byte),Integer), CType(CType(248,Byte),Integer))
        Me.pnlPatientDemographicsContainer.Controls.Add(Me.pnlRightFill)
        Me.pnlPatientDemographicsContainer.Controls.Add(Me.pnlLeft)
        Me.pnlPatientDemographicsContainer.Location = New System.Drawing.Point(1, 23)
        Me.pnlPatientDemographicsContainer.Name = "pnlPatientDemographicsContainer"
        Me.pnlPatientDemographicsContainer.Size = New System.Drawing.Size(371, 304)
        Me.pnlPatientDemographicsContainer.TabIndex = 0
        '
        'pnlRightFill
        '
        Me.pnlRightFill.AutoScroll = true
        Me.pnlRightFill.BackColor = System.Drawing.Color.Transparent
        Me.pnlRightFill.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlRightFill.Controls.Add(Me.pnl_workphone)
        Me.pnlRightFill.Controls.Add(Me.pnl_Businesscenter)
        Me.pnlRightFill.Controls.Add(Me.pnl_Occupation)
        Me.pnlRightFill.Controls.Add(Me.pnl_MedicalCategory)
        Me.pnlRightFill.Controls.Add(Me.pnl_Language)
        Me.pnlRightFill.Controls.Add(Me.pnl_Ethinicity)
        Me.pnlRightFill.Controls.Add(Me.pnl_Race)
        Me.pnlRightFill.Controls.Add(Me.pnl_LabStatus)
        Me.pnlRightFill.Controls.Add(Me.Pnl_PatPhone)
        Me.pnlRightFill.Controls.Add(Me.Pnl_PCPComPhone)
        Me.pnlRightFill.Controls.Add(Me.Pnl_PCPPracPhone)
        Me.pnlRightFill.Controls.Add(Me.pnl_TertiaryInsurance)
        Me.pnlRightFill.Controls.Add(Me.pnl_SecondaryInsurance)
        Me.pnlRightFill.Controls.Add(Me.pnl_PrimaryInsurance)
        Me.pnlRightFill.Controls.Add(Me.Pnl_PCPBusPhone)
        Me.pnlRightFill.Controls.Add(Me.Pnl_Status)
        Me.pnlRightFill.Controls.Add(Me.Pnl_PCP)
        Me.pnlRightFill.Controls.Add(Me.Pnl_Pharmacy)
        Me.pnlRightFill.Controls.Add(Me.Pnl_PharmacyAddress)
        Me.pnlRightFill.Controls.Add(Me.Pnl_PharmacyPhone)
        Me.pnlRightFill.Controls.Add(Me.pnl_Provider)
        Me.pnlRightFill.Controls.Add(Me.Pnl_EmMobile)
        Me.pnlRightFill.Controls.Add(Me.Pnl_EmPhone)
        Me.pnlRightFill.Controls.Add(Me.Pnl_EmContact)
        Me.pnlRightFill.Controls.Add(Me.Pnl_Email)
        Me.pnlRightFill.Controls.Add(Me.Pnl_Fax)
        Me.pnlRightFill.Controls.Add(Me.Pnl_Home)
        Me.pnlRightFill.Controls.Add(Me.Pnl_Mobile)
        Me.pnlRightFill.Controls.Add(Me.Pnl_Gender)
        Me.pnlRightFill.Controls.Add(Me.pnlAddress)
        Me.pnlRightFill.Controls.Add(Me.Label5)
        Me.pnlRightFill.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlRightFill.Location = New System.Drawing.Point(200, 0)
        Me.pnlRightFill.Name = "pnlRightFill"
        Me.pnlRightFill.Padding = New System.Windows.Forms.Padding(0, 8, 0, 0)
        Me.pnlRightFill.Size = New System.Drawing.Size(171, 304)
        Me.pnlRightFill.TabIndex = 153
        '
        'pnl_workphone
        '
        Me.pnl_workphone.BackColor = System.Drawing.Color.Transparent
        Me.pnl_workphone.Controls.Add(Me.lblworkphone)
        Me.pnl_workphone.Controls.Add(Me.Label89)
        Me.pnl_workphone.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_workphone.Location = New System.Drawing.Point(0, 442)
        Me.pnl_workphone.Name = "pnl_workphone"
        Me.pnl_workphone.Size = New System.Drawing.Size(154, 15)
        Me.pnl_workphone.TabIndex = 169
        '
        'lblworkphone
        '
        Me.lblworkphone.AutoEllipsis = true
        Me.lblworkphone.BackColor = System.Drawing.Color.Transparent
        Me.lblworkphone.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblworkphone.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblworkphone.ForeColor = System.Drawing.Color.FromArgb(CType(CType(21,Byte),Integer), CType(CType(66,Byte),Integer), CType(CType(139,Byte),Integer))
        Me.lblworkphone.Location = New System.Drawing.Point(91, 0)
        Me.lblworkphone.Name = "lblworkphone"
        Me.lblworkphone.Size = New System.Drawing.Size(63, 15)
        Me.lblworkphone.TabIndex = 151
        Me.lblworkphone.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label89
        '
        Me.Label89.BackColor = System.Drawing.Color.Transparent
        Me.Label89.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label89.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label89.ForeColor = System.Drawing.Color.FromArgb(CType(CType(21,Byte),Integer), CType(CType(66,Byte),Integer), CType(CType(139,Byte),Integer))
        Me.Label89.Location = New System.Drawing.Point(0, 0)
        Me.Label89.Name = "Label89"
        Me.Label89.Size = New System.Drawing.Size(91, 15)
        Me.Label89.TabIndex = 150
        Me.Label89.Text = "Work Phone :"
        Me.Label89.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'pnl_Businesscenter
        '
        Me.pnl_Businesscenter.BackColor = System.Drawing.Color.Transparent
        Me.pnl_Businesscenter.Controls.Add(Me.lblbusinesscenter)
        Me.pnl_Businesscenter.Controls.Add(Me.Label88)
        Me.pnl_Businesscenter.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_Businesscenter.Location = New System.Drawing.Point(0, 427)
        Me.pnl_Businesscenter.Name = "pnl_Businesscenter"
        Me.pnl_Businesscenter.Size = New System.Drawing.Size(154, 15)
        Me.pnl_Businesscenter.TabIndex = 168
        '
        'lblbusinesscenter
        '
        Me.lblbusinesscenter.AutoEllipsis = true
        Me.lblbusinesscenter.BackColor = System.Drawing.Color.Transparent
        Me.lblbusinesscenter.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblbusinesscenter.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblbusinesscenter.ForeColor = System.Drawing.Color.FromArgb(CType(CType(21,Byte),Integer), CType(CType(66,Byte),Integer), CType(CType(139,Byte),Integer))
        Me.lblbusinesscenter.Location = New System.Drawing.Point(91, 0)
        Me.lblbusinesscenter.Name = "lblbusinesscenter"
        Me.lblbusinesscenter.Size = New System.Drawing.Size(63, 15)
        Me.lblbusinesscenter.TabIndex = 151
        Me.lblbusinesscenter.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label88
        '
        Me.Label88.BackColor = System.Drawing.Color.Transparent
        Me.Label88.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label88.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label88.ForeColor = System.Drawing.Color.FromArgb(CType(CType(21,Byte),Integer), CType(CType(66,Byte),Integer), CType(CType(139,Byte),Integer))
        Me.Label88.Location = New System.Drawing.Point(0, 0)
        Me.Label88.Name = "Label88"
        Me.Label88.Size = New System.Drawing.Size(91, 15)
        Me.Label88.TabIndex = 150
        Me.Label88.Text = "Bus. Center :"
        Me.Label88.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'pnl_Occupation
        '
        Me.pnl_Occupation.BackColor = System.Drawing.Color.Transparent
        Me.pnl_Occupation.Controls.Add(Me.lbloccupation)
        Me.pnl_Occupation.Controls.Add(Me.Label87)
        Me.pnl_Occupation.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_Occupation.Location = New System.Drawing.Point(0, 412)
        Me.pnl_Occupation.Name = "pnl_Occupation"
        Me.pnl_Occupation.Size = New System.Drawing.Size(154, 15)
        Me.pnl_Occupation.TabIndex = 167
        '
        'lbloccupation
        '
        Me.lbloccupation.AutoEllipsis = true
        Me.lbloccupation.BackColor = System.Drawing.Color.Transparent
        Me.lbloccupation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbloccupation.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lbloccupation.ForeColor = System.Drawing.Color.FromArgb(CType(CType(21,Byte),Integer), CType(CType(66,Byte),Integer), CType(CType(139,Byte),Integer))
        Me.lbloccupation.Location = New System.Drawing.Point(91, 0)
        Me.lbloccupation.Name = "lbloccupation"
        Me.lbloccupation.Size = New System.Drawing.Size(63, 15)
        Me.lbloccupation.TabIndex = 151
        Me.lbloccupation.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label87
        '
        Me.Label87.BackColor = System.Drawing.Color.Transparent
        Me.Label87.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label87.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label87.ForeColor = System.Drawing.Color.FromArgb(CType(CType(21,Byte),Integer), CType(CType(66,Byte),Integer), CType(CType(139,Byte),Integer))
        Me.Label87.Location = New System.Drawing.Point(0, 0)
        Me.Label87.Name = "Label87"
        Me.Label87.Size = New System.Drawing.Size(91, 15)
        Me.Label87.TabIndex = 150
        Me.Label87.Text = "Occupation :"
        Me.Label87.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'pnl_MedicalCategory
        '
        Me.pnl_MedicalCategory.BackColor = System.Drawing.Color.Transparent
        Me.pnl_MedicalCategory.Controls.Add(Me.lblMedicalCategory)
        Me.pnl_MedicalCategory.Controls.Add(Me.Label69)
        Me.pnl_MedicalCategory.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_MedicalCategory.Location = New System.Drawing.Point(0, 397)
        Me.pnl_MedicalCategory.Name = "pnl_MedicalCategory"
        Me.pnl_MedicalCategory.Size = New System.Drawing.Size(154, 15)
        Me.pnl_MedicalCategory.TabIndex = 166
        '
        'lblMedicalCategory
        '
        Me.lblMedicalCategory.AutoEllipsis = true
        Me.lblMedicalCategory.BackColor = System.Drawing.Color.Transparent
        Me.lblMedicalCategory.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblMedicalCategory.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblMedicalCategory.ForeColor = System.Drawing.Color.FromArgb(CType(CType(21,Byte),Integer), CType(CType(66,Byte),Integer), CType(CType(139,Byte),Integer))
        Me.lblMedicalCategory.Location = New System.Drawing.Point(91, 0)
        Me.lblMedicalCategory.Name = "lblMedicalCategory"
        Me.lblMedicalCategory.Size = New System.Drawing.Size(63, 15)
        Me.lblMedicalCategory.TabIndex = 151
        Me.lblMedicalCategory.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label69
        '
        Me.Label69.BackColor = System.Drawing.Color.Transparent
        Me.Label69.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label69.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label69.ForeColor = System.Drawing.Color.FromArgb(CType(CType(21,Byte),Integer), CType(CType(66,Byte),Integer), CType(CType(139,Byte),Integer))
        Me.Label69.Location = New System.Drawing.Point(0, 0)
        Me.Label69.Name = "Label69"
        Me.Label69.Size = New System.Drawing.Size(91, 15)
        Me.Label69.TabIndex = 150
        Me.Label69.Text = "Med.Cat :"
        Me.Label69.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'pnl_Language
        '
        Me.pnl_Language.BackColor = System.Drawing.Color.Transparent
        Me.pnl_Language.Controls.Add(Me.lblLanguage)
        Me.pnl_Language.Controls.Add(Me.Label66)
        Me.pnl_Language.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_Language.Location = New System.Drawing.Point(0, 382)
        Me.pnl_Language.Name = "pnl_Language"
        Me.pnl_Language.Size = New System.Drawing.Size(154, 15)
        Me.pnl_Language.TabIndex = 163
        '
        'lblLanguage
        '
        Me.lblLanguage.AutoEllipsis = true
        Me.lblLanguage.BackColor = System.Drawing.Color.Transparent
        Me.lblLanguage.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblLanguage.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblLanguage.ForeColor = System.Drawing.Color.FromArgb(CType(CType(21,Byte),Integer), CType(CType(66,Byte),Integer), CType(CType(139,Byte),Integer))
        Me.lblLanguage.Location = New System.Drawing.Point(91, 0)
        Me.lblLanguage.Name = "lblLanguage"
        Me.lblLanguage.Size = New System.Drawing.Size(63, 15)
        Me.lblLanguage.TabIndex = 151
        Me.lblLanguage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label66
        '
        Me.Label66.BackColor = System.Drawing.Color.Transparent
        Me.Label66.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label66.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label66.ForeColor = System.Drawing.Color.FromArgb(CType(CType(21,Byte),Integer), CType(CType(66,Byte),Integer), CType(CType(139,Byte),Integer))
        Me.Label66.Location = New System.Drawing.Point(0, 0)
        Me.Label66.Name = "Label66"
        Me.Label66.Size = New System.Drawing.Size(91, 15)
        Me.Label66.TabIndex = 150
        Me.Label66.Text = "Language :"
        Me.Label66.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'pnl_Ethinicity
        '
        Me.pnl_Ethinicity.BackColor = System.Drawing.Color.Transparent
        Me.pnl_Ethinicity.Controls.Add(Me.lblEthinicity)
        Me.pnl_Ethinicity.Controls.Add(Me.Label68)
        Me.pnl_Ethinicity.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_Ethinicity.Location = New System.Drawing.Point(0, 367)
        Me.pnl_Ethinicity.Name = "pnl_Ethinicity"
        Me.pnl_Ethinicity.Size = New System.Drawing.Size(154, 15)
        Me.pnl_Ethinicity.TabIndex = 165
        '
        'lblEthinicity
        '
        Me.lblEthinicity.AutoEllipsis = true
        Me.lblEthinicity.BackColor = System.Drawing.Color.Transparent
        Me.lblEthinicity.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblEthinicity.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblEthinicity.ForeColor = System.Drawing.Color.FromArgb(CType(CType(21,Byte),Integer), CType(CType(66,Byte),Integer), CType(CType(139,Byte),Integer))
        Me.lblEthinicity.Location = New System.Drawing.Point(91, 0)
        Me.lblEthinicity.Name = "lblEthinicity"
        Me.lblEthinicity.Size = New System.Drawing.Size(63, 15)
        Me.lblEthinicity.TabIndex = 151
        Me.lblEthinicity.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label68
        '
        Me.Label68.BackColor = System.Drawing.Color.Transparent
        Me.Label68.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label68.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label68.ForeColor = System.Drawing.Color.FromArgb(CType(CType(21,Byte),Integer), CType(CType(66,Byte),Integer), CType(CType(139,Byte),Integer))
        Me.Label68.Location = New System.Drawing.Point(0, 0)
        Me.Label68.Name = "Label68"
        Me.Label68.Size = New System.Drawing.Size(91, 15)
        Me.Label68.TabIndex = 150
        Me.Label68.Text = "Ethnicity :"
        Me.Label68.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'pnl_Race
        '
        Me.pnl_Race.BackColor = System.Drawing.Color.Transparent
        Me.pnl_Race.Controls.Add(Me.lblRace)
        Me.pnl_Race.Controls.Add(Me.Label67)
        Me.pnl_Race.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_Race.Location = New System.Drawing.Point(0, 352)
        Me.pnl_Race.Name = "pnl_Race"
        Me.pnl_Race.Size = New System.Drawing.Size(154, 15)
        Me.pnl_Race.TabIndex = 164
        '
        'lblRace
        '
        Me.lblRace.AutoEllipsis = true
        Me.lblRace.BackColor = System.Drawing.Color.Transparent
        Me.lblRace.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblRace.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblRace.ForeColor = System.Drawing.Color.FromArgb(CType(CType(21,Byte),Integer), CType(CType(66,Byte),Integer), CType(CType(139,Byte),Integer))
        Me.lblRace.Location = New System.Drawing.Point(91, 0)
        Me.lblRace.Name = "lblRace"
        Me.lblRace.Size = New System.Drawing.Size(63, 15)
        Me.lblRace.TabIndex = 151
        Me.lblRace.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label67
        '
        Me.Label67.BackColor = System.Drawing.Color.Transparent
        Me.Label67.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label67.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label67.ForeColor = System.Drawing.Color.FromArgb(CType(CType(21,Byte),Integer), CType(CType(66,Byte),Integer), CType(CType(139,Byte),Integer))
        Me.Label67.Location = New System.Drawing.Point(0, 0)
        Me.Label67.Name = "Label67"
        Me.Label67.Size = New System.Drawing.Size(91, 15)
        Me.Label67.TabIndex = 150
        Me.Label67.Text = "Race :"
        Me.Label67.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'pnl_LabStatus
        '
        Me.pnl_LabStatus.BackColor = System.Drawing.Color.Transparent
        Me.pnl_LabStatus.Controls.Add(Me.lbl_ShowLabStatus)
        Me.pnl_LabStatus.Controls.Add(Me.lbl_LabStatus)
        Me.pnl_LabStatus.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_LabStatus.Location = New System.Drawing.Point(0, 337)
        Me.pnl_LabStatus.Name = "pnl_LabStatus"
        Me.pnl_LabStatus.Size = New System.Drawing.Size(154, 15)
        Me.pnl_LabStatus.TabIndex = 161
        '
        'lbl_ShowLabStatus
        '
        Me.lbl_ShowLabStatus.BackColor = System.Drawing.Color.Transparent
        Me.lbl_ShowLabStatus.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_ShowLabStatus.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lbl_ShowLabStatus.ForeColor = System.Drawing.Color.FromArgb(CType(CType(21,Byte),Integer), CType(CType(66,Byte),Integer), CType(CType(139,Byte),Integer))
        Me.lbl_ShowLabStatus.Location = New System.Drawing.Point(91, 0)
        Me.lbl_ShowLabStatus.Name = "lbl_ShowLabStatus"
        Me.lbl_ShowLabStatus.Size = New System.Drawing.Size(63, 15)
        Me.lbl_ShowLabStatus.TabIndex = 151
        Me.lbl_ShowLabStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lbl_LabStatus
        '
        Me.lbl_LabStatus.BackColor = System.Drawing.Color.Transparent
        Me.lbl_LabStatus.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_LabStatus.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lbl_LabStatus.ForeColor = System.Drawing.Color.FromArgb(CType(CType(21,Byte),Integer), CType(CType(66,Byte),Integer), CType(CType(139,Byte),Integer))
        Me.lbl_LabStatus.Location = New System.Drawing.Point(0, 0)
        Me.lbl_LabStatus.Name = "lbl_LabStatus"
        Me.lbl_LabStatus.Size = New System.Drawing.Size(91, 15)
        Me.lbl_LabStatus.TabIndex = 150
        Me.lbl_LabStatus.Text = "Lab Status :"
        Me.lbl_LabStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Pnl_PatPhone
        '
        Me.Pnl_PatPhone.Controls.Add(Me.lb_PatPhone)
        Me.Pnl_PatPhone.Controls.Add(Me.Label56)
        Me.Pnl_PatPhone.Dock = System.Windows.Forms.DockStyle.Top
        Me.Pnl_PatPhone.Location = New System.Drawing.Point(0, 322)
        Me.Pnl_PatPhone.Name = "Pnl_PatPhone"
        Me.Pnl_PatPhone.Size = New System.Drawing.Size(154, 15)
        Me.Pnl_PatPhone.TabIndex = 159
        '
        'lb_PatPhone
        '
        Me.lb_PatPhone.AutoEllipsis = true
        Me.lb_PatPhone.BackColor = System.Drawing.Color.Transparent
        Me.lb_PatPhone.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lb_PatPhone.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lb_PatPhone.ForeColor = System.Drawing.Color.FromArgb(CType(CType(21,Byte),Integer), CType(CType(66,Byte),Integer), CType(CType(139,Byte),Integer))
        Me.lb_PatPhone.Location = New System.Drawing.Point(91, 0)
        Me.lb_PatPhone.Name = "lb_PatPhone"
        Me.lb_PatPhone.Size = New System.Drawing.Size(63, 15)
        Me.lb_PatPhone.TabIndex = 151
        Me.lb_PatPhone.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label56
        '
        Me.Label56.BackColor = System.Drawing.Color.Transparent
        Me.Label56.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label56.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label56.ForeColor = System.Drawing.Color.FromArgb(CType(CType(21,Byte),Integer), CType(CType(66,Byte),Integer), CType(CType(139,Byte),Integer))
        Me.Label56.Location = New System.Drawing.Point(0, 0)
        Me.Label56.Name = "Label56"
        Me.Label56.Size = New System.Drawing.Size(91, 15)
        Me.Label56.TabIndex = 150
        Me.Label56.Text = " Phone :"
        Me.Label56.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Pnl_PCPComPhone
        '
        Me.Pnl_PCPComPhone.Controls.Add(Me.lbl_ShowPCPCpmPhone)
        Me.Pnl_PCPComPhone.Controls.Add(Me.lbl_PCPComPhone)
        Me.Pnl_PCPComPhone.Dock = System.Windows.Forms.DockStyle.Top
        Me.Pnl_PCPComPhone.Location = New System.Drawing.Point(0, 307)
        Me.Pnl_PCPComPhone.Name = "Pnl_PCPComPhone"
        Me.Pnl_PCPComPhone.Size = New System.Drawing.Size(154, 15)
        Me.Pnl_PCPComPhone.TabIndex = 158
        '
        'lbl_ShowPCPCpmPhone
        '
        Me.lbl_ShowPCPCpmPhone.AutoEllipsis = true
        Me.lbl_ShowPCPCpmPhone.BackColor = System.Drawing.Color.Transparent
        Me.lbl_ShowPCPCpmPhone.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_ShowPCPCpmPhone.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lbl_ShowPCPCpmPhone.ForeColor = System.Drawing.Color.FromArgb(CType(CType(21,Byte),Integer), CType(CType(66,Byte),Integer), CType(CType(139,Byte),Integer))
        Me.lbl_ShowPCPCpmPhone.Location = New System.Drawing.Point(91, 0)
        Me.lbl_ShowPCPCpmPhone.Name = "lbl_ShowPCPCpmPhone"
        Me.lbl_ShowPCPCpmPhone.Size = New System.Drawing.Size(63, 15)
        Me.lbl_ShowPCPCpmPhone.TabIndex = 151
        Me.lbl_ShowPCPCpmPhone.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lbl_PCPComPhone
        '
        Me.lbl_PCPComPhone.BackColor = System.Drawing.Color.Transparent
        Me.lbl_PCPComPhone.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_PCPComPhone.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lbl_PCPComPhone.ForeColor = System.Drawing.Color.FromArgb(CType(CType(21,Byte),Integer), CType(CType(66,Byte),Integer), CType(CType(139,Byte),Integer))
        Me.lbl_PCPComPhone.Location = New System.Drawing.Point(0, 0)
        Me.lbl_PCPComPhone.Name = "lbl_PCPComPhone"
        Me.lbl_PCPComPhone.Size = New System.Drawing.Size(91, 15)
        Me.lbl_PCPComPhone.TabIndex = 150
        Me.lbl_PCPComPhone.Text = "PCP Com.Ph. :"
        Me.lbl_PCPComPhone.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Pnl_PCPPracPhone
        '
        Me.Pnl_PCPPracPhone.Controls.Add(Me.lbl_ShowPCPPracPhone)
        Me.Pnl_PCPPracPhone.Controls.Add(Me.lbl_PCPPracPhone)
        Me.Pnl_PCPPracPhone.Dock = System.Windows.Forms.DockStyle.Top
        Me.Pnl_PCPPracPhone.Location = New System.Drawing.Point(0, 292)
        Me.Pnl_PCPPracPhone.Name = "Pnl_PCPPracPhone"
        Me.Pnl_PCPPracPhone.Size = New System.Drawing.Size(154, 15)
        Me.Pnl_PCPPracPhone.TabIndex = 157
        '
        'lbl_ShowPCPPracPhone
        '
        Me.lbl_ShowPCPPracPhone.AutoEllipsis = true
        Me.lbl_ShowPCPPracPhone.BackColor = System.Drawing.Color.Transparent
        Me.lbl_ShowPCPPracPhone.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_ShowPCPPracPhone.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lbl_ShowPCPPracPhone.ForeColor = System.Drawing.Color.FromArgb(CType(CType(21,Byte),Integer), CType(CType(66,Byte),Integer), CType(CType(139,Byte),Integer))
        Me.lbl_ShowPCPPracPhone.Location = New System.Drawing.Point(91, 0)
        Me.lbl_ShowPCPPracPhone.Name = "lbl_ShowPCPPracPhone"
        Me.lbl_ShowPCPPracPhone.Size = New System.Drawing.Size(63, 15)
        Me.lbl_ShowPCPPracPhone.TabIndex = 151
        Me.lbl_ShowPCPPracPhone.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lbl_PCPPracPhone
        '
        Me.lbl_PCPPracPhone.BackColor = System.Drawing.Color.Transparent
        Me.lbl_PCPPracPhone.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_PCPPracPhone.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lbl_PCPPracPhone.ForeColor = System.Drawing.Color.FromArgb(CType(CType(21,Byte),Integer), CType(CType(66,Byte),Integer), CType(CType(139,Byte),Integer))
        Me.lbl_PCPPracPhone.Location = New System.Drawing.Point(0, 0)
        Me.lbl_PCPPracPhone.Name = "lbl_PCPPracPhone"
        Me.lbl_PCPPracPhone.Size = New System.Drawing.Size(91, 15)
        Me.lbl_PCPPracPhone.TabIndex = 150
        Me.lbl_PCPPracPhone.Text = "PCP Prac.Ph. :"
        Me.lbl_PCPPracPhone.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'pnl_TertiaryInsurance
        '
        Me.pnl_TertiaryInsurance.BackColor = System.Drawing.Color.Transparent
        Me.pnl_TertiaryInsurance.Controls.Add(Me.lbl_TertiaryInsurance)
        Me.pnl_TertiaryInsurance.Controls.Add(Me.Label90)
        Me.pnl_TertiaryInsurance.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_TertiaryInsurance.Location = New System.Drawing.Point(0, 277)
        Me.pnl_TertiaryInsurance.Name = "pnl_TertiaryInsurance"
        Me.pnl_TertiaryInsurance.Size = New System.Drawing.Size(154, 15)
        Me.pnl_TertiaryInsurance.TabIndex = 171
        '
        'lbl_TertiaryInsurance
        '
        Me.lbl_TertiaryInsurance.AutoEllipsis = true
        Me.lbl_TertiaryInsurance.BackColor = System.Drawing.Color.Transparent
        Me.lbl_TertiaryInsurance.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_TertiaryInsurance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lbl_TertiaryInsurance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(21,Byte),Integer), CType(CType(66,Byte),Integer), CType(CType(139,Byte),Integer))
        Me.lbl_TertiaryInsurance.Location = New System.Drawing.Point(91, 0)
        Me.lbl_TertiaryInsurance.Name = "lbl_TertiaryInsurance"
        Me.lbl_TertiaryInsurance.Size = New System.Drawing.Size(63, 15)
        Me.lbl_TertiaryInsurance.TabIndex = 151
        Me.lbl_TertiaryInsurance.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label90
        '
        Me.Label90.BackColor = System.Drawing.Color.Transparent
        Me.Label90.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label90.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label90.ForeColor = System.Drawing.Color.FromArgb(CType(CType(21,Byte),Integer), CType(CType(66,Byte),Integer), CType(CType(139,Byte),Integer))
        Me.Label90.Location = New System.Drawing.Point(0, 0)
        Me.Label90.Name = "Label90"
        Me.Label90.Size = New System.Drawing.Size(91, 15)
        Me.Label90.TabIndex = 150
        Me.Label90.Text = "Tertiary Ins. :"
        Me.Label90.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'pnl_SecondaryInsurance
        '
        Me.pnl_SecondaryInsurance.BackColor = System.Drawing.Color.Transparent
        Me.pnl_SecondaryInsurance.Controls.Add(Me.lblSecondaryInsurance)
        Me.pnl_SecondaryInsurance.Controls.Add(Me.Label91)
        Me.pnl_SecondaryInsurance.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_SecondaryInsurance.Location = New System.Drawing.Point(0, 262)
        Me.pnl_SecondaryInsurance.Name = "pnl_SecondaryInsurance"
        Me.pnl_SecondaryInsurance.Size = New System.Drawing.Size(154, 15)
        Me.pnl_SecondaryInsurance.TabIndex = 172
        '
        'lblSecondaryInsurance
        '
        Me.lblSecondaryInsurance.AutoEllipsis = true
        Me.lblSecondaryInsurance.BackColor = System.Drawing.Color.Transparent
        Me.lblSecondaryInsurance.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblSecondaryInsurance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblSecondaryInsurance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(21,Byte),Integer), CType(CType(66,Byte),Integer), CType(CType(139,Byte),Integer))
        Me.lblSecondaryInsurance.Location = New System.Drawing.Point(91, 0)
        Me.lblSecondaryInsurance.Name = "lblSecondaryInsurance"
        Me.lblSecondaryInsurance.Size = New System.Drawing.Size(63, 15)
        Me.lblSecondaryInsurance.TabIndex = 151
        Me.lblSecondaryInsurance.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label91
        '
        Me.Label91.BackColor = System.Drawing.Color.Transparent
        Me.Label91.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label91.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label91.ForeColor = System.Drawing.Color.FromArgb(CType(CType(21,Byte),Integer), CType(CType(66,Byte),Integer), CType(CType(139,Byte),Integer))
        Me.Label91.Location = New System.Drawing.Point(0, 0)
        Me.Label91.Name = "Label91"
        Me.Label91.Size = New System.Drawing.Size(91, 15)
        Me.Label91.TabIndex = 150
        Me.Label91.Text = "Secondary Ins. :"
        Me.Label91.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'pnl_PrimaryInsurance
        '
        Me.pnl_PrimaryInsurance.BackColor = System.Drawing.Color.Transparent
        Me.pnl_PrimaryInsurance.Controls.Add(Me.lblPrimaryInsurance)
        Me.pnl_PrimaryInsurance.Controls.Add(Me.Label92)
        Me.pnl_PrimaryInsurance.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_PrimaryInsurance.Location = New System.Drawing.Point(0, 247)
        Me.pnl_PrimaryInsurance.Name = "pnl_PrimaryInsurance"
        Me.pnl_PrimaryInsurance.Size = New System.Drawing.Size(154, 15)
        Me.pnl_PrimaryInsurance.TabIndex = 173
        '
        'lblPrimaryInsurance
        '
        Me.lblPrimaryInsurance.AutoEllipsis = true
        Me.lblPrimaryInsurance.BackColor = System.Drawing.Color.Transparent
        Me.lblPrimaryInsurance.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblPrimaryInsurance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblPrimaryInsurance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(21,Byte),Integer), CType(CType(66,Byte),Integer), CType(CType(139,Byte),Integer))
        Me.lblPrimaryInsurance.Location = New System.Drawing.Point(91, 0)
        Me.lblPrimaryInsurance.Name = "lblPrimaryInsurance"
        Me.lblPrimaryInsurance.Size = New System.Drawing.Size(63, 15)
        Me.lblPrimaryInsurance.TabIndex = 151
        Me.lblPrimaryInsurance.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label92
        '
        Me.Label92.BackColor = System.Drawing.Color.Transparent
        Me.Label92.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label92.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label92.ForeColor = System.Drawing.Color.FromArgb(CType(CType(21,Byte),Integer), CType(CType(66,Byte),Integer), CType(CType(139,Byte),Integer))
        Me.Label92.Location = New System.Drawing.Point(0, 0)
        Me.Label92.Name = "Label92"
        Me.Label92.Size = New System.Drawing.Size(91, 15)
        Me.Label92.TabIndex = 150
        Me.Label92.Text = "Primary Ins. :"
        Me.Label92.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Pnl_PCPBusPhone
        '
        Me.Pnl_PCPBusPhone.Controls.Add(Me.lbl_ShowPCPbusPhone)
        Me.Pnl_PCPBusPhone.Controls.Add(Me.lbl_PCPbusPhone)
        Me.Pnl_PCPBusPhone.Dock = System.Windows.Forms.DockStyle.Top
        Me.Pnl_PCPBusPhone.Location = New System.Drawing.Point(0, 232)
        Me.Pnl_PCPBusPhone.Name = "Pnl_PCPBusPhone"
        Me.Pnl_PCPBusPhone.Size = New System.Drawing.Size(154, 15)
        Me.Pnl_PCPBusPhone.TabIndex = 156
        '
        'lbl_ShowPCPbusPhone
        '
        Me.lbl_ShowPCPbusPhone.AutoEllipsis = true
        Me.lbl_ShowPCPbusPhone.BackColor = System.Drawing.Color.Transparent
        Me.lbl_ShowPCPbusPhone.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_ShowPCPbusPhone.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lbl_ShowPCPbusPhone.ForeColor = System.Drawing.Color.FromArgb(CType(CType(21,Byte),Integer), CType(CType(66,Byte),Integer), CType(CType(139,Byte),Integer))
        Me.lbl_ShowPCPbusPhone.Location = New System.Drawing.Point(91, 0)
        Me.lbl_ShowPCPbusPhone.Name = "lbl_ShowPCPbusPhone"
        Me.lbl_ShowPCPbusPhone.Size = New System.Drawing.Size(63, 15)
        Me.lbl_ShowPCPbusPhone.TabIndex = 151
        Me.lbl_ShowPCPbusPhone.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lbl_PCPbusPhone
        '
        Me.lbl_PCPbusPhone.BackColor = System.Drawing.Color.Transparent
        Me.lbl_PCPbusPhone.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_PCPbusPhone.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lbl_PCPbusPhone.ForeColor = System.Drawing.Color.FromArgb(CType(CType(21,Byte),Integer), CType(CType(66,Byte),Integer), CType(CType(139,Byte),Integer))
        Me.lbl_PCPbusPhone.Location = New System.Drawing.Point(0, 0)
        Me.lbl_PCPbusPhone.Name = "lbl_PCPbusPhone"
        Me.lbl_PCPbusPhone.Size = New System.Drawing.Size(91, 15)
        Me.lbl_PCPbusPhone.TabIndex = 150
        Me.lbl_PCPbusPhone.Text = "PCP Phone :"
        Me.lbl_PCPbusPhone.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Pnl_Status
        '
        Me.Pnl_Status.Controls.Add(Me.lbl_ShowStatus)
        Me.Pnl_Status.Controls.Add(Me.lbl_Patientstatus)
        Me.Pnl_Status.Dock = System.Windows.Forms.DockStyle.Top
        Me.Pnl_Status.Location = New System.Drawing.Point(0, 217)
        Me.Pnl_Status.Name = "Pnl_Status"
        Me.Pnl_Status.Size = New System.Drawing.Size(154, 15)
        Me.Pnl_Status.TabIndex = 154
        '
        'lbl_ShowStatus
        '
        Me.lbl_ShowStatus.AutoEllipsis = true
        Me.lbl_ShowStatus.BackColor = System.Drawing.Color.Transparent
        Me.lbl_ShowStatus.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_ShowStatus.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lbl_ShowStatus.ForeColor = System.Drawing.Color.FromArgb(CType(CType(21,Byte),Integer), CType(CType(66,Byte),Integer), CType(CType(139,Byte),Integer))
        Me.lbl_ShowStatus.Location = New System.Drawing.Point(91, 0)
        Me.lbl_ShowStatus.Name = "lbl_ShowStatus"
        Me.lbl_ShowStatus.Size = New System.Drawing.Size(63, 15)
        Me.lbl_ShowStatus.TabIndex = 151
        Me.lbl_ShowStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lbl_Patientstatus
        '
        Me.lbl_Patientstatus.BackColor = System.Drawing.Color.Transparent
        Me.lbl_Patientstatus.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_Patientstatus.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lbl_Patientstatus.ForeColor = System.Drawing.Color.FromArgb(CType(CType(21,Byte),Integer), CType(CType(66,Byte),Integer), CType(CType(139,Byte),Integer))
        Me.lbl_Patientstatus.Location = New System.Drawing.Point(0, 0)
        Me.lbl_Patientstatus.Name = "lbl_Patientstatus"
        Me.lbl_Patientstatus.Size = New System.Drawing.Size(91, 15)
        Me.lbl_Patientstatus.TabIndex = 150
        Me.lbl_Patientstatus.Text = "Status :"
        Me.lbl_Patientstatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Pnl_PCP
        '
        Me.Pnl_PCP.Controls.Add(Me.lblPD_PriCarePhysician)
        Me.Pnl_PCP.Controls.Add(Me.Label60)
        Me.Pnl_PCP.Dock = System.Windows.Forms.DockStyle.Top
        Me.Pnl_PCP.Location = New System.Drawing.Point(0, 202)
        Me.Pnl_PCP.Name = "Pnl_PCP"
        Me.Pnl_PCP.Size = New System.Drawing.Size(154, 15)
        Me.Pnl_PCP.TabIndex = 153
        '
        'lblPD_PriCarePhysician
        '
        Me.lblPD_PriCarePhysician.AutoEllipsis = true
        Me.lblPD_PriCarePhysician.BackColor = System.Drawing.Color.Transparent
        Me.lblPD_PriCarePhysician.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblPD_PriCarePhysician.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblPD_PriCarePhysician.ForeColor = System.Drawing.Color.FromArgb(CType(CType(21,Byte),Integer), CType(CType(66,Byte),Integer), CType(CType(139,Byte),Integer))
        Me.lblPD_PriCarePhysician.Location = New System.Drawing.Point(91, 0)
        Me.lblPD_PriCarePhysician.Name = "lblPD_PriCarePhysician"
        Me.lblPD_PriCarePhysician.Size = New System.Drawing.Size(63, 15)
        Me.lblPD_PriCarePhysician.TabIndex = 144
        Me.lblPD_PriCarePhysician.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label60
        '
        Me.Label60.BackColor = System.Drawing.Color.Transparent
        Me.Label60.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label60.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label60.ForeColor = System.Drawing.Color.FromArgb(CType(CType(21,Byte),Integer), CType(CType(66,Byte),Integer), CType(CType(139,Byte),Integer))
        Me.Label60.Location = New System.Drawing.Point(0, 0)
        Me.Label60.Name = "Label60"
        Me.Label60.Size = New System.Drawing.Size(91, 15)
        Me.Label60.TabIndex = 142
        Me.Label60.Text = "PCP :"
        Me.Label60.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Pnl_Pharmacy
        '
        Me.Pnl_Pharmacy.Controls.Add(Me.lblPD_Pharmacy)
        Me.Pnl_Pharmacy.Controls.Add(Me.Label39)
        Me.Pnl_Pharmacy.Dock = System.Windows.Forms.DockStyle.Top
        Me.Pnl_Pharmacy.Location = New System.Drawing.Point(0, 187)
        Me.Pnl_Pharmacy.Name = "Pnl_Pharmacy"
        Me.Pnl_Pharmacy.Size = New System.Drawing.Size(154, 15)
        Me.Pnl_Pharmacy.TabIndex = 5
        '
        'lblPD_Pharmacy
        '
        Me.lblPD_Pharmacy.AutoEllipsis = true
        Me.lblPD_Pharmacy.BackColor = System.Drawing.Color.Transparent
        Me.lblPD_Pharmacy.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblPD_Pharmacy.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblPD_Pharmacy.ForeColor = System.Drawing.Color.FromArgb(CType(CType(21,Byte),Integer), CType(CType(66,Byte),Integer), CType(CType(139,Byte),Integer))
        Me.lblPD_Pharmacy.Location = New System.Drawing.Point(91, 0)
        Me.lblPD_Pharmacy.Name = "lblPD_Pharmacy"
        Me.lblPD_Pharmacy.Size = New System.Drawing.Size(63, 15)
        Me.lblPD_Pharmacy.TabIndex = 143
        Me.lblPD_Pharmacy.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label39
        '
        Me.Label39.BackColor = System.Drawing.Color.Transparent
        Me.Label39.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label39.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label39.ForeColor = System.Drawing.Color.FromArgb(CType(CType(21,Byte),Integer), CType(CType(66,Byte),Integer), CType(CType(139,Byte),Integer))
        Me.Label39.Location = New System.Drawing.Point(0, 0)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(91, 15)
        Me.Label39.TabIndex = 141
        Me.Label39.Text = "Pharmacy :"
        Me.Label39.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Pnl_PharmacyAddress
        '
        Me.Pnl_PharmacyAddress.BackColor = System.Drawing.Color.Transparent
        Me.Pnl_PharmacyAddress.Controls.Add(Me.lblPD_PharmacyAddress)
        Me.Pnl_PharmacyAddress.Controls.Add(Me.lblPD_PharmacyAddr)
        Me.Pnl_PharmacyAddress.Dock = System.Windows.Forms.DockStyle.Top
        Me.Pnl_PharmacyAddress.Location = New System.Drawing.Point(0, 172)
        Me.Pnl_PharmacyAddress.Name = "Pnl_PharmacyAddress"
        Me.Pnl_PharmacyAddress.Size = New System.Drawing.Size(154, 15)
        Me.Pnl_PharmacyAddress.TabIndex = 170
        '
        'lblPD_PharmacyAddress
        '
        Me.lblPD_PharmacyAddress.AutoEllipsis = true
        Me.lblPD_PharmacyAddress.BackColor = System.Drawing.Color.Transparent
        Me.lblPD_PharmacyAddress.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblPD_PharmacyAddress.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblPD_PharmacyAddress.ForeColor = System.Drawing.Color.FromArgb(CType(CType(21,Byte),Integer), CType(CType(66,Byte),Integer), CType(CType(139,Byte),Integer))
        Me.lblPD_PharmacyAddress.Location = New System.Drawing.Point(91, 0)
        Me.lblPD_PharmacyAddress.Name = "lblPD_PharmacyAddress"
        Me.lblPD_PharmacyAddress.Size = New System.Drawing.Size(63, 15)
        Me.lblPD_PharmacyAddress.TabIndex = 143
        Me.lblPD_PharmacyAddress.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblPD_PharmacyAddr
        '
        Me.lblPD_PharmacyAddr.BackColor = System.Drawing.Color.Transparent
        Me.lblPD_PharmacyAddr.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblPD_PharmacyAddr.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblPD_PharmacyAddr.ForeColor = System.Drawing.Color.FromArgb(CType(CType(21,Byte),Integer), CType(CType(66,Byte),Integer), CType(CType(139,Byte),Integer))
        Me.lblPD_PharmacyAddr.Location = New System.Drawing.Point(0, 0)
        Me.lblPD_PharmacyAddr.Name = "lblPD_PharmacyAddr"
        Me.lblPD_PharmacyAddr.Size = New System.Drawing.Size(91, 15)
        Me.lblPD_PharmacyAddr.TabIndex = 141
        Me.lblPD_PharmacyAddr.Text = "Pharmacy Addr. :"
        Me.lblPD_PharmacyAddr.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Pnl_PharmacyPhone
        '
        Me.Pnl_PharmacyPhone.Controls.Add(Me.lblPD_PharmacyPhone)
        Me.Pnl_PharmacyPhone.Controls.Add(Me.Label61)
        Me.Pnl_PharmacyPhone.Dock = System.Windows.Forms.DockStyle.Top
        Me.Pnl_PharmacyPhone.Location = New System.Drawing.Point(0, 157)
        Me.Pnl_PharmacyPhone.Name = "Pnl_PharmacyPhone"
        Me.Pnl_PharmacyPhone.Size = New System.Drawing.Size(154, 15)
        Me.Pnl_PharmacyPhone.TabIndex = 162
        '
        'lblPD_PharmacyPhone
        '
        Me.lblPD_PharmacyPhone.AutoEllipsis = true
        Me.lblPD_PharmacyPhone.BackColor = System.Drawing.Color.Transparent
        Me.lblPD_PharmacyPhone.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblPD_PharmacyPhone.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblPD_PharmacyPhone.ForeColor = System.Drawing.Color.FromArgb(CType(CType(21,Byte),Integer), CType(CType(66,Byte),Integer), CType(CType(139,Byte),Integer))
        Me.lblPD_PharmacyPhone.Location = New System.Drawing.Point(91, 0)
        Me.lblPD_PharmacyPhone.Name = "lblPD_PharmacyPhone"
        Me.lblPD_PharmacyPhone.Size = New System.Drawing.Size(63, 15)
        Me.lblPD_PharmacyPhone.TabIndex = 143
        Me.lblPD_PharmacyPhone.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label61
        '
        Me.Label61.BackColor = System.Drawing.Color.Transparent
        Me.Label61.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label61.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label61.ForeColor = System.Drawing.Color.FromArgb(CType(CType(21,Byte),Integer), CType(CType(66,Byte),Integer), CType(CType(139,Byte),Integer))
        Me.Label61.Location = New System.Drawing.Point(0, 0)
        Me.Label61.Name = "Label61"
        Me.Label61.Size = New System.Drawing.Size(91, 15)
        Me.Label61.TabIndex = 141
        Me.Label61.Text = "Pharmacy Ph. :"
        Me.Label61.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'pnl_Provider
        '
        Me.pnl_Provider.BackColor = System.Drawing.Color.Transparent
        Me.pnl_Provider.Controls.Add(Me.lblPD_Provider)
        Me.pnl_Provider.Controls.Add(Me.Label93)
        Me.pnl_Provider.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_Provider.Location = New System.Drawing.Point(0, 142)
        Me.pnl_Provider.Name = "pnl_Provider"
        Me.pnl_Provider.Size = New System.Drawing.Size(154, 15)
        Me.pnl_Provider.TabIndex = 174
        '
        'lblPD_Provider
        '
        Me.lblPD_Provider.AutoEllipsis = true
        Me.lblPD_Provider.BackColor = System.Drawing.Color.Transparent
        Me.lblPD_Provider.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblPD_Provider.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblPD_Provider.ForeColor = System.Drawing.Color.FromArgb(CType(CType(21,Byte),Integer), CType(CType(66,Byte),Integer), CType(CType(139,Byte),Integer))
        Me.lblPD_Provider.Location = New System.Drawing.Point(91, 0)
        Me.lblPD_Provider.Name = "lblPD_Provider"
        Me.lblPD_Provider.Size = New System.Drawing.Size(63, 15)
        Me.lblPD_Provider.TabIndex = 151
        Me.lblPD_Provider.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label93
        '
        Me.Label93.BackColor = System.Drawing.Color.Transparent
        Me.Label93.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label93.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label93.ForeColor = System.Drawing.Color.FromArgb(CType(CType(21,Byte),Integer), CType(CType(66,Byte),Integer), CType(CType(139,Byte),Integer))
        Me.Label93.Location = New System.Drawing.Point(0, 0)
        Me.Label93.Name = "Label93"
        Me.Label93.Size = New System.Drawing.Size(91, 15)
        Me.Label93.TabIndex = 150
        Me.Label93.Text = "Provider :"
        Me.Label93.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Pnl_EmMobile
        '
        Me.Pnl_EmMobile.Controls.Add(Me.lbl_EmMobile)
        Me.Pnl_EmMobile.Controls.Add(Me.Label57)
        Me.Pnl_EmMobile.Dock = System.Windows.Forms.DockStyle.Top
        Me.Pnl_EmMobile.Location = New System.Drawing.Point(0, 127)
        Me.Pnl_EmMobile.Name = "Pnl_EmMobile"
        Me.Pnl_EmMobile.Size = New System.Drawing.Size(154, 15)
        Me.Pnl_EmMobile.TabIndex = 2
        '
        'lbl_EmMobile
        '
        Me.lbl_EmMobile.AutoEllipsis = true
        Me.lbl_EmMobile.BackColor = System.Drawing.Color.Transparent
        Me.lbl_EmMobile.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_EmMobile.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lbl_EmMobile.ForeColor = System.Drawing.Color.FromArgb(CType(CType(21,Byte),Integer), CType(CType(66,Byte),Integer), CType(CType(139,Byte),Integer))
        Me.lbl_EmMobile.Location = New System.Drawing.Point(91, 0)
        Me.lbl_EmMobile.Name = "lbl_EmMobile"
        Me.lbl_EmMobile.Size = New System.Drawing.Size(63, 15)
        Me.lbl_EmMobile.TabIndex = 151
        Me.lbl_EmMobile.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label57
        '
        Me.Label57.BackColor = System.Drawing.Color.Transparent
        Me.Label57.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label57.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label57.ForeColor = System.Drawing.Color.FromArgb(CType(CType(21,Byte),Integer), CType(CType(66,Byte),Integer), CType(CType(139,Byte),Integer))
        Me.Label57.Location = New System.Drawing.Point(0, 0)
        Me.Label57.Name = "Label57"
        Me.Label57.Size = New System.Drawing.Size(91, 15)
        Me.Label57.TabIndex = 150
        Me.Label57.Text = "EM.Mobile :"
        Me.Label57.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Pnl_EmPhone
        '
        Me.Pnl_EmPhone.Controls.Add(Me.lbl_EmPhone)
        Me.Pnl_EmPhone.Controls.Add(Me.Label47)
        Me.Pnl_EmPhone.Dock = System.Windows.Forms.DockStyle.Top
        Me.Pnl_EmPhone.Location = New System.Drawing.Point(0, 112)
        Me.Pnl_EmPhone.Name = "Pnl_EmPhone"
        Me.Pnl_EmPhone.Size = New System.Drawing.Size(154, 15)
        Me.Pnl_EmPhone.TabIndex = 4
        '
        'lbl_EmPhone
        '
        Me.lbl_EmPhone.AutoEllipsis = true
        Me.lbl_EmPhone.BackColor = System.Drawing.Color.Transparent
        Me.lbl_EmPhone.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_EmPhone.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lbl_EmPhone.ForeColor = System.Drawing.Color.FromArgb(CType(CType(21,Byte),Integer), CType(CType(66,Byte),Integer), CType(CType(139,Byte),Integer))
        Me.lbl_EmPhone.Location = New System.Drawing.Point(91, 0)
        Me.lbl_EmPhone.Name = "lbl_EmPhone"
        Me.lbl_EmPhone.Size = New System.Drawing.Size(63, 15)
        Me.lbl_EmPhone.TabIndex = 151
        Me.lbl_EmPhone.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label47
        '
        Me.Label47.BackColor = System.Drawing.Color.Transparent
        Me.Label47.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label47.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label47.ForeColor = System.Drawing.Color.FromArgb(CType(CType(21,Byte),Integer), CType(CType(66,Byte),Integer), CType(CType(139,Byte),Integer))
        Me.Label47.Location = New System.Drawing.Point(0, 0)
        Me.Label47.Name = "Label47"
        Me.Label47.Size = New System.Drawing.Size(91, 15)
        Me.Label47.TabIndex = 150
        Me.Label47.Text = "EM.Phone :"
        Me.Label47.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Pnl_EmContact
        '
        Me.Pnl_EmContact.Controls.Add(Me.lbl_Emcontact)
        Me.Pnl_EmContact.Controls.Add(Me.Label40)
        Me.Pnl_EmContact.Dock = System.Windows.Forms.DockStyle.Top
        Me.Pnl_EmContact.Location = New System.Drawing.Point(0, 97)
        Me.Pnl_EmContact.Name = "Pnl_EmContact"
        Me.Pnl_EmContact.Size = New System.Drawing.Size(154, 15)
        Me.Pnl_EmContact.TabIndex = 2
        '
        'lbl_Emcontact
        '
        Me.lbl_Emcontact.AutoEllipsis = true
        Me.lbl_Emcontact.BackColor = System.Drawing.Color.Transparent
        Me.lbl_Emcontact.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_Emcontact.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lbl_Emcontact.ForeColor = System.Drawing.Color.FromArgb(CType(CType(21,Byte),Integer), CType(CType(66,Byte),Integer), CType(CType(139,Byte),Integer))
        Me.lbl_Emcontact.Location = New System.Drawing.Point(91, 0)
        Me.lbl_Emcontact.Name = "lbl_Emcontact"
        Me.lbl_Emcontact.Size = New System.Drawing.Size(63, 15)
        Me.lbl_Emcontact.TabIndex = 144
        Me.lbl_Emcontact.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label40
        '
        Me.Label40.BackColor = System.Drawing.Color.Transparent
        Me.Label40.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label40.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label40.ForeColor = System.Drawing.Color.FromArgb(CType(CType(21,Byte),Integer), CType(CType(66,Byte),Integer), CType(CType(139,Byte),Integer))
        Me.Label40.Location = New System.Drawing.Point(0, 0)
        Me.Label40.Name = "Label40"
        Me.Label40.Size = New System.Drawing.Size(91, 15)
        Me.Label40.TabIndex = 142
        Me.Label40.Text = "EM.Contact :"
        Me.Label40.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Pnl_Email
        '
        Me.Pnl_Email.Controls.Add(Me.lblPD_Email)
        Me.Pnl_Email.Controls.Add(Me.Label7)
        Me.Pnl_Email.Dock = System.Windows.Forms.DockStyle.Top
        Me.Pnl_Email.Location = New System.Drawing.Point(0, 82)
        Me.Pnl_Email.Name = "Pnl_Email"
        Me.Pnl_Email.Size = New System.Drawing.Size(154, 15)
        Me.Pnl_Email.TabIndex = 2
        '
        'lblPD_Email
        '
        Me.lblPD_Email.AutoEllipsis = true
        Me.lblPD_Email.BackColor = System.Drawing.Color.Transparent
        Me.lblPD_Email.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblPD_Email.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblPD_Email.ForeColor = System.Drawing.Color.FromArgb(CType(CType(21,Byte),Integer), CType(CType(66,Byte),Integer), CType(CType(139,Byte),Integer))
        Me.lblPD_Email.Location = New System.Drawing.Point(91, 0)
        Me.lblPD_Email.Name = "lblPD_Email"
        Me.lblPD_Email.Size = New System.Drawing.Size(63, 15)
        Me.lblPD_Email.TabIndex = 137
        Me.lblPD_Email.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label7.ForeColor = System.Drawing.Color.FromArgb(CType(CType(21,Byte),Integer), CType(CType(66,Byte),Integer), CType(CType(139,Byte),Integer))
        Me.Label7.Location = New System.Drawing.Point(0, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(91, 15)
        Me.Label7.TabIndex = 128
        Me.Label7.Text = "Email :"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Pnl_Fax
        '
        Me.Pnl_Fax.Controls.Add(Me.lblPD_Fax)
        Me.Pnl_Fax.Controls.Add(Me.label23)
        Me.Pnl_Fax.Dock = System.Windows.Forms.DockStyle.Top
        Me.Pnl_Fax.Location = New System.Drawing.Point(0, 67)
        Me.Pnl_Fax.Name = "Pnl_Fax"
        Me.Pnl_Fax.Size = New System.Drawing.Size(154, 15)
        Me.Pnl_Fax.TabIndex = 2
        '
        'lblPD_Fax
        '
        Me.lblPD_Fax.AutoEllipsis = true
        Me.lblPD_Fax.BackColor = System.Drawing.Color.Transparent
        Me.lblPD_Fax.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblPD_Fax.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblPD_Fax.ForeColor = System.Drawing.Color.FromArgb(CType(CType(21,Byte),Integer), CType(CType(66,Byte),Integer), CType(CType(139,Byte),Integer))
        Me.lblPD_Fax.Location = New System.Drawing.Point(91, 0)
        Me.lblPD_Fax.Name = "lblPD_Fax"
        Me.lblPD_Fax.Size = New System.Drawing.Size(63, 15)
        Me.lblPD_Fax.TabIndex = 136
        Me.lblPD_Fax.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'label23
        '
        Me.label23.BackColor = System.Drawing.Color.Transparent
        Me.label23.Dock = System.Windows.Forms.DockStyle.Left
        Me.label23.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.label23.ForeColor = System.Drawing.Color.FromArgb(CType(CType(21,Byte),Integer), CType(CType(66,Byte),Integer), CType(CType(139,Byte),Integer))
        Me.label23.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.label23.Location = New System.Drawing.Point(0, 0)
        Me.label23.Name = "label23"
        Me.label23.Size = New System.Drawing.Size(91, 15)
        Me.label23.TabIndex = 129
        Me.label23.Text = "Fax :"
        Me.label23.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Pnl_Home
        '
        Me.Pnl_Home.Controls.Add(Me.lblPD_HomePhone)
        Me.Pnl_Home.Controls.Add(Me.Label9)
        Me.Pnl_Home.Dock = System.Windows.Forms.DockStyle.Top
        Me.Pnl_Home.Location = New System.Drawing.Point(0, 52)
        Me.Pnl_Home.Name = "Pnl_Home"
        Me.Pnl_Home.Size = New System.Drawing.Size(154, 15)
        Me.Pnl_Home.TabIndex = 3
        '
        'lblPD_HomePhone
        '
        Me.lblPD_HomePhone.AutoEllipsis = true
        Me.lblPD_HomePhone.BackColor = System.Drawing.Color.Transparent
        Me.lblPD_HomePhone.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblPD_HomePhone.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblPD_HomePhone.ForeColor = System.Drawing.Color.FromArgb(CType(CType(21,Byte),Integer), CType(CType(66,Byte),Integer), CType(CType(139,Byte),Integer))
        Me.lblPD_HomePhone.Location = New System.Drawing.Point(91, 0)
        Me.lblPD_HomePhone.Name = "lblPD_HomePhone"
        Me.lblPD_HomePhone.Size = New System.Drawing.Size(63, 15)
        Me.lblPD_HomePhone.TabIndex = 135
        Me.lblPD_HomePhone.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label9
        '
        Me.Label9.AutoEllipsis = true
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label9.ForeColor = System.Drawing.Color.FromArgb(CType(CType(21,Byte),Integer), CType(CType(66,Byte),Integer), CType(CType(139,Byte),Integer))
        Me.Label9.Location = New System.Drawing.Point(0, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(91, 15)
        Me.Label9.TabIndex = 126
        Me.Label9.Text = "Home :"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Pnl_Mobile
        '
        Me.Pnl_Mobile.Controls.Add(Me.lblPD_Mobile)
        Me.Pnl_Mobile.Controls.Add(Me.Label8)
        Me.Pnl_Mobile.Dock = System.Windows.Forms.DockStyle.Top
        Me.Pnl_Mobile.Location = New System.Drawing.Point(0, 37)
        Me.Pnl_Mobile.Name = "Pnl_Mobile"
        Me.Pnl_Mobile.Size = New System.Drawing.Size(154, 15)
        Me.Pnl_Mobile.TabIndex = 2
        '
        'lblPD_Mobile
        '
        Me.lblPD_Mobile.AutoEllipsis = true
        Me.lblPD_Mobile.BackColor = System.Drawing.Color.Transparent
        Me.lblPD_Mobile.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblPD_Mobile.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblPD_Mobile.ForeColor = System.Drawing.Color.FromArgb(CType(CType(21,Byte),Integer), CType(CType(66,Byte),Integer), CType(CType(139,Byte),Integer))
        Me.lblPD_Mobile.Location = New System.Drawing.Point(91, 0)
        Me.lblPD_Mobile.Name = "lblPD_Mobile"
        Me.lblPD_Mobile.Size = New System.Drawing.Size(63, 15)
        Me.lblPD_Mobile.TabIndex = 134
        Me.lblPD_Mobile.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label8.ForeColor = System.Drawing.Color.FromArgb(CType(CType(21,Byte),Integer), CType(CType(66,Byte),Integer), CType(CType(139,Byte),Integer))
        Me.Label8.Location = New System.Drawing.Point(0, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(91, 15)
        Me.Label8.TabIndex = 127
        Me.Label8.Text = "Mobile :"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Pnl_Gender
        '
        Me.Pnl_Gender.Controls.Add(Me.lblPD_Gender)
        Me.Pnl_Gender.Controls.Add(Me.Label46)
        Me.Pnl_Gender.Dock = System.Windows.Forms.DockStyle.Top
        Me.Pnl_Gender.Location = New System.Drawing.Point(0, 22)
        Me.Pnl_Gender.Name = "Pnl_Gender"
        Me.Pnl_Gender.Size = New System.Drawing.Size(154, 15)
        Me.Pnl_Gender.TabIndex = 1
        '
        'lblPD_Gender
        '
        Me.lblPD_Gender.AutoEllipsis = true
        Me.lblPD_Gender.BackColor = System.Drawing.Color.Transparent
        Me.lblPD_Gender.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblPD_Gender.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblPD_Gender.ForeColor = System.Drawing.Color.FromArgb(CType(CType(21,Byte),Integer), CType(CType(66,Byte),Integer), CType(CType(139,Byte),Integer))
        Me.lblPD_Gender.Location = New System.Drawing.Point(91, 0)
        Me.lblPD_Gender.Name = "lblPD_Gender"
        Me.lblPD_Gender.Size = New System.Drawing.Size(63, 15)
        Me.lblPD_Gender.TabIndex = 147
        Me.lblPD_Gender.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label46
        '
        Me.Label46.BackColor = System.Drawing.Color.Transparent
        Me.Label46.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label46.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label46.ForeColor = System.Drawing.Color.FromArgb(CType(CType(21,Byte),Integer), CType(CType(66,Byte),Integer), CType(CType(139,Byte),Integer))
        Me.Label46.Location = New System.Drawing.Point(0, 0)
        Me.Label46.Name = "Label46"
        Me.Label46.Size = New System.Drawing.Size(91, 15)
        Me.Label46.TabIndex = 146
        Me.Label46.Text = "Gender :"
        Me.Label46.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'pnlAddress
        '
        Me.pnlAddress.Controls.Add(Me.lblPD_Address)
        Me.pnlAddress.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlAddress.Location = New System.Drawing.Point(0, 8)
        Me.pnlAddress.Name = "pnlAddress"
        Me.pnlAddress.Size = New System.Drawing.Size(154, 14)
        Me.pnlAddress.TabIndex = 0
        '
        'lblPD_Address
        '
        Me.lblPD_Address.AutoEllipsis = true
        Me.lblPD_Address.BackColor = System.Drawing.Color.Transparent
        Me.lblPD_Address.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblPD_Address.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblPD_Address.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.lblPD_Address.Location = New System.Drawing.Point(0, 0)
        Me.lblPD_Address.Name = "lblPD_Address"
        Me.lblPD_Address.Size = New System.Drawing.Size(154, 14)
        Me.lblPD_Address.TabIndex = 133
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.White
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(21,Byte),Integer), CType(CType(66,Byte),Integer), CType(CType(139,Byte),Integer))
        Me.Label5.Location = New System.Drawing.Point(0, 8)
        Me.Label5.MaximumSize = New System.Drawing.Size(281, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(154, 0)
        Me.Label5.TabIndex = 155
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'pnlLeft
        '
        Me.pnlLeft.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Gradient
        Me.pnlLeft.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlLeft.Controls.Add(Me.picConfidentialInfo)
        Me.pnlLeft.Controls.Add(Me.pnlReconciliationAlert)
        Me.pnlLeft.Controls.Add(Me.pnlPatientSavings)
        Me.pnlLeft.Controls.Add(Me.lblBadDebt)
        Me.pnlLeft.Controls.Add(Me.lnklblAmendmentsAlert)
        Me.pnlLeft.Controls.Add(Me.lblPD_Age)
        Me.pnlLeft.Controls.Add(Me.lblPD_DateOfBirth)
        Me.pnlLeft.Controls.Add(Me.lblPD_Name)
        Me.pnlLeft.Controls.Add(Me.lblPD_Code)
        Me.pnlLeft.Controls.Add(Me.pnlPatientPhoto)
        Me.pnlLeft.Controls.Add(Me.Panel1)
        Me.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlLeft.Location = New System.Drawing.Point(0, 0)
        Me.pnlLeft.Name = "pnlLeft"
        Me.pnlLeft.Size = New System.Drawing.Size(200, 304)
        Me.pnlLeft.TabIndex = 152
        '
        'picConfidentialInfo
        '
        Me.picConfidentialInfo.BackColor = System.Drawing.Color.Transparent
        Me.picConfidentialInfo.Dock = System.Windows.Forms.DockStyle.Top
        Me.picConfidentialInfo.Image = CType(resources.GetObject("picConfidentialInfo.Image"),System.Drawing.Image)
        Me.picConfidentialInfo.Location = New System.Drawing.Point(7, 252)
        Me.picConfidentialInfo.Name = "picConfidentialInfo"
        Me.picConfidentialInfo.Size = New System.Drawing.Size(193, 23)
        Me.picConfidentialInfo.TabIndex = 148
        Me.picConfidentialInfo.TabStop = false
        Me.picConfidentialInfo.Visible = false
        '
        'pnlReconciliationAlert
        '
        Me.pnlReconciliationAlert.BackColor = System.Drawing.Color.Transparent
        Me.pnlReconciliationAlert.Controls.Add(Me.PicReconciliationAlert)
        Me.pnlReconciliationAlert.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlReconciliationAlert.Location = New System.Drawing.Point(7, 226)
        Me.pnlReconciliationAlert.Name = "pnlReconciliationAlert"
        Me.pnlReconciliationAlert.Size = New System.Drawing.Size(193, 26)
        Me.pnlReconciliationAlert.TabIndex = 153
        Me.pnlReconciliationAlert.Visible = false
        '
        'PicReconciliationAlert
        '
        Me.PicReconciliationAlert.BackColor = System.Drawing.Color.Transparent
        Me.PicReconciliationAlert.Dock = System.Windows.Forms.DockStyle.Left
        Me.PicReconciliationAlert.Image = CType(resources.GetObject("PicReconciliationAlert.Image"),System.Drawing.Image)
        Me.PicReconciliationAlert.Location = New System.Drawing.Point(0, 0)
        Me.PicReconciliationAlert.Name = "PicReconciliationAlert"
        Me.PicReconciliationAlert.Size = New System.Drawing.Size(120, 26)
        Me.PicReconciliationAlert.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PicReconciliationAlert.TabIndex = 152
        Me.PicReconciliationAlert.TabStop = false
        '
        'pnlPatientSavings
        '
        Me.pnlPatientSavings.BackColor = System.Drawing.Color.Transparent
        Me.pnlPatientSavings.Controls.Add(Me.lblPatientSavings)
        Me.pnlPatientSavings.Controls.Add(Me.picPatientSavings)
        Me.pnlPatientSavings.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlPatientSavings.Location = New System.Drawing.Point(7, 201)
        Me.pnlPatientSavings.Name = "pnlPatientSavings"
        Me.pnlPatientSavings.Size = New System.Drawing.Size(193, 25)
        Me.pnlPatientSavings.TabIndex = 156
        '
        'lblPatientSavings
        '
        Me.lblPatientSavings.BackColor = System.Drawing.Color.Transparent
        Me.lblPatientSavings.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblPatientSavings.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblPatientSavings.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0,Byte),Integer), CType(CType(51,Byte),Integer), CType(CType(255,Byte),Integer))
        Me.lblPatientSavings.Location = New System.Drawing.Point(24, 0)
        Me.lblPatientSavings.Name = "lblPatientSavings"
        Me.lblPatientSavings.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.lblPatientSavings.Size = New System.Drawing.Size(75, 25)
        Me.lblPatientSavings.TabIndex = 153
        Me.lblPatientSavings.Text = "Rx Savings"
        Me.lblPatientSavings.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'picPatientSavings
        '
        Me.picPatientSavings.BackColor = System.Drawing.Color.Transparent
        Me.picPatientSavings.Dock = System.Windows.Forms.DockStyle.Left
        Me.picPatientSavings.Image = CType(resources.GetObject("picPatientSavings.Image"),System.Drawing.Image)
        Me.picPatientSavings.Location = New System.Drawing.Point(0, 0)
        Me.picPatientSavings.Name = "picPatientSavings"
        Me.picPatientSavings.Size = New System.Drawing.Size(24, 25)
        Me.picPatientSavings.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.picPatientSavings.TabIndex = 152
        Me.picPatientSavings.TabStop = false
        '
        'lblBadDebt
        '
        Me.lblBadDebt.BackColor = System.Drawing.Color.Transparent
        Me.lblBadDebt.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblBadDebt.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblBadDebt.ForeColor = System.Drawing.Color.Red
        Me.lblBadDebt.Location = New System.Drawing.Point(7, 188)
        Me.lblBadDebt.Name = "lblBadDebt"
        Me.lblBadDebt.Size = New System.Drawing.Size(193, 13)
        Me.lblBadDebt.TabIndex = 157
        Me.lblBadDebt.Text = "BAD DEBT"
        '
        'lnklblAmendmentsAlert
        '
        Me.lnklblAmendmentsAlert.AutoEllipsis = true
        Me.lnklblAmendmentsAlert.AutoSize = true
        Me.lnklblAmendmentsAlert.BackColor = System.Drawing.Color.Transparent
        Me.lnklblAmendmentsAlert.DisabledLinkColor = System.Drawing.Color.FromArgb(CType(CType(192,Byte),Integer), CType(CType(64,Byte),Integer), CType(CType(0,Byte),Integer))
        Me.lnklblAmendmentsAlert.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lnklblAmendmentsAlert.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192,Byte),Integer), CType(CType(64,Byte),Integer), CType(CType(0,Byte),Integer))
        Me.lnklblAmendmentsAlert.LinkColor = System.Drawing.Color.FromArgb(CType(CType(192,Byte),Integer), CType(CType(64,Byte),Integer), CType(CType(0,Byte),Integer))
        Me.lnklblAmendmentsAlert.Location = New System.Drawing.Point(7, 275)
        Me.lnklblAmendmentsAlert.Name = "lnklblAmendmentsAlert"
        Me.lnklblAmendmentsAlert.Padding = New System.Windows.Forms.Padding(2, 8, 0, 8)
        Me.lnklblAmendmentsAlert.Size = New System.Drawing.Size(153, 29)
        Me.lnklblAmendmentsAlert.TabIndex = 155
        Me.lnklblAmendmentsAlert.TabStop = true
        Me.lnklblAmendmentsAlert.Text = "Amendments (0 pending)"
        Me.lnklblAmendmentsAlert.Visible = false
        Me.lnklblAmendmentsAlert.VisitedLinkColor = System.Drawing.Color.FromArgb(CType(CType(192,Byte),Integer), CType(CType(64,Byte),Integer), CType(CType(0,Byte),Integer))
        '
        'lblPD_Age
        '
        Me.lblPD_Age.BackColor = System.Drawing.Color.Transparent
        Me.lblPD_Age.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblPD_Age.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblPD_Age.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.lblPD_Age.Location = New System.Drawing.Point(7, 175)
        Me.lblPD_Age.Name = "lblPD_Age"
        Me.lblPD_Age.Size = New System.Drawing.Size(193, 13)
        Me.lblPD_Age.TabIndex = 149
        Me.lblPD_Age.Text = "age"
        '
        'lblPD_DateOfBirth
        '
        Me.lblPD_DateOfBirth.BackColor = System.Drawing.Color.Transparent
        Me.lblPD_DateOfBirth.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblPD_DateOfBirth.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblPD_DateOfBirth.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.lblPD_DateOfBirth.Location = New System.Drawing.Point(7, 162)
        Me.lblPD_DateOfBirth.Name = "lblPD_DateOfBirth"
        Me.lblPD_DateOfBirth.Size = New System.Drawing.Size(193, 13)
        Me.lblPD_DateOfBirth.TabIndex = 138
        Me.lblPD_DateOfBirth.Text = "dateofbirth"
        '
        'lblPD_Name
        '
        Me.lblPD_Name.AutoSize = true
        Me.lblPD_Name.BackColor = System.Drawing.Color.Transparent
        Me.lblPD_Name.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblPD_Name.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblPD_Name.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.lblPD_Name.Location = New System.Drawing.Point(7, 149)
        Me.lblPD_Name.MaximumSize = New System.Drawing.Size(169, 0)
        Me.lblPD_Name.Name = "lblPD_Name"
        Me.lblPD_Name.Size = New System.Drawing.Size(150, 13)
        Me.lblPD_Name.TabIndex = 140
        Me.lblPD_Name.Text = "WWWWWWWWWWWWW"
        '
        'lblPD_Code
        '
        Me.lblPD_Code.AutoSize = true
        Me.lblPD_Code.BackColor = System.Drawing.Color.Transparent
        Me.lblPD_Code.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblPD_Code.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblPD_Code.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.lblPD_Code.Location = New System.Drawing.Point(7, 136)
        Me.lblPD_Code.MaximumSize = New System.Drawing.Size(169, 0)
        Me.lblPD_Code.Name = "lblPD_Code"
        Me.lblPD_Code.Size = New System.Drawing.Size(34, 13)
        Me.lblPD_Code.TabIndex = 139
        Me.lblPD_Code.Text = "code"
        '
        'pnlPatientPhoto
        '
        Me.pnlPatientPhoto.BackColor = System.Drawing.Color.Transparent
        Me.pnlPatientPhoto.Controls.Add(Me.btnIntuitMsg)
        Me.pnlPatientPhoto.Controls.Add(Me.pnlPhotoBorder)
        Me.pnlPatientPhoto.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlPatientPhoto.Location = New System.Drawing.Point(7, 0)
        Me.pnlPatientPhoto.Name = "pnlPatientPhoto"
        Me.pnlPatientPhoto.Size = New System.Drawing.Size(193, 136)
        Me.pnlPatientPhoto.TabIndex = 150
        '
        'pnlPhotoBorder
        '
        Me.pnlPhotoBorder.BackColor = System.Drawing.Color.Transparent
        Me.pnlPhotoBorder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlPhotoBorder.Controls.Add(Me.picPD_Photo)
        Me.pnlPhotoBorder.Location = New System.Drawing.Point(2, 7)
        Me.pnlPhotoBorder.Name = "pnlPhotoBorder"
        Me.pnlPhotoBorder.Size = New System.Drawing.Size(114, 126)
        Me.pnlPhotoBorder.TabIndex = 145
        '
        'picPD_Photo
        '
        Me.picPD_Photo.BackColor = System.Drawing.Color.Gainsboro
        Me.picPD_Photo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.picPD_Photo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.picPD_Photo.Location = New System.Drawing.Point(2, 2)
        Me.picPD_Photo.Name = "picPD_Photo"
        Me.picPD_Photo.Size = New System.Drawing.Size(108, 120)
        Me.picPD_Photo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.picPD_Photo.TabIndex = 125
        Me.picPD_Photo.TabStop = false
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Transparent
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(7, 304)
        Me.Panel1.TabIndex = 151
        '
        'pnlPatientCard
        '
        Me.pnlPatientCard.CaptionFormatStyle.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.pnlPatientCard.CaptionStyle = Janus.Windows.UI.Dock.PanelCaptionStyle.Dark
        Me.pnlPatientCard.CloseButtonVisible = Janus.Windows.UI.InheritableBoolean.[False]
        Me.pnlPatientCard.FloatingLocation = New System.Drawing.Point(542, 579)
        Me.pnlPatientCard.Icon = CType(resources.GetObject("pnlPatientCard.Icon"),System.Drawing.Icon)
        Me.pnlPatientCard.InnerContainer = Me.pnlPatientCardContainer
        Me.pnlPatientCard.InnerContainerFormatStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(250,Byte),Integer), CType(CType(251,Byte),Integer), CType(CType(254,Byte),Integer))
        Me.pnlPatientCard.InnerContainerFormatStyle.BackColorGradient = System.Drawing.Color.FromArgb(CType(CType(227,Byte),Integer), CType(CType(241,Byte),Integer), CType(CType(255,Byte),Integer))
        Me.pnlPatientCard.InnerContainerFormatStyle.BackgroundGradientMode = Janus.Windows.UI.BackgroundGradientMode.Vertical
        Me.pnlPatientCard.Location = New System.Drawing.Point(377, 4)
        Me.pnlPatientCard.Name = "pnlPatientCard"
        Me.pnlPatientCard.Size = New System.Drawing.Size(322, 328)
        Me.pnlPatientCard.TabIndex = 4
        Me.pnlPatientCard.Text = "Patient Cards"
        '
        'pnlPatientCardContainer
        '
        Me.pnlPatientCardContainer.AutoScroll = true
        Me.pnlPatientCardContainer.Controls.Add(Me.pnlMainPatientCardButton)
        Me.pnlPatientCardContainer.Controls.Add(Me.pnlYESLAB)
        Me.pnlPatientCardContainer.Location = New System.Drawing.Point(1, 23)
        Me.pnlPatientCardContainer.Name = "pnlPatientCardContainer"
        Me.pnlPatientCardContainer.Size = New System.Drawing.Size(320, 304)
        Me.pnlPatientCardContainer.TabIndex = 0
        '
        'pnlMainPatientCardButton
        '
        Me.pnlMainPatientCardButton.BackColor = System.Drawing.Color.Transparent
        Me.pnlMainPatientCardButton.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Gradient
        Me.pnlMainPatientCardButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlMainPatientCardButton.Controls.Add(Me.pnlPatientCardButtonContainer)
        Me.pnlMainPatientCardButton.Controls.Add(Me.Label71)
        Me.pnlMainPatientCardButton.Controls.Add(Me.Label72)
        Me.pnlMainPatientCardButton.Controls.Add(Me.Label73)
        Me.pnlMainPatientCardButton.Controls.Add(Me.Label74)
        Me.pnlMainPatientCardButton.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMainPatientCardButton.Location = New System.Drawing.Point(0, 0)
        Me.pnlMainPatientCardButton.Name = "pnlMainPatientCardButton"
        Me.pnlMainPatientCardButton.Size = New System.Drawing.Size(320, 304)
        Me.pnlMainPatientCardButton.TabIndex = 124
        '
        'pnlPatientCardButtonContainer
        '
        Me.pnlPatientCardButtonContainer.AutoScroll = true
        Me.pnlPatientCardButtonContainer.BackColor = System.Drawing.Color.Transparent
        Me.pnlPatientCardButtonContainer.Controls.Add(Me.Panel8)
        Me.pnlPatientCardButtonContainer.Controls.Add(Me.pnlPatientCardsButton)
        Me.pnlPatientCardButtonContainer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlPatientCardButtonContainer.Location = New System.Drawing.Point(16, 8)
        Me.pnlPatientCardButtonContainer.Name = "pnlPatientCardButtonContainer"
        Me.pnlPatientCardButtonContainer.Size = New System.Drawing.Size(296, 296)
        Me.pnlPatientCardButtonContainer.TabIndex = 116
        '
        'Panel8
        '
        Me.Panel8.Controls.Add(Me.picPC_Cards)
        Me.Panel8.Controls.Add(Me.Label13)
        Me.Panel8.Controls.Add(Me.Label41)
        Me.Panel8.Controls.Add(Me.Panel7)
        Me.Panel8.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel8.Location = New System.Drawing.Point(0, 0)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(262, 296)
        Me.Panel8.TabIndex = 127
        '
        'picPC_Cards
        '
        Me.picPC_Cards.BackgroundImage = CType(resources.GetObject("picPC_Cards.BackgroundImage"),System.Drawing.Image)
        Me.picPC_Cards.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.picPC_Cards.Dock = System.Windows.Forms.DockStyle.Fill
        Me.picPC_Cards.Location = New System.Drawing.Point(18, 0)
        Me.picPC_Cards.Name = "picPC_Cards"
        Me.picPC_Cards.Size = New System.Drawing.Size(244, 274)
        Me.picPC_Cards.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picPC_Cards.TabIndex = 113
        Me.picPC_Cards.TabStop = false
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.Transparent
        Me.Label13.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label13.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label13.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label13.Location = New System.Drawing.Point(8, 0)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(10, 274)
        Me.Label13.TabIndex = 120
        '
        'Label41
        '
        Me.Label41.BackColor = System.Drawing.Color.Transparent
        Me.Label41.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label41.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label41.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label41.Location = New System.Drawing.Point(0, 0)
        Me.Label41.Name = "Label41"
        Me.Label41.Size = New System.Drawing.Size(8, 274)
        Me.Label41.TabIndex = 121
        '
        'Panel7
        '
        Me.Panel7.BackColor = System.Drawing.Color.Transparent
        Me.Panel7.Controls.Add(Me.lblScandate)
        Me.Panel7.Controls.Add(Me.Label63)
        Me.Panel7.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel7.Location = New System.Drawing.Point(0, 274)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(262, 22)
        Me.Panel7.TabIndex = 126
        '
        'lblScandate
        '
        Me.lblScandate.BackColor = System.Drawing.Color.Transparent
        Me.lblScandate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblScandate.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblScandate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(21,Byte),Integer), CType(CType(66,Byte),Integer), CType(CType(139,Byte),Integer))
        Me.lblScandate.Location = New System.Drawing.Point(0, 0)
        Me.lblScandate.Name = "lblScandate"
        Me.lblScandate.Size = New System.Drawing.Size(244, 22)
        Me.lblScandate.TabIndex = 128
        Me.lblScandate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label63
        '
        Me.Label63.BackColor = System.Drawing.Color.Transparent
        Me.Label63.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label63.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label63.ForeColor = System.Drawing.Color.FromArgb(CType(CType(21,Byte),Integer), CType(CType(66,Byte),Integer), CType(CType(139,Byte),Integer))
        Me.Label63.Location = New System.Drawing.Point(244, 0)
        Me.Label63.Name = "Label63"
        Me.Label63.Size = New System.Drawing.Size(18, 22)
        Me.Label63.TabIndex = 129
        Me.Label63.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'pnlPatientCardsButton
        '
        Me.pnlPatientCardsButton.BackColor = System.Drawing.Color.Transparent
        Me.pnlPatientCardsButton.Controls.Add(Me.btnPC_MoveFirst)
        Me.pnlPatientCardsButton.Controls.Add(Me.Label14)
        Me.pnlPatientCardsButton.Controls.Add(Me.btnPC_MovePrevious)
        Me.pnlPatientCardsButton.Controls.Add(Me.label20)
        Me.pnlPatientCardsButton.Controls.Add(Me.btnPC_MoveNext)
        Me.pnlPatientCardsButton.Controls.Add(Me.label21)
        Me.pnlPatientCardsButton.Controls.Add(Me.btnPC_MoveLast)
        Me.pnlPatientCardsButton.Controls.Add(Me.label24)
        Me.pnlPatientCardsButton.Controls.Add(Me.btnPC_PrintCards)
        Me.pnlPatientCardsButton.Controls.Add(Me.label22)
        Me.pnlPatientCardsButton.Controls.Add(Me.btnPC_DeleteCard)
        Me.pnlPatientCardsButton.Controls.Add(Me.Label55)
        Me.pnlPatientCardsButton.Controls.Add(Me.btnPC_ScanCard)
        Me.pnlPatientCardsButton.Controls.Add(Me.Label15)
        Me.pnlPatientCardsButton.Controls.Add(Me.Label16)
        Me.pnlPatientCardsButton.Controls.Add(Me.Label11)
        Me.pnlPatientCardsButton.Dock = System.Windows.Forms.DockStyle.Right
        Me.pnlPatientCardsButton.Location = New System.Drawing.Point(262, 0)
        Me.pnlPatientCardsButton.Name = "pnlPatientCardsButton"
        Me.pnlPatientCardsButton.Size = New System.Drawing.Size(34, 296)
        Me.pnlPatientCardsButton.TabIndex = 115
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.Transparent
        Me.Label14.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label14.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label14.ForeColor = System.Drawing.Color.Black
        Me.Label14.Location = New System.Drawing.Point(0, 177)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(34, 5)
        Me.Label14.TabIndex = 108
        '
        'label20
        '
        Me.label20.BackColor = System.Drawing.Color.Transparent
        Me.label20.Dock = System.Windows.Forms.DockStyle.Top
        Me.label20.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.label20.ForeColor = System.Drawing.Color.Black
        Me.label20.Location = New System.Drawing.Point(0, 149)
        Me.label20.Name = "label20"
        Me.label20.Size = New System.Drawing.Size(34, 5)
        Me.label20.TabIndex = 110
        '
        'label21
        '
        Me.label21.BackColor = System.Drawing.Color.Transparent
        Me.label21.Dock = System.Windows.Forms.DockStyle.Top
        Me.label21.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.label21.ForeColor = System.Drawing.Color.Black
        Me.label21.Location = New System.Drawing.Point(0, 121)
        Me.label21.Name = "label21"
        Me.label21.Size = New System.Drawing.Size(34, 5)
        Me.label21.TabIndex = 112
        '
        'label24
        '
        Me.label24.BackColor = System.Drawing.Color.Transparent
        Me.label24.Dock = System.Windows.Forms.DockStyle.Top
        Me.label24.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.label24.ForeColor = System.Drawing.Color.Black
        Me.label24.Location = New System.Drawing.Point(0, 93)
        Me.label24.Name = "label24"
        Me.label24.Size = New System.Drawing.Size(34, 5)
        Me.label24.TabIndex = 118
        '
        'label22
        '
        Me.label22.BackColor = System.Drawing.Color.Transparent
        Me.label22.Dock = System.Windows.Forms.DockStyle.Top
        Me.label22.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.label22.ForeColor = System.Drawing.Color.Black
        Me.label22.Location = New System.Drawing.Point(0, 65)
        Me.label22.Name = "label22"
        Me.label22.Size = New System.Drawing.Size(34, 5)
        Me.label22.TabIndex = 115
        '
        'Label55
        '
        Me.Label55.BackColor = System.Drawing.Color.Transparent
        Me.Label55.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label55.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label55.ForeColor = System.Drawing.Color.Black
        Me.Label55.Location = New System.Drawing.Point(0, 37)
        Me.Label55.Name = "Label55"
        Me.Label55.Size = New System.Drawing.Size(34, 5)
        Me.Label55.TabIndex = 121
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.Transparent
        Me.Label15.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label15.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label15.ForeColor = System.Drawing.Color.Black
        Me.Label15.Location = New System.Drawing.Point(0, 294)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(34, 2)
        Me.Label15.TabIndex = 100
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.Transparent
        Me.Label16.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label16.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label16.ForeColor = System.Drawing.Color.Black
        Me.Label16.Location = New System.Drawing.Point(0, 10)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(34, 2)
        Me.Label16.TabIndex = 99
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label11.ForeColor = System.Drawing.Color.Black
        Me.Label11.Location = New System.Drawing.Point(0, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(34, 10)
        Me.Label11.TabIndex = 119
        '
        'Label71
        '
        Me.Label71.BackColor = System.Drawing.Color.Transparent
        Me.Label71.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label71.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label71.ForeColor = System.Drawing.Color.Black
        Me.Label71.Location = New System.Drawing.Point(312, 8)
        Me.Label71.Name = "Label71"
        Me.Label71.Size = New System.Drawing.Size(8, 296)
        Me.Label71.TabIndex = 110
        '
        'Label72
        '
        Me.Label72.BackColor = System.Drawing.Color.Transparent
        Me.Label72.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label72.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label72.ForeColor = System.Drawing.Color.Black
        Me.Label72.Location = New System.Drawing.Point(8, 8)
        Me.Label72.Name = "Label72"
        Me.Label72.Size = New System.Drawing.Size(8, 296)
        Me.Label72.TabIndex = 114
        '
        'Label73
        '
        Me.Label73.BackColor = System.Drawing.Color.Transparent
        Me.Label73.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label73.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label73.ForeColor = System.Drawing.Color.Black
        Me.Label73.Location = New System.Drawing.Point(8, 0)
        Me.Label73.Name = "Label73"
        Me.Label73.Size = New System.Drawing.Size(312, 8)
        Me.Label73.TabIndex = 112
        '
        'Label74
        '
        Me.Label74.BackColor = System.Drawing.Color.Transparent
        Me.Label74.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label74.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label74.ForeColor = System.Drawing.Color.Black
        Me.Label74.Location = New System.Drawing.Point(0, 0)
        Me.Label74.Name = "Label74"
        Me.Label74.Size = New System.Drawing.Size(8, 304)
        Me.Label74.TabIndex = 109
        '
        'pnlYESLAB
        '
        Me.pnlYESLAB.BackColor = System.Drawing.Color.Transparent
        Me.pnlYESLAB.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Gradient
        Me.pnlYESLAB.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlYESLAB.Controls.Add(Me.PictureBox3)
        Me.pnlYESLAB.Controls.Add(Me.Panel6)
        Me.pnlYESLAB.Controls.Add(Me.Label17)
        Me.pnlYESLAB.Controls.Add(Me.Label27)
        Me.pnlYESLAB.Controls.Add(Me.Label25)
        Me.pnlYESLAB.Controls.Add(Me.Label28)
        Me.pnlYESLAB.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlYESLAB.Location = New System.Drawing.Point(0, 0)
        Me.pnlYESLAB.Name = "pnlYESLAB"
        Me.pnlYESLAB.Size = New System.Drawing.Size(320, 304)
        Me.pnlYESLAB.TabIndex = 125
        '
        'PictureBox3
        '
        Me.PictureBox3.BackgroundImage = Global.gloEMR.My.Resources.Resources.Green_YES_Lab
        Me.PictureBox3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PictureBox3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PictureBox3.Location = New System.Drawing.Point(18, 8)
        Me.PictureBox3.Name = "PictureBox3"
        Me.PictureBox3.Size = New System.Drawing.Size(270, 288)
        Me.PictureBox3.TabIndex = 122
        Me.PictureBox3.TabStop = false
        '
        'Panel6
        '
        Me.Panel6.Controls.Add(Me.btnYesClose)
        Me.Panel6.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel6.Location = New System.Drawing.Point(288, 8)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(22, 288)
        Me.Panel6.TabIndex = 122
        '
        'btnYesClose
        '
        Me.btnYesClose.BackColor = System.Drawing.Color.Transparent
        Me.btnYesClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnYesClose.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnYesClose.FlatAppearance.BorderSize = 0
        Me.btnYesClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnYesClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnYesClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnYesClose.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btnYesClose.Image = CType(resources.GetObject("btnYesClose.Image"),System.Drawing.Image)
        Me.btnYesClose.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.btnYesClose.Location = New System.Drawing.Point(0, 0)
        Me.btnYesClose.Name = "btnYesClose"
        Me.btnYesClose.Size = New System.Drawing.Size(26, 288)
        Me.btnYesClose.TabIndex = 123
        Me.btnYesClose.UseVisualStyleBackColor = false
        '
        'Label17
        '
        Me.Label17.BackColor = System.Drawing.Color.Transparent
        Me.Label17.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label17.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label17.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label17.Location = New System.Drawing.Point(310, 8)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(10, 288)
        Me.Label17.TabIndex = 124
        '
        'Label27
        '
        Me.Label27.BackColor = System.Drawing.Color.Transparent
        Me.Label27.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label27.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label27.ForeColor = System.Drawing.Color.Black
        Me.Label27.Location = New System.Drawing.Point(0, 8)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(18, 288)
        Me.Label27.TabIndex = 110
        '
        'Label25
        '
        Me.Label25.BackColor = System.Drawing.Color.Transparent
        Me.Label25.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label25.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label25.ForeColor = System.Drawing.Color.Black
        Me.Label25.Location = New System.Drawing.Point(0, 296)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(320, 8)
        Me.Label25.TabIndex = 126
        '
        'Label28
        '
        Me.Label28.BackColor = System.Drawing.Color.Transparent
        Me.Label28.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label28.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label28.ForeColor = System.Drawing.Color.Black
        Me.Label28.Location = New System.Drawing.Point(0, 0)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(320, 8)
        Me.Label28.TabIndex = 125
        '
        'pnlJPatientStatus
        '
        Me.pnlJPatientStatus.CaptionFormatStyle.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.pnlJPatientStatus.CaptionStyle = Janus.Windows.UI.Dock.PanelCaptionStyle.Dark
        Me.pnlJPatientStatus.CloseButtonVisible = Janus.Windows.UI.InheritableBoolean.[False]
        Me.pnlJPatientStatus.Icon = CType(resources.GetObject("pnlJPatientStatus.Icon"),System.Drawing.Icon)
        Me.pnlJPatientStatus.InnerContainer = Me.pnlJPatientStatusContainer
        Me.pnlJPatientStatus.InnerContainerFormatStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(250,Byte),Integer), CType(CType(251,Byte),Integer), CType(CType(254,Byte),Integer))
        Me.pnlJPatientStatus.InnerContainerFormatStyle.BackColorGradient = System.Drawing.Color.FromArgb(CType(CType(227,Byte),Integer), CType(CType(241,Byte),Integer), CType(CType(255,Byte),Integer))
        Me.pnlJPatientStatus.InnerContainerFormatStyle.BackgroundGradientMode = Janus.Windows.UI.BackgroundGradientMode.Vertical
        Me.pnlJPatientStatus.Location = New System.Drawing.Point(703, 4)
        Me.pnlJPatientStatus.Name = "pnlJPatientStatus"
        Me.pnlJPatientStatus.Size = New System.Drawing.Size(254, 328)
        Me.pnlJPatientStatus.TabIndex = 4
        Me.pnlJPatientStatus.Text = "Patient Status"
        '
        'pnlJPatientStatusContainer
        '
        Me.pnlJPatientStatusContainer.AutoScroll = true
        Me.pnlJPatientStatusContainer.Controls.Add(Me.pnlPatientStatus)
        Me.pnlJPatientStatusContainer.Controls.Add(Me.pnlPatientAlert)
        Me.pnlJPatientStatusContainer.Location = New System.Drawing.Point(1, 23)
        Me.pnlJPatientStatusContainer.Name = "pnlJPatientStatusContainer"
        Me.pnlJPatientStatusContainer.Size = New System.Drawing.Size(252, 304)
        Me.pnlJPatientStatusContainer.TabIndex = 0
        '
        'pnlPatientStatus
        '
        Me.pnlPatientStatus.BackColor = System.Drawing.Color.Transparent
        Me.pnlPatientStatus.Controls.Add(Me.pnlPAtientStaturEnvironment)
        Me.pnlPatientStatus.Controls.Add(Me.Label42)
        Me.pnlPatientStatus.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlPatientStatus.Location = New System.Drawing.Point(0, 0)
        Me.pnlPatientStatus.Name = "pnlPatientStatus"
        Me.pnlPatientStatus.Size = New System.Drawing.Size(252, 227)
        Me.pnlPatientStatus.TabIndex = 120
        '
        'pnlPAtientStaturEnvironment
        '
        Me.pnlPAtientStaturEnvironment.Controls.Add(Me.pnlPatientStatusGrid)
        Me.pnlPAtientStaturEnvironment.Controls.Add(Me.pnlPatientStatusColor)
        Me.pnlPAtientStaturEnvironment.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlPAtientStaturEnvironment.Location = New System.Drawing.Point(0, 1)
        Me.pnlPAtientStaturEnvironment.Name = "pnlPAtientStaturEnvironment"
        Me.pnlPAtientStaturEnvironment.Size = New System.Drawing.Size(252, 226)
        Me.pnlPAtientStaturEnvironment.TabIndex = 151
        '
        'pnlPatientStatusGrid
        '
        Me.pnlPatientStatusGrid.Controls.Add(Me.c1PatientStatus)
        Me.pnlPatientStatusGrid.Controls.Add(Me.Label58)
        Me.pnlPatientStatusGrid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlPatientStatusGrid.Location = New System.Drawing.Point(0, 41)
        Me.pnlPatientStatusGrid.Name = "pnlPatientStatusGrid"
        Me.pnlPatientStatusGrid.Size = New System.Drawing.Size(252, 185)
        Me.pnlPatientStatusGrid.TabIndex = 152
        '
        'c1PatientStatus
        '
        Me.c1PatientStatus.BackColor = System.Drawing.Color.FromArgb(CType(CType(240,Byte),Integer), CType(CType(247,Byte),Integer), CType(CType(255,Byte),Integer))
        Me.c1PatientStatus.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.c1PatientStatus.ColumnInfo = "10,0,0,0,0,90,Columns:0{Width:180;}"&Global.Microsoft.VisualBasic.ChrW(9)
        Me.c1PatientStatus.Dock = System.Windows.Forms.DockStyle.Fill
        Me.c1PatientStatus.EditOptions = C1.Win.C1FlexGrid.EditFlags.None
        Me.c1PatientStatus.ExtendLastCol = true
        Me.c1PatientStatus.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.c1PatientStatus.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.c1PatientStatus.Location = New System.Drawing.Point(0, 1)
        Me.c1PatientStatus.Name = "c1PatientStatus"
        Me.c1PatientStatus.Rows.Count = 1
        Me.c1PatientStatus.Rows.DefaultSize = 18
        Me.c1PatientStatus.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.c1PatientStatus.Size = New System.Drawing.Size(252, 184)
        Me.c1PatientStatus.StyleInfo = resources.GetString("c1PatientStatus.StyleInfo")
        Me.c1PatientStatus.TabIndex = 151
        Me.c1PatientStatus.Tree.NodeImageCollapsed = CType(resources.GetObject("c1PatientStatus.Tree.NodeImageCollapsed"),System.Drawing.Image)
        Me.c1PatientStatus.Tree.NodeImageExpanded = CType(resources.GetObject("c1PatientStatus.Tree.NodeImageExpanded"),System.Drawing.Image)
        '
        'Label58
        '
        Me.Label58.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label58.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label58.Location = New System.Drawing.Point(0, 0)
        Me.Label58.Name = "Label58"
        Me.Label58.Size = New System.Drawing.Size(252, 1)
        Me.Label58.TabIndex = 152
        '
        'pnlPatientStatusColor
        '
        Me.pnlPatientStatusColor.AutoScroll = true
        Me.pnlPatientStatusColor.Controls.Add(Me.lblENV_05)
        Me.pnlPatientStatusColor.Controls.Add(Me.lblENV_03)
        Me.pnlPatientStatusColor.Controls.Add(Me.lblENV_02)
        Me.pnlPatientStatusColor.Controls.Add(Me.lblENV_04)
        Me.pnlPatientStatusColor.Controls.Add(Me.lblENV_06)
        Me.pnlPatientStatusColor.Controls.Add(Me.lblENV_01)
        Me.pnlPatientStatusColor.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlPatientStatusColor.Location = New System.Drawing.Point(0, 0)
        Me.pnlPatientStatusColor.Name = "pnlPatientStatusColor"
        Me.pnlPatientStatusColor.Size = New System.Drawing.Size(252, 41)
        Me.pnlPatientStatusColor.TabIndex = 72
        '
        'lblENV_05
        '
        Me.lblENV_05.BackColor = System.Drawing.Color.White
        Me.lblENV_05.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblENV_05.Location = New System.Drawing.Point(167, 11)
        Me.lblENV_05.Name = "lblENV_05"
        Me.lblENV_05.Size = New System.Drawing.Size(31, 20)
        Me.lblENV_05.TabIndex = 149
        '
        'lblENV_03
        '
        Me.lblENV_03.BackColor = System.Drawing.Color.White
        Me.lblENV_03.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblENV_03.Location = New System.Drawing.Point(91, 11)
        Me.lblENV_03.Name = "lblENV_03"
        Me.lblENV_03.Size = New System.Drawing.Size(31, 20)
        Me.lblENV_03.TabIndex = 147
        '
        'lblENV_02
        '
        Me.lblENV_02.BackColor = System.Drawing.Color.White
        Me.lblENV_02.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblENV_02.Location = New System.Drawing.Point(53, 11)
        Me.lblENV_02.Name = "lblENV_02"
        Me.lblENV_02.Size = New System.Drawing.Size(31, 20)
        Me.lblENV_02.TabIndex = 146
        '
        'lblENV_04
        '
        Me.lblENV_04.BackColor = System.Drawing.Color.White
        Me.lblENV_04.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblENV_04.Location = New System.Drawing.Point(129, 11)
        Me.lblENV_04.Name = "lblENV_04"
        Me.lblENV_04.Size = New System.Drawing.Size(31, 20)
        Me.lblENV_04.TabIndex = 148
        '
        'lblENV_06
        '
        Me.lblENV_06.BackColor = System.Drawing.Color.White
        Me.lblENV_06.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblENV_06.Location = New System.Drawing.Point(205, 11)
        Me.lblENV_06.Name = "lblENV_06"
        Me.lblENV_06.Size = New System.Drawing.Size(31, 20)
        Me.lblENV_06.TabIndex = 150
        '
        'lblENV_01
        '
        Me.lblENV_01.BackColor = System.Drawing.Color.White
        Me.lblENV_01.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblENV_01.Location = New System.Drawing.Point(15, 11)
        Me.lblENV_01.Name = "lblENV_01"
        Me.lblENV_01.Size = New System.Drawing.Size(31, 20)
        Me.lblENV_01.TabIndex = 145
        '
        'Label42
        '
        Me.Label42.BackColor = System.Drawing.Color.FromArgb(CType(CType(101,Byte),Integer), CType(CType(147,Byte),Integer), CType(CType(207,Byte),Integer))
        Me.Label42.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label42.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label42.ForeColor = System.Drawing.Color.Black
        Me.Label42.Location = New System.Drawing.Point(0, 0)
        Me.Label42.Name = "Label42"
        Me.Label42.Size = New System.Drawing.Size(252, 1)
        Me.Label42.TabIndex = 131
        '
        'pnlPatientAlert
        '
        Me.pnlPatientAlert.BackColor = System.Drawing.Color.Transparent
        Me.pnlPatientAlert.Controls.Add(Me.txtPatientAlert)
        Me.pnlPatientAlert.Controls.Add(Me.Label43)
        Me.pnlPatientAlert.Controls.Add(Me.pnlPatientAlertHeader)
        Me.pnlPatientAlert.Controls.Add(Me.Label10)
        Me.pnlPatientAlert.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlPatientAlert.Location = New System.Drawing.Point(0, 227)
        Me.pnlPatientAlert.Name = "pnlPatientAlert"
        Me.pnlPatientAlert.Size = New System.Drawing.Size(252, 77)
        Me.pnlPatientAlert.TabIndex = 119
        '
        'txtPatientAlert
        '
        Me.txtPatientAlert.BackColor = System.Drawing.Color.FromArgb(CType(CType(250,Byte),Integer), CType(CType(251,Byte),Integer), CType(CType(254,Byte),Integer))
        Me.txtPatientAlert.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtPatientAlert.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtPatientAlert.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtPatientAlert.ForeColor = System.Drawing.Color.FromArgb(CType(CType(21,Byte),Integer), CType(CType(66,Byte),Integer), CType(CType(139,Byte),Integer))
        Me.txtPatientAlert.Location = New System.Drawing.Point(2, 23)
        Me.txtPatientAlert.Multiline = true
        Me.txtPatientAlert.Name = "txtPatientAlert"
        Me.txtPatientAlert.ReadOnly = true
        Me.txtPatientAlert.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtPatientAlert.Size = New System.Drawing.Size(250, 54)
        Me.txtPatientAlert.TabIndex = 86
        '
        'Label43
        '
        Me.Label43.BackColor = System.Drawing.Color.White
        Me.Label43.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label43.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label43.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label43.Location = New System.Drawing.Point(0, 23)
        Me.Label43.Name = "Label43"
        Me.Label43.Size = New System.Drawing.Size(2, 54)
        Me.Label43.TabIndex = 132
        '
        'pnlPatientAlertHeader
        '
        Me.pnlPatientAlertHeader.BackColor = System.Drawing.Color.Transparent
        Me.pnlPatientAlertHeader.BackgroundImage = CType(resources.GetObject("pnlPatientAlertHeader.BackgroundImage"),System.Drawing.Image)
        Me.pnlPatientAlertHeader.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlPatientAlertHeader.Controls.Add(Me.Label12)
        Me.pnlPatientAlertHeader.Controls.Add(Me.btnModifyPatientAlert)
        Me.pnlPatientAlertHeader.Controls.Add(Me.btnClosePatientAlert)
        Me.pnlPatientAlertHeader.Controls.Add(Me.Label45)
        Me.pnlPatientAlertHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlPatientAlertHeader.Location = New System.Drawing.Point(0, 1)
        Me.pnlPatientAlertHeader.Name = "pnlPatientAlertHeader"
        Me.pnlPatientAlertHeader.Size = New System.Drawing.Size(252, 22)
        Me.pnlPatientAlertHeader.TabIndex = 111
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.Transparent
        Me.Label12.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label12.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Label12.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label12.Location = New System.Drawing.Point(0, 0)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(208, 21)
        Me.Label12.TabIndex = 140
        Me.Label12.Text = " Patient Alert"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label45
        '
        Me.Label45.BackColor = System.Drawing.Color.FromArgb(CType(CType(101,Byte),Integer), CType(CType(147,Byte),Integer), CType(CType(207,Byte),Integer))
        Me.Label45.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label45.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label45.ForeColor = System.Drawing.Color.Black
        Me.Label45.Location = New System.Drawing.Point(0, 21)
        Me.Label45.Name = "Label45"
        Me.Label45.Size = New System.Drawing.Size(252, 1)
        Me.Label45.TabIndex = 141
        Me.Label45.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.FromArgb(CType(CType(101,Byte),Integer), CType(CType(147,Byte),Integer), CType(CType(207,Byte),Integer))
        Me.Label10.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label10.ForeColor = System.Drawing.Color.Black
        Me.Label10.Location = New System.Drawing.Point(0, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(252, 1)
        Me.Label10.TabIndex = 131
        '
        'uiPanel0
        '
        Me.uiPanel0.Closed = true
        Me.uiPanel0.InnerContainer = Me.uiPanel0Container
        Me.uiPanel0.Location = New System.Drawing.Point(224, 92)
        Me.uiPanel0.Name = "uiPanel0"
        Me.uiPanel0.Size = New System.Drawing.Size(200, 190)
        Me.uiPanel0.TabIndex = 4
        Me.uiPanel0.Text = "Panel 0"
        '
        'uiPanel0Container
        '
        Me.uiPanel0Container.Location = New System.Drawing.Point(0, 0)
        Me.uiPanel0Container.Name = "uiPanel0Container"
        Me.uiPanel0Container.Size = New System.Drawing.Size(200, 190)
        Me.uiPanel0Container.TabIndex = 0
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.White
        Me.Panel4.Controls.Add(Me.pnlEligibilityCheck)
        Me.Panel4.Controls.Add(Me.pnlUnappliedCopay)
        Me.Panel4.Controls.Add(Me.pnlCopayAlert)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel4.Location = New System.Drawing.Point(0, 0)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(269, 145)
        Me.Panel4.TabIndex = 0
        '
        'pnlEligibilityCheck
        '
        Me.pnlEligibilityCheck.Controls.Add(Me.c1EligibilityCheck)
        Me.pnlEligibilityCheck.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlEligibilityCheck.Location = New System.Drawing.Point(0, 50)
        Me.pnlEligibilityCheck.Name = "pnlEligibilityCheck"
        Me.pnlEligibilityCheck.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlEligibilityCheck.Size = New System.Drawing.Size(269, 25)
        Me.pnlEligibilityCheck.TabIndex = 10
        '
        'c1EligibilityCheck
        '
        Me.c1EligibilityCheck.AllowEditing = false
        Me.c1EligibilityCheck.BackColor = System.Drawing.Color.FromArgb(CType(CType(240,Byte),Integer), CType(CType(247,Byte),Integer), CType(CType(255,Byte),Integer))
        Me.c1EligibilityCheck.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.c1EligibilityCheck.ColumnInfo = "0,0,0,0,0,95,Columns:"
        Me.c1EligibilityCheck.Dock = System.Windows.Forms.DockStyle.Fill
        Me.c1EligibilityCheck.EditOptions = C1.Win.C1FlexGrid.EditFlags.None
        Me.c1EligibilityCheck.ExtendLastCol = true
        Me.c1EligibilityCheck.FocusRect = C1.Win.C1FlexGrid.FocusRectEnum.None
        Me.c1EligibilityCheck.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.c1EligibilityCheck.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.c1EligibilityCheck.HighLight = C1.Win.C1FlexGrid.HighLightEnum.Never
        Me.c1EligibilityCheck.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.None
        Me.c1EligibilityCheck.Location = New System.Drawing.Point(3, 3)
        Me.c1EligibilityCheck.Name = "c1EligibilityCheck"
        Me.c1EligibilityCheck.Rows.Count = 0
        Me.c1EligibilityCheck.Rows.DefaultSize = 19
        Me.c1EligibilityCheck.Rows.Fixed = 0
        Me.c1EligibilityCheck.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.c1EligibilityCheck.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Cell
        Me.c1EligibilityCheck.ShowCellLabels = true
        Me.c1EligibilityCheck.Size = New System.Drawing.Size(263, 19)
        Me.c1EligibilityCheck.StyleInfo = resources.GetString("c1EligibilityCheck.StyleInfo")
        Me.c1EligibilityCheck.TabIndex = 5
        Me.c1EligibilityCheck.Tree.NodeImageCollapsed = CType(resources.GetObject("c1EligibilityCheck.Tree.NodeImageCollapsed"),System.Drawing.Image)
        Me.c1EligibilityCheck.Tree.NodeImageExpanded = CType(resources.GetObject("c1EligibilityCheck.Tree.NodeImageExpanded"),System.Drawing.Image)
        '
        'pnlUnappliedCopay
        '
        Me.pnlUnappliedCopay.Controls.Add(Me.c1UnappliedCopayAlert)
        Me.pnlUnappliedCopay.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlUnappliedCopay.Location = New System.Drawing.Point(0, 25)
        Me.pnlUnappliedCopay.Name = "pnlUnappliedCopay"
        Me.pnlUnappliedCopay.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlUnappliedCopay.Size = New System.Drawing.Size(269, 25)
        Me.pnlUnappliedCopay.TabIndex = 12
        '
        'c1UnappliedCopayAlert
        '
        Me.c1UnappliedCopayAlert.AllowEditing = false
        Me.c1UnappliedCopayAlert.BackColor = System.Drawing.Color.FromArgb(CType(CType(240,Byte),Integer), CType(CType(247,Byte),Integer), CType(CType(255,Byte),Integer))
        Me.c1UnappliedCopayAlert.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.c1UnappliedCopayAlert.ColumnInfo = "0,0,0,0,0,95,Columns:"
        Me.c1UnappliedCopayAlert.Dock = System.Windows.Forms.DockStyle.Fill
        Me.c1UnappliedCopayAlert.EditOptions = C1.Win.C1FlexGrid.EditFlags.None
        Me.c1UnappliedCopayAlert.ExtendLastCol = true
        Me.c1UnappliedCopayAlert.FocusRect = C1.Win.C1FlexGrid.FocusRectEnum.None
        Me.c1UnappliedCopayAlert.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.c1UnappliedCopayAlert.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.c1UnappliedCopayAlert.HighLight = C1.Win.C1FlexGrid.HighLightEnum.Never
        Me.c1UnappliedCopayAlert.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.None
        Me.c1UnappliedCopayAlert.Location = New System.Drawing.Point(3, 3)
        Me.c1UnappliedCopayAlert.Name = "c1UnappliedCopayAlert"
        Me.c1UnappliedCopayAlert.Rows.Count = 0
        Me.c1UnappliedCopayAlert.Rows.DefaultSize = 19
        Me.c1UnappliedCopayAlert.Rows.Fixed = 0
        Me.c1UnappliedCopayAlert.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.c1UnappliedCopayAlert.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Cell
        Me.c1UnappliedCopayAlert.ShowCellLabels = true
        Me.c1UnappliedCopayAlert.Size = New System.Drawing.Size(263, 19)
        Me.c1UnappliedCopayAlert.StyleInfo = resources.GetString("c1UnappliedCopayAlert.StyleInfo")
        Me.c1UnappliedCopayAlert.TabIndex = 0
        Me.c1UnappliedCopayAlert.Tree.NodeImageCollapsed = CType(resources.GetObject("c1UnappliedCopayAlert.Tree.NodeImageCollapsed"),System.Drawing.Image)
        Me.c1UnappliedCopayAlert.Tree.NodeImageExpanded = CType(resources.GetObject("c1UnappliedCopayAlert.Tree.NodeImageExpanded"),System.Drawing.Image)
        '
        'pnlCopayAlert
        '
        Me.pnlCopayAlert.Controls.Add(Me.c1CopayAlert)
        Me.pnlCopayAlert.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlCopayAlert.Location = New System.Drawing.Point(0, 0)
        Me.pnlCopayAlert.Name = "pnlCopayAlert"
        Me.pnlCopayAlert.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlCopayAlert.Size = New System.Drawing.Size(269, 25)
        Me.pnlCopayAlert.TabIndex = 11
        '
        'c1CopayAlert
        '
        Me.c1CopayAlert.AllowEditing = false
        Me.c1CopayAlert.BackColor = System.Drawing.Color.FromArgb(CType(CType(240,Byte),Integer), CType(CType(247,Byte),Integer), CType(CType(255,Byte),Integer))
        Me.c1CopayAlert.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.c1CopayAlert.ColumnInfo = "0,0,0,0,0,95,Columns:"
        Me.c1CopayAlert.Dock = System.Windows.Forms.DockStyle.Fill
        Me.c1CopayAlert.EditOptions = C1.Win.C1FlexGrid.EditFlags.None
        Me.c1CopayAlert.ExtendLastCol = true
        Me.c1CopayAlert.FocusRect = C1.Win.C1FlexGrid.FocusRectEnum.None
        Me.c1CopayAlert.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.c1CopayAlert.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.c1CopayAlert.HighLight = C1.Win.C1FlexGrid.HighLightEnum.Never
        Me.c1CopayAlert.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.None
        Me.c1CopayAlert.Location = New System.Drawing.Point(3, 3)
        Me.c1CopayAlert.Name = "c1CopayAlert"
        Me.c1CopayAlert.Rows.Count = 0
        Me.c1CopayAlert.Rows.DefaultSize = 19
        Me.c1CopayAlert.Rows.Fixed = 0
        Me.c1CopayAlert.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.c1CopayAlert.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Cell
        Me.c1CopayAlert.ShowCellLabels = true
        Me.c1CopayAlert.Size = New System.Drawing.Size(263, 19)
        Me.c1CopayAlert.StyleInfo = resources.GetString("c1CopayAlert.StyleInfo")
        Me.c1CopayAlert.TabIndex = 5
        Me.c1CopayAlert.Tree.NodeImageCollapsed = CType(resources.GetObject("c1CopayAlert.Tree.NodeImageCollapsed"),System.Drawing.Image)
        Me.c1CopayAlert.Tree.NodeImageExpanded = CType(resources.GetObject("c1CopayAlert.Tree.NodeImageExpanded"),System.Drawing.Image)
        '
        'c1Mails
        '
        Me.c1Mails.BackColor = System.Drawing.Color.FromArgb(CType(CType(240,Byte),Integer), CType(CType(247,Byte),Integer), CType(CType(255,Byte),Integer))
        Me.c1Mails.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.c1Mails.ColumnInfo = "10,0,0,0,0,90,Columns:"
        Me.c1Mails.Dock = System.Windows.Forms.DockStyle.Fill
        Me.c1Mails.EditOptions = C1.Win.C1FlexGrid.EditFlags.None
        Me.c1Mails.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.c1Mails.Location = New System.Drawing.Point(0, 0)
        Me.c1Mails.Name = "c1Mails"
        Me.c1Mails.Rows.Count = 5
        Me.c1Mails.Rows.DefaultSize = 18
        Me.c1Mails.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.c1Mails.Size = New System.Drawing.Size(219, 462)
        Me.c1Mails.StyleInfo = resources.GetString("c1Mails.StyleInfo")
        Me.c1Mails.TabIndex = 5
        Me.c1Mails.Tree.NodeImageCollapsed = CType(resources.GetObject("c1Mails.Tree.NodeImageCollapsed"),System.Drawing.Image)
        Me.c1Mails.Tree.NodeImageExpanded = CType(resources.GetObject("c1Mails.Tree.NodeImageExpanded"),System.Drawing.Image)
        '
        'pnlPatientDemo
        '
        Me.pnlPatientDemo.AutoHide = true
        Me.pnlPatientDemo.CloseButtonVisible = Janus.Windows.UI.InheritableBoolean.[False]
        Me.pnlPatientDemo.InnerContainer = Me.pnlPatientDemoContainer
        Me.pnlPatientDemo.Location = New System.Drawing.Point(0, 0)
        Me.pnlPatientDemo.Name = "pnlPatientDemo"
        Me.pnlPatientDemo.Size = New System.Drawing.Size(810, 181)
        Me.pnlPatientDemo.TabIndex = 4
        Me.pnlPatientDemo.Text = "Panel 1"
        '
        'pnlPatientDemoContainer
        '
        Me.pnlPatientDemoContainer.Location = New System.Drawing.Point(0, 0)
        Me.pnlPatientDemoContainer.Name = "pnlPatientDemoContainer"
        Me.pnlPatientDemoContainer.Size = New System.Drawing.Size(810, 181)
        Me.pnlPatientDemoContainer.TabIndex = 0
        '
        'pnlPatientCards
        '
        Me.pnlPatientCards.AutoHide = true
        Me.pnlPatientCards.FloatingLocation = New System.Drawing.Point(401, 560)
        Me.pnlPatientCards.InnerContainer = Me.pnlPatientCardsContainer
        Me.pnlPatientCards.Location = New System.Drawing.Point(0, 0)
        Me.pnlPatientCards.Name = "pnlPatientCards"
        Me.pnlPatientCards.Size = New System.Drawing.Size(810, 181)
        Me.pnlPatientCards.TabIndex = 4
        Me.pnlPatientCards.Text = "Patient Cards"
        '
        'pnlPatientCardsContainer
        '
        Me.pnlPatientCardsContainer.Location = New System.Drawing.Point(0, 0)
        Me.pnlPatientCardsContainer.Name = "pnlPatientCardsContainer"
        Me.pnlPatientCardsContainer.Size = New System.Drawing.Size(810, 181)
        Me.pnlPatientCardsContainer.TabIndex = 0
        '
        'oPatientListControl
        '
        Me.oPatientListControl.BackColor = System.Drawing.Color.FromArgb(CType(CType(207,Byte),Integer), CType(CType(224,Byte),Integer), CType(CType(248,Byte),Integer))
        Me.oPatientListControl.ChkIsAccess = false
        Me.oPatientListControl.ClinicID = CType(1,Long)
        Me.oPatientListControl.ControlHeader = ""
        Me.oPatientListControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.oPatientListControl.FirstName = ""
        Me.oPatientListControl.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.oPatientListControl.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.oPatientListControl.IsSecurityUser = false
        Me.oPatientListControl.LastName = ""
        Me.oPatientListControl.Location = New System.Drawing.Point(0, 23)
        Me.oPatientListControl.Margin = New System.Windows.Forms.Padding(4)
        Me.oPatientListControl.MiddleName = ""
        Me.oPatientListControl.Name = "oPatientListControl"
        Me.oPatientListControl.PatientCode = ""
        Me.oPatientListControl.PatientID = CType(0,Long)
        Me.oPatientListControl.ProviderID = CType(0,Long)
        Me.oPatientListControl.ProviderName = ""
        Me.oPatientListControl.SelectedPatientID = CType(0,Long)
        Me.oPatientListControl.Size = New System.Drawing.Size(957, 34)
        Me.oPatientListControl.TabIndex = 0
        '
        'pnlPatientList
        '
        Me.pnlPatientList.Controls.Add(Me.oPatientListControl)
        Me.pnlPatientList.Controls.Add(Me.pnlPatientListHeader)
        Me.pnlPatientList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlPatientList.Location = New System.Drawing.Point(280, 92)
        Me.pnlPatientList.Name = "pnlPatientList"
        Me.pnlPatientList.Size = New System.Drawing.Size(957, 57)
        Me.pnlPatientList.TabIndex = 72
        '
        'pnlPatientListHeader
        '
        Me.pnlPatientListHeader.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Blue2007
        Me.pnlPatientListHeader.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlPatientListHeader.Controls.Add(Me.Label26)
        Me.pnlPatientListHeader.Controls.Add(Me.Label36)
        Me.pnlPatientListHeader.Controls.Add(Me.Label30)
        Me.pnlPatientListHeader.Controls.Add(Me.Label59)
        Me.pnlPatientListHeader.Controls.Add(Me.lblPatientListHeader)
        Me.pnlPatientListHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlPatientListHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlPatientListHeader.Name = "pnlPatientListHeader"
        Me.pnlPatientListHeader.Size = New System.Drawing.Size(957, 23)
        Me.pnlPatientListHeader.TabIndex = 1
        '
        'Label26
        '
        Me.Label26.BackColor = System.Drawing.Color.FromArgb(CType(CType(101,Byte),Integer), CType(CType(147,Byte),Integer), CType(CType(207,Byte),Integer))
        Me.Label26.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label26.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label26.ForeColor = System.Drawing.Color.Black
        Me.Label26.Location = New System.Drawing.Point(1, 0)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(955, 1)
        Me.Label26.TabIndex = 68
        Me.Label26.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label36
        '
        Me.Label36.BackColor = System.Drawing.Color.FromArgb(CType(CType(101,Byte),Integer), CType(CType(147,Byte),Integer), CType(CType(207,Byte),Integer))
        Me.Label36.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label36.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label36.ForeColor = System.Drawing.Color.Black
        Me.Label36.Location = New System.Drawing.Point(956, 0)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(1, 22)
        Me.Label36.TabIndex = 127
        Me.Label36.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label30
        '
        Me.Label30.BackColor = System.Drawing.Color.FromArgb(CType(CType(101,Byte),Integer), CType(CType(147,Byte),Integer), CType(CType(207,Byte),Integer))
        Me.Label30.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label30.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label30.ForeColor = System.Drawing.Color.Black
        Me.Label30.Location = New System.Drawing.Point(0, 0)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(1, 22)
        Me.Label30.TabIndex = 126
        Me.Label30.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label59
        '
        Me.Label59.BackColor = System.Drawing.Color.FromArgb(CType(CType(101,Byte),Integer), CType(CType(147,Byte),Integer), CType(CType(207,Byte),Integer))
        Me.Label59.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label59.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label59.ForeColor = System.Drawing.Color.Black
        Me.Label59.Location = New System.Drawing.Point(0, 22)
        Me.Label59.Name = "Label59"
        Me.Label59.Size = New System.Drawing.Size(957, 1)
        Me.Label59.TabIndex = 125
        Me.Label59.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblPatientListHeader
        '
        Me.lblPatientListHeader.BackColor = System.Drawing.Color.Transparent
        Me.lblPatientListHeader.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblPatientListHeader.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblPatientListHeader.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.lblPatientListHeader.Image = CType(resources.GetObject("lblPatientListHeader.Image"),System.Drawing.Image)
        Me.lblPatientListHeader.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblPatientListHeader.Location = New System.Drawing.Point(0, 0)
        Me.lblPatientListHeader.Name = "lblPatientListHeader"
        Me.lblPatientListHeader.Size = New System.Drawing.Size(957, 23)
        Me.lblPatientListHeader.TabIndex = 124
        Me.lblPatientListHeader.Text = "       Patient List"
        Me.lblPatientListHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cmnuProblemList
        '
        Me.cmnuProblemList.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuAddProblemList})
        '
        'mnuAddProblemList
        '
        Me.mnuAddProblemList.Index = 0
        Me.mnuAddProblemList.Text = "Add Problem"
        '
        'cmnuTasks
        '
        Me.cmnuTasks.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.cmnuTask_Delete, Me.cmnuTask_Complete, Me.cmnuTask_Add, Me.cmnuTask_AcceptTask})
        '
        'cmnuTask_Delete
        '
        Me.cmnuTask_Delete.Index = 0
        Me.cmnuTask_Delete.Text = "Delete Task"
        Me.cmnuTask_Delete.Visible = false
        '
        'cmnuTask_Complete
        '
        Me.cmnuTask_Complete.Index = 1
        Me.cmnuTask_Complete.Text = "Complete Task"
        '
        'cmnuTask_Add
        '
        Me.cmnuTask_Add.Index = 2
        Me.cmnuTask_Add.Text = "Add Task"
        '
        'cmnuTask_AcceptTask
        '
        Me.cmnuTask_AcceptTask.Index = 3
        Me.cmnuTask_AcceptTask.Text = "Accept Task"
        '
        'FileSystemWatcher1
        '
        Me.FileSystemWatcher1.EnableRaisingEvents = true
        Me.FileSystemWatcher1.SynchronizingObject = Me
        '
        'fsw_RecieveFAX
        '
        Me.fsw_RecieveFAX.EnableRaisingEvents = true
        Me.fsw_RecieveFAX.SynchronizingObject = Me
        '
        'timerLockScreen
        '
        Me.timerLockScreen.Interval = 240000
        '
        'tmrBirthDayReminder
        '
        Me.tmrBirthDayReminder.Interval = 50
        '
        'tmrshowsurepnl
        '
        Me.tmrshowsurepnl.Interval = 50
        '
        'tmrsurescriptAlert
        '
        '
        'tmrMessages
        '
        '
        'pnlBirthDayReminder
        '
        Me.pnlBirthDayReminder.BackColor = System.Drawing.Color.FromArgb(CType(CType(254,Byte),Integer), CType(CType(254,Byte),Integer), CType(CType(222,Byte),Integer))
        Me.pnlBirthDayReminder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlBirthDayReminder.Controls.Add(Me.Label18)
        Me.pnlBirthDayReminder.Controls.Add(Me.Label19)
        Me.pnlBirthDayReminder.Controls.Add(Me.picBirthReminderClose)
        Me.pnlBirthDayReminder.Controls.Add(Me.lblBirthDayMessage)
        Me.pnlBirthDayReminder.Location = New System.Drawing.Point(748, 802)
        Me.pnlBirthDayReminder.Name = "pnlBirthDayReminder"
        Me.pnlBirthDayReminder.Size = New System.Drawing.Size(374, 91)
        Me.pnlBirthDayReminder.TabIndex = 75
        '
        'Label18
        '
        Me.Label18.AutoSize = true
        Me.Label18.Location = New System.Drawing.Point(8, 64)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(53, 13)
        Me.Label18.TabIndex = 68
        Me.Label18.Text = "Extremity"
        Me.Label18.Visible = false
        '
        'Label19
        '
        Me.Label19.Font = New System.Drawing.Font("Arial", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label19.ForeColor = System.Drawing.Color.Black
        Me.Label19.Location = New System.Drawing.Point(10, 8)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(120, 13)
        Me.Label19.TabIndex = 67
        Me.Label19.Text = "Birthday Reminder"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'picBirthReminderClose
        '
        Me.picBirthReminderClose.Image = CType(resources.GetObject("picBirthReminderClose.Image"),System.Drawing.Image)
        Me.picBirthReminderClose.Location = New System.Drawing.Point(343, 0)
        Me.picBirthReminderClose.Name = "picBirthReminderClose"
        Me.picBirthReminderClose.Size = New System.Drawing.Size(21, 18)
        Me.picBirthReminderClose.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.picBirthReminderClose.TabIndex = 59
        Me.picBirthReminderClose.TabStop = false
        '
        'lblBirthDayMessage
        '
        Me.lblBirthDayMessage.BackColor = System.Drawing.SystemColors.Info
        Me.lblBirthDayMessage.Font = New System.Drawing.Font("Arial", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblBirthDayMessage.ForeColor = System.Drawing.Color.Blue
        Me.lblBirthDayMessage.Location = New System.Drawing.Point(10, 28)
        Me.lblBirthDayMessage.Name = "lblBirthDayMessage"
        Me.lblBirthDayMessage.Size = New System.Drawing.Size(350, 36)
        Me.lblBirthDayMessage.TabIndex = 58
        '
        'pnlSurescriptAlert
        '
        Me.pnlSurescriptAlert.BackColor = System.Drawing.Color.FromArgb(CType(CType(207,Byte),Integer), CType(CType(224,Byte),Integer), CType(CType(248,Byte),Integer))
        Me.pnlSurescriptAlert.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlSurescriptAlert.Controls.Add(Me.pnlsright)
        Me.pnlSurescriptAlert.Controls.Add(Me.pnlsTop)
        Me.pnlSurescriptAlert.Location = New System.Drawing.Point(828, 801)
        Me.pnlSurescriptAlert.Name = "pnlSurescriptAlert"
        Me.pnlSurescriptAlert.Size = New System.Drawing.Size(384, 150)
        Me.pnlSurescriptAlert.TabIndex = 76
        '
        'pnlsright
        '
        Me.pnlsright.BackColor = System.Drawing.Color.Transparent
        Me.pnlsright.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlsright.Controls.Add(Me.Panel3)
        Me.pnlsright.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlsright.Location = New System.Drawing.Point(0, 23)
        Me.pnlsright.Name = "pnlsright"
        Me.pnlsright.Size = New System.Drawing.Size(384, 127)
        Me.pnlsright.TabIndex = 4
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.Label52)
        Me.Panel3.Controls.Add(Me.Label53)
        Me.Panel3.Controls.Add(Me.C1AlertMessages)
        Me.Panel3.Controls.Add(Me.Panel18)
        Me.Panel3.Controls.Add(Me.Label51)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel3.Location = New System.Drawing.Point(0, 0)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(384, 127)
        Me.Panel3.TabIndex = 16
        '
        'Label52
        '
        Me.Label52.BackColor = System.Drawing.Color.FromArgb(CType(CType(114,Byte),Integer), CType(CType(75,Byte),Integer), CType(CType(0,Byte),Integer))
        Me.Label52.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label52.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label52.Location = New System.Drawing.Point(0, 1)
        Me.Label52.Name = "Label52"
        Me.Label52.Size = New System.Drawing.Size(1, 125)
        Me.Label52.TabIndex = 13
        Me.Label52.Text = "label4"
        '
        'Label53
        '
        Me.Label53.BackColor = System.Drawing.Color.FromArgb(CType(CType(114,Byte),Integer), CType(CType(75,Byte),Integer), CType(CType(0,Byte),Integer))
        Me.Label53.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label53.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label53.Location = New System.Drawing.Point(383, 1)
        Me.Label53.Name = "Label53"
        Me.Label53.Size = New System.Drawing.Size(1, 125)
        Me.Label53.TabIndex = 12
        Me.Label53.Text = "label3"
        '
        'C1AlertMessages
        '
        Me.C1AlertMessages.BackColor = System.Drawing.Color.FromArgb(CType(CType(255,Byte),Integer), CType(CType(249,Byte),Integer), CType(CType(234,Byte),Integer))
        Me.C1AlertMessages.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.C1AlertMessages.ColumnInfo = "10,0,0,0,0,90,Columns:"
        Me.C1AlertMessages.Dock = System.Windows.Forms.DockStyle.Fill
        Me.C1AlertMessages.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.C1AlertMessages.ForeColor = System.Drawing.Color.FromArgb(CType(CType(114,Byte),Integer), CType(CType(75,Byte),Integer), CType(CType(0,Byte),Integer))
        Me.C1AlertMessages.Location = New System.Drawing.Point(0, 1)
        Me.C1AlertMessages.Name = "C1AlertMessages"
        Me.C1AlertMessages.Rows.Count = 0
        Me.C1AlertMessages.Rows.DefaultSize = 18
        Me.C1AlertMessages.Rows.Fixed = 0
        Me.C1AlertMessages.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Cell
        Me.C1AlertMessages.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        Me.C1AlertMessages.ShowCellLabels = true
        Me.C1AlertMessages.Size = New System.Drawing.Size(384, 125)
        Me.C1AlertMessages.StyleInfo = resources.GetString("C1AlertMessages.StyleInfo")
        Me.C1AlertMessages.TabIndex = 10
        '
        'Panel18
        '
        Me.Panel18.BackgroundImage = CType(resources.GetObject("Panel18.BackgroundImage"),System.Drawing.Image)
        Me.Panel18.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.Panel18.Controls.Add(Me.Panel19)
        Me.Panel18.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel18.Location = New System.Drawing.Point(0, 0)
        Me.Panel18.Name = "Panel18"
        Me.Panel18.Size = New System.Drawing.Size(384, 1)
        Me.Panel18.TabIndex = 9
        '
        'Panel19
        '
        Me.Panel19.BackColor = System.Drawing.Color.FromArgb(CType(CType(73,Byte),Integer), CType(CType(111,Byte),Integer), CType(CType(152,Byte),Integer))
        Me.Panel19.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel19.Location = New System.Drawing.Point(0, 0)
        Me.Panel19.Name = "Panel19"
        Me.Panel19.Size = New System.Drawing.Size(384, 1)
        Me.Panel19.TabIndex = 7
        '
        'Label51
        '
        Me.Label51.BackColor = System.Drawing.Color.FromArgb(CType(CType(114,Byte),Integer), CType(CType(75,Byte),Integer), CType(CType(0,Byte),Integer))
        Me.Label51.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label51.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label51.Location = New System.Drawing.Point(0, 126)
        Me.Label51.Name = "Label51"
        Me.Label51.Size = New System.Drawing.Size(384, 1)
        Me.Label51.TabIndex = 14
        Me.Label51.Text = "label2"
        '
        'pnlsTop
        '
        Me.pnlsTop.BackColor = System.Drawing.Color.Transparent
        Me.pnlsTop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlsTop.Controls.Add(Me.Panel2)
        Me.pnlsTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlsTop.Location = New System.Drawing.Point(0, 0)
        Me.pnlsTop.Name = "pnlsTop"
        Me.pnlsTop.Size = New System.Drawing.Size(384, 23)
        Me.pnlsTop.TabIndex = 2
        '
        'Panel2
        '
        Me.Panel2.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongYellow
        Me.Panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel2.Controls.Add(Me.btnSRefresh)
        Me.Panel2.Controls.Add(Me.btnSurescriptClose)
        Me.Panel2.Controls.Add(Me.Label32)
        Me.Panel2.Controls.Add(Me.Label48)
        Me.Panel2.Controls.Add(Me.Label50)
        Me.Panel2.Controls.Add(Me.Label49)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(384, 23)
        Me.Panel2.TabIndex = 15
        '
        'btnSRefresh
        '
        Me.btnSRefresh.BackColor = System.Drawing.Color.Transparent
        Me.btnSRefresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnSRefresh.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnSRefresh.FlatAppearance.BorderSize = 0
        Me.btnSRefresh.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnSRefresh.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnSRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSRefresh.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btnSRefresh.Image = CType(resources.GetObject("btnSRefresh.Image"),System.Drawing.Image)
        Me.btnSRefresh.Location = New System.Drawing.Point(339, 1)
        Me.btnSRefresh.Name = "btnSRefresh"
        Me.btnSRefresh.Size = New System.Drawing.Size(22, 22)
        Me.btnSRefresh.TabIndex = 2
        Me.btnSRefresh.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSRefresh.UseVisualStyleBackColor = false
        '
        'btnSurescriptClose
        '
        Me.btnSurescriptClose.BackColor = System.Drawing.Color.Transparent
        Me.btnSurescriptClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnSurescriptClose.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnSurescriptClose.FlatAppearance.BorderSize = 0
        Me.btnSurescriptClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnSurescriptClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnSurescriptClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSurescriptClose.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btnSurescriptClose.Image = CType(resources.GetObject("btnSurescriptClose.Image"),System.Drawing.Image)
        Me.btnSurescriptClose.Location = New System.Drawing.Point(361, 1)
        Me.btnSurescriptClose.Name = "btnSurescriptClose"
        Me.btnSurescriptClose.Size = New System.Drawing.Size(22, 22)
        Me.btnSurescriptClose.TabIndex = 0
        Me.btnSurescriptClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSurescriptClose.UseVisualStyleBackColor = false
        '
        'Label32
        '
        Me.Label32.BackColor = System.Drawing.Color.Transparent
        Me.Label32.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label32.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label32.ForeColor = System.Drawing.Color.FromArgb(CType(CType(114,Byte),Integer), CType(CType(75,Byte),Integer), CType(CType(0,Byte),Integer))
        Me.Label32.Image = CType(resources.GetObject("Label32.Image"),System.Drawing.Image)
        Me.Label32.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label32.Location = New System.Drawing.Point(1, 1)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(382, 22)
        Me.Label32.TabIndex = 1
        Me.Label32.Text = "       Pending Messages"
        Me.Label32.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label48
        '
        Me.Label48.BackColor = System.Drawing.Color.FromArgb(CType(CType(114,Byte),Integer), CType(CType(75,Byte),Integer), CType(CType(0,Byte),Integer))
        Me.Label48.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label48.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label48.Location = New System.Drawing.Point(0, 1)
        Me.Label48.Name = "Label48"
        Me.Label48.Size = New System.Drawing.Size(1, 22)
        Me.Label48.TabIndex = 11
        Me.Label48.Text = "label4"
        '
        'Label50
        '
        Me.Label50.BackColor = System.Drawing.Color.FromArgb(CType(CType(114,Byte),Integer), CType(CType(75,Byte),Integer), CType(CType(0,Byte),Integer))
        Me.Label50.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label50.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label50.Location = New System.Drawing.Point(0, 0)
        Me.Label50.Name = "Label50"
        Me.Label50.Size = New System.Drawing.Size(383, 1)
        Me.Label50.TabIndex = 9
        Me.Label50.Text = "label1"
        '
        'Label49
        '
        Me.Label49.BackColor = System.Drawing.Color.FromArgb(CType(CType(114,Byte),Integer), CType(CType(75,Byte),Integer), CType(CType(0,Byte),Integer))
        Me.Label49.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label49.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label49.Location = New System.Drawing.Point(383, 0)
        Me.Label49.Name = "Label49"
        Me.Label49.Size = New System.Drawing.Size(1, 23)
        Me.Label49.TabIndex = 10
        Me.Label49.Text = "label3"
        '
        'cMnuPatient
        '
        Me.cMnuPatient.Name = "cMnuPatient"
        Me.cMnuPatient.Size = New System.Drawing.Size(61, 4)
        '
        'pnlTAsk_TaskSentToMe
        '
        Me.pnlTAsk_TaskSentToMe.InnerContainer = Me.pnlTAsk_TaskSentToMeContainer
        Me.pnlTAsk_TaskSentToMe.Location = New System.Drawing.Point(0, 22)
        Me.pnlTAsk_TaskSentToMe.Name = "pnlTAsk_TaskSentToMe"
        Me.pnlTAsk_TaskSentToMe.Size = New System.Drawing.Size(196, 58)
        Me.pnlTAsk_TaskSentToMe.TabIndex = 4
        Me.pnlTAsk_TaskSentToMe.Text = "Tasks For Me"
        '
        'pnlTAsk_TaskSentToMeContainer
        '
        Me.pnlTAsk_TaskSentToMeContainer.Location = New System.Drawing.Point(0, 0)
        Me.pnlTAsk_TaskSentToMeContainer.Name = "pnlTAsk_TaskSentToMeContainer"
        Me.pnlTAsk_TaskSentToMeContainer.Size = New System.Drawing.Size(196, 58)
        Me.pnlTAsk_TaskSentToMeContainer.TabIndex = 0
        '
        'pnlTask_TaskSentByMe
        '
        Me.pnlTask_TaskSentByMe.InnerContainer = Me.pnlTask_TaskSentByMeContainer
        Me.pnlTask_TaskSentByMe.Location = New System.Drawing.Point(0, 146)
        Me.pnlTask_TaskSentByMe.Name = "pnlTask_TaskSentByMe"
        Me.pnlTask_TaskSentByMe.Size = New System.Drawing.Size(196, 57)
        Me.pnlTask_TaskSentByMe.TabIndex = 4
        Me.pnlTask_TaskSentByMe.Text = "Task Sent By Me"
        '
        'pnlTask_TaskSentByMeContainer
        '
        Me.pnlTask_TaskSentByMeContainer.Location = New System.Drawing.Point(0, 0)
        Me.pnlTask_TaskSentByMeContainer.Name = "pnlTask_TaskSentByMeContainer"
        Me.pnlTask_TaskSentByMeContainer.Size = New System.Drawing.Size(196, 57)
        Me.pnlTask_TaskSentByMeContainer.TabIndex = 0
        '
        'pnlTask_MyTasks
        '
        Me.pnlTask_MyTasks.InnerContainer = Me.pnlTask_MyTasksContainer
        Me.pnlTask_MyTasks.Location = New System.Drawing.Point(0, 84)
        Me.pnlTask_MyTasks.Name = "pnlTask_MyTasks"
        Me.pnlTask_MyTasks.Size = New System.Drawing.Size(196, 58)
        Me.pnlTask_MyTasks.TabIndex = 4
        Me.pnlTask_MyTasks.Text = "My Tasks"
        '
        'pnlTask_MyTasksContainer
        '
        Me.pnlTask_MyTasksContainer.Location = New System.Drawing.Point(0, 0)
        Me.pnlTask_MyTasksContainer.Name = "pnlTask_MyTasksContainer"
        Me.pnlTask_MyTasksContainer.Size = New System.Drawing.Size(196, 58)
        Me.pnlTask_MyTasksContainer.TabIndex = 0
        '
        'cmnuToolStripCustomize
        '
        Me.cmnuToolStripCustomize.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CustomizeToolStripMenuItem})
        Me.cmnuToolStripCustomize.Name = "cmnuToolStripCustomize"
        Me.cmnuToolStripCustomize.Size = New System.Drawing.Size(131, 26)
        '
        'CustomizeToolStripMenuItem
        '
        Me.CustomizeToolStripMenuItem.Image = CType(resources.GetObject("CustomizeToolStripMenuItem.Image"),System.Drawing.Image)
        Me.CustomizeToolStripMenuItem.Name = "CustomizeToolStripMenuItem"
        Me.CustomizeToolStripMenuItem.Size = New System.Drawing.Size(130, 22)
        Me.CustomizeToolStripMenuItem.Text = "Customize"
        '
        'cmnuPatientDetails
        '
        Me.cmnuPatientDetails.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem1, Me.AddTriageToolStripMenuItem})
        Me.cmnuPatientDetails.Name = "cMnuPatient"
        Me.cmnuPatientDetails.Size = New System.Drawing.Size(133, 48)
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.ToolStripMenuItem1.Image = CType(resources.GetObject("ToolStripMenuItem1.Image"),System.Drawing.Image)
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(132, 22)
        Me.ToolStripMenuItem1.Text = "New Exam"
        '
        'AddTriageToolStripMenuItem
        '
        Me.AddTriageToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.AddTriageToolStripMenuItem.Image = CType(resources.GetObject("AddTriageToolStripMenuItem.Image"),System.Drawing.Image)
        Me.AddTriageToolStripMenuItem.Name = "AddTriageToolStripMenuItem"
        Me.AddTriageToolStripMenuItem.Size = New System.Drawing.Size(132, 22)
        Me.AddTriageToolStripMenuItem.Text = "Add Triage"
        '
        'imgList_Common
        '
        Me.imgList_Common.ImageStream = CType(resources.GetObject("imgList_Common.ImageStream"),System.Windows.Forms.ImageListStreamer)
        Me.imgList_Common.TransparentColor = System.Drawing.Color.Transparent
        Me.imgList_Common.Images.SetKeyName(0, "")
        Me.imgList_Common.Images.SetKeyName(1, "")
        Me.imgList_Common.Images.SetKeyName(2, "")
        Me.imgList_Common.Images.SetKeyName(3, "")
        Me.imgList_Common.Images.SetKeyName(4, "")
        Me.imgList_Common.Images.SetKeyName(5, "")
        Me.imgList_Common.Images.SetKeyName(6, "")
        Me.imgList_Common.Images.SetKeyName(7, "Patient Cheif Complement.ico")
        Me.imgList_Common.Images.SetKeyName(8, "")
        Me.imgList_Common.Images.SetKeyName(9, "")
        Me.imgList_Common.Images.SetKeyName(10, "Pull Chart.ico")
        Me.imgList_Common.Images.SetKeyName(11, "")
        Me.imgList_Common.Images.SetKeyName(12, "")
        Me.imgList_Common.Images.SetKeyName(13, "")
        Me.imgList_Common.Images.SetKeyName(14, "")
        Me.imgList_Common.Images.SetKeyName(15, "")
        Me.imgList_Common.Images.SetKeyName(16, "New.ico")
        Me.imgList_Common.Images.SetKeyName(17, "Note.ico")
        Me.imgList_Common.Images.SetKeyName(18, "High PriorityRed.png")
        Me.imgList_Common.Images.SetKeyName(19, "Low Priority.png")
        Me.imgList_Common.Images.SetKeyName(20, "Password Policy.ico")
        Me.imgList_Common.Images.SetKeyName(21, "Report.ico")
        Me.imgList_Common.Images.SetKeyName(22, "New report03.ico")
        Me.imgList_Common.Images.SetKeyName(23, "Report_New.ico")
        Me.imgList_Common.Images.SetKeyName(24, "Save as Copy.ico")
        Me.imgList_Common.Images.SetKeyName(25, "Yes Lab.ico")
        Me.imgList_Common.Images.SetKeyName(26, "No Lab.ico")
        Me.imgList_Common.Images.SetKeyName(27, "Generate CCD.ico")
        Me.imgList_Common.Images.SetKeyName(28, "CheckIN.ico")
        Me.imgList_Common.Images.SetKeyName(29, "Checkout01.ico")
        Me.imgList_Common.Images.SetKeyName(30, "Cases.ico")
        Me.imgList_Common.Images.SetKeyName(31, "Copy Exam.ico")
        Me.imgList_Common.Images.SetKeyName(32, "Patient Specific.ico")
        Me.imgList_Common.Images.SetKeyName(33, "Patient care plan.ico")
        Me.imgList_Common.Images.SetKeyName(34, "Generate CDA.ico")
        Me.imgList_Common.Images.SetKeyName(35, "amendment.ico")
        Me.imgList_Common.Images.SetKeyName(36, "Task_NoOwner.ico")
        Me.imgList_Common.Images.SetKeyName(37, "Task_OtherTaken.ico")
        Me.imgList_Common.Images.SetKeyName(38, "Task_Owner.ico")
        Me.imgList_Common.Images.SetKeyName(39, "Task_Single.ico")
        '
        'cmnu_Tasks
        '
        Me.cmnu_Tasks.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.cmnu_Tasks.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmnuItem_Priority, Me.cmnuItem_NewTask, Me.cmnuItem_AcceptTask, Me.cmnuItem_RejectTask, Me.cmnuItem_CompleteTask, Me.cmnuItem_CompleteAll, Me.cmunItem_Completed, Me.cmnuItem_TaskSeparator, Me.cmnuItem_TrackTasks, Me.cmnuItem_TaskTake, Me.cmnuItem_DeleteTask, Me.cmnuItem_FollowUpTask})
        Me.cmnu_Tasks.Name = "cmnu_Appointment"
        Me.cmnu_Tasks.Size = New System.Drawing.Size(205, 252)
        '
        'cmnuItem_Priority
        '
        Me.cmnuItem_Priority.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.cmnuItem_Priority.Image = CType(resources.GetObject("cmnuItem_Priority.Image"),System.Drawing.Image)
        Me.cmnuItem_Priority.Name = "cmnuItem_Priority"
        Me.cmnuItem_Priority.Size = New System.Drawing.Size(204, 22)
        Me.cmnuItem_Priority.Text = "Priority"
        '
        'cmnuItem_NewTask
        '
        Me.cmnuItem_NewTask.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.cmnuItem_NewTask.Image = CType(resources.GetObject("cmnuItem_NewTask.Image"),System.Drawing.Image)
        Me.cmnuItem_NewTask.Name = "cmnuItem_NewTask"
        Me.cmnuItem_NewTask.Size = New System.Drawing.Size(204, 22)
        Me.cmnuItem_NewTask.Text = "New Task"
        '
        'cmnuItem_AcceptTask
        '
        Me.cmnuItem_AcceptTask.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.cmnuItem_AcceptTask.Image = CType(resources.GetObject("cmnuItem_AcceptTask.Image"),System.Drawing.Image)
        Me.cmnuItem_AcceptTask.Name = "cmnuItem_AcceptTask"
        Me.cmnuItem_AcceptTask.Size = New System.Drawing.Size(204, 22)
        Me.cmnuItem_AcceptTask.Text = "Accept Task"
        '
        'cmnuItem_RejectTask
        '
        Me.cmnuItem_RejectTask.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.cmnuItem_RejectTask.Image = CType(resources.GetObject("cmnuItem_RejectTask.Image"),System.Drawing.Image)
        Me.cmnuItem_RejectTask.Name = "cmnuItem_RejectTask"
        Me.cmnuItem_RejectTask.Size = New System.Drawing.Size(204, 22)
        Me.cmnuItem_RejectTask.Text = "Decline Task"
        '
        'cmnuItem_CompleteTask
        '
        Me.cmnuItem_CompleteTask.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.cmnuItem_CompleteTask.Image = CType(resources.GetObject("cmnuItem_CompleteTask.Image"),System.Drawing.Image)
        Me.cmnuItem_CompleteTask.Name = "cmnuItem_CompleteTask"
        Me.cmnuItem_CompleteTask.Size = New System.Drawing.Size(204, 22)
        Me.cmnuItem_CompleteTask.Text = "Complete Task"
        '
        'cmnuItem_CompleteAll
        '
        Me.cmnuItem_CompleteAll.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.cmnuItem_CompleteAll.Image = CType(resources.GetObject("cmnuItem_CompleteAll.Image"),System.Drawing.Image)
        Me.cmnuItem_CompleteAll.Name = "cmnuItem_CompleteAll"
        Me.cmnuItem_CompleteAll.Size = New System.Drawing.Size(204, 22)
        Me.cmnuItem_CompleteAll.Text = "Complete Task for all Users"
        '
        'cmunItem_Completed
        '
        Me.cmunItem_Completed.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmnuItem_0Percent, Me.cmnuItem_25Percent, Me.cmnuItem_50Percent, Me.cmnuItem_75Percent, Me.cmnuItem_100Percent})
        Me.cmunItem_Completed.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.cmunItem_Completed.Image = CType(resources.GetObject("cmunItem_Completed.Image"),System.Drawing.Image)
        Me.cmunItem_Completed.Name = "cmunItem_Completed"
        Me.cmunItem_Completed.Size = New System.Drawing.Size(204, 22)
        Me.cmunItem_Completed.Text = "Partial completion options"
        '
        'cmnuItem_0Percent
        '
        Me.cmnuItem_0Percent.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.cmnuItem_0Percent.Image = CType(resources.GetObject("cmnuItem_0Percent.Image"),System.Drawing.Image)
        Me.cmnuItem_0Percent.Name = "cmnuItem_0Percent"
        Me.cmnuItem_0Percent.Size = New System.Drawing.Size(106, 22)
        Me.cmnuItem_0Percent.Tag = "0"
        Me.cmnuItem_0Percent.Text = "0 %"
        '
        'cmnuItem_25Percent
        '
        Me.cmnuItem_25Percent.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.cmnuItem_25Percent.Image = CType(resources.GetObject("cmnuItem_25Percent.Image"),System.Drawing.Image)
        Me.cmnuItem_25Percent.Name = "cmnuItem_25Percent"
        Me.cmnuItem_25Percent.Size = New System.Drawing.Size(106, 22)
        Me.cmnuItem_25Percent.Tag = "25"
        Me.cmnuItem_25Percent.Text = "25 %"
        '
        'cmnuItem_50Percent
        '
        Me.cmnuItem_50Percent.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.cmnuItem_50Percent.Image = CType(resources.GetObject("cmnuItem_50Percent.Image"),System.Drawing.Image)
        Me.cmnuItem_50Percent.Name = "cmnuItem_50Percent"
        Me.cmnuItem_50Percent.Size = New System.Drawing.Size(106, 22)
        Me.cmnuItem_50Percent.Tag = "50"
        Me.cmnuItem_50Percent.Text = "50 %"
        '
        'cmnuItem_75Percent
        '
        Me.cmnuItem_75Percent.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.cmnuItem_75Percent.Image = CType(resources.GetObject("cmnuItem_75Percent.Image"),System.Drawing.Image)
        Me.cmnuItem_75Percent.Name = "cmnuItem_75Percent"
        Me.cmnuItem_75Percent.Size = New System.Drawing.Size(106, 22)
        Me.cmnuItem_75Percent.Tag = "75"
        Me.cmnuItem_75Percent.Text = "75 %"
        '
        'cmnuItem_100Percent
        '
        Me.cmnuItem_100Percent.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.cmnuItem_100Percent.Image = CType(resources.GetObject("cmnuItem_100Percent.Image"),System.Drawing.Image)
        Me.cmnuItem_100Percent.Name = "cmnuItem_100Percent"
        Me.cmnuItem_100Percent.Size = New System.Drawing.Size(106, 22)
        Me.cmnuItem_100Percent.Tag = "100"
        Me.cmnuItem_100Percent.Text = "100 %"
        '
        'cmnuItem_TaskSeparator
        '
        Me.cmnuItem_TaskSeparator.Name = "cmnuItem_TaskSeparator"
        Me.cmnuItem_TaskSeparator.Size = New System.Drawing.Size(201, 6)
        '
        'cmnuItem_TrackTasks
        '
        Me.cmnuItem_TrackTasks.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.cmnuItem_TrackTasks.Image = CType(resources.GetObject("cmnuItem_TrackTasks.Image"),System.Drawing.Image)
        Me.cmnuItem_TrackTasks.Name = "cmnuItem_TrackTasks"
        Me.cmnuItem_TrackTasks.Size = New System.Drawing.Size(204, 22)
        Me.cmnuItem_TrackTasks.Text = "Track Tasks"
        '
        'cmnuItem_TaskTake
        '
        Me.cmnuItem_TaskTake.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.cmnuItem_TaskTake.Image = CType(resources.GetObject("cmnuItem_TaskTake.Image"),System.Drawing.Image)
        Me.cmnuItem_TaskTake.Name = "cmnuItem_TaskTake"
        Me.cmnuItem_TaskTake.Size = New System.Drawing.Size(204, 22)
        Me.cmnuItem_TaskTake.Text = "Take"
        '
        'cmnuItem_DeleteTask
        '
        Me.cmnuItem_DeleteTask.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.cmnuItem_DeleteTask.Image = CType(resources.GetObject("cmnuItem_DeleteTask.Image"),System.Drawing.Image)
        Me.cmnuItem_DeleteTask.Name = "cmnuItem_DeleteTask"
        Me.cmnuItem_DeleteTask.Size = New System.Drawing.Size(204, 22)
        Me.cmnuItem_DeleteTask.Tag = "Delete"
        Me.cmnuItem_DeleteTask.Text = "Delete"
        Me.cmnuItem_DeleteTask.ToolTipText = "Delete"
        Me.cmnuItem_DeleteTask.Visible = false
        '
        'cmnuItem_FollowUpTask
        '
        Me.cmnuItem_FollowUpTask.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.cmnuItem_FollowUpTask.Image = CType(resources.GetObject("cmnuItem_FollowUpTask.Image"),System.Drawing.Image)
        Me.cmnuItem_FollowUpTask.Name = "cmnuItem_FollowUpTask"
        Me.cmnuItem_FollowUpTask.Size = New System.Drawing.Size(204, 22)
        Me.cmnuItem_FollowUpTask.Tag = "Followup"
        Me.cmnuItem_FollowUpTask.Text = "Follow Up"
        Me.cmnuItem_FollowUpTask.Visible = false
        '
        'C1SuperTooltip1
        '
        Me.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None
        Me.C1SuperTooltip1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        '
        'Imgts_PatientDetails
        '
        Me.Imgts_PatientDetails.ImageStream = CType(resources.GetObject("Imgts_PatientDetails.ImageStream"),System.Windows.Forms.ImageListStreamer)
        Me.Imgts_PatientDetails.TransparentColor = System.Drawing.Color.Transparent
        Me.Imgts_PatientDetails.Images.SetKeyName(0, "History Add.png")
        Me.Imgts_PatientDetails.Images.SetKeyName(1, "Insurance Add.png")
        Me.Imgts_PatientDetails.Images.SetKeyName(2, "Billing Add.png")
        Me.Imgts_PatientDetails.Images.SetKeyName(3, "Vitals Add.png")
        Me.Imgts_PatientDetails.Images.SetKeyName(4, "Pending Fax Add.png")
        Me.Imgts_PatientDetails.Images.SetKeyName(5, "Sent Fax Add.png")
        Me.Imgts_PatientDetails.Images.SetKeyName(6, "Lab add.png")
        Me.Imgts_PatientDetails.Images.SetKeyName(7, "Health Plan Add.png")
        Me.Imgts_PatientDetails.Images.SetKeyName(8, "Aduit Trial Add.png")
        Me.Imgts_PatientDetails.Images.SetKeyName(9, "Medication Add.png")
        Me.Imgts_PatientDetails.Images.SetKeyName(10, "RXMX.ico")
        Me.Imgts_PatientDetails.Images.SetKeyName(11, "Problem List Add.png")
        Me.Imgts_PatientDetails.Images.SetKeyName(12, "View Document Add.png")
        Me.Imgts_PatientDetails.Images.SetKeyName(13, "Orders Add.png")
        Me.Imgts_PatientDetails.Images.SetKeyName(14, "New Message.ico")
        Me.Imgts_PatientDetails.Images.SetKeyName(15, "Past Exam Add.png")
        Me.Imgts_PatientDetails.Images.SetKeyName(16, "Nurse Note Add.png")
        Me.Imgts_PatientDetails.Images.SetKeyName(17, "Patient Consent Add.png")
        Me.Imgts_PatientDetails.Images.SetKeyName(18, "Patient Latter Add.png")
        Me.Imgts_PatientDetails.Images.SetKeyName(19, "Disclosure Management Add.png")
        Me.Imgts_PatientDetails.Images.SetKeyName(20, "Pat control customization.ico")
        Me.Imgts_PatientDetails.Images.SetKeyName(21, "Show Graph.ico")
        Me.Imgts_PatientDetails.Images.SetKeyName(22, "Advance Graph.ico")
        Me.Imgts_PatientDetails.Images.SetKeyName(23, "Message History.ico")
        Me.Imgts_PatientDetails.Images.SetKeyName(24, "Prior Authorization.ico")
        Me.Imgts_PatientDetails.Images.SetKeyName(25, "Eligibility.ico")
        Me.Imgts_PatientDetails.Images.SetKeyName(26, "Co-pay.ico")
        Me.Imgts_PatientDetails.Images.SetKeyName(27, "Claim Validation Settings.ico")
        Me.Imgts_PatientDetails.Images.SetKeyName(28, "Modify.ico")
        Me.Imgts_PatientDetails.Images.SetKeyName(29, "Ledger.ico")
        Me.Imgts_PatientDetails.Images.SetKeyName(30, "Payment.ico")
        Me.Imgts_PatientDetails.Images.SetKeyName(31, "Add Triage.ico")
        Me.Imgts_PatientDetails.Images.SetKeyName(32, "Add pharmacy Notes.ico")
        Me.Imgts_PatientDetails.Images.SetKeyName(33, "Patient Status.ico")
        Me.Imgts_PatientDetails.Images.SetKeyName(34, "Modify Referrals.png")
        Me.Imgts_PatientDetails.Images.SetKeyName(35, "Modify Proir Authorization.ico")
        Me.Imgts_PatientDetails.Images.SetKeyName(36, "CheckTemplate.ico")
        Me.Imgts_PatientDetails.Images.SetKeyName(37, "Appointment Letter Templatenew.ico")
        Me.Imgts_PatientDetails.Images.SetKeyName(38, "Rx Status.ico")
        Me.Imgts_PatientDetails.Images.SetKeyName(39, "Complete Task.ico")
        Me.Imgts_PatientDetails.Images.SetKeyName(40, "Accept Task.ico")
        Me.Imgts_PatientDetails.Images.SetKeyName(41, "0%.ico")
        Me.Imgts_PatientDetails.Images.SetKeyName(42, "25%.ico")
        Me.Imgts_PatientDetails.Images.SetKeyName(43, "50%.ico")
        Me.Imgts_PatientDetails.Images.SetKeyName(44, "75%.ico")
        Me.Imgts_PatientDetails.Images.SetKeyName(45, "100%.ico")
        Me.Imgts_PatientDetails.Images.SetKeyName(46, "Percentage.ico")
        Me.Imgts_PatientDetails.Images.SetKeyName(47, "Decline Task.ico")
        Me.Imgts_PatientDetails.Images.SetKeyName(48, "Today.png")
        Me.Imgts_PatientDetails.Images.SetKeyName(49, "Tomorrow.png")
        Me.Imgts_PatientDetails.Images.SetKeyName(50, "No Date.png")
        Me.Imgts_PatientDetails.Images.SetKeyName(51, "Flag Yellow.png")
        Me.Imgts_PatientDetails.Images.SetKeyName(52, "Low Priority.png")
        Me.Imgts_PatientDetails.Images.SetKeyName(53, "High PriorityRed.png")
        Me.Imgts_PatientDetails.Images.SetKeyName(54, "Edit.png")
        Me.Imgts_PatientDetails.Images.SetKeyName(55, "Delete.png")
        Me.Imgts_PatientDetails.Images.SetKeyName(56, "Follow UP.png")
        Me.Imgts_PatientDetails.Images.SetKeyName(57, "Add Cases.ico")
        Me.Imgts_PatientDetails.Images.SetKeyName(58, "Modify Cases.ico")
        Me.Imgts_PatientDetails.Images.SetKeyName(59, "View Benefits.ico")
        Me.Imgts_PatientDetails.Images.SetKeyName(60, "Communication.ico")
        Me.Imgts_PatientDetails.Images.SetKeyName(61, "Intuit Secure Messaging.ico")
        Me.Imgts_PatientDetails.Images.SetKeyName(62, "Reply.ico")
        Me.Imgts_PatientDetails.Images.SetKeyName(63, "Create Task.ico")
        Me.Imgts_PatientDetails.Images.SetKeyName(64, "Delete message.ico")
        Me.Imgts_PatientDetails.Images.SetKeyName(65, "New secure message.ico")
        Me.Imgts_PatientDetails.Images.SetKeyName(66, "read message.ico")
        Me.Imgts_PatientDetails.Images.SetKeyName(67, "Unread message.ico")
        Me.Imgts_PatientDetails.Images.SetKeyName(68, "rightarrow01.ico")
        Me.Imgts_PatientDetails.Images.SetKeyName(69, "leftarrow01.ico")
        Me.Imgts_PatientDetails.Images.SetKeyName(70, "Open message.ico")
        Me.Imgts_PatientDetails.Images.SetKeyName(71, "View History.ico")
        Me.Imgts_PatientDetails.Images.SetKeyName(72, "High Priority.ico")
        Me.Imgts_PatientDetails.Images.SetKeyName(73, "View Order.ico")
        Me.Imgts_PatientDetails.Images.SetKeyName(74, "Modify Order.ico")
        Me.Imgts_PatientDetails.Images.SetKeyName(75, "AddPatientEducation.ico")
        Me.Imgts_PatientDetails.Images.SetKeyName(76, "RxFillNotification.ico")
        Me.Imgts_PatientDetails.Images.SetKeyName(77, "CancelRx.png")
        Me.Imgts_PatientDetails.Images.SetKeyName(78, "CancelRxNotification.ico")
        '
        'cmnu_messages
        '
        Me.cmnu_messages.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.cmnu_messages.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripSeparator10})
        Me.cmnu_messages.Name = "cmnu_Appointment"
        Me.cmnu_messages.Size = New System.Drawing.Size(61, 10)
        '
        'ToolStripSeparator10
        '
        Me.ToolStripSeparator10.Name = "ToolStripSeparator10"
        Me.ToolStripSeparator10.Size = New System.Drawing.Size(57, 6)
        '
        'tmrCopayAlert
        '
        Me.tmrCopayAlert.Interval = 300
        '
        'pnlFormularyTransactionMessage
        '
        Me.pnlFormularyTransactionMessage.BackColor = System.Drawing.Color.White
        Me.pnlFormularyTransactionMessage.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Gradient
        Me.pnlFormularyTransactionMessage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlFormularyTransactionMessage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlFormularyTransactionMessage.Controls.Add(Me.lblFormularyTransactionMessage)
        Me.pnlFormularyTransactionMessage.Controls.Add(Me.Label6)
        Me.pnlFormularyTransactionMessage.Location = New System.Drawing.Point(421, 347)
        Me.pnlFormularyTransactionMessage.Name = "pnlFormularyTransactionMessage"
        Me.pnlFormularyTransactionMessage.Size = New System.Drawing.Size(423, 80)
        Me.pnlFormularyTransactionMessage.TabIndex = 85
        Me.pnlFormularyTransactionMessage.Visible = false
        '
        'lblFormularyTransactionMessage
        '
        Me.lblFormularyTransactionMessage.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.lblFormularyTransactionMessage.AutoSize = true
        Me.lblFormularyTransactionMessage.BackColor = System.Drawing.Color.Transparent
        Me.lblFormularyTransactionMessage.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblFormularyTransactionMessage.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192,Byte),Integer), CType(CType(64,Byte),Integer), CType(CType(0,Byte),Integer))
        Me.lblFormularyTransactionMessage.Location = New System.Drawing.Point(21, 33)
        Me.lblFormularyTransactionMessage.MaximumSize = New System.Drawing.Size(382, 16)
        Me.lblFormularyTransactionMessage.MinimumSize = New System.Drawing.Size(382, 16)
        Me.lblFormularyTransactionMessage.Name = "lblFormularyTransactionMessage"
        Me.lblFormularyTransactionMessage.Size = New System.Drawing.Size(382, 16)
        Me.lblFormularyTransactionMessage.TabIndex = 61
        Me.lblFormularyTransactionMessage.Text = "Sending eligibility information…"
        '
        'Label6
        '
        Me.Label6.AutoSize = true
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 12!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label6.Location = New System.Drawing.Point(20, 7)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(119, 19)
        Me.Label6.TabIndex = 61
        Me.Label6.Text = "Please wait..."
        '
        'PrintDialog1
        '
        Me.PrintDialog1.UseEXDialog = true
        '
        'printDoc
        '
        '
        'CmnuPatientstatus
        '
        Me.CmnuPatientstatus.Name = "CmnuPatientstatus"
        Me.CmnuPatientstatus.Size = New System.Drawing.Size(61, 4)
        '
        'BackgroundWorker_MessageTimer
        '
        '
        'BackgroundWorker_InitiliseFaxSetting
        '
        '
        'HelpComponent1
        '
        Me.HelpComponent1.Mode = gloEMR.Help.HelpComponent.ProviderMode.Client
        '
        'pnlWait
        '
        Me.pnlWait.BackColor = System.Drawing.Color.White
        Me.pnlWait.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Gradient
        Me.pnlWait.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlWait.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlWait.Controls.Add(Me.Label84)
        Me.pnlWait.Controls.Add(Me.Label85)
        Me.pnlWait.Location = New System.Drawing.Point(421, 331)
        Me.pnlWait.Name = "pnlWait"
        Me.pnlWait.Size = New System.Drawing.Size(423, 80)
        Me.pnlWait.TabIndex = 95
        Me.pnlWait.Visible = false
        '
        'Label84
        '
        Me.Label84.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.Label84.AutoSize = true
        Me.Label84.BackColor = System.Drawing.Color.Transparent
        Me.Label84.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label84.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192,Byte),Integer), CType(CType(64,Byte),Integer), CType(CType(0,Byte),Integer))
        Me.Label84.Location = New System.Drawing.Point(21, 33)
        Me.Label84.MaximumSize = New System.Drawing.Size(382, 16)
        Me.Label84.MinimumSize = New System.Drawing.Size(382, 16)
        Me.Label84.Name = "Label84"
        Me.Label84.Size = New System.Drawing.Size(382, 16)
        Me.Label84.TabIndex = 61
        Me.Label84.Text = "Loading Template..."
        '
        'Label85
        '
        Me.Label85.AutoSize = true
        Me.Label85.BackColor = System.Drawing.Color.Transparent
        Me.Label85.Font = New System.Drawing.Font("Tahoma", 12!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label85.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label85.Location = New System.Drawing.Point(20, 7)
        Me.Label85.Name = "Label85"
        Me.Label85.Size = New System.Drawing.Size(119, 19)
        Me.Label85.TabIndex = 61
        Me.Label85.Text = "Please wait..."
        '
        'notifyInbox
        '
        Me.notifyInbox.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info
        Me.notifyInbox.BalloonTipTitle = "New secure messages are available"
        Me.notifyInbox.Icon = CType(resources.GetObject("notifyInbox.Icon"),System.Drawing.Icon)
        Me.notifyInbox.Text = "New secure messages are available"
        '
        'MainMenu
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207,Byte),Integer), CType(CType(224,Byte),Integer), CType(CType(248,Byte),Integer))
        Me.ClientSize = New System.Drawing.Size(1265, 742)
        Me.Controls.Add(Me.pnlWait)
        Me.Controls.Add(Me.pnlFormularyTransactionMessage)
        Me.Controls.Add(Me.pnlSurescriptAlert)
        Me.Controls.Add(Me.pnlBirthDayReminder)
        Me.Controls.Add(Me.pnlPatientList)
        Me.Controls.Add(Me.uiPanel0)
        Me.Controls.Add(Me.pnlGrPatientDemoCardsStatus)
        Me.Controls.Add(Me.pnlPatientDetail)
        Me.Controls.Add(Me.pnlLeft_Nav)
        Me.Controls.Add(Me.pnlMainStatusBar)
        Me.Controls.Add(Me.pnlMainToolBar)
        Me.Controls.Add(Me.pnlMainMenu)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
        Me.IsMdiContainer = true
        Me.KeyPreview = true
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "MainMenu"
        Me.Text = "QEMR"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.picLockScreen,System.ComponentModel.ISupportInitialize).EndInit
        Me.pnlMainToolBar.ResumeLayout(false)
        Me.pnlMainToolBar.PerformLayout
        Me.tlbStripMain.ResumeLayout(false)
        Me.tlbStripMain.PerformLayout
        Me.pnlMainMenu.ResumeLayout(false)
        Me.pnlMainMenu.PerformLayout
        Me.MenuStrip1.ResumeLayout(false)
        Me.MenuStrip1.PerformLayout
        Me.pnlMainStatusBar.ResumeLayout(false)
        Me.pnlMainStatusBar.PerformLayout
        CType(Me.Vcmd,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.DgnEngineControl1,System.ComponentModel.ISupportInitialize).EndInit
        Me.StatusStrip.ResumeLayout(false)
        Me.StatusStrip.PerformLayout
        CType(Me.DgnMicBtn1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.uiPanelManager1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.pnlPatientCheckInStatus,System.ComponentModel.ISupportInitialize).EndInit
        Me.pnlPatientCheckInStatus.ResumeLayout(false)
        Me.pnlPatientCheckInStatusContainer.ResumeLayout(false)
        CType(Me.c1PatientCheckInStatus,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.pnlLeft_Nav,System.ComponentModel.ISupportInitialize).EndInit
        Me.pnlLeft_Nav.ResumeLayout(false)
        CType(Me.pnl_Messages,System.ComponentModel.ISupportInitialize).EndInit
        Me.pnl_Messages.ResumeLayout(false)
        Me.pnl_MessagesContainer.ResumeLayout(false)
        CType(Me.C1Mesages,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.pnlTasks,System.ComponentModel.ISupportInitialize).EndInit
        Me.pnlTasks.ResumeLayout(false)
        Me.pnlTasksContainer.ResumeLayout(false)
        Me.Panel12.ResumeLayout(False)
        CType(Me._flexGroup.Grid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me._flexGroup,System.ComponentModel.ISupportInitialize).EndInit
        Me.Panel9.ResumeLayout(false)
        Me.Panel9.PerformLayout
        Me.Panel10.ResumeLayout(false)
        Me.Panel10.PerformLayout
        CType(Me.pictureBox2,System.ComponentModel.ISupportInitialize).EndInit
        Me.pnlViewMoreTask.ResumeLayout(false)
        CType(Me.C1UserTasks,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.pnlNav_UnfinishedExams,System.ComponentModel.ISupportInitialize).EndInit
        Me.pnlNav_UnfinishedExams.ResumeLayout(false)
        Me.pnlNav_UnfinishedExams_Container.ResumeLayout(false)
        CType(Me.C1UnfinishedExam,System.ComponentModel.ISupportInitialize).EndInit
        Me.pnllblLinkUnfinishedexam.ResumeLayout(false)
        CType(Me.pnlNavigator,System.ComponentModel.ISupportInitialize).EndInit
        Me.pnlNavigator.ResumeLayout(false)
        CType(Me.pnlNav_Appointments,System.ComponentModel.ISupportInitialize).EndInit
        Me.pnlNav_Appointments.ResumeLayout(false)
        Me.pnlNav_AppointmentsContainer.ResumeLayout(false)
        CType(Me.C1Appointments,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.pnlNav_Calendar,System.ComponentModel.ISupportInitialize).EndInit
        Me.pnlNav_Calendar.ResumeLayout(false)
        CType(Me.pnlNav_Myday,System.ComponentModel.ISupportInitialize).EndInit
        Me.pnlNav_Myday.ResumeLayout(false)
        CType(Me.pnlNav_Triage,System.ComponentModel.ISupportInitialize).EndInit
        Me.pnlNav_Triage.ResumeLayout(false)
        Me.pnlNav_TriageContainer.ResumeLayout(false)
        CType(Me.C1Triage,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.pnlPatientDetail,System.ComponentModel.ISupportInitialize).EndInit
        Me.pnlPatientDetail.ResumeLayout(false)
        Me.pnlPatientDetailContainer.ResumeLayout(false)
        Me.pnlPatientDetails.ResumeLayout(false)
        CType(Me.C1dgPatientDetails,System.ComponentModel.ISupportInitialize).EndInit
        Me.pnlArchiveInfo.ResumeLayout(false)
        Me.pnlSearchFilter.ResumeLayout(false)
        Me.pnlCancelRx.ResumeLayout(false)
        Me.pnlCancelRx.PerformLayout
        Me.pnlCase.ResumeLayout(false)
        Me.pnlSpeciality.ResumeLayout(false)
        Me.Panel13.ResumeLayout(false)
        Me.Panel5.ResumeLayout(false)
        Me.Panel14.ResumeLayout(false)
        Me.Panel14.PerformLayout
        Me.Panel16.ResumeLayout(false)
        Me.Panel16.PerformLayout
        Me.pnlPatientDetailsHeaders.ResumeLayout(false)
        Me.pnlPatientDetailsHeaders.PerformLayout
        Me.ts_PatientDetails.ResumeLayout(false)
        Me.ts_PatientDetails.PerformLayout
        CType(Me.pnlTask,System.ComponentModel.ISupportInitialize).EndInit
        Me.pnlTask.ResumeLayout(false)
        CType(Me.pnlTasks_MYTask,System.ComponentModel.ISupportInitialize).EndInit
        Me.pnlTasks_MYTask.ResumeLayout(false)
        CType(Me.pnlTask_TaskRequests,System.ComponentModel.ISupportInitialize).EndInit
        Me.pnlTask_TaskRequests.ResumeLayout(false)
        CType(Me.pnlTask_RequestsSent,System.ComponentModel.ISupportInitialize).EndInit
        Me.pnlTask_RequestsSent.ResumeLayout(false)
        CType(Me.pnlGrPatientDemoCardsStatus,System.ComponentModel.ISupportInitialize).EndInit
        Me.pnlGrPatientDemoCardsStatus.ResumeLayout(false)
        CType(Me.pnlPatientDemographics,System.ComponentModel.ISupportInitialize).EndInit
        Me.pnlPatientDemographics.ResumeLayout(false)
        Me.pnlPatientDemographicsContainer.ResumeLayout(false)
        Me.pnlRightFill.ResumeLayout(false)
        Me.pnl_workphone.ResumeLayout(false)
        Me.pnl_Businesscenter.ResumeLayout(false)
        Me.pnl_Occupation.ResumeLayout(false)
        Me.pnl_MedicalCategory.ResumeLayout(false)
        Me.pnl_Language.ResumeLayout(false)
        Me.pnl_Ethinicity.ResumeLayout(false)
        Me.pnl_Race.ResumeLayout(false)
        Me.pnl_LabStatus.ResumeLayout(false)
        Me.Pnl_PatPhone.ResumeLayout(false)
        Me.Pnl_PCPComPhone.ResumeLayout(false)
        Me.Pnl_PCPPracPhone.ResumeLayout(false)
        Me.pnl_TertiaryInsurance.ResumeLayout(false)
        Me.pnl_SecondaryInsurance.ResumeLayout(false)
        Me.pnl_PrimaryInsurance.ResumeLayout(false)
        Me.Pnl_PCPBusPhone.ResumeLayout(false)
        Me.Pnl_Status.ResumeLayout(false)
        Me.Pnl_PCP.ResumeLayout(false)
        Me.Pnl_Pharmacy.ResumeLayout(false)
        Me.Pnl_PharmacyAddress.ResumeLayout(false)
        Me.Pnl_PharmacyPhone.ResumeLayout(false)
        Me.pnl_Provider.ResumeLayout(false)
        Me.Pnl_EmMobile.ResumeLayout(false)
        Me.Pnl_EmPhone.ResumeLayout(false)
        Me.Pnl_EmContact.ResumeLayout(false)
        Me.Pnl_Email.ResumeLayout(false)
        Me.Pnl_Fax.ResumeLayout(false)
        Me.Pnl_Home.ResumeLayout(false)
        Me.Pnl_Mobile.ResumeLayout(false)
        Me.Pnl_Gender.ResumeLayout(false)
        Me.pnlAddress.ResumeLayout(false)
        Me.pnlLeft.ResumeLayout(false)
        Me.pnlLeft.PerformLayout
        CType(Me.picConfidentialInfo,System.ComponentModel.ISupportInitialize).EndInit
        Me.pnlReconciliationAlert.ResumeLayout(false)
        Me.pnlReconciliationAlert.PerformLayout
        CType(Me.PicReconciliationAlert,System.ComponentModel.ISupportInitialize).EndInit
        Me.pnlPatientSavings.ResumeLayout(false)
        Me.pnlPatientSavings.PerformLayout
        CType(Me.picPatientSavings,System.ComponentModel.ISupportInitialize).EndInit
        Me.pnlPatientPhoto.ResumeLayout(false)
        Me.pnlPhotoBorder.ResumeLayout(false)
        CType(Me.picPD_Photo,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.pnlPatientCard,System.ComponentModel.ISupportInitialize).EndInit
        Me.pnlPatientCard.ResumeLayout(false)
        Me.pnlPatientCardContainer.ResumeLayout(false)
        Me.pnlMainPatientCardButton.ResumeLayout(false)
        Me.pnlPatientCardButtonContainer.ResumeLayout(false)
        Me.Panel8.ResumeLayout(false)
        CType(Me.picPC_Cards,System.ComponentModel.ISupportInitialize).EndInit
        Me.Panel7.ResumeLayout(false)
        Me.pnlPatientCardsButton.ResumeLayout(false)
        Me.pnlYESLAB.ResumeLayout(false)
        CType(Me.PictureBox3,System.ComponentModel.ISupportInitialize).EndInit
        Me.Panel6.ResumeLayout(false)
        CType(Me.pnlJPatientStatus,System.ComponentModel.ISupportInitialize).EndInit
        Me.pnlJPatientStatus.ResumeLayout(false)
        Me.pnlJPatientStatusContainer.ResumeLayout(false)
        Me.pnlPatientStatus.ResumeLayout(false)
        Me.pnlPAtientStaturEnvironment.ResumeLayout(false)
        Me.pnlPatientStatusGrid.ResumeLayout(false)
        CType(Me.c1PatientStatus,System.ComponentModel.ISupportInitialize).EndInit
        Me.pnlPatientStatusColor.ResumeLayout(false)
        Me.pnlPatientAlert.ResumeLayout(false)
        Me.pnlPatientAlert.PerformLayout
        Me.pnlPatientAlertHeader.ResumeLayout(false)
        CType(Me.uiPanel0,System.ComponentModel.ISupportInitialize).EndInit
        Me.uiPanel0.ResumeLayout(false)
        Me.Panel4.ResumeLayout(false)
        Me.pnlEligibilityCheck.ResumeLayout(false)
        CType(Me.c1EligibilityCheck,System.ComponentModel.ISupportInitialize).EndInit
        Me.pnlUnappliedCopay.ResumeLayout(false)
        CType(Me.c1UnappliedCopayAlert,System.ComponentModel.ISupportInitialize).EndInit
        Me.pnlCopayAlert.ResumeLayout(false)
        CType(Me.c1CopayAlert,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.c1Mails,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.pnlPatientDemo,System.ComponentModel.ISupportInitialize).EndInit
        Me.pnlPatientDemo.ResumeLayout(false)
        CType(Me.pnlPatientCards,System.ComponentModel.ISupportInitialize).EndInit
        Me.pnlPatientCards.ResumeLayout(false)
        Me.pnlPatientList.ResumeLayout(false)
        Me.pnlPatientListHeader.ResumeLayout(false)
        CType(Me.FileSystemWatcher1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.fsw_RecieveFAX,System.ComponentModel.ISupportInitialize).EndInit
        Me.pnlBirthDayReminder.ResumeLayout(false)
        Me.pnlBirthDayReminder.PerformLayout
        CType(Me.picBirthReminderClose,System.ComponentModel.ISupportInitialize).EndInit
        Me.pnlSurescriptAlert.ResumeLayout(false)
        Me.pnlsright.ResumeLayout(false)
        Me.Panel3.ResumeLayout(false)
        CType(Me.C1AlertMessages,System.ComponentModel.ISupportInitialize).EndInit
        Me.Panel18.ResumeLayout(false)
        Me.pnlsTop.ResumeLayout(false)
        Me.Panel2.ResumeLayout(false)
        CType(Me.pnlTAsk_TaskSentToMe,System.ComponentModel.ISupportInitialize).EndInit
        Me.pnlTAsk_TaskSentToMe.ResumeLayout(false)
        CType(Me.pnlTask_TaskSentByMe,System.ComponentModel.ISupportInitialize).EndInit
        Me.pnlTask_TaskSentByMe.ResumeLayout(false)
        CType(Me.pnlTask_MyTasks,System.ComponentModel.ISupportInitialize).EndInit
        Me.pnlTask_MyTasks.ResumeLayout(false)
        Me.cmnuToolStripCustomize.ResumeLayout(false)
        Me.cmnuPatientDetails.ResumeLayout(false)
        Me.cmnu_Tasks.ResumeLayout(false)
        Me.cmnu_messages.ResumeLayout(false)
        Me.pnlFormularyTransactionMessage.ResumeLayout(false)
        Me.pnlFormularyTransactionMessage.PerformLayout
        Me.pnlWait.ResumeLayout(false)
        Me.pnlWait.PerformLayout
        Me.ResumeLayout(false)

End Sub

    Friend WithEvents ToolTip As System.Windows.Forms.ToolTip
    Friend WithEvents pnlMainToolBar As System.Windows.Forms.Panel
    Friend WithEvents picLockScreen As System.Windows.Forms.PictureBox
    Friend WithEvents tlbStripMain As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents tlbbtn_NewPatient As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlbbtn_ModifyPatient As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlsPatRegSep As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tlbbtn_History As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlbbtn_Prescription As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlbbtn_Orders As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlbbtn_LabOrders As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlsMainSep As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tlbbtn_NewExam As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlbbtn_PastExam As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlbbtn_UnFinishedExams As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlsExamSep As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tlbbtn_Calender As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlsDOCmgntSep As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tlbbtn_ScanDocs As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlbbtn_FormGallery As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlbbtn_LockScreen As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlbbtn_Close As System.Windows.Forms.ToolStripButton
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents mnuFile As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_DashBoard As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuFileImport As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuFileImport_Genius As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuFileExit As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMasters As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMastersDrugs As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMaster_DrugProviderAssociation As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMastersSIG As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents sep2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuMastersTemplates As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMastersCategory As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMastersDMSCategory As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMastersRCMCategory As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents sep3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuMasterROS As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMastersHistory As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMastersOBPlan As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents sep4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuMastersContacts As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents sep5 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuMastersLabSetup As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMastersRadiology As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents sep8 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuMastersFlowsheet As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMastersSpeciality As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents sep6 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnu_DM_Setup As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_IM_Setup As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents sep7 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuMaster_SmartDiagnosis As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMaster_SmartTreatment As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMaster_FormGallery As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMaster_StatusUsers As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMaster_PatientSummaryScreen As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMaster_AppointmentBook As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuPatient As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuPatientRegistration As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuModifyPatient As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents sep10 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuPatientROS As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuPatientHistory As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuPatientVitals As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents sep11 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuPatientMessages As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuPatientPrescription As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents sep12 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuPatient_RadiologyOrders As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuPatient_LabOrder As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents sep13 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuPatientLetters As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuPatientPTProtocols As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuPatientConcent As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuNurseNotes As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents sep14 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuPatientFlowSheet As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuPatient_FormGallery As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuPatient_Tasks As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents sep15 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnu_IM_Transaction As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuProblemList As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuPatient_FindHealthPlan As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_UploadVideo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuView As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuView_PatientVitals As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuView_OBVitals As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuView_Tasks As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuView_Messages As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuView_PatientEducation As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuView_FormGallery As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuView_Referrals As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuView_PatientLetters As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuView_PTProtocol As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuView_PatientConcent As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuView_NurseNotes As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuView_PatientVideo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuViewReceivedFaxes As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuViewPatientSummaryScreen As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuTools As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuSettings As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuTools_DefaultDisplaySettings As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents sep16 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuTools_Customization As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents sep17 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuToolsVoiceCenter As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuDoctorSpeakerConfiguration As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents sep18 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuExportTemplates As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuImportTemplates As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuUpdateExistingTemplates As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuTool_UpdateOtherTemplates As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuTool_UpgradeTemplates As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents sep19 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuMergePatRecords As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents sep20 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuUnlockExams As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuImportVitalGraphData As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_TimeSynchronization As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents sep21 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuToolsToolbar As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuToolsStatusbar As System.Windows.Forms.ToolStripMenuItem
    'Recover Exam
    Friend WithEvents mnuTools_RecoverExam As System.Windows.Forms.ToolStripMenuItem


    Friend WithEvents mnuCustomLink As System.Windows.Forms.ToolStripMenuItem

    Friend WithEvents mnuUpdateExam As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuUpdateTemplates As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuReports As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuReportUnfinishedExam As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuSep_UnfinishedExam As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuFAXReports As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuFAXReport As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuPendingFAXesWithMaxAttempts As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuPendingFAXesWithoutTIFF As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuReportExamPrintFAX As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuReviewExams As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuBatchReferrals As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents sep23 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuPatientDemographics As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnurptHCFAReport As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuRptDignosisLabResult As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuRpt_HealthPlan As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents sep24 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnu_DM_Rpt_DueGuideline As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuRptIMDueReport As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuRpt_LabGraph As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuWindow As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuHelp As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuSupport As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuAboutUs As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents pnlMainMenu As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents pnlMainStatusBar As System.Windows.Forms.Panel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Private WithEvents uiPanelManager1 As Janus.Windows.UI.Dock.UIPanelManager
    Friend WithEvents pnlNavigator As Janus.Windows.UI.Dock.UIPanelGroup
    Friend WithEvents pnlNav_Appointments As Janus.Windows.UI.Dock.UIPanel
    Friend WithEvents pnlNav_AppointmentsContainer As Janus.Windows.UI.Dock.UIPanelInnerContainer
    Friend WithEvents pnlNav_Calendar As Janus.Windows.UI.Dock.UIPanel
    Friend WithEvents pnlNav_CalendarContainer As Janus.Windows.UI.Dock.UIPanelInnerContainer
    Friend WithEvents pnlPatientDetail As Janus.Windows.UI.Dock.UIPanel
    Friend WithEvents pnlPatientDetailContainer As Janus.Windows.UI.Dock.UIPanelInnerContainer
    Friend WithEvents pnlPatientList As System.Windows.Forms.Panel
    'Friend WithEvents tmrFaxRecievedclose As System.Windows.Forms.Timer
    Friend WithEvents cmnuProblemList As System.Windows.Forms.ContextMenu
    Friend WithEvents mnuAddProblemList As System.Windows.Forms.MenuItem
    Friend WithEvents ChartPull As System.Windows.Forms.ContextMenu
    Friend WithEvents cmnuShortCut As System.Windows.Forms.ContextMenu
    Friend WithEvents cmnuTasks As System.Windows.Forms.ContextMenu
    Friend WithEvents cmnuTask_Delete As System.Windows.Forms.MenuItem
    Friend WithEvents cmnuTask_Complete As System.Windows.Forms.MenuItem
    Friend WithEvents cmnuTask_Add As System.Windows.Forms.MenuItem
    Friend WithEvents FileSystemWatcher1 As System.IO.FileSystemWatcher
    'Friend WithEvents tmrpnlFaxReminder As System.Windows.Forms.Timer
    Friend WithEvents fsw_RecieveFAX As System.IO.FileSystemWatcher
    Friend WithEvents timerLockScreen As System.Windows.Forms.Timer
    Friend WithEvents tmrBirthDayReminder As System.Windows.Forms.Timer
    'Friend WithEvents ColorDialog1 As System.Windows.Forms.ColorDialog
    Friend WithEvents tmrshowsurepnl As System.Windows.Forms.Timer
    Friend WithEvents tmrsurescriptAlert As System.Windows.Forms.Timer
    Friend WithEvents ToolTip2 As System.Windows.Forms.ToolTip
    Friend WithEvents tmrMessages As System.Windows.Forms.Timer
    Friend WithEvents pnlBirthDayReminder As System.Windows.Forms.Panel
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents picBirthReminderClose As System.Windows.Forms.PictureBox
    Friend WithEvents lblBirthDayMessage As System.Windows.Forms.Label
    Friend WithEvents pnlSurescriptAlert As System.Windows.Forms.Panel
    Friend WithEvents pnlsright As System.Windows.Forms.Panel
    Friend WithEvents C1AlertMessages As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents Panel18 As System.Windows.Forms.Panel
    Friend WithEvents Panel19 As System.Windows.Forms.Panel
    Friend WithEvents pnlsTop As System.Windows.Forms.Panel
    Friend WithEvents btnSRefresh As System.Windows.Forms.Button
    Friend WithEvents btnSurescriptClose As System.Windows.Forms.Button
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents cMnuPatient As System.Windows.Forms.ContextMenuStrip
    'Friend WithEvents cmnuPatient_NewExam As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents pnlTask As Janus.Windows.UI.Dock.UIPanelGroup
    Friend WithEvents pnlTAsk_TaskSentToMe As Janus.Windows.UI.Dock.UIPanel
    Friend WithEvents pnlTAsk_TaskSentToMeContainer As Janus.Windows.UI.Dock.UIPanelInnerContainer
    Friend WithEvents pnlTask_TaskSentByMe As Janus.Windows.UI.Dock.UIPanel
    Friend WithEvents pnlTask_TaskSentByMeContainer As Janus.Windows.UI.Dock.UIPanelInnerContainer
    Friend WithEvents pnlTask_MyTasks As Janus.Windows.UI.Dock.UIPanel
    Friend WithEvents pnlTask_MyTasksContainer As Janus.Windows.UI.Dock.UIPanelInnerContainer
    Friend WithEvents pnlTasks_MYTask As Janus.Windows.UI.Dock.UIPanel
    Friend WithEvents pnlTasks_MYTaskContainer As Janus.Windows.UI.Dock.UIPanelInnerContainer
    Friend WithEvents pnlTask_TaskRequests As Janus.Windows.UI.Dock.UIPanel
    Friend WithEvents pnlTask_TaskRequestsContainer As Janus.Windows.UI.Dock.UIPanelInnerContainer
    Friend WithEvents pnlTask_RequestsSent As Janus.Windows.UI.Dock.UIPanel
    Friend WithEvents pnlTask_RequestsSentContainer As Janus.Windows.UI.Dock.UIPanelInnerContainer
    Friend WithEvents oPatientListControl As gloPatient.PatientListControl
    Friend WithEvents pnlPatientDetails As System.Windows.Forms.Panel
    Friend WithEvents pnlPatientDetailsHeaders As System.Windows.Forms.Panel
    Friend WithEvents ts_PatientDetails As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents tsbtn_History As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlsHistorySep As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tsbtn_Medication As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlsMedSep As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tsbtn_Prescription As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlsRxSep As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tsbtn_ProblemList As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlsProblemSep As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tsbtn_ViewDocs As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlsViewDocsSep As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tsbtn_Orders As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlsOrdersSep As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tsbtn_Messages As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlsMessageSep As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tsbtn_PastExam As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlsPastExamSep As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tsbtn_Vital As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlsNewExamSep As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tsbtn_PendingFax As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlsPendingFaxSep As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tsbtn_Sentfax As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlsSentFaxSep As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tsbtn_AuditTrail As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbtn_Selected As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbtn_Hover As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbtn_Normal As System.Windows.Forms.ToolStripButton
    Friend WithEvents mnuTools_CardScanner As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tlbbtn_ScanCard As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlsCalenderSep As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents pnlPatientListHeader As System.Windows.Forms.Panel
    Friend WithEvents lblPatientListHeader As System.Windows.Forms.Label
    Friend WithEvents pnlPatientDemo As Janus.Windows.UI.Dock.UIPanel
    Friend WithEvents pnlPatientDemoContainer As Janus.Windows.UI.Dock.UIPanelInnerContainer
    Friend WithEvents pnlPatientCards As Janus.Windows.UI.Dock.UIPanel
    Friend WithEvents pnlPatientCardsContainer As Janus.Windows.UI.Dock.UIPanelInnerContainer


    Friend WithEvents pnlPatientCard As Janus.Windows.UI.Dock.UIPanel
    Friend WithEvents pnlPatientCardContainer As Janus.Windows.UI.Dock.UIPanelInnerContainer
    Friend WithEvents pnlPatientDemographics As Janus.Windows.UI.Dock.UIPanel
    Friend WithEvents pnlPatientDemographicsContainer As Janus.Windows.UI.Dock.UIPanelInnerContainer
    Friend WithEvents pnlGrPatientDemoCardsStatus As Janus.Windows.UI.Dock.UIPanelGroup
    Friend WithEvents pnlJPatientStatus As Janus.Windows.UI.Dock.UIPanel
    Friend WithEvents pnlJPatientStatusContainer As Janus.Windows.UI.Dock.UIPanelInnerContainer
    Friend WithEvents pnlPatientAlert As System.Windows.Forms.Panel
    Private WithEvents txtPatientAlert As System.Windows.Forms.TextBox
    Friend WithEvents pnlPatientAlertHeader As System.Windows.Forms.Panel
    Friend WithEvents btnModifyPatientAlert As System.Windows.Forms.Button
    Friend WithEvents btnClosePatientAlert As System.Windows.Forms.Button
    Private WithEvents pnlPatientCardsButton As System.Windows.Forms.Panel
    Private WithEvents label24 As System.Windows.Forms.Label
    Private WithEvents btnPC_DeleteCard As System.Windows.Forms.Button
    Private WithEvents label22 As System.Windows.Forms.Label
    Private WithEvents btnPC_PrintCards As System.Windows.Forms.Button
    Private WithEvents btnPC_MoveLast As System.Windows.Forms.Button
    Private WithEvents label21 As System.Windows.Forms.Label
    Private WithEvents btnPC_MoveNext As System.Windows.Forms.Button
    Private WithEvents label20 As System.Windows.Forms.Label
    Private WithEvents btnPC_MovePrevious As System.Windows.Forms.Button
    Private WithEvents Label14 As System.Windows.Forms.Label
    Private WithEvents btnPC_MoveFirst As System.Windows.Forms.Button
    Private WithEvents Label15 As System.Windows.Forms.Label
    Private WithEvents Label16 As System.Windows.Forms.Label
    Private WithEvents picPC_Cards As System.Windows.Forms.PictureBox
    Friend WithEvents lblPD_Name As System.Windows.Forms.Label
    Friend WithEvents lblPD_Code As System.Windows.Forms.Label
    Friend WithEvents lblPD_DateOfBirth As System.Windows.Forms.Label
    Friend WithEvents lblPD_Email As System.Windows.Forms.Label
    Friend WithEvents lblPD_Fax As System.Windows.Forms.Label
    Friend WithEvents lblPD_HomePhone As System.Windows.Forms.Label
    Friend WithEvents lblPD_Mobile As System.Windows.Forms.Label
    Friend WithEvents lblPD_Address As System.Windows.Forms.Label
    Private WithEvents Label7 As System.Windows.Forms.Label
    Private WithEvents Label8 As System.Windows.Forms.Label
    Private WithEvents Label9 As System.Windows.Forms.Label
    Private WithEvents label23 As System.Windows.Forms.Label
    Private WithEvents picPD_Photo As System.Windows.Forms.PictureBox
    Private WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents pnlPatientCardButtonContainer As System.Windows.Forms.Panel
    Private WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Private WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents Label38 As System.Windows.Forms.Label
    Friend WithEvents cmnuToolStripCustomize As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents CustomizeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents lblPD_Pharmacy As System.Windows.Forms.Label
    Private WithEvents Label40 As System.Windows.Forms.Label
    Private WithEvents Label39 As System.Windows.Forms.Label
    Private WithEvents lbl_Emcontact As System.Windows.Forms.Label
    Friend WithEvents tsbtn_Insurance As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tsbtn_Billing As System.Windows.Forms.ToolStripButton
    Friend WithEvents StatusStrip As System.Windows.Forms.StatusStrip
    Friend WithEvents sslbl_Login As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents sslbl_Version As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents sslbl_VoiceInfo As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents sslbl_LastModifiedDate As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents pnlPatientStatus As System.Windows.Forms.Panel
    Private WithEvents Label42 As System.Windows.Forms.Label
    'Friend WithEvents pnlJPatientStatus As Janus.Windows.UI.Dock.UIPanel
    Private WithEvents Label41 As System.Windows.Forms.Label
    Friend WithEvents lblENV_06 As System.Windows.Forms.Label
    Friend WithEvents lblENV_05 As System.Windows.Forms.Label
    Friend WithEvents lblENV_04 As System.Windows.Forms.Label
    Friend WithEvents lblENV_03 As System.Windows.Forms.Label
    Friend WithEvents lblENV_02 As System.Windows.Forms.Label
    Friend WithEvents lblENV_01 As System.Windows.Forms.Label
    Friend WithEvents cmnuPatientDetails As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents Label43 As System.Windows.Forms.Label
    Private WithEvents imgList_Common As System.Windows.Forms.ImageList
    Private WithEvents cmnu_Tasks As System.Windows.Forms.ContextMenuStrip
    Private WithEvents cmnuItem_Priority As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents cmunItem_Completed As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents cmnuItem_0Percent As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents cmnuItem_25Percent As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents cmnuItem_50Percent As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents cmnuItem_75Percent As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents cmnuItem_100Percent As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuItem_TaskSeparator As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cmnuItem_NewTask As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents c1Mails As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents mnuView_Mails As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator6 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tsbtn_Labs As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tsbtn_NurseNotes As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbtn_PatientConsent As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbtn_DisclosureMgt As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator7 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator8 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator15 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents pnlPhotoBorder As System.Windows.Forms.Panel
    Friend WithEvents pnlPAtientStaturEnvironment As System.Windows.Forms.Panel
    Friend WithEvents tsbtn_PatientLetters As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator19 As System.Windows.Forms.ToolStripSeparator
    Private WithEvents lblPD_Gender As System.Windows.Forms.Label
    Private WithEvents Label46 As System.Windows.Forms.Label
    Friend WithEvents tlbbtn_Microphone As System.Windows.Forms.ToolStripButton
    Friend WithEvents sslbl_CurrentSpeaker As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents mnuRefillRequest As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuVwErrorMessages As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMaster_SmartOrder As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMaster_ICDCPTGallery As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMastersDisclosuerSet As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents sep9 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuDisclosureMgmt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tlsFormGalerySep As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuView_DisclosureMgmt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuOutStandingOrders As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents sep22 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnu_ViewPendingLabOrders As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuDicomViewer As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuLiquidData As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_ViewPatientSynopsis As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuImportCCD As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuGenerateCCD As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuView_PatientConfidential As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents picConfidentialInfo As System.Windows.Forms.PictureBox
    Friend WithEvents mnu_BillingCharges As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_BillingBatch As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_BillingPayment As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_BillingBalance As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_BillingLedger As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_BillingRemittance As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents Label45 As System.Windows.Forms.Label
    Friend WithEvents cmnuTask_AcceptTask As System.Windows.Forms.MenuItem
    Friend WithEvents cmnuItem_AcceptTask As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_BillingConfiguration As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuRpt_PatientReminderLetters As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_CV_Setup As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_TaxID_Setup As System.Windows.Forms.ToolStripMenuItem
    'Friend WithEvents cmnuPatientItem_CheckIn As System.Windows.Forms.ToolStripMenuItem
    'Friend WithEvents cmnuPatientItem_CheckOut As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents c1PatientStatus As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents pnlPatientStatusColor As System.Windows.Forms.Panel
    Friend WithEvents pnlPatientStatusGrid As System.Windows.Forms.Panel
    Friend WithEvents mnuView_CardioVascularRisk As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuView_PatientChiefComplaints As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tlbbtn_PatientSynopsis As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator9 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents C1dgPatientDetails As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents pnlNav_Triage As Janus.Windows.UI.Dock.UIPanel
    Friend WithEvents pnlNav_TriageContainer As Janus.Windows.UI.Dock.UIPanelInnerContainer
    Friend WithEvents mnuView_Triage As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents C1Triage As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents C1Mesages As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents mnuViewCCDFiles As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents pnlNav_Myday As Janus.Windows.UI.Dock.UIPanel
    Friend WithEvents pnlNav_MydayContainer As Janus.Windows.UI.Dock.UIPanelInnerContainer
    Private WithEvents Label51 As System.Windows.Forms.Label
    Private WithEvents Label52 As System.Windows.Forms.Label
    Private WithEvents Label53 As System.Windows.Forms.Label
    Private WithEvents Label48 As System.Windows.Forms.Label
    Private WithEvents Label49 As System.Windows.Forms.Label
    Private WithEvents Label50 As System.Windows.Forms.Label
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents C1Appointments As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents lblPD_Age As System.Windows.Forms.Label
    Friend WithEvents C1UnfinishedExam As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents pnlPatientCheckInStatus As Janus.Windows.UI.Dock.UIPanel
    Friend WithEvents pnlPatientCheckInStatusContainer As Janus.Windows.UI.Dock.UIPanelInnerContainer
    Friend WithEvents c1PatientCheckInStatus As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents Vcmd As AxDNSTools.AxDgnVoiceCmd
    Friend WithEvents DgnMicBtn1 As AxDNSTools.AxDgnMicBtn
    Friend WithEvents DgnEngineControl1 As AxDNSTools.AxDgnEngineControl
    Friend WithEvents Imgts_PatientDetails As System.Windows.Forms.ImageList
    Friend WithEvents tsbtn_EligbilityInfo As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlbbtn_RxHub As System.Windows.Forms.ToolStripButton
    Private WithEvents cmnu_messages As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ToolStripSeparator10 As System.Windows.Forms.ToolStripSeparator
    Private WithEvents lbl_Patientstatus As System.Windows.Forms.Label
    Friend WithEvents lbl_ShowStatus As System.Windows.Forms.Label
    Friend WithEvents mnu_ClearPatientDocuments As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_ScanDocs As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents pnlRightFill As System.Windows.Forms.Panel
    Friend WithEvents pnlLeft As System.Windows.Forms.Panel
    Friend WithEvents Pnl_Email As System.Windows.Forms.Panel
    Friend WithEvents Pnl_Fax As System.Windows.Forms.Panel
    Friend WithEvents Pnl_Home As System.Windows.Forms.Panel
    Friend WithEvents Pnl_Mobile As System.Windows.Forms.Panel
    Friend WithEvents Pnl_Gender As System.Windows.Forms.Panel
    Friend WithEvents pnlAddress As System.Windows.Forms.Panel
    Friend WithEvents Pnl_EmMobile As System.Windows.Forms.Panel
    Friend WithEvents Pnl_EmPhone As System.Windows.Forms.Panel
    Friend WithEvents Pnl_EmContact As System.Windows.Forms.Panel
    Friend WithEvents Pnl_Status As System.Windows.Forms.Panel
    Private WithEvents Label57 As System.Windows.Forms.Label
    Friend WithEvents lbl_EmMobile As System.Windows.Forms.Label
    Friend WithEvents Pnl_PCP As System.Windows.Forms.Panel
    Friend WithEvents Pnl_Pharmacy As System.Windows.Forms.Panel
    Private WithEvents Label47 As System.Windows.Forms.Label
    Friend WithEvents lbl_EmPhone As System.Windows.Forms.Label
    Private WithEvents Label60 As System.Windows.Forms.Label
    Private WithEvents lblPD_PriCarePhysician As System.Windows.Forms.Label
    Private WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents tlbbtn_Vitals As System.Windows.Forms.ToolStripButton
    Friend WithEvents pnlPatientPhoto As System.Windows.Forms.Panel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents tsbtn_Appointments As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlbbtn_Sechedule As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator11 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tsbtn_Balance As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnu_Appointment As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_Schedule As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Pnl_PCPBusPhone As System.Windows.Forms.Panel
    Friend WithEvents lbl_ShowPCPbusPhone As System.Windows.Forms.Label
    Private WithEvents lbl_PCPbusPhone As System.Windows.Forms.Label
    Friend WithEvents Pnl_PCPComPhone As System.Windows.Forms.Panel
    Friend WithEvents lbl_ShowPCPCpmPhone As System.Windows.Forms.Label
    Private WithEvents lbl_PCPComPhone As System.Windows.Forms.Label
    Friend WithEvents Pnl_PCPPracPhone As System.Windows.Forms.Panel
    Friend WithEvents lbl_ShowPCPPracPhone As System.Windows.Forms.Label
    Private WithEvents lbl_PCPPracPhone As System.Windows.Forms.Label
    Friend WithEvents mnu_TemplateAssociation As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_ViewDocument As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents pnlArchiveInfo As System.Windows.Forms.Panel
    Friend WithEvents lblInfo As System.Windows.Forms.Label
    Friend WithEvents mnu_BillingAdvPayment As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_TaskMailConfiguration As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_ViewSchedule As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuVwHL7MessageQueue As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents mnu_MISReports As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents mnu_MISReports_PatientPaymentHistory As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents mnu_MISReports_PatientTransactionHistory As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents mnu_MISReports_Patientstatement As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents ToolStripSeparator12 As System.Windows.Forms.ToolStripSeparator
    Private WithEvents mnu_MISReports_ProductionByDoctor As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents mnu_MISReports_ProductionByFacility As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents mnu_MISReports_ProductionByDate As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents mnu_MISReports_ProductionByMonth As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents mnu_MISReports_ProductionByProcedureGroup As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents mnu_MISReports_ProductionByProcedureCode As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents mnu_MISReports_ProductionByInsuranceCarrier As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents mnu_MISReports_ProductionByFacilityByPatient As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents mnu_MISReports_ProductionByFacilityByPatientDetail As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents toolStripSeparator16 As System.Windows.Forms.ToolStripSeparator
    Private WithEvents mnu_MISReports_ReimbursementByMonth As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents mnu_MISReports_ReimbursementByMonthDetail As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents mnu_MISReports_ReimbursementByInsuranceCarrier As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents mnu_MISReports_ReimbursementByInsuranceByCPT As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents mnu_MISReports_ReimbursementByCPTByInsurance As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents mnu_MISReports_ReimbursementByDoctorByInsurance As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents mnu_MISReports_ReimbursementByInsuranceForCPT As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents mnu_MISReports_ReimbursementDetailsByAccount As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents toolStripSeparator26 As System.Windows.Forms.ToolStripSeparator
    Private WithEvents mnu_MISReports_AgingAnalysis As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents mnu_MISReports_AgingSummaryByPatient As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents mnu_MISReports_AgingSummaryByInsuranceCarrier As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents toolStripSeparator27 As System.Windows.Forms.ToolStripSeparator
    Private WithEvents mnu_MISReports_ProductionByPhysicianGroup As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents mnu_MISReports_ProductionAnalysisByFacility As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents mnu_MISReports_ProductionAnalysisByprocedureGroup As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents mnu_MISReports_ProductionAnalysisandTrendsByMonth As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents mnu_MISReports_ProductionTrendsByProcedureGrop As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuTools_CardImage As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsbtn_Referrals As System.Windows.Forms.ToolStripButton
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Private WithEvents pnlUnappliedCopay As System.Windows.Forms.Panel
    Private WithEvents c1UnappliedCopayAlert As C1.Win.C1FlexGrid.C1FlexGrid
    Private WithEvents pnlCopayAlert As System.Windows.Forms.Panel
    Private WithEvents c1CopayAlert As C1.Win.C1FlexGrid.C1FlexGrid
    Private WithEvents pnlEligibilityCheck As System.Windows.Forms.Panel
    Private WithEvents c1EligibilityCheck As C1.Win.C1FlexGrid.C1FlexGrid
    Private WithEvents tmrCopayAlert As System.Windows.Forms.Timer
    Friend WithEvents ToolStripSeparator13 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuView_Triage1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ZipToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuVital As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuTableTemplate As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuVitalNorms As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuOBVital As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuTools_PrescriptionProviderAssociation As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_ClosedJournals As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuRights As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsbtn_Triage As System.Windows.Forms.ToolStripButton
    Friend WithEvents AddTriageToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsbtn_PatientEducation As System.Windows.Forms.ToolStripButton
    Friend WithEvents sslbl_MessagePriority As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents pnlFormularyTransactionMessage As System.Windows.Forms.Panel
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents lblFormularyTransactionMessage As System.Windows.Forms.Label
    Friend WithEvents sslbl_SQLServerDatabase As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents uiPanel0 As Janus.Windows.UI.Dock.UIPanel
    Friend WithEvents uiPanel0Container As Janus.Windows.UI.Dock.UIPanelInnerContainer
    Friend WithEvents pnlLeft_Nav As Janus.Windows.UI.Dock.UIPanelGroup
    Friend WithEvents pnl_Messages As Janus.Windows.UI.Dock.UIPanel
    Friend WithEvents pnl_MessagesContainer As Janus.Windows.UI.Dock.UIPanelInnerContainer
    Friend WithEvents pnlTasks As Janus.Windows.UI.Dock.UIPanel
    Friend WithEvents pnlTasksContainer As Janus.Windows.UI.Dock.UIPanelInnerContainer
    Friend WithEvents mnuVwDeniedRefillReq As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuVwDeniedChangeReq As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuRxFillNotifications As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuView_PatientTemplates As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuPatientEducation As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Pnl_PatPhone As System.Windows.Forms.Panel
    Friend WithEvents lb_PatPhone As System.Windows.Forms.Label
    Private WithEvents Label56 As System.Windows.Forms.Label
    Private WithEvents Label55 As System.Windows.Forms.Label
    Private WithEvents btnPC_ScanCard As System.Windows.Forms.Button
    Friend WithEvents mnurpt_PatientICD9CPT As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PrintDialog1 As System.Windows.Forms.PrintDialog
    Friend WithEvents printDoc As System.Drawing.Printing.PrintDocument
    Friend WithEvents CmnuPatientstatus As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents Label58 As System.Windows.Forms.Label
    Friend WithEvents Label36 As System.Windows.Forms.Label
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents Label59 As System.Windows.Forms.Label
    Friend WithEvents CCHIT11ReportToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMUDashboardMainMenu As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuReportPatientsAlerts As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuReportICD9Rx As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tlbbtn_gloLabOrders As System.Windows.Forms.ToolStripButton
    Friend WithEvents mnuReportClinicalDecision As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuReportBPMeasurement As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuReportPatientHistory As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuPatientBMIReport As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuPatientRxReport As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tlbbtn_GenerateCCD As System.Windows.Forms.ToolStripButton
    Friend WithEvents cmnuItem_RejectTask As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuItem_CompleteTask As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuItem_TrackTasks As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuPatientVitalUsageReport As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsbtn_NewExam As System.Windows.Forms.ToolStripButton
    Friend WithEvents mnuAllergyUsageReport As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuProblemUsageReport As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuDemographicUsageReport As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMedicationUsagereport As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuEprescribingreport As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuHistoryUsageReport As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsbtn_PriorAuthorization As System.Windows.Forms.ToolStripButton
    Friend WithEvents pnlNav_UnfinishedExams As Janus.Windows.UI.Dock.UIPanel
    Friend WithEvents pnlNav_UnfinishedExams_Container As Janus.Windows.UI.Dock.UIPanelInnerContainer
    Friend WithEvents mnuUnfinishedReconciliationLists As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuPatientReminder As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CustomizableReportsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuChangePass As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_Patient_list_report As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem3 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents HealthVaultToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuBatchPrintTemplates As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tlbbtn_Timeline As System.Windows.Forms.ToolStripButton
    Friend WithEvents pnlMainPatientCardButton As System.Windows.Forms.Panel
    Private WithEvents Label71 As System.Windows.Forms.Label
    Private WithEvents Label72 As System.Windows.Forms.Label
    Private WithEvents Label73 As System.Windows.Forms.Label
    Private WithEvents Label74 As System.Windows.Forms.Label
    Friend WithEvents pnlYESLAB As System.Windows.Forms.Panel
    Friend WithEvents PictureBox3 As System.Windows.Forms.PictureBox
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Private WithEvents btnYesClose As System.Windows.Forms.Button
    Private WithEvents Label17 As System.Windows.Forms.Label
    Private WithEvents Label27 As System.Windows.Forms.Label
    Private WithEvents Label25 As System.Windows.Forms.Label
    Private WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents pnl_LabStatus As System.Windows.Forms.Panel
    Friend WithEvents lbl_ShowLabStatus As System.Windows.Forms.Label
    Private WithEvents lbl_LabStatus As System.Windows.Forms.Label
    Friend WithEvents Pnl_PharmacyPhone As System.Windows.Forms.Panel
    Private WithEvents lblPD_PharmacyPhone As System.Windows.Forms.Label
    Private WithEvents Label61 As System.Windows.Forms.Label
    Friend WithEvents HealthVaultSendRequestAccess As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuGeneralMessageQueue As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuVwEARdata As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuRpt_LabManifest As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents pnlSearchFilter As System.Windows.Forms.Panel
    Friend WithEvents Panel13 As System.Windows.Forms.Panel
    Friend WithEvents cmbProvider As System.Windows.Forms.ComboBox
    Friend WithEvents lblProvider As System.Windows.Forms.Label
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents chkGetLatestActive As System.Windows.Forms.CheckBox
    Friend WithEvents Panel14 As System.Windows.Forms.Panel
    Friend WithEvents cmbStatus As System.Windows.Forms.ComboBox
    Friend WithEvents lblStatus As System.Windows.Forms.Label
    Friend WithEvents Panel16 As System.Windows.Forms.Panel
    Friend WithEvents dtpToDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblto As System.Windows.Forms.Label
    Friend WithEvents dtpFromDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents chkenbdate As System.Windows.Forms.CheckBox
    Friend WithEvents lblFrom As System.Windows.Forms.Label
    Friend WithEvents btnReset As System.Windows.Forms.Button
    Friend WithEvents Label54 As System.Windows.Forms.Label
    Friend WithEvents Label62 As System.Windows.Forms.Label
    Friend WithEvents Label37 As System.Windows.Forms.Label
    Friend WithEvents MnuClinicalChart As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BackgroundWorker_MessageTimer As System.ComponentModel.BackgroundWorker
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Private WithEvents lblScandate As System.Windows.Forms.Label
    Friend WithEvents BackgroundWorker_InitiliseFaxSetting As System.ComponentModel.BackgroundWorker
    Private WithEvents Label63 As System.Windows.Forms.Label
    Friend WithEvents Panel8 As System.Windows.Forms.Panel
    Friend WithEvents ToolStripMenuItem2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuMUDashboard As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMUDashboard_Stage2 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMUDashboard_Stage1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents QualityMeasureDashboardToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CloseToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents HelpComponent1 As gloEMR.Help.HelpComponent
    Friend WithEvents mnuUserGuide As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuSearch As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ContentsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator14 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tsbtn_PatientTasks As System.Windows.Forms.ToolStripButton
    Friend WithEvents MessageQueueToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents InterfaceReportsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PatientActivationReportsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuItem_CompleteAll As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuItem_TaskTake As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsbtn_PatientCases As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlbbtnSaveCDS As System.Windows.Forms.ToolStripButton
    Friend WithEvents mnuViewCDSFiles As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents ReviewIntuitPatientsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuSharepoint As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ConnectCommunityToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents JoinExistingGroupToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CreateNewGroupToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CreateProfileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents NewProfileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ModiyProfileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MyCommunityToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ShareToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem22 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuShareTemplate As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuShareTemplateUpload As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuShareTemplateDownload As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuShareLiquid As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuShareLiquidDataUplaod As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuShareLiquidDataDownload As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuShareHistory As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuShareHistoryUpload As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuShareHistoryDownload As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem6 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuShareUploadFlowsheet As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuShareddlFlowsheet As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem7 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuShareDmSetupUpload As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuShareDmSetupDownload As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem8 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuShareUploadIMSetup As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuShareDownloadIMSetup As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem9 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuShareUploadCVSetup As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuShareDownloadCVSetup As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuShareSmartDx As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuShareUploadSmartDx As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuShareDownloadSmartDx As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuShareShareSmartDx As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuShareUploadSmTreatment As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuShareDownloadSmTreatment As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem12 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuShareUploadSmOrder As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuShareDownloadSmOrder As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem13 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuShareUploadformglry As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuShareDlformglry As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem15 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnushupappconf As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnushdlappconf As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem16 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnushupblconf As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnushdlblconf As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnushtaskmail As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnushuptaskmail As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnushdntaskmail As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuSPReports As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnutemplate As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents LiquidDataToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents HistoryToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FlowsheetToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DMSetupToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents IMSetupToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CVSetupToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuSmartDiag As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SmartTreatmentToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SmartOrderToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FormGalleryToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PatientSummeryToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AppointmentConfigurationToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BillingConfigurationToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TaskMailConfigurationToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents GloSkypeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents VoiceCallToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents VideoCallToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TextMessageToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ConferenceToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CommunityTimeLineToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents JoinGloTimeLineToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PatientCollaborationToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CareExchangeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents GloTimeLineDashboardToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SelectedPatientToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MultiplePatientsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ClinicalExchangeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem19 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem18 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MicrosoftHealthValtToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem20 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem21 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SurescriptsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents GloAnalyticsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents HowAmIDoingToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents HowAreMyPatientsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents C1SuperTooltip1 As C1.Win.C1SuperTooltip.C1SuperTooltip
    Friend WithEvents mnuTools_DirectInbox As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tlbbtn_Inbox As System.Windows.Forms.ToolStripButton
    Friend WithEvents mnuRpt_VaccineInventoryReport As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuTools_SendSecureMessage As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsbtn_IntuitCommunication As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbtn_PatientCommunication As System.Windows.Forms.ToolStripButton
    Friend WithEvents txtSearchIntuit As System.Windows.Forms.TextBox
    Friend WithEvents btnIntuitMsg As System.Windows.Forms.Button
    Friend WithEvents mnuChangePwd As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents pnlSpeciality As System.Windows.Forms.Panel
    Friend WithEvents cmbTemplateSpeciality As System.Windows.Forms.ComboBox
    Friend WithEvents Label64 As System.Windows.Forms.Label
    Friend WithEvents Label44 As System.Windows.Forms.Label
    Friend WithEvents mnuMastersFamilyMember As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuVwRECList As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuViewAllCCDCCRFiles As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuUnfinishedFiles As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PicReconciliationAlert As System.Windows.Forms.PictureBox
    Friend WithEvents pnlReconciliationAlert As System.Windows.Forms.Panel
    Friend WithEvents mnu_ViewRecommendation As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuTools_SecureMsg As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuRpt_OpenRecommendations As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents pnl_Language As System.Windows.Forms.Panel
    Friend WithEvents lblLanguage As System.Windows.Forms.Label
    Private WithEvents Label66 As System.Windows.Forms.Label
    Friend WithEvents pnl_Ethinicity As System.Windows.Forms.Panel
    Friend WithEvents lblEthinicity As System.Windows.Forms.Label
    Private WithEvents Label68 As System.Windows.Forms.Label
    Friend WithEvents pnl_Race As System.Windows.Forms.Panel
    Friend WithEvents lblRace As System.Windows.Forms.Label
    Private WithEvents Label67 As System.Windows.Forms.Label
    Friend WithEvents mnuClinicalInstruction As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuCarePlan As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuEducationMapping As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuGenerateCDA As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tlbbtn_GenerateCDA As System.Windows.Forms.ToolStripButton
    Friend WithEvents mnuViewAmendments As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents lnklblAmendmentsAlert As System.Windows.Forms.LinkLabel
    Friend WithEvents mnuTools_ExportSummary As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuTools_CCDASchedule As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuTools_IntuitPatient As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents pnlPatientSavings As System.Windows.Forms.Panel
    Friend WithEvents lblPatientSavings As System.Windows.Forms.Label
    Friend WithEvents picPatientSavings As System.Windows.Forms.PictureBox
    Friend WithEvents mnuMaster_ICD9Gallery As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMaster_ICD10Gallery As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMaster_CPTGallery As System.Windows.Forms.ToolStripMenuItem
    'Friend WithEvents mnuView_PatientFrom As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMaster_ClinicalInstructions As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMaster_CarePlan As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuICDAnalysis As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsbtn_Order As System.Windows.Forms.ToolStripButton
    Friend WithEvents SplitterOrderComments As System.Windows.Forms.Splitter
    Friend WithEvents txtOrderComment As System.Windows.Forms.RichTextBox
    Friend WithEvents MedicaidCensusReportDashboardToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_rpt_Appointments As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_rpt_NoShowAppointments As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMedicalCategory As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator17 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents pnl_MedicalCategory As System.Windows.Forms.Panel
    Friend WithEvents lblMedicalCategory As System.Windows.Forms.Label
    Private WithEvents Label69 As System.Windows.Forms.Label
    Friend WithEvents Panel9 As System.Windows.Forms.Panel
    Friend WithEvents cmbFilters As System.Windows.Forms.ComboBox
    Friend WithEvents Label65 As System.Windows.Forms.Label
    Friend WithEvents _flexGroup As gloEMR.FlexGroupControl
    Friend WithEvents Panel10 As System.Windows.Forms.Panel
    Friend WithEvents txtTaskSearch As gloUserControlLibrary.gloSearchTextBox
    Friend WithEvents Label75 As System.Windows.Forms.Label
    Friend WithEvents pictureBox2 As System.Windows.Forms.PictureBox
    Friend WithEvents Label76 As System.Windows.Forms.Label
    Friend WithEvents Label77 As System.Windows.Forms.Label
    Private WithEvents Label78 As System.Windows.Forms.Label
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Private WithEvents Label79 As System.Windows.Forms.Label
    Private WithEvents Label80 As System.Windows.Forms.Label
    Private WithEvents Label81 As System.Windows.Forms.Label
    Friend WithEvents Label70 As System.Windows.Forms.Label
    Friend WithEvents Panel12 As System.Windows.Forms.Panel
    Friend WithEvents pnlViewMoreTask As System.Windows.Forms.Panel
    Friend WithEvents lblLinkViewMoreTask As System.Windows.Forms.LinkLabel
    Friend WithEvents C1UserTasks As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents mnuConsentTracking As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsbtn_NYWCForms As System.Windows.Forms.ToolStripButton
    Friend WithEvents mnuOBReport As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents pnlCase As System.Windows.Forms.Panel
    Friend WithEvents Label83 As System.Windows.Forms.Label
    Friend WithEvents cmbCase As System.Windows.Forms.ComboBox
    Friend WithEvents tls_CodeGuide As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents pnlWait As System.Windows.Forms.Panel
    Friend WithEvents Label84 As System.Windows.Forms.Label
    Friend WithEvents Label85 As System.Windows.Forms.Label
    Friend WithEvents mnu_rpt_CensusAppointments As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents pnl_workphone As System.Windows.Forms.Panel
    Friend WithEvents lblworkphone As System.Windows.Forms.Label
    Private WithEvents Label89 As System.Windows.Forms.Label
    Friend WithEvents pnl_Businesscenter As System.Windows.Forms.Panel
    Friend WithEvents lblbusinesscenter As System.Windows.Forms.Label
    Private WithEvents Label88 As System.Windows.Forms.Label
    Friend WithEvents pnl_Occupation As System.Windows.Forms.Panel
    Friend WithEvents lbloccupation As System.Windows.Forms.Label
    Private WithEvents Label87 As System.Windows.Forms.Label
    Friend WithEvents MnuExamFinishReport As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuReports_InactiveCPTSReport As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tlbbtn_RCMDocs As System.Windows.Forms.ToolStripButton
    Friend WithEvents lblBadDebt As System.Windows.Forms.Label
    Friend WithEvents Pnl_PharmacyAddress As System.Windows.Forms.Panel
    Private WithEvents lblPD_PharmacyAddress As System.Windows.Forms.Label
    Private WithEvents lblPD_PharmacyAddr As System.Windows.Forms.Label
    Friend WithEvents mnuReports_DrugMigrationReport As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_ClinicalChartPrintQueue As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMUDashboard_Stage2_Mod As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMUDashboard_Stage3 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_rpt_ConfirmAppointments As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuePARequests As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents sslbl_SingleSignOn As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents pnl_PrimaryInsurance As System.Windows.Forms.Panel
    Friend WithEvents lblPrimaryInsurance As System.Windows.Forms.Label
    Private WithEvents Label92 As System.Windows.Forms.Label
    Friend WithEvents pnl_SecondaryInsurance As System.Windows.Forms.Panel
    Friend WithEvents lblSecondaryInsurance As System.Windows.Forms.Label
    Private WithEvents Label91 As System.Windows.Forms.Label
    Friend WithEvents pnl_TertiaryInsurance As System.Windows.Forms.Panel
    Friend WithEvents lbl_TertiaryInsurance As System.Windows.Forms.Label
    Private WithEvents Label90 As System.Windows.Forms.Label
    Friend WithEvents pnl_Provider As System.Windows.Forms.Panel
    Friend WithEvents lblPD_Provider As System.Windows.Forms.Label
    Private WithEvents Label93 As System.Windows.Forms.Label
    Friend WithEvents mnu_MIPS As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_MIPS_Quality As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_MIPS_Quality_2017 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_MIPS_Quality_2019 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_MIPS_ACI As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuEPCS As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuControlledSubstanceERX As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuEPCSAuditTrail As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuTools_QRDAImport As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMUDashboard_2017_ACI As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuPDRPrograms As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuPDRProgramsReports As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuPDMPPrograms As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents notifyInbox As System.Windows.Forms.NotifyIcon
    Friend WithEvents mnu_SocPsycBehobs As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_ImplantableDevice_Setup As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_ImplantableDevices As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator18 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuPendingRxChangeRequest As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator20 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator21 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuReports_PortalPHIReview As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_PortalPHI As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator22 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuToolsCDAPatientInfoStatus As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuPlanOfTreatment As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_CCDAPatientConsent As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_APIHarness As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_APIHarness_Roles As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_APIHarness_Users As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_APIHarness_Reports As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents pnlCancelRx As System.Windows.Forms.Panel
    Friend WithEvents chkCancelRx As System.Windows.Forms.CheckBox
    Friend WithEvents mnuViewPrescriptions As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuVwSummaryCareRecord As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuTools_ProviderEducation As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsbtn_SPB As System.Windows.Forms.ToolStripButton
    Friend WithEvents pnllblLinkUnfinishedexam As System.Windows.Forms.Panel
    Friend WithEvents lblLinkUnfinishedAll As System.Windows.Forms.LinkLabel
    Friend WithEvents mnuTools_RefreshDevicesPrinters As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_ScreeningTools As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents HOOSJRToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents KOOSJRKNEESurveyToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents HOOSTotalAssesmentToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents KOOSKNEESurveyToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PROMIS10ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PROMIS29ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents VETERANSRAND12ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents VETERANSRAND36SurveyToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    'Friend WithEvents PHQ9ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PHQ2ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ViewScreenings As System.Windows.Forms.ToolStripMenuItem    
    Friend WithEvents ToolStripMenuItem26 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem27 As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents cmnuItem_DeleteTask As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents cmnuItem_FollowUpTask As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_MIPSACI_2019 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_Stage3_2019 As System.Windows.Forms.ToolStripMenuItem
End Class
