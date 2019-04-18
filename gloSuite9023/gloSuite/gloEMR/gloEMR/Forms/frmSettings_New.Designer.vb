<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSettings_New
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            Try
                gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
            Catch
            End Try
            components.Dispose()
        End If
        Try
            If Not IsNothing(ColorDialog1) Then
                ColorDialog1.Dispose()
                ColorDialog1 = Nothing
            End If
        Catch ex As Exception

        End Try
        Try
            If Not IsNothing(FolderBrowserDialog1) Then
                FolderBrowserDialog1.Dispose()
                FolderBrowserDialog1 = Nothing
            End If
        Catch ex As Exception

        End Try
        Try
            If Not IsNothing(clordialogWord) Then
                clordialogWord.Dispose()
                clordialogWord = Nothing
            End If
        Catch ex As Exception

        End Try
       

        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSettings_New))
        Me.pnlMAIN = New System.Windows.Forms.Panel()
        Me.tc_Settings = New System.Windows.Forms.TabControl()
        Me.tp_Voice = New System.Windows.Forms.TabPage()
        Me.GroupBox13 = New System.Windows.Forms.GroupBox()
        Me.chkLocalSigature = New System.Windows.Forms.CheckBox()
        Me.cbSigPlusTS = New System.Windows.Forms.CheckBox()
        Me.txtTabletType = New System.Windows.Forms.TextBox()
        Me.Label195 = New System.Windows.Forms.Label()
        Me.txtIPAddress = New System.Windows.Forms.TextBox()
        Me.Label191 = New System.Windows.Forms.Label()
        Me.txtMultiUSB = New System.Windows.Forms.TextBox()
        Me.Label188 = New System.Windows.Forms.Label()
        Me.txtTabletPortPath = New System.Windows.Forms.TextBox()
        Me.Label106 = New System.Windows.Forms.Label()
        Me.grb_PatientNotesFooterSettings = New System.Windows.Forms.GroupBox()
        Me.rdo_IncludePageNo_No = New System.Windows.Forms.RadioButton()
        Me.rdo_IncludePageNo_Yes = New System.Windows.Forms.RadioButton()
        Me.Label68 = New System.Windows.Forms.Label()
        Me.grWordhighlighhcolr = New System.Windows.Forms.GroupBox()
        Me.cmbHighlight = New System.Windows.Forms.ComboBox()
        Me.lblcolor = New System.Windows.Forms.Label()
        Me.chksethighlightcolr = New System.Windows.Forms.CheckBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.rbtnNone = New System.Windows.Forms.RadioButton()
        Me.rbtnSelect = New System.Windows.Forms.RadioButton()
        Me.rbtnNotes = New System.Windows.Forms.RadioButton()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.pnlResultBoxPosition = New System.Windows.Forms.Panel()
        Me.optBottomRight = New System.Windows.Forms.RadioButton()
        Me.optBottomLeft = New System.Windows.Forms.RadioButton()
        Me.optTopRight = New System.Windows.Forms.RadioButton()
        Me.optTopLeft = New System.Windows.Forms.RadioButton()
        Me.optYes = New System.Windows.Forms.RadioButton()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.optNo = New System.Windows.Forms.RadioButton()
        Me.GroupBox15 = New System.Windows.Forms.GroupBox()
        Me.Label203 = New System.Windows.Forms.Label()
        Me.numAutoSaveMinutes = New System.Windows.Forms.NumericUpDown()
        Me.chkAutoSaveExam = New System.Windows.Forms.CheckBox()
        Me.TextBox3 = New System.Windows.Forms.TextBox()
        Me.Label200 = New System.Windows.Forms.Label()
        Me.TextBox4 = New System.Windows.Forms.TextBox()
        Me.Label201 = New System.Windows.Forms.Label()
        Me.TextBox5 = New System.Windows.Forms.TextBox()
        Me.Label202 = New System.Windows.Forms.Label()
        Me.Label73 = New System.Windows.Forms.Label()
        Me.Label74 = New System.Windows.Forms.Label()
        Me.Label72 = New System.Windows.Forms.Label()
        Me.Label65 = New System.Windows.Forms.Label()
        Me.Label75 = New System.Windows.Forms.Label()
        Me.Label66 = New System.Windows.Forms.Label()
        Me.Label64 = New System.Windows.Forms.Label()
        Me.Label67 = New System.Windows.Forms.Label()
        Me.tp_FAXSettings = New System.Windows.Forms.TabPage()
        Me.GroupBox14 = New System.Windows.Forms.GroupBox()
        Me.Label199 = New System.Windows.Forms.Label()
        Me.txtFaxDownloadPath = New System.Windows.Forms.TextBox()
        Me.btnBrowseDownloadPath = New System.Windows.Forms.Button()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Label196 = New System.Windows.Forms.Label()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.Label197 = New System.Windows.Forms.Label()
        Me.Label198 = New System.Windows.Forms.Label()
        Me.GroupBox7 = New System.Windows.Forms.GroupBox()
        Me.chkHandleFAXIssue = New System.Windows.Forms.CheckBox()
        Me.chkCoverPage = New System.Windows.Forms.CheckBox()
        Me.cmbFAXPrinter = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtFAXOutputDirectory = New System.Windows.Forms.TextBox()
        Me.btnBrowseFAXOutputDirectory = New System.Windows.Forms.Button()
        Me.Label76 = New System.Windows.Forms.Label()
        Me.Label77 = New System.Windows.Forms.Label()
        Me.Label78 = New System.Windows.Forms.Label()
        Me.Label79 = New System.Windows.Forms.Label()
        Me.Label60 = New System.Windows.Forms.Label()
        Me.Label61 = New System.Windows.Forms.Label()
        Me.Label62 = New System.Windows.Forms.Label()
        Me.Label63 = New System.Windows.Forms.Label()
        Me.tp_ServerPaths = New System.Windows.Forms.TabPage()
        Me.GroupBox8 = New System.Windows.Forms.GroupBox()
        Me.txtDICOMPath = New System.Windows.Forms.TextBox()
        Me.lblDICOMPath = New System.Windows.Forms.Label()
        Me.btnBrowseDICOMPath = New System.Windows.Forms.Button()
        Me.txtVMSPath = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtServerPath = New System.Windows.Forms.TextBox()
        Me.btnServerpath = New System.Windows.Forms.Button()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtDMSPath = New System.Windows.Forms.TextBox()
        Me.btnBrowseDMSPath = New System.Windows.Forms.Button()
        Me.lblDMSMessage = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.btnBrowseVMSPath = New System.Windows.Forms.Button()
        Me.Label80 = New System.Windows.Forms.Label()
        Me.Label81 = New System.Windows.Forms.Label()
        Me.Label82 = New System.Windows.Forms.Label()
        Me.Label83 = New System.Windows.Forms.Label()
        Me.Label56 = New System.Windows.Forms.Label()
        Me.Label57 = New System.Windows.Forms.Label()
        Me.Label58 = New System.Windows.Forms.Label()
        Me.Label59 = New System.Windows.Forms.Label()
        Me.tp_DrugInteraction = New System.Windows.Forms.TabPage()
        Me.pnlDI_Main = New System.Windows.Forms.Panel()
        Me.GroupBox22 = New System.Windows.Forms.GroupBox()
        Me.chkDrugDiseaseInteraction = New System.Windows.Forms.CheckBox()
        Me.chkDrugFoodInteraction = New System.Windows.Forms.CheckBox()
        Me.chkDrugAllergyInteraction = New System.Windows.Forms.CheckBox()
        Me.chkAdverseDrugEffect = New System.Windows.Forms.CheckBox()
        Me.chkDrugDrugInteraction = New System.Windows.Forms.CheckBox()
        Me.chkDuplicateTherapy = New System.Windows.Forms.CheckBox()
        Me.chkDrugAlert = New System.Windows.Forms.CheckBox()
        Me.grpDrugInteraction = New System.Windows.Forms.GroupBox()
        Me.pnlDrugInteraction = New System.Windows.Forms.Panel()
        Me.cmbADEOnset = New System.Windows.Forms.ComboBox()
        Me.Label204 = New System.Windows.Forms.Label()
        Me.cmbDFADoc = New System.Windows.Forms.ComboBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.cmbDIDoc = New System.Windows.Forms.ComboBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.cmbDFA = New System.Windows.Forms.ComboBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.cmbDI = New System.Windows.Forms.ComboBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.cmbADE = New System.Windows.Forms.ComboBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label84 = New System.Windows.Forms.Label()
        Me.Label85 = New System.Windows.Forms.Label()
        Me.Label86 = New System.Windows.Forms.Label()
        Me.Label87 = New System.Windows.Forms.Label()
        Me.Label52 = New System.Windows.Forms.Label()
        Me.Label53 = New System.Windows.Forms.Label()
        Me.Label54 = New System.Windows.Forms.Label()
        Me.Label55 = New System.Windows.Forms.Label()
        Me.tp_OtherSettings = New System.Windows.Forms.TabPage()
        Me.Label48 = New System.Windows.Forms.Label()
        Me.Label49 = New System.Windows.Forms.Label()
        Me.Label50 = New System.Windows.Forms.Label()
        Me.Label51 = New System.Windows.Forms.Label()
        Me.pnlOtherSettings = New System.Windows.Forms.Panel()
        Me.GroupBox26 = New System.Windows.Forms.GroupBox()
        Me.chkEnableLocalWelchAllynECGDevice = New System.Windows.Forms.CheckBox()
        Me.GroupBox12 = New System.Windows.Forms.GroupBox()
        Me.chk_ErrorLogs = New System.Windows.Forms.CheckBox()
        Me.chk_ApplicationLog = New System.Windows.Forms.CheckBox()
        Me.Panel28 = New System.Windows.Forms.Panel()
        Me.grbSearchSetting = New System.Windows.Forms.GroupBox()
        Me.chkResetSearch = New System.Windows.Forms.CheckBox()
        Me.GroupBox10 = New System.Windows.Forms.GroupBox()
        Me.ChkPatientConfiInfo = New System.Windows.Forms.CheckBox()
        Me.GroupBox11 = New System.Windows.Forms.GroupBox()
        Me.Label180 = New System.Windows.Forms.Label()
        Me.txtInfo = New System.Windows.Forms.TextBox()
        Me.grpbxPatientSynopsis = New System.Windows.Forms.GroupBox()
        Me.numPatientSypnosisTabCount = New System.Windows.Forms.NumericUpDown()
        Me.lblPatientTabCount = New System.Windows.Forms.Label()
        Me.grpbxRxMxDrugBtnSetting = New System.Windows.Forms.GroupBox()
        Me.cmbMxDrugBtn = New System.Windows.Forms.ComboBox()
        Me.lblMx = New System.Windows.Forms.Label()
        Me.cmbRxDrugBtn = New System.Windows.Forms.ComboBox()
        Me.lblRx = New System.Windows.Forms.Label()
        Me.gbRemotePrintSetting = New System.Windows.Forms.GroupBox()
        Me.cmbNoTemplatesJob = New System.Windows.Forms.ComboBox()
        Me.Label213 = New System.Windows.Forms.Label()
        Me.chkZipMetadata = New System.Windows.Forms.CheckBox()
        Me.pnlPrintImages = New System.Windows.Forms.Panel()
        Me.rbPrintImagesEMF = New System.Windows.Forms.RadioButton()
        Me.Label209 = New System.Windows.Forms.Label()
        Me.rbPrintImagesPNG = New System.Windows.Forms.RadioButton()
        Me.Panel25 = New System.Windows.Forms.Panel()
        Me.rbPrintSSRSReportEMF = New System.Windows.Forms.RadioButton()
        Me.Label208 = New System.Windows.Forms.Label()
        Me.rbPrintSSRSReportPDF = New System.Windows.Forms.RadioButton()
        Me.pnlPrintClaims = New System.Windows.Forms.Panel()
        Me.rbPrintClaimsEMF = New System.Windows.Forms.RadioButton()
        Me.Label210 = New System.Windows.Forms.Label()
        Me.rbPrintClaimsPDF = New System.Windows.Forms.RadioButton()
        Me.Panel24 = New System.Windows.Forms.Panel()
        Me.rbPrintWordDocEMF = New System.Windows.Forms.RadioButton()
        Me.Label207 = New System.Windows.Forms.Label()
        Me.rbPrintWordDocPDF = New System.Windows.Forms.RadioButton()
        Me.cmbNoPagesSplit = New System.Windows.Forms.ComboBox()
        Me.Label206 = New System.Windows.Forms.Label()
        Me.Label205 = New System.Windows.Forms.Label()
        Me.chkAddFooterService = New System.Windows.Forms.CheckBox()
        Me.chkEnableLocalPrinter = New System.Windows.Forms.CheckBox()
        Me.Panel29 = New System.Windows.Forms.Panel()
        Me.gb_DefaultPrinterSettings = New System.Windows.Forms.GroupBox()
        Me.chkUseDefaultPrinter = New System.Windows.Forms.CheckBox()
        Me.Gbox_DefaultNavgtn = New System.Windows.Forms.GroupBox()
        Me.Cmb_NavgtnPnl = New System.Windows.Forms.ComboBox()
        Me.Lbl_NavgtnPnl = New System.Windows.Forms.Label()
        Me.Label71 = New System.Windows.Forms.Label()
        Me.grb_AutoRefreshSettings = New System.Windows.Forms.GroupBox()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.num_MessagesRefreshTime = New System.Windows.Forms.NumericUpDown()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.grBday = New System.Windows.Forms.GroupBox()
        Me.pnlBday = New System.Windows.Forms.Panel()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.numBdayReminder = New System.Windows.Forms.NumericUpDown()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.chkBdayReminder = New System.Windows.Forms.CheckBox()
        Me.grSettings = New System.Windows.Forms.GroupBox()
        Me.chkOutbound = New System.Windows.Forms.CheckBox()
        Me.grHL7Settings = New System.Windows.Forms.GroupBox()
        Me.chkHL7Appointment = New System.Windows.Forms.CheckBox()
        Me.chkHL7Immunization = New System.Windows.Forms.CheckBox()
        Me.rbHL7 = New System.Windows.Forms.CheckBox()
        Me.rbGenius = New System.Windows.Forms.CheckBox()
        Me.chkPatientReg = New System.Windows.Forms.CheckBox()
        Me.chkSaveandFinish = New System.Windows.Forms.CheckBox()
        Me.chkSaveandClose = New System.Windows.Forms.CheckBox()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.pnlClinicEnv = New System.Windows.Forms.Panel()
        Me.lblENV_06 = New System.Windows.Forms.Label()
        Me.lblENV_05 = New System.Windows.Forms.Label()
        Me.lblENV_04 = New System.Windows.Forms.Label()
        Me.lblENV_03 = New System.Windows.Forms.Label()
        Me.lblENV_02 = New System.Windows.Forms.Label()
        Me.lblENV_01 = New System.Windows.Forms.Label()
        Me.grpLockScreen = New System.Windows.Forms.GroupBox()
        Me.chkAutoApplicationLock = New System.Windows.Forms.CheckBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.numLockTime = New System.Windows.Forms.NumericUpDown()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label69 = New System.Windows.Forms.Label()
        Me.Label70 = New System.Windows.Forms.Label()
        Me.tp_SureScriptSettings = New System.Windows.Forms.TabPage()
        Me.grpsurescripSecurMsg = New System.Windows.Forms.GroupBox()
        Me.chksecureSureScriptsetting = New System.Windows.Forms.CheckBox()
        Me.chkShowOffFormularyAlternatives = New System.Windows.Forms.CheckBox()
        Me.chkMachineFormularyAlert = New System.Windows.Forms.CheckBox()
        Me.grpFormularySettings = New System.Windows.Forms.GroupBox()
        Me.chkShowNDCInMedication = New System.Windows.Forms.CheckBox()
        Me.chkNDCInAlternativeGrid = New System.Windows.Forms.CheckBox()
        Me.grpsurescriptalert = New System.Windows.Forms.GroupBox()
        Me.chkSurescriptFaxSettings = New System.Windows.Forms.CheckBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.cmbInterval = New System.Windows.Forms.ComboBox()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.chksureAlert = New System.Windows.Forms.CheckBox()
        Me.chkFormularyAlertnativesOffFormularyDrgs = New System.Windows.Forms.CheckBox()
        Me.Label88 = New System.Windows.Forms.Label()
        Me.chkFormularyAlertnativesNRDrgs = New System.Windows.Forms.CheckBox()
        Me.Label89 = New System.Windows.Forms.Label()
        Me.chkFormularyAlertnativesAllDrgs = New System.Windows.Forms.CheckBox()
        Me.Label90 = New System.Windows.Forms.Label()
        Me.Label91 = New System.Windows.Forms.Label()
        Me.Label44 = New System.Windows.Forms.Label()
        Me.Label45 = New System.Windows.Forms.Label()
        Me.Label46 = New System.Windows.Forms.Label()
        Me.Label47 = New System.Windows.Forms.Label()
        Me.tp_DMSSettings = New System.Windows.Forms.TabPage()
        Me.GroupBox9 = New System.Windows.Forms.GroupBox()
        Me.Label105 = New System.Windows.Forms.Label()
        Me.Label104 = New System.Windows.Forms.Label()
        Me.numBufferSize = New System.Windows.Forms.NumericUpDown()
        Me.Panel31 = New System.Windows.Forms.Panel()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.Label109 = New System.Windows.Forms.Label()
        Me.cmbImageFormat = New System.Windows.Forms.ComboBox()
        Me.Label211 = New System.Windows.Forms.Label()
        Me.Label212 = New System.Windows.Forms.Label()
        Me.Label107 = New System.Windows.Forms.Label()
        Me.lblCardLocation = New System.Windows.Forms.Label()
        Me.txtStartX = New System.Windows.Forms.TextBox()
        Me.txtStartY = New System.Windows.Forms.TextBox()
        Me.Label119 = New System.Windows.Forms.Label()
        Me.cmbSupportedSize = New System.Windows.Forms.ComboBox()
        Me.lblSuportedSize = New System.Windows.Forms.Label()
        Me.cmbScanMode = New System.Windows.Forms.ComboBox()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.cmbBitDepth = New System.Windows.Forms.ComboBox()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.Label117 = New System.Windows.Forms.Label()
        Me.Label120 = New System.Windows.Forms.Label()
        Me.txtCardWidth = New System.Windows.Forms.TextBox()
        Me.txtCardLength = New System.Windows.Forms.TextBox()
        Me.Label115 = New System.Windows.Forms.Label()
        Me.Label116 = New System.Windows.Forms.Label()
        Me.cmbScanSide = New System.Windows.Forms.ComboBox()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.Label118 = New System.Windows.Forms.Label()
        Me.cmbContrast = New System.Windows.Forms.ComboBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.cmbBrightness = New System.Windows.Forms.ComboBox()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.chkShowScannerDialog = New System.Windows.Forms.CheckBox()
        Me.cmbResolution = New System.Windows.Forms.ComboBox()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.cmbScanner = New System.Windows.Forms.ComboBox()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.rbScanDocumentWithMonth = New System.Windows.Forms.RadioButton()
        Me.rbScanDocumentWithoutMonth = New System.Windows.Forms.RadioButton()
        Me.pnlRemoteScan = New System.Windows.Forms.GroupBox()
        Me.cmbRemoteScanSideFeeder = New System.Windows.Forms.ComboBox()
        Me.cmbRemoteImageFormat = New System.Windows.Forms.ComboBox()
        Me.Label110 = New System.Windows.Forms.Label()
        Me.Label233 = New System.Windows.Forms.Label()
        Me.txtRemoteStartX = New System.Windows.Forms.TextBox()
        Me.cmbRemoteSupportedSize = New System.Windows.Forms.ComboBox()
        Me.txtRemoteStartY = New System.Windows.Forms.TextBox()
        Me.Label234 = New System.Windows.Forms.Label()
        Me.Label235 = New System.Windows.Forms.Label()
        Me.Label236 = New System.Windows.Forms.Label()
        Me.Label237 = New System.Windows.Forms.Label()
        Me.chkRemoteShowScannerDialog = New System.Windows.Forms.CheckBox()
        Me.txtRemoteCardWidth = New System.Windows.Forms.TextBox()
        Me.Label238 = New System.Windows.Forms.Label()
        Me.txtRemoteCardLength = New System.Windows.Forms.TextBox()
        Me.Label239 = New System.Windows.Forms.Label()
        Me.Label240 = New System.Windows.Forms.Label()
        Me.cmbRemoteBitDepth = New System.Windows.Forms.ComboBox()
        Me.Label241 = New System.Windows.Forms.Label()
        Me.Label242 = New System.Windows.Forms.Label()
        Me.Label243 = New System.Windows.Forms.Label()
        Me.Label244 = New System.Windows.Forms.Label()
        Me.Label245 = New System.Windows.Forms.Label()
        Me.Label246 = New System.Windows.Forms.Label()
        Me.cmbRemoteContrast = New System.Windows.Forms.ComboBox()
        Me.Label247 = New System.Windows.Forms.Label()
        Me.Label248 = New System.Windows.Forms.Label()
        Me.cmbRemoteScanSide = New System.Windows.Forms.ComboBox()
        Me.cmbRemoteBrightness = New System.Windows.Forms.ComboBox()
        Me.Label249 = New System.Windows.Forms.Label()
        Me.Label250 = New System.Windows.Forms.Label()
        Me.GroupBox24 = New System.Windows.Forms.GroupBox()
        Me.RadioButton1 = New System.Windows.Forms.RadioButton()
        Me.RadioButton2 = New System.Windows.Forms.RadioButton()
        Me.cmbRemoteScanMode = New System.Windows.Forms.ComboBox()
        Me.cmbRemoteResolution = New System.Windows.Forms.ComboBox()
        Me.cmbRemoteScanner = New System.Windows.Forms.ComboBox()
        Me.GroupBox23 = New System.Windows.Forms.GroupBox()
        Me.chkZipScannerSettings = New System.Windows.Forms.CheckBox()
        Me.chkEliminatePegasus = New System.Windows.Forms.CheckBox()
        Me.btnRefreshTwainScanners = New System.Windows.Forms.Button()
        Me.btnRefreshScanners = New System.Windows.Forms.Button()
        Me.chkEnableRemoteScanner = New System.Windows.Forms.CheckBox()
        Me.Label92 = New System.Windows.Forms.Label()
        Me.Label93 = New System.Windows.Forms.Label()
        Me.Label94 = New System.Windows.Forms.Label()
        Me.Label95 = New System.Windows.Forms.Label()
        Me.Label40 = New System.Windows.Forms.Label()
        Me.Label41 = New System.Windows.Forms.Label()
        Me.Label42 = New System.Windows.Forms.Label()
        Me.Label43 = New System.Windows.Forms.Label()
        Me.tp_PatientControl = New System.Windows.Forms.TabPage()
        Me.GroupBox25 = New System.Windows.Forms.GroupBox()
        Me.chkClearDashboardSearch = New System.Windows.Forms.CheckBox()
        Me.grbExportReport = New System.Windows.Forms.GroupBox()
        Me.btnClearReportPath = New System.Windows.Forms.Button()
        Me.btnBrowseReportPath = New System.Windows.Forms.Button()
        Me.txtExportReportPath = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.chkExportReport = New System.Windows.Forms.CheckBox()
        Me.grbDefaultProvider = New System.Windows.Forms.GroupBox()
        Me.cmbDefaultProvider = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.grbPatientBillingAlerts = New System.Windows.Forms.GroupBox()
        Me.btnBrowseAlertColor = New System.Windows.Forms.Button()
        Me.txtAlertColor = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.chkShowBlinkingAlert = New System.Windows.Forms.CheckBox()
        Me.GrpBoxPatienDemographics = New System.Windows.Forms.GroupBox()
        Me.trvDemographics = New System.Windows.Forms.TreeView()
        Me.grbPatientSearch = New System.Windows.Forms.GroupBox()
        Me.trvPatientSearch = New System.Windows.Forms.TreeView()
        Me.GroupBox6 = New System.Windows.Forms.GroupBox()
        Me.trvPatientColumns = New System.Windows.Forms.TreeView()
        Me.gr_PatientSearch = New System.Windows.Forms.GroupBox()
        Me.cmbSearchColumns = New System.Windows.Forms.ComboBox()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.Label96 = New System.Windows.Forms.Label()
        Me.Label97 = New System.Windows.Forms.Label()
        Me.Label98 = New System.Windows.Forms.Label()
        Me.Label99 = New System.Windows.Forms.Label()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.Label39 = New System.Windows.Forms.Label()
        Me.tp_ExchangeServer = New System.Windows.Forms.TabPage()
        Me.gr_ExchangeServerSettings = New System.Windows.Forms.GroupBox()
        Me.txtExchangeDomain = New System.Windows.Forms.TextBox()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.txtExchangeURL = New System.Windows.Forms.TextBox()
        Me.Label100 = New System.Windows.Forms.Label()
        Me.Label101 = New System.Windows.Forms.Label()
        Me.Label102 = New System.Windows.Forms.Label()
        Me.Label103 = New System.Windows.Forms.Label()
        Me.lbl_BottomBrd = New System.Windows.Forms.Label()
        Me.lbl_LeftBrd = New System.Windows.Forms.Label()
        Me.lbl_RightBrd = New System.Windows.Forms.Label()
        Me.lbl_TopBrd = New System.Windows.Forms.Label()
        Me.tp_Appointment = New System.Windows.Forms.TabPage()
        Me.grb_CheckedoutAppointment = New System.Windows.Forms.GroupBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.chkCheckedoutAppointments = New System.Windows.Forms.CheckBox()
        Me.Label114 = New System.Windows.Forms.Label()
        Me.grbFollowup = New System.Windows.Forms.GroupBox()
        Me.rbFollowupFromToday = New System.Windows.Forms.RadioButton()
        Me.rbFolloupFromDate = New System.Windows.Forms.RadioButton()
        Me.grbAppointments = New System.Windows.Forms.GroupBox()
        Me.Label181 = New System.Windows.Forms.Label()
        Me.num_NoofColOnCalndr = New System.Windows.Forms.NumericUpDown()
        Me.lblCalCol = New System.Windows.Forms.Label()
        Me.chkShowTemplate = New System.Windows.Forms.CheckBox()
        Me.num_NoofApptInaSlot = New System.Windows.Forms.NumericUpDown()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label111 = New System.Windows.Forms.Label()
        Me.Label112 = New System.Windows.Forms.Label()
        Me.Label113 = New System.Windows.Forms.Label()
        Me.tb_SmartSettings = New System.Windows.Forms.TabPage()
        Me.PnlCustomTask = New System.Windows.Forms.Panel()
        Me.Panel10 = New System.Windows.Forms.Panel()
        Me.Panel18 = New System.Windows.Forms.Panel()
        Me.chklst_SmartOrders = New System.Windows.Forms.CheckedListBox()
        Me.Label182 = New System.Windows.Forms.Label()
        Me.Label176 = New System.Windows.Forms.Label()
        Me.Label146 = New System.Windows.Forms.Label()
        Me.Label147 = New System.Windows.Forms.Label()
        Me.Panel19 = New System.Windows.Forms.Panel()
        Me.chk_SmartOrder = New System.Windows.Forms.CheckBox()
        Me.Button13 = New System.Windows.Forms.Button()
        Me.Label184 = New System.Windows.Forms.Label()
        Me.btnClearAllOrder = New System.Windows.Forms.Button()
        Me.Label183 = New System.Windows.Forms.Label()
        Me.btn_UpOrders = New System.Windows.Forms.Button()
        Me.Label167 = New System.Windows.Forms.Label()
        Me.btn_DownOrders = New System.Windows.Forms.Button()
        Me.Label148 = New System.Windows.Forms.Label()
        Me.Label150 = New System.Windows.Forms.Label()
        Me.Label151 = New System.Windows.Forms.Label()
        Me.Panel20 = New System.Windows.Forms.Panel()
        Me.chklst_SmartTreatment = New System.Windows.Forms.CheckedListBox()
        Me.Label179 = New System.Windows.Forms.Label()
        Me.Label175 = New System.Windows.Forms.Label()
        Me.Label152 = New System.Windows.Forms.Label()
        Me.Label153 = New System.Windows.Forms.Label()
        Me.Panel21 = New System.Windows.Forms.Panel()
        Me.chk_SmartTreatment = New System.Windows.Forms.CheckBox()
        Me.Button14 = New System.Windows.Forms.Button()
        Me.Label166 = New System.Windows.Forms.Label()
        Me.btnClearAllCPT = New System.Windows.Forms.Button()
        Me.Label165 = New System.Windows.Forms.Label()
        Me.btn_UpTreatment = New System.Windows.Forms.Button()
        Me.Label164 = New System.Windows.Forms.Label()
        Me.btn_DownTreatment = New System.Windows.Forms.Button()
        Me.Label154 = New System.Windows.Forms.Label()
        Me.Label156 = New System.Windows.Forms.Label()
        Me.Label157 = New System.Windows.Forms.Label()
        Me.Panel14 = New System.Windows.Forms.Panel()
        Me.chklst_SmartDiagnosis = New System.Windows.Forms.CheckedListBox()
        Me.Label172 = New System.Windows.Forms.Label()
        Me.Label171 = New System.Windows.Forms.Label()
        Me.Label134 = New System.Windows.Forms.Label()
        Me.Label135 = New System.Windows.Forms.Label()
        Me.Panel15 = New System.Windows.Forms.Panel()
        Me.chk_SmartDiagnosis = New System.Windows.Forms.CheckBox()
        Me.btnSelectAllDX = New System.Windows.Forms.Button()
        Me.Label170 = New System.Windows.Forms.Label()
        Me.btnClearAllDX = New System.Windows.Forms.Button()
        Me.Label169 = New System.Windows.Forms.Label()
        Me.btn_UpDiagnosis = New System.Windows.Forms.Button()
        Me.Label168 = New System.Windows.Forms.Label()
        Me.btn_DownDiagnosis = New System.Windows.Forms.Button()
        Me.Label139 = New System.Windows.Forms.Label()
        Me.Label136 = New System.Windows.Forms.Label()
        Me.Label137 = New System.Windows.Forms.Label()
        Me.Label133 = New System.Windows.Forms.Label()
        Me.Panel9 = New System.Windows.Forms.Panel()
        Me.Label189 = New System.Windows.Forms.Label()
        Me.Label187 = New System.Windows.Forms.Label()
        Me.Label186 = New System.Windows.Forms.Label()
        Me.Label185 = New System.Windows.Forms.Label()
        Me.Splitter1 = New System.Windows.Forms.Splitter()
        Me.Panel26 = New System.Windows.Forms.Panel()
        Me.Panel22 = New System.Windows.Forms.Panel()
        Me.C1SmartOrdersSendTask = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.pnlSmartOrder_Select = New System.Windows.Forms.Panel()
        Me.btnSmartOrderView_All = New System.Windows.Forms.Button()
        Me.btnSmartOrderView_Cancel = New System.Windows.Forms.Button()
        Me.btnSmartOrderSelect_Cancel = New System.Windows.Forms.Button()
        Me.btnSmartOrderSelect_All = New System.Windows.Forms.Button()
        Me.Label141 = New System.Windows.Forms.Label()
        Me.chklst_SmartOrdersSendTask = New System.Windows.Forms.CheckedListBox()
        Me.Label158 = New System.Windows.Forms.Label()
        Me.Label159 = New System.Windows.Forms.Label()
        Me.Panel23 = New System.Windows.Forms.Panel()
        Me.chk_SmartOrder_SendTask = New System.Windows.Forms.CheckBox()
        Me.Label160 = New System.Windows.Forms.Label()
        Me.Label161 = New System.Windows.Forms.Label()
        Me.Label162 = New System.Windows.Forms.Label()
        Me.Label163 = New System.Windows.Forms.Label()
        Me.Panel16 = New System.Windows.Forms.Panel()
        Me.C1SmartTreatmentSendTask = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.pnlSmartTreatment_Select = New System.Windows.Forms.Panel()
        Me.btnSmartTreatmentView_All = New System.Windows.Forms.Button()
        Me.btnSmartTreatmentView_Cancel = New System.Windows.Forms.Button()
        Me.btnSmartTreatmentSelect_Cancel = New System.Windows.Forms.Button()
        Me.btnSmartTreatmentSelect_All = New System.Windows.Forms.Button()
        Me.Label143 = New System.Windows.Forms.Label()
        Me.chklst_SmartTreatmentSendTask = New System.Windows.Forms.CheckedListBox()
        Me.Label140 = New System.Windows.Forms.Label()
        Me.Panel17 = New System.Windows.Forms.Panel()
        Me.chk_SmartTreatment_SendTask = New System.Windows.Forms.CheckBox()
        Me.Label142 = New System.Windows.Forms.Label()
        Me.Label144 = New System.Windows.Forms.Label()
        Me.Label145 = New System.Windows.Forms.Label()
        Me.Panel12 = New System.Windows.Forms.Panel()
        Me.C1SmartDiagnosisSendTask = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.pnlSmartDiagnosis_Select = New System.Windows.Forms.Panel()
        Me.btnSmartDiagnosisView_All = New System.Windows.Forms.Button()
        Me.btnSmartDiagnosisView_Cancel = New System.Windows.Forms.Button()
        Me.btnSmartDiagnosisSelect_Cancel = New System.Windows.Forms.Button()
        Me.btnSmartDiagnosisSelect_All = New System.Windows.Forms.Button()
        Me.Label149 = New System.Windows.Forms.Label()
        Me.chklst_SmartDiagnosisSendTask = New System.Windows.Forms.CheckedListBox()
        Me.CheckBox10 = New System.Windows.Forms.CheckBox()
        Me.Label129 = New System.Windows.Forms.Label()
        Me.Label128 = New System.Windows.Forms.Label()
        Me.Panel13 = New System.Windows.Forms.Panel()
        Me.chk_SmartDiagnosis_SendTask = New System.Windows.Forms.CheckBox()
        Me.Label138 = New System.Windows.Forms.Label()
        Me.Label130 = New System.Windows.Forms.Label()
        Me.Label131 = New System.Windows.Forms.Label()
        Me.Label132 = New System.Windows.Forms.Label()
        Me.Panel27 = New System.Windows.Forms.Panel()
        Me.Label190 = New System.Windows.Forms.Label()
        Me.Label192 = New System.Windows.Forms.Label()
        Me.Label193 = New System.Windows.Forms.Label()
        Me.Label194 = New System.Windows.Forms.Label()
        Me.Panel11 = New System.Windows.Forms.Panel()
        Me.Label178 = New System.Windows.Forms.Label()
        Me.Label177 = New System.Windows.Forms.Label()
        Me.Label174 = New System.Windows.Forms.Label()
        Me.Label173 = New System.Windows.Forms.Label()
        Me.Label155 = New System.Windows.Forms.Label()
        Me.CheckBox9 = New System.Windows.Forms.CheckBox()
        Me.ColorDialog1 = New System.Windows.Forms.ColorDialog()
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog()
        Me.clordialogWord = New System.Windows.Forms.ColorDialog()
        Me.pnl_tlspTOP = New System.Windows.Forms.Panel()
        Me.tlsp_Settings = New gloGlobal.gloToolStripIgnoreFocus()
        Me.ts_btnOk = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnCancel = New System.Windows.Forms.ToolStripButton()
        Me.TwainPro1 = New PegasusImaging.WinForms.TwainPro5.TwainPro(Me.components)
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.btnSelectAllOrder = New System.Windows.Forms.Button()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.btnSelectALLCPT = New System.Windows.Forms.Button()
        Me.Label108 = New System.Windows.Forms.Label()
        Me.Label121 = New System.Windows.Forms.Label()
        Me.Label122 = New System.Windows.Forms.Label()
        Me.Label123 = New System.Windows.Forms.Label()
        Me.CheckBox4 = New System.Windows.Forms.CheckBox()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.GroupBox16 = New System.Windows.Forms.GroupBox()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.CheckedListBox1 = New System.Windows.Forms.CheckedListBox()
        Me.GroupBox17 = New System.Windows.Forms.GroupBox()
        Me.CheckBox2 = New System.Windows.Forms.CheckBox()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.CheckedListBox2 = New System.Windows.Forms.CheckedListBox()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.GroupBox18 = New System.Windows.Forms.GroupBox()
        Me.CheckBox3 = New System.Windows.Forms.CheckBox()
        Me.CheckedListBox3 = New System.Windows.Forms.CheckedListBox()
        Me.GroupBox19 = New System.Windows.Forms.GroupBox()
        Me.CheckBox5 = New System.Windows.Forms.CheckBox()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.Button6 = New System.Windows.Forms.Button()
        Me.CheckedListBox4 = New System.Windows.Forms.CheckedListBox()
        Me.Button7 = New System.Windows.Forms.Button()
        Me.Button8 = New System.Windows.Forms.Button()
        Me.Label124 = New System.Windows.Forms.Label()
        Me.Label125 = New System.Windows.Forms.Label()
        Me.Label126 = New System.Windows.Forms.Label()
        Me.Label127 = New System.Windows.Forms.Label()
        Me.GroupBox20 = New System.Windows.Forms.GroupBox()
        Me.CheckBox6 = New System.Windows.Forms.CheckBox()
        Me.CheckBox7 = New System.Windows.Forms.CheckBox()
        Me.CheckedListBox5 = New System.Windows.Forms.CheckedListBox()
        Me.GroupBox21 = New System.Windows.Forms.GroupBox()
        Me.CheckBox8 = New System.Windows.Forms.CheckBox()
        Me.Panel8 = New System.Windows.Forms.Panel()
        Me.Button9 = New System.Windows.Forms.Button()
        Me.Button10 = New System.Windows.Forms.Button()
        Me.Button11 = New System.Windows.Forms.Button()
        Me.Button12 = New System.Windows.Forms.Button()
        Me.CheckedListBox6 = New System.Windows.Forms.CheckedListBox()
        Me.imgTreeVIew = New System.Windows.Forms.ImageList(Me.components)
        Me.GroupBox27 = New System.Windows.Forms.GroupBox()
        Me.chkGreyScreenIssue = New System.Windows.Forms.CheckBox()
        Me.TextBox6 = New System.Windows.Forms.TextBox()
        Me.Label214 = New System.Windows.Forms.Label()
        Me.TextBox7 = New System.Windows.Forms.TextBox()
        Me.Label215 = New System.Windows.Forms.Label()
        Me.TextBox8 = New System.Windows.Forms.TextBox()
        Me.Label216 = New System.Windows.Forms.Label()
        Me.pnlMAIN.SuspendLayout
        Me.tc_Settings.SuspendLayout
        Me.tp_Voice.SuspendLayout
        Me.GroupBox13.SuspendLayout
        Me.grb_PatientNotesFooterSettings.SuspendLayout
        Me.grWordhighlighhcolr.SuspendLayout
        Me.GroupBox2.SuspendLayout
        Me.GroupBox1.SuspendLayout
        Me.pnlResultBoxPosition.SuspendLayout
        Me.GroupBox15.SuspendLayout
        CType(Me.numAutoSaveMinutes,System.ComponentModel.ISupportInitialize).BeginInit
        Me.tp_FAXSettings.SuspendLayout
        Me.GroupBox14.SuspendLayout
        Me.GroupBox7.SuspendLayout
        Me.tp_ServerPaths.SuspendLayout
        Me.GroupBox8.SuspendLayout
        Me.tp_DrugInteraction.SuspendLayout
        Me.pnlDI_Main.SuspendLayout
        Me.GroupBox22.SuspendLayout
        Me.grpDrugInteraction.SuspendLayout
        Me.pnlDrugInteraction.SuspendLayout
        Me.tp_OtherSettings.SuspendLayout
        Me.pnlOtherSettings.SuspendLayout
        Me.GroupBox26.SuspendLayout
        Me.GroupBox12.SuspendLayout
        Me.Panel28.SuspendLayout
        Me.grbSearchSetting.SuspendLayout
        Me.GroupBox10.SuspendLayout
        Me.GroupBox11.SuspendLayout
        Me.grpbxPatientSynopsis.SuspendLayout
        CType(Me.numPatientSypnosisTabCount,System.ComponentModel.ISupportInitialize).BeginInit
        Me.grpbxRxMxDrugBtnSetting.SuspendLayout
        Me.gbRemotePrintSetting.SuspendLayout
        Me.pnlPrintImages.SuspendLayout
        Me.Panel25.SuspendLayout
        Me.pnlPrintClaims.SuspendLayout
        Me.Panel24.SuspendLayout
        Me.Panel29.SuspendLayout
        Me.gb_DefaultPrinterSettings.SuspendLayout
        Me.Gbox_DefaultNavgtn.SuspendLayout
        Me.grb_AutoRefreshSettings.SuspendLayout
        Me.Panel6.SuspendLayout
        CType(Me.num_MessagesRefreshTime,System.ComponentModel.ISupportInitialize).BeginInit
        Me.grBday.SuspendLayout
        Me.pnlBday.SuspendLayout
        CType(Me.numBdayReminder,System.ComponentModel.ISupportInitialize).BeginInit
        Me.grSettings.SuspendLayout
        Me.grHL7Settings.SuspendLayout
        Me.GroupBox5.SuspendLayout
        Me.pnlClinicEnv.SuspendLayout
        Me.grpLockScreen.SuspendLayout
        CType(Me.numLockTime,System.ComponentModel.ISupportInitialize).BeginInit
        Me.tp_SureScriptSettings.SuspendLayout
        Me.grpsurescripSecurMsg.SuspendLayout
        Me.grpFormularySettings.SuspendLayout
        Me.grpsurescriptalert.SuspendLayout
        Me.Panel1.SuspendLayout
        Me.tp_DMSSettings.SuspendLayout
        Me.GroupBox9.SuspendLayout
        CType(Me.numBufferSize,System.ComponentModel.ISupportInitialize).BeginInit
        Me.Panel31.SuspendLayout
        Me.GroupBox3.SuspendLayout
        Me.GroupBox4.SuspendLayout
        Me.pnlRemoteScan.SuspendLayout
        Me.GroupBox24.SuspendLayout
        Me.GroupBox23.SuspendLayout
        Me.tp_PatientControl.SuspendLayout
        Me.GroupBox25.SuspendLayout
        Me.grbExportReport.SuspendLayout
        Me.grbDefaultProvider.SuspendLayout
        Me.grbPatientBillingAlerts.SuspendLayout
        Me.GrpBoxPatienDemographics.SuspendLayout
        Me.grbPatientSearch.SuspendLayout
        Me.GroupBox6.SuspendLayout
        Me.gr_PatientSearch.SuspendLayout
        Me.tp_ExchangeServer.SuspendLayout
        Me.gr_ExchangeServerSettings.SuspendLayout
        Me.tp_Appointment.SuspendLayout
        Me.grb_CheckedoutAppointment.SuspendLayout
        Me.grbFollowup.SuspendLayout
        Me.grbAppointments.SuspendLayout
        CType(Me.num_NoofColOnCalndr,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.num_NoofApptInaSlot,System.ComponentModel.ISupportInitialize).BeginInit
        Me.tb_SmartSettings.SuspendLayout
        Me.Panel10.SuspendLayout
        Me.Panel18.SuspendLayout
        Me.Panel19.SuspendLayout
        Me.Panel20.SuspendLayout
        Me.Panel21.SuspendLayout
        Me.Panel14.SuspendLayout
        Me.Panel15.SuspendLayout
        Me.Panel9.SuspendLayout
        Me.Panel26.SuspendLayout
        Me.Panel22.SuspendLayout
        CType(Me.C1SmartOrdersSendTask,System.ComponentModel.ISupportInitialize).BeginInit
        Me.pnlSmartOrder_Select.SuspendLayout
        Me.Panel23.SuspendLayout
        Me.Panel16.SuspendLayout
        CType(Me.C1SmartTreatmentSendTask,System.ComponentModel.ISupportInitialize).BeginInit
        Me.pnlSmartTreatment_Select.SuspendLayout
        Me.Panel17.SuspendLayout
        Me.Panel12.SuspendLayout
        CType(Me.C1SmartDiagnosisSendTask,System.ComponentModel.ISupportInitialize).BeginInit
        Me.pnlSmartDiagnosis_Select.SuspendLayout
        Me.Panel13.SuspendLayout
        Me.Panel27.SuspendLayout
        Me.Panel11.SuspendLayout
        Me.pnl_tlspTOP.SuspendLayout
        Me.tlsp_Settings.SuspendLayout
        Me.GroupBox16.SuspendLayout
        Me.GroupBox17.SuspendLayout
        Me.Panel5.SuspendLayout
        Me.GroupBox18.SuspendLayout
        Me.GroupBox19.SuspendLayout
        Me.Panel7.SuspendLayout
        Me.GroupBox20.SuspendLayout
        Me.GroupBox21.SuspendLayout
        Me.Panel8.SuspendLayout
        Me.GroupBox27.SuspendLayout
        Me.SuspendLayout
        '
        'pnlMAIN
        '
        Me.pnlMAIN.BackColor = System.Drawing.Color.FromArgb(CType(CType(207,Byte),Integer), CType(CType(224,Byte),Integer), CType(CType(248,Byte),Integer))
        Me.pnlMAIN.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlMAIN.Controls.Add(Me.tc_Settings)
        Me.pnlMAIN.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMAIN.Location = New System.Drawing.Point(0, 55)
        Me.pnlMAIN.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.pnlMAIN.Name = "pnlMAIN"
        Me.pnlMAIN.Size = New System.Drawing.Size(692, 694)
        Me.pnlMAIN.TabIndex = 6
        '
        'tc_Settings
        '
        Me.tc_Settings.Controls.Add(Me.tp_Voice)
        Me.tc_Settings.Controls.Add(Me.tp_FAXSettings)
        Me.tc_Settings.Controls.Add(Me.tp_ServerPaths)
        Me.tc_Settings.Controls.Add(Me.tp_DrugInteraction)
        Me.tc_Settings.Controls.Add(Me.tp_OtherSettings)
        Me.tc_Settings.Controls.Add(Me.tp_SureScriptSettings)
        Me.tc_Settings.Controls.Add(Me.tp_DMSSettings)
        Me.tc_Settings.Controls.Add(Me.tp_PatientControl)
        Me.tc_Settings.Controls.Add(Me.tp_ExchangeServer)
        Me.tc_Settings.Controls.Add(Me.tp_Appointment)
        Me.tc_Settings.Controls.Add(Me.tb_SmartSettings)
        Me.tc_Settings.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tc_Settings.Location = New System.Drawing.Point(0, 0)
        Me.tc_Settings.Multiline = true
        Me.tc_Settings.Name = "tc_Settings"
        Me.tc_Settings.SelectedIndex = 0
        Me.tc_Settings.Size = New System.Drawing.Size(692, 694)
        Me.tc_Settings.TabIndex = 10
        '
        'tp_Voice
        '
        Me.tp_Voice.BackColor = System.Drawing.Color.FromArgb(CType(CType(207,Byte),Integer), CType(CType(224,Byte),Integer), CType(CType(248,Byte),Integer))
        Me.tp_Voice.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tp_Voice.Controls.Add(Me.GroupBox27)
        Me.tp_Voice.Controls.Add(Me.GroupBox13)
        Me.tp_Voice.Controls.Add(Me.grb_PatientNotesFooterSettings)
        Me.tp_Voice.Controls.Add(Me.grWordhighlighhcolr)
        Me.tp_Voice.Controls.Add(Me.GroupBox2)
        Me.tp_Voice.Controls.Add(Me.GroupBox1)
        Me.tp_Voice.Controls.Add(Me.GroupBox15)
        Me.tp_Voice.Controls.Add(Me.Label73)
        Me.tp_Voice.Controls.Add(Me.Label74)
        Me.tp_Voice.Controls.Add(Me.Label72)
        Me.tp_Voice.Controls.Add(Me.Label65)
        Me.tp_Voice.Controls.Add(Me.Label75)
        Me.tp_Voice.Controls.Add(Me.Label66)
        Me.tp_Voice.Controls.Add(Me.Label64)
        Me.tp_Voice.Controls.Add(Me.Label67)
        Me.tp_Voice.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.tp_Voice.Location = New System.Drawing.Point(4, 42)
        Me.tp_Voice.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.tp_Voice.Name = "tp_Voice"
        Me.tp_Voice.Size = New System.Drawing.Size(684, 648)
        Me.tp_Voice.TabIndex = 0
        Me.tp_Voice.Text = "Word Settings"
        '
        'GroupBox13
        '
        Me.GroupBox13.BackColor = System.Drawing.Color.FromArgb(CType(CType(207,Byte),Integer), CType(CType(224,Byte),Integer), CType(CType(248,Byte),Integer))
        Me.GroupBox13.Controls.Add(Me.chkLocalSigature)
        Me.GroupBox13.Controls.Add(Me.cbSigPlusTS)
        Me.GroupBox13.Controls.Add(Me.txtTabletType)
        Me.GroupBox13.Controls.Add(Me.Label195)
        Me.GroupBox13.Controls.Add(Me.txtIPAddress)
        Me.GroupBox13.Controls.Add(Me.Label191)
        Me.GroupBox13.Controls.Add(Me.txtMultiUSB)
        Me.GroupBox13.Controls.Add(Me.Label188)
        Me.GroupBox13.Controls.Add(Me.txtTabletPortPath)
        Me.GroupBox13.Controls.Add(Me.Label106)
        Me.GroupBox13.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox13.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GroupBox13.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.GroupBox13.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.GroupBox13.Location = New System.Drawing.Point(6, 345)
        Me.GroupBox13.Name = "GroupBox13"
        Me.GroupBox13.Size = New System.Drawing.Size(672, 81)
        Me.GroupBox13.TabIndex = 5
        Me.GroupBox13.TabStop = false
        Me.GroupBox13.Text = "Signature Pad Settings"
        '
        'chkLocalSigature
        '
        Me.chkLocalSigature.AutoSize = true
        Me.chkLocalSigature.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkLocalSigature.Location = New System.Drawing.Point(17, 49)
        Me.chkLocalSigature.Name = "chkLocalSigature"
        Me.chkLocalSigature.Size = New System.Drawing.Size(169, 18)
        Me.chkLocalSigature.TabIndex = 102
        Me.chkLocalSigature.Text = "Enable local Signature Pad"
        Me.chkLocalSigature.UseVisualStyleBackColor = true
        '
        'cbSigPlusTS
        '
        Me.cbSigPlusTS.AutoSize = true
        Me.cbSigPlusTS.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.cbSigPlusTS.Location = New System.Drawing.Point(17, 25)
        Me.cbSigPlusTS.Name = "cbSigPlusTS"
        Me.cbSigPlusTS.Size = New System.Drawing.Size(233, 18)
        Me.cbSigPlusTS.TabIndex = 0
        Me.cbSigPlusTS.Text = "Support for Remote Desktop Services"
        Me.cbSigPlusTS.UseVisualStyleBackColor = true
        '
        'txtTabletType
        '
        Me.txtTabletType.Enabled = false
        Me.txtTabletType.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtTabletType.Location = New System.Drawing.Point(102, 81)
        Me.txtTabletType.MaxLength = 1
        Me.txtTabletType.Name = "txtTabletType"
        Me.txtTabletType.ShortcutsEnabled = false
        Me.txtTabletType.Size = New System.Drawing.Size(33, 22)
        Me.txtTabletType.TabIndex = 100
        Me.txtTabletType.TabStop = false
        Me.txtTabletType.Text = "0"
        Me.txtTabletType.Visible = false
        '
        'Label195
        '
        Me.Label195.AutoSize = true
        Me.Label195.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label195.Location = New System.Drawing.Point(15, 84)
        Me.Label195.Name = "Label195"
        Me.Label195.Size = New System.Drawing.Size(82, 14)
        Me.Label195.TabIndex = 99
        Me.Label195.Text = "Tablet Type :"
        Me.Label195.Visible = false
        '
        'txtIPAddress
        '
        Me.txtIPAddress.Enabled = false
        Me.txtIPAddress.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtIPAddress.Location = New System.Drawing.Point(399, 80)
        Me.txtIPAddress.MaxLength = 15
        Me.txtIPAddress.Name = "txtIPAddress"
        Me.txtIPAddress.ShortcutsEnabled = false
        Me.txtIPAddress.Size = New System.Drawing.Size(148, 22)
        Me.txtIPAddress.TabIndex = 98
        Me.txtIPAddress.TabStop = false
        Me.txtIPAddress.Visible = false
        '
        'Label191
        '
        Me.Label191.AutoSize = true
        Me.Label191.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label191.Location = New System.Drawing.Point(322, 83)
        Me.Label191.Name = "Label191"
        Me.Label191.Size = New System.Drawing.Size(73, 14)
        Me.Label191.TabIndex = 97
        Me.Label191.Text = "IP Address :"
        Me.Label191.Visible = false
        '
        'txtMultiUSB
        '
        Me.txtMultiUSB.Enabled = false
        Me.txtMultiUSB.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtMultiUSB.Location = New System.Drawing.Point(257, 80)
        Me.txtMultiUSB.MaxLength = 1
        Me.txtMultiUSB.Name = "txtMultiUSB"
        Me.txtMultiUSB.ShortcutsEnabled = false
        Me.txtMultiUSB.Size = New System.Drawing.Size(33, 22)
        Me.txtMultiUSB.TabIndex = 96
        Me.txtMultiUSB.TabStop = false
        Me.txtMultiUSB.Text = "0"
        Me.txtMultiUSB.Visible = false
        '
        'Label188
        '
        Me.Label188.AutoSize = true
        Me.Label188.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label188.Location = New System.Drawing.Point(146, 83)
        Me.Label188.Name = "Label188"
        Me.Label188.Size = New System.Drawing.Size(106, 14)
        Me.Label188.TabIndex = 95
        Me.Label188.Text = "Multi USB Enable :"
        Me.Label188.Visible = false
        '
        'txtTabletPortPath
        '
        Me.txtTabletPortPath.Enabled = false
        Me.txtTabletPortPath.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtTabletPortPath.Location = New System.Drawing.Point(414, 25)
        Me.txtTabletPortPath.Name = "txtTabletPortPath"
        Me.txtTabletPortPath.ShortcutsEnabled = false
        Me.txtTabletPortPath.Size = New System.Drawing.Size(183, 22)
        Me.txtTabletPortPath.TabIndex = 1
        '
        'Label106
        '
        Me.Label106.AutoSize = true
        Me.Label106.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label106.Location = New System.Drawing.Point(304, 28)
        Me.Label106.Name = "Label106"
        Me.Label106.Size = New System.Drawing.Size(106, 14)
        Me.Label106.TabIndex = 7
        Me.Label106.Text = "Tablet Port Path :"
        '
        'grb_PatientNotesFooterSettings
        '
        Me.grb_PatientNotesFooterSettings.BackColor = System.Drawing.Color.FromArgb(CType(CType(207,Byte),Integer), CType(CType(224,Byte),Integer), CType(CType(248,Byte),Integer))
        Me.grb_PatientNotesFooterSettings.Controls.Add(Me.rdo_IncludePageNo_No)
        Me.grb_PatientNotesFooterSettings.Controls.Add(Me.rdo_IncludePageNo_Yes)
        Me.grb_PatientNotesFooterSettings.Controls.Add(Me.Label68)
        Me.grb_PatientNotesFooterSettings.Dock = System.Windows.Forms.DockStyle.Top
        Me.grb_PatientNotesFooterSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.grb_PatientNotesFooterSettings.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.grb_PatientNotesFooterSettings.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.grb_PatientNotesFooterSettings.Location = New System.Drawing.Point(6, 284)
        Me.grb_PatientNotesFooterSettings.Name = "grb_PatientNotesFooterSettings"
        Me.grb_PatientNotesFooterSettings.Size = New System.Drawing.Size(672, 61)
        Me.grb_PatientNotesFooterSettings.TabIndex = 4
        Me.grb_PatientNotesFooterSettings.TabStop = false
        Me.grb_PatientNotesFooterSettings.Text = "Patient Notes Footer Settings"
        '
        'rdo_IncludePageNo_No
        '
        Me.rdo_IncludePageNo_No.AutoSize = true
        Me.rdo_IncludePageNo_No.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.rdo_IncludePageNo_No.Location = New System.Drawing.Point(324, 23)
        Me.rdo_IncludePageNo_No.Name = "rdo_IncludePageNo_No"
        Me.rdo_IncludePageNo_No.Size = New System.Drawing.Size(40, 18)
        Me.rdo_IncludePageNo_No.TabIndex = 1
        Me.rdo_IncludePageNo_No.Text = "No"
        Me.rdo_IncludePageNo_No.UseVisualStyleBackColor = true
        '
        'rdo_IncludePageNo_Yes
        '
        Me.rdo_IncludePageNo_Yes.AutoSize = true
        Me.rdo_IncludePageNo_Yes.Checked = true
        Me.rdo_IncludePageNo_Yes.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.rdo_IncludePageNo_Yes.Location = New System.Drawing.Point(267, 23)
        Me.rdo_IncludePageNo_Yes.Name = "rdo_IncludePageNo_Yes"
        Me.rdo_IncludePageNo_Yes.Size = New System.Drawing.Size(45, 18)
        Me.rdo_IncludePageNo_Yes.TabIndex = 0
        Me.rdo_IncludePageNo_Yes.TabStop = true
        Me.rdo_IncludePageNo_Yes.Text = "Yes"
        Me.rdo_IncludePageNo_Yes.UseVisualStyleBackColor = true
        '
        'Label68
        '
        Me.Label68.AutoSize = true
        Me.Label68.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label68.Location = New System.Drawing.Point(14, 25)
        Me.Label68.Name = "Label68"
        Me.Label68.Size = New System.Drawing.Size(248, 14)
        Me.Label68.TabIndex = 7
        Me.Label68.Text = "Include Page No && Patient Name in Footer :"
        '
        'grWordhighlighhcolr
        '
        Me.grWordhighlighhcolr.BackColor = System.Drawing.Color.FromArgb(CType(CType(207,Byte),Integer), CType(CType(224,Byte),Integer), CType(CType(248,Byte),Integer))
        Me.grWordhighlighhcolr.Controls.Add(Me.cmbHighlight)
        Me.grWordhighlighhcolr.Controls.Add(Me.lblcolor)
        Me.grWordhighlighhcolr.Controls.Add(Me.chksethighlightcolr)
        Me.grWordhighlighhcolr.Dock = System.Windows.Forms.DockStyle.Top
        Me.grWordhighlighhcolr.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.grWordhighlighhcolr.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.grWordhighlighhcolr.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.grWordhighlighhcolr.Location = New System.Drawing.Point(6, 227)
        Me.grWordhighlighhcolr.Name = "grWordhighlighhcolr"
        Me.grWordhighlighhcolr.Size = New System.Drawing.Size(672, 57)
        Me.grWordhighlighhcolr.TabIndex = 3
        Me.grWordhighlighhcolr.TabStop = false
        Me.grWordhighlighhcolr.Text = "General Settings"
        '
        'cmbHighlight
        '
        Me.cmbHighlight.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbHighlight.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.cmbHighlight.Location = New System.Drawing.Point(281, 21)
        Me.cmbHighlight.Name = "cmbHighlight"
        Me.cmbHighlight.Size = New System.Drawing.Size(101, 22)
        Me.cmbHighlight.TabIndex = 1
        '
        'lblcolor
        '
        Me.lblcolor.AutoSize = true
        Me.lblcolor.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblcolor.Location = New System.Drawing.Point(192, 25)
        Me.lblcolor.Name = "lblcolor"
        Me.lblcolor.Size = New System.Drawing.Size(86, 14)
        Me.lblcolor.TabIndex = 7
        Me.lblcolor.Text = "Choose Color :"
        '
        'chksethighlightcolr
        '
        Me.chksethighlightcolr.AutoSize = true
        Me.chksethighlightcolr.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chksethighlightcolr.Location = New System.Drawing.Point(17, 23)
        Me.chksethighlightcolr.Name = "chksethighlightcolr"
        Me.chksethighlightcolr.Size = New System.Drawing.Size(141, 18)
        Me.chksethighlightcolr.TabIndex = 0
        Me.chksethighlightcolr.Text = "Set Highlighted Color"
        Me.chksethighlightcolr.UseVisualStyleBackColor = true
        '
        'GroupBox2
        '
        Me.GroupBox2.BackColor = System.Drawing.Color.FromArgb(CType(CType(207,Byte),Integer), CType(CType(224,Byte),Integer), CType(CType(248,Byte),Integer))
        Me.GroupBox2.Controls.Add(Me.Label25)
        Me.GroupBox2.Controls.Add(Me.rbtnNone)
        Me.GroupBox2.Controls.Add(Me.rbtnSelect)
        Me.GroupBox2.Controls.Add(Me.rbtnNotes)
        Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GroupBox2.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.GroupBox2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.GroupBox2.Location = New System.Drawing.Point(6, 170)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(672, 57)
        Me.GroupBox2.TabIndex = 2
        Me.GroupBox2.TabStop = false
        Me.GroupBox2.Text = "Referral Settings"
        '
        'Label25
        '
        Me.Label25.AutoSize = true
        Me.Label25.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label25.Location = New System.Drawing.Point(14, 27)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(179, 14)
        Me.Label25.TabIndex = 31
        Me.Label25.Text = "Referral Letter Should Include :"
        '
        'rbtnNone
        '
        Me.rbtnNone.AutoSize = true
        Me.rbtnNone.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.rbtnNone.Location = New System.Drawing.Point(436, 25)
        Me.rbtnNone.Name = "rbtnNone"
        Me.rbtnNone.Size = New System.Drawing.Size(54, 18)
        Me.rbtnNone.TabIndex = 3
        Me.rbtnNone.Text = "None"
        Me.rbtnNone.UseVisualStyleBackColor = true
        '
        'rbtnSelect
        '
        Me.rbtnSelect.AutoSize = true
        Me.rbtnSelect.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.rbtnSelect.Location = New System.Drawing.Point(318, 25)
        Me.rbtnSelect.Name = "rbtnSelect"
        Me.rbtnSelect.Size = New System.Drawing.Size(109, 18)
        Me.rbtnSelect.TabIndex = 1
        Me.rbtnSelect.Text = "Selected Notes"
        Me.rbtnSelect.UseVisualStyleBackColor = true
        '
        'rbtnNotes
        '
        Me.rbtnNotes.AutoSize = true
        Me.rbtnNotes.Checked = true
        Me.rbtnNotes.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.rbtnNotes.Location = New System.Drawing.Point(194, 25)
        Me.rbtnNotes.Name = "rbtnNotes"
        Me.rbtnNotes.Size = New System.Drawing.Size(122, 18)
        Me.rbtnNotes.TabIndex = 0
        Me.rbtnNotes.TabStop = true
        Me.rbtnNotes.Text = "Complete Notes"
        Me.rbtnNotes.UseVisualStyleBackColor = true
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(207,Byte),Integer), CType(CType(224,Byte),Integer), CType(CType(248,Byte),Integer))
        Me.GroupBox1.Controls.Add(Me.pnlResultBoxPosition)
        Me.GroupBox1.Controls.Add(Me.optYes)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.optNo)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GroupBox1.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.GroupBox1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.GroupBox1.Location = New System.Drawing.Point(6, 62)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(672, 108)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = false
        Me.GroupBox1.Text = "Voice Settings"
        '
        'pnlResultBoxPosition
        '
        Me.pnlResultBoxPosition.Controls.Add(Me.optBottomRight)
        Me.pnlResultBoxPosition.Controls.Add(Me.optBottomLeft)
        Me.pnlResultBoxPosition.Controls.Add(Me.optTopRight)
        Me.pnlResultBoxPosition.Controls.Add(Me.optTopLeft)
        Me.pnlResultBoxPosition.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.pnlResultBoxPosition.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.pnlResultBoxPosition.Location = New System.Drawing.Point(209, 51)
        Me.pnlResultBoxPosition.Name = "pnlResultBoxPosition"
        Me.pnlResultBoxPosition.Size = New System.Drawing.Size(250, 50)
        Me.pnlResultBoxPosition.TabIndex = 3
        '
        'optBottomRight
        '
        Me.optBottomRight.AutoSize = true
        Me.optBottomRight.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.optBottomRight.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.optBottomRight.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.optBottomRight.Location = New System.Drawing.Point(121, 28)
        Me.optBottomRight.Name = "optBottomRight"
        Me.optBottomRight.Size = New System.Drawing.Size(98, 18)
        Me.optBottomRight.TabIndex = 3
        Me.optBottomRight.Text = "Bottom Right"
        '
        'optBottomLeft
        '
        Me.optBottomLeft.AutoSize = true
        Me.optBottomLeft.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.optBottomLeft.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.optBottomLeft.Location = New System.Drawing.Point(5, 28)
        Me.optBottomLeft.Name = "optBottomLeft"
        Me.optBottomLeft.Size = New System.Drawing.Size(92, 18)
        Me.optBottomLeft.TabIndex = 1
        Me.optBottomLeft.Text = "Bottom Left"
        '
        'optTopRight
        '
        Me.optTopRight.AutoSize = true
        Me.optTopRight.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.optTopRight.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.optTopRight.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.optTopRight.Location = New System.Drawing.Point(140, 4)
        Me.optTopRight.Name = "optTopRight"
        Me.optTopRight.Size = New System.Drawing.Size(79, 18)
        Me.optTopRight.TabIndex = 2
        Me.optTopRight.Text = "Top Right"
        '
        'optTopLeft
        '
        Me.optTopLeft.AutoSize = true
        Me.optTopLeft.Checked = true
        Me.optTopLeft.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.optTopLeft.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.optTopLeft.Location = New System.Drawing.Point(5, 4)
        Me.optTopLeft.Name = "optTopLeft"
        Me.optTopLeft.Size = New System.Drawing.Size(77, 18)
        Me.optTopLeft.TabIndex = 0
        Me.optTopLeft.TabStop = true
        Me.optTopLeft.Text = "Top Left"
        '
        'optYes
        '
        Me.optYes.Checked = true
        Me.optYes.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.optYes.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.optYes.Location = New System.Drawing.Point(214, 26)
        Me.optYes.Name = "optYes"
        Me.optYes.Size = New System.Drawing.Size(49, 16)
        Me.optYes.TabIndex = 1
        Me.optYes.TabStop = true
        Me.optYes.Text = "&Yes"
        '
        'Label1
        '
        Me.Label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label1.Location = New System.Drawing.Point(4, 23)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(201, 23)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Show Voice command Result Box :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label2
        '
        Me.Label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label2.Location = New System.Drawing.Point(77, 53)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(128, 14)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Result Box Position :"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'optNo
        '
        Me.optNo.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.optNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.optNo.Location = New System.Drawing.Point(269, 26)
        Me.optNo.Name = "optNo"
        Me.optNo.Size = New System.Drawing.Size(44, 16)
        Me.optNo.TabIndex = 2
        Me.optNo.Text = "&No"
        '
        'GroupBox15
        '
        Me.GroupBox15.BackColor = System.Drawing.Color.FromArgb(CType(CType(207,Byte),Integer), CType(CType(224,Byte),Integer), CType(CType(248,Byte),Integer))
        Me.GroupBox15.Controls.Add(Me.Label203)
        Me.GroupBox15.Controls.Add(Me.numAutoSaveMinutes)
        Me.GroupBox15.Controls.Add(Me.chkAutoSaveExam)
        Me.GroupBox15.Controls.Add(Me.TextBox3)
        Me.GroupBox15.Controls.Add(Me.Label200)
        Me.GroupBox15.Controls.Add(Me.TextBox4)
        Me.GroupBox15.Controls.Add(Me.Label201)
        Me.GroupBox15.Controls.Add(Me.TextBox5)
        Me.GroupBox15.Controls.Add(Me.Label202)
        Me.GroupBox15.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox15.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GroupBox15.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.GroupBox15.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.GroupBox15.Location = New System.Drawing.Point(6, 6)
        Me.GroupBox15.Name = "GroupBox15"
        Me.GroupBox15.Size = New System.Drawing.Size(672, 56)
        Me.GroupBox15.TabIndex = 0
        Me.GroupBox15.TabStop = false
        Me.GroupBox15.Text = "Auto Recovery Settings"
        Me.GroupBox15.Visible = false
        '
        'Label203
        '
        Me.Label203.AutoSize = true
        Me.Label203.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label203.Location = New System.Drawing.Point(285, 25)
        Me.Label203.Name = "Label203"
        Me.Label203.Size = New System.Drawing.Size(49, 14)
        Me.Label203.TabIndex = 103
        Me.Label203.Text = "Minutes"
        '
        'numAutoSaveMinutes
        '
        Me.numAutoSaveMinutes.Enabled = false
        Me.numAutoSaveMinutes.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.numAutoSaveMinutes.Location = New System.Drawing.Point(236, 21)
        Me.numAutoSaveMinutes.Maximum = New Decimal(New Integer() {10, 0, 0, 0})
        Me.numAutoSaveMinutes.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.numAutoSaveMinutes.Name = "numAutoSaveMinutes"
        Me.numAutoSaveMinutes.Size = New System.Drawing.Size(45, 22)
        Me.numAutoSaveMinutes.TabIndex = 1
        Me.numAutoSaveMinutes.Value = New Decimal(New Integer() {2, 0, 0, 0})
        '
        'chkAutoSaveExam
        '
        Me.chkAutoSaveExam.AutoSize = true
        Me.chkAutoSaveExam.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkAutoSaveExam.Location = New System.Drawing.Point(18, 23)
        Me.chkAutoSaveExam.Name = "chkAutoSaveExam"
        Me.chkAutoSaveExam.Size = New System.Drawing.Size(218, 18)
        Me.chkAutoSaveExam.TabIndex = 0
        Me.chkAutoSaveExam.Text = "Auto save Exam Notes after every "
        Me.chkAutoSaveExam.UseVisualStyleBackColor = true
        '
        'TextBox3
        '
        Me.TextBox3.Enabled = false
        Me.TextBox3.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.TextBox3.Location = New System.Drawing.Point(102, 70)
        Me.TextBox3.MaxLength = 1
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.ShortcutsEnabled = false
        Me.TextBox3.Size = New System.Drawing.Size(33, 22)
        Me.TextBox3.TabIndex = 100
        Me.TextBox3.TabStop = false
        Me.TextBox3.Text = "0"
        Me.TextBox3.Visible = false
        '
        'Label200
        '
        Me.Label200.AutoSize = true
        Me.Label200.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label200.Location = New System.Drawing.Point(15, 73)
        Me.Label200.Name = "Label200"
        Me.Label200.Size = New System.Drawing.Size(82, 14)
        Me.Label200.TabIndex = 99
        Me.Label200.Text = "Tablet Type :"
        Me.Label200.Visible = false
        '
        'TextBox4
        '
        Me.TextBox4.Enabled = false
        Me.TextBox4.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.TextBox4.Location = New System.Drawing.Point(399, 69)
        Me.TextBox4.MaxLength = 15
        Me.TextBox4.Name = "TextBox4"
        Me.TextBox4.ShortcutsEnabled = false
        Me.TextBox4.Size = New System.Drawing.Size(148, 22)
        Me.TextBox4.TabIndex = 98
        Me.TextBox4.TabStop = false
        Me.TextBox4.Visible = false
        '
        'Label201
        '
        Me.Label201.AutoSize = true
        Me.Label201.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label201.Location = New System.Drawing.Point(322, 72)
        Me.Label201.Name = "Label201"
        Me.Label201.Size = New System.Drawing.Size(73, 14)
        Me.Label201.TabIndex = 97
        Me.Label201.Text = "IP Address :"
        Me.Label201.Visible = false
        '
        'TextBox5
        '
        Me.TextBox5.Enabled = false
        Me.TextBox5.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.TextBox5.Location = New System.Drawing.Point(257, 69)
        Me.TextBox5.MaxLength = 1
        Me.TextBox5.Name = "TextBox5"
        Me.TextBox5.ShortcutsEnabled = false
        Me.TextBox5.Size = New System.Drawing.Size(33, 22)
        Me.TextBox5.TabIndex = 96
        Me.TextBox5.TabStop = false
        Me.TextBox5.Text = "0"
        Me.TextBox5.Visible = false
        '
        'Label202
        '
        Me.Label202.AutoSize = true
        Me.Label202.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label202.Location = New System.Drawing.Point(146, 72)
        Me.Label202.Name = "Label202"
        Me.Label202.Size = New System.Drawing.Size(106, 14)
        Me.Label202.TabIndex = 95
        Me.Label202.Text = "Multi USB Enable :"
        Me.Label202.Visible = false
        '
        'Label73
        '
        Me.Label73.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label73.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label73.Location = New System.Drawing.Point(6, 1)
        Me.Label73.Name = "Label73"
        Me.Label73.Size = New System.Drawing.Size(672, 5)
        Me.Label73.TabIndex = 37
        Me.Label73.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label74
        '
        Me.Label74.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label74.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label74.Location = New System.Drawing.Point(1, 1)
        Me.Label74.Name = "Label74"
        Me.Label74.Size = New System.Drawing.Size(5, 641)
        Me.Label74.TabIndex = 38
        Me.Label74.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label72
        '
        Me.Label72.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label72.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label72.Location = New System.Drawing.Point(1, 642)
        Me.Label72.Name = "Label72"
        Me.Label72.Size = New System.Drawing.Size(677, 5)
        Me.Label72.TabIndex = 40
        Me.Label72.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label65
        '
        Me.Label65.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label65.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label65.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label65.Location = New System.Drawing.Point(0, 1)
        Me.Label65.Name = "Label65"
        Me.Label65.Size = New System.Drawing.Size(1, 646)
        Me.Label65.TabIndex = 34
        Me.Label65.Text = "label4"
        '
        'Label75
        '
        Me.Label75.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label75.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label75.Location = New System.Drawing.Point(678, 1)
        Me.Label75.Name = "Label75"
        Me.Label75.Size = New System.Drawing.Size(5, 646)
        Me.Label75.TabIndex = 39
        Me.Label75.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label66
        '
        Me.Label66.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label66.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label66.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label66.Location = New System.Drawing.Point(683, 1)
        Me.Label66.Name = "Label66"
        Me.Label66.Size = New System.Drawing.Size(1, 646)
        Me.Label66.TabIndex = 33
        Me.Label66.Text = "label3"
        '
        'Label64
        '
        Me.Label64.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label64.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label64.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label64.Location = New System.Drawing.Point(0, 647)
        Me.Label64.Name = "Label64"
        Me.Label64.Size = New System.Drawing.Size(684, 1)
        Me.Label64.TabIndex = 35
        Me.Label64.Text = "label2"
        '
        'Label67
        '
        Me.Label67.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label67.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label67.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label67.Location = New System.Drawing.Point(0, 0)
        Me.Label67.Name = "Label67"
        Me.Label67.Size = New System.Drawing.Size(684, 1)
        Me.Label67.TabIndex = 32
        Me.Label67.Text = "label1"
        '
        'tp_FAXSettings
        '
        Me.tp_FAXSettings.BackColor = System.Drawing.Color.FromArgb(CType(CType(207,Byte),Integer), CType(CType(224,Byte),Integer), CType(CType(248,Byte),Integer))
        Me.tp_FAXSettings.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tp_FAXSettings.Controls.Add(Me.GroupBox14)
        Me.tp_FAXSettings.Controls.Add(Me.GroupBox7)
        Me.tp_FAXSettings.Controls.Add(Me.Label76)
        Me.tp_FAXSettings.Controls.Add(Me.Label77)
        Me.tp_FAXSettings.Controls.Add(Me.Label78)
        Me.tp_FAXSettings.Controls.Add(Me.Label79)
        Me.tp_FAXSettings.Controls.Add(Me.Label60)
        Me.tp_FAXSettings.Controls.Add(Me.Label61)
        Me.tp_FAXSettings.Controls.Add(Me.Label62)
        Me.tp_FAXSettings.Controls.Add(Me.Label63)
        Me.tp_FAXSettings.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.tp_FAXSettings.Location = New System.Drawing.Point(4, 42)
        Me.tp_FAXSettings.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.tp_FAXSettings.Name = "tp_FAXSettings"
        Me.tp_FAXSettings.Size = New System.Drawing.Size(684, 648)
        Me.tp_FAXSettings.TabIndex = 1
        Me.tp_FAXSettings.Text = "Fax Settings"
        '
        'GroupBox14
        '
        Me.GroupBox14.BackColor = System.Drawing.Color.FromArgb(CType(CType(207,Byte),Integer), CType(CType(224,Byte),Integer), CType(CType(248,Byte),Integer))
        Me.GroupBox14.Controls.Add(Me.Label199)
        Me.GroupBox14.Controls.Add(Me.txtFaxDownloadPath)
        Me.GroupBox14.Controls.Add(Me.btnBrowseDownloadPath)
        Me.GroupBox14.Controls.Add(Me.TextBox1)
        Me.GroupBox14.Controls.Add(Me.Label196)
        Me.GroupBox14.Controls.Add(Me.TextBox2)
        Me.GroupBox14.Controls.Add(Me.Label197)
        Me.GroupBox14.Controls.Add(Me.Label198)
        Me.GroupBox14.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox14.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GroupBox14.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.GroupBox14.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.GroupBox14.Location = New System.Drawing.Point(6, 144)
        Me.GroupBox14.Name = "GroupBox14"
        Me.GroupBox14.Size = New System.Drawing.Size(672, 62)
        Me.GroupBox14.TabIndex = 43
        Me.GroupBox14.TabStop = false
        Me.GroupBox14.Text = "Settings for Remote Desktop Services"
        '
        'Label199
        '
        Me.Label199.AutoSize = true
        Me.Label199.BackColor = System.Drawing.Color.Transparent
        Me.Label199.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label199.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label199.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label199.Location = New System.Drawing.Point(26, 29)
        Me.Label199.Name = "Label199"
        Me.Label199.Size = New System.Drawing.Size(144, 14)
        Me.Label199.TabIndex = 101
        Me.Label199.Text = "Fax Download Directory :"
        Me.Label199.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtFaxDownloadPath
        '
        Me.txtFaxDownloadPath.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtFaxDownloadPath.ForeColor = System.Drawing.Color.Black
        Me.txtFaxDownloadPath.Location = New System.Drawing.Point(172, 25)
        Me.txtFaxDownloadPath.Name = "txtFaxDownloadPath"
        Me.txtFaxDownloadPath.Size = New System.Drawing.Size(289, 22)
        Me.txtFaxDownloadPath.TabIndex = 15
        '
        'btnBrowseDownloadPath
        '
        Me.btnBrowseDownloadPath.BackColor = System.Drawing.Color.White
        Me.btnBrowseDownloadPath.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.btnBrowseDownloadPath.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnBrowseDownloadPath.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255,Byte),Integer), CType(CType(197,Byte),Integer), CType(CType(108,Byte),Integer))
        Me.btnBrowseDownloadPath.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255,Byte),Integer), CType(CType(224,Byte),Integer), CType(CType(160,Byte),Integer))
        Me.btnBrowseDownloadPath.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBrowseDownloadPath.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btnBrowseDownloadPath.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.btnBrowseDownloadPath.Image = CType(resources.GetObject("btnBrowseDownloadPath.Image"),System.Drawing.Image)
        Me.btnBrowseDownloadPath.Location = New System.Drawing.Point(465, 25)
        Me.btnBrowseDownloadPath.Name = "btnBrowseDownloadPath"
        Me.btnBrowseDownloadPath.Size = New System.Drawing.Size(22, 22)
        Me.btnBrowseDownloadPath.TabIndex = 16
        Me.btnBrowseDownloadPath.UseVisualStyleBackColor = false
        '
        'TextBox1
        '
        Me.TextBox1.Enabled = false
        Me.TextBox1.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.TextBox1.Location = New System.Drawing.Point(102, 70)
        Me.TextBox1.MaxLength = 1
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.ShortcutsEnabled = false
        Me.TextBox1.Size = New System.Drawing.Size(33, 22)
        Me.TextBox1.TabIndex = 100
        Me.TextBox1.TabStop = false
        Me.TextBox1.Text = "0"
        Me.TextBox1.Visible = false
        '
        'Label196
        '
        Me.Label196.AutoSize = true
        Me.Label196.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label196.Location = New System.Drawing.Point(15, 73)
        Me.Label196.Name = "Label196"
        Me.Label196.Size = New System.Drawing.Size(82, 14)
        Me.Label196.TabIndex = 99
        Me.Label196.Text = "Tablet Type :"
        Me.Label196.Visible = false
        '
        'TextBox2
        '
        Me.TextBox2.Enabled = false
        Me.TextBox2.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.TextBox2.Location = New System.Drawing.Point(399, 69)
        Me.TextBox2.MaxLength = 15
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.ShortcutsEnabled = false
        Me.TextBox2.Size = New System.Drawing.Size(148, 22)
        Me.TextBox2.TabIndex = 98
        Me.TextBox2.TabStop = false
        Me.TextBox2.Visible = false
        '
        'Label197
        '
        Me.Label197.AutoSize = true
        Me.Label197.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label197.Location = New System.Drawing.Point(322, 72)
        Me.Label197.Name = "Label197"
        Me.Label197.Size = New System.Drawing.Size(73, 14)
        Me.Label197.TabIndex = 97
        Me.Label197.Text = "IP Address :"
        Me.Label197.Visible = false
        '
        'Label198
        '
        Me.Label198.AutoSize = true
        Me.Label198.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label198.Location = New System.Drawing.Point(146, 72)
        Me.Label198.Name = "Label198"
        Me.Label198.Size = New System.Drawing.Size(106, 14)
        Me.Label198.TabIndex = 95
        Me.Label198.Text = "Multi USB Enable :"
        Me.Label198.Visible = false
        '
        'GroupBox7
        '
        Me.GroupBox7.Controls.Add(Me.chkHandleFAXIssue)
        Me.GroupBox7.Controls.Add(Me.chkCoverPage)
        Me.GroupBox7.Controls.Add(Me.cmbFAXPrinter)
        Me.GroupBox7.Controls.Add(Me.Label6)
        Me.GroupBox7.Controls.Add(Me.Label7)
        Me.GroupBox7.Controls.Add(Me.txtFAXOutputDirectory)
        Me.GroupBox7.Controls.Add(Me.btnBrowseFAXOutputDirectory)
        Me.GroupBox7.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox7.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.GroupBox7.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.GroupBox7.Location = New System.Drawing.Point(6, 6)
        Me.GroupBox7.Name = "GroupBox7"
        Me.GroupBox7.Size = New System.Drawing.Size(672, 138)
        Me.GroupBox7.TabIndex = 25
        Me.GroupBox7.TabStop = false
        Me.GroupBox7.Text = "Fax Settings"
        '
        'chkHandleFAXIssue
        '
        Me.chkHandleFAXIssue.AutoSize = true
        Me.chkHandleFAXIssue.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkHandleFAXIssue.Location = New System.Drawing.Point(172, 107)
        Me.chkHandleFAXIssue.Name = "chkHandleFAXIssue"
        Me.chkHandleFAXIssue.Size = New System.Drawing.Size(205, 18)
        Me.chkHandleFAXIssue.TabIndex = 15
        Me.chkHandleFAXIssue.Text = "Same Cover Page for all Referrals"
        Me.chkHandleFAXIssue.UseVisualStyleBackColor = true
        '
        'chkCoverPage
        '
        Me.chkCoverPage.AutoSize = true
        Me.chkCoverPage.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkCoverPage.Location = New System.Drawing.Point(172, 82)
        Me.chkCoverPage.Name = "chkCoverPage"
        Me.chkCoverPage.Size = New System.Drawing.Size(132, 18)
        Me.chkCoverPage.TabIndex = 15
        Me.chkCoverPage.Text = "Include Cover Page"
        Me.chkCoverPage.UseVisualStyleBackColor = true
        '
        'cmbFAXPrinter
        '
        Me.cmbFAXPrinter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbFAXPrinter.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.cmbFAXPrinter.ForeColor = System.Drawing.Color.Black
        Me.cmbFAXPrinter.Location = New System.Drawing.Point(172, 24)
        Me.cmbFAXPrinter.Name = "cmbFAXPrinter"
        Me.cmbFAXPrinter.Size = New System.Drawing.Size(289, 22)
        Me.cmbFAXPrinter.TabIndex = 9
        '
        'Label6
        '
        Me.Label6.AutoSize = true
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label6.Location = New System.Drawing.Point(62, 28)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(108, 14)
        Me.Label6.TabIndex = 8
        Me.Label6.Text = "Fax Printer Name :"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label7
        '
        Me.Label7.AutoSize = true
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label7.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label7.Location = New System.Drawing.Point(40, 57)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(130, 14)
        Me.Label7.TabIndex = 10
        Me.Label7.Text = "Fax Output Directory :"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtFAXOutputDirectory
        '
        Me.txtFAXOutputDirectory.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtFAXOutputDirectory.ForeColor = System.Drawing.Color.Black
        Me.txtFAXOutputDirectory.Location = New System.Drawing.Point(172, 53)
        Me.txtFAXOutputDirectory.Name = "txtFAXOutputDirectory"
        Me.txtFAXOutputDirectory.Size = New System.Drawing.Size(289, 22)
        Me.txtFAXOutputDirectory.TabIndex = 11
        '
        'btnBrowseFAXOutputDirectory
        '
        Me.btnBrowseFAXOutputDirectory.BackColor = System.Drawing.Color.White
        Me.btnBrowseFAXOutputDirectory.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.btnBrowseFAXOutputDirectory.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnBrowseFAXOutputDirectory.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255,Byte),Integer), CType(CType(197,Byte),Integer), CType(CType(108,Byte),Integer))
        Me.btnBrowseFAXOutputDirectory.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255,Byte),Integer), CType(CType(224,Byte),Integer), CType(CType(160,Byte),Integer))
        Me.btnBrowseFAXOutputDirectory.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBrowseFAXOutputDirectory.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btnBrowseFAXOutputDirectory.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.btnBrowseFAXOutputDirectory.Image = CType(resources.GetObject("btnBrowseFAXOutputDirectory.Image"),System.Drawing.Image)
        Me.btnBrowseFAXOutputDirectory.Location = New System.Drawing.Point(465, 53)
        Me.btnBrowseFAXOutputDirectory.Name = "btnBrowseFAXOutputDirectory"
        Me.btnBrowseFAXOutputDirectory.Size = New System.Drawing.Size(22, 22)
        Me.btnBrowseFAXOutputDirectory.TabIndex = 12
        Me.btnBrowseFAXOutputDirectory.UseVisualStyleBackColor = false
        '
        'Label76
        '
        Me.Label76.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label76.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label76.Location = New System.Drawing.Point(6, 642)
        Me.Label76.Name = "Label76"
        Me.Label76.Size = New System.Drawing.Size(672, 5)
        Me.Label76.TabIndex = 24
        Me.Label76.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label77
        '
        Me.Label77.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label77.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label77.Location = New System.Drawing.Point(6, 1)
        Me.Label77.Name = "Label77"
        Me.Label77.Size = New System.Drawing.Size(672, 5)
        Me.Label77.TabIndex = 21
        Me.Label77.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label78
        '
        Me.Label78.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label78.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label78.Location = New System.Drawing.Point(1, 1)
        Me.Label78.Name = "Label78"
        Me.Label78.Size = New System.Drawing.Size(5, 646)
        Me.Label78.TabIndex = 22
        Me.Label78.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label79
        '
        Me.Label79.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label79.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label79.Location = New System.Drawing.Point(678, 1)
        Me.Label79.Name = "Label79"
        Me.Label79.Size = New System.Drawing.Size(5, 646)
        Me.Label79.TabIndex = 23
        Me.Label79.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label60
        '
        Me.Label60.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label60.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label60.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label60.Location = New System.Drawing.Point(1, 647)
        Me.Label60.Name = "Label60"
        Me.Label60.Size = New System.Drawing.Size(682, 1)
        Me.Label60.TabIndex = 18
        Me.Label60.Text = "label2"
        '
        'Label61
        '
        Me.Label61.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label61.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label61.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label61.Location = New System.Drawing.Point(0, 1)
        Me.Label61.Name = "Label61"
        Me.Label61.Size = New System.Drawing.Size(1, 647)
        Me.Label61.TabIndex = 17
        Me.Label61.Text = "label4"
        '
        'Label62
        '
        Me.Label62.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label62.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label62.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label62.Location = New System.Drawing.Point(683, 1)
        Me.Label62.Name = "Label62"
        Me.Label62.Size = New System.Drawing.Size(1, 647)
        Me.Label62.TabIndex = 16
        Me.Label62.Text = "label3"
        '
        'Label63
        '
        Me.Label63.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label63.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label63.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label63.Location = New System.Drawing.Point(0, 0)
        Me.Label63.Name = "Label63"
        Me.Label63.Size = New System.Drawing.Size(684, 1)
        Me.Label63.TabIndex = 15
        Me.Label63.Text = "label1"
        '
        'tp_ServerPaths
        '
        Me.tp_ServerPaths.BackColor = System.Drawing.Color.FromArgb(CType(CType(207,Byte),Integer), CType(CType(224,Byte),Integer), CType(CType(248,Byte),Integer))
        Me.tp_ServerPaths.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tp_ServerPaths.Controls.Add(Me.GroupBox8)
        Me.tp_ServerPaths.Controls.Add(Me.Label80)
        Me.tp_ServerPaths.Controls.Add(Me.Label81)
        Me.tp_ServerPaths.Controls.Add(Me.Label82)
        Me.tp_ServerPaths.Controls.Add(Me.Label83)
        Me.tp_ServerPaths.Controls.Add(Me.Label56)
        Me.tp_ServerPaths.Controls.Add(Me.Label57)
        Me.tp_ServerPaths.Controls.Add(Me.Label58)
        Me.tp_ServerPaths.Controls.Add(Me.Label59)
        Me.tp_ServerPaths.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.tp_ServerPaths.Location = New System.Drawing.Point(4, 42)
        Me.tp_ServerPaths.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.tp_ServerPaths.Name = "tp_ServerPaths"
        Me.tp_ServerPaths.Size = New System.Drawing.Size(684, 648)
        Me.tp_ServerPaths.TabIndex = 2
        Me.tp_ServerPaths.Text = "Server Path"
        '
        'GroupBox8
        '
        Me.GroupBox8.Controls.Add(Me.txtDICOMPath)
        Me.GroupBox8.Controls.Add(Me.lblDICOMPath)
        Me.GroupBox8.Controls.Add(Me.btnBrowseDICOMPath)
        Me.GroupBox8.Controls.Add(Me.txtVMSPath)
        Me.GroupBox8.Controls.Add(Me.Label9)
        Me.GroupBox8.Controls.Add(Me.txtServerPath)
        Me.GroupBox8.Controls.Add(Me.btnServerpath)
        Me.GroupBox8.Controls.Add(Me.Label8)
        Me.GroupBox8.Controls.Add(Me.txtDMSPath)
        Me.GroupBox8.Controls.Add(Me.btnBrowseDMSPath)
        Me.GroupBox8.Controls.Add(Me.lblDMSMessage)
        Me.GroupBox8.Controls.Add(Me.Label22)
        Me.GroupBox8.Controls.Add(Me.btnBrowseVMSPath)
        Me.GroupBox8.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox8.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.GroupBox8.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.GroupBox8.Location = New System.Drawing.Point(6, 6)
        Me.GroupBox8.Name = "GroupBox8"
        Me.GroupBox8.Size = New System.Drawing.Size(672, 175)
        Me.GroupBox8.TabIndex = 28
        Me.GroupBox8.TabStop = false
        Me.GroupBox8.Text = "Server Path"
        '
        'txtDICOMPath
        '
        Me.txtDICOMPath.Font = New System.Drawing.Font("Verdana", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtDICOMPath.Location = New System.Drawing.Point(114, 134)
        Me.txtDICOMPath.Name = "txtDICOMPath"
        Me.txtDICOMPath.Size = New System.Drawing.Size(466, 22)
        Me.txtDICOMPath.TabIndex = 21
        '
        'lblDICOMPath
        '
        Me.lblDICOMPath.AutoSize = true
        Me.lblDICOMPath.BackColor = System.Drawing.Color.Transparent
        Me.lblDICOMPath.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblDICOMPath.Location = New System.Drawing.Point(29, 138)
        Me.lblDICOMPath.Name = "lblDICOMPath"
        Me.lblDICOMPath.Size = New System.Drawing.Size(81, 14)
        Me.lblDICOMPath.TabIndex = 20
        Me.lblDICOMPath.Text = "DICOM Path :"
        Me.lblDICOMPath.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnBrowseDICOMPath
        '
        Me.btnBrowseDICOMPath.BackColor = System.Drawing.Color.White
        Me.btnBrowseDICOMPath.BackgroundImage = CType(resources.GetObject("btnBrowseDICOMPath.BackgroundImage"),System.Drawing.Image)
        Me.btnBrowseDICOMPath.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnBrowseDICOMPath.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.btnBrowseDICOMPath.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255,Byte),Integer), CType(CType(197,Byte),Integer), CType(CType(108,Byte),Integer))
        Me.btnBrowseDICOMPath.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255,Byte),Integer), CType(CType(224,Byte),Integer), CType(CType(160,Byte),Integer))
        Me.btnBrowseDICOMPath.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBrowseDICOMPath.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btnBrowseDICOMPath.Image = CType(resources.GetObject("btnBrowseDICOMPath.Image"),System.Drawing.Image)
        Me.btnBrowseDICOMPath.Location = New System.Drawing.Point(584, 134)
        Me.btnBrowseDICOMPath.Name = "btnBrowseDICOMPath"
        Me.btnBrowseDICOMPath.Size = New System.Drawing.Size(22, 22)
        Me.btnBrowseDICOMPath.TabIndex = 22
        Me.btnBrowseDICOMPath.UseVisualStyleBackColor = false
        '
        'txtVMSPath
        '
        Me.txtVMSPath.Font = New System.Drawing.Font("Verdana", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtVMSPath.Location = New System.Drawing.Point(114, 100)
        Me.txtVMSPath.Name = "txtVMSPath"
        Me.txtVMSPath.Size = New System.Drawing.Size(466, 22)
        Me.txtVMSPath.TabIndex = 18
        '
        'Label9
        '
        Me.Label9.AutoSize = true
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label9.Location = New System.Drawing.Point(31, 70)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(79, 14)
        Me.Label9.TabIndex = 13
        Me.Label9.Text = "Server Path :"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtServerPath
        '
        Me.txtServerPath.Font = New System.Drawing.Font("Verdana", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtServerPath.Location = New System.Drawing.Point(114, 66)
        Me.txtServerPath.Name = "txtServerPath"
        Me.txtServerPath.Size = New System.Drawing.Size(466, 22)
        Me.txtServerPath.TabIndex = 14
        '
        'btnServerpath
        '
        Me.btnServerpath.BackColor = System.Drawing.Color.White
        Me.btnServerpath.BackgroundImage = CType(resources.GetObject("btnServerpath.BackgroundImage"),System.Drawing.Image)
        Me.btnServerpath.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnServerpath.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.btnServerpath.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255,Byte),Integer), CType(CType(197,Byte),Integer), CType(CType(108,Byte),Integer))
        Me.btnServerpath.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255,Byte),Integer), CType(CType(224,Byte),Integer), CType(CType(160,Byte),Integer))
        Me.btnServerpath.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnServerpath.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btnServerpath.Image = CType(resources.GetObject("btnServerpath.Image"),System.Drawing.Image)
        Me.btnServerpath.Location = New System.Drawing.Point(584, 66)
        Me.btnServerpath.Name = "btnServerpath"
        Me.btnServerpath.Size = New System.Drawing.Size(22, 22)
        Me.btnServerpath.TabIndex = 15
        Me.btnServerpath.UseVisualStyleBackColor = false
        '
        'Label8
        '
        Me.Label8.AutoSize = true
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label8.Location = New System.Drawing.Point(42, 29)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(68, 14)
        Me.Label8.TabIndex = 10
        Me.Label8.Text = "DMS Path :"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtDMSPath
        '
        Me.txtDMSPath.Font = New System.Drawing.Font("Verdana", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtDMSPath.Location = New System.Drawing.Point(114, 25)
        Me.txtDMSPath.Name = "txtDMSPath"
        Me.txtDMSPath.Size = New System.Drawing.Size(466, 22)
        Me.txtDMSPath.TabIndex = 11
        '
        'btnBrowseDMSPath
        '
        Me.btnBrowseDMSPath.BackColor = System.Drawing.Color.White
        Me.btnBrowseDMSPath.BackgroundImage = CType(resources.GetObject("btnBrowseDMSPath.BackgroundImage"),System.Drawing.Image)
        Me.btnBrowseDMSPath.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnBrowseDMSPath.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.btnBrowseDMSPath.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255,Byte),Integer), CType(CType(197,Byte),Integer), CType(CType(108,Byte),Integer))
        Me.btnBrowseDMSPath.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255,Byte),Integer), CType(CType(224,Byte),Integer), CType(CType(160,Byte),Integer))
        Me.btnBrowseDMSPath.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBrowseDMSPath.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btnBrowseDMSPath.Image = CType(resources.GetObject("btnBrowseDMSPath.Image"),System.Drawing.Image)
        Me.btnBrowseDMSPath.Location = New System.Drawing.Point(584, 25)
        Me.btnBrowseDMSPath.Name = "btnBrowseDMSPath"
        Me.btnBrowseDMSPath.Size = New System.Drawing.Size(22, 22)
        Me.btnBrowseDMSPath.TabIndex = 12
        Me.btnBrowseDMSPath.UseVisualStyleBackColor = false
        '
        'lblDMSMessage
        '
        Me.lblDMSMessage.AutoSize = true
        Me.lblDMSMessage.BackColor = System.Drawing.Color.Transparent
        Me.lblDMSMessage.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblDMSMessage.ForeColor = System.Drawing.Color.Chocolate
        Me.lblDMSMessage.Location = New System.Drawing.Point(114, 49)
        Me.lblDMSMessage.Name = "lblDMSMessage"
        Me.lblDMSMessage.Size = New System.Drawing.Size(257, 13)
        Me.lblDMSMessage.TabIndex = 16
        Me.lblDMSMessage.Text = "Scan Document or View Document is opened"
        Me.lblDMSMessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label22
        '
        Me.Label22.AutoSize = true
        Me.Label22.BackColor = System.Drawing.Color.Transparent
        Me.Label22.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label22.Location = New System.Drawing.Point(35, 104)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(75, 14)
        Me.Label22.TabIndex = 17
        Me.Label22.Text = "Video Path :"
        Me.Label22.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnBrowseVMSPath
        '
        Me.btnBrowseVMSPath.BackColor = System.Drawing.Color.White
        Me.btnBrowseVMSPath.BackgroundImage = CType(resources.GetObject("btnBrowseVMSPath.BackgroundImage"),System.Drawing.Image)
        Me.btnBrowseVMSPath.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnBrowseVMSPath.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.btnBrowseVMSPath.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255,Byte),Integer), CType(CType(197,Byte),Integer), CType(CType(108,Byte),Integer))
        Me.btnBrowseVMSPath.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255,Byte),Integer), CType(CType(224,Byte),Integer), CType(CType(160,Byte),Integer))
        Me.btnBrowseVMSPath.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBrowseVMSPath.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btnBrowseVMSPath.Image = CType(resources.GetObject("btnBrowseVMSPath.Image"),System.Drawing.Image)
        Me.btnBrowseVMSPath.Location = New System.Drawing.Point(584, 100)
        Me.btnBrowseVMSPath.Name = "btnBrowseVMSPath"
        Me.btnBrowseVMSPath.Size = New System.Drawing.Size(22, 22)
        Me.btnBrowseVMSPath.TabIndex = 19
        Me.btnBrowseVMSPath.UseVisualStyleBackColor = false
        '
        'Label80
        '
        Me.Label80.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label80.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label80.Location = New System.Drawing.Point(6, 642)
        Me.Label80.Name = "Label80"
        Me.Label80.Size = New System.Drawing.Size(672, 5)
        Me.Label80.TabIndex = 27
        Me.Label80.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label81
        '
        Me.Label81.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label81.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label81.Location = New System.Drawing.Point(6, 1)
        Me.Label81.Name = "Label81"
        Me.Label81.Size = New System.Drawing.Size(672, 5)
        Me.Label81.TabIndex = 24
        Me.Label81.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label82
        '
        Me.Label82.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label82.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label82.Location = New System.Drawing.Point(1, 1)
        Me.Label82.Name = "Label82"
        Me.Label82.Size = New System.Drawing.Size(5, 646)
        Me.Label82.TabIndex = 25
        Me.Label82.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label83
        '
        Me.Label83.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label83.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label83.Location = New System.Drawing.Point(678, 1)
        Me.Label83.Name = "Label83"
        Me.Label83.Size = New System.Drawing.Size(5, 646)
        Me.Label83.TabIndex = 26
        Me.Label83.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label56
        '
        Me.Label56.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label56.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label56.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label56.Location = New System.Drawing.Point(1, 647)
        Me.Label56.Name = "Label56"
        Me.Label56.Size = New System.Drawing.Size(682, 1)
        Me.Label56.TabIndex = 23
        Me.Label56.Text = "label2"
        '
        'Label57
        '
        Me.Label57.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label57.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label57.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label57.Location = New System.Drawing.Point(0, 1)
        Me.Label57.Name = "Label57"
        Me.Label57.Size = New System.Drawing.Size(1, 647)
        Me.Label57.TabIndex = 22
        Me.Label57.Text = "label4"
        '
        'Label58
        '
        Me.Label58.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label58.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label58.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label58.Location = New System.Drawing.Point(683, 1)
        Me.Label58.Name = "Label58"
        Me.Label58.Size = New System.Drawing.Size(1, 647)
        Me.Label58.TabIndex = 21
        Me.Label58.Text = "label3"
        '
        'Label59
        '
        Me.Label59.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label59.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label59.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label59.Location = New System.Drawing.Point(0, 0)
        Me.Label59.Name = "Label59"
        Me.Label59.Size = New System.Drawing.Size(684, 1)
        Me.Label59.TabIndex = 20
        Me.Label59.Text = "label1"
        '
        'tp_DrugInteraction
        '
        Me.tp_DrugInteraction.BackColor = System.Drawing.Color.FromArgb(CType(CType(207,Byte),Integer), CType(CType(224,Byte),Integer), CType(CType(248,Byte),Integer))
        Me.tp_DrugInteraction.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tp_DrugInteraction.Controls.Add(Me.pnlDI_Main)
        Me.tp_DrugInteraction.Controls.Add(Me.Label84)
        Me.tp_DrugInteraction.Controls.Add(Me.Label85)
        Me.tp_DrugInteraction.Controls.Add(Me.Label86)
        Me.tp_DrugInteraction.Controls.Add(Me.Label87)
        Me.tp_DrugInteraction.Controls.Add(Me.Label52)
        Me.tp_DrugInteraction.Controls.Add(Me.Label53)
        Me.tp_DrugInteraction.Controls.Add(Me.Label54)
        Me.tp_DrugInteraction.Controls.Add(Me.Label55)
        Me.tp_DrugInteraction.ForeColor = System.Drawing.Color.Black
        Me.tp_DrugInteraction.Location = New System.Drawing.Point(4, 42)
        Me.tp_DrugInteraction.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.tp_DrugInteraction.Name = "tp_DrugInteraction"
        Me.tp_DrugInteraction.Size = New System.Drawing.Size(684, 648)
        Me.tp_DrugInteraction.TabIndex = 3
        Me.tp_DrugInteraction.Text = " Drug Interaction "
        Me.tp_DrugInteraction.UseVisualStyleBackColor = true
        '
        'pnlDI_Main
        '
        Me.pnlDI_Main.BackColor = System.Drawing.Color.FromArgb(CType(CType(207,Byte),Integer), CType(CType(224,Byte),Integer), CType(CType(248,Byte),Integer))
        Me.pnlDI_Main.Controls.Add(Me.GroupBox22)
        Me.pnlDI_Main.Controls.Add(Me.chkDrugAlert)
        Me.pnlDI_Main.Controls.Add(Me.grpDrugInteraction)
        Me.pnlDI_Main.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlDI_Main.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.pnlDI_Main.Location = New System.Drawing.Point(6, 6)
        Me.pnlDI_Main.Name = "pnlDI_Main"
        Me.pnlDI_Main.Size = New System.Drawing.Size(672, 636)
        Me.pnlDI_Main.TabIndex = 1
        '
        'GroupBox22
        '
        Me.GroupBox22.BackColor = System.Drawing.Color.FromArgb(CType(CType(207,Byte),Integer), CType(CType(224,Byte),Integer), CType(CType(248,Byte),Integer))
        Me.GroupBox22.Controls.Add(Me.chkDrugDiseaseInteraction)
        Me.GroupBox22.Controls.Add(Me.chkDrugFoodInteraction)
        Me.GroupBox22.Controls.Add(Me.chkDrugAllergyInteraction)
        Me.GroupBox22.Controls.Add(Me.chkAdverseDrugEffect)
        Me.GroupBox22.Controls.Add(Me.chkDrugDrugInteraction)
        Me.GroupBox22.Controls.Add(Me.chkDuplicateTherapy)
        Me.GroupBox22.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GroupBox22.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.GroupBox22.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.GroupBox22.Location = New System.Drawing.Point(10, 178)
        Me.GroupBox22.Name = "GroupBox22"
        Me.GroupBox22.Size = New System.Drawing.Size(648, 120)
        Me.GroupBox22.TabIndex = 2
        Me.GroupBox22.TabStop = false
        Me.GroupBox22.Text = "Drug Interaction Alert"
        '
        'chkDrugDiseaseInteraction
        '
        Me.chkDrugDiseaseInteraction.AutoSize = true
        Me.chkDrugDiseaseInteraction.Font = New System.Drawing.Font("Verdana", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkDrugDiseaseInteraction.Location = New System.Drawing.Point(43, 77)
        Me.chkDrugDiseaseInteraction.Name = "chkDrugDiseaseInteraction"
        Me.chkDrugDiseaseInteraction.Size = New System.Drawing.Size(217, 18)
        Me.chkDrugDiseaseInteraction.TabIndex = 2
        Me.chkDrugDiseaseInteraction.Text = "Drug-Disease Interaction Alert"
        '
        'chkDrugFoodInteraction
        '
        Me.chkDrugFoodInteraction.AutoSize = true
        Me.chkDrugFoodInteraction.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkDrugFoodInteraction.Location = New System.Drawing.Point(346, 25)
        Me.chkDrugFoodInteraction.Name = "chkDrugFoodInteraction"
        Me.chkDrugFoodInteraction.Size = New System.Drawing.Size(177, 18)
        Me.chkDrugFoodInteraction.TabIndex = 3
        Me.chkDrugFoodInteraction.Text = "Drug-Food Interaction Alert"
        '
        'chkDrugAllergyInteraction
        '
        Me.chkDrugAllergyInteraction.AutoSize = true
        Me.chkDrugAllergyInteraction.Checked = true
        Me.chkDrugAllergyInteraction.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkDrugAllergyInteraction.Enabled = false
        Me.chkDrugAllergyInteraction.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkDrugAllergyInteraction.Location = New System.Drawing.Point(43, 51)
        Me.chkDrugAllergyInteraction.Name = "chkDrugAllergyInteraction"
        Me.chkDrugAllergyInteraction.Size = New System.Drawing.Size(186, 18)
        Me.chkDrugAllergyInteraction.TabIndex = 1
        Me.chkDrugAllergyInteraction.Text = "Drug-Allergy Interaction Alert"
        '
        'chkAdverseDrugEffect
        '
        Me.chkAdverseDrugEffect.AutoSize = true
        Me.chkAdverseDrugEffect.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkAdverseDrugEffect.Location = New System.Drawing.Point(346, 75)
        Me.chkAdverseDrugEffect.Name = "chkAdverseDrugEffect"
        Me.chkAdverseDrugEffect.Size = New System.Drawing.Size(167, 18)
        Me.chkAdverseDrugEffect.TabIndex = 5
        Me.chkAdverseDrugEffect.Text = "Adverse Drug Effect Alert"
        '
        'chkDrugDrugInteraction
        '
        Me.chkDrugDrugInteraction.AutoSize = true
        Me.chkDrugDrugInteraction.Checked = true
        Me.chkDrugDrugInteraction.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkDrugDrugInteraction.Enabled = false
        Me.chkDrugDrugInteraction.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkDrugDrugInteraction.Location = New System.Drawing.Point(43, 26)
        Me.chkDrugDrugInteraction.Name = "chkDrugDrugInteraction"
        Me.chkDrugDrugInteraction.Size = New System.Drawing.Size(176, 18)
        Me.chkDrugDrugInteraction.TabIndex = 0
        Me.chkDrugDrugInteraction.Text = "Drug-Drug Interaction Alert"
        '
        'chkDuplicateTherapy
        '
        Me.chkDuplicateTherapy.AutoSize = true
        Me.chkDuplicateTherapy.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkDuplicateTherapy.Location = New System.Drawing.Point(346, 50)
        Me.chkDuplicateTherapy.Name = "chkDuplicateTherapy"
        Me.chkDuplicateTherapy.Size = New System.Drawing.Size(155, 18)
        Me.chkDuplicateTherapy.TabIndex = 4
        Me.chkDuplicateTherapy.Text = "Duplicate Therapy Alert"
        '
        'chkDrugAlert
        '
        Me.chkDrugAlert.AutoSize = true
        Me.chkDrugAlert.Font = New System.Drawing.Font("Verdana", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkDrugAlert.Location = New System.Drawing.Point(13, 314)
        Me.chkDrugAlert.Name = "chkDrugAlert"
        Me.chkDrugAlert.Size = New System.Drawing.Size(152, 18)
        Me.chkDrugAlert.TabIndex = 6
        Me.chkDrugAlert.Text = "Show Override Alert"
        '
        'grpDrugInteraction
        '
        Me.grpDrugInteraction.BackColor = System.Drawing.Color.FromArgb(CType(CType(207,Byte),Integer), CType(CType(224,Byte),Integer), CType(CType(248,Byte),Integer))
        Me.grpDrugInteraction.Controls.Add(Me.pnlDrugInteraction)
        Me.grpDrugInteraction.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.grpDrugInteraction.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.grpDrugInteraction.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.grpDrugInteraction.Location = New System.Drawing.Point(10, 21)
        Me.grpDrugInteraction.Name = "grpDrugInteraction"
        Me.grpDrugInteraction.Size = New System.Drawing.Size(648, 141)
        Me.grpDrugInteraction.TabIndex = 3
        Me.grpDrugInteraction.TabStop = false
        Me.grpDrugInteraction.Text = "Drug Interaction"
        '
        'pnlDrugInteraction
        '
        Me.pnlDrugInteraction.Controls.Add(Me.cmbADEOnset)
        Me.pnlDrugInteraction.Controls.Add(Me.Label204)
        Me.pnlDrugInteraction.Controls.Add(Me.cmbDFADoc)
        Me.pnlDrugInteraction.Controls.Add(Me.Label18)
        Me.pnlDrugInteraction.Controls.Add(Me.cmbDIDoc)
        Me.pnlDrugInteraction.Controls.Add(Me.Label17)
        Me.pnlDrugInteraction.Controls.Add(Me.cmbDFA)
        Me.pnlDrugInteraction.Controls.Add(Me.Label16)
        Me.pnlDrugInteraction.Controls.Add(Me.cmbDI)
        Me.pnlDrugInteraction.Controls.Add(Me.Label15)
        Me.pnlDrugInteraction.Controls.Add(Me.cmbADE)
        Me.pnlDrugInteraction.Controls.Add(Me.Label14)
        Me.pnlDrugInteraction.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlDrugInteraction.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.pnlDrugInteraction.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.pnlDrugInteraction.Location = New System.Drawing.Point(3, 18)
        Me.pnlDrugInteraction.Name = "pnlDrugInteraction"
        Me.pnlDrugInteraction.Size = New System.Drawing.Size(642, 120)
        Me.pnlDrugInteraction.TabIndex = 0
        '
        'cmbADEOnset
        '
        Me.cmbADEOnset.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbADEOnset.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.cmbADEOnset.Location = New System.Drawing.Point(456, 10)
        Me.cmbADEOnset.Name = "cmbADEOnset"
        Me.cmbADEOnset.Size = New System.Drawing.Size(157, 22)
        Me.cmbADEOnset.TabIndex = 29
        '
        'Label204
        '
        Me.Label204.AutoSize = true
        Me.Label204.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label204.Location = New System.Drawing.Point(346, 14)
        Me.Label204.Name = "Label204"
        Me.Label204.Size = New System.Drawing.Size(107, 14)
        Me.Label204.TabIndex = 30
        Me.Label204.Text = "ADE Onset Level :"
        Me.Label204.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmbDFADoc
        '
        Me.cmbDFADoc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDFADoc.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.cmbDFADoc.Location = New System.Drawing.Point(456, 73)
        Me.cmbDFADoc.Name = "cmbDFADoc"
        Me.cmbDFADoc.Size = New System.Drawing.Size(157, 22)
        Me.cmbDFADoc.TabIndex = 4
        '
        'Label18
        '
        Me.Label18.AutoSize = true
        Me.Label18.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label18.Location = New System.Drawing.Point(323, 77)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(130, 14)
        Me.Label18.TabIndex = 28
        Me.Label18.Text = "DFA Document Level :"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmbDIDoc
        '
        Me.cmbDIDoc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDIDoc.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.cmbDIDoc.Location = New System.Drawing.Point(456, 42)
        Me.cmbDIDoc.Name = "cmbDIDoc"
        Me.cmbDIDoc.Size = New System.Drawing.Size(157, 22)
        Me.cmbDIDoc.TabIndex = 3
        '
        'Label17
        '
        Me.Label17.AutoSize = true
        Me.Label17.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label17.Location = New System.Drawing.Point(333, 46)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(120, 14)
        Me.Label17.TabIndex = 26
        Me.Label17.Text = "DI Document Level :"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmbDFA
        '
        Me.cmbDFA.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDFA.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.cmbDFA.Location = New System.Drawing.Point(159, 77)
        Me.cmbDFA.Name = "cmbDFA"
        Me.cmbDFA.Size = New System.Drawing.Size(157, 22)
        Me.cmbDFA.TabIndex = 2
        '
        'Label16
        '
        Me.Label16.AutoSize = true
        Me.Label16.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label16.Location = New System.Drawing.Point(39, 81)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(117, 14)
        Me.Label16.TabIndex = 24
        Me.Label16.Text = "DFA Severity Level :"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmbDI
        '
        Me.cmbDI.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDI.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.cmbDI.Location = New System.Drawing.Point(159, 46)
        Me.cmbDI.Name = "cmbDI"
        Me.cmbDI.Size = New System.Drawing.Size(157, 22)
        Me.cmbDI.TabIndex = 1
        '
        'Label15
        '
        Me.Label15.AutoSize = true
        Me.Label15.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label15.Location = New System.Drawing.Point(49, 50)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(107, 14)
        Me.Label15.TabIndex = 22
        Me.Label15.Text = "DI Severity Level :"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmbADE
        '
        Me.cmbADE.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbADE.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.cmbADE.Location = New System.Drawing.Point(159, 13)
        Me.cmbADE.Name = "cmbADE"
        Me.cmbADE.Size = New System.Drawing.Size(157, 22)
        Me.cmbADE.TabIndex = 0
        '
        'Label14
        '
        Me.Label14.AutoSize = true
        Me.Label14.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label14.Location = New System.Drawing.Point(38, 18)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(118, 14)
        Me.Label14.TabIndex = 20
        Me.Label14.Text = "ADE Severity Level :"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label84
        '
        Me.Label84.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label84.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label84.Location = New System.Drawing.Point(6, 642)
        Me.Label84.Name = "Label84"
        Me.Label84.Size = New System.Drawing.Size(672, 5)
        Me.Label84.TabIndex = 24
        Me.Label84.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label85
        '
        Me.Label85.BackColor = System.Drawing.Color.FromArgb(CType(CType(207,Byte),Integer), CType(CType(224,Byte),Integer), CType(CType(248,Byte),Integer))
        Me.Label85.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label85.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label85.Location = New System.Drawing.Point(6, 1)
        Me.Label85.Name = "Label85"
        Me.Label85.Size = New System.Drawing.Size(672, 5)
        Me.Label85.TabIndex = 21
        Me.Label85.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label86
        '
        Me.Label86.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label86.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label86.Location = New System.Drawing.Point(1, 1)
        Me.Label86.Name = "Label86"
        Me.Label86.Size = New System.Drawing.Size(5, 646)
        Me.Label86.TabIndex = 22
        Me.Label86.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label87
        '
        Me.Label87.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label87.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label87.Location = New System.Drawing.Point(678, 1)
        Me.Label87.Name = "Label87"
        Me.Label87.Size = New System.Drawing.Size(5, 646)
        Me.Label87.TabIndex = 23
        Me.Label87.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label52
        '
        Me.Label52.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label52.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label52.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label52.Location = New System.Drawing.Point(1, 647)
        Me.Label52.Name = "Label52"
        Me.Label52.Size = New System.Drawing.Size(682, 1)
        Me.Label52.TabIndex = 8
        Me.Label52.Text = "label2"
        '
        'Label53
        '
        Me.Label53.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label53.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label53.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label53.Location = New System.Drawing.Point(0, 1)
        Me.Label53.Name = "Label53"
        Me.Label53.Size = New System.Drawing.Size(1, 647)
        Me.Label53.TabIndex = 7
        Me.Label53.Text = "label4"
        '
        'Label54
        '
        Me.Label54.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label54.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label54.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label54.Location = New System.Drawing.Point(683, 1)
        Me.Label54.Name = "Label54"
        Me.Label54.Size = New System.Drawing.Size(1, 647)
        Me.Label54.TabIndex = 6
        Me.Label54.Text = "label3"
        '
        'Label55
        '
        Me.Label55.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label55.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label55.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label55.Location = New System.Drawing.Point(0, 0)
        Me.Label55.Name = "Label55"
        Me.Label55.Size = New System.Drawing.Size(684, 1)
        Me.Label55.TabIndex = 5
        Me.Label55.Text = "label1"
        '
        'tp_OtherSettings
        '
        Me.tp_OtherSettings.BackColor = System.Drawing.Color.FromArgb(CType(CType(207,Byte),Integer), CType(CType(224,Byte),Integer), CType(CType(248,Byte),Integer))
        Me.tp_OtherSettings.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tp_OtherSettings.Controls.Add(Me.Label48)
        Me.tp_OtherSettings.Controls.Add(Me.Label49)
        Me.tp_OtherSettings.Controls.Add(Me.Label50)
        Me.tp_OtherSettings.Controls.Add(Me.Label51)
        Me.tp_OtherSettings.Controls.Add(Me.pnlOtherSettings)
        Me.tp_OtherSettings.ForeColor = System.Drawing.Color.Black
        Me.tp_OtherSettings.Location = New System.Drawing.Point(4, 42)
        Me.tp_OtherSettings.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.tp_OtherSettings.Name = "tp_OtherSettings"
        Me.tp_OtherSettings.Size = New System.Drawing.Size(684, 648)
        Me.tp_OtherSettings.TabIndex = 4
        Me.tp_OtherSettings.Text = "Other Settings"
        Me.tp_OtherSettings.UseVisualStyleBackColor = true
        '
        'Label48
        '
        Me.Label48.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label48.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label48.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label48.Location = New System.Drawing.Point(1, 647)
        Me.Label48.Name = "Label48"
        Me.Label48.Size = New System.Drawing.Size(682, 1)
        Me.Label48.TabIndex = 8
        Me.Label48.Text = "label2"
        '
        'Label49
        '
        Me.Label49.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label49.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label49.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label49.Location = New System.Drawing.Point(0, 1)
        Me.Label49.Name = "Label49"
        Me.Label49.Size = New System.Drawing.Size(1, 647)
        Me.Label49.TabIndex = 7
        Me.Label49.Text = "label4"
        '
        'Label50
        '
        Me.Label50.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label50.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label50.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label50.Location = New System.Drawing.Point(683, 1)
        Me.Label50.Name = "Label50"
        Me.Label50.Size = New System.Drawing.Size(1, 647)
        Me.Label50.TabIndex = 6
        Me.Label50.Text = "label3"
        '
        'Label51
        '
        Me.Label51.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label51.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label51.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label51.Location = New System.Drawing.Point(0, 0)
        Me.Label51.Name = "Label51"
        Me.Label51.Size = New System.Drawing.Size(684, 1)
        Me.Label51.TabIndex = 5
        Me.Label51.Text = "label1"
        '
        'pnlOtherSettings
        '
        Me.pnlOtherSettings.BackColor = System.Drawing.Color.FromArgb(CType(CType(207,Byte),Integer), CType(CType(224,Byte),Integer), CType(CType(248,Byte),Integer))
        Me.pnlOtherSettings.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlOtherSettings.Controls.Add(Me.GroupBox26)
        Me.pnlOtherSettings.Controls.Add(Me.GroupBox12)
        Me.pnlOtherSettings.Controls.Add(Me.Panel28)
        Me.pnlOtherSettings.Controls.Add(Me.GroupBox11)
        Me.pnlOtherSettings.Controls.Add(Me.grpbxPatientSynopsis)
        Me.pnlOtherSettings.Controls.Add(Me.grpbxRxMxDrugBtnSetting)
        Me.pnlOtherSettings.Controls.Add(Me.gbRemotePrintSetting)
        Me.pnlOtherSettings.Controls.Add(Me.Panel29)
        Me.pnlOtherSettings.Controls.Add(Me.Label71)
        Me.pnlOtherSettings.Controls.Add(Me.grb_AutoRefreshSettings)
        Me.pnlOtherSettings.Controls.Add(Me.grBday)
        Me.pnlOtherSettings.Controls.Add(Me.grSettings)
        Me.pnlOtherSettings.Controls.Add(Me.GroupBox5)
        Me.pnlOtherSettings.Controls.Add(Me.grpLockScreen)
        Me.pnlOtherSettings.Controls.Add(Me.Label10)
        Me.pnlOtherSettings.Controls.Add(Me.Label69)
        Me.pnlOtherSettings.Controls.Add(Me.Label70)
        Me.pnlOtherSettings.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlOtherSettings.Location = New System.Drawing.Point(0, 0)
        Me.pnlOtherSettings.Name = "pnlOtherSettings"
        Me.pnlOtherSettings.Size = New System.Drawing.Size(684, 648)
        Me.pnlOtherSettings.TabIndex = 0
        '
        'GroupBox26
        '
        Me.GroupBox26.BackColor = System.Drawing.Color.FromArgb(CType(CType(207,Byte),Integer), CType(CType(224,Byte),Integer), CType(CType(248,Byte),Integer))
        Me.GroupBox26.Controls.Add(Me.chkEnableLocalWelchAllynECGDevice)
        Me.GroupBox26.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox26.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GroupBox26.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.GroupBox26.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.GroupBox26.Location = New System.Drawing.Point(5, 601)
        Me.GroupBox26.Name = "GroupBox26"
        Me.GroupBox26.Size = New System.Drawing.Size(674, 41)
        Me.GroupBox26.TabIndex = 23
        Me.GroupBox26.TabStop = false
        Me.GroupBox26.Text = "Device Interface Settings"
        '
        'chkEnableLocalWelchAllynECGDevice
        '
        Me.chkEnableLocalWelchAllynECGDevice.AutoSize = true
        Me.chkEnableLocalWelchAllynECGDevice.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkEnableLocalWelchAllynECGDevice.Location = New System.Drawing.Point(38, 21)
        Me.chkEnableLocalWelchAllynECGDevice.Name = "chkEnableLocalWelchAllynECGDevice"
        Me.chkEnableLocalWelchAllynECGDevice.Size = New System.Drawing.Size(222, 18)
        Me.chkEnableLocalWelchAllynECGDevice.TabIndex = 1
        Me.chkEnableLocalWelchAllynECGDevice.Text = "Enable Local WelchAllyn ECG Device"
        Me.chkEnableLocalWelchAllynECGDevice.UseVisualStyleBackColor = true
        '
        'GroupBox12
        '
        Me.GroupBox12.BackColor = System.Drawing.Color.FromArgb(CType(CType(207,Byte),Integer), CType(CType(224,Byte),Integer), CType(CType(248,Byte),Integer))
        Me.GroupBox12.Controls.Add(Me.chk_ErrorLogs)
        Me.GroupBox12.Controls.Add(Me.chk_ApplicationLog)
        Me.GroupBox12.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox12.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GroupBox12.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.GroupBox12.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.GroupBox12.Location = New System.Drawing.Point(5, 562)
        Me.GroupBox12.Name = "GroupBox12"
        Me.GroupBox12.Size = New System.Drawing.Size(674, 39)
        Me.GroupBox12.TabIndex = 10
        Me.GroupBox12.TabStop = false
        Me.GroupBox12.Text = "Error Log Settings"
        '
        'chk_ErrorLogs
        '
        Me.chk_ErrorLogs.AutoSize = true
        Me.chk_ErrorLogs.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chk_ErrorLogs.Location = New System.Drawing.Point(361, 15)
        Me.chk_ErrorLogs.Name = "chk_ErrorLogs"
        Me.chk_ErrorLogs.Size = New System.Drawing.Size(121, 18)
        Me.chk_ErrorLogs.TabIndex = 1
        Me.chk_ErrorLogs.Text = "Enable Error Logs"
        Me.chk_ErrorLogs.UseVisualStyleBackColor = true
        '
        'chk_ApplicationLog
        '
        Me.chk_ApplicationLog.AutoSize = true
        Me.chk_ApplicationLog.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chk_ApplicationLog.Location = New System.Drawing.Point(38, 18)
        Me.chk_ApplicationLog.Name = "chk_ApplicationLog"
        Me.chk_ApplicationLog.Size = New System.Drawing.Size(154, 18)
        Me.chk_ApplicationLog.TabIndex = 0
        Me.chk_ApplicationLog.Text = "Enable Application Logs"
        Me.chk_ApplicationLog.UseVisualStyleBackColor = true
        '
        'Panel28
        '
        Me.Panel28.Controls.Add(Me.grbSearchSetting)
        Me.Panel28.Controls.Add(Me.GroupBox10)
        Me.Panel28.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel28.Location = New System.Drawing.Point(5, 519)
        Me.Panel28.Name = "Panel28"
        Me.Panel28.Size = New System.Drawing.Size(674, 43)
        Me.Panel28.TabIndex = 9
        '
        'grbSearchSetting
        '
        Me.grbSearchSetting.BackColor = System.Drawing.Color.FromArgb(CType(CType(207,Byte),Integer), CType(CType(224,Byte),Integer), CType(CType(248,Byte),Integer))
        Me.grbSearchSetting.Controls.Add(Me.chkResetSearch)
        Me.grbSearchSetting.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grbSearchSetting.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.grbSearchSetting.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.grbSearchSetting.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.grbSearchSetting.Location = New System.Drawing.Point(0, 0)
        Me.grbSearchSetting.Name = "grbSearchSetting"
        Me.grbSearchSetting.Size = New System.Drawing.Size(323, 43)
        Me.grbSearchSetting.TabIndex = 11
        Me.grbSearchSetting.TabStop = false
        Me.grbSearchSetting.Text = "Search Settings"
        '
        'chkResetSearch
        '
        Me.chkResetSearch.AutoSize = true
        Me.chkResetSearch.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkResetSearch.Location = New System.Drawing.Point(38, 19)
        Me.chkResetSearch.Name = "chkResetSearch"
        Me.chkResetSearch.Size = New System.Drawing.Size(226, 18)
        Me.chkResetSearch.TabIndex = 0
        Me.chkResetSearch.Text = "Reset Search after Category Change"
        Me.chkResetSearch.UseVisualStyleBackColor = true
        '
        'GroupBox10
        '
        Me.GroupBox10.BackColor = System.Drawing.Color.FromArgb(CType(CType(207,Byte),Integer), CType(CType(224,Byte),Integer), CType(CType(248,Byte),Integer))
        Me.GroupBox10.Controls.Add(Me.ChkPatientConfiInfo)
        Me.GroupBox10.Dock = System.Windows.Forms.DockStyle.Right
        Me.GroupBox10.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GroupBox10.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.GroupBox10.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.GroupBox10.Location = New System.Drawing.Point(323, 0)
        Me.GroupBox10.Name = "GroupBox10"
        Me.GroupBox10.Size = New System.Drawing.Size(351, 43)
        Me.GroupBox10.TabIndex = 12
        Me.GroupBox10.TabStop = false
        Me.GroupBox10.Text = "Patient Confidential Settings"
        '
        'ChkPatientConfiInfo
        '
        Me.ChkPatientConfiInfo.AutoSize = true
        Me.ChkPatientConfiInfo.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.ChkPatientConfiInfo.Location = New System.Drawing.Point(38, 21)
        Me.ChkPatientConfiInfo.Name = "ChkPatientConfiInfo"
        Me.ChkPatientConfiInfo.Size = New System.Drawing.Size(234, 18)
        Me.ChkPatientConfiInfo.TabIndex = 0
        Me.ChkPatientConfiInfo.Text = "Show Patient Confidential Information"
        Me.ChkPatientConfiInfo.UseVisualStyleBackColor = true
        '
        'GroupBox11
        '
        Me.GroupBox11.BackColor = System.Drawing.Color.FromArgb(CType(CType(207,Byte),Integer), CType(CType(224,Byte),Integer), CType(CType(248,Byte),Integer))
        Me.GroupBox11.Controls.Add(Me.Label180)
        Me.GroupBox11.Controls.Add(Me.txtInfo)
        Me.GroupBox11.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox11.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GroupBox11.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.GroupBox11.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.GroupBox11.Location = New System.Drawing.Point(5, 476)
        Me.GroupBox11.Name = "GroupBox11"
        Me.GroupBox11.Size = New System.Drawing.Size(674, 43)
        Me.GroupBox11.TabIndex = 8
        Me.GroupBox11.TabStop = false
        '
        'Label180
        '
        Me.Label180.AutoSize = true
        Me.Label180.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label180.Location = New System.Drawing.Point(30, 19)
        Me.Label180.Name = "Label180"
        Me.Label180.Size = New System.Drawing.Size(140, 14)
        Me.Label180.TabIndex = 32
        Me.Label180.Text = "Medication Information :"
        '
        'txtInfo
        '
        Me.txtInfo.Location = New System.Drawing.Point(173, 15)
        Me.txtInfo.Name = "txtInfo"
        Me.txtInfo.Size = New System.Drawing.Size(419, 22)
        Me.txtInfo.TabIndex = 0
        '
        'grpbxPatientSynopsis
        '
        Me.grpbxPatientSynopsis.BackColor = System.Drawing.Color.FromArgb(CType(CType(207,Byte),Integer), CType(CType(224,Byte),Integer), CType(CType(248,Byte),Integer))
        Me.grpbxPatientSynopsis.Controls.Add(Me.numPatientSypnosisTabCount)
        Me.grpbxPatientSynopsis.Controls.Add(Me.lblPatientTabCount)
        Me.grpbxPatientSynopsis.Dock = System.Windows.Forms.DockStyle.Top
        Me.grpbxPatientSynopsis.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.grpbxPatientSynopsis.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.grpbxPatientSynopsis.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.grpbxPatientSynopsis.Location = New System.Drawing.Point(5, 434)
        Me.grpbxPatientSynopsis.Name = "grpbxPatientSynopsis"
        Me.grpbxPatientSynopsis.Size = New System.Drawing.Size(674, 42)
        Me.grpbxPatientSynopsis.TabIndex = 7
        Me.grpbxPatientSynopsis.TabStop = false
        Me.grpbxPatientSynopsis.Text = "Patient Synopsis"
        '
        'numPatientSypnosisTabCount
        '
        Me.numPatientSypnosisTabCount.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.numPatientSypnosisTabCount.Location = New System.Drawing.Point(178, 16)
        Me.numPatientSypnosisTabCount.Maximum = New Decimal(New Integer() {25, 0, 0, 0})
        Me.numPatientSypnosisTabCount.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.numPatientSypnosisTabCount.Name = "numPatientSypnosisTabCount"
        Me.numPatientSypnosisTabCount.Size = New System.Drawing.Size(45, 22)
        Me.numPatientSypnosisTabCount.TabIndex = 0
        Me.numPatientSypnosisTabCount.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'lblPatientTabCount
        '
        Me.lblPatientTabCount.AutoSize = true
        Me.lblPatientTabCount.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblPatientTabCount.Location = New System.Drawing.Point(56, 20)
        Me.lblPatientTabCount.Name = "lblPatientTabCount"
        Me.lblPatientTabCount.Size = New System.Drawing.Size(116, 14)
        Me.lblPatientTabCount.TabIndex = 31
        Me.lblPatientTabCount.Text = "Patient Tab Count :"
        '
        'grpbxRxMxDrugBtnSetting
        '
        Me.grpbxRxMxDrugBtnSetting.BackColor = System.Drawing.Color.FromArgb(CType(CType(207,Byte),Integer), CType(CType(224,Byte),Integer), CType(CType(248,Byte),Integer))
        Me.grpbxRxMxDrugBtnSetting.Controls.Add(Me.cmbMxDrugBtn)
        Me.grpbxRxMxDrugBtnSetting.Controls.Add(Me.lblMx)
        Me.grpbxRxMxDrugBtnSetting.Controls.Add(Me.cmbRxDrugBtn)
        Me.grpbxRxMxDrugBtnSetting.Controls.Add(Me.lblRx)
        Me.grpbxRxMxDrugBtnSetting.Dock = System.Windows.Forms.DockStyle.Top
        Me.grpbxRxMxDrugBtnSetting.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.grpbxRxMxDrugBtnSetting.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.grpbxRxMxDrugBtnSetting.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.grpbxRxMxDrugBtnSetting.Location = New System.Drawing.Point(5, 391)
        Me.grpbxRxMxDrugBtnSetting.Name = "grpbxRxMxDrugBtnSetting"
        Me.grpbxRxMxDrugBtnSetting.Size = New System.Drawing.Size(674, 43)
        Me.grpbxRxMxDrugBtnSetting.TabIndex = 6
        Me.grpbxRxMxDrugBtnSetting.TabStop = false
        Me.grpbxRxMxDrugBtnSetting.Text = "Rx / Mx Drug Button Settings"
        '
        'cmbMxDrugBtn
        '
        Me.cmbMxDrugBtn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbMxDrugBtn.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.cmbMxDrugBtn.Location = New System.Drawing.Point(472, 16)
        Me.cmbMxDrugBtn.Name = "cmbMxDrugBtn"
        Me.cmbMxDrugBtn.Size = New System.Drawing.Size(177, 22)
        Me.cmbMxDrugBtn.TabIndex = 1
        Me.cmbMxDrugBtn.Visible = false
        '
        'lblMx
        '
        Me.lblMx.AutoSize = true
        Me.lblMx.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblMx.Location = New System.Drawing.Point(396, 20)
        Me.lblMx.Name = "lblMx"
        Me.lblMx.Size = New System.Drawing.Size(73, 14)
        Me.lblMx.TabIndex = 33
        Me.lblMx.Text = "Medication :"
        Me.lblMx.Visible = false
        '
        'cmbRxDrugBtn
        '
        Me.cmbRxDrugBtn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbRxDrugBtn.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.cmbRxDrugBtn.Location = New System.Drawing.Point(176, 16)
        Me.cmbRxDrugBtn.Name = "cmbRxDrugBtn"
        Me.cmbRxDrugBtn.Size = New System.Drawing.Size(177, 22)
        Me.cmbRxDrugBtn.TabIndex = 0
        '
        'lblRx
        '
        Me.lblRx.AutoSize = true
        Me.lblRx.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblRx.Location = New System.Drawing.Point(93, 20)
        Me.lblRx.Name = "lblRx"
        Me.lblRx.Size = New System.Drawing.Size(78, 14)
        Me.lblRx.TabIndex = 31
        Me.lblRx.Text = "Prescription :"
        '
        'gbRemotePrintSetting
        '
        Me.gbRemotePrintSetting.BackColor = System.Drawing.Color.FromArgb(CType(CType(207,Byte),Integer), CType(CType(224,Byte),Integer), CType(CType(248,Byte),Integer))
        Me.gbRemotePrintSetting.Controls.Add(Me.cmbNoTemplatesJob)
        Me.gbRemotePrintSetting.Controls.Add(Me.Label213)
        Me.gbRemotePrintSetting.Controls.Add(Me.chkZipMetadata)
        Me.gbRemotePrintSetting.Controls.Add(Me.pnlPrintImages)
        Me.gbRemotePrintSetting.Controls.Add(Me.Panel25)
        Me.gbRemotePrintSetting.Controls.Add(Me.pnlPrintClaims)
        Me.gbRemotePrintSetting.Controls.Add(Me.Panel24)
        Me.gbRemotePrintSetting.Controls.Add(Me.cmbNoPagesSplit)
        Me.gbRemotePrintSetting.Controls.Add(Me.Label206)
        Me.gbRemotePrintSetting.Controls.Add(Me.Label205)
        Me.gbRemotePrintSetting.Controls.Add(Me.chkAddFooterService)
        Me.gbRemotePrintSetting.Controls.Add(Me.chkEnableLocalPrinter)
        Me.gbRemotePrintSetting.Dock = System.Windows.Forms.DockStyle.Top
        Me.gbRemotePrintSetting.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.gbRemotePrintSetting.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.gbRemotePrintSetting.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.gbRemotePrintSetting.Location = New System.Drawing.Point(5, 234)
        Me.gbRemotePrintSetting.Name = "gbRemotePrintSetting"
        Me.gbRemotePrintSetting.Size = New System.Drawing.Size(674, 157)
        Me.gbRemotePrintSetting.TabIndex = 5
        Me.gbRemotePrintSetting.TabStop = false
        Me.gbRemotePrintSetting.Text = "Remote Printer Settings"
        '
        'cmbNoTemplatesJob
        '
        Me.cmbNoTemplatesJob.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbNoTemplatesJob.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.cmbNoTemplatesJob.ForeColor = System.Drawing.Color.Black
        Me.cmbNoTemplatesJob.FormattingEnabled = true
        Me.cmbNoTemplatesJob.Location = New System.Drawing.Point(472, 42)
        Me.cmbNoTemplatesJob.Name = "cmbNoTemplatesJob"
        Me.cmbNoTemplatesJob.Size = New System.Drawing.Size(177, 22)
        Me.cmbNoTemplatesJob.TabIndex = 47
        '
        'Label213
        '
        Me.Label213.AutoSize = true
        Me.Label213.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label213.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label213.Location = New System.Drawing.Point(335, 46)
        Me.Label213.Name = "Label213"
        Me.Label213.Size = New System.Drawing.Size(134, 14)
        Me.Label213.TabIndex = 48
        Me.Label213.Text = "No. of Templates/Job :"
        '
        'chkZipMetadata
        '
        Me.chkZipMetadata.AutoSize = true
        Me.chkZipMetadata.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkZipMetadata.Location = New System.Drawing.Point(38, 44)
        Me.chkZipMetadata.Name = "chkZipMetadata"
        Me.chkZipMetadata.Size = New System.Drawing.Size(97, 18)
        Me.chkZipMetadata.TabIndex = 1
        Me.chkZipMetadata.Text = "Zip Metadata"
        Me.chkZipMetadata.UseVisualStyleBackColor = true
        '
        'pnlPrintImages
        '
        Me.pnlPrintImages.Controls.Add(Me.rbPrintImagesEMF)
        Me.pnlPrintImages.Controls.Add(Me.Label209)
        Me.pnlPrintImages.Controls.Add(Me.rbPrintImagesPNG)
        Me.pnlPrintImages.Location = New System.Drawing.Point(344, 99)
        Me.pnlPrintImages.Name = "pnlPrintImages"
        Me.pnlPrintImages.Size = New System.Drawing.Size(288, 27)
        Me.pnlPrintImages.TabIndex = 6
        '
        'rbPrintImagesEMF
        '
        Me.rbPrintImagesEMF.AutoSize = true
        Me.rbPrintImagesEMF.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.rbPrintImagesEMF.Location = New System.Drawing.Point(205, 4)
        Me.rbPrintImagesEMF.Name = "rbPrintImagesEMF"
        Me.rbPrintImagesEMF.Size = New System.Drawing.Size(47, 18)
        Me.rbPrintImagesEMF.TabIndex = 1
        Me.rbPrintImagesEMF.TabStop = true
        Me.rbPrintImagesEMF.Text = "EMF"
        Me.rbPrintImagesEMF.UseVisualStyleBackColor = true
        '
        'Label209
        '
        Me.Label209.AutoSize = true
        Me.Label209.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label209.Location = New System.Drawing.Point(42, 6)
        Me.Label209.Name = "Label209"
        Me.Label209.Size = New System.Drawing.Size(83, 14)
        Me.Label209.TabIndex = 39
        Me.Label209.Text = "Print Images :"
        '
        'rbPrintImagesPNG
        '
        Me.rbPrintImagesPNG.AutoSize = true
        Me.rbPrintImagesPNG.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.rbPrintImagesPNG.Location = New System.Drawing.Point(128, 4)
        Me.rbPrintImagesPNG.Name = "rbPrintImagesPNG"
        Me.rbPrintImagesPNG.Size = New System.Drawing.Size(48, 18)
        Me.rbPrintImagesPNG.TabIndex = 0
        Me.rbPrintImagesPNG.TabStop = true
        Me.rbPrintImagesPNG.Text = "PNG"
        Me.rbPrintImagesPNG.UseVisualStyleBackColor = true
        '
        'Panel25
        '
        Me.Panel25.Controls.Add(Me.rbPrintSSRSReportEMF)
        Me.Panel25.Controls.Add(Me.Label208)
        Me.Panel25.Controls.Add(Me.rbPrintSSRSReportPDF)
        Me.Panel25.Location = New System.Drawing.Point(344, 71)
        Me.Panel25.Name = "Panel25"
        Me.Panel25.Size = New System.Drawing.Size(288, 27)
        Me.Panel25.TabIndex = 4
        '
        'rbPrintSSRSReportEMF
        '
        Me.rbPrintSSRSReportEMF.AutoSize = true
        Me.rbPrintSSRSReportEMF.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.rbPrintSSRSReportEMF.Location = New System.Drawing.Point(205, 4)
        Me.rbPrintSSRSReportEMF.Name = "rbPrintSSRSReportEMF"
        Me.rbPrintSSRSReportEMF.Size = New System.Drawing.Size(47, 18)
        Me.rbPrintSSRSReportEMF.TabIndex = 1
        Me.rbPrintSSRSReportEMF.TabStop = true
        Me.rbPrintSSRSReportEMF.Text = "EMF"
        Me.rbPrintSSRSReportEMF.UseVisualStyleBackColor = true
        '
        'Label208
        '
        Me.Label208.AutoSize = true
        Me.Label208.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label208.Location = New System.Drawing.Point(7, 6)
        Me.Label208.Name = "Label208"
        Me.Label208.Size = New System.Drawing.Size(118, 14)
        Me.Label208.TabIndex = 39
        Me.Label208.Text = "Print SSRS Reports :"
        '
        'rbPrintSSRSReportPDF
        '
        Me.rbPrintSSRSReportPDF.AutoSize = true
        Me.rbPrintSSRSReportPDF.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.rbPrintSSRSReportPDF.Location = New System.Drawing.Point(128, 4)
        Me.rbPrintSSRSReportPDF.Name = "rbPrintSSRSReportPDF"
        Me.rbPrintSSRSReportPDF.Size = New System.Drawing.Size(46, 18)
        Me.rbPrintSSRSReportPDF.TabIndex = 0
        Me.rbPrintSSRSReportPDF.TabStop = true
        Me.rbPrintSSRSReportPDF.Text = "PDF"
        Me.rbPrintSSRSReportPDF.UseVisualStyleBackColor = true
        '
        'pnlPrintClaims
        '
        Me.pnlPrintClaims.Controls.Add(Me.rbPrintClaimsEMF)
        Me.pnlPrintClaims.Controls.Add(Me.Label210)
        Me.pnlPrintClaims.Controls.Add(Me.rbPrintClaimsPDF)
        Me.pnlPrintClaims.Location = New System.Drawing.Point(14, 99)
        Me.pnlPrintClaims.Name = "pnlPrintClaims"
        Me.pnlPrintClaims.Size = New System.Drawing.Size(301, 27)
        Me.pnlPrintClaims.TabIndex = 5
        '
        'rbPrintClaimsEMF
        '
        Me.rbPrintClaimsEMF.AutoSize = true
        Me.rbPrintClaimsEMF.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.rbPrintClaimsEMF.Location = New System.Drawing.Point(225, 4)
        Me.rbPrintClaimsEMF.Name = "rbPrintClaimsEMF"
        Me.rbPrintClaimsEMF.Size = New System.Drawing.Size(47, 18)
        Me.rbPrintClaimsEMF.TabIndex = 1
        Me.rbPrintClaimsEMF.TabStop = true
        Me.rbPrintClaimsEMF.Text = "EMF"
        Me.rbPrintClaimsEMF.UseVisualStyleBackColor = true
        '
        'Label210
        '
        Me.Label210.AutoSize = true
        Me.Label210.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label210.Location = New System.Drawing.Point(78, 6)
        Me.Label210.Name = "Label210"
        Me.Label210.Size = New System.Drawing.Size(76, 14)
        Me.Label210.TabIndex = 36
        Me.Label210.Text = "Print Claims :"
        '
        'rbPrintClaimsPDF
        '
        Me.rbPrintClaimsPDF.AutoSize = true
        Me.rbPrintClaimsPDF.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.rbPrintClaimsPDF.Location = New System.Drawing.Point(156, 4)
        Me.rbPrintClaimsPDF.Name = "rbPrintClaimsPDF"
        Me.rbPrintClaimsPDF.Size = New System.Drawing.Size(46, 18)
        Me.rbPrintClaimsPDF.TabIndex = 0
        Me.rbPrintClaimsPDF.TabStop = true
        Me.rbPrintClaimsPDF.Text = "PDF"
        Me.rbPrintClaimsPDF.UseVisualStyleBackColor = true
        '
        'Panel24
        '
        Me.Panel24.Controls.Add(Me.rbPrintWordDocEMF)
        Me.Panel24.Controls.Add(Me.Label207)
        Me.Panel24.Controls.Add(Me.rbPrintWordDocPDF)
        Me.Panel24.Location = New System.Drawing.Point(14, 71)
        Me.Panel24.Name = "Panel24"
        Me.Panel24.Size = New System.Drawing.Size(301, 27)
        Me.Panel24.TabIndex = 3
        '
        'rbPrintWordDocEMF
        '
        Me.rbPrintWordDocEMF.AutoSize = true
        Me.rbPrintWordDocEMF.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.rbPrintWordDocEMF.Location = New System.Drawing.Point(225, 4)
        Me.rbPrintWordDocEMF.Name = "rbPrintWordDocEMF"
        Me.rbPrintWordDocEMF.Size = New System.Drawing.Size(47, 18)
        Me.rbPrintWordDocEMF.TabIndex = 1
        Me.rbPrintWordDocEMF.TabStop = true
        Me.rbPrintWordDocEMF.Text = "EMF"
        Me.rbPrintWordDocEMF.UseVisualStyleBackColor = true
        '
        'Label207
        '
        Me.Label207.AutoSize = true
        Me.Label207.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label207.Location = New System.Drawing.Point(21, 6)
        Me.Label207.Name = "Label207"
        Me.Label207.Size = New System.Drawing.Size(133, 14)
        Me.Label207.TabIndex = 36
        Me.Label207.Text = "Print word Document :"
        '
        'rbPrintWordDocPDF
        '
        Me.rbPrintWordDocPDF.AutoSize = true
        Me.rbPrintWordDocPDF.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.rbPrintWordDocPDF.Location = New System.Drawing.Point(156, 4)
        Me.rbPrintWordDocPDF.Name = "rbPrintWordDocPDF"
        Me.rbPrintWordDocPDF.Size = New System.Drawing.Size(46, 18)
        Me.rbPrintWordDocPDF.TabIndex = 0
        Me.rbPrintWordDocPDF.TabStop = true
        Me.rbPrintWordDocPDF.Text = "PDF"
        Me.rbPrintWordDocPDF.UseVisualStyleBackColor = true
        '
        'cmbNoPagesSplit
        '
        Me.cmbNoPagesSplit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbNoPagesSplit.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.cmbNoPagesSplit.Location = New System.Drawing.Point(472, 17)
        Me.cmbNoPagesSplit.Name = "cmbNoPagesSplit"
        Me.cmbNoPagesSplit.Size = New System.Drawing.Size(177, 22)
        Me.cmbNoPagesSplit.TabIndex = 2
        '
        'Label206
        '
        Me.Label206.AutoSize = true
        Me.Label206.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label206.Location = New System.Drawing.Point(341, 21)
        Me.Label206.Name = "Label206"
        Me.Label206.Size = New System.Drawing.Size(128, 14)
        Me.Label206.TabIndex = 33
        Me.Label206.Text = "No. of Pages to Split :"
        '
        'Label205
        '
        Me.Label205.AutoSize = true
        Me.Label205.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label205.Location = New System.Drawing.Point(13, 134)
        Me.Label205.Name = "Label205"
        Me.Label205.Size = New System.Drawing.Size(536, 14)
        Me.Label205.TabIndex = 35
        Me.Label205.Text = "Note:  If Local printer setting is enabled, default printer will be used as per s"& _ 
    "ervice configuration."
        '
        'chkAddFooterService
        '
        Me.chkAddFooterService.AutoSize = true
        Me.chkAddFooterService.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkAddFooterService.Location = New System.Drawing.Point(38, 169)
        Me.chkAddFooterService.Name = "chkAddFooterService"
        Me.chkAddFooterService.Size = New System.Drawing.Size(144, 18)
        Me.chkAddFooterService.TabIndex = 1
        Me.chkAddFooterService.Text = "Add Footer in Service"
        Me.chkAddFooterService.UseVisualStyleBackColor = true
        Me.chkAddFooterService.Visible = false
        '
        'chkEnableLocalPrinter
        '
        Me.chkEnableLocalPrinter.AutoSize = true
        Me.chkEnableLocalPrinter.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkEnableLocalPrinter.Location = New System.Drawing.Point(38, 19)
        Me.chkEnableLocalPrinter.Name = "chkEnableLocalPrinter"
        Me.chkEnableLocalPrinter.Size = New System.Drawing.Size(133, 18)
        Me.chkEnableLocalPrinter.TabIndex = 0
        Me.chkEnableLocalPrinter.Text = "Enable Local Printer"
        Me.chkEnableLocalPrinter.UseVisualStyleBackColor = true
        '
        'Panel29
        '
        Me.Panel29.Controls.Add(Me.gb_DefaultPrinterSettings)
        Me.Panel29.Controls.Add(Me.Gbox_DefaultNavgtn)
        Me.Panel29.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel29.Location = New System.Drawing.Point(5, 191)
        Me.Panel29.Name = "Panel29"
        Me.Panel29.Size = New System.Drawing.Size(674, 43)
        Me.Panel29.TabIndex = 4
        '
        'gb_DefaultPrinterSettings
        '
        Me.gb_DefaultPrinterSettings.BackColor = System.Drawing.Color.FromArgb(CType(CType(207,Byte),Integer), CType(CType(224,Byte),Integer), CType(CType(248,Byte),Integer))
        Me.gb_DefaultPrinterSettings.Controls.Add(Me.chkUseDefaultPrinter)
        Me.gb_DefaultPrinterSettings.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gb_DefaultPrinterSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.gb_DefaultPrinterSettings.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.gb_DefaultPrinterSettings.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.gb_DefaultPrinterSettings.Location = New System.Drawing.Point(0, 0)
        Me.gb_DefaultPrinterSettings.Name = "gb_DefaultPrinterSettings"
        Me.gb_DefaultPrinterSettings.Size = New System.Drawing.Size(323, 43)
        Me.gb_DefaultPrinterSettings.TabIndex = 6
        Me.gb_DefaultPrinterSettings.TabStop = false
        Me.gb_DefaultPrinterSettings.Text = "Default Printer Settings"
        '
        'chkUseDefaultPrinter
        '
        Me.chkUseDefaultPrinter.AutoSize = true
        Me.chkUseDefaultPrinter.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkUseDefaultPrinter.Location = New System.Drawing.Point(38, 19)
        Me.chkUseDefaultPrinter.Name = "chkUseDefaultPrinter"
        Me.chkUseDefaultPrinter.Size = New System.Drawing.Size(129, 18)
        Me.chkUseDefaultPrinter.TabIndex = 0
        Me.chkUseDefaultPrinter.Text = "Use Default Printer"
        Me.chkUseDefaultPrinter.UseVisualStyleBackColor = true
        '
        'Gbox_DefaultNavgtn
        '
        Me.Gbox_DefaultNavgtn.BackColor = System.Drawing.Color.FromArgb(CType(CType(207,Byte),Integer), CType(CType(224,Byte),Integer), CType(CType(248,Byte),Integer))
        Me.Gbox_DefaultNavgtn.Controls.Add(Me.Cmb_NavgtnPnl)
        Me.Gbox_DefaultNavgtn.Controls.Add(Me.Lbl_NavgtnPnl)
        Me.Gbox_DefaultNavgtn.Dock = System.Windows.Forms.DockStyle.Right
        Me.Gbox_DefaultNavgtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Gbox_DefaultNavgtn.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Gbox_DefaultNavgtn.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Gbox_DefaultNavgtn.Location = New System.Drawing.Point(323, 0)
        Me.Gbox_DefaultNavgtn.Name = "Gbox_DefaultNavgtn"
        Me.Gbox_DefaultNavgtn.Size = New System.Drawing.Size(351, 43)
        Me.Gbox_DefaultNavgtn.TabIndex = 4
        Me.Gbox_DefaultNavgtn.TabStop = false
        Me.Gbox_DefaultNavgtn.Text = "Default Navigation Settings"
        '
        'Cmb_NavgtnPnl
        '
        Me.Cmb_NavgtnPnl.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_NavgtnPnl.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Cmb_NavgtnPnl.Location = New System.Drawing.Point(149, 16)
        Me.Cmb_NavgtnPnl.Name = "Cmb_NavgtnPnl"
        Me.Cmb_NavgtnPnl.Size = New System.Drawing.Size(177, 22)
        Me.Cmb_NavgtnPnl.TabIndex = 0
        '
        'Lbl_NavgtnPnl
        '
        Me.Lbl_NavgtnPnl.AutoSize = true
        Me.Lbl_NavgtnPnl.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Lbl_NavgtnPnl.Location = New System.Drawing.Point(32, 20)
        Me.Lbl_NavgtnPnl.Name = "Lbl_NavgtnPnl"
        Me.Lbl_NavgtnPnl.Size = New System.Drawing.Size(114, 14)
        Me.Lbl_NavgtnPnl.TabIndex = 31
        Me.Lbl_NavgtnPnl.Text = "Default Navigation :"
        '
        'Label71
        '
        Me.Label71.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label71.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label71.Location = New System.Drawing.Point(5, 638)
        Me.Label71.Name = "Label71"
        Me.Label71.Size = New System.Drawing.Size(674, 10)
        Me.Label71.TabIndex = 20
        Me.Label71.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'grb_AutoRefreshSettings
        '
        Me.grb_AutoRefreshSettings.BackColor = System.Drawing.Color.FromArgb(CType(CType(207,Byte),Integer), CType(CType(224,Byte),Integer), CType(CType(248,Byte),Integer))
        Me.grb_AutoRefreshSettings.Controls.Add(Me.Panel6)
        Me.grb_AutoRefreshSettings.Dock = System.Windows.Forms.DockStyle.Top
        Me.grb_AutoRefreshSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.grb_AutoRefreshSettings.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.grb_AutoRefreshSettings.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.grb_AutoRefreshSettings.Location = New System.Drawing.Point(5, 148)
        Me.grb_AutoRefreshSettings.Name = "grb_AutoRefreshSettings"
        Me.grb_AutoRefreshSettings.Size = New System.Drawing.Size(674, 43)
        Me.grb_AutoRefreshSettings.TabIndex = 3
        Me.grb_AutoRefreshSettings.TabStop = false
        Me.grb_AutoRefreshSettings.Text = "Auto Refresh Settings"
        '
        'Panel6
        '
        Me.Panel6.Controls.Add(Me.Label23)
        Me.Panel6.Controls.Add(Me.num_MessagesRefreshTime)
        Me.Panel6.Controls.Add(Me.Label24)
        Me.Panel6.Location = New System.Drawing.Point(13, 15)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(487, 24)
        Me.Panel6.TabIndex = 1
        '
        'Label23
        '
        Me.Label23.AutoSize = true
        Me.Label23.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label23.Location = New System.Drawing.Point(337, 5)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(49, 14)
        Me.Label23.TabIndex = 6
        Me.Label23.Text = "Minutes"
        '
        'num_MessagesRefreshTime
        '
        Me.num_MessagesRefreshTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.num_MessagesRefreshTime.DecimalPlaces = 1
        Me.num_MessagesRefreshTime.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.num_MessagesRefreshTime.Increment = New Decimal(New Integer() {5, 0, 0, 65536})
        Me.num_MessagesRefreshTime.Location = New System.Drawing.Point(287, 1)
        Me.num_MessagesRefreshTime.Maximum = New Decimal(New Integer() {60, 0, 0, 0})
        Me.num_MessagesRefreshTime.Minimum = New Decimal(New Integer() {1, 0, 0, 65536})
        Me.num_MessagesRefreshTime.Name = "num_MessagesRefreshTime"
        Me.num_MessagesRefreshTime.Size = New System.Drawing.Size(45, 22)
        Me.num_MessagesRefreshTime.TabIndex = 0
        Me.num_MessagesRefreshTime.Value = New Decimal(New Integer() {5, 0, 0, 0})
        '
        'Label24
        '
        Me.Label24.AutoSize = true
        Me.Label24.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label24.Location = New System.Drawing.Point(35, 5)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(249, 14)
        Me.Label24.TabIndex = 4
        Me.Label24.Text = "Auto Refresh Messages / Tasks after every :"
        '
        'grBday
        '
        Me.grBday.BackColor = System.Drawing.Color.FromArgb(CType(CType(207,Byte),Integer), CType(CType(224,Byte),Integer), CType(CType(248,Byte),Integer))
        Me.grBday.Controls.Add(Me.pnlBday)
        Me.grBday.Controls.Add(Me.chkBdayReminder)
        Me.grBday.Dock = System.Windows.Forms.DockStyle.Top
        Me.grBday.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.grBday.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.grBday.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.grBday.Location = New System.Drawing.Point(5, 103)
        Me.grBday.Name = "grBday"
        Me.grBday.Size = New System.Drawing.Size(674, 45)
        Me.grBday.TabIndex = 2
        Me.grBday.TabStop = false
        Me.grBday.Text = "Birthday Reminder Settings"
        '
        'pnlBday
        '
        Me.pnlBday.Controls.Add(Me.Label20)
        Me.pnlBday.Controls.Add(Me.numBdayReminder)
        Me.pnlBday.Controls.Add(Me.Label21)
        Me.pnlBday.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.pnlBday.Location = New System.Drawing.Point(199, 18)
        Me.pnlBday.Name = "pnlBday"
        Me.pnlBday.Size = New System.Drawing.Size(243, 24)
        Me.pnlBday.TabIndex = 1
        Me.pnlBday.Visible = false
        '
        'Label20
        '
        Me.Label20.AutoSize = true
        Me.Label20.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label20.Location = New System.Drawing.Point(147, 5)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(56, 14)
        Me.Label20.TabIndex = 6
        Me.Label20.Text = "(In days)"
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'numBdayReminder
        '
        Me.numBdayReminder.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.numBdayReminder.Location = New System.Drawing.Point(100, 1)
        Me.numBdayReminder.Maximum = New Decimal(New Integer() {60, 0, 0, 0})
        Me.numBdayReminder.Name = "numBdayReminder"
        Me.numBdayReminder.Size = New System.Drawing.Size(45, 22)
        Me.numBdayReminder.TabIndex = 0
        Me.numBdayReminder.Value = New Decimal(New Integer() {10, 0, 0, 0})
        '
        'Label21
        '
        Me.Label21.AutoSize = true
        Me.Label21.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label21.Location = New System.Drawing.Point(2, 5)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(95, 14)
        Me.Label21.TabIndex = 4
        Me.Label21.Text = "Reminder Days :"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'chkBdayReminder
        '
        Me.chkBdayReminder.AutoSize = true
        Me.chkBdayReminder.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkBdayReminder.Location = New System.Drawing.Point(38, 21)
        Me.chkBdayReminder.Name = "chkBdayReminder"
        Me.chkBdayReminder.Size = New System.Drawing.Size(160, 18)
        Me.chkBdayReminder.TabIndex = 0
        Me.chkBdayReminder.Text = "Show Birthday Reminder"
        Me.chkBdayReminder.UseVisualStyleBackColor = true
        '
        'grSettings
        '
        Me.grSettings.BackColor = System.Drawing.Color.FromArgb(CType(CType(207,Byte),Integer), CType(CType(224,Byte),Integer), CType(CType(248,Byte),Integer))
        Me.grSettings.Controls.Add(Me.chkOutbound)
        Me.grSettings.Controls.Add(Me.grHL7Settings)
        Me.grSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.grSettings.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.grSettings.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.grSettings.Location = New System.Drawing.Point(5, 103)
        Me.grSettings.Name = "grSettings"
        Me.grSettings.Size = New System.Drawing.Size(674, 78)
        Me.grSettings.TabIndex = 3
        Me.grSettings.TabStop = false
        Me.grSettings.Visible = false
        '
        'chkOutbound
        '
        Me.chkOutbound.AutoSize = true
        Me.chkOutbound.BackColor = System.Drawing.Color.FromArgb(CType(CType(207,Byte),Integer), CType(CType(224,Byte),Integer), CType(CType(248,Byte),Integer))
        Me.chkOutbound.Location = New System.Drawing.Point(8, 1)
        Me.chkOutbound.Name = "chkOutbound"
        Me.chkOutbound.Size = New System.Drawing.Size(204, 18)
        Me.chkOutbound.TabIndex = 6
        Me.chkOutbound.Text = "Generate Outbound Message"
        Me.chkOutbound.UseVisualStyleBackColor = false
        '
        'grHL7Settings
        '
        Me.grHL7Settings.Controls.Add(Me.chkHL7Appointment)
        Me.grHL7Settings.Controls.Add(Me.chkHL7Immunization)
        Me.grHL7Settings.Controls.Add(Me.rbHL7)
        Me.grHL7Settings.Controls.Add(Me.rbGenius)
        Me.grHL7Settings.Controls.Add(Me.chkPatientReg)
        Me.grHL7Settings.Controls.Add(Me.chkSaveandFinish)
        Me.grHL7Settings.Controls.Add(Me.chkSaveandClose)
        Me.grHL7Settings.Location = New System.Drawing.Point(15, 14)
        Me.grHL7Settings.Name = "grHL7Settings"
        Me.grHL7Settings.Size = New System.Drawing.Size(640, 55)
        Me.grHL7Settings.TabIndex = 3
        Me.grHL7Settings.TabStop = false
        '
        'chkHL7Appointment
        '
        Me.chkHL7Appointment.AutoSize = true
        Me.chkHL7Appointment.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkHL7Appointment.Location = New System.Drawing.Point(452, 12)
        Me.chkHL7Appointment.Name = "chkHL7Appointment"
        Me.chkHL7Appointment.Size = New System.Drawing.Size(169, 18)
        Me.chkHL7Appointment.TabIndex = 6
        Me.chkHL7Appointment.Text = "Send Appointment Details"
        Me.chkHL7Appointment.UseVisualStyleBackColor = true
        '
        'chkHL7Immunization
        '
        Me.chkHL7Immunization.AutoSize = true
        Me.chkHL7Immunization.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkHL7Immunization.Location = New System.Drawing.Point(232, 12)
        Me.chkHL7Immunization.Name = "chkHL7Immunization"
        Me.chkHL7Immunization.Size = New System.Drawing.Size(169, 18)
        Me.chkHL7Immunization.TabIndex = 5
        Me.chkHL7Immunization.Text = "Send Immunization Details"
        Me.chkHL7Immunization.UseVisualStyleBackColor = true
        '
        'rbHL7
        '
        Me.rbHL7.AutoSize = true
        Me.rbHL7.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.rbHL7.Location = New System.Drawing.Point(118, 12)
        Me.rbHL7.Name = "rbHL7"
        Me.rbHL7.Size = New System.Drawing.Size(47, 18)
        Me.rbHL7.TabIndex = 1
        Me.rbHL7.Text = "HL7"
        Me.rbHL7.UseVisualStyleBackColor = true
        '
        'rbGenius
        '
        Me.rbGenius.AutoSize = true
        Me.rbGenius.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.rbGenius.Location = New System.Drawing.Point(19, 12)
        Me.rbGenius.Name = "rbGenius"
        Me.rbGenius.Size = New System.Drawing.Size(62, 18)
        Me.rbGenius.TabIndex = 0
        Me.rbGenius.Text = "Genius"
        Me.rbGenius.UseVisualStyleBackColor = true
        '
        'chkPatientReg
        '
        Me.chkPatientReg.AutoSize = true
        Me.chkPatientReg.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkPatientReg.Location = New System.Drawing.Point(452, 33)
        Me.chkPatientReg.Name = "chkPatientReg"
        Me.chkPatientReg.Size = New System.Drawing.Size(136, 18)
        Me.chkPatientReg.TabIndex = 4
        Me.chkPatientReg.Text = "Send Patient Details"
        Me.chkPatientReg.UseVisualStyleBackColor = true
        '
        'chkSaveandFinish
        '
        Me.chkSaveandFinish.AutoSize = true
        Me.chkSaveandFinish.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkSaveandFinish.Location = New System.Drawing.Point(232, 33)
        Me.chkSaveandFinish.Name = "chkSaveandFinish"
        Me.chkSaveandFinish.Size = New System.Drawing.Size(206, 18)
        Me.chkSaveandFinish.TabIndex = 3
        Me.chkSaveandFinish.Text = "Send Charges on Save and Finish"
        Me.chkSaveandFinish.UseVisualStyleBackColor = true
        '
        'chkSaveandClose
        '
        Me.chkSaveandClose.AutoSize = true
        Me.chkSaveandClose.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkSaveandClose.Location = New System.Drawing.Point(19, 33)
        Me.chkSaveandClose.Name = "chkSaveandClose"
        Me.chkSaveandClose.Size = New System.Drawing.Size(205, 18)
        Me.chkSaveandClose.TabIndex = 2
        Me.chkSaveandClose.Text = "Send Charges on Save and Close"
        Me.chkSaveandClose.UseVisualStyleBackColor = true
        '
        'GroupBox5
        '
        Me.GroupBox5.BackColor = System.Drawing.Color.FromArgb(CType(CType(207,Byte),Integer), CType(CType(224,Byte),Integer), CType(CType(248,Byte),Integer))
        Me.GroupBox5.Controls.Add(Me.pnlClinicEnv)
        Me.GroupBox5.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox5.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GroupBox5.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.GroupBox5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.GroupBox5.Location = New System.Drawing.Point(5, 47)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(674, 56)
        Me.GroupBox5.TabIndex = 2
        Me.GroupBox5.TabStop = false
        Me.GroupBox5.Text = "Clinical Environment Settings"
        '
        'pnlClinicEnv
        '
        Me.pnlClinicEnv.Controls.Add(Me.lblENV_06)
        Me.pnlClinicEnv.Controls.Add(Me.lblENV_05)
        Me.pnlClinicEnv.Controls.Add(Me.lblENV_04)
        Me.pnlClinicEnv.Controls.Add(Me.lblENV_03)
        Me.pnlClinicEnv.Controls.Add(Me.lblENV_02)
        Me.pnlClinicEnv.Controls.Add(Me.lblENV_01)
        Me.pnlClinicEnv.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlClinicEnv.Font = New System.Drawing.Font("Verdana", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.pnlClinicEnv.Location = New System.Drawing.Point(3, 18)
        Me.pnlClinicEnv.Name = "pnlClinicEnv"
        Me.pnlClinicEnv.Size = New System.Drawing.Size(668, 35)
        Me.pnlClinicEnv.TabIndex = 74
        '
        'lblENV_06
        '
        Me.lblENV_06.BackColor = System.Drawing.Color.White
        Me.lblENV_06.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblENV_06.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblENV_06.Location = New System.Drawing.Point(234, 6)
        Me.lblENV_06.Name = "lblENV_06"
        Me.lblENV_06.Size = New System.Drawing.Size(30, 23)
        Me.lblENV_06.TabIndex = 9
        Me.lblENV_06.Tag = "6"
        Me.lblENV_06.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblENV_05
        '
        Me.lblENV_05.BackColor = System.Drawing.Color.White
        Me.lblENV_05.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblENV_05.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblENV_05.Location = New System.Drawing.Point(192, 6)
        Me.lblENV_05.Name = "lblENV_05"
        Me.lblENV_05.Size = New System.Drawing.Size(30, 23)
        Me.lblENV_05.TabIndex = 10
        Me.lblENV_05.Tag = "5"
        Me.lblENV_05.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblENV_04
        '
        Me.lblENV_04.BackColor = System.Drawing.Color.White
        Me.lblENV_04.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblENV_04.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblENV_04.Location = New System.Drawing.Point(150, 6)
        Me.lblENV_04.Name = "lblENV_04"
        Me.lblENV_04.Size = New System.Drawing.Size(30, 23)
        Me.lblENV_04.TabIndex = 8
        Me.lblENV_04.Tag = "4"
        Me.lblENV_04.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblENV_03
        '
        Me.lblENV_03.BackColor = System.Drawing.Color.White
        Me.lblENV_03.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblENV_03.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblENV_03.Location = New System.Drawing.Point(108, 6)
        Me.lblENV_03.Name = "lblENV_03"
        Me.lblENV_03.Size = New System.Drawing.Size(30, 23)
        Me.lblENV_03.TabIndex = 6
        Me.lblENV_03.Tag = "3"
        Me.lblENV_03.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblENV_02
        '
        Me.lblENV_02.BackColor = System.Drawing.Color.White
        Me.lblENV_02.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblENV_02.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblENV_02.Location = New System.Drawing.Point(66, 6)
        Me.lblENV_02.Name = "lblENV_02"
        Me.lblENV_02.Size = New System.Drawing.Size(30, 23)
        Me.lblENV_02.TabIndex = 7
        Me.lblENV_02.Tag = "2"
        Me.lblENV_02.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblENV_01
        '
        Me.lblENV_01.BackColor = System.Drawing.Color.White
        Me.lblENV_01.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblENV_01.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblENV_01.Location = New System.Drawing.Point(24, 6)
        Me.lblENV_01.Name = "lblENV_01"
        Me.lblENV_01.Size = New System.Drawing.Size(30, 23)
        Me.lblENV_01.TabIndex = 5
        Me.lblENV_01.Tag = "1"
        Me.lblENV_01.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'grpLockScreen
        '
        Me.grpLockScreen.BackColor = System.Drawing.Color.FromArgb(CType(CType(207,Byte),Integer), CType(CType(224,Byte),Integer), CType(CType(248,Byte),Integer))
        Me.grpLockScreen.Controls.Add(Me.chkAutoApplicationLock)
        Me.grpLockScreen.Controls.Add(Me.Label11)
        Me.grpLockScreen.Controls.Add(Me.numLockTime)
        Me.grpLockScreen.Dock = System.Windows.Forms.DockStyle.Top
        Me.grpLockScreen.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.grpLockScreen.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.grpLockScreen.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.grpLockScreen.Location = New System.Drawing.Point(5, 5)
        Me.grpLockScreen.Name = "grpLockScreen"
        Me.grpLockScreen.Size = New System.Drawing.Size(674, 42)
        Me.grpLockScreen.TabIndex = 1
        Me.grpLockScreen.TabStop = false
        Me.grpLockScreen.Text = "Lock Screen Settings"
        '
        'chkAutoApplicationLock
        '
        Me.chkAutoApplicationLock.AutoSize = true
        Me.chkAutoApplicationLock.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkAutoApplicationLock.Location = New System.Drawing.Point(13, 18)
        Me.chkAutoApplicationLock.Name = "chkAutoApplicationLock"
        Me.chkAutoApplicationLock.Size = New System.Drawing.Size(145, 18)
        Me.chkAutoApplicationLock.TabIndex = 0
        Me.chkAutoApplicationLock.Text = "Auto Application Lock"
        Me.chkAutoApplicationLock.UseVisualStyleBackColor = true
        '
        'Label11
        '
        Me.Label11.AutoSize = true
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label11.Location = New System.Drawing.Point(209, 20)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(75, 14)
        Me.Label11.TabIndex = 3
        Me.Label11.Text = "(In minutes)"
        '
        'numLockTime
        '
        Me.numLockTime.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.numLockTime.Location = New System.Drawing.Point(160, 16)
        Me.numLockTime.Maximum = New Decimal(New Integer() {60, 0, 0, 0})
        Me.numLockTime.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.numLockTime.Name = "numLockTime"
        Me.numLockTime.Size = New System.Drawing.Size(45, 22)
        Me.numLockTime.TabIndex = 1
        Me.numLockTime.Value = New Decimal(New Integer() {10, 0, 0, 0})
        '
        'Label10
        '
        Me.Label10.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label10.Location = New System.Drawing.Point(5, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(674, 5)
        Me.Label10.TabIndex = 0
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label69
        '
        Me.Label69.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label69.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label69.Location = New System.Drawing.Point(0, 0)
        Me.Label69.Name = "Label69"
        Me.Label69.Size = New System.Drawing.Size(5, 648)
        Me.Label69.TabIndex = 18
        Me.Label69.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label70
        '
        Me.Label70.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label70.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label70.Location = New System.Drawing.Point(679, 0)
        Me.Label70.Name = "Label70"
        Me.Label70.Size = New System.Drawing.Size(5, 648)
        Me.Label70.TabIndex = 19
        Me.Label70.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'tp_SureScriptSettings
        '
        Me.tp_SureScriptSettings.BackColor = System.Drawing.Color.FromArgb(CType(CType(207,Byte),Integer), CType(CType(224,Byte),Integer), CType(CType(248,Byte),Integer))
        Me.tp_SureScriptSettings.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tp_SureScriptSettings.Controls.Add(Me.grpsurescripSecurMsg)
        Me.tp_SureScriptSettings.Controls.Add(Me.chkShowOffFormularyAlternatives)
        Me.tp_SureScriptSettings.Controls.Add(Me.chkMachineFormularyAlert)
        Me.tp_SureScriptSettings.Controls.Add(Me.grpFormularySettings)
        Me.tp_SureScriptSettings.Controls.Add(Me.grpsurescriptalert)
        Me.tp_SureScriptSettings.Controls.Add(Me.chkFormularyAlertnativesOffFormularyDrgs)
        Me.tp_SureScriptSettings.Controls.Add(Me.Label88)
        Me.tp_SureScriptSettings.Controls.Add(Me.chkFormularyAlertnativesNRDrgs)
        Me.tp_SureScriptSettings.Controls.Add(Me.Label89)
        Me.tp_SureScriptSettings.Controls.Add(Me.chkFormularyAlertnativesAllDrgs)
        Me.tp_SureScriptSettings.Controls.Add(Me.Label90)
        Me.tp_SureScriptSettings.Controls.Add(Me.Label91)
        Me.tp_SureScriptSettings.Controls.Add(Me.Label44)
        Me.tp_SureScriptSettings.Controls.Add(Me.Label45)
        Me.tp_SureScriptSettings.Controls.Add(Me.Label46)
        Me.tp_SureScriptSettings.Controls.Add(Me.Label47)
        Me.tp_SureScriptSettings.Location = New System.Drawing.Point(4, 42)
        Me.tp_SureScriptSettings.Name = "tp_SureScriptSettings"
        Me.tp_SureScriptSettings.Size = New System.Drawing.Size(684, 648)
        Me.tp_SureScriptSettings.TabIndex = 5
        Me.tp_SureScriptSettings.Text = "Surescript Settings"
        '
        'grpsurescripSecurMsg
        '
        Me.grpsurescripSecurMsg.BackColor = System.Drawing.Color.FromArgb(CType(CType(207,Byte),Integer), CType(CType(224,Byte),Integer), CType(CType(248,Byte),Integer))
        Me.grpsurescripSecurMsg.Controls.Add(Me.chksecureSureScriptsetting)
        Me.grpsurescripSecurMsg.Dock = System.Windows.Forms.DockStyle.Top
        Me.grpsurescripSecurMsg.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.grpsurescripSecurMsg.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.grpsurescripSecurMsg.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.grpsurescripSecurMsg.Location = New System.Drawing.Point(6, 168)
        Me.grpsurescripSecurMsg.Name = "grpsurescripSecurMsg"
        Me.grpsurescripSecurMsg.Size = New System.Drawing.Size(672, 55)
        Me.grpsurescripSecurMsg.TabIndex = 2
        Me.grpsurescripSecurMsg.TabStop = false
        Me.grpsurescripSecurMsg.Text = "Provider DIRECT Message Settings"
        '
        'chksecureSureScriptsetting
        '
        Me.chksecureSureScriptsetting.AutoSize = true
        Me.chksecureSureScriptsetting.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chksecureSureScriptsetting.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.chksecureSureScriptsetting.Location = New System.Drawing.Point(38, 25)
        Me.chksecureSureScriptsetting.Name = "chksecureSureScriptsetting"
        Me.chksecureSureScriptsetting.Size = New System.Drawing.Size(241, 18)
        Me.chksecureSureScriptsetting.TabIndex = 0
        Me.chksecureSureScriptsetting.Text = "Add Referrals Note in Message Content"
        Me.chksecureSureScriptsetting.UseVisualStyleBackColor = true
        '
        'chkShowOffFormularyAlternatives
        '
        Me.chkShowOffFormularyAlternatives.AutoSize = true
        Me.chkShowOffFormularyAlternatives.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkShowOffFormularyAlternatives.Location = New System.Drawing.Point(63, 500)
        Me.chkShowOffFormularyAlternatives.Name = "chkShowOffFormularyAlternatives"
        Me.chkShowOffFormularyAlternatives.Size = New System.Drawing.Size(202, 18)
        Me.chkShowOffFormularyAlternatives.TabIndex = 46
        Me.chkShowOffFormularyAlternatives.Text = "Show Off Formulary Alternatives"
        Me.chkShowOffFormularyAlternatives.UseVisualStyleBackColor = true
        Me.chkShowOffFormularyAlternatives.Visible = false
        '
        'chkMachineFormularyAlert
        '
        Me.chkMachineFormularyAlert.AutoSize = true
        Me.chkMachineFormularyAlert.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMachineFormularyAlert.Location = New System.Drawing.Point(63, 524)
        Me.chkMachineFormularyAlert.Name = "chkMachineFormularyAlert"
        Me.chkMachineFormularyAlert.Size = New System.Drawing.Size(156, 18)
        Me.chkMachineFormularyAlert.TabIndex = 44
        Me.chkMachineFormularyAlert.Text = "Machine Formulary Alert"
        Me.chkMachineFormularyAlert.UseVisualStyleBackColor = true
        Me.chkMachineFormularyAlert.Visible = false
        '
        'grpFormularySettings
        '
        Me.grpFormularySettings.Controls.Add(Me.chkShowNDCInMedication)
        Me.grpFormularySettings.Controls.Add(Me.chkNDCInAlternativeGrid)
        Me.grpFormularySettings.Dock = System.Windows.Forms.DockStyle.Top
        Me.grpFormularySettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.grpFormularySettings.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold)
        Me.grpFormularySettings.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.grpFormularySettings.Location = New System.Drawing.Point(6, 86)
        Me.grpFormularySettings.Name = "grpFormularySettings"
        Me.grpFormularySettings.Size = New System.Drawing.Size(672, 82)
        Me.grpFormularySettings.TabIndex = 1
        Me.grpFormularySettings.TabStop = false
        Me.grpFormularySettings.Text = "Formulary Settings"
        '
        'chkShowNDCInMedication
        '
        Me.chkShowNDCInMedication.AutoSize = true
        Me.chkShowNDCInMedication.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkShowNDCInMedication.Location = New System.Drawing.Point(38, 54)
        Me.chkShowNDCInMedication.Name = "chkShowNDCInMedication"
        Me.chkShowNDCInMedication.Size = New System.Drawing.Size(202, 18)
        Me.chkShowNDCInMedication.TabIndex = 1
        Me.chkShowNDCInMedication.Text = "Show NDC In Medication History"
        Me.chkShowNDCInMedication.UseVisualStyleBackColor = true
        '
        'chkNDCInAlternativeGrid
        '
        Me.chkNDCInAlternativeGrid.AutoSize = true
        Me.chkNDCInAlternativeGrid.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkNDCInAlternativeGrid.Location = New System.Drawing.Point(38, 26)
        Me.chkNDCInAlternativeGrid.Name = "chkNDCInAlternativeGrid"
        Me.chkNDCInAlternativeGrid.Size = New System.Drawing.Size(167, 18)
        Me.chkNDCInAlternativeGrid.TabIndex = 0
        Me.chkNDCInAlternativeGrid.Text = "Show NDC In Alternatives"
        Me.chkNDCInAlternativeGrid.UseVisualStyleBackColor = true
        '
        'grpsurescriptalert
        '
        Me.grpsurescriptalert.BackColor = System.Drawing.Color.FromArgb(CType(CType(207,Byte),Integer), CType(CType(224,Byte),Integer), CType(CType(248,Byte),Integer))
        Me.grpsurescriptalert.Controls.Add(Me.chkSurescriptFaxSettings)
        Me.grpsurescriptalert.Controls.Add(Me.Panel1)
        Me.grpsurescriptalert.Controls.Add(Me.chksureAlert)
        Me.grpsurescriptalert.Dock = System.Windows.Forms.DockStyle.Top
        Me.grpsurescriptalert.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.grpsurescriptalert.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.grpsurescriptalert.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.grpsurescriptalert.Location = New System.Drawing.Point(6, 6)
        Me.grpsurescriptalert.Name = "grpsurescriptalert"
        Me.grpsurescriptalert.Size = New System.Drawing.Size(672, 80)
        Me.grpsurescriptalert.TabIndex = 0
        Me.grpsurescriptalert.TabStop = false
        Me.grpsurescriptalert.Text = "Surescript Settings"
        '
        'chkSurescriptFaxSettings
        '
        Me.chkSurescriptFaxSettings.AutoSize = true
        Me.chkSurescriptFaxSettings.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkSurescriptFaxSettings.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.chkSurescriptFaxSettings.Location = New System.Drawing.Point(38, 49)
        Me.chkSurescriptFaxSettings.Name = "chkSurescriptFaxSettings"
        Me.chkSurescriptFaxSettings.Size = New System.Drawing.Size(225, 18)
        Me.chkSurescriptFaxSettings.TabIndex = 3
        Me.chkSurescriptFaxSettings.Text = "Enable Fax for Surescript Pharmacies"
        Me.chkSurescriptFaxSettings.UseVisualStyleBackColor = true
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.cmbInterval)
        Me.Panel1.Controls.Add(Me.Label26)
        Me.Panel1.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Panel1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Panel1.Location = New System.Drawing.Point(229, 15)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(213, 25)
        Me.Panel1.TabIndex = 2
        '
        'cmbInterval
        '
        Me.cmbInterval.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbInterval.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.cmbInterval.FormattingEnabled = true
        Me.cmbInterval.Location = New System.Drawing.Point(107, 2)
        Me.cmbInterval.Name = "cmbInterval"
        Me.cmbInterval.Size = New System.Drawing.Size(75, 22)
        Me.cmbInterval.TabIndex = 0
        '
        'Label26
        '
        Me.Label26.AutoSize = true
        Me.Label26.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label26.Location = New System.Drawing.Point(0, 6)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(108, 14)
        Me.Label26.TabIndex = 4
        Me.Label26.Text = "Show Alert After :"
        Me.Label26.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'chksureAlert
        '
        Me.chksureAlert.AutoSize = true
        Me.chksureAlert.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chksureAlert.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.chksureAlert.Location = New System.Drawing.Point(38, 21)
        Me.chksureAlert.Name = "chksureAlert"
        Me.chksureAlert.Size = New System.Drawing.Size(185, 18)
        Me.chksureAlert.TabIndex = 1
        Me.chksureAlert.Text = "Show Pending Message Alert"
        Me.chksureAlert.UseVisualStyleBackColor = true
        '
        'chkFormularyAlertnativesOffFormularyDrgs
        '
        Me.chkFormularyAlertnativesOffFormularyDrgs.AutoSize = true
        Me.chkFormularyAlertnativesOffFormularyDrgs.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkFormularyAlertnativesOffFormularyDrgs.Location = New System.Drawing.Point(63, 449)
        Me.chkFormularyAlertnativesOffFormularyDrgs.Name = "chkFormularyAlertnativesOffFormularyDrgs"
        Me.chkFormularyAlertnativesOffFormularyDrgs.Size = New System.Drawing.Size(312, 18)
        Me.chkFormularyAlertnativesOffFormularyDrgs.TabIndex = 42
        Me.chkFormularyAlertnativesOffFormularyDrgs.Text = "Show Formulary Alternatives for Off Formulary Drugs"
        Me.chkFormularyAlertnativesOffFormularyDrgs.UseVisualStyleBackColor = true
        Me.chkFormularyAlertnativesOffFormularyDrgs.Visible = false
        '
        'Label88
        '
        Me.Label88.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label88.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label88.Location = New System.Drawing.Point(6, 642)
        Me.Label88.Name = "Label88"
        Me.Label88.Size = New System.Drawing.Size(672, 5)
        Me.Label88.TabIndex = 30
        Me.Label88.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'chkFormularyAlertnativesNRDrgs
        '
        Me.chkFormularyAlertnativesNRDrgs.AutoSize = true
        Me.chkFormularyAlertnativesNRDrgs.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkFormularyAlertnativesNRDrgs.Location = New System.Drawing.Point(63, 476)
        Me.chkFormularyAlertnativesNRDrgs.Name = "chkFormularyAlertnativesNRDrgs"
        Me.chkFormularyAlertnativesNRDrgs.Size = New System.Drawing.Size(334, 18)
        Me.chkFormularyAlertnativesNRDrgs.TabIndex = 43
        Me.chkFormularyAlertnativesNRDrgs.Text = "Show Formulary Alternatives for Not Reimbursable Drugs"
        Me.chkFormularyAlertnativesNRDrgs.UseVisualStyleBackColor = true
        Me.chkFormularyAlertnativesNRDrgs.Visible = false
        '
        'Label89
        '
        Me.Label89.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label89.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label89.Location = New System.Drawing.Point(6, 1)
        Me.Label89.Name = "Label89"
        Me.Label89.Size = New System.Drawing.Size(672, 5)
        Me.Label89.TabIndex = 27
        Me.Label89.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'chkFormularyAlertnativesAllDrgs
        '
        Me.chkFormularyAlertnativesAllDrgs.AutoSize = true
        Me.chkFormularyAlertnativesAllDrgs.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkFormularyAlertnativesAllDrgs.Location = New System.Drawing.Point(63, 422)
        Me.chkFormularyAlertnativesAllDrgs.Name = "chkFormularyAlertnativesAllDrgs"
        Me.chkFormularyAlertnativesAllDrgs.Size = New System.Drawing.Size(251, 18)
        Me.chkFormularyAlertnativesAllDrgs.TabIndex = 41
        Me.chkFormularyAlertnativesAllDrgs.Text = "Show Formulary Alternatives for All Drugs"
        Me.chkFormularyAlertnativesAllDrgs.UseVisualStyleBackColor = true
        Me.chkFormularyAlertnativesAllDrgs.Visible = false
        '
        'Label90
        '
        Me.Label90.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label90.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label90.Location = New System.Drawing.Point(1, 1)
        Me.Label90.Name = "Label90"
        Me.Label90.Size = New System.Drawing.Size(5, 646)
        Me.Label90.TabIndex = 28
        Me.Label90.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label91
        '
        Me.Label91.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label91.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label91.Location = New System.Drawing.Point(678, 1)
        Me.Label91.Name = "Label91"
        Me.Label91.Size = New System.Drawing.Size(5, 646)
        Me.Label91.TabIndex = 29
        Me.Label91.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label44
        '
        Me.Label44.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label44.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label44.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label44.Location = New System.Drawing.Point(1, 647)
        Me.Label44.Name = "Label44"
        Me.Label44.Size = New System.Drawing.Size(682, 1)
        Me.Label44.TabIndex = 26
        Me.Label44.Text = "label2"
        '
        'Label45
        '
        Me.Label45.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label45.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label45.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label45.Location = New System.Drawing.Point(0, 1)
        Me.Label45.Name = "Label45"
        Me.Label45.Size = New System.Drawing.Size(1, 647)
        Me.Label45.TabIndex = 25
        Me.Label45.Text = "label4"
        '
        'Label46
        '
        Me.Label46.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label46.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label46.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label46.Location = New System.Drawing.Point(683, 1)
        Me.Label46.Name = "Label46"
        Me.Label46.Size = New System.Drawing.Size(1, 647)
        Me.Label46.TabIndex = 24
        Me.Label46.Text = "label3"
        '
        'Label47
        '
        Me.Label47.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label47.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label47.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label47.Location = New System.Drawing.Point(0, 0)
        Me.Label47.Name = "Label47"
        Me.Label47.Size = New System.Drawing.Size(684, 1)
        Me.Label47.TabIndex = 23
        Me.Label47.Text = "label1"
        '
        'tp_DMSSettings
        '
        Me.tp_DMSSettings.BackColor = System.Drawing.Color.FromArgb(CType(CType(207,Byte),Integer), CType(CType(224,Byte),Integer), CType(CType(248,Byte),Integer))
        Me.tp_DMSSettings.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tp_DMSSettings.Controls.Add(Me.GroupBox9)
        Me.tp_DMSSettings.Controls.Add(Me.Panel31)
        Me.tp_DMSSettings.Controls.Add(Me.GroupBox23)
        Me.tp_DMSSettings.Controls.Add(Me.Label92)
        Me.tp_DMSSettings.Controls.Add(Me.Label93)
        Me.tp_DMSSettings.Controls.Add(Me.Label94)
        Me.tp_DMSSettings.Controls.Add(Me.Label95)
        Me.tp_DMSSettings.Controls.Add(Me.Label40)
        Me.tp_DMSSettings.Controls.Add(Me.Label41)
        Me.tp_DMSSettings.Controls.Add(Me.Label42)
        Me.tp_DMSSettings.Controls.Add(Me.Label43)
        Me.tp_DMSSettings.Location = New System.Drawing.Point(4, 42)
        Me.tp_DMSSettings.Name = "tp_DMSSettings"
        Me.tp_DMSSettings.Size = New System.Drawing.Size(684, 648)
        Me.tp_DMSSettings.TabIndex = 6
        Me.tp_DMSSettings.Text = "DMS Settings"
        '
        'GroupBox9
        '
        Me.GroupBox9.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox9.Controls.Add(Me.Label105)
        Me.GroupBox9.Controls.Add(Me.Label104)
        Me.GroupBox9.Controls.Add(Me.numBufferSize)
        Me.GroupBox9.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox9.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.GroupBox9.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.GroupBox9.Location = New System.Drawing.Point(6, 330)
        Me.GroupBox9.Name = "GroupBox9"
        Me.GroupBox9.Size = New System.Drawing.Size(672, 51)
        Me.GroupBox9.TabIndex = 3
        Me.GroupBox9.TabStop = false
        '
        'Label105
        '
        Me.Label105.AutoSize = true
        Me.Label105.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label105.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label105.Location = New System.Drawing.Point(167, 23)
        Me.Label105.Name = "Label105"
        Me.Label105.Size = New System.Drawing.Size(23, 14)
        Me.Label105.TabIndex = 40
        Me.Label105.Text = "MB"
        '
        'Label104
        '
        Me.Label104.AutoSize = true
        Me.Label104.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label104.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label104.Location = New System.Drawing.Point(28, 23)
        Me.Label104.Name = "Label104"
        Me.Label104.Size = New System.Drawing.Size(73, 14)
        Me.Label104.TabIndex = 40
        Me.Label104.Text = "Buffer Size :"
        '
        'numBufferSize
        '
        Me.numBufferSize.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.numBufferSize.Location = New System.Drawing.Point(104, 19)
        Me.numBufferSize.Maximum = New Decimal(New Integer() {10, 0, 0, 0})
        Me.numBufferSize.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.numBufferSize.Name = "numBufferSize"
        Me.numBufferSize.Size = New System.Drawing.Size(59, 22)
        Me.numBufferSize.TabIndex = 0
        Me.numBufferSize.Value = New Decimal(New Integer() {10, 0, 0, 0})
        '
        'Panel31
        '
        Me.Panel31.Controls.Add(Me.GroupBox3)
        Me.Panel31.Controls.Add(Me.pnlRemoteScan)
        Me.Panel31.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel31.Location = New System.Drawing.Point(6, 79)
        Me.Panel31.Name = "Panel31"
        Me.Panel31.Size = New System.Drawing.Size(672, 251)
        Me.Panel31.TabIndex = 71
        '
        'GroupBox3
        '
        Me.GroupBox3.BackColor = System.Drawing.Color.FromArgb(CType(CType(207,Byte),Integer), CType(CType(224,Byte),Integer), CType(CType(248,Byte),Integer))
        Me.GroupBox3.Controls.Add(Me.Label109)
        Me.GroupBox3.Controls.Add(Me.cmbImageFormat)
        Me.GroupBox3.Controls.Add(Me.Label211)
        Me.GroupBox3.Controls.Add(Me.Label212)
        Me.GroupBox3.Controls.Add(Me.Label107)
        Me.GroupBox3.Controls.Add(Me.lblCardLocation)
        Me.GroupBox3.Controls.Add(Me.txtStartX)
        Me.GroupBox3.Controls.Add(Me.txtStartY)
        Me.GroupBox3.Controls.Add(Me.Label119)
        Me.GroupBox3.Controls.Add(Me.cmbSupportedSize)
        Me.GroupBox3.Controls.Add(Me.lblSuportedSize)
        Me.GroupBox3.Controls.Add(Me.cmbScanMode)
        Me.GroupBox3.Controls.Add(Me.Label28)
        Me.GroupBox3.Controls.Add(Me.cmbBitDepth)
        Me.GroupBox3.Controls.Add(Me.Label32)
        Me.GroupBox3.Controls.Add(Me.Label117)
        Me.GroupBox3.Controls.Add(Me.Label120)
        Me.GroupBox3.Controls.Add(Me.txtCardWidth)
        Me.GroupBox3.Controls.Add(Me.txtCardLength)
        Me.GroupBox3.Controls.Add(Me.Label115)
        Me.GroupBox3.Controls.Add(Me.Label116)
        Me.GroupBox3.Controls.Add(Me.cmbScanSide)
        Me.GroupBox3.Controls.Add(Me.Label29)
        Me.GroupBox3.Controls.Add(Me.Label118)
        Me.GroupBox3.Controls.Add(Me.cmbContrast)
        Me.GroupBox3.Controls.Add(Me.Label19)
        Me.GroupBox3.Controls.Add(Me.cmbBrightness)
        Me.GroupBox3.Controls.Add(Me.Label27)
        Me.GroupBox3.Controls.Add(Me.chkShowScannerDialog)
        Me.GroupBox3.Controls.Add(Me.cmbResolution)
        Me.GroupBox3.Controls.Add(Me.Label30)
        Me.GroupBox3.Controls.Add(Me.cmbScanner)
        Me.GroupBox3.Controls.Add(Me.Label31)
        Me.GroupBox3.Controls.Add(Me.GroupBox4)
        Me.GroupBox3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox3.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.GroupBox3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.GroupBox3.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(672, 251)
        Me.GroupBox3.TabIndex = 2
        Me.GroupBox3.TabStop = false
        Me.GroupBox3.Text = "Scanner Settings"
        '
        'Label109
        '
        Me.Label109.AutoSize = true
        Me.Label109.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label109.Location = New System.Drawing.Point(340, 115)
        Me.Label109.Name = "Label109"
        Me.Label109.Size = New System.Drawing.Size(91, 14)
        Me.Label109.TabIndex = 115
        Me.Label109.Text = "Image Format :"
        '
        'cmbImageFormat
        '
        Me.cmbImageFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbImageFormat.Enabled = false
        Me.cmbImageFormat.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.cmbImageFormat.FormattingEnabled = true
        Me.cmbImageFormat.Location = New System.Drawing.Point(433, 111)
        Me.cmbImageFormat.Name = "cmbImageFormat"
        Me.cmbImageFormat.Size = New System.Drawing.Size(219, 22)
        Me.cmbImageFormat.TabIndex = 114
        '
        'Label211
        '
        Me.Label211.AutoSize = true
        Me.Label211.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label211.Location = New System.Drawing.Point(455, 203)
        Me.Label211.Name = "Label211"
        Me.Label211.Size = New System.Drawing.Size(26, 11)
        Me.Label211.TabIndex = 112
        Me.Label211.Text = "(Left)"
        '
        'Label212
        '
        Me.Label212.AutoSize = true
        Me.Label212.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label212.Location = New System.Drawing.Point(545, 203)
        Me.Label212.Name = "Label212"
        Me.Label212.Size = New System.Drawing.Size(27, 11)
        Me.Label212.TabIndex = 113
        Me.Label212.Text = "(Top)"
        '
        'Label107
        '
        Me.Label107.AutoSize = true
        Me.Label107.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label107.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label107.Location = New System.Drawing.Point(597, 181)
        Me.Label107.Name = "Label107"
        Me.Label107.Size = New System.Drawing.Size(43, 14)
        Me.Label107.TabIndex = 111
        Me.Label107.Text = "Inches"
        '
        'lblCardLocation
        '
        Me.lblCardLocation.AutoSize = true
        Me.lblCardLocation.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblCardLocation.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.lblCardLocation.Location = New System.Drawing.Point(342, 181)
        Me.lblCardLocation.Name = "lblCardLocation"
        Me.lblCardLocation.Size = New System.Drawing.Size(89, 14)
        Me.lblCardLocation.TabIndex = 110
        Me.lblCardLocation.Text = "Card Location :"
        '
        'txtStartX
        '
        Me.txtStartX.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtStartX.Location = New System.Drawing.Point(433, 177)
        Me.txtStartX.Name = "txtStartX"
        Me.txtStartX.ShortcutsEnabled = false
        Me.txtStartX.Size = New System.Drawing.Size(71, 22)
        Me.txtStartX.TabIndex = 11
        '
        'txtStartY
        '
        Me.txtStartY.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtStartY.Location = New System.Drawing.Point(523, 177)
        Me.txtStartY.Name = "txtStartY"
        Me.txtStartY.ShortcutsEnabled = false
        Me.txtStartY.Size = New System.Drawing.Size(71, 22)
        Me.txtStartY.TabIndex = 12
        '
        'Label119
        '
        Me.Label119.AutoSize = true
        Me.Label119.Font = New System.Drawing.Font("Sylfaen", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label119.Location = New System.Drawing.Point(507, 180)
        Me.Label119.Name = "Label119"
        Me.Label119.Size = New System.Drawing.Size(16, 16)
        Me.Label119.TabIndex = 107
        Me.Label119.Text = "X"
        '
        'cmbSupportedSize
        '
        Me.cmbSupportedSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSupportedSize.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.cmbSupportedSize.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.cmbSupportedSize.FormattingEnabled = true
        Me.cmbSupportedSize.Location = New System.Drawing.Point(109, 139)
        Me.cmbSupportedSize.Name = "cmbSupportedSize"
        Me.cmbSupportedSize.Size = New System.Drawing.Size(219, 22)
        Me.cmbSupportedSize.TabIndex = 10
        '
        'lblSuportedSize
        '
        Me.lblSuportedSize.AutoSize = true
        Me.lblSuportedSize.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblSuportedSize.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.lblSuportedSize.Location = New System.Drawing.Point(9, 143)
        Me.lblSuportedSize.Name = "lblSuportedSize"
        Me.lblSuportedSize.Size = New System.Drawing.Size(98, 14)
        Me.lblSuportedSize.TabIndex = 103
        Me.lblSuportedSize.Text = "Supported Size :"
        '
        'cmbScanMode
        '
        Me.cmbScanMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbScanMode.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.cmbScanMode.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.cmbScanMode.FormattingEnabled = true
        Me.cmbScanMode.Location = New System.Drawing.Point(433, 27)
        Me.cmbScanMode.Name = "cmbScanMode"
        Me.cmbScanMode.Size = New System.Drawing.Size(219, 22)
        Me.cmbScanMode.TabIndex = 2
        '
        'Label28
        '
        Me.Label28.AutoSize = true
        Me.Label28.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label28.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label28.Location = New System.Drawing.Point(356, 31)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(75, 14)
        Me.Label28.TabIndex = 101
        Me.Label28.Text = "Scan Mode :"
        '
        'cmbBitDepth
        '
        Me.cmbBitDepth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbBitDepth.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.cmbBitDepth.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.cmbBitDepth.FormattingEnabled = true
        Me.cmbBitDepth.Location = New System.Drawing.Point(109, 55)
        Me.cmbBitDepth.Name = "cmbBitDepth"
        Me.cmbBitDepth.Size = New System.Drawing.Size(219, 22)
        Me.cmbBitDepth.TabIndex = 3
        '
        'Label32
        '
        Me.Label32.AutoSize = true
        Me.Label32.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label32.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label32.Location = New System.Drawing.Point(28, 59)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(79, 14)
        Me.Label32.TabIndex = 99
        Me.Label32.Text = "Scan Depth :"
        '
        'Label117
        '
        Me.Label117.AutoSize = true
        Me.Label117.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label117.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label117.Location = New System.Drawing.Point(597, 143)
        Me.Label117.Name = "Label117"
        Me.Label117.Size = New System.Drawing.Size(43, 14)
        Me.Label117.TabIndex = 98
        Me.Label117.Text = "Inches"
        '
        'Label120
        '
        Me.Label120.AutoSize = true
        Me.Label120.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label120.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label120.Location = New System.Drawing.Point(367, 143)
        Me.Label120.Name = "Label120"
        Me.Label120.Size = New System.Drawing.Size(64, 14)
        Me.Label120.TabIndex = 97
        Me.Label120.Text = "Card Size :"
        '
        'txtCardWidth
        '
        Me.txtCardWidth.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtCardWidth.Location = New System.Drawing.Point(433, 139)
        Me.txtCardWidth.Name = "txtCardWidth"
        Me.txtCardWidth.ShortcutsEnabled = false
        Me.txtCardWidth.Size = New System.Drawing.Size(71, 22)
        Me.txtCardWidth.TabIndex = 8
        '
        'txtCardLength
        '
        Me.txtCardLength.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtCardLength.Location = New System.Drawing.Point(523, 139)
        Me.txtCardLength.Name = "txtCardLength"
        Me.txtCardLength.ShortcutsEnabled = false
        Me.txtCardLength.Size = New System.Drawing.Size(71, 22)
        Me.txtCardLength.TabIndex = 9
        '
        'Label115
        '
        Me.Label115.AutoSize = true
        Me.Label115.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label115.Location = New System.Drawing.Point(451, 164)
        Me.Label115.Name = "Label115"
        Me.Label115.Size = New System.Drawing.Size(34, 11)
        Me.Label115.TabIndex = 95
        Me.Label115.Text = "(Width)"
        '
        'Label116
        '
        Me.Label116.AutoSize = true
        Me.Label116.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label116.Location = New System.Drawing.Point(539, 164)
        Me.Label116.Name = "Label116"
        Me.Label116.Size = New System.Drawing.Size(38, 11)
        Me.Label116.TabIndex = 96
        Me.Label116.Text = "(Length)"
        '
        'cmbScanSide
        '
        Me.cmbScanSide.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbScanSide.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.cmbScanSide.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.cmbScanSide.FormattingEnabled = true
        Me.cmbScanSide.Location = New System.Drawing.Point(109, 111)
        Me.cmbScanSide.Name = "cmbScanSide"
        Me.cmbScanSide.Size = New System.Drawing.Size(219, 22)
        Me.cmbScanSide.TabIndex = 7
        '
        'Label29
        '
        Me.Label29.AutoSize = true
        Me.Label29.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label29.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label29.Location = New System.Drawing.Point(39, 115)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(68, 14)
        Me.Label29.TabIndex = 81
        Me.Label29.Text = "Scan Side :"
        '
        'Label118
        '
        Me.Label118.AutoSize = true
        Me.Label118.Font = New System.Drawing.Font("Sylfaen", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label118.Location = New System.Drawing.Point(507, 142)
        Me.Label118.Name = "Label118"
        Me.Label118.Size = New System.Drawing.Size(16, 16)
        Me.Label118.TabIndex = 94
        Me.Label118.Text = "X"
        '
        'cmbContrast
        '
        Me.cmbContrast.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbContrast.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.cmbContrast.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.cmbContrast.FormattingEnabled = true
        Me.cmbContrast.Location = New System.Drawing.Point(433, 83)
        Me.cmbContrast.Name = "cmbContrast"
        Me.cmbContrast.Size = New System.Drawing.Size(219, 22)
        Me.cmbContrast.TabIndex = 6
        '
        'Label19
        '
        Me.Label19.AutoSize = true
        Me.Label19.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label19.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label19.Location = New System.Drawing.Point(370, 87)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(61, 14)
        Me.Label19.TabIndex = 90
        Me.Label19.Text = "Contrast :"
        '
        'cmbBrightness
        '
        Me.cmbBrightness.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbBrightness.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.cmbBrightness.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.cmbBrightness.FormattingEnabled = true
        Me.cmbBrightness.Location = New System.Drawing.Point(109, 83)
        Me.cmbBrightness.Name = "cmbBrightness"
        Me.cmbBrightness.Size = New System.Drawing.Size(219, 22)
        Me.cmbBrightness.TabIndex = 5
        '
        'Label27
        '
        Me.Label27.AutoSize = true
        Me.Label27.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label27.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label27.Location = New System.Drawing.Point(36, 87)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(71, 14)
        Me.Label27.TabIndex = 88
        Me.Label27.Text = "Brightness :"
        '
        'chkShowScannerDialog
        '
        Me.chkShowScannerDialog.AutoSize = true
        Me.chkShowScannerDialog.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkShowScannerDialog.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.chkShowScannerDialog.Location = New System.Drawing.Point(433, 222)
        Me.chkShowScannerDialog.Name = "chkShowScannerDialog"
        Me.chkShowScannerDialog.Size = New System.Drawing.Size(141, 18)
        Me.chkShowScannerDialog.TabIndex = 13
        Me.chkShowScannerDialog.Text = "Show Scanner Dialog"
        Me.chkShowScannerDialog.UseVisualStyleBackColor = true
        '
        'cmbResolution
        '
        Me.cmbResolution.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbResolution.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.cmbResolution.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.cmbResolution.FormattingEnabled = true
        Me.cmbResolution.Location = New System.Drawing.Point(433, 55)
        Me.cmbResolution.Name = "cmbResolution"
        Me.cmbResolution.Size = New System.Drawing.Size(219, 22)
        Me.cmbResolution.TabIndex = 4
        '
        'Label30
        '
        Me.Label30.AutoSize = true
        Me.Label30.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label30.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label30.Location = New System.Drawing.Point(360, 59)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(71, 14)
        Me.Label30.TabIndex = 82
        Me.Label30.Text = "Resolution :"
        '
        'cmbScanner
        '
        Me.cmbScanner.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbScanner.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.cmbScanner.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.cmbScanner.FormattingEnabled = true
        Me.cmbScanner.Location = New System.Drawing.Point(109, 27)
        Me.cmbScanner.Name = "cmbScanner"
        Me.cmbScanner.Size = New System.Drawing.Size(219, 22)
        Me.cmbScanner.TabIndex = 1
        '
        'Label31
        '
        Me.Label31.AutoSize = true
        Me.Label31.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label31.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label31.Location = New System.Drawing.Point(48, 31)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(59, 14)
        Me.Label31.TabIndex = 83
        Me.Label31.Text = "Scanner :"
        '
        'GroupBox4
        '
        Me.GroupBox4.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox4.Controls.Add(Me.rbScanDocumentWithMonth)
        Me.GroupBox4.Controls.Add(Me.rbScanDocumentWithoutMonth)
        Me.GroupBox4.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.GroupBox4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.GroupBox4.Location = New System.Drawing.Point(24, 335)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(452, 50)
        Me.GroupBox4.TabIndex = 58
        Me.GroupBox4.TabStop = false
        Me.GroupBox4.Text = "Document View"
        Me.GroupBox4.Visible = false
        '
        'rbScanDocumentWithMonth
        '
        Me.rbScanDocumentWithMonth.AutoSize = true
        Me.rbScanDocumentWithMonth.BackColor = System.Drawing.Color.Transparent
        Me.rbScanDocumentWithMonth.Checked = true
        Me.rbScanDocumentWithMonth.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rbScanDocumentWithMonth.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.rbScanDocumentWithMonth.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.rbScanDocumentWithMonth.Location = New System.Drawing.Point(20, 21)
        Me.rbScanDocumentWithMonth.Name = "rbScanDocumentWithMonth"
        Me.rbScanDocumentWithMonth.Size = New System.Drawing.Size(171, 18)
        Me.rbScanDocumentWithMonth.TabIndex = 18
        Me.rbScanDocumentWithMonth.TabStop = true
        Me.rbScanDocumentWithMonth.Text = "Documents With Month"
        Me.rbScanDocumentWithMonth.UseVisualStyleBackColor = false
        '
        'rbScanDocumentWithoutMonth
        '
        Me.rbScanDocumentWithoutMonth.AutoSize = true
        Me.rbScanDocumentWithoutMonth.BackColor = System.Drawing.Color.Transparent
        Me.rbScanDocumentWithoutMonth.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rbScanDocumentWithoutMonth.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.rbScanDocumentWithoutMonth.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.rbScanDocumentWithoutMonth.Location = New System.Drawing.Point(243, 21)
        Me.rbScanDocumentWithoutMonth.Name = "rbScanDocumentWithoutMonth"
        Me.rbScanDocumentWithoutMonth.Size = New System.Drawing.Size(174, 18)
        Me.rbScanDocumentWithoutMonth.TabIndex = 54
        Me.rbScanDocumentWithoutMonth.Text = "Documents Without Month"
        Me.rbScanDocumentWithoutMonth.UseVisualStyleBackColor = false
        '
        'pnlRemoteScan
        '
        Me.pnlRemoteScan.BackColor = System.Drawing.Color.FromArgb(CType(CType(207,Byte),Integer), CType(CType(224,Byte),Integer), CType(CType(248,Byte),Integer))
        Me.pnlRemoteScan.Controls.Add(Me.cmbRemoteScanSideFeeder)
        Me.pnlRemoteScan.Controls.Add(Me.cmbRemoteImageFormat)
        Me.pnlRemoteScan.Controls.Add(Me.Label110)
        Me.pnlRemoteScan.Controls.Add(Me.Label233)
        Me.pnlRemoteScan.Controls.Add(Me.txtRemoteStartX)
        Me.pnlRemoteScan.Controls.Add(Me.cmbRemoteSupportedSize)
        Me.pnlRemoteScan.Controls.Add(Me.txtRemoteStartY)
        Me.pnlRemoteScan.Controls.Add(Me.Label234)
        Me.pnlRemoteScan.Controls.Add(Me.Label235)
        Me.pnlRemoteScan.Controls.Add(Me.Label236)
        Me.pnlRemoteScan.Controls.Add(Me.Label237)
        Me.pnlRemoteScan.Controls.Add(Me.chkRemoteShowScannerDialog)
        Me.pnlRemoteScan.Controls.Add(Me.txtRemoteCardWidth)
        Me.pnlRemoteScan.Controls.Add(Me.Label238)
        Me.pnlRemoteScan.Controls.Add(Me.txtRemoteCardLength)
        Me.pnlRemoteScan.Controls.Add(Me.Label239)
        Me.pnlRemoteScan.Controls.Add(Me.Label240)
        Me.pnlRemoteScan.Controls.Add(Me.cmbRemoteBitDepth)
        Me.pnlRemoteScan.Controls.Add(Me.Label241)
        Me.pnlRemoteScan.Controls.Add(Me.Label242)
        Me.pnlRemoteScan.Controls.Add(Me.Label243)
        Me.pnlRemoteScan.Controls.Add(Me.Label244)
        Me.pnlRemoteScan.Controls.Add(Me.Label245)
        Me.pnlRemoteScan.Controls.Add(Me.Label246)
        Me.pnlRemoteScan.Controls.Add(Me.cmbRemoteContrast)
        Me.pnlRemoteScan.Controls.Add(Me.Label247)
        Me.pnlRemoteScan.Controls.Add(Me.Label248)
        Me.pnlRemoteScan.Controls.Add(Me.cmbRemoteScanSide)
        Me.pnlRemoteScan.Controls.Add(Me.cmbRemoteBrightness)
        Me.pnlRemoteScan.Controls.Add(Me.Label249)
        Me.pnlRemoteScan.Controls.Add(Me.Label250)
        Me.pnlRemoteScan.Controls.Add(Me.GroupBox24)
        Me.pnlRemoteScan.Controls.Add(Me.cmbRemoteScanMode)
        Me.pnlRemoteScan.Controls.Add(Me.cmbRemoteResolution)
        Me.pnlRemoteScan.Controls.Add(Me.cmbRemoteScanner)
        Me.pnlRemoteScan.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlRemoteScan.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.pnlRemoteScan.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.pnlRemoteScan.Location = New System.Drawing.Point(0, 0)
        Me.pnlRemoteScan.Name = "pnlRemoteScan"
        Me.pnlRemoteScan.Size = New System.Drawing.Size(672, 251)
        Me.pnlRemoteScan.TabIndex = 69
        Me.pnlRemoteScan.TabStop = false
        Me.pnlRemoteScan.Text = "Local Settings"
        '
        'cmbRemoteScanSideFeeder
        '
        Me.cmbRemoteScanSideFeeder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbRemoteScanSideFeeder.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.cmbRemoteScanSideFeeder.FormattingEnabled = true
        Me.cmbRemoteScanSideFeeder.Location = New System.Drawing.Point(222, 112)
        Me.cmbRemoteScanSideFeeder.Name = "cmbRemoteScanSideFeeder"
        Me.cmbRemoteScanSideFeeder.Size = New System.Drawing.Size(106, 22)
        Me.cmbRemoteScanSideFeeder.TabIndex = 7
        '
        'cmbRemoteImageFormat
        '
        Me.cmbRemoteImageFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbRemoteImageFormat.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.cmbRemoteImageFormat.FormattingEnabled = true
        Me.cmbRemoteImageFormat.Location = New System.Drawing.Point(433, 112)
        Me.cmbRemoteImageFormat.Name = "cmbRemoteImageFormat"
        Me.cmbRemoteImageFormat.Size = New System.Drawing.Size(219, 22)
        Me.cmbRemoteImageFormat.TabIndex = 8
        '
        'Label110
        '
        Me.Label110.AutoSize = true
        Me.Label110.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label110.Location = New System.Drawing.Point(340, 116)
        Me.Label110.Name = "Label110"
        Me.Label110.Size = New System.Drawing.Size(91, 14)
        Me.Label110.TabIndex = 117
        Me.Label110.Text = "Image Format :"
        '
        'Label233
        '
        Me.Label233.AutoSize = true
        Me.Label233.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label233.Location = New System.Drawing.Point(455, 206)
        Me.Label233.Name = "Label233"
        Me.Label233.Size = New System.Drawing.Size(26, 11)
        Me.Label233.TabIndex = 112
        Me.Label233.Text = "(Left)"
        '
        'txtRemoteStartX
        '
        Me.txtRemoteStartX.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtRemoteStartX.Location = New System.Drawing.Point(433, 181)
        Me.txtRemoteStartX.MaxLength = 10
        Me.txtRemoteStartX.Name = "txtRemoteStartX"
        Me.txtRemoteStartX.ShortcutsEnabled = false
        Me.txtRemoteStartX.Size = New System.Drawing.Size(71, 22)
        Me.txtRemoteStartX.TabIndex = 2
        '
        'cmbRemoteSupportedSize
        '
        Me.cmbRemoteSupportedSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbRemoteSupportedSize.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.cmbRemoteSupportedSize.FormattingEnabled = true
        Me.cmbRemoteSupportedSize.Location = New System.Drawing.Point(109, 142)
        Me.cmbRemoteSupportedSize.Name = "cmbRemoteSupportedSize"
        Me.cmbRemoteSupportedSize.Size = New System.Drawing.Size(219, 22)
        Me.cmbRemoteSupportedSize.TabIndex = 9
        '
        'txtRemoteStartY
        '
        Me.txtRemoteStartY.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtRemoteStartY.Location = New System.Drawing.Point(523, 181)
        Me.txtRemoteStartY.MaxLength = 10
        Me.txtRemoteStartY.Name = "txtRemoteStartY"
        Me.txtRemoteStartY.ShortcutsEnabled = false
        Me.txtRemoteStartY.Size = New System.Drawing.Size(71, 22)
        Me.txtRemoteStartY.TabIndex = 13
        '
        'Label234
        '
        Me.Label234.AutoSize = true
        Me.Label234.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label234.Location = New System.Drawing.Point(545, 206)
        Me.Label234.Name = "Label234"
        Me.Label234.Size = New System.Drawing.Size(27, 11)
        Me.Label234.TabIndex = 113
        Me.Label234.Text = "(Top)"
        '
        'Label235
        '
        Me.Label235.AutoSize = true
        Me.Label235.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label235.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label235.Location = New System.Drawing.Point(597, 185)
        Me.Label235.Name = "Label235"
        Me.Label235.Size = New System.Drawing.Size(43, 14)
        Me.Label235.TabIndex = 111
        Me.Label235.Text = "Inches"
        '
        'Label236
        '
        Me.Label236.AutoSize = true
        Me.Label236.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label236.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label236.Location = New System.Drawing.Point(340, 185)
        Me.Label236.Name = "Label236"
        Me.Label236.Size = New System.Drawing.Size(89, 14)
        Me.Label236.TabIndex = 110
        Me.Label236.Text = "Card Location :"
        '
        'Label237
        '
        Me.Label237.AutoSize = true
        Me.Label237.Font = New System.Drawing.Font("Sylfaen", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label237.Location = New System.Drawing.Point(507, 184)
        Me.Label237.Name = "Label237"
        Me.Label237.Size = New System.Drawing.Size(16, 16)
        Me.Label237.TabIndex = 107
        Me.Label237.Text = "X"
        '
        'chkRemoteShowScannerDialog
        '
        Me.chkRemoteShowScannerDialog.AutoSize = true
        Me.chkRemoteShowScannerDialog.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkRemoteShowScannerDialog.Location = New System.Drawing.Point(433, 221)
        Me.chkRemoteShowScannerDialog.Name = "chkRemoteShowScannerDialog"
        Me.chkRemoteShowScannerDialog.Size = New System.Drawing.Size(141, 18)
        Me.chkRemoteShowScannerDialog.TabIndex = 14
        Me.chkRemoteShowScannerDialog.Text = "Show Scanner Dialog"
        Me.chkRemoteShowScannerDialog.UseVisualStyleBackColor = true
        '
        'txtRemoteCardWidth
        '
        Me.txtRemoteCardWidth.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtRemoteCardWidth.Location = New System.Drawing.Point(433, 142)
        Me.txtRemoteCardWidth.MaxLength = 10
        Me.txtRemoteCardWidth.Name = "txtRemoteCardWidth"
        Me.txtRemoteCardWidth.ShortcutsEnabled = false
        Me.txtRemoteCardWidth.Size = New System.Drawing.Size(71, 22)
        Me.txtRemoteCardWidth.TabIndex = 10
        '
        'Label238
        '
        Me.Label238.AutoSize = true
        Me.Label238.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label238.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label238.Location = New System.Drawing.Point(8, 146)
        Me.Label238.Name = "Label238"
        Me.Label238.Size = New System.Drawing.Size(98, 14)
        Me.Label238.TabIndex = 103
        Me.Label238.Text = "Supported Size :"
        '
        'txtRemoteCardLength
        '
        Me.txtRemoteCardLength.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtRemoteCardLength.Location = New System.Drawing.Point(523, 142)
        Me.txtRemoteCardLength.MaxLength = 10
        Me.txtRemoteCardLength.Name = "txtRemoteCardLength"
        Me.txtRemoteCardLength.ShortcutsEnabled = false
        Me.txtRemoteCardLength.Size = New System.Drawing.Size(71, 22)
        Me.txtRemoteCardLength.TabIndex = 11
        '
        'Label239
        '
        Me.Label239.AutoSize = true
        Me.Label239.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label239.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label239.Location = New System.Drawing.Point(354, 31)
        Me.Label239.Name = "Label239"
        Me.Label239.Size = New System.Drawing.Size(75, 14)
        Me.Label239.TabIndex = 101
        Me.Label239.Text = "Scan Mode :"
        '
        'Label240
        '
        Me.Label240.AutoSize = true
        Me.Label240.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label240.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label240.Location = New System.Drawing.Point(27, 58)
        Me.Label240.Name = "Label240"
        Me.Label240.Size = New System.Drawing.Size(79, 14)
        Me.Label240.TabIndex = 99
        Me.Label240.Text = "Scan Depth :"
        '
        'cmbRemoteBitDepth
        '
        Me.cmbRemoteBitDepth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbRemoteBitDepth.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.cmbRemoteBitDepth.FormattingEnabled = true
        Me.cmbRemoteBitDepth.Location = New System.Drawing.Point(109, 54)
        Me.cmbRemoteBitDepth.Name = "cmbRemoteBitDepth"
        Me.cmbRemoteBitDepth.Size = New System.Drawing.Size(219, 22)
        Me.cmbRemoteBitDepth.TabIndex = 2
        '
        'Label241
        '
        Me.Label241.AutoSize = true
        Me.Label241.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label241.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label241.Location = New System.Drawing.Point(597, 146)
        Me.Label241.Name = "Label241"
        Me.Label241.Size = New System.Drawing.Size(43, 14)
        Me.Label241.TabIndex = 98
        Me.Label241.Text = "Inches"
        '
        'Label242
        '
        Me.Label242.AutoSize = true
        Me.Label242.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label242.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label242.Location = New System.Drawing.Point(365, 146)
        Me.Label242.Name = "Label242"
        Me.Label242.Size = New System.Drawing.Size(64, 14)
        Me.Label242.TabIndex = 97
        Me.Label242.Text = "Card Size :"
        '
        'Label243
        '
        Me.Label243.AutoSize = true
        Me.Label243.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label243.Location = New System.Drawing.Point(451, 166)
        Me.Label243.Name = "Label243"
        Me.Label243.Size = New System.Drawing.Size(34, 11)
        Me.Label243.TabIndex = 95
        Me.Label243.Text = "(Width)"
        '
        'Label244
        '
        Me.Label244.AutoSize = true
        Me.Label244.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label244.Location = New System.Drawing.Point(539, 166)
        Me.Label244.Name = "Label244"
        Me.Label244.Size = New System.Drawing.Size(38, 11)
        Me.Label244.TabIndex = 96
        Me.Label244.Text = "(Length)"
        '
        'Label245
        '
        Me.Label245.AutoSize = true
        Me.Label245.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label245.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label245.Location = New System.Drawing.Point(38, 116)
        Me.Label245.Name = "Label245"
        Me.Label245.Size = New System.Drawing.Size(68, 14)
        Me.Label245.TabIndex = 81
        Me.Label245.Text = "Scan Side :"
        '
        'Label246
        '
        Me.Label246.AutoSize = true
        Me.Label246.Font = New System.Drawing.Font("Sylfaen", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label246.Location = New System.Drawing.Point(507, 145)
        Me.Label246.Name = "Label246"
        Me.Label246.Size = New System.Drawing.Size(16, 16)
        Me.Label246.TabIndex = 94
        Me.Label246.Text = "X"
        '
        'cmbRemoteContrast
        '
        Me.cmbRemoteContrast.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbRemoteContrast.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.cmbRemoteContrast.FormattingEnabled = true
        Me.cmbRemoteContrast.Location = New System.Drawing.Point(433, 83)
        Me.cmbRemoteContrast.Name = "cmbRemoteContrast"
        Me.cmbRemoteContrast.Size = New System.Drawing.Size(219, 22)
        Me.cmbRemoteContrast.TabIndex = 5
        '
        'Label247
        '
        Me.Label247.AutoSize = true
        Me.Label247.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label247.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label247.Location = New System.Drawing.Point(368, 87)
        Me.Label247.Name = "Label247"
        Me.Label247.Size = New System.Drawing.Size(61, 14)
        Me.Label247.TabIndex = 90
        Me.Label247.Text = "Contrast :"
        '
        'Label248
        '
        Me.Label248.AutoSize = true
        Me.Label248.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label248.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label248.Location = New System.Drawing.Point(35, 87)
        Me.Label248.Name = "Label248"
        Me.Label248.Size = New System.Drawing.Size(71, 14)
        Me.Label248.TabIndex = 88
        Me.Label248.Text = "Brightness :"
        '
        'cmbRemoteScanSide
        '
        Me.cmbRemoteScanSide.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbRemoteScanSide.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.cmbRemoteScanSide.FormattingEnabled = true
        Me.cmbRemoteScanSide.Location = New System.Drawing.Point(109, 112)
        Me.cmbRemoteScanSide.Name = "cmbRemoteScanSide"
        Me.cmbRemoteScanSide.Size = New System.Drawing.Size(106, 22)
        Me.cmbRemoteScanSide.TabIndex = 6
        '
        'cmbRemoteBrightness
        '
        Me.cmbRemoteBrightness.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbRemoteBrightness.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.cmbRemoteBrightness.FormattingEnabled = true
        Me.cmbRemoteBrightness.Location = New System.Drawing.Point(109, 83)
        Me.cmbRemoteBrightness.Name = "cmbRemoteBrightness"
        Me.cmbRemoteBrightness.Size = New System.Drawing.Size(219, 22)
        Me.cmbRemoteBrightness.TabIndex = 4
        '
        'Label249
        '
        Me.Label249.AutoSize = true
        Me.Label249.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label249.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label249.Location = New System.Drawing.Point(358, 58)
        Me.Label249.Name = "Label249"
        Me.Label249.Size = New System.Drawing.Size(71, 14)
        Me.Label249.TabIndex = 82
        Me.Label249.Text = "Resolution :"
        '
        'Label250
        '
        Me.Label250.AutoSize = true
        Me.Label250.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label250.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label250.Location = New System.Drawing.Point(47, 31)
        Me.Label250.Name = "Label250"
        Me.Label250.Size = New System.Drawing.Size(59, 14)
        Me.Label250.TabIndex = 83
        Me.Label250.Text = "Scanner :"
        '
        'GroupBox24
        '
        Me.GroupBox24.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox24.Controls.Add(Me.RadioButton1)
        Me.GroupBox24.Controls.Add(Me.RadioButton2)
        Me.GroupBox24.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.GroupBox24.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.GroupBox24.Location = New System.Drawing.Point(24, 335)
        Me.GroupBox24.Name = "GroupBox24"
        Me.GroupBox24.Size = New System.Drawing.Size(452, 50)
        Me.GroupBox24.TabIndex = 58
        Me.GroupBox24.TabStop = false
        Me.GroupBox24.Text = "Document View"
        Me.GroupBox24.Visible = false
        '
        'RadioButton1
        '
        Me.RadioButton1.AutoSize = true
        Me.RadioButton1.BackColor = System.Drawing.Color.Transparent
        Me.RadioButton1.Checked = true
        Me.RadioButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.RadioButton1.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.RadioButton1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.RadioButton1.Location = New System.Drawing.Point(20, 21)
        Me.RadioButton1.Name = "RadioButton1"
        Me.RadioButton1.Size = New System.Drawing.Size(171, 18)
        Me.RadioButton1.TabIndex = 18
        Me.RadioButton1.TabStop = true
        Me.RadioButton1.Text = "Documents With Month"
        Me.RadioButton1.UseVisualStyleBackColor = false
        '
        'RadioButton2
        '
        Me.RadioButton2.AutoSize = true
        Me.RadioButton2.BackColor = System.Drawing.Color.Transparent
        Me.RadioButton2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.RadioButton2.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.RadioButton2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.RadioButton2.Location = New System.Drawing.Point(243, 21)
        Me.RadioButton2.Name = "RadioButton2"
        Me.RadioButton2.Size = New System.Drawing.Size(174, 18)
        Me.RadioButton2.TabIndex = 54
        Me.RadioButton2.Text = "Documents Without Month"
        Me.RadioButton2.UseVisualStyleBackColor = false
        '
        'cmbRemoteScanMode
        '
        Me.cmbRemoteScanMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbRemoteScanMode.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.cmbRemoteScanMode.FormattingEnabled = true
        Me.cmbRemoteScanMode.Location = New System.Drawing.Point(433, 27)
        Me.cmbRemoteScanMode.Name = "cmbRemoteScanMode"
        Me.cmbRemoteScanMode.Size = New System.Drawing.Size(219, 22)
        Me.cmbRemoteScanMode.TabIndex = 1
        '
        'cmbRemoteResolution
        '
        Me.cmbRemoteResolution.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbRemoteResolution.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.cmbRemoteResolution.FormattingEnabled = true
        Me.cmbRemoteResolution.Location = New System.Drawing.Point(433, 54)
        Me.cmbRemoteResolution.Name = "cmbRemoteResolution"
        Me.cmbRemoteResolution.Size = New System.Drawing.Size(219, 22)
        Me.cmbRemoteResolution.TabIndex = 3
        '
        'cmbRemoteScanner
        '
        Me.cmbRemoteScanner.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbRemoteScanner.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.cmbRemoteScanner.Location = New System.Drawing.Point(109, 27)
        Me.cmbRemoteScanner.Name = "cmbRemoteScanner"
        Me.cmbRemoteScanner.Size = New System.Drawing.Size(219, 22)
        Me.cmbRemoteScanner.TabIndex = 0
        '
        'GroupBox23
        '
        Me.GroupBox23.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox23.Controls.Add(Me.chkZipScannerSettings)
        Me.GroupBox23.Controls.Add(Me.chkEliminatePegasus)
        Me.GroupBox23.Controls.Add(Me.btnRefreshTwainScanners)
        Me.GroupBox23.Controls.Add(Me.btnRefreshScanners)
        Me.GroupBox23.Controls.Add(Me.chkEnableRemoteScanner)
        Me.GroupBox23.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox23.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.GroupBox23.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.GroupBox23.Location = New System.Drawing.Point(6, 6)
        Me.GroupBox23.Name = "GroupBox23"
        Me.GroupBox23.Size = New System.Drawing.Size(672, 73)
        Me.GroupBox23.TabIndex = 0
        Me.GroupBox23.TabStop = false
        '
        'chkZipScannerSettings
        '
        Me.chkZipScannerSettings.AutoSize = true
        Me.chkZipScannerSettings.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkZipScannerSettings.Location = New System.Drawing.Point(111, 43)
        Me.chkZipScannerSettings.Name = "chkZipScannerSettings"
        Me.chkZipScannerSettings.Size = New System.Drawing.Size(139, 18)
        Me.chkZipScannerSettings.TabIndex = 4
        Me.chkZipScannerSettings.Tag = "Zip Scanner Settings"
        Me.chkZipScannerSettings.Text = "Zip Scanner Settings"
        Me.chkZipScannerSettings.UseVisualStyleBackColor = true
        '
        'chkEliminatePegasus
        '
        Me.chkEliminatePegasus.AutoSize = true
        Me.chkEliminatePegasus.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkEliminatePegasus.Location = New System.Drawing.Point(437, 17)
        Me.chkEliminatePegasus.Name = "chkEliminatePegasus"
        Me.chkEliminatePegasus.Size = New System.Drawing.Size(92, 18)
        Me.chkEliminatePegasus.TabIndex = 2
        Me.chkEliminatePegasus.Text = "Use gloScan"
        Me.chkEliminatePegasus.UseVisualStyleBackColor = true
        '
        'btnRefreshTwainScanners
        '
        Me.btnRefreshTwainScanners.BackColor = System.Drawing.Color.Transparent
        Me.btnRefreshTwainScanners.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.btnRefreshTwainScanners.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnRefreshTwainScanners.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnRefreshTwainScanners.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnRefreshTwainScanners.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnRefreshTwainScanners.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRefreshTwainScanners.Image = CType(resources.GetObject("btnRefreshTwainScanners.Image"),System.Drawing.Image)
        Me.btnRefreshTwainScanners.Location = New System.Drawing.Point(539, 15)
        Me.btnRefreshTwainScanners.Name = "btnRefreshTwainScanners"
        Me.btnRefreshTwainScanners.Size = New System.Drawing.Size(28, 23)
        Me.btnRefreshTwainScanners.TabIndex = 3
        Me.btnRefreshTwainScanners.UseVisualStyleBackColor = false
        '
        'btnRefreshScanners
        '
        Me.btnRefreshScanners.BackColor = System.Drawing.Color.Transparent
        Me.btnRefreshScanners.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.btnRefreshScanners.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnRefreshScanners.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnRefreshScanners.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnRefreshScanners.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnRefreshScanners.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRefreshScanners.Image = CType(resources.GetObject("btnRefreshScanners.Image"),System.Drawing.Image)
        Me.btnRefreshScanners.Location = New System.Drawing.Point(256, 15)
        Me.btnRefreshScanners.Name = "btnRefreshScanners"
        Me.btnRefreshScanners.Size = New System.Drawing.Size(28, 23)
        Me.btnRefreshScanners.TabIndex = 1
        Me.btnRefreshScanners.UseVisualStyleBackColor = false
        '
        'chkEnableRemoteScanner
        '
        Me.chkEnableRemoteScanner.AutoSize = true
        Me.chkEnableRemoteScanner.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkEnableRemoteScanner.Location = New System.Drawing.Point(111, 17)
        Me.chkEnableRemoteScanner.Name = "chkEnableRemoteScanner"
        Me.chkEnableRemoteScanner.Size = New System.Drawing.Size(141, 18)
        Me.chkEnableRemoteScanner.TabIndex = 0
        Me.chkEnableRemoteScanner.Text = "Enable Local Scanner"
        Me.chkEnableRemoteScanner.UseVisualStyleBackColor = true
        '
        'Label92
        '
        Me.Label92.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label92.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label92.Location = New System.Drawing.Point(6, 642)
        Me.Label92.Name = "Label92"
        Me.Label92.Size = New System.Drawing.Size(672, 5)
        Me.Label92.TabIndex = 67
        Me.Label92.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label93
        '
        Me.Label93.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label93.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label93.Location = New System.Drawing.Point(6, 1)
        Me.Label93.Name = "Label93"
        Me.Label93.Size = New System.Drawing.Size(672, 5)
        Me.Label93.TabIndex = 64
        Me.Label93.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label94
        '
        Me.Label94.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label94.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label94.Location = New System.Drawing.Point(1, 1)
        Me.Label94.Name = "Label94"
        Me.Label94.Size = New System.Drawing.Size(5, 646)
        Me.Label94.TabIndex = 65
        Me.Label94.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label95
        '
        Me.Label95.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label95.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label95.Location = New System.Drawing.Point(678, 1)
        Me.Label95.Name = "Label95"
        Me.Label95.Size = New System.Drawing.Size(5, 646)
        Me.Label95.TabIndex = 66
        Me.Label95.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label40
        '
        Me.Label40.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label40.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label40.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label40.Location = New System.Drawing.Point(1, 647)
        Me.Label40.Name = "Label40"
        Me.Label40.Size = New System.Drawing.Size(682, 1)
        Me.Label40.TabIndex = 63
        Me.Label40.Text = "label2"
        '
        'Label41
        '
        Me.Label41.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label41.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label41.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label41.Location = New System.Drawing.Point(0, 1)
        Me.Label41.Name = "Label41"
        Me.Label41.Size = New System.Drawing.Size(1, 647)
        Me.Label41.TabIndex = 62
        Me.Label41.Text = "label4"
        '
        'Label42
        '
        Me.Label42.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label42.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label42.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label42.Location = New System.Drawing.Point(683, 1)
        Me.Label42.Name = "Label42"
        Me.Label42.Size = New System.Drawing.Size(1, 647)
        Me.Label42.TabIndex = 61
        Me.Label42.Text = "label3"
        '
        'Label43
        '
        Me.Label43.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label43.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label43.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label43.Location = New System.Drawing.Point(0, 0)
        Me.Label43.Name = "Label43"
        Me.Label43.Size = New System.Drawing.Size(684, 1)
        Me.Label43.TabIndex = 60
        Me.Label43.Text = "label1"
        '
        'tp_PatientControl
        '
        Me.tp_PatientControl.BackColor = System.Drawing.Color.FromArgb(CType(CType(207,Byte),Integer), CType(CType(224,Byte),Integer), CType(CType(248,Byte),Integer))
        Me.tp_PatientControl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tp_PatientControl.Controls.Add(Me.GroupBox25)
        Me.tp_PatientControl.Controls.Add(Me.grbExportReport)
        Me.tp_PatientControl.Controls.Add(Me.grbDefaultProvider)
        Me.tp_PatientControl.Controls.Add(Me.grbPatientBillingAlerts)
        Me.tp_PatientControl.Controls.Add(Me.GrpBoxPatienDemographics)
        Me.tp_PatientControl.Controls.Add(Me.grbPatientSearch)
        Me.tp_PatientControl.Controls.Add(Me.GroupBox6)
        Me.tp_PatientControl.Controls.Add(Me.gr_PatientSearch)
        Me.tp_PatientControl.Controls.Add(Me.Label96)
        Me.tp_PatientControl.Controls.Add(Me.Label97)
        Me.tp_PatientControl.Controls.Add(Me.Label98)
        Me.tp_PatientControl.Controls.Add(Me.Label99)
        Me.tp_PatientControl.Controls.Add(Me.Label36)
        Me.tp_PatientControl.Controls.Add(Me.Label37)
        Me.tp_PatientControl.Controls.Add(Me.Label38)
        Me.tp_PatientControl.Controls.Add(Me.Label39)
        Me.tp_PatientControl.Location = New System.Drawing.Point(4, 42)
        Me.tp_PatientControl.Name = "tp_PatientControl"
        Me.tp_PatientControl.Size = New System.Drawing.Size(684, 648)
        Me.tp_PatientControl.TabIndex = 7
        Me.tp_PatientControl.Text = "Dashboard Settings"
        '
        'GroupBox25
        '
        Me.GroupBox25.BackColor = System.Drawing.Color.FromArgb(CType(CType(207,Byte),Integer), CType(CType(224,Byte),Integer), CType(CType(248,Byte),Integer))
        Me.GroupBox25.Controls.Add(Me.chkClearDashboardSearch)
        Me.GroupBox25.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox25.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.GroupBox25.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.GroupBox25.Location = New System.Drawing.Point(6, 645)
        Me.GroupBox25.Name = "GroupBox25"
        Me.GroupBox25.Size = New System.Drawing.Size(672, 50)
        Me.GroupBox25.TabIndex = 6
        Me.GroupBox25.TabStop = false
        '
        'chkClearDashboardSearch
        '
        Me.chkClearDashboardSearch.AutoSize = true
        Me.chkClearDashboardSearch.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkClearDashboardSearch.Location = New System.Drawing.Point(29, 21)
        Me.chkClearDashboardSearch.Name = "chkClearDashboardSearch"
        Me.chkClearDashboardSearch.Size = New System.Drawing.Size(154, 18)
        Me.chkClearDashboardSearch.TabIndex = 1
        Me.chkClearDashboardSearch.Text = "Clear Dashboard Search"
        Me.chkClearDashboardSearch.UseVisualStyleBackColor = true
        '
        'grbExportReport
        '
        Me.grbExportReport.BackColor = System.Drawing.Color.FromArgb(CType(CType(207,Byte),Integer), CType(CType(224,Byte),Integer), CType(CType(248,Byte),Integer))
        Me.grbExportReport.Controls.Add(Me.btnClearReportPath)
        Me.grbExportReport.Controls.Add(Me.btnBrowseReportPath)
        Me.grbExportReport.Controls.Add(Me.txtExportReportPath)
        Me.grbExportReport.Controls.Add(Me.Label12)
        Me.grbExportReport.Controls.Add(Me.chkExportReport)
        Me.grbExportReport.Dock = System.Windows.Forms.DockStyle.Top
        Me.grbExportReport.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.grbExportReport.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.grbExportReport.Location = New System.Drawing.Point(6, 566)
        Me.grbExportReport.Name = "grbExportReport"
        Me.grbExportReport.Size = New System.Drawing.Size(672, 79)
        Me.grbExportReport.TabIndex = 5
        Me.grbExportReport.TabStop = false
        Me.grbExportReport.Text = "Export Report"
        Me.grbExportReport.Visible = false
        '
        'btnClearReportPath
        '
        Me.btnClearReportPath.BackColor = System.Drawing.Color.White
        Me.btnClearReportPath.BackgroundImage = CType(resources.GetObject("btnClearReportPath.BackgroundImage"),System.Drawing.Image)
        Me.btnClearReportPath.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClearReportPath.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.btnClearReportPath.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255,Byte),Integer), CType(CType(197,Byte),Integer), CType(CType(108,Byte),Integer))
        Me.btnClearReportPath.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255,Byte),Integer), CType(CType(224,Byte),Integer), CType(CType(160,Byte),Integer))
        Me.btnClearReportPath.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClearReportPath.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btnClearReportPath.Image = CType(resources.GetObject("btnClearReportPath.Image"),System.Drawing.Image)
        Me.btnClearReportPath.Location = New System.Drawing.Point(460, 47)
        Me.btnClearReportPath.Name = "btnClearReportPath"
        Me.btnClearReportPath.Size = New System.Drawing.Size(22, 22)
        Me.btnClearReportPath.TabIndex = 3
        Me.btnClearReportPath.UseVisualStyleBackColor = false
        '
        'btnBrowseReportPath
        '
        Me.btnBrowseReportPath.BackColor = System.Drawing.Color.White
        Me.btnBrowseReportPath.BackgroundImage = CType(resources.GetObject("btnBrowseReportPath.BackgroundImage"),System.Drawing.Image)
        Me.btnBrowseReportPath.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnBrowseReportPath.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.btnBrowseReportPath.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255,Byte),Integer), CType(CType(197,Byte),Integer), CType(CType(108,Byte),Integer))
        Me.btnBrowseReportPath.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255,Byte),Integer), CType(CType(224,Byte),Integer), CType(CType(160,Byte),Integer))
        Me.btnBrowseReportPath.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBrowseReportPath.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btnBrowseReportPath.Image = CType(resources.GetObject("btnBrowseReportPath.Image"),System.Drawing.Image)
        Me.btnBrowseReportPath.Location = New System.Drawing.Point(434, 47)
        Me.btnBrowseReportPath.Name = "btnBrowseReportPath"
        Me.btnBrowseReportPath.Size = New System.Drawing.Size(22, 22)
        Me.btnBrowseReportPath.TabIndex = 2
        Me.btnBrowseReportPath.UseVisualStyleBackColor = false
        '
        'txtExportReportPath
        '
        Me.txtExportReportPath.BackColor = System.Drawing.Color.White
        Me.txtExportReportPath.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtExportReportPath.Location = New System.Drawing.Point(113, 47)
        Me.txtExportReportPath.Name = "txtExportReportPath"
        Me.txtExportReportPath.ReadOnly = true
        Me.txtExportReportPath.Size = New System.Drawing.Size(317, 22)
        Me.txtExportReportPath.TabIndex = 4
        '
        'Label12
        '
        Me.Label12.AutoSize = true
        Me.Label12.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label12.Location = New System.Drawing.Point(26, 51)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(85, 14)
        Me.Label12.TabIndex = 2
        Me.Label12.Text = "Report Path : "
        '
        'chkExportReport
        '
        Me.chkExportReport.AutoSize = true
        Me.chkExportReport.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkExportReport.Location = New System.Drawing.Point(29, 23)
        Me.chkExportReport.Name = "chkExportReport"
        Me.chkExportReport.Size = New System.Drawing.Size(212, 18)
        Me.chkExportReport.TabIndex = 1
        Me.chkExportReport.Text = "Export Report to Default Location"
        Me.chkExportReport.UseVisualStyleBackColor = true
        '
        'grbDefaultProvider
        '
        Me.grbDefaultProvider.BackColor = System.Drawing.Color.FromArgb(CType(CType(207,Byte),Integer), CType(CType(224,Byte),Integer), CType(CType(248,Byte),Integer))
        Me.grbDefaultProvider.Controls.Add(Me.cmbDefaultProvider)
        Me.grbDefaultProvider.Controls.Add(Me.Label5)
        Me.grbDefaultProvider.Dock = System.Windows.Forms.DockStyle.Top
        Me.grbDefaultProvider.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.grbDefaultProvider.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.grbDefaultProvider.Location = New System.Drawing.Point(6, 516)
        Me.grbDefaultProvider.Name = "grbDefaultProvider"
        Me.grbDefaultProvider.Size = New System.Drawing.Size(672, 50)
        Me.grbDefaultProvider.TabIndex = 4
        Me.grbDefaultProvider.TabStop = false
        Me.grbDefaultProvider.Text = "Default Provider"
        '
        'cmbDefaultProvider
        '
        Me.cmbDefaultProvider.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDefaultProvider.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.cmbDefaultProvider.FormattingEnabled = true
        Me.cmbDefaultProvider.Location = New System.Drawing.Point(134, 20)
        Me.cmbDefaultProvider.Name = "cmbDefaultProvider"
        Me.cmbDefaultProvider.Size = New System.Drawing.Size(170, 22)
        Me.cmbDefaultProvider.TabIndex = 0
        '
        'Label5
        '
        Me.Label5.AutoSize = true
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label5.Location = New System.Drawing.Point(26, 24)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(106, 14)
        Me.Label5.TabIndex = 1
        Me.Label5.Text = "Default Provider : "
        '
        'grbPatientBillingAlerts
        '
        Me.grbPatientBillingAlerts.BackColor = System.Drawing.Color.FromArgb(CType(CType(207,Byte),Integer), CType(CType(224,Byte),Integer), CType(CType(248,Byte),Integer))
        Me.grbPatientBillingAlerts.Controls.Add(Me.btnBrowseAlertColor)
        Me.grbPatientBillingAlerts.Controls.Add(Me.txtAlertColor)
        Me.grbPatientBillingAlerts.Controls.Add(Me.Label4)
        Me.grbPatientBillingAlerts.Controls.Add(Me.chkShowBlinkingAlert)
        Me.grbPatientBillingAlerts.Dock = System.Windows.Forms.DockStyle.Top
        Me.grbPatientBillingAlerts.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.grbPatientBillingAlerts.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.grbPatientBillingAlerts.Location = New System.Drawing.Point(6, 471)
        Me.grbPatientBillingAlerts.Name = "grbPatientBillingAlerts"
        Me.grbPatientBillingAlerts.Size = New System.Drawing.Size(672, 45)
        Me.grbPatientBillingAlerts.TabIndex = 3
        Me.grbPatientBillingAlerts.TabStop = false
        Me.grbPatientBillingAlerts.Text = "Patient Billing Alerts"
        Me.grbPatientBillingAlerts.Visible = false
        '
        'btnBrowseAlertColor
        '
        Me.btnBrowseAlertColor.BackColor = System.Drawing.Color.White
        Me.btnBrowseAlertColor.BackgroundImage = CType(resources.GetObject("btnBrowseAlertColor.BackgroundImage"),System.Drawing.Image)
        Me.btnBrowseAlertColor.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnBrowseAlertColor.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.btnBrowseAlertColor.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255,Byte),Integer), CType(CType(197,Byte),Integer), CType(CType(108,Byte),Integer))
        Me.btnBrowseAlertColor.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255,Byte),Integer), CType(CType(224,Byte),Integer), CType(CType(160,Byte),Integer))
        Me.btnBrowseAlertColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBrowseAlertColor.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btnBrowseAlertColor.Image = CType(resources.GetObject("btnBrowseAlertColor.Image"),System.Drawing.Image)
        Me.btnBrowseAlertColor.Location = New System.Drawing.Point(282, 19)
        Me.btnBrowseAlertColor.Name = "btnBrowseAlertColor"
        Me.btnBrowseAlertColor.Size = New System.Drawing.Size(22, 22)
        Me.btnBrowseAlertColor.TabIndex = 1
        Me.btnBrowseAlertColor.UseVisualStyleBackColor = false
        '
        'txtAlertColor
        '
        Me.txtAlertColor.BackColor = System.Drawing.Color.White
        Me.txtAlertColor.Enabled = false
        Me.txtAlertColor.Location = New System.Drawing.Point(249, 19)
        Me.txtAlertColor.Name = "txtAlertColor"
        Me.txtAlertColor.ReadOnly = true
        Me.txtAlertColor.Size = New System.Drawing.Size(29, 22)
        Me.txtAlertColor.TabIndex = 2
        '
        'Label4
        '
        Me.Label4.AutoSize = true
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label4.Location = New System.Drawing.Point(170, 23)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(76, 14)
        Me.Label4.TabIndex = 1
        Me.Label4.Text = "Alert Color : "
        '
        'chkShowBlinkingAlert
        '
        Me.chkShowBlinkingAlert.AutoSize = true
        Me.chkShowBlinkingAlert.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkShowBlinkingAlert.Location = New System.Drawing.Point(29, 21)
        Me.chkShowBlinkingAlert.Name = "chkShowBlinkingAlert"
        Me.chkShowBlinkingAlert.Size = New System.Drawing.Size(131, 18)
        Me.chkShowBlinkingAlert.TabIndex = 0
        Me.chkShowBlinkingAlert.Text = "Show Blinking Alert"
        Me.chkShowBlinkingAlert.UseVisualStyleBackColor = true
        '
        'GrpBoxPatienDemographics
        '
        Me.GrpBoxPatienDemographics.BackColor = System.Drawing.Color.FromArgb(CType(CType(207,Byte),Integer), CType(CType(224,Byte),Integer), CType(CType(248,Byte),Integer))
        Me.GrpBoxPatienDemographics.Controls.Add(Me.trvDemographics)
        Me.GrpBoxPatienDemographics.Dock = System.Windows.Forms.DockStyle.Top
        Me.GrpBoxPatienDemographics.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.GrpBoxPatienDemographics.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.GrpBoxPatienDemographics.Location = New System.Drawing.Point(6, 316)
        Me.GrpBoxPatienDemographics.Name = "GrpBoxPatienDemographics"
        Me.GrpBoxPatienDemographics.Padding = New System.Windows.Forms.Padding(10, 5, 10, 10)
        Me.GrpBoxPatienDemographics.Size = New System.Drawing.Size(672, 155)
        Me.GrpBoxPatienDemographics.TabIndex = 2
        Me.GrpBoxPatienDemographics.TabStop = false
        Me.GrpBoxPatienDemographics.Text = "Patient Demographics"
        '
        'trvDemographics
        '
        Me.trvDemographics.CheckBoxes = true
        Me.trvDemographics.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trvDemographics.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.trvDemographics.ForeColor = System.Drawing.SystemColors.WindowText
        Me.trvDemographics.Location = New System.Drawing.Point(10, 20)
        Me.trvDemographics.Name = "trvDemographics"
        Me.trvDemographics.Size = New System.Drawing.Size(652, 125)
        Me.trvDemographics.TabIndex = 0
        '
        'grbPatientSearch
        '
        Me.grbPatientSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(207,Byte),Integer), CType(CType(224,Byte),Integer), CType(CType(248,Byte),Integer))
        Me.grbPatientSearch.Controls.Add(Me.trvPatientSearch)
        Me.grbPatientSearch.Dock = System.Windows.Forms.DockStyle.Top
        Me.grbPatientSearch.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.grbPatientSearch.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.grbPatientSearch.Location = New System.Drawing.Point(6, 161)
        Me.grbPatientSearch.Name = "grbPatientSearch"
        Me.grbPatientSearch.Padding = New System.Windows.Forms.Padding(10, 5, 10, 10)
        Me.grbPatientSearch.Size = New System.Drawing.Size(672, 155)
        Me.grbPatientSearch.TabIndex = 1
        Me.grbPatientSearch.TabStop = false
        Me.grbPatientSearch.Text = "Patient Search"
        '
        'trvPatientSearch
        '
        Me.trvPatientSearch.CheckBoxes = true
        Me.trvPatientSearch.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trvPatientSearch.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.trvPatientSearch.ForeColor = System.Drawing.SystemColors.WindowText
        Me.trvPatientSearch.Location = New System.Drawing.Point(10, 20)
        Me.trvPatientSearch.Name = "trvPatientSearch"
        Me.trvPatientSearch.Size = New System.Drawing.Size(652, 125)
        Me.trvPatientSearch.TabIndex = 0
        '
        'GroupBox6
        '
        Me.GroupBox6.BackColor = System.Drawing.Color.FromArgb(CType(CType(207,Byte),Integer), CType(CType(224,Byte),Integer), CType(CType(248,Byte),Integer))
        Me.GroupBox6.Controls.Add(Me.trvPatientColumns)
        Me.GroupBox6.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox6.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.GroupBox6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.GroupBox6.Location = New System.Drawing.Point(6, 6)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Padding = New System.Windows.Forms.Padding(10, 5, 10, 10)
        Me.GroupBox6.Size = New System.Drawing.Size(672, 155)
        Me.GroupBox6.TabIndex = 0
        Me.GroupBox6.TabStop = false
        Me.GroupBox6.Text = "Patient Data"
        '
        'trvPatientColumns
        '
        Me.trvPatientColumns.CheckBoxes = true
        Me.trvPatientColumns.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trvPatientColumns.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.trvPatientColumns.ForeColor = System.Drawing.SystemColors.WindowText
        Me.trvPatientColumns.Location = New System.Drawing.Point(10, 20)
        Me.trvPatientColumns.Name = "trvPatientColumns"
        Me.trvPatientColumns.Size = New System.Drawing.Size(652, 125)
        Me.trvPatientColumns.TabIndex = 0
        '
        'gr_PatientSearch
        '
        Me.gr_PatientSearch.BackColor = System.Drawing.Color.Transparent
        Me.gr_PatientSearch.Controls.Add(Me.cmbSearchColumns)
        Me.gr_PatientSearch.Controls.Add(Me.Label33)
        Me.gr_PatientSearch.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.gr_PatientSearch.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.gr_PatientSearch.Location = New System.Drawing.Point(6, 6)
        Me.gr_PatientSearch.Name = "gr_PatientSearch"
        Me.gr_PatientSearch.Size = New System.Drawing.Size(670, 57)
        Me.gr_PatientSearch.TabIndex = 57
        Me.gr_PatientSearch.TabStop = false
        Me.gr_PatientSearch.Text = "Patient Search"
        Me.gr_PatientSearch.Visible = false
        '
        'cmbSearchColumns
        '
        Me.cmbSearchColumns.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSearchColumns.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.cmbSearchColumns.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.cmbSearchColumns.FormattingEnabled = true
        Me.cmbSearchColumns.Items.AddRange(New Object() {"First Name", "Last Name", "Code", "SSN "})
        Me.cmbSearchColumns.Location = New System.Drawing.Point(154, 21)
        Me.cmbSearchColumns.Name = "cmbSearchColumns"
        Me.cmbSearchColumns.Size = New System.Drawing.Size(215, 22)
        Me.cmbSearchColumns.TabIndex = 0
        '
        'Label33
        '
        Me.Label33.AutoSize = true
        Me.Label33.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label33.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label33.Location = New System.Drawing.Point(11, 24)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(139, 14)
        Me.Label33.TabIndex = 0
        Me.Label33.Text = "Default Search Column :"
        '
        'Label96
        '
        Me.Label96.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label96.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label96.Location = New System.Drawing.Point(6, 642)
        Me.Label96.Name = "Label96"
        Me.Label96.Size = New System.Drawing.Size(672, 5)
        Me.Label96.TabIndex = 66
        Me.Label96.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label97
        '
        Me.Label97.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label97.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label97.Location = New System.Drawing.Point(6, 1)
        Me.Label97.Name = "Label97"
        Me.Label97.Size = New System.Drawing.Size(672, 5)
        Me.Label97.TabIndex = 63
        Me.Label97.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label98
        '
        Me.Label98.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label98.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label98.Location = New System.Drawing.Point(1, 1)
        Me.Label98.Name = "Label98"
        Me.Label98.Size = New System.Drawing.Size(5, 646)
        Me.Label98.TabIndex = 64
        Me.Label98.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label99
        '
        Me.Label99.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label99.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label99.Location = New System.Drawing.Point(678, 1)
        Me.Label99.Name = "Label99"
        Me.Label99.Size = New System.Drawing.Size(5, 646)
        Me.Label99.TabIndex = 65
        Me.Label99.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label36
        '
        Me.Label36.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label36.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label36.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label36.Location = New System.Drawing.Point(1, 647)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(682, 1)
        Me.Label36.TabIndex = 62
        Me.Label36.Text = "label2"
        '
        'Label37
        '
        Me.Label37.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label37.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label37.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label37.Location = New System.Drawing.Point(0, 1)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(1, 647)
        Me.Label37.TabIndex = 61
        Me.Label37.Text = "label4"
        '
        'Label38
        '
        Me.Label38.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label38.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label38.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label38.Location = New System.Drawing.Point(683, 1)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(1, 647)
        Me.Label38.TabIndex = 60
        Me.Label38.Text = "label3"
        '
        'Label39
        '
        Me.Label39.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label39.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label39.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label39.Location = New System.Drawing.Point(0, 0)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(684, 1)
        Me.Label39.TabIndex = 59
        Me.Label39.Text = "label1"
        '
        'tp_ExchangeServer
        '
        Me.tp_ExchangeServer.BackColor = System.Drawing.Color.FromArgb(CType(CType(207,Byte),Integer), CType(CType(224,Byte),Integer), CType(CType(248,Byte),Integer))
        Me.tp_ExchangeServer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tp_ExchangeServer.Controls.Add(Me.gr_ExchangeServerSettings)
        Me.tp_ExchangeServer.Controls.Add(Me.Label100)
        Me.tp_ExchangeServer.Controls.Add(Me.Label101)
        Me.tp_ExchangeServer.Controls.Add(Me.Label102)
        Me.tp_ExchangeServer.Controls.Add(Me.Label103)
        Me.tp_ExchangeServer.Controls.Add(Me.lbl_BottomBrd)
        Me.tp_ExchangeServer.Controls.Add(Me.lbl_LeftBrd)
        Me.tp_ExchangeServer.Controls.Add(Me.lbl_RightBrd)
        Me.tp_ExchangeServer.Controls.Add(Me.lbl_TopBrd)
        Me.tp_ExchangeServer.Location = New System.Drawing.Point(4, 42)
        Me.tp_ExchangeServer.Name = "tp_ExchangeServer"
        Me.tp_ExchangeServer.Size = New System.Drawing.Size(684, 648)
        Me.tp_ExchangeServer.TabIndex = 8
        Me.tp_ExchangeServer.Text = "Exchange Server"
        '
        'gr_ExchangeServerSettings
        '
        Me.gr_ExchangeServerSettings.BackColor = System.Drawing.Color.FromArgb(CType(CType(207,Byte),Integer), CType(CType(224,Byte),Integer), CType(CType(248,Byte),Integer))
        Me.gr_ExchangeServerSettings.Controls.Add(Me.txtExchangeDomain)
        Me.gr_ExchangeServerSettings.Controls.Add(Me.Label34)
        Me.gr_ExchangeServerSettings.Controls.Add(Me.Label35)
        Me.gr_ExchangeServerSettings.Controls.Add(Me.txtExchangeURL)
        Me.gr_ExchangeServerSettings.Dock = System.Windows.Forms.DockStyle.Top
        Me.gr_ExchangeServerSettings.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.gr_ExchangeServerSettings.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.gr_ExchangeServerSettings.Location = New System.Drawing.Point(6, 6)
        Me.gr_ExchangeServerSettings.Name = "gr_ExchangeServerSettings"
        Me.gr_ExchangeServerSettings.Size = New System.Drawing.Size(672, 85)
        Me.gr_ExchangeServerSettings.TabIndex = 58
        Me.gr_ExchangeServerSettings.TabStop = false
        Me.gr_ExchangeServerSettings.Text = "Exchange Server Settings"
        '
        'txtExchangeDomain
        '
        Me.txtExchangeDomain.BackColor = System.Drawing.Color.White
        Me.txtExchangeDomain.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtExchangeDomain.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.txtExchangeDomain.Location = New System.Drawing.Point(134, 23)
        Me.txtExchangeDomain.Margin = New System.Windows.Forms.Padding(2)
        Me.txtExchangeDomain.Name = "txtExchangeDomain"
        Me.txtExchangeDomain.Size = New System.Drawing.Size(277, 22)
        Me.txtExchangeDomain.TabIndex = 0
        '
        'Label34
        '
        Me.Label34.AutoSize = true
        Me.Label34.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label34.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label34.Location = New System.Drawing.Point(38, 55)
        Me.Label34.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(93, 14)
        Me.Label34.TabIndex = 72
        Me.Label34.Text = "Exchange URL :"
        '
        'Label35
        '
        Me.Label35.AutoSize = true
        Me.Label35.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label35.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label35.Location = New System.Drawing.Point(19, 27)
        Me.Label35.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(112, 14)
        Me.Label35.TabIndex = 74
        Me.Label35.Text = "Exchange Domain :"
        '
        'txtExchangeURL
        '
        Me.txtExchangeURL.BackColor = System.Drawing.Color.White
        Me.txtExchangeURL.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtExchangeURL.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.txtExchangeURL.Location = New System.Drawing.Point(134, 51)
        Me.txtExchangeURL.Margin = New System.Windows.Forms.Padding(2)
        Me.txtExchangeURL.Name = "txtExchangeURL"
        Me.txtExchangeURL.Size = New System.Drawing.Size(277, 22)
        Me.txtExchangeURL.TabIndex = 1
        '
        'Label100
        '
        Me.Label100.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label100.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label100.Location = New System.Drawing.Point(6, 642)
        Me.Label100.Name = "Label100"
        Me.Label100.Size = New System.Drawing.Size(672, 5)
        Me.Label100.TabIndex = 66
        Me.Label100.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label101
        '
        Me.Label101.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label101.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label101.Location = New System.Drawing.Point(6, 1)
        Me.Label101.Name = "Label101"
        Me.Label101.Size = New System.Drawing.Size(672, 5)
        Me.Label101.TabIndex = 63
        Me.Label101.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label102
        '
        Me.Label102.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label102.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label102.Location = New System.Drawing.Point(1, 1)
        Me.Label102.Name = "Label102"
        Me.Label102.Size = New System.Drawing.Size(5, 646)
        Me.Label102.TabIndex = 64
        Me.Label102.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label103
        '
        Me.Label103.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label103.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label103.Location = New System.Drawing.Point(678, 1)
        Me.Label103.Name = "Label103"
        Me.Label103.Size = New System.Drawing.Size(5, 646)
        Me.Label103.TabIndex = 65
        Me.Label103.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lbl_BottomBrd
        '
        Me.lbl_BottomBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.lbl_BottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_BottomBrd.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.lbl_BottomBrd.Location = New System.Drawing.Point(1, 647)
        Me.lbl_BottomBrd.Name = "lbl_BottomBrd"
        Me.lbl_BottomBrd.Size = New System.Drawing.Size(682, 1)
        Me.lbl_BottomBrd.TabIndex = 62
        Me.lbl_BottomBrd.Text = "label2"
        '
        'lbl_LeftBrd
        '
        Me.lbl_LeftBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.lbl_LeftBrd.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_LeftBrd.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lbl_LeftBrd.Location = New System.Drawing.Point(0, 1)
        Me.lbl_LeftBrd.Name = "lbl_LeftBrd"
        Me.lbl_LeftBrd.Size = New System.Drawing.Size(1, 647)
        Me.lbl_LeftBrd.TabIndex = 61
        Me.lbl_LeftBrd.Text = "label4"
        '
        'lbl_RightBrd
        '
        Me.lbl_RightBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.lbl_RightBrd.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_RightBrd.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.lbl_RightBrd.Location = New System.Drawing.Point(683, 1)
        Me.lbl_RightBrd.Name = "lbl_RightBrd"
        Me.lbl_RightBrd.Size = New System.Drawing.Size(1, 647)
        Me.lbl_RightBrd.TabIndex = 60
        Me.lbl_RightBrd.Text = "label3"
        '
        'lbl_TopBrd
        '
        Me.lbl_TopBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.lbl_TopBrd.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_TopBrd.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lbl_TopBrd.Location = New System.Drawing.Point(0, 0)
        Me.lbl_TopBrd.Name = "lbl_TopBrd"
        Me.lbl_TopBrd.Size = New System.Drawing.Size(684, 1)
        Me.lbl_TopBrd.TabIndex = 59
        Me.lbl_TopBrd.Text = "label1"
        '
        'tp_Appointment
        '
        Me.tp_Appointment.BackColor = System.Drawing.Color.FromArgb(CType(CType(207,Byte),Integer), CType(CType(224,Byte),Integer), CType(CType(248,Byte),Integer))
        Me.tp_Appointment.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tp_Appointment.Controls.Add(Me.grb_CheckedoutAppointment)
        Me.tp_Appointment.Controls.Add(Me.Label114)
        Me.tp_Appointment.Controls.Add(Me.grbFollowup)
        Me.tp_Appointment.Controls.Add(Me.grbAppointments)
        Me.tp_Appointment.Controls.Add(Me.Label111)
        Me.tp_Appointment.Controls.Add(Me.Label112)
        Me.tp_Appointment.Controls.Add(Me.Label113)
        Me.tp_Appointment.Location = New System.Drawing.Point(4, 42)
        Me.tp_Appointment.Name = "tp_Appointment"
        Me.tp_Appointment.Size = New System.Drawing.Size(684, 648)
        Me.tp_Appointment.TabIndex = 10
        Me.tp_Appointment.Text = "Appointment"
        '
        'grb_CheckedoutAppointment
        '
        Me.grb_CheckedoutAppointment.Controls.Add(Me.Label3)
        Me.grb_CheckedoutAppointment.Controls.Add(Me.chkCheckedoutAppointments)
        Me.grb_CheckedoutAppointment.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.grb_CheckedoutAppointment.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.grb_CheckedoutAppointment.Location = New System.Drawing.Point(8, 159)
        Me.grb_CheckedoutAppointment.Name = "grb_CheckedoutAppointment"
        Me.grb_CheckedoutAppointment.Size = New System.Drawing.Size(666, 60)
        Me.grb_CheckedoutAppointment.TabIndex = 67
        Me.grb_CheckedoutAppointment.TabStop = false
        Me.grb_CheckedoutAppointment.Text = "Checked out Appointments"
        '
        'Label3
        '
        Me.Label3.AutoSize = true
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label3.Location = New System.Drawing.Point(17, 30)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(201, 14)
        Me.Label3.TabIndex = 25
        Me.Label3.Text = "Show Checked out Appointments :"
        '
        'chkCheckedoutAppointments
        '
        Me.chkCheckedoutAppointments.AutoSize = true
        Me.chkCheckedoutAppointments.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkCheckedoutAppointments.Location = New System.Drawing.Point(218, 31)
        Me.chkCheckedoutAppointments.Name = "chkCheckedoutAppointments"
        Me.chkCheckedoutAppointments.Size = New System.Drawing.Size(15, 14)
        Me.chkCheckedoutAppointments.TabIndex = 24
        Me.chkCheckedoutAppointments.UseVisualStyleBackColor = true
        '
        'Label114
        '
        Me.Label114.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label114.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label114.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label114.Location = New System.Drawing.Point(1, 647)
        Me.Label114.Name = "Label114"
        Me.Label114.Size = New System.Drawing.Size(682, 1)
        Me.Label114.TabIndex = 65
        Me.Label114.Text = "label1"
        '
        'grbFollowup
        '
        Me.grbFollowup.Controls.Add(Me.rbFollowupFromToday)
        Me.grbFollowup.Controls.Add(Me.rbFolloupFromDate)
        Me.grbFollowup.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.grbFollowup.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.grbFollowup.Location = New System.Drawing.Point(8, 107)
        Me.grbFollowup.Name = "grbFollowup"
        Me.grbFollowup.Size = New System.Drawing.Size(667, 51)
        Me.grbFollowup.TabIndex = 0
        Me.grbFollowup.TabStop = false
        Me.grbFollowup.Text = "Followup"
        '
        'rbFollowupFromToday
        '
        Me.rbFollowupFromToday.AutoSize = true
        Me.rbFollowupFromToday.Checked = true
        Me.rbFollowupFromToday.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.rbFollowupFromToday.Location = New System.Drawing.Point(260, 22)
        Me.rbFollowupFromToday.Name = "rbFollowupFromToday"
        Me.rbFollowupFromToday.Size = New System.Drawing.Size(131, 18)
        Me.rbFollowupFromToday.TabIndex = 7
        Me.rbFollowupFromToday.TabStop = true
        Me.rbFollowupFromToday.Text = "Start from Today"
        Me.rbFollowupFromToday.UseVisualStyleBackColor = true
        '
        'rbFolloupFromDate
        '
        Me.rbFolloupFromDate.AutoSize = true
        Me.rbFolloupFromDate.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.rbFolloupFromDate.Location = New System.Drawing.Point(74, 22)
        Me.rbFolloupFromDate.Name = "rbFolloupFromDate"
        Me.rbFolloupFromDate.Size = New System.Drawing.Size(167, 18)
        Me.rbFolloupFromDate.TabIndex = 6
        Me.rbFolloupFromDate.Text = "Start from Followup Date "
        Me.rbFolloupFromDate.UseVisualStyleBackColor = true
        '
        'grbAppointments
        '
        Me.grbAppointments.Controls.Add(Me.Label181)
        Me.grbAppointments.Controls.Add(Me.num_NoofColOnCalndr)
        Me.grbAppointments.Controls.Add(Me.lblCalCol)
        Me.grbAppointments.Controls.Add(Me.chkShowTemplate)
        Me.grbAppointments.Controls.Add(Me.num_NoofApptInaSlot)
        Me.grbAppointments.Controls.Add(Me.Label13)
        Me.grbAppointments.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.grbAppointments.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.grbAppointments.Location = New System.Drawing.Point(6, 6)
        Me.grbAppointments.Name = "grbAppointments"
        Me.grbAppointments.Size = New System.Drawing.Size(666, 87)
        Me.grbAppointments.TabIndex = 0
        Me.grbAppointments.TabStop = false
        Me.grbAppointments.Text = "Appointments"
        '
        'Label181
        '
        Me.Label181.AutoSize = true
        Me.Label181.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label181.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label181.Location = New System.Drawing.Point(209, 56)
        Me.Label181.Name = "Label181"
        Me.Label181.Size = New System.Drawing.Size(101, 14)
        Me.Label181.TabIndex = 13
        Me.Label181.Text = "Calendar columns"
        '
        'num_NoofColOnCalndr
        '
        Me.num_NoofColOnCalndr.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.num_NoofColOnCalndr.ForeColor = System.Drawing.Color.Black
        Me.num_NoofColOnCalndr.Location = New System.Drawing.Point(160, 54)
        Me.num_NoofColOnCalndr.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.num_NoofColOnCalndr.Name = "num_NoofColOnCalndr"
        Me.num_NoofColOnCalndr.Size = New System.Drawing.Size(45, 22)
        Me.num_NoofColOnCalndr.TabIndex = 12
        Me.num_NoofColOnCalndr.Value = New Decimal(New Integer() {3, 0, 0, 0})
        '
        'lblCalCol
        '
        Me.lblCalCol.AutoSize = true
        Me.lblCalCol.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblCalCol.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.lblCalCol.Location = New System.Drawing.Point(53, 56)
        Me.lblCalCol.Name = "lblCalCol"
        Me.lblCalCol.Size = New System.Drawing.Size(104, 14)
        Me.lblCalCol.TabIndex = 11
        Me.lblCalCol.Text = "My screen can fit "
        '
        'chkShowTemplate
        '
        Me.chkShowTemplate.AutoSize = true
        Me.chkShowTemplate.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkShowTemplate.Location = New System.Drawing.Point(295, 24)
        Me.chkShowTemplate.Name = "chkShowTemplate"
        Me.chkShowTemplate.Size = New System.Drawing.Size(113, 18)
        Me.chkShowTemplate.TabIndex = 4
        Me.chkShowTemplate.Text = "Show Template"
        Me.chkShowTemplate.UseVisualStyleBackColor = true
        '
        'num_NoofApptInaSlot
        '
        Me.num_NoofApptInaSlot.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.num_NoofApptInaSlot.ForeColor = System.Drawing.Color.Black
        Me.num_NoofApptInaSlot.Location = New System.Drawing.Point(245, 21)
        Me.num_NoofApptInaSlot.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.num_NoofApptInaSlot.Name = "num_NoofApptInaSlot"
        Me.num_NoofApptInaSlot.Size = New System.Drawing.Size(45, 22)
        Me.num_NoofApptInaSlot.TabIndex = 3
        Me.num_NoofApptInaSlot.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'Label13
        '
        Me.Label13.AutoSize = true
        Me.Label13.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label13.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label13.Location = New System.Drawing.Point(53, 25)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(182, 14)
        Me.Label13.TabIndex = 2
        Me.Label13.Text = "No. of appointment's per hour :"
        '
        'Label111
        '
        Me.Label111.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label111.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label111.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label111.Location = New System.Drawing.Point(0, 1)
        Me.Label111.Name = "Label111"
        Me.Label111.Size = New System.Drawing.Size(1, 647)
        Me.Label111.TabIndex = 62
        Me.Label111.Text = "label4"
        '
        'Label112
        '
        Me.Label112.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label112.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label112.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label112.Location = New System.Drawing.Point(683, 1)
        Me.Label112.Name = "Label112"
        Me.Label112.Size = New System.Drawing.Size(1, 647)
        Me.Label112.TabIndex = 63
        Me.Label112.Text = "label4"
        '
        'Label113
        '
        Me.Label113.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label113.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label113.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label113.Location = New System.Drawing.Point(0, 0)
        Me.Label113.Name = "Label113"
        Me.Label113.Size = New System.Drawing.Size(684, 1)
        Me.Label113.TabIndex = 64
        Me.Label113.Text = "label1"
        '
        'tb_SmartSettings
        '
        Me.tb_SmartSettings.BackColor = System.Drawing.Color.FromArgb(CType(CType(207,Byte),Integer), CType(CType(224,Byte),Integer), CType(CType(248,Byte),Integer))
        Me.tb_SmartSettings.Controls.Add(Me.PnlCustomTask)
        Me.tb_SmartSettings.Controls.Add(Me.Panel10)
        Me.tb_SmartSettings.Controls.Add(Me.Splitter1)
        Me.tb_SmartSettings.Controls.Add(Me.Panel26)
        Me.tb_SmartSettings.Controls.Add(Me.Panel11)
        Me.tb_SmartSettings.Location = New System.Drawing.Point(4, 42)
        Me.tb_SmartSettings.Name = "tb_SmartSettings"
        Me.tb_SmartSettings.Size = New System.Drawing.Size(684, 648)
        Me.tb_SmartSettings.TabIndex = 11
        Me.tb_SmartSettings.Text = "Smart Settings"
        Me.tb_SmartSettings.UseVisualStyleBackColor = true
        '
        'PnlCustomTask
        '
        Me.PnlCustomTask.Location = New System.Drawing.Point(316, 106)
        Me.PnlCustomTask.Name = "PnlCustomTask"
        Me.PnlCustomTask.Size = New System.Drawing.Size(249, 117)
        Me.PnlCustomTask.TabIndex = 73
        Me.PnlCustomTask.Visible = false
        '
        'Panel10
        '
        Me.Panel10.Controls.Add(Me.Panel18)
        Me.Panel10.Controls.Add(Me.Panel20)
        Me.Panel10.Controls.Add(Me.Panel14)
        Me.Panel10.Controls.Add(Me.Panel9)
        Me.Panel10.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel10.Location = New System.Drawing.Point(0, 0)
        Me.Panel10.Name = "Panel10"
        Me.Panel10.Size = New System.Drawing.Size(230, 648)
        Me.Panel10.TabIndex = 91
        '
        'Panel18
        '
        Me.Panel18.BackColor = System.Drawing.Color.White
        Me.Panel18.Controls.Add(Me.chklst_SmartOrders)
        Me.Panel18.Controls.Add(Me.Label182)
        Me.Panel18.Controls.Add(Me.Label176)
        Me.Panel18.Controls.Add(Me.Label146)
        Me.Panel18.Controls.Add(Me.Label147)
        Me.Panel18.Controls.Add(Me.Panel19)
        Me.Panel18.Controls.Add(Me.Label150)
        Me.Panel18.Controls.Add(Me.Label151)
        Me.Panel18.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel18.Location = New System.Drawing.Point(0, 364)
        Me.Panel18.Name = "Panel18"
        Me.Panel18.Size = New System.Drawing.Size(230, 284)
        Me.Panel18.TabIndex = 82
        '
        'chklst_SmartOrders
        '
        Me.chklst_SmartOrders.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.chklst_SmartOrders.CheckOnClick = true
        Me.chklst_SmartOrders.Dock = System.Windows.Forms.DockStyle.Fill
        Me.chklst_SmartOrders.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chklst_SmartOrders.FormattingEnabled = true
        Me.chklst_SmartOrders.Items.AddRange(New Object() {"Lab Orders", "Order Templates", "Referral Letter", "Drugs", "Flowsheet"})
        Me.chklst_SmartOrders.Location = New System.Drawing.Point(5, 29)
        Me.chklst_SmartOrders.Name = "chklst_SmartOrders"
        Me.chklst_SmartOrders.Size = New System.Drawing.Size(224, 254)
        Me.chklst_SmartOrders.TabIndex = 0
        '
        'Label182
        '
        Me.Label182.BackColor = System.Drawing.Color.White
        Me.Label182.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label182.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label182.Location = New System.Drawing.Point(1, 29)
        Me.Label182.Name = "Label182"
        Me.Label182.Size = New System.Drawing.Size(4, 254)
        Me.Label182.TabIndex = 71
        '
        'Label176
        '
        Me.Label176.BackColor = System.Drawing.Color.White
        Me.Label176.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label176.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label176.Location = New System.Drawing.Point(1, 25)
        Me.Label176.Name = "Label176"
        Me.Label176.Size = New System.Drawing.Size(228, 4)
        Me.Label176.TabIndex = 70
        '
        'Label146
        '
        Me.Label146.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label146.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label146.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label146.Location = New System.Drawing.Point(1, 24)
        Me.Label146.Name = "Label146"
        Me.Label146.Size = New System.Drawing.Size(228, 1)
        Me.Label146.TabIndex = 64
        Me.Label146.Text = "label2"
        '
        'Label147
        '
        Me.Label147.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label147.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label147.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label147.Location = New System.Drawing.Point(1, 283)
        Me.Label147.Name = "Label147"
        Me.Label147.Size = New System.Drawing.Size(228, 1)
        Me.Label147.TabIndex = 63
        Me.Label147.Text = "label2"
        '
        'Panel19
        '
        Me.Panel19.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.Panel19.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel19.Controls.Add(Me.chk_SmartOrder)
        Me.Panel19.Controls.Add(Me.Button13)
        Me.Panel19.Controls.Add(Me.Label184)
        Me.Panel19.Controls.Add(Me.btnClearAllOrder)
        Me.Panel19.Controls.Add(Me.Label183)
        Me.Panel19.Controls.Add(Me.btn_UpOrders)
        Me.Panel19.Controls.Add(Me.Label167)
        Me.Panel19.Controls.Add(Me.btn_DownOrders)
        Me.Panel19.Controls.Add(Me.Label148)
        Me.Panel19.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel19.Location = New System.Drawing.Point(1, 0)
        Me.Panel19.Name = "Panel19"
        Me.Panel19.Size = New System.Drawing.Size(228, 24)
        Me.Panel19.TabIndex = 0
        '
        'chk_SmartOrder
        '
        Me.chk_SmartOrder.AutoSize = true
        Me.chk_SmartOrder.Location = New System.Drawing.Point(101, 7)
        Me.chk_SmartOrder.Name = "chk_SmartOrder"
        Me.chk_SmartOrder.Size = New System.Drawing.Size(15, 14)
        Me.chk_SmartOrder.TabIndex = 74
        Me.chk_SmartOrder.UseVisualStyleBackColor = true
        Me.chk_SmartOrder.Visible = false
        '
        'Button13
        '
        Me.Button13.BackColor = System.Drawing.Color.Transparent
        Me.Button13.Dock = System.Windows.Forms.DockStyle.Right
        Me.Button13.FlatAppearance.BorderSize = 0
        Me.Button13.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.Button13.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.Button13.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button13.Image = CType(resources.GetObject("Button13.Image"),System.Drawing.Image)
        Me.Button13.Location = New System.Drawing.Point(128, 0)
        Me.Button13.Name = "Button13"
        Me.Button13.Size = New System.Drawing.Size(22, 24)
        Me.Button13.TabIndex = 3
        Me.Button13.Tag = "SelectAll"
        Me.Button13.UseVisualStyleBackColor = false
        '
        'Label184
        '
        Me.Label184.BackColor = System.Drawing.Color.Transparent
        Me.Label184.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label184.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label184.Location = New System.Drawing.Point(150, 0)
        Me.Label184.Name = "Label184"
        Me.Label184.Size = New System.Drawing.Size(4, 24)
        Me.Label184.TabIndex = 73
        Me.Label184.Text = "label2"
        '
        'btnClearAllOrder
        '
        Me.btnClearAllOrder.BackColor = System.Drawing.Color.Transparent
        Me.btnClearAllOrder.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnClearAllOrder.FlatAppearance.BorderSize = 0
        Me.btnClearAllOrder.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnClearAllOrder.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnClearAllOrder.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClearAllOrder.Image = CType(resources.GetObject("btnClearAllOrder.Image"),System.Drawing.Image)
        Me.btnClearAllOrder.Location = New System.Drawing.Point(154, 0)
        Me.btnClearAllOrder.Name = "btnClearAllOrder"
        Me.btnClearAllOrder.Size = New System.Drawing.Size(22, 24)
        Me.btnClearAllOrder.TabIndex = 4
        Me.btnClearAllOrder.Tag = "ClearAll"
        Me.btnClearAllOrder.UseVisualStyleBackColor = false
        '
        'Label183
        '
        Me.Label183.BackColor = System.Drawing.Color.Transparent
        Me.Label183.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label183.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label183.Location = New System.Drawing.Point(176, 0)
        Me.Label183.Name = "Label183"
        Me.Label183.Size = New System.Drawing.Size(4, 24)
        Me.Label183.TabIndex = 72
        Me.Label183.Text = "label2"
        '
        'btn_UpOrders
        '
        Me.btn_UpOrders.BackgroundImage = CType(resources.GetObject("btn_UpOrders.BackgroundImage"),System.Drawing.Image)
        Me.btn_UpOrders.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btn_UpOrders.Dock = System.Windows.Forms.DockStyle.Right
        Me.btn_UpOrders.FlatAppearance.BorderSize = 0
        Me.btn_UpOrders.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_UpOrders.Image = CType(resources.GetObject("btn_UpOrders.Image"),System.Drawing.Image)
        Me.btn_UpOrders.Location = New System.Drawing.Point(180, 0)
        Me.btn_UpOrders.Name = "btn_UpOrders"
        Me.btn_UpOrders.Size = New System.Drawing.Size(22, 24)
        Me.btn_UpOrders.TabIndex = 1
        Me.btn_UpOrders.UseVisualStyleBackColor = true
        '
        'Label167
        '
        Me.Label167.BackColor = System.Drawing.Color.Transparent
        Me.Label167.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label167.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label167.Location = New System.Drawing.Point(202, 0)
        Me.Label167.Name = "Label167"
        Me.Label167.Size = New System.Drawing.Size(4, 24)
        Me.Label167.TabIndex = 71
        Me.Label167.Text = "label2"
        '
        'btn_DownOrders
        '
        Me.btn_DownOrders.BackgroundImage = CType(resources.GetObject("btn_DownOrders.BackgroundImage"),System.Drawing.Image)
        Me.btn_DownOrders.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btn_DownOrders.Dock = System.Windows.Forms.DockStyle.Right
        Me.btn_DownOrders.FlatAppearance.BorderSize = 0
        Me.btn_DownOrders.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_DownOrders.Image = CType(resources.GetObject("btn_DownOrders.Image"),System.Drawing.Image)
        Me.btn_DownOrders.Location = New System.Drawing.Point(206, 0)
        Me.btn_DownOrders.Name = "btn_DownOrders"
        Me.btn_DownOrders.Size = New System.Drawing.Size(22, 24)
        Me.btn_DownOrders.TabIndex = 2
        Me.btn_DownOrders.UseVisualStyleBackColor = true
        '
        'Label148
        '
        Me.Label148.BackColor = System.Drawing.Color.Transparent
        Me.Label148.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label148.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label148.Location = New System.Drawing.Point(0, 0)
        Me.Label148.Name = "Label148"
        Me.Label148.Size = New System.Drawing.Size(228, 24)
        Me.Label148.TabIndex = 67
        Me.Label148.Text = "  Orders"
        Me.Label148.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label150
        '
        Me.Label150.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label150.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label150.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label150.Location = New System.Drawing.Point(0, 0)
        Me.Label150.Name = "Label150"
        Me.Label150.Size = New System.Drawing.Size(1, 284)
        Me.Label150.TabIndex = 66
        Me.Label150.Text = "label2"
        '
        'Label151
        '
        Me.Label151.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label151.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label151.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label151.Location = New System.Drawing.Point(229, 0)
        Me.Label151.Name = "Label151"
        Me.Label151.Size = New System.Drawing.Size(1, 284)
        Me.Label151.TabIndex = 67
        Me.Label151.Text = "label2"
        '
        'Panel20
        '
        Me.Panel20.BackColor = System.Drawing.Color.White
        Me.Panel20.Controls.Add(Me.chklst_SmartTreatment)
        Me.Panel20.Controls.Add(Me.Label179)
        Me.Panel20.Controls.Add(Me.Label175)
        Me.Panel20.Controls.Add(Me.Label152)
        Me.Panel20.Controls.Add(Me.Label153)
        Me.Panel20.Controls.Add(Me.Panel21)
        Me.Panel20.Controls.Add(Me.Label156)
        Me.Panel20.Controls.Add(Me.Label157)
        Me.Panel20.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel20.Location = New System.Drawing.Point(0, 194)
        Me.Panel20.Name = "Panel20"
        Me.Panel20.Size = New System.Drawing.Size(230, 170)
        Me.Panel20.TabIndex = 82
        '
        'chklst_SmartTreatment
        '
        Me.chklst_SmartTreatment.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.chklst_SmartTreatment.CheckOnClick = true
        Me.chklst_SmartTreatment.Dock = System.Windows.Forms.DockStyle.Fill
        Me.chklst_SmartTreatment.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chklst_SmartTreatment.FormattingEnabled = true
        Me.chklst_SmartTreatment.Items.AddRange(New Object() {"ICD9", "Drugs", "Patient Education", "Tags", "Flowsheet", "Lab Orders", "Order Templates", "Referral Letter"})
        Me.chklst_SmartTreatment.Location = New System.Drawing.Point(5, 29)
        Me.chklst_SmartTreatment.Name = "chklst_SmartTreatment"
        Me.chklst_SmartTreatment.Size = New System.Drawing.Size(224, 140)
        Me.chklst_SmartTreatment.TabIndex = 0
        '
        'Label179
        '
        Me.Label179.BackColor = System.Drawing.Color.White
        Me.Label179.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label179.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label179.Location = New System.Drawing.Point(1, 29)
        Me.Label179.Name = "Label179"
        Me.Label179.Size = New System.Drawing.Size(4, 140)
        Me.Label179.TabIndex = 71
        '
        'Label175
        '
        Me.Label175.BackColor = System.Drawing.Color.White
        Me.Label175.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label175.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label175.Location = New System.Drawing.Point(1, 25)
        Me.Label175.Name = "Label175"
        Me.Label175.Size = New System.Drawing.Size(228, 4)
        Me.Label175.TabIndex = 70
        '
        'Label152
        '
        Me.Label152.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label152.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label152.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label152.Location = New System.Drawing.Point(1, 24)
        Me.Label152.Name = "Label152"
        Me.Label152.Size = New System.Drawing.Size(228, 1)
        Me.Label152.TabIndex = 64
        Me.Label152.Text = "label2"
        '
        'Label153
        '
        Me.Label153.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label153.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label153.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label153.Location = New System.Drawing.Point(1, 169)
        Me.Label153.Name = "Label153"
        Me.Label153.Size = New System.Drawing.Size(228, 1)
        Me.Label153.TabIndex = 63
        Me.Label153.Text = "label2"
        '
        'Panel21
        '
        Me.Panel21.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.Panel21.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel21.Controls.Add(Me.chk_SmartTreatment)
        Me.Panel21.Controls.Add(Me.Button14)
        Me.Panel21.Controls.Add(Me.Label166)
        Me.Panel21.Controls.Add(Me.btnClearAllCPT)
        Me.Panel21.Controls.Add(Me.Label165)
        Me.Panel21.Controls.Add(Me.btn_UpTreatment)
        Me.Panel21.Controls.Add(Me.Label164)
        Me.Panel21.Controls.Add(Me.btn_DownTreatment)
        Me.Panel21.Controls.Add(Me.Label154)
        Me.Panel21.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel21.Location = New System.Drawing.Point(1, 0)
        Me.Panel21.Name = "Panel21"
        Me.Panel21.Size = New System.Drawing.Size(228, 24)
        Me.Panel21.TabIndex = 0
        '
        'chk_SmartTreatment
        '
        Me.chk_SmartTreatment.AutoSize = true
        Me.chk_SmartTreatment.Location = New System.Drawing.Point(101, 7)
        Me.chk_SmartTreatment.Name = "chk_SmartTreatment"
        Me.chk_SmartTreatment.Size = New System.Drawing.Size(15, 14)
        Me.chk_SmartTreatment.TabIndex = 73
        Me.chk_SmartTreatment.UseVisualStyleBackColor = true
        Me.chk_SmartTreatment.Visible = false
        '
        'Button14
        '
        Me.Button14.BackColor = System.Drawing.Color.Transparent
        Me.Button14.Dock = System.Windows.Forms.DockStyle.Right
        Me.Button14.FlatAppearance.BorderSize = 0
        Me.Button14.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.Button14.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.Button14.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button14.Image = CType(resources.GetObject("Button14.Image"),System.Drawing.Image)
        Me.Button14.Location = New System.Drawing.Point(128, 0)
        Me.Button14.Name = "Button14"
        Me.Button14.Size = New System.Drawing.Size(22, 24)
        Me.Button14.TabIndex = 3
        Me.Button14.Tag = "SelectAll"
        Me.Button14.UseVisualStyleBackColor = false
        '
        'Label166
        '
        Me.Label166.BackColor = System.Drawing.Color.Transparent
        Me.Label166.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label166.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label166.Location = New System.Drawing.Point(150, 0)
        Me.Label166.Name = "Label166"
        Me.Label166.Size = New System.Drawing.Size(4, 24)
        Me.Label166.TabIndex = 72
        Me.Label166.Text = "label2"
        '
        'btnClearAllCPT
        '
        Me.btnClearAllCPT.BackColor = System.Drawing.Color.Transparent
        Me.btnClearAllCPT.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnClearAllCPT.FlatAppearance.BorderSize = 0
        Me.btnClearAllCPT.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnClearAllCPT.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnClearAllCPT.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClearAllCPT.Image = CType(resources.GetObject("btnClearAllCPT.Image"),System.Drawing.Image)
        Me.btnClearAllCPT.Location = New System.Drawing.Point(154, 0)
        Me.btnClearAllCPT.Name = "btnClearAllCPT"
        Me.btnClearAllCPT.Size = New System.Drawing.Size(22, 24)
        Me.btnClearAllCPT.TabIndex = 4
        Me.btnClearAllCPT.Tag = "ClearAll"
        Me.btnClearAllCPT.UseVisualStyleBackColor = false
        '
        'Label165
        '
        Me.Label165.BackColor = System.Drawing.Color.Transparent
        Me.Label165.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label165.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label165.Location = New System.Drawing.Point(176, 0)
        Me.Label165.Name = "Label165"
        Me.Label165.Size = New System.Drawing.Size(4, 24)
        Me.Label165.TabIndex = 71
        Me.Label165.Text = "label2"
        '
        'btn_UpTreatment
        '
        Me.btn_UpTreatment.BackgroundImage = CType(resources.GetObject("btn_UpTreatment.BackgroundImage"),System.Drawing.Image)
        Me.btn_UpTreatment.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btn_UpTreatment.Dock = System.Windows.Forms.DockStyle.Right
        Me.btn_UpTreatment.FlatAppearance.BorderSize = 0
        Me.btn_UpTreatment.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_UpTreatment.Image = CType(resources.GetObject("btn_UpTreatment.Image"),System.Drawing.Image)
        Me.btn_UpTreatment.Location = New System.Drawing.Point(180, 0)
        Me.btn_UpTreatment.Name = "btn_UpTreatment"
        Me.btn_UpTreatment.Size = New System.Drawing.Size(22, 24)
        Me.btn_UpTreatment.TabIndex = 1
        Me.btn_UpTreatment.UseVisualStyleBackColor = true
        '
        'Label164
        '
        Me.Label164.BackColor = System.Drawing.Color.Transparent
        Me.Label164.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label164.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label164.Location = New System.Drawing.Point(202, 0)
        Me.Label164.Name = "Label164"
        Me.Label164.Size = New System.Drawing.Size(4, 24)
        Me.Label164.TabIndex = 70
        Me.Label164.Text = "label2"
        '
        'btn_DownTreatment
        '
        Me.btn_DownTreatment.BackgroundImage = CType(resources.GetObject("btn_DownTreatment.BackgroundImage"),System.Drawing.Image)
        Me.btn_DownTreatment.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btn_DownTreatment.Dock = System.Windows.Forms.DockStyle.Right
        Me.btn_DownTreatment.FlatAppearance.BorderSize = 0
        Me.btn_DownTreatment.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_DownTreatment.Image = CType(resources.GetObject("btn_DownTreatment.Image"),System.Drawing.Image)
        Me.btn_DownTreatment.Location = New System.Drawing.Point(206, 0)
        Me.btn_DownTreatment.Name = "btn_DownTreatment"
        Me.btn_DownTreatment.Size = New System.Drawing.Size(22, 24)
        Me.btn_DownTreatment.TabIndex = 2
        Me.btn_DownTreatment.UseVisualStyleBackColor = true
        '
        'Label154
        '
        Me.Label154.BackColor = System.Drawing.Color.Transparent
        Me.Label154.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label154.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label154.Location = New System.Drawing.Point(0, 0)
        Me.Label154.Name = "Label154"
        Me.Label154.Size = New System.Drawing.Size(228, 24)
        Me.Label154.TabIndex = 67
        Me.Label154.Text = "  Treatment"
        Me.Label154.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label156
        '
        Me.Label156.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label156.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label156.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label156.Location = New System.Drawing.Point(0, 0)
        Me.Label156.Name = "Label156"
        Me.Label156.Size = New System.Drawing.Size(1, 170)
        Me.Label156.TabIndex = 66
        Me.Label156.Text = "label2"
        '
        'Label157
        '
        Me.Label157.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label157.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label157.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label157.Location = New System.Drawing.Point(229, 0)
        Me.Label157.Name = "Label157"
        Me.Label157.Size = New System.Drawing.Size(1, 170)
        Me.Label157.TabIndex = 67
        Me.Label157.Text = "label2"
        '
        'Panel14
        '
        Me.Panel14.BackColor = System.Drawing.Color.White
        Me.Panel14.Controls.Add(Me.chklst_SmartDiagnosis)
        Me.Panel14.Controls.Add(Me.Label172)
        Me.Panel14.Controls.Add(Me.Label171)
        Me.Panel14.Controls.Add(Me.Label134)
        Me.Panel14.Controls.Add(Me.Label135)
        Me.Panel14.Controls.Add(Me.Panel15)
        Me.Panel14.Controls.Add(Me.Label136)
        Me.Panel14.Controls.Add(Me.Label137)
        Me.Panel14.Controls.Add(Me.Label133)
        Me.Panel14.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel14.Location = New System.Drawing.Point(0, 24)
        Me.Panel14.Name = "Panel14"
        Me.Panel14.Size = New System.Drawing.Size(230, 170)
        Me.Panel14.TabIndex = 82
        '
        'chklst_SmartDiagnosis
        '
        Me.chklst_SmartDiagnosis.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.chklst_SmartDiagnosis.CheckOnClick = true
        Me.chklst_SmartDiagnosis.Dock = System.Windows.Forms.DockStyle.Fill
        Me.chklst_SmartDiagnosis.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chklst_SmartDiagnosis.FormattingEnabled = true
        Me.chklst_SmartDiagnosis.Items.AddRange(New Object() {"CPT", "Drugs", "Patient Education", "Tags", "Flowsheet", "Lab Orders", "Order Templates", "Referral Letter"})
        Me.chklst_SmartDiagnosis.Location = New System.Drawing.Point(5, 30)
        Me.chklst_SmartDiagnosis.Name = "chklst_SmartDiagnosis"
        Me.chklst_SmartDiagnosis.Size = New System.Drawing.Size(224, 139)
        Me.chklst_SmartDiagnosis.TabIndex = 0
        '
        'Label172
        '
        Me.Label172.BackColor = System.Drawing.Color.White
        Me.Label172.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label172.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label172.Location = New System.Drawing.Point(5, 26)
        Me.Label172.Name = "Label172"
        Me.Label172.Size = New System.Drawing.Size(224, 4)
        Me.Label172.TabIndex = 69
        '
        'Label171
        '
        Me.Label171.BackColor = System.Drawing.Color.White
        Me.Label171.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label171.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label171.Location = New System.Drawing.Point(1, 26)
        Me.Label171.Name = "Label171"
        Me.Label171.Size = New System.Drawing.Size(4, 143)
        Me.Label171.TabIndex = 68
        '
        'Label134
        '
        Me.Label134.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label134.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label134.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label134.Location = New System.Drawing.Point(1, 25)
        Me.Label134.Name = "Label134"
        Me.Label134.Size = New System.Drawing.Size(228, 1)
        Me.Label134.TabIndex = 64
        Me.Label134.Text = "label2"
        '
        'Label135
        '
        Me.Label135.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label135.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label135.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label135.Location = New System.Drawing.Point(1, 169)
        Me.Label135.Name = "Label135"
        Me.Label135.Size = New System.Drawing.Size(228, 1)
        Me.Label135.TabIndex = 63
        Me.Label135.Text = "label2"
        '
        'Panel15
        '
        Me.Panel15.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.Panel15.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel15.Controls.Add(Me.chk_SmartDiagnosis)
        Me.Panel15.Controls.Add(Me.btnSelectAllDX)
        Me.Panel15.Controls.Add(Me.Label170)
        Me.Panel15.Controls.Add(Me.btnClearAllDX)
        Me.Panel15.Controls.Add(Me.Label169)
        Me.Panel15.Controls.Add(Me.btn_UpDiagnosis)
        Me.Panel15.Controls.Add(Me.Label168)
        Me.Panel15.Controls.Add(Me.btn_DownDiagnosis)
        Me.Panel15.Controls.Add(Me.Label139)
        Me.Panel15.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel15.Location = New System.Drawing.Point(1, 1)
        Me.Panel15.Name = "Panel15"
        Me.Panel15.Size = New System.Drawing.Size(228, 24)
        Me.Panel15.TabIndex = 0
        '
        'chk_SmartDiagnosis
        '
        Me.chk_SmartDiagnosis.AutoSize = true
        Me.chk_SmartDiagnosis.Location = New System.Drawing.Point(101, 6)
        Me.chk_SmartDiagnosis.Name = "chk_SmartDiagnosis"
        Me.chk_SmartDiagnosis.Size = New System.Drawing.Size(15, 14)
        Me.chk_SmartDiagnosis.TabIndex = 72
        Me.chk_SmartDiagnosis.UseVisualStyleBackColor = true
        Me.chk_SmartDiagnosis.Visible = false
        '
        'btnSelectAllDX
        '
        Me.btnSelectAllDX.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.btnSelectAllDX.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnSelectAllDX.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnSelectAllDX.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.btnSelectAllDX.FlatAppearance.BorderSize = 0
        Me.btnSelectAllDX.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnSelectAllDX.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnSelectAllDX.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSelectAllDX.Image = CType(resources.GetObject("btnSelectAllDX.Image"),System.Drawing.Image)
        Me.btnSelectAllDX.Location = New System.Drawing.Point(128, 0)
        Me.btnSelectAllDX.Name = "btnSelectAllDX"
        Me.btnSelectAllDX.Size = New System.Drawing.Size(22, 24)
        Me.btnSelectAllDX.TabIndex = 3
        Me.btnSelectAllDX.Tag = "SelectAll"
        Me.btnSelectAllDX.UseVisualStyleBackColor = true
        '
        'Label170
        '
        Me.Label170.BackColor = System.Drawing.Color.Transparent
        Me.Label170.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label170.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label170.Location = New System.Drawing.Point(150, 0)
        Me.Label170.Name = "Label170"
        Me.Label170.Size = New System.Drawing.Size(4, 24)
        Me.Label170.TabIndex = 71
        Me.Label170.Text = "label2"
        '
        'btnClearAllDX
        '
        Me.btnClearAllDX.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.btnClearAllDX.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClearAllDX.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnClearAllDX.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.btnClearAllDX.FlatAppearance.BorderSize = 0
        Me.btnClearAllDX.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnClearAllDX.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnClearAllDX.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClearAllDX.Image = CType(resources.GetObject("btnClearAllDX.Image"),System.Drawing.Image)
        Me.btnClearAllDX.Location = New System.Drawing.Point(154, 0)
        Me.btnClearAllDX.Name = "btnClearAllDX"
        Me.btnClearAllDX.Size = New System.Drawing.Size(22, 24)
        Me.btnClearAllDX.TabIndex = 4
        Me.btnClearAllDX.Tag = "ClearAll"
        Me.btnClearAllDX.UseVisualStyleBackColor = true
        '
        'Label169
        '
        Me.Label169.BackColor = System.Drawing.Color.Transparent
        Me.Label169.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label169.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label169.Location = New System.Drawing.Point(176, 0)
        Me.Label169.Name = "Label169"
        Me.Label169.Size = New System.Drawing.Size(4, 24)
        Me.Label169.TabIndex = 70
        Me.Label169.Text = "label2"
        '
        'btn_UpDiagnosis
        '
        Me.btn_UpDiagnosis.BackgroundImage = CType(resources.GetObject("btn_UpDiagnosis.BackgroundImage"),System.Drawing.Image)
        Me.btn_UpDiagnosis.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btn_UpDiagnosis.Dock = System.Windows.Forms.DockStyle.Right
        Me.btn_UpDiagnosis.FlatAppearance.BorderSize = 0
        Me.btn_UpDiagnosis.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_UpDiagnosis.Image = CType(resources.GetObject("btn_UpDiagnosis.Image"),System.Drawing.Image)
        Me.btn_UpDiagnosis.Location = New System.Drawing.Point(180, 0)
        Me.btn_UpDiagnosis.Name = "btn_UpDiagnosis"
        Me.btn_UpDiagnosis.Size = New System.Drawing.Size(22, 24)
        Me.btn_UpDiagnosis.TabIndex = 1
        Me.btn_UpDiagnosis.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.btn_UpDiagnosis.UseVisualStyleBackColor = true
        '
        'Label168
        '
        Me.Label168.BackColor = System.Drawing.Color.Transparent
        Me.Label168.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label168.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label168.Location = New System.Drawing.Point(202, 0)
        Me.Label168.Name = "Label168"
        Me.Label168.Size = New System.Drawing.Size(4, 24)
        Me.Label168.TabIndex = 69
        Me.Label168.Text = "label2"
        '
        'btn_DownDiagnosis
        '
        Me.btn_DownDiagnosis.BackgroundImage = CType(resources.GetObject("btn_DownDiagnosis.BackgroundImage"),System.Drawing.Image)
        Me.btn_DownDiagnosis.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btn_DownDiagnosis.Dock = System.Windows.Forms.DockStyle.Right
        Me.btn_DownDiagnosis.FlatAppearance.BorderSize = 0
        Me.btn_DownDiagnosis.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_DownDiagnosis.Image = CType(resources.GetObject("btn_DownDiagnosis.Image"),System.Drawing.Image)
        Me.btn_DownDiagnosis.Location = New System.Drawing.Point(206, 0)
        Me.btn_DownDiagnosis.Name = "btn_DownDiagnosis"
        Me.btn_DownDiagnosis.Size = New System.Drawing.Size(22, 24)
        Me.btn_DownDiagnosis.TabIndex = 2
        Me.btn_DownDiagnosis.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.btn_DownDiagnosis.UseVisualStyleBackColor = true
        '
        'Label139
        '
        Me.Label139.BackColor = System.Drawing.Color.Transparent
        Me.Label139.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label139.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label139.Location = New System.Drawing.Point(0, 0)
        Me.Label139.Name = "Label139"
        Me.Label139.Size = New System.Drawing.Size(228, 24)
        Me.Label139.TabIndex = 68
        Me.Label139.Text = "  Diagnosis"
        Me.Label139.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label136
        '
        Me.Label136.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label136.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label136.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label136.Location = New System.Drawing.Point(1, 0)
        Me.Label136.Name = "Label136"
        Me.Label136.Size = New System.Drawing.Size(228, 1)
        Me.Label136.TabIndex = 65
        Me.Label136.Text = "label2"
        '
        'Label137
        '
        Me.Label137.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label137.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label137.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label137.Location = New System.Drawing.Point(0, 0)
        Me.Label137.Name = "Label137"
        Me.Label137.Size = New System.Drawing.Size(1, 170)
        Me.Label137.TabIndex = 66
        Me.Label137.Text = "label2"
        '
        'Label133
        '
        Me.Label133.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label133.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label133.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label133.Location = New System.Drawing.Point(229, 0)
        Me.Label133.Name = "Label133"
        Me.Label133.Size = New System.Drawing.Size(1, 170)
        Me.Label133.TabIndex = 67
        Me.Label133.Text = "label2"
        '
        'Panel9
        '
        Me.Panel9.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Blue2007
        Me.Panel9.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel9.Controls.Add(Me.Label189)
        Me.Panel9.Controls.Add(Me.Label187)
        Me.Panel9.Controls.Add(Me.Label186)
        Me.Panel9.Controls.Add(Me.Label185)
        Me.Panel9.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel9.Location = New System.Drawing.Point(0, 0)
        Me.Panel9.Name = "Panel9"
        Me.Panel9.Size = New System.Drawing.Size(230, 24)
        Me.Panel9.TabIndex = 90
        '
        'Label189
        '
        Me.Label189.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label189.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label189.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label189.Location = New System.Drawing.Point(1, 0)
        Me.Label189.Name = "Label189"
        Me.Label189.Size = New System.Drawing.Size(228, 1)
        Me.Label189.TabIndex = 71
        Me.Label189.Text = "label2"
        '
        'Label187
        '
        Me.Label187.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label187.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label187.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label187.Location = New System.Drawing.Point(229, 0)
        Me.Label187.Name = "Label187"
        Me.Label187.Size = New System.Drawing.Size(1, 24)
        Me.Label187.TabIndex = 69
        Me.Label187.Text = "label2"
        '
        'Label186
        '
        Me.Label186.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label186.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label186.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label186.Location = New System.Drawing.Point(0, 0)
        Me.Label186.Name = "Label186"
        Me.Label186.Size = New System.Drawing.Size(1, 24)
        Me.Label186.TabIndex = 68
        Me.Label186.Text = "label2"
        '
        'Label185
        '
        Me.Label185.BackColor = System.Drawing.Color.Transparent
        Me.Label185.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label185.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label185.ForeColor = System.Drawing.Color.White
        Me.Label185.Location = New System.Drawing.Point(0, 0)
        Me.Label185.Name = "Label185"
        Me.Label185.Size = New System.Drawing.Size(230, 24)
        Me.Label185.TabIndex = 67
        Me.Label185.Text = "  Smart Workflow"
        Me.Label185.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Splitter1
        '
        Me.Splitter1.Dock = System.Windows.Forms.DockStyle.Right
        Me.Splitter1.Enabled = false
        Me.Splitter1.Location = New System.Drawing.Point(230, 0)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(4, 648)
        Me.Splitter1.TabIndex = 92
        Me.Splitter1.TabStop = false
        '
        'Panel26
        '
        Me.Panel26.Controls.Add(Me.Panel22)
        Me.Panel26.Controls.Add(Me.Panel16)
        Me.Panel26.Controls.Add(Me.Panel12)
        Me.Panel26.Controls.Add(Me.Panel27)
        Me.Panel26.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel26.Location = New System.Drawing.Point(234, 0)
        Me.Panel26.Name = "Panel26"
        Me.Panel26.Size = New System.Drawing.Size(450, 648)
        Me.Panel26.TabIndex = 91
        '
        'Panel22
        '
        Me.Panel22.BackColor = System.Drawing.Color.White
        Me.Panel22.Controls.Add(Me.C1SmartOrdersSendTask)
        Me.Panel22.Controls.Add(Me.pnlSmartOrder_Select)
        Me.Panel22.Controls.Add(Me.chklst_SmartOrdersSendTask)
        Me.Panel22.Controls.Add(Me.Label158)
        Me.Panel22.Controls.Add(Me.Label159)
        Me.Panel22.Controls.Add(Me.Panel23)
        Me.Panel22.Controls.Add(Me.Label161)
        Me.Panel22.Controls.Add(Me.Label162)
        Me.Panel22.Controls.Add(Me.Label163)
        Me.Panel22.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel22.Location = New System.Drawing.Point(0, 364)
        Me.Panel22.Name = "Panel22"
        Me.Panel22.Size = New System.Drawing.Size(450, 284)
        Me.Panel22.TabIndex = 82
        '
        'C1SmartOrdersSendTask
        '
        Me.C1SmartOrdersSendTask.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.C1SmartOrdersSendTask.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None
        Me.C1SmartOrdersSendTask.BackColor = System.Drawing.Color.FromArgb(CType(CType(240,Byte),Integer), CType(CType(247,Byte),Integer), CType(CType(255,Byte),Integer))
        Me.C1SmartOrdersSendTask.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.C1SmartOrdersSendTask.ColumnInfo = "1,0,0,0,0,95,Columns:0{StyleFixed:""TextAlign:CenterCenter;ImageAlign:CenterCenter"& _ 
    ";"";}"&Global.Microsoft.VisualBasic.ChrW(9)
        Me.C1SmartOrdersSendTask.Dock = System.Windows.Forms.DockStyle.Fill
        Me.C1SmartOrdersSendTask.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.C1SmartOrdersSendTask.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.C1SmartOrdersSendTask.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross
        Me.C1SmartOrdersSendTask.Location = New System.Drawing.Point(1, 50)
        Me.C1SmartOrdersSendTask.Name = "C1SmartOrdersSendTask"
        Me.C1SmartOrdersSendTask.Rows.Count = 1
        Me.C1SmartOrdersSendTask.Rows.DefaultSize = 19
        Me.C1SmartOrdersSendTask.Rows.Fixed = 0
        Me.C1SmartOrdersSendTask.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.C1SmartOrdersSendTask.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        Me.C1SmartOrdersSendTask.ShowCellLabels = true
        Me.C1SmartOrdersSendTask.ShowSortPosition = C1.Win.C1FlexGrid.ShowSortPositionEnum.None
        Me.C1SmartOrdersSendTask.Size = New System.Drawing.Size(448, 233)
        Me.C1SmartOrdersSendTask.StyleInfo = resources.GetString("C1SmartOrdersSendTask.StyleInfo")
        Me.C1SmartOrdersSendTask.TabIndex = 72
        Me.C1SmartOrdersSendTask.Tree.NodeImageCollapsed = CType(resources.GetObject("C1SmartOrdersSendTask.Tree.NodeImageCollapsed"),System.Drawing.Image)
        Me.C1SmartOrdersSendTask.Tree.NodeImageExpanded = CType(resources.GetObject("C1SmartOrdersSendTask.Tree.NodeImageExpanded"),System.Drawing.Image)
        '
        'pnlSmartOrder_Select
        '
        Me.pnlSmartOrder_Select.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.pnlSmartOrder_Select.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlSmartOrder_Select.Controls.Add(Me.btnSmartOrderView_All)
        Me.pnlSmartOrder_Select.Controls.Add(Me.btnSmartOrderView_Cancel)
        Me.pnlSmartOrder_Select.Controls.Add(Me.btnSmartOrderSelect_Cancel)
        Me.pnlSmartOrder_Select.Controls.Add(Me.btnSmartOrderSelect_All)
        Me.pnlSmartOrder_Select.Controls.Add(Me.Label141)
        Me.pnlSmartOrder_Select.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlSmartOrder_Select.Location = New System.Drawing.Point(1, 26)
        Me.pnlSmartOrder_Select.Name = "pnlSmartOrder_Select"
        Me.pnlSmartOrder_Select.Size = New System.Drawing.Size(448, 24)
        Me.pnlSmartOrder_Select.TabIndex = 74
        '
        'btnSmartOrderView_All
        '
        Me.btnSmartOrderView_All.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.btnSmartOrderView_All.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnSmartOrderView_All.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnSmartOrderView_All.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.btnSmartOrderView_All.FlatAppearance.BorderSize = 0
        Me.btnSmartOrderView_All.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnSmartOrderView_All.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnSmartOrderView_All.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSmartOrderView_All.Image = CType(resources.GetObject("btnSmartOrderView_All.Image"),System.Drawing.Image)
        Me.btnSmartOrderView_All.Location = New System.Drawing.Point(404, 0)
        Me.btnSmartOrderView_All.Name = "btnSmartOrderView_All"
        Me.btnSmartOrderView_All.Size = New System.Drawing.Size(22, 23)
        Me.btnSmartOrderView_All.TabIndex = 7
        Me.btnSmartOrderView_All.Tag = "SelectAll"
        Me.btnSmartOrderView_All.UseVisualStyleBackColor = true
        '
        'btnSmartOrderView_Cancel
        '
        Me.btnSmartOrderView_Cancel.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.btnSmartOrderView_Cancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnSmartOrderView_Cancel.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnSmartOrderView_Cancel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.btnSmartOrderView_Cancel.FlatAppearance.BorderSize = 0
        Me.btnSmartOrderView_Cancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnSmartOrderView_Cancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnSmartOrderView_Cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSmartOrderView_Cancel.Image = CType(resources.GetObject("btnSmartOrderView_Cancel.Image"),System.Drawing.Image)
        Me.btnSmartOrderView_Cancel.Location = New System.Drawing.Point(426, 0)
        Me.btnSmartOrderView_Cancel.Name = "btnSmartOrderView_Cancel"
        Me.btnSmartOrderView_Cancel.Size = New System.Drawing.Size(22, 23)
        Me.btnSmartOrderView_Cancel.TabIndex = 6
        Me.btnSmartOrderView_Cancel.Tag = "ClearAll"
        Me.btnSmartOrderView_Cancel.UseVisualStyleBackColor = true
        '
        'btnSmartOrderSelect_Cancel
        '
        Me.btnSmartOrderSelect_Cancel.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.btnSmartOrderSelect_Cancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnSmartOrderSelect_Cancel.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnSmartOrderSelect_Cancel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.btnSmartOrderSelect_Cancel.FlatAppearance.BorderSize = 0
        Me.btnSmartOrderSelect_Cancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnSmartOrderSelect_Cancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnSmartOrderSelect_Cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSmartOrderSelect_Cancel.Image = CType(resources.GetObject("btnSmartOrderSelect_Cancel.Image"),System.Drawing.Image)
        Me.btnSmartOrderSelect_Cancel.Location = New System.Drawing.Point(22, 0)
        Me.btnSmartOrderSelect_Cancel.Name = "btnSmartOrderSelect_Cancel"
        Me.btnSmartOrderSelect_Cancel.Size = New System.Drawing.Size(22, 23)
        Me.btnSmartOrderSelect_Cancel.TabIndex = 5
        Me.btnSmartOrderSelect_Cancel.Tag = "ClearAll"
        Me.btnSmartOrderSelect_Cancel.UseVisualStyleBackColor = true
        '
        'btnSmartOrderSelect_All
        '
        Me.btnSmartOrderSelect_All.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.btnSmartOrderSelect_All.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnSmartOrderSelect_All.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnSmartOrderSelect_All.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.btnSmartOrderSelect_All.FlatAppearance.BorderSize = 0
        Me.btnSmartOrderSelect_All.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnSmartOrderSelect_All.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnSmartOrderSelect_All.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSmartOrderSelect_All.Image = CType(resources.GetObject("btnSmartOrderSelect_All.Image"),System.Drawing.Image)
        Me.btnSmartOrderSelect_All.Location = New System.Drawing.Point(0, 0)
        Me.btnSmartOrderSelect_All.Name = "btnSmartOrderSelect_All"
        Me.btnSmartOrderSelect_All.Size = New System.Drawing.Size(22, 23)
        Me.btnSmartOrderSelect_All.TabIndex = 4
        Me.btnSmartOrderSelect_All.Tag = "SelectAll"
        Me.btnSmartOrderSelect_All.UseVisualStyleBackColor = true
        '
        'Label141
        '
        Me.Label141.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label141.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label141.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label141.Location = New System.Drawing.Point(0, 23)
        Me.Label141.Name = "Label141"
        Me.Label141.Size = New System.Drawing.Size(448, 1)
        Me.Label141.TabIndex = 64
        Me.Label141.Text = "label2"
        '
        'chklst_SmartOrdersSendTask
        '
        Me.chklst_SmartOrdersSendTask.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.chklst_SmartOrdersSendTask.CheckOnClick = true
        Me.chklst_SmartOrdersSendTask.Dock = System.Windows.Forms.DockStyle.Fill
        Me.chklst_SmartOrdersSendTask.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chklst_SmartOrdersSendTask.FormattingEnabled = true
        Me.chklst_SmartOrdersSendTask.Items.AddRange(New Object() {"Lab Orders", "Order Templates", "Flowsheet", "Drugs"})
        Me.chklst_SmartOrdersSendTask.Location = New System.Drawing.Point(1, 26)
        Me.chklst_SmartOrdersSendTask.Name = "chklst_SmartOrdersSendTask"
        Me.chklst_SmartOrdersSendTask.Size = New System.Drawing.Size(448, 257)
        Me.chklst_SmartOrdersSendTask.TabIndex = 0
        Me.chklst_SmartOrdersSendTask.Visible = false
        '
        'Label158
        '
        Me.Label158.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label158.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label158.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label158.Location = New System.Drawing.Point(1, 25)
        Me.Label158.Name = "Label158"
        Me.Label158.Size = New System.Drawing.Size(448, 1)
        Me.Label158.TabIndex = 64
        Me.Label158.Text = "label2"
        '
        'Label159
        '
        Me.Label159.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label159.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label159.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label159.Location = New System.Drawing.Point(1, 283)
        Me.Label159.Name = "Label159"
        Me.Label159.Size = New System.Drawing.Size(448, 1)
        Me.Label159.TabIndex = 63
        Me.Label159.Text = "label2"
        '
        'Panel23
        '
        Me.Panel23.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.Panel23.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel23.Controls.Add(Me.chk_SmartOrder_SendTask)
        Me.Panel23.Controls.Add(Me.Label160)
        Me.Panel23.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel23.Location = New System.Drawing.Point(1, 1)
        Me.Panel23.Name = "Panel23"
        Me.Panel23.Size = New System.Drawing.Size(448, 24)
        Me.Panel23.TabIndex = 0
        '
        'chk_SmartOrder_SendTask
        '
        Me.chk_SmartOrder_SendTask.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.chk_SmartOrder_SendTask.AutoSize = true
        Me.chk_SmartOrder_SendTask.Location = New System.Drawing.Point(427, 5)
        Me.chk_SmartOrder_SendTask.Name = "chk_SmartOrder_SendTask"
        Me.chk_SmartOrder_SendTask.Size = New System.Drawing.Size(15, 14)
        Me.chk_SmartOrder_SendTask.TabIndex = 5
        Me.chk_SmartOrder_SendTask.UseVisualStyleBackColor = true
        Me.chk_SmartOrder_SendTask.Visible = false
        '
        'Label160
        '
        Me.Label160.BackColor = System.Drawing.Color.Transparent
        Me.Label160.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label160.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label160.Location = New System.Drawing.Point(0, 0)
        Me.Label160.Name = "Label160"
        Me.Label160.Size = New System.Drawing.Size(448, 24)
        Me.Label160.TabIndex = 67
        Me.Label160.Text = "   Orders "
        Me.Label160.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label161
        '
        Me.Label161.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label161.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label161.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label161.Location = New System.Drawing.Point(1, 0)
        Me.Label161.Name = "Label161"
        Me.Label161.Size = New System.Drawing.Size(448, 1)
        Me.Label161.TabIndex = 65
        Me.Label161.Text = "label2"
        '
        'Label162
        '
        Me.Label162.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label162.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label162.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label162.Location = New System.Drawing.Point(0, 0)
        Me.Label162.Name = "Label162"
        Me.Label162.Size = New System.Drawing.Size(1, 284)
        Me.Label162.TabIndex = 66
        Me.Label162.Text = "label2"
        '
        'Label163
        '
        Me.Label163.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label163.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label163.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label163.Location = New System.Drawing.Point(449, 0)
        Me.Label163.Name = "Label163"
        Me.Label163.Size = New System.Drawing.Size(1, 284)
        Me.Label163.TabIndex = 67
        Me.Label163.Text = "label2"
        '
        'Panel16
        '
        Me.Panel16.BackColor = System.Drawing.Color.White
        Me.Panel16.Controls.Add(Me.C1SmartTreatmentSendTask)
        Me.Panel16.Controls.Add(Me.pnlSmartTreatment_Select)
        Me.Panel16.Controls.Add(Me.chklst_SmartTreatmentSendTask)
        Me.Panel16.Controls.Add(Me.Label140)
        Me.Panel16.Controls.Add(Me.Panel17)
        Me.Panel16.Controls.Add(Me.Label144)
        Me.Panel16.Controls.Add(Me.Label145)
        Me.Panel16.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel16.Location = New System.Drawing.Point(0, 194)
        Me.Panel16.Name = "Panel16"
        Me.Panel16.Size = New System.Drawing.Size(450, 170)
        Me.Panel16.TabIndex = 82
        '
        'C1SmartTreatmentSendTask
        '
        Me.C1SmartTreatmentSendTask.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.C1SmartTreatmentSendTask.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None
        Me.C1SmartTreatmentSendTask.BackColor = System.Drawing.Color.FromArgb(CType(CType(240,Byte),Integer), CType(CType(247,Byte),Integer), CType(CType(255,Byte),Integer))
        Me.C1SmartTreatmentSendTask.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.C1SmartTreatmentSendTask.ColumnInfo = "1,0,0,0,0,95,Columns:0{StyleFixed:""TextAlign:CenterCenter;ImageAlign:CenterCenter"& _ 
    ";"";}"&Global.Microsoft.VisualBasic.ChrW(9)
        Me.C1SmartTreatmentSendTask.Dock = System.Windows.Forms.DockStyle.Fill
        Me.C1SmartTreatmentSendTask.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.C1SmartTreatmentSendTask.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.C1SmartTreatmentSendTask.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross
        Me.C1SmartTreatmentSendTask.Location = New System.Drawing.Point(1, 49)
        Me.C1SmartTreatmentSendTask.Name = "C1SmartTreatmentSendTask"
        Me.C1SmartTreatmentSendTask.Rows.Count = 1
        Me.C1SmartTreatmentSendTask.Rows.DefaultSize = 19
        Me.C1SmartTreatmentSendTask.Rows.Fixed = 0
        Me.C1SmartTreatmentSendTask.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.C1SmartTreatmentSendTask.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        Me.C1SmartTreatmentSendTask.ShowCellLabels = true
        Me.C1SmartTreatmentSendTask.ShowSortPosition = C1.Win.C1FlexGrid.ShowSortPositionEnum.None
        Me.C1SmartTreatmentSendTask.Size = New System.Drawing.Size(448, 121)
        Me.C1SmartTreatmentSendTask.StyleInfo = resources.GetString("C1SmartTreatmentSendTask.StyleInfo")
        Me.C1SmartTreatmentSendTask.TabIndex = 72
        Me.C1SmartTreatmentSendTask.Tree.NodeImageCollapsed = CType(resources.GetObject("C1SmartTreatmentSendTask.Tree.NodeImageCollapsed"),System.Drawing.Image)
        Me.C1SmartTreatmentSendTask.Tree.NodeImageExpanded = CType(resources.GetObject("C1SmartTreatmentSendTask.Tree.NodeImageExpanded"),System.Drawing.Image)
        '
        'pnlSmartTreatment_Select
        '
        Me.pnlSmartTreatment_Select.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.pnlSmartTreatment_Select.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlSmartTreatment_Select.Controls.Add(Me.btnSmartTreatmentView_All)
        Me.pnlSmartTreatment_Select.Controls.Add(Me.btnSmartTreatmentView_Cancel)
        Me.pnlSmartTreatment_Select.Controls.Add(Me.btnSmartTreatmentSelect_Cancel)
        Me.pnlSmartTreatment_Select.Controls.Add(Me.btnSmartTreatmentSelect_All)
        Me.pnlSmartTreatment_Select.Controls.Add(Me.Label143)
        Me.pnlSmartTreatment_Select.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlSmartTreatment_Select.Location = New System.Drawing.Point(1, 25)
        Me.pnlSmartTreatment_Select.Name = "pnlSmartTreatment_Select"
        Me.pnlSmartTreatment_Select.Size = New System.Drawing.Size(448, 24)
        Me.pnlSmartTreatment_Select.TabIndex = 74
        '
        'btnSmartTreatmentView_All
        '
        Me.btnSmartTreatmentView_All.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.btnSmartTreatmentView_All.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnSmartTreatmentView_All.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnSmartTreatmentView_All.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.btnSmartTreatmentView_All.FlatAppearance.BorderSize = 0
        Me.btnSmartTreatmentView_All.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnSmartTreatmentView_All.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnSmartTreatmentView_All.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSmartTreatmentView_All.Image = CType(resources.GetObject("btnSmartTreatmentView_All.Image"),System.Drawing.Image)
        Me.btnSmartTreatmentView_All.Location = New System.Drawing.Point(404, 0)
        Me.btnSmartTreatmentView_All.Name = "btnSmartTreatmentView_All"
        Me.btnSmartTreatmentView_All.Size = New System.Drawing.Size(22, 23)
        Me.btnSmartTreatmentView_All.TabIndex = 7
        Me.btnSmartTreatmentView_All.Tag = "SelectAll"
        Me.btnSmartTreatmentView_All.UseVisualStyleBackColor = true
        '
        'btnSmartTreatmentView_Cancel
        '
        Me.btnSmartTreatmentView_Cancel.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.btnSmartTreatmentView_Cancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnSmartTreatmentView_Cancel.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnSmartTreatmentView_Cancel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.btnSmartTreatmentView_Cancel.FlatAppearance.BorderSize = 0
        Me.btnSmartTreatmentView_Cancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnSmartTreatmentView_Cancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnSmartTreatmentView_Cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSmartTreatmentView_Cancel.Image = CType(resources.GetObject("btnSmartTreatmentView_Cancel.Image"),System.Drawing.Image)
        Me.btnSmartTreatmentView_Cancel.Location = New System.Drawing.Point(426, 0)
        Me.btnSmartTreatmentView_Cancel.Name = "btnSmartTreatmentView_Cancel"
        Me.btnSmartTreatmentView_Cancel.Size = New System.Drawing.Size(22, 23)
        Me.btnSmartTreatmentView_Cancel.TabIndex = 6
        Me.btnSmartTreatmentView_Cancel.Tag = "ClearAll"
        Me.btnSmartTreatmentView_Cancel.UseVisualStyleBackColor = true
        '
        'btnSmartTreatmentSelect_Cancel
        '
        Me.btnSmartTreatmentSelect_Cancel.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.btnSmartTreatmentSelect_Cancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnSmartTreatmentSelect_Cancel.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnSmartTreatmentSelect_Cancel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.btnSmartTreatmentSelect_Cancel.FlatAppearance.BorderSize = 0
        Me.btnSmartTreatmentSelect_Cancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnSmartTreatmentSelect_Cancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnSmartTreatmentSelect_Cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSmartTreatmentSelect_Cancel.Image = CType(resources.GetObject("btnSmartTreatmentSelect_Cancel.Image"),System.Drawing.Image)
        Me.btnSmartTreatmentSelect_Cancel.Location = New System.Drawing.Point(22, 0)
        Me.btnSmartTreatmentSelect_Cancel.Name = "btnSmartTreatmentSelect_Cancel"
        Me.btnSmartTreatmentSelect_Cancel.Size = New System.Drawing.Size(22, 23)
        Me.btnSmartTreatmentSelect_Cancel.TabIndex = 5
        Me.btnSmartTreatmentSelect_Cancel.Tag = "ClearAll"
        Me.btnSmartTreatmentSelect_Cancel.UseVisualStyleBackColor = true
        '
        'btnSmartTreatmentSelect_All
        '
        Me.btnSmartTreatmentSelect_All.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.btnSmartTreatmentSelect_All.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnSmartTreatmentSelect_All.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnSmartTreatmentSelect_All.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.btnSmartTreatmentSelect_All.FlatAppearance.BorderSize = 0
        Me.btnSmartTreatmentSelect_All.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnSmartTreatmentSelect_All.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnSmartTreatmentSelect_All.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSmartTreatmentSelect_All.Image = CType(resources.GetObject("btnSmartTreatmentSelect_All.Image"),System.Drawing.Image)
        Me.btnSmartTreatmentSelect_All.Location = New System.Drawing.Point(0, 0)
        Me.btnSmartTreatmentSelect_All.Name = "btnSmartTreatmentSelect_All"
        Me.btnSmartTreatmentSelect_All.Size = New System.Drawing.Size(22, 23)
        Me.btnSmartTreatmentSelect_All.TabIndex = 4
        Me.btnSmartTreatmentSelect_All.Tag = "SelectAll"
        Me.btnSmartTreatmentSelect_All.UseVisualStyleBackColor = true
        '
        'Label143
        '
        Me.Label143.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label143.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label143.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label143.Location = New System.Drawing.Point(0, 23)
        Me.Label143.Name = "Label143"
        Me.Label143.Size = New System.Drawing.Size(448, 1)
        Me.Label143.TabIndex = 65
        Me.Label143.Text = "label2"
        '
        'chklst_SmartTreatmentSendTask
        '
        Me.chklst_SmartTreatmentSendTask.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.chklst_SmartTreatmentSendTask.CheckOnClick = true
        Me.chklst_SmartTreatmentSendTask.Dock = System.Windows.Forms.DockStyle.Fill
        Me.chklst_SmartTreatmentSendTask.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chklst_SmartTreatmentSendTask.FormattingEnabled = true
        Me.chklst_SmartTreatmentSendTask.Items.AddRange(New Object() {"Lab Orders", "Order Templates", "Flowsheet", "Drugs"})
        Me.chklst_SmartTreatmentSendTask.Location = New System.Drawing.Point(1, 25)
        Me.chklst_SmartTreatmentSendTask.Name = "chklst_SmartTreatmentSendTask"
        Me.chklst_SmartTreatmentSendTask.Size = New System.Drawing.Size(448, 145)
        Me.chklst_SmartTreatmentSendTask.TabIndex = 0
        Me.chklst_SmartTreatmentSendTask.Visible = false
        '
        'Label140
        '
        Me.Label140.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label140.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label140.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label140.Location = New System.Drawing.Point(1, 24)
        Me.Label140.Name = "Label140"
        Me.Label140.Size = New System.Drawing.Size(448, 1)
        Me.Label140.TabIndex = 64
        Me.Label140.Text = "label2"
        '
        'Panel17
        '
        Me.Panel17.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.Panel17.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel17.Controls.Add(Me.chk_SmartTreatment_SendTask)
        Me.Panel17.Controls.Add(Me.Label142)
        Me.Panel17.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel17.Location = New System.Drawing.Point(1, 0)
        Me.Panel17.Name = "Panel17"
        Me.Panel17.Size = New System.Drawing.Size(448, 24)
        Me.Panel17.TabIndex = 0
        '
        'chk_SmartTreatment_SendTask
        '
        Me.chk_SmartTreatment_SendTask.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.chk_SmartTreatment_SendTask.AutoSize = true
        Me.chk_SmartTreatment_SendTask.Location = New System.Drawing.Point(427, 6)
        Me.chk_SmartTreatment_SendTask.Name = "chk_SmartTreatment_SendTask"
        Me.chk_SmartTreatment_SendTask.Size = New System.Drawing.Size(15, 14)
        Me.chk_SmartTreatment_SendTask.TabIndex = 5
        Me.chk_SmartTreatment_SendTask.UseVisualStyleBackColor = true
        Me.chk_SmartTreatment_SendTask.Visible = false
        '
        'Label142
        '
        Me.Label142.BackColor = System.Drawing.Color.Transparent
        Me.Label142.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label142.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label142.Location = New System.Drawing.Point(0, 0)
        Me.Label142.Name = "Label142"
        Me.Label142.Size = New System.Drawing.Size(448, 24)
        Me.Label142.TabIndex = 67
        Me.Label142.Text = "   Treatment"
        Me.Label142.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label144
        '
        Me.Label144.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label144.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label144.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label144.Location = New System.Drawing.Point(0, 0)
        Me.Label144.Name = "Label144"
        Me.Label144.Size = New System.Drawing.Size(1, 170)
        Me.Label144.TabIndex = 66
        Me.Label144.Text = "label2"
        '
        'Label145
        '
        Me.Label145.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label145.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label145.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label145.Location = New System.Drawing.Point(449, 0)
        Me.Label145.Name = "Label145"
        Me.Label145.Size = New System.Drawing.Size(1, 170)
        Me.Label145.TabIndex = 67
        Me.Label145.Text = "label2"
        '
        'Panel12
        '
        Me.Panel12.BackColor = System.Drawing.Color.White
        Me.Panel12.Controls.Add(Me.C1SmartDiagnosisSendTask)
        Me.Panel12.Controls.Add(Me.pnlSmartDiagnosis_Select)
        Me.Panel12.Controls.Add(Me.chklst_SmartDiagnosisSendTask)
        Me.Panel12.Controls.Add(Me.CheckBox10)
        Me.Panel12.Controls.Add(Me.Label129)
        Me.Panel12.Controls.Add(Me.Label128)
        Me.Panel12.Controls.Add(Me.Panel13)
        Me.Panel12.Controls.Add(Me.Label130)
        Me.Panel12.Controls.Add(Me.Label131)
        Me.Panel12.Controls.Add(Me.Label132)
        Me.Panel12.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel12.Location = New System.Drawing.Point(0, 24)
        Me.Panel12.Name = "Panel12"
        Me.Panel12.Size = New System.Drawing.Size(450, 170)
        Me.Panel12.TabIndex = 82
        '
        'C1SmartDiagnosisSendTask
        '
        Me.C1SmartDiagnosisSendTask.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.C1SmartDiagnosisSendTask.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None
        Me.C1SmartDiagnosisSendTask.BackColor = System.Drawing.Color.FromArgb(CType(CType(240,Byte),Integer), CType(CType(247,Byte),Integer), CType(CType(255,Byte),Integer))
        Me.C1SmartDiagnosisSendTask.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.C1SmartDiagnosisSendTask.ColumnInfo = "1,0,0,0,0,95,Columns:0{StyleFixed:""TextAlign:CenterCenter;ImageAlign:CenterCenter"& _ 
    ";"";}"&Global.Microsoft.VisualBasic.ChrW(9)
        Me.C1SmartDiagnosisSendTask.Dock = System.Windows.Forms.DockStyle.Fill
        Me.C1SmartDiagnosisSendTask.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.C1SmartDiagnosisSendTask.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.C1SmartDiagnosisSendTask.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross
        Me.C1SmartDiagnosisSendTask.Location = New System.Drawing.Point(1, 50)
        Me.C1SmartDiagnosisSendTask.Name = "C1SmartDiagnosisSendTask"
        Me.C1SmartDiagnosisSendTask.Rows.Count = 1
        Me.C1SmartDiagnosisSendTask.Rows.DefaultSize = 19
        Me.C1SmartDiagnosisSendTask.Rows.Fixed = 0
        Me.C1SmartDiagnosisSendTask.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.C1SmartDiagnosisSendTask.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        Me.C1SmartDiagnosisSendTask.ShowCellLabels = true
        Me.C1SmartDiagnosisSendTask.ShowSortPosition = C1.Win.C1FlexGrid.ShowSortPositionEnum.None
        Me.C1SmartDiagnosisSendTask.Size = New System.Drawing.Size(448, 119)
        Me.C1SmartDiagnosisSendTask.StyleInfo = resources.GetString("C1SmartDiagnosisSendTask.StyleInfo")
        Me.C1SmartDiagnosisSendTask.TabIndex = 72
        Me.C1SmartDiagnosisSendTask.Tree.NodeImageCollapsed = CType(resources.GetObject("C1SmartDiagnosisSendTask.Tree.NodeImageCollapsed"),System.Drawing.Image)
        Me.C1SmartDiagnosisSendTask.Tree.NodeImageExpanded = CType(resources.GetObject("C1SmartDiagnosisSendTask.Tree.NodeImageExpanded"),System.Drawing.Image)
        '
        'pnlSmartDiagnosis_Select
        '
        Me.pnlSmartDiagnosis_Select.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.pnlSmartDiagnosis_Select.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlSmartDiagnosis_Select.Controls.Add(Me.btnSmartDiagnosisView_All)
        Me.pnlSmartDiagnosis_Select.Controls.Add(Me.btnSmartDiagnosisView_Cancel)
        Me.pnlSmartDiagnosis_Select.Controls.Add(Me.btnSmartDiagnosisSelect_Cancel)
        Me.pnlSmartDiagnosis_Select.Controls.Add(Me.btnSmartDiagnosisSelect_All)
        Me.pnlSmartDiagnosis_Select.Controls.Add(Me.Label149)
        Me.pnlSmartDiagnosis_Select.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlSmartDiagnosis_Select.Location = New System.Drawing.Point(1, 26)
        Me.pnlSmartDiagnosis_Select.Name = "pnlSmartDiagnosis_Select"
        Me.pnlSmartDiagnosis_Select.Size = New System.Drawing.Size(448, 24)
        Me.pnlSmartDiagnosis_Select.TabIndex = 73
        '
        'btnSmartDiagnosisView_All
        '
        Me.btnSmartDiagnosisView_All.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.btnSmartDiagnosisView_All.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnSmartDiagnosisView_All.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnSmartDiagnosisView_All.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.btnSmartDiagnosisView_All.FlatAppearance.BorderSize = 0
        Me.btnSmartDiagnosisView_All.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnSmartDiagnosisView_All.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnSmartDiagnosisView_All.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSmartDiagnosisView_All.Image = CType(resources.GetObject("btnSmartDiagnosisView_All.Image"),System.Drawing.Image)
        Me.btnSmartDiagnosisView_All.Location = New System.Drawing.Point(404, 0)
        Me.btnSmartDiagnosisView_All.Name = "btnSmartDiagnosisView_All"
        Me.btnSmartDiagnosisView_All.Size = New System.Drawing.Size(22, 23)
        Me.btnSmartDiagnosisView_All.TabIndex = 7
        Me.btnSmartDiagnosisView_All.Tag = "SelectAll"
        Me.btnSmartDiagnosisView_All.UseVisualStyleBackColor = true
        '
        'btnSmartDiagnosisView_Cancel
        '
        Me.btnSmartDiagnosisView_Cancel.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.btnSmartDiagnosisView_Cancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnSmartDiagnosisView_Cancel.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnSmartDiagnosisView_Cancel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.btnSmartDiagnosisView_Cancel.FlatAppearance.BorderSize = 0
        Me.btnSmartDiagnosisView_Cancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnSmartDiagnosisView_Cancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnSmartDiagnosisView_Cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSmartDiagnosisView_Cancel.Image = CType(resources.GetObject("btnSmartDiagnosisView_Cancel.Image"),System.Drawing.Image)
        Me.btnSmartDiagnosisView_Cancel.Location = New System.Drawing.Point(426, 0)
        Me.btnSmartDiagnosisView_Cancel.Name = "btnSmartDiagnosisView_Cancel"
        Me.btnSmartDiagnosisView_Cancel.Size = New System.Drawing.Size(22, 23)
        Me.btnSmartDiagnosisView_Cancel.TabIndex = 6
        Me.btnSmartDiagnosisView_Cancel.Tag = "ClearAll"
        Me.btnSmartDiagnosisView_Cancel.UseVisualStyleBackColor = true
        '
        'btnSmartDiagnosisSelect_Cancel
        '
        Me.btnSmartDiagnosisSelect_Cancel.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.btnSmartDiagnosisSelect_Cancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnSmartDiagnosisSelect_Cancel.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnSmartDiagnosisSelect_Cancel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.btnSmartDiagnosisSelect_Cancel.FlatAppearance.BorderSize = 0
        Me.btnSmartDiagnosisSelect_Cancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnSmartDiagnosisSelect_Cancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnSmartDiagnosisSelect_Cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSmartDiagnosisSelect_Cancel.Image = CType(resources.GetObject("btnSmartDiagnosisSelect_Cancel.Image"),System.Drawing.Image)
        Me.btnSmartDiagnosisSelect_Cancel.Location = New System.Drawing.Point(22, 0)
        Me.btnSmartDiagnosisSelect_Cancel.Name = "btnSmartDiagnosisSelect_Cancel"
        Me.btnSmartDiagnosisSelect_Cancel.Size = New System.Drawing.Size(22, 23)
        Me.btnSmartDiagnosisSelect_Cancel.TabIndex = 5
        Me.btnSmartDiagnosisSelect_Cancel.Tag = "ClearAll"
        Me.btnSmartDiagnosisSelect_Cancel.UseVisualStyleBackColor = true
        '
        'btnSmartDiagnosisSelect_All
        '
        Me.btnSmartDiagnosisSelect_All.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.btnSmartDiagnosisSelect_All.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnSmartDiagnosisSelect_All.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnSmartDiagnosisSelect_All.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.btnSmartDiagnosisSelect_All.FlatAppearance.BorderSize = 0
        Me.btnSmartDiagnosisSelect_All.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnSmartDiagnosisSelect_All.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnSmartDiagnosisSelect_All.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSmartDiagnosisSelect_All.Image = CType(resources.GetObject("btnSmartDiagnosisSelect_All.Image"),System.Drawing.Image)
        Me.btnSmartDiagnosisSelect_All.Location = New System.Drawing.Point(0, 0)
        Me.btnSmartDiagnosisSelect_All.Name = "btnSmartDiagnosisSelect_All"
        Me.btnSmartDiagnosisSelect_All.Size = New System.Drawing.Size(22, 23)
        Me.btnSmartDiagnosisSelect_All.TabIndex = 4
        Me.btnSmartDiagnosisSelect_All.Tag = "SelectAll"
        Me.btnSmartDiagnosisSelect_All.UseVisualStyleBackColor = true
        '
        'Label149
        '
        Me.Label149.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label149.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label149.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label149.Location = New System.Drawing.Point(0, 23)
        Me.Label149.Name = "Label149"
        Me.Label149.Size = New System.Drawing.Size(448, 1)
        Me.Label149.TabIndex = 65
        Me.Label149.Text = "label2"
        '
        'chklst_SmartDiagnosisSendTask
        '
        Me.chklst_SmartDiagnosisSendTask.BackColor = System.Drawing.Color.White
        Me.chklst_SmartDiagnosisSendTask.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.chklst_SmartDiagnosisSendTask.CheckOnClick = true
        Me.chklst_SmartDiagnosisSendTask.Dock = System.Windows.Forms.DockStyle.Fill
        Me.chklst_SmartDiagnosisSendTask.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chklst_SmartDiagnosisSendTask.FormattingEnabled = true
        Me.chklst_SmartDiagnosisSendTask.Items.AddRange(New Object() {"Lab Orders", "Order Templates", "Flowsheet", "Drugs"})
        Me.chklst_SmartDiagnosisSendTask.Location = New System.Drawing.Point(1, 26)
        Me.chklst_SmartDiagnosisSendTask.Name = "chklst_SmartDiagnosisSendTask"
        Me.chklst_SmartDiagnosisSendTask.Size = New System.Drawing.Size(448, 143)
        Me.chklst_SmartDiagnosisSendTask.TabIndex = 0
        Me.chklst_SmartDiagnosisSendTask.Visible = false
        '
        'CheckBox10
        '
        Me.CheckBox10.AutoSize = true
        Me.CheckBox10.Location = New System.Drawing.Point(66, 29)
        Me.CheckBox10.Name = "CheckBox10"
        Me.CheckBox10.Size = New System.Drawing.Size(15, 14)
        Me.CheckBox10.TabIndex = 5
        Me.CheckBox10.UseVisualStyleBackColor = true
        '
        'Label129
        '
        Me.Label129.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label129.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label129.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label129.Location = New System.Drawing.Point(1, 25)
        Me.Label129.Name = "Label129"
        Me.Label129.Size = New System.Drawing.Size(448, 1)
        Me.Label129.TabIndex = 64
        Me.Label129.Text = "label2"
        '
        'Label128
        '
        Me.Label128.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label128.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label128.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label128.Location = New System.Drawing.Point(1, 169)
        Me.Label128.Name = "Label128"
        Me.Label128.Size = New System.Drawing.Size(448, 1)
        Me.Label128.TabIndex = 63
        Me.Label128.Text = "label2"
        '
        'Panel13
        '
        Me.Panel13.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.Panel13.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel13.Controls.Add(Me.chk_SmartDiagnosis_SendTask)
        Me.Panel13.Controls.Add(Me.Label138)
        Me.Panel13.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel13.Location = New System.Drawing.Point(1, 1)
        Me.Panel13.Name = "Panel13"
        Me.Panel13.Size = New System.Drawing.Size(448, 24)
        Me.Panel13.TabIndex = 0
        '
        'chk_SmartDiagnosis_SendTask
        '
        Me.chk_SmartDiagnosis_SendTask.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.chk_SmartDiagnosis_SendTask.AutoSize = true
        Me.chk_SmartDiagnosis_SendTask.Location = New System.Drawing.Point(426, 6)
        Me.chk_SmartDiagnosis_SendTask.Name = "chk_SmartDiagnosis_SendTask"
        Me.chk_SmartDiagnosis_SendTask.Size = New System.Drawing.Size(15, 14)
        Me.chk_SmartDiagnosis_SendTask.TabIndex = 5
        Me.chk_SmartDiagnosis_SendTask.UseVisualStyleBackColor = true
        Me.chk_SmartDiagnosis_SendTask.Visible = false
        '
        'Label138
        '
        Me.Label138.BackColor = System.Drawing.Color.Transparent
        Me.Label138.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label138.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label138.Location = New System.Drawing.Point(0, 0)
        Me.Label138.Name = "Label138"
        Me.Label138.Size = New System.Drawing.Size(448, 24)
        Me.Label138.TabIndex = 67
        Me.Label138.Text = "   Diagnosis "
        Me.Label138.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label130
        '
        Me.Label130.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label130.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label130.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label130.Location = New System.Drawing.Point(1, 0)
        Me.Label130.Name = "Label130"
        Me.Label130.Size = New System.Drawing.Size(448, 1)
        Me.Label130.TabIndex = 65
        Me.Label130.Text = "label2"
        '
        'Label131
        '
        Me.Label131.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label131.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label131.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label131.Location = New System.Drawing.Point(0, 0)
        Me.Label131.Name = "Label131"
        Me.Label131.Size = New System.Drawing.Size(1, 170)
        Me.Label131.TabIndex = 66
        Me.Label131.Text = "label2"
        '
        'Label132
        '
        Me.Label132.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label132.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label132.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label132.Location = New System.Drawing.Point(449, 0)
        Me.Label132.Name = "Label132"
        Me.Label132.Size = New System.Drawing.Size(1, 170)
        Me.Label132.TabIndex = 67
        Me.Label132.Text = "label2"
        '
        'Panel27
        '
        Me.Panel27.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Blue2007
        Me.Panel27.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel27.Controls.Add(Me.Label190)
        Me.Panel27.Controls.Add(Me.Label192)
        Me.Panel27.Controls.Add(Me.Label193)
        Me.Panel27.Controls.Add(Me.Label194)
        Me.Panel27.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel27.Location = New System.Drawing.Point(0, 0)
        Me.Panel27.Name = "Panel27"
        Me.Panel27.Size = New System.Drawing.Size(450, 24)
        Me.Panel27.TabIndex = 90
        '
        'Label190
        '
        Me.Label190.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label190.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label190.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label190.Location = New System.Drawing.Point(1, 0)
        Me.Label190.Name = "Label190"
        Me.Label190.Size = New System.Drawing.Size(448, 1)
        Me.Label190.TabIndex = 71
        Me.Label190.Text = "label2"
        '
        'Label192
        '
        Me.Label192.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label192.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label192.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label192.Location = New System.Drawing.Point(449, 0)
        Me.Label192.Name = "Label192"
        Me.Label192.Size = New System.Drawing.Size(1, 24)
        Me.Label192.TabIndex = 69
        Me.Label192.Text = "label2"
        '
        'Label193
        '
        Me.Label193.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label193.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label193.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label193.Location = New System.Drawing.Point(0, 0)
        Me.Label193.Name = "Label193"
        Me.Label193.Size = New System.Drawing.Size(1, 24)
        Me.Label193.TabIndex = 68
        Me.Label193.Text = "label2"
        '
        'Label194
        '
        Me.Label194.BackColor = System.Drawing.Color.Transparent
        Me.Label194.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label194.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label194.ForeColor = System.Drawing.Color.White
        Me.Label194.Location = New System.Drawing.Point(0, 0)
        Me.Label194.Name = "Label194"
        Me.Label194.Size = New System.Drawing.Size(450, 24)
        Me.Label194.TabIndex = 67
        Me.Label194.Text = "   Send Task "
        Me.Label194.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Panel11
        '
        Me.Panel11.Controls.Add(Me.Label178)
        Me.Panel11.Controls.Add(Me.Label177)
        Me.Panel11.Controls.Add(Me.Label174)
        Me.Panel11.Controls.Add(Me.Label173)
        Me.Panel11.Controls.Add(Me.Label155)
        Me.Panel11.Location = New System.Drawing.Point(0, 517)
        Me.Panel11.Name = "Panel11"
        Me.Panel11.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.Panel11.Size = New System.Drawing.Size(682, 51)
        Me.Panel11.TabIndex = 93
        Me.Panel11.Visible = false
        '
        'Label178
        '
        Me.Label178.AutoSize = true
        Me.Label178.BackColor = System.Drawing.Color.Transparent
        Me.Label178.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label178.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label178.Location = New System.Drawing.Point(7, 8)
        Me.Label178.Name = "Label178"
        Me.Label178.Size = New System.Drawing.Size(56, 14)
        Me.Label178.TabIndex = 71
        Me.Label178.Text = "   Note :"
        Me.Label178.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label177
        '
        Me.Label177.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label177.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label177.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label177.Location = New System.Drawing.Point(1, 50)
        Me.Label177.Name = "Label177"
        Me.Label177.Size = New System.Drawing.Size(680, 1)
        Me.Label177.TabIndex = 70
        Me.Label177.Text = "label2"
        '
        'Label174
        '
        Me.Label174.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label174.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label174.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label174.Location = New System.Drawing.Point(1, 3)
        Me.Label174.Name = "Label174"
        Me.Label174.Size = New System.Drawing.Size(680, 1)
        Me.Label174.TabIndex = 69
        Me.Label174.Text = "label2"
        '
        'Label173
        '
        Me.Label173.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label173.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label173.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label173.Location = New System.Drawing.Point(681, 3)
        Me.Label173.Name = "Label173"
        Me.Label173.Size = New System.Drawing.Size(1, 48)
        Me.Label173.TabIndex = 68
        Me.Label173.Text = "label2"
        '
        'Label155
        '
        Me.Label155.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label155.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label155.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label155.Location = New System.Drawing.Point(0, 3)
        Me.Label155.Name = "Label155"
        Me.Label155.Size = New System.Drawing.Size(1, 48)
        Me.Label155.TabIndex = 67
        Me.Label155.Text = "label2"
        '
        'CheckBox9
        '
        Me.CheckBox9.AutoSize = true
        Me.CheckBox9.Location = New System.Drawing.Point(29, 392)
        Me.CheckBox9.Name = "CheckBox9"
        Me.CheckBox9.Size = New System.Drawing.Size(15, 14)
        Me.CheckBox9.TabIndex = 5
        Me.CheckBox9.UseVisualStyleBackColor = true
        '
        'pnl_tlspTOP
        '
        Me.pnl_tlspTOP.BackColor = System.Drawing.Color.FromArgb(CType(CType(181,Byte),Integer), CType(CType(216,Byte),Integer), CType(CType(242,Byte),Integer))
        Me.pnl_tlspTOP.Controls.Add(Me.tlsp_Settings)
        Me.pnl_tlspTOP.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_tlspTOP.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold)
        Me.pnl_tlspTOP.Location = New System.Drawing.Point(0, 0)
        Me.pnl_tlspTOP.Name = "pnl_tlspTOP"
        Me.pnl_tlspTOP.Size = New System.Drawing.Size(692, 55)
        Me.pnl_tlspTOP.TabIndex = 7
        '
        'tlsp_Settings
        '
        Me.tlsp_Settings.BackColor = System.Drawing.Color.Transparent
        Me.tlsp_Settings.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tlsp_Settings.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlsp_Settings.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.tlsp_Settings.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tlsp_Settings.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_btnOk, Me.ts_btnCancel})
        Me.tlsp_Settings.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tlsp_Settings.Location = New System.Drawing.Point(0, 0)
        Me.tlsp_Settings.Name = "tlsp_Settings"
        Me.tlsp_Settings.Size = New System.Drawing.Size(692, 53)
        Me.tlsp_Settings.TabIndex = 0
        Me.tlsp_Settings.Text = "toolStrip1"
        '
        'ts_btnOk
        '
        Me.ts_btnOk.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.ts_btnOk.Image = CType(resources.GetObject("ts_btnOk.Image"),System.Drawing.Image)
        Me.ts_btnOk.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnOk.Name = "ts_btnOk"
        Me.ts_btnOk.Size = New System.Drawing.Size(66, 50)
        Me.ts_btnOk.Tag = "OK"
        Me.ts_btnOk.Text = "&Save&&Cls"
        Me.ts_btnOk.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ts_btnOk.ToolTipText = "Save and Close"
        '
        'ts_btnCancel
        '
        Me.ts_btnCancel.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.ts_btnCancel.Image = CType(resources.GetObject("ts_btnCancel.Image"),System.Drawing.Image)
        Me.ts_btnCancel.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnCancel.Name = "ts_btnCancel"
        Me.ts_btnCancel.Size = New System.Drawing.Size(43, 50)
        Me.ts_btnCancel.Tag = "Cancel"
        Me.ts_btnCancel.Text = "&Close"
        Me.ts_btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'Panel4
        '
        Me.Panel4.Location = New System.Drawing.Point(0, 0)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(200, 100)
        Me.Panel4.TabIndex = 0
        '
        'btnSelectAllOrder
        '
        Me.btnSelectAllOrder.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnSelectAllOrder.FlatAppearance.BorderSize = 0
        Me.btnSelectAllOrder.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnSelectAllOrder.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnSelectAllOrder.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSelectAllOrder.Image = CType(resources.GetObject("btnSelectAllOrder.Image"),System.Drawing.Image)
        Me.btnSelectAllOrder.Location = New System.Drawing.Point(-9, 0)
        Me.btnSelectAllOrder.Name = "btnSelectAllOrder"
        Me.btnSelectAllOrder.Size = New System.Drawing.Size(29, 10)
        Me.btnSelectAllOrder.TabIndex = 3
        Me.btnSelectAllOrder.Tag = "SelectAll"
        Me.btnSelectAllOrder.UseVisualStyleBackColor = true
        '
        'Panel3
        '
        Me.Panel3.Location = New System.Drawing.Point(0, 0)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(200, 100)
        Me.Panel3.TabIndex = 0
        '
        'btnSelectALLCPT
        '
        Me.btnSelectALLCPT.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnSelectALLCPT.FlatAppearance.BorderSize = 0
        Me.btnSelectALLCPT.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnSelectALLCPT.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnSelectALLCPT.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSelectALLCPT.Image = CType(resources.GetObject("btnSelectALLCPT.Image"),System.Drawing.Image)
        Me.btnSelectALLCPT.Location = New System.Drawing.Point(-2, 0)
        Me.btnSelectALLCPT.Name = "btnSelectALLCPT"
        Me.btnSelectALLCPT.Size = New System.Drawing.Size(29, 10)
        Me.btnSelectALLCPT.TabIndex = 3
        Me.btnSelectALLCPT.Tag = "SelectAll"
        Me.btnSelectALLCPT.UseVisualStyleBackColor = true
        '
        'Label108
        '
        Me.Label108.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label108.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label108.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label108.Location = New System.Drawing.Point(4, 564)
        Me.Label108.Name = "Label108"
        Me.Label108.Size = New System.Drawing.Size(674, 1)
        Me.Label108.TabIndex = 66
        Me.Label108.Text = "label4"
        '
        'Label121
        '
        Me.Label121.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label121.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label121.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label121.Location = New System.Drawing.Point(4, 3)
        Me.Label121.Name = "Label121"
        Me.Label121.Size = New System.Drawing.Size(674, 1)
        Me.Label121.TabIndex = 65
        Me.Label121.Text = "label4"
        '
        'Label122
        '
        Me.Label122.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label122.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label122.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label122.Location = New System.Drawing.Point(678, 3)
        Me.Label122.Name = "Label122"
        Me.Label122.Size = New System.Drawing.Size(1, 562)
        Me.Label122.TabIndex = 64
        Me.Label122.Text = "label4"
        '
        'Label123
        '
        Me.Label123.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label123.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label123.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label123.Location = New System.Drawing.Point(3, 3)
        Me.Label123.Name = "Label123"
        Me.Label123.Size = New System.Drawing.Size(1, 562)
        Me.Label123.TabIndex = 63
        Me.Label123.Text = "label4"
        '
        'CheckBox4
        '
        Me.CheckBox4.AutoSize = true
        Me.CheckBox4.Location = New System.Drawing.Point(29, 392)
        Me.CheckBox4.Name = "CheckBox4"
        Me.CheckBox4.Size = New System.Drawing.Size(15, 14)
        Me.CheckBox4.TabIndex = 5
        Me.CheckBox4.UseVisualStyleBackColor = true
        '
        'Panel2
        '
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(200, 100)
        Me.Panel2.TabIndex = 0
        '
        'GroupBox16
        '
        Me.GroupBox16.Controls.Add(Me.CheckBox1)
        Me.GroupBox16.Controls.Add(Me.CheckedListBox1)
        Me.GroupBox16.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.GroupBox16.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.GroupBox16.Location = New System.Drawing.Point(345, 388)
        Me.GroupBox16.Name = "GroupBox16"
        Me.GroupBox16.Size = New System.Drawing.Size(299, 162)
        Me.GroupBox16.TabIndex = 67
        Me.GroupBox16.TabStop = false
        Me.GroupBox16.Text = "Smart Orders Send Task"
        Me.GroupBox16.Visible = false
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = true
        Me.CheckBox1.Location = New System.Drawing.Point(32, 22)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(15, 14)
        Me.CheckBox1.TabIndex = 5
        Me.CheckBox1.UseVisualStyleBackColor = true
        '
        'CheckedListBox1
        '
        Me.CheckedListBox1.CheckOnClick = true
        Me.CheckedListBox1.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.CheckedListBox1.FormattingEnabled = true
        Me.CheckedListBox1.Items.AddRange(New Object() {"Lab Orders", "Order Templates", "Flowsheet", "Drugs"})
        Me.CheckedListBox1.Location = New System.Drawing.Point(29, 41)
        Me.CheckedListBox1.Name = "CheckedListBox1"
        Me.CheckedListBox1.Size = New System.Drawing.Size(225, 72)
        Me.CheckedListBox1.TabIndex = 0
        '
        'GroupBox17
        '
        Me.GroupBox17.Controls.Add(Me.CheckBox2)
        Me.GroupBox17.Controls.Add(Me.Panel5)
        Me.GroupBox17.Controls.Add(Me.Button3)
        Me.GroupBox17.Controls.Add(Me.CheckedListBox2)
        Me.GroupBox17.Controls.Add(Me.Button4)
        Me.GroupBox17.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.GroupBox17.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.GroupBox17.Location = New System.Drawing.Point(24, 388)
        Me.GroupBox17.Name = "GroupBox17"
        Me.GroupBox17.Size = New System.Drawing.Size(299, 162)
        Me.GroupBox17.TabIndex = 67
        Me.GroupBox17.TabStop = false
        Me.GroupBox17.Text = "Smart Orders"
        '
        'CheckBox2
        '
        Me.CheckBox2.AutoSize = true
        Me.CheckBox2.Location = New System.Drawing.Point(32, 30)
        Me.CheckBox2.Name = "CheckBox2"
        Me.CheckBox2.Size = New System.Drawing.Size(15, 14)
        Me.CheckBox2.TabIndex = 5
        Me.CheckBox2.UseVisualStyleBackColor = true
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.Button1)
        Me.Panel5.Controls.Add(Me.Button2)
        Me.Panel5.Location = New System.Drawing.Point(6, 137)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(20, 10)
        Me.Panel5.TabIndex = 4
        Me.Panel5.Visible = false
        '
        'Button1
        '
        Me.Button1.Dock = System.Windows.Forms.DockStyle.Left
        Me.Button1.FlatAppearance.BorderSize = 0
        Me.Button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.Button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.Image = CType(resources.GetObject("Button1.Image"),System.Drawing.Image)
        Me.Button1.Location = New System.Drawing.Point(0, 0)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(29, 10)
        Me.Button1.TabIndex = 4
        Me.Button1.Tag = "ClearAll"
        Me.Button1.UseVisualStyleBackColor = true
        '
        'Button2
        '
        Me.Button2.Dock = System.Windows.Forms.DockStyle.Right
        Me.Button2.FlatAppearance.BorderSize = 0
        Me.Button2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.Button2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.Button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button2.Image = CType(resources.GetObject("Button2.Image"),System.Drawing.Image)
        Me.Button2.Location = New System.Drawing.Point(-9, 0)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(29, 10)
        Me.Button2.TabIndex = 3
        Me.Button2.Tag = "SelectAll"
        Me.Button2.UseVisualStyleBackColor = true
        '
        'Button3
        '
        Me.Button3.BackgroundImage = CType(resources.GetObject("Button3.BackgroundImage"),System.Drawing.Image)
        Me.Button3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button3.Image = CType(resources.GetObject("Button3.Image"),System.Drawing.Image)
        Me.Button3.Location = New System.Drawing.Point(261, 74)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(22, 22)
        Me.Button3.TabIndex = 1
        Me.Button3.UseVisualStyleBackColor = true
        '
        'CheckedListBox2
        '
        Me.CheckedListBox2.CheckOnClick = true
        Me.CheckedListBox2.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.CheckedListBox2.FormattingEnabled = true
        Me.CheckedListBox2.Items.AddRange(New Object() {"Lab Orders", "Order Templates", "Referral Letter", "Drugs", "Flowsheet"})
        Me.CheckedListBox2.Location = New System.Drawing.Point(29, 50)
        Me.CheckedListBox2.Name = "CheckedListBox2"
        Me.CheckedListBox2.Size = New System.Drawing.Size(225, 72)
        Me.CheckedListBox2.TabIndex = 0
        '
        'Button4
        '
        Me.Button4.BackgroundImage = CType(resources.GetObject("Button4.BackgroundImage"),System.Drawing.Image)
        Me.Button4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button4.Image = CType(resources.GetObject("Button4.Image"),System.Drawing.Image)
        Me.Button4.Location = New System.Drawing.Point(261, 102)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(22, 22)
        Me.Button4.TabIndex = 2
        Me.Button4.UseVisualStyleBackColor = true
        '
        'GroupBox18
        '
        Me.GroupBox18.Controls.Add(Me.CheckBox3)
        Me.GroupBox18.Controls.Add(Me.CheckedListBox3)
        Me.GroupBox18.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.GroupBox18.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.GroupBox18.Location = New System.Drawing.Point(345, 208)
        Me.GroupBox18.Name = "GroupBox18"
        Me.GroupBox18.Size = New System.Drawing.Size(299, 174)
        Me.GroupBox18.TabIndex = 67
        Me.GroupBox18.TabStop = false
        Me.GroupBox18.Text = "Smart Treatment Send Task"
        Me.GroupBox18.Visible = false
        '
        'CheckBox3
        '
        Me.CheckBox3.AutoSize = true
        Me.CheckBox3.Location = New System.Drawing.Point(32, 37)
        Me.CheckBox3.Name = "CheckBox3"
        Me.CheckBox3.Size = New System.Drawing.Size(15, 14)
        Me.CheckBox3.TabIndex = 5
        Me.CheckBox3.UseVisualStyleBackColor = true
        '
        'CheckedListBox3
        '
        Me.CheckedListBox3.CheckOnClick = true
        Me.CheckedListBox3.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.CheckedListBox3.FormattingEnabled = true
        Me.CheckedListBox3.Items.AddRange(New Object() {"Lab Orders", "Order Templates", "Flowsheet", "Drugs"})
        Me.CheckedListBox3.Location = New System.Drawing.Point(29, 57)
        Me.CheckedListBox3.Name = "CheckedListBox3"
        Me.CheckedListBox3.Size = New System.Drawing.Size(225, 72)
        Me.CheckedListBox3.TabIndex = 0
        '
        'GroupBox19
        '
        Me.GroupBox19.Controls.Add(Me.CheckBox5)
        Me.GroupBox19.Controls.Add(Me.Panel7)
        Me.GroupBox19.Controls.Add(Me.CheckedListBox4)
        Me.GroupBox19.Controls.Add(Me.Button7)
        Me.GroupBox19.Controls.Add(Me.Button8)
        Me.GroupBox19.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.GroupBox19.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.GroupBox19.Location = New System.Drawing.Point(24, 208)
        Me.GroupBox19.Name = "GroupBox19"
        Me.GroupBox19.Size = New System.Drawing.Size(299, 174)
        Me.GroupBox19.TabIndex = 67
        Me.GroupBox19.TabStop = false
        Me.GroupBox19.Text = "Smart Treatment"
        '
        'CheckBox5
        '
        Me.CheckBox5.AutoSize = true
        Me.CheckBox5.Location = New System.Drawing.Point(32, 31)
        Me.CheckBox5.Name = "CheckBox5"
        Me.CheckBox5.Size = New System.Drawing.Size(15, 14)
        Me.CheckBox5.TabIndex = 5
        Me.CheckBox5.UseVisualStyleBackColor = true
        '
        'Panel7
        '
        Me.Panel7.Controls.Add(Me.Button5)
        Me.Panel7.Controls.Add(Me.Button6)
        Me.Panel7.Location = New System.Drawing.Point(13, 158)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(27, 10)
        Me.Panel7.TabIndex = 4
        Me.Panel7.Visible = false
        '
        'Button5
        '
        Me.Button5.Dock = System.Windows.Forms.DockStyle.Left
        Me.Button5.FlatAppearance.BorderSize = 0
        Me.Button5.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.Button5.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.Button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button5.Image = CType(resources.GetObject("Button5.Image"),System.Drawing.Image)
        Me.Button5.Location = New System.Drawing.Point(0, 0)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(29, 10)
        Me.Button5.TabIndex = 4
        Me.Button5.Tag = "ClearAll"
        Me.Button5.UseVisualStyleBackColor = true
        '
        'Button6
        '
        Me.Button6.Dock = System.Windows.Forms.DockStyle.Right
        Me.Button6.FlatAppearance.BorderSize = 0
        Me.Button6.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.Button6.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.Button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button6.Image = CType(resources.GetObject("Button6.Image"),System.Drawing.Image)
        Me.Button6.Location = New System.Drawing.Point(-2, 0)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(29, 10)
        Me.Button6.TabIndex = 3
        Me.Button6.Tag = "SelectAll"
        Me.Button6.UseVisualStyleBackColor = true
        '
        'CheckedListBox4
        '
        Me.CheckedListBox4.CheckOnClick = true
        Me.CheckedListBox4.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.CheckedListBox4.FormattingEnabled = true
        Me.CheckedListBox4.Items.AddRange(New Object() {"ICD9", "Drugs", "Patient Education", "Tags", "Flowsheet", "Lab Orders", "Order Templates", "Referral Letter"})
        Me.CheckedListBox4.Location = New System.Drawing.Point(29, 50)
        Me.CheckedListBox4.Name = "CheckedListBox4"
        Me.CheckedListBox4.Size = New System.Drawing.Size(225, 72)
        Me.CheckedListBox4.TabIndex = 0
        '
        'Button7
        '
        Me.Button7.BackgroundImage = CType(resources.GetObject("Button7.BackgroundImage"),System.Drawing.Image)
        Me.Button7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button7.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button7.Image = CType(resources.GetObject("Button7.Image"),System.Drawing.Image)
        Me.Button7.Location = New System.Drawing.Point(261, 110)
        Me.Button7.Name = "Button7"
        Me.Button7.Size = New System.Drawing.Size(22, 22)
        Me.Button7.TabIndex = 2
        Me.Button7.UseVisualStyleBackColor = true
        '
        'Button8
        '
        Me.Button8.BackgroundImage = CType(resources.GetObject("Button8.BackgroundImage"),System.Drawing.Image)
        Me.Button8.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button8.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button8.Image = CType(resources.GetObject("Button8.Image"),System.Drawing.Image)
        Me.Button8.Location = New System.Drawing.Point(261, 82)
        Me.Button8.Name = "Button8"
        Me.Button8.Size = New System.Drawing.Size(22, 22)
        Me.Button8.TabIndex = 1
        Me.Button8.UseVisualStyleBackColor = true
        '
        'Label124
        '
        Me.Label124.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label124.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label124.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label124.Location = New System.Drawing.Point(4, 564)
        Me.Label124.Name = "Label124"
        Me.Label124.Size = New System.Drawing.Size(674, 1)
        Me.Label124.TabIndex = 66
        Me.Label124.Text = "label4"
        '
        'Label125
        '
        Me.Label125.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label125.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label125.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label125.Location = New System.Drawing.Point(4, 3)
        Me.Label125.Name = "Label125"
        Me.Label125.Size = New System.Drawing.Size(674, 1)
        Me.Label125.TabIndex = 65
        Me.Label125.Text = "label4"
        '
        'Label126
        '
        Me.Label126.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label126.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label126.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label126.Location = New System.Drawing.Point(678, 3)
        Me.Label126.Name = "Label126"
        Me.Label126.Size = New System.Drawing.Size(1, 562)
        Me.Label126.TabIndex = 64
        Me.Label126.Text = "label4"
        '
        'Label127
        '
        Me.Label127.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label127.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label127.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label127.Location = New System.Drawing.Point(3, 3)
        Me.Label127.Name = "Label127"
        Me.Label127.Size = New System.Drawing.Size(1, 562)
        Me.Label127.TabIndex = 63
        Me.Label127.Text = "label4"
        '
        'GroupBox20
        '
        Me.GroupBox20.Controls.Add(Me.CheckBox6)
        Me.GroupBox20.Controls.Add(Me.CheckBox7)
        Me.GroupBox20.Controls.Add(Me.CheckedListBox5)
        Me.GroupBox20.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.GroupBox20.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.GroupBox20.Location = New System.Drawing.Point(345, 17)
        Me.GroupBox20.Name = "GroupBox20"
        Me.GroupBox20.Size = New System.Drawing.Size(299, 179)
        Me.GroupBox20.TabIndex = 4
        Me.GroupBox20.TabStop = false
        Me.GroupBox20.Text = "Smart Diagnosis Send Task"
        Me.GroupBox20.Visible = false
        '
        'CheckBox6
        '
        Me.CheckBox6.AutoSize = true
        Me.CheckBox6.Location = New System.Drawing.Point(31, 34)
        Me.CheckBox6.Name = "CheckBox6"
        Me.CheckBox6.Size = New System.Drawing.Size(15, 14)
        Me.CheckBox6.TabIndex = 5
        Me.CheckBox6.UseVisualStyleBackColor = true
        '
        'CheckBox7
        '
        Me.CheckBox7.AutoSize = true
        Me.CheckBox7.Location = New System.Drawing.Point(29, 392)
        Me.CheckBox7.Name = "CheckBox7"
        Me.CheckBox7.Size = New System.Drawing.Size(15, 14)
        Me.CheckBox7.TabIndex = 5
        Me.CheckBox7.UseVisualStyleBackColor = true
        '
        'CheckedListBox5
        '
        Me.CheckedListBox5.CheckOnClick = true
        Me.CheckedListBox5.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.CheckedListBox5.FormattingEnabled = true
        Me.CheckedListBox5.Items.AddRange(New Object() {"Lab Orders", "Order Templates", "Flowsheet", "Drugs"})
        Me.CheckedListBox5.Location = New System.Drawing.Point(29, 53)
        Me.CheckedListBox5.Name = "CheckedListBox5"
        Me.CheckedListBox5.Size = New System.Drawing.Size(225, 72)
        Me.CheckedListBox5.TabIndex = 0
        '
        'GroupBox21
        '
        Me.GroupBox21.Controls.Add(Me.CheckBox8)
        Me.GroupBox21.Controls.Add(Me.Panel8)
        Me.GroupBox21.Controls.Add(Me.Button11)
        Me.GroupBox21.Controls.Add(Me.Button12)
        Me.GroupBox21.Controls.Add(Me.CheckedListBox6)
        Me.GroupBox21.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.GroupBox21.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.GroupBox21.Location = New System.Drawing.Point(24, 17)
        Me.GroupBox21.Name = "GroupBox21"
        Me.GroupBox21.Size = New System.Drawing.Size(299, 179)
        Me.GroupBox21.TabIndex = 4
        Me.GroupBox21.TabStop = false
        Me.GroupBox21.Text = "Smart Diagnosis"
        '
        'CheckBox8
        '
        Me.CheckBox8.AutoSize = true
        Me.CheckBox8.Location = New System.Drawing.Point(32, 36)
        Me.CheckBox8.Name = "CheckBox8"
        Me.CheckBox8.Size = New System.Drawing.Size(15, 14)
        Me.CheckBox8.TabIndex = 5
        Me.CheckBox8.UseVisualStyleBackColor = true
        '
        'Panel8
        '
        Me.Panel8.Controls.Add(Me.Button9)
        Me.Panel8.Controls.Add(Me.Button10)
        Me.Panel8.Location = New System.Drawing.Point(13, 163)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(62, 10)
        Me.Panel8.TabIndex = 4
        Me.Panel8.Visible = false
        '
        'Button9
        '
        Me.Button9.Dock = System.Windows.Forms.DockStyle.Left
        Me.Button9.FlatAppearance.BorderSize = 0
        Me.Button9.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.Button9.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.Button9.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button9.Image = CType(resources.GetObject("Button9.Image"),System.Drawing.Image)
        Me.Button9.Location = New System.Drawing.Point(0, 0)
        Me.Button9.Name = "Button9"
        Me.Button9.Size = New System.Drawing.Size(29, 10)
        Me.Button9.TabIndex = 4
        Me.Button9.Tag = "ClearAll"
        Me.Button9.UseVisualStyleBackColor = true
        '
        'Button10
        '
        Me.Button10.Dock = System.Windows.Forms.DockStyle.Right
        Me.Button10.FlatAppearance.BorderSize = 0
        Me.Button10.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.Button10.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.Button10.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button10.Image = CType(resources.GetObject("Button10.Image"),System.Drawing.Image)
        Me.Button10.Location = New System.Drawing.Point(33, 0)
        Me.Button10.Name = "Button10"
        Me.Button10.Size = New System.Drawing.Size(29, 10)
        Me.Button10.TabIndex = 3
        Me.Button10.Tag = "SelectAll"
        Me.Button10.UseVisualStyleBackColor = true
        '
        'Button11
        '
        Me.Button11.BackgroundImage = CType(resources.GetObject("Button11.BackgroundImage"),System.Drawing.Image)
        Me.Button11.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button11.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button11.Image = CType(resources.GetObject("Button11.Image"),System.Drawing.Image)
        Me.Button11.Location = New System.Drawing.Point(261, 87)
        Me.Button11.Name = "Button11"
        Me.Button11.Size = New System.Drawing.Size(22, 22)
        Me.Button11.TabIndex = 1
        Me.Button11.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.Button11.UseVisualStyleBackColor = true
        '
        'Button12
        '
        Me.Button12.BackgroundImage = CType(resources.GetObject("Button12.BackgroundImage"),System.Drawing.Image)
        Me.Button12.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button12.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button12.Image = CType(resources.GetObject("Button12.Image"),System.Drawing.Image)
        Me.Button12.Location = New System.Drawing.Point(261, 115)
        Me.Button12.Name = "Button12"
        Me.Button12.Size = New System.Drawing.Size(22, 22)
        Me.Button12.TabIndex = 2
        Me.Button12.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.Button12.UseVisualStyleBackColor = true
        '
        'CheckedListBox6
        '
        Me.CheckedListBox6.CheckOnClick = true
        Me.CheckedListBox6.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.CheckedListBox6.FormattingEnabled = true
        Me.CheckedListBox6.Items.AddRange(New Object() {"CPT", "Drugs", "Patient Education", "Tags", "Flowsheet", "Lab Orders", "Order Templates", "Referral Letter"})
        Me.CheckedListBox6.Location = New System.Drawing.Point(29, 55)
        Me.CheckedListBox6.Name = "CheckedListBox6"
        Me.CheckedListBox6.Size = New System.Drawing.Size(225, 72)
        Me.CheckedListBox6.TabIndex = 0
        '
        'imgTreeVIew
        '
        Me.imgTreeVIew.ImageStream = CType(resources.GetObject("imgTreeVIew.ImageStream"),System.Windows.Forms.ImageListStreamer)
        Me.imgTreeVIew.TransparentColor = System.Drawing.Color.Transparent
        Me.imgTreeVIew.Images.SetKeyName(0, "Browse.ico")
        '
        'GroupBox27
        '
        Me.GroupBox27.BackColor = System.Drawing.Color.FromArgb(CType(CType(207,Byte),Integer), CType(CType(224,Byte),Integer), CType(CType(248,Byte),Integer))
        Me.GroupBox27.Controls.Add(Me.chkGreyScreenIssue)
        Me.GroupBox27.Controls.Add(Me.TextBox6)
        Me.GroupBox27.Controls.Add(Me.Label214)
        Me.GroupBox27.Controls.Add(Me.TextBox7)
        Me.GroupBox27.Controls.Add(Me.Label215)
        Me.GroupBox27.Controls.Add(Me.TextBox8)
        Me.GroupBox27.Controls.Add(Me.Label216)
        Me.GroupBox27.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox27.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GroupBox27.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.GroupBox27.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.GroupBox27.Location = New System.Drawing.Point(6, 426)
        Me.GroupBox27.Name = "GroupBox27"
        Me.GroupBox27.Size = New System.Drawing.Size(672, 57)
        Me.GroupBox27.TabIndex = 43
        Me.GroupBox27.TabStop = false
        Me.GroupBox27.Text = "Grey Screen Issue Settings"
        '
        'chkGreyScreenIssue
        '
        Me.chkGreyScreenIssue.AutoSize = true
        Me.chkGreyScreenIssue.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkGreyScreenIssue.Location = New System.Drawing.Point(17, 25)
        Me.chkGreyScreenIssue.Name = "chkGreyScreenIssue"
        Me.chkGreyScreenIssue.Size = New System.Drawing.Size(170, 18)
        Me.chkGreyScreenIssue.TabIndex = 0
        Me.chkGreyScreenIssue.Text = "Resolve Grey Screen Issue"
        Me.chkGreyScreenIssue.UseVisualStyleBackColor = true
        '
        'TextBox6
        '
        Me.TextBox6.Enabled = false
        Me.TextBox6.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.TextBox6.Location = New System.Drawing.Point(102, 81)
        Me.TextBox6.MaxLength = 1
        Me.TextBox6.Name = "TextBox6"
        Me.TextBox6.ShortcutsEnabled = false
        Me.TextBox6.Size = New System.Drawing.Size(33, 22)
        Me.TextBox6.TabIndex = 100
        Me.TextBox6.TabStop = false
        Me.TextBox6.Text = "0"
        Me.TextBox6.Visible = false
        '
        'Label214
        '
        Me.Label214.AutoSize = true
        Me.Label214.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label214.Location = New System.Drawing.Point(15, 84)
        Me.Label214.Name = "Label214"
        Me.Label214.Size = New System.Drawing.Size(82, 14)
        Me.Label214.TabIndex = 99
        Me.Label214.Text = "Tablet Type :"
        Me.Label214.Visible = false
        '
        'TextBox7
        '
        Me.TextBox7.Enabled = false
        Me.TextBox7.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.TextBox7.Location = New System.Drawing.Point(399, 80)
        Me.TextBox7.MaxLength = 15
        Me.TextBox7.Name = "TextBox7"
        Me.TextBox7.ShortcutsEnabled = false
        Me.TextBox7.Size = New System.Drawing.Size(148, 22)
        Me.TextBox7.TabIndex = 98
        Me.TextBox7.TabStop = false
        Me.TextBox7.Visible = false
        '
        'Label215
        '
        Me.Label215.AutoSize = true
        Me.Label215.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label215.Location = New System.Drawing.Point(322, 83)
        Me.Label215.Name = "Label215"
        Me.Label215.Size = New System.Drawing.Size(73, 14)
        Me.Label215.TabIndex = 97
        Me.Label215.Text = "IP Address :"
        Me.Label215.Visible = false
        '
        'TextBox8
        '
        Me.TextBox8.Enabled = false
        Me.TextBox8.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.TextBox8.Location = New System.Drawing.Point(257, 80)
        Me.TextBox8.MaxLength = 1
        Me.TextBox8.Name = "TextBox8"
        Me.TextBox8.ShortcutsEnabled = false
        Me.TextBox8.Size = New System.Drawing.Size(33, 22)
        Me.TextBox8.TabIndex = 96
        Me.TextBox8.TabStop = false
        Me.TextBox8.Text = "0"
        Me.TextBox8.Visible = false
        '
        'Label216
        '
        Me.Label216.AutoSize = true
        Me.Label216.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label216.Location = New System.Drawing.Point(146, 83)
        Me.Label216.Name = "Label216"
        Me.Label216.Size = New System.Drawing.Size(106, 14)
        Me.Label216.TabIndex = 95
        Me.Label216.Text = "Multi USB Enable :"
        Me.Label216.Visible = false
        '
        'frmSettings_New
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7!, 14!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207,Byte),Integer), CType(CType(224,Byte),Integer), CType(CType(248,Byte),Integer))
        Me.ClientSize = New System.Drawing.Size(692, 749)
        Me.Controls.Add(Me.pnlMAIN)
        Me.Controls.Add(Me.pnl_tlspTOP)
        Me.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.MaximizeBox = false
        Me.MinimizeBox = false
        Me.Name = "frmSettings_New"
        Me.Text = "Settings"
        Me.pnlMAIN.ResumeLayout(false)
        Me.tc_Settings.ResumeLayout(false)
        Me.tp_Voice.ResumeLayout(false)
        Me.GroupBox13.ResumeLayout(false)
        Me.GroupBox13.PerformLayout
        Me.grb_PatientNotesFooterSettings.ResumeLayout(false)
        Me.grb_PatientNotesFooterSettings.PerformLayout
        Me.grWordhighlighhcolr.ResumeLayout(false)
        Me.grWordhighlighhcolr.PerformLayout
        Me.GroupBox2.ResumeLayout(false)
        Me.GroupBox2.PerformLayout
        Me.GroupBox1.ResumeLayout(false)
        Me.pnlResultBoxPosition.ResumeLayout(false)
        Me.pnlResultBoxPosition.PerformLayout
        Me.GroupBox15.ResumeLayout(false)
        Me.GroupBox15.PerformLayout
        CType(Me.numAutoSaveMinutes,System.ComponentModel.ISupportInitialize).EndInit
        Me.tp_FAXSettings.ResumeLayout(false)
        Me.GroupBox14.ResumeLayout(false)
        Me.GroupBox14.PerformLayout
        Me.GroupBox7.ResumeLayout(false)
        Me.GroupBox7.PerformLayout
        Me.tp_ServerPaths.ResumeLayout(false)
        Me.GroupBox8.ResumeLayout(false)
        Me.GroupBox8.PerformLayout
        Me.tp_DrugInteraction.ResumeLayout(false)
        Me.pnlDI_Main.ResumeLayout(false)
        Me.pnlDI_Main.PerformLayout
        Me.GroupBox22.ResumeLayout(false)
        Me.GroupBox22.PerformLayout
        Me.grpDrugInteraction.ResumeLayout(false)
        Me.pnlDrugInteraction.ResumeLayout(false)
        Me.pnlDrugInteraction.PerformLayout
        Me.tp_OtherSettings.ResumeLayout(false)
        Me.pnlOtherSettings.ResumeLayout(false)
        Me.GroupBox26.ResumeLayout(false)
        Me.GroupBox26.PerformLayout
        Me.GroupBox12.ResumeLayout(false)
        Me.GroupBox12.PerformLayout
        Me.Panel28.ResumeLayout(false)
        Me.grbSearchSetting.ResumeLayout(false)
        Me.grbSearchSetting.PerformLayout
        Me.GroupBox10.ResumeLayout(false)
        Me.GroupBox10.PerformLayout
        Me.GroupBox11.ResumeLayout(false)
        Me.GroupBox11.PerformLayout
        Me.grpbxPatientSynopsis.ResumeLayout(false)
        Me.grpbxPatientSynopsis.PerformLayout
        CType(Me.numPatientSypnosisTabCount,System.ComponentModel.ISupportInitialize).EndInit
        Me.grpbxRxMxDrugBtnSetting.ResumeLayout(false)
        Me.grpbxRxMxDrugBtnSetting.PerformLayout
        Me.gbRemotePrintSetting.ResumeLayout(false)
        Me.gbRemotePrintSetting.PerformLayout
        Me.pnlPrintImages.ResumeLayout(false)
        Me.pnlPrintImages.PerformLayout
        Me.Panel25.ResumeLayout(false)
        Me.Panel25.PerformLayout
        Me.pnlPrintClaims.ResumeLayout(false)
        Me.pnlPrintClaims.PerformLayout
        Me.Panel24.ResumeLayout(false)
        Me.Panel24.PerformLayout
        Me.Panel29.ResumeLayout(false)
        Me.gb_DefaultPrinterSettings.ResumeLayout(false)
        Me.gb_DefaultPrinterSettings.PerformLayout
        Me.Gbox_DefaultNavgtn.ResumeLayout(false)
        Me.Gbox_DefaultNavgtn.PerformLayout
        Me.grb_AutoRefreshSettings.ResumeLayout(false)
        Me.Panel6.ResumeLayout(false)
        Me.Panel6.PerformLayout
        CType(Me.num_MessagesRefreshTime,System.ComponentModel.ISupportInitialize).EndInit
        Me.grBday.ResumeLayout(false)
        Me.grBday.PerformLayout
        Me.pnlBday.ResumeLayout(false)
        Me.pnlBday.PerformLayout
        CType(Me.numBdayReminder,System.ComponentModel.ISupportInitialize).EndInit
        Me.grSettings.ResumeLayout(false)
        Me.grSettings.PerformLayout
        Me.grHL7Settings.ResumeLayout(false)
        Me.grHL7Settings.PerformLayout
        Me.GroupBox5.ResumeLayout(false)
        Me.pnlClinicEnv.ResumeLayout(false)
        Me.grpLockScreen.ResumeLayout(false)
        Me.grpLockScreen.PerformLayout
        CType(Me.numLockTime,System.ComponentModel.ISupportInitialize).EndInit
        Me.tp_SureScriptSettings.ResumeLayout(false)
        Me.tp_SureScriptSettings.PerformLayout
        Me.grpsurescripSecurMsg.ResumeLayout(false)
        Me.grpsurescripSecurMsg.PerformLayout
        Me.grpFormularySettings.ResumeLayout(false)
        Me.grpFormularySettings.PerformLayout
        Me.grpsurescriptalert.ResumeLayout(false)
        Me.grpsurescriptalert.PerformLayout
        Me.Panel1.ResumeLayout(false)
        Me.Panel1.PerformLayout
        Me.tp_DMSSettings.ResumeLayout(false)
        Me.GroupBox9.ResumeLayout(false)
        Me.GroupBox9.PerformLayout
        CType(Me.numBufferSize,System.ComponentModel.ISupportInitialize).EndInit
        Me.Panel31.ResumeLayout(false)
        Me.GroupBox3.ResumeLayout(false)
        Me.GroupBox3.PerformLayout
        Me.GroupBox4.ResumeLayout(false)
        Me.GroupBox4.PerformLayout
        Me.pnlRemoteScan.ResumeLayout(false)
        Me.pnlRemoteScan.PerformLayout
        Me.GroupBox24.ResumeLayout(false)
        Me.GroupBox24.PerformLayout
        Me.GroupBox23.ResumeLayout(false)
        Me.GroupBox23.PerformLayout
        Me.tp_PatientControl.ResumeLayout(false)
        Me.GroupBox25.ResumeLayout(false)
        Me.GroupBox25.PerformLayout
        Me.grbExportReport.ResumeLayout(false)
        Me.grbExportReport.PerformLayout
        Me.grbDefaultProvider.ResumeLayout(false)
        Me.grbDefaultProvider.PerformLayout
        Me.grbPatientBillingAlerts.ResumeLayout(false)
        Me.grbPatientBillingAlerts.PerformLayout
        Me.GrpBoxPatienDemographics.ResumeLayout(false)
        Me.grbPatientSearch.ResumeLayout(false)
        Me.GroupBox6.ResumeLayout(false)
        Me.gr_PatientSearch.ResumeLayout(false)
        Me.gr_PatientSearch.PerformLayout
        Me.tp_ExchangeServer.ResumeLayout(false)
        Me.gr_ExchangeServerSettings.ResumeLayout(false)
        Me.gr_ExchangeServerSettings.PerformLayout
        Me.tp_Appointment.ResumeLayout(false)
        Me.grb_CheckedoutAppointment.ResumeLayout(false)
        Me.grb_CheckedoutAppointment.PerformLayout
        Me.grbFollowup.ResumeLayout(false)
        Me.grbFollowup.PerformLayout
        Me.grbAppointments.ResumeLayout(false)
        Me.grbAppointments.PerformLayout
        CType(Me.num_NoofColOnCalndr,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.num_NoofApptInaSlot,System.ComponentModel.ISupportInitialize).EndInit
        Me.tb_SmartSettings.ResumeLayout(false)
        Me.Panel10.ResumeLayout(false)
        Me.Panel18.ResumeLayout(false)
        Me.Panel19.ResumeLayout(false)
        Me.Panel19.PerformLayout
        Me.Panel20.ResumeLayout(false)
        Me.Panel21.ResumeLayout(false)
        Me.Panel21.PerformLayout
        Me.Panel14.ResumeLayout(false)
        Me.Panel15.ResumeLayout(false)
        Me.Panel15.PerformLayout
        Me.Panel9.ResumeLayout(false)
        Me.Panel26.ResumeLayout(false)
        Me.Panel22.ResumeLayout(false)
        CType(Me.C1SmartOrdersSendTask,System.ComponentModel.ISupportInitialize).EndInit
        Me.pnlSmartOrder_Select.ResumeLayout(false)
        Me.Panel23.ResumeLayout(false)
        Me.Panel23.PerformLayout
        Me.Panel16.ResumeLayout(false)
        CType(Me.C1SmartTreatmentSendTask,System.ComponentModel.ISupportInitialize).EndInit
        Me.pnlSmartTreatment_Select.ResumeLayout(false)
        Me.Panel17.ResumeLayout(false)
        Me.Panel17.PerformLayout
        Me.Panel12.ResumeLayout(false)
        Me.Panel12.PerformLayout
        CType(Me.C1SmartDiagnosisSendTask,System.ComponentModel.ISupportInitialize).EndInit
        Me.pnlSmartDiagnosis_Select.ResumeLayout(false)
        Me.Panel13.ResumeLayout(false)
        Me.Panel13.PerformLayout
        Me.Panel27.ResumeLayout(false)
        Me.Panel11.ResumeLayout(false)
        Me.Panel11.PerformLayout
        Me.pnl_tlspTOP.ResumeLayout(false)
        Me.pnl_tlspTOP.PerformLayout
        Me.tlsp_Settings.ResumeLayout(false)
        Me.tlsp_Settings.PerformLayout
        Me.GroupBox16.ResumeLayout(false)
        Me.GroupBox16.PerformLayout
        Me.GroupBox17.ResumeLayout(false)
        Me.GroupBox17.PerformLayout
        Me.Panel5.ResumeLayout(false)
        Me.GroupBox18.ResumeLayout(false)
        Me.GroupBox18.PerformLayout
        Me.GroupBox19.ResumeLayout(false)
        Me.GroupBox19.PerformLayout
        Me.Panel7.ResumeLayout(false)
        Me.GroupBox20.ResumeLayout(false)
        Me.GroupBox20.PerformLayout
        Me.GroupBox21.ResumeLayout(false)
        Me.GroupBox21.PerformLayout
        Me.Panel8.ResumeLayout(false)
        Me.GroupBox27.ResumeLayout(false)
        Me.GroupBox27.PerformLayout
        Me.ResumeLayout(false)

End Sub
    Friend WithEvents pnlMAIN As System.Windows.Forms.Panel
    Friend WithEvents ColorDialog1 As System.Windows.Forms.ColorDialog
    Friend WithEvents FolderBrowserDialog1 As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents clordialogWord As System.Windows.Forms.ColorDialog
    Private WithEvents pnl_tlspTOP As System.Windows.Forms.Panel
    Private WithEvents tlsp_Settings As gloGlobal.gloToolStripIgnoreFocus
    Private WithEvents ts_btnOk As System.Windows.Forms.ToolStripButton
    Private WithEvents ts_btnCancel As System.Windows.Forms.ToolStripButton

    Friend WithEvents TwainPro1 As PegasusImaging.WinForms.TwainPro5.TwainPro
    Friend WithEvents tc_Settings As System.Windows.Forms.TabControl
    Friend WithEvents tp_Voice As System.Windows.Forms.TabPage
    Friend WithEvents grb_PatientNotesFooterSettings As System.Windows.Forms.GroupBox
    Friend WithEvents rdo_IncludePageNo_No As System.Windows.Forms.RadioButton
    Friend WithEvents rdo_IncludePageNo_Yes As System.Windows.Forms.RadioButton
    Friend WithEvents Label68 As System.Windows.Forms.Label
    Friend WithEvents grWordhighlighhcolr As System.Windows.Forms.GroupBox
    Friend WithEvents cmbHighlight As System.Windows.Forms.ComboBox
    Friend WithEvents lblcolor As System.Windows.Forms.Label
    Friend WithEvents chksethighlightcolr As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents rbtnNone As System.Windows.Forms.RadioButton
    Friend WithEvents rbtnSelect As System.Windows.Forms.RadioButton
    Friend WithEvents rbtnNotes As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents pnlResultBoxPosition As System.Windows.Forms.Panel
    Friend WithEvents optBottomRight As System.Windows.Forms.RadioButton
    Friend WithEvents optBottomLeft As System.Windows.Forms.RadioButton
    Friend WithEvents optTopRight As System.Windows.Forms.RadioButton
    Friend WithEvents optTopLeft As System.Windows.Forms.RadioButton
    Friend WithEvents optYes As System.Windows.Forms.RadioButton
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents optNo As System.Windows.Forms.RadioButton
    Friend WithEvents Label73 As System.Windows.Forms.Label
    Friend WithEvents Label74 As System.Windows.Forms.Label
    Friend WithEvents Label72 As System.Windows.Forms.Label
    Private WithEvents Label65 As System.Windows.Forms.Label
    Friend WithEvents Label75 As System.Windows.Forms.Label
    Private WithEvents Label66 As System.Windows.Forms.Label
    Private WithEvents Label64 As System.Windows.Forms.Label
    Private WithEvents Label67 As System.Windows.Forms.Label
    Friend WithEvents tp_FAXSettings As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox7 As System.Windows.Forms.GroupBox
    Friend WithEvents cmbFAXPrinter As System.Windows.Forms.ComboBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtFAXOutputDirectory As System.Windows.Forms.TextBox
    Friend WithEvents btnBrowseFAXOutputDirectory As System.Windows.Forms.Button
    Friend WithEvents Label76 As System.Windows.Forms.Label
    Friend WithEvents Label77 As System.Windows.Forms.Label
    Friend WithEvents Label78 As System.Windows.Forms.Label
    Friend WithEvents Label79 As System.Windows.Forms.Label
    Private WithEvents Label60 As System.Windows.Forms.Label
    Private WithEvents Label61 As System.Windows.Forms.Label
    Private WithEvents Label62 As System.Windows.Forms.Label
    Private WithEvents Label63 As System.Windows.Forms.Label
    Friend WithEvents tp_ServerPaths As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox8 As System.Windows.Forms.GroupBox
    Friend WithEvents txtDICOMPath As System.Windows.Forms.TextBox
    Friend WithEvents lblDICOMPath As System.Windows.Forms.Label
    Friend WithEvents btnBrowseDICOMPath As System.Windows.Forms.Button
    Friend WithEvents txtVMSPath As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtServerPath As System.Windows.Forms.TextBox
    Friend WithEvents btnServerpath As System.Windows.Forms.Button
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtDMSPath As System.Windows.Forms.TextBox
    Friend WithEvents btnBrowseDMSPath As System.Windows.Forms.Button
    Friend WithEvents lblDMSMessage As System.Windows.Forms.Label
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents btnBrowseVMSPath As System.Windows.Forms.Button
    Friend WithEvents Label80 As System.Windows.Forms.Label
    Friend WithEvents Label81 As System.Windows.Forms.Label
    Friend WithEvents Label82 As System.Windows.Forms.Label
    Friend WithEvents Label83 As System.Windows.Forms.Label
    Private WithEvents Label56 As System.Windows.Forms.Label
    Private WithEvents Label57 As System.Windows.Forms.Label
    Private WithEvents Label58 As System.Windows.Forms.Label
    Private WithEvents Label59 As System.Windows.Forms.Label
    Friend WithEvents tp_DrugInteraction As System.Windows.Forms.TabPage
    Friend WithEvents Label84 As System.Windows.Forms.Label
    Friend WithEvents Label85 As System.Windows.Forms.Label
    Friend WithEvents Label86 As System.Windows.Forms.Label
    Friend WithEvents Label87 As System.Windows.Forms.Label
    Private WithEvents Label52 As System.Windows.Forms.Label
    Private WithEvents Label53 As System.Windows.Forms.Label
    Private WithEvents Label54 As System.Windows.Forms.Label
    Private WithEvents Label55 As System.Windows.Forms.Label
    Friend WithEvents tp_OtherSettings As System.Windows.Forms.TabPage
    Private WithEvents Label48 As System.Windows.Forms.Label
    Private WithEvents Label49 As System.Windows.Forms.Label
    Private WithEvents Label50 As System.Windows.Forms.Label
    Private WithEvents Label51 As System.Windows.Forms.Label
    Friend WithEvents pnlOtherSettings As System.Windows.Forms.Panel
    Friend WithEvents grbSearchSetting As System.Windows.Forms.GroupBox
    Friend WithEvents chkResetSearch As System.Windows.Forms.CheckBox
    Friend WithEvents grpbxPatientSynopsis As System.Windows.Forms.GroupBox
    Friend WithEvents numPatientSypnosisTabCount As System.Windows.Forms.NumericUpDown
    Friend WithEvents lblPatientTabCount As System.Windows.Forms.Label
    Friend WithEvents grpbxRxMxDrugBtnSetting As System.Windows.Forms.GroupBox
    Friend WithEvents cmbMxDrugBtn As System.Windows.Forms.ComboBox
    Friend WithEvents lblMx As System.Windows.Forms.Label
    Friend WithEvents cmbRxDrugBtn As System.Windows.Forms.ComboBox
    Friend WithEvents lblRx As System.Windows.Forms.Label
    Friend WithEvents Gbox_DefaultNavgtn As System.Windows.Forms.GroupBox
    Friend WithEvents Cmb_NavgtnPnl As System.Windows.Forms.ComboBox
    Friend WithEvents Lbl_NavgtnPnl As System.Windows.Forms.Label
    Friend WithEvents Label71 As System.Windows.Forms.Label
    Friend WithEvents grb_AutoRefreshSettings As System.Windows.Forms.GroupBox
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents num_MessagesRefreshTime As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents grBday As System.Windows.Forms.GroupBox
    Friend WithEvents pnlBday As System.Windows.Forms.Panel
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents numBdayReminder As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents chkBdayReminder As System.Windows.Forms.CheckBox
    Friend WithEvents grSettings As System.Windows.Forms.GroupBox
    Friend WithEvents chkOutbound As System.Windows.Forms.CheckBox
    Friend WithEvents grHL7Settings As System.Windows.Forms.GroupBox
    Friend WithEvents chkPatientReg As System.Windows.Forms.CheckBox
    Friend WithEvents chkSaveandFinish As System.Windows.Forms.CheckBox
    Friend WithEvents chkSaveandClose As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents pnlClinicEnv As System.Windows.Forms.Panel
    Friend WithEvents lblENV_06 As System.Windows.Forms.Label
    Friend WithEvents lblENV_05 As System.Windows.Forms.Label
    Friend WithEvents lblENV_04 As System.Windows.Forms.Label
    Friend WithEvents lblENV_03 As System.Windows.Forms.Label
    Friend WithEvents lblENV_02 As System.Windows.Forms.Label
    Friend WithEvents lblENV_01 As System.Windows.Forms.Label
    Friend WithEvents grpLockScreen As System.Windows.Forms.GroupBox
    Friend WithEvents chkAutoApplicationLock As System.Windows.Forms.CheckBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents numLockTime As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label69 As System.Windows.Forms.Label
    Friend WithEvents Label70 As System.Windows.Forms.Label
    Friend WithEvents tp_SureScriptSettings As System.Windows.Forms.TabPage
    Friend WithEvents grpFormularySettings As System.Windows.Forms.GroupBox
    Friend WithEvents chkFormularyAlertnativesOffFormularyDrgs As System.Windows.Forms.CheckBox
    Friend WithEvents chkFormularyAlertnativesNRDrgs As System.Windows.Forms.CheckBox
    Friend WithEvents chkMachineFormularyAlert As System.Windows.Forms.CheckBox
    Friend WithEvents chkFormularyAlertnativesAllDrgs As System.Windows.Forms.CheckBox
    Friend WithEvents grpsurescriptalert As System.Windows.Forms.GroupBox
    Friend WithEvents chkSurescriptFaxSettings As System.Windows.Forms.CheckBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents cmbInterval As System.Windows.Forms.ComboBox
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents chksureAlert As System.Windows.Forms.CheckBox
    Friend WithEvents Label88 As System.Windows.Forms.Label
    Friend WithEvents Label89 As System.Windows.Forms.Label
    Friend WithEvents Label90 As System.Windows.Forms.Label
    Friend WithEvents Label91 As System.Windows.Forms.Label
    Private WithEvents Label44 As System.Windows.Forms.Label
    Private WithEvents Label45 As System.Windows.Forms.Label
    Private WithEvents Label46 As System.Windows.Forms.Label
    Private WithEvents Label47 As System.Windows.Forms.Label
    Friend WithEvents tp_DMSSettings As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox9 As System.Windows.Forms.GroupBox
    Private WithEvents Label105 As System.Windows.Forms.Label
    Private WithEvents Label104 As System.Windows.Forms.Label
    Private WithEvents numBufferSize As System.Windows.Forms.NumericUpDown
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents rbScanDocumentWithMonth As System.Windows.Forms.RadioButton
    Friend WithEvents rbScanDocumentWithoutMonth As System.Windows.Forms.RadioButton
    Friend WithEvents Label92 As System.Windows.Forms.Label
    Friend WithEvents Label93 As System.Windows.Forms.Label
    Friend WithEvents Label94 As System.Windows.Forms.Label
    Friend WithEvents Label95 As System.Windows.Forms.Label
    Private WithEvents Label40 As System.Windows.Forms.Label
    Private WithEvents Label41 As System.Windows.Forms.Label
    Private WithEvents Label42 As System.Windows.Forms.Label
    Private WithEvents Label43 As System.Windows.Forms.Label
    Friend WithEvents tp_PatientControl As System.Windows.Forms.TabPage
    Private WithEvents grbExportReport As System.Windows.Forms.GroupBox
    Friend WithEvents btnClearReportPath As System.Windows.Forms.Button
    Friend WithEvents btnBrowseReportPath As System.Windows.Forms.Button
    Friend WithEvents txtExportReportPath As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents chkExportReport As System.Windows.Forms.CheckBox
    Private WithEvents grbDefaultProvider As System.Windows.Forms.GroupBox
    Friend WithEvents cmbDefaultProvider As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents grbPatientBillingAlerts As System.Windows.Forms.GroupBox
    Friend WithEvents btnBrowseAlertColor As System.Windows.Forms.Button
    Friend WithEvents txtAlertColor As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents chkShowBlinkingAlert As System.Windows.Forms.CheckBox
    Private WithEvents GrpBoxPatienDemographics As System.Windows.Forms.GroupBox
    Private WithEvents trvDemographics As System.Windows.Forms.TreeView
    Private WithEvents GroupBox6 As System.Windows.Forms.GroupBox
    Private WithEvents trvPatientColumns As System.Windows.Forms.TreeView
    Private WithEvents gr_PatientSearch As System.Windows.Forms.GroupBox
    Private WithEvents cmbSearchColumns As System.Windows.Forms.ComboBox
    Private WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents Label96 As System.Windows.Forms.Label
    Friend WithEvents Label97 As System.Windows.Forms.Label
    Friend WithEvents Label98 As System.Windows.Forms.Label
    Friend WithEvents Label99 As System.Windows.Forms.Label
    Private WithEvents Label36 As System.Windows.Forms.Label
    Private WithEvents Label37 As System.Windows.Forms.Label
    Private WithEvents Label38 As System.Windows.Forms.Label
    Private WithEvents Label39 As System.Windows.Forms.Label
    Friend WithEvents tp_ExchangeServer As System.Windows.Forms.TabPage
    Private WithEvents gr_ExchangeServerSettings As System.Windows.Forms.GroupBox
    Friend WithEvents txtExchangeDomain As System.Windows.Forms.TextBox
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents Label35 As System.Windows.Forms.Label
    Friend WithEvents txtExchangeURL As System.Windows.Forms.TextBox
    Friend WithEvents Label100 As System.Windows.Forms.Label
    Friend WithEvents Label101 As System.Windows.Forms.Label
    Friend WithEvents Label102 As System.Windows.Forms.Label
    Friend WithEvents Label103 As System.Windows.Forms.Label
    Private WithEvents lbl_BottomBrd As System.Windows.Forms.Label
    Private WithEvents lbl_LeftBrd As System.Windows.Forms.Label
    Private WithEvents lbl_RightBrd As System.Windows.Forms.Label
    Private WithEvents lbl_TopBrd As System.Windows.Forms.Label
    Friend WithEvents tp_Appointment As System.Windows.Forms.TabPage
    Private WithEvents Label114 As System.Windows.Forms.Label
    Friend WithEvents grbFollowup As System.Windows.Forms.GroupBox
    Private WithEvents rbFollowupFromToday As System.Windows.Forms.RadioButton
    Private WithEvents rbFolloupFromDate As System.Windows.Forms.RadioButton
    Friend WithEvents grbAppointments As System.Windows.Forms.GroupBox
    Private WithEvents chkShowTemplate As System.Windows.Forms.CheckBox
    Private WithEvents num_NoofApptInaSlot As System.Windows.Forms.NumericUpDown
    Private WithEvents Label13 As System.Windows.Forms.Label
    Private WithEvents Label111 As System.Windows.Forms.Label
    Private WithEvents Label112 As System.Windows.Forms.Label
    Private WithEvents Label113 As System.Windows.Forms.Label
    Friend WithEvents rbHL7 As System.Windows.Forms.CheckBox
    Friend WithEvents rbGenius As System.Windows.Forms.CheckBox
    Friend WithEvents grb_CheckedoutAppointment As System.Windows.Forms.GroupBox
    Private WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents chkCheckedoutAppointments As System.Windows.Forms.CheckBox
    Private WithEvents cmbScanMode As System.Windows.Forms.ComboBox
    Private WithEvents Label28 As System.Windows.Forms.Label
    Private WithEvents cmbBitDepth As System.Windows.Forms.ComboBox
    Private WithEvents Label32 As System.Windows.Forms.Label
    Private WithEvents Label117 As System.Windows.Forms.Label
    Private WithEvents Label120 As System.Windows.Forms.Label
    Private WithEvents txtCardWidth As System.Windows.Forms.TextBox
    Private WithEvents txtCardLength As System.Windows.Forms.TextBox
    Private WithEvents Label115 As System.Windows.Forms.Label
    Private WithEvents Label116 As System.Windows.Forms.Label
    Private WithEvents cmbScanSide As System.Windows.Forms.ComboBox
    Private WithEvents Label29 As System.Windows.Forms.Label
    Private WithEvents Label118 As System.Windows.Forms.Label
    Private WithEvents cmbContrast As System.Windows.Forms.ComboBox
    Private WithEvents Label19 As System.Windows.Forms.Label
    Private WithEvents cmbBrightness As System.Windows.Forms.ComboBox
    Private WithEvents Label27 As System.Windows.Forms.Label
    Private WithEvents chkShowScannerDialog As System.Windows.Forms.CheckBox
    Private WithEvents cmbResolution As System.Windows.Forms.ComboBox
    Private WithEvents Label30 As System.Windows.Forms.Label
    Private WithEvents cmbScanner As System.Windows.Forms.ComboBox
    Private WithEvents Label31 As System.Windows.Forms.Label
    Private WithEvents cmbSupportedSize As System.Windows.Forms.ComboBox
    Private WithEvents lblSuportedSize As System.Windows.Forms.Label
    Private WithEvents Label107 As System.Windows.Forms.Label
    Private WithEvents lblCardLocation As System.Windows.Forms.Label
    Private WithEvents txtStartX As System.Windows.Forms.TextBox
    Private WithEvents txtStartY As System.Windows.Forms.TextBox
    Private WithEvents Label119 As System.Windows.Forms.Label
    Friend WithEvents tb_SmartSettings As System.Windows.Forms.TabPage
    'Friend WithEvents GroupBox15 As System.Windows.Forms.GroupBox
    'Friend WithEvents chk_SmartOrder_SendTask As System.Windows.Forms.CheckBox
    'Friend WithEvents chklst_SmartOrdersSendTask As System.Windows.Forms.CheckedListBox
    'Friend WithEvents GroupBox12 As System.Windows.Forms.GroupBox
    'Friend WithEvents chk_SmartOrder As System.Windows.Forms.CheckBox
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    'Friend WithEvents btnClearAllOrder As System.Windows.Forms.Button
    Friend WithEvents btnSelectAllOrder As System.Windows.Forms.Button
    'Friend WithEvents btn_UpOrders As System.Windows.Forms.Button
    'Friend WithEvents chklst_SmartOrders As System.Windows.Forms.CheckedListBox
    'Friend WithEvents btn_DownOrders As System.Windows.Forms.Button
    'Friend WithEvents GroupBox14 As System.Windows.Forms.GroupBox
    'Friend WithEvents chk_SmartTreatment_SendTask As System.Windows.Forms.CheckBox
    'Friend WithEvents chklst_SmartTreatmentSendTask As System.Windows.Forms.CheckedListBox
    'Friend WithEvents GroupBox11 As System.Windows.Forms.GroupBox
    'Friend WithEvents chk_SmartTreatment As System.Windows.Forms.CheckBox
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    'Friend WithEvents btnClearAllCPT As System.Windows.Forms.Button
    Friend WithEvents btnSelectALLCPT As System.Windows.Forms.Button
    'Friend WithEvents chklst_SmartTreatment As System.Windows.Forms.CheckedListBox
    'Friend WithEvents btn_DownTreatment As System.Windows.Forms.Button
    'Friend WithEvents btn_UpTreatment As System.Windows.Forms.Button
    Private WithEvents Label108 As System.Windows.Forms.Label
    Private WithEvents Label121 As System.Windows.Forms.Label
    Private WithEvents Label122 As System.Windows.Forms.Label
    Private WithEvents Label123 As System.Windows.Forms.Label
    'Friend WithEvents GroupBox13 As System.Windows.Forms.GroupBox
    'Friend WithEvents chk_SmartDiagnosis_SendTask As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox4 As System.Windows.Forms.CheckBox
    'Friend WithEvents chklst_SmartDiagnosisSendTask As System.Windows.Forms.CheckedListBox
    'Friend WithEvents GroupBox10 As System.Windows.Forms.GroupBox
    'Friend WithEvents chk_SmartDiagnosis As System.Windows.Forms.CheckBox
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    'Friend WithEvents btnClearAllDX As System.Windows.Forms.Button
    'Friend WithEvents btnSelectAllDX As System.Windows.Forms.Button
    'Friend WithEvents btn_UpDiagnosis As System.Windows.Forms.Button
    'Friend WithEvents btn_DownDiagnosis As System.Windows.Forms.Button
    'Friend WithEvents chklst_SmartDiagnosis As System.Windows.Forms.CheckedListBox
    Friend WithEvents GroupBox16 As System.Windows.Forms.GroupBox
    Friend WithEvents CheckBox1 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckedListBox1 As System.Windows.Forms.CheckedListBox
    Friend WithEvents GroupBox17 As System.Windows.Forms.GroupBox
    Friend WithEvents CheckBox2 As System.Windows.Forms.CheckBox
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents CheckedListBox2 As System.Windows.Forms.CheckedListBox
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents GroupBox18 As System.Windows.Forms.GroupBox
    Friend WithEvents CheckBox3 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckedListBox3 As System.Windows.Forms.CheckedListBox
    Friend WithEvents GroupBox19 As System.Windows.Forms.GroupBox
    Friend WithEvents CheckBox5 As System.Windows.Forms.CheckBox
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Friend WithEvents Button5 As System.Windows.Forms.Button
    Friend WithEvents Button6 As System.Windows.Forms.Button
    Friend WithEvents CheckedListBox4 As System.Windows.Forms.CheckedListBox
    Friend WithEvents Button7 As System.Windows.Forms.Button
    Friend WithEvents Button8 As System.Windows.Forms.Button
    Private WithEvents Label124 As System.Windows.Forms.Label
    Private WithEvents Label125 As System.Windows.Forms.Label
    Private WithEvents Label126 As System.Windows.Forms.Label
    Private WithEvents Label127 As System.Windows.Forms.Label
    Friend WithEvents GroupBox20 As System.Windows.Forms.GroupBox
    Friend WithEvents CheckBox6 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox7 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckedListBox5 As System.Windows.Forms.CheckedListBox
    Friend WithEvents GroupBox21 As System.Windows.Forms.GroupBox
    Friend WithEvents CheckBox8 As System.Windows.Forms.CheckBox
    Friend WithEvents Panel8 As System.Windows.Forms.Panel
    Friend WithEvents Button9 As System.Windows.Forms.Button
    Friend WithEvents Button10 As System.Windows.Forms.Button
    Friend WithEvents Button11 As System.Windows.Forms.Button
    Friend WithEvents Button12 As System.Windows.Forms.Button
    Friend WithEvents CheckedListBox6 As System.Windows.Forms.CheckedListBox
    'Friend WithEvents GroupBox13 As System.Windows.Forms.GroupBox
    'Friend WithEvents chk_SmartDiagnosis_SendTask As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox9 As System.Windows.Forms.CheckBox
    'Friend WithEvents chklst_SmartDiagnosisSendTask As System.Windows.Forms.CheckedListBox
    'Friend WithEvents chk_SmartOrder As System.Windows.Forms.CheckBox
    Friend WithEvents btnClearAllOrder As System.Windows.Forms.Button
    Friend WithEvents Button13 As System.Windows.Forms.Button
    Friend WithEvents btn_UpOrders As System.Windows.Forms.Button
    Friend WithEvents chklst_SmartOrders As System.Windows.Forms.CheckedListBox
    Friend WithEvents btn_DownOrders As System.Windows.Forms.Button
    'Friend WithEvents chk_SmartTreatment As System.Windows.Forms.CheckBox
    Friend WithEvents btnClearAllCPT As System.Windows.Forms.Button
    Friend WithEvents Button14 As System.Windows.Forms.Button
    Friend WithEvents chklst_SmartTreatment As System.Windows.Forms.CheckedListBox
    Friend WithEvents btn_DownTreatment As System.Windows.Forms.Button
    Friend WithEvents btn_UpTreatment As System.Windows.Forms.Button
    'Friend WithEvents chk_SmartDiagnosis As System.Windows.Forms.CheckBox
    Friend WithEvents btnClearAllDX As System.Windows.Forms.Button
    Friend WithEvents btnSelectAllDX As System.Windows.Forms.Button
    Friend WithEvents btn_UpDiagnosis As System.Windows.Forms.Button
    Friend WithEvents btn_DownDiagnosis As System.Windows.Forms.Button
    Friend WithEvents chklst_SmartDiagnosis As System.Windows.Forms.CheckedListBox
    Friend WithEvents Panel14 As System.Windows.Forms.Panel
    Private WithEvents Label134 As System.Windows.Forms.Label
    Friend WithEvents Panel15 As System.Windows.Forms.Panel
    Private WithEvents Label139 As System.Windows.Forms.Label
    Private WithEvents Label136 As System.Windows.Forms.Label
    Private WithEvents Label137 As System.Windows.Forms.Label
    Private WithEvents Label133 As System.Windows.Forms.Label
    Friend WithEvents Panel18 As System.Windows.Forms.Panel
    Private WithEvents Label146 As System.Windows.Forms.Label
    Private WithEvents Label147 As System.Windows.Forms.Label
    Friend WithEvents Panel19 As System.Windows.Forms.Panel
    Private WithEvents Label148 As System.Windows.Forms.Label
    Private WithEvents Label150 As System.Windows.Forms.Label
    Private WithEvents Label151 As System.Windows.Forms.Label
    Private WithEvents Label170 As System.Windows.Forms.Label
    Private WithEvents Label169 As System.Windows.Forms.Label
    Private WithEvents Label168 As System.Windows.Forms.Label
    Friend WithEvents Panel20 As System.Windows.Forms.Panel
    Private WithEvents Label152 As System.Windows.Forms.Label
    Friend WithEvents Panel21 As System.Windows.Forms.Panel
    Private WithEvents Label154 As System.Windows.Forms.Label
    Private WithEvents Label156 As System.Windows.Forms.Label
    Private WithEvents Label157 As System.Windows.Forms.Label
    Private WithEvents Label172 As System.Windows.Forms.Label
    Private WithEvents Label171 As System.Windows.Forms.Label
    Private WithEvents Label179 As System.Windows.Forms.Label
    Private WithEvents Label175 As System.Windows.Forms.Label
    Private WithEvents Label182 As System.Windows.Forms.Label
    Private WithEvents Label176 As System.Windows.Forms.Label
    Private WithEvents Label167 As System.Windows.Forms.Label
    Private WithEvents Label166 As System.Windows.Forms.Label
    Private WithEvents Label165 As System.Windows.Forms.Label
    Private WithEvents Label164 As System.Windows.Forms.Label
    Private WithEvents Label184 As System.Windows.Forms.Label
    Private WithEvents Label183 As System.Windows.Forms.Label
    Friend WithEvents Panel9 As System.Windows.Forms.Panel
    Private WithEvents Label185 As System.Windows.Forms.Label
    Friend WithEvents Panel10 As System.Windows.Forms.Panel
    Private WithEvents Label189 As System.Windows.Forms.Label
    Private WithEvents Label187 As System.Windows.Forms.Label
    Private WithEvents Label186 As System.Windows.Forms.Label
    Friend WithEvents Panel26 As System.Windows.Forms.Panel
    Friend WithEvents Panel22 As System.Windows.Forms.Panel
    Friend WithEvents chklst_SmartOrdersSendTask As System.Windows.Forms.CheckedListBox
    Private WithEvents Label158 As System.Windows.Forms.Label
    Private WithEvents Label159 As System.Windows.Forms.Label
    Friend WithEvents Panel23 As System.Windows.Forms.Panel
    Friend WithEvents chk_SmartOrder_SendTask As System.Windows.Forms.CheckBox
    Private WithEvents Label161 As System.Windows.Forms.Label
    Private WithEvents Label162 As System.Windows.Forms.Label
    Private WithEvents Label163 As System.Windows.Forms.Label
    Friend WithEvents Panel16 As System.Windows.Forms.Panel
    Friend WithEvents chklst_SmartTreatmentSendTask As System.Windows.Forms.CheckedListBox
    Private WithEvents Label140 As System.Windows.Forms.Label
    Friend WithEvents Panel17 As System.Windows.Forms.Panel
    Friend WithEvents chk_SmartTreatment_SendTask As System.Windows.Forms.CheckBox
    Private WithEvents Label142 As System.Windows.Forms.Label
    Private WithEvents Label144 As System.Windows.Forms.Label
    Private WithEvents Label145 As System.Windows.Forms.Label
    Friend WithEvents Panel12 As System.Windows.Forms.Panel
    Friend WithEvents chklst_SmartDiagnosisSendTask As System.Windows.Forms.CheckedListBox
    Friend WithEvents CheckBox10 As System.Windows.Forms.CheckBox
    Private WithEvents Label129 As System.Windows.Forms.Label
    Private WithEvents Label128 As System.Windows.Forms.Label
    Friend WithEvents Panel13 As System.Windows.Forms.Panel
    Friend WithEvents chk_SmartDiagnosis_SendTask As System.Windows.Forms.CheckBox
    Private WithEvents Label138 As System.Windows.Forms.Label
    Private WithEvents Label130 As System.Windows.Forms.Label
    Private WithEvents Label131 As System.Windows.Forms.Label
    Private WithEvents Label132 As System.Windows.Forms.Label
    Friend WithEvents Panel27 As System.Windows.Forms.Panel
    Private WithEvents Label190 As System.Windows.Forms.Label
    Private WithEvents Label192 As System.Windows.Forms.Label
    Private WithEvents Label193 As System.Windows.Forms.Label
    Private WithEvents Label194 As System.Windows.Forms.Label
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
    Private WithEvents Label153 As System.Windows.Forms.Label
    Private WithEvents Label135 As System.Windows.Forms.Label
    Private WithEvents Label160 As System.Windows.Forms.Label
    Friend WithEvents imgTreeVIew As System.Windows.Forms.ImageList
    Friend WithEvents C1SmartOrdersSendTask As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents C1SmartTreatmentSendTask As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents C1SmartDiagnosisSendTask As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents PnlCustomTask As System.Windows.Forms.Panel
    Friend WithEvents chk_SmartDiagnosis As System.Windows.Forms.CheckBox
    Friend WithEvents chk_SmartTreatment As System.Windows.Forms.CheckBox
    Friend WithEvents chk_SmartOrder As System.Windows.Forms.CheckBox
    Friend WithEvents pnlSmartDiagnosis_Select As System.Windows.Forms.Panel
    Friend WithEvents btnSmartDiagnosisSelect_Cancel As System.Windows.Forms.Button
    Friend WithEvents btnSmartDiagnosisSelect_All As System.Windows.Forms.Button
    Friend WithEvents btnSmartDiagnosisView_All As System.Windows.Forms.Button
    Friend WithEvents btnSmartDiagnosisView_Cancel As System.Windows.Forms.Button
    Friend WithEvents pnlSmartTreatment_Select As System.Windows.Forms.Panel
    Friend WithEvents btnSmartTreatmentView_All As System.Windows.Forms.Button
    Friend WithEvents btnSmartTreatmentView_Cancel As System.Windows.Forms.Button
    Friend WithEvents btnSmartTreatmentSelect_Cancel As System.Windows.Forms.Button
    Friend WithEvents btnSmartTreatmentSelect_All As System.Windows.Forms.Button
    Friend WithEvents pnlSmartOrder_Select As System.Windows.Forms.Panel
    Friend WithEvents btnSmartOrderView_All As System.Windows.Forms.Button
    Friend WithEvents btnSmartOrderView_Cancel As System.Windows.Forms.Button
    Friend WithEvents btnSmartOrderSelect_Cancel As System.Windows.Forms.Button
    Friend WithEvents btnSmartOrderSelect_All As System.Windows.Forms.Button
    Friend WithEvents chkHL7Immunization As System.Windows.Forms.CheckBox
    Private WithEvents Label141 As System.Windows.Forms.Label
    Private WithEvents Label143 As System.Windows.Forms.Label
    Private WithEvents Label149 As System.Windows.Forms.Label
    Friend WithEvents Panel11 As System.Windows.Forms.Panel
    Private WithEvents Label178 As System.Windows.Forms.Label
    Private WithEvents Label177 As System.Windows.Forms.Label
    Private WithEvents Label174 As System.Windows.Forms.Label
    Private WithEvents Label173 As System.Windows.Forms.Label
    Private WithEvents Label155 As System.Windows.Forms.Label
    Friend WithEvents GroupBox10 As System.Windows.Forms.GroupBox
    Friend WithEvents ChkPatientConfiInfo As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox11 As System.Windows.Forms.GroupBox
    Friend WithEvents Label180 As System.Windows.Forms.Label
    Friend WithEvents txtInfo As System.Windows.Forms.TextBox
    Private WithEvents Label181 As System.Windows.Forms.Label
    Private WithEvents num_NoofColOnCalndr As System.Windows.Forms.NumericUpDown
    Private WithEvents lblCalCol As System.Windows.Forms.Label
    Friend WithEvents GroupBox12 As System.Windows.Forms.GroupBox
    Friend WithEvents chk_ErrorLogs As System.Windows.Forms.CheckBox
    Friend WithEvents chk_ApplicationLog As System.Windows.Forms.CheckBox
    Friend WithEvents chkHL7Appointment As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox13 As System.Windows.Forms.GroupBox
    Friend WithEvents cbSigPlusTS As System.Windows.Forms.CheckBox
    Private WithEvents txtTabletType As System.Windows.Forms.TextBox
    Friend WithEvents Label195 As System.Windows.Forms.Label
    Private WithEvents txtIPAddress As System.Windows.Forms.TextBox
    Friend WithEvents Label191 As System.Windows.Forms.Label
    Private WithEvents txtMultiUSB As System.Windows.Forms.TextBox
    Friend WithEvents Label188 As System.Windows.Forms.Label
    Private WithEvents txtTabletPortPath As System.Windows.Forms.TextBox
    Friend WithEvents Label106 As System.Windows.Forms.Label
    Friend WithEvents chkShowOffFormularyAlternatives As System.Windows.Forms.CheckBox
    Friend WithEvents chkNDCInAlternativeGrid As System.Windows.Forms.CheckBox
    Friend WithEvents chkShowNDCInMedication As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox14 As System.Windows.Forms.GroupBox
    Friend WithEvents Label199 As System.Windows.Forms.Label
    Friend WithEvents txtFaxDownloadPath As System.Windows.Forms.TextBox
    Friend WithEvents btnBrowseDownloadPath As System.Windows.Forms.Button
    Private WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents Label196 As System.Windows.Forms.Label
    Private WithEvents TextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents Label197 As System.Windows.Forms.Label
    Friend WithEvents Label198 As System.Windows.Forms.Label
    Friend WithEvents grpsurescripSecurMsg As System.Windows.Forms.GroupBox
    Friend WithEvents chksecureSureScriptsetting As System.Windows.Forms.CheckBox
    Private WithEvents grbPatientSearch As System.Windows.Forms.GroupBox
    Private WithEvents trvPatientSearch As System.Windows.Forms.TreeView
    Friend WithEvents chkHandleFAXIssue As System.Windows.Forms.CheckBox
    Friend WithEvents chkCoverPage As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox15 As System.Windows.Forms.GroupBox
    Friend WithEvents chkAutoSaveExam As System.Windows.Forms.CheckBox
    Private WithEvents TextBox3 As System.Windows.Forms.TextBox
    Friend WithEvents Label200 As System.Windows.Forms.Label
    Private WithEvents TextBox4 As System.Windows.Forms.TextBox
    Friend WithEvents Label201 As System.Windows.Forms.Label
    Private WithEvents TextBox5 As System.Windows.Forms.TextBox
    Friend WithEvents Label202 As System.Windows.Forms.Label
    Friend WithEvents numAutoSaveMinutes As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label203 As System.Windows.Forms.Label
    Friend WithEvents pnlDI_Main As System.Windows.Forms.Panel
    Friend WithEvents GroupBox22 As System.Windows.Forms.GroupBox
    Friend WithEvents chkDrugDiseaseInteraction As System.Windows.Forms.CheckBox
    Friend WithEvents chkDrugFoodInteraction As System.Windows.Forms.CheckBox
    Friend WithEvents chkDrugAllergyInteraction As System.Windows.Forms.CheckBox
    Friend WithEvents chkAdverseDrugEffect As System.Windows.Forms.CheckBox
    Friend WithEvents chkDrugDrugInteraction As System.Windows.Forms.CheckBox
    Friend WithEvents chkDuplicateTherapy As System.Windows.Forms.CheckBox
    Friend WithEvents chkDrugAlert As System.Windows.Forms.CheckBox
    Friend WithEvents grpDrugInteraction As System.Windows.Forms.GroupBox
    Friend WithEvents pnlDrugInteraction As System.Windows.Forms.Panel
    Friend WithEvents cmbADEOnset As System.Windows.Forms.ComboBox
    Friend WithEvents Label204 As System.Windows.Forms.Label
    Friend WithEvents cmbDFADoc As System.Windows.Forms.ComboBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents cmbDIDoc As System.Windows.Forms.ComboBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents cmbDFA As System.Windows.Forms.ComboBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents cmbDI As System.Windows.Forms.ComboBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents cmbADE As System.Windows.Forms.ComboBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents gbRemotePrintSetting As System.Windows.Forms.GroupBox
    Friend WithEvents Label205 As System.Windows.Forms.Label
    Friend WithEvents chkAddFooterService As System.Windows.Forms.CheckBox
    Friend WithEvents chkEnableLocalPrinter As System.Windows.Forms.CheckBox
    Friend WithEvents gb_DefaultPrinterSettings As System.Windows.Forms.GroupBox
    Friend WithEvents chkUseDefaultPrinter As System.Windows.Forms.CheckBox
    Friend WithEvents cmbNoPagesSplit As System.Windows.Forms.ComboBox
    Friend WithEvents Label206 As System.Windows.Forms.Label
    Friend WithEvents Panel25 As System.Windows.Forms.Panel
    Friend WithEvents rbPrintSSRSReportEMF As System.Windows.Forms.RadioButton
    Friend WithEvents Label208 As System.Windows.Forms.Label
    Friend WithEvents rbPrintSSRSReportPDF As System.Windows.Forms.RadioButton
    Friend WithEvents Panel24 As System.Windows.Forms.Panel
    Friend WithEvents rbPrintWordDocEMF As System.Windows.Forms.RadioButton
    Friend WithEvents Label207 As System.Windows.Forms.Label
    Friend WithEvents rbPrintWordDocPDF As System.Windows.Forms.RadioButton
    Friend WithEvents Panel28 As System.Windows.Forms.Panel
    Friend WithEvents pnlPrintImages As System.Windows.Forms.Panel
    Friend WithEvents rbPrintImagesEMF As System.Windows.Forms.RadioButton
    Friend WithEvents Label209 As System.Windows.Forms.Label
    Friend WithEvents rbPrintImagesPNG As System.Windows.Forms.RadioButton
    Friend WithEvents pnlPrintClaims As System.Windows.Forms.Panel
    Friend WithEvents rbPrintClaimsEMF As System.Windows.Forms.RadioButton
    Friend WithEvents Label210 As System.Windows.Forms.Label
    Friend WithEvents rbPrintClaimsPDF As System.Windows.Forms.RadioButton
    Friend WithEvents Panel29 As System.Windows.Forms.Panel
    Friend WithEvents chkLocalSigature As System.Windows.Forms.CheckBox
    Friend WithEvents chkEnableRemoteScanner As System.Windows.Forms.CheckBox
    Private WithEvents Label211 As System.Windows.Forms.Label
    Private WithEvents Label212 As System.Windows.Forms.Label
    Private WithEvents btnRefreshScanners As System.Windows.Forms.Button
    Friend WithEvents pnlRemoteScan As System.Windows.Forms.GroupBox
    Private WithEvents Label233 As System.Windows.Forms.Label
    Private WithEvents txtRemoteStartX As System.Windows.Forms.TextBox
    Private WithEvents cmbRemoteSupportedSize As System.Windows.Forms.ComboBox
    Private WithEvents txtRemoteStartY As System.Windows.Forms.TextBox
    Private WithEvents Label234 As System.Windows.Forms.Label
    Private WithEvents Label235 As System.Windows.Forms.Label
    Private WithEvents Label236 As System.Windows.Forms.Label
    Private WithEvents Label237 As System.Windows.Forms.Label
    Private WithEvents chkRemoteShowScannerDialog As System.Windows.Forms.CheckBox
    Private WithEvents txtRemoteCardWidth As System.Windows.Forms.TextBox
    Private WithEvents Label238 As System.Windows.Forms.Label
    Private WithEvents txtRemoteCardLength As System.Windows.Forms.TextBox
    Private WithEvents Label239 As System.Windows.Forms.Label
    Private WithEvents Label240 As System.Windows.Forms.Label
    Private WithEvents cmbRemoteBitDepth As System.Windows.Forms.ComboBox
    Private WithEvents Label241 As System.Windows.Forms.Label
    Private WithEvents Label242 As System.Windows.Forms.Label
    Private WithEvents Label243 As System.Windows.Forms.Label
    Private WithEvents Label244 As System.Windows.Forms.Label
    Private WithEvents Label245 As System.Windows.Forms.Label
    Private WithEvents Label246 As System.Windows.Forms.Label
    Private WithEvents cmbRemoteContrast As System.Windows.Forms.ComboBox
    Private WithEvents Label247 As System.Windows.Forms.Label
    Private WithEvents Label248 As System.Windows.Forms.Label
    Private WithEvents cmbRemoteScanSide As System.Windows.Forms.ComboBox
    Private WithEvents cmbRemoteBrightness As System.Windows.Forms.ComboBox
    Private WithEvents Label249 As System.Windows.Forms.Label
    Private WithEvents Label250 As System.Windows.Forms.Label
    Friend WithEvents GroupBox24 As System.Windows.Forms.GroupBox
    Friend WithEvents RadioButton1 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton2 As System.Windows.Forms.RadioButton
    Private WithEvents cmbRemoteScanMode As System.Windows.Forms.ComboBox
    Private WithEvents cmbRemoteResolution As System.Windows.Forms.ComboBox
    Private WithEvents cmbRemoteScanner As System.Windows.Forms.ComboBox
    Friend WithEvents Panel31 As System.Windows.Forms.Panel
    Friend WithEvents GroupBox23 As System.Windows.Forms.GroupBox
    Private WithEvents cmbImageFormat As System.Windows.Forms.ComboBox
    Private WithEvents Label109 As System.Windows.Forms.Label
    Private WithEvents cmbRemoteImageFormat As System.Windows.Forms.ComboBox
    Private WithEvents Label110 As System.Windows.Forms.Label
    Friend WithEvents chkEliminatePegasus As System.Windows.Forms.CheckBox
    Private WithEvents btnRefreshTwainScanners As System.Windows.Forms.Button
    Private WithEvents GroupBox25 As System.Windows.Forms.GroupBox
    Friend WithEvents chkClearDashboardSearch As System.Windows.Forms.CheckBox
    Friend WithEvents chkZipMetadata As System.Windows.Forms.CheckBox
    Friend WithEvents chkZipScannerSettings As System.Windows.Forms.CheckBox
    Private WithEvents cmbRemoteScanSideFeeder As System.Windows.Forms.ComboBox
    Friend WithEvents GroupBox26 As System.Windows.Forms.GroupBox
    Friend WithEvents chkEnableLocalWelchAllynECGDevice As System.Windows.Forms.CheckBox
    Private WithEvents cmbNoTemplatesJob As System.Windows.Forms.ComboBox
    Private WithEvents Label213 As System.Windows.Forms.Label
    Friend WithEvents GroupBox27 As System.Windows.Forms.GroupBox
    Friend WithEvents chkGreyScreenIssue As System.Windows.Forms.CheckBox
    Private WithEvents TextBox6 As System.Windows.Forms.TextBox
    Friend WithEvents Label214 As System.Windows.Forms.Label
    Private WithEvents TextBox7 As System.Windows.Forms.TextBox
    Friend WithEvents Label215 As System.Windows.Forms.Label
    Private WithEvents TextBox8 As System.Windows.Forms.TextBox
    Friend WithEvents Label216 As System.Windows.Forms.Label

End Class
